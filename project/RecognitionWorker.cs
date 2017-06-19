using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Management;
using System.Runtime.Remoting.Channels;

namespace MultiFaceRec
{
    class RecognitionWorker
    {
        readonly HaarCascade _gFace;
        private readonly Dictionary<string, Image<Gray, byte>> _gTrainedImages;

        private readonly string _gFormattedDbPath;
        private readonly string _gSourceDbPath;
        private readonly string _gTestDbPath;

        public int GFacesNotRecognizedCounter = 0;
        public int GfrrCounter = 0;
        public int GfarCounter = 0;
        public int GCorrectCounter = 0;

        private Dictionary<string, float> _distances;


        private readonly RecognitionWorker.Log _gLogger;

        public delegate void Log(string message);

        private techcode.EigenObjectRecognizer recognizer;

        public RecognitionWorker(string sourceDbPath, string formattedDbPath, string testDbPath, Log gLogger)
        {
            // load haar cascade for face recognition
            _gFace = new HaarCascade("haarcascade_frontalface_default.xml");
            _gTrainedImages = new Dictionary<string, Image<Gray, byte>>();

            _gFormattedDbPath = formattedDbPath;
            _gSourceDbPath = sourceDbPath;
            _gTestDbPath = testDbPath;

            _gLogger = gLogger;
            _distances = new Dictionary<string, float>();

            LoadDefaultSettings(_gFormattedDbPath);
        }

        public void LoadDefaultSettings(string formattedDbPath)
        {
            BackgroundWorker bw = new BackgroundWorker() { WorkerReportsProgress = true };
            bw.DoWork += new DoWorkEventHandler(
                (object sender, DoWorkEventArgs e) =>
                {
                    BackgroundWorker bw1 = sender as BackgroundWorker;
                    try
                    {
                        //Load of previus trainned faces and labels for each image
                        var trainedLabelsPath = Path.Combine(formattedDbPath,
                            MainSettings.Default.trainedLabelsFileName);

                        if (!File.Exists(trainedLabelsPath))
                            throw new FileNotFoundException($"{MainSettings.Default.trainedLabelsFileName} not exists");

                        // loading all label rows, contains filenames
                        var labelsInfo = File.ReadAllText(trainedLabelsPath);
                        var labels = labelsInfo.Split(';');

                        int current = 0;
                        decimal total = labels.Count();
                        decimal in1 = Math.Ceiling(total/100);

                        // loading each image in a dictionary
                        foreach (var label in labels)
                        {
                            var progress = current/in1;
                            if (progress <= 100)
                                bw1.ReportProgress(Convert.ToInt32(progress));

                            if (string.IsNullOrEmpty(label)) continue;

                            var trainingImagePath = Path.Combine(formattedDbPath, label + ".bmp");

                            if (!File.Exists(trainingImagePath))
                                throw new FileNotFoundException();

                            // saving image to dictionary
                            if (!_gTrainedImages.ContainsKey(label))
                                _gTrainedImages.Add(label, new Image<Gray, byte>(new Bitmap(trainingImagePath)));

                            current++;
                        }

                        //_gLogger.Invoke($"Default settings loaded, {_gTrainedImages.Count} images ready! for work\n");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                });

            bw.ProgressChanged += new ProgressChangedEventHandler(
                (object sender, ProgressChangedEventArgs args) => _gLogger.Invoke($"Completed {args.ProgressPercentage}% of config loading\n"));
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                (object sender, RunWorkerCompletedEventArgs args) => MessageBox.Show("Loading default settings completed!"));
            bw.RunWorkerAsync();
        }

        public Dictionary<Rectangle, Image<Gray, byte>> ExtractFaces(string filePath)
        {
            // load source image and convert it to gray
            var img = Image.FromFile(filePath);
            var tImg = new Image<Bgr, byte>(new Bitmap(img));
            var grayImg = tImg.Convert<Gray, Byte>();

            // trying to detect all faces on gray source image
            MCvAvgComp[][] facesDetected = grayImg.DetectHaarCascade(
             _gFace,
             1.2,
             10,
             Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
             new Size(20, 20));

            // add all detected areas as 100x100 resized images(as value)
            // and source image coord rect (as key) where this image were detected
            return facesDetected[0].ToDictionary(
                f => f.rect, 
                f => tImg.
                    Copy(f.rect).Convert<Gray, byte>().
                    Resize(200, 200, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC)
                    );
        }

        public void TrainAll()
        {
            foreach (var file in Directory.GetFiles(_gSourceDbPath))
            {
                var fileName = Path.GetFileNameWithoutExtension(file);

                if(fileName != null && !_gTrainedImages.ContainsKey(fileName))
                    TrainOne(file);
            }

            _gLogger($"Training stage completed. {GFacesNotRecognizedCounter} not recognized daces\n");
        }

        public void TrainOne(string imagePath)
        {
            var filename = Path.GetFileNameWithoutExtension(imagePath);

            if (filename != null && _gTrainedImages.ContainsKey(filename))
            {
                //_gLogger($"file {filename} already loaded\n");
                return;
            }

            //_gLogger.Invoke($"Image {imagePath} in train progress\n");

            var faces = ExtractFaces(imagePath);

            if (faces.Count == 0)
            {
                //_gLogger.Invoke($"No faces detected on image {imagePath}\n");
                GFacesNotRecognizedCounter++;
                return;
            }

            if (faces.Count > 1)
            {
                //_gLogger.Invoke($"2 faces on one image detected. one will be removed\n");
                faces.Remove(faces.Keys.First());
            }

            // extracting each face from input image
            // resizing to 20x20, making gray
            // and saving it to a folder
            foreach (var face in faces)
            {
                if (filename != null && !_gTrainedImages.ContainsKey(filename)) _gTrainedImages.Add(filename, face.Value);

                if (imagePath == null) continue;
                var imageSavePath = Path.Combine(
                    _gFormattedDbPath,
                    Path.GetFileName(imagePath));

                var labelsSavePath = Path.Combine(
                    _gFormattedDbPath,
                    MainSettings.Default.trainedLabelsFileName);

                face.Value.Save(imageSavePath);
                File.AppendAllText(
                    labelsSavePath, 
                    Path.GetFileNameWithoutExtension(imagePath) + ';');

                //_gLogger.Invoke($"file {imageSavePath} succesfully saved");
            }
        }

        public void RecognizeOne(string imagePath)
        {
            if (_gTrainedImages.Count == 0) throw new Exception("train database not loaded");

            string filename = Path.GetFileNameWithoutExtension(imagePath);

            //_gLogger.Invoke($"Image {filename} recognising\n");

            var faces = ExtractFaces(imagePath);

            if (faces.Count == 0)
            {
                //_gLogger.Invoke($"No faces detected on image {imagePath}\n");
                return;
            }

            if (faces.Count > 1)
            {
                //_gLogger.Invoke($"2 faces on one image detected. one will be removed\n");
                faces.Remove(faces.Keys.First());
            }

            foreach (var face in faces)
            {
                //TermCriteria for face recognition with numbers of trained images like maxIteration
                var termCrit = new MCvTermCriteria(_gTrainedImages.Count, 0.001);

                //Eigen face recognizer
                if(recognizer == null)
                    recognizer = new techcode.EigenObjectRecognizer(
                        _gTrainedImages.Values.ToArray(),
                        _gTrainedImages.Keys.ToArray(),
                        MainSettings.Default.maxLastDistance,
                        ref termCrit);

                float lastDistance = 0;
                var recognizedName = recognizer.Recognize(face.Value, out lastDistance);
                _distances.Add($"{filename}:{recognizedName}", lastDistance);

                if (string.IsNullOrEmpty(recognizedName))
                {
                    // correct
                    GfrrCounter++;
                }
                else
                {
                    if (ExtractGroupPrefix(recognizedName) != ExtractGroupPrefix(filename))
                        GfarCounter++;
                    // CORRECT
                    else
                        GCorrectCounter++;
                }

            }
        }

        public void RecognizeAll()
        {
            BackgroundWorker bw = new BackgroundWorker() {WorkerReportsProgress = true};
            
            bw.DoWork += (object sender, DoWorkEventArgs args) =>
            {
                MessageBox.Show("RecognizeAll started");
                var current = 0;
                decimal total = Directory.GetFiles(_gTestDbPath).Count();
                var in1 = Math.Ceiling(total/100);
                var bw1 = sender as BackgroundWorker;
                foreach (
                    var file in Directory.GetFiles(_gTestDbPath))
                {
                    var fileName = Path.GetFileNameWithoutExtension(file);

                    RecognizeOne(file);

                    var progress = current/in1;

                    if (progress <= 100)
                        bw1.ReportProgress(Convert.ToInt32(progress));

                    current++;
                }
            };
            bw.ProgressChanged +=
                (object sender, ProgressChangedEventArgs args) =>
                    _gLogger.Invoke($"recognition - {args.ProgressPercentage}% completed\n");
            bw.RunWorkerCompleted +=
                (object sender, RunWorkerCompletedEventArgs args) =>
                {
                    _gLogger.Invoke(
                        $"recognition completed: far: {GfarCounter}, frr: {GfrrCounter}, correct: {GCorrectCounter}\n");
                    foreach (var distance in _distances)
                    {
                        _gLogger.Invoke($"{distance.Key}-{distance.Value};\n");
                    }

                };

            bw.RunWorkerAsync();
        }

        public string ExtractGroupPrefix(string filename)
        {
            var splitted = filename.Split('_');
            return splitted[0];
        }
    }
}


////Multiple face detection and recognition in real time
////Using EmguCV cross platform .Net wrapper to the Intel OpenCV image processing library for C#.Net
////Writed by Sergio Andrés Guitérrez Rojas
////"Serg3ant" for the delveloper comunity
//// Sergiogut1805@hotmail.com
////Regards from Bucaramanga-Colombia ;)

//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Windows.Forms;
//using Emgu.CV;
//using Emgu.CV.Structure;
//using Emgu.CV.CvEnum;
//using System.IO;
//using System.Diagnostics;

//namespace MultiFaceRec
//{
//    public partial class FrmPrincipal : Form
//    {
//        //Declararation of all variables, vectors and haarcascades
//        Image<Bgr, Byte> currentFrame;
//        Capture grabber;
//        HaarCascade face;
//        HaarCascade eye;
//        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.5d, 0.5d);
//        Image<Gray, byte> result, TrainedFace = null;
//        Image<Gray, byte> gray = null;
//        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
//        List<string> labels = new List<string>();
//        List<string> NamePersons = new List<string>();
//        int ContTrain, NumLabels, t;
//        float maxLastDistance = 3000;
//        string name, names = null;
//        private int _trainDatabaseNotRecognizedFecesCounter = 0;


//        public FrmPrincipal()
//        {
//            InitializeComponent();
//            //Load haarcascades for face detection
//            face = new HaarCascade("haarcascade_frontalface_default.xml");
//            //eye = new HaarCascade("haarcascade_eye.xml");
//            try
//            {
//                //Load of previus trainned faces and labels for each image
//                string trainedLabels = Path.Combine(MainSettings.Default.databasePath,
//                    MainSettings.Default.processedTrainDatabaseFolderName);

//                trainedLabels = Path.Combine(trainedLabels, MainSettings.Default.trainedLabelsFileName);

//                if (!File.Exists(trainedLabels))
//                {
//                    throw new FileNotFoundException($"{MainSettings.Default.trainedLabelsFileName} not exists");
//                }

//                string labelsinfo = File.ReadAllText(trainedLabels);
//                var Labels = labelsinfo.Split(';');
//                Array.Resize(ref Labels, Labels.Length - 1);

//                if (Labels.Length == 0)
//                {
//                    throw new Exception("TrainedLabs.txt is empty");
//                }

//                NumLabels = Labels.Length;
//                ContTrain = NumLabels;

//                foreach (var label in Labels)
//                {
//                    var imgName = label + ".bmp";
//                    var path = Path.Combine(MainSettings.Default.databasePath,
//                        MainSettings.Default.trainDatabaseFolderName);

//                    path = Path.Combine(path, imgName);

//                    if (!File.Exists(path))
//                    {
//                        throw new Exception($"training image {path} does not exists");
//                    }

//                    trainingImages.Add(new Image<Gray, byte>(path));
//                    labels.Add(label);
//                }

//                rtb_Logs.AppendText($"Loaded {NumLabels} training images from local database\n");

//            }
//            catch (Exception e)
//            {
//                MessageBox.Show(e.ToString());
//                //MessageBox.Show("Nothing in binary database, please add at least a face(Simply train the prototype with the Add Face Button).", "Triained faces load", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//            }

//        }


//        private void button1_Click(object sender, EventArgs e)
//        {
//            //Initialize the capture device
//            grabber = new Capture();
//            grabber.QueryFrame();
//            //grabber.QueryFrame();
//            //Initialize the FrameGraber event
//            Application.Idle += new EventHandler(FrameGrabber);
//            button1.Enabled = false;
//        }

//        private void button3_Click_2(object sender, EventArgs e)
//        {
//            Train();
//        }

//        private void btn_DoRecognition_Click(object sender, EventArgs e)
//        {
//            Recognize();
//        }

//        public Dictionary<Rectangle, Image<Gray, byte>> ExtractFaces(String path)
//        {
//            Dictionary<Rectangle, Image<Gray, byte>> res = new Dictionary<Rectangle, Image<Gray, byte>>();
//            var img = Image.FromFile(path);
//            currentFrame = new Image<Bgr, byte>(new Bitmap(img));
//            gray = currentFrame.Convert<Gray, Byte>();

//            MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(
//             face,
//             1.2,
//             10,
//             Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
//             new Size(20, 20));

//            foreach (var f in facesDetected[0])
//            {
//                res.Add(f.rect, currentFrame.Copy(f.rect).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC));
//            }

//            return res;
//        }

//        private void button2_Click(object sender, System.EventArgs e)
//        {
//            try
//            {
//                //Trained face counter
//                ContTrain = ContTrain + 1;

//                //Get a gray frame from capture device
//                gray = grabber.QueryGrayFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

//                //Face Detector
//                MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(
//                face,
//                1.2,
//                10,
//                Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
//                new Size(20, 20));

//                //Action for each element detected
//                foreach (MCvAvgComp f in facesDetected[0])
//                {
//                    TrainedFace = currentFrame.Copy(f.rect).Convert<Gray, byte>();
//                    break;
//                }

//                //resize face detected image for force to compare the same size with the 
//                //test image with cubic interpolation type method
//                TrainedFace = result.Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
//                trainingImages.Add(TrainedFace);
//                labels.Add(textBox1.Text);

//                //Show face added in gray scale
//                imageBox1.Image = TrainedFace;

//                //Write the number of triained faces in a file text for further load
//                File.WriteAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt", trainingImages.ToArray().Length.ToString() + "%");

//                //Write the labels of triained faces in a file text for further load
//                for (int i = 1; i < trainingImages.ToArray().Length + 1; i++)
//                {
//                    trainingImages.ToArray()[i - 1].Save(Application.StartupPath + "/TrainedFaces/face" + i + ".bmp");
//                    File.AppendAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt", labels.ToArray()[i - 1] + "%");
//                }

//                MessageBox.Show(textBox1.Text + "´s face detected and added :)", "Training OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//            catch
//            {
//                MessageBox.Show("Enable the face detection first", "Training Fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//            }
//        }

//        void Recognize()
//        {
//            String imagePath =
//                Path.Combine(MainSettings.Default.databasePath, MainSettings.Default.testDatabaseFolderName);

//            foreach (var image in Directory.GetFiles(imagePath))
//            {
//                rtb_Logs.AppendText($"Image {image} recognising\n");

//                var faces = ExtractFaces(image);

//                if (faces.Count == 0)
//                {
//                    rtb_Logs.AppendText($"No faces detected on image {image}\n");
//                    return;
//                }

//                foreach (var face in faces)
//                {
//                    if (trainingImages.ToArray().Length != 0)
//                    {
//                        //TermCriteria for face recognition with numbers of trained images like maxIteration
//                        MCvTermCriteria termCrit = new MCvTermCriteria(ContTrain, 0.001);

//                        //Eigen face recognizer
//                        EigenObjectRecognizer recognizer = new EigenObjectRecognizer(
//                            trainingImages.ToArray(),
//                            labels.ToArray(),
//                            3000,
//                            ref termCrit);

//                        float last_distance = 0;
//                        name = recognizer.Recognize(face.Value, out last_distance);

//                        //Draw the label for each face detected and recognized
//                        currentFrame.Draw(name, ref font, new Point(face.Key.X - 2, face.Key.Y - 2), new Bgr(Color.LightGreen));
//                        currentFrame.Draw(face.Key, new Bgr(Color.Red), 2);
//                        currentFrame.Save(image + "_rec.bmp");
//                    }
//                }
//            }

//        }

//        void Train()
//        {
//            foreach (var file in Directory.GetFiles(Path.Combine(MainSettings.Default.databasePath, MainSettings.Default.trainDatabaseFolderName)))
//            {
//                if (labels.Contains(Path.GetFileNameWithoutExtension(file)))
//                    continue;

//                AddImageToTrainDatabase(file);
//            }

//            rtb_Output.AppendText($"on {_trainDatabaseNotRecognizedFecesCounter} images face not detected");
//        }

//        void AddImageToTrainDatabase(String imagePath)
//        {
//            rtb_Logs.AppendText($"Image {imagePath} in train progress\n");

//            var faces = ExtractFaces(imagePath);

//            if (faces.Count == 0)
//            {
//                rtb_Logs.AppendText($"No faces detected on image {imagePath}\n");
//                _trainDatabaseNotRecognizedFecesCounter++;
//                return;
//            }

//            var image = Image.FromFile(imagePath);
//            currentFrame = new Image<Bgr, byte>(new Bitmap(image));

//            foreach (var face in faces)
//            {
//                currentFrame.Draw(face.Key, new Bgr(Color.Red), 2);

//                trainingImages.Add(face.Value);
//                labels.Add(Path.GetFileNameWithoutExtension(imagePath));

//                imageBox1.Image = face.Value;

//                var saveDir = Path.Combine(MainSettings.Default.databasePath,
//                    MainSettings.Default.processedTrainDatabaseFolderName);

//                face.Value.Save(Path.Combine(saveDir, Path.GetFileName(imagePath)));

//                var labelsFile = Path.Combine(saveDir, MainSettings.Default.trainedLabelsFileName);

//                File.AppendAllText(labelsFile, Path.GetFileNameWithoutExtension(imagePath) + ';');
//            }

//            imageBoxFrameGrabber.Image = currentFrame;
//        }

//        void FrameGrabber(object sender, EventArgs e)
//        {
//            label3.Text = "0";
//            //label4.Text = "";
//            NamePersons.Add("");


//            //Get the current frame form capture device
//            currentFrame = grabber.QueryFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);


//            //Convert it to Grayscale
//            gray = currentFrame.Convert<Gray, Byte>();

//            //Face Detector
//            MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(
//              face,
//              1.2,
//              10,
//              Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
//              new Size(20, 20));

//            //Action for each element detected
//            foreach (MCvAvgComp f in facesDetected[0])
//            {
//                t = t + 1;
//                result = currentFrame.Copy(f.rect).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
//                //draw the face detected in the 0th (gray) channel with blue color
//                currentFrame.Draw(f.rect, new Bgr(Color.Red), 2);


//                if (trainingImages.ToArray().Length != 0)
//                {
//                    //TermCriteria for face recognition with numbers of trained images like maxIteration
//                    MCvTermCriteria termCrit = new MCvTermCriteria(ContTrain, 0.001);

//                    //Eigen face recognizer
//                    EigenObjectRecognizer recognizer = new EigenObjectRecognizer(
//                       trainingImages.ToArray(),
//                       labels.ToArray(),
//                       3000,
//                       ref termCrit);

//                    float last_distance = 0;
//                    name = recognizer.Recognize(result, out last_distance);

//                    //Draw the label for each face detected and recognized
//                    currentFrame.Draw(name, ref font, new Point(f.rect.X - 2, f.rect.Y - 2), new Bgr(Color.LightGreen));

//                }

//                NamePersons[t - 1] = name;
//                NamePersons.Add("");


//                //Set the number of faces detected on the scene
//                label3.Text = facesDetected[0].Length.ToString();

//                /*
//                //Set the region of interest on the faces

//                gray.ROI = f.rect;
//                MCvAvgComp[][] eyesDetected = gray.DetectHaarCascade(
//                   eye,
//                   1.1,
//                   10,
//                   Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
//                   new Size(20, 20));
//                gray.ROI = Rectangle.Empty;

//                foreach (MCvAvgComp ey in eyesDetected[0])
//                {
//                    Rectangle eyeRect = ey.rect;
//                    eyeRect.Offset(f.rect.X, f.rect.Y);
//                    currentFrame.Draw(eyeRect, new Bgr(Color.Blue), 2);
//                }
//                 */

//            }
//            t = 0;

//            //Names concatenation of persons recognized
//            for (int nnn = 0; nnn < facesDetected[0].Length; nnn++)
//            {
//                names = names + NamePersons[nnn] + ", ";
//            }

//            //Show the faces procesed and recognized
//            if (facesDetected[0].Length == 0)
//            {
//                rtb_Logs.Text += "No face detected";
//            }

//            imageBoxFrameGrabber.Image = currentFrame;
//            label4.Text = names;
//            names = "";
//            //Clear the list(vector) of names
//            NamePersons.Clear();

//        }

//        private void button3_Click(object sender, EventArgs e)
//        {
//            Process.Start("Donate.html");
//        }

//    }
//}
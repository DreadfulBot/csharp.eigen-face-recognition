using System;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace MultiFaceRec
{
    class FileWorker
    {
        private readonly IDatabaseFiles _databaseFiles;
        private readonly IFormattedFiles _formattedFiles;

        private readonly Logger _gLogger;
        private readonly RecognitionWorker _rw;
        public delegate void Logger(string message);

        public FileWorker(IDatabaseFiles databaseFiles,
            IFormattedFiles formattedFiles, Logger gLogger, RecognitionWorker rw)
        {
            _databaseFiles = databaseFiles;
            _formattedFiles = formattedFiles;
            _gLogger = gLogger;
            _rw = rw;
        }

        public void ProcessGroups()
        {
            BackgroundWorker bw = new BackgroundWorker() {WorkerReportsProgress = true};
            bw.DoWork +=
                (object sender, DoWorkEventArgs args) =>
                {
                    BackgroundWorker bw1 = sender as BackgroundWorker;

                    var current = 0;
                    decimal total = _databaseFiles.GetFileGroupsNames().Count();
                    var in1 = Math.Ceiling(total/100);

                    foreach (var fileGroup in _databaseFiles.GetFileGroupsNames())
                    {
                        //_gLogger.Invoke($"Group {Path.GetFileName(fileGroup)} in progress\n");
                        var files = _databaseFiles.GetFileGroupPhotosNames(fileGroup);

                        // half-images counter of each group
                        var halfBarier = (int) Math.Floor((float) files.Length/2);

                        // moving half files to test folder
                        for (var i = 0; i < halfBarier; i++)
                        {
                            _formattedFiles.AddToTest(files[i]);
                        }

                        // starting to learn system on other half
                        for (var i = halfBarier; i < files.Length; i++)
                        {
                            _rw.TrainOne(files[i]);
                        }

                        var progress = current/in1;

                        if(progress <= 100)
                            bw1.ReportProgress(Convert.ToInt32(progress));

                        current++;

                    }

                    //_gLogger($"Training stage completed. {_rw.GFacesNotRecognizedCounter} not recognized daces\n");
                };

            bw.ProgressChanged +=
                (object sender, ProgressChangedEventArgs args) =>
                    _gLogger.Invoke($"Progress groups - {args.ProgressPercentage} completed\n");

            bw.RunWorkerAsync();
        }
    }
}

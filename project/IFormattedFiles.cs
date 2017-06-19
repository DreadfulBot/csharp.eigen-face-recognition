using System.IO;

namespace MultiFaceRec
{
    interface IFormattedFiles
    {
        void AddToTest(string srcFile);
    }

    public class LocalFormattedFiles : IFormattedFiles
    {
        private readonly string _testDirPath ;

        private readonly Logger _logger;
        public delegate void Logger(string message);

        public LocalFormattedFiles(string testDirPath, Logger logger)
        {
            _testDirPath = testDirPath;
            _logger = logger;
        }

        public void AddToTest(string srcFile)
        {
            var targetFileName = Path.Combine(_testDirPath, Path.GetFileName(srcFile));
            if (!File.Exists(targetFileName))
            {
                File.Copy(srcFile, targetFileName);
            }
            else
            {
                //_logger.Invoke($"Файл {srcFile} уже доступен в директории теста");
            }
        }
    }
}

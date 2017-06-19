using System.IO;

namespace MultiFaceRec
{
    interface IDatabaseFiles
    {
        string[] GetFileGroupsNames();

        string[] GetFileGroupPhotosNames(string fileGroupPath);

    }

    public class LocalDatabaseFiles : IDatabaseFiles
    {
        private readonly string _databasePath;
        // databasePath - директория с группами тестовых изображений
        public LocalDatabaseFiles(string databasePath)
        {
            _databasePath = databasePath;
            if(!Directory.Exists(_databasePath))
                throw new DirectoryNotFoundException("Директория с базой данных изображение не существует");
        }

        public virtual string[] GetFileGroupsNames()
        {
            var di = Directory.GetDirectories(_databasePath);
            return di;
        }

        public virtual string[] GetFileGroupPhotosNames(string fileGroupPath)
        {
            var di = Directory.GetFiles(fileGroupPath);
            return di;
        }
    }
}

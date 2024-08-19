using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.WorkflowTestFramework.Helpers
{
    public class FileIOHelper
    {
        /// <summary>
        /// Validate if a file with the given name exists inside the directory folder
        /// </summary>
        /// <param name="DirectoryPath"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public bool ValidateIfFileExists(string DirectoryPath, string FileName)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(DirectoryPath);

            FileInfo[] files = dirInfo.GetFiles(FileName, SearchOption.TopDirectoryOnly);

            if (files.Length == 1)
                return true;

            return false;
        }

        public List<string> GetFilesPath(string DirectoryPath, string SearchPatther)
        {
            List<string> filesPath = new List<string>();

            DirectoryInfo dirInfo = new DirectoryInfo(DirectoryPath);

            FileInfo[] files = dirInfo.GetFiles(SearchPatther, SearchOption.TopDirectoryOnly);

            foreach (FileInfo file in files)
                filesPath.Add(file.FullName);

            return filesPath;
        }

        public string[] OpenFileAndReadAllLines(string DirectoryPath, string FileName)
        {

            DirectoryInfo dirInfo = new DirectoryInfo(DirectoryPath);

            FileInfo file = dirInfo.GetFiles(FileName, SearchOption.TopDirectoryOnly).FirstOrDefault();

            return File.ReadAllLines(file.FullName);

        }

        /// <summary>
        /// Create the directory if it does not exists.
        /// If the directory already exists then the method will remove all files inside the directory
        /// </summary>
        /// <param name="Directory"></param>
        public void CreateDirectoryAndRemoveFiles(string Directory)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Directory);

            if (!dirInfo.Exists)
                dirInfo.Create();

            foreach (var file in dirInfo.EnumerateFiles())
            {
                FileInfo f = new FileInfo(file.FullName);
                f.Delete();
            }

        }

        public byte[] ReadFileIntoByteArray(string filePath)
        {
            return System.IO.File.ReadAllBytes(filePath);

        }
    }
}

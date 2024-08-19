using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO.Compression;

namespace Phoenix.UITests.Framework.FileSystem
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

        /// <summary>
        /// Validate if a file with the given name exists inside a Network Directory Folder
        /// </summary>
        /// <param name="DirectoryPath"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public bool ValidateIfFileExists(string DirectoryPath, string FileName, string Username, String Password)
        {

            using (NetworkConnection con = new NetworkConnection(DirectoryPath, new NetworkCredential(Username, Password)))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(DirectoryPath);

                FileInfo[] files = dirInfo.GetFiles(FileName, SearchOption.TopDirectoryOnly);

                if (files.Length >= 1)
                    return true;

                return false;
            }
        }

        /// <summary>
        /// Open a file inside a Network Directory Folder and read all lines
        /// </summary>
        /// <param name="DirectoryPath"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public int CountTotalFiles(string DirectoryPath, string FileName, string Username, String Password)
        {

            using (NetworkConnection con = new NetworkConnection(DirectoryPath, new NetworkCredential(Username, Password)))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(DirectoryPath);

                return dirInfo.GetFiles(FileName, SearchOption.TopDirectoryOnly).Count();

            }
        }

        public string[] OpenFileAndReadAllLines(string DirectoryPath, string FileName)
        {

            DirectoryInfo dirInfo = new DirectoryInfo(DirectoryPath);

            FileInfo file = dirInfo.GetFiles(FileName, SearchOption.TopDirectoryOnly).FirstOrDefault();

            return File.ReadAllLines(file.FullName);
            
        }

        /// <summary>
        /// Open a file inside a Network Directory Folder and read all lines
        /// </summary>
        /// <param name="DirectoryPath"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public string[] OpenFileAndReadAllLines(string DirectoryPath, string FileName, string Username, String Password)
        {

            using (NetworkConnection con = new NetworkConnection(DirectoryPath, new NetworkCredential(Username, Password)))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(DirectoryPath);

                FileInfo file = dirInfo.GetFiles(FileName, SearchOption.TopDirectoryOnly).FirstOrDefault();
                
                return File.ReadAllLines(file.FullName);
            }
        }

        /// <summary>
        /// Open a file inside a Network Directory Folder and read all lines
        /// </summary>
        /// <param name="DirectoryPath"></param>
        /// <param name="FileName"></param>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <param name="FilePosition">The position if the file we want to open. If multiple files match the file name pattern then this property will identify what file to open</param>
        /// <returns></returns>
        public string[] OpenFileAndReadAllLines(string DirectoryPath, string FileName, string Username, String Password, int FilePosition)
        {

            using (NetworkConnection con = new NetworkConnection(DirectoryPath, new NetworkCredential(Username, Password)))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(DirectoryPath);

                FileInfo file = dirInfo.GetFiles(FileName, SearchOption.TopDirectoryOnly)[FilePosition];

                return File.ReadAllLines(file.FullName);
            }
        }

        /// <summary>
        /// Delete all files in the Network directory path that match the search pattern
        /// </summary>
        /// <param name="DirectoryPath"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public void DeleteFiles(string DirectoryPath, string FileName, string Username, String Password)
        {

            using (NetworkConnection con = new NetworkConnection(DirectoryPath, new NetworkCredential(Username, Password)))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(DirectoryPath);

                FileInfo[] files = dirInfo.GetFiles(FileName, SearchOption.TopDirectoryOnly);

                foreach (FileInfo file in files)
                {
                    File.Delete(file.FullName);
                }

            }
        }

        /// <summary>
        /// Create the directory if it does not exists.
        /// If the directory already exists then the method will remove all files inside the directory
        /// </summary>
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

        /// <summary>
        /// Create the directory if it does not exists.
        /// </summary>
        public void CreateDirectory(string DirectoryPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(DirectoryPath);

            if (!dirInfo.Exists)
                dirInfo.Create();
        }

        /// <summary>
        /// Picks a zip file and extract its content to the extract folder
        /// </summary>
        /// <param name="zipPath"></param>
        /// <param name="extractPath">the folder that will have the zip content</param>
        public void UnzipFile(string zipPath, string extractPath)
        {
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    // Gets the full path to ensure that relative segments are removed.
                    string destinationPath = Path.GetFullPath(Path.Combine(extractPath, entry.FullName));

                    // Ordinal match is safest, case-sensitive volumes can be mounted within volumes that are case-insensitive.
                    if (destinationPath.StartsWith(extractPath, StringComparison.Ordinal))
                        entry.ExtractToFile(destinationPath);
                    
                }
            }

        }

        public byte[] ReadFileIntoByteArray(string filePath)
        {
            return System.IO.File.ReadAllBytes(filePath);

        }

        public bool WaitForFileToExist(string DirectoryPath, string FileName, int TotalTries)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(DirectoryPath);

            int count = 0;
            while(count < TotalTries)
            {
                FileInfo[] files = dirInfo.GetFiles(FileName, SearchOption.TopDirectoryOnly);

                if (files.Length == 1)
                    return true;

                System.Threading.Thread.Sleep(1000);
                count++;
            }

            return false;
        }
    }
}

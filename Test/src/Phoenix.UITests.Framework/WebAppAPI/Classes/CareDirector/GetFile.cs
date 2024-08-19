using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.UITests.Framework.WebAppAPI.Interfaces;
using RestSharp;

namespace Phoenix.UITests.Framework.WebAppAPI.Classes
{
    internal class GetFile : IGetFile
    {

        string getFileURL = "getfile/1/";

        public void DownloadZipFile(string FileToDownload, string DownloadPath, string AuthenticationCookie)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("appURL");
            var appUri = new Uri(baseURL);
            string domain = baseURL.Replace("https://", "").Replace("http://", "").Replace("/", "");

            var client = new RestClient(baseURL);

            var request = new RestRequest(getFileURL + FileToDownload);
            request.AddHeader("Accept", "application/zip");

            //request.AddParameter("CareDirectorWebAuth", AuthenticationCookie, ParameterType.Cookie);
            //client.AddCookie("CareDirectorWebAuth", AuthenticationCookie, "phoenixqa.careworks.ie", request.Resource);
            System.Net.Cookie authCookie = new System.Net.Cookie("CareDirectorWebAuth", AuthenticationCookie, "/", domain);
            request.CookieContainer = new System.Net.CookieContainer();
            request.CookieContainer.Add(appUri, authCookie);

            //var fileBytes = client.DownloadData(request, true);
            //System.IO.File.WriteAllBytes(DownloadPath, fileBytes);


            byte[] fileBytes = client.DownloadData(request);
            
            System.IO.File.WriteAllBytes(DownloadPath + "file.zip", fileBytes);

            System.IO.Stream memoryStram = new System.IO.MemoryStream(fileBytes);

            System.IO.Stream unzippedEntryStream;
            ZipArchive archive = new ZipArchive(memoryStram);
            foreach (ZipArchiveEntry zipEntry in archive.Entries)
            {
                unzippedEntryStream = zipEntry.Open();

                using (var fileStream = System.IO.File.Create(DownloadPath + zipEntry.Name))
                {
                    unzippedEntryStream.CopyTo(fileStream);                    
                }
            }
        }

        
    }
}
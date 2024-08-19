using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.UITests.Framework.WebAppAPI.Interfaces;
using Phoenix.UITests.Framework.WebAppAPI.Classes;

namespace Phoenix.UITests.Framework.WebAppAPI.Proxies
{
    public class GetFileProxy : IGetFile
    {
        public GetFileProxy()
        {
            _getFile = new GetFile();
        }

        private IGetFile _getFile;


        public void DownloadZipFile(string FileToDownload, string DownloadPath, string AuthenticationCookie)
        {
            _getFile.DownloadZipFile(FileToDownload, DownloadPath, AuthenticationCookie);
        }
    }
}

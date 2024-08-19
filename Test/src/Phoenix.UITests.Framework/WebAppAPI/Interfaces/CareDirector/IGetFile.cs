using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.WebAppAPI.Interfaces
{
    interface IGetFile
    {
        void DownloadZipFile(string FileToDownload, string DownloadPath, string AuthenticationCookie);
    }
}

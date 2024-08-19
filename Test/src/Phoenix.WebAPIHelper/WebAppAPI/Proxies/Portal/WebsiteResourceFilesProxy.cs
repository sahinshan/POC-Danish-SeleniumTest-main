using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.WebAPIHelper.WebAppAPI.Interfaces;
using Phoenix.WebAPIHelper.WebAppAPI.Classes;

namespace Phoenix.WebAPIHelper.WebAppAPI.Proxies
{
    public class WebsiteResourceFilesProxy
    {
        public WebsiteResourceFilesProxy()
        {
            _WebsiteResourceFiles = new WebsiteResourceFiles();
        }


        private IWebsiteResourceFiles _WebsiteResourceFiles;


        public string GetResourceFileByName(Guid WebsiteID, string ResourceFileName, string SecurityToken)
        {
            return _WebsiteResourceFiles.GetResourceFileByName(WebsiteID, ResourceFileName, SecurityToken);
        }

    }
}

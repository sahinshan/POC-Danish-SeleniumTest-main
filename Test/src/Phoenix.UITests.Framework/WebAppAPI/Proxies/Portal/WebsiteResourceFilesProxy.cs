using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.UITests.Framework.WebAppAPI.Interfaces;
using Phoenix.UITests.Framework.WebAppAPI.Classes;

namespace Phoenix.UITests.Framework.WebAppAPI.Proxies
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

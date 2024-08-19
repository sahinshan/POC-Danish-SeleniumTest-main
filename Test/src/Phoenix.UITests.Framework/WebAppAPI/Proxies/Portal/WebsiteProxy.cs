using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.UITests.Framework.WebAppAPI.Interfaces;
using Phoenix.UITests.Framework.WebAppAPI.Classes;

namespace Phoenix.UITests.Framework.WebAppAPI.Proxies
{
    public class WebsiteProxy
    {
        public WebsiteProxy()
        {
            _Website = new Website();
        }


        private IWebsite _Website;


        public Entities.Portal.Website GetWebsiteInfo(Guid WebsiteID, string SecurityToken)
        {
            return _Website.GetWebsiteInfo(WebsiteID, SecurityToken);
        }

        public Entities.Portal.Website GetWebsiteInfo(string WebsiteID, string SecurityToken)
        {
            return _Website.GetWebsiteInfo(WebsiteID, SecurityToken);
        }

    }
}

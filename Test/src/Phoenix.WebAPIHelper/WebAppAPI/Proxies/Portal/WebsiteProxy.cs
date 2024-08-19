using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.WebAPIHelper.WebAppAPI.Interfaces;
using Phoenix.WebAPIHelper.WebAppAPI.Classes;

namespace Phoenix.WebAPIHelper.WebAppAPI.Proxies
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

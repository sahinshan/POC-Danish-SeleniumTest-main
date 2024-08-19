using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.WebAPIHelper.WebAppAPI.Interfaces;
using Phoenix.WebAPIHelper.WebAppAPI.Classes;

namespace Phoenix.WebAPIHelper.WebAppAPI.Proxies
{
    public class WebsitePagesProxy
    {
        public WebsitePagesProxy()
        {
            _IWebsitePages = new WebsitePages();
        }


        private IWebsitePages _IWebsitePages;


        public List<Entities.Portal.WebsitePage> GetWebsitePagesInfo(Guid WebsiteID, string SecurityToken)
        {
            return _IWebsitePages.GetWebsitePagesInfo(WebsiteID, SecurityToken);
        }

        public Entities.Portal.WebsitePage GetWebsitePage(Guid WebsiteID, string PageName, string SecurityToken, string PortalUserToken)
        {
            return _IWebsitePages.GetWebsitePage(WebsiteID, PageName, SecurityToken, PortalUserToken);
        }

    }
}

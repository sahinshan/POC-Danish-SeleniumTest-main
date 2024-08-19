using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.WebAPIHelper.WebAppAPI.Interfaces
{
    interface IWebsitePages
    {
        List<Entities.Portal.WebsitePage> GetWebsitePagesInfo(Guid WebsiteID, string SecurityToken);
        Entities.Portal.WebsitePage GetWebsitePage(Guid WebsiteID, string PageName, string SecurityToken, string PortalUserToken);
    }
}

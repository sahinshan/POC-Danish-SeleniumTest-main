using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.WebAppAPI.Interfaces
{
    interface IWebsite
    {
        Entities.Portal.Website GetWebsiteInfo(Guid WebsiteID, string SecurityToken);
        Entities.Portal.Website GetWebsiteInfo(string WebsiteID, string SecurityToken);
    }
}

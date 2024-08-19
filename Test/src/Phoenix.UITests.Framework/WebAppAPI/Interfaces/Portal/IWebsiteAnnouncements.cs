using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.WebAppAPI.Interfaces
{
    interface IWebsiteAnnouncements
    {
        List<Entities.Portal.WebsiteAnnouncements> Announcements(Guid WebsiteID, string SecurityToken);
        List<Entities.Portal.WebsiteAnnouncements> Announcements(string WebsiteID, string SecurityToken);
    }
}

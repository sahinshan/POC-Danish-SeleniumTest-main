using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.UITests.Framework.WebAppAPI.Interfaces;
using Phoenix.UITests.Framework.WebAppAPI.Classes;

namespace Phoenix.UITests.Framework.WebAppAPI.Proxies
{
    public class WebsiteAnnouncementsProxy
    {
        public WebsiteAnnouncementsProxy()
        {
            _WebsiteAnnouncements = new WebsiteAnnouncements();
        }


        private IWebsiteAnnouncements _WebsiteAnnouncements;


        public List<Entities.Portal.WebsiteAnnouncements> Announcements(Guid WebsiteID, string SecurityToken)
        {
            return _WebsiteAnnouncements.Announcements(WebsiteID, SecurityToken);
        }

        public List<Entities.Portal.WebsiteAnnouncements> Announcements(string WebsiteID, string SecurityToken)
        {
            return _WebsiteAnnouncements.Announcements(WebsiteID, SecurityToken);
        }

    }
}

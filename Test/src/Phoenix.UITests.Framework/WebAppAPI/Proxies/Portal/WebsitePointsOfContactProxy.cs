using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.UITests.Framework.WebAppAPI.Interfaces;
using Phoenix.UITests.Framework.WebAppAPI.Classes;

namespace Phoenix.UITests.Framework.WebAppAPI.Proxies
{
    public class WebsitePointsOfContactProxy
    {
        public WebsitePointsOfContactProxy()
        {
            _WebsitePointsOfContact = new WebsitePointsOfContact();
        }


        private IWebsitePointsOfContact _WebsitePointsOfContact;


        public List<Entities.Portal.WebsitePointOfContact> PointsOfContacts(Guid WebsiteID, string SecurityToken)
        {
            return _WebsitePointsOfContact.PointsOfContacts(WebsiteID, SecurityToken);
        }

    }
}

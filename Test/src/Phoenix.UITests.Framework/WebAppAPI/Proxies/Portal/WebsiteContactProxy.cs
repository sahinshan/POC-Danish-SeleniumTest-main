using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.UITests.Framework.WebAppAPI.Interfaces;
using Phoenix.UITests.Framework.WebAppAPI.Classes;

namespace Phoenix.UITests.Framework.WebAppAPI.Proxies
{
    public class WebsiteContactProxy
    {
        public WebsiteContactProxy()
        {
            _WebsiteContact = new WebsiteContact();
        }


        private IWebsiteContact _WebsiteContact;


        public Guid CreateContact(Entities.Portal.WebsiteContact WebsiteContact, Guid WebsiteID, string SecurityToken)
        {
            return _WebsiteContact.CreateContact(WebsiteContact, WebsiteID, SecurityToken);
        }

    }
}

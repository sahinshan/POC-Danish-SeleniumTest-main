using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.WebAPIHelper.WebAppAPI.Interfaces;
using Phoenix.WebAPIHelper.WebAppAPI.Classes;

namespace Phoenix.WebAPIHelper.WebAppAPI.Proxies
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.WebAPIHelper.WebAppAPI.Entities.Portal
{
    public class WebsiteContact
    {
        public WebsiteContact() { }

        public WebsiteContact(string name, string email, string Message, string Subject, Guid WebsitePointOfContactId) 
        {
            this.email = email;
            this.message = Message;
            this.subject = Subject;
            this.websitepointofcontactid = WebsitePointOfContactId;
        }

        public string email { get; set; }
        public string message { get; set; }
        public string subject { get; set; }
        public Guid websitepointofcontactid { get; set; }
    }
}

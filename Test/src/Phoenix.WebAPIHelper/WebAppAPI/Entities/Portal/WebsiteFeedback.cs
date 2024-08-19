using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.WebAPIHelper.WebAppAPI.Entities.Portal
{
    public class WebsiteFeedback
    {
        public WebsiteFeedback() { }

        public WebsiteFeedback(string name, string email, string Message, Guid websitefeedbacktypeid) 
        {
            this.name = name;
            this.email = email;
            this.message = Message;
            this.feedbacktypeid = websitefeedbacktypeid;
        }


        public string name { get; set; }
        public string email { get; set; }
        public string message { get; set; }
        public Guid feedbacktypeid { get; set; }
    }
}

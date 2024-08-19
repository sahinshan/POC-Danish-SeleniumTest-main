using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.WebAPIHelper.WebAppAPI.Entities.Portal
{
    public class WebsiteUserLogin
    {
        public WebsiteUserLogin() { }


        public string ApplicationKey { get; set; }
        public string ApplicationSecret { get; set; }
        public Guid EnvironmentId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class WebsiteUserAccessData
    {
        public WebsiteUserAccessData() { }


        public string AccessToken { get; set; }
        public DateTime? ExpireOn { get; set; }
        public Guid? PinId { get; set; }
        public string Message { get; set; }

    }
}

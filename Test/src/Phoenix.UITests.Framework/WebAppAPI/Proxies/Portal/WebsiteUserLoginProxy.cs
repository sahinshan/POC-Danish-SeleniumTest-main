using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.UITests.Framework.WebAppAPI.Interfaces;
using Phoenix.UITests.Framework.WebAppAPI.Classes;

namespace Phoenix.UITests.Framework.WebAppAPI.Proxies
{
    public class WebsiteUserLoginProxy
    {
        public WebsiteUserLoginProxy()
        {
            _WebsiteUserLogin = new WebsiteUserLogin();
        }


        private IWebsiteUserLogin _WebsiteUserLogin;


        public Entities.Portal.WebsiteUserAccessData LoginUser(Guid WebsiteID, Entities.Portal.WebsiteUserLogin UserLoginInfo, string SecurityToken)
        {
            return _WebsiteUserLogin.LoginUser(WebsiteID, UserLoginInfo, SecurityToken);
        }

        public Entities.Portal.WebsiteUserAccessData ValidatePin(Guid WebsiteID, Entities.Portal.WebsiteUserPin UserPinInfo, string SecurityToken)
        {
            return _WebsiteUserLogin.ValidatePin(WebsiteID, UserPinInfo, SecurityToken);
        }
        public Entities.Portal.WebsiteUserAccessData ReissuePin(Guid WebsiteID, Entities.Portal.WebsiteUserReIssuePin reissuePin, string SecurityToken)
        {
            return _WebsiteUserLogin.ReissuePin(WebsiteID, reissuePin, SecurityToken);
        }
        

    }
}

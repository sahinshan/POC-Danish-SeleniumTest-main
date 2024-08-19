using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.WebAPIHelper.WebAppAPI.Interfaces
{
    interface IWebsiteUserLogin
    {
        Entities.Portal.WebsiteUserAccessData LoginUser(Guid WebsiteID, Entities.Portal.WebsiteUserLogin UserLoginInfo, string SecurityToken);
        
        Entities.Portal.WebsiteUserAccessData ValidatePin(Guid WebsiteID, Entities.Portal.WebsiteUserPin UserPinInfo, string SecurityToken);
        Entities.Portal.WebsiteUserAccessData ReissuePin(Guid WebsiteID, Entities.Portal.WebsiteUserReIssuePin reissuePin, string SecurityToken);
    }
}

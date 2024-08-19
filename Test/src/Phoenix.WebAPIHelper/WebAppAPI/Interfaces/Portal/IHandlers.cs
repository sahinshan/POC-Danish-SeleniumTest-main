using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.WebAPIHelper.WebAppAPI.Interfaces
{
    interface IHandlers
    {
        Entities.Portal.HandlerResponse ExecuteCode(Entities.Portal.ExecuteWebsiteHandlerRequest HandlerRequest, Guid websiteId, string SecurityToken, string PortalUserToken);
        
    }
}

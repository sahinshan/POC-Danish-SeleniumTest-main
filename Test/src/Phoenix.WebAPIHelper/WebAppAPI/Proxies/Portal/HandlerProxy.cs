using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.WebAPIHelper.WebAppAPI.Interfaces;
using Phoenix.WebAPIHelper.WebAppAPI.Classes;

namespace Phoenix.WebAPIHelper.WebAppAPI.Proxies
{
    public class HandlersProxy
    {
        public HandlersProxy()
        {
            _handlers = new Handlers();
        }


        private IHandlers _handlers;


        public Entities.Portal.HandlerResponse ExecuteCode(Entities.Portal.ExecuteWebsiteHandlerRequest HandlerRequest, Guid websiteId, string SecurityToken, string PortalUserToken)
        {
            return _handlers.ExecuteCode(HandlerRequest, websiteId, SecurityToken, PortalUserToken);
        }
    }
}

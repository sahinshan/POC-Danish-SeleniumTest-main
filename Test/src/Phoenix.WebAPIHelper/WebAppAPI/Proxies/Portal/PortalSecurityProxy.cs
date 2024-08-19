using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.WebAPIHelper.WebAppAPI.Interfaces;
using Phoenix.WebAPIHelper.WebAppAPI.Classes;

namespace Phoenix.WebAPIHelper.WebAppAPI.Proxies
{
    public class PortalSecurityProxy
    {
        public PortalSecurityProxy()
        {
            _portalSecurity = new PortalSecurity();
        }


        private IPortalSecurity _portalSecurity;


        public string Token { get; set; }


        public string GetToken()
        {
            this.Token = _portalSecurity.GetToken();

            return this.Token;
        }

    }
}

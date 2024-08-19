using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.UITests.Framework.WebAppAPI.Interfaces;
using Phoenix.UITests.Framework.WebAppAPI.Classes;

namespace Phoenix.UITests.Framework.WebAppAPI.Proxies
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.UITests.Framework.WebAppAPI.Interfaces;
using Phoenix.UITests.Framework.WebAppAPI.Classes;

namespace Phoenix.UITests.Framework.WebAppAPI.Proxies
{
    public class AuditProxy : IAudit
    {
        public AuditProxy()
        {
            _audit = new Audit();
        }

        private IAudit _audit;


        public Entities.CareDirector.AuditResponse RetrieveAudits(Entities.CareDirector.AuditSearch AuditSearch, string AuthenticationCookie)
        {
            return _audit.RetrieveAudits(AuditSearch, AuthenticationCookie);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.WebAppAPI.Interfaces
{
    interface IAudit
    {
        Phoenix.UITests.Framework.WebAppAPI.Entities.CareDirector.AuditResponse RetrieveAudits(Phoenix.UITests.Framework.WebAppAPI.Entities.CareDirector.AuditSearch AuditSearch, string AuthenticationCookie);
    }
}

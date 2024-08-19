using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.WebAppAPI.Interfaces
{
    interface ICacheMonitor
    {
        void Execute(string CWCacheKey, string CWServerName, string CWCacheItemName, string AuthenticationCookie);
    }
}

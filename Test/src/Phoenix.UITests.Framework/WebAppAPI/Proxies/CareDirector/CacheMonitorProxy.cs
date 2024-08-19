using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.UITests.Framework.WebAppAPI.Interfaces;
using Phoenix.UITests.Framework.WebAppAPI.Classes;

namespace Phoenix.UITests.Framework.WebAppAPI.Proxies
{
    public class CacheMonitorProxy : ICacheMonitor
    {
        public CacheMonitorProxy()
        {
            _CacheMonitorClass = new CacheMonitor();
        }

        private ICacheMonitor _CacheMonitorClass;


        public void Execute(string CWCacheKey, string CWServerName, string CWCacheItemName, string AuthenticationCookie)
        {
            _CacheMonitorClass.Execute(CWCacheKey, CWServerName, CWCacheItemName, AuthenticationCookie);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.UITests.Framework.WebAppAPI.Interfaces;
using Phoenix.UITests.Framework.WebAppAPI.Classes;

namespace Phoenix.UITests.Framework.WebAppAPI.Proxies
{
    public class ScheduleJobProxy : IScheduleJob
    {
        public ScheduleJobProxy()
        {
            _scheduleJobClass = new ScheduleJob();
        }

        private IScheduleJob _scheduleJobClass;


        public void Execute(string ScheduleJobID, string AuthenticationCookie)
        {
            _scheduleJobClass.Execute(ScheduleJobID, AuthenticationCookie);
        }
    }
}

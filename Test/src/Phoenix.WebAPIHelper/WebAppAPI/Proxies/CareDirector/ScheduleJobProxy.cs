using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.WebAPIHelper.WebAppAPI.Interfaces;
using Phoenix.WebAPIHelper.WebAppAPI.Classes;

namespace Phoenix.WebAPIHelper.WebAppAPI.Proxies
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

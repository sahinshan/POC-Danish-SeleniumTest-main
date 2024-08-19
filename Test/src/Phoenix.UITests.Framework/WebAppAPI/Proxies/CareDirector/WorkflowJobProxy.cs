using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.UITests.Framework.WebAppAPI.Interfaces;
using Phoenix.UITests.Framework.WebAppAPI.Classes;

namespace Phoenix.UITests.Framework.WebAppAPI.Proxies
{
    public class WorkflowJobProxy : IWorkflowJob
    {
        public WorkflowJobProxy()
        {
            _WorkflowJobClass = new WorkflowJob();
        }

        private IWorkflowJob _WorkflowJobClass;


        public void Execute(string WorkflowJobID, string AuthenticationCookie)
        {
            _WorkflowJobClass.Execute(WorkflowJobID, AuthenticationCookie);
        }
    }
}

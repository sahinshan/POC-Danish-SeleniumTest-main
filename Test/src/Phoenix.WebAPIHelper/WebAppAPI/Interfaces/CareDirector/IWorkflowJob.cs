using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.WebAPIHelper.WebAppAPI.Interfaces
{
    interface IWorkflowJob
    {
        void Execute(string WorkflowJobID, string AuthenticationCookie);
    }
}

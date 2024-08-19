using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.WebAppAPI.Interfaces
{
    public interface IZephyr
    {
        string UpdateTestExecutionStatus(string issueID, string projectID, string versionID);
        string GetTestCycleID(string IssueID, string projectID);
    }
}

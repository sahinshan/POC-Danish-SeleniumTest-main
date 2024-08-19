using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlassianServiceAPI.Models
{
    /// <summary>
    /// The test outcome for Jira
    /// </summary>
    public enum JiraTestOutcome
    {
        Inconclusive = -1,
        Passed = 1,
        Failed = 2,
        InProgress = 3
    }
}

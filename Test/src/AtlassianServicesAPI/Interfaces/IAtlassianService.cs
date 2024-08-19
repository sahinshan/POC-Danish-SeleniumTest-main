using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlassianServicesAPI.Interfaces
{
    public interface IAtlassianService
    {
        long ProjectId { get; }

        void AddStepsToTestCase(string issuekey, IList<AtlassianServiceAPI.Models.Step> steps, bool overwritten = false);
        void AddTestCaseToCycle(string issueKey, string cycleName, string versionName, long projectId);
        void AddTestCaseToCycle(AtlassianServiceAPI.Models.Issue issue, AtlassianServiceAPI.Models.Cycle cycle);
        void CloneTestCycle(string cycleName, string fromVersionName, string toVersion);
        AtlassianServiceAPI.Models.Issue CreateTestCase(AtlassianServiceAPI.Models.IssueCreation issueCreation);
        AtlassianServiceAPI.Models.Issue CreateTestCase(string projectKey, AtlassianServicesAPI.Models.IssueInCycle issueInCycle, string reporter = null);
        AtlassianServiceAPI.Models.Version CreateVersion(string versionName, string projectKey);
        AtlassianServiceAPI.Models.Version GetReleaseVersion(string versionName);
        AtlassianServiceAPI.Models.Cycle GetTestCycle(long versionId, long projectId, string cycleName);
        AtlassianServiceAPI.Models.Issue UpdateTestCase(string issueKey, AtlassianServiceAPI.Models.IssueUpdate issueUpdate);
        void UpdateTestCaseInVersion(string testCaseKey, string versionName, AtlassianServiceAPI.Models.JiraTestOutcome status);
        void UpdateTestStatus(string testCaseKey, string versionName, AtlassianServiceAPI.Models.JiraTestOutcome outcome);
    }
}

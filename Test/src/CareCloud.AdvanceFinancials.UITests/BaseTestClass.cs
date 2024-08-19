using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using Phoenix.DBHelper;
using Phoenix.UITests.Framework.CommonMethods;
using Phoenix.UITests.Framework.WebAppAPI;


namespace CareCloud.AdvanceFinancials.UITests
{
    public abstract class BaseTestClass
    {
        #region Internal Properties
        
        internal AtlassianServiceAPI.Models.Zapi zapi;
        internal AtlassianServiceAPI.Models.JiraApi jiraAPI;
        internal AtlassianServicesAPI.AtlassianService atlassianService;
        internal string versionName;

        #endregion

        #region Private Properties

        private TestContext testContextInstance;

        #endregion
        
        #region Public Properties

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        public DatabaseHelper dbHelper;
        public CommonMethods commonMethodsHelper;
        public CommonMethodsDB commonMethodsDB;
        public WebAPIHelper webAPIHelper;

        #endregion

        #region Public Methods

        [TestInitialize()]
        public void TestInitializationMethod()
        {
            dbHelper = new DatabaseHelper();
            commonMethodsHelper = new CommonMethods();
            commonMethodsDB = new CommonMethodsDB(dbHelper, TestContext);
            webAPIHelper = new WebAPIHelper();
        }

        [TestCleanup()]
        public void TestnCleanupMethod()
        {
            bool testPassed = this.TestContext.CurrentTestOutcome == UnitTestOutcome.Passed;

            SendTestResultsToZephyr(testPassed);
        }

        #endregion

        #region Private Methods

        private void SendTestResultsToZephyr(bool testPassed)
        {
            var updateTestResult = ConfigurationManager.AppSettings["UpdateStatusInZephyr"];

            if (updateTestResult.Equals("true"))
            {
                string jiraIssueID = (string)this.TestContext.Properties["JiraIssueID"];

                //if we have a jira id for the test then we will update its status in jira
                try
                {
                    if (jiraIssueID != null)
                    {
                        SetCorrectConfigurationBaseOnJiraTest(jiraIssueID);

                        if (testPassed)
                            atlassianService.UpdateTestStatus(jiraIssueID, versionName, "Automation_Regression_Testing_UI", AtlassianServiceAPI.Models.JiraTestOutcome.Passed);
                        else
                            atlassianService.UpdateTestStatus(jiraIssueID, versionName, "Automation_Regression_Testing_UI", "....", AtlassianServiceAPI.Models.JiraTestOutcome.Failed);
                    }
                }
                catch { }
            }
        }

        private void SetCorrectConfigurationBaseOnJiraTest(string JiraIssueID)
        {
            zapi = new AtlassianServiceAPI.Models.Zapi()
            {
                AccessKey = ConfigurationManager.AppSettings["AccessKey"],
                SecretKey = ConfigurationManager.AppSettings["SecretKey"],
                User = ConfigurationManager.AppSettings["User"],
            };

            if (JiraIssueID.StartsWith("ACC"))
            {
                jiraAPI = new AtlassianServiceAPI.Models.JiraApi()
                {
                    Authentication = ConfigurationManager.AppSettings["Authentication"],
                    JiraCloudUrl = ConfigurationManager.AppSettings["JiraCloudUrl"],
                    ProjectKey = ConfigurationManager.AppSettings["ACCProjectKey"]
                };

                versionName = ConfigurationManager.AppSettings["ACCCurrentVersionName"];
            }
            else if (JiraIssueID.StartsWith("CDV6"))
            {
                jiraAPI = new AtlassianServiceAPI.Models.JiraApi()
                {
                    Authentication = ConfigurationManager.AppSettings["Authentication"],
                    JiraCloudUrl = ConfigurationManager.AppSettings["JiraCloudUrl"],
                    ProjectKey = ConfigurationManager.AppSettings["CDV6ProjectKey"]
                };

                versionName = ConfigurationManager.AppSettings["CDV6CurrentVersionName"];
            }

            atlassianService = new AtlassianServicesAPI.AtlassianService(zapi, jiraAPI);

        }

        #endregion

    }
}

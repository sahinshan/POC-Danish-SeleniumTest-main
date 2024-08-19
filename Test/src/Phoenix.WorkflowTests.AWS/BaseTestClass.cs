using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.WorkflowTests.AWS
{
    public abstract class BaseTestClass
    {
        #region Private variable

        private TestContext testContextInstance;

        #endregion

        #region Public Property

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
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

        public string DownloadsDirectory { get; set; }

        public DBHelper.DatabaseHelper dbHelper { get; set; }

        public Phoenix.WebAPIHelper.WebAppAPI.WebAPIHelper webApiHelper { get; set; }

        public Phoenix.WorkflowTestFramework.Helpers.FileIOHelper fileIOHelper { get; set; }

        public Phoenix.WorkflowTests.AWS.CommonWorkflowMethods commonWorkflowMethods { get; set; }

        #endregion

        [TestInitialize()]
        public void SetupTest()
        {
            DownloadsDirectory = TestContext.TestRunDirectory + "\\Downloads";

            dbHelper = new Phoenix.DBHelper.DatabaseHelper();

            webApiHelper = new WebAPIHelper.WebAppAPI.WebAPIHelper();

            fileIOHelper = new WorkflowTestFramework.Helpers.FileIOHelper();

            commonWorkflowMethods = new CommonWorkflowMethods(dbHelper, fileIOHelper, TestContext);
        }

        [TestCleanup()]
        public virtual void MyTestCleanup()
        {
            string jiraIssueID = (string)this.TestContext.Properties["JiraIssueID"];

            //if we have a jira id for the test then we will update its status in jira
            if (jiraIssueID != null)
            {
                bool testPassed = this.TestContext.CurrentTestOutcome == UnitTestOutcome.Passed;

                var zapi = new AtlassianServiceAPI.Models.Zapi()
                {
                    AccessKey = ConfigurationManager.AppSettings["AccessKey"],
                    SecretKey = ConfigurationManager.AppSettings["SecretKey"],
                    User = ConfigurationManager.AppSettings["User"],
                };

                var jiraAPI = new AtlassianServiceAPI.Models.JiraApi()
                {
                    Authentication = ConfigurationManager.AppSettings["Authentication"],
                    JiraCloudUrl = ConfigurationManager.AppSettings["JiraCloudUrl"],
                    ProjectKey = ConfigurationManager.AppSettings["ProjectKey"]
                };

                AtlassianServicesAPI.AtlassianService atlassianService = new AtlassianServicesAPI.AtlassianService(zapi, jiraAPI);

                string versionName = ConfigurationManager.AppSettings["CurrentVersionName"];

                if (testPassed)
                    atlassianService.UpdateTestStatus(jiraIssueID, versionName, "Automated Testing Workflows", AtlassianServiceAPI.Models.JiraTestOutcome.Passed);
                else
                    atlassianService.UpdateTestStatus(jiraIssueID, versionName, "Automated Testing Workflows", AtlassianServiceAPI.Models.JiraTestOutcome.Failed);

            }
        }

        public void GetAllTestNamesAndDescriptions()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("TestName,Description,JiraIssueID");

            Type t = this.GetType();

            foreach (var method in t.GetMethods())
            {
                TestMethodAttribute testMethod = null;
                DescriptionAttribute descAttr = null;
                TestPropertyAttribute propertyAttr = null;

                foreach (var attribute in method.GetCustomAttributes(false))
                {
                    if (attribute is TestMethodAttribute)
                        testMethod = attribute as TestMethodAttribute;

                    if (attribute is DescriptionAttribute)
                        descAttr = attribute as DescriptionAttribute;

                    if (attribute is TestPropertyAttribute && (attribute as TestPropertyAttribute).Name == "JiraIssueID")
                        propertyAttr = attribute as TestPropertyAttribute;
                }

                if (testMethod != null && propertyAttr != null && !string.IsNullOrEmpty((propertyAttr as TestPropertyAttribute).Value))
                {
                    //sb.AppendLine(method.Name + "," + descAttr.Description.Replace(",", ";") + "," + (propertyAttr as TestPropertyAttribute).Value);
                    sb.AppendLine((propertyAttr as TestPropertyAttribute).Value);
                    continue;
                }
                //if (testMethod != null)
                //{
                //    sb.AppendLine(method.Name + "," + descAttr.Description.Replace(",", ";"));
                //    continue;
                //}
                if (testMethod != null)
                {
                    sb.AppendLine(method.Name + "," + t.FullName + "." + method.Name);
                    continue;
                }

            }

            Console.WriteLine(sb.ToString());
        }


    }
}

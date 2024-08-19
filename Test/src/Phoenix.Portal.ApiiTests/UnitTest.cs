﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Portal.ApiTests
{
    public abstract class UnitTest
    {
        public Phoenix.UITests.Framework.WebAppAPI.WebAPIHelper WebAPIHelper;
        public DBHelper.DatabaseHelper dbHelper;


        private TestContext testContextInstance;

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

        [TestInitialize()]
        public void SetupTest()
        {
            WebAPIHelper = new UITests.Framework.WebAppAPI.WebAPIHelper();
            dbHelper = new DBHelper.DatabaseHelper();

            //Authenticate against the portal API and extract the Security Token
            this.WebAPIHelper.PortalSecurityProxy.GetToken();
        }

        [TestCleanup()]
        public virtual void MyTestCleanup()
        {
            string jiraIssueID = (string)this.TestContext.Properties["JiraIssueID"];

            //if we have a jira id for the test then we will update its status in jira
            if (!string.IsNullOrEmpty(jiraIssueID))
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
                    atlassianService.UpdateTestStatus(jiraIssueID, versionName, AtlassianServiceAPI.Models.JiraTestOutcome.Passed);
                else
                    atlassianService.UpdateTestStatus(jiraIssueID, versionName, AtlassianServiceAPI.Models.JiraTestOutcome.Failed);


            }

        }



        public void GetAllTestNamesAndDescriptions()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("TestName,Description");

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

                if (testMethod != null && propertyAttr != null && !string.IsNullOrEmpty(propertyAttr.Value))
                {
                    sb.AppendLine(propertyAttr.Value);
                    continue;
                }
                if (testMethod != null)
                {
                    sb.AppendLine(method.Name + "," + descAttr.Description.Replace(",", ";"));
                    continue;
                }

            }

            Console.WriteLine(sb.ToString());
        }
    }
}

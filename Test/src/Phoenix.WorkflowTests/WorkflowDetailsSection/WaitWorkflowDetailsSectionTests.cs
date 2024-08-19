//using System;
//using System.Linq;
//using System.Collections.Generic;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Text;
//using Phoenix.WorkflowTestFramework;
//using System.Configuration;

//namespace Phoenix.WorkflowTests.WorkflowDetailsSection
//{
//    [TestClass]
//    public class WaitWorkflowDetailsSectionTests
//    {
//        public Phoenix.WorkflowTestFramework.PhoenixPlatformServiceHelper phoenixPlatformServiceHelper { get; set; }

//        readonly Guid WFCDV6_9078PublishedExecuteBefore = new Guid("1acff0a7-faa8-eb11-a323-005056926fe4"); //WF CDV6-9078 (published - Execute Before)
//        readonly Guid WFCDV6_9078PublishedExecuteAfter = new Guid("69691345-fba8-eb11-a323-005056926fe4"); //WF CDV6-9078 (published - Execute Before)

//        [TestInitialize]
//        public void TestInitializationMethod()
//        {
//            phoenixPlatformServiceHelper = new WorkflowTestFramework.PhoenixPlatformServiceHelper();
//            var authRequest = phoenixPlatformServiceHelper.GetAuthenticationRequest("Workflow_Test_User_0", "Passw0rd_!");
//            var authResponse = phoenixPlatformServiceHelper.AuthenticateUser(authRequest);
//            phoenixPlatformServiceHelper.SetServiceConnectionDataFromAuthenticationResponse(authResponse);
//        }



//        [TestProperty("JiraIssueID", "")]
//        [TestMethod]
//        [Description("Create a new phone call record (phone call date set to 5 days in the future) - " +
//            "validate that a workflow job for the workflow 'WF CDV6-9078 (published - Execute Before)' is created - " +
//            "Validate that the workflow trigger date is set to 2 days before the phone call date.")]
//        public void WaitWorkflow_TestMethod001()
//        {
//            //ARRANGE 
//            string subject = "WF Testing - CDV6-9078 - Execute Before - Scenario 1";
//            string description = "Sample Description ...";
//            Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//            string callerIdTableName = "person";
//            string callerIDName = "Adolfo Abbott";

//            Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//            string recipientIdTableName = "systemuser";
//            string recipientIDName = "José Brazeta";

//            string phoneNumber = "0987654321234";

//            Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//            string regardingName = "Adolfo Abbott";

//            Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//            DateTime phonecallDate = DateTime.Now.AddDays(5).Date;


//            //get all phone call records for the person
//            List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//            //Delete the records
//            foreach (Guid phonecall in phoneCallIDs)
//                phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);

//            //Delete all Workflow jobs
//            foreach (var wfjobid in phoenixPlatformServiceHelper.GetWorkflowJobsForWorkflow(WFCDV6_9078PublishedExecuteBefore))
//                phoenixPlatformServiceHelper.DeleteWorkflowJob(wfjobid);


//            //ACT
//            Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, 
//                recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, phonecallDate);

//            //ASSERT
//            var workflowJobIds = phoenixPlatformServiceHelper.GetWorkflowJobsForWorkflow(WFCDV6_9078PublishedExecuteBefore);
//            Assert.AreEqual(1, workflowJobIds.Count);

//            var expectedTriggerDate = phonecallDate.AddDays(-2);
//            var workflowJobFields = phoenixPlatformServiceHelper.GetWorkflowJobById(workflowJobIds[0], "triggerdate");
//            Assert.AreEqual(expectedTriggerDate, ((DateTime)workflowJobFields["triggerdate"]).ToLocalTime());
//        }

//        [TestProperty("JiraIssueID", "")]
//        [TestMethod]
//        [Description("Create a new phone call record (phone call date set to 5 days in the future) - " +
//            "validate that a workflow job for the workflow 'WF CDV6-9078 (published - Execute Before)' is created - " +
//            "Wait for 1 minute and 30 seconds (so that the workflow job service can run) - " +
//            "Validate that the workflow job is not executed (trigger date not reached)")]
//        public void WaitWorkflow_TestMethod002()
//        {
//            //ARRANGE 
//            string subject = "WF Testing - CDV6-9078 - Execute Before - Scenario 1";
//            string description = "Sample Description ...";
//            Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//            string callerIdTableName = "person";
//            string callerIDName = "Adolfo Abbott";

//            Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//            string recipientIdTableName = "systemuser";
//            string recipientIDName = "José Brazeta";

//            string phoneNumber = "0987654321234";

//            Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//            string regardingName = "Adolfo Abbott";

//            Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//            DateTime phonecallDate = DateTime.Now.AddDays(5).Date;


//            //get all phone call records for the person
//            List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//            //Delete the records
//            foreach (Guid phonecall in phoneCallIDs)
//                phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);

//            //Delete all Workflow jobs
//            foreach (var wfjobid in phoenixPlatformServiceHelper.GetWorkflowJobsForWorkflow(WFCDV6_9078PublishedExecuteBefore))
//                phoenixPlatformServiceHelper.DeleteWorkflowJob(wfjobid);


//            //ACT
//            Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName,
//                recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, phonecallDate);


//            //Wait for 1 minute and 30 seconds so that the workflow job service can run
//            System.Threading.Thread.Sleep(new TimeSpan(0, 1, 30));



//            //ASSERT
//            var workflowJobIds = phoenixPlatformServiceHelper.GetWorkflowJobsForWorkflow(WFCDV6_9078PublishedExecuteBefore);
//            Assert.AreEqual(1, workflowJobIds.Count);

//            var expectedTriggerDate = phonecallDate.AddDays(-2);
//            var workflowJobFields = phoenixPlatformServiceHelper.GetWorkflowJobById(workflowJobIds[0], "triggerdate");
//            Assert.AreEqual(expectedTriggerDate, ((DateTime)workflowJobFields["triggerdate"]).ToLocalTime());

//            //If the job was executed then the phone call subject should have been changed
//            var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "subject");
//            Assert.AreEqual("WF Testing - CDV6-9078 - Execute Before - Scenario 1", fields["subject"]);
//        }

//        [TestProperty("JiraIssueID", "")]
//        [TestMethod]
//        [Description("Create a new phone call record (phone call date set to 2 days in the future) - " +
//            "validate that a workflow job for the workflow 'WF CDV6-9078 (published - Execute Before)' is created - " +
//            "Wait for 1 minute and 30 seconds (so that the workflow job service can run) - " +
//            "Validate that the workflow job is executed (trigger date should be set to today)")]
//        public void WaitWorkflow_TestMethod003()
//        {
//            //ARRANGE 
//            string subject = "WF Testing - CDV6-9078 - Execute Before - Scenario 1";
//            string description = "Sample Description ...";
//            Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//            string callerIdTableName = "person";
//            string callerIDName = "Adolfo Abbott";

//            Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//            string recipientIdTableName = "systemuser";
//            string recipientIDName = "José Brazeta";

//            string phoneNumber = "0987654321234";

//            Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//            string regardingName = "Adolfo Abbott";

//            Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//            DateTime phonecallDate = DateTime.Now.AddDays(2).Date;


//            //get all phone call records for the person
//            List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//            //Delete the records
//            foreach (Guid phonecall in phoneCallIDs)
//                phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);

//            //Delete all Workflow jobs
//            foreach (var wfjobid in phoenixPlatformServiceHelper.GetWorkflowJobsForWorkflow(WFCDV6_9078PublishedExecuteBefore))
//                phoenixPlatformServiceHelper.DeleteWorkflowJob(wfjobid);


//            //ACT
//            Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName,
//                recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, phonecallDate);


//            //Wait for 1 minute and 30 seconds so that the workflow job service can run
//            System.Threading.Thread.Sleep(new TimeSpan(0, 1, 30));



//            //ASSERT
//            var workflowJobIds = phoenixPlatformServiceHelper.GetWorkflowJobsForWorkflow(WFCDV6_9078PublishedExecuteBefore);
//            Assert.AreEqual(1, workflowJobIds.Count);

//            var expectedTriggerDate = phonecallDate.AddDays(-2);
//            var workflowJobFields = phoenixPlatformServiceHelper.GetWorkflowJobById(workflowJobIds[0], "triggerdate");
//            Assert.AreEqual(expectedTriggerDate, ((DateTime)workflowJobFields["triggerdate"]).ToLocalTime());

//            //If the job was executed then the phone call subject should have been changed
//            var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "subject");
//            Assert.AreEqual("WF Testing - CDV6-9078 - Execute Before - Scenario 1 - Triggered", fields["subject"]);
//        }

//        [TestProperty("JiraIssueID", "")]
//        [TestMethod]
//        [Description("Create a new phone call record (phone call date set to 1 days in the future) - " +
//            "validate that a workflow job for the workflow 'WF CDV6-9078 (published - Execute Before)' is created - " +
//            "Wait for 1 minute and 30 seconds (so that the workflow job service can run) - " +
//            "Validate that the workflow job is executed (trigger date should be set to yesterday)")]
//        public void WaitWorkflow_TestMethod004()
//        {
//            //ARRANGE 
//            string subject = "WF Testing - CDV6-9078 - Execute Before - Scenario 1";
//            string description = "Sample Description ...";
//            Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//            string callerIdTableName = "person";
//            string callerIDName = "Adolfo Abbott";

//            Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//            string recipientIdTableName = "systemuser";
//            string recipientIDName = "José Brazeta";

//            string phoneNumber = "0987654321234";

//            Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//            string regardingName = "Adolfo Abbott";

//            Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//            DateTime phonecallDate = DateTime.Now.AddDays(1).Date;


//            //get all phone call records for the person
//            List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//            //Delete the records
//            foreach (Guid phonecall in phoneCallIDs)
//                phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);

//            //Delete all Workflow jobs
//            foreach (var wfjobid in phoenixPlatformServiceHelper.GetWorkflowJobsForWorkflow(WFCDV6_9078PublishedExecuteBefore))
//                phoenixPlatformServiceHelper.DeleteWorkflowJob(wfjobid);


//            //ACT
//            Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName,
//                recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, phonecallDate);


//            //Wait for 1 minute and 30 seconds so that the workflow job service can run
//            System.Threading.Thread.Sleep(new TimeSpan(0, 1, 30));



//            //ASSERT
//            var workflowJobIds = phoenixPlatformServiceHelper.GetWorkflowJobsForWorkflow(WFCDV6_9078PublishedExecuteBefore);
//            Assert.AreEqual(1, workflowJobIds.Count);

//            var expectedTriggerDate = phonecallDate.AddDays(-2);
//            var workflowJobFields = phoenixPlatformServiceHelper.GetWorkflowJobById(workflowJobIds[0], "triggerdate");
//            Assert.AreEqual(expectedTriggerDate, ((DateTime)workflowJobFields["triggerdate"]).ToLocalTime());

//            //If the job was executed then the phone call subject should have been changed
//            var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "subject");
//            Assert.AreEqual("WF Testing - CDV6-9078 - Execute Before - Scenario 1 - Triggered", fields["subject"]);
//        }




//        [TestProperty("JiraIssueID", "")]
//        [TestMethod]
//        [Description("Create a new phone call record (phone call date set to 3 days in the past) - " +
//            "validate that a workflow job for the workflow 'WF CDV6-9078 (published - Execute After)' is created - " +
//            "Wait for 1 minute and 30 seconds (so that the workflow job service can run) - " +
//            "Validate that the workflow job is executed (trigger date is 1 days in the past)")]
//        public void WaitWorkflow_TestMethod005()
//        {
//            //ARRANGE 
//            string subject = "WF Testing - CDV6-9078 - Execute After - Scenario 1";
//            string description = "Sample Description ...";
//            Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//            string callerIdTableName = "person";
//            string callerIDName = "Adolfo Abbott";

//            Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//            string recipientIdTableName = "systemuser";
//            string recipientIDName = "José Brazeta";

//            string phoneNumber = "0987654321234";

//            Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//            string regardingName = "Adolfo Abbott";

//            Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//            DateTime phonecallDate = DateTime.Now.AddDays(-3).Date;


//            //get all phone call records for the person
//            List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//            //Delete the records
//            foreach (Guid phonecall in phoneCallIDs)
//                phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);

//            //Delete all Workflow jobs
//            foreach (var wfjobid in phoenixPlatformServiceHelper.GetWorkflowJobsForWorkflow(WFCDV6_9078PublishedExecuteAfter))
//                phoenixPlatformServiceHelper.DeleteWorkflowJob(wfjobid);


//            //ACT
//            Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName,
//                recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, phonecallDate);


//            //Wait for 1 minute and 30 seconds so that the workflow job service can run
//            System.Threading.Thread.Sleep(new TimeSpan(0, 1, 30));



//            //ASSERT
//            var workflowJobIds = phoenixPlatformServiceHelper.GetWorkflowJobsForWorkflow(WFCDV6_9078PublishedExecuteAfter);
//            Assert.AreEqual(1, workflowJobIds.Count);

//            var expectedTriggerDate = phonecallDate.AddDays(2);
//            var workflowJobFields = phoenixPlatformServiceHelper.GetWorkflowJobById(workflowJobIds[0], "triggerdate");
//            Assert.AreEqual(expectedTriggerDate, ((DateTime)workflowJobFields["triggerdate"]).ToLocalTime());

//            //If the job was executed then the phone call subject should have been changed
//            var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "subject");
//            Assert.AreEqual("WF Testing - CDV6-9078 - Execute After - Scenario 1 - Triggered", fields["subject"]);
//        }

//        [TestProperty("JiraIssueID", "")]
//        [TestMethod]
//        [Description("Create a new phone call record (phone call date set to minus 2 days, plus 1 hour, plus 20 minutes in the past) - " +
//            "validate that a workflow job for the workflow 'WF CDV6-9078 (published - Execute After)' is created - " +
//            "Wait for 1 minute and 30 seconds (so that the workflow job service can run) - " +
//            "Validate that the workflow job is NOT executed (trigger date is set to today 1 hour and 20 minutes in the future)")]
//        public void WaitWorkflow_TestMethod006()
//        {
//            //ARRANGE 
//            string subject = "WF Testing - CDV6-9078 - Execute After - Scenario 1";
//            string description = "Sample Description ...";
//            Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//            string callerIdTableName = "person";
//            string callerIDName = "Adolfo Abbott";

//            Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//            string recipientIdTableName = "systemuser";
//            string recipientIDName = "José Brazeta";

//            string phoneNumber = "0987654321234";

//            Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//            string regardingName = "Adolfo Abbott";

//            Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//            DateTime phonecallDate = DateTime.Now.AddDays(-2).AddHours(1).AddMinutes(20);


//            //get all phone call records for the person
//            List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//            //Delete the records
//            foreach (Guid phonecall in phoneCallIDs)
//                phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);

//            //Delete all Workflow jobs
//            foreach (var wfjobid in phoenixPlatformServiceHelper.GetWorkflowJobsForWorkflow(WFCDV6_9078PublishedExecuteAfter))
//                phoenixPlatformServiceHelper.DeleteWorkflowJob(wfjobid);


//            //ACT
//            Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName,
//                recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, phonecallDate);


//            //Wait for 1 minute and 30 seconds so that the workflow job service can run
//            System.Threading.Thread.Sleep(new TimeSpan(0, 1, 30));



//            //ASSERT
//            var workflowJobIds = phoenixPlatformServiceHelper.GetWorkflowJobsForWorkflow(WFCDV6_9078PublishedExecuteAfter);
//            Assert.AreEqual(1, workflowJobIds.Count);

//            var expectedTriggerDate = phonecallDate.AddDays(2);
//            var workflowJobFields = phoenixPlatformServiceHelper.GetWorkflowJobById(workflowJobIds[0], "triggerdate");
            
//            Assert.AreEqual(expectedTriggerDate.WithoutMilliseconds(), ((DateTime)workflowJobFields["triggerdate"]).ToLocalTime().WithoutMilliseconds());

//            //If the job was executed then the phone call subject should have been changed
//            var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "subject");
//            Assert.AreEqual("WF Testing - CDV6-9078 - Execute After - Scenario 1", fields["subject"]);
//        }

//        [TestProperty("JiraIssueID", "")]
//        [TestMethod]
//        [Description("Create a new phone call record (phone call date set to 1 day in the past) - " +
//            "validate that a workflow job for the workflow 'WF CDV6-9078 (published - Execute After)' is created - " +
//            "Wait for 1 minute and 30 seconds (so that the workflow job service can run) - " +
//            "Validate that the workflow job is NOT executed (trigger date is set to tomorrow)")]
//        public void WaitWorkflow_TestMethod007()
//        {
//            //ARRANGE 
//            string subject = "WF Testing - CDV6-9078 - Execute After - Scenario 1";
//            string description = "Sample Description ...";
//            Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//            string callerIdTableName = "person";
//            string callerIDName = "Adolfo Abbott";

//            Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//            string recipientIdTableName = "systemuser";
//            string recipientIDName = "José Brazeta";

//            string phoneNumber = "0987654321234";

//            Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//            string regardingName = "Adolfo Abbott";

//            Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//            DateTime phonecallDate = DateTime.Now.AddDays(-1);


//            //get all phone call records for the person
//            List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//            //Delete the records
//            foreach (Guid phonecall in phoneCallIDs)
//                phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);

//            //Delete all Workflow jobs
//            foreach (var wfjobid in phoenixPlatformServiceHelper.GetWorkflowJobsForWorkflow(WFCDV6_9078PublishedExecuteAfter))
//                phoenixPlatformServiceHelper.DeleteWorkflowJob(wfjobid);


//            //ACT
//            Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName,
//                recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, phonecallDate);


//            //Wait for 1 minute and 30 seconds so that the workflow job service can run
//            System.Threading.Thread.Sleep(new TimeSpan(0, 1, 30));



//            //ASSERT
//            var workflowJobIds = phoenixPlatformServiceHelper.GetWorkflowJobsForWorkflow(WFCDV6_9078PublishedExecuteAfter);
//            Assert.AreEqual(1, workflowJobIds.Count);

//            var expectedTriggerDate = phonecallDate.AddDays(2);
//            var workflowJobFields = phoenixPlatformServiceHelper.GetWorkflowJobById(workflowJobIds[0], "triggerdate");

//            Assert.AreEqual(expectedTriggerDate.WithoutMilliseconds(), ((DateTime)workflowJobFields["triggerdate"]).ToLocalTime().WithoutMilliseconds());

//            //If the job was executed then the phone call subject should have been changed
//            var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "subject");
//            Assert.AreEqual("WF Testing - CDV6-9078 - Execute After - Scenario 1", fields["subject"]);
//        }





//        [Description("Method will return the name of all tests and the Description of each one")]
//        [TestMethod]
//        public void GetTestNames()
//        {
//            StringBuilder sb = new StringBuilder();
//            sb.AppendLine("TestName,Description,JiraID");

//            Type t = this.GetType();

//            foreach (var method in t.GetMethods())
//            {
//                TestMethodAttribute testMethod = null;
//                DescriptionAttribute descAttr = null;
//                TestPropertyAttribute propertyAttr = null;

//                foreach (var attribute in method.GetCustomAttributes(false))
//                {
//                    if (attribute is TestMethodAttribute)
//                        testMethod = attribute as TestMethodAttribute;

//                    if (attribute is DescriptionAttribute)
//                        descAttr = attribute as DescriptionAttribute;

//                    if (attribute is TestPropertyAttribute && (attribute as TestPropertyAttribute).Name == "JiraIssueID")
//                        propertyAttr = attribute as TestPropertyAttribute;
//                }

//                if (testMethod != null && propertyAttr != null)
//                {
//                    sb.AppendLine(propertyAttr.Value);
//                    //sb.AppendLine(method.Name + "," + descAttr.Description + "," + propertyAttr.Value);
//                    continue;
//                }
//                if (testMethod != null)
//                {
//                    sb.AppendLine(method.Name + "," + descAttr.Description);
//                    continue;
//                }

//            }

//            Console.WriteLine(sb.ToString());
//        }



//        private TestContext testContextInstance;

//        public TestContext TestContext
//        {
//            get
//            {
//                return testContextInstance;
//            }
//            set
//            {
//                testContextInstance = value;
//            }
//        }

//        [TestCleanup()]
//        public virtual void MyTestCleanup()
//        {
//            string jiraIssueID = (string)this.TestContext.Properties["JiraIssueID"];

//            //if we have a jira id for the test then we will update its status in jira
//            if (jiraIssueID != null)
//            {
//                bool testPassed = this.TestContext.CurrentTestOutcome == UnitTestOutcome.Passed;

//                var zapi = new AtlassianServiceAPI.Models.Zapi()
//                {
//                    AccessKey = ConfigurationManager.AppSettings["AccessKey"],
//                    SecretKey = ConfigurationManager.AppSettings["SecretKey"],
//                    User = ConfigurationManager.AppSettings["User"],
//                };

//                var jiraAPI = new AtlassianServiceAPI.Models.JiraApi()
//                {
//                    Authentication = ConfigurationManager.AppSettings["Authentication"],
//                    JiraCloudUrl = ConfigurationManager.AppSettings["JiraCloudUrl"],
//                    ProjectKey = ConfigurationManager.AppSettings["ProjectKey"]
//                };

//                AtlassianServicesAPI.AtlassianService atlassianService = new AtlassianServicesAPI.AtlassianService(zapi, jiraAPI);

//                string versionName = ConfigurationManager.AppSettings["CurrentVersionName"];

//                if (testPassed)
//                    atlassianService.UpdateTestStatus(jiraIssueID, versionName, "Automated Testing Workflows", AtlassianServiceAPI.Models.JiraTestOutcome.Passed);
//                else
//                    atlassianService.UpdateTestStatus(jiraIssueID, versionName, "Automated Testing Workflows", AtlassianServiceAPI.Models.JiraTestOutcome.Failed);


//            }
//        }
//    }
//}

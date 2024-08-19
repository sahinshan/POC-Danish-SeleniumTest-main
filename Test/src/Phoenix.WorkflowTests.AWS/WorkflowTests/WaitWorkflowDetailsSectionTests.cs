using System;
using System.Configuration;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phoenix.WorkflowTestFramework;

namespace Phoenix.WorkflowTests.AWS
{
    [TestClass]
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("Files\\WF CDV6-9078 (published - Execute Before).Zip")]
    [DeploymentItem("Files\\WF CDV6-9078 (publised - Execute After).Zip")]
    public class WaitWorkflowDetailsSectionTests : BaseTestClass
    {
        private Guid WFCDV6_9078PublishedExecuteBefore;  //WF CDV6-9078 (published - Execute Before)
        private Guid WFCDV6_9078PublishedExecuteAfter;  //WF CDV6-9078 (publised - Execute After)

        //[TestInitialize]
        //public void TestInitializationMethod()
        //{
        //    phoenixPlatformServiceHelper = new WorkflowTestFramework.PhoenixPlatformServiceHelper();
        //    var authRequest = phoenixPlatformServiceHelper.GetAuthenticationRequest("Workflow_Test_User_0", "Passw0rd_!");
        //    var authResponse = phoenixPlatformServiceHelper.AuthenticateUser(authRequest);
        //    phoenixPlatformServiceHelper.SetServiceConnectionDataFromAuthenticationResponse(authResponse);
        //}

        #region Private Properties

        private string tenantName;
        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _defaultBusinessUnitId;
        private Guid _defaultTeamId;
        private Guid _ethnicityId;

        #endregion

        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

        [TestInitialize()]
        public void TestClassInitializationMethod()
        {
            #region Default user

            tenantName = ConfigurationManager.AppSettings["TenantName"];
            string username = ConfigurationManager.AppSettings["Username"];
            string DataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

            if (DataEncoded.Equals("true"))
            {
                var base64EncodedBytes = System.Convert.FromBase64String(username);
                username = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }

            var userId = dbHelper.systemUser.GetSystemUserByUserName(username).FirstOrDefault();
            dbHelper.systemUser.UpdateLastPasswordChangedDate(userId, DateTime.Now.Date);

            #endregion

            #region Authentication Provider

            _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").First();

            #endregion

            #region Product Language

            if (!dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any())
                dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
            _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

            #endregion

            #region Business Unit

            if (!dbHelper.businessUnit.GetByName("UK Local Authority Social Care").Any())
                dbHelper.businessUnit.CreateBusinessUnit("UK Local Authority Social Care");
            _defaultBusinessUnitId = dbHelper.businessUnit.GetByName("UK Local Authority Social Care")[0];

            #endregion

            #region Default Team

            if (!dbHelper.team.GetTeamIdByName("UK Local Authority Social Care").Any())
                dbHelper.team.CreateTeam("UK Local Authority Social Care", null, _defaultBusinessUnitId, "907678", "UKLocalAuthoritySocialCare@careworkstempmail.com", "UK Local Authority Social Care", "020 123456");
            _defaultTeamId = dbHelper.team.GetTeamIdByName("UK Local Authority Social Care")[0];

            #endregion

            #region Ethnicity

            if (!dbHelper.ethnicity.GetEthnicityIdByName("Irish").Any())
                dbHelper.ethnicity.CreateEthnicity(_defaultTeamId, "Irish", new DateTime(2020, 1, 1));
            _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Irish")[0];

            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-22195")]
        [TestMethod]
        [Description("Create a new phone call record (phone call date set to 5 days in the future) - " +
            "validate that a workflow job for the workflow 'WF CDV6-9078 (published - Execute Before)' is created - " +
            "Validate that the workflow trigger date is set to 2 days before the phone call date.")]
        public void WaitWorkflow_TestMethod001()
        {
            WFCDV6_9078PublishedExecuteBefore = commonWorkflowMethods.CreateWorkflowIfNeeded("WF CDV6-9078 (published - Execute Before)", "WF CDV6-9078 (published - Execute Before).Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Testing - CDV6-9078 - Execute Before - Scenario 1";
            string description = "Sample Description ...";
            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;
            var baseDate = DateTime.Now.AddDays(5).Date;
            DateTime phonecallDate = new DateTime(baseDate.Year, baseDate.Month, baseDate.Day);

            //Delete all Workflow jobs
            foreach (var wfjobid in dbHelper.workflowJob.GetWorkflowJobByWorkflowId(WFCDV6_9078PublishedExecuteBefore))
                dbHelper.workflowJob.DeleteWorkflowJob(wfjobid);


            //ACT
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, 
                recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phonecallDate);

            //ASSERT
            var workflowJobIds = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(WFCDV6_9078PublishedExecuteBefore);
            Assert.AreEqual(1, workflowJobIds.Count);

            var expectedTriggerDate = phonecallDate.AddDays(-2);
            var workflowJobFields = dbHelper.workflowJob.GetWorkflowJobByID(workflowJobIds[0], "triggerdate");
            Assert.AreEqual(expectedTriggerDate, ((DateTime)workflowJobFields["triggerdate"]).ToLocalTime());
        }

        [TestProperty("JiraIssueID", "CDV6-22196")]
        [TestMethod]
        [Description("Create a new phone call record (phone call date set to 5 days in the future) - " +
            "validate that a workflow job for the workflow 'WF CDV6-9078 (published - Execute Before)' is created - " +
            "Wait for 1 minute and 30 seconds (so that the workflow job service can run) - " +
            "Validate that the workflow job is not executed (trigger date not reached)")]
        public void WaitWorkflow_TestMethod002()
        {
            WFCDV6_9078PublishedExecuteBefore = commonWorkflowMethods.CreateWorkflowIfNeeded("WF CDV6-9078 (published - Execute Before)", "WF CDV6-9078 (published - Execute Before).Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Testing - CDV6-9078 - Execute Before - Scenario 1";
            string description = "Sample Description ...";
            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;
            var baseDate = DateTime.Now.AddDays(5).Date;
            DateTime phonecallDate = new DateTime(baseDate.Year, baseDate.Month, baseDate.Day);

            //Delete all Workflow jobs
            foreach (var wfjobid in dbHelper.workflowJob.GetWorkflowJobByWorkflowId(WFCDV6_9078PublishedExecuteBefore))
                dbHelper.workflowJob.DeleteWorkflowJob(wfjobid);

            //ACT
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName,
                recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phonecallDate);


            //Wait for 1 minute and 30 seconds so that the workflow job service can run
            System.Threading.Thread.Sleep(new TimeSpan(0, 1, 30));



            //ASSERT
            var workflowJobIds = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(WFCDV6_9078PublishedExecuteBefore);
            Assert.AreEqual(1, workflowJobIds.Count);

            var expectedTriggerDate = phonecallDate.AddDays(-2);
            var workflowJobFields = dbHelper.workflowJob.GetWorkflowJobByID(workflowJobIds[0], "triggerdate");
            Assert.AreEqual(expectedTriggerDate, ((DateTime)workflowJobFields["triggerdate"]).ToLocalTime());

            //If the job was executed then the phone call subject should have been changed
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "subject");
            Assert.AreEqual("WF Testing - CDV6-9078 - Execute Before - Scenario 1", fields["subject"]);
        }

        [TestProperty("JiraIssueID", "CDV6-22197")]
        [TestMethod]
        [Description("Create a new phone call record (phone call date set to 2 days in the future) - " +
            "validate that a workflow job for the workflow 'WF CDV6-9078 (published - Execute Before)' is created - " +
            "Wait for 1 minute and 30 seconds (so that the workflow job service can run) - " +
            "Validate that the workflow job is executed (trigger date should be set to today)")]
        public void WaitWorkflow_TestMethod003()
        {
            WFCDV6_9078PublishedExecuteBefore = commonWorkflowMethods.CreateWorkflowIfNeeded("WF CDV6-9078 (published - Execute Before)", "WF CDV6-9078 (published - Execute Before).Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Testing - CDV6-9078 - Execute Before - Scenario 1";
            string description = "Sample Description ...";
            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;
            var baseDate = DateTime.Now.AddDays(2).Date;
            DateTime phonecallDate = new DateTime(baseDate.Year, baseDate.Month, baseDate.Day);

            //Delete all Workflow jobs
            foreach (var wfjobid in dbHelper.workflowJob.GetWorkflowJobByWorkflowId(WFCDV6_9078PublishedExecuteBefore))
                dbHelper.workflowJob.DeleteWorkflowJob(wfjobid);


            //ACT
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName,
                recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phonecallDate);


            //Wait for 1 minute and 30 seconds so that the workflow job service can run
            System.Threading.Thread.Sleep(new TimeSpan(0, 1, 30));



            //ASSERT
            var workflowJobIds = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(WFCDV6_9078PublishedExecuteBefore);
            Assert.AreEqual(1, workflowJobIds.Count);

            var expectedTriggerDate = phonecallDate.AddDays(-2);
            var workflowJobFields = dbHelper.workflowJob.GetWorkflowJobByID(workflowJobIds[0], "triggerdate");
            Assert.AreEqual(expectedTriggerDate, ((DateTime)workflowJobFields["triggerdate"]).ToLocalTime());

            //If the job was executed then the phone call subject should have been changed
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "subject");
            Assert.AreEqual("WF Testing - CDV6-9078 - Execute Before - Scenario 1 - Triggered", fields["subject"]);
        }

        [TestProperty("JiraIssueID", "CDV6-22198")]
        [TestMethod]
        [Description("Create a new phone call record (phone call date set to 1 days in the future) - " +
            "validate that a workflow job for the workflow 'WF CDV6-9078 (published - Execute Before)' is created - " +
            "Wait for 1 minute and 30 seconds (so that the workflow job service can run) - " +
            "Validate that the workflow job is executed (trigger date should be set to yesterday)")]
        public void WaitWorkflow_TestMethod004()
        {
            WFCDV6_9078PublishedExecuteBefore = commonWorkflowMethods.CreateWorkflowIfNeeded("WF CDV6-9078 (published - Execute Before)", "WF CDV6-9078 (published - Execute Before).Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Testing - CDV6-9078 - Execute Before - Scenario 1";
            string description = "Sample Description ...";
            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;
            var baseDate = DateTime.Now.AddDays(1).Date;
            DateTime phonecallDate = new DateTime(baseDate.Year, baseDate.Month, baseDate.Day);

            //Delete all Workflow jobs
            foreach (var wfjobid in dbHelper.workflowJob.GetWorkflowJobByWorkflowId(WFCDV6_9078PublishedExecuteBefore))
                dbHelper.workflowJob.DeleteWorkflowJob(wfjobid);


            //ACT
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName,
                recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phonecallDate);


            //Wait for 1 minute and 30 seconds so that the workflow job service can run
            System.Threading.Thread.Sleep(new TimeSpan(0, 1, 30));



            //ASSERT
            var workflowJobIds = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(WFCDV6_9078PublishedExecuteBefore);
            Assert.AreEqual(1, workflowJobIds.Count);

            var expectedTriggerDate = phonecallDate.AddDays(-2);
            var workflowJobFields = dbHelper.workflowJob.GetWorkflowJobByID(workflowJobIds[0], "triggerdate");
            Assert.AreEqual(expectedTriggerDate, ((DateTime)workflowJobFields["triggerdate"]).ToLocalTime());

            //If the job was executed then the phone call subject should have been changed
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "subject");
            Assert.AreEqual("WF Testing - CDV6-9078 - Execute Before - Scenario 1 - Triggered", fields["subject"]);
        }




        [TestProperty("JiraIssueID", "CDV6-22199")]
        [TestMethod]
        [Description("Create a new phone call record (phone call date set to 3 days in the past) - " +
            "validate that a workflow job for the workflow 'WF CDV6-9078 (published - Execute After)' is created - " +
            "Wait for 1 minute and 30 seconds (so that the workflow job service can run) - " +
            "Validate that the workflow job is executed (trigger date is 1 days in the past)")]
        public void WaitWorkflow_TestMethod005()
        {
            WFCDV6_9078PublishedExecuteAfter = commonWorkflowMethods.CreateWorkflowIfNeeded("WF CDV6-9078 (publised - Execute After)", "WF CDV6-9078 (publised - Execute After).Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Testing - CDV6-9078 - Execute After - Scenario 1";
            string description = "Sample Description ...";
            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;
            var baseDate = DateTime.Now.AddDays(-3).Date;
            DateTime phonecallDate = new DateTime(baseDate.Year, baseDate.Month, baseDate.Day);

            //Delete all Workflow jobs
            foreach (var wfjobid in dbHelper.workflowJob.GetWorkflowJobByWorkflowId(WFCDV6_9078PublishedExecuteAfter))
                dbHelper.workflowJob.DeleteWorkflowJob(wfjobid);


            //ACT
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName,
                recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phonecallDate);


            //Wait for 1 minute and 30 seconds so that the workflow job service can run
            System.Threading.Thread.Sleep(new TimeSpan(0, 1, 30));



            //ASSERT
            var workflowJobIds = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(WFCDV6_9078PublishedExecuteAfter);
            Assert.AreEqual(1, workflowJobIds.Count);

            var expectedTriggerDate = phonecallDate.AddDays(2);
            var workflowJobFields = dbHelper.workflowJob.GetWorkflowJobByID(workflowJobIds[0], "triggerdate");
            Assert.AreEqual(expectedTriggerDate, ((DateTime)workflowJobFields["triggerdate"]).ToLocalTime());

            //If the job was executed then the phone call subject should have been changed
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "subject");
            Assert.AreEqual("WF Testing - CDV6-9078 - Execute After - Scenario 1 - Triggered", fields["subject"]);
        }

        [TestProperty("JiraIssueID", "CDV6-22200")]
        [TestMethod]
        [Description("Create a new phone call record (phone call date set to minus 2 days, plus 1 hour, plus 20 minutes in the past) - " +
            "validate that a workflow job for the workflow 'WF CDV6-9078 (published - Execute After)' is created - " +
            "Wait for 1 minute and 30 seconds (so that the workflow job service can run) - " +
            "Validate that the workflow job is NOT executed (trigger date is set to today 1 hour and 20 minutes in the future)")]
        public void WaitWorkflow_TestMethod006()
        {
            WFCDV6_9078PublishedExecuteAfter = commonWorkflowMethods.CreateWorkflowIfNeeded("WF CDV6-9078 (publised - Execute After)", "WF CDV6-9078 (publised - Execute After).Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Testing - CDV6-9078 - Execute After - Scenario 1";
            string description = "Sample Description ...";
            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;

            var baseDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            DateTime phonecallDate = baseDate.AddDays(-2).AddHours(1).AddMinutes(20);

            //Delete all Workflow jobs
            foreach (var wfjobid in dbHelper.workflowJob.GetWorkflowJobByWorkflowId(WFCDV6_9078PublishedExecuteAfter))
                dbHelper.workflowJob.DeleteWorkflowJob(wfjobid);


            //ACT
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName,
                recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phonecallDate);


            //Wait for 1 minute and 30 seconds so that the workflow job service can run
            System.Threading.Thread.Sleep(new TimeSpan(0, 1, 30));



            //ASSERT
            var workflowJobIds = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(WFCDV6_9078PublishedExecuteAfter);
            Assert.AreEqual(1, workflowJobIds.Count);

            var expectedTriggerDate = phonecallDate.AddDays(2);
            var workflowJobFields = dbHelper.workflowJob.GetWorkflowJobByID(workflowJobIds[0], "triggerdate");
            
            Assert.AreEqual(expectedTriggerDate.WithoutMilliseconds(), ((DateTime)workflowJobFields["triggerdate"]).ToLocalTime().WithoutMilliseconds());

            //If the job was executed then the phone call subject should have been changed
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "subject");
            Assert.AreEqual("WF Testing - CDV6-9078 - Execute After - Scenario 1", fields["subject"]);
        }

        [TestProperty("JiraIssueID", "CDV6-22201")]
        [TestMethod]
        [Description("Create a new phone call record (phone call date set to 1 day in the past) - " +
            "validate that a workflow job for the workflow 'WF CDV6-9078 (published - Execute After)' is created - " +
            "Wait for 1 minute and 30 seconds (so that the workflow job service can run) - " +
            "Validate that the workflow job is NOT executed (trigger date is set to tomorrow)")]
        public void WaitWorkflow_TestMethod007()
        {
            WFCDV6_9078PublishedExecuteAfter = commonWorkflowMethods.CreateWorkflowIfNeeded("WF CDV6-9078 (publised - Execute After)", "WF CDV6-9078 (publised - Execute After).Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Testing - CDV6-9078 - Execute After - Scenario 1";
            string description = "Sample Description ...";
            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;

            DateTime phonecallDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            //Delete all Workflow jobs
            foreach (var wfjobid in dbHelper.workflowJob.GetWorkflowJobByWorkflowId(WFCDV6_9078PublishedExecuteAfter))
                dbHelper.workflowJob.DeleteWorkflowJob(wfjobid);


            //ACT
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName,
                recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phonecallDate);


            //Wait for 1 minute and 30 seconds so that the workflow job service can run
            System.Threading.Thread.Sleep(new TimeSpan(0, 1, 30));



            //ASSERT
            var workflowJobIds = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(WFCDV6_9078PublishedExecuteAfter);
            Assert.AreEqual(1, workflowJobIds.Count);

            var expectedTriggerDate = phonecallDate.AddDays(2);
            var workflowJobFields = dbHelper.workflowJob.GetWorkflowJobByID(workflowJobIds[0], "triggerdate");

            Assert.AreEqual(expectedTriggerDate.WithoutMilliseconds(), ((DateTime)workflowJobFields["triggerdate"]).ToLocalTime().WithoutMilliseconds());

            //If the job was executed then the phone call subject should have been changed
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "subject");
            Assert.AreEqual("WF Testing - CDV6-9078 - Execute After - Scenario 1", fields["subject"]);
        }


        

    }
}

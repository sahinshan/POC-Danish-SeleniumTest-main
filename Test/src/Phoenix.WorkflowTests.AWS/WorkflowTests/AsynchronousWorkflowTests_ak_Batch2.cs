using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phoenix.DBHelper;
using Phoenix.DBHelper.Models;
using Phoenix.WorkflowTestFramework;

namespace Phoenix.WorkflowTests.AWS
{
    [TestClass]
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("Files\\WF Async Automated Testing - Validate Actions for Async workflow.Zip")]
    [DeploymentItem("Files\\WF Async Automated Testing - Validate Actions for Async workflow 2.Zip")]
    [DeploymentItem("Files\\WF Async Workflow Testing - Validations of Conditions and Steps.Zip")]
    [DeploymentItem("Files\\WF Async Automated Testing - Testing the Operators in Conditions.Zip")]
    [DeploymentItem("Files\\WF Async Workflow Testing - Validations of complex scenarios.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - S31 - Child Process.Zip")]
    public class AsynchronousWorkflowTests_ak_Batch2 : BaseTestClass
    {

        //readonly Guid AsyncTestWorkflowConditionsAndStepsID = new Guid("71E2AF9B-8480-E911-A2C5-005056926FE4"); //WF Async Workflow Testing - Validations of Conditions and Steps //////// 46 - 78 - even nos
        //readonly Guid AsyncTestWorkflowOperatorsInConditionsID = new Guid("0E008BF1-4881-E911-A2C5-005056926FE4"); //WF Async Automated Testing - Testing the Operators in Conditions //////// 81, 84, 86, 88 - 139, 141, 143
        //readonly Guid AsyncTestWorkflowComplexScenariosID = new Guid("523ef4dd-f581-e911-a2c5-005056926fe4"); //WF Async Workflow Testing - Validations of complex scenarios //////// 183.1, 183.2, 185

        //readonly Guid AsyncTestWorkflowValidateActions1ID = new Guid("29fd047f-2982-e911-a2c5-005056926fe4"); //WF Async Automated Testing - Validate Actions for Async workflow ///////////// 28 - 35, 39 - 43.1
        //readonly Guid AsyncTestWorkflowValidateActions2ID = new Guid("3b9cf153-c482-e911-a2c5-005056926fe4"); //WF Async Automated Testing - Validate Actions for Async workflow 2 /////////// 38 - 40.7
        internal Guid AsyncTestWorkflowValidateActions1ID;
        internal Guid AsyncTestWorkflowValidateActions2ID;
        internal Guid AsyncTestWorkflowConditionsAndStepsID;
        internal Guid AsyncTestWorkflowOperatorsInConditionsID;
        internal Guid AsyncTestWorkflowComplexScenariosID;

        #region Private Properties
        private string tenantName;
        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _defaultBusinessUnitId;
        private Guid _healthBusinessUnitId;
        private Guid _defaultTeamId;
        private Guid _teamId2;
        private Guid _ethnicityId;
        private Guid _executeWorkflowscheduledJobId;
        private Guid _activityCategoryId;
        private Guid _activityCategoryId2;
        private Guid _activitySubCategoryId;
        private Guid _activitySubCategoryId2;
        private Guid _activityReasonId;
        private Guid _activityPriorityId;
        private Guid _activityOutcomeId;
        private Guid _phoneCallId;
        private Guid eventCategoryID;

        private Guid _providerId;
        private string _providerName = "Ynys Mon - Mental Health - Provider";

        #endregion

        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

        [TestInitialize()]
        public void TestClassInitializationMethod()
        {
            #region Tenant Information

            tenantName = System.Configuration.ConfigurationManager.AppSettings["TenantName"];

            #endregion

            #region Default user

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

            if (!dbHelper.businessUnit.GetByName("Health").Any())
                dbHelper.businessUnit.CreateBusinessUnit("Health");
            _healthBusinessUnitId = dbHelper.businessUnit.GetByName("Health")[0];

            #endregion

            #region Default Team          

            if (!dbHelper.team.GetTeamIdByName("UK Local Authority Social Care").Any())
                dbHelper.team.CreateTeam("UK Local Authority Social Care", null, _defaultBusinessUnitId, "907678", "UKLocalAuthoritySocialCare@careworkstempmail.com", "UK Local Authority Social Care", "020 123456");
            _defaultTeamId = dbHelper.team.GetTeamIdByName("UK Local Authority Social Care")[0];

            #endregion

            //#region Team 2
            //_teamId2 = commonWorkflowMethods.CreateTeam("Blaenau Gwent - Primary Health Social Worker", _defaultBusinessUnitId);

            //#endregion

            #region Ethnicity

            if (!dbHelper.ethnicity.GetEthnicityIdByName("Irish").Any())
                dbHelper.ethnicity.CreateEthnicity(_defaultTeamId, "Irish", new DateTime(2020, 1, 1));
            _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Irish")[0];

            #endregion

            #region Activity Categories

            if (!dbHelper.activityCategory.GetByName("Assessment").Any())
                dbHelper.activityCategory.CreateActivityCategory(new Guid("1d2b78b8-9d45-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _defaultTeamId);
            _activityCategoryId = dbHelper.activityCategory.GetByName("Assessment")[0];

            if (!dbHelper.activityCategory.GetByName("Advice").Any())
                dbHelper.activityCategory.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2019, 3, 13), _defaultTeamId);
            _activityCategoryId2 = dbHelper.activityCategory.GetByName("Advice")[0];


            #endregion

            #region Activity Sub Categories

            if (!dbHelper.activitySubCategory.GetByName("Health Assessment").Any())
                dbHelper.activitySubCategory.CreateActivitySubCategory(new Guid("eec317f4-9d45-e911-a2c5-005056926fe4"), "Health Assessment", new DateTime(2020, 1, 1), _activityCategoryId, _defaultTeamId);
            _activitySubCategoryId = dbHelper.activitySubCategory.GetByName("Health Assessment")[0];

            if (!dbHelper.activitySubCategory.GetByName("Home Support").Any())
                dbHelper.activitySubCategory.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId2, _defaultTeamId);
            _activitySubCategoryId2 = dbHelper.activitySubCategory.GetByName("Home Support")[0];

            #endregion

            #region Activity Reason

            if (!dbHelper.activityReason.GetByName("First Response").Any())
                dbHelper.activityReason.CreateActivityReason(new Guid("b9ec74e3-9c45-e911-a2c5-005056926fe4"), "First Response", new DateTime(2020, 1, 1), _defaultTeamId);
            _activityReasonId = dbHelper.activityReason.GetByName("First Response")[0];

            #endregion

            #region Activity Priority

            if (!dbHelper.activityPriority.GetByName("Normal").Any())
                dbHelper.activityPriority.CreateActivityPriority("Normal", new DateTime(2020, 1, 1), _defaultTeamId);
            _activityPriorityId = dbHelper.activityPriority.GetByName("Normal")[0];

            #endregion

            #region Activity Outcome

            if (!dbHelper.activityOutcome.GetByName("Completed").Any())
                dbHelper.activityOutcome.CreateActivityOutcome(new Guid("4c2bec1c-9e45-e911-a2c5-005056926fe4"), "Completed", new DateTime(2020, 1, 1), _defaultTeamId);
            _activityOutcomeId = dbHelper.activityOutcome.GetByName("Completed")[0];

            #endregion

            #region Provider

            if (!dbHelper.provider.GetProviderByName(_providerName).Any())
                _providerId = dbHelper.provider.CreateProvider(new Guid("81cc3d13-c7cd-4118-b60c-9f6596f966a4"), _providerName, _defaultTeamId);
            _providerId = dbHelper.provider.GetProviderByName(_providerName)[0];

            #endregion

            #region Scheduled Job Update

            _executeWorkflowscheduledJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Execute Workflow Jobs")[0];
            dbHelper.scheduledJob.UpdateScheduledJobEveryMinuteField(_executeWorkflowscheduledJobId, 1);
            #endregion
        }

        [TestProperty("JiraIssueID", "CDV6-7489")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 39 - Test Get Initiating User Action action (Initiating user is Workflow_Test_User_1)")]
        public void WorkflowDetailsSection_TestMethod039()
        {
            AsyncTestWorkflowValidateActions1ID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Validate Actions for Async workflow", "WF Async Automated Testing - Validate Actions for Async workflow.Zip");
            var teamId = commonWorkflowMethods.CreateTeam("UK Local Authority Social Care Team 2", _defaultBusinessUnitId);
            Guid workflow1UserId = commonWorkflowMethods.CreateSystemUserRecord("Workflow_Test_User_1", "Workflow", "Test_User_1", "Passw0rd_!", _defaultBusinessUnitId, teamId, _languageId, _authenticationproviderid);
            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 39";
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

            //ACT

            //login with Workflow_Test_User_1
            dbHelper = new DBHelper.DatabaseHelper("Workflow_Test_User_1", "Passw0rd_!", tenantName);

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowValidateActions1ID, 1).FirstOrDefault();
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "responsibleuserid", "callerid", "calleridname", "CallerIdTableName", "recipientid", "recipientIdTableName", "recipientIdName");
            Assert.AreEqual(workflow1UserId.ToString(), (string)fields["responsibleuserid"]);
            Assert.AreEqual(workflow1UserId.ToString(), (string)fields["callerid"]);
            Assert.AreEqual("Workflow Test_User_1", (string)fields["calleridname"]);
            Assert.AreEqual("systemuser", (string)fields["calleridtablename"]);
            Assert.AreEqual(workflow1UserId.ToString(), (string)fields["recipientid"]);
            Assert.AreEqual("Workflow Test_User_1", (string)fields["recipientidname"]);
            Assert.AreEqual("systemuser", (string)fields["recipientidtablename"]);
        }


        [TestProperty("JiraIssueID", "CDV6-7490")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 39.1 - Test Get Initiating User Action action (Initiating user is NOT Workflow_Test_User_1)")]
        public void WorkflowDetailsSection_TestMethod039_1()
        {
            AsyncTestWorkflowValidateActions1ID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Validate Actions for Async workflow", "WF Async Automated Testing - Validate Actions for Async workflow.Zip");
            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 39";
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

            //ACT
            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowValidateActions1ID, 1).FirstOrDefault();
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "responsibleuserid");
            Assert.AreEqual(false, fields.ContainsKey("responsibleuserid"));
            //Assert.IsNull(fields["responsibleuserid"]);
        }


        [TestProperty("JiraIssueID", "CDV6-7493")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 41 - Test Add Days action")]
        public void WorkflowDetailsSection_TestMethod041()
        {
            AsyncTestWorkflowValidateActions1ID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Validate Actions for Async workflow", "WF Async Automated Testing - Validate Actions for Async workflow.Zip");
            var teamId = commonWorkflowMethods.CreateTeam("UK Local Authority Social Care Team 2", _defaultBusinessUnitId);
            Guid workflow1UserId = commonWorkflowMethods.CreateSystemUserRecord("Workflow_Test_User_1", "Workflow", "Test_User_1", "Passw0rd_!", _defaultBusinessUnitId, teamId, _languageId, _authenticationproviderid);
            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 41";
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

            DateTime phonecallDate = new DateTime(2018, 1, 1);

            //ACT

            //login with Workflow_Test_User_1
            dbHelper = new DBHelper.DatabaseHelper("Workflow_Test_User_1", "Passw0rd_!", tenantName);

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phonecallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowValidateActions1ID, 1).FirstOrDefault();
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            DateTime expectedPhonecallDate = phonecallDate
                .AddYears(1)
                .AddMonths(2)
                .AddDays(21) //add 3 weeks = 3 * 7 = 21 days
                .AddDays(4)
                .AddHours(5)
                .AddMinutes(6);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual(expectedPhonecallDate.ToString("dd'/'MM'/'yyyy HH:mm:ss"), (string)fields["notes"]);
        }

        [TestProperty("JiraIssueID", "CDV6-7494")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 41 - Test Add Days action - phone call date is null")]
        public void WorkflowDetailsSection_TestMethod041_1()
        {
            AsyncTestWorkflowValidateActions1ID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Validate Actions for Async workflow", "WF Async Automated Testing - Validate Actions for Async workflow.Zip");
            var teamId = commonWorkflowMethods.CreateTeam("UK Local Authority Social Care Team 2", _defaultBusinessUnitId);
            commonWorkflowMethods.CreateSystemUserRecord("Workflow_Test_User_1", "Workflow", "Test_User_1", "Passw0rd_!", _defaultBusinessUnitId, teamId, _languageId, _authenticationproviderid);
            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 41";
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

            DateTime? phonecallDate = null;

            //ACT

            //login with Workflow_Test_User_1
            dbHelper = new DBHelper.DatabaseHelper("Workflow_Test_User_1", "Passw0rd_!", tenantName);

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phonecallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowValidateActions1ID, 1).FirstOrDefault();
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("01/01/0001 00:00:00", (string)fields["notes"]);
        }

        [TestProperty("JiraIssueID", "CDV6-7495")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 42 - Test Get Higher Date action (event date is the bigger date)")]
        public void WorkflowDetailsSection_TestMethod042()
        {
            AsyncTestWorkflowValidateActions1ID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Validate Actions for Async workflow", "WF Async Automated Testing - Validate Actions for Async workflow.Zip");
            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 42";
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
            DateTime phoneCallDate = new DateTime(2019, 2, 1, 9, 15, 30);
            
            bool InformationByThirdParty = false;
            int DirectionID = 1; //incoming

            bool IsSignificantEvent = true;
            DateTime SignificantEventDate = new DateTime(2019, 3, 1);
            eventCategoryID = commonWorkflowMethods.CreateSignificantEventCategory("ASyncWFEvent_" + DateTime.Now.ToString("yyyyMMddHHmmss"), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), _defaultTeamId, null, null, null);

            //ACT
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, userid,
                null, InformationByThirdParty, DirectionID, IsSignificantEvent, SignificantEventDate, eventCategoryID);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowValidateActions1ID, 1).FirstOrDefault();
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual(SignificantEventDate.AddSeconds(10).ToString("dd'/'MM'/'yyyy HH:mm:ss"), (string)fields["notes"]); //we need to add 10 seconds due to a issue with phoenix - //Some times while executing modified on adding on start date is greater than modified of due date, hence increase 10 second of due date to make start date always less than due date.
        }

        [TestProperty("JiraIssueID", "CDV6-7496")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 42.1 - Test Get Higher Date action (phone call date is the bigger date)")]
        public void WorkflowDetailsSection_TestMethod042_1()
        {
            AsyncTestWorkflowValidateActions1ID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Validate Actions for Async workflow", "WF Async Automated Testing - Validate Actions for Async workflow.Zip");
            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 42";
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
            DateTime phoneCallDate = new DateTime(2019, 4, 1, 9, 15, 30);

            bool InformationByThirdParty = false;
            int DirectionID = 1; //incoming

            bool IsSignificantEvent = true;
            DateTime SignificantEventDate = new DateTime(2019, 3, 1);
            eventCategoryID = commonWorkflowMethods.CreateSignificantEventCategory("ASyncWFEvent_" + DateTime.Now.ToString("yyyyMMddHHmmss"), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), _defaultTeamId, null, null, null);

            //ACT
            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, userid,
                null, InformationByThirdParty, DirectionID, IsSignificantEvent, SignificantEventDate, eventCategoryID);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowValidateActions1ID, 1).FirstOrDefault();
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");            
            Assert.AreEqual(phoneCallDate.AddSeconds(10).ToString("dd'/'MM'/'yyyy HH:mm:ss"), (string)fields["notes"]);
        }

        [TestProperty("JiraIssueID", "CDV6-7497")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 42.1 - Test Get Higher Date action (phone call date is null)")]
        public void WorkflowDetailsSection_TestMethod042_2()
        {
            AsyncTestWorkflowValidateActions1ID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Validate Actions for Async workflow", "WF Async Automated Testing - Validate Actions for Async workflow.Zip");
            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 42";
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
            DateTime? phoneCallDate = null;
            
            bool InformationByThirdParty = false;
            int DirectionID = 1; //incoming

            bool IsSignificantEvent = true;
            DateTime SignificantEventDate = new DateTime(2019, 3, 1);
            eventCategoryID = commonWorkflowMethods.CreateSignificantEventCategory("ASyncWFEvent_" + DateTime.Now.ToString("yyyyMMddHHmmss"), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), _defaultTeamId, null, null, null);

            //ACT
            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, userid,
                null, InformationByThirdParty, DirectionID, IsSignificantEvent, SignificantEventDate, eventCategoryID);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowValidateActions1ID, 1).FirstOrDefault();
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual(SignificantEventDate.AddSeconds(10).ToString("dd'/'MM'/'yyyy HH:mm:ss"), (string)fields["notes"]);
        }

        [TestProperty("JiraIssueID", "CDV6-7498")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 42.1 - Test Get Higher Date action (Event date is null)")]
        public void WorkflowDetailsSection_TestMethod042_3()
        {
            AsyncTestWorkflowValidateActions1ID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Validate Actions for Async workflow", "WF Async Automated Testing - Validate Actions for Async workflow.Zip");
            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 42";
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

            DateTime phoneCallDate = new DateTime(2019, 4, 1, 9, 15, 30);

            //ACT            
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null,
                null, false, 1, false, null, null, false);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowValidateActions1ID, 1).FirstOrDefault();
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual(phoneCallDate.AddSeconds(10).ToString("dd'/'MM'/'yyyy HH:mm:ss"), (string)fields["notes"]);
        }

        [TestProperty("JiraIssueID", "CDV6-7499")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 43 - Test Subtract Days action")]
        public void WorkflowDetailsSection_TestMethod043()
        {
            AsyncTestWorkflowValidateActions1ID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Validate Actions for Async workflow", "WF Async Automated Testing - Validate Actions for Async workflow.Zip");
            var teamId = commonWorkflowMethods.CreateTeam("UK Local Authority Social Care Team 2", _defaultBusinessUnitId);
            commonWorkflowMethods.CreateSystemUserRecord("Workflow_Test_User_1", "Workflow", "Test_User_1", "Passw0rd_!", _defaultBusinessUnitId, teamId, _languageId, _authenticationproviderid);
            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 43";
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
            DateTime phonecallDate = new DateTime(2018, 1, 1);

            //ACT

            //login with Workflow_Test_User_1
            dbHelper = new DBHelper.DatabaseHelper("Workflow_Test_User_1", "Passw0rd_!", tenantName);

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phonecallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowValidateActions1ID, 1).FirstOrDefault();
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            DateTime expectedPhoneCallDate = phonecallDate.ToUniversalTime()
                .AddYears(-1)
                .AddMonths(-2)
                .AddDays(-21) //add 3 weeks = 3 * 7 = 21 days
                .AddDays(-4)
                .AddHours(-5)
                .AddMinutes(-6);

            DateTime finalDate = expectedPhoneCallDate.ToLocalTime();

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual(finalDate.ToString("dd'/'MM'/'yyyy HH:mm:ss"), (string)fields["notes"]);
        }

        [TestProperty("JiraIssueID", "CDV6-7500")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 43 - Test Subtract Days action - phone call date is null")]
        public void WorkflowDetailsSection_TestMethod043_0()
        {
            AsyncTestWorkflowValidateActions1ID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Validate Actions for Async workflow", "WF Async Automated Testing - Validate Actions for Async workflow.Zip");
            var teamId = commonWorkflowMethods.CreateTeam("UK Local Authority Social Care Team 2", _defaultBusinessUnitId);
            commonWorkflowMethods.CreateSystemUserRecord("Workflow_Test_User_1", "Workflow", "Test_User_1", "Passw0rd_!", _defaultBusinessUnitId, teamId, _languageId, _authenticationproviderid);
            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 43";
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

            DateTime? phonecallDate = null;

            //ACT

            //login with Workflow_Test_User_1
            dbHelper = new DBHelper.DatabaseHelper("Workflow_Test_User_1", "Passw0rd_!", tenantName);

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phonecallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowValidateActions1ID, 1).FirstOrDefault();
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("01/01/0001 00:00:00", (string)fields["notes"]);
        }
    }
}

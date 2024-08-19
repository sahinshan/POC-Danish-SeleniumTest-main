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
    [DeploymentItem("Files\\Workflow Test Document 1.Zip")]
    [DeploymentItem("Files\\WF Async Automated Testing - Validate Actions for Async workflow.Zip")]
    [DeploymentItem("Files\\WF Async Automated Testing - Validate Actions for Async workflow 2.Zip")]
    [DeploymentItem("Files\\WF Async Workflow Testing - Validations of Conditions and Steps.Zip")]
    [DeploymentItem("Files\\WF Async Automated Testing - Testing the Operators in Conditions.Zip")]
    [DeploymentItem("Files\\WF Async Workflow Testing - Validations of complex scenarios.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - S31 - Child Process.Zip")]
    public class AsynchronousWorkflowTests_ak_Batch5 : BaseTestClass
    {

        //readonly Guid AsyncTestWorkflowConditionsAndStepsID = new Guid("71E2AF9B-8480-E911-A2C5-005056926FE4"); //WF Async Workflow Testing - Validations of Conditions and Steps //////// 46 - 78 - even nos
        internal Guid AsyncTestWorkflowValidateActions1ID;
        internal Guid AsyncTestWorkflowValidateActions2ID;
        internal Guid AsyncTestWorkflowConditionsAndStepsID;
        internal Guid AsyncTestWorkflowOperatorsInConditionsID;
        internal Guid AsyncTestWorkflowComplexScenariosID;

        #region Private Properties
        private string _tenantName;
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
        private Guid _activityReasonId2;
        private Guid _activityPriorityId;
        private Guid _activityOutcomeId;
        private Guid _activityOutcomeId2;
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
            
            _tenantName = ConfigurationManager.AppSettings["TenantName"];

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

            if (!dbHelper.activityReason.GetByName("Assessment").Any())
                dbHelper.activityReason.CreateActivityReason("Assessment", new DateTime(2020, 1, 1), _defaultTeamId);
            _activityReasonId2 = dbHelper.activityReason.GetByName("Assessment")[0];

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

            if (!dbHelper.activityOutcome.GetByName("More information needed").Any())
                dbHelper.activityOutcome.CreateActivityOutcome("More information needed", new DateTime(2020, 1, 1), _defaultTeamId);
            _activityOutcomeId2 = dbHelper.activityOutcome.GetByName("More information needed")[0];

            #endregion

            #region Scheduled Job Update

            _executeWorkflowscheduledJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Execute Workflow Jobs")[0];
            dbHelper.scheduledJob.UpdateScheduledJobEveryMinuteField(_executeWorkflowscheduledJobId, 1);
            #endregion

            #region Web API auth system user

            commonWorkflowMethods.CreateSystemUserRecord("webapiauthuser", "webapi", "authuser", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            #endregion

            #region Web API auth system user

            commonWorkflowMethods.CreateSystemUserRecord("webapiauthuser", "webapi", "authuser", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            #endregion
        }



        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7513")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 60 - Validation for a Date field")]
        public void WorkflowDetailsSection_TestMethod060()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 60";
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
            Guid eventCategoryID = commonWorkflowMethods.CreateSignificantEventCategory("ASyncWFEvent_" + DateTime.Now.ToString("yyyyMMddHHmmss"), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), _defaultTeamId, null, null, null);

            //ACT
            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, userid,
                null, InformationByThirdParty, DirectionID, IsSignificantEvent, SignificantEventDate, eventCategoryID);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 60 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7514")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 60 - Validation for a Date field")]
        public void WorkflowDetailsSection_TestMethod061()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 60";
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
            int DirectionID = 1;

            bool IsSignificantEvent = true;
            DateTime SignificantEventDate = new DateTime(2018, 10, 12);
            Guid eventCategoryID = commonWorkflowMethods.CreateSignificantEventCategory("ASyncWFEvent_" + DateTime.Now.ToString("yyyyMMddHHmmss"), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), _defaultTeamId, null, null, null);


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, userid,
                null, InformationByThirdParty, DirectionID, IsSignificantEvent, SignificantEventDate, eventCategoryID);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7515")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 62 - Validation for a DateTime field")]
        public void WorkflowDetailsSection_TestMethod062()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 62";
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

            DateTime phoneCallDate = new DateTime(2019, 3, 4);


            //ACT
            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 62 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7516")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 62 - Validation for a DateTime field")]
        public void WorkflowDetailsSection_TestMethod063()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 62";
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

            DateTime phoneCallDate = new DateTime(2019, 2, 10);

            //ACT
            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }


        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7517")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 64 - Validation for a MultiLookup field")]
        public void WorkflowDetailsSection_TestMethod064()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            //var firstName = "John";
            //var middleName = "WF Test";
            //var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");

            var firstName = "Adolfo";
            var middleName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var lastName = "Abbott";
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(new Guid("78a78172-6135-4d9f-9abb-d079a12b253d"), firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 64";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 64 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7518")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 64 - Validation for a MultiLookup field")]
        public void WorkflowDetailsSection_TestMethod065()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonWorkflowMethods.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            //Create Social care case for the person
            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 18);
            var caseTitle = (string)(dbHelper.Case.GetCaseByID(caseID, "Title")["title"]);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 64";
            string description = "Sample Description ...";
            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            //Guid regardingID = personId;
            //string regardingName = personFullName;
            Guid regardingID = caseID;
            string regardingName = caseTitle;

            //var personid = new Guid("1b18dee7-e41e-464b-afe4-50070253de4b");
            //var personidName = "Adrian Abbott";

            var personid = personId;
            var personidName = personFullName;

            //Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
            //Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

            //DateTime phoneCallDate = new DateTime(2019, 3, 4);

            //ACT
            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForCase(subject, description, callerID, callerIdTableName, callerIDName,
                recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, personid, personidName, _defaultTeamId, "UK Local Authority Social Care", DateTime.Now.WithoutMilliseconds(), null, null);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }



        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7519")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 66 - Validation for a Large Data Textbox field")]
        public void WorkflowDetailsSection_TestMethod066()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 66";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 66 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7520")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 66 - Validation for a Large Data Textbox field")]
        public void WorkflowDetailsSection_TestMethod067()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 66";
            string description = null;
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual(false, fields.ContainsKey("notes"));
        }




        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7521")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 68 - Validation for a Phone field")]
        public void WorkflowDetailsSection_TestMethod068()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 68";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 68 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7522")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 68 - Validation for a Phone field")]
        public void WorkflowDetailsSection_TestMethod069()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 68";
            string description = "Sample Description ...";
            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "9462748";

            Guid regardingID = personId;
            string regardingName = personFullName;

            //ACT
            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7523")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 70 - Validation for 'And' condition in if statment")]
        public void WorkflowDetailsSection_TestMethod070()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 70";
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

            DateTime phoneCallDate = new DateTime(2019, 3, 11);
            int direction = 1; //Incoming

            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, false, direction);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 70 - Action 1 Activated", (string)fields["notes"]);
        }



    }
}

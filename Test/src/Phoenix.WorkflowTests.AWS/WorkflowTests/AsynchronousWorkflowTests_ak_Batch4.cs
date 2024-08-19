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
    public class AsynchronousWorkflowTests_ak_Batch4 : BaseTestClass
    {

        //readonly Guid AsyncTestWorkflowConditionsAndStepsID = new Guid("71E2AF9B-8480-E911-A2C5-005056926FE4"); //WF Async Workflow Testing - Validations of Conditions and Steps //////// 46 - 78 - even nos
        internal Guid AsyncTestWorkflowValidateActions1ID;
        internal Guid AsyncTestWorkflowValidateActions2ID;
        internal Guid AsyncTestWorkflowConditionsAndStepsID;
        internal Guid AsyncTestWorkflowOperatorsInConditionsID;
        internal Guid AsyncTestWorkflowComplexScenariosID;

        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

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
        private Guid _activityReasonId2;
        private Guid _activityPriorityId;
        private Guid _activityOutcomeId;
        private Guid _activityOutcomeId2;
        private Guid _phoneCallId;
        private Guid eventCategoryID;

        private Guid _providerId;
        private string _providerName = "Ynys Mon - Mental Health - Provider";

        #endregion

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
        }



        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7501")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 46 - WF step with 2 inner condition statments")]
        public void WorkflowDetailsSection_TestMethod046()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);            

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 46";
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

            DateTime phoneCallDate = new DateTime(2019, 2, 1);

            //ACT
            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var descriptionAfterSave = (string)(dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes")["notes"]);
            Assert.AreEqual("WF Async Testing - Scenario 46 - Action 1 Activated", descriptionAfterSave);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7502")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 47 - WF step with 2 inner condition statments (2nd if statment not executing)")]
        public void WorkflowDetailsSection_TestMethod047()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);            

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 46";
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

            DateTime phoneCallDate = new DateTime(2019, 3, 1); //this phone call date should not trigger the 2nd if statment in the workflow

            //ACT
            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var descriptionAfterSave = (string)(dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes")["notes"]);
            Assert.AreEqual("Sample Description ...", descriptionAfterSave);
        }



        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7503")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 48 - Multiple Steps validation")]
        public void WorkflowDetailsSection_TestMethod048()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 48";
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


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Async Testing - Scenario 48 - Action 1 Activated", (string)fields["notes"]);
            Assert.AreEqual("01/02/2019 09:15:30 - 0987654321234", (string)fields["subject"]);
        }


        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7504")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 49 - Multiple Steps validation")]
        public void WorkflowDetailsSection_TestMethod049()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 48";
            string description = "Sample Description ...";
            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "";

            Guid regardingID = personId;
            string regardingName = personFullName;

            DateTime? phoneCallDate = null;


            //ACT
            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Async Testing - Scenario 48 - Action 1 Activated", (string)fields["notes"]);
            Assert.AreEqual("-", (string)fields["subject"]);
        }


        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7505")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 50 - If Else statments validation")]
        public void WorkflowDetailsSection_TestMethod050()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 50 - 1";
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


            //ACT
            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 50 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7506")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 51 - If Else statments validation")]
        public void WorkflowDetailsSection_TestMethod051()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 50 - 2";
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

            //ACT
            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 50 - Action 2 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7507")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 52 - Validation for a Business Object Reference field")]
        public void WorkflowDetailsSection_TestMethod052()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 52";
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

            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, userid, _activityCategoryId2);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1,  "notes");
            Assert.AreEqual("WF Async Testing - Scenario 52 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7508")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 52 - Validation for a Business Object Reference field")]
        public void WorkflowDetailsSection_TestMethod053()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 52";
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

            //Guid jBrazetaResponsibleUSerID = new Guid("32972024-0839-E911-A2C5-005056926FE4");

            //Guid assessmentActivityCategoryID = new Guid("1d2b78b8-9d45-e911-a2c5-005056926fe4");


            //ACT
            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, userid, _activityCategoryId);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");            
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7509")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 54 - Validation for a boolean field")]
        public void WorkflowDetailsSection_TestMethod054()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 54";
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
            
            bool InformationByThirdParty = true;



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, userid, null, InformationByThirdParty);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 54 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7510")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 54 - Validation for a boolean field")]
        public void WorkflowDetailsSection_TestMethod055()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 54";
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


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, userid, null, InformationByThirdParty);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7511")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 56 - Validation for a Picklist field")]
        public void WorkflowDetailsSection_TestMethod056()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 56";
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


            //ACT
            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, userid,
                null, InformationByThirdParty, DirectionID);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Async Testing - Scenario 56 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7512")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 56 - Validation for a Picklist field")]
        public void WorkflowDetailsSection_TestMethod057()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 56";
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
            int DirectionID = 2; //outgoing


            //ACT
            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, userid,
                null, InformationByThirdParty, DirectionID);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }
    }
}

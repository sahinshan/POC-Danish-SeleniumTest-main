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
    public class AsyncWorkflowDetailsSectionTests_VJ_Batch1 : BaseTestClass
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

        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _defaultBusinessUnitId;
        private Guid _healthBusinessUnitId;
        private Guid _defaultTeamId;
        private Guid _teamId2;
        private Guid _ethnicityId;
        private Guid _executeWorkflowscheduledJobId;
        private Guid _activityCategoryId;
        private Guid _activitySubCategoryId;
        private Guid _activityReasonId;
        private Guid _activityPriorityId;
        private Guid _phoneCallId;
        private string _tenantName;

        private Guid _providerId;
        private string _providerName = "Ynys Mon - Mental Health - Provider";

        #endregion

        [TestInitialize()]
        public void TestClassInitializationMethod()
        {
            #region Tenant

            _tenantName = ConfigurationManager.AppSettings["TenantName"];

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

            #endregion

            #region Activity Sub Categories

            if (!dbHelper.activitySubCategory.GetByName("Health Assessment").Any())
                dbHelper.activitySubCategory.CreateActivitySubCategory(new Guid("eec317f4-9d45-e911-a2c5-005056926fe4"), "Health Assessment", new DateTime(2020, 1, 1), _activityCategoryId, _defaultTeamId);
            _activitySubCategoryId = dbHelper.activitySubCategory.GetByName("Health Assessment")[0];

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

            #region Provider
            if (!dbHelper.provider.GetProviderByName(_providerName).Any())
                _providerId = dbHelper.provider.CreateProvider(new Guid("81cc3d13-c7cd-4118-b60c-9f6596f966a4"), _providerName, _defaultTeamId);
            _providerId = dbHelper.provider.GetProviderByName(_providerName)[0];

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
        [TestProperty("JiraIssueID", "CDV6-7524")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 70 - Validation for 'And' condition in if statment")]
        public void WorkflowDetailsSection_TestMethod071()
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

            DateTime phoneCallDate = new DateTime(2019, 3, 9);
            int direction = 1; //Incoming

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, false, direction);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7525")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 70 - Validation for 'And' condition in if statment")]
        public void WorkflowDetailsSection_TestMethod071_1()
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
            int direction = 2; //Incoming

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, false, direction);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7526")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 70 - Validation for 'And' condition in if statment")]
        public void WorkflowDetailsSection_TestMethod071_2()
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

            DateTime phoneCallDate = new DateTime(2019, 3, 4);
            int direction = 2; //Incoming

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, false, direction);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7527")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 72 - Validation for 'Or' condition in if statment")]
        public void WorkflowDetailsSection_TestMethod072()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 72";
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
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, false, direction);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 72 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7528")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 73 - Validation for 'Or' condition in if statment")]
        public void WorkflowDetailsSection_TestMethod073()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 72";
            string description = "Sample Description ...";
            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "234234234";

            Guid regardingID = personId;
            string regardingName = personFullName;

            DateTime phoneCallDate = new DateTime(2019, 3, 11);
            int direction = 1; //Incoming

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, false, direction);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 72 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7529")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 74 - Validation for 'Or' condition in if statment")]
        public void WorkflowDetailsSection_TestMethod074()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 72";
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
            int direction = 2; //outgoing

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, false, direction);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 72 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7530")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 75 - Validation for 'Or' condition in if statment")]
        public void WorkflowDetailsSection_TestMethod075()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 72";
            string description = "Sample Description ...";
            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "23424";

            Guid regardingID = personId;
            string regardingName = personFullName;

            DateTime phoneCallDate = new DateTime(2019, 3, 11);
            int direction = 2; //outgoing

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, false, direction);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7531")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 76 - Condition for related business object")]
        public void WorkflowDetailsSection_TestMethod076()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);
            dbHelper.person.UpdateNHSNumber(personId, "123 456 7881");

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 76";
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
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 76 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7532")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 77 - Condition for related business object")]
        public void WorkflowDetailsSection_TestMethod077()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 76";
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
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7533")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 78 - Condition for related business object")]
        public void WorkflowDetailsSection_TestMethod078()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 78.1";
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
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 78 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7534")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 79 - Condition for related business object")]
        public void WorkflowDetailsSection_TestMethod079()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 78.2";
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
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 78 - Action 2 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7535")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 80 - Condition for related business object")]
        public void WorkflowDetailsSection_TestMethod080()
        {
            AsyncTestWorkflowConditionsAndStepsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of Conditions and Steps", "WF Async Workflow Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 78.3";
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
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowConditionsAndStepsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7536")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 81 - Validate the 'Equals' operator")]
        public void WorkflowDetailsSection_TestMethod081()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 81";
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

            DateTime phonecalldate = DateTime.Now.Date;
            bool InformationByThirdParty = false;
            bool IsCaseNote = false;

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phonecalldate, null, null, InformationByThirdParty, 1, false, null, null, IsCaseNote);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            System.Threading.Thread.Sleep(2000);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 81 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7537")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 82 - Validate the 'Equals' operator")]
        public void WorkflowDetailsSection_TestMethod082()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 81";
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

            DateTime phonecalldate = DateTime.Now.Date;
            bool InformationByThirdParty = true;
            bool IsCaseNote = true;

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phonecalldate, null, null, InformationByThirdParty, 1, false, null, null, IsCaseNote);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 81 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7538")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 83 - Validate the 'Equals' operator")]
        public void WorkflowDetailsSection_TestMethod083()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 81";
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

            DateTime phonecalldate = DateTime.Now.Date;
            bool InformationByThirdParty = true;
            bool IsCaseNote = false;

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phonecalldate, null, null, InformationByThirdParty, 1, false, null, null, IsCaseNote);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7539")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 83 - Validate the 'Equals' operator")]
        public void WorkflowDetailsSection_TestMethod083_1()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 81";
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

            DateTime phonecalldate = DateTime.Now.Date;
            bool InformationByThirdParty = false;
            bool IsCaseNote = true;

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phonecalldate, null, null, InformationByThirdParty, 1, false, null, null, IsCaseNote);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7540")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 84 - Validate the 'Does Not Equal' operator")]
        public void WorkflowDetailsSection_TestMethod084()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 84";
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

            Guid jbrazetaUserID = userid;
            DateTime phonecalldate = DateTime.Now.Date;
            bool InformationByThirdParty = false;
            bool IsCaseNote = false;

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phonecalldate, jbrazetaUserID, null, InformationByThirdParty, 1, false, null, null, IsCaseNote);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 84 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7541")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 84.1 - Validate the 'Does Not Equal' operator (responsible user field is null)")]
        public void WorkflowDetailsSection_TestMethod084_1()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 84";
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


            DateTime phonecalldate = DateTime.Now.Date;
            bool InformationByThirdParty = false;
            bool IsCaseNote = false;

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phonecalldate, null, null, InformationByThirdParty, 1, false, null, null, IsCaseNote);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7542")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 84 - Validate the 'Does Not Equal' operator")]
        public void WorkflowDetailsSection_TestMethod085()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);
            var Workflow_Test_User_1USerID = commonWorkflowMethods.CreateSystemUserRecord(new Guid("B9B8B4C7-1552-E911-A2C5-005056926FE4"), "Workflow_Test_User_10", "Workflow", "Test User 10", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 84";
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

            DateTime phonecalldate = DateTime.Now.Date;
            bool InformationByThirdParty = false;
            bool IsCaseNote = false;

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phonecalldate, Workflow_Test_User_1USerID, null, InformationByThirdParty, 1, false, null, null, IsCaseNote);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7543")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 86 - Validate the 'contains data' operator")]
        public void WorkflowDetailsSection_TestMethod086()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 86";
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

            DateTime phoneCallDate = DateTime.Now.Date;
            bool IsSignificantEvent = true;
            DateTime SignificantEventDate = new DateTime(2019, 3, 1);
            Guid eventCathegoryID = commonWorkflowMethods.CreateSignificantEventCategoryIfNeeded("Category 1", DateTime.Now.Date, _defaultTeamId);

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 86 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7544")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 87 - Validate the 'contains data' operator")]
        public void WorkflowDetailsSection_TestMethod087()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 86";
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
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7545")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 88 - Validate the 'does not contains data' operator")]
        public void WorkflowDetailsSection_TestMethod088()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 88";
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

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 88 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7546")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 89 - Validate the 'does not contains data' operator")]
        public void WorkflowDetailsSection_TestMethod089()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 88";
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

            DateTime phoneCallDate = DateTime.Now;

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7547")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 97 - Validate the 'Like' operator")]
        public void WorkflowDetailsSection_TestMethod097()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 97";
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
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 97 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7548")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 97.1 - Validate the 'Like' operator")]
        public void WorkflowDetailsSection_TestMethod097_1()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 97";
            string description = "Sample Description ...";
            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "000098765432123444444";

            Guid regardingID = personId;
            string regardingName = personFullName;

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 97 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7549")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 98 - Validate the 'Like' operator")]
        public void WorkflowDetailsSection_TestMethod098()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 97";
            string description = "Sample Description ...";
            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "123456";

            Guid regardingID = personId;
            string regardingName = personFullName;

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7550")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 98.1 - Validate the 'Like' operator")]
        public void WorkflowDetailsSection_TestMethod098_1()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 97";
            string description = "Sample Description ...";
            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654";

            Guid regardingID = personId;
            string regardingName = personFullName;

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7551")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 99 - Validate the 'Starts With' operator")]
        public void WorkflowDetailsSection_TestMethod099()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 99";
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
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 99 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7552")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 100 - Validate the 'Starts With' operator")]
        public void WorkflowDetailsSection_TestMethod100()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 99";
            string description = "Sample Description ...";
            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0986654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7554")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 101 - Validate the 'Between' operator")]
        public void WorkflowDetailsSection_TestMethod101()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 101";
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

            DateTime PhoneCallDate = new DateTime(2019, 3, 6);

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 101 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7555")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 101.1 - Validate the 'Between' operator")]
        public void WorkflowDetailsSection_TestMethod101_1()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 101";
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

            DateTime PhoneCallDate = new DateTime(2019, 3, 7);

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 101 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7556")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 101.2 - Validate the 'Between' operator")]
        public void WorkflowDetailsSection_TestMethod101_2()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 101";
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

            DateTime PhoneCallDate = new DateTime(2019, 3, 8);

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 101 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7557")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 102 - Validate the 'Between' operator")]
        public void WorkflowDetailsSection_TestMethod102()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 101";
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

            DateTime PhoneCallDate = new DateTime(2019, 3, 9);

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7558")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 102.1 - Validate the 'Between' operator")]
        public void WorkflowDetailsSection_TestMethod102_1()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 101";
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

            DateTime PhoneCallDate = new DateTime(2019, 2, 28);

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7559")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 102.2 - Validate the 'Between' operator")]
        public void WorkflowDetailsSection_TestMethod102_2()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 101";
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

            DateTime? PhoneCallDate = null;

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7560")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 103 - Validate the 'Is Grated Than' operator")]
        public void WorkflowDetailsSection_TestMethod103()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 103";
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

            DateTime PhoneCallDate = new DateTime(2019, 3, 26);

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 103 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7561")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 104 - Validate the 'Is Grated Than' operator")]
        public void WorkflowDetailsSection_TestMethod104()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 103";
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

            DateTime PhoneCallDate = new DateTime(2019, 3, 25);

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7562")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 103.1 - Validate the 'Is Grated Than' operator")]
        public void WorkflowDetailsSection_TestMethod104_1()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 103";
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

            DateTime PhoneCallDate = new DateTime(2019, 3, 24);

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7563")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 103.1 - Validate the 'Is Grated Than' operator")]
        public void WorkflowDetailsSection_TestMethod104_2()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 103";
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

            DateTime? PhoneCallDate = null;

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7564")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 105 - Validate the 'In Future' operator")]
        public void WorkflowDetailsSection_TestMethod105()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 105";
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

            DateTime PhoneCallDate = DateTime.Now.AddDays(1).Date;

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 105 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7565")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 106 - Validate the 'In Future' operator")]
        public void WorkflowDetailsSection_TestMethod106()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 105";
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

            DateTime PhoneCallDate = DateTime.Now.AddDays(-1).Date;

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7566")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 106.1 - Validate the 'In Future' operator")]
        public void WorkflowDetailsSection_TestMethod106_1()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 105";
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

            DateTime? PhoneCallDate = null;

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7567")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 107 - Validate the 'Next N Days' operator")]
        public void WorkflowDetailsSection_TestMethod107()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 107";
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

            DateTime PhoneCallDate = DateTime.Now.AddDays(1).Date;

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 107 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7568")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 107.1 - Validate the 'Next N Days' operator")]
        public void WorkflowDetailsSection_TestMethod107_1()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 107";
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

            DateTime PhoneCallDate = DateTime.Now.AddDays(2).Date;

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 107 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7569")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 108 - Validate the 'Next N Days' operator")]
        public void WorkflowDetailsSection_TestMethod108()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 107";
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

            DateTime PhoneCallDate = DateTime.Now.AddDays(4);

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7570")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 108.1 - Validate the 'Next N Days' operator")]
        public void WorkflowDetailsSection_TestMethod108_1()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 107";
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

            DateTime PhoneCallDate = DateTime.Now.AddDays(-1).Date;

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7571")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 108.2 - Validate the 'Next N Days' operator")]
        public void WorkflowDetailsSection_TestMethod108_2()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 107";
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

            DateTime? PhoneCallDate = null;

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7572")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 109 - Validate the 'Last N Months' operator")]
        public void WorkflowDetailsSection_TestMethod109()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 109";
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

            DateTime PhoneCallDate = DateTime.Now.AddMonths(-1).Date;

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 109 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7573")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 109.1 - Validate the 'Last N Months' operator")]
        public void WorkflowDetailsSection_TestMethod109_1()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 109";
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

            DateTime PhoneCallDate = DateTime.Now.AddMonths(-2).AddDays(1).Date;

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("WF Async Testing - Scenario 109 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7574")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 109.2 - Validate the 'Last N Months' operator")]
        public void WorkflowDetailsSection_TestMethod109_2()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 109";
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

            DateTime PhoneCallDate = DateTime.Now.Date.AddMonths(-2).AddDays(-1);

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7575")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 109.3 - Validate the 'Last N Months' operator")]
        public void WorkflowDetailsSection_TestMethod109_3()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 109";
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

            DateTime PhoneCallDate = DateTime.Now.AddDays(1).Date;

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7576")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 109.4 - Validate the 'Last N Months' operator")]
        public void WorkflowDetailsSection_TestMethod109_4()
        {
            AsyncTestWorkflowOperatorsInConditionsID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 109";
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

            DateTime? PhoneCallDate = null;

            //ACT
            //create the records
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowOperatorsInConditionsID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        public void GetTestNames()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("TestName,Description,JiraID");

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

                if (testMethod != null && propertyAttr != null)
                {
                    if (!string.IsNullOrEmpty(propertyAttr.Value))
                        sb.AppendLine(propertyAttr.Value);
                    //sb.AppendLine(method.Name + "," + descAttr.Description + "," + propertyAttr.Value);
                    continue;
                }
                //if (testMethod != null)
                //{
                //    sb.AppendLine(method.Name + "," + descAttr.Description);
                //    continue;
                //}

            }

            Console.WriteLine(sb.ToString());
        }

    }
}

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
    public class AsyncWorkflowDetailsSectionTests : BaseTestClass
    {
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
            dbHelper.team.UpdateEmailAddress(_defaultTeamId, "UKLocalAuthoritySocialCare@careworkstempmail.com");

            #endregion

            #region Ethnicity

            if (!dbHelper.ethnicity.GetEthnicityIdByName("Irish").Any())
                dbHelper.ethnicity.CreateEthnicity(_defaultTeamId, "Irish", new DateTime(2020, 1, 1));
            _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Irish")[0];

            #endregion

            #region Activity Categories

            _activityCategoryId = commonWorkflowMethods.CreateActivityCategory(new Guid("1d2b78b8-9d45-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _defaultTeamId);
            //if (!dbHelper.activityCategory.GetByName("Assessment").Any())
            //    dbHelper.activityCategory.CreateActivityCategory(new Guid("1d2b78b8-9d45-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _defaultTeamId);
            //_activityCategoryId = dbHelper.activityCategory.GetByName("Assessment")[0];

            _activityCategoryId2 = commonWorkflowMethods.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2019, 3, 13), _defaultTeamId);
            //if (!dbHelper.activityCategory.GetByName("Advice").Any())
            //    dbHelper.activityCategory.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2019, 3, 13), _defaultTeamId);
            //_activityCategoryId2 = dbHelper.activityCategory.GetByName("Advice")[0];


            #endregion

            #region Activity Sub Categories

            _activitySubCategoryId = commonWorkflowMethods.CreateActivitySubCategory(new Guid("eec317f4-9d45-e911-a2c5-005056926fe4"), "Health Assessment", new DateTime(2020, 1, 1), _activityCategoryId, _defaultTeamId);
            //if (!dbHelper.activitySubCategory.GetByName("Health Assessment").Any())
            //    dbHelper.activitySubCategory.CreateActivitySubCategory(new Guid("eec317f4-9d45-e911-a2c5-005056926fe4"), "Health Assessment", new DateTime(2020, 1, 1), _activityCategoryId, _defaultTeamId);
            //_activitySubCategoryId = dbHelper.activitySubCategory.GetByName("Health Assessment")[0];

            _activitySubCategoryId2 = commonWorkflowMethods.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId2, _defaultTeamId);
            //if (!dbHelper.activitySubCategory.GetByName("Home Support").Any())
            //    dbHelper.activitySubCategory.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId2, _defaultTeamId);
            //_activitySubCategoryId2 = dbHelper.activitySubCategory.GetByName("Home Support")[0];

            #endregion

            #region Activity Reason

            _activityReasonId = commonWorkflowMethods.CreateActivityReason(new Guid("b9ec74e3-9c45-e911-a2c5-005056926fe4"), "First Response", new DateTime(2020, 1, 1), _defaultTeamId);
            //if (!dbHelper.activityReason.GetByName("First Response").Any())
            //    dbHelper.activityReason.CreateActivityReason(new Guid("b9ec74e3-9c45-e911-a2c5-005056926fe4"), "First Response", new DateTime(2020, 1, 1), _defaultTeamId);
            //_activityReasonId = dbHelper.activityReason.GetByName("First Response")[0];

            #endregion

            #region Activity Priority

            if (!dbHelper.activityPriority.GetByName("Normal").Any())
                dbHelper.activityPriority.CreateActivityPriority("Normal", new DateTime(2020, 1, 1), _defaultTeamId);
            _activityPriorityId = dbHelper.activityPriority.GetByName("Normal")[0];

            #endregion

            #region Activity Outcome

            _activityOutcomeId = commonWorkflowMethods.CreateActivityOutcome(new Guid("4c2bec1c-9e45-e911-a2c5-005056926fe4"), "Completed", new DateTime(2020, 1, 1), _defaultTeamId);
            //if (!dbHelper.activityOutcome.GetByName("Completed").Any())
            //    dbHelper.activityOutcome.CreateActivityOutcome(new Guid("4c2bec1c-9e45-e911-a2c5-005056926fe4"), "Completed", new DateTime(2020, 1, 1), _defaultTeamId);
            //_activityOutcomeId = dbHelper.activityOutcome.GetByName("Completed")[0];

            #endregion

            #region Provider
            
            if (!dbHelper.provider.GetProviderByName(_providerName).Any())
                _providerId = dbHelper.provider.CreateProvider(new Guid("81cc3d13-c7cd-4118-b60c-9f6596f966a4"),_providerName, _defaultTeamId);
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

        [TestProperty("JiraIssueID", "CDV6-7476")]
        [Description("Automation Script for the Test Case - Scenario 28 - Test update record action")]
        public void WorkflowDetailsSection_TestMethod028()
        {
            AsyncTestWorkflowValidateActions1ID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Validate Actions for Async workflow", "WF Async Automated Testing - Validate Actions for Async workflow.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 28";
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

            dbHelper = new DBHelper.DatabaseHelper();

            //ACT
            //create the record
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowValidateActions1ID, 1).FirstOrDefault();

            webApiHelper.Security.Authenticate(tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            //System.Threading.Thread.Sleep(60000);
            var phoneCallFields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "CallerId", "CallerIdTableName", "CallerIdName", "PhoneNumber", "DirectionId", "subject", "PhoneCallDate");

            Guid providerCallerID = _providerId;
            string providerCallerIDTableName = "provider";
            //string providerName = "Ynys Mon - Mental Health - Provider";

            Assert.AreEqual(providerCallerID.ToString(), phoneCallFields["callerid".ToLower()].ToString());
            Assert.AreEqual(providerCallerIDTableName, (string)phoneCallFields["calleridtablename".ToLower()]);
            Assert.AreEqual(_providerName, (string)phoneCallFields["calleridname".ToLower()]);
            Assert.AreEqual("987654321", (string)phoneCallFields["phonenumber".ToLower()]);
            Assert.AreEqual(2, (int)phoneCallFields["directionid".ToLower()]);
            Assert.AreEqual("WF Async Testing - Scenario 28 - Action 1 Activated", (string)phoneCallFields["subject".ToLower()]);
            Assert.AreEqual(new DateTime(2019, 3, 1, 9, 0, 0), (DateTime)phoneCallFields["phonecalldate".ToLower()]);

        }


        [TestProperty("JiraIssueID", "CDV6-7477")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 29 - Test Assign record action")]       
        public void WorkflowDetailsSection_TestMethod029()
        {
            AsyncTestWorkflowValidateActions1ID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Validate Actions for Async workflow", "WF Async Automated Testing - Validate Actions for Async workflow.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var teamId = commonWorkflowMethods.CreateTeam(new Guid("214a6bd2-9adf-4b1c-a0c0-a123b58471bd"), "Blaenau Gwent - Primary Health Social Worker", userid,_healthBusinessUnitId);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 29";
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
            //create the record
            _phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowValidateActions1ID, 1).FirstOrDefault();

            webApiHelper.Security.Authenticate(tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var phoneCallFields = dbHelper.phoneCall.GetPhoneCallByID(_phoneCallId, "OwnerId", "OwningBusinessUnitId");
            

            //Guid blaenauGwentPrimaryHealthSocialWorkerTeamID = new Guid("214a6bd2-9adf-4b1c-a0c0-a123b58471bd");
            //Guid healthBusinessUnitID = new Guid("4567d62a-1039-e911-a2c5-005056926fe4");

            Assert.AreEqual(teamId.ToString(), phoneCallFields["OwnerId".ToLower()].ToString());
            Assert.AreEqual(_healthBusinessUnitId.ToString(), phoneCallFields["OwningBusinessUnitId".ToLower()].ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-7478")]
        [TestMethod]        
        [Description("Automation Script for the Test Case - Scenario 30 - Test Send Email record action")]
        public void WorkflowDetailsSection_TestMethod030()
        {
            AsyncTestWorkflowValidateActions1ID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Validate Actions for Async workflow", "WF Async Automated Testing - Validate Actions for Async workflow.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);
            //var fromUserid = commonWorkflowMethods.CreateSystemUserRecord("ajaykini", "Ajay", "Kini", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //Guid regardingID2 = commonWorkflowMethods.CreatePersonRecord(new Guid("78a78172-6135-4d9f-9abb-d079a12b253d"), "Adolfo", "Sanders", "Abbott", _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 30";
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

            ////get all Email records for the person
            //List<Guid> emailIDs = dbHelper.email.GetEmailByRegardingID(regardingID2);

            ////Delete the records
            //foreach (Guid mailId in emailIDs)
            //{
            //    dbHelper.email.DeleteEmail(mailId);
            //}


            //ACT


            //create the record
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowValidateActions1ID, 1).FirstOrDefault();

            webApiHelper.Security.Authenticate(tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);


            //get all Email records for the person
            List<Guid> emailIDs = dbHelper.email.GetEmailByRegardingID(regardingID);
            Assert.AreEqual(1, emailIDs.Count);

            //get the email info
            var email = dbHelper.email.GetEmailByID(emailIDs[0],
                "Subject", "RegardingID", "RegardingIDName", "RegardingIdTableName", "DueDate", "emailfromlookupid", "Notes",
                "ActivityReasonId", "ActivityOutcomeId", "ActivityCategoryId", "ActivitySubCategoryId", "PersonId", "ResponsibleUserId", "ownerid");

            Assert.AreEqual("WF Async Testing - Scenario 30 - Action 1 Activated", (string)email["subject".ToLower()]);
            Assert.AreEqual(regardingID.ToString(), email["RegardingID".ToLower()].ToString());
            Assert.AreEqual(regardingName, email["RegardingIDName".ToLower().ToString()]);
            Assert.AreEqual("person", email["RegardingIdTableName".ToLower()].ToString());
            Assert.AreEqual(new DateTime(2019, 03, 01, 9, 0, 0), (DateTime)email["DueDate".ToLower()]);
            Assert.AreEqual(_defaultTeamId.ToString(), email["emailfromlookupid".ToLower()].ToString());
            Assert.AreEqual("Mail Description ...", email["notes".ToLower()].ToString());
            Assert.AreEqual(_activityReasonId.ToString(), email["activityreasonid".ToLower()].ToString());//First Response
            Assert.AreEqual(_activityOutcomeId.ToString(), email["activityoutcomeid".ToLower()].ToString());//Completed
            Assert.AreEqual(_activityCategoryId2.ToString(), email["activitycategoryid".ToLower()].ToString());//Advice
            Assert.AreEqual(_activitySubCategoryId2.ToString(), email["activitysubCategoryid".ToLower()].ToString()); //Home Support
            Assert.AreEqual(regardingID.ToString(), email["personid".ToLower()].ToString());
            //Assert.AreEqual(userid.ToString(), email["ResponsibleUserId".ToLower()].ToString());
            Assert.AreEqual(false, email.ContainsKey("responsibleuserid".ToLower()));
            Assert.AreEqual(_defaultTeamId.ToString(), email["ownerid".ToLower()].ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-7479")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 31 - Test Start Wrokflow action")]
        public void WorkflowDetailsSection_TestMethod031()
        {

            AsyncTestWorkflowValidateActions1ID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Validate Actions for Async workflow", "WF Async Automated Testing - Validate Actions for Async workflow.Zip");
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S31 - Child Process", "WF Automated Testing - S31 - Child Process.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);            

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 31";
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
            //create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowValidateActions1ID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            //get all Phone Call records for the person
            var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "subject");

            Assert.AreEqual("WF Testing - Scenario 31 - Action 1 Activated", phoneCallfields["subject".ToLower()].ToString());
        }


        [TestProperty("JiraIssueID", "CDV6-7480")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 32 - Test Change Record Status action")]
        public void WorkflowDetailsSection_TestMethod032()
        {
            AsyncTestWorkflowValidateActions1ID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Validate Actions for Async workflow", "WF Async Automated Testing - Validate Actions for Async workflow.Zip");           

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 32";
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
            //create the record            
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);

            //ASSERT            
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowValidateActions1ID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            //get all Phone Call records for the person            
            var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "Inactive");
            Assert.AreEqual(true, (bool)phoneCallfields["inactive".ToLower()]);
        }

        [TestProperty("JiraIssueID", "CDV6-7481")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 33 - Test Stop Workflow action")]
        public void WorkflowDetailsSection_TestMethod033()
        {
            AsyncTestWorkflowValidateActions1ID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Validate Actions for Async workflow", "WF Async Automated Testing - Validate Actions for Async workflow.Zip");            

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 33";
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
            //create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowValidateActions1ID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);


            //get all Phone Call records for the person
            var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "subject");
            Assert.AreEqual("WF Async Testing - Scenario 33", phoneCallfields["subject".ToLower()].ToString());
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations to Business Data Actions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7482")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 34 - Test Apply Data Restriction action")]
        public void WorkflowDetailsSection_TestMethod034()
        {
            AsyncTestWorkflowValidateActions1ID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Validate Actions for Async workflow", "WF Async Automated Testing - Validate Actions for Async workflow.Zip");
            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var user2id = commonWorkflowMethods.CreateSystemUserRecord("Bakshish.Singh", "Bakshish", "Singh", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var dataRestrictionDenyID = commonWorkflowMethods.CreateDataRestrictionRecord(new Guid("00ea02a5-2852-e911-a2c5-005056926fe4"), "Workflow Data Restriction 4", 2, _defaultTeamId);
            commonWorkflowMethods.CreateUserRestrictedDataAccess(dataRestrictionDenyID, user2id, DateTime.Now.Date, _defaultTeamId);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 34";
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
            //create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);

            //ASSERT            
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowValidateActions1ID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "datarestrictionid");
            Assert.AreEqual(dataRestrictionDenyID.ToString(), phoneCallfields["DataRestrictionId".ToLower()].ToString());
        }

        ///// <summary>
        ///// Workflow under test: WF Automated Testing - Validations to Business Data Actions
        ///// </summary>
        //[TestProperty("JiraIssueID", "CDV6-7483")]
        //[TestMethod]
        //[Description("Automation Script for the Test Case - Scenario 34 - Test Apply Data Restriction action (WF action should override existing data restrictions)")]
        //public void WorkflowDetailsSection_TestMethod034_1()
        //{
        //    AsyncTestWorkflowValidateActions1ID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Validate Actions for Async workflow", "WF Async Automated Testing - Validate Actions for Async workflow.Zip");
        //    var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);


        //    var user2id = commonWorkflowMethods.CreateSystemUserRecord("Bakshish.Singh", "Bakshish", "Singh", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

        //    var dataRestrictionDenyID = commonWorkflowMethods.CreateDataRestrictionRecord(new Guid("00ea02a5-2852-e911-a2c5-005056926fe4"), "Workflow Data Restriction 4", 2, _defaultTeamId);
        //    commonWorkflowMethods.CreateUserRestrictedDataAccess(dataRestrictionDenyID, user2id, DateTime.Now.Date, _defaultTeamId);
        //    var dataRestrictionAllowUserOrTeamID = commonWorkflowMethods.CreateDataRestrictionRecord(new Guid("66f587ed-2752-e911-a2c5-005056926fe4"), "Workflow Data Restriction 1", 1, _defaultTeamId);
        //    commonWorkflowMethods.CreateTeamRestrictedDataAccess(dataRestrictionAllowUserOrTeamID, _defaultTeamId, DateTime.Now.Date, _defaultTeamId);

        //    var firstName = "John";
        //    var middleName = "WF Test";
        //    var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
        //    var personFullName = firstName + " " + lastName;
        //    var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

        //    //ARRANGE 
        //    string subject = "Temporary Subject";
        //    string description = "Sample Description ...";
        //    Guid callerID = personId;
        //    string callerIdTableName = "person";
        //    string callerIDName = personFullName;
        //    Guid recipientID = userid;
        //    string recipientIdTableName = "systemuser";
        //    string recipientIDName = "José Brazeta";
        //    string phoneNumber = "0987654321234";
        //    Guid regardingID = personId;
        //    string regardingName = personFullName;

        //    webApiHelper.Security.Authenticate(tenantName, "webapiauthuser", "Passw0rd_!");

        //    //create the record
        //    Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);

        //    var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowValidateActions1ID, 1).FirstOrDefault();
        //    webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
        //    dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

        //    //ACT
        //    //Update the record
        //    dbHelper.phoneCall.RestrictPhoneCall(phoneCallID, dataRestrictionAllowUserOrTeamID);
        //    dbHelper.phoneCall.UpdatePhoneCallSubject(phoneCallID, "WF Async Testing - Scenario 34");

        //    System.Threading.Thread.Sleep(2000);

        //    //ASSERT
        //    newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowValidateActions1ID, 1).FirstOrDefault();
        //    webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
        //    dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

        //    var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "datarestrictionid");
        //    Assert.AreEqual(dataRestrictionDenyID.ToString(), phoneCallfields["DataRestrictionId".ToLower()].ToString());
        //}

        ///// <summary>
        ///// Workflow under test: WF Automated Testing - Validations to Business Data Actions
        ///// </summary>
        //[TestProperty("JiraIssueID", "CDV6-7484")]
        //[TestMethod]
        //[Description("Automation Script for the Test Case - Scenario 34.2 - Test Apply Data Restriction action (WF action should not be able to override existing data restrictions)")]
        //public void WorkflowDetailsSection_TestMethod034_2()
        //{
        //    AsyncTestWorkflowValidateActions1ID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Validate Actions for Async workflow", "WF Async Automated Testing - Validate Actions for Async workflow.Zip");
        //    var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

        //    var user2id = commonWorkflowMethods.CreateSystemUserRecord("Bakshish.Singh", "Bakshish", "Singh", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

        //    var dataRestrictionDenyID = commonWorkflowMethods.CreateDataRestrictionRecord(new Guid("00ea02a5-2852-e911-a2c5-005056926fe4"), "Workflow Data Restriction 4", 2, _defaultTeamId);
        //    commonWorkflowMethods.CreateUserRestrictedDataAccess(dataRestrictionDenyID, user2id, DateTime.Now.Date, _defaultTeamId);
        //    //var dataRestrictionAllowUserOrTeamID = commonWorkflowMethods.CreateDataRestrictionRecord(new Guid("66f587ed-2752-e911-a2c5-005056926fe4"), "Workflow Data Restriction 1", 1, _defaultTeamId);
        //    //commonWorkflowMethods.CreateTeamRestrictedDataAccess(dataRestrictionAllowUserOrTeamID, _defaultTeamId, DateTime.Now.Date, _defaultTeamId);

        //    var firstName = "John";
        //    var middleName = "WF Test";
        //    var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
        //    var personFullName = firstName + " " + lastName;
        //    var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

        //    //ARRANGE 
        //    string subject = "Temporary Subject";
        //    string description = "Sample Description ...";
        //    Guid callerID = personId;
        //    string callerIdTableName = "person";
        //    string callerIDName = personFullName;
        //    Guid recipientID = userid;
        //    string recipientIdTableName = "systemuser";
        //    string recipientIDName = "José Brazeta";
        //    string phoneNumber = "0987654321234";
        //    Guid regardingID = personId;
        //    string regardingName = personFullName;

        //    //create the record
        //    Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


        //    //ACT
        //    //Update the record
        //    dbHelper.phoneCall.RestrictPhoneCall(phoneCallID, dataRestrictionDenyID);
        //    dbHelper.phoneCall.UpdatePhoneCallSubject(phoneCallID, "WF Async Testing - Scenario 34.2");

        //    //ASSERT
        //    var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowValidateActions1ID, 1).FirstOrDefault();
        //    webApiHelper.Security.Authenticate(tenantName, "webapiauthuser", "Passw0rd_!");
        //    webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
        //    dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

        //    var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "datarestrictionid");
        //    Assert.AreEqual(dataRestrictionDenyID.ToString(), phoneCallfields["DataRestrictionId".ToLower()].ToString());
        //}

        ///// <summary>
        ///// Workflow under test: WF Automated Testing - Validations to Business Data Actions
        ///// </summary>
        //[TestProperty("JiraIssueID", "CDV6-7485")]
        //[TestMethod]
        //[Description("Automation Script for the Test Case - Scenario 34.3 - Test Apply Data Restriction action (WF action should NOT take effect if the restriction will restrict the responsible user)")]
        //public void WorkflowDetailsSection_TestMethod034_3()
        //{
        //    AsyncTestWorkflowValidateActions1ID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Validate Actions for Async workflow", "WF Async Automated Testing - Validate Actions for Async workflow.Zip");
        //    var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);            

        //    var dataRestrictionResponsibleUserID = commonWorkflowMethods.CreateDataRestrictionRecord(new Guid("0db0a490-2852-e911-a2c5-005056926fe4"), "Workflow Data Restriction 3", 2, _defaultTeamId);
        //    commonWorkflowMethods.CreateUserRestrictedDataAccess(dataRestrictionResponsibleUserID, userid, DateTime.Now.Date, _defaultTeamId);

        //    var firstName = "John";
        //    var middleName = "WF Test";
        //    var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
        //    var personFullName = firstName + " " + lastName;
        //    var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

        //    //ARRANGE 
        //    string subject = "Temporary Subject";
        //    string description = "Sample Description ...";
        //    Guid callerID = personId;
        //    string callerIdTableName = "person";
        //    string callerIDName = personFullName;
        //    Guid recipientID = userid;
        //    string recipientIdTableName = "systemuser";
        //    string recipientIDName = "José Brazeta";
        //    string phoneNumber = "0987654321234";
        //    Guid regardingID = personId;
        //    string regardingName = personFullName;
        //    //Guid responsibleUserID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
        //    Guid responsibleUserID = userid;

        //    //create the record
        //    Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, DateTime.Now, responsibleUserID);

        //    //ACT

        //    //Update the record
        //    dbHelper.phoneCall.RestrictPhoneCall(phoneCallID, dataRestrictionResponsibleUserID);
        //    dbHelper.phoneCall.UpdatePhoneCallSubject(phoneCallID, "WF Async Testing - Scenario 34.3");

        //    //ASSERT
        //    var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowValidateActions1ID, 1).FirstOrDefault();
        //    webApiHelper.Security.Authenticate(tenantName, "webapiauthuser", "Passw0rd_!");
        //    webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
        //    dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

        //    var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "datarestrictionid");            
        //    Assert.AreEqual(dataRestrictionResponsibleUserID.ToString(), phoneCallfields["DataRestrictionId".ToLower()].ToString());
        //}


        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations to Business Data Actions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7486")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 35 - Test Business Data Count action")]
        public void WorkflowDetailsSection_TestMethod035()
        {
            AsyncTestWorkflowValidateActions1ID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Validate Actions for Async workflow", "WF Async Automated Testing - Validate Actions for Async workflow.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "John";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Async Testing - Scenario 35";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone call 1", description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);
            Guid phoneCallID2 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowValidateActions1ID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowValidateActions1ID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var phoneCall1Fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            var phoneCall2Fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID2, "notes");

            Assert.AreEqual("Sample Description ...", phoneCall1Fields["notes".ToLower()].ToString());
            Assert.AreEqual("2", phoneCall2Fields["notes".ToLower()].ToString()); //if the count operation succeded then the description field should be updated
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

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareDirector.Sdk.SystemEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phoenix.DBHelper;
using Phoenix.DBHelper.Models;
using Phoenix.WorkflowTestFramework;

namespace Phoenix.WorkflowTests.AWS
{
    [TestClass]
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("Files\\WF Async Workflow Testing - Validations of complex scenarios.Zip")]
    [DeploymentItem("Files\\Person Initial Assessment.Zip")]
    [DeploymentItem("Files\\WF Async Automated Testing - Testing the Operators in Conditions.Zip")]
    public class AsyncWorkflowDetailsSectionTests_JB : BaseTestClass
    {
        
        
        #region Private Properties

        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _defaultBusinessUnitId;
        private Guid _defaultTeamId;
        private Guid _ethnicityId;
        private Guid _executeWorkflowscheduledJobId;
        private Guid _activityCategoryId;
        private Guid _activitySubCategoryId;
        private Guid _activityReasonId;
        private Guid _activityPriorityId;
        private string _tenantName;
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
                dbHelper.activityReason.CreateActivityReason("First Response", new DateTime(2020, 1, 1), _defaultTeamId);
            _activityReasonId = dbHelper.activityReason.GetByName("First Response")[0];

            #endregion

            #region Activity Priority

            if (!dbHelper.activityPriority.GetByName("Normal").Any())
                dbHelper.activityPriority.CreateActivityPriority("Normal", new DateTime(2020, 1, 1), _defaultTeamId);
            _activityPriorityId = dbHelper.activityPriority.GetByName("Normal")[0];

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
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7577")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 111 - Validate the 'Older Than N Years' operator")]
        public void WorkflowDetailsSection_TestMethod111()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 111";
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



            DateTime PhoneCallDate = DateTime.Now.AddYears(-2).AddDays(-1).Date;

            

            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Async Testing - Scenario 111 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7578")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 112 - Validate the 'Older Than N Years' operator")]
        public void WorkflowDetailsSection_TestMethod112()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 111";
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



            DateTime PhoneCallDate = DateTime.Now.AddYears(-2).AddDays(1).Date;

            

            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7579")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 112.1 - Validate the 'Older Than N Years' operator")]
        public void WorkflowDetailsSection_TestMethod112_1()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 111";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7580")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 112.2 - Validate the 'Older Than N Years' operator")]
        public void WorkflowDetailsSection_TestMethod112_2()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 111";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }



        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7581")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 113 - Validate the 'Today' operator")]
        public void WorkflowDetailsSection_TestMethod113()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 113";
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



            DateTime PhoneCallDate = DateTime.Now.Date.AddHours(8);

            

            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Async Testing - Scenario 113 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7582")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 114 - Validate the 'Today' operator")]
        public void WorkflowDetailsSection_TestMethod114()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 113";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7583")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 114.1 - Validate the 'Today' operator")]
        public void WorkflowDetailsSection_TestMethod114_1()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 113";
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



            DateTime PhoneCallDate = DateTime.Now.AddDays(1).Date.AddHours(7);

            

            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7584")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 114.2 - Validate the 'Today' operator")]
        public void WorkflowDetailsSection_TestMethod114_2()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 113";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }



        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7585")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 115 - Validate the 'Not Between' operator")]
        public void WorkflowDetailsSection_TestMethod115()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 115";
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



            DateTime PhoneCallDate = new DateTime(2019, 3, 11);

            

            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Async Testing - Scenario 115 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7586")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 115.1 - Validate the 'Not Between' operator")]
        public void WorkflowDetailsSection_TestMethod115_1()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 115";
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



            DateTime PhoneCallDate = new DateTime(2019, 3, 15);

            

            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Async Testing - Scenario 115 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7587")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 116 - Validate the 'Not Between' operator")]
        public void WorkflowDetailsSection_TestMethod116()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 115";
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



            DateTime PhoneCallDate = new DateTime(2019, 3, 12);

            

            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7588")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 116.1 - Validate the 'Not Between' operator")]
        public void WorkflowDetailsSection_TestMethod116_1()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 115";
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



            DateTime PhoneCallDate = new DateTime(2019, 3, 13);

            

            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7589")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 116.2 - Validate the 'Not Between' operator")]
        public void WorkflowDetailsSection_TestMethod116_2()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 115";
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



            DateTime PhoneCallDate = new DateTime(2019, 3, 14);

            

            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7590")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 116.3 - Validate the 'Not Between' operator")]
        public void WorkflowDetailsSection_TestMethod116_3()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 115";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }










        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7591")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 117 - Validate the 'Is Grated Than Or Equal To' operator")]
        public void WorkflowDetailsSection_TestMethod117()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE
            string subject = "WF Async Testing - Scenario 117";
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



            DateTime PhoneCallDate = new DateTime(2019, 3, 14);

            


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Async Testing - Scenario 117 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7592")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 118 - Validate the 'Is Grated Than Or Equal To' operator")]
        public void WorkflowDetailsSection_TestMethod118()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE
            string subject = "WF Async Testing - Scenario 117";
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



            DateTime PhoneCallDate = new DateTime(2019, 3, 15);

            


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Async Testing - Scenario 117 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7593")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 119 - Validate the 'Is Grated Than Or Equal To' operator")]
        public void WorkflowDetailsSection_TestMethod119()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE
            string subject = "WF Async Testing - Scenario 117";
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



            DateTime PhoneCallDate = new DateTime(2019, 3, 13);

            


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }


        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7594")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 120 - Validate the 'Is Less Than' operator")]
        public void WorkflowDetailsSection_TestMethod120()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE
            string subject = "WF Async Testing - Scenario 120";
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



            DateTime PhoneCallDate = new DateTime(2019, 3, 13);

            


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Async Testing - Scenario 120 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7595")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 121 - Validate the 'Is Less Than' operator")]
        public void WorkflowDetailsSection_TestMethod121()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE
            string subject = "WF Async Testing - Scenario 120";
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



            DateTime PhoneCallDate = new DateTime(2019, 3, 14);

            

            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7596")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 121.1 - Validate the 'Is Less Than' operator")]
        public void WorkflowDetailsSection_TestMethod121_1()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE
            string subject = "WF Async Testing - Scenario 120";
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



            DateTime PhoneCallDate = new DateTime(2019, 3, 15);

            
            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }



        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7597")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 122 - Validate the 'Is Less Than Or Equal To' operator")]
        public void WorkflowDetailsSection_TestMethod122()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE
            string subject = "WF Async Testing - Scenario 122";
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



            DateTime PhoneCallDate = new DateTime(2019, 3, 13);

            


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Async Testing - Scenario 122 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7598")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 123 - Validate the 'Is Less Than Or Equal To' operator")]
        public void WorkflowDetailsSection_TestMethod123()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE
            string subject = "WF Async Testing - Scenario 122";
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



            DateTime PhoneCallDate = new DateTime(2019, 3, 14);

            

            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Async Testing - Scenario 122 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7599")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 124 - Validate the 'Is Less Than Or Equal To' operator")]
        public void WorkflowDetailsSection_TestMethod124()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE
            string subject = "WF Async Testing - Scenario 122";
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



            DateTime PhoneCallDate = new DateTime(2019, 3, 15);

            

            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }



        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7600")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 125 - Validate the 'In Past' operator")]
        public void WorkflowDetailsSection_TestMethod125()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE
            string subject = "WF Async Testing - Scenario 125";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Async Testing - Scenario 125 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7601")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 126 - Validate the 'In Past' operator")]
        public void WorkflowDetailsSection_TestMethod126()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE
            string subject = "WF Async Testing - Scenario 125";
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



            DateTime PhoneCallDate = DateTime.Now.AddHours(2);

            


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }



        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7602")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 127 - Validate the 'Is Grated Than Today's Date' operator")]
        public void WorkflowDetailsSection_TestMethod127()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE
            string subject = "WF Async Testing - Scenario 127";
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


            var baseDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime PhoneCallDate = baseDate.AddDays(1).Date;

            


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Async Testing - Scenario 127 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7603")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 128 - Validate the 'Is Grated Than Today's Date' operator")]
        public void WorkflowDetailsSection_TestMethod128()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE
            string subject = "WF Async Testing - Scenario 127";
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



            DateTime PhoneCallDate = DateTime.Now.Date;

            


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7604")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 128.1 - Validate the 'Is Grated Than Today's Date' operator")]
        public void WorkflowDetailsSection_TestMethod128_1()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE
            string subject = "WF Async Testing - Scenario 127";
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



            DateTime PhoneCallDate = DateTime.Now.AddDays(-1);

            


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }



        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7605")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 129 - Validate the 'Is Less Than Today's Date' operator")]
        public void WorkflowDetailsSection_TestMethod129()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE
            string subject = "WF Async Testing - Scenario 129";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Async Testing - Scenario 129 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7606")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 130 - Validate the 'Is Less Than Today's Date' operator")]
        public void WorkflowDetailsSection_TestMethod130()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE
            string subject = "WF Async Testing - Scenario 129";
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



            DateTime PhoneCallDate = DateTime.Now;

            

            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }



        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7607")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 131 - Validate the 'Last N Days' operator")]
        public void WorkflowDetailsSection_TestMethod131()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE
            string subject = "WF Async Testing - Scenario 131";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Async Testing - Scenario 131 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7608")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 132 - Validate the 'Last N Days' operator")]
        public void WorkflowDetailsSection_TestMethod132()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE
            string subject = "WF Async Testing - Scenario 131";
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



            DateTime PhoneCallDate = DateTime.Now.AddDays(-3).Date;

            


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7609")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 132.1 - Validate the 'Last N Days' operator")]
        public void WorkflowDetailsSection_TestMethod132_1()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE
            string subject = "WF Async Testing - Scenario 131";
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


            var baseDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime PhoneCallDate = baseDate.AddDays(1).Date;

            


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }


        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7610")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 133 - Validate the 'Last N Years' operator")]
        public void WorkflowDetailsSection_TestMethod133()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE
            string subject = "WF Async Testing - Scenario 133";
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



            DateTime PhoneCallDate = DateTime.Now.AddYears(-1).AddMonths(-6).Date;

            


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Async Testing - Scenario 133 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7611")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 134 - Validate the 'Last N Years' operator")]
        public void WorkflowDetailsSection_TestMethod134()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE
            string subject = "WF Async Testing - Scenario 133";
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



            DateTime PhoneCallDate = DateTime.Now.AddYears(-2).AddMonths(-6).Date;

            


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7612")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 134.1 - Validate the 'Last N Years' operator")]
        public void WorkflowDetailsSection_TestMethod134_1()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE
            string subject = "WF Async Testing - Scenario 133";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }



        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7613")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 135 - Validate the 'Next N Months' operator")]
        public void WorkflowDetailsSection_TestMethod135()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE
            string subject = "WF Async Testing - Scenario 135";
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



            DateTime PhoneCallDate = DateTime.Now.AddMonths(2).Date;

            

            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Async Testing - Scenario 135 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7614")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 136 - Validate the 'Next N Months' operator")]
        public void WorkflowDetailsSection_TestMethod136()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE
            string subject = "WF Async Testing - Scenario 135";
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



            DateTime PhoneCallDate = DateTime.Now.AddMonths(4).Date;

            

            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7615")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 136.1 - Validate the 'Next N Months' operator")]
        public void WorkflowDetailsSection_TestMethod136_1()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE
            string subject = "WF Async Testing - Scenario 135";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }









        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7616")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 137 - Validate the 'Next N Years' operator")]
        public void WorkflowDetailsSection_TestMethod137()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 137";
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

            DateTime PhoneCallDate = DateTime.Now.AddYears(1).AddMonths(6).Date;


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Async Testing - Scenario 137 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7617")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 138 - Validate the 'Next N Years' operator")]
        public void WorkflowDetailsSection_TestMethod138()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 137";
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


            DateTime PhoneCallDate = DateTime.Now.AddYears(2).AddMonths(1).Date;


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7618")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 138.1 - Validate the 'Next N Years' operator")]
        public void WorkflowDetailsSection_TestMethod138_1()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 137";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }



        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7619")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 139 - Validate the 'Older Than N Days' operator")]
        public void WorkflowDetailsSection_TestMethod139()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 139";
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

            DateTime PhoneCallDate = DateTime.Now.AddDays(-3);



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Async Testing - Scenario 139 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7620")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 140 - Validate the 'Older Than N Days' operator")]
        public void WorkflowDetailsSection_TestMethod140()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 139";
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

            DateTime PhoneCallDate = DateTime.Now.AddDays(-1);



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7621")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 140.1 - Validate the 'Older Than N Days' operator")]
        public void WorkflowDetailsSection_TestMethod140_1()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 139";
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

            DateTime PhoneCallDate = DateTime.Now.AddDays(1);



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }






        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7622")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 141 - Validate the 'Older Than N Months' operator")]
        public void WorkflowDetailsSection_TestMethod141()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 141";
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

            DateTime PhoneCallDate = DateTime.Now.AddMonths(-3);



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Async Testing - Scenario 141 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7623")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 142 - Validate the 'Older Than N Months' operator")]
        public void WorkflowDetailsSection_TestMethod142()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 141";
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

            DateTime PhoneCallDate = DateTime.Now.AddMonths(-1).AddDays(-10);


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7624")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 142.1 - Validate the 'Older Than N Months' operator")]
        public void WorkflowDetailsSection_TestMethod142_1()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 141";
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

            DateTime PhoneCallDate = DateTime.Now.AddDays(1);


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }




        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7625")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 143 - Validate the 'Older Than N Months' operator")]
        public void WorkflowDetailsSection_TestMethod143()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 143";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0997654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Async Testing - Scenario 143 - Action 1 Activated", (string)fields["notes"]);
        }

        /// <summary>
        /// Workflow under test: WF Automated Testing - Testing the operators in conditions
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7626")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 144 - Validate the 'Older Than N Months' operator")]
        public void WorkflowDetailsSection_TestMethod144()
        {
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Testing the Operators in Conditions", "WF Async Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Async Testing - Scenario 143";
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
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }




        /// <summary>
        /// 
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7627")]
        [TestMethod]
        [Description("Automation Script for the Test Case 183 - Testing the use of a Create Action as 'Local Value' ")]
        public void WorkflowDetailsSection_TestMethod183()
        {
            //ARRANGE 
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of complex scenarios", "WF Async Workflow Testing - Validations of complex scenarios.Zip");

            var caseActionTypeId = commonWorkflowMethods.CreateCaseActionTypeIfNeeded(new Guid("2ccc59a4-056c-ed11-83c0-0a1ad2d8e5ec"), "Default", _defaultTeamId, 110000000);

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonWorkflowMethods.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            //Create Social care case for the person
            string AdditionalInformation = "WF Async Testing - Scenario 183";
            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2022, 3, 8), new DateTime(2022, 3, 7), 18, AdditionalInformation, "CMB Scenario 183");


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var caseActions = dbHelper.caseAction.GetCaseActionByCaseID(caseID);
            var caseNotes = dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personId);


            Assert.AreEqual(1, caseActions.Count);
            Assert.AreEqual(1, caseNotes.Count);


            var caseActionFields = dbHelper.caseAction.GetCaseActionByID(caseActions[0], "personid", "ownerid", "duedate", "caseactiontypeid");
            Assert.AreEqual(personId.ToString(), caseActionFields["personid"].ToString());
            Assert.AreEqual(_defaultTeamId.ToString(), caseActionFields["ownerid"].ToString());
            Assert.AreEqual(new DateTime(2022, 3, 7), caseActionFields["duedate"]);
            Assert.AreEqual(caseActionTypeId.ToString(), caseActionFields["caseactiontypeid"].ToString());

            var casenoteFields = dbHelper.personCaseNote.GetPersonCaseNoteByID(caseNotes[0], "subject", "notes");
            Assert.AreEqual("Case Note for Case Action --> Default", (string)casenoteFields["subject"]);
            string expectedDescription = string.Format("<p> <->  <->  <-> </p>");
            Assert.AreEqual(expectedDescription, (string)casenoteFields["notes"]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7628")]
        [TestMethod]
        [Description("Automation Script for the Test Case 184 - Testing the use of a Create Action as 'Local Value' - Create action not executed")]
        public void WorkflowDetailsSection_TestMethod184()
        {
            //ARRANGE 
            //ARRANGE 
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of complex scenarios", "WF Async Workflow Testing - Validations of complex scenarios.Zip");

            var caseActionTypeId = commonWorkflowMethods.CreateCaseActionTypeIfNeeded(new Guid("2ccc59a4-056c-ed11-83c0-0a1ad2d8e5ec"), "Default", _defaultTeamId, 110000000);

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonWorkflowMethods.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            //Create Social care case for the person
            string AdditionalInformation = "WF Async Testing - Scenario 183";
            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2022, 3, 8), new DateTime(2022, 3, 7), 18, AdditionalInformation, "XPTO");




            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);


            var caseActions = dbHelper.caseAction.GetCaseActionByCaseID(caseID);
            var caseNotes = dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personId);


            Assert.AreEqual(0, caseActions.Count);
            Assert.AreEqual(1, caseNotes.Count);


            var casenoteFields = dbHelper.personCaseNote.GetPersonCaseNoteByID(caseNotes[0], "subject", "notes");
            Assert.AreEqual("Case Note for Case Action -->", (string)casenoteFields["subject".ToString()]);
            string expectedDescription = string.Format("<p> <->  <->  <-> </p>");
            Assert.AreEqual(expectedDescription, (string)casenoteFields["notes".ToString()]);
        }




        /// <summary>
        /// 
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7629")]
        [TestMethod]
        [Description("Automation Script for the Test Case 185 - Testing the use of a child record validation in a If statment - Child record contains data")]
        public void WorkflowDetailsSection_TestMethod185()
        {
            //ARRANGE 
            var documentID = commonWorkflowMethods.CreateDocumentIfNeeded("Person Initial Assessment", "Person Initial Assessment.Zip");
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of complex scenarios", "WF Async Workflow Testing - Validations of complex scenarios.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonWorkflowMethods.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            //Create Social care case for the person
            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2022, 3, 8), new DateTime(2022, 3, 7), 18);



            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var formIDs = dbHelper.personForm.GetPersonFormByPersonID(personId);
            var caseNotes = dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personId);

            Assert.AreEqual(0, formIDs.Count);
            Assert.AreEqual(0, caseNotes.Count);



            //ACT

            //Create a Case Action record
            var _caseActionTypeId = commonWorkflowMethods.CreateCaseActionTypeIfNeeded(new Guid("a1dce1ba-5c75-e911-a2c5-005056926fe4"), "Core Assessment", _defaultTeamId, 110000000);
            dbHelper.caseAction.CreateCaseAction(_defaultTeamId, personId, caseID, _caseActionTypeId, DateTime.Now.Date);

            //Update the case additional details
            dbHelper.Case.UpdateAdditionalInformation(caseID, "WF Async Testing - Scenario 185");


            //ASSERT
            newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            formIDs = dbHelper.personForm.GetPersonFormByPersonID(personId);
            caseNotes = dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personId);

            Assert.AreEqual(1, formIDs.Count);
            Assert.AreEqual(0, caseNotes.Count);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7630")]
        [TestMethod]
        [Description("Automation Script for the Test Case 185 - Testing the use of a child record validation in a If statment - Child record does not contain data")]
        public void WorkflowDetailsSection_TestMethod186()
        {
            var documentID = commonWorkflowMethods.CreateDocumentIfNeeded("Person Initial Assessment", "Person Initial Assessment.Zip");
            Guid workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Workflow Testing - Validations of complex scenarios", "WF Async Workflow Testing - Validations of complex scenarios.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonWorkflowMethods.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            //Create Social care case for the person
            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2022, 3, 8), new DateTime(2022, 3, 7), 18);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var formIDs = dbHelper.personForm.GetPersonFormByPersonID(personId);
            var caseNotes = dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personId);

            Assert.AreEqual(0, formIDs.Count);
            Assert.AreEqual(0, caseNotes.Count);



            //ACT

            //Update the case additional details
            dbHelper.Case.UpdateAdditionalInformation(caseID, "WF Async Testing - Scenario 185");


            //ASSERT
            newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            formIDs = dbHelper.personForm.GetPersonFormByPersonID(personId);
            caseNotes = dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personId);

            Assert.AreEqual(0, formIDs.Count);
            Assert.AreEqual(1, caseNotes.Count);
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

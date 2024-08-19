using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareDirector.Sdk.SystemEntities;
using CareDirector.Sdk.SystemEntities.WordAddIn;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phoenix.DBHelper;
using Phoenix.DBHelper.Models;
using Phoenix.WorkflowTestFramework;

namespace Phoenix.WorkflowTests.AWS
{
    [TestClass]
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("Files\\WF Automated Testing - S1.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - S3.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - S5.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - S7.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - S8.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - S10.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - S12.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - S14.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - S16.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - S19.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - S22.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - S24.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - S25.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - S26.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - S27.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - S28.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - S29.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - S30.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - S31.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - S32.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - S33.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - Validations to Business Data Actions.Zip")]
    [DeploymentItem("Files\\Person Employment Date Overlap.Zip")]
    [DeploymentItem("Files\\Test CDV6-7678.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - S38.Zip")]
    [DeploymentItem("Files\\Workflow Test Document 1.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - S40.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - S44.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - Validations of Conditions and Steps.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - Testing the Operators in Conditions.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - Testing the Custom Fields Tool.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - Testing the Custom Fields Tool 2.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - Bugs.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - Bugs 2.Zip")]
    [DeploymentItem("Files\\Person Initial Assessment.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - Bugs 3.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - Set a lookup field to a MultiSelectMultiLookup Tests and Date and Datetime related tests.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - Rich Text Box Updates.Zip")]
    [DeploymentItem("Files\\Automation - Person Form 1.Zip")]
    [DeploymentItem("Files\\WF Person Form Testing - Jira ID CDV6-8028.Zip")]
    [DeploymentItem("Files\\WF Testing Owner in Create Action - Jira ID CDV6-8283.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - CDV6-8527.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - CDV6-9096 - Child WF - UI Test to unpublish.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - CDV6-9096 - Child WF (published).Zip")]
    [DeploymentItem("Files\\WF Automated Testing - CDV6-9096 - Child WF (un-published).Zip")]
    [DeploymentItem("Files\\WF Automated Testing - CDV6-9096 - Parent WF.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - CDV6-9097.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - CDV6-8893.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - CDV6-8893 (Case Form).Zip")]
    [DeploymentItem("Files\\Document CDV6-8893.Zip")]
    [DeploymentItem("Files\\Testing CDV6-15933.Zip")]
    public class WorkflowDetailsSectionTests : BaseTestClass
    {
        #region Private Properties

        private string tenantName;
        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _defaultBusinessUnitId;
        private Guid _defaultTeamId;
        private Guid _ethnicityId;
        private Guid _activityCategoryId;
        private Guid _activitySubCategoryId;
        private Guid _activityOutcomeId;
        private Guid _activityReasonId;
        private Guid _activityPriorityId;

        #endregion


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

            #region Activity Categories

            if (!dbHelper.activityCategory.GetByName("Advice").Any())
                dbHelper.activityCategory.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _defaultTeamId);
            _activityCategoryId = dbHelper.activityCategory.GetByName("Advice")[0];

            #endregion

            #region Activity Sub Categories

            if (!dbHelper.activitySubCategory.GetByName("Home Support").Any())
                dbHelper.activitySubCategory.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId, _defaultTeamId);
            _activitySubCategoryId = dbHelper.activitySubCategory.GetByName("Home Support")[0];

            #endregion

            #region Activity Outcome

            if (!dbHelper.activityOutcome.GetByName("More information needed").Any())
                dbHelper.activityOutcome.CreateActivityOutcome("More information needed", new DateTime(2020, 1, 1), _defaultTeamId);
            _activityOutcomeId = dbHelper.activityOutcome.GetByName("More information needed")[0];

            #endregion

            #region Activity Reason

            if (!dbHelper.activityReason.GetByName("Assessment").Any())
                dbHelper.activityReason.CreateActivityReason("Assessment", new DateTime(2020, 1, 1), _defaultTeamId);
            _activityReasonId = dbHelper.activityReason.GetByName("Assessment")[0];

            #endregion

            #region Activity Priority

            if (!dbHelper.activityPriority.GetByName("Normal").Any())
                dbHelper.activityPriority.CreateActivityPriority("Normal", new DateTime(2020, 1, 1), _defaultTeamId);
            _activityPriorityId = dbHelper.activityPriority.GetByName("Normal")[0];

            #endregion
        }


        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7631")]
        [Description("Automation Script for the Test Case - Scenario 1 - User creating the record do not belongs to the team that owns the workflow")]

        public void WorkflowDetailsSection_TestMethod001()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S1", "WF Automated Testing - S1.Zip");

            var teamId = commonWorkflowMethods.CreateTeam("UK Local Authority Social Care Team 2", _defaultBusinessUnitId);

            commonWorkflowMethods.CreateSystemUserRecord("Workflow_Test_User_1", "Workflow", "Test_User_1", "Passw0rd_!", _defaultBusinessUnitId, teamId, _languageId, _authenticationproviderid);

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testinng - Scenario 1";
            string description = "Sample Description ...";

            Guid callerID = personId;// ;
            string callerIdTableName = "person";
            string callerIDName = personFullName;// ;

            Guid recipientID = userid;// ;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId; //;
            string regardingName = personFullName;// ;

            //

            //ACT

            //login with a user that do not belongs to Caredirector team
            dbHelper = new DBHelper.DatabaseHelper("Workflow_Test_User_1", "Passw0rd_!");

            //create the phone call record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            bool inactive = (bool)(dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "inactive")["inactive"]);
            Assert.IsFalse(inactive);

        }

        [TestProperty("JiraIssueID", "CDV6-7632")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 2 - User creating the record belongs to the team that owns the workflow")]
        public void WorkflowDetailsSection_TestMethod002()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S1", "WF Automated Testing - S1.Zip", _defaultTeamId);

            commonWorkflowMethods.CreateSystemUserRecord("Workflow_Test_User_2", "Workflow", "Test_User_2", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid); //this user belongs to team 1

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testinng - Scenario 1";
            string description = "Sample Description ...";

            Guid callerID = personId;// ;
            string callerIdTableName = "person";
            string callerIDName = personFullName;// ;

            Guid recipientID = userid;// ;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId; //;
            string regardingName = personFullName; // ;

            //

            //login with a user that do not belongs to Caredirector team
            dbHelper = new DBHelper.DatabaseHelper("Workflow_Test_User_2", "Passw0rd_!");


            //ACT

            //create the phone call record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            bool inactive = (bool)(dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "inactive")["inactive"]);
            Assert.IsTrue(inactive);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7633")]
        [Description("Automation Script for the Test Case - Scenario 3 - User creating the record do not belongs to the Business Unit that owns the workflow")]
        public void WorkflowDetailsSection_TestMethod003()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S3", "WF Automated Testing - S3.Zip");

            var businessUnitId = commonWorkflowMethods.CreateBusinessUnit("UK Local Authority Social Care BU2", null);
            var teamId = commonWorkflowMethods.CreateTeam("UK Local Authority Social Care Team 3", businessUnitId);

            commonWorkflowMethods.CreateSystemUserRecord("Workflow_Test_User_3", "Workflow", "Test_User_1", "Passw0rd_!", businessUnitId, teamId, _languageId, _authenticationproviderid);

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testinng - Scenario 3";
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

            //login with a user that do not belongs to Caredirector QA Business Unit
            dbHelper = new DBHelper.DatabaseHelper("Workflow_Test_User_3", "Passw0rd_!");

            //create the phone call record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            bool inactive = (bool)(dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "inactive")["inactive"]);
            Assert.IsFalse(inactive);

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7634")]
        [Description("Automation Script for the Test Case - Scenario 4 - User creating the record belongs to the Business Unit that owns the workflow")]
        public void WorkflowDetailsSection_TestMethod004()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S3", "WF Automated Testing - S3.Zip", _defaultTeamId);

            var teamId = commonWorkflowMethods.CreateTeam("UK Local Authority Social Care Team 2", _defaultBusinessUnitId);

            commonWorkflowMethods.CreateSystemUserRecord("Workflow_Test_User_1", "Workflow", "Test_User_1", "Passw0rd_!", _defaultBusinessUnitId, teamId, _languageId, _authenticationproviderid);

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testinng - Scenario 3";
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

            //login with a user that belong to the same Business Unit
            dbHelper = new DBHelper.DatabaseHelper("Workflow_Test_User_1", "Passw0rd_!");

            //ACT

            //create the phone call record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            bool inactive = (bool)(dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "inactive")["inactive"]);
            Assert.IsTrue(inactive);

        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7635")]
        [Description("Automation Script for the Test Case - Scenario 5 - User creating the record do not belongs to a Child Business Unit that owns the workflow")]
        public void WorkflowDetailsSection_TestMethod005()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S5", "WF Automated Testing - S5.Zip");

            var businessUnitId = commonWorkflowMethods.CreateBusinessUnit("UK Local Authority Social Care BU2", null);
            var teamId = commonWorkflowMethods.CreateTeam("UK Local Authority Social Care Team 3", businessUnitId);

            commonWorkflowMethods.CreateSystemUserRecord("Workflow_Test_User_3", "Workflow", "Test_User_3", "Passw0rd_!", businessUnitId, teamId, _languageId, _authenticationproviderid);

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testinng - Scenario 5";
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


            //login with a user that belong to the same Business Unit
            dbHelper = new DBHelper.DatabaseHelper("Workflow_Test_User_3", "Passw0rd_!");


            //ACT

            //create the phone call record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            bool inactive = (bool)(dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "inactive")["inactive"]);
            Assert.IsFalse(inactive);

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7636")]
        [Description("Automation Script for the Test Case - Scenario 6 - User creating the record belongs to a Child Business Unit that owns the workflow")]
        public void WorkflowDetailsSection_TestMethod006()
        {
            commonWorkflowMethods.CreateSystemUserRecord("Workflow_Test_ParentUser_4", "Workflow", "Test_ParentUser_4", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            //login with a user that belong to the default BU and Team
            dbHelper = new DBHelper.DatabaseHelper("Workflow_Test_ParentUser_4", "Passw0rd_!");
            commonWorkflowMethods = new CommonWorkflowMethods(dbHelper, fileIOHelper, TestContext);

            //creator of the workflow will be "Workflow_Test_ParentUser_4" and his team will be the responsible team for the workflow
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S5", "WF Automated Testing - S5.Zip");


            var businessUnitId = commonWorkflowMethods.CreateBusinessUnit("UK Local Authority Social Care BU3", _defaultBusinessUnitId); //Child BU 
            var teamId = commonWorkflowMethods.CreateTeam("UK Local Authority Social Care Team 4", businessUnitId);

            commonWorkflowMethods.CreateSystemUserRecord("Workflow_Test_User_4", "Workflow", "Test_User_4", "Passw0rd_!", businessUnitId, teamId, _languageId, _authenticationproviderid);

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Testinng - Scenario 5";
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


            //login with a user that belong to the same Business Unit
            dbHelper = new DBHelper.DatabaseHelper("Workflow_Test_User_4", "Passw0rd_!");
            commonWorkflowMethods = new CommonWorkflowMethods(dbHelper, fileIOHelper, TestContext);

            //ACT

            //create the phone call record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            bool inactive = (bool)(dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "inactive")["inactive"]);
            Assert.IsTrue(inactive);

        }


        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7637")]
        [Description("Automation Script for the Test Case - Scenario 7 - WF scope is set to Organization - User creating the record do not belongs to the BU (or child BU) that owns the workflow")]
        public void WorkflowDetailsSection_TestMethod007()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S7", "WF Automated Testing - S7.Zip");

            var businessUnitId = commonWorkflowMethods.CreateBusinessUnit("UK Local Authority Social Care BU2", null);
            var teamId = commonWorkflowMethods.CreateTeam("UK Local Authority Social Care Team 3", businessUnitId);

            commonWorkflowMethods.CreateSystemUserRecord("Workflow_Test_User_3", "Workflow", "Test_User_1", "Passw0rd_!", businessUnitId, teamId, _languageId, _authenticationproviderid);

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testinng - Scenario 7";
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

            //user Workflow_Test_User_3 belongs to a different BU
            dbHelper = new DBHelper.DatabaseHelper("Workflow_Test_User_3", "Passw0rd_!");

            //ACT

            //create the phone call record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            bool inactive = (bool)(dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "inactive")["inactive"]);
            Assert.IsTrue(inactive);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7638")]
        [Description("Automation Script for the Test Case - Scenario 8 - Create a record when the WF Start options are all set to No")]
        public void WorkflowDetailsSection_TestMethod008()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S8", "WF Automated Testing - S8.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testinng - Scenario 8";
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

            //create the phone call record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            string descriptionAfterSave = (string)(dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "notes")["notes"]);
            Assert.AreEqual(description, descriptionAfterSave);

        }


        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7639")]
        [Description("Automation Script for the Test Case - Scenario 9 - Update a record when the WF Start options are all set to No")]
        public void WorkflowDetailsSection_TestMethod009()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S8", "WF Automated Testing - S8.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testinng - Scenario 8";
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


            //create the phone call record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);

            //ACT
            dbHelper.phoneCall.UpdatePhoneCallRecordDescription(phoneCallID, "Description field update ...");


            //ASSERT
            string descriptionAfterSave = (string)(dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "notes")["notes"]);
            Assert.AreEqual("Description field update ...", descriptionAfterSave);

        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7640")]
        [Description("Automation Script for the Test Case - Scenario 10 - Create a record when the only the WF 'Record Is Created' is set to Yes")]
        public void WorkflowDetailsSection_TestMethod010()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S10", "WF Automated Testing - S10.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testinng - Scenario 10";
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

            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            string descriptionAfterSave = (string)(dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "notes")["notes"]);
            Assert.AreEqual("<p>WF Testinng - Scenario 10 - Action 1 Activated</p>", descriptionAfterSave);

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7641")]
        [Description("Automation Script for the Test Case - Scenario 11 - Update a record when the only the WF 'Record Is Created' is set to Yes")]
        public void WorkflowDetailsSection_TestMethod011()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S10", "WF Automated Testing - S10.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testinng - Scenario 10";
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


            //create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ACT
            //update the record
            dbHelper.phoneCall.UpdatePhoneCallRecordDescription(phoneCallID, "WF Testinng - Scenario 10 - Update");


            //ASSERT
            string descriptionAfterSave = (string)(dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "notes")["notes"]);
            Assert.AreEqual("WF Testinng - Scenario 10 - Update", descriptionAfterSave);

        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7642")]
        [Description("Automation Script for the Test Case - Scenario 12 - Update a record without updating the record status")]
        public void WorkflowDetailsSection_TestMethod012()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S12", "WF Automated Testing - S12.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testinng - Scenario 12";
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



            //create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);

            //ACT

            //update the record 
            dbHelper.phoneCall.UpdatePhoneCallRecordDescription(phoneCallID, "WF Testinng - Scenario 12 - Update");


            //ASSERT
            string descriptionAfterSave = (string)(dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "notes")["notes"]);
            Assert.AreEqual("WF Testinng - Scenario 12 - Update", descriptionAfterSave);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7643")]
        [Description("Automation Script for the Test Case - Scenario 13 - Update the record statusId to completed and set the record as inactive")]
        public void WorkflowDetailsSection_TestMethod013()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S12", "WF Automated Testing - S12.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testinng - Scenario 12";
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



            //create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);

            //ACT

            //update the record
            dbHelper.phoneCall.UpdatePhoneCallRecordDescription(phoneCallID, "WF Testinng - Scenario 12 - Update");
            dbHelper.phoneCall.UpdateStatus(phoneCallID, 2);


            //ASSERT
            string descriptionAfterSave = (string)(dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "notes")["notes"]);
            Assert.AreEqual("<p>WF Testinng - Scenario 12 - Action 1 Activated</p>", descriptionAfterSave);
        }




        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7644")]
        [Description("Automation Script for the Test Case - Scenario 14 - Update the Phone Call Date field")]
        public void WorkflowDetailsSection_TestMethod014()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S14", "WF Automated Testing - S14.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testinng - Scenario 14";
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


            //create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);

            //ACT

            //update the record 
            dbHelper.phoneCall.UpdatePhoneCallRecordDescription(phoneCallID, "WF Testinng - Scenario 14 - Update");
            dbHelper.phoneCall.UpdatePhoneCallDateField(phoneCallID, DateTime.Now.WithoutMilliseconds().AddHours(-1));


            //ASSERT
            System.Threading.Thread.Sleep(2000);
            string descriptionAfterSave = (string)(dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "notes")["notes"]);
            Assert.AreEqual("<p>WF Testinng - Scenario 14 - Action 1 Activated</p>", descriptionAfterSave);
        }

        [TestProperty("JiraIssueID", "CDV6-7645")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 15 - Update a field different from the Phone Call Date field")]
        public void WorkflowDetailsSection_TestMethod015()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S14", "WF Automated Testing - S14.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testinng - Scenario 14";
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


            //create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);

            //ACT

            //update the record 
            dbHelper.phoneCall.UpdatePhoneCallRecordDescription(phoneCallID, "WF Testinng - Scenario 14 - Update");


            //ASSERT
            string descriptionAfterSave = (string)(dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "notes")["notes"]);
            Assert.AreEqual("WF Testinng - Scenario 14 - Update", descriptionAfterSave);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7646")]
        [Description("Automation Script for the Test Case - Scenario 16 - Update a field other than the responsible team")]

        public void WorkflowDetailsSection_TestMethod016()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S16", "WF Automated Testing - S16.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testinng - Scenario 16";
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


            //create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);

            //ACT

            //update the record 
            dbHelper.phoneCall.UpdatePhoneCallRecordDescription(phoneCallID, "WF Testinng - Scenario 16 - Update");


            //ASSERT
            System.Threading.Thread.Sleep(2000);
            string descriptionAfterSave = (string)(dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "notes")["notes"]);
            Assert.AreEqual("WF Testinng - Scenario 16 - Update", descriptionAfterSave);
        }

        //[TestMethod]
        //[TestProperty("JiraIssueID", "CDV6-7647")]
        //[Description("Automation Script for the Test Case - Scenario 17 - Update the Responsible Team")]
        //public void WorkflowDetailsSection_TestMethod017()
        //{
        //    commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S16", "WF Automated Testing - S16.Zip");

        //    var team2Id = commonWorkflowMethods.CreateTeam("UK Local Authority Social Care Team 2", _defaultBusinessUnitId);

        //    var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

        //    var firstName = "Jhon";
        //    var middleName = "WF Test";
        //    var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
        //    var personFullName = firstName + " " + lastName;
        //    var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



        //    //ARRANGE 
        //    string subject = "WF Testinng - Scenario 16";
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

        //    //update the record 
        //    dbHelper.phoneCall.UpdatePhoneCallRecordDescription(phoneCallID, "WF Testinng - Scenario 16 - Update");
        //    dbHelper.phoneCall.AssignPhoneCallRecords(phoneCallID, team2Id, _defaultBusinessUnitId, null);


        //    //ASSERT
        //    string descriptionAfterSave = (string)(dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "notes")["notes"]);
        //    Assert.AreEqual("<p>WF Testinng - Scenario 16 - Action 1 Activated</p>", descriptionAfterSave);
        //}

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7648")]
        [Description("Automation Script for the Test Case - Scenario 17.1 - Workflow should NOT be triggered by the create event")]
        public void WorkflowDetailsSection_TestMethod017_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S16", "WF Automated Testing - S16.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testinng - Scenario 16";
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
            string descriptionAfterSave = (string)(dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "notes")["notes"]);
            Assert.AreEqual("Sample Description ...", descriptionAfterSave);
        }

        //[TestMethod]
        //[TestProperty("JiraIssueID", "CDV6-7649")]
        //[Description("Automation Script for the Test Case - Scenario 18 - Update the Responsible User")]
        //public void WorkflowDetailsSection_TestMethod018()
        //{
        //    commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S16", "WF Automated Testing - S16.Zip");

        //    var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

        //    var firstName = "Jhon";
        //    var middleName = "WF Test";
        //    var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
        //    var personFullName = firstName + " " + lastName;
        //    var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


        //    //ARRANGE 
        //    string subject = "WF Testinng - Scenario 16";
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

        //    //update the record 
        //    dbHelper.phoneCall.UpdatePhoneCallRecordDescription(phoneCallID, "WF Testinng - Scenario 16 - Update");
        //    dbHelper.phoneCall.AssignPhoneCallRecords(phoneCallID, _defaultTeamId, _defaultBusinessUnitId, userid);


        //    //ASSERT
        //    string descriptionAfterSave = (string)(dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "notes")["notes"]);
        //    Assert.AreEqual("<p>WF Testinng - Scenario 16 - Action 1 Activated</p>", descriptionAfterSave);
        //}



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7650")]
        [Description("Automation Script for the Test Case - Scenario 19 - Creating a new record should not trigger the WF")]
        public void WorkflowDetailsSection_TestMethod019()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S19", "WF Automated Testing - S19.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testinng - Scenario 19";
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

            //get all phone call records for the person
            var phoneCallIDs = dbHelper.phoneCall.GetPhoneCallByPersonID(regardingID);
            Assert.AreEqual(1, phoneCallIDs.Count);
            Assert.AreEqual(phoneCallID, phoneCallIDs[0]);

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7651")]
        [Description("Automation Script for the Test Case - Scenario 20 - Updating a record should not trigger the WF")]
        public void WorkflowDetailsSection_TestMethod020()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S19", "WF Automated Testing - S19.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testinng - Scenario 19";
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


            //create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ACT

            dbHelper.phoneCall.UpdatePhoneCallRecordDescription(phoneCallID, "WF Testinng - Scenario 19 - Update");


            //ASSERT

            //get all phone call records for the person
            System.Threading.Thread.Sleep(3000);
            var phoneCallIDs = dbHelper.phoneCall.GetPhoneCallByPersonID(regardingID);
            Assert.AreEqual(1, phoneCallIDs.Count);
            Assert.AreEqual(phoneCallID, phoneCallIDs[0]);

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7652")]
        [Description("Automation Script for the Test Case - Scenario 21 - Deleting a record should not trigger the WF")]
        public void WorkflowDetailsSection_TestMethod021()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S19", "WF Automated Testing - S19.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testinng - Scenario 19";
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


            //create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ACT

            dbHelper.phoneCall.DeletePhoneCall(phoneCallID);


            //ASSERT

            //get all phone call records for the person
            System.Threading.Thread.Sleep(3000);
            var phoneCallIDs = dbHelper.phoneCall.GetPhoneCallByPersonID(regardingID);
            Assert.AreEqual(1, phoneCallIDs.Count);
            Assert.AreNotEqual(phoneCallID, phoneCallIDs[0]);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallIDs[0], "subject", "notes");
            string phoneCallSubject = (string)fields["subject"];
            string phoneCallDescription = (string)fields["notes"];

            Assert.AreEqual("Phone Call record generated by WF Automated Testing - S19", phoneCallSubject);
            Assert.AreEqual("<p>WF Testinng - Scenario 19 - Action 1 Activated</p>", phoneCallDescription);
        }




        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7654")]
        [Description("Automation Script for the Test Case - Scenario 22 - WF triggering another WF")]
        public void WorkflowDetailsSection_TestMethod022()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S22", "WF Automated Testing - S22.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testinng - Scenario 22";
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

            DateTime PhoneCallDate = new DateTime(2019, 3, 2);


            //ACT

            //create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT

            string phoneCallDescription = (string)(dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "notes")["notes"]);
            Assert.AreEqual("<p>WF Testinng - Scenario 22 - Action 1 Activated</p>", phoneCallDescription);

        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7656")]
        [Description("Automation Script for the Test Case - Scenario 24 - Automatically delete completed Jobs")]
        public void WorkflowDetailsSection_TestMethod024()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S24", "WF Automated Testing - S24.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testinng - Scenario 24";
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

            string phoneCallDescription = (string)(dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "notes")["notes"]);
            Assert.AreEqual("<p>WF Testinng - Scenario 24 - Action 1 Activated</p>", phoneCallDescription);

            List<Guid> workflowIds = dbHelper.workflow.GetWorkflowByName("WF Automated Testing - S24");
            if (workflowIds.Count > 1)
                Assert.Fail("We have more than 1 workflow with the same name");

            int totalJobsForWF = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowIds[0]).Count;
            Assert.AreEqual(0, totalJobsForWF);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7657")]
        [Description("Automation Script for the Test Case - Scenario 25 - Pre-Sync WF will display a message")]
        public void WorkflowDetailsSection_TestMethod025()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S25", "WF Automated Testing - S25.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testinng - Scenario 25";
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
            try
            {
                Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);
            }
            catch (Exception ex)
            {
                //ASSERT
                Assert.AreEqual("'WF Automated Testing - S25': WF Testinng - Scenario 25 - Action 1 Activated", ex.Message);
                return;
            }


            Assert.Fail("An error should have been triggered by the Workflow 'WF Automated Testing - S25' ");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7658")]
        [Description("Automation Script for the Test Case - Scenario 25.1 - Create a record with a different subject and validate that the WF is not triggered")]
        public void WorkflowDetailsSection_TestMethod025_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S25", "WF Automated Testing - S25.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testinng - Scenario 25.1";
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


            //create the record (if an exception is triggered by the WF the test will fail)
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);

        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7659")]
        [Description("Automation Script for the Test Case - Scenario 26 - Pre-Sync WF with Stop Workflow action")]
        public void WorkflowDetailsSection_TestMethod026()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S26", "WF Automated Testing - S26.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testinng - Scenario 26";
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


            //create the record (if an exception is triggered by the WF the test will fail)
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT

        }


        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7660")]
        [Description("Automation Script for the Test Case - Scenario 27 - Test create record action")]
        public void WorkflowDetailsSection_TestMethod027()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S27", "WF Automated Testing - S27.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testinng - Scenario 27";
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

            //get all phone call records for the person
            var phoneCallIDs = dbHelper.phoneCall.GetPhoneCallByPersonID(regardingID);
            Assert.AreEqual(2, phoneCallIDs.Count);

            //validate the description field of the record created by the WF
            Guid newPhoneCallRecord = phoneCallIDs.FirstOrDefault(c => c != phoneCallID);
            string recordSubjectField = (string)(dbHelper.phoneCall.GetPhoneCallByID(newPhoneCallRecord, "subject")["subject"]);
            Assert.AreEqual("WF Testinng - Scenario 27 - Action 1 Activated", recordSubjectField);
        }


        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7661")]
        [Description("Automation Script for the Test Case - Scenario 28 - Test update record action")]
        public void WorkflowDetailsSection_TestMethod028()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S28", "WF Automated Testing - S28.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testinng - Scenario 28";
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
            var phoneCallFields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID
                , "CallerId", "CallerIdTableName", "CallerIdName", "PhoneNumber", "DirectionId", "subject", "PhoneCallDate");

            Assert.AreEqual(recipientID, new Guid(((phoneCallFields["CallerId".ToLower()]).ToString())));
            Assert.AreEqual(recipientIdTableName, (string)phoneCallFields["CallerIdTableName".ToLower()]);
            Assert.AreEqual(recipientIDName, (string)phoneCallFields["CallerIdName".ToLower()]);
            Assert.AreEqual("987654321", (string)phoneCallFields["PhoneNumber".ToLower()]);
            Assert.AreEqual(2, (int)phoneCallFields["DirectionId".ToLower()]);
            Assert.AreEqual("WF Testinng - Scenario 28 - Action 1 Activated", (string)phoneCallFields["subject".ToLower()]);
            Assert.AreEqual(new DateTime(2019, 3, 1, 9, 0, 0), (DateTime)phoneCallFields["PhoneCallDate".ToLower()]);

        }


        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7662")]
        [Description("Automation Script for the Test Case - Scenario 29 - Test Assign record action")]
        public void WorkflowDetailsSection_TestMethod029()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S29", "WF Automated Testing - S29.Zip");

            var newTeamId = commonWorkflowMethods.CreateTeam("Team CDV6-7662", _defaultBusinessUnitId);

            var userid = commonWorkflowMethods.CreateSystemUserRecord("User_CDV6_7662", "User", "CDV6-7662", "Passw0rd_!", _defaultBusinessUnitId, newTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testinng - Scenario 29";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "User CDV6-7662";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;


            //ACT


            //create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var phoneCallFields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "OwnerId", "OwningBusinessUnitId");

            Assert.AreEqual(newTeamId.ToString(), (phoneCallFields["OwnerId".ToLower()]).ToString());
            Assert.AreEqual(_defaultBusinessUnitId.ToString(), (phoneCallFields["OwningBusinessUnitId".ToLower()]).ToString());

        }


        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7663")]
        [Description("Automation Script for the Test Case - Scenario 30 - Test Send Email record action")]
        public void WorkflowDetailsSection_TestMethod030()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S30", "WF Automated Testing - S30.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var user2id = commonWorkflowMethods.CreateSystemUserRecord("Test_CDV6_7663", "Test", "CDV6-7663", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Testing - Scenario 30";
            string description = "Sample Description ...";

            Guid callerID = user2id;
            string callerIdTableName = "systemuser";
            string callerIDName = "Test CDV6-7663";

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            var phoneCallDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            Guid regardingID = personId;
            string regardingName = personFullName;


            //ACT


            //create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, userid, phoneCallDate,
                _activityReasonId, _activityPriorityId, _activityCategoryId, _activitySubCategoryId, _activityOutcomeId);


            //ASSERT

            //get all Email records for the person
            var emailIDs = dbHelper.email.GetEmailByRegardingID(regardingID);
            Assert.AreEqual(1, emailIDs.Count);

            //get the email info
            var email = dbHelper.email.GetEmailByID(emailIDs[0],
                "Subject", "RegardingID", "RegardingIDName", "RegardingIdTableName", "DueDate", "EmailFromLookupId", "Notes",
                "ActivityReasonId", "ActivityOutcomeId", "ActivityCategoryId", "ActivitySubCategoryId", "PersonId", "ResponsibleUserId");

            Assert.AreEqual("WF Testinng - Scenario 30 - Action 1 Activated", (string)email["subject".ToLower()]);
            Assert.AreEqual(regardingID.ToString(), email["RegardingID".ToLower()].ToString());
            Assert.AreEqual(regardingName, (string)email["RegardingIDName".ToLower()]);
            Assert.AreEqual("person", (string)email["RegardingIdTableName".ToLower()]);
            Assert.AreEqual(new DateTime(2019, 03, 01, 9, 0, 0), (DateTime)email["DueDate".ToLower()]);
            //Assert.AreEqual(callerID.ToString(), email["EmailFromLookupId".ToLower()].ToString());
            Assert.AreEqual("<p>Mail Description ...</p>", (string)email["notes".ToLower()]);
            Assert.AreEqual(_activityReasonId.ToString(), email["ActivityReasonId".ToLower()].ToString());
            Assert.AreEqual(_activityOutcomeId.ToString(), email["ActivityOutcomeId".ToLower()].ToString());
            Assert.AreEqual(_activityCategoryId.ToString(), email["ActivityCategoryId".ToLower()].ToString());
            Assert.AreEqual(_activitySubCategoryId.ToString(), email["ActivitySubCategoryId".ToLower()].ToString());
            Assert.AreEqual(regardingID.ToString(), email["PersonId".ToLower()].ToString());
            Assert.AreEqual(userid.ToString(), email["ResponsibleUserId".ToLower()].ToString());

        }


        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7664")]
        [Description("Automation Script for the Test Case - Scenario 31 - Test Start Workflow action")]
        public void WorkflowDetailsSection_TestMethod031()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S31", "WF Automated Testing - S31.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 31";
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

            //get all Email records for the person
            var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "subject");
            Assert.AreEqual("WF Testing - Scenario 31 - Action 1 Activated", (string)phoneCallfields["subject".ToLower()]);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7665")]
        [Description("Automation Script for the Test Case - Scenario 32 - Test Change Record Status action")]
        public void WorkflowDetailsSection_TestMethod032()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S32", "WF Automated Testing - S32.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 32";
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

            //get all Email records for the person
            var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "Inactive");
            Assert.AreEqual(true, (bool)phoneCallfields["inactive".ToLower()]);
        }


        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7666")]
        [Description("Automation Script for the Test Case - Scenario 33 - Test Stop Workflow action")]
        public void WorkflowDetailsSection_TestMethod033()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S33", "WF Automated Testing - S33.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Testing - Scenario 33";
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

            //get all Email records for the person
            var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "subject");
            Assert.AreEqual("WF Testing - Scenario 33", (string)phoneCallfields["subject".ToLower()]);
        }



        //[TestMethod]
        //[TestProperty("JiraIssueID", "CDV6-7667")]
        //[Description("Automation Script for the Test Case - Scenario 34 - Test Apply Data Restriction action")]
        //public void WorkflowDetailsSection_TestMethod034()
        //{
        //    commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations to Business Data Actions", "WF Automated Testing - Validations to Business Data Actions.Zip");

        //    var dataRestrictionID = commonWorkflowMethods.CreateDataRestrictionRecord("Test_DR_CDV6_7666", 1, _defaultTeamId);

        //    var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);
        //    commonWorkflowMethods.CreateUserRestrictedDataAccess(dataRestrictionID, userid, DateTime.Now.Date, _defaultTeamId);

        //    var firstName = "Jhon";
        //    var middleName = "WF Test";
        //    var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
        //    var personFullName = firstName + " " + lastName;
        //    var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);
        //    dbHelper.person.RestrictPerson(personId, dataRestrictionID);

        //    //ARRANGE 
        //    string subject = "WF Testing - Scenario 34";
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


        //    //ACT

        //    //create the record
        //    Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


        //    //ASSERT
        //    dbHelper = new DBHelper.DatabaseHelper("Jose.brazeta", "Passw0rd_!");
        //    var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "datarestrictionid");
        //    Assert.AreEqual(dataRestrictionID.ToString(), phoneCallfields["DataRestrictionId".ToLower()].ToString());
        //}

        //[TestMethod]
        //[TestProperty("JiraIssueID", "CDV6-7668")]
        //[Description("Automation Script for the Test Case - Scenario 34 - Test Apply Data Restriction action (WF action should override existing data restrictions)")]
        //public void WorkflowDetailsSection_TestMethod034_1()
        //{
        //    commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations to Business Data Actions", "WF Automated Testing - Validations to Business Data Actions.Zip");

        //    var dataRestriction1ID = commonWorkflowMethods.CreateDataRestrictionRecord("Test_DR_CDV6_7668_1", 1, _defaultTeamId);
        //    var dataRestriction2ID = commonWorkflowMethods.CreateDataRestrictionRecord("Test_DR_CDV6_7668_2", 1, _defaultTeamId);

        //    var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);
        //    commonWorkflowMethods.CreateUserRestrictedDataAccess(dataRestriction1ID, userid, DateTime.Now.Date, _defaultTeamId);
        //    commonWorkflowMethods.CreateUserRestrictedDataAccess(dataRestriction2ID, userid, DateTime.Now.Date, _defaultTeamId);

        //    var firstName = "Jhon";
        //    var middleName = "WF Test";
        //    var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
        //    var personFullName = firstName + " " + lastName;
        //    var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);
        //    dbHelper.person.RestrictPerson(personId, dataRestriction1ID);

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
        //    dbHelper.phoneCall.RestrictPhoneCall(phoneCallID, dataRestriction2ID);
        //    dbHelper.phoneCall.UpdateSubject(phoneCallID, "WF Testing - Scenario 34", regardingID, regardingName, "person");

        //    //ASSERT
        //    dbHelper = new DBHelper.DatabaseHelper("Jose.brazeta", "Passw0rd_!");
        //    var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "DataRestrictionId");
        //    Assert.AreEqual(dataRestriction1ID.ToString(), phoneCallfields["DataRestrictionId".ToLower()].ToString());
        //}

        //[TestMethod]
        //[TestProperty("JiraIssueID", "CDV6-7669")]
        //[Description("Automation Script for the Test Case - Scenario 34.2 - Test Apply Data Restriction action (WF action should not be able to override existing data restrictions)")]
        //public void WorkflowDetailsSection_TestMethod034_2()
        //{
        //    commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations to Business Data Actions", "WF Automated Testing - Validations to Business Data Actions.Zip");

        //    var dataRestriction1ID = commonWorkflowMethods.CreateDataRestrictionRecord("Test_DR_CDV6_7669_1", 1, _defaultTeamId);
        //    var dataRestriction2ID = commonWorkflowMethods.CreateDataRestrictionRecord("Test_DR_CDV6_7669_2", 1, _defaultTeamId);

        //    var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

        //    commonWorkflowMethods.CreateUserRestrictedDataAccess(dataRestriction2ID, userid, DateTime.Now.Date, _defaultTeamId);

        //    dbHelper = new DBHelper.DatabaseHelper("Jose.brazeta", "Passw0rd_!");

        //    var firstName = "Jhon";
        //    var middleName = "WF Test";
        //    var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
        //    var personFullName = firstName + " " + lastName;
        //    var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);
        //    dbHelper.person.RestrictPerson(personId, dataRestriction1ID);

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
        //    dbHelper.phoneCall.RestrictPhoneCall(phoneCallID, dataRestriction2ID);
        //    dbHelper.phoneCall.UpdateSubject(phoneCallID, "WF Testing - Scenario 34.2", regardingID, regardingName, "person");



        //    //ASSERT

        //    var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "DataRestrictionId");
        //    Assert.AreEqual(dataRestriction2ID.ToString(), phoneCallfields["DataRestrictionId".ToLower()].ToString());
        //}

        //[TestMethod]
        //[TestProperty("JiraIssueID", "CDV6-7670")]
        //[Description("Automation Script for the Test Case - Scenario 34.3 - Test Apply Data Restriction action (WF action should NOT take effect if the restriction will restrict the responsible user)")]
        //public void WorkflowDetailsSection_TestMethod034_3()
        //{
        //    commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations to Business Data Actions", "WF Automated Testing - Validations to Business Data Actions.Zip");

        //    var dataRestriction1ID = commonWorkflowMethods.CreateDataRestrictionRecord("Test_DR_CDV6_7670_1", 1, _defaultTeamId);
        //    var dataRestriction2ID = commonWorkflowMethods.CreateDataRestrictionRecord("Test_DR_CDV6_7670_2", 1, _defaultTeamId);

        //    var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

        //    commonWorkflowMethods.CreateUserRestrictedDataAccess(dataRestriction2ID, userid, DateTime.Now.Date, _defaultTeamId);

        //    dbHelper = new DBHelper.DatabaseHelper("Jose.brazeta", "Passw0rd_!");

        //    var firstName = "Jhon";
        //    var middleName = "WF Test";
        //    var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
        //    var personFullName = firstName + " " + lastName;
        //    var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);
        //    dbHelper.person.RestrictPerson(personId, dataRestriction1ID);


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
        //    Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, userid, DateTime.Now.WithoutMilliseconds(), null, null, null, null, null);


        //    //ACT

        //    //Update the record
        //    dbHelper.phoneCall.RestrictPhoneCall(phoneCallID, dataRestriction2ID);
        //    dbHelper.phoneCall.UpdateSubject(phoneCallID, "WF Testing - Scenario 34.3", regardingID, regardingName, "person");



        //    //ASSERT
        //    var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "DataRestrictionId");
        //    Assert.AreEqual(dataRestriction2ID.ToString(), phoneCallfields["DataRestrictionId".ToLower()].ToString());
        //}

        /// <summary>
        /// Workflow under test: WF Automated Testing - Validations to Business Data Actions
        /// </summary>
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7671")]
        [Description("Automation Script for the Test Case - Scenario 35 - Test Business Data Count action")]
        public void WorkflowDetailsSection_TestMethod035()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations to Business Data Actions", "WF Automated Testing - Validations to Business Data Actions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 35";
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
            string phonecall1Description = (string)(dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes")["notes"]);
            string phonecall2Description = (string)(dbHelper.phoneCall.GetPhoneCallByID(phoneCallID2, "notes")["notes"]);

            Assert.AreEqual("Sample Description ...", phonecall1Description);
            Assert.AreEqual("2", phonecall2Description); //if the count operation succeded then the description field should be updated (there is a restricted record that is also being counted)
        }




        /// <summary>
        /// Workflow Under Test: Person Employment Date Overlap
        /// </summary>
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7672")]
        [Description("Automation Script for the Test Case - Scenario 36 - Test Business Data Overlap action (dates will everlap)")]
        public void WorkflowDetailsSection_TestMethod036()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("Person Employment Date Overlap", "Person Employment Date Overlap.Zip");

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string title = "Employment for Adolfo Abbott created by José Brazeta on" + DateTime.Now.WithoutMilliseconds().ToString("dd/MM/yyyy hh:mm:ss");
            string employer = "Workflow Testing - Employer 2";

            Guid EmploymentWeeklyHoursWorkedId = dbHelper.employmentWeeklyHoursWorked.GetByName("30 + hours").FirstOrDefault();
            Guid EmploymentStatusId = dbHelper.employmentStatus.GetByName("Employed").FirstOrDefault();
            Guid? EmploymentTypeId = null;
            Guid? EmploymentReasonLeftId = null;

            DateTime startDate1 = new DateTime(2019, 3, 16);
            DateTime endDate1 = new DateTime(2019, 3, 19);

            DateTime startDate2 = new DateTime(2019, 3, 17);
            DateTime endDate2 = new DateTime(2019, 3, 18);


            //First employment record
            dbHelper.personEmployment.CreatePersonEmployment(title, employer, personId, _defaultTeamId, EmploymentWeeklyHoursWorkedId, EmploymentStatusId, EmploymentTypeId, EmploymentReasonLeftId, startDate1, endDate1);


            //ACT

            //create the record
            try
            {
                dbHelper.personEmployment.CreatePersonEmployment(title, employer, personId, _defaultTeamId, EmploymentWeeklyHoursWorkedId, EmploymentStatusId, EmploymentTypeId, EmploymentReasonLeftId, startDate2, endDate2);

            }
            catch (Exception ex)
            {
                Assert.AreEqual("'Person Employment Date Overlap': An employment record for this person already exists for the period.", ex.Message);
                return;
            }


            //ASSERT
            Assert.Fail("Workflow should throw an exception");
        }

        /// <summary>
        /// Workflow Under Test: Person Employment Date Overlap
        /// </summary>
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7673")]
        [Description("Automation Script for the Test Case - Scenario 36.1 - Test Business Data Overlap action (dates will everlap)")]
        public void WorkflowDetailsSection_TestMethod036_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("Person Employment Date Overlap", "Person Employment Date Overlap.Zip");

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string title = "Employment for Adolfo Abbott created by José Brazeta on" + DateTime.Now.WithoutMilliseconds().ToString("dd/MM/yyyy hh:mm:ss");
            string employer = "Workflow Testing - Employer 2";

            Guid EmploymentWeeklyHoursWorkedId = dbHelper.employmentWeeklyHoursWorked.GetByName("30 + hours").FirstOrDefault();
            Guid EmploymentStatusId = dbHelper.employmentStatus.GetByName("Employed").FirstOrDefault();
            Guid? EmploymentTypeId = null;
            Guid? EmploymentReasonLeftId = null;

            DateTime startDate1 = new DateTime(2019, 3, 16);
            DateTime endDate1 = new DateTime(2019, 3, 19);

            DateTime startDate2 = new DateTime(2019, 3, 15);
            DateTime endDate2 = new DateTime(2019, 3, 16);


            //First employment record
            dbHelper.personEmployment.CreatePersonEmployment(title, employer, personId, _defaultTeamId, EmploymentWeeklyHoursWorkedId, EmploymentStatusId, EmploymentTypeId, EmploymentReasonLeftId, startDate1, endDate1);



            //ACT

            //create the record
            try
            {
                dbHelper.personEmployment.CreatePersonEmployment(title, employer, personId, _defaultTeamId, EmploymentWeeklyHoursWorkedId, EmploymentStatusId, EmploymentTypeId, EmploymentReasonLeftId, startDate2, endDate2);

            }
            catch (Exception ex)
            {
                Assert.AreEqual("'Person Employment Date Overlap': An employment record for this person already exists for the period.", ex.Message);
                return;
            }


            //ASSERT
            Assert.Fail("Workflow should throw an exception");
        }

        /// <summary>
        /// Workflow Under Test: Person Employment Date Overlap
        /// </summary>
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7674")]
        [Description("Automation Script for the Test Case - Scenario 36.2 - Test Business Data Overlap action (dates will everlap)")]
        public void WorkflowDetailsSection_TestMethod036_2()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("Person Employment Date Overlap", "Person Employment Date Overlap.Zip");

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string title = "Employment for Adolfo Abbott created by José Brazeta on" + DateTime.Now.WithoutMilliseconds().ToString("dd/MM/yyyy hh:mm:ss");
            string employer = "Workflow Testing - Employer 2";

            Guid EmploymentWeeklyHoursWorkedId = dbHelper.employmentWeeklyHoursWorked.GetByName("30 + hours").FirstOrDefault();
            Guid EmploymentStatusId = dbHelper.employmentStatus.GetByName("Employed").FirstOrDefault();
            Guid? EmploymentTypeId = null;
            Guid? EmploymentReasonLeftId = null;

            DateTime startDate1 = new DateTime(2019, 3, 16);
            DateTime endDate1 = new DateTime(2019, 3, 19);

            DateTime startDate2 = new DateTime(2019, 3, 19);
            DateTime endDate2 = new DateTime(2019, 3, 20);


            //First employment record
            dbHelper.personEmployment.CreatePersonEmployment(title, employer, personId, _defaultTeamId, EmploymentWeeklyHoursWorkedId, EmploymentStatusId, EmploymentTypeId, EmploymentReasonLeftId, startDate1, endDate1);


            //ACT

            //create the record
            try
            {
                dbHelper.personEmployment.CreatePersonEmployment(title, employer, personId, _defaultTeamId, EmploymentWeeklyHoursWorkedId, EmploymentStatusId, EmploymentTypeId, EmploymentReasonLeftId, startDate2, endDate2);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("'Person Employment Date Overlap': An employment record for this person already exists for the period.", ex.Message);
                return;
            }


            //ASSERT
            Assert.Fail("Workflow should throw an exception");
        }

        /// <summary>
        /// Workflow Under Test: Person Employment Date Overlap
        /// </summary>
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7675")]
        [Description("Automation Script for the Test Case - Scenario 36.3 - Test Business Data Overlap action (dates will everlap)")]
        public void WorkflowDetailsSection_TestMethod036_3()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("Person Employment Date Overlap", "Person Employment Date Overlap.Zip");

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string title = "Employment for Adolfo Abbott created by José Brazeta on" + DateTime.Now.WithoutMilliseconds().ToString("dd/MM/yyyy hh:mm:ss");
            string employer = "Workflow Testing - Employer 2";

            Guid EmploymentWeeklyHoursWorkedId = dbHelper.employmentWeeklyHoursWorked.GetByName("30 + hours").FirstOrDefault();
            Guid EmploymentStatusId = dbHelper.employmentStatus.GetByName("Employed").FirstOrDefault();
            Guid? EmploymentTypeId = null;
            Guid? EmploymentReasonLeftId = null;

            DateTime startDate1 = new DateTime(2019, 3, 16);
            DateTime endDate1 = new DateTime(2019, 3, 19);

            DateTime startDate2 = new DateTime(2019, 3, 15);
            DateTime endDate2 = new DateTime(2019, 3, 20);


            //First employment record
            dbHelper.personEmployment.CreatePersonEmployment(title, employer, personId, _defaultTeamId, EmploymentWeeklyHoursWorkedId, EmploymentStatusId, EmploymentTypeId, EmploymentReasonLeftId, startDate1, endDate1);




            //ACT

            //create the record
            try
            {
                dbHelper.personEmployment.CreatePersonEmployment(title, employer, personId, _defaultTeamId, EmploymentWeeklyHoursWorkedId, EmploymentStatusId, EmploymentTypeId, EmploymentReasonLeftId, startDate2, endDate2);

            }
            catch (Exception ex)
            {
                Assert.AreEqual("'Person Employment Date Overlap': An employment record for this person already exists for the period.", ex.Message);
                return;
            }


            //ASSERT
            Assert.Fail("Workflow should throw an exception");
        }


        /// <summary>
        /// Workflow Under Test: Person Employment Date Overlap
        /// </summary>
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7676")]
        [Description("Automation Script for the Test Case - Scenario 36.4 - Test Business Data Overlap action (dates will NOT everlap)")]
        public void WorkflowDetailsSection_TestMethod036_4()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("Person Employment Date Overlap", "Person Employment Date Overlap.Zip");

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string title = "Employment for Adolfo Abbott created by José Brazeta on" + DateTime.Now.WithoutMilliseconds().ToString("dd/MM/yyyy hh:mm:ss");
            string employer = "Workflow Testing - Employer 2";

            Guid EmploymentWeeklyHoursWorkedId = dbHelper.employmentWeeklyHoursWorked.GetByName("30 + hours").FirstOrDefault();
            Guid EmploymentStatusId = dbHelper.employmentStatus.GetByName("Employed").FirstOrDefault();
            Guid? EmploymentTypeId = null;
            Guid? EmploymentReasonLeftId = null;

            DateTime startDate1 = new DateTime(2019, 3, 16);
            DateTime endDate1 = new DateTime(2019, 3, 19);

            DateTime startDate2 = new DateTime(2019, 3, 10);
            DateTime endDate2 = new DateTime(2019, 3, 15);


            //First employment record
            dbHelper.personEmployment.CreatePersonEmployment(title, employer, personId, _defaultTeamId, EmploymentWeeklyHoursWorkedId, EmploymentStatusId, EmploymentTypeId, EmploymentReasonLeftId, startDate1, endDate1);


            //ACT

            //create the record
            var employmentID = dbHelper.personEmployment.CreatePersonEmployment(title, employer, personId, _defaultTeamId, EmploymentWeeklyHoursWorkedId, EmploymentStatusId, EmploymentTypeId, EmploymentReasonLeftId, startDate2, endDate2);


            //ASSERT
            var fields = dbHelper.personEmployment.GetPersonEmploymentByID(employmentID, "Employer");
            Assert.AreEqual(employer, (string)fields["employer"]);
        }

        /// <summary>
        /// Workflow Under Test: Person Employment Date Overlap
        /// </summary>
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7677")]
        [Description("Automation Script for the Test Case - Scenario 36.5 - Test Business Data Overlap action (dates will NOT everlap)")]
        public void WorkflowDetailsSection_TestMethod036_5()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("Person Employment Date Overlap", "Person Employment Date Overlap.Zip");

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string title = "Employment for Adolfo Abbott created by José Brazeta on" + DateTime.Now.WithoutMilliseconds().ToString("dd/MM/yyyy hh:mm:ss");
            string employer = "Workflow Testing - Employer 2";

            Guid EmploymentWeeklyHoursWorkedId = dbHelper.employmentWeeklyHoursWorked.GetByName("30 + hours").FirstOrDefault();
            Guid EmploymentStatusId = dbHelper.employmentStatus.GetByName("Employed").FirstOrDefault();
            Guid? EmploymentTypeId = null;
            Guid? EmploymentReasonLeftId = null;

            DateTime startDate1 = new DateTime(2019, 3, 16);
            DateTime endDate1 = new DateTime(2019, 3, 19);

            DateTime startDate2 = new DateTime(2019, 3, 20);
            DateTime endDate2 = new DateTime(2019, 3, 22);


            //First employment record
            dbHelper.personEmployment.CreatePersonEmployment(title, employer, personId, _defaultTeamId, EmploymentWeeklyHoursWorkedId, EmploymentStatusId, EmploymentTypeId, EmploymentReasonLeftId, startDate1, endDate1);


            //ACT

            //create the record
            var employmentID = dbHelper.personEmployment.CreatePersonEmployment(title, employer, personId, _defaultTeamId, EmploymentWeeklyHoursWorkedId, EmploymentStatusId, EmploymentTypeId, EmploymentReasonLeftId, startDate2, endDate2);


            //ASSERT
            var fields = dbHelper.personEmployment.GetPersonEmploymentByID(employmentID, "Employer");
            Assert.AreEqual(employer, (string)fields["employer"]);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7678")]
        [Description("Automation Script for the Test Case - Scenario 38 - Test CHECK MULTIPLE RESPONSE ITEM BY IDENTIFIER  action (checklist answer NOT selected)")]
        public void WorkflowDetailsSection_TestMethod038()
        {
            var documentId = commonWorkflowMethods.CreateDocumentIfNeeded("Test CDV6-7678", "Test CDV6-7678.Zip");

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S38", "WF Automated Testing - S38.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonWorkflowMethods.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            //Create Social care case for the person
            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 18);


            //ARRANGE 

            //create the case form
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_defaultTeamId, documentId, personId, caseID, new DateTime(2017, 1, 1));

            //get the multiOptionAnswer
            var questionCatalogueId = dbHelper.questionCatalogue.GetByQuestionName("Multi Response CDV6-7678").FirstOrDefault();
            var multiOptionAnswerId = dbHelper.multiOptionAnswer.GetByNameAndQuestionCatalogueID("Day 1", questionCatalogueId).First();

            //get the Document Question Identifier for 'Multiple Response'
            var documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("LOC-DQ-1")[0];

            //set the answer for Multiple Response
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(caseFormID, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswerChecklist.CreateDocumentAnswerChecklist(documentAnswerID, multiOptionAnswerId, true);


            //ACT

            //set the date to activate the workflow
            dbHelper.caseForm.UpdateCaseFormRecord(caseFormID, new DateTime(2019, 1, 1), false);


            //ASSERT

            //Separate assessment field should not be updated by the WF
            var fields = dbHelper.caseForm.GetCaseFormByID(caseFormID, "startdate", "separateassessment");
            Assert.AreEqual(new DateTime(2019, 1, 1), (DateTime)fields["startdate"]);
            Assert.AreEqual(false, (bool)fields["separateassessment"]);

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7679")]
        [Description("Automation Script for the Test Case - Scenario 38.1 - Test CHECK MULTIPLE RESPONSE ITEM BY IDENTIFIER  action (checklist answer selected)")]
        public void WorkflowDetailsSection_TestMethod038_1()
        {
            var documentId = commonWorkflowMethods.CreateDocumentIfNeeded("Test CDV6-7678", "Test CDV6-7678.Zip");

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S38", "WF Automated Testing - S38.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonWorkflowMethods.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            //Create Social care case for the person
            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 18);


            //ARRANGE 

            //create the case form
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_defaultTeamId, documentId, personId, caseID, new DateTime(2017, 1, 1));

            //get the multiOptionAnswer
            var questionCatalogueId = dbHelper.questionCatalogue.GetByQuestionName("Multi Response CDV6-7678").FirstOrDefault();
            var multiOptionAnswerId = dbHelper.multiOptionAnswer.GetByNameAndQuestionCatalogueID("Day 2", questionCatalogueId).First();

            //get the Document Question Identifier for 'Multiple Response'
            var documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("LOC-DQ-1")[0];

            //set the answer for Multiple Response
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(caseFormID, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswerChecklist.CreateDocumentAnswerChecklist(documentAnswerID, multiOptionAnswerId, true);


            //ACT

            //set the date to activate the workflow
            dbHelper.caseForm.UpdateCaseFormRecord(caseFormID, new DateTime(2019, 1, 1), false);


            //ASSERT

            //Separate assessment field should not be updated by the WF
            var fields = dbHelper.caseForm.GetCaseFormByID(caseFormID, "startdate", "separateassessment");
            Assert.AreEqual(new DateTime(2019, 1, 1), (DateTime)fields["startdate"]);
            Assert.AreEqual(true, (bool)fields["separateassessment"]);

        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7680")]
        [Description("Automation Script for the Test Case - Scenario 39 - Test Get Initiating User Action action (Initiating user is Workflow_Test_User_1)")]
        public void WorkflowDetailsSection_TestMethod039()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations to Business Data Actions", "WF Automated Testing - Validations to Business Data Actions.Zip");

            var teamId = commonWorkflowMethods.CreateTeam("UK Local Authority Social Care Team 2", _defaultBusinessUnitId);

            var userId = commonWorkflowMethods.CreateSystemUserRecord("Workflow_Test_User_1", "Workflow", "Test_User_1", "Passw0rd_!", _defaultBusinessUnitId, teamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 39";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userId;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;




            //ACT

            //login with Workflow_Test_User_1
            dbHelper = new DBHelper.DatabaseHelper("Workflow_Test_User_1", "Passw0rd_!");

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);




            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "callerid");
            Assert.AreEqual(userId.ToString(), fields["callerid"].ToString());
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7681")]
        [Description("Automation Script for the Test Case - Scenario 39.1 - Test Get Initiating User Action action (Initiating user is NOT Workflow_Test_User_1)")]
        public void WorkflowDetailsSection_TestMethod039_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations to Business Data Actions", "WF Automated Testing - Validations to Business Data Actions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 39";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "callerid");
            Assert.AreEqual(callerID.ToString(), fields["callerid"].ToString());
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7682")]
        [Description("Automation Script for the Test Case - Scenario 39.2 - Test setting the value of a multi-lookup to another multi-lookup (multi-lookup options match) - exctract the value from a Recipient and set it to Caller - (this is an additional scenario created to validate a bug fix)")]
        public void WorkflowDetailsSection_TestMethod039_2_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations to Business Data Actions", "WF Automated Testing - Validations to Business Data Actions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 39.2";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "callerid", "calleridname", "CallerIdTableName");

            Assert.AreEqual(recipientID.ToString(), fields["callerid"].ToString());
            Assert.AreEqual(recipientIDName, (string)fields["calleridname"]);
            Assert.AreEqual(recipientIdTableName, (string)fields["calleridtablename"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7683")]
        [Description("Automation Script for the Test Case - Scenario 39.2 - Test setting the value of a multi-lookup to another multi-lookup (multi-lookup options match) - exctract the value from a Recipient and set it to Caller - (this is an additional scenario created to validate a bug fix)")]
        public void WorkflowDetailsSection_TestMethod039_2_2()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations to Business Data Actions", "WF Automated Testing - Validations to Business Data Actions.Zip");

            var providerId = commonWorkflowMethods.CreateProviderIfNeeded("Ynys Mon - Mental Health - Provider CDV6-7683", _defaultTeamId);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 39.2";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = providerId;
            string recipientIdTableName = "provider";
            string recipientIDName = "Ynys Mon - Mental Health - Provider";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);




            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "callerid", "calleridname", "CallerIdTableName");

            Assert.AreEqual(recipientID.ToString(), fields["callerid"].ToString());
            Assert.AreEqual(recipientIDName, (string)fields["calleridname"]);
            Assert.AreEqual(recipientIdTableName, (string)fields["calleridtablename"]);
        }




        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7684")]
        [Description("Automation Script for the Test Case - Scenario 39.3 - Test setting the value of a multi-lookup to another multi-lookup (multi-lookup options may not math) - exctract the value from the Regarding field and set it to Caller - (this is an additional scenario created to validate a bug fix)")]
        public void WorkflowDetailsSection_TestMethod039_3_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations to Business Data Actions", "WF Automated Testing - Validations to Business Data Actions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var providerId = commonWorkflowMethods.CreateProviderIfNeeded("Ynys Mon - Mental Health - Provider CDV6-7684", _defaultTeamId);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 39.3";
            string description = "Sample Description ...";

            Guid callerID = providerId;
            string callerIdTableName = "provider";
            string callerIDName = "Ynys Mon - Mental Health - Provider";

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingIdName = personFullName;
            string regardingIdTableName = "person";



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingIdName, _defaultTeamId, regardingIdTableName);



            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "callerid", "calleridname", "CallerIdTableName");

            Assert.AreEqual(regardingID.ToString(), fields["callerid"].ToString());
            Assert.AreEqual(regardingIdName, (string)fields["calleridname"]);
            Assert.AreEqual(regardingIdTableName, (string)fields["calleridtablename"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7685")]
        [Description("Automation Script for the Test Case - Scenario 39.3 - Test setting the value of a multi-lookup to another multi-lookup (multi-lookup options may not math) - exctract the value from the Regarding field and set it to Caller - (this is an additional scenario created to validate a bug fix)")]
        public void WorkflowDetailsSection_TestMethod039_3_2()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations to Business Data Actions", "WF Automated Testing - Validations to Business Data Actions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var providerId = commonWorkflowMethods.CreateProviderIfNeeded("Ynys Mon - Mental Health - Provider CDV6-7685", _defaultTeamId);

            var firstName = "Jhon";
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
            string subject = "WF Testing - Scenario 39.3";
            string description = "Sample Description ...";

            Guid callerID = providerId;
            string callerIdTableName = "provider";
            string callerIDName = "Ynys Mon - Mental Health - Provider";

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = caseID;
            string regardingIdName = caseTitle;

            Guid personid = personId;
            string personidName = personFullName;




            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForCase(
                subject, description, callerID, callerIdTableName, callerIDName,
                recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingIdName, personid, personidName, _defaultTeamId, "CareDirector QA", DateTime.Now.WithoutMilliseconds(), null, null);




            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "callerid", "calleridname", "CallerIdTableName");

            Assert.IsFalse(fields.ContainsKey("callerid"));
            Assert.IsFalse(fields.ContainsKey("calleridname"));
            Assert.IsFalse(fields.ContainsKey("calleridtablename"));
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7686")]
        [Description("Automation Script for the Test Case - Scenario 40 - Test Get Answer By Identifier action (Answers should activate WF actions)")]
        public void WorkflowDetailsSection_TestMethod040()
        {
            var documentId = commonWorkflowMethods.CreateDocumentIfNeeded("Workflow Test Document 1", "Workflow Test Document 1.Zip");

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S40", "WF Automated Testing - S40.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Adolfo";
            var middleName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var lastName = "Abbott";
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonWorkflowMethods.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            //Create Social care case for the person
            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 18);



            //ARRANGE 

            dbHelper = new DBHelper.DatabaseHelper("Jose.brazeta", "Passw0rd_!");

            //create the case form
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_defaultTeamId, documentId, personId, caseID, new DateTime(2019, 1, 1));

            var multipleResponseQuestionId = dbHelper.questionCatalogue.GetByQuestionName("WF Multiple Response").FirstOrDefault();
            var multiOptionAnswer1Id = dbHelper.multiOptionAnswer.GetByNameAndQuestionCatalogueID("Day 2", multipleResponseQuestionId).FirstOrDefault();

            var multipleChoiceQuestionId = dbHelper.questionCatalogue.GetByQuestionName("WF Multiple Choice").FirstOrDefault();
            var multiOptionAnswer2Id = dbHelper.multiOptionAnswer.GetByNameAndQuestionCatalogueID("Option 2", multipleChoiceQuestionId).FirstOrDefault();

            var documentPickListId = dbHelper.documentPickList.GetByName("Religion").FirstOrDefault();
            var documentPickListValueId = dbHelper.documentPickListValue.GetByTextAndDocumentPickListId("Budist", documentPickListId).FirstOrDefault();

            //get the Document Question Identifiers 
            var documentQuestionIdentifier1Id = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-38")[0]; //WF Short Answer
            var documentQuestionIdentifier2Id = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-34")[0]; //Multiple Response
            var documentQuestionIdentifier3Id = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-33")[0]; //Multiple Choice
            var documentQuestionIdentifier4Id = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-37")[0]; //PickList
            var documentQuestionIdentifier5Id = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-29")[0]; //Boolean
            var documentQuestionIdentifier6Id = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-32")[0]; //Lookup

            //set the answer for WF Short Answer
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(caseFormID, documentQuestionIdentifier1Id)[0];
            dbHelper.documentAnswer.UpdateShortAnswer(documentAnswerID, "Jhon Doe");

            //set the answer for Multiple Response
            documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(caseFormID, documentQuestionIdentifier2Id)[0];
            dbHelper.documentAnswerChecklist.CreateDocumentAnswerChecklist(documentAnswerID, multiOptionAnswer1Id, true);

            //set the answer for Multiple Choice
            documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(caseFormID, documentQuestionIdentifier3Id)[0];
            dbHelper.documentAnswer.UpdateMultichoiceAnswer(documentAnswerID, multiOptionAnswer2Id);

            //set the answer for PickList
            documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(caseFormID, documentQuestionIdentifier4Id)[0];
            dbHelper.documentAnswer.UpdatePicklistValueAnswer(documentAnswerID, documentPickListValueId);

            //set the answer for Boolean
            documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(caseFormID, documentQuestionIdentifier5Id)[0];
            dbHelper.documentAnswer.UpdateTrueFalseAnswer(documentAnswerID, true);

            //set the answer for Lookup
            documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(caseFormID, documentQuestionIdentifier6Id)[0];
            dbHelper.documentAnswer.UpdateLookupAnswer(documentAnswerID, personId, "person", "Adolfo Abbott");


            //ACT

            //set the date to activate the workflow
            dbHelper.caseForm.UpdateCaseFormRecord(caseFormID, new DateTime(2019, 3, 1), false);


            //ASSERT

            //validate updates performed by the workflow (all if statments to the Get Answer By Identifier action should be activated)
            var fields = dbHelper.caseForm.GetCaseFormByID(caseFormID, "duedate", "ResponsibleUserID", "SeparateAssessment", "CarerDeclinedJointAssessment", "JointCarerAssessment", "NewPerson");
            Assert.AreEqual(new DateTime(2019, 3, 2), (DateTime)fields["duedate"]);
            Assert.AreEqual(userid.ToString(), fields["responsibleuserid"].ToString());
            Assert.AreEqual(true, (bool)fields["separateassessment"]);
            Assert.AreEqual(true, (bool)fields["carerdeclinedjointassessment"]);
            Assert.AreEqual(true, (bool)fields["jointcarerassessment"]);
            Assert.AreEqual(true, (bool)fields["newperson".ToLower()]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7687")]
        [Description("Automation Script for the Test Case - Scenario 40.1 - Test Get Answer By Identifier action (Answers should NOT activate WF actions)")]
        public void WorkflowDetailsSection_TestMethod040_1()
        {
            var documentId = commonWorkflowMethods.CreateDocumentIfNeeded("Workflow Test Document 1", "Workflow Test Document 1.Zip");

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S40", "WF Automated Testing - S40.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonWorkflowMethods.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            //Create Social care case for the person
            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 18);



            //ARRANGE 

            dbHelper = new DBHelper.DatabaseHelper("Jose.brazeta", "Passw0rd_!");

            //create the case form
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_defaultTeamId, documentId, personId, caseID, new DateTime(2019, 1, 1));

            var multipleResponseQuestionId = dbHelper.questionCatalogue.GetByQuestionName("WF Multiple Response").FirstOrDefault();
            var multiOptionAnswer1Id = dbHelper.multiOptionAnswer.GetByNameAndQuestionCatalogueID("Day 1", multipleResponseQuestionId).FirstOrDefault();

            var multipleChoiceQuestionId = dbHelper.questionCatalogue.GetByQuestionName("WF Multiple Choice").FirstOrDefault();
            var multiOptionAnswer2Id = dbHelper.multiOptionAnswer.GetByNameAndQuestionCatalogueID("Option 1", multipleChoiceQuestionId).FirstOrDefault();

            var documentPickListId = dbHelper.documentPickList.GetByName("Religion").FirstOrDefault();
            var documentPickListValueId = dbHelper.documentPickListValue.GetByTextAndDocumentPickListId("Jedi", documentPickListId).FirstOrDefault();

            //get the Document Question Identifiers 
            var documentQuestionIdentifier1Id = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-38")[0]; //WF Short Answer
            var documentQuestionIdentifier2Id = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-34")[0]; //Multiple Response
            var documentQuestionIdentifier3Id = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-33")[0]; //Multiple Choice
            var documentQuestionIdentifier4Id = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-37")[0]; //PickList
            var documentQuestionIdentifier5Id = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-29")[0]; //Boolean
            var documentQuestionIdentifier6Id = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-32")[0]; //Lookup

            //set the answer for WF Short Answer
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(caseFormID, documentQuestionIdentifier1Id)[0];
            dbHelper.documentAnswer.UpdateShortAnswer(documentAnswerID, "Something Else");

            //set the answer for Multiple Response
            documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(caseFormID, documentQuestionIdentifier2Id)[0];
            dbHelper.documentAnswerChecklist.CreateDocumentAnswerChecklist(documentAnswerID, multiOptionAnswer1Id, true);

            //set the answer for Multiple Choice
            documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(caseFormID, documentQuestionIdentifier3Id)[0];
            dbHelper.documentAnswer.UpdateMultichoiceAnswer(documentAnswerID, multiOptionAnswer2Id);

            //set the answer for PickList
            documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(caseFormID, documentQuestionIdentifier4Id)[0];
            dbHelper.documentAnswer.UpdatePicklistValueAnswer(documentAnswerID, documentPickListValueId);

            //set the answer for Boolean
            documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(caseFormID, documentQuestionIdentifier5Id)[0];
            dbHelper.documentAnswer.UpdateTrueFalseAnswer(documentAnswerID, false);

            //set the answer for Lookup
            documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(caseFormID, documentQuestionIdentifier6Id)[0];
            dbHelper.documentAnswer.UpdateLookupAnswer(documentAnswerID, personId, "person", personFullName);


            //ACT

            //set the date to activate the workflow
            dbHelper.caseForm.UpdateCaseFormRecord(caseFormID, new DateTime(2019, 3, 1), false);

            //ASSERT

            //validate updates performed by the workflow (all if statments to the Get Answer By Identifier action should NOT be activated)
            var fields = dbHelper.caseForm.GetCaseFormByID(caseFormID, "duedate", "ResponsibleUserID", "SeparateAssessment", "CarerDeclinedJointAssessment", "JointCarerAssessment", "NewPerson");
            Assert.AreEqual(false, fields.ContainsKey("duedate"));
            Assert.AreEqual(false, fields.ContainsKey("ResponsibleUserID".ToLower()));
            Assert.AreEqual(false, (bool)fields["SeparateAssessment".ToLower()]);
            Assert.AreEqual(false, (bool)fields["CarerDeclinedJointAssessment".ToLower()]);
            Assert.AreEqual(false, (bool)fields["JointCarerAssessment".ToLower()]);
            Assert.AreEqual(false, (bool)fields["NewPerson".ToLower()]);
        }




        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7688")]
        [Description("Automation Script for the Test Case - Scenario 41 - Test Add Days action")]
        public void WorkflowDetailsSection_TestMethod041()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations to Business Data Actions", "WF Automated Testing - Validations to Business Data Actions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 41";
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

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phonecallDate);




            //ASSERT
            DateTime expectedPhonecallDate = phonecallDate
                .AddYears(1)
                .AddMonths(2)
                .AddDays(21) //add 3 weeks = 3 * 7 = 21 days
                .AddDays(4)
                .AddHours(5)
                .AddMinutes(6);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual(expectedPhonecallDate.ToString(), (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7689")]
        [Description("Automation Script for the Test Case - Scenario 41 - Test Add Days action - phone call date is null")]
        public void WorkflowDetailsSection_TestMethod041_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations to Business Data Actions", "WF Automated Testing - Validations to Business Data Actions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 41";
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

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phonecallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("01/01/0001 00:00:00", (string)fields["notes"]);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7690")]
        [Description("Automation Script for the Test Case - Scenario 42 - Test Get Higher Date action (event date is the bigger date)")]
        public void WorkflowDetailsSection_TestMethod042()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations to Business Data Actions", "WF Automated Testing - Validations to Business Data Actions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 42";
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

            Guid jBrazetaResponsibleUSerID = userid;
            bool InformationByThirdParty = false;
            int DirectionID = 1; //incoming

            bool IsSignificantEvent = true;
            DateTime SignificantEventDate = new DateTime(2019, 3, 1);
            Guid eventCathegoryID = commonWorkflowMethods.CreateSignificantEventCategoryIfNeeded("Default", DateTime.Now.Date, _defaultTeamId);



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, jBrazetaResponsibleUSerID,
                null, InformationByThirdParty, DirectionID, IsSignificantEvent, SignificantEventDate, eventCathegoryID);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual(SignificantEventDate.AddSeconds(10).ToString(), (string)fields["notes"]); //we need to add 10 seconds due to a issue with phoenix - //Some times while executing modified on adding on start date is greater than modified of due date, hence increase 10 second of due date to make start date always less than due date.
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7691")]
        [Description("Automation Script for the Test Case - Scenario 42.1 - Test Get Higher Date action (phone call date is the bigger date)")]
        public void WorkflowDetailsSection_TestMethod042_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations to Business Data Actions", "WF Automated Testing - Validations to Business Data Actions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 42";
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

            Guid jBrazetaResponsibleUSerID = userid;
            bool InformationByThirdParty = false;
            int DirectionID = 1; //incoming

            bool IsSignificantEvent = true;
            DateTime SignificantEventDate = new DateTime(2019, 3, 1);
            Guid eventCathegoryID = commonWorkflowMethods.CreateSignificantEventCategoryIfNeeded("Default", DateTime.Now.Date, _defaultTeamId);



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, jBrazetaResponsibleUSerID,
                null, InformationByThirdParty, DirectionID, IsSignificantEvent, SignificantEventDate, eventCathegoryID);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual(phoneCallDate.AddSeconds(10).ToString(), (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7692")]
        [Description("Automation Script for the Test Case - Scenario 42.1 - Test Get Higher Date action (phone call date is null)")]
        public void WorkflowDetailsSection_TestMethod042_2()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations to Business Data Actions", "WF Automated Testing - Validations to Business Data Actions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 42";
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

            Guid jBrazetaResponsibleUSerID = userid;
            bool InformationByThirdParty = false;
            int DirectionID = 1; //incoming

            bool IsSignificantEvent = true;
            DateTime SignificantEventDate = new DateTime(2019, 3, 1);
            Guid eventCathegoryID = commonWorkflowMethods.CreateSignificantEventCategoryIfNeeded("Default", DateTime.Now.Date, _defaultTeamId);


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, jBrazetaResponsibleUSerID,
                null, InformationByThirdParty, DirectionID, IsSignificantEvent, SignificantEventDate, eventCathegoryID);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual(SignificantEventDate.AddSeconds(10).ToString(), (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7693")]
        [Description("Automation Script for the Test Case - Scenario 42.1 - Test Get Higher Date action (Event date is null)")]
        public void WorkflowDetailsSection_TestMethod042_3()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations to Business Data Actions", "WF Automated Testing - Validations to Business Data Actions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 42";
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

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, false, 1, false, null, null, false);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual(phoneCallDate.AddSeconds(10).ToString(), (string)fields["notes"]);
        }


        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7694")]
        [Description("Automation Script for the Test Case - Scenario 43 - Test Subtract Days action")]
        public void WorkflowDetailsSection_TestMethod043()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations to Business Data Actions", "WF Automated Testing - Validations to Business Data Actions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 43";
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

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phonecallDate);




            //ASSERT
            
            DateTime expectedPhonecallDate = phonecallDate
                .AddYears(-1)
                .AddMonths(-2)
                .AddDays(-21) //add 3 weeks = 3 * 7 = 21 days
                .AddDays(-4)
                .AddHours(-5)
                .AddMinutes(-6);

            DateTime finalDate = expectedPhonecallDate.ToLocalTime();

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual(finalDate.ToString(), (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7695")]
        [Description("Automation Script for the Test Case - Scenario 43 - Test Subtract Days action - phone call date is null")]
        public void WorkflowDetailsSection_TestMethod043_0()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations to Business Data Actions", "WF Automated Testing - Validations to Business Data Actions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 43";
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


            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phonecallDate);




            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("01/01/0001 00:00:00", (string)fields["notes"]);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7696")]
        [Description("Automation Script for the Test Case - Scenario 43.1 - Test Concatenate action")]
        public void WorkflowDetailsSection_TestMethod043_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations to Business Data Actions", "WF Automated Testing - Validations to Business Data Actions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Testing - Scenario 43.1";
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

            //create the 
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject");

            Assert.AreEqual("WF Testing - Scenario 43.1Sample Description ...", (string)fields["subject"]);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7697")]
        [Description("Automation Script for the Test Case - Scenario 44 - Test Update action in a Async workflow (update related person)")]
        public void WorkflowDetailsSection_TestMethod044()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S44", "WF Automated Testing - S44.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var midName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, midName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Testing - Scenario 44";
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
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            int numberOfTries = 1;
            while (numberOfTries <= 75)
            {
                var personData = dbHelper.person.GetPersonById(regardingID, "middlename", "Telephone3", "CreditorNumber");

                if (!personData.ContainsKey("middlename") || !personData.ContainsKey("telephone3") || !personData.ContainsKey("creditornumber"))
                    continue;

                string middleName = (string)personData["middlename"];
                string telephone3 = (string)personData["telephone3"];
                string creditornumber = (string)personData["creditornumber"];

                if (middleName == "Sanders" && telephone3 == "+0351987780" && creditornumber == "13579A")
                    return;

                System.Threading.Thread.Sleep(1000);
                numberOfTries++;

                if (numberOfTries >= 75)
                    Assert.Fail("Workflow took more than 75 seconds to run");
            }
        }




        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7698")]
        [Description("Automation Script for the Test Case - Scenario 46 - WF step with 2 inner condition statments")]

        public void WorkflowDetailsSection_TestMethod046()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 46";
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
            var descriptionAfterSave = (string)(dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes")["notes"]);
            Assert.AreEqual("WF Testinng - Scenario 46 - Action 1 Activated", descriptionAfterSave);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7699")]
        [Description("Automation Script for the Test Case - Scenario 47 - WF step with 2 inner condition statments (2nd if statment not executing)")]
        public void WorkflowDetailsSection_TestMethod047()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 46";
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
            var descriptionAfterSave = (string)(dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes")["notes"]);
            Assert.AreEqual("Sample Description ...", descriptionAfterSave);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7700")]
        [Description("Automation Script for the Test Case - Scenario 48 - Multiple Steps validation")]
        public void WorkflowDetailsSection_TestMethod048()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 48";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 48 - Action 1 Activated", (string)fields["notes"]);
            Assert.AreEqual("01/02/2019 09:15:30 - 0987654321234", (string)fields["subject"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7701")]
        [Description("Automation Script for the Test Case - Scenario 49 - Multiple Steps validation")]
        public void WorkflowDetailsSection_TestMethod049()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 48";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 48 - Action 1 Activated", (string)fields["notes"]);
            Assert.AreEqual("-", (string)fields["subject"]);
        }




        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7702")]
        [Description("Automation Script for the Test Case - Scenario 50 - If Else statments validation")]
        public void WorkflowDetailsSection_TestMethod050()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 50 - 1";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 50 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7703")]
        [Description("Automation Script for the Test Case - Scenario 51 - If Else statments validation")]
        public void WorkflowDetailsSection_TestMethod051()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 50 - 2";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 50 - Action 2 Activated", (string)fields["notes"]);
        }




        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7704")]
        [Description("Automation Script for the Test Case - Scenario 52 - Validation for a Business Object Reference field")]
        public void WorkflowDetailsSection_TestMethod052()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 52";
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
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, userid, _activityCategoryId);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 52 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7705")]
        [Description("Automation Script for the Test Case - Scenario 52 - Validation for a Business Object Reference field")]
        public void WorkflowDetailsSection_TestMethod053()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            if (!dbHelper.activityCategory.GetByName("Assessment").Any())
                dbHelper.activityCategory.CreateActivityCategory("Assessment", new DateTime(2020, 1, 1), _defaultTeamId);
            var assessmentActivityCategoryID = dbHelper.activityCategory.GetByName("Assessment")[0];


            //ARRANGE 
            string subject = "WF Testing - Scenario 52";
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
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, userid, assessmentActivityCategoryID);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7706")]
        [Description("Automation Script for the Test Case - Scenario 54 - Validation for a boolean field")]
        public void WorkflowDetailsSection_TestMethod054()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 54";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 54 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7707")]
        [Description("Automation Script for the Test Case - Scenario 54 - Validation for a boolean field")]
        public void WorkflowDetailsSection_TestMethod055()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Testing - Scenario 54";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7708")]
        [Description("Automation Script for the Test Case - Scenario 56 - Validation for a Picklist field")]
        public void WorkflowDetailsSection_TestMethod056()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 56";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 56 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7709")]
        [Description("Automation Script for the Test Case - Scenario 56 - Validation for a Picklist field")]
        public void WorkflowDetailsSection_TestMethod057()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 56";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7710")]
        [Description("Automation Script for the Test Case - Scenario 60 - Validation for a Date field")]
        public void WorkflowDetailsSection_TestMethod060()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 60";
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
            Guid eventCathegoryID = commonWorkflowMethods.CreateSignificantEventCategoryIfNeeded("Default", DateTime.Now.Date, _defaultTeamId);




            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, userid,
                null, InformationByThirdParty, DirectionID, IsSignificantEvent, SignificantEventDate, eventCathegoryID);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 60 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7711")]
        [Description("Automation Script for the Test Case - Scenario 60 - Validation for a Date field")]
        public void WorkflowDetailsSection_TestMethod061()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 60";
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
            Guid eventCathegoryID = commonWorkflowMethods.CreateSignificantEventCategoryIfNeeded("Default", DateTime.Now.Date, _defaultTeamId);



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, userid,
                null, InformationByThirdParty, DirectionID, IsSignificantEvent, SignificantEventDate, eventCathegoryID);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }


        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7712")]
        [Description("Automation Script for the Test Case - Scenario 62 - Validation for a DateTime field")]
        public void WorkflowDetailsSection_TestMethod062()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 62";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 62 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7713")]
        [Description("Automation Script for the Test Case - Scenario 62 - Validation for a DateTime field")]
        public void WorkflowDetailsSection_TestMethod063()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 62";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7714")]
        [Description("Automation Script for the Test Case - Scenario 64 - Validation for a MultiLookup field")]
        public void WorkflowDetailsSection_TestMethod064()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Adolfo";
            var middleName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var lastName = "Abbott";
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(new Guid("78a78172-6135-4d9f-9abb-d079a12b253d"), firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 64";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 64 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7715")]
        [Description("Automation Script for the Test Case - Scenario 64 - Validation for a MultiLookup field")]
        public void WorkflowDetailsSection_TestMethod065()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
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
            string subject = "WF Testing - Scenario 64";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = "Adolfo Abbott";

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = caseID;
            string regardingName = caseTitle;

            var personid = personId;
            var personidName = personFullName;


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForCase(
                subject, description, callerID, callerIdTableName, callerIDName,
                recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, personid, personidName, _defaultTeamId, "UK Local Authority Social Care", DateTime.Now.WithoutMilliseconds(), null, null);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }





        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7716")]
        [Description("Automation Script for the Test Case - Scenario 66 - Validation for a Large Data Textbox field")]
        public void WorkflowDetailsSection_TestMethod066()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 66";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 66 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7717")]
        [Description("Automation Script for the Test Case - Scenario 66 - Validation for a Large Data Textbox field")]
        public void WorkflowDetailsSection_TestMethod067()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 66";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual(false, fields.ContainsKey("notes"));
        }





        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7718")]
        [Description("Automation Script for the Test Case - Scenario 68 - Validation for a Phone field")]
        public void WorkflowDetailsSection_TestMethod068()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 68";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 68 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7719")]
        [Description("Automation Script for the Test Case - Scenario 68 - Validation for a Phone field")]
        public void WorkflowDetailsSection_TestMethod069()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 68";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }




        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7720")]
        [Description("Automation Script for the Test Case - Scenario 70 - Validation for 'And' condition in if statment")]
        public void WorkflowDetailsSection_TestMethod070()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 70";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 70 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7721")]
        [Description("Automation Script for the Test Case - Scenario 70 - Validation for 'And' condition in if statment")]
        public void WorkflowDetailsSection_TestMethod071()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 70";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, false, direction);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7722")]
        [Description("Automation Script for the Test Case - Scenario 70 - Validation for 'And' condition in if statment")]
        public void WorkflowDetailsSection_TestMethod071_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 70";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, false, direction);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7723")]
        [Description("Automation Script for the Test Case - Scenario 70 - Validation for 'And' condition in if statment")]
        public void WorkflowDetailsSection_TestMethod071_2()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 70";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, false, direction);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }





        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7724")]
        [Description("Automation Script for the Test Case - Scenario 72 - Validation for 'Or' condition in if statment")]
        public void WorkflowDetailsSection_TestMethod072()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 72";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 72 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7725")]
        [Description("Automation Script for the Test Case - Scenario 73 - Validation for 'Or' condition in if statment")]
        public void WorkflowDetailsSection_TestMethod073()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 72";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, false, direction);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 72 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7726")]
        [Description("Automation Script for the Test Case - Scenario 74 - Validation for 'Or' condition in if statment")]
        public void WorkflowDetailsSection_TestMethod074()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 72";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, false, direction);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 72 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7727")]
        [Description("Automation Script for the Test Case - Scenario 75 - Validation for 'Or' condition in if statment")]
        public void WorkflowDetailsSection_TestMethod075()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 72";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, false, direction);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }





        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7728")]
        [Description("Automation Script for the Test Case - Scenario 76 - Condition for related business object")]
        public void WorkflowDetailsSection_TestMethod076()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);
            dbHelper.person.UpdateNHSNumber(personId, "123 456 7881");


            //ARRANGE 
            string subject = "WF Testing - Scenario 76";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 76 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7729")]
        [Description("Automation Script for the Test Case - Scenario 77 - Condition for related business object")]
        public void WorkflowDetailsSection_TestMethod077()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 76";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }






        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7730")]
        [Description("Automation Script for the Test Case - Scenario 78 - Condition for related business object")]
        public void WorkflowDetailsSection_TestMethod078()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 78.1";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 78 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7731")]
        [Description("Automation Script for the Test Case - Scenario 79 - Condition for related business object")]
        public void WorkflowDetailsSection_TestMethod079()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 78.2";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 78 - Action 2 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7732")]
        [Description("Automation Script for the Test Case - Scenario 80 - Condition for related business object")]
        public void WorkflowDetailsSection_TestMethod080()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Validations of Conditions and Steps", "WF Automated Testing - Validations of Conditions and Steps.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 78.3";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }





        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7733")]
        [Description("Automation Script for the Test Case - Scenario 81 - Validate the 'Equals' operator")]
        public void WorkflowDetailsSection_TestMethod081()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 81";
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

            DateTime phonecalldate = DateTime.Now.WithoutMilliseconds().Date;
            bool InformationByThirdParty = false;
            bool IsCaseNote = false;


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phonecalldate, null, null, InformationByThirdParty, 1, false, null, null, IsCaseNote);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 81 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7734")]
        [Description("Automation Script for the Test Case - Scenario 82 - Validate the 'Equals' operator")]
        public void WorkflowDetailsSection_TestMethod082()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 81";
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

            DateTime phonecalldate = DateTime.Now.WithoutMilliseconds().Date;
            bool InformationByThirdParty = true;
            bool IsCaseNote = true;


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phonecalldate, null, null, InformationByThirdParty, 1, false, null, null, IsCaseNote);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 81 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7735")]
        [Description("Automation Script for the Test Case - Scenario 83 - Validate the 'Equals' operator")]
        public void WorkflowDetailsSection_TestMethod083()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 81";
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

            DateTime phonecalldate = DateTime.Now.WithoutMilliseconds().Date;
            bool InformationByThirdParty = true;
            bool IsCaseNote = false;


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phonecalldate, null, null, InformationByThirdParty, 1, false, null, null, IsCaseNote);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7736")]
        [Description("Automation Script for the Test Case - Scenario 83 - Validate the 'Equals' operator")]
        public void WorkflowDetailsSection_TestMethod083_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 81";
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

            DateTime phonecalldate = DateTime.Now.WithoutMilliseconds().Date;
            bool InformationByThirdParty = false;
            bool IsCaseNote = true;



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phonecalldate, null, null, InformationByThirdParty, 1, false, null, null, IsCaseNote);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }




        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7737")]
        [Description("Automation Script for the Test Case - Scenario 84 - Validate the 'Does Not Equal' operator")]
        public void WorkflowDetailsSection_TestMethod084()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var user1id = commonWorkflowMethods.CreateSystemUserRecord("Workflow_Test_User_10", "Workflow", "Test_User_10", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var user2id = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);


            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 84";
            string description = "Sample Description ...";

            Guid callerID = user1id;
            string callerIdTableName = "systemuser";
            string callerIDName = "Workflow Test_User_10";

            Guid recipientID = user2id;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;

            DateTime phonecalldate = DateTime.Now.WithoutMilliseconds().Date;
            bool InformationByThirdParty = false;
            bool IsCaseNote = false;

            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phonecalldate, user2id, null, InformationByThirdParty, 1, false, null, null, IsCaseNote);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("<p>WF Testinng - Scenario 84 - Action 1 Activated</p>", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7738")]
        [Description("Automation Script for the Test Case - Scenario 84.1 - Validate the 'Does Not Equal' operator (caller field is null)")]
        public void WorkflowDetailsSection_TestMethod084_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 84";
            string description = "Sample Description ...";

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;

            DateTime phonecalldate = DateTime.Now.WithoutMilliseconds().Date;
            bool InformationByThirdParty = false;
            bool IsCaseNote = false;


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, null, null, null, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phonecalldate, null, null, InformationByThirdParty, 1, false, null, null, IsCaseNote);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7739")]
        [Description("Automation Script for the Test Case - Scenario 84 - Validate the 'Does Not Equal' operator")]
        public void WorkflowDetailsSection_TestMethod085()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var user1id = commonWorkflowMethods.CreateSystemUserRecord("Workflow_Test_User_9", "Workflow", "Test_User_9", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var user2id = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 84";
            string description = "Sample Description ...";

            Guid callerID = user1id;
            string callerIdTableName = "systemuser";
            string callerIDName = "Workflow Test_User_9";

            Guid recipientID = user2id;
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phonecalldate, user2id, null, InformationByThirdParty, 1, false, null, null, IsCaseNote);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }




        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7740")]
        [Description("Automation Script for the Test Case - Scenario 86 - Validate the 'contains data' operator")]
        public void WorkflowDetailsSection_TestMethod086()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 86";
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
            Guid eventCathegoryID = commonWorkflowMethods.CreateSignificantEventCategoryIfNeeded("Default", DateTime.Now.Date, _defaultTeamId);



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 86 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7741")]
        [Description("Automation Script for the Test Case - Scenario 87 - Validate the 'contains data' operator")]
        public void WorkflowDetailsSection_TestMethod087()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 86";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }




        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7742")]
        [Description("Automation Script for the Test Case - Scenario 88 - Validate the 'does not contains data' operator")]
        public void WorkflowDetailsSection_TestMethod088()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 88";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 88 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7743")]
        [Description("Automation Script for the Test Case - Scenario 89 - Validate the 'does not contains data' operator")]
        public void WorkflowDetailsSection_TestMethod089()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 88";
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

            DateTime phoneCallDate = DateTime.Now.WithoutMilliseconds();



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }




        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7744")]
        [Description("Automation Script for the Test Case - Scenario 90 - Validate the 'In' operator")]
        public void WorkflowDetailsSection_TestMethod090()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Maureen";
            var middleName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var lastName = "Wheeler";
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(new Guid("da2faf81-c7ce-40cd-a2b6-7d056a88c317"), firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 90";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 90 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7745")]
        [Description("Automation Script for the Test Case - Scenario 91 - Validate the 'In' operator")]
        public void WorkflowDetailsSection_TestMethod091()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Ruben";
            var middleName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var lastName = "Stein";
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(new Guid("e6795d93-73db-4797-b8de-79f176838111"), firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 90";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 90 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7746")]
        [Description("Automation Script for the Test Case - Scenario 92 - Validate the 'In' operator")]
        public void WorkflowDetailsSection_TestMethod092()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 90";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-21339")]
        [Description("Automation Script for the Test Case - Scenario 90.2 - Validate the 'In' operator with only 1 option inside")]
        public void WorkflowDetailsSection_TestMethod090_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Maureen";
            var middleName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var lastName = "Wheeler";
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(new Guid("da2faf81-c7ce-40cd-a2b6-7d056a88c317"), firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 90 (2)";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("<p>WF Testinng - Scenario 90 - Action 2&nbsp;Activated</p>", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-21340")]
        [Description("Automation Script for the Test Case - Scenario 90.2 - Validate the 'In' operator with only 1 option inside")]
        public void WorkflowDetailsSection_TestMethod090_2()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 90 (2)";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }



        
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7751")]
        [Description("Automation Script for the Test Case - Scenario 97 - Validate the 'Like' operator")]
        public void WorkflowDetailsSection_TestMethod097()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 97";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 97 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7752")]
        [Description("Automation Script for the Test Case - Scenario 97.1 - Validate the 'Like' operator")]
        public void WorkflowDetailsSection_TestMethod097_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 97";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 97 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7753")]
        [Description("Automation Script for the Test Case - Scenario 98 - Validate the 'Like' operator")]
        public void WorkflowDetailsSection_TestMethod098()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 97";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7754")]
        [Description("Automation Script for the Test Case - Scenario 98.1 - Validate the 'Like' operator")]
        public void WorkflowDetailsSection_TestMethod098_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 97";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }


        
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7755")]
        [Description("Automation Script for the Test Case - Scenario 99 - Validate the 'Starts With' operator")]
        public void WorkflowDetailsSection_TestMethod099()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 99";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 99 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7756")]
        [Description("Automation Script for the Test Case - Scenario 100 - Validate the 'Starts With' operator")]
        public void WorkflowDetailsSection_TestMethod100()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 99";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7757")]
        [Description("Automation Script for the Test Case - Scenario 100.1 - Validate the 'Starts With' operator")]
        public void WorkflowDetailsSection_TestMethod100_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Testing - Scenario 99";
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



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }




        
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7758")]
        [Description("Automation Script for the Test Case - Scenario 101 - Validate the 'Between' operator")]
        public void WorkflowDetailsSection_TestMethod101()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 101";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 101 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7759")]
        [Description("Automation Script for the Test Case - Scenario 101.1 - Validate the 'Between' operator")]
        public void WorkflowDetailsSection_TestMethod101_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 101";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 101 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7760")]
        [Description("Automation Script for the Test Case - Scenario 101.2 - Validate the 'Between' operator")]
        public void WorkflowDetailsSection_TestMethod101_2()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 101";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 101 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7761")]
        [Description("Automation Script for the Test Case - Scenario 101.2 - Validate the 'Between' operator")]
        public void WorkflowDetailsSection_TestMethod101_3()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 101";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7762")]
        [Description("Automation Script for the Test Case - Scenario 102 - Validate the 'Between' operator")]
        public void WorkflowDetailsSection_TestMethod102()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 101";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7763")]
        [Description("Automation Script for the Test Case - Scenario 102.1 - Validate the 'Between' operator")]
        public void WorkflowDetailsSection_TestMethod102_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 101";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7764")]
        [Description("Automation Script for the Test Case - Scenario 102.2 - Validate the 'Between' operator")]
        public void WorkflowDetailsSection_TestMethod102_2()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 101";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }



        
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7765")]
        [Description("Automation Script for the Test Case - Scenario 103 - Validate the 'Is Grated Than' operator")]
        public void WorkflowDetailsSection_TestMethod103()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 103";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 103 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7766")]
        [Description("Automation Script for the Test Case - Scenario 104 - Validate the 'Is Grated Than' operator")]
        public void WorkflowDetailsSection_TestMethod104()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 103";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7767")]
        [Description("Automation Script for the Test Case - Scenario 103.1 - Validate the 'Is Grated Than' operator")]
        public void WorkflowDetailsSection_TestMethod104_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 103";
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
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7768")]
        [Description("Automation Script for the Test Case - Scenario 103.1 - Validate the 'Is Grated Than' operator")]
        public void WorkflowDetailsSection_TestMethod104_2()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 103";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }





        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7769")]
        [Description("Automation Script for the Test Case - Scenario 105 - Validate the 'In Future' operator")]
        public void WorkflowDetailsSection_TestMethod105()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 105";
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

            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(1).Date;


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 105 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7770")]
        [Description("Automation Script for the Test Case - Scenario 106 - Validate the 'In Future' operator")]
        public void WorkflowDetailsSection_TestMethod106()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 105";
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

            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(-1).Date;



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7771")]
        [Description("Automation Script for the Test Case - Scenario 106.1 - Validate the 'In Future' operator")]
        public void WorkflowDetailsSection_TestMethod106_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 105";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7772")]
        [Description("Automation Script for the Test Case - Scenario 107 - Validate the 'Next N Days' operator")]
        public void WorkflowDetailsSection_TestMethod107()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 107";
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

            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(1).Date;


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 107 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7773")]
        [Description("Automation Script for the Test Case - Scenario 107.1 - Validate the 'Next N Days' operator")]
        public void WorkflowDetailsSection_TestMethod107_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 107";
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

            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(2).Date;



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 107 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7774")]
        [Description("Automation Script for the Test Case - Scenario 108 - Validate the 'Next N Days' operator")]
        public void WorkflowDetailsSection_TestMethod108()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 107";
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

            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(4).Date;




            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7775")]
        [Description("Automation Script for the Test Case - Scenario 108.1 - Validate the 'Next N Days' operator")]
        public void WorkflowDetailsSection_TestMethod108_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 107";
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

            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(-1).Date;




            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7776")]
        [Description("Automation Script for the Test Case - Scenario 108.2 - Validate the 'Next N Days' operator")]
        public void WorkflowDetailsSection_TestMethod108_2()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 107";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }




        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7777")]
        [Description("Automation Script for the Test Case - Scenario 109 - Validate the 'Last N Months' operator")]
        public void WorkflowDetailsSection_TestMethod109()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 109";
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

            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddMonths(-1).Date;




            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 109 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7778")]
        [Description("Automation Script for the Test Case - Scenario 109.1 - Validate the 'Last N Months' operator")]
        public void WorkflowDetailsSection_TestMethod109_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 109";
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

            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddMonths(-2).AddDays(1).Date;


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 109 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7779")]
        [Description("Automation Script for the Test Case - Scenario 109.2 - Validate the 'Last N Months' operator")]
        public void WorkflowDetailsSection_TestMethod109_2()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 109";
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

            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().Date.AddMonths(-2).AddDays(-1);


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7780")]
        [Description("Automation Script for the Test Case - Scenario 109.3 - Validate the 'Last N Months' operator")]
        public void WorkflowDetailsSection_TestMethod109_3()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 109";
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

            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(1).Date;




            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7781")]
        [Description("Automation Script for the Test Case - Scenario 109.4 - Validate the 'Last N Months' operator")]
        public void WorkflowDetailsSection_TestMethod109_4()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 109";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }




        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7782")]
        [Description("Automation Script for the Test Case - Scenario 111 - Validate the 'Older Than N Years' operator")]
        public void WorkflowDetailsSection_TestMethod111()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 111";
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

            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddYears(-2).AddDays(-1).Date;


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 111 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7783")]
        [Description("Automation Script for the Test Case - Scenario 112 - Validate the 'Older Than N Years' operator")]
        public void WorkflowDetailsSection_TestMethod112()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 111";
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

            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddYears(-2).AddDays(1).Date;




            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7784")]
        [Description("Automation Script for the Test Case - Scenario 112.1 - Validate the 'Older Than N Years' operator")]
        public void WorkflowDetailsSection_TestMethod112_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 111";
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

            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(1).Date;


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7785")]
        [Description("Automation Script for the Test Case - Scenario 112.2 - Validate the 'Older Than N Years' operator")]
        public void WorkflowDetailsSection_TestMethod112_2()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 111";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7786")]
        [Description("Automation Script for the Test Case - Scenario 113 - Validate the 'Today' operator")]
        public void WorkflowDetailsSection_TestMethod113()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 113";
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

            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().Date.AddHours(8);




            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 113 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7787")]
        [Description("Automation Script for the Test Case - Scenario 114 - Validate the 'Today' operator")]
        public void WorkflowDetailsSection_TestMethod114()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 113";
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

            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(-1).Date;



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7788")]
        [Description("Automation Script for the Test Case - Scenario 114.1 - Validate the 'Today' operator")]
        public void WorkflowDetailsSection_TestMethod114_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 113";
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

            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(1).Date.AddHours(7);



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7789")]
        [Description("Automation Script for the Test Case - Scenario 114.2 - Validate the 'Today' operator")]
        public void WorkflowDetailsSection_TestMethod114_2()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 113";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7790")]
        [Description("Automation Script for the Test Case - Scenario 115 - Validate the 'Not Between' operator")]
        public void WorkflowDetailsSection_TestMethod115()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 115";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 115 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7791")]
        [Description("Automation Script for the Test Case - Scenario 115.1 - Validate the 'Not Between' operator")]
        public void WorkflowDetailsSection_TestMethod115_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 115";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 115 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7792")]
        [Description("Automation Script for the Test Case - Scenario 116 - Validate the 'Not Between' operator")]
        public void WorkflowDetailsSection_TestMethod116()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 115";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7793")]
        [Description("Automation Script for the Test Case - Scenario 116.1 - Validate the 'Not Between' operator")]
        public void WorkflowDetailsSection_TestMethod116_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 115";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7794")]
        [Description("Automation Script for the Test Case - Scenario 116.2 - Validate the 'Not Between' operator")]
        public void WorkflowDetailsSection_TestMethod116_2()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 115";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7795")]
        [Description("Automation Script for the Test Case - Scenario 116.3 - Validate the 'Not Between' operator")]
        public void WorkflowDetailsSection_TestMethod116_3()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 115";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7796")]
        [Description("Automation Script for the Test Case - Scenario 117 - Validate the 'Is Grated Than Or Equal To' operator")]
        public void WorkflowDetailsSection_TestMethod117()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 117";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 117 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7797")]
        [Description("Automation Script for the Test Case - Scenario 118 - Validate the 'Is Grated Than Or Equal To' operator")]
        public void WorkflowDetailsSection_TestMethod118()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 117";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 117 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7798")]
        [Description("Automation Script for the Test Case - Scenario 119 - Validate the 'Is Grated Than Or Equal To' operator")]
        public void WorkflowDetailsSection_TestMethod119()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 117";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }


        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7799")]
        [Description("Automation Script for the Test Case - Scenario 120 - Validate the 'Is Less Than' operator")]
        public void WorkflowDetailsSection_TestMethod120()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 120";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 120 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7800")]
        [Description("Automation Script for the Test Case - Scenario 121 - Validate the 'Is Less Than' operator")]
        public void WorkflowDetailsSection_TestMethod121()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 120";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7801")]
        [Description("Automation Script for the Test Case - Scenario 121.1 - Validate the 'Is Less Than' operator")]
        public void WorkflowDetailsSection_TestMethod121_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 120";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }



        
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7802")]
        [Description("Automation Script for the Test Case - Scenario 122 - Validate the 'Is Less Than Or Equal To' operator")]
        public void WorkflowDetailsSection_TestMethod122()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 122";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 122 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7803")]
        [Description("Automation Script for the Test Case - Scenario 123 - Validate the 'Is Less Than Or Equal To' operator")]
        public void WorkflowDetailsSection_TestMethod123()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 122";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 122 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7804")]
        [Description("Automation Script for the Test Case - Scenario 124 - Validate the 'Is Less Than Or Equal To' operator")]
        public void WorkflowDetailsSection_TestMethod124()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 122";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }






        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7805")]
        [Description("Automation Script for the Test Case - Scenario 125 - Validate the 'In Past' operator")]
        public void WorkflowDetailsSection_TestMethod125()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 125";
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



            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(-1).Date;





            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 125 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7806")]
        [Description("Automation Script for the Test Case - Scenario 126 - Validate the 'In Past' operator")]
        public void WorkflowDetailsSection_TestMethod126()
        {

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 125";
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



            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddHours(2);





            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7807")]
        [Description("Automation Script for the Test Case - Scenario 127 - Validate the 'Is Grated Than Today's Date' operator")]
        public void WorkflowDetailsSection_TestMethod127()
        {

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 127";
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



            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(1).Date;





            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 127 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7808")]
        [Description("Automation Script for the Test Case - Scenario 128 - Validate the 'Is Grated Than Today's Date' operator")]
        public void WorkflowDetailsSection_TestMethod128()
        {

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 127";
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



            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().Date;





            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7809")]
        [Description("Automation Script for the Test Case - Scenario 128.1 - Validate the 'Is Grated Than Today's Date' operator")]
        public void WorkflowDetailsSection_TestMethod128_1()
        {

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 127";
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



            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(-1);





            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7810")]
        [Description("Automation Script for the Test Case - Scenario 129 - Validate the 'Is Less Than Today's Date' operator")]
        public void WorkflowDetailsSection_TestMethod129()
        {

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 129";
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



            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(-1).Date;





            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 129 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7811")]
        [Description("Automation Script for the Test Case - Scenario 130 - Validate the 'Is Less Than Today's Date' operator")]
        public void WorkflowDetailsSection_TestMethod130()
        {

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 129";
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



            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds();





            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7812")]
        [Description("Automation Script for the Test Case - Scenario 131 - Validate the 'Last N Days' operator")]
        public void WorkflowDetailsSection_TestMethod131()
        {

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 131";
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



            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(-1).Date;





            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 131 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7813")]
        [Description("Automation Script for the Test Case - Scenario 132 - Validate the 'Last N Days' operator")]
        public void WorkflowDetailsSection_TestMethod132()
        {

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 131";
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



            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(-3).Date;





            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7814")]
        [Description("Automation Script for the Test Case - Scenario 132.1 - Validate the 'Last N Days' operator")]
        public void WorkflowDetailsSection_TestMethod132_1()
        {

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 131";
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



            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(1).Date;





            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }


        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7815")]
        [Description("Automation Script for the Test Case - Scenario 133 - Validate the 'Last N Years' operator")]
        public void WorkflowDetailsSection_TestMethod133()
        {

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 133";
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



            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddYears(-1).AddMonths(-6).Date;





            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 133 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7816")]
        [Description("Automation Script for the Test Case - Scenario 134 - Validate the 'Last N Years' operator")]
        public void WorkflowDetailsSection_TestMethod134()
        {

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 133";
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



            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddYears(-2).AddMonths(-6).Date;





            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7817")]
        [Description("Automation Script for the Test Case - Scenario 134.1 - Validate the 'Last N Years' operator")]
        public void WorkflowDetailsSection_TestMethod134_1()
        {

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 133";
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



            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(1).Date;





            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7818")]
        [Description("Automation Script for the Test Case - Scenario 135 - Validate the 'Next N Months' operator")]
        public void WorkflowDetailsSection_TestMethod135()
        {

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 135";
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



            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddMonths(2).Date;





            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 135 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7819")]
        [Description("Automation Script for the Test Case - Scenario 136 - Validate the 'Next N Months' operator")]
        public void WorkflowDetailsSection_TestMethod136()
        {

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 135";
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



            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddMonths(4).Date;





            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7820")]
        [Description("Automation Script for the Test Case - Scenario 136.1 - Validate the 'Next N Months' operator")]
        public void WorkflowDetailsSection_TestMethod136_1()
        {

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 135";
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



            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(-1).Date;





            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7821")]
        [Description("Automation Script for the Test Case - Scenario 137 - Validate the 'Next N Years' operator")]
        public void WorkflowDetailsSection_TestMethod137()
        {

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 137";
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



            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddYears(1).AddMonths(6).Date;





            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 137 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7822")]
        [Description("Automation Script for the Test Case - Scenario 138 - Validate the 'Next N Years' operator")]
        public void WorkflowDetailsSection_TestMethod138()
        {

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 137";
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



            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddYears(2).AddMonths(1).Date;





            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7823")]
        [Description("Automation Script for the Test Case - Scenario 138.1 - Validate the 'Next N Years' operator")]
        public void WorkflowDetailsSection_TestMethod138_1()
        {

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 137";
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



            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(-1).Date;





            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }


        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7824")]
        [Description("Automation Script for the Test Case - Scenario 139 - Validate the 'Older Than N Days' operator")]
        public void WorkflowDetailsSection_TestMethod139()
        {

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 139";
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



            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(-3);





            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 139 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7825")]
        [Description("Automation Script for the Test Case - Scenario 140 - Validate the 'Older Than N Days' operator")]
        public void WorkflowDetailsSection_TestMethod140()
        {

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 139";
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



            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(-1);





            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7826")]
        [Description("Automation Script for the Test Case - Scenario 140.1 - Validate the 'Older Than N Days' operator")]
        public void WorkflowDetailsSection_TestMethod140_1()
        {

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 139";
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



            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(1);





            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7827")]
        [Description("Automation Script for the Test Case - Scenario 141 - Validate the 'Older Than N Months' operator")]
        public void WorkflowDetailsSection_TestMethod141()
        {

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 141";
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



            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddMonths(-3);





            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 141 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7828")]
        [Description("Automation Script for the Test Case - Scenario 142 - Validate the 'Older Than N Months' operator")]
        public void WorkflowDetailsSection_TestMethod142()
        {

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 141";
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



            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddMonths(-1).AddDays(-10);





            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7829")]
        [Description("Automation Script for the Test Case - Scenario 142.1 - Validate the 'Older Than N Months' operator")]
        public void WorkflowDetailsSection_TestMethod142_1()
        {

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 141";
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



            DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(1);





            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }




        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7830")]
        [Description("Automation Script for the Test Case - Scenario 143 - Validate the 'Older Than N Months' operator")]
        public void WorkflowDetailsSection_TestMethod143()
        {

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 143";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("WF Testinng - Scenario 143 - Action 1 Activated", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7831")]
        [Description("Automation Script for the Test Case - Scenario 144 - Validate the 'Older Than N Months' operator")]
        public void WorkflowDetailsSection_TestMethod144()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Operators in Conditions", "WF Automated Testing - Testing the Operators in Conditions.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 143";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
        }





        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7832")]
        [Description("Automation Script for the Test Case - Scenario 149 - Custom Fields Tool - test Clear action")]
        public void WorkflowDetailsSection_TestMethod149()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 149";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual(false, fields.ContainsKey("notes"));
        }


        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7833")]
        [Description("Automation Script for the Test Case - Scenario 150 - Custom Fields Tool - extract caller field ")]
        public void WorkflowDetailsSection_TestMethod150()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 150";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "subject", "notes");
            Assert.AreEqual(personFullName, (string)fields["notes"]);
        }


        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7834")]
        [Description("Automation Script for the Test Case - Scenario 151 - Custom Fields Tool - extract phone number field")]
        public void WorkflowDetailsSection_TestMethod151()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 151";
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
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("0997654321234", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7835")]
        [Description("Automation Script for the Test Case - Scenario 152 - Custom Fields Tool - extract phone number field")]
        public void WorkflowDetailsSection_TestMethod152()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 151";
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



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual(false, fields.ContainsKey("notes"));
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7836")]
        [Description("Automation Script for the Test Case - Scenario 153 - Custom Fields Tool - Test Default Value")]
        public void WorkflowDetailsSection_TestMethod153()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 153";
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




            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("No Phone Number", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7837")]
        [Description("Automation Script for the Test Case - Scenario 153.1 - Custom Fields Tool - Test Default Value")]
        public void WorkflowDetailsSection_TestMethod153_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 153";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321";

            Guid regardingID = personId;
            string regardingName = personFullName;




            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("0987654321", (string)fields["notes"]);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7838")]
        [Description("Automation Script for the Test Case - Scenario 154 - Custom Fields Tool - Extract Direction field")]
        public void WorkflowDetailsSection_TestMethod154()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 154";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321";

            Guid regardingID = personId;
            string regardingName = personFullName;


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("Incoming", (string)fields["notes"]);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7839")]
        [Description("Automation Script for the Test Case - Scenario 155 - Custom Fields Tool - Extract Phone Call Date field")]
        public void WorkflowDetailsSection_TestMethod155()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 155";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321";

            Guid regardingID = personId;
            string regardingName = personFullName;

            DateTime phoneCallDate = new DateTime(2019, 3, 27, 10, 30, 00);



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("27/03/2019 10:30:00", (string)fields["notes"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7840")]
        [Description("Automation Script for the Test Case - Scenario 156 - Custom Fields Tool - Extract Phone Call Date field")]
        public void WorkflowDetailsSection_TestMethod156()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 155";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321";

            Guid regardingID = personId;
            string regardingName = personFullName;

            DateTime? phoneCallDate = null;



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual(false, fields.ContainsKey("notes"));
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7841")]
        [Description("Automation Script for the Test Case - Scenario 157 - Custom Fields Tool - Extract boolean field")]
        public void WorkflowDetailsSection_TestMethod157()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 157";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321";

            Guid regardingID = personId;
            string regardingName = personFullName;

            DateTime phoneCallDate = new DateTime(2019, 3, 27, 10, 30, 00);


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, true);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("Yes", (string)fields["notes"]);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7842")]
        [Description("Automation Script for the Test Case - Scenario 158 - Custom Fields Tool - Extract boolean field")]
        public void WorkflowDetailsSection_TestMethod158()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 158";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321";

            Guid regardingID = personId;
            string regardingName = personFullName;

            DateTime phoneCallDate = new DateTime(2019, 3, 27, 10, 30, 00);

            DateTime eventDate = new DateTime(2019, 3, 26);
            Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, false, 1, true, eventDate, eventCathegoryID);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("26/03/2019", (string)fields["notes"]);
        }

        [TestProperty("JiraIssueID", "CDV6-7843")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 159 - Custom Fields Tool - Extract boolean field")]
        public void WorkflowDetailsSection_TestMethod159()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 158";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321";

            Guid regardingID = personId;
            string regardingName = personFullName;

            DateTime phoneCallDate = new DateTime(2019, 3, 27, 10, 30, 00);

            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, false, 1, false, null, null, false);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual(false, fields.ContainsKey("notes"));
        }




        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7844")]
        [Description("Automation Script for the Test Case - Scenario 160 - Custom Fields Tool - Extract boolean field")]
        public void WorkflowDetailsSection_TestMethod160()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 160";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321";

            Guid regardingID = personId;
            string regardingName = personFullName;

            DateTime phoneCallDate = new DateTime(2019, 3, 27, 10, 30, 00);
            bool IsCaseNote = true;
            bool informationByThirdParty = false;


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, informationByThirdParty, 1, false, null, null, IsCaseNote);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "InformationByThirdParty");
            Assert.AreEqual(true, (bool)fields["InformationByThirdParty".ToLower()]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7845")]
        [Description("Automation Script for the Test Case - Scenario 161 - Custom Fields Tool - Extract boolean field")]
        public void WorkflowDetailsSection_TestMethod161()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 160";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321";

            Guid regardingID = personId;
            string regardingName = personFullName;

            DateTime phoneCallDate = new DateTime(2019, 3, 27, 10, 30, 00);
            bool IsCaseNote = false;
            bool informationByThirdParty = true;



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, informationByThirdParty, 1, false, null, null, IsCaseNote);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "InformationByThirdParty");
            Assert.AreEqual(false, (bool)fields["InformationByThirdParty".ToLower()]);
        }


        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7846")]
        [Description("Automation Script for the Test Case - Scenario 162 - Custom Fields Tool - Extract boolean field")]
        public void WorkflowDetailsSection_TestMethod162()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 162";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321";

            Guid regardingID = personId;
            string regardingName = personFullName;

            DateTime phoneCallDate = new DateTime(2019, 3, 27, 10, 30, 00);
            bool IsCaseNote = false;
            bool informationByThirdParty = false;



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, informationByThirdParty, 1, false, null, null, IsCaseNote);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "iscasenote");
            Assert.AreEqual(true, (bool)fields["iscasenote".ToLower()]);
        }


        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7847")]
        [Description("Automation Script for the Test Case - Scenario 163 - Custom Fields Tool - Extract boolean field")]
        public void WorkflowDetailsSection_TestMethod163()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 163";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321";

            Guid regardingID = personId;
            string regardingName = personFullName;




            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            string username = ConfigurationManager.AppSettings["Username"];
            string DataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

            if (DataEncoded.Equals("true"))
            {
                var base64EncodedBytes = System.Convert.FromBase64String(username);
                username = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }

            var createdByUserId = dbHelper.systemUser.GetSystemUserByUserName(username).FirstOrDefault();
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "ResponsibleUserId");
            Assert.AreEqual(createdByUserId.ToString(), fields["ResponsibleUserId".ToLower()].ToString());
        }


        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7848")]
        [Description("Automation Script for the Test Case - Scenario 164 - Custom Fields Tool - Extract boolean field")]
        public void WorkflowDetailsSection_TestMethod164()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var Workflow_Test_User_30_Userid = commonWorkflowMethods.CreateSystemUserRecord(new Guid("0b82e39a-cf64-ed11-a336-005056926fe4"), "Workflow_Test_User_30", "Workflow", "Test User 30", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 164";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321";

            Guid regardingID = personId;
            string regardingName = personFullName;


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "ResponsibleUserId");
            Assert.AreEqual(Workflow_Test_User_30_Userid.ToString(), fields["ResponsibleUserId".ToLower()].ToString());
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7849")]
        [Description("Automation Script for the Test Case - Scenario 165 - Custom Fields Tool - Extract boolean field")]
        public void WorkflowDetailsSection_TestMethod165()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 165";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321";

            Guid regardingID = personId;
            string regardingName = personFullName;

            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual(personFullName, (string)fields["notes".ToLower()]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7850")]
        [Description("Automation Script for the Test Case - Scenario 166 - Custom Fields Tool - Extract boolean field")]
        public void WorkflowDetailsSection_TestMethod166()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var Workflow_Test_User_40_Userid = commonWorkflowMethods.CreateSystemUserRecord("Workflow_Test_User_40", "Workflow", "Test User 40", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 165";
            string description = "Sample Description ...";

            Guid callerID = Workflow_Test_User_40_Userid;
            string callerIdTableName = "systemuser";
            string callerIDName = "Workflow Test User 40";

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321";

            Guid regardingID = personId;
            string regardingName = personFullName;




            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual(false, fields.ContainsKey("notes"));
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7851")]
        [Description("Automation Script for the Test Case - Scenario 167 - Custom Fields Tool - Extract boolean field")]
        public void WorkflowDetailsSection_TestMethod167()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var Workflow_Test_User_50_Userid = commonWorkflowMethods.CreateSystemUserRecord("Workflow_Test_User_50", "Workflow", "Test User 50", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 167";
            string description = "Sample Description ...";

            Guid callerID = Workflow_Test_User_50_Userid;
            string callerIdTableName = "systemuser";
            string callerIDName = "Workflow Test User 50";

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321";

            Guid regardingID = personId;
            string regardingName = personFullName;



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("Workflow Test User 50", (string)fields["notes".ToLower()]);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7852")]
        [Description("Automation Script for the Test Case - Scenario 168 - Custom Fields Tool - Extract boolean field")]
        public void WorkflowDetailsSection_TestMethod168()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);
            dbHelper.person.UpdateAllowEmail(personId, true);


            //ARRANGE 
            string subject = "WF Testing - Scenario 168";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321";

            Guid regardingID = personId;
            string regardingName = personFullName;




            //ACT

            //Create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "IsCaseNote");
            Assert.AreEqual(true, (bool)fields["IsCaseNote".ToLower()]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7853")]
        [Description("Automation Script for the Test Case - Scenario 169 - Custom Fields Tool - Extract boolean field")]
        public void WorkflowDetailsSection_TestMethod169()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 168";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321";

            Guid regardingID = new Guid("1b18dee7-e41e-464b-afe4-50070253de4b");
            string regardingName = "Adrian Abbott";



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "IsCaseNote");
            Assert.AreEqual(false, fields.ContainsKey("IsCaseNote".ToLower()));
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7854")]
        [Description("Automation Script for the Test Case - Scenario 170 - Custom Fields Tool - Extract boolean field")]
        public void WorkflowDetailsSection_TestMethod170()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 170";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321";

            Guid regardingID = personId;
            string regardingName = personFullName;




            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "PhoneCallDate");
            Assert.AreEqual(new DateTime(2000, 1, 2), (DateTime)fields["PhoneCallDate".ToLower()]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7855")]
        [Description("Automation Script for the Test Case - Scenario 170.1 - Custom Fields Tool - Extract boolean field")]
        public void WorkflowDetailsSection_TestMethod170_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);
            dbHelper.person.UpdateDOBAndAgeTypeId(personId, 3, null, 19); //Estmated age

            //ARRANGE 
            string subject = "WF Testing - Scenario 170";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321";

            Guid regardingID = new Guid("1b18dee7-e41e-464b-afe4-50070253de4b");
            string regardingName = "Adrian Abbott";





            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "PhoneCallDate");
            Assert.AreEqual(false, fields.ContainsKey("PhoneCallDate".ToLower()));
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7856")]
        [Description("Automation Script for the Test Case - Scenario 171 - Custom Fields Tool - Append to existing")]
        public void WorkflowDetailsSection_TestMethod171()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 171";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321";

            Guid regardingID = personId;
            string regardingName = personFullName;



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("Sample Description ...WF Testing - Scenario 171", (string)fields["notes".ToLower()]);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7857")]
        [Description("Automation Script for the Test Case - Scenario 172 - Custom Fields Tool - Prepend to existing")]
        public void WorkflowDetailsSection_TestMethod172()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 172";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321";

            Guid regardingID = personId;
            string regardingName = personFullName;




            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("WF Testing - Scenario 172Sample Description ...", (string)fields["notes".ToLower()]);
        }




        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7858")]
        [Description("Automation Script for the Test Case - Scenario 173 - Custom Fields Tool - Set to Before")]
        public void WorkflowDetailsSection_TestMethod173()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId, new DateTime(2015, 1, 6));


            //ARRANGE 
            string subject = "WF Testing - Scenario 173";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321";

            Guid regardingID = personId;
            string regardingName = personFullName;




            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            DateTime expectedPhoneCallDate = new DateTime(2015, 1, 6).AddMonths(-2).AddDays(-3).AddHours(-4).AddMinutes(-5);
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "PhoneCallDate");
            Assert.AreEqual(expectedPhoneCallDate, (DateTime)fields["PhoneCallDate".ToLower()]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7859")]
        [Description("Automation Script for the Test Case - Scenario 173.1 - Custom Fields Tool - Set to Before (person has no DOB)")]
        public void WorkflowDetailsSection_TestMethod173_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);
            dbHelper.person.UpdateDOBAndAgeTypeId(personId, 3, null, 19); //Estmated age

            //ARRANGE 
            string subject = "WF Testing - Scenario 173";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321";

            Guid regardingID = personId;
            string regardingName = personFullName;



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "PhoneCallDate");
            Assert.AreEqual(false, fields.ContainsKey("PhoneCallDate".ToLower()));
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7860")]
        [Description("Automation Script for the Test Case - Scenario 174 - Custom Fields Tool - Set to After")]
        public void WorkflowDetailsSection_TestMethod174()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId, new DateTime(2015, 1, 6));


            //ARRANGE 
            string subject = "WF Testing - Scenario 174";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321";

            Guid regardingID = personId;
            string regardingName = personFullName;



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            DateTime expectedPhoneCallDate = new DateTime(2015, 1, 6).AddMonths(2).AddDays(3).AddHours(4).AddMinutes(5);
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "PhoneCallDate");
            Assert.AreEqual(expectedPhoneCallDate, (DateTime)fields["PhoneCallDate".ToLower()]);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7861")]
        [Description("Automation Script for the Test Case - Scenario 174.1 - Custom Fields Tool - Set to After")]
        public void WorkflowDetailsSection_TestMethod174_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);
            dbHelper.person.UpdateDOBAndAgeTypeId(personId, 3, null, 19); //Estmated age


            //ARRANGE 
            string subject = "WF Testing - Scenario 174";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321";

            Guid regardingID = personId;
            string regardingName = personFullName;



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "PhoneCallDate");
            Assert.AreEqual(false, fields.ContainsKey("PhoneCallDate".ToLower()));
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7862")]
        [Description("Automation Script for the Test Case - Scenario 175 - Custom Fields Tool - Set to Concatenation result")]
        public void WorkflowDetailsSection_TestMethod175()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 175";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321";

            Guid regardingID = personId;
            string regardingName = personFullName;




            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("Primary - UK Local Authority Social Care", (string)fields["notes".ToLower()]);
        }







        [TestProperty("JiraIssueID", "CDV6-7863")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 176 - Custom Fields Tool - Increment By")]
        public void WorkflowDetailsSection_TestMethod176()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool 2", "WF Automated Testing - Testing the Custom Fields Tool 2.Zip");

            //ARRANGE 
            var currentDate = DateTime.Now.ToString("yyyyMMdd.HHmmss");
            var activityCategoryName = "WF Testing - Scenario 176." + currentDate;
            int? code = 2;
            int? importcreatesequence = 3;


            //ACT
            var activityCategoryId = dbHelper.activityCategory.CreateActivityCategory(activityCategoryName, DateTime.Now.Date, _defaultTeamId, code, importcreatesequence);



            //ASSERT
            var fields = dbHelper.activityCategory.GetActivityCategoryByID(activityCategoryId, "code");
            Assert.AreEqual(5, (int)fields["code".ToLower()]);
        }

        [TestProperty("JiraIssueID", "CDV6-7864")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 176.1 - Custom Fields Tool - Increment By")]
        public void WorkflowDetailsSection_TestMethod176_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool 2", "WF Automated Testing - Testing the Custom Fields Tool 2.Zip");

            //ARRANGE 
            var currentDate = DateTime.Now.ToString("yyyyMMdd.HHmmss");
            var activityCategoryName = "WF Testing - Scenario 176." + currentDate;
            int? code = 2;
            int? importcreatesequence = null;


            //ACT
            var activityCategoryId = dbHelper.activityCategory.CreateActivityCategory(activityCategoryName, DateTime.Now.Date, _defaultTeamId, code, importcreatesequence);



            //ASSERT
            var fields = dbHelper.activityCategory.GetActivityCategoryByID(activityCategoryId, "code");
            Assert.AreEqual(2, (int)fields["code".ToLower()]);
        }






        [TestProperty("JiraIssueID", "CDV6-7865")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 177 - Custom Fields Tool - Decrement By")]
        public void WorkflowDetailsSection_TestMethod177()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool 2", "WF Automated Testing - Testing the Custom Fields Tool 2.Zip");

            //ARRANGE 
            var currentDate = DateTime.Now.ToString("yyyyMMdd.HHmmss");
            var activityCategoryName = "WF Testing - Scenario 177." + currentDate;
            int? code = 2;
            int? importcreatesequence = 3;


            //ACT
            var activityCategoryId = dbHelper.activityCategory.CreateActivityCategory(activityCategoryName, DateTime.Now.Date, _defaultTeamId, code, importcreatesequence);



            //ASSERT
            var fields = dbHelper.activityCategory.GetActivityCategoryByID(activityCategoryId, "code");
            Assert.AreEqual(-1, (int)fields["code".ToLower()]);
        }

        [TestProperty("JiraIssueID", "CDV6-7866")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 177.1 - Custom Fields Tool - Decrement By")]
        public void WorkflowDetailsSection_TestMethod177_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool 2", "WF Automated Testing - Testing the Custom Fields Tool 2.Zip");

            //ARRANGE 
            var currentDate = DateTime.Now.ToString("yyyyMMdd.HHmmss");
            var activityCategoryName = "WF Testing - Scenario 177." + currentDate;
            int? code = 2;
            int? importcreatesequence = null;


            //ACT
            var activityCategoryId = dbHelper.activityCategory.CreateActivityCategory(activityCategoryName, DateTime.Now.Date, _defaultTeamId, code, importcreatesequence);



            //ASSERT
            var fields = dbHelper.activityCategory.GetActivityCategoryByID(activityCategoryId, "code");
            Assert.AreEqual(2, (int)fields["code".ToLower()]);
        }





        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7867")]
        [Description("Automation Script for the Test Case - Scenario 178 - Custom Fields Tool - Multiply By")]
        public void WorkflowDetailsSection_TestMethod178()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool 2", "WF Automated Testing - Testing the Custom Fields Tool 2.Zip");

            //ARRANGE 
            var currentDate = DateTime.Now.ToString("yyyyMMdd.HHmmss");
            var activityCategoryName = "WF Testing - Scenario 178." + currentDate;
            int? code = 2;
            int? importcreatesequence = 3;


            //ACT
            var activityCategoryId = dbHelper.activityCategory.CreateActivityCategory(activityCategoryName, DateTime.Now.Date, _defaultTeamId, code, importcreatesequence);



            //ASSERT
            var fields = dbHelper.activityCategory.GetActivityCategoryByID(activityCategoryId, "code");
            Assert.AreEqual(6, (int)fields["code".ToLower()]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7868")]
        [Description("Automation Script for the Test Case - Scenario 178.1 - Custom Fields Tool - Multiply By")]
        public void WorkflowDetailsSection_TestMethod178_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool 2", "WF Automated Testing - Testing the Custom Fields Tool 2.Zip");

            //ARRANGE 
            var currentDate = DateTime.Now.ToString("yyyyMMdd.HHmmss");
            var activityCategoryName = "WF Testing - Scenario 178." + currentDate;
            int? code = 2;
            int? importcreatesequence = null;


            //ACT
            var activityCategoryId = dbHelper.activityCategory.CreateActivityCategory(activityCategoryName, DateTime.Now.Date, _defaultTeamId, code, importcreatesequence);



            //ASSERT
            var fields = dbHelper.activityCategory.GetActivityCategoryByID(activityCategoryId, "code");
            Assert.AreEqual(0, (int)fields["code".ToLower()]);
        }






        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7869")]
        [Description("Automation Script for the Test Case - Scenario 179 - Custom Fields Tool - Test to the 'FirstNotNull' behaviour")]
        public void WorkflowDetailsSection_TestMethod179()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 179";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321";
            DateTime phoneCallDate = DateTime.Now.WithoutMilliseconds();

            Guid regardingID = personId;
            string regardingName = personFullName;



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("0987654321", (string)fields["notes".ToLower()]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7870")]
        [Description("Automation Script for the Test Case - Scenario 180 - Custom Fields Tool - Test to the 'FirstNotNull' behaviour")]
        public void WorkflowDetailsSection_TestMethod180()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 179";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321";
            DateTime? phoneCallDate = null;

            Guid regardingID = personId;
            string regardingName = personFullName;



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("0987654321", (string)fields["notes".ToLower()]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7871")]
        [Description("Automation Script for the Test Case - Scenario 181 - Custom Fields Tool - Test to the 'FirstNotNull' behaviour")]
        public void WorkflowDetailsSection_TestMethod181()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 179";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "";
            DateTime? phoneCallDate = DateTime.UtcNow.WithoutMilliseconds();

            Guid regardingID = personId;
            string regardingName = personFullName;



            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual(phoneCallDate.Value.ToString(), (string)fields["notes".ToLower()]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7872")]
        [Description("Automation Script for the Test Case - Scenario 182 - Custom Fields Tool - Test to the 'FirstNotNull' behaviour")]
        public void WorkflowDetailsSection_TestMethod182()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Testing the Custom Fields Tool", "WF Automated Testing - Testing the Custom Fields Tool.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 179";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "";
            DateTime? phoneCallDate = null;

            Guid regardingID = personId;
            string regardingName = personFullName;


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "notes");
            Assert.AreEqual("Sample Description ...", fields["notes".ToLower()]);
        }

        #region Bugs

        /// <summary>
        /// When we create an update action in a workflow some fields are filled by default.
        /// In the case of Phone Call records all bolean fields and the Direction field (picklist) have values by default.
        /// This means even that we are forced to update those fields even if we don´t want to.
        /// If a WF action is triggered all booleans will be changed to the default values ("No"), and the Direction field will be changed to "Incoming"
        /// </summary>
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7881")]
        [Description("Automation Script for the Test Case - Bug 1 - Validate the fix for the bug that forced updates on boolean fields, even when those fields were not meant to be updated in the WF Update action")]
        public void WorkflowDetailsSection_Bugs_TestMethod1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Bugs", "WF Automated Testing - Bugs.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            var significantEventCategoryId = commonWorkflowMethods.CreateSignificantEventCategoryIfNeeded("Category 1", DateTime.Now, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Testing - Bug 1";
            string description = "Sample Description ...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321";

            Guid regardingID = personId;
            string regardingName = personFullName;

            DateTime phoneCallDate = DateTime.Now.WithoutMilliseconds().Date;
            bool informationByThirdParty = true;
            bool isCaseNote = true;
            bool isSignificantEvent = true;
            DateTime SignificantEventDate = new DateTime(2019, 3, 1);


            //ACT

            //create the records
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, informationByThirdParty, 2, isSignificantEvent, SignificantEventDate, significantEventCategoryId, isCaseNote);


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "DirectionId", "InformationByThirdParty", "IsCaseNote", "IsSignificantEvent", "SignificantEventDate", "SignificantEventCategoryId");
            Assert.AreEqual(2, (int)fields["DirectionId".ToLower()]);
            Assert.AreEqual(true, (bool)fields["InformationByThirdParty".ToLower()]);
            Assert.AreEqual(true, (bool)fields["IsCaseNote".ToLower()]);
            Assert.AreEqual(true, (bool)fields["IsSignificantEvent".ToLower()]);
            Assert.AreEqual(SignificantEventDate, (DateTime)fields["SignificantEventDate".ToLower()]);
            Assert.AreEqual(significantEventCategoryId.ToString(), fields["SignificantEventCategoryId".ToLower()].ToString());
        }



        /// <summary>
        /// There was an issue identified by Andy when he created a workkflow in the test environment to automatically create Case records when a user create a Person Contact
        /// Andy workflow was called "Contact to Case auto complete"
        /// 
        /// If Contact Status Equals [Accepted] And Contact Outcome Equals [Community Outcome] And Date/Time Contact Accepted Contains Data Then
        ///         Create Record: 'Case'
        /// 
        /// "Description" : "when contact is accepted it creates the case record"
        /// 
        /// 
        /// 
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7882")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Bug 2 - Validate the fix for the bugs identified by Andy - WF should create a case record when a new person contact is created in the system")]
        public void WorkflowDetailsSection_Bugs_TestMethod2_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Bugs 2", "WF Automated Testing - Bugs 2.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);




            //ARRANGE 
            Guid ContactStatusId = commonWorkflowMethods.CreateCreateContactStatusIfNeeded(new Guid("d4c87543-7384-ea11-a2cd-005056926fe4"), "Accept First Response Referral", "999", _defaultTeamId, 4, true);
            Guid ContactOutcomeId = commonWorkflowMethods.CreateCreateContactOutcomeIfNeeded(new Guid("be16eda6-a077-e911-a2c5-005056926fe4"), _defaultTeamId, "Community Outcome", true, true, 2);
            Guid ContactTypeId = commonWorkflowMethods.CreateCreateContactTypeIfNeeded("First Response", _defaultTeamId);
            Guid ContactSourceId = commonWorkflowMethods.CreateContactSourceIfNeeded("Friend", _defaultTeamId);
            Guid ContactReasonId = commonWorkflowMethods.CreateContactReasonIfNeeded("Ongoing Psychosis", _defaultTeamId);
            Guid ContactPresentingPriorityId = commonWorkflowMethods.CreateContactPresentingPriorityNeeded("Other", _defaultTeamId);

            Guid RegardingId = personId;
            string regardingidtablename = "person";
            string regardingidname = personFullName;

            string PresentingNeed = "pn ....";
            string ContactSummary = "WF Testing - Bug 2";

            DateTime contactRecievedDateTime = DateTime.Now.WithoutMilliseconds().AddHours(-8);
            DateTime contactAssignedDateTime = contactRecievedDateTime.AddHours(3);




            //ACT
            dbHelper.contact.CreateContact(_defaultTeamId, personId, ContactTypeId, ContactReasonId, ContactPresentingPriorityId,
                ContactStatusId, userid, RegardingId, regardingidtablename, regardingidname,
                contactRecievedDateTime, PresentingNeed, ContactSummary, 1, 1, DateTime.Now.Date, ContactOutcomeId);


            //ASSERT
            List<Guid> cases = dbHelper.Case.GetActiveCasesByPersonID(RegardingId);
            Assert.AreEqual(1, cases.Count());

            var caseFields = dbHelper.Case.GetCaseById(cases[0], "OwnerId", "PersonId", "InitialContactId", "ContactReceivedDateTime", "ContactReceivedById", "RequestReceivedDateTime", "ResponsibleUserId", "ContactReasonId", "PresentingPriorityId", "PresentingNeedDetails", "AdditionalInformation", "ContactSourceId", "ContactMadeById", "ContactMadeByIdTableName", "ContactMadeByIdName", "ContactMadeByName", "PersonAwareOfContactId", "NextOfKinAwareOfContactId", "communityandclinicteamid", "ServiceTypeRequestedId", "CaseStatusId");

            Assert.AreEqual(_defaultTeamId.ToString(), caseFields["OwnerId".ToLower()].ToString());
            Assert.AreEqual(RegardingId.ToString(), caseFields["PersonId".ToLower()].ToString());

        }


        /// <summary>
        /// There was an issue identified by Andy when he created a workkflow in the test environment to automatically create person forms and case actions when a case record was accepted
        /// Workflow was async and had conditions who compare integer values
        /// 
        /// 
        /// </summary>
        [TestProperty("JiraIssueID", "CDV6-7884")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Bug 3 - Validate the fix for the bugs identified by Andy - Async workflows with conditions that compare integer values were failing")]
        public void WorkflowDetailsSection_Bugs_TestMethod3_1()
        {
            //ARRANGE 
            var documentID = commonWorkflowMethods.CreateDocumentIfNeeded("Person Initial Assessment", "Person Initial Assessment.Zip");
            var workflowId = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Bugs 3", "WF Automated Testing - Bugs 3.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            var _caseActionTypeId = commonWorkflowMethods.CreateCaseActionTypeIfNeeded(new Guid("a1dce1ba-5c75-e911-a2c5-005056926fe4"), "Core Assessment", _defaultTeamId, 110000000);
            var _contactSourceId = commonWorkflowMethods.CreateContactSourceIfNeeded("Friend", _defaultTeamId);
            var _contactReasonId = commonWorkflowMethods.CreateContactReasonIfNeeded("Ongoing Psychosis", _defaultTeamId);
            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var dataFormId = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();


            //ACT
            var contactReceivedDateTime = new DateTime(2021, 11, 10);
            var _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, _contactReasonId, dataFormId, _contactSourceId, contactReceivedDateTime, new DateTime(2021, 11, 11), 20, "WF Testing - Bug 3");



            //ASSERT
            var caseFields = dbHelper.Case.GetCaseById(_caseId, "title");
            string caseTitle = (string)caseFields["title".ToLower()];

            var formIDs = dbHelper.personForm.GetPersonFormByPersonID(personId);
            var caseActions = dbHelper.caseAction.GetCaseActionByCaseID(_caseId);
            var caseNotes = dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personId);
            int totalCount = 0;

            while (formIDs.Count < 1 || caseActions.Count < 1 || caseNotes.Count < 1)
            {
                totalCount++;

                if (totalCount > 120)
                    Assert.Fail("It took more than 75 seconds for the workflow to execute");

                System.Threading.Thread.Sleep(1000);

                formIDs = dbHelper.personForm.GetPersonFormByPersonID(personId);
                caseActions = dbHelper.caseAction.GetCaseActionByCaseID(_caseId);
                caseNotes = dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personId);
            }


            Assert.AreEqual(1, formIDs.Count);
            Assert.AreEqual(1, caseActions.Count);
            Assert.AreEqual(1, caseNotes.Count);


            var personFormFields = dbHelper.personForm.GetPersonFormByID(formIDs[0], "documentid", "assessmentstatusid", "ownerid");
            Assert.AreEqual(documentID.ToString(), personFormFields["documentid"].ToString());
            Assert.AreEqual(1, (int)personFormFields["assessmentstatusid".ToString()]);
            Assert.AreEqual(_defaultTeamId.ToString(), personFormFields["ownerid"].ToString());


            var teamName = dbHelper.team.GetTeamByID(_defaultTeamId, "name")["name"];

            var caseActionFields = dbHelper.caseAction.GetCaseActionByID(caseActions[0], "personid", "caseactiontypeid", "duedate", "actiondetails");
            Assert.AreEqual(personId.ToString(), caseActionFields["personid"].ToString());
            Assert.AreEqual(_caseActionTypeId.ToString(), caseActionFields["caseactiontypeid"].ToString());
            Assert.AreEqual(new DateTime(2019, 5, 23), (DateTime)caseActionFields["duedate".ToString()]);
            string expectedActionDetails = string.Format("{0} <-> {1} <-> {2} <-> {3}", caseTitle, teamName, "", contactReceivedDateTime.ToString());
            Assert.AreEqual(expectedActionDetails, caseActionFields["actiondetails"]);



            var casenoteFields = dbHelper.personCaseNote.GetPersonCaseNoteByID(caseNotes[0], "subject", "casenotedate");
            Assert.AreEqual(caseTitle + " <-> Allocate to Team", (string)casenoteFields["subject".ToString()]);
            Assert.AreEqual(contactReceivedDateTime.ToUniversalTime().ToString(), ((DateTime)casenoteFields["casenotedate"]).ToUniversalTime().ToString());

        }


        #endregion


        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7873")]
        [Description("Automation Script for the Test Case - Scenario 183 - Testing the latest changes to the to let the users set a lookup field to a MultiSelectMultiLookup field (e.g. set a PersonId to the Email’s “To” field when using the Send Email Activity)")]
        public void WorkflowDetailsSection_TestMethod183()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Set a lookup field to a MultiSelectMultiLookup Tests & Date and Datetime related tests", "WF Automated Testing - Set a lookup field to a MultiSelectMultiLookup Tests and Date and Datetime related tests.Zip");

            var Workflow_Test_User_60_Userid = commonWorkflowMethods.CreateSystemUserRecord("Workflow_Test_User_60", "Workflow", "Test User 60", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);
            dbHelper.person.UpdatePrimaryEmail(personId, firstName + lastName + "@somemail.com");


            //ARRANGE 
            string subject = "WF Testing - Scenario 183";
            string description = "WF Testing - Scenario 183";

            Guid callerID = Workflow_Test_User_60_Userid;
            string callerIdTableName = "systemuser";
            string callerIDName = "Workflow Test User 60";

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;


            //ACT


            //create the record
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT

            //get all Email records for the person
            var emailIDs = dbHelper.email.GetEmailByRegardingID(regardingID);
            Assert.AreEqual(1, emailIDs.Count);

            //get the email info
            var email = dbHelper.email.GetEmailByID(emailIDs[0], "Subject", "RegardingID", "RegardingIDName", "RegardingIdTableName", "EmailFromLookupId");

            Assert.AreEqual("WF Testinng - Scenario 183 - Action 1 Activated", (string)email["subject".ToLower()]);
            Assert.AreEqual(callerID.ToString(), email["EmailFromLookupId".ToLower()].ToString());
            Assert.AreEqual(regardingID.ToString(), email["RegardingID".ToLower()].ToString());
            Assert.AreEqual(regardingName, (string)email["RegardingIDName".ToLower()]);
            Assert.AreEqual("person", (string)email["RegardingIdTableName".ToLower()]);

            List<Guid> emailToID = dbHelper.emailTo.GetByEmailID(emailIDs[0]);
            Assert.AreEqual(1, emailToID.Count);

            var emailTo = dbHelper.emailTo.GetByID(emailToID[0], "RegardingId", "RegardingIdTableName", "RegardingIdName");
            Assert.AreEqual(regardingID.ToString(), emailTo["RegardingId".ToLower()].ToString());
            Assert.AreEqual(regardingName, (string)emailTo["RegardingIdName".ToLower()]);
            Assert.AreEqual("person", (string)emailTo["RegardingIdTableName".ToLower()]);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7874")]
        [Description("Automation Script for the Test Case - Scenario 184 - Testing the latest changes to the to let the users set a lookup field to a MultiSelectMultiLookup field (e.g. set a PersonId to the Email’s “To” field when using the Send Email Activity). In this scenario the phone call caller field is set to a person record")]
        public void WorkflowDetailsSection_TestMethod184_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Set a lookup field to a MultiSelectMultiLookup Tests & Date and Datetime related tests", "WF Automated Testing - Set a lookup field to a MultiSelectMultiLookup Tests and Date and Datetime related tests.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);
            dbHelper.person.UpdatePrimaryEmail(personId, firstName + lastName + "@somemail.com");
            dbHelper.person.UpdateAllowEmail(personId, true);

            //ARRANGE 
            string subject = "WF Testing - Scenario 184";
            string description = "WF Testing - Scenario 184";

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
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT

            //get all Email records for the person
            var emailIDs = dbHelper.email.GetEmailByRegardingID(regardingID);
            Assert.AreEqual(1, emailIDs.Count);

            //get the email info
            var email = dbHelper.email.GetEmailByID(emailIDs[0], "Subject", "RegardingID", "RegardingIDName", "RegardingIdTableName", "EmailFromLookupId");

            Assert.AreEqual("WF Testinng - Scenario 184 - Action 1 Activated", (string)email["subject".ToLower()]);
            Assert.AreEqual(recipientID.ToString(), email["EmailFromLookupId".ToLower()].ToString());
            Assert.AreEqual(regardingID.ToString(), email["RegardingID".ToLower()].ToString());
            Assert.AreEqual(regardingName, (string)email["RegardingIDName".ToLower()]);
            Assert.AreEqual("person", (string)email["RegardingIdTableName".ToLower()]);


            List<Guid> emailToID = dbHelper.emailTo.GetByEmailID(emailIDs[0]);
            Assert.AreEqual(1, emailToID.Count);

            var emailTo = dbHelper.emailTo.GetByID(emailToID[0], "RegardingId", "RegardingIdTableName", "RegardingIdName");
            Assert.AreEqual(callerID.ToString(), emailTo["RegardingId".ToLower()].ToString());
            Assert.AreEqual(callerIDName, (string)emailTo["RegardingIdName".ToLower()]);
            Assert.AreEqual(callerIdTableName, (string)emailTo["RegardingIdTableName".ToLower()]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7875")]
        [Description("Automation Script for the Test Case - Scenario 184 - Testing the latest changes to the to let the users set a lookup field to a MultiSelectMultiLookup field (e.g. set a PersonId to the Email’s “To” field when using the Send Email Activity). In this scenario the phone call caller field is set to a provider record")]
        public void WorkflowDetailsSection_TestMethod184_2()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Set a lookup field to a MultiSelectMultiLookup Tests & Date and Datetime related tests", "WF Automated Testing - Set a lookup field to a MultiSelectMultiLookup Tests and Date and Datetime related tests.Zip");

            var providerId = commonWorkflowMethods.CreateProviderIfNeeded("Ynys Mon - Mental Health - Provider - 7875", _defaultTeamId, "YnysMon-MentalHealth-Provider7875@mail.com");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Scenario 184";
            string description = "WF Testing - Scenario 184";

            Guid callerID = providerId;
            string callerIdTableName = "provider";
            string callerIDName = "Ynys Mon - Mental Health - Provider - 7875";

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;


            //ACT


            //create the record
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT

            //get all Email records for the person
            var emailIDs = dbHelper.email.GetEmailByRegardingID(regardingID);
            Assert.AreEqual(1, emailIDs.Count);

            //get the email info
            var email = dbHelper.email.GetEmailByID(emailIDs[0], "Subject", "RegardingID", "RegardingIDName", "RegardingIdTableName", "EmailFromLookupId");

            Assert.AreEqual("WF Testinng - Scenario 184 - Action 1 Activated", (string)email["subject".ToLower()]);
            Assert.AreEqual(recipientID.ToString(), email["EmailFromLookupId".ToLower()].ToString());
            Assert.AreEqual(regardingID.ToString(), email["RegardingID".ToLower()].ToString());
            Assert.AreEqual(regardingName, (string)email["RegardingIDName".ToLower()]);
            Assert.AreEqual("person", (string)email["RegardingIdTableName".ToLower()]);


            List<Guid> emailToID = dbHelper.emailTo.GetByEmailID(emailIDs[0]);
            Assert.AreEqual(1, emailToID.Count);

            var emailTo = dbHelper.emailTo.GetByID(emailToID[0], "RegardingId", "RegardingIdTableName", "RegardingIdName");
            Assert.AreEqual(callerID.ToString(), emailTo["RegardingId".ToLower()].ToString());
            Assert.AreEqual(callerIDName, (string)emailTo["RegardingIdName".ToLower()]);
            Assert.AreEqual(callerIdTableName, (string)emailTo["RegardingIdTableName".ToLower()]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7876")]
        [Description("Automation Script for the Test Case - Scenario 184 - Testing the latest changes to the to let the users set a lookup field to a MultiSelectMultiLookup field (e.g. set a PersonId to the Email’s “To” field when using the Send Email Activity). In this scenario the phone call caller field is set to a professional record")]
        public void WorkflowDetailsSection_TestMethod184_3()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Set a lookup field to a MultiSelectMultiLookup Tests & Date and Datetime related tests", "WF Automated Testing - Set a lookup field to a MultiSelectMultiLookup Tests and Date and Datetime related tests.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            var professionTypeId = dbHelper.professionType.GetByName("Care Coordinator").First();
            var professionalEmail = "Anthony" + lastName + "@mail.com";
            var professionalID = dbHelper.professional.CreateProfessional(_defaultTeamId, professionTypeId, "", "Anthony", lastName, professionalEmail);

            //ARRANGE 
            string subject = "WF Testing - Scenario 184";
            string description = "WF Testing - Scenario 184";

            Guid callerID = professionalID;
            string callerIdTableName = "professional";
            string callerIDName = "Anthony " + lastName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;

            //ACT


            //create the record
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT

            //get all Email records for the person
            var emailIDs = dbHelper.email.GetEmailByRegardingID(regardingID);
            Assert.AreEqual(1, emailIDs.Count);

            //get the email info
            var email = dbHelper.email.GetEmailByID(emailIDs[0], "Subject", "RegardingID", "RegardingIDName", "RegardingIdTableName", "EmailFromLookupId");

            Assert.AreEqual("WF Testinng - Scenario 184 - Action 1 Activated", (string)email["subject".ToLower()]);
            Assert.AreEqual(recipientID.ToString(), email["EmailFromLookupId".ToLower()].ToString());
            Assert.AreEqual(regardingID.ToString(), email["RegardingID".ToLower()].ToString());
            Assert.AreEqual(regardingName, (string)email["RegardingIDName".ToLower()]);
            Assert.AreEqual("person", (string)email["RegardingIdTableName".ToLower()]);


            List<Guid> emailToID = dbHelper.emailTo.GetByEmailID(emailIDs[0]);
            Assert.AreEqual(1, emailToID.Count);

            var emailTo = dbHelper.emailTo.GetByID(emailToID[0], "RegardingId", "RegardingIdTableName", "RegardingIdName");
            Assert.AreEqual(callerID.ToString(), emailTo["RegardingId".ToLower()].ToString());
            Assert.AreEqual(callerIDName, (string)emailTo["RegardingIdName".ToLower()]);
            Assert.AreEqual(callerIdTableName, (string)emailTo["RegardingIdTableName".ToLower()]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7877")]
        [Description("Automation Script for the Test Case - Scenario 184 - Testing the latest changes to the to let the users set a lookup field to a MultiSelectMultiLookup field (e.g. set a PersonId to the Email’s “To” field when using the Send Email Activity). In this scenario the phone call caller field is set to a system user record")]
        public void WorkflowDetailsSection_TestMethod184_4()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Set a lookup field to a MultiSelectMultiLookup Tests & Date and Datetime related tests", "WF Automated Testing - Set a lookup field to a MultiSelectMultiLookup Tests and Date and Datetime related tests.Zip");

            var Workflow_Test_User_70_Userid = commonWorkflowMethods.CreateSystemUserRecord("Workflow_Test_User_70", "Workflow", "Test User 70", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 184";
            string description = "WF Testing - Scenario 184";

            Guid callerID = Workflow_Test_User_70_Userid;
            string callerIdTableName = "systemuser";
            string callerIDName = "Workflow Test User 70";

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;


            //ACT


            //create the record
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT

            //get all Email records for the person
            var emailIDs = dbHelper.email.GetEmailByRegardingID(regardingID);
            Assert.AreEqual(1, emailIDs.Count);

            //get the email info
            var email = dbHelper.email.GetEmailByID(emailIDs[0], "Subject", "RegardingID", "RegardingIDName", "RegardingIdTableName", "EmailFromLookupId");
            Assert.AreEqual("WF Testinng - Scenario 184 - Action 1 Activated", (string)email["subject".ToLower()]);
            Assert.AreEqual(recipientID.ToString(), email["EmailFromLookupId".ToLower()].ToString());
            Assert.AreEqual(regardingID.ToString(), email["RegardingID".ToLower()].ToString());
            Assert.AreEqual(regardingName, (string)email["RegardingIDName".ToLower()]);
            Assert.AreEqual("person", (string)email["RegardingIdTableName".ToLower()]);


            List<Guid> emailToID = dbHelper.emailTo.GetByEmailID(emailIDs[0]);
            Assert.AreEqual(1, emailToID.Count);

            var emailTo = dbHelper.emailTo.GetByID(emailToID[0], "RegardingId", "RegardingIdTableName", "RegardingIdName");
            Assert.AreEqual(callerID.ToString(), emailTo["RegardingId".ToLower()].ToString());
            Assert.AreEqual(callerIDName, (string)emailTo["RegardingIdName".ToLower()]);
            Assert.AreEqual(callerIdTableName, (string)emailTo["RegardingIdTableName".ToLower()]);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-7878")]
        [Description("Automation Script for the Test Case - Scenario 185 - Testing the latest changes to the to let the users set a lookup field to a MultiSelectMultiLookup field (e.g. set a PersonId to the Email’s “To” field when using the Send Email Activity). In this scenario the To field (email) will be set using a Responsible User reference (phone call)")]
        public void WorkflowDetailsSection_TestMethod185()
        {
            dbHelper.team.UpdateEmailAddress(_defaultTeamId, "UKLocalAuthoritySocialCare@mainmail.com");

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Set a lookup field to a MultiSelectMultiLookup Tests & Date and Datetime related tests", "WF Automated Testing - Set a lookup field to a MultiSelectMultiLookup Tests and Date and Datetime related tests.Zip");

            var Workflow_Test_User_80_Userid = commonWorkflowMethods.CreateSystemUserRecord("Workflow_Test_User_80", "Workflow", "Test User 80", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            //ARRANGE 
            string subject = "WF Testing - Scenario 185";
            string description = "WF Testing - Scenario 185";

            Guid callerID = Workflow_Test_User_80_Userid;
            string callerIdTableName = "systemuser";
            string callerIDName = "Workflow Test User 80";

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;


            //ACT


            //create the record
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, DateTime.Now.WithoutMilliseconds());


            //ASSERT

            //get all Email records for the person
            var emailIDs = dbHelper.email.GetEmailByRegardingID(regardingID);
            Assert.AreEqual(1, emailIDs.Count);

            //get the email info
            var email = dbHelper.email.GetEmailByID(emailIDs[0], "Subject", "RegardingID", "RegardingIDName", "RegardingIdTableName", "EmailFromLookupId");

            Assert.AreEqual("WF Testinng - Scenario 185 - Action 1 Activated", (string)email["subject".ToLower()]);
            Assert.AreEqual(callerID.ToString(), email["EmailFromLookupId".ToLower()].ToString());
            Assert.AreEqual(regardingID.ToString(), email["RegardingID".ToLower()].ToString());
            Assert.AreEqual(regardingName, (string)email["RegardingIDName".ToLower()]);
            Assert.AreEqual("person", (string)email["RegardingIdTableName".ToLower()]);


            List<Guid> emailToID = dbHelper.emailTo.GetByEmailID(emailIDs[0]);
            Assert.AreEqual(1, emailToID.Count);

            var emailTo = dbHelper.emailTo.GetByID(emailToID[0], "RegardingId", "RegardingIdTableName", "RegardingIdName");
            Assert.AreEqual(_defaultTeamId.ToString(), emailTo["RegardingId".ToLower()].ToString());
            Assert.AreEqual("UK Local Authority Social Care", (string)emailTo["RegardingIdName".ToLower()]);
            Assert.AreEqual("team", (string)emailTo["RegardingIdTableName".ToLower()]);
        }



        [TestProperty("JiraIssueID", "CDV6-7880")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 187 - Testing the latest changes allow the set of a datetime field using a date field and the reverse operation (setting a date field using a datetime field). in this scenario the date and datetime fields in the phonecall record have values (not null)")]
        public void WorkflowDetailsSection_TestMethod187_1()
        {
            dbHelper.team.UpdateEmailAddress(_defaultTeamId, "UKLocalAuthoritySocialCare@mainmail.com");

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Set a lookup field to a MultiSelectMultiLookup Tests & Date and Datetime related tests", "WF Automated Testing - Set a lookup field to a MultiSelectMultiLookup Tests and Date and Datetime related tests.Zip");

            var Workflow_Test_User_90_Userid = commonWorkflowMethods.CreateSystemUserRecord("Workflow_Test_User_90", "Workflow", "Test User 90", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);
            dbHelper.person.UpdateAllowEmail(personId, true);
            dbHelper.person.UpdatePrimaryEmail(personId, firstName + lastName + "@mail.com");


            //ARRANGE 
            string subject = "WF Testing - Scenario 186";
            string description = "WF Testing - Scenario 186 - desc....";

            Guid callerID = Workflow_Test_User_90_Userid;
            string callerIdTableName = "systemuser";
            string callerIDName = "Workflow Test User 90";

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;

            DateTime phoneCallDate = new DateTime(2020, 2, 1, 9, 30, 0);
            DateTime eventDate = new DateTime(2020, 2, 3, 0, 0, 0);


            //ACT


            //create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID,
                recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, false, 1, true, eventDate);


            //ASSERT

            //get all Email records for the person
            var emailIDs = dbHelper.email.GetEmailByRegardingID(regardingID);
            Assert.AreEqual(1, emailIDs.Count);

            //get the email info
            var email = dbHelper.email.GetEmailByID(emailIDs[0], "duedate", "significanteventdate");

            Assert.AreEqual(eventDate, (DateTime)email["duedate".ToLower()]);
            Assert.AreEqual(phoneCallDate.Date, (DateTime)email["significanteventdate".ToLower()]);

        }


        [TestProperty("JiraIssueID", "CDV6-21848")]
        [TestMethod]
        [Description("")]
        public void WorkflowDetailsSection_RichTextBoxUpdates_01()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - Rich Text Box Updates", "WF Automated Testing - Rich Text Box Updates.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - Rich Text Editor - Scenario 1";
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

            DateTime PhoneCallDate = new DateTime(2019, 3, 2);

            //ACT

            ////login with a user that do not belongs to Caredirector team
            //dbHelper = new DatabaseHelper("Jose.brazeta", "Passw0rd_!", tenantName);

            //create the phone call record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate, "person");


            //ASSERT
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "notes");
            Assert.AreEqual("<p>Subject:&nbsp;WF Testing - Rich Text Editor - Scenario 1</p>\n\n<p>Phone Number:&nbsp;0987654321234</p>\n\n<p>Recipient:&nbsp;José Brazeta</p>\n\n<p>Caller:&nbsp;" + personFullName + "</p>\n\n<p>Regarding:&nbsp;" + personFullName + "</p>\n\n<p>Phone Call Date:&nbsp;02/03/2019 00:00:00</p>\n\n<p>&nbsp;</p>", fields["notes"]);

        }



        #region https://advancedcsg.atlassian.net/browse/CDV6-8028

        [TestProperty("JiraIssueID", "CDV6-21855")]
        [TestMethod]
        [Description("Automation Script for the defect https://advancedcsg.atlassian.net/browse/CDV6-8028 - Scenario 1 - " +
            "Validate that workflows can now use picklist values in conditions 'Business Data: Get Answer By Identifier: Picklist Answer (Document Pick List Value)' - " +
            "In this scenario the picklist value will match the WF condition")]
        public void WorkflowDetailsSection_TestMethod188()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Person Form Testing - Jira ID CDV6-8028", "WF Person Form Testing - Jira ID CDV6-8028.Zip");
            
            var documentId = commonWorkflowMethods.CreateDocumentIfNeeded("Automation - Person Form 1", "Automation - Person Form 1.Zip");

            var firstName = "CDV6_8028";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            var personFormId = dbHelper.personForm.CreatePersonForm(_defaultTeamId, personId, documentId, DateTime.Now.Date);

            //get the "Jedi" document Pick List Value
            var documentPickListId = dbHelper.documentPickList.GetByName("Religion").FirstOrDefault();
            var documentPickListValueId = dbHelper.documentPickListValue.GetByTextAndDocumentPickListId("Jedi", documentPickListId).FirstOrDefault();

            Guid documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-129").FirstOrDefault();

            //Set the answer for the WF PickList Answer question
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(personFormId, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswer.UpdatePicklistValueAnswer(documentAnswerID, documentPickListValueId);

            //ACT

            //change the status again to trigger the WF
            dbHelper.personForm.UpdatePersonFormStatus(personFormId, 4); //set to Not Started


            //ASSERT

            //get all Email records for the person
            var fields = dbHelper.personForm.GetPersonFormByID(personFormId, "reviewdate");
            Assert.AreEqual(new DateTime(2021, 1, 11), fields["reviewdate"]);
        }

        [TestProperty("JiraIssueID", "CDV6-21856")]
        [TestMethod]
        [Description("Automation Script for the defect https://advancedcsg.atlassian.net/browse/CDV6-8028 - Scenario 1 - " +
            "Validate that workflows can now use picklist values in conditions 'Business Data: Get Answer By Identifier: Picklist Answer (Document Pick List Value)' - " +
            "In this scenario the picklist value will NOT match the WF condition")]
        public void WorkflowDetailsSection_TestMethod188_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Person Form Testing - Jira ID CDV6-8028", "WF Person Form Testing - Jira ID CDV6-8028.Zip");

            var documentId = commonWorkflowMethods.CreateDocumentIfNeeded("Automation - Person Form 1", "Automation - Person Form 1.Zip");

            var firstName = "CDV6_8028";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            var personFormId = dbHelper.personForm.CreatePersonForm(_defaultTeamId, personId, documentId, DateTime.Now.Date);

            //get the "Jedi" document Pick List Value
            var documentPickListId = dbHelper.documentPickList.GetByName("Religion").FirstOrDefault();
            var documentPickListValueId = dbHelper.documentPickListValue.GetByTextAndDocumentPickListId("Atheist", documentPickListId).FirstOrDefault();

            Guid documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-129").FirstOrDefault();

            //Set the answer for the WF PickList Answer question
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(personFormId, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswer.UpdatePicklistValueAnswer(documentAnswerID, documentPickListValueId);


            //ACT

            //change the status again to trigger the WF
            dbHelper.personForm.UpdatePersonFormStatus(personFormId, 4); //set to Not Started


            //ASSERT

            //get all Email records for the person
            var fields = dbHelper.personForm.GetPersonFormByID(personFormId, "reviewdate");
            Assert.IsFalse(fields.ContainsKey("reviewdate"));
        }

        [TestProperty("JiraIssueID", "CDV6-21857")]
        [TestMethod]
        [Description("Automation Script for the defect https://advancedcsg.atlassian.net/browse/CDV6-8028 - Scenario 2 - " +
           "Validate that workflow conditions can now reference the primary key of the record that triggered the update - " +
           "Positive scenario where the record triggering the workflow will match the workflow condition")]
        public void WorkflowDetailsSection_TestMethod188_2()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Person Form Testing - Jira ID CDV6-8028", "WF Person Form Testing - Jira ID CDV6-8028.Zip");

            var documentId = commonWorkflowMethods.CreateDocumentIfNeeded("Automation - Person Form 1", "Automation - Person Form 1.Zip");

            var firstName = "CDV6_8028";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            var personFormId = dbHelper.personForm.CreatePersonForm(_defaultTeamId, personId, documentId, DateTime.Now.Date);

            //get the "Jedi" document Pick List Value
            var documentPickListId = dbHelper.documentPickList.GetByName("Religion").FirstOrDefault();
            var documentPickListValueId = dbHelper.documentPickListValue.GetByTextAndDocumentPickListId("Budist", documentPickListId).FirstOrDefault();

            Guid documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-129").FirstOrDefault();

            //Set the answer for the WF PickList Answer question
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(personFormId, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswer.UpdatePicklistValueAnswer(documentAnswerID, documentPickListValueId);
            //ACT

            //change the status again to trigger the WF
            dbHelper.personForm.UpdatePersonFormStatus(personFormId, 4); //set to Not Started


            //ASSERT

            //get all Email records for the person
            var fields = dbHelper.personForm.GetPersonFormByID(personFormId, "reviewdate");
            Assert.AreEqual(new DateTime(2021, 1, 31), fields["reviewdate"]);
        }

        [TestProperty("JiraIssueID", "CDV6-21858")]
        [TestMethod]
        [Description("Automation Script for the defect https://advancedcsg.atlassian.net/browse/CDV6-8028 - Scenario 2 - " +
           "Validate that workflow conditions can now reference the primary key of the record that triggered the update - " +
           "Positive scenario where the record triggering the workflow will match the workflow condition")]
        public void WorkflowDetailsSection_TestMethod188_3()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Person Form Testing - Jira ID CDV6-8028", "WF Person Form Testing - Jira ID CDV6-8028.Zip");

            var documentId = commonWorkflowMethods.CreateDocumentIfNeeded("Automation - Person Form 1", "Automation - Person Form 1.Zip");

            var firstName = "CDV6_8028";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            var personFormId = dbHelper.personForm.CreatePersonForm(_defaultTeamId, personId, documentId, DateTime.Now.Date);

            //get the "Jedi" document Pick List Value
            var documentPickListId = dbHelper.documentPickList.GetByName("Religion").FirstOrDefault();
            var documentPickListValueId = dbHelper.documentPickListValue.GetByTextAndDocumentPickListId("Atheist", documentPickListId).FirstOrDefault();

            Guid documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-129").FirstOrDefault();

            //Set the answer for the WF PickList Answer question
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(personFormId, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswer.UpdatePicklistValueAnswer(documentAnswerID, documentPickListValueId);


            //ACT

            //change the status again to trigger the WF
            dbHelper.personForm.UpdatePersonFormStatus(personFormId, 4); //set to Not Started


            //ASSERT

            //get all Email records for the person
            var fields = dbHelper.personForm.GetPersonFormByID(personFormId, "reviewdate");
            Assert.IsFalse(fields.ContainsKey("reviewdate"));
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-8283

        [TestProperty("JiraIssueID", "CDV6-21890")]
        [TestMethod]
        [Description("Automation Script for the defect https://advancedcsg.atlassian.net/browse/CDV6-8283 - Scenario 1 - " +
           "Validate that the Responsible team can be set to the default value when using the Create Record action")]
        public void WorkflowDetailsSection_SetOwnerOnCreateAction_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Testing Owner in Create Action - Jira ID CDV6-8283", "WF Testing Owner in Create Action - Jira ID CDV6-8283.Zip");

            var newTeamId = commonWorkflowMethods.CreateTeam("Team CDV6-7662", _defaultBusinessUnitId);

            var user1id = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);
            var user2id = commonWorkflowMethods.CreateSystemUserRecord("User_CDV6_7662", "User", "CDV6-7662", "Passw0rd_!", _defaultBusinessUnitId, newTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            dbHelper.person.UpdateAllowEmail(personId, true);
            dbHelper.person.UpdatePrimaryEmail(personId, firstName + lastName + "@somemail.com");

            Guid wfTeamId = commonWorkflowMethods.CreateTeam(new Guid("4a131548-9666-ed11-a336-005056926fe4"), "CDV6-8283 T1", _defaultBusinessUnitId);

            //ARRANGE 
            string subject = "WF Testing CDV6-8283 Default Owner";
            string description = "desc....";

            Guid callerID = user1id;
            string callerIdTableName = "systemuser";
            string callerIDName = "José Brazeta";

            Guid recipientID = user2id;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "User CDV6-7662";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;

            DateTime phoneCallDate = new DateTime(2020, 2, 1, 9, 30, 0);
            DateTime eventDate = new DateTime(2020, 2, 3, 0, 0, 0);

            //ACT


            //create the record
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID,
                recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, false, 1, true, eventDate);




            //ASSERT

            //get all Phone Call records for the person
            var phoneCalls = dbHelper.phoneCall.GetPhoneCallByPersonID(regardingID, "Workflow Activated - Use default team");
            Assert.AreEqual(1, phoneCalls.Count);

            //validate the ownerid field is set to the default value
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCalls[0], "ownerid");
            Assert.AreEqual(wfTeamId.ToString(), fields["ownerid"].ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-21891")]
        [TestMethod]
        [Description("Automation Script for the defect https://advancedcsg.atlassian.net/browse/CDV6-8283 - Scenario 1 - " +
           "Validate that the Responsible team can be set to the default value when using the Send Email action")]
        public void WorkflowDetailsSection_SetOwnerOnSendEmailAction_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Testing Owner in Create Action - Jira ID CDV6-8283", "WF Testing Owner in Create Action - Jira ID CDV6-8283.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);
            var user2id = commonWorkflowMethods.CreateSystemUserRecord("Test.CDV6.21891", "Test", "CDV6-21891", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            Guid wfTeamId = commonWorkflowMethods.CreateTeam(new Guid("4a131548-9666-ed11-a336-005056926fe4"), "CDV6-8283 T1", _defaultBusinessUnitId);


            //ARRANGE 
            string subject = "WF Testing CDV6-8283 Default Owner";
            string description = "desc....";

            Guid callerID = user2id;
            string callerIdTableName = "systemuser";
            string callerIDName = "Test CDV6-21891";

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;

            DateTime phoneCallDate = new DateTime(2020, 2, 1, 9, 30, 0);
            DateTime eventDate = new DateTime(2020, 2, 3, 0, 0, 0);

            //ACT


            //create the record
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID,
                recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, false, 1, true, eventDate);


            //ASSERT

            //get all Phone Call records for the person
            var emails = dbHelper.email.GetEmailByRegardingID(regardingID);
            Assert.AreEqual(1, emails.Count);

            //validate the ownerid field is set to the default value
            var fields = dbHelper.email.GetEmailByID(emails[0], "ownerid");
            Assert.AreEqual(wfTeamId.ToString(), fields["ownerid"].ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-21893")]
        [TestMethod]
        [Description("Automation Script for the defect https://advancedcsg.atlassian.net/browse/CDV6-8283 - Scenario 2 - " +
           "Validate that the Responsible team can be set to a specified value when using the Create Record action")]
        public void WorkflowDetailsSection_SetOwnerOnCreateAction_2()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Testing Owner in Create Action - Jira ID CDV6-8283", "WF Testing Owner in Create Action - Jira ID CDV6-8283.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);
            var user2id = commonWorkflowMethods.CreateSystemUserRecord("Test.CDV6.21893", "Test", "CDV6-21893", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing CDV6-8283 Set Owner";
            string description = "desc....";

            Guid callerID = user2id;
            string callerIdTableName = "systemuser";
            string callerIDName = "Test CDV6-21893";

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;

            DateTime phoneCallDate = new DateTime(2020, 2, 1, 9, 30, 0);
            DateTime eventDate = new DateTime(2020, 2, 3, 0, 0, 0);


            //create the record
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID,
                recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, false, 1, true, eventDate);


            //ASSERT

            //get all Phone Call records for the person
            var phoneCalls = dbHelper.phoneCall.GetPhoneCallByPersonID(regardingID, "Workflow Activated - Set team");
            Assert.AreEqual(1, phoneCalls.Count);

            //validate the ownerid field is set to the specified value in the create action
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCalls[0], "ownerid");
            Assert.AreEqual(_defaultTeamId.ToString(), fields["ownerid"].ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-21894")]
        [TestMethod]
        [Description("Automation Script for the defect https://advancedcsg.atlassian.net/browse/CDV6-8283 - Scenario 2 - " +
           "Validate that the Responsible team can be set to a specified value when using the Create Record action")]
        public void WorkflowDetailsSection_SetOwnerOnSendEmailAction_2()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Testing Owner in Create Action - Jira ID CDV6-8283", "WF Testing Owner in Create Action - Jira ID CDV6-8283.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);
            var user2id = commonWorkflowMethods.CreateSystemUserRecord("Test.CDV6.21894", "Test", "CDV6-21894", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing CDV6-8283 Set Owner";
            string description = "desc....";

            Guid callerID = user2id;
            string callerIdTableName = "systemuser";
            string callerIDName = "Test CDV6-21894";

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;

            DateTime phoneCallDate = new DateTime(2020, 2, 1, 9, 30, 0);
            DateTime eventDate = new DateTime(2020, 2, 3, 0, 0, 0);


            //create the record
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID,
                recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, false, 1, true, eventDate);



            //ASSERT

            //get all Phone Call records for the person
            var emails = dbHelper.email.GetEmailByRegardingID(regardingID);
            Assert.AreEqual(1, emails.Count);

            //validate the ownerid field is set to the specified value in the create action
            var fields = dbHelper.email.GetEmailByID(emails[0], "ownerid");
            Assert.AreEqual(_defaultTeamId.ToString(), fields["ownerid"].ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-21895")]
        [TestMethod]
        [Description("Automation Script for the defect https://advancedcsg.atlassian.net/browse/CDV6-8283 - Scenario 3 - " +
           "Validate that the Responsible team is not changed in a Update action")]
        public void WorkflowDetailsSection_SetOwnerOnCreateAction_3()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Testing Owner in Create Action - Jira ID CDV6-8283", "WF Testing Owner in Create Action - Jira ID CDV6-8283.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing CDV6-8283 Update";
            string description = "desc....";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;

            DateTime phoneCallDate = new DateTime(2020, 2, 1, 9, 30, 0);
            DateTime eventDate = new DateTime(2020, 2, 3, 0, 0, 0);

            //ACT


            //create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID,
                recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, phoneCallDate, null, null, false, 1, true, eventDate);


            //ASSERT

            //get all Phone Call records for the person
            var phoneCalls = dbHelper.phoneCall.GetPhoneCallByRegardingID(regardingID);
            Assert.AreEqual(1, phoneCalls.Count);

            //validate the ownerid field is set to the specified value in the create action
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCalls[0], "ownerid", "notes");
            Assert.AreEqual(_defaultTeamId.ToString(), fields["ownerid"].ToString());
            Assert.AreEqual("<p>UPDATE</p>", fields["notes"]);

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-8527

        
        [TestProperty("JiraIssueID", "CDV6-21896")]
        [TestMethod]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-8527 - " +
            "Test for the defect identified in CDV6-8527 - Test the Assign of a record that contains a data restriction.")]
        public void WorkflowDetailsSection_AssignRestrictionRecord_TestMethod01()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-8527", "WF Automated Testing - CDV6-8527.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var dataRestrictionId = commonWorkflowMethods.CreateDataRestrictionIfNeeded(new Guid("19752dc1-5d67-ed11-a336-005056926fe4"), "DR CDV6-8527", 1, _defaultTeamId);
            commonWorkflowMethods.CreateUserRestrictedDataAccess(dataRestrictionId, userid, DateTime.Now.Date, _defaultTeamId);
            
            var teamid = commonWorkflowMethods.CreateTeam(new Guid("91f5f232-5e67-ed11-a336-005056926fe4"), "Team CDV6-8527", _defaultBusinessUnitId);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing CDV68527 Scenario 1";
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

            dbHelper = new DatabaseHelper("Jose.brazeta", "Passw0rd_!", tenantName);

            //create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT
            var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "datarestrictionid", "OwnerId", "OwningBusinessUnitId");

            Assert.AreEqual(dataRestrictionId.ToString(), phoneCallfields["DataRestrictionId".ToLower()].ToString());
            Assert.AreEqual(teamid.ToString(), phoneCallfields["OwnerId".ToLower()].ToString());
            Assert.AreEqual(_defaultBusinessUnitId.ToString(), phoneCallfields["OwningBusinessUnitId".ToLower()].ToString());
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-8799

        [TestProperty("JiraIssueID", "CDV6-21917")]
        [TestMethod]
        [Description("Test for the story identified in CDV6-8799 - Allow Team Email to be used in Workflow")]
        public void WorkflowDetailsSection_CDV68799_TestMethod001()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S30", "WF Automated Testing - S30.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);
            
            var teamID = commonWorkflowMethods.CreateTeam("CDV6-8799 T1", _defaultBusinessUnitId);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);
            
            
            //ARRANGE 
            string subject = "WF Testing - Scenario 30.1";
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
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, 
                recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


            //ASSERT

            //get all Email records for the person
            var emailIDs = dbHelper.email.GetEmailByRegardingID(regardingID);
            Assert.AreEqual(1, emailIDs.Count);

            //get the email info
            var email = dbHelper.email.GetEmailByID(emailIDs[0], "Subject", "emailfromlookupid");
            Assert.AreEqual("WF Testinng - Scenario 30.1 - Action 1 Activated", (string)email["subject".ToLower()]);
            Assert.AreEqual(teamID.ToString(), email["emailfromlookupid".ToLower()].ToString());

            var emailToIds = dbHelper.emailTo.GetByEmailID(emailIDs[0]);
            Assert.AreEqual(1, emailToIds.Count);

            var emailTo = dbHelper.emailTo.GetByID(emailToIds[0], "RegardingId", "RegardingIdTableName", "RegardingIdName");
            Assert.AreEqual(recipientID.ToString(), emailTo["RegardingId".ToLower()].ToString());
            Assert.AreEqual(recipientIDName, (string)emailTo["RegardingIdName".ToLower()]);
            Assert.AreEqual(recipientIdTableName, (string)emailTo["RegardingIdTableName".ToLower()]);
        }

        [TestProperty("JiraIssueID", "CDV6-21918")]
        [TestMethod]
        [Description("Test for the story identified in CDV6-8799 - Allow Team Email to be used in Workflow")]
        public void WorkflowDetailsSection_CDV68799_TestMethod002()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - S30", "WF Automated Testing - S30.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);
            
            var user2id = commonWorkflowMethods.CreateSystemUserRecord("Test.CDV6.21918", "Test", "CDV6-21918", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var teamID = commonWorkflowMethods.CreateTeam("CDV6-8799 T1", _defaultBusinessUnitId);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Testing - Scenario 30.2";
            string description = "Sample Description ...";

            Guid callerID = user2id;
            string callerIdTableName = "systemuser";
            string callerIDName = "Test CDV6-21918";

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;


            //ACT


            //create the record
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


            //ASSERT

            //get all Email records for the person
            var emailIDs = dbHelper.email.GetEmailByRegardingID(regardingID);
            Assert.AreEqual(1, emailIDs.Count);

            //get the email info
            var email = dbHelper.email.GetEmailByID(emailIDs[0], "Subject");
            Assert.AreEqual("WF Testinng - Scenario 30.2 - Action 1 Activated", (string)email["subject".ToLower()]);

            var emailToIds = dbHelper.emailTo.GetByEmailID(emailIDs[0]);
            Assert.AreEqual(1, emailToIds.Count);

            var emailTo = dbHelper.emailTo.GetByID(emailToIds[0], "RegardingId", "RegardingIdTableName", "RegardingIdName");
            Assert.AreEqual(teamID.ToString(), emailTo["RegardingId".ToLower()].ToString());
            Assert.AreEqual("CDV6-8799 T1", (string)emailTo["RegardingIdName".ToLower()]);
            Assert.AreEqual("team", (string)emailTo["RegardingIdTableName".ToLower()]);
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-9096

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-21922")]
        [Description("Test for the story identified in CDV6-9096 - Allow a child workflow to be unpublished when linked to a parent workflow - Parent has call to published WF")]
        public void WorkflowDetailsSection_CDV69096_TestMethod001()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-9096 - Child WF (un-published)", "WF Automated Testing - CDV6-9096 - Child WF (un-published).Zip");
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-9096 - Child WF (published)", "WF Automated Testing - CDV6-9096 - Child WF (published).Zip");
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-9096 - Child WF - UI Test to unpublish", "WF Automated Testing - CDV6-9096 - Child WF - UI Test to unpublish.Zip");
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-9096 - Parent WF", "WF Automated Testing - CDV6-9096 - Parent WF.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "WF Automated Testing - CDV6-9096 - Scenario 1";
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

            //get all Email records for the person
            var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "subject");
            Assert.AreEqual("WF Automated Testing - CDV6-9096 - Published Child WF Triggered", (string)phoneCallfields["subject".ToLower()]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-21923")]
        [Description("Test for the story identified in CDV6-9096 - Allow a child workflow to be unpublished when linked to a parent workflow - Parent has call to unpublished WF")]
        public void WorkflowDetailsSection_CDV69096_TestMethod002()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-9096 - Child WF (un-published)", "WF Automated Testing - CDV6-9096 - Child WF (un-published).Zip");
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-9096 - Child WF (published)", "WF Automated Testing - CDV6-9096 - Child WF (published).Zip");
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-9096 - Child WF - UI Test to unpublish", "WF Automated Testing - CDV6-9096 - Child WF - UI Test to unpublish.Zip");
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-9096 - Parent WF", "WF Automated Testing - CDV6-9096 - Parent WF.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Automated Testing - CDV6-9096 - Scenario 2";
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

            //get all Email records for the person
            var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "subject");
            Assert.AreEqual("WF Automated Testing - CDV6-9096 - Scenario 2", (string)phoneCallfields["subject".ToLower()]);
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-9097

        [TestProperty("JiraIssueID", "CDV6-21924")]
        [TestMethod]
        [Description("Workflow under test 'WF Automated Testing - CDV6-9097' - Step 1 (Action, Condition, Inner Action) ")]
        public void WorkflowDetailsSection_CDV69097_TestMethod001()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-9097", "WF Automated Testing - CDV6-9097.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "CDV6-9097 Step 1 Test -";
            string description = "Desc...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;

            //ACT: create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT

            //get all Email records for the person
            var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "notes");
            Assert.AreEqual("<p>CDV6-9097 Step 1 Test -Desc...</p>", (string)phoneCallfields["notes".ToLower()]);
        }

        [TestProperty("JiraIssueID", "CDV6-21925")]
        [TestMethod]
        [Description("Workflow under test 'WF Automated Testing - CDV6-9097' - Step 2 (Action, Action, Condition, Inner Action) ")]
        public void WorkflowDetailsSection_CDV69097_TestMethod002()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-9097", "WF Automated Testing - CDV6-9097.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "CDV6-9097 Step 2 Test -";
            string description = "Desc...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;

            DateTime PhoneCallDate = new DateTime(2021, 4, 3, 9, 45, 0);



            //ACT: create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(
                subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
                recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT

            //get all Email records for the person
            var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "subject");
            Assert.AreEqual("CDV6-9097 Step 2 Test - 2nd If activated", (string)phoneCallfields["subject".ToLower()]);
        }

        [TestProperty("JiraIssueID", "CDV6-21926")]
        [TestMethod]
        [Description("Workflow under test 'WF Automated Testing - CDV6-9097' - Step 3 (Action, Condition, Inner Condition, Inner Action) ")]
        public void WorkflowDetailsSection_CDV69097_TestMethod003()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-9097", "WF Automated Testing - CDV6-9097.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "CDV6-9097 Step 3 Test -";
            string description = "Desc...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;

            DateTime PhoneCallDate = new DateTime(2021, 4, 7, 15, 30, 0);


            //ACT: create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT

            //get all Email records for the person
            var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "notes");
            Assert.AreEqual("<p>CDV6-9097 Step 3 Test -07/04/2021 15:30:00</p>", (string)phoneCallfields["notes".ToLower()]);
        }

        [TestProperty("JiraIssueID", "CDV6-21927")]
        [TestMethod]
        [Description("Workflow under test 'WF Automated Testing - CDV6-9097' - Step 4 (Action, Condition, Inner Action, Action) ")]
        public void WorkflowDetailsSection_CDV69097_TestMethod004()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-9097", "WF Automated Testing - CDV6-9097.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "CDV6-9097 Step 4 Test";
            string description = "Desc...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;

            DateTime PhoneCallDate = new DateTime(2021, 4, 7, 15, 30, 0);



            //ACT: create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT

            //get all Email records for the person
            var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "subject");
            Assert.AreEqual("CDV6-9097 Step 4 Test - Activated", (string)phoneCallfields["subject".ToLower()]);
        }

        [TestProperty("JiraIssueID", "CDV6-21928")]
        [TestMethod]
        [Description("Workflow under test 'WF Automated Testing - CDV6-9097' - Step 5 (Action, Condition, Inner Action, Action, Condition, Inner Action) ")]
        public void WorkflowDetailsSection_CDV69097_TestMethod005_1()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-9097", "WF Automated Testing - CDV6-9097.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);



            //ARRANGE 
            string subject = "CDV6-9097 Step 5 - Condition 1";
            string description = "Desc...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;

            DateTime PhoneCallDate = new DateTime(2021, 4, 7, 15, 30, 0);


            //ACT: create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT

            //get all Email records for the person
            var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "notes");
            Assert.AreEqual("<p>04/04/2021 15:30:00</p>", (string)phoneCallfields["notes".ToLower()]);
        }

        [TestProperty("JiraIssueID", "CDV6-21929")]
        [TestMethod]
        [Description("Workflow under test 'WF Automated Testing - CDV6-9097' - Step 5 (Action, Condition, Inner Action, Action, Condition, Inner Action) ")]
        public void WorkflowDetailsSection_CDV69097_TestMethod005_2()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-9097", "WF Automated Testing - CDV6-9097.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "CDV6-9097 Step 5 - Condition 2";
            string description = "Desc...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;

            DateTime PhoneCallDate = new DateTime(2021, 4, 7, 15, 30, 0);



            dbHelper = new DatabaseHelper("Jose.brazeta", "Passw0rd_!", tenantName);

            //ACT: create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId, PhoneCallDate);


            //ASSERT

            //get all Email records for the person
            var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "notes");
            Assert.AreEqual("<p>José Brazeta</p>", (string)phoneCallfields["notes".ToLower()]);
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-8893

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-21933")]
        [Description("Workflow under test 'WF Automated Testing - CDV6-8893' - Step 1 - First if condition (positive scenario)")]
        public void WorkflowDetailsSection_CDV8893_TestMethod001()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-8893", "WF Automated Testing - CDV6-8893.Zip");
            
            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            var amyStrong_PersonId = commonWorkflowMethods.CreatePersonRecord(new Guid("94F669D6-2811-45D5-B649-F390DE5E1177"), "Amy", "", "Strong", _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - CDV6-8893-Scenario 1";
            string description = "Desc...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = amyStrong_PersonId;
            string regardingName = "Amy Strong";

            

            //ACT: create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT

            //get all Email records for the person
            var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "subject");
            Assert.AreEqual("WF Testing - CDV6-8893-Scenario 1 - Activated", (string)phoneCallfields["subject".ToLower()]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-21934")]
        [Description("Workflow under test 'WF Automated Testing - CDV6-8893' - Step 1 - First if condition (positive scenario)")]
        public void WorkflowDetailsSection_CDV8893_TestMethod002()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-8893", "WF Automated Testing - CDV6-8893.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            var patrickRogers_PersonId = commonWorkflowMethods.CreatePersonRecord(new Guid("13fc7ce3-6d95-47ae-b120-5277f699a1e2"), "Patrick", "", "Rogers", _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - CDV6-8893-Scenario 1";
            string description = "Desc...";
            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = patrickRogers_PersonId;
            string regardingName = "Patrick Rogers";

            


            //ACT: create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT

            //get all Email records for the person
            var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "subject");
            Assert.AreEqual("WF Testing - CDV6-8893-Scenario 1 - Activated", (string)phoneCallfields["subject".ToLower()]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-21935")]
        [Description("Workflow under test 'WF Automated Testing - CDV6-8893' - Step 1 - First if condition (positive scenario)")]
        public void WorkflowDetailsSection_CDV8893_TestMethod003()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-8893", "WF Automated Testing - CDV6-8893.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            var marciBurnett_PersonId = commonWorkflowMethods.CreatePersonRecord(new Guid("045e13fe-3b4e-4700-b96a-da606f1e607e"), "Marci", "", "Burnett", _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - CDV6-8893-Scenario 1";
            string description = "Desc...";
            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = marciBurnett_PersonId;
            string regardingName = "Marci Burnett";


            //ACT: create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT

            //get all Email records for the person
            var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "subject");
            Assert.AreEqual("WF Testing - CDV6-8893-Scenario 1 - Activated", (string)phoneCallfields["subject".ToLower()]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-21936")]
        [Description("Workflow under test 'WF Automated Testing - CDV6-8893' - Step 1 - First if condition (negative scenario)")]
        public void WorkflowDetailsSection_CDV8893_TestMethod004()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-8893", "WF Automated Testing - CDV6-8893.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);


            //ARRANGE 
            string subject = "WF Testing - CDV6-8893-Scenario 1";
            string description = "Desc...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";


            Guid regardingID = personId;
            string regardingName = personFullName;

            

            //ACT: create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT

            //get all Email records for the person
            var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "subject");
            Assert.AreEqual("WF Testing - CDV6-8893-Scenario 1", (string)phoneCallfields["subject".ToLower()]);
        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-21937")]
        [Description("Workflow under test 'WF Automated Testing - CDV6-8893' - Step 1 - Second if condition (positive scenario)")]
        public void WorkflowDetailsSection_CDV8893_TestMethod005()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-8893", "WF Automated Testing - CDV6-8893.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            dbHelper.person.UpdatePreferredDay(personId, 1); //Monday

            //ARRANGE 
            string subject = "WF Testing - CDV6-8893-Scenario 2";
            string description = "Desc...";
            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;

            

            //ACT: create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT

            //get all Email records for the person
            var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "subject");
            Assert.AreEqual("WF Testing - CDV6-8893-Scenario 2 - Activated", (string)phoneCallfields["subject".ToLower()]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-21938")]
        [Description("Workflow under test 'WF Automated Testing - CDV6-8893' - Step 1 - Second if condition (positive scenario)")]
        public void WorkflowDetailsSection_CDV8893_TestMethod006()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-8893", "WF Automated Testing - CDV6-8893.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            dbHelper.person.UpdatePreferredDay(personId, 2); //Tuesday

            //ARRANGE 
            string subject = "WF Testing - CDV6-8893-Scenario 2";
            string description = "Desc...";
            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;

            

            //ACT: create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT

            //get all Email records for the person
            var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "subject");
            Assert.AreEqual("WF Testing - CDV6-8893-Scenario 2 - Activated", (string)phoneCallfields["subject".ToLower()]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-21939")]
        [Description("Workflow under test 'WF Automated Testing - CDV6-8893' - Step 1 - Second if condition (positive scenario)")]
        public void WorkflowDetailsSection_CDV8893_TestMethod007()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-8893", "WF Automated Testing - CDV6-8893.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            dbHelper.person.UpdatePreferredDay(personId, 3); //Wednesday

            //ARRANGE 
            string subject = "WF Testing - CDV6-8893-Scenario 2";
            string description = "Desc...";

            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;

            

            //ACT: create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT

            //get all Email records for the person
            var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "subject");
            Assert.AreEqual("WF Testing - CDV6-8893-Scenario 2 - Activated", (string)phoneCallfields["subject".ToLower()]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-21940")]
        [Description("Workflow under test 'WF Automated Testing - CDV6-8893' - Step 1 - Second if condition (negative scenario)")]
        public void WorkflowDetailsSection_CDV8893_TestMethod008()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-8893", "WF Automated Testing - CDV6-8893.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            dbHelper.person.UpdatePreferredDay(personId, 4); //Thursday

            //ARRANGE 
            string subject = "WF Testing - CDV6-8893-Scenario 2";
            string description = "Desc...";
            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;

            

            //ACT: create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT

            //get all Email records for the person
            var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "subject");
            Assert.AreEqual("WF Testing - CDV6-8893-Scenario 2", (string)phoneCallfields["subject".ToLower()]);
        }


            
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-21941")]
        [Description("Workflow under test 'WF Automated Testing - CDV6-8893' - Step 1 - Third if condition (positive scenario)")]
        public void WorkflowDetailsSection_CDV8893_TestMethod009()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-8893", "WF Automated Testing - CDV6-8893.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            dbHelper.person.UpdatePreferredDay(personId, 4); //Thursday

            //ARRANGE 
            string subject = "WF Testing - CDV6-8893-Scenario 3";
            string description = "Desc...";
            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;

            


            //ACT: create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT

            //get all Email records for the person
            var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "subject");
            Assert.AreEqual("WF Testing - CDV6-8893-Scenario 3 - Activated", (string)phoneCallfields["subject".ToLower()]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-21942")]
        [Description("Workflow under test 'WF Automated Testing - CDV6-8893' - Step 1 - Third if condition (negative scenario)")]
        public void WorkflowDetailsSection_CDV8893_TestMethod010()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-8893", "WF Automated Testing - CDV6-8893.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            dbHelper.person.UpdatePreferredDay(personId, 3); //Wednesday

            //ARRANGE 
            string subject = "WF Testing - CDV6-8893-Scenario 3";
            string description = "Desc...";
            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;

            


            //ACT: create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT

            //get all Email records for the person
            var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "subject");
            Assert.AreEqual("WF Testing - CDV6-8893-Scenario 3", (string)phoneCallfields["subject".ToLower()]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-21943")]
        [Description("Workflow under test 'WF Automated Testing - CDV6-8893' - Step 1 - Third if condition (negative scenario)")]
        public void WorkflowDetailsSection_CDV8893_TestMethod011()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-8893", "WF Automated Testing - CDV6-8893.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            dbHelper.person.UpdatePreferredDay(personId, 2); //Tuesday

            //ARRANGE 
            string subject = "WF Testing - CDV6-8893-Scenario 3";
            string description = "Desc...";
            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;

            


            //ACT: create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT

            //get all Email records for the person
            var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "subject");
            Assert.AreEqual("WF Testing - CDV6-8893-Scenario 3", (string)phoneCallfields["subject".ToLower()]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-21944")]
        [Description("Workflow under test 'WF Automated Testing - CDV6-8893' - Step 1 - Third if condition (negative scenario)")]
        public void WorkflowDetailsSection_CDV8893_TestMethod012()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-8893", "WF Automated Testing - CDV6-8893.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            dbHelper.person.UpdatePreferredDay(personId, 1); //Monday

            //ARRANGE 
            string subject = "WF Testing - CDV6-8893-Scenario 3";
            string description = "Desc...";
            Guid callerID = personId;
            string callerIdTableName = "person";
            string callerIDName = personFullName;

            Guid recipientID = userid;
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = personId;
            string regardingName = personFullName;

            


            //ACT: create the record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, _defaultTeamId);


            //ASSERT

            //get all Email records for the person
            var phoneCallfields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "subject");
            Assert.AreEqual("WF Testing - CDV6-8893-Scenario 3", (string)phoneCallfields["subject".ToLower()]);
        }




        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-21945")]
        [Description("Workflow under test 'WF Automated Testing - CDV6-8893 (Case Form)' - Step 1 - first if condition (positive scenario)")]
        public void WorkflowDetailsSection_CDV8893_TestMethod013()
        {
            var documentID = commonWorkflowMethods.CreateDocumentIfNeeded("Document CDV6-8893", "Document CDV6-8893.Zip");

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-8893 (Case Form)", "WF Automated Testing - CDV6-8893 (Case Form).Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonWorkflowMethods.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            //Create Social care case for the person
            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 18);

            //create the case form
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_defaultTeamId, documentID, personId, caseID, new DateTime(2021, 4, 12));

            //get the document picklist values
            var documentPickListId = dbHelper.documentPickList.GetByName("Religion").FirstOrDefault();
            var documentPickListValueId = dbHelper.documentPickListValue.GetByTextAndDocumentPickListId("Budist", documentPickListId).FirstOrDefault();

            //get the Document Question Identifiers 
            var documentQuestionIdentifier4Id = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-3246")[0]; //PickList

            //set the answer for PickList
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(caseFormID, documentQuestionIdentifier4Id)[0];
            dbHelper.documentAnswer.UpdatePicklistValueAnswer(documentAnswerID, documentPickListValueId);


            //ACT
            //set the dates that will trigger the workflow
            dbHelper.caseForm.UpdateCaseFormRecord(caseFormID, new DateTime(2021, 4, 1), new DateTime(2021, 4, 2), new DateTime(2021, 4, 3));


            //ASSERT
            //if the workflow was triggered then the review date shoudl be set to 09/04/2021
            var fields = dbHelper.caseForm.GetCaseFormByID(caseFormID, "reviewdate");
            Assert.AreEqual(new DateTime(2021, 4, 9), (DateTime)fields["reviewdate"]);

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-21946")]
        [Description("Workflow under test 'WF Automated Testing - CDV6-8893 (Case Form)' - Step 1 - first if condition (positive scenario)")]
        public void WorkflowDetailsSection_CDV8893_TestMethod014()
        {
            var documentID = commonWorkflowMethods.CreateDocumentIfNeeded("Document CDV6-8893", "Document CDV6-8893.Zip");

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-8893 (Case Form)", "WF Automated Testing - CDV6-8893 (Case Form).Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonWorkflowMethods.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            //Create Social care case for the person
            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 18);

            //create the case form
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_defaultTeamId, documentID, personId, caseID, new DateTime(2021, 4, 12));

            //get the document picklist values
            var documentPickListId = dbHelper.documentPickList.GetByName("Religion").FirstOrDefault();
            var documentPickListValueId = dbHelper.documentPickListValue.GetByTextAndDocumentPickListId("Atheist", documentPickListId).FirstOrDefault();

            //get the Document Question Identifiers 
            var documentQuestionIdentifier4Id = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-3246")[0]; //PickList

            //set the answer for PickList
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(caseFormID, documentQuestionIdentifier4Id)[0];
            dbHelper.documentAnswer.UpdatePicklistValueAnswer(documentAnswerID, documentPickListValueId);


            //ACT
            //set the dates that will trigger the workflow
            dbHelper.caseForm.UpdateCaseFormRecord(caseFormID, new DateTime(2021, 4, 1), new DateTime(2021, 4, 2), new DateTime(2021, 4, 3));


            //ASSERT
            //if the workflow was triggered then the review date shoudl be set to 09/04/2021
            var fields = dbHelper.caseForm.GetCaseFormByID(caseFormID, "reviewdate");
            Assert.AreEqual(new DateTime(2021, 4, 9), (DateTime)fields["reviewdate"]);

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-21947")]
        [Description("Workflow under test 'WF Automated Testing - CDV6-8893 (Case Form)' - Step 1 - first if condition (negative scenario)")]
        public void WorkflowDetailsSection_CDV8893_TestMethod015()
        {
            var documentID = commonWorkflowMethods.CreateDocumentIfNeeded("Document CDV6-8893", "Document CDV6-8893.Zip");

            commonWorkflowMethods.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-8893 (Case Form)", "WF Automated Testing - CDV6-8893 (Case Form).Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonWorkflowMethods.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            //Create Social care case for the person
            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 18);

            //create the case form
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_defaultTeamId, documentID, personId, caseID, new DateTime(2021, 4, 12));

            //get the document picklist values
            var documentPickListId = dbHelper.documentPickList.GetByName("Religion").FirstOrDefault();
            var documentPickListValueId = dbHelper.documentPickListValue.GetByTextAndDocumentPickListId("Jedi", documentPickListId).FirstOrDefault();

            //get the Document Question Identifiers 
            var documentQuestionIdentifier4Id = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-3246")[0]; //PickList

            //set the answer for PickList
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(caseFormID, documentQuestionIdentifier4Id)[0];
            dbHelper.documentAnswer.UpdatePicklistValueAnswer(documentAnswerID, documentPickListValueId);


            //ACT
            //set the dates that will trigger the workflow
            dbHelper.caseForm.UpdateCaseFormRecord(caseFormID, new DateTime(2021, 4, 1), new DateTime(2021, 4, 2), new DateTime(2021, 4, 3));


            //ASSERT
            //if the workflow was triggered then the review date shoudl be set to 09/04/2021
            var fields = dbHelper.caseForm.GetCaseFormByID(caseFormID, "reviewdate");
            Assert.AreEqual(new DateTime(2021, 4, 3), (DateTime)fields["reviewdate"]);

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-15933

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-16457")]
        [Description("Automation Script for the Test Case - CDV6-15933 - Test Assign record action")]
        public void WorkflowDetailsSection_CDV6_15933_TestMethod01()
        {
            //ARRANGE 

            commonWorkflowMethods.CreateWorkflowIfNeeded("Testing CDV6-15933", "Testing CDV6-15933.Zip");

            var assignTeamId = commonWorkflowMethods.CreateTeam(new Guid("37d2b508-ca69-ed11-a336-005056926fe4"), "CDV6-15933 T1", _defaultBusinessUnitId);

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonWorkflowMethods.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            //Create Social care case for the person
            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2022, 3, 2), new DateTime(2022, 3, 1), 18);
            var caseTitle = (string)dbHelper.Case.GetCaseByID(caseID, "title")["title"];

            var taskId = dbHelper.task.CreateTask("Test Task", "", _defaultTeamId, null, null, null, null, null, null, caseID, personId, DateTime.Now.Date, caseID, caseTitle, "case");

            //ACT
            //update the Case record
            dbHelper.Case.UpdateAdditionalInformation(caseID, "updated ....");


            //ASSERT

            var caseFields = dbHelper.Case.GetCaseById(caseID, "ownerid");
            Assert.AreEqual(assignTeamId.ToString(), caseFields["ownerid"].ToString());

            var taskFields = dbHelper.task.GetTaskByID(taskId, "ownerid");
            Assert.AreEqual(assignTeamId.ToString(), taskFields["ownerid"].ToString());
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-16462")]
        [Description("Automation Script for the Test Case - CDV6-15933 - Test Assign record action")]
        public void WorkflowDetailsSection_CDV6_15933_TestMethod02()
        {
            commonWorkflowMethods.CreateWorkflowIfNeeded("Testing CDV6-15933", "Testing CDV6-15933.Zip");

            var assignTeamId = commonWorkflowMethods.CreateTeam(new Guid("03709f17-ca69-ed11-a336-005056926fe4"), "CDV6-15933 T2", _defaultBusinessUnitId);

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonWorkflowMethods.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            //Create Social care case for the person
            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2022, 3, 8), new DateTime(2022, 3, 7), 18);
            var caseTitle = (string)dbHelper.Case.GetCaseByID(caseID, "title")["title"];

            var taskId = dbHelper.task.CreateTask("Test Task", "", _defaultTeamId, null, null, null, null, null, null, caseID, personId, DateTime.Now.Date, caseID, caseTitle, "case");


            //ACT
            //update the Case record
            dbHelper.Case.UpdateAdditionalInformation(caseID, "updated ....");


            //ASSERT
            var caseFields = dbHelper.Case.GetCaseById(caseID, "ownerid");
            Assert.AreEqual(assignTeamId.ToString(), caseFields["ownerid"].ToString());

            var taskFields = dbHelper.task.GetTaskByID(taskId, "ownerid");
            Assert.AreEqual(_defaultTeamId.ToString(), taskFields["ownerid"].ToString());
        }

        #endregion

        

    }
}

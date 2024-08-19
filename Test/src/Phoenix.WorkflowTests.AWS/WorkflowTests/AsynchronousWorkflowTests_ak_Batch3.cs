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
    public class AsynchronousWorkflowTests_ak_Batch3 : BaseTestClass
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

            #region Web API auth system user

            commonWorkflowMethods.CreateSystemUserRecord("webapiauthuser", "webapi", "authuser", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            #endregion
        }

        [TestProperty("JiraIssueID", "CDV6-7487")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 38 - Test CHECK MULTIPLE RESPONSE ITEM BY IDENTIFIER  action (checklist answer NOT selected)")]
        public void WorkflowDetailsSection_TestMethod038()
        {
            var wfTestDocumentId = commonWorkflowMethods.CreateDocumentIfNeeded("Workflow Test Document 1", "Workflow Test Document 1.Zip");

            AsyncTestWorkflowValidateActions2ID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Validate Actions for Async workflow 2", "WF Async Automated Testing - Validate Actions for Async workflow 2.Zip");

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

            //ARRANGE 

            //Guid caseFormID = new Guid("3975da51-4152-e911-a2c5-005056926fe4");
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_defaultTeamId, wfTestDocumentId, personId, caseID, new DateTime(2017, 1, 1));

            //get the multiOptionAnswer
            var multipleResponseQuestionId = dbHelper.questionCatalogue.GetByQuestionName("WF Multiple Response").FirstOrDefault();
            var multiOptionAnswerId = dbHelper.multiOptionAnswer.GetByNameAndQuestionCatalogueID("Day 1", multipleResponseQuestionId).First();

            //get the Document Question Identifier for 'Multiple Response'
            var documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-34")[0];//Multiple Response

            //set the answer for the question
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(caseFormID, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswerChecklist.CreateDocumentAnswerChecklist(documentAnswerID, multiOptionAnswerId, true);


            //ACT

            //set the date to activate the workflow
            dbHelper.caseForm.UpdateCaseFormRecord(caseFormID, new DateTime(2018, 1, 1), false);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowValidateActions2ID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);


            //Separate assessment field should not be updated by the WF
            var fields = dbHelper.caseForm.GetCaseFormByID(caseFormID, "startdate", "separateassessment");
            Assert.AreEqual(new DateTime(2018, 1, 1), (DateTime)fields["startdate"]);
            Assert.AreEqual(false, (bool)fields["separateassessment"]);

        }

        [TestProperty("JiraIssueID", "CDV6-7488")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 38.1 - Test CHECK MULTIPLE RESPONSE ITEM BY IDENTIFIER  action (checklist answer selected)")]
        public void WorkflowDetailsSection_TestMethod038_1()
        {
            var wfTestDocumentId = commonWorkflowMethods.CreateDocumentIfNeeded("Workflow Test Document 1", "Workflow Test Document 1.Zip");

            AsyncTestWorkflowValidateActions2ID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Validate Actions for Async workflow 2", "WF Async Automated Testing - Validate Actions for Async workflow 2.Zip");

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

            //ARRANGE 

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_defaultTeamId, wfTestDocumentId, personId, caseID, new DateTime(2017, 1, 1));

            //get the multi response Answer
            var multipleResponseQuestionId = dbHelper.questionCatalogue.GetByQuestionName("WF Multiple Response").FirstOrDefault();
            var multiOptionAnswer2Id = dbHelper.multiOptionAnswer.GetByNameAndQuestionCatalogueID("Day 2", multipleResponseQuestionId).FirstOrDefault();

            //get the Document Question Identifier for 'Multiple Response'            
            var documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-34")[0]; //Multiple Response

            //set the answer for the question            
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(caseFormID, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswerChecklist.CreateDocumentAnswerChecklist(documentAnswerID, multiOptionAnswer2Id, true);

            //ACT
            //set the date to activate the workflow
            dbHelper.caseForm.UpdateCaseFormRecord(caseFormID, new DateTime(2018, 1, 1), false);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowValidateActions2ID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);


            //Separate assessment field should be updated by the WF
            var fields = dbHelper.caseForm.GetCaseFormByID(caseFormID, "startdate", "separateassessment");
            Assert.AreEqual(new DateTime(2018, 1, 1), (DateTime)fields["startdate"]);
            Assert.AreEqual(true, (bool)fields["separateassessment"]);
        }

        [TestProperty("JiraIssueID", "CDV6-7491")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 40 - Test Get Answer By Identifier action (Answers should activate WF actions)")]
        public void WorkflowDetailsSection_TestMethod040()
        {
            var wfTestDocumentId = commonWorkflowMethods.CreateDocumentIfNeeded("Workflow Test Document 1", "Workflow Test Document 1.Zip");

            AsyncTestWorkflowValidateActions2ID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Validate Actions for Async workflow 2", "WF Async Automated Testing - Validate Actions for Async workflow 2.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var Workflow_Test_User = commonWorkflowMethods.CreateSystemUserRecord(new Guid("1ab2f044-0352-e911-a2c5-005056926fe4"), "WorkflowTest.User1", "WorkflowTest", "User1", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var firstName = "Adolfo";
            var middleName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var lastName = "Abbott";
            var personFullName = firstName + " " + lastName;
            var personId = commonWorkflowMethods.CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonWorkflowMethods.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            //Create Social care case for the person
            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 18);

            //ARRANGE
            dbHelper = new DBHelper.DatabaseHelper("WorkflowTest.User1", "Passw0rd_!", tenantName);

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_defaultTeamId, wfTestDocumentId, personId, caseID, new DateTime(2017, 1, 1));
            //Guid caseFormID = new Guid("34241d9d-4152-e911-a2c5-005056926fe4");  //case form that will activate WF actions


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
            dbHelper.documentAnswer.UpdateLookupAnswer(documentAnswerID, personId, "person", personFullName);
          

            //ACT
            //set the date to activate the workflow
            dbHelper.caseForm.UpdateCaseFormRecord(caseFormID, new DateTime(2018, 3, 1), false);


            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowValidateActions2ID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);


            //validate updates performed by the workflow (all if statments to the Get Answer By Identifier action should be activated)            
            var fields = dbHelper.caseForm.GetCaseFormByID(caseFormID, "DueDate", "ResponsibleUserid", "SeparateAssessment", "CarerDeclinedJointAssessment", "JointCarerAssessment", "NewPerson");
            Assert.AreEqual(new DateTime(2019, 3, 2), (DateTime)fields["duedate"]);
            Assert.AreEqual(Workflow_Test_User.ToString(), fields["responsibleuserid"].ToString());
            Assert.AreEqual(true, (bool)fields["separateassessment"]);
            Assert.AreEqual(true, (bool)fields["carerdeclinedjointassessment"]);
            Assert.AreEqual(true, (bool)fields["jointcarerassessment"]);
            Assert.AreEqual(true, (bool)fields["newperson".ToLower()]);
        }

        [TestProperty("JiraIssueID", "CDV6-7492")]
        [TestMethod]
        [Description("Automation Script for the Test Case - Scenario 40.1 - Test Get Answer By Identifier action (Answers should NOT activate WF actions)")]
        public void WorkflowDetailsSection_TestMethod040_1()
        {
            var wfTestDocumentId = commonWorkflowMethods.CreateDocumentIfNeeded("Workflow Test Document 1", "Workflow Test Document 1.Zip");

            AsyncTestWorkflowValidateActions2ID = commonWorkflowMethods.CreateWorkflowIfNeeded("WF Async Automated Testing - Validate Actions for Async workflow 2", "WF Async Automated Testing - Validate Actions for Async workflow 2.Zip");

            var userid = commonWorkflowMethods.CreateSystemUserRecord("Jose.brazeta", "José", "Brazeta", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            var Workflow_Test_User = commonWorkflowMethods.CreateSystemUserRecord(new Guid("1ab2f044-0352-e911-a2c5-005056926fe4"), "WorkflowTest.User1", "WorkflowTest", "User1", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

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

            //ARRANGE 

            dbHelper = new DBHelper.DatabaseHelper("WorkflowTest.User1", "Passw0rd_!", tenantName);

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_defaultTeamId, wfTestDocumentId, personId, caseID, new DateTime(2017, 1, 1));

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
            dbHelper.caseForm.UpdateCaseFormRecord(caseFormID, new DateTime(2018, 3, 1), false);

            //ASSERT
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(AsyncTestWorkflowValidateActions2ID, 1).FirstOrDefault();
            webApiHelper.Security.Authenticate(tenantName, "webapiauthuser", "Passw0rd_!");
            webApiHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), webApiHelper.Security.AuthenticationCookie);
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            //validate updates performed by the workflow (all if statments to the Get Answer By Identifier action should NOT be activated)
            var fields = dbHelper.caseForm.GetCaseFormByID(caseFormID, "DueDate", "ResponsibleUserid", "SeparateAssessment", "CarerDeclinedJointAssessment", "JointCarerAssessment", "NewPerson");
            //Assert.AreEqual(null, fields["duedate"]);
            //Assert.AreEqual(null, fields["ResponsibleUserID".ToLower()]);
            Assert.AreEqual(false, fields.ContainsKey("duedate"));
            Assert.AreEqual(false, fields.ContainsKey("responsibleuserid".ToLower()));
            Assert.AreEqual(false, (bool)fields["SeparateAssessment".ToLower()]);
            Assert.AreEqual(false, (bool)fields["CarerDeclinedJointAssessment".ToLower()]);
            Assert.AreEqual(false, (bool)fields["JointCarerAssessment".ToLower()]);
            Assert.AreEqual(false, (bool)fields["NewPerson".ToLower()]);
        }


    }
}

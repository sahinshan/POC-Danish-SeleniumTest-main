using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Cases.CaseForm
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("Files\\Assessment Form.Zip")]
    public class Case_CaseFormFormTasks_UITestCases : FunctionalTest
    {
        #region Properties

        private string _tenantName;
        private Guid _languageId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private string _systemUserName;
        private Guid _contactReasonId;
        private Guid _systemUserId;
        private Guid _personID;
        private Guid _caseId;
        private string _caseNumber;
        private Guid _caseStatusId;
        private Guid _caseFormId;
        private Guid _dataFormId;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _documentId;
        private Guid _activityCategoryId;
        private Guid _activitySubCategoryId;
        private Guid _activityReasonId;
        private Guid _activityPriorityId;
        private Guid _activityOutcomeId;
        private Guid _caseFormTask1ID;
        private Guid _caseFormTask2ID;
        private string _caseFormTitle;

        #endregion

        [TestInitialize()]
        public void CaseFormTask_SetupTest()
        {

            try
            {
                #region Tenant

                _tenantName = ConfigurationManager.AppSettings["TenantName"];
                dbHelper = new DBHelper.DatabaseHelper(_tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper, fileIOHelper, TestContext);

                #endregion

                #region Business Unit

                if (!dbHelper.businessUnit.GetByName("CareDirector QA").Any())
                    dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
                _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];

                #endregion

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Team

                if (!dbHelper.team.GetTeamIdByName("CareDirector QA").Any())
                    dbHelper.team.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
                _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

                #endregion                

                #region Language

                if (!dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any())
                    _languageId = dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                if (_languageId == Guid.Empty)
                    _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").FirstOrDefault();
                #endregion

                #region Ethnicity

                if (!dbHelper.ethnicity.GetEthnicityIdByName("English").Any())
                    dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "English", new DateTime(2020, 1, 1));
                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];

                #endregion

                #region Data Form

                _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

                #endregion

                #region System User "CaseFormTaskUser1"

                _systemUserName = "CaseFormTaskUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "CaseFormTask", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion

                #region Case Status

                _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();

                #endregion

                #region Contact Reason

                _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

                #endregion

                #region Activity Categories                

                _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Activity Sub Categories

                _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId, _careDirectorQA_TeamId);

                #endregion

                #region Activity Reason

                _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Activity Priority

                _activityPriorityId = commonMethodsDB.CreateActivityPriority(new Guid("1e164c51-9d45-e911-a2c5-005056926fe4"), "High", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Activity Outcome

                _activityOutcomeId = commonMethodsDB.CreateActivityOutcome(new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"), "More information needed", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Document

                string documentName = "Assessment Form";
                _documentId = commonMethodsDB.CreateDocumentIfNeeded(documentName, "Assessment Form.Zip");

                #endregion

                #region Person

                var firstName = "Automation";
                var lastName = _currentDateSuffix;
                _personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));

                #endregion

                #region Case & Case Form

                _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2015, 06, 29), new DateTime(2021, 11, 11), 20, "Care Form Record For Case Note");
                _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];

                // Create a new case form
                _caseFormId = dbHelper.caseForm.CreateCaseForm(_careDirectorQA_TeamId, _personID, firstName + lastName, _systemUserId, _caseId, _caseNumber.ToString(), _documentId, "Assessment Form", 1, new DateTime(2021, 02, 06), null, new DateTime(2021, 06, 06));
                _caseFormTitle = (string)dbHelper.caseForm.GetCaseFormByID(_caseFormId, "title")["title"];

                #endregion

                #region Case Form Task

                _caseFormTask1ID = dbHelper.task.CreateTask("Case Form Task 01", "Case Form Task 01 description", _careDirectorQA_TeamId, _systemUserId,
                    _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, _caseId,
                    _personID, new DateTime(2021, 2, 22), _caseFormId, _caseFormTitle, "caseform");

                _caseFormTask2ID = dbHelper.task.CreateTask("Case Form Task 02", "", _careDirectorQA_TeamId, _systemUserId,
                    null, null, null, null, null, _caseId, _personID, null, _caseFormId, _caseFormTitle, "caseform");


                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }


        #region https://advancedcsg.atlassian.net/browse/CDV6-8614


        [TestProperty("JiraIssueID", "CDV6-9951")]
        [Description(
           "Login in the web app - Open a Case Form record - Navigate to the Case Form Tasks area - " +
            "Validate that the Case Form Tasks page is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseFormTasks_UITestMethod01()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber, _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(_caseFormId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .NavigateToCaseFormTasksArea();

            caseFormTasksPage
                .WaitForCaseFormTasksPageToLoad()
                .ValidateSubjectCellText(_caseFormTask1ID.ToString(), "Case Form Task 01")
                .ValidateDueCellText(_caseFormTask1ID.ToString(), "22/02/2021 00:00:00")
                .ValidateStatusCellText(_caseFormTask1ID.ToString(), "Open")
                .ValidateRegardingCellText(_caseFormTask1ID.ToString(), _caseFormTitle)
                .ValidateReasonCellText(_caseFormTask1ID.ToString(), "Assessment")
                .ValidateResponsibleTeamCellText(_caseFormTask1ID.ToString(), "CareDirector QA")
                .ValidateResponsibleUserCellText(_caseFormTask1ID.ToString(), "CaseFormTask User1")

                .ValidateSubjectCellText(_caseFormTask2ID.ToString(), "Case Form Task 02")
                .ValidateDueCellText(_caseFormTask2ID.ToString(), "")
                .ValidateStatusCellText(_caseFormTask2ID.ToString(), "Open")
                .ValidateRegardingCellText(_caseFormTask2ID.ToString(), _caseFormTitle)
                .ValidateReasonCellText(_caseFormTask2ID.ToString(), "")
                .ValidateResponsibleTeamCellText(_caseFormTask2ID.ToString(), "CareDirector QA")
                .ValidateResponsibleUserCellText(_caseFormTask2ID.ToString(), "CaseFormTask User1");
        }

        [TestProperty("JiraIssueID", "CDV6-9952")]
        [Description(
            "Login in the web app - Open a Case Form record - Navigate to the Case Form Tasks area - " +
            "Search for Case Form Task record using a search query that should match a record subject - " +
            "Tap on the search button - Validate that the matching records are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseFormTasks_UITestMethod02()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber, _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(_caseFormId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .NavigateToCaseFormTasksArea();

            caseFormTasksPage
                .WaitForCaseFormTasksPageToLoad()
                .SearchCaseFormTaskRecord("Case Form Task 01")
                .ValidateRecordPresent(_caseFormTask1ID.ToString())
                .ValidateRecordNotPresent(_caseFormTask2ID.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-9953")]
        [Description(
            "Login in the web app - Open a Case Form record - Navigate to the Case Form Tasks area - Open a Case Form Task record (all fields must have values) - " +
            "Validate that the user is redirected to the Case Form Tasks record Page - Validate all field values")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseFormTasks_UITestMethod03()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber, _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(_caseFormId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .NavigateToCaseFormTasksArea();

            caseFormTasksPage
                .WaitForCaseFormTasksPageToLoad()
                .OpenCaseFormTaskRecord(_caseFormTask1ID.ToString());

            caseFormTaskRecordPage
                .WaitForCaseFormTaskRecordPageToLoad("Case Form Task 01")
                .ValidateSubject("Case Form Task 01")
                .ValidateDescription("Case Form Task 01 description")

                .ValidateRegardingFieldLinkText(_caseFormTitle)
                .ValidateReasonFieldLinkText("Assessment")
                .ValidatePriorityFieldLinkText("High")
                .ValidateDue("22/02/2021", "00:00")
                .ValidateStatus("Open")
                .ValidateContainsInformationProvidedByAThirdPartyCheckedOption(false)
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA")
                .ValidateResponsibleUserFieldLinkText("CaseFormTask User1")
                .ValidateCategoryFieldLinkText("Advice")
                .ValidateSubCategoryFieldLinkText("Home Support")
                .ValidateOutcomeFieldLinkText("More information needed")
                .ValidateIsCaseNoteCheckedOption(false)
                .ValidateSignificantEventCheckedOption(false)
                .ValidateIsClonedCheckedOption(false);
        }

        #endregion


        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        [TestCategory("UITest")]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}

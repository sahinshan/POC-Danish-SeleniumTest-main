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
    public class Case_CaseFormCaseNotes_UITestCases : FunctionalTest
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
        private Guid _caseFormCaseNote1ID;
        private Guid _caseFormCaseNote2ID;
        private Guid _systemSettingId;

        #endregion

        [TestInitialize()]
        public void CaseFormCaseNotes_SetupTest()
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

                #region System User "CaseFormCaseNoteUser1"

                _systemUserName = "CaseFormCaseNoteUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "CaseFormCaseNote", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

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

                #region System Setting

                if (!dbHelper.systemSetting.GetSystemSettingIdByName("AllowMultipleActiveSocialCareCase").Any())
                    _systemSettingId = dbHelper.systemSetting.CreateSystemSetting("AllowMultipleActiveSocialCareCase", "false", "When set to true the organisation will be able to decide if they want to allow multiple active social care referrals", false, "false");
                _systemSettingId = dbHelper.systemSetting.GetSystemSettingIdByName("AllowMultipleActiveSocialCareCase").FirstOrDefault();

                #endregion

                #region Case & Case Form

                _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2015, 06, 29), new DateTime(2021, 11, 11), 20, "Care Form Record For Case Note");
                _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];

                // Create a new case form
                _caseFormId = dbHelper.caseForm.CreateCaseForm(_careDirectorQA_TeamId, _personID, firstName + lastName, _systemUserId, _caseId, _caseNumber.ToString(), _documentId, "Assessment Form", 1, new DateTime(2021, 02, 06), null, new DateTime(2021, 06, 06));

                #endregion

                #region Case Form Case Notes

                _caseFormCaseNote1ID = dbHelper.caseFormCaseNote.CreateCaseFormCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case Form Case Note 001",
                    "Case Form Case Note 001 description", _caseFormId, _caseId, _personID, _activityCategoryId, _activitySubCategoryId,
                    _activityOutcomeId, _activityReasonId, _activityPriorityId, false, null, null, null, new DateTime(2021, 02, 19), true, 1, false);

                _caseFormCaseNote2ID = dbHelper.caseFormCaseNote.CreateCaseFormCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case Form Case Note 002",
                    "", _caseFormId, _caseId, _personID, null, null,
                    null, null, null, false, null, null, null, new DateTime(2021, 02, 19), false, 1, false);

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-8461


        [TestProperty("JiraIssueID", "CDV6-9922")]
        [Description(
           "Login in the web app - Open a Case Form record - Navigate to the Case Form Case Notes area - " +
            "Validate that the Case Form Case Notes page is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseFormCaseNotes_UITestMethod01()
        {
            var _createdOn_caseFormCaseNote1 = (DateTime)dbHelper.caseFormCaseNote.GetByID(_caseFormCaseNote1ID, "createdon")["createdon"];
            var _createdOn_caseFormCaseNote2 = (DateTime)dbHelper.caseFormCaseNote.GetByID(_caseFormCaseNote2ID, "createdon")["createdon"];

            var userLocalTime_CaseNote1 = _createdOn_caseFormCaseNote1;
            var userLocalTime_CaseNote2 = _createdOn_caseFormCaseNote2;

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
                .NavigateToCaseFormCaseNotesArea();

            caseFormCaseNotesPage
                .WaitForCaseFormCaseNotesPageToLoad()
                .ValidateRecordPresent(_caseFormCaseNote1ID.ToString())
                .ValidateRecordPresent(_caseFormCaseNote2ID.ToString())
                .ValidateSubjectCellText(_caseFormCaseNote1ID.ToString(), "Case Form Case Note 001")
                .ValidateDateCellText(_caseFormCaseNote1ID.ToString(), "19/02/2021 00:00:00")
                .ValidateStatusCellText(_caseFormCaseNote1ID.ToString(), "Open")
                .ValidateCreatedByCellText(_caseFormCaseNote1ID.ToString(), "System Administrator")
                .ValidateCreatedOnCellText(_caseFormCaseNote1ID.ToString(), userLocalTime_CaseNote1.ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture))

                .ValidateSubjectCellText(_caseFormCaseNote2ID.ToString(), "Case Form Case Note 002")
                .ValidateDateCellText(_caseFormCaseNote2ID.ToString(), "19/02/2021 00:00:00")
                .ValidateStatusCellText(_caseFormCaseNote2ID.ToString(), "Open")
                .ValidateCreatedByCellText(_caseFormCaseNote2ID.ToString(), "System Administrator")
                .ValidateCreatedOnCellText(_caseFormCaseNote2ID.ToString(), userLocalTime_CaseNote2.ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture));
        }

        [TestProperty("JiraIssueID", "CDV6-9923")]
        [Description(
            "Login in the web app - Open a Case Form record - Navigate to the Case Form Case Notes area - " +
            "Search for Case Form Case Note record using a search query that should match a record subject - " +
            "Tap on the search button - Validate that the matching records are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseFormCaseNotes_UITestMethod02()
        {

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
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
                .NavigateToCaseFormCaseNotesArea();

            caseFormCaseNotesPage
                .WaitForCaseFormCaseNotesPageToLoad()
                .SearchCaseFormCaseNoteRecord("Case Form Case Note 001")
                .ValidateRecordPresent(_caseFormCaseNote1ID.ToString())
                .ValidateRecordNotPresent(_caseFormCaseNote2ID.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-9924")]
        [Description(
            "Login in the web app - Open a Case Form record - Navigate to the Case Form Case Notes area - Open a Case Form Case Note record (all fields must have values) - " +
            "Validate that the user is redirected to the Case Form Case Notes record Page - Validate all field values")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseFormCaseNotes_UITestMethod03()
        {
            string _caseFormTitle = (string)dbHelper.caseForm.GetCaseFormByID(_caseFormId, "title")["title"];

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
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
                .NavigateToCaseFormCaseNotesArea();

            caseFormCaseNotesPage
                .WaitForCaseFormCaseNotesPageToLoad()
                .OpenCaseFormCaseNoteRecord(_caseFormCaseNote1ID.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("Case Form Case Note 001")
                .ValidateSubject("Case Form Case Note 001")
                .ValidateDescription("Case Form Case Note 001 description")
                .ValidateCaseFormFieldLinkText(_caseFormTitle)
                .ValidateReasonFieldLinkText("Assessment")
                .ValidatePriorityFieldLinkText("High")
                .ValidateDate("19/02/2021", "00:00")
                .ValidateStatus("Open")
                .ValidateContainsInformationProvidedByAThirdPartyYesOptionChecked(true)
                .ValidateContainsInformationProvidedByAThirdPartyNoOptionChecked(false)
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA")
                .ValidateResponsibleUserFieldLinkText("CaseFormCaseNote User1")
                .ValidateCategoryFieldLinkText("Advice")
                .ValidateSubCategoryFieldLinkText("Home Support")
                .ValidateOutcomeFieldLinkText("More information needed")
                .ValidateSignificantEventYesOptionChecked(false)
                .ValidateSignificantEventNoOptionChecked(true)
                .ValidateIsClonedOptionChecked(false)
                .ValidateIsClonedNoOptionChecked(true);
        }

        [TestProperty("JiraIssueID", "CDV6-9925")]
        [Description(
            "Login in the web app - Open a Case Form record - Navigate to the Case Form Case Notes area - Open a Case Form Case Note record (only mandatory fields have values) - " +
            "Validate that the user is redirected to the Case Form Case Note record page - Validate all field values")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseFormCaseNotes_UITestMethod04()
        {
            string _caseFormTitle = (string)dbHelper.caseForm.GetCaseFormByID(_caseFormId, "title")["title"];

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
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
                .NavigateToCaseFormCaseNotesArea();

            caseFormCaseNotesPage
                .WaitForCaseFormCaseNotesPageToLoad()
                .OpenCaseFormCaseNoteRecord(_caseFormCaseNote2ID.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("Case Form Case Note 002")

                .ValidateSubject("Case Form Case Note 002")
                .ValidateDescription("")

                .ValidateCaseFormFieldLinkText(_caseFormTitle)
                .ValidateReasonFieldLinkText("")
                .ValidatePriorityFieldLinkText("")
                .ValidateDate("19/02/2021", "00:00")
                .ValidateStatus("Open")
                .ValidateContainsInformationProvidedByAThirdPartyYesOptionChecked(false)
                .ValidateContainsInformationProvidedByAThirdPartyNoOptionChecked(true)
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA")
                .ValidateResponsibleUserFieldLinkText("CaseFormCaseNote User1")
                .ValidateCategoryFieldLinkText("")
                .ValidateSubCategoryFieldLinkText("")
                .ValidateOutcomeFieldLinkText("")

                .ValidateSignificantEventYesOptionChecked(false)
                .ValidateSignificantEventNoOptionChecked(true)

                .ValidateIsClonedOptionChecked(false)
                .ValidateIsClonedNoOptionChecked(true)
                ;
        }


        [TestProperty("JiraIssueID", "CDV6-9926")]
        [Description(
            "Login in the web app - Open a Case Form record - Navigate to the Case Form Case Notes area - Open a Case Form Case Note record - " +
            "Update all fields (leave Is Encrypted set to No) - Tap on the save button - Validate that the Case Form record is saved")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseFormCaseNotes_UITestMethod05()
        {

            string _caseFormTitle = (string)dbHelper.caseForm.GetCaseFormByID(_caseFormId, "title")["title"];
            var caseFormCaseNoteId = dbHelper.caseFormCaseNote.CreateCaseFormCaseNote(_careDirectorQA_TeamId, null, "Case Form Case Note 003", "", _caseFormId, _caseId, _personID,
                null, null, null, null, null, false, null, null, null,
                DateTime.Now, false, 1, false);

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
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
                .NavigateToCaseFormCaseNotesArea();

            caseFormCaseNotesPage
                .WaitForCaseFormCaseNotesPageToLoad()
                .OpenCaseFormCaseNoteRecord(caseFormCaseNoteId.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("Case Form Case Note 003")
                .InsertSubject("Case Form Case Note 003 updated")
                .InsertDescription("description goes here")
                .ClickReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activityReasonId.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("Case Form Case Note 003")
                .ClickPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activityPriorityId.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("Case Form Case Note 003")
                .InsertDate("19/02/2021", "12:30")
                .ClickContainsInformationProvidedByAThirdParty_YesRadioButton()
                .ClickResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CaseFormCaseNote User1").TapSearchButton().SelectResultElement(_systemUserId.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("Case Form Case Note 003")
                .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activityCategoryId.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("Case Form Case Note 003")
                .ClickSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activitySubCategoryId.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("Case Form Case Note 003")
                .ClickOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activityOutcomeId.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("Case Form Case Note 003")
                .ClickSaveAndCloseButton();

            caseFormCaseNotesPage
                .WaitForCaseFormCaseNotesPageToLoad()
                .OpenCaseFormCaseNoteRecord(caseFormCaseNoteId.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("Case Form Case Note 003 updated")

                .ValidateSubject("Case Form Case Note 003 updated")
                .ValidateDescription("description goes here")

                .ValidateCaseFormFieldLinkText(_caseFormTitle)
                .ValidateReasonFieldLinkText("Assessment")
                .ValidatePriorityFieldLinkText("High")
                .ValidateDate("19/02/2021", "12:30")
                .ValidateStatus("Open")
                .ValidateContainsInformationProvidedByAThirdPartyYesOptionChecked(true)
                .ValidateContainsInformationProvidedByAThirdPartyNoOptionChecked(false)
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA")
                .ValidateResponsibleUserFieldLinkText("CaseFormCaseNote User1")
                .ValidateCategoryFieldLinkText("Advice")
                .ValidateSubCategoryFieldLinkText("Home Support")
                .ValidateOutcomeFieldLinkText("More information needed")
                .ValidateSignificantEventYesOptionChecked(false)
                .ValidateSignificantEventNoOptionChecked(true)
                .ValidateIsClonedOptionChecked(false)
                .ValidateIsClonedNoOptionChecked(true);
        }

        [TestProperty("JiraIssueID", "CDV6-9927")]
        [Description(
            "Login in the web app - Open a Case Form record - Navigate to the Case Form Case Notes area - Open a Case Form Case Note record - " +
            "Update all editable fields - Tap on the save button - Validate that the Case Form record is saved")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseFormCaseNotes_UITestMethod06()
        {
            string _caseFormTitle = (string)dbHelper.caseForm.GetCaseFormByID(_caseFormId, "title")["title"];
            var caseFormCaseNoteId = dbHelper.caseFormCaseNote.CreateCaseFormCaseNote(_careDirectorQA_TeamId, null, "Case Form Case Note 003", "", _caseFormId, _caseId, _personID,
                null, null, null, null, null, false, null, null, null,
                DateTime.Now, false, 1, false);

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
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
                .NavigateToCaseFormCaseNotesArea();

            caseFormCaseNotesPage
                .WaitForCaseFormCaseNotesPageToLoad()
                .OpenCaseFormCaseNoteRecord(caseFormCaseNoteId.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("Case Form Case Note 003")
                .InsertSubject("Case Form Case Note 003 updated")
                .InsertDescription("description goes here")
                .ClickReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activityReasonId.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("Case Form Case Note 003")
                .ClickPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activityPriorityId.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("Case Form Case Note 003")
                .InsertDate("19/02/2021", "12:30")
                .ClickContainsInformationProvidedByAThirdParty_YesRadioButton()
                .ClickResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CaseFormCaseNote User1").TapSearchButton().SelectResultElement(_systemUserId.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("Case Form Case Note 003")
                .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activityCategoryId.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("Case Form Case Note 003")
                .ClickSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activitySubCategoryId.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("Case Form Case Note 003")
                .ClickOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activityOutcomeId.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("Case Form Case Note 003")
                .ClickSaveButton()
                .ClickBackButton();

            caseFormCaseNotesPage
                .WaitForCaseFormCaseNotesPageToLoad()
                .OpenCaseFormCaseNoteRecord(caseFormCaseNoteId.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("Case Form Case Note 003 updated")

                .ValidateSubject("Case Form Case Note 003 updated")
                .ValidateDescription("description goes here")

                .ValidateCaseFormFieldLinkText(_caseFormTitle)
                .ValidateReasonFieldLinkText("Assessment")
                .ValidatePriorityFieldLinkText("High")
                .ValidateDate("19/02/2021", "12:30")
                .ValidateStatus("Open")
                .ValidateContainsInformationProvidedByAThirdPartyYesOptionChecked(true)
                .ValidateContainsInformationProvidedByAThirdPartyNoOptionChecked(false)
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA")
                .ValidateResponsibleUserFieldLinkText("CaseFormCaseNote User1")
                .ValidateCategoryFieldLinkText("Advice")
                .ValidateSubCategoryFieldLinkText("Home Support")
                .ValidateOutcomeFieldLinkText("More information needed")
                .ValidateSignificantEventYesOptionChecked(false)
                .ValidateSignificantEventNoOptionChecked(true)
                .ValidateIsClonedOptionChecked(false)
                .ValidateIsClonedNoOptionChecked(true);

        }

        [TestProperty("JiraIssueID", "CDV6-9928")]
        [Description(
            "Login in the web app - Open a Case Form record - Navigate to the Case Form Case Notes area - Tap on the add new record button - " +
            "Set data in all fields - Tap on the save button - Validate that the record is saved")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseFormCaseNotes_UITestMethod07()
        {
            string _caseFormTitle = (string)dbHelper.caseForm.GetCaseFormByID(_caseFormId, "title")["title"];

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
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
                .NavigateToCaseFormCaseNotesArea();

            caseFormCaseNotesPage
                .WaitForCaseFormCaseNotesPageToLoad()
                .ClickNewRecordButton();

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("New")
                .InsertSubject("Case Form Case Note 003")
                .InsertDescription("description goes here")
                .ClickReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activityReasonId.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("New")
                .ClickPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activityPriorityId.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("New")
                .InsertDate("19/02/2021", "12:30")
                .SelectStatus("Open")
                .ClickContainsInformationProvidedByAThirdParty_YesRadioButton()
                .ClickResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CaseFormCaseNote User1").TapSearchButton().SelectResultElement(_systemUserId.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("New")
                .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activityCategoryId.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("New")
                .ClickSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activitySubCategoryId.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("New")
                .ClickOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activityOutcomeId.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            caseFormCaseNotesPage
                .WaitForCaseFormCaseNotesPageToLoad();

            var casenoterecords = dbHelper.caseFormCaseNote.GetByCaseFormIdAndSubject(_caseFormId, "Case Form Case Note 003");
            Assert.AreEqual(1, casenoterecords.Count());


            caseFormCaseNotesPage
                .WaitForCaseFormCaseNotesPageToLoad()
                .OpenCaseFormCaseNoteRecord(casenoterecords[0].ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("Case Form Case Note 003")

                .ValidateSubject("Case Form Case Note 003")
                .ValidateDescription("description goes here")

                .ValidateCaseFormFieldLinkText(_caseFormTitle)
                .ValidateReasonFieldLinkText("Assessment")
                .ValidatePriorityFieldLinkText("High")
                .ValidateDate("19/02/2021", "12:30")
                .ValidateStatus("Open")
                .ValidateContainsInformationProvidedByAThirdPartyYesOptionChecked(true)
                .ValidateContainsInformationProvidedByAThirdPartyNoOptionChecked(false)
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA")
                .ValidateResponsibleUserFieldLinkText("CaseFormCaseNote User1")
                .ValidateCategoryFieldLinkText("Advice")
                .ValidateSubCategoryFieldLinkText("Home Support")
                .ValidateOutcomeFieldLinkText("More information needed")
                .ValidateSignificantEventYesOptionChecked(false)
                .ValidateSignificantEventNoOptionChecked(true)
                .ValidateIsClonedOptionChecked(false)
                .ValidateIsClonedNoOptionChecked(true);
        }

        [TestProperty("JiraIssueID", "CDV6-9929")]
        [Description(
            "Login in the web app - Open a Case Form record - Navigate to the Case Form Case Notes area - Tap on the add new record button - " +
            "Set data in mandatory fields only - Tap on the save button - Validate that the record is saved")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseFormCaseNotes_UITestMethod08()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
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
                .NavigateToCaseFormCaseNotesArea();

            caseFormCaseNotesPage
                .WaitForCaseFormCaseNotesPageToLoad()
                .ClickNewRecordButton();

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("New")
                .InsertSubject("Case Form Case Note 003")
                .InsertDate("19/02/2021", "12:30")
                .SelectStatus("Open")
                .ClickSaveAndCloseButton();

            caseFormCaseNotesPage
                .WaitForCaseFormCaseNotesPageToLoad();

            var casenoterecords = dbHelper.caseFormCaseNote.GetByCaseFormIdAndSubject(_caseFormId, "Case Form Case Note 003");
            Assert.AreEqual(1, casenoterecords.Count());

            string _caseFormTitle = (string)dbHelper.caseForm.GetCaseFormByID(_caseFormId, "title")["title"];

            caseFormCaseNotesPage
                .WaitForCaseFormCaseNotesPageToLoad()
                .OpenCaseFormCaseNoteRecord(casenoterecords[0].ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("Case Form Case Note 003")
                .ValidateSubject("Case Form Case Note 003")
                .ValidateDescription("")
                .ValidateCaseFormFieldLinkText(_caseFormTitle)
                .ValidateReasonFieldLinkText("")
                .ValidatePriorityFieldLinkText("")
                .ValidateDate("19/02/2021", "12:30")
                .ValidateStatus("Open")
                .ValidateContainsInformationProvidedByAThirdPartyYesOptionChecked(false)
                .ValidateContainsInformationProvidedByAThirdPartyNoOptionChecked(true)
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA")
                .ValidateResponsibleUserFieldLinkText("CaseFormCaseNote User1")
                .ValidateCategoryFieldLinkText("")
                .ValidateSubCategoryFieldLinkText("")
                .ValidateOutcomeFieldLinkText("")
                .ValidateSignificantEventYesOptionChecked(false)
                .ValidateSignificantEventNoOptionChecked(true)
                .ValidateIsClonedOptionChecked(false)
                .ValidateIsClonedNoOptionChecked(true);
        }

        [TestProperty("JiraIssueID", "CDV6-9930")]
        [Description(
            "Login in the web app - Open a Case Form record - Navigate to the Case Form Case Notes area - Open a Case Form Case Note record - Tap on the delete button - " +
            "Confirm the delete operation - Validate that the record is deleted from the database")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseFormCaseNotes_UITestMethod09()
        {
            var caseFormCaseNoteId = dbHelper.caseFormCaseNote.CreateCaseFormCaseNote(_careDirectorQA_TeamId, null, "Case Form Case Note 003", "", _caseFormId, _caseId, _personID,
                null, null, null, null, null, false, null, null, null,
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), false, 1, false);

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
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
                .NavigateToCaseFormCaseNotesArea();

            caseFormCaseNotesPage
                .WaitForCaseFormCaseNotesPageToLoad()
                .OpenCaseFormCaseNoteRecord(caseFormCaseNoteId.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("Case Form Case Note 003")
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            caseFormCaseNotesPage
                .WaitForCaseFormCaseNotesPageToLoad();

            System.Threading.Thread.Sleep(1500);

            var casenoterecords = dbHelper.caseFormCaseNote.GetByCaseFormIdAndSubject(_caseFormId, "Case Form Case Note 003");
            Assert.AreEqual(0, casenoterecords.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-9931")]
        [Description(
            "Login in the web app - Open a Case Form record - Navigate to the Case Form Case Notes area - Tap on the add new record button - " +
            "Set data in all mandatory fields except for subject - Tap on the save button - " +
            "Validate that the user is prevented from saving the record.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseFormCaseNotes_UITestMethod10()
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
                .NavigateToCaseFormCaseNotesArea();

            caseFormCaseNotesPage
                .WaitForCaseFormCaseNotesPageToLoad()
                .ClickNewRecordButton();

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("New")
                //.InsertSubject("Case Form Case Note 003")
                .InsertDate("19/02/2021", "12:30")
                .SelectStatus("Open")
                .ClickSaveAndCloseButton()
                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.")
                .ValidateSubjectErrorLabelVisibility(true)
                .ValidatesubjectErrorLabelText("Please fill out this field.")
                .ValidateDateErrorLabelVisibility(false)
                .ValidateTimeErrorLabelVisibility(false)
                .ValidateStatusErrorLabelVisibility(false);

            System.Threading.Thread.Sleep(1500);
            var casenoterecords = dbHelper.caseFormCaseNote.GetByCaseFormIdAndSubject(_caseFormId, "Case Form Case Note 003");
            Assert.AreEqual(0, casenoterecords.Count());
        }

        [TestProperty("JiraIssueID", "CDV6-9932")]
        [Description(
            "Login in the web app - Open a Case Form record - Navigate to the Case Form Case Notes area - Tap on the add new record button - " +
            "Set data in all mandatory fields except for Date - Tap on the save button - " +
            "Validate that the user is prevented from saving the record.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseFormCaseNotes_UITestMethod11()
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
                .NavigateToCaseFormCaseNotesArea();

            caseFormCaseNotesPage
                .WaitForCaseFormCaseNotesPageToLoad()
                .ClickNewRecordButton();

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("New")
                .InsertSubject("Case Form Case Note 003")
                //.InsertDate("19/02/2021", "12:30")
                .SelectStatus("Open")
                .ClickSaveAndCloseButton()
                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.")
                .ValidateSubjectErrorLabelVisibility(false)
                .ValidateDateErrorLabelVisibility(true)
                .ValidateDateErrorLabelText("Please fill out this field.")
                .ValidateTimeErrorLabelVisibility(true)
                .ValidateTimeErrorLabelText("Please fill out this field.")
                .ValidateStatusErrorLabelVisibility(false);

            var casenoterecords = dbHelper.caseFormCaseNote.GetByCaseFormIdAndSubject(_caseFormId, "Case Form Case Note 003");
            Assert.AreEqual(0, casenoterecords.Count());
        }

        [TestProperty("JiraIssueID", "CDV6-9933")]
        [Description(
            "Login in the web app - Open a Case Form record - Navigate to the Case Form Case Notes area - Tap on the add new record button - " +
            "Set data in all mandatory fields except for Status - Tap on the save button - " +
            "Validate that the user is prevented from saving the record.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseFormCaseNotes_UITestMethod12()
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
                .NavigateToCaseFormCaseNotesArea();

            caseFormCaseNotesPage
                .WaitForCaseFormCaseNotesPageToLoad()
                .ClickNewRecordButton();

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("New")
                .InsertSubject("Case Form Case Note 003")
                .InsertDate("19/02/2021", "12:30")
                //.SelectStatus("Open")
                .ClickSaveAndCloseButton()
                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.")
                .ValidateSubjectErrorLabelVisibility(false)
                .ValidateDateErrorLabelVisibility(false)
                .ValidateTimeErrorLabelVisibility(false)
                .ValidateStatusErrorLabelVisibility(true)
                .ValidateStatusErrorLabelText("Please fill out this field.");

            var casenoterecords = dbHelper.caseFormCaseNote.GetByCaseFormIdAndSubject(_caseFormId, "Case Form Case Note 003");
            Assert.AreEqual(0, casenoterecords.Count());
        }

        [TestProperty("JiraIssueID", "CDV6-9934")]
        [Description(
            "Login in the web app - Open a Case Form record - Navigate to the Case Form Case Notes area - Tap on the add new record button - " +
            "Set data in all mandatory fields - Set Category to Advice - Tap on the Sub_Category button - Validate that the displayed results are filtered accoarding to the selected category")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseFormCaseNotes_UITestMethod13()
        {
            #region Activity Categories                

            var _activityCategory1Id = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);
            var _activityCategory2Id = commonMethodsDB.CreateActivityCategory(new Guid("fc599e31-5f75-e911-a2c5-005056926fe4"), "Mental Heath Care Contacts", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Sub Categories

            var _activitySubCategory1Id = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategory1Id, _careDirectorQA_TeamId);
            var _activitySubCategory2Id = commonMethodsDB.CreateActivitySubCategory(new Guid("405f28e8-6075-e911-a2c5-005056926fe4"), "Carers Assessment", new DateTime(2020, 1, 1), _activityCategory2Id, _careDirectorQA_TeamId);
            var _activitySubCategory3Id = commonMethodsDB.CreateActivitySubCategory(new Guid("95859a60-5f75-e911-a2c5-005056926fe4"), "Core Assessment", new DateTime(2020, 1, 1), _activityCategory2Id, _careDirectorQA_TeamId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
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
                .NavigateToCaseFormCaseNotesArea();

            caseFormCaseNotesPage
                .WaitForCaseFormCaseNotesPageToLoad()
                .ClickNewRecordButton();

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("New")
                .InsertSubject("Case Form Case Note 003")
                .InsertDescription("description goes here")
                .InsertDate("19/02/2021", "12:30")
                .SelectStatus("Open")
                .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activityCategory1Id.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("New")
                .ClickSubCategoryLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(_activitySubCategory1Id.ToString())
                .ValidateResultElementNotPresent(_activitySubCategory2Id.ToString())
                .ValidateResultElementNotPresent(_activitySubCategory3Id.ToString());
        }


        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-8611 & https://advancedcsg.atlassian.net/browse/CDV6-8463

        [TestProperty("JiraIssueID", "CDV6-9935")]
        [Description(
            "Login in the web app - Open a Case Form record - Navigate to the Case Form Case Notes area - Open a Case Form Case Note record (all fields must have values) - " +
            "Wait for the Case Form Case Notes record page to load - Click on the Clone Case Note Button - Validate that the clone popup is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CloneCaseFormCaseNotes_UITestMethod01()
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
                .NavigateToCaseFormCaseNotesArea();

            caseFormCaseNotesPage
                .WaitForCaseFormCaseNotesPageToLoad()
                .OpenCaseFormCaseNoteRecord(_caseFormCaseNote1ID.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("Case Form Case Note 001")
                .ClickCloneButton();

            cloneActivityPopup
                .WaitForCloneActivityPopupToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-9936")]
        [Description(
            "Login in the web app - Open a Case Form record - Navigate to the Case Form Case Notes area - Open a Case Form Case Note record (all fields must have values) - " +
            "Wait for the Case Form Case Notes record page to load - Click on the Clone Case Note Button - Wait for the the clone popup to load - " +
            "Set the 'Clone Activity to' picklist to Person - Select the person record - Tap on the Clone Button - Wait for the Clone popup to be closed - " +
            "Validate that the a new Case Note is associated with the person record - Validate that all fields match the original case note")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CloneCaseFormCaseNotes_UITestMethod02()
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
                .NavigateToCaseFormCaseNotesArea();

            caseFormCaseNotesPage
                .WaitForCaseFormCaseNotesPageToLoad()
                .OpenCaseFormCaseNoteRecord(_caseFormCaseNote1ID.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("Case Form Case Note 001")
                .ClickCloneButton();

            cloneActivityPopup
                .WaitForCloneActivityPopupToLoad()
                .SelectBusinessObjectTypeText("Person")
                .SelectRecord(_personID.ToString())
                .ClickCloneButton();

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("Case Form Case Note 001");

            var records = dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(_personID);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.personCaseNote.GetPersonCaseNoteByID(records[0],
                "subject", "notes", "personid", "activityreasonid", "activitypriorityid", "casenotedate",
                "statusid", "informationbythirdparty", "ownerid", "responsibleuserid", "activitycategoryid",
                "activitysubcategoryid", "activityoutcomeid", "issignificantevent", "iscloned", "clonedfromid", "clonedfromidtablename", "clonedfromidname");

            Assert.AreEqual("Case Form Case Note 001", fields["subject"]);
            Assert.AreEqual("Case Form Case Note 001 description", fields["notes"]);
            Assert.AreEqual(_personID.ToString(), fields["personid"]);
            Assert.AreEqual(_activityReasonId.ToString(), fields["activityreasonid"]);
            Assert.AreEqual(_activityPriorityId.ToString(), fields["activitypriorityid"]);
            Assert.AreEqual(new DateTime(2021, 2, 19), fields["casenotedate"]);
            Assert.AreEqual(1, fields["statusid"]);
            Assert.AreEqual(true, fields["informationbythirdparty"]);
            Assert.AreEqual(_careDirectorQA_TeamId.ToString(), fields["ownerid"]);
            Assert.AreEqual(_systemUserId.ToString(), fields["responsibleuserid"]);
            Assert.AreEqual(_activityCategoryId.ToString(), fields["activitycategoryid"]);
            Assert.AreEqual(_activitySubCategoryId.ToString(), fields["activitysubcategoryid"]);
            Assert.AreEqual(_activityOutcomeId.ToString(), fields["activityoutcomeid"]);
            Assert.AreEqual(false, fields["issignificantevent"]);
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(_caseFormCaseNote1ID.ToString(), fields["clonedfromid"]);
            Assert.AreEqual("caseformcasenote", fields["clonedfromidtablename"]);
            Assert.AreEqual("Case Form Case Note 001", fields["clonedfromidname"]);
        }

        [TestProperty("JiraIssueID", "CDV6-9937")]
        [Description(
            "Login in the web app - Open a Case Form record - Navigate to the Case Form Case Notes area - Open a Case Form Case Note record (all fields must have values) - " +
            "Wait for the Case Form Case Notes record page to load - Click on the Clone Case Note Button - Wait for the the clone popup to load - " +
            "Set the 'Clone Activity to' picklist to Case - Select a Case record - Tap on the Clone Button - Wait for the Clone popup to be closed - " +
            "Validate that the a new Case Note is associated with the Case record - Validate that all fields match the original case note")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CloneCaseFormCaseNotes_UITestMethod03()
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
                .NavigateToCaseFormCaseNotesArea();

            caseFormCaseNotesPage
                .WaitForCaseFormCaseNotesPageToLoad()
                .OpenCaseFormCaseNoteRecord(_caseFormCaseNote1ID.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("Case Form Case Note 001")
                .ClickCloneButton();

            cloneActivityPopup
                .WaitForCloneActivityPopupToLoad()
                .SelectBusinessObjectTypeText("Case")
                .SelectRecord(_caseId.ToString())
                .ClickCloneButton();

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("Case Form Case Note 001");

            var records = dbHelper.caseCaseNote.GetByCaseID(_caseId);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.caseCaseNote.GetByID(records[0],
                "subject", "notes", "personid", "caseid", "activityreasonid", "activitypriorityid", "casenotedate",
                "statusid", "informationbythirdparty", "ownerid", "responsibleuserid", "activitycategoryid",
                "activitysubcategoryid", "activityoutcomeid", "issignificantevent", "iscloned", "clonedfromid", "clonedfromidtablename", "clonedfromidname");

            Assert.AreEqual("Case Form Case Note 001", fields["subject"]);
            Assert.AreEqual("Case Form Case Note 001 description", fields["notes"]);
            Assert.AreEqual(_personID.ToString(), fields["personid"]);
            Assert.AreEqual(_caseId.ToString(), fields["caseid"]);
            Assert.AreEqual(_activityReasonId.ToString(), fields["activityreasonid"]);
            Assert.AreEqual(_activityPriorityId.ToString(), fields["activitypriorityid"]);
            Assert.AreEqual(new DateTime(2021, 2, 19), fields["casenotedate"]);
            Assert.AreEqual(1, fields["statusid"]);
            Assert.AreEqual(true, fields["informationbythirdparty"]);
            Assert.AreEqual(_careDirectorQA_TeamId.ToString(), fields["ownerid"]);
            Assert.AreEqual(_systemUserId.ToString(), fields["responsibleuserid"]);
            Assert.AreEqual(_activityCategoryId.ToString(), fields["activitycategoryid"]);
            Assert.AreEqual(_activitySubCategoryId.ToString(), fields["activitysubcategoryid"]);
            Assert.AreEqual(_activityOutcomeId.ToString(), fields["activityoutcomeid"]);
            Assert.AreEqual(false, fields["issignificantevent"]);
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(_caseFormCaseNote1ID.ToString(), fields["clonedfromid"]);
            Assert.AreEqual("caseformcasenote", fields["clonedfromidtablename"]);
            Assert.AreEqual("Case Form Case Note 001", fields["clonedfromidname"]);
        }

        [TestProperty("JiraIssueID", "CDV6-9938")]
        [Description(
            "Login in the web app - Open a Case Form record - Navigate to the Case Form Case Notes area - Open a Case Form Case Note record (all fields must have values) - " +
            "Wait for the Case Form Case Notes record page to load - Click on the Clone Case Note Button - Wait for the the clone popup to load - " +
            "Set the 'Clone Activity to' picklist to Case - Select a multiple Case record - Tap on the Clone Button - Wait for the Clone popup to be closed - " +
            "Validate that the a new Case Note is associated with the each selected Case record - Validate that all fields match the original case note")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CloneCaseFormCaseNotes_UITestMethod04()
        {
            dbHelper.systemSetting.UpdateSystemSettingValue(_systemSettingId, "true");
            var _case2Id = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 04, 28), new DateTime(2020, 04, 28), 20, "Case Record");
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
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
                .NavigateToCaseFormCaseNotesArea();

            caseFormCaseNotesPage
                .WaitForCaseFormCaseNotesPageToLoad()
                .OpenCaseFormCaseNoteRecord(_caseFormCaseNote1ID.ToString());

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("Case Form Case Note 001")
                .ClickCloneButton();

            cloneActivityPopup
                .WaitForCloneActivityPopupToLoad()
                .SelectBusinessObjectTypeText("Case")
                .SelectRecord(_caseId.ToString())
                .SelectRecord(_case2Id.ToString())
                .ClickCloneButton();

            caseFormCaseNoteRecordPage
                .WaitForCaseFormCaseNoteRecordPageToLoad("Case Form Case Note 001");

            //check the case notes for the 1st case record
            var records = dbHelper.caseCaseNote.GetByCaseID(_caseId);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.caseCaseNote.GetByID(records[0],
                "subject", "notes", "personid", "caseid", "activityreasonid", "activitypriorityid", "casenotedate",
                "statusid", "informationbythirdparty", "ownerid", "responsibleuserid", "activitycategoryid",
                "activitysubcategoryid", "activityoutcomeid", "issignificantevent", "iscloned", "clonedfromid", "clonedfromidtablename", "clonedfromidname");

            Assert.AreEqual("Case Form Case Note 001", fields["subject"]);
            Assert.AreEqual("Case Form Case Note 001 description", fields["notes"]);
            Assert.AreEqual(_personID.ToString(), fields["personid"]);
            Assert.AreEqual(_caseId.ToString(), fields["caseid"]);
            Assert.AreEqual(_activityReasonId.ToString(), fields["activityreasonid"]);
            Assert.AreEqual(_activityPriorityId.ToString(), fields["activitypriorityid"]);
            Assert.AreEqual(new DateTime(2021, 2, 19), fields["casenotedate"]);
            Assert.AreEqual(1, fields["statusid"]);
            Assert.AreEqual(true, fields["informationbythirdparty"]);
            Assert.AreEqual(_careDirectorQA_TeamId.ToString(), fields["ownerid"]);
            Assert.AreEqual(_systemUserId.ToString(), fields["responsibleuserid"]);
            Assert.AreEqual(_activityCategoryId.ToString(), fields["activitycategoryid"]);
            Assert.AreEqual(_activitySubCategoryId.ToString(), fields["activitysubcategoryid"]);
            Assert.AreEqual(_activityOutcomeId.ToString(), fields["activityoutcomeid"]);
            Assert.AreEqual(false, fields["issignificantevent"]);
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(_caseFormCaseNote1ID.ToString(), fields["clonedfromid"]);
            Assert.AreEqual("caseformcasenote", fields["clonedfromidtablename"]);
            Assert.AreEqual("Case Form Case Note 001", fields["clonedfromidname"]);


            //check the case notes for the 2nd case record
            records = dbHelper.caseCaseNote.GetByCaseID(_case2Id);
            Assert.AreEqual(1, records.Count);

            fields = dbHelper.caseCaseNote.GetByID(records[0],
                "subject", "notes", "personid", "caseid", "activityreasonid", "activitypriorityid", "casenotedate",
                "statusid", "informationbythirdparty", "ownerid", "responsibleuserid", "activitycategoryid",
                "activitysubcategoryid", "activityoutcomeid", "issignificantevent", "iscloned", "clonedfromid", "clonedfromidtablename", "clonedfromidname");

            Assert.AreEqual("Case Form Case Note 001", fields["subject"]);
            Assert.AreEqual("Case Form Case Note 001 description", fields["notes"]);
            Assert.AreEqual(_personID.ToString(), fields["personid"]);
            Assert.AreEqual(_case2Id.ToString(), fields["caseid"]);
            Assert.AreEqual(_activityReasonId.ToString(), fields["activityreasonid"]);
            Assert.AreEqual(_activityPriorityId.ToString(), fields["activitypriorityid"]);
            Assert.AreEqual(new DateTime(2021, 2, 19), fields["casenotedate"]);
            Assert.AreEqual(1, fields["statusid"]);
            Assert.AreEqual(true, fields["informationbythirdparty"]);
            Assert.AreEqual(_careDirectorQA_TeamId.ToString(), fields["ownerid"]);
            Assert.AreEqual(_systemUserId.ToString(), fields["responsibleuserid"]);
            Assert.AreEqual(_activityCategoryId.ToString(), fields["activitycategoryid"]);
            Assert.AreEqual(_activitySubCategoryId.ToString(), fields["activitysubcategoryid"]);
            Assert.AreEqual(_activityOutcomeId.ToString(), fields["activityoutcomeid"]);
            Assert.AreEqual(false, fields["issignificantevent"]);
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(_caseFormCaseNote1ID.ToString(), fields["clonedfromid"]);
            Assert.AreEqual("caseformcasenote", fields["clonedfromidtablename"]);
            Assert.AreEqual("Case Form Case Note 001", fields["clonedfromidname"]);
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

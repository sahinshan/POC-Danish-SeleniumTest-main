using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("Files\\Automation - Person Form 1.Zip")]
    public class Person_PersonFormCaseNotes_UITestCases : FunctionalTest
    {
        private string _environmentName;
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private string _systemUsername;
        private String _defaultUserFullname;
        private string _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");
        private string _firstName;
        private string _lastName;
        private string _personFullname;
        private Guid _personId;
        private string _personNumber;
        private Guid _documentId1;
        private Guid _activityPriorityId_Normal;
        private Guid _activityPriorityId_High;
        private Guid _activityCategoryId;
        private Guid _activitySubCategoryId;
        private Guid _activityReasonId;
        private Guid _activityOutcomeId;
        private Guid _appointmentTypeId;
        string document1Name = "Automation - Person Form 1";
        private Guid personFormID;
        private string personFormTitle;

        [TestInitialize()]
        public void TestsSetupMethod()
        {
            try
            {
                #region Environment 

                _environmentName = ConfigurationManager.AppSettings["EnvironmentName"];

                #endregion

                #region Internal

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];
                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);
                _defaultUserFullname = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(defaultSystemUserId, "fullname")["fullname"];

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("Security BU2");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("Security T2", null, _businessUnitId, "907678", "SecurityT2@careworkstempmail.com", "Security T2", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region System User PFCaseNoteUser1

                _systemUsername = "PFCaseNoteUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "PFCaseNote", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

                #region Activity Categories                

                _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _teamId);

                #endregion

                #region Activity Sub Categories

                _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId, _teamId);

                #endregion

                #region Activity Reason

                _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _teamId);

                #endregion

                #region Activity Priority

                _activityPriorityId_Normal = commonMethodsDB.CreateActivityPriority(new Guid("5246a13f-9d45-e911-a2c5-005056926fe4"), "Normal", new DateTime(2019, 1, 1), _teamId);
                _activityPriorityId_High = commonMethodsDB.CreateActivityPriority(new Guid("1e164c51-9d45-e911-a2c5-005056926fe4"), "High", new DateTime(2020, 1, 1), _teamId);

                #endregion

                #region Activity Outcome

                _activityOutcomeId = commonMethodsDB.CreateActivityOutcome(new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"), "More information needed", new DateTime(2020, 1, 1), _teamId);

                #endregion

                #region Appointment Type

                _appointmentTypeId = commonMethodsDB.CreateAppointmentTypeIfNeeded("Conference", _teamId);

                #endregion

                #region Person

                _firstName = "John";
                _lastName = "LN_" + _currentDateString;
                _personFullname = _firstName + " " + _lastName;
                _personId = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId, new DateTime(2003, 1, 2));
                _personNumber = dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"].ToString();

                #endregion

                #region Document

                _documentId1 = commonMethodsDB.CreateDocumentIfNeeded(document1Name, "Automation - Person Form 1.Zip");

                #endregion

                #region Person Form
                personFormID = dbHelper.personForm.CreatePersonForm(_teamId, _personId, _documentId1, new DateTime(2021, 2, 19), true);
                personFormTitle = (string)dbHelper.personForm.GetPersonFormByID(personFormID, "title")["title"];

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


        [TestProperty("JiraIssueID", "CDV6-24869")]
        [Description(
           "Login in the web app - Open a Person Form record - Navigate to the Person Form Case Notes area - " +
            "Validate that the Person Form Case Notes page is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonFormCaseNotes_UITestMethod01()
        {
            Guid personFormCaseNote1ID = dbHelper.personFormCaseNote.CreatePersonFormCaseNote(_teamId, _systemUserId, "Person Form Case Note 001",
                "Person Form Case Note 001 description", personFormID, _personId, _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId_Normal, false, null, null,
                null, new DateTime(2021, 2, 19, 15, 20, 00), false, 1, false);

            Guid personFormCaseNote2ID = dbHelper.personFormCaseNote.CreatePersonFormCaseNote(_teamId, _systemUserId, "Person Form Case Note 002",
                "", personFormID, _personId, null, null, null, null, null, false, null, null,
                null, new DateTime(2021, 2, 19, 08, 55, 00), false, 1, false);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .NavigateToPersonFormCaseNotesArea();

            personFormCaseNotesPage
                .WaitForPersonFormCaseNotesPageToLoad();

            var personFormCaseNote1fields = dbHelper.personFormCaseNote.GetByID(personFormCaseNote1ID, "createdon");
            string personCaseNote1_createdon = ((DateTime)personFormCaseNote1fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            var personFormCaseNote2fields = dbHelper.personFormCaseNote.GetByID(personFormCaseNote2ID, "createdon");
            string personCaseNote2_createdon = ((DateTime)personFormCaseNote2fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            personFormCaseNotesPage
                .WaitForPersonFormCaseNotesPageToLoad()
                .ValidateSubjectCellText(personFormCaseNote1ID.ToString(), "Person Form Case Note 001")
                .ValidateDateCellText(personFormCaseNote1ID.ToString(), "19/02/2021 15:20:00")
                .ValidateStatusCellText(personFormCaseNote1ID.ToString(), "Open")
                .ValidateCreatedByCellText(personFormCaseNote1ID.ToString(), _defaultUserFullname)
                .ValidateCreatedOnCellText(personFormCaseNote1ID.ToString(), personCaseNote1_createdon)

                .ValidateSubjectCellText(personFormCaseNote2ID.ToString(), "Person Form Case Note 002")
                .ValidateDateCellText(personFormCaseNote2ID.ToString(), "19/02/2021 08:55:00")
                .ValidateStatusCellText(personFormCaseNote2ID.ToString(), "Open")
                .ValidateCreatedByCellText(personFormCaseNote2ID.ToString(), _defaultUserFullname)
                .ValidateCreatedOnCellText(personFormCaseNote2ID.ToString(), personCaseNote2_createdon);
        }

        [TestProperty("JiraIssueID", "CDV6-24870")]
        [Description(
            "Login in the web app - Open a Person Form record - Navigate to the Person Form Case Notes area - " +
            "Search for Person Form Case Note record using a search query that should match a record subject - " +
            "Tap on the search button - Validate that the matching records are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonFormCaseNotes_UITestMethod02()
        {
            Guid personFormCaseNote1ID = dbHelper.personFormCaseNote.CreatePersonFormCaseNote(_teamId, _systemUserId, "Person Form Case Note 001",
                "Person Form Case Note 001 description", personFormID, _personId, _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId_Normal, false, null, null,
                null, new DateTime(2021, 2, 19, 15, 20, 00), false, 1, false);

            Guid personFormCaseNote2ID = dbHelper.personFormCaseNote.CreatePersonFormCaseNote(_teamId, _systemUserId, "Person Form Case Note 002",
                "", personFormID, _personId, null, null, null, null, null, false, null, null,
                null, new DateTime(2021, 2, 19, 08, 55, 00), false, 1, false);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .NavigateToPersonFormCaseNotesArea();

            personFormCaseNotesPage
                .WaitForPersonFormCaseNotesPageToLoad()
                .SearchPersonFormCaseNoteRecord("Person Form Case Note 001")
                .ValidateRecordPresent(personFormCaseNote1ID.ToString())
                .ValidateRecordNotPresent(personFormCaseNote2ID.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-24871")]
        [Description(
            "Login in the web app - Open a Person Form record - Navigate to the Person Form Case Notes area - Open a Person Form Case Note record (all fields must have values) - " +
            "Validate that the user is redirected to the Person Form Case Notes record Page - Validate all field values")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonFormCaseNotes_UITestMethod03()
        {
            Guid personFormCaseNote1ID = dbHelper.personFormCaseNote.CreatePersonFormCaseNote(_teamId, _systemUserId, "Person Form Case Note 001",
                "Person Form Case Note 001 description", personFormID, _personId, _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId_High, false, null, null,
                null, new DateTime(2021, 2, 19, 15, 20, 00), true, 1, false);
            string personFormTitle = (string)dbHelper.personForm.GetPersonFormByID(personFormID, "title")["title"];

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .NavigateToPersonFormCaseNotesArea();

            personFormCaseNotesPage
                .WaitForPersonFormCaseNotesPageToLoad()
                .OpenPersonFormCaseNoteRecord(personFormCaseNote1ID.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("Person Form Case Note 001")

                .ValidateSubject("Person Form Case Note 001")
                .ValidateDescription("Person Form Case Note 001 description")

                .ValidatePersonFormFieldLinkText(personFormTitle)
                .ValidateReasonFieldLinkText("Assessment")
                .ValidatePriorityFieldLinkText("High")
                .ValidateDate("19/02/2021", "15:20")
                .ValidateStatus("Open")
                .ValidateContainsInformationProvidedByAThirdPartyYesOptionChecked(true)
                .ValidateContainsInformationProvidedByAThirdPartyNoOptionChecked(false)
                .ValidateResponsibleTeamFieldLinkText("Security T2")
                .ValidateResponsibleUserFieldLinkText("PFCaseNote User1")
                .ValidateCategoryFieldLinkText("Advice")
                .ValidateSubCategoryFieldLinkText("Home Support")
                .ValidateOutcomeFieldLinkText("More information needed")

                .ValidateSignificantEventYesOptionChecked(false)
                .ValidateSignificantEventNoOptionChecked(true)

                .ValidateIsClonedOptionChecked(false)
                .ValidateIsClonedNoOptionChecked(true);
        }

        [TestProperty("JiraIssueID", "CDV6-24872")]
        [Description(
            "Login in the web app - Open a Person Form record - Navigate to the Person Form Case Notes area - Open a Person Form Case Note record (only mandatory fields have values) - " +
            "Validate that the user is redirected to the Person Form Case Note record page - Validate all field values")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonFormCaseNotes_UITestMethod04()
        {
            Guid personFormCaseNote2ID = dbHelper.personFormCaseNote.CreatePersonFormCaseNote(_teamId, _systemUserId, "Person Form Case Note 002",
                "", personFormID, _personId, null, null, null, null, null, false, null, null,
                null, new DateTime(2021, 2, 19, 08, 55, 00), false, 1, false);
            string personFormTitle = (string)dbHelper.personForm.GetPersonFormByID(personFormID, "title")["title"];

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .NavigateToPersonFormCaseNotesArea();

            personFormCaseNotesPage
                .WaitForPersonFormCaseNotesPageToLoad()
                .OpenPersonFormCaseNoteRecord(personFormCaseNote2ID.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("Person Form Case Note 002")

                .ValidateSubject("Person Form Case Note 002")
                .ValidateDescription("")

                .ValidatePersonFormFieldLinkText(personFormTitle)
                .ValidateReasonFieldLinkText("")
                .ValidatePriorityFieldLinkText("")
                .ValidateDate("19/02/2021", "08:55")
                .ValidateStatus("Open")
                .ValidateContainsInformationProvidedByAThirdPartyYesOptionChecked(false)
                .ValidateContainsInformationProvidedByAThirdPartyNoOptionChecked(true)
                .ValidateResponsibleTeamFieldLinkText("Security T2")
                .ValidateResponsibleUserFieldLinkText("PFCaseNote User1")
                .ValidateCategoryFieldLinkText("")
                .ValidateSubCategoryFieldLinkText("")
                .ValidateOutcomeFieldLinkText("")

                .ValidateSignificantEventYesOptionChecked(false)
                .ValidateSignificantEventNoOptionChecked(true)

                .ValidateIsClonedOptionChecked(false)
                .ValidateIsClonedNoOptionChecked(true)
                ;
        }


        [TestProperty("JiraIssueID", "CDV6-24873")]
        [Description(
            "Login in the web app - Open a Person Form record - Navigate to the Person Form Case Notes area - Open a Person Form Case Note record - " +
            "Update all fields (leave Is Encrypted set to No) - Tap on the save button - Validate that the Person Form record is saved")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonFormCaseNotes_UITestMethod05()
        {
            var personFormCaseNoteId = dbHelper.personFormCaseNote.CreatePersonFormCaseNote(_teamId, null, "Person Form Case Note 003", "", personFormID, _personId,
                null, null, null, null, null, false, null, null, null,
                DateTime.Now, true, 1, false);
            string personFormTitle = (string)dbHelper.personForm.GetPersonFormByID(personFormID, "title")["title"];

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .NavigateToPersonFormCaseNotesArea();

            personFormCaseNotesPage
                .WaitForPersonFormCaseNotesPageToLoad()
                .OpenPersonFormCaseNoteRecord(personFormCaseNoteId.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("Person Form Case Note 003")
                .InsertSubject("Person Form Case Note 003 updated")
                .InsertDescription("description goes here")
                .ClickReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activityReasonId.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("Person Form Case Note 003")
                .ClickPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activityPriorityId_Normal.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("Person Form Case Note 003")
                .InsertDate("19/02/2021", "12:30")
                .ClickContainsInformationProvidedByAThirdParty_YesRadioButton()
                .ClickResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("PFCaseNoteUser1").TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("Person Form Case Note 003")
                .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activityCategoryId.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("Person Form Case Note 003")
                .ClickSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activitySubCategoryId.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("Person Form Case Note 003")
                .ClickOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activityOutcomeId.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("Person Form Case Note 003")
                .ClickSaveAndCloseButton();

            personFormCaseNotesPage
                .WaitForPersonFormCaseNotesPageToLoad()
                .OpenPersonFormCaseNoteRecord(personFormCaseNoteId.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("Person Form Case Note 003 updated")

                .ValidateSubject("Person Form Case Note 003 updated")
                .ValidateDescription("description goes here")

                .ValidatePersonFormFieldLinkText(personFormTitle)
                .ValidateReasonFieldLinkText("Assessment")
                .ValidatePriorityFieldLinkText("Normal")
                .ValidateDate("19/02/2021", "12:30")
                .ValidateStatus("Open")
                .ValidateContainsInformationProvidedByAThirdPartyYesOptionChecked(true)
                .ValidateContainsInformationProvidedByAThirdPartyNoOptionChecked(false)
                .ValidateResponsibleTeamFieldLinkText("Security T2")
                .ValidateResponsibleUserFieldLinkText("PFCaseNote User1")
                .ValidateCategoryFieldLinkText("Advice")
                .ValidateSubCategoryFieldLinkText("Home Support")
                .ValidateOutcomeFieldLinkText("More information needed")

                .ValidateSignificantEventYesOptionChecked(false)
                .ValidateSignificantEventNoOptionChecked(true)

                .ValidateIsClonedOptionChecked(false)
                .ValidateIsClonedNoOptionChecked(true)
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24874")]
        [Description(
            "Login in the web app - Open a Person Form record - Navigate to the Person Form Case Notes area - Open a Person Form Case Note record - " +
            "Update all editable fields - Tap on the save button - Validate that the Person Form record is saved")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonFormCaseNotes_UITestMethod06()
        {
            var personFormCaseNoteId = dbHelper.personFormCaseNote.CreatePersonFormCaseNote(_teamId, null, "Person Form Case Note 003", "", personFormID, _personId,
                null, null, null, null, null, false, null, null, null,
                DateTime.Now, false, 1, false);
            string personFormTitle = (string)dbHelper.personForm.GetPersonFormByID(personFormID, "title")["title"];

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .NavigateToPersonFormCaseNotesArea();

            personFormCaseNotesPage
                .WaitForPersonFormCaseNotesPageToLoad()
                .OpenPersonFormCaseNoteRecord(personFormCaseNoteId.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("Person Form Case Note 003")
                .InsertSubject("Person Form Case Note 003 updated")
                .InsertDescription("description goes here")
                .ClickReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activityReasonId.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("Person Form Case Note 003")
                .ClickPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activityPriorityId_Normal.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("Person Form Case Note 003")
                .InsertDate("19/02/2021", "12:30")
                .ClickContainsInformationProvidedByAThirdParty_YesRadioButton()
                .ClickResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("PFCaseNoteUser1").TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("Person Form Case Note 003")
                .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activityCategoryId.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("Person Form Case Note 003")
                .ClickSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activitySubCategoryId.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("Person Form Case Note 003")
                .ClickOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activityOutcomeId.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("Person Form Case Note 003")
                .ClickSaveButton()
                .ClickBackButton();

            personFormCaseNotesPage
                .WaitForPersonFormCaseNotesPageToLoad()
                .OpenPersonFormCaseNoteRecord(personFormCaseNoteId.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("Person Form Case Note 003 updated")

                .ValidateSubject("Person Form Case Note 003 updated")
                .ValidateDescription("description goes here")

                .ValidatePersonFormFieldLinkText(personFormTitle)
                .ValidateReasonFieldLinkText("Assessment")
                .ValidatePriorityFieldLinkText("Normal")
                .ValidateDate("19/02/2021", "12:30")
                .ValidateStatus("Open")
                .ValidateContainsInformationProvidedByAThirdPartyYesOptionChecked(true)
                .ValidateContainsInformationProvidedByAThirdPartyNoOptionChecked(false)
                .ValidateResponsibleTeamFieldLinkText("Security T2")
                .ValidateResponsibleUserFieldLinkText("PFCaseNote User1")
                .ValidateCategoryFieldLinkText("Advice")
                .ValidateSubCategoryFieldLinkText("Home Support")
                .ValidateOutcomeFieldLinkText("More information needed")

                .ValidateSignificantEventYesOptionChecked(false)
                .ValidateSignificantEventNoOptionChecked(true)

                .ValidateIsClonedOptionChecked(false)
                .ValidateIsClonedNoOptionChecked(true)
                ;

        }

        [TestProperty("JiraIssueID", "CDV6-24875")]
        [Description(
            "Login in the web app - Open a Person Form record - Navigate to the Person Form Case Notes area - Tap on the add new record button - " +
            "Set data in all fields - Tap on the save button - Validate that the record is saved")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonFormCaseNotes_UITestMethod07()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .NavigateToPersonFormCaseNotesArea();

            personFormCaseNotesPage
                .WaitForPersonFormCaseNotesPageToLoad()
                .ClickNewRecordButton();

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("New")
                .InsertSubject("Person Form Case Note 003")
                .InsertDescription("description goes here")
                .ClickReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activityReasonId.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("New")
                .ClickPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activityPriorityId_Normal.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("New")
                .InsertDate("19/02/2021", "12:30")
                .SelectStatus("Open")
                .ClickContainsInformationProvidedByAThirdParty_YesRadioButton()
                .ClickResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("PFCaseNoteUser1").TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("New")
                .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activityCategoryId.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("New")
                .ClickSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activitySubCategoryId.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("New")
                .ClickOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activityOutcomeId.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            personFormCaseNotesPage
                .WaitForPersonFormCaseNotesPageToLoad();

            var casenoterecords = dbHelper.personFormCaseNote.GetByPersonFormIdAndSubject(personFormID, "Person Form Case Note 003");
            Assert.AreEqual(1, casenoterecords.Count());

            string personFormTitle = (string)dbHelper.personForm.GetPersonFormByID(personFormID, "title")["title"];

            personFormCaseNotesPage
                .WaitForPersonFormCaseNotesPageToLoad()
                .OpenPersonFormCaseNoteRecord(casenoterecords[0].ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("Person Form Case Note 003")

                .ValidateSubject("Person Form Case Note 003")
                .ValidateDescription("description goes here")

                .ValidatePersonFormFieldLinkText(personFormTitle)
                .ValidateReasonFieldLinkText("Assessment")
                .ValidatePriorityFieldLinkText("Normal")
                .ValidateDate("19/02/2021", "12:30")
                .ValidateStatus("Open")
                .ValidateContainsInformationProvidedByAThirdPartyYesOptionChecked(true)
                .ValidateContainsInformationProvidedByAThirdPartyNoOptionChecked(false)
                .ValidateResponsibleTeamFieldLinkText("Security T2")
                .ValidateResponsibleUserFieldLinkText("PFCaseNote User1")
                .ValidateCategoryFieldLinkText("Advice")
                .ValidateSubCategoryFieldLinkText("Home Support")
                .ValidateOutcomeFieldLinkText("More information needed")

                .ValidateSignificantEventYesOptionChecked(false)
                .ValidateSignificantEventNoOptionChecked(true)

                .ValidateIsClonedOptionChecked(false)
                .ValidateIsClonedNoOptionChecked(true)
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24876")]
        [Description(
            "Login in the web app - Open a Person Form record - Navigate to the Person Form Case Notes area - Tap on the add new record button - " +
            "Set data in mandatory fields only - Tap on the save button - Validate that the record is saved")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonFormCaseNotes_UITestMethod08()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .NavigateToPersonFormCaseNotesArea();

            personFormCaseNotesPage
                .WaitForPersonFormCaseNotesPageToLoad()
                .ClickNewRecordButton();

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("New")
                .InsertSubject("Person Form Case Note 003")
                .InsertDate("19/02/2021", "12:30")
                .SelectStatus("Open")
                .ClickSaveAndCloseButton();

            personFormCaseNotesPage
                .WaitForPersonFormCaseNotesPageToLoad();

            var casenoterecords = dbHelper.personFormCaseNote.GetByPersonFormIdAndSubject(personFormID, "Person Form Case Note 003");
            Assert.AreEqual(1, casenoterecords.Count());

            string personFormTitle = (string)dbHelper.personForm.GetPersonFormByID(personFormID, "title")["title"];

            personFormCaseNotesPage
                .WaitForPersonFormCaseNotesPageToLoad()
                .OpenPersonFormCaseNoteRecord(casenoterecords[0].ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("Person Form Case Note 003")

                .ValidateSubject("Person Form Case Note 003")
                .ValidateDescription("")

                .ValidatePersonFormFieldLinkText(personFormTitle)
                .ValidateReasonFieldLinkText("")
                .ValidatePriorityFieldLinkText("")
                .ValidateDate("19/02/2021", "12:30")
                .ValidateStatus("Open")
                .ValidateContainsInformationProvidedByAThirdPartyYesOptionChecked(false)
                .ValidateContainsInformationProvidedByAThirdPartyNoOptionChecked(true)
                .ValidateResponsibleTeamFieldLinkText("Security T2")
                .ValidateResponsibleUserFieldLinkText("PFCaseNote User1")
                .ValidateCategoryFieldLinkText("")
                .ValidateSubCategoryFieldLinkText("")
                .ValidateOutcomeFieldLinkText("")

                .ValidateSignificantEventYesOptionChecked(false)
                .ValidateSignificantEventNoOptionChecked(true)

                .ValidateIsClonedOptionChecked(false)
                .ValidateIsClonedNoOptionChecked(true)
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24877")]
        [Description(
            "Login in the web app - Open a Person Form record - Navigate to the Person Form Case Notes area - Open a Person Form Case Note record - Tap on the delete button - " +
            "Confirm the delete operation - Validate that the record is deleted from the database")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonFormCaseNotes_UITestMethod09()
        {
            var personFormCaseNoteId = dbHelper.personFormCaseNote.CreatePersonFormCaseNote(_teamId, null, "Person Form Case Note 003", "", personFormID, _personId,
                null, null, null, null, null, false, null, null, null,
                DateTime.Now, false, 1, false);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .NavigateToPersonFormCaseNotesArea();

            personFormCaseNotesPage
                .WaitForPersonFormCaseNotesPageToLoad()
                .OpenPersonFormCaseNoteRecord(personFormCaseNoteId.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("Person Form Case Note 003")
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            personFormCaseNotesPage
                .WaitForPersonFormCaseNotesPageToLoad();

            var casenoterecords = dbHelper.personFormCaseNote.GetByPersonFormIdAndSubject(personFormID, "Person Form Case Note 003");
            Assert.AreEqual(0, casenoterecords.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-24878")]
        [Description(
            "Login in the web app - Open a Person Form record - Navigate to the Person Form Case Notes area - Tap on the add new record button - " +
            "Set data in all mandatory fields except for subject - Tap on the save button - " +
            "Validate that the user is prevented from saving the record.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonFormCaseNotes_UITestMethod10()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .NavigateToPersonFormCaseNotesArea();

            personFormCaseNotesPage
                .WaitForPersonFormCaseNotesPageToLoad()
                .ClickNewRecordButton();

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("New")
                .InsertDate("19/02/2021", "12:30")
                .SelectStatus("Open")
                .ClickSaveAndCloseButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.")
                .ValidateSubjectErrorLabelVisibility(true)
                .ValidatesubjectErrorLabelText("Please fill out this field.")
                .ValidateDateErrorLabelVisibility(false)
                .ValidateTimeErrorLabelVisibility(false)
                .ValidateStatusErrorLabelVisibility(false)
                ;

            var casenoterecords = dbHelper.personFormCaseNote.GetByPersonFormIdAndSubject(personFormID, "Person Form Case Note 003");
            Assert.AreEqual(0, casenoterecords.Count());
        }

        [TestProperty("JiraIssueID", "CDV6-24879")]
        [Description(
            "Login in the web app - Open a Person Form record - Navigate to the Person Form Case Notes area - Tap on the add new record button - " +
            "Set data in all mandatory fields except for Date - Tap on the save button - " +
            "Validate that the user is prevented from saving the record.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonFormCaseNotes_UITestMethod11()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .NavigateToPersonFormCaseNotesArea();

            personFormCaseNotesPage
                .WaitForPersonFormCaseNotesPageToLoad()
                .ClickNewRecordButton();

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("New")
                .InsertSubject("Person Form Case Note 003")
                .SelectStatus("Open")
                .ClickSaveAndCloseButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.")
                .ValidateSubjectErrorLabelVisibility(false)
                .ValidateDateErrorLabelVisibility(true)
                .ValidateDateErrorLabelText("Please fill out this field.")
                .ValidateTimeErrorLabelVisibility(true)
                .ValidateTimeErrorLabelText("Please fill out this field.")
                .ValidateStatusErrorLabelVisibility(false)
                ;

            var casenoterecords = dbHelper.personFormCaseNote.GetByPersonFormIdAndSubject(personFormID, "Person Form Case Note 003");
            Assert.AreEqual(0, casenoterecords.Count());
        }

        [TestProperty("JiraIssueID", "CDV6-24880")]
        [Description(
            "Login in the web app - Open a Person Form record - Navigate to the Person Form Case Notes area - Tap on the add new record button - " +
            "Set data in all mandatory fields except for Status - Tap on the save button - " +
            "Validate that the user is prevented from saving the record.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonFormCaseNotes_UITestMethod12()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .NavigateToPersonFormCaseNotesArea();

            personFormCaseNotesPage
                .WaitForPersonFormCaseNotesPageToLoad()
                .ClickNewRecordButton();

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("New")
                .InsertSubject("Person Form Case Note 003")
                .InsertDate("19/02/2021", "12:30")
                //.SelectStatus("Open")
                .ClickSaveAndCloseButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.")
                .ValidateSubjectErrorLabelVisibility(false)
                .ValidateDateErrorLabelVisibility(false)
                .ValidateTimeErrorLabelVisibility(false)
                .ValidateStatusErrorLabelVisibility(true)
                .ValidateStatusErrorLabelText("Please fill out this field.")
                ;

            var casenoterecords = dbHelper.personFormCaseNote.GetByPersonFormIdAndSubject(personFormID, "Person Form Case Note 003");
            Assert.AreEqual(0, casenoterecords.Count());
        }

        [TestProperty("JiraIssueID", "CDV6-24881")]
        [Description(
            "Login in the web app - Open a Person Form record - Navigate to the Person Form Case Notes area - Tap on the add new record button - " +
            "Set data in all mandatory fields - Set Category to Advice - Tap on the Sub_Category button - Validate that the displayed results are filtered accoarding to the selected category")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonFormCaseNotes_UITestMethod13()
        {
            #region Activity Sub Categories
            var _mentalHealthCareContactActivityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("fc599e31-5f75-e911-a2c5-005056926fe4"), "Mental Health Care Contacts", new DateTime(2020, 1, 1), _teamId);
            var activitySubCategory2ID = commonMethodsDB.CreateActivitySubCategory(new Guid("405f28e8-6075-e911-a2c5-005056926fe4"), "Carers Assessment", new DateTime(2019, 1, 1), _mentalHealthCareContactActivityCategoryId, _teamId);
            var activitySubCategory3ID = commonMethodsDB.CreateActivitySubCategory(new Guid("95859a60-5f75-e911-a2c5-005056926fe4"), "Core Assessment", new DateTime(2019, 1, 1), _mentalHealthCareContactActivityCategoryId, _teamId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .NavigateToPersonFormCaseNotesArea();

            personFormCaseNotesPage
                .WaitForPersonFormCaseNotesPageToLoad()
                .ClickNewRecordButton();

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("New")
                .InsertSubject("Person Form Case Note 003")
                .InsertDescription("description goes here")
                .InsertDate("19/02/2021", "12:30")
                .SelectStatus("Open")
                .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_activityCategoryId.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("New")
                .ClickSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(_activitySubCategoryId.ToString())
                .ValidateResultElementNotPresent(activitySubCategory2ID.ToString())
                .ValidateResultElementNotPresent(activitySubCategory3ID.ToString());
        }


        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-8611 && https://advancedcsg.atlassian.net/browse/CDV6-8614

        [TestProperty("JiraIssueID", "CDV6-24882")]
        [Description(
            "Login in the web app - Open a Person Form record - Navigate to the Person Form Case Notes area - Open a Person Form Case Note record (all fields must have values) - " +
            "Wait for the Person Form Case Notes record page to load - Click on the Clone Case Note Button - Validate that the clone popup is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ClonePersonFormCaseNotes_UITestMethod01()
        {
            Guid personFormCaseNote1ID = dbHelper.personFormCaseNote.CreatePersonFormCaseNote(_teamId, _systemUserId, "Person Form Case Note 001",
                "Person Form Case Note 001 description", personFormID, _personId, _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId_Normal, false, null, null,
                null, new DateTime(2021, 2, 19, 15, 20, 00), true, 1, false);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .NavigateToPersonFormCaseNotesArea();

            personFormCaseNotesPage
                .WaitForPersonFormCaseNotesPageToLoad()
                .OpenPersonFormCaseNoteRecord(personFormCaseNote1ID.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("Person Form Case Note 001")
                .ClickCloneButton();

            cloneActivityPopup
                .WaitForCloneActivityPopupToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-24883")]
        [Description(
            "Login in the web app - Open a Person Form record - Navigate to the Person Form Case Notes area - Open a Person Form Case Note record (all fields must have values) - " +
            "Wait for the Person Form Case Notes record page to load - Click on the Clone Case Note Button - Wait for the the clone popup to load - " +
            "Set the 'Clone Activity to' picklist to Person - Select the person record - Tap on the Clone Button - Wait for the Clone popup to be closed - " +
            "Validate that the a new Case Note is associated with the person record - Validate that all fields match the original case note")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ClonePersonFormCaseNotes_UITestMethod02()
        {
            Guid personFormCaseNote1ID = dbHelper.personFormCaseNote.CreatePersonFormCaseNote(_teamId, _systemUserId, "Person Form Case Note 001",
                "Person Form Case Note 001 description", personFormID, _personId, _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId_Normal, false, null, null,
                null, new DateTime(2021, 2, 19, 15, 20, 00), true, 1, false);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .NavigateToPersonFormCaseNotesArea();

            personFormCaseNotesPage
                .WaitForPersonFormCaseNotesPageToLoad()
                .OpenPersonFormCaseNoteRecord(personFormCaseNote1ID.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("Person Form Case Note 001")
                .ClickCloneButton();

            cloneActivityPopup
                .WaitForCloneActivityPopupToLoad()
                .SelectBusinessObjectTypeText("Person")
                .SelectRecord(_personId.ToString())
                .ClickCloneButton();

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("Person Form Case Note 001");

            var records = dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(_personId);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.personCaseNote.GetPersonCaseNoteByID(records[0],
                "subject", "notes", "personid", "activityreasonid", "activitypriorityid", "casenotedate",
                "statusid", "informationbythirdparty", "ownerid", "responsibleuserid", "activitycategoryid",
                "activitysubcategoryid", "activityoutcomeid", "issignificantevent", "iscloned", "clonedfromid", "clonedfromidtablename", "clonedfromidname");

            Assert.AreEqual("Person Form Case Note 001", fields["subject"]);
            Assert.AreEqual("Person Form Case Note 001 description", fields["notes"]);
            Assert.AreEqual(_personId.ToString(), fields["personid"].ToString());
            Assert.AreEqual(_activityReasonId.ToString(), fields["activityreasonid"].ToString());
            Assert.AreEqual(_activityPriorityId_Normal.ToString(), fields["activitypriorityid"].ToString());
            Assert.AreEqual(new DateTime(2021, 2, 19, 15, 20, 00), ((DateTime)fields["casenotedate"]).ToLocalTime());
            Assert.AreEqual(1, fields["statusid"]);
            Assert.AreEqual(true, fields["informationbythirdparty"]);
            Assert.AreEqual(_teamId.ToString(), fields["ownerid"].ToString());
            Assert.AreEqual(_systemUserId.ToString(), fields["responsibleuserid"].ToString());
            Assert.AreEqual(_activityCategoryId.ToString(), fields["activitycategoryid"].ToString());
            Assert.AreEqual(_activitySubCategoryId.ToString(), fields["activitysubcategoryid"].ToString());
            Assert.AreEqual(_activityOutcomeId.ToString(), fields["activityoutcomeid"].ToString());
            Assert.AreEqual(false, fields["issignificantevent"]);
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(personFormCaseNote1ID.ToString(), fields["clonedfromid"].ToString());
            Assert.AreEqual("personformcasenote", fields["clonedfromidtablename"]);
            Assert.AreEqual("Person Form Case Note 001", fields["clonedfromidname"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24884")]
        [Description(
            "Login in the web app - Open a Person Form record - Navigate to the Person Form Case Notes area - Open a Person Form Case Note record (all fields must have values) - " +
            "Wait for the Person Form Case Notes record page to load - Click on the Clone Case Note Button - Wait for the the clone popup to load - " +
            "Set the 'Clone Activity to' picklist to Case - Select a Case record - Tap on the Clone Button - Wait for the Clone popup to be closed - " +
            "Validate that the a new Case Note is associated with the Case record - Validate that all fields match the original case note")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ClonePersonFormCaseNotes_UITestMethod03()
        {
            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _teamId);

            #endregion

            #region Case

            Guid case1ID = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, _personId, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 1, 1, 8, 0, 0), new DateTime(2020, 1, 1, 9, 0, 0), 20);

            #endregion

            Guid personFormCaseNote1ID = dbHelper.personFormCaseNote.CreatePersonFormCaseNote(_teamId, _systemUserId, "Person Form Case Note 001",
                "Person Form Case Note 001 description", personFormID, _personId, _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId_Normal, false, null, null,
                null, new DateTime(2021, 2, 19, 15, 20, 00), true, 1, false);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .NavigateToPersonFormCaseNotesArea();

            personFormCaseNotesPage
                .WaitForPersonFormCaseNotesPageToLoad()
                .OpenPersonFormCaseNoteRecord(personFormCaseNote1ID.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("Person Form Case Note 001")
                .ClickCloneButton();

            cloneActivityPopup
                .WaitForCloneActivityPopupToLoad()
                .SelectBusinessObjectTypeText("Case")
                .SelectRecord(case1ID.ToString())
                .ClickCloneButton();

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("Person Form Case Note 001");

            var records = dbHelper.caseCaseNote.GetByCaseID(case1ID);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.caseCaseNote.GetByID(records[0],
                "subject", "notes", "personid", "caseid", "activityreasonid", "activitypriorityid", "casenotedate",
                "statusid", "informationbythirdparty", "ownerid", "responsibleuserid", "activitycategoryid",
                "activitysubcategoryid", "activityoutcomeid", "issignificantevent", "iscloned", "clonedfromid", "clonedfromidtablename", "clonedfromidname");

            Assert.AreEqual("Person Form Case Note 001", fields["subject"]);
            Assert.AreEqual("Person Form Case Note 001 description", fields["notes"]);
            Assert.AreEqual(_personId.ToString(), fields["personid"].ToString());
            Assert.AreEqual(case1ID.ToString(), fields["caseid"].ToString());
            Assert.AreEqual(_activityReasonId.ToString(), fields["activityreasonid"].ToString());
            Assert.AreEqual(_activityPriorityId_Normal.ToString(), fields["activitypriorityid"].ToString());
            Assert.AreEqual(new DateTime(2021, 2, 19, 15, 20, 00), ((DateTime)fields["casenotedate"]).ToLocalTime());
            Assert.AreEqual(1, fields["statusid"]);
            Assert.AreEqual(true, fields["informationbythirdparty"]);
            Assert.AreEqual(_teamId.ToString(), fields["ownerid"].ToString());
            Assert.AreEqual(_systemUserId.ToString(), fields["responsibleuserid"].ToString());
            Assert.AreEqual(_activityCategoryId.ToString(), fields["activitycategoryid"].ToString());
            Assert.AreEqual(_activitySubCategoryId.ToString(), fields["activitysubcategoryid"].ToString());
            Assert.AreEqual(_activityOutcomeId.ToString(), fields["activityoutcomeid"].ToString());
            Assert.AreEqual(false, fields["issignificantevent"]);
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(personFormCaseNote1ID.ToString(), fields["clonedfromid"].ToString());
            Assert.AreEqual("personformcasenote", fields["clonedfromidtablename"]);
            Assert.AreEqual("Person Form Case Note 001", fields["clonedfromidname"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24885")]
        [Description(
            "Login in the web app - Open a Person Form record - Navigate to the Person Form Case Notes area - Open a Person Form Case Note record (all fields must have values) - " +
            "Wait for the Person Form Case Notes record page to load - Click on the Clone Case Note Button - Wait for the the clone popup to load - " +
            "Set the 'Clone Activity to' picklist to Case - Select a multiple Case record - Tap on the Clone Button - Wait for the Clone popup to be closed - " +
            "Validate that the a new Case Note is associated with the each selected Case record - Validate that all fields match the original case note")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ClonePersonFormCaseNotes_UITestMethod04()
        {
            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _teamId);

            #endregion

            #region Case

            Guid case1ID = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, _personId, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 1, 1, 8, 0, 0), new DateTime(2020, 1, 1, 9, 0, 0), 20);
            Guid case2ID = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, _personId, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 1, 2, 8, 0, 0), new DateTime(2020, 1, 2, 9, 0, 0), 20);

            #endregion

            Guid personFormCaseNote1ID = dbHelper.personFormCaseNote.CreatePersonFormCaseNote(_teamId, _systemUserId, "Person Form Case Note 001",
                "Person Form Case Note 001 description", personFormID, _personId, _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId_Normal, false, null, null,
                null, new DateTime(2021, 2, 19, 15, 20, 00), true, 1, false);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .NavigateToPersonFormCaseNotesArea();

            personFormCaseNotesPage
                .WaitForPersonFormCaseNotesPageToLoad()
                .OpenPersonFormCaseNoteRecord(personFormCaseNote1ID.ToString());

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("Person Form Case Note 001")
                .ClickCloneButton();

            cloneActivityPopup
                .WaitForCloneActivityPopupToLoad()
                .SelectBusinessObjectTypeText("Case")
                .SelectRecord(case1ID.ToString())
                .SelectRecord(case2ID.ToString())
                .ClickCloneButton();

            personFormCaseNoteRecordPage
                .WaitForPersonFormCaseNoteRecordPageToLoad("Person Form Case Note 001");


            //check the case notes for the 1st case record
            var records = dbHelper.caseCaseNote.GetByCaseID(case1ID);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.caseCaseNote.GetByID(records[0],
                "subject", "notes", "personid", "caseid", "activityreasonid", "activitypriorityid", "casenotedate",
                "statusid", "informationbythirdparty", "ownerid", "responsibleuserid", "activitycategoryid",
                "activitysubcategoryid", "activityoutcomeid", "issignificantevent", "iscloned", "clonedfromid", "clonedfromidtablename", "clonedfromidname");

            var expectedDate = new DateTime(2021, 2, 19, 15, 20, 0);

            Assert.AreEqual("Person Form Case Note 001", fields["subject"]);
            Assert.AreEqual("Person Form Case Note 001 description", fields["notes"]);
            Assert.AreEqual(_personId.ToString(), fields["personid"].ToString());
            Assert.AreEqual(case1ID.ToString(), fields["caseid"].ToString());
            Assert.AreEqual(_activityReasonId.ToString(), fields["activityreasonid"].ToString());
            Assert.AreEqual(_activityPriorityId_Normal.ToString(), fields["activitypriorityid"].ToString());
            Assert.AreEqual(expectedDate, ((DateTime)fields["casenotedate"]).ToLocalTime());
            Assert.AreEqual(1, fields["statusid"]);
            Assert.AreEqual(true, fields["informationbythirdparty"]);
            Assert.AreEqual(_teamId.ToString(), fields["ownerid"].ToString());
            Assert.AreEqual(_systemUserId.ToString(), fields["responsibleuserid"].ToString());
            Assert.AreEqual(_activityCategoryId.ToString(), fields["activitycategoryid"].ToString());
            Assert.AreEqual(_activitySubCategoryId.ToString(), fields["activitysubcategoryid"].ToString());
            Assert.AreEqual(_activityOutcomeId.ToString(), fields["activityoutcomeid"].ToString());
            Assert.AreEqual(false, fields["issignificantevent"]);
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(personFormCaseNote1ID.ToString(), fields["clonedfromid"].ToString());
            Assert.AreEqual("personformcasenote", fields["clonedfromidtablename"]);
            Assert.AreEqual("Person Form Case Note 001", fields["clonedfromidname"]);



            //check the case notes for the 2nd case record
            records = dbHelper.caseCaseNote.GetByCaseID(case2ID);
            Assert.AreEqual(1, records.Count);

            fields = dbHelper.caseCaseNote.GetByID(records[0],
                "subject", "notes", "personid", "caseid", "activityreasonid", "activitypriorityid", "casenotedate",
                "statusid", "informationbythirdparty", "ownerid", "responsibleuserid", "activitycategoryid",
                "activitysubcategoryid", "activityoutcomeid", "issignificantevent", "iscloned", "clonedfromid", "clonedfromidtablename", "clonedfromidname");

            Assert.AreEqual("Person Form Case Note 001", fields["subject"]);
            Assert.AreEqual("Person Form Case Note 001 description", fields["notes"]);
            Assert.AreEqual(_personId.ToString(), fields["personid"].ToString());
            Assert.AreEqual(case2ID.ToString(), fields["caseid"].ToString());
            Assert.AreEqual(_activityReasonId.ToString(), fields["activityreasonid"].ToString());
            Assert.AreEqual(_activityPriorityId_Normal.ToString(), fields["activitypriorityid"].ToString());
            Assert.AreEqual(expectedDate, ((DateTime)fields["casenotedate"]).ToLocalTime());
            Assert.AreEqual(1, fields["statusid"]);
            Assert.AreEqual(true, fields["informationbythirdparty"]);
            Assert.AreEqual(_teamId.ToString(), fields["ownerid"].ToString());
            Assert.AreEqual(_systemUserId.ToString(), fields["responsibleuserid"].ToString());
            Assert.AreEqual(_activityCategoryId.ToString(), fields["activitycategoryid"].ToString());
            Assert.AreEqual(_activitySubCategoryId.ToString(), fields["activitysubcategoryid"].ToString());
            Assert.AreEqual(_activityOutcomeId.ToString(), fields["activityoutcomeid"].ToString());
            Assert.AreEqual(false, fields["issignificantevent"]);
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(personFormCaseNote1ID.ToString(), fields["clonedfromid"].ToString());
            Assert.AreEqual("personformcasenote", fields["clonedfromidtablename"]);
            Assert.AreEqual("Person Form Case Note 001", fields["clonedfromidname"]);
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

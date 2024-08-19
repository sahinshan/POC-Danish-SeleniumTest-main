using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.DailyCareBOs
{

    /// <summary>
    /// This class contains Automated UI test scripts for Daily Care BO > Keyworker Note
    /// </summary>
    [TestClass]
    public class DailyCareBO_UITestCases : FunctionalTest
    {
        #region Properties
        private Guid _languageId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private Guid _maritalStatusId;
        private Guid _systemUserId;
        private Guid _personID;
        private string _personFullName;
        private string _systemUsername;
        private Guid _documentTypeId;
        private Guid _documentSubTypeId;

        private string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");

        #endregion

        [TestInitialize()]
        public void DailyCare_BO_SetupTest()
        {

            try
            {
                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];
                var _defaultSystemUserFullName = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(defaultSystemUserId, "fullname")["fullname"];
                var _localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, _localZone.StandardName);

                #endregion

                #region Business Unit
                _businessUnitId = commonMethodsDB.CreateBusinessUnit("DailyCareBU");

                #endregion

                #region Providers

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("DailyCareTeam", null, _businessUnitId, "907678", "DailyCareTeam@careworkstempmail.com", "DailyCareTeam", "020 123456");

                #endregion

                #region Marital Status

                _maritalStatusId = commonMethodsDB.CreateMaritalStatus("Civil Partner", new DateTime(2000, 1, 1), _teamId);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Lanuage

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region SecurityProfiles

                var userSecProfiles = new List<Guid>()
                {
                dbHelper.securityProfile.GetSecurityProfileByName("Security Management Access")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("Settings Area Access")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("Person (Edit)")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("Person Module (Edit)")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("Daily Care (Edit)")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("System User (Edit)")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (Edit)")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("Can Manage Reference Data")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Reference Data (Edit)")[0]
            };

                #endregion

                #region Create SystemUser 

                _systemUsername = "DailyCareUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "DailyCare", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, userSecProfiles);
                dbHelper.systemUser.UpdateEmployeeTypeId(_systemUserId, 3);

                #endregion

                #region Person

                var firstName = "Ariel";
                var lastName = currentTimeString;
                var addresstypeid = 6; //Home

                _personID = commonMethodsDB.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, new DateTime(2020, 10, 20), addresstypeid, 1, "9876543210", "", "1234567890", "",
                "pna", "pno", "st", "dist", "tow", "cou", "CR0 3RL");
                _personFullName = firstName + " " + lastName;

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }

        #region https://advancedcsg.atlassian.net/browse/ACC-8181

        [TestProperty("JiraIssueID", "ACC-2217")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-2217 - " +
            "To verify the new updates for keyworker notes BO.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [DeploymentItem("Files\\video.mp4"), DeploymentItem("chromedriver.exe")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Person Keyworker Notes")]
        public void DailyCareBO_UITestMethod001()
        {
            var todayDate = commonMethodsHelper.GetCurrentDateWithoutCulture();

            #region Document Type

            _documentTypeId = commonMethodsDB.CreateAttachDocumentType(_teamId, "Daily Care Support Information", new DateTime(2019, 1, 1));

            #endregion

            #region Document Sub Type

            _documentSubTypeId = commonMethodsDB.CreateAttachDocumentSubType(_teamId, "Daily Care Support Documentation", new DateTime(2019, 1, 1), _documentTypeId);

            #endregion

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByLastName(currentTimeString)
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, false)
                .NavigateToKeyworkerNotesPage();

            personKeyworkerNotesPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            personKeyworkerNoteRecordPage
                .WaitForPageToLoad()
                .VerifyPersonLookupFieldLinkText(_personFullName)
                .VerifyDateAndTimeOccurredFieldsAreDisplayed(true);

            #endregion

            #region Step 2

            personKeyworkerNoteRecordPage
                .VerifyTotalTimeSpentWithClientMinutesFieldIsDisplayed(true)
                .VerifyKeyworkerNotesTextAreaFieldIsDisplayed(true);

            personKeyworkerNoteRecordPage
                .InsertTotalTimeSpentWithClientMinutes("Test8181")
                .VerifyTotalTimeSpentWithClientMinutesFieldErrorText("Please enter a value between 1 and 1440.");

            #endregion

            #region Step 3

            var DateOccurred = todayDate.AddDays(-2).AddHours(1);

            personKeyworkerNoteRecordPage
                .SetDateOccurred(DateOccurred.ToString("dd'/'MM'/'yyyy"))
                .SetTimeOccurred(DateOccurred.ToString("HH:mm"))
                .InsertTotalTimeSpentWithClientMinutes("15")
                .InsertKeyworkerNotesText("Test8181 " + currentTimeString)
                .ClickSave()
                .WaitForPageToLoad();

            personKeyworkerNoteRecordPage
                .WaitForPageToLoad()
                .VerifyTotalTimeSpentWithClientMinutesFieldText("15")
                .VerifyDateAndTimeOccurredDateFieldText(DateOccurred.ToString("dd'/'MM'/'yyyy"))
                .VerifyDateAndTimeOccurredTimeFieldText(DateOccurred.ToString("HH:mm"));

            //Verify Resident Section
            personKeyworkerNoteRecordPage
                .VerifyResidentVoiceSectionIsDisplayed(true)
                .WaitForResidentVoiceSectionToLoad();

            //Verify Attachment can be added
            personKeyworkerNoteRecordPage
                .ClickNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("cpkeyworkernotesattachment")
                .ClickOnExpandIcon();

            string dateText = DateOccurred.ToString("dd'/'MM'/'yyyy");
            string timeText = DateOccurred.ToString("HH:mm");

            keyworkerNotesAttachmentRecordPage
                .WaitForPageToLoad()
                .ValidateFileFieldIsDisplayed()
                .InsertTextOnTitle("Test " + currentTimeString)
                .InsertTextOnDate(dateText)
                .InsertTextOnDate_Time(timeText)
                .ClickDocumentTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Daily Care Support Information", _documentTypeId);

            keyworkerNotesAttachmentRecordPage
                .WaitForPageToLoad()
                .ClickDocumentSubTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Daily Care Support Documentation", _documentSubTypeId);

            keyworkerNotesAttachmentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnCaption("Caption " + currentTimeString)
                .UploadFile(TestContext.DeploymentDirectory + "\\video.mp4")
                .WaitForPageToLoad()
                .ClickSaveButton()
                .WaitForPageToLoad();

            keyworkerNotesAttachmentRecordPage
                .WaitForPageToLoad()
                .ValidateTitleText("Test " + currentTimeString)
                .ValidateDateText(dateText)
                .ValidateDate_TimeText(timeText)
                .ValidateCaptionText("Caption " + currentTimeString)
                .ValidateFileLinkText("video.mp4");

            var keyworkerNotesAttachmentId = dbHelper.cpKeyworkerNotesAttachment.GetByPersonID(_personID)[0];

            keyworkerNotesAttachmentRecordPage
                .ClickBackButton();

            personKeyworkerNoteRecordPage
                .WaitForPageToLoad()
                .WaitForResidentVoiceSectionToLoad()
                .ValidateRecordIsPresent(keyworkerNotesAttachmentId);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-8182

        [TestProperty("JiraIssueID", "ACC-2347")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-2347 - " +
            "To verify the new field \"Preferences\" under field person in following BO’s Welfare Check, Mobility, Turning, Daily Personal Care.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Welfare Check")]
        [TestProperty("Screen2", "Person Mobility")]
        [TestProperty("Screen3", "Person Repositioning")]
        [TestProperty("Screen4", "Person Daily Personal Care")]
        public void DailyCareBO_UITestMethod002()
        {

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByLastName(currentTimeString)
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, false)
                .NavigateToWelfareChecksPage();

            personWelfareChecksPage
                .WaitForPageToLoad(false)
                .ClickNewRecordButton();

            personWelfareCheckRecordPage
                .WaitForPageToLoad()
                .VerifyPreferencesTextAreaFieldIsDisplayed(true)
                .VerifyPreferencesTextAreaFieldMaxLength("4000")
                .VerifyPreferencesTextAreaFieldIsDisabled(true);

            personWelfareCheckRecordPage
                .WaitForPageToLoad()
                .VerifyPreferencesTextAreaFieldText("No preferences recorded, please check with Ariel.")
                .ClickBack();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            #endregion

            #region Step 2

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, false)
                .NavigateToPersonMobilityPage();

            personMobilityPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            personMobilityRecordPage
                .WaitForPageToLoad()
                .VerifyPreferencesTextAreaFieldIsDisplayed(true)
                .VerifyPreferencesTextAreaFieldMaxLength("4000")
                .VerifyPreferencesTextAreaFieldIsDisabled(true)
                .VerifyPreferencesTextAreaFieldText("No preferences recorded, please check with Ariel.")
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            #endregion

            #region Step 3

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, false)
                .NavigateToRepositioningPage();

            personRepositioningPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            personRepositioningRecordPage
                .WaitForPageToLoad()
                .VerifyPreferencesTextAreaFieldIsDisplayed(true)
                .VerifyPreferencesTextAreaFieldMaxLength("4000")
                .VerifyPreferencesTextAreaFieldIsDisabled(true)
                .VerifyPreferencesTextAreaFieldText("No preferences recorded, please check with Ariel.")
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            #endregion

            #region Step 4

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, false)
                .NavigateToDailyPersonalCarePage();

            personDailyPersonalCarePage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .VerifyPreferencesTextAreaFieldIsDisplayed(true)
                .VerifyPreferencesTextAreaFieldMaxLength("4000")
                .VerifyPreferencesTextAreaFieldIsDisabled(true)
                .VerifyPreferencesTextAreaFieldText("No preferences recorded, please check with Ariel.");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-8333

        [TestProperty("JiraIssueID", "ACC-2727")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-2727 - " +
            "To verify the system view Mobility BO.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Person Mobility")]
        public void DailyCareBO_UITestMethod003()
        {

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByLastName(currentTimeString)
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, false)
                .NavigateToPersonMobilityPage();

            personMobilityPage
                .WaitForPageToLoad()
                .ValidateHeaderCellText(2, "Date and Time Occurred")
                .ValidateHeaderCellText(3, "Consent Given?")
                .ValidateHeaderCellText(4, "Non-consent Detail");

            #endregion

            #region Step 2

            personMobilityPage
                .ValidateHeaderCellNotPresent("Distance mobilised")
                .ValidateHeaderCellNotPresent("Location");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2726")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-2726 - " +
            "verify deprecated & updated fields n mobility BO in Web app")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Person Mobility")]
        public void DailyCareBO_UITestMethod004()
        {

            #region Person Mobility

            var personMobilityId = dbHelper
                .cPPersonMobility
                .CreatePersonMobility(_personID, _teamId, _businessUnitId, DateTime.Now.ToUniversalTime(), 2, 3, DateTime.Now.Date, 1, DateTime.Now.AddHours(1).TimeOfDay, null);

            #endregion

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByLastName(currentTimeString)
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, false)
                .NavigateToPersonMobilityPage();

            personMobilityPage
                .WaitForPageToLoad()
                .ValidateHeaderCellNotPresent("Distance mobilised")
                .ValidateHeaderCellNotPresent("Location");

            #endregion

            #region Step 2

            personMobilityPage
                .OpenRecord(personMobilityId);

            personMobilityRecordPage
                .WaitForPageToLoad()
                .ValidateDeferredToDateFieldLabel("Deferred To Date?")
                .ValidateDeferredToDateFieldVisible(true)
                .VerifyDeferredToDate_DatePickerIsDisplayed(true);

            personMobilityRecordPage
                .ValidateSelectedNonConsentDetail("Deferred")
                .ValidateDeferredToDateFieldVisible(true)
                .SelectNonConsentDetail("Declined")
                .ValidateDeferredToDateFieldVisible(false);

            personMobilityRecordPage
                .WaitForPageToLoad()
                .SelectNonConsentDetail("Deferred")
                .SetDeferredToDate(DateTime.Now.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("\"Deferred To?\" cannot be before today.")
                .TapCloseButton();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-8381

        [TestProperty("JiraIssueID", "ACC-2728")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-2728 - " +
            "To verify the values in the Business object of Mobility BO.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Person Mobility")]
        public void DailyCareBO_UITestMethod005()
        {

            #region Login user and reference data

            var _systemUsername2 = "DailyCareUserAdmin";
            commonMethodsDB.CreateSystemUserRecord(_systemUsername2, "DailyCare", "UserAdmin", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            var CpPersonMobilityBoId = dbHelper.businessObject.GetBusinessObjectByName("CPPersonMobility")[0];
            var MobilityDistanceUnitBoId = dbHelper.businessObject.GetBusinessObjectByName("MobilityDistanceUnit")[0];

            #endregion

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername2, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCustomizationsSection();

            customizationsPage
                .WaitForCustomizationsPageToLoad()
                .ClickBusinessObjectsButton();

            businessObjectsPage
                .WaitForBusinessObjectsPageToLoad()
                .InsertQuickSearchText("Mobility")
                .ClickQuickSearchButton()
                .WaitForBusinessObjectsPageToLoad()
                .ValidateRecordIsPresent(MobilityDistanceUnitBoId.ToString(), true);

            businessObjectsPage
                .InsertQuickSearchText("CPPersonMobility")
                .ClickQuickSearchButton()
                .WaitForBusinessObjectsPageToLoad();

            businessObjectsPage
                .OpenRecord(CpPersonMobilityBoId.ToString());

            businessObjectRecordPage
                .WaitForBusinessObjectRecordPageToLoad()
                .ValidateOwnershipFieldVisible("Team Based")
                .ValidateBusinessModuleFieldValue("Care Provider Care Plan");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickSignOutButton();

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("mobility")
                .TapSearchButton()
                .ValidateReferenceDataMainHeaderVisibility("Care Provider Care Plan", true)
                .ClickReferenceDataMainHeader("Care Provider Care Plan")
                .ValidateReferenceDataElementVisibility("Mobility Distance Units", true);


            #endregion

        }


        #endregion

        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.DailyCareBOs
{

    /// <summary>
    /// This class contains Automated UI test scripts for Daily Care BO
    /// </summary>
    [TestClass]
    public class DailyCareBO_ACC_3603_UITestCases : FunctionalTest
    {
        #region Properties
        private string _tenantName;
        private Guid _languageId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private Guid _maritalStatusId;
        private Guid _systemUserId;
        private Guid _personID;
        private string _systemUsername;
        private string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid defaultTeamId;

        #endregion

        [TestInitialize()]
        public void DailyCare_KeyworkerBO_SetupTest()
        {

            try
            {

                #region Environment Name

                _tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

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

                #region Create SystemUser 

                _systemUsername = "DailyCareUser3";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "DailyCare", "User3", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);


                #region Team membership

                defaultTeamId = dbHelper.team.GetFirstTeams(1, 1, true)[0];
                commonMethodsDB.CreateTeamMember(defaultTeamId, _systemUserId, new DateTime(2024, 6, 1), null);

                #endregion

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }

        #region https://advancedcsg.atlassian.net/browse/ACC-8471

        [TestProperty("JiraIssueID", "ACC-3603")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-3603 - " +
            "Verify that user can create attachments for Key Worker Notes BO")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [DeploymentItem("Files\\video.mp4"), DeploymentItem("Files\\ImageFile.jpeg"), DeploymentItem("chromedriver.exe")]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Person Keyworker Notes")]
        public void DailyCareBO_ACC3603_UITestMethod001()
        {
            #region Person

            var firstName = "Andre";
            var lastName = currentTimeString;
            var addresstypeid = 6; //Home

            _personID = commonMethodsDB.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, new DateTime(2020, 10, 20), addresstypeid, 1, "9876543210", "", "1234567890", "",
            "pna", "pno", "st", "dist", "tow", "cou", "CR0 3RL");
            var _personFullName = firstName + " " + lastName;

            #endregion

            #region Document Type

            var _documentTypeId1 = commonMethodsDB.CreateAttachDocumentType(_teamId, "Daily Care Support Information", new DateTime(2019, 1, 1));
            var _documentTypeId2 = commonMethodsDB.CreateAttachDocumentType(_teamId, "Policy", new DateTime(2022, 7, 20));

            #endregion

            #region Document Sub Type

            var _documentSubTypeId1 = commonMethodsDB.CreateAttachDocumentSubType(_teamId, "Daily Care Support Documentation", new DateTime(2019, 1, 1), _documentTypeId1);
            var _documentSubTypeId2 = commonMethodsDB.CreateAttachDocumentSubType(_teamId, "Accident/Incident", new DateTime(2022, 7, 20), _documentTypeId2);

            #endregion

            var todayDate = commonMethodsHelper.GetCurrentDateWithoutCulture();

            var CpCPKeyWorkerNotesAttachmentBoId = dbHelper.businessObject.GetBusinessObjectByName("CPKeyWorkerNotesAttachment")[0];

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCustomizationsSection();

            customizationsPage
                .WaitForCustomizationsPageToLoad()
                .ClickBusinessObjectsButton();

            businessObjectsPage
                .WaitForBusinessObjectsPageToLoad()
                .InsertQuickSearchText("CPKeyWorkerNotesAttachment")
                .ClickQuickSearchButton()
                .WaitForBusinessObjectsPageToLoad()
                .OpenRecord(CpCPKeyWorkerNotesAttachmentBoId.ToString());

            businessObjectRecordPage
                .WaitForBusinessObjectRecordPageToLoad()
                .ValidateNameFieldValue("cpkeyworkernotesattachment")
                .ValidateTypeFieldValue("Attachment")
                .ValidateSingularNameFieldValue("Attachment (For Keyworker Notes)")
                .ValidatePluralNameFieldValue("Attachments (For Keyworker Notes)")
                .ValidateOwnershipFieldVisible("Team Based")
                .ValidateBusinessModuleFieldValue("Care Provider Care Plan")
                .ValidateDescriptionFieldValue("Attachments related to Keyworker Notes records");

            #endregion

            #region Step 3

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
                .WaitForPageToLoad();

            #endregion

            #region Step 4

            personKeyworkerNotesPage
                .ClickNewRecordButton();

            personKeyworkerNoteRecordPage
                .WaitForPageToLoad()
                .SetDateOccurred(todayDate.ToString("dd'/'MM'/'yyyy"))
                .SetTimeOccurred(todayDate.ToUniversalTime().AddHours(-1).ToString("HH:mm"))
                .InsertTotalTimeSpentWithClientMinutes("15")
                .InsertKeyworkerNotesText("Test3603 " + currentTimeString)
                .ClickSave()
                .WaitForPageToLoad();

            personKeyworkerNoteRecordPage
                .WaitForPageToLoad()
                .VerifyTotalTimeSpentWithClientMinutesFieldText("15")
                .VerifyDateAndTimeOccurredDateFieldText(todayDate.ToString("dd'/'MM'/'yyyy"))
                .VerifyDateAndTimeOccurredTimeFieldText(todayDate.ToUniversalTime().AddHours(-1).ToString("HH:mm"))
                .VerifyKeyworkerNotesTextAreaFieldText("Test3603 " + currentTimeString);

            #endregion

            #region Step 5

            //Verify Resident Section
            personKeyworkerNoteRecordPage
                .VerifyResidentVoiceSectionIsDisplayed(true)
                .WaitForResidentVoiceSectionToLoad();

            #endregion

            #region Step 6

            //Verify Attachment can be added
            personKeyworkerNoteRecordPage
                .ClickNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("cpkeyworkernotesattachment")
                .ClickOnExpandIcon();

            string dateText = todayDate.ToString("dd'/'MM'/'yyyy");
            string timeText = todayDate.ToUniversalTime().AddHours(1).ToString("HH:mm");

            keyworkerNotesAttachmentRecordPage
                .WaitForPageToLoad()
                .ValidateTitleFieldIsDisplayed(true)
                .ValidateDateFieldIsDisplayed(true)
                .ValidateDate_TimeFieldIsDisplayed(true)
                .ValidateFileFieldIsDisplayed()
                .ValidateCareRecordLookupButtonIsDisplayed(true)
                .ValidateDocumentTypeLookupButtonIsDisplayed(true)
                .ValidateDocumentSubTypeLookupButtonIsDisplayed(true)
                .ValidateCaptionFieldIsDisplayed(true);

            #endregion

            #region Step 7

            keyworkerNotesAttachmentRecordPage
                .InsertTextOnTitle("Test " + currentTimeString)
                .InsertTextOnDate(dateText)
                .InsertTextOnDate_Time(timeText)
                .ClickDocumentTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Daily Care Support Information", _documentTypeId1);

            keyworkerNotesAttachmentRecordPage
                .WaitForPageToLoad()
                .ClickDocumentSubTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Daily Care Support Documentation", _documentSubTypeId1);

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

            #endregion

            #region Step 8

            var _keyworkerNoteId1 = dbHelper.cpPersonKeyworkerNote.GetByPersonId(_personID)[0];

            var _keyworkerNotesAttachmentId1 = dbHelper.cpKeyworkerNotesAttachment.GetByPersonIDAndSortedByDate(_personID)[0];

            var Date2 = todayDate.AddDays(-3).ToUniversalTime().AddHours(1);
            var _keyworkerNotesAttachmentId2 = dbHelper.cpKeyworkerNotesAttachment.CreateCPKeyworkerNotesAttachment(_teamId, _personID, "Record 2", Date2, _documentTypeId2, _documentSubTypeId2, _keyworkerNoteId1, TestContext.DeploymentDirectory + "\\video.mp4", "KW Attachment 2");

            var Date3 = todayDate.ToUniversalTime().AddHours(-2);
            var _keyworkerNotesAttachmentId3 = dbHelper.cpKeyworkerNotesAttachment.CreateCPKeyworkerNotesAttachment(_teamId, _personID, "Record 3", Date3, _documentTypeId1, _documentSubTypeId1, _keyworkerNoteId1, TestContext.DeploymentDirectory + "\\ImageFile.jpeg", "KW Attachment 3");

            keyworkerNotesAttachmentRecordPage
                .ClickBackButton();

            personKeyworkerNoteRecordPage
                .WaitForPageToLoad()
                .WaitForResidentVoiceSectionToLoad()
                .ValidateColumnIsSortedByDescendingOrder(5)
                .ValidateColumnsSortOrderDescending(5, "Date")
                .ValidateRecordIsPresent(1, _keyworkerNotesAttachmentId1)
                .ValidateRecordIsPresent(2, _keyworkerNotesAttachmentId3)
                .ValidateRecordIsPresent(3, _keyworkerNotesAttachmentId2);

            #endregion

            #region Step 9

            personKeyworkerNoteRecordPage
                .WaitForPageToLoad()
                .NavigateToAttachmentsPage();

            keyworkerNotesAttachmentsPage
                .WaitForPageToLoad()
                .ValidateRecordPosition(1, _keyworkerNotesAttachmentId1.ToString())
                .ValidateRecordPosition(2, _keyworkerNotesAttachmentId3.ToString())
                .ValidateRecordPosition(3, _keyworkerNotesAttachmentId2.ToString());

            keyworkerNotesAttachmentsPage
                .SearchRecord("Policy")
                .WaitForPageToLoad()
                .ValidateRecordPresent(_keyworkerNotesAttachmentId2, true)
                .ValidateRecordPresent(_keyworkerNotesAttachmentId1, false)
                .ValidateRecordPresent(_keyworkerNotesAttachmentId3, false);

            keyworkerNotesAttachmentsPage
                .SearchRecord("Daily Care Support Documentation")
                .WaitForPageToLoad()
                .ValidateRecordPresent(_keyworkerNotesAttachmentId1, true)
                .ValidateRecordPresent(_keyworkerNotesAttachmentId3, true)
                .ValidateRecordPresent(_keyworkerNotesAttachmentId2, false);

            keyworkerNotesAttachmentsPage
                .SearchRecord("Accident/Incident")
                .WaitForPageToLoad()
                .ValidateRecordPresent(_keyworkerNotesAttachmentId2, true)
                .ValidateRecordPresent(_keyworkerNotesAttachmentId1, false)
                .ValidateRecordPresent(_keyworkerNotesAttachmentId3, false);

            keyworkerNotesAttachmentsPage
                .SearchRecord("Daily Care Support Information")
                .WaitForPageToLoad()
                .ValidateRecordPresent(_keyworkerNotesAttachmentId1, true)
                .ValidateRecordPresent(_keyworkerNotesAttachmentId3, true)
                .ValidateRecordPresent(_keyworkerNotesAttachmentId2, false);

            #endregion

            #region Step 10

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Attachments (For Keyworker Notes)")
                .SelectFilter("1", "Attachment (For Keyworker Notes)")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Test " + currentTimeString, _keyworkerNotesAttachmentId1);

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_keyworkerNotesAttachmentId1.ToString())
                .ValidateSearchResultRecordNotPresent(_keyworkerNotesAttachmentId2.ToString())
                .ValidateSearchResultRecordNotPresent(_keyworkerNotesAttachmentId3.ToString());

            advanceSearchPage
                .ClickBackButton_ResultsPage();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Attachments (For Keyworker Notes)")
                .ClickDeleteButton()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(7)
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(7)
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_keyworkerNotesAttachmentId1.ToString())
                .ValidateSearchResultRecordPresent(_keyworkerNotesAttachmentId2.ToString())
                .ValidateSearchResultRecordPresent(_keyworkerNotesAttachmentId3.ToString());

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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Phoenix.UITests.Cases
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    public class CaseRecord_UITestCases : FunctionalTest
    {

        private Guid _languageId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private Guid _contactReasonId;
        private Guid _contactSourceId;
        private Guid _maritalStatusId;
        private Guid _systemUserId;
        private string _systemUserName;
        private Guid _personID;
        private Guid _carePlanType;
        private Guid _caseId;
        private Guid _caseStatusId;
        private Guid _dataFormId;
        private string _caseNumber;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");

        [TestInitialize()]
        public void Person_CarePlan_SetupTest()
        {
            try
            {
                #region System Settings

                var systemSettingExists = dbHelper.systemSetting.GetSystemSettingIdByName("AllowMultipleActiveSocialCareCase").Any();
                if (!systemSettingExists)
                {
                    dbHelper.systemSetting.CreateSystemSetting("AllowMultipleActiveSocialCareCase", "true", "When set to true the organization will be able to decide if they want to allow multiple active social care referrals", false, null);
                }
                else
                {
                    var systemSettingID = dbHelper.systemSetting.GetSystemSettingIdByName("AllowMultipleActiveSocialCareCase").First();
                    dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID, "true");
                }

                #endregion

                #region Business Unit

                _careDirectorQA_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

                #endregion

                #region Providers

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Teams

                _careDirectorQA_TeamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion

                #region Marital Status

                _maritalStatusId = commonMethodsDB.CreateMaritalStatus("Civil Partner", new DateTime(2000, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_careDirectorQA_TeamId, "PersonCarePlan_Ethnicity", new DateTime(2020, 1, 1));

                #endregion

                #region Contact Reason

                _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("PersonCareTest_ContactReason", _careDirectorQA_TeamId);

                #endregion

                #region Contact Source

                _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("PersonCareTest_ContactSource", _careDirectorQA_TeamId);

                #endregion

                #region Create System User Record

                _systemUserName = "Case_Record_User_1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "CaseRecord", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion

                #region Person

                _personID = commonMethodsDB.CreatePersonRecord("Person_Care", "Person" + _currentDateSuffix, _ethnicityId, _careDirectorQA_TeamId);

                #endregion

                #region Care Plan Type

                _carePlanType = commonMethodsDB.CreateCarePlanTypeId("Activities of Daily Living", DateTime.Now, _careDirectorQA_TeamId);


                #endregion

                #region Case 

                _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
                _dataFormId = dbHelper.dataForm.GetByName("Community Health Case").FirstOrDefault();

                _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 20);
                _caseNumber = (string)dbHelper.Case.GetCaseById(_caseId, "casenumber")["casenumber"];

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                this.ShutDownAllProcesses();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-8562


        [TestProperty("JiraIssueID", "CDV6-10257")]
        [Description("Open a case record in Edit mode - Tap on the Copy Record Link button - Validate that the Copy To Clipboard Dynamic Dialog Popup is displayed")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_CopyPageHyperlink_UITestMethod01()
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
                .ClickCopyRecordLinkButton();

            copyToClipboardDynamicDialogPopup
                .WaitForCopyToClipboardDynamicDialogPopupToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-10258")]
        [Description("Open a case record in Edit mode - Tap on the Copy Record Link button - Wait for the Copy To Clipboard Dynamic Dialog Popup to be displayed - " +
            "Validate that the page link is generated and displayed in the text area of the dialog")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_CopyPageHyperlink_UITestMethod02()
        {
            string businessObjectName = "case";
            string expectedHyperlink = string.Format("{0}/pages/Default.aspx?ReturnUrl=%2Fpages%2Feditpage.aspx%3Fid%3D{1}%26type%3D{2}", appURL, _caseId.ToString(), businessObjectName);

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
                .ClickCopyRecordLinkButton();

            copyToClipboardDynamicDialogPopup
                .WaitForCopyToClipboardDynamicDialogPopupToLoad()
                .ValidateTextAreaLink(expectedHyperlink);
        }

        [TestProperty("JiraIssueID", "CDV6-10259")]
        [Description("Open a case record in Edit mode - Tap on the Copy Record Link button - Wait for the Copy To Clipboard Dynamic Dialog Popup to be displayed - " +
            "Click on the Copy button - Validate that the Hyperlink is copied to the clipboard")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_CopyPageHyperlink_UITestMethod03()
        {
            string businessObjectName = "case";
            string expectedHyperlink = string.Format("{0}/pages/Default.aspx?ReturnUrl=%2Fpages%2Feditpage.aspx%3Fid%3D{1}%26type%3D{2}", appURL, _caseId.ToString(), businessObjectName);

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
                .ClickCopyRecordLinkButton();

            copyToClipboardDynamicDialogPopup
                .WaitForCopyToClipboardDynamicDialogPopupToLoad()
                .TapCopyButton();

            string clipboardText = System.Windows.Forms.Clipboard.GetText();
            Assert.AreEqual(expectedHyperlink, clipboardText);
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-7377

        [TestProperty("JiraIssueID", "CDV6-10260")]
        [Description("Login on Caredirector - Enter a Case record Hyperlink in the browser URL - Validate that the user is redirected to the Case record page")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_OpenPageHyperlink_UITestMethod01()
        {
            string businessObjectName = "case";
            string expectedHyperlink = string.Format("{0}/pages/Default.aspx?ReturnUrl=..%2Fpages%2Feditpage.aspx%3Fid%3D{1}%26type%3D{2}", appURL, _caseId.ToString(), businessObjectName);
            var _caseNumberTitle = (string)dbHelper.Case.GetCaseById(_caseId, "title")["title"];

            //login in the app
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad();

            //navigate to the hyperlink url and validate that the case record page loads
            caseRecordPage
                .OpenCaseRecordHyperlink(expectedHyperlink)
                .WaitForCaseRecordPageToLoadFromHyperlink(_caseNumberTitle)
                .ValidateCaseNoFieldValue(_caseNumber);

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-14012

        [TestProperty("JiraIssueID", "CDV6-14748")]
        [Description("Login CD Web - > Work Place  -> My Work -> Cases -> Check the options in the View-> Should have a new view My Pinned Cases along with the existing views" +
            "Select My Pinned Cases-> Should display only the Case records pinned to the logged in user")]
        [TestMethod, TestCategory("UITest")]
        public void CaseRecord_UITestMethod01()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .ValidateMyPinnedcasesOption("My Pinned Cases")
                .SearchByCaseNumber(_caseNumber.ToString(), _caseId.ToString())
                .SelectPersonCaseRecord(_caseId.ToString())
                .ClickAdditionalItemsMenuButton()
                .ClickPinToMe()
                .SelectSearchResultsDropDown("My Pinned Cases")
                .ValidateCaesRecord(_caseId.ToString(), _caseId.ToString());
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

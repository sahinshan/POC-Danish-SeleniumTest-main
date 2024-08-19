using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Cases
{
    [TestClass]
    public class CaseInvolvements_UITestCases : FunctionalTest
    {

        #region Properties

        private string _tenantName;
        private Guid _languageId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private string _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _defaultSystemUserId;
        private string _systemUserName;
        private Guid _systemUserId;

        #endregion

        [TestInitialize()]
        public void SetupTestMethod()
        {
            try
            {
                #region Tenant

                _tenantName = ConfigurationManager.AppSettings["TenantName"];
                dbHelper = new DBHelper.DatabaseHelper(_tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper, fileIOHelper, TestContext);

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                _defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];
                var _localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(_defaultSystemUserId, _localZone.StandardName);

                #endregion

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("Case Involvements BU1");

                #endregion

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("Case Involvements T1", null, _businessUnitId, "907678", "CaseInvolvementsT1@careworkstempmail.com", "CaseInvolvements T1", "020 123456");

                #endregion                

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

                #endregion

                #region System User

                _systemUserName = "CaseInvolvementUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "CaseInvolvement", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }

        #region https://advancedcsg.atlassian.net/browse/ACC-2341

        [TestProperty("JiraIssueID", "CDV6-25757")]
        [Description("Step(s) 5 to 8 from the original test CDV6-23357")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseInvolvement_UITestMethod01()
        {
            #region System User

            var newSystemUserName = "CIU_" + _currentDateString;
            var newSystemUserId = commonMethodsDB.CreateSystemUserRecord(newSystemUserName, "CIU", _currentDateString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Person

            var personFirstName = "Daelen";
            var personLastName = _currentDateString;
            var _personFullName = "Daelen " + personLastName;
            var _personID = commonMethodsDB.CreatePersonRecord(personFirstName, personLastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));

            #endregion

            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default Contact Reason", _teamId);

            #endregion

            #region Case

            Guid _caseID = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, _personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 1, 1, 8, 0, 0), new DateTime(2020, 1, 1, 9, 0, 0), 20);
            var caseFields = dbHelper.Case.GetCaseByID(_caseID, "title", "casenumber");
            var caseTitle = (string)(caseFields["title"]);
            var caseNumber = (string)(caseFields["casenumber"]);

            #endregion

            #region LAC Episode

            var lacEpisodeID = dbHelper.LACEpisode.CreateLACEpisode(_teamId, _caseID, caseTitle, _personID, new DateTime(2020, 1, 1));

            #endregion

            #region Social Worker Change Reasons

            var socialWorkerChangeReasonId = commonMethodsDB.CreateSocialWorkerChangeReason("Default Social Worker Change Reasons", new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Step 5

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, _caseID.ToString())
                .OpenCaseRecord(_caseID.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToCaseInvolvement();

            caseInvolvementsPage
                .WaitForCaseInvolvementsPageToLoad();

            var responsibleUserInvolvementId = dbHelper.CaseInvolvement.GetByCaseID(_caseID, _systemUserId)[0];

            caseInvolvementsPage
                .OpenRecord(responsibleUserInvolvementId);

            caseInvolvementRecordPage
                .WaitForCaseInvolvementRecordPageToLoad()
                .ValidateSocialWorkerChangeReasonLookupButtonDisabled(true);

            #endregion

            #region Step 6

            caseInvolvementRecordPage
                .ClickBackButton();

            caseRecordPage
                .WaitForCaseRecordPageToFullyLoadIframes()
                .ClickDetailsLink()
                .ClickResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").SearchAndSelectRecord(newSystemUserName, newSystemUserId);

            caseRecordPage
                .WaitForCaseRecordPageToFullyLoadIframes()
                .ClickSaveButton();

            reasonForChangeOfSocialWorkerDialogPopup
                .WaitForReasonForChangeOfSocialWorkerDialogPopupToLoad();

            #endregion

            #region Step 7

            reasonForChangeOfSocialWorkerDialogPopup
                .TapSocialWorkerChangeReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default Social Worker Change Reasons", socialWorkerChangeReasonId);

            caseRecordPage
                .WaitForCaseRecordPageToFullyLoadIframes();

            reasonForChangeOfSocialWorkerDialogPopup
                .WaitForReasonForChangeOfSocialWorkerDialogPopupToLoad()
                .TapOkButton();

            caseRecordPage
                .WaitForCaseRecordPageToFullyLoadIframes()
                .ClickBackButton();

            casesPage
                .WaitForCasesPageToLoad()
                .OpenCaseRecord(_caseID.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToFullyLoadIframes()
                .ClickDetailsLink()
                .ValidateResponsibleUserLinkFieldText("CIU " + _currentDateString);

            #endregion

            #region Step 8

            caseRecordPage
                .NavigateToCaseInvolvement();

            caseInvolvementsPage
                .WaitForCaseInvolvementsPageToLoad();

            var newResponsibleUserInvolvementId = dbHelper.CaseInvolvement.GetByCaseID(_caseID, newSystemUserId)[0];

            caseInvolvementsPage
                .ValidateRecordVisible(newResponsibleUserInvolvementId.ToString(), true);

            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-25758")]
        [Description("Step(s) 9 to 10 from the original test CDV6-23357")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseInvolvement_UITestMethod02()
        {
            #region Team

            var newTeamId = commonMethodsDB.CreateTeam("Case Involvements T2", null, _businessUnitId, "907678", "CaseInvolvementsT2@careworkstempmail.com", "CaseInvolvements T2", "020 123456");

            #endregion

            #region System User

            var newSystemUserName = "CIU_" + _currentDateString;
            var newSystemUserId = commonMethodsDB.CreateSystemUserRecord(newSystemUserName, "CIU", _currentDateString, "Passw0rd_!", _businessUnitId, newTeamId, _languageId, _authenticationproviderid);

            #endregion



            #region Person

            var personFirstName = "Daelen";
            var personLastName = _currentDateString;
            var _personFullName = "Daelen " + personLastName;
            var _personID = commonMethodsDB.CreatePersonRecord(personFirstName, personLastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));

            #endregion

            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default Contact Reason", _teamId);

            #endregion

            #region Case

            Guid _caseID = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, _personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 1, 1, 8, 0, 0), new DateTime(2020, 1, 1, 9, 0, 0), 20);
            var caseFields = dbHelper.Case.GetCaseByID(_caseID, "title", "casenumber");
            var caseTitle = (string)(caseFields["title"]);
            var caseNumber = (string)(caseFields["casenumber"]);

            #endregion

            #region LAC Episode

            var lacEpisodeID = dbHelper.LACEpisode.CreateLACEpisode(_teamId, _caseID, caseTitle, _personID, new DateTime(2020, 1, 1));

            #endregion

            #region Social Worker Change Reasons

            var socialWorkerChangeReasonId = commonMethodsDB.CreateSocialWorkerChangeReason("Default Social Worker Change Reasons", new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Step 9

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, _caseID.ToString())
                .SelectCaseRecord(_caseID.ToString())
                .ClickAssignRecordButton();

            assignRecordPopup
                .WaitForAssignRecordPopupToLoad()
                .ClickResponsibleTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").SearchAndSelectRecord("Case Involvements T2", newTeamId);

            casesPage
                .WaitForCasesPageToLoad();

            assignRecordPopup
                .WaitForAssignRecordPopupToLoad()
                .SelectResponsibleUserDecision("Change on current and child records")
                .ClickResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("CIU " + _currentDateString, newSystemUserId);

            casesPage.
                WaitForCasesPageToLoad();

            assignRecordPopup
                .WaitForAssignRecordPopupToLoad()
                .ClickSocialWorkerChangeReasonLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default Social Worker Change Reasons", socialWorkerChangeReasonId);

            casesPage
                .WaitForCasesPageToLoad();

            assignRecordPopup
                .WaitForAssignRecordPopupToLoad()
                .TapOkButton();

            casesPage
                .WaitForCasesPageToLoad();

            #endregion

            #region Step 10

            casesPage
                .OpenCaseRecord(_caseID.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToCaseInvolvement();

            caseInvolvementsPage
                .WaitForCaseInvolvementsPageToLoad();

            var responsibleUserInvolvementId = dbHelper.CaseInvolvement.GetByCaseID(_caseID, newSystemUserId)[0];

            caseInvolvementsPage
                .ValidateRecordVisible(responsibleUserInvolvementId.ToString(), true);

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

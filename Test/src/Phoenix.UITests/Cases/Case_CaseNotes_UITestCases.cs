using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Cases
{
    [TestClass]
    public class Case_CaseNotes_UITestCases : FunctionalTest
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
        private Guid _otherPresentingPriority;
        private Guid _emergencyPresentingPriority;
        private Guid _systemUserId;
        private Guid _personID;
        private Guid caseid;
        private Guid caseid2;
        private string caseNumber;
        private string caseNumber2;
        private Guid _caseStatusId;
        private Guid _caseStatusId2;
        private Guid _dataFormId;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _activityCategoryId;
        private Guid _activitySubCategoryId;
        private Guid _activityReasonId;
        private Guid _activityPriorityId;
        private Guid _activityOutcomeId;
        private Guid _systemSettingId;
        private Guid _significantEventCategoryId;
        private Guid _significantEventSubCategoryId;

        #endregion

        [TestInitialize()]
        public void Case_CaseNotes_SetupTest()
        {
            try
            {
                #region Tenant

                _tenantName = ConfigurationManager.AppSettings["TenantName"];
                dbHelper = new DBHelper.DatabaseHelper(_tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper, fileIOHelper, TestContext);

                #endregion

                #region Business Unit

                _careDirectorQA_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

                #endregion

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Team

                _careDirectorQA_TeamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion                

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_careDirectorQA_TeamId, "English", new DateTime(2020, 1, 1));

                #endregion

                #region Data Form

                _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

                #endregion

                #region System User "CaseFormCaseNoteUser1"

                _systemUserName = "CaseCaseNoteUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "CaseCaseNote", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion

                #region Case Status

                _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
                _caseStatusId2 = dbHelper.caseStatus.GetByName("Awaiting Further Information").FirstOrDefault();

                #endregion

                #region Contact Reason

                _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Test_Contact (Comm)", _careDirectorQA_TeamId);

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

                #region Presenting Priority
                if (!dbHelper.contactPresentingPriority.GetByName("Other").Any())
                {
                    dbHelper.contactPresentingPriority.CreateContactPresentingPriority(_careDirectorQA_TeamId, "Other", "3", "3", new DateTime(2019, 03, 27));
                }
                _otherPresentingPriority = dbHelper.contactPresentingPriority.GetByName("Other").First();

                if (!dbHelper.contactPresentingPriority.GetByName("Emergency").Any())
                {
                    dbHelper.contactPresentingPriority.CreateContactPresentingPriority(_careDirectorQA_TeamId, "Emergency", "1", "1", new DateTime(2019, 02, 27));
                }
                _emergencyPresentingPriority = dbHelper.contactPresentingPriority.GetByName("Emergency").First();

                #endregion

                #region Significant Event Category
                _significantEventCategoryId = commonMethodsDB.CreateSignificantEventCategory("Default11404", DateTime.Now.Date, _careDirectorQA_TeamId, null, null, null);

                #endregion

                #region Significant Event Sub Category
                if (!dbHelper.significantEventSubCategory.SignificantEventSubCategoryByName("SubCategory11404").Any())
                {
                    dbHelper.significantEventSubCategory.CreateSignificantEventSubCategory(_careDirectorQA_TeamId, "SubCategory11404", _significantEventCategoryId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, null);
                }
                _significantEventSubCategoryId = dbHelper.significantEventSubCategory.SignificantEventSubCategoryByName("SubCategory11404").FirstOrDefault();

                #endregion

                #region System Setting                
                if (!dbHelper.systemSetting.GetSystemSettingIdByName("AllowMultipleActiveSocialCareCase").Any())
                    _systemSettingId = dbHelper.systemSetting.CreateSystemSetting("AllowMultipleActiveSocialCareCase", "false", "When set to true the organisation will be able to decide if they want to allow multiple active social care referrals", false, "false");
                _systemSettingId = dbHelper.systemSetting.GetSystemSettingIdByName("AllowMultipleActiveSocialCareCase").FirstOrDefault();

                #endregion

                #region Person

                var firstName = "CaseNoteCloneAutomation";
                var lastName = _currentDateSuffix;
                _personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2003, 1, 2));

                #endregion

                #region Case

                var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                caseid = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2017, 03, 10), new DateTime(2017, 03, 10), 20, "Case Case Note Information");
                caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-8418

        [TestProperty("JiraIssueID", "CDV6-11403")]
        [Description("Open a Case Notes (For Case) record (with data in all fields) - Click on the clone button - Wait for the clone popup to be displayed - " +
            "Select a Case record as the destination record - Confirm the clone operation - Validate that the case note record is properly cloned")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseCaseNotes_Cloning_UITestMethod01()
        {
            var CaseNoteID = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case Note (For Case) 001", "Case Note (For Case) Description",
                caseid, _personID, _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, false, null, null, null, new DateTime(2019, 04, 02, 0, 0, 0));

            dbHelper.systemSetting.UpdateSystemSettingValue(_systemSettingId, "true");

            var _careDirectorQA_TeamId2 = commonMethodsDB.CreateTeam("CareDirector QA2", null, _careDirectorQA_BusinessUnitId, "907679", "CareDirectorQA2@careworkstempmail.com", "CareDirector QA2", "020 123554");
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord("TestUser_11403", "Test", "User_114031", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId2, _languageId, _authenticationproviderid);
            var targetCaseID = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId2, _personID, _systemUserId2, _systemUserId2, _caseStatusId2, _contactReasonId, _dataFormId, null, new DateTime(2019, 04, 02), new DateTime(2019, 04, 02), 20);
            caseNumber2 = (string)dbHelper.Case.GetCaseByID(targetCaseID, "casenumber")["casenumber"];


            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToCaseNotesPage();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad()
                .OpenCaseCaseNoteRecord(CaseNoteID.ToString());

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad()
                .ClickCloneButton();

            cloneActivityPopup
                .WaitForCloneActivityPopupToLoad()
                .SelectBusinessObjectTypeText("Case")
                .SelectRetainStatus("Yes")
                .SelectRecord(targetCaseID.ToString())
                .ClickCloneButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("1 of 1 Activities Cloned Successfully")
                .ClickCloseButton();

            var records = dbHelper.caseCaseNote.GetByCaseID(targetCaseID);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.caseCaseNote.GetByID(records[0],
                "subject", "notes", "personid", "ownerid", "activityreasonid", "responsibleuserid",
                "activitypriorityid", "activitycategoryid", "casenotedate", "activitysubcategoryid", "statusid", "activityoutcomeid",
                "informationbythirdparty", "issignificantevent", "significanteventcategoryid", "significanteventdate", "significanteventsubcategoryid",
                "iscloned", "clonedfromid");

            var userLocalTime = (DateTime)fields["casenotedate"];

            var statusid = 1; //Open

            Assert.AreEqual("Case Note (For Case) 001", fields["subject"]);
            Assert.AreEqual("Case Note (For Case) Description", fields["notes"]);
            Assert.AreEqual(_personID.ToString(), fields["personid"]);
            Assert.AreEqual(_careDirectorQA_TeamId.ToString(), fields["ownerid"]);
            Assert.AreEqual(_activityReasonId.ToString(), fields["activityreasonid"]);
            Assert.AreEqual(_systemUserId.ToString(), fields["responsibleuserid"]);
            Assert.AreEqual(_activityPriorityId.ToString(), fields["activitypriorityid"]);
            Assert.AreEqual(_activityCategoryId.ToString(), fields["activitycategoryid"]);
            Assert.AreEqual(new DateTime(2019, 04, 02, 0, 0, 0), userLocalTime);
            Assert.AreEqual(_activitySubCategoryId.ToString(), fields["activitysubcategoryid"]);
            Assert.AreEqual(statusid, fields["statusid"]);
            Assert.AreEqual(_activityOutcomeId.ToString(), fields["activityoutcomeid"]);
            Assert.AreEqual(false, fields["informationbythirdparty"]);
            Assert.AreEqual(false, fields["issignificantevent"]);
            Assert.IsTrue(!fields.ContainsKey("significanteventcategoryid"));
            Assert.IsTrue(!fields.ContainsKey("significanteventdate"));
            Assert.IsTrue(!fields.ContainsKey("significanteventsubcategoryid"));
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(CaseNoteID.ToString(), fields["clonedfromid"]);

        }

        [TestProperty("JiraIssueID", "CDV6-11404")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-2446 - " +
            "Open a Case Notes (For Case) record (with data in all fields) - Click on the clone button - Wait for the clone popup to be displayed - " +
            "Select a person record as the destination record - Confirm the clone operation - Validate that the case note record is properly cloned")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseCaseNotes_Cloning_UITestMethod02()
        {

            var CaseNoteID = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case Note (For Case) 001", "Case Note (For Case) Description",
                caseid, _personID, _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, true, _significantEventCategoryId, _significantEventSubCategoryId, new DateTime(2019, 04, 02), new DateTime(2019, 04, 02));

            dbHelper.systemSetting.UpdateSystemSettingValue(_systemSettingId, "true");

            var _careDirectorQA_TeamId2 = commonMethodsDB.CreateTeam("CareDirector QA2", null, _careDirectorQA_BusinessUnitId, "907679", "CareDirectorQA2@careworkstempmail.com", "CareDirector QA2", "020 123554");
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord("TestUser_11404", "Test", "User_114041", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId2, _languageId, _authenticationproviderid);
            var targetCaseID = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId2, _personID, _systemUserId2, _systemUserId2, _caseStatusId2, _contactReasonId, _dataFormId, null, new DateTime(2019, 04, 02), new DateTime(2019, 04, 02), 20);
            caseNumber2 = (string)dbHelper.Case.GetCaseByID(targetCaseID, "casenumber")["casenumber"];

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToCaseNotesPage();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad()
                .OpenCaseCaseNoteRecord(CaseNoteID.ToString());

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad()
                .ClickCloneButton();

            cloneActivityPopup
                .WaitForCloneActivityPopupToLoad()
                .SelectBusinessObjectTypeText("Person")
                .SelectRetainStatus("Yes")
                .SelectRecord(_personID.ToString())
                .ClickCloneButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("1 of 1 Activities Cloned Successfully")
                .ClickCloseButton();

            var records = dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(_personID);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.personCaseNote.GetPersonCaseNoteByID(records[0],
                "subject", "notes", "personid", "ownerid", "activityreasonid", "responsibleuserid",
                "activitypriorityid", "activitycategoryid", "casenotedate", "activitysubcategoryid", "statusid", "activityoutcomeid",
                "informationbythirdparty", "issignificantevent", "significanteventcategoryid", "significanteventdate", "significanteventsubcategoryid",
                "iscloned", "clonedfromid");

            var userLocalTime = (DateTime)fields["casenotedate"];

            var statusid = 1; //Open

            Assert.AreEqual("Case Note (For Case) 001", fields["subject"]);
            Assert.AreEqual("Case Note (For Case) Description", fields["notes"]);
            Assert.AreEqual(_personID.ToString(), fields["personid"]);
            Assert.AreEqual(_careDirectorQA_TeamId.ToString(), fields["ownerid"]);
            Assert.AreEqual(_activityReasonId.ToString(), fields["activityreasonid"]);
            Assert.AreEqual(_systemUserId.ToString(), fields["responsibleuserid"]);
            Assert.AreEqual(_activityPriorityId.ToString(), fields["activitypriorityid"]);
            Assert.AreEqual(_activityCategoryId.ToString(), fields["activitycategoryid"]);
            Assert.AreEqual(new DateTime(2019, 04, 02), userLocalTime);
            Assert.AreEqual(_activitySubCategoryId.ToString(), fields["activitysubcategoryid"]);
            Assert.AreEqual(statusid, fields["statusid"]);
            Assert.AreEqual(_activityOutcomeId.ToString(), fields["activityoutcomeid"]);
            Assert.AreEqual(false, fields["informationbythirdparty"]);
            Assert.AreEqual(true, fields["issignificantevent"]);
            Assert.AreEqual(_significantEventCategoryId.ToString(), fields["significanteventcategoryid"]);
            Assert.AreEqual(new DateTime(2019, 04, 02), userLocalTime);
            Assert.AreEqual(_significantEventSubCategoryId.ToString(), fields["significanteventsubcategoryid"]);
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(CaseNoteID.ToString(), fields["clonedfromid"]);

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

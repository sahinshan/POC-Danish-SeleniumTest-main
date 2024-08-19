using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Cases
{
    [TestClass]
    public class Case_LeaveAWOL_UITestCases : FunctionalTest
    {
        #region Properties
        private string _tenantName;
        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _ethnicityId;
        private Guid _contactReasonId;
        private Guid _contactSourceId;
        private Guid _personID;
        private string _personFullName;
        private Guid _caseId;
        private string _caseNumber;
        private Guid _dataFormId;//Inpatient Case
        private Guid _admission_CaseStatusId;
        private Guid _awaitingAdmission_caseStatusId;
        private Guid _inpatientAdmissionSourceId;
        private string _provider_Name;
        private Guid _provider_HospitalId;
        private Guid _inpationAdmissionMethodId;
        private Guid _wardSpecialityId;//Adult Acute
        private string _inpatientWardName;
        private Guid _inpatientWardId;
        private string _inpatientBayName;
        private Guid _inpatientBayId;
        private Guid _inpatientBedId;
        private Guid _inpatientBedTypeId;
        private Guid _inpatientLeaveTypeId;
        private string _systemUsername;
        private Guid _systemUserId;
        private Guid _activityCategoryId;
        private Guid _activitySubCategoryId;
        private Guid _activityReasonId;
        private Guid _activityPriorityId;
        private Guid _activityOutcomeId;
        private Guid _significantEventCategoryId;
        private Guid _significantEventSubCategoryId;
        private string _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");
        #endregion

        [TestInitialize()]
        public void TestInitializationMethod()
        {
            try
            {
                #region Tenant

                _tenantName = ConfigurationManager.AppSettings["TenantName"];
                dbHelper = new DBHelper.DatabaseHelper(_tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper, fileIOHelper, TestContext);

                #endregion

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion 

                #region Business Unit

                _careDirectorQA_BusinessUnitId = commonMethodsDB.CreateBusinessUnit(("CareDirector QA"));

                #endregion  

                #region Team
                _careDirectorQA_TeamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion 

                #region System User

                _systemUsername = "CloneAWOLUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "CloneAWOL", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion 

                #region Data Form

                _dataFormId = dbHelper.dataForm.GetByName("InpatientCase")[0];

                #endregion

                #region Ethnicity
                _ethnicityId = commonMethodsDB.CreateEthnicity(_careDirectorQA_TeamId, "LeaveAWOL_Ethnicity", new DateTime(2020, 1, 1));

                #endregion 

                #region Case Status
                _admission_CaseStatusId = dbHelper.caseStatus.GetByName("Admission").FirstOrDefault();
                _awaitingAdmission_caseStatusId = dbHelper.caseStatus.GetByName("Awaiting Admission").FirstOrDefault();

                #endregion

                #region Contact Reason

                _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("LeaveAWOL_ContactReason", _careDirectorQA_TeamId);

                #endregion 

                #region Contact Source

                _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("LeaveAWOL_ContactSource", _careDirectorQA_TeamId);

                #endregion

                #region Inpatient Bed Type
                _inpatientBedTypeId = dbHelper.inpatientBedType.GetByName("Clinitron")[0];

                #endregion

                #region Hospital Ward Specialty
                _wardSpecialityId = dbHelper.inpatientWardSpecialty.GetByName("Adult Acute")[0];

                #endregion

                #region Contact Inpatient Admission Source

                var inpatientAdmissionSourceExists = dbHelper.inpatientAdmissionSource.GetByName("LeaveAWOL_InpatientAdmissionSource").Any();
                if (!inpatientAdmissionSourceExists)
                    dbHelper.inpatientAdmissionSource.CreateInpatientAdmissionSource(_careDirectorQA_TeamId, "LeaveAWOL_InpatientAdmissionSource", new DateTime(2020, 1, 1));
                _inpatientAdmissionSourceId = dbHelper.inpatientAdmissionSource.GetByName("LeaveAWOL_InpatientAdmissionSource")[0];

                #endregion

                #region Provider_Hospital

                _provider_Name = "Aut_Clone_LeaveAWOL_" + _currentDateString;
                _provider_HospitalId = commonMethodsDB.CreateProvider(_provider_Name, _careDirectorQA_TeamId);

                #endregion 

                #region Ward

                _inpatientWardName = "Ward_" + _currentDateString;
                _inpatientWardId = dbHelper.inpatientWard.CreateInpatientWard(_careDirectorQA_TeamId, _provider_HospitalId, _systemUserId, _wardSpecialityId, _inpatientWardName, new DateTime(2020, 1, 1));

                #endregion 

                #region Bay/Room

                _inpatientBayName = "Bay_" + _currentDateString;
                _inpatientBayId = dbHelper.inpatientBay.CreateInpatientCaseBay(_careDirectorQA_TeamId, _inpatientWardId, _inpatientBayName, 1, "4", "4", "4", 2);

                #endregion

                #region Bed

                var inpatientBedExists = dbHelper.inpatientBed.GetInpatientBedByInpatientBayId(_inpatientBayId).Any();
                if (!inpatientBedExists)
                    dbHelper.inpatientBed.CreateInpatientBed(_careDirectorQA_TeamId, "12665", "4", "4", _inpatientBayId, 1, _inpatientBedTypeId, "4");
                _inpatientBedId = dbHelper.inpatientBed.GetInpatientBedByInpatientBayId(_inpatientBayId)[0];

                #endregion

                #region InpatientAdmissionMethod

                var inpatientAdmissionMethodExists = dbHelper.inpatientAdmissionMethod.GetAdmissionMethodByName("Automation_AdmissionLeaveAWOL").Any();
                if (!inpatientAdmissionMethodExists)
                    dbHelper.inpatientAdmissionMethod.CreateAdmissionMethod("Automation_AdmissionLeaveAWOL", _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, new DateTime(2020, 1, 1));
                _inpationAdmissionMethodId = dbHelper.inpatientAdmissionMethod.GetAdmissionMethodByName("Automation_AdmissionLeaveAWOL")[0];

                #endregion

                #region Person 1

                var personFirstName = DateTime.Now.ToString("yyyyMMddHHmmss");
                _personFullName = personFirstName + " AutomationLeaveAWOLLastName";
                _personID = commonMethodsDB.CreatePersonRecord(personFirstName, "AutomationLeaveAWOLLastName", _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));

                #endregion

                #region To Create Inpatient Case record 1

                _caseId = dbHelper.Case.CreateInpatientCaseRecordWithStatusAsAdmission(_careDirectorQA_TeamId, _personID, DateTime.Now.Date, _systemUserId, "hdsa", _systemUserId, _admission_CaseStatusId, _contactReasonId, DateTime.Now.Date, _dataFormId, _contactSourceId, _inpatientWardId, _inpatientBayId, _inpatientBedId, _inpatientAdmissionSourceId, _inpationAdmissionMethodId, _systemUserId, DateTime.Now.Date, _provider_HospitalId, _inpatientWardId, 1, DateTime.Now.Date, false, false, false, false, false, false, false, false, false, false);
                _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];

                #endregion 

                #region Inpatient Leave Type

                var inpatientLeaveTypeExists = dbHelper.inpatientLeaveType.GetInpatientLeaveTypeByName("Automation_LeaveAWOLType").Any();
                if (!inpatientLeaveTypeExists)
                    dbHelper.inpatientLeaveType.CreateInpatientLeaveType("Automation_LeaveAWOLType", _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, new DateTime(2020, 1, 1));
                _inpatientLeaveTypeId = dbHelper.inpatientLeaveType.GetInpatientLeaveTypeByName("Automation_LeaveAWOLType")[0];

                #endregion 

                #region Activity Categories                

                _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("1d2b78b8-9d45-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Activity Sub Categories

                _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("eec317f4-9d45-e911-a2c5-005056926fe4"), "Health Assessment", new DateTime(2020, 1, 1), _activityCategoryId, _careDirectorQA_TeamId);

                #endregion

                #region Activity Reason

                _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Activity Priority

                _activityPriorityId = commonMethodsDB.CreateActivityPriority(new Guid("5246a13f-9d45-e911-a2c5-005056926fe4"), "Normal", new DateTime(2022, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Activity Outcome

                _activityOutcomeId = commonMethodsDB.CreateActivityOutcome(new Guid("4c2bec1c-9e45-e911-a2c5-005056926fe4"), "Completed", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Significant Event Category
                _significantEventCategoryId = commonMethodsDB.CreateSignificantEventCategory("DefaultAwol", DateTime.Now.Date, _careDirectorQA_TeamId, null, null, null);

                #endregion

                #region Significant Event Sub Category
                if (!dbHelper.significantEventSubCategory.SignificantEventSubCategoryByName("SubCategoryAwol").Any())
                {
                    dbHelper.significantEventSubCategory.CreateSignificantEventSubCategory(_careDirectorQA_TeamId, "SubCategoryAwol", _significantEventCategoryId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, null);
                }
                _significantEventSubCategoryId = dbHelper.significantEventSubCategory.SignificantEventSubCategoryByName("SubCategoryAwol").FirstOrDefault();

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



        #region https://advancedcsg.atlassian.net/browse/CDV6-8418

        [TestProperty("JiraIssueID", "CDV6-11209")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-2446 - " +
            "Open a Case Note (For Inpatient Leave Awol) record (with data in all fields) - Click on the clone button - Wait for the clone popup to be displayed - " +
            "Confirm the clone operation - Validate that the case note record is properly cloned")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void LeaveAWOLCaseNotes_Cloning_UITestMethod01()
        {
            var currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var LeaveAWOLID = dbHelper.inpatientLeaveAwol.CreateLeaveAWOLRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _personID, _caseId,
                DateTime.Now.Date, _inpatientLeaveTypeId, DateTime.Now.Date, DateTime.Now.AddDays(1).Date, "systemuser", _systemUserId, _systemUsername);

            var LeaveAWOLCaseNoteID = dbHelper.inpatientLeaveAwolCaseNote.CreateInpatientLeaveAwolCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case Note (For Inpatient Leave Awol) 001",
                "Case Note (For Inpatient Leave Awol) Description", LeaveAWOLID, _caseId, _personID, _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId,
                true, _significantEventCategoryId, _significantEventSubCategoryId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), true, 1);

            var _careDirectorQA_TeamId2 = commonMethodsDB.CreateTeam("CareDirector QA2", null, _careDirectorQA_BusinessUnitId, "907679", "CareDirectorQA2@careworkstempmail.com", "CareDirector QA2", "020 123554");
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord("TestUser_11209", "Test", "User_11209", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId2, _languageId, _authenticationproviderid);

            var targetCaseID = dbHelper.Case.CreateInpatientCaseRecord(_dataFormId, _careDirectorQA_TeamId2, _personID, currentDate,
                _systemUserId2, _systemUserId2, _contactReasonId, _contactSourceId, "hdsa", 3, _awaitingAdmission_caseStatusId,
                _systemUserId2, currentDate, currentDate, currentDate, currentDate, _provider_HospitalId,
                _inpatientWardId, false, false, false, false, false, false, false, false, false, false, 2);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber, _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToLeaveAWOLPage();

            leavesAWOLPage
                .WaitForLeavesAWOLPageToLoad()
                .OpenLeaveAWOLRecord(LeaveAWOLID.ToString());

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .NavigateToLeaveAwolCaseNotesArea();

            inpatientLeaveAwolCaseNotesPage
                .WaitForInpatientLeaveAwolCaseNotesPageToLoad()
                .OpenInpatientLeaveAwolCaseNoteRecord(LeaveAWOLCaseNoteID.ToString());

            inpatientLeaveAwolCaseNoteRecordPage
                .WaitForInpatientLeaveAwolCaseNoteRecordPageToLoad()
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

            var statusid = 1; //Open

            Assert.AreEqual("Case Note (For Inpatient Leave Awol) 001", fields["subject"]);
            Assert.AreEqual("Case Note (For Inpatient Leave Awol) Description", fields["notes"]);
            Assert.AreEqual(_personID.ToString(), fields["personid"]);
            Assert.AreEqual(_careDirectorQA_TeamId.ToString(), fields["ownerid"]);
            Assert.AreEqual(_activityReasonId.ToString(), fields["activityreasonid"]);
            Assert.AreEqual(_systemUserId.ToString(), fields["responsibleuserid"]);
            Assert.AreEqual(_activityPriorityId.ToString(), fields["activitypriorityid"]);
            Assert.AreEqual(_activityCategoryId.ToString(), fields["activitycategoryid"]);
            Assert.AreEqual(currentDate, ((DateTime)fields["casenotedate"]).ToLocalTime());
            Assert.AreEqual(_activitySubCategoryId.ToString(), fields["activitysubcategoryid"]);
            Assert.AreEqual(statusid, fields["statusid"]);
            Assert.AreEqual(_activityOutcomeId.ToString(), fields["activityoutcomeid"]);
            Assert.AreEqual(true, fields["informationbythirdparty"]);
            Assert.AreEqual(true, fields["issignificantevent"]);
            Assert.AreEqual(_significantEventCategoryId.ToString(), fields["significanteventcategoryid"]);
            Assert.AreEqual(currentDate.ToLocalTime(), ((DateTime)fields["significanteventdate"]).ToLocalTime());
            Assert.AreEqual(_significantEventSubCategoryId.ToString(), fields["significanteventsubcategoryid"]);
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(LeaveAWOLCaseNoteID.ToString(), fields["clonedfromid"]);
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

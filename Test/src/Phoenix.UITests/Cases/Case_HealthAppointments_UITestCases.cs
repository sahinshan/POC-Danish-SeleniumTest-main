using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Cases
{
    [TestClass]
    public class Case_HealthAppointments_UITestCases : FunctionalTest
    {

        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _ethnicityId;
        private Guid _contactReasonId;
        private Guid _contactSourceId;
        private Guid _personID;
        private string _firstName;
        private string _lastName;
        private Guid _caseStatusId;
        private Guid _communityAndClinicTeamId;
        private Guid _communityClinicDiaryViewSetupId;
        private Guid _providerId_Carer;
        private Guid _caseId;
        private string caseNumber;
        private Guid _contactAdministrativeCategory;
        private Guid _caseServiceTypeRequestedid;
        private Guid _dataFormId_CommunityHealthCase;
        private Guid _communityClinicAppointmentContactTypesId;
        private Guid _communityClinicLocationTypesId;
        private Guid _healthAppointmentReasonId;
        private Guid _communityClinicCareInterventionId;
        public Guid adminUserID1;
        private string _systemUsername;
        private string _systemUserFullName;
        private Guid _systemUserId;
        private Guid HealthAppointmentID;
        private Guid HealthAppointmentCaseNoteID;
        private Guid _activityReasonId;
        private Guid _activityPriorityId;
        private Guid _activityCategoryId;
        private DateTime sourceDate;
        private DateTime significantEventDate;
        private Guid _activitySubCategoryId;
        private Guid _activityOutcomeId;
        private Guid _significantEventCategoryId;
        private Guid _significantEventSubCategoryId;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");

        [TestInitialize()]
        public void TestInitializationMethod()
        {
            try
            {
                sourceDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                significantEventDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").First();

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _careDirectorQA_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

                #endregion  Business Unit

                #region Team

                _careDirectorQA_TeamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion Team

                #region System User "CaseFormCaseNoteUser1"

                _systemUsername = "HealthAppointmentUser_" + _currentDateSuffix;
                _systemUserFullName = "HealthAppointmentUser " + _currentDateSuffix;
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "HealthAppointmentUser", _currentDateSuffix, "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_careDirectorQA_TeamId, "Appointment_Ethnicity", new DateTime(2020, 1, 1));

                #endregion Ethnicity

                #region Contact Reason

                _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Test_Contact (Comm)", _careDirectorQA_TeamId);

                #endregion Contact Reason

                #region Contact Source

                _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Family", _careDirectorQA_TeamId);

                #endregion Contact Source

                #region Health Appointment Reason

                _healthAppointmentReasonId = commonMethodsDB.CreateHealthAppointmentReason(_careDirectorQA_TeamId, "Follow Up Appointment", new DateTime(2020, 1, 1), "3", null);

                #endregion

                #region Contact Administrative Category

                _contactAdministrativeCategory = commonMethodsDB.CreateContactAdministrativeCategory(_careDirectorQA_TeamId, "Test_Administrative Category", new DateTime(2020, 1, 1));

                #endregion

                #region Case Service Type Requested

                _caseServiceTypeRequestedid = commonMethodsDB.CreateCaseServiceTypeRequested(_careDirectorQA_TeamId, "Advice and Consultation", new DateTime(2020, 1, 1));

                #endregion

                #region Data Form Community Health Case

                _dataFormId_CommunityHealthCase = dbHelper.dataForm.GetByName("CommunityHealthCase").FirstOrDefault();

                #endregion

                #region Community/Clinic Appointment Contact Types

                _communityClinicAppointmentContactTypesId = commonMethodsDB.CreateHealthAppointmentContactType(_careDirectorQA_TeamId, "Community_Clinic Appointment Contact Types_Appointment", new DateTime(2020, 1, 1), "3");

                #endregion Community/Clinic Appointment Contact Types

                #region Community/Clinic Appointment Location Types

                _communityClinicLocationTypesId = commonMethodsDB.CreateHealthAppointmentLocationType(_careDirectorQA_TeamId, "Health Clinic managed by Voluntary or Private Agents", new DateTime(2020, 1, 1));

                #endregion Community/Clinic Appointment Location Types

                #region Provider (Carer)

                _providerId_Carer = commonMethodsDB.CreateProvider("CareDirector QA Provider Health Appointment", _careDirectorQA_TeamId, 7);

                #endregion

                #region Community and Clinic Team

                _communityAndClinicTeamId = commonMethodsDB.CreateCommunityAndClinicTeam(_careDirectorQA_TeamId, _providerId_Carer, _careDirectorQA_TeamId, "Ynys Mon - Local Health Board - Primary Team", "Created by Health Appointments");

                _communityClinicDiaryViewSetupId = dbHelper.communityClinicDiaryViewSetup.CreateCommunityClinicDiaryViewSetup(_careDirectorQA_TeamId, _communityAndClinicTeamId, "Home Visit Data", new DateTime(2023, 1, 1), new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0), 500, 100, 500);

                #endregion

                #region Recurrence pattern

                var recurrencePatternExists = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").Any();
                if (!recurrencePatternExists)
                    dbHelper.recurrencePattern.CreateRecurrencePattern(1, 1);

                var _recurrencePatternId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").FirstOrDefault();

                #endregion

                #region Create New User WorkSchedule01

                if (!dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserId).Any())
                    dbHelper.userWorkSchedule.CreateUserWorkSchedule("Work Schedule", _systemUserId, _careDirectorQA_TeamId, _recurrencePatternId, new DateTime(2023, 1, 1), null, new TimeSpan(0, 5, 0), new TimeSpan(23, 55, 0));

                #endregion Create New User WorkSchedule01

                #region Community Clinic Linked Professional

                var _linkedProfessional = dbHelper.communityClinicLinkedProfessional.CreateCommunityClinicLinkedProfessional(_careDirectorQA_TeamId, _communityClinicDiaryViewSetupId, _systemUserId, new DateTime(2023, 2, 2), new TimeSpan(1, 5, 0),
                                                                new TimeSpan(23, 55, 0), _recurrencePatternId, _systemUserFullName);
                #endregion Community Clinic Linked Professional

                #region Community Clinic Care Intervention

                var communityClinicCareIntervention = dbHelper.communityClinicCareIntervention.GetByName("Physical Rehab").Any();
                if (!communityClinicCareIntervention)
                    dbHelper.communityClinicCareIntervention.CreateCommunityClinicCareIntervention(_careDirectorQA_TeamId, "Physical Rehab", new DateTime(2020, 1, 1));
                _communityClinicCareInterventionId = dbHelper.communityClinicCareIntervention.GetByName("Physical Rehab")[0];

                #endregion Health Appointment Outcome Type

                #region Person 1

                _firstName = "Jhon";
                _lastName = DateTime.Now.ToString("yyyyMMdd.HHmmss");
                _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);

                #endregion

                #region Case Status

                _caseStatusId = dbHelper.caseStatus.GetByName("Awaiting Further Information").First();

                #endregion

                #region Community Case record

                _caseId = dbHelper.Case.CreateCommunityHealthCaseRecord(_careDirectorQA_TeamId, _personID, _systemUserId, _communityAndClinicTeamId, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                        _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-3).Date, DateTime.Now.AddDays(-2).Date, DateTime.Now.Date, "a relevant directory where a user would be entitled to make thoroughly clear what is");

                caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];

                #endregion

                #region Activity Categories

                _activityCategoryId = commonMethodsDB.CreateActivityCategory("Assessment", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Activity Sub Categories

                _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory("Health Assessment", new DateTime(2020, 1, 1), _activityCategoryId, _careDirectorQA_TeamId);

                #endregion

                #region Activity Outcome

                _activityOutcomeId = commonMethodsDB.CreateActivityOutcome("Completed", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Activity Reason

                _activityReasonId = commonMethodsDB.CreateActivityReason("Assessment", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Activity Priority

                _activityPriorityId = commonMethodsDB.CreateActivityPriority("Normal", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Significant Event Category

                _activityPriorityId = commonMethodsDB.CreateActivityPriority("Normal", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Significant Event Category

                if (!dbHelper.significantEventCategory.GetByName("Category 1").Any())
                    dbHelper.significantEventCategory.CreateSignificantEventCategory("Category 1", sourceDate, _careDirectorQA_TeamId, null, "", null);
                _significantEventCategoryId = dbHelper.significantEventCategory.GetByName("Category 1")[0];

                #endregion

                #region Significant Event Category

                if (!dbHelper.significantEventSubCategory.GetByName("Category 1_Sub").Any())
                    dbHelper.significantEventSubCategory.CreateSignificantEventSubCategory("Category 1_Sub", sourceDate, _significantEventCategoryId, _careDirectorQA_TeamId);
                _significantEventSubCategoryId = dbHelper.significantEventSubCategory.GetByName("Category 1_Sub")[0];

                #endregion

                #region Data Form Id (Appointments)

                var dataformId = dbHelper.dataForm.GetByName("Appointments").FirstOrDefault();

                #endregion

                #region Health Appointment

                TimeSpan startTime = new TimeSpan(10, 0, 0);
                TimeSpan endTime = new TimeSpan(10, 5, 0);

                HealthAppointmentID = dbHelper.healthAppointment.CreateHealthAppointment1(
                _careDirectorQA_TeamId, _personID, dataformId, _communityClinicAppointmentContactTypesId, _healthAppointmentReasonId, _caseId,
                sourceDate, startTime, endTime, sourceDate, _systemUserId, _communityAndClinicTeamId, _communityClinicLocationTypesId, _systemUserId);

                #endregion

                #region Health Appointment Case Note

                HealthAppointmentCaseNoteID = dbHelper.healthAppointmentCaseNote.CreateHealthAppointmentCaseNote(_careDirectorQA_TeamId, _systemUserId, _caseId,
                                    _personID, HealthAppointmentID, _communityClinicCareInterventionId, _activityCategoryId, _activitySubCategoryId,
                                    _activityOutcomeId, _activityReasonId, _activityPriorityId, sourceDate, "Health Appointment Case Note 001",
                                    1, "Health Appointment Case Note Description", true, true, false, _significantEventCategoryId, _significantEventSubCategoryId, significantEventDate);

                #endregion

            }
            catch (Exception ex)
            {
                if (driver != null)
                    driver.Quit();

                this.ShutDownAllProcesses();

                throw ex;
            }
        }


        #region https://advancedcsg.atlassian.net/browse/CDV6-8418

        [TestProperty("JiraIssueID", "CDV6-11405")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-2446 - " +
            "Open a health appointment case note record (with data in all fields) - Click on the clone button - Wait for the clone popup to be displayed - " +
            "Confirm the clone operation - Validate that the case note record is properly cloned")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void HealthAppointmentCaseNote_Cloning_UITestMethod01()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToHealthAppointmentsPage();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .OpenCaseHealthAppointmentRecord(HealthAppointmentID.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .NavigateToCaseHHealthAppointmentCaseNotesArea();

            healthAppointmentCaseNotesPage
                .WaitForHealthAppointmentCaseNotesPageToLoad()
                .OpenHealthAppointmentCaseNoteRecord(HealthAppointmentCaseNoteID.ToString());

            healthAppointmentCaseNoteRecordPage
                .WaitForHealthAppointmentCaseNoteRecordPageToLoad()
                .ClickCloneButton();

            cloneActivityPopup
                .WaitForCloneActivityPopupToLoad()
                .SelectBusinessObjectTypeText("Case")
                .SelectRetainStatus("Yes")
                .SelectRecord(_caseId.ToString())
                .ClickCloneButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("1 of 1 Activities Cloned Successfully")
                .ClickCloseButton();

            var records = dbHelper.caseCaseNote.GetByCaseID(_caseId);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.caseCaseNote.GetByID(records[0],
                "subject", "notes", "personid", "ownerid", "activityreasonid", "responsibleuserid",
                "activitypriorityid", "activitycategoryid", "casenotedate", "activitysubcategoryid", "statusid", "activityoutcomeid",
                "informationbythirdparty", "issignificantevent", "significanteventcategoryid", "significanteventdate", "significanteventsubcategoryid",
                "iscloned", "clonedfromid");

            Assert.AreEqual("Health Appointment Case Note 001", fields["subject"]);
            Assert.AreEqual("Health Appointment Case Note Description", fields["notes"]);
            Assert.AreEqual(_personID.ToString(), fields["personid"]);
            Assert.AreEqual(_careDirectorQA_TeamId.ToString(), fields["ownerid"]);
            Assert.AreEqual(_activityReasonId.ToString(), fields["activityreasonid"]);
            Assert.AreEqual(_systemUserId.ToString(), fields["responsibleuserid"]);
            Assert.AreEqual(_activityPriorityId.ToString(), fields["activitypriorityid"]);
            Assert.AreEqual(_activityCategoryId.ToString(), fields["activitycategoryid"]);
            Assert.AreEqual(sourceDate, ((DateTime)fields["casenotedate"]).ToLocalTime());
            Assert.AreEqual(_activitySubCategoryId.ToString(), fields["activitysubcategoryid"]);
            Assert.AreEqual(1, fields["statusid"]);
            Assert.AreEqual(_activityOutcomeId.ToString(), fields["activityoutcomeid"]);
            Assert.AreEqual(true, fields["informationbythirdparty"]);
            Assert.AreEqual(true, fields["issignificantevent"]);
            Assert.AreEqual(_significantEventCategoryId.ToString(), fields["significanteventcategoryid"]);
            Assert.AreEqual(_significantEventSubCategoryId.ToString(), fields["significanteventsubcategoryid"]);
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(HealthAppointmentCaseNoteID.ToString(), fields["clonedfromid"]);
            Assert.AreEqual(significantEventDate.ToLocalTime(), ((DateTime)fields["significanteventdate"]).ToLocalTime());

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

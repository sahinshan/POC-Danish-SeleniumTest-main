using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Cases
{
    [DeploymentItem("Files\\RTT Event - Case Discharged.Zip")]
    [DeploymentItem("Files\\RTT Event - Consultant Episode RTT Treatment Status.Zip")]
    [DeploymentItem("Files\\RTT Event - Health Appointment Outcome.Zip")]
    [DeploymentItem("Files\\RTT Event - New RTT Case.Zip"), DeploymentItem("chromedriver.exe")]
    [TestClass]
    public class Case_HealthAppointmentOutcomes_RTTTreatment_UITestCases : FunctionalTest
    {
        private string _environmentName;
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private string _systemUsername;
        private Guid _jobRoleTypeId;
        private Guid _communityAndClinicTeamId;
        private Guid _caseStatusId;
        private Guid _contactReasonId;
        private Guid _contactAdministrativeCategory;
        private Guid _caseServiceTypeRequestedid;
        private Guid _dataFormId_CommunityHealthCase;
        private Guid _dataformId_Appointments;
        private Guid _contactSourceId;
        private Guid rttPathwaySetupId;
        private Guid rttTreatmentStatusId;
        private Guid _communityClinicAppointmentContactTypesId;
        private Guid _communityClinicLocationTypesId;
        private Guid _healthAppointmentReasonId;
        private Guid _personID;
        private string _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");

        #region RTT Treatment Status

        private Guid _rttTreatmentStatus_10_Id;
        private Guid _rttTreatmentStatus_11_Id;
        private Guid _rttTreatmentStatus_12_Id;
        private Guid _rttTreatmentStatus_20_Id;
        private Guid _rttTreatmentStatus_21_Id;
        private Guid _rttTreatmentStatus_30_Id;
        private Guid _rttTreatmentStatus_31_Id;
        private Guid _rttTreatmentStatus_32_Id;
        private Guid _rttTreatmentStatus_33_Id;
        private Guid _rttTreatmentStatus_34_Id;
        private Guid _rttTreatmentStatus_35_Id;
        private Guid _rttTreatmentStatus_36_Id;
        private Guid _rttTreatmentStatus_90_Id;
        private Guid _rttTreatmentStatus_91_Id;
        private Guid _rttTreatmentStatus_92_Id;
        private Guid _rttTreatmentStatus_98_Id;
        private Guid _rttTreatmentStatus_99_Id;

        #endregion

        #region Community/Clinic Appointment Outcome Types

        private Guid _communityClinicAppointmentOutcomeType_10_Id;
        private Guid _communityClinicAppointmentOutcomeType_11_Id;
        private Guid _communityClinicAppointmentOutcomeType_12_Id;
        private Guid _communityClinicAppointmentOutcomeType_20_Id;
        private Guid _communityClinicAppointmentOutcomeType_21_Id;
        private Guid _communityClinicAppointmentOutcomeType_30_Id;
        private Guid _communityClinicAppointmentOutcomeType_31_Id;
        private Guid _communityClinicAppointmentOutcomeType_32_Id;
        private Guid _communityClinicAppointmentOutcomeType_33_Id;
        private Guid _communityClinicAppointmentOutcomeType_34_Id;
        private Guid _communityClinicAppointmentOutcomeType_35_Id;
        private Guid _communityClinicAppointmentOutcomeType_36_Id;
        private Guid _communityClinicAppointmentOutcomeType_90_Id;
        private Guid _communityClinicAppointmentOutcomeType_91_Id;
        private Guid _communityClinicAppointmentOutcomeType_92_Id;
        private Guid _communityClinicAppointmentOutcomeType_98_Id;
        private Guid _communityClinicAppointmentOutcomeType_99_Id;

        #endregion

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

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("RTT BU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("RTT T1", null, _businessUnitId, "907678", "RTTT1@careworkstempmail.com", "RTT T1", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region Default System User

                _systemUsername = "RTTTreatmentUser_" + _currentDateString;
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "RTTTreatmentUser", _currentDateString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
                var _systemUserFullName = "RTTTreatmentUser " + _currentDateString;

                var unscheduledAppointmentsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Unscheduled Health Appointments").First();
                commonMethodsDB.CreateUserSecurityProfile(_systemUserId, unscheduledAppointmentsSecurityProfileId);

                #endregion

                #region Job Role Type

                _jobRoleTypeId = commonMethodsDB.CreateJobRoleType(_teamId, "Default Consultant", new DateTime(2020, 1, 1), 1);
                dbHelper.jobRoleType.UpdateIsConsultantId(_jobRoleTypeId, true);

                #endregion

                #region Update System User Job Role Type

                dbHelper.systemUser.UpdateJobRoleType(_systemUserId, _jobRoleTypeId);

                #endregion

                #region Person 1

                var _firstName = "Jhon";
                var _lastName = DateTime.Now.ToString("yyyyMMdd.HHmmss");
                _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);

                #endregion

                #region Case Status

                _caseStatusId = dbHelper.caseStatus.GetByName("Awaiting Further Information").First();

                #endregion

                #region Contact Reason

                _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Test_Contact (Comm)", _teamId);

                #endregion Contact Reason

                #region Contact Source

                _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Default Contact Source", _teamId);

                #endregion

                #region Health Appointment Reason

                _healthAppointmentReasonId = commonMethodsDB.CreateHealthAppointmentReason(_teamId, "Follow Up Appointment", new DateTime(2020, 1, 1), "3", null);

                #endregion

                #region Contact Administrative Category

                _contactAdministrativeCategory = commonMethodsDB.CreateContactAdministrativeCategory(_teamId, "Test_Administrative Category", new DateTime(2020, 1, 1));

                #endregion

                #region Case Service Type Requested

                _caseServiceTypeRequestedid = commonMethodsDB.CreateCaseServiceTypeRequested(_teamId, "Advice and Consultation", new DateTime(2020, 1, 1));

                #endregion

                #region Data Form Community Health Case

                _dataFormId_CommunityHealthCase = dbHelper.dataForm.GetByName("CommunityHealthCase").FirstOrDefault();
                _dataformId_Appointments = dbHelper.dataForm.GetByName("Appointments").FirstOrDefault();

                #endregion

                #region Community/Clinic Appointment Contact Types

                _communityClinicAppointmentContactTypesId = commonMethodsDB.CreateHealthAppointmentContactType(_teamId, "Community_Clinic Appointment Contact Types_Appointment", new DateTime(2020, 1, 1), "3");

                #endregion

                #region Community/Clinic Appointment Location Types

                _communityClinicLocationTypesId = commonMethodsDB.CreateHealthAppointmentLocationType(_teamId, "Health Clinic managed by Voluntary or Private Agents", new DateTime(2020, 1, 1));

                #endregion


                #region RTT Treatment Function

                var rttTreatmentFunctionCodeId = dbHelper.rttTreatmentFunctionCode.GetByName("General Surgery Service")[0];

                #endregion

                #region Provider (Carer)

                var _providerId_Carer = commonMethodsDB.CreateProvider("CareDirector QA Provider Health Appointment", _teamId, 7);
                dbHelper.provider.UpdateRTTTreatmentFunction(_providerId_Carer, rttTreatmentFunctionCodeId);

                #endregion

                #region Community and Clinic Team

                _communityAndClinicTeamId = commonMethodsDB.CreateCommunityAndClinicTeam(_teamId, _providerId_Carer, _teamId, "RTT Treatment Status Team", "Created by Health Appointments");

                var _communityClinicDiaryViewSetupId = dbHelper.communityClinicDiaryViewSetup.CreateCommunityClinicDiaryViewSetup(_teamId, _communityAndClinicTeamId, "Home Visit Data", new DateTime(2023, 1, 1), new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0), 500, 100, 500);

                #endregion

                #region Recurrence pattern

                var recurrencePatternExists = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").Any();
                if (!recurrencePatternExists)
                    dbHelper.recurrencePattern.CreateRecurrencePattern(1, 1);

                var _recurrencePatternId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").FirstOrDefault();

                #endregion

                #region Create New User WorkSchedule01

                if (!dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserId).Any())
                    dbHelper.userWorkSchedule.CreateUserWorkSchedule("Work Schedule", _systemUserId, _teamId, _recurrencePatternId, new DateTime(2023, 1, 1), null, new TimeSpan(0, 5, 0), new TimeSpan(23, 55, 0));

                #endregion

                #region Community Clinic Linked Professional

                var _linkedProfessional = dbHelper.communityClinicLinkedProfessional.CreateCommunityClinicLinkedProfessional(_teamId, _communityClinicDiaryViewSetupId, _systemUserId, new DateTime(2023, 2, 2), new TimeSpan(1, 5, 0),
                                                                new TimeSpan(23, 55, 0), _recurrencePatternId, _systemUserFullName);
                #endregion

                #region Import the workflows and publish them

                var workflow1Id = commonMethodsDB.CreateWorkflowIfNeeded("RTT Event - Case Discharged", "RTT Event - Case Discharged.Zip");
                var workflow2Id = commonMethodsDB.CreateWorkflowIfNeeded("RTT Event - Consultant Episode RTT Treatment Status", "RTT Event - Consultant Episode RTT Treatment Status.Zip");
                var workflow3Id = commonMethodsDB.CreateWorkflowIfNeeded("RTT Event - Health Appointment Outcome", "RTT Event - Health Appointment Outcome.Zip");
                var workflow4Id = commonMethodsDB.CreateWorkflowIfNeeded("RTT Event - New RTT Case", "RTT Event - New RTT Case.Zip");

                dbHelper.workflow.UpdatePublishedField(workflow1Id, true);
                dbHelper.workflow.UpdatePublishedField(workflow2Id, true);
                dbHelper.workflow.UpdatePublishedField(workflow3Id, true);
                dbHelper.workflow.UpdatePublishedField(workflow4Id, true);

                #endregion

                #region RTT Treatment Statuses

                rttTreatmentStatusId = dbHelper.rttTreatmentStatus.GetByName("First Activity in new RTT period")[0];

                _rttTreatmentStatus_10_Id = dbHelper.rttTreatmentStatus.GetByGovCode("10")[0];
                _rttTreatmentStatus_11_Id = dbHelper.rttTreatmentStatus.GetByGovCode("11")[0];
                _rttTreatmentStatus_12_Id = dbHelper.rttTreatmentStatus.GetByGovCode("12")[0];
                _rttTreatmentStatus_20_Id = dbHelper.rttTreatmentStatus.GetByGovCode("20")[0];
                _rttTreatmentStatus_21_Id = dbHelper.rttTreatmentStatus.GetByGovCode("21")[0];
                _rttTreatmentStatus_30_Id = dbHelper.rttTreatmentStatus.GetByGovCode("30")[0];

                _rttTreatmentStatus_31_Id = dbHelper.rttTreatmentStatus.GetByGovCode("31")[0];
                _rttTreatmentStatus_32_Id = dbHelper.rttTreatmentStatus.GetByGovCode("32")[0];
                _rttTreatmentStatus_33_Id = dbHelper.rttTreatmentStatus.GetByGovCode("33")[0];
                _rttTreatmentStatus_34_Id = dbHelper.rttTreatmentStatus.GetByGovCode("34")[0];

                _rttTreatmentStatus_35_Id = dbHelper.rttTreatmentStatus.GetByGovCode("35")[0];
                _rttTreatmentStatus_36_Id = dbHelper.rttTreatmentStatus.GetByGovCode("36")[0];
                _rttTreatmentStatus_90_Id = dbHelper.rttTreatmentStatus.GetByGovCode("90")[0];
                _rttTreatmentStatus_91_Id = dbHelper.rttTreatmentStatus.GetByGovCode("91")[0];
                _rttTreatmentStatus_92_Id = dbHelper.rttTreatmentStatus.GetByGovCode("92")[0];
                _rttTreatmentStatus_98_Id = dbHelper.rttTreatmentStatus.GetByGovCode("98")[0];
                _rttTreatmentStatus_99_Id = dbHelper.rttTreatmentStatus.GetByGovCode("99")[0];

                #endregion

                #region RTT Pathway Setup

                rttPathwaySetupId = commonMethodsDB.CreateRTTPathwaySetup(_teamId, "Default RTT Pathway", new DateTime(2020, 1, 1), 2, 5, 10);

                #endregion

                #region Create Community Clinic Appointment Outcome Types

                CreateCommunityClinicAppointmentOutcomeTypes();

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        public void CreateCommunityClinicAppointmentOutcomeTypes()
        {
            _communityClinicAppointmentOutcomeType_10_Id = commonMethodsDB.CreateHealthAppointmentOutcomeType(_teamId, "RTT_CC_AppointmentOutcome_10", new DateTime(2021, 1, 1), "", "10", _rttTreatmentStatus_10_Id);
            _communityClinicAppointmentOutcomeType_11_Id = commonMethodsDB.CreateHealthAppointmentOutcomeType(_teamId, "RTT_CC_AppointmentOutcome_11", new DateTime(2021, 1, 1), "", "11", _rttTreatmentStatus_11_Id);
            _communityClinicAppointmentOutcomeType_12_Id = commonMethodsDB.CreateHealthAppointmentOutcomeType(_teamId, "RTT_CC_AppointmentOutcome_12", new DateTime(2021, 1, 1), "", "12", _rttTreatmentStatus_12_Id);
            _communityClinicAppointmentOutcomeType_20_Id = commonMethodsDB.CreateHealthAppointmentOutcomeType(_teamId, "RTT_CC_AppointmentOutcome_20", new DateTime(2021, 1, 1), "", "20", _rttTreatmentStatus_20_Id);
            _communityClinicAppointmentOutcomeType_21_Id = commonMethodsDB.CreateHealthAppointmentOutcomeType(_teamId, "RTT_CC_AppointmentOutcome_21", new DateTime(2021, 1, 1), "", "21", _rttTreatmentStatus_21_Id);
            _communityClinicAppointmentOutcomeType_30_Id = commonMethodsDB.CreateHealthAppointmentOutcomeType(_teamId, "RTT_CC_AppointmentOutcome_30", new DateTime(2021, 1, 1), "", "30", _rttTreatmentStatus_30_Id);

            _communityClinicAppointmentOutcomeType_31_Id = commonMethodsDB.CreateHealthAppointmentOutcomeType(_teamId, "RTT_CC_AppointmentOutcome_31", new DateTime(2021, 1, 1), "", "31", _rttTreatmentStatus_31_Id);
            _communityClinicAppointmentOutcomeType_32_Id = commonMethodsDB.CreateHealthAppointmentOutcomeType(_teamId, "RTT_CC_AppointmentOutcome_32", new DateTime(2021, 1, 1), "", "32", _rttTreatmentStatus_32_Id);
            _communityClinicAppointmentOutcomeType_33_Id = commonMethodsDB.CreateHealthAppointmentOutcomeType(_teamId, "RTT_CC_AppointmentOutcome_33", new DateTime(2021, 1, 1), "", "33", _rttTreatmentStatus_33_Id);
            _communityClinicAppointmentOutcomeType_34_Id = commonMethodsDB.CreateHealthAppointmentOutcomeType(_teamId, "RTT_CC_AppointmentOutcome_34", new DateTime(2021, 1, 1), "", "34", _rttTreatmentStatus_34_Id);

            _communityClinicAppointmentOutcomeType_35_Id = commonMethodsDB.CreateHealthAppointmentOutcomeType(_teamId, "RTT_CC_AppointmentOutcome_35", new DateTime(2021, 1, 1), "", "35", _rttTreatmentStatus_35_Id);
            _communityClinicAppointmentOutcomeType_36_Id = commonMethodsDB.CreateHealthAppointmentOutcomeType(_teamId, "RTT_CC_AppointmentOutcome_36", new DateTime(2021, 1, 1), "", "36", _rttTreatmentStatus_36_Id);
            _communityClinicAppointmentOutcomeType_90_Id = commonMethodsDB.CreateHealthAppointmentOutcomeType(_teamId, "RTT_CC_AppointmentOutcome_90", new DateTime(2021, 1, 1), "", "90", _rttTreatmentStatus_90_Id);
            _communityClinicAppointmentOutcomeType_91_Id = commonMethodsDB.CreateHealthAppointmentOutcomeType(_teamId, "RTT_CC_AppointmentOutcome_91", new DateTime(2021, 1, 1), "", "91", _rttTreatmentStatus_91_Id);
            _communityClinicAppointmentOutcomeType_92_Id = commonMethodsDB.CreateHealthAppointmentOutcomeType(_teamId, "RTT_CC_AppointmentOutcome_92", new DateTime(2021, 1, 1), "", "92", _rttTreatmentStatus_92_Id);
            _communityClinicAppointmentOutcomeType_98_Id = commonMethodsDB.CreateHealthAppointmentOutcomeType(_teamId, "RTT_CC_AppointmentOutcome_98", new DateTime(2021, 1, 1), "", "98", _rttTreatmentStatus_98_Id);
            _communityClinicAppointmentOutcomeType_99_Id = commonMethodsDB.CreateHealthAppointmentOutcomeType(_teamId, "RTT_CC_AppointmentOutcome_99", new DateTime(2021, 1, 1), "", "99", _rttTreatmentStatus_99_Id);
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-1791 & https://advancedcsg.atlassian.net/browse/ACC-2334

        [TestProperty("JiraIssueID", "ACC-2360")]
        [Description("Step(s) 1 to 3 for 'Appointments' & 'Unscheduled Appointment' for the test CDV6-22682")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_HealthAppointmentOutcomes_RTTTreatment_UITestMethod01()
        {
            #region Community Case record

            var rttReferral = 1;

            // Case Record 1
            var _caseId = dbHelper.Case.CreateCommunityHealthCaseRecord(_teamId, _personID, _systemUserId, _communityAndClinicTeamId, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                            _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-5).Date, DateTime.Now.Date, "a relevant directory where a user would be entitled to make thoroughly clear what is",
                            _systemUserId, DateTime.Now.Date, rttReferral, rttPathwaySetupId, rttTreatmentStatusId);
            var _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];

            // Case Record 2
            var _caseId2 = dbHelper.Case.CreateCommunityHealthCaseRecord(_teamId, _personID, _systemUserId, _communityAndClinicTeamId, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                            _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-5).Date, DateTime.Now.Date, "a relevant directory where a user would be entitled to make thoroughly clear what is",
                            _systemUserId, DateTime.Now.Date, rttReferral, rttPathwaySetupId, _rttTreatmentStatus_21_Id);

            var _caseNumber2 = (string)dbHelper.Case.GetCaseByID(_caseId2, "casenumber")["casenumber"];

            #endregion

            #region Health Appointment

            TimeSpan startTime = new TimeSpan(10, 0, 0);
            TimeSpan endTime = new TimeSpan(10, 5, 0);

            // Health Appointment Record 1 for Case 1
            var _healthAppointmentID = dbHelper.healthAppointment.CreateHealthAppointment1(
                                    _teamId, _personID, _dataformId_Appointments, _communityClinicAppointmentContactTypesId, _healthAppointmentReasonId, _caseId,
                                    DateTime.Now.Date, startTime, endTime, DateTime.Now.Date, _systemUserId, _communityAndClinicTeamId, _communityClinicLocationTypesId, _systemUserId);

            // Health Appointment Record 2 for Case 2
            var _healthAppointmentID2 = dbHelper.healthAppointment.CreateHealthAppointment1(
                                    _teamId, _personID, _dataformId_Appointments, _communityClinicAppointmentContactTypesId, _healthAppointmentReasonId, _caseId2,
                                    DateTime.Now.AddDays(-2).Date, startTime, endTime, DateTime.Now.AddDays(-2).Date, _systemUserId, _communityAndClinicTeamId, _communityClinicLocationTypesId, _systemUserId);

            #endregion

            #region Step 1 to 2 & 15

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber.ToString(), _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber.ToString());

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToHealthAppointmentsPage();

            var rttWaitTimeRecords = dbHelper.rttWaitTime.GetByRelatedCase(_caseId);
            Assert.AreEqual(1, rttWaitTimeRecords.Count);
            var rttWaitTimeRecordId = rttWaitTimeRecords.First();

            var rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(1, rttEventsRecords.Count);

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .OpenCaseHealthAppointmentRecord(_healthAppointmentID.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickOutcomeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_10_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_11_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_12_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_20_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_21_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_30_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_31_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_32_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_33_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_34_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_35_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_36_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_90_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_91_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_92_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_98_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_99_Id.ToString())
                .ClickCloseButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .TapBackButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickNewRecordButton();

            // Step 15 for Unscheduled Appointment
            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Unscheduled Appointment")
                .TapNextButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickOutcomeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_10_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_11_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_12_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_20_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_21_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_30_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_31_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_32_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_33_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_34_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_35_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_36_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_90_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_91_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_92_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_98_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_99_Id.ToString())
                .ClickCloseButton();

            #endregion

            #region Step 3 & 15

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber2.ToString(), _caseId2.ToString())
                .OpenCaseRecord(_caseId2.ToString(), _caseNumber2.ToString());

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToHealthAppointmentsPage();

            var rttWaitTimeRecords2 = dbHelper.rttWaitTime.GetByRelatedCase(_caseId2);
            Assert.AreEqual(1, rttWaitTimeRecords2.Count);
            var rttWaitTimeRecordId2 = rttWaitTimeRecords2.First();

            var rttEventsRecords2 = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId2);
            Assert.AreEqual(1, rttEventsRecords2.Count);

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .OpenCaseHealthAppointmentRecord(_healthAppointmentID2.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickOutcomeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_10_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_11_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_12_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_20_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_21_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_30_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_31_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_32_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_33_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_34_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_35_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_36_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_90_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_91_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_92_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_98_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_99_Id.ToString())
                .ClickCloseButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .TapBackButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickNewRecordButton();

            // Step 15 for Unscheduled Appointment
            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Unscheduled Appointment")
                .TapNextButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickOutcomeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_10_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_11_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_12_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_20_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_21_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_30_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_31_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_32_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_33_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_34_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_35_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_36_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_90_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_91_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_92_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_98_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_99_Id.ToString())
                .ClickCloseButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2361")]
        [Description("Step(s) 4 to 5 for 'Appointments' & 'Unscheduled Appointment' for the test CDV6-22682")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_HealthAppointmentOutcomes_RTTTreatment_UITestMethod02()
        {
            #region Community Case record

            var rttReferral = 1;

            // Case Record 1
            var _caseId = dbHelper.Case.CreateCommunityHealthCaseRecord(_teamId, _personID, _systemUserId, _communityAndClinicTeamId, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                            _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-5).Date, DateTime.Now.Date, "a relevant directory where a user would be entitled to make thoroughly clear what is",
                            _systemUserId, DateTime.Now.Date, rttReferral, rttPathwaySetupId, _rttTreatmentStatus_30_Id);

            var _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];

            // Case Record 2
            var _caseId2 = dbHelper.Case.CreateCommunityHealthCaseRecord(_teamId, _personID, _systemUserId, _communityAndClinicTeamId, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                            _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-5).Date, DateTime.Now.Date, "a relevant directory where a user would be entitled to make thoroughly clear what is",
                            _systemUserId, DateTime.Now.Date, rttReferral, rttPathwaySetupId, _rttTreatmentStatus_31_Id);

            var _caseNumber2 = (string)dbHelper.Case.GetCaseByID(_caseId2, "casenumber")["casenumber"];

            #endregion

            #region Health Appointment

            TimeSpan startTime = new TimeSpan(10, 0, 0);
            TimeSpan endTime = new TimeSpan(10, 5, 0);

            // Health Appointment Record 1 for Case 1
            var _healthAppointmentID = dbHelper.healthAppointment.CreateHealthAppointment1(
                                _teamId, _personID, _dataformId_Appointments, _communityClinicAppointmentContactTypesId, _healthAppointmentReasonId, _caseId,
                                DateTime.Now.Date, startTime, endTime, DateTime.Now.Date, _systemUserId, _communityAndClinicTeamId, _communityClinicLocationTypesId, _systemUserId);

            // Health Appointment Record 2 for Case 2
            var _healthAppointmentID2 = dbHelper.healthAppointment.CreateHealthAppointment1(
                                _teamId, _personID, _dataformId_Appointments, _communityClinicAppointmentContactTypesId, _healthAppointmentReasonId, _caseId2,
                                DateTime.Now.AddDays(-2).Date, startTime, endTime, DateTime.Now.AddDays(-2).Date, _systemUserId, _communityAndClinicTeamId, _communityClinicLocationTypesId, _systemUserId);

            #endregion

            #region Step 4 & 15

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber.ToString(), _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber.ToString());

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToHealthAppointmentsPage();

            var rttWaitTimeRecords = dbHelper.rttWaitTime.GetByRelatedCase(_caseId);
            Assert.AreEqual(1, rttWaitTimeRecords.Count);
            var rttWaitTimeRecordId = rttWaitTimeRecords.First();

            var rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(1, rttEventsRecords.Count);

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .OpenCaseHealthAppointmentRecord(_healthAppointmentID.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickOutcomeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_10_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_11_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_12_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_20_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_21_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_30_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_31_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_32_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_33_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_34_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_35_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_36_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_90_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_91_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_92_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_98_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_99_Id.ToString())
                .ClickCloseButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .TapBackButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickNewRecordButton();

            // Step 15 for Unscheduled Appointment
            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Unscheduled Appointment")
                .TapNextButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickOutcomeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_10_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_11_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_12_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_20_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_21_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_30_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_31_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_32_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_33_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_34_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_35_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_36_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_90_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_91_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_92_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_98_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_99_Id.ToString())
                .ClickCloseButton();

            #endregion

            #region Step 5 & 15

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber2.ToString(), _caseId2.ToString())
                .OpenCaseRecord(_caseId2.ToString(), _caseNumber2.ToString());

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToHealthAppointmentsPage();

            var rttWaitTimeRecords2 = dbHelper.rttWaitTime.GetByRelatedCase(_caseId2);
            Assert.AreEqual(1, rttWaitTimeRecords2.Count);
            var rttWaitTimeRecordId2 = rttWaitTimeRecords2.First();

            var rttEventsRecords2 = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId2);
            Assert.AreEqual(1, rttEventsRecords2.Count);

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .OpenCaseHealthAppointmentRecord(_healthAppointmentID2.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickOutcomeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_10_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_11_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_12_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_20_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_21_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_30_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_31_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_32_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_33_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_34_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_35_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_36_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_90_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_91_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_92_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_98_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_99_Id.ToString())
                .ClickCloseButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .TapBackButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickNewRecordButton();

            // Step 15 for Unscheduled Appointment
            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Unscheduled Appointment")
                .TapNextButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickOutcomeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_10_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_11_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_12_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_20_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_21_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_30_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_31_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_32_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_33_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_34_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_35_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_36_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_90_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_91_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_92_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_98_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_99_Id.ToString())
                .ClickCloseButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2362")]
        [Description("Step(s) 6 to 7 for 'Appointments' & Unscheduled Appointment' for the test CDV6-22682")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_HealthAppointmentOutcomes_RTTTreatment_UITestMethod03()
        {
            #region Community Case record

            var rttReferral = 1;

            // Case Record 1
            var _caseId = dbHelper.Case.CreateCommunityHealthCaseRecord(_teamId, _personID, _systemUserId, _communityAndClinicTeamId, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                            _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-5).Date, DateTime.Now.Date, "a relevant directory where a user would be entitled to make thoroughly clear what is",
                            _systemUserId, DateTime.Now.Date, rttReferral, rttPathwaySetupId, _rttTreatmentStatus_34_Id);

            var _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];

            // Case Record 2
            var _caseId2 = dbHelper.Case.CreateCommunityHealthCaseRecord(_teamId, _personID, _systemUserId, _communityAndClinicTeamId, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                            _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-5).Date, DateTime.Now.Date, "a relevant directory where a user would be entitled to make thoroughly clear what is",
                            _systemUserId, DateTime.Now.Date, rttReferral, rttPathwaySetupId, _rttTreatmentStatus_35_Id);

            var _caseNumber2 = (string)dbHelper.Case.GetCaseByID(_caseId2, "casenumber")["casenumber"];

            #endregion

            #region Health Appointment

            TimeSpan startTime = new TimeSpan(10, 0, 0);
            TimeSpan endTime = new TimeSpan(10, 5, 0);

            // Health Appointment Record 1 for Case 1
            var _healthAppointmentID = dbHelper.healthAppointment.CreateHealthAppointment1(
                                    _teamId, _personID, _dataformId_Appointments, _communityClinicAppointmentContactTypesId, _healthAppointmentReasonId, _caseId,
                                    DateTime.Now.Date, startTime, endTime, DateTime.Now.Date, _systemUserId, _communityAndClinicTeamId, _communityClinicLocationTypesId, _systemUserId);

            // Health Appointment Record 2 for Case 2
            var _healthAppointmentID2 = dbHelper.healthAppointment.CreateHealthAppointment1(
                                    _teamId, _personID, _dataformId_Appointments, _communityClinicAppointmentContactTypesId, _healthAppointmentReasonId, _caseId2,
                                    DateTime.Now.AddDays(-2).Date, startTime, endTime, DateTime.Now.AddDays(-2).Date, _systemUserId, _communityAndClinicTeamId, _communityClinicLocationTypesId, _systemUserId);

            #endregion

            #region Step 6 & 15

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber.ToString(), _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber.ToString());

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToHealthAppointmentsPage();

            var rttWaitTimeRecords = dbHelper.rttWaitTime.GetByRelatedCase(_caseId);
            Assert.AreEqual(1, rttWaitTimeRecords.Count);
            var rttWaitTimeRecordId = rttWaitTimeRecords.First();

            var rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(1, rttEventsRecords.Count);

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .OpenCaseHealthAppointmentRecord(_healthAppointmentID.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickOutcomeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_10_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_11_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_12_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_20_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_21_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_30_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_31_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_32_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_33_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_34_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_35_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_36_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_90_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_91_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_92_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_98_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_99_Id.ToString())
                .ClickCloseButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .TapBackButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickNewRecordButton();

            // Step 15 for Unscheduled Appointment
            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Unscheduled Appointment")
                .TapNextButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickOutcomeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_10_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_11_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_12_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_20_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_21_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_30_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_31_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_32_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_33_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_34_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_35_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_36_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_90_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_91_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_92_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_98_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_99_Id.ToString())
                .ClickCloseButton();

            #endregion

            #region Step 7 & 15

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber2.ToString(), _caseId2.ToString())
                .OpenCaseRecord(_caseId2.ToString(), _caseNumber2.ToString());

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToHealthAppointmentsPage();

            var rttWaitTimeRecords2 = dbHelper.rttWaitTime.GetByRelatedCase(_caseId2);
            Assert.AreEqual(1, rttWaitTimeRecords2.Count);
            var rttWaitTimeRecordId2 = rttWaitTimeRecords2.First();

            var rttEventsRecords2 = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId2);
            Assert.AreEqual(1, rttEventsRecords2.Count);

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .OpenCaseHealthAppointmentRecord(_healthAppointmentID2.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickOutcomeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_10_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_11_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_12_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_20_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_21_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_30_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_31_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_32_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_33_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_34_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_35_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_36_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_90_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_91_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_92_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_98_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_99_Id.ToString())
                .ClickCloseButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .TapBackButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickNewRecordButton();

            // Step 15 for Unscheduled Appointment
            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Unscheduled Appointment")
                .TapNextButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickOutcomeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_10_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_11_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_12_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_20_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_21_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_30_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_31_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_32_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_33_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_34_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_35_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_36_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_90_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_91_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_92_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_98_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_99_Id.ToString())
                .ClickCloseButton();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-2363")]
        [Description("Step(s) 8 to 10 for 'Appointments' & 'Unscheduled Appointment' for the test CDV6-22682")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_HealthAppointmentOutcomes_RTTTreatment_UITestMethod04()
        {
            #region Community Case record

            var rttReferral = 1;

            // Case Record 1
            var _caseId = dbHelper.Case.CreateCommunityHealthCaseRecord(_teamId, _personID, _systemUserId, _communityAndClinicTeamId, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                            _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-5).Date, DateTime.Now.Date, "a relevant directory where a user would be entitled to make thoroughly clear what is",
                            _systemUserId, DateTime.Now.Date, rttReferral, rttPathwaySetupId, _rttTreatmentStatus_90_Id);

            var _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];

            // Case Record 2
            var _caseId2 = dbHelper.Case.CreateCommunityHealthCaseRecord(_teamId, _personID, _systemUserId, _communityAndClinicTeamId, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                            _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-5).Date, DateTime.Now.Date, "a relevant directory where a user would be entitled to make thoroughly clear what is",
                            _systemUserId, DateTime.Now.Date, rttReferral, rttPathwaySetupId, _rttTreatmentStatus_91_Id);

            var _caseNumber2 = (string)dbHelper.Case.GetCaseByID(_caseId2, "casenumber")["casenumber"];

            // Case Record 3
            var _caseId3 = dbHelper.Case.CreateCommunityHealthCaseRecord(_teamId, _personID, _systemUserId, _communityAndClinicTeamId, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                            _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-5).Date, DateTime.Now.Date, "a relevant directory where a user would be entitled to make thoroughly clear what is",
                            _systemUserId, DateTime.Now.Date, rttReferral, rttPathwaySetupId, _rttTreatmentStatus_99_Id);

            var _caseNumber3 = (string)dbHelper.Case.GetCaseByID(_caseId3, "casenumber")["casenumber"];

            #endregion

            #region Health Appointment

            TimeSpan startTime = new TimeSpan(10, 0, 0);
            TimeSpan endTime = new TimeSpan(10, 5, 0);

            // Health Appointment Record 1 for Case 1
            var _healthAppointmentID = dbHelper.healthAppointment.CreateHealthAppointment1(
                                    _teamId, _personID, _dataformId_Appointments, _communityClinicAppointmentContactTypesId, _healthAppointmentReasonId, _caseId,
                                    DateTime.Now.Date, startTime, endTime, DateTime.Now.Date, _systemUserId, _communityAndClinicTeamId, _communityClinicLocationTypesId, _systemUserId);

            // Health Appointment Record 2 for Case 2
            var _healthAppointmentID2 = dbHelper.healthAppointment.CreateHealthAppointment1(
                                    _teamId, _personID, _dataformId_Appointments, _communityClinicAppointmentContactTypesId, _healthAppointmentReasonId, _caseId2,
                                    DateTime.Now.AddDays(-2).Date, startTime, endTime, DateTime.Now.AddDays(-2).Date, _systemUserId, _communityAndClinicTeamId, _communityClinicLocationTypesId, _systemUserId);

            // Health Appointment Record 3 for Case 3
            var _healthAppointmentID3 = dbHelper.healthAppointment.CreateHealthAppointment1(
                                    _teamId, _personID, _dataformId_Appointments, _communityClinicAppointmentContactTypesId, _healthAppointmentReasonId, _caseId3,
                                    DateTime.Now.AddDays(-4).Date, startTime, endTime, DateTime.Now.AddDays(-4).Date, _systemUserId, _communityAndClinicTeamId, _communityClinicLocationTypesId, _systemUserId);

            #endregion

            #region Step 8 & 15

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber.ToString(), _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber.ToString());

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToHealthAppointmentsPage();

            var rttWaitTimeRecords = dbHelper.rttWaitTime.GetByRelatedCase(_caseId);
            Assert.AreEqual(1, rttWaitTimeRecords.Count);
            var rttWaitTimeRecordId = rttWaitTimeRecords.First();

            var rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(1, rttEventsRecords.Count);

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .OpenCaseHealthAppointmentRecord(_healthAppointmentID.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickOutcomeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_10_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_11_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_12_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_20_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_21_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_30_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_31_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_32_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_33_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_34_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_35_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_36_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_90_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_91_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_92_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_98_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_99_Id.ToString())
                .ClickCloseButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .TapBackButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickNewRecordButton();

            // Step 15 for Unscheduled Appointment
            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Unscheduled Appointment")
                .TapNextButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickOutcomeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_10_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_11_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_12_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_20_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_21_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_30_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_31_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_32_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_33_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_34_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_35_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_36_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_90_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_91_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_92_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_98_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_99_Id.ToString())
                .ClickCloseButton();

            #endregion

            #region Step 9 & 15

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber2.ToString(), _caseId2.ToString())
                .OpenCaseRecord(_caseId2.ToString(), _caseNumber2.ToString());

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToHealthAppointmentsPage();

            var rttWaitTimeRecords2 = dbHelper.rttWaitTime.GetByRelatedCase(_caseId2);
            Assert.AreEqual(1, rttWaitTimeRecords2.Count);
            var rttWaitTimeRecordId2 = rttWaitTimeRecords2.First();

            var rttEventsRecords2 = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId2);
            Assert.AreEqual(1, rttEventsRecords2.Count);

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .OpenCaseHealthAppointmentRecord(_healthAppointmentID2.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickOutcomeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_10_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_11_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_12_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_20_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_21_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_30_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_31_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_32_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_33_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_34_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_35_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_36_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_90_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_91_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_92_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_98_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_99_Id.ToString())
                .ClickCloseButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .TapBackButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickNewRecordButton();

            // Step 15 for Unscheduled Appointment
            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Unscheduled Appointment")
                .TapNextButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickOutcomeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_10_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_11_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_12_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_20_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_21_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_30_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_31_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_32_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_33_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_34_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_35_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_36_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_90_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_91_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_92_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_98_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_99_Id.ToString())
                .ClickCloseButton();

            #endregion

            #region Step 10 & 15

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber3.ToString(), _caseId3.ToString())
                .OpenCaseRecord(_caseId3.ToString(), _caseNumber3.ToString());

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToHealthAppointmentsPage();

            var rttWaitTimeRecords3 = dbHelper.rttWaitTime.GetByRelatedCase(_caseId3);
            Assert.AreEqual(1, rttWaitTimeRecords3.Count);
            var rttWaitTimeRecordId3 = rttWaitTimeRecords3.First();

            var rttEventsRecords3 = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId3);
            Assert.AreEqual(1, rttEventsRecords3.Count);

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .OpenCaseHealthAppointmentRecord(_healthAppointmentID3.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickOutcomeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_10_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_11_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_12_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_20_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_21_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_30_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_31_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_32_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_33_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_34_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_35_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_36_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_90_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_91_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_92_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_98_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_99_Id.ToString())
                .ClickCloseButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .TapBackButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickNewRecordButton();

            // Step 15 for Unscheduled Appointment
            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Unscheduled Appointment")
                .TapNextButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickOutcomeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_10_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_11_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_12_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_20_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_21_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_30_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_31_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_32_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_33_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_34_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_35_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_36_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_90_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_91_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_92_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_98_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_99_Id.ToString())
                .ClickCloseButton();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-2364")]
        [Description("Step(s) 11 to 13 for 'Appointments' & Step 11 to 12 for 'Unscheduled Appointment' for the test CDV6-22682")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_HealthAppointmentOutcomes_RTTTreatment_UITestMethod05()
        {
            #region Community Case record

            var rttReferral = 1;

            // Case Record 1
            var _caseId = dbHelper.Case.CreateCommunityHealthCaseRecord(_teamId, _personID, _systemUserId, _communityAndClinicTeamId, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                            _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-5).Date, DateTime.Now.Date, "a relevant directory where a user would be entitled to make thoroughly clear what is",
                            _systemUserId, DateTime.Now.Date, rttReferral, rttPathwaySetupId, _rttTreatmentStatus_98_Id);

            var _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];

            // Case Record 2
            var _caseId2 = dbHelper.Case.CreateCommunityHealthCaseRecord(_teamId, _personID, _systemUserId, _communityAndClinicTeamId, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                            _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-5).Date, DateTime.Now.Date, "a relevant directory where a user would be entitled to make thoroughly clear what is",
                            _systemUserId, DateTime.Now.Date, rttReferral, rttPathwaySetupId, _rttTreatmentStatus_36_Id);

            var _caseNumber2 = (string)dbHelper.Case.GetCaseByID(_caseId2, "casenumber")["casenumber"];

            #endregion

            #region Health Appointment

            TimeSpan startTime = new TimeSpan(10, 0, 0);
            TimeSpan endTime = new TimeSpan(10, 5, 0);

            // Health Appointment Record 1 for Case 1
            var _healthAppointmentID = dbHelper.healthAppointment.CreateHealthAppointment1(
                                    _teamId, _personID, _dataformId_Appointments, _communityClinicAppointmentContactTypesId, _healthAppointmentReasonId, _caseId,
                                    DateTime.Now.Date, startTime, endTime, DateTime.Now.Date, _systemUserId, _communityAndClinicTeamId, _communityClinicLocationTypesId, _systemUserId);

            // Health Appointment Record 2 for Case 2
            var _healthAppointmentID2 = dbHelper.healthAppointment.CreateHealthAppointment1(
                                    _teamId, _personID, _dataformId_Appointments, _communityClinicAppointmentContactTypesId, _healthAppointmentReasonId, _caseId2,
                                    DateTime.Now.AddDays(-2).Date, startTime, endTime, DateTime.Now.AddDays(-2).Date, _systemUserId, _communityAndClinicTeamId, _communityClinicLocationTypesId, _systemUserId);

            #endregion

            #region Step 12 & 15

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber.ToString(), _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber.ToString());

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToHealthAppointmentsPage();

            var rttWaitTimeRecords = dbHelper.rttWaitTime.GetByRelatedCase(_caseId);
            Assert.AreEqual(1, rttWaitTimeRecords.Count);
            var rttWaitTimeRecordId = rttWaitTimeRecords.First();

            var rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(1, rttEventsRecords.Count);

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .OpenCaseHealthAppointmentRecord(_healthAppointmentID.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickOutcomeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_10_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_11_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_12_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_20_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_21_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_30_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_31_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_32_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_33_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_34_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_35_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_36_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_90_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_91_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_92_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_98_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_99_Id.ToString())
                .ClickCloseButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .TapBackButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickNewRecordButton();

            // Step 15 for Unscheduled Appointment
            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Unscheduled Appointment")
                .TapNextButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickOutcomeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_10_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_11_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_12_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_20_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_21_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_30_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_31_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_32_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_33_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_34_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_35_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_36_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_90_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_91_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_92_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_98_Id.ToString())
                .ValidateResultElementNotPresent(_communityClinicAppointmentOutcomeType_99_Id.ToString())
                .ClickCloseButton();

            #endregion

            #region Step 11 & 15

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber2.ToString(), _caseId2.ToString())
                .OpenCaseRecord(_caseId2.ToString(), _caseNumber2.ToString());

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToHealthAppointmentsPage();

            var rttWaitTimeRecords2 = dbHelper.rttWaitTime.GetByRelatedCase(_caseId2);
            Assert.AreEqual(1, rttWaitTimeRecords2.Count);
            var rttWaitTimeRecordId2 = rttWaitTimeRecords2.First();

            var rttEventsRecords2 = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId2);
            Assert.AreEqual(1, rttEventsRecords2.Count);

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickNewRecordButton();

            // Step 15 for Unscheduled Appointment
            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Unscheduled Appointment")
                .TapNextButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickOutcomeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_10_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_11_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_12_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_20_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_21_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_30_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_31_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_32_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_33_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_34_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_35_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_36_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_90_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_91_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_92_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_98_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_99_Id.ToString())
                .ClickCloseButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .TapBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .OpenCaseHealthAppointmentRecord(_healthAppointmentID2.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickOutcomeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_10_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_11_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_12_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_20_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_21_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_30_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_31_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_32_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_33_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_34_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_35_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_36_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_90_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_91_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_92_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_98_Id.ToString())
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_99_Id.ToString())

                .SelectResultElement(_communityClinicAppointmentOutcomeType_10_Id);

            #endregion

            #region Step 13

            caseHealthAppointmentRecordPage
                .TapSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateOutcomeLookupButtonDisabled(true);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-2365")]
        [Description("Step(s) 14 for 'Appointments' for the test CDV6-22682")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_HealthAppointmentOutcomes_RTTTreatment_UITestMethod06()
        {
            string rttTreatmentStatus_34_Name = (string)dbHelper.rttTreatmentStatus.GetByID(_rttTreatmentStatus_34_Id, "name")["name"];

            #region Community Case record

            var rttReferral = 1;

            var _caseId = dbHelper.Case.CreateCommunityHealthCaseRecord(_teamId, _personID, _systemUserId, _communityAndClinicTeamId, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                            _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, new DateTime(2023, 6, 9), new DateTime(2023, 6, 9), new DateTime(2023, 6, 9), DateTime.Now.Date, "a relevant directory where a user would be entitled to make thoroughly clear what is",
                            _systemUserId, new DateTime(2023, 6, 9), rttReferral, rttPathwaySetupId, _rttTreatmentStatus_36_Id);

            var _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];

            #endregion

            #region Health Appointment

            TimeSpan startTime = new TimeSpan(10, 0, 0);
            TimeSpan endTime = new TimeSpan(10, 5, 0);

            var _healthAppointmentID = dbHelper.healthAppointment.CreateHealthAppointment1(
                                    _teamId, _personID, _dataformId_Appointments, _communityClinicAppointmentContactTypesId, _healthAppointmentReasonId, _caseId,
                                    new DateTime(2023, 6, 9), startTime, endTime, new DateTime(2023, 6, 9), _systemUserId, _communityAndClinicTeamId, _communityClinicLocationTypesId, _systemUserId);

            #endregion

            #region Step 14

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber.ToString(), _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber.ToString());

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToHealthAppointmentsPage();

            var rttWaitTimeRecords = dbHelper.rttWaitTime.GetByRelatedCase(_caseId);
            Assert.AreEqual(1, rttWaitTimeRecords.Count);
            var rttWaitTimeRecordId = rttWaitTimeRecords.First();

            var rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(1, rttEventsRecords.Count);
            var rttEventRecordId = rttEventsRecords.First();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .OpenCaseHealthAppointmentRecord(_healthAppointmentID.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickOutcomeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_10_Id.ToString())
                .SelectResultElement(_communityClinicAppointmentOutcomeType_10_Id);

            caseHealthAppointmentRecordPage
                .TapSaveAndCloseButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsMenuSectionToLoad()
                .NavigateToRTTWaitTimePage();


            rttWaitTimesPage
                .WaitForRTTWaitTimesPageToLoad();

            var rttEventsRecords2 = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(2, rttEventsRecords2.Count);
            var rttEventRecordId2 = rttEventsRecords2.First();

            rttWaitTimesPage
                .OpenRecord(rttWaitTimeRecordId);

            rttWaitTimeRecordPage
                .WaitForRTTWaitTimeRecordPageToLoad()
                .WaitForRTTEventsSectionToLoad()
                .ValidateRTTEventRecordPresent(rttEventRecordId.ToString())
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 2, new DateTime(2023, 6, 9).ToString("dd'/'MM'/'yyyy"))
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 3, "Patient Died")
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 4, "36")
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 5, "0")
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 6, "0")

                .ValidateRTTEventRecordPresent(rttEventRecordId2.ToString())
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 2, new DateTime(2023, 6, 9).ToString("dd'/'MM'/'yyyy"))
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 3, "First Activity in new RTT period")
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 4, "10")
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 5, "0")
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 6, "0");

            rttWaitTimeRecordPage
                .WaitForRTTWaitTimeRecordPageToLoad()
                .ClickBackButton();

            rttWaitTimesPage
                .WaitForRTTWaitTimesPageToLoad();

            var _healthAppointmentID2 = dbHelper.healthAppointment.CreateHealthAppointment1(
                                    _teamId, _personID, _dataformId_Appointments, _communityClinicAppointmentContactTypesId, _healthAppointmentReasonId, _caseId,
                                    new DateTime(2023, 6, 12), startTime, endTime, new DateTime(2023, 6, 12), _systemUserId, _communityAndClinicTeamId, _communityClinicLocationTypesId, _systemUserId);

            dbHelper.healthAppointment.UpdateHealthAppointmentOutcomeType(_healthAppointmentID2, _communityClinicAppointmentOutcomeType_34_Id);

            rttWaitTimesPage
                .OpenRecord(rttWaitTimeRecordId);

            var rttEventsRecords3 = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(3, rttEventsRecords3.Count);
            var rttEventRecordId3 = rttEventsRecords3.First();

            rttWaitTimeRecordPage
                .WaitForRTTWaitTimeRecordPageToLoad()
                .WaitForRTTEventsSectionToLoad()
                .ValidateRTTEventRecordPresent(rttEventRecordId3.ToString())
                .ValidateRTTEventRecordCellText(rttEventRecordId3.ToString(), 2, new DateTime(2023, 6, 12).ToString("dd'/'MM'/'yyyy"))
                .ValidateRTTEventRecordCellText(rttEventRecordId3.ToString(), 3, rttTreatmentStatus_34_Name)
                .ValidateRTTEventRecordCellText(rttEventRecordId3.ToString(), 4, "34");

            rttWaitTimeRecordPage
                .WaitForRTTWaitTimeRecordPageToLoad()
                .ClickBackButton();

            rttWaitTimesPage
                .WaitForRTTWaitTimesMenusSectionToLoad()
                .NavigateToHealthAppointmentsContactsAndOffersPage();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .SelectViewResultDropDown("Related Child Records")
                .OpenCaseHealthAppointmentRecord(_healthAppointmentID2.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPage()
                .ValidateOutcomeLookupButtonDisabled(true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2383")]
        [Description("Step(s) 13 & 14 for 'Unscheduled Appointment' for the test CDV6-22682")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_HealthAppointmentOutcomes_RTTTreatment_UITestMethod07()
        {
            string rttTreatmentStatus_34_Name = (string)dbHelper.rttTreatmentStatus.GetByID(_rttTreatmentStatus_34_Id, "name")["name"];

            #region Community Case record

            var rttReferral = 1;

            var _caseId = dbHelper.Case.CreateCommunityHealthCaseRecord(_teamId, _personID, _systemUserId, _communityAndClinicTeamId, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                            _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, new DateTime(2023, 6, 9), new DateTime(2023, 6, 9), new DateTime(2023, 6, 9), DateTime.Now.Date, "a relevant directory where a user would be entitled to make thoroughly clear what is",
                            _systemUserId, new DateTime(2023, 6, 9), rttReferral, rttPathwaySetupId, _rttTreatmentStatus_36_Id);

            var _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];

            #endregion

            #region Unscheduled Health Appointment

            var _dataformId_UnscheduledAppointments = dbHelper.dataForm.GetByName("Unscheduled Appointment").FirstOrDefault();

            var _unscheduledAppointment1 = dbHelper.healthAppointment.CreateHealthAppointmentForUnscheduled(_teamId, _personID, _dataformId_UnscheduledAppointments, _communityClinicAppointmentContactTypesId, _healthAppointmentReasonId, _caseId,
                                           new DateTime(2023, 6, 9), new TimeSpan(0, 5, 0), new DateTime(2023, 6, 9), new TimeSpan(1, 30, 0), _systemUserId, _communityAndClinicTeamId,
                                           _communityClinicLocationTypesId, 8, _systemUserId, "Unscheduled Appointment 1", false);

            #endregion

            #region Step 13 & 14

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber.ToString(), _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber.ToString());

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToHealthAppointmentsPage();

            var rttWaitTimeRecords = dbHelper.rttWaitTime.GetByRelatedCase(_caseId);
            Assert.AreEqual(1, rttWaitTimeRecords.Count);
            var rttWaitTimeRecordId = rttWaitTimeRecords.First();

            var rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(1, rttEventsRecords.Count);
            var rttEventRecordId = rttEventsRecords.First();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .OpenCaseHealthAppointmentRecord(_unscheduledAppointment1.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickOutcomeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(_communityClinicAppointmentOutcomeType_10_Id.ToString())
                .SelectResultElement(_communityClinicAppointmentOutcomeType_10_Id);

            caseHealthAppointmentRecordPage
                .TapSaveButton()
                .WaitForRecordToBeSaved()
                .TapBackButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsMenuSectionToLoad()
                .NavigateToRTTWaitTimePage();

            rttWaitTimesPage
                .WaitForRTTWaitTimesPageToLoad();

            var rttEventsRecords2 = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(2, rttEventsRecords2.Count);
            var rttEventRecordId2 = rttEventsRecords2.First();

            rttWaitTimesPage
                .OpenRecord(rttWaitTimeRecordId);

            rttWaitTimeRecordPage
                .WaitForRTTWaitTimeRecordPageToLoad()
                .WaitForRTTEventsSectionToLoad()
                .ValidateRTTEventRecordPresent(rttEventRecordId.ToString())
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 2, new DateTime(2023, 6, 9).ToString("dd'/'MM'/'yyyy"))
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 3, "Patient Died")
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 4, "36")
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 5, "0")
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 6, "0")

                .ValidateRTTEventRecordPresent(rttEventRecordId2.ToString())
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 2, new DateTime(2023, 6, 9).ToString("dd'/'MM'/'yyyy"))
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 3, "First Activity in new RTT period")
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 4, "10")
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 5, "0")
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 6, "0");

            rttWaitTimeRecordPage
                .WaitForRTTWaitTimeRecordPageToLoad()
                .ClickBackButton();

            rttWaitTimesPage
                .WaitForRTTWaitTimesPageToLoad();

            // Unscheduled Appointment 2
            var _unscheduledAppointment2 = dbHelper.healthAppointment.CreateHealthAppointmentForUnscheduled(_teamId, _personID, _dataformId_UnscheduledAppointments, _communityClinicAppointmentContactTypesId, _healthAppointmentReasonId, _caseId,
                                           new DateTime(2023, 6, 12), new TimeSpan(0, 5, 0), new DateTime(2023, 6, 12), new TimeSpan(1, 30, 0), _systemUserId, _communityAndClinicTeamId,
                                           _communityClinicLocationTypesId, 8, _systemUserId, "Unscheduled Appointment 2", false);

            dbHelper.healthAppointment.UpdateHealthAppointmentOutcomeType(_unscheduledAppointment2, _communityClinicAppointmentOutcomeType_34_Id);

            rttWaitTimesPage
                .OpenRecord(rttWaitTimeRecordId);

            var rttEventsRecords3 = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(3, rttEventsRecords3.Count);
            var rttEventRecordId3 = rttEventsRecords3.First();

            rttWaitTimeRecordPage
                .WaitForRTTWaitTimeRecordPageToLoad()
                .WaitForRTTEventsSectionToLoad()
                .ValidateRTTEventRecordPresent(rttEventRecordId3.ToString())
                .ValidateRTTEventRecordCellText(rttEventRecordId3.ToString(), 2, new DateTime(2023, 6, 12).ToString("dd'/'MM'/'yyyy"))
                .ValidateRTTEventRecordCellText(rttEventRecordId3.ToString(), 3, rttTreatmentStatus_34_Name)
                .ValidateRTTEventRecordCellText(rttEventRecordId3.ToString(), 4, "34");

            rttWaitTimeRecordPage
                .WaitForRTTWaitTimeRecordPageToLoad()
                .ClickBackButton();

            rttWaitTimesPage
                .WaitForRTTWaitTimesMenusSectionToLoad()
                .NavigateToHealthAppointmentsContactsAndOffersPage();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .SelectViewResultDropDown("Related Child Records")
                .OpenCaseHealthAppointmentRecord(_unscheduledAppointment2.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPage()
                .ValidateOutcomeLookupButtonDisabled(true);

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

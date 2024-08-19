using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Cases
{
    [DeploymentItem("Files\\RTT Event - Case Discharged.Zip"), DeploymentItem("Files\\RTT Event - Consultant Episode RTT Treatment Status.Zip"), DeploymentItem("Files\\RTT Event - Health Appointment Outcome.Zip"), DeploymentItem("Files\\RTT Event - New RTT Case.Zip"), DeploymentItem("chromedriver.exe")]
    [TestClass]
    public class Case_RTT_Treatment_UITestCases : FunctionalTest
    {
        private string _environmentName;
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private string _systemUserFullName;
        private Guid _systemUserId2;
        private string _systemUsername2;
        private Guid _systemUserId3;
        private string _systemUsername;
        private Guid _jobRoleTypeId;
        private DateTime _currentDate;
        private Guid _personID;
        private string _personFullName;
        private Guid _caseId;
        private string _caseNumber;
        private Guid _admission_CaseStatusId;
        private Guid _awaitingFurtherInformation_caseStatusId;
        private Guid _contactReasonId_DefaultInpatient;
        private Guid _contactReasonId_Community;
        private Guid _dataFormId_InpatientCase;
        private Guid _dataFormId_CommunityHealthCase;
        private Guid _contactSourceId;
        private string _contactAdministrativeCategoryName;
        private Guid _contactAdministrativeCategoryId;
        private Guid _caseServiceTypeRequestedid;
        private string _caseServiceTypeRequestedName;
        private string _contactSourceName;
        private Guid _inpatientWardId;
        private Guid _inpatientBayId;
        private Guid _inpatientBedId;
        private Guid _inpatientAdmissionSourceId;
        private Guid _inpationAdmissionMethodId;
        private Guid _provider_HospitalId;
        private string _provider_Name;
        private Guid _rttTreatmentStatusId;
        private Guid _rttPathwaySetupId;
        private string _rttPathwaySetupName;
        private Guid _recurrencePatternId;
        private Guid _communityAndClinicTeamId;
        private Guid _healthAppointmentId;
        private Guid rttTreatmentFunctionCodeId;
        private string rttTreatmentFunctionCodeName;
        private Guid _providerId_Carer;
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

        #region Case Closure Reason

        private Guid _rttCaseClosureReason_10_Id;
        private Guid _rttCaseClosureReason_11_Id;
        private Guid _rttCaseClosureReason_12_Id;
        private Guid _rttCaseClosureReason_20_Id;
        private Guid _rttCaseClosureReason_21_Id;
        private Guid _rttCaseClosureReason_30_Id;
        private Guid _rttCaseClosureReason_31_Id;
        private Guid _rttCaseClosureReason_32_Id;
        private Guid _rttCaseClosureReason_33_Id;
        private Guid _rttCaseClosureReason_34_Id;
        private Guid _rttCaseClosureReason_35_Id;
        private Guid _rttCaseClosureReason_36_Id;
        private Guid _rttCaseClosureReason_90_Id;
        private Guid _rttCaseClosureReason_91_Id;
        private Guid _rttCaseClosureReason_92_Id;
        private Guid _rttCaseClosureReason_98_Id;
        private Guid _rttCaseClosureReason_99_Id;
        private Guid _rttCaseClosureReason_WithoutCode_Id;

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

                _systemUsername = "RTTTreatmentUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "RTTTreatment", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
                _systemUserFullName = "RTTTreatment User1";

                #endregion

                #region Consultant User (Sytem User)

                _systemUsername2 = "ConsultantUser2" + _currentDateString;
                _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_systemUsername2, "Consultant", "User2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

                #region Job Role Type

                _jobRoleTypeId = commonMethodsDB.CreateJobRoleType(_teamId, "Default Consultant", new DateTime(2020, 1, 1), 1);
                dbHelper.jobRoleType.UpdateIsConsultantId(_jobRoleTypeId, true);

                #endregion

                #region Update System User Job Role Type

                dbHelper.systemUser.UpdateJobRoleType(_systemUserId, _jobRoleTypeId);
                dbHelper.systemUser.UpdateJobRoleType(_systemUserId2, _jobRoleTypeId);

                #endregion

                #region Person

                var personFirstName = "RTT_Event";
                var personLastName = DateTime.Now.ToString("yyyyMMddHHmmss");
                _personFullName = "RTT_Event " + personLastName;
                _personID = commonMethodsDB.CreatePersonRecord(personFirstName, personLastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));

                #endregion

                #region Case Status

                _admission_CaseStatusId = dbHelper.caseStatus.GetByName("Admission").FirstOrDefault();
                _awaitingFurtherInformation_caseStatusId = dbHelper.caseStatus.GetByName("Awaiting Further Information").First();

                #endregion

                #region Contact Reason

                _contactReasonId_DefaultInpatient = commonMethodsDB.CreateContactReason(_teamId, "Default Inpatient Management Contact Reason", new DateTime(2020, 1, 1), 140000001, 2, false);
                _contactReasonId_Community = commonMethodsDB.CreateContactReasonIfNeeded("Test Contact Reason (Comm)", _teamId, 140000000, false);

                #endregion

                #region Contact Source

                _contactSourceName = "Default Contact Source";
                _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded(_contactSourceName, _teamId);

                #endregion

                #region Data Form

                _dataFormId_InpatientCase = dbHelper.dataForm.GetByName("InpatientCase")[0];
                _dataFormId_CommunityHealthCase = dbHelper.dataForm.GetByName("CommunityHealthCase").FirstOrDefault();

                #endregion

                #region Hospital Ward Specialty

                var _wardSpecialityId = dbHelper.inpatientWardSpecialty.GetByName("Adult Acute")[0];

                #endregion

                #region Inpatient Bed Type

                var _inpatientBedTypeId = dbHelper.inpatientBedType.GetByName("Clinitron")[0];

                #endregion

                #region Rtt Treatment Function

                rttTreatmentFunctionCodeName = "General Surgery Service";
                rttTreatmentFunctionCodeId = dbHelper.rttTreatmentFunctionCode.GetByName(rttTreatmentFunctionCodeName)[0];

                #endregion

                #region Provider

                _provider_Name = "RTT_" + _currentDateString;
                _provider_HospitalId = commonMethodsDB.CreateProvider(_provider_Name, _teamId);
                dbHelper.provider.UpdateRTTTreatmentFunction(_provider_HospitalId, rttTreatmentFunctionCodeId);

                #endregion

                #region Ward

                var _inpatientWardName = "Ward_" + _currentDateString;
                _inpatientWardId = dbHelper.inpatientWard.CreateInpatientWard(_teamId, _provider_HospitalId, _systemUserId, _wardSpecialityId, _inpatientWardName, new DateTime(2020, 1, 1));

                #endregion

                #region Bay/Room

                var _inpatientBayName = "Bay_" + _currentDateString;
                _inpatientBayId = dbHelper.inpatientBay.CreateInpatientCaseBay(_teamId, _inpatientWardId, _inpatientBayName, 1, "4", "4", "4", 2);

                #endregion

                #region Bed

                _inpatientBedId = dbHelper.inpatientBed.CreateInpatientBed(_teamId, "12665", "4", "4", _inpatientBayId, 1, _inpatientBedTypeId, "4");

                #endregion

                #region Contact Inpatient Admission Source

                _inpatientAdmissionSourceId = commonMethodsDB.CreateInpatientAdmissionSource(_teamId, "Default Inpatient Admission Source", new DateTime(2020, 1, 1));

                #endregion

                #region Inpatient Admission Method

                _inpationAdmissionMethodId = commonMethodsDB.CreateInpatientAdmissionMethod("Default Inpatient Admission Method", _teamId, _businessUnitId, new DateTime(2020, 1, 1));

                #endregion

                #region Contact Administrative Category

                _contactAdministrativeCategoryName = "Test_Administrative Category";
                _contactAdministrativeCategoryId = commonMethodsDB.CreateContactAdministrativeCategory(_teamId, _contactAdministrativeCategoryName, new DateTime(2020, 1, 1));

                #endregion

                #region Case Service Type Requested

                _caseServiceTypeRequestedName = "Advice and Consultation";
                _caseServiceTypeRequestedid = commonMethodsDB.CreateCaseServiceTypeRequested(_teamId, _caseServiceTypeRequestedName, new DateTime(2020, 1, 1));

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

                _rttTreatmentStatusId = dbHelper.rttTreatmentStatus.GetByName("First Activity in new RTT period")[0];

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

                _rttPathwaySetupName = "Default RTT Pathway";
                _rttPathwaySetupId = commonMethodsDB.CreateRTTPathwaySetup(_teamId, _rttPathwaySetupName, new DateTime(2020, 1, 1), 2, 5, 10);

                #endregion

                #region Consultant Episode End Reason

                commonMethodsDB.CreateInpatientConsultantEpisodeEndReason(_teamId, "Default End Reason", new DateTime(2020, 1, 1), 21, false, true, false);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        public void CreateCaseClosureReasons()
        {
            _rttCaseClosureReason_10_Id = commonMethodsDB.CreateCaseClosureReason(_teamId, "RTT_CCR_Inpatient_10", "", "10", new DateTime(2021, 1, 1), 140000001, false, _rttTreatmentStatus_10_Id);
            _rttCaseClosureReason_11_Id = commonMethodsDB.CreateCaseClosureReason(_teamId, "RTT_CCR_Inpatient_11", "", "11", new DateTime(2021, 1, 1), 140000001, false, _rttTreatmentStatus_11_Id);
            _rttCaseClosureReason_12_Id = commonMethodsDB.CreateCaseClosureReason(_teamId, "RTT_CCR_Inpatient_12", "", "12", new DateTime(2021, 1, 1), 140000001, false, _rttTreatmentStatus_12_Id);
            _rttCaseClosureReason_20_Id = commonMethodsDB.CreateCaseClosureReason(_teamId, "RTT_CCR_Inpatient_20", "", "20", new DateTime(2021, 1, 1), 140000001, false, _rttTreatmentStatus_20_Id);
            _rttCaseClosureReason_21_Id = commonMethodsDB.CreateCaseClosureReason(_teamId, "RTT_CCR_Inpatient_21", "", "21", new DateTime(2021, 1, 1), 140000001, false, _rttTreatmentStatus_21_Id);
            _rttCaseClosureReason_30_Id = commonMethodsDB.CreateCaseClosureReason(_teamId, "RTT_CCR_Inpatient_30", "", "30", new DateTime(2021, 1, 1), 140000001, false, _rttTreatmentStatus_30_Id);

            _rttCaseClosureReason_31_Id = commonMethodsDB.CreateCaseClosureReason(_teamId, "RTT_CCR_Inpatient_31", "", "31", new DateTime(2021, 1, 1), 140000001, false, _rttTreatmentStatus_31_Id);
            _rttCaseClosureReason_32_Id = commonMethodsDB.CreateCaseClosureReason(_teamId, "RTT_CCR_Inpatient_32", "", "32", new DateTime(2021, 1, 1), 140000001, false, _rttTreatmentStatus_32_Id);
            _rttCaseClosureReason_33_Id = commonMethodsDB.CreateCaseClosureReason(_teamId, "RTT_CCR_Inpatient_33", "", "33", new DateTime(2021, 1, 1), 140000001, false, _rttTreatmentStatus_33_Id);
            _rttCaseClosureReason_34_Id = commonMethodsDB.CreateCaseClosureReason(_teamId, "RTT_CCR_Inpatient_34", "", "34", new DateTime(2021, 1, 1), 140000001, false, _rttTreatmentStatus_34_Id);

            _rttCaseClosureReason_35_Id = commonMethodsDB.CreateCaseClosureReason(_teamId, "RTT_CCR_Inpatient_35", "", "35", new DateTime(2021, 1, 1), 140000001, false, _rttTreatmentStatus_35_Id);
            _rttCaseClosureReason_36_Id = commonMethodsDB.CreateCaseClosureReason(_teamId, "RTT_CCR_Inpatient_36", "", "36", new DateTime(2021, 1, 1), 140000001, false, _rttTreatmentStatus_36_Id);
            _rttCaseClosureReason_90_Id = commonMethodsDB.CreateCaseClosureReason(_teamId, "RTT_CCR_Inpatient_90", "", "90", new DateTime(2021, 1, 1), 140000001, false, _rttTreatmentStatus_90_Id);
            _rttCaseClosureReason_91_Id = commonMethodsDB.CreateCaseClosureReason(_teamId, "RTT_CCR_Inpatient_91", "", "91", new DateTime(2021, 1, 1), 140000001, false, _rttTreatmentStatus_91_Id);
            _rttCaseClosureReason_92_Id = commonMethodsDB.CreateCaseClosureReason(_teamId, "RTT_CCR_Inpatient_92", "", "92", new DateTime(2021, 1, 1), 140000001, false, _rttTreatmentStatus_92_Id);
            _rttCaseClosureReason_98_Id = commonMethodsDB.CreateCaseClosureReason(_teamId, "RTT_CCR_Inpatient_98", "", "98", new DateTime(2021, 1, 1), 140000001, false, _rttTreatmentStatus_98_Id);
            _rttCaseClosureReason_99_Id = commonMethodsDB.CreateCaseClosureReason(_teamId, "RTT_CCR_Inpatient_99", "", "99", new DateTime(2021, 1, 1), 140000001, false, _rttTreatmentStatus_99_Id);
        }

        public void CreateConsultantUser_SytemUser3()
        {
            #region Consultant User (Sytem User)

            var _systemUsername3 = "ConsultantUser3" + _currentDateString;
            _systemUserId3 = commonMethodsDB.CreateSystemUserRecord(_systemUsername3, "Consultant", "User3", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            dbHelper.systemUser.UpdateJobRoleType(_systemUserId3, _jobRoleTypeId);

            #endregion
        }

        public void CreateInpatientCase()
        {
            #region Create Inpatient Case

            var rttReferral = 1; //Yes
            _currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            _caseId = dbHelper.Case.CreateInpatientCaseRecordWithStatusAsAdmission(
                        _teamId, _personID, _currentDate, _systemUserId, "presenting needs ...",
                        _systemUserId, _admission_CaseStatusId, _contactReasonId_DefaultInpatient, _currentDate, _dataFormId_InpatientCase,
                        _contactSourceId, _inpatientWardId, _inpatientBayId, _inpatientBedId, _inpatientAdmissionSourceId,
                        _inpationAdmissionMethodId, _systemUserId, _currentDate, _provider_HospitalId,
                        _inpatientWardId, 1, _currentDate,
                        false, false, false, false, false, false, false, false, false, false,
                        rttReferral, _rttTreatmentStatusId, _rttPathwaySetupId);

            _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];

            #endregion
        }

        public void CreateRequireDataAndCaseCommunityCase()
        {
            #region Provider (Carer)

            _providerId_Carer = commonMethodsDB.CreateProvider("CareDirector QA Provider Health Appointment", _teamId, 7);
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

            _recurrencePatternId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").FirstOrDefault();

            #endregion

            #region System User

            _systemUsername = "RTTTreatmentUser_" + _currentDateString;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "RTTTreatmentUser", _currentDateString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            _systemUserFullName = "RTTTreatmentUser " + _currentDateString;

            dbHelper.systemUser.UpdateJobRoleType(_systemUserId, _jobRoleTypeId);

            #endregion

            #region Create New User WorkSchedule01

            if (!dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserId).Any())
                dbHelper.userWorkSchedule.CreateUserWorkSchedule("Work Schedule", _systemUserId, _teamId, _recurrencePatternId, new DateTime(2023, 1, 1), null, new TimeSpan(0, 5, 0), new TimeSpan(23, 55, 0));

            #endregion

            #region Community Clinic Linked Professional

            dbHelper.communityClinicLinkedProfessional.CreateCommunityClinicLinkedProfessional(_teamId, _communityClinicDiaryViewSetupId, _systemUserId, new DateTime(2023, 2, 2), new TimeSpan(1, 5, 0), new TimeSpan(23, 55, 0), _recurrencePatternId, _systemUserFullName);

            #endregion
        }

        public void CreateHealthAppointment()
        {
            #region Health Appointment

            TimeSpan startTime = new TimeSpan(10, 0, 0);
            TimeSpan endTime = new TimeSpan(10, 5, 0);

            #region Data Form Community Health Case

            var _dataformId_Appointments = dbHelper.dataForm.GetByName("Appointments").FirstOrDefault();

            #endregion

            #region Community/Clinic Appointment Contact Types

            var _communityClinicAppointmentContactTypesId = commonMethodsDB.CreateHealthAppointmentContactType(_teamId, "Community_Clinic Appointment Contact Types_Appointment", new DateTime(2020, 1, 1), "3");

            #endregion

            #region Health Appointment Reason

            var _healthAppointmentReasonId = commonMethodsDB.CreateHealthAppointmentReason(_teamId, "Follow Up Appointment", new DateTime(2020, 1, 1), "3", null);

            #endregion

            #region Community/Clinic Appointment Location Types

            var _communityClinicLocationTypesId = commonMethodsDB.CreateHealthAppointmentLocationType(_teamId, "Health Clinic managed by Voluntary or Private Agents", new DateTime(2020, 1, 1));

            #endregion

            _healthAppointmentId = dbHelper.healthAppointment.CreateHealthAppointment1(
                                    _teamId, _personID, _dataformId_Appointments, _communityClinicAppointmentContactTypesId, _healthAppointmentReasonId, _caseId,
                                    new DateTime(2023, 6, 9), startTime, endTime, new DateTime(2023, 6, 9), _systemUserId, _communityAndClinicTeamId, _communityClinicLocationTypesId, _systemUserId);

            #endregion
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-1085

        [TestProperty("JiraIssueID", "ACC-1185")]
        [Description("Step(s) 1 to 3 for the test CDV6-23643")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_RTT_Treatment_UITestMethod01()
        {
            CreateInpatientCase();

            string rttTreatmentStatus_34_Name = (string)dbHelper.rttTreatmentStatus.GetByID(_rttTreatmentStatus_34_Id, "name")["name"];

            #region Step 1 & 2

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber.ToString(), _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber.ToString());

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToRTTWaitTimePage();

            rttWaitTimesPage
                .WaitForRTTWaitTimesPageToLoad();

            var rttWaitTimeRecords = dbHelper.rttWaitTime.GetByRelatedCase(_caseId);
            Assert.AreEqual(1, rttWaitTimeRecords.Count);
            var rttWaitTimeRecordId = rttWaitTimeRecords.First();

            var rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(1, rttEventsRecords.Count);
            var rttEventRecordId = rttEventsRecords.First();

            rttWaitTimesPage
                .OpenRecord(rttWaitTimeRecordId);

            rttWaitTimeRecordPage
                .WaitForRTTWaitTimeRecordPageToLoad()
                .WaitForRTTEventsSectionToLoad()
                .ValidateRTTEventRecordRowPosition(rttEventRecordId.ToString(), 1)
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 2, _currentDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 3, "First Activity in new RTT period")
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 4, "10")
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 5, "0")
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 6, "0");

            rttWaitTimeRecordPage
                .WaitForRTTWaitTimeRecordPageToLoad()
                .ClickBackButton();

            rttWaitTimesPage
                .WaitForRTTWaitTimesMenusSectionToLoad()
                .NavigateToConsultantEpisodesPage();

            consultantEpisodesPage
                .WaitForConsultantEpisodesPageToLoad()
                .ClickNewRecordButton();

            consultantEpisodesRecordPage
                .WaitForConsultantEpisodesRecordPageToLoad()
                .ClickRTTTreatmentStatusLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementNotPresent(_rttTreatmentStatus_10_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_11_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_12_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_20_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_21_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_30_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_31_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_32_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_33_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_34_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_35_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_36_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_90_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_91_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_92_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_98_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_99_Id.ToString())
                .ClickCloseButton();

            #endregion

            #region Step 3

            var InpatientConsultantEpisode2 = dbHelper.inpatientConsultantEpisode.CreateInpatientConsultantEpisode(_teamId, _caseId, _personID, _systemUserId2, null, _rttTreatmentStatus_34_Id, null, _currentDate, null);

            consultantEpisodesRecordPage
                .WaitForConsultantEpisodesRecordPageToLoad()
                .TapBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            consultantEpisodesPage
                .WaitForConsultantEpisodesPageToLoad()
                .ClickRefreshButton()
                .WaitForConsultantEpisodesPageToLoad()
                .ValidateConsultantEpisodeRecord(InpatientConsultantEpisode2.ToString())
                .ClickNewRecordButton();

            consultantEpisodesRecordPage
                .WaitForConsultantEpisodesRecordPageToLoad()
                .ClickRTTTreatmentStatusLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementNotPresent(_rttTreatmentStatus_10_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_11_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_12_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_20_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_21_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_30_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_31_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_32_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_33_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_34_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_35_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_36_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_90_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_91_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_92_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_98_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_99_Id.ToString())
                .ClickCloseButton();

            consultantEpisodesRecordPage
                .WaitForConsultantEpisodesRecordPageToLoad()
                .TapBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            consultantEpisodesPage
                .WaitForConsultantEpisodesMenuSectionsToLoad()
                .NavigateToRTTWaitTimePage();

            rttWaitTimesPage
                .WaitForRTTWaitTimesPageToLoad();

            rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(2, rttEventsRecords.Count);
            rttEventRecordId = rttEventsRecords.First();

            rttWaitTimesPage
                .OpenRecord(rttWaitTimeRecordId);

            rttWaitTimeRecordPage
                .WaitForRTTWaitTimeRecordPageToLoad()
                .WaitForRTTEventsSectionToLoad()
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 2, _currentDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 3, rttTreatmentStatus_34_Name.ToString())
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 4, "34")
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 5, "0")
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 6, "0");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-1192")]
        [Description("Step(s) 4 & 8 for the test CDV6-23643")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_RTT_Treatment_UITestMethod02()
        {
            CreateInpatientCase();

            #region RTT Treatment Status 

            string rttTreatmentStatus_30_Name = (string)dbHelper.rttTreatmentStatus.GetByID(_rttTreatmentStatus_30_Id, "name")["name"];
            string rttTreatmentStatus_98_Name = (string)dbHelper.rttTreatmentStatus.GetByID(_rttTreatmentStatus_98_Id, "name")["name"];

            var _rttTreatmentStatus_100_Name = "100_RTT Treatment Status";
            var _rttTreatmentStatus_100_Id = commonMethodsDB.CreateRTTTreatmentStatus(_teamId, _businessUnitId, _rttTreatmentStatus_100_Name, "100", 1, new DateTime(2023, 1, 1), _rttTreatmentStatus_100_Name);

            #endregion

            #region Step 4

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber.ToString(), _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber.ToString());

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToConsultantEpisodesPage();

            var rttWaitTimeRecords = dbHelper.rttWaitTime.GetByRelatedCase(_caseId);
            Assert.AreEqual(1, rttWaitTimeRecords.Count);
            var rttWaitTimeRecordId = rttWaitTimeRecords.First();

            var rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(1, rttEventsRecords.Count);

            consultantEpisodesPage
                .WaitForConsultantEpisodesPageToLoad();

            var InpatientConsultantEpisode2 = dbHelper.inpatientConsultantEpisode.CreateInpatientConsultantEpisode(_teamId, _caseId, _personID, _systemUserId2, null, _rttTreatmentStatus_30_Id, null, _currentDate, null);

            consultantEpisodesPage
                .ClickRefreshButton()
                .WaitForConsultantEpisodesPageToLoad()
                .ValidateConsultantEpisodeRecord(InpatientConsultantEpisode2.ToString())
                .ClickNewRecordButton();

            consultantEpisodesRecordPage
                .WaitForConsultantEpisodesRecordPageToLoad()
                .ClickRTTTreatmentStatusLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(_rttTreatmentStatus_10_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_11_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_12_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_20_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_21_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_30_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_31_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_32_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_33_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_34_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_35_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_36_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_90_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_91_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_92_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_98_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_99_Id.ToString())
                .ClickCloseButton();

            #endregion

            #region Step 8

            rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            var rttEventRecordId2 = rttEventsRecords.First();

            CreateConsultantUser_SytemUser3();

            var InpatientConsultantEpisode3 = dbHelper.inpatientConsultantEpisode.CreateInpatientConsultantEpisode(_teamId, _caseId, _personID, _systemUserId3, null, _rttTreatmentStatus_98_Id, null, _currentDate, null);

            consultantEpisodesRecordPage
                .WaitForConsultantEpisodesRecordPageToLoad()
                .TapBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            consultantEpisodesPage
                .WaitForConsultantEpisodesPageToLoad()
                .ClickRefreshButton()
                .WaitForConsultantEpisodesPageToLoad()
                .ValidateConsultantEpisodeRecord(InpatientConsultantEpisode3.ToString())
                .ClickNewRecordButton();

            consultantEpisodesRecordPage
                .WaitForConsultantEpisodesRecordPageToLoad()
                .ClickRTTTreatmentStatusLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(_rttTreatmentStatus_10_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_11_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_12_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_20_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_21_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_30_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_31_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_32_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_33_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_34_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_35_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_36_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_90_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_91_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_92_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_98_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_99_Id.ToString())
                .TypeSearchQuery(_rttTreatmentStatus_100_Name.ToString())
                .TapSearchButton()
                .ValidateResultElementPresent(_rttTreatmentStatus_100_Id.ToString())
                .ClickCloseButton();

            consultantEpisodesRecordPage
                .WaitForConsultantEpisodesRecordPageToLoad()
                .TapBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            consultantEpisodesPage
                .WaitForConsultantEpisodesMenuSectionsToLoad()
                .NavigateToRTTWaitTimePage();

            rttWaitTimesPage
                .WaitForRTTWaitTimesPageToLoad();

            rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(3, rttEventsRecords.Count);
            var rttEventRecordId3 = rttEventsRecords.First();

            rttWaitTimesPage
                .OpenRecord(rttWaitTimeRecordId);

            rttWaitTimeRecordPage
                .WaitForRTTWaitTimeRecordPageToLoad()
                .WaitForRTTEventsSectionToLoad()
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 2, _currentDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 3, rttTreatmentStatus_30_Name.ToString())
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 4, "30")
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 5, "0")
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 6, "0")

                .ValidateRTTEventRecordCellText(rttEventRecordId3.ToString(), 2, _currentDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRTTEventRecordCellText(rttEventRecordId3.ToString(), 3, rttTreatmentStatus_98_Name.ToString())
                .ValidateRTTEventRecordCellText(rttEventRecordId3.ToString(), 4, "98")
                .ValidateRTTEventRecordCellText(rttEventRecordId3.ToString(), 5, "0")
                .ValidateRTTEventRecordCellText(rttEventRecordId3.ToString(), 6, "0");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-1193")]
        [Description("Step(s) 5 & 7 for the test CDV6-23643")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_RTT_Treatment_UITestMethod03()
        {
            CreateInpatientCase();

            #region RTT Treatment Status

            string rttTreatmentStatus_32_Name = (string)dbHelper.rttTreatmentStatus.GetByID(_rttTreatmentStatus_32_Id, "name")["name"];
            string rttTreatmentStatus_36_Name = (string)dbHelper.rttTreatmentStatus.GetByID(_rttTreatmentStatus_36_Id, "name")["name"];

            var _rttTreatmentStatus_100_Name = "100_RTT Treatment Status";
            var _rttTreatmentStatus_100_Id = commonMethodsDB.CreateRTTTreatmentStatus(_teamId, _businessUnitId, _rttTreatmentStatus_100_Name, "100", 1, new DateTime(2023, 1, 1), _rttTreatmentStatus_100_Name);

            #endregion

            #region Step 5

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber.ToString(), _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber.ToString());

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToConsultantEpisodesPage();

            var rttWaitTimeRecords = dbHelper.rttWaitTime.GetByRelatedCase(_caseId);
            Assert.AreEqual(1, rttWaitTimeRecords.Count);
            var rttWaitTimeRecordId = rttWaitTimeRecords.First();

            var rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(1, rttEventsRecords.Count);

            consultantEpisodesPage
                .WaitForConsultantEpisodesPageToLoad();

            var InpatientConsultantEpisode2 = dbHelper.inpatientConsultantEpisode.CreateInpatientConsultantEpisode(_teamId, _caseId, _personID, _systemUserId2, null, _rttTreatmentStatus_32_Id, null, _currentDate, null);

            consultantEpisodesPage
                .ClickRefreshButton()
                .WaitForConsultantEpisodesPageToLoad()
                .ValidateConsultantEpisodeRecord(InpatientConsultantEpisode2.ToString())
                .ClickNewRecordButton();

            consultantEpisodesRecordPage
                .WaitForConsultantEpisodesRecordPageToLoad()
                .ClickRTTTreatmentStatusLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementNotPresent(_rttTreatmentStatus_10_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_11_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_12_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_20_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_21_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_30_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_31_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_32_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_33_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_34_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_35_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_36_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_90_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_91_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_92_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_98_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_99_Id.ToString())
                .ClickCloseButton();

            #endregion

            #region Step 7

            rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            var rttEventRecordId2 = rttEventsRecords.First();

            CreateConsultantUser_SytemUser3();

            var InpatientConsultantEpisode3 = dbHelper.inpatientConsultantEpisode.CreateInpatientConsultantEpisode(_teamId, _caseId, _personID, _systemUserId3, null, _rttTreatmentStatus_36_Id, null, _currentDate, null);

            consultantEpisodesRecordPage
                .WaitForConsultantEpisodesRecordPageToLoad()
                .TapBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            consultantEpisodesPage
                .WaitForConsultantEpisodesPageToLoad()
                .ClickRefreshButton()
                .WaitForConsultantEpisodesPageToLoad()
                .ValidateConsultantEpisodeRecord(InpatientConsultantEpisode3.ToString())
                .ClickNewRecordButton();

            consultantEpisodesRecordPage
                .WaitForConsultantEpisodesRecordPageToLoad()
                .ClickRTTTreatmentStatusLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(_rttTreatmentStatus_10_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_11_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_12_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_20_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_21_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_30_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_31_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_32_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_33_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_34_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_35_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_36_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_90_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_91_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_92_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_98_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_99_Id.ToString())
                .TypeSearchQuery(_rttTreatmentStatus_100_Name.ToString())
                .TapSearchButton()
                .ValidateResultElementPresent(_rttTreatmentStatus_100_Id.ToString())
                .ClickCloseButton();

            consultantEpisodesRecordPage
                .WaitForConsultantEpisodesRecordPageToLoad()
                .TapBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            consultantEpisodesPage
                .WaitForConsultantEpisodesMenuSectionsToLoad()
                .NavigateToRTTWaitTimePage();

            rttWaitTimesPage
                .WaitForRTTWaitTimesPageToLoad();

            rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(3, rttEventsRecords.Count);
            var rttEventRecordId3 = rttEventsRecords.First();

            rttWaitTimesPage
                .OpenRecord(rttWaitTimeRecordId);

            rttWaitTimeRecordPage
                .WaitForRTTWaitTimeRecordPageToLoad()
                .WaitForRTTEventsSectionToLoad()
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 2, _currentDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 3, rttTreatmentStatus_32_Name.ToString())
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 4, "32")
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 5, "0")
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 6, "0")

                .ValidateRTTEventRecordCellText(rttEventRecordId3.ToString(), 2, _currentDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRTTEventRecordCellText(rttEventRecordId3.ToString(), 3, rttTreatmentStatus_36_Name.ToString())
                .ValidateRTTEventRecordCellText(rttEventRecordId3.ToString(), 4, "36")
                .ValidateRTTEventRecordCellText(rttEventRecordId3.ToString(), 5, "0")
                .ValidateRTTEventRecordCellText(rttEventRecordId3.ToString(), 6, "0");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-1194")]
        [Description("Step(s) 6 for the test CDV6-23643")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_RTT_Treatment_UITestMethod04()
        {
            CreateInpatientCase();

            string rttTreatmentStatus_90_Name = (string)dbHelper.rttTreatmentStatus.GetByID(_rttTreatmentStatus_90_Id, "name")["name"];

            #region Step 6

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber.ToString(), _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber.ToString());

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToConsultantEpisodesPage();

            var rttWaitTimeRecords = dbHelper.rttWaitTime.GetByRelatedCase(_caseId);
            Assert.AreEqual(1, rttWaitTimeRecords.Count);
            var rttWaitTimeRecordId = rttWaitTimeRecords.First();

            var rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(1, rttEventsRecords.Count);

            consultantEpisodesPage
                .WaitForConsultantEpisodesPageToLoad();

            var InpatientConsultantEpisode2 = dbHelper.inpatientConsultantEpisode.CreateInpatientConsultantEpisode(_teamId, _caseId, _personID, _systemUserId2, null, _rttTreatmentStatus_90_Id, null, _currentDate, null);

            consultantEpisodesPage
                .ClickRefreshButton()
                .WaitForConsultantEpisodesPageToLoad()
                .ValidateConsultantEpisodeRecord(InpatientConsultantEpisode2.ToString())
                .ClickNewRecordButton();

            consultantEpisodesRecordPage
                .WaitForConsultantEpisodesRecordPageToLoad()
                .ClickRTTTreatmentStatusLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(_rttTreatmentStatus_10_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_11_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_12_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_20_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_21_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_30_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_31_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_32_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_33_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_34_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_35_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_36_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_90_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_91_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_92_Id.ToString())
                .ValidateResultElementNotPresent(_rttTreatmentStatus_98_Id.ToString())
                .ValidateResultElementPresent(_rttTreatmentStatus_99_Id.ToString())
                .ClickCloseButton();

            consultantEpisodesRecordPage
                .WaitForConsultantEpisodesRecordPageToLoad()
                .TapBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            consultantEpisodesPage
                .WaitForConsultantEpisodesMenuSectionsToLoad()
                .NavigateToRTTWaitTimePage();

            rttWaitTimesPage
                .WaitForRTTWaitTimesPageToLoad();

            rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(2, rttEventsRecords.Count);
            var rttEventRecordId2 = rttEventsRecords.First();

            rttWaitTimesPage
                .OpenRecord(rttWaitTimeRecordId);

            rttWaitTimeRecordPage
                .WaitForRTTWaitTimeRecordPageToLoad()
                .WaitForRTTEventsSectionToLoad()
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 2, _currentDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 3, rttTreatmentStatus_90_Name.ToString())
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 4, "90")
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 5, "0")
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 6, "0");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-2062

        [TestProperty("JiraIssueID", "ACC-2084")]
        [Description("Step(s) 1 to 3 & 9 for the test CDV6-23533")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_RTT_Treatment_UITestMethod05()
        {
            CreateInpatientCase();
            CreateCaseClosureReasons();

            #region Step 1 & 2

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
                .SelectInpatientStatus("Discharge Awaiting Closure");

            var rttWaitTimeRecords = dbHelper.rttWaitTime.GetByRelatedCase(_caseId);
            Assert.AreEqual(1, rttWaitTimeRecords.Count);
            var rttWaitTimeRecordId = rttWaitTimeRecords.First();

            var rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(1, rttEventsRecords.Count);

            caseRecordPage
                .ClickDischargeMethodLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementNotPresent(_rttCaseClosureReason_10_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_11_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_12_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_20_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_21_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_30_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_31_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_32_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_33_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_34_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_35_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_36_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_90_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_91_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_92_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_98_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_99_Id.ToString())
                .ClickCloseButton();

            caseRecordPage
                .SwitchToDefaultFrameAndWaitForCaseRecordPageToLoad()
                .SelectInpatientStatus("Discharge");

            caseRecordPage
                .ClickDischargeMethodLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementNotPresent(_rttCaseClosureReason_10_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_11_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_12_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_20_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_21_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_30_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_31_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_32_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_33_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_34_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_35_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_36_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_90_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_91_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_92_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_98_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_99_Id.ToString())
                .ClickCloseButton();

            #endregion

            #region Step 3  & 9

            dbHelper.inpatientConsultantEpisode.CreateInpatientConsultantEpisode(_teamId, _caseId, _personID, _systemUserId2, null, _rttTreatmentStatus_34_Id, null, _currentDate, null);

            caseRecordPage
                .SwitchToDefaultFrameAndWaitForCaseRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber.ToString(), _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber.ToString());

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .SelectInpatientStatus("Discharge Awaiting Closure");

            rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(2, rttEventsRecords.Count);

            caseRecordPage
                .ClickDischargeMethodLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementNotPresent(_rttCaseClosureReason_10_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_11_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_12_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_20_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_21_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_30_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_31_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_32_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_33_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_34_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_35_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_36_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_90_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_91_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_92_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_98_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_99_Id.ToString())
                .ClickCloseButton();


            caseRecordPage
                .SwitchToDefaultFrameAndWaitForCaseRecordPageToLoad()
                .SelectInpatientStatus("Discharge");

            caseRecordPage
                .ClickDischargeMethodLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementNotPresent(_rttCaseClosureReason_10_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_11_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_12_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_20_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_21_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_30_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_31_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_32_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_33_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_34_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_35_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_36_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_90_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_91_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_92_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_98_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_99_Id.ToString())
                .ClickCloseButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2085")]
        [Description("Step(s) 4 & 8 & 9 for the test CDV6-23533")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_RTT_Treatment_UITestMethod06()
        {
            CreateInpatientCase();
            CreateCaseClosureReasons();

            #region RTT Treatment Status 

            string rttTreatmentStatus_10_Name = (string)dbHelper.rttTreatmentStatus.GetByID(_rttTreatmentStatus_10_Id, "name")["name"];
            string rttTreatmentStatus_30_Name = (string)dbHelper.rttTreatmentStatus.GetByID(_rttTreatmentStatus_30_Id, "name")["name"];
            string rttTreatmentStatus_98_Name = (string)dbHelper.rttTreatmentStatus.GetByID(_rttTreatmentStatus_98_Id, "name")["name"];

            #endregion

            #region RTT Events Records

            var rttWaitTimeRecords = dbHelper.rttWaitTime.GetByRelatedCase(_caseId);
            Assert.AreEqual(1, rttWaitTimeRecords.Count);
            var rttWaitTimeRecordId = rttWaitTimeRecords.First();

            var rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(1, rttEventsRecords.Count);
            var rttEventRecordId1 = rttEventsRecords.First();

            #endregion

            #region Inpatient Consultant Episode 2

            dbHelper.inpatientConsultantEpisode.CreateInpatientConsultantEpisode(_teamId, _caseId, _personID, _systemUserId2, null, _rttTreatmentStatus_30_Id, null, _currentDate, null);

            #endregion

            #region Step 4 & 9

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
                .SelectInpatientStatus("Discharge Awaiting Closure");

            rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(2, rttEventsRecords.Count);

            caseRecordPage
                .ClickDischargeMethodLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(_rttCaseClosureReason_10_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_11_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_12_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_20_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_21_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_30_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_31_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_32_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_33_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_34_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_35_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_36_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_90_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_91_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_92_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_98_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_99_Id.ToString())
                .ClickCloseButton();

            caseRecordPage
                .SwitchToDefaultFrameAndWaitForCaseRecordPageToLoad()
                .SelectInpatientStatus("Discharge");

            caseRecordPage
                .ClickDischargeMethodLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(_rttCaseClosureReason_10_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_11_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_12_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_20_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_21_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_30_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_31_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_32_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_33_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_34_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_35_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_36_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_90_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_91_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_92_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_98_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_99_Id.ToString())
                .ClickCloseButton();

            #endregion

            #region Step 8 & 9

            rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            var rttEventRecordId2 = rttEventsRecords.First();

            CreateConsultantUser_SytemUser3();

            dbHelper.inpatientConsultantEpisode.CreateInpatientConsultantEpisode(_teamId, _caseId, _personID, _systemUserId3, null, _rttTreatmentStatus_98_Id, null, _currentDate, null);

            caseRecordPage
                .SwitchToDefaultFrameAndWaitForCaseRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber.ToString(), _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber.ToString());

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .SelectInpatientStatus("Discharge Awaiting Closure");

            rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(3, rttEventsRecords.Count);

            caseRecordPage
                .ClickDischargeMethodLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementNotPresent(_rttCaseClosureReason_10_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_11_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_12_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_20_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_21_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_30_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_31_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_32_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_33_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_34_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_35_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_36_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_90_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_91_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_92_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_98_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_99_Id.ToString())
                .ClickCloseButton();

            caseRecordPage
                .SwitchToDefaultFrameAndWaitForCaseRecordPageToLoad()
                .SelectInpatientStatus("Discharge");

            caseRecordPage
                .ClickDischargeMethodLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementNotPresent(_rttCaseClosureReason_10_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_11_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_12_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_20_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_21_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_30_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_31_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_32_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_33_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_34_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_35_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_36_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_90_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_91_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_92_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_98_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_99_Id.ToString())
                .ClickCloseButton();


            rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            var rttEventRecordId3 = rttEventsRecords.First();

            caseRecordPage
                .SwitchToDefaultFrameAndWaitForCaseRecordPageToLoad()
                .NavigateToRTTWaitTimePage();

            rttWaitTimesPage
                .WaitForRTTWaitTimesPageToLoad()
                .OpenRecord(rttWaitTimeRecordId);

            rttWaitTimeRecordPage
                .WaitForRTTWaitTimeRecordPageToLoad()
                .WaitForRTTEventsSectionToLoad()
                .ValidateRTTEventRecordCellText(rttEventRecordId1.ToString(), 2, _currentDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRTTEventRecordCellText(rttEventRecordId1.ToString(), 3, rttTreatmentStatus_10_Name.ToString())
                .ValidateRTTEventRecordCellText(rttEventRecordId1.ToString(), 4, "10")
                .ValidateRTTEventRecordCellText(rttEventRecordId1.ToString(), 5, "0")
                .ValidateRTTEventRecordCellText(rttEventRecordId1.ToString(), 6, "0")

                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 2, _currentDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 3, rttTreatmentStatus_30_Name.ToString())
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 4, "30")
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 5, "0")
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 6, "0")

                .ValidateRTTEventRecordCellText(rttEventRecordId3.ToString(), 2, _currentDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRTTEventRecordCellText(rttEventRecordId3.ToString(), 3, rttTreatmentStatus_98_Name.ToString())
                .ValidateRTTEventRecordCellText(rttEventRecordId3.ToString(), 4, "98")
                .ValidateRTTEventRecordCellText(rttEventRecordId3.ToString(), 5, "0")
                .ValidateRTTEventRecordCellText(rttEventRecordId3.ToString(), 6, "0");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2086")]
        [Description("Step(s) 5 & 7 & 9 for the test CDV6-23533")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_RTT_Treatment_UITestMethod07()
        {
            CreateInpatientCase();
            CreateCaseClosureReasons();

            #region RTT Treatment Status

            string rttTreatmentStatus_10_Name = (string)dbHelper.rttTreatmentStatus.GetByID(_rttTreatmentStatus_10_Id, "name")["name"];
            string rttTreatmentStatus_32_Name = (string)dbHelper.rttTreatmentStatus.GetByID(_rttTreatmentStatus_32_Id, "name")["name"];
            string rttTreatmentStatus_36_Name = (string)dbHelper.rttTreatmentStatus.GetByID(_rttTreatmentStatus_36_Id, "name")["name"];

            var _rttTreatmentStatus_100_Id = commonMethodsDB.CreateRTTTreatmentStatus(_teamId, _businessUnitId, "100_RTT Treatment Status", "100", 1, new DateTime(2023, 1, 1), "100_RTT Treatment Status");

            var _rttCaseClosureReason_WithoutCode_Name = "RTT_CCR_Inpatient_WithoutCode";
            _rttCaseClosureReason_WithoutCode_Id = commonMethodsDB.CreateCaseClosureReason(_teamId, _rttCaseClosureReason_WithoutCode_Name, "", "", new DateTime(2021, 1, 1), 140000001, false, _rttTreatmentStatus_100_Id);

            #endregion

            var rttWaitTimeRecords = dbHelper.rttWaitTime.GetByRelatedCase(_caseId);
            Assert.AreEqual(1, rttWaitTimeRecords.Count);
            var rttWaitTimeRecordId = rttWaitTimeRecords.First();

            var rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(1, rttEventsRecords.Count);
            var rttEventRecordId1 = rttEventsRecords.First();

            #region Inpatient Consultant Episode 2

            dbHelper.inpatientConsultantEpisode.CreateInpatientConsultantEpisode(_teamId, _caseId, _personID, _systemUserId2, null, _rttTreatmentStatus_32_Id, null, _currentDate, null);

            #endregion

            #region Step 5 & 9

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
                .SelectInpatientStatus("Discharge Awaiting Closure");

            rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(2, rttEventsRecords.Count);

            caseRecordPage
                .ClickDischargeMethodLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementNotPresent(_rttCaseClosureReason_10_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_11_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_12_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_20_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_21_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_30_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_31_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_32_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_33_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_34_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_35_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_36_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_90_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_91_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_92_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_98_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_99_Id.ToString())
                .ClickCloseButton();

            caseRecordPage
                .SwitchToDefaultFrameAndWaitForCaseRecordPageToLoad()
                .SelectInpatientStatus("Discharge");

            caseRecordPage
                .ClickDischargeMethodLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementNotPresent(_rttCaseClosureReason_10_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_11_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_12_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_20_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_21_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_30_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_31_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_32_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_33_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_34_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_35_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_36_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_90_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_91_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_92_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_98_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_99_Id.ToString())
                .ClickCloseButton();

            #endregion

            #region Step 7 & 9

            rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            var rttEventRecordId2 = rttEventsRecords.First();

            CreateConsultantUser_SytemUser3();

            dbHelper.inpatientConsultantEpisode.CreateInpatientConsultantEpisode(_teamId, _caseId, _personID, _systemUserId3, null, _rttTreatmentStatus_36_Id, null, _currentDate, null);

            caseRecordPage
                .SwitchToDefaultFrameAndWaitForCaseRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber.ToString(), _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber.ToString());

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .SelectInpatientStatus("Discharge Awaiting Closure");

            rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(3, rttEventsRecords.Count);

            caseRecordPage
                .ClickDischargeMethodLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(_rttCaseClosureReason_10_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_11_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_12_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_20_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_21_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_30_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_31_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_32_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_33_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_34_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_35_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_36_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_90_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_91_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_92_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_98_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_99_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_WithoutCode_Id.ToString())
                .ClickCloseButton();

            caseRecordPage
                .SwitchToDefaultFrameAndWaitForCaseRecordPageToLoad()
                .SelectInpatientStatus("Discharge");

            caseRecordPage
                .ClickDischargeMethodLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(_rttCaseClosureReason_10_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_11_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_12_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_20_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_21_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_30_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_31_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_32_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_33_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_34_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_35_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_36_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_90_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_91_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_92_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_98_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_99_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_WithoutCode_Id.ToString())
                .ClickCloseButton();

            rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            var rttEventRecordId3 = rttEventsRecords.First();

            caseRecordPage
                .SwitchToDefaultFrameAndWaitForCaseRecordPageToLoad()
                .NavigateToRTTWaitTimePage();

            rttWaitTimesPage
                .WaitForRTTWaitTimesPageToLoad()
                .OpenRecord(rttWaitTimeRecordId);

            rttWaitTimeRecordPage
                .WaitForRTTWaitTimeRecordPageToLoad()
                .WaitForRTTEventsSectionToLoad()
                .ValidateRTTEventRecordCellText(rttEventRecordId1.ToString(), 2, _currentDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRTTEventRecordCellText(rttEventRecordId1.ToString(), 3, rttTreatmentStatus_10_Name.ToString())
                .ValidateRTTEventRecordCellText(rttEventRecordId1.ToString(), 4, "10")
                .ValidateRTTEventRecordCellText(rttEventRecordId1.ToString(), 5, "0")
                .ValidateRTTEventRecordCellText(rttEventRecordId1.ToString(), 6, "0")

                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 2, _currentDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 3, rttTreatmentStatus_32_Name.ToString())
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 4, "32")
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 5, "0")
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 6, "0")

                .ValidateRTTEventRecordCellText(rttEventRecordId3.ToString(), 2, _currentDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRTTEventRecordCellText(rttEventRecordId3.ToString(), 3, rttTreatmentStatus_36_Name.ToString())
                .ValidateRTTEventRecordCellText(rttEventRecordId3.ToString(), 4, "36")
                .ValidateRTTEventRecordCellText(rttEventRecordId3.ToString(), 5, "0")
                .ValidateRTTEventRecordCellText(rttEventRecordId3.ToString(), 6, "0");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2087")]
        [Description("Step(s) 6 & 9 for the test CDV6-23533")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_RTT_Treatment_UITestMethod08()
        {
            CreateInpatientCase();
            CreateCaseClosureReasons();

            #region RTT Treatment Status

            string rttTreatmentStatus_10_Name = (string)dbHelper.rttTreatmentStatus.GetByID(_rttTreatmentStatus_10_Id, "name")["name"];
            string rttTreatmentStatus_90_Name = (string)dbHelper.rttTreatmentStatus.GetByID(_rttTreatmentStatus_90_Id, "name")["name"];

            #endregion

            var rttWaitTimeRecords = dbHelper.rttWaitTime.GetByRelatedCase(_caseId);
            Assert.AreEqual(1, rttWaitTimeRecords.Count);
            var rttWaitTimeRecordId = rttWaitTimeRecords.First();

            var rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(1, rttEventsRecords.Count);
            var rttEventRecordId1 = rttEventsRecords.First();

            #region Inpatient Consultant Episode 2

            dbHelper.inpatientConsultantEpisode.CreateInpatientConsultantEpisode(_teamId, _caseId, _personID, _systemUserId2, null, _rttTreatmentStatus_90_Id, null, _currentDate, null);

            #endregion

            #region Step 6 & 9

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
                .SelectInpatientStatus("Discharge Awaiting Closure");

            rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(2, rttEventsRecords.Count);

            caseRecordPage
                .ClickDischargeMethodLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(_rttCaseClosureReason_10_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_11_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_12_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_20_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_21_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_30_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_31_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_32_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_33_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_34_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_35_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_36_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_90_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_91_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_92_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_98_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_99_Id.ToString())
                .ClickCloseButton();

            caseRecordPage
                .SwitchToDefaultFrameAndWaitForCaseRecordPageToLoad()
                .SelectInpatientStatus("Discharge");

            caseRecordPage
                .ClickDischargeMethodLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(_rttCaseClosureReason_10_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_11_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_12_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_20_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_21_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_30_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_31_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_32_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_33_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_34_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_35_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_36_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_90_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_91_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_92_Id.ToString())
                .ValidateResultElementNotPresent(_rttCaseClosureReason_98_Id.ToString())
                .ValidateResultElementPresent(_rttCaseClosureReason_99_Id.ToString())
                .ClickCloseButton();


            rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            var rttEventRecordId2 = rttEventsRecords.First();

            caseRecordPage
                .SwitchToDefaultFrameAndWaitForCaseRecordPageToLoad()
                .NavigateToRTTWaitTimePage();

            rttWaitTimesPage
                .WaitForRTTWaitTimesPageToLoad()
                .OpenRecord(rttWaitTimeRecordId);

            rttWaitTimeRecordPage
                .WaitForRTTWaitTimeRecordPageToLoad()
                .WaitForRTTEventsSectionToLoad()
                .ValidateRTTEventRecordCellText(rttEventRecordId1.ToString(), 2, _currentDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRTTEventRecordCellText(rttEventRecordId1.ToString(), 3, rttTreatmentStatus_10_Name.ToString())
                .ValidateRTTEventRecordCellText(rttEventRecordId1.ToString(), 4, "10")
                .ValidateRTTEventRecordCellText(rttEventRecordId1.ToString(), 5, "0")
                .ValidateRTTEventRecordCellText(rttEventRecordId1.ToString(), 6, "0")

                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 2, _currentDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 3, rttTreatmentStatus_90_Name.ToString())
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 4, "90")
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 5, "0")
                .ValidateRTTEventRecordCellText(rttEventRecordId2.ToString(), 6, "0");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2116")]
        [Description("Step(s) 10 to 11 for the test CDV6-23533")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_RTT_Treatment_UITestMethod09()
        {
            CreateInpatientCase();
            CreateCaseClosureReasons();

            #region Inpatient Discharge Destination

            var actualDischargeDestination = commonMethodsDB.CreateInpatientDischargeDestination(_teamId, "Usual residence", new DateTime(2020, 1, 1));

            #endregion

            #region Step 10 & 11

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
                .SelectInpatientStatus("Discharge Awaiting Closure")
                .ClickDischargeMethodLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("RTT_CCR_Inpatient_20")
                .TapSearchButton()
                .SelectResultElement(_rttCaseClosureReason_20_Id.ToString());

            caseRecordPage
                .InsertActualDischargeDateTime(_currentDate.ToString("dd'/'MM'/'yyyy"), "00:00")
                .ClickActualDischargeDestinationLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Usual residence")
                .TapSearchButton()
                .SelectResultElement(actualDischargeDestination.ToString());

            caseRecordPage
                .ClickTeamResponsibleForFollowUpAppointmentLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookFor("Teams")
                .SelectLookIn("Lookup View")
                .TypeSearchQuery("RTT T1")
                .TapSearchButton()
                .SelectResultElement(_teamId.ToString());

            caseRecordPage
                .ClickSaveButton();

            System.Threading.Thread.Sleep(2000);

            caseRecordPage
                .ValidateInpatientStatusSelectedText("Discharge Awaiting Closure")
                .ValidateDischargeMethodLookupButtonDisabled(false)
                .SelectInpatientStatus("Discharge")
                .ClickSaveButton();

            System.Threading.Thread.Sleep(2000);

            caseRecordPage
                .ValidateInpatientStatusSelectedText("Discharge")
                .ValidateDischargeMethodLookupButtonDisabled(true);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-2335

        [TestProperty("JiraIssueID", "ACC-2440")]
        [Description("Step(s) 1, 2 & 4 for the test CDV6-22778")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_RTTEvent_UITestMethod01()
        {
            CreateRequireDataAndCaseCommunityCase();

            #region Case Closuer Reason

            _rttCaseClosureReason_21_Id = commonMethodsDB.CreateCaseClosureReason(_teamId, "RTT_CCR_Community_21", "", "21", new DateTime(2021, 1, 1), 140000000, false, _rttTreatmentStatus_21_Id);

            #endregion

            #region Provider (Transfer To)

            var _provider_Name2 = "RTT_2_" + _currentDateString;
            var _provider_Id2 = commonMethodsDB.CreateProvider(_provider_Name2, _teamId);
            dbHelper.provider.UpdateRTTTreatmentFunction(_provider_HospitalId, rttTreatmentFunctionCodeId);

            #endregion

            #region Update RTT Pathway

            dbHelper.rttPathwaySetup.UpdateBreachOccursAfter(_rttPathwaySetupId, 10);

            #endregion

            #region Community Case record

            var rttReferral = 1;

            _caseId = dbHelper.Case.CreateCommunityHealthCaseRecord(_teamId, _personID, _systemUserId, _communityAndClinicTeamId, _systemUserId, _awaitingFurtherInformation_caseStatusId, _contactReasonId_Community, _contactAdministrativeCategoryId,
                            _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, new DateTime(2023, 6, 9), new DateTime(2023, 6, 9), new DateTime(2023, 6, 9), DateTime.Now.Date, "a relevant directory where a user would be entitled to make thoroughly clear what is",
                            _systemUserId, DateTime.Now.Date, rttReferral, _rttPathwaySetupId, _rttTreatmentStatusId);

            _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];

            #endregion

            #region Step 1

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
                .NavigateToRTTWaitTimePage();

            var rttWaitTimeRecords = dbHelper.rttWaitTime.GetByRelatedCase(_caseId);
            Assert.AreEqual(1, rttWaitTimeRecords.Count);
            var rttWaitTimeRecordId = rttWaitTimeRecords.First();

            rttWaitTimesPage
                .WaitForRTTWaitTimesPageToLoad()
                .OpenRecord(rttWaitTimeRecordId);

            var rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(1, rttEventsRecords.Count);
            var rttEventRecordId = rttEventsRecords.First();

            #endregion

            #region Step 2

            rttWaitTimeRecordPage
                .WaitForRTTWaitTimeRecordPageToLoad()
                .ValidateCaseStartDateText(new DateTime(2023, 6, 9).ToString("dd'/'MM'/'yyyy"))
                .ValidateBreachDateText(new DateTime(2023, 6, 19).ToString("dd'/'MM'/'yyyy"))
                .ValidateIsClockRunning_YesRadioButtonChecked()
                .ValidateIsClockRunning_NoRadioButtonNotChecked()
                .WaitForRTTEventsSectionToLoad()
                .ValidateRTTEventRecordPresent(rttEventRecordId.ToString())
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 2, new DateTime(2023, 6, 9).ToString("dd'/'MM'/'yyyy"))
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 3, "First Activity in new RTT period")
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 4, "10")
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 5, "0")
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 6, "0");

            rttWaitTimeRecordPage
                .WaitForRTTWaitTimeRecordPageToLoad()
                .NavigateToRttEventsSubPage();

            rttEventsPage
                .WaitForRTTEventsPageToLoad()
                .OpenRecord(rttEventRecordId);

            rttEventRecordPage
                .WaitForRTTEventRecordPageToLoad()
                .ValidateDateText(new DateTime(2023, 6, 9).ToString("dd'/'MM'/'yyyy"))
                .ValidateRttTreatmentStatusLinkText("First Activity in new RTT period")
                .ValidateRTTStatusCodeText("10")
                .ValidateDaysWaitedText("0")
                .ValidateWeeksWaitedText("0");

            #endregion

            #region Step 4

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber.ToString(), _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber.ToString());

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .ClickDischargePerson_YesOption()
                .ValidateDischargeMethodFieldVisibility(true)
                .ClickDischargeMethodLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("RTT_CCR_Community_21").TapSearchButton().SelectResultElement(_rttCaseClosureReason_21_Id);

            caseRecordPage
                .ValidateTransferToProviderFieldVisibility(true)
                .ClickTransferToProviderLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_provider_Name2).TapSearchButton().SelectResultElement(_provider_Id2);

            caseRecordPage
                .ValidateDischargingProfessionalFieldVisibility(true)
                .ClickDischargingProfessionalLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery(_systemUsername2).TapSearchButton().SelectResultElement(_systemUserId2);

            caseRecordPage
                .ClickSaveButton()
                .WaitForCaseRecordToSaved();

            caseRecordPage
                .NavigateToRTTWaitTimePage();

            rttWaitTimesPage
                .WaitForRTTWaitTimesPageToLoad()
                .OpenRecord(rttWaitTimeRecordId);

            rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(2, rttEventsRecords.Count);
            var rttEventRecordId2 = rttEventsRecords.First();

            rttWaitTimeRecordPage
                .WaitForRTTWaitTimeRecordPageToLoad()
                .NavigateToRttEventsSubPage();

            rttEventsPage
                .WaitForRTTEventsPageToLoad()
                .OpenRecord(rttEventRecordId2);

            rttEventRecordPage
                .WaitForRTTEventRecordPageToLoad()
                .ValidateTransferProviderLinkText(_provider_Name2);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2441")]
        [Description("Step(s) 3 & 8 for the test CDV6-22778")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_RTTEvent_UITestMethod02()
        {
            CreateRequireDataAndCaseCommunityCase();

            #region Provider (Carer)

            var provider_WithoutRTT = commonMethodsDB.CreateProvider("Provider_Supplier_WithoutRTTTreatmentFunction", _teamId, 2);

            #endregion

            #region Community and Clinic Team

            var _communityAndClinicTeamName_2 = "Community Team Without RTT Function";
            var _communityAndClinicTeamId_2 = commonMethodsDB.CreateCommunityAndClinicTeam(_teamId, provider_WithoutRTT, _teamId, _communityAndClinicTeamName_2, "Created by Without RTT Treatment Function");
            var _communityClinicDiaryViewSetupId2 = dbHelper.communityClinicDiaryViewSetup.CreateCommunityClinicDiaryViewSetup(_teamId, _communityAndClinicTeamId_2, "Home Visit Data", new DateTime(2023, 1, 1), new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0), 500, 100, 500);

            #endregion

            #region Community Clinic Linked Professional

            dbHelper.communityClinicLinkedProfessional.CreateCommunityClinicLinkedProfessional(_teamId, _communityClinicDiaryViewSetupId2, _systemUserId, new DateTime(2023, 2, 2), new TimeSpan(1, 5, 0), new TimeSpan(23, 55, 0), _recurrencePatternId, _systemUserFullName);

            #endregion

            #region Step 3 

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .ClickNewRecordButton();

            selectCaseTypePopUp.WaitForSelectCaseTypePopUpToLoad().SelectViewByText("Community Health Case").TapNextButton();

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickPersonLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(_personFullName.ToString()).TapSearchButton().SelectResultElement(_personID);

            caseRecordPage
                .ClickContactReasonLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Test Contact Reason (Comm)").TapSearchButton().SelectResultElement(_contactReasonId_Community);

            caseRecordPage
                .InsertDateContactReceived(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ClickContactReceivedByLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery(_systemUsername.ToString()).TapSearchButton().SelectResultElement(_systemUserId);

            caseRecordPage
                .InsertDateRequestReceived(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .InsertOutlineNeedForAdmission("Community Case")
                .ClickContactSourceLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_contactSourceName.ToString()).TapSearchButton().SelectResultElement(_contactSourceId);

            caseRecordPage
                .ClickAdministrativeCategoryLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_contactAdministrativeCategoryName.ToString()).TapSearchButton().SelectResultElement(_contactAdministrativeCategoryId);

            caseRecordPage
                .SelectIsThePersonAwareOfTheContact("Not Known")
                .ClickCommunityClinicTeamRequiredLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_communityAndClinicTeamName_2.ToString()).TapSearchButton().SelectResultElement(_communityAndClinicTeamId_2);

            caseRecordPage
                .SelectRTTReferral("Yes")
                .ClickRTTTreatmentStatusLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_rttTreatmentStatus_10_Id);

            caseRecordPage
                .ClickRTTPathwayLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default RTT Pathway").TapSearchButton().SelectResultElement(_rttPathwaySetupId);

            caseRecordPage
                .ClickCurrentConsultantLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery(_systemUsername.ToString()).TapSearchButton().SelectResultElement(_systemUserId);

            caseRecordPage
                .ClickServiceTypeRequestedLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_caseServiceTypeRequestedName.ToString()).TapSearchButton().SelectResultElement(_caseServiceTypeRequestedid);

            caseRecordPage
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForCareCloudDynamicDialoguePopUpToLoad()
                .ValidateMessage("The system is unable to create this RTT Case because the RTT Treatment Function is not set on the Provider")
                .TapCloseButton();

            #endregion

            #region Step 8

            caseRecordPage
                .SelectRTTReferral("No")
                .ClickSaveButton()
                .NavigateToRTTWaitTimePage();

            rttWaitTimesPage
                .WaitForRTTWaitTimesPageToLoad()
                .ValidateNoRecordMessageVisibile(true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2442")]
        [Description("Step(s) 5 to 7 for the test CDV6-22778")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_RTTEvent_UITestMethod03()
        {
            CreateRequireDataAndCaseCommunityCase();

            #region Community Case record

            var rttReferral = 1;
            // Case Record 1
            _caseId = dbHelper.Case.CreateCommunityHealthCaseRecord(_teamId, _personID, _systemUserId, _communityAndClinicTeamId, _systemUserId, _awaitingFurtherInformation_caseStatusId, _contactReasonId_Community, _contactAdministrativeCategoryId,
                            _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, new DateTime(2023, 6, 9), new DateTime(2023, 6, 9), new DateTime(2023, 6, 9), DateTime.Now.Date, "a relevant directory where a user would be entitled to make thoroughly clear what is",
                            _systemUserId, DateTime.Now.Date, rttReferral, _rttPathwaySetupId, _rttTreatmentStatus_34_Id);

            _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];

            // Case Record 2
            var _caseId2 = dbHelper.Case.CreateCommunityHealthCaseRecord(_teamId, _personID, _systemUserId, _communityAndClinicTeamId, _systemUserId, _awaitingFurtherInformation_caseStatusId, _contactReasonId_Community, _contactAdministrativeCategoryId,
                            _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, new DateTime(2023, 6, 9), new DateTime(2023, 6, 9), new DateTime(2023, 6, 9), DateTime.Now.Date, "a relevant directory where a user would be entitled to make thoroughly clear what is",
                            _systemUserId, DateTime.Now.Date, rttReferral, _rttPathwaySetupId, _rttTreatmentStatus_10_Id);

            var _caseNumber2 = (string)dbHelper.Case.GetCaseByID(_caseId2, "casenumber")["casenumber"];

            #endregion

            #region Step 5

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
                .NavigateToRTTWaitTimePage();

            var rttWaitTimeRecords = dbHelper.rttWaitTime.GetByRelatedCase(_caseId);
            Assert.AreEqual(1, rttWaitTimeRecords.Count);
            var rttWaitTimeRecordId = rttWaitTimeRecords.First();

            rttWaitTimesPage
                .WaitForRTTWaitTimesPageToLoad()
                .OpenRecord(rttWaitTimeRecordId);

            var rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(1, rttEventsRecords.Count);

            rttWaitTimeRecordPage
                .WaitForRTTWaitTimeRecordPageToLoad()
                .ValidateIsClockRunning_YesRadioButtonNotChecked()
                .ValidateIsClockRunning_NoRadioButtonChecked()
                .ClickBackButton();

            #endregion

            #region Step 7

            // Create Health Appointment and change the Outcome Type to 21 (Transferred)
            var _communityClinicAppointmentOutcomeType_21_Id = commonMethodsDB.CreateHealthAppointmentOutcomeType(_teamId, "RTT_CC_AppointmentOutcome_21", new DateTime(2021, 1, 1), "", "21", _rttTreatmentStatus_21_Id);
            CreateHealthAppointment();
            dbHelper.healthAppointment.UpdateHealthAppointmentOutcomeType(_healthAppointmentId, _communityClinicAppointmentOutcomeType_21_Id);

            rttWaitTimesPage
                .WaitForRTTWaitTimesPageToLoad()
                .OpenRecord(rttWaitTimeRecordId);

            rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(2, rttEventsRecords.Count);

            rttWaitTimeRecordPage
                .WaitForRTTWaitTimeRecordPageToLoad()
                .ValidateIsClockRunning_YesRadioButtonNotChecked()
                .ValidateIsClockRunning_NoRadioButtonChecked();

            #endregion

            #region Step 6

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber2.ToString(), _caseId2.ToString())
                .OpenCaseRecord(_caseId2.ToString(), _caseNumber2.ToString());

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToRTTWaitTimePage();

            rttWaitTimeRecords = dbHelper.rttWaitTime.GetByRelatedCase(_caseId2);
            Assert.AreEqual(1, rttWaitTimeRecords.Count);
            rttWaitTimeRecordId = rttWaitTimeRecords.First();

            rttWaitTimesPage
                .WaitForRTTWaitTimesPageToLoad()
                .OpenRecord(rttWaitTimeRecordId);

            rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(1, rttEventsRecords.Count);

            rttWaitTimeRecordPage
                .WaitForRTTWaitTimeRecordPageToLoad()
                .ValidateIsClockRunning_YesRadioButtonChecked()
                .ValidateIsClockRunning_NoRadioButtonNotChecked();

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-2552

        [TestProperty("JiraIssueID", "ACC-2638")]
        [Description("Step(s) 1 to 5 for the test CDV6-22618")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_RTTWait_UITestMethod01()
        {
            CreateRequireDataAndCaseCommunityCase();

            DateTime currentDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now);

            #region Update RTT Pathway

            dbHelper.rttPathwaySetup.UpdateBreachOccursAfter(_rttPathwaySetupId, 10);

            #endregion

            #region Community Case record

            var rttReferral = 1;

            _caseId = dbHelper.Case.CreateCommunityHealthCaseRecord(_teamId, _personID, _systemUserId, _communityAndClinicTeamId, _systemUserId, _awaitingFurtherInformation_caseStatusId, _contactReasonId_Community, _contactAdministrativeCategoryId,
                            _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, currentDate, currentDate, currentDate, currentDate, "a relevant directory where a user would be entitled to make thoroughly clear what is",
                            _systemUserId, DateTime.Now.Date, rttReferral, _rttPathwaySetupId, _rttTreatmentStatusId);

            _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];
            var _caseTitle = dbHelper.Case.GetCaseByID(_caseId, "title")["title"];

            #endregion

            #region Step 1 to 5

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
                .NavigateToRTTWaitTimePage();

            var rttWaitTimeRecords = dbHelper.rttWaitTime.GetByRelatedCase(_caseId);
            Assert.AreEqual(1, rttWaitTimeRecords.Count);
            var rttWaitTimeRecordId = rttWaitTimeRecords.First();

            rttWaitTimesPage
                .WaitForRTTWaitTimesPageToLoad()
                .OpenRecord(rttWaitTimeRecordId);

            var rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(1, rttEventsRecords.Count);
            var rttEventRecordId = rttEventsRecords.First();

            rttWaitTimeRecordPage
                .WaitForRTTWaitTimeRecordPageToLoad()
                .ValidateRelatedCaseLinkText(_caseTitle.ToString())
                .ValidateRTTPathwayLinkText(_rttPathwaySetupName)
                .ValidateRTTTreatmentFunctionLinkText(rttTreatmentFunctionCodeName)
                .ValidateProviderLinkText("CareDirector QA Provider Health Appointment")
                .ValidateCaseStartDateText(currentDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateBreachDateText(currentDate.AddDays(10).ToString("dd'/'MM'/'yyyy"))
                .ValidateDaysWaitedText("0")
                .ValidateWeeksWaitedText("0")
                .ValidateResponsibleUserLinkText(_systemUserFullName)
                .ValidateIsClockRunning_YesRadioButtonChecked()
                .ValidateIsClockRunning_NoRadioButtonNotChecked()

                .WaitForRTTEventsSectionToLoad()
                .ValidateRTTEventRecordPresent(rttEventRecordId.ToString())
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 2, currentDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 3, "First Activity in new RTT period")
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 4, "10")
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 5, "0")
                .ValidateRTTEventRecordCellText(rttEventRecordId.ToString(), 6, "0");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2640")]
        [Description("Step(s) 9 to 12 for the test CDV6-22618")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_RTTWait_UITestMethod02()
        {
            CreateRequireDataAndCaseCommunityCase();

            DateTime currentDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now);
            DateTime PastDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.Date.AddDays(-1));

            #region Step 9 to 12

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .ClickNewRecordButton();

            selectCaseTypePopUp
                .WaitForSelectCaseTypePopUpToLoad()
                .SelectViewByText("Community Health Case")
                .TapNextButton();

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickPersonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("All Active People")
                .TypeSearchQuery(_personFullName.ToString())
                .TapSearchButton()
                .SelectResultElement(_personID);

            caseRecordPage
                .ClickContactReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Test Contact Reason (Comm)")
                .TapSearchButton()
                .SelectResultElement(_contactReasonId_Community);

            caseRecordPage
                .InsertDateContactReceived(currentDate.ToString("dd'/'MM'/'yyyy"))
                .ClickContactReceivedByLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup View")
                .TypeSearchQuery(_systemUsername.ToString())
                .TapSearchButton()
                .SelectResultElement(_systemUserId);

            caseRecordPage
                .InsertDateRequestReceived(currentDate.ToString("dd'/'MM'/'yyyy"))
                .InsertOutlineNeedForAdmission("Community Case")
                .ClickContactSourceLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_contactSourceName.ToString())
                .TapSearchButton()
                .SelectResultElement(_contactSourceId);

            caseRecordPage
                .ClickAdministrativeCategoryLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_contactAdministrativeCategoryName.ToString())
                .TapSearchButton()
                .SelectResultElement(_contactAdministrativeCategoryId);

            caseRecordPage
                .SelectIsThePersonAwareOfTheContact("Not Known")
                .ClickCommunityClinicTeamRequiredLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("RTT Treatment Status Team")
                .TapSearchButton()
                .SelectResultElement(_communityAndClinicTeamId);

            caseRecordPage
                .SelectRTTReferral("Transferred-In")
                .ValidateRTTTreatmentStatusFieldVisibility(true)
                .ValidateRTTPathwayFieldVisibility(true)
                .ValidateTransferredFromProviderFieldVisibility(true)
                .ValidateOriginalRTTReferralStartDateFieldVisibility(true)

                .ValidateRTTTreatmentStatus_MandatoryField(true)
                .ValidateRTTPathway_MandatoryField(true)
                .ValidateTransferredFromProvider_MandatoryField(true)
                .ValidateOriginalRTTReferralStartDate_MandatoryField(true);

            caseRecordPage
                .ClickRTTTreatmentStatusLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("First Activity in new RTT period")
                .TapSearchButton()
                .SelectResultElement(_rttTreatmentStatusId.ToString());

            caseRecordPage
                .ClickRTTPathwayLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_rttPathwaySetupName)
                .TapSearchButton()
                .SelectResultElement(_rttPathwaySetupId.ToString());

            caseRecordPage
                .ClickTransferredFromProviderLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_provider_Name)
                .TapSearchButton()
                .SelectResultElement(_provider_HospitalId.ToString());

            caseRecordPage
                .InsertOriginalRTTReferralStartDate(PastDate.ToString("dd'/'MM'/'yyyy"))
                .ClickCurrentConsultantLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup View")
                .TypeSearchQuery(_systemUsername)
                .TapSearchButton()
                .SelectResultElement(_systemUserId.ToString());

            caseRecordPage
                .ClickServiceTypeRequestedLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_caseServiceTypeRequestedName)
                .TapSearchButton()
                .SelectResultElement(_caseServiceTypeRequestedid.ToString());

            caseRecordPage
                .ClickSaveButton()
                .WaitForCaseRecordToSaved();

            var cases = dbHelper.Case.GetCasesByPersonID(_personID);
            Assert.AreEqual(1, cases.Count);
            var caseTitle = (string)dbHelper.Case.GetCaseByID(cases[0], "title")["title"];

            caseRecordPage
                .ValidateCaseRecordTitle(caseTitle);

            caseRecordPage
                .TapDetailsLink()
                .ValidateRTTTreatmentStatusFieldVisibility(true)
                .ValidateRTTPathwayFieldVisibility(true)
                .ValidateRTTReferralSelectedText("Transferred-In")
                .ValidateRTTTreatmentStatusLinkText("First Activity in new RTT period")
                .ValidateRTTPathwayLinkText(_rttPathwaySetupName)
                .ValidateTransferredFromProviderLinkText(_provider_Name)
                .ValidateOriginalRTTReferralStartDateFieldValue(PastDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateTransferredFromProviderLookupIsDisabled(false)
                .ValidateOriginalRTTReferralStartDateFieldIsDisabled(true);

            caseRecordPage
                .SwitchToDefaultFrameAndWaitForCaseRecordPageToLoad()
                .NavigateToRTTWaitTimePage();

            var rttWaitTimeRecords = dbHelper.rttWaitTime.GetByRelatedCase(cases[0]);
            Assert.AreEqual(1, rttWaitTimeRecords.Count);
            var rttWaitTimeRecordId = rttWaitTimeRecords.First();

            rttWaitTimesPage
                .WaitForRTTWaitTimesPageToLoad()
                .ValidateRecordVisible(rttWaitTimeRecordId.ToString());

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

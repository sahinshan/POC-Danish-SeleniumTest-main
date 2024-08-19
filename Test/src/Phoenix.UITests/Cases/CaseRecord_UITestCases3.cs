using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Cases
{
    [DeploymentItem("Files\\RTT Event - Case Discharged.Zip"), DeploymentItem("Files\\RTT Event - Consultant Episode RTT Treatment Status.Zip"), DeploymentItem("Files\\RTT Event - Health Appointment Outcome.Zip"), DeploymentItem("Files\\RTT Event - New RTT Case.Zip"), DeploymentItem("chromedriver.exe")]
    [TestClass]
    public class CaseRecord_UITestCases3 : FunctionalTest
    {
        private string _environmentName;
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private string _systemUsername;
        private string _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");


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

                #region System User RTTUser3

                _systemUsername = "RTTUser3";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "RTT", "User3", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion             
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        public void ImportRttWorkflows()
        {
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
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-1084

        [TestProperty("JiraIssueID", "CDV6-23644")]
        [Description("Step(s) 1 and 2 for the test CDV6-23644")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_RTT_UITestMethod01()
        {
            #region Import Workflows

            ImportRttWorkflows();

            #endregion

            #region Job Role Type

            var _jobRoleTypeId = commonMethodsDB.CreateJobRoleType(_teamId, "Default Consultant", new DateTime(2020, 1, 1), 1);
            dbHelper.jobRoleType.UpdateIsConsultantId(_jobRoleTypeId, true);

            #endregion

            #region Update System User Job Role Type

            dbHelper.systemUser.UpdateJobRoleType(_systemUserId, _jobRoleTypeId);

            #endregion

            #region Person

            var personFirstName = "Logan";
            var personLastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = "Logan " + personLastName;
            var _personID = commonMethodsDB.CreatePersonRecord(personFirstName, personLastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));

            #endregion

            #region Case Status
            var _admission_CaseStatusId = dbHelper.caseStatus.GetByName("Admission").FirstOrDefault();
            var _awaitingAdmission_caseStatusId = dbHelper.caseStatus.GetByName("Awaiting Admission").FirstOrDefault();

            #endregion

            #region Contact Reason

            var _contactReasonId = commonMethodsDB.CreateContactReason(_teamId, "Default Inpatient Management Contact Reason", new DateTime(2020, 1, 1), 140000001, 2, false);

            #endregion

            #region Contact Source

            var _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Default Contact Source", _teamId);

            #endregion

            #region Data Form

            var _dataFormId = dbHelper.dataForm.GetByName("InpatientCase")[0];

            #endregion

            #region Hospital Ward Specialty

            var _wardSpecialityId = dbHelper.inpatientWardSpecialty.GetByName("Adult Acute")[0];

            #endregion

            #region Inpatient Bed Type

            var _inpatientBedTypeId = dbHelper.inpatientBedType.GetByName("Clinitron")[0];

            #endregion

            #region Rtt Treatment Function

            var rttTreatmentFunctionCodeId = dbHelper.rttTreatmentFunctionCode.GetByName("General Surgery Service")[0];

            #endregion

            #region Provider

            var _provider_Name = "RTT_" + _currentDateString;
            var _provider_HospitalId = commonMethodsDB.CreateProvider(_provider_Name, _teamId);
            dbHelper.provider.UpdateRTTTreatmentFunction(_provider_HospitalId, rttTreatmentFunctionCodeId);

            #endregion

            #region Ward

            var _inpatientWardName = "Ward_" + _currentDateString;
            var _inpatientWardId = dbHelper.inpatientWard.CreateInpatientWard(_teamId, _provider_HospitalId, _systemUserId, _wardSpecialityId, _inpatientWardName, new DateTime(2020, 1, 1));

            #endregion

            #region Bay/Room

            var _inpatientBayName = "Bay_" + _currentDateString;
            var _inpatientBayId = dbHelper.inpatientBay.CreateInpatientCaseBay(_teamId, _inpatientWardId, _inpatientBayName, 1, "4", "4", "4", 2);

            #endregion

            #region Bed

            var _inpatientBedId = dbHelper.inpatientBed.CreateInpatientBed(_teamId, "12665", "4", "4", _inpatientBayId, 1, _inpatientBedTypeId, "4");

            #endregion

            #region Contact Inpatient Admission Source

            var _inpatientAdmissionSourceId = commonMethodsDB.CreateInpatientAdmissionSource(_teamId, "Default Inpatient Admission Source", new DateTime(2020, 1, 1));

            #endregion

            #region Inpatient Admission Method

            var _inpationAdmissionMethodId = commonMethodsDB.CreateInpatientAdmissionMethod("Default Inpatient Admission Method", _teamId, _businessUnitId, new DateTime(2020, 1, 1));

            #endregion

            #region RTT Treatment Statuses

            var rttTreatmentStatusId = dbHelper.rttTreatmentStatus.GetByName("First Activity in new RTT period")[0];

            #endregion

            #region RTT Pathway Setup

            var rttPathwaySetupId = commonMethodsDB.CreateRTTPathwaySetup(_teamId, "Default RTT Pathway", new DateTime(2020, 1, 1), 2, 5, 10);

            #endregion

            #region Case

            var rttReferral = 1; //Yes
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var _caseId = dbHelper.Case.CreateInpatientCaseRecordWithStatusAsAdmission(
                _teamId, _personID, currentDate, _systemUserId, "presenting needs ...",
                _systemUserId, _admission_CaseStatusId, _contactReasonId, currentDate, _dataFormId,
                _contactSourceId, _inpatientWardId, _inpatientBayId, _inpatientBedId, _inpatientAdmissionSourceId,
                _inpationAdmissionMethodId, _systemUserId, currentDate, _provider_HospitalId,
                _inpatientWardId, 1, currentDate,
                false, false, false, false, false, false, false, false, false, false,
                rttReferral, rttTreatmentStatusId, rttPathwaySetupId);

            var _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login("RTTUser3", "Passw0rd_!", _environmentName)
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
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

            rttWaitTimesPage
                .OpenRecord(rttWaitTimeRecordId);

            rttWaitTimeRecordPage
                .WaitForRTTWaitTimeRecordPageToLoad()
                .ValidateBreachDateText(currentDate.AddDays(10).ToString("dd'/'MM'/'yyyy"))
                .NavigateToRttEventsSubPage();

            var rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(1, rttEventsRecords.Count);
            var rttEventRecordId = rttEventsRecords.First();

            #endregion

            #region Step 2

            rttEventsPage
                .WaitForRTTEventsPageToLoad()
                .OpenRecord(rttEventRecordId);

            rttEventRecordPage
                .WaitForRTTEventRecordPageToLoad()
                .ValidateDateText(currentDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRttTreatmentStatusLinkText("First Activity in new RTT period")
                .ValidateRTTStatusCodeText("10")
                .ValidateDaysWaitedText("0")
                .ValidateWeeksWaitedText("0")
                ;

            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-25216")]
        [Description("Step(s) 3 for the test CDV6-23644")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_RTT_UITestMethod02()
        {
            #region Import Workflows

            ImportRttWorkflows();

            #endregion

            #region Job Role Type

            var _jobRoleTypeId = commonMethodsDB.CreateJobRoleType(_teamId, "Default Consultant", new DateTime(2020, 1, 1), 1);
            dbHelper.jobRoleType.UpdateIsConsultantId(_jobRoleTypeId, true);

            #endregion

            #region Update System User Job Role Type

            dbHelper.systemUser.UpdateJobRoleType(_systemUserId, _jobRoleTypeId);

            #endregion

            #region Person

            var personFirstName = "Logan";
            var personLastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = "Logan " + personLastName;
            var _personID = commonMethodsDB.CreatePersonRecord(personFirstName, personLastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));

            #endregion

            #region Case Status
            var _admission_CaseStatusId = dbHelper.caseStatus.GetByName("Admission").FirstOrDefault();
            var _awaitingAdmission_caseStatusId = dbHelper.caseStatus.GetByName("Awaiting Admission").FirstOrDefault();

            #endregion

            #region Contact Reason

            var _contactReasonId = commonMethodsDB.CreateContactReason(_teamId, "Default Inpatient Management Contact Reason", new DateTime(2020, 1, 1), 140000001, 2, false);

            #endregion

            #region Contact Source

            var _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Default Contact Source", _teamId);

            #endregion

            #region Data Form

            var _dataFormId = dbHelper.dataForm.GetByName("InpatientCase")[0];

            #endregion

            #region Hospital Ward Specialty

            var _wardSpecialityId = dbHelper.inpatientWardSpecialty.GetByName("Adult Acute")[0];

            #endregion

            #region Inpatient Bed Type

            var _inpatientBedTypeId = dbHelper.inpatientBedType.GetByName("Clinitron")[0];

            #endregion

            #region Provider

            var _provider_Name = "RTT_" + _currentDateString;
            var _provider_HospitalId = commonMethodsDB.CreateProvider(_provider_Name, _teamId);

            #endregion

            #region Ward

            var _inpatientWardName = "Ward_" + _currentDateString;
            var _inpatientWardId = dbHelper.inpatientWard.CreateInpatientWard(_teamId, _provider_HospitalId, _systemUserId, _wardSpecialityId, _inpatientWardName, new DateTime(2020, 1, 1));

            #endregion

            #region Bay/Room

            var _inpatientBayName = "Bay_" + _currentDateString;
            var _inpatientBayId = dbHelper.inpatientBay.CreateInpatientCaseBay(_teamId, _inpatientWardId, _inpatientBayName, 1, "4", "4", "4", 2);

            #endregion

            #region Bed

            var _inpatientBedId = dbHelper.inpatientBed.CreateInpatientBed(_teamId, "12665", "4", "4", _inpatientBayId, 1, _inpatientBedTypeId, "4");

            #endregion

            #region Contact Inpatient Admission Source

            var _inpatientAdmissionSourceId = commonMethodsDB.CreateInpatientAdmissionSource(_teamId, "Default Inpatient Admission Source", new DateTime(2020, 1, 1));

            #endregion

            #region Inpatient Admission Method

            var _inpationAdmissionMethodId = commonMethodsDB.CreateInpatientAdmissionMethod("Default Inpatient Admission Method", _teamId, _businessUnitId, new DateTime(2020, 1, 1));

            #endregion

            #region RTT Treatment Statuses

            var rttTreatmentStatusId = dbHelper.rttTreatmentStatus.GetByName("First Activity in new RTT period")[0];

            #endregion

            #region RTT Pathway Setup

            var rttPathwaySetupId = commonMethodsDB.CreateRTTPathwaySetup(_teamId, "Default RTT Pathway", new DateTime(2020, 1, 1), 2, 5, 10);

            #endregion

            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture();

            #region Step 3

            loginPage
                .GoToLoginPage()
                .Login("RTTUser3", "Passw0rd_!", _environmentName)
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByLastName(personLastName, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .ClickNewRecordButton();

            selectCaseTypePopUp.WaitForSelectCaseTypePopUpToLoad().SelectViewByText("Inpatient Case").TapNextButton();

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .InsertDateContactReceived(currentDate.ToString("dd'/'MM'/'yyyy"))
                .ClickContactReceivedByLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery(_systemUsername).TapSearchButton().SelectResultElement(_systemUserId);

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickContactReasonLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default Inpatient Management Contact Reason").TapSearchButton().SelectResultElement(_contactReasonId);

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickContactSourceLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default Contact Source").TapSearchButton().SelectResultElement(_contactSourceId);

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickAdmissionSourceLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default Inpatient Admission Source").TapSearchButton().SelectResultElement(_inpatientAdmissionSourceId);

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .InsertOutlineNeedForAdmission("need info ...")
                .ClickAdmissionMethodLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default Inpatient Admission Method").TapSearchButton().SelectResultElement(_inpationAdmissionMethodId);

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .InsertAdmissionDateTime(currentDate.ToString("dd'/'MM'/'yyyy"), "")
                .ClickCurrentConsultantLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery(_systemUsername).TapSearchButton().SelectResultElement(_systemUserId);

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickHospitalLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_provider_Name).TapSearchButton().SelectResultElement(_provider_HospitalId);

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickWardLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_inpatientWardName).TapSearchButton().SelectResultElement(_inpatientWardId);

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickBayRoomLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_inpatientBayName).TapSearchButton().SelectResultElement(_inpatientBayId);

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickBedLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TapSearchButton().SelectResultElement(_inpatientBedId);

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickResponsibleWardLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TapSearchButton().SelectResultElement(_inpatientWardId);

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .SelectRTTReferral("Yes")
                .ClickRTTTreatmentStatusLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("First Activity in new RTT period").TapSearchButton().SelectResultElement(rttTreatmentStatusId);

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickRTTPathwayLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default RTT Pathway").TapSearchButton().SelectResultElement(rttPathwaySetupId);

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("The system is unable to create this RTT Case because the RTT Treatment Function is not set on the Provider")
                .TapCloseButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-25217")]
        [Description("Step(s) 4 for the test CDV6-23644")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_RTT_UITestMethod03()
        {
            #region Import Workflows

            ImportRttWorkflows();

            #endregion

            #region Job Role Type

            var _jobRoleTypeId = commonMethodsDB.CreateJobRoleType(_teamId, "Default Consultant", new DateTime(2020, 1, 1), 1);
            dbHelper.jobRoleType.UpdateIsConsultantId(_jobRoleTypeId, true);

            #endregion

            #region Update System User Job Role Type

            dbHelper.systemUser.UpdateJobRoleType(_systemUserId, _jobRoleTypeId);

            #endregion

            #region Person

            var personFirstName = "Logan";
            var personLastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = "Logan " + personLastName;
            var _personID = commonMethodsDB.CreatePersonRecord(personFirstName, personLastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));

            #endregion

            #region Case Status
            var _admission_CaseStatusId = dbHelper.caseStatus.GetByName("Admission").FirstOrDefault();
            var _awaitingAdmission_caseStatusId = dbHelper.caseStatus.GetByName("Awaiting Admission").FirstOrDefault();

            #endregion

            #region Contact Reason

            var _contactReasonId = commonMethodsDB.CreateContactReason(_teamId, "Default Inpatient Management Contact Reason", new DateTime(2020, 1, 1), 140000001, 2, false);

            #endregion

            #region Contact Source

            var _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Default Contact Source", _teamId);

            #endregion

            #region Data Form

            var _dataFormId = dbHelper.dataForm.GetByName("InpatientCase")[0];

            #endregion

            #region Hospital Ward Specialty

            var _wardSpecialityId = dbHelper.inpatientWardSpecialty.GetByName("Adult Acute")[0];

            #endregion

            #region Inpatient Bed Type

            var _inpatientBedTypeId = dbHelper.inpatientBedType.GetByName("Clinitron")[0];

            #endregion

            #region Rtt Treatment Function

            var rttTreatmentFunctionCodeId = dbHelper.rttTreatmentFunctionCode.GetByName("General Surgery Service")[0];

            #endregion

            #region Provider

            var _provider_Name = "RTT_" + _currentDateString;
            var _provider_HospitalId = commonMethodsDB.CreateProvider(_provider_Name, _teamId);
            dbHelper.provider.UpdateRTTTreatmentFunction(_provider_HospitalId, rttTreatmentFunctionCodeId);
            #endregion

            #region Ward

            var _inpatientWardName = "Ward_" + _currentDateString;
            var _inpatientWardId = dbHelper.inpatientWard.CreateInpatientWard(_teamId, _provider_HospitalId, _systemUserId, _wardSpecialityId, _inpatientWardName, new DateTime(2020, 1, 1));

            #endregion

            #region Bay/Room

            var _inpatientBayName = "Bay_" + _currentDateString;
            var _inpatientBayId = dbHelper.inpatientBay.CreateInpatientCaseBay(_teamId, _inpatientWardId, _inpatientBayName, 1, "4", "4", "4", 2);

            #endregion

            #region Bed

            var _inpatientBedId = dbHelper.inpatientBed.CreateInpatientBed(_teamId, "12665", "4", "4", _inpatientBayId, 1, _inpatientBedTypeId, "4");

            #endregion

            #region Contact Inpatient Admission Source

            var _inpatientAdmissionSourceId = commonMethodsDB.CreateInpatientAdmissionSource(_teamId, "Default Inpatient Admission Source", new DateTime(2020, 1, 1));

            #endregion

            #region Inpatient Admission Method

            var _inpationAdmissionMethodId = commonMethodsDB.CreateInpatientAdmissionMethod("Default Inpatient Admission Method", _teamId, _businessUnitId, new DateTime(2020, 1, 1));

            #endregion

            #region RTT Treatment Statuses

            Guid? rttTreatmentStatusId = null;

            #endregion

            #region RTT Pathway Setup

            Guid? rttPathwaySetupId = null;

            #endregion

            #region Case

            var rttReferral = 2; //No
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var _caseId = dbHelper.Case.CreateInpatientCaseRecordWithStatusAsAdmission(
                _teamId, _personID, currentDate, _systemUserId, "presenting needs ...",
                _systemUserId, _admission_CaseStatusId, _contactReasonId, currentDate, _dataFormId,
                _contactSourceId, _inpatientWardId, _inpatientBayId, _inpatientBedId, _inpatientAdmissionSourceId,
                _inpationAdmissionMethodId, _systemUserId, currentDate, _provider_HospitalId,
                _inpatientWardId, 1, currentDate,
                false, false, false, false, false, false, false, false, false, false,
                rttReferral, rttTreatmentStatusId, rttPathwaySetupId);

            var _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];

            #endregion

            #region Step 4

            loginPage
                .GoToLoginPage()
                .Login("RTTUser3", "Passw0rd_!", _environmentName)
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
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
            Assert.AreEqual(0, rttWaitTimeRecords.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-25218")]
        [Description("Step(s) 5 for the test CDV6-23644")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_RTT_UITestMethod04()
        {
            #region Import Workflows

            ImportRttWorkflows();

            #endregion

            #region Job Role Type

            var _jobRoleTypeId = commonMethodsDB.CreateJobRoleType(_teamId, "Default Consultant", new DateTime(2020, 1, 1), 1);
            dbHelper.jobRoleType.UpdateIsConsultantId(_jobRoleTypeId, true);

            #endregion

            #region Update System User Job Role Type

            dbHelper.systemUser.UpdateJobRoleType(_systemUserId, _jobRoleTypeId);

            #endregion

            #region Person

            var personFirstName = "Logan";
            var personLastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = "Logan " + personLastName;
            var _personID = commonMethodsDB.CreatePersonRecord(personFirstName, personLastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));

            #endregion

            #region Case Status
            var _admission_CaseStatusId = dbHelper.caseStatus.GetByName("Admission").FirstOrDefault();
            var _awaitingAdmission_caseStatusId = dbHelper.caseStatus.GetByName("Awaiting Admission").FirstOrDefault();

            #endregion

            #region Contact Reason

            var _contactReasonId = commonMethodsDB.CreateContactReason(_teamId, "Default Inpatient Management Contact Reason", new DateTime(2020, 1, 1), 140000001, 2, false);

            #endregion

            #region Contact Source

            var _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Default Contact Source", _teamId);

            #endregion

            #region Data Form

            var _dataFormId = dbHelper.dataForm.GetByName("InpatientCase")[0];

            #endregion

            #region Hospital Ward Specialty

            var _wardSpecialityId = dbHelper.inpatientWardSpecialty.GetByName("Adult Acute")[0];

            #endregion

            #region Inpatient Bed Type

            var _inpatientBedTypeId = dbHelper.inpatientBedType.GetByName("Clinitron")[0];

            #endregion

            #region Rtt Treatment Function

            var rttTreatmentFunctionCodeId = dbHelper.rttTreatmentFunctionCode.GetByName("General Surgery Service")[0];

            #endregion

            #region Provider

            var _provider_Name = "RTT_" + _currentDateString;
            var _provider_HospitalId = commonMethodsDB.CreateProvider(_provider_Name, _teamId);
            dbHelper.provider.UpdateRTTTreatmentFunction(_provider_HospitalId, rttTreatmentFunctionCodeId);

            var _provider2_Name = "RTT2_" + _currentDateString;
            var _provider2_HospitalId = commonMethodsDB.CreateProvider(_provider2_Name, _teamId);
            dbHelper.provider.UpdateRTTTreatmentFunction(_provider2_HospitalId, rttTreatmentFunctionCodeId);

            #endregion

            #region Ward

            var _inpatientWardName = "Ward_" + _currentDateString;
            var _inpatientWardId = dbHelper.inpatientWard.CreateInpatientWard(_teamId, _provider_HospitalId, _systemUserId, _wardSpecialityId, _inpatientWardName, new DateTime(2020, 1, 1));

            #endregion

            #region Bay/Room

            var _inpatientBayName = "Bay_" + _currentDateString;
            var _inpatientBayId = dbHelper.inpatientBay.CreateInpatientCaseBay(_teamId, _inpatientWardId, _inpatientBayName, 1, "4", "4", "4", 2);

            #endregion

            #region Bed

            var _inpatientBedId = dbHelper.inpatientBed.CreateInpatientBed(_teamId, "12665", "4", "4", _inpatientBayId, 1, _inpatientBedTypeId, "4");

            #endregion

            #region Contact Inpatient Admission Source

            var _inpatientAdmissionSourceId = commonMethodsDB.CreateInpatientAdmissionSource(_teamId, "Default Inpatient Admission Source", new DateTime(2020, 1, 1));

            #endregion

            #region Inpatient Admission Method

            var _inpationAdmissionMethodId = commonMethodsDB.CreateInpatientAdmissionMethod("Default Inpatient Admission Method", _teamId, _businessUnitId, new DateTime(2020, 1, 1));

            #endregion

            #region RTT Treatment Statuses

            var rttTreatmentStatusId = dbHelper.rttTreatmentStatus.GetByName("First Activity in new RTT period")[0];

            #endregion

            #region RTT Pathway Setup

            var rttPathwaySetupId = commonMethodsDB.CreateRTTPathwaySetup(_teamId, "Default RTT Pathway", new DateTime(2020, 1, 1), 2, 5, 10);

            #endregion

            #region Case

            var rttReferral = 3; //Transferred-In
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var _caseId = dbHelper.Case.CreateInpatientCaseRecordWithStatusAsAdmission(
                _teamId, _personID, currentDate, _systemUserId, "presenting needs ...",
                _systemUserId, _admission_CaseStatusId, _contactReasonId, currentDate, _dataFormId,
                _contactSourceId, _inpatientWardId, _inpatientBayId, _inpatientBedId, _inpatientAdmissionSourceId,
                _inpationAdmissionMethodId, _systemUserId, currentDate, _provider_HospitalId,
                _inpatientWardId, 1, currentDate,
                false, false, false, false, false, false, false, false, false, false,
                rttReferral, rttTreatmentStatusId, rttPathwaySetupId, _provider2_HospitalId, currentDate.AddDays(-1));

            var _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];

            #endregion

            #region Step 5

            loginPage
                .GoToLoginPage()
                .Login("RTTUser3", "Passw0rd_!", _environmentName)
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
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

            rttWaitTimesPage
                .OpenRecord(rttWaitTimeRecordId);

            rttWaitTimeRecordPage
                .WaitForRTTWaitTimeRecordPageToLoad()
                .NavigateToRttEventsSubPage();

            var rttEventsRecords = dbHelper.rttEvent.GetByRTTWaitTime(rttWaitTimeRecordId);
            Assert.AreEqual(1, rttEventsRecords.Count);
            var rttEventRecordId = rttEventsRecords.First();

            rttEventsPage
                .WaitForRTTEventsPageToLoad()
                .OpenRecord(rttEventRecordId);

            rttEventRecordPage
                .WaitForRTTEventRecordPageToLoad();

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

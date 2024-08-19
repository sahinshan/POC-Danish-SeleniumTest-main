using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Cases
{
    [DeploymentItem("Files\\RTT Event - Case Discharged.Zip"), DeploymentItem("Files\\RTT Event - Consultant Episode RTT Treatment Status.Zip"), DeploymentItem("Files\\RTT Event - Health Appointment Outcome.Zip"), DeploymentItem("Files\\RTT Event - New RTT Case.Zip"), DeploymentItem("chromedriver.exe")]
    [TestClass]
    public class Case_RTT_Field_UITestCases : FunctionalTest
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
        private Guid _personID;
        private string _personFullName;
        private Guid _contactReasonId_Investigative;
        private string _contactReasonName_Investigative;
        private Guid _contactReasonId_Treatment;
        private string _contactReasonName_Treatment;
        private Guid _contactSourceId;
        private Guid _inpatientAdmissionSourceId;
        private Guid _inpationAdmissionMethodId;
        private string _consultantUsername;
        private Guid _consultantUserId;
        private string _provider_Name;
        private Guid _provider_Id;
        private Guid _inpatientWardId;
        private Guid _inpatientBayRoomId;
        private Guid _inpatientBedId;
        private string _rttTreatmentStatusName;
        private Guid _rttTreatmentStatusId;
        private string _rttPathwaySetupName;
        private Guid _rttPathwaySetupId;
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

                _systemUsername = "RTTFieldUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "RTTField", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

                #region Consultant User (Sytem User)

                _consultantUsername = "ConsultantUser1" + _currentDateString;
                _consultantUserId = commonMethodsDB.CreateSystemUserRecord(_consultantUsername, "Consultant", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

                #region Job Role Type

                _jobRoleTypeId = commonMethodsDB.CreateJobRoleType(_teamId, "Default Consultant", new DateTime(2020, 1, 1), 1);
                dbHelper.jobRoleType.UpdateIsConsultantId(_jobRoleTypeId, true);

                #endregion

                #region Update System User Job Role Type

                dbHelper.systemUser.UpdateJobRoleType(_systemUserId, _jobRoleTypeId);
                dbHelper.systemUser.UpdateJobRoleType(_consultantUserId, _jobRoleTypeId);

                #endregion

                #region Person

                var personFirstName = "RTT_Field_Person";
                var personLastName = DateTime.Now.ToString("yyyyMMddHHmmss");
                _personFullName = "RTT_Field_Person " + personLastName;
                _personID = commonMethodsDB.CreatePersonRecord(personFirstName, personLastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));

                #endregion

                #region Case Status

                var _admission_CaseStatusId = dbHelper.caseStatus.GetByName("Admission").FirstOrDefault();
                var _awaitingAdmission_caseStatusId = dbHelper.caseStatus.GetByName("Awaiting Admission").FirstOrDefault();

                #endregion

                #region Contact Reason

                _contactReasonName_Investigative = "Contact_Reason_Investigative";
                _contactReasonId_Investigative = commonMethodsDB.CreateContactReason(_teamId, _contactReasonName_Investigative, new DateTime(2021, 1, 1), 140000001, 1, false);

                _contactReasonName_Treatment = "Contact_Reason_Treatment";
                _contactReasonId_Treatment = commonMethodsDB.CreateContactReason(_teamId, _contactReasonName_Treatment, new DateTime(2021, 1, 1), 140000001, 2, false);

                #endregion

                #region Contact Source

                _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Default Contact Source", _teamId);

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

                _provider_Name = "RTT_" + _currentDateString;
                _provider_Id = commonMethodsDB.CreateProvider(_provider_Name, _teamId);
                dbHelper.provider.UpdateRTTTreatmentFunction(_provider_Id, rttTreatmentFunctionCodeId);

                #endregion

                #region Ward

                var _inpatientWardName = "Ward_" + _currentDateString;
                _inpatientWardId = dbHelper.inpatientWard.CreateInpatientWard(_teamId, _provider_Id, _systemUserId, _wardSpecialityId, _inpatientWardName, new DateTime(2020, 1, 1));

                #endregion

                #region Bay/Room

                var _inpatientBayRoomName = "Bay_" + _currentDateString;
                _inpatientBayRoomId = dbHelper.inpatientBay.CreateInpatientCaseBay(_teamId, _inpatientWardId, _inpatientBayRoomName, 1, "4", "4", "4", 2);

                #endregion

                #region Bed

                _inpatientBedId = dbHelper.inpatientBed.CreateInpatientBed(_teamId, "12665", "4", "4", _inpatientBayRoomId, 1, _inpatientBedTypeId, "4");

                #endregion

                #region Contact Inpatient Admission Source

                _inpatientAdmissionSourceId = commonMethodsDB.CreateInpatientAdmissionSource(_teamId, "Default Inpatient Admission Source", new DateTime(2020, 1, 1));

                #endregion

                #region Inpatient Admission Method

                _inpationAdmissionMethodId = commonMethodsDB.CreateInpatientAdmissionMethod("Default Inpatient Admission Method", _teamId, _businessUnitId, new DateTime(2020, 1, 1));

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

                #region  RTT Treatment Statuses

                _rttTreatmentStatusName = "First Activity in new RTT period";
                _rttTreatmentStatusId = dbHelper.rttTreatmentStatus.GetByName(_rttTreatmentStatusName)[0];

                #endregion

                #region RTT Pathway Setup

                _rttPathwaySetupName = "Default RTT Pathway";
                _rttPathwaySetupId = commonMethodsDB.CreateRTTPathwaySetup(_teamId, _rttPathwaySetupName, new DateTime(2020, 1, 1), 2, 5, 10);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-2064

        [TestProperty("JiraIssueID", "ACC-2191")]
        [Description("Step(s) 1 to 5 for the test CDV6-23539")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_RTT_Field_UITestMethod01()
        {
            #region Step 1 & 2

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Contact Reasons")
                .TapSearchButton()
                .ClickReferenceDataMainHeader("Contact/Case")
                .ClickReferenceDataElement("Contact Reasons");

            contactReasonsPage
                .WaitForContactReasonsPageToLoad()
                .ClickNewRecordButton();

            contactReasonRecordPage
                .WaitForContactReasonRecordPageToLoad()
                .SelectBusinessType("Inpatient Management")
                .ValidateAdmissionTypeFieldVisibility(true)
                .ValidateAdmissionTypePicklist_FieldOptionIsPresent("Investigative")
                .ValidateAdmissionTypePicklist_FieldOptionIsPresent("Treatment");

            #endregion

            #region Step 3

            contactReasonRecordPage
                .ClickSaveButton()
                .ValidateNotificationAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateNameFieldErrorLabelText("Please fill out this field.")
                .ValidateAdmissionTypeFieldErrorLabelText("Please fill out this field.");

            #endregion

            #region Step 4 & 5

            contactReasonRecordPage
                .InsertTextOnName("Investigative_ACC_" + _currentDateString)
                .SelectAdmissionType("Investigative")
                .ClickSaveAndCloseButton();

            contactReasonsPage
                .WaitForContactReasonsPageToLoad()
                .ClickNewRecordButton();

            contactReasonRecordPage
                .WaitForContactReasonRecordPageToLoad()
                .InsertTextOnName("Treatment_ACC_" + _currentDateString)
                .SelectBusinessType("Inpatient Management")
                .SelectAdmissionType("Treatment")
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(2000);

            var _contactReasonId1 = dbHelper.contactReason.GetByName("Investigative_ACC_" + _currentDateString)[0];
            var _contactReasonId2 = dbHelper.contactReason.GetByName("Treatment_ACC_" + _currentDateString)[0];

            contactReasonsPage
                .WaitForContactReasonsPageToLoad()
                .InsertSearchQuery("Investigative_ACC_" + _currentDateString)
                .TapSearchButton()
                .ValidateRecordPresent(_contactReasonId1.ToString())

                .InsertSearchQuery("Treatment_ACC_" + _currentDateString)
                .TapSearchButton()
                .ValidateRecordPresent(_contactReasonId2.ToString());

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2192")]
        [Description("Step(s) 6 to 7 for the test CDV6-23539")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_RTT_Field_UITestMethod02()
        {
            DateTime CurrentDate = DateTime.Now;

            #region Step 6 & 7

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .ClickNewRecordButton();

            selectCaseTypePopUp.WaitForSelectCaseTypePopUpToLoad().SelectViewByText("Inpatient Case").TapNextButton();

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickPersonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("All Active People")
                .TypeSearchQuery(_personFullName)
                .TapSearchButton()
                .SelectResultElement(_personID.ToString());

            caseRecordPage
                .ClickContactReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_contactReasonName_Investigative)
                .TapSearchButton()
                .SelectResultElement(_contactReasonId_Investigative.ToString());

            System.Threading.Thread.Sleep(1000);

            caseRecordPage
                .ValidateRTTReferralFieldIsDisabled(true)
                .ValidateRTTReferralSelectedText("No")
                .InsertDateContactReceived(CurrentDate.ToString("dd'/'MM'/'yyyy"))
                .InsertTimeContactReceived("00:00")
                .ClickContactReceivedByLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_systemUsername)
                .TapSearchButton()
                .SelectResultElement(_systemUserId.ToString());

            caseRecordPage
                .ClickContactSourceLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Default Contact Source")
                .TapSearchButton()
                .SelectResultElement(_contactSourceId.ToString());

            caseRecordPage
                .ClickAdmissionSourceLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Default Inpatient Admission Source")
                .TapSearchButton()
                .SelectResultElement(_inpatientAdmissionSourceId.ToString());

            caseRecordPage
                .InsertOutlineNeedForAdmission("Automation")
                .ClickAdmissionMethodLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Default Inpatient Admission Method")
                .TapSearchButton()
                .SelectResultElement(_inpationAdmissionMethodId.ToString());

            caseRecordPage
                .InsertAdmissionTime(CurrentDate.ToString("dd'/'MM'/'yyyy"))
                .ClickCurrentConsultantLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_consultantUsername)
                .TapSearchButton()
                .SelectResultElement(_consultantUserId.ToString());

            caseRecordPage
                .ClickHospitalLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_provider_Name)
                .TapSearchButton()
                .SelectResultElement(_provider_Id.ToString());

            caseRecordPage
                .ClickResponsibleWardLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_inpatientWardId.ToString());

            caseRecordPage
                .ClickWardLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_inpatientWardId.ToString());

            caseRecordPage
                .ClickBayRoomLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_inpatientBayRoomId.ToString());

            caseRecordPage
                .ClickBedLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_inpatientBedId.ToString());

            caseRecordPage
                .ClickSaveButton();

            System.Threading.Thread.Sleep(3000);

            var cases = dbHelper.Case.GetCasesByPersonID(_personID);
            Assert.AreEqual(1, cases.Count);

            caseRecordPage
                .TapDetailsLink()
                .ValidateRTTTreatmentStatusFieldVisibility(false)
                .ValidateRTTPathwayFieldVisibility(false);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2193")]
        [Description("Step(s) 8 to 9 for the test CDV6-23539")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_RTT_Field_UITestMethod03()
        {
            DateTime CurrentDate = DateTime.Now;

            #region Step 8 & 9

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .ClickNewRecordButton();

            selectCaseTypePopUp.WaitForSelectCaseTypePopUpToLoad().SelectViewByText("Inpatient Case").TapNextButton();

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickPersonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("All Active People")
                .TypeSearchQuery(_personFullName)
                .TapSearchButton()
                .SelectResultElement(_personID.ToString());

            caseRecordPage
                .ClickContactReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_contactReasonName_Treatment)
                .TapSearchButton()
                .SelectResultElement(_contactReasonId_Treatment.ToString());

            System.Threading.Thread.Sleep(1000);

            caseRecordPage
                .ValidateRTTReferralFieldIsDisabled(false)
                .ValidateRTTReferralSelectedText("")
                .ValidateRTTReferralPicklist_FieldOptionIsPresent("Yes")
                .ValidateRTTReferralPicklist_FieldOptionIsPresent("No")
                .SelectRTTReferral("No")
                .InsertDateContactReceived(CurrentDate.ToString("dd'/'MM'/'yyyy"))
                .InsertTimeContactReceived("00:00")
                .ClickContactReceivedByLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_systemUsername)
                .TapSearchButton()
                .SelectResultElement(_systemUserId.ToString());

            caseRecordPage
                .ClickContactSourceLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Default Contact Source")
                .TapSearchButton()
                .SelectResultElement(_contactSourceId.ToString());

            caseRecordPage
                .ClickAdmissionSourceLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Default Inpatient Admission Source")
                .TapSearchButton()
                .SelectResultElement(_inpatientAdmissionSourceId.ToString());

            caseRecordPage
                .InsertOutlineNeedForAdmission("Automation")
                .ClickAdmissionMethodLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Default Inpatient Admission Method")
                .TapSearchButton()
                .SelectResultElement(_inpationAdmissionMethodId.ToString());

            caseRecordPage
                .InsertAdmissionTime(CurrentDate.ToString("dd'/'MM'/'yyyy"))
                .ClickCurrentConsultantLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_consultantUsername)
                .TapSearchButton()
                .SelectResultElement(_consultantUserId.ToString());

            caseRecordPage
                .ClickHospitalLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_provider_Name)
                .TapSearchButton()
                .SelectResultElement(_provider_Id.ToString());

            caseRecordPage
                .ClickResponsibleWardLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_inpatientWardId.ToString());

            caseRecordPage
                .ClickWardLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_inpatientWardId.ToString());

            caseRecordPage
                .ClickBayRoomLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_inpatientBayRoomId.ToString());

            caseRecordPage
                .ClickBedLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_inpatientBedId.ToString());

            caseRecordPage
                .ClickSaveButton();

            System.Threading.Thread.Sleep(3000);

            var cases = dbHelper.Case.GetCasesByPersonID(_personID);
            Assert.AreEqual(1, cases.Count);

            caseRecordPage
                .TapDetailsLink()
                .ValidateRTTTreatmentStatusFieldVisibility(false)
                .ValidateRTTPathwayFieldVisibility(false);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2194")]
        [Description("Step(s) 10 to 12 for the test CDV6-23539")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_RTT_Field_UITestMethod04()
        {
            DateTime CurrentDate = DateTime.Now;

            #region Step 10

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .ClickNewRecordButton();

            selectCaseTypePopUp.WaitForSelectCaseTypePopUpToLoad().SelectViewByText("Inpatient Case").TapNextButton();

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickPersonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("All Active People")
                .TypeSearchQuery(_personFullName)
                .TapSearchButton()
                .SelectResultElement(_personID.ToString());

            caseRecordPage
                .ClickContactReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_contactReasonName_Treatment)
                .TapSearchButton()
                .SelectResultElement(_contactReasonId_Treatment.ToString());

            System.Threading.Thread.Sleep(1000);

            caseRecordPage

                .InsertDateContactReceived(CurrentDate.ToString("dd'/'MM'/'yyyy"))
                .InsertTimeContactReceived("00:00")
                .ClickContactReceivedByLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_systemUsername)
                .TapSearchButton()
                .SelectResultElement(_systemUserId.ToString());

            caseRecordPage
                .ClickContactSourceLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Default Contact Source")
                .TapSearchButton()
                .SelectResultElement(_contactSourceId.ToString());

            caseRecordPage
                .ClickAdmissionSourceLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Default Inpatient Admission Source")
                .TapSearchButton()
                .SelectResultElement(_inpatientAdmissionSourceId.ToString());

            caseRecordPage
                .InsertOutlineNeedForAdmission("Automation")
                .ClickAdmissionMethodLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Default Inpatient Admission Method")
                .TapSearchButton()
                .SelectResultElement(_inpationAdmissionMethodId.ToString());

            caseRecordPage
                .InsertAdmissionTime(CurrentDate.ToString("dd'/'MM'/'yyyy"))
                .ClickCurrentConsultantLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_consultantUsername)
                .TapSearchButton()
                .SelectResultElement(_consultantUserId.ToString());

            caseRecordPage
                .ClickHospitalLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_provider_Name)
                .TapSearchButton()
                .SelectResultElement(_provider_Id.ToString());

            caseRecordPage
                .ClickResponsibleWardLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_inpatientWardId.ToString());

            caseRecordPage
                .ClickWardLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_inpatientWardId.ToString());

            caseRecordPage
                .ClickBayRoomLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_inpatientBayRoomId.ToString());

            caseRecordPage
                .ClickBedLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_inpatientBedId.ToString());

            caseRecordPage
                .SelectRTTReferral("Yes")
                .ValidateRTTTreatmentStatusFieldVisibility(true)
                .ValidateRTTPathwayFieldVisibility(true);

            #endregion

            #region Step 11

            caseRecordPage
                .ClickRTTTreatmentStatusLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_rttTreatmentStatusName)
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
                .ClickSaveButton();

            System.Threading.Thread.Sleep(3000);

            var cases = dbHelper.Case.GetCasesByPersonID(_personID);
            Assert.AreEqual(1, cases.Count);

            caseRecordPage
                .TapDetailsLink()
                .ValidateRTTTreatmentStatusFieldVisibility(true)
                .ValidateRTTPathwayFieldVisibility(true)
                .ValidateRTTReferralSelectedText("Yes")
                .ValidateRTTTreatmentStatusLinkText(_rttTreatmentStatusName)
                .ValidateRTTPathwayLinkText(_rttPathwaySetupName)
                .ValidateRTTReferralFieldIsDisabled(true)
                .ValidateRTTTreatmentStatusFieldIsDisabled(true)
                .ValidateRTTPathwayFieldIsDisabled(true);

            #endregion

            #region Step 12

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SelectSearchResultsDropDown("Admitted Inpatient Management Cases View");
            System.Threading.Thread.Sleep(2000);

            casesPage
                .ValidateHeaderRecordCellText(9, "RTT Referral")
                .SelectSearchResultsDropDown("Awaiting Admission Inpatient Management Cases View");

            System.Threading.Thread.Sleep(2000);

            casesPage
                .ValidateHeaderRecordCellText(9, "RTT Referral")
                .SelectSearchResultsDropDown("Discharge Awaiting Closure Inpatient Management Cases View");

            System.Threading.Thread.Sleep(2000);

            casesPage
                .ValidateHeaderRecordCellText(10, "RTT Referral");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2195")]
        [Description("Step(s) 13 to 14 for the test CDV6-23539")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_RTT_Field_UITestMethod05()
        {
            #region Step 13 & 14

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Contact Reasons")
                .TapSearchButton()
                .ClickReferenceDataMainHeader("Contact/Case")
                .ClickReferenceDataElement("Contact Reasons");

            contactReasonsPage
                .WaitForContactReasonsPageToLoad()
                .ValidateHeaderCellText(6, "Admission Type");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Cases")
                .ClickSelectFilterFieldOption("1")
                .ValidateSelectFilterFieldOptionIsPresent("1", "RTT Referral")
                .ValidateSelectFilterFieldOptionIsPresent("1", "RTT Treatment Status")
                .ValidateSelectFilterFieldOptionIsPresent("1", "RTT Pathway");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2196")]
        [Description("Step(s) 15 to 18 for the test CDV6-23539")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_RTT_Field_UITestMethod06()
        {
            DateTime CurrentDate = DateTime.Now;
            DateTime PastDate = DateTime.Now.Date.AddDays(-1);

            #region Step 15

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .ClickNewRecordButton();

            selectCaseTypePopUp.WaitForSelectCaseTypePopUpToLoad().SelectViewByText("Inpatient Case").TapNextButton();

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickPersonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("All Active People")
                .TypeSearchQuery(_personFullName)
                .TapSearchButton()
                .SelectResultElement(_personID.ToString());

            caseRecordPage
                .ClickContactReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_contactReasonName_Treatment)
                .TapSearchButton()
                .SelectResultElement(_contactReasonId_Treatment.ToString());

            System.Threading.Thread.Sleep(1000);

            caseRecordPage
                .InsertDateContactReceived(CurrentDate.ToString("dd'/'MM'/'yyyy"))
                .InsertTimeContactReceived("00:00")
                .ClickContactReceivedByLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_systemUsername)
                .TapSearchButton()
                .SelectResultElement(_systemUserId.ToString());

            caseRecordPage
                .ClickContactSourceLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Default Contact Source")
                .TapSearchButton()
                .SelectResultElement(_contactSourceId.ToString());

            caseRecordPage
                .ClickAdmissionSourceLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Default Inpatient Admission Source")
                .TapSearchButton()
                .SelectResultElement(_inpatientAdmissionSourceId.ToString());

            caseRecordPage
                .InsertOutlineNeedForAdmission("Automation")
                .ClickAdmissionMethodLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Default Inpatient Admission Method")
                .TapSearchButton()
                .SelectResultElement(_inpationAdmissionMethodId.ToString());

            caseRecordPage
                .InsertAdmissionTime(CurrentDate.ToString("dd'/'MM'/'yyyy"))
                .ClickCurrentConsultantLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_consultantUsername)
                .TapSearchButton()
                .SelectResultElement(_consultantUserId.ToString());

            caseRecordPage
                .ClickHospitalLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_provider_Name)
                .TapSearchButton()
                .SelectResultElement(_provider_Id.ToString());

            caseRecordPage
                .ClickResponsibleWardLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_inpatientWardId.ToString());

            caseRecordPage
                .ClickWardLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_inpatientWardId.ToString());

            caseRecordPage
                .ClickBayRoomLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_inpatientBayRoomId.ToString());

            caseRecordPage
                .ClickBedLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_inpatientBedId.ToString());

            caseRecordPage
                .SelectRTTReferral("Transferred-In")
                .ValidateRTTTreatmentStatusFieldVisibility(true)
                .ValidateRTTPathwayFieldVisibility(true)
                .ValidateTransferredFromProviderFieldVisibility(true)
                .ValidateOriginalRTTReferralStartDateFieldVisibility(true);

            #endregion

            #region Step 16

            caseRecordPage
                .ValidateRTTTreatmentStatus_MandatoryField(true)
                .ValidateRTTPathway_MandatoryField(true)
                .ValidateTransferredFromProvider_MandatoryField(true)
                .ValidateOriginalRTTReferralStartDate_MandatoryField(true);

            caseRecordPage
                .ClickRTTTreatmentStatusLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_rttTreatmentStatusName)
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
                .SelectResultElement(_provider_Id.ToString());

            caseRecordPage
                .InsertOriginalRTTReferralStartDate(PastDate.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton();

            caseRecordPage
                .WaitForCaseRecordToSaved();

            var cases = dbHelper.Case.GetCasesByPersonID(_personID);
            Assert.AreEqual(1, cases.Count);
            var caseTitle = (string)dbHelper.Case.GetCaseByID(cases[0], "title")["title"];

            caseRecordPage
                .ValidateCaseRecordTitle(caseTitle);

            #endregion

            #region Step 17

            caseRecordPage
                .TapDetailsLink()
                .ValidateRTTTreatmentStatusFieldVisibility(true)
                .ValidateRTTPathwayFieldVisibility(true)
                .ValidateRTTReferralSelectedText("Transferred-In")
                .ValidateRTTTreatmentStatusLinkText(_rttTreatmentStatusName)
                .ValidateRTTPathwayLinkText(_rttPathwaySetupName)
                .ValidateTransferredFromProviderLinkText(_provider_Name)
                .ValidateOriginalRTTReferralStartDateFieldValue(PastDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateTransferredFromProviderLookupIsDisabled(false)
                .ValidateOriginalRTTReferralStartDateFieldIsDisabled(true);

            #endregion

            #region Step 18

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

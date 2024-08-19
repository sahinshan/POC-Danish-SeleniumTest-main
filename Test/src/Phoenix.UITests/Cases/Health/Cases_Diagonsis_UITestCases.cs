using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People.Health
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    /// 
    [TestClass]
    public class Case_Diagnosis_UITestCases : FunctionalTest
    {
        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _AutomationDiagnosisUser1_SystemUserId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _contactReasonId;
        private Guid _contactSourceId;
        private Guid _personID;
        private string _personFullName;
        private string _personFirstName;
        private string _personLastName;
        private int _personNumber;
        private Guid _caseId;
        private string _caseNumber;
        private string _caseTitle;
        private Guid _dataFormId;
        private Guid _inpatientAdmissionSourceId;
        private Guid _provider_HospitalId;
        private Guid _inpationAdmissionMethodId;
        private Guid _wardSpecialityId;
        private Guid _inpationWardId;
        private Guid _inpationBayId;
        private Guid _inpationBedId;
        private Guid _inpatientBedTypeId;
        private Guid _diagnosisId;
        private Guid _personDiagnosisEndReasonId;


        [TestInitialize()]
        public void TestInitializationMethod()
        {
            try
            {
                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);

                #endregion

                #region Language

                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
                if (!language)
                    dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
                _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

                #endregion 

                #region Business Unit

                var businessUnitExists = dbHelper.businessUnit.GetByName("CareDirector Diagnosis BU").Any();
                if (!businessUnitExists)
                    dbHelper.businessUnit.CreateBusinessUnit("CareDirector Diagnosis BU");
                _businessUnitId = dbHelper.businessUnit.GetByName("CareDirector Diagnosis BU")[0];

                #endregion 

                #region Team

                var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector Diagnosis Team").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CareDirector Diagnosis Team", null, _businessUnitId, "907678", "CareDirectorDiagnosisTeam@careworkstempmail.com", "CareDirector Diagnosis Team", "020 123456");
                _teamId = dbHelper.team.GetTeamIdByName("CareDirector Diagnosis Team")[0];

                #endregion 

                #region System User CaseDiagnosisUser1

                commonMethodsDB.CreateSystemUserRecord("CaseDiagnosisUser1", "CaseDiagnosis", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

                #region System User

                var automationSmokeTestUser1Exists = dbHelper.systemUser.GetSystemUserByUserName("Automation_Diagnosis_Test_User_1").Any();
                if (!automationSmokeTestUser1Exists)
                {
                    _AutomationDiagnosisUser1_SystemUserId = dbHelper.systemUser.CreateSystemUser("Automation_Diagnosis_Test_User_1", "Automation", " Diagnosis Test User 1", "Automation Diagnosis Test User 1", "Passw0rd_!", "Automation_Diagnosis_Test_User_1@somemail.com", "Automation_Diagnosis_Test_User_1@secureemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _businessUnitId, _teamId, DateTime.Now.Date);

                    var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();

                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_AutomationDiagnosisUser1_SystemUserId, systemAdministratorSecurityProfileId);
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_AutomationDiagnosisUser1_SystemUserId, systemUserSecureFieldsSecurityProfileId);
                }
                if (_AutomationDiagnosisUser1_SystemUserId == Guid.Empty)
                {
                    _AutomationDiagnosisUser1_SystemUserId = dbHelper.systemUser.GetSystemUserByUserName("Automation_Diagnosis_Test_User_1").FirstOrDefault();
                }

                #endregion 

                #region Ethnicity

                var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("Diagnosis_Ethnicity").Any();
                if (!ethnicitiesExist)
                    dbHelper.ethnicity.CreateEthnicity(_teamId, "Diagnosis_Ethnicity", new DateTime(2020, 1, 1));
                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Diagnosis_Ethnicity")[0];

                #endregion 

                #region Contact Reason

                var contactReasonExists = dbHelper.contactReason.GetByName("ConsultantEpisode_ContactReason").Any();
                if (!contactReasonExists)
                    dbHelper.contactReason.CreateContactReason(_teamId, "ConsultantEpisode_ContactReason", new DateTime(2020, 1, 1), 140000001, false);
                _contactReasonId = dbHelper.contactReason.GetByName("ConsultantEpisode_ContactReason")[0];

                #endregion 

                #region Contact Source

                var contactSourceExists = dbHelper.contactSource.GetByName("ConsultantEpisode_ContactSource").Any();
                if (!contactSourceExists)
                    dbHelper.contactSource.CreateContactSource(_teamId, "ConsultantEpisode_ContactSource", new DateTime(2020, 1, 1));
                _contactSourceId = dbHelper.contactSource.GetByName("ConsultantEpisode_ContactSource")[0];

                #endregion 

                #region Contact Inpatient Admission Source

                var inpatientAdmissionSourceExists = dbHelper.inpatientAdmissionSource.GetByName("ConsultantEpisode_InpatientAdmissionSource").Any();
                if (!inpatientAdmissionSourceExists)
                    dbHelper.inpatientAdmissionSource.CreateInpatientAdmissionSource(_teamId, "ConsultantEpisode_InpatientAdmissionSource", new DateTime(2020, 1, 1));
                _inpatientAdmissionSourceId = dbHelper.inpatientAdmissionSource.GetByName("ConsultantEpisode_InpatientAdmissionSource")[0];

                #endregion 

                #region Provider_Hospital

                var providerName = "Automation_Provider_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                _provider_HospitalId = commonMethodsDB.CreateProvider(providerName, _teamId);

                #endregion 

                #region Inpatient Ward Speciality

                _wardSpecialityId = commonMethodsDB.CreateInpatientWardSpecialty("Adult Acute", new DateTime(2022, 1, 1), _teamId);

                #endregion

                #region Ward

                var inpatientWardExists = dbHelper.inpatientWard.GetInpatientWardByTitle("Automation_Ward_Testing", _provider_HospitalId).Any();
                if (!inpatientWardExists)
                    dbHelper.inpatientWard.CreateInpatientWard(_teamId, _provider_HospitalId, _AutomationDiagnosisUser1_SystemUserId, _wardSpecialityId, "Automation_Ward_Testing", new DateTime(2020, 1, 1));
                _inpationWardId = dbHelper.inpatientWard.GetInpatientWardByTitle("Automation_Ward_Testing", _provider_HospitalId)[0];

                #endregion 

                #region Bay/Room

                var inpatientBayRoomExists = dbHelper.inpatientBay.GetInpatientBayByName("Automation_Bay_Testing", _inpationWardId).Any();
                if (!inpatientBayRoomExists)
                    dbHelper.inpatientBay.CreateInpatientCaseBay(_teamId, _inpationWardId, "Automation_Bay_Testing", 1, "5", "5", "5", 2);
                _inpationBayId = dbHelper.inpatientBay.GetInpatientBayByName("Automation_Bay_Testing", _inpationWardId)[0];

                #endregion 

                #region Bed Type

                _inpatientBedTypeId = commonMethodsDB.CreateInpatientBedType("Clinitron", new DateTime(2022, 1, 1), _teamId);

                #endregion

                #region Bed

                var inpatientBedExists = dbHelper.inpatientBed.GetInpatientBedByInpatientBayId(_inpationBayId).Any();
                if (!inpatientBedExists)
                    dbHelper.inpatientBed.CreateInpatientBed(_teamId, "12345", "5", "5", _inpationBayId, 1, _inpatientBedTypeId, "5");

                _inpationBedId = dbHelper.inpatientBed.GetInpatientBedByInpatientBayId(_inpationBayId)[0];

                #endregion 

                #region InpatientAdmissionMethod

                var inpatientAdmissionMethodExists = dbHelper.inpatientAdmissionMethod.GetAdmissionMethodByName("Automation_Admission").Any();
                if (!inpatientAdmissionMethodExists)
                    dbHelper.inpatientAdmissionMethod.CreateAdmissionMethod("Automation_Admission", _teamId, _businessUnitId, new DateTime(2020, 1, 1));
                _inpationAdmissionMethodId = dbHelper.inpatientAdmissionMethod.GetAdmissionMethodByName("Automation_Admission")[0];

                #endregion 

                #region Person

                _personFirstName = "Automation_Diagnosis_Person";
                _personLastName = DateTime.Now.ToString("yyyyMMddHHmmss");
                _personFullName = _personFirstName + " " + _personLastName;
                _personID = commonMethodsDB.CreatePersonRecord(_personFirstName, _personLastName, _ethnicityId, _teamId);
                _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

                #endregion 

                #region Diagnosis

                var diagnosisExists = dbHelper.diagnosis.GetByName("AutomationDiagnosis_Testing").Any();
                if (!diagnosisExists)
                    dbHelper.diagnosis.CreateDiagnosis(_teamId, "AutomationDiagnosis_Testing", DateTime.Now.AddDays(-20).Date, "AutomationDiagnosis");
                _diagnosisId = dbHelper.diagnosis.GetByName("AutomationDiagnosis_Testing")[0];

                #endregion 

                #region PersonDiagnosisEndReason

                var personDiagnosisEndReasonExists = dbHelper.personDiagnosisEndReason.GetByName("AutomationDiagnosisEndReason_Testing").Any();
                if (!personDiagnosisEndReasonExists)
                    dbHelper.personDiagnosisEndReason.CreatePersonDiagnosisEndReason(_teamId, "AutomationDiagnosisEndReason_Testing", DateTime.Now.Date);
                _personDiagnosisEndReasonId = dbHelper.personDiagnosisEndReason.GetByName("AutomationDiagnosisEndReason_Testing")[0];

                #endregion 

                #region Data Form

                _dataFormId = dbHelper.dataForm.GetByName("InpatientCase")[0];

                #endregion

                #region Case

                _caseId = dbHelper.Case.CreateInpatientCaseRecordWithStatusAsAdmission(_teamId, _personID, DateTime.Now.Date, _AutomationDiagnosisUser1_SystemUserId, "hdsa", _AutomationDiagnosisUser1_SystemUserId, null, _contactReasonId, DateTime.Now.Date, _dataFormId, _contactSourceId, _inpationWardId, _inpationBayId, _inpationBedId, _inpatientAdmissionSourceId, _inpationAdmissionMethodId, _AutomationDiagnosisUser1_SystemUserId, DateTime.Now.Date, _provider_HospitalId, _inpationWardId, 1, DateTime.Now.Date, false, false, false, false, false, false, false, false, false, false);
                var caseFields = dbHelper.Case.GetCaseByID(_caseId, "casenumber", "title");
                _caseNumber = (string)(caseFields["casenumber"]);
                _caseTitle = (string)(caseFields["title"]);

                #endregion 


                ////By default activate SNOMED
                //var snomedBusinessModuleId = dbHelper.businessModule.GetBusinessModuleByName("SNOMED").FirstOrDefault();
                //bool moduleInactive = (bool)dbHelper.businessModule.GetBusinessModuleByID(snomedBusinessModuleId, "inactive")["inactive"];
                //if (moduleInactive)
                //    dbHelper.businessModule.ActivateModule(snomedBusinessModuleId);
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                this.ShutDownAllProcesses();

                throw;
            }
        }

        #region Test methods

        [TestProperty("JiraIssueID", "CDV6-14782")]
        [Description("Navigate to People + open person Record +Navigate to Case Section + Select Inpatient Case Recor " +
                     "Navigete to Health+ select Diagnosis and try to save the record without entering any Fields" +
                     "Validate Notification Error Message is Displayed")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_Diagnosis_UITestCases01()
        {

            loginPage
                .GoToLoginPage()
                .Login("CaseDiagnosisUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
               WaitForPeoplePageToLoad()
               .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
               .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .NavigateToDiagnosisPage();

            personHealthDiagnosesPage
                 .WaitForPersonHealthDiagnosesPageToLoad()
                 .ClickNewRecordButton();

            PersonHealthDiagnosisRecordPage
                .WaitForPersonHealthDiagnosisRecordPageToLoad("New")
                .ClickSaveButton()
                .ValidateNotificationErrorMessage("Some data is not correct. Please review the data in the Form.");

        }

        [TestProperty("JiraIssueID", "CDV6-14783")]
        [Description("Navigate to People + open person Record +Navigate to Case Section + Select Inpatient Case Recor " +
                    "Navigete to Health->Diagnosis+Create the Record with Primary or Secondary Field='Primary' + Provisional or Confirmed='Provisional'+Source of Diagnosis='Practitioner Diagnosis' save the record'" +
                     "Validate Diagnosis Record is Created Even though Sonmed CT Field is Disabled")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_Diagnosis_UITestCases02()
        {

            ////for this test deactivate SNOMED
            //var snomedBusinessModuleId = dbHelper.businessModule.GetBusinessModuleByName("SNOMED").FirstOrDefault();
            //bool moduleInactive = (bool)dbHelper.businessModule.GetBusinessModuleByID(snomedBusinessModuleId, "inactive")["inactive"];
            //if (!moduleInactive)
            //    dbHelper.businessModule.DeactivateModule(snomedBusinessModuleId);

            loginPage
                .GoToLoginPage()
                .Login("CaseDiagnosisUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
               WaitForPeoplePageToLoad()
               .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
               .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .NavigateToDiagnosisPage();

            personHealthDiagnosesPage
                 .WaitForPersonHealthDiagnosesPageToLoad()
                 .ClickNewRecordButton();

            var InpatientonsultantEpisodeRecords = dbHelper.inpatientConsultantEpisode.GetByPersonId(_personID);
            Assert.AreEqual(1, InpatientonsultantEpisodeRecords.Count);

            PersonHealthDiagnosisRecordPage
                .WaitForPersonHealthDiagnosisRecordPageToLoad("New")
                .SelectSourceOfDiagnosis("Practitioner diagnosis")
                .ClickConsultantEpisodeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Automation_Diagnosis_Person AutomationDiagnosisLastName")
                .TapSearchButton()
                .SelectResultElement(InpatientonsultantEpisodeRecords[0].ToString());

            PersonHealthDiagnosisRecordPage
               .WaitForPersonHealthDiagnosisRecordPageToLoad("New")
               .ValidateSnomedCTFieldVisible(false)
               .SelectProvisionalOrConfirmedid("Provisional")
               .SelectPrimaryOrSecondaryid("Primary")
               .SelectisPersonAwerOfDiagnosis("Yes")
               .InsertDiagnosisDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .ClickDiagnosisLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("AutomationDiagnosis_Testing")
                .TapSearchButton()
                .SelectResultElement(_diagnosisId.ToString());

            PersonHealthDiagnosisRecordPage
               .WaitForPersonHealthDiagnosisRecordPageToLoad("New")
               .SelectAsteriskorDaggerid("1")
               .ClickSaveButton();

            System.Threading.Thread.Sleep(3000);

            var personDiagnosisRecords = dbHelper.personDiagnosis.GetByPersonId(_personID);
            Assert.AreEqual(1, personDiagnosisRecords.Count);

            PersonHealthDiagnosisRecordPage
               .WaitForPersonHealthDiagnosisRecordPageToLoad("Diagnosis for " + _personFullName + " created by CaseDiagnosis User1")
               .ValidateSourceOfDiagnosisField("2")
               .ValidateConsultantEpisodeField("Consultant Episode within Case " + _caseTitle)
               .ValidateDiagnosisField("AutomationDiagnosis_Testing")
               .ValidateAsteriskOrDaggerField("1")
               .ValidateProvisionalOrConfirmedField("1")
               .ValidateisPersonAwerOfDiagnosisField("1")
               .ValidatePrimaryOrSecondaryField("1")
               .ValidateDiagnosisDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));
        }

        [TestProperty("JiraIssueID", "CDV6-14784")]
        [Description("Navigate to People + open person Record +Navigate to Case Section + Select Inpatient Case Recor " +
                    "Navigete to Health->Diagnosis+Create the Record with Primary or Secondary Field='Secondary and try to save the record'" +
                    "Validate Alert Message is displayed(A Secondary Diagnosis record cannot be created due to no Primary Diagnosis existing for this Consultant Episode)")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_Diagnosis_UITestCases03()
        {

            loginPage
                .GoToLoginPage()
                .Login("CaseDiagnosisUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
               WaitForPeoplePageToLoad()
               .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
               .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .NavigateToDiagnosisPage();

            personHealthDiagnosesPage
                 .WaitForPersonHealthDiagnosesPageToLoad()
                 .ClickNewRecordButton();

            var InpatientonsultantEpisodeRecords = dbHelper.inpatientConsultantEpisode.GetByPersonId(_personID);
            Assert.AreEqual(1, InpatientonsultantEpisodeRecords.Count);

            PersonHealthDiagnosisRecordPage
                .WaitForPersonHealthDiagnosisRecordPageToLoad("New")
                .SelectSourceOfDiagnosis("Practitioner diagnosis")
                .ClickConsultantEpisodeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Automation_Diagnosis_Person AutomationDiagnosisLastName")
                .TapSearchButton()
                .SelectResultElement(InpatientonsultantEpisodeRecords[0].ToString());

            PersonHealthDiagnosisRecordPage
               .WaitForPersonHealthDiagnosisRecordPageToLoad("New")
               .SelectAsteriskorDaggerid("1")
               .SelectProvisionalOrConfirmedid("Provisional")
               .SelectPrimaryOrSecondaryid("Secondary")
               .SelectisPersonAwerOfDiagnosis("Yes")
               .InsertDiagnosisDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .ClickDiagnosisLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("AutomationDiagnosis_Testing")
                .TapSearchButton()
                .SelectResultElement(_diagnosisId.ToString());

            PersonHealthDiagnosisRecordPage
               .WaitForPersonHealthDiagnosisRecordPageToLoad("New")
               .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("A Secondary Diagnosis record cannot be created due to no Primary Diagnosis existing for this Consultant Episode")
                .TapCloseButton();

        }

        [TestProperty("JiraIssueID", "CDV6-14785")]
        [Description("Navigate to People + open person Record +Navigate to Case Section + Select Inpatient Case Recor " +
                   "Navigete to Health->Diagnosis+Create the Record with source of Diagnosis Field='Clinical Coder diagnosis'" +
                   "Validate Professional/Coder who Confirmed Diagnosis Field is Visible")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_Diagnosis_UITestCases04()
        {

            loginPage
                .GoToLoginPage()
                .Login("CaseDiagnosisUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
               WaitForPeoplePageToLoad()
               .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
               .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .NavigateToDiagnosisPage();

            personHealthDiagnosesPage
                 .WaitForPersonHealthDiagnosesPageToLoad()
                 .ClickNewRecordButton();

            var InpatientonsultantEpisodeRecords = dbHelper.inpatientConsultantEpisode.GetByPersonId(_personID);
            Assert.AreEqual(1, InpatientonsultantEpisodeRecords.Count);

            PersonHealthDiagnosisRecordPage
                .WaitForPersonHealthDiagnosisRecordPageToLoad("New")
                .ValidateProfessionalCoderConfirmedDiagnosisFieldVisible(false)
                .SelectSourceOfDiagnosis("Clinical Coder diagnosis")
                .WaitForPersonHealthDiagnosisRecordPageToLoad("New")
                .ValidateProfessionalCoderConfirmedDiagnosisFieldVisible(true);

        }

        [TestProperty("JiraIssueID", "CDV6-14786")]
        [Description("Navigate to People + open person Record +Navigate to Case Section + Select Inpatient Case Recor " +
                    "Navigete to Health->Diagnosis+Create the Record with Coding Schema='ICD-10' and then Fill all the mandatory Fields hit save record Button'" +
                    "Validate ICD-10 field is Visbile and Record is Created Upon Filling all the Mandatory Fields")]
        [TestMethod, TestCategory("UITest"), Ignore()]
        public void Case_Diagnosis_UITestCases05()
        {


            loginPage
                .GoToLoginPage()
                .Login("CaseDiagnosisUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
               WaitForPeoplePageToLoad()
               .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
               .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .NavigateToDiagnosisPage();

            personHealthDiagnosesPage
                 .WaitForPersonHealthDiagnosesPageToLoad()
                 .ClickNewRecordButton();

            var InpatientonsultantEpisodeRecords = dbHelper.inpatientConsultantEpisode.GetByPersonId(_personID);
            Assert.AreEqual(1, InpatientonsultantEpisodeRecords.Count);

            PersonHealthDiagnosisRecordPage
                .WaitForPersonHealthDiagnosisRecordPageToLoad("New")
                .SelectSourceOfDiagnosis("Practitioner diagnosis")
                .ClickConsultantEpisodeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Automation_Diagnosis_Person AutomationDiagnosisLastName")
                .TapSearchButton()
                .SelectResultElement(InpatientonsultantEpisodeRecords[0].ToString());

            PersonHealthDiagnosisRecordPage
               .WaitForPersonHealthDiagnosisRecordPageToLoad("New")
               .SelectAsteriskorDaggerid("1")
               .SelectProvisionalOrConfirmedid("Provisional")
               .SelectPrimaryOrSecondaryid("Primary")
               .SelectisPersonAwerOfDiagnosis("Yes")
               .InsertDiagnosisDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .SelectCodingSchemaField("ICD-10")
               .ValidateDiagnosisICD10FieldVisible(true)
               .ClickDiagnosisLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("AutomationDiagnosis_Testing")
                .TapSearchButton()
                .SelectResultElement(_diagnosisId.ToString());

            PersonHealthDiagnosisRecordPage
               .WaitForPersonHealthDiagnosisRecordPageToLoad("New")
               .ClickSaveButton();

            System.Threading.Thread.Sleep(3000);

            var personDiagnosisRecords = dbHelper.personDiagnosis.GetByPersonId(_personID);
            Assert.AreEqual(1, personDiagnosisRecords.Count);


        }

        [TestProperty("JiraIssueID", "CDV6-14876")]
        [Description("Navigate to People + open person Record +Navigate to Case Section + Select Inpatient Case Recor " +
                   "Navigete to Health->Diagnosis+Create the Record with Coding Schema='Snomed CT' and then Fill all the mandatory Fields hit save record Button'" +
                   "Validate Snomed CT field is Visbile and Record is Created Upon Filling all the Mandatory Fields")]
        [TestMethod, TestCategory("UITest"), Ignore()]
        public void Case_Diagnosis_UITestCases06()
        {

            string termid = "771469002";

            loginPage
                .GoToLoginPage()
                .Login("CaseDiagnosisUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
               WaitForPeoplePageToLoad()
               .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
               .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .NavigateToDiagnosisPage();

            personHealthDiagnosesPage
                 .WaitForPersonHealthDiagnosesPageToLoad()
                 .ClickNewRecordButton();

            var InpatientonsultantEpisodeRecords = dbHelper.inpatientConsultantEpisode.GetByPersonId(_personID);
            Assert.AreEqual(1, InpatientonsultantEpisodeRecords.Count);

            PersonHealthDiagnosisRecordPage
                .WaitForPersonHealthDiagnosisRecordPageToLoad("New")
                .SelectSourceOfDiagnosis("Practitioner diagnosis")
                .ClickConsultantEpisodeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Automation_Diagnosis_Person AutomationDiagnosisLastName")
                .TapSearchButton()
                .SelectResultElement(InpatientonsultantEpisodeRecords[0].ToString());

            PersonHealthDiagnosisRecordPage
               .WaitForPersonHealthDiagnosisRecordPageToLoad("New")
               .SelectAsteriskorDaggerid("1")
               .SelectProvisionalOrConfirmedid("Provisional")
               .SelectPrimaryOrSecondaryid("Primary")
               .SelectisPersonAwerOfDiagnosis("Yes")
               .SelectCodingSchemaField("SNOMED CT")
               .InsertDiagnosisDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));


            System.Threading.Thread.Sleep(3000);

            PersonHealthDiagnosisRecordPage
              .WaitForPersonHealthDiagnosisRecordPageToLoad("New")
              //.ValidateSnomedCTFieldVisible(true)
              .ClickSomedCtLookupButton();

            System.Threading.Thread.Sleep(5000);

            snomedLookupPopup
                .WaitForSnomedLookupPopupToLoad()
                .InsertTerm("Early-onset spastic ataxia, myoclonic epilepsy, neuropathy syndrome")
                .TapTermSearchButton()
                .SelectResultElement(termid.ToString());

            PersonHealthDiagnosisRecordPage
               .WaitForPersonHealthDiagnosisRecordPageToLoad("New")
               .ClickSaveButton();

            System.Threading.Thread.Sleep(3000);

            var personDiagnosisRecords = dbHelper.personDiagnosis.GetByPersonId(_personID);
            Assert.AreEqual(1, personDiagnosisRecords.Count);


        }

        [TestProperty("JiraIssueID", "CDV6-14877")]
        [Description("Navigate to People + open person Record +Navigate to Case Section + Select Inpatient Case Recor " +
                     "Navigete to Health->Diagnosis+Create the Diagnosis Record+Update the Record with Primary or Secondary Field='Primary' + Provisional or Confirmed='Provisional'+Source of Diagnosis='Clinical Coder diagnosis' save the record'" +
                     "Validate Confirmation Error Message is displaye('Confirmed must be selected when Source of Diagnosis is 'Clinical Coder diagnosis'.')" +
                      "Update the Record With Source of Diagnosis='Practitioner diagnosis' and then hit hte save button" +
                     "Validate System Allows the uswwr to update the source of Diagnosis Field")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_Diagnosis_UITestCases07()
        {

            loginPage
                .GoToLoginPage()
                .Login("CaseDiagnosisUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
               WaitForPeoplePageToLoad()
               .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
               .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .NavigateToDiagnosisPage();

            personHealthDiagnosesPage
                 .WaitForPersonHealthDiagnosesPageToLoad()
                 .ClickNewRecordButton();

            var InpatientonsultantEpisodeRecords = dbHelper.inpatientConsultantEpisode.GetByPersonId(_personID);
            Assert.AreEqual(1, InpatientonsultantEpisodeRecords.Count);

            PersonHealthDiagnosisRecordPage
                .WaitForPersonHealthDiagnosisRecordPageToLoad("New")
                .SelectSourceOfDiagnosis("Practitioner diagnosis")
                .ClickConsultantEpisodeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Automation_Diagnosis_Person AutomationDiagnosisLastName")
                .TapSearchButton()
                .SelectResultElement(InpatientonsultantEpisodeRecords[0].ToString());

            PersonHealthDiagnosisRecordPage
               .WaitForPersonHealthDiagnosisRecordPageToLoad("New")
               .SelectAsteriskorDaggerid("1")
               .SelectProvisionalOrConfirmedid("Provisional")
               .SelectPrimaryOrSecondaryid("Primary")
               .SelectisPersonAwerOfDiagnosis("Yes")
               .InsertDiagnosisDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .ClickDiagnosisLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("AutomationDiagnosis_Testing")
                .TapSearchButton()
                .SelectResultElement(_diagnosisId.ToString());

            PersonHealthDiagnosisRecordPage
               .WaitForPersonHealthDiagnosisRecordPageToLoad("New")
               .ClickSaveButton();

            System.Threading.Thread.Sleep(3000);

            var personDiagnosisRecords = dbHelper.personDiagnosis.GetByPersonId(_personID);
            Assert.AreEqual(1, personDiagnosisRecords.Count);

            PersonHealthDiagnosisRecordPage
               .WaitForPersonHealthDiagnosisRecordPageToLoad("Diagnosis for " + _personFullName + " created by CaseDiagnosis User1")
               .SelectSourceOfDiagnosis("Clinical Coder diagnosis")
               .SelectProvisionalOrConfirmedid("Provisional")
               .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Confirmed must be selected when Source of Diagnosis is 'Clinical Coder diagnosis'.")
                .TapCloseButton();

            PersonHealthDiagnosisRecordPage
              .WaitForPersonHealthDiagnosisRecordPageToLoad("Diagnosis for " + _personFullName + " created by CaseDiagnosis User1")
              .ValidateProfessionalCoderConfirmedDiagnosisFieldVisible(false)
              .ValidateConfirmedDateFieldVisible(false)
              .SelectSourceOfDiagnosis("Clinical Coder diagnosis")
              .SelectProvisionalOrConfirmedid("Confirmed")
              .ValidateProfessionalCoderConfirmedDiagnosisFieldVisible(true)
              .ValidateConfirmedDateFieldVisible(true)
              .ClickProfessionalWhoConfirmedLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Automation_Diagnosis_Test_User_1")
                .TapSearchButton()
                .SelectResultElement(_AutomationDiagnosisUser1_SystemUserId.ToString());

            PersonHealthDiagnosisRecordPage
             .WaitForPersonHealthDiagnosisRecordPageToLoad("Diagnosis for " + _personFullName + " created by CaseDiagnosis User1")
             .ClickSaveButton();

            System.Threading.Thread.Sleep(3000);

            PersonHealthDiagnosisRecordPage
             .WaitForPersonHealthDiagnosisRecordPageToLoad("Diagnosis for " + _personFullName + " created by CaseDiagnosis User1")
             .ValidateProfessionalWhoConfirmedDiagnosisFieldDisabled(true)
             .ValidateConfirmedDateFieldDisabled(true)
             .ValidateProvisonalConfirmedFieldDisabled(true)
             .SelectSourceOfDiagnosis("Practitioner diagnosis")
             .ClickSaveButton()
             .WaitForPersonHealthDiagnosisRecordPageToLoad("Diagnosis for " + _personFullName + " created by CaseDiagnosis User1")
             .ValidateSourceOfDiagnosisField("2");



        }

        [TestProperty("JiraIssueID", "CDV6-14878")]
        [Description("Navigate to People + open person Record +Navigate to Case Section + Select Inpatient Case Recor " +
                     "Navigete to Health->Diagnosis+Create the Diagnosis Record with Primary or Secondary Field='Primary' + Provisional or Confirmed='Confirmed'+Source of Diagnosis='Clinical Coder diagnosis' save the record'" +
                     "Update the Record With Date Ended Field" +
                     "Validate End Reason And Professional Who Provided End Reason Fields Are Visible" +
                     "Update the Record with End Reason And Professional Who Provided End Reason Fields Are Visible" +
                     "Validate that Record Is Inactive")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_Diagnosis_UITestCases08()
        {

            loginPage
                .GoToLoginPage()
                .Login("CaseDiagnosisUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
               WaitForPeoplePageToLoad()
               .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
               .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .NavigateToDiagnosisPage();

            personHealthDiagnosesPage
                 .WaitForPersonHealthDiagnosesPageToLoad()
                 .ClickNewRecordButton();

            var InpatientonsultantEpisodeRecords = dbHelper.inpatientConsultantEpisode.GetByPersonId(_personID);
            Assert.AreEqual(1, InpatientonsultantEpisodeRecords.Count);

            PersonHealthDiagnosisRecordPage
                .WaitForPersonHealthDiagnosisRecordPageToLoad("New")
                .SelectSourceOfDiagnosis("Practitioner diagnosis")
                .ClickConsultantEpisodeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Automation_Diagnosis_Person AutomationDiagnosisLastName")
                .TapSearchButton()
                .SelectResultElement(InpatientonsultantEpisodeRecords[0].ToString());

            PersonHealthDiagnosisRecordPage
               .WaitForPersonHealthDiagnosisRecordPageToLoad("New")
               .SelectAsteriskorDaggerid("1")
               .SelectProvisionalOrConfirmedid("Provisional")
               .SelectPrimaryOrSecondaryid("Primary")
               .InsertDiagnosisDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .ClickDiagnosisLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("AutomationDiagnosis_Testing")
                .TapSearchButton()
                .SelectResultElement(_diagnosisId.ToString());

            PersonHealthDiagnosisRecordPage
               .WaitForPersonHealthDiagnosisRecordPageToLoad("New")
               .ValidateProfessionalProvidedEndReasonFieldVisible(false)
               .ValidateEndReasonFieldVisible(false)
               .InsertDateEnded(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .SelectisPersonAwerOfDiagnosis("Yes")
               .WaitForPersonHealthDiagnosisRecordPageToLoad("New")
               .ValidateProfessionalProvidedEndReasonFieldVisible(true)
               .ValidateEndReasonFieldVisible(true)
               .ClickProfessionalProvidedEndReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Automation_Diagnosis_Test_User_1")
                .TapSearchButton()
                .SelectResultElement(_AutomationDiagnosisUser1_SystemUserId.ToString());

            PersonHealthDiagnosisRecordPage
              .WaitForPersonHealthDiagnosisRecordPageToLoad("New")
              .ClickEndReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("AutomationDiagnosisEndReason_Testing")
                .TapSearchButton()
                .SelectResultElement(_personDiagnosisEndReasonId.ToString());

            PersonHealthDiagnosisRecordPage
              .WaitForPersonHealthDiagnosisRecordPageToLoad("New")
              .ClickSaveButton();

            System.Threading.Thread.Sleep(3000);

            var personDiagnosisRecords = dbHelper.personDiagnosis.GetByPersonId(_personID);
            Assert.AreEqual(1, personDiagnosisRecords.Count);

            var personDiagnosisRecordsFields = dbHelper.personDiagnosis.GetByID(personDiagnosisRecords[0], "inactive");
            Assert.AreEqual(true, personDiagnosisRecordsFields["inactive"]);

            PersonHealthDiagnosisRecordPage
               .WaitForSavedPersonHealthDiagnosisRecordPageToLoad("Diagnosis for " + _personFullName + " created by CaseDiagnosis User1")
               .ValidateEndReasonField("AutomationDiagnosisEndReason_Testing")
               .ValidateProfessionalProvidedEndReasonField("Automation Diagnosis Test User 1");

        }


        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

        #endregion
    }
}

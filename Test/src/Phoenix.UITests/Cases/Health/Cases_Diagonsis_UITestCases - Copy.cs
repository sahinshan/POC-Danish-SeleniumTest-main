using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.People.Health
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    /// 
    [TestClass]
    public class Case_Diagnosis_UITestCases : FunctionalTest
    {
        private Guid _authenticationproviderid = new Guid("64d2d456-11dc-e611-80d4-0050560502cc");//Internal
        private Guid _languageId;
        private Guid _AutomationDiagnosisUser1_SystemUserId;
        private Guid _AutomationDiagnosisUser2_SystemUserId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _ethnicityId;
        private Guid _contactReasonId;
        private Guid _contactSourceId;
        private Guid _personID;
        private int _personNumber;
        private string _personFullName;
        private Guid _secondaryCaseReasonId;
        private Guid _caseId;
        private string _caseNumber;
        private Guid _dataFormListid = new Guid("fde6a5fc-2db7-e811-80dc-0050560502cc");//Inpatient Case
        private Guid _casestatusid = new Guid("3B39D7C7-573B-E911-80DC-0050560502CC");//Admission
        private Guid _inpatientAdmissionSourceId;
        private Guid _provider_HospitalId;
        private Guid _inpationAdmissionMethodId;
        private Guid _wardSpecialityId = new Guid("08295329-10bc-e811-80dc-0050560502cc");//Adult Acute
        private Guid _inpationWardId;
        private Guid _inpationBayId;
        private Guid _inpationBedId;
        private Guid _inpatientBedTypeId = new Guid("4280BA43-F122-E911-80DC-0050560502CC");//Clinitron
        private Guid _actualDischargeDestinationId;
        private Guid caseCloseingReasonid = new Guid("F7F821A2-B3C0-E811-80DC-0050560502CC");//Person Deceased



        private Guid _caseStatusID = new Guid("18206816-583B-E911-80DC-0050560502CC");




        [TestInitialize()]
        public void TestInitializationMethod()
        {
            #region Language

            var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
            if (!language)
                dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
            _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

            #endregion Language

            #region Business Unit

            var businessUnitExists = dbHelper.businessUnit.GetByName("CareDirector QA").Any();
            if (!businessUnitExists)
                dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
            _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];

            #endregion  Business Unit

            #region Team

            var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector QA").Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
            _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

            #endregion Team

            #region System User

            var automationSmokeTestUser1Exists = dbHelper.systemUser.GetSystemUserByUserName("Automation_Diagnosis_Test_User_1").Any();
            if (!automationSmokeTestUser1Exists)
            {
                _AutomationDiagnosisUser1_SystemUserId = dbHelper.systemUser.CreateSystemUser("Automation_Diagnosis_Test_User_1", "Automation", " Diagnosis Test User 1", "Automation Diagnosis Test User 1", "Passw0rd_!", "Automation_Diagnosis_Test_User_1@somemail.com", "Automation_Diagnosis_Test_User_1@secureemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, DateTime.Now.Date);

                var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("CW Systemuser - Secure Fields (Edit)").First();

                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_AutomationDiagnosisUser1_SystemUserId, systemAdministratorSecurityProfileId);
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_AutomationDiagnosisUser1_SystemUserId, systemUserSecureFieldsSecurityProfileId);
            }
            if (_AutomationDiagnosisUser1_SystemUserId == Guid.Empty)
            {
                _AutomationDiagnosisUser1_SystemUserId = dbHelper.systemUser.GetSystemUserByUserName("Automation_Diagnosis_Test_User_1").FirstOrDefault();
            }

            #endregion System User

            #region Ethnicity

            var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("Diagnosis_Ethnicity").Any();
            if (!ethnicitiesExist)
                dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "Diagnosis_Ethnicity", new DateTime(2020, 1, 1));
            _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Diagnosis_Ethnicity")[0];

            #endregion Ethnicity

            #region Contact Reason

            var contactReasonExists = dbHelper.contactReason.GetByName("ConsultantEpisode_ContactReason").Any();
            if (!contactReasonExists)
                dbHelper.contactReason.CreateContactReason(_careDirectorQA_TeamId, "ConsultantEpisode_ContactReason", new DateTime(2020, 1, 1), 140000001, false);
            _contactReasonId = dbHelper.contactReason.GetByName("ConsultantEpisode_ContactReason")[0];

            #endregion Contact Reason

            #region Contact Source

            var contactSourceExists = dbHelper.contactSource.GetByName("ConsultantEpisode_ContactSource").Any();
            if (!contactSourceExists)
                dbHelper.contactSource.CreateContactSource(_careDirectorQA_TeamId, "ConsultantEpisode_ContactSource", new DateTime(2020, 1, 1));
            _contactSourceId = dbHelper.contactSource.GetByName("ConsultantEpisode_ContactSource")[0];

            #endregion Contact Source


            #region Contact Inpatient Admission Source

            var inpatientAdmissionSourceExists = dbHelper.inpatientAdmissionSource.GetByName("ConsultantEpisode_InpatientAdmissionSource").Any();
            if (!inpatientAdmissionSourceExists)
                dbHelper.inpatientAdmissionSource.CreateInpatientAdmissionSource(_careDirectorQA_TeamId, "ConsultantEpisode_InpatientAdmissionSource", new DateTime(2020, 1, 1));
            _inpatientAdmissionSourceId = dbHelper.inpatientAdmissionSource.GetByName("ConsultantEpisode_InpatientAdmissionSource")[0];

            #endregion Contact Inpatient Admission Source

            #region Provider_Hospital

            var providerHospitalExists = dbHelper.provider.GetProviderByName("Automation_Provider").Any();
            if (!providerHospitalExists)
                dbHelper.provider.CreateProvider("Automation_Provider",_careDirectorQA_TeamId);
            _provider_HospitalId = dbHelper.provider.GetProviderByName("Automation_Provider")[0];

            #endregion Provider_Hospital

            #region Ward



            var inpatientWardExists = dbHelper.inpatientWard.GetInpatientWardByTitle("Automation_Ward_Testing").Any();
            if (!inpatientWardExists)
                dbHelper.inpatientWard.CreateInpatientWard(_careDirectorQA_TeamId, _provider_HospitalId,_AutomationDiagnosisUser1_SystemUserId, _wardSpecialityId, "Automation_Ward_Testing", new DateTime(2020, 1, 1));
            _inpationWardId = dbHelper.inpatientWard.GetInpatientWardByTitle("Automation_Ward_Testing")[0];



            #endregion Ward

            #region Bay/Room



            var inpatientBayRoomExists = dbHelper.inpatientBay.GetInpatientBayByName("Automation_Bay_Testing").Any();
            if (!inpatientBayRoomExists)
                dbHelper.inpatientBay.CreateInpatientCaseBay(_careDirectorQA_TeamId, _inpationWardId, "Automation_Bay_Testing", 1, "5", "5", "5", 2);
            _inpationBayId = dbHelper.inpatientBay.GetInpatientBayByName("Automation_Bay_Testing")[0];



            #endregion Bay/Room

            #region Bed



            var inpatientBedExists = dbHelper.inpatientBed.GetInpatientBedByInpatientBayId(_inpationBayId).Any();
            if (!inpatientBedExists)



                dbHelper.inpatientBed.CreateInpatientBed(_careDirectorQA_TeamId, "12345", "5", "5", _inpationBayId, 1, _inpatientBedTypeId, "5");



            _inpationBedId = dbHelper.inpatientBed.GetInpatientBedByInpatientBayId(_inpationBayId)[0];



            #endregion Bed


            //Elective Admission Waiting List


            #region InpatientAdmissionMethod

            var inpatientAdmissionMethodExists = dbHelper.inpatientAdmissionMethod.GetAdmissionMethodByName("Automation_Admission").Any();
            if (!inpatientAdmissionMethodExists)
                dbHelper.inpatientAdmissionMethod.CreateAdmissionMethod("Automation_Admission", _careDirectorQA_TeamId,_careDirectorQA_BusinessUnitId, new DateTime(2020, 1, 1));
            _inpationAdmissionMethodId = dbHelper.inpatientAdmissionMethod.GetAdmissionMethodByName("Automation_Admission")[0];

            #endregion InpatientAdmissionMethod

            #region Person

            var personRecordExists = dbHelper.person.GetByFirstName("Automation_Diagnosis_Person").Any();
            if (!personRecordExists)
            {
                _personID = dbHelper.person.CreatePersonRecord("", "Automation_Diagnosis_Person", "", "AutomationDiagnosisLastName", "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
                _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            }
            if (_personID == Guid.Empty)
            {
                _personID = dbHelper.person.GetByFirstName("Automation_Diagnosis_Person").FirstOrDefault();
                _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            }
            _personFullName = "Automation_Diagnosis_Person AutomationDiagnosisLastName";

            #endregion Person

            #region Delete Method

            //updating discharge details
            var caseCloseingReasonid = new Guid("7e585578-1ecc-ea11-a2cd-005056926fe4");//Deceased
            var actualDischargeDestination = new Guid("cd731222-3a7d-e911-a2c5-005056926fe4");//Usual residence
            

            foreach (var caseid in dbHelper.Case.GetCasesByPersonID(_personID))
            {
                dbHelper.Case.UpdateCaseRecord(caseid, 4, DateTime.Now.AddDays(1).Date, caseCloseingReasonid, actualDischargeDestination);
            }

            //deleteing Case Status History

            foreach (var caseHistoryid in dbHelper.CaseStatusHistory.GetByPersonID(_personID))
            {

                dbHelper.CaseStatusHistory.DeleteCaseStatusHistory(caseHistoryid);
            }

            //Deleting Case Record
            foreach (var caseid in dbHelper.Case.GetCasesByPersonID(_personID))
            {
                foreach (var caseinvolvementid in dbHelper.CaseInvolvement.GetByCaseID(caseid))
                {
                    foreach (var inpatientconsultantid in dbHelper.inpatientConsultantEpisode.GetByPersonId(_personID))
                    {
                        foreach (var caseHistoryid in dbHelper.CaseStatusHistory.GetByPersonID(_personID))
                        {

                            dbHelper.CaseStatusHistory.DeleteCaseStatusHistory(caseHistoryid);
                        }
                        dbHelper.inpatientConsultantEpisode.DeleteInpatientConsultantEpisode(inpatientconsultantid);
                    }
                    dbHelper.CaseInvolvement.DeleteCaseInvolvement(caseinvolvementid);
                }
                

                dbHelper.Case.DeleteCase(caseid);
            }

            #endregion

            #region Case

            var  _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var dataFormId = dbHelper.dataForm.GetByName("Inpatient Case").FirstOrDefault();

            var caseRecordsExist = dbHelper.Case.GetCasesByPersonID(_personID).Any();
            if (!caseRecordsExist)
            {

                 _caseId = dbHelper.Case.CreateInpatientCaseRecordWithStatusAsAdmission(_careDirectorQA_TeamId, _personID, DateTime.Now.Date, _AutomationDiagnosisUser1_SystemUserId, "hdsa", _AutomationDiagnosisUser1_SystemUserId, _caseStatusId, _contactReasonId, DateTime.Now.Date, dataFormId, _contactSourceId, _inpationWardId, _inpationBayId, _inpationBedId, _inpatientAdmissionSourceId, _inpationAdmissionMethodId, _AutomationDiagnosisUser1_SystemUserId, DateTime.Now.Date, _provider_HospitalId, _inpationWardId, 1, DateTime.Now.Date,
            false, false, false, false, false, false, false, false, false, false);

                _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];

                
            }
            if (_caseId == Guid.Empty)
            {
                _caseId = dbHelper.Case.GetCasesByPersonID(_personID).FirstOrDefault();
                _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];
            }

             
            #endregion Case


            ////By default activate SNOMED
            //var snomedBusinessModuleId = dbHelper.businessModule.GetBusinessModuleByName("SNOMED").FirstOrDefault();
            //bool moduleInactive = (bool)dbHelper.businessModule.GetBusinessModuleByID(snomedBusinessModuleId, "inactive")["inactive"];
            //if (moduleInactive)
            //    dbHelper.businessModule.ActivateModule(snomedBusinessModuleId);
        }

        #region 

        [TestProperty("JiraIssueID", "CDV6-")]
        [Description("")]
        [TestMethod]
        public void Case_Diagnosis_UITestCases01()
        {

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
               WaitForPeoplePageToLoad()
               .SearchPersonRecord(_personNumber.ToString(), _personID.ToString())
               .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad();





        }
        #endregion
    }
}

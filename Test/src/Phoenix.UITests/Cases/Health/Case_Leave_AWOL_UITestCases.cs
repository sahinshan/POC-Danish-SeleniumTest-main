using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Cases
{
    [TestClass]

    public class Case_Leave_AWOL_UITestCases : FunctionalTest
    {
        private Guid _authenticationproviderid = new Guid("64d2d456-11dc-e611-80d4-0050560502cc");//Internal
        private Guid _languageId;
        private Guid _AutomationLeaveAWOLUser1_SystemUserId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _ethnicityId;
        private Guid _contactReasonId;
        private Guid _contactSourceId;
        private Guid _personID;
        private int _personNumber;
        private string _personFullName;
        private Guid _person2ID;
        private int _personNumber2;
        private string _personFullName2;
        private Guid _caseId;
        private string _caseNumber;
        private Guid _case2Id;
        private string _caseNumber2;
        private Guid _dataFormListid = new Guid("fde6a5fc-2db7-e811-80dc-0050560502cc");//Inpatient Case
        private Guid _casestatusid = new Guid("3B39D7C7-573B-E911-80DC-0050560502CC");//Admission
        private Guid _inpatientAdmissionSourceId;
        private string _provider_Name;
        private Guid _provider_HospitalId;
        private string _provider2_Name;
        private Guid _provider_Hospital2Id;
        private Guid _inpationAdmissionMethodId;
        private Guid _wardSpecialityId = new Guid("08295329-10bc-e811-80dc-0050560502cc");//Adult Acute
        private string _inpationWardName;
        private Guid _inpationWardId;
        private string _inpationWard2Name;
        private Guid _inpationWard2Id;
        private string _inpationBayName;
        private Guid _inpationBayId;
        private Guid _inpationBayRoom2Id;
        private Guid _inpationBedId;
        private Guid _inpationBed2Id;
        private Guid _inpatientBedTypeId = new Guid("4280BA43-F122-E911-80DC-0050560502CC");//Clinitron
        private Guid _actualDischargeDestinationId;
        private Guid caseCloseingReasonid = new Guid("F7F821A2-B3C0-E811-80DC-0050560502CC");//Person Deceased
        private Guid _caseStatusID = new Guid("18206816-583B-E911-80DC-0050560502CC");
        private Guid _inpatientCaseAwaitingForAdmission;
        private Guid _inpatientLeaveCancellationReasonId;
        private Guid _inpatientLeaveTypeId;
        private Guid _personAbsenceTypeId;
        private Guid _missingPersonId;
        private Guid _inpatientLeaveEndReasonId;
        private Guid _inpatientLeaveEndReasonAWOLId;
        private Guid _documenttypeid;
        private Guid _documentsubtypeid;
        private string _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _caseClosureReason2Id;
        private string _systemUsername;
        private Guid _systemUserId;


        [TestInitialize()]
        public void TestInitializationMethod()
        {
            try
            {
                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];

                #endregion

                #region Language

                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
                if (!language)
                    dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
                _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

                #endregion 

                #region Business Unit

                var businessUnitExists = dbHelper.businessUnit.GetByName("CareDirector QA").Any();
                if (!businessUnitExists)
                    dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
                _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];

                #endregion  

                #region Team

                var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector QA").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
                _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

                #endregion 

                #region System User

                var automationLeaveAWOLTEstUSer1Exists = dbHelper.systemUser.GetSystemUserByUserName("Automation_LeaveAWOL_Test_User_1").Any();
                if (!automationLeaveAWOLTEstUSer1Exists)
                {
                    _AutomationLeaveAWOLUser1_SystemUserId = dbHelper.systemUser.CreateSystemUser("Automation_LeaveAWOL_Test_User_1", "Automation", " LeaveAWOL Test User 1", "Automation LeaveAWOL Test User 1", "Passw0rd_!", "Automation_LeaveAWOL_Test_User_1@somemail.com", "Automation_LeaveAWOL_Test_User_1@secureemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, DateTime.Now.Date);

                    var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();

                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_AutomationLeaveAWOLUser1_SystemUserId, systemAdministratorSecurityProfileId);
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_AutomationLeaveAWOLUser1_SystemUserId, systemUserSecureFieldsSecurityProfileId);
                }
                if (_AutomationLeaveAWOLUser1_SystemUserId == Guid.Empty)
                {
                    _AutomationLeaveAWOLUser1_SystemUserId = dbHelper.systemUser.GetSystemUserByUserName("Automation_LeaveAWOL_Test_User_1").FirstOrDefault();
                }

                #endregion 

                #region Ethnicity

                var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("LeaveAWOL_Ethnicity").Any();
                if (!ethnicitiesExist)
                    dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "LeaveAWOL_Ethnicity", new DateTime(2020, 1, 1));
                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("LeaveAWOL_Ethnicity")[0];

                #endregion 

                #region Contact Reason

                var contactReasonExists = dbHelper.contactReason.GetByName("LeaveAWOL_ContactReason").Any();
                if (!contactReasonExists)
                    dbHelper.contactReason.CreateContactReason(_careDirectorQA_TeamId, "LeaveAWOL_ContactReason", new DateTime(2020, 1, 1), 140000001, false);
                _contactReasonId = dbHelper.contactReason.GetByName("LeaveAWOL_ContactReason")[0];

                #endregion 

                #region Contact Source

                var contactSourceExists = dbHelper.contactSource.GetByName("LeaveAWOL_ContactSource").Any();
                if (!contactSourceExists)
                    dbHelper.contactSource.CreateContactSource(_careDirectorQA_TeamId, "LeaveAWOL_ContactSource", new DateTime(2020, 1, 1));
                _contactSourceId = dbHelper.contactSource.GetByName("LeaveAWOL_ContactSource")[0];

                #endregion 


                #region Contact Inpatient Admission Source

                var inpatientAdmissionSourceExists = dbHelper.inpatientAdmissionSource.GetByName("LeaveAWOL_InpatientAdmissionSource").Any();
                if (!inpatientAdmissionSourceExists)
                    dbHelper.inpatientAdmissionSource.CreateInpatientAdmissionSource(_careDirectorQA_TeamId, "LeaveAWOL_InpatientAdmissionSource", new DateTime(2020, 1, 1));
                _inpatientAdmissionSourceId = dbHelper.inpatientAdmissionSource.GetByName("LeaveAWOL_InpatientAdmissionSource")[0];

                #endregion 

                #region Provider_Hospital

                _provider_Name = "Aut_Prov_LeaveAWOL_" + _currentDateString;
                _provider_HospitalId = dbHelper.provider.CreateProvider(_provider_Name, _careDirectorQA_TeamId);

                _provider2_Name = "Aut_Prov2_LeaveAWOL_" + _currentDateString;
                _provider_Hospital2Id = dbHelper.provider.CreateProvider(_provider2_Name, _careDirectorQA_TeamId);

                #endregion 

                #region Ward

                _inpationWardName = "Ward_" + _currentDateString;
                _inpationWardId = dbHelper.inpatientWard.CreateInpatientWard(_careDirectorQA_TeamId, _provider_HospitalId, _AutomationLeaveAWOLUser1_SystemUserId, _wardSpecialityId, _inpationWardName, new DateTime(2020, 1, 1));

                _inpationWard2Name = "Ward2_" + _currentDateString;
                _inpationWard2Id = dbHelper.inpatientWard.CreateInpatientWard(_careDirectorQA_TeamId, _provider_Hospital2Id, _AutomationLeaveAWOLUser1_SystemUserId, _wardSpecialityId, _inpationWard2Name, new DateTime(2020, 1, 1));

                #endregion 

                #region Bay/Room

                _inpationBayName = "Bay_" + _currentDateString;
                _inpationBayId = dbHelper.inpatientBay.CreateInpatientCaseBay(_careDirectorQA_TeamId, _inpationWardId, _inpationBayName, 1, "4", "4", "4", 2);

                _inpationBayRoom2Id = dbHelper.inpatientBay.CreateInpatientCaseBay(_careDirectorQA_TeamId, _inpationWard2Id, "Bay2_" + _currentDateString, 1, "3", "3", "3", 2);

                #endregion

                #region Bed

                var inpatientBedExists = dbHelper.inpatientBed.GetInpatientBedByInpatientBayId(_inpationBayId).Any();
                if (!inpatientBedExists)
                    dbHelper.inpatientBed.CreateInpatientBed(_careDirectorQA_TeamId, "12665", "4", "4", _inpationBayId, 1, _inpatientBedTypeId, "4");
                _inpationBedId = dbHelper.inpatientBed.GetInpatientBedByInpatientBayId(_inpationBayId)[0];

                var inpatientBed2Exists = dbHelper.inpatientBed.GetInpatientBedByInpatientBayId(_inpationBayRoom2Id).Any();
                if (!inpatientBed2Exists)
                    dbHelper.inpatientBed.CreateInpatientBed(_careDirectorQA_TeamId, "12664", "3", "3", _inpationBayRoom2Id, 1, _inpatientBedTypeId, "3");
                _inpationBed2Id = dbHelper.inpatientBed.GetInpatientBedByInpatientBayId(_inpationBayRoom2Id)[0];



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
                _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

                #endregion 

                #region To Create Inpatient Case record 1

                DateTime admissionDate = new DateTime(2022, 1, 1);
                _caseId = dbHelper.Case.CreateInpatientCaseRecordWithStatusAsAdmission(_careDirectorQA_TeamId, _personID, DateTime.Now.Date, _AutomationLeaveAWOLUser1_SystemUserId, "hdsa", _AutomationLeaveAWOLUser1_SystemUserId, _caseStatusID, _contactReasonId, DateTime.Now.Date, _dataFormListid, _contactSourceId, _inpationWardId, _inpationBayId, _inpationBedId, _inpatientAdmissionSourceId, _inpationAdmissionMethodId, _AutomationLeaveAWOLUser1_SystemUserId, admissionDate, _provider_HospitalId, _inpationWardId, 1, DateTime.Now.Date, false, false, false, false, false, false, false, false, false, false, 2);
                _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];

                #endregion 


                #region Inpatient Leave  Cancellation Reason

                var inpatientLeaveCancellationReasonExists = dbHelper.inpatientLeaveCancellationReason.GetInpatientLeaveCancellationReasonByName("Automation_LeaveAWOLCancellation").Any();
                if (!inpatientLeaveCancellationReasonExists)
                    dbHelper.inpatientLeaveCancellationReason.CreateInpatientLeaveCancellationReason("Automation_LeaveAWOLCancellation", _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, new DateTime(2020, 1, 1));
                _inpatientLeaveCancellationReasonId = dbHelper.inpatientLeaveCancellationReason.GetInpatientLeaveCancellationReasonByName("Automation_LeaveAWOLCancellation")[0];

                inpatientLeaveCancellationReasonExists = dbHelper.inpatientLeaveCancellationReason.GetInpatientLeaveCancellationReasonByName("Automation_CancellationValidForDischarge").Any();
                if (!inpatientLeaveCancellationReasonExists)
                    dbHelper.inpatientLeaveCancellationReason.CreateInpatientLeaveCancellationReason("Automation_CancellationValidForDischarge", _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, new DateTime(2020, 1, 1), true);


                #endregion

                #region Inpatient Leave Type

                var inpatientLeaveTypeExists = dbHelper.inpatientLeaveType.GetInpatientLeaveTypeByName("Automation_LeaveAWOLType").Any();
                if (!inpatientLeaveTypeExists)
                    dbHelper.inpatientLeaveType.CreateInpatientLeaveType("Automation_LeaveAWOLType", _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, new DateTime(2020, 1, 1));
                _inpatientLeaveTypeId = dbHelper.inpatientLeaveType.GetInpatientLeaveTypeByName("Automation_LeaveAWOLType")[0];

                #endregion 

                #region Person Absence Type

                var personAbsenceTypeExists = dbHelper.personAbsenceType.GetPersonAbsenceTypeByName("Automation_LeaveAWOLAbsenceType").Any();
                if (!personAbsenceTypeExists)
                    dbHelper.personAbsenceType.CreatePersonAbsenceType("Automation_LeaveAWOLAbsenceType", _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, new DateTime(2020, 1, 1));
                _personAbsenceTypeId = dbHelper.inpatientLeaveType.GetInpatientLeaveTypeByName("Automation_LeaveAWOLAbsenceType").FirstOrDefault();

                #endregion

                #region Case Closure Reason

                if (!dbHelper.caseClosureReason.GetByName("Automation_LeaveAWOLClosureReason").Any())
                    dbHelper.caseClosureReason.CreateCaseClosureReason(_careDirectorQA_TeamId, "Automation_LeaveAWOLClosureReason", "", "", new DateTime(2020, 1, 1), 140000001, true);
                _caseClosureReason2Id = dbHelper.caseClosureReason.GetByName("Automation_LeaveAWOLClosureReason").FirstOrDefault();

                #endregion

                #region Inpatient Discharge Destination

                if (!dbHelper.inpatientDischargeDestination.GetByName("Usual residence").Any())
                    dbHelper.inpatientDischargeDestination.CreateInpatientDischargeDestination(_careDirectorQA_TeamId, "Usual residence", new DateTime(2020, 1, 1));
                _actualDischargeDestinationId = dbHelper.inpatientDischargeDestination.GetByName("Usual residence").FirstOrDefault();

                #endregion

                #region Missing Person

                var missingPersonExists = dbHelper.missingPerson.GetByPersonId(_personID).Any();
                if (!missingPersonExists)
                    dbHelper.missingPerson.CreateMissingPerson(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, new DateTime(2022, 1, 1), _personID, _personAbsenceTypeId);
                _missingPersonId = dbHelper.missingPerson.GetByPersonId(_personID)[0];

                #endregion 


                #region Inpatient Leave end Reason

                var inpatientLeaveEndReasonExists = dbHelper.inpatientLeaveEndReason.GetInpatientLeaveEndReasonByName("Automation_LeaveAWOL_EndReason").Any();
                if (!inpatientLeaveEndReasonExists)
                    dbHelper.inpatientLeaveEndReason.CreateInpatientLeaveEndReason("Automation_LeaveAWOL_EndReason", _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, new DateTime(2022, 1, 1), false);
                _inpatientLeaveEndReasonId = dbHelper.inpatientLeaveEndReason.GetInpatientLeaveEndReasonByName("Automation_LeaveAWOL_EndReason")[0];


                var inpatientLeaveEndReasonAWOLExists = dbHelper.inpatientLeaveEndReason.GetInpatientLeaveEndReasonByName("AWOL REASON_1").Any();
                if (!inpatientLeaveEndReasonAWOLExists)
                    dbHelper.inpatientLeaveEndReason.CreateInpatientLeaveEndReason("AWOL REASON_1", _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, new DateTime(2022, 1, 1), true);
                _inpatientLeaveEndReasonAWOLId = dbHelper.inpatientLeaveEndReason.GetInpatientLeaveEndReasonByName("AWOL REASON_1")[0];

                #endregion 

                #region Attach Document Type

                var attachDocumentTypeExists = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Inpatient Case Leave AWOL Attachment type").Any();
                if (!attachDocumentTypeExists)
                    dbHelper.attachDocumentType.CreateAttachDocumentType(_careDirectorQA_TeamId, "Inpatient Case Leave AWOL Attachment type", new DateTime(2020, 1, 1));
                _documenttypeid = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Inpatient Case Leave AWOL Attachment type")[0];

                #endregion

                #region Attach Document Sub Type

                var attachDocumentSubTypeExists = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Inpatient Case Leave AWOL Attachment sub type").Any();
                if (!attachDocumentSubTypeExists)
                    dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(_careDirectorQA_TeamId, "Inpatient Case Leave AWOL Attachment sub type", new DateTime(2020, 1, 1), _documenttypeid);
                _documentsubtypeid = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Inpatient Case Leave AWOL Attachment sub type")[0];

                #endregion

                #region System User FinancialAssessmentUser1

                _systemUsername = "AWOLUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "AWOL", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

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





        #region https://advancedcsg.atlassian.net/browse/CDV6-14906

        [TestProperty("JiraIssueID", "CDV6-14953")]
        [Description("Open the Inpatient case with Admission status" + "Navigate to Menu-Related Items-LeaveAWOL" +
            "Click on Add new record Button" + "Enter the required fields and validate the created record.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void LeaveAWOL_UITestMethod01()
        {
            var agreedLeaveDate = new DateTime(2022, 1, 2);
            var agreedReturnLeaveDate = agreedLeaveDate.AddDays(2).Date;

            loginPage
                .GoToLoginPage()
                .Login("AWOLUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
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
                .NavigateToLeaveAWOLPage();

            leavesAWOLPage
                .WaitForLeavesAWOLPageToLoad()
                .ClickNewRecordButton();

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .ClickNamedProfessionalLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Automation_LeaveAWOL_Test_User_1")
                .TapSearchButton()
                .SelectResultElement(_AutomationLeaveAWOLUser1_SystemUserId.ToString());

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .InsertAgreedLeaveDateTime(agreedLeaveDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .InsertAgreedReturnLeaveDateTime(agreedReturnLeaveDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .ClickWhoAuthorisedLeaveIdLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Automation_LeaveAWOL_Test_User_1")
                .TapSearchButton()
                .SelectResultElement(_AutomationLeaveAWOLUser1_SystemUserId.ToString());

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .ClickLeaveTypeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Automation_LeaveAWOLType")
                .TapSearchButton()
                .SelectResultElement(_inpatientLeaveTypeId.ToString());

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .TapSaveButton();

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var leaveAWOL = dbHelper.inpatientLeaveAwol.GetByPersonId(_personID);
            Assert.AreEqual(1, leaveAWOL.Count);

            var leaveAWOLRecord = dbHelper.inpatientLeaveAwol.GetByID(leaveAWOL[0], "personid", "caseid", "ownerid",
                                    "namedprofessionalid", "admissiondatetime", "inpatientleavetypeid", "agreedleavedatetime",
                                    "agreedreturndatetime", "whoauthorisedleaveidtablename");


            Assert.AreEqual(_personID.ToString(), leaveAWOLRecord["personid"].ToString());
            Assert.AreEqual(_caseId.ToString(), leaveAWOLRecord["caseid"].ToString());
            Assert.AreEqual(_careDirectorQA_TeamId.ToString(), leaveAWOLRecord["ownerid"].ToString());
            Assert.AreEqual(_AutomationLeaveAWOLUser1_SystemUserId.ToString(), leaveAWOLRecord["namedprofessionalid"].ToString());
            Assert.AreEqual(new DateTime(2022, 1, 1), ((DateTime)leaveAWOLRecord["admissiondatetime"]).ToLocalTime().Date);
            Assert.AreEqual(agreedLeaveDate, ((DateTime)leaveAWOLRecord["agreedleavedatetime"]).ToLocalTime().Date);
            Assert.AreEqual(_inpatientLeaveTypeId.ToString(), leaveAWOLRecord["inpatientleavetypeid"].ToString());
            Assert.AreEqual(agreedReturnLeaveDate, ((DateTime)leaveAWOLRecord["agreedreturndatetime"]).ToLocalTime().Date);
            Assert.AreEqual("systemuser", leaveAWOLRecord["whoauthorisedleaveidtablename"]);

        }


        [TestProperty("JiraIssueID", "CDV6-14969")]
        [Description("Open the Inpatient case with Awaiting Admission status" +
            "Navigate to Menu-Related Items and validate the Leave/AWOL is not available")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void LeaveAWOL_UITestMethod02()
        {

            //creating Inpatient Case Record With Admission Status=" Awaiting For Admission"
            _inpatientCaseAwaitingForAdmission = dbHelper.Case.CreateInpatientCaseRecord(_dataFormListid, _careDirectorQA_TeamId, _personID, DateTime.Now.Date,
                _AutomationLeaveAWOLUser1_SystemUserId, _AutomationLeaveAWOLUser1_SystemUserId
                , _contactReasonId, _contactSourceId, "Inpatient Case Awaiting for Admission", 3, _caseStatusID,
                _AutomationLeaveAWOLUser1_SystemUserId, DateTime.Now.Date, DateTime.Now.Date, DateTime.Now.Date, DateTime.Now.Date, _provider_HospitalId,
                _inpationWardId, false, false, false, false, false, false, false, false, false, false);

            loginPage
                .GoToLoginPage()
                .Login("AWOLUser1", "Passw0rd_!")
                ;

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
                 .OpenCaseRecord(_inpatientCaseAwaitingForAdmission.ToString());

            personCasesRecordPage
                 .WaitForPersonCasesRecordPageToLoad()
                 .ValidateLeaveAWOLNotVisible(true);



        }

        [TestProperty("JiraIssueID", "CDV6-14970")]
        [Description("Open the Inpatient case with Discharge Awaiting Closure" + "Navigate to Menu-Related Items-LeaveAWOL" +
           "Click on Add new record Button" + "Enter the required fields and validate the error message displayed")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void LeaveAWOL_UITestMethod03()
        {
            //Update Discharge Awaiting Closure

            foreach (var caseid in dbHelper.Case.GetCasesByPersonID(_personID))
            {
                dbHelper.Case.UpdateCaseRecord(caseid, 4, DateTime.Now.AddDays(1).Date, caseCloseingReasonid, _actualDischargeDestinationId);
            }

            var agreedLeaveDate = new DateTime(2022, 1, 2);
            var agreedReturnLeaveDate = agreedLeaveDate.AddDays(2).Date;

            loginPage
             .GoToLoginPage()
             .Login("AWOLUser1", "Passw0rd_!")
             ;

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
                 .NavigateToLeaveAWOLPage();

            leavesAWOLPage
                 .WaitForLeavesAWOLPageToLoad()
                 .ClickNewRecordButton();

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .ClickNamedProfessionalLookUpButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("AUTOMATION")
                 .TapSearchButton()
                 .SelectResultElement(_AutomationLeaveAWOLUser1_SystemUserId.ToString());

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .InsertAgreedLeaveDateTime(agreedLeaveDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .InsertAgreedReturnLeaveDateTime(agreedReturnLeaveDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .ClickWhoAuthorisedLeaveIdLookUpButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery("AUTOMATION")
               .TapSearchButton()
               .SelectResultElement(_AutomationLeaveAWOLUser1_SystemUserId.ToString());

            leaveAWOLRecordPage
               .WaitForLeaveAWOLRecordPageToLoad()
               .TapSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Leave/Awol records can only be created or modified if the Case has Inpatient Status Admission");


        }

        [TestProperty("JiraIssueID", "CDV6-14972")]
        [Description("Open the Inpatient case with Discharge status" + "Navigate to Menu-Related Items-LeaveAWOL" +
         "Validate the new record button is not visible.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void LeaveAWOL_UITestMethod04()
        {
            //Update case status to Discharge 

            foreach (var caseid in dbHelper.Case.GetCasesByPersonID(_personID))
            {
                dbHelper.Case.UpdateCaseRecord(caseid, 2, DateTime.Now.AddDays(1).Date, caseCloseingReasonid, _actualDischargeDestinationId);
            }

            var agreedLeaveDate = DateTime.Now.Date;
            var agreedReturnLeaveDate = DateTime.Now.AddDays(2).Date;

            loginPage
             .GoToLoginPage()
             .Login("AWOLUser1", "Passw0rd_!")
             ;

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
                 .NavigateToLeaveAWOLPage();

            leavesAWOLPage
                 .WaitForLeavesAWOLPageToLoad()
                 .ValidateNewRecordButtonNotVisible();

        }

        [TestProperty("JiraIssueID", "CDV6-14973")]
        [Description("Open the Inpatient case with Admission status" + "Navigate to Menu-Related Items-LeaveAWOL" +
          "Click on Add new record Button" + "Enter the Agreed leave date after than the Agreed Return leave date" +
            "Validate the Alert Text")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void LeaveAWOL_UITestMethod05()
        {

            var agreedLeaveDate = new DateTime(2022, 1, 8);
            var agreedReturnLeaveDate = new DateTime(2022, 1, 2);

            loginPage
                .GoToLoginPage()
                .Login("AWOLUser1", "Passw0rd_!")
                ;

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
                 .NavigateToLeaveAWOLPage();


            leavesAWOLPage
                .WaitForLeavesAWOLPageToLoad()
                .ClickNewRecordButton();

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .ClickNamedProfessionalLookUpButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("AUTOMATION")
                 .TapSearchButton()
                 .SelectResultElement(_AutomationLeaveAWOLUser1_SystemUserId.ToString());

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .InsertAgreedLeaveDateTime(agreedLeaveDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .InsertAgreedReturnLeaveDateTime(agreedReturnLeaveDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .ClickWhoAuthorisedLeaveIdLookUpButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery("AUTOMATION")
               .TapSearchButton()
               .SelectResultElement(_AutomationLeaveAWOLUser1_SystemUserId.ToString());

            leaveAWOLRecordPage
               .WaitForLeaveAWOLRecordPageToLoad()
               .TapSaveAndCloseButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Agreed Leave Date/Time cannot be after Agreed Return Date/Time");


        }

        [TestProperty("JiraIssueID", "CDV6-14974")]
        [Description("Open the Inpatient case with Admission status" + "Navigate to Menu-Related Items-LeaveAWOL" +
           "Click on Add new record Button" + "Enter the required fields and validate the created record.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void LeaveAWOL_UITestMethod06()
        {
            var agreedLeaveDate = new DateTime(2022, 1, 2);
            var agreedReturnLeaveDate = agreedLeaveDate.AddDays(2).Date;
            var CancelledLeaveDate = new DateTime(2022, 1, 2);

            loginPage
                .GoToLoginPage()
                .Login("AWOLUser1", "Passw0rd_!")
                ;

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
                 .NavigateToLeaveAWOLPage();


            leavesAWOLPage
                .WaitForLeavesAWOLPageToLoad()
                .ClickNewRecordButton();

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .ClickNamedProfessionalLookUpButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("AUTOMATION")
                 .TapSearchButton()
                 .SelectResultElement(_AutomationLeaveAWOLUser1_SystemUserId.ToString());

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .InsertAgreedLeaveDateTime(agreedLeaveDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .InsertAgreedReturnLeaveDateTime(agreedReturnLeaveDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .ClickWhoAuthorisedLeaveIdLookUpButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery("AUTOMATION")
               .TapSearchButton()
               .SelectResultElement(_AutomationLeaveAWOLUser1_SystemUserId.ToString());

            leaveAWOLRecordPage
               .WaitForLeaveAWOLRecordPageToLoad()
               .TapSaveAndCloseButton();

            System.Threading.Thread.Sleep(5000);

            var leaveAWOL = dbHelper.inpatientLeaveAwol.GetByPersonId(_personID);
            Assert.AreEqual(1, leaveAWOL.Count);


            leavesAWOLPage
                .WaitForLeavesAWOLPageToLoad()
                .OpenLeaveAWOLRecord(leaveAWOL[0].ToString());


            leaveAWOLRecordPage
              .WaitForLeaveAWOLRecordPageToLoad()
              .ClickLeaveCancel_YesRadioButton()
              .InsertCancelledLeaveDateTime(CancelledLeaveDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
              .ClickCancellationReasonLookUpButton();


            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery("Automation_LeaveAWOLCancellation")
               .TapSearchButton()
               .SelectResultElement(_inpatientLeaveCancellationReasonId.ToString());


            leaveAWOLRecordPage
              .WaitForLeaveAWOLRecordPageToLoad()
              .ClickCancellationByLookUpButton();

            lookupPopup
              .WaitForLookupPopupToLoad()
              .TypeSearchQuery("AUTOMATION")
              .TapSearchButton()
              .SelectResultElement(_AutomationLeaveAWOLUser1_SystemUserId.ToString());

            leaveAWOLRecordPage
             .WaitForLeaveAWOLRecordPageToLoad()
             .TapSaveAndCloseButton();

            System.Threading.Thread.Sleep(5000);


            var leaveAWOLr = dbHelper.inpatientLeaveAwol.GetByPersonId(_personID);
            Assert.AreEqual(1, leaveAWOL.Count);

            var leaveAWOLRecord = dbHelper.inpatientLeaveAwol.GetByID(leaveAWOLr[0], "personid", "caseid",
                                   "cancellationdatetime", "inpatientleavecancellationreasonid", "cancelledbyid");


            Assert.AreEqual(_personID.ToString(), leaveAWOLRecord["personid"].ToString());
            Assert.AreEqual(_caseId.ToString(), leaveAWOLRecord["caseid"].ToString());
            Assert.AreEqual(CancelledLeaveDate, ((DateTime)leaveAWOLRecord["cancellationdatetime"]).ToLocalTime().Date);
            Assert.AreEqual(_inpatientLeaveCancellationReasonId.ToString(), leaveAWOLRecord["inpatientleavecancellationreasonid"].ToString());
            Assert.AreEqual(_AutomationLeaveAWOLUser1_SystemUserId.ToString(), leaveAWOLRecord["cancelledbyid"].ToString());

        }


        [TestProperty("JiraIssueID", "CDV6-14983")]
        [Description("Open the Inpatient case with Admission status" + "Navigate to Menu-Related Items-LeaveAWOL" +
           "Open the Leave AWOL record , enter the Actual leave date and save it" + " Verify that Actual Leave Date/Time cannot be after Actual Return Date/Time" +
            "Validate the Alert")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void LeaveAWOL_UITestMethod07()
        {
            var leaveAWOLRecord = dbHelper.inpatientLeaveAwol.CreateLeaveAWOLRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _personID, _caseId, new DateTime(2022, 1, 1), _inpatientLeaveTypeId, new DateTime(2022, 1, 1), new DateTime(2022, 1, 1), "systemuser", _AutomationLeaveAWOLUser1_SystemUserId, "Automation  LeaveAWOL Test User 1");

            DateTime actualDate = new DateTime(2022, 1, 6);
            DateTime actualReturnDate = new DateTime(2022, 1, 1);

            loginPage
              .GoToLoginPage()
              .Login("AWOLUser1", "Passw0rd_!")
              ;

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
                 .NavigateToLeaveAWOLPage();


            leavesAWOLPage
                .WaitForLeavesAWOLPageToLoad()
                .OpenLeaveAWOLRecord(leaveAWOLRecord.ToString());

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .InsertActualLeaveDateTime(actualDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .TapSaveButton();

            System.Threading.Thread.Sleep(5000);


            leaveAWOLRecordPage
               .WaitForLeaveAWOLRecordPageToLoad()
               .InsertActualReturnLeaveDateTime(actualReturnDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
               .TapSaveAndCloseButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Actual Leave Date/Time cannot be after Actual Return Date/Time");

        }

        [TestProperty("JiraIssueID", "CDV6-14984")]
        [Description("Open the Inpatient case with Admission status" + "Navigate to Menu-Related Items-LeaveAWOL" +
          "Open the Leave AWOL record , enter future date in Actual leave date and save it" +
           "Validate the Alert")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void LeaveAWOL_UITestMethod08()
        {
            var leaveAWOLRecord = dbHelper.inpatientLeaveAwol.CreateLeaveAWOLRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _personID, _caseId, DateTime.Now.Date, _inpatientLeaveTypeId, DateTime.Now.Date, DateTime.Now.AddDays(1).Date, "systemuser", _AutomationLeaveAWOLUser1_SystemUserId, "Automation  LeaveAWOL Test User 1");

            DateTime actualDate = DateTime.Now.AddDays(10).Date;


            loginPage
              .GoToLoginPage()
              .Login("AWOLUser1", "Passw0rd_!")
              ;

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
                 .NavigateToLeaveAWOLPage();


            leavesAWOLPage
                .WaitForLeavesAWOLPageToLoad()
                .OpenLeaveAWOLRecord(leaveAWOLRecord.ToString());

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .InsertActualLeaveDateTime(actualDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .TapSaveAndCloseButton();

            alertPopup
                 .WaitForAlertPopupToLoad()
                 .ValidateAlertText("Actual Leave Date Time cannot be in the future");

        }

        [TestProperty("JiraIssueID", "CDV6-14985")]
        [Description("Open the Inpatient case with Admission status" + "Navigate to Menu-Related Items-LeaveAWOL" +
          "Open the Leave AWOL record , enter the Actual leave date and save it" + " Enter future date in Actual return leave date and save it " +
           "Validate the Alert")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void LeaveAWOL_UITestMethod09()
        {
            var leaveAWOLRecord = dbHelper.inpatientLeaveAwol.CreateLeaveAWOLRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _personID, _caseId, DateTime.Now.Date, _inpatientLeaveTypeId, DateTime.Now.Date, DateTime.Now.AddDays(1).Date, "systemuser", _AutomationLeaveAWOLUser1_SystemUserId, "Automation  LeaveAWOL Test User 1");

            DateTime actualDate = DateTime.Now.AddDays(-1).Date;
            DateTime actualReturnDate = DateTime.Now.AddDays(10).Date;

            loginPage
              .GoToLoginPage()
              .Login("AWOLUser1", "Passw0rd_!")
              ;

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
                 .NavigateToLeaveAWOLPage();


            leavesAWOLPage
                .WaitForLeavesAWOLPageToLoad()
                .OpenLeaveAWOLRecord(leaveAWOLRecord.ToString());

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .InsertActualLeaveDateTime(actualDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .TapSaveButton();

            System.Threading.Thread.Sleep(2000);


            leaveAWOLRecordPage
               .WaitForLeaveAWOLRecordPageToLoad()
               .InsertActualReturnLeaveDateTime(actualReturnDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
               .TapSaveAndCloseButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Actual Return Date Time cannot be in the future");

        }

        [TestProperty("JiraIssueID", "CDV6-14986")]
        [Description("Open the Inpatient case with Admission status" + "Navigate to Menu-Related Items-LeaveAWOL" +
         "Open the Leave AWOL record , Click on PersonAWOL yes radio button and validate the additional fields are displayed" +
            "Enter the required fields data and save the record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void LeaveAWOL_UITestMethod10()
        {
            var leaveAWOLRecord = dbHelper.inpatientLeaveAwol.CreateLeaveAWOLRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _personID, _caseId, DateTime.Now.Date, _inpatientLeaveTypeId, DateTime.Now.Date, DateTime.Now.AddDays(1).Date, "systemuser", _AutomationLeaveAWOLUser1_SystemUserId, "Automation  LeaveAWOL Test User 1");

            DateTime classedDate = DateTime.Now.Date;


            loginPage
              .GoToLoginPage()
              .Login("AWOLUser1", "Passw0rd_!")
              ;

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
                 .NavigateToLeaveAWOLPage();


            leavesAWOLPage
                .WaitForLeavesAWOLPageToLoad()
                .OpenLeaveAWOLRecord(leaveAWOLRecord.ToString());

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .ClickPersonAWOL_YesRadioButton();

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .ValidatePersonAWOLAdditionalFields()
                .SelectPoliceInformed("1")
                .ClickLinkedMissingPersonRecordLookUpButton();


            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("")
                .TapSearchButton()
                .ClickAddSelectedButton(_missingPersonId.ToString());


            leaveAWOLRecordPage
               .WaitForLeaveAWOLRecordPageToLoad()
               .InsertClassedAsAWOLDateTime(classedDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
               .TapSaveAndCloseButton();

            System.Threading.Thread.Sleep(5000);


            var leaveAWOL = dbHelper.inpatientLeaveAwol.GetByPersonId(_personID);
            Assert.AreEqual(1, leaveAWOL.Count);

            var leaveAWOLRecordfields = dbHelper.inpatientLeaveAwol.GetByID(leaveAWOL[0], "personid", "caseid",
                                   "personawol", "classedasawoldatetime");


            Assert.AreEqual(_personID.ToString(), leaveAWOLRecordfields["personid"].ToString());
            Assert.AreEqual(_caseId.ToString(), leaveAWOLRecordfields["caseid"].ToString());
            Assert.AreEqual(true, leaveAWOLRecordfields["personawol"]);
            Assert.AreEqual(classedDate, ((DateTime)leaveAWOLRecordfields["classedasawoldatetime"]).ToLocalTime().Date);

        }

        [TestProperty("JiraIssueID", "CDV6-14987")]
        [Description("Open the Inpatient case with Admission status" + "Navigate to Menu-Related Items-LeaveAWOL" +
        "Open the Leave AWOL record , Click on PersonAWOL yes radio button and Enter the future date in Classed as AWOL date" +
            "Validate the alert text")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void LeaveAWOL_UITestMethod11()
        {
            var leaveAWOLRecord = dbHelper.inpatientLeaveAwol.CreateLeaveAWOLRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _personID, _caseId, DateTime.Now.Date, _inpatientLeaveTypeId, DateTime.Now.Date, DateTime.Now.AddDays(1).Date, "systemuser", _AutomationLeaveAWOLUser1_SystemUserId, "Automation  LeaveAWOL Test User 1");

            DateTime classedDate = DateTime.Now.AddDays(5).Date;


            loginPage
              .GoToLoginPage()
              .Login("AWOLUser1", "Passw0rd_!")
              ;

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
                 .NavigateToLeaveAWOLPage();


            leavesAWOLPage
                .WaitForLeavesAWOLPageToLoad()
                .OpenLeaveAWOLRecord(leaveAWOLRecord.ToString());

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .ClickPersonAWOL_YesRadioButton();

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .ValidatePersonAWOLAdditionalFields()
                .InsertClassedAsAWOLDateTime(classedDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .TapSaveAndCloseButton();


            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Classed as AWOL Date Time cannot be in the future");

        }

        [TestProperty("JiraIssueID", "CDV6-14989")]
        [Description("Open the Inpatient case with Admission status" + "Navigate to Menu-Related Items-LeaveAWOL" +
       "Open the Leave AWOL record update the Actual leave date and save and close" + "Create new record with same Actual leave date and save" +
          "Validate the error message")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void LeaveAWOL_UITestMethod12()
        {
            var leaveAWOLRecord = dbHelper.inpatientLeaveAwol.CreateLeaveAWOLRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _personID, _caseId, DateTime.Now.Date, _inpatientLeaveTypeId, DateTime.Now.Date, DateTime.Now.AddDays(1).Date, "systemuser", _AutomationLeaveAWOLUser1_SystemUserId, "Automation  LeaveAWOL Test User 1");

            var agreedLeaveDate = DateTime.Now.Date;
            var agreedReturnLeaveDate = DateTime.Now.Date;
            var actualLeaveDate = DateTime.Now.Date;


            loginPage
                .GoToLoginPage()
                .Login("AWOLUser1", "Passw0rd_!")
                ;

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
                 .NavigateToLeaveAWOLPage();

            leavesAWOLPage
               .WaitForLeavesAWOLPageToLoad()
               .OpenLeaveAWOLRecord(leaveAWOLRecord.ToString());

            leaveAWOLRecordPage
              .WaitForLeaveAWOLRecordPageToLoad()
              .InsertActualLeaveDateTime(actualLeaveDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
              .TapSaveAndCloseButton();

            System.Threading.Thread.Sleep(3000);

            leavesAWOLPage
                 .WaitForLeavesAWOLPageToLoad()
                 .ClickNewRecordButton();

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .ClickNamedProfessionalLookUpButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("AUTOMATION")
                 .TapSearchButton()
                 .SelectResultElement(_AutomationLeaveAWOLUser1_SystemUserId.ToString());

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .InsertAgreedLeaveDateTime(agreedLeaveDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .InsertAgreedReturnLeaveDateTime(agreedReturnLeaveDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .ClickWhoAuthorisedLeaveIdLookUpButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery("AUTOMATION")
               .TapSearchButton()
               .SelectResultElement(_AutomationLeaveAWOLUser1_SystemUserId.ToString());

            leaveAWOLRecordPage
               .WaitForLeaveAWOLRecordPageToLoad()
               .ClickLeaveTypeLookUpButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery("Automation_LeaveAWOLType")
               .TapSearchButton()
               .SelectResultElement(_inpatientLeaveTypeId.ToString());

            leaveAWOLRecordPage
               .WaitForLeaveAWOLRecordPageToLoad()
               .InsertActualLeaveDateTime(actualLeaveDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
               .TapSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("There is already an active record of leave/awol, please close it before creating a new one");

        }

        [TestProperty("JiraIssueID", "CDV6-14992")]
        [Description("Open the Inpatient case with Admission status" + "Navigate to Menu-Related Items-LeaveAWOL" +
         "Open the Leave AWOL record update and click Person AWOL yes option and enter the ClassedAs AWOL leave Date and save it" +
          "Create new record with Person AWOL yes option and enter the same Classed As AWOL leave Date and save it" +
         "Validate the error message")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void LeaveAWOL_UITestMethod13()
        {
            var leaveAWOLRecord = dbHelper.inpatientLeaveAwol.CreateLeaveAWOLRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _personID, _caseId, DateTime.Now.Date, _inpatientLeaveTypeId, DateTime.Now.Date, DateTime.Now.AddDays(1).Date, "systemuser", _AutomationLeaveAWOLUser1_SystemUserId, "Automation  LeaveAWOL Test User 1");

            var classedASAWOLLeaveDate = DateTime.Now.Date;


            loginPage
                .GoToLoginPage()
                .Login("AWOLUser1", "Passw0rd_!")
                ;

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
                 .NavigateToLeaveAWOLPage();

            leavesAWOLPage
               .WaitForLeavesAWOLPageToLoad()
               .OpenLeaveAWOLRecord(leaveAWOLRecord.ToString());

            leaveAWOLRecordPage
              .WaitForLeaveAWOLRecordPageToLoad()
              .ClickPersonAWOL_YesRadioButton()
              .InsertClassedAsAWOLDateTime(classedASAWOLLeaveDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
              .TapSaveAndCloseButton();

            System.Threading.Thread.Sleep(3000);

            leavesAWOLPage
                 .WaitForLeavesAWOLPageToLoad()
                 .ClickNewRecordButton();

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .ClickNamedProfessionalLookUpButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("AUTOMATION")
                 .TapSearchButton()
                 .SelectResultElement(_AutomationLeaveAWOLUser1_SystemUserId.ToString());

            leaveAWOLRecordPage
              .WaitForLeaveAWOLRecordPageToLoad()
              .ClickPersonAWOL_YesRadioButton()
              .InsertClassedAsAWOLDateTime(classedASAWOLLeaveDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
              .TapSaveAndCloseButton();


            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("There is already an active record of leave/awol, please close it before creating a new one");

        }

        [TestProperty("JiraIssueID", "CDV6-14995")]
        [Description("Open the Inpatient case with Admission status" + "Navigate to Menu-Related Items-LeaveAWOL" +
        "Open the Leave AWOL record with null Actual leave date" + "Click add new record button and create another leave AWOL record with null Actual leave date" +
            "Validate user should be able to create multiple records with null Actual leave date field.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void LeaveAWOL_UITestMethod14()
        {
            var leaveAWOLRecord1 = dbHelper.inpatientLeaveAwol.CreateLeaveAWOLRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _personID, _caseId, DateTime.Now.Date, _inpatientLeaveTypeId, DateTime.Now.Date, DateTime.Now.AddDays(1).Date, "systemuser", _AutomationLeaveAWOLUser1_SystemUserId, "Automation  LeaveAWOL Test User 1");

            var agreedLeaveDate = DateTime.Now.Date;
            var agreedReturnLeaveDate = DateTime.Now.AddDays(2).Date;


            loginPage
                .GoToLoginPage()
                .Login("AWOLUser1", "Passw0rd_!")
                ;

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
                 .NavigateToLeaveAWOLPage();


            leavesAWOLPage
                .WaitForLeavesAWOLPageToLoad()
                .ClickNewRecordButton();

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .ClickNamedProfessionalLookUpButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("AUTOMATION")
                 .TapSearchButton()
                 .SelectResultElement(_AutomationLeaveAWOLUser1_SystemUserId.ToString());

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .InsertAgreedLeaveDateTime(agreedLeaveDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .InsertAgreedReturnLeaveDateTime(agreedReturnLeaveDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .ClickWhoAuthorisedLeaveIdLookUpButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery("AUTOMATION")
               .TapSearchButton()
               .SelectResultElement(_AutomationLeaveAWOLUser1_SystemUserId.ToString());

            leaveAWOLRecordPage
               .WaitForLeaveAWOLRecordPageToLoad()
               .ClickLeaveTypeLookUpButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery("Automation_LeaveAWOLType")
               .TapSearchButton()
               .SelectResultElement(_inpatientLeaveTypeId.ToString());

            leaveAWOLRecordPage
               .WaitForLeaveAWOLRecordPageToLoad()
               .TapSaveButton();

            leaveAWOLRecordPage
              .WaitForLeaveAWOLRecordPageToLoad();

            System.Threading.Thread.Sleep(5000);

            var leaveAWOL = dbHelper.inpatientLeaveAwol.GetByPersonId(_personID);
            Assert.AreEqual(2, leaveAWOL.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-15012")]
        [Description("Open the Inpatient case with Admission status" + "Navigate to Menu-Related Items-LeaveAWOL" +
           "Open the Leave AWOL record , enter the Actual leave date and save it" + "Enter Actual return date, end reason and save the record" +
            "Validate the record saved and Validate the record is in Inactive")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void LeaveAWOL_UITestMethod15()
        {
            var leaveAWOLRecord = dbHelper.inpatientLeaveAwol.CreateLeaveAWOLRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _personID, _caseId, DateTime.Now.Date, _inpatientLeaveTypeId, DateTime.Now.Date, DateTime.Now.AddDays(1).Date, "systemuser", _AutomationLeaveAWOLUser1_SystemUserId, "Automation  LeaveAWOL Test User 1");

            DateTime actualDate = DateTime.Now.AddDays(-1).Date;
            DateTime actualReturnDate = DateTime.Now.Date;

            loginPage
              .GoToLoginPage()
              .Login("AWOLUser1", "Passw0rd_!")
              ;

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
                 .NavigateToLeaveAWOLPage();


            leavesAWOLPage
                .WaitForLeavesAWOLPageToLoad()
                .OpenLeaveAWOLRecord(leaveAWOLRecord.ToString());

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .InsertActualLeaveDateTime(actualDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .TapSaveAndCloseButton();

            System.Threading.Thread.Sleep(5000);


            leavesAWOLPage
                .WaitForLeavesAWOLPageToLoad()
                .OpenLeaveAWOLRecord(leaveAWOLRecord.ToString());

            leaveAWOLRecordPage
              .WaitForLeaveAWOLRecordPageToLoad()
              .InsertActualReturnLeaveDateTime(actualReturnDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
              .ClickEndReasonLookUpButton();

            lookupPopup
             .WaitForLookupPopupToLoad()
             .TypeSearchQuery("AUTOMATION")
             .TapSearchButton()
             .SelectResultElement(_inpatientLeaveEndReasonId.ToString());

            leaveAWOLRecordPage
             .WaitForLeaveAWOLRecordPageToLoad()
             .TapSaveButton();

            System.Threading.Thread.Sleep(5000);

            var leaveAWOL = dbHelper.inpatientLeaveAwol.GetByPersonId(_personID);
            Assert.AreEqual(1, leaveAWOL.Count);

            var leaveAWOLRecordFields = dbHelper.inpatientLeaveAwol.GetByID(leaveAWOL[0], "personid", "caseid", "actualleavedatetime", "actualreturndatetime", "inpatientleaveendreasonid");

            Assert.AreEqual(_personID.ToString(), leaveAWOLRecordFields["personid"].ToString());
            Assert.AreEqual(_caseId.ToString(), leaveAWOLRecordFields["caseid"].ToString());
            Assert.AreEqual(actualDate, ((DateTime)leaveAWOLRecordFields["actualleavedatetime"]).ToLocalTime().Date);
            Assert.AreEqual(actualReturnDate, ((DateTime)leaveAWOLRecordFields["actualreturndatetime"]).ToLocalTime().Date);
            Assert.AreEqual(_inpatientLeaveEndReasonId.ToString(), leaveAWOLRecordFields["inpatientleaveendreasonid"].ToString());


            leaveAWOLRecordPage
                 .WaitForLeaveAWOLRecordPageToLoad()
                 .ValidateActualReturnDateFieldDisabled()
                 .ValidateEndReasonLookUpButtonDisabled()
                 .ValidateReturnedtotheBedDisabled();


            System.Threading.Thread.Sleep(5000);


            var leaveAWOL_Inactive = dbHelper.inpatientLeaveAwol.GetByPersonId(_personID);
            Assert.AreEqual(1, leaveAWOL_Inactive.Count);

            var leaveAWOLRecordInactiveFields = dbHelper.inpatientLeaveAwol.GetByID(leaveAWOL_Inactive[0], "inactive");

            Assert.AreEqual(true, leaveAWOLRecordInactiveFields["inactive"]);
        }

        [TestProperty("JiraIssueID", "CDV6-15020")]
        [Description("Open the Inpatient case with Admission status" + "Navigate to Menu-Related Items-LeaveAWOL" +
         "Create each record with person AWOL as yes and No " + "User should be able to create records with yes and no option" +
          "Validate the record saved")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void LeaveAWOL_UITestMethod16()
        {
            var leaveAWOLRecord = dbHelper.inpatientLeaveAwol.CreateLeaveAWOLRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _personID, _caseId, DateTime.Now.Date, _inpatientLeaveTypeId, DateTime.Now.Date, DateTime.Now.AddDays(1).Date, "systemuser", _AutomationLeaveAWOLUser1_SystemUserId, "Automation  LeaveAWOL Test User 1");

            DateTime classedDate = DateTime.Now.Date;

            loginPage
              .GoToLoginPage()
              .Login("AWOLUser1", "Passw0rd_!")
              ;

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
                 .NavigateToLeaveAWOLPage();


            leavesAWOLPage
                .WaitForLeavesAWOLPageToLoad()
                .ClickNewRecordButton();

            leaveAWOLRecordPage
              .WaitForLeaveAWOLRecordPageToLoad()
              .ClickPersonAWOL_YesRadioButton()
              .WaitForLeaveAWOLRecordPageToLoad();

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .ValidatePersonAWOLAdditionalFields()
                .SelectPoliceInformed("1")
                .ClickLinkedMissingPersonRecordLookUpButton();


            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("")
                .TapSearchButton()
                .ClickAddSelectedButton(_missingPersonId.ToString());


            leaveAWOLRecordPage
               .WaitForLeaveAWOLRecordPageToLoad()
               .InsertClassedAsAWOLDateTime(classedDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
               .TapSaveAndCloseButton();

            System.Threading.Thread.Sleep(5000);


            var leaveAWOL = dbHelper.inpatientLeaveAwol.GetByPersonId(_personID);
            Assert.AreEqual(2, leaveAWOL.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-15021")]
        [Description("Open the Inpatient case with Admission status" + "Navigate to Menu-Related Items-LeaveAWOL" +
       "Validate the Related Menu Items and Activities sub menu items")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void LeaveAWOL_UITestMethod17()
        {
            var leaveAWOLRecord = dbHelper.inpatientLeaveAwol.CreateLeaveAWOLRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _personID, _caseId, DateTime.Now.Date, _inpatientLeaveTypeId, DateTime.Now.Date, DateTime.Now.AddDays(1).Date, "systemuser", _AutomationLeaveAWOLUser1_SystemUserId, "Automation  LeaveAWOL Test User 1");



            loginPage
              .GoToLoginPage()
              .Login("AWOLUser1", "Passw0rd_!")
              ;

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
                 .NavigateToLeaveAWOLPage();


            leavesAWOLPage
                .WaitForLeavesAWOLPageToLoad()
                .OpenLeaveAWOLRecord(leaveAWOLRecord.ToString());

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .NavigateToActivities()
                .ValidateActivitiesSubMenuAvailable()
                .ValidateRelativeItemsSubMenuAvailable();
        }

        [TestProperty("JiraIssueID", "CDV6-15077")]
        [Description("Open the Inpatient case with Admission status" + "Navigate to Menu-Related Items-LeaveAWOL" +
       "Open the Leave Awol record and Navigate to Attachment area" + "Create new attachment and validate the attachment is saved")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [DeploymentItem("Files\\Document.txt"), DeploymentItem("chromedriver.exe")]
        public void LeaveAWOL_UITestMethod18()
        {
            var leaveAWOLRecord = dbHelper.inpatientLeaveAwol.CreateLeaveAWOLRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _personID, _caseId, DateTime.Now.Date, _inpatientLeaveTypeId, DateTime.Now.Date, DateTime.Now.AddDays(1).Date, "systemuser", _AutomationLeaveAWOLUser1_SystemUserId, "Automation  LeaveAWOL Test User 1");

            DateTime attachmentDate = DateTime.Now.Date;

            loginPage
              .GoToLoginPage()
              .Login("AWOLUser1", "Passw0rd_!")
              ;

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
                 .NavigateToLeaveAWOLPage();


            leavesAWOLPage
                .WaitForLeavesAWOLPageToLoad()
                .OpenLeaveAWOLRecord(leaveAWOLRecord.ToString());

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .NavigateToAttachmentArea();

            inpatientLeaveAwolAttachmentsPage
                .WaitForInpatientLeaveAwolAttachmentsPageToLoad()
                .ClickNewRecordButton();

            inpatientLeaveAwolAttachmentsRecordPage
                .WaitForInpatientLeaveAwolAttachmentsRecordPageToLoad()
                .InsertTitle("Attachment 01")
                .InsertDate(attachmentDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .ClickDocumentTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Inpatient Case Leave AWOL Attachment type")
                .TapSearchButton()
                .SelectResultElement(_documenttypeid.ToString());


            inpatientLeaveAwolAttachmentsRecordPage
               .WaitForInpatientLeaveAwolAttachmentsRecordPageToLoad()
               .ClickDocumentSubTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Inpatient Case Leave AWOL Attachment sub type")
                .TapSearchButton()
                .SelectResultElement(_documentsubtypeid.ToString());

            inpatientLeaveAwolAttachmentsRecordPage
                .WaitForInpatientLeaveAwolAttachmentsRecordPageToLoad()
                .UploadFile(TestContext.DeploymentDirectory + "\\Document.txt")
                .ClickSaveButton();

            inpatientLeaveAwolAttachmentsRecordPage
              .WaitForInpatientLeaveAwolAttachmentsRecordPageToLoad()
              .ClickBackButton();

            System.Threading.Thread.Sleep(5000);

            var attachments = dbHelper.inpatientLeaveAwolAttachment.GetByPersonId(_personID);
            Assert.AreEqual(1, attachments.Count);
            var newAttachment = attachments[0];

            inpatientLeaveAwolAttachmentsPage
                  .WaitForInpatientLeaveAwolAttachmentsPageToLoad()
                  .ValidateNoRecordMessageVisibile(false)
                  .ValidateRecordVisible(newAttachment.ToString());



        }

        [TestProperty("JiraIssueID", "CDV6-15081")]
        [Description("Open the Inpatient case with Admission status" + "Navigate to Menu-Related Items-LeaveAWOL" +
       "Update the inpatient case status to discharge" + "Navigate to Leave AWOL and validate the record is available under Active Leave/AWOLs View  and All Leave/AWOLs View " +
        "Open the respective record and validate the record is in read only.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void LeaveAWOL_UITestMethod19()
        {
            var leaveAWOLRecord = dbHelper.inpatientLeaveAwol.CreateLeaveAWOLRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _personID, _caseId, DateTime.Now.Date, _inpatientLeaveTypeId, DateTime.Now.Date, DateTime.Now.AddDays(1).Date, "systemuser", _AutomationLeaveAWOLUser1_SystemUserId, "Automation  LeaveAWOL Test User 1");

            DateTime dischargeDate = DateTime.Now.Date;


            foreach (var caseid in dbHelper.Case.GetCasesByPersonID(_personID))
            {
                dbHelper.Case.UpdateCaseRecord(caseid, 2, dischargeDate.AddDays(01), caseCloseingReasonid, _actualDischargeDestinationId);
            }

            loginPage
                .GoToLoginPage()
                .Login("AWOLUser1", "Passw0rd_!")
                ;

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
                 .NavigateToLeaveAWOLPage();


            leavesAWOLPage
                .WaitForLeavesAWOLPageToLoad()
                .ValidateNoRecordMessageVisibile(true)
                .SelectViewSelector("Active Leave/AWOLs View")
                .ValidateNoRecordMessageVisibile(true);






            leavesAWOLPage
               .WaitForLeavesAWOLPageToLoad()
               .SelectViewSelector("All Leave/AWOLs View")
               .ValidateNoRecordMessageVisibile(false)
               .ValidateRecordVisible(leaveAWOLRecord.ToString());



            System.Threading.Thread.Sleep(5000);


            var leaveAWOL_Inactive = dbHelper.inpatientLeaveAwol.GetByPersonId(_personID);
            Assert.AreEqual(1, leaveAWOL_Inactive.Count);

            var leaveAWOLRecordInactiveFields = dbHelper.inpatientLeaveAwol.GetByID(leaveAWOL_Inactive[0], "inactive");

            Assert.AreEqual(true, leaveAWOLRecordInactiveFields["inactive"]);
        }


        [TestProperty("JiraIssueID", "CDV6-15152")]
        [Description("Open the Inpatient case with Admission status" + "Navigate to Menu-Related Items-LeaveAWOL" +
        "Update the inpatient case status to discharge" + "Navigate to Leave AWOL and validate the record is available under Leave/AWOL Widget View and  Inactive Leave/AWOLs View  " +
           "Open the respective record and validate the record is in read only.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void LeaveAWOL_UITestMethod20()
        {
            var leaveAWOLRecord = dbHelper.inpatientLeaveAwol.CreateLeaveAWOLRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _personID, _caseId, DateTime.Now.Date, _inpatientLeaveTypeId, DateTime.Now.Date, DateTime.Now.AddDays(1).Date, "systemuser", _AutomationLeaveAWOLUser1_SystemUserId, "Automation  LeaveAWOL Test User 1");

            DateTime dischargeDate = DateTime.Now.Date;
            DateTime classedDate = DateTime.Now.Date;
            DateTime actualReturnDate = DateTime.Now.Date;




            loginPage
                .GoToLoginPage()
                .Login("AWOLUser1", "Passw0rd_!")
                ;

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
                 .NavigateToLeaveAWOLPage();


            leavesAWOLPage
                .WaitForLeavesAWOLPageToLoad()
                .OpenLeaveAWOLRecord(leaveAWOLRecord.ToString());

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .ClickPersonAWOL_YesRadioButton();

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .ValidatePersonAWOLAdditionalFields()
                .InsertClassedAsAWOLDateTime(classedDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .TapSaveButton();

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .InsertActualReturnLeaveDateTime(actualReturnDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .TapSaveButton()
                .WaitForLeaveAWOLRecordPageToLoad();

            foreach (var caseid in dbHelper.Case.GetCasesByPersonID(_personID))
            {
                dbHelper.Case.UpdateCaseRecord(caseid, 2, dischargeDate.AddDays(01), _caseClosureReason2Id, _actualDischargeDestinationId);
            }

            System.Threading.Thread.Sleep(5000);


            var leaveAWOL_Inactive = dbHelper.inpatientLeaveAwol.GetByPersonId(_personID);
            Assert.AreEqual(1, leaveAWOL_Inactive.Count);

            var leaveAWOLRecordInactiveFields = dbHelper.inpatientLeaveAwol.GetByID(leaveAWOL_Inactive[0], "inactive");

            Assert.AreEqual(true, leaveAWOLRecordInactiveFields["inactive"]);


            System.Threading.Thread.Sleep(5000);


            var leaveAWOL = dbHelper.inpatientLeaveAwol.GetByPersonId(_personID);
            Assert.AreEqual(1, leaveAWOL.Count);

            var leaveAWOLRecordFields = dbHelper.inpatientLeaveAwol.GetByID(leaveAWOL[0], "inactive");

            Assert.AreEqual(true, leaveAWOLRecordFields["inactive"]);




        }

        [TestProperty("JiraIssueID", "CDV6-15179")]
        [Description("Open the Inpatient case with Admission status" + "Navigate to Menu-Related Items-LeaveAWOL" +
           "Open the Leave AWOL record , enter the Actual leave date and save it" + "Enter Actual return date, end reason and save the record" +
            "Click No option for Retuened to bed " + "Enter the same Hospital, ward, bay and bed" +
            "Validate the pop up message and error message")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void LeaveAWOL_UITestMethod21()
        {
            var leaveAWOLRecord = dbHelper.inpatientLeaveAwol.CreateLeaveAWOLRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _personID, _caseId, DateTime.Now.Date, _inpatientLeaveTypeId, DateTime.Now.Date, DateTime.Now.AddDays(1).Date, "systemuser", _AutomationLeaveAWOLUser1_SystemUserId, "Automation  LeaveAWOL Test User 1");

            DateTime actualDate = DateTime.Now.AddDays(-1).Date;
            DateTime actualReturnDate = DateTime.Now.Date;

            loginPage
              .GoToLoginPage()
              .Login("AWOLUser1", "Passw0rd_!")
              ;

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
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
                 .NavigateToLeaveAWOLPage();


            leavesAWOLPage
                .WaitForLeavesAWOLPageToLoad()
                .OpenLeaveAWOLRecord(leaveAWOLRecord.ToString());

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .InsertActualLeaveDateTime(actualDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .TapSaveAndCloseButton();

            System.Threading.Thread.Sleep(5000);


            leavesAWOLPage
                   .WaitForLeavesAWOLPageToLoad()
                   .OpenLeaveAWOLRecord(leaveAWOLRecord.ToString());

            leaveAWOLRecordPage
                  .WaitForLeaveAWOLRecordPageToLoad()
                  .InsertActualReturnLeaveDateTime(actualReturnDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                  .ClickEndReasonLookUpButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("AUTOMATION")
                 .TapSearchButton()
                 .SelectResultElement(_inpatientLeaveEndReasonId.ToString());

            leaveAWOLRecordPage
                 .WaitForLeaveAWOLRecordPageToLoad()
                 .ClickReturnedToTheBed_NoOption();

            leaveAWOLRecordPage
                 .WaitForLeaveAWOLRecordPageToLoad()
                 .ClickHospitalLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_provider_Name)
                .TapSearchButton()
                .SelectResultElement(_provider_HospitalId.ToString());

            leaveAWOLRecordPage
                 .WaitForLeaveAWOLRecordPageToLoad()
                 .ClickWardLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_inpationWardName)
                .TapSearchButton()
                .SelectResultElement(_inpationWardId.ToString());

            leaveAWOLRecordPage
                 .WaitForLeaveAWOLRecordPageToLoad()
                 .ClickBayLookUpButton();


            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_inpationBayName)
                .TapSearchButton()
                .SelectResultElement(_inpationBayId.ToString());


            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .ClickBedLookUpButton();


            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery(_inpationBayName)
               .TapSearchButton()
               .SelectResultElement(_inpationBedId.ToString());

            leaveAWOLRecordPage
               .WaitForLeaveAWOLRecordPageToLoad()
               .TapSaveButton();

            alertPopup
                 .WaitForAlertPopupToLoad()
                 .ValidateAlertText("You have moved the patient to a different bed, please refresh the inpatient case to reflect the new information.")
                 .TapOKButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Occupancy details are same as mentioned on Inpatient Case, please change the returned to bed field to yes and continue.");



        }

        [TestProperty("JiraIssueID", "CDV6-15183")]
        [Description("Open the Inpatient case with Admission status" + "Navigate to Menu-Related Items-LeaveAWOL" +
       "Open the Leave AWOL record , Click on Person AWOL yes radio button and Enter the date in Classed as AWOL date" +
       "Save the record and Enter the Actual return date and End reason" + "Validate the record is inactive")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void LeaveAWOL_UITestMethod22()
        {
            var leaveAWOLRecord = dbHelper.inpatientLeaveAwol.CreateLeaveAWOLRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _personID, _caseId, DateTime.Now.Date, _inpatientLeaveTypeId, DateTime.Now.Date, DateTime.Now.AddDays(1).Date, "systemuser", _AutomationLeaveAWOLUser1_SystemUserId, "Automation  LeaveAWOL Test User 1");

            DateTime classedDate = DateTime.Now.Date;
            DateTime actualReturnDate = DateTime.Now.Date;

            loginPage
              .GoToLoginPage()
              .Login("AWOLUser1", "Passw0rd_!")
              ;

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
                 .NavigateToLeaveAWOLPage();


            leavesAWOLPage
                .WaitForLeavesAWOLPageToLoad()
                .OpenLeaveAWOLRecord(leaveAWOLRecord.ToString());

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .ClickPersonAWOL_YesRadioButton();

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .ValidatePersonAWOLAdditionalFields()
                .InsertClassedAsAWOLDateTime(classedDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .TapSaveButton();

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .InsertActualReturnLeaveDateTime(actualReturnDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .ClickEndReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("AWOL Reason_1")
                .TapSearchButton()
                .SelectResultElement(_inpatientLeaveEndReasonAWOLId.ToString());

            leaveAWOLRecordPage
                  .WaitForLeaveAWOLRecordPageToLoad()
                  .TapSaveButton();

            System.Threading.Thread.Sleep(5000);


            var leaveAWOL_Inactive = dbHelper.inpatientLeaveAwol.GetByPersonId(_personID);
            Assert.AreEqual(1, leaveAWOL_Inactive.Count);

            var leaveAWOLRecordInactiveFields = dbHelper.inpatientLeaveAwol.GetByID(leaveAWOL_Inactive[0], "inactive");

            Assert.AreEqual(true, leaveAWOLRecordInactiveFields["inactive"]);

        }

        [TestProperty("JiraIssueID", "CDV6-25000")]
        [Description("Create Person 1 record with Inpatient case status as Admission and Active leave/AWOL record." +
            "Open the Leave AWOL record and update the Actual leave date and save it" +
            "Enter the Actual return date as same as the Actual leave date and save it" +
            "Create Person 2 record with inpatient case as Admission status and Active leave/AWOL record." +
            "Open the Leave Awol record and Enter the Actual leave Date as  same Person 1 Actual leave date and save it." +
            "Enter the Actual return date of person 2 as same as the Actual leave date  of Person 2 and save it" +
            "Click on the Returned to the bed as no option" + "Enter the person 1  hospital, ward , bay and bed" +
            "Click on the save Button and Clickon the Ok button to transfer the Person 2 bed to person 1" +
            "Record should be created successfully and Validate the same.")]

        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void LeaveAWOL_UITestMethod23()
        {

            var leaveAWOLRecord = dbHelper.inpatientLeaveAwol.CreateLeaveAWOLRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _personID, _caseId, DateTime.Now.AddDays(-15).Date, _inpatientLeaveTypeId, DateTime.Now.AddDays(-12).Date, DateTime.Now.AddDays(-10).Date, "systemuser", _AutomationLeaveAWOLUser1_SystemUserId, "Automation  LeaveAWOL Test User 1");


            DateTime actualLeaveDate = DateTime.Now.AddDays(-12).Date;

            loginPage
              .GoToLoginPage()
              .Login("AWOLUser1", "Passw0rd_!")
              ;

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
                 .NavigateToLeaveAWOLPage();


            leavesAWOLPage
                .WaitForLeavesAWOLPageToLoad()
                .OpenLeaveAWOLRecord(leaveAWOLRecord.ToString());

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .InsertActualLeaveDateTime(actualLeaveDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .TapSaveAndCloseButton();


            //Creating new person,new Inpatient case record 

            #region Person 2

            var personFirstName = DateTime.Now.ToString("yyyyMMddHHmmss");
            _personFullName2 = personFirstName + " AutomationLeaveAWOLLastName2";
            _person2ID = commonMethodsDB.CreatePersonRecord(personFirstName, "AutomationLeaveAWOLLastName2", _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));
            _personNumber2 = (int)dbHelper.person.GetPersonById(_person2ID, "personnumber")["personnumber"];

            #endregion 



            #region To Create Inpatient Case record 2


            var caseRecord2Exist = dbHelper.Case.GetCasesByPersonID(_person2ID).Any();
            if (!caseRecord2Exist)
            {

                _case2Id = dbHelper.Case.CreateInpatientCaseRecordWithStatusAsAdmission(_careDirectorQA_TeamId, _person2ID, DateTime.Now.Date, _AutomationLeaveAWOLUser1_SystemUserId, "hdsa", _AutomationLeaveAWOLUser1_SystemUserId, _caseStatusID, _contactReasonId, DateTime.Now.Date, _dataFormListid, _contactSourceId, _inpationWardId, _inpationBayRoom2Id, _inpationBed2Id, _inpatientAdmissionSourceId, _inpationAdmissionMethodId, _AutomationLeaveAWOLUser1_SystemUserId, DateTime.Now.Date, _provider_HospitalId, _inpationWardId, 1, DateTime.Now.Date,
           false, false, false, false, false, false, false, false, false, false);

                _caseNumber2 = (string)dbHelper.Case.GetCaseByID(_case2Id, "casenumber")["casenumber"];


            }
            if (_case2Id == Guid.Empty)
            {
                _case2Id = dbHelper.Case.GetCasesByPersonID(_person2ID).FirstOrDefault();
                _caseNumber2 = (string)dbHelper.Case.GetCaseByID(_case2Id, "casenumber")["casenumber"];
            }


            #endregion To Create Inpatient Case record 2


            var leaveAWOLRecord2 = dbHelper.inpatientLeaveAwol.CreateLeaveAWOLRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _person2ID, _case2Id, DateTime.Now.AddDays(-15).Date, _inpatientLeaveTypeId, DateTime.Now.AddDays(-12).Date, DateTime.Now.AddDays(-11).Date, "systemuser", _AutomationLeaveAWOLUser1_SystemUserId, "Automation  LeaveAWOL Test User 1");

            mainMenu
              .WaitForMainMenuToLoad()
              .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                 .SearchPersonRecordByID(_personNumber2.ToString(), _person2ID.ToString())
                 .OpenPersonRecord(_person2ID.ToString());

            personRecordPage
                 .WaitForPersonRecordPageToLoad()
                 .TapCasesTab();

            personCasesPage
                 .WaitForPersonCasesPageToLoad()
                 .OpenCaseRecord(_case2Id.ToString());

            personCasesRecordPage
                 .WaitForPersonCasesRecordPageToLoad()
                 .NavigateToLeaveAWOLPage();


            leavesAWOLPage
                .WaitForLeavesAWOLPageToLoad()
                .OpenLeaveAWOLRecord(leaveAWOLRecord2.ToString());

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .InsertActualLeaveDateTime(actualLeaveDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .TapSaveButton();

            leaveAWOLRecordPage
               .WaitForLeaveAWOLRecordPageToLoad()
               .InsertActualReturnLeaveDateTime(actualLeaveDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
               .ClickReturnedToTheBed_NoOption()
               .WaitForLeaveAWOLRecordPageToLoad()
               .ClickHospitalLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_provider_Name)
                .TapSearchButton()
                .SelectResultElement(_provider_HospitalId.ToString());

            leaveAWOLRecordPage
                 .WaitForLeaveAWOLRecordPageToLoad()
                 .ClickWardLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_inpationWardName)
                .TapSearchButton()
                .SelectResultElement(_inpationWardId.ToString());

            leaveAWOLRecordPage
                 .WaitForLeaveAWOLRecordPageToLoad()
                 .ClickBayLookUpButton();


            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_inpationBayName)
                .TapSearchButton()
                .SelectResultElement(_inpationBayId.ToString());


            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .ClickBedLookUpButton();


            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery(_inpationBayName)
               .TapSearchButton()
               .SelectResultElement(_inpationBedId.ToString());

            leaveAWOLRecordPage
               .WaitForLeaveAWOLRecordPageToLoad()
               .TapSaveButton();

            alertPopup
               .WaitForAlertPopupToLoad()
               .ValidateAlertText("You have moved the patient to a different bed, please refresh the inpatient case to reflect the new information.")
               .TapOKButton();

            System.Threading.Thread.Sleep(2000);

            var leaveAWOL = dbHelper.inpatientLeaveAwol.GetByPersonId(_person2ID);
            Assert.AreEqual(1, leaveAWOL.Count);

            var leaveAWOLRecordFields = dbHelper.inpatientLeaveAwol.GetByID(leaveAWOL[0], "providerid", "inpatientwardid", "inpatientbayid", "inpatientbedid");

            Assert.AreEqual(_provider_HospitalId.ToString(), leaveAWOLRecordFields["providerid"].ToString());
            Assert.AreEqual(_inpationWardId.ToString(), leaveAWOLRecordFields["inpatientwardid"].ToString());
            Assert.AreEqual(_inpationBayId.ToString(), leaveAWOLRecordFields["inpatientbayid"].ToString());
            Assert.AreEqual(_inpationBedId.ToString(), leaveAWOLRecordFields["inpatientbedid"].ToString());



        }

        [TestProperty("JiraIssueID", "CDV6-15204")]
        [Description("Create Person 1 record with Inpatient case status as Admission and Active leave/AWOL record." +
           "Open the AWOL record and Click on Person AWOL to Yes radio button " +
            "Enter the Classed date and Actual Leave date and save it." +
           "Create Person 2 record with inpatient case as Admission status and Active leave/AWOL record." +
          "Open the AWOL record and Click on Person AWOL to Yes radio button " +
             "Enter the Classed date and Actual Leave date and save it." +
           "Enter the Actual return date of person 2 as same as the Actual leave date  of Person 2 and save it" +
           "Click on the Returned to the bed as no option" + "Enter the person 1  hospital, ward , bay and bed" +
           "Click on the save Button and Clickon the Ok button to transfer the Person 2 bed to person 1" +
           "Record should be created successfully and Validate the same.")]

        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void LeaveAWOL_UITestMethod24()
        {

            var leaveAWOLRecord = dbHelper.inpatientLeaveAwol.CreateLeaveAWOLRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _personID, _caseId, DateTime.Now.AddDays(-15).Date, _inpatientLeaveTypeId, DateTime.Now.AddDays(-12).Date, DateTime.Now.AddDays(-10).Date, "systemuser", _AutomationLeaveAWOLUser1_SystemUserId, "Automation  LeaveAWOL Test User 1");


            DateTime ClassedLeaveDate = DateTime.Now.AddDays(-12).Date;

            loginPage
              .GoToLoginPage()
              .Login("AWOLUser1", "Passw0rd_!")
              ;

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
                 .NavigateToLeaveAWOLPage();


            leavesAWOLPage
                .WaitForLeavesAWOLPageToLoad()
                .OpenLeaveAWOLRecord(leaveAWOLRecord.ToString());

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .ClickPersonAWOL_YesRadioButton()
                .InsertClassedAsAWOLDateTime(ClassedLeaveDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .TapSaveAndCloseButton();




            //Creating new person,new Inpatient case record 

            #region Person 2

            var personFirstName = DateTime.Now.ToString("yyyyMMddHHmmss");
            _personFullName2 = personFirstName + " AutomationLeaveAWOLLastName2";
            _person2ID = commonMethodsDB.CreatePersonRecord(personFirstName, "AutomationLeaveAWOLLastName2", _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));
            _personNumber2 = (int)dbHelper.person.GetPersonById(_person2ID, "personnumber")["personnumber"];

            #endregion Person 2



            #region To Create Inpatient Case record 2


            var caseRecord2Exist = dbHelper.Case.GetCasesByPersonID(_person2ID).Any();
            if (!caseRecord2Exist)
            {

                _case2Id = dbHelper.Case.CreateInpatientCaseRecordWithStatusAsAdmission(_careDirectorQA_TeamId, _person2ID, DateTime.Now.Date, _AutomationLeaveAWOLUser1_SystemUserId, "hdsa", _AutomationLeaveAWOLUser1_SystemUserId, _caseStatusID, _contactReasonId, DateTime.Now.Date, _dataFormListid, _contactSourceId, _inpationWardId, _inpationBayRoom2Id, _inpationBed2Id, _inpatientAdmissionSourceId, _inpationAdmissionMethodId, _AutomationLeaveAWOLUser1_SystemUserId, DateTime.Now.Date, _provider_HospitalId, _inpationWardId, 1, DateTime.Now.Date,
           false, false, false, false, false, false, false, false, false, false);

                _caseNumber2 = (string)dbHelper.Case.GetCaseByID(_case2Id, "casenumber")["casenumber"];


            }
            if (_case2Id == Guid.Empty)
            {
                _case2Id = dbHelper.Case.GetCasesByPersonID(_person2ID).FirstOrDefault();
                _caseNumber2 = (string)dbHelper.Case.GetCaseByID(_case2Id, "casenumber")["casenumber"];
            }


            #endregion To Create Inpatient Case record 2


            var leaveAWOLRecord2 = dbHelper.inpatientLeaveAwol.CreateLeaveAWOLRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _person2ID, _case2Id, DateTime.Now.AddDays(-15).Date, _inpatientLeaveTypeId, DateTime.Now.AddDays(-12).Date, DateTime.Now.AddDays(-11).Date, "systemuser", _AutomationLeaveAWOLUser1_SystemUserId, "Automation  LeaveAWOL Test User 1");

            mainMenu
              .WaitForMainMenuToLoad()
              .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                 .SearchPersonRecordByID(_personNumber2.ToString(), _person2ID.ToString())
                 .OpenPersonRecord(_person2ID.ToString());

            personRecordPage
                 .WaitForPersonRecordPageToLoad()
                 .TapCasesTab();

            personCasesPage
                 .WaitForPersonCasesPageToLoad()
                 .OpenCaseRecord(_case2Id.ToString());

            personCasesRecordPage
                 .WaitForPersonCasesRecordPageToLoad()
                 .NavigateToLeaveAWOLPage();


            leavesAWOLPage
                .WaitForLeavesAWOLPageToLoad()
                .OpenLeaveAWOLRecord(leaveAWOLRecord2.ToString());

            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .ClickPersonAWOL_YesRadioButton()
                .InsertClassedAsAWOLDateTime(ClassedLeaveDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .TapSaveButton()
                .InsertActualReturnLeaveDateTime(ClassedLeaveDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .ClickReturnedToTheBed_NoOption()
                .WaitForLeaveAWOLRecordPageToLoad()
                .ClickHospitalLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_provider_Name)
                .TapSearchButton()
                .SelectResultElement(_provider_HospitalId.ToString());

            leaveAWOLRecordPage
                 .WaitForLeaveAWOLRecordPageToLoad()
                 .ClickWardLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_inpationWardName)
                .TapSearchButton()
                .SelectResultElement(_inpationWardId.ToString());

            leaveAWOLRecordPage
                 .WaitForLeaveAWOLRecordPageToLoad()
                 .ClickBayLookUpButton();


            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_inpationBayName)
                .TapSearchButton()
                .SelectResultElement(_inpationBayId.ToString());


            leaveAWOLRecordPage
                .WaitForLeaveAWOLRecordPageToLoad()
                .ClickBedLookUpButton();


            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery(_inpationBayName)
               .TapSearchButton()
               .SelectResultElement(_inpationBedId.ToString());

            leaveAWOLRecordPage
               .WaitForLeaveAWOLRecordPageToLoad()
               .TapSaveButton();

            alertPopup
               .WaitForAlertPopupToLoad()
               .ValidateAlertText("You have moved the patient to a different bed, please refresh the inpatient case to reflect the new information.")
               .TapOKButton();

            System.Threading.Thread.Sleep(2000);

            var leaveAWOL = dbHelper.inpatientLeaveAwol.GetByPersonId(_person2ID);
            Assert.AreEqual(1, leaveAWOL.Count);

            var leaveAWOLRecordFields = dbHelper.inpatientLeaveAwol.GetByID(leaveAWOL[0], "providerid", "inpatientwardid", "inpatientbayid", "inpatientbedid");

            Assert.AreEqual(_provider_HospitalId.ToString(), leaveAWOLRecordFields["providerid"].ToString());
            Assert.AreEqual(_inpationWardId.ToString(), leaveAWOLRecordFields["inpatientwardid"].ToString());
            Assert.AreEqual(_inpationBayId.ToString(), leaveAWOLRecordFields["inpatientbayid"].ToString());
            Assert.AreEqual(_inpationBedId.ToString(), leaveAWOLRecordFields["inpatientbedid"].ToString());



        }




        #endregion

    }

}


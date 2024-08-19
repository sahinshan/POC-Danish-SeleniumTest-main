using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Phoenix.UITests.Cases
{
    [TestClass]

    public class Case_HealthAppointmentsContactOffers_UITestCases : FunctionalTest
    {

        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid AutomationCDV617002User1_SystemUserId;
        private Guid AutomationCDV617002User2_SystemUserId;
        private Guid adminUserId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _ethnicityId;
        private Guid _contactReasonId;
        private Guid _contactSourceId;
        private Guid _personID;
        private int _personNumber;
        private string _firstName;
        private string _lastName;
        private Guid _person2ID;
        private int _personNumber2;
        private string _personFullName;
        private string _personFullName2;
        private Guid _dataFormId_HealthAppointmentType;
        private Guid _provider_HospitalId;
        private Guid _caseStatusId;
        private Guid _communityAndClinicTeamId;
        private Guid _communityClinicDiaryViewSetupId;
        private Guid _providerId_Carer;
        private Guid _caseId;
        private Guid _case2Id;
        private Guid _contactAdministrativeCategory;
        private Guid _caseServiceTypeRequestedid;
        private Guid _dataFormId_CommunityHealthCase;
        private Guid _communityClinicAppointmentContactTypesId;
        private Guid _communityClinicLocationTypesId;
        private Guid _providerRoomsId;
        private Guid _recurrencePatternId;
        private Guid _providerClinicRoom;
        private Guid _healthAppointmentReasonId;
        private string linkedProfessional_title = "Automation CDV6-17002 Test User 1";
        private Guid _linkedProfessional;
        private string _CommunityclinicTeams_Title = "CareDirector QA Community And Clinic Team Health Appointment Team 2";
        private Guid _healthAppointmentAttendeeAdvocateTypeId;
        private Guid _healthAppointmentOutcomeTypeId;
        private Guid _communityClinicCareInterventionId;
        public Guid adminUserID1;
        private Guid _workflowId;
        private Guid _HealthAppointmentBusinessObjectId;
        private string _loginUsername;
        private string _systemUsername;


        [TestInitialize()]
        public void TestInitializationMethod()
        {
            try
            {

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").First();

                #endregion

                #region Health Appointment Type Id

                _dataFormId_HealthAppointmentType = dbHelper.dataForm.GetByName("Unscheduled Appointment").FirstOrDefault();

                #endregion Health Appointment Type Id

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

                #region System User 1

                var automationCDV617002TestUSer1Exists = dbHelper.systemUser.GetSystemUserByUserName("UnscheduleAppointment_CDV6_17002").Any();
                if (!automationCDV617002TestUSer1Exists)
                {
                    AutomationCDV617002User1_SystemUserId = dbHelper.systemUser.CreateSystemUser("UnscheduleAppointment_CDV6_17002", "Automation", "CDV6-17002 Test User 1", "Automation CDV6-17002 Test User 1", "Passw0rd_!", "UnscheduleAppointment_CDV6_17002@somemail.com", "UnscheduleAppointment_CDV6_17002@secureemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, DateTime.Now.Date);

                    var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();
                    var systemUserUnscheduledAppointmentsProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Unscheduled Health Appointments").First();

                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(AutomationCDV617002User1_SystemUserId, systemAdministratorSecurityProfileId);
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(AutomationCDV617002User1_SystemUserId, systemUserSecureFieldsSecurityProfileId);
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(AutomationCDV617002User1_SystemUserId, systemUserUnscheduledAppointmentsProfileId);
                }
                if (AutomationCDV617002User1_SystemUserId == Guid.Empty)
                {
                    AutomationCDV617002User1_SystemUserId = dbHelper.systemUser.GetSystemUserByUserName("UnscheduleAppointment_CDV6_17002").FirstOrDefault();
                }

                adminUserId = dbHelper.systemUser.GetSystemUserByUserName("UnscheduleAppointment_CDV6_17002").FirstOrDefault();
                dbHelper.systemUser.UpdateLastPasswordChangedDate(adminUserId, DateTime.Now.Date);
                dbHelper.systemUser.UpdateLastPasswordChangedDate(AutomationCDV617002User1_SystemUserId, DateTime.Now.Date);
                _loginUsername = "UnscheduleAppointment_CDV6_17002";

                #endregion System User 1

                #region System User 2

                var automationCDV617002TestUSer2Exists = dbHelper.systemUser.GetSystemUserByUserName("Automation_HealthAppointment_Test_User_2").Any();
                if (!automationCDV617002TestUSer2Exists)
                {
                    AutomationCDV617002User2_SystemUserId = dbHelper.systemUser.CreateSystemUser("Automation_HealthAppointment_Test_User_2", "Automation", "Health Test User 2", "Automation Health Test User 2", "Passw0rd_!", "Automation_CDV6-17002_Test_User_2@somemail.com", "Automation_HealthAppointment_Test_User_2_Test_User_2@secureemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, DateTime.Now.Date);

                    var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();


                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(AutomationCDV617002User2_SystemUserId, systemAdministratorSecurityProfileId);
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(AutomationCDV617002User2_SystemUserId, systemUserSecureFieldsSecurityProfileId);

                }
                if (AutomationCDV617002User2_SystemUserId == Guid.Empty)
                {
                    AutomationCDV617002User2_SystemUserId = dbHelper.systemUser.GetSystemUserByUserName("Automation_HealthAppointment_Test_User_2").FirstOrDefault();
                }

                adminUserId = dbHelper.systemUser.GetSystemUserByUserName("Automation_HealthAppointment_Test_User_2").FirstOrDefault();
                dbHelper.systemUser.UpdateLastPasswordChangedDate(adminUserId, DateTime.Now.Date);
                dbHelper.systemUser.UpdateLastPasswordChangedDate(AutomationCDV617002User2_SystemUserId, DateTime.Now.Date);
                _systemUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(AutomationCDV617002User2_SystemUserId, "username")["username"];


                #endregion System User 2

                #region Ethnicity

                var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("Appointment_Ethnicity").Any();
                if (!ethnicitiesExist)
                    dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "Appointment_Ethnicity", new DateTime(2020, 1, 1));
                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Appointment_Ethnicity")[0];

                #endregion Ethnicity

                #region Contact Reason

                var contactReasonExists = dbHelper.contactReason.GetByName("Appointment_ContactReason").Any();
                if (!contactReasonExists)
                    dbHelper.contactReason.CreateContactReason(_careDirectorQA_TeamId, "Appointment_ContactReason", new DateTime(2020, 1, 1), 140000001, false);
                _contactReasonId = dbHelper.contactReason.GetByName("Appointment_ContactReason")[0];

                #endregion Contact Reason

                #region Contact Source

                var contactSourceExists = dbHelper.contactSource.GetByName("Appointment_ContactSource").Any();
                if (!contactSourceExists)
                    dbHelper.contactSource.CreateContactSource(_careDirectorQA_TeamId, "Appointment_ContactSource", new DateTime(2020, 1, 1));
                _contactSourceId = dbHelper.contactSource.GetByName("Appointment_ContactSource")[0];

                #endregion Contact Source

                #region Recurrence pattern

                var recurrencePatternExists = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").Any();
                if (!recurrencePatternExists)
                    _recurrencePatternId = dbHelper.recurrencePattern.CreateRecurrencePattern(1, 1);

                if (_recurrencePatternId == Guid.Empty)
                    _recurrencePatternId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").FirstOrDefault();

                #endregion

                #region Provider_Hospital

                var providerHospitalExists = dbHelper.provider.GetProviderByName("Automation_Provider_HealthAppointments").Any();
                if (!providerHospitalExists)
                    dbHelper.provider.CreateProvider("Automation_Provider_HealthAppointments", _careDirectorQA_TeamId);
                _provider_HospitalId = dbHelper.provider.GetProviderByName("Automation_Provider_HealthAppointments")[0];

                #endregion Provider_Hospital

                #region Providers Room

                var providerRoomsExists = dbHelper.providerRoom.GetProviderRoomByName("Automation_Provider_HealthAppointments_ClinicRooms").Any();
                if (!providerRoomsExists)
                    dbHelper.providerRoom.CreateProviderRoom("Automation_Provider_HealthAppointments_ClinicRooms", _careDirectorQA_TeamId, _provider_HospitalId);
                _providerRoomsId = dbHelper.providerRoom.GetProviderRoomByName("Automation_Provider_HealthAppointments_ClinicRooms")[0];

                #endregion

                #region Provider (Carer)

                var carerProviderExists = dbHelper.provider.GetProviderByName("CareDirector QA Provider Health Appointment").Any();
                if (!carerProviderExists)
                {
                    _providerId_Carer = dbHelper.provider.CreateProvider("CareDirector QA Provider Health Appointment", _careDirectorQA_TeamId, 7);

                }
                if (_providerId_Carer == Guid.Empty)
                {
                    _providerId_Carer = dbHelper.provider.GetProviderByName("CareDirector QA Provider Health Appointment").FirstOrDefault();
                }

                #endregion

                #region Community and Clinic Team

                var communityAndClinicTeamExists = dbHelper.communityAndClinicTeam.GetByTitle(_CommunityclinicTeams_Title).Any();
                if (!communityAndClinicTeamExists)
                {
                    _communityAndClinicTeamId = dbHelper.communityAndClinicTeam.CreateCommunityAndClinicTeam(_careDirectorQA_TeamId, _providerId_Carer, _careDirectorQA_TeamId, _CommunityclinicTeams_Title, "Created by Health Appointments");
                    _communityClinicDiaryViewSetupId = dbHelper.communityClinicDiaryViewSetup.CreateCommunityClinicDiaryViewSetupWithNoHomeVisit(_careDirectorQA_TeamId, _communityAndClinicTeamId, "CommunityAndClinicTeamNoHomeVisit", new DateTime(2020, 1, 1), new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), 500, 100, 500, _provider_HospitalId);
                }
                if (_communityClinicDiaryViewSetupId == Guid.Empty)
                {
                    _communityAndClinicTeamId = dbHelper.communityAndClinicTeam.GetByTitle(_CommunityclinicTeams_Title).FirstOrDefault();
                    _communityClinicDiaryViewSetupId = dbHelper.communityClinicDiaryViewSetup.GetByTitle("CommunityAndClinicTeamNoHomeVisit").FirstOrDefault();
                }

                #endregion

                #region Health Appointment Reason

                _healthAppointmentReasonId = commonMethodsDB.CreateHealthAppointmentReason(new Guid("05786b1b-a6ae-ed11-83ea-0a7be9a500fe"), _careDirectorQA_TeamId, "Assessment", new DateTime(2020, 1, 1), "1", null);

                #endregion

                #region Contact Administrative Category

                var contactAdministrativeCategoryExists = dbHelper.contactAdministrativeCategory.GetByName("NHS Patient").Any();

                if (!contactAdministrativeCategoryExists)
                    _contactAdministrativeCategory = dbHelper.contactAdministrativeCategory.CreateContactAdministrativeCategory(_careDirectorQA_TeamId, "NHS Patient", new DateTime(2020, 1, 1));

                if (_contactAdministrativeCategory == Guid.Empty)
                    _contactAdministrativeCategory = dbHelper.contactAdministrativeCategory.GetByName("NHS Patient").FirstOrDefault();

                #endregion

                #region Case Service Type Requested

                var caseServiceTypeRequestedExists = dbHelper.caseServiceTypeRequested.GetByName("Advice and Consultation").Any();

                if (!caseServiceTypeRequestedExists)
                    _caseServiceTypeRequestedid = dbHelper.caseServiceTypeRequested.CreateCaseServiceTypeRequested(_careDirectorQA_TeamId, "Advice and Consultation", new DateTime(2020, 1, 1));

                if (_caseServiceTypeRequestedid == Guid.Empty)
                    _caseServiceTypeRequestedid = dbHelper.caseServiceTypeRequested.GetByName("Advice and Consultation").FirstOrDefault();

                #endregion

                #region Data Form Community Health Case

                _dataFormId_CommunityHealthCase = dbHelper.dataForm.GetByName("CommunityHealthCase").FirstOrDefault();

                #endregion

                #region Community/Clinic Appointment Contact Types

                var communityClinicAppointmentContactTypesExists = dbHelper.healthAppointmentContactType.GetByName("Appointment_Test Automation").Any();
                if (!communityClinicAppointmentContactTypesExists)
                    dbHelper.healthAppointmentContactType.CreateHealthAppointmentContactType(_careDirectorQA_TeamId, "Appointment_Test Automation", new DateTime(2020, 1, 1), "3");
                _communityClinicAppointmentContactTypesId = dbHelper.healthAppointmentContactType.GetByName("Appointment_Test Automation")[0];

                #endregion Community/Clinic Appointment Contact Types


                #region Community/Clinic Appointment Location Types

                var communityClinicAppointmentLocationTypesExists = dbHelper.healthAppointmentLocationType.GetByName("Appointment_Test Automation_Location").Any();
                if (!communityClinicAppointmentLocationTypesExists)
                    dbHelper.healthAppointmentLocationType.CreateHealthAppointmentLocationType(_careDirectorQA_TeamId, "Appointment_Test Automation_Location", new DateTime(2020, 1, 1));
                _communityClinicLocationTypesId = dbHelper.healthAppointmentLocationType.GetByName("Appointment_Test Automation_Location")[0];

                #endregion Community/Clinic Appointment Location Types

                #region Health Appointment Attendee Advocate Type

                var healthAppointmentAttendeeAdvocateType = dbHelper.healthAppointmentAttendeeAdvocateType.GetByName("Appointment_Test Automation_AttendeeAdvocateType").Any();
                if (!healthAppointmentAttendeeAdvocateType)
                    dbHelper.healthAppointmentAttendeeAdvocateType.CreateHealthAppointmentAttendeeAdvocateType(_careDirectorQA_TeamId, "Appointment_Test Automation_AttendeeAdvocateType", new DateTime(2020, 1, 1));
                _healthAppointmentAttendeeAdvocateTypeId = dbHelper.healthAppointmentAttendeeAdvocateType.GetByName("Appointment_Test Automation_AttendeeAdvocateType")[0];

                #endregion  Health Appointment Attendee Advocate Type

                #region Health Appointment Outcome Type

                var healthAppointmentOutcomeType = dbHelper.healthAppointmentOutcomeType.GetByName("Appointment_Test Automation_OutcomeType").Any();
                if (!healthAppointmentOutcomeType)
                    dbHelper.healthAppointmentOutcomeType.CreateHealthAppointmentOutcomeType(_careDirectorQA_TeamId, "Appointment_Test Automation_OutcomeType", new DateTime(2020, 1, 1));
                _healthAppointmentOutcomeTypeId = dbHelper.healthAppointmentOutcomeType.GetByName("Appointment_Test Automation_OutcomeType")[0];

                #endregion Health Appointment Outcome Type

                #region Community Clinic CareIntervention

                var communityClinicCareIntervention = dbHelper.communityClinicCareIntervention.GetByName("Appointment_Test_Automation_CareIntervention").Any();
                if (!communityClinicCareIntervention)
                    dbHelper.communityClinicCareIntervention.CreateCommunityClinicCareIntervention(_careDirectorQA_TeamId, "Appointment_Test_Automation_CareIntervention", new DateTime(2020, 1, 1));
                _communityClinicCareInterventionId = dbHelper.communityClinicCareIntervention.GetByName("Appointment_Test_Automation_CareIntervention")[0];

                #endregion Health Appointment Outcome Type

                #region Person 1

                _firstName = "Jhon";
                _lastName = DateTime.Now.ToString("yyyyMMdd.HHmmss");
                _personFullName = _firstName + " " + _lastName;

                _personID = dbHelper.person.CreatePersonRecord("", _firstName, "", _lastName, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
                _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

                #endregion

                #region Case Status

                _caseStatusId = dbHelper.caseStatus.GetByName("Waiting List").First();

                #endregion

                #region Community Case record 1

                var caseRecordsExist = dbHelper.Case.GetCasesByPersonID(_personID).Any();
                if (!caseRecordsExist)
                {

                    _caseId = dbHelper.Case.CreateCommunityHealthCaseRecord(_careDirectorQA_TeamId, _personID, AutomationCDV617002User1_SystemUserId, _communityAndClinicTeamId, AutomationCDV617002User1_SystemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                        _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-3).Date, DateTime.Now.AddDays(-2).Date, DateTime.Now.Date, "Unscheduled Appointments");

                }
                if (_caseId == Guid.Empty)
                {
                    _caseId = dbHelper.Case.GetCasesByPersonID(_personID).FirstOrDefault();
                }

                _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();

                #endregion Community Case record 1

                #region Person 2

                var personRecord2Exists = dbHelper.person.GetByFirstName("Automation_Health Appointment_Person 2").Any();
                if (!personRecord2Exists)
                {
                    _person2ID = dbHelper.person.CreatePersonRecord("", "Automation_Health Appointment_Person 2", "", "Health Appointment", "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
                    _personNumber2 = (int)dbHelper.person.GetPersonById(_person2ID, "personnumber")["personnumber"];
                }
                if (_person2ID == Guid.Empty)
                {
                    _person2ID = dbHelper.person.GetByFirstName("Automation_Health Appointment_Person 2").FirstOrDefault();
                    _personNumber2 = (int)dbHelper.person.GetPersonById(_person2ID, "personnumber")["personnumber"];
                }
                _personFullName2 = "Automation_Health Appointment_Person 2 Health Appointment";

                #endregion Person 2

                #region Community Case record 2

                var caseRecord2Exist = dbHelper.Case.GetCasesByPersonID(_person2ID).Any();
                if (!caseRecord2Exist)
                {

                    _case2Id = dbHelper.Case.CreateCommunityHealthCaseRecord(_careDirectorQA_TeamId, _person2ID, AutomationCDV617002User2_SystemUserId, _communityAndClinicTeamId, AutomationCDV617002User2_SystemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                                    _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-3).Date, DateTime.Now.AddDays(-2).Date, DateTime.Now.Date, "Unscheduled Appointments Health");

                }
                if (_case2Id == Guid.Empty)
                {
                    _case2Id = dbHelper.Case.GetCasesByPersonID(_person2ID).FirstOrDefault();

                }

                #endregion Community Case record 2

                #region Clinic Room
                var clinicRoomExists = dbHelper.clinicRoom.GetClinicRoomByTitle("Automation_Provider_HealthAppointments_ClinicRooms").Any();
                if (!clinicRoomExists)
                    dbHelper.clinicRoom.CreateClinicRoom(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _communityClinicDiaryViewSetupId, _providerRoomsId, _recurrencePatternId,
                                                DateTime.Now.AddMonths(-1).Date, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0));
                _providerClinicRoom = dbHelper.clinicRoom.GetClinicRoomByTitle("Automation_Provider_HealthAppointments_ClinicRooms").FirstOrDefault();

                #endregion  Clinic Room

                #region Community Clinic Linked Professional

                var IDs = dbHelper.communityClinicLinkedProfessional.GetLinkedProfessionalByID(_communityClinicDiaryViewSetupId);
                foreach (var ID in IDs)
                {
                    dbHelper.communityClinicLinkedProfessional.DeleteCommunityClinicLinkedProfessional(ID);
                }


                var linkedProfessionalsExists = dbHelper.communityClinicLinkedProfessional.GetByTitle(linkedProfessional_title).Any();
                if (!linkedProfessionalsExists)
                    dbHelper.communityClinicLinkedProfessional.CreateCommunityClinicLinkedProfessional(_careDirectorQA_TeamId, _communityClinicDiaryViewSetupId, AutomationCDV617002User1_SystemUserId, DateTime.Now.AddDays(-30).Date, new TimeSpan(1, 0, 0),
                                                                new TimeSpan(23, 0, 0), _recurrencePatternId, linkedProfessional_title);

                _linkedProfessional = dbHelper.communityClinicLinkedProfessional.GetByTitle(linkedProfessional_title).FirstOrDefault();

                #endregion Community Clinic Linked Professional

                #region Business Object

                _HealthAppointmentBusinessObjectId = dbHelper.businessObject.GetBusinessObjectByName("HealthAppointment").FirstOrDefault();

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

        #region https://advancedcsg.atlassian.net/browse/CDV6-17002

        [TestProperty("JiraIssueID", "CDV6-17190")]
        [Description("Login CD application with security profiles Add CW Unscheduled Health Appointments " + "Open person record in test data -> click on cases -> Open Community case -> Menu -> Related Items -> Click on Health Appointments/ Contacts & offers ->" +
            " click on + ->  Verify that Unscheduled Appointments is displaying in Select Health Appointment Type pop up.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void HealthAppointmentsContactOffers_UITestCases01()
        {
            foreach (var healthAppointmentCaseNoteId in dbHelper.healthAppointmentCaseNote.GetHealthAppointmentCaseNoteByPersonId(_personID))
            {
                dbHelper.healthAppointmentCaseNote.DeleteHealthAppointmentCaseNote(healthAppointmentCaseNoteId);
            }

            foreach (var healthAppointmentId in dbHelper.healthAppointment.GetHealthAppointmentByCaseID(_caseId))
            {
                foreach (var bookedslotid in dbHelper.clinicBookedSlot.GetByHealthAppointmentID(healthAppointmentId))
                {
                    dbHelper.clinicBookedSlot.DeleteClinicBookedSlot(bookedslotid);
                }

                dbHelper.healthAppointment.DeleteHealthAppointment(healthAppointmentId);
            }


            #region Step 1 and Step 2           

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!")
              .WaitFormHomePageToLoad();

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                 .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                 .OpenPersonRecord(_personID.ToString());

            personRecordPage
                 .WaitForSystemUserPersonRecordPageToLoad()
                 .TapCasesTab();

            personCasesPage
                 .WaitForPersonCasesPageToLoad()
                 .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                 .WaitForPersonCasesRecordPageToLoad()
                 .NavigateToHealthAppointmentsPage();

            System.Threading.Thread.Sleep(3000);

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickNewRecordButton();

            System.Threading.Thread.Sleep(3000);

            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .ValidateHealthAppointmentOption("Unscheduled Appointment");

            #endregion Step 1 & Step 2

        }


        [TestProperty("JiraIssueID", "CDV6-17194")]
        [Description("Login CD application with security profiles Add CW Unscheduled Health Appointments " + "Open person record in test data -> click on cases -> Open Community case -> Menu -> Related Items -> Click on Health Appointments/ Contacts & offers ->" +
            " click on + -> Select  Unscheduled Appointments in Select Health Appointment Type pop up." + "Enter all the mandatory fields" +
            "Enter the start date and start time greater than the current time" + " Click on Save" + "Validate the error message: Start Date/Time cannot be in the future.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void HealthAppointmentsContactOffers_UITestCases02()
        {

            foreach (var healthAppointmentCaseNoteId in dbHelper.healthAppointmentCaseNote.GetHealthAppointmentCaseNoteByPersonId(_personID))
            {
                dbHelper.healthAppointmentCaseNote.DeleteHealthAppointmentCaseNote(healthAppointmentCaseNoteId);
            }

            foreach (var healthAppointmentId in dbHelper.healthAppointment.GetHealthAppointmentByCaseID(_caseId))
            {
                foreach (var bookedslotid in dbHelper.clinicBookedSlot.GetByHealthAppointmentID(healthAppointmentId))
                {
                    dbHelper.clinicBookedSlot.DeleteClinicBookedSlot(bookedslotid);
                }

                dbHelper.healthAppointment.DeleteHealthAppointment(healthAppointmentId);
            }


            #region step 3

            loginPage
                  .GoToLoginPage()
                  .Login(_loginUsername, "Passw0rd_!")
                  .WaitFormHomePageToLoad();

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToPeopleSection();

            peoplePage.
                  WaitForPeoplePageToLoad()
                 .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                 .OpenPersonRecord(_personID.ToString());

            System.Threading.Thread.Sleep(5000);

            personRecordPage
                    .WaitForSystemUserPersonRecordPageToLoad()
                    .TapCasesTab();

            System.Threading.Thread.Sleep(5000);

            personCasesPage
                    .WaitForPersonCasesPageToLoad()
                    .WaitForPersonCasesPageToLoad()
                    .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                  .WaitForPersonCasesRecordPageToLoad()
                  .NavigateToHealthAppointmentsPage();


            System.Threading.Thread.Sleep(5000);

            caseHealthAppointmentsPage
                 .WaitForCaseHealthAppointmentsPageToLoad()
                 .WaitForCaseHealthAppointmentsPageToLoad()
                 .ClickNewRecordButton();

            System.Threading.Thread.Sleep(3000);


            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Unscheduled Appointment")
                .TapNextButton();

            System.Threading.Thread.Sleep(3000);

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickContactTypeLookUpButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("Appointment_Test Automation")
                 .TapSearchButton()
                 .SelectResultElement(_communityClinicAppointmentContactTypesId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickhealthAppointmentReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Assessment")
                .TapSearchButton()
                .SelectResultElement(_healthAppointmentReasonId.ToString());



            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad()
               .InsertStartDate(DateTime.Now.AddDays(2).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .InsertStartTime("09:30")
               .InsertEndDate(DateTime.Now.AddDays(4).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .InsertEndTime("11:30")
               .ClickLocationTypesLookUpButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("Appointment_Test Automation_Location")
                 .TapSearchButton()
                 .SelectResultElement(_communityClinicLocationTypesId.ToString());

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad()
               .ClickLeadProfessionalLookUpButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery(_loginUsername)
               .TapSearchButton()
               .SelectResultElement(AutomationCDV617002User1_SystemUserId.ToString());


            caseHealthAppointmentRecordPage
                 .WaitForCaseHealthAppointmentRecordPageToLoad()
                 .TapSaveButton();

            dynamicDialogPopup
                 .WaitForDynamicDialogPopupToLoad()
                 .ValidateMessage("Start Date/Time cannot be in the future.")
                 .TapCloseButton();

            #endregion Step 3






        }


        [TestProperty("JiraIssueID", "CDV6-17195")]
        [Description("Login CD application with security profiles Add CW Unscheduled Health Appointments " + "Open person record in test data -> click on cases -> Open Community case -> Menu -> Related Items -> Click on Health Appointments/ Contacts & offers ->" +
            " click on + -> Select  Unscheduled Appointments in Select Health Appointment Type pop up." + "Enter all the mandatory fields" +
            "Enter the start date and start time greater than the current time" + " Click on Save" + "Validate the error message: Start Date/Time cannot be after End Date/Time.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void HealthAppointmentsContactOffers_UITestCases03()
        {
            foreach (var healthAppointmentCaseNoteId in dbHelper.healthAppointmentCaseNote.GetHealthAppointmentCaseNoteByPersonId(_personID))
            {
                dbHelper.healthAppointmentCaseNote.DeleteHealthAppointmentCaseNote(healthAppointmentCaseNoteId);
            }

            foreach (var healthAppointmentId in dbHelper.healthAppointment.GetHealthAppointmentByCaseID(_caseId))
            {
                foreach (var bookedslotid in dbHelper.clinicBookedSlot.GetByHealthAppointmentID(healthAppointmentId))
                {
                    dbHelper.clinicBookedSlot.DeleteClinicBookedSlot(bookedslotid);
                }

                dbHelper.healthAppointment.DeleteHealthAppointment(healthAppointmentId);
            }

            #region step 4

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!")
              .WaitFormHomePageToLoad();

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()

                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                 .OpenPersonRecord(_personID.ToString());

            personRecordPage
                 .WaitForSystemUserPersonRecordPageToLoad()
                 .TapCasesTab();

            personCasesPage
                 .WaitForPersonCasesPageToLoad()
                 .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                 .WaitForPersonCasesRecordPageToLoad()
                 .NavigateToHealthAppointmentsPage();


            System.Threading.Thread.Sleep(3000);
            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickNewRecordButton();

            System.Threading.Thread.Sleep(3000);


            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Unscheduled Appointment")
                .TapNextButton();

            System.Threading.Thread.Sleep(3000);

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickContactTypeLookUpButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("Appointment_Test Automation")
                 .TapSearchButton()
                 .SelectResultElement(_communityClinicAppointmentContactTypesId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickhealthAppointmentReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Assessment")
                .TapSearchButton()
                .SelectResultElement(_healthAppointmentReasonId.ToString());



            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad()
               .ClickLocationTypesLookUpButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("Appointment_Test Automation_Location")
                 .TapSearchButton()
                 .SelectResultElement(_communityClinicLocationTypesId.ToString());

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad()
               .ClickLeadProfessionalLookUpButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery(_loginUsername)
               .TapSearchButton()
               .SelectResultElement(AutomationCDV617002User1_SystemUserId.ToString());


            caseHealthAppointmentRecordPage
             .WaitForCaseHealthAppointmentRecordPageToLoad()
             .InsertStartDate(DateTime.Now.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
             .InsertStartTime(DateTime.Now.AddHours(2).ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture))
             .InsertEndDate(DateTime.Now.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
             .InsertEndTime(DateTime.Now.AddHours(1).ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture))
             .TapSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Start Date/Time cannot be after End Date/Time.")
                .TapCloseButton();

            #endregion Step 4

        }

        [TestProperty("JiraIssueID", "CDV6-17197")]
        [Description("Login CD application with security profiles Add CW Unscheduled Health Appointments " + "Open person record in test data -> click on cases -> Open Community case -> Menu -> Related Items -> Click on Health Appointments/ Contacts & offers ->" +
            " click on + -> Select  Unscheduled Appointments in Select Health Appointment Type pop up." + "Enter all the mandatory fields" + "Enter the earlier dates and time" +
            " Click on Save" + "Validate outcome section." + "Validate Reshedule option is not visible.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void HealthAppointmentsContactOffers_UITestCases04()
        {
            foreach (var healthAppointmentCaseNoteId in dbHelper.healthAppointmentCaseNote.GetHealthAppointmentCaseNoteByPersonId(_personID))
            {
                dbHelper.healthAppointmentCaseNote.DeleteHealthAppointmentCaseNote(healthAppointmentCaseNoteId);
            }

            foreach (var healthAppointmentId in dbHelper.healthAppointment.GetHealthAppointmentByCaseID(_caseId))
            {
                foreach (var bookedslotid in dbHelper.clinicBookedSlot.GetByHealthAppointmentID(healthAppointmentId))
                {
                    dbHelper.clinicBookedSlot.DeleteClinicBookedSlot(bookedslotid);
                }

                dbHelper.healthAppointment.DeleteHealthAppointment(healthAppointmentId);
            }

            var _rttTreatmentStatus_10_Id = dbHelper.rttTreatmentStatus.GetByGovCode("10")[0];
            dbHelper.healthAppointmentOutcomeType.UpdateRTTTreatmentStatusId(_healthAppointmentOutcomeTypeId, _rttTreatmentStatus_10_Id);


            #region step 5 and 6 

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!")
              .WaitFormHomePageToLoad();

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                 .OpenPersonRecord(_personID.ToString());

            personRecordPage
                 .WaitForSystemUserPersonRecordPageToLoad()
                 .TapCasesTab();

            personCasesPage
                 .WaitForPersonCasesPageToLoad()
                 .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                 .WaitForPersonCasesRecordPageToLoad()
                 .NavigateToHealthAppointmentsPage();


            System.Threading.Thread.Sleep(3000);
            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickNewRecordButton();

            System.Threading.Thread.Sleep(3000);


            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Unscheduled Appointment")
                .TapNextButton();

            System.Threading.Thread.Sleep(3000);

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickContactTypeLookUpButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("Appointment_Test Automation")
                 .TapSearchButton()
                 .SelectResultElement(_communityClinicAppointmentContactTypesId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickhealthAppointmentReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Assessment")
                .TapSearchButton()
                .SelectResultElement(_healthAppointmentReasonId.ToString());



            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad()
               .ClickLocationTypesLookUpButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("Appointment_Test Automation_Location")
                 .TapSearchButton()
                 .SelectResultElement(_communityClinicLocationTypesId.ToString());

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad()
               .ClickLeadProfessionalLookUpButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery(_loginUsername)
               .TapSearchButton()
               .SelectResultElement(AutomationCDV617002User1_SystemUserId.ToString());


            caseHealthAppointmentRecordPage
             .WaitForCaseHealthAppointmentRecordPageToLoad()
             .InsertStartDate(DateTime.Now.AddDays(-1).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
             .InsertStartTime("09:30")
             .InsertEndDate(DateTime.Now.AddDays(-1).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
             .InsertEndTime("10:00")
             .TapSaveButton();

            caseHealthAppointmentRecordPage
             .WaitForCaseHealthAppointmentRecordPageToLoad()
             .ValidateOutcomeSection()
             .ClickAdvocateInAttendenceLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Appointment_Test Automation_AttendeeAdvocateType")
                .TapSearchButton()
                .ClickAddSelectedButton(_healthAppointmentAttendeeAdvocateTypeId.ToString());


            caseHealthAppointmentRecordPage
              .WaitForCaseHealthAppointmentRecordPageToLoad()
              .ClickOutcomeLookUpButton();

            lookupPopup
              .WaitForLookupPopupToLoad()
              .TypeSearchQuery("Appointment_Test Automation_OutcomeType")
              .TapSearchButton()
              .SelectResultElement(_healthAppointmentOutcomeTypeId.ToString());

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad()
               .ClickCareInterventionLookUpButton();


            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Appointment_Test_Automation_CareIntervention")
                .TapSearchButton()
                .SelectResultElement(_communityClinicCareInterventionId.ToString());

            caseHealthAppointmentRecordPage
              .WaitForCaseHealthAppointmentRecordPageToLoad()
              .TapSaveButton();

            System.Threading.Thread.Sleep(5000);

            var healthAppointmentRecords = dbHelper.healthAppointment.GetHealthAppointmentByCaseID(_caseId);


            var healthAppointmentRecordsFields = dbHelper.healthAppointment.GetHealthAppointmentByID(healthAppointmentRecords[0], "ownerid", "title", "healthappointmentreasonid", "caseid", "startdate",
                                                        "starttime", "enddate", "endtime", "healthappointmentoutcometypeid", "communityandclinicteamid", "healthappointmentlocationtypeid",
                                                        "healthprofessionalid", "CommunityclinicCareinterventionid");

            Assert.AreEqual(_careDirectorQA_TeamId.ToString(), healthAppointmentRecordsFields["ownerid"].ToString());
            Assert.AreEqual(_healthAppointmentReasonId.ToString(), healthAppointmentRecordsFields["healthappointmentreasonid"].ToString());
            Assert.AreEqual(_caseId.ToString(), healthAppointmentRecordsFields["caseid"].ToString());
            Assert.AreEqual(DateTime.Now.AddDays(-1).Date, healthAppointmentRecordsFields["startdate"]);
            Assert.AreEqual("09:30:00", healthAppointmentRecordsFields["starttime"].ToString());
            Assert.AreEqual(DateTime.Now.AddDays(-1).Date, healthAppointmentRecordsFields["enddate"]);
            Assert.AreEqual("10:00:00", healthAppointmentRecordsFields["endtime"].ToString());
            Assert.AreEqual(_healthAppointmentOutcomeTypeId.ToString(), healthAppointmentRecordsFields["healthappointmentoutcometypeid"].ToString());
            Assert.AreEqual(_communityAndClinicTeamId.ToString(), healthAppointmentRecordsFields["communityandclinicteamid"].ToString());
            Assert.AreEqual(_communityClinicLocationTypesId.ToString(), healthAppointmentRecordsFields["healthappointmentlocationtypeid"].ToString());
            Assert.AreEqual(AutomationCDV617002User1_SystemUserId.ToString(), healthAppointmentRecordsFields["healthprofessionalid"].ToString());




            #endregion Step 5 and 6 

            #region Step 8

            System.Threading.Thread.Sleep(5000);

            caseHealthAppointmentRecordPage
             .WaitForCaseHealthAppointmentRecordPage()
             .ValidateMenuOptionsForUnscheduledAppointments_ToolBarButton();

            #endregion Step 8



        }


        [TestProperty("JiraIssueID", "CDV6-17198")]
        [Description("Login CD application with security profiles Add CW Unscheduled Health Appointments " + "Open person record in test data -> click on cases -> Open Community case -> Menu -> Related Items -> Click on Health Appointments/ Contacts & offers ->" +
            " click on + -> Select  Appointments in Select Health Appointment Type pop up." + "Enter all the mandatory fields" +
            "Enter the Future start date and start time greater than the current time" + " Click on Save" + "Validate the Reshudule option is visible")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void HealthAppointmentsContactOffers_UITestCases05()
        {
            foreach (var healthAppointmentCaseNoteId in dbHelper.healthAppointmentCaseNote.GetHealthAppointmentCaseNoteByPersonId(_personID))
            {
                dbHelper.healthAppointmentCaseNote.DeleteHealthAppointmentCaseNote(healthAppointmentCaseNoteId);
            }

            foreach (var healthAppointmentId in dbHelper.healthAppointment.GetHealthAppointmentByCaseID(_caseId))
            {
                foreach (var bookedslotid in dbHelper.clinicBookedSlot.GetByHealthAppointmentID(healthAppointmentId))
                {
                    dbHelper.clinicBookedSlot.DeleteClinicBookedSlot(bookedslotid);
                }

                dbHelper.healthAppointment.DeleteHealthAppointment(healthAppointmentId);
            }





            #region Step 9 

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!")
              .WaitFormHomePageToLoad();


            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                 .OpenPersonRecord(_personID.ToString());

            personRecordPage
                 .WaitForSystemUserPersonRecordPageToLoad()
                 .TapCasesTab();

            personCasesPage
                 .WaitForPersonCasesPageToLoad()
                 .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                 .WaitForPersonCasesRecordPageToLoad()
                 .NavigateToHealthAppointmentsPage();


            System.Threading.Thread.Sleep(3000);
            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickNewRecordButton();

            System.Threading.Thread.Sleep(3000);


            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Appointments")
                .TapNextButton();

            System.Threading.Thread.Sleep(3000);

            caseHealthAppointmentRecordPage
              .WaitForCaseHealthAppointmentRecordPageToLoad()
              .ClickhealthAppointmentReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Assessment")
                .TapSearchButton()
                .SelectResultElement(_healthAppointmentReasonId.ToString());



            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad()
               .ClickContactTypeLookUpButton();

            lookupPopup
                  .WaitForLookupPopupToLoad()
                  .TypeSearchQuery("Appointment_Test Automation")
                  .TapSearchButton()
                  .SelectResultElement(_communityClinicAppointmentContactTypesId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .InsertStartDate(DateTime.Now.AddDays(1).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertStartTime("07:05")
                .InsertEndDate(DateTime.Now.AddDays(1).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertEndTime("07:10")
                .ClickLocationTypesLookUpButton();

            lookupPopup
                   .WaitForLookupPopupToLoad()
                   .TypeSearchQuery("Appointment_Test Automation_Location")
                   .TapSearchButton()
                   .SelectResultElement(_communityClinicLocationTypesId.ToString());

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad()
               .ClickLocationLookUpButton();

            lookupPopup
                  .WaitForLookupPopupToLoad()
                  .TypeSearchQuery("Automation_Provider_HealthAppointments")
                  .TapSearchButton()
                  .SelectResultElement(_provider_HospitalId.ToString());

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad()
               .ClickRoomLookUpButton();

            lookupPopup
              .WaitForLookupPopupToLoad()
              .TypeSearchQuery("Automation_Provider_HealthAppointments_ClinicRooms")
              .TapSearchButton()
              .SelectResultElement(_providerRoomsId.ToString());

            caseHealthAppointmentRecordPage
             .WaitForCaseHealthAppointmentRecordPageToLoad()
             .TapSaveButton();

            System.Threading.Thread.Sleep(5000);

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPage()
                .ValidateMenuOptionsForAppointments_ToolBarButton();

            #endregion Step 9 


        }


        [TestProperty("JiraIssueID", "CDV6-17199")]
        [Description("Login CD application with security profiles Add CW Unscheduled Health Appointments " + "Open person record in test data -> click on cases -> Open Community case -> Menu -> Related Items -> Click on Health Appointments/ Contacts & offers ->" +
            " click on + -> Select  Contacts and Offers in Select Health Appointment Type pop up." + "Enter all the mandatory fields" +
            "Enter the Future start date and start time greater than the current time" + " Click on Save" + "Validate the Reshudule option is visible")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void HealthAppointmentsContactOffers_UITestCases06()
        {
            foreach (var healthAppointmentCaseNoteId in dbHelper.healthAppointmentCaseNote.GetHealthAppointmentCaseNoteByPersonId(_personID))
            {
                dbHelper.healthAppointmentCaseNote.DeleteHealthAppointmentCaseNote(healthAppointmentCaseNoteId);
            }

            foreach (var healthAppointmentId in dbHelper.healthAppointment.GetHealthAppointmentByCaseID(_caseId))
            {
                foreach (var bookedslotid in dbHelper.clinicBookedSlot.GetByHealthAppointmentID(healthAppointmentId))
                {
                    dbHelper.clinicBookedSlot.DeleteClinicBookedSlot(bookedslotid);
                }

                dbHelper.healthAppointment.DeleteHealthAppointment(healthAppointmentId);
            }


            #region Step 10 

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!")
              .WaitFormHomePageToLoad();


            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                 .OpenPersonRecord(_personID.ToString());

            personRecordPage
                 .WaitForSystemUserPersonRecordPageToLoad()
                 .TapCasesTab();

            personCasesPage
                 .WaitForPersonCasesPageToLoad()
                 .OpenCaseRecord(_caseId.ToString());

            System.Threading.Thread.Sleep(3000);

            personCasesRecordPage
                 .WaitForPersonCasesRecordPageToLoad()
                 .NavigateToHealthAppointmentsPage();


            System.Threading.Thread.Sleep(3000);
            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickNewRecordButton();

            System.Threading.Thread.Sleep(3000);


            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Contacts & Offers")
                .TapNextButton();

            System.Threading.Thread.Sleep(3000);

            caseHealthAppointmentRecordPage
              .WaitForCaseHealthAppointmentRecordPageToLoad()
              .ClickhealthAppointmentReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Assessment")
                .TapSearchButton()
                .SelectResultElement(_healthAppointmentReasonId.ToString());



            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad()
               .ClickContactTypeLookUpButton();

            lookupPopup
                  .WaitForLookupPopupToLoad()
                  .TypeSearchQuery("Appointment_Test Automation")
                  .TapSearchButton()
                  .SelectResultElement(_communityClinicAppointmentContactTypesId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .InsertStartDate(DateTime.Now.AddDays(1).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertStartTime("08:00")
                .InsertEndDate(DateTime.Now.AddDays(1).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertEndTime("08:05")
                .ClickLocationTypesLookUpButton();

            lookupPopup
                   .WaitForLookupPopupToLoad()
                   .TypeSearchQuery("Appointment_Test Automation_Location")
                   .TapSearchButton()
                   .SelectResultElement(_communityClinicLocationTypesId.ToString());

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad()
               .ClickLocationLookUpButton();

            lookupPopup
                  .WaitForLookupPopupToLoad()
                  .TypeSearchQuery("Automation_Provider_HealthAppointments")
                  .TapSearchButton()
                  .SelectResultElement(_provider_HospitalId.ToString());

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad()
               .ClickRoomLookUpButton();

            lookupPopup
              .WaitForLookupPopupToLoad()
              .TypeSearchQuery("Automation_Provider_HealthAppointments_ClinicRooms")
              .TapSearchButton()
              .SelectResultElement(_providerRoomsId.ToString());

            caseHealthAppointmentRecordPage
             .WaitForCaseHealthAppointmentRecordPageToLoad()
             .TapSaveButton();

            System.Threading.Thread.Sleep(6000);

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPage()
                .ValidateMenuOptionsForAppointments_ToolBarButton();

            #endregion Step 10 
        }


        [TestProperty("JiraIssueID", "CDV6-17389")]
        [Description("Login CD application with security profiles Add CW Unscheduled Health Appointments " + "Open person record in test data -> click on cases -> Open Community case -> Menu -> Related Items -> Click on Health Appointments/ Contacts & offers ->" +
            "Create a Workflow whenever the Health appointment created with the Appointment reason as Assessment and with the system userid" +
            "Workflow should create the Health appointment case note automatically." + "Validate the same")]
        [DeploymentItem("Files\\Health_Appointment_CDV6_17002.Zip"), DeploymentItem("chromedriver.exe")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void HealthAppointmentsContactOffers_UITestCases07()
        {
            var _firstName = "Automation_Appointment_Person";
            var _lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            _personFullName = _firstName + " " + _lastName;
            _personID = dbHelper.person.CreatePersonRecord("", _firstName, "", _lastName, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
            _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            _caseId = dbHelper.Case.CreateCommunityHealthCaseRecord(_careDirectorQA_TeamId, _personID, AutomationCDV617002User1_SystemUserId, _communityAndClinicTeamId, AutomationCDV617002User1_SystemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory, _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-3).Date, DateTime.Now.AddDays(-2).Date, DateTime.Now.Date, "Unscheduled Appointments");


            #region Workflow

            _workflowId = commonMethodsDB.CreateWorkflowIfNeeded("Health_Appointment_CDV6_17002", "Health_Appointment_CDV6_17002.Zip");
            dbHelper.workflow.UpdatePublishedField(_workflowId, true);

            #endregion

            //Step 7 

            dbHelper = new DBHelper.DatabaseHelper(_loginUsername, "Passw0rd_!");

            var unscheduledAppointment = dbHelper.healthAppointment.CreateHealthAppointmentForUnscheduled(_careDirectorQA_TeamId, _personID, _dataFormId_HealthAppointmentType, _communityClinicAppointmentContactTypesId, _healthAppointmentReasonId, _caseId,
                                          DateTime.Now, new TimeSpan(0, 5, 0), DateTime.Now, new TimeSpan(1, 30, 0), AutomationCDV617002User1_SystemUserId, _communityAndClinicTeamId,
                                          _communityClinicLocationTypesId, 8, AutomationCDV617002User1_SystemUserId, "Unscheduled Appointment 1", false);


            //get all "Not Started" workflow jobs
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(_workflowId, 1).FirstOrDefault();

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the Workflow Job and wait for the Idle status
            WebAPIHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);


            loginPage
                  .GoToLoginPage()
                  .Login(_loginUsername, "Passw0rd_!")
                  .WaitFormHomePageToLoad();


            mainMenu
                   .WaitForMainMenuToLoad()
                   .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                 .OpenPersonRecord(_personID.ToString());

            personRecordPage
                 .WaitForSystemUserPersonRecordPageToLoad()
                 .TapCasesTab();

            personCasesPage
                 .WaitForPersonCasesPageToLoad()
                 .OpenCaseRecord(_caseId.ToString());

            System.Threading.Thread.Sleep(3000);

            personCasesRecordPage
                 .WaitForPersonCasesRecordPageToLoad()
                 .NavigateToHealthAppointmentsPage();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .WaitForCaseHealthAppointmentsPageToLoad()
                .SelectCaseHealthAppointmentRecord(unscheduledAppointment.ToString())
                .OpenCaseHealthAppointmentRecord(unscheduledAppointment.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad();


            var caseNote = dbHelper.healthAppointmentCaseNote.GetCaseNoteByHealthAppointmentID(unscheduledAppointment);
            Assert.AreEqual(1, caseNote.Count);


        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-17082

        [TestProperty("JiraIssueID", "CDV6-17387")]
        [Description("Login CD application with security profiles Add CW Unscheduled Health Appointments " + "Open person record in test data -> click on cases -> Open Community case -> Menu -> Related Items -> Click on Health Appointments/ Contacts & offers ->" +
         " click on + -> Select  Unscheduled Appointments in Select Health Appointment Type pop up." + "Validate the Health Appointment page is displayed." +
        "Validate the autopopulated fields" + "Validate the Home Visit and travel time section." +
        "Validated the error test by saving the record" + "Validate the Lead professional look up button values" +
        "Validate the available slots button.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void HealthAppointmentsContactOffers_UITestCases08()
        {
            foreach (var healthAppointmentCaseNoteId in dbHelper.healthAppointmentCaseNote.GetHealthAppointmentCaseNoteByPersonId(_personID))
            {
                dbHelper.healthAppointmentCaseNote.DeleteHealthAppointmentCaseNote(healthAppointmentCaseNoteId);
            }


            foreach (var healthAppointmentId in dbHelper.healthAppointment.GetHealthAppointmentByCaseID(_caseId))
            {
                foreach (var bookedslotid in dbHelper.clinicBookedSlot.GetByHealthAppointmentID(healthAppointmentId))
                {
                    dbHelper.clinicBookedSlot.DeleteClinicBookedSlot(bookedslotid);
                }

                dbHelper.healthAppointment.DeleteHealthAppointment(healthAppointmentId);
            }


            #region step 1

            loginPage
                  .GoToLoginPage()
                  .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToPeopleSection();

            peoplePage.
                  WaitForPeoplePageToLoad()
                 .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                 .OpenPersonRecord(_personID.ToString());


            personRecordPage
                    .WaitForSystemUserPersonRecordPageToLoad()
                    .TapCasesTab();

            personCasesPage
                    .WaitForPersonCasesPageToLoad()
                    .WaitForPersonCasesPageToLoad()
                    .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                  .WaitForPersonCasesRecordPageToLoad()
                  .NavigateToHealthAppointmentsPage();


            caseHealthAppointmentsPage
                 .WaitForCaseHealthAppointmentsPageToLoad()
                 .WaitForCaseHealthAppointmentsPageToLoad()
                 .ClickNewRecordButton();

            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Unscheduled Appointment")
                .TapNextButton();


            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad();

            #endregion Step 1

            #region Step 2

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad()
               .ValidateAllAutoPopulatedFields(true);

            #endregion Step 2

            #region Step 3

            caseHealthAppointmentRecordPage
             .WaitForCaseHealthAppointmentRecordPageToLoad()
             .ValidateHomeVisitSectionVisibile(false)
             .ValidateTravelSectionForHPVisibile(false);

            #endregion Step 3

            #region Step 4 

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .TapSaveButton()
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateMessageAreaTextVisibileForFields(true);

            #endregion Step 4

            #region Step 6

            caseHealthAppointmentRecordPage
               .ValidateSelectAvailableSlotButtonVisibile(false);

            #endregion Step 6

            #region Step 5 

            caseHealthAppointmentRecordPage
                .ClickLeadProfessionalLookUpButton();

            lookupPopup
              .WaitForLookupPopupToLoad()
              .TypeSearchQuery(_loginUsername)
              .TapSearchButton()
              .ValidateResultElementPresent(AutomationCDV617002User1_SystemUserId.ToString());

            #endregion Step 5


        }

        [TestProperty("JiraIssueID", "CDV6-17388")]
        [Description("Login CD application with security profiles Add CW Unscheduled Health Appointments " + "Open person record in test data -> click on cases -> Open Community case -> Menu -> Related Items -> Click on Health Appointments/ Contacts & offers ->" +
        " click on + -> Select  Appointments in Select Health Appointment Type pop up." + "Enter all the mandatory fields" +
        "Enter the start date and start time and the end date and time with the gap of more than 12 hours" + " Click on Save" + "Validate the error message displayed.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void HealthAppointmentsContactOffers_UITestCases09()
        {

            foreach (var healthAppointmentCaseNoteId in dbHelper.healthAppointmentCaseNote.GetHealthAppointmentCaseNoteByPersonId(_personID))
            {
                dbHelper.healthAppointmentCaseNote.DeleteHealthAppointmentCaseNote(healthAppointmentCaseNoteId);
            }

            foreach (var healthAppointmentId in dbHelper.healthAppointment.GetHealthAppointmentByCaseID(_caseId))
            {
                foreach (var bookedslotid in dbHelper.clinicBookedSlot.GetByHealthAppointmentID(healthAppointmentId))
                {
                    dbHelper.clinicBookedSlot.DeleteClinicBookedSlot(bookedslotid);
                }

                dbHelper.healthAppointment.DeleteHealthAppointment(healthAppointmentId);
            }

            #region Step 7



            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!")
              .WaitFormHomePageToLoad();

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                 .OpenPersonRecord(_personID.ToString());

            personRecordPage
                 .WaitForSystemUserPersonRecordPageToLoad()
                 .TapCasesTab();

            personCasesPage
                 .WaitForPersonCasesPageToLoad()
                 .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                 .WaitForPersonCasesRecordPageToLoad()
                 .NavigateToHealthAppointmentsPage();


            System.Threading.Thread.Sleep(3000);

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickNewRecordButton();

            System.Threading.Thread.Sleep(3000);


            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Unscheduled Appointment")
                .TapNextButton();

            System.Threading.Thread.Sleep(3000);

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickContactTypeLookUpButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("Appointment_Test Automation")
                 .TapSearchButton()
                 .SelectResultElement(_communityClinicAppointmentContactTypesId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickhealthAppointmentReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Assessment")
                .TapSearchButton()
                .SelectResultElement(_healthAppointmentReasonId.ToString());



            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad()
               .ClickLocationTypesLookUpButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("Appointment_Test Automation_Location")
                 .TapSearchButton()
                 .SelectResultElement(_communityClinicLocationTypesId.ToString());

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad()
               .ClickLeadProfessionalLookUpButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery(_loginUsername)
               .TapSearchButton()
               .SelectResultElement(AutomationCDV617002User1_SystemUserId.ToString());


            caseHealthAppointmentRecordPage
             .WaitForCaseHealthAppointmentRecordPageToLoad()
             .InsertStartDate(DateTime.Now.AddDays(-2).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
             .InsertStartTime("09:30")
             .InsertEndDate(DateTime.Now.AddDays(-1).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
             .InsertEndTime("10:00")
             .TapSaveButton();



            System.Threading.Thread.Sleep(5000);

            dynamicDialogPopup
              .WaitForDynamicDialogPopupToLoad()
              .ValidateMessage("The difference between Start Date/Time and End Date/Time cannot be greater than 12 hours.");



            #endregion step 7


        }


        [TestProperty("JiraIssueID", "CDV6-17390")]
        [Description("Login CD application with security profiles Add CW Unscheduled Health Appointments " + "Open person record in test data -> click on cases -> Open Community case -> Menu -> Related Items -> Click on Health Appointments/ Contacts & offers ->" +
        " click on + -> Select  Appointments in Select Health Appointment Type pop up." + "Enter all the mandatory fields" +
        "Enter the start date and start time and the end date and end time later than the start time" + " Click on Save" + "Validate the error message displayed.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void HealthAppointmentsContactOffers_UITestCases10()
        {
            foreach (var healthAppointmentCaseNoteId in dbHelper.healthAppointmentCaseNote.GetHealthAppointmentCaseNoteByPersonId(_personID))
            {
                dbHelper.healthAppointmentCaseNote.DeleteHealthAppointmentCaseNote(healthAppointmentCaseNoteId);
            }

            foreach (var healthAppointmentId in dbHelper.healthAppointment.GetHealthAppointmentByCaseID(_caseId))
            {
                foreach (var bookedslotid in dbHelper.clinicBookedSlot.GetByHealthAppointmentID(healthAppointmentId))
                {
                    dbHelper.clinicBookedSlot.DeleteClinicBookedSlot(bookedslotid);
                }

                dbHelper.healthAppointment.DeleteHealthAppointment(healthAppointmentId);
            }

            #region Step 8



            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!")
              .WaitFormHomePageToLoad();

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                 .OpenPersonRecord(_personID.ToString());

            personRecordPage
                 .WaitForSystemUserPersonRecordPageToLoad()
                 .TapCasesTab();

            personCasesPage
                 .WaitForPersonCasesPageToLoad()
                 .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                 .WaitForPersonCasesRecordPageToLoad()
                 .NavigateToHealthAppointmentsPage();


            System.Threading.Thread.Sleep(3000);
            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickNewRecordButton();

            System.Threading.Thread.Sleep(3000);


            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Unscheduled Appointment")
                .TapNextButton();

            System.Threading.Thread.Sleep(3000);

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickContactTypeLookUpButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("Appointment_Test Automation")
                 .TapSearchButton()
                 .SelectResultElement(_communityClinicAppointmentContactTypesId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickhealthAppointmentReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Assessment")
                .TapSearchButton()
                .SelectResultElement(_healthAppointmentReasonId.ToString());



            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad()
               .ClickLocationTypesLookUpButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("Appointment_Test Automation_Location")
                 .TapSearchButton()
                 .SelectResultElement(_communityClinicLocationTypesId.ToString());

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad()
               .ClickLeadProfessionalLookUpButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery(_loginUsername)
               .TapSearchButton()
               .SelectResultElement(AutomationCDV617002User1_SystemUserId.ToString());


            caseHealthAppointmentRecordPage
             .WaitForCaseHealthAppointmentRecordPageToLoad()
             .InsertStartDate(DateTime.Now.AddDays(-2).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
             .InsertStartTime("09:30")
             .InsertEndDate(DateTime.Now.AddDays(-2).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
             .InsertEndTime("08:00")
             .TapSaveButton();



            System.Threading.Thread.Sleep(5000);

            dynamicDialogPopup
              .WaitForDynamicDialogPopupToLoad()
              .ValidateMessage("Start Date/Time cannot be after End Date/Time.");



            #endregion step 8


        }

        [TestProperty("JiraIssueID", "CDV6-17391")]
        [Description("Login CD application with security profiles Add CW Unscheduled Health Appointments " + "Open person record in test data -> click on cases -> Open Community case -> Menu -> Related Items -> Click on Health Appointments/ Contacts & offers ->" +
        " click on + -> Select  Appointments in Select Health Appointment Type pop up." + "Enter all the mandatory fields" +
        "Enter the start date as early as the end date and start time and enter the end date and time" + " Click on Save" + "Validate the error message displayed.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void HealthAppointmentsContactOffers_UITestCases11()
        {
            foreach (var healthAppointmentCaseNoteId in dbHelper.healthAppointmentCaseNote.GetHealthAppointmentCaseNoteByPersonId(_personID))
            {
                dbHelper.healthAppointmentCaseNote.DeleteHealthAppointmentCaseNote(healthAppointmentCaseNoteId);
            }

            foreach (var healthAppointmentId in dbHelper.healthAppointment.GetHealthAppointmentByCaseID(_caseId))
            {
                foreach (var bookedslotid in dbHelper.clinicBookedSlot.GetByHealthAppointmentID(healthAppointmentId))
                {
                    dbHelper.clinicBookedSlot.DeleteClinicBookedSlot(bookedslotid);
                }

                dbHelper.healthAppointment.DeleteHealthAppointment(healthAppointmentId);
            }

            #region Step 9



            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                 .OpenPersonRecord(_personID.ToString());

            personRecordPage
                 .WaitForSystemUserPersonRecordPageToLoad()
                 .TapCasesTab();

            personCasesPage
                 .WaitForPersonCasesPageToLoad()
                 .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                 .WaitForPersonCasesRecordPageToLoad()
                 .NavigateToHealthAppointmentsPage();


            System.Threading.Thread.Sleep(3000);
            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickNewRecordButton();

            System.Threading.Thread.Sleep(3000);


            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Unscheduled Appointment")
                .TapNextButton();

            System.Threading.Thread.Sleep(3000);

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickContactTypeLookUpButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("Appointment_Test Automation")
                 .TapSearchButton()
                 .SelectResultElement(_communityClinicAppointmentContactTypesId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickhealthAppointmentReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Assessment")
                .TapSearchButton()
                .SelectResultElement(_healthAppointmentReasonId.ToString());



            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad()
               .ClickLocationTypesLookUpButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("Appointment_Test Automation_Location")
                 .TapSearchButton()
                 .SelectResultElement(_communityClinicLocationTypesId.ToString());

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad()
               .ClickLeadProfessionalLookUpButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery("Automation CDV6-17002 Test User 1")
               .TapSearchButton()
               .SelectResultElement(AutomationCDV617002User1_SystemUserId.ToString());


            caseHealthAppointmentRecordPage
             .WaitForCaseHealthAppointmentRecordPageToLoad()
             .InsertStartDate(DateTime.Now.AddDays(-2).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
             .InsertStartTime("09:30")
             .InsertEndDate(DateTime.Now.AddDays(-5).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
             .InsertEndTime("08:00")
             .TapSaveButton();



            System.Threading.Thread.Sleep(5000);

            dynamicDialogPopup
              .WaitForDynamicDialogPopupToLoad()
              .ValidateMessage("Start Date/Time cannot be after End Date/Time.");



            #endregion step 9


        }

        [TestProperty("JiraIssueID", "CDV6-17392")]
        [Description("Login CD application with security profiles Add CW Unscheduled Health Appointments " + "Open person record in test data -> click on cases -> Open Community case -> Menu -> Related Items -> Click on Health Appointments/ Contacts & offers ->" +
        " click on + -> Select  Appointments in Select Health Appointment Type pop up." + "Enter all the mandatory fields" +
        "Enter the future start date  & time and enter the end date and time in future" + " Click on Save" + "Validate the error message displayed.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void HealthAppointmentsContactOffers_UITestCases12()
        {
            foreach (var healthAppointmentCaseNoteId in dbHelper.healthAppointmentCaseNote.GetHealthAppointmentCaseNoteByPersonId(_personID))
            {
                dbHelper.healthAppointmentCaseNote.DeleteHealthAppointmentCaseNote(healthAppointmentCaseNoteId);
            }

            foreach (var healthAppointmentId in dbHelper.healthAppointment.GetHealthAppointmentByCaseID(_caseId))
            {
                foreach (var bookedslotid in dbHelper.clinicBookedSlot.GetByHealthAppointmentID(healthAppointmentId))
                {
                    dbHelper.clinicBookedSlot.DeleteClinicBookedSlot(bookedslotid);
                }

                dbHelper.healthAppointment.DeleteHealthAppointment(healthAppointmentId);
            }

            #region Step 10



            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!")
              .WaitFormHomePageToLoad();

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                 .OpenPersonRecord(_personID.ToString());

            personRecordPage
                 .WaitForSystemUserPersonRecordPageToLoad()
                 .TapCasesTab();

            personCasesPage
                 .WaitForPersonCasesPageToLoad()
                 .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                 .WaitForPersonCasesRecordPageToLoad()
                 .NavigateToHealthAppointmentsPage();


            System.Threading.Thread.Sleep(3000);
            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickNewRecordButton();

            System.Threading.Thread.Sleep(3000);


            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Unscheduled Appointment")
                .TapNextButton();

            System.Threading.Thread.Sleep(3000);

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickContactTypeLookUpButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("Appointment_Test Automation")
                 .TapSearchButton()
                 .SelectResultElement(_communityClinicAppointmentContactTypesId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickhealthAppointmentReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Assessment")
                .TapSearchButton()
                .SelectResultElement(_healthAppointmentReasonId.ToString());



            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad()
               .ClickLocationTypesLookUpButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("Appointment_Test Automation_Location")
                 .TapSearchButton()
                 .SelectResultElement(_communityClinicLocationTypesId.ToString());

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad()
               .ClickLeadProfessionalLookUpButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery(_loginUsername)
               .TapSearchButton()
               .SelectResultElement(AutomationCDV617002User1_SystemUserId.ToString());


            caseHealthAppointmentRecordPage
             .WaitForCaseHealthAppointmentRecordPageToLoad()
             .InsertStartDate(DateTime.Now.AddDays(2).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
             .InsertStartTime("09:30")
             .InsertEndDate(DateTime.Now.AddDays(2).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
             .InsertEndTime("10:00")
             .TapSaveButton();



            System.Threading.Thread.Sleep(5000);

            dynamicDialogPopup
              .WaitForDynamicDialogPopupToLoad()
              .ValidateMessage("Start Date/Time cannot be in the future.");



            #endregion step 10


        }


        [TestProperty("JiraIssueID", "CDV6-17393")]
        [Description("Login CD application with security profiles Add CW Unscheduled Health Appointments " + "Open person record in test data -> click on cases -> Open Community case -> Menu -> Related Items -> Click on Health Appointments/ Contacts & offers ->" +
        " click on + -> Select  Appointments in Select Health Appointment Type pop up." + "Enter all the mandatory fields" +
        "Enter the past start date  & time and enter the end date and time in the past" + " Click on Save" + "Validate the saved record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void HealthAppointmentsContactOffers_UITestCases13()
        {
            foreach (var healthAppointmentCaseNoteId in dbHelper.healthAppointmentCaseNote.GetHealthAppointmentCaseNoteByPersonId(_personID))
            {
                dbHelper.healthAppointmentCaseNote.DeleteHealthAppointmentCaseNote(healthAppointmentCaseNoteId);
            }


            foreach (var healthAppointmentId in dbHelper.healthAppointment.GetHealthAppointmentByCaseID(_caseId))
            {
                foreach (var bookedslotid in dbHelper.clinicBookedSlot.GetByHealthAppointmentID(healthAppointmentId))
                {
                    dbHelper.clinicBookedSlot.DeleteClinicBookedSlot(bookedslotid);
                }

                dbHelper.healthAppointment.DeleteHealthAppointment(healthAppointmentId);
            }

            #region Step 11



            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!")
              .WaitFormHomePageToLoad();

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                 .OpenPersonRecord(_personID.ToString());

            personRecordPage
                 .WaitForSystemUserPersonRecordPageToLoad()
                 .TapCasesTab();

            personCasesPage
                 .WaitForPersonCasesPageToLoad()
                 .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                 .WaitForPersonCasesRecordPageToLoad()
                 .NavigateToHealthAppointmentsPage();


            System.Threading.Thread.Sleep(3000);
            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickNewRecordButton();

            System.Threading.Thread.Sleep(3000);


            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Unscheduled Appointment")
                .TapNextButton();

            System.Threading.Thread.Sleep(3000);

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickContactTypeLookUpButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("Appointment_Test Automation")
                 .TapSearchButton()
                 .SelectResultElement(_communityClinicAppointmentContactTypesId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickhealthAppointmentReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Assessment")
                .TapSearchButton()
                .SelectResultElement(_healthAppointmentReasonId.ToString());



            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad()
               .ClickLocationTypesLookUpButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("Appointment_Test Automation_Location")
                 .TapSearchButton()
                 .SelectResultElement(_communityClinicLocationTypesId.ToString());

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad()
               .ClickLeadProfessionalLookUpButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery(_loginUsername)
               .TapSearchButton()
               .SelectResultElement(AutomationCDV617002User1_SystemUserId.ToString());


            caseHealthAppointmentRecordPage
             .WaitForCaseHealthAppointmentRecordPageToLoad()
             .InsertStartDate(DateTime.Now.AddDays(-2).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
             .InsertStartTime("09:30")
             .InsertEndDate(DateTime.Now.AddDays(-2).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
             .InsertEndTime("10:00")
             .TapSaveButton();



            System.Threading.Thread.Sleep(5000);

            var healthAppointmentRecords = dbHelper.healthAppointment.GetHealthAppointmentByCaseID(_caseId);
            Assert.AreEqual(1, healthAppointmentRecords.Count());



            #endregion step 11


        }

        [TestProperty("JiraIssueID", "CDV6-17394")]
        [Description("Login CD application with security profiles Add CW Unscheduled Health Appointments " + "Open person record in test data -> click on cases -> Open Community case -> Menu -> Related Items -> Click on Health Appointments/ Contacts & offers ->" +
        " click on + -> Select  Appointments in Select Health Appointment Type pop up." + "Enter all the mandatory fields" +
        "Enter the past start date  & time and enter the end date and time in the past" + " Click on Save" + "Click on Additional professional requires as yes option" +
            "Validate the Additional section for Additional professional required is displayed")]

        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void HealthAppointmentsContactOffers_UITestCases14()
        {
            foreach (var healthAppointmentCaseNoteId in dbHelper.healthAppointmentCaseNote.GetHealthAppointmentCaseNoteByPersonId(_personID))
            {
                dbHelper.healthAppointmentCaseNote.DeleteHealthAppointmentCaseNote(healthAppointmentCaseNoteId);
            }

            foreach (var healthAppointmentId in dbHelper.healthAppointment.GetHealthAppointmentByCaseID(_caseId))
            {
                foreach (var bookedslotid in dbHelper.clinicBookedSlot.GetByHealthAppointmentID(healthAppointmentId))
                {
                    dbHelper.clinicBookedSlot.DeleteClinicBookedSlot(bookedslotid);
                }

                dbHelper.healthAppointment.DeleteHealthAppointment(healthAppointmentId);
            }

            #region Step 12



            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!")
              .WaitFormHomePageToLoad();

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                 .OpenPersonRecord(_personID.ToString());

            personRecordPage
                 .WaitForSystemUserPersonRecordPageToLoad()
                 .TapCasesTab();

            personCasesPage
                 .WaitForPersonCasesPageToLoad()
                 .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                 .WaitForPersonCasesRecordPageToLoad()
                 .NavigateToHealthAppointmentsPage();


            System.Threading.Thread.Sleep(3000);
            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickNewRecordButton();

            System.Threading.Thread.Sleep(3000);


            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Unscheduled Appointment")
                .TapNextButton();

            System.Threading.Thread.Sleep(3000);

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickContactTypeLookUpButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("Appointment_Test Automation")
                 .TapSearchButton()
                 .SelectResultElement(_communityClinicAppointmentContactTypesId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickhealthAppointmentReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Assessment")
                .TapSearchButton()
                .SelectResultElement(_healthAppointmentReasonId.ToString());



            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad()
               .ClickLocationTypesLookUpButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("Appointment_Test Automation_Location")
                 .TapSearchButton()
                 .SelectResultElement(_communityClinicLocationTypesId.ToString());

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad()
               .ClickLeadProfessionalLookUpButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery(_loginUsername)
               .TapSearchButton()
               .SelectResultElement(AutomationCDV617002User1_SystemUserId.ToString());


            caseHealthAppointmentRecordPage
             .WaitForCaseHealthAppointmentRecordPageToLoad()
             .InsertStartDate(DateTime.Now.AddDays(-2).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
             .InsertStartTime("09:30")
             .InsertEndDate(DateTime.Now.AddDays(-2).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
             .InsertEndTime("10:00")
             .ClickAdditionalProfessionalRequired_RadioButton()
             .TapSaveButton();



            System.Threading.Thread.Sleep(5000);

            var healthAppointmentRecords = dbHelper.healthAppointment.GetHealthAppointmentByCaseID(_caseId);
            Assert.AreEqual(1, healthAppointmentRecords.Count());

            caseHealthAppointmentRecordPage
              .WaitForCaseHealthAppointmentRecordPage()
              .WaitForCaseHealthAppointmentRecordPageWithAdditionalProfessionalRequiredSection();



            #endregion step 12


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




using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Phoenix.UITests.Cases
{
    [TestClass]
    public class Case_UnscheduledHealthAppointmentsBatch02_UITestCases : FunctionalTest
    {

        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid AutomationCDV617082User1_SystemUserId;
        private Guid AutomationCDV617082User2_SystemUserId;
        private Guid AutomationCDV617082User3_SystemUserId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _ethnicityId;
        private Guid _contactReasonId;
        private Guid _contactSourceId;
        private Guid _personID;
        private int _personNumber;
        private string _personFullName;
        private Guid _dataFormId;//Unscheduled Appointments        
        private Guid _provider_HospitalId;
        private Guid _caseStatusID;
        private Guid _communityAndClinicTeamId;
        private Guid _communityClinicDiaryViewSetupId;
        private Guid _providerId_Carer;
        private Guid _caseId;
        private Guid _contactAdministrativeCategory;
        private Guid _caseServiceTypeRequestedid;
        private Guid _dataFormId_CommunityHealthCase;
        private Guid _communityClinicAppointmentContactTypesId;
        private Guid _communityClinicLocationTypesId;
        private Guid _providerRoomsId;
        private Guid _recurrencePatternId;
        private Guid _providerClinicRoom;
        private Guid _healthAppointmentReasonId;
        private string _healthappointmentReason;
        private string linkedProfessional_title = "Automation CDV6-17082 Test User 1";
        private string linkedProfessional2_title = "Automation CDV6-17082 Test User 2";
        private string linkedProfessional3_title = "Automation CDV6-17082 Test User 3";
        private Guid _linkedProfessional;
        private Guid _linkedProfessional2;
        private Guid _linkedProfessional3;
        private string _CommunityclinicTeams_Title = "CareDirector QA Community And Clinic Team Health Appointment Team 17802";
        private Guid _communityClinicCareInterventionId;
        private Guid _workflowId;
        private Guid _HealthAppointmentBusinessObjectId;
        private string _loginUsername;
        private string _systemUsername2;
        private string _systemUsername3;

        [TestInitialize()]
        public void TestInitializationMethod()
        {

            try
            {
                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").First();


                #endregion

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

                var automationCDV617082TestUSer1Exists = dbHelper.systemUser.GetSystemUserByUserName("Automation_CDV6-17082_Test_User_1").Any();
                if (!automationCDV617082TestUSer1Exists)
                {
                    AutomationCDV617082User1_SystemUserId = dbHelper.systemUser.CreateSystemUser("Automation_CDV6-17082_Test_User_1", "Automation", " CDV6-17082 Test User 1", "Automation CDV6-17082 Test User 1", "Passw0rd_!", "Automation_CDV6-17082_Test_User_1@somemail.com", "Automation_CDV6-17082_Test_User_1@secureemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, DateTime.Now.Date);

                    var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();
                    var systemUserUnscheduledAppointmentsProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Unscheduled Health Appointments").First();

                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(AutomationCDV617082User1_SystemUserId, systemAdministratorSecurityProfileId);
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(AutomationCDV617082User1_SystemUserId, systemUserSecureFieldsSecurityProfileId);
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(AutomationCDV617082User1_SystemUserId, systemUserUnscheduledAppointmentsProfileId);
                }
                if (AutomationCDV617082User1_SystemUserId == Guid.Empty)
                {
                    AutomationCDV617082User1_SystemUserId = dbHelper.systemUser.GetSystemUserByUserName("Automation_CDV6-17082_Test_User_1").FirstOrDefault();
                }

                dbHelper.systemUser.UpdateLastPasswordChangedDate(AutomationCDV617082User1_SystemUserId, DateTime.Now);
                _loginUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(AutomationCDV617082User1_SystemUserId, "username")["username"];

                #endregion System User 1

                #region System User 2

                var automationCDV617082TestUser2Exists = dbHelper.systemUser.GetSystemUserByUserName("Automation_CDV6-17082_Test_User_2").Any();
                if (!automationCDV617082TestUser2Exists)
                {
                    AutomationCDV617082User2_SystemUserId = dbHelper.systemUser.CreateSystemUser("Automation_CDV6-17082_Test_User_2", "Automation", " CDV6-17082 Test User 2", "Automation CDV6-17082 Test User 2", "Passw0rd_!", "Automation_CDV6-17082_Test_User_2@somemail.com", "Automation_CDV6-17082_Test_User_2@secureemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, DateTime.Now.Date);

                    var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();
                    var systemUserUnscheduledAppointmentsProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Unscheduled Health Appointments").First();

                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(AutomationCDV617082User2_SystemUserId, systemAdministratorSecurityProfileId);
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(AutomationCDV617082User2_SystemUserId, systemUserSecureFieldsSecurityProfileId);
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(AutomationCDV617082User2_SystemUserId, systemUserUnscheduledAppointmentsProfileId);
                }
                if (AutomationCDV617082User2_SystemUserId == Guid.Empty)
                {
                    AutomationCDV617082User2_SystemUserId = dbHelper.systemUser.GetSystemUserByUserName("Automation_CDV6-17082_Test_User_2").FirstOrDefault();
                }
                dbHelper.systemUser.UpdateLastPasswordChangedDate(AutomationCDV617082User2_SystemUserId, DateTime.Now);
                _systemUsername2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(AutomationCDV617082User2_SystemUserId, "username")["username"];

                #endregion System User 2

                #region System User 3

                var automationCDV617082TestUser3Exists = dbHelper.systemUser.GetSystemUserByUserName("Automation_CDV6-17082_Test_User_3").Any();
                if (!automationCDV617082TestUser3Exists)
                {
                    AutomationCDV617082User3_SystemUserId = dbHelper.systemUser.CreateSystemUser("Automation_CDV6-17082_Test_User_3", "Automation", " CDV6-17082 Test User 3", "Automation CDV6-17082 Test User 3", "Passw0rd_!", "Automation_CDV6-17082_Test_User_3@somemail.com", "Automation_CDV6-17082_Test_User_3@secureemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, DateTime.Now.Date);

                    var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();
                    var systemUserUnscheduledAppointmentsProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Unscheduled Health Appointments").First();

                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(AutomationCDV617082User3_SystemUserId, systemAdministratorSecurityProfileId);
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(AutomationCDV617082User3_SystemUserId, systemUserSecureFieldsSecurityProfileId);
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(AutomationCDV617082User3_SystemUserId, systemUserUnscheduledAppointmentsProfileId);
                }
                if (AutomationCDV617082User3_SystemUserId == Guid.Empty)
                {
                    AutomationCDV617082User3_SystemUserId = dbHelper.systemUser.GetSystemUserByUserName("Automation_CDV6-17082_Test_User_3").FirstOrDefault();
                }
                dbHelper.systemUser.UpdateLastPasswordChangedDate(AutomationCDV617082User3_SystemUserId, DateTime.Now);
                _systemUsername3 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(AutomationCDV617082User3_SystemUserId, "username")["username"];

                #endregion System User 3

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

                var providerHospitalExists = dbHelper.provider.GetProviderByName("Automation_Provider_HealthAppointments_17802").Any();
                if (!providerHospitalExists)
                    dbHelper.provider.CreateProvider("Automation_Provider_HealthAppointments_17802", _careDirectorQA_TeamId);
                _provider_HospitalId = dbHelper.provider.GetProviderByName("Automation_Provider_HealthAppointments_17802")[0];

                #endregion Provider_Hospital

                #region Providers Room

                var providerRoomsExists = dbHelper.providerRoom.GetProviderRoomByName("Automation_Provider_HealthAppointments_ClinicRooms_17802").Any();
                if (!providerRoomsExists)
                    dbHelper.providerRoom.CreateProviderRoom("Automation_Provider_HealthAppointments_ClinicRooms_17802", _careDirectorQA_TeamId, _provider_HospitalId);
                _providerRoomsId = dbHelper.providerRoom.GetProviderRoomByName("Automation_Provider_HealthAppointments_ClinicRooms_17802")[0];

                #endregion

                #region Provider (Carer)
                var carerProviderExists = dbHelper.provider.GetProviderByName("CareDirector QA Provider Health Appointment 17802").Any();
                if (!carerProviderExists)
                {
                    _providerId_Carer = dbHelper.provider.CreateProvider("CareDirector QA Provider Health Appointment 17802", _careDirectorQA_TeamId, 7);

                }
                if (_providerId_Carer == Guid.Empty)
                {
                    _providerId_Carer = dbHelper.provider.GetProviderByName("CareDirector QA Provider Health Appointment 17802").FirstOrDefault();
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

                var healthAppointmentReasonExists = dbHelper.healthAppointmentReason.GetByName("Assessment_17082").Any();

                if (!healthAppointmentReasonExists)
                    _healthAppointmentReasonId = dbHelper.healthAppointmentReason.CreateHealthAppointmentReason(_careDirectorQA_TeamId, "Assessment_17082", new DateTime(2020, 1, 1), "1", null);

                if (_healthAppointmentReasonId == Guid.Empty)
                    _healthAppointmentReasonId = dbHelper.healthAppointmentReason.GetByName("Assessment_17082").FirstOrDefault();

                _healthappointmentReason = (string)dbHelper.healthAppointmentReason.GetByID(_healthAppointmentReasonId, "name")["name"];


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

                #region Data Form

                _dataFormId_CommunityHealthCase = dbHelper.dataForm.GetByName("CommunityHealthCase").FirstOrDefault();

                _dataFormId = dbHelper.dataForm.GetByName("Unscheduled Appointment").FirstOrDefault();

                #endregion

                #region Case Status
                _caseStatusID = dbHelper.caseStatus.GetByName("Allocate To Team").FirstOrDefault();

                #endregion

                #region Community/Clinic Appointment Contact Types

                var communityClinicAppointmentContactTypesExists = dbHelper.healthAppointmentContactType.GetByName("Appointment_Test Automation").Any();
                if (!communityClinicAppointmentContactTypesExists)
                    dbHelper.healthAppointmentContactType.CreateHealthAppointmentContactType(_careDirectorQA_TeamId, "Appointment_Test Automation", new DateTime(2020, 1, 1), "3");
                _communityClinicAppointmentContactTypesId = dbHelper.healthAppointmentContactType.GetByName("Appointment_Test Automation")[0];

                #endregion Community/Clinic Appointment Contact Types

                #region Community/Clinic Appointment Location Types

                var communityClinicAppointmentLocationTypesExists = dbHelper.healthAppointmentLocationType.GetByName("Appointment_Test Automation_Location_17802").Any();
                if (!communityClinicAppointmentLocationTypesExists)
                    dbHelper.healthAppointmentLocationType.CreateHealthAppointmentLocationType(_careDirectorQA_TeamId, "Appointment_Test Automation_Location_17802", new DateTime(2020, 1, 1));
                _communityClinicLocationTypesId = dbHelper.healthAppointmentLocationType.GetByName("Appointment_Test Automation_Location_17802")[0];

                #endregion Community/Clinic Appointment Location Types

                #region Community Clinic CareIntervention

                var communityClinicCareIntervention = dbHelper.communityClinicCareIntervention.GetByName("Appointment_Test_Automation_CareIntervention_17802").Any();
                if (!communityClinicCareIntervention)
                    dbHelper.communityClinicCareIntervention.CreateCommunityClinicCareIntervention(_careDirectorQA_TeamId, "Appointment_Test_Automation_CareIntervention_17802", new DateTime(2020, 1, 1));
                _communityClinicCareInterventionId = dbHelper.communityClinicCareIntervention.GetByName("Appointment_Test_Automation_CareIntervention_17802")[0];

                #endregion Health Appointment Outcome Type

                #region Person 1

                var personRecordExists = dbHelper.person.GetByFirstName("Automation_Appointment_Person_17802").Any();
                if (!personRecordExists)
                {
                    _personID = dbHelper.person.CreatePersonRecord("", "Automation_Appointment_Person_17802", "", "AutomationAppointmentLastName", "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
                    _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
                }
                if (_personID == Guid.Empty)
                {
                    _personID = dbHelper.person.GetByFirstName("Automation_Appointment_Person_17802").FirstOrDefault();
                    _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
                }
                _personFullName = "Automation_Appointment_Person_17802 AutomationAppointmentLastName";

                #endregion Person 1

                #region Community Case record 1

                var caseRecordsExist = dbHelper.Case.GetCasesByPersonID(_personID).Any();
                if (!caseRecordsExist)
                {

                    _caseId = dbHelper.Case.CreateCommunityHealthCaseRecord(_careDirectorQA_TeamId, _personID, AutomationCDV617082User1_SystemUserId, _communityAndClinicTeamId, AutomationCDV617082User1_SystemUserId, _caseStatusID, _contactReasonId, _contactAdministrativeCategory,
                                    _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-3).Date, DateTime.Now.AddDays(-2).Date, DateTime.Now.Date, "Unscheduled Appointments");


                }
                if (_caseId == Guid.Empty)
                {
                    _caseId = dbHelper.Case.GetCasesByPersonID(_personID).FirstOrDefault();

                }


                #endregion Community Case record 1

                #region Clinic Room

                foreach (var healthAppointmentId in dbHelper.healthAppointment.GetHealthAppointmentByCaseID(_caseId))
                {
                    foreach (var bookedslotid in dbHelper.clinicBookedSlot.GetByHealthAppointmentID(healthAppointmentId))
                    {
                        dbHelper.clinicBookedSlot.DeleteClinicBookedSlot(bookedslotid);
                    }

                }

                var clinicRoomExists = dbHelper.clinicRoom.GetClinicRoomByTitle("Automation_Provider_HealthAppointments_ClinicRooms_17802").Any();
                if (!clinicRoomExists)
                    dbHelper.clinicRoom.CreateClinicRoom(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _communityClinicDiaryViewSetupId, _providerRoomsId, _recurrencePatternId,
                                                DateTime.Now.AddMonths(-1).Date, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0));
                _providerClinicRoom = dbHelper.clinicRoom.GetClinicRoomByTitle("Automation_Provider_HealthAppointments_ClinicRooms_17802").FirstOrDefault();

                if (_providerClinicRoom == Guid.Empty)
                {
                    _providerClinicRoom = dbHelper.clinicRoom.GetClinicRoomByTitle("Automation_Provider_HealthAppointments_ClinicRooms_17802").FirstOrDefault();

                }

                #endregion  Clinic Room

                #region Community Clinic Linked Professional

                var IDs = dbHelper.communityClinicLinkedProfessional.GetLinkedProfessionalByID(_communityClinicDiaryViewSetupId);
                foreach (var ID in IDs)
                {
                    dbHelper.communityClinicLinkedProfessional.DeleteCommunityClinicLinkedProfessional(ID);
                }


                var linkedProfessionalsExists = dbHelper.communityClinicLinkedProfessional.GetByTitle(linkedProfessional_title).Any();
                if (!linkedProfessionalsExists)
                    dbHelper.communityClinicLinkedProfessional.CreateCommunityClinicLinkedProfessional(_careDirectorQA_TeamId, _communityClinicDiaryViewSetupId, AutomationCDV617082User1_SystemUserId, DateTime.Now.AddDays(-20).Date, new TimeSpan(1, 0, 0),
                                                                new TimeSpan(23, 0, 0), _recurrencePatternId, linkedProfessional_title);

                _linkedProfessional = dbHelper.communityClinicLinkedProfessional.GetByTitle(linkedProfessional_title).FirstOrDefault();


                #endregion Community Clinic Linked Professional

                #region Community Clinic Linked Professional 2
                var linkedProfessional2Exists = dbHelper.communityClinicLinkedProfessional.GetByTitle(linkedProfessional2_title).Any();
                if (!linkedProfessional2Exists)
                    dbHelper.communityClinicLinkedProfessional.CreateCommunityClinicLinkedProfessional(_careDirectorQA_TeamId, _communityClinicDiaryViewSetupId, AutomationCDV617082User2_SystemUserId, DateTime.Now.AddDays(-20).Date, new TimeSpan(1, 0, 0),
                                                                new TimeSpan(23, 0, 0), _recurrencePatternId, linkedProfessional2_title);

                _linkedProfessional2 = dbHelper.communityClinicLinkedProfessional.GetByTitle(linkedProfessional2_title).FirstOrDefault();
                #endregion Community Clinic Linked Professional 2

                #region Community Clinic Linked Professional 3

                var linkedProfessional3Exists = dbHelper.communityClinicLinkedProfessional.GetByTitle(linkedProfessional3_title).Any();
                if (!linkedProfessional3Exists)
                    dbHelper.communityClinicLinkedProfessional.CreateCommunityClinicLinkedProfessional(_careDirectorQA_TeamId, _communityClinicDiaryViewSetupId, AutomationCDV617082User3_SystemUserId, DateTime.Now.AddDays(-20).Date, new TimeSpan(1, 0, 0),
                                                                new TimeSpan(23, 0, 0), _recurrencePatternId, linkedProfessional3_title);

                _linkedProfessional = dbHelper.communityClinicLinkedProfessional.GetByTitle(linkedProfessional3_title).FirstOrDefault();


                #endregion Community Clinic Linked Professional 3

                #region Health Appointment

                _HealthAppointmentBusinessObjectId = dbHelper.businessObject.GetBusinessObjectByName("HealthAppointment").FirstOrDefault();

                #endregion

                #region Cleanup Health Appoinments

                var casenoteIDs = dbHelper.healthAppointmentCaseNote.GetHealthAppointmentCaseNoteByPersonId(_personID);

                foreach (var caseNoteId in casenoteIDs)
                {
                    dbHelper.healthAppointmentCaseNote.DeleteHealthAppointmentCaseNote(caseNoteId);
                }

                var healthAppointments = dbHelper.healthAppointment.GetHealthAppointmentByCaseID(_caseId);

                foreach (var healthAppointment in healthAppointments)
                {
                    var healthProfessionals = dbHelper.healthAppointmentAdditionalProfessional.GetHealthAppointmentAdditionalProfessionalByHealthAppointmentID(healthAppointment);

                    foreach (var healthProfessional in healthProfessionals)
                        dbHelper.healthAppointmentAdditionalProfessional.DeleteHealthAppointmentAdditionalProfessional(healthProfessional);

                }

                foreach (var healthAppointment in healthAppointments)
                {
                    dbHelper.healthAppointment.DeleteHealthAppointment(healthAppointment);
                }

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

        #region https://advancedcsg.atlassian.net/browse/CDV6-17082

        [TestProperty("JiraIssueID", "CDV6-17496")]
        [Description("Login CD application with security profiles Add CW Unscheduled Health Appointments " + "Open person record in test data -> click on cases -> Open Community case -> Menu -> Related Items -> Click on Health Appointments/ Contacts & offers ->" +
            " click on + -> Select Unscheduled Appointments in Select Health Appointment Type pop up." + "Enter all the mandatory fields" +
            " Click on Save" + "update the changes in unscheduled appointment to trigger the workflow " + "Verify the case note record is generated against the unscheduled appointment record upon successful run of workflow.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void UnscheduledHealthAppointmentsBatch02_UITestCases01()
        {

            #region Step 25
            string _workflowXml = "<WorkflowRule RunOrdered=\"false\"><WorkflowStep Id=\"1\" Name=\"New Step\"><ActionsOrConditions><WorkflowActionOrCondition Id=\"014b2eb6-0d63-47c2-b04a-46a5bfd3e2f5\" ProcessOrder=\"0\"><GroupCondition Id=\"b312bff2-edf8-4bab-b680-c6c6efe428de\" Name=\"Appointment Reason &lt;i&gt;Equals&lt;/i&gt; [Assessment_17082] &lt;strong&gt;And&lt;/strong&gt; Modified By &lt;i&gt;Equals&lt;/i&gt; [Automation_CDV6-17082_Test_User_1]\" ConditionGroupByType=\"And\"><Conditions><WorkflowCondition Id=\"05fc923b-13dd-4cc7-bc99-b34d6f9cbd1e\" ConditionOperatorType=\"1\" RelationshipParental=\"false\" LeftElement=\"healthappointmentreasonid\" " +
                "RightValue=\"" + _healthAppointmentReasonId.ToString() + "\" RightLabel=\"Assessment_17082\" TableName=\"healthappointmentreason\"/><WorkflowCondition Id =\"e29ac15a-6ed2-42df-a5fd-c07632cd9f9d\" ConditionOperatorType=\"1\" RelationshipParental=\"false\" LeftElement=\"modifiedby\" RightValue=\"" + AutomationCDV617082User1_SystemUserId.ToString() + "\" RightLabel=\"Automation_CDV6-17082_Test_User_1\" TableName=\"systemuser\"/></Conditions><ThenActions><WorkflowAction Id=\"2f989201-f6a0-4581-98d0-cfbf7026aec2\" Name=\"Create Record: 'Health Appointment Case Note'\" CustomTitle=\"false\" ActionType=\"1\" BusinessObject=\"c8c4894c-f0e8-e811-80dc-0050560502cc\"><WorkflowActionField TargetElement=\"subject\" Operator=\"1\"><WorkflowActionValue ValueElement=\"startdate\" BusinessObject=\"554696c5-d8a4-e611-80d3-0050560502cc\"/></WorkflowActionField><WorkflowActionField TargetElement=\"healthappointmentid\" Operator=\"1\"><WorkflowActionValue ValueElement=\"healthappointmentid\" BusinessObject=\"554696c5-d8a4-e611-80d3-0050560502cc\"/></WorkflowActionField><WorkflowActionField TargetElement=\"casenotedate\" Operator=\"1\"><WorkflowActionValue ValueCustom=\"2022-04-18 00:00\"/></WorkflowActionField><WorkflowActionField TargetElement=\"statusid\" Operator=\"1\"><WorkflowActionValue DisplayName=\"Open\" ValueCustom=\"1\"/></WorkflowActionField><WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\"><WorkflowActionValue ValueCustom=\"false\"/></WorkflowActionField><WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\"><WorkflowActionValue ValueElement=\"ownerid\" BusinessObject=\"554696c5-d8a4-e611-80d3-0050560502cc\"/></WorkflowActionField><WorkflowActionField TargetElement=\"communitycliniccareinterventionid\" Operator=\"1\"><WorkflowActionValue ValueElement=\"communitycliniccareinterventionid\" BusinessObject=\"554696c5-d8a4-e611-80d3-0050560502cc\"/></WorkflowActionField><WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\"><WorkflowActionValue ValueCustom=\"false\"/></WorkflowActionField><WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\"><WorkflowActionValue ValueCustom=\"false\"/></WorkflowActionField></WorkflowAction></ThenActions></GroupCondition></WorkflowActionOrCondition></ActionsOrConditions></WorkflowStep></WorkflowRule>";


            #region Workflow

            var workflowExists = dbHelper.workflow.GetWorkflowByName("Health_Appointment_CDV6_17082").Any();
            if (!workflowExists)
            {
                _workflowId = dbHelper.workflow.CreateASyncWorkflowRecord("Health_Appointment_CDV6_17082", "Used for Unscheduled Appointment Update", _careDirectorQA_TeamId, _HealthAppointmentBusinessObjectId, _workflowXml.Replace("BusinessObjectIdToReplaceHere", _HealthAppointmentBusinessObjectId.ToString()), false, true, "ResponsibleUserId");
            }
            if (_workflowId == Guid.Empty)
                _workflowId = dbHelper.workflow.GetWorkflowByName("Health_Appointment_CDV6_17082")[0];

            #endregion

            var unscheduledAppointment = dbHelper.healthAppointment.CreateHealthAppointmentForUnscheduled(_careDirectorQA_TeamId, _personID, _dataFormId, _communityClinicAppointmentContactTypesId, _healthAppointmentReasonId, _caseId,
                                            DateTime.Now, new TimeSpan(0, 5, 0), DateTime.Now, new TimeSpan(1, 30, 0), AutomationCDV617082User1_SystemUserId, _communityAndClinicTeamId,
                                            _communityClinicLocationTypesId, 8, AutomationCDV617082User1_SystemUserId, "Unscheduled Appointment 1", false);


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

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .WaitForCaseHealthAppointmentsPageToLoad()
                .SelectCaseHealthAppointmentRecord(unscheduledAppointment.ToString())
                .OpenCaseHealthAppointmentRecord(unscheduledAppointment.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickResponsibleUserLookupButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("Automation_CDV6-17082_Test_User_2")
                 .TapSearchButton()
                 .SelectResultElement(AutomationCDV617082User2_SystemUserId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .TapSaveButton();

            caseHealthAppointmentRecordPage
                .TapBackButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .WaitForCaseHealthAppointmentsPageToLoad()
                .SelectCaseHealthAppointmentRecord(unscheduledAppointment.ToString())
                .OpenCaseHealthAppointmentRecord(unscheduledAppointment.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad();

            //get all "Not Started" workflow jobs
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(_workflowId, 1).FirstOrDefault();

            //authenticate against the v6 Web API
            //this.WebAPIHelper.Security.Authenticate();
            WebAPIHelper.Security.Authenticate();

            //execute the Workflow Job and wait for the Idle status
            WebAPIHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            var caseNote = dbHelper.healthAppointmentCaseNote.GetCaseNoteByHealthAppointmentID(unscheduledAppointment);
            Assert.AreEqual(1, caseNote.Count);

            #endregion

        }


        [TestProperty("JiraIssueID", "CDV6-17497")]
        [Description("Login CD application with security profiles Add CW Unscheduled Health Appointments " + "Open person record in test data -> click on cases -> Open Community case -> Menu -> Related Items -> Click on Health Appointments/ Contacts & offers ->" +
            "Click on + -> Select Unscheduled Appointments in Select Health Appointment Type pop up." + "Enter all the mandatory fields" +
            "Click on Save" + "update the changes in unscheduled appointment to trigger the workflow " + "Verify the case note record is generated against the unscheduled appointment record upon successful run of workflow." +
            "Set Yes in Additional Professional Required? Flag -> Click save button -> Verify that Additional flag sub grid is displayed in the unscheduled appointment record." +
            "Click create new record button in additional professional grid -> Verify the Professional remaining for full duration? Flag is set to yes and the fields associated with the flag is disabled -> Click save button -> Verify the validation is displayed on the required fields." +
            "Select the lead professional in the professional field -> Click Save button -> Verify the validation message is displayed as \"The Professional is already added to the Appointment as the Lead Health Professional.\"" +
            "Update the lead professional lookup with another user -> Click save and return to previous page button -> Verify that user able to create the additional professional record against the unscheduled appointment." +
            "Click Create new record button in additional professional grid -> Select the same user selected in above step -> Verify that the validation message is displayed as \"The Professional is already added to the Appointment.\"" +
            "Select another user (user 3) -> Click save and return to previous page button -> Verify that user able to create the additional professional record against the unscheduled appointment record." +
            "Open the additional profession record -> Select Professional remaining for full duration? Flag to 'No' -> Update the start/end time which is differs from lead appointment time -> Verify the validation message is displayed as 'The Dates / Times should overlap the Appointment Dates / Times.'" +
            "Click Close button -> Set the time same as lead appointment record and update the start/end date which is differs from lead appointment -> Click Save button -> Verify the validation message is displayed as 'The Dates / Times should overlap the Appointment Dates / Times.'" +
            "Click Close button -> Verify that user able to update the start date/time and end date/time of additional health professional appointment within the lead's appointment record." +
            "Verify that user able to create the multiple unscheduled appointment record by overlapping with the existing appointment record.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void UnscheduledHealthAppointmentsBatch02_UITestCases02()
        {

            var unscheduledAppointment = dbHelper.healthAppointment.CreateHealthAppointmentForUnscheduled(_careDirectorQA_TeamId, _personID, _dataFormId, _communityClinicAppointmentContactTypesId, _healthAppointmentReasonId, _caseId,
                                            DateTime.Now, new TimeSpan(0, 5, 0), DateTime.Now, new TimeSpan(1, 30, 0), AutomationCDV617082User1_SystemUserId, _communityAndClinicTeamId,
                                            _communityClinicLocationTypesId, 8, AutomationCDV617082User1_SystemUserId, "Unscheduled Appointment 1", false);

            #region Step 12 and Step 13
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

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .WaitForCaseHealthAppointmentsPageToLoad()
                .SelectCaseHealthAppointmentRecord(unscheduledAppointment.ToString())
                .OpenCaseHealthAppointmentRecord(unscheduledAppointment.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickAdditionalProfessionalRequired_RadioButton()
                .TapSaveButton()
                .WaitForRecordToBeClosedAndSaved();

            caseHealthAppointmentRecordPage
                .WaitForAdditionalProfessionalRequiredAreaToLoad();

            caseHealthAppointmentRecordPage
                .ClickAddNewRecordButton();

            communityClinicAdditionalHealthProfessionalRecordPage
                .WaitForCommunityClinicAdditionalHealthProfessionalRecordPageToLoad()
                .ValidateProfessionalRemainingForFullDurationOption(true)
                .ValidateStartDateFieldDisabled()
                .ValidateStartTimeFieldDisabled()
                .ValidateEndDateFieldDisabled()
                .ValidateEndTimeFieldDisabled()
                .TapSaveButton();

            communityClinicAdditionalHealthProfessionalRecordPage
                .ValidateNotificationMessage("Some data is not correct. Please review the data in the Form.")
                .ValidateProfessionalFieldRequiredErrorMessage("Please fill out this field.");

            #endregion

            #region Step 14 to Step 20

            communityClinicAdditionalHealthProfessionalRecordPage
                .ClickHealthProfessionalFieldLookupButton();


            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery(_loginUsername)
                 .TapSearchButton()
                 .SelectResultElement(AutomationCDV617082User1_SystemUserId.ToString());

            communityClinicAdditionalHealthProfessionalRecordPage
                .WaitForCommunityClinicAdditionalHealthProfessionalRecordPageToLoad()
                .TapSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("The Professional is already added to the Appointment as the Lead Health Professional.")
                .TapCloseButton();

            communityClinicAdditionalHealthProfessionalRecordPage
                .WaitForCommunityClinicAdditionalHealthProfessionalRecordPageToLoad()
                .ClickHealthProfessionalFieldLookupButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery(_systemUsername2)
                 .TapSearchButton()
                 .SelectResultElement(AutomationCDV617082User2_SystemUserId.ToString());

            communityClinicAdditionalHealthProfessionalRecordPage
                .WaitForCommunityClinicAdditionalHealthProfessionalRecordPageToLoad()
                .TapSaveButton()
                .ValidateCommunityClinicHealthAppointmentFieldValue(_personFullName + ", " + _healthappointmentReason)
                .ValidateProfessionalRemainingForFullDurationOption(true)
                .ValidateHealthProfessionalFieldValue("Automation CDV6-17082 Test User 2")
                .ValidateStartDate(commonMethodsHelper.GetCurrentDate())
                .ValidateStartTime("00:05")
                .ValidateEndDate(commonMethodsHelper.GetCurrentDate())
                .ValidateEndTime("01:30");

            communityClinicAdditionalHealthProfessionalRecordPage
                .TapBackButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .WaitForAdditionalProfessionalRequiredAreaToLoad()
                .ClickAddNewRecordButton();

            communityClinicAdditionalHealthProfessionalRecordPage
                .WaitForCommunityClinicAdditionalHealthProfessionalRecordPageToLoad()
                .ClickHealthProfessionalFieldLookupButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery(_systemUsername2)
                 .TapSearchButton()
                 .SelectResultElement(AutomationCDV617082User2_SystemUserId.ToString());

            communityClinicAdditionalHealthProfessionalRecordPage
                .WaitForCommunityClinicAdditionalHealthProfessionalRecordPageToLoad()
                .TapSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("The Professional is already added to the Appointment.")
                .TapCloseButton();

            communityClinicAdditionalHealthProfessionalRecordPage
                .WaitForCommunityClinicAdditionalHealthProfessionalRecordPageToLoad()
                .ClickHealthProfessionalFieldLookupButton();

            lookupPopup
                  .WaitForLookupPopupToLoad()
                  .TypeSearchQuery(_systemUsername3)
                  .TapSearchButton()
                  .SelectResultElement(AutomationCDV617082User3_SystemUserId.ToString());

            communityClinicAdditionalHealthProfessionalRecordPage
                .WaitForCommunityClinicAdditionalHealthProfessionalRecordPageToLoad()
                .TapSaveButton();

            communityClinicAdditionalHealthProfessionalRecordPage
                .SelectProfessionalRemainingForFullDurationOption(false)
                .InsertStartTime("12:05")
                .InsertEndTime("12:30")
                .TapSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("The Dates/Times should overlap the Appointment Dates/Times.")
                .TapCloseButton();

            communityClinicAdditionalHealthProfessionalRecordPage
                .WaitForCommunityClinicAdditionalHealthProfessionalRecordPageToLoad()
                .InsertStartTime("00:05")
                .InsertEndTime("01:30")
                .InsertStartDate(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .InsertEndDate(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .TapSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("The Dates/Times should overlap the Appointment Dates/Times.")
                .TapCloseButton();

            communityClinicAdditionalHealthProfessionalRecordPage
                .WaitForCommunityClinicAdditionalHealthProfessionalRecordPageToLoad()
                .InsertStartTime("00:30")
                .InsertEndTime("01:00")
                .InsertStartDate(commonMethodsHelper.GetCurrentDate())
                .InsertEndDate(commonMethodsHelper.GetCurrentDate())
                .TapSaveButton()
                .ValidateStartDate(commonMethodsHelper.GetCurrentDate())
                .ValidateStartTime("00:30")
                .ValidateEndDate(commonMethodsHelper.GetCurrentDate())
                .ValidateEndTime("01:00");

            #endregion

            #region Step 21
            dbHelper.healthAppointment.CreateHealthAppointmentForUnscheduled(_careDirectorQA_TeamId, _personID, _dataFormId, _communityClinicAppointmentContactTypesId, _healthAppointmentReasonId, _caseId,
                                                        DateTime.Now, new TimeSpan(0, 5, 0), DateTime.Now, new TimeSpan(1, 30, 0), AutomationCDV617082User1_SystemUserId, _communityAndClinicTeamId,
                                                        _communityClinicLocationTypesId, 8, AutomationCDV617082User1_SystemUserId, "Unscheduled Appointment 2", false);

            dbHelper.healthAppointment.CreateHealthAppointmentForUnscheduled(_careDirectorQA_TeamId, _personID, _dataFormId, _communityClinicAppointmentContactTypesId, _healthAppointmentReasonId, _caseId,
                                            DateTime.Now, new TimeSpan(0, 5, 0), DateTime.Now, new TimeSpan(1, 30, 0), AutomationCDV617082User1_SystemUserId, _communityAndClinicTeamId,
                                            _communityClinicLocationTypesId, 8, AutomationCDV617082User1_SystemUserId, "Unscheduled Appointment 3", false);

            var healthAppointments = dbHelper.healthAppointment.GetHealthAppointmentByCaseID(_caseId);
            Assert.AreEqual(3, healthAppointments.Count);
            #endregion


        }


        [TestProperty("JiraIssueID", "CDV6-17498")]
        [Description("Login CD application with security profiles Add CW Unscheduled Health Appointments. Create an Unscheduled Appointment Record " +
            "Navigate to Workplace -> My work -> Health Diary -> Select the community team mentioned in the pre - requisite -> Select the lead professional -> Verify that the unscheduled appointment record created against the lead professional is not displayed in the health diary grid." +
            "Open the lead profession in system users view -> Click Diary -> Verify that the Unscheduled appointment is not displayed in the user diary grid.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void UnscheduledHealthAppointmentsBatch02_UITestCases03()
        {

            #region Step 22

            var unscheduledAppointment = dbHelper.healthAppointment.CreateHealthAppointmentForUnscheduled(_careDirectorQA_TeamId, _personID, _dataFormId, _communityClinicAppointmentContactTypesId, _healthAppointmentReasonId, _caseId,
                                            DateTime.Now, new TimeSpan(3, 0, 0), DateTime.Now, new TimeSpan(3, 30, 0), AutomationCDV617082User1_SystemUserId, _communityAndClinicTeamId,
                                            _communityClinicLocationTypesId, 8, AutomationCDV617082User1_SystemUserId, "Unscheduled Appointment 1", false);


            loginPage
                  .GoToLoginPage()
                  .Login(_loginUsername, "Passw0rd_!")
                  .WaitFormHomePageToLoad();

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToHealthDiarySection();

            healthDiaryViewPage
                .WaitForHealthDiaryViewPageToLoad()
                .WaitForCalendarToLoad()
                .InsertDate(commonMethodsHelper.GetCurrentDate());

            healthDiaryViewPage
                .ClickCommunityClinicTeamLookupButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery(_CommunityclinicTeams_Title)
                 .TapSearchButton()
                 .SelectResultElement(_communityAndClinicTeamId.ToString());

            healthDiaryViewPage
                .WaitForHealthDiaryViewPageToLoad()
                .ClickHomeVisitYesNoOption(false)
                .ClickLocationLookupButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("Automation_Provider_HealthAppointments_17802")
                 .TapSearchButton()
                 .SelectResultElement(_provider_HospitalId.ToString());

            healthDiaryViewPage
                .WaitForHealthDiaryViewPageToLoad()
                .WaitForCalendarToLoad()
                .ValidateUnscheduledHealthAppointmentNotDisplayed("03:00", DateTime.Now.ToString("yyyy-MM-dd"), unscheduledAppointment.ToString());
            #endregion

            #region Step 23
            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
             .WaitForSystemUsersPageToLoad()
             .InsertUserName(_systemUsername2)
             .ClickSearchButton()
             .OpenRecord(AutomationCDV617082User2_SystemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDiaryPage();

            systemUser_DiaryPage
                .WaitForDiaryPageToLoad()
                .WaitForCalendarToLoad()
                .InsertDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidateUnscheduledHealthAppointmentNotDisplayed("03:00", DateTime.Now.ToString("yyyy-MM-dd"), unscheduledAppointment.ToString());

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
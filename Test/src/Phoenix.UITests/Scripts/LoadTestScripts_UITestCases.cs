
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.UITests.SmokeTests
{
    //[TestClass]
    public class LoadTestScripts_UITestCases : FunctionalTest
    {
        private Guid _authenticationproviderid = new Guid("64d2d456-11dc-e611-80d4-0050560502cc");//Internal
        private Guid _load_Test_User_01Id;
        private Guid _languageId;
        private Guid _careDirector_BusinessUnitId;
        private Guid _careDirector_TeamId;
        private Guid _recurrencePatternId;
        private Guid _ethnicity1Id;
        private Guid _ethnicity2Id;
        private Guid _ethnicity3Id;
        private Guid _ethnicity4Id;
        private Guid _attachDocumentTypeId;
        private Guid _attachDocumentSubTypeId;
        private Guid _financeclientcategoryid;
        private Guid _glCodeLocationId;
        private Guid _glCodeId;
        private Guid _rateUnitId;
        private Guid _serviceProvisionStartReasonId;
        private Guid _serviceElement1Id;
        private Guid _serviceElement2Id;
        private Guid _currentRannkingId;
        private Guid _providerId_Supplier;
        private Guid _serviceProvidedId;
        private Guid _placementRoomTypeId;
        private Guid _financialAssessmentStatusId_Draft;
        private Guid _financialAssessmentStatusId_ReadyForAuthorisation;
        private Guid _financialAssessmentStatusId_Authorised;
        private Guid _incomeSupportTypeId;
        private Guid _financeScheduleTypeId;
        private Guid _chargingRuleTypeId;
        private Guid _financialAssessmentTypeId;
        private Guid _contributionTypeId;
        private Guid _recoveryMethodId;
        private Guid _DebtorBatchGroupingId;
        private Guid _providerId_Carer;
        private Guid _communityAndClinicTeamId;
        private Guid _communityClinicDiaryViewSetupId;
        private Guid _caseStatusId;
        private Guid _socialCareCaseStatusId;
        private Guid _contactReasonId;
        private Guid _contactAdministrativeCategory;
        private Guid _caseServiceTypeRequestedid;
        private Guid _dataFormId_CommunityHealthCase;
        private Guid _dataFormId_SocialCareCase;
        private Guid _dataFormId_Appointments;
        private Guid _ContactSourceId;
        private Guid _healthAppointmentContactTypeId;
        private Guid _healthAppointmentLocationTypeId;
        private Guid _healthAppointmentReasonId;
        private Guid _applicationId;
        private Guid _homeScreenId;

        //[TestInitialize()]
        public void LoadScript_Setup()
        {

            #region Language

            var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
            if (!language)
                dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
            _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

            #endregion Language

            #region Business Unit

            var businessUnitExists = dbHelper.businessUnit.GetByName("CareDirector").Any();
            if (!businessUnitExists)
                dbHelper.businessUnit.CreateBusinessUnit("CareDirector");
            _careDirector_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector")[0];

            #endregion

            #region Team

            var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector").Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam("CareDirector", null, _careDirector_BusinessUnitId, "1", "CareDirectorQA@careworkstempmail.com", "Default team for business unit", "");
            _careDirector_TeamId = dbHelper.team.GetTeamIdByName("CareDirector")[0];

            #endregion

            #region Recurrence pattern

            var recurrencePatternExists = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").Any();
            if (!recurrencePatternExists)
                _recurrencePatternId = dbHelper.recurrencePattern.CreateRecurrencePattern(1, 1);

            if (_recurrencePatternId == Guid.Empty)
                _recurrencePatternId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").FirstOrDefault();

            #endregion

            #region System User

            var automationSmokeTestUser1Exists = dbHelper.systemUser.GetSystemUserByUserName("Load_Test_User_01").Any();
            if (!automationSmokeTestUser1Exists)
            {
                _load_Test_User_01Id = dbHelper.systemUser.CreateSystemUser("Load_Test_User_01", "Automation", "Smoke Test User 1", "Load Test User 1", "Passw0rd_!", "Load_Test_User_01@somemail.com", "Load_Test_User_01@secureemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirector_BusinessUnitId, _careDirector_TeamId, DateTime.Now.Date);

                var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Systemuser - Secure Fields (Edit)").First();

                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_load_Test_User_01Id, systemAdministratorSecurityProfileId);
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_load_Test_User_01Id, systemUserSecureFieldsSecurityProfileId);

                dbHelper.userWorkSchedule.CreateUserWorkSchedule("default", _careDirector_TeamId, _load_Test_User_01Id, _recurrencePatternId, new DateTime(2021, 1, 1), null, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0));
            }
            if (_load_Test_User_01Id == Guid.Empty)
            {
                _load_Test_User_01Id = dbHelper.systemUser.GetSystemUserByUserName("Load_Test_User_01").FirstOrDefault();
            }

            #endregion

            #region Ethnicity

            var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("English").Any();
            if (!ethnicitiesExist)
                dbHelper.ethnicity.CreateEthnicity(_careDirector_TeamId, "English", new DateTime(2020, 1, 1));
            _ethnicity1Id = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];

            ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("Irish").Any();
            if (!ethnicitiesExist)
                dbHelper.ethnicity.CreateEthnicity(_careDirector_TeamId, "Irish", new DateTime(2020, 1, 1));
            _ethnicity2Id = dbHelper.ethnicity.GetEthnicityIdByName("Irish")[0];

            ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("Indian").Any();
            if (!ethnicitiesExist)
                dbHelper.ethnicity.CreateEthnicity(_careDirector_TeamId, "Indian", new DateTime(2020, 1, 1));
            _ethnicity3Id = dbHelper.ethnicity.GetEthnicityIdByName("Indian")[0];

            ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("African").Any();
            if (!ethnicitiesExist)
                dbHelper.ethnicity.CreateEthnicity(_careDirector_TeamId, "African", new DateTime(2020, 1, 1));
            _ethnicity4Id = dbHelper.ethnicity.GetEthnicityIdByName("African")[0];

            #endregion

            #region Attach Document Type

            var attachDocumentTypeExists = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Text File").Any();
            if (!attachDocumentTypeExists)
                dbHelper.attachDocumentType.CreateAttachDocumentType(_careDirector_TeamId, "Text File", new DateTime(2020, 1, 1));
            _attachDocumentTypeId = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Text File")[0];

            #endregion

            #region Attach Document Sub Type

            var attachDocumentSubTypeExists = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("MS Word Document").Any();
            if (!attachDocumentSubTypeExists)
                dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(_careDirector_TeamId, "MS Word Document", new DateTime(2020, 1, 1), _attachDocumentTypeId);
            _attachDocumentSubTypeId = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("MS Word Document")[0];

            #endregion

            #region Finance Client Category

            var financeclientcategoryExists = dbHelper.financeClientCategory.GetByName("PLM").Any();
            if (!financeclientcategoryExists)
                dbHelper.financeClientCategory.CreateFinanceClientCategory(_careDirector_TeamId, "PLM", new DateTime(2020, 1, 1), "11");
            _financeclientcategoryid = dbHelper.financeClientCategory.GetByName("PLM")[0];

            #endregion

            #region GL Code Location

            _glCodeLocationId = dbHelper.glCodeLocation.GetByName("Special Scheme Type").FirstOrDefault();

            #endregion

            #region GL Code

            var glCodeExists = dbHelper.glCode.GetByName("123 \\ Special Scheme \\ Exempt from Charging? = No").Any();
            if (!glCodeExists)
                dbHelper.glCode.CreateGLCode(_careDirector_TeamId, _glCodeLocationId, "Special Scheme", "123", "123");
            _glCodeId = dbHelper.glCode.GetByName("123 \\ Special Scheme \\ Exempt from Charging? = No")[0];

            #endregion

            #region RateUnit

            _rateUnitId = dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault();

            #endregion

            #region Service Provision Start Reason

            var serviceProvisionStartReasonExists = dbHelper.serviceProvisionStartReason.GetByName("New Placement").Any();
            if (!serviceProvisionStartReasonExists)
                dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(_careDirector_TeamId, _careDirector_BusinessUnitId, "New Placement", new DateTime(2020, 1, 1));
            _serviceProvisionStartReasonId = dbHelper.serviceProvisionStartReason.GetByName("New Placement")[0];

            #endregion

            #region Service Element 1

            var serviceElement1Exists = dbHelper.serviceElement1.GetByName("QA - SE1").Any();
            if (!serviceElement1Exists)
                dbHelper.serviceElement1.CreateServiceElement1(_careDirector_TeamId, "QA - SE1", new DateTime(2020, 1, 1), 123321, 1, 1);
            _serviceElement1Id = dbHelper.serviceElement1.GetByName("QA - SE1")[0];

            #endregion

            #region Service Element 1 Valid Rate Unit

            var serviceElement1RateUnitExists = dbHelper.serviceElement1ValidRateUnits.GetByServiceElement1AndRateUnit(_serviceElement1Id, _rateUnitId).Any();
            if (!serviceElement1RateUnitExists)
                dbHelper.serviceElement1ValidRateUnits.CreateServiceElement1ValidRateUnits(_serviceElement1Id, _rateUnitId);
            _serviceElement1Id = dbHelper.serviceElement1.GetByName("QA - SE1")[0];

            #endregion

            #region Service Element 2

            var serviceElement2Exists = dbHelper.serviceElement2.GetByName("QA - SE2").Any();
            if (!serviceElement2Exists)
                dbHelper.serviceElement2.CreateServiceElement2(_careDirector_TeamId, _careDirector_BusinessUnitId, "QA - SE2", new DateTime(2020, 1, 1), 1);
            _serviceElement2Id = dbHelper.serviceElement2.GetByName("QA - SE2")[0];

            #endregion

            #region Current Ranking

            _currentRannkingId = dbHelper.currentRanking.GetCurrentRankingByName("Outstanding").FirstOrDefault();

            #endregion

            #region Provider (Supplier)

            var supplierProviderExists = dbHelper.provider.GetProviderByName("CareDirector QA Provider").Any();
            if (!supplierProviderExists)
            {
                _providerId_Supplier = dbHelper.provider.CreateProvider("CareDirector QA Provider", _careDirector_TeamId, 2);
                _serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_careDirector_TeamId, _load_Test_User_01Id, _providerId_Supplier, _serviceElement1Id, _serviceElement2Id, _financeclientcategoryid, _currentRannkingId, _glCodeId, 2);

            }
            if (_providerId_Supplier == Guid.Empty)
            {
                _providerId_Supplier = dbHelper.provider.GetProviderByName("CareDirector QA Provider").FirstOrDefault();
                _serviceProvidedId = dbHelper.serviceProvided.GetByProviderId(_providerId_Supplier).FirstOrDefault();
            }

            #endregion

            #region Placement Room Type

            _placementRoomTypeId = dbHelper.placementRoomType.GetPlacementRoomTypeByName("Not Applicable").FirstOrDefault();

            #endregion

            #region Financial Assessment Status

            _financialAssessmentStatusId_Draft = dbHelper.financialAssessmentStatus.GetFinancialAssessmentStatusByName("Draft").FirstOrDefault();
            _financialAssessmentStatusId_ReadyForAuthorisation = dbHelper.financialAssessmentStatus.GetFinancialAssessmentStatusByName("Ready for Authorisation").FirstOrDefault();
            _financialAssessmentStatusId_Authorised = dbHelper.financialAssessmentStatus.GetFinancialAssessmentStatusByName("Authorised").FirstOrDefault();

            #endregion

            #region Income Support Type

            _incomeSupportTypeId = dbHelper.incomeSupportType.GetByName("IS (Manual Value)").FirstOrDefault();

            #endregion

            #region Finance Schedule Type

            _financeScheduleTypeId = dbHelper.financeScheduleType.GetByName("Residential").FirstOrDefault();

            #endregion

            #region Charging Rule Type

            var chargingRuleTypeExists = dbHelper.chargingRuleType.GetChargingRuleTypeByName("Residential Permanent Stay").Any();
            if (!chargingRuleTypeExists)
            {
                _chargingRuleTypeId = dbHelper.chargingRuleType.CreateChargingRuleType("Residential Permanent Stay", _careDirector_TeamId, new DateTime(2020, 1, 1));
                dbHelper.incomeSupportTypeChargingRuleTypes.CreateIncomeSupportTypeChargingRuleTypes(_incomeSupportTypeId, _chargingRuleTypeId);
                dbHelper.scheduleSetup.CreateScheduleSetup(_careDirector_TeamId, _financeScheduleTypeId, _chargingRuleTypeId, new DateTime(2020, 1, 1), 100);
                dbHelper.chargingRuleSetup.CreateChargingRuleSetup(_careDirector_TeamId, _chargingRuleTypeId, 3, new DateTime(020, 1, 1));
                dbHelper.chargeforServicesSetup.CreateChargeforServicesSetup(_careDirector_TeamId, _chargingRuleTypeId, _serviceElement1Id, _serviceElement2Id, _financeclientcategoryid, _rateUnitId, new DateTime(2020, 1, 1));
            }
            if (_chargingRuleTypeId == Guid.Empty)
            {
                _chargingRuleTypeId = dbHelper.chargingRuleType.GetChargingRuleTypeByName("Residential Permanent Stay").FirstOrDefault();
            }

            #endregion

            #region Financial Assessment Type

            _financialAssessmentTypeId = dbHelper.financialAssessmentType.GetByName("Full Assessment").FirstOrDefault();

            #endregion

            #region Contribution Type

            _contributionTypeId = dbHelper.contributionType.GetByName("Person Charge").FirstOrDefault();

            #endregion

            #region Recovery Method

            _recoveryMethodId = dbHelper.recoveryMethod.GetByName("Debtor Invoice").FirstOrDefault();

            #endregion

            #region Recovery Method

            _DebtorBatchGroupingId = dbHelper.debtorBatchGrouping.GetByName("Batching Not Applicable").FirstOrDefault();

            #endregion

            #region Provider (Carer)

            var carerProviderExists = dbHelper.provider.GetProviderByName("CareDirector QA Carer Provider").Any();
            if (!carerProviderExists)
            {
                _providerId_Carer = dbHelper.provider.CreateProvider("CareDirector QA Carer Provider", _careDirector_TeamId, 7);

            }
            if (_providerId_Carer == Guid.Empty)
            {
                _providerId_Carer = dbHelper.provider.GetProviderByName("CareDirector QA Carer Provider").FirstOrDefault();
            }

            #endregion

            #region Community and Clinic Team

            //var communityAndClinicTeamExists = dbHelper.communityAndClinicTeam.GetByTitle("CareDirector QA Community And Clinic Team").Any();
            //if (!communityAndClinicTeamExists)
            //{
            //    _communityAndClinicTeamId = dbHelper.communityAndClinicTeam.CreateCommunityAndClinicTeam(_careDirector_TeamId, _providerId_Carer, _careDirector_TeamId, "CareDirector QA Community And Clinic Team", "Created by the load testing scripts");
            //    _communityClinicDiaryViewSetupId = dbHelper.communityClinicDiaryViewSetup.CreateCommunityClinicDiaryViewSetup(_careDirector_TeamId, _communityAndClinicTeamId, "Community And Clinic Team - Home Visit", new DateTime(2020, 1, 1), new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), 500, 100, 500);



            //}
            //if (_communityClinicDiaryViewSetupId == Guid.Empty)
            //{
            //    _communityAndClinicTeamId = dbHelper.communityAndClinicTeam.GetByTitle("CareDirector QA Community And Clinic Team").FirstOrDefault();
            //    _communityClinicDiaryViewSetupId = dbHelper.communityClinicDiaryViewSetup.GetByTitle("Community And Clinic Team - Home Visit").FirstOrDefault();
            //}

            #endregion

            #region Case Status

            _caseStatusId = dbHelper.caseStatus.GetByName("New Cases").FirstOrDefault();
            _socialCareCaseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();

            #endregion

            #region Contact Reason

            var contactReasonExists = dbHelper.contactReason.GetByName("Advice/Consultation").Any();
            if (!contactReasonExists)
                _contactReasonId = dbHelper.contactReason.CreateContactReason(_careDirector_TeamId, "Advice/Consultation", new DateTime(2020, 1, 1), 140000000, false);

            if (_contactReasonId == Guid.Empty)
                _contactReasonId = dbHelper.contactReason.GetByName("Advice/Consultation")[0];

            #endregion

            #region Contact Administrative Category

            var contactAdministrativeCategoryExists = dbHelper.contactAdministrativeCategory.GetByName("NHS Patient").Any();

            if (!contactAdministrativeCategoryExists)
                _contactAdministrativeCategory = dbHelper.contactAdministrativeCategory.CreateContactAdministrativeCategory(_careDirector_TeamId, "NHS Patient", new DateTime(2020, 1, 1));

            if (_contactAdministrativeCategory == Guid.Empty)
                _contactAdministrativeCategory = dbHelper.contactAdministrativeCategory.GetByName("NHS Patient").FirstOrDefault();

            #endregion

            #region Case Service Type Requested

            var caseServiceTypeRequestedExists = dbHelper.caseServiceTypeRequested.GetByName("Advice and Consultation").Any();

            if (!caseServiceTypeRequestedExists)
                _caseServiceTypeRequestedid = dbHelper.caseServiceTypeRequested.CreateCaseServiceTypeRequested(_careDirector_TeamId, "Advice and Consultation", new DateTime(2020, 1, 1));

            if (_caseServiceTypeRequestedid == Guid.Empty)
                _caseServiceTypeRequestedid = dbHelper.caseServiceTypeRequested.GetByName("Advice and Consultation").FirstOrDefault();

            #endregion

            #region Data Form

            _dataFormId_CommunityHealthCase = dbHelper.dataForm.GetByName("CommunityHealthCase").FirstOrDefault();
            _dataFormId_SocialCareCase = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            _dataFormId_Appointments = dbHelper.dataForm.GetByName("Appointments").FirstOrDefault();

            #endregion

            #region Contact Source

            var contactSourceExists = dbHelper.contactSource.GetByName("ANONYMOUS").Any();

            if (!contactSourceExists)
                _ContactSourceId = dbHelper.contactSource.CreateContactSource(_careDirector_TeamId, "ANONYMOUS", new DateTime(2020, 1, 1), "21", "10");

            if (_ContactSourceId == Guid.Empty)
                _ContactSourceId = dbHelper.contactAdministrativeCategory.GetByName("ANONYMOUS").FirstOrDefault();


            #endregion

            #region Health Appointment Contact Type

            //_healthAppointmentContactTypeId = dbHelper.healthAppointmentContactType.GetByName("Face To Face").FirstOrDefault();

            #endregion

            #region Health Appointment Reason

            //var healthAppointmentReasonExists = dbHelper.healthAppointmentReason.GetByName("Assessment").Any();

            //if (!healthAppointmentReasonExists)
            //    _healthAppointmentReasonId = dbHelper.healthAppointmentReason.CreateHealthAppointmentReason(_careDirector_TeamId, "Assessment", new DateTime(2020, 1, 1), "1", null);

            //if (_healthAppointmentReasonId == Guid.Empty)
            //    _healthAppointmentReasonId = dbHelper.healthAppointmentReason.GetByName("Assessment").FirstOrDefault();

            #endregion

            #region application

            _applicationId = dbHelper.application.GetByName("CareDirector").FirstOrDefault();

            #endregion

            #region homeScreen

            _homeScreenId = dbHelper.homeScreen.GetHomeScreenByName("DT Customer based example Home screen").FirstOrDefault();

            #endregion
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-14428

        //[Description("")]
        //[TestMethod]
        public void LoadTestScript_GetPersonRecordsPerTeam_Script01()
        {
            List<Guid> teams = new List<Guid>();

            for (int i = 601; i <= 650; i++)
            {
                var username = "performance_user_" + i;
                var userid = dbHelper.systemUser.GetSystemUserByUserName(username).First();
                var defaultTeamid = Guid.Parse((dbHelper.systemUser.GetSystemUserBySystemUserID(userid, "defaultteamid")["defaultteamid"]).ToString());

                if (!teams.Contains(defaultTeamid))
                    teams.Add(defaultTeamid);
            }
            Console.WriteLine("Team Id,Team Name,Person Id, Person Number, First Name, Last Name");


            foreach (var teamid in teams)
            {
                var teamName = (string)dbHelper.team.GetTeamByID(teamid, "name")["name"];

                List<Guid> personRecords = dbHelper.person.GetByResponsibleTeam(teamid, 1000);

                foreach (var personid in personRecords)
                {
                    var personData = dbHelper.person.GetPersonById(personid, "personnumber", "firstname", "lastname");
                    Console.WriteLine(teamid + "," + teamName + "," + personid + "," + personData["personnumber"] + "," + personData["firstname"] + "," + personData["lastname"]);
                }
            }

        }

        [Description("Assign correct security profiles to the users")]
        //[TestMethod]
        public void LoadTestScript_AssignCorrectSecurityProfilesToUsers_Script01()
        {
            var securityProfileId1 = dbHelper.securityProfile.GetSecurityProfileByName("Activities (BU Edit)").First();
            var securityProfileId2 = dbHelper.securityProfile.GetSecurityProfileByName("Advanced Search").First();
            var securityProfileId3 = dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User").First();
            var securityProfileId4 = dbHelper.securityProfile.GetSecurityProfileByName("Case Module (BU Edit)").First();
            var securityProfileId5 = dbHelper.securityProfile.GetSecurityProfileByName("Contact Module (BU Edit)").First();
            var securityProfileId6 = dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (Edit)").First();
            var securityProfileId7 = dbHelper.securityProfile.GetSecurityProfileByName("Finance Reference Data (BU Edit)").First();
            var securityProfileId8 = dbHelper.securityProfile.GetSecurityProfileByName("Financial Assessment (BU Edit)").First();
            var securityProfileId9 = dbHelper.securityProfile.GetSecurityProfileByName("Person - Secure Fields (Edit)").First();
            var securityProfileId10 = dbHelper.securityProfile.GetSecurityProfileByName("Person Module (BU Edit)").First();
            var securityProfileId11 = dbHelper.securityProfile.GetSecurityProfileByName("Service Provision (BU Edit)").First();
            var securityProfileId12 = dbHelper.securityProfile.GetSecurityProfileByName("Timeline Record (BU View)").First();


            foreach (var userid in dbHelper.systemUser.GetSystemUserByUserNamePrefix("performance_user_"))
            {
                foreach (var userSecurityProfileId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userid))
                {
                    dbHelper.userSecurityProfile.DeleteUserSecurityProfile(userSecurityProfileId);
                }

                dbHelper.userSecurityProfile.CreateUserSecurityProfile(userid, securityProfileId1);
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(userid, securityProfileId2);
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(userid, securityProfileId3);
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(userid, securityProfileId4);
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(userid, securityProfileId5);
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(userid, securityProfileId6);
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(userid, securityProfileId7);
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(userid, securityProfileId8);
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(userid, securityProfileId9);
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(userid, securityProfileId10);
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(userid, securityProfileId11);
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(userid, securityProfileId12);
            }
        }

        [Description("Assign correct security profiles to the users")]
        //[TestMethod]
        public void LoadTestScript_AssignCorrectSecurityProfilesToUsers_Script02()
        {
            var securityProfileId1 = dbHelper.securityProfile.GetSecurityProfileByName("Provider (BU Edit)").First();
            var securityProfileId2 = dbHelper.securityProfile.GetSecurityProfileByName("Finance Setup (BU Edit)").First();
            var securityProfileId3 = dbHelper.securityProfile.GetSecurityProfileByName("Finance Area Admin").First();
            var securityProfileId4 = dbHelper.securityProfile.GetSecurityProfileByName("Settings Area Access").First();
            int count = 1;

            foreach (var userid in dbHelper.systemUser.GetSystemUserByUserNamePrefix("performance_user_"))
            {
                Console.WriteLine("User --> " + count);
                count++;

                dbHelper.userSecurityProfile.CreateMultipleUserSecurityProfile(userid, new List<Guid> { securityProfileId1, securityProfileId2, securityProfileId3, securityProfileId4 });
            }
        }

        [Description("All users are currently linked to 1 team. Create new teams and link users to the new teams")]
        //[TestMethod]
        public void LoadTestScript_AddNewTeamsAndLinkNewUsers_Script01()
        {
            DateTime startDate = new DateTime(2020, 1, 1);
            for (int i = 1; i <= 100; i++)
            {
                var teamName = "CD VP Testing Team " + i;
                var teamEmail = "CDVPTestingTeam" + i + "@careworkstempmail.com";

                var teamsExist = dbHelper.team.GetTeamIdByName(teamName).Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam(teamName, null, _careDirector_BusinessUnitId, "1", teamEmail, "Team used for VP Testing", "");
                var newTeamid = dbHelper.team.GetTeamIdByName(teamName)[0];

                var matchingSystemUsers = dbHelper.systemUser.GetByDefaultTeamId("performance_user_", _careDirector_TeamId).Take(10); //get 10 users that belong to the default team
                if (matchingSystemUsers != null && matchingSystemUsers.Count() > 0)
                {
                    foreach (var systemUserId in matchingSystemUsers)
                    {
                        dbHelper.teamMember.CreateTeamMember(newTeamid, systemUserId, startDate, null);//Link the user to the new team 
                        dbHelper.systemUser.UpdateDefaultTeam(systemUserId, newTeamid);//change the default team

                        var teamMemberId = dbHelper.teamMember.GetTeamMemberByUserAndTeamID(systemUserId, _careDirector_TeamId).FirstOrDefault();
                        dbHelper.teamMember.DeleteTeamMember(teamMemberId); //remove the old team from the user
                    }
                }

            }
        }

        [Description("Populate the database with person records (Males)")]
        //[TestMethod]
        public void LoadTestScript_CreatePersonRecords_Script01()
        {
            var titles = new List<string> { "Mr", "Sir", "Dr", "Professor" };
            var maleFirstNames = new List<string> { "James", "Robert", "John", "Michael", "William", "David", "Richard", "Joseph", "Thomas", "Charles", "Christopher", "Daniel", "Matthew", "Anthony", "Mark", "Donald", "Steven", "Paul", "Andrew", "Joshua", "Kenneth", "Kevin", "Brian", "George", "Edward", "Ronald", "Timothy", "Jason", "Jeffrey", "Ryan", "Jacob", "Gary", "Nicholas", "Eric", "Jonathan", "Stephen", "Larry", "Justin", "Scott", "Brandon", "Benjamin", "Samuel", "Gregory", "Frank", "Alexander", "Raymond", "Patrick", "Jack", "Dennis", "Jerry", "Tyler", "Aaron", "Jose", "Adam", "Henry", "Nathan", "Douglas", "Zachary", "Peter", "Kyle", "Walter", "Ethan", "Jeremy", "Harold", "Keith", "Christian", "Roger", "Noah", "Gerald", "Carl", "Terry", "Sean", "Austin", "Arthur", "Lawrence", "Jesse", "Dylan", "Bryan", "Joe", "Jordan", "Billy", "Bruce", "Albert", "Willie", "Gabriel", "Logan", "Alan", "Juan", "Wayne", "Roy", "Ralph", "Randy", "Eugene", "Vincent", "Russell", "Elijah", "Louis", "Bobby", "Philip", "Johnny" };
            var lastNames = new List<string> { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez", "Hernandez", "Lopez", "Gonzales", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin", "Lee", "Perez", "Thompson", "White", "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson", "Walker", "Young", "Allen", "King", "Wright", "Scott", "Torres", "Nguyen", "Hill", "Flores", "Green", "Adams", "Nelson", "Baker", "Hall", "Rivera", "Campbell", "Mitchell", "Carter", "Roberts", "Gomez", "Phillips", "Evans", "Turner", "Diaz", "Parker", "Cruz", "Edwards", "Collins", "Reyes", "Stewart", "Morris", "Morales", "Murphy", "Cook", "Rogers", "Gutierrez", "Ortiz", "Morgan", "Cooper", "Peterson", "Bailey", "Reed", "Kelly", "Howard", "Ramos", "Kim", "Cox", "Ward", "Richardson", "Watson", "Brooks", "Chavez", "Wood", "James", "Bennet", "Gray", "Mendoza", "Ruiz", "Hughes", "Price", "Alvarez", "Castillo", "Sanders", "Patel", "Myers", "Long", "Ross", "Foster", "Jimenez" };
            var minDateOfBirth = new DateTime(1940, 1, 1);
            var maxDateOfBirth = new DateTime(2002, 12, 31);
            var Ethnicities = new List<Guid> { _ethnicity1Id, _ethnicity2Id, _ethnicity3Id, _ethnicity4Id };
            var genders = new List<int> { 1 };

            for (int i = 0; i <= 1000; i++)
            {
                Console.WriteLine("Cicle --> " + i);
                dbHelper.person.CreateMultiplePersonRecords(100, titles, maleFirstNames, lastNames, minDateOfBirth, maxDateOfBirth, Ethnicities, _careDirector_TeamId, 1, genders);
                dbHelper = new DBHelper.DatabaseHelper();
            }
        }

        [Description("Populate the database with person records (Famales)")]
        //[TestMethod]
        public void LoadTestScript_CreatePersonRecords_Script02()
        {
            var titles = new List<string> { "Ms", "Mrs", "Miss", "Dr", "Professor" };
            var maleFirstNames = new List<string> { "James", "Robert", "John", "Michael", "William", "David", "Richard", "Joseph", "Thomas", "Charles", "Christopher", "Daniel", "Matthew", "Anthony", "Mark", "Donald", "Steven", "Paul", "Andrew", "Joshua", "Kenneth", "Kevin", "Brian", "George", "Edward", "Ronald", "Timothy", "Jason", "Jeffrey", "Ryan", "Jacob", "Gary", "Nicholas", "Eric", "Jonathan", "Stephen", "Larry", "Justin", "Scott", "Brandon", "Benjamin", "Samuel", "Gregory", "Frank", "Alexander", "Raymond", "Patrick", "Jack", "Dennis", "Jerry", "Tyler", "Aaron", "Jose", "Adam", "Henry", "Nathan", "Douglas", "Zachary", "Peter", "Kyle", "Walter", "Ethan", "Jeremy", "Harold", "Keith", "Christian", "Roger", "Noah", "Gerald", "Carl", "Terry", "Sean", "Austin", "Arthur", "Lawrence", "Jesse", "Dylan", "Bryan", "Joe", "Jordan", "Billy", "Bruce", "Albert", "Willie", "Gabriel", "Logan", "Alan", "Juan", "Wayne", "Roy", "Ralph", "Randy", "Eugene", "Vincent", "Russell", "Elijah", "Louis", "Bobby", "Philip", "Johnny" };
            var lastNames = new List<string> { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez", "Hernandez", "Lopez", "Gonzales", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin", "Lee", "Perez", "Thompson", "White", "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson", "Walker", "Young", "Allen", "King", "Wright", "Scott", "Torres", "Nguyen", "Hill", "Flores", "Green", "Adams", "Nelson", "Baker", "Hall", "Rivera", "Campbell", "Mitchell", "Carter", "Roberts", "Gomez", "Phillips", "Evans", "Turner", "Diaz", "Parker", "Cruz", "Edwards", "Collins", "Reyes", "Stewart", "Morris", "Morales", "Murphy", "Cook", "Rogers", "Gutierrez", "Ortiz", "Morgan", "Cooper", "Peterson", "Bailey", "Reed", "Kelly", "Howard", "Ramos", "Kim", "Cox", "Ward", "Richardson", "Watson", "Brooks", "Chavez", "Wood", "James", "Bennet", "Gray", "Mendoza", "Ruiz", "Hughes", "Price", "Alvarez", "Castillo", "Sanders", "Patel", "Myers", "Long", "Ross", "Foster", "Jimenez" };
            var minDateOfBirth = new DateTime(1940, 1, 1);
            var maxDateOfBirth = new DateTime(2002, 12, 31);
            var Ethnicities = new List<Guid> { _ethnicity1Id, _ethnicity2Id, _ethnicity3Id, _ethnicity4Id };
            var genders = new List<int> { 2 };

            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine("Cicle --> " + i);
                dbHelper.person.CreateMultiplePersonRecords(100, titles, maleFirstNames, lastNames, minDateOfBirth, maxDateOfBirth, Ethnicities, _careDirector_TeamId, 1, genders);
                dbHelper = new DBHelper.DatabaseHelper();
            }
        }

        [Description("Populate the database with person case notes, phone calls, tasks and appointments")]
        //[TestMethod]
        public void LoadTestScript_CreatePersonActivitiesRecords_Script01()
        {
            var textLists = new List<string> {
                "Knight Rider, a shadowy flight into the dangerous world of a man who does not exist.",
                "Michael Knight, a young loner on a crusade to champion the cause of the innocent, the helpless in a world of criminals who operate above the law.",
                "Barnaby The Bears my name, never call me Jack or James, I will sing my way to fame, Barnaby the Bears my name.",
                "Birds taught me to sing, when they took me to their king, first I had to fly, in the sky so high so high, so high so high so high.",
                "Treacle pudding, fish and chips, fizzy drinks and liquorice, flowers, rivers, sand and sea, snowflakes and the stars are free.",
                "This is my boss, Jonathan Hart, a self-made millionaire, hes quite a guy.",
                "This is Mrs H., shes gorgeous, shes one lady who knows how to take care of herself.",
                "By the way, my name is Max.",
                "I take care of both of them, which aint easy, cause when they met it was MURDER! ",
                "One for all and all for one, Muskehounds are always ready.",
                "One for all and all for one, helping everybody.",
                "One for all and all for one, its a pretty story.",
                "Sharing everything with fun, thats the way to be.",
                "One for all and all for one, Muskehounds are always ready.",
                "One for all and all for one, can sound pretty corny.",
                "If youve got a problem chum, think how it could be.",
                "Knight Rider, a shadowy flight into the dangerous world of a man who does not exist.",
                "Michael Knight, a young loner on a crusade to champion the cause of the innocent, the helpless in a world of criminals who operate above the law.",
                "I never spend much time in school but I taught ladies plenty.",
                "Its true I hire my body out for pay, hey hey.",
                "Ive gotten burned over Cheryl Tiegs, blown up for Raquel Welch.",
                "But when I end up in the hay its only hay, hey hey.",
                "I might jump an open drawbridge, or Tarzan from a vine.",
                "Cause Im the unknown stuntman that makes Eastwood look so fine.",
                "Top Cat! The most effectual Top Cat! Whos intellectual close friends get to call him T.C., providing its with dignity.",
                "Top Cat! The indisputable leader of the gang.",
                "Hes the boss, hes a pip, hes the championship.",
                "Hes the most tip top, Top Cat.",
                "Mutley, you snickering, floppy eared hound.",
                "When courage is needed, youre never around.",
                "Those medals you wear on your moth-eaten chest should be there for bungling at which you are best.",
                "So, stop that pigeon, stop that pigeon, stop that pigeon, stop that pigeon, stop that pigeon, stop that pigeon, stop that pigeon.",
                "Howwww! Nab him, jab him, tab him, grab him, stop that pigeon now." };


            var matchingPersonRecords = dbHelper.person.GetByPreferredName("LoadDataScript ");
            var systemUsers = dbHelper.systemUser.GetSystemUserByUserNamePrefix("performance_user_");

            for (int i = 0; i < 2000; i++)
            {
                Console.WriteLine("Cicle --> " + i);

                var startdate = commonMethodsHelper.GenerateRandomDate(new DateTime(2020, 1, 1), DateTime.Now.Date);
                var startTime = new TimeSpan(10, 0, 0);
                var endTime = new TimeSpan(10, 5, 0);
                var statusid = 4; //Scheduled
                var showTimeAsId = 5; //Busy

                try
                {
                    dbHelper.personCaseNote.CreateMultipleCaseNoteRecords(20, _careDirector_TeamId, textLists, textLists, matchingPersonRecords, new DateTime(2020, 1, 1), DateTime.Now.Date, systemUsers);
                    dbHelper = new DBHelper.DatabaseHelper();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                try
                {
                    startdate = commonMethodsHelper.GenerateRandomDate(new DateTime(2020, 1, 1), DateTime.Now.Date);
                    dbHelper.phoneCall.CreateMultiplePersonPhoneCallRecords(20, textLists, textLists, matchingPersonRecords, "123321123", _careDirector_TeamId, systemUsers, startdate);
                    dbHelper = new DBHelper.DatabaseHelper();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                try
                {
                    startdate = commonMethodsHelper.GenerateRandomDate(new DateTime(2020, 1, 1), DateTime.Now.Date);
                    dbHelper.task.CreateMultiplePersonTasks(20, matchingPersonRecords, textLists, textLists, _careDirector_TeamId, startdate, systemUsers);
                    dbHelper = new DBHelper.DatabaseHelper();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                try
                {
                    startdate = commonMethodsHelper.GenerateRandomDate(new DateTime(2020, 1, 1), DateTime.Now.Date);
                    dbHelper.appointment.CreateMultiplePersonAppointments(20, _careDirector_TeamId, matchingPersonRecords, systemUsers, textLists, textLists, textLists, startdate, startTime, startdate, endTime, statusid, showTimeAsId);
                    dbHelper = new DBHelper.DatabaseHelper();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                try
                {
                    startdate = commonMethodsHelper.GenerateRandomDate(new DateTime(2020, 1, 1), DateTime.Now.Date);
                    dbHelper.email.CreateMultiplePersonEmails(20, _careDirector_TeamId, matchingPersonRecords, systemUsers, textLists, 1);
                    dbHelper = new DBHelper.DatabaseHelper();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        [Description("Populate the database with person attachments")]
        //[TestMethod]
        public void LoadTestScript_CreatePersonAttachmentRecords_Script01()
        {
            var matchingPersonRecords = dbHelper.person.GetByPreferredName("LoadDataScript ");

            int totalRecordsToCreate = matchingPersonRecords.Count > 1000 ? 1000 : matchingPersonRecords.Count;

            for (int i = 0; i < totalRecordsToCreate; i++)
            {
                Console.WriteLine("Cicle --> " + i);
                try
                {
                    dbHelper.personAttachment.CreateMultipleAttachmentRecords(20, _careDirector_TeamId, matchingPersonRecords[i], "Load Test Script For Person Attachment", DateTime.Now.Date, _attachDocumentTypeId, _attachDocumentSubTypeId, "c:\\temp\\Doc2ToUpload.txt");

                    dbHelper = new DBHelper.DatabaseHelper();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        [Description("Populate the database with test user accounts")]
        //[TestMethod]
        public void LoadTestScript_CreateSystemUserRecords_Script01()
        {
            var userNamePrefix = "performance_user_";
            var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
            var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Systemuser - Secure Fields (Edit)").First();

            for (int i = 1001; i <= 1010; i++)
            {
                var systemUserExists = dbHelper.systemUser.GetSystemUserByUserName(userNamePrefix + i).Any();

                if (!systemUserExists)
                {
                    var userid = dbHelper.systemUser.CreateSystemUser(userNamePrefix + i, "Performance User", i.ToString(), "Performance User " + i, "Passw0rd_!", userNamePrefix + i + "@somemail.com", userNamePrefix + i + "@secureemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirector_BusinessUnitId, _careDirector_TeamId, DateTime.Now.Date);

                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(userid, systemAdministratorSecurityProfileId);
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(userid, systemUserSecureFieldsSecurityProfileId);

                    var systemUserApplicationId = dbHelper.userApplication.GetBySystemUserAndApplicationId(userid, _applicationId).FirstOrDefault();
                    dbHelper.userApplication.UpdateUserApplication(systemUserApplicationId, _homeScreenId);
                }

            }
        }

        [Description("Populate the database with test user accounts")]
        //[TestMethod]
        public void LoadTestScript_LinkHomeScreensToUsers_Script01()
        {
            var userNamePrefix = "performance_user_";

            for (int i = 1; i <= 1002; i++)
            {
                var systemUserExists = dbHelper.systemUser.GetSystemUserByUserName(userNamePrefix + i).Any();

                if (systemUserExists)
                {
                    var userid = dbHelper.systemUser.GetSystemUserByUserName(userNamePrefix + i).FirstOrDefault();
                    var systemUserApplicationId = dbHelper.userApplication.GetBySystemUserAndApplicationId(userid, _applicationId).FirstOrDefault();
                    dbHelper.userApplication.UpdateUserApplication(systemUserApplicationId, _homeScreenId);
                }

            }
        }

        [Description("Populate the database with Service Element 1 records where Who To Pay = 'Provider'")]
        //[TestMethod]
        public void LoadTestScript_CreateServiceElement1_Script01()
        {
            //List<string> ServiceElement1Names = new List<string> { "Test SE 1", "Test Data 02", "Sample Data test", "Test SE3", "Test Care - Service Element 1", "Test Poll 1", "Test QA", "Mission QA", "Fin Invoice SE 1", "Service Provision Test Person - SE1", "IS - SE1 Test", "QA Test - SE1", "Element 1", "Work Station - Provider - SE1 - Hrs(Quater)", "Work Station - Provider - SE1 - Units(Parts)", "Work Station - Provider - SE1 - One Off", "ML_SE_1 Planned", "ML_SE_1_Actual", "Work Station - Provider - SE1 - Hrs(Quater) - Rate periods", "E 1 Test", "SE 1 - Call off Budget", "TF - SP - Provider - SE1", "Final SE1 - Provider - Units", "Test Service Element", "QA - SE", "Home Care", "Test_05", "Adult Residential", "Secure Accomodation", "Work Test SE1", "Lem-Test-SE1", "Test Element1", "Whole Units", "Residential Care (Adults)", "Nursing Care (Adults)", "Residential Care (Older)", "Nursing Care (Older)", "Reg_SE1", "Last", "Nov_Per 1 Hour", "Dec_test_CNK", "QA - SE 1 - Person", "Jan_rel_block", "Acknldg 1", "1 SE", "Jan_SE_adj", "SE1_Test_FPT", "SE1_Test SC", "SE_Jan_09", "SE2_Test_FPT", "Inv Element 1", "Serv_Elem1", "Serv_Elem_1_Contrib", "Test SE1_Provider", "Adjust days_SE1", "Inv SE 1", "Element Inv 1", "Element Invoice 1", "QA-123", "NSE_1", "Ele 1", "1 SE", "Test Ele 1", "Feb_Provider_01", "Feb_SE1_person", "uniq1SE", "Element 1 - Test", "Element1 - Test1", "Element1 - Invoice", "Test Provider_20", "Invoice Element - 1", "Invoice Ele -1", "Invoice S1", "Mar Element 1", "Test_Provider_Inactive", "March Element 1", "Element 16", "mar17", "Element 20", "Element 23", "Element 24", "Element 25", "Test Element 25", "Element - 25", "Element 26", "Element 27", "Element 28", "Element - 30", "Test_SE1", "SE1_Apr20", "Element Apr 01", "Planned Home Care", "Test SE1", "Apr 29 Ele", "Sedebtors_6520", "FUCSE1_6520", "Ele Prov 11", "Ele", "SE 1 Prov 11", "Ele 12 May", "Debt_SE1-may12", "SE1sup", "Ele 13 May", "IxiloSE1", "Ele 14 May", "Ele 19", "May210520SE1", "Advanced_SE1", "Ad_SE1", "Test_003", "Ele 27", "SE1_gruoup", "Ele 28 May", "Test_Carer_Mar12", "28maySE1_SP", "May28_SE1", "June1_SE1", "june4_SE1", "J8_SE1", "J9SE1", "j10", "11junSE1supp", "11suppjun20", "J11_SE1", "12junSE1SP", "14jun2020", "J20_S1", "Ele 23 Jun", "23jun20", "23SEjun20", "Check test", "Ele 24 Jun", "24junSE", "J24_SE1", "25620junSE", "j25620SE", "immed", "Service25jun", "29jun20", "02julDeb", "02DebSP", "03juldeb", "jul05_SE1", "j06_se1", "j07_Se1", "j08_SE1", "08julsd", "080720DSP", "09jul20_SE1", "j09_SE1", "10SE1_20", "Ele 14 Jul", "Ele 21 Jul", "21SPT0720", "J22_SE1", "23720julSE1", "Ele 23 Jul", "Ele 24 Jul", "27720se1", "Direct Payment", "Confinement", "J29_SE1", "Ele 30 Jul", "A03_S1", "Ele 5 Aug", "6820SE1", "Ele 6 Aug", "6820xmlse1", "SE138201", "Ele 14 Aug", "Ele 17 Aug", "28Aug_SE1", "Ele 31 Aug", "1920SE1", "11sep_SE1", "14test_SE1update", "15Sep_SE1", "GraphnetElement1", "SE1_Nov", "SE1_Regression", "For Demo SE1", "Residential Care", "TEST_1020", "Nursing Care", "Mala Service element 1", "Test FA", "Test FA 31", "Test SDV", "Service 8 Apr 1", "SDV 1", "Test Supplier Pmts", "Test Debtors", "Test SP 20", "Test SE 21", "21 Apr SE 1", "Test Suppliers 21", "Test SE 22", "Test Supplier pmts 22", "Test Sup 22 (SE1)", "Test Supp 22 - SE1", "Test Debs - SE 1", "Suppliers 23 - SE1", "Test_Provider", "SE1_Mala_Test", "Test Ad hoc - SE1", "Ad hoc test -SE 1", "27 Ad hoc test -SE1", "28 Ad hoc test- Debs - SE1", "Ad hoc test - Debtors 28 - SE1", "Deb hoc provider - SE1", "28 Debts-SE1", "DT 28-SE1", "Test Charges 29 -SE1", "FA Charges 29-SE1", "FA 29 - SE1", "FA 30 -SE1", "SE1-Demo8734", "DB 04-SE1", "Test DB 04-SE1", "Test FA 04 - SE1", "Test C4S - SE1", "Test SP 05 - SE1", "Test SP 06-SE1", "Test FA Cancel -SE1", "Test Rate -SE1", "Equipment 123", "Equipment 789", "12 May SP1", "Test  SE 17 - SE1", "Test TT - SE1", "Test Extract 18-SE1", "Supplier Extract 18-SE1", "Supplier Extract 2 - SE 1", "Deb 19-I-SE1", "Test_SE 1 Prov 1", "Test SDV 24-SE1", "Test_SE1 Prov S1", "Test SDV 25", "SDV 25 -I-SE1", "SDV 25-II-SE1", "Test FT 25-SE1", "Explore Application", "Test Suppliers 27 - I-SE1", "Test Debtors - 27-SE1", "Rate Schedule 27 -II", "Test_Debt_SE_1_S1", "Test Charges 27 -SE1", "CFS 27-SE1", "Test Suppliers May - SE1", "Test Debtors May -SE1", "Test Debtor May -I-SE1", "Test Deb 2888-SE1", "Service E 1", "Test_supplier_SE_S1", "Test Utility - SE1", "Test_Supplier_SE1_S3", "Primary", "Test Utility June", "Test Utility 04", "Test june7 - SE1", "Test_FIN_Prov_SE1_S4", "Test Supplier Jun 8 - SE1", "Test Debtors Jun 8 -SE1", "AR001", "Mala 08 21", "Test Deb 08", "New Provider SE", "S001pro", "KT", "s001E", "Pearson Spector", "Test_Prov_SE1_S4", "Test_FIN_Prov_SE1_S5", "test_fin_prov_SE1_s6", "Test_fin_carer_SE1_S5", "Service 11june_1", "Test_j08_se1", "service 11fun_1", "SDVR", "14June_1", "Test_s6_ j08_se1", "JanOneSix", "Test Extract 15 - SE1", "Test_June_06_SE1_S10", "Test Supplier Jun 16 -SE1", "Test Deb 16 -SE1", "Brokerage SE1", "test_June_SE1_1606", "Test__Provider_June_SE1_1606_02", "Test_Provider_June_SE1_1606_03", "Mala Service element 1", "Test Tariff - SE1", "Test Tariff -I- SE1", "Test_Provider_June_SE1_1706", "Test_Provider_BR__SE1_1706", "SE001pro", "test_ProvSE1_BR_1706_02", "01Jun21-SE1", "test_ProvSE1_1706_03", "Test_Deb_SE1_1706_04", "SE1 Test Data", "Test_Prov_SE1_1706_05", "SE001debtor", "SE001 provider", "Debtor-SE1", "SE1 carer", "test_Prov_SP_SE1_1706_05", "Debtor (JanOneSix)", "Test_Prov_BR_SE1_1806_01", "Test_SE1_InterestTrans_1806_01", "June18_SP1", "DT (JanOneSix)", "Test_SE1_InterestCalc_1806_02", "test_SE1_Interestcalc_1806_03", "Test_SE1_2106", "Home Care Services", "Test Deferred Payments 21 -SE1", "Test Transaction -SE1", "Test IC - II", "Test IC 22 - I", "KT June 22", "Test_SVS_SE1_debt_2206", "Test Block - SE1", "GLcode-SE1", "Test FT 22 - SE1", "test_2306", "test_2306_01", "tes_2306_02", "FT Test", "Test_SVS_SE1_Supplierpayments_2806", "Test_SVS_SE1_DebtorPayments_2906", "SE DTemplate", "June30SE1_SP_FinExtracts", "IT001", "SE002", "Test_SVS_SE1_SupplierPayment_0507", "Test_SVS_SE1_SupplierPayments_0507_02", "Test_SVS_SE1_DebtorPayments_6.2.2.3", "test01", "Test_SE1_SVS_DP_6.2.2.3_02", "Test_SVS_SE1_DebtorPayments_6.2.2.3_03", "Test_SVS_SE1_DebtorPayments_6.2.2.3_04", "Test_SVS_SE1_DebtorPayments_6.2.2.3_04New", "Test_SVS_SE1Deb_6.2.2.3_05", "Regression 6.2.2.3", "July08SE1-Task 1", "July08SE1-Task SP", "Test_SE1_SVS_6.2.2.3_0807", "Test Extract Tool -Suppliers - SE1", "Ext No = Capacity Check - SE1", "Nach", "Test_SVS_SE1_0907", "Extract SP-July 09SE1", "Test_SVS_SE1Deb_0907_01", "Test_SVS_SE1_SupplierPayments_1207", "Test_SVS_SE1_DebPayments_1207", "service1", "service2", "serviceelement", "July14SE1_New Extract SP£", "Test_SVS_SE1_IT_1607_02", "Test Service Provided -I", "TEST_SVS_SE1_SUPPLIERPAYMENTS_1907", "Home Carer Plans", "Test Extract 26 - SE1", "July26SE1 Extract Supplier", "Test Supp 26 - SE1", "Test EF - SE1", "Aug 03 Extract Supplier-SE1", "Aug03 SC-2 Extract Supplier-SE1", "Test Trigg - SE1", "Retest Trigg - SE1", "Test RA Invoice - SE1", "Test RA invoice 04 - SE1", "Test RA Inv -SE1", "Test TD - SE1", "Test Temp 1 - SE1", "Test Uprate - SE1", "Check10", "test10", "PC01", "Test FA 16", "pc3", "pc09", "Test CR1 17- II", "Aug 18 Supplier Extract SE1", "Aug 18 Deptor Extract SE1", "pc11 supplier", "SD1", "Test ED - SE1", "Aug 23 Supplier Extracts SE1", "01Jun21-SE1", "Aug 26 SE1 - CDV6 12122", "sp check 26", "test26 supplier", "Test DB 26 -SE1", "Test Portal - SE1", "COS1", "CPW", "Aug 31 CDV6-12055", "Test 12221 -I -SE1", "Test 12221 -II-SE1", "SE 1st Sep Provider", "Sep 01 Supplier Transactions SE1", "SD01", "Residential - Invoice By", "Test Portal Sep -SE1", "Sep 07 Supplier Trans SE1", "OnlySE1", "BOTHSE1andSE2", "Check123", "Testing99", "Test SDV 17", "SE1Person", "Sep 24 Supplier Transactions SE1", "Test Uprates - SE1", "28 Sep Supplier SE1", "Sep 28 DebtorT SE1", "TestGLcode", "Test Uprates 26 -SE1", "Test Sep 28 SE1", "Reg1", "Test FA_Reg 29 - SE1", "pp yes", "pp no", "issue1", "PcBlock1", "Pptblock1", "Pptcblock1", "Pc-spot1", "Ppt-spot1", "Pptc-spot1", "PcBlock2", "Pptblock2", "Pptcblock2", "Pc-spot2", "Ppt-spot2", "Pptc-spot2", "Test SE1 with mapping", "KTAutomationSE1", "TEST_SVS_SE1_1510", "test_SVS_1510_01", "Oct 15 FA-SP SE1", "Oct 27 Debtor with SP SE1", "Test FA 27 SPO", "Test NRPRS", "Test CPW - SE1", "Test NRPRS - SE1", "Test NRPRS 09 - SE1", "Nov 10 Supplier Extracts SE1", "Nov 10 Fees Extracts_Wrong", "Nov 10 DE SE1", "Nov 12 SE1", "Test Uprated Nov - SE1", "Test CBC - SE1", "Test CBC 1- SE1", "Test CBC 2- SE1", "Test CBC 3- SE1", "Test CBC 4- SE1", "Test CBC 5- SE1", "Test CBC-II -SE1", "Test CBC - III-SE1", "Test CDV6-13650-SE1", "Test CDV6-13650(Unit Divisor=Null) -SE1", "Test GL Code Update -SE1", "Test GL COde- fee", "Test Provider SE1", "john", "Barak", "bharath", "BarakSe1", "subrag", "chrishse1", "Michal", "Test FA Dec - SE1", "SE22- yes", "SE22-NO", "Test CDV6-13568 SE1", "Test CDV6-13568(Unit Divisor=Null) - SE1", "Test Transaction 23 -SE1", "Test CFT -SE1", "parker" };
            //var startDate = new DateTime(2020, 1, 1);
            //int whoToPay = 1; //Provider

            //foreach (var serviceElement1Name in ServiceElement1Names)
            //{
            //    var recordExists = dbHelper.serviceElement1.GetByName(serviceElement1Name).Any();
            //    if (!recordExists)
            //        dbHelper.serviceElement1.CreateServiceElement1(_careDirector_TeamId, _careDirector_BusinessUnitId, serviceElement1Name, startDate, whoToPay, Guid.Empty);
            //}

        }

        [Description("Populate the database with Service Element 1 records where Who To Pay = 'Carer'")]
        //[TestMethod]
        public void LoadTestScript_CreateServiceElement1_Script02()
        {
            //List<string> ServiceElement1Names = new List<string> { "Test Data 01", "Test Data_01", "Test Data_02", "SE2", "Test SE2", "Test Caught", "PLM", "Funct Test - Carer", "QA Test Carer", "Test Data_03", "Test Data - Carer", "Carer Ex Test", "Element Ca", "Work Station - Carer - SE1 - E&E Rules - No", "Work Station - Carer - SE1 - E&E Rules - Yes", "Carer Test", "Carer 111", "Test", "Foster Care", "ReTest - Fin", "SP Allowance - carer", "Sum Service", "QA Test - Allowance", "Test Calci A", "Test Data - E&E", "Fin Test - Carer", "Removed Data - SE1", "December Release - SE1", "FosterCare_POF5_POFA", "Stat Data - SE1", "Test Carer_Feb", "Test_Carer_Mar", "Test_Carer_Mar11", "Test_Carer_Mar12", "Test_GL_Sector", "Test_GL_Provider", "Test_GL_Fee", "Carer 234", "Apr 24 Element", "SE_Carer_APR28", "ASGLB SE 1", "QA Fin SE-1", "May4SE-Carer", "Carer Ele 5", "Allownacemay5", "Carer 5", "Ele 6 May", "Element Carer 7 May", "Test_April_Carer", "Element 8 May", "Ele Car 11", "Ele 1 Car 11", "May11ins", "vinaybala_Element1", "May12-SE1Carer", "SE1_Carer-may", "Advanced12SE1", "SE13520", "Test_Care_SA_01", "Carer Ele 14 May", "ixilocarer", "AAservice-001", "Element 18 May", "FeeSE1_18520", "Finance Team - Service Element 1 Data", "Service Element 1 - Finance Team", "Carer Ele 19", "AAService-002", "May200520_SE1", "Ele 21 May", "may21520", "22mayfee1", "Ele 22 May", "260520_Feecontra", "AAService-003", "27may05SE1", "Ele 27 May", "AAService-004", "260520_Feecontra", "AAService-005", "Ele 29 May", "29may20", "Test_ChildBU_Carer", "AAService-006", "Ele 1 Jun", "AAService-007", "1 Jun Ele", "Test_SE1_Note1", "01junSE1", "AAService-008", "June Ele", "AAService-009", "02jun20", "03jun20SE1", "AAService-010", "AAService-011", "04jun20SE1", "080620SE1", "090620se1", "1allowance09620", "AAService-012", "AAService-013", "10jun260", "Carer_Example12", "Carer_R12", "AAService-014", "AAService-015", "Test_Auth_Carer", "01_extract_SE", "Extract SE - 1", "Carer Extract", "AAService-016", "1906 - SE - Extract", "Ele 19 Jun", "AAService-017", "Test_Carer_12c", "AAService-018", "AAService_Advance", "Test_Code2", "Test Code3", "Test_Code4", "Test_Carer_12d", "03julcarer", "03carerjul", "03Feejul", "fee3july", "0807Carerxml", "0807carCSV", "2107SE1", "21Fee0720", "Ele 23 Carer", "24720carerSE1", "24SE1jul20", "24testfee", "Carer277SE1", "Carer Ele 27", "28720Carer", "Carer 30 Jul", "6820csvCSE1", "6820csvSE1carer", "68rlsSE1", "GraphnetElement2", "Mala service element 1", "Ele 20 Jan", "Carer Ele 20 Jan", "BU Ele 1", "Ele 21 Jan", "Ele", "Ele new BU", "Test SP March", "Service 10", "Mala carer SE1", "Ele 11 Mar", "SE 1 16 Mar", "Ele 17 Mar", "Ele 18 Mar", "Ele 18 Sam", "Ele 19 Mar", "Ele 19 Sam", "Test FA contribution", "Service 8 Apr 11", "Test Carer pmts", "Test Carer 16", "Test Carer 20", "21 Car SE 1", "Test Carer 21", "Test draft", "Test Carer 23 - SE1", "Test CP-SE1", "Test CPM -SE1", "Test Allowance - SE1", "Test FEE 06-SE1", "Equipment FCP 789", "Equipment FCP 123", "12 May SE1 Care", "13 may carer", "Carer extract 19-SE1", "Test_SE 1 Carer 1", "TEST_SE 1 Carer S1", "Test Carer 27-SE1", "Test Carer May-SE1", "Will Turner", "Pepper Potters", "Service_Ele 1", "Ser_Ty 1", "Test_Carer_SE_S1", "Test_Carer_SE_S2", "KT Day2&3", "Primary Sevice", "Test_FIN_Carer_SE1_S4", "Sample_Service E 1", "Carer Primary Service", "testcp", "S001carer", "s001care$", "Test Exemption", "testtesttest", "Test Carer 16 June -SE1", "Test__Carer_June_SE1_1606_02", "Test_Carer_June_SE1_1606_03", "SE001 carer", "Carer (JanOneSix)", "Test_Carer_SE1_1806", "June18_C_A_1", "June18_C_Fee_1", "June29SE1_FI_Allow", "June29SE1_FI_C-Allow", "Test APC - SE1", "Test_SVS_SE1_CarerPayments_2901", "Test_SVS_Fee_SE1_CarerPAyments_2906", "Test APC II", "Test_SVS_Fee_SE1_CarerPayments3006", "Test Jun 30 - SE1", "APC 30 -SE1", "Test E & E -SE1", "July01SE1_FinExt_Allow_Wrong", "July01SE1_FinExt_Allow", "E & E = Yes - SE1", "Exemption = Yes", "Ex = Yes", "EX = NO", "EX 'YES'", "Test Ext - SE1", "Test Extension = Yes - SE1", "Test Extension = No+'Age'", "2 07 care", "Test Extension = Yes+Age", "Test Extension = No+Gender", "Test Extension = Yes+Gender", "Test E 'Yes'", "Test E 'No'", "Test_SVS_SE1Carer_0507", "Test Extension July = Yes+Age - SE1", "Test Extension July = Yes+Gender - SE1", "SE002 carer", "Test Allowance 07", "allowance001", "July08SE-Extract CarerAllow", "Ext No = Capacity Check - SE1", "Ext No = Age Check -SE1", "Ext No = Gender", "Ext Yes = Capacity Check", "Ext Yes = Age", "Ext Yes = Gender", "Service001 carer", "Test Carer Extract - SE1", "Test_SVS_SE1_0907_01", "Test_SVS_SE1_Carer_0907", "Carer Extract1", "Test_SVS_SE1_CarerPayments_1207", "Test_SVS_SE1_CarerPayments_1307", "Test_SVS_SE1_Fee_CarerPayments_1307", "Test_SVS_Fee_SE1_CarerPayments_1307_02", "Test_SVS_Fee_CarerPayments_1307_03", "Test AT-SE1", "testservice", "Test AT 2 £- SE1", "Test AT 14 - SE1", "test1234", "Test Allowance 19", "Test Allowance 19 - II", "Test AC -I", "Test Birthday", "Test Allow - I", "Test Special", "Test Christmas", "Test Approved-CT", "Test Approved CT - I", "service_allowance1", "TTService", "OFSTED 2021 Element one", "Test Approved CT-I(End Date)", "Retest Birthday - SE1", "Retest Christmas - SE1", "Retest 11581 - SE1", "July 29 Rule Yes", "Aug 03 Carer Allowance - SE", "Aug 03 Carer Fees - SE", "pc02", "pc05 carer", "pc06 carer", "pc07", "pc08", "FFCarer", "Test CAD-I", "Aug 18 Carer_Allowance Extract SE1", "Aug 18 Carer_Fees Extract SE1", "Pc10", "Test Pro - 18 - SE1", "Test 11202 - SE1", "Test Allowance Transc", "PC11 carer", "Test CAD 20", "AC01", "CT01-N0", "CT02 yes", "Testd", "ACT01", "Retest", "test26", "Ofsted Service Element one with Care Types", "Sep 28 Carer Allowance SE1", "Sep 28 Carer Fess SE1", "testglcode", "REG yes", "reg no", "test12333", "test", "Nov 10 Allowance Extracts", "Nov 10 Fees Extracts", "Test payee -SE1", "Test GL Code- FEES", "DEC02", "DEC-02", "DEC03", "Nov3", "jan01" };
            //var startDate = new DateTime(2020, 1, 1);
            //int whoToPay = 2; //Carer

            //foreach (var serviceElement1Name in ServiceElement1Names)
            //{
            //    var recordExists = dbHelper.serviceElement1.GetByName(serviceElement1Name).Any();
            //    if (!recordExists)
            //        dbHelper.serviceElement1.CreateServiceElement1(_careDirector_TeamId, _careDirector_BusinessUnitId, serviceElement1Name, startDate, whoToPay, Guid.Empty);
            //}

        }

        [Description("Populate the database with Service Element 1 records where Who To Pay = 'Person'")]
        //[TestMethod]
        public void LoadTestScript_CreateServiceElement1_Script03()
        {
            //List<string> ServiceElement1Names = new List<string> { "Test Service Element 1", "Work Station - Person - SE1 - Units(Parts) - Invoice", "Work Station - Person - SE1 - Hours(Quater) - No Payments", "Work Station - Person - SE1 - One Off - Schedule", "Service 1", "Test SE 1 Person", "QA Tag", "Jan_Release", "Test SE1-Person1", "Feb_Test SE", "Test_SC1", "QAZ", "Person_SE1_FFY", "SE_Feb_per", "Test Person_20", "Test_SE1_Person2", "Test_SE1_Person3", "Test_Person_Mar", "Test_SC_Person", "Serv_Mar_17", "Test_Demo_Fin", "14test_SE1", "ser  test element", "Per_Person1", "Mala Person SE1", "Test March 11", "Service 8 Apr 111", "21 Apr Per 1", "Test Rate Req - SE1", "Test_SE 1 Person", "Test_SE 1 Person S1", "Test_SE_S1", "Rate Schedule 27 -I", "Test_FIN_Person_SE1_S4", "Test data SE1_person", "SE check 1", "s001person", "Test_Person_June_SE1_1606_02", "Test_Person_June_SE1_1606_03", "18June_1", "Test Uprates", "June25_SE1", "June30SE1_Deb_FinExtarct", "JULY02SE1_DAY 14", "July07DeptorExtract_SE1_Wrong", "July07SE1DeptorExtract", "July08SE1-Extract Debtor", "Test_SE1_Person_0907", "July14SE1_Wrong Extract SP£", "Aug 03 Debtor - SE1", "SE001 person", "SE person001", "Test Uprate (P) - SE1", "Aug 12 SE1", "pc04", "Sep 09-8369", "SE1Personupdate", "Oct 27 Debtor Extracts SE1", "Nov 11 Uprated SE1", "Test CPW-Person SE1", "SE-Dec27" };
            //var startDate = new DateTime(2020, 1, 1);
            //int whoToPay = 3; //Carer

            //foreach (var serviceElement1Name in ServiceElement1Names)
            //{
            //    var recordExists = dbHelper.serviceElement1.GetByName(serviceElement1Name).Any();
            //    if (!recordExists)
            //        dbHelper.serviceElement1.CreateServiceElement1(_careDirector_TeamId, _careDirector_BusinessUnitId, serviceElement1Name, startDate, whoToPay, Guid.Empty);
            //}

        }

        [Description("Populate the database with Service Element 2 records")]
        //[TestMethod]
        public void LoadTestScript_CreateServiceElement2_Script01()
        {
            List<string> ServiceElement2Names = new List<string> { "01Jun21-SE2", "09jul20_SE2", "11sep_SE2", "12964SE2", "14june_2", "15Sep_SE2", "18June_2", "21 Apr Pr SE 2", "21 Apr SE 2", "28 Debts-SE2", "28 Sep Supplier SE2", "28aug_SE2", "A03_SE2", "Acknlog 2", "AD_SE2", "Adjust date _SE2", "Advanced_SE2", "Advanced12SE2", "allowance001", "AR", "Aug 03 Debtor - SE2", "Aug 03 Extract Supplier-SE2", "Aug 12 SE2", "Aug 18 Deptor Extract  SE2", "Aug 18 Supplier Extract SE2", "Aug 23 Supplier Extracts SE2", "Aug 26 SE2 - CDV6 12122", "Aug 31 CDV6 - 12055", "Aug03 SC-2 Extract Supplier-SE2", "barakse2", "bharath2", "brokerage SE2", "Carer (JanOneSix)", "Carer Extract", "Carer Extract 19-SE2", "carer se2", "Cash", "CFS 27-SE2", "Check123", "chrish2", "DB 04-SE2", "Deb 19-I-SE2", "Deb hoc provider - SE2", "Debtor (JanOneSix)", "DEbtor-SE2", "DeC02", "December Release - SE2", "DT (JanOneSix)", "DT 28-SE2", "Explore Application 2", "Ext No = Capacity Check - SE2", "Extra Care", "Extract SP-July 09SE2", "FA 29 - SE2", "FA 30 - SE2", "FA Charges 29-SE2", "Feb 2 SE2", "Feb_SE2_person", "Fin Invoice SE 2", "Final SE2 - Provider - Units", "For Demo SE2", "Free Nursing Care (Fixed)", "GLcode-SE2", "GraphnetFile Element 2", "Inactive", "IT001", "ixlno", "J06_Se2", "J07_Se2", "j08_Se2", "j09_Se2", "j10", "J11SE", "J20_S2", "J22_SE2", "J24_SE2", "J29_SE2", "J8", "j9", "jan2_rel_block", "Jan2_Release", "JanOneSix", "johne2", "Jul05_SE2", "JULY02SE2_DAY 14,", "July07DeptorExtract_SE2_Wrong", "July07SE2DeptorExtract", "July08SE2-Extract Debtor", "July08SE2-Task 1", "July08SE2-Task SP", "July14SE2 Extract SP£", "July14SE2_New Extract SP£", "July26SE2 Extract Supplier", "June1_SE2", "June18_C_A_2", "June18_SP2", "JUne2_SE2", "June25_SE2", "June29SE2_FI_Allow", "June30SE2_Deb_FinExtarct", "June30SE2_SP_FinExtracts", "KT Day 2 and 3", "KT June 22", "KT2", "Long Stay (Fixed)", "Long Stay (Negotiated)", "Long Term Care (Fixed)", "Long Term Care (Negotiated)", "Mala 08 212", "Mala Service element 2", "Mar17 se2", "May210520SE2", "May28_SE2", "MaySE2-Carer", "ML_SE_2_one", "ML_SE_2_two", "Nach", "New Provider SE2", "Nov 10 DE SE2", "Nov 10 Supplier Extracts SE2", "Nov 11 Uprates SE2", "Nov 12 SE2", "Nov_2", "NSE2", "Oct 15 FA-SP SE2", "Oct 27 Debtor Extracts SE2", "Oct 27 Debtor with SP SE2", "Older Adults Dementia", "parker2", "Pearson", "Pepper Potters", "Per_Person2", "Personal Care", "Planned Personal Care", "Prison", "prov_fev_se2", "QA - SE", "QA - SE 2 - Person", "QA Tag 2", "QA-123", "QAZ 2", "Rate Schedule 27 -II-SE2", "Rate Schedule 27 -I-SE2", "Reg_SE2", "Regression 6.2.2.3", "S2", "s2001", "Sample_Service E 2", "sdf", "SDV 2", "SDV 25 -I-SE2", "SDV 25-II-SE2", "SdVR2", "SE 1 - Call off Budget", "SE 2 Check 1", "se002", "SE002 carer", "SE002 carer", "se002 debtor", "SE002 Pro", "SE002 Provider", "SE004", "SE2 1st Provi", "SE2 DTemplate", "SE2 FT", "SE2_123", "SE2_CAR_APR28", "SE2_mar", "SE2_Nov", "SE2_per", "SE2_Regression", "SE22", "SE-22-NO", "SE2-Demo8734", "SE2personupdate", "SE2sup", "Secondary", "Secondary Service", "SE-Dec27", "SElement 2", "Selement1234", "Sep 01 Supplier Transactions SE2", "Sep 07 Supplier Trans SE2", "Sep 24 Supplier Transactions SE2", "Sep 28 DebtorT SE2", "Ser_Ele 2", "Serv_Elem2", "Serv_Elem2_COntri", "Serv_jan09_SE2", "Service 11fun_2", "Service 11june_2", "Service E 2", "Service Ele 2", "service001", "service02", "service2", "subrag2", "Supplier Extract 18-SE2", "Supplier Extract 18-SE2-Varied", "Supplier Extract 2 - SE 2", "Suppliers 23- SE 2", "TF - SP - Provider - SE1", "Third Party Top Up", "Uniq2SE2", "Will Turner", "Work Station - Person - SE2 - Hours(Quater) - No Payments", "Work Station - Person - SE2 - One Off - Schedule", "Work Station - Person - SE2 - Units(Parts) - Invoice", "Work Station - Provider - SE2 - Hrs(Quater)", "Work Station - Provider - SE2 - Hrs(Quater) - Rate periods", "Work Station - Provider - SE2 - One Off", "Work Station - Provider - SE2 - Units(Parts)", "zsdcx" };
            var startDate = new DateTime(2020, 1, 1);

            foreach (var serviceElement2Name in ServiceElement2Names)
            {
                var recordExists = dbHelper.serviceElement1.GetByName(serviceElement2Name).Any();
                if (!recordExists)
                    dbHelper.serviceElement2.CreateServiceElement2(_careDirector_TeamId, _careDirector_BusinessUnitId, serviceElement2Name, startDate, 1);
            }
        }

        [Description("Populate the database with Service Provisions and Financial Assessments")]
        //[TestMethod]
        public void LoadTestScript_CreateServiceProvisionAndFinancialAssessment_Script01()
        {
            var matchingPersonRecords = dbHelper.person.GetByPreferredName("LoadDataScript ");
            int totalRecordsToCreate = matchingPersonRecords.Count > 50000 ? 50000 : matchingPersonRecords.Count;

            var _serviceProvisionStatusId_Authorised = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Authorised").FirstOrDefault();
            var startdate = new DateTime(2021, 12, 6);
            var enddate = new DateTime(2021, 12, 12);

            var systemUsers = dbHelper.systemUser.GetSystemUserByUserNamePrefix("performance_user_");
            int totalsystemUsers = systemUsers.Count;
            Random r = new Random();

            for (int i = 0; i < totalRecordsToCreate; i++)
            {
                Console.WriteLine("Cicle --> " + i);

                var responsibleuserid = systemUsers[r.Next(0, totalsystemUsers)];

                var _serviceProvisionId = dbHelper.serviceProvision.CreateServiceProvision(_careDirector_TeamId,
                    responsibleuserid, matchingPersonRecords[i], _serviceProvisionStatusId_Authorised,
                    _serviceElement1Id, _serviceElement2Id, _financeclientcategoryid,
                    _glCodeId, _rateUnitId, _serviceProvisionStartReasonId, null, _careDirector_TeamId, _serviceProvidedId, _providerId_Supplier,
                    responsibleuserid, _placementRoomTypeId, startdate, enddate, startdate);

                var _financialAssessmentID = dbHelper.financialAssessment.CreateFinancialAssessment(
                    _careDirector_TeamId, responsibleuserid, matchingPersonRecords[i], _financialAssessmentStatusId_Draft, _chargingRuleTypeId,
                    _incomeSupportTypeId, responsibleuserid, _financialAssessmentTypeId, startdate, enddate, startdate, 1, 291.35m);

                dbHelper.faContribution.CreateFAContribution(_financialAssessmentID, _serviceProvisionId, matchingPersonRecords[i], _careDirector_TeamId, _contributionTypeId, _recoveryMethodId, _DebtorBatchGroupingId, matchingPersonRecords[i], "person", "person name", startdate, enddate);

                dbHelper.financialAssessment.UpdateFinancialAssessmentStatus(_financialAssessmentID, _financialAssessmentStatusId_ReadyForAuthorisation);
                //dbHelper.financialAssessment.UpdateFinancialAssessmentStatus(_financialAssessmentID, _financialAssessmentStatusId_Authorised);

                dbHelper = new DBHelper.DatabaseHelper();
            }
        }

        [Description("Populate the database with Service Provisions and Service Deliveries")]
        //[TestMethod]
        public void LoadTestScript_CreateServiceProvisionAndSeriveDelivery_Script01()
        {
            var titles = new List<string> { "Mr", "Sir", "Dr", "Professor" };
            var maleFirstNames = new List<string> { "James", "Robert", "John", "Michael", "William", "David", "Richard", "Joseph", "Thomas", "Charles", "Christopher", "Daniel", "Matthew", "Anthony", "Mark", "Donald", "Steven", "Paul", "Andrew", "Joshua", "Kenneth", "Kevin", "Brian", "George", "Edward", "Ronald", "Timothy", "Jason", "Jeffrey", "Ryan", "Jacob", "Gary", "Nicholas", "Eric", "Jonathan", "Stephen", "Larry", "Justin", "Scott", "Brandon", "Benjamin", "Samuel", "Gregory", "Frank", "Alexander", "Raymond", "Patrick", "Jack", "Dennis", "Jerry", "Tyler", "Aaron", "Jose", "Adam", "Henry", "Nathan", "Douglas", "Zachary", "Peter", "Kyle", "Walter", "Ethan", "Jeremy", "Harold", "Keith", "Christian", "Roger", "Noah", "Gerald", "Carl", "Terry", "Sean", "Austin", "Arthur", "Lawrence", "Jesse", "Dylan", "Bryan", "Joe", "Jordan", "Billy", "Bruce", "Albert", "Willie", "Gabriel", "Logan", "Alan", "Juan", "Wayne", "Roy", "Ralph", "Randy", "Eugene", "Vincent", "Russell", "Elijah", "Louis", "Bobby", "Philip", "Johnny" };
            var lastNames = new List<string> { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez", "Hernandez", "Lopez", "Gonzales", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin", "Lee", "Perez", "Thompson", "White", "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson", "Walker", "Young", "Allen", "King", "Wright", "Scott", "Torres", "Nguyen", "Hill", "Flores", "Green", "Adams", "Nelson", "Baker", "Hall", "Rivera", "Campbell", "Mitchell", "Carter", "Roberts", "Gomez", "Phillips", "Evans", "Turner", "Diaz", "Parker", "Cruz", "Edwards", "Collins", "Reyes", "Stewart", "Morris", "Morales", "Murphy", "Cook", "Rogers", "Gutierrez", "Ortiz", "Morgan", "Cooper", "Peterson", "Bailey", "Reed", "Kelly", "Howard", "Ramos", "Kim", "Cox", "Ward", "Richardson", "Watson", "Brooks", "Chavez", "Wood", "James", "Bennet", "Gray", "Mendoza", "Ruiz", "Hughes", "Price", "Alvarez", "Castillo", "Sanders", "Patel", "Myers", "Long", "Ross", "Foster", "Jimenez" };
            var minDateOfBirth = new DateTime(1940, 1, 1);
            var maxDateOfBirth = new DateTime(2002, 12, 31);
            var Ethnicities = new List<Guid> { _ethnicity1Id, _ethnicity2Id, _ethnicity3Id, _ethnicity4Id };
            var genders = new List<int> { 1 };

            var _serviceProvisionStatusId_Draft = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Draft").FirstOrDefault();
            var _serviceProvisionStatusId_Authorised = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Authorised").FirstOrDefault();
            var startdate = new DateTime(2021, 12, 6);
            var enddate = new DateTime(2021, 12, 12);

            var systemUsers = dbHelper.systemUser.GetSystemUserByUserNamePrefix("performance_user_");
            int totalsystemUsers = systemUsers.Count;
            Random r = new Random();


            for (int i = 0; i <= 170; i++)
            {
                Console.WriteLine("Cicle --> " + i);
                var personIds = dbHelper.person.CreateMultiplePersonRecords(100, titles, maleFirstNames, lastNames, minDateOfBirth, maxDateOfBirth, Ethnicities, _careDirector_TeamId, 1, genders);
                dbHelper = new DBHelper.DatabaseHelper();

                foreach (var _personId in personIds)
                {
                    Console.WriteLine("Cicle --> " + i);

                    var responsibleuserid = systemUsers[r.Next(0, totalsystemUsers)];

                    var _serviceProvisionId = dbHelper.serviceProvision.CreateServiceProvision(_careDirector_TeamId,
                        responsibleuserid, _personId, _serviceProvisionStatusId_Draft,
                        _serviceElement1Id, _serviceElement2Id, _financeclientcategoryid,
                        _glCodeId, _rateUnitId, _serviceProvisionStartReasonId, null, _careDirector_TeamId, _serviceProvidedId, _providerId_Supplier,
                        responsibleuserid, _placementRoomTypeId, startdate, enddate, startdate);

                    dbHelper.serviceDelivery.CreateServiceDelivery(_careDirector_TeamId, _personId, _serviceProvisionId, _rateUnitId, 1, 1, true, true, true, true, true, true, true, true, "Load.Test.ServiceDelivery");

                    dbHelper = new DBHelper.DatabaseHelper();

                }

            }
        }



        [Description("Populate the database with Community Health Case records and Health Appointments")]
        //[TestMethod]
        public void LoadTestScript_CommunityHealthCases_Script01()
        {
            var matchingPersonRecords = dbHelper.person.GetByPreferredName("LoadDataScript");
            int totalRecordsToCreate = matchingPersonRecords.Count > 1 ? 1 : matchingPersonRecords.Count;

            var startdate = commonMethodsHelper.GenerateRandomDate(new DateTime(2019, 1, 1), DateTime.Now.Date);

            for (int i = 0; i < totalRecordsToCreate; i++)
            {
                Console.WriteLine("Cicle --> " + i);

                string presentingneeddetails = "presenting needs ...";

                Guid caseID = dbHelper.Case.CreateCommunityHealthCaseRecord(_careDirector_TeamId, matchingPersonRecords[i],
                    _load_Test_User_01Id, _communityAndClinicTeamId, _load_Test_User_01Id, _caseStatusId, _contactReasonId,
                    _contactAdministrativeCategory, _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _ContactSourceId, startdate,
                    startdate, startdate, startdate, presentingneeddetails);

                //create Health Appointtment 
                TimeSpan startTime1 = new TimeSpan(10, 0, 0);
                TimeSpan endTime1 = new TimeSpan(10, 5, 0);
                TimeSpan startTime2 = new TimeSpan(10, 10, 0);
                TimeSpan endTime2 = new TimeSpan(10, 15, 0);
                TimeSpan startTime3 = new TimeSpan(10, 20, 0);
                TimeSpan endTime3 = new TimeSpan(10, 25, 0);
                bool cancelappointment = false;
                Guid? wholedtheappointmentid = null;
                Guid? healthappointmentoutcometypeid = null;
                int? cancellationreasontypeid = null;
                int? nonattendancetypeid = null;
                Guid? WhoCancelledTheAppointmentId = null;
                string WhoCancelledTheAppointmentIdName = "";
                string WhoCancelledTheAppointmentIdTableName = "";
                string whocancelledtheappointmentfreetext = "";
                DateTime? dateunavailablefrom = null;
                DateTime? dateavailablefrom = null;
                Guid? healthappointmentabsencereasonid = null;
                DateTime? cnanotificationdate = null;
                bool additionalprofessionalrequired = true;
                bool addtraveltimetoappointment = false;
                bool returntobaseafterappointment = false;

                //Guid healthAppointment1ID = dbHelper.healthAppointment.CreateHealthAppointment(_careDirector_TeamId, matchingPersonRecords[i], "Person First Name",
                //    _dataFormId_Appointments, _healthAppointmentContactTypeId, _healthAppointmentReasonId, "Assessment", caseID, _load_Test_User_01Id,
                //    _communityAndClinicTeamId, _healthAppointmentLocationTypeId, "Clients or patients home", _load_Test_User_01Id, "appointment information ...",
                //    startdate, startTime1, endTime1, startdate, cancelappointment, wholedtheappointmentid, healthappointmentoutcometypeid,
                //    cancellationreasontypeid, nonattendancetypeid, WhoCancelledTheAppointmentId, WhoCancelledTheAppointmentIdName,
                //    WhoCancelledTheAppointmentIdTableName, whocancelledtheappointmentfreetext, dateunavailablefrom, dateavailablefrom,
                //    healthappointmentabsencereasonid, cnanotificationdate, additionalprofessionalrequired, addtraveltimetoappointment, returntobaseafterappointment, true, true);

                //Guid healthAppointment2ID = dbHelper.healthAppointment.CreateHealthAppointment(_careDirector_TeamId, matchingPersonRecords[i], "Person First Name",
                //    _dataFormId_Appointments, _healthAppointmentContactTypeId, _healthAppointmentReasonId, "Assessment", caseID, _load_Test_User_01Id,
                //    _communityAndClinicTeamId, _healthAppointmentLocationTypeId, "Clients or patients home", _load_Test_User_01Id, "appointment information ...",
                //    startdate, startTime2, endTime2, startdate, cancelappointment, wholedtheappointmentid, healthappointmentoutcometypeid,
                //    cancellationreasontypeid, nonattendancetypeid, WhoCancelledTheAppointmentId, WhoCancelledTheAppointmentIdName,
                //    WhoCancelledTheAppointmentIdTableName, whocancelledtheappointmentfreetext, dateunavailablefrom, dateavailablefrom,
                //    healthappointmentabsencereasonid, cnanotificationdate, additionalprofessionalrequired, addtraveltimetoappointment, returntobaseafterappointment, true, true);

                //Guid healthAppointment3ID = dbHelper.healthAppointment.CreateHealthAppointment(_careDirector_TeamId, matchingPersonRecords[i], "Person First Name",
                //    _dataFormId_Appointments, _healthAppointmentContactTypeId, _healthAppointmentReasonId, "Assessment", caseID, _load_Test_User_01Id,
                //    _communityAndClinicTeamId, _healthAppointmentLocationTypeId, "Clients or patients home", _load_Test_User_01Id, "appointment information ...",
                //    startdate, startTime3, endTime3, startdate, cancelappointment, wholedtheappointmentid, healthappointmentoutcometypeid,
                //    cancellationreasontypeid, nonattendancetypeid, WhoCancelledTheAppointmentId, WhoCancelledTheAppointmentIdName,
                //    WhoCancelledTheAppointmentIdTableName, whocancelledtheappointmentfreetext, dateunavailablefrom, dateavailablefrom,
                //    healthappointmentabsencereasonid, cnanotificationdate, additionalprofessionalrequired, addtraveltimetoappointment, returntobaseafterappointment, true, true);

                dbHelper = new DBHelper.DatabaseHelper();
            }
        }

        [Description("Populate the database with Social Care Case records")]
        //[TestMethod]
        public void LoadTestScript_SocialCareCases_Script01()
        {
            var titles = new List<string> { "Mr", "Sir", "Dr", "Professor" };
            var maleFirstNames = new List<string> { "James", "Robert", "John", "Michael", "William", "David", "Richard", "Joseph", "Thomas", "Charles", "Christopher", "Daniel", "Matthew", "Anthony", "Mark", "Donald", "Steven", "Paul", "Andrew", "Joshua", "Kenneth", "Kevin", "Brian", "George", "Edward", "Ronald", "Timothy", "Jason", "Jeffrey", "Ryan", "Jacob", "Gary", "Nicholas", "Eric", "Jonathan", "Stephen", "Larry", "Justin", "Scott", "Brandon", "Benjamin", "Samuel", "Gregory", "Frank", "Alexander", "Raymond", "Patrick", "Jack", "Dennis", "Jerry", "Tyler", "Aaron", "Jose", "Adam", "Henry", "Nathan", "Douglas", "Zachary", "Peter", "Kyle", "Walter", "Ethan", "Jeremy", "Harold", "Keith", "Christian", "Roger", "Noah", "Gerald", "Carl", "Terry", "Sean", "Austin", "Arthur", "Lawrence", "Jesse", "Dylan", "Bryan", "Joe", "Jordan", "Billy", "Bruce", "Albert", "Willie", "Gabriel", "Logan", "Alan", "Juan", "Wayne", "Roy", "Ralph", "Randy", "Eugene", "Vincent", "Russell", "Elijah", "Louis", "Bobby", "Philip", "Johnny" };
            var lastNames = new List<string> { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez", "Hernandez", "Lopez", "Gonzales", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin", "Lee", "Perez", "Thompson", "White", "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson", "Walker", "Young", "Allen", "King", "Wright", "Scott", "Torres", "Nguyen", "Hill", "Flores", "Green", "Adams", "Nelson", "Baker", "Hall", "Rivera", "Campbell", "Mitchell", "Carter", "Roberts", "Gomez", "Phillips", "Evans", "Turner", "Diaz", "Parker", "Cruz", "Edwards", "Collins", "Reyes", "Stewart", "Morris", "Morales", "Murphy", "Cook", "Rogers", "Gutierrez", "Ortiz", "Morgan", "Cooper", "Peterson", "Bailey", "Reed", "Kelly", "Howard", "Ramos", "Kim", "Cox", "Ward", "Richardson", "Watson", "Brooks", "Chavez", "Wood", "James", "Bennet", "Gray", "Mendoza", "Ruiz", "Hughes", "Price", "Alvarez", "Castillo", "Sanders", "Patel", "Myers", "Long", "Ross", "Foster", "Jimenez" };
            var minDateOfBirth = new DateTime(1940, 1, 1);
            var maxDateOfBirth = new DateTime(2002, 12, 31);
            var Ethnicities = new List<Guid> { _ethnicity1Id, _ethnicity2Id, _ethnicity3Id, _ethnicity4Id };
            var genders = new List<int> { 1 };
            var systemUsers = dbHelper.systemUser.GetSystemUserByUserNamePrefix("performance_user_");

            Random r = new Random();

            for (int i = 0; i <= 1000; i++)
            {
                Console.WriteLine("Cicle --> " + i);
                var personIds = dbHelper.person.CreateMultiplePersonRecords(100, titles, maleFirstNames, lastNames, minDateOfBirth, maxDateOfBirth, Ethnicities, _careDirector_TeamId, 1, genders);
                dbHelper = new DBHelper.DatabaseHelper();

                var startdate = commonMethodsHelper.GenerateRandomDate(new DateTime(2019, 1, 1), DateTime.Now.Date);
                dbHelper.Case.CreateMultipleSocialCareCaseRecord(_careDirector_TeamId, personIds, systemUsers, systemUsers, _socialCareCaseStatusId, _contactReasonId, _dataFormId_SocialCareCase, _ContactSourceId, startdate, startdate, 18);
                dbHelper = new DBHelper.DatabaseHelper();
            }

        }

        [Description("Populate the database with User Data Views")]
        //[TestMethod]
        public void LoadTestScript_CreateUserDataView_Script01()
        {
            //var view1 = new UserDataView();
            //view1.userdataviewid = new Guid("B41C3723-D542-EC11-A32D-F90A4322A942");
            //view1.businessobjectid = new Guid("79F4EFC4-BFB1-E811-80DC-0050560502CC");
            //view1.owninguserid = _load_Test_User_01Id;
            //view1.name = "DT - Unallocated Cases";
            //view1.dataqueryxml = "<DataQuery   TableName=\"case\" TableAlias=\"CASE\">    <Relationships>      <DataRelationship RightTableName=\"casestatus\" RightTableAlias=\"CASE_CASESTATUSID_CASESTATUS\" JoinType=\"InnerJoin\">        <DataRelationshipJoins>          <DataRelationshipJoin TableAlias=\"CASE\" TableFieldName=\"casestatusid\" RightTableFieldName=\"casestatusid\" ValueType=\"Field\" />        </DataRelationshipJoins>      </DataRelationship>    </Relationships>    <Filter FilterType=\"And\">      <Filters>        <DataFilter FilterType=\"And\">          <Conditions>            <Condition TableAlias=\"CASE\" Field=\"OWNERID\" Operator=\"EqualsCurrentUserDefaultTeam\" ValueType=\"None\" />            <Condition TableAlias=\"CASE\" Field=\"RESPONSIBLEUSERID\" Operator=\"Null\" ValueType=\"None\" />          </Conditions>        </DataFilter>        <DataFilter FilterType=\"And\">          <Conditions>            <Condition TableAlias=\"CASE_CASESTATUSID_CASESTATUS\" Field=\"CASESTATUSTYPEID\" Operator=\"NotEqual\" ValueType=\"SingleValue\">              <ConditionValue DbType=\"Int\" IntValue=\"5\" />            </Condition>          </Conditions>        </DataFilter>      </Filters>    </Filter>    <ApplyFieldLevelSecurity>false</ApplyFieldLevelSecurity>  </DataQuery>";
            //view1.layoutxml = "<Layout/>";
            //view1.conditionbuilderxml = "<ConditionBuilderQuery  >    <BusinessObjectId>79f4efc4-bfb1-e811-80dc-0050560502cc</BusinessObjectId>    <DisplayName>Case</DisplayName>    <Parental>false</Parental>    <Type>0</Type>    <Rules>      <Condition>AND</Condition>      <Valid>true</Valid>      <Rules>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>adf4efc4-bfb1-e811-80dc-0050560502cc</Id>          <Field>adf4efc4-bfb1-e811-80dc-0050560502cc</Field>          <Operator>EqualsCurrentUserDefaultTeam</Operator>          <Data>            <IsDateTime>false</IsDateTime>            <BusinessObjectName>team</BusinessObjectName>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>6ccf2fd0-0dc0-e811-80dc-0050560502cc</Id>          <Field>6ccf2fd0-0dc0-e811-80dc-0050560502cc</Field>          <Operator>Null</Operator>          <Data>            <IsDateTime>false</IsDateTime>            <BusinessObjectName>systemuser</BusinessObjectName>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>      </Rules>    </Rules>    <RelatedBusinessObjects>      <ConditionBuilderQuery>        <BusinessObjectId>93201404-a9c0-e811-80dc-0050560502cc</BusinessObjectId>        <DisplayName>Case Status | Case Status</DisplayName>        <RelationshipId>59bc3127-aec0-e811-80dc-0050560502cc</RelationshipId>        <RelationshipFieldId>53bc3127-aec0-e811-80dc-0050560502cc</RelationshipFieldId>        <RelationshipFieldName>casestatusid</RelationshipFieldName>        <Parental>true</Parental>        <Type>0</Type>        <Rules>          <Condition>AND</Condition>          <Valid>true</Valid>          <Rules>            <ConditionBuilderRule>              <Valid>false</Valid>              <Id>88dcd9f3-adc0-e811-80dc-0050560502cc</Id>              <Field>88dcd9f3-adc0-e811-80dc-0050560502cc</Field>              <Operator>NotEqual</Operator>              <Value>                <ConditionBuilderRuleValue>                  <Key>Field</Key>                  <Label>Close and Resolve Case</Label>                  <Value>5</Value>                  <ValueType>Value</ValueType>                  <IsLookup>true</IsLookup>                </ConditionBuilderRuleValue>              </Value>              <Data>                <IsDateTime>false</IsDateTime>                <OptionSetId>2cfb7188-aac0-e811-80dc-0050560502cc</OptionSetId>                <FilterType>0</FilterType>                <DataType>0</DataType>              </Data>            </ConditionBuilderRule>          </Rules>        </Rules>        <RelatedBusinessObjects />      </ConditionBuilderQuery>    </RelatedBusinessObjects>  </ConditionBuilderQuery>";
            //view1.firstsortfieldid = new Guid("BD13A05F-2FC2-E811-80DC-0050560502CC");
            //view1.secondsortfieldid = null;

            //dbHelper.userDataView.CreateUserDataView(view1.userdataviewid, view1.businessobjectid, view1.owninguserid, view1.name, view1.dataqueryxml, view1.layoutxml, view1.conditionbuilderxml, view1.firstsortfieldid, view1.secondsortfieldid);

            //view1.userdataviewid = new Guid("1627A5C1-D542-EC11-A32D-F90A4322A942");
            //view1.businessobjectid = new Guid("79F4EFC4-BFB1-E811-80DC-0050560502CC");
            //view1.owninguserid = _load_Test_User_01Id;
            //view1.name = "DT - My Default Team - unallocated Secondary Cases";
            //view1.dataqueryxml = "<DataQuery   TableName=\"case\" TableAlias=\"CASE\">    <Relationships>      <DataRelationship RightTableName=\"caseinvolvement\" RightTableAlias=\"CASEINVOLVEMENT_CASEID_CASE\" JoinType=\"InnerJoin\" OneToMany=\"true\">        <DataRelationshipJoins>          <DataRelationshipJoin TableAlias=\"CASE\" TableFieldName=\"caseid\" RightTableFieldName=\"caseid\" ValueType=\"Field\" />        </DataRelationshipJoins>      </DataRelationship>    </Relationships>    <Filter FilterType=\"And\">      <Filters>        <DataFilter FilterType=\"And\">          <Conditions>            <Condition TableAlias=\"CASE\" Field=\"INACTIVE\" Operator=\"Equal\" ValueType=\"SingleValue\">              <ConditionValue DbType=\"Bit\" BoolValue=\"false\" />            </Condition>            <Condition TableAlias=\"CASE\" Field=\"CASEID\" Operator=\"NotInDataView\" ValueType=\"SingleValue\">              <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"281d0874-d542-ec11-a32d-f90a4322a942\" />            </Condition>          </Conditions>        </DataFilter>        <DataFilter FilterType=\"And\">          <Conditions>            <Condition TableAlias=\"CASEINVOLVEMENT_CASEID_CASE\" Field=\"INVOLVEMENTMEMBERID\" Operator=\"EqualsCurrentUserDefaultTeam\" ValueType=\"None\" />            <Condition TableAlias=\"CASEINVOLVEMENT_CASEID_CASE\" Field=\"INVOLVEMENTROLEID\" Operator=\"Equal\" ValueType=\"SingleValue\">              <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"fda89be5-2ac2-e811-80dc-0050560502cc\" />            </Condition>            <Condition TableAlias=\"CASEINVOLVEMENT_CASEID_CASE\" Field=\"INACTIVE\" Operator=\"Equal\" ValueType=\"SingleValue\">              <ConditionValue DbType=\"Bit\" BoolValue=\"false\" />            </Condition>          </Conditions>        </DataFilter>      </Filters>    </Filter>    <ApplyFieldLevelSecurity>false</ApplyFieldLevelSecurity>  </DataQuery>";
            //view1.layoutxml = "<Layout/>";
            //view1.conditionbuilderxml = "<ConditionBuilderQuery  >    <BusinessObjectId>79f4efc4-bfb1-e811-80dc-0050560502cc</BusinessObjectId>    <DisplayName>Case</DisplayName>    <Parental>false</Parental>    <Type>0</Type>    <Rules>      <Condition>AND</Condition>      <Valid>true</Valid>      <Rules>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>cbf4efc4-bfb1-e811-80dc-0050560502cc</Id>          <Field>cbf4efc4-bfb1-e811-80dc-0050560502cc</Field>          <Operator>Equal</Operator>          <Value>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Value>false</Value>              <ValueType>Value</ValueType>              <IsLookup>false</IsLookup>            </ConditionBuilderRuleValue>          </Value>        </ConditionBuilderRule>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>81f4efc4-bfb1-e811-80dc-0050560502cc</Id>          <Field>81f4efc4-bfb1-e811-80dc-0050560502cc</Field>          <Operator>NotInDataView</Operator>          <Value>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>DT - Cases with at least one active Secondary Worker from my Default Team</Label>              <Value>281d0874-d542-ec11-a32d-f90a4322a942</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>          </Value>          <Data>            <IsDateTime>false</IsDateTime>            <BusinessObjectName>case</BusinessObjectName>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>      </Rules>    </Rules>    <RelatedBusinessObjects>      <ConditionBuilderQuery>        <BusinessObjectId>5c9198f4-a6c1-e811-9c04-1866da1e3bda</BusinessObjectId>        <DisplayName>Case Involvements | Case</DisplayName>        <RelationshipId>ce983ebc-0cc3-e811-80dc-0050560502cc</RelationshipId>        <RelationshipFieldId>c8983ebc-0cc3-e811-80dc-0050560502cc</RelationshipFieldId>        <RelationshipFieldName>caseid</RelationshipFieldName>        <Parental>false</Parental>        <Type>0</Type>        <Rules>          <Condition>AND</Condition>          <Valid>true</Valid>          <Rules>            <ConditionBuilderRule>              <Valid>false</Valid>              <Id>c69198f4-a6c1-e811-9c04-1866da1e3bda</Id>              <Field>c69198f4-a6c1-e811-9c04-1866da1e3bda</Field>              <Operator>EqualsCurrentUserDefaultTeam</Operator>              <Data>                <IsDateTime>false</IsDateTime>                <BusinessObjectName>caseinvolvement</BusinessObjectName>                <MultiLookupFieldName>involvementmemberid</MultiLookupFieldName>                <FilterType>0</FilterType>                <DataType>0</DataType>              </Data>            </ConditionBuilderRule>            <ConditionBuilderRule>              <Valid>false</Valid>              <Id>e29198f4-a6c1-e811-9c04-1866da1e3bda</Id>              <Field>e29198f4-a6c1-e811-9c04-1866da1e3bda</Field>              <Operator>Equal</Operator>              <Value>                <ConditionBuilderRuleValue>                  <Key>Field</Key>                  <Label>Secondary Team</Label>                  <Value>fda89be5-2ac2-e811-80dc-0050560502cc</Value>                  <ValueType>Value</ValueType>                  <IsLookup>true</IsLookup>                </ConditionBuilderRuleValue>              </Value>              <Data>                <IsDateTime>false</IsDateTime>                <BusinessObjectName>involvementrole</BusinessObjectName>                <FilterType>0</FilterType>                <DataType>0</DataType>              </Data>            </ConditionBuilderRule>            <ConditionBuilderRule>              <Valid>false</Valid>              <Id>ac9198f4-a6c1-e811-9c04-1866da1e3bda</Id>              <Field>ac9198f4-a6c1-e811-9c04-1866da1e3bda</Field>              <Operator>Equal</Operator>              <Value>                <ConditionBuilderRuleValue>                  <Key>Field</Key>                  <Value>false</Value>                  <ValueType>Value</ValueType>                  <IsLookup>false</IsLookup>                </ConditionBuilderRuleValue>              </Value>            </ConditionBuilderRule>          </Rules>        </Rules>        <RelatedBusinessObjects />      </ConditionBuilderQuery>    </RelatedBusinessObjects>  </ConditionBuilderQuery>";
            //view1.firstsortfieldid = new Guid("BD13A05F-2FC2-E811-80DC-0050560502CC");
            //view1.secondsortfieldid = null;

            //dbHelper.userDataView.CreateUserDataView(view1.userdataviewid, view1.businessobjectid, view1.owninguserid, view1.name, view1.dataqueryxml, view1.layoutxml, view1.conditionbuilderxml, view1.firstsortfieldid, view1.secondsortfieldid);



            //view1.userdataviewid = new Guid("38F06CB2-BD41-EA11-A2C8-005056926FE4");
            //view1.businessobjectid = new Guid("B1E05CA6-ACB1-E811-80DC-0050560502CC");
            //view1.owninguserid = _load_Test_User_01Id;
            //view1.name = "DT Contacts";
            //view1.dataqueryxml = "<DataQuery TableName=\"contact\" TableAlias=\"CONTACT\" IsSubQuery=\"false\"><Filter FilterType=\"And\"><Conditions><Condition TableAlias=\"CONTACT\" Field=\"CREATEDBY\" Operator=\"EqualUserId\" ValueType=\"None\"/></Conditions></Filter></DataQuery>";
            //view1.layoutxml = "<Layout/>";
            //view1.conditionbuilderxml = "<ConditionBuilderQuery><BusinessObjectId>b1e05ca6-acb1-e811-80dc-0050560502cc</BusinessObjectId><DisplayName>Contact</DisplayName><Parental>false</Parental><Type>0</Type><Rules><Condition>AND</Condition><Valid>true</Valid><Rules><ConditionBuilderRule><Valid>false</Valid><Id>c1e05ca6-acb1-e811-80dc-0050560502cc</Id><Field>c1e05ca6-acb1-e811-80dc-0050560502cc</Field><Operator>EqualUserId</Operator><Data><IsDateTime>false</IsDateTime><BusinessObjectName>systemuser</BusinessObjectName><FilterType>0</FilterType><DataType>0</DataType></Data></ConditionBuilderRule></Rules></Rules><RelatedBusinessObjects/></ConditionBuilderQuery>";
            //view1.firstsortfieldid = null;
            //view1.secondsortfieldid = null;

            //dbHelper.userDataView.CreateUserDataView(view1.userdataviewid, view1.businessobjectid, view1.owninguserid, view1.name, view1.dataqueryxml, view1.layoutxml, view1.conditionbuilderxml, view1.firstsortfieldid, view1.secondsortfieldid);

            //view1.userdataviewid = new Guid("D84987B0-D742-EC11-A32D-F90A4322A942");
            //view1.businessobjectid = new Guid("1143C28D-FCBF-E811-9C04-1866DA1E3BDA");
            //view1.owninguserid = _load_Test_User_01Id;
            //view1.name = "DT Manager - Case Forms for Authorisation";
            //view1.dataqueryxml = "<DataQuery   TableName=\"caseform\" TableAlias=\"CASEFORM\">    <Filter FilterType=\"And\">      <Conditions>        <Condition TableAlias=\"CASEFORM\" Field=\"OWNERID\" Operator=\"EqualsCurrentUserDefaultTeam\" ValueType=\"None\" />        <Condition TableAlias=\"CASEFORM\" Field=\"ASSESSMENTSTATUSID\" Operator=\"Equal\" ValueType=\"SingleValue\">          <ConditionValue DbType=\"Int\" IntValue=\"2\" />        </Condition>      </Conditions>    </Filter>    <ApplyFieldLevelSecurity>false</ApplyFieldLevelSecurity>  </DataQuery>";
            //view1.layoutxml = "<Layout   />";
            //view1.conditionbuilderxml = "<ConditionBuilderQuery  >    <BusinessObjectId>1143c28d-fcbf-e811-9c04-1866da1e3bda</BusinessObjectId>    <DisplayName>Form (Case)</DisplayName>    <Parental>false</Parental>    <Type>0</Type>    <Rules>      <Condition>AND</Condition>      <Valid>true</Valid>      <Rules>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>4543c28d-fcbf-e811-9c04-1866da1e3bda</Id>          <Field>4543c28d-fcbf-e811-9c04-1866da1e3bda</Field>          <Operator>EqualsCurrentUserDefaultTeam</Operator>          <Data>            <IsDateTime>false</IsDateTime>            <BusinessObjectName>team</BusinessObjectName>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>8b43c28d-fcbf-e811-9c04-1866da1e3bda</Id>          <Field>8b43c28d-fcbf-e811-9c04-1866da1e3bda</Field>          <Operator>Equal</Operator>          <Value>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>Complete</Label>              <Value>2</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>          </Value>          <Data>            <IsDateTime>false</IsDateTime>            <OptionSetId>30ac28b0-c855-e311-b694-1803731f3ee3</OptionSetId>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>      </Rules>    </Rules>    <RelatedBusinessObjects />  </ConditionBuilderQuery>";
            //view1.firstsortfieldid = new Guid("9D43C28D-FCBF-E811-9C04-1866DA1E3BDA");
            //view1.secondsortfieldid = new Guid("2B43C28D-FCBF-E811-9C04-1866DA1E3BDA");

            //dbHelper.userDataView.CreateUserDataView(view1.userdataviewid, view1.businessobjectid, view1.owninguserid, view1.name, view1.dataqueryxml, view1.layoutxml, view1.conditionbuilderxml, view1.firstsortfieldid, view1.secondsortfieldid);

            //view1.userdataviewid = new Guid("873F53DA-D742-EC11-A32D-F90A4322A942");
            //view1.businessobjectid = new Guid("1143C28D-FCBF-E811-9C04-1866DA1E3BDA");
            //view1.owninguserid = _load_Test_User_01Id;
            //view1.name = "DT Manager - Case Forms with Worker";
            //view1.dataqueryxml = "<DataQuery   TableName=\"caseform\" TableAlias=\"CASEFORM\">    <Filter FilterType=\"And\">      <Conditions>        <Condition TableAlias=\"CASEFORM\" Field=\"OWNERID\" Operator=\"EqualsCurrentUserDefaultTeam\" ValueType=\"None\" />        <Condition TableAlias=\"CASEFORM\" Field=\"ASSESSMENTSTATUSID\" Operator=\"Equal\" ValueType=\"SingleValue\">          <ConditionValue DbType=\"Int\" IntValue=\"1\" />        </Condition>      </Conditions>    </Filter>    <ApplyFieldLevelSecurity>false</ApplyFieldLevelSecurity>  </DataQuery>";
            //view1.layoutxml = "<Layout   />";
            //view1.conditionbuilderxml = "<ConditionBuilderQuery  >    <BusinessObjectId>1143c28d-fcbf-e811-9c04-1866da1e3bda</BusinessObjectId>    <DisplayName>Form (Case)</DisplayName>    <Parental>false</Parental>    <Type>0</Type>    <Rules>      <Condition>AND</Condition>      <Valid>true</Valid>      <Rules>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>4543c28d-fcbf-e811-9c04-1866da1e3bda</Id>          <Field>4543c28d-fcbf-e811-9c04-1866da1e3bda</Field>          <Operator>EqualsCurrentUserDefaultTeam</Operator>          <Data>            <IsDateTime>false</IsDateTime>            <BusinessObjectName>team</BusinessObjectName>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>8b43c28d-fcbf-e811-9c04-1866da1e3bda</Id>          <Field>8b43c28d-fcbf-e811-9c04-1866da1e3bda</Field>          <Operator>Equal</Operator>          <Value>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>In Progress</Label>              <Value>1</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>          </Value>          <Data>            <IsDateTime>false</IsDateTime>            <OptionSetId>30ac28b0-c855-e311-b694-1803731f3ee3</OptionSetId>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>      </Rules>    </Rules>    <RelatedBusinessObjects />  </ConditionBuilderQuery>";
            //view1.firstsortfieldid = new Guid("9D43C28D-FCBF-E811-9C04-1866DA1E3BDA");
            //view1.secondsortfieldid = new Guid("9543C28D-FCBF-E811-9C04-1866DA1E3BDA");


            //dbHelper.userDataView.CreateUserDataView(view1.userdataviewid, view1.businessobjectid, view1.owninguserid, view1.name, view1.dataqueryxml, view1.layoutxml, view1.conditionbuilderxml, view1.firstsortfieldid, view1.secondsortfieldid);

            //view1.userdataviewid = new Guid("4BFDF600-D842-EC11-A32D-F90A4322A942");
            //view1.businessobjectid = new Guid("1143C28D-FCBF-E811-9C04-1866DA1E3BDA");
            //view1.owninguserid = _load_Test_User_01Id;
            //view1.name = "DT Manager - Case Forms authorised in last 15 days";
            //view1.dataqueryxml = "<DataQuery   TableName=\"caseform\" TableAlias=\"CASEFORM\">    <Filter FilterType=\"And\">      <Conditions>        <Condition TableAlias=\"CASEFORM\" Field=\"OWNERID\" Operator=\"EqualsCurrentUserDefaultTeam\" ValueType=\"None\" />        <Condition TableAlias=\"CASEFORM\" Field=\"ASSESSMENTSTATUSID\" Operator=\"Equal\" ValueType=\"SingleValue\">          <ConditionValue DbType=\"Int\" IntValue=\"3\" />        </Condition>        <Condition TableAlias=\"CASEFORM\" Field=\"SIGNOFFDATE\" Operator=\"LastNDays\" ValueType=\"SingleValue\">          <ConditionValue DbType=\"Int\" IntValue=\"15\" />        </Condition>      </Conditions>    </Filter>    <UnionQueries />    <ApplyFieldLevelSecurity>false</ApplyFieldLevelSecurity>  </DataQuery>";
            //view1.layoutxml = null;
            //view1.conditionbuilderxml = "<ConditionBuilderQuery  >    <BusinessObjectId>1143c28d-fcbf-e811-9c04-1866da1e3bda</BusinessObjectId>    <DisplayName>Form (Case)</DisplayName>    <Parental>false</Parental>    <Type>0</Type>    <Rules>      <Condition>AND</Condition>      <Valid>true</Valid>      <Rules>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>4543c28d-fcbf-e811-9c04-1866da1e3bda</Id>          <Field>4543c28d-fcbf-e811-9c04-1866da1e3bda</Field>          <Operator>EqualsCurrentUserDefaultTeam</Operator>          <Data>            <IsDateTime>false</IsDateTime>            <BusinessObjectName>team</BusinessObjectName>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>8b43c28d-fcbf-e811-9c04-1866da1e3bda</Id>          <Field>8b43c28d-fcbf-e811-9c04-1866da1e3bda</Field>          <Operator>Equal</Operator>          <Value>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>Closed</Label>              <Value>3</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>          </Value>          <Data>            <IsDateTime>false</IsDateTime>            <OptionSetId>30ac28b0-c855-e311-b694-1803731f3ee3</OptionSetId>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>7f75c093-fcbf-e811-9c04-1866da1e3bda</Id>          <Field>7f75c093-fcbf-e811-9c04-1866da1e3bda</Field>          <Operator>LastNDays</Operator>          <Value>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Value>15</Value>              <ValueType>Value</ValueType>              <IsLookup>false</IsLookup>            </ConditionBuilderRuleValue>          </Value>          <Data>            <IsDateTime>true</IsDateTime>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>      </Rules>    </Rules>    <RelatedBusinessObjects />  </ConditionBuilderQuery>";
            //view1.firstsortfieldid = new Guid("7575C093-FCBF-E811-9C04-1866DA1E3BDA");
            //view1.secondsortfieldid = null;


            //dbHelper.userDataView.CreateUserDataView(view1.userdataviewid, view1.businessobjectid, view1.owninguserid, view1.name, view1.dataqueryxml, view1.layoutxml, view1.conditionbuilderxml, view1.firstsortfieldid, view1.secondsortfieldid);

            //view1.userdataviewid = new Guid("EBF4C414-E342-EC11-A32D-F90A4322A942");
            //view1.businessobjectid = new Guid("49353AAB-F3A5-E811-80DC-0050560502CC");
            //view1.owninguserid = _load_Test_User_01Id;
            //view1.name = "DT - My Phone Call Activities";
            //view1.dataqueryxml = "<DataQuery   TableName=\"phonecall\" TableAlias=\"PHONECALL\">    <Filter FilterType=\"And\">      <Conditions>        <Condition TableAlias=\"PHONECALL\" Field=\"RESPONSIBLEUSERID\" Operator=\"EqualUserId\" ValueType=\"None\" />        <Condition TableAlias=\"PHONECALL\" Field=\"INACTIVE\" Operator=\"Equal\" ValueType=\"SingleValue\">          <ConditionValue DbType=\"Bit\" BoolValue=\"false\" />        </Condition>      </Conditions>    </Filter>    <UnionQueries />    <ApplyFieldLevelSecurity>false</ApplyFieldLevelSecurity>  </DataQuery>";
            //view1.layoutxml = "<Layout  >    <Columns>      <Column Name=\"phonecalldate\" Width=\"150\" FieldId=\"936caba9-f4a5-e811-80dc-0050560502cc\" />      <Column Name=\"personnumber\" Width=\"150\" FieldId=\"d1d39522-f39a-e811-80dc-0050560502cc\">        <RelationshipId>1316ff43-f5a5-e811-80dc-0050560502cc</RelationshipId>      </Column>      <Column Name=\"regardingid\" Width=\"200\" FieldId=\"cb353aab-f3a5-e811-80dc-0050560502cc\" />      <Column Name=\"subject\" Width=\"200\" FieldId=\"9b353aab-f3a5-e811-80dc-0050560502cc\" />    </Columns>  </Layout>";
            //view1.conditionbuilderxml = "<ConditionBuilderQuery  >    <BusinessObjectId>49353aab-f3a5-e811-80dc-0050560502cc</BusinessObjectId>    <DisplayName>Phone Call</DisplayName>    <Parental>false</Parental>    <Type>0</Type>    <Rules>      <Condition>AND</Condition>      <Valid>true</Valid>      <Rules>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>87353aab-f3a5-e811-80dc-0050560502cc</Id>          <Field>87353aab-f3a5-e811-80dc-0050560502cc</Field>          <Operator>EqualUserId</Operator>          <Data>            <IsDateTime>false</IsDateTime>            <BusinessObjectName>systemuser</BusinessObjectName>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>a5353aab-f3a5-e811-80dc-0050560502cc</Id>          <Field>a5353aab-f3a5-e811-80dc-0050560502cc</Field>          <Operator>Equal</Operator>          <Value>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Value>false</Value>              <ValueType>Value</ValueType>              <IsLookup>false</IsLookup>            </ConditionBuilderRuleValue>          </Value>        </ConditionBuilderRule>      </Rules>    </Rules>    <RelatedBusinessObjects />  </ConditionBuilderQuery>";
            //view1.firstsortfieldid = new Guid("63353AAB-F3A5-E811-80DC-0050560502CC");
            //view1.secondsortfieldid = null;


            //dbHelper.userDataView.CreateUserDataView(view1.userdataviewid, view1.businessobjectid, view1.owninguserid, view1.name, view1.dataqueryxml, view1.layoutxml, view1.conditionbuilderxml, view1.firstsortfieldid, view1.secondsortfieldid);

            //view1.userdataviewid = new Guid("96556C2F-E342-EC11-A32D-F90A4322A942");
            //view1.businessobjectid = new Guid("04F4874B-199D-E611-9BD2-1866DA1E4209");
            //view1.owninguserid = _load_Test_User_01Id;
            //view1.name = "DT - My Tasks";
            //view1.dataqueryxml = "<DataQuery   TableName=\"task\" TableAlias=\"TASK\">    <Filter FilterType=\"And\">      <Conditions>        <Condition TableAlias=\"TASK\" Field=\"RESPONSIBLEUSERID\" Operator=\"EqualUserId\" ValueType=\"None\" />        <Condition TableAlias=\"TASK\" Field=\"INACTIVE\" Operator=\"Equal\" ValueType=\"SingleValue\">          <ConditionValue DbType=\"Bit\" BoolValue=\"false\" />        </Condition>      </Conditions>    </Filter>    <UnionQueries />    <ApplyFieldLevelSecurity>false</ApplyFieldLevelSecurity>  </DataQuery>";
            //view1.layoutxml = "<Layout  >    <Columns>      <Column Name=\"duedate\" Width=\"150\" FieldId=\"a565b468-4ea0-e611-80d3-0050560502cc\" />      <Column Name=\"personnumber\" Width=\"150\" FieldId=\"d1d39522-f39a-e811-80dc-0050560502cc\">        <RelationshipId>2eb91488-09ac-e611-80d3-0050560502cc</RelationshipId>      </Column>      <Column Name=\"regardingid\" Width=\"150\" FieldId=\"3ff4874b-199d-e611-9bd2-1866da1e4209\" />      <Column Name=\"subject\" Width=\"200\" FieldId=\"38f4874b-199d-e611-9bd2-1866da1e4209\" />    </Columns>  </Layout>";
            //view1.conditionbuilderxml = "<ConditionBuilderQuery  >    <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>    <DisplayName>Task</DisplayName>    <Parental>false</Parental>    <Type>0</Type>    <Rules>      <Condition>AND</Condition>      <Valid>true</Valid>      <Rules>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>d890a5f6-46a5-e811-80dc-0050560502cc</Id>          <Field>d890a5f6-46a5-e811-80dc-0050560502cc</Field>          <Operator>EqualUserId</Operator>          <Data>            <IsDateTime>false</IsDateTime>            <BusinessObjectName>systemuser</BusinessObjectName>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>33f4874b-199d-e611-9bd2-1866da1e4209</Id>          <Field>33f4874b-199d-e611-9bd2-1866da1e4209</Field>          <Operator>Equal</Operator>          <Value>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Value>false</Value>              <ValueType>Value</ValueType>              <IsLookup>false</IsLookup>            </ConditionBuilderRuleValue>          </Value>        </ConditionBuilderRule>      </Rules>    </Rules>    <RelatedBusinessObjects />  </ConditionBuilderQuery>";
            //view1.firstsortfieldid = new Guid("A565B468-4EA0-E611-80D3-0050560502CC");
            //view1.secondsortfieldid = new Guid("16F4874B-199D-E611-9BD2-1866DA1E4209");


            //dbHelper.userDataView.CreateUserDataView(view1.userdataviewid, view1.businessobjectid, view1.owninguserid, view1.name, view1.dataqueryxml, view1.layoutxml, view1.conditionbuilderxml, view1.firstsortfieldid, view1.secondsortfieldid);

            //view1.userdataviewid = new Guid("A2DBD3ED-E342-EC11-A32D-F90A4322A942");
            //view1.businessobjectid = new Guid("B41B53F3-3010-E911-80DC-0050560502CC");
            //view1.owninguserid = _load_Test_User_01Id;
            //view1.name = "DT - My Case notes (Case)";
            //view1.dataqueryxml = "<DataQuery   TableName=\"casecasenote\" TableAlias=\"CASECASENOTE\">    <Filter FilterType=\"And\">      <Conditions>        <Condition TableAlias=\"CASECASENOTE\" Field=\"RESPONSIBLEUSERID\" Operator=\"EqualUserId\" ValueType=\"None\" />        <Condition TableAlias=\"CASECASENOTE\" Field=\"STATUSID\" Operator=\"Equal\" ValueType=\"SingleValue\">          <ConditionValue DbType=\"Int\" IntValue=\"1\" />        </Condition>      </Conditions>    </Filter>    <ApplyFieldLevelSecurity>false</ApplyFieldLevelSecurity>  </DataQuery>";
            //view1.layoutxml = "<Layout  >    <Columns>      <Column Name=\"casenotedate\" Width=\"150\" FieldId=\"691c53f3-3010-e911-80dc-0050560502cc\" />      <Column Name=\"personnumber\" Width=\"150\" FieldId=\"d1d39522-f39a-e811-80dc-0050560502cc\">        <RelationshipId>f602034a-3110-e911-80dc-0050560502cc</RelationshipId>      </Column>      <Column Name=\"caseid\" Width=\"200\" FieldId=\"c4a00b2b-3110-e911-80dc-0050560502cc\" />      <Column Name=\"subject\" Width=\"200\" FieldId=\"ef1b53f3-3010-e911-80dc-0050560502cc\" />    </Columns>  </Layout>";
            //view1.conditionbuilderxml = "<ConditionBuilderQuery  >    <BusinessObjectId>b41b53f3-3010-e911-80dc-0050560502cc</BusinessObjectId>    <DisplayName>Case Note (For Case)</DisplayName>    <Parental>false</Parental>    <Type>0</Type>    <Rules>      <Condition>AND</Condition>      <Valid>true</Valid>      <Rules>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>4bb23f31-3210-e911-80dc-0050560502cc</Id>          <Field>4bb23f31-3210-e911-80dc-0050560502cc</Field>          <Operator>EqualUserId</Operator>          <Data>            <IsDateTime>false</IsDateTime>            <BusinessObjectName>systemuser</BusinessObjectName>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>701c53f3-3010-e911-80dc-0050560502cc</Id>          <Field>701c53f3-3010-e911-80dc-0050560502cc</Field>          <Operator>Equal</Operator>          <Value>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>Open</Label>              <Value>1</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>          </Value>          <Data>            <IsDateTime>false</IsDateTime>            <OptionSetId>c6d06592-eca9-e811-80dc-0050560502cc</OptionSetId>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>      </Rules>    </Rules>    <RelatedBusinessObjects />  </ConditionBuilderQuery>";
            //view1.firstsortfieldid = new Guid("691C53F3-3010-E911-80DC-0050560502CC");
            //view1.secondsortfieldid = null;


            //dbHelper.userDataView.CreateUserDataView(view1.userdataviewid, view1.businessobjectid, view1.owninguserid, view1.name, view1.dataqueryxml, view1.layoutxml, view1.conditionbuilderxml, view1.firstsortfieldid, view1.secondsortfieldid);

            //view1.userdataviewid = new Guid("6CCC9E38-E442-EC11-A32D-F90A4322A942");
            //view1.businessobjectid = new Guid("06E5659D-EAA9-E811-80DC-0050560502CC");
            //view1.owninguserid = _load_Test_User_01Id;
            //view1.name = "DT - My Case Notes (Person)";
            //view1.dataqueryxml = "<DataQuery   TableName=\"personcasenote\" TableAlias=\"PERSONCASENOTE\">    <Filter FilterType=\"And\">      <Conditions>        <Condition TableAlias=\"PERSONCASENOTE\" Field=\"RESPONSIBLEUSERID\" Operator=\"EqualUserId\" ValueType=\"None\" />        <Condition TableAlias=\"PERSONCASENOTE\" Field=\"STATUSID\" Operator=\"Equal\" ValueType=\"SingleValue\">          <ConditionValue DbType=\"Int\" IntValue=\"1\" />        </Condition>      </Conditions>    </Filter>    <ApplyFieldLevelSecurity>false</ApplyFieldLevelSecurity>  </DataQuery>";
            //view1.layoutxml = "<Layout  >    <Columns>      <Column Name=\"casenotedate\" Width=\"110\" FieldId=\"b9c6f8c1-eca9-e811-80dc-0050560502cc\" />      <Column Name=\"personnumber\" Width=\"150\" FieldId=\"d1d39522-f39a-e811-80dc-0050560502cc\">        <RelationshipId>8b838ef2-eaa9-e811-80dc-0050560502cc</RelationshipId>      </Column>      <Column Name=\"personid\" Width=\"200\" FieldId=\"85838ef2-eaa9-e811-80dc-0050560502cc\" />      <Column Name=\"subject\" Width=\"200\" FieldId=\"58e5659d-eaa9-e811-80dc-0050560502cc\" />    </Columns>  </Layout>";
            //view1.conditionbuilderxml = "<ConditionBuilderQuery  >    <BusinessObjectId>06e5659d-eaa9-e811-80dc-0050560502cc</BusinessObjectId>    <DisplayName>Person Case Note</DisplayName>    <Parental>false</Parental>    <Type>0</Type>    <Rules>      <Condition>AND</Condition>      <Valid>true</Valid>      <Rules>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>44e5659d-eaa9-e811-80dc-0050560502cc</Id>          <Field>44e5659d-eaa9-e811-80dc-0050560502cc</Field>          <Operator>EqualUserId</Operator>          <Data>            <IsDateTime>false</IsDateTime>            <BusinessObjectName>systemuser</BusinessObjectName>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>7c4b61d5-eca9-e811-80dc-0050560502cc</Id>          <Field>7c4b61d5-eca9-e811-80dc-0050560502cc</Field>          <Operator>Equal</Operator>          <Value>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>Open</Label>              <Value>1</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>          </Value>          <Data>            <IsDateTime>false</IsDateTime>            <OptionSetId>c6d06592-eca9-e811-80dc-0050560502cc</OptionSetId>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>      </Rules>    </Rules>    <RelatedBusinessObjects />  </ConditionBuilderQuery>";
            //view1.firstsortfieldid = new Guid("B9C6F8C1-ECA9-E811-80DC-0050560502CC");
            //view1.secondsortfieldid = null;


            //dbHelper.userDataView.CreateUserDataView(view1.userdataviewid, view1.businessobjectid, view1.owninguserid, view1.name, view1.dataqueryxml, view1.layoutxml, view1.conditionbuilderxml, view1.firstsortfieldid, view1.secondsortfieldid);

            //view1.userdataviewid = new Guid("7E6D41A1-E642-EC11-A32D-F90A4322A942");
            //view1.businessobjectid = new Guid("B7BF3CE1-A689-E811-9BFC-1866DA1E4209");
            //view1.owninguserid = _load_Test_User_01Id;
            //view1.name = "DT My Letters Activities";
            //view1.dataqueryxml = "<DataQuery   TableName=\"letter\" TableAlias=\"LETTER\">    <Filter FilterType=\"And\">      <Conditions>        <Condition TableAlias=\"LETTER\" Field=\"RESPONSIBLEUSERID\" Operator=\"EqualUserId\" ValueType=\"None\" />        <Condition TableAlias=\"LETTER\" Field=\"STATUSID\" Operator=\"Equal\" ValueType=\"SingleValue\">          <ConditionValue DbType=\"Int\" IntValue=\"1\" />        </Condition>      </Conditions>    </Filter>    <UnionQueries />    <ApplyFieldLevelSecurity>false</ApplyFieldLevelSecurity>  </DataQuery>";
            //view1.layoutxml = "<Layout  >    <Columns>      <Column Name=\"letterdate\" Width=\"150\" FieldId=\"9982edfc-53a1-e811-80dc-0050560502cc\" />      <Column Name=\"personnumber\" Width=\"150\" FieldId=\"d1d39522-f39a-e811-80dc-0050560502cc\">        <RelationshipId>ca23d13f-47a1-e811-80dc-0050560502cc</RelationshipId>      </Column>      <Column Name=\"regardingid\" Width=\"150\" FieldId=\"eebf3ce1-a689-e811-9bfc-1866da1e4209\" />    </Columns>  </Layout>";
            //view1.conditionbuilderxml = "<ConditionBuilderQuery  >    <BusinessObjectId>b7bf3ce1-a689-e811-9bfc-1866da1e4209</BusinessObjectId>    <DisplayName>Letter</DisplayName>    <Parental>false</Parental>    <Type>0</Type>    <Rules>      <Condition>AND</Condition>      <Valid>true</Valid>      <Rules>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>d6dcb5d4-41a1-e811-80dc-0050560502cc</Id>          <Field>d6dcb5d4-41a1-e811-80dc-0050560502cc</Field>          <Operator>EqualUserId</Operator>          <Data>            <IsDateTime>false</IsDateTime>            <BusinessObjectName>systemuser</BusinessObjectName>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>f3c65ef5-49a1-e811-80dc-0050560502cc</Id>          <Field>f3c65ef5-49a1-e811-80dc-0050560502cc</Field>          <Operator>Equal</Operator>          <Value>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>In Progress</Label>              <Value>1</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>          </Value>          <Data>            <IsDateTime>false</IsDateTime>            <OptionSetId>aaffee84-49a1-e811-80dc-0050560502cc</OptionSetId>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>      </Rules>    </Rules>    <RelatedBusinessObjects />  </ConditionBuilderQuery>";
            //view1.firstsortfieldid = new Guid("9982EDFC-53A1-E811-80DC-0050560502CC");
            //view1.secondsortfieldid = null;


            //dbHelper.userDataView.CreateUserDataView(view1.userdataviewid, view1.businessobjectid, view1.owninguserid, view1.name, view1.dataqueryxml, view1.layoutxml, view1.conditionbuilderxml, view1.firstsortfieldid, view1.secondsortfieldid);

            //view1.userdataviewid = new Guid("27E1BED7-E642-EC11-A32D-F90A4322A942");
            //view1.businessobjectid = new Guid("4F2F70BE-4ED9-E411-9691-005056C00008");
            //view1.owninguserid = _load_Test_User_01Id;
            //view1.name = "DT - My Emails";
            //view1.dataqueryxml = "<DataQuery   TableName=\"email\" TableAlias=\"EMAIL\">    <Filter FilterType=\"And\">      <Conditions>        <Condition TableAlias=\"EMAIL\" Field=\"RESPONSIBLEUSERID\" Operator=\"EqualUserId\" ValueType=\"None\" />        <Condition TableAlias=\"EMAIL\" Field=\"STATUSID\" Operator=\"In\" ValueType=\"MultipleValues\">          <ConditionValues>            <ConditionValue DbType=\"Int\" IntValue=\"1\" />            <ConditionValue DbType=\"Int\" IntValue=\"3\" />            <ConditionValue DbType=\"Int\" IntValue=\"4\" />          </ConditionValues>        </Condition>      </Conditions>    </Filter>    <UnionQueries />    <ApplyFieldLevelSecurity>false</ApplyFieldLevelSecurity>  </DataQuery>";
            //view1.layoutxml = "<Layout  >    <Columns>      <Column Name=\"duedate\" Width=\"150\" FieldId=\"e7af7eb1-83c8-e811-80dc-0050560502cc\" />      <Column Name=\"personnumber\" Width=\"150\" FieldId=\"d1d39522-f39a-e811-80dc-0050560502cc\">        <RelationshipId>10304bb2-f0c7-e811-80dc-0050560502cc</RelationshipId>      </Column>      <Column Name=\"regardingid\" Width=\"150\" FieldId=\"832f70be-4ed9-e411-9691-005056c00008\" />    </Columns>  </Layout>";
            //view1.conditionbuilderxml = "<ConditionBuilderQuery  >    <BusinessObjectId>4f2f70be-4ed9-e411-9691-005056c00008</BusinessObjectId>    <DisplayName>Email</DisplayName>    <Parental>false</Parental>    <Type>0</Type>    <Rules>      <Condition>AND</Condition>      <Valid>true</Valid>      <Rules>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>7bc3ef18-83c8-e811-80dc-0050560502cc</Id>          <Field>7bc3ef18-83c8-e811-80dc-0050560502cc</Field>          <Operator>EqualUserId</Operator>          <Data>            <IsDateTime>false</IsDateTime>            <BusinessObjectName>systemuser</BusinessObjectName>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>2f8ef224-4fd9-e411-9691-005056c00008</Id>          <Field>2f8ef224-4fd9-e411-9691-005056c00008</Field>          <Operator>In</Operator>          <Value>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>Draft</Label>              <Value>1</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>Failed to Send</Label>              <Value>3</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>Pending Send</Label>              <Value>4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>          </Value>          <Data>            <IsDateTime>false</IsDateTime>            <OptionSetId>9473dff3-4af5-e311-9e4e-f8b156af4f99</OptionSetId>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>      </Rules>    </Rules>    <RelatedBusinessObjects />  </ConditionBuilderQuery>";
            //view1.firstsortfieldid = new Guid("E7AF7EB1-83C8-E811-80DC-0050560502CC");
            //view1.secondsortfieldid = new Guid("612F70BE-4ED9-E411-9691-005056C00008");


            //dbHelper.userDataView.CreateUserDataView(view1.userdataviewid, view1.businessobjectid, view1.owninguserid, view1.name, view1.dataqueryxml, view1.layoutxml, view1.conditionbuilderxml, view1.firstsortfieldid, view1.secondsortfieldid);

            //view1.userdataviewid = new Guid("31C04011-E742-EC11-A32D-F90A4322A942");
            //view1.businessobjectid = new Guid("3762BC3A-D8A4-E611-80D3-0050560502CC");
            //view1.owninguserid = _load_Test_User_01Id;
            //view1.name = "DT - My Appointments";
            //view1.dataqueryxml = "<DataQuery   TableName=\"appointment\" TableAlias=\"APPOINTMENT\">    <Filter FilterType=\"And\">      <Conditions>        <Condition TableAlias=\"APPOINTMENT\" Field=\"RESPONSIBLEUSERID\" Operator=\"EqualUserId\" ValueType=\"None\" />        <Condition TableAlias=\"APPOINTMENT\" Field=\"STATUSID\" Operator=\"Equal\" ValueType=\"SingleValue\">          <ConditionValue DbType=\"Int\" IntValue=\"4\" />        </Condition>      </Conditions>    </Filter>    <ApplyFieldLevelSecurity>false</ApplyFieldLevelSecurity>  </DataQuery>";
            //view1.layoutxml = "<Layout  >    <Columns>      <Column Name=\"startdate\" Width=\"150\" FieldId=\"c0c17eb2-a9cc-e811-80dc-0050560502cc\" />      <Column Name=\"personnumber\" Width=\"150\" FieldId=\"d1d39522-f39a-e811-80dc-0050560502cc\">        <RelationshipId>6f6075b9-08a6-e611-80d3-0050560502cc</RelationshipId>      </Column>      <Column Name=\"regardingid\" Width=\"200\" FieldId=\"7262bc3a-d8a4-e611-80d3-0050560502cc\" />    </Columns>  </Layout>";
            //view1.conditionbuilderxml = "<ConditionBuilderQuery  >    <BusinessObjectId>3762bc3a-d8a4-e611-80d3-0050560502cc</BusinessObjectId>    <DisplayName>Appointment</DisplayName>    <Parental>false</Parental>    <Type>0</Type>    <Rules>      <Condition>AND</Condition>      <Valid>true</Valid>      <Rules>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>0d22ebd3-ebca-e811-80dc-0050560502cc</Id>          <Field>0d22ebd3-ebca-e811-80dc-0050560502cc</Field>          <Operator>EqualUserId</Operator>          <Data>            <IsDateTime>false</IsDateTime>            <BusinessObjectName>systemuser</BusinessObjectName>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>9a62bc3a-d8a4-e611-80d3-0050560502cc</Id>          <Field>9a62bc3a-d8a4-e611-80d3-0050560502cc</Field>          <Operator>Equal</Operator>          <Value>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>Scheduled</Label>              <Value>4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>          </Value>          <Data>            <IsDateTime>false</IsDateTime>            <OptionSetId>984d5825-fbab-e611-80d3-0050560502cc</OptionSetId>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>      </Rules>    </Rules>    <RelatedBusinessObjects />  </ConditionBuilderQuery>";
            //view1.firstsortfieldid = new Guid("C0C17EB2-A9CC-E811-80DC-0050560502CC");
            //view1.secondsortfieldid = null;

            //dbHelper.userDataView.CreateUserDataView(view1.userdataviewid, view1.businessobjectid, view1.owninguserid, view1.name, view1.dataqueryxml, view1.layoutxml, view1.conditionbuilderxml, view1.firstsortfieldid, view1.secondsortfieldid);

            //view1.userdataviewid = new Guid("FC2C334C-ED42-EC11-A32D-F90A4322A942");
            //view1.businessobjectid = new Guid("5C9198F4-A6C1-E811-9C04-1866DA1E3BDA");
            //view1.owninguserid = _load_Test_User_01Id;
            //view1.name = "DT - My Involvements";
            //view1.dataqueryxml = "<DataQuery   TableName=\"caseinvolvement\" TableAlias=\"CASEINVOLVEMENT\">    <Filter FilterType=\"And\">      <Conditions>        <Condition TableAlias=\"CASEINVOLVEMENT\" Field=\"ENDDATE\" Operator=\"Null\" ValueType=\"None\" />        <Condition TableAlias=\"CASEINVOLVEMENT\" Field=\"INVOLVEMENTMEMBERID\" Operator=\"EqualUserId\" ValueType=\"None\" />      </Conditions>    </Filter>    <UnionQueries />    <ApplyFieldLevelSecurity>false</ApplyFieldLevelSecurity>  </DataQuery>";
            //view1.layoutxml = "<Layout  >    <Columns>      <Column Name=\"startdate\" Width=\"110\" FieldId=\"c2b390fa-a6c1-e811-9c04-1866da1e3bda\" />      <Column Name=\"caseid\" Width=\"200\" FieldId=\"c8983ebc-0cc3-e811-80dc-0050560502cc\" />      <Column Name=\"involvementreasonid\" Width=\"300\" FieldId=\"ec9198f4-a6c1-e811-9c04-1866da1e3bda\" />    </Columns>  </Layout>";
            //view1.conditionbuilderxml = "<ConditionBuilderQuery  >    <BusinessObjectId>5c9198f4-a6c1-e811-9c04-1866da1e3bda</BusinessObjectId>    <DisplayName>Case Involvement</DisplayName>    <Parental>false</Parental>    <Type>0</Type>    <Rules>      <Condition>AND</Condition>      <Valid>true</Valid>      <Rules>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>cab390fa-a6c1-e811-9c04-1866da1e3bda</Id>          <Field>cab390fa-a6c1-e811-9c04-1866da1e3bda</Field>          <Operator>Null</Operator>          <Data>            <IsDateTime>true</IsDateTime>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>c69198f4-a6c1-e811-9c04-1866da1e3bda</Id>          <Field>c69198f4-a6c1-e811-9c04-1866da1e3bda</Field>          <Operator>EqualUserId</Operator>          <Data>            <IsDateTime>false</IsDateTime>            <BusinessObjectName>caseinvolvement</BusinessObjectName>            <MultiLookupFieldName>involvementmemberid</MultiLookupFieldName>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>      </Rules>    </Rules>    <RelatedBusinessObjects />  </ConditionBuilderQuery>";
            //view1.firstsortfieldid = new Guid("C2B390FA-A6C1-E811-9C04-1866DA1E3BDA");
            //view1.secondsortfieldid = null;

            //dbHelper.userDataView.CreateUserDataView(view1.userdataviewid, view1.businessobjectid, view1.owninguserid, view1.name, view1.dataqueryxml, view1.layoutxml, view1.conditionbuilderxml, view1.firstsortfieldid, view1.secondsortfieldid);

            //view1.userdataviewid = new Guid("E907953D-EE42-EC11-A32D-F90A4322A942");
            //view1.businessobjectid = new Guid("1143C28D-FCBF-E811-9C04-1866DA1E3BDA");
            //view1.owninguserid = _load_Test_User_01Id;
            //view1.name = "DT - Forms Awaiting Allocation (specific Form types)";
            //view1.dataqueryxml = "<DataQuery   TableName=\"caseform\" TableAlias=\"CASEFORM\">    <Filter FilterType=\"And\">      <Conditions>        <Condition TableAlias=\"CASEFORM\" Field=\"DOCUMENTID\" Operator=\"In\" ValueType=\"MultipleValues\">          <ConditionValues>            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"57386dbd-a60b-4c4d-bea8-354633c5191e\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"d92feb2b-cf7f-4050-969d-418617eeaf51\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"6ffa9fbd-c2ea-487a-8612-b33ccf31af10\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"8a7c877a-1d5f-ea11-a2cb-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"64f136ed-b55f-ea11-a2cb-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"7d39da59-d441-ea11-a2c8-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"13f7f76b-b0cd-ea11-a2cd-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"63e23ca0-74d5-ea11-a2cd-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"7ae8feb2-980f-ea11-a2c8-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"67b9ef8b-f51f-ea11-a2c8-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"cf8a8203-3589-e911-a2c5-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"b7a550db-f061-e911-a2c5-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"ee7e883b-bf98-e911-a2c6-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"7f0bd08f-7367-e911-a82d-000d3a0b666b\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"0102ae66-c269-e911-a82f-000d3a0b666b\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"3e7dac5f-0549-ea11-a853-000d3a0cc781\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"8727bc45-a349-ea11-a854-000d3a0cc781\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"85e25293-c149-ea11-a854-000d3a0cc781\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"c1b2dea9-7a4a-ea11-a855-000d3a0cc781\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"f965277b-7e4a-ea11-a855-000d3a0cc781\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"42da9439-814a-ea11-a855-000d3a0cc781\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"49935212-554b-ea11-a856-000d3a0cc781\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"7dba361f-3dc7-ea11-a8bd-000d3a0cc781\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"914a321f-b729-eb11-a905-000d3a0cc781\" />          </ConditionValues>        </Condition>        <Condition TableAlias=\"CASEFORM\" Field=\"ASSESSMENTSTATUSID\" Operator=\"In\" ValueType=\"MultipleValues\">          <ConditionValues>            <ConditionValue DbType=\"Int\" IntValue=\"4\" />            <ConditionValue DbType=\"Int\" IntValue=\"1\" />          </ConditionValues>        </Condition>        <Condition TableAlias=\"CASEFORM\" Field=\"OWNERID\" Operator=\"Equal\" ValueType=\"SingleValue\">          <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />        </Condition>        <Condition TableAlias=\"CASEFORM\" Field=\"RESPONSIBLEUSERID\" Operator=\"Null\" ValueType=\"None\" />      </Conditions>    </Filter>    <ApplyFieldLevelSecurity>false</ApplyFieldLevelSecurity>  </DataQuery>";
            //view1.layoutxml = "<Layout  >    <Columns>      <Column Name=\"startdate\" Width=\"110\" FieldId=\"9543c28d-fcbf-e811-9c04-1866da1e3bda\" />      <Column Name=\"responsibleuserid\" Width=\"150\" FieldId=\"9d027e07-09c0-e811-80dc-0050560502cc\" />      <Column Name=\"personnumber\" Width=\"150\" FieldId=\"d1d39522-f39a-e811-80dc-0050560502cc\">        <RelationshipId>135ab1a3-08c0-e811-80dc-0050560502cc</RelationshipId>      </Column>      <Column Name=\"addressboroughid\" Width=\"150\" FieldId=\"11d68963-f99a-e811-80dc-0050560502cc\">        <RelationshipId>135ab1a3-08c0-e811-80dc-0050560502cc</RelationshipId>      </Column>      <Column Name=\"personid\" Width=\"150\" FieldId=\"0d5ab1a3-08c0-e811-80dc-0050560502cc\" />      <Column Name=\"casenumber\" Width=\"150\" FieldId=\"4a743b13-eac6-e811-80dc-0050560502cc\">        <RelationshipId>ecababbc-08c0-e811-80dc-0050560502cc</RelationshipId>      </Column>      <Column Name=\"documentid\" Width=\"200\" FieldId=\"8143c28d-fcbf-e811-9c04-1866da1e3bda\" />      <Column Name=\"assessmentstatusid\" Width=\"150\" FieldId=\"8b43c28d-fcbf-e811-9c04-1866da1e3bda\" />      <Column Name=\"duedate\" Width=\"150\" FieldId=\"9d43c28d-fcbf-e811-9c04-1866da1e3bda\" />      <Column Name=\"ownerid\" Width=\"200\" FieldId=\"4543c28d-fcbf-e811-9c04-1866da1e3bda\" />    </Columns>  </Layout>";
            //view1.conditionbuilderxml = "<ConditionBuilderQuery  >    <BusinessObjectId>1143c28d-fcbf-e811-9c04-1866da1e3bda</BusinessObjectId>    <DisplayName>Form (Case)</DisplayName>    <Parental>false</Parental>    <Type>0</Type>    <Rules>      <Condition>AND</Condition>      <Valid>true</Valid>      <Rules>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>8143c28d-fcbf-e811-9c04-1866da1e3bda</Id>          <Field>8143c28d-fcbf-e811-9c04-1866da1e3bda</Field>          <Operator>In</Operator>          <Value>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>Sample Care Assessment</Label>              <Value>57386dbd-a60b-4c4d-bea8-354633c5191e</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>DT Test Document 2 (2nd version)</Label>              <Value>d92feb2b-cf7f-4050-969d-418617eeaf51</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>MOR Training Business Rules</Label>              <Value>6ffa9fbd-c2ea-487a-8612-b33ccf31af10</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>Adult Care and Support Plan</Label>              <Value>8a7c877a-1d5f-ea11-a2cb-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>Hants Example Document</Label>              <Value>64f136ed-b55f-ea11-a2cb-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>DT Mobile Test Form</Label>              <Value>7d39da59-d441-ea11-a2c8-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>CMS example DT</Label>              <Value>13f7f76b-b0cd-ea11-a2cd-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>DT Test 4</Label>              <Value>63e23ca0-74d5-ea11-a2cd-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>Swindon Skip</Label>              <Value>7ae8feb2-980f-ea11-a2c8-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>Home Visit Record</Label>              <Value>67b9ef8b-f51f-ea11-a2c8-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>DT Test Document</Label>              <Value>cf8a8203-3589-e911-a2c5-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>SCC - Adult Care and Support Plan</Label>              <Value>b7a550db-f061-e911-a2c5-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>DT Test Document 2</Label>              <Value>ee7e883b-bf98-e911-a2c6-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>MOR Training</Label>              <Value>7f0bd08f-7367-e911-a82d-000d3a0b666b</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>MOR Training SDE Case</Label>              <Value>0102ae66-c269-e911-a82f-000d3a0b666b</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>CDA - Care and Support Plan</Label>              <Value>3e7dac5f-0549-ea11-a853-000d3a0cc781</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>CDA - Draft Support Plan</Label>              <Value>8727bc45-a349-ea11-a854-000d3a0cc781</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>CDC -  Looked After Child Care Plan</Label>              <Value>85e25293-c149-ea11-a854-000d3a0cc781</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>CDC - Section 47 Report CP2</Label>              <Value>c1b2dea9-7a4a-ea11-a855-000d3a0cc781</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>CDC - Strategy Discussion/Meeting Doc CP1</Label>              <Value>f965277b-7e4a-ea11-a855-000d3a0cc781</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>CDC - Child In Need Care Plan</Label>              <Value>42da9439-814a-ea11-a855-000d3a0cc781</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>CDA - Care Act Assessment</Label>              <Value>49935212-554b-ea11-a856-000d3a0cc781</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>Dave UAT 2</Label>              <Value>7dba361f-3dc7-ea11-a8bd-000d3a0cc781</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>DT Test Document X</Label>              <Value>914a321f-b729-eb11-a905-000d3a0cc781</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>          </Value>          <Data>            <IsDateTime>false</IsDateTime>            <BusinessObjectName>document</BusinessObjectName>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>8b43c28d-fcbf-e811-9c04-1866da1e3bda</Id>          <Field>8b43c28d-fcbf-e811-9c04-1866da1e3bda</Field>          <Operator>In</Operator>          <Value>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>Not Initialized</Label>              <Value>4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>In Progress</Label>              <Value>1</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>          </Value>          <Data>            <IsDateTime>false</IsDateTime>            <OptionSetId>30ac28b0-c855-e311-b694-1803731f3ee3</OptionSetId>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>4543c28d-fcbf-e811-9c04-1866da1e3bda</Id>          <Field>4543c28d-fcbf-e811-9c04-1866da1e3bda</Field>          <Operator>Equal</Operator>          <Value>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>CareDirector QA</Label>              <Value>b6060dfa-7333-43b2-a662-3d9cadab12e5</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>          </Value>          <Data>            <IsDateTime>false</IsDateTime>            <BusinessObjectName>team</BusinessObjectName>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>9d027e07-09c0-e811-80dc-0050560502cc</Id>          <Field>9d027e07-09c0-e811-80dc-0050560502cc</Field>          <Operator>Null</Operator>          <Data>            <IsDateTime>false</IsDateTime>            <BusinessObjectName>systemuser</BusinessObjectName>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>      </Rules>    </Rules>    <RelatedBusinessObjects />  </ConditionBuilderQuery>";
            //view1.firstsortfieldid = new Guid("9D43C28D-FCBF-E811-9C04-1866DA1E3BDA");
            //view1.secondsortfieldid = new Guid("9543C28D-FCBF-E811-9C04-1866DA1E3BDA");


            //dbHelper.userDataView.CreateUserDataView(view1.userdataviewid, view1.businessobjectid, view1.owninguserid, view1.name, view1.dataqueryxml, view1.layoutxml, view1.conditionbuilderxml, view1.firstsortfieldid, view1.secondsortfieldid);

            //view1.userdataviewid = new Guid("7D40B059-EE42-EC11-A32D-F90A4322A942");
            //view1.businessobjectid = new Guid("1143C28D-FCBF-E811-9C04-1866DA1E3BDA");
            //view1.owninguserid = _load_Test_User_01Id;
            //view1.name = "DT - Forms Allocated to me (Specific Types)";
            //view1.dataqueryxml = "<DataQuery   TableName=\"caseform\" TableAlias=\"CASEFORM\">    <Filter FilterType=\"And\">      <Conditions>        <Condition TableAlias=\"CASEFORM\" Field=\"DOCUMENTID\" Operator=\"In\" ValueType=\"MultipleValues\">          <ConditionValues>            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"57386dbd-a60b-4c4d-bea8-354633c5191e\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"d92feb2b-cf7f-4050-969d-418617eeaf51\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"6ffa9fbd-c2ea-487a-8612-b33ccf31af10\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"8a7c877a-1d5f-ea11-a2cb-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"64f136ed-b55f-ea11-a2cb-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"7d39da59-d441-ea11-a2c8-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"13f7f76b-b0cd-ea11-a2cd-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"63e23ca0-74d5-ea11-a2cd-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"7ae8feb2-980f-ea11-a2c8-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"67b9ef8b-f51f-ea11-a2c8-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"cf8a8203-3589-e911-a2c5-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"b7a550db-f061-e911-a2c5-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"ee7e883b-bf98-e911-a2c6-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"7f0bd08f-7367-e911-a82d-000d3a0b666b\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"0102ae66-c269-e911-a82f-000d3a0b666b\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"3e7dac5f-0549-ea11-a853-000d3a0cc781\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"8727bc45-a349-ea11-a854-000d3a0cc781\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"85e25293-c149-ea11-a854-000d3a0cc781\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"c1b2dea9-7a4a-ea11-a855-000d3a0cc781\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"f965277b-7e4a-ea11-a855-000d3a0cc781\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"42da9439-814a-ea11-a855-000d3a0cc781\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"49935212-554b-ea11-a856-000d3a0cc781\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"7dba361f-3dc7-ea11-a8bd-000d3a0cc781\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"914a321f-b729-eb11-a905-000d3a0cc781\" />          </ConditionValues>        </Condition>        <Condition TableAlias=\"CASEFORM\" Field=\"ASSESSMENTSTATUSID\" Operator=\"In\" ValueType=\"MultipleValues\">          <ConditionValues>            <ConditionValue DbType=\"Int\" IntValue=\"4\" />            <ConditionValue DbType=\"Int\" IntValue=\"1\" />          </ConditionValues>        </Condition>        <Condition TableAlias=\"CASEFORM\" Field=\"OWNERID\" Operator=\"Equal\" ValueType=\"SingleValue\">          <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />        </Condition>        <Condition TableAlias=\"CASEFORM\" Field=\"RESPONSIBLEUSERID\" Operator=\"Null\" ValueType=\"None\" />      </Conditions>    </Filter>    <ApplyFieldLevelSecurity>false</ApplyFieldLevelSecurity>  </DataQuery>";
            //view1.layoutxml = "<Layout  >    <Columns>      <Column Name=\"startdate\" Width=\"110\" FieldId=\"9543c28d-fcbf-e811-9c04-1866da1e3bda\" />      <Column Name=\"responsibleuserid\" Width=\"150\" FieldId=\"9d027e07-09c0-e811-80dc-0050560502cc\" />      <Column Name=\"personnumber\" Width=\"150\" FieldId=\"d1d39522-f39a-e811-80dc-0050560502cc\">        <RelationshipId>135ab1a3-08c0-e811-80dc-0050560502cc</RelationshipId>      </Column>      <Column Name=\"addressboroughid\" Width=\"150\" FieldId=\"11d68963-f99a-e811-80dc-0050560502cc\">        <RelationshipId>135ab1a3-08c0-e811-80dc-0050560502cc</RelationshipId>      </Column>      <Column Name=\"personid\" Width=\"150\" FieldId=\"0d5ab1a3-08c0-e811-80dc-0050560502cc\" />      <Column Name=\"casenumber\" Width=\"150\" FieldId=\"4a743b13-eac6-e811-80dc-0050560502cc\">        <RelationshipId>ecababbc-08c0-e811-80dc-0050560502cc</RelationshipId>      </Column>      <Column Name=\"documentid\" Width=\"200\" FieldId=\"8143c28d-fcbf-e811-9c04-1866da1e3bda\" />      <Column Name=\"assessmentstatusid\" Width=\"150\" FieldId=\"8b43c28d-fcbf-e811-9c04-1866da1e3bda\" />      <Column Name=\"duedate\" Width=\"150\" FieldId=\"9d43c28d-fcbf-e811-9c04-1866da1e3bda\" />      <Column Name=\"ownerid\" Width=\"200\" FieldId=\"4543c28d-fcbf-e811-9c04-1866da1e3bda\" />    </Columns>  </Layout>";
            //view1.conditionbuilderxml = "<ConditionBuilderQuery  >    <BusinessObjectId>1143c28d-fcbf-e811-9c04-1866da1e3bda</BusinessObjectId>    <DisplayName>Form (Case)</DisplayName>    <Parental>false</Parental>    <Type>0</Type>    <Rules>      <Condition>AND</Condition>      <Valid>true</Valid>      <Rules>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>8143c28d-fcbf-e811-9c04-1866da1e3bda</Id>          <Field>8143c28d-fcbf-e811-9c04-1866da1e3bda</Field>          <Operator>In</Operator>          <Value>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>Sample Care Assessment</Label>              <Value>57386dbd-a60b-4c4d-bea8-354633c5191e</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>DT Test Document 2 (2nd version)</Label>              <Value>d92feb2b-cf7f-4050-969d-418617eeaf51</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>MOR Training Business Rules</Label>              <Value>6ffa9fbd-c2ea-487a-8612-b33ccf31af10</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>Adult Care and Support Plan</Label>              <Value>8a7c877a-1d5f-ea11-a2cb-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>Hants Example Document</Label>              <Value>64f136ed-b55f-ea11-a2cb-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>DT Mobile Test Form</Label>              <Value>7d39da59-d441-ea11-a2c8-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>CMS example DT</Label>              <Value>13f7f76b-b0cd-ea11-a2cd-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>DT Test 4</Label>              <Value>63e23ca0-74d5-ea11-a2cd-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>Swindon Skip</Label>              <Value>7ae8feb2-980f-ea11-a2c8-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>Home Visit Record</Label>              <Value>67b9ef8b-f51f-ea11-a2c8-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>DT Test Document</Label>              <Value>cf8a8203-3589-e911-a2c5-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>SCC - Adult Care and Support Plan</Label>              <Value>b7a550db-f061-e911-a2c5-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>DT Test Document 2</Label>              <Value>ee7e883b-bf98-e911-a2c6-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>MOR Training</Label>              <Value>7f0bd08f-7367-e911-a82d-000d3a0b666b</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>MOR Training SDE Case</Label>              <Value>0102ae66-c269-e911-a82f-000d3a0b666b</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>CDA - Care and Support Plan</Label>              <Value>3e7dac5f-0549-ea11-a853-000d3a0cc781</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>CDA - Draft Support Plan</Label>              <Value>8727bc45-a349-ea11-a854-000d3a0cc781</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>CDC -  Looked After Child Care Plan</Label>              <Value>85e25293-c149-ea11-a854-000d3a0cc781</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>CDC - Section 47 Report CP2</Label>              <Value>c1b2dea9-7a4a-ea11-a855-000d3a0cc781</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>CDC - Strategy Discussion/Meeting Doc CP1</Label>              <Value>f965277b-7e4a-ea11-a855-000d3a0cc781</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>CDC - Child In Need Care Plan</Label>              <Value>42da9439-814a-ea11-a855-000d3a0cc781</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>CDA - Care Act Assessment</Label>              <Value>49935212-554b-ea11-a856-000d3a0cc781</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>Dave UAT 2</Label>              <Value>7dba361f-3dc7-ea11-a8bd-000d3a0cc781</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>DT Test Document X</Label>              <Value>914a321f-b729-eb11-a905-000d3a0cc781</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>          </Value>          <Data>            <IsDateTime>false</IsDateTime>            <BusinessObjectName>document</BusinessObjectName>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>8b43c28d-fcbf-e811-9c04-1866da1e3bda</Id>          <Field>8b43c28d-fcbf-e811-9c04-1866da1e3bda</Field>          <Operator>In</Operator>          <Value>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>Not Initialized</Label>              <Value>4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>In Progress</Label>              <Value>1</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>          </Value>          <Data>            <IsDateTime>false</IsDateTime>            <OptionSetId>30ac28b0-c855-e311-b694-1803731f3ee3</OptionSetId>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>4543c28d-fcbf-e811-9c04-1866da1e3bda</Id>          <Field>4543c28d-fcbf-e811-9c04-1866da1e3bda</Field>          <Operator>Equal</Operator>          <Value>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>CareDirector QA</Label>              <Value>b6060dfa-7333-43b2-a662-3d9cadab12e5</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>          </Value>          <Data>            <IsDateTime>false</IsDateTime>            <BusinessObjectName>team</BusinessObjectName>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>9d027e07-09c0-e811-80dc-0050560502cc</Id>          <Field>9d027e07-09c0-e811-80dc-0050560502cc</Field>          <Operator>Null</Operator>          <Data>            <IsDateTime>false</IsDateTime>            <BusinessObjectName>systemuser</BusinessObjectName>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>      </Rules>    </Rules>    <RelatedBusinessObjects />  </ConditionBuilderQuery>";
            //view1.firstsortfieldid = new Guid("9D43C28D-FCBF-E811-9C04-1866DA1E3BDA");
            //view1.secondsortfieldid = new Guid("9543C28D-FCBF-E811-9C04-1866DA1E3BDA");


            //dbHelper.userDataView.CreateUserDataView(view1.userdataviewid, view1.businessobjectid, view1.owninguserid, view1.name, view1.dataqueryxml, view1.layoutxml, view1.conditionbuilderxml, view1.firstsortfieldid, view1.secondsortfieldid);

            //view1.userdataviewid = new Guid("C9C1BA75-EE42-EC11-A32D-F90A4322A942");
            //view1.businessobjectid = new Guid("1143C28D-FCBF-E811-9C04-1866DA1E3BDA");
            //view1.owninguserid = _load_Test_User_01Id;
            //view1.name = "DT - Forms Allocated to My Teams (Specific Types)";
            //view1.dataqueryxml = "<DataQuery   TableName=\"caseform\" TableAlias=\"CASEFORM\">    <Filter FilterType=\"And\">      <Conditions>        <Condition TableAlias=\"CASEFORM\" Field=\"DOCUMENTID\" Operator=\"In\" ValueType=\"MultipleValues\">          <ConditionValues>            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"57386dbd-a60b-4c4d-bea8-354633c5191e\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"d92feb2b-cf7f-4050-969d-418617eeaf51\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"6ffa9fbd-c2ea-487a-8612-b33ccf31af10\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"8a7c877a-1d5f-ea11-a2cb-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"64f136ed-b55f-ea11-a2cb-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"7d39da59-d441-ea11-a2c8-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"13f7f76b-b0cd-ea11-a2cd-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"63e23ca0-74d5-ea11-a2cd-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"7ae8feb2-980f-ea11-a2c8-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"67b9ef8b-f51f-ea11-a2c8-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"cf8a8203-3589-e911-a2c5-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"b7a550db-f061-e911-a2c5-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"ee7e883b-bf98-e911-a2c6-005056926fe4\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"7f0bd08f-7367-e911-a82d-000d3a0b666b\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"0102ae66-c269-e911-a82f-000d3a0b666b\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"3e7dac5f-0549-ea11-a853-000d3a0cc781\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"8727bc45-a349-ea11-a854-000d3a0cc781\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"85e25293-c149-ea11-a854-000d3a0cc781\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"c1b2dea9-7a4a-ea11-a855-000d3a0cc781\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"f965277b-7e4a-ea11-a855-000d3a0cc781\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"42da9439-814a-ea11-a855-000d3a0cc781\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"49935212-554b-ea11-a856-000d3a0cc781\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"7dba361f-3dc7-ea11-a8bd-000d3a0cc781\" />            <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"914a321f-b729-eb11-a905-000d3a0cc781\" />          </ConditionValues>        </Condition>        <Condition TableAlias=\"CASEFORM\" Field=\"ASSESSMENTSTATUSID\" Operator=\"In\" ValueType=\"MultipleValues\">          <ConditionValues>            <ConditionValue DbType=\"Int\" IntValue=\"4\" />            <ConditionValue DbType=\"Int\" IntValue=\"1\" />          </ConditionValues>        </Condition>        <Condition TableAlias=\"CASEFORM\" Field=\"OWNERID\" Operator=\"Equal\" ValueType=\"SingleValue\">          <ConditionValue DbType=\"UniqueIdentifier\" GuidValue=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />        </Condition>        <Condition TableAlias=\"CASEFORM\" Field=\"RESPONSIBLEUSERID\" Operator=\"Null\" ValueType=\"None\" />      </Conditions>    </Filter>    <ApplyFieldLevelSecurity>false</ApplyFieldLevelSecurity>  </DataQuery>";
            //view1.layoutxml = "<Layout  >    <Columns>      <Column Name=\"startdate\" Width=\"110\" FieldId=\"9543c28d-fcbf-e811-9c04-1866da1e3bda\" />      <Column Name=\"responsibleuserid\" Width=\"150\" FieldId=\"9d027e07-09c0-e811-80dc-0050560502cc\" />      <Column Name=\"personnumber\" Width=\"150\" FieldId=\"d1d39522-f39a-e811-80dc-0050560502cc\">        <RelationshipId>135ab1a3-08c0-e811-80dc-0050560502cc</RelationshipId>      </Column>      <Column Name=\"addressboroughid\" Width=\"150\" FieldId=\"11d68963-f99a-e811-80dc-0050560502cc\">        <RelationshipId>135ab1a3-08c0-e811-80dc-0050560502cc</RelationshipId>      </Column>      <Column Name=\"personid\" Width=\"150\" FieldId=\"0d5ab1a3-08c0-e811-80dc-0050560502cc\" />      <Column Name=\"casenumber\" Width=\"150\" FieldId=\"4a743b13-eac6-e811-80dc-0050560502cc\">        <RelationshipId>ecababbc-08c0-e811-80dc-0050560502cc</RelationshipId>      </Column>      <Column Name=\"documentid\" Width=\"200\" FieldId=\"8143c28d-fcbf-e811-9c04-1866da1e3bda\" />      <Column Name=\"assessmentstatusid\" Width=\"150\" FieldId=\"8b43c28d-fcbf-e811-9c04-1866da1e3bda\" />      <Column Name=\"duedate\" Width=\"150\" FieldId=\"9d43c28d-fcbf-e811-9c04-1866da1e3bda\" />      <Column Name=\"ownerid\" Width=\"200\" FieldId=\"4543c28d-fcbf-e811-9c04-1866da1e3bda\" />    </Columns>  </Layout>";
            //view1.conditionbuilderxml = "<ConditionBuilderQuery  >    <BusinessObjectId>1143c28d-fcbf-e811-9c04-1866da1e3bda</BusinessObjectId>    <DisplayName>Form (Case)</DisplayName>    <Parental>false</Parental>    <Type>0</Type>    <Rules>      <Condition>AND</Condition>      <Valid>true</Valid>      <Rules>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>8143c28d-fcbf-e811-9c04-1866da1e3bda</Id>          <Field>8143c28d-fcbf-e811-9c04-1866da1e3bda</Field>          <Operator>In</Operator>          <Value>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>Sample Care Assessment</Label>              <Value>57386dbd-a60b-4c4d-bea8-354633c5191e</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>DT Test Document 2 (2nd version)</Label>              <Value>d92feb2b-cf7f-4050-969d-418617eeaf51</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>MOR Training Business Rules</Label>              <Value>6ffa9fbd-c2ea-487a-8612-b33ccf31af10</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>Adult Care and Support Plan</Label>              <Value>8a7c877a-1d5f-ea11-a2cb-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>Hants Example Document</Label>              <Value>64f136ed-b55f-ea11-a2cb-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>DT Mobile Test Form</Label>              <Value>7d39da59-d441-ea11-a2c8-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>CMS example DT</Label>              <Value>13f7f76b-b0cd-ea11-a2cd-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>DT Test 4</Label>              <Value>63e23ca0-74d5-ea11-a2cd-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>Swindon Skip</Label>              <Value>7ae8feb2-980f-ea11-a2c8-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>Home Visit Record</Label>              <Value>67b9ef8b-f51f-ea11-a2c8-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>DT Test Document</Label>              <Value>cf8a8203-3589-e911-a2c5-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>SCC - Adult Care and Support Plan</Label>              <Value>b7a550db-f061-e911-a2c5-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>DT Test Document 2</Label>              <Value>ee7e883b-bf98-e911-a2c6-005056926fe4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>MOR Training</Label>              <Value>7f0bd08f-7367-e911-a82d-000d3a0b666b</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>MOR Training SDE Case</Label>              <Value>0102ae66-c269-e911-a82f-000d3a0b666b</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>CDA - Care and Support Plan</Label>              <Value>3e7dac5f-0549-ea11-a853-000d3a0cc781</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>CDA - Draft Support Plan</Label>              <Value>8727bc45-a349-ea11-a854-000d3a0cc781</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>CDC -  Looked After Child Care Plan</Label>              <Value>85e25293-c149-ea11-a854-000d3a0cc781</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>CDC - Section 47 Report CP2</Label>              <Value>c1b2dea9-7a4a-ea11-a855-000d3a0cc781</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>CDC - Strategy Discussion/Meeting Doc CP1</Label>              <Value>f965277b-7e4a-ea11-a855-000d3a0cc781</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>CDC - Child In Need Care Plan</Label>              <Value>42da9439-814a-ea11-a855-000d3a0cc781</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>CDA - Care Act Assessment</Label>              <Value>49935212-554b-ea11-a856-000d3a0cc781</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>Dave UAT 2</Label>              <Value>7dba361f-3dc7-ea11-a8bd-000d3a0cc781</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>DT Test Document X</Label>              <Value>914a321f-b729-eb11-a905-000d3a0cc781</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>          </Value>          <Data>            <IsDateTime>false</IsDateTime>            <BusinessObjectName>document</BusinessObjectName>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>8b43c28d-fcbf-e811-9c04-1866da1e3bda</Id>          <Field>8b43c28d-fcbf-e811-9c04-1866da1e3bda</Field>          <Operator>In</Operator>          <Value>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>Not Initialized</Label>              <Value>4</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>In Progress</Label>              <Value>1</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>          </Value>          <Data>            <IsDateTime>false</IsDateTime>            <OptionSetId>30ac28b0-c855-e311-b694-1803731f3ee3</OptionSetId>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>4543c28d-fcbf-e811-9c04-1866da1e3bda</Id>          <Field>4543c28d-fcbf-e811-9c04-1866da1e3bda</Field>          <Operator>Equal</Operator>          <Value>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Label>CareDirector QA</Label>              <Value>b6060dfa-7333-43b2-a662-3d9cadab12e5</Value>              <ValueType>Value</ValueType>              <IsLookup>true</IsLookup>            </ConditionBuilderRuleValue>          </Value>          <Data>            <IsDateTime>false</IsDateTime>            <BusinessObjectName>team</BusinessObjectName>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>9d027e07-09c0-e811-80dc-0050560502cc</Id>          <Field>9d027e07-09c0-e811-80dc-0050560502cc</Field>          <Operator>Null</Operator>          <Data>            <IsDateTime>false</IsDateTime>            <BusinessObjectName>systemuser</BusinessObjectName>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>      </Rules>    </Rules>    <RelatedBusinessObjects />  </ConditionBuilderQuery>";
            //view1.firstsortfieldid = new Guid("9D43C28D-FCBF-E811-9C04-1866DA1E3BDA");
            //view1.secondsortfieldid = new Guid("9543C28D-FCBF-E811-9C04-1866DA1E3BDA");


            //dbHelper.userDataView.CreateUserDataView(view1.userdataviewid, view1.businessobjectid, view1.owninguserid, view1.name, view1.dataqueryxml, view1.layoutxml, view1.conditionbuilderxml, view1.firstsortfieldid, view1.secondsortfieldid);

            //view1.userdataviewid = new Guid("D1441611-FD45-EC11-A32D-F90A4322A942");
            //view1.businessobjectid = new Guid("B1E05CA6-ACB1-E811-80DC-0050560502CC");
            //view1.owninguserid = _load_Test_User_01Id;
            //view1.name = "DT - First Response Contacts in Progress";
            //view1.dataqueryxml = "<DataQuery   TableName=\"contact\" TableAlias=\"CONTACT\">    <Filter FilterType=\"And\">      <Conditions>        <Condition TableAlias=\"CONTACT\" Field=\"INACTIVE\" Operator=\"Equal\" ValueType=\"SingleValue\">          <ConditionValue DbType=\"Bit\" BoolValue=\"false\" />        </Condition>        <Condition TableAlias=\"CONTACT\" Field=\"OWNERID\" Operator=\"EqualsCurrentUserDefaultTeam\" ValueType=\"None\" />      </Conditions>    </Filter>    <UnionQueries />    <ApplyFieldLevelSecurity>false</ApplyFieldLevelSecurity>  </DataQuery>";
            //view1.layoutxml = "<Layout  >    <Columns>      <Column Name=\"contactreceiveddatetime\" Width=\"150\" FieldId=\"7754450a-6267-e911-a2c5-0050569231cf\" />      <Column Name=\"contactstatusid\" Width=\"150\" FieldId=\"d79c0252-6667-e911-a2c5-0050569231cf\" />      <Column Name=\"contactpresentingpriorityid\" Width=\"150\" FieldId=\"18ec3697-6567-e911-a2c5-0050569231cf\" />      <Column Name=\"personid\" Width=\"200\" FieldId=\"2cb7e740-37d8-e811-9bee-989096c9be3d\" />      <Column Name=\"additionalinformation\" Width=\"200\" FieldId=\"7d078802-6667-e911-a2c5-0050569231cf\" />      <Column Name=\"contactreasonid\" Width=\"150\" FieldId=\"aaf0cd7e-6567-e911-a2c5-0050569231cf\" />      <Column Name=\"contactmadebyid\" Width=\"150\" FieldId=\"b131ee5a-6367-e911-a2c5-0050569231cf\" />      <Column Name=\"contactmadebyfreetext\" Width=\"150\" FieldId=\"0b85ef8a-6467-e911-a2c5-0050569231cf\" />      <Column Name=\"referralpriorityid\" Width=\"150\" FieldId=\"fc842c3c-6767-e911-a2c5-0050569231cf\" />    </Columns>  </Layout>";
            //view1.conditionbuilderxml = "<ConditionBuilderQuery  >    <BusinessObjectId>b1e05ca6-acb1-e811-80dc-0050560502cc</BusinessObjectId>    <DisplayName>Contact</DisplayName>    <Parental>false</Parental>    <Type>0</Type>    <Rules>      <Condition>AND</Condition>      <Valid>true</Valid>      <Rules>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>03e15ca6-acb1-e811-80dc-0050560502cc</Id>          <Field>03e15ca6-acb1-e811-80dc-0050560502cc</Field>          <Operator>Equal</Operator>          <Value>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Value>false</Value>              <ValueType>Value</ValueType>              <IsLookup>false</IsLookup>            </ConditionBuilderRuleValue>          </Value>        </ConditionBuilderRule>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>e5e05ca6-acb1-e811-80dc-0050560502cc</Id>          <Field>e5e05ca6-acb1-e811-80dc-0050560502cc</Field>          <Operator>EqualsCurrentUserDefaultTeam</Operator>          <Data>            <IsDateTime>false</IsDateTime>            <BusinessObjectName>team</BusinessObjectName>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>      </Rules>    </Rules>    <RelatedBusinessObjects />  </ConditionBuilderQuery>";
            //view1.firstsortfieldid = new Guid("7754450A-6267-E911-A2C5-0050569231CF");
            //view1.secondsortfieldid = null;


            //dbHelper.userDataView.CreateUserDataView(view1.userdataviewid, view1.businessobjectid, view1.owninguserid, view1.name, view1.dataqueryxml, view1.layoutxml, view1.conditionbuilderxml, view1.firstsortfieldid, view1.secondsortfieldid);

            //view1.userdataviewid = new Guid("F26EBD80-FD45-EC11-A32D-F90A4322A942");
            //view1.businessobjectid = new Guid("B1E05CA6-ACB1-E811-80DC-0050560502CC");
            //view1.owninguserid = _load_Test_User_01Id;
            //view1.name = "DT - My Open Contacts";
            //view1.dataqueryxml = "<DataQuery   TableName=\"contact\" TableAlias=\"CONTACT\">    <Filter FilterType=\"And\">      <Conditions>        <Condition TableAlias=\"CONTACT\" Field=\"INACTIVE\" Operator=\"Equal\" ValueType=\"SingleValue\">          <ConditionValue DbType=\"Bit\" BoolValue=\"false\" />        </Condition>        <Condition TableAlias=\"CONTACT\" Field=\"OWNERID\" Operator=\"EqualsCurrentUserDefaultTeam\" ValueType=\"None\" />      </Conditions>    </Filter>    <ApplyFieldLevelSecurity>false</ApplyFieldLevelSecurity>  </DataQuery>";
            //view1.layoutxml = "<Layout  >    <Columns>      <Column Name=\"contactreceiveddatetime\" Width=\"150\" FieldId=\"7754450a-6267-e911-a2c5-0050569231cf\" />      <Column Name=\"contactstatusid\" Width=\"150\" FieldId=\"d79c0252-6667-e911-a2c5-0050569231cf\" />      <Column Name=\"contactpresentingpriorityid\" Width=\"150\" FieldId=\"18ec3697-6567-e911-a2c5-0050569231cf\" />      <Column Name=\"personid\" Width=\"200\" FieldId=\"2cb7e740-37d8-e811-9bee-989096c9be3d\" />      <Column Name=\"additionalinformation\" Width=\"200\" FieldId=\"7d078802-6667-e911-a2c5-0050569231cf\" />      <Column Name=\"contactreasonid\" Width=\"150\" FieldId=\"aaf0cd7e-6567-e911-a2c5-0050569231cf\" />      <Column Name=\"contactmadebyid\" Width=\"150\" FieldId=\"b131ee5a-6367-e911-a2c5-0050569231cf\" />      <Column Name=\"contactmadebyfreetext\" Width=\"150\" FieldId=\"0b85ef8a-6467-e911-a2c5-0050569231cf\" />      <Column Name=\"referralpriorityid\" Width=\"150\" FieldId=\"fc842c3c-6767-e911-a2c5-0050569231cf\" />    </Columns>  </Layout>";
            //view1.conditionbuilderxml = "<ConditionBuilderQuery  >    <BusinessObjectId>b1e05ca6-acb1-e811-80dc-0050560502cc</BusinessObjectId>    <DisplayName>Contact</DisplayName>    <Parental>false</Parental>    <Type>0</Type>    <Rules>      <Condition>AND</Condition>      <Valid>true</Valid>      <Rules>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>03e15ca6-acb1-e811-80dc-0050560502cc</Id>          <Field>03e15ca6-acb1-e811-80dc-0050560502cc</Field>          <Operator>Equal</Operator>          <Value>            <ConditionBuilderRuleValue>              <Key>Field</Key>              <Value>false</Value>              <ValueType>Value</ValueType>              <IsLookup>false</IsLookup>            </ConditionBuilderRuleValue>          </Value>        </ConditionBuilderRule>        <ConditionBuilderRule>          <Valid>false</Valid>          <Id>e5e05ca6-acb1-e811-80dc-0050560502cc</Id>          <Field>e5e05ca6-acb1-e811-80dc-0050560502cc</Field>          <Operator>EqualsCurrentUserDefaultTeam</Operator>          <Data>            <IsDateTime>false</IsDateTime>            <BusinessObjectName>team</BusinessObjectName>            <FilterType>0</FilterType>            <DataType>0</DataType>          </Data>        </ConditionBuilderRule>      </Rules>    </Rules>    <RelatedBusinessObjects />  </ConditionBuilderQuery>";
            //view1.firstsortfieldid = new Guid("7754450A-6267-E911-A2C5-0050569231CF");
            //view1.secondsortfieldid = null;

            //dbHelper.userDataView.CreateUserDataView(view1.userdataviewid, view1.businessobjectid, view1.owninguserid, view1.name, view1.dataqueryxml, view1.layoutxml, view1.conditionbuilderxml, view1.firstsortfieldid, view1.secondsortfieldid);
        }

        [Description("Set Authorisation Levels for all system users")]
        //[TestMethod]
        public void UpdateSystemUsers_AuthorizationLevel_Script01()
        {
            var startDate = new DateTime(2019, 1, 1);
            var financerecordid = 4;
            var systemUsers = dbHelper.systemUser.GetSystemUserByUserNamePrefix("performance_user_");
            foreach (var systemUsersid in systemUsers)
            {
                dbHelper.authorisationLevel.CreateAuthorisationLevel(_careDirector_TeamId, systemUsersid, startDate, financerecordid, 999999, true, true);
            }
        }

        [Description("Set Service Deliveries for all service provisions")]
        //[TestMethod]
        public void serviceProvision_AddServiceDeliveries_Script01()
        {
            var serviceProvisions = dbHelper.serviceProvision.GetAll();
            var newRateUnit = dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)")[0];
            foreach (var serviceProvisionId in serviceProvisions)
            {
                var personid = new Guid((dbHelper.serviceProvision.GetByID(serviceProvisionId, "personid")["personid"]).ToString());
                dbHelper.serviceDelivery.CreateServiceDelivery(_careDirector_TeamId, personid, serviceProvisionId, newRateUnit, 1, 1, true, true, true, true, true, true, true, true, "");
            }
        }



        [Description("Populate the database with person records (Males)")]
        //[TestMethod]
        public void LoadTestScript_CreatePersonWithActivityRecords_Script01()
        {
            var textLists = new List<string> {
                "Mutley, you snickering, floppy eared hound.",
                "When courage is needed, youre never around.",
                "Those medals you wear on your moth-eaten chest should be there for bungling at which you are best.",
                "Howwww! Nab him, jab him, tab him, grab him, stop that pigeon now." };



            var personID = dbHelper.person.CreatePersonRecord("Mr", "Jhon", "Invisible", "Cena", "Cena", new DateTime(2000, 1, 1), _ethnicity1Id, _careDirector_TeamId, 1, 1);
            var systemUsers = dbHelper.systemUser.GetSystemUserByUserName("test_user_1");
            if (!systemUsers.Any())
                dbHelper.systemUser.CreateSystemUser("test_user_1", "Test", "User 1", "Test User 1", "Passw0rd_!", "test_user_1@somemail.com", "test_user_1@secureemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirector_BusinessUnitId, _careDirector_TeamId, DateTime.Now.Date);
            List<Guid> personIDs = new List<Guid> { personID };
            systemUsers = dbHelper.systemUser.GetSystemUserByUserName("test_user_1");

            for (int i = 0; i < 3000; i++)
            {
                Console.WriteLine("Cicle --> " + i);

                var startdate = commonMethodsHelper.GenerateRandomDate(new DateTime(2020, 1, 1), DateTime.Now.Date);
                var startTime = new TimeSpan(10, 0, 0);
                var endTime = new TimeSpan(10, 5, 0);
                var statusid = 4; //Scheduled
                var showTimeAsId = 5; //Busy

                try
                {
                    dbHelper.personCaseNote.CreateMultipleCaseNoteRecords(1, _careDirector_TeamId, textLists, textLists, personID, new DateTime(2020, 1, 1), DateTime.Now.Date, systemUsers[0]);
                    dbHelper = new DBHelper.DatabaseHelper();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                try
                {
                    startdate = commonMethodsHelper.GenerateRandomDate(new DateTime(2020, 1, 1), DateTime.Now.Date);
                    dbHelper.phoneCall.CreateMultiplePersonPhoneCallRecords(1, textLists, textLists, personIDs, "123321123", _careDirector_TeamId, systemUsers, startdate);
                    dbHelper = new DBHelper.DatabaseHelper();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                try
                {
                    startdate = commonMethodsHelper.GenerateRandomDate(new DateTime(2020, 1, 1), DateTime.Now.Date);
                    dbHelper.task.CreateMultiplePersonTasks(1, personIDs, textLists, textLists, _careDirector_TeamId, startdate, systemUsers);
                    dbHelper = new DBHelper.DatabaseHelper();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                try
                {
                    startdate = commonMethodsHelper.GenerateRandomDate(new DateTime(2020, 1, 1), DateTime.Now.Date);
                    dbHelper.appointment.CreateMultiplePersonAppointments(1, _careDirector_TeamId, personIDs, systemUsers, textLists, textLists, textLists, startdate, startTime, startdate, endTime, statusid, showTimeAsId);
                    dbHelper = new DBHelper.DatabaseHelper();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                try
                {
                    startdate = commonMethodsHelper.GenerateRandomDate(new DateTime(2020, 1, 1), DateTime.Now.Date);
                    dbHelper.email.CreateMultiplePersonEmails(1, _careDirector_TeamId, personIDs, systemUsers, textLists, 1);
                    dbHelper = new DBHelper.DatabaseHelper();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }















        }


        #endregion
    }





    internal class UserDataView
    {
        public Guid userdataviewid { get; set; }
        public Guid businessobjectid { get; set; }
        public Guid owninguserid { get; set; }
        public string name { get; set; }
        public string dataqueryxml { get; set; }
        public string layoutxml { get; set; }
        public string conditionbuilderxml { get; set; }
        public Guid? firstsortfieldid { get; set; }
        public Guid? secondsortfieldid { get; set; }

    }
}

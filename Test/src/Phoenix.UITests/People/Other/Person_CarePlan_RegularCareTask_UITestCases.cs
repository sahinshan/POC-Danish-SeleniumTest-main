//using System;
//using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Collections.Generic;
//using static System.Net.Mime.MediaTypeNames;

//namespace Phoenix.UITests.People
//{
//    /// <summary>
//    /// This class contains Automated UI test scripts for Regular Care Tasks
//    /// </summary>
//    [TestClass]
//    public class Person_CarePlan_RegularCareTask_UITestCases : FunctionalTest
//    {
//        private Guid _languageId;
//        private Guid _careDirectorQA_BusinessUnitId;
//        private Guid _careDirectorQA_TeamId;
//        private Guid _authenticationproviderid;
//        private Guid _ethnicityId;
//        private Guid _maritalStatusId;
//        private Guid _defaultUserId;
//        private Guid _systemUserId;
//        private Guid _personID;
//        private int _personNumber;
//        private string _personFullName;
//        private Guid _carePlanType;
//        private Guid _personCarePlanID;
//        private Guid _personcareplanregularcaskid;
//        private Guid _careTaskid;
//        private Guid _careTaskid1;
//        private Guid _personcareplanregularcaskid_inactive;
//        private string _systemUsername;
//        private Guid _personCarePlanFormID;
//        private Guid _bookingType5;
//        private Guid _careProviderStaffRoleTypeid;
//        private Guid _employmentContractTypeid1;
//        private Guid _providereId1;
//        private Guid _recurrencePattern_Every1WeekMondayId;
//        private Guid _recurrencePattern_Every1WeekTuesdayId;
//        private Guid _recurrencePattern_Every1WeekWednesdayId;
//        private Guid _recurrencePattern_Every1WeekThursdayId;
//        private Guid _recurrencePattern_Every1WeekFridayId;
//        private Guid _recurrencePattern_Every1WeekSaturdayId;
//        private Guid _recurrencePattern_Every1WeekSundayId;
//        private Guid _availabilityTypes_StandardId;
//        private Guid _availabilityTypes_OverTimeId;
//        private Guid _cpdiarybookingid;
//        private Guid _personcontractId;
//        private Guid _contractschemeid;
//        private Guid _recurrencePatternId_EveryDay;
//        private string _adminSystemUsername;
//        private Guid _adminSystemUserId;

//        [TestInitialize()]
//        public void Person_CarePlan_SetupTest()
//        {

//            try
//            {

//                #region Business Unit

//                var businessUnitExists = dbHelper.businessUnit.GetByName("CareDirector QA").Any();
//                if (!businessUnitExists)
//                    dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
//                _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];

//                #endregion

//                #region Providers

//                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

//                #endregion

//                #region Team

//                var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector QA").Any();
//                if (!teamsExist)
//                    dbHelper.team.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
//                _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

//                #endregion

//                #region Marital Status

//                var maritalStatusExist = dbHelper.maritalStatus.GetMaritalStatusIdByName("Civil Partner").Any();
//                if (!maritalStatusExist)
//                {
//                    _maritalStatusId = dbHelper.maritalStatus.CreateMaritalStatus("Civil Partner", new DateTime(2000, 1, 1), _careDirectorQA_TeamId);
//                }
//                if (_maritalStatusId == Guid.Empty)
//                {
//                    _maritalStatusId = dbHelper.maritalStatus.GetMaritalStatusIdByName("Civil Partner").FirstOrDefault();
//                }
//                #endregion

//                #region Language

//                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
//                if (!language)
//                {
//                    _languageId = dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
//                }
//                if (_languageId == Guid.Empty)
//                {
//                    _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").FirstOrDefault();
//                }
//                #endregion Lanuage

//                #region Ethnicity

//                var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("PersonCarePlan_Ethnicity").Any();
//                if (!ethnicitiesExist)
//                    dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "PersonCarePlan_Ethnicity", new DateTime(2020, 1, 1));
//                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("PersonCarePlan_Ethnicity")[0];

//                #endregion

//                #region SecurityProfiles

//                var userSecProfiles = new List<Guid>();

//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Bed Management (Edit)")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Bed Management Setup (Edit)")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Create Person Absences")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Alert/Hazard Module (Edit)")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("CAMT Integration")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Can Access Customizations")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Can View People We Support")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Planning (Edit)")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Availability Type View Only")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Plan Forms (Edit)")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Reference Data (Edit)")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Scheduling Setup (Edit)")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Staff Availability (Edit)")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Staff Demographics (Edit)")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Rostering (Edit)")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Delete Booking Diary")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Delete Booking Schedules")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Worker Contract (Edit)")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (View)")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Daily Care (Edit)")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Person (Edit)")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Person Module (Edit)")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Provider (Edit)")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Qualified to Authorise Care Plans")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Security Management Access")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Settings Area Access")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("System User (Edit)")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("System User Employment Contract (Field Edit)")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("System User Open Ended Absence (Edit)")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("System User Open Ended Absence (View)")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("User Diaries (Edit)")[0]);
//                //userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Person Prospect (View/Edit)")[0]);
//                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Person Tracking")[0]);



//                #endregion



//                #region Create SystemUser 

//                _systemUsername = "TestUser6";
//                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "ServiceProvisions", "User4", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid, userSecProfiles);
//                dbHelper.systemUser.UpdateEmployeeTypeId(_systemUserId, 3);

//                #endregion

//                #region Booking Type 5 -> "Booking (to service user)" 

//                if (!dbHelper.cpBookingType.GetByName("BookingType-5").Any())
//                    _bookingType5 = dbHelper.cpBookingType.CreateBookingType("BookingType-5", 5, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 2, false, 1440);

//                if (_bookingType5 == Guid.Empty)
//                    _bookingType5 = dbHelper.cpBookingType.GetByName("BookingType-5").First();

//                #endregion

//                #region Provider 1

//                if (!dbHelper.provider.GetProviderByName("Provider-001").Any())
//                {
//                    _providereId1 = dbHelper.provider.CreateProvider("Provider-001", _careDirectorQA_TeamId, 13, true); //create a "Residential Establishment" provider

//                    dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_careDirectorQA_TeamId, _providereId1, _bookingType5, true);

//                }

//                if (_providereId1 == Guid.Empty)
//                    _providereId1 = dbHelper.provider.GetProviderByName("Provider-001").First();

//                #endregion

//                #region Person
//                var firstName = "Person_CarePlan1" + DateTime.Now.ToString("yyyyMMddHHmmss");
//                var lastName = "LN_CDV6_17302";
//                var personRecordExists = dbHelper.person.GetByFirstName(firstName).Any();

//                _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
//                _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];


//                _personFullName = "Person_CarePlan1 LN_CDV6_17302" + DateTime.Now.ToString("yyyyMMddHHmmss");

//                #endregion

//                #region create contract scheme

//                var contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
//                _contractschemeid = commonMethodsDB.CreateCareProviderContractScheme(_careDirectorQA_TeamId, _systemUserId, _careDirectorQA_BusinessUnitId, "Contract-Scheme-001", new DateTime(2000, 1, 2), contractSchemeCode, _providereId1, _providereId1);

//                #endregion

//                #region create person contract

//                _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_careDirectorQA_TeamId, "title", _personID, _systemUserId, _providereId1, _contractschemeid, _providereId1, DateTime.Today);

//                #endregion

//                #region Care provider staff role type
//               _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careDirectorQA_TeamId, "Role-001", "1234", null, new DateTime(2020, 1, 1), null);
//                  #endregion

//                #region Employment Contract Type - Salaried
//                _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_careDirectorQA_TeamId, "Salaried", "", null, new DateTime(2020, 1, 1));
//                #endregion

//                #region Recurrence Patterns

//                _recurrencePattern_Every1WeekMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on monday").First();
//                _recurrencePattern_Every1WeekTuesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on tuesday").First();
//                _recurrencePattern_Every1WeekWednesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on wednesday").First();
//                _recurrencePattern_Every1WeekThursdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on thursday").First();
//                _recurrencePattern_Every1WeekFridayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on friday").First();
//                _recurrencePattern_Every1WeekSaturdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on saturday").First();
//                _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();
//                var recurrencePatternExists = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").Any();
//                if (!recurrencePatternExists)
//                    _recurrencePatternId_EveryDay = dbHelper.recurrencePattern.CreateRecurrencePattern(1, 1);


//                _recurrencePatternId_EveryDay = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").First();

//                #endregion

//                #region Availability Type
//                  _availabilityTypes_StandardId = commonMethodsDB.CreateAvailabilityType("Standard", 3, false, _careDirectorQA_TeamId, 1, 1, true);

//                  _availabilityTypes_OverTimeId = commonMethodsDB.CreateAvailabilityType("OverTime", 4, false, _careDirectorQA_TeamId, 1, 1, true);

//                #endregion

//                #region care Tasks
//                   _careTaskid = commonMethodsDB.CreateCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Assist with bath or shower1", 001, DateTime.Now);

//                   _careTaskid1 = commonMethodsDB.CreateCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Assist with dressing1", 002, DateTime.Now);

//                #endregion


//            }
//            catch
//            {
//                if (driver != null)
//                    driver.Quit();

//                throw;
//            }

//        }
//        internal DateTime GetThisWeekFirstWednesday()
//        {
//            DateTime dt = DateTime.Now;
//            int diff = (7 + (dt.DayOfWeek - DayOfWeek.Wednesday)) % 7;
//            return dt.AddDays(-1 * diff).Date;

//        }

//        #region https://advancedcsg.atlassian.net/browse/ACC-1796

//        [TestProperty("JiraIssueID", "ACC-3162")]
//        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-23126 - " +
//            "Verify the update to Regular Care Task with new “Inactive “field and grids for Active & Inactive records.As a Care Coordinator verify the update to Regular Care Task with new field “Inactive “and grids with Active & Inactive records" +
//            "PreCondition:People record should exist.Active Care plan record should be available for selected person record")]
//        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
//        [TestMethod]
//        public void PersonCarePlan_RegularCareTask_Testmethod01()
//        {
//            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);

//            _personcareplanregularcaskid = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, false, _personID, _careTaskid, null, "ActiveRegularCareTask", _personCarePlanID);

//            _personcareplanregularcaskid_inactive = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, true, _personID, _careTaskid1, null, "InActiveRegularCareTask", _personCarePlanID);
//            _personcareplanregularcaskid_inactive = dbHelper.regularCareTask.GetByCarePlanID(_personCarePlanID, true).FirstOrDefault();

//            loginPage
//                .GoToLoginPage()
//                .Login(_systemUsername, "Passw0rd_!");

//            mainMenu
//                .WaitForMainMenuToLoad()
//                .NavigateToPeopleSection();

//            peoplePage
//                .WaitForPeoplePageToLoad()
//                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
//                .OpenPersonRecord(_personID.ToString());

//            personRecordPage
//                .WaitForPersonRecordPageToLoad(false, false, false, true)
//                //.WaitForPersonRecordPageToLoad()
//                .TapCarePlansTab();

//            personCarePlansSubPage
//                .WaitForPersonCarePlansSubPageToLoad()
//                .ClickRegularCareTasksLink();

//            personCarePlansSubPage_regularCareTasksTab
//                .WaitForPersonCarePlansSubPage_RegularCareTasksTabToLoad()
//                .ValidateRecordCellText(_personcareplanregularcaskid.ToString(), 2, "Assist with bath or shower");

//            personCarePlansSubPage_regularCareTasksTab
//               .WaitForPersonCarePlansSubPage_RegularCareTasksInactiveTabToLoad()
//               .ValidateRecordCellText(_personcareplanregularcaskid_inactive.ToString(), 2, "Assist with dressing")
//               .ClickCreateNewRecord();

//            personCarePlansSubPage_RegularCareTasksTab_Record
//                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
//                .ValidatePersonLabelVisibility(true)
//                .ValidateCareTaskLabelVisibility(true)
//                .ValidateLinkedActionLabelVisibility(true)
//                .ValidateResponsibleTeamLabelVisibility(true)
//                .ValidateCarePlanLabelVisibility(true)
//                .ValidateInactive_NoRadioButtonChecked();

//        }


//        [TestProperty("JiraIssueID", "ACC-3163")]
//        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-23127 - " +
//            "Verify reactivate button when a care task is inactive in Regular Care Task .As a Care Coordinator Verify reactivate button when a care task is inactive in Regular Care Task" +
//            "PreCondition:People record should exist.Active Care plan record should be available for selected person record")]
//        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
//        [TestMethod]
//        public void PersonCarePlan_RegularCareTask_TestMethod02()
//        {
//            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);

//            _personcareplanregularcaskid_inactive = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, true, _personID, _careTaskid, null, "InActiveRegularCareTask", _personCarePlanID);
//            _personcareplanregularcaskid_inactive = dbHelper.regularCareTask.GetByCarePlanID(_personCarePlanID, true).FirstOrDefault();

//            loginPage
//                .GoToLoginPage()
//                .Login(_systemUsername, "Passw0rd_!");

//            mainMenu
//                .WaitForMainMenuToLoad()
//                .NavigateToPeopleSection();

//            peoplePage
//                .WaitForPeoplePageToLoad()
//                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
//                .OpenPersonRecord(_personID.ToString());

//            personRecordPage
//                .WaitForPersonRecordPageToLoad(false, false, false, true)
//                //.WaitForPersonRecordPageToLoad()
//                .TapCarePlansTab();

//            personCarePlansSubPage
//                .WaitForPersonCarePlansSubPageToLoad()
//                .ClickRegularCareTasksLink();

//            personCarePlansSubPage_regularCareTasksTab
//                .WaitForPersonCarePlansSubPage_RegularCareTasksInactiveTabToLoad()
//                .ClickRecordCellText(_personcareplanregularcaskid_inactive.ToString(), 2);


//            personCarePlansSubPage_RegularCareTasksTab_Record
//             .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
//             .ValidateActivateButtonVisibility(true);


//        }

//        [TestProperty("JiraIssueID", "ACC-3164")]
//        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-23128 - " +
//           "Verify different validation shown for Inactive regular care task records  .As a Care Coordinator Verify different validation shown for Inactive regular care task records while creating a new regular care task or a booking with regular care task" +
//           "PreCondition:People record should exist.Active Care plan record should be available for selected person record")]
//        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
//        [TestMethod]
//        public void PersonCarePlan_RegularCareTask_TestMethod03()
//        {
//            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);

//            _personcareplanregularcaskid_inactive = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, true, _personID, _careTaskid, null, "InActiveRegularCareTask", _personCarePlanID);
//            _personcareplanregularcaskid_inactive = dbHelper.regularCareTask.GetByCarePlanID(_personCarePlanID, true).FirstOrDefault();

//            dbHelper.personCarePlan.UpdateStatus(_personCarePlanID, 2);

//            loginPage
//                .GoToLoginPage()
//                .Login(_systemUsername, "Passw0rd_!");

//            mainMenu
//                .WaitForMainMenuToLoad()
//                .NavigateToPeopleSection();

//            peoplePage
//                .WaitForPeoplePageToLoad()
//                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
//                .OpenPersonRecord(_personID.ToString());

//            personRecordPage
//                .WaitForPersonRecordPageToLoad(false, false, false, true)
//                .TapCarePlansTab();

//            personCarePlansSubPage
//                .WaitForPersonCarePlansSubPageToLoad()
//                .ClickRegularCareTasksLink();


//            personCarePlansSubPage_regularCareTasksTab
//               .WaitForPersonCarePlansSubPage_RegularCareTasksInactiveTabToLoad()
//                 .ClickCreateNewRecord();

//            personCarePlansSubPage_RegularCareTasksTab_Record
//                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
//                //.ClickInactive_YesRadioButton()
//                .ClickCareTaskLookUp();

//            lookupPopup
//                .WaitForLookupPopupToLoad()
//                .TypeSearchQuery("Assist with bath or shower1")
//                .TapSearchButton()
//                .SelectResultElement(_careTaskid.ToString());

//            personCarePlansSubPage_RegularCareTasksTab_Record
//               .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
//               .ClickSaveNClose();


//            personCarePlansSubPage_regularCareTasksTab
//               .WaitForPersonCarePlansSubPage_RegularCareTasksInactiveTabToLoad()
//               .ClickRecordCellText(_personcareplanregularcaskid_inactive.ToString(), 2);

//            personCarePlansSubPage_RegularCareTasksTab_Record
//                 .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
//                 .ClickActivateBtn();

//            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to activate this record? To continue, click ok.").TapOKButton();


//            personCarePlansSubPage_RegularCareTasksTab_Record
//                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
//                .ValidateMessageAreaText("A record with the same action and task already exists.");


//        }


//        [TestProperty("JiraIssueID", "ACC-3165")]
//        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-23128 - " +
//           "Verify different validation shown for Inactive regular care task records  .As a Care Coordinator Verify different validation shown for Inactive regular care task records while creating a new regular care task or a booking with regular care task" +
//           "PreCondition:People record should exist.Active Care plan record should be available for selected person record")]
//        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
//        [TestMethod]
//        public void PersonCarePlan_RegularCareTask_TestMethod03_Scenario2()
//        {

//            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);
//            //Inactive Care Tasks

//            _personcareplanregularcaskid_inactive = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, true, _personID, _careTaskid, null, "InActiveRegularCareTask", _personCarePlanID);
//            _personcareplanregularcaskid_inactive = dbHelper.regularCareTask.GetByCarePlanID(_personCarePlanID, true).FirstOrDefault();
//            //Active Care Tasks
//            _personcareplanregularcaskid = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, false, _personID, _careTaskid1, null, "ActiveRegularCareTask", _personCarePlanID);
//            _personcareplanregularcaskid = dbHelper.regularCareTask.GetByCarePlanID(_personCarePlanID, false).FirstOrDefault();

//            //Create a System User Employment Contract

//            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

//            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
//            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

//            //Link Booking Types with the Employment Contract created previously
//            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

//            #region create diary booking
//            var todayDate = DateTime.Now.Date;
//            var _applicantId = dbHelper.applicant.CreateApplicant("test", "lastname", _careDirectorQA_TeamId);
//            var _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _systemUserId, _careDirectorQA_TeamId, DateTime.Now, _careDirectorQA_TeamId, 1);

//            var _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, todayDate, null, new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _roleApplication, _applicantId, 1);
//            var _UserDiaryId = dbHelper.userDairy.createUserDairy(_systemUserId, _careDirectorQA_TeamId, _UserScheduleIdTypeId, _applicantId, _roleApplication, todayDate, todayDate, 60, 1380);


//            _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _bookingType5, _providereId1, todayDate, new TimeSpan(12, 0, 0), todayDate, new TimeSpan(12, 30, 0), "staff", 30, 1, "people", 890);

//            var staffid = dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_careDirectorQA_TeamId, "test1", _cpdiarybookingid, _systemUserEmploymentContractId, _systemUserId);
//            dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_careDirectorQA_TeamId, _cpdiarybookingid, _personID, _personcontractId);

//            #endregion


//            loginPage
//                .GoToLoginPage()
//                .Login(_systemUsername, "Passw0rd_!");

//            mainMenu
//               .WaitForMainMenuToLoad()
//               .NavigateToProviderDiarySection();

//            providerDiaryPage
//                .WaitForProviderDiaryPageToLoad()
//                .selectProvider("Provider-001 - No Address")
//                .ClickPrevioudDateButton()
//                .ClickDiaryBooking(_cpdiarybookingid.ToString())
//                .WaitForProviderDiaryEditPageToLoad()
//                .ClickCareTasksButton()
//                .ValidateCareTasksAssigned("Assist with dressing1");

//        }

//        #endregion


//    }
//}


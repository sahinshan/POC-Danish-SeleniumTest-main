using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.UITests.People
{
    /// <summary>
    /// This class contains Automated UI test scripts for Regular Care Tasks
    /// </summary>
    [TestClass]
    public class Person_CarePlan_ScheduleRegularCareTask_UITestCases : FunctionalTest
    {
        private Guid _languageId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private Guid _maritalStatusId;
        private Guid _defaultUserId;
        private Guid _systemUserId;
        private Guid _personID;
        private int _personNumber;
        private string _personFullName;
        private Guid _carePlanType;
        private Guid _personCarePlanID;
        private Guid _personcareplanregularcaskid;
        private Guid _personcareplanregularcaskscheduleid;
        private Guid _careTaskid;
        private Guid _careTaskid1;
        private Guid _personcareplanregularcaskid_inactive;
        private string _systemUsername;
        private Guid _personCarePlanFormID;
        private Guid _bookingType5;
        private Guid _careProviderStaffRoleTypeid;
        private Guid _employmentContractTypeid1;
        private Guid _providereId1;
        private Guid _recurrencePattern_Every1WeekMondayId;
        private Guid _recurrencePattern_Every1WeekTuesdayId;
        private Guid _recurrencePattern_Every1WeekWednesdayId;
        private Guid _recurrencePattern_Every1WeekThursdayId;
        private Guid _recurrencePattern_Every1WeekFridayId;
        private Guid _recurrencePattern_Every1WeekSaturdayId;
        private Guid _recurrencePattern_Every1WeekSundayId;
        private Guid _availabilityTypes_StandardId;
        private Guid _availabilityTypes_OverTimeId;
        private Guid _cpdiarybookingid;
        private Guid _personcontractId;
        private Guid _contractschemeid;
        private Guid _recurrencePatternId_EveryDay;
        private string _adminSystemUsername;
        private Guid _adminSystemUserId;

        [TestInitialize()]
        public void Person_CarePlan_SetupTest()
        {

            try
            {

                #region Business Unit

                var businessUnitExists = dbHelper.businessUnit.GetByName("CareDirector QA").Any();
                if (!businessUnitExists)
                    dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
                _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];

                #endregion

                #region Providers

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Team

                var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector QA").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
                _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

                #endregion

                #region Marital Status

                var maritalStatusExist = dbHelper.maritalStatus.GetMaritalStatusIdByName("Civil Partner").Any();
                if (!maritalStatusExist)
                {
                    _maritalStatusId = dbHelper.maritalStatus.CreateMaritalStatus("Civil Partner", new DateTime(2000, 1, 1), _careDirectorQA_TeamId);
                }
                if (_maritalStatusId == Guid.Empty)
                {
                    _maritalStatusId = dbHelper.maritalStatus.GetMaritalStatusIdByName("Civil Partner").FirstOrDefault();
                }
                #endregion

                #region Language

                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
                if (!language)
                {
                    _languageId = dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
                }
                if (_languageId == Guid.Empty)
                {
                    _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").FirstOrDefault();
                }
                #endregion Lanuage

                #region Ethnicity

                var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("PersonCarePlan_Ethnicity").Any();
                if (!ethnicitiesExist)
                    dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "PersonCarePlan_Ethnicity", new DateTime(2020, 1, 1));
                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("PersonCarePlan_Ethnicity")[0];

                #endregion

                #region SecurityProfiles

                var userSecProfiles = new List<Guid>();

                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Bed Management (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Bed Management Setup (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Create Person Absences")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Alert/Hazard Module (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("CAMT Integration")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Can Access Customizations")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Can View People We Support")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Planning (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Availability Type View Only")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Plan Forms (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Reference Data (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Scheduling Setup (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Staff Availability (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Staff Demographics (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Rostering (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Delete Booking Diary")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Delete Booking Schedules")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Worker Contract (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (View)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Daily Care (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Person (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Person Module (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Provider (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Qualified to Authorise Care Plans")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Security Management Access")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Settings Area Access")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("System User (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("System User Employment Contract (Field Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("System User Open Ended Absence (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("System User Open Ended Absence (View)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("User Diaries (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Person Tracking")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Advanced Search")[0]);



                #endregion

                #region Create SystemUser 

                _systemUsername = "TestUser6";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "Test", "User6", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid, userSecProfiles);
                dbHelper.systemUser.UpdateEmployeeTypeId(_systemUserId, 3);

                #endregion

                #region Create Admin SystemUser 


                _adminSystemUsername = "AdminUser";
                _adminSystemUserId = commonMethodsDB.CreateSystemUserRecord(_adminSystemUsername, "AdvancedSearch", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
                dbHelper.systemUser.UpdateEmployeeTypeId(_systemUserId, 3);


                #endregion

                #region Booking Type 5 -> "Booking (to service user)" 

                if (!dbHelper.cpBookingType.GetByName("BookingType-5").Any())
                    _bookingType5 = dbHelper.cpBookingType.CreateBookingType("BookingType-5", 5, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 2, false, 1440);

                if (_bookingType5 == Guid.Empty)
                    _bookingType5 = dbHelper.cpBookingType.GetByName("BookingType-5").First();

                #endregion

                #region Provider 1

                if (!dbHelper.provider.GetProviderByName("Provider-001").Any())
                {
                    _providereId1 = dbHelper.provider.CreateProvider("Provider-001", _careDirectorQA_TeamId, 13, true); //create a "Residential Establishment" provider

                    dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_careDirectorQA_TeamId, _providereId1, _bookingType5, true);

                }

                if (_providereId1 == Guid.Empty)
                    _providereId1 = dbHelper.provider.GetProviderByName("Provider-001").First();

                #endregion

                #region Person
                var firstName = "Person_CarePlan1" + DateTime.Now.ToString("yyyyMMddHHmmss");
                var lastName = "LN_CDV6_17302";
                var personRecordExists = dbHelper.person.GetByFirstName(firstName).Any();

                _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
                _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];


                _personFullName = "Person_CarePlan1 LN_CDV6_17302" + DateTime.Now.ToString("yyyyMMddHHmmss");

                #endregion

                #region create contract scheme

                var contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
                _contractschemeid = commonMethodsDB.CreateCareProviderContractScheme(_careDirectorQA_TeamId, _systemUserId, _careDirectorQA_BusinessUnitId, "Contract-Scheme-001", new DateTime(2000, 1, 2), contractSchemeCode, _providereId1, _providereId1);

                #endregion

                #region create person contract

                _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_careDirectorQA_TeamId, "title", _personID, _systemUserId, _providereId1, _contractschemeid, _providereId1, DateTime.Today);

                #endregion

                #region Care provider staff role type
                _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careDirectorQA_TeamId, "Role-001", "1234", null, new DateTime(2020, 1, 1), null);
                #endregion

                #region Employment Contract Type - Salaried
                _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_careDirectorQA_TeamId, "Salaried", "", null, new DateTime(2020, 1, 1));
                #endregion

                #region Recurrence Patterns

                _recurrencePattern_Every1WeekMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on monday").First();
                _recurrencePattern_Every1WeekTuesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on tuesday").First();
                _recurrencePattern_Every1WeekWednesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on wednesday").First();
                _recurrencePattern_Every1WeekThursdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on thursday").First();
                _recurrencePattern_Every1WeekFridayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on friday").First();
                _recurrencePattern_Every1WeekSaturdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on saturday").First();
                _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();
                var recurrencePatternExists = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").Any();
                if (!recurrencePatternExists)
                    _recurrencePatternId_EveryDay = dbHelper.recurrencePattern.CreateRecurrencePattern(1, 1);


                _recurrencePatternId_EveryDay = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").First();

                #endregion

                #region Availability Type
                _availabilityTypes_StandardId = commonMethodsDB.CreateAvailabilityType("Standard", 3, false, _careDirectorQA_TeamId, 1, 1, true);

                _availabilityTypes_OverTimeId = commonMethodsDB.CreateAvailabilityType("OverTime", 4, false, _careDirectorQA_TeamId, 1, 1, true);

                #endregion

                #region care Tasks
                _careTaskid = commonMethodsDB.CreateCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Assist with bath or shower1", 001, DateTime.Now);

                _careTaskid1 = commonMethodsDB.CreateCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Assist with dressing1", 002, DateTime.Now);

                #endregion


            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }
        internal DateTime GetThisWeekFirstWednesday()
        {
            DateTime dt = DateTime.Now;
            int diff = (7 + (dt.DayOfWeek - DayOfWeek.Wednesday)) % 7;
            return dt.AddDays(-1 * diff).Date;

        }

        #region https://advancedcsg.atlassian.net/browse/ACC-1438

        [TestProperty("JiraIssueID", "ACC-5724")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-5724 - " +
            "As a care coordinator Verify addition of new sub BO “Care Task Schedule” within Regular Care Task record and fields on form while creating ‘Care Task Schedule’ record" +
            "PreCondition:People record should exist.Person should have some active and inactive regular care tasks.Step 1 to 7 of the Original Tests.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Regular Care")]
        public void PersonCarePlan_ScheduleRegularCareTask_Testmethod01()
        {
            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);

            _personcareplanregularcaskid = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, false, _personID, _careTaskid, null, "ActiveRegularCareTask", _personCarePlanID);


            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickRegularCareLink();

            personCarePlansSubPage_regularCareTasksTab
                .WaitForPersonCarePlansSubPage_RegularCareTasksTabToLoad()
                .ClickRecordCellText(_personcareplanregularcaskid.ToString(), 2);

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("regularcaretask").ClickOnExpandIcon();

            personCarePlansSubPage_RegularCareTasksTab_Record
                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
                .ClickCareScheduleTab()
                .WaitForPersonCarePlansSubPage_Schedules_PageToLoad()
                .VerifyCareScheduleTitle(true)
                .ClickNewCareScheduleButton();

            PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage
                .WaitForCareScheludesRecordPageToLoadinDrawerMode()
                .ValidatePersonLookupButtonVisible(true)
                .ValidateRegularCareLookupButtonVisible(true)
                .ClickRegularCareLookupButton();

            regularCareLookupPopUp
                .WaitForLookupViewPopupToLoad()
                .ValidateLookINDropDownText("Active Records with an active Care Plan")
                .ClickCloseButton();

            PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage
                .WaitForCareScheludesRecordPageToLoadinDrawerMode()
                .ValidateStartDateFieldValue(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDateFieldValue("");
        }


        [TestProperty("JiraIssueID", "ACC-5759")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-5759 - " +
           "As a care coordinator Verify addition of new sub BO “Care Task Schedule” within Regular Care Task record and fields on form while creating ‘Care Task Schedule’ record" +
           "PreCondition:People record should exist.Person should have some active and inactive regular care tasks.Step 8 of the Original Tests.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Regular Care")]
        public void PersonCarePlan_ScheduleRegularCareTask_Testmethod02()
        {
            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);

            _personcareplanregularcaskid = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, false, _personID, _careTaskid, null, "ActiveRegularCareTask", _personCarePlanID);

            var CareShiftId = commonMethodsDB.CreateCareProviderCarePeriodSetup(_careDirectorQA_TeamId, "Morning", DateTime.Now.TimeOfDay);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickRegularCareLink();

            personCarePlansSubPage_regularCareTasksTab
                .WaitForPersonCarePlansSubPage_RegularCareTasksTabToLoad()
                .ClickRecordCellText(_personcareplanregularcaskid.ToString(), 2);

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("regularcaretask").ClickOnExpandIcon();

            personCarePlansSubPage_RegularCareTasksTab_Record
                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
                .ClickCareScheduleTab()
                .WaitForPersonCarePlansSubPage_Schedules_PageToLoad()
                .VerifyCareScheduleTitle(true)
                .ClickNewCareScheduleButton();

            PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage
                .WaitForCareScheludesRecordPageToLoadinDrawerMode()
                .SelectTimeRShift("Time")
                .ValidateSelectTimeLabel("Select Time for Care to be Given*")
                .ValidateTimeLableFieldVisibility(true)
                .SelectTimeRShift("Shift")
                .ValidateSelectShiftLabel("Select Shift for Care to be Given*")
                .ClickSelectShiftLookUp();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Morning").TapSearchButton().SelectResultElement(CareShiftId.ToString());

            PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage
                .WaitForCareScheludesRecordPageToLoadinDrawerMode();
        }

        [TestProperty("JiraIssueID", "ACC-5778")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-5778 - " +
           "As a care coordinator Verify addition of new sub BO “Care Task Schedule” within Regular Care Task record and fields on form while creating ‘Care Task Schedule’ record" +
           "PreCondition:People record should exist.Person should have some active and inactive regular care tasks.Step 9-13 of the Original Tests.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Regular Care")]
        public void PersonCarePlan_ScheduleRegularCareTask_Testmethod03()
        {
            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);

            _personcareplanregularcaskid = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, false, _personID, _careTaskid, null, "ActiveRegularCareTask", _personCarePlanID);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickRegularCareLink();

            personCarePlansSubPage_regularCareTasksTab
                .WaitForPersonCarePlansSubPage_RegularCareTasksTabToLoad()
                .ClickRecordCellText(_personcareplanregularcaskid.ToString(), 2);

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("regularcaretask").ClickOnExpandIcon();

            personCarePlansSubPage_RegularCareTasksTab_Record
                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
                .ClickCareScheduleTab()
                .WaitForPersonCarePlansSubPage_Schedules_PageToLoad()
                .VerifyCareScheduleTitle(true)
                .ClickNewCareScheduleButton();


            PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage
                .WaitForCareScheludesRecordPageToLoadinDrawerMode()
                .ValidateLastRunDateFieldVisibility(true)
                .ValidateRecurrencePatternPicklistSelectedText("")
                .ValidateRecurrencePatternPicklist_FieldOptionIsPresent("Hourly")
                .ValidateRecurrencePatternPicklist_FieldOptionIsPresent("Daily")
                .ValidateRecurrencePatternPicklist_FieldOptionIsPresent("Weekly")
                .SelectRecurrencePatternPicklist("Hourly")
                .ValidateRecurEveryXHourLabel("Recur every (x) Hour*")
                .ValidateDoesNotOccurFromLabel(true, "Does not occur from")
                .ValidateDoesNotOccurToLabel(true, "Does not occur to")
                .SelectRecurrencePatternPicklist("Daily")
                .ValidateRecurEveryXDayLabel(true, "Recur every (x) Day*")
                .SelectRecurrencePatternPicklist("Weekly")
                .ValidateRecurEveryXWeekLabel(true, "Recur every (x) Week*")
                .ValidateRadioButtonRecurEveryXDayTextVisibility("monday", true)
                .ValidateRadioButtonRecurEveryXDayTextVisibility("tuesday", true)
                .ValidateRadioButtonRecurEveryXDayTextVisibility("wednesday", true)
                .ValidateRadioButtonRecurEveryXDayTextVisibility("thursday", true)
                .ValidateRadioButtonRecurEveryXDayTextVisibility("friday", true)
                .ValidateRadioButtonRecurEveryXDayTextVisibility("saturday", true)
                .ValidateRadioButtonRecurEveryXDayTextVisibility("sunday", true)
                .ValidateRadioButtonRecurEveryXDayOptionVisibility("monday", 1, true)
                .ValidateRadioButtonRecurEveryXDayOptionVisibility("tuesday", 1, true)
                .ValidateRadioButtonRecurEveryXDayOptionVisibility("wednesday", 1, true)
                .ValidateRadioButtonRecurEveryXDayOptionVisibility("thursday", 1, true)
                .ValidateRadioButtonRecurEveryXDayOptionVisibility("friday", 1, true)
                .ValidateRadioButtonRecurEveryXDayOptionVisibility("saturday", 1, true)
                .ValidateRadioButtonRecurEveryXDayOptionVisibility("sunday", 1, true);

        }

        [TestProperty("JiraIssueID", "ACC-5809")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-5809 - " +
           "As a care coordinator Verify addition of new sub BO “Care Task Schedule” within Regular Care Task record and fields on form while creating ‘Care Task Schedule’ record" +
           "PreCondition:People record should exist.Person should have some active and inactive regular care tasks.Step 14-15 of the Original Tests.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Regular Care")]
        public void PersonCarePlan_ScheduleRegularCareTask_Testmethod04()
        {
            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);

            _personcareplanregularcaskid = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, false, _personID, _careTaskid, null, "ActiveRegularCareTask", _personCarePlanID);

            _personcareplanregularcaskid_inactive = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, true, _personID, _careTaskid1, null, "InActiveRegularCareTask", _personCarePlanID);
            _personcareplanregularcaskid_inactive = dbHelper.regularCareTask.GetByCarePlanID(_personCarePlanID, true).First();

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickRegularCareLink();

            personCarePlansSubPage_regularCareTasksTab
                .WaitForPersonCarePlansSubPage_RegularCareTasksInactiveTabToLoad()
                .ClickRecordCellText(_personcareplanregularcaskid_inactive.ToString(), 2);


            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("regularcaretask").ClickOnExpandIcon();

            personCarePlansSubPage_RegularCareTasksTab_Record
                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
                .ClickCareScheduleTab()
                .WaitForPersonCarePlansSubPage_Schedules_PageToLoad()
                .VerifyCareScheduleTitle(true)
                .ValidateNewCareScheduleButton(false);

        }

        [TestProperty("JiraIssueID", "ACC-5813")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-5813 - " +
          "As a care coordinator Verify the fields and searchable columns in the system view and fields available in Advance search for Care Task Schedule Bo" +
          "PreCondition:People record should exist.Person should have some active and inactive regular care tasks.Step 1-6 of the Original Tests.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Regular Care")]
        public void PersonCarePlan_ScheduleRegularCareTask_Testmethod05()
        {
            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);

            _personcareplanregularcaskid = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, false, _personID, _careTaskid, null, "ActiveRegularCareTask", _personCarePlanID);


            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickRegularCareLink();

            personCarePlansSubPage_regularCareTasksTab
                .WaitForPersonCarePlansSubPage_RegularCareTasksTabToLoad()
                .ClickRecordCellText(_personcareplanregularcaskid.ToString(), 2);


            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("regularcaretask").ClickOnExpandIcon();

            personCarePlansSubPage_RegularCareTasksTab_Record
                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
                .ClickCareScheduleTab()
                .WaitForPersonCarePlansSubPage_Schedules_PageToLoad()
                .VerifyCareScheduleTitle(true)
                .ClickNewCareScheduleButton();


            PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage
                .WaitForCareScheludesRecordPageToLoadinDrawerMode()
                .SelectTimeRShift("Time")
                .SetSelectTimeField("10:00")
                .SelectRecurrencePatternPicklist("Hourly")
                .SetRecurEveryXHourField("1")
                .clickSaveNCloseBtn();
            System.Threading.Thread.Sleep(2000);

            personCarePlansSubPage_RegularCareTasksTab_Record
                .WaitForPersonCarePlansSubPage_Schedules_PageToLoad()
                .ClickNewCareScheduleButton();

            PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage
                .WaitForCareScheludesRecordPageToLoadinDrawerMode()
                .SelectTimeRShift("Time")
                .SetSelectTimeField("10:00")
                .SelectRecurrencePatternPicklist("Daily")
                .SetRecurEveryXDay("1")
                .clickSaveNCloseBtn();
            System.Threading.Thread.Sleep(2000);

            personCarePlansSubPage_RegularCareTasksTab_Record
                .WaitForPersonCarePlansSubPage_Schedules_PageToLoad()
                .ClickNewCareScheduleButton();

            PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage
                .WaitForCareScheludesRecordPageToLoadinDrawerMode()
                .SelectTimeRShift("Time")
                .SetSelectTimeField("10:00")
                .SelectRecurrencePatternPicklist("Weekly")
                .SetRecurEveryXWeek("1")
                .SelectRadioButtonRecurEveryXDayOption("monday", 1)
                .clickSaveNCloseBtn();

            System.Threading.Thread.Sleep(2000);
            var regularcarescheduleid = dbHelper.cpRegularCareTaskSchedule.GetByCPRegularCareTaskId(_personcareplanregularcaskid).FirstOrDefault();
            System.Threading.Thread.Sleep(2000);

            personCarePlansSubPage_RegularCareTasksTab_Record
              .WaitForPersonCarePlansSubPage_Schedules_PageToLoad()
              .ClickRecordCellText(regularcarescheduleid.ToString(), 2);

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("cpregularcaretaskschedule");

            PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage
                .WaitForCareScheludesRecordPageToLoadinDrawerMode()
                .SelectTimeRShift("Time")
                .SetSelectTimeField("11:00")
                .SelectRecurrencePatternPicklist("Weekly")
                .SetRecurEveryXWeek("1")
                .SelectRadioButtonRecurEveryXDayOption("tuesday", 1)
                .clickSaveNCloseBtn();
        }

        [TestProperty("JiraIssueID", "ACC-5844")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-5844 - " +
          "As a care coordinator Verify creating a new record under Care task schedule record within regular care task record" +
          "PreCondition:People record should exist.Person should have some active and inactive regular care tasks.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Regular Care")]
        public void PersonCarePlan_ScheduleRegularCareTask_Testmethod06()
        {
            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);

            _personcareplanregularcaskid = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, false, _personID, _careTaskid, null, "ActiveRegularCareTask", _personCarePlanID);


            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickRegularCareLink();

            personCarePlansSubPage_regularCareTasksTab
                .WaitForPersonCarePlansSubPage_RegularCareTasksTabToLoad()
                .ClickRecordCellText(_personcareplanregularcaskid.ToString(), 2);

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("regularcaretask").ClickOnExpandIcon();

            personCarePlansSubPage_RegularCareTasksTab_Record
                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
                .ClickCareScheduleTab()
                .WaitForPersonCarePlansSubPage_Schedules_PageToLoad()
                .VerifyCareScheduleTitle(true)
                .ClickNewCareScheduleButton();


            PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage
                .WaitForCareScheludesRecordPageToLoadinDrawerMode()
                .SelectTimeRShift("Time")
                .SetSelectTimeField("10:00")
                .SelectRecurrencePatternPicklist("Hourly")
                .SetRecurEveryXHourField("1")
                .clickSaveNCloseBtn();

            System.Threading.Thread.Sleep(3000);

            personCarePlansSubPage_RegularCareTasksTab_Record
                .WaitForPersonCarePlansSubPage_Schedules_PageToLoad()
                .VerifyCareScheduleTitle(true)
                .ClickNewCareScheduleButton();

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("regularcaretask").ClickOnExpandIcon();

            PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage
                .WaitForCareScheludesRecordPageToLoadinDrawerMode()
                .SelectTimeRShift("Time")
                .SetSelectTimeField("10:00")
                .SelectRecurrencePatternPicklist("Daily")
                .SetRecurEveryXDay("1")
                .clickSaveNCloseBtn();
            System.Threading.Thread.Sleep(2000);

            personCarePlansSubPage_RegularCareTasksTab_Record
                .WaitForPersonCarePlansSubPage_Schedules_PageToLoad()
                .VerifyCareScheduleTitle(true)
                .ClickNewCareScheduleButton();

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("regularcaretask").ClickOnExpandIcon();

            PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage
                .WaitForCareScheludesRecordPageToLoadinDrawerMode()
                .SelectTimeRShift("Time")
                .SetSelectTimeField("10:00")
                .SelectRecurrencePatternPicklist("Weekly")
                .SetRecurEveryXWeek("1")
                .SelectRadioButtonRecurEveryXDayOption("tuesday", 1)
                .clickSaveNCloseBtn();

            System.Threading.Thread.Sleep(2000);
            var regularcarescheduleid = dbHelper.cpRegularCareTaskSchedule.GetByCPRegularCareTaskId(_personcareplanregularcaskid).FirstOrDefault();
            System.Threading.Thread.Sleep(2000);


            personCarePlansSubPage_RegularCareTasksTab_Record
              .WaitForPersonCarePlansSubPage_Schedules_PageToLoad()
              .ClickRecordCellText(regularcarescheduleid.ToString(), 2);


            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("cpregularcaretaskschedule");

            PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage
                .WaitForCareScheludesRecordPageToLoadinDrawerMode()
                .SelectTimeRShift("Time")
                .SetSelectTimeField("11:00")
                .SelectRecurrencePatternPicklist("Weekly")
                .SetRecurEveryXWeek("1")
                .SelectRadioButtonRecurEveryXDayOption("tuesday", 1)
                .clickSaveNCloseBtn();

            mainMenu
               .WaitForMainMenuToLoad()
               .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Regular Cares");

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectFilter("1", "Name")
                .SelectOperator("1", "Equals")
                .InsertRuleValueText("1", "Assist with bath or shower1")
                .ClickSearchButton()
                .WaitForResultsPageToLoadwithNoExportToExcel()
                .ValidateSearchResultRecordPresent(_personcareplanregularcaskid.ToString());
        }


        [TestProperty("JiraIssueID", "ACC-5848")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-5848 - " +
         "As a care coordinator Verify creating a new record under Care task schedule record within regular care task record" +
         "PreCondition:People record should exist.Person should have some active and inactive regular care tasks.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Regular Care")]
        public void PersonCarePlan_ScheduleRegularCareTask_Testmethod07()
        {
            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);

            _personcareplanregularcaskid = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, false, _personID, _careTaskid, null, "ActiveRegularCareTask", _personCarePlanID);


            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickRegularCareLink();

            personCarePlansSubPage_regularCareTasksTab
                .WaitForPersonCarePlansSubPage_RegularCareTasksTabToLoad()
                .ClickRecordCellText(_personcareplanregularcaskid.ToString(), 2);

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("regularcaretask").ClickOnExpandIcon();

            personCarePlansSubPage_RegularCareTasksTab_Record
                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
                .ClickCareScheduleTab()
                .WaitForPersonCarePlansSubPage_Schedules_PageToLoad()
                .VerifyCareScheduleTitle(true)
                .ClickNewCareScheduleButton();


            PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage
                .WaitForCareScheludesRecordPageToLoadinDrawerMode()
                .SelectTimeRShift("Time")
                .SetSelectTimeField("10:00")
                .SelectRecurrencePatternPicklist("Hourly")
                .SetRecurEveryXHourField("1")
                .clickSaveNCloseBtn();
            System.Threading.Thread.Sleep(3000);

            personCarePlansSubPage_RegularCareTasksTab_Record
                .WaitForPersonCarePlansSubPage_Schedules_PageToLoad()
                .VerifyCareScheduleTitle(true)
                .ClickNewCareScheduleButton();

            PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage
                .WaitForCareScheludesRecordPageToLoadinDrawerMode()
                .SelectTimeRShift("Time")
                .SetSelectTimeField("10:00")
                .SelectRecurrencePatternPicklist("Daily")
                .SetRecurEveryXDay("1")
                .clickSaveNCloseBtn();
            System.Threading.Thread.Sleep(2000);


            personCarePlansSubPage_RegularCareTasksTab_Record
                .WaitForPersonCarePlansSubPage_Schedules_PageToLoad()
                .VerifyCareScheduleTitle(true)
                .ClickNewCareScheduleButton();

            PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage
                .WaitForCareScheludesRecordPageToLoadinDrawerMode()
                .SelectTimeRShift("Time")
                .SetSelectTimeField("10:00")
                .SelectRecurrencePatternPicklist("Weekly")
                .SetRecurEveryXWeek("1")
                .SelectRadioButtonRecurEveryXDayOption("tuesday", 1)
                .clickSaveNCloseBtn();

            System.Threading.Thread.Sleep(2000);
            var regularcarescheduleid = dbHelper.cpRegularCareTaskSchedule.GetByCPRegularCareTaskId(_personcareplanregularcaskid).FirstOrDefault();
            System.Threading.Thread.Sleep(2000);

            personCarePlansSubPage_RegularCareTasksTab_Record
              .WaitForPersonCarePlansSubPage_Schedules_PageToLoad()
              .ClickRecordCellText(regularcarescheduleid.ToString(), 2);

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("cpregularcaretaskschedule");

            PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage
                .WaitForCareScheludesRecordPageToLoadinDrawerMode()
                .ValidateDeleteButtonVisible(false);


        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1812

        [TestProperty("JiraIssueID", "ACC-5862")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-5862 - " +
            "To verify Care Task Diary record creation when the  Regular Care Task and Schedule record is created." +
            "PreCondition:At least one person record should be present.At least one care plan record should be present.At least one regular care task record should be present.Scenario1 Automated.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Regular Care")]
        public void PersonCarePlan_ScheduleRegularCareTask_Testmethod08()
        {
            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);
            _personcareplanregularcaskid = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, false, _personID, _careTaskid, null, "ActiveRegularCareTask", _personCarePlanID);

            loginPage
                   .GoToLoginPage()
                   .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickRegularCareLink();

            personCarePlansSubPage_regularCareTasksTab
                .WaitForPersonCarePlansSubPage_RegularCareTasksTabToLoad()
                .ClickRecordCellText(_personcareplanregularcaskid.ToString(), 2);

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("regularcaretask").ClickOnExpandIcon();

            personCarePlansSubPage_RegularCareTasksTab_Record
                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
                .ClickCareScheduleTab()
                .WaitForPersonCarePlansSubPage_Schedules_PageToLoad()
                .VerifyCareScheduleTitle(true)
                .ClickNewCareScheduleButton();


            PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage
                .WaitForCareScheludesRecordPageToLoadinDrawerMode()
                .SelectTimeRShift("Time")
                .SetSelectTimeField("10:00")
                .SelectRecurrencePatternPicklist("Hourly")
                .SetRecurEveryXHourField("2")
                .clickSaveNCloseBtn();
            System.Threading.Thread.Sleep(3000);

            personCarePlansSubPage_RegularCareTasksTab_Record
                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
                .ClickDetailsTab()
                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
                .ClickSaveNClose();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCareDiaryLink();

            DateTime startdate = DateTime.Now.AddDays(7);
            var careDiaryIDs = dbHelper.cpRegularCareTaskDiary.GetByPersonIdAndCarePlanId(_personID, _personCarePlanID);
            var CareDiaryCount = careDiaryIDs.Count();
            System.Threading.Thread.Sleep(1000);

            CareDiaryPage
               .WaitForPersonCarePlansSubPage_CareDiaryPageToLoad()
               .SelectRecordPicklist("Related Records")
               .ValidateCareDiaryRecordCount("1 - " + CareDiaryCount.ToString());
        }

        [TestProperty("JiraIssueID", "ACC-5891")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-5891 - " +
           "To verify Care Task Diary record creation when the  Regular Care Task and Schedule record is created." +
           "PreCondition:At least one person record should be present.At least one care plan record should be present.At least one regular care task record should be present.Scenario2 Automated.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Regular Care")]
        public void PersonCarePlan_ScheduleRegularCareTask_Testmethod09()
        {
            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);
            _personcareplanregularcaskid = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, false, _personID, _careTaskid, null, "ActiveRegularCareTask", _personCarePlanID);

            loginPage
                   .GoToLoginPage()
                   .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickRegularCareLink();

            personCarePlansSubPage_regularCareTasksTab
                .WaitForPersonCarePlansSubPage_RegularCareTasksTabToLoad()
                .ClickRecordCellText(_personcareplanregularcaskid.ToString(), 2);

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("regularcaretask").ClickOnExpandIcon();

            personCarePlansSubPage_RegularCareTasksTab_Record
                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
                .ClickCareScheduleTab()
                .WaitForPersonCarePlansSubPage_Schedules_PageToLoad()
                .VerifyCareScheduleTitle(true)
                .ClickNewCareScheduleButton();


            PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage
                .WaitForCareScheludesRecordPageToLoadinDrawerMode()
                .SelectTimeRShift("Time")
                .SetSelectTimeField("10:00")
                .SelectRecurrencePatternPicklist("Daily")
                .SetRecurEveryXDay("2")
                .clickSaveNCloseBtn();
            System.Threading.Thread.Sleep(3000);

            personCarePlansSubPage_RegularCareTasksTab_Record
                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
                .ClickDetailsTab()
                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
                .ClickSaveNClose();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCareDiaryLink();

            DateTime startdate = DateTime.Now.AddDays(7);
            var careDiaryIDs = dbHelper.cpRegularCareTaskDiary.GetByPersonIdAndCarePlanId(_personID, _personCarePlanID);
            var CareDiaryCount = careDiaryIDs.Count();
            System.Threading.Thread.Sleep(1000);

            CareDiaryPage
               .WaitForPersonCarePlansSubPage_CareDiaryPageToLoad()
               .SelectRecordPicklist("Related Records")
               .ValidateCareDiaryRecordCount("1 - " + CareDiaryCount.ToString());
        }

        [TestProperty("JiraIssueID", "ACC-5914")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-5914 - " +
          "To verify Care Task Diary record creation when the  Regular Care Task and Schedule record is created." +
          "PreCondition:At least one person record should be present.At least one care plan record should be present.At least one regular care task record should be present.Scenario3 Automated.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Regular Care")]
        public void PersonCarePlan_ScheduleRegularCareTask_Testmethod010()
        {
            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);
            _personcareplanregularcaskid = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, false, _personID, _careTaskid, null, "ActiveRegularCareTask", _personCarePlanID);

            loginPage
                   .GoToLoginPage()
                   .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickRegularCareLink();

            personCarePlansSubPage_regularCareTasksTab
                .WaitForPersonCarePlansSubPage_RegularCareTasksTabToLoad()
                .ClickRecordCellText(_personcareplanregularcaskid.ToString(), 2);

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("regularcaretask").ClickOnExpandIcon();

            personCarePlansSubPage_RegularCareTasksTab_Record
                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
                .ClickCareScheduleTab()
                .WaitForPersonCarePlansSubPage_Schedules_PageToLoad()
                .VerifyCareScheduleTitle(true)
                .ClickNewCareScheduleButton();

            DateTime startdate = DateTime.Now.AddDays(-16);

            PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage
                .WaitForCareScheludesRecordPageToLoadinDrawerMode()
                .SetStartDateFieldValue(startdate.ToString("dd'/'MM'/'yyyy"))
                .SelectTimeRShift("Time")
                .SetSelectTimeField("10:00")
                .SelectRecurrencePatternPicklist("Daily")
                .SetRecurEveryXDay("2")
                .clickSaveNCloseBtn();
            System.Threading.Thread.Sleep(3000);

            personCarePlansSubPage_RegularCareTasksTab_Record
                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
                .ClickDetailsTab()
                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
                .ClickSaveNClose();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCareDiaryLink();

            var careDiaryIDs = dbHelper.cpRegularCareTaskDiary.GetByPersonIdAndCarePlanId(_personID, _personCarePlanID);
            var CareDiaryCount = careDiaryIDs.Count();
            System.Threading.Thread.Sleep(1000);

            CareDiaryPage
               .WaitForPersonCarePlansSubPage_CareDiaryPageToLoad()
               .SelectRecordPicklist("Related Records")
               .ValidateCareDiaryRecordCount("1 - " + CareDiaryCount.ToString());
        }

        [TestProperty("JiraIssueID", "ACC-5938")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-5938 - " +
         "To verify Care Task Diary record creation when the  Regular Care Task and Schedule record is created." +
         "PreCondition:At least one person record should be present.At least one care plan record should be present.At least one regular care task record should be present.Scenario4 Automated.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Regular Care")]
        public void PersonCarePlan_ScheduleRegularCareTask_Testmethod011()
        {
            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);
            _personcareplanregularcaskid = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, false, _personID, _careTaskid, null, "ActiveRegularCareTask", _personCarePlanID);

            loginPage
                   .GoToLoginPage()
                   .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickRegularCareLink();

            personCarePlansSubPage_regularCareTasksTab
                .WaitForPersonCarePlansSubPage_RegularCareTasksTabToLoad()
                .ClickRecordCellText(_personcareplanregularcaskid.ToString(), 2);

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("regularcaretask").ClickOnExpandIcon();

            personCarePlansSubPage_RegularCareTasksTab_Record
                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
                .ClickCareScheduleTab()
                .WaitForPersonCarePlansSubPage_Schedules_PageToLoad()
                .VerifyCareScheduleTitle(true)
                .ClickNewCareScheduleButton();

            DateTime startdate = DateTime.Now.AddDays(-16);

            PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage
                .WaitForCareScheludesRecordPageToLoadinDrawerMode()
                .SetStartDateFieldValue(startdate.ToString("dd'/'MM'/'yyyy"))
                .SelectTimeRShift("Time")
                .SetSelectTimeField("10:00")
                .SelectRecurrencePatternPicklist("Daily")
                .SetRecurEveryXDay("2")
                .clickSaveNCloseBtn();
            System.Threading.Thread.Sleep(3000);

            personCarePlansSubPage_RegularCareTasksTab_Record
                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
                .ClickDetailsTab()
                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
                .ClickSaveNClose();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCareDiaryLink();

            System.Threading.Thread.Sleep(2000);
            var careDiaryIDs = dbHelper.cpRegularCareTaskDiary.GetByPersonIdAndCarePlanId(_personID, _personCarePlanID);
            var CareDiaryCount = careDiaryIDs.Count();

            CareDiaryPage
               .WaitForPersonCarePlansSubPage_CareDiaryPageToLoad()
               .SelectRecordPicklist("Related Records")
               .ValidateCareDiaryRecordCount("1 - " + CareDiaryCount.ToString());

            personCarePlansSubPage
               .WaitForPersonCarePlansSubPageToLoad()
               .ClickRegularCareLink();

            personCarePlansSubPage_regularCareTasksTab
                .WaitForPersonCarePlansSubPage_RegularCareTasksTabToLoad()
                .ClickRecordCellText(_personcareplanregularcaskid.ToString(), 2);

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("regularcaretask").ClickOnExpandIcon();

            personCarePlansSubPage_RegularCareTasksTab_Record
                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
                .ClickCareScheduleTab()
                .WaitForPersonCarePlansSubPage_Schedules_PageToLoad();
            System.Threading.Thread.Sleep(2000);
            var regularcarescheduleid = dbHelper.cpRegularCareTaskSchedule.GetByCPRegularCareTaskId(_personcareplanregularcaskid).FirstOrDefault();
            System.Threading.Thread.Sleep(2000);


            personCarePlansSubPage_RegularCareTasksTab_Record
                 .WaitForPersonCarePlansSubPage_Schedules_PageToLoad()
                 .ClickRecordCellText(regularcarescheduleid.ToString(), 2);

            DateTime enddate = DateTime.Now;


            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("cpregularcaretaskschedule").ClickOnExpandIcon();

            PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage
                .WaitForPersonCarePlansSubPage_RegularCareTasks_CareScheludesRecordPageToLoad()
                .SetEndDateFieldValue(enddate.ToString("dd'/'MM'/'yyyy"))
                .clickSaveNCloseBtn();


            System.Threading.Thread.Sleep(3000);

            personCarePlansSubPage_RegularCareTasksTab_Record
                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
                .ClickDetailsTab()
                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
                .ClickSaveNClose();


            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCareDiaryLink();

            var NocareDiaryIDs = dbHelper.cpRegularCareTaskDiary.GetByPersonIdAndCarePlanId(_personID, _personCarePlanID);
            var NoCareDiaryCount = NocareDiaryIDs.Count();
            System.Threading.Thread.Sleep(1000);

            CareDiaryPage
               .WaitForPersonCarePlansSubPage_CareDiaryPageToLoad()
               .SelectRecordPicklist("Related Records")
               .ValidateCareDiaryRecordCount("0 - " + NoCareDiaryCount.ToString());


        }
        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5622

        [TestProperty("JiraIssueID", "ACC-6131")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-6131 - " +
        "To verify the Care Task Schedule Diary record deletion in Care Task Schedule form." +
        "PreCondition:At least one person record should be present.At least one care plan record should be present.At least one regular care task record should be present.Atleast one existing care schedule should be present")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Regular Care")]
        public void PersonCarePlan_ScheduleRegularCareTask_Testmethod012()
        {
            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);
            _personcareplanregularcaskid = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, false, _personID, _careTaskid, null, "ActiveRegularCareTask", _personCarePlanID);
            _personcareplanregularcaskscheduleid = dbHelper.cpRegularCareTaskSchedule.CreateCPBookingRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _personID, _personcareplanregularcaskid, _personCarePlanID, DateTime.Now, new TimeSpan(10, 0, 0), 2, 1, DateTime.Now);
            loginPage
                   .GoToLoginPage()
                   .Login(_adminSystemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickRegularCareLink();

            personCarePlansSubPage_regularCareTasksTab
                .WaitForPersonCarePlansSubPage_RegularCareTasksTabToLoad()
                .ClickRecordCellText(_personcareplanregularcaskid.ToString(), 2);

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("regularcaretask").ClickOnExpandIcon();

            personCarePlansSubPage_RegularCareTasksTab_Record
                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
                .ClickCareScheduleTab()
                .WaitForPersonCarePlansSubPage_Schedules_PageToLoad()
                .ClickRecordCellText(_personcareplanregularcaskscheduleid.ToString(), 2);

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("cpregularcaretaskschedule");

            PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage
               .WaitForCareScheludesRecordPageToLoadinDrawerMode()
               .ClickInactive_YesRadioButton()
               .clickSaveNCloseBtn();

            System.Threading.Thread.Sleep(3000);

            personCarePlansSubPage_RegularCareTasksTab_Record
                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
                .ClickDetailsTab()
                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
                .ClickDeleteRecord();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            dynamicDialogPopup
               .WaitForCareCloudDynamicDialoguePopUpToLoad()
               .ValidateMessage("Related record exists in Care Schedule. Please delete related records before deleting record in Regular Care.")
               .TapCloseButton();

            personCarePlansSubPage_regularCareTasksTab
                .WaitForPersonCarePlansSubPage_RegularCareTasksTabToLoad();

        }

        [TestProperty("JiraIssueID", "ACC-6138")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-6138 - " +
       "To verify Care Task Diary record creation through schedule job to process Regular Care Task schedule." +
       "PreCondition:At least one person record should be present.At least one care plan record should be present.At least one regular care task record should be present.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Regular Care")]
        public void PersonCarePlan_ScheduleRegularCareTask_Testmethod013()
        {
            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);
            _personcareplanregularcaskid = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, false, _personID, _careTaskid, null, "ActiveRegularCareTask", _personCarePlanID);
            var scheduleJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Create Regular Care Task Diaries By Schedules")[0]; //Create Regular Care Task Diaries By Schedules

            loginPage
                   .GoToLoginPage()
                   .Login(_adminSystemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickRegularCareLink();

            personCarePlansSubPage_regularCareTasksTab
                .WaitForPersonCarePlansSubPage_RegularCareTasksTabToLoad()
                .ClickRecordCellText(_personcareplanregularcaskid.ToString(), 2);

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("regularcaretask").ClickOnExpandIcon();

            personCarePlansSubPage_RegularCareTasksTab_Record
                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
                .ClickCareScheduleTab()
                .WaitForPersonCarePlansSubPage_Schedules_PageToLoad()
                .ClickNewCareScheduleButton();

            PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage
                 .WaitForCareScheludesRecordPageToLoadinDrawerMode()
                 .SelectTimeRShift("Time")
                 .SetSelectTimeField("10:00")
                 .SelectRecurrencePatternPicklist("Daily")
                 .SetRecurEveryXDay("1")
                 .clickSaveNCloseBtn();

            System.Threading.Thread.Sleep(2000);

            personCarePlansSubPage_RegularCareTasksTab_Record
               .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
               .ClickDetailsTab()
               .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad();

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemManagementSection();

            systemManagementPage
                .WaitForSystemManagementPageToLoad()
                .ClickScheduledJobsLink();

            scheduleJobsPage
                .WaitForScheduleJobsPageToLoad()
                .SearchRecord("Create Regular Care Task Diaries By Schedules")
                .OpenRecord(scheduleJobId.ToString());

            scheduleJobsRecordPage
                .WaitForScheduleJobsRecordPageToLoad()
                .TapExecuteJobButton()
                .TapSaveAndCloseButton();

            scheduleJobsPage
                .WaitForScheduleJobsPageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickRegularCareLink();

            personCarePlansSubPage_regularCareTasksTab
                .WaitForPersonCarePlansSubPage_RegularCareTasksTabToLoad()
                .ClickRecordCellText(_personcareplanregularcaskid.ToString(), 2);

            System.Threading.Thread.Sleep(2000);
            var regularcarescheduleid = dbHelper.cpRegularCareTaskSchedule.GetByCPRegularCareTaskId(_personcareplanregularcaskid).FirstOrDefault();
            System.Threading.Thread.Sleep(2000);

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("regularcaretask").ClickOnExpandIcon();

            personCarePlansSubPage_RegularCareTasksTab_Record
                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
                .ClickCareScheduleTab()
                .WaitForPersonCarePlansSubPage_Schedules_PageToLoad()
                .ClickRecordCellText(regularcarescheduleid.ToString(), 2);

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("cpregularcaretaskschedule");

            PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage
               .WaitForCareScheludesRecordPageToLoadinDrawerMode()
               .ValidateLastRunDate(DateTime.Today.Date.ToString("dd'/'MM'/'yyyy"))
               .clickSaveNCloseBtn();

            System.Threading.Thread.Sleep(3000);

            personCarePlansSubPage_RegularCareTasksTab_Record
                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
                .ClickDetailsTab()
                .WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
                .ClickSaveNClose();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCareDiaryLink();

            var NocareDiaryIDs = dbHelper.cpRegularCareTaskDiary.GetByPersonIdAndCarePlanId(_personID, _personCarePlanID);
            var NoCareDiaryCount = NocareDiaryIDs.Count();
            System.Threading.Thread.Sleep(1000);

            CareDiaryPage
               .WaitForPersonCarePlansSubPage_CareDiaryPageToLoad()
               .SelectRecordPicklist("Related Records")
               .ValidateCareDiaryRecordCount("1 - " + NoCareDiaryCount.ToString());

        }

        #endregion
    }

}


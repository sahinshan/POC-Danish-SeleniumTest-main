using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phoenix.DBHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.UITests.PersonAbsence
{
    /// <summary>
    /// This class contains Automated UI test scripts for Regular Care Tasks
    /// </summary>
    [TestClass]
    public class PersonAbsence_Booking_UITestCases : FunctionalTest
    {
        private Guid _languageId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private Guid _maritalStatusId;
        private Guid _systemUserId;
        private Guid _adminSystemUserId;
        private string _systemUsername;
        private string _adminSystemUsername;
        private Guid _bookingType5;
        private Guid _bookingType6;
        private Guid _providereId1;
        private string _personFirstName;
        private Guid _personID;
        private int _personNumber;
        private string _personFullName;
        private Guid _schedulingsetupid = new Guid("8B61B44F-B485-EC11-A350-0050569231CF");//Scheduling setup id
        private Guid _cpPersonabsencereasonid;
        private string _cpPersonabsencereasonName;
        private Guid _contractschemeid;
        private Guid _personcontractId;
        private Guid _careProviderStaffRoleTypeid;
        private Guid _employmentContractTypeid1;
        private Guid _availabilityTypes_StandardId;
        private Guid _recurrencePattern_Every1WeekMondayId;
        private Guid _recurrencePattern_Every1WeekTuesdayId;
        private Guid _recurrencePattern_Every1WeekWednesdayId;
        private Guid _recurrencePattern_Every1WeekThursdayId;
        private Guid _recurrencePattern_Every1WeekFridayId;
        private Guid _recurrencePattern_Every1WeekSaturdayId;
        private Guid _recurrencePattern_Every1WeekSundayId;
        private Guid _recurrencePatternId_EveryDay;
        private Guid _cpabsenceId;



        [TestInitialize()]
        public void PersonAbsence_SetupTest()
        {

            try
            {
                #region Business Unit

                _careDirectorQA_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

                #endregion

                #region Providers

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Team

                _careDirectorQA_TeamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion

                #region Marital Status

                _maritalStatusId = commonMethodsDB.CreateMaritalStatus("Civil Partner", new DateTime(2000, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Lanuage

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_careDirectorQA_TeamId, "PersonCarePlan_Ethnicity", new DateTime(2020, 1, 1));

                #endregion

                #region SecurityProfiles

                var userSecProfiles = new List<Guid>();

                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Bed Management (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Bed Management Setup (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Create Person Absences")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Create Person Contract and Person Contract Services")[0]);
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

                #endregion

                #region Create SystemUser 

                _systemUsername = "TestUser9oct5" + DateTime.Now.ToString("yyyyMMddHHmmss");
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "ServiceProvisions", "User4", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid, userSecProfiles);
                dbHelper.systemUser.UpdateEmployeeTypeId(_systemUserId, 3);

                #endregion

                #region Create Admin SystemUser 


                _adminSystemUsername = "AdminUser";
                _adminSystemUserId = commonMethodsDB.CreateSystemUserRecord(_adminSystemUsername, "AdvancedSearch", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
                dbHelper.systemUser.UpdateEmployeeTypeId(_systemUserId, 3);


                #endregion

                #region Booking Type 5 -> "Booking (to service user)" 

                _bookingType5 = commonMethodsDB.CreateCPBookingType("BookingType-5", 5, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 2, null, 1440);

                _bookingType6 = commonMethodsDB.CreateCPBookingType("BookingType-6", 6, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 2, true, 1440);
                dbHelper.cpBookingType.UpdateIsAbsence(_bookingType6, true);

                #endregion

                #region update Care Provider scheduling set up

                dbHelper.cPSchedulingSetup.UpdateCPScheduleSetup(_schedulingsetupid, _bookingType6);

                #endregion

                #region Provider 1

                _providereId1 = commonMethodsDB.CreateProvider("Provider_" + DateTime.Now.ToString("yyyyMMdd"), _careDirectorQA_TeamId, 13, true); //create a "Residential Establishment" provider
                commonMethodsDB.CreateProviderAllowableBookingTypes(_careDirectorQA_TeamId, _providereId1, _bookingType5, true);
                commonMethodsDB.CreateProviderAllowableBookingTypes(_careDirectorQA_TeamId, _providereId1, _bookingType6, false);

                #endregion

                #region Person

                _personFirstName = "Person_Absence" + DateTime.Now.ToString("yyyyMMddHHmmss");
                var lastName = "LN_CDV6_24104";
                var personRecordExists = dbHelper.person.GetByFirstName(_personFirstName).Any();

                _personID = dbHelper.person.CreatePersonRecord("", _personFirstName, "", lastName, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
                _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

                _personFullName = _personFirstName + lastName;

                #endregion

                #region create contract scheme

                _contractschemeid = commonMethodsDB.CreateCareProviderContractScheme(_careDirectorQA_TeamId, _systemUserId, _careDirectorQA_BusinessUnitId, "Contract-Scheme-002", new DateTime(2000, 1, 2), 734, _providereId1, _providereId1);

                #endregion

                #region create person contract


                _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_careDirectorQA_TeamId, "title", _personID, _systemUserId, _providereId1, _contractschemeid, _providereId1, new DateTime(2023, 6, 1));
                dbHelper.careProviderPersonContract.UpdatePcIsEnabledForScheduleBooking(_personcontractId, true);

                #endregion

                #region Care provider staff role type

                _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careDirectorQA_TeamId, "Role-001", "1234", null, new DateTime(2020, 1, 1), null);

                #endregion

                #region Employment Contract Type - Salaried

                _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_careDirectorQA_TeamId, "Salaried", "", null, new DateTime(2020, 1, 1));

                #endregion

                #region Create Care Provider Person Absence Reason

                _cpPersonabsencereasonName = "Person Absence Reason Hospital";
                _cpPersonabsencereasonid = commonMethodsDB.CreateCPPersonAbsenceReason(_cpPersonabsencereasonName, _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, 123, DateTime.Today);

                #endregion

                #region Availability Type
                _availabilityTypes_StandardId = commonMethodsDB.CreateAvailabilityType("Standard", 3, false, _careDirectorQA_TeamId, 1, 1, true);


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

                #region Care Provider Scheduling Setup

                var cPSchedulingSetupId = dbHelper.cPSchedulingSetup.GetAllActiveRecords().FirstOrDefault();
                dbHelper.cPSchedulingSetup.UpdateCheckStaffAvailability(cPSchedulingSetupId, 4); //Check and Offer Create

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
            int diff = (7 + (dt.DayOfWeek - DayOfWeek.Monday)) % 7;
            return dt.AddDays(-1 * diff).Date;

        }

        #region https://advancedcsg.atlassian.net/browse/ACC-2067

        [TestProperty("JiraIssueID", "ACC-3562")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-24104 - " +
            "As a Care Coordinator Verify the created booking for person absence.Pre-Condition-> Person record should exist-> Should have active person contract to create a person absence-> Default Booking Type for Person Absence should be selected in Scheduling Setup" +
            "navigate to People Diary and verify created absence booking by selecting required provider.Verify the created booking on Person Diary for created person absence.Verify populated fields on Booking Diary from Person absence")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("BusinessModule2", "Person Contracts")]
        [TestProperty("BusinessModule3", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Person Absences")]
        [TestProperty("Screen2", "Person Contracts")]
        [TestProperty("Screen3", "People Diary")]
        //open bug: https://advancedcsg.atlassian.net/browse/ACC-7454
        public void PersonAbsence_VerifyAbsenceBooking_01()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);
            var provider = (string)(dbHelper.provider.GetProviderByID(_providereId1, "name"))["name"];


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
                .NavigateToHealthPersonAbsencesPage();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickNewRecordButton();

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .ClickHealthAbsenceReasonLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectLookIn("Lookup Records")
               .TypeSearchQuery(_cpPersonabsencereasonName)
               .TapSearchButton()
               .SelectResultElement(_cpPersonabsencereasonid.ToString());

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .InsertHealthAbsencePlannedStartDateTime(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndDateTime(DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSaveNClose();

            dynamicDialogPopup
                .WaitForCareCloudDynamicDialoguePopUpToLoad()
                .ValidateMessage("No bookings exist during the absence period")
                .TapCloseButton();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickRefreshPage();


            //Get the PersonAbsenceID
            var _cppersonabsencid = dbHelper.cpPersonAbsence.GetByPersonId(_personID).FirstOrDefault();

            //Get the Booking Diary Id
            var _cpbookingdiaryid = dbHelper.cPBookingDiary.GetCPBookingIdByCreator(_systemUserId).First();

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .NavigateToPersonContractsPage();
            

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId.ToString());

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonAbsenceTab();

            personAbsencesPage
                .WaitForPersonAbsencesPageToLoadInsidePersonContractPage()
                .OpenRecord(_cppersonabsencid.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("cppersonabsence").ClickOnExpandIcon();

            personAbsencesRecordPage
                .WaitForPersonAbsencesPageToLoad()
                .ValidatePersonAbsence_PlannedStartDate(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
                .ValidatePersonAbsence_PlannedEndDate(DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
                .ValidatePersonAbsence_DurationInDays("1.00")
                .ValidatePersonAbsence_DurationInHours("24.00");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleDiarySection();

            

            String Date = DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy");
            String NextDate = DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy");

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .selectProvider(provider + " - No Address")
                .WaitForPeopleDiaryPageToLoad()
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(Convert.ToDateTime(Date));

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .MouseHoverDiaryBooking(_cpbookingdiaryid.ToString())
                .ValidateTimeLabelText("Planned Time: " + Date + " 00:00 - " + NextDate + " 00:00")
                .ValidateBookingTypeLabelText("Booking Type: BookingType-6");

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-2670

        [TestProperty("JiraIssueID", "ACC-3563")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-24106 - " +
            "As a care coordinator verify system behaviour to create a booking for person absence based on person absence type.Pre-Condition-> Person record should exist-> Should have active person contract to create a person absence-> Default Booking Type for Person Absence should be selected in Scheduling Setup" +
            "navigate to People Diary and verify created absence booking by selecting required provider.Verify the created booking on Person Diary for created person absence.Verify populated fields on Booking Diary from Person absence")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("BusinessModule2", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Person Absences")]
        [TestProperty("Screen2", "People Diary")]
        public void PersonAbsence_VerifyAbsenceBooking_02()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);
            var provider = (string)(dbHelper.provider.GetProviderByID(_providereId1, "name"))["name"];



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
                .NavigateToHealthPersonAbsencesPage();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickNewRecordButton();

            //Verify system behavior while creating a person absence record as below which is of Open-ended Absence type  
            //Planned Start=Yes, Planned End=No, Actual Start=No, Actual End=No

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .ClickHealthAbsenceReasonLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectLookIn("Lookup Records")
               .TypeSearchQuery(_cpPersonabsencereasonName)
               .TapSearchButton()
               .SelectResultElement(_cpPersonabsencereasonid.ToString());

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .InsertHealthAbsencePlannedStartDateTime(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSaveNClose();


            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickRefreshPage();

            mainMenu
                .WaitForMainMenuToLoad(true, true, true, false, false, false)
                .NavigateToPeopleDiarySection();


            String Date = DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy");

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .selectProvider(provider + " - No Address")
                .WaitForPeopleDiaryPageToLoad()
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(Convert.ToDateTime(Date));

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .ValidateDiaryBooking(_personcontractId.ToString());
        }

        //Step-4 Actual Start DAte -yes
        [TestProperty("JiraIssueID", "ACC-3564")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-24106 - " +
            "As a care coordinator verify system behaviour to create a booking for person absence based on person absence type.Pre-Condition-> Person record should exist-> Should have active person contract to create a person absence-> Default Booking Type for Person Absence should be selected in Scheduling Setup" +
            "navigate to People Diary and verify created absence booking by selecting required provider.Verify the created booking on Person Diary for created person absence.Verify populated fields on Booking Diary from Person absence")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("BusinessModule2", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Person Absences")]
        [TestProperty("Screen2", "People Diary")]
        public void PersonAbsence_VerifyAbsenceBooking_03()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

            var provider = (string)(dbHelper.provider.GetProviderByID(_providereId1, "name"))["name"];


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
                .NavigateToHealthPersonAbsencesPage();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickNewRecordButton();

            //Verify system behavior while creating a person absence record as below which is of Open-ended Absence type  
            //Planned Start=No, Planned End=No, Actual Start=Yes, Actual End=No

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .ClickHealthAbsenceReasonLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectLookIn("Lookup Records")
               .TypeSearchQuery(_cpPersonabsencereasonName)
               .TapSearchButton()
               .SelectResultElement(_cpPersonabsencereasonid.ToString());

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .InsertHealthAbsenceActualStartDate(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSaveNClose();


            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickRefreshPage();

            mainMenu
                .WaitForMainMenuToLoad(true, true, true, false, false, false)
                .NavigateToPeopleDiarySection();


            String Date = DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy");


            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .selectProvider(provider + " - No Address")
                .WaitForPeopleDiaryPageToLoad()
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(Convert.ToDateTime(Date));

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .ValidateDiaryBooking(_personcontractId.ToString());
        }

        //Step 5 Planned Start - Yes,Actual Start DAte -yes
        [TestProperty("JiraIssueID", "ACC-3565")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-24106 - " +
            "As a care coordinator verify system behaviour to create a booking for person absence based on person absence type.Pre-Condition-> Person record should exist-> Should have active person contract to create a person absence-> Default Booking Type for Person Absence should be selected in Scheduling Setup" +
            "navigate to People Diary and verify created absence booking by selecting required provider.Verify the created booking on Person Diary for created person absence.Verify populated fields on Booking Diary from Person absence")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("BusinessModule2", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Person Absences")]
        [TestProperty("Screen2", "People Diary")]
        public void PersonAbsence_VerifyAbsenceBooking_04()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

            var provider = (string)(dbHelper.provider.GetProviderByID(_providereId1, "name"))["name"];


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
                .NavigateToHealthPersonAbsencesPage();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickNewRecordButton();

            //Verify system behavior while creating a person absence record as below which is of Open-ended Absence type  
            //Planned Start=Yes, Planned End=No, Actual Start=Yes, Actual End=No

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .ClickHealthAbsenceReasonLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectLookIn("Lookup Records")
               .TypeSearchQuery(_cpPersonabsencereasonName)
               .TapSearchButton()
               .SelectResultElement(_cpPersonabsencereasonid.ToString());

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .InsertHealthAbsencePlannedStartDateTime(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceActualStartDate(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSaveNClose();


            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickRefreshPage();

            mainMenu
                .WaitForMainMenuToLoad(true, true, true, false, false, false)
                .NavigateToPeopleDiarySection();


            String Date = DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy");

            peopleDiaryPage
               .WaitForPeopleDiaryPageToLoad()
               .selectProvider(provider + " - No Address")
               .WaitForPeopleDiaryPageToLoad()
               .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(Convert.ToDateTime(Date));

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .ValidateDiaryBooking(_personcontractId.ToString());
        }

        //Step 6 Planned Start - Yes,Planned End DAte -yes
        [TestProperty("JiraIssueID", "ACC-3566")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-24106 - " +
            "As a care coordinator verify system behaviour to create a booking for person absence based on person absence type.Pre-Condition-> Person record should exist-> Should have active person contract to create a person absence-> Default Booking Type for Person Absence should be selected in Scheduling Setup" +
            "navigate to People Diary and verify created absence booking by selecting required provider.Verify the created booking on Person Diary for created person absence.Verify populated fields on Booking Diary from Person absence")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("BusinessModule2", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Person Absences")]
        [TestProperty("Screen2", "People Diary")]
        public void PersonAbsence_VerifyAbsenceBooking_05()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

            var provider = (string)(dbHelper.provider.GetProviderByID(_providereId1, "name"))["name"];


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
                .NavigateToHealthPersonAbsencesPage();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickNewRecordButton();

            //Verify system behavior while creating a person absence record as below which is of Open-ended Absence type  
            //Planned Start=Yes, Planned End=Yes, Actual Start=No, Actual End=No

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .ClickHealthAbsenceReasonLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectLookIn("Lookup Records")
               .TypeSearchQuery(_cpPersonabsencereasonName)
               .TapSearchButton()
               .SelectResultElement(_cpPersonabsencereasonid.ToString());

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .InsertHealthAbsencePlannedStartDateTime(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndDateTime(DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSaveNClose();


            dynamicDialogPopup
                 .WaitForCareCloudDynamicDialoguePopUpToLoad()
                 .ValidateMessage("No bookings exist during the absence period")
                 .TapCloseButton();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickRefreshPage();


            //Get the PersonAbsenceID
            var _cppersonabsencid = dbHelper.cpPersonAbsence.GetByPersonId(_personID).FirstOrDefault();

            //Get the Booking Diary Id
            var _cpbookingdiaryid = dbHelper.cPBookingDiary.GetCPBookingIdByCreator(_systemUserId).First();


            mainMenu
                .WaitForMainMenuToLoad(true, true, true, false, false, false)
                .NavigateToPeopleDiarySection();


            String Date = DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy");
            String NextDate = DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy");

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .selectProvider(provider + " - No Address")
                .WaitForPeopleDiaryPageToLoad()
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(Convert.ToDateTime(Date));

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .MouseHoverDiaryBooking(_cpbookingdiaryid.ToString())
                .ValidateTimeLabelText("Planned Time: " + Date + " 00:00 - " + NextDate + " 00:00")
                .ValidateBookingTypeLabelText("Booking Type: BookingType-6");



        }

        //Step 7 Planned End DAte -yes,Actual Start Date-Yes
        [TestProperty("JiraIssueID", "ACC-3567")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-24106 - " +
            "As a care coordinator verify system behaviour to create a booking for person absence based on person absence type.Pre-Condition-> Person record should exist-> Should have active person contract to create a person absence-> Default Booking Type for Person Absence should be selected in Scheduling Setup" +
            "navigate to People Diary and verify created absence booking by selecting required provider.Verify the created booking on Person Diary for created person absence.Verify populated fields on Booking Diary from Person absence")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("BusinessModule2", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Person Absences")]
        [TestProperty("Screen2", "People Diary")]
        public void PersonAbsence_VerifyAbsenceBooking_06()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);
            var provider = (string)(dbHelper.provider.GetProviderByID(_providereId1, "name"))["name"];



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
                .NavigateToHealthPersonAbsencesPage();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickNewRecordButton();

            //Verify system behavior while creating a person absence record as below which is of Open-ended Absence type  
            //Planned Start=No, Planned End=Yes, Actual Start=Yes, Actual End=No

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .ClickHealthAbsenceReasonLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectLookIn("Lookup Records")
               .TypeSearchQuery(_cpPersonabsencereasonName)
               .TapSearchButton()
               .SelectResultElement(_cpPersonabsencereasonid.ToString());

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .InsertHealthAbsenceActualStartDate(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndDateTime(DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSaveNClose();


            dynamicDialogPopup
                 .WaitForCareCloudDynamicDialoguePopUpToLoad()
                 .ValidateMessage("No bookings exist during the absence period")
                 .TapCloseButton();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickRefreshPage();


            //Get the PersonAbsenceID
            var _cppersonabsencid = dbHelper.cpPersonAbsence.GetByPersonId(_personID).FirstOrDefault();

            //Get the Booking Diary Id
            var _cpbookingdiaryid = dbHelper.cPBookingDiary.GetCPBookingIdByCreator(_systemUserId).First();


            mainMenu
                .WaitForMainMenuToLoad(true, true, true, false, false, false)
                .NavigateToPeopleDiarySection();


            String Date = DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy");
            String NextDate = DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy");

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .selectProvider(provider + " - No Address")
                .WaitForPeopleDiaryPageToLoad()
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(Convert.ToDateTime(Date));

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .MouseHoverDiaryBooking(_cpbookingdiaryid.ToString())
                .ValidateTimeLabelText("Planned Time: " + Date + " 00:00 - " + NextDate + " 00:00")
                .ValidateBookingTypeLabelText("Booking Type: BookingType-6");


        }

        //Step 8 Actual Start Date-Yes,Actual End Date-Yes
        [TestProperty("JiraIssueID", "ACC-3568")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-24106 - " +
            "As a care coordinator verify system behaviour to create a booking for person absence based on person absence type.Pre-Condition-> Person record should exist-> Should have active person contract to create a person absence-> Default Booking Type for Person Absence should be selected in Scheduling Setup" +
            "navigate to People Diary and verify created absence booking by selecting required provider.Verify the created booking on Person Diary for created person absence.Verify populated fields on Booking Diary from Person absence")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("BusinessModule2", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Person Absences")]
        [TestProperty("Screen2", "People Diary")]
        public void PersonAbsence_VerifyAbsenceBooking_07()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);
            var provider = (string)(dbHelper.provider.GetProviderByID(_providereId1, "name"))["name"];



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
                .NavigateToHealthPersonAbsencesPage();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickNewRecordButton();

            //Verify system behavior while creating a person absence record as below which is of Open-ended Absence type  
            //Planned Start=No, Planned End=No, Actual Start=Yes, Actual End=Yes

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .ClickHealthAbsenceReasonLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectLookIn("Lookup Records")
               .TypeSearchQuery(_cpPersonabsencereasonName)
               .TapSearchButton()
               .SelectResultElement(_cpPersonabsencereasonid.ToString());

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .InsertHealthAbsenceActualStartDate(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceActualEndDate(DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSaveNClose();


            dynamicDialogPopup
                 .WaitForCareCloudDynamicDialoguePopUpToLoad()
                 .ValidateMessage("No bookings exist during the absence period")
                 .TapCloseButton();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickRefreshPage();

            //Get the Booking Diary Id
            var _cpbookingdiaryid = dbHelper.cPBookingDiary.GetCPBookingIdByCreator(_systemUserId).First();


            mainMenu
                .WaitForMainMenuToLoad(true, true, true, false, false, false)
                .NavigateToPeopleDiarySection();


            String Date = DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy");
            String NextDate = DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy");

            peopleDiaryPage
               .WaitForPeopleDiaryPageToLoad()
               .selectProvider(provider + " - No Address")
               .WaitForPeopleDiaryPageToLoad()
               .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(Convert.ToDateTime(Date));

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .MouseHoverDiaryBooking(_cpbookingdiaryid.ToString())
                .ValidateTimeLabelText("Planned Time: " + Date + " 00:00 - " + NextDate + " 00:00")
                .ValidateBookingTypeLabelText("Booking Type: BookingType-6");
        }

        //Step 9 Planned Start Date-Yes ,Actual Start Date-Yes,Planned End Date-Yes,Actual End Date-Yes
        [TestProperty("JiraIssueID", "ACC-3569")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-24106 - " +
              "As a care coordinator verify system behaviour to create a booking for person absence based on person absence type.Pre-Condition-> Person record should exist-> Should have active person contract to create a person absence-> Default Booking Type for Person Absence should be selected in Scheduling Setup" +
              "navigate to People Diary and verify created absence booking by selecting required provider.Verify the created booking on Person Diary for created person absence.Verify populated fields on Booking Diary from Person absence")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("BusinessModule2", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Person Absences")]
        [TestProperty("Screen2", "People Diary")]
        public void PersonAbsence_VerifyAbsenceBooking_08()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);
            var provider = (string)(dbHelper.provider.GetProviderByID(_providereId1, "name"))["name"];



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
                .NavigateToHealthPersonAbsencesPage();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickNewRecordButton();

            //Verify system behavior while creating a person absence record as below which is of Open-ended Absence type  
            //Planned Start=Yes, Planned End=Yes, Actual Start=Yes, Actual End=Yes

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .ClickHealthAbsenceReasonLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectLookIn("Lookup Records")
               .TypeSearchQuery(_cpPersonabsencereasonName)
               .TapSearchButton()
               .SelectResultElement(_cpPersonabsencereasonid.ToString());

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .InsertHealthAbsencePlannedStartDateTime(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceActualStartDate(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndDateTime(DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceActualEndDate(DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSaveNClose();


            dynamicDialogPopup
                 .WaitForCareCloudDynamicDialoguePopUpToLoad()
                 .ValidateMessage("No bookings exist during the absence period")
                 .TapCloseButton();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickRefreshPage();

            //Get the Booking Diary Id
            var _cpbookingdiaryid = dbHelper.cPBookingDiary.GetCPBookingIdByCreator(_systemUserId).First();


            mainMenu
                .WaitForMainMenuToLoad(true, true, true, false, false, false)
                .NavigateToPeopleDiarySection();


            String Date = DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy");
            String NextDate = DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy");

            peopleDiaryPage
               .WaitForPeopleDiaryPageToLoad()
               .selectProvider(provider + " - No Address")
               .WaitForPeopleDiaryPageToLoad()
               .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(Convert.ToDateTime(Date));

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .MouseHoverDiaryBooking(_cpbookingdiaryid.ToString())
                .ValidateTimeLabelText("Planned Time: " + Date + " 00:00 - " + NextDate + " 00:00")
                .ValidateBookingTypeLabelText("Booking Type: BookingType-6");
        }

        //Step 10 Planned Start Date-Yes ,Actual Start Date-Yes,Planned End Date-No,Actual End Date-Yes
        [TestProperty("JiraIssueID", "ACC-3570")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-24106 - " +
              "As a care coordinator verify system behaviour to create a booking for person absence based on person absence type.Pre-Condition-> Person record should exist-> Should have active person contract to create a person absence-> Default Booking Type for Person Absence should be selected in Scheduling Setup" +
              "navigate to People Diary and verify created absence booking by selecting required provider.Verify the created booking on Person Diary for created person absence.Verify populated fields on Booking Diary from Person absence")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("BusinessModule2", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Person Absences")]
        [TestProperty("Screen2", "People Diary")]
        public void PersonAbsence_VerifyAbsenceBooking_09()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);
            var provider = (string)(dbHelper.provider.GetProviderByID(_providereId1, "name"))["name"];



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
                .NavigateToHealthPersonAbsencesPage();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickNewRecordButton();

            //Verify system behavior while creating a person absence record as below which is of Open-ended Absence type  
            //Planned Start=Yes, Planned End=No, Actual Start=Yes, Actual End=Yes

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .ClickHealthAbsenceReasonLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectLookIn("Lookup Records")
               .TypeSearchQuery(_cpPersonabsencereasonName)
               .TapSearchButton()
               .SelectResultElement(_cpPersonabsencereasonid.ToString());

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .InsertHealthAbsencePlannedStartDateTime(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceActualStartDate(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceActualEndDate(DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSaveNClose();


            dynamicDialogPopup
                 .WaitForCareCloudDynamicDialoguePopUpToLoad()
                 .ValidateMessage("No bookings exist during the absence period")
                 .TapCloseButton();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickRefreshPage();

            //Get the Booking Diary Id
            var _cpbookingdiaryid = dbHelper.cPBookingDiary.GetCPBookingIdByCreator(_systemUserId).First();


            mainMenu
                .WaitForMainMenuToLoad(true, true, true, false, false, false)
                .NavigateToPeopleDiarySection();


            String Date = DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy");
            String NextDate = DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy");

            peopleDiaryPage
               .WaitForPeopleDiaryPageToLoad()
               .selectProvider(provider + " - No Address")
               .WaitForPeopleDiaryPageToLoad()
               .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(Convert.ToDateTime(Date));

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .MouseHoverDiaryBooking(_cpbookingdiaryid.ToString())
                .ValidateTimeLabelText("Planned Time: " + Date + " 00:00 - " + NextDate + " 00:00")
                .ValidateBookingTypeLabelText("Booking Type: BookingType-6");


        }

        //Step 11 Planned Start Date-No ,Actual Start Date-Yes,Planned End Date-No,Actual End Date-Yes
        [TestProperty("JiraIssueID", "ACC-3571")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-24106 - " +
            "As a care coordinator verify system behaviour to create a booking for person absence based on person absence type.Pre-Condition-> Person record should exist-> Should have active person contract to create a person absence-> Default Booking Type for Person Absence should be selected in Scheduling Setup" +
            "navigate to People Diary and verify created absence booking by selecting required provider.Verify the created booking on Person Diary for created person absence.Verify populated fields on Booking Diary from Person absence")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("BusinessModule2", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Person Absences")]
        [TestProperty("Screen2", "People Diary")]
        public void PersonAbsence_VerifyAbsenceBooking_10()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);
            var provider = (string)(dbHelper.provider.GetProviderByID(_providereId1, "name"))["name"];

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
                .NavigateToHealthPersonAbsencesPage();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickNewRecordButton();

            //Verify system behavior while creating a person absence record as below which is of Open-ended Absence type  
            //Planned Start=No, Planned End=Yes, Actual Start=Yes, Actual End=Yes

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .ClickHealthAbsenceReasonLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectLookIn("Lookup Records")
               .TypeSearchQuery(_cpPersonabsencereasonName)
               .TapSearchButton()
               .SelectResultElement(_cpPersonabsencereasonid.ToString());

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .InsertHealthAbsenceActualStartDate(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceActualEndDate(DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndDateTime(DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSaveNClose();


            dynamicDialogPopup
                 .WaitForCareCloudDynamicDialoguePopUpToLoad()
                 .ValidateMessage("No bookings exist during the absence period")
                 .TapCloseButton();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickRefreshPage();

            //Get the Booking Diary Id
            var _cpbookingdiaryid = dbHelper.cPBookingDiary.GetCPBookingIdByCreator(_systemUserId).First();


            mainMenu
                .WaitForMainMenuToLoad(true, true, true, false, false, false)
                .NavigateToPeopleDiarySection();


            String Date = DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy");
            String NextDate = DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy");

            peopleDiaryPage
               .WaitForPeopleDiaryPageToLoad()
               .selectProvider(provider + " - No Address")
               .WaitForPeopleDiaryPageToLoad()
               .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(Convert.ToDateTime(Date));


            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .MouseHoverDiaryBooking(_cpbookingdiaryid.ToString())
               .ValidateTimeLabelText("Planned Time: " + Date + " 00:00 - " + NextDate + " 00:00")
               .ValidateBookingTypeLabelText("Booking Type: BookingType-6");

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4542

        [TestProperty("JiraIssueID", "ACC-311")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-311 - " +
            "As a Care Coordinator verify update to person absence booking when there is a change in person absence record.Pre-Condition-> Person record should exist-> Should have active person contract to create a person absence-> Default Booking Type for Person Absence should be selected in Scheduling Setup")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("BusinessModule2", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Person Absences")]
        [TestProperty("Screen2", "People Diary")]
        public void PersonAbsence_VerifyAbsenceBooking_011()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);
            var provider = (string)(dbHelper.provider.GetProviderByID(_providereId1, "name"))["name"];



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
                .NavigateToHealthPersonAbsencesPage();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickNewRecordButton();

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .ClickHealthAbsenceReasonLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectLookIn("Lookup Records")
               .TypeSearchQuery(_cpPersonabsencereasonName)
               .TapSearchButton()
               .SelectResultElement(_cpPersonabsencereasonid.ToString());

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .InsertHealthAbsencePlannedStartDateTime(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndDateTime(DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSaveNClose();

            dynamicDialogPopup
                .WaitForCareCloudDynamicDialoguePopUpToLoad()
                .ValidateMessage("No bookings exist during the absence period")
                .TapCloseButton();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickRefreshPage();


            //Get the PersonAbsenceID
            var _cppersonabsencid = dbHelper.cpPersonAbsence.GetByPersonId(_personID).FirstOrDefault();

            //Get the Booking Diary Id
            var _cpbookingdiaryid = dbHelper.cPBookingDiary.GetCPBookingIdByCreator(_systemUserId).First();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleDiarySection();

            System.Threading.Thread.Sleep(1000);

            String Date = DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy");
            String NextDate = DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy");

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
               .selectProvider(provider + " - No Address")
                .WaitForPeopleDiaryPageToLoad()
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(Convert.ToDateTime(Date));

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .MouseHoverDiaryBooking(_cpbookingdiaryid.ToString())
                .ValidateTimeLabelText("Planned Time: " + Date + " 00:00 - " + NextDate + " 00:00")
                .ValidateBookingTypeLabelText("Booking Type: BookingType-6");

            mainMenu
                .WaitForMainMenuToLoad(true, true, true, false, false, false)
                .NavigateToPeopleSection();

            peoplePage
               .WaitForPeoplePageToLoad()
               .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
               .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .NavigateToHealthPersonAbsencesPage();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .OpenPersonAbsenceRecord(_cppersonabsencid.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("cppersonabsence").ClickOnExpandIcon();

            personHealthAbsencesRecordPage
               .WaitForPersonHealthAbsencesEditRecordPageToLoad()
               .InsertHealthAbsencePlannedStartDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .ClickHealthAbsenceSaveNClose();

            dynamicDialogPopup
                .WaitForCareCloudDynamicDialoguePopUpToLoad()
                .ValidateMessage("No bookings exist during the absence period")
                .TapCloseButton();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickRefreshPage();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleDiarySection();


            String UpdatedDate = DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy");

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .selectProvider(provider + " - No Address")
                .WaitForPeopleDiaryPageToLoad()
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(Convert.ToDateTime(UpdatedDate));

           
            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .MouseHoverDiaryBooking(_cpbookingdiaryid.ToString())
                .ValidateTimeLabelText("Planned Time: " + UpdatedDate + " 00:00 - 23:45")
                .ValidateBookingTypeLabelText("Booking Type: BookingType-6");
        }

        [TestProperty("JiraIssueID", "ACC-314")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-314 - " +
           "Verify as a system administrator able to delete a person absence record.Pre-Condition-> Person record should exist-> Should have active person contract to create a person absence-> Default Booking Type for Person Absence should be selected in Scheduling Setup")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("BusinessModule2", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Person Absences")]
        [TestProperty("Screen2", "People Diary")]
        public void PersonAbsence_VerifyAbsenceBooking_012()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);
            var provider = (string)(dbHelper.provider.GetProviderByID(_providereId1, "name"))["name"];

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
                .NavigateToHealthPersonAbsencesPage();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickNewRecordButton();

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .ClickHealthAbsenceReasonLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectLookIn("Lookup Records")
               .TypeSearchQuery(_cpPersonabsencereasonName)
               .TapSearchButton()
               .SelectResultElement(_cpPersonabsencereasonid.ToString());

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .InsertHealthAbsencePlannedStartDateTime(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndDateTime(DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSaveNClose();

            dynamicDialogPopup
                .WaitForCareCloudDynamicDialoguePopUpToLoad()
                .ValidateMessage("No bookings exist during the absence period")
                .TapCloseButton();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickRefreshPage();


            //Get the PersonAbsenceID
            var _cppersonabsencid = dbHelper.cpPersonAbsence.GetByPersonId(_personID).FirstOrDefault();

            //Get the Booking Diary Id
            var _cpbookingdiaryid = dbHelper.cPBookingDiary.GetCPBookingIdByCreator(_adminSystemUserId).First();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleDiarySection();


            String Date = DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy");
            String NextDate = DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy");

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
               .selectProvider(provider + " - No Address")
                .WaitForPeopleDiaryPageToLoad()
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(Convert.ToDateTime(Date));


            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .MouseHoverDiaryBooking(_cpbookingdiaryid.ToString())
                .ValidateTimeLabelText("Planned Time: " + Date + " 00:00 - " + NextDate + " 00:00")
                .ValidateBookingTypeLabelText("Booking Type: BookingType-6");

            mainMenu
                .WaitForMainMenuToLoad(true, true, true, false, false, false)
                .NavigateToPeopleSection();

            peoplePage
               .WaitForPeoplePageToLoad()
               .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
               .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .NavigateToHealthPersonAbsencesPage();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .OpenPersonAbsenceRecord(_cppersonabsencid.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("cppersonabsence").ClickOnExpandIcon();

            personHealthAbsencesRecordPage
               .WaitForPersonHealthAbsencesEditRecordPageToLoad()
               .ClickDeleteRecordBtn();

            alertPopup.WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleDiarySection();


            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
               .selectProvider(provider + " - No Address")
                .WaitForPeopleDiaryPageToLoad()
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(Convert.ToDateTime(Date));

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .ValidateDiaryBooking(_personcontractId.ToString());
        }

        [TestProperty("JiraIssueID", "ACC-321")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-321 - " +
           "As a Care Coordinator Verify system behavior by changing booking or changing person absence record after a booking is created with person absence .Pre-Condition-> Person record should exist-> Should have active person contract to create a person absence-> Default Booking Type for Person Absence should be selected in Scheduling Setup")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("BusinessModule2", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Person Absences")]
        [TestProperty("Screen2", "People Diary")]
        public void PersonAbsence_VerifyAbsenceBooking_013()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);
            var provider = (string)(dbHelper.provider.GetProviderByID(_providereId1, "name"))["name"];


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
                .NavigateToHealthPersonAbsencesPage();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickNewRecordButton();

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .ClickHealthAbsenceReasonLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectLookIn("Lookup Records")
               .TypeSearchQuery(_cpPersonabsencereasonName)
               .TapSearchButton()
               .SelectResultElement(_cpPersonabsencereasonid.ToString());

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .InsertHealthAbsencePlannedStartDateTime(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndDateTime(DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSaveNClose();

            dynamicDialogPopup
                .WaitForCareCloudDynamicDialoguePopUpToLoad()
                .ValidateMessage("No bookings exist during the absence period")
                .TapCloseButton();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickRefreshPage();


            //Get the PersonAbsenceID
            var _cppersonabsencid = dbHelper.cpPersonAbsence.GetByPersonId(_personID).FirstOrDefault();

            //Get the Booking Diary Id
            var _cpbookingdiaryid = dbHelper.cPBookingDiary.GetCPBookingIdByCreator(_systemUserId).First();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleDiarySection();


            String Date = DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy");

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                 .selectProvider(provider + " - No Address")
                .WaitForPeopleDiaryPageToLoad()
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(Convert.ToDateTime(Date));

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .clickDiaryBooking(_cpbookingdiaryid.ToString());

            amendDiaryBookingPopUp
                .WaitForAmendDiaryBookingPopUpPopupPageToLoad()
                .ValidateDialogText("This booking was created from a Person Absence and cannot be amended." +
                "  Absence Reason: Person Absence Reason Hospital " +
                " The booking(s) can be deleted if the Person Absence is made ‘inactive’. You can view the Person Absence by clicking here:"
                )
                .ClickOnCloseButton();
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4809

        [TestProperty("JiraIssueID", "ACC-399")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-399 - " +
            "To verify the new section Open-ended Absence Planned or Actual End Date is not null in the person absence form." +
           "Login to AWS QA as Care Coordinator-> workplace-> my work-> people-> select one existing person record-> menu-> daily care-> absence-> click on '+' icon and fill all mandatory fields along with planned or actual end date or open the existing record with Planned or Actual End Date and verify the new section open ended absence." +
           "The new section Open-ended Absence will hide from the person absence form." +
           "Pre-Requisite:At least one person record should be present.)"
           + "At least one person open-ended absence record should be present.")]

        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("Screen1", "Person Absences")]
        public void OpenEndedAbsence_VerifyAbsenceBooking_014()
        {

            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);



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
                .NavigateToHealthPersonAbsencesPage();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickNewRecordButton();

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .ClickHealthAbsenceReasonLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectLookIn("Lookup Records")
               .TypeSearchQuery(_cpPersonabsencereasonName)
               .TapSearchButton()
               .SelectResultElement(_cpPersonabsencereasonid.ToString());

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .InsertHealthAbsencePlannedStartDateTime(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndDateTime(DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSave();

            dynamicDialogPopup
                .WaitForCareCloudDynamicDialoguePopUpToLoad()
                .ValidateMessage("No bookings exist during the absence period")
                .TapCloseButton();

            personHealthAbsencesRecordPage
              .WaitForPageToLoadInDrawerMode()
              .VerifyOpenEndedAbsenceNotVisible();


        }

        [TestProperty("JiraIssueID", "ACC-397")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-397 - " +
           "To verify the new section \"Open-ended Absence\" Planned or Actual End Date is null in the person absence form." +
          "Login to AWS QA as Care Coordinator->  workplace-> my work-> people-> select one existing person record-> menu-> daily care-> absence-> click on \"+\" icon or open the existing record which is not having Planned or Actual End Date and verify the new section open ended absence" +
          "1.The new section open ended absence should be present under notifications.It should be with label De-allocate staff from scheduled bookings.It should be present with boolean fields yes or no.4.By default it should set it to false." +
          "Pre-Requisite:At least one person record should be present.)")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("Screen1", "Person Absences")]
        public void OpenEndedAbsence_VerifyAbsenceBooking_015()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);



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
                .NavigateToHealthPersonAbsencesPage();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickNewRecordButton();

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .ClickHealthAbsenceReasonLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectLookIn("Lookup Records")
               .TypeSearchQuery(_cpPersonabsencereasonName)
               .TapSearchButton()
               .SelectResultElement(_cpPersonabsencereasonid.ToString());

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .InsertHealthAbsencePlannedStartDateTime(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSave();


            personHealthAbsencesRecordPage
              .WaitForPageToLoadInDrawerMode()
              .VerifyOpenEndedAbsenceVisible()
              .ValidateDeAllocateStaff_NoRadioButtonChecked();
        }

        [TestProperty("JiraIssueID", "ACC-631")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-397 - " +
           "To verify the CP Schedule Bookings record when de-allocate flag is set to true in person absence is created." +
          "Step:1 Login to AWS QA as care coordinator-> workplace-> my work-> people-> select one existing record-> menu-> absence-> create record as per the pre condition for person absence" +
          "Step:2 Go to schedule bookings-> click on add booking-> fill all the mandatory fields and  select multiple staff" +
          "Step:3 Now switch back to person absence record-> open the record and set the de-allocate flag to yes -> click on save.)" +
          "Step:4 Then go to schedule booking-> and verify the staff allocated previously were all unassigned")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("BusinessModule2", "Care Provider Person Absence")]
        [TestProperty("Screen1", "Person Absences")]
        [TestProperty("Screen2", "Provider Schedule")]
        public void OpenEndedAbsence_VerifyAbsenceBooking_016()
        {
            var todayDate = DateTime.Now.Date;
            var _applicantId = dbHelper.applicant.CreateApplicant("test", "lastname", _careDirectorQA_TeamId);
            var _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _systemUserId, _careDirectorQA_TeamId, DateTime.Now, _careDirectorQA_TeamId, 1);

            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            var _bookingType = commonMethodsDB.CreateCPBookingType("BookingType-5", 5, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 2, true, 1440);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_careDirectorQA_TeamId, _providereId1, _bookingType, true);

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);
            //dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType);

            var _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, todayDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), _roleApplication, _applicantId, 1);
            var _UserScheduleIdTypeId1 = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekMondayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, todayDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), _roleApplication, _applicantId, 1);
            var _UserScheduleIdTypeId2 = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekTuesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, todayDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), _roleApplication, _applicantId, 1);
            var _UserScheduleIdTypeId3 = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekThursdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, todayDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), _roleApplication, _applicantId, 1);
            var _UserScheduleIdTypeId4 = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekFridayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, todayDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), _roleApplication, _applicantId, 1);
            var _UserScheduleIdTypeId5 = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekSaturdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, todayDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), _roleApplication, _applicantId, 1);
            var _UserScheduleIdTypeId6 = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekSundayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, todayDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), _roleApplication, _applicantId, 1);

            #region Person Absence

            var personContractIds = new System.Collections.Generic.List<Guid> { _personcontractId };
            var cpPersonAbsenceId = dbHelper.cpPersonAbsence.CreateCPPersonAbsence(_careDirectorQA_TeamId, _personID, new DateTime(2023, 6, 10), personContractIds, _cpPersonabsencereasonid, new DateTime(2023, 6, 10));

            var provider = (string)(dbHelper.provider.GetProviderByID(_providereId1, "name"))["name"];


            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad()
               .selectProvider(provider + " - No Address")
               .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BookingType-5")
                .SetStartTime("12", "00")
                .SetEndTime("13", "00")
                .ClickSelectPeople();

            selectMultiplePeoplePopUp
                .WaitForSelectMultiplePeoplePopUpPageToLoad()
                .SearchRecords(_personFirstName)
                .ClickPeopleRecordCellText(_personcontractId.ToString())
                .ClickConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .ClickOnlyShowMatchedStaff()
                .ClickStaffRecordCellText(_systemUserEmploymentContractId.ToString())
                .ClickStaffConfirmSelection();


            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(2000);

            var cpbookingschedulestaffid = dbHelper.cpBookingScheduleStaff.GetBySystemUserEmploymentContractId(_systemUserEmploymentContractId).FirstOrDefault();
            var cpbookingscheduleId = (Guid)dbHelper.cpBookingScheduleStaff.GetById(cpbookingschedulestaffid, "cpbookingscheduleid")["cpbookingscheduleid"];
            //var cpbookingscheduleId = dbHelper.cpBookingScheduleStaff.GetById(cpbookingschedulestaffid, "cpbookingscheduleid").FirstOrDefault();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .NavigateToHealthPersonAbsencesPage();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                 .OpenPersonAbsenceRecord(cpPersonAbsenceId.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("cppersonabsence").ClickOnExpandIcon();

            personHealthAbsencesRecordPage
               .WaitForPersonHealthAbsencesEditRecordPageToLoad()
               .ClickDeAllocateStaff_YesRadioButton()
               .ClickHealthAbsenceSaveNClose();

            System.Threading.Thread.Sleep(2000);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad()
               .selectProvider(provider + " - No Address")
               .MouseHoverDiaryBooking(cpbookingscheduleId.ToString())
               .ValidateStaffLabelText("Unassigned");
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5048

        [TestProperty("JiraIssueID", "ACC-765")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-765 - " +
           "As a care coordinator verify absence duration calculation for Duration (Days) and Duration (hours) fields of Person Absence" +
           "Pre-Requisite:Person record should exist")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("Screen1", "Person Absences")]
        public void OpenEndedAbsence_VerifyAbsenceBooking_018()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);



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
                .NavigateToHealthPersonAbsencesPage();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickNewRecordButton();

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .ClickHealthAbsenceReasonLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectLookIn("Lookup Records")
               .TypeSearchQuery(_cpPersonabsencereasonName)
               .TapSearchButton()
               .SelectResultElement(_cpPersonabsencereasonid.ToString());

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .InsertHealthAbsencePlannedStartDateTime(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndDateTime(DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSave();

            dynamicDialogPopup
                .WaitForCareCloudDynamicDialoguePopUpToLoad()
                .ValidateMessage("No bookings exist during the absence period")
                .TapCloseButton();

            System.Threading.Thread.Sleep(2000);

            DateTime startTime = DateTime.Today.Date.AddDays(-2);
            DateTime endTime = DateTime.Today.Date.AddDays(-1);
            double span = startTime.Subtract(endTime).TotalMinutes;
            Console.WriteLine(Math.Abs(span));
            double durationInDays = Math.Abs(span) / 1440;
            double durationInhours = Math.Abs(span) / 60;

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .ValidateDurationDaysText(durationInDays.ToString() + ".00")
               .ValidateDurationHoursText(durationInhours.ToString() + ".00");
        }

        [TestProperty("JiraIssueID", "ACC-766")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-766 - " +
         "Verify system behavior to calculate absence duration based on Planned /Actual Start /End date & time " +
         "Pre-Requisite:Person record should exist")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("Screen1", "Person Absences")]
        public void OpenEndedAbsence_VerifyAbsenceBooking_019()
        {

            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);


            #region step 1

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
            #endregion

            #region step 2

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .NavigateToHealthPersonAbsencesPage();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickNewRecordButton();

            #endregion

            #region step 3

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .ClickHealthAbsenceReasonLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectLookIn("Lookup Records")
               .TypeSearchQuery(_cpPersonabsencereasonName)
               .TapSearchButton()
               .SelectResultElement(_cpPersonabsencereasonid.ToString());

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .InsertHealthAbsencePlannedStartDateTime(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndDateTime(DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSave();

            dynamicDialogPopup
                .WaitForCareCloudDynamicDialoguePopUpToLoad()
                .ValidateMessage("No bookings exist during the absence period")
                .TapCloseButton();

            #endregion 

            DateTime startTime = DateTime.Today.Date.AddDays(-2);
            DateTime endTime = DateTime.Today.Date.AddDays(-1);
            double span = startTime.Subtract(endTime).TotalMinutes;
            Console.WriteLine(Math.Abs(span));
            double durationInDays = Math.Abs(span) / 1440;
            double durationInhours = Math.Abs(span) / 60;

            System.Threading.Thread.Sleep(2000);

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .ValidateDurationDaysText(durationInDays.ToString() + ".00")
               .ValidateDurationHoursText(durationInhours.ToString() + ".00")

            #region step 4 Planned Start - No, Planned End - Yes, Actual Start - Yes, Actual End - No

               .ClearHealthAbsencePlannedStartDateTime()
               .InsertHealthAbsencePlannedEndDateTime(DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceActualStartDate(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
               .ClickHealthAbsenceSave();

            dynamicDialogPopup
               .WaitForCareCloudDynamicDialoguePopUpToLoad()
               .ValidateMessage("No bookings exist during the absence period")
               .TapCloseButton();

            System.Threading.Thread.Sleep(2000);

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .ValidateDurationDaysText(durationInDays.ToString() + ".00")
               .ValidateDurationHoursText(durationInhours.ToString() + ".00")

            #endregion

            #region step 5 Planned Start - No, Planned End - No, Actual Start - Yes, Actual End -Yes

               .ClearHealthAbsencePlannedEndDateTime()
               .InsertHealthAbsenceActualStartDate(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceActualEndDate(DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .ClickHealthAbsenceSave();

            dynamicDialogPopup
               .WaitForCareCloudDynamicDialoguePopUpToLoad()
               .ValidateMessage("No bookings exist during the absence period")
               .TapCloseButton();

            System.Threading.Thread.Sleep(2000);


            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .ValidateDurationDaysText(durationInDays.ToString() + ".00")
               .ValidateDurationHoursText(durationInhours.ToString() + ".00");

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-5102")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-5102 - " +
            "Step 6,7 of the actual test ACC-766 " +
            "Pre-Requisite:Person record should exist")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("Screen1", "Person Absences")]
        public void OpenEndedAbsence_VerifyAbsenceBooking_020()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);


            #region step 1

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
            #endregion

            #region step 2

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .NavigateToHealthPersonAbsencesPage();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickNewRecordButton();
            #endregion

            #region step 3

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .ClickHealthAbsenceReasonLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectLookIn("Lookup Records")
               .TypeSearchQuery(_cpPersonabsencereasonName)
               .TapSearchButton()
               .SelectResultElement(_cpPersonabsencereasonid.ToString());

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .InsertHealthAbsencePlannedStartDateTime(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndDateTime(DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSave();

            dynamicDialogPopup
                .WaitForCareCloudDynamicDialoguePopUpToLoad()
                .ValidateMessage("No bookings exist during the absence period")
                .TapCloseButton();

            #endregion 

            DateTime startTime = DateTime.Today.Date.AddDays(-2);
            DateTime endTime = DateTime.Today.Date.AddDays(-1);
            double span = startTime.Subtract(endTime).TotalMinutes;
            Console.WriteLine(Math.Abs(span));
            double durationInDays = Math.Abs(span) / 1440;
            double durationInhours = Math.Abs(span) / 60;

            System.Threading.Thread.Sleep(2000);

            #region step 6 Planned Start - Yes, Planned End - Yes, Actual Start - Yes, Actual End -No

            personHealthAbsencesRecordPage
              .WaitForPageToLoadInDrawerMode()
              .InsertHealthAbsencePlannedStartDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
              .InsertHealthAbsenceActualStartDate(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
              .InsertHealthAbsencePlannedEndDateTime(DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
              .ClickHealthAbsenceSave();

            dynamicDialogPopup
               .WaitForCareCloudDynamicDialoguePopUpToLoad()
               .ValidateMessage("No bookings exist during the absence period")
               .TapCloseButton();

            System.Threading.Thread.Sleep(2000);
            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .ValidateDurationDaysText(durationInDays.ToString() + ".00")
               .ValidateDurationHoursText(durationInhours.ToString() + ".00")

            #endregion

            #region step 7 Planned Start - Yes, Planned End - No, Actual Start - Yes, Actual End - Yes

               .InsertHealthAbsencePlannedStartDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceActualStartDate(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
               .ClearHealthAbsencePlannedEndDateTime()
               .InsertHealthAbsenceActualEndDate(DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .ClickHealthAbsenceSave();

            dynamicDialogPopup
               .WaitForCareCloudDynamicDialoguePopUpToLoad()
               .ValidateMessage("No bookings exist during the absence period")
               .TapCloseButton();

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .ValidateDurationDaysText(durationInDays.ToString() + ".00")
               .ValidateDurationHoursText(durationInhours.ToString() + ".00");

            #endregion
        }


        [TestProperty("JiraIssueID", "ACC-5103")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-5103 - " +
            "Step 8 of the actual test ACC-766 " +
            "Pre-Requisite:Person record should exist")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("Screen1", "Person Absences")]
        public void OpenEndedAbsence_VerifyAbsenceBooking_021()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);


            #region step 1

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
            #endregion

            #region step 2

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .NavigateToHealthPersonAbsencesPage();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickNewRecordButton();

            #endregion

            DateTime startTime = DateTime.Today.Date.AddDays(-2);
            DateTime endTime = DateTime.Today.Date.AddDays(-1);
            double span = startTime.Subtract(endTime).TotalMinutes;
            Console.WriteLine(Math.Abs(span));
            double durationInDays = Math.Abs(span) / 1440;
            double durationInhours = Math.Abs(span) / 60;

            System.Threading.Thread.Sleep(2000);

            #region step 8 Planned Start - No, Planned End - Yes, Actual Start - Yes, Actual End - Yes

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .ClickHealthAbsenceReasonLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectLookIn("Lookup Records")
               .TypeSearchQuery(_cpPersonabsencereasonName)
               .TapSearchButton()
               .SelectResultElement(_cpPersonabsencereasonid.ToString());

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .ClearHealthAbsencePlannedStartDateTime()
               .InsertHealthAbsencePlannedEndDateTime(DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceActualStartDate(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceActualEndDate(DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSave();

            dynamicDialogPopup
               .WaitForCareCloudDynamicDialoguePopUpToLoad()
               .ValidateMessage("No bookings exist during the absence period")
               .TapCloseButton();

            System.Threading.Thread.Sleep(2000);

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .ValidateDurationDaysText(durationInDays.ToString() + ".00")
               .ValidateDurationHoursText(durationInhours.ToString() + ".00");

            #endregion



        }

        [TestProperty("JiraIssueID", "ACC-5104")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-5103 - " +
            "Step 9 of the actual test ACC-766 " +
            "Pre-Requisite:Person record should exist")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("Screen1", "Person Absences")]
        public void OpenEndedAbsence_VerifyAbsenceBooking_022()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);


            #region step 1

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
            #endregion

            #region step 2

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .NavigateToHealthPersonAbsencesPage();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickNewRecordButton();
            #endregion

            DateTime startTime = DateTime.Today.Date.AddDays(-2);
            DateTime endTime = DateTime.Today.Date.AddDays(-1);
            double span = startTime.Subtract(endTime).TotalMinutes;
            Console.WriteLine(Math.Abs(span));
            double durationInDays = Math.Abs(span) / 1440;
            double durationInhours = Math.Abs(span) / 60;

            System.Threading.Thread.Sleep(2000);

            #region step 9 Planned Start - Yes, Planned End - Yes, Actual Start - Yes, Actual End - Yes

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .ClickHealthAbsenceReasonLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectLookIn("Lookup Records")
               .TypeSearchQuery(_cpPersonabsencereasonName)
               .TapSearchButton()
               .SelectResultElement(_cpPersonabsencereasonid.ToString());

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .InsertHealthAbsencePlannedStartDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndDateTime(DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceActualStartDate(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceActualEndDate(DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSave();

            dynamicDialogPopup
               .WaitForCareCloudDynamicDialoguePopUpToLoad()
               .ValidateMessage("No bookings exist during the absence period")
               .TapCloseButton();

            System.Threading.Thread.Sleep(2000);

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .ValidateDurationDaysText(durationInDays.ToString() + ".00")
               .ValidateDurationHoursText(durationInhours.ToString() + ".00");

            #endregion



        }

        [TestProperty("JiraIssueID", "ACC-767")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-767 - " +
         "Verify system behaviour with respective to duration fields when absence type is open ended absence" +
         "Pre-Requisite:Person Absence record should exist")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("Screen1", "Person Absences")]
        public void OpenEndedAbsence_VerifyAbsenceBooking_023()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

            #region step 1

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

            #endregion

            #region step 2

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .NavigateToHealthPersonAbsencesPage();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickNewRecordButton();

            #endregion

            DateTime startTime = DateTime.Today.Date.AddDays(-2);
            DateTime endTime = DateTime.Today.Date.AddDays(-1);
            double span = startTime.Subtract(endTime).TotalMinutes;
            Console.WriteLine(Math.Abs(span));
            double durationInDays = Math.Abs(span) / 1440;
            double durationInhours = Math.Abs(span) / 60;

            System.Threading.Thread.Sleep(2000);

            #region step 3 Planned Start - Yes, Planned End - No, Actual Start - Yes, Actual End - No

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .ClickHealthAbsenceReasonLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectLookIn("Lookup Records")
               .TypeSearchQuery(_cpPersonabsencereasonName)
               .TapSearchButton()
               .SelectResultElement(_cpPersonabsencereasonid.ToString());

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .InsertHealthAbsencePlannedStartDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceActualStartDate(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSave();

            System.Threading.Thread.Sleep(2000);

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .ValidateDurationDaysText("")
               .ValidateDurationHoursText("");

            #endregion



        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5529

        [TestProperty("JiraIssueID", "ACC-343")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-343 - " +
           "Verify system behavior by setting inactive flag as true in Person Absence form" +
           "Pre-Requisite:Person record should exist.Should have active person contract to create a person absence")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("BusinessModule2", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Person Absences")]
        [TestProperty("Screen2", "People Diary")]
        public void OpenEndedAbsence_VerifyAbsenceBooking_024()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);
            var provider = (string)(dbHelper.provider.GetProviderByID(_providereId1, "name"))["name"];

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
                .NavigateToHealthPersonAbsencesPage();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickNewRecordButton();

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .ClickHealthAbsenceReasonLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectLookIn("Lookup Records")
               .TypeSearchQuery(_cpPersonabsencereasonName)
               .TapSearchButton()
               .SelectResultElement(_cpPersonabsencereasonid.ToString());

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .InsertHealthAbsencePlannedStartDateTime(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndDateTime(DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSaveNClose();

            dynamicDialogPopup
                .WaitForCareCloudDynamicDialoguePopUpToLoad()
                .ValidateMessage("No bookings exist during the absence period")
                .TapCloseButton();

            System.Threading.Thread.Sleep(3000);

            personHealthAbsencesPage
               .WaitForPersonHealthAbsencesPageToLoad()
               .ClickRefreshPage();


            //Get the PersonAbsenceID
            var _cppersonabsencid = dbHelper.cpPersonAbsence.GetByPersonId(_personID).FirstOrDefault();

            //Get the Booking Diary Id
            var _cpbookingdiaryid = dbHelper.cPBookingDiary.GetCPBookingIdByCreator(_systemUserId).First();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleDiarySection();


            String Date = DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy");
            String NextDate = DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy");

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                 .selectProvider(provider + " - No Address")
                .WaitForPeopleDiaryPageToLoad()
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(Convert.ToDateTime(Date));

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .MouseHoverDiaryBooking(_cpbookingdiaryid.ToString())
                .ValidateTimeLabelText("Planned Time: " + Date + " 00:00 - " + NextDate + " 00:00")
                .ValidateBookingTypeLabelText("Booking Type: BookingType-6");

            mainMenu
               .WaitForMainMenuToLoad(true, true, true, false, false, false)
               .NavigateToPeopleSection();

            peoplePage
               .WaitForPeoplePageToLoad()
               .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
               .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .NavigateToHealthPersonAbsencesPage();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .OpenPersonAbsenceRecord(_cppersonabsencid.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("cppersonabsence").ClickOnExpandIcon();

            personHealthAbsencesRecordPage
               .WaitForPersonHealthAbsencesEditRecordPageToLoad()
               .ClickHealthAbsenceInactiveYesRadio()
               .ClickHealthAbsenceSave();

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToPeopleDiarySection();


            peopleDiaryPage
                 .WaitForPeopleDiaryPageToLoad()
                  .selectProvider(provider + " - No Address")
                 .WaitForPeopleDiaryPageToLoad()
                 .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(Convert.ToDateTime(Date));

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .ValidateDiaryBooking(_personcontractId.ToString());

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-8867

        [TestProperty("JiraIssueID", "ACC-9190")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-9190 - " +
           "create a person absence via absence record ending at midnight, observe in diary wallcharts, diary is automatically created for this duration\r\nEx: Absence created for 09/07/2024 22hrs to 10/07/2024 00hrs" +
           "Pre-Requisite:Person record should exist.Should have active person contract to create a person absence")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("BusinessModule2", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Person Absences")]
        [TestProperty("Screen2", "People Diary")]
        public void OpenEndedAbsence_VerifyAbsenceBooking_025()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

            var provider = (string)(dbHelper.provider.GetProviderByID(_providereId1, "name"))["name"];
           
            #region Login and navigate to Person record

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
            #endregion

            #region create Person Absences

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .NavigateToHealthPersonAbsencesPage();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickNewRecordButton();

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .ClickHealthAbsenceReasonLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectLookIn("Lookup Records")
               .TypeSearchQuery(_cpPersonabsencereasonName)
               .TapSearchButton()
               .SelectResultElement(_cpPersonabsencereasonid.ToString());

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .InsertHealthAbsencePlannedStartDateTime(DateTime.Today.ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedStarTime("20:00")
               .InsertHealthAbsencePlannedEndDateTime(DateTime.Today.Date.AddDays(1).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndTime("00:00")
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSaveNClose();

            dynamicDialogPopup
                .WaitForCareCloudDynamicDialoguePopUpToLoad()
                .ValidateMessage("No bookings exist during the absence period")
                .TapCloseButton();

            System.Threading.Thread.Sleep(3000);

            personHealthAbsencesPage
               .WaitForPersonHealthAbsencesPageToLoad()
               .ClickRefreshPage();

            #endregion

            //Get the PersonAbsenceID
            var _cppersonabsencid = dbHelper.cpPersonAbsence.GetByPersonId(_personID).FirstOrDefault();

            //Get the Booking Diary Id
            var _cpbookingdiaryid = dbHelper.cPBookingDiary.GetCPBookingIdByCreator(_systemUserId).First();
            DateTime bookingStartDate = DateTime.Today;
            DateTime bookingEndDate = bookingStartDate.AddDays(1);
           
            #region validate the booking BTC-6 in Person Diary

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .TapDiaryTab();

            personDiaryRecordPage
                .WaitForPersonDiaryPageToLoad()
                .MouseHoverDiaryBooking(_cpbookingdiaryid.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateBookingTypeLabelText("Booking Type: BookingType-6");

            personDiaryRecordPage
                .WaitForPersonDiaryPageToLoad()
                .ClickTodayButton();

            System.Threading.Thread.Sleep(2000);

            personDiaryRecordPage
               .WaitForPersonDiaryPageToLoad()
               .MouseHoverDiaryBooking(_cpbookingdiaryid.ToString())
               .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
               .ValidateBookingTypeLabelText("Booking Type: BookingType-6");

            #endregion

            #region Validate Booking in People Diary Page

            mainMenu
               .WaitForMainMenuToLoad(true, true, true, false, false, false)
               .NavigateToPeopleDiarySection();

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .selectProvider(provider + " - No Address");

            System.Threading.Thread.Sleep(3000);

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .MouseHoverDiaryBooking(_cpbookingdiaryid.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateBookingTypeLabelText("Booking Type: BookingType-6");

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .ClickNextDateButton();

            System.Threading.Thread.Sleep(2000);

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .MouseHoverDiaryBooking(_cpbookingdiaryid.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateBookingTypeLabelText("Booking Type: BookingType-6");

            #endregion

        }

        #endregion

    }
}


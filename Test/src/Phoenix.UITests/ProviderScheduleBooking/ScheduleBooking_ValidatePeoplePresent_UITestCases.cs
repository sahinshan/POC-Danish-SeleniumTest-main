using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;


namespace Phoenix.UITests.ProviderScheduleBooking
{
    /// <summary>
    /// This class contains Automated UI test scripts for Provider Schedule Booking - Validate People Present for Booking
    /// </summary>
    [TestClass]
    public class ScheduleBooking_ValidatePeoplePresent_UITestCases : FunctionalTest
    {
        #region Properties

        private string EnvironmentName;
        private Guid authenticationproviderid;
        private Guid _languageId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid _defaultLoginUserID;
        public Guid environmentid;
        private string _loginUser_Username;
        private string teamName;
        private string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");
        private string tenantName;
        private Guid cPSchedulingSetupId;

        internal Guid _recurrencePattern_Every1WeekMondayId;
        internal Guid _recurrencePattern_Every1WeekTuesdayId;
        internal Guid _recurrencePattern_Every1WeekWednesdayId;
        internal Guid _recurrencePattern_Every1WeekThursdayId;
        internal Guid _recurrencePattern_Every1WeekFridayId;
        internal Guid _recurrencePattern_Every1WeekSaturdayId;
        internal Guid _recurrencePattern_Every1WeekSundayId;

        #endregion

        #region Internal Methods

        internal void CreateUserWorkSchedule(Guid UserId, Guid TeamId, Guid SystemUserEmploymentContractId, Guid availabilityTypeId)
        {
            for (int i = 0; i < 7; i++)
            {
                var workScheduleDate = DateTime.Now.AddDays(i).Date;

                switch (workScheduleDate.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekSundayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    case DayOfWeek.Monday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekMondayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    case DayOfWeek.Tuesday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekTuesdayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    case DayOfWeek.Wednesday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekWednesdayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    case DayOfWeek.Thursday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekThursdayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    case DayOfWeek.Friday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekFridayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    case DayOfWeek.Saturday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekSaturdayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        [TestInitialize()]
        public void TestSetup()
        {
            try
            {
                #region Authentication

                authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").First();

                #endregion

                #region SDK API User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];
                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);

                #endregion

                #region Environment Name

                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CareProvidersPeopleBU");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                teamName = "CareProvidersPeopleTeam";
                _teamId = commonMethodsDB.CreateTeam(teamName, null, _businessUnitId, "90400", "CareProviderPeopleTeams@careworkstempmail.com", teamName, "020 125556");

                #endregion

                #region Create default system user

                _loginUser_Username = "ScheduleBookingPeopleUser1";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUser_Username, "ScheduleBookingPeople", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

                #endregion

                #region Care Provider Scheduling Setup

                cPSchedulingSetupId = dbHelper.cPSchedulingSetup.GetAllActiveRecords().FirstOrDefault();
                dbHelper.cPSchedulingSetup.UpdateCheckStaffAvailability(cPSchedulingSetupId, 4); //Check and Offer Create
                dbHelper.cPSchedulingSetup.UpdateUpdateBookingEndDayDateTime(cPSchedulingSetupId, true);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-5251

        [TestProperty("JiraIssueID", "ACC-2663")]
        [Description("Step(s) 1 to 12 from the original test ACC-2663")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        [TestProperty("Screen2", "People Schedule")]
        [TestProperty("Screen3", "Express Book")]
        [TestProperty("Screen4", "Express Booking Result")]
        [TestProperty("Screen5", "Provider Diary")]
        [TestProperty("Screen6", "People Diary")]
        public void ScheduleBooking_ValidatePeople_ACC_2663_UITestCases001()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "P2663a " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var establishmentProviderName = "Provider " + currentTimeString;
            var establishmentProviderId = commonMethodsDB.CreateProvider(establishmentProviderName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type 2

            dbHelper.cPSchedulingSetup.UpdateUpdateBookingEndDayDateTime(cPSchedulingSetupId, true);

            var _bookingType2Id = commonMethodsDB.CreateCPBookingType("BTC2 2663", 2, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, 1000, null, null, null);

            #endregion

            #region Booking Type 5 -> "Booking (To Service User)"

            var _bookingType5Name = "BTC5 2663";
            var _bookingType5Id = commonMethodsDB.CreateCPBookingType(_bookingType5Name, 5, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, null, null, null, null, 3);

            #endregion

            #region Booking Type 6 -> "Booking (Service User non-care booking)" 

            var _bookingType6Name = "BTC6 2663";
            var _bookingType6Id = commonMethodsDB.CreateCPBookingType(_bookingType6Name, 6, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, establishmentProviderId, _bookingType6Id, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType5Id, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType2Id, false);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 2663a", "99910", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleTypeid2 = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 2663b", "99911", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleTypeid3 = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 2663c", "99912", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region Recurrence Patterns

            _recurrencePattern_Every1WeekMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on monday").First();
            _recurrencePattern_Every1WeekTuesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on tuesday").First();
            _recurrencePattern_Every1WeekWednesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on wednesday").First();
            _recurrencePattern_Every1WeekThursdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on thursday").First();
            _recurrencePattern_Every1WeekFridayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on friday").First();
            _recurrencePattern_Every1WeekSaturdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on saturday").First();
            _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region System User

            //set the user as rostered user and use a persona to link it to the user
            var user1name = "PeopleUser1" + currentTimeString;
            var user1FirstName = "People";
            var user1LastName = "User1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            var user2name = "PeopleUser2" + currentTimeString;
            var user2FirstName = "People";
            var user2LastName = "User2 " + currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser1EmploymentContractId2 = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid2, _teamId, _employmentContractTypeid1, 47);
            var _systemUser1EmploymentContractId3 = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid3, _teamId, _employmentContractTypeid1, 47);

            var _systemUser2EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser2Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            var _systemUser1EmploymentContractId2_Title = (string)dbHelper.systemUserEmploymentContract.GetByID(_systemUser1EmploymentContractId2, "name")["name"];
            var _systemUser1EmploymentContractId3_Title = (string)dbHelper.systemUserEmploymentContract.GetByID(_systemUser1EmploymentContractId3, "name")["name"];

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType6Id);

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType5Id);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId2, _bookingType5Id);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId3, _bookingType5Id);

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2Id);

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingType6Id);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingType5Id);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingType2Id);


            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser2Id, _teamId, _systemUser2EmploymentContractId, _availabilityTypeId);

            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId2, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId3, _availabilityTypeId);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, establishmentProviderId, funderProviderID);

            var contractScheme2Name = "CPCSB_" + currentTimeString;
            var contractScheme2Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme2Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme2Name, new DateTime(2020, 1, 1), contractScheme2Code, providerId, funderProviderID);
            dbHelper.careProviderContractScheme.UpdateDefaultAllPersonContractsEnabledForScheduledBookings(careProviderContractScheme2Id, true);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, true);

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType6Id, null, "");
            commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType5Id, null, "");

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", establishmentProviderId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType6Id, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractServiceId2 = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme2Id, careProviderService1Id, null, _bookingType5Id, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Pedro";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, establishmentProviderId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);
            string _personContractTitle = (string)dbHelper.careProviderPersonContract.GetByID(_personcontractId, "title")["title"];


            var _personcontractId2 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme2Id, funderProviderID, todayDate.AddDays(-5), null, true);
            string _personContractTitle2 = (string)dbHelper.careProviderPersonContract.GetByID(_personcontractId2, "title")["title"];

            #endregion

            #region Booking Schedule


            //Booking1 - Booking Type - BTC5. assigned staff. People - Null
            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType5Id, 1, 1, 1, new TimeSpan(4, 0, 0), new TimeSpan(22, 0, 0), providerId, "People Validation Test");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);
            var cpBookingSchedule1Id_Title = (string)dbHelper.cpBookingSchedule.GetById(cpBookingSchedule1Id, "title")["title"];

            //Booking2 - Booking Type - BTC6. Unassigned staff. People - Assigned
            var cpBookingSchedule2Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType6Id, 1, 2, 2, new TimeSpan(9, 0, 0), new TimeSpan(13, 0, 0), establishmentProviderId, "People Validation Test");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule2Id, 1);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule2Id, _personID, _personcontractId, careProviderContractServiceId);

            //Booking3 - Booking Type - BTC2. Assigned staff. People - Null
            var cpBookingSchedule3Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2Id, 1, 6, 6, new TimeSpan(9, 0, 0), new TimeSpan(13, 0, 0), providerId, "People Validation Test");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule3Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule3Id, _systemUser2EmploymentContractId, systemUser2Id);

            #endregion

            #region Express Booking Criteria

            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, providerId, commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(13), commonMethodsHelper.GetCurrentDateWithoutCulture(), providerId, "provider", "P2663a " + currentTimeString);
            var _expressBookingCriteriaId2 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, establishmentProviderId, commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(13), commonMethodsHelper.GetCurrentDateWithoutCulture(), _personcontractId, "careproviderpersoncontract", _personContractTitle);


            #endregion

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            #endregion

            #region Step 3

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderSchedulePageToLoad()
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true)
                .ValidateScheduleBookingIsPresent(cpBookingSchedule3Id.ToString(), true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleScheduleSection();

            peopleSchedulePage
                .WaitForPeopleSchedulePageToLoad()
                .selectProvider(establishmentProviderName + " - pna, pno, st, dst, tw, co, CR0 3RL", establishmentProviderId)
                .WaitForPeopleSchedulePageToLoad()
                .ValidateScheduleBookingIsPresent(cpBookingSchedule2Id.ToString(), true);

            #endregion

            #region Step 4

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            #endregion

            #region Step 5

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + currentTimeString)
                .ValidateRecordPresent(_expressBookingCriteriaId1, true)
                .ValidateRecordPresent(_expressBookingCriteriaId2, true);

            #endregion

            #region Step 6, 7

            #region Scheduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(4000);

            #endregion

            #region Scheduled job for Express Book

            //get the schedule job id
            scheduleJobId = dbHelper.scheduledJob.GetByPartialName(firstName + " " + lastName).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            expressBookingCriteriaPage
                .ClickRefreshButton()
                .WaitForExpressBookingCriteriaPageToLoad()
                .OpenRecord(_expressBookingCriteriaId1);

            //get express booking result by express book criteria id from dbHelper
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(_expressBookingCriteriaId1);
            Assert.AreEqual(1, expressBookingResults.Count); //1 Express Booking result is present for booking schedule associated with BTC5

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .OpenRecord(expressBookingResults[0]);

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoad()
                .ValidateBookingScheduleLinkText(cpBookingSchedule1Id_Title)
                .ValidateExpressBookingFailureReasonSelectedText("Incorrect Number Of People")
                .ValidateExceptionMessageText("Bookings of this type should have exactly one person assigned but there are 0. Booking not created.")
                .ClickBackButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickBackButton();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .OpenRecord(_expressBookingCriteriaId2);

            expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(_expressBookingCriteriaId2);
            Assert.AreEqual(0, expressBookingResults.Count); //0 Express Booking result for booking schedule associated with BTC6

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .ValidateNoRecordMessageVisible(true);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Care Provider Diary Bookings")
                .ClickDeleteButton()
                .ClickSearchButton();

            //Provider Diary Booking for Provider Schedule Booking using BTC2
            var cpDiaryBooking_BTC2_Id1 = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(1, cpDiaryBooking_BTC2_Id1.Count);

            //People Diary Booking for People Schedule Booking using BTC6
            var cpDiaryBooking_BTC6_Id2 = dbHelper.cPBookingDiary.GetByLocationId(establishmentProviderId);
            Assert.AreEqual(1, cpDiaryBooking_BTC6_Id2.Count);

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(2)
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(2)
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(cpDiaryBooking_BTC2_Id1[0].ToString())
                .ValidateSearchResultRecordPresent(cpDiaryBooking_BTC6_Id2[0].ToString());

            #endregion

            #region Step 8

            var peopleDiaryExpressBookDate = commonMethodsHelper.GetDayOfWeek(commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7), DayOfWeek.Tuesday);
            var providerDiaryExpressBookDate1 = commonMethodsHelper.GetDayOfWeek(commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7), DayOfWeek.Saturday);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleDiarySection();

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .selectProvider(establishmentProviderName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForPeopleDiaryPageToLoad()
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(peopleDiaryExpressBookDate);

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(cpDiaryBooking_BTC6_Id2[0].ToString());

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL", providerId)
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(providerDiaryExpressBookDate1);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(cpDiaryBooking_BTC2_Id1[0].ToString(), true);

            #endregion

            #region Step 9 and Step 10

            //Booking1 - Booking Type - BTC5. assigned staff. People - Null
            cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType5Id, 1, 3, 3, new TimeSpan(4, 0, 0), new TimeSpan(22, 0, 0), providerId, "People Validation Test2");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId2, systemUser1Id);
            cpBookingSchedule1Id_Title = (string)dbHelper.cpBookingSchedule.GetById(cpBookingSchedule1Id, "title")["title"];

            //Booking1 - Booking Type - BTC5. assigned staff. People - Assigned
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId2);

            dbHelper.person.UpdateDeceased(_personID, true);

            //create express booking criteria for system user 1 employment contract id2
            var _expressBookingCriteriaId3 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, providerId, commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(13), commonMethodsHelper.GetCurrentDateWithoutCulture(), _systemUser1EmploymentContractId2, "systemuseremploymentcontract", _systemUser1EmploymentContractId2_Title);

            #region Scheduled job for Express Book

            //get the schedule job id
            scheduleJobId = dbHelper.scheduledJob.GetByPartialName(_systemUser1EmploymentContractId2_Title).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + currentTimeString)
                .ValidateRecordPresent(_expressBookingCriteriaId3, true)
                .OpenRecord(_expressBookingCriteriaId3);

            expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(_expressBookingCriteriaId3);
            Assert.AreEqual(2, expressBookingResults.Count); //2 Express Booking results are present for booking schedule associated with BTC5

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .OpenRecord(expressBookingResults[0]);

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoad()
                .ValidateBookingScheduleLinkText(cpBookingSchedule1Id_Title)
                .ValidateExpressBookingFailureReasonSelectedText("Person Deceased")
                .ValidateExceptionMessageText(firstName + " " + lastName + " cannot be assigned to bookings as they are deceased.")
                .ClickBackButton();

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .OpenRecord(expressBookingResults[1]);

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoad()
                .ValidateBookingScheduleLinkText(cpBookingSchedule1Id_Title)
                .ValidateExpressBookingFailureReasonSelectedText("Incorrect Number Of People")
                .ValidateExceptionMessageText("Bookings of this type should have exactly one person assigned but there are 0. Booking not created.")
                .ClickBackButton();

            #endregion

            #region Step 11 and Step 12

            //This scenario to test overlapping booking for same person at same duration is not valid anymore. 2 Schedule bookings cannot be created.

            dbHelper.person.UpdateDeceased(_personID, false);

            cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType5Id, 1, 4, 4, new TimeSpan(7, 0, 0), new TimeSpan(10, 15, 0), providerId, "People Validation Test3");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId3, systemUser1Id);
            cpBookingSchedule1Id_Title = (string)dbHelper.cpBookingSchedule.GetById(cpBookingSchedule1Id, "title")["title"];
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId2);

            //create express booking criteria for system user 1 employment contract id3
            var _expressBookingCriteriaId4 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, providerId, commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(13), commonMethodsHelper.GetCurrentDateWithoutCulture(), _systemUser1EmploymentContractId3, "systemuseremploymentcontract", _systemUser1EmploymentContractId3_Title);

            #region Scheduled job for Express Book

            //get the schedule job id
            scheduleJobId = dbHelper.scheduledJob.GetByPartialName(_systemUser1EmploymentContractId3_Title).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + currentTimeString)
                .ValidateRecordPresent(_expressBookingCriteriaId4, true)
                .OpenRecord(_expressBookingCriteriaId4);

            expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(_expressBookingCriteriaId4);
            Assert.AreEqual(0, expressBookingResults.Count); //0 Express Booking results are present for booking schedule associated with BTC5

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .ValidateNoRecordMessageVisible(true);

            var cpDiaryBookingId2 = dbHelper.cPBookingDiary.GetByCPBookingScheduleId(cpBookingSchedule1Id);
            Assert.AreEqual(1, cpDiaryBookingId2.Count);
            var providerDiaryExpressBookDate2 = commonMethodsHelper.GetDayOfWeek(commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7), DayOfWeek.Thursday);
            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL", providerId)
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(providerDiaryExpressBookDate2);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(cpDiaryBookingId2[0].ToString(), true);


            #endregion

        }

        #endregion

    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.ProviderDiary
{
    /// <summary>
    /// This class contains Automated UI test scripts for Express Book - Contract Validations.
    /// </summary>
    [TestClass]
    public class ExpressBook_ACC_6669_UITestCases : FunctionalTest
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
        private Guid cpSchedulingSetupId;

        internal Guid _recurrencePattern_Every1WeekMondayId;
        internal Guid _recurrencePattern_Every1WeekTuesdayId;
        internal Guid _recurrencePattern_Every1WeekWednesdayId;
        internal Guid _recurrencePattern_Every1WeekThursdayId;
        internal Guid _recurrencePattern_Every1WeekFridayId;
        internal Guid _recurrencePattern_Every1WeekSaturdayId;
        internal Guid _recurrencePattern_Every1WeekSundayId;
        private Guid cpBookingScheduleId;

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

        internal Guid CreateCPBookingSchedule(Guid TeamID, Guid BookingTypeId, Guid ProviderID, Guid SystemUserEmploymentContractId, Guid SystemUserId)
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            switch (todayDate.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 1, 1, new TimeSpan(0, 0, 0), new TimeSpan(8, 0, 0), ProviderID, "Express Book Contracted Hours Validation1");
                    dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingScheduleId, 1);
                    dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);
                    break;

                case DayOfWeek.Tuesday:
                    cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 2, 2, new TimeSpan(0, 0, 0), new TimeSpan(8, 0, 0), ProviderID, "Express Book Contracted Hours Validation1");
                    dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);
                    break;

                case DayOfWeek.Wednesday:
                    cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 3, 3, new TimeSpan(0, 0, 0), new TimeSpan(8, 0, 0), ProviderID, "Express Book Contracted Hours Validation1");
                    dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);
                    break;

                case DayOfWeek.Thursday:
                    cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 4, 4, new TimeSpan(0, 0, 0), new TimeSpan(8, 0, 0), ProviderID, "Express Book Contracted Hours Validation1");
                    dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);
                    break;

                case DayOfWeek.Friday:
                    cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 5, 5, new TimeSpan(0, 0, 0), new TimeSpan(8, 0, 0), ProviderID, "Express Book Contracted Hours Validation1");
                    dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);
                    break;

                case DayOfWeek.Saturday:
                    cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 6, 6, new TimeSpan(0, 0, 0), new TimeSpan(8, 0, 0), ProviderID, "Express Book Contracted Hours Validation1");
                    dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);
                    break;

                case DayOfWeek.Sunday:
                    cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 7, 7, new TimeSpan(0, 0, 0), new TimeSpan(8, 0, 0), ProviderID, "Express Book Contracted Hours Validation1");
                    dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);
                    break;
            }
            return cpBookingScheduleId;
        }

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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("Care Providers EBCV");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                teamName = "Care Providers EBCV";
                _teamId = commonMethodsDB.CreateTeam(teamName, null, _businessUnitId, "90400", "CareProvidersCV@careworkstempmail.com", teamName, "020 123456");

                #endregion

                #region Create default system user

                _loginUser_Username = "EBUser6669";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUser_Username, "EB", "User6669", "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

                #endregion

                #region Care Provider Scheduling Setup

                cpSchedulingSetupId = dbHelper.cPSchedulingSetup.GetAllActiveRecords().FirstOrDefault();
                dbHelper.cPSchedulingSetup.UpdateCheckStaffAvailability(cpSchedulingSetupId, 4); //Check and Offer Create
                dbHelper.cPSchedulingSetup.UpdateUpdateBookingEndDayDateTime(cpSchedulingSetupId, true);
                dbHelper.cPSchedulingSetup.UpdateHourlyField(cpSchedulingSetupId, 2); //No Check                

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }


        #region https://advancedcsg.atlassian.net/browse/ACC-xxxx

        [TestProperty("JiraIssueID", "ACC-6685")]
        [Description("Step(s) 1 to 10 from the original test ACC-6669 - Contract Type = Hourly, Check")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ExpressBook_ACC_6669_UITesCases001()
        {

            #region Provider

            var providerName = "Hosp6685 " + currentTimeString;
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 3, true); //Hospital

            #endregion

            #region Booking Type

            var _bookingType1Name = "BTC1 6685";
            var _bookingType1Id = commonMethodsDB.CreateCPBookingType(_bookingType1Name, 1, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType1Id, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6685", "98910", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Hourly

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Hourly", "", null, new DateTime(2020, 1, 1));

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

            var user1name = "EB6685SU1_" + currentTimeString;
            var user1FirstName = "EB6685";
            var user1LastName = "SU1_" + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            string _systemUser1EmploymentContractId_Title = (string)dbHelper.systemUserEmploymentContract.GetByID(_systemUser1EmploymentContractId, "name")["name"];

            dbHelper.systemUserEmploymentContract.UpdateContractHoursPerWeek(_systemUser1EmploymentContractId, 9m);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType1Id);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule
            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1Id, 1, 5, 5, new TimeSpan(0, 0, 0), new TimeSpan(8, 0, 0), providerId, "Express Book Contracted Hours Validation2");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

            var cpBookingSchedule2Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1Id, 1, 7, 1, new TimeSpan(22, 0, 0), new TimeSpan(2, 0, 0), providerId, "Express Book Contracted Hours Validation2");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule2Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule2Id, _systemUser1EmploymentContractId, systemUser1Id);

            #endregion

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 8

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(providerName + " - No Address");

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true)
                .ValidateScheduleBookingIsPresent(cpBookingSchedule2Id.ToString(), true);

            #endregion

            #region Step 9, 10

            #region Express Booking Criteria for current week

            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, providerId, commonMethodsHelper.GetThisWeekFirstMonday(), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(6), commonMethodsHelper.GetCurrentDateWithoutCulture(), _systemUser1EmploymentContractId, "systemuseremploymentcontract", _systemUser1EmploymentContractId_Title);

            #endregion

            dbHelper.cPSchedulingSetup.UpdateHourlyField(cpSchedulingSetupId, 1); //Check

            #region Schdeduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(_systemUser1EmploymentContractId_Title).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(_expressBookingCriteriaId1);
            Assert.AreEqual(1, expressBookingResultId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + currentTimeString)
                .OpenRecord(_expressBookingCriteriaId1);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ValidateStatusSelectedText("Succeeded")
                .ClickResultsTab();

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .OpenRecord(expressBookingResultId[0]);

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoad()
                .ValidateExpressBookingFailureReasonSelectedText("Staff Exceeded Contract Hours")
                .ValidateExceptionMessageText("This booking exceeds the contracted hours for " + user1FirstName + " " + user1LastName + ", they have been deallocated.");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(providerName + " - No Address");

            var careProviderDiaryBookings1 = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(2, careProviderDiaryBookings1.Count);

            Guid careProviderDiaryBooking1 = dbHelper.cPBookingDiary.GetByCPBookingScheduleId(cpBookingSchedule1Id)[0];
            Guid careProviderDiaryBooking2 = dbHelper.cPBookingDiary.GetByCPBookingScheduleId(cpBookingSchedule2Id)[0];

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDatePicker();

            //wait for calendar picker popup to load and select this week's friday's date from calendar picker
            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(commonMethodsHelper.GetThisWeekFirstMonday().AddDays(4));

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(careProviderDiaryBooking1.ToString(), true);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickMinusButton();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickNextDateButton();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(careProviderDiaryBooking2.ToString(), true)
                .ValidateUnassignedDiaryBookingIsPresent(careProviderDiaryBooking2.ToString());

            #region Express Booking Criteria for next week

            var _expressBookingCriteriaId2 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, providerId, commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(13), commonMethodsHelper.GetCurrentDateWithoutCulture(), _systemUser1EmploymentContractId, "systemuseremploymentcontract", _systemUser1EmploymentContractId_Title);

            #endregion

            #region Schdeduled job for Express Book

            //get the schedule job id
            scheduleJobId = dbHelper.scheduledJob.GetByPartialName(_systemUser1EmploymentContractId_Title).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(_expressBookingCriteriaId2);
            Assert.AreEqual(1, expressBookingResultId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + currentTimeString)
                .OpenRecord(_expressBookingCriteriaId2);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ValidateStatusSelectedText("Succeeded")
                .ClickResultsTab();

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .OpenRecord(expressBookingResultId[0]);

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoad()
                .ValidateExpressBookingFailureReasonSelectedText("Staff Exceeded Contract Hours")
                .ValidateExceptionMessageText("This booking exceeds the contracted hours for " + user1FirstName + " " + user1LastName + ", they have been deallocated.");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(providerName + " - No Address");

            careProviderDiaryBookings1 = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(4, careProviderDiaryBookings1.Count);

            Guid careProviderDiaryBooking3 = dbHelper.cPBookingDiary.GetByCPBookingScheduleId(cpBookingSchedule1Id)[0];
            Guid careProviderDiaryBooking4 = dbHelper.cPBookingDiary.GetByCPBookingScheduleId(cpBookingSchedule2Id)[0];

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickViewWeekButton();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(careProviderDiaryBooking1.ToString(), true)
                .ValidateDiaryBookingIsPresent(careProviderDiaryBooking2.ToString(), true)
                .ValidateUnassignedDiaryBookingIsPresent(careProviderDiaryBooking2.ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickNextDateButton();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(careProviderDiaryBooking3.ToString(), true)
                .ValidateDiaryBookingIsPresent(careProviderDiaryBooking4.ToString(), true)
                .ValidateUnassignedDiaryBookingIsPresent(careProviderDiaryBooking4.ToString());

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6686")]
        [Description("Step(s) 1 to 10 from the original test ACC-6669 - Contract Type = Hourly, Warn Only")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ExpressBook_ACC_6669_UITesCases002()
        {

            #region Provider

            var providerName = "Hosp6686 " + currentTimeString;
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 3, true); //Hospital

            #endregion

            #region Booking Type

            var _bookingType1Name = "BTC1 6686";
            var _bookingType1Id = commonMethodsDB.CreateCPBookingType(_bookingType1Name, 1, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType1Id, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6686", "98910", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Hourly

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Hourly", "", null, new DateTime(2020, 1, 1));

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

            var user1name = "EB6686SU1_" + currentTimeString;
            var user1FirstName = "EB6686";
            var user1LastName = "SU1_" + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            string _systemUser1EmploymentContractId_Title = (string)dbHelper.systemUserEmploymentContract.GetByID(_systemUser1EmploymentContractId, "name")["name"];

            dbHelper.systemUserEmploymentContract.UpdateContractHoursPerWeek(_systemUser1EmploymentContractId, 9m);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType1Id);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule
            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1Id, 1, 5, 5, new TimeSpan(0, 0, 0), new TimeSpan(8, 0, 0), providerId, "Express Book Contracted Hours Validation2");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

            var cpBookingSchedule2Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1Id, 1, 7, 1, new TimeSpan(22, 0, 0), new TimeSpan(2, 0, 0), providerId, "Express Book Contracted Hours Validation2");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule2Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule2Id, _systemUser1EmploymentContractId, systemUser1Id);

            #endregion

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 8

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(providerName + " - No Address");

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true)
                .ValidateScheduleBookingIsPresent(cpBookingSchedule2Id.ToString(), true);

            #endregion

            #region Step 9, 10

            #region Express Booking Criteria for current week

            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, providerId, commonMethodsHelper.GetThisWeekFirstMonday(), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(6), commonMethodsHelper.GetCurrentDateWithoutCulture(), _systemUser1EmploymentContractId, "systemuseremploymentcontract", _systemUser1EmploymentContractId_Title);

            #endregion

            dbHelper.cPSchedulingSetup.UpdateHourlyField(cpSchedulingSetupId, 3); //Warn Only

            #region Schdeduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(_systemUser1EmploymentContractId_Title).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(_expressBookingCriteriaId1);
            Assert.AreEqual(1, expressBookingResultId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + currentTimeString)
                .OpenRecord(_expressBookingCriteriaId1);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ValidateStatusSelectedText("Succeeded")
                .ClickResultsTab();

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .OpenRecord(expressBookingResultId[0]);

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoad()
                .ValidateExpressBookingFailureReasonSelectedText("Staff Exceeded Contract Hours")
                .ValidateExceptionMessageText("Warning - This booking exceeds the contracted hours for " + user1FirstName + " " + user1LastName + ".");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(providerName + " - No Address");

            var careProviderDiaryBookings1 = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(2, careProviderDiaryBookings1.Count);

            Guid careProviderDiaryBooking1 = dbHelper.cPBookingDiary.GetByCPBookingScheduleId(cpBookingSchedule1Id)[0];
            Guid careProviderDiaryBooking2 = dbHelper.cPBookingDiary.GetByCPBookingScheduleId(cpBookingSchedule2Id)[0];

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDatePicker();

            //wait for calendar picker popup to load and select this week's friday's date from calendar picker
            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(commonMethodsHelper.GetThisWeekFirstMonday().AddDays(4));

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(careProviderDiaryBooking1.ToString(), true);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickMinusButton();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickNextDateButton();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(careProviderDiaryBooking2.ToString(), true)
                .ValidateUnassignedDiaryBookingIsNotPresent(careProviderDiaryBooking2.ToString());

            #region Express Booking Criteria for next week

            var _expressBookingCriteriaId2 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, providerId, commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(13), commonMethodsHelper.GetCurrentDateWithoutCulture(), _systemUser1EmploymentContractId, "systemuseremploymentcontract", _systemUser1EmploymentContractId_Title);

            #endregion

            #region Schdeduled job for Express Book

            //get the schedule job id
            scheduleJobId = dbHelper.scheduledJob.GetByPartialName(_systemUser1EmploymentContractId_Title).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(_expressBookingCriteriaId2);
            Assert.AreEqual(2, expressBookingResultId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + currentTimeString)
                .OpenRecord(_expressBookingCriteriaId2);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ValidateStatusSelectedText("Succeeded")
                .ClickResultsTab();

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .OpenRecord(expressBookingResultId[0]);

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoad()
                .ValidateExpressBookingFailureReasonSelectedText("Staff Exceeded Contract Hours")
                .ValidateExceptionMessageText("Warning - This booking exceeds the contracted hours for " + user1FirstName + " " + user1LastName + ".")
                .ClickBackButton();

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .OpenRecord(expressBookingResultId[1]);

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoad()
                .ValidateExpressBookingFailureReasonSelectedText("Staff Exceeded Contract Hours")
                .ValidateExceptionMessageText("Warning - This booking exceeds the contracted hours for " + user1FirstName + " " + user1LastName + ".");


            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(providerName + " - No Address");

            careProviderDiaryBookings1 = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(4, careProviderDiaryBookings1.Count);

            Guid careProviderDiaryBooking3 = dbHelper.cPBookingDiary.GetByCPBookingScheduleId(cpBookingSchedule1Id)[0];
            Guid careProviderDiaryBooking4 = dbHelper.cPBookingDiary.GetByCPBookingScheduleId(cpBookingSchedule2Id)[0];

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickViewWeekButton();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(careProviderDiaryBooking1.ToString(), true)
                .ValidateDiaryBookingIsPresent(careProviderDiaryBooking2.ToString(), true)
                .ValidateUnassignedDiaryBookingIsNotPresent(careProviderDiaryBooking2.ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickNextDateButton();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(careProviderDiaryBooking3.ToString(), true)
                .ValidateDiaryBookingIsPresent(careProviderDiaryBooking4.ToString(), true)
                .ValidateUnassignedDiaryBookingIsNotPresent(careProviderDiaryBooking4.ToString());

            #endregion

        }

        #endregion

    }
}

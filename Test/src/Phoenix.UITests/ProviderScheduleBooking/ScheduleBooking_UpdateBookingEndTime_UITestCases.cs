using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;


namespace Phoenix.UITests.ProviderScheduleBooking
{
    /// <summary>
    /// This class contains Automated UI test scripts for Provider Schedule Booking - Update End Time Day Date Validation
    /// </summary>
    [TestClass]
    public class ScheduleBooking_UpdateBookingEndTime_UITestCases : FunctionalTest
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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CareProviders");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                teamName = "CareProviders";
                _teamId = commonMethodsDB.CreateTeam(teamName, null, _businessUnitId, "90400", "CareProviders@careworkstempmail.com", teamName, "020 125556");

                #endregion

                #region Create default system user

                _loginUser_Username = "ScheduleBookingEndDateUser1";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUser_Username, "ScheduleBookingEndDate", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

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

        #region https://advancedcsg.atlassian.net/browse/ACC-6497

        [TestProperty("JiraIssueID", "ACC-6514")]
        [Description("Step(s) 1 to 12, 15 from the original test ACC-6496")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ScheduleBooking_UpdateBookingEndTime_ACC_6496_UITestCases001()
        {

            #region Provider

            var providerName = "P6514 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Booking Type

            dbHelper.cPSchedulingSetup.UpdateUpdateBookingEndDayDateTime(cPSchedulingSetupId, true);

            var _bookingTypeBTC1 = commonMethodsDB.CreateCPBookingType("BTC1 6514 " + currentTimeString, 1, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 3, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC1, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6514", "99910", null, new DateTime(2020, 1, 1), null);

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
            var user1name = "ValidationUser1" + currentTimeString;
            var user1FirstName = "Validation";
            var user1LastName = "User1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            var user2name = "ValidationUser2" + currentTimeString;
            var user2FirstName = "Validation";
            var user2LastName = "User2 " + currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser2EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser2Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingTypeBTC1);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser2Id, _teamId, _systemUser2EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingTypeBTC1, 1, 1, 1, new TimeSpan(4, 0, 0), new TimeSpan(22, 0, 0), providerId, "Update End Time Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);


            var cpBookingSchedule2Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingTypeBTC1, 1, 6, 1, new TimeSpan(9, 0, 0), new TimeSpan(2, 0, 0), providerId, "Update End Time Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule2Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule2Id, _systemUser2EmploymentContractId, systemUser2Id);

            #endregion

            #region Step 1 to Step 6, Step 8

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderSchedulePageToLoad()
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .SetStartTime("03", "00");

            createScheduleBookingPopup
                .ValidateStartTime("03:00")
                .ValidateEndTime("21:00")
                .ClickSaveChangesBookingButton();

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateStartTime("03:00")
                .ValidateEndTime("21:00")
                .ClickOnCloseButton();

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .SetStartDay("Sunday")
                .SetStartTime("23", "00")
                .ValidateStartDayText("Sunday")
                .ValidateEndDayText("Monday")
                .ValidateStartTime("23:00")
                .ValidateEndTime("17:00")
                .ClickSaveChangesBookingButton();

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateStartDayText("Sunday")
                .ValidateEndDayText("Monday")
                .ValidateStartTime("23:00")
                .ValidateEndTime("17:00")
                .ClickOnCloseButton();

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickScheduleBooking(cpBookingSchedule2Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateStartTime("09:00")
                .ValidateEndTime("02:00")
                .SetStartDay("Sunday")
                .ValidateStartDayText("Sunday")
                .ValidateEndDayText("Tuesday")
                .ClickSaveChangesBookingButton();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickScheduleBooking(cpBookingSchedule2Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateStartDayText("Sunday")
                .ValidateEndDayText("Tuesday")
                .ValidateStartTime("09:00")
                .ValidateEndTime("02:00")
                .ClickOnCloseButton();

            #endregion

            #region Step 15

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickScheduleBooking(cpBookingSchedule2Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .SetEndTime("01", "00")
                .ValidateEndTime("01:00")
                .ValidateStartTime("09:00")
                .ClickSaveChangesBookingButton();

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickScheduleBooking(cpBookingSchedule2Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateStartDayText("Sunday")
                .ValidateEndDayText("Tuesday")
                .ValidateStartTime("09:00")
                .ValidateEndTime("01:00")
                .ClickOnCloseButton();

            #endregion

            #region Step 9 - 10, Step 12

            dbHelper.cPSchedulingSetup.UpdateUpdateBookingEndDayDateTime(cPSchedulingSetupId, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderSchedulePageToLoad()
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .SetStartTime("02", "00");

            createScheduleBookingPopup
                .ValidateStartTime("02:00")
                .ValidateEndTime("17:00")
                .ClickSaveChangesBookingButton();

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateStartTime("02:00")
                .ValidateEndTime("17:00")
                .ClickOnCloseButton();

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .SetStartDay("Monday")
                .SetStartTime("22", "00")
                .ValidateStartDayText("Monday")
                .ValidateEndDayText("Monday")
                .ValidateStartTime("22:00")
                .ValidateEndTime("17:00")
                .ClickSaveChangesBookingButton();

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateStartDayText("Monday")
                .ValidateEndDayText("Monday")
                .ValidateStartTime("22:00")
                .ValidateEndTime("17:00")
                .ClickOnCloseButton();

            dbHelper.cPSchedulingSetup.UpdateUpdateBookingEndDayDateTime(cPSchedulingSetupId, true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6515")]
        [Description("Step(s) 13 to 14 from the original test ACC-6496")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Employee Schedule")]
        public void ScheduleBooking_UpdateBookingEndTime_ACC_6496_UITestCases002()
        {

            #region Provider

            var providerName = "P6515 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Booking Type

            dbHelper.cPSchedulingSetup.UpdateUpdateBookingEndDayDateTime(cPSchedulingSetupId, true);

            var _bookingTypeBTC1 = commonMethodsDB.CreateCPBookingType("BTC1 6515 " + currentTimeString, 1, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC1, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6515", "99910", null, new DateTime(2020, 1, 1), null);

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
            var user1name = "ValidationUser3" + currentTimeString;
            var user1FirstName = "Validation";
            var user1LastName = "User3 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            var user2name = "ValidationUser4" + currentTimeString;
            var user2FirstName = "Validation";
            var user2LastName = "User4 " + currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser2EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser2Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingTypeBTC1);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser2Id, _teamId, _systemUser2EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingTypeBTC1, 1, 1, 1, new TimeSpan(4, 0, 0), new TimeSpan(22, 0, 0), providerId, "Update End Time Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);


            var cpBookingSchedule2Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingTypeBTC1, 1, 6, 1, new TimeSpan(9, 0, 0), new TimeSpan(2, 0, 0), providerId, "Update End Time Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule2Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule2Id, _systemUser2EmploymentContractId, systemUser2Id);

            #endregion

            #region Step 13, 14

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(user1name)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(systemUser1Id);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmployeeSchedulePage();

            employeeSchedulePage
                .WaitForEmployeeSchedulePageToLoad()
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .SetStartTime("03", "00");

            createScheduleBookingPopup
                .ValidateStartTime("03:00")
                .ValidateEndTime("21:00")
                .ClickSaveChangesBookingButton();

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupClosed();

            employeeSchedulePage
                .WaitForEmployeeSchedulePageToLoad()
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateStartTime("03:00")
                .ValidateEndTime("21:00")
                .ClickOnCloseButton();

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupClosed();

            employeeSchedulePage
                .WaitForEmployeeSchedulePageToLoad()
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .SetStartDay("Sunday")
                .SetStartTime("23", "00")
                .ValidateStartDayText("Sunday")
                .ValidateEndDayText("Monday")
                .ValidateStartTime("23:00")
                .ValidateEndTime("17:00")
                .ClickSaveChangesBookingButton();

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupClosed();

            employeeSchedulePage
                .WaitForEmployeeSchedulePageToLoad()
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateStartDayText("Sunday")
                .ValidateEndDayText("Monday")
                .ValidateStartTime("23:00")
                .ValidateEndTime("17:00")
                .ClickOnCloseButton();

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupClosed();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(user2name)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(systemUser2Id);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmployeeSchedulePage();

            employeeSchedulePage
                .WaitForEmployeeSchedulePageToLoad()
                .ClickScheduleBooking(cpBookingSchedule2Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateStartTime("09:00")
                .ValidateEndTime("02:00")
                .SetStartDay("Sunday")
                .ValidateStartDayText("Sunday")
                .ValidateEndDayText("Tuesday")
                .ClickSaveChangesBookingButton();

            employeeSchedulePage
                .WaitForEmployeeSchedulePageToLoad()
                .ClickScheduleBooking(cpBookingSchedule2Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateStartDayText("Sunday")
                .ValidateEndDayText("Tuesday")
                .ValidateStartTime("09:00")
                .ValidateEndTime("02:00")
                .ClickOnCloseButton();

            #endregion

        }

        #endregion

    }
}

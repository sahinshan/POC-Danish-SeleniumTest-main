using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;


namespace Phoenix.UITests.ProviderScheduleBooking
{
    /// <summary>
    /// This class contains Automated UI test scripts for Diary Booking - Update End Time Day Date Validation
    /// </summary>
    [TestClass]
    public class DiaryBooking_UpdateBookingEndTime_UITestCases : FunctionalTest
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

                _loginUser_Username = "DiaryBookingEndDateUser1";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUser_Username, "DiaryBookingEndDate", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

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

        #region https://advancedcsg.atlassian.net/browse/ACC-6519

        [TestProperty("JiraIssueID", "ACC-6536")]
        [Description("Step(s) 1 to 12, 15 from the original test ACC-6517")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void DiaryBooking_UpdateBookingEndTime_ACC_6517_UITestCases001()
        {
            var futureDate = commonMethodsHelper.GetDatePartWithoutCulture().AddDays(3);
            var nextDate = commonMethodsHelper.GetDatePartWithoutCulture().AddDays(2);
            var previousDate = commonMethodsHelper.GetDatePartWithoutCulture();

            var startYear = futureDate.Year.ToString();
            var startMonth = futureDate.ToString("MMMM");
            var startDay = futureDate.Day.ToString();

            var nextDateYear = nextDate.Year.ToString();
            var nextDateMonth = nextDate.ToString("MMMM");
            var nextDateDay = nextDate.Day.ToString();

            var previousDateYear = previousDate.Year.ToString();
            var previousDateMonth = previousDate.ToString("MMMM");
            var previousDateDay = previousDate.Day.ToString();

            #region Scheduling Setup

            dbHelper.cPSchedulingSetup.UpdateUpdateBookingEndDayDateTime(cPSchedulingSetupId, true);

            #endregion

            #region Provider

            var providerName = "P6536 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Booking Type


            var _bookingTypeBTC1 = commonMethodsDB.CreateCPBookingType("BTC1 6536 " + currentTimeString, 1, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC1, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6536", "99910", null, new DateTime(2020, 1, 1), null);

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
            var user1name = "DiaryValidationUser1" + currentTimeString;
            var user1FirstName = "DiaryValidation";
            var user1LastName = "User1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            var user2name = "DiaryValidationUser2" + currentTimeString;
            var user2FirstName = "DiaryValidation";
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

            #region Diary Booking

            var cpDiaryBookingId1 = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingTypeBTC1, providerId, nextDate, new TimeSpan(9, 0, 0), nextDate, new TimeSpan(13, 00, 0));

            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", cpDiaryBookingId1, _systemUser1EmploymentContractId, systemUser1Id);

            var cpDiaryBookingId2 = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingTypeBTC1, providerId, nextDate, new TimeSpan(10, 0, 0), nextDate, new TimeSpan(12, 00, 0));

            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", cpDiaryBookingId2, _systemUser2EmploymentContractId, systemUser2Id);

            #endregion

            #region Step 1 to Step 8

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL", providerId);

            System.Threading.Thread.Sleep(1500);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(nextDate);

            //Step 5 - Change start time
            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(cpDiaryBookingId1.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .InsertStartTime("03", "00");

            createDiaryBookingPopup
                .ValidateStartTime("03:00")
                .ValidateEndTime("07:00")
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupClosed();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(cpDiaryBookingId1.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateStartTime("03:00")
                .ValidateEndTime("07:00")
                .ClickOnCloseButton();

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupClosed();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(cpDiaryBookingId1.ToString());

            //Step 6, 8 - Change start time so that end time spills over to next day
            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .SelectStartDate(previousDateYear, previousDateMonth, previousDateDay)
                .InsertStartTime("23", "00")
                .ValidateStartDate(previousDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDate(previousDate.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .ValidateStartTime("23:00")
                .ValidateEndTime("03:00")
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupClosed();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(previousDate);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(cpDiaryBookingId1.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .SelectStartDate(startYear, startMonth, startDay)
                .ValidateStartDate(futureDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDate(futureDate.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .ValidateStartTime("23:00")
                .ValidateEndTime("03:00")
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupClosed();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(futureDate);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(cpDiaryBookingId1.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .SelectStartDate(startYear, startMonth, startDay)
                .ValidateStartDate(futureDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDate(futureDate.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .ValidateStartTime("23:00")
                .ValidateEndTime("03:00")
                .ClickOnCloseButton()
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("You have unsaved changes. Are you sure you want to close the drawer?")
                .ClickConfirmButton_WarningDialogue();

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupClosed();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(nextDate);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(cpDiaryBookingId2.ToString());

            //Step 7 - Change start date
            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateStartTime("10:00")
                .ValidateEndTime("12:00")
                .SelectStartDate(nextDateYear, nextDateMonth, nextDateDay)
                .ValidateStartDate(nextDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDate(nextDate.ToString("dd'/'MM'/'yyyy"))
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupClosed();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(nextDate);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(cpDiaryBookingId2.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateStartDate(nextDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDate(nextDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateStartTime("10:00")
                .ValidateEndTime("12:00")
                .ClickOnCloseButton();

            #endregion

            #region Step 15

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupClosed();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(cpDiaryBookingId2.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .InsertEndTime("18", "00")
                .ValidateEndTime("18:00")
                .ValidateStartTime("10:00")
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupClosed();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(cpDiaryBookingId2.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateStartDate(nextDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDate(nextDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateStartTime("10:00")
                .ValidateEndTime("18:00")
                .ClickOnCloseButton();

            #endregion

            #region Step 9 - 10, Step 12

            dbHelper.cPSchedulingSetup.UpdateUpdateBookingEndDayDateTime(cPSchedulingSetupId, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL", providerId);

            System.Threading.Thread.Sleep(1500);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(futureDate);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(cpDiaryBookingId1.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateStartDate(futureDate.ToString("dd'/'MM'/'yyyy"))
                .InsertStartTime("02", "00");

            createDiaryBookingPopup
                .ValidateStartTime("02:00")
                .ValidateEndTime("03:00")
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupClosed();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(cpDiaryBookingId1.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateStartDate(futureDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDate(futureDate.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .ValidateStartTime("02:00")
                .ValidateEndTime("03:00")
                .ClickOnCloseButton();

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupClosed();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(cpDiaryBookingId1.ToString());

            //Step 9 - Change Start Time -
            //Step 10 is a generic statement for End Date, End Time validation and same as Step 12

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .SelectStartDate(nextDateYear, nextDateMonth, nextDateDay)
                .InsertStartTime("23", "30")
                .ValidateStartDate(nextDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDate(futureDate.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .ValidateStartTime("23:30")
                .ValidateEndTime("03:00")
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupClosed();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(nextDate);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(cpDiaryBookingId1.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateStartDate(nextDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDate(futureDate.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .ValidateStartTime("23:30")
                .ValidateEndTime("03:00")
                .ClickOnCloseButton();

            dbHelper.cPSchedulingSetup.UpdateUpdateBookingEndDayDateTime(cPSchedulingSetupId, true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6537")]
        [Description("Step(s) 13 to 14 from the original test ACC-6517")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Employee Diary")]
        public void DiaryBooking_UpdateBookingEndTime_ACC_6517_UITestCases002()
        {

            var futureDate = commonMethodsHelper.GetDatePartWithoutCulture().AddDays(3);
            var nextDate = commonMethodsHelper.GetDatePartWithoutCulture().AddDays(2);
            var previousDate = commonMethodsHelper.GetDatePartWithoutCulture();

            var nextDateYear = nextDate.Year.ToString();
            var nextDateMonth = nextDate.ToString("MMMM");
            var nextDateDay = nextDate.Day.ToString();

            var previousDateYear = previousDate.Year.ToString();
            var previousDateMonth = previousDate.ToString("MMMM");
            var previousDateDay = previousDate.Day.ToString();

            #region Provider

            var providerName = "P6537 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Booking Type

            dbHelper.cPSchedulingSetup.UpdateUpdateBookingEndDayDateTime(cPSchedulingSetupId, true);

            var _bookingTypeBTC1 = commonMethodsDB.CreateCPBookingType("BTC1 6537 " + currentTimeString, 1, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC1, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6537", "99910", null, new DateTime(2020, 1, 1), null);

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
            var user1name = "DiaryValidationUser3" + currentTimeString;
            var user1FirstName = "DiaryValidation";
            var user1LastName = "User3 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            var user2name = "DiaryValidationUser4" + currentTimeString;
            var user2FirstName = "DiaryValidation";
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

            #region Diary Booking

            var cpDiaryBookingId1 = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingTypeBTC1, providerId, nextDate, new TimeSpan(4, 0, 0), nextDate, new TimeSpan(22, 00, 0));

            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", cpDiaryBookingId1, _systemUser1EmploymentContractId, systemUser1Id);

            var cpDiaryBookingId2 = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingTypeBTC1, providerId, nextDate, new TimeSpan(9, 0, 0), nextDate, new TimeSpan(14, 00, 0));

            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", cpDiaryBookingId2, _systemUser2EmploymentContractId, systemUser2Id);

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
                .NavigateToEmployeeDiaryPage();

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad();

            employeeDiaryPage
                .ClickDiaryBooking(cpDiaryBookingId1.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .InsertStartTime("03", "00");

            createDiaryBookingPopup
                .ValidateStartTime("03:00")
                .ValidateEndTime("21:00")
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupClosed();

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ClickDiaryBooking(cpDiaryBookingId1.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateStartTime("03:00")
                .ValidateEndTime("21:00")
                .ValidateStartDate(nextDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDate(nextDate.ToString("dd'/'MM'/'yyyy"))
                .ClickOnCloseButton();

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupClosed();

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ClickDiaryBooking(cpDiaryBookingId1.ToString());

            //Step 6 - Change Start Date for the booking
            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .SelectStartDate(previousDateYear, previousDateMonth, previousDateDay)
                .ValidateStartDate(previousDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDate(previousDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateStartTime("03:00")
                .ValidateEndTime("21:00")
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupClosed();

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ClickDiaryBooking(cpDiaryBookingId1.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .SelectStartDate(previousDateYear, previousDateMonth, previousDateDay)
                .ValidateStartDate(previousDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDate(previousDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateStartTime("03:00")
                .ValidateEndTime("21:00")
                .ClickOnCloseButton()
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("You have unsaved changes. Are you sure you want to close the drawer?")
                .ClickConfirmButton_WarningDialogue();

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupClosed();

            //Step 7, 8 - Change start time so that end time spills over to next day

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
                .NavigateToEmployeeDiaryPage();

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ClickDiaryBooking(cpDiaryBookingId2.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .InsertStartTime("21", "00")
                .ValidateStartDate(nextDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDate(futureDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateStartTime("21:00")
                .ValidateEndTime("02:00")
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupClosed();

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ClickDiaryBooking(cpDiaryBookingId2.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateStartDate(nextDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDate(futureDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateStartTime("21:00")
                .ValidateEndTime("02:00")
                .ClickOnCloseButton();

            //Step 9 - Change Start Time
            //Step 10 is a generic statement for End Date, End Time validation and same as Step 11 and 12


            dbHelper.cPSchedulingSetup.UpdateUpdateBookingEndDayDateTime(cPSchedulingSetupId, false);

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
                .NavigateToEmployeeDiaryPage();

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ClickDiaryBooking(cpDiaryBookingId2.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .InsertStartTime("23", "00")
                .ValidateStartDate(nextDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDate(futureDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateStartTime("23:00")
                .ValidateEndTime("02:00")
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupClosed();

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ClickDiaryBooking(cpDiaryBookingId2.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateStartDate(nextDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDate(futureDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateStartTime("23:00")
                .ValidateEndTime("02:00");

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .SelectStartDate(nextDateYear, nextDateMonth, nextDateDay)
                .InsertStartTime("06", "00")
                .ValidateStartDate(nextDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDate(futureDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateStartTime("06:00")
                .ValidateEndTime("02:00")
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupClosed();

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ClickDiaryBooking(cpDiaryBookingId2.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateStartDate(nextDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDate(futureDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateStartTime("06:00")
                .ValidateEndTime("02:00");

            dbHelper.cPSchedulingSetup.UpdateUpdateBookingEndDayDateTime(cPSchedulingSetupId, true);

            #endregion

        }


        #endregion

    }
}

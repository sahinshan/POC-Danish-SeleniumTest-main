using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.ProviderScheduleBooking
{
    /// <summary>
    /// This class contains Automated UI test scripts for Provider Schedule Booking.
    /// </summary>    
    [TestClass]
    public class ProviderScheduleBooking_UITestCases : FunctionalTest
    {
        #region Private Properties

        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private string _systemUsername;
        private string _defaultSystemUserFullname;
        private TimeZone _localZone;
        private string _teamName;
        private string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");
        private string currentDateString = DateTime.Now.ToString("yyyyMMdd");
        private string _tenantName;
        private string EnvironmentName;
        private Guid cPSchedulingSetupId;
        private Guid _hourlyOvertime_availabilityTypeId;

        internal Guid _recurrencePattern_Every1WeekMondayId;
        internal Guid _recurrencePattern_Every1WeekTuesdayId;
        internal Guid _recurrencePattern_Every1WeekWednesdayId;
        internal Guid _recurrencePattern_Every1WeekThursdayId;
        internal Guid _recurrencePattern_Every1WeekFridayId;
        internal Guid _recurrencePattern_Every1WeekSaturdayId;
        internal Guid _recurrencePattern_Every1WeekSundayId;

        internal Guid _recurrencePattern_Every2WeeksMondayId;
        internal Guid _recurrencePattern_Every2WeeksTuesdayId;
        internal Guid _recurrencePattern_Every2WeeksWednesdayId;
        internal Guid _recurrencePattern_Every2WeeksThursdayId;
        internal Guid _recurrencePattern_Every2WeeksFridayId;
        internal Guid _recurrencePattern_Every2WeeksSaturdayId;
        internal Guid _recurrencePattern_Every2WeeksSundayId;

        #endregion

        #region Test Initialize
        [TestInitialize()]
        public void ProviderScheduleBookingTestSetupMethod()
        {
            try
            {
                #region Internal

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];
                _localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, _localZone.StandardName);
                _defaultSystemUserFullname = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(defaultSystemUserId, "fullname")["fullname"];

                #endregion

                #region Environment Name

                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                _tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("PSB BU " + currentDateString);

                #endregion

                #region Team

                _teamName = "PSBT " + currentDateString;
                _teamId = commonMethodsDB.CreateTeam(_teamName, null, _businessUnitId, "107623", "PSBT" + currentDateString + "@careworkstempmail.com", "Provider Schedule Booking Team " + currentDateString, "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region Create default system user

                _systemUsername = "ProviderScheduleBookingUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "ProviderScheduleBooking", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

                #region Care Provider Scheduling Setup

                cPSchedulingSetupId = dbHelper.cPSchedulingSetup.GetAllActiveRecords().FirstOrDefault();
                dbHelper.cPSchedulingSetup.UpdateCheckStaffAvailability(cPSchedulingSetupId, 4); //Check and Offer Create

                #endregion

                #region CP Schedule Setup - Set Auto Refresh to No

                cPSchedulingSetupId = dbHelper.cPSchedulingSetup.GetAllActiveRecords().FirstOrDefault();
                dbHelper.cPSchedulingSetup.UpdateAutoRefresh(cPSchedulingSetupId, false); //Set Auto Refresh to No
                dbHelper.cPSchedulingSetup.UpdateAutoRefreshInterval(cPSchedulingSetupId, null);

                #endregion

                #region Availabilty Type - Hourly/Overtime - Validity For Diary Bookings - Valid

                _hourlyOvertime_availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Hourly/Overtime").First();
                dbHelper.availabilityTypes.UpdateDiaryBookingsValidityId(_hourlyOvertime_availabilityTypeId, 1);

                #endregion

                #region System Settings

                commonMethodsDB.CreateSystemSetting("EnableQAEdit", "true", "This setting enables the Add and Save buttons in the standard Care Cloud forms for some business objects. This will allow QA to test scenarios that would be difficult to reproduce otherwise. \r\n\r\nThis setting replaces the EnableAdvancedSearchAllToolbarButtons setting.\r\nIt should not be shipped.", false, "");

                #endregion
            }

            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #endregion

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

        internal void CreateUserWorkScheduleForFrequency(Guid UserId, Guid TeamId, int frequencyOfOccurrence, Guid SystemUserEmploymentContractId, Guid availabilityTypeId, DateTime workScheduleDate, TimeSpan StartTime, TimeSpan EndTime, int WeekNumber)
        {

            for (int i = 0; i < 7; i++)
            {
                switch (workScheduleDate.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every2WeeksSundayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, StartTime, EndTime, WeekNumber);
                        if (frequencyOfOccurrence > 1)
                            workScheduleDate = workScheduleDate.AddDays((frequencyOfOccurrence - 1) * 7);
                        break;
                    case DayOfWeek.Monday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every2WeeksMondayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, StartTime, EndTime, WeekNumber);
                        break;
                    case DayOfWeek.Tuesday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every2WeeksTuesdayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, StartTime, EndTime, WeekNumber);
                        break;
                    case DayOfWeek.Wednesday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every2WeeksWednesdayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, StartTime, EndTime, WeekNumber);
                        break;
                    case DayOfWeek.Thursday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every2WeeksThursdayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, StartTime, EndTime, WeekNumber);
                        break;
                    case DayOfWeek.Friday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every2WeeksFridayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, StartTime, EndTime, WeekNumber);
                        break;
                    case DayOfWeek.Saturday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every2WeeksSaturdayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, StartTime, EndTime, WeekNumber);
                        break;
                    default:
                        break;
                }
                workScheduleDate = workScheduleDate.AddDays(1);
            }
        }

        internal void CreateUserWorkSchedule(Guid UserId, Guid TeamId, Guid SystemUserEmploymentContractId, Guid availabilityTypeId, DateTime workScheduleDate, TimeSpan StartTime, TimeSpan EndTime, int WeekNumber)
        {

            switch (workScheduleDate.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekSundayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, StartTime, EndTime, WeekNumber);
                    break;
                case DayOfWeek.Monday:
                    dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekMondayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, StartTime, EndTime, WeekNumber);
                    break;
                case DayOfWeek.Tuesday:
                    dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekTuesdayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, StartTime, EndTime, WeekNumber);
                    break;
                case DayOfWeek.Wednesday:
                    dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekWednesdayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, StartTime, EndTime, WeekNumber);
                    break;
                case DayOfWeek.Thursday:
                    dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekThursdayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, StartTime, EndTime, WeekNumber);
                    break;
                case DayOfWeek.Friday:
                    dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekFridayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, StartTime, EndTime, WeekNumber);
                    break;
                case DayOfWeek.Saturday:
                    dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekSaturdayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, StartTime, EndTime, WeekNumber);
                    break;
                default:
                    break;
            }
        }


        #region https://advancedcsg.atlassian.net/browse/ACC-5109

        [TestProperty("JiraIssueID", "ACC-5130")]
        [Description("Step(s) 1 to 7 from the original test ACC-4767")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_UITestMethod001()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var _providerName = "Prov " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            #endregion

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            #endregion

            #region Step 3

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad();

            #endregion

            #region Step 4

            createScheduleBookingPopup
                .ValidateRosteringTabIsVisible(true)
                .ValidateOccurrenceTabIsVisible(true)
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickRosteringTab();

            #endregion

            #region Step 5

            createScheduleBookingPopup
                .ValidateMandatoryFieldIsDisplayed("Location / Provider", true)
                .ValidateMandatoryFieldIsDisplayed("Booking Type", true)
                .ValidateMandatoryFieldIsDisplayed("Start Day", true)
                .ValidateMandatoryFieldIsDisplayed("Start Time", true)
                .ValidateMandatoryFieldIsDisplayed("End Day", true)
                .ValidateMandatoryFieldIsDisplayed("End Time", true)
                .ValidateMandatoryFieldIsDisplayed("Comments", false)
                .ValidateMandatoryFieldIsDisplayed("Staff", false);

            createScheduleBookingPopup
                .ValidateBookingTypeDropDownText("Select")
                .ClickCreateBooking();

            #endregion

            #region Step 6

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("You haven't filled in all required fields.")
                .ClickDismissButton_DynamicDialogue();


            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5109 2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, false);

            #endregion

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickOnCloseButton()
                .WaitForCreateScheduleBookingPopupClosed();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC ACC-5109 2")
                .RemoveStaffFromSelectedStaffField("0")
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("You need to have at least 1 staff member for this booking type.")
                .ClickDismissButton_DynamicDialogue();

            #endregion

            #region Step 7  

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickAddUnassignedStaff();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .InsertTextInCommentsTextArea(_providerName + currentTimeString)
                .ClickCreateBooking();

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(2000);

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);


            providerSchedulePage
               .WaitForProviderSchedulePageToLoad()
               .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1000);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateStaffLabelText("Staff: Unassigned × 1")
                .ValidateTimeLabelText("Time: " + DateTime.Now.ToString("dddd") + " 06:00 - 22:00")
                .ValidateProviderLabelText("Provider: " + _providerName)
                .ValidateAddressLabelText("Address: No Address")
                .ValidateBookingTypeLabelText("Booking Type: BTC ACC-5109 2")
                .ValidateOccursLabelText("Occurs: Every 1 week");

            providerSchedulePage
                .ValidateScheduleBookingSliderTitle(careProviderBookingSchedules[0].ToString(), "BTC ACC-5109 2");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5140

        [TestProperty("JiraIssueID", "ACC-5139")]
        [Description("Step(s) 8 to 12 from the original test ACC-4767")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_UITestMethod002()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role5140" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "Prov " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5139 2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "StaffA" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "StaffA ", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            string _staffBName = "StaffB" + currentTimeString;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_staffBName, "StaffB ", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId2, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId2, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId2, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Step 8

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address");

            #endregion

            #region Step 9

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .InsertTextInCommentsTextArea(_providerName + currentTimeString);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1.ToString())
                .ClickStaffRecordCellText(_systemUserEmploymentContractId2.ToString())
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1000);

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(1000);

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad()
               .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(2000);

            providerSchedulePage
                .ValidateTimeLabelText("Time: " + DateTime.Now.ToString("dddd") + " 06:00 - 22:00")
                .ValidateProviderLabelText("Provider: " + _providerName)
                .ValidateAddressLabelText("Address: No Address")
                .ValidateBookingTypeLabelText("Booking Type: BTC ACC-5139 2")
                .ValidateOccursLabelText("Occurs: Every 1 week")
                .ValidateStaffLabelText("Staff: StaffA " + currentTimeString + ", " + "StaffB " + currentTimeString);

            providerSchedulePage
                .ValidateScheduleBookingSliderTitle(careProviderBookingSchedules[0].ToString(), "BTC ACC-5139 2");

            #endregion

            #region Step 10 - Start/End Day is set to Current Day, Start/End Time is set to Default Start/End time from Booking Type.

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad()
               .ClickScheduleBooking(careProviderBookingSchedules[0].ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")
                .ValidateBookingTypeDropDownText("BTC ACC-5139 2")
                .ValidateGenderPreferenceBookingDropdownText("No Preference")
                .ValidateSelectedStaffFieldValues(_systemUserEmploymentContractId1.ToString(), "StaffA " + currentTimeString)
                .ValidateSelectedStaffFieldValues(_systemUserEmploymentContractId2.ToString(), "StaffB " + currentTimeString)
                .ValidateStartDayText(todayDate.DayOfWeek.ToString())
                .ValidateStartTime("06:00")
                .ValidateEndDayText(todayDate.DayOfWeek.ToString())
                .ValidateEndTime("22:00")
                .ValidateTextInCommentsTextArea(_providerName + currentTimeString);

            #endregion

            #region Step 10 and Step 11 - Edit Start/End Day and Start/End Time

            string differentDay = todayDate.AddDays(1).DayOfWeek.ToString();

            createScheduleBookingPopup
                .SetStartDay(differentDay)
                .SetStartTime("08", "00")
                .SetEndTime("23", "00")
                .SetEndDay(differentDay)
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(2000);

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad()
               .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(2000);

            providerSchedulePage
                .ValidateTimeLabelText("08:00 - 23:00");

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad()
               .ClickScheduleBooking(careProviderBookingSchedules[0].ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")
                .ValidateStartDayText(differentDay)
                .ValidateEndDayText(differentDay)
                .ValidateStartTime("08:00")
                .ValidateEndTime("23:00");

            #endregion

            #region Step 12

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .SetStartTime("08", "00")
                .SetEndTime("08", "05");

            createScheduleBookingPopup
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("The booking must be a multiple of 15 minutes.")
                .ClickDismissButton_DynamicDialogue();

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5172

        [TestProperty("JiraIssueID", "ACC-5173")]
        [Description("Step(s) 13 to 18 from the original test ACC-4767")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_UITestMethod003()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role5173" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "P5173 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5173 2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "StaffA2" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "StaffA2", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            string _staffBName = "StaffB2" + currentTimeString;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_staffBName, "StaffB2", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId2, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId2, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId2, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Step 13

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")
                .ClickOccurrenceTab();

            createScheduleBookingPopup
                .ValidateOnAPublicHolidayMandatoryFieldVisibility(true)
                .ValidateBookingTakesPlaceEveryMandatoryFieldVisibility(true)
                .ValidateNextDueToTakePlaceMandatoryFieldVisibility(false)
                .ValidateMandatoryFieldIsDisplayed("First Occurence", false)
                .ValidateMandatoryFieldIsDisplayed("Last Occurence", false);

            #endregion

            #region Step 14

            createScheduleBookingPopup
                .ValidateBookingTakesPlaceEveryDropDownText("1 week")
                .ValidateOnAPublicHolidayDropDownText("Does Occur")
                .ValidateNextDueToTakePlaceDate("")
                .ValidateFirstOccurrenceValue("")
                .ValidateLastOccurrenceValue("");

            #endregion

            #region Step 15

            createScheduleBookingPopup
                .ValidateNextDueToTakePlaceMandatoryFieldVisibility(false)
                .SelectBookingTakesPlaceEvery("2 weeks")
                .ValidateNextDueToTakePlaceMandatoryFieldVisibility(true);

            #endregion

            #region Step 17

            var targetYear = todayDate.Year.ToString();
            var targetMonth = todayDate.ToString("MMMM");
            var targetDay = todayDate.Day.ToString();

            var NextDueTargetYear = DateTime.Now.AddDays(14).Year.ToString();
            var NextDueTargetMonth = DateTime.Now.AddDays(14).ToString("MMMM");
            var NextDueTargetDay = DateTime.Now.AddDays(14).Day.ToString();

            var LastOccurrenceTargetYear = DateTime.Now.AddDays(21).Year.ToString();
            var LastOccurrenceTargetMonth = DateTime.Now.AddDays(21).ToString("MMMM");
            var LastOccurrenceTargetDay = DateTime.Now.AddDays(21).Day.ToString();

            createScheduleBookingPopup
                .ValidateCurrentDateSelectedInFirstOccurenceCalendar(targetYear, targetMonth, targetDay);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectOnAPublicHoliday("Does Not Occur")
                .SelectNextDueToTakePlaceDate(NextDueTargetYear, NextDueTargetMonth, NextDueTargetDay)
                .SelectFirstOccurrenceDate(targetYear, targetMonth, targetDay)
                .SelectLastOccurrenceDate(LastOccurrenceTargetYear, LastOccurrenceTargetMonth, LastOccurrenceTargetDay);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickResetChangesButton()
                .ClickOccurrenceTab();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ValidateBookingTakesPlaceEveryDropDownText("1 week")
                .ValidateOnAPublicHolidayDropDownText("Does Occur")
                .ValidateNextDueToTakePlaceDate("")
                .ValidateFirstOccurrenceValue("")
                .ValidateLastOccurrenceValue("");

            #endregion

            #region Step 16 and Step 18

            createScheduleBookingPopup
                .ClickCreateAnotherBookingPicklist() //check
                .ClickCreateAnotherBookingPicklist(); //un-check

            createScheduleBookingPopup
                .ClickRosteringTab()
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .InsertTextInCommentsTextArea(_providerName + currentTimeString);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .VerifyResponsibleTeamIsDisplayed(_teamId, _teamName)
                .EnterTextIntoFilterStaffByNameSearchBox("StaffA2 " + currentTimeString);

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1.ToString());

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(2000);

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad()
               .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(2000);

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad()
               .ClickScheduleBooking(careProviderBookingSchedules[0].ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")
                .ValidateBookingTypeDropDownText("BTC ACC-5173 2")
                .ValidateStartDayText(todayDate.DayOfWeek.ToString())
                .ValidateEndDayText(todayDate.DayOfWeek.ToString())
                .ValidateStartTime("06:00")
                .ValidateEndTime("22:00")
                .ValidateGenderPreferenceBookingDropdownText("No Preference")
                .ValidateSelectedStaffFieldValues(_systemUserEmploymentContractId1.ToString(), "StaffA2 " + currentTimeString)
                .ValidateTextInCommentsTextArea(_providerName + currentTimeString);

            #endregion

            createScheduleBookingPopup
                .ClickOnCloseButton()
                .WaitForEditScheduleBookingPopupClosed();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartTime("09", "00")
                .SetEndTime("23", "00")
                .InsertTextInCommentsTextArea("2_" + _providerName + currentTimeString);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .VerifyResponsibleTeamIsDisplayed(_teamId, _teamName)
                .EnterTextIntoFilterStaffByNameSearchBox("StaffB2 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId2.ToString())
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .SelectBookingTakesPlaceEvery("2 weeks")
                .SelectNextDueToTakePlaceDate(NextDueTargetYear, NextDueTargetMonth, NextDueTargetDay)
                .SelectFirstOccurrenceDate(targetYear, targetMonth, targetDay)
                .SelectLastOccurrenceDate(LastOccurrenceTargetYear, LastOccurrenceTargetMonth, LastOccurrenceTargetDay)
                .ClickCreateAnotherBookingPicklist() //check
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ClickSaveButton_DynamicDialogue();

            System.Threading.Thread.Sleep(2000);

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ClickDismissButton_DynamicDialogue();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickOnCloseButton();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickScheduleBooking(careProviderBookingSchedules[0].ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickRosteringTab()
                .ValidateLocationProviderText(_providerName + " - No Address")
                .ValidateBookingTypeDropDownText("BTC ACC-5173 2")
                .ValidateStartDayText(todayDate.DayOfWeek.ToString())
                .ValidateEndDayText(todayDate.DayOfWeek.ToString())
                .ValidateStartTime("09:00")
                .ValidateEndTime("23:00")
                .ValidateGenderPreferenceBookingDropdownText("No Preference")
                .ValidateSelectedStaffFieldValues(_systemUserEmploymentContractId2.ToString(), "StaffB2 " + currentTimeString)
                .ValidateTextInCommentsTextArea("2_" + _providerName + currentTimeString);

            createScheduleBookingPopup
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .ValidateBookingTakesPlaceEveryDropDownText("2 weeks")
                .ValidateNextDueToTakePlaceDate(DateTime.Now.AddDays(14).ToString("dd'/'MM'/'yyyy"))
                .ValidateFirstOccurrenceValue(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidateLastOccurrenceValue(DateTime.Now.AddDays(21).ToString("dd'/'MM'/'yyyy"))
                .ClickOnCloseButton();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Care Provider Booking Schedules")
                .ClickDeleteButton()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(2)
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(2)
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(careProviderBookingSchedules[0].ToString());


        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1794

        [TestProperty("JiraIssueID", "ACC-4798")]
        [Description("Step(s) 5 to 16 from the original test ACC-4798")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void CareProviderSchedulingSetup_UITestMethod001()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var _providerName = "P4798 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-4798 2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Care Provider Scheduling Setup

            Guid _cpSchedulingSetupId = dbHelper.cPSchedulingSetup.GetCPSchedulingSetupByPlannedBookingPrecision(5)[0];


            #endregion

            #region Step 5

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSchedulingSetupPage();

            #endregion

            #region Step 6

            careProviderSchedulingSetupPage
                .WaitForCareProviderSchedulingSetupPageToLoad()
                .OpenRecord(_cpSchedulingSetupId);

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("cpschedulingsetup")
                .ClickOnExpandIcon();

            careProviderSchedulingSetupRecordPage
                .WaitForCareProviderSchedulingSetupRecordPageToLoad()
                .ValidateAllFieldsOfBookingsSection()
                .ValidateAllFieldsOfValidationSection();

            #endregion

            #region Step 7

            careProviderSchedulingSetupRecordPage
                .ValidatePlannedBookingPrecisionMandatoryFieldVisibility(true);

            #endregion

            #region Step 8

            careProviderSchedulingSetupRecordPage
                .ValidatePlannedBookingPrecisionTextFieldValue("5");

            #endregion

            #region Step 9

            careProviderSchedulingSetupRecordPage
                .ValidatePlannedBookingPrecisionFieldIsDisabled(true);

            #endregion

            #region Step 10
            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")
                .ValidateRosteringTabIsVisible();

            #endregion

            #region Step 11

            createScheduleBookingPopup
                .ValidateStartDayText(todayDate.DayOfWeek.ToString())
                .ValidateEndDayText(todayDate.DayOfWeek.ToString())
                .ValidateStartTime("06:00")
                .ValidateEndTime("22:00");

            #endregion

            #region Step 12

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartTime("12", "00")
                .SetEndTime("12", "45")
                .ClickCreateBooking();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(2000);

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad()
               .ClickScheduleBooking(careProviderBookingSchedules[0].ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateStartTime("12:00")
                .ValidateEndTime("12:45");

            #endregion

            #region Step 13

            createScheduleBookingPopup
                .SetStartTime("09", "07")
                .SetEndTime("12", "46");

            System.Threading.Thread.Sleep(500);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateStartTime("09:05")
                .ValidateEndTime("12:45");

            #endregion

            #region Step 14

            createScheduleBookingPopup
                .SetStartTime("09", "01")
                .SetEndTime("12", "44");

            System.Threading.Thread.Sleep(500);

            createScheduleBookingPopup
                .ValidateStartTime("09:00")
                .ValidateEndTime("12:45");

            #endregion

            #region Step 15

            createScheduleBookingPopup
                .SetStartTime("09", "08");

            System.Threading.Thread.Sleep(500);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateStartTime("09:10");

            createScheduleBookingPopup
                .SetStartTime("10", "13")
                .SetEndTime("12", "58");

            System.Threading.Thread.Sleep(500);

            createScheduleBookingPopup
                .ValidateStartTime("10:15")
                .ValidateEndTime("13:00")
                .ClickCreateBooking();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            providerSchedulePage
               .ClickScheduleBooking(careProviderBookingSchedules[0].ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateStartTime("10:15")
                .ValidateEndTime("13:00");

            #endregion

            #region Step 16

            createScheduleBookingPopup
                .SetStartTime("ab", "xy")
                .SetEndTime("pq", "lm");

            System.Threading.Thread.Sleep(500);

            createScheduleBookingPopup
                .ValidateStartTime("00:00")
                .ValidateEndTime("00:00");

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5250

        [TestProperty("JiraIssueID", "ACC-4802")]
        [Description("Step(s) 2 to 10 from the original test ACC-4802")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_UITestMethod004()
        {

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role4802" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "P4802 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            var _providerName2 = "P4802b " + currentTimeString;
            var _providerId2 = commonMethodsDB.CreateProvider(_providerName2, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-4802 2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-4802 2b", 2, 960, new TimeSpan(4, 0, 0), new TimeSpan(16, 0, 0), 1, false, null, null, null, 1);
            var _bookingType3 = commonMethodsDB.CreateCPBookingType("BTC ACC-4802 2c", 2, 720, new TimeSpan(4, 0, 0), new TimeSpan(16, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId2, _bookingType2, true);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId2, _bookingType3, false);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "StaffA3" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "StaffA3", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            string _staffBName = "StaffB3" + currentTimeString;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_staffBName, "StaffB3", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            string _staffCName = "StaffC3" + currentTimeString;
            var _systemUserId3 = commonMethodsDB.CreateSystemUserRecord(_staffCName, "StaffC3", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId3, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId2, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId3, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId2, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId3 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId3, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId3, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId3, _bookingType3);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId2, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId3, _teamId, _systemUserEmploymentContractId3, _availabilityTypeId);

            #endregion

            #region Step 2

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ValidateCreateAnotherBookingCheckboxIsVisible(true)
                .ValidateResetChangesButtonIsVisible(true);

            #endregion

            #region Step 3

            createScheduleBookingPopup
                .ValidateCreateAnotherBookingCheckboxIsChecked(false);

            #endregion

            #region Step 4

            createScheduleBookingPopup
                .ClickRosteringTab()
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .InsertTextInCommentsTextArea("1_" + _providerName + currentTimeString);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("StaffA3 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1.ToString())
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateAnotherBookingPicklist() //check
                .ClickCreateBooking()
                .WaitForDynamicDialogueToLoad()
                .ClickDismissButton_DynamicDialogue();

            System.Threading.Thread.Sleep(6000);

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            #endregion

            #region Step 5, Step 8

            createScheduleBookingPopup
                .ValidateCreateAnotherBookingCheckboxIsChecked(true) //step 8
                .ClickRosteringTab()
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .InsertTextInCommentsTextArea("2_" + _providerName + currentTimeString);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("StaffB3 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId2.ToString())
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartTime("08", "00")
                .SetEndTime("20", "00")
                .ValidateCreateAnotherBookingCheckboxIsChecked(true) //step 8
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ClickDismissButton_DynamicDialogue();

            System.Threading.Thread.Sleep(1000);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ValidateCreateAnotherBookingCheckboxIsChecked(true) //step 8
                .ClickOnCloseButton();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Care Provider Booking Schedules")
                .SelectFilter("1", "Booking Type")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup for Provider").SearchAndSelectRecord("BTC ACC-4802 2", _bookingType1);

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(2)
                .ClickColumnHeader(2)
                .ValidateSearchResultRecordPresent(careProviderBookingSchedules[0].ToString())
                .ValidateSearchResultRecordPresent(careProviderBookingSchedules[1].ToString());

            #endregion

            #region Step 6

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName2 + " - No Address")
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickRosteringTab()
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ValidateBookingTypeDropDownText("BTC ACC-4802 2b")
                .SelectBookingType("BTC ACC-4802 2c")
                .InsertTextInCommentsTextArea("3_" + _providerName + currentTimeString)
                .SelectGenderPreferenceBookingDropdownText("Male")
                .SetStartDay(todayDate.AddDays(1).DayOfWeek.ToString())
                .SetEndDay(todayDate.AddDays(1).DayOfWeek.ToString())
                .SetStartTime("02", "00")
                .SetEndTime("18", "00");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("StaffC3 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId3.ToString())
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateAnotherBookingPicklist() //check
                .ClickResetChangesButton();

            createScheduleBookingPopup
                .ValidateStartDayText(todayDate.DayOfWeek.ToString())
                .ValidateEndDayText(todayDate.DayOfWeek.ToString())
                .ValidateStartTime("04:00")
                .ValidateEndTime("16:00")
                .ValidateGenderPreferenceBookingDropdownText("No Preference")
                .ValidateTextInCommentsTextArea("")
                .ValidateBookingTypeDropDownText("BTC ACC-4802 2b")
                .ValidateSelectedStaffFieldValues("0", "Unassigned");

            #endregion

            #region Step 7

            createScheduleBookingPopup
                .ValidateCreateAnotherBookingCheckboxIsChecked(true)
                .ClickOnCloseButton();

            #endregion

            #region Step 9

            //Step 9 is deprecated.
            //Notes: We can not select staff contract without selecting a booking type, Also, a validation asking for filling all the fields would get displayed.Step is not valid anymore.

            #endregion

            #region Step 10

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName2 + " - No Address")
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ValidateCreateAnotherBookingCheckboxIsChecked(false);

            #endregion

        }


        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5252

        [TestProperty("JiraIssueID", "ACC-4634")]
        [Description("Step(s) 1 to 11 from the original test ACC-4634")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_UITestMethod005()
        {

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Business Unit

            var _businessUnitId2 = commonMethodsDB.CreateBusinessUnit("PSB BU2 " + currentDateString);

            #endregion

            #region Team

            var _teamName2 = "PSBT2 " + currentDateString;
            var _teamId2 = commonMethodsDB.CreateTeam(_teamName2, null, _businessUnitId2, "107624", "PSBT2_" + currentDateString + "@careworkstempmail.com", "Provider Schedule Booking Team2 " + currentDateString, "020 123456");

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role4634" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "P4634 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId2, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-4634", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "StaffA4" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "StaffA4", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId1, 1); //1 = Male

            string _staffBName = "StaffB4" + currentTimeString;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_staffBName, "StaffB4", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId2, 1); //1 = Male

            string _staffCName = "StaffC4" + currentTimeString;
            var _systemUserId3 = commonMethodsDB.CreateSystemUserRecord(_staffCName, "StaffC4", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId3, 1); //1 = Male

            string _staffDName = "StaffD4" + currentTimeString;
            var _systemUserId4 = commonMethodsDB.CreateSystemUserRecord(_staffDName, "StaffD4", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId4, 1); //1 = Male

            string _staffEName = "StaffE4" + currentTimeString;
            var _systemUserId5 = commonMethodsDB.CreateSystemUserRecord(_staffEName, "StaffE4", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId5, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId2, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId3, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId4, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId5, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region Team member for Team 2 - all System Users

            var teamMemberExists = dbHelper.teamMember.GetTeamMemberByUserAndTeamID(_systemUserId, _teamId2).Any();

            if (!teamMemberExists)
                dbHelper.teamMember.CreateTeamMember(_teamId2, _systemUserId, DateTime.Now, null);

            teamMemberExists = dbHelper.teamMember.GetTeamMemberByUserAndTeamID(_systemUserId1, _teamId2).Any();

            if (!teamMemberExists)
                dbHelper.teamMember.CreateTeamMember(_teamId2, _systemUserId1, DateTime.Now, null);

            teamMemberExists = dbHelper.teamMember.GetTeamMemberByUserAndTeamID(_systemUserId2, _teamId2).Any();

            if (!teamMemberExists)
                dbHelper.teamMember.CreateTeamMember(_teamId2, _systemUserId2, DateTime.Now, null);

            teamMemberExists = dbHelper.teamMember.GetTeamMemberByUserAndTeamID(_systemUserId3, _teamId2).Any();

            if (!teamMemberExists)
                dbHelper.teamMember.CreateTeamMember(_teamId2, _systemUserId3, DateTime.Now, null);

            teamMemberExists = dbHelper.teamMember.GetTeamMemberByUserAndTeamID(_systemUserId4, _teamId2).Any();

            if (!teamMemberExists)
                dbHelper.teamMember.CreateTeamMember(_teamId2, _systemUserId4, DateTime.Now, null);

            teamMemberExists = dbHelper.teamMember.GetTeamMemberByUserAndTeamID(_systemUserId5, _teamId2).Any();

            if (!teamMemberExists)
                dbHelper.teamMember.CreateTeamMember(_teamId2, _systemUserId5, DateTime.Now, null);

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId, _teamId2 });
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId2, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId, _teamId2 });
            var _notStarted_systemUserEmploymentContractId5 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId5, null, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId, _teamId2 });

            #endregion

            #region System User Employment Contract for Suspended status

            var systemUserEmploymentContractStartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);
            var suspension_systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId3, systemUserEmploymentContractStartDate, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId, _teamId2 });


            #endregion

            #region System User Employment Contract for Ended status

            var systemUserEmploymentContractStartDate2 = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var ended_systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId4, systemUserEmploymentContractStartDate2, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId, _teamId2 });

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(suspension_systemUserEmploymentContractId, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(ended_systemUserEmploymentContractId, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_notStarted_systemUserEmploymentContractId5, _bookingType1);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId2, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId3, _teamId, suspension_systemUserEmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId4, _teamId, ended_systemUserEmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId5, _teamId, _notStarted_systemUserEmploymentContractId5, _availabilityTypeId);

            #endregion

            #region End System User Employment Contract

            var systemUserEmploymentContractEndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-2);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(ended_systemUserEmploymentContractId, systemUserEmploymentContractEndDate, contractEndReasonId);

            #endregion

            #region Create System User Contract Suspension

            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date;
            var contracts = new List<Guid> { suspension_systemUserEmploymentContractId };
            var suspensionId = dbHelper.systemUserSuspension.CreateSystemUserSuspension(_systemUserId3, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);

            #endregion

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            #endregion

            #region Step 3

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ValidateRosteringTabIsVisible()
                .ValidateLocationProviderText(_providerName + " - No Address");

            #endregion

            #region Step 4

            createScheduleBookingPopup
                .ValidateBookingTypeDropDownText("BTC ACC-4634")
                .ValidateStartDayText(todayDate.DayOfWeek.ToString())
                .ValidateEndDayText(todayDate.DayOfWeek.ToString())
                .ValidateStartTime("06:00")
                .ValidateEndTime("22:00")
                .ValidateGenderPreferenceBookingDropdownText("No Preference")
                .ValidateSelectedStaffFieldValues("0", "Unassigned")
                .ValidateTextInCommentsTextArea("")
                .ValidateCreateAnotherBookingCheckboxIsChecked(false)
                .ValidateResetChangesButtonIsVisible(true);

            createScheduleBookingPopup
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .ValidateBookingTakesPlaceEveryDropDownText("1 week")
                .ValidateNextDueToTakePlaceFieldDisabled(true)
                .ValidateOnAPublicHolidayDropDownText("Does Occur")
                .ValidateFirstOccurrenceValue("")
                .ValidateLastOccurrenceValue("")
                .ValidateCreateAnotherBookingCheckboxIsVisible(true)
                .ValidateCreateAnotherBookingCheckboxIsChecked(false);

            #endregion

            #region Step 5

            createScheduleBookingPopup
                .ClickRosteringTab()
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickAddUnassignedStaff()
                .ValidateSelectedStaffFieldValues("0", "Unassigned", 1)
                .ValidateSelectedStaffFieldValues("1", "Unassigned", 2);

            #endregion

            #region Step 6 and Step 7

            createScheduleBookingPopup
                .RemoveStaffFromSelectedStaffField("1")
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("StaffA4 " + currentTimeString)
                .VerifyStaffRecordIsDisplayed(_systemUserEmploymentContractId1)
                .EnterTextIntoFilterStaffByNameSearchBox("StaffB4 " + currentTimeString)
                .VerifyStaffRecordIsDisplayed(_systemUserEmploymentContractId2)
                .EnterTextIntoFilterStaffByNameSearchBox("StaffC4 " + currentTimeString)
                .VerifyStaffRecordIsDisplayed(suspension_systemUserEmploymentContractId)
                .EnterTextIntoFilterStaffByNameSearchBox("StaffD4 " + currentTimeString)
                .VerifyStaffRecordIsDisplayed(ended_systemUserEmploymentContractId, false)
                .EnterTextIntoFilterStaffByNameSearchBox("StaffE4 " + currentTimeString)
                .VerifyStaffRecordIsDisplayed(_notStarted_systemUserEmploymentContractId5);

            #endregion

            #region Step 8

            selectStaffPopup
                .ClickStaffRecordCellText(_notStarted_systemUserEmploymentContractId5.ToString())
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("StaffE4 " + currentTimeString + " - " + "Role4634" + currentTimeString + " contract has not started yet, and will not be allocated to this diary booking until this contract has started.")
                .ClickDismissButton_DynamicDialogue();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("StaffC4 " + currentTimeString)
                .ClickStaffRecordCellText(suspension_systemUserEmploymentContractId.ToString())
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .RemoveStaffFromSelectedStaffField(_notStarted_systemUserEmploymentContractId5)
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("StaffC4 " + currentTimeString + " - " + "Role4634" + currentTimeString + " contract is currently suspended, and will not be allocated to this diary booking until this suspension has ended.")
                .ClickDismissButton_DynamicDialogue();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("StaffA4 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1.ToString())
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .RemoveStaffFromSelectedStaffField(suspension_systemUserEmploymentContractId)
                .InsertTextInCommentsTextArea("1_" + "StaffA4 " + currentTimeString)
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(2000);

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            var cpBookingScheduleStaffId = dbHelper.cpBookingScheduleStaff.GetBySystemUserEmploymentContractId(_systemUserEmploymentContractId1)[0];

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Care Provider Booking Schedules")
                .ClickDeleteButton();

            advanceSearchPage
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(2)
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(2)
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(careProviderBookingSchedules[0].ToString());

            advanceSearchPage
                .OpenRecord(careProviderBookingSchedules[0].ToString());

            careProviderBookingScheduleRecordPage
                .WaitForCareProviderBookingScheduleRecordPageToLoadFromAdvancedSearch()
                .WaitForStaffSectionToLoad()
                .ValidateBookingScheduleStaffRecordIsDisplayed(cpBookingScheduleStaffId)
                .OpenBookingScheduleStaffRecord(cpBookingScheduleStaffId);

            bookingScheduleStaffRecordPage
                .WaitForBookingScheduleStaffRecordPageToLoad()
                .ValidateRosteredEmployeeLinkText("StaffA4 " + currentTimeString);


            #endregion

            #region Step 9

            dbHelper.systemUserEmploymentContract.UpdateEndDate(_systemUserEmploymentContractId1, systemUserEmploymentContractEndDate, null);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ClickScheduleBooking(careProviderBookingSchedules[0].ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateSelectedStaffFieldValues("0", "Unassigned")
                .ClickOnCloseButton();

            #endregion

            #region Step 10

            dbHelper.systemUserEmploymentContract.UpdateEndDate(_systemUserEmploymentContractId1, null, null);

            #region Booking Schedule

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, careProviderBookingSchedules[0], _systemUserEmploymentContractId1, _systemUserId1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, careProviderBookingSchedules[0], _systemUserEmploymentContractId2, _systemUserId2);
            var cp_cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(careProviderBookingSchedules[0], null);

            foreach (var cp_cpBookingScheduleStaffId in cp_cpBookingScheduleStaffIds)
            {
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(cp_cpBookingScheduleStaffId);
            }

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .ClickScheduleBooking(careProviderBookingSchedules[0].ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateSelectedStaffFieldValues(_systemUserEmploymentContractId1.ToString(), "StaffA4 " + currentTimeString, 1)
                .ValidateSelectedStaffFieldValues(_systemUserEmploymentContractId2.ToString(), "StaffB4 " + currentTimeString, 2)
                .ClickOnCloseButton()
                .WaitForEditScheduleBookingPopupClosed();

            dbHelper.systemUserEmploymentContract.UpdateEndDate(_systemUserEmploymentContractId1, systemUserEmploymentContractEndDate, null);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ClickScheduleBooking(careProviderBookingSchedules[0].ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateSelectedStaffFieldValues("0", "Unassigned", 1)
                .ValidateSelectedStaffFieldValues(_systemUserEmploymentContractId2.ToString(), "StaffB4 " + currentTimeString, 2)
                .ClickOnCloseButton()
                .WaitForEditScheduleBookingPopupClosed();

            #endregion

            #region Step 11

            dbHelper.systemUserEmploymentContract.UpdateEndDate(_systemUserEmploymentContractId1, null, null);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .ClickScheduleBooking(careProviderBookingSchedules[0].ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .RemoveStaffFromSelectedStaffField("0")
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("StaffA4 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1.ToString())
                .ClickStaffConfirmSelection();

            dbHelper.systemUserEmploymentContract.DeleteCanWorksAt(_systemUserEmploymentContractId1, new List<Guid> { _teamId2 });

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .RemoveStaffFromSelectedStaffField(_systemUserEmploymentContractId2)
                .ClickCreateBooking();

            string EmploymentContract1Title = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_systemUserEmploymentContractId1, "name")["name"];

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("The staff contract of " + "StaffA4 " + currentTimeString + " " + EmploymentContract1Title + " is invalid for " + _providerName + ".")
                .ClickDismissButton_DynamicDialogue();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5257

        [TestProperty("JiraIssueID", "ACC-4800")]
        [Description("Step(s) 3 to 8 from the original test ACC-4800")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_UITestMethod006()
        {

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role4800" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "P4800 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-4800", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "StaffA5" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "StaffA5", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId1, 1); //1 = Male

            string _staffBName = "StaffB5" + currentTimeString;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_staffBName, "StaffB5", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId2, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId2, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId2, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId2, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            #endregion

            #region Step 3, Step 5 and  Step 7

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")
                .ValidateBookingTypeDropDownText("BTC ACC-4800")
                .ValidateStartDayText(todayDate.DayOfWeek.ToString())
                .ValidateEndDayText(todayDate.DayOfWeek.ToString())
                .ValidateStartTime("06:00")
                .ValidateEndTime("22:00")
                .ValidateGenderPreferenceBookingDropdownText("No Preference")
                .ValidateCommentsTextAreaIsVisible(true)
                .ValidateTextInCommentsTextArea("")
                .InsertTextInCommentsTextArea("Schedule_" + currentTimeString + "\r\nSchedule_2_" + currentTimeString + "\r\nSchedule_3_" + currentTimeString + "\r\nSchedule_4_" + currentTimeString)
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);


            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Care Provider Booking Schedules")
                .ClickDeleteButton()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(2)
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(2)
                .WaitForResultsPageToLoad()
                .OpenRecord(careProviderBookingSchedules[0].ToString());

            careProviderBookingScheduleRecordPage
                .WaitForCareProviderBookingScheduleRecordPageToLoadFromAdvancedSearch()
                .ValidateCommentsText("Schedule_" + currentTimeString + "\r\n" + "Schedule_2_" + currentTimeString + "\r\nSchedule_3_" + currentTimeString + "\r\nSchedule_4_" + currentTimeString);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .InsertTextInCommentsTextArea("Schedule_" + currentTimeString + "\r\n" + "Schedule_2_" + currentTimeString + "\r\nSchedule_3_" + currentTimeString + "\r\nSchedule_4_" + currentTimeString)
                .ClickCreateBooking();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(4000);

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            providerSchedulePage
                .ClickScheduleBooking(careProviderBookingSchedules[0].ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateTextInCommentsTextArea("Schedule_" + currentTimeString + "\r\n" + "Schedule_2_" + currentTimeString + "\r\nSchedule_3_" + currentTimeString + "\r\nSchedule_4_" + currentTimeString)
                .ClickOnCloseButton();

            #endregion

            #region Step 4, Step 6 and Step 8

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address", _providerId)
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")
                .ValidateBookingTypeDropDownText("BTC ACC-4800")
                .ValidateCommentsTextAreaIsVisible(true)
                .ValidateTextInCommentsTextArea("")
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("StaffA5 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .InsertTextInCommentsTextArea("Diary_" + currentTimeString + "\r\n" + "Diary_2_" + currentTimeString + "\r\nDiary_3_" + currentTimeString + "\r\nDiary_4_" + currentTimeString)
                .ClickCreateBooking();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();

            System.Threading.Thread.Sleep(4000);

            var careProviderDiaryBooking = dbHelper.cPBookingDiary.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderDiaryBooking.Count);

            providerDiaryPage
                .ClickDiaryBooking(careProviderDiaryBooking[0].ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateTextInCommentsTextArea("Diary_" + currentTimeString + "\r\n" + "Diary_2_" + currentTimeString + "\r\nDiary_3_" + currentTimeString + "\r\nDiary_4_" + currentTimeString);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5384

        [TestProperty("JiraIssueID", "ACC-4818")]
        [Description("Step(s) 1 to 16 from the original test ACC-4818")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_UITestMethod007()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role4818" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "P4818 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-4818", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "StaffA6" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "StaffA6", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId1, 1); //1 = Male

            string _staffBName = "StaffB6" + currentTimeString;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_staffBName, "StaffB6", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId2, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId2, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId2, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId2, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region System Setting 

            var systemSettingId = dbHelper.systemSetting.GetSystemSettingIdByName("EnableAdvancedSearchAllToolbarButtons")[0];
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingId, "true");

            #endregion

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            #endregion

            #region Step 3

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("StaffA6 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .InsertTextInCommentsTextArea("Schedule4818_" + currentTimeString);

            #endregion

            #region Step 4

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .ValidateNextDueToTakePlaceMandatoryFieldVisibility(false)
                .ValidateNextDueToTakePlaceInputDisabled(true)
                .SelectBookingTakesPlaceEvery("2 weeks")
                .ValidateNextDueToTakePlaceMandatoryFieldVisibility(true)
                .ValidateNextDueToTakePlaceInputDisabled(false);

            #endregion

            #region Step 5

            //First Occurrence Dates
            var firstOccurrenceTargetDay1 = todayDate.AddDays(1);
            var firstOccurrenceTargetDay2 = todayDate.AddDays(8);
            var firstOccurrenceTargetYear1 = firstOccurrenceTargetDay1.Year.ToString();
            var firstOccurrenceTargetMonth1 = firstOccurrenceTargetDay1.ToString("MMMM");
            var firstOccurrenceTargetYear2 = firstOccurrenceTargetDay2.Year.ToString();
            var firstOccurrenceTargetMonth2 = firstOccurrenceTargetDay2.ToString("MMMM");

            var lastOccurrenceNotSelectableDate = firstOccurrenceTargetDay1.AddDays(-2);

            //Last Occurrence Dates
            var lastOccurrenceDay1 = firstOccurrenceTargetDay1.AddDays(15);
            var lastOccurrenceYear1 = lastOccurrenceDay1.Year.ToString();
            var lastOccurrenceMonth1 = lastOccurrenceDay1.ToString("MMMM");

            createScheduleBookingPopup
                .SelectFirstOccurrenceDate(firstOccurrenceTargetYear2, firstOccurrenceTargetMonth2, firstOccurrenceTargetDay2.Day.ToString())
                .ValidateFirstOccurrenceValue(todayDate.AddDays(7).ToString("dd'/'MM'/'yyyy"))

                .ValidateLastOccurrenceDateNotSelectable(lastOccurrenceNotSelectableDate.Year.ToString(), lastOccurrenceNotSelectableDate.ToString("MMMM"), lastOccurrenceNotSelectableDate.Day.ToString())

                .SelectFirstOccurrenceDate(firstOccurrenceTargetYear1, firstOccurrenceTargetMonth1, firstOccurrenceTargetDay1.Day.ToString())
                .ValidateFirstOccurrenceValue(todayDate.ToString("dd'/'MM'/'yyyy"));

            #endregion

            #region Step 6

            createScheduleBookingPopup
                .SelectLastOccurrenceDate(lastOccurrenceYear1, lastOccurrenceMonth1, lastOccurrenceDay1.Day.ToString())
                .ValidateLastOccurrenceValue(lastOccurrenceDay1.AddDays(-2).ToString("dd'/'MM'/'yyyy"));

            #endregion

            #region Step 7

            var nextDueToTakePlaceDate1 = todayDate;
            var nextDueToTakePlaceDate2 = todayDate.AddDays(7);
            var nextDueToTakePlaceDate3 = todayDate.AddDays(14);

            var nonSelectableNextDueToTakePlaceDate1 = nextDueToTakePlaceDate1.AddDays(-1);
            var nonSelectableNextDueToTakePlaceDate2 = nextDueToTakePlaceDate1.AddDays(15);

            var nonSelectableNextDueToTakePlaceYear1 = nonSelectableNextDueToTakePlaceDate1.Year.ToString();
            var nonSelectableNextDueToTakePlaceMonth1 = nonSelectableNextDueToTakePlaceDate1.ToString("MMMM");

            var nonSelectableNextDueToTakePlaceYear2 = nonSelectableNextDueToTakePlaceDate2.Year.ToString();
            var nonSelectableNextDueToTakePlaceMonth2 = nonSelectableNextDueToTakePlaceDate2.ToString("MMMM");

            var nextDueToTakePlaceYear1 = nextDueToTakePlaceDate1.Year.ToString();
            var nextDueToTakePlaceMonth1 = nextDueToTakePlaceDate1.ToString("MMMM");
            var nextDueToTakePlaceYear2 = nextDueToTakePlaceDate2.Year.ToString();
            var nextDueToTakePlaceMonth2 = nextDueToTakePlaceDate2.ToString("MMMM");
            var nextDueToTakePlaceYear3 = nextDueToTakePlaceDate3.Year.ToString();
            var nextDueToTakePlaceMonth3 = nextDueToTakePlaceDate3.ToString("MMMM");

            createScheduleBookingPopup
                .ValidateNextDueToTakePlaceDateNotSelectable(nonSelectableNextDueToTakePlaceYear1, nonSelectableNextDueToTakePlaceMonth1, nonSelectableNextDueToTakePlaceDate1.Day.ToString())
                .ValidateNextDueToTakePlaceDateNotSelectable(nonSelectableNextDueToTakePlaceYear2, nonSelectableNextDueToTakePlaceMonth2, nonSelectableNextDueToTakePlaceDate2.Day.ToString());

            createScheduleBookingPopup
                .ValidateNextDueToTakePlaceDateSelectable(nextDueToTakePlaceYear1, nextDueToTakePlaceMonth1, nextDueToTakePlaceDate1.Day.ToString())
                .ValidateNextDueToTakePlaceDateSelectable(nextDueToTakePlaceYear2, nextDueToTakePlaceMonth2, nextDueToTakePlaceDate2.Day.ToString())
                .ValidateNextDueToTakePlaceDateSelectable(nextDueToTakePlaceYear3, nextDueToTakePlaceMonth3, nextDueToTakePlaceDate3.Day.ToString());

            #endregion

            #region Step 8, Step 9

            createScheduleBookingPopup
                .SelectNextDueToTakePlaceDate(nextDueToTakePlaceYear2, nextDueToTakePlaceMonth2, nextDueToTakePlaceDate2.Day.ToString())
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1000);

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("This booking will not be booked until " + todayDate.ToString("dd'/'MM'/'yyyy") + ".", 1)
                .ValidateMessage_DynamicDialogue("This booking will be deleted when express booking is run for a period after " + lastOccurrenceDay1.AddDays(-2).ToString("dd'/'MM'/'yyyy") + ".", 2);

            createScheduleBookingPopup
                .ClickSaveButton_DynamicDialogue();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            #endregion

            #region Step 10

            providerSchedulePage
                .ClickScheduleBooking(careProviderBookingSchedules[0].ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateStartDayText(todayDate.DayOfWeek.ToString())
                .ValidateEndDayText(todayDate.DayOfWeek.ToString())
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .ValidateFirstOccurrenceValue(todayDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateLastOccurrenceValue(lastOccurrenceDay1.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
                .ValidateNextDueToTakePlaceDate(nextDueToTakePlaceDate2.ToString("dd'/'MM'/'yyyy"));

            #endregion

            #region Step 11

            createScheduleBookingPopup
                .ClickRosteringTab()
                .WaitForEditScheduleBookingPopupPageToLoad()
                .SetStartDay(todayDate.AddDays(1).DayOfWeek.ToString());

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateIssueMessage_DynamicDialogue("Next due to take place has been changed to " + nextDueToTakePlaceDate2.AddDays(1).ToString("dd'/'MM'/'yyyy") + ".", 0)
                .ValidateIssueMessage_DynamicDialogue("First occurrence has been changed to " + todayDate.AddDays(1).ToString("dd'/'MM'/'yyyy") + ".", 1)
                .ValidateIssueMessage_DynamicDialogue("Last occurrence has been changed to " + lastOccurrenceDay1.AddDays(-1).ToString("dd'/'MM'/'yyyy") + ".", 2);

            createScheduleBookingPopup
                .ClickDismissButton_DynamicDialogue();

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickOnCloseButton()
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("You have unsaved changes. Are you sure you want to close the drawer?")
                .ClickConfirmButton_WarningDialogue();

            #endregion

            #region Step 12

            //Same as step 9 - create a schedule booking.

            #endregion

            #region Step 13

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Care Provider Booking Schedules")
                .ClickDeleteButton()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(2)
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(2)
                .OpenRecord(careProviderBookingSchedules[0].ToString());

            careProviderBookingScheduleRecordPage
                .WaitForCareProviderBookingScheduleRecordPageToLoadFromAdvancedSearch()
                .InsertTextOnFirstOccurrenceDate(firstOccurrenceTargetDay1.ToString("dd'/'MM'/'yyyy"))
                .ClickLastOccurrenceDateDatePicker();

            calendarDatePicker
                .WaitForCalendarPickerPopupToLoad()
                .SelectCalendarDate(lastOccurrenceDay1);

            careProviderBookingScheduleRecordPage
                .WaitForCareProviderBookingScheduleRecordPageToLoadFromAdvancedSearch()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Occurrence dates must fall on the same day of the week as Start Day Of Week")
                .TapCloseButton();

            careProviderBookingScheduleRecordPage
                .WaitForCareProviderBookingScheduleRecordPageToLoadFromAdvancedSearch()
                .InsertTextOnFirstOccurrenceDate(todayDate.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnLastOccurrenceDate(todayDate.AddDays(14).ToString("dd'/'MM'/'yyyy"));

            #endregion

            #region Step 14

            careProviderBookingScheduleRecordPage
                .ClickNextoccurrencedateDatePicker();

            calendarDatePicker
                .WaitForCalendarPickerPopupToLoad()
                .SelectCalendarDate(todayDate.AddDays(-2));

            careProviderBookingScheduleRecordPage
                .WaitForCareProviderBookingScheduleRecordPageToLoadFromAdvancedSearch()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("First occurrence date cannot be > Next Due to Take Place date")
                .TapCloseButton();

            careProviderBookingScheduleRecordPage
                .WaitForCareProviderBookingScheduleRecordPageToLoadFromAdvancedSearch()
                .InsertTextOnNextOccurrenceDate(todayDate.ToString("dd'/'MM'/'yyyy"))
                .ClickFirstOccurrenceDateDatePicker();

            calendarDatePicker
                .WaitForCalendarPickerPopupToLoad()
                .SelectCalendarDate(todayDate.AddDays(7));

            careProviderBookingScheduleRecordPage
                .WaitForCareProviderBookingScheduleRecordPageToLoadFromAdvancedSearch()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("First occurrence date cannot be > Next Due to Take Place date")
                .TapCloseButton();

            careProviderBookingScheduleRecordPage
                .WaitForCareProviderBookingScheduleRecordPageToLoadFromAdvancedSearch()
                .InsertTextOnNextOccurrenceDate(todayDate.AddDays(-7).ToString("dd'/'MM'/'yyyy"))
                .ClickFirstOccurrenceDateDatePicker();

            calendarDatePicker
                .WaitForCalendarPickerPopupToLoad()
                .SelectCalendarDate(todayDate.AddDays(-7));

            careProviderBookingScheduleRecordPage
                .WaitForCareProviderBookingScheduleRecordPageToLoadFromAdvancedSearch()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Next Due to Take Place date cannot be retrospective")
                .TapCloseButton();

            careProviderBookingScheduleRecordPage
                .WaitForCareProviderBookingScheduleRecordPageToLoadFromAdvancedSearch()
                .InsertTextOnNextOccurrenceDate(todayDate.AddDays(7).ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnFirstOccurrenceDate(todayDate.ToString("dd'/'MM'/'yyyy"));

            #endregion

            #region Step 15

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ClickScheduleBooking(careProviderBookingSchedules[0].ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .SetStartDay(todayDate.AddDays(1).DayOfWeek.ToString());

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateIssueMessage_DynamicDialogue("Next due to take place has been changed to " + nextDueToTakePlaceDate2.AddDays(1).ToString("dd'/'MM'/'yyyy") + ".", 0)
                .ValidateIssueMessage_DynamicDialogue("First occurrence has been changed to " + todayDate.AddDays(1).ToString("dd'/'MM'/'yyyy") + ".", 1)
                .ValidateIssueMessage_DynamicDialogue("Last occurrence has been changed to " + lastOccurrenceDay1.AddDays(-1).ToString("dd'/'MM'/'yyyy") + ".", 2);

            createScheduleBookingPopup
                .ClickDismissButton_DynamicDialogue();

            createScheduleBookingPopup
                .SetEndDay(todayDate.AddDays(1).DayOfWeek.ToString())
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1000);

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("This booking will not be booked until " + todayDate.AddDays(1).ToString("dd'/'MM'/'yyyy") + ".", 1)
                .ValidateMessage_DynamicDialogue("This booking will be deleted when express booking is run for a period after " + lastOccurrenceDay1.AddDays(-1).ToString("dd'/'MM'/'yyyy") + ".", 2)
                .ClickSaveButton_DynamicDialogue();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            #endregion

            #region Step 16

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Care Provider Booking Schedules")
                .ClickDeleteButton()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(2)
                .ClickColumnHeader(2)
                .OpenRecord(careProviderBookingSchedules[0].ToString());

            careProviderBookingScheduleRecordPage
                .WaitForCareProviderBookingScheduleRecordPageToLoadFromAdvancedSearch()
                .InsertTextOnFirstOccurrenceDate(todayDate.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnLastOccurrenceDate(todayDate.AddDays(16).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Occurrence dates must fall on the same day of the week as Start Day Of Week")
                .TapCloseButton();

            careProviderBookingScheduleRecordPage
                .WaitForCareProviderBookingScheduleRecordPageToLoadFromAdvancedSearch()
                .InsertTextOnFirstOccurrenceDate(todayDate.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnLastOccurrenceDate(todayDate.AddDays(15).ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnNextOccurrenceDate(todayDate.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("First occurrence date cannot be > Next Due to Take Place date")
                .TapCloseButton();

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5384

        [TestProperty("JiraIssueID", "ACC-5470")]
        [Description("Step(s) 1 to 7 from the original test ACC-4821")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_UITestMethod008()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Public Holiday

            var bankHolidayDate1 = DateTime.Now.AddDays(7);

            var bankHolidayDay1 = bankHolidayDate1.Day;
            var bankHolidayMonth1 = bankHolidayDate1.Month;
            var bankHolidayYear1 = bankHolidayDate1.Year;

            bankHolidayDate1 = new DateTime(bankHolidayYear1, bankHolidayMonth1, bankHolidayDay1);
            var bankHolidayName1 = "Holiday " + bankHolidayDate1.ToString("dd'/'MM'/'yyyy");
            var _bankHolidayId1 = commonMethodsDB.CreateBankHoliday(bankHolidayName1, bankHolidayDate1, bankHolidayName1);

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role5470" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "P5470 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5470", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "StaffA7" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "StaffA7", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId1, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);

            #endregion

            #region Option Set

            var optionSetName = "CP Express Book on Public Holiday";
            var optionSetId = dbHelper.optionSet.GetOptionSetIdByName(optionSetName)[0];

            #endregion

            #region Option set Values

            var DoesOccur_OptionsetValueId = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetId, "Does Occur")[0];
            var DoesNotOccur_OptionsetValueId = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetId, "Does Not Occur")[0];

            #endregion

            #region Booking Schedule

            var startDay = (int)bankHolidayDate1.DayOfWeek;
            var endDay = (int)bankHolidayDate1.DayOfWeek;

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 2, startDay, endDay, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), _providerId, "Express Book Testing " + currentTimeString);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId1, _systemUserId1);


            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
            {
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);
            }

            DateTime NextDueToTakePlace1;
            var firstoccurrencedate1 = commonMethodsHelper.GetDayOfWeek(todayDate, bankHolidayDate1.DayOfWeek);
            NextDueToTakePlace1 = firstoccurrencedate1.AddDays(7);

            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 2, NextDueToTakePlace1, firstoccurrencedate1, NextDueToTakePlace1);

            #endregion

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemManagementSection();

            systemManagementPage
                .WaitForSystemManagementPageToLoad()
                .ClickPublicHolidaysLink();

            publicHolidaysPage
                .WaitForPublicHolidaysPageToLoad()
                .ValidatePublicHolidaysRecordIsAvailable(_bankHolidayId1, true);

            #endregion

            #region Step 3

            //Option sets will no longer be available in the Customizations page.

            //mainMenu
            //    .WaitForMainMenuToLoad()
            //    .NavigateToCustomizationsSection();

            //customizationsPage
            //    .WaitForCustomizationsPageToLoad()
            //    .ClickOptionSetsButton();

            //optionSetsPage
            //    .WaitForOptionSetsPageToLoad()
            //    .InsertQuickSearchText(optionSetName)
            //    .ClickQuickSearchButton()
            //    .ValidateRecordIsPresent(optionSetId.ToString(), true)
            //    .OpenRecord(optionSetId.ToString());

            //optionSetsRecordPage
            //    .WaitForOptionSetsRecordPageToLoad()
            //    .ValidateOptionSetTextValue(optionSetName)
            //    .NavigateToOptionSetValuesPage();

            //optionsetValuesPage
            //    .WaitForOptionsetValuesPageToLoad()
            //    .ValidateOptionSetRecordIsAvailable(DoesOccur_OptionsetValueId, true)
            //    .ValidateOptionSetRecordIsAvailable(DoesNotOccur_OptionsetValueId, true);

            #endregion

            #region Step 4

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .ValidateOnAPublicHolidayDropDownText("Does Occur")
                .ValidateOnAPublicHolidayPicklistOptionIsAvailable("Does Occur", true)
                .ValidateOnAPublicHolidayPicklistOptionIsAvailable("Does Not Occur", true);

            #endregion

            #region Step 5

            createScheduleBookingPopup
                .SelectOnAPublicHoliday("Does Occur")
                .ClickRosteringTab()
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickOnCloseButton()
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("You have unsaved changes. Are you sure you want to close the drawer?")
                .ClickConfirmButton_WarningDialogue();

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupClosed();

            #region Express Book for Provider

            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId, commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(21), commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId, "provider", "P4821 " + currentTimeString);

            #endregion

            #region Schdeduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            var expressBookingCriteriaId = dbHelper.cpExpressBookingCriteria.GetByRegardingID(_providerId).First();
            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(0, expressBookingResultId.Count);

            var cpDiaryBookingId = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            Assert.AreEqual(1, cpDiaryBookingId.Count);

            var cpDiaryBookingId1 = cpDiaryBookingId[0];

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(bankHolidayDate1.ToString("yyyy"), bankHolidayDate1.ToString("MMMM"), bankHolidayDate1.Day.ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .MouseHoverDiaryBooking(cpDiaryBookingId1.ToString())
                .ValidateTimeLabelText("Planned Time: " + bankHolidayDate1.ToString("dd'/'MM'/'yyyy") + " 06:00 - 22:00");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5471")]
        [Description("Step(s) 6, 7 from the original test ACC-4821")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_UITestMethod009()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Public Holiday

            var bankHolidayDate2 = todayDate.AddDays(8);
            var bankHolidayName2 = "Holiday " + bankHolidayDate2.ToString("dd'/'MM'/'yyyy");
            var _bankHolidayId2 = commonMethodsDB.CreateBankHoliday(bankHolidayName2, bankHolidayDate2, bankHolidayName2);

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role5471" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "P5471 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5471", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffBName = "StaffB7" + currentTimeString;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_staffBName, "StaffB7", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId2, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId2, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId2, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId2, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var startDay = (int)bankHolidayDate2.DayOfWeek;
            var endDay = (int)bankHolidayDate2.DayOfWeek;

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 2, startDay, endDay, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), _providerId, "Express Book Testing " + currentTimeString);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId2, _systemUserId2);


            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
            {
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);
            }

            DateTime NextDueToTakePlace1;

            var firstoccurrencedate1 = commonMethodsHelper.GetDayOfWeek(todayDate.AddDays(1), bankHolidayDate2.DayOfWeek);
            NextDueToTakePlace1 = firstoccurrencedate1.AddDays(7);

            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 2, NextDueToTakePlace1, firstoccurrencedate1, NextDueToTakePlace1, 2);

            #endregion

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemManagementSection();

            systemManagementPage
                .WaitForSystemManagementPageToLoad()
                .ClickPublicHolidaysLink();

            publicHolidaysPage
                .WaitForPublicHolidaysPageToLoad()
                .ValidatePublicHolidaysRecordIsAvailable(_bankHolidayId2, true);

            #endregion

            #region Step 6

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .ValidateOnAPublicHolidayDropDownText("Does Not Occur")
                .ClickOnCloseButton();

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupClosed();

            #region Express Book for Provider

            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId, commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(21), commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId, "provider", "P4821 " + currentTimeString);

            #endregion

            #region Schdeduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            var expressBookingCriteriaId = dbHelper.cpExpressBookingCriteria.GetByRegardingID(_providerId).First();
            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResultId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address", _providerId);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(bankHolidayDate2.ToString("yyyy"), bankHolidayDate2.ToString("MMMM"), bankHolidayDate2.Day.ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();

            var cpDiaryBookingId = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            Assert.AreEqual(0, cpDiaryBookingId.Count);

            #endregion

            #region Step 7

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + currentTimeString)
                .ValidateRecordPresent(_expressBookingCriteriaId1, true)
                .OpenRecord(_expressBookingCriteriaId1);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .ValidateRecordPresent(expressBookingResultId[0], true)
                .OpenRecord(expressBookingResultId[0]);

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoad()
                .ValidateExceptionMessageText("Booking not required due to public holiday.");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5412

        [TestProperty("JiraIssueID", "ACC-4823")]
        [Description("Step(s) 1 to 7 from the original test ACC-4823")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_UITestMethod010()
        {

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role4823" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "P4823 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-4823", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            dbHelper.cpBookingType.UpdateValidTo(_bookingType1, null);
            dbHelper.cpBookingType.UpdateValidFrom(_bookingType1, null);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "StaffA8" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "StaffA8", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId1, 1); //1 = Male

            string _staffBName = "StaffB8" + currentTimeString;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_staffBName, "StaffB8", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId2, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId2, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId2, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId2, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            int startDay = (int)todayDate.DayOfWeek;

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, startDay, startDay, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), _providerId, "Provider Schedule Booking Testing " + currentTimeString);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId2, _systemUserId2);


            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
            {
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);
            }

            #endregion

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            #endregion

            #region Step 3

            var validFromDate1 = DateTime.Now.AddDays(2); //GreaterThanCurrentDate

            var validFromDate1Day = validFromDate1.Day;
            var validFromDate1Month = validFromDate1.Month;
            var validFromDate1Year = validFromDate1.Year;

            validFromDate1 = new DateTime(validFromDate1Year, validFromDate1Month, validFromDate1Day);

            dbHelper.cpBookingType.UpdateValidFrom(_bookingType1, validFromDate1);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad();

            #endregion

            #region Step 4

            createScheduleBookingPopup
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("Please consider updating the booking type BTC ACC-4823. It is now invalid, valid from " + validFromDate1.ToString("dd'/'MM'/'yyyy") + ".")
                .ClickDismissButton_DynamicDialogue();

            dbHelper.cpBookingType.UpdateValidFrom(_bookingType1, null);

            createScheduleBookingPopup
                .WaitForDeleteBookingDynamicDialoguePopupClosed()
                .ClickOnCloseButton();

            #endregion

            #region Step 5

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupClosed();

            var validToDate1 = DateTime.Now.AddDays(-2); //LesserThanCurrentDate

            var validToDate1Day = validToDate1.Day;
            var validToDate1Month = validToDate1.Month;
            var validToDate1Year = validToDate1.Year;

            validToDate1 = new DateTime(validToDate1Year, validToDate1Month, validToDate1Day);

            dbHelper.cpBookingType.UpdateValidTo(_bookingType1, validToDate1);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("Please consider updating the booking type BTC ACC-4823. It is now invalid, valid to " + validToDate1.ToString("dd'/'MM'/'yyyy") + ".")
                .ClickDismissButton_DynamicDialogue();

            dbHelper.cpBookingType.UpdateValidTo(_bookingType1, null);

            createScheduleBookingPopup
                .WaitForDeleteBookingDynamicDialoguePopupClosed()
                .ClickOnCloseButton();

            #endregion

            #region Step 6

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupClosed();

            var validFromDate2 = DateTime.Now.AddDays(-14); //LesserThanCurrentDate

            var validFromDate2Day = validFromDate2.Day;
            var validFromDate2Month = validFromDate2.Month;
            var validFromDate2Year = validFromDate2.Year;

            validFromDate2 = new DateTime(validFromDate2Year, validFromDate2Month, validFromDate2Day);

            dbHelper.cpBookingType.UpdateValidFrom(_bookingType1, validFromDate2);


            var validToDate2 = DateTime.Now.AddDays(-7); //LesserThanCurrentDate

            var validToDate2Day = validToDate2.Day;
            var validToDate2Month = validToDate2.Month;
            var validToDate2Year = validToDate2.Year;

            validToDate2 = new DateTime(validToDate2Year, validToDate2Month, validToDate2Day);

            dbHelper.cpBookingType.UpdateValidTo(_bookingType1, validToDate2);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("Please consider updating the booking type BTC ACC-4823. It is now invalid, valid between " + validFromDate2.ToString("dd'/'MM'/'yyyy") + " - " + validToDate2.ToString("dd'/'MM'/'yyyy") + ".")
                .ClickDismissButton_DynamicDialogue();

            dbHelper.cpBookingType.UpdateValidTo(_bookingType1, null);
            dbHelper.cpBookingType.UpdateValidFrom(_bookingType1, null);

            createScheduleBookingPopup
                .WaitForDeleteBookingDynamicDialoguePopupClosed()
                .ClickOnCloseButton();

            #endregion

            #region Step 7

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupClosed();

            var validFromDate3 = DateTime.Now.AddDays(7); //GreaterThanCurrentDate

            var validFromDate3Day = validFromDate3.Day;
            var validFromDate3Month = validFromDate3.Month;
            var validFromDate3Year = validFromDate3.Year;

            validFromDate3 = new DateTime(validFromDate3Year, validFromDate3Month, validFromDate3Day);

            dbHelper.cpBookingType.UpdateValidFrom(_bookingType1, validFromDate3);


            var validToDate3 = DateTime.Now.AddDays(14); //GreaterThanCurrentDate

            var validToDate3Day = validToDate3.Day;
            var validToDate3Month = validToDate3.Month;
            var validToDate3Year = validToDate3.Year;

            validToDate3 = new DateTime(validToDate3Year, validToDate3Month, validToDate3Day);

            dbHelper.cpBookingType.UpdateValidTo(_bookingType1, validToDate3);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("Please consider updating the booking type BTC ACC-4823. It is now invalid, valid between " + validFromDate3.ToString("dd'/'MM'/'yyyy") + " - " + validToDate3.ToString("dd'/'MM'/'yyyy") + ".")
                .ClickDismissButton_DynamicDialogue();

            dbHelper.cpBookingType.UpdateValidTo(_bookingType1, null);
            dbHelper.cpBookingType.UpdateValidFrom(_bookingType1, null);

            createScheduleBookingPopup
                .WaitForDeleteBookingDynamicDialoguePopupClosed()
                .ClickOnCloseButton();

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5413

        [TestProperty("JiraIssueID", "ACC-4831")]
        [Description("Step(s) 1 to 9 from the original test ACC-4831")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_UITestMethod011()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role4831" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "P4831 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-4831", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "StaffA9" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "StaffA9", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId1, 1); //1 = Male

            string _staffBName = "StaffB9" + currentTimeString;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_staffBName, "StaffB9", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId2, 1); //1 = Male

            string _staffCName = "StaffC9" + currentTimeString;
            var _systemUserId3 = commonMethodsDB.CreateSystemUserRecord(_staffCName, "StaffC9", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId3, 1); //1 = Male

            string _staffDName = "StaffD9" + currentTimeString;
            var _systemUserId4 = commonMethodsDB.CreateSystemUserRecord(_staffDName, "StaffD9", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId4, 1); //1 = Male

            string _staffEName = "StaffE9" + currentTimeString;
            var _systemUserId5 = commonMethodsDB.CreateSystemUserRecord(_staffEName, "StaffE9", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId5, 1); //1 = Male

            string _staffFName = "StaffF9" + currentTimeString; //Suspended
            var _systemUserId6 = commonMethodsDB.CreateSystemUserRecord(_staffFName, "StaffF9", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId6, 1); //1 = Male

            string _staffGName = "StaffG9" + currentTimeString; //Not Started
            var _systemUserId7 = commonMethodsDB.CreateSystemUserRecord(_staffGName, "StaffG9", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId7, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId2, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId3, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId4, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId5, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId6, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId7, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId2, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _notStarted_systemUserEmploymentContractId5 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId5, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            dbHelper.systemUserEmploymentContract.UpdateStartDate(_notStarted_systemUserEmploymentContractId5, null);
            var _notStarted_systemUserEmploymentContractId6 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId7, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            dbHelper.systemUserEmploymentContract.UpdateStartDate(_notStarted_systemUserEmploymentContractId6, null);

            #endregion

            #region System User Employment Contract for Suspended status

            var systemUserEmploymentContractStartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);
            var suspended_systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId3, systemUserEmploymentContractStartDate, _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var suspended_systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId6, systemUserEmploymentContractStartDate, _staffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region System User Employment Contract for Ended status

            var systemUserEmploymentContractStartDate2 = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var ended_systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId4, systemUserEmploymentContractStartDate2, _staffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(suspended_systemUserEmploymentContractId, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(ended_systemUserEmploymentContractId, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_notStarted_systemUserEmploymentContractId5, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_notStarted_systemUserEmploymentContractId6, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(suspended_systemUserEmploymentContractId2, _bookingType1);


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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId2, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId3, _teamId, suspended_systemUserEmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId4, _teamId, ended_systemUserEmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId5, _teamId, _notStarted_systemUserEmploymentContractId5, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId6, _teamId, suspended_systemUserEmploymentContractId2, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId7, _teamId, _notStarted_systemUserEmploymentContractId6, _availabilityTypeId);

            #endregion

            #region End System User Employment Contract

            var systemUserEmploymentContractEndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-2);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(ended_systemUserEmploymentContractId, systemUserEmploymentContractEndDate, contractEndReasonId);

            #endregion

            #region Create System User Contract Suspension

            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date;
            var contracts1 = new List<Guid> { suspended_systemUserEmploymentContractId };
            var contracts2 = new List<Guid> { suspended_systemUserEmploymentContractId2 };
            var suspensionId1 = dbHelper.systemUserSuspension.CreateSystemUserSuspension(_systemUserId3, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts1);
            var suspensionId2 = dbHelper.systemUserSuspension.CreateSystemUserSuspension(_systemUserId6, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts2);

            #endregion

            #region Booking Schedule

            int startDay = (int)todayDate.DayOfWeek;

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, startDay, startDay, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), _providerId, "Provider Schedule Booking Testing " + currentTimeString);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId2, _systemUserId2);


            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
            {
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);
            }

            #endregion

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 9

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("StaffC9 " + currentTimeString)
                .ClickStaffRecordCellText(suspended_systemUserEmploymentContractId)
                .EnterTextIntoFilterStaffByNameSearchBox("StaffE9 " + currentTimeString)
                .ClickStaffRecordCellText(_notStarted_systemUserEmploymentContractId5)
                .EnterTextIntoFilterStaffByNameSearchBox("StaffG9 " + currentTimeString)
                .ClickStaffRecordCellText(_notStarted_systemUserEmploymentContractId6)
                .EnterTextIntoFilterStaffByNameSearchBox("StaffF9 " + currentTimeString)
                .ClickStaffRecordCellText(suspended_systemUserEmploymentContractId2)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .InsertTextInCommentsTextArea("Edit_3_Schedule4831_" + currentTimeString)
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("StaffC9 " + currentTimeString + " - " + "Role4831" + currentTimeString + " contract is currently suspended, and will not be allocated to this diary booking until this suspension has ended.", 1)
                .ValidateMessage_DynamicDialogue("StaffE9 " + currentTimeString + " - " + "Role4831" + currentTimeString + " contract has not started yet, and will not be allocated to this diary booking until this contract has started.", 2)
                .ValidateMessage_DynamicDialogue("StaffG9 " + currentTimeString + " - " + "Role4831" + currentTimeString + " contract has not started yet, and will not be allocated to this diary booking until this contract has started.", 3)
                .ValidateMessage_DynamicDialogue("StaffF9 " + currentTimeString + " - " + "Role4831" + currentTimeString + " contract is currently suspended, and will not be allocated to this diary booking until this suspension has ended.", 4)
                .ClickDismissButton_DynamicDialogue();

            System.Threading.Thread.Sleep(1000);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickOnCloseButton()
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("You have unsaved changes. Are you sure you want to close the drawer?")
                .ClickConfirmButton_WarningDialogue();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            #endregion

            #region Step 3

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("StaffC9 " + currentTimeString)
                .ClickStaffRecordCellText(suspended_systemUserEmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .InsertTextInCommentsTextArea("Schedule4831_" + currentTimeString)
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("StaffC9 " + currentTimeString + " - " + "Role4831" + currentTimeString + " contract is currently suspended, and will not be allocated to this diary booking until this suspension has ended.")
                .ClickDismissButton_DynamicDialogue();

            #endregion

            #region Step 2

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("StaffC9 " + currentTimeString + " - " + "Role4831" + currentTimeString + " contract is currently suspended, and will not be allocated to this diary booking until this suspension has ended.")
                .ClickSaveButton_DynamicDialogue();

            System.Threading.Thread.Sleep(1000);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            #endregion

            #region Step 5

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("StaffE9 " + currentTimeString)
                .ClickStaffRecordCellText(_notStarted_systemUserEmploymentContractId5)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .InsertTextInCommentsTextArea("Schedule4831_" + currentTimeString)
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("StaffE9 " + currentTimeString + " - " + "Role4831" + currentTimeString + " contract has not started yet, and will not be allocated to this diary booking until this contract has started.")
                .ClickDismissButton_DynamicDialogue();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            #endregion

            #region Step 4

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("StaffE9 " + currentTimeString + " - " + "Role4831" + currentTimeString + " contract has not started yet, and will not be allocated to this diary booking until this contract has started.")
                .ClickSaveButton_DynamicDialogue();

            System.Threading.Thread.Sleep(1000);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(3, careProviderBookingSchedules.Count);

            #endregion

            #region Step 7

            var _bookingScheduleStaffId1 = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingSchedule1Id)[0];
            string _employmentContractId1 = dbHelper.cpBookingScheduleStaff.GetById(_bookingScheduleStaffId1, "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString();
            Assert.AreEqual(_employmentContractId1, _systemUserEmploymentContractId2.ToString());

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("StaffF9 " + currentTimeString)
                .ClickStaffRecordCellText(suspended_systemUserEmploymentContractId2)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .RemoveStaffFromSelectedStaffField(_systemUserEmploymentContractId2)
                .InsertTextInCommentsTextArea("Edit_Schedule4831_" + currentTimeString)
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("StaffF9 " + currentTimeString + " - " + "Role4831" + currentTimeString + " contract is currently suspended, and will not be allocated to this diary booking until this suspension has ended.")
                .ClickDismissButton_DynamicDialogue();

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad();

            var _bookingScheduleStaffId2 = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingSchedule1Id)[0];
            string _employmentContractId2 = dbHelper.cpBookingScheduleStaff.GetById(_bookingScheduleStaffId2, "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString();

            Assert.AreEqual(_employmentContractId1, _employmentContractId2);

            #endregion

            #region Step 6

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .InsertTextInCommentsTextArea("Edit_Schedule4831_" + currentTimeString)
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("StaffF9 " + currentTimeString + " - " + "Role4831" + currentTimeString + " contract is currently suspended, and will not be allocated to this diary booking until this suspension has ended.")
                .ClickSaveButton_DynamicDialogue();

            System.Threading.Thread.Sleep(1000);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            _bookingScheduleStaffId2 = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingSchedule1Id)[0];
            _employmentContractId2 = dbHelper.cpBookingScheduleStaff.GetById(_bookingScheduleStaffId2, "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString();

            Assert.AreEqual(_employmentContractId2, suspended_systemUserEmploymentContractId2.ToString());

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(3, careProviderBookingSchedules.Count);

            #endregion

            #region Step 8

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("StaffG9 " + currentTimeString)
                .ClickStaffRecordCellText(_notStarted_systemUserEmploymentContractId6)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .RemoveStaffFromSelectedStaffField(suspended_systemUserEmploymentContractId2)
                .InsertTextInCommentsTextArea("Edit_2_Schedule4831_" + currentTimeString)
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("StaffG9 " + currentTimeString + " - " + "Role4831" + currentTimeString + " contract has not started yet, and will not be allocated to this diary booking until this contract has started.")
                .ClickSaveButton_DynamicDialogue();

            System.Threading.Thread.Sleep(1000);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            _bookingScheduleStaffId2 = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingSchedule1Id)[0];
            _employmentContractId2 = dbHelper.cpBookingScheduleStaff.GetById(_bookingScheduleStaffId2, "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString();

            Assert.AreEqual(_employmentContractId2, _notStarted_systemUserEmploymentContractId6.ToString());

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(3, careProviderBookingSchedules.Count);

            #endregion


        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5584

        [TestProperty("JiraIssueID", "ACC-5575")]
        [Description("Step(s) 1 to 2 from the original test ACC-4825")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_UITestMethod012()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role4825" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "P4825 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-4825", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "CarerA1" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "StaffA9", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId1, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            int startDay = (int)todayDate.DayOfWeek;

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, startDay, startDay, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), _providerId, "Provider Schedule Booking Testing " + currentTimeString);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId1, _systemUserId1);


            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
            {
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);
            }

            DateTime LastOccurrenceDate = todayDate;

            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 1, null, null, LastOccurrenceDate, 1);

            #endregion

            #region Express Book for Provider

            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId, commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(13), commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId, "provider", "P5575 " + currentTimeString);

            #endregion

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Care Provider Booking Schedules")
                .ClickDeleteButton()
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(2)
                .ClickColumnHeader(2)
                .OpenRecord(cpBookingSchedule1Id.ToString());

            careProviderBookingScheduleRecordPage
                .WaitForCareProviderBookingScheduleRecordPageToLoadFromAdvancedSearch()
                .ValidateIsdeleted_NoOptionChecked()
                .ValidateIsdeleted_YesOptionNotChecked();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + currentTimeString)
                .ValidateRecordPresent(_expressBookingCriteriaId1, true)
                .OpenRecord(_expressBookingCriteriaId1);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ValidateStatusSelectedText("Pending")
                .ClickResultsTab();

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .ValidateNoRecordMessageVisible(true);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickDetailsTab()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id)
                .ClickCloseButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickBackButton();

            #region Scheduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + currentTimeString)
                .ValidateRecordPresent(_expressBookingCriteriaId1, true)
                .OpenRecord(_expressBookingCriteriaId1);

            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(_expressBookingCriteriaId1);
            Assert.AreEqual(1, expressBookingResultId.Count);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ValidateStatusSelectedText("Succeeded")
                .ClickResultsTab();

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .ValidateRecordPresent(expressBookingResultId[0], true);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickDetailsTab()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad(false)
                .ValidateRecordNotPresent(cpBookingSchedule1Id)
                .ValidateNoRecordMessageVisible(true)
                .ClickCloseButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Care Provider Booking Schedules")
                .ClickDeleteButton()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(2)
                .ClickColumnHeader(2)
                .OpenRecord(cpBookingSchedule1Id.ToString());

            careProviderBookingScheduleRecordPage
                .WaitForCareProviderBookingScheduleRecordPageToLoadFromAdvancedSearch()
                .ValidateIsdeleted_YesOptionChecked()
                .ValidateIsdeleted_NoOptionNotChecked();

            var cpBookingSchedule2Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, startDay, startDay, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), _providerId, "Provider Schedule Booking Testing " + currentTimeString);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule2Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule2Id, _systemUserEmploymentContractId1, _systemUserId1);

            _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
            {
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);
            }

            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule2Id, 1, null, null, LastOccurrenceDate, 1);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), false)
                .ValidateScheduleBookingIsPresent(cpBookingSchedule2Id.ToString(), true);

            #region Express Book for Provider

            var _expressBookingCriteriaId2 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId, commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(13), commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId, "provider", "P5575 " + currentTimeString);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickHeaderCellLink(9);

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickHeaderCellLink(9);

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ValidateRecordPresent(_expressBookingCriteriaId2, true)
                .OpenRecord(_expressBookingCriteriaId2);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickDetailsTab()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordNotPresent(cpBookingSchedule1Id)
                .ValidateRecordPresent(cpBookingSchedule2Id)
                .ClickCloseButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5576")]
        [Description("Step(s) 3 from the original test ACC-4825")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_UITestMethod013()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role4825" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "P4825 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-4825", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "CarerA1" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "CarerA1", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId1, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            int startDay = (int)todayDate.DayOfWeek;

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, startDay, startDay, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), _providerId, "Provider Schedule Booking Testing " + currentTimeString);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId1, _systemUserId1);


            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
            {
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);
            }

            DateTime LastOccurrenceDate = todayDate;

            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 1, null, null, LastOccurrenceDate, 1);

            #endregion

            #region Express Book for Provider

            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId, commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(13), commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId, "provider", "P5575 " + currentTimeString);

            #endregion

            #region Scheduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 3

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Care Provider Booking Schedules")
                .ClickDeleteButton()
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(2)
                .ClickColumnHeader(2)
                .OpenRecord(cpBookingSchedule1Id.ToString());

            careProviderBookingScheduleRecordPage
                .WaitForCareProviderBookingScheduleRecordPageToLoadFromAdvancedSearch()
                .ValidateIsdeleted_YesOptionChecked()
                .ValidateIsdeleted_NoOptionNotChecked();

            careProviderBookingScheduleRecordPage
                .NavigateToRelatedItemsSubPage_Audit();

            var auditSearch1 = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 2, //update operation
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = cpBookingSchedule1Id.ToString(),
                ParentTypeName = "cpbookingschedule",
                RecordsPerPage = "50",
                ViewType = "0",
                AllowMultiSelect = "false",
                ViewGroup = "1",
                Year = "Last 90 Days",
                IsGeneralAuditSearch = false,
                UsePaging = true,
                PageNumber = 1
            };

            WebAPIHelper.Security.Authenticate();
            var auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch1, WebAPIHelper.Security.AuthenticationCookie);
            Assert.AreEqual("Updated", auditResponseData.GridData[0].cols[0].Text);
            Assert.AreEqual("Service Account", auditResponseData.GridData[0].cols[1].Text);

            var auditRecordId1 = auditResponseData.GridData[0].Id;

            auditListPage
                .WaitForAuditListPageToLoadFromReferenceDataPage("cpbookingschedule", "iframe_CWDataFormDialog")
                .ValidateCellText(1, 2, "Updated");

            auditListPage
                .WaitForAuditListPageToLoadFromReferenceDataPage("cpbookingschedule", "iframe_CWDataFormDialog")
                .ValidateRecordPresent(auditRecordId1)
                .ClickOnAuditRecord(auditRecordId1);

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Updated")
                .ValidateChangedBy("Service Account")
                .ValidateFieldNameCellText(2, "Deleted")
                .ValidateOldValueCellText(2, "No")
                .ValidateNewValueCellText(2, "Yes")
                .TapCloseButton();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5627

        [TestProperty("JiraIssueID", "ACC-5635")]
        [Description("Automation for step 2 from the original test ACC-4824 - Part 1 - Frequency is every 2 weeks")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_UITestMethod014()
        {

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role5635" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "P5635 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5635", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "CarerA2" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "CarerA2", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId1, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var startDate2 = commonMethodsHelper.GetWeekFirstMonday(todayDate).AddDays(6);

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, 7, 1, new TimeSpan(6, 0, 0), new TimeSpan(6, 0, 0), _providerId, "Provider Schedule Booking 5635 " + currentTimeString);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId1, _systemUserId1);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
            {
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);
            }

            DateTime NextOccurrenceDate = startDate2;
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 2, NextOccurrenceDate, null, null, 1);

            #endregion

            #region Express Book for Provider

            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId, commonMethodsHelper.GetThisWeekFirstMonday(), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(6), commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId, "provider", "P5635 " + currentTimeString);

            #endregion

            #region Scheduled job for Express Book

            System.Threading.Thread.Sleep(4000);

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            #region Step 2 - Frequency is every 2 weeks

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + currentTimeString)
                .ValidateRecordPresent(_expressBookingCriteriaId1, true)
                .OpenRecord(_expressBookingCriteriaId1);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .ValidateNoRecordMessageVisible(true);

            var expressBookingCriteriaId = dbHelper.cpExpressBookingCriteria.GetByRegardingID(_providerId).First();
            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(0, expressBookingResultId.Count);

            var cpDiaryBookingId = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            Assert.AreEqual(1, cpDiaryBookingId.Count);

            var cpDiaryBookingId1 = cpDiaryBookingId[0];

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(startDate2.ToString("yyyy"), startDate2.ToString("MMMM"), startDate2.Day.ToString());

            System.Threading.Thread.Sleep(5000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(cpDiaryBookingId1.ToString(), true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .ValidateBookingTakesPlaceEveryDropDownText("2 weeks")
                .ValidateNextDueToTakePlaceDate(startDate2.AddDays(14).ToString("dd'/'MM'/'yyyy"));

            #endregion

        }


        [TestProperty("JiraIssueID", "ACC-5637")]
        [Description("Automation for step 2 from the original test ACC-4824 - Part 1 - Frequency is every 3 weeks")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_UITestMethod015()
        {

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role5637" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "P5637 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5637", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "CarerA3" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "CarerA3", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId1, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var startDate2 = commonMethodsHelper.GetWeekFirstMonday(todayDate).AddDays(7);

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, 1, 2, new TimeSpan(6, 0, 0), new TimeSpan(6, 0, 0), _providerId, "Provider Schedule Booking 5635 " + currentTimeString);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId1, _systemUserId1);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
            {
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);
            }

            DateTime NextOccurrenceDate = startDate2;
            DateTime FirstOccurrenceDate = commonMethodsHelper.GetWeekFirstMonday(todayDate);
            DateTime LastOccurrenceDate = startDate2.AddDays(49);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 3, NextOccurrenceDate, FirstOccurrenceDate, LastOccurrenceDate, 1);

            #endregion

            #region Express Book for Provider

            var expressBookingCriteriaId = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId, commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(13), commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId, "provider", "P5637 " + currentTimeString);

            #endregion

            #region Scheduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            #region Step 2 - Freqeuncy is every 3 weeks

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(0, expressBookingResultId.Count);

            var cpDiaryBookingId = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            Assert.AreEqual(1, cpDiaryBookingId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .ValidateBookingTakesPlaceEveryDropDownText("3 weeks")
                .ValidateNextDueToTakePlaceDate(startDate2.AddDays(21).ToString("dd'/'MM'/'yyyy"));

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5640")]
        [Description("Automation for step 2 from the original test ACC-4824 - Part 1 - Frequency is every 4 weeks")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_UITestMethod016()
        {

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role5640" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "P5640 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5640", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "CarerA4" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "CarerA4", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId1, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var startDate2 = commonMethodsHelper.GetWeekFirstMonday(todayDate).AddDays(1);

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, 2, 3, new TimeSpan(6, 0, 0), new TimeSpan(6, 0, 0), _providerId, "Provider Schedule Booking 5635 " + currentTimeString);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId1, _systemUserId1);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
            {
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);
            }

            DateTime NextOccurrenceDate = startDate2.AddDays(14);
            DateTime FirstOccurrenceDate = startDate2.AddDays(7);
            DateTime LastOccurrenceDate = startDate2.AddDays(56);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 4, NextOccurrenceDate, FirstOccurrenceDate, LastOccurrenceDate, 1);

            #endregion

            #region Express Book for Provider

            var expressBookingCriteriaId = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId, commonMethodsHelper.GetThisWeekFirstMonday(), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(6), commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId, "provider", "P5637 " + currentTimeString);

            #endregion

            #region Scheduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            #region Step 2 - Freqeuncy is every 4 weeks

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResultId.Count);

            var cpDiaryBookingId = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            Assert.AreEqual(0, cpDiaryBookingId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .ValidateBookingTakesPlaceEveryDropDownText("4 weeks")
                .ValidateNextDueToTakePlaceDate(NextOccurrenceDate.ToString("dd'/'MM'/'yyyy"));

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5652")]
        [Description("Automation for step 2 from the original test ACC-4824 - Part 1 - Frequency is every 5 weeks")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_UITestMethod017()
        {

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role5652" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "P5652 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5652", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "CarerA5" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "CarerA5", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId1, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 5, 3, 4, new TimeSpan(22, 0, 0), new TimeSpan(6, 0, 0), _providerId, "Provider Schedule Booking 5635 " + currentTimeString);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId1, _systemUserId1);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
            {
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);
            }

            DateTime FirstOccurrenceDate = commonMethodsHelper.GetDayOfWeek(todayDate, DayOfWeek.Wednesday).AddDays(7);
            DateTime NextOccurrenceDate = FirstOccurrenceDate.AddDays(35);
            DateTime LastOccurrenceDate = FirstOccurrenceDate.AddDays(105);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 5, NextOccurrenceDate, FirstOccurrenceDate, LastOccurrenceDate, 1);

            #endregion

            #region Express Book for Provider

            var expressBookingCriteriaId = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId, commonMethodsHelper.GetWeekFirstMonday(FirstOccurrenceDate.AddDays(35)), commonMethodsHelper.GetWeekFirstMonday(FirstOccurrenceDate.AddDays(41)), commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId, "provider", "P5637 " + currentTimeString);

            #endregion

            #region Scheduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            #region Step 2 - Freqeuncy is every 5 weeks

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(0, expressBookingResultId.Count);

            var cpDiaryBookingId = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            Assert.AreEqual(1, cpDiaryBookingId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .ValidateBookingTakesPlaceEveryDropDownText("5 weeks")
                .ValidateNextDueToTakePlaceDate(FirstOccurrenceDate.AddDays(70).ToString("dd'/'MM'/'yyyy"));

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5658")]
        [Description("Automation for step 3 from the original test ACC-4824 - Part 1 - Freqeuncy is every 2 weeks, every 3 weeks, every 5 weeks")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_UITestMethod018()
        {

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role5658" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName1 = "P5658 " + currentTimeString;
            var _providerId1 = commonMethodsDB.CreateProvider(_providerName1, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5658", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId1, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "CarerA6" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "CarerA6", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId1, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);

            #endregion

            #region Booking Schedule - frequency = 2 - first/last occurrence - empty, next due to < express book start date

            var startDate1 = commonMethodsHelper.GetCurrentDateWithoutCulture();
            int startDay1 = (int)startDate1.DayOfWeek;

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, startDay1, startDay1, new TimeSpan(6, 0, 0), new TimeSpan(14, 0, 0), _providerId1, "Provider 1 Schedule Booking 5658 " + currentTimeString);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId1, _systemUserId1);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
            {
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);
            }

            DateTime NextOccurrenceDate1 = startDate1;
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 2, NextOccurrenceDate1, null, null, 1);

            #endregion

            #region Booking Schedule - frequency = 3 - first occurrence < express book start date, last occurrence > express book start date , next due to < express book start date

            var startDate2 = commonMethodsHelper.GetCurrentDateWithoutCulture();
            int startDay2 = (int)startDate1.DayOfWeek;

            var cpBookingSchedule2Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, startDay2, startDay2, new TimeSpan(16, 0, 0), new TimeSpan(20, 0, 0), _providerId1, "Provider 2 Schedule Booking 5658 " + currentTimeString);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule2Id, _systemUserEmploymentContractId1, _systemUserId1);

            _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule2Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
            {
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);
            }

            DateTime NextOccurrenceDate2 = startDate2.AddDays(7); //12-12

            DateTime FirstOccurrenceDate2 = startDate2; //5-12
            DateTime LastOccurrenceDate2 = startDate2.AddDays(49);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule2Id, 3, NextOccurrenceDate2, FirstOccurrenceDate2, LastOccurrenceDate2, 1);


            #endregion

            #region Booking Schedule - frequency = 4 - first occurrence > express book start date, last occurrence > express book end date, next due to < express book start date
            //Scenario not applicable, next due date cannot be older than the express book start date when first occurrence date is greater than express book start date

            #endregion

            #region Booking Schedule - frequency = 5 - first occurrence < express book start date, last occurrence > express book end date, next due to < express book start date

            var cpBookingSchedule3Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, startDay2, startDay2, new TimeSpan(1, 0, 0), new TimeSpan(5, 0, 0), _providerId1, "Provider 3 Schedule Booking 5658 " + currentTimeString);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule3Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule3Id, _systemUserEmploymentContractId1, _systemUserId1);

            _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule3Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
            {
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);
            }

            DateTime NextOccurrenceDate3 = startDate2.AddDays(14); //19-12

            DateTime FirstOccurrenceDate3 = startDate2.AddDays(7); //12-12
            DateTime LastOccurrenceDate3 = startDate2.AddDays(56);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule3Id, 5, NextOccurrenceDate3, FirstOccurrenceDate3, LastOccurrenceDate3, 1);


            #endregion

            #region Express Book for Provider

            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId1, commonMethodsHelper.GetThisWeekFirstMonday().AddDays(14), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(20), commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId1, "provider", "P5635 " + currentTimeString);
            var _expressBookingCriteriaId2 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId1, commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(13), commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId1, "provider", "P5635_2 " + currentTimeString);
            var _expressBookingCriteriaId3 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId1, commonMethodsHelper.GetThisWeekFirstMonday().AddDays(14), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(20), commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId1, "provider", "P5635_3 " + currentTimeString);

            #endregion

            #region Scheduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            #region Step 2 - Freqeuncy is every 2 weeks, every 3 weeks, every 5 weeks

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName1 + " - No Address")
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .ValidateBookingTakesPlaceEveryDropDownText("2 weeks")
                .ValidateNextDueToTakePlaceDate(NextOccurrenceDate1.AddDays(28).ToString("dd'/'MM'/'yyyy"))
                .ClickOnCloseButton()
                .WaitForEditScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickScheduleBooking(cpBookingSchedule2Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .ValidateBookingTakesPlaceEveryDropDownText("3 weeks")
                .ValidateNextDueToTakePlaceDate(NextOccurrenceDate2.AddDays(21).ToString("dd'/'MM'/'yyyy"))
                .ClickOnCloseButton()
                .WaitForEditScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickScheduleBooking(cpBookingSchedule3Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .ValidateBookingTakesPlaceEveryDropDownText("5 weeks")
                .ValidateNextDueToTakePlaceDate(NextOccurrenceDate3.AddDays(35).ToString("dd'/'MM'/'yyyy"));

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5663

        [TestProperty("JiraIssueID", "ACC-5679")]
        [Description("Automation for step 4 from the original test ACC-4824 - Part 2 - Freqeuncy is every 2 weeks")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_UITestMethod019()
        {

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role5679" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName1 = "P5679 " + currentTimeString;
            var _providerId1 = commonMethodsDB.CreateProvider(_providerName1, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5679", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId1, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "CarerA7" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "CarerA7", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId1, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);

            #endregion

            #region Booking Schedule - frequency = 2, first/last occurrence - empty, next due to - current week , start day - Monday

            var startDate1 = commonMethodsHelper.GetWeekFirstMonday(todayDate);
            int startDay1 = (int)startDate1.DayOfWeek;

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, startDay1, startDay1, new TimeSpan(6, 0, 0), new TimeSpan(14, 0, 0), _providerId1, "Provider 1 Schedule Booking 5679 " + currentTimeString);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId1, _systemUserId1);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
            {
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);
            }

            DateTime NextOccurrenceDate1 = startDate1.AddDays(7);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 2, NextOccurrenceDate1, null, null, 1);

            #endregion

            #region Express Book for Provider

            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId1, commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(13), commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId1, "provider", "P5635 " + currentTimeString);

            #endregion

            #region Scheduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            #region Step 4 - Freqeuncy is every 2 weeks

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName1 + " - No Address")
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .ValidateBookingTakesPlaceEveryDropDownText("2 weeks")
                .ValidateNextDueToTakePlaceDate(NextOccurrenceDate1.AddDays(14).ToString("dd'/'MM'/'yyyy"))
                .ClickOnCloseButton()
                .WaitForEditScheduleBookingPopupClosed();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-5681")]
        [Description("Automation for step 4 from the original test ACC-4824 - Part 2 - Freqeuncy is every 3 weeks")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_UITestMethod020()
        {

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role5681" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName1 = "P5681 " + currentTimeString;
            var _providerId1 = commonMethodsDB.CreateProvider(_providerName1, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5681", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId1, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "CarerA8" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "CarerA8", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId1, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);

            #endregion

            #region Booking Schedule - frequency = 3, first occurrence < express book start date, last occurrence > express book start date , next due to - current week , start day - Tuesday

            var startDate1 = commonMethodsHelper.GetDateWithoutCulture(todayDate);
            int startDay1 = (int)startDate1.DayOfWeek;

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, startDay1, startDay1, new TimeSpan(6, 0, 0), new TimeSpan(14, 0, 0), _providerId1, "Provider 1 Schedule Booking 5681 " + currentTimeString);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId1, _systemUserId1);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
            {
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);
            }

            DateTime FirstOccurrenceDate1 = startDate1.AddDays(-21);
            DateTime NextOccurrenceDate1 = startDate1;
            DateTime LastOccurrenceDate1 = startDate1.AddDays(42);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 3, NextOccurrenceDate1, FirstOccurrenceDate1, LastOccurrenceDate1, 1);

            #endregion

            #region Express Book for Provider

            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId1, commonMethodsHelper.GetThisWeekFirstMonday(), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(6), commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId1, "provider", "P5635 " + currentTimeString);

            #endregion

            #region Scheduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            #region Step 4 - Freqeuncy is every 3 weeks

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName1 + " - No Address")
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .ValidateBookingTakesPlaceEveryDropDownText("3 weeks")
                .ValidateNextDueToTakePlaceDate(NextOccurrenceDate1.AddDays(21).ToString("dd'/'MM'/'yyyy"))
                .ClickOnCloseButton()
                .WaitForEditScheduleBookingPopupClosed();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-5684")]
        [Description("Automation for step 4 from the original test ACC-4824 - Part 2 - Freqeuncy is every 4 weeks")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_UITestMethod021()
        {

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role5684" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName1 = "P5684 " + currentTimeString;
            var _providerId1 = commonMethodsDB.CreateProvider(_providerName1, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5684", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId1, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "CarerA9" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "CarerA9", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId1, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);

            #endregion

            #region Booking Schedule - frequency = 4, first occurrence > express book start date, last occurrence > express book end date, next due to - current week , start day - Wednesday

            var startDate1 = commonMethodsHelper.GetDayOfWeek(todayDate.AddDays(7), DayOfWeek.Wednesday);
            int startDay1 = (int)startDate1.DayOfWeek;

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, startDay1, startDay1, new TimeSpan(6, 0, 0), new TimeSpan(14, 0, 0), _providerId1, "Provider 1 Schedule Booking 5679 " + currentTimeString);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId1, _systemUserId1);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
            {
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);
            }

            DateTime FirstOccurrenceDate1 = startDate1.AddDays(-28);
            DateTime NextOccurrenceDate1 = startDate1;
            DateTime LastOccurrenceDate1 = startDate1.AddDays(28);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 4, NextOccurrenceDate1, FirstOccurrenceDate1, LastOccurrenceDate1, 1);

            #endregion

            #region Express Book for Provider

            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId1, commonMethodsHelper.GetWeekFirstMonday(startDate1.AddDays(-56)), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-50), commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId1, "provider", "P5684 " + currentTimeString);

            #endregion

            #region Scheduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            #region Step 4 - Freqeuncy is every 4 weeks

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName1 + " - No Address")
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .ValidateBookingTakesPlaceEveryDropDownText("4 weeks")
                .ValidateNextDueToTakePlaceDate(NextOccurrenceDate1.ToString("dd'/'MM'/'yyyy"))
                .ClickOnCloseButton()
                .WaitForEditScheduleBookingPopupClosed();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5687")]
        [Description("Automation for step 4 from the original test ACC-4824 - Part 2 - Freqeuncy is every 5 weeks")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_UITestMethod022()
        {

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role5687" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName1 = "P5687 " + currentTimeString;
            var _providerId1 = commonMethodsDB.CreateProvider(_providerName1, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5687", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId1, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "CarerA10" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "CarerA10", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId1, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);

            #endregion

            #region Booking Schedule - frequency = 3, first occurrence < express book start date, last occurrence > express book start date , next due to - current week , start day - Tuesday

            var startDate1 = commonMethodsHelper.GetDayOfWeek(todayDate.AddDays(7), DayOfWeek.Thursday);
            int startDay1 = (int)startDate1.DayOfWeek;

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, startDay1, startDay1, new TimeSpan(6, 0, 0), new TimeSpan(14, 0, 0), _providerId1, "Provider 1 Schedule Booking 5687 " + currentTimeString);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId1, _systemUserId1);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
            {
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);
            }

            DateTime FirstOccurrenceDate1 = startDate1.AddDays(-35);
            DateTime NextOccurrenceDate1 = startDate1;
            DateTime LastOccurrenceDate1 = startDate1.AddDays(35);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 5, NextOccurrenceDate1, FirstOccurrenceDate1, LastOccurrenceDate1, 1);

            #endregion

            #region Express Book for Provider

            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId1, commonMethodsHelper.GetThisWeekFirstMonday(), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(6), commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId1, "provider", "P5635 " + currentTimeString);

            #endregion

            #region Scheduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            #region Step 4 - Freqeuncy is every 5 weeks

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName1 + " - No Address")
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .ValidateBookingTakesPlaceEveryDropDownText("5 weeks")
                .ValidateNextDueToTakePlaceDate(NextOccurrenceDate1.ToString("dd'/'MM'/'yyyy"))
                .ClickOnCloseButton()
                .WaitForEditScheduleBookingPopupClosed();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-5690")]
        [Description("Automation for step 5 from the original test ACC-4824 - Part 2 - Freqeuncy is every 2 weeks")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        [TestProperty("Screen2", "Advanced Search")]
        public void ProviderScheduleBooking_UITestMethod023()
        {

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role5690" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName1 = "P5690 " + currentTimeString;
            var _providerId1 = commonMethodsDB.CreateProvider(_providerName1, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5690", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId1, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "CarerB" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "CarerB", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId1, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);

            #endregion

            #region Booking Schedule - frequency = 2, next due to - current week friday, first occurrence - last week friday, last occurrence - next week friday

            var startDate1 = commonMethodsHelper.GetDayOfWeek(todayDate, DayOfWeek.Friday);
            int startDay1 = (int)startDate1.DayOfWeek;

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, startDay1, startDay1, new TimeSpan(6, 0, 0), new TimeSpan(14, 0, 0), _providerId1, "Provider 1 Schedule Booking 5690 " + currentTimeString);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId1, _systemUserId1);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
            {
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);
            }

            DateTime NextOccurrenceDate1 = startDate1;
            DateTime FirstOccurrenceDate1 = startDate1.AddDays(-7);
            DateTime LastOccurrenceDate1 = startDate1.AddDays(7);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 2, NextOccurrenceDate1, FirstOccurrenceDate1, LastOccurrenceDate1, 1);

            #endregion

            #region Express Book for Provider

            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId1, commonMethodsHelper.GetThisWeekFirstMonday(), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(6), commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId1, "provider", "P5635 " + currentTimeString);

            #endregion

            #region Step 5 - Freqeuncy is every 2 weeks

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Care Provider Booking Schedules")
                .ClickDeleteButton()
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(2)
                .ClickColumnHeader(2)
                .OpenRecord(cpBookingSchedule1Id.ToString());

            careProviderBookingScheduleRecordPage
                .WaitForCareProviderBookingScheduleRecordPageToLoadFromAdvancedSearch()
                .ValidateIsdeleted_NoOptionChecked()
                .ValidateIsdeleted_YesOptionNotChecked();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + currentTimeString)
                .OpenRecord(_expressBookingCriteriaId1);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickDetailsTab()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id)
                .ClickCloseButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickBackButton();

            #region Scheduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + currentTimeString)
                .OpenRecord(_expressBookingCriteriaId1);

            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(_expressBookingCriteriaId1);
            Assert.AreEqual(0, expressBookingResultId.Count);

            var cpDiaryBookingId = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            Assert.AreEqual(1, cpDiaryBookingId.Count);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ValidateStatusSelectedText("Succeeded")
                .ClickResultsTab();

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .ValidateNoRecordMessageVisible(true);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Care Provider Booking Schedules")
                .ClickDeleteButton()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(2)
                .ClickColumnHeader(2)
                .OpenRecord(cpBookingSchedule1Id.ToString());

            careProviderBookingScheduleRecordPage
                .WaitForCareProviderBookingScheduleRecordPageToLoadFromAdvancedSearch()
                .ValidateNextoccurrencedateText(NextOccurrenceDate1.AddDays(14).ToString("dd'/'MM'/'yyyy"));

            careProviderBookingScheduleRecordPage
                .ValidateIsdeleted_YesOptionChecked()
                .ValidateIsdeleted_NoOptionNotChecked();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5755

        [TestProperty("JiraIssueID", "ACC-5756")]
        [Description("Automation for steps 1 to 10, 12, 13 from the original test ACC-4797.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Option Sets")]
        [TestProperty("Screen2", "Advanced Search")]
        [TestProperty("Screen3", "Express Booking Criteria")]
        public void ProviderScheduleBooking_UITestMethod024()
        {

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Public Holiday

            var BankHolidayYear = todayDate.AddYears(1).Year;
            commonMethodsDB.CreateBankHoliday("Christmas " + BankHolidayYear.ToString("yyyy"), new DateTime(BankHolidayYear, 12, 25), "Christmas Day " + BankHolidayYear.ToString("yyyy"));

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role5690" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName1 = "P5756 " + currentTimeString;
            var _providerId1 = commonMethodsDB.CreateProvider(_providerName1, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5756", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId1, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "CarerC1" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "CarerC1", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId1, 1); //1 = Male

            string _staffBName = "CarerC2" + currentTimeString;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_staffBName, "CarerC2", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId1, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId2, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId2, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId2, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var startDate1 = new DateTime(BankHolidayYear, 12, 25);
            int startDay1 = (int)startDate1.DayOfWeek;

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, startDay1, startDay1, new TimeSpan(6, 0, 0), new TimeSpan(14, 0, 0), _providerId1, "Provider 1 Schedule Booking 5756 " + currentTimeString);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId1, _systemUserId1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId2, _systemUserId2);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
            {
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);
            }

            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 1, null, null, null, 2);

            #endregion

            #region Express Book for Provider

            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId1, commonMethodsHelper.GetWeekFirstMonday(new DateTime(BankHolidayYear, 12, 25)), commonMethodsHelper.GetWeekFirstMonday(new DateTime(BankHolidayYear, 12, 25)).AddDays(6), commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId1, "provider", "P5756 " + currentTimeString);

            #endregion

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 6

            //Steps 6 will be ignored as the Options Sets entry is no longer available in QA / Automation tenants. Only tenants where customizations are active will have access to Option Sets

            //#region Option Set

            //var optionSetName = "ExpressBookingFailureReason";
            //var optionSetId = dbHelper.optionSet.GetOptionSetIdByName(optionSetName)[0];

            //#endregion

            //mainMenu
            //    .WaitForMainMenuToLoad()
            //    .NavigateToCustomizationsSection();

            //customizationsPage
            //    .WaitForCustomizationsPageToLoad()
            //    .ClickOptionSetsButton();

            //optionSetsPage
            //    .WaitForOptionSetsPageToLoad()
            //    .InsertQuickSearchText(optionSetName)
            //    .ClickQuickSearchButton()
            //    .ValidateRecordIsPresent(optionSetId.ToString(), true);

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            #endregion

            #region Step 3

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Express Booking Results")
                .ClickSelectFilterFieldOption("1")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Date of Booking")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Start Time")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Finish Time")
                //.ValidateSelectFilterFieldOptionIsPresent("1", "Staff Name")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Provider Name")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Exception Message")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Booking Schedule")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Express Booking Criteria")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Booking Diary")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Express Booking Exception Reason")
                .ClickSelectFilterFieldOption("1");

            advanceSearchPage
                .SelectFilter("1", "Date of Booking")
                .SelectFilter("1", "Start Time")
                .SelectFilter("1", "Finish Time")
                //.SelectFieldOption("1", "Staff Name")
                .SelectFilter("1", "Provider Name")
                .SelectFilter("1", "Exception Message")
                .SelectFilter("1", "Booking Schedule")
                .SelectFilter("1", "Express Booking Criteria")
                .SelectFilter("1", "Booking Diary")
                .SelectFilter("1", "Express Booking Exception Reason");

            #endregion

            #region Step 4

            advanceSearchPage
                .SelectFilterInsideOptGroup("1", "Provider Name")
                .SelectOperator("1", "Contains Data")
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ClickNewRecordButton_ResultsPage();

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoadFromAdvancedSearch()
                .ValidateMandatoryFieldIsDisplayed("Express Booking Exception Reason")
                .ValidateMandatoryFieldIsDisplayed("Date of Booking")
                .ValidateMandatoryFieldIsDisplayed("Start Time")
                .ValidateMandatoryFieldIsDisplayed("Finish Time")
                .ValidateMandatoryFieldIsDisplayed("Schedule Booking")
                .ValidateMandatoryFieldIsDisplayed("Express Booking Criteria")
                .ValidateMandatoryFieldIsDisplayed("Exception Message");

            #endregion

            #region Step 5

            expressBookingResultRecordPage
                .ValidateMandatoryFieldIsDisplayed("Staff Names", false)
                .ValidateMandatoryFieldIsDisplayed("Provider Name", false)
                .ValidateMandatoryFieldIsDisplayed("Diary Booking", false);

            #endregion

            #region Step 7

            expressBookingResultRecordPage
                .ValidateDateOfBookingDisabled(true)
                .ValidateStartTimeDisabled(true)
                .ValidateFinishTimeDisabled(true)
                .ValidateStaffNamesDisabled(true)
                .ValidateProviderNameDisabled(true)
                .ValidateExpressBookingExceptionReasonDisabled(true);

            #endregion

            #region Step 8

            #region Scheduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            var expressBookingCriteriaId = dbHelper.cpExpressBookingCriteria.GetByRegardingID(_providerId1).First();
            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResultId.Count);

            expressBookingResultRecordPage
                .ClickBackButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ClickBackButton_ResultsPage();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectFilterInsideOptGroup("1", "Express Booking Result")
                .SelectOperator("1", "Contains Data")
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(13)
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(13)
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(expressBookingResultId[0].ToString())
                .OpenRecord(expressBookingResultId[0].ToString());

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoadFromAdvancedSearch()
                .ValidateStaffNamesContainsText("CarerC2 " + currentTimeString)
                .ValidateStaffNamesContainsText("CarerC1 " + currentTimeString);

            #endregion

            #region Step 9, Step 10

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoadFromAdvancedSearch()
                .ValidateProviderNameText(_providerName1)
                .ValidateExceptionMessageText("Booking not required due to public holiday.")
                .ValidateExpressBookingFailureReasonSelectedText("Booking not required");
            #endregion

            #region Step 13

            expressBookingResultRecordPage
                .ValidateBookingScheduleLookupButtonVisible(true)
                .ValidateBookingDiaryLookupButtonVisible(true);

            #endregion

            #region Step 12

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + currentTimeString)
                .OpenRecord(expressBookingCriteriaId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5776

        [TestProperty("JiraIssueID", "ACC-5780")]
        [Description("Automation for step 11 from the original test ACC-4797 - when Diary Booking is successful.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Advanced Search")]
        public void ProviderScheduleBooking_UITestMethod025()
        {

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Public Holiday

            var BankHolidayYear = todayDate.AddYears(1).Year;
            commonMethodsDB.CreateBankHoliday("Christmas " + BankHolidayYear.ToString("yyyy"), new DateTime(BankHolidayYear, 12, 25), "Christmas Day " + BankHolidayYear.ToString("yyyy"));

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role5780" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName1 = "P5780 " + currentTimeString;
            var _providerId1 = commonMethodsDB.CreateProvider(_providerName1, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5780", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId1, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "CarerD1" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "CarerD1", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId1, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, null, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var startDate1 = new DateTime(BankHolidayYear, 12, 25);
            int startDay1 = (int)startDate1.DayOfWeek;

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, startDay1, startDay1, new TimeSpan(6, 0, 0), new TimeSpan(14, 0, 0), _providerId1, "Provider 1 Schedule Booking 5756 " + currentTimeString);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId1, _systemUserId1);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
            {
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);
            }

            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 1, null, null, null, 1);

            #endregion

            #region Express Book for Provider

            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId1, commonMethodsHelper.GetWeekFirstMonday(new DateTime(BankHolidayYear, 12, 25)), commonMethodsHelper.GetWeekFirstMonday(new DateTime(BankHolidayYear, 12, 25)).AddDays(6), commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId1, "provider", "P5780 " + currentTimeString);

            #endregion

            #region Step 11

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            #region Scheduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(_expressBookingCriteriaId1);
            Assert.AreEqual(1, expressBookingResultId.Count);

            var cpDiaryBookingId = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            Assert.AreEqual(1, cpDiaryBookingId.Count);

            var cpDiaryBookingId1 = cpDiaryBookingId[0];
            string cpDiaryBookingId1_Title = (string)dbHelper.cPBookingDiary.GetCPBookingDiaryByID(cpDiaryBookingId1, "title")["title"];

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Express Booking Results")
                .SelectFilterInsideOptGroup("1", "Express Booking Result")
                .SelectOperator("1", "Contains Data")
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(13)
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(13)
                .OpenRecord(expressBookingResultId[0].ToString());

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoadFromAdvancedSearch()
                .ValidateStaffNamesText("CarerD1 " + currentTimeString)
                .ValidateProviderNameText(_providerName1)
                .ValidateExceptionMessageText("CarerD1 " + currentTimeString + " - Role5780" + currentTimeString + " contract is not started at the selected booking time. This staff member has been deallocated.")
                .ValidateExpressBookingFailureReasonSelectedText("Invalid staff contract")
                .ValidateBookingDiaryLookupButtonVisible(true)
                .ValidateBookingDiaryLinkText(cpDiaryBookingId1_Title);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5786")]
        [Description("Automation for step 11 from the original test ACC-4797 - when Diary Booking is unsuccessful.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Advanced Search")]
        public void ProviderScheduleBooking_UITestMethod026()
        {

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Public Holiday

            var BankHolidayYear = todayDate.AddYears(1).Year;
            commonMethodsDB.CreateBankHoliday("Christmas " + BankHolidayYear.ToString("yyyy"), new DateTime(BankHolidayYear, 12, 25), "Christmas Day " + BankHolidayYear.ToString("yyyy"));

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role5786" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName1 = "P5786 " + currentTimeString;
            var _providerId1 = commonMethodsDB.CreateProvider(_providerName1, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5786", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId1, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "CarerD2" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "CarerD2", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId1, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var startDate1 = new DateTime(BankHolidayYear, 12, 25);
            int startDay1 = (int)startDate1.DayOfWeek;

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, startDay1, startDay1, new TimeSpan(6, 0, 0), new TimeSpan(14, 0, 0), _providerId1, "Provider 1 Schedule Booking 5756 " + currentTimeString);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId1, _systemUserId1);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
            {
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);
            }

            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 1, null, null, null, 2);

            #endregion

            #region Express Book for Provider

            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId1, commonMethodsHelper.GetWeekFirstMonday(new DateTime(BankHolidayYear, 12, 25)), commonMethodsHelper.GetWeekFirstMonday(new DateTime(BankHolidayYear, 12, 25)).AddDays(6), commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId1, "provider", "P5786 " + currentTimeString);

            #endregion

            #region Step 11

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            #region Scheduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(_expressBookingCriteriaId1);
            Assert.AreEqual(1, expressBookingResultId.Count);

            var cpDiaryBookingId = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            Assert.AreEqual(0, cpDiaryBookingId.Count);

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Express Booking Results")
                .SelectFilterInsideOptGroup("1", "Express Booking Result")
                .SelectOperator("1", "Contains Data")
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(13)
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(13)
                .OpenRecord(expressBookingResultId[0].ToString());

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoadFromAdvancedSearch()
                .ValidateStaffNamesContainsText("")
                .ValidateProviderNameText(_providerName1)
                .ValidateExceptionMessageText("Booking not required due to public holiday.")
                .ValidateExpressBookingFailureReasonSelectedText("Booking not required")
                .ValidateBookingDiaryLookupButtonVisible(true)
                .ValidateBookingDiaryLinkText("");

            #endregion
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-5913

        [TestProperty("JiraIssueID", "ACC-5912")]
        [Description("Automation for steps from the original test ACC-5912.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        [TestProperty("Screen2", "Advanced Search")]
        [TestProperty("Screen3", "Provider Diary")]
        public void ProviderScheduleResponsibleTeam_UITestMethod001()
        {

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Business Unit 2

            var _businessUnitId2 = commonMethodsDB.CreateBusinessUnit("PSB BU2 " + currentDateString);

            #endregion

            #region Team 2

            string _teamName2 = "PSBT2 " + currentDateString;
            var _teamId2 = commonMethodsDB.CreateTeam(_teamName2, null, _businessUnitId2, "107623", "PSBT2_" + currentDateString + "@careworkstempmail.com", "Provider Schedule Booking Team2 " + currentDateString, "020 123456");

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role4823" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "P5912 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId2, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5912", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "Staff1A" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "Staff1A", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId1, 1); //1 = Male

            //Create Team Member for System User 1 to Team 2
            commonMethodsDB.CreateTeamMember(_teamId2, _systemUserId1, new DateTime(2023, 1, 1), null);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId2, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            int startDay = (int)todayDate.DayOfWeek;

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, startDay, startDay, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), _providerId, "Provider Schedule Booking Testing " + currentTimeString);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);


            #endregion

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_staffAName, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 4

            //navigate to Provider Schedule page

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("Staff1A " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupClosed();

            //navigate to advanced search page from main menu
            //wait for page to load and select record type = care provider booking schedules
            //Click delete button and click search button

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Care Provider Booking Schedules")
                .ClickDeleteButton()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(2)
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(2)
                .WaitForResultsPageToLoad()
                .OpenRecord(cpBookingSchedule1Id.ToString());

            careProviderBookingScheduleRecordPage
                .WaitForCareProviderBookingScheduleRecordPageToLoadFromAdvancedSearch()
                .ValidateResponsibleTeamLinkText(_teamName2);

            #endregion

            #region Step 5

            var cpBookingScheduleStaffId = dbHelper.cpBookingScheduleStaff.GetBySystemUserEmploymentContractId(_systemUserEmploymentContractId1)[0];

            careProviderBookingScheduleRecordPage
                .WaitForStaffSectionToLoad()
                .OpenBookingScheduleStaffRecord(cpBookingScheduleStaffId);

            bookingScheduleStaffRecordPage
                .WaitForBookingScheduleStaffRecordPageToLoad()
                .ValidateResponsibleTeamLinkText(_teamName2);

            #endregion

            #region Step 6

            //navigate to Provider diary page from main menu
            //select provider
            //create diary booking

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address", _providerId)
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")
                .InsertStartTime("02", "00")
                .InsertEndTime("06", "00")
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .ClickOnlyShowAvailableStaff()
                .EnterTextIntoFilterStaffByNameSearchBox("Staff1A " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address", _providerId)
                .WaitForProviderDiaryPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var careProviderDiaryBooking = dbHelper.cPBookingDiary.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderDiaryBooking.Count);

            //navigate to advanced search page from main menu
            //wait for page to load and select record type = care provider diary bookings
            //Click delete button and click search button

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Care Provider Diary Bookings")
                .ClickDeleteButton()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(2)
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(2)
                .WaitForResultsPageToLoad()
                .SelectSearchResultRecord(careProviderDiaryBooking[0].ToString())
                .ClickEditButton_ResultsPage();

            diaryBookingsRecordPage
                .WaitForDiaryBookingRecordPageToLoadFromAdvancedSearch()
                .ValidateResponsibleTeamLinkText(_teamName2);

            #endregion

            #region Step 7

            var cpBookingDiaryStaffId = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(careProviderDiaryBooking[0])[0];

            diaryBookingsRecordPage
                .WaitForDiaryBookingRecordPageToLoadFromAdvancedSearch()
                .WaitForBookingDiaryStaffSubRecordPageToLoad()
                .OpenDiaryBookingStaffRecord(cpBookingDiaryStaffId);

            bookingDiaryStaffRecordPage
                .WaitForBookingDiaryStaffRecordPageToLoad()
                .ValidateResponsibleTeamLinkText(_teamName2);

            #endregion

            #region Step 8

            string _cpDiaryBooking_OwningBusinessUnitId = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(cpBookingDiaryStaffId, "owningbusinessunitid")["owningbusinessunitid"].ToString();
            Assert.AreEqual(_businessUnitId2.ToString(), _cpDiaryBooking_OwningBusinessUnitId.ToString());

            var _cpDiaryBookingStaffEmploymentContractId_ResponsibleTeamId = dbHelper.systemUserEmploymentContract.GetByID(_systemUserEmploymentContractId1, "ownerid")["ownerid"].ToString();
            Assert.AreEqual(_teamId2.ToString(), _cpDiaryBookingStaffEmploymentContractId_ResponsibleTeamId.ToString());

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("Staff1A" + currentTimeString)
                .ClickSearchButton()
                .OpenRecord(_systemUserId1);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(_systemUserEmploymentContractId1);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateResponsibleTeamLinkFieldText(_teamName2);

            #endregion

            #region Step 9

            string _cpBookingSchedule_OwningBusinessUnitId = dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffId, "owningbusinessunitid")["owningbusinessunitid"].ToString();
            Assert.AreEqual(_businessUnitId2.ToString(), _cpBookingSchedule_OwningBusinessUnitId.ToString());

            string _cpDiaryBookingStaffEmploymentContractId_string = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(cpBookingDiaryStaffId, "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString();
            Guid _cpDiaryBookingStaffEmploymentContractId = new Guid(_cpDiaryBookingStaffEmploymentContractId_string);
            var _cpDiaryBookingStaffEmploymentContractId_ResponsibleTeamId2 = dbHelper.systemUserEmploymentContract.GetByID(_cpDiaryBookingStaffEmploymentContractId, "ownerid")["ownerid"].ToString();
            Assert.AreEqual(_teamId2.ToString(), _cpDiaryBookingStaffEmploymentContractId_ResponsibleTeamId2.ToString());

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5997

        [TestProperty("JiraIssueID", "ACC-5998")]
        [Description("Automation for step 1 to 8 from the original test ACC-5988.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_ACC_5988_UITestMethod001()
        {

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role5998" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "P5998 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5998 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5998 2", 2, null, null, null, 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType2, false);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "CookA1" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "CookA1", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType2);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);

            #endregion

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            #endregion

            #region Step 3

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn("Monday");

            #endregion

            #region Step 4

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ValidateStartDayText("Monday")
                .ValidateEndDayText("Monday");

            #endregion

            #region Step 5

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ValidateStartTime("12:00")
                .ValidateEndTime("12:15");

            #endregion

            #region Step 6

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ValidateBookingDurationText("15 minutes");

            #endregion

            #region Step 7

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC ACC-5998 1");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ValidateStartTime("12:00")
                .ValidateEndTime("04:00");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC ACC-5998 2");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ValidateStartTime("12:00")
                .ValidateEndTime("12:15");

            #endregion

            #region Step 8

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartTime("09", "58")
                .SetEndTime("10", "58");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ValidateStartTime("10:00")
                .ValidateEndTime("11:00");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartTime("09", "37")
                .SetEndTime("10", "37");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ValidateStartTime("09:35")
                .ValidateEndTime("10:35");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartTime("09", "38")
                .SetEndTime("10", "38");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ValidateStartTime("09:40")
                .ValidateEndTime("10:40");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartTime("09", "24")
                .SetEndTime("10", "24");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ValidateStartTime("09:25")
                .ValidateEndTime("10:25");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartTime("09", "07")
                .SetEndTime("10", "07");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ValidateStartTime("09:05")
                .ValidateEndTime("10:05");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CookA1 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(1000);

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad()
               .ClickScheduleBooking(careProviderBookingSchedules[0].ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateStartTime("09:05")
                .ValidateEndTime("10:05")
                .ValidateBookingDurationText("1 hour")
                .ValidateSelectedStaffFieldValues(_systemUserEmploymentContractId1, "CookA1 " + currentTimeString)
                .ClickOnCloseButton()
                .WaitForEditScheduleBookingPopupClosed();

            #endregion

            #region Step 9

            #region CP Schedule Setup - Set Auto Refresh to Yes

            dbHelper.cPSchedulingSetup.UpdateAutoRefresh(cPSchedulingSetupId, true); //Set Auto Refresh to Yes
            int _autoRefreshInterval = 30;
            dbHelper.cPSchedulingSetup.UpdateAutoRefreshInterval(cPSchedulingSetupId, _autoRefreshInterval);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderSchedulePageToLoad();

            providerSchedulePage
                .WaitForRefreshPanelToDisappear()
                .WaitForProviderSchedulePageToLoad()
                .ValidateScheduleBookingIsPresent(careProviderBookingSchedules[0].ToString(), true);

            providerSchedulePage
                .ClickGridColumn("Tuesday");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ValidateBookingTypeDropDownText("Select")
                .ValidateStartDayText("Tuesday")
                .ValidateEndDayText("Tuesday")
                .ValidateStartTime("12:00")
                .ValidateEndTime("12:15")
                .ValidateManageStaffButtonDisabled(true);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6059

        [TestProperty("JiraIssueID", "ACC-6065")]
        [Description("Automation for step 10 from the original test ACC-5988.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_ACC_5988_UITestMethod002()
        {

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role6065" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "P6065 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-6065 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "CookA2" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "CookA2", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);

            #endregion

            #region Step 10

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn("Wednesday");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay("Wednesday")
                .SetEndDay("Wednesday")
                .SetStartTime("00", "00")
                .SetEndTime("12", "00")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CookA2 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1000);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();


            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateStaffLabelText("Staff: CookA2 " + currentTimeString)
                .ValidateTimeLabelText("Wednesday 00:00 - 12:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC ACC-6065 1")
                .ValidateOccursLabelText("Every 1 week");

            //
            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn("Friday");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay("Friday")
                .SetStartTime("00", "00")
                .SetEndTime("00", "00")
                .SetEndDay("Monday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CookA2 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1000);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();


            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateScheduleBookingForFridayIsPresent(careProviderBookingSchedules[0].ToString(), true)
                .ValidateScheduleBookingForSaturdayIsPresent(careProviderBookingSchedules[0].ToString(), true)
                .ValidateScheduleBookingForSundayIsPresent(careProviderBookingSchedules[0].ToString(), true);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1000);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateStaffLabelText("Staff: CookA2 " + currentTimeString)
                .ValidateTimeLabelText("Friday 00:00 - Monday 00:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC ACC-6065 1")
                .ValidateOccursLabelText("Every 1 week");


            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6070")]
        [Description("Automation for step 11 from the original test ACC-5988.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_ACC_5988_UITestMethod003()
        {

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role6070" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "P6070 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-6070 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "CookA3" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "CookA3", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);

            #endregion

            #region Step 11

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn("Sunday");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay("Sunday")
                .SetStartTime("00", "00")
                .SetEndTime("06", "00")
                .SetEndDay("Sunday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CookA3 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();


            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateStaffLabelText("Staff: CookA3 " + currentTimeString)
                .ValidateTimeLabelText("Sunday 00:00 - 06:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC ACC-6070 1")
                .ValidateOccursLabelText("Every 1 week");

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn("Sunday");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay("Sunday")
                .SetStartTime("18", "15")
                .SetEndTime("23", "45")
                .SetEndDay("Sunday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CookA3 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();


            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(2000);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateStaffLabelText("CookA3 " + currentTimeString)
                .ValidateTimeLabelText("Sunday 18:15 - 23:45")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC ACC-6070 1")
                .ValidateOccursLabelText("Every 1 week");

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn("Sunday");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay("Sunday")
                .SetStartTime("06", "00")
                .SetEndTime("09", "00")
                .SetEndDay("Sunday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CookA3 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();


            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(3, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1000);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateStaffLabelText("CookA3 " + currentTimeString)
                .ValidateTimeLabelText("Sunday 06:00 - 09:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC ACC-6070 1")
                .ValidateOccursLabelText("Every 1 week");

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn("Sunday");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay("Sunday")
                .SetStartTime("09", "00")
                .SetEndTime("18", "15")
                .SetEndDay("Sunday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CookA3 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();


            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(4, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1000);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateStaffLabelText("CookA3 " + currentTimeString)
                .ValidateTimeLabelText("Sunday 09:00 - 18:15")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC ACC-6070 1")
                .ValidateOccursLabelText("Every 1 week");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6085")]
        [Description("Automation for step 12 from the original test ACC-5988.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_ACC_5988_UITestMethod004()
        {

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role6085" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "P6085 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-6085 1", 2, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "CookA4" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "CookA4", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            string _staffBName = "CookB4" + currentTimeString;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_staffBName, "CookB4", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            string _staffCName = "CookC4" + currentTimeString;
            var _systemUserId3 = commonMethodsDB.CreateSystemUserRecord(_staffCName, "CookC4", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            string _staffDName = "CookD4" + currentTimeString;
            var _systemUserId4 = commonMethodsDB.CreateSystemUserRecord(_staffDName, "CookD4", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);


            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId2, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId3, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId4, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId2, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId3 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId3, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId4 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId4, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId3, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId4, _bookingType1);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId2, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId3, _teamId, _systemUserEmploymentContractId3, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId4, _teamId, _systemUserEmploymentContractId4, _availabilityTypeId);

            #endregion

            #region Step 12

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn("Monday");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay("Monday")
                .SetStartTime("00", "00")
                .SetEndTime("03", "00")
                .SetEndDay("Monday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CookA4 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();


            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateStaffLabelText("CookA4 " + currentTimeString)
                .ValidateTimeLabelText("Monday 00:00 - 03:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC ACC-6085 1")
                .ValidateOccursLabelText("Every 1 week");

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn("Monday");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay("Monday")
                .SetStartTime("02", "30")
                .SetEndTime("12", "00")
                .SetEndDay("Monday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CookB4 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId2)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();


            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(2000);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateStaffLabelText("CookB4 " + currentTimeString)
                .ValidateTimeLabelText("Monday 02:30 - 12:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC ACC-6085 1")
                .ValidateOccursLabelText("Every 1 week");

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn("Monday");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay("Monday")
                .SetStartTime("14", "00")
                .SetEndTime("23", "00")
                .SetEndDay("Monday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CookD4 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId4)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();


            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(3, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1000);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateStaffLabelText("CookD4 " + currentTimeString)
                .ValidateTimeLabelText("Monday 14:00 - 23:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC ACC-6085 1")
                .ValidateOccursLabelText("Every 1 week");

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn("Monday");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay("Monday")
                .SetStartTime("09", "00")
                .SetEndTime("17", "00")
                .SetEndDay("Monday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CookC4 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId3)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();


            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(4, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1000);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateStaffLabelText("CookC4 " + currentTimeString)
                .ValidateTimeLabelText("Monday 09:00 - 17:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC ACC-6085 1")
                .ValidateOccursLabelText("Every 1 week");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6060

        [TestProperty("JiraIssueID", "ACC-6129")]
        [Description("Automation for step 13 from the original test ACC-5988.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_ACC_5988_UITestMethod005()
        {

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role6129" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "P6129 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingTypeBTC1 = commonMethodsDB.CreateCPBookingType("BTC1 ACC-6129", 1, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 1);
            var _bookingTypeBTC2 = commonMethodsDB.CreateCPBookingType("BTC2 ACC-6129", 2, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 2, false, 1000, null, null, null);
            var _bookingTypeBTC3 = commonMethodsDB.CreateCPBookingType("BTC3 ACC-6129", 3, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 1);
            var _bookingTypeBTC4 = commonMethodsDB.CreateCPBookingType("BTC4 ACC-6129", 4, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, true, null, null, null, null);


            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeBTC1, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeBTC2, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeBTC3, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeBTC4, false);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "CookA5" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "CookA5", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            string _staffBName = "CookB5" + currentTimeString;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_staffBName, "CookB5", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            string _staffCName = "CookC5" + currentTimeString;
            var _systemUserId3 = commonMethodsDB.CreateSystemUserRecord(_staffCName, "CookC5", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            string _staffDName = "CookD5" + currentTimeString;
            var _systemUserId4 = commonMethodsDB.CreateSystemUserRecord(_staffDName, "CookD5", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);


            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId2, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId3, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId4, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId2, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId3 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId3, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId4 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId4, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId3, _bookingTypeBTC3);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId4, _bookingTypeBTC4);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId2, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId3, _teamId, _systemUserEmploymentContractId3, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId4, _teamId, _systemUserEmploymentContractId4, _availabilityTypeId);

            #endregion

            #region Step 13

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn("Monday");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC1 ACC-6129")
                .SetStartDay("Monday")
                .SetStartTime("02", "00")
                .SetEndTime("06", "00")
                .SetEndDay("Monday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CookA5 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();


            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateStaffLabelText("CookA5 " + currentTimeString)
                .ValidateTimeLabelText("Monday 02:00 - 06:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC1 ACC-6129")
                .ValidateOccursLabelText("Every 1 week");

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn("Monday");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC2 ACC-6129")
                .SetStartDay("Monday")
                .SetStartTime("02", "00")
                .SetEndTime("06", "00")
                .SetEndDay("Monday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CookB5 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId2)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();


            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateStaffLabelText("CookB5 " + currentTimeString)
                .ValidateTimeLabelText("Monday 02:00 - 06:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC2 ACC-6129")
                .ValidateOccursLabelText("Every 1 week");

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn("Monday");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC3 ACC-6129")
                .SetStartDay("Monday")
                .SetStartTime("02", "00")
                .SetEndTime("06", "00")
                .SetEndDay("Monday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CookC5 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId3)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();


            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(3, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateStaffLabelText("CookC5 " + currentTimeString)
                .ValidateTimeLabelText("Monday 02:00 - 06:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC3 ACC-6129")
                .ValidateOccursLabelText("Every 1 week");

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn("Monday");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC4 ACC-6129")
                .SetStartDay("Monday")
                .SetStartTime("02", "00")
                .SetEndTime("06", "00")
                .SetEndDay("Monday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CookD5 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId4)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking()
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("You have selected a booking type dedicated for absences. Do you want to save?")
                .ClickConfirmButton_WarningDialogue();

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(4, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateStaffLabelText("CookD5 " + currentTimeString)
                .ValidateTimeLabelText("Monday 02:00 - 06:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC4 ACC-6129")
                .ValidateOccursLabelText("Every 1 week");
            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6134")]
        [Description("Automation for step 14, step 21 from the original test ACC-5988.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_ACC_5988_UITestMethod006()
        {

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role6134" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "P6134 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingTypeBTC2 = commonMethodsDB.CreateCPBookingType("BTC2 ACC-6134", 2, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 2, false, 1000, null, null, null);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeBTC2, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "CookA6" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "CookA6", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            string _staffBName = "CookB6" + currentTimeString;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_staffBName, "CookB6", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            string _staffCName = "CookC6" + currentTimeString;
            var _systemUserId3 = commonMethodsDB.CreateSystemUserRecord(_staffCName, "CookC6", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            string _staffDName = "CookD6" + currentTimeString;
            var _systemUserId4 = commonMethodsDB.CreateSystemUserRecord(_staffDName, "CookD6", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            string _staffEName = "CookE6" + currentTimeString;
            var _systemUserId5 = commonMethodsDB.CreateSystemUserRecord(_staffEName, "CookE6", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            string _staffFName = "CookF6" + currentTimeString;
            var _systemUserId6 = commonMethodsDB.CreateSystemUserRecord(_staffFName, "CookF6", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);


            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId2, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId3, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId4, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId5, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId6, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId2, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId3 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId3, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId4 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId4, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId5 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId5, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId6 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId6, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId3, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId4, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId5, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId6, _bookingTypeBTC2);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId2, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId3, _teamId, _systemUserEmploymentContractId3, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId4, _teamId, _systemUserEmploymentContractId4, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId5, _teamId, _systemUserEmploymentContractId5, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId6, _teamId, _systemUserEmploymentContractId6, _availabilityTypeId);

            #endregion

            #region Step 13, Step 21

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn("Monday");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay("Monday")
                .SetStartTime("02", "00")
                .SetEndTime("06", "00")
                .SetEndDay("Monday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CookA6 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .EnterTextIntoFilterStaffByNameSearchBox("CookB6 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId2)
                .EnterTextIntoFilterStaffByNameSearchBox("CookC6 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId3)
                .EnterTextIntoFilterStaffByNameSearchBox("CookD6 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId4)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateSliderLabel_BookingTypeNameText(careProviderBookingSchedules[0].ToString(), "BTC2 ACC-6134")
                .ValidateSliderLabel_NumberOfStaffText(careProviderBookingSchedules[0].ToString(), "4/4")
                .ValidateSlider_BGColorAllocated(careProviderBookingSchedules[0].ToString());

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateStaffLabelText("CookA6 " + currentTimeString + ", CookB6 " + currentTimeString + ", + 2 others")
                .ValidateTimeLabelText("Monday 02:00 - 06:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC2 ACC-6134")
                .ValidateOccursLabelText("Every 1 week");

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn("Monday");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay("Monday")
                .SetStartTime("02", "00")
                .SetEndTime("06", "00")
                .SetEndDay("Monday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CookE6 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId5)
                .EnterTextIntoFilterStaffByNameSearchBox("CookF6 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId6)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickAddUnassignedStaff()
                .ClickAddUnassignedStaff()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateSliderLabel_BookingTypeNameText(careProviderBookingSchedules[0].ToString(), "BTC2 ACC-6134")
                .ValidateSliderLabel_NumberOfStaffText(careProviderBookingSchedules[0].ToString(), "2/4")
                .ValidateSlider_BGColorUnallocated(careProviderBookingSchedules[0].ToString());

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateStaffLabelText("CookE6 " + currentTimeString + ", CookF6 " + currentTimeString)
                .ValidateTimeLabelText("Monday 02:00 - 06:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC2 ACC-6134")
                .ValidateOccursLabelText("Every 1 week");

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn("Monday");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay("Monday")
                .SetStartTime("02", "00")
                .SetEndTime("06", "00")
                .SetEndDay("Monday")
                .ClickAddUnassignedStaff()
                .ClickAddUnassignedStaff()
                .ClickAddUnassignedStaff()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(3, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateSliderLabel_BookingTypeNameText(careProviderBookingSchedules[0].ToString(), "BTC2 ACC-6134")
                .ValidateSliderLabel_NumberOfStaffText(careProviderBookingSchedules[0].ToString(), "0/4")
                .ValidateSlider_BGColorUnallocated(careProviderBookingSchedules[0].ToString());

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateStaffLabelText("Unassigned")
                .ValidateTimeLabelText("Monday 02:00 - 06:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC2 ACC-6134")
                .ValidateOccursLabelText("Every 1 week");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6140")]
        [Description("Automation for step 15 from the original test ACC-5988.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_ACC_5988_UITestMethod007()
        {

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role6140" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "P6140 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingTypeBTC2 = commonMethodsDB.CreateCPBookingType("BTC2 ACC-6140", 2, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 2, false, 1000, null, null, null);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeBTC2, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "CookA7" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "CookA7", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            string _staffBName = "CookB7" + currentTimeString;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_staffBName, "CookB7", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            string _staffCName = "CookC7" + currentTimeString;
            var _systemUserId3 = commonMethodsDB.CreateSystemUserRecord(_staffCName, "CookC7", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);


            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId2, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId3, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId2, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId3 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId3, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId3, _bookingTypeBTC2);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId2, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId3, _teamId, _systemUserEmploymentContractId3, _availabilityTypeId);

            #endregion

            #region Step 15

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn("Monday");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay("Monday")
                .SetStartTime("02", "00")
                .SetEndTime("06", "00")
                .SetEndDay("Monday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CookA7 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            var thisWeekMondayDate = commonMethodsHelper.GetThisWeekFirstMonday();
            string year = thisWeekMondayDate.ToString("yyyy");
            string month = thisWeekMondayDate.ToString("MMMM");
            string date = thisWeekMondayDate.Day.ToString();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .SelectFirstOccurrenceDate(year, month, date)
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("This booking will not be booked until " + thisWeekMondayDate.ToString("dd'/'MM'/'yyyy") + ".")
                .ClickConfirmButton_WarningDialogue();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateTimeLabelText("Monday 02:00 - 06:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC2 ACC-6140")
                .ValidateOccursLabelText("Every 1 week")
                .ValidateStaffLabelText("CookA7 " + currentTimeString);

            var nextWeekMondayDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);
            string year2 = nextWeekMondayDate.ToString("yyyy");
            string month2 = nextWeekMondayDate.ToString("MMMM");
            string date2 = nextWeekMondayDate.Day.ToString();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn("Monday");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay("Monday")
                .SetStartTime("02", "00")
                .SetEndTime("06", "00")
                .SetEndDay("Monday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CookB7 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId2)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .SelectFirstOccurrenceDate(year, month, date)
                .SelectLastOccurrenceDate(year2, month2, date2)
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("This booking will not be booked until " + thisWeekMondayDate.ToString("dd'/'MM'/'yyyy") + ".", 1)
                .ValidateMessage_DynamicDialogue("This booking will be deleted when express booking is run for a period after " + nextWeekMondayDate.ToString("dd'/'MM'/'yyyy") + ".", 2)
                .ClickConfirmButton_WarningDialogue();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateTimeLabelText("Monday 02:00 - 06:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC2 ACC-6140")
                .ValidateOccursLabelText("Every 1 week")
                .ValidateStaffLabelText("CookB7 " + currentTimeString);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6142")]
        [Description("Automation for step 16 from the original test ACC-5988.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_ACC_5988_UITestMethod008()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            string todayDay = todayDate.DayOfWeek.ToString();

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role6142" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "P6142 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingTypeBTC2 = commonMethodsDB.CreateCPBookingType("BTC2 ACC-6142", 2, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 2, false, 1000, null, null, null);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeBTC2, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "CookA8" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "CookA8", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            string _staffBName = "CookB8" + currentTimeString;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_staffBName, "CookB8", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId2, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId2, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingTypeBTC2);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId2, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Step 16

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn(todayDay);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay(todayDay)
                .SetStartTime("02", "00")
                .SetEndTime("06", "00")
                .SetEndDay(todayDay)
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CookA8 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            var nextDueDate = todayDate.AddDays(7);
            string nextDueDateYear1 = nextDueDate.ToString("yyyy");
            string nextDueDateMonth1 = nextDueDate.ToString("MMMM");
            string nextDueDateDate1 = nextDueDate.Day.ToString();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .SelectBookingTakesPlaceEvery("2 weeks")
                .SelectNextDueToTakePlaceDate(nextDueDateYear1, nextDueDateMonth1, nextDueDateDate1)
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateTimeLabelText(todayDay + " 02:00 - 06:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC2 ACC-6142")
                .ValidateOccursLabelText("Every 2 weeks")
                .ValidateStaffLabelText("CookA8 " + currentTimeString);

            var nextDueDate2 = todayDate.AddDays(15);
            string nextDueDateYear2 = nextDueDate2.ToString("yyyy");
            string nextDueDateMonth2 = nextDueDate2.ToString("MMMM");
            string nextDueDateDate2 = nextDueDate2.Day.ToString();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn(todayDay);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay(todayDay)
                .SetStartTime("02", "00")
                .SetEndTime("06", "00")
                .SetEndDay(todayDay)
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CookB8 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId2)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .SelectBookingTakesPlaceEvery("3 weeks")
                .SelectNextDueToTakePlaceDate(nextDueDateYear2, nextDueDateMonth2, nextDueDateDate2)
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateTimeLabelText(todayDay + " 02:00 - 06:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC2 ACC-6142")
                .ValidateOccursLabelText("Every 3 weeks")
                .ValidateStaffLabelText("CookB8 " + currentTimeString);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6061

        [TestProperty("JiraIssueID", "ACC-6160")]
        [Description("Automation for step 17 from the original test ACC-5988.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_ACC_5988_UITestMethod009()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            string todayDay = todayDate.DayOfWeek.ToString();

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role6160" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "P6160 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingTypeBTC2 = commonMethodsDB.CreateCPBookingType("BTC2 ACC-6160", 2, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 2, false, 1000, null, null, null);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeBTC2, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "CookA9" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "CookA9", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            string _staffBName = "CookB9" + currentTimeString;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_staffBName, "CookB9", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId2, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId2, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingTypeBTC2);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId2, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Step 17

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn(todayDay);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay(todayDay)
                .SetStartTime("02", "00")
                .SetEndTime("06", "00")
                .SetEndDay(todayDay)
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CookA9 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            var nextDueDate = todayDate.AddDays(7);
            string nextDueDateYear1 = nextDueDate.ToString("yyyy");
            string nextDueDateMonth1 = nextDueDate.ToString("MMMM");
            string nextDueDateDate1 = nextDueDate.Day.ToString();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .SelectBookingTakesPlaceEvery("2 weeks")
                .SelectNextDueToTakePlaceDate(nextDueDateYear1, nextDueDateMonth1, nextDueDateDate1)
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateTimeLabelText(todayDay + " 02:00 - 06:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC2 ACC-6160")
                .ValidateOccursLabelText("Every 2 weeks")
                .ValidateStaffLabelText("CookA9 " + currentTimeString);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn(todayDay);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay(todayDay)
                .SetStartTime("02", "00")
                .SetEndTime("06", "00")
                .SetEndDay(todayDay)
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CookB9 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId2)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .SelectBookingTakesPlaceEvery("2 weeks")
                .SelectNextDueToTakePlaceDate(nextDueDateYear1, nextDueDateMonth1, nextDueDateDate1)
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateTimeLabelText(todayDay + " 02:00 - 06:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC2 ACC-6160")
                .ValidateOccursLabelText("Every 2 weeks")
                .ValidateStaffLabelText("CookB9 " + currentTimeString);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6167")]
        [Description("Automation for step 18 from the original test ACC-5988.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_ACC_5988_UITestMethod010()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            string todayDay = todayDate.DayOfWeek.ToString();

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role6167" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "P6167 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingTypeBTC2A = commonMethodsDB.CreateCPBookingType("BTC2 ACC-6167A", 2, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 2, false, 1000, null, null, null);
            var _bookingTypeBTC2B = commonMethodsDB.CreateCPBookingType("BTC2 ACC-6167B", 2, null, null, null, 1, false, null, null, null, 1);


            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeBTC2A, true);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeBTC2B, false);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "CarerA1" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "CarerA1", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            string _staffBName = "CarerA2" + currentTimeString;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_staffBName, "CarerA2", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId2, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId2, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingTypeBTC2A);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingTypeBTC2A);

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingTypeBTC2B);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingTypeBTC2B);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId2, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Step 18

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn(todayDay);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay(todayDay)
                .SetStartTime("02", "00")
                .SetEndTime("06", "00")
                .SetEndDay(todayDay)
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CarerA1 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateTimeLabelText(todayDay + " 02:00 - 06:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC2 ACC-6167A")
                .ValidateOccursLabelText("Every 1 week")
                .ValidateStaffLabelText("CarerA1 " + currentTimeString);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn(todayDay);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC2 ACC-6167B")
                .SetStartDay(todayDay)
                .SetStartTime("12", "00")
                .SetEndTime("12", "15")
                .SetEndDay(todayDay)
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CarerA2 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId2)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateTimeLabelText(todayDay + " 12:00 - 12:15")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC2 ACC-6167B")
                .ValidateOccursLabelText("Every 1 week")
                .ValidateStaffLabelText("CarerA2 " + currentTimeString);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6168")]
        [Description("Automation for step 19, 20 from the original test ACC-5988.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_ACC_5988_UITestMethod011()
        {

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role6168" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "P6168 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingTypeBTC2 = commonMethodsDB.CreateCPBookingType("BTC2 ACC-6168", 2, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 2, false, 1000, null, null, null);


            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeBTC2, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "CarerB1" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "CarerB1", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingTypeBTC2);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);

            #endregion

            #region Step 19, 20

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn("Sunday");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay("Sunday")
                .SetStartTime("02", "00")
                .SetEndTime("06", "00")
                .SetEndDay("Sunday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CarerB1 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateScheduleBookingIsPresent(careProviderBookingSchedules[0].ToString(), true)
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateTimeLabelText("Sunday 02:00 - 06:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC2 ACC-6168")
                .ValidateOccursLabelText("Every 1 week")
                .ValidateStaffLabelText("CarerB1 " + currentTimeString);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn("Monday");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay("Monday")
                .SetStartTime("18", "00")
                .SetEndTime("22", "00")
                .SetEndDay("Monday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CarerB1 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateScheduleBookingIsPresent(careProviderBookingSchedules[0].ToString(), true)
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateTimeLabelText("Monday 18:00 - 22:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC2 ACC-6168")
                .ValidateOccursLabelText("Every 1 week")
                .ValidateStaffLabelText("CarerB1 " + currentTimeString);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn("Tuesday");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay("Tuesday")
                .SetStartTime("18", "00")
                .SetEndTime("22", "00")
                .SetEndDay("Tuesday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CarerB1 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(3, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateScheduleBookingIsPresent(careProviderBookingSchedules[0].ToString(), true)
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateTimeLabelText("Tuesday 18:00 - 22:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC2 ACC-6168")
                .ValidateOccursLabelText("Every 1 week")
                .ValidateStaffLabelText("CarerB1 " + currentTimeString);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn("Friday");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay("Friday")
                .SetStartTime("18", "00")
                .SetEndTime("22", "00")
                .SetEndDay("Friday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CarerB1 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(4, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateScheduleBookingIsPresent(careProviderBookingSchedules[0].ToString(), true)
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateTimeLabelText("Friday 18:00 - 22:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC2 ACC-6168")
                .ValidateOccursLabelText("Every 1 week")
                .ValidateStaffLabelText("CarerB1 " + currentTimeString);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn("Saturday");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay("Saturday")
                .SetStartTime("06", "15")
                .SetEndTime("10", "15")
                .SetEndDay("Saturday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CarerB1 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(5, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateScheduleBookingIsPresent(careProviderBookingSchedules[0].ToString(), true)
                .ValidateBorderForFrequencyEquals1Week(careProviderBookingSchedules[0].ToString())
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateTimeLabelText("Saturday 06:15 - 10:15")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC2 ACC-6168")
                .ValidateOccursLabelText("Every 1 week")
                .ValidateStaffLabelText("CarerB1 " + currentTimeString);

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var nextDueDate = commonMethodsHelper.GetWeekFirstMonday(todayDate).AddDays(28);
            string nextDueDateDay = nextDueDate.DayOfWeek.ToString();

            string nextDueDateYear1 = nextDueDate.ToString("yyyy");
            string nextDueDateMonth1 = nextDueDate.ToString("MMMM");
            string nextDueDateDate1 = nextDueDate.Day.ToString();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn(nextDueDateDay);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay(nextDueDateDay)
                .SetStartTime("10", "00")
                .SetEndTime("14", "00")
                .SetEndDay(nextDueDateDay)
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("CarerB1 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .SelectBookingTakesPlaceEvery("4 weeks")
                .SelectNextDueToTakePlaceDate(nextDueDateYear1, nextDueDateMonth1, nextDueDateDate1)
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(6, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateScheduleBookingIsPresent(careProviderBookingSchedules[0].ToString(), true)
                .ValidateBorderForFrequencyGreaterThan1Week(careProviderBookingSchedules[0].ToString())
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateTimeLabelText(nextDueDateDay + " 10:00 - 14:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC2 ACC-6168")
                .ValidateOccursLabelText("Every 4 weeks")
                .ValidateStaffLabelText("CarerB1 " + currentTimeString);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6203

        [TestProperty("JiraIssueID", "ACC-6202")]
        [Description("Automation for step 1 to step 7 from the original test ACC-6201.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        [TestProperty("Screen2", "Express Booking Results")]
        [TestProperty("Screen3", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6201_UITestMethod001()
        {
            var availabilityDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);
            string availabilityDay = availabilityDate.DayOfWeek.ToString();
            string availabilityDate_Year = availabilityDate.Year.ToString();
            string availabilityDate_Month = availabilityDate.ToString("MMMM");
            string availabilityDate_Date = availabilityDate.Day.ToString();

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role6202" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName1 = "P6202A " + currentTimeString;
            var _providerId1 = commonMethodsDB.CreateProvider(_providerName1, _teamId, 12, true); // Training Provider

            var _providerName2 = "P6202B " + currentTimeString;
            var _providerId2 = commonMethodsDB.CreateProvider(_providerName2, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingTypeBTC2 = commonMethodsDB.CreateCPBookingType("BTC2 ACC-6202", 2, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 2, false, 1000, null, null, null);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId1, _bookingTypeBTC2, true);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId2, _bookingTypeBTC2, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "HelpA1" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "HelpA1", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            string _staffBName = "HelpA2" + currentTimeString;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_staffBName, "HelpA2", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            string _staffCName = "HelpA3" + currentTimeString;
            var _systemUserId3 = commonMethodsDB.CreateSystemUserRecord(_staffCName, "HelpA3", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            string _staffDName = "HelpA4" + currentTimeString;
            var _systemUserId4 = commonMethodsDB.CreateSystemUserRecord(_staffDName, "HelpA4", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);


            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId2, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId3, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId4, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId2, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId3 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId3, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId4 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId4, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId4_Title = (string)dbHelper.systemUserEmploymentContract.GetByID(_systemUserEmploymentContractId4, "name")["name"];

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId3, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId4, _bookingTypeBTC2);

            #endregion

            #region Recurrence Patterns

            _recurrencePattern_Every1WeekMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on monday").First();
            _recurrencePattern_Every2WeeksMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 2 week(s) on monday").First();

            #endregion

            #region User Work Schedule

            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId1, _teamId, _recurrencePattern_Every2WeeksMondayId, _systemUserEmploymentContractId1, _availabilityTypeId, availabilityDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 2);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId2, _teamId, _recurrencePattern_Every2WeeksMondayId, _systemUserEmploymentContractId2, _availabilityTypeId, availabilityDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 2);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId3, _teamId, _recurrencePattern_Every2WeeksMondayId, _systemUserEmploymentContractId3, _availabilityTypeId, availabilityDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 2);
            var _userWorkscheduleId4 = dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId4, _teamId, _recurrencePattern_Every2WeeksMondayId, _systemUserEmploymentContractId4, _availabilityTypeId, availabilityDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 2);

            #endregion

            #region Step 1. Step 3 and Step 5, Step 6

            //Step 2 and Step 4 - Test the same as Step 3 and Step 5.
            //Step 7 - Verify the same result as Step 6 and Step 5.

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName1 + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn(availabilityDay);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartTime("12", "00")
                .SetEndTime("16", "00")
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .SelectBookingTakesPlaceEvery("2 weeks")
                .SelectNextDueToTakePlaceDate(availabilityDate_Year, availabilityDate_Month, availabilityDate_Date)
                .ClickRosteringTab()
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId2)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId1);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateSliderLabel_NumberOfStaffText(careProviderBookingSchedules[0].ToString(), "2/2")
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateDueToTakePlaceLabelText(availabilityDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateStaffLabelText("HelpA1 " + currentTimeString + ", " + "HelpA2 " + currentTimeString);

            #region Express Book for Provider

            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId1, commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(21), commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId1, "provider", "P6202A " + currentTimeString);

            #endregion

            #region Schdeduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(_expressBookingCriteriaId1);
            Assert.AreEqual(0, expressBookingResultId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + currentTimeString)
                .OpenRecord(_expressBookingCriteriaId1);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .ValidateNoRecordMessageVisible(true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            var careProviderDiaryBookings1 = dbHelper.cPBookingDiary.GetByLocationId(_providerId1);
            Assert.AreEqual(1, careProviderDiaryBookings1.Count);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName1 + " - No Address", _providerId1);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(availabilityDate.ToString("yyyy"), availabilityDate.ToString("MMMM"), availabilityDate.Day.ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(_systemUserEmploymentContractId1.ToString(), careProviderDiaryBookings1[0].ToString())
                .ValidateDiaryBookingIsPresent(_systemUserEmploymentContractId2.ToString(), careProviderDiaryBookings1[0].ToString());

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName2 + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn(availabilityDay);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartTime("12", "00")
                .SetEndTime("16", "00")
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .SelectBookingTakesPlaceEvery("2 weeks")
                .SelectNextDueToTakePlaceDate(availabilityDate_Year, availabilityDate_Month, availabilityDate_Date)
                .ClickRosteringTab()
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId3)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId4)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            var careProviderBookingSchedules2 = dbHelper.cpBookingSchedule.GetByLocationId(_providerId2);
            Assert.AreEqual(1, careProviderBookingSchedules2.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateSliderLabel_NumberOfStaffText(careProviderBookingSchedules2[0].ToString(), "2/2")
                .MouseHoverDiaryBooking(careProviderBookingSchedules2[0].ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateDueToTakePlaceLabelText(availabilityDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateStaffLabelText("HelpA3 " + currentTimeString + ", " + "HelpA4 " + currentTimeString);

            #region Express Book for Provider

            var _expressBookingCriteriaId2 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId2, commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(21), commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId2, "provider", "P6202B " + currentTimeString);

            #endregion

            dbHelper.userWorkSchedule.DeleteUserWorkSchedule(_userWorkscheduleId4);

            #region Schdeduled job for Express Book

            //get the schedule job id
            scheduleJobId = dbHelper.scheduledJob.GetByPartialName("P6202B " + currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            var expressBookingResultId2 = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(_expressBookingCriteriaId2);
            Assert.AreEqual(1, expressBookingResultId2.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + currentTimeString)
                .OpenRecord(_expressBookingCriteriaId2);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .OpenRecord(expressBookingResultId2[0]);

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoad()
                .ValidateExceptionMessageText("HelpA4 " + currentTimeString + " - " + _systemUserEmploymentContractId4_Title + " is not available at this time. An unassigned booking has been created.");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            var careProviderDiaryBooking2 = dbHelper.cPBookingDiary.GetByLocationId(_providerId2);
            Assert.AreEqual(1, careProviderDiaryBooking2.Count);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName2 + " - No Address", _providerId2)
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(availabilityDate.ToString("yyyy"), availabilityDate.ToString("MMMM"), availabilityDate.Day.ToString())
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(_systemUserEmploymentContractId3.ToString(), careProviderDiaryBooking2[0].ToString())
                .ValidateDiaryBookingIsPresent(_systemUserEmploymentContractId4.ToString(), careProviderDiaryBooking2[0].ToString(), false)
                .ValidateUnassignedDiaryBookingIsPresent(careProviderDiaryBooking2[0].ToString());

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6256

        [TestProperty("JiraIssueID", "ACC-6255")]
        [Description("Automation for step 8 to step 9 from the original test ACC-6201.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        [TestProperty("Screen2", "Express Booking Results")]
        [TestProperty("Screen3", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6201_UITestMethod002()
        {
            var availabilityDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);
            string availabilityDay = availabilityDate.DayOfWeek.ToString();

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role6255" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName1 = "P6255 " + currentTimeString;
            var _providerId1 = commonMethodsDB.CreateProvider(_providerName1, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingTypeBTC4 = commonMethodsDB.CreateCPBookingType("BTC4 ACC-6255A", 4, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, false, false, false, false, false);
            var _bookingTypeBTC4_AssumeStaff = commonMethodsDB.CreateCPBookingType("BTC4 ACC-6255B", 4, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, false, true, false, false, false);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId1, _bookingTypeBTC4, true);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId1, _bookingTypeBTC4_AssumeStaff, false);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "AssociateA1" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "AssociateA1", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            string _staffBName = "AssociateA2" + currentTimeString;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_staffBName, "AssociateA2", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            string _staffCName = "AssociateA3" + currentTimeString;
            var _systemUserId3 = commonMethodsDB.CreateSystemUserRecord(_staffCName, "AssociateA3", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            string _staffDName = "AssociateA4" + currentTimeString;
            var _systemUserId4 = commonMethodsDB.CreateSystemUserRecord(_staffDName, "AssociateA4", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);


            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId2, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId3, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId4, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId2, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId3 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId3, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId4 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId4, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId3_Title = (string)dbHelper.systemUserEmploymentContract.GetByID(_systemUserEmploymentContractId3, "name")["name"];

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingTypeBTC4);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingTypeBTC4);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId3, _bookingTypeBTC4_AssumeStaff);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId4, _bookingTypeBTC4_AssumeStaff);

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

            #region User Work Schedule

            var _user3WorkscheduleId1 = dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId3, _teamId, _recurrencePattern_Every1WeekMondayId, _systemUserEmploymentContractId3, _availabilityTypeId, availabilityDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 2);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId4, _teamId, _recurrencePattern_Every1WeekMondayId, _systemUserEmploymentContractId4, _availabilityTypeId, availabilityDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 2);

            #endregion

            #region CP Scheduling Setup Set Staff Availabilty Check

            dbHelper.cPSchedulingSetup.UpdateCheckStaffAvailability(cPSchedulingSetupId, 1); //No Check

            #endregion

            #region Step 8

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName1 + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn(availabilityDay);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC4 ACC-6255B")
                .SetStartTime("15", "00")
                .SetEndTime("19", "00")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickOnlyShowAvailableStaff()
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId2)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            //createScheduleBookingPopup
            //    .WaitForCreateScheduleBookingPopupClosed();

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId1);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateSliderLabel_NumberOfStaffText(careProviderBookingSchedules[0].ToString(), "2/2")
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateStaffLabelText("AssociateA1 " + currentTimeString + ", " + "AssociateA2 " + currentTimeString);

            #region Express Book for Provider

            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId1, commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(21), commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId1, "provider", "P6255 " + currentTimeString);

            #endregion

            #region Schdeduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(_expressBookingCriteriaId1);
            Assert.AreEqual(0, expressBookingResultId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + currentTimeString)
                .OpenRecord(_expressBookingCriteriaId1);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .ValidateNoRecordMessageVisible(true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            var careProviderDiaryBookings1 = dbHelper.cPBookingDiary.GetByLocationId(_providerId1);
            Assert.AreEqual(1, careProviderDiaryBookings1.Count);

            #endregion

            #region Step 9

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName1 + " - No Address", _providerId1);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(availabilityDate.ToString("yyyy"), availabilityDate.ToString("MMMM"), availabilityDate.Day.ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(_systemUserEmploymentContractId1.ToString(), careProviderDiaryBookings1[0].ToString())
                .ValidateDiaryBookingIsPresent(_systemUserEmploymentContractId2.ToString(), careProviderDiaryBookings1[0].ToString());

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName1 + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .ClickGridColumn(availabilityDay);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartTime("11", "00")
                .SetEndTime("15", "00")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId3)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId4)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId1);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateSliderLabel_NumberOfStaffText(careProviderBookingSchedules[0].ToString(), "2/2")
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateStaffLabelText("AssociateA3 " + currentTimeString + ", " + "AssociateA4 " + currentTimeString);

            dbHelper.cPSchedulingSetup.UpdateCheckStaffAvailability(cPSchedulingSetupId, 4); //Check and Offer Create

            #region Express Book for Provider

            var _expressBookingCriteriaId2 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId1, commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(21), commonMethodsHelper.GetCurrentDateWithoutCulture(), _systemUserEmploymentContractId3, "systemuseremploymentcontract", _systemUserEmploymentContractId3_Title);

            #endregion

            dbHelper.userWorkSchedule.DeleteUserWorkSchedule(_user3WorkscheduleId1);

            #region Schdeduled job for Express Book

            //get the schedule job id
            scheduleJobId = dbHelper.scheduledJob.GetByPartialName(_systemUserEmploymentContractId3_Title).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            var expressBookingResultId2 = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(_expressBookingCriteriaId2);
            Assert.AreEqual(1, expressBookingResultId2.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + currentTimeString)
                .OpenRecord(_expressBookingCriteriaId2);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .OpenRecord(expressBookingResultId2[0]);

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoad()
                .ValidateExpressBookingFailureReasonSelectedText("Staff Unavailable")
                .ValidateExceptionMessageText("AssociateA3 " + currentTimeString + " - " + _systemUserEmploymentContractId3_Title + " is not available at this time. An unassigned booking has been created.");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            var careProviderDiaryBooking2 = dbHelper.cPBookingDiary.GetByLocationId(_providerId1);
            Assert.AreEqual(2, careProviderDiaryBooking2.Count);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName1 + " - No Address", _providerId1);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(availabilityDate.ToString("yyyy"), availabilityDate.ToString("MMMM"), availabilityDate.Day.ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(_systemUserEmploymentContractId3.ToString(), careProviderDiaryBooking2[0].ToString(), false)
                .ValidateDiaryBookingIsPresent(_systemUserEmploymentContractId4.ToString(), careProviderDiaryBooking2[0].ToString())
                .ValidateUnassignedDiaryBookingIsPresent(careProviderDiaryBooking2[0].ToString());

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6293

        [TestProperty("JiraIssueID", "ACC-6300")]
        [Description("Automation for step 10 from the original test ACC-6201.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        [TestProperty("Screen2", "Express Booking Results")]
        [TestProperty("Screen3", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6201_UITestMethod003()
        {

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            dbHelper.availabilityTypes.UpdateDiaryBookingsValidityId(_hourlyOvertime_availabilityTypeId, 2);

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role6300" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName1 = "P6300 " + currentTimeString;
            var _providerId1 = commonMethodsDB.CreateProvider(_providerName1, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingTypeBTC2 = commonMethodsDB.CreateCPBookingType("BTC ACC-6300", 2, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId1, _bookingTypeBTC2, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "AssociateB1" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "AssociateB1", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            string _staffBName = "AssociateB2" + currentTimeString;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_staffBName, "AssociateB2", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId2, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId2, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId1_Title = (string)dbHelper.systemUserEmploymentContract.GetByID(_systemUserEmploymentContractId1, "name")["name"];

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingTypeBTC2);

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

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _hourlyOvertime_availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId2, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Step 10

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName1 + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartTime("15", "00")
                .SetEndTime("19", "00")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId2)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId1);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateSliderLabel_NumberOfStaffText(careProviderBookingSchedules[0].ToString(), "2/2")
                .MouseHoverDiaryBooking(careProviderBookingSchedules[0].ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateStaffLabelText("AssociateB1 " + currentTimeString + ", " + "AssociateB2 " + currentTimeString);

            #region Express Book for Provider

            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId1, commonMethodsHelper.GetThisWeekFirstMonday(), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(6), commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId1, "provider", "P6255b " + currentTimeString);

            #endregion

            #region Schdeduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

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
                .ClickResultsTab();

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .ValidateNoRecordMessageVisible(false)
                .OpenRecord(expressBookingResultId[0]);

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoad()
                .ValidateExpressBookingFailureReasonSelectedText("Staff Unavailable")
                .ValidateExceptionMessageText("AssociateB1 " + currentTimeString + " - " + _systemUserEmploymentContractId1_Title + " is not available at this time. An unassigned booking has been created.");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            var careProviderDiaryBookings1 = dbHelper.cPBookingDiary.GetByLocationId(_providerId1);
            Assert.AreEqual(1, careProviderDiaryBookings1.Count);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName1 + " - No Address");

            System.Threading.Thread.Sleep(1000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateUnassignedDiaryBookingIsPresent(careProviderDiaryBookings1[0].ToString())
                .ValidateDiaryBookingIsPresent(_systemUserEmploymentContractId1.ToString(), careProviderDiaryBookings1[0].ToString(), false)
                .ValidateDiaryBookingIsPresent(_systemUserEmploymentContractId2.ToString(), careProviderDiaryBookings1[0].ToString());

            dbHelper.availabilityTypes.UpdateDiaryBookingsValidityId(_hourlyOvertime_availabilityTypeId, 1);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6311")]
        [Description("Automation for step 11 from the original test ACC-6201.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        [TestProperty("Screen2", "Express Booking Results")]
        [TestProperty("Screen3", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6201_UITestMethod004()
        {

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role6311" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName1 = "P6311 " + currentTimeString;
            var _providerId1 = commonMethodsDB.CreateProvider(_providerName1, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingTypeBTC2 = commonMethodsDB.CreateCPBookingType("BTC ACC-6311", 2, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId1, _bookingTypeBTC2, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "AssociateC1" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "AssociateC1", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingTypeBTC2);

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

            #region User Work Schedule
            var availabilityDate = commonMethodsHelper.GetDatePartWithoutCulture();
            string availabilityDay = availabilityDate.DayOfWeek.ToString();

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId, availabilityDate, new TimeSpan(0, 0, 0), new TimeSpan(10, 0, 0), 1);
            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _hourlyOvertime_availabilityTypeId, availabilityDate, new TimeSpan(10, 0, 0), new TimeSpan(14, 0, 0), 1);

            #endregion

            #region Step 11

            loginPage
            .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName1 + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay(availabilityDay)
                .SetStartTime("06", "00")
                .SetEndTime("10", "00")
                .SetEndDay(availabilityDay)
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId1);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId1 = careProviderBookingSchedules[0];

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateSliderLabel_NumberOfStaffText(cpBookingScheduleId1.ToString(), "1/1")
                .MouseHoverDiaryBooking(cpBookingScheduleId1.ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateTimeLabelText(availabilityDay + " 06:00 - 10:00")
                .ValidateStaffLabelText("AssociateC1 " + currentTimeString);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay(availabilityDay)
                .SetStartTime("10", "00")
                .SetEndTime("14", "00")
                .SetEndDay(availabilityDay)
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId1);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            var cpBookingScheduleId2 = careProviderBookingSchedules[0];

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateSliderLabel_NumberOfStaffText(cpBookingScheduleId2.ToString(), "1/1")
                .MouseHoverDiaryBooking(cpBookingScheduleId2.ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateTimeLabelText("10:00 - 14:00")
                .ValidateStaffLabelText("AssociateC1 " + currentTimeString);

            #region Express Book for Provider

            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId1, commonMethodsHelper.GetThisWeekFirstMonday(), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(6), commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId1, "provider", "P6255b " + currentTimeString);

            #endregion

            #region Schdeduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(_expressBookingCriteriaId1);
            Assert.AreEqual(0, expressBookingResultId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + currentTimeString)
                .OpenRecord(_expressBookingCriteriaId1);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .ValidateNoRecordMessageVisible(true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            var careProviderDiaryBookings1 = dbHelper.cPBookingDiary.GetByLocationId(_providerId1);
            Assert.AreEqual(2, careProviderDiaryBookings1.Count);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName1 + " - No Address");

            System.Threading.Thread.Sleep(1000);

            var cpBookingDiaryStaffId = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(careProviderDiaryBookings1[0]);
            Assert.AreEqual(1, cpBookingDiaryStaffId.Count);

            //6am booking should have staff
            var diaryBookingId = dbHelper.cPBookingDiary.GetByBookingScheduleAndPlannedStartTime(cpBookingScheduleId1).FirstOrDefault();
            var bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookingId);
            Assert.AreEqual(1, bookingDiaryStaffRecords.Count);
            var fields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUserEmploymentContractId1.ToString(), fields["systemuseremploymentcontractid"].ToString());

            //10am booking should have staff
            var diaryBookingId2 = dbHelper.cPBookingDiary.GetByBookingScheduleAndPlannedStartTime(cpBookingScheduleId2).FirstOrDefault();
            bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookingId2);
            Assert.AreEqual(1, bookingDiaryStaffRecords.Count);
            fields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUserEmploymentContractId1.ToString(), fields["systemuseremploymentcontractid"].ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateUnassignedDiaryBookingIsNotPresent(diaryBookingId.ToString())
                .ValidateUnassignedDiaryBookingIsNotPresent(diaryBookingId2.ToString())
                .ValidateDiaryBookingIsPresent(_systemUserEmploymentContractId1.ToString(), diaryBookingId.ToString())
                .ValidateDiaryBookingIsPresent(_systemUserEmploymentContractId1.ToString(), diaryBookingId2.ToString());

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-6335")]
        [Description("Automation for step 13 from the original test ACC-6201.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        [TestProperty("Screen2", "Express Booking Results")]
        [TestProperty("Screen3", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6201_UITestMethod005()
        {

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            dbHelper.availabilityTypes.UpdateDiaryBookingsValidityId(_hourlyOvertime_availabilityTypeId, 2);

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role6311" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName1 = "P6335 " + currentTimeString;
            var _providerId1 = commonMethodsDB.CreateProvider(_providerName1, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingTypeBTC2 = commonMethodsDB.CreateCPBookingType("BTC ACC-6335", 2, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId1, _bookingTypeBTC2, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "AssociateD1" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "AssociateD1", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            string _staffBName = "AssociateD2" + currentTimeString;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_staffBName, "AssociateD2", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId2, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId2, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);
            var _systemUserEmploymentContractId1_Title = (string)dbHelper.systemUserEmploymentContract.GetByID(_systemUserEmploymentContractId1, "name")["name"];
            var _systemUserEmploymentContractId2_Title = (string)dbHelper.systemUserEmploymentContract.GetByID(_systemUserEmploymentContractId2, "name")["name"];

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingTypeBTC2);

            #endregion

            #region Recurrence Patterns

            _recurrencePattern_Every2WeeksMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 2 week(s) on monday").First();
            _recurrencePattern_Every2WeeksTuesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 2 week(s) on tuesday").First();
            _recurrencePattern_Every2WeeksWednesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 2 week(s) on wednesday").First();
            _recurrencePattern_Every2WeeksThursdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 2 week(s) on thursday").First();
            _recurrencePattern_Every2WeeksFridayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 2 week(s) on friday").First();
            _recurrencePattern_Every2WeeksSaturdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 2 week(s) on saturday").First();
            _recurrencePattern_Every2WeeksSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 2 week(s) on sunday").First();

            #endregion

            #region User Work Schedule
            var nextMonday_Date = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);
            var nextDueDate = commonMethodsHelper.GetDatePartWithoutCulture().AddDays(7);
            string availabilityDay = nextDueDate.DayOfWeek.ToString();
            string nextDueDate_Year = nextDueDate.Year.ToString();
            string nextDueDate_Month = nextDueDate.ToString("MMMM");
            string nextDueDate_Date = nextDueDate.Day.ToString();

            var nextDueDate2 = commonMethodsHelper.GetDatePartWithoutCulture().AddDays(2);
            string nextDueDate2_Year = nextDueDate2.Year.ToString();
            string nextDueDate2_Month = nextDueDate2.ToString("MMMM");
            string nextDueDate2_Date = nextDueDate2.Day.ToString();

            CreateUserWorkScheduleForFrequency(_systemUserId1, _teamId, 1, _systemUserEmploymentContractId1, _availabilityTypeId, commonMethodsHelper.GetDatePartWithoutCulture(), new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1);
            CreateUserWorkScheduleForFrequency(_systemUserId2, _teamId, 2, _systemUserEmploymentContractId2, _hourlyOvertime_availabilityTypeId, nextMonday_Date, new TimeSpan(10, 0, 0), new TimeSpan(14, 0, 0), 2);

            #endregion

            #region Step 13

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName1 + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay(availabilityDay)
                .SetStartTime("10", "00")
                .SetEndTime("14", "00")
                .SetEndDay(availabilityDay)
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .SelectBookingTakesPlaceEvery("2 weeks")
                .SelectNextDueToTakePlaceDate(nextDueDate_Year, nextDueDate_Month, nextDueDate_Date)
                .ClickRosteringTab()
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .VerifyStaffRecordIsDisplayed(_systemUserEmploymentContractId1, false)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId2)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId1);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId1 = careProviderBookingSchedules[0];

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateSliderLabel_NumberOfStaffText(cpBookingScheduleId1.ToString(), "1/1")
                .MouseHoverDiaryBooking(cpBookingScheduleId1.ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateTimeLabelText("10:00 - 14:00")
                .ValidateStaffLabelText("AssociateD2 " + currentTimeString);

            #region Express Book for Provider

            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId1, commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(13), commonMethodsHelper.GetCurrentDateWithoutCulture(), _systemUserEmploymentContractId2, "systemuseremploymentcontract", _systemUserEmploymentContractId2_Title);

            #endregion

            #region Schdeduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

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
                .ClickResultsTab();

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .ValidateNoRecordMessageVisible(false)
                .OpenRecord(expressBookingResultId[0]);

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoad()
                .ValidateExpressBookingFailureReasonSelectedText("Staff Unavailable")
                .ValidateExceptionMessageText("AssociateD2 " + currentTimeString + " - " + _systemUserEmploymentContractId2_Title + " is not available at this time. An unassigned booking has been created.");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            var careProviderDiaryBookings1 = dbHelper.cPBookingDiary.GetByLocationId(_providerId1);
            Assert.AreEqual(1, careProviderDiaryBookings1.Count);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName1 + " - No Address", _providerId1);

            System.Threading.Thread.Sleep(1000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(nextDueDate.ToString("yyyy"), nextDueDate.ToString("MMMM"), nextDueDate.Day.ToString());

            System.Threading.Thread.Sleep(1000);

            var cpBookingDiaryStaffId = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(careProviderDiaryBookings1[0]);
            Assert.AreEqual(1, cpBookingDiaryStaffId.Count);

            //10am booking should not have staff
            var diaryBookingId = dbHelper.cPBookingDiary.GetByBookingScheduleAndPlannedStartTime(cpBookingScheduleId1).FirstOrDefault();
            var bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookingId);
            Assert.AreEqual(1, bookingDiaryStaffRecords.Count);
            var fields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.IsFalse(fields.ContainsKey("systemuseremploymentcontractid"));

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateUnassignedDiaryBookingIsPresent(diaryBookingId.ToString());

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName1 + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            var nextDueDate2_Day = nextDueDate2.DayOfWeek.ToString();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartDay(nextDueDate2_Day)
                .SetStartTime("06", "00")
                .SetEndTime("10", "00")
                .SetEndDay(nextDueDate2_Day)
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .SelectBookingTakesPlaceEvery("2 weeks")
                .SelectNextDueToTakePlaceDate(nextDueDate2_Year, nextDueDate2_Month, nextDueDate2_Date)
                .ClickRosteringTab()
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .VerifyStaffRecordIsDisplayed(_systemUserEmploymentContractId2, false)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId1);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId2 = careProviderBookingSchedules[0];

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateSliderLabel_NumberOfStaffText(cpBookingScheduleId2.ToString(), "1/1")
                .MouseHoverDiaryBooking(cpBookingScheduleId2.ToString());

            System.Threading.Thread.Sleep(1500);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateTimeLabelText("06:00 - 10:00")
                .ValidateStaffLabelText("AssociateD1 " + currentTimeString);

            #region Express Book for Provider

            var _expressBookingCriteriaId2 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId1, commonMethodsHelper.GetWeekFirstMonday(nextDueDate2), commonMethodsHelper.GetWeekFirstMonday(nextDueDate2).AddDays(6), commonMethodsHelper.GetCurrentDateWithoutCulture(), _systemUserEmploymentContractId1, "systemuseremploymentcontract", _systemUserEmploymentContractId1_Title);

            #endregion

            #region Schdeduled job for Express Book

            //get the schedule job id
            scheduleJobId = dbHelper.scheduledJob.GetByPartialName(_systemUserEmploymentContractId1_Title).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            var expressBookingResultId2 = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(_expressBookingCriteriaId2);
            Assert.AreEqual(0, expressBookingResultId2.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + currentTimeString)
                .OpenRecord(_expressBookingCriteriaId2);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .ValidateNoRecordMessageVisible(true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            careProviderDiaryBookings1 = dbHelper.cPBookingDiary.GetByLocationId(_providerId1);
            Assert.AreEqual(2, careProviderDiaryBookings1.Count);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName1 + " - No Address", _providerId1);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(nextDueDate2.ToString("yyyy"), nextDueDate2.ToString("MMMM"), nextDueDate2.Day.ToString());

            System.Threading.Thread.Sleep(1000);

            cpBookingDiaryStaffId = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(careProviderDiaryBookings1[0]);
            Assert.AreEqual(1, cpBookingDiaryStaffId.Count);

            //6am booking should have staff
            var diaryBookingId2 = dbHelper.cPBookingDiary.GetByBookingScheduleAndPlannedStartTime(cpBookingScheduleId2).FirstOrDefault();
            bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookingId2);
            Assert.AreEqual(1, bookingDiaryStaffRecords.Count);
            fields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUserEmploymentContractId1.ToString(), fields["systemuseremploymentcontractid"].ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(_systemUserEmploymentContractId1.ToString(), diaryBookingId2.ToString());

        }

        #endregion

        #endregion

    }
}

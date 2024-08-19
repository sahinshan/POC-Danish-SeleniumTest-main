using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.ProviderDiary
{
    [TestClass]
    public class ProviderDiary_UITestCases : FunctionalTest
    {
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private string _systemUsername;
        private string _providerName;
        private Guid _providerId;
        private TimeZone _localZone;
        private string _tenantName;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");

        internal Guid _recurrencePattern_Every1WeekMondayId;
        internal Guid _recurrencePattern_Every1WeekTuesdayId;
        internal Guid _recurrencePattern_Every1WeekWednesdayId;
        internal Guid _recurrencePattern_Every1WeekThursdayId;
        internal Guid _recurrencePattern_Every1WeekFridayId;
        internal Guid _recurrencePattern_Every1WeekSaturdayId;
        internal Guid _recurrencePattern_Every1WeekSundayId;

        [TestInitialize()]
        public void TestsSetupMethod()
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
                var _defaultSystemUserFullName = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(defaultSystemUserId, "fullname")["fullname"];
                _localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, _localZone.StandardName);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("Provider Diary");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("Provider Diary T1", null, _businessUnitId, "907643", "ProviderDiaryT1@careworkstempmail.com", "ProviderDiary T1", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region Environment Name

                var EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                _tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

                #region Login System User

                _systemUsername = "ProviderDiaryUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "ProviderDiary", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
                dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, _localZone.StandardName);
                var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
                dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, thisWeekMonday);

                #endregion

                #region Provider 1

                _providerName = "Diary Provider_" + _currentDateSuffix;
                _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        internal void CreateUserWorkSchedule(Guid UserId, Guid TeamId, Guid SystemUserEmploymentContractId, Guid availabilityTypeId)
        {
            #region Recurrence Patterns

            _recurrencePattern_Every1WeekMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on monday").First();
            _recurrencePattern_Every1WeekTuesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on tuesday").First();
            _recurrencePattern_Every1WeekWednesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on wednesday").First();
            _recurrencePattern_Every1WeekThursdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on thursday").First();
            _recurrencePattern_Every1WeekFridayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on friday").First();
            _recurrencePattern_Every1WeekSaturdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on saturday").First();
            _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();

            #endregion

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

        internal void CreateUserWorkSchedule(Guid UserId, Guid TeamId, Guid SystemUserEmploymentContractId, Guid availabilityTypeId, DateTime DateOfWeek)
        {
            #region Recurrence Patterns

            _recurrencePattern_Every1WeekMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on monday").First();
            _recurrencePattern_Every1WeekTuesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on tuesday").First();
            _recurrencePattern_Every1WeekWednesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on wednesday").First();
            _recurrencePattern_Every1WeekThursdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on thursday").First();
            _recurrencePattern_Every1WeekFridayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on friday").First();
            _recurrencePattern_Every1WeekSaturdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on saturday").First();
            _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();

            #endregion

            var workScheduleDate = DateOfWeek;

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


        #region https://advancedcsg.atlassian.net/browse/ACC-6063

        [TestProperty("JiraIssueID", "ACC-6119")]
        [Description("Step(s) 1 to 11 from the original jira test ACC-6062")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderDiary_UITestMethod001()
        {
            #region Booking Type

            var _bookingTypeName = "BTC ACC-6119";
            var _bookingTypeId = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId, false);

            #endregion

            #region Step 1 to 4

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")
                .ValidateLocation_ProviderMandatoryFieldVisibility(true)
                .ValidateBookingTypeMandatoryFieldVisibility(true)
                .ValidateStartDateMandatoryFieldVisibility(true)
                .ValidateStartTimeMandatoryFieldVisibility(true)
                .ValidateEndDateMandatoryFieldVisibility(true)
                .ValidateEndTimeMandatoryFieldVisibility(true)
                .ValidateCommentsMandatoryFieldVisibility(false)
                .ClickCreateBooking();

            #endregion

            #region Step 5

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("You haven't filled in all required fields.")
                .ClickDismissButton_DynamicDialogue();

            #endregion

            #region Step 6 & 7

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .SelectBookingType(_bookingTypeName)
                .InsertStartTime("14", "00")
                .InsertEndTime("12", "00")
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("Invalid start / end time combination")
                .ClickDismissButton_DynamicDialogue();

            #endregion

            #region Step 8 & 9

            var startYear = DateTime.Now.Date.Year.ToString();
            var startMonth = DateTime.Now.Date.ToString("MMMM");
            var startDay = DateTime.Now.Date.Day.ToString();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .SelectStartDate(startYear, startMonth, startDay)
                .SelectEndDate(startYear, startMonth, startDay)
                .InsertStartTime("11", "00")
                .InsertEndTime("11", "00")
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1000);

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("Invalid start / end time combination")
                .ClickDismissButton_DynamicDialogue();

            #endregion

            #region Step 10

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .InsertStartTime("11", "00")
                .InsertEndTime("12", "20")
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1000);

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("The booking must be a multiple of 15 minutes.")
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .InsertStartTime("11", "00")
                .InsertEndTime("12", "00")
                .InsertTextInCommentsTextArea("Test")
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            var careProviderDiaryBooking = dbHelper.cPBookingDiary.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderDiaryBooking.Count);

            #endregion

            #region Step 11

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(careProviderDiaryBooking[0].ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateBookingTypeDropDownText(_bookingTypeName)
                .ValidateStartDate(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateStartTime("11:00")
                .ValidateEndDate(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndTime("12:00");

            createDiaryBookingPopup
                .InsertStartTime("13", "00")
                .InsertEndTime("14", "00")
                .InsertTextInCommentsTextArea("Update Record")
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1000);

            careProviderDiaryBooking = dbHelper.cPBookingDiary.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderDiaryBooking.Count);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(careProviderDiaryBooking[0].ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateBookingTypeDropDownText(_bookingTypeName)
                .ValidateStartDate(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateStartTime("13:00")
                .ValidateEndDate(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndTime("14:00");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-6120")]
        [Description("Step(s) 12 to 16 from the original jira test ACC-6062")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderDiary_UITestMethod002()
        {
            #region Booking Type

            var _bookingTypeName = "BTC ACC-6120";
            var _bookingTypeId = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId, false);

            #endregion

            #region Care Provider Booking Diary

            var todayDate = DateTime.UtcNow.Date;
            var _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "title", _bookingTypeId, _providerId, todayDate, new TimeSpan(12, 0, 0), todayDate, new TimeSpan(13, 0, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "Unallocated", _cpdiarybookingid, null, null);

            #endregion

            #region Step 12

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(_cpdiarybookingid.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateBookingTypeDropDownText(_bookingTypeName)
                .ValidateStartDate(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateStartTime("12:00")
                .ValidateEndDate(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndTime("13:00");

            createDiaryBookingPopup
                .InsertStartTime("14", "00")
                .InsertEndTime("15", "00")
                .InsertTextInCommentsTextArea("Update Record")
                .ClickAddUnassignedStaff()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1000);
            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(_cpdiarybookingid.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateBookingTypeDropDownText(_bookingTypeName)
                .ValidateStartDate(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateStartTime("14:00")
                .ValidateEndDate(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndTime("15:00");

            createDiaryBookingPopup
                .ValidateHistoryTabIsVisible()
                .ClickHistoryTab()
                .WaitForHistoryTabToLoad()
                .ValidateHeaderOnHistoryTab("Unassigned " + _bookingTypeName + " - diary booking starting at 14:00 on " + DateTime.Now.Date.ToString("dd'/'MM'/'yyyy") + " at " + _providerName + ".")
                .ExpandHistoryDetailSection(1)
                .ExpandSpecificField("Comments")
                .ValidateExpandedFieldIsVisible("Comments", true)
                .ValidateCurrentAndPreviousValue("Comments", "Update Record", "");

            createDiaryBookingPopup
                .ExpandSpecificField("End Time")
                .ValidateExpandedFieldIsVisible("End Time", true)
                .ValidateCurrentAndPreviousValue("End Time", "15:00", "13:00");

            createDiaryBookingPopup
                .ExpandSpecificField("Start Time")
                .ValidateExpandedFieldIsVisible("Start Time", true)
                .ValidateCurrentAndPreviousValue("Start Time", "14:00", "12:00")
                .ClickOnCloseButton();

            #endregion

            #region Step 14

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Care Provider Diary Bookings")
                .SelectFilter("1", "Location / Provider")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_providerName).TapSearchButton().SelectResultElement(_providerId.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_cpdiarybookingid.ToString())
                .OpenRecord(_cpdiarybookingid.ToString());

            #endregion

            #region Step 15

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateBookingTypeDropDownText(_bookingTypeName)
                .ValidateStartDate(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateStartTime("14:00")
                .ValidateEndDate(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndTime("15:00")

                .InsertStartTime("10", "00")
                .InsertEndTime("12", "00")
                .ClickCreateBooking();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(_cpdiarybookingid.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateBookingTypeDropDownText(_bookingTypeName)
                .ValidateStartDate(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateStartTime("10:00")
                .ValidateEndDate(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndTime("12:00");

            #endregion

            #region Step 13

            createDiaryBookingPopup
                .ClickOnDeleteButton()
                .WaitForDeleteBookingDynamicDialogueToLoad()
                .SelectReasonForDeletePicklistOption("Added in error")
                .InsertTextInComments_DeleteBookingDynamicDialogue("Delete Booking ACC-6062.")
                .ClickDeleteButton_DeleteBookingDynamicDialogue();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupClosed();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateBookingStatus("Booking deleted");

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6289

        [TestProperty("JiraIssueID", "ACC-6383")]
        [Description("Step(s) 1 to 12 from the original jira test ACC-6288")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderDiaryBooking_ACC_6288_UITestMethod001()
        {
            #region Update Delete Reason Required Schedule to True

            var cPSchedulingSetupId = dbHelper.cPSchedulingSetup.GetAllActiveRecords().FirstOrDefault();
            dbHelper.cPSchedulingSetup.UpdateDeleteReasonRequiredDiary(cPSchedulingSetupId, true);

            #endregion

            #region Booking Type

            var _bookingTypeName = "BTC ACC-6288 1";
            var _bookingTypeId = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 2, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6288", "6288", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1);

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId);

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region Care Provider Booking Diary

            var todayDate = DateTime.UtcNow.Date;
            var _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "title", _bookingTypeId, _providerId, todayDate, new TimeSpan(12, 0, 0), todayDate, new TimeSpan(13, 00, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", _cpdiarybookingid, _systemUserEmploymentContractId, _systemUserId);

            #endregion

            #region Step 1 & 2

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSchedulingSetupPage();

            careProviderSchedulingSetupPage
                .WaitForCareProviderSchedulingSetupPageToLoad()
                .OpenRecord(cPSchedulingSetupId);

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("cpschedulingsetup")
                .ClickOnExpandIcon();

            careProviderSchedulingSetupRecordPage
                .WaitForCareProviderSchedulingSetupRecordPageToLoad()
                .ValidateDeleteReasonRequiredDiary_OptionIsCheckedOrNot(true);

            #endregion

            #region Step 3 to 6

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(_cpdiarybookingid.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateDeleteButtonIsPresent(true);

            #endregion

            #region Step 7

            createDiaryBookingPopup
                .ClickOnDeleteButton();

            createDiaryBookingPopup
                .WaitForDeleteBookingDynamicDialogueToLoad()
                .ValidateReasonForDeleteMandatoryIconVisible(true)
                .ValidateCommentsMandatoryIconVisible_DeleteBookingDialouge(false)
                .ValidateSelectedReasonForDeletePickListOption("Select");

            #endregion

            #region Step 8

            createDiaryBookingPopup
                .SelectReasonForDeletePicklistOption("Added in error")
                .ValidateSelectedReasonForDeletePickListOption("Added in error");

            #endregion

            #region Step 9

            createDiaryBookingPopup
                .ClickCancelButton_DeleteBookingDynamicDialogue()
                .WaitForDeleteBookingDynamicDialoguePopupClosed();

            #endregion

            #region Step 10 & 11

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ClickOnDeleteButton();

            createDiaryBookingPopup
                .WaitForDeleteBookingDynamicDialogueToLoad()
                .SelectReasonForDeletePicklistOption("Added in error")
                .InsertTextInComments_DeleteBookingDynamicDialogue("Deleting Booking")
                .ClickDeleteButton_DeleteBookingDynamicDialogue();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupClosed();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateBookingStatus("Booking deleted");

            #endregion

            #region Step 12

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(_cpdiarybookingid.ToString(), false);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6290

        [TestProperty("JiraIssueID", "ACC-6384")]
        [Description("Step(s) 13 to 17 from the original jira test ACC-6288")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6288_UITestMethod002()
        {
            #region Booking Type

            var _bookingTypeName = "BTC ACC-6288 1";
            var _bookingTypeId = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 2, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6288", "6288", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1);

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId);

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region Care Provider Booking Diary

            var todayDate = DateTime.UtcNow.Date;
            var _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "title", _bookingTypeId, _providerId, todayDate, new TimeSpan(12, 0, 0), todayDate, new TimeSpan(13, 00, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", _cpdiarybookingid, _systemUserEmploymentContractId, _systemUserId);

            #endregion

            #region Step 13 & 14

            #region Update Delete Reason Required Schedule to False

            var cPSchedulingSetupId = dbHelper.cPSchedulingSetup.GetAllActiveRecords().FirstOrDefault();
            dbHelper.cPSchedulingSetup.UpdateDeleteReasonRequiredDiary(cPSchedulingSetupId, false);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(_cpdiarybookingid.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateDeleteButtonIsPresent()
                .ClickOnDeleteButton();

            createDiaryBookingPopup
                .WaitForDeleteBookingDynamicDialogueToLoad(false)
                .ValidateDeleteAlertMessage("Are you sure you want to delete?")
                .ClickCancelButton_DeleteBookingDynamicDialogue()
                .WaitForDeleteBookingDynamicDialoguePopupClosed(false);

            #endregion

            #region Step 15

            createDiaryBookingPopup
                .ClickOnDeleteButton()
                .WaitForDeleteBookingDynamicDialogueToLoad(false)
                .ValidateDeleteAlertMessage("Are you sure you want to delete?")
                .ClickDeleteButton_DeleteBookingDynamicDialogue()
                .WaitForDeleteBookingDynamicDialoguePopupClosed(false);


            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateBookingStatus("Booking deleted");

            #endregion

            #region Step 16

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(_cpdiarybookingid.ToString(), false);

            #endregion

            #region Step 17

            dbHelper.cPSchedulingSetup.UpdateDeleteReasonRequiredDiary(cPSchedulingSetupId, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSchedulingSetupPage();

            careProviderSchedulingSetupPage
                .WaitForCareProviderSchedulingSetupPageToLoad()
                .OpenRecord(cPSchedulingSetupId);

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("cpschedulingsetup")
                .ClickOnExpandIcon();

            careProviderSchedulingSetupRecordPage
                .WaitForCareProviderSchedulingSetupRecordPageToLoad()
                .ValidateDeleteReasonRequiredDiary_OptionIsCheckedOrNot(true);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-6385")]
        [Description("Step(s) 18 to 19 from the original jira test ACC-6288")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        // Step 18 Failing due to open Bug https://advancedcsg.atlassian.net/browse/ACC-3978 & Step 19 is blocked due to bug
        public void ProviderScheduleBooking_ACC_6288_UITestMethod003()
        {
            #region Booking Type

            var _bookingTypeName = "BTC ACC-6288 1";
            var _bookingTypeId = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 2, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6288", "6288", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1);

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId);

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region Care Provider Booking Diary

            var todayDate = DateTime.UtcNow.Date;
            var _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "title", _bookingTypeId, _providerId, todayDate, new TimeSpan(12, 0, 0), todayDate, new TimeSpan(13, 00, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", _cpdiarybookingid, _systemUserEmploymentContractId, _systemUserId);

            #endregion

            #region Booking Schedule Deletion Reasons

            var _cpBookingDiaryDeletionReasonName = "ACC-6288_Reason_" + _currentDateSuffix;
            var isExists = dbHelper.cpBookingDiaryDeletionReason.GetCPBookingDiaryDeletionReasonByName(_cpBookingDiaryDeletionReasonName).Any();
            if (!isExists)
                dbHelper.cpBookingDiaryDeletionReason.CreateCPBookingDiaryDeletionReason(_teamId, _cpBookingDiaryDeletionReasonName, null, new DateTime(2022, 1, 1), null);

            var _cpBookingDiaryDeletionReasonId = dbHelper.cpBookingDiaryDeletionReason.GetCPBookingDiaryDeletionReasonByName(_cpBookingDiaryDeletionReasonName)[0];

            #endregion

            #region Step 18

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(_cpdiarybookingid.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateDeleteButtonIsPresent()
                .ClickOnDeleteButton();

            createDiaryBookingPopup
                .WaitForDeleteBookingDynamicDialogueToLoad()
                .SelectReasonForDeletePicklistOption(_cpBookingDiaryDeletionReasonName)
                .InsertTextInComments_DeleteBookingDynamicDialogue("Delete Diary Booking.")
                .ClickDeleteButton_DeleteBookingDynamicDialogue();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupClosed();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateBookingStatus("Booking deleted");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Care Provider Diary Bookings")
                .SelectFilter("1", "Booking Diary Deletion Reason")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_cpBookingDiaryDeletionReasonName).TapSearchButton().SelectResultElement(_cpBookingDiaryDeletionReasonId.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()

                .ClickColumnHeader(2)
                .ClickColumnHeader(2)
                .ValidateSearchResultRecordPresent(_cpdiarybookingid.ToString());

            System.Threading.Thread.Sleep(2000);
            advanceSearchPage
                .ResultsPageValidateHeaderCellText(16, "Deleted")
                .ValidateSearchResultRecordCellContent(_cpdiarybookingid.ToString(), 16, "Yes");

            advanceSearchPage
                .OpenRecord(_cpdiarybookingid.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6295

        [TestProperty("JiraIssueID", "ACC-6500")]
        [Description("Step(s) 1 to 4 from the original jira test ACC-6292")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6292_UITestMethod001()
        {
            #region Update Enable Scheduling to False

            dbHelper.provider.UpdateEnableScheduling(_providerId, false);

            #endregion

            #region Booking Type

            var _bookingTypeName = "BTC-1 ACC-6292";
            var _bookingTypeId = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 1, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId, true);

            #endregion

            #region Step 1 & 2

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateProviderIsPresent(_providerName + " - No Address", false);

            #endregion

            #region Step 3

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Care Provider Diary Bookings")
                .ClickDeleteButton()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateNewRecordButton_ResultsPageIsNotPresent(true);

            #endregion

            #region Step 4

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(_providerName, _providerId.ToString())
                .OpenProviderRecord(_providerId.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .TapDetailsTab();

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickEnableSchedulingRadioButton(true)
                .ClickSaveAndCloseButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateProviderIsPresent(_providerName + " - No Address", true)
                .selectProvider(_providerName + " - No Address");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6501")]
        [Description("Step(s) 5 from the original jira test ACC-6292")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6292_UITestMethod002()
        {
            #region Create SystemUser Record

            var _systemUserName = "Diary_Booking_" + _currentDateSuffix;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Diary Booking Contract", "Service User " + _currentDateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, thisWeekMonday);

            #endregion

            #region Booking Type

            var _bookingTypeName = "BTC-1 ACC-6292";
            var _bookingTypeId = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 1, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            var _bookingTypeName2 = "BTC-2 ACC-6292";
            var _bookingTypeId2 = commonMethodsDB.CreateCPBookingType(_bookingTypeName2, 2, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            var _bookingTypeName3 = "BTC-3 ACC-6292";
            var _bookingTypeId3 = commonMethodsDB.CreateCPBookingType(_bookingTypeName3, 3, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId, true);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId2, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId3, false);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6292", "6292", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeId = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId);

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId2).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId2);

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId3).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId3);

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region Care Provider Booking Diary

            var todayDate = DateTime.UtcNow.Date;
            var _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "title", _bookingTypeId, _providerId, todayDate, new TimeSpan(12, 0, 0), todayDate, new TimeSpan(13, 00, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", _cpdiarybookingid, _systemUserEmploymentContractId, _systemUserId);

            #endregion

            #region Step 5

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateProviderIsPresent(_providerName + " - No Address", true)
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(_cpdiarybookingid.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateBookingTypeDropDownText(_bookingTypeName)

                .SelectBookingType(_bookingTypeName2)
                .ClickCreateBooking();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateBookingStatus("Booking edited");

            providerDiaryPage
                .ClickDiaryBooking(_cpdiarybookingid.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateBookingTypeDropDownText(_bookingTypeName2)
                .SelectBookingType(_bookingTypeName3)
                .ClickCreateBooking();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateBookingStatus("Booking edited");

            providerDiaryPage
                .ClickDiaryBooking(_cpdiarybookingid.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateBookingTypeDropDownText(_bookingTypeName3);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6502")]
        [Description("Step(s) 6 from the original jira test ACC-6292")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6292_UITestMethod003()
        {
            #region Booking Type

            DateTime bookingFromDate = DateTime.Now.AddDays(-7);
            DateTime bookingToDate = DateTime.Now.AddDays(7);
            var _bookingTypeName = "BTC ACC-6292_Auto";
            var _bookingTypeId = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 2, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);
            dbHelper.cpBookingType.UpdateValidFrom(_bookingTypeId, bookingFromDate);
            dbHelper.cpBookingType.UpdateValidTo(_bookingTypeId, bookingToDate);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6292", "6292", null, new DateTime(2020, 1, 1), null);

            #endregion


            #region Employment Contract Type - Contracted

            var _employmentContractTypeId = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId);

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region Care Provider Booking Diary

            var todayDate = DateTime.UtcNow.Date;
            var _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "title", _bookingTypeId, _providerId, todayDate, new TimeSpan(12, 0, 0), todayDate, new TimeSpan(13, 00, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", _cpdiarybookingid, _systemUserEmploymentContractId, _systemUserId);

            #endregion

            #region Step 6

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(_cpdiarybookingid.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateBookingTypeDropDownText(_bookingTypeName)
                .ClickOnCloseButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToBookingTypesPage();

            bookingTypesPage
                .WaitForBookingTypesPageToLoad()
                .InsertQuickSearchText(_bookingTypeName)
                .ClickQuickSearchButton()
                .OpenRecord(_bookingTypeId.ToString());

            var validToDate = DateTime.Now.AddDays(-2);
            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad()
                .InsertTextOnValidToDate(validToDate.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(_cpdiarybookingid.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad();

            System.Threading.Thread.Sleep(1000);
            createDiaryBookingPopup
                .ValidateBookingTypeDropDownText(_bookingTypeName + " - Invalid")
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("Please consider updating the booking type " + _bookingTypeName + ". It is now invalid, valid between " + bookingFromDate.ToString("dd'/'MM'/'yyyy") + " - " + validToDate.ToString("dd'/'MM'/'yyyy") + ".")
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("Please consider updating the booking type " + _bookingTypeName + ". It is now invalid, valid between " + bookingFromDate.ToString("dd'/'MM'/'yyyy") + " - " + validToDate.ToString("dd'/'MM'/'yyyy") + ".")
                .ClickSaveButton_DynamicDialogue();

            System.Threading.Thread.Sleep(5000);
            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("The booking type is not valid for the date range of the booking.")
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .ClickOnCloseButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6503")]
        [Description("Step(s) 7 from the original jira test ACC-6292")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6292_UITestMethod004()
        {
            #region Booking Type

            var _bookingTypeName = "BTC ACC-6292_Auto";
            var _bookingTypeId = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 2, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);
            dbHelper.cpBookingType.UpdateValidFrom(_bookingTypeId, null);
            dbHelper.cpBookingType.UpdateValidTo(_bookingTypeId, null);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6292", "6292", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeId = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId);

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region Care Provider Booking Diary

            var todayDate = DateTime.UtcNow.Date;
            var _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "title", _bookingTypeId, _providerId, todayDate, new TimeSpan(12, 0, 0), todayDate, new TimeSpan(13, 00, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", _cpdiarybookingid, _systemUserEmploymentContractId, _systemUserId);

            #endregion

            #region Step 7

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToBookingTypesPage();

            bookingTypesPage
                .WaitForBookingTypesPageToLoad()
                .InsertQuickSearchText(_bookingTypeName)
                .ClickQuickSearchButton()
                .OpenRecord(_bookingTypeId.ToString());

            var validFromDate = DateTime.Now.AddDays(1);
            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad()
                .InsertTextOnValidFromDate(validFromDate.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnValidToDate("")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(_cpdiarybookingid.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad();

            System.Threading.Thread.Sleep(1000);
            createDiaryBookingPopup
                .ValidateBookingTypeDropDownText(_bookingTypeName + " - Invalid")
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("Please consider updating the booking type " + _bookingTypeName + ". It is now invalid, valid from " + validFromDate.ToString("dd'/'MM'/'yyyy") + ".")
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("Please consider updating the booking type " + _bookingTypeName + ". It is now invalid, valid from " + validFromDate.ToString("dd'/'MM'/'yyyy") + ".")
                .ClickSaveButton_DynamicDialogue();

            System.Threading.Thread.Sleep(5000);
            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("The booking type is not valid for the date range of the booking.")
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .ClickOnCloseButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6504")]
        [Description("Step(s) 8 from the original jira test ACC-6292")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6292_UITestMethod005()
        {
            #region Create SystemUser Record

            var _systemUserName = "Diary_Booking_" + _currentDateSuffix;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Diary Booking", "User " + _currentDateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var _systemUserFullName = "Diary Booking User " + _currentDateSuffix;

            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, thisWeekMonday);

            #endregion

            #region Booking Type

            var _bookingTypeName = "BTC ACC-6292_Auto";
            var _bookingTypeId = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 2, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);
            dbHelper.cpBookingType.UpdateValidFrom(_bookingTypeId, null);
            dbHelper.cpBookingType.UpdateValidTo(_bookingTypeId, null);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeName = "CPSRT 6292";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "6292", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeId = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId);

            #endregion

            #region Availability Type

            var _availabilityTypeName = "Salaried/Contracted";
            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName(_availabilityTypeName).First();

            #endregion

            #region User Work Schedule

            var _recurrencePatternId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").FirstOrDefault();
            var workScheduleDate = DateTime.UtcNow.Date.AddDays(-1);
            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule("Ad-Hoc: " + _systemUserFullName + " - " + _careProviderStaffRoleTypeName + ", " + _availabilityTypeName + "", _systemUserId, _teamId, _recurrencePatternId, _systemUserEmploymentContractId, _availabilityTypeId, workScheduleDate, workScheduleDate, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), null, true);

            #endregion

            #region Step 8

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToBookingTypesPage();

            bookingTypesPage
                .WaitForBookingTypesPageToLoad()
                .InsertQuickSearchText(_bookingTypeName)
                .ClickQuickSearchButton()
                .OpenRecord(_bookingTypeId.ToString());

            var validFromDate = DateTime.Now.Date;
            var validToDate = DateTime.Now.AddDays(2);
            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad()
                .InsertTextOnValidFromDate(validFromDate.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnValidToDate(validToDate.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            var startYear = DateTime.Now.AddDays(-1).Year.ToString();
            var startMonth = DateTime.Now.AddDays(-1).ToString("MMMM");
            var startDay = DateTime.Now.AddDays(-1).Day.ToString();

            var endYear = DateTime.Now.AddDays(1).Year.ToString();
            var endMonth = DateTime.Now.AddDays(1).ToString("MMMM");
            var endDay = DateTime.Now.AddDays(1).Day.ToString();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")

                .SelectBookingType(_bookingTypeName)
                .SelectStartDate(startYear, startMonth, startDay)
                .SelectEndDate(endYear, endMonth, endDay)
                .InsertStartTime("12", "00")
                .InsertEndTime("14", "00")

                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(_systemUserFullName)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("The booking type is not valid for the date range of the booking.")
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .ClickOnCloseButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6505")]
        [Description("Step(s) 9 from the original jira test ACC-6292")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6292_UITestMethod006()
        {
            #region Create SystemUser Record

            var _systemUserName = "Diary_Booking_" + _currentDateSuffix;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Diary Booking", "User " + _currentDateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var _systemUserFullName = "Diary Booking User " + _currentDateSuffix;

            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, thisWeekMonday);

            #endregion

            #region Booking Type

            var _bookingTypeName = "BTC ACC-6292_Auto";
            var _bookingTypeId = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 2, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);
            dbHelper.cpBookingType.UpdateValidFrom(_bookingTypeId, null);
            dbHelper.cpBookingType.UpdateValidTo(_bookingTypeId, null);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeName = "CPSRT 6292";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "6292", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeId = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId);

            #endregion

            #region Availability Type

            var _availabilityTypeName = "Salaried/Contracted";
            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName(_availabilityTypeName).First();

            #endregion

            #region User Work Schedule

            var _recurrencePatternId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").FirstOrDefault();
            var workScheduleDate = DateTime.UtcNow.Date.AddDays(-1);
            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule("Ad-Hoc: " + _systemUserFullName + " - " + _careProviderStaffRoleTypeName + ", " + _availabilityTypeName + "", _systemUserId, _teamId, _recurrencePatternId, _systemUserEmploymentContractId, _availabilityTypeId, workScheduleDate, workScheduleDate, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), null, true);

            #endregion

            #region Step 9

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToBookingTypesPage();

            bookingTypesPage
                .WaitForBookingTypesPageToLoad()
                .InsertQuickSearchText(_bookingTypeName)
                .ClickQuickSearchButton()
                .OpenRecord(_bookingTypeId.ToString());

            var validToDate = DateTime.Now.AddDays(1);
            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad()
                .InsertTextOnValidToDate(validToDate.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            var startYear = DateTime.Now.AddDays(-1).Year.ToString();
            var startMonth = DateTime.Now.AddDays(-1).ToString("MMMM");
            var startDay = DateTime.Now.AddDays(-1).Day.ToString();

            var endYear = DateTime.Now.AddDays(2).Year.ToString();
            var endMonth = DateTime.Now.AddDays(2).ToString("MMMM");
            var endDay = DateTime.Now.AddDays(2).Day.ToString();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")

                .WaitForCreateDiaryBookingPopupToLoad()
                .SelectBookingType(_bookingTypeName)
                .SelectStartDate(startYear, startMonth, startDay)
                .SelectEndDate(endYear, endMonth, endDay)
                .InsertStartTime("12", "00")
                .InsertEndTime("14", "00")

                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(_systemUserFullName)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("The booking type is not valid for the date range of the booking.")
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .ClickOnCloseButton();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6456

        [TestProperty("JiraIssueID", "ACC-6506")]
        [Description("Step(s) 10 from the original jira test ACC-6292")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6292_UITestMethod007()
        {
            #region Create SystemUser Record

            var _systemUserName = "Diary_Booking_" + _currentDateSuffix;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Diary Booking", "User " + _currentDateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var _systemUserFullName = "Diary Booking User " + _currentDateSuffix;

            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, thisWeekMonday);

            #endregion

            #region Booking Type

            var _bookingTypeName = "BTC ACC-6292_Auto";
            var _bookingTypeId = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 2, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);
            dbHelper.cpBookingType.UpdateValidFrom(_bookingTypeId, null);
            dbHelper.cpBookingType.UpdateValidTo(_bookingTypeId, null);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeName = "CPSRT 6292";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "6292", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeId = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId);

            #endregion

            #region Availability Type

            var _availabilityTypeName = "Salaried/Contracted";
            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName(_availabilityTypeName).First();

            #endregion

            #region User Work Schedule

            var _recurrencePatternId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").FirstOrDefault();
            var workScheduleDate = DateTime.UtcNow.Date.AddDays(-1);
            var workScheduleDate1 = DateTime.UtcNow.Date.AddDays(-2);
            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule("Ad-Hoc: " + _systemUserFullName + " - " + _careProviderStaffRoleTypeName + ", " + _availabilityTypeName + "", _systemUserId, _teamId, _recurrencePatternId, _systemUserEmploymentContractId, _availabilityTypeId, workScheduleDate, workScheduleDate, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), null, true);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule("Ad-Hoc: " + _systemUserFullName + " - " + _careProviderStaffRoleTypeName + ", " + _availabilityTypeName + "", _systemUserId, _teamId, _recurrencePatternId, _systemUserEmploymentContractId, _availabilityTypeId, workScheduleDate1, workScheduleDate1, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), null, true);

            #endregion

            #region Step 10

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToBookingTypesPage();

            bookingTypesPage
                .WaitForBookingTypesPageToLoad()
                .InsertQuickSearchText(_bookingTypeName)
                .ClickQuickSearchButton()
                .OpenRecord(_bookingTypeId.ToString());

            var validFromDate = DateTime.Now.AddDays(-1);
            var validToDate = DateTime.Now.AddDays(1);
            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad()
                .InsertTextOnValidToDate(validFromDate.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnValidToDate(validToDate.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            var startYear = DateTime.Now.AddDays(-2).Year.ToString();
            var startMonth = DateTime.Now.AddDays(-2).ToString("MMMM");
            var startDay = DateTime.Now.AddDays(-2).Day.ToString();

            var endYear = DateTime.Now.AddDays(2).Year.ToString();
            var endMonth = DateTime.Now.AddDays(2).ToString("MMMM");
            var endDay = DateTime.Now.AddDays(2).Day.ToString();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")

                .WaitForCreateDiaryBookingPopupToLoad()
                .SelectBookingType(_bookingTypeName)
                .SelectStartDate(startYear, startMonth, startDay)
                .SelectEndDate(endYear, endMonth, endDay)
                .InsertStartTime("12", "00")
                .InsertEndTime("14", "00")

                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(_systemUserFullName)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("The booking type is not valid for the date range of the booking.")
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .ClickOnCloseButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6507")]
        [Description("Step(s) 11 from the original jira test ACC-6292")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6292_UITestMethod008()
        {
            #region Create SystemUser Record

            var _systemUserName = "Diary_Booking_" + _currentDateSuffix;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Diary Booking", "User " + _currentDateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var _systemUserFullName = "Diary Booking User " + _currentDateSuffix;

            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, thisWeekMonday);

            #endregion

            #region Booking Type

            var _bookingTypeName = "BTC ACC-6292_Auto";
            var _bookingTypeId = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 2, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);
            dbHelper.cpBookingType.UpdateValidFrom(_bookingTypeId, null);
            dbHelper.cpBookingType.UpdateValidTo(_bookingTypeId, null);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeName = "CPSRT 6292";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "6292", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeId = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId);

            #endregion

            #region Availability Type

            var _availabilityTypeName = "Salaried/Contracted";
            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName(_availabilityTypeName).First();

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region Step 11

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToBookingTypesPage();

            bookingTypesPage
                .WaitForBookingTypesPageToLoad()
                .InsertQuickSearchText(_bookingTypeName)
                .ClickQuickSearchButton()
                .OpenRecord(_bookingTypeId.ToString());

            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad()
                .ValidateValidFromDateText("")
                .ValidateValidToDateText("")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            var startYear = DateTime.Now.Year.ToString();
            var startMonth = DateTime.Now.ToString("MMMM");
            var startDay = DateTime.Now.Day.ToString();

            var endYear = DateTime.Now.AddDays(1).Year.ToString();
            var endMonth = DateTime.Now.AddDays(1).ToString("MMMM");
            var endDay = DateTime.Now.AddDays(1).Day.ToString();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")

                .WaitForCreateDiaryBookingPopupToLoad()
                .SelectBookingType(_bookingTypeName)
                .SelectStartDate(startYear, startMonth, startDay)
                .SelectEndDate(endYear, endMonth, endDay)
                .InsertStartTime("12", "00")
                .InsertEndTime("14", "00")

                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(_systemUserFullName)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .ClickCreateBooking();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateBookingStatus("Booking created");

            System.Threading.Thread.Sleep(1500);
            var careProviderDiaryBooking = dbHelper.cPBookingDiary.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderDiaryBooking.Count);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(careProviderDiaryBooking[0].ToString());

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6508")]
        [Description("Step(s) 12 from the original jira test ACC-6292")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6292_UITestMethod009()
        {
            #region Booking Type

            var _bookingTypeName = "BTC ACC-6292_Auto";
            var _bookingTypeId = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 2, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);
            dbHelper.cpBookingType.UpdateValidFrom(_bookingTypeId, null);
            dbHelper.cpBookingType.UpdateValidTo(_bookingTypeId, null);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeName = "CPSRT 6292";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "6292", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeId = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId);

            #endregion

            #region Availability Type

            var _availabilityTypeName = "Salaried/Contracted";
            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName(_availabilityTypeName).First();

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region Step 12

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToBookingTypesPage();

            bookingTypesPage
                .WaitForBookingTypesPageToLoad()
                .InsertQuickSearchText(_bookingTypeName)
                .ClickQuickSearchButton()
                .OpenRecord(_bookingTypeId.ToString());

            var validFromDate = DateTime.Now.AddDays(-3);
            var validToDate = DateTime.Now.AddDays(3);
            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad()
                .InsertTextOnValidToDate(validFromDate.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnValidToDate(validToDate.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderDiaryPageToLoad();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            var startYear = DateTime.Now.Year.ToString();
            var startMonth = DateTime.Now.ToString("MMMM");
            var startDay = DateTime.Now.Day.ToString();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")

                .WaitForCreateDiaryBookingPopupToLoad()
                .SelectBookingType(_bookingTypeName)
                .SelectStartDate(startYear, startMonth, startDay)
                .SelectEndDate(startYear, startMonth, startDay)
                .InsertStartTime("12", "00")
                .InsertEndTime("14", "00")

                .ClickCreateBooking();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateBookingStatus("Booking created");

            System.Threading.Thread.Sleep(1500);
            var careProviderDiaryBooking = dbHelper.cPBookingDiary.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderDiaryBooking.Count);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(careProviderDiaryBooking[0].ToString());

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6509")]
        [Description("Step(s) 14 & 15 from the original jira test ACC-6292")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6292_UITestMethod010()
        {
            #region Create SystemUser Record

            var _systemUserName = "Diary_Booking_" + _currentDateSuffix;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Diary Booking", "User " + _currentDateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, thisWeekMonday);

            #endregion

            #region Booking Type

            var _bookingTypeName = "BTC-1 ACC-6292";
            var _bookingTypeId = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 1, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            var _bookingTypeName2 = "BTC-2 ACC-6292";
            var _bookingTypeId2 = commonMethodsDB.CreateCPBookingType(_bookingTypeName2, 2, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            var _bookingTypeName3 = "BTC-3 ACC-6292";
            var _bookingTypeId3 = commonMethodsDB.CreateCPBookingType(_bookingTypeName3, 3, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            var ProviderAllowableBookingTypesId1 = commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeName = "CPSRT 6292";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "6292", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeId = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId);

            #endregion

            #region Availability Type

            var _availabilityTypeName = "Salaried/Contracted";
            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName(_availabilityTypeName).First();

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region Care Provider Booking Diary

            var todayDate = DateTime.UtcNow.Date;
            var _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "title", _bookingTypeId, _providerId, todayDate, new TimeSpan(12, 0, 0), todayDate, new TimeSpan(13, 00, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", _cpdiarybookingid, _systemUserEmploymentContractId, _systemUserId);

            #endregion

            #region Step 14 & 15

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(_cpdiarybookingid.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateBookingTypeDropDownText(_bookingTypeName)
                .ValidateBookingTypeIsPresent(_bookingTypeName2, false)
                .ValidateBookingTypeIsPresent(_bookingTypeName3, false)
                .ClickOnCloseButton();

            #region Provider Allowable Booking Types

            var ProviderAllowableBookingTypesId2 = commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId2, false);
            var ProviderAllowableBookingTypesId3 = commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId3, false);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(_providerName, _providerId.ToString())
                .OpenProviderRecord(_providerId.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .TapDetailsTab();

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .WaitForProvidersSchedulingBookingTypesSectionToLoad()
                .ValidateRecordIsPresent(ProviderAllowableBookingTypesId2.ToString(), true)
                .ValidateRecordIsPresent(ProviderAllowableBookingTypesId3.ToString(), true)
                .SelectRecord(ProviderAllowableBookingTypesId1.ToString())
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("1 item(s) deleted.")
                .TapOKButton();

            providerRecordPage
                .WaitForProviderRecordPageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(_cpdiarybookingid.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")
                .ValidateBookingTypeDropDownText(_bookingTypeName + " - Invalid");

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ClickOnCloseButton();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ValidateBookingTypeIsPresent(_bookingTypeName, false)
                .ValidateBookingTypeIsPresent(_bookingTypeName2, true)
                .ValidateBookingTypeIsPresent(_bookingTypeName3, true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6510")]
        [Description("Step(s) 18 from the original jira test ACC-6292")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6292_UITestMethod011()
        {
            #region Create SystemUser Record

            var _systemUserName = "Diary_Booking_" + _currentDateSuffix;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Diary Booking", "User " + _currentDateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var _systemUserFullName = "Diary Booking User " + _currentDateSuffix;

            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, thisWeekMonday);

            #endregion

            #region Booking Type

            var _bookingTypeName = "BTC-1 ACC-6292";
            var _bookingTypeId = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 1, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            var _bookingTypeName2 = "BTC-2 ACC-6292";
            var _bookingTypeId2 = commonMethodsDB.CreateCPBookingType(_bookingTypeName2, 2, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            var _bookingTypeName3 = "BTC-3 ACC-6292";
            var _bookingTypeId3 = commonMethodsDB.CreateCPBookingType(_bookingTypeName3, 3, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId, true);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId2, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId3, false);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeName = "CPSRT 6292";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "6292", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeId = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId);

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId2).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId2);

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId3).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId3);

            #endregion

            #region Availability Type

            var _availabilityTypeName = "Salaried/Contracted";
            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName(_availabilityTypeName).First();

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region Step 18 - A

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ValidateBookingTypeDropDownText(_bookingTypeName)
                .InsertStartTime("08", "00")
                .InsertEndTime("09", "00")

                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(_systemUserFullName)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .ClickCreateBooking();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateBookingStatus("Booking created");

            System.Threading.Thread.Sleep(1500);
            var careProviderDiaryBooking = dbHelper.cPBookingDiary.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderDiaryBooking.Count);

            #endregion

            #region Step 18 -B

            providerDiaryPage
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .SelectBookingType(_bookingTypeName2)
                .InsertStartTime("10", "00")
                .InsertEndTime("12", "00")

                .VerifySelectedStaffRecordInStaffForBookingIsDisplayed("0", "Unassigned", true);

            createDiaryBookingPopup
                .ClickCreateBooking();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateBookingStatus("Booking created");

            System.Threading.Thread.Sleep(1500);
            careProviderDiaryBooking = dbHelper.cPBookingDiary.GetByLocationId(_providerId);
            Assert.AreEqual(2, careProviderDiaryBooking.Count);

            #endregion

            #region Step 18 - C

            providerDiaryPage
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .SelectBookingType(_bookingTypeName2)
                .InsertStartTime("13", "00")
                .InsertEndTime("14", "00")

                .RemoveStaffFromSelectedStaffField("0");

            createDiaryBookingPopup
                .ClickCreateBooking();

            createDiaryBookingPopup
              .WaitForDynamicDialogueToLoad()
              .ValidateMessage_DynamicDialogue("You need to have at least 1 staff member for this booking type.")
              .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .ClickOnCloseButton()
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("You have unsaved changes. Are you sure you want to close the drawer?")
                .ClickConfirmButton_WarningDialogue();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();

            careProviderDiaryBooking = dbHelper.cPBookingDiary.GetByLocationId(_providerId);
            Assert.AreEqual(2, careProviderDiaryBooking.Count);

            #endregion

            #region Step 18 - D

            providerDiaryPage
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .SelectBookingType(_bookingTypeName3)
                .InsertStartTime("13", "00")
                .InsertEndTime("14", "00")

                .RemoveStaffFromSelectedStaffField("0");

            createDiaryBookingPopup
                .ClickCreateBooking();

            createDiaryBookingPopup
              .WaitForDynamicDialogueToLoad()
              .ValidateMessage_DynamicDialogue("You need to have at least 1 staff member for this booking type.")
              .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .ClickOnCloseButton();

            careProviderDiaryBooking = dbHelper.cPBookingDiary.GetByLocationId(_providerId);
            Assert.AreEqual(2, careProviderDiaryBooking.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6511")]
        [Description("Step(s) 19 from the original jira test ACC-6292")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6292_UITestMethod012()
        {
            #region Create SystemUser Record

            var _systemUserName = "Diary_Booking_" + _currentDateSuffix;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Diary Booking", "User " + _currentDateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var _systemUserFullName = "Diary Booking User " + _currentDateSuffix;

            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, thisWeekMonday);

            #endregion

            #region Booking Type

            var _bookingTypeName = "BTC-1 ACC-6292";
            var _bookingTypeId = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 1, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            var _bookingTypeName4 = "BTC-4 ACC-6292";
            var _bookingTypeId4 = commonMethodsDB.CreateCPBookingType(_bookingTypeName4, 4, 960, null, null, 1, false, null, null, null, null);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId, true);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId4, false);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeName = "CPSRT 6292";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "6292", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeId = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId);

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId4).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId4);

            #endregion

            #region Availability Type

            var _availabilityTypeName = "Salaried/Contracted";
            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName(_availabilityTypeName).First();

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region Step 19 - A

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ValidateBookingTypeDropDownText(_bookingTypeName)
                .SelectBookingType(_bookingTypeName4)
                .InsertStartTime("08", "00")
                .InsertEndTime("09", "00")

                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(_systemUserFullName)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .ClickCreateBooking();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateBookingStatus("Booking created");

            System.Threading.Thread.Sleep(1500);
            var careProviderDiaryBooking = dbHelper.cPBookingDiary.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderDiaryBooking.Count);

            #endregion

            #region Step 19 - B

            providerDiaryPage
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .SelectBookingType(_bookingTypeName4)
                .InsertStartTime("10", "00")
                .InsertEndTime("12", "00")

                .VerifySelectedStaffRecordInStaffForBookingIsDisplayed("0", "Unassigned", true);

            createDiaryBookingPopup
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("You need to have at least 1 staff member allocated for this booking type.")
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .ClickOnCloseButton()
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("You have unsaved changes. Are you sure you want to close the drawer?")
                .ClickConfirmButton_WarningDialogue();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();

            careProviderDiaryBooking = dbHelper.cPBookingDiary.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderDiaryBooking.Count);

            #endregion

            #region Step 19 - C

            providerDiaryPage
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .SelectBookingType(_bookingTypeName4)
                .InsertStartTime("13", "00")
                .InsertEndTime("14", "00")

                .RemoveStaffFromSelectedStaffField("0");

            createDiaryBookingPopup
                .ClickCreateBooking();

            createDiaryBookingPopup
              .WaitForDynamicDialogueToLoad()
              .ValidateMessage_DynamicDialogue("You need to have at least 1 staff member for this booking type.")
              .ValidateMessage_DynamicDialogue("You need to have at least 1 staff member allocated for this booking type.", 2)
              .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .ClickOnCloseButton();

            careProviderDiaryBooking = dbHelper.cPBookingDiary.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderDiaryBooking.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6512")]
        [Description("Step(s) 20 from the original jira test ACC-6292")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6292_UITestMethod013()
        {
            #region Create SystemUser Record

            var _systemUserName = "Diary_Booking_" + _currentDateSuffix;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Diary Booking", "User " + _currentDateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, thisWeekMonday);

            #endregion

            #region Booking Type

            var _bookingTypeName = "BTC-1 ACC-6292";
            var _bookingTypeId = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 1, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            var _bookingTypeName2 = "BTC-2 ACC-6292";
            var _bookingTypeId2 = commonMethodsDB.CreateCPBookingType(_bookingTypeName2, 2, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            var _bookingTypeName3 = "BTC-3 ACC-6292";
            var _bookingTypeId3 = commonMethodsDB.CreateCPBookingType(_bookingTypeName3, 3, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId, true);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId2, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId3, false);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeName = "CPSRT 6292";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "6292", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeId = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId);

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId2).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId2);

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId3).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId3);

            #endregion

            #region Availability Type

            var _availabilityTypeName = "Salaried/Contracted";
            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName(_availabilityTypeName).First();

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region Care Provider Booking Diary

            var todayDate = DateTime.UtcNow.Date;
            var _cpdiarybookingId1 = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "title", _bookingTypeId, _providerId, todayDate, new TimeSpan(10, 0, 0), todayDate, new TimeSpan(12, 00, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", _cpdiarybookingId1, _systemUserEmploymentContractId, _systemUserId);

            var _cpdiarybookingId2 = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "title", _bookingTypeId2, _providerId, todayDate, new TimeSpan(13, 0, 0), todayDate, new TimeSpan(14, 00, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "Unallocated", _cpdiarybookingId2, null, null);

            var _cpdiarybookingId3 = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "title", _bookingTypeId3, _providerId, todayDate, new TimeSpan(15, 0, 0), todayDate, new TimeSpan(16, 00, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", _cpdiarybookingId3, _systemUserEmploymentContractId, _systemUserId);

            #endregion

            #region Step 20 - A

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(_cpdiarybookingId1.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateBookingTypeDropDownText(_bookingTypeName)

                .RemoveStaffFromSelectedStaffField(_systemUserEmploymentContractId.ToString());

            createDiaryBookingPopup
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("You need to have at least 1 staff member for this booking type.")
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .ClickOnCloseButton()
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("You have unsaved changes. Are you sure you want to close the drawer?")
                .ClickConfirmButton_WarningDialogue();

            #endregion

            #region Step 20 - B

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(_cpdiarybookingId2.ToString());

            System.Threading.Thread.Sleep(1000);
            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateBookingTypeDropDownText(_bookingTypeName2)

                .RemoveStaffFromSelectedStaffField("0");

            createDiaryBookingPopup
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("You need to have at least 1 staff member for this booking type.")
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .ClickOnCloseButton()
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("You have unsaved changes. Are you sure you want to close the drawer?")
                .ClickConfirmButton_WarningDialogue();

            #endregion

            #region Step 20 - C

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(_cpdiarybookingId3.ToString());

            System.Threading.Thread.Sleep(1000);
            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateBookingTypeDropDownText(_bookingTypeName3)

                .RemoveStaffFromSelectedStaffField(_systemUserEmploymentContractId.ToString());

            createDiaryBookingPopup
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("You need to have at least 1 staff member for this booking type.")
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .ClickOnCloseButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6513")]
        [Description("Step(s) 21 from the original jira test ACC-6292")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6292_UITestMethod014()
        {
            #region Create SystemUser Record

            var _systemUserName = "Diary_Booking_" + _currentDateSuffix;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Diary Booking", "User " + _currentDateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, thisWeekMonday);

            #endregion

            #region Booking Type

            var _bookingTypeName4 = "BTC-4 ACC-6292";
            var _bookingTypeId4 = commonMethodsDB.CreateCPBookingType(_bookingTypeName4, 4, 960, null, null, 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId4, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeName = "CPSRT 6292";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "6292", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeId = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId4).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId4);

            #endregion

            #region Availability Type

            var _availabilityTypeName = "Salaried/Contracted";
            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName(_availabilityTypeName).First();

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region Care Provider Booking Diary

            var todayDate = DateTime.UtcNow.Date;
            var _cpdiarybookingId1 = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "title", _bookingTypeId4, _providerId, todayDate, new TimeSpan(10, 0, 0), todayDate, new TimeSpan(12, 00, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", _cpdiarybookingId1, _systemUserEmploymentContractId, _systemUserId);

            #endregion

            #region Step 21

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(_cpdiarybookingId1.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateBookingTypeDropDownText(_bookingTypeName4)

                .RemoveStaffFromSelectedStaffField(_systemUserEmploymentContractId.ToString());

            createDiaryBookingPopup
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("You need to have at least 1 staff member for this booking type.")
                .ValidateMessage_DynamicDialogue("You need to have at least 1 staff member allocated for this booking type.", 2)
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .ClickOnCloseButton();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6520

        [TestProperty("JiraIssueID", "ACC-6711")]
        [Description("Step(s) 1 to 5 from the original jira test ACC-6518")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6518_UITestMethod001()
        {
            #region Business Unit

            var _businessUnitId2 = commonMethodsDB.CreateBusinessUnit("Provider Diary Booking BU2");

            #endregion

            #region Team

            var _teamName2 = "ProviderDiaryBookingT2";
            var _teamId2 = commonMethodsDB.CreateTeam(_teamName2, null, _businessUnitId2, "107624", "ProviderDiaryBookingT2@careworkstempmail.com", "ProviderDiaryBookingT2", "020 123456");

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "Diary_Booking_" + _currentDateSuffix;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Diary Booking", "User " + _currentDateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var _systemUserFullName = "Diary Booking User " + _currentDateSuffix;

            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, thisWeekMonday);

            #endregion

            #region Security Profile

            var _securityProfileid = dbHelper.securityProfile.GetSecurityProfileByName("System User Employment Contract (Field Edit)")[0];
            commonMethodsDB.CreateUserSecurityProfile(_systemUserId, _securityProfileid);

            #endregion

            #region Booking Type

            var _bookingTypeName = "BTC-1 ACC-6518";
            var _bookingTypeId = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 1, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeName1 = "CPSRT 6518-1";
            var _careProviderStaffRoleTypeId1 = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName1, "62921", null, new DateTime(2020, 1, 1), null);

            var _careProviderStaffRoleTypeName2 = "CPSRT 6518-2";
            var _careProviderStaffRoleTypeId2 = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName2, "62922", null, new DateTime(2020, 1, 1), null);

            var _careProviderStaffRoleTypeName3 = "CPSRT 6518-3";
            var _careProviderStaffRoleTypeId3 = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName3, "62923", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeId = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _active_SystemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeId1, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });
            var _notStarted_systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, null, _careProviderStaffRoleTypeId1, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region System User Employment Contract for Suspended status

            var systemUserEmploymentContractStartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);
            var _suspension_systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, systemUserEmploymentContractStartDate, _careProviderStaffRoleTypeId2, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region System User Employment Contract for Ended status

            var systemUserEmploymentContractStartDate2 = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var _ended_systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, systemUserEmploymentContractStartDate2, _careProviderStaffRoleTypeId3, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_active_SystemUserEmploymentContractId, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_active_SystemUserEmploymentContractId, _bookingTypeId);

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_notStarted_systemUserEmploymentContractId, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_notStarted_systemUserEmploymentContractId, _bookingTypeId);

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_suspension_systemUserEmploymentContractId, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_suspension_systemUserEmploymentContractId, _bookingTypeId);

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_ended_systemUserEmploymentContractId, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_ended_systemUserEmploymentContractId, _bookingTypeId);

            #endregion

            #region Availability Type

            var _availabilityTypeName = "Salaried/Contracted";
            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName(_availabilityTypeName).First();

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(_systemUserId, _teamId, _active_SystemUserEmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId, _teamId, _notStarted_systemUserEmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId, _teamId, _suspension_systemUserEmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId, _teamId, _ended_systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region End System User Employment Contract

            var systemUserEmploymentContractEndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-2);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(_ended_systemUserEmploymentContractId, systemUserEmploymentContractEndDate, contractEndReasonId);

            #endregion

            #region Create System User Contract Suspension

            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date;
            dbHelper.systemUserSuspension.CreateSystemUserSuspension(_systemUserId, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, new List<Guid> { _suspension_systemUserEmploymentContractId });

            #endregion

            #region Step 2 & 4

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")

                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(_systemUserFullName)
                .VerifyStaffRecordIsDisplayed(_active_SystemUserEmploymentContractId)
                .VerifyStaffRecordIsDisplayed(_notStarted_systemUserEmploymentContractId)
                .VerifyStaffRecordIsDisplayed(_suspension_systemUserEmploymentContractId)
                .VerifyStaffRecordIsDisplayed(_ended_systemUserEmploymentContractId, false)
                .ClickStaffRecordCellText(_active_SystemUserEmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickOnCloseButton()
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("You have unsaved changes. Are you sure you want to close the drawer?")
                .ClickConfirmButton_WarningDialogue();

            #endregion

            #region Step 3 & 5

            // Update Team for Employment Contract
            dbHelper.systemUserEmploymentContract.UpdateOwnerId(_active_SystemUserEmploymentContractId, _teamId2);

            // Update Can Work At Team
            dbHelper.systemUserEmploymentContract.UpdateCanWorksAt(_active_SystemUserEmploymentContractId, new List<Guid> { _teamId });

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")

                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(_systemUserFullName)
                .VerifyStaffRecordIsDisplayed(_active_SystemUserEmploymentContractId)
                .ClickStaffRecordCellText(_active_SystemUserEmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking()
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("Employee Diary Booking User " + _currentDateSuffix + " has more than one contract; Diary Booking User " + _currentDateSuffix + ", 40.00 hrs, and Diary Booking User " + _currentDateSuffix + ". 40.00 hrs, Are you sure you are allocating the booking to the correct employment contract in line with the employees pay arrangement?")
                .ClickConfirmButton_WarningDialogue();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateBookingStatus("Booking created");

            System.Threading.Thread.Sleep(1500);
            var careProviderDiaryBooking = dbHelper.cPBookingDiary.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderDiaryBooking.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6712")]
        [Description("Step(s) 6 & 7 from the original jira test ACC-6518")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6518_UITestMethod002()
        {
            #region Create SystemUser Record

            var _systemUserName = "Diary_Booking_User_" + _currentDateSuffix;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Diary Booking", "User_" + _currentDateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var _systemUserFullName = "Diary Booking User_" + _currentDateSuffix;

            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, thisWeekMonday);

            #endregion

            #region Security Profile

            var _securityProfileid = dbHelper.securityProfile.GetSecurityProfileByName("System User Employment Contract (Field Edit)")[0];
            commonMethodsDB.CreateUserSecurityProfile(_systemUserId, _securityProfileid);

            #endregion

            #region Booking Type

            var _bookingTypeName = "BTC-1 ACC-6518";
            var _bookingTypeId = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 1, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeName1 = "CPSRT 6518-1";
            var _careProviderStaffRoleTypeId1 = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName1, "62921", null, new DateTime(2020, 1, 1), null);

            var _careProviderStaffRoleTypeName2 = "CPSRT 6518-2";
            var _careProviderStaffRoleTypeId2 = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName2, "62922", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeId = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _notStarted_systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, null, _careProviderStaffRoleTypeId1, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region System User Employment Contract for Suspended status

            var systemUserEmploymentContractStartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);
            var _suspension_systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, systemUserEmploymentContractStartDate, _careProviderStaffRoleTypeId2, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_notStarted_systemUserEmploymentContractId, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_notStarted_systemUserEmploymentContractId, _bookingTypeId);

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_suspension_systemUserEmploymentContractId, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_suspension_systemUserEmploymentContractId, _bookingTypeId);

            #endregion

            #region Availability Type

            var _availabilityTypeName = "Salaried/Contracted";
            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName(_availabilityTypeName).First();

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(_systemUserId, _teamId, _notStarted_systemUserEmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId, _teamId, _suspension_systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region Create System User Contract Suspension

            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date;
            dbHelper.systemUserSuspension.CreateSystemUserSuspension(_systemUserId, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, new List<Guid> { _suspension_systemUserEmploymentContractId });

            #endregion

            #region Step 6

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")

                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(_systemUserFullName)
                .VerifyStaffRecordIsDisplayed(_suspension_systemUserEmploymentContractId)
                .ClickStaffRecordCellText(_suspension_systemUserEmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("" + _systemUserFullName + " - " + _careProviderStaffRoleTypeName2 + " contract is suspended at the selected booking time.")
                .ClickDismissButton_DynamicDialogue();

            #endregion

            #region Step 7

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .RemoveStaffFromSelectedStaffField(_suspension_systemUserEmploymentContractId.ToString())

                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(_systemUserFullName)
                .VerifyStaffRecordIsDisplayed(_notStarted_systemUserEmploymentContractId)
                .ClickStaffRecordCellText(_notStarted_systemUserEmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("" + _systemUserFullName + " - " + _careProviderStaffRoleTypeName1 + " contract is not started at the selected booking time.")
                .ClickDismissButton_DynamicDialogue();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6521

        [TestProperty("JiraIssueID", "ACC-6713")]
        [Description("Step(s) 8 from the original jira test ACC-6518")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6518_UITestMethod003()
        {
            #region Create SystemUser Record

            var _systemUserName = "Diary_Booking_" + _currentDateSuffix;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Diary Booking", "User " + _currentDateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var _systemUserFullName = "Diary Booking User " + _currentDateSuffix;

            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, thisWeekMonday);

            #endregion

            #region Booking Type

            var _bookingTypeName1 = "Morning Test View";
            var _bookingTypeId1 = commonMethodsDB.CreateCPBookingType(_bookingTypeName1, 1, 960, new TimeSpan(8, 0, 0), new TimeSpan(16, 0, 0), 1, false, null, null, null, 1);

            var _bookingTypeName2 = "Second Test View";
            var _bookingTypeId2 = commonMethodsDB.CreateCPBookingType(_bookingTypeName2, 1, 960, new TimeSpan(16, 0, 0), new TimeSpan(23, 0, 0), 1, false, null, null, null, 1);

            var _bookingTypeName3 = "Day Shift";
            var _bookingTypeId3 = commonMethodsDB.CreateCPBookingType(_bookingTypeName3, 1, 960, new TimeSpan(0, 0, 0), new TimeSpan(23, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId1, true);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId2, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId3, false);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeName1 = "CPSRT 6518-1";
            var _careProviderStaffRoleTypeId1 = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName1, "62921", null, new DateTime(2020, 1, 1), null);

            var _careProviderStaffRoleTypeName2 = "CPSRT 6518-2";
            commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName2, "62922", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeId = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeId1, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId1).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId1);

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId2).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId2);

            #endregion

            #region Availability Type

            var _availabilityTypeName = "Salaried/Contracted";
            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName(_availabilityTypeName).First();

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region Step 8

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")
                .SelectBookingType(_bookingTypeName3)

                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(_systemUserFullName)
                .VerifyStaffRecordIsDisplayed(_systemUserEmploymentContractId)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("The use of this Booking Type is not currently configured for " + _systemUserFullName + " - " + _careProviderStaffRoleTypeName1 + ".")
                .ClickDismissButton_DynamicDialogue();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6714")]
        [Description("Step(s) 9 from the original jira test ACC-6518")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6518_UITestMethod004()
        {
            #region Create SystemUser Record

            var _systemUserName = "Diary_Booking_" + _currentDateSuffix;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Diary Booking", "User " + _currentDateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var _systemUserFullName = "Diary Booking User " + _currentDateSuffix;

            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, thisWeekMonday);

            #endregion

            #region Booking Type

            var _bookingTypeName1 = "BTC-1 ACC-6518";
            var _bookingTypeId1 = commonMethodsDB.CreateCPBookingType(_bookingTypeName1, 1, 960, new TimeSpan(7, 0, 0), new TimeSpan(23, 0, 0), 1, false, null, null, null, 1);

            var _bookingTypeName2 = "BTC-2 ACC-6518";
            var _bookingTypeId2 = commonMethodsDB.CreateCPBookingType(_bookingTypeName2, 1, 960, new TimeSpan(7, 0, 0), new TimeSpan(23, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            var ProviderAllowableBookingTypesId1 = commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId1, true);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId2, false);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeName1 = "CPSRT 6518-1";
            var _careProviderStaffRoleTypeId1 = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName1, "62921", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeId = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeId1, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId1).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId1);

            #endregion

            #region Availability Type

            var _availabilityTypeName = "Salaried/Contracted";
            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName(_availabilityTypeName).First();

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region Step 9

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")
                .SelectBookingType(_bookingTypeName1)

                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(_systemUserFullName)
                .VerifyStaffRecordIsDisplayed(_systemUserEmploymentContractId)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .OpenDuplicateTab()
                .SwitchToNewTab();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(_providerName, _providerId.ToString())
                .OpenProviderRecord(_providerId.ToString());

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickDetailsTab()
                .WaitForProvidersSchedulingBookingTypesSectionToLoad()
                .SelectRecord(ProviderAllowableBookingTypesId1.ToString())
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("1 item(s) deleted.")
                .TapOKButton();

            createDiaryBookingPopup
                .SwitchToPreviousTab();
            System.Threading.Thread.Sleep(1000);

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("The booking type is not on the allowable booking types list for the provider.")
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickOnCloseButton();

            var careProviderDiaryBooking = dbHelper.cPBookingDiary.GetByLocationId(_providerId);
            Assert.AreEqual(0, careProviderDiaryBooking.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6715")]
        [Description("Step(s) 11 from the original jira test ACC-6518")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6518_UITestMethod005()
        {
            #region Create SystemUser Record

            var _systemUserName = "Diary_Booking_" + _currentDateSuffix;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Diary Booking", "User " + _currentDateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var _systemUserFullName = "Diary Booking User " + _currentDateSuffix;

            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, thisWeekMonday);

            #endregion

            #region Booking Type

            var _bookingTypeName1 = "BTC-1 ACC-6518";
            var _bookingTypeId1 = commonMethodsDB.CreateCPBookingType(_bookingTypeName1, 1, 960, new TimeSpan(7, 0, 0), new TimeSpan(23, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId1, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeName1 = "CPSRT 6518-1";
            var _careProviderStaffRoleTypeId1 = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName1, "62921", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeId = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1, 6, 0, 0), _careProviderStaffRoleTypeId1, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId1).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId1);

            #endregion

            #region Availability Type

            var _availabilityTypeName = "Salaried/Contracted";
            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName(_availabilityTypeName).First();

            #endregion

            #region User Work Schedule

            _recurrencePattern_Every1WeekMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on monday").First();
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _teamId, _recurrencePattern_Every1WeekMondayId, _systemUserEmploymentContractId, _availabilityTypeId, thisWeekMonday, null, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), 1);

            #endregion

            #region Step 11

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            var startYear = thisWeekMonday.Year.ToString();
            var startMonth = thisWeekMonday.ToString("MMMM");
            var startDay = thisWeekMonday.Day.ToString();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")
                .SelectBookingType(_bookingTypeName1)
                .SelectStartDate(startYear, startMonth, startDay)
                .SelectEndDate(startYear, startMonth, startDay)
                .InsertStartTime("08", "00")
                .InsertEndTime("19", "00")
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .ClickOnlyShowAvailableStaff()
                .EnterTextIntoFilterStaffByNameSearchBox(_systemUserFullName)
                .VerifyStaffRecordIsDisplayed(_systemUserEmploymentContractId)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(_systemUserFullName + ", " + _careProviderStaffRoleTypeName1 + ", 40.00 hrs, 01/01/2022 (Active) is not available at this time. Add a block of diary availability of the type selected below to cover the duration?")
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickOnCloseButton();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6683

        [TestProperty("JiraIssueID", "ACC-6716")]
        [Description("Step(s) 12 from the original jira test ACC-6518")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6518_UITestMethod006()
        {
            #region Create SystemUser Record

            var todayDate = DateTime.UtcNow.Date;

            var _systemUserName = "Diary_Booking_1" + _currentDateSuffix;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Diary Booking", "User 1_" + _currentDateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, thisWeekMonday);

            // Second System User for Staff Contract
            var _systemUserName2 = "Diary_Booking_2" + _currentDateSuffix;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_systemUserName2, "Diary Booking", "User 2_" + _currentDateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var _systemUserFullName2 = "Diary Booking User 2_" + _currentDateSuffix;

            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId2, localZone.StandardName);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId2, thisWeekMonday);

            #endregion

            #region Booking Type

            var _bookingTypeName1 = "BTC-1 ACC-6518";
            var _bookingTypeId1 = commonMethodsDB.CreateCPBookingType(_bookingTypeName1, 1, 960, new TimeSpan(7, 0, 0), new TimeSpan(23, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId1, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeName1 = "CPSRT 6518-1";
            var _careProviderStaffRoleTypeId1 = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName1, "62921", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeId = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1, 6, 0, 0), _careProviderStaffRoleTypeId1, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId2, new DateTime(2022, 1, 1, 6, 0, 0), _careProviderStaffRoleTypeId1, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId1).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId1);

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId2, _bookingTypeId1).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingTypeId1);

            #endregion

            #region Availability Type

            var _availabilityTypeName = "Salaried/Contracted";
            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName(_availabilityTypeName).First();

            #endregion

            #region User Work Schedule

            DateTime dateOfWeek = todayDate;
            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId, dateOfWeek);

            #endregion

            #region Care Provider Booking Diary

            var _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "title", _bookingTypeId1, _providerId, todayDate, new TimeSpan(12, 0, 0), todayDate, new TimeSpan(13, 0, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", _cpdiarybookingid, _systemUserEmploymentContractId, _systemUserId);

            #endregion

            #region Step 12

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(_cpdiarybookingid.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .ClickOnlyShowAvailableStaff()
                .EnterTextIntoFilterStaffByNameSearchBox(_systemUserFullName2)
                .VerifyStaffRecordIsDisplayed(_systemUserEmploymentContractId2)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId2)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(_systemUserFullName2 + ", " + _careProviderStaffRoleTypeName1 + ", 40.00 hrs, 01/01/2022 (Active) is not available at this time. Add a block of diary availability of the type selected below to cover the duration?")
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ClickOnCloseButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6717")]
        [Description("Step(s) 13 from the original jira test ACC-6518")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6518_UITestMethod007()
        {
            #region Create SystemUser Record

            var todayDate = DateTime.UtcNow.Date;

            var _systemUserName = "Diary_Booking_" + _currentDateSuffix;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Diary Booking", "User " + _currentDateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var _systemUserFullName = "Diary Booking User " + _currentDateSuffix;

            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, thisWeekMonday);

            #endregion

            #region Booking Type

            var _bookingTypeName1 = "BTC-1 ACC-6518";
            var _bookingTypeId1 = commonMethodsDB.CreateCPBookingType(_bookingTypeName1, 1, 960, new TimeSpan(7, 0, 0), new TimeSpan(23, 0, 0), 1, false, null, null, null, 1);

            var _bookingTypeName2 = "BTC-2 ACC-6518";
            var _bookingTypeId2 = commonMethodsDB.CreateCPBookingType(_bookingTypeName2, 1, 960, new TimeSpan(7, 0, 0), new TimeSpan(23, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId1, true);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId2, false);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeName = "CPSRT 6518-1";
            var _careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "62921", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeId = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeId, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId1).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId1);

            #endregion

            #region Availability Type

            var _availabilityTypeName = "Salaried/Contracted";
            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName(_availabilityTypeName).First();

            #endregion

            #region User Work Schedule

            DateTime dateOfWeek = todayDate;
            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId, dateOfWeek);

            #endregion

            #region Care Provider Booking Diary

            var _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "title", _bookingTypeId1, _providerId, todayDate, new TimeSpan(12, 0, 0), todayDate, new TimeSpan(13, 0, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", _cpdiarybookingid, _systemUserEmploymentContractId, _systemUserId);

            #endregion

            #region Step 13

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(_cpdiarybookingid.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")
                .SelectBookingType(_bookingTypeName2)

                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("The use of this Booking Type is not currently configured for " + _systemUserFullName + " - " + _careProviderStaffRoleTypeName + ".")
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ClickOnCloseButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6718")]
        [Description("Step(s) 14 from the original jira test ACC-6518")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6518_UITestMethod008()
        {
            #region Create SystemUser Record

            var todayDate = DateTime.UtcNow.Date;

            var _systemUserName = "Diary_Booking_" + _currentDateSuffix;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Diary Booking", "User " + _currentDateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var _systemUserFullName = "Diary Booking User " + _currentDateSuffix;

            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, thisWeekMonday);

            #endregion

            #region Booking Type

            var _bookingTypeName = "BTC-1 ACC-6518";
            var _bookingTypeId = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 1, 960, new TimeSpan(7, 0, 0), new TimeSpan(23, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeName = "CPSRT 6518-1";
            var _careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "62921", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeId = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeId, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId);

            #endregion

            #region Availability Type

            var _availabilityTypeName = "Salaried/Contracted";
            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName(_availabilityTypeName).First();

            #endregion

            #region User Work Schedule

            DateTime dateOfWeek = todayDate;
            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId, dateOfWeek);

            #endregion

            #region Care Provider Booking Diary

            var _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "title", _bookingTypeId, _providerId, todayDate, new TimeSpan(12, 0, 0), todayDate, new TimeSpan(13, 0, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", _cpdiarybookingid, _systemUserEmploymentContractId, _systemUserId);

            #endregion

            #region Step 14

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(_systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ClickAvailableBookingTypes_RemoveRecordIcon(_bookingTypeId.ToString())
                .ClickSaveAndCloseButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(_cpdiarybookingid.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")

                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("The use of this Booking Type is not currently configured for " + _systemUserFullName + " - " + _careProviderStaffRoleTypeName + ".")
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ClickOnCloseButton();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6732

        [TestProperty("JiraIssueID", "ACC-6719")]
        [Description("Step(s) 15 from the original jira test ACC-6518")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6518_UITestMethod009()
        {
            #region Create SystemUser Record

            var _systemUserName = "Diary_Booking_" + _currentDateSuffix;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Diary Booking", "User " + _currentDateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var _systemUserFullName = "Diary Booking User " + _currentDateSuffix;

            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, thisWeekMonday);

            #endregion

            #region Booking Type

            var _bookingTypeName = "BTC-1 ACC-6518";
            var _bookingTypeId = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 1, 960, new TimeSpan(7, 0, 0), new TimeSpan(23, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeName = "CPSRT 6518-1";
            var _careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "62921", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeId = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeId, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId);

            #endregion

            #region Availability Type

            var _availabilityTypeName = "Salaried/Contracted";
            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName(_availabilityTypeName).First();

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region Step 15

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(_systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .InsertStartDate("", "")
                .ClickSaveAndCloseButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .ClickOnlyShowAvailableStaff()
                .EnterTextIntoFilterStaffByNameSearchBox(_systemUserFullName)
                .VerifyStaffRecordIsDisplayed(_systemUserEmploymentContractId)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("" + _systemUserFullName + " - " + _careProviderStaffRoleTypeName + " contract is not started at the selected booking time.")
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickOnCloseButton();

            var careProviderDiaryBooking = dbHelper.cPBookingDiary.GetByLocationId(_providerId);
            Assert.AreEqual(0, careProviderDiaryBooking.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6720")]
        [Description("Step(s) 16 & 17 from the original jira test ACC-6518")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6518_UITestMethod010()
        {
            #region Create SystemUser Record

            var _systemUserName = "Diary_Booking_" + _currentDateSuffix;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Diary Booking", "User " + _currentDateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var _systemUserFullName = "Diary Booking User " + _currentDateSuffix;

            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, thisWeekMonday);

            // Second Staff System user

            var _systemUserName2 = "Diary_Booking_2_" + _currentDateSuffix;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_systemUserName2, "Diary Booking", "User 2_" + _currentDateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var _systemUserFullName2 = "Diary Booking User 2_" + _currentDateSuffix;

            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId2, localZone.StandardName);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId2, thisWeekMonday);

            #endregion

            #region Booking Type

            var _bookingTypeName = "BTC-1 ACC-6518";
            var _bookingTypeId = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 1, 960, new TimeSpan(7, 0, 0), new TimeSpan(23, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeName = "CPSRT 6518-1";
            var _careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "62921", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeId = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeId, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId }, null);
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId2, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeId, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId);

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId2, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingTypeId);

            #endregion

            #region Availability Type

            var _availabilityTypeName = "Salaried/Contracted";
            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName(_availabilityTypeName).First();

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId2, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Suspended System User Employment Contract

            // System User Employment Contract 1
            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date.AddDays(-3);
            var systemUserSuspensionEndDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date.AddDays(3);

            var contracts = new List<Guid> { _systemUserEmploymentContractId };
            var suspensionId = dbHelper.systemUserSuspension.CreateSystemUserSuspension(_systemUserId, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);
            dbHelper.systemUserSuspension.UpdateSuspensionEndDate(suspensionId, systemUserSuspensionEndDate, contracts);

            // System User Employment Contract 2

            var systemUserSuspensionFutureStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date.AddDays(2);
            dbHelper.systemUserSuspension.CreateSystemUserSuspension(_systemUserId2, systemUserSuspensionFutureStartDate, _teamId, systemUserSuspensionReasonId, new List<Guid> { _systemUserEmploymentContractId2 });

            #endregion

            #region Step 16

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(_systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateSuspensionStartDateText(systemUserSuspensionStartDate.ToString("dd'/'MM'/'yyyy HH:mm:ss"))
                .ValidateSuspensionEndDateText(systemUserSuspensionEndDate.ToString("dd'/'MM'/'yyyy HH:mm:ss"));

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(_systemUserFullName)
                .VerifyStaffRecordIsDisplayed(_systemUserEmploymentContractId)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("" + _systemUserFullName + " - " + _careProviderStaffRoleTypeName + " contract is suspended at the selected booking time.")
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickOnCloseButton()
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("You have unsaved changes. Are you sure you want to close the drawer?")
                .ClickConfirmButton_WarningDialogue();

            var careProviderDiaryBooking = dbHelper.cPBookingDiary.GetByLocationId(_providerId);
            Assert.AreEqual(0, careProviderDiaryBooking.Count);

            #endregion

            #region Step 17

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            var bookingYear = DateTime.Now.Date.AddDays(3).Year.ToString();
            var bookingMonth = DateTime.Now.Date.AddDays(3).ToString("MMMM");
            var bookingDay = DateTime.Now.Date.AddDays(3).Day.ToString();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")
                .SelectStartDate(bookingYear, bookingMonth, bookingDay)
                .SelectEndDate(bookingYear, bookingMonth, bookingDay)
                .InsertStartTime("12", "00")
                .InsertEndTime("14", "00")
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(_systemUserFullName2)
                .VerifyStaffRecordIsDisplayed(_systemUserEmploymentContractId2)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId2)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("" + _systemUserFullName2 + " - " + _careProviderStaffRoleTypeName + " contract is suspended at the selected booking time.")
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickOnCloseButton()
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("You have unsaved changes. Are you sure you want to close the drawer?")
                .ClickConfirmButton_WarningDialogue();

            careProviderDiaryBooking = dbHelper.cPBookingDiary.GetByLocationId(_providerId);
            Assert.AreEqual(0, careProviderDiaryBooking.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6808")]
        [Description("Step(s) 18 & 19 from the original jira test ACC-6518")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6518_UITestMethod011()
        {
            #region Team

            var _teamName2 = "Team B";
            var _teamId2 = commonMethodsDB.CreateTeam(_teamName2, null, _businessUnitId, "107624", "TeamB@careworkstempmail.com", "TeamB", "020 123456");

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "Diary_Booking_" + _currentDateSuffix;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Diary Booking", "User " + _currentDateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var _systemUserFullName = "Diary Booking User " + _currentDateSuffix;

            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, thisWeekMonday);

            // Second Staff System user

            var _systemUserName2 = "Diary_Booking_2_" + _currentDateSuffix;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_systemUserName2, "Diary Booking", "User 2_" + _currentDateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var _systemUserFullName2 = "Diary Booking User 2_" + _currentDateSuffix;

            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId2, localZone.StandardName);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId2, thisWeekMonday);

            #endregion

            #region Booking Type

            var _bookingTypeName = "BTC-1 ACC-6518";
            var _bookingTypeId = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 1, 960, new TimeSpan(7, 0, 0), new TimeSpan(23, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeName = "CPSRT 6518-1";
            var _careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "62921", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeId = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeId, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId2, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeId, _teamId2, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId);

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId2, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingTypeId);

            #endregion

            #region Availability Type

            var _availabilityTypeName = "Salaried/Contracted";
            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName(_availabilityTypeName).First();

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId2, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Step 18 & 19

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(_systemUserFullName)
                .VerifyStaffRecordIsDisplayed(_systemUserEmploymentContractId)
                .EnterTextIntoFilterStaffByNameSearchBox(_systemUserFullName2)
                .VerifyStaffRecordIsDisplayed(_systemUserEmploymentContractId2)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId2)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateBookingStatus("Booking created");

            System.Threading.Thread.Sleep(1500);
            var careProviderDiaryBooking = dbHelper.cPBookingDiary.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderDiaryBooking.Count);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6733

        [TestProperty("JiraIssueID", "ACC-6809")]
        [Description("Step(s) 20 from the original jira test ACC-6518")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6518_UITestMethod012()
        {
            #region Create SystemUser Record

            var _systemUserName = "Diary_Booking_" + _currentDateSuffix;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Diary Booking", "User " + _currentDateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var _systemUserFullName = "Diary Booking User " + _currentDateSuffix;

            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, thisWeekMonday);

            #endregion

            #region Booking Type

            var _bookingTypeName1 = "ACC-6518_Morning";
            var _bookingTypeId1 = commonMethodsDB.CreateCPBookingType(_bookingTypeName1, 1, 960, new TimeSpan(7, 0, 0), new TimeSpan(23, 0, 0), 1, false, null, null, null, 1);

            var _bookingTypeName2 = "ACC-6518_Afternoon";
            var _bookingTypeId2 = commonMethodsDB.CreateCPBookingType(_bookingTypeName2, 1, 960, new TimeSpan(7, 0, 0), new TimeSpan(23, 0, 0), 1, false, null, null, null, 1);

            var _bookingTypeName3 = "ACC-6518_Night";
            var _bookingTypeId3 = commonMethodsDB.CreateCPBookingType(_bookingTypeName3, 1, 960, new TimeSpan(7, 0, 0), new TimeSpan(23, 0, 0), 1, false, null, null, null, 1);

            var _bookingTypeName4 = "ACC-6518_LateNight";
            var _bookingTypeId4 = commonMethodsDB.CreateCPBookingType(_bookingTypeName4, 1, 960, new TimeSpan(7, 0, 0), new TimeSpan(23, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId1, true);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId2, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId3, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId4, false);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeName = "CPSRT 6518-1";
            var _careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "62921", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeId = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeId, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId1).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId1);

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingTypeId2).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingTypeId2);

            #endregion

            #region Availability Type

            var _availabilityTypeName = "Salaried/Contracted";
            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName(_availabilityTypeName).First();

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region Step 20

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")
                .SelectBookingType(_bookingTypeName3)
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(_systemUserFullName)
                .VerifyStaffRecordIsDisplayed(_systemUserEmploymentContractId)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("The use of this Booking Type is not currently configured for " + _systemUserFullName + " - " + _careProviderStaffRoleTypeName + ".")
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickOnCloseButton();

            var careProviderDiaryBooking = dbHelper.cPBookingDiary.GetByLocationId(_providerId);
            Assert.AreEqual(0, careProviderDiaryBooking.Count);

            #endregion

            // Step 21 to 23 already covered.

        }

        [TestProperty("JiraIssueID", "ACC-6810")]
        [Description("Step(s) 24 to 26 from the original jira test ACC-6518")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderScheduleBooking_ACC_6518_UITestMethod013()
        {
            #region Create SystemUser Record

            var _systemUserName = "Diary_Booking_" + _currentDateSuffix;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Diary Booking", "User " + _currentDateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var _systemUserFullName = "Diary Booking User " + _currentDateSuffix;

            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, thisWeekMonday);

            #endregion

            #region Booking Type

            var _bookingTypeName = "BTC-1 ACC-6518";
            var _bookingTypeId = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 1, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingTypeId, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeName1 = "CPSRT 6518-1";
            var _careProviderStaffRoleTypeId1 = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName1, "62921", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeId = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _active_SystemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeId1, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });
            var _notStarted_systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, null, _careProviderStaffRoleTypeId1, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region System User Employment Contract for Suspended status

            var systemUserEmploymentContractStartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);
            var _suspension_systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, systemUserEmploymentContractStartDate, _careProviderStaffRoleTypeId1, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_active_SystemUserEmploymentContractId, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_active_SystemUserEmploymentContractId, _bookingTypeId);

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_notStarted_systemUserEmploymentContractId, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_notStarted_systemUserEmploymentContractId, _bookingTypeId);

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_suspension_systemUserEmploymentContractId, _bookingTypeId).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_suspension_systemUserEmploymentContractId, _bookingTypeId);

            #endregion

            #region Availability Type

            var _availabilityTypeName = "Salaried/Contracted";
            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName(_availabilityTypeName).First();

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(_systemUserId, _teamId, _active_SystemUserEmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId, _teamId, _notStarted_systemUserEmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId, _teamId, _suspension_systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region Create System User Contract Suspension

            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date;
            dbHelper.systemUserSuspension.CreateSystemUserSuspension(_systemUserId, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, new List<Guid> { _suspension_systemUserEmploymentContractId });

            #endregion

            #region Step 24

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            var startYear = DateTime.Now.Date.Year.ToString();
            var startMonth = DateTime.Now.Date.ToString("MMMM");
            var startDay = DateTime.Now.Date.Day.ToString();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")
                .SelectStartDate(startYear, startMonth, startDay)
                .SelectEndDate(startYear, startMonth, startDay)
                .InsertStartTime("07", "00")
                .InsertEndTime("08", "00")
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(_systemUserFullName)
                .VerifyStaffRecordIsDisplayed(_active_SystemUserEmploymentContractId)
                .ClickStaffRecordCellText(_active_SystemUserEmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickAddUnassignedStaff()
                .ClickCreateBooking()
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("Employee Diary Booking User " + _currentDateSuffix + " has more than one contract; Diary Booking User " + _currentDateSuffix + ", 40.00 hrs, and Diary Booking User " + _currentDateSuffix + ". 40.00 hrs, Are you sure you are allocating the booking to the correct employment contract in line with the employees pay arrangement?")
                .ClickConfirmButton_WarningDialogue();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateBookingStatus("Booking created");

            System.Threading.Thread.Sleep(1500);
            var careProviderDiaryBooking = dbHelper.cPBookingDiary.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderDiaryBooking.Count);

            #endregion

            #region Step 25

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")
                .SelectStartDate(startYear, startMonth, startDay)
                .SelectEndDate(startYear, startMonth, startDay)
                .InsertStartTime("09", "00")
                .InsertEndTime("10", "00")
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(_systemUserFullName)
                .VerifyStaffRecordIsDisplayed(_suspension_systemUserEmploymentContractId)
                .ClickStaffRecordCellText(_suspension_systemUserEmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickAddUnassignedStaff()
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("" + _systemUserFullName + " - " + _careProviderStaffRoleTypeName1 + " contract is suspended at the selected booking time.")
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickOnCloseButton()
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("You have unsaved changes. Are you sure you want to close the drawer?")
                .ClickConfirmButton_WarningDialogue();

            careProviderDiaryBooking = dbHelper.cPBookingDiary.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderDiaryBooking.Count);

            #endregion

            #region Step 26

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")
                .SelectStartDate(startYear, startMonth, startDay)
                .SelectEndDate(startYear, startMonth, startDay)
                .InsertStartTime("11", "00")
                .InsertEndTime("12", "00")
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(_systemUserFullName)
                .VerifyStaffRecordIsDisplayed(_notStarted_systemUserEmploymentContractId)
                .ClickStaffRecordCellText(_notStarted_systemUserEmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickAddUnassignedStaff()
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("" + _systemUserFullName + " - " + _careProviderStaffRoleTypeName1 + " contract is not started at the selected booking time.")
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickOnCloseButton()
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("You have unsaved changes. Are you sure you want to close the drawer?")
                .ClickConfirmButton_WarningDialogue();

            careProviderDiaryBooking = dbHelper.cPBookingDiary.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderDiaryBooking.Count);

            #endregion

        }

        #endregion
    }
}

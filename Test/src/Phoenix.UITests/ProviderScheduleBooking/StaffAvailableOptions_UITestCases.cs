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
    public class StaffAvailableOptions_UITestCases : FunctionalTest
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
        private string _tenantName;
        private string EnvironmentName;

        internal Guid _recurrencePattern_Every1WeekMondayId;
        internal Guid _recurrencePattern_Every1WeekTuesdayId;
        internal Guid _recurrencePattern_Every1WeekWednesdayId;
        internal Guid _recurrencePattern_Every1WeekThursdayId;
        internal Guid _recurrencePattern_Every1WeekFridayId;
        internal Guid _recurrencePattern_Every1WeekSaturdayId;
        internal Guid _recurrencePattern_Every1WeekSundayId;

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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("Provider Schedule Booking BU");

                #endregion

                #region Team

                _teamName = "ProviderScheduleBookingT1";
                _teamId = commonMethodsDB.CreateTeam(_teamName, null, _businessUnitId, "107623", "ProviderScheduleBookingT1@careworkstempmail.com", "ProviderScheduleBookingT1", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region Create default system user

                _systemUsername = "StaffAvailableOptionUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "StaffAvailableOption", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

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

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5847

        [TestProperty("JiraIssueID", "ACC-5846")]
        [Description("Automation for step 1 to 8 from the original test ACC-5846 - when Diary Booking is successful.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Booking Type")]
        [TestProperty("Screen2", "Provider Schedule")]
        public void StaffAvailableOptions_UITestMethod001()
        {

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role5846" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName1 = "P5846a " + currentTimeString;
            var _providerId1 = commonMethodsDB.CreateProvider(_providerName1, _teamId, 12, true); // Training Provider

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "CarerE1" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "CarerE1", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId1, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1, 6, 0, 0), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { });

            #endregion

            #region Step 1 - 9

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2

            // Navigate to Booking Types Page
            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToBookingTypesPage();

            // Open new booking type record page
            bookingTypesPage
                .WaitForBookingTypesPageToLoad()
                .ClickNewRecordButton();

            // Create new booking type record
            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad()
                .InsertTextOnName("5846BT4_" + currentTimeString)
                .SelectBookingTypeClass("Booking (to internal non-care booking e.g. annual leave, training)")
                .SelectWorkingContractedTime("Count full booking length")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad()
                .ValidateBookingTypeClassSelectedText("Booking (to internal non-care booking e.g. annual leave, training)");

            #endregion

            #region Step 3

            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad()
                .ValidateStaffNonContactTimeSectionDisplayed(true);

            #endregion

            #region Step 4, 5, 6

            bookingTypeRecordPage
                .ValidateAssumeStaffAvailable_NoOptionChecked()
                .ValidateAssumeStaffAvailable_YesOptionNotChecked();

            #endregion

            #region Step 9

            var _bookingTypeId1 = dbHelper.cpBookingType.GetByName("5846BT4_" + currentTimeString)[0];

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId1, _bookingTypeId1, true);

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingTypeId1);

            #endregion

            //Navigate to Provider Schedule Page from Main Menu

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName1 + " - No Address")
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartTime("08", "00")
                .SetEndTime("12", "00")
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .ClickOnlyShowAvailableStaff()
                .EnterTextIntoFilterStaffByNameSearchBox("CarerE1 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("CarerE1 " + currentTimeString + ", " + "Role5846" + currentTimeString + ", 40.00 hrs, 01/01/2022 (Active) is not available at this time.")
                .ClickDismissButton_WarningDialogue();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickOnCloseButton()
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("You have unsaved changes. Are you sure you want to close the drawer?")
                .ClickConfirmButton_WarningDialogue()
                .WaitForCreateScheduleBookingPopupClosed();

            var careProviderBookingSchedules1 = dbHelper.cpBookingSchedule.GetByLocationId(_providerId1);
            Assert.AreEqual(0, careProviderBookingSchedules1.Count);

            #endregion

            #region Step 7

            var bookingTypeRecordId = dbHelper.cpBookingType.GetByName("5846BT4_" + currentTimeString)[0];

            //Navigate to Booking Types Page from Main Menu
            //Search and Open Booking Type Record Page
            //Click Assume Staff Available - Yes Option and save the record

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToBookingTypesPage();

            bookingTypesPage
                .WaitForBookingTypesPageToLoad()
                .InsertQuickSearchText("5846BT4_" + currentTimeString)
                .ClickQuickSearchButton()
                .WaitForBookingTypesPageToLoad()
                .OpenRecord(bookingTypeRecordId.ToString());

            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad()
                .ClickAssumeStaffAvailable_YesOption()
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForBookingTypeRecordPageToLoad()
                .ValidateAssumeStaffAvailable_YesOptionChecked()
                .ValidateAssumeStaffAvailable_NoOptionNotChecked();

            #endregion

            #region Step 8

            //Navigate to Provider Schedule Page from Main Menu

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName1 + " - No Address")
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SetStartTime("12", "00")
                .SetEndTime("16", "00")
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .ClickOnlyShowAvailableStaff()
                .EnterTextIntoFilterStaffByNameSearchBox("CarerE1 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1000);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            careProviderBookingSchedules1 = dbHelper.cpBookingSchedule.GetByLocationId(_providerId1);
            Assert.AreEqual(1, careProviderBookingSchedules1.Count);

            providerSchedulePage
                .ValidateScheduleBookingIsPresent(careProviderBookingSchedules1[0].ToString(), true);

            #endregion

            #region Step 10

            var _bookingTypeId2 = commonMethodsDB.CreateCPBookingType("5846BT1_" + currentTimeString, 1, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingTypeId3 = commonMethodsDB.CreateCPBookingType("5846BT2_" + currentTimeString, 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingTypeId4 = commonMethodsDB.CreateCPBookingType("5846BT3_" + currentTimeString, 3, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToBookingTypesPage();

            bookingTypesPage
                .WaitForBookingTypesPageToLoad()
                .InsertQuickSearchText("5846BT1_" + currentTimeString)
                .ClickQuickSearchButton()
                .WaitForBookingTypesPageToLoad()
                .OpenRecord(_bookingTypeId2.ToString());

            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad()
                .ValidateAssumeStaffAvailable_OptionsAreDisplayed(false)
                .ClickBackButton();

            bookingTypesPage
                .WaitForBookingTypesPageToLoad()
                .InsertQuickSearchText("5846BT2_" + currentTimeString)
                .ClickQuickSearchButton()
                .WaitForBookingTypesPageToLoad()
                .OpenRecord(_bookingTypeId3.ToString());

            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad()
                .ValidateAssumeStaffAvailable_OptionsAreDisplayed(false)
                .ClickBackButton();

            bookingTypesPage
                .WaitForBookingTypesPageToLoad()
                .InsertQuickSearchText("5846BT3_" + currentTimeString)
                .ClickQuickSearchButton()
                .WaitForBookingTypesPageToLoad()
                .OpenRecord(_bookingTypeId4.ToString());

            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad()
                .ValidateAssumeStaffAvailable_OptionsAreDisplayed(false);

            #region Express Book for Provider

            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId1, commonMethodsHelper.GetWeekFirstMonday(todayDate), commonMethodsHelper.GetWeekFirstMonday(todayDate).AddDays(6), commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId1, "provider", "P5846 " + currentTimeString);

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

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.GetByLocationId(_providerId1)[0];

            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(_expressBookingCriteriaId1);
            Assert.AreEqual(0, expressBookingResultId.Count);

            var cpDiaryBookingId = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            Assert.AreEqual(1, cpDiaryBookingId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName1 + " - No Address", _providerId1)
                .WaitForProviderDiaryPageToLoad();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(cpDiaryBookingId[0].ToString(), true);

            #endregion

        }

        #endregion

    }
}

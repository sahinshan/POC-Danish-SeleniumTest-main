using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.ProviderScheduleBooking
{
    /// <summary>
    /// This class contains Automated UI test scripts for Provider Schedule Booking - Double Booking Checks
    /// </summary>
    [TestClass]
    public class ScheduleBooking_DoubleBookingChecks_UITestCases : FunctionalTest
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

        internal Guid _recurrencePattern_Every1WeekMondayId;
        internal Guid _recurrencePattern_Every1WeekTuesdayId;
        internal Guid _recurrencePattern_Every1WeekWednesdayId;
        internal Guid _recurrencePattern_Every1WeekThursdayId;
        internal Guid _recurrencePattern_Every1WeekFridayId;
        internal Guid _recurrencePattern_Every1WeekSaturdayId;
        internal Guid _recurrencePattern_Every1WeekSundayId;

        #endregion

        #region Inernal Methods

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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("Care Providers EBDC");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                teamName = "Care Providers EBDC";
                _teamId = commonMethodsDB.CreateTeam(teamName, null, _businessUnitId, "90400", "CareProvidersEBDC@careworkstempmail.com", teamName, "020 125556");

                #endregion

                #region Create default system user

                _loginUser_Username = "ProviderScheduleDoubleBookingCheck_User_1";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUser_Username, "ProviderScheduleDoubleBookingCheck", "User_1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

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

        #region https://advancedcsg.atlassian.net/browse/ACC-6342

        [TestProperty("JiraIssueID", "ACC-6343")]
        [Description("Step(s) 1 to 4 from the original test ACC-6341")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ScheduleBooking_DoubleBookingCheck_ACC_6431_UITestCases001()
        {

            #region Provider

            var providerName = "P6343 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Booking Type


            var _bookingTypeBTC1 = commonMethodsDB.CreateCPBookingType("BTC1 ACC-6343", 1, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 1);
            var _bookingTypeBTC2 = commonMethodsDB.CreateCPBookingType("BTC2 ACC-6343", 2, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 2, false, 1000, null, null, null);
            var _bookingTypeBTC3 = commonMethodsDB.CreateCPBookingType("BTC3 ACC-6343", 3, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 1);


            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC1, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC2, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC3, false);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6343", "99910", null, new DateTime(2020, 1, 1), null);

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
            var user1name = "cpsu_2_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 2 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingTypeBTC3);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC1, 2).FirstOrDefault(); //Booking (to location) > Booking (to internal care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 3); //Prevent

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC1, 3).FirstOrDefault(); //Booking (to location) > Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC2, 1).FirstOrDefault(); //Booking (to internal care activity) > Booking (to location)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 2); //Warn Only

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC2, 3).FirstOrDefault(); //Booking (to internal care activity) > Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 3); //Prevent

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC3, 2).FirstOrDefault(); //Booking (to extermal care activity) > Booking (to intenal care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 2); //Warn Only

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC3, 1).FirstOrDefault(); //Booking (to internal care activity) > Booking (to location)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 3); //Prevent

            #endregion

            #region Step 1 to Step 4

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
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC1 ACC-6343")
                .SetStartDay("Tuesday")
                .SetStartTime("09", "00")
                .SetEndTime("15", "00")
                .SetEndDay("Tuesday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId1 = careProviderBookingSchedules[0];

            var cpBookingScheduleStaffs = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId1);
            Assert.AreEqual(1, cpBookingScheduleStaffs.Count);
            var fields = dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), fields["systemuseremploymentcontractid"].ToString());

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC2 ACC-6343")
                .SetStartDay("Tuesday")
                .SetStartTime("10", "00")
                .SetEndTime("11", "00")
                .SetEndDay("Tuesday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickOnlyShowAvailableStaff()
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(user1FirstName + " " + user1LastName + " already has a regular booking in the schedule at this time.")
                .ClickDismissButton_DynamicDialogue();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);


            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC3 ACC-6343")
                .SetStartTime("12", "00")
                .SetEndTime("14", "00");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(user1FirstName + " " + user1LastName + " already has a regular booking in the schedule at this time.")
                .ClickDismissButton_WarningDialogue();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickOnCloseButton()
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("You have unsaved changes. Are you sure you want to close the drawer?")
                .ClickConfirmButton_WarningDialogue();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            #region Booking Type Clash Action

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC1, 2).FirstOrDefault(); //Booking (to location) > Booking (to internal care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC1, 3).FirstOrDefault(); //Booking (to location) > Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC2, 1).FirstOrDefault(); //Booking (to internal care activity) > Booking (to location)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC2, 3).FirstOrDefault(); //Booking (to internal care activity) > Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC3, 2).FirstOrDefault(); //Booking (to extermal care activity) > Booking (to intenal care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC3, 1).FirstOrDefault(); //Booking (to internal care activity) > Booking (to location)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC1 ACC-6343")
                .SetStartDay("Wednesday")
                .SetStartTime("09", "00")
                .SetEndTime("15", "00")
                .SetEndDay("Wednesday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId2 = careProviderBookingSchedules[0];

            cpBookingScheduleStaffs = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId2);
            Assert.AreEqual(1, cpBookingScheduleStaffs.Count);
            fields = dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), fields["systemuseremploymentcontractid"].ToString());

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC2 ACC-6343")
                .SetStartDay("Wednesday")
                .SetStartTime("10", "00")
                .SetEndTime("11", "00")
                .SetEndDay("Wednesday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickOnlyShowAvailableStaff()
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(3, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId3 = careProviderBookingSchedules[0];

            cpBookingScheduleStaffs = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId3);
            Assert.AreEqual(1, cpBookingScheduleStaffs.Count);
            fields = dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), fields["systemuseremploymentcontractid"].ToString());

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC3 ACC-6343")
                .SetStartDay("Wednesday")
                .SetStartTime("12", "00")
                .SetEndTime("14", "00")
                .SetEndDay("Wednesday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickOnlyShowAvailableStaff()
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1500);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(4, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId4 = careProviderBookingSchedules[0];

            cpBookingScheduleStaffs = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId4);
            Assert.AreEqual(1, cpBookingScheduleStaffs.Count);
            fields = dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), fields["systemuseremploymentcontractid"].ToString());

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-6355")]
        [Description("Step(s) 5 to 6 from the original test ACC-6341")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ScheduleBooking_DoubleBookingCheck_ACC_6431_UITestCases002()
        {

            #region Provider

            var providerName = "P6355 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Booking Type

            var _bookingTypeBTC1 = commonMethodsDB.CreateCPBookingType("BTC1 ACC-6355", 1, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 1);
            var _bookingTypeBTC2 = commonMethodsDB.CreateCPBookingType("BTC2 ACC-6355", 2, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 2, false, 1000, null, null, null);
            var _bookingTypeBTC3 = commonMethodsDB.CreateCPBookingType("BTC3 ACC-6355", 3, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC1, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC2, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC3, false);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6355", "99910", null, new DateTime(2020, 1, 1), null);

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
            var user1name = "cpsu_2_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 2 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingTypeBTC3);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC1, 2).FirstOrDefault(); //Booking (to location) > Booking (to internal care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC1, 3).FirstOrDefault(); //Booking (to location) > Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 2); //Warn Only

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC2, 1).FirstOrDefault(); //Booking (to internal care activity) > Booking (to location)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 2); //Warn Only

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC2, 3).FirstOrDefault(); //Booking (to internal care activity) > Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC3, 2).FirstOrDefault(); //Booking (to extermal care activity) > Booking (to intenal care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 2); //Warn Only

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC3, 1).FirstOrDefault(); //Booking (to internal care activity) > Booking (to location)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            #endregion

            #region Step 5 to Step 6

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
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC1 ACC-6355")
                .SetStartDay("Thursday")
                .SetStartTime("09", "00")
                .SetEndTime("15", "00")
                .SetEndDay("Thursday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId1 = careProviderBookingSchedules[0];

            var cpBookingScheduleStaffs = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId1);
            Assert.AreEqual(1, cpBookingScheduleStaffs.Count);
            var fields = dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), fields["systemuseremploymentcontractid"].ToString());

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC2 ACC-6355")
                .SetStartDay("Thursday")
                .SetStartTime("10", "00")
                .SetEndTime("11", "00")
                .SetEndDay("Thursday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickOnlyShowAvailableStaff()
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(user1FirstName + " " + user1LastName + " already has a regular booking in the schedule at this time. Do you want to create this booking anyway?")
                .ClickDismissButton_DynamicDialogue();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(user1FirstName + " " + user1LastName + " already has a regular booking in the schedule at this time. Do you want to create this booking anyway?")
                .ClickSaveButton_DynamicDialogue();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId2 = careProviderBookingSchedules[0];

            cpBookingScheduleStaffs = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId2);
            Assert.AreEqual(1, cpBookingScheduleStaffs.Count);
            fields = dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), fields["systemuseremploymentcontractid"].ToString());

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC3 ACC-6355")
                .SetStartDay("Thursday")
                .SetStartTime("12", "00")
                .SetEndTime("14", "00")
                .SetEndDay("Thursday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickOnlyShowAvailableStaff()
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(user1FirstName + " " + user1LastName + " already has a regular booking in the schedule at this time. Do you want to create this booking anyway?")
                .ClickDismissButton_DynamicDialogue();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(user1FirstName + " " + user1LastName + " already has a regular booking in the schedule at this time. Do you want to create this booking anyway?")
                .ClickSaveButton_DynamicDialogue();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(3, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId3 = careProviderBookingSchedules[0];

            cpBookingScheduleStaffs = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId3);
            Assert.AreEqual(1, cpBookingScheduleStaffs.Count);
            fields = dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), fields["systemuseremploymentcontractid"].ToString());

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-6356")]
        [Description("Step(s) 7 to 9 from the original test ACC-6341")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ScheduleBooking_DoubleBookingCheck_ACC_6431_UITestCases003()
        {

            #region Provider

            var providerName = "P6356 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Booking Type

            var _bookingTypeBTC1 = commonMethodsDB.CreateCPBookingType("BTC1 ACC-6356", 1, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 1);
            var _bookingTypeBTC2 = commonMethodsDB.CreateCPBookingType("BTC2 ACC-6356", 2, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 2, false, 1000, null, null, null);
            var _bookingTypeBTC3 = commonMethodsDB.CreateCPBookingType("BTC3 ACC-6356", 3, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC1, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC2, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC3, false);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6356", "99910", null, new DateTime(2020, 1, 1), null);

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
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "CP1";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            var user2name = "cpsu_2_" + currentTimeString;
            var user2FirstName = "CP2";
            var user2LastName = "System User 2 " + currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser2Id, commonMethodsHelper.GetThisWeekFirstMonday());

            var user3name = "cpsu_3_" + currentTimeString;
            var user3FirstName = "CP3";
            var user3LastName = "System User 3 " + currentTimeString;
            var systemUser3Id = commonMethodsDB.CreateSystemUserRecord(user3name, user3FirstName, user3LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser3Id, commonMethodsHelper.GetThisWeekFirstMonday());

            var user4name = "cpsu_4_" + currentTimeString;
            var user4FirstName = "CP4";
            var user4LastName = "System User 4 " + currentTimeString;
            var systemUser4Id = commonMethodsDB.CreateSystemUserRecord(user4name, user4FirstName, user4LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser4Id, commonMethodsHelper.GetThisWeekFirstMonday());

            var user5name = "cpsu_5_" + currentTimeString;
            var user5FirstName = "CP5";
            var user5LastName = "System User 5 " + currentTimeString;
            var systemUser5Id = commonMethodsDB.CreateSystemUserRecord(user5name, user5FirstName, user5LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser5Id, commonMethodsHelper.GetThisWeekFirstMonday());

            var user6name = "cpsu_6_" + currentTimeString;
            var user6FirstName = "CP6";
            var user6LastName = "System User 6 " + currentTimeString;
            var systemUser6Id = commonMethodsDB.CreateSystemUserRecord(user6name, user6FirstName, user6LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser6Id, commonMethodsHelper.GetThisWeekFirstMonday());


            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser2EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser2Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser3EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser3Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser4EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser4Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser5EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser5Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser6EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser6Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingTypeBTC3);

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingTypeBTC3);

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContractId, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContractId, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContractId, _bookingTypeBTC3);

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser4EmploymentContractId, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser4EmploymentContractId, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser4EmploymentContractId, _bookingTypeBTC3);

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser5EmploymentContractId, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser5EmploymentContractId, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser5EmploymentContractId, _bookingTypeBTC3);

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser6EmploymentContractId, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser6EmploymentContractId, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser6EmploymentContractId, _bookingTypeBTC3);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser2Id, _teamId, _systemUser2EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser3Id, _teamId, _systemUser3EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser4Id, _teamId, _systemUser4EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser5Id, _teamId, _systemUser5EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser6Id, _teamId, _systemUser6EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC1, 2).FirstOrDefault(); //Booking (to location) > Booking (to internal care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC1, 3).FirstOrDefault(); //Booking (to location) > Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 2); //Warn Only

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC2, 1).FirstOrDefault(); //Booking (to internal care activity) > Booking (to location)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 2); //Warn Only

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC2, 3).FirstOrDefault(); //Booking (to internal care activity) > Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC3, 2).FirstOrDefault(); //Booking (to extermal care activity) > Booking (to intenal care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 2); //Warn Only

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC3, 1).FirstOrDefault(); //Booking (to internal care activity) > Booking (to location)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 3); //Prevent

            #endregion

            #region Step 7 to Step 9

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
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC1 ACC-6356")
                .SetStartDay("Friday")
                .SetStartTime("09", "00")
                .SetEndTime("15", "00")
                .SetEndDay("Friday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser2EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser3EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            System.Threading.Thread.Sleep(1200);

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId1 = careProviderBookingSchedules[0];

            var cpBookingScheduleStaffs = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId1);
            Assert.AreEqual(3, cpBookingScheduleStaffs.Count);
            List<string> contracts = new List<string>();
            contracts.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[0], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            contracts.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[1], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            contracts.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[2], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            Assert.IsTrue(contracts.Contains(_systemUser1EmploymentContractId.ToString()));
            Assert.IsTrue(contracts.Contains(_systemUser2EmploymentContractId.ToString()));
            Assert.IsTrue(contracts.Contains(_systemUser3EmploymentContractId.ToString()));

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC2 ACC-6356")
                .SetStartDay("Friday")
                .SetStartTime("10", "00")
                .SetEndTime("11", "00")
                .SetEndDay("Friday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUser4EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser5EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser6EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1200);

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId2 = careProviderBookingSchedules[0];

            cpBookingScheduleStaffs = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId2);
            Assert.AreEqual(3, cpBookingScheduleStaffs.Count);
            contracts = new List<string>();
            contracts.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[0], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            contracts.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[1], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            contracts.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[2], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            Assert.IsTrue(contracts.Contains(_systemUser4EmploymentContractId.ToString()));
            Assert.IsTrue(contracts.Contains(_systemUser5EmploymentContractId.ToString()));
            Assert.IsTrue(contracts.Contains(_systemUser6EmploymentContractId.ToString()));

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC3 ACC-6356")
                .SetStartDay("Friday")
                .SetStartTime("12", "00")
                .SetEndTime("14", "00")
                .SetEndDay("Friday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickOnlyShowAvailableStaff()
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser3EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser4EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1200);

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(user1FirstName + " " + user1LastName + " and " + user3FirstName + " " + user3LastName + " already have regular bookings in the schedule at this time.")
                .ClickDismissButton_DynamicDialogue();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            #endregion

            #region Booking Type Clash Action

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC1, 3).FirstOrDefault(); //Booking (to location) > Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC3, 1).FirstOrDefault(); //Booking (to internal care activity) > Booking (to location)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 2); //Warn Only

            #endregion

            createScheduleBookingPopup
                .ClickOnCloseButton()
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("You have unsaved changes. Are you sure you want to close the drawer?")
                .ClickConfirmButton_WarningDialogue();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC2 ACC-6356")
                .SetStartDay("Friday")
                .SetStartTime("10", "00")
                .SetEndTime("11", "00")
                .SetEndDay("Friday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickOnlyShowAvailableStaff()
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser2EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser3EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(user1FirstName + " " + user1LastName + ", " + user2FirstName + " " + user2LastName + " and " + user3FirstName + " " + user3LastName + " already have regular bookings in the schedule at this time. Do you want to create this booking anyway?")
                .ClickSaveButton_DynamicDialogue();

            System.Threading.Thread.Sleep(1200);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(3, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId3 = careProviderBookingSchedules[0];

            cpBookingScheduleStaffs = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId3);
            Assert.AreEqual(3, cpBookingScheduleStaffs.Count);
            contracts = new List<string>();
            contracts.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[0], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            contracts.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[1], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            contracts.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[2], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            Assert.IsTrue(contracts.Contains(_systemUser1EmploymentContractId.ToString()));
            Assert.IsTrue(contracts.Contains(_systemUser2EmploymentContractId.ToString()));
            Assert.IsTrue(contracts.Contains(_systemUser3EmploymentContractId.ToString()));

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC3 ACC-6356")
                .SetStartDay("Friday")
                .SetStartTime("12", "00")
                .SetEndTime("14", "00")
                .SetEndDay("Friday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickOnlyShowAvailableStaff()
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser2EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser3EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(user1FirstName + " " + user1LastName + ", " + user2FirstName + " " + user2LastName + " and " + user3FirstName + " " + user3LastName + " already have regular bookings in the schedule at this time. Do you want to create this booking anyway?")
                .ClickSaveButton_DynamicDialogue();

            System.Threading.Thread.Sleep(1200);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(4, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId4 = careProviderBookingSchedules[0];

            cpBookingScheduleStaffs = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId4);
            Assert.AreEqual(3, cpBookingScheduleStaffs.Count);
            contracts = new List<string>();
            contracts.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[0], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            contracts.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[1], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            contracts.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[2], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            Assert.IsTrue(contracts.Contains(_systemUser1EmploymentContractId.ToString()));
            Assert.IsTrue(contracts.Contains(_systemUser2EmploymentContractId.ToString()));
            Assert.IsTrue(contracts.Contains(_systemUser3EmploymentContractId.ToString()));

            #region Booking Type Clash Action

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC1, 2).FirstOrDefault(); //Booking (to location) > Booking (to internal care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC1, 3).FirstOrDefault(); //Booking (to location) > Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC2, 1).FirstOrDefault(); //Booking (to internal care activity) > Booking (to location)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC2, 3).FirstOrDefault(); //Booking (to internal care activity) > Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC3, 2).FirstOrDefault(); //Booking (to extermal care activity) > Booking (to intenal care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC3, 1).FirstOrDefault(); //Booking (to internal care activity) > Booking (to location)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC1 ACC-6356")
                .SetStartDay("Sunday")
                .SetStartTime("09", "00")
                .SetEndTime("15", "00")
                .SetEndDay("Sunday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser2EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser3EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            System.Threading.Thread.Sleep(1200);

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(5, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId5 = careProviderBookingSchedules[0];

            cpBookingScheduleStaffs = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId5);
            Assert.AreEqual(3, cpBookingScheduleStaffs.Count);
            contracts = new List<string>();
            contracts.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[0], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            contracts.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[1], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            contracts.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[2], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            Assert.IsTrue(contracts.Contains(_systemUser1EmploymentContractId.ToString()));
            Assert.IsTrue(contracts.Contains(_systemUser2EmploymentContractId.ToString()));
            Assert.IsTrue(contracts.Contains(_systemUser3EmploymentContractId.ToString()));

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC2 ACC-6356")
                .SetStartDay("Sunday")
                .SetStartTime("10", "00")
                .SetEndTime("11", "00")
                .SetEndDay("Sunday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickOnlyShowAvailableStaff()
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser2EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser3EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            System.Threading.Thread.Sleep(1500);

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(6, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId6 = careProviderBookingSchedules[0];

            cpBookingScheduleStaffs = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId6);
            Assert.AreEqual(3, cpBookingScheduleStaffs.Count);
            contracts = new List<string>();
            contracts.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[0], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            contracts.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[1], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            contracts.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[2], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            Assert.IsTrue(contracts.Contains(_systemUser1EmploymentContractId.ToString()));
            Assert.IsTrue(contracts.Contains(_systemUser2EmploymentContractId.ToString()));
            Assert.IsTrue(contracts.Contains(_systemUser3EmploymentContractId.ToString()));

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC3 ACC-6356")
                .SetStartDay("Sunday")
                .SetStartTime("12", "00")
                .SetEndTime("14", "00")
                .SetEndDay("Sunday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickOnlyShowAvailableStaff()
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser2EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser3EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            System.Threading.Thread.Sleep(1500);

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(7, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId7 = careProviderBookingSchedules[0];

            cpBookingScheduleStaffs = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId7);
            Assert.AreEqual(3, cpBookingScheduleStaffs.Count);
            contracts = new List<string>();
            contracts.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[0], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            contracts.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[1], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            contracts.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[2], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            Assert.IsTrue(contracts.Contains(_systemUser1EmploymentContractId.ToString()));
            Assert.IsTrue(contracts.Contains(_systemUser2EmploymentContractId.ToString()));
            Assert.IsTrue(contracts.Contains(_systemUser3EmploymentContractId.ToString()));
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-6440

        [TestProperty("JiraIssueID", "ACC-6442")]
        [Description("Step(s) 10 from the original test ACC-6341")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ScheduleBooking_DoubleBookingCheck_ACC_6431_UITestCases004()
        {

            #region Provider

            var providerName = "P6442 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Booking Type

            var _bookingTypeBTC1 = commonMethodsDB.CreateCPBookingType("BTC1 ACC-6442", 1, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 1);
            var _bookingTypeBTC2 = commonMethodsDB.CreateCPBookingType("BTC2 ACC-6442", 2, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 2, false, 1000, null, null, null);
            var _bookingTypeBTC3 = commonMethodsDB.CreateCPBookingType("BTC3 ACC-6442", 3, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC1, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC2, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC3, false);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6442", "99910", null, new DateTime(2020, 1, 1), null);

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
            var user1name = "cpsu_1a_" + currentTimeString;
            var user1FirstName = "CP1a";
            var user1LastName = "System User 1a " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            var user2name = "cpsu_2a_" + currentTimeString;
            var user2FirstName = "CP2a";
            var user2LastName = "System User 2a " + currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser2Id, commonMethodsHelper.GetThisWeekFirstMonday());

            var user3name = "cpsu_3a_" + currentTimeString;
            var user3FirstName = "CP3a";
            var user3LastName = "System User 3a " + currentTimeString;
            var systemUser3Id = commonMethodsDB.CreateSystemUserRecord(user3name, user3FirstName, user3LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser3Id, commonMethodsHelper.GetThisWeekFirstMonday());

            var user4name = "cpsu_4a_" + currentTimeString;
            var user4FirstName = "CP4a";
            var user4LastName = "System User 4a " + currentTimeString;
            var systemUser4Id = commonMethodsDB.CreateSystemUserRecord(user4name, user4FirstName, user4LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser4Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser2EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser2Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser3EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser3Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser4EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser4Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingTypeBTC3);

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingTypeBTC3);

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContractId, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContractId, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContractId, _bookingTypeBTC3);

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser4EmploymentContractId, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser4EmploymentContractId, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser4EmploymentContractId, _bookingTypeBTC3);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser2Id, _teamId, _systemUser2EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser3Id, _teamId, _systemUser3EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser4Id, _teamId, _systemUser4EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC1, 2).FirstOrDefault(); //Booking (to location) > Booking (to internal care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 3); //Prevent

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC1, 3).FirstOrDefault(); //Booking (to location) > Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 2); //Warn Only

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC2, 1).FirstOrDefault(); //Booking (to internal care activity) > Booking (to location)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC2, 3).FirstOrDefault(); //Booking (to internal care activity) > Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 3); //Prevent

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC3, 2).FirstOrDefault(); //Booking (to extermal care activity) > Booking (to intenal care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC3, 1).FirstOrDefault(); //Booking (to internal care activity) > Booking (to location)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 3); //Prevent

            #endregion

            #region Step 10

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
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC1 ACC-6442")
                .SetStartDay("Monday")
                .SetStartTime("09", "00")
                .SetEndTime("13", "00")
                .SetEndDay("Monday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser2EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser3EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            System.Threading.Thread.Sleep(1200);

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId1 = careProviderBookingSchedules[0];

            var cpBookingScheduleStaffs = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId1);
            Assert.AreEqual(3, cpBookingScheduleStaffs.Count);
            var cpBookingScheduleStaffIds = new List<string>();

            cpBookingScheduleStaffIds.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[0], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            cpBookingScheduleStaffIds.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[1], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            cpBookingScheduleStaffIds.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[2], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());

            Assert.IsTrue(cpBookingScheduleStaffIds.Contains(_systemUser1EmploymentContractId.ToString()));
            Assert.IsTrue(cpBookingScheduleStaffIds.Contains(_systemUser2EmploymentContractId.ToString()));
            Assert.IsTrue(cpBookingScheduleStaffIds.Contains(_systemUser3EmploymentContractId.ToString()));

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            var nextDueDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);
            string nextDueDate_Year = nextDueDate.Year.ToString();
            string nextDueDate_Month = nextDueDate.ToString("MMMM");
            string nextDueDate_Date = nextDueDate.Day.ToString();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC2 ACC-6442")
                .SetStartDay("Monday")
                .SetStartTime("11", "00")
                .SetEndTime("13", "00")
                .SetEndDay("Monday")
                .ClickOccurrenceTab()
                .SelectBookingTakesPlaceEvery("2 weeks")
                .SelectNextDueToTakePlaceDate(nextDueDate_Year, nextDueDate_Month, nextDueDate_Date)
                .ClickRosteringTab()
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickOnlyShowAvailableStaff()
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser2EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser4EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(user1FirstName + " " + user1LastName + " and " + user2FirstName + " " + user2LastName + " already have regular bookings in the schedule at this time.")
                .ClickDismissButton_DynamicDialogue();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            createScheduleBookingPopup
                .ClickOnCloseButton()
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("You have unsaved changes. Are you sure you want to close the drawer?")
                .ClickConfirmButton_WarningDialogue();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC3 ACC-6442")
                .SetStartDay("Monday")
                .SetStartTime("12", "00")
                .SetEndTime("14", "00")
                .SetEndDay("Monday")
                .ClickOccurrenceTab()
                .SelectBookingTakesPlaceEvery("2 weeks")
                .SelectNextDueToTakePlaceDate(nextDueDate_Year, nextDueDate_Month, nextDueDate_Date)
                .ClickRosteringTab()
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickOnlyShowAvailableStaff()
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser2EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser4EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(user1FirstName + " " + user1LastName + " and " + user2FirstName + " " + user2LastName + " already have regular bookings in the schedule at this time.")

                .ClickDismissButton_DynamicDialogue();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-6444")]
        [Description("Step 11 from the original test ACC-6341")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ScheduleBooking_DoubleBookingCheck_ACC_6431_UITestCases005()
        {
            #region Next Due Date

            var nextDueDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(21);
            string nextDueDate_Year = nextDueDate.Year.ToString();
            string nextDueDate_Month = nextDueDate.ToString("MMMM");
            string nextDueDate_Date = nextDueDate.Day.ToString();

            var nextDueDate2 = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(63);
            string nextDueDate_Year2 = nextDueDate2.Year.ToString();
            string nextDueDate_Month2 = nextDueDate2.ToString("MMMM");
            string nextDueDate_Date2 = nextDueDate2.Day.ToString();

            #endregion

            #region Provider

            var providerName = "P6444 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Booking Type

            var _bookingTypeBTC1 = commonMethodsDB.CreateCPBookingType("BTC1 ACC-6444", 1, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 1);
            var _bookingTypeBTC2 = commonMethodsDB.CreateCPBookingType("BTC2 ACC-6444", 2, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 2, false, 1000, null, null, null);
            var _bookingTypeBTC3 = commonMethodsDB.CreateCPBookingType("BTC3 ACC-6444", 3, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC1, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC2, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC3, false);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6444", "99910", null, new DateTime(2020, 1, 1), null);

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
            var user1name = "cpsu_1b_" + currentTimeString;
            var user1FirstName = "CP1b";
            var user1LastName = "System User 1b " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            var user2name = "cpsu_2b_" + currentTimeString;
            var user2FirstName = "CP2b";
            var user2LastName = "System User 2b " + currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser2Id, commonMethodsHelper.GetThisWeekFirstMonday());

            var user3name = "cpsu_3b_" + currentTimeString;
            var user3FirstName = "CP3b";
            var user3LastName = "System User 3b " + currentTimeString;
            var systemUser3Id = commonMethodsDB.CreateSystemUserRecord(user3name, user3FirstName, user3LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser3Id, commonMethodsHelper.GetThisWeekFirstMonday());

            var user4name = "cpsu_4b_" + currentTimeString;
            var user4FirstName = "CP4b";
            var user4LastName = "System User 4b " + currentTimeString;
            var systemUser4Id = commonMethodsDB.CreateSystemUserRecord(user4name, user4FirstName, user4LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser4Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser2EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser2Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser3EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser3Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser4EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser4Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingTypeBTC3);

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingTypeBTC3);

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContractId, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContractId, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContractId, _bookingTypeBTC3);

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser4EmploymentContractId, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser4EmploymentContractId, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser4EmploymentContractId, _bookingTypeBTC3);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser2Id, _teamId, _systemUser2EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser3Id, _teamId, _systemUser3EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser4Id, _teamId, _systemUser4EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC1, 2).FirstOrDefault(); //Booking (to location) > Booking (to internal care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 3); //Prevent

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC1, 3).FirstOrDefault(); //Booking (to location) > Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 2); //Warn Only

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC2, 1).FirstOrDefault(); //Booking (to internal care activity) > Booking (to location)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC2, 3).FirstOrDefault(); //Booking (to internal care activity) > Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 3); //Prevent

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC3, 2).FirstOrDefault(); //Booking (to extermal care activity) > Booking (to intenal care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC3, 1).FirstOrDefault(); //Booking (to internal care activity) > Booking (to location)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 2); //Warn Only

            #endregion

            #region Step 11

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
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC3 ACC-6444")
                .SetStartDay("Monday")
                .SetStartTime("15", "30")
                .SetEndTime("18", "30")
                .SetEndDay("Monday")
                .ClickOccurrenceTab()
                .SelectBookingTakesPlaceEvery("3 weeks")
                .SelectNextDueToTakePlaceDate(nextDueDate_Year, nextDueDate_Month, nextDueDate_Date)
                .ClickRosteringTab()
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser2EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser3EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1200);

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId1 = careProviderBookingSchedules[0];

            var cpBookingScheduleStaffs = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId1);
            Assert.AreEqual(3, cpBookingScheduleStaffs.Count);
            var cpBookingScheduleStaffIds = new List<string>();


            cpBookingScheduleStaffIds.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[0], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            cpBookingScheduleStaffIds.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[1], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            cpBookingScheduleStaffIds.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[2], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());

            Assert.IsTrue(cpBookingScheduleStaffIds.Contains(_systemUser1EmploymentContractId.ToString()));
            Assert.IsTrue(cpBookingScheduleStaffIds.Contains(_systemUser2EmploymentContractId.ToString()));
            Assert.IsTrue(cpBookingScheduleStaffIds.Contains(_systemUser3EmploymentContractId.ToString()));

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC1 ACC-6444")
                .SetStartDay("Monday")
                .SetStartTime("16", "00")
                .SetEndTime("17", "00")
                .SetEndDay("Monday")
                .ClickOccurrenceTab()
                .SelectBookingTakesPlaceEvery("9 weeks")
                .SelectNextDueToTakePlaceDate(nextDueDate_Year2, nextDueDate_Month2, nextDueDate_Date2)
                .ClickRosteringTab()
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickOnlyShowAvailableStaff()
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser2EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser4EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(user1FirstName + " " + user1LastName + " and " + user2FirstName + " " + user2LastName + " already have regular bookings in the schedule at this time. Do you want to create this booking anyway?")
                .ClickDismissButton_DynamicDialogue();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(user1FirstName + " " + user1LastName + " and " + user2FirstName + " " + user2LastName + " already have regular bookings in the schedule at this time. Do you want to create this booking anyway?")
                .ClickSaveButton_DynamicDialogue();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId2 = careProviderBookingSchedules[0];

            cpBookingScheduleStaffs = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId2);
            Assert.AreEqual(3, cpBookingScheduleStaffs.Count);
            cpBookingScheduleStaffIds = new List<string>();

            cpBookingScheduleStaffIds.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[0], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            cpBookingScheduleStaffIds.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[1], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            cpBookingScheduleStaffIds.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[2], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());

            Assert.IsTrue(cpBookingScheduleStaffIds.Contains(_systemUser1EmploymentContractId.ToString()));
            Assert.IsTrue(cpBookingScheduleStaffIds.Contains(_systemUser2EmploymentContractId.ToString()));
            Assert.IsTrue(cpBookingScheduleStaffIds.Contains(_systemUser4EmploymentContractId.ToString()));

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-6446")]
        [Description("Step 12 from the original test ACC-6341")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ScheduleBooking_DoubleBookingCheck_ACC_6431_UITestCases006()
        {
            #region Next Due Date

            var nextDueDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(28);
            nextDueDate = commonMethodsHelper.GetDayOfWeek(nextDueDate, DayOfWeek.Tuesday);
            string nextDueDate_Year = nextDueDate.Year.ToString();
            string nextDueDate_Month = nextDueDate.ToString("MMMM");
            string nextDueDate_Date = nextDueDate.Day.ToString();

            #endregion

            #region Provider

            var providerName = "P6446 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Booking Type

            var _bookingTypeBTC1 = commonMethodsDB.CreateCPBookingType("BTC1 ACC-6446", 1, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 1);
            var _bookingTypeBTC2 = commonMethodsDB.CreateCPBookingType("BTC2 ACC-6446", 2, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 2, false, 1000, null, null, null);
            var _bookingTypeBTC3 = commonMethodsDB.CreateCPBookingType("BTC3 ACC-6446", 3, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC1, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC2, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC3, false);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6446", "99910", null, new DateTime(2020, 1, 1), null);

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
            var user1name = "cpsu_1c_" + currentTimeString;
            var user1FirstName = "CP1c";
            var user1LastName = "System User 1c " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            var user2name = "cpsu_2c_" + currentTimeString;
            var user2FirstName = "CP2c";
            var user2LastName = "System User 2c " + currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser2Id, commonMethodsHelper.GetThisWeekFirstMonday());

            var user3name = "cpsu_3c_" + currentTimeString;
            var user3FirstName = "CP3c";
            var user3LastName = "System User 3c " + currentTimeString;
            var systemUser3Id = commonMethodsDB.CreateSystemUserRecord(user3name, user3FirstName, user3LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser3Id, commonMethodsHelper.GetThisWeekFirstMonday());

            var user4name = "cpsu_4c_" + currentTimeString;
            var user4FirstName = "CP4c";
            var user4LastName = "System User 4c " + currentTimeString;
            var systemUser4Id = commonMethodsDB.CreateSystemUserRecord(user4name, user4FirstName, user4LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser4Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser2EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser2Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser3EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser3Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser4EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser4Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingTypeBTC3);

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingTypeBTC3);

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContractId, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContractId, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContractId, _bookingTypeBTC3);

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser4EmploymentContractId, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser4EmploymentContractId, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser4EmploymentContractId, _bookingTypeBTC3);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser2Id, _teamId, _systemUser2EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser3Id, _teamId, _systemUser3EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser4Id, _teamId, _systemUser4EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC1, 2).FirstOrDefault(); //Booking (to location) > Booking (to internal care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 2); //Warn Only

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC1, 3).FirstOrDefault(); //Booking (to location) > Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 2); //Warn Only

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC2, 1).FirstOrDefault(); //Booking (to internal care activity) > Booking (to location)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC2, 3).FirstOrDefault(); //Booking (to internal care activity) > Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 3); //Prevent

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC3, 2).FirstOrDefault(); //Booking (to extermal care activity) > Booking (to intenal care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 2); //Warn Only

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC3, 1).FirstOrDefault(); //Booking (to internal care activity) > Booking (to location)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            #endregion

            #region Step 12

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
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC1 ACC-6446")
                .SetStartDay("Tuesday")
                .SetStartTime("00", "00")
                .SetEndTime("06", "00")
                .SetEndDay("Tuesday")
                .ClickOccurrenceTab()
                .SelectBookingTakesPlaceEvery("4 weeks")
                .SelectNextDueToTakePlaceDate(nextDueDate_Year, nextDueDate_Month, nextDueDate_Date)
                .ClickRosteringTab()
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser2EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser3EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            System.Threading.Thread.Sleep(1200);

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId1 = careProviderBookingSchedules[0];

            var cpBookingScheduleStaffs = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId1);
            Assert.AreEqual(3, cpBookingScheduleStaffs.Count);
            var fields = dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), fields["systemuseremploymentcontractid"].ToString());
            fields = dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[1], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser2EmploymentContractId.ToString(), fields["systemuseremploymentcontractid"].ToString());
            fields = dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[2], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser3EmploymentContractId.ToString(), fields["systemuseremploymentcontractid"].ToString());

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC2 ACC-6446")
                .SetStartDay("Tuesday")
                .SetStartTime("04", "00")
                .SetEndTime("06", "00")
                .SetEndDay("Tuesday")
                .ClickOccurrenceTab()
                .SelectBookingTakesPlaceEvery("5 weeks")
                .SelectNextDueToTakePlaceDate(nextDueDate_Year, nextDueDate_Month, nextDueDate_Date)
                .ClickRosteringTab()
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickOnlyShowAvailableStaff()
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser2EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser4EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(user1FirstName + " " + user1LastName + " and " + user2FirstName + " " + user2LastName + " already have regular bookings in the schedule at this time. Do you want to create this booking anyway?")
                .ClickDismissButton_DynamicDialogue();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(user1FirstName + " " + user1LastName + " and " + user2FirstName + " " + user2LastName + " already have regular bookings in the schedule at this time. Do you want to create this booking anyway?")
                .ClickSaveButton_DynamicDialogue();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId2 = careProviderBookingSchedules[0];

            cpBookingScheduleStaffs = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId2);
            Assert.AreEqual(3, cpBookingScheduleStaffs.Count);
            List<string> contracts = new List<string>();
            contracts.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[0], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            contracts.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[1], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            contracts.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[2], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            Assert.IsTrue(contracts.Contains(_systemUser1EmploymentContractId.ToString()));
            Assert.IsTrue(contracts.Contains(_systemUser2EmploymentContractId.ToString()));
            Assert.IsTrue(contracts.Contains(_systemUser4EmploymentContractId.ToString()));

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-6452")]
        [Description("Step 13 from the original test ACC-6341")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ScheduleBooking_DoubleBookingCheck_ACC_6431_UITestCases007()
        {
            #region Next Due Date

            var nextDueDate = commonMethodsHelper.GetThisWeekFirstMonday();
            nextDueDate = nextDueDate.AddDays(6);
            string nextDueDate_Year = nextDueDate.Year.ToString();
            string nextDueDate_Month = nextDueDate.ToString("MMMM");
            string nextDueDate_Date = nextDueDate.Day.ToString();

            var nextDueDate2 = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);
            nextDueDate2 = nextDueDate2.AddDays(6);
            string nextDueDate_Year2 = nextDueDate2.Year.ToString();
            string nextDueDate_Month2 = nextDueDate2.ToString("MMMM");
            string nextDueDate_Date2 = nextDueDate2.Day.ToString();

            #endregion

            #region Provider

            var providerName = "P6452 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Booking Type

            var _bookingTypeBTC1 = commonMethodsDB.CreateCPBookingType("BTC1 ACC-6452", 1, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 1);
            var _bookingTypeBTC2 = commonMethodsDB.CreateCPBookingType("BTC2 ACC-6452", 2, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 2, false, 1000, null, null, null);
            var _bookingTypeBTC3 = commonMethodsDB.CreateCPBookingType("BTC3 ACC-6452", 3, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC1, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC2, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC3, false);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6452", "99910", null, new DateTime(2020, 1, 1), null);

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
            var user1name = "cpsu_1d_" + currentTimeString;
            var user1FirstName = "CP1d";
            var user1LastName = "System User 1d " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingTypeBTC3);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC1, 2).FirstOrDefault(); //Booking (to location) > Booking (to internal care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 2); //Warn Only

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC1, 3).FirstOrDefault(); //Booking (to location) > Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 2); //Warn Only

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC2, 1).FirstOrDefault(); //Booking (to internal care activity) > Booking (to location)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC2, 3).FirstOrDefault(); //Booking (to internal care activity) > Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 3); //Prevent

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC3, 2).FirstOrDefault(); //Booking (to extermal care activity) > Booking (to intenal care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 2); //Warn Only

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC3, 1).FirstOrDefault(); //Booking (to internal care activity) > Booking (to location)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            #endregion

            #region Step 13

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
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC1 ACC-6452")
                .SetStartDay("Sunday")
                .SetStartTime("05", "00")
                .SetEndTime("08", "00")
                .SetEndDay("Sunday")
                .ClickOccurrenceTab()
                .SelectBookingTakesPlaceEvery("2 weeks")
                .SelectNextDueToTakePlaceDate(nextDueDate_Year, nextDueDate_Month, nextDueDate_Date)
                .ClickRosteringTab()
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(1200);

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId1 = careProviderBookingSchedules[0];

            var cpBookingScheduleStaffs = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId1);
            Assert.AreEqual(1, cpBookingScheduleStaffs.Count);
            var fields = dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), fields["systemuseremploymentcontractid"].ToString());

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC2 ACC-6452")
                .SetStartDay("Sunday")
                .SetStartTime("06", "00")
                .SetEndTime("09", "00")
                .SetEndDay("Sunday")
                .ClickOccurrenceTab()
                .SelectBookingTakesPlaceEvery("2 weeks")
                .SelectNextDueToTakePlaceDate(nextDueDate_Year2, nextDueDate_Month2, nextDueDate_Date2)
                .ClickRosteringTab()
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            System.Threading.Thread.Sleep(1200);

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId2 = careProviderBookingSchedules[0];

            cpBookingScheduleStaffs = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId2);
            Assert.AreEqual(1, cpBookingScheduleStaffs.Count);
            fields = dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), fields["systemuseremploymentcontractid"].ToString());


            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-6453")]
        [Description("Step 14 from the original test ACC-6341")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ScheduleBooking_DoubleBookingCheck_ACC_6431_UITestCases008()
        {
            #region Provider

            var providerName = "P6453 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Booking Type

            var _bookingTypeBTC1 = commonMethodsDB.CreateCPBookingType("BTC1 ACC-6453", 1, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 1);
            var _bookingTypeBTC2 = commonMethodsDB.CreateCPBookingType("BTC2 ACC-6453", 2, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 2, false, 1000, null, null, null);
            var _bookingTypeBTC3 = commonMethodsDB.CreateCPBookingType("BTC3 ACC-6453", 3, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC1, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC2, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC3, false);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6453", "99910", null, new DateTime(2020, 1, 1), null);

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
            var user1name = "cpsu_1e_" + currentTimeString;
            var user1FirstName = "CP1e";
            var user1LastName = "System User 1e " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            var user2name = "cpsu_2e_" + currentTimeString;
            var user2FirstName = "CP2e";
            var user2LastName = "System User 2e " + currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser2Id, commonMethodsHelper.GetThisWeekFirstMonday());

            var user3name = "cpsu_3e_" + currentTimeString;
            var user3FirstName = "CP3e";
            var user3LastName = "System User 3e " + currentTimeString;
            var systemUser3Id = commonMethodsDB.CreateSystemUserRecord(user3name, user3FirstName, user3LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser3Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser2EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser2Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser3EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser3Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingTypeBTC3);

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingTypeBTC3);

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContractId, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContractId, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContractId, _bookingTypeBTC3);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser2Id, _teamId, _systemUser2EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser3Id, _teamId, _systemUser3EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC1, 3).FirstOrDefault(); //Booking (to location) > Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 2); //Warn Only

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC2, 3).FirstOrDefault(); //Booking (to internal care activity) > Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 3); //Prevent

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC3, 2).FirstOrDefault(); //Booking (to extermal care activity) > Booking (to intenal care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC3, 1).FirstOrDefault(); //Booking (to internal care activity) > Booking (to location)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 3); //Prevent

            #endregion

            #region Step 14

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL");

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC1 ACC-6453")
                .SetStartDay("Monday")
                .SetStartTime("05", "00")
                .SetEndTime("09", "00")
                .SetEndDay("Monday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser2EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser3EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(2000);

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId1 = careProviderBookingSchedules[0];

            var cpBookingScheduleStaffs = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId1);
            Assert.AreEqual(3, cpBookingScheduleStaffs.Count);
            var cpBookingScheduleStaffIds = new List<string>();

            cpBookingScheduleStaffIds.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[0], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            cpBookingScheduleStaffIds.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[1], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            cpBookingScheduleStaffIds.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[2], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());

            Assert.IsTrue(cpBookingScheduleStaffIds.Contains(_systemUser1EmploymentContractId.ToString()));
            Assert.IsTrue(cpBookingScheduleStaffIds.Contains(_systemUser2EmploymentContractId.ToString()));
            Assert.IsTrue(cpBookingScheduleStaffIds.Contains(_systemUser3EmploymentContractId.ToString()));

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC2 ACC-6453")
                .SetStartDay("Monday")
                .SetStartTime("10", "00")
                .SetEndTime("12", "00")
                .SetEndDay("Monday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser2EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser3EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(2000);

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId2 = careProviderBookingSchedules[0];

            cpBookingScheduleStaffs = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId2);
            Assert.AreEqual(3, cpBookingScheduleStaffs.Count);
            cpBookingScheduleStaffIds = new List<string>();

            cpBookingScheduleStaffIds.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[0], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            cpBookingScheduleStaffIds.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[1], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            cpBookingScheduleStaffIds.Add(dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[2], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());

            Assert.IsTrue(cpBookingScheduleStaffIds.Contains(_systemUser1EmploymentContractId.ToString()));
            Assert.IsTrue(cpBookingScheduleStaffIds.Contains(_systemUser2EmploymentContractId.ToString()));
            Assert.IsTrue(cpBookingScheduleStaffIds.Contains(_systemUser3EmploymentContractId.ToString()));

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC3 ACC-6453")
                .SetStartDay("Monday")
                .SetStartTime("08", "00")
                .SetEndTime("11", "00")
                .SetEndDay("Monday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickOnlyShowAvailableStaff()
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser2EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser3EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessageContainsText_DynamicDialogue(user1FirstName + " " + user1LastName)
                .ValidateMessageContainsText_DynamicDialogue(user2FirstName + " " + user2LastName)
                .ValidateMessageContainsText_DynamicDialogue(user3FirstName + " " + user3LastName)
                .ValidateMessageContainsText_DynamicDialogue(" already have regular bookings in the schedule at this time.")
                .ClickDismissButton_DynamicDialogue();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickOnCloseButton();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6441

        [TestProperty("JiraIssueID", "ACC-6463")]
        [Description("Step(s) 15 from the original test ACC-6341")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ScheduleBooking_DoubleBookingCheck_ACC_6431_UITestCases009()
        {
            #region Provider

            var providerName = "P6463 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Booking Type

            var _bookingTypeBTC1 = commonMethodsDB.CreateCPBookingType("BTC1 ACC-6463", 1, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 1);
            var _bookingTypeBTC2 = commonMethodsDB.CreateCPBookingType("BTC2 ACC-6463", 2, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 2, false, 1000, null, null, null);
            var _bookingTypeBTC3 = commonMethodsDB.CreateCPBookingType("BTC3 ACC-6463", 3, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC1, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC2, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC3, false);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid1 = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "6463 Nurse", "99910", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleTypeid2 = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "6463 Carer", "99910", null, new DateTime(2020, 1, 1), null);

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
            Guid _hourlyOvertime_availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Hourly/Overtime").First();
            dbHelper.availabilityTypes.UpdateDiaryBookingsValidityId(_hourlyOvertime_availabilityTypeId, 1);

            #endregion

            #region System User

            //set the user as rostered user and use a persona to link it to the user
            var user1name = "cpsu_1f_" + currentTimeString;
            var user1FirstName = "CP1f";
            var user1LastName = "SU1f " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());


            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid1, _teamId, _employmentContractTypeid1, 47);
            var _systemUserEmploymentContractId2 = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid2, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingTypeBTC3);

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingTypeBTC3);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUserEmploymentContractId2, _hourlyOvertime_availabilityTypeId);

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC1, 2).FirstOrDefault(); //Booking (to location) > Booking (to internal care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 3); //Prevent

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC1, 3).FirstOrDefault(); //Booking (to location) > Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC2, 1).FirstOrDefault(); //Booking (to internal care activity) > Booking (to location)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC2, 3).FirstOrDefault(); //Booking (to internal care activity) > Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 3); //Prevent

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC3, 2).FirstOrDefault(); //Booking (to extermal care activity) > Booking (to intenal care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingTypeBTC3, 1).FirstOrDefault(); //Booking (to internal care activity) > Booking (to location)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            #endregion

            #region Step 15

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL");

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC1 ACC-6463")
                .SetStartDay("Monday")
                .SetStartTime("09", "00")
                .SetEndTime("15", "00")
                .SetEndDay("Monday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking()
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("Employee CP1f SU1f " + currentTimeString + " has more than one contract; CP1f SU1f " + currentTimeString + ", 47.00 hrs, and CP1f SU1f " + currentTimeString + ". 47.00 hrs, Are you sure you are allocating the booking to the correct employment contract in line with the employees pay arrangement?")
                .ClickConfirmButton_WarningDialogue();

            System.Threading.Thread.Sleep(2000);

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId1 = careProviderBookingSchedules[0];

            var cpBookingScheduleStaffs = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId1);
            Assert.AreEqual(1, cpBookingScheduleStaffs.Count);
            var fields = dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUserEmploymentContractId1.ToString(), fields["systemuseremploymentcontractid"].ToString());

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC2 ACC-6463")
                .SetStartDay("Monday")
                .SetStartTime("11", "00")
                .SetEndTime("17", "00")
                .SetEndDay("Monday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickOnlyShowAvailableStaff()
                .ClickStaffRecordCellText(_systemUserEmploymentContractId2)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(user1FirstName + " " + user1LastName + " already has a regular booking in the schedule at this time.")
                .ClickDismissButton_DynamicDialogue();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            createScheduleBookingPopup
                .ClickOnCloseButton()
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("You have unsaved changes. Are you sure you want to close the drawer?")
                .ClickConfirmButton_WarningDialogue();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC3 ACC-6463")
                .SetStartDay("Monday")
                .SetStartTime("11", "00")
                .SetEndTime("17", "00")
                .SetEndDay("Monday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickOnlyShowAvailableStaff()
                .ClickStaffRecordCellText(_systemUserEmploymentContractId2)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking()
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("Employee CP1f SU1f " + currentTimeString + " has more than one contract; CP1f SU1f " + currentTimeString + ", 47.00 hrs, and CP1f SU1f " + currentTimeString + ". 47.00 hrs, Are you sure you are allocating the booking to the correct employment contract in line with the employees pay arrangement?")
                .ClickConfirmButton_WarningDialogue();

            System.Threading.Thread.Sleep(2000);

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId2 = careProviderBookingSchedules[0];

            cpBookingScheduleStaffs = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId2);
            Assert.AreEqual(1, cpBookingScheduleStaffs.Count);
            fields = dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUserEmploymentContractId2.ToString(), fields["systemuseremploymentcontractid"].ToString());

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-6465")]
        [Description("Step 16 from the original test ACC-6341")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ScheduleBooking_DoubleBookingCheck_ACC_6431_UITestCases010()
        {
            #region Provider

            var providerName = "P6465 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Booking Type

            var _bookingTypeBTC1 = commonMethodsDB.CreateCPBookingType("BTC1 ACC-6465", 1, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 1);
            var _bookingTypeBTC2 = commonMethodsDB.CreateCPBookingType("BTC2 ACC-6465", 2, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 2, false, 1000, null, null, null);
            var _bookingTypeBTC3 = commonMethodsDB.CreateCPBookingType("BTC3 ACC-6465", 3, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC1, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC2, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingTypeBTC3, false);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid1 = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "6465 Carer", "99910", null, new DateTime(2020, 1, 1), null);
            //var _careProviderStaffRoleTypeid2 = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "6463 Carer", "99910", null, new DateTime(2020, 1, 1), null);

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
            var user1name = "cpsu_1g_" + currentTimeString;
            var user1FirstName = "CP1g";
            var user1LastName = "SU1g " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());


            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid1, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingTypeBTC1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingTypeBTC2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingTypeBTC3);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);

            #endregion

            #region Step 16

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
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC1 ACC-6465")
                .SetStartDay("Monday")
                .SetStartTime("09", "00")
                .SetEndTime("15", "00")
                .SetEndDay("Monday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            System.Threading.Thread.Sleep(1200);

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId1 = careProviderBookingSchedules[0];

            var cpBookingScheduleStaffs = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId1);
            Assert.AreEqual(1, cpBookingScheduleStaffs.Count);
            var fields = dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUserEmploymentContractId1.ToString(), fields["systemuseremploymentcontractid"].ToString());

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC2 ACC-6465")
                .SetStartDay("Monday")
                .SetStartTime("11", "00")
                .SetEndTime("17", "00")
                .SetEndDay("Monday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickOnlyShowAvailableStaff()
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            System.Threading.Thread.Sleep(1200);

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            Guid cpBookingScheduleId2 = careProviderBookingSchedules[0];

            cpBookingScheduleStaffs = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId2);
            Assert.AreEqual(1, cpBookingScheduleStaffs.Count);
            fields = dbHelper.cpBookingScheduleStaff.GetById(cpBookingScheduleStaffs[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUserEmploymentContractId1.ToString(), fields["systemuseremploymentcontractid"].ToString());

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC3 ACC-6465")
                .SetStartDay("Monday")
                .SetStartTime("16", "00")
                .SetEndTime("22", "00")
                .SetEndDay("Monday")
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickOnlyShowAvailableStaff()
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(user1FirstName + " " + user1LastName + " already has a regular booking in the schedule at this time.")
                .ClickDismissButton_DynamicDialogue();

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(2, careProviderBookingSchedules.Count);

            createScheduleBookingPopup
                .ClickOnCloseButton();

            #endregion
        }

        #endregion

        #endregion



    }
}

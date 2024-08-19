using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Providers
{
    /// <summary>
    /// This class contains Automated UI test scripts for Provider Type
    /// </summary>
    [TestClass]
    public class ProviderSchedule2_JB_UITestCases : FunctionalTest
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

        internal void CreateUserWorkSchedule(Guid UserId, Guid TeamId, Guid SystemUserEmploymentContractId, Guid availabilityTypeId, TimeSpan startTime, TimeSpan endTime)
        {
            for (int i = 0; i < 7; i++)
            {
                var workScheduleDate = DateTime.Now.AddDays(i).Date;

                switch (workScheduleDate.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekSundayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, startTime, endTime, 1);
                        break;
                    case DayOfWeek.Monday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekMondayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, startTime, endTime, 1);
                        break;
                    case DayOfWeek.Tuesday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekTuesdayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, startTime, endTime, 1);
                        break;
                    case DayOfWeek.Wednesday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekWednesdayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, startTime, endTime, 1);
                        break;
                    case DayOfWeek.Thursday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekThursdayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, startTime, endTime, 1);
                        break;
                    case DayOfWeek.Friday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekFridayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, startTime, endTime, 1);
                        break;
                    case DayOfWeek.Saturday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekSaturdayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, startTime, endTime, 1);
                        break;
                    default:
                        break;
                }
            }
        }

        internal void CreateUserWorkSchedule(Guid UserId, Guid TeamId, Guid SystemUserEmploymentContractId, Guid availabilityTypeId, TimeSpan startTime, TimeSpan endTime, DateTime workScheduleDate, Guid recurrencePattern)
        {
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, recurrencePattern, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, startTime, endTime, 1);
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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("PS BU A");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                teamName = "PS T A";
                _teamId = commonMethodsDB.CreateTeam(teamName, null, _businessUnitId, "90400", "PSTA@careworkstempmail.com", teamName, "020 123456");

                #endregion

                #region Create default system user

                _loginUser_Username = "ProviderSchedule_User_95";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUser_Username, "ProviderSchedule", "User_90", "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

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

        #region https://advancedcsg.atlassian.net/browse/ACC-7737

        [TestProperty("JiraIssueID", "ACC-7978")]
        [Description("Step(s) 1 to 2  from the original test - Create new booking - Context menu Copy")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderSchedule_ACC_7708_UITestCases001()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-7708 A", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType2, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, true);

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMapping1Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType2, null, "");

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 1; //Monday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = todayDate.AddYears(1);
            var separateinvoices = false;

            dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2023, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);


            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Juaquin";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2023, 11, 20), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType2Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Cook", "3", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractType1id = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));

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

            var user1name = "George_" + currentTimeString;
            var user1FirstName = "George";
            var user1LastName = currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user2name = "Jearldine_" + currentTimeString;
            var user2FirstName = "Jearldine";
            var user2LastName = currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user3name = "Bethany_" + currentTimeString;
            var user3FirstName = "Bethany";
            var user3LastName = currentTimeString;
            var systemUser3Id = commonMethodsDB.CreateSystemUserRecord(user3name, user3FirstName, user3LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user4name = "John_" + currentTimeString;
            var user4FirstName = "John";
            var user4LastName = currentTimeString;
            var systemUser4Id = commonMethodsDB.CreateSystemUserRecord(user4name, user4FirstName, user4LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user5name = "Jack_" + currentTimeString;
            var user5FirstName = "Jack";
            var user5LastName = currentTimeString;
            var systemUser5Id = commonMethodsDB.CreateSystemUserRecord(user5name, user5FirstName, user5LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser2Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser3Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser4Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser5Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, null, _careProviderStaffRoleType1Id, _teamId, _employmentContractType1id, 47);

            var _systemUser2EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser2Id, commonMethodsHelper.GetDatePartWithoutCulture().AddMonths(2), _careProviderStaffRoleType1Id, _teamId, _employmentContractType1id, 47);

            var _systemUser3EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser3Id, commonMethodsHelper.GetDatePartWithoutCulture().AddMonths(-1), _careProviderStaffRoleType2Id, _teamId, _employmentContractType1id, 47);

            var _systemUser4EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser4Id, commonMethodsHelper.GetDatePartWithoutCulture().AddMonths(-1), _careProviderStaffRoleType1Id, _teamId, _employmentContractType1id, 47);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(_systemUser4EmploymentContractId, commonMethodsHelper.GetDatePartWithoutCulture().AddMonths(1).AddHours(20), contractEndReasonId);

            var _systemUser5EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser5Id, commonMethodsHelper.GetDatePartWithoutCulture().AddMonths(-1), _careProviderStaffRoleType1Id, _teamId, _employmentContractType1id, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser4EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser5EmploymentContractId, _bookingType2);

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region Create System User Contract Suspension

            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-5)).Date;
            var contracts = new List<Guid> { _systemUser3EmploymentContractId };
            dbHelper.systemUserSuspension.CreateSystemUserSuspension(systemUser3Id, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser2Id, _teamId, _systemUser2EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser3Id, _teamId, _systemUser3EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser4Id, _teamId, _systemUser4EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser5Id, _teamId, _systemUser5EmploymentContractId, _availabilityTypeId);

            #endregion


            #region Step 1 - 2

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
                .SelectBookingType("BTC ACC-7708 A")
                .SetStartDay("Sunday")
                .SetStartTime("07", "00")
                .SetEndTime("08", "00")
                .SetEndDay("Sunday")
                .ClickSelectPeople();

            selectMultiplePeoplePopUp
                .WaitForSelectMultiplePeoplePopUpPageToLoad()
                .ClickPeopleRecordCellText(_personcontractId.ToString())
                .ClickConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .ClickOnlyShowAvailableStaff()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser2EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser3EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser4EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser5EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking()
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessageLine("George " + currentTimeString + " - Helper contract has not started yet, and will not be allocated to this diary booking until this contract has started.", 1)
                .ValidateWarningAlertMessageLine("Jearldine " + currentTimeString + " - Helper contract has not started yet, and will not be allocated to this diary booking until this contract has started.", 2)
                .ValidateWarningAlertMessageLine("Bethany " + currentTimeString + " - Cook contract is currently suspended, and will not be allocated to this diary booking until this suspension has ended.", 3)
                .ValidateWarningAlertMessageLine("John " + currentTimeString + " - Helper contract has an end date of: " + commonMethodsHelper.GetDatePartWithoutCulture().AddMonths(1).ToString("dd/MM/yyyy"), 4)
                .ClickConfirmButton_WarningDialogue();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(4000);

            var bookings = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, bookings.Count);
            var booking1Id = bookings[0];

            //Copy the booking
            providerSchedulePage
                .RightClickDiaryBooking(booking1Id)
                .WaitForContextMenuToLoad()
                .ClickCopyButton_ContextMenu();

            //Past the booking on Monday
            providerSchedulePage
                .RightClickGridArea(DayOfWeek.Monday)
                .WaitForContextMenuToLoad()
                .ClickPastButton_ContextMenu();

            wallchartWarningDialogPopup
                .WaitForWallchartWarningDialogPopupToLoad()
                .ValidateMessageLine(1, "Booking:")
                .ValidateMessageLine(2, "BTC ACC-7708 A - schedule booking starting at 07:00 on Monday at Provider " + currentTimeString + " assigned to George " + currentTimeString + ", Jearldine " + currentTimeString + ", Bethany " + currentTimeString + ", John " + currentTimeString + ", and Jack " + currentTimeString + ".")
                .ValidateMessageLine(3, "Issues:")
                .ValidateBulletlistLine(0, "George " + currentTimeString + " - Helper contract has not started yet, and will not be allocated to this diary booking until this contract has started.")
                .ValidateBulletlistLine(1, "Jearldine " + currentTimeString + " - Helper contract has not started yet, and will not be allocated to this diary booking until this contract has started.")
                .ValidateBulletlistLine(2, "Bethany " + currentTimeString + " - Cook contract is currently suspended, and will not be allocated to this diary booking until this suspension has ended.")
                .ValidateBulletlistLine(3, "John " + currentTimeString + " - Helper contract has an end date of: " + commonMethodsHelper.GetDatePartWithoutCulture().AddMonths(1).ToString("dd/MM/yyyy"))
                .ClickSaveButton();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderSchedulePageToLoad();

            bookings = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(2, bookings.Count);
            var booking2Id = bookings.Where(c => c != booking1Id).First();

            providerSchedulePage
                .ClickDiaryBooking(booking2Id);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateSelectedStaffFieldValues(_systemUser1EmploymentContractId, "George " + currentTimeString)
                .ValidateSelectedStaffFieldValues(_systemUser2EmploymentContractId, "Jearldine " + currentTimeString)
                .ValidateSelectedStaffFieldValues(_systemUser3EmploymentContractId, "Bethany " + currentTimeString)
                .ValidateSelectedStaffFieldValues(_systemUser4EmploymentContractId, "John " + currentTimeString)
                .ValidateSelectedStaffFieldValues(_systemUser5EmploymentContractId, "Jack " + currentTimeString);

            #endregion

        }



        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-7961

        [TestProperty("JiraIssueID", "ACC-8109")]
        [Description("Step(s) 2  from the original test - Context menu Copy To")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderSchedule_ACC_7708_UITestCases002()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-7708 A", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType2, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, true);

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMapping1Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType2, null, "");

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 1; //Monday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = todayDate.AddYears(1);
            var separateinvoices = false;

            dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2023, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);


            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Juaquin";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2023, 11, 20), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType2Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Cook", "3", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractType1id = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));

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

            var user1name = "George_" + currentTimeString;
            var user1FirstName = "George";
            var user1LastName = currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user2name = "Jearldine_" + currentTimeString;
            var user2FirstName = "Jearldine";
            var user2LastName = currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user3name = "Bethany_" + currentTimeString;
            var user3FirstName = "Bethany";
            var user3LastName = currentTimeString;
            var systemUser3Id = commonMethodsDB.CreateSystemUserRecord(user3name, user3FirstName, user3LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user4name = "John_" + currentTimeString;
            var user4FirstName = "John";
            var user4LastName = currentTimeString;
            var systemUser4Id = commonMethodsDB.CreateSystemUserRecord(user4name, user4FirstName, user4LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user5name = "Jack_" + currentTimeString;
            var user5FirstName = "Jack";
            var user5LastName = currentTimeString;
            var systemUser5Id = commonMethodsDB.CreateSystemUserRecord(user5name, user5FirstName, user5LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser2Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser3Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser4Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser5Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, null, _careProviderStaffRoleType1Id, _teamId, _employmentContractType1id, 47);

            var _systemUser2EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser2Id, commonMethodsHelper.GetDatePartWithoutCulture().AddMonths(2), _careProviderStaffRoleType1Id, _teamId, _employmentContractType1id, 47);

            var _systemUser3EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser3Id, commonMethodsHelper.GetDatePartWithoutCulture().AddMonths(-1), _careProviderStaffRoleType2Id, _teamId, _employmentContractType1id, 47);

            var _systemUser4EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser4Id, commonMethodsHelper.GetDatePartWithoutCulture().AddMonths(-1), _careProviderStaffRoleType1Id, _teamId, _employmentContractType1id, 47);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(_systemUser4EmploymentContractId, commonMethodsHelper.GetDatePartWithoutCulture().AddMonths(1).AddHours(20), contractEndReasonId);

            var _systemUser5EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser5Id, commonMethodsHelper.GetDatePartWithoutCulture().AddMonths(-1), _careProviderStaffRoleType1Id, _teamId, _employmentContractType1id, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser4EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser5EmploymentContractId, _bookingType2);

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region Create System User Contract Suspension

            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-5)).Date;
            var contracts = new List<Guid> { _systemUser3EmploymentContractId };
            dbHelper.systemUserSuspension.CreateSystemUserSuspension(systemUser3Id, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser2Id, _teamId, _systemUser2EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser3Id, _teamId, _systemUser3EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser4Id, _teamId, _systemUser4EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser5Id, _teamId, _systemUser5EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var booking1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 7, 7, new TimeSpan(7, 0, 0), new TimeSpan(11, 0, 0), providerId, "Express Book Testing", careProviderService1Id);
            dbHelper.cpBookingSchedule.UpdateGenderPreference(booking1Id, 1);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, booking1Id, _personID, _personcontractId, careProviderContractServiceId);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, booking1Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, booking1Id, _systemUser2EmploymentContractId, systemUser2Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, booking1Id, _systemUser3EmploymentContractId, systemUser3Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, booking1Id, _systemUser4EmploymentContractId, systemUser4Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, booking1Id, _systemUser5EmploymentContractId, systemUser5Id);

            #endregion


            #region Step 2

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
                .RightClickDiaryBooking(booking1Id)
                .WaitForContextMenuToLoad()
                .ClickCopyToButton_ContextMenu();

            copyBookingToDialogPopup
                .WaitForPopupToLoad()
                .SelectDaysOfWeek("Tuesday", "Wednesday")
                .ClickCopyButton();

            wallchartWarningDialogPopup
                .WaitForWallchartWarningDialogPopupToLoad()
                .ValidateMessageLine(1, "Booking:")
                .ValidateMessageLine(2, "BTC ACC-7708 A - schedule booking starting at 07:00 on Tuesday at Provider " + currentTimeString + " assigned to George " + currentTimeString + ", Jearldine " + currentTimeString + ", Bethany " + currentTimeString + ", John " + currentTimeString + ", and Jack " + currentTimeString + ".")
                .ValidateMessageLine(3, "Issues:")
                .ValidateBulletlistLine(0, "George " + currentTimeString + " - Helper contract has not started yet, and will not be allocated to this diary booking until this contract has started.")
                .ValidateBulletlistLine(1, "Jearldine " + currentTimeString + " - Helper contract has not started yet, and will not be allocated to this diary booking until this contract has started.")
                .ValidateBulletlistLine(2, "Bethany " + currentTimeString + " - Cook contract is currently suspended, and will not be allocated to this diary booking until this suspension has ended.")
                .ValidateBulletlistLine(3, "John " + currentTimeString + " - Helper contract has an end date of: " + commonMethodsHelper.GetDatePartWithoutCulture().AddMonths(1).ToString("dd/MM/yyyy"))
                .ClickSaveButton();

            System.Threading.Thread.Sleep(2000);

            wallchartWarningDialogPopup
                .WaitForWallchartWarningDialogPopupToLoad()
                .ValidateMessageLine(1, "Booking:")
                .ValidateMessageLine(2, "BTC ACC-7708 A - schedule booking starting at 07:00 on Wednesday at Provider " + currentTimeString + " assigned to George " + currentTimeString + ", Jearldine " + currentTimeString + ", Bethany " + currentTimeString + ", John " + currentTimeString + ", and Jack " + currentTimeString + ".")
                .ValidateMessageLine(3, "Issues:")
                .ValidateBulletlistLine(0, "George " + currentTimeString + " - Helper contract has not started yet, and will not be allocated to this diary booking until this contract has started.")
                .ValidateBulletlistLine(1, "Jearldine " + currentTimeString + " - Helper contract has not started yet, and will not be allocated to this diary booking until this contract has started.")
                .ValidateBulletlistLine(2, "Bethany " + currentTimeString + " - Cook contract is currently suspended, and will not be allocated to this diary booking until this suspension has ended.")
                .ValidateBulletlistLine(3, "John " + currentTimeString + " - Helper contract has an end date of: " + commonMethodsHelper.GetDatePartWithoutCulture().AddMonths(1).ToString("dd/MM/yyyy"))
                .ClickSaveButton();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderSchedulePageToLoad();

            var bookings = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(3, bookings.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-8111")]
        [Description("Step(s) 2  from the original test - Specify Care Worker")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderSchedule_ACC_7708_UITestCases003()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-7708 A", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType2, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, true);

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMapping1Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType2, null, "");

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 1; //Monday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = todayDate.AddYears(1);
            var separateinvoices = false;

            dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2023, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);


            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Juaquin";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2023, 11, 20), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType2Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Cook", "3", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractType1id = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));

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

            var user1name = "George_" + currentTimeString;
            var user1FirstName = "George";
            var user1LastName = currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user2name = "Jearldine_" + currentTimeString;
            var user2FirstName = "Jearldine";
            var user2LastName = currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user3name = "Bethany_" + currentTimeString;
            var user3FirstName = "Bethany";
            var user3LastName = currentTimeString;
            var systemUser3Id = commonMethodsDB.CreateSystemUserRecord(user3name, user3FirstName, user3LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user4name = "John_" + currentTimeString;
            var user4FirstName = "John";
            var user4LastName = currentTimeString;
            var systemUser4Id = commonMethodsDB.CreateSystemUserRecord(user4name, user4FirstName, user4LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user5name = "Jack_" + currentTimeString;
            var user5FirstName = "Jack";
            var user5LastName = currentTimeString;
            var systemUser5Id = commonMethodsDB.CreateSystemUserRecord(user5name, user5FirstName, user5LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser2Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser3Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser4Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser5Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, null, _careProviderStaffRoleType1Id, _teamId, _employmentContractType1id, 47);

            var _systemUser2EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser2Id, commonMethodsHelper.GetDatePartWithoutCulture().AddMonths(2), _careProviderStaffRoleType1Id, _teamId, _employmentContractType1id, 47);

            var _systemUser3EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser3Id, commonMethodsHelper.GetDatePartWithoutCulture().AddMonths(-1), _careProviderStaffRoleType2Id, _teamId, _employmentContractType1id, 47);

            var _systemUser4EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser4Id, commonMethodsHelper.GetDatePartWithoutCulture().AddMonths(-1), _careProviderStaffRoleType1Id, _teamId, _employmentContractType1id, 47);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(_systemUser4EmploymentContractId, commonMethodsHelper.GetDatePartWithoutCulture().AddMonths(1).AddHours(20), contractEndReasonId);

            var _systemUser5EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser5Id, commonMethodsHelper.GetDatePartWithoutCulture().AddMonths(-1), _careProviderStaffRoleType1Id, _teamId, _employmentContractType1id, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser4EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser5EmploymentContractId, _bookingType2);

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region Create System User Contract Suspension

            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-5)).Date;
            var contracts = new List<Guid> { _systemUser3EmploymentContractId };
            dbHelper.systemUserSuspension.CreateSystemUserSuspension(systemUser3Id, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser2Id, _teamId, _systemUser2EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser3Id, _teamId, _systemUser3EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser4Id, _teamId, _systemUser4EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser5Id, _teamId, _systemUser5EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var booking1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 3, 3, new TimeSpan(7, 0, 0), new TimeSpan(11, 0, 0), providerId, "Express Book Testing");
            var booking2Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 4, 4, new TimeSpan(7, 0, 0), new TimeSpan(11, 0, 0), providerId, "Express Book Testing");
            var booking3Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 5, 5, new TimeSpan(7, 0, 0), new TimeSpan(11, 0, 0), providerId, "Express Book Testing");
            var booking4Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 6, 6, new TimeSpan(7, 0, 0), new TimeSpan(11, 0, 0), providerId, "Express Book Testing");
            var booking5Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 7, 7, new TimeSpan(7, 0, 0), new TimeSpan(11, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(booking1Id, 1);
            dbHelper.cpBookingSchedule.UpdateGenderPreference(booking2Id, 1);
            dbHelper.cpBookingSchedule.UpdateGenderPreference(booking3Id, 1);
            dbHelper.cpBookingSchedule.UpdateGenderPreference(booking4Id, 1);
            dbHelper.cpBookingSchedule.UpdateGenderPreference(booking5Id, 1);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, booking1Id, _personID, _personcontractId, careProviderContractServiceId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, booking2Id, _personID, _personcontractId, careProviderContractServiceId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, booking3Id, _personID, _personcontractId, careProviderContractServiceId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, booking4Id, _personID, _personcontractId, careProviderContractServiceId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, booking5Id, _personID, _personcontractId, careProviderContractServiceId);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, booking1Id, null, null);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, booking2Id, null, null);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, booking3Id, null, null);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, booking4Id, null, null);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, booking5Id, null, null);

            #endregion


            #region Step 2

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            #region Update Booking 1

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderSchedulePageToLoad()
                .RightClickDiaryBooking(booking1Id)
                .WaitForContextMenuToLoad()
                .ClickSpecifyCareWorkerButton_ContextMenu();

            specifyCareWorkerPopup
                .WaitForPopupToLoad()
                .SelectCareWorker(currentTimeString, _systemUser1EmploymentContractId)
                .ClickSaveChangesButton();

            wallchartWarningDialogPopup
                .WaitForWallchartWarningDialogPopupToLoad()
                .ValidateMessageLine(1, "Booking:")
                .ValidateMessageLine(2, "Unassigned BTC ACC-7708 A - schedule booking starting at 07:00 on Wednesday at Provider " + currentTimeString + ".")
                .ValidateMessageLine(3, "Issue:")
                .ValidateBulletlistLine(0, "George " + currentTimeString + " - Helper contract has not started yet, and will not be allocated to this diary booking until this contract has started.")
                .ClickSaveButton();

            #endregion

            System.Threading.Thread.Sleep(2000);

            #region Update Booking 2

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderSchedulePageToLoad()
                .RightClickDiaryBooking(booking2Id)
                .WaitForContextMenuToLoad()
                .ClickSpecifyCareWorkerButton_ContextMenu();

            specifyCareWorkerPopup
                .WaitForPopupToLoad()
                .SelectCareWorker(currentTimeString, _systemUser2EmploymentContractId)
                .ClickSaveChangesButton();

            wallchartWarningDialogPopup
                .WaitForWallchartWarningDialogPopupToLoad()
                .ValidateMessageLine(1, "Booking:")
                .ValidateMessageLine(2, "Unassigned BTC ACC-7708 A - schedule booking starting at 07:00 on Thursday at Provider " + currentTimeString + ".")
                .ValidateMessageLine(3, "Issue:")
                .ValidateBulletlistLine(0, "Jearldine " + currentTimeString + " - Helper contract has not started yet, and will not be allocated to this diary booking until this contract has started.")
                .ClickSaveButton();

            #endregion

            System.Threading.Thread.Sleep(2000);

            #region Update Booking 3

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderSchedulePageToLoad()
                .RightClickDiaryBooking(booking3Id)
                .WaitForContextMenuToLoad()
                .ClickSpecifyCareWorkerButton_ContextMenu();

            specifyCareWorkerPopup
                .WaitForPopupToLoad()
                .SelectCareWorker(currentTimeString, _systemUser3EmploymentContractId)
                .ClickSaveChangesButton();

            wallchartWarningDialogPopup
                .WaitForWallchartWarningDialogPopupToLoad()
                .ValidateMessageLine(1, "Booking:")
                .ValidateMessageLine(2, "Unassigned BTC ACC-7708 A - schedule booking starting at 07:00 on Friday at Provider " + currentTimeString + ".")
                .ValidateMessageLine(3, "Issue:")
                .ValidateBulletlistLine(0, "Bethany " + currentTimeString + " - Cook contract is currently suspended, and will not be allocated to this diary booking until this suspension has ended.")
                .ClickSaveButton();

            #endregion

            System.Threading.Thread.Sleep(2000);

            #region Update Booking 4

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderSchedulePageToLoad()
                .RightClickDiaryBooking(booking4Id)
                .WaitForContextMenuToLoad()
                .ClickSpecifyCareWorkerButton_ContextMenu();

            specifyCareWorkerPopup
                .WaitForPopupToLoad()
                .SelectCareWorker(currentTimeString, _systemUser4EmploymentContractId)
                .ClickSaveChangesButton();

            wallchartWarningDialogPopup
                .WaitForWallchartWarningDialogPopupToLoad()
                .ValidateMessageLine(1, "Booking:")
                .ValidateMessageLine(2, "Unassigned BTC ACC-7708 A - schedule booking starting at 07:00 on Saturday at Provider " + currentTimeString + ".")
                .ValidateMessageLine(3, "Issue:")
                .ValidateBulletlistLine(0, "John " + currentTimeString + " - Helper contract has an end date of: " + commonMethodsHelper.GetDatePartWithoutCulture().AddMonths(1).ToString("dd/MM/yyyy"))
                .ClickSaveButton();

            #endregion

            System.Threading.Thread.Sleep(2000);

            #region Update Booking 5

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderSchedulePageToLoad()
                .RightClickDiaryBooking(booking5Id)
                .WaitForContextMenuToLoad()
                .ClickSpecifyCareWorkerButton_ContextMenu();

            specifyCareWorkerPopup
                .WaitForPopupToLoad()
                .SelectCareWorker(currentTimeString, _systemUser5EmploymentContractId)
                .ClickSaveChangesButton();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderSchedulePageToLoad();

            #endregion

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-8112")]
        [Description("Step(s) 2  from the original test - Drag booking")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderSchedule_ACC_7708_UITestCases004()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-7708 A", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType2, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, true);

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMapping1Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType2, null, "");

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 1; //Monday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = todayDate.AddYears(1);
            var separateinvoices = false;

            dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2023, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);


            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Juaquin";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2023, 11, 20), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType2Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Cook", "3", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractType1id = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));

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

            var user1name = "George_" + currentTimeString;
            var user1FirstName = "George";
            var user1LastName = currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user2name = "Jearldine_" + currentTimeString;
            var user2FirstName = "Jearldine";
            var user2LastName = currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user3name = "Bethany_" + currentTimeString;
            var user3FirstName = "Bethany";
            var user3LastName = currentTimeString;
            var systemUser3Id = commonMethodsDB.CreateSystemUserRecord(user3name, user3FirstName, user3LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user4name = "John_" + currentTimeString;
            var user4FirstName = "John";
            var user4LastName = currentTimeString;
            var systemUser4Id = commonMethodsDB.CreateSystemUserRecord(user4name, user4FirstName, user4LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user5name = "Jack_" + currentTimeString;
            var user5FirstName = "Jack";
            var user5LastName = currentTimeString;
            var systemUser5Id = commonMethodsDB.CreateSystemUserRecord(user5name, user5FirstName, user5LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser2Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser3Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser4Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser5Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, null, _careProviderStaffRoleType1Id, _teamId, _employmentContractType1id, 47);

            var _systemUser2EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser2Id, commonMethodsHelper.GetDatePartWithoutCulture().AddMonths(2), _careProviderStaffRoleType1Id, _teamId, _employmentContractType1id, 47);

            var _systemUser3EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser3Id, commonMethodsHelper.GetDatePartWithoutCulture().AddMonths(-1), _careProviderStaffRoleType2Id, _teamId, _employmentContractType1id, 47);

            var _systemUser4EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser4Id, commonMethodsHelper.GetDatePartWithoutCulture().AddMonths(-1), _careProviderStaffRoleType1Id, _teamId, _employmentContractType1id, 47);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(_systemUser4EmploymentContractId, commonMethodsHelper.GetDatePartWithoutCulture().AddMonths(1).AddHours(20), contractEndReasonId);

            var _systemUser5EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser5Id, commonMethodsHelper.GetDatePartWithoutCulture().AddMonths(-1), _careProviderStaffRoleType1Id, _teamId, _employmentContractType1id, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser4EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser5EmploymentContractId, _bookingType2);

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region Create System User Contract Suspension

            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-5)).Date;
            var contracts = new List<Guid> { _systemUser3EmploymentContractId };
            dbHelper.systemUserSuspension.CreateSystemUserSuspension(systemUser3Id, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser2Id, _teamId, _systemUser2EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser3Id, _teamId, _systemUser3EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser4Id, _teamId, _systemUser4EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser5Id, _teamId, _systemUser5EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var booking1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 7, 7, new TimeSpan(7, 0, 0), new TimeSpan(11, 0, 0), providerId, "Express Book Testing");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(booking1Id, 1);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, booking1Id, _personID, _personcontractId, careProviderContractServiceId);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, booking1Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, booking1Id, _systemUser2EmploymentContractId, systemUser2Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, booking1Id, _systemUser3EmploymentContractId, systemUser3Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, booking1Id, _systemUser4EmploymentContractId, systemUser4Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, booking1Id, _systemUser5EmploymentContractId, systemUser5Id);

            #endregion


            #region Step 2

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(4000);

            providerSchedulePage
                .DragBookingToDayOfWeekArea(booking1Id, DayOfWeek.Monday);

            wallchartWarningDialogPopup
                .WaitForWallchartWarningDialogPopupToLoad(false)
                .ValidateMessageLine(1, "Booking:")
                .ValidateMessageLine(2, "BTC ACC-7708 A - schedule booking starting at 07:00 on Monday at Provider " + currentTimeString + " assigned to George " + currentTimeString + ", Jearldine " + currentTimeString + ", Bethany " + currentTimeString + ", John " + currentTimeString + ", and Jack " + currentTimeString + ".")
                .ValidateMessageLine(3, "Issues:")
                .ValidateBulletlistLine(0, "George " + currentTimeString + " - Helper contract has not started yet, and will not be allocated to this diary booking until this contract has started.")
                .ValidateBulletlistLine(1, "Jearldine " + currentTimeString + " - Helper contract has not started yet, and will not be allocated to this diary booking until this contract has started.")
                .ValidateBulletlistLine(2, "Bethany " + currentTimeString + " - Cook contract is currently suspended, and will not be allocated to this diary booking until this suspension has ended.")
                .ValidateBulletlistLine(3, "John " + currentTimeString + " - Helper contract has an end date of: " + commonMethodsHelper.GetDatePartWithoutCulture().AddMonths(1).ToString("dd/MM/yyyy"))
                .ClickSaveButton();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderSchedulePageToLoad();

            var bookings = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, bookings.Count);

            #endregion

        }

        #endregion
    }
}

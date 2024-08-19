using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Providers
{
    /// <summary>
    /// This class contains Automated UI test scripts for Provider Type
    /// </summary>
    [TestClass]
    public class ProviderSchedule_JB_UITestCases : FunctionalTest
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
        private string currentDateString = DateTime.Now.ToString("yyyyMMdd");
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

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("PSBU1 " + currentDateString);

                #endregion

                #region Team

                teamName = "PST1 " + currentDateString;
                _teamId = commonMethodsDB.CreateTeam(teamName, null, _businessUnitId, "90400", "PSTA" + currentDateString + "@careworkstempmail.com", teamName, "020 123456");

                #endregion

                #region Create default system user

                _loginUser_Username = "ProviderSchedule_User_90";
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

        #region https://advancedcsg.atlassian.net/browse/ACC-5051

        [TestProperty("JiraIssueID", "ACC-5123")]
        [Description("Step(s) 1 to 8 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderSchedule_UITestCases001()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5051 2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion


            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2 and 3

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad()
               .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
               .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad();

            #endregion

            #region Step 4

            createScheduleBookingPopup
                .ValidateSelectedWeeks("1 week")
                .SelectBookingTakesPlaceEvery("52 weeks");

            #endregion

            #region Step 5, 6 & 7

            createScheduleBookingPopup
                .ValidateNextDueToTakePlaceFieldDisabled(false)
                .ValidateNextDueToTakePlaceMandatoryIconVisible(true)
                .SelectBookingTakesPlaceEvery("1 week")
                .ValidateNextDueToTakePlaceFieldDisabled(true)
                .ValidateNextDueToTakePlaceMandatoryIconVisible(false);

            #endregion

            #region Step 8

            createScheduleBookingPopup
                .ClickRosteringTab()

                .SelectBookingType("BTC ACC-5051 2")
                .ClickSelectPeople();

            selectMultiplePeoplePopUp
                .WaitForSelectMultiplePeoplePopUpPageToLoad()
                .ClickPeopleRecordCellText(_personcontractId.ToString())
                .ClickConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(3000);

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            var fields = dbHelper.cpBookingSchedule.GetById(careProviderBookingSchedules[0], "firstoccurrencedate");
            Assert.IsFalse(fields.ContainsKey("firstoccurrencedate"));

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-5124")]
        [Description("Step(s) 9 to 17 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderSchedule_UITestCases002()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5051 2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Step 9

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad()
               .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
               .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad();

            var targetYear = DateTime.Now.AddDays(7).Year.ToString();
            var targetMonth = DateTime.Now.AddDays(7).ToString("MMMM");
            var targetDay = DateTime.Now.AddDays(7).Day.ToString();

            createScheduleBookingPopup
                .SelectFirstOccurrenceDate("2023", "November", "1")
                .SelectLastOccurrenceDate(targetYear, targetMonth, targetDay);

            #endregion

            #region Step 10

            targetYear = DateTime.Now.Year.ToString();
            targetMonth = DateTime.Now.ToString("MMMM");
            targetDay = DateTime.Now.Day.ToString();

            createScheduleBookingPopup
                .ClickResetChangesButton()
                .ClickOccurrenceTab()
                .SelectFirstOccurrenceDate(targetYear, targetMonth, targetDay)
                .SelectLastOccurrenceDate(targetYear, targetMonth, targetDay);

            #endregion

            #region Step 11

            targetYear = DateTime.Now.AddDays(14).Year.ToString();
            targetMonth = DateTime.Now.AddDays(14).ToString("MMMM");
            targetDay = DateTime.Now.AddDays(14).Day.ToString();

            createScheduleBookingPopup
                .ClickResetChangesButton()
                .ClickOccurrenceTab()
                .SelectFirstOccurrenceDate(targetYear, targetMonth, targetDay)
                .SelectLastOccurrenceDate(targetYear, targetMonth, targetDay);

            #endregion

            #region Step 12

            createScheduleBookingPopup
                .ClickResetChangesButton()
                .ClickOccurrenceTab()
                .ClickOnCloseButton();

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad()
               .clickAddBooking();

            var firstOccurence = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(14);
            var firstOccurenceYear = firstOccurence.Year.ToString();
            var firstOccurenceMonth = firstOccurence.ToString("MMMM");
            var firstOccurenceDay = firstOccurence.Day.ToString();

            var lastOccurenceYear = firstOccurence.AddDays(-7).Year.ToString();
            var lastOccurenceMonth = firstOccurence.AddDays(-7).ToString("MMMM");
            var lastOccurenceDay = firstOccurence.AddDays(-7).Day.ToString();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .SelectFirstOccurrenceDate(firstOccurenceYear, firstOccurenceMonth, firstOccurenceDay)
                .ValidateLastOccurrenceDateNotSelectable(lastOccurenceYear, lastOccurenceMonth, lastOccurenceDay);

            #endregion

            #region Step 13

            //Step 13 seems to be outdated in the original test. if we select 2 weeks in “Booking Takes Place Every“, then on the “Next Due To Take Place“ field I can only select dates after today. I cannot select any date in the past 

            targetYear = DateTime.Now.AddDays(7).Year.ToString();
            targetMonth = DateTime.Now.AddDays(7).ToString("MMMM");
            targetDay = DateTime.Now.AddDays(7).Day.ToString();

            var NextDueTargetYear = DateTime.Now.AddDays(2).Year.ToString();
            var NextDueTargetMonth = DateTime.Now.AddDays(2).ToString("MMMM");
            var NextDueTargetDay = DateTime.Now.AddDays(2).Day.ToString();

            createScheduleBookingPopup
                .ClickResetChangesButton()
                .ClickOccurrenceTab()
                .SelectBookingTakesPlaceEvery("2 weeks")
                .SelectFirstOccurrenceDate(targetYear, targetMonth, targetDay)
                .ValidateNextDueToTakePlaceDateNotSelectable(NextDueTargetYear, NextDueTargetMonth, NextDueTargetDay);

            #endregion

            #region Step 14

            //Step 13 seems to be outdated in the original test. if we select 2 weeks in “Booking Takes Place Every“, then on the “Next Due To Take Place“ field I can only select dates after today. I cannot select any date in the past 

            createScheduleBookingPopup
                .SelectBookingTakesPlaceEvery("3 weeks")
                .ValidateNextDueToTakePlaceDateSelectable(targetYear, targetMonth, targetDay)
                .SelectNextDueToTakePlaceDate(targetYear, targetMonth, targetDay);

            #endregion

            #region Step 15

            createScheduleBookingPopup
                .ClickCreateAnotherBookingPicklist() //check
                .ClickCreateAnotherBookingPicklist(); //un-check

            #endregion

            #region Step 16

            createScheduleBookingPopup
                .ClickResetChangesButton()
                .ClickOccurrenceTab()
                .ValidateSelectedWeeks("1 week")
                .ValidateNextDueToTakePlaceDate("")
                .ValidateFirstOccurrenceValue("")
                .ValidateLastOccurrenceValue("");

            #endregion

            #region Step 17

            createScheduleBookingPopup
                .ClickOnCloseButton()
                .WaitForCreateScheduleBookingPopupClosed();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-5125")]
        [Description("Step(s) 18 to 22 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderSchedule_UITestCases003()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5051 2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion


            #region Step 18

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad()
               .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
               .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC ACC-5051 2")
                .ClickSelectPeople();

            selectMultiplePeoplePopUp
                .WaitForSelectMultiplePeoplePopUpPageToLoad()
                .ClickPeopleRecordCellText(_personcontractId.ToString())
                .ClickConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .ClickCreateBooking();

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(3000);

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            #endregion

            #region Step 19

            providerSchedulePage
               .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC ACC-5051 2")
                .ClickSelectPeople();

            selectMultiplePeoplePopUp
                .WaitForSelectMultiplePeoplePopUpPageToLoad()
                .ClickPeopleRecordCellText(_personcontractId.ToString())
                .ClickConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .ClickOnCloseButton()
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("You have unsaved changes. Are you sure you want to close the drawer?")
                .ClickConfirmButton_WarningDialogue();

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(3000);

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            #endregion

            #region Step 20

            //here we only need to validate that if the number of weeks = 1 then the we need to check that the "Next Due To Take Place" field is disabled
            //this is already done in Steps 5, 6 & 7 (test ProviderSchedule_UITestCases001)

            #endregion

            #region Step 21

            //Step 21 will be ignored for automation. We can only insert dates using the calendar tool

            #endregion

            #region Step 22

            providerSchedulePage
               .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("You haven't filled in all required fields.")
                .ClickDismissButton_DynamicDialogue();

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5154

        [TestProperty("JiraIssueID", "ACC-5205")]
        [Description("Step(s) 1 to 3 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderSchedule_UITestCases004()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5154 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType3 = commonMethodsDB.CreateCPBookingType("BTC ACC-5154 2", 3, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType2, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType3, false);

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
            var careProviderServiceMapping2Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType3, null, "");

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
            var careProviderContractService2Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType3, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService2Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 13, _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region System User

            //set the user as roostered user and use a persona to link it to the user
            var username = "cpsu_" + currentTimeString;
            var userFirstName = "Care Provider";
            var userLastName = "System User " + currentTimeString;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5154", "99997", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType3);

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

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), providerId, "Express Book Testing");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingScheduleId, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingScheduleId, _systemUserEmploymentContractId, systemUserId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingScheduleId, _personID, _personcontractId, careProviderContractServiceId);

            #endregion


            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2 (part 1)

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad()
               .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
               .WaitForProviderSchedulePageToLoad()
               .ClickDiaryBooking(cpBookingScheduleId);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC ACC-5154 2")
                .ClickCreateBooking();

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad()
               .ClickDiaryBooking(cpBookingScheduleId);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateBookingTypeDropDownText("BTC ACC-5154 2");

            #endregion

            #region Step 2 (part 2)

            createScheduleBookingPopup
                .RemoveStaffFromSelectedStaffField(_systemUserEmploymentContractId)
                .ClickAddUnassignedStaff()
                .ClickCreateBooking();

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad()
               .ClickDiaryBooking(cpBookingScheduleId);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateSelectedStaffFieldValues("0", "Unassigned");

            #endregion

            #region Step 2 (part 3)

            createScheduleBookingPopup
                .SetStartTime("06", "30")
                .SetEndTime("09", "30")
                .ClickCreateBooking();

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad()
               .ClickDiaryBooking(cpBookingScheduleId);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateStartTime("06:30")
                .ValidateEndTime("09:30");

            #endregion

            #region Step 2 (part 4)

            var NextDueToTakePlaceDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);
            var NextDueToTakePlaceDateYear = NextDueToTakePlaceDate.Year.ToString();
            var NextDueToTakePlaceDateMonth = NextDueToTakePlaceDate.ToString("MMMM");
            var NextDueToTakePlaceDateDay = NextDueToTakePlaceDate.Day.ToString();

            createScheduleBookingPopup
                .ClickOccurrenceTab()
                .SelectBookingTakesPlaceEvery("2 weeks")
                .SelectNextDueToTakePlaceDate(NextDueToTakePlaceDateYear, NextDueToTakePlaceDateMonth, NextDueToTakePlaceDateDay)
                .ClickCreateBooking();

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad()
               .ClickDiaryBooking(cpBookingScheduleId);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .ValidateSelectedWeeks("2 weeks")
                .ValidateNextDueToTakePlaceDate(NextDueToTakePlaceDate.ToString("dd'/'MM'/'yyyy"));

            #endregion

            #region Step 2 (part 5)

            var FirstOccurrence = NextDueToTakePlaceDate;
            var FirstOccurrenceYear = FirstOccurrence.Year.ToString();
            var FirstOccurrenceMonth = FirstOccurrence.ToString("MMMM");
            var FirstOccurrenceDay = FirstOccurrence.Day.ToString();

            var LastOccurrence = NextDueToTakePlaceDate.AddDays(7);
            var LastOccurrenceYear = LastOccurrence.Year.ToString();
            var LastOccurrenceMonth = LastOccurrence.ToString("MMMM");
            var LastOccurrenceDay = LastOccurrence.Day.ToString();

            createScheduleBookingPopup
                .SelectFirstOccurrenceDate(FirstOccurrenceYear, FirstOccurrenceMonth, FirstOccurrenceDay)
                .SelectLastOccurrenceDate(LastOccurrenceYear, LastOccurrenceMonth, LastOccurrenceDay)
                .ClickCreateBooking()
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(string.Format("This booking will not be booked until {0}.", FirstOccurrence.ToString("dd'/'MM'/'yyyy")), 1)
                .ValidateMessage_DynamicDialogue(string.Format("This booking will be deleted when express booking is run for a period after {0}.", LastOccurrence.ToString("dd'/'MM'/'yyyy")), 2)
                .ClickSaveButton_DynamicDialogue();

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad()
               .ClickDiaryBooking(cpBookingScheduleId);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .ValidateFirstOccurrenceValue(FirstOccurrence.ToString("dd'/'MM'/'yyyy"))
                .ValidateLastOccurrenceValue(LastOccurrence.ToString("dd'/'MM'/'yyyy"));

            #endregion

            #region Step 2 (part 6)

            createScheduleBookingPopup
                .ClickRosteringTab()
                .InsertTextInCommentsTextArea("some comments here ...")
                .ClickCreateBooking()
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(string.Format("This booking will not be booked until {0}.", FirstOccurrence.ToString("dd'/'MM'/'yyyy")), 1)
                .ValidateMessage_DynamicDialogue(string.Format("This booking will be deleted when express booking is run for a period after {0}.", LastOccurrence.ToString("dd'/'MM'/'yyyy")), 2)
                .ClickSaveButton_DynamicDialogue();

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad()
               .ClickDiaryBooking(cpBookingScheduleId);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateTextInCommentsTextArea("some comments here ...");


            #endregion

            #region Step 3

            var auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 2, //update operation
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = cpBookingScheduleId.ToString(),
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
            var auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);
            Assert.AreEqual(6, auditResponseData.GridData.Count);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-5206")]
        [Description("Step(s) 4 from the original test [Updating the booking type field]")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderSchedule_UITestCases005()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5154 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType3 = commonMethodsDB.CreateCPBookingType("BTC ACC-5154 Past", 3, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            dbHelper.cpBookingType.UpdateValidTo(_bookingType3, null);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType2, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType3, false);

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
            var careProviderServiceMapping2Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType3, null, "");

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
            var careProviderContractService2Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType3, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService2Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 13, _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region System User

            //set the user as roostered user and use a persona to link it to the user
            var username = "cpsu_" + currentTimeString;
            var userFirstName = "Care Provider";
            var userLastName = "System User " + currentTimeString;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5154", "99997", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType3);

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

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), providerId, "Express Book Testing");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingScheduleId, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingScheduleId, _systemUserEmploymentContractId, systemUserId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingScheduleId, _personID, _personcontractId, careProviderContractServiceId);

            #endregion


            #region Step 4 [1 b]

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
               .ClickDiaryBooking(cpBookingScheduleId);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad();

            dbHelper.cpBookingType.UpdateValidTo(_bookingType3, new DateTime(2023, 11, 1));

            createScheduleBookingPopup
                .SelectBookingType("BTC ACC-5154 Past")
                .ClickCreateBooking()
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("Please update the booking type BTC ACC-5154 Past, it is now invalid. Valid to 01/11/2023.")
                .ClickDismissButton_DynamicDialogue();

            #endregion

            #region Step 4 [2 a]

            createScheduleBookingPopup
                .SelectBookingType("BTC ACC-5154 1")
                .SetStartTime("07", "10")
                .SetEndTime("08", "00")
                .ClickCreateBooking()
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("The booking must be a multiple of 15 minutes.")
                .ClickDismissButton_DynamicDialogue()
                .SetStartTime("07", "00")
                .SetEndTime("08", "00");

            #endregion

            #region Step 4 [3 a]

            createScheduleBookingPopup
                .RemoveStaffFromSelectedStaffField(_systemUserEmploymentContractId)
                .ClickCreateBooking()
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("You need to have at least 1 staff member for this booking type.")
                .ClickDismissButton_DynamicDialogue()
                .ClickOnCloseButton();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-5207")]
        [Description("Step(s) 5 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderSchedule_UITestCases006()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5154 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType3 = commonMethodsDB.CreateCPBookingType("BTC ACC-5154 Past", 3, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            dbHelper.cpBookingType.UpdateValidTo(_bookingType3, null);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType2, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType3, false);

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
            var careProviderServiceMapping2Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType3, null, "");

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
            var careProviderContractService2Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType3, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService2Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 13, _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region System User

            //set the user as roostered user and use a persona to link it to the user
            var username = "cpsu_" + currentTimeString;
            var userFirstName = "Care Provider";
            var userLastName = "System User " + currentTimeString;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5154", "99997", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType3);

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

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var nextDueToTakePlace = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);
            var firstOccurence = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);
            var lastOccurence = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(14);

            var cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), providerId, "Express Book Testing");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingScheduleId, 1);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingScheduleId, 2, nextDueToTakePlace, firstOccurence, lastOccurence);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingScheduleId, _systemUserEmploymentContractId, systemUserId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingScheduleId, _personID, _personcontractId, careProviderContractServiceId);

            #endregion


            #region Step 5

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
               .ClickDiaryBooking(cpBookingScheduleId);


            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .SetEndDay("Tuesday")
                .SetStartDay("Tuesday");

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateIssueMessage_DynamicDialogue(string.Format("Next due to take place has been changed to {0}.", nextDueToTakePlace.AddDays(1).ToString("dd'/'MM'/'yyyy")), 0)
                .ValidateIssueMessage_DynamicDialogue(string.Format("First occurrence has been changed to {0}.", firstOccurence.AddDays(1).ToString("dd'/'MM'/'yyyy")), 1)
                .ValidateIssueMessage_DynamicDialogue(string.Format("Last occurrence has been changed to {0}.", lastOccurence.AddDays(1).ToString("dd'/'MM'/'yyyy")), 2)
                .ClickDismissButton_DynamicDialogue()

                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(string.Format("This booking will not be booked until {0}.", firstOccurence.AddDays(1).ToString("dd'/'MM'/'yyyy")), 1)
                .ValidateMessage_DynamicDialogue(string.Format("This booking will be deleted when express booking is run for a period after {0}.", lastOccurence.AddDays(1).ToString("dd'/'MM'/'yyyy")), 2)
                .ClickConfirmButton_WarningDialogue();

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad()
               .ClickDiaryBooking(cpBookingScheduleId);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .ValidateNextDueToTakePlaceDate(nextDueToTakePlace.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .ValidateFirstOccurrenceValue(firstOccurence.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .ValidateLastOccurrenceValue(lastOccurence.AddDays(1).ToString("dd'/'MM'/'yyyy"));

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5208")]
        [Description("Step(s) 6 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderSchedule_UITestCases007()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5154 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType3 = commonMethodsDB.CreateCPBookingType("BTC ACC-5154 Past", 3, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            dbHelper.cpBookingType.UpdateValidTo(_bookingType3, null);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType2, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType3, false);

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
            var careProviderServiceMapping2Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType3, null, "");

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
            var careProviderContractService2Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType3, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService2Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 13, _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region System User

            //set the user as roostered user and use a persona to link it to the user
            var username = "cpsu_" + currentTimeString;
            var userFirstName = "Care Provider";
            var userLastName = "System User " + currentTimeString;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5154", "99997", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType3);

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

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), providerId, "Express Book Testing");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingScheduleId, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingScheduleId, _systemUserEmploymentContractId, systemUserId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingScheduleId, _personID, _personcontractId, careProviderContractServiceId);

            #endregion


            #region Step 6

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
               .ClickDiaryBooking(cpBookingScheduleId);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .SelectWeeksOption("2 weeks");



            var nextDueToTakePlace = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(14);
            var nextDueToTakePlaceDateYear = nextDueToTakePlace.Year.ToString();
            var nextDueToTakePlaceDateMonth = nextDueToTakePlace.ToString("MMMM");
            var nextDueToTakePlaceDateDay = nextDueToTakePlace.Day.ToString();

            var firstOccurence = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(14);
            var firstOccurenceYear = firstOccurence.Year.ToString();
            var firstOccurenceMonth = firstOccurence.ToString("MMMM");
            var firstOccurenceDay = firstOccurence.Day.ToString();

            var lastOccurence = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);
            var lastOccurenceYear = lastOccurence.Year.ToString();
            var lastOccurenceMonth = lastOccurence.ToString("MMMM");
            var lastOccurenceDay = lastOccurence.Day.ToString();

            createScheduleBookingPopup
                .SelectNextDueToTakePlaceDate(nextDueToTakePlaceDateYear, nextDueToTakePlaceDateMonth, nextDueToTakePlaceDateDay)
                .SelectFirstOccurrenceDate(firstOccurenceYear, firstOccurenceMonth, firstOccurenceDay)
                .ValidateLastOccurrenceDateNotSelectable(lastOccurenceYear, lastOccurenceMonth, lastOccurenceDay)
                .ClickResetChangesButton()
                .ClickOccurrenceTab();



            nextDueToTakePlace = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(21);
            nextDueToTakePlaceDateYear = nextDueToTakePlace.Year.ToString();
            nextDueToTakePlaceDateMonth = nextDueToTakePlace.ToString("MMMM");
            nextDueToTakePlaceDateDay = nextDueToTakePlace.Day.ToString();

            firstOccurence = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);
            firstOccurenceYear = firstOccurence.Year.ToString();
            firstOccurenceMonth = firstOccurence.ToString("MMMM");
            firstOccurenceDay = firstOccurence.Day.ToString();

            lastOccurence = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(14);
            lastOccurenceYear = lastOccurence.Year.ToString();
            lastOccurenceMonth = lastOccurence.ToString("MMMM");
            lastOccurenceDay = lastOccurence.Day.ToString();

            createScheduleBookingPopup
                .SelectWeeksOption("2 weeks")
                .SelectFirstOccurrenceDate(firstOccurenceYear, firstOccurenceMonth, firstOccurenceDay)
                .SelectLastOccurrenceDate(lastOccurenceYear, lastOccurenceMonth, lastOccurenceDay)
                .ValidateNextDueToTakePlaceDateNotSelectable(nextDueToTakePlaceDateYear, nextDueToTakePlaceDateMonth, nextDueToTakePlaceDateDay);


            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5257

        [TestProperty("JiraIssueID", "ACC-5306")]
        [Description("Step(s) 1 to 6 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderSchedule_ACC_4790_UITestCases001()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5257 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region System User

            //set the user as roostered user and use a persona to link it to the user
            var username = "cpsu_" + currentTimeString;
            var userFirstName = "Care Provider";
            var userLastName = "System User " + currentTimeString;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5257", "99996", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType2);

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

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);

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

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            #endregion

            #region Step 3

            var dayOfWeek = DateTime.Now.ToString("dddd");

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ValidateLocationProviderText(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .ValidateBookingTypeDropDownText("Select")
                .ValidateStartDayText(dayOfWeek)
                .ValidateEndDayText(dayOfWeek)
                .ValidateStartTime("")
                .ValidateEndTime("")
                .ValidateGenderPreferenceBookingDropdownText("No Preference")
                .ValidateTextInCommentsTextArea("");

            #endregion

            #region Step 4

            createScheduleBookingPopup
                .SelectBookingType("BTC ACC-5257 1")
                .ValidateStartTime("06:00")
                .ValidateEndTime("22:00")
                .ValidateSelectedStaffFieldValues("0", "Unassigned");

            #endregion

            #region Step 5

            createScheduleBookingPopup
                .ClickCreateBooking();

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(3000);
            var bookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, bookingSchedules.Count);
            var cpBookingScheduleId = bookingSchedules[0];

            var allCPBookingScheduleStaff = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId);
            Assert.AreEqual(1, allCPBookingScheduleStaff.Count);

            providerSchedulePage
                .ClickDiaryBooking(cpBookingScheduleId);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateSelectedStaffFieldValues("0", "Unassigned");

            #endregion

            #region Step 6

            createScheduleBookingPopup
                .ClickAddUnassignedStaff()
                .ClickCreateBooking();

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(3000);

            providerSchedulePage
                .ClickDiaryBooking(cpBookingScheduleId);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad();

            allCPBookingScheduleStaff = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId);
            Assert.AreEqual(2, allCPBookingScheduleStaff.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5307")]
        [Description("Step(s) 9 to 12 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderSchedule_ACC_4790_UITestCases002()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5257 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5257", "99996", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user2name = "cpsu_2_" + currentTimeString;
            var user2FirstName = "Care Provider";
            var user2LastName = "System User 2 " + currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser2Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser2EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser2Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser2Id, _teamId, _systemUser2EmploymentContractId, _availabilityTypeId);

            #endregion


            #region Step 9


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
                .SelectBookingType("BTC ACC-5257 1")
                .SetStartTime("06", "00")
                .SetEndTime("07", "00")
                .ClickSelectPeople();

            selectMultiplePeoplePopUp
                .WaitForSelectMultiplePeoplePopUpPageToLoad()
                .ClickPeopleRecordCellText(_personcontractId.ToString())
                .ClickConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("Care Provider System User 1 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(3000);
            var bookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, bookingSchedules.Count);
            var cpBookingScheduleId = bookingSchedules[0];

            var allCPBookingScheduleStaff = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId);
            Assert.AreEqual(1, allCPBookingScheduleStaff.Count);

            providerSchedulePage
                .ClickDiaryBooking(cpBookingScheduleId);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateSelectedStaffFieldValues(_systemUser1EmploymentContractId, "Care Provider System User 1 " + currentTimeString);

            #endregion

            #region Step 10

            createScheduleBookingPopup
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("Care Provider System User 2 " + currentTimeString)
                .ClickStaffRecordCellText(_systemUser2EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(3000);
            allCPBookingScheduleStaff = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId);
            Assert.AreEqual(2, allCPBookingScheduleStaff.Count);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickDiaryBooking(cpBookingScheduleId);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateSelectedStaffFieldValues(_systemUser1EmploymentContractId, "Care Provider System User 1 " + currentTimeString)
                .ValidateSelectedStaffFieldValues(_systemUser2EmploymentContractId, "Care Provider System User 2 " + currentTimeString);

            #endregion

            #region Step 11

            //Not automatable

            #endregion

            #region Step 12

            createScheduleBookingPopup
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("Care Provider System User 1 " + currentTimeString)
                .ValidateStaffRecordNotVisible(_systemUser1EmploymentContractId)

                .EnterTextIntoFilterStaffByNameSearchBox("Care Provider System User 2 " + currentTimeString)
                .ValidateStaffRecordNotVisible(_systemUser2EmploymentContractId)

                .ClickStaffConfirmSelection();

            #endregion

            #region Step 13

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()

                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .ValidateOccurrenceTabTopLabelContainsText("Care Provider System User 1 " + currentTimeString)
                .ValidateOccurrenceTabTopLabelContainsText("Care Provider System User 2 " + currentTimeString);

            #endregion

            #region Step 14

            createScheduleBookingPopup
                .ClickRosteringTab()
                .ClickAddUnassignedStaff()
                .ClickCreateBooking();

            providerSchedulePage
               .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(3000);
            allCPBookingScheduleStaff = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId);
            Assert.AreEqual(3, allCPBookingScheduleStaff.Count);

            providerSchedulePage
                .ClickDiaryBooking(cpBookingScheduleId);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateSelectedStaffFieldValues(_systemUser1EmploymentContractId, "Care Provider System User 1 " + currentTimeString)
                .ValidateSelectedStaffFieldValues(_systemUser2EmploymentContractId, "Care Provider System User 2 " + currentTimeString)
                .GetAndValidateUnassignedStaffFieldValue("Unassigned");

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5341

        [TestProperty("JiraIssueID", "ACC-5449")]
        [Description("Step(s) 6 to 8 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4799_UITestCases001()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5341 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5341", "99995", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), providerId, "Express Book Testing");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingScheduleId, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingScheduleId, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingScheduleId, _personID, _personcontractId, careProviderContractServiceId);

            #endregion


            #region Step 6


            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ValidateRegardingLinkText("")
                .ClickRegardingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookFor("Providers")
                .TypeSearchQuery(funderProviderName).TapSearchButton().ValidateResultElementNotPresent(funderProviderID)
                .SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ValidateStatusSelectedText("Pending")
                .ValidateStatusFieldDisabled();

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            DateTime ExpressBookingStartDate;

            if (commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date > thisWeekMonday)
                ExpressBookingStartDate = thisWeekMonday.AddDays(7);
            else
                ExpressBookingStartDate = thisWeekMonday;

            var ExpressBookingEndDate = ExpressBookingStartDate.AddDays(6); //notice that Monday counts as day 1, so we just need to add 6 more days (to get the following Sunday)
            var ScheduledJobStartDate = DateTime.Now.Date;

            expressBookingCriteriaRecordPage
                .ValidateExpressBookingStartDateText(ExpressBookingStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateExpressBookingEndDateFieldDisabled()
                .ValidateExpressBookingEndDateText(ExpressBookingEndDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateScheduledJobStartDateText(ScheduledJobStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateScheduledJobEndDateTimeFieldDisabled();

            #endregion

            #region Step 7 & 8

            expressBookingCriteriaRecordPage
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingScheduleId);


            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5450")]
        [Description("Step(s) 9 to 11 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4799_UITestCases002()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5341 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5341", "99995", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), providerId, "Express Book Testing");
            var cpBookingSchedule2Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule2Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule2Id, _systemUser1EmploymentContractId, systemUser1Id);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule2Id, _personID, _personcontractId, careProviderContractServiceId);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            DateTime NextDueToTakePlace;

            if (commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date > thisWeekMonday)
                NextDueToTakePlace = thisWeekMonday.AddDays(7);
            else
                NextDueToTakePlace = thisWeekMonday;

            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 2, NextDueToTakePlace, null, null);

            var firstoccurrencedate1 = NextDueToTakePlace;
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule2Id, 1, null, firstoccurrencedate1, null);

            #endregion


            #region Step 9

            //Step 9 is invalid. if Number of weeks is greater than 1 then the “Next occurrence date“ field becomes mandatory, it cannot be empty

            #endregion

            #region Step 10


            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id);


            #endregion

            #region Step 11

            processScheduledBookingsForWeekCommencingPopup
                .ValidateRecordPresent(cpBookingSchedule2Id);

            #endregion

        }

        //deactivated because of https://advancedcsg.atlassian.net/browse/ACC-5399
        //[TestProperty("JiraIssueID", "ACC-5451")]
        //[Description("Step(s) 12 from the original test")]
        //[TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //[TestProperty("BusinessModule1", "Care Provider Scheduling")]
        //[TestProperty("Screen1", "Express Booking Criteria")]
        //public void ProviderSchedule_ACC_4799_UITestCases003()
        //{
        //    var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

        //    #region Provider

        //    var providerName = "Provider " + currentTimeString;
        //    var addressType = 10; //Home
        //    var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

        //    var funderProviderName = "Funder Provider " + currentTimeString;
        //    var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

        //    #endregion

        //    #region Booking Type

        //    var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5341 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

        //    #endregion

        //    #region Provider Allowable Booking Types

        //    commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType2, false);

        //    #endregion

        //    #region Care Provider Contract Scheme

        //    var contractScheme1Name = "CPCS_" + currentTimeString;
        //    var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
        //    var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

        //    #endregion

        //    #region Care Provider Service

        //    var careProviderServiceName = "CPS A " + currentTimeString;
        //    var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
        //    var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, true);

        //    #endregion

        //    #region Care Provider Service Mapping

        //    var careProviderServiceMapping1Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType2, null, "");

        //    #endregion

        //    #region Care Provider Extract Name

        //    var careProviderExtractName = "CPEN " + currentTimeString;
        //    var careProviderExtractNameCode = dbHelper.careProviderService.GetHighestCode() + 1;
        //    var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

        //    #endregion

        //    #region Care Provider Batch Grouping

        //    var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

        //    #endregion

        //    #region Finance Invoice Batch Setup

        //    var invoicebyid = 1; //funder
        //    var careproviderinvoicefrequencyid = 1; //Every Week
        //    var createbatchwithin = 1;
        //    var chargetodayid = 1; //Monday
        //    var whentobatchfinancetransactionsid = 3; //Does Not Matter
        //    var useenddatewhenbatchingfinancetransactions = true;
        //    var financetransactionsupto = todayDate.AddYears(1);
        //    var separateinvoices = false;

        //    dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
        //        careProviderContractScheme1Id, _careProviderBatchGroupingId,
        //        new DateTime(2023, 1, 1), new TimeSpan(0, 0, 0),
        //        invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
        //        careProviderExtractNameId, true,
        //        _teamId);

        //    #endregion

        //    #region Care Provider Rate Unit

        //    var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

        //    #endregion

        //    #region VAT Code

        //    var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

        //    #endregion

        //    #region Care Provider Contract Service

        //    var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

        //    #endregion

        //    #region Contract Service Rate Period

        //    dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);


        //    #endregion

        //    #region Ethnicity

        //    var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

        //    #endregion

        //    #region Person

        //    var firstName = "Carlos";
        //    var lastName = currentTimeString;
        //    var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

        //    #endregion

        //    #region Person Contract

        //    var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

        //    #endregion

        //    #region Person Contract Service

        //    dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

        //    #endregion

        //    #region Care Provider Staff Role Type

        //    var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5341", "99995", null, new DateTime(2020, 1, 1), null);

        //    #endregion

        //    #region Employment Contract Type - Salaried

        //    var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));

        //    #endregion

        //    #region Recurrence Patterns

        //    _recurrencePattern_Every1WeekMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on monday").First();
        //    _recurrencePattern_Every1WeekTuesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on tuesday").First();
        //    _recurrencePattern_Every1WeekWednesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on wednesday").First();
        //    _recurrencePattern_Every1WeekThursdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on thursday").First();
        //    _recurrencePattern_Every1WeekFridayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on friday").First();
        //    _recurrencePattern_Every1WeekSaturdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on saturday").First();
        //    _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();

        //    #endregion

        //    #region Availability Type

        //    var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

        //    #endregion

        //    #region System User

        //    //set the user as roostered user and use a persona to link it to the user
        //    var user1name = "cpsu_1_" + currentTimeString;
        //    var user1FirstName = "Care Provider";
        //    var user1LastName = "System User 1 " + currentTimeString;
        //    var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

        //    dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

        //    #endregion

        //    #region System User Employment Contract

        //    var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

        //    #endregion

        //    #region System User Employment Contract CP Booking Type

        //    dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

        //    #endregion

        //    #region User Work Schedule

        //    //Create the user work schedule for all days of the week
        //    CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

        //    #endregion

        //    #region Booking Schedule

        //    var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(11, 0, 0), new TimeSpan(12, 0, 0), providerId, "Express Book Testing");

        //    dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

        //    dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

        //    dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

        //    var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
        //    DateTime NextDueToTakePlace;

        //    if (commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date > thisWeekMonday)
        //        NextDueToTakePlace = thisWeekMonday.AddDays(7);
        //    else
        //        NextDueToTakePlace = thisWeekMonday;

        //    var firstoccurrencedate2 = NextDueToTakePlace.AddDays(21);
        //    dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 1, null, firstoccurrencedate2, null);

        //    #endregion


        //    #region Step 12

        //    loginPage
        //       .GoToLoginPage()
        //       .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToExpressBookSection();

        //    expressBookingCriteriaPage
        //        .WaitForExpressBookingCriteriaPageToLoad()
        //        .ClickNewRecordButton();

        //    expressBookingCriteriaRecordPage
        //        .WaitForExpressBookingCriteriaRecordPageToLoad()
        //        .ClickRegardingLookupButton();

        //    lookupPopup
        //        .WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

        //    expressBookingCriteriaRecordPage
        //        .WaitForExpressBookingCriteriaRecordPageToLoad()
        //        .ClickViewScheduledBookings();

        //    processScheduledBookingsForWeekCommencingPopup
        //        .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
        //        .ValidateRecordNotPresent(cpBookingSchedule1Id);


        //    #endregion


        //}

        [TestProperty("JiraIssueID", "ACC-5452")]
        [Description("Step(s) 13 to 14 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4799_UITestCases004()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5341 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5341", "99995", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(11, 0, 0), new TimeSpan(12, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            DateTime NextDueToTakePlace;

            if (commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date > thisWeekMonday)
                NextDueToTakePlace = thisWeekMonday.AddDays(7);
            else
                NextDueToTakePlace = thisWeekMonday;

            var firstoccurrencedate1 = NextDueToTakePlace;
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 2, NextDueToTakePlace, firstoccurrencedate1, null);

            #endregion

            #region Step 13

            //Step 13 is invalid now. If the Number of weeks is greater than 1 then the Next Due To Take Place field becomes mandatory.

            #endregion

            #region Step 14

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id);


            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-5453")]
        [Description("Step(s) 15 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4799_UITestCases005()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5341 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5341", "99995", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(11, 0, 0), new TimeSpan(12, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            DateTime firstCorrectMonday;

            if (commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date > thisWeekMonday)
                firstCorrectMonday = thisWeekMonday.AddDays(7);
            else
                firstCorrectMonday = thisWeekMonday;

            var firstoccurrencedate = firstCorrectMonday;
            var nextDueToTakePlace = firstCorrectMonday.AddDays(7);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 2, nextDueToTakePlace, firstoccurrencedate, null);

            #endregion

            #region Step 15

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id);


            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-5454")]
        [Description("Step(s) 16 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4799_UITestCases006()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5341 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5341", "99995", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(11, 0, 0), new TimeSpan(12, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            DateTime firstCorrectMonday;

            if (commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date > thisWeekMonday)
                firstCorrectMonday = thisWeekMonday.AddDays(7);
            else
                firstCorrectMonday = thisWeekMonday;

            var firstoccurrencedate = firstCorrectMonday;
            var nextDueToTakePlace = firstoccurrencedate;
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 2, nextDueToTakePlace, firstoccurrencedate, null);

            #endregion

            #region Step 16

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(firstCorrectMonday.AddDays(7).ToString("dd'/'MM'/'yyyy"))
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id);

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-5455")]
        [Description("Step(s) 17 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4799_UITestCases007()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5341 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5341", "99995", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(11, 0, 0), new TimeSpan(12, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            DateTime firstCorrectMonday;

            if (commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date > thisWeekMonday)
                firstCorrectMonday = thisWeekMonday.AddDays(7);
            else
                firstCorrectMonday = thisWeekMonday;

            var lastoccurrencedate = firstCorrectMonday.AddDays(14);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 1, null, null, lastoccurrencedate);

            #endregion

            #region Step 17

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id);

            #endregion


        }

        //deactivated because of https://advancedcsg.atlassian.net/browse/ACC-5399
        //[TestProperty("JiraIssueID", "ACC-5456")]
        //[Description("Step(s) 18 from the original test")]
        //[TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //[TestProperty("BusinessModule1", "Care Provider Scheduling")]
        //[TestProperty("Screen1", "Express Booking Criteria")]
        //public void ProviderSchedule_ACC_4799_UITestCases008()
        //{
        //    var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

        //    #region Provider

        //    var providerName = "Provider " + currentTimeString;
        //    var addressType = 10; //Home
        //    var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

        //    var funderProviderName = "Funder Provider " + currentTimeString;
        //    var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

        //    #endregion

        //    #region Booking Type

        //    var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5341 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

        //    #endregion

        //    #region Provider Allowable Booking Types

        //    commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType2, false);

        //    #endregion

        //    #region Care Provider Contract Scheme

        //    var contractScheme1Name = "CPCS_" + currentTimeString;
        //    var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
        //    var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

        //    #endregion

        //    #region Care Provider Service

        //    var careProviderServiceName = "CPS A " + currentTimeString;
        //    var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
        //    var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, true);

        //    #endregion

        //    #region Care Provider Service Mapping

        //    var careProviderServiceMapping1Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType2, null, "");

        //    #endregion

        //    #region Care Provider Extract Name

        //    var careProviderExtractName = "CPEN " + currentTimeString;
        //    var careProviderExtractNameCode = dbHelper.careProviderService.GetHighestCode() + 1;
        //    var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

        //    #endregion

        //    #region Care Provider Batch Grouping

        //    var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

        //    #endregion

        //    #region Finance Invoice Batch Setup

        //    var invoicebyid = 1; //funder
        //    var careproviderinvoicefrequencyid = 1; //Every Week
        //    var createbatchwithin = 1;
        //    var chargetodayid = 1; //Monday
        //    var whentobatchfinancetransactionsid = 3; //Does Not Matter
        //    var useenddatewhenbatchingfinancetransactions = true;
        //    var financetransactionsupto = todayDate.AddYears(1);
        //    var separateinvoices = false;

        //    dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
        //        careProviderContractScheme1Id, _careProviderBatchGroupingId,
        //        new DateTime(2023, 1, 1), new TimeSpan(0, 0, 0),
        //        invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
        //        careProviderExtractNameId, true,
        //        _teamId);

        //    #endregion

        //    #region Care Provider Rate Unit

        //    var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

        //    #endregion

        //    #region VAT Code

        //    var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

        //    #endregion

        //    #region Care Provider Contract Service

        //    var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

        //    #endregion

        //    #region Contract Service Rate Period

        //    dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);


        //    #endregion

        //    #region Ethnicity

        //    var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

        //    #endregion

        //    #region Person

        //    var firstName = "Carlos";
        //    var lastName = currentTimeString;
        //    var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

        //    #endregion

        //    #region Person Contract

        //    var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

        //    #endregion

        //    #region Person Contract Service

        //    dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

        //    #endregion

        //    #region Care Provider Staff Role Type

        //    var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5341", "99995", null, new DateTime(2020, 1, 1), null);

        //    #endregion

        //    #region Employment Contract Type - Salaried

        //    var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));

        //    #endregion

        //    #region Recurrence Patterns

        //    _recurrencePattern_Every1WeekMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on monday").First();
        //    _recurrencePattern_Every1WeekTuesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on tuesday").First();
        //    _recurrencePattern_Every1WeekWednesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on wednesday").First();
        //    _recurrencePattern_Every1WeekThursdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on thursday").First();
        //    _recurrencePattern_Every1WeekFridayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on friday").First();
        //    _recurrencePattern_Every1WeekSaturdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on saturday").First();
        //    _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();

        //    #endregion

        //    #region Availability Type

        //    var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

        //    #endregion

        //    #region System User

        //    //set the user as roostered user and use a persona to link it to the user
        //    var user1name = "cpsu_1_" + currentTimeString;
        //    var user1FirstName = "Care Provider";
        //    var user1LastName = "System User 1 " + currentTimeString;
        //    var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

        //    dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

        //    #endregion

        //    #region System User Employment Contract

        //    var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

        //    #endregion

        //    #region System User Employment Contract CP Booking Type

        //    dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

        //    #endregion

        //    #region User Work Schedule

        //    //Create the user work schedule for all days of the week
        //    CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

        //    #endregion

        //    #region Booking Schedule

        //    var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(11, 0, 0), new TimeSpan(12, 0, 0), providerId, "Express Book Testing");

        //    dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

        //    dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

        //    dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

        //    var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
        //    DateTime firstCorrectMonday;

        //    if (commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date > thisWeekMonday)
        //        firstCorrectMonday = thisWeekMonday.AddDays(7);
        //    else
        //        firstCorrectMonday = thisWeekMonday;

        //    var lastoccurrencedate = firstCorrectMonday.AddDays(7);
        //    dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 1, null, null, lastoccurrencedate);

        //    #endregion

        //    #region Step 18

        //    loginPage
        //       .GoToLoginPage()
        //       .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToExpressBookSection();

        //    expressBookingCriteriaPage
        //        .WaitForExpressBookingCriteriaPageToLoad()
        //        .ClickNewRecordButton();

        //    expressBookingCriteriaRecordPage
        //        .WaitForExpressBookingCriteriaRecordPageToLoad()
        //        .ClickRegardingLookupButton();

        //    lookupPopup
        //        .WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

        //    expressBookingCriteriaRecordPage
        //        .WaitForExpressBookingCriteriaRecordPageToLoad()
        //        .InsertTextOnExpressBookingStartDate(firstCorrectMonday.AddDays(14).ToString("dd'/'MM'/'yyyy"))
        //        .ClickViewScheduledBookings();

        //    processScheduledBookingsForWeekCommencingPopup
        //        .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
        //        .ValidateRecordNotPresent(cpBookingSchedule1Id);

        //    #endregion


        //}

        [TestProperty("JiraIssueID", "ACC-5457")]
        [Description("Step(s) 20 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4799_UITestCases009()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5341 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5341", "99995", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(11, 0, 0), new TimeSpan(12, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            DateTime firstCorrectMonday;

            if (commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date > thisWeekMonday)
                firstCorrectMonday = thisWeekMonday.AddDays(7);
            else
                firstCorrectMonday = thisWeekMonday;

            var nextDueDate = firstCorrectMonday;
            var lastoccurrencedate = firstCorrectMonday.AddDays(14);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 2, nextDueDate, null, lastoccurrencedate);

            #endregion

            #region Step 20

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id);

            #endregion


        }

        //deactivated because of https://advancedcsg.atlassian.net/browse/ACC-5399
        //[TestProperty("JiraIssueID", "ACC-5458")]
        //[Description("Step(s) 21 from the original test")]
        //[TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //[TestProperty("BusinessModule1", "Care Provider Scheduling")]
        //[TestProperty("Screen1", "Express Booking Criteria")]
        //public void ProviderSchedule_ACC_4799_UITestCases010()
        //{
        //    var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

        //    #region Provider

        //    var providerName = "Provider " + currentTimeString;
        //    var addressType = 10; //Home
        //    var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

        //    var funderProviderName = "Funder Provider " + currentTimeString;
        //    var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

        //    #endregion

        //    #region Booking Type

        //    var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5341 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

        //    #endregion

        //    #region Provider Allowable Booking Types

        //    commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType2, false);

        //    #endregion

        //    #region Care Provider Contract Scheme

        //    var contractScheme1Name = "CPCS_" + currentTimeString;
        //    var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
        //    var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

        //    #endregion

        //    #region Care Provider Service

        //    var careProviderServiceName = "CPS A " + currentTimeString;
        //    var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
        //    var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, true);

        //    #endregion

        //    #region Care Provider Service Mapping

        //    var careProviderServiceMapping1Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType2, null, "");

        //    #endregion

        //    #region Care Provider Extract Name

        //    var careProviderExtractName = "CPEN " + currentTimeString;
        //    var careProviderExtractNameCode = dbHelper.careProviderService.GetHighestCode() + 1;
        //    var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

        //    #endregion

        //    #region Care Provider Batch Grouping

        //    var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

        //    #endregion

        //    #region Finance Invoice Batch Setup

        //    var invoicebyid = 1; //funder
        //    var careproviderinvoicefrequencyid = 1; //Every Week
        //    var createbatchwithin = 1;
        //    var chargetodayid = 1; //Monday
        //    var whentobatchfinancetransactionsid = 3; //Does Not Matter
        //    var useenddatewhenbatchingfinancetransactions = true;
        //    var financetransactionsupto = todayDate.AddYears(1);
        //    var separateinvoices = false;

        //    dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
        //        careProviderContractScheme1Id, _careProviderBatchGroupingId,
        //        new DateTime(2023, 1, 1), new TimeSpan(0, 0, 0),
        //        invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
        //        careProviderExtractNameId, true,
        //        _teamId);

        //    #endregion

        //    #region Care Provider Rate Unit

        //    var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

        //    #endregion

        //    #region VAT Code

        //    var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

        //    #endregion

        //    #region Care Provider Contract Service

        //    var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

        //    #endregion

        //    #region Contract Service Rate Period

        //    dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);


        //    #endregion

        //    #region Ethnicity

        //    var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

        //    #endregion

        //    #region Person

        //    var firstName = "Carlos";
        //    var lastName = currentTimeString;
        //    var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

        //    #endregion

        //    #region Person Contract

        //    var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

        //    #endregion

        //    #region Person Contract Service

        //    dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

        //    #endregion

        //    #region Care Provider Staff Role Type

        //    var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5341", "99995", null, new DateTime(2020, 1, 1), null);

        //    #endregion

        //    #region Employment Contract Type - Salaried

        //    var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));

        //    #endregion

        //    #region Recurrence Patterns

        //    _recurrencePattern_Every1WeekMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on monday").First();
        //    _recurrencePattern_Every1WeekTuesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on tuesday").First();
        //    _recurrencePattern_Every1WeekWednesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on wednesday").First();
        //    _recurrencePattern_Every1WeekThursdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on thursday").First();
        //    _recurrencePattern_Every1WeekFridayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on friday").First();
        //    _recurrencePattern_Every1WeekSaturdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on saturday").First();
        //    _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();

        //    #endregion

        //    #region Availability Type

        //    var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

        //    #endregion

        //    #region System User

        //    //set the user as roostered user and use a persona to link it to the user
        //    var user1name = "cpsu_1_" + currentTimeString;
        //    var user1FirstName = "Care Provider";
        //    var user1LastName = "System User 1 " + currentTimeString;
        //    var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

        //    dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

        //    #endregion

        //    #region System User Employment Contract

        //    var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

        //    #endregion

        //    #region System User Employment Contract CP Booking Type

        //    dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

        //    #endregion

        //    #region User Work Schedule

        //    //Create the user work schedule for all days of the week
        //    CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

        //    #endregion

        //    #region Booking Schedule

        //    var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(11, 0, 0), new TimeSpan(12, 0, 0), providerId, "Express Book Testing");

        //    dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

        //    dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

        //    dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

        //    var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
        //    DateTime firstCorrectMonday;

        //    if (commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date > thisWeekMonday)
        //        firstCorrectMonday = thisWeekMonday.AddDays(7);
        //    else
        //        firstCorrectMonday = thisWeekMonday;

        //    var nextDueDate = firstCorrectMonday.AddDays(7);
        //    var lastoccurrencedate = firstCorrectMonday.AddDays(14);
        //    dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 2, nextDueDate, null, lastoccurrencedate);

        //    #endregion

        //    #region Step 21

        //    loginPage
        //       .GoToLoginPage()
        //       .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToExpressBookSection();

        //    expressBookingCriteriaPage
        //        .WaitForExpressBookingCriteriaPageToLoad()
        //        .ClickNewRecordButton();

        //    expressBookingCriteriaRecordPage
        //        .WaitForExpressBookingCriteriaRecordPageToLoad()
        //        .ClickRegardingLookupButton();

        //    lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

        //    expressBookingCriteriaRecordPage
        //        .WaitForExpressBookingCriteriaRecordPageToLoad()
        //        .ClickViewScheduledBookings();

        //    processScheduledBookingsForWeekCommencingPopup
        //        .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
        //        .ValidateRecordNotPresent(cpBookingSchedule1Id);

        //    #endregion


        //}

        [TestProperty("JiraIssueID", "ACC-5459")]
        [Description("Step(s) 22 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4799_UITestCases011()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5341 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5341", "99995", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(11, 0, 0), new TimeSpan(12, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            DateTime firstCorrectMonday;

            if (commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date > thisWeekMonday)
                firstCorrectMonday = thisWeekMonday.AddDays(7);
            else
                firstCorrectMonday = thisWeekMonday;

            var nextDueDate = firstCorrectMonday;
            var lastoccurrencedate = firstCorrectMonday.AddDays(21);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 2, nextDueDate, null, lastoccurrencedate);

            #endregion

            #region Step 22

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(firstCorrectMonday.AddDays(7).ToString("dd'/'MM'/'yyyy"))
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id)
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickSaveAndCloseButton();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickRefreshButton();

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            var expressBookingCriteriaId = dbHelper.cpExpressBookingCriteria.GetByRegardingID(providerId).First();
            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId).First();
            var exceptionmessage = dbHelper.cpExpressBookingResult.GetById(expressBookingResultId, "exceptionmessage")["exceptionmessage"].ToString();

            Assert.AreEqual("Booking not required. The schedule booking is not needed on this week.", exceptionmessage);

            #endregion


        }

        //deactivated because of https://advancedcsg.atlassian.net/browse/ACC-5399
        //[TestProperty("JiraIssueID", "ACC-5460")]
        //[Description("Step(s) 23 from the original test")]
        //[TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //[TestProperty("BusinessModule1", "Care Provider Scheduling")]
        //[TestProperty("Screen1", "Express Booking Criteria")]
        //public void ProviderSchedule_ACC_4799_UITestCases012()
        //{
        //    var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

        //    #region Provider

        //    var providerName = "Provider " + currentTimeString;
        //    var addressType = 10; //Home
        //    var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

        //    var funderProviderName = "Funder Provider " + currentTimeString;
        //    var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

        //    #endregion

        //    #region Booking Type

        //    var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5341 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

        //    #endregion

        //    #region Provider Allowable Booking Types

        //    commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType2, false);

        //    #endregion

        //    #region Care Provider Contract Scheme

        //    var contractScheme1Name = "CPCS_" + currentTimeString;
        //    var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
        //    var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

        //    #endregion

        //    #region Care Provider Service

        //    var careProviderServiceName = "CPS A " + currentTimeString;
        //    var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
        //    var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, true);

        //    #endregion

        //    #region Care Provider Service Mapping

        //    var careProviderServiceMapping1Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType2, null, "");

        //    #endregion

        //    #region Care Provider Extract Name

        //    var careProviderExtractName = "CPEN " + currentTimeString;
        //    var careProviderExtractNameCode = dbHelper.careProviderService.GetHighestCode() + 1;
        //    var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

        //    #endregion

        //    #region Care Provider Batch Grouping

        //    var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

        //    #endregion

        //    #region Finance Invoice Batch Setup

        //    var invoicebyid = 1; //funder
        //    var careproviderinvoicefrequencyid = 1; //Every Week
        //    var createbatchwithin = 1;
        //    var chargetodayid = 1; //Monday
        //    var whentobatchfinancetransactionsid = 3; //Does Not Matter
        //    var useenddatewhenbatchingfinancetransactions = true;
        //    var financetransactionsupto = todayDate.AddYears(1);
        //    var separateinvoices = false;

        //    dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
        //        careProviderContractScheme1Id, _careProviderBatchGroupingId,
        //        new DateTime(2023, 1, 1), new TimeSpan(0, 0, 0),
        //        invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
        //        careProviderExtractNameId, true,
        //        _teamId);

        //    #endregion

        //    #region Care Provider Rate Unit

        //    var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

        //    #endregion

        //    #region VAT Code

        //    var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

        //    #endregion

        //    #region Care Provider Contract Service

        //    var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

        //    #endregion

        //    #region Contract Service Rate Period

        //    dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);


        //    #endregion

        //    #region Ethnicity

        //    var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

        //    #endregion

        //    #region Person

        //    var firstName = "Carlos";
        //    var lastName = currentTimeString;
        //    var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

        //    #endregion

        //    #region Person Contract

        //    var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

        //    #endregion

        //    #region Person Contract Service

        //    dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

        //    #endregion

        //    #region Care Provider Staff Role Type

        //    var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5341", "99995", null, new DateTime(2020, 1, 1), null);

        //    #endregion

        //    #region Employment Contract Type - Salaried

        //    var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));

        //    #endregion

        //    #region Recurrence Patterns

        //    _recurrencePattern_Every1WeekMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on monday").First();
        //    _recurrencePattern_Every1WeekTuesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on tuesday").First();
        //    _recurrencePattern_Every1WeekWednesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on wednesday").First();
        //    _recurrencePattern_Every1WeekThursdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on thursday").First();
        //    _recurrencePattern_Every1WeekFridayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on friday").First();
        //    _recurrencePattern_Every1WeekSaturdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on saturday").First();
        //    _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();

        //    #endregion

        //    #region Availability Type

        //    var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

        //    #endregion

        //    #region System User

        //    //set the user as roostered user and use a persona to link it to the user
        //    var user1name = "cpsu_1_" + currentTimeString;
        //    var user1FirstName = "Care Provider";
        //    var user1LastName = "System User 1 " + currentTimeString;
        //    var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

        //    dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

        //    #endregion

        //    #region System User Employment Contract

        //    var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

        //    #endregion

        //    #region System User Employment Contract CP Booking Type

        //    dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

        //    #endregion

        //    #region User Work Schedule

        //    //Create the user work schedule for all days of the week
        //    CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

        //    #endregion

        //    #region Booking Schedule

        //    var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(11, 0, 0), new TimeSpan(12, 0, 0), providerId, "Express Book Testing");

        //    dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

        //    dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

        //    dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

        //    var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
        //    DateTime firstCorrectMonday;

        //    if (commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date > thisWeekMonday)
        //        firstCorrectMonday = thisWeekMonday.AddDays(7);
        //    else
        //        firstCorrectMonday = thisWeekMonday;

        //    var nextDueDate = firstCorrectMonday.AddDays(7);
        //    var lastoccurrencedate = firstCorrectMonday.AddDays(14);
        //    dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 2, nextDueDate, null, lastoccurrencedate);

        //    #endregion

        //    #region Step 23

        //    loginPage
        //       .GoToLoginPage()
        //       .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToExpressBookSection();

        //    expressBookingCriteriaPage
        //        .WaitForExpressBookingCriteriaPageToLoad()
        //        .ClickNewRecordButton();

        //    expressBookingCriteriaRecordPage
        //        .WaitForExpressBookingCriteriaRecordPageToLoad()
        //        .ClickRegardingLookupButton();

        //    lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

        //    expressBookingCriteriaRecordPage
        //        .WaitForExpressBookingCriteriaRecordPageToLoad()
        //        .ClickViewScheduledBookings();

        //    processScheduledBookingsForWeekCommencingPopup
        //        .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
        //        .ValidateRecordNotPresent(cpBookingSchedule1Id);

        //    #endregion


        //}

        //[TestProperty("JiraIssueID", "ACC-5461")]
        //[Description("Step(s) 24 from the original test")]
        //[TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //[TestProperty("BusinessModule1", "Care Provider Scheduling")]
        //[TestProperty("Screen1", "Express Booking Criteria")]
        //public void ProviderSchedule_ACC_4799_UITestCases013()
        //{
        //    var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

        //    #region Provider

        //    var providerName = "Provider " + currentTimeString;
        //    var addressType = 10; //Home
        //    var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

        //    var funderProviderName = "Funder Provider " + currentTimeString;
        //    var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

        //    #endregion

        //    #region Booking Type

        //    var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5341 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

        //    #endregion

        //    #region Provider Allowable Booking Types

        //    commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType2, false);

        //    #endregion

        //    #region Care Provider Contract Scheme

        //    var contractScheme1Name = "CPCS_" + currentTimeString;
        //    var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
        //    var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

        //    #endregion

        //    #region Care Provider Service

        //    var careProviderServiceName = "CPS A " + currentTimeString;
        //    var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
        //    var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, true);

        //    #endregion

        //    #region Care Provider Service Mapping

        //    var careProviderServiceMapping1Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType2, null, "");

        //    #endregion

        //    #region Care Provider Extract Name

        //    var careProviderExtractName = "CPEN " + currentTimeString;
        //    var careProviderExtractNameCode = dbHelper.careProviderService.GetHighestCode() + 1;
        //    var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

        //    #endregion

        //    #region Care Provider Batch Grouping

        //    var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

        //    #endregion

        //    #region Finance Invoice Batch Setup

        //    var invoicebyid = 1; //funder
        //    var careproviderinvoicefrequencyid = 1; //Every Week
        //    var createbatchwithin = 1;
        //    var chargetodayid = 1; //Monday
        //    var whentobatchfinancetransactionsid = 3; //Does Not Matter
        //    var useenddatewhenbatchingfinancetransactions = true;
        //    var financetransactionsupto = todayDate.AddYears(1);
        //    var separateinvoices = false;

        //    dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
        //        careProviderContractScheme1Id, _careProviderBatchGroupingId,
        //        new DateTime(2023, 1, 1), new TimeSpan(0, 0, 0),
        //        invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
        //        careProviderExtractNameId, true,
        //        _teamId);

        //    #endregion

        //    #region Care Provider Rate Unit

        //    var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

        //    #endregion

        //    #region VAT Code

        //    var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

        //    #endregion

        //    #region Care Provider Contract Service

        //    var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

        //    #endregion

        //    #region Contract Service Rate Period

        //    dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);


        //    #endregion

        //    #region Ethnicity

        //    var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

        //    #endregion

        //    #region Person

        //    var firstName = "Carlos";
        //    var lastName = currentTimeString;
        //    var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

        //    #endregion

        //    #region Person Contract

        //    var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

        //    #endregion

        //    #region Person Contract Service

        //    dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

        //    #endregion

        //    #region Care Provider Staff Role Type

        //    var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5341", "99995", null, new DateTime(2020, 1, 1), null);

        //    #endregion

        //    #region Employment Contract Type - Salaried

        //    var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));

        //    #endregion

        //    #region Recurrence Patterns

        //    _recurrencePattern_Every1WeekMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on monday").First();
        //    _recurrencePattern_Every1WeekTuesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on tuesday").First();
        //    _recurrencePattern_Every1WeekWednesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on wednesday").First();
        //    _recurrencePattern_Every1WeekThursdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on thursday").First();
        //    _recurrencePattern_Every1WeekFridayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on friday").First();
        //    _recurrencePattern_Every1WeekSaturdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on saturday").First();
        //    _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();

        //    #endregion

        //    #region Availability Type

        //    var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

        //    #endregion

        //    #region System User

        //    //set the user as roostered user and use a persona to link it to the user
        //    var user1name = "cpsu_1_" + currentTimeString;
        //    var user1FirstName = "Care Provider";
        //    var user1LastName = "System User 1 " + currentTimeString;
        //    var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

        //    dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

        //    #endregion

        //    #region System User Employment Contract

        //    var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

        //    #endregion

        //    #region System User Employment Contract CP Booking Type

        //    dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

        //    #endregion

        //    #region User Work Schedule

        //    //Create the user work schedule for all days of the week
        //    CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

        //    #endregion

        //    #region Booking Schedule

        //    var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(11, 0, 0), new TimeSpan(12, 0, 0), providerId, "Express Book Testing");

        //    dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

        //    dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

        //    dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

        //    var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
        //    DateTime firstCorrectMonday;

        //    if (commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date > thisWeekMonday)
        //        firstCorrectMonday = thisWeekMonday.AddDays(7);
        //    else
        //        firstCorrectMonday = thisWeekMonday;

        //    var nextDueDate = firstCorrectMonday;
        //    var firstoccurrencedate = firstCorrectMonday;
        //    var lastoccurrencedate = firstCorrectMonday.AddDays(7);
        //    dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 2, nextDueDate, firstoccurrencedate, lastoccurrencedate);

        //    #endregion

        //    #region Step 24

        //    loginPage
        //       .GoToLoginPage()
        //       .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToExpressBookSection();

        //    expressBookingCriteriaPage
        //        .WaitForExpressBookingCriteriaPageToLoad()
        //        .ClickNewRecordButton();

        //    expressBookingCriteriaRecordPage
        //        .WaitForExpressBookingCriteriaRecordPageToLoad()
        //        .ClickRegardingLookupButton();

        //    lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

        //    expressBookingCriteriaRecordPage
        //        .WaitForExpressBookingCriteriaRecordPageToLoad()
        //        .InsertTextOnExpressBookingStartDate(firstCorrectMonday.AddDays(14).ToString("dd'/'MM'/'yyyy"))
        //        .ClickViewScheduledBookings();

        //    processScheduledBookingsForWeekCommencingPopup
        //        .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
        //        .ValidateRecordNotPresent(cpBookingSchedule1Id);

        //    #endregion


        //}

        [TestProperty("JiraIssueID", "ACC-5462")]
        [Description("Step(s) 25 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4799_UITestCases014()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5341 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5341", "99995", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(11, 0, 0), new TimeSpan(12, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            DateTime firstCorrectMonday;

            if (commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date > thisWeekMonday)
                firstCorrectMonday = thisWeekMonday.AddDays(7);
            else
                firstCorrectMonday = thisWeekMonday;

            var nextDueDate = firstCorrectMonday;
            var firstoccurrencedate = firstCorrectMonday;
            var lastoccurrencedate = firstCorrectMonday.AddDays(7);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 2, nextDueDate, firstoccurrencedate, lastoccurrencedate);

            #endregion

            #region Step 25

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id);

            #endregion


        }

        //deactivated because of https://advancedcsg.atlassian.net/browse/ACC-5399
        //[TestProperty("JiraIssueID", "ACC-5463")]
        //[Description("Step(s) 26 from the original test")]
        //[TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //[TestProperty("BusinessModule1", "Care Provider Scheduling")]
        //[TestProperty("Screen1", "Express Booking Criteria")]
        //public void ProviderSchedule_ACC_4799_UITestCases015()
        //{
        //    var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

        //    #region Provider

        //    var providerName = "Provider " + currentTimeString;
        //    var addressType = 10; //Home
        //    var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

        //    var funderProviderName = "Funder Provider " + currentTimeString;
        //    var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

        //    #endregion

        //    #region Booking Type

        //    var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5341 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

        //    #endregion

        //    #region Provider Allowable Booking Types

        //    commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType2, false);

        //    #endregion

        //    #region Care Provider Contract Scheme

        //    var contractScheme1Name = "CPCS_" + currentTimeString;
        //    var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
        //    var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

        //    #endregion

        //    #region Care Provider Service

        //    var careProviderServiceName = "CPS A " + currentTimeString;
        //    var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
        //    var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, true);

        //    #endregion

        //    #region Care Provider Service Mapping

        //    var careProviderServiceMapping1Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType2, null, "");

        //    #endregion

        //    #region Care Provider Extract Name

        //    var careProviderExtractName = "CPEN " + currentTimeString;
        //    var careProviderExtractNameCode = dbHelper.careProviderService.GetHighestCode() + 1;
        //    var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

        //    #endregion

        //    #region Care Provider Batch Grouping

        //    var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

        //    #endregion

        //    #region Finance Invoice Batch Setup

        //    var invoicebyid = 1; //funder
        //    var careproviderinvoicefrequencyid = 1; //Every Week
        //    var createbatchwithin = 1;
        //    var chargetodayid = 1; //Monday
        //    var whentobatchfinancetransactionsid = 3; //Does Not Matter
        //    var useenddatewhenbatchingfinancetransactions = true;
        //    var financetransactionsupto = todayDate.AddYears(1);
        //    var separateinvoices = false;

        //    dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
        //        careProviderContractScheme1Id, _careProviderBatchGroupingId,
        //        new DateTime(2023, 1, 1), new TimeSpan(0, 0, 0),
        //        invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
        //        careProviderExtractNameId, true,
        //        _teamId);

        //    #endregion

        //    #region Care Provider Rate Unit

        //    var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

        //    #endregion

        //    #region VAT Code

        //    var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

        //    #endregion

        //    #region Care Provider Contract Service

        //    var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

        //    #endregion

        //    #region Contract Service Rate Period

        //    dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);


        //    #endregion

        //    #region Ethnicity

        //    var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

        //    #endregion

        //    #region Person

        //    var firstName = "Carlos";
        //    var lastName = currentTimeString;
        //    var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

        //    #endregion

        //    #region Person Contract

        //    var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

        //    #endregion

        //    #region Person Contract Service

        //    dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

        //    #endregion

        //    #region Care Provider Staff Role Type

        //    var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5341", "99995", null, new DateTime(2020, 1, 1), null);

        //    #endregion

        //    #region Employment Contract Type - Salaried

        //    var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));

        //    #endregion

        //    #region Recurrence Patterns

        //    _recurrencePattern_Every1WeekMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on monday").First();
        //    _recurrencePattern_Every1WeekTuesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on tuesday").First();
        //    _recurrencePattern_Every1WeekWednesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on wednesday").First();
        //    _recurrencePattern_Every1WeekThursdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on thursday").First();
        //    _recurrencePattern_Every1WeekFridayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on friday").First();
        //    _recurrencePattern_Every1WeekSaturdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on saturday").First();
        //    _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();

        //    #endregion

        //    #region Availability Type

        //    var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

        //    #endregion

        //    #region System User

        //    //set the user as roostered user and use a persona to link it to the user
        //    var user1name = "cpsu_1_" + currentTimeString;
        //    var user1FirstName = "Care Provider";
        //    var user1LastName = "System User 1 " + currentTimeString;
        //    var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

        //    dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

        //    #endregion

        //    #region System User Employment Contract

        //    var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

        //    #endregion

        //    #region System User Employment Contract CP Booking Type

        //    dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

        //    #endregion

        //    #region User Work Schedule

        //    //Create the user work schedule for all days of the week
        //    CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

        //    #endregion

        //    #region Booking Schedule

        //    var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(11, 0, 0), new TimeSpan(12, 0, 0), providerId, "Express Book Testing");

        //    dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

        //    dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

        //    dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

        //    var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
        //    DateTime firstCorrectMonday;

        //    if (commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date > thisWeekMonday)
        //        firstCorrectMonday = thisWeekMonday.AddDays(7);
        //    else
        //        firstCorrectMonday = thisWeekMonday;

        //    var nextDueDate = firstCorrectMonday.AddDays(14);
        //    var firstoccurrencedate = firstCorrectMonday.AddDays(14);
        //    var lastoccurrencedate = firstCorrectMonday.AddDays(21);
        //    dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 2, nextDueDate, firstoccurrencedate, lastoccurrencedate);

        //    #endregion

        //    #region Step 26

        //    loginPage
        //       .GoToLoginPage()
        //       .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToExpressBookSection();

        //    expressBookingCriteriaPage
        //        .WaitForExpressBookingCriteriaPageToLoad()
        //        .ClickNewRecordButton();

        //    expressBookingCriteriaRecordPage
        //        .WaitForExpressBookingCriteriaRecordPageToLoad()
        //        .ClickRegardingLookupButton();

        //    lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

        //    expressBookingCriteriaRecordPage
        //        .WaitForExpressBookingCriteriaRecordPageToLoad()
        //        .ClickViewScheduledBookings();

        //    processScheduledBookingsForWeekCommencingPopup
        //        .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
        //        .ValidateRecordNotPresent(cpBookingSchedule1Id);

        //    #endregion

        //}

        [TestProperty("JiraIssueID", "ACC-5464")]
        [Description("Step(s) 27 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4799_UITestCases016()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5341 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5341", "99995", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(11, 0, 0), new TimeSpan(12, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            DateTime firstCorrectMonday;

            if (commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date > thisWeekMonday)
                firstCorrectMonday = thisWeekMonday.AddDays(7);
            else
                firstCorrectMonday = thisWeekMonday;

            var nextDueDate = firstCorrectMonday;
            var firstoccurrencedate = firstCorrectMonday;
            var lastoccurrencedate = firstCorrectMonday.AddDays(14);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 2, nextDueDate, firstoccurrencedate, lastoccurrencedate);

            #endregion

            #region Step 27

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(firstCorrectMonday.AddDays(7).ToString("dd'/'MM'/'yyyy"))
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5465")]
        [Description("Step(s) 28 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4799_UITestCases017()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5341 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5341", "99995", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(11, 0, 0), new TimeSpan(12, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            DateTime firstCorrectMonday;

            if (commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date > thisWeekMonday)
                firstCorrectMonday = thisWeekMonday.AddDays(7);
            else
                firstCorrectMonday = thisWeekMonday;

            var nextDueDate = firstCorrectMonday;
            var firstoccurrencedate = firstCorrectMonday;
            var lastoccurrencedate = firstCorrectMonday.AddDays(14);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 2, nextDueDate, firstoccurrencedate, lastoccurrencedate);

            #endregion

            #region Step 28

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5466")]
        [Description("Step(s) 29 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4799_UITestCases018()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5341 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5341", "99995", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(11, 0, 0), new TimeSpan(12, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            DateTime firstCorrectMonday;

            if (commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date > thisWeekMonday)
                firstCorrectMonday = thisWeekMonday.AddDays(7);
            else
                firstCorrectMonday = thisWeekMonday;

            var firstoccurrencedate = firstCorrectMonday;
            var lastoccurrencedate = firstCorrectMonday.AddDays(21);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 1, null, firstoccurrencedate, lastoccurrencedate);

            #endregion

            #region Step 29

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(firstCorrectMonday.AddDays(7).ToString("dd'/'MM'/'yyyy"))
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5467")]
        [Description("Step(s) 30 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4799_UITestCases019()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5341 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5341", "99995", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(11, 0, 0), new TimeSpan(12, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            DateTime firstCorrectMonday;

            if (commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date > thisWeekMonday)
                firstCorrectMonday = thisWeekMonday.AddDays(7);
            else
                firstCorrectMonday = thisWeekMonday;

            var nextDueDate = firstCorrectMonday.AddDays(7);
            var firstoccurrencedate = firstCorrectMonday;
            var lastoccurrencedate = firstCorrectMonday.AddDays(21);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 2, nextDueDate, firstoccurrencedate, lastoccurrencedate);

            #endregion

            #region Step 30

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(firstCorrectMonday.AddDays(7).ToString("dd'/'MM'/'yyyy"))
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5468")]
        [Description("Step(s) 32 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4799_UITestCases020()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5341 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5341", "99995", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(11, 0, 0), new TimeSpan(12, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            DateTime firstCorrectMonday;

            if (commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date > thisWeekMonday)
                firstCorrectMonday = thisWeekMonday.AddDays(7);
            else
                firstCorrectMonday = thisWeekMonday;

            var nextDueDate = firstCorrectMonday;
            var firstoccurrencedate = firstCorrectMonday;
            var lastoccurrencedate = firstCorrectMonday.AddDays(21);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 2, nextDueDate, firstoccurrencedate, lastoccurrencedate);

            #endregion

            #region Step 32

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(firstCorrectMonday.AddDays(7).ToString("dd'/'MM'/'yyyy"))
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id)
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickSaveAndCloseButton();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickRefreshButton();



            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            var expressBookingCriteriaId = dbHelper.cpExpressBookingCriteria.GetByRegardingID(providerId).First();
            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId).First();
            var exceptionmessage = dbHelper.cpExpressBookingResult.GetById(expressBookingResultId, "exceptionmessage")["exceptionmessage"].ToString();

            Assert.AreEqual("Booking not required. The schedule booking is not needed on this week.", exceptionmessage);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5517

        [TestProperty("JiraIssueID", "ACC-5540")]
        [Description("Step(s) 1 to 2 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4820_UITestCases001()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5517 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5517", "99995", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), providerId, "Express Book Testing");
            var cpBookingSchedule2Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule2Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule2Id, _systemUser1EmploymentContractId, systemUser1Id);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule2Id, _personID, _personcontractId, careProviderContractServiceId);

            #endregion


            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id)
                .ValidateRecordPresent(cpBookingSchedule2Id)
                .ClickCloseAndSaveButton();

            var nextWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(nextWeekMonday.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveAndCloseButton();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickRefreshButton();

            var expressBookings = dbHelper.cpExpressBookingCriteria.GetByRegardingID(providerId);
            Assert.AreEqual(1, expressBookings.Count);

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            //validate that the Express Booking Criteria status changes to Succeeded
            var expressBookingCriteriaId = expressBookings.First();
            var expressBookingCriteriaStatusId = (int)(dbHelper.cpExpressBookingCriteria.GetByID(expressBookingCriteriaId, "statusid")["statusid"]);
            Assert.AreEqual(3, expressBookingCriteriaStatusId);

            //Validate that we have no errors logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(0, expressBookingResults.Count);

            //validate that the 2 Diary Bookings are created
            var diaryBookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(2, diaryBookings.Count);


            //at this point we will create an express booking for the same week
            expressBookingCriteriaPage
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id)
                .ValidateRecordPresent(cpBookingSchedule2Id)
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(nextWeekMonday.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveAndCloseButton();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickRefreshButton();

            expressBookings = dbHelper.cpExpressBookingCriteria.GetByRegardingID(providerId);
            Assert.AreEqual(2, expressBookings.Count);

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            //validate that the Express Booking Criteria status changes to Succeeded
            var expressBookingCriteria2Id = expressBookings.Where(c => c != expressBookingCriteriaId).First();
            expressBookingCriteriaStatusId = (int)(dbHelper.cpExpressBookingCriteria.GetByID(expressBookingCriteria2Id, "statusid")["statusid"]);
            Assert.AreEqual(3, expressBookingCriteriaStatusId);

            //get the results for the 2nd express booking criteria record
            expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteria2Id);
            Assert.AreEqual(2, expressBookingResults.Count);

            //first result should have an exception message logged
            var exceptionmessage = dbHelper.cpExpressBookingResult.GetById(expressBookingResults[0], "exceptionmessage")["exceptionmessage"].ToString();
            Assert.AreEqual("Booking already express booked", exceptionmessage);


            //second result should have an exception message logged
            exceptionmessage = dbHelper.cpExpressBookingResult.GetById(expressBookingResults[1], "exceptionmessage")["exceptionmessage"].ToString();
            Assert.AreEqual("Booking already express booked", exceptionmessage);

            //validate that we still have only 2 Diary Bookings created created for the provider
            diaryBookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(2, diaryBookings.Count);

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-5541")]
        [Description("Step(s) 3 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4820_UITestCases002()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5517 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5517", "99995", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), providerId, "Express Book Testing");
            var cpBookingSchedule2Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule2Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule2Id, _systemUser1EmploymentContractId, systemUser1Id);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule2Id, _personID, _personcontractId, careProviderContractServiceId);

            #endregion


            #region Step 3

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id)
                .ValidateRecordPresent(cpBookingSchedule2Id)
                .ClickCloseAndSaveButton();

            var nextWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(nextWeekMonday.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveAndCloseButton();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickRefreshButton();

            var expressBookings = dbHelper.cpExpressBookingCriteria.GetByRegardingID(providerId);
            Assert.AreEqual(1, expressBookings.Count);

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            //validate that the Express Booking Criteria status changes to Succeeded
            var expressBookingCriteriaId = expressBookings.First();
            var expressBookingCriteriaStatusId = (int)(dbHelper.cpExpressBookingCriteria.GetByID(expressBookingCriteriaId, "statusid")["statusid"]);
            Assert.AreEqual(3, expressBookingCriteriaStatusId);

            //Validate that we have no errors logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(0, expressBookingResults.Count);

            //validate that the 2 Diary Bookings are created
            var diaryBookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(2, diaryBookings.Count);


            #region Booking Schedule

            //create the 3rd schedule booking record
            var cpBookingSchedule3Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(10, 15, 0), new TimeSpan(11, 0, 0), providerId, "Express Book Testing");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule3Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule3Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule3Id, _personID, _personcontractId, careProviderContractServiceId);

            #endregion


            //at this point we will create an express booking for the same week
            expressBookingCriteriaPage
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id)
                .ValidateRecordPresent(cpBookingSchedule2Id)
                .ValidateRecordPresent(cpBookingSchedule3Id)
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(nextWeekMonday.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveAndCloseButton();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickRefreshButton();

            expressBookings = dbHelper.cpExpressBookingCriteria.GetByRegardingID(providerId);
            Assert.AreEqual(2, expressBookings.Count);

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            //validate that the Express Booking Criteria status changes to Succeeded
            var expressBookingCriteria2Id = expressBookings.Where(c => c != expressBookingCriteriaId).First();
            expressBookingCriteriaStatusId = (int)(dbHelper.cpExpressBookingCriteria.GetByID(expressBookingCriteria2Id, "statusid")["statusid"]);
            Assert.AreEqual(3, expressBookingCriteriaStatusId);

            //get the results for the 2nd express booking criteria record
            expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteria2Id);
            Assert.AreEqual(2, expressBookingResults.Count);

            //first result should have an exception message logged
            var exceptionmessage = dbHelper.cpExpressBookingResult.GetById(expressBookingResults[0], "exceptionmessage")["exceptionmessage"].ToString();
            Assert.AreEqual("Booking already express booked", exceptionmessage);


            //second result should have an exception message logged
            exceptionmessage = dbHelper.cpExpressBookingResult.GetById(expressBookingResults[1], "exceptionmessage")["exceptionmessage"].ToString();
            Assert.AreEqual("Booking already express booked", exceptionmessage);

            //validate that we now have 3 Diary Bookings created created for the provider
            diaryBookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(3, diaryBookings.Count);

            #endregion


        }


        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5518

        [TestProperty("JiraIssueID", "ACC-5588")]
        [Description("Step(s) 1 to 2 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_5544_UITestCases001()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5544 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5544", "99994", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

            #endregion


            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id)
                .ClickCloseAndSaveButton();

            var nextWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(nextWeekMonday.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveAndCloseButton();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickRefreshButton();

            var expressBookings = dbHelper.cpExpressBookingCriteria.GetByRegardingID(providerId);
            Assert.AreEqual(1, expressBookings.Count);

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            //validate that the Express Booking Criteria status changes to Succeeded
            var expressBookingCriteriaId = expressBookings.First();
            var expressBookingCriteriaStatusId = (int)(dbHelper.cpExpressBookingCriteria.GetByID(expressBookingCriteriaId, "statusid")["statusid"]);
            Assert.AreEqual(3, expressBookingCriteriaStatusId);

            //Validate that we have no errors logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(0, expressBookingResults.Count);

            //validate that the 1 Diary Bookings is created
            var diaryBookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(1, diaryBookings.Count);

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-5589")]
        [Description("Step(s) 4 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_5544_UITestCases002()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5544 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5544", "99994", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), providerId, "Express Book Testing");

            var nextWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 2, nextWeekMonday, null, null);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

            #endregion


            #region Step 4

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id)
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(nextWeekMonday.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveAndCloseButton();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickRefreshButton();

            var expressBookings = dbHelper.cpExpressBookingCriteria.GetByRegardingID(providerId);
            Assert.AreEqual(1, expressBookings.Count);

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            //validate that the Express Booking Criteria status changes to Succeeded
            var expressBookingCriteriaId = expressBookings.First();
            var expressBookingCriteriaStatusId = (int)(dbHelper.cpExpressBookingCriteria.GetByID(expressBookingCriteriaId, "statusid")["statusid"]);
            Assert.AreEqual(3, expressBookingCriteriaStatusId);

            //Validate that we have no errors logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(0, expressBookingResults.Count);

            //validate that the 1 Diary Bookings is created
            var diaryBookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(1, diaryBookings.Count);

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-5590")]
        [Description("Step(s) 5 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_5544_UITestCases003()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5544 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5544", "99994", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), providerId, "Express Book Testing");

            var nextWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 1, null, nextWeekMonday, null);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

            #endregion


            #region Step 5

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id)
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(nextWeekMonday.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveAndCloseButton();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickRefreshButton();

            var expressBookings = dbHelper.cpExpressBookingCriteria.GetByRegardingID(providerId);
            Assert.AreEqual(1, expressBookings.Count);

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            //validate that the Express Booking Criteria status changes to Succeeded
            var expressBookingCriteriaId = expressBookings.First();
            var expressBookingCriteriaStatusId = (int)(dbHelper.cpExpressBookingCriteria.GetByID(expressBookingCriteriaId, "statusid")["statusid"]);
            Assert.AreEqual(3, expressBookingCriteriaStatusId);

            //Validate that we have no errors logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(0, expressBookingResults.Count);

            //validate that the 1 Diary Bookings is created
            var diaryBookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(1, diaryBookings.Count);

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-5591")]
        [Description("Step(s) 6 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_5544_UITestCases004()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5544 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5544", "99994", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), providerId, "Express Book Testing");

            var nextWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);
            DateTime? nextoccurrencedate = null;
            DateTime? firstoccurrencedate = nextWeekMonday.AddDays(21);
            DateTime? lastoccurrencedate = null;
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 1, nextoccurrencedate, firstoccurrencedate, lastoccurrencedate);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

            #endregion


            #region Step 6

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id)
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(nextWeekMonday.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveAndCloseButton();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickRefreshButton();

            var expressBookings = dbHelper.cpExpressBookingCriteria.GetByRegardingID(providerId);
            Assert.AreEqual(1, expressBookings.Count);

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            //validate that the Express Booking Criteria status changes to Succeeded
            var expressBookingCriteriaId = expressBookings.First();
            var expressBookingCriteriaStatusId = (int)(dbHelper.cpExpressBookingCriteria.GetByID(expressBookingCriteriaId, "statusid")["statusid"]);
            Assert.AreEqual(3, expressBookingCriteriaStatusId);

            //Validate that we have no errors logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResults.Count);
            var exceptionmessage = dbHelper.cpExpressBookingResult.GetById(expressBookingResults[0], "exceptionmessage")["exceptionmessage"].ToString();
            Assert.AreEqual("Booking not required. The express booking period is before the first occurrence of the booking.", exceptionmessage);

            //validate that the 0 Diary Bookings is created
            var diaryBookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(0, diaryBookings.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5592")]
        [Description("Step(s) 9 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_5544_UITestCases005()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5544 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5544", "99994", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), providerId, "Express Book Testing");

            var nextWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);
            DateTime? nextoccurrencedate = nextWeekMonday.AddDays(7);
            DateTime? firstoccurrencedate = nextWeekMonday;
            DateTime? lastoccurrencedate = null;
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 2, nextoccurrencedate, firstoccurrencedate, lastoccurrencedate);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

            #endregion


            #region Step 9

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id)
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(nextWeekMonday.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveAndCloseButton();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickRefreshButton();

            var expressBookings = dbHelper.cpExpressBookingCriteria.GetByRegardingID(providerId);
            Assert.AreEqual(1, expressBookings.Count);

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            //validate that the Express Booking Criteria status changes to Succeeded
            var expressBookingCriteriaId = expressBookings.First();
            var expressBookingCriteriaStatusId = (int)(dbHelper.cpExpressBookingCriteria.GetByID(expressBookingCriteriaId, "statusid")["statusid"]);
            Assert.AreEqual(3, expressBookingCriteriaStatusId);

            //Validate that we have no errors logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResults.Count);
            var exceptionmessage = dbHelper.cpExpressBookingResult.GetById(expressBookingResults[0], "exceptionmessage")["exceptionmessage"].ToString();
            Assert.AreEqual("Booking not required. The schedule booking is not needed on this week.", exceptionmessage);

            //validate that the 0 Diary Bookings is created
            var diaryBookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(0, diaryBookings.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5593")]
        [Description("Step(s) 10 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_5544_UITestCases006()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5544 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5544", "99994", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), providerId, "Express Book Testing");

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            var nextWeekMonday = thisWeekMonday.AddDays(7);
            DateTime? nextoccurrencedate = nextWeekMonday;
            DateTime? firstoccurrencedate = nextWeekMonday;
            DateTime? lastoccurrencedate = null;
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 2, nextoccurrencedate, firstoccurrencedate, lastoccurrencedate);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

            #endregion


            #region Step 10

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id)
                .ClickCloseAndSaveButton();

            var expressBookingStartDate = nextWeekMonday.AddDays(7).ToString("dd'/'MM'/'yyyy");

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(expressBookingStartDate)
                .ClickSaveAndCloseButton();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickRefreshButton();

            var expressBookings = dbHelper.cpExpressBookingCriteria.GetByRegardingID(providerId);
            Assert.AreEqual(1, expressBookings.Count);

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            //validate that the Express Booking Criteria status changes to Succeeded
            var expressBookingCriteriaId = expressBookings.First();
            var expressBookingCriteriaStatusId = (int)(dbHelper.cpExpressBookingCriteria.GetByID(expressBookingCriteriaId, "statusid")["statusid"]);
            Assert.AreEqual(3, expressBookingCriteriaStatusId);

            //Validate that we have no errors logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResults.Count);
            var exceptionmessage = dbHelper.cpExpressBookingResult.GetById(expressBookingResults[0], "exceptionmessage")["exceptionmessage"].ToString();
            Assert.AreEqual("Booking not required. The schedule booking is not needed on this week.", exceptionmessage);

            //validate that the 0 Diary Bookings is created
            var diaryBookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(0, diaryBookings.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5594")]
        [Description("Step(s) 11 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_5544_UITestCases007()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5544 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5544", "99994", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), providerId, "Express Book Testing");

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            var nextWeekMonday = thisWeekMonday.AddDays(7);
            DateTime? nextoccurrencedate = null;
            DateTime? firstoccurrencedate = null;
            DateTime? lastoccurrencedate = nextWeekMonday;
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 1, nextoccurrencedate, firstoccurrencedate, lastoccurrencedate);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

            #endregion


            #region Step 11

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id)
                .ClickCloseAndSaveButton();

            var expressBookingStartDate = nextWeekMonday.ToString("dd'/'MM'/'yyyy");

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(expressBookingStartDate)
                .ClickSaveAndCloseButton();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickRefreshButton();

            var expressBookings = dbHelper.cpExpressBookingCriteria.GetByRegardingID(providerId);
            Assert.AreEqual(1, expressBookings.Count);

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            //validate that the Express Booking Criteria status changes to Succeeded
            var expressBookingCriteriaId = expressBookings.First();
            var expressBookingCriteriaStatusId = (int)(dbHelper.cpExpressBookingCriteria.GetByID(expressBookingCriteriaId, "statusid")["statusid"]);
            Assert.AreEqual(3, expressBookingCriteriaStatusId);

            //Validate that we have no errors logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(0, expressBookingResults.Count);

            //validate that the 0 Diary Bookings is created
            var diaryBookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(1, diaryBookings.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5595")]
        [Description("Step(s) 12 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_5544_UITestCases008()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5544 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5544", "99994", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), providerId, "Express Book Testing");

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            var nextWeekMonday = thisWeekMonday.AddDays(7);
            DateTime? nextoccurrencedate = null;
            DateTime? firstoccurrencedate = null;
            DateTime? lastoccurrencedate = nextWeekMonday;
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 1, nextoccurrencedate, firstoccurrencedate, lastoccurrencedate);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

            #endregion


            #region Step 12

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id)
                .ClickCloseAndSaveButton();

            var expressBookingStartDate = nextWeekMonday.AddDays(7).ToString("dd'/'MM'/'yyyy");

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(expressBookingStartDate)
                .ClickSaveAndCloseButton();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickRefreshButton();

            var expressBookings = dbHelper.cpExpressBookingCriteria.GetByRegardingID(providerId);
            Assert.AreEqual(1, expressBookings.Count);

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            //validate that the Express Booking Criteria status changes to Succeeded
            var expressBookingCriteriaId = expressBookings.First();
            var expressBookingCriteriaStatusId = (int)(dbHelper.cpExpressBookingCriteria.GetByID(expressBookingCriteriaId, "statusid")["statusid"]);
            Assert.AreEqual(3, expressBookingCriteriaStatusId);

            //Validate that we have no errors logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResults.Count);
            var exceptionmessage = dbHelper.cpExpressBookingResult.GetById(expressBookingResults[0], "exceptionmessage")["exceptionmessage"].ToString();
            Assert.AreEqual("Booking not required. The express booking period is after the last occurrence of the booking.", exceptionmessage);

            //validate that the 0 Diary Bookings is created
            var diaryBookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(0, diaryBookings.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5596")]
        [Description("Step(s) 13 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_5544_UITestCases009()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5544 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5544", "99994", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), providerId, "Express Book Testing");

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            var nextWeekMonday = thisWeekMonday.AddDays(7);
            DateTime? nextoccurrencedate = null;
            DateTime? firstoccurrencedate = null;
            DateTime? lastoccurrencedate = nextWeekMonday.AddDays(7);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 1, nextoccurrencedate, firstoccurrencedate, lastoccurrencedate);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

            #endregion


            #region Step 13

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id)
                .ClickCloseAndSaveButton();

            var expressBookingStartDate = nextWeekMonday.ToString("dd'/'MM'/'yyyy");

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(expressBookingStartDate)
                .ClickSaveAndCloseButton();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickRefreshButton();

            var expressBookings = dbHelper.cpExpressBookingCriteria.GetByRegardingID(providerId);
            Assert.AreEqual(1, expressBookings.Count);

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            //validate that the Express Booking Criteria status changes to Succeeded
            var expressBookingCriteriaId = expressBookings.First();
            var expressBookingCriteriaStatusId = (int)(dbHelper.cpExpressBookingCriteria.GetByID(expressBookingCriteriaId, "statusid")["statusid"]);
            Assert.AreEqual(3, expressBookingCriteriaStatusId);

            //Validate that we have no errors logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(0, expressBookingResults.Count);

            //validate that the 0 Diary Bookings is created
            var diaryBookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(1, diaryBookings.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5597")]
        [Description("Step(s) 14 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_5544_UITestCases010()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5544 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5544", "99994", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), providerId, "Express Book Testing");

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            var nextWeekMonday = thisWeekMonday.AddDays(7);
            DateTime? nextoccurrencedate = nextWeekMonday;
            DateTime? firstoccurrencedate = null;
            DateTime? lastoccurrencedate = nextWeekMonday;
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 2, nextoccurrencedate, firstoccurrencedate, lastoccurrencedate);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

            #endregion


            #region Step 14

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id)
                .ClickCloseAndSaveButton();

            var expressBookingStartDate = nextWeekMonday.ToString("dd'/'MM'/'yyyy");

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(expressBookingStartDate)
                .ClickSaveAndCloseButton();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickRefreshButton();

            var expressBookings = dbHelper.cpExpressBookingCriteria.GetByRegardingID(providerId);
            Assert.AreEqual(1, expressBookings.Count);

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            //validate that the Express Booking Criteria status changes to Succeeded
            var expressBookingCriteriaId = expressBookings.First();
            var expressBookingCriteriaStatusId = (int)(dbHelper.cpExpressBookingCriteria.GetByID(expressBookingCriteriaId, "statusid")["statusid"]);
            Assert.AreEqual(3, expressBookingCriteriaStatusId);

            //Validate that we have no errors logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(0, expressBookingResults.Count);

            //validate that the 0 Diary Bookings is created
            var diaryBookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(1, diaryBookings.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5598")]
        [Description("Step(s) 15 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_5544_UITestCases011()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5544 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5544", "99994", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), providerId, "Express Book Testing");

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            var nextWeekMonday = thisWeekMonday.AddDays(7);
            DateTime? nextoccurrencedate = nextWeekMonday.AddDays(7);
            DateTime? firstoccurrencedate = null;
            DateTime? lastoccurrencedate = nextWeekMonday.AddDays(21);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 2, nextoccurrencedate, firstoccurrencedate, lastoccurrencedate);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

            #endregion


            #region Step 15

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id)
                .ClickCloseAndSaveButton();

            var expressBookingStartDate = nextWeekMonday.ToString("dd'/'MM'/'yyyy");

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(expressBookingStartDate)
                .ClickSaveAndCloseButton();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickRefreshButton();

            var expressBookings = dbHelper.cpExpressBookingCriteria.GetByRegardingID(providerId);
            Assert.AreEqual(1, expressBookings.Count);

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            //validate that the Express Booking Criteria status changes to Succeeded
            var expressBookingCriteriaId = expressBookings.First();
            var expressBookingCriteriaStatusId = (int)(dbHelper.cpExpressBookingCriteria.GetByID(expressBookingCriteriaId, "statusid")["statusid"]);
            Assert.AreEqual(3, expressBookingCriteriaStatusId);

            //Validate that we have no errors logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResults.Count);
            var exceptionmessage = dbHelper.cpExpressBookingResult.GetById(expressBookingResults[0], "exceptionmessage")["exceptionmessage"].ToString();
            Assert.AreEqual("Booking not required. The schedule booking is not needed on this week.", exceptionmessage);

            //validate that the 0 Diary Bookings is created
            var diaryBookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(0, diaryBookings.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5599")]
        [Description("Step(s) 16 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_5544_UITestCases012()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5544 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5544", "99994", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), providerId, "Express Book Testing");

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            var nextWeekMonday = thisWeekMonday.AddDays(7);
            DateTime? nextoccurrencedate = nextWeekMonday;
            DateTime? firstoccurrencedate = null;
            DateTime? lastoccurrencedate = nextWeekMonday.AddDays(21);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 2, nextoccurrencedate, firstoccurrencedate, lastoccurrencedate);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);

            #endregion


            #region Step 16

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id)
                .ClickCloseAndSaveButton();

            var expressBookingStartDate = nextWeekMonday.AddDays(7).ToString("dd'/'MM'/'yyyy");

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(expressBookingStartDate)
                .ClickSaveAndCloseButton();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickRefreshButton();

            var expressBookings = dbHelper.cpExpressBookingCriteria.GetByRegardingID(providerId);
            Assert.AreEqual(1, expressBookings.Count);

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            //validate that the Express Booking Criteria status changes to Succeeded
            var expressBookingCriteriaId = expressBookings.First();
            var expressBookingCriteriaStatusId = (int)(dbHelper.cpExpressBookingCriteria.GetByID(expressBookingCriteriaId, "statusid")["statusid"]);
            Assert.AreEqual(3, expressBookingCriteriaStatusId);

            //Validate that we have no errors logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResults.Count);
            var exceptionmessage = dbHelper.cpExpressBookingResult.GetById(expressBookingResults[0], "exceptionmessage")["exceptionmessage"].ToString();
            Assert.AreEqual("Booking not required. The schedule booking is not needed on this week.", exceptionmessage);

            //validate that the 0 Diary Bookings is created
            var diaryBookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(0, diaryBookings.Count);

            //next due to take place date should NOT be updated for the scheduled booking because no Diary Booking was created
            var nextDueDateUpdated = (DateTime)(dbHelper.cpBookingSchedule.GetById(cpBookingSchedule1Id, "nextoccurrencedate")["nextoccurrencedate"]);
            Assert.AreEqual(nextWeekMonday, nextDueDateUpdated);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5600")]
        [Description("Step(s) 17 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_5544_UITestCases013()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5544 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5544", "99994", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), providerId, "Express Book Testing");
            var cpBookingSchedule2Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(8, 0, 0), new TimeSpan(9, 0, 0), providerId, "Express Book Testing");
            var cpBookingSchedule3Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0), providerId, "Express Book Testing");

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            var nextWeekMonday = thisWeekMonday.AddDays(7);

            DateTime? nextoccurrencedate1 = null;
            DateTime? firstoccurrencedate1 = nextWeekMonday;
            DateTime? lastoccurrencedate1 = nextWeekMonday.AddDays(7);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 1, nextoccurrencedate1, firstoccurrencedate1, lastoccurrencedate1);

            DateTime? nextoccurrencedate2 = null;
            DateTime? firstoccurrencedate2 = nextWeekMonday.AddDays(21);
            DateTime? lastoccurrencedate2 = nextWeekMonday.AddDays(28);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule2Id, 1, nextoccurrencedate2, firstoccurrencedate2, lastoccurrencedate2);

            DateTime? nextoccurrencedate3 = nextWeekMonday.AddDays(21);
            DateTime? firstoccurrencedate3 = nextWeekMonday.AddDays(7);
            DateTime? lastoccurrencedate3 = nextWeekMonday.AddDays(21);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule3Id, 2, nextoccurrencedate3, firstoccurrencedate3, lastoccurrencedate3);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule2Id, 1);
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule3Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule2Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule3Id, _systemUser1EmploymentContractId, systemUser1Id);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule2Id, _personID, _personcontractId, careProviderContractServiceId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule3Id, _personID, _personcontractId, careProviderContractServiceId);

            #endregion


            #region Step 17

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id)
                .ClickCloseAndSaveButton();

            var expressBookingStartDate = nextWeekMonday.AddDays(14).ToString("dd'/'MM'/'yyyy");

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(expressBookingStartDate)
                .ClickSaveAndCloseButton();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickRefreshButton();

            var expressBookings = dbHelper.cpExpressBookingCriteria.GetByRegardingID(providerId);
            Assert.AreEqual(1, expressBookings.Count);

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            //validate that the Express Booking Criteria status changes to Succeeded
            var expressBookingCriteriaId = expressBookings.First();
            var expressBookingCriteriaStatusId = (int)(dbHelper.cpExpressBookingCriteria.GetByID(expressBookingCriteriaId, "statusid")["statusid"]);
            Assert.AreEqual(3, expressBookingCriteriaStatusId);

            //Validate that we have no errors logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(3, expressBookingResults.Count);

            //validate that the 0 Diary Bookings is created
            var diaryBookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(0, diaryBookings.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5601")]
        [Description("Step(s) 18 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_5544_UITestCases014()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-5544 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5544", "99994", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), providerId, "Express Book Testing");
            var cpBookingSchedule2Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(8, 0, 0), new TimeSpan(9, 0, 0), providerId, "Express Book Testing");
            var cpBookingSchedule3Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0), providerId, "Express Book Testing");

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            var nextWeekMonday = thisWeekMonday.AddDays(7);

            DateTime? nextoccurrencedate1 = null;
            DateTime? firstoccurrencedate1 = nextWeekMonday.AddDays(14);
            DateTime? lastoccurrencedate1 = nextWeekMonday.AddDays(21);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule1Id, 1, nextoccurrencedate1, firstoccurrencedate1, lastoccurrencedate1);

            DateTime? nextoccurrencedate2 = null;
            DateTime? firstoccurrencedate2 = nextWeekMonday.AddDays(7);
            DateTime? lastoccurrencedate2 = nextWeekMonday.AddDays(21);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule2Id, 1, nextoccurrencedate2, firstoccurrencedate2, lastoccurrencedate2);

            DateTime? nextoccurrencedate3 = nextWeekMonday.AddDays(14);
            DateTime? firstoccurrencedate3 = nextWeekMonday.AddDays(7);
            DateTime? lastoccurrencedate3 = nextWeekMonday.AddDays(21);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingSchedule3Id, 2, nextoccurrencedate3, firstoccurrencedate3, lastoccurrencedate3);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule2Id, 1);
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule3Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule2Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule3Id, _systemUser1EmploymentContractId, systemUser1Id);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractServiceId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule2Id, _personID, _personcontractId, careProviderContractServiceId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule3Id, _personID, _personcontractId, careProviderContractServiceId);

            #endregion


            #region Step 18

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(providerName, providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateRecordPresent(cpBookingSchedule1Id)
                .ClickCloseAndSaveButton();

            var expressBookingStartDate = nextWeekMonday.AddDays(14).ToString("dd'/'MM'/'yyyy");

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(expressBookingStartDate)
                .ClickSaveAndCloseButton();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickRefreshButton();

            var expressBookings = dbHelper.cpExpressBookingCriteria.GetByRegardingID(providerId);
            Assert.AreEqual(1, expressBookings.Count);

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            //validate that the Express Booking Criteria status changes to Succeeded
            var expressBookingCriteriaId = expressBookings.First();
            var expressBookingCriteriaStatusId = (int)(dbHelper.cpExpressBookingCriteria.GetByID(expressBookingCriteriaId, "statusid")["statusid"]);
            Assert.AreEqual(3, expressBookingCriteriaStatusId);

            //Validate that we have no errors logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(0, expressBookingResults.Count);

            //validate that the 0 Diary Bookings is created
            var diaryBookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(3, diaryBookings.Count);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5763

        [TestProperty("JiraIssueID", "ACC-6027")]
        [Description("Step(s) 1 to 5 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4835_UITestCases001()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-4835 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 4835", "99992", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            DateTime mondayDate = thisWeekMonday;
            if (DateTime.Now.Date > thisWeekMonday)
                mondayDate = thisWeekMonday.AddDays(7);

            dbHelper.userWorkSchedule.CreateUserWorkSchedule(systemUser1Id, _teamId, _recurrencePattern_Every1WeekMondayId, _systemUser1EmploymentContractId, _availabilityTypeId, mondayDate, null, new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0), 1);


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

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC ACC-4835 1")
                .SetStartDay("Monday")
                .SetEndDay("Monday")
                .SetStartTime("09", "00")
                .SetEndTime("09", "15")
                .ClickSelectPeople();

            selectMultiplePeoplePopUp
                .WaitForSelectMultiplePeoplePopUpPageToLoad()
                .ClickPeopleRecordCellText(_personcontractId.ToString())
                .ClickConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickManageStaffButton();

            //Need to uncheck "Only Show Available Staff" because of a bug
            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .ClickOnlyShowAvailableStaff()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(4000);

            //Validate that the 1 Diary Bookings is created
            var diaryBookings = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, diaryBookings.Count);
            var bookingID = diaryBookings[0];

            #endregion

            #region Step 3

            //Same as step 2

            #endregion

            #region Step 4

            //Same as step 2

            #endregion

            #region Step 5

            providerSchedulePage
                .ClickDiaryBooking(bookingID);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .SetEndTime("10", "00")
                .ClickCreateBooking();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(4000);

            //Validate that the 1 Diary Bookings is created
            diaryBookings = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, diaryBookings.Count);

            var endTime = (TimeSpan)(dbHelper.cpBookingSchedule.GetById(bookingID, "endtime")["endtime"]);
            Assert.AreEqual(new TimeSpan(10, 0, 0), endTime);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-6028")]
        [Description("Step(s) 6 to 7 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4835_UITestCases002()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-4835 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 4835", "99992", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            DateTime mondayDate = thisWeekMonday;
            if (DateTime.Now.Date > thisWeekMonday)
                mondayDate = thisWeekMonday.AddDays(7);

            dbHelper.userWorkSchedule.CreateUserWorkSchedule(systemUser1Id, _teamId, _recurrencePattern_Every1WeekMondayId, _systemUser1EmploymentContractId, _availabilityTypeId, mondayDate, null, new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0), 1);


            #endregion


            #region Step 6

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
                .SelectBookingType("BTC ACC-4835 1")
                .SetStartDay("Monday")
                .SetEndDay("Monday")
                .SetStartTime("09", "00")
                .SetEndTime("10", "30")
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
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking()
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(user1FirstName + " " + user1LastName + ", CPSRT 4835, 47.00 hrs, 01/01/2022 (Active) is not available at this time.", 1)
                .ClickDismissButton_DynamicDialogue();

            #endregion

            #region Step 7

            createScheduleBookingPopup
                .SetEndTime("09", "15")
                .ClickCreateBooking();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(4000);

            //Validate that the 1 Diary Bookings is created
            var diaryBookings = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, diaryBookings.Count);
            var bookingID = diaryBookings[0];

            providerSchedulePage
                .ClickDiaryBooking(bookingID);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .SetEndTime("10", "30")
                .ClickCreateBooking()
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(user1FirstName + " " + user1LastName + ", CPSRT 4835, 47.00 hrs, 01/01/2022 (Active) is not available at this time.", 1)
                .ClickDismissButton_DynamicDialogue();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6029")]
        [Description("Step(s) 8 to 9 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4835_UITestCases003()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-4835 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 4835", "99992", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            DateTime mondayDate = thisWeekMonday;
            if (DateTime.Now.Date > thisWeekMonday)
                mondayDate = thisWeekMonday.AddDays(7);

            dbHelper.userWorkSchedule.CreateUserWorkSchedule(systemUser1Id, _teamId, _recurrencePattern_Every1WeekMondayId, _systemUser1EmploymentContractId, _availabilityTypeId, mondayDate,
                null, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), 1);


            #endregion


            #region Step 8

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
                .SelectBookingType("BTC ACC-4835 1")
                .SetStartDay("Monday")
                .SetEndDay("Monday")
                .SetStartTime("00", "00")
                .SetEndTime("06", "30")
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
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking()
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(user1FirstName + " " + user1LastName + ", CPSRT 4835, 47.00 hrs, 01/01/2022 (Active) is not available at this time.", 1)
                .ClickDismissButton_DynamicDialogue();

            #endregion

            #region Step 9

            createScheduleBookingPopup
                .SetStartTime("09", "00")
                .SetEndTime("09", "15")
                .ClickCreateBooking();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(4000);

            //Validate that the 1 Diary Bookings is created
            var diaryBookings = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, diaryBookings.Count);
            var bookingID = diaryBookings[0];

            providerSchedulePage
                .ClickDiaryBooking(bookingID);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .SetStartTime("00", "00")
                .SetEndTime("06", "00")
                .ClickCreateBooking()
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(user1FirstName + " " + user1LastName + ", CPSRT 4835, 47.00 hrs, 01/01/2022 (Active) is not available at this time.", 1)
                .ClickDismissButton_DynamicDialogue();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6030")]
        [Description("Step(s) 10 to 11 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4835_UITestCases004()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-4835 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 4835", "99992", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            DateTime mondayDate = thisWeekMonday;
            if (DateTime.Now.Date > thisWeekMonday)
                mondayDate = thisWeekMonday.AddDays(7);

            dbHelper.userWorkSchedule.CreateUserWorkSchedule(systemUser1Id, _teamId, _recurrencePattern_Every1WeekMondayId, _systemUser1EmploymentContractId, _availabilityTypeId, mondayDate,
                null, new TimeSpan(9, 0, 0), new TimeSpan(21, 0, 0), 1);


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
                .SelectBookingType("BTC ACC-4835 1")
                .SetStartDay("Monday")
                .SetEndDay("Monday")
                .SetStartTime("00", "00")
                .SetEndTime("06", "00")
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
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking()
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(user1FirstName + " " + user1LastName + ", CPSRT 4835, 47.00 hrs, 01/01/2022 (Active) is not available at this time.", 1)
                .ClickDismissButton_DynamicDialogue();

            #endregion

            #region Step 11

            createScheduleBookingPopup
                .SetStartTime("09", "00")
                .SetEndTime("09", "15")
                .ClickCreateBooking();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(4000);

            //Validate that the 1 Diary Bookings is created
            var diaryBookings = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, diaryBookings.Count);
            var bookingID = diaryBookings[0];

            providerSchedulePage
                .ClickDiaryBooking(bookingID);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .SetStartTime("00", "00")
                .SetEndTime("06", "00")
                .ClickCreateBooking()
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(user1FirstName + " " + user1LastName + ", CPSRT 4835, 47.00 hrs, 01/01/2022 (Active) is not available at this time.", 1)
                .ClickDismissButton_DynamicDialogue();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6031")]
        [Description("Step(s) 12 to 13 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4835_UITestCases005()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-4835 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 4835", "99992", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            DateTime mondayDate = thisWeekMonday;
            if (DateTime.Now.Date > thisWeekMonday)
                mondayDate = thisWeekMonday.AddDays(7);

            dbHelper.userWorkSchedule.CreateUserWorkSchedule(systemUser1Id, _teamId, _recurrencePattern_Every1WeekMondayId, _systemUser1EmploymentContractId, _availabilityTypeId, mondayDate,
                null, new TimeSpan(9, 0, 0), new TimeSpan(21, 0, 0), 1);


            #endregion

            #region Care Provider Scheduling Setup

            var cPSchedulingSetupId = dbHelper.cPSchedulingSetup.GetAllActiveRecords().FirstOrDefault();
            dbHelper.cPSchedulingSetup.UpdateCheckStaffAvailability(cPSchedulingSetupId, 1); //No Check

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
                .SelectBookingType("BTC ACC-4835 1")
                .SetStartDay("Monday")
                .SetEndDay("Monday")
                .SetStartTime("00", "00")
                .SetEndTime("06", "00")
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
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(4000);

            //Validate that the 1 Diary Bookings is created
            var diaryBookings = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, diaryBookings.Count);
            var bookingID = diaryBookings[0];

            #endregion

            #region Step 13

            providerSchedulePage
                .ClickDiaryBooking(bookingID);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateStartTime("00:00")
                .ValidateEndTime("06:00")
                .SetStartTime("00", "15")
                .SetEndTime("06", "15")
                .ClickCreateBooking();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(4000);

            providerSchedulePage
                .ClickDiaryBooking(bookingID);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateStartTime("00:15")
                .ValidateEndTime("06:15");

            #endregion

            #region Care Provider Scheduling Setup

            dbHelper.cPSchedulingSetup.UpdateCheckStaffAvailability(cPSchedulingSetupId, 4); //Check and Offer Create

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6033")]
        [Description("Step(s) 14 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4835_UITestCases007()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-4835 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 4835", "99992", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            DateTime sundayDate = thisWeekMonday.AddDays(6);

            dbHelper.userWorkSchedule.CreateUserWorkSchedule(systemUser1Id, _teamId, _recurrencePattern_Every1WeekSundayId, _systemUser1EmploymentContractId, _availabilityTypeId, sundayDate,
                null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);

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
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC ACC-4835 1")
                .SetStartDay("Sunday")
                .SetStartTime("00", "00")
                .SetEndTime("00", "00")
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
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(4000);

            //Validate that the 1 Diary Bookings is created
            var diaryBookings = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, diaryBookings.Count);
            var bookingID = diaryBookings[0];

            providerSchedulePage
                .ClickDiaryBooking(bookingID);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateStartTime("00:00")
                .ValidateEndTime("00:00")
                .ValidateStartDayText("Sunday")
                .ValidateEndDayText("Sunday");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6034")]
        [Description("Step(s) 15 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4835_UITestCases008()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-4835 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 4835", "99992", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            DateTime sundayDate = thisWeekMonday.AddDays(6);

            dbHelper.userWorkSchedule.CreateUserWorkSchedule(systemUser1Id, _teamId, _recurrencePattern_Every1WeekSundayId, _systemUser1EmploymentContractId, _availabilityTypeId, sundayDate,
                null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);

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
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC ACC-4835 1")
                .SetStartDay("Sunday")
                .SetStartTime("00", "00")
                .SetEndTime("23", "45")
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
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(4000);

            //Validate that the 1 Diary Bookings is created
            var diaryBookings = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, diaryBookings.Count);
            var bookingID = diaryBookings[0];

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickDiaryBooking(bookingID);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateStartTime("00:00")
                .ValidateEndTime("23:45")
                .ValidateStartDayText("Sunday")
                .ValidateEndDayText("Sunday")
                .SetEndTime("00", "00")
                .SetEndDay("Sunday")
                .ClickCreateBooking();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(4000);

            providerSchedulePage
                .ClickDiaryBooking(bookingID);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateStartTime("00:00")
                .ValidateEndTime("00:00")
                .ValidateStartDayText("Sunday")
                .ValidateEndDayText("Sunday");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6035")]
        [Description("Step(s) 16 to 17 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4835_UITestCases009()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-4835 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 4835", "99992", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user2name = "cpsu_2_" + currentTimeString;
            var user2FirstName = "Care Provider";
            var user2LastName = "System User " + currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user3name = "cpsu_3_" + currentTimeString;
            var user3FirstName = "Care Provider";
            var user3LastName = "System User " + currentTimeString;
            var systemUser3Id = commonMethodsDB.CreateSystemUserRecord(user3name, user3FirstName, user3LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser2Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser3Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser2EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser2Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser3EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser3Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0));
            CreateUserWorkSchedule(systemUser2Id, _teamId, _systemUser2EmploymentContractId, _availabilityTypeId, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0));
            CreateUserWorkSchedule(systemUser3Id, _teamId, _systemUser3EmploymentContractId, _availabilityTypeId, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0));

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
                .SelectBookingType("BTC ACC-4835 1")
                .SetStartDay("Sunday")
                .SetStartTime("09", "00")
                .SetEndTime("17", "00")
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
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(4000);

            //Validate that the 1 Diary Bookings is created
            var diaryBookings = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, diaryBookings.Count);
            var bookingID = diaryBookings[0];

            #endregion

            #region Step 17

            providerSchedulePage
                .ClickDiaryBooking(bookingID);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateStartTime("09:00")
                .ValidateEndTime("17:00")
                .SetStartTime("09", "00")
                .SetEndTime("16", "45")
                .ClickCreateBooking();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(4000);

            providerSchedulePage
                .ClickDiaryBooking(bookingID);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateStartTime("09:00")
                .ValidateEndTime("16:45")
                .SetStartTime("09", "00")
                .SetEndTime("17", "00")
                .ClickCreateBooking();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(4000);

            providerSchedulePage
                .ClickDiaryBooking(bookingID);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateStartTime("09:00")
                .ValidateEndTime("17:00");

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-6036")]
        [Description("Step(s) 18 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4835_UITestCases010()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-4835 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 4835", "99992", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user2name = "cpsu_2_" + currentTimeString;
            var user2FirstName = "Care Provider";
            var user2LastName = "System User " + currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user3name = "cpsu_3_" + currentTimeString;
            var user3FirstName = "Care Provider";
            var user3LastName = "System User " + currentTimeString;
            var systemUser3Id = commonMethodsDB.CreateSystemUserRecord(user3name, user3FirstName, user3LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser2Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser3Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser2EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser2Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser3EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser3Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            DateTime sundayDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(6);

            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId, new TimeSpan(9, 0, 0), new TimeSpan(12, 0, 0), sundayDate, _recurrencePattern_Every1WeekSundayId);
            CreateUserWorkSchedule(systemUser2Id, _teamId, _systemUser2EmploymentContractId, _availabilityTypeId, new TimeSpan(15, 0, 0), new TimeSpan(21, 0, 0), sundayDate, _recurrencePattern_Every1WeekSundayId);
            CreateUserWorkSchedule(systemUser3Id, _teamId, _systemUser3EmploymentContractId, _availabilityTypeId, new TimeSpan(15, 0, 0), new TimeSpan(21, 0, 0), sundayDate, _recurrencePattern_Every1WeekSundayId);

            #endregion


            #region Step 18

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
                .SelectBookingType("BTC ACC-4835 1")
                .SetStartDay("Sunday")
                .SetStartTime("15", "00")
                .SetEndTime("21", "00")
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
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking()
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(user1FirstName + " " + user1LastName + ", CPSRT 4835, 47.00 hrs, 01/01/2022 (Active) is not available at this time.", 1)
                .ClickDismissButton_DynamicDialogue();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6037")]
        [Description("Step(s) 19 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4835_UITestCases011()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-4835 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 4835", "99992", null, new DateTime(2020, 1, 1), null);

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

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user2name = "cpsu_2_" + currentTimeString;
            var user2FirstName = "Care Provider";
            var user2LastName = "System User " + currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user3name = "cpsu_3_" + currentTimeString;
            var user3FirstName = "Care Provider";
            var user3LastName = "System User " + currentTimeString;
            var systemUser3Id = commonMethodsDB.CreateSystemUserRecord(user3name, user3FirstName, user3LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser2Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser3Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser2EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser2Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser3EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser3Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            DateTime sundayDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(6);

            //need to change the start and end times so that the availability overlaps, so that we can create a valid schedule booking
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId, new TimeSpan(9, 0, 0), new TimeSpan(16, 0, 0), sundayDate, _recurrencePattern_Every1WeekSundayId);
            CreateUserWorkSchedule(systemUser2Id, _teamId, _systemUser2EmploymentContractId, _availabilityTypeId, new TimeSpan(15, 0, 0), new TimeSpan(21, 0, 0), sundayDate, _recurrencePattern_Every1WeekSundayId);
            CreateUserWorkSchedule(systemUser3Id, _teamId, _systemUser3EmploymentContractId, _availabilityTypeId, new TimeSpan(15, 0, 0), new TimeSpan(21, 0, 0), sundayDate, _recurrencePattern_Every1WeekSundayId);

            #endregion

            #region Booking Schedule

            var cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 7, 7, new TimeSpan(15, 0, 0), new TimeSpan(16, 0, 0), providerId, "Express Book Testing");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingScheduleId, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingScheduleId, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingScheduleId, _systemUser2EmploymentContractId, systemUser2Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingScheduleId, _systemUser3EmploymentContractId, systemUser3Id);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingScheduleId, _personID, _personcontractId, careProviderContractServiceId);

            #endregion


            #region Step 19

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
                .ClickDiaryBooking(cpBookingScheduleId);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .SelectBookingType("BTC ACC-4835 1")
                .SetStartTime("16", "00")
                .SetEndTime("21", "00")
                .ClickCreateBooking()
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue(user1FirstName + " " + user1LastName + ", CPSRT 4835, 47.00 hrs, 01/01/2022 (Active) is not available at this time.", 1)
                .ClickDismissButton_DynamicDialogue();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6038")]
        [Description("Step(s) 20 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4835_UITestCases012()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-4835 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 4835", "99992", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region Recurrence Patterns

            _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();
            var _recurrencePattern_Every2WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 2 week(s) on sunday").First();

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region System User

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user2name = "cpsu_2_" + currentTimeString;
            var user2FirstName = "Care Provider";
            var user2LastName = "System User " + currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user3name = "cpsu_3_" + currentTimeString;
            var user3FirstName = "Care Provider";
            var user3LastName = "System User " + currentTimeString;
            var systemUser3Id = commonMethodsDB.CreateSystemUserRecord(user3name, user3FirstName, user3LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser2Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser3Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser2EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser2Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser3EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser3Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            DateTime sundayDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(6);

            //need to change the start and end times so that the availability overlaps, so that we can create a valid schedule booking
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), sundayDate, _recurrencePattern_Every2WeekSundayId);
            CreateUserWorkSchedule(systemUser2Id, _teamId, _systemUser2EmploymentContractId, _availabilityTypeId, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), sundayDate, _recurrencePattern_Every2WeekSundayId);
            CreateUserWorkSchedule(systemUser3Id, _teamId, _systemUser3EmploymentContractId, _availabilityTypeId, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), sundayDate, _recurrencePattern_Every2WeekSundayId);

            #endregion


            #region Step 20

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
                .SelectBookingType("BTC ACC-4835 1")
                .SetStartDay("Sunday")
                .SetStartTime("09", "00")
                .SetEndTime("17", "00")
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
                .ClickStaffConfirmSelection();

            var targetYear = sundayDate.Year.ToString();
            var targetMonth = sundayDate.ToString("MMMM");
            var targetDay = sundayDate.Day.ToString();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .SelectBookingTakesPlaceEvery("2 weeks")
                .SelectNextDueToTakePlaceDate(targetYear, targetMonth, targetDay);

            createScheduleBookingPopup
                .ClickRosteringTab()
                .ClickCreateBooking();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(4000);

            //Validate that the 1 Diary Bookings is created
            var diaryBookings = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, diaryBookings.Count);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5962

        [TestProperty("JiraIssueID", "ACC-6039")]
        [Description("Step(s) 21 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4835_UITestCases013()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-4835 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 4835", "99992", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region Recurrence Patterns

            _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();
            var _recurrencePattern_Every2WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 2 week(s) on sunday").First();

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region System User

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user2name = "cpsu_2_" + currentTimeString;
            var user2FirstName = "Care Provider";
            var user2LastName = "System User " + currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user3name = "cpsu_3_" + currentTimeString;
            var user3FirstName = "Care Provider";
            var user3LastName = "System User " + currentTimeString;
            var systemUser3Id = commonMethodsDB.CreateSystemUserRecord(user3name, user3FirstName, user3LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser2Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser3Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser2EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser2Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser3EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser3Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            DateTime sundayDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(6);

            //need to change the start and end times so that the availability overlaps, so that we can create a valid schedule booking
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), sundayDate, _recurrencePattern_Every2WeekSundayId);
            CreateUserWorkSchedule(systemUser2Id, _teamId, _systemUser2EmploymentContractId, _availabilityTypeId, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), sundayDate, _recurrencePattern_Every2WeekSundayId);
            CreateUserWorkSchedule(systemUser3Id, _teamId, _systemUser3EmploymentContractId, _availabilityTypeId, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), sundayDate, _recurrencePattern_Every2WeekSundayId);

            #endregion

            #region Booking Schedule

            var cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 7, 7, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), providerId, "Express Book Testing");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingScheduleId, 1);
            dbHelper.cpBookingSchedule.UpdateOccurenceInformation(cpBookingScheduleId, 2, sundayDate, null, null);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingScheduleId, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingScheduleId, _systemUser2EmploymentContractId, systemUser2Id);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingScheduleId, _personID, _personcontractId, careProviderContractServiceId);

            #endregion


            #region Step 20

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
                .ClickDiaryBooking(cpBookingScheduleId);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .ClickOnlyShowAvailableStaff()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUser3EmploymentContractId)
                .ClickStaffConfirmSelection();

            var targetYear = sundayDate.Year.ToString();
            var targetMonth = sundayDate.ToString("MMMM");
            var targetDay = sundayDate.Day.ToString();

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(4000);

            //Validate that the 3 staff members are linked with the booking
            var allrecords = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleId(cpBookingScheduleId);
            Assert.AreEqual(3, allrecords.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6040")]
        [Description("Step(s) 22 to 24 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4835_UITestCases014()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-4835 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 4835", "99992", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region Recurrence Patterns

            _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();
            var _recurrencePattern_Every2WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 2 week(s) on sunday").First();

            #endregion

            #region Availability Type

            var _availabilityTypeId = commonMethodsDB.CreateAvailabilityType("ACC-5962 SB Invalid", 21, false, _teamId, 1, 2, true);

            #endregion

            #region System User

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            DateTime sundayDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(6);

            //need to change the start and end times so that the availability overlaps, so that we can create a valid schedule booking
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), sundayDate, _recurrencePattern_Every1WeekSundayId);

            #endregion

            #region Step 22

            //This is already covered by all other tests that create schedule bookings

            #endregion

            #region Step 23

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
                .SelectBookingType("BTC ACC-4835 1")
                .SetStartDay("Sunday")
                .SetStartTime("09", "00")
                .SetEndTime("17", "00")
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
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking()
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("Care Provider System User " + currentTimeString + ", CPSRT 4835, 47.00 hrs, 01/01/2022 (Active) is not available at this time.")
                .ClickDismissButton_DynamicDialogue();


            #endregion

            #region Step 24

            //this step is already covered by other tests

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-6041")]
        [Description("Step(s) 25 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4835_UITestCases015()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-4835 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 4835", "99992", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region Recurrence Patterns

            _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();

            #endregion

            #region Availability Type

            var _availabilityType1Id = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();
            var _availabilityType2Id = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Hourly/Overtime").First();

            #endregion

            #region System User

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            DateTime sundayDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(6);

            //need to change the start and end times so that the availability overlaps, so that we can create a valid schedule booking
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityType1Id, new TimeSpan(1, 0, 0), new TimeSpan(5, 0, 0), sundayDate, _recurrencePattern_Every1WeekSundayId);
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityType1Id, new TimeSpan(5, 0, 0), new TimeSpan(8, 0, 0), sundayDate, _recurrencePattern_Every1WeekSundayId);

            #endregion


            #region Step 25

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
                .SelectBookingType("BTC ACC-4835 1")
                .SetStartDay("Sunday")
                .SetStartTime("04", "00")
                .SetEndTime("06", "00")
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
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(4000);

            //Validate that the 1 Diary Bookings is created
            var diaryBookings = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, diaryBookings.Count);
            var bookingID = diaryBookings[0];

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6042")]
        [Description("Step(s) 26 to 27 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4835_UITestCases016()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Care Provider Scheduling Setup

            var cPSchedulingSetupId = dbHelper.cPSchedulingSetup.GetAllActiveRecords().FirstOrDefault();
            dbHelper.cPSchedulingSetup.UpdateCheckStaffAvailability(cPSchedulingSetupId, 5); //Warn and Offer Create

            #endregion

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-4835 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 4835", "99992", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));
            var _employmentContractTypeid2 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Hourly", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region Recurrence Patterns

            _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();

            #endregion

            #region Availability Type

            var _availabilityType1Id = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();
            var _availabilityType2Id = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Hourly/Overtime").First();

            #endregion

            #region System User

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContract1Id = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1, 6, 0, 0), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser1EmploymentContract2Id = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1, 6, 0, 0), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid2, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContract1Id, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContract2Id, _bookingType2);

            #endregion

            #region User Work Schedule

            DateTime sundayDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(6);

            //need to change the start and end times so that the availability overlaps, so that we can create a valid schedule booking
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContract1Id, _availabilityType1Id, new TimeSpan(1, 0, 0), new TimeSpan(5, 0, 0), sundayDate, _recurrencePattern_Every1WeekSundayId);
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContract2Id, _availabilityType1Id, new TimeSpan(5, 0, 0), new TimeSpan(8, 0, 0), sundayDate, _recurrencePattern_Every1WeekSundayId);

            #endregion


            #region Step 26

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
                .SelectBookingType("BTC ACC-4835 1")
                .SetStartDay("Sunday")
                .SetStartTime("06", "00")
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
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickOnlyShowAvailableStaff()
                .ClickStaffRecordCellText(_systemUser1EmploymentContract1Id)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking()

                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessageLine("Employee Care Provider System User " + currentTimeString + " has more than one contract; Care Provider System User " + currentTimeString + ", 47.00 hrs, and Care Provider System User " + currentTimeString + ". 47.00 hrs, Are you sure you are allocating the booking to the correct employment contract in line with the employees pay arrangement?", 1)
                .ValidateWarningAlertMessageLine("Warning - Care Provider System User " + currentTimeString + ", CPSRT 4835, 47.00 hrs, 01/01/2022 (Active) is not available at this time.", 2)
                .ClickDismissButton_WarningDialogue();

            #endregion

            #region Step 27

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .RemoveStaffFromSelectedStaffField(_systemUser1EmploymentContract1Id)
                .ClickManageStaffButton();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickOnlyShowAvailableStaff()
                .ClickStaffRecordCellText(_systemUser1EmploymentContract2Id)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking()

                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("Employee Care Provider System User " + currentTimeString + " has more than one contract; Care Provider System User " + currentTimeString + ", 47.00 hrs, and Care Provider System User " + currentTimeString + ". 47.00 hrs, Are you sure you are allocating the booking to the correct employment contract in line with the employees pay arrangement?")
                .ClickConfirmButton_WarningDialogue();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad();

            System.Threading.Thread.Sleep(4000);

            //Validate that the 1 Diary Bookings is created
            var diaryBookings = dbHelper.cpBookingSchedule.GetByLocationId(providerId);
            Assert.AreEqual(1, diaryBookings.Count);
            var bookingID = diaryBookings[0];

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6043")]
        [Description("Step(s) 28 to 33 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ProviderSchedule_ACC_4835_UITestCases017()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-4835 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 4835", "99992", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region Recurrence Patterns

            _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();
            _recurrencePattern_Every1WeekMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on monday").First();

            #endregion

            #region Availability Type

            var _availabilityType1Id = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();
            var _availabilityType2Id = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Hourly/Overtime").First();

            #endregion

            #region System User

            //set the user as roostered user and use a persona to link it to the user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContract1Id = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContract1Id, _bookingType2);

            #endregion

            #region User Work Schedule

            DateTime sundayDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(6);
            DateTime mondayDate = sundayDate.AddDays(-1);

            //need to change the start and end times so that the availability overlaps, so that we can create a valid schedule booking
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContract1Id, _availabilityType1Id, new TimeSpan(9, 0, 0), new TimeSpan(13, 0, 0), sundayDate, _recurrencePattern_Every1WeekSundayId);
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContract1Id, _availabilityType1Id, new TimeSpan(9, 0, 0), new TimeSpan(13, 0, 0), mondayDate, _recurrencePattern_Every1WeekMondayId);

            #endregion


            #region Step 28

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
                .SelectBookingType("BTC ACC-4835 1")
                .SetStartDay("Sunday")
                .SetStartTime("10", "00")
                .SetEndTime("11", "00")
                .SetEndDay("Tuesday")
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
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickOnlyShowAvailableStaff()
                .ClickStaffRecordCellText(_systemUser1EmploymentContract1Id)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking()
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("Care Provider System User " + currentTimeString + ", CPSRT 4835, 47.00 hrs, 01/01/2022 (Active) is not available at this time.")
                .ClickDismissButton_DynamicDialogue();

            #endregion

            #region Step 29 to 32

            //this steps will be ignored as they dont have enough / valid information in Jira

            #endregion

            #region Step 33

            //Step 33 seems to be repeated, previous steps cover this scenario

            #endregion

        }

        #endregion
    }
}

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
    public class ExpressBook_JB_UITestCases : FunctionalTest
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

        internal Guid CreateBookingSchedule(Guid teamId, Guid _bookingTypeID, int StartDayOfWeekId, int EndDayOfWeekId, TimeSpan StartTime, TimeSpan EndTime, Guid providerId, List<UserEmploymentContractInfo> employmentContractInfo, Guid _personID, Guid _personcontractId, Guid careProviderContractServiceId)
        {
            var cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(teamId, _bookingTypeID, 1, StartDayOfWeekId, EndDayOfWeekId, StartTime, EndTime, providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingScheduleId, 1);

            foreach (var item in employmentContractInfo)
                dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(teamId, cpBookingScheduleId, item.SystemUserEmploymentContractId, item.SystemUserId);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(teamId, cpBookingScheduleId, _personID, _personcontractId, careProviderContractServiceId);

            return cpBookingScheduleId;
        }

        #endregion

        #region Private Classes

        internal class UserEmploymentContractInfo
        {
            public UserEmploymentContractInfo(Guid _systemUserId, Guid _systemUserEmploymentContractId)
            {
                SystemUserId = _systemUserId;
                SystemUserEmploymentContractId = _systemUserEmploymentContractId;
            }

            public Guid SystemUserId { get; set; }
            public Guid SystemUserEmploymentContractId { get; set; }
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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("Care Providers EB");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                teamName = "Care Providers EB";
                _teamId = commonMethodsDB.CreateTeam(teamName, null, _businessUnitId, "90400", "CareProvidersEB@careworkstempmail.com", teamName, "020 123456");

                #endregion

                #region Create default system user

                _loginUser_Username = "ProviderSchedule_User_85";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUser_Username, "ProviderSchedule", "User_85", "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

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

        #region https://advancedcsg.atlassian.net/browse/ACC-6072

        [TestProperty("JiraIssueID", "ACC-6419")]
        [Description("Step(s) 1 to 2 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ExpressBook_ACC_6074_UITestCases001()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-6074 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType2, 2).FirstOrDefault();
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 3); //Prevent

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

            var firstName = "Pedro";
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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6074", "99910", null, new DateTime(2020, 1, 1), null);

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

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(9, 0, 0), new TimeSpan(12, 0, 0), providerId, "Express Book Testing");
            var cpBookingSchedule2Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(12, 0, 0), new TimeSpan(18, 0, 0), providerId, "Express Book Testing");
            var cpBookingSchedule3Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(18, 0, 0), new TimeSpan(23, 0, 0), providerId, "Express Book Testing");

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

            expressBookingCriteriaRecordPage
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
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

            //validate that the 3 Diary Bookings are created
            var diaryBookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(3, diaryBookings.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6420")]
        [Description("Step(s) 3 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ExpressBook_ACC_6074_UITestCases002()
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

            var _bookingType1A = commonMethodsDB.CreateCPBookingType("BTC ACC-6074 1A", 1, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1); //Booking (to location)

            var _bookingType1B = commonMethodsDB.CreateCPBookingType("BTC ACC-6074 1B", 1, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1); //Booking (to location)

            var _bookingType2A = commonMethodsDB.CreateCPBookingType("BTC ACC-6074 2A", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1); //Booking (to internal care activity)

            var _bookingType3A = commonMethodsDB.CreateCPBookingType("BTC ACC-6074 3A", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1); //Booking (to external care activity)

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType1A, 1).FirstOrDefault(); //Booking (to location)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType1A, 2).FirstOrDefault(); //Booking (to internal care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType1A, 3).FirstOrDefault(); //Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 2); //Warn Only



            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType1B, 1).FirstOrDefault(); //Booking (to location)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 3); //Prevent

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType1B, 2).FirstOrDefault(); //Booking (to internal care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType1B, 3).FirstOrDefault(); //Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 2); //Warn Only



            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType2A, 1).FirstOrDefault(); //Booking (to location)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 2); //Warn Only

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType2A, 3).FirstOrDefault(); //Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow



            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType3A, 2).FirstOrDefault(); //Booking (to internal care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType3A, 1).FirstOrDefault(); //Booking (to location)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 3); //Prevent

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType1A, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType1B, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType2A, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType3A, false);

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

            var careProviderServiceMapping1Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType1A, null, "");
            var careProviderServiceMapping2Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType1B, null, "");
            var careProviderServiceMapping3Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType2A, null, "");
            var careProviderServiceMapping4Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType3A, null, "");

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

            var careProviderContractService1Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType1A, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService2Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType1B, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService3Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType2A, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService4Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType3A, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService1Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService2Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService3Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService4Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);


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

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractService1Id, todayDate, 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractService2Id, todayDate, 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractService3Id, todayDate, 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractService4Id, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6074", "99910", null, new DateTime(2020, 1, 1), null);

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

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType1A);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType1B);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2A);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType3A);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1A, 1, 1, 1, new TimeSpan(9, 0, 0), new TimeSpan(15, 0, 0), providerId, "Express Book Testing");
            var cpBookingSchedule2Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1B, 1, 1, 1, new TimeSpan(11, 0, 0), new TimeSpan(17, 0, 0), providerId, "Express Book Testing");
            var cpBookingSchedule3Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2A, 1, 1, 1, new TimeSpan(10, 0, 0), new TimeSpan(14, 0, 0), providerId, "Express Book Testing");
            var cpBookingSchedule4Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType3A, 1, 1, 1, new TimeSpan(8, 0, 0), new TimeSpan(17, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule2Id, 1);
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule3Id, 1);
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule4Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule2Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule3Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule4Id, _systemUser1EmploymentContractId, systemUser1Id);

            //dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontractId, careProviderContractService1Id);
            //dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule2Id, _personID, _personcontractId, careProviderContractService2Id);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule3Id, _personID, _personcontractId, careProviderContractService3Id);
            //dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule4Id, _personID, _personcontractId, careProviderContractService4Id);

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
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
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

            //Validate that we have 3 errors logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(3, expressBookingResults.Count);

            //validate that the 4 Diary Bookings are created
            var diaryBookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(4, diaryBookings.Count);



            //8am booking should have staff
            var diaryBookingId = dbHelper.cPBookingDiary.GetByBookingScheduleAndPlannedStartTime(cpBookingSchedule4Id).FirstOrDefault();
            var bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookingId);
            Assert.AreEqual(1, bookingDiaryStaffRecords.Count);
            var fields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), fields["systemuseremploymentcontractid"].ToString());

            //9am booking should have NO staff
            diaryBookingId = dbHelper.cPBookingDiary.GetByBookingScheduleAndPlannedStartTime(cpBookingSchedule1Id).FirstOrDefault();
            bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookingId);
            Assert.AreEqual(1, bookingDiaryStaffRecords.Count);
            fields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.IsFalse(fields.ContainsKey("systemuseremploymentcontractid"));

            //10am booking should have NO staff
            diaryBookingId = dbHelper.cPBookingDiary.GetByBookingScheduleAndPlannedStartTime(cpBookingSchedule3Id).FirstOrDefault();
            bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookingId);
            Assert.AreEqual(1, bookingDiaryStaffRecords.Count);
            fields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.IsFalse(fields.ContainsKey("systemuseremploymentcontractid"));

            //11am booking should have NO staff
            diaryBookingId = dbHelper.cPBookingDiary.GetByBookingScheduleAndPlannedStartTime(cpBookingSchedule2Id).FirstOrDefault();
            bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookingId);
            Assert.AreEqual(1, bookingDiaryStaffRecords.Count);
            fields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.IsFalse(fields.ContainsKey("systemuseremploymentcontractid"));


            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6421")]
        [Description("Step(s) 4 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ExpressBook_ACC_6074_UITestCases003()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-6074 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType2, 2).FirstOrDefault();
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 3); //Prevent

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

            var firstName = "Pedro";
            var lastName = currentTimeString;
            var _person1ID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            firstName = "Xico";
            lastName = currentTimeString;
            var _person2ID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontract1Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _person1ID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);
            var _personcontract2Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _person2ID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontract1Id, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontract2Id, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6074", "99910", null, new DateTime(2020, 1, 1), null);

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

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _person1ID, _personcontract1Id, careProviderContractServiceId);

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
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
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

            //validate that the 1 Diary Bookings are created
            var diaryBookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(1, diaryBookings.Count);

            //9am booking should have staff
            var diaryBookingId = dbHelper.cPBookingDiary.GetByBookingScheduleAndPlannedStartTime(cpBookingSchedule1Id).FirstOrDefault();
            var bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookingId);
            Assert.AreEqual(1, bookingDiaryStaffRecords.Count);
            var fields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), fields["systemuseremploymentcontractid"].ToString());


            #region Booking Schedule

            var cpBookingSchedule2Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(5, 0, 0), new TimeSpan(20, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule2Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule2Id, _systemUser1EmploymentContractId, systemUser1Id);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule2Id, _person2ID, _personcontract2Id, careProviderContractServiceId);

            #endregion


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
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
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
            var expressBookingCriteria2Id = expressBookings.Where(x => x != expressBookingCriteriaId).First();
            var expressBookingCriteriaStatus2Id = (int)(dbHelper.cpExpressBookingCriteria.GetByID(expressBookingCriteria2Id, "statusid")["statusid"]);
            Assert.AreEqual(3, expressBookingCriteriaStatus2Id);

            //Validate that we have 2 error logged in the results
            expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteria2Id);
            Assert.AreEqual(2, expressBookingResults.Count);
            var messages = new List<string>();
            messages.Add(dbHelper.cpExpressBookingResult.GetById(expressBookingResults[0], "exceptionmessage")["exceptionmessage"].ToString());
            messages.Add(dbHelper.cpExpressBookingResult.GetById(expressBookingResults[1], "exceptionmessage")["exceptionmessage"].ToString());
            Assert.IsTrue(messages.Contains("Booking already express booked"));
            Assert.IsTrue(messages.Contains("Care Provider System User 1 " + currentTimeString + " already has a diary booking at this time."));

            //validate that the 0 Diary Bookings are created
            diaryBookings = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule2Id);
            Assert.AreEqual(1, diaryBookings.Count);

            //5am booking should have NO staff linked to it
            var diaryBooking2Id = dbHelper.cPBookingDiary.GetByBookingScheduleAndPlannedStartTime(cpBookingSchedule2Id).FirstOrDefault();
            bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBooking2Id);
            Assert.AreEqual(1, bookingDiaryStaffRecords.Count);
            fields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.IsFalse(fields.ContainsKey("systemuseremploymentcontractid"));

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6074

        [TestProperty("JiraIssueID", "ACC-6422")]
        [Description("Step(s) 5 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ExpressBook_ACC_6074_UITestCases004()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-6074 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType2, 2).FirstOrDefault();
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 2); //Warn Only

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

            var firstName = "Pedro";
            var lastName = currentTimeString;
            var _person1ID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            firstName = "Xico";
            lastName = currentTimeString;
            var _person2ID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontract1Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _person1ID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);
            var _personcontract2Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _person2ID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontract1Id, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontract2Id, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6074", "99910", null, new DateTime(2020, 1, 1), null);

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

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(2, 0, 0), new TimeSpan(9, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _person1ID, _personcontract1Id, careProviderContractServiceId);

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
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
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

            //validate that the 1 Diary Bookings are created
            var diaryBookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(1, diaryBookings.Count);

            //2am booking should have staff
            var diaryBookingId = dbHelper.cPBookingDiary.GetByBookingScheduleAndPlannedStartTime(cpBookingSchedule1Id).FirstOrDefault();
            var bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookingId);
            Assert.AreEqual(1, bookingDiaryStaffRecords.Count);
            var fields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), fields["systemuseremploymentcontractid"].ToString());


            #region Booking Schedule

            var cpBookingSchedule2Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(18, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule2Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule2Id, _systemUser1EmploymentContractId, systemUser1Id);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule2Id, _person2ID, _personcontract2Id, careProviderContractServiceId);

            #endregion


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
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
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
            var expressBookingCriteria2Id = expressBookings.Where(x => x != expressBookingCriteriaId).First();
            var expressBookingCriteriaStatus2Id = (int)(dbHelper.cpExpressBookingCriteria.GetByID(expressBookingCriteria2Id, "statusid")["statusid"]);
            Assert.AreEqual(3, expressBookingCriteriaStatus2Id);

            //Validate that we have 1 error logged in the results
            expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteria2Id);
            Assert.AreEqual(1, expressBookingResults.Count);
            var messages = new List<string>();
            messages.Add(dbHelper.cpExpressBookingResult.GetById(expressBookingResults[0], "exceptionmessage")["exceptionmessage"].ToString());
            Assert.IsTrue(messages.Contains("Booking already express booked"));

            //validate that the 1 Diary Bookings is created
            diaryBookings = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule2Id);
            Assert.AreEqual(1, diaryBookings.Count);

            //7am booking should have NO staff linked to it
            var diaryBooking2Id = dbHelper.cPBookingDiary.GetByBookingScheduleAndPlannedStartTime(cpBookingSchedule2Id).FirstOrDefault();
            bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBooking2Id);
            Assert.AreEqual(1, bookingDiaryStaffRecords.Count);
            fields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), fields["systemuseremploymentcontractid"].ToString());

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6423")]
        [Description("Step(s) 6 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ExpressBook_ACC_6074_UITestCases005()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-6074 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType2, 2).FirstOrDefault();
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

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

            var firstName = "Pedro";
            var lastName = currentTimeString;
            var _person1ID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            firstName = "Xico";
            lastName = currentTimeString;
            var _person2ID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontract1Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _person1ID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);
            var _personcontract2Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _person2ID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontract1Id, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontract2Id, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6074", "99910", null, new DateTime(2020, 1, 1), null);

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

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _person1ID, _personcontract1Id, careProviderContractServiceId);

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
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
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

            //validate that the 1 Diary Bookings are created
            var diaryBookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(1, diaryBookings.Count);

            //2am booking should have staff
            var diaryBookingId = dbHelper.cPBookingDiary.GetByBookingScheduleAndPlannedStartTime(cpBookingSchedule1Id).FirstOrDefault();
            var bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookingId);
            Assert.AreEqual(1, bookingDiaryStaffRecords.Count);
            var fields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), fields["systemuseremploymentcontractid"].ToString());




            #region Booking Schedule

            var cpBookingSchedule2Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(13, 0, 0), new TimeSpan(19, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule2Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule2Id, _systemUser1EmploymentContractId, systemUser1Id);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule2Id, _person2ID, _personcontract2Id, careProviderContractServiceId);

            #endregion


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
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
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
            var expressBookingCriteria2Id = expressBookings.Where(x => x != expressBookingCriteriaId).First();
            var expressBookingCriteriaStatus2Id = (int)(dbHelper.cpExpressBookingCriteria.GetByID(expressBookingCriteria2Id, "statusid")["statusid"]);
            Assert.AreEqual(3, expressBookingCriteriaStatus2Id);

            //Validate that we have 1 error logged in the results
            expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteria2Id);
            Assert.AreEqual(1, expressBookingResults.Count);
            var messages = new List<string>();
            messages.Add(dbHelper.cpExpressBookingResult.GetById(expressBookingResults[0], "exceptionmessage")["exceptionmessage"].ToString());
            Assert.IsTrue(messages.Contains("Booking already express booked"));

            //validate that the 1 Diary Bookings is created
            diaryBookings = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule2Id);
            Assert.AreEqual(1, diaryBookings.Count);

            //7am booking should have NO staff linked to it
            var diaryBooking2Id = dbHelper.cPBookingDiary.GetByBookingScheduleAndPlannedStartTime(cpBookingSchedule2Id).FirstOrDefault();
            bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBooking2Id);
            Assert.AreEqual(1, bookingDiaryStaffRecords.Count);
            fields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), fields["systemuseremploymentcontractid"].ToString());

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-6424")]
        [Description("Step(s) 7 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ExpressBook_ACC_6074_UITestCases006()
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

            var _bookingType1C = commonMethodsDB.CreateCPBookingType("BTC ACC-6074 1C", 1, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1); //Booking (to location)

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType1C, 1).FirstOrDefault();
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 3); //Prevent

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType1C, false);

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

            var careProviderServiceMapping1Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType1C, null, "");

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

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType1C, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);


            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Pedro";
            var lastName = currentTimeString;
            var _person1ID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            firstName = "Xico";
            lastName = currentTimeString;
            var _person2ID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontract1Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _person1ID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);
            var _personcontract2Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _person2ID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontract1Id, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontract2Id, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6074", "99910", null, new DateTime(2020, 1, 1), null);

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

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType1C);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Diary Booking

            var nextWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);
            var cpBookingDiaryId = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType1C, providerId, nextWeekMonday, new TimeSpan(2, 0, 0), nextWeekMonday, new TimeSpan(9, 0, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", cpBookingDiaryId, _systemUser1EmploymentContractId, systemUser1Id);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1C, 1, 1, 1, new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

            #endregion


            #region Step 7

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
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
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

            //Validate that we have 1 error logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResults.Count);
            var messages = new List<string>();
            messages.Add(dbHelper.cpExpressBookingResult.GetById(expressBookingResults[0], "exceptionmessage")["exceptionmessage"].ToString());
            Assert.IsTrue(messages.Contains("Care Provider System User 1 " + currentTimeString + " already has a diary booking at this time."));

            //validate that the 0 Diary Bookings are created for the schedule booking record 
            var diaryBookings = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            Assert.AreEqual(1, diaryBookings.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6425")]
        [Description("Step(s) 8 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ExpressBook_ACC_6074_UITestCases007()
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

            var _bookingType2B = commonMethodsDB.CreateCPBookingType("BTC ACC-6074 2B", 1, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1); //Booking (to location)

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType2B, 2).FirstOrDefault();
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 3); //Prevent

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType2B, false);

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

            var careProviderServiceMapping1Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType2B, null, "");

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

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType2B, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);


            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Pedro";
            var lastName = currentTimeString;
            var _person1ID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            firstName = "Xico";
            lastName = currentTimeString;
            var _person2ID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontract1Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _person1ID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);
            var _personcontract2Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _person2ID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontract1Id, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontract2Id, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6074", "99910", null, new DateTime(2020, 1, 1), null);

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

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2B);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Diary Booking

            var nextWeekTuesday = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(8);
            var cpBookingDiaryId = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType2B, providerId, nextWeekTuesday, new TimeSpan(2, 0, 0), nextWeekTuesday, new TimeSpan(9, 0, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", cpBookingDiaryId, _systemUser1EmploymentContractId, systemUser1Id);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2B, 1, 1, 3, new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

            #endregion


            #region Step 8

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
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
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

            //Validate that we have 1 error logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResults.Count);
            var messages = new List<string>();
            messages.Add(dbHelper.cpExpressBookingResult.GetById(expressBookingResults[0], "exceptionmessage")["exceptionmessage"].ToString());
            Assert.IsTrue(messages.Contains("Care Provider System User 1 " + currentTimeString + " already has a diary booking at this time."));

            //validate that the 0 Diary Bookings are created for the schedule booking record 
            var diaryBookings = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            Assert.AreEqual(1, diaryBookings.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6426")]
        [Description("Step(s) 9 to 10 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ExpressBook_ACC_6074_UITestCases008()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var provider1Name = "Provider A " + currentTimeString;
            var addressType = 10; //Home
            var provider1Id = commonMethodsDB.CreateProvider(provider1Name, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var provider2Name = "Provider B " + currentTimeString;
            var provider2Id = commonMethodsDB.CreateProvider(provider2Name, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2B = commonMethodsDB.CreateCPBookingType("BTC ACC-6074 2B", 1, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1); //Booking (to location)

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType2B, 2).FirstOrDefault();
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 3); //Prevent

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, provider1Id, _bookingType2B, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, provider2Id, _bookingType2B, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1Id, funderProviderID);

            var contractScheme2Name = "CPCS_B_" + currentTimeString;
            var contractScheme2Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme2Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme2Name, new DateTime(2020, 1, 1), contractScheme2Code, provider2Id, funderProviderID);


            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, true);

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMapping1Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType2B, null, "");

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

            dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme2Id, _careProviderBatchGroupingId,
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

            var careProviderContractService1Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", provider1Id, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType2B, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService2Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", provider2Id, funderProviderID, careProviderContractScheme2Id, careProviderService1Id, null, _bookingType2B, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService1Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService2Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);


            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Pedro";
            var lastName = currentTimeString;
            var _person1ID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontract1Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _person1ID, _defaultLoginUserID, provider1Id, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);
            var _personcontract2Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _person1ID, _defaultLoginUserID, provider2Id, careProviderContractScheme2Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontract1Id, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractService1Id, todayDate, 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontract2Id, _teamId, careProviderContractScheme2Id, careProviderService1Id, careProviderContractService2Id, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6074", "99910", null, new DateTime(2020, 1, 1), null);

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

            ////set the user as roostered user and use a persona to link it to the user
            //var user2name = "cpsu_1_" + currentTimeString;
            //var user2FirstName = "Care Provider";
            //var user2LastName = "System User 1 " + currentTimeString;
            //var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            //dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser2Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            //var _systemUser2EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser2Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2B);
            //dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingType2B);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2B, 1, 1, 1, new TimeSpan(1, 0, 0), new TimeSpan(10, 0, 0), provider1Id, "Express Book Testing");
            var cpBookingSchedule2Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2B, 1, 1, 1, new TimeSpan(9, 0, 0), new TimeSpan(16, 0, 0), provider1Id, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule2Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule2Id, _systemUser1EmploymentContractId, systemUser1Id);

            #endregion


            #region Step 9

            //step 9 is included in step 10 process (I have checked this one with Hari)

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

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(provider1Name, provider1Id);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickSaveAndCloseButton();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickRefreshButton();

            var expressBookings = dbHelper.cpExpressBookingCriteria.GetByRegardingID(provider1Id);
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

            //Validate that we have 1 error logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResults.Count);
            var messages = new List<string>();
            messages.Add(dbHelper.cpExpressBookingResult.GetById(expressBookingResults[0], "exceptionmessage")["exceptionmessage"].ToString());
            Assert.IsTrue(messages.Contains("Care Provider System User 1 " + currentTimeString + " already has a diary booking at this time."));

            //validate that the 1 Diary Bookings are created for the schedule booking record 
            var diaryBookings = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            Assert.AreEqual(1, diaryBookings.Count);



            #region Booking Schedule

            var cpBookingSchedule3Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2B, 1, 1, 1, new TimeSpan(1, 0, 0), new TimeSpan(10, 0, 0), provider2Id, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule3Id, _systemUser1EmploymentContractId, systemUser1Id);

            #endregion


            expressBookingCriteriaPage
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(provider2Name, provider2Id);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickSaveAndCloseButton();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickRefreshButton();

            expressBookings = dbHelper.cpExpressBookingCriteria.GetByRegardingID(provider2Id);
            Assert.AreEqual(1, expressBookings.Count);

            //get the schedule job id
            scheduleJobId = dbHelper.scheduledJob.GetByPartialName("Provider B " + currentTimeString).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            //validate that the Express Booking Criteria status changes to Succeeded
            expressBookingCriteriaId = expressBookings.First();
            expressBookingCriteriaStatusId = (int)(dbHelper.cpExpressBookingCriteria.GetByID(expressBookingCriteriaId, "statusid")["statusid"]);
            Assert.AreEqual(3, expressBookingCriteriaStatusId);

            //Validate that we have 1 error logged in the results
            expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResults.Count);
            messages = new List<string>();
            messages.Add(dbHelper.cpExpressBookingResult.GetById(expressBookingResults[0], "exceptionmessage")["exceptionmessage"].ToString());
            Assert.IsTrue(messages.Contains("Care Provider System User 1 " + currentTimeString + " already has a diary booking at this time."));

            //validate that the 1 Diary Bookings are created for the schedule booking record (with dealocated staff)
            diaryBookings = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule3Id);
            Assert.AreEqual(1, diaryBookings.Count);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6308

        [TestProperty("JiraIssueID", "ACC-6427")]
        [Description("Step(s) 1 to 4 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "System Users")]
        public void ExpressBook_ACC_6250_UITestCases001()
        {
            #region System User

            //Create the system user
            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User" + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion


            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(user1name)
                .ClickSearchButton()
                .OpenRecord(systemUser1Id);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad();

            #endregion

            #region Step 3 and 4

            systemUserRecordPage
                .NavigateToDetailsPage()
                .ValidateMaximumWorkingHoursErrorLabelVisible(false);

            systemUserRecordPage
                .InsertMaximumWorkingHours("-1")
                .ValidateMaximumWorkingHoursErrorLabelVisible(true)
                .ValidateMaximumWorkingHoursErrorLabelText("Please enter a value between 1 and 168.");

            systemUserRecordPage
                .InsertMaximumWorkingHours("169")
                .ValidateMaximumWorkingHoursErrorLabelVisible(true)
                .ValidateMaximumWorkingHoursErrorLabelText("Please enter a value between 1 and 168.");

            systemUserRecordPage
                .InsertMaximumWorkingHours("168")
                .ValidateMaximumWorkingHoursErrorLabelVisible(false)
                .ClickSaveAndCloseButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .OpenRecord(systemUser1Id);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateMaximumWorkingHours("168");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6428")]
        [Description("Step(s) 5 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ExpressBook_ACC_6250_UITestCases002()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-6250 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var firstName = "Pedro";
            var lastName = currentTimeString;
            var _person1ID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            firstName = "Xico";
            lastName = currentTimeString;
            var _person2ID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontract1Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _person1ID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);
            var _personcontract2Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _person2ID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontract1Id, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontract2Id, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6250", "98910", null, new DateTime(2020, 1, 1), null);

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

            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            var user2name = "cpsu_2_" + currentTimeString;
            var user2FirstName = "Care Provider";
            var user2LastName = "System User 2 " + currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser2Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser2Id, 40);

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

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser2Id, _teamId, _systemUser2EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            List<UserEmploymentContractInfo> usersEmploymentInfo = new List<UserEmploymentContractInfo>
            {
                new UserEmploymentContractInfo(systemUser1Id, _systemUser1EmploymentContractId),
                new UserEmploymentContractInfo(systemUser2Id, _systemUser2EmploymentContractId),
            };

            var user1BookingSchedule1Id = CreateBookingSchedule(_teamId, _bookingType2, 1, 1, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), providerId, usersEmploymentInfo, _person1ID, _personcontract1Id, careProviderContractServiceId);
            var user1BookingSchedule2Id = CreateBookingSchedule(_teamId, _bookingType2, 2, 2, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), providerId, usersEmploymentInfo, _person1ID, _personcontract1Id, careProviderContractServiceId);
            var user1BookingSchedule3Id = CreateBookingSchedule(_teamId, _bookingType2, 3, 3, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), providerId, usersEmploymentInfo, _person1ID, _personcontract1Id, careProviderContractServiceId);
            var user1BookingSchedule4Id = CreateBookingSchedule(_teamId, _bookingType2, 4, 4, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), providerId, usersEmploymentInfo, _person1ID, _personcontract1Id, careProviderContractServiceId);
            var user1BookingSchedule5Id = CreateBookingSchedule(_teamId, _bookingType2, 5, 5, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), providerId, usersEmploymentInfo, _person1ID, _personcontract1Id, careProviderContractServiceId);

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
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
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

            //validate that the 5 Diary Bookings are created
            var diaryBookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(5, diaryBookings.Count);



            //Monday booking should have 2 staff members
            var diaryBookingId = dbHelper.cPBookingDiary.GetByBookingScheduleAndPlannedStartTime(user1BookingSchedule1Id).FirstOrDefault();
            var bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookingId);
            Assert.AreEqual(2, bookingDiaryStaffRecords.Count);
            List<string> contracts = new List<string>();
            contracts.Add(dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            contracts.Add(dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[1], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            Assert.IsTrue(contracts.Contains(_systemUser1EmploymentContractId.ToString()));
            Assert.IsTrue(contracts.Contains(_systemUser2EmploymentContractId.ToString()));

            //Tueday booking should have 2 staff members
            diaryBookingId = dbHelper.cPBookingDiary.GetByBookingScheduleAndPlannedStartTime(user1BookingSchedule2Id).FirstOrDefault();
            bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookingId);
            Assert.AreEqual(2, bookingDiaryStaffRecords.Count);
            contracts = new List<string>();
            contracts.Add(dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            contracts.Add(dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[1], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            Assert.IsTrue(contracts.Contains(_systemUser1EmploymentContractId.ToString()));
            Assert.IsTrue(contracts.Contains(_systemUser2EmploymentContractId.ToString()));

            //Wednesday booking should have 2 staff members
            diaryBookingId = dbHelper.cPBookingDiary.GetByBookingScheduleAndPlannedStartTime(user1BookingSchedule3Id).FirstOrDefault();
            bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookingId);
            Assert.AreEqual(2, bookingDiaryStaffRecords.Count);
            contracts = new List<string>();
            contracts.Add(dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            contracts.Add(dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[1], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            Assert.IsTrue(contracts.Contains(_systemUser1EmploymentContractId.ToString()));
            Assert.IsTrue(contracts.Contains(_systemUser2EmploymentContractId.ToString()));

            //Thursday booking should have 2 staff members
            diaryBookingId = dbHelper.cPBookingDiary.GetByBookingScheduleAndPlannedStartTime(user1BookingSchedule4Id).FirstOrDefault();
            bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookingId);
            Assert.AreEqual(2, bookingDiaryStaffRecords.Count);
            contracts = new List<string>();
            contracts.Add(dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            contracts.Add(dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[1], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            Assert.IsTrue(contracts.Contains(_systemUser1EmploymentContractId.ToString()));
            Assert.IsTrue(contracts.Contains(_systemUser2EmploymentContractId.ToString()));

            //Friday booking should have 2 staff members
            diaryBookingId = dbHelper.cPBookingDiary.GetByBookingScheduleAndPlannedStartTime(user1BookingSchedule5Id).FirstOrDefault();
            bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookingId);
            Assert.AreEqual(2, bookingDiaryStaffRecords.Count);
            contracts = new List<string>();
            contracts.Add(dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            contracts.Add(dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[1], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            Assert.IsTrue(contracts.Contains(_systemUser1EmploymentContractId.ToString()));
            Assert.IsTrue(contracts.Contains(_systemUser2EmploymentContractId.ToString()));


            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6429")]
        [Description("Step(s) 6 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ExpressBook_ACC_6250_UITestCases003()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-6250 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var firstName = "Pedro";
            var lastName = currentTimeString;
            var _person1ID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            firstName = "Xico";
            lastName = currentTimeString;
            var _person2ID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontract1Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _person1ID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);
            var _personcontract2Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _person2ID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontract1Id, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontract2Id, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6250", "98910", null, new DateTime(2020, 1, 1), null);

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

            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            var user2name = "cpsu_2_" + currentTimeString;
            var user2FirstName = "Care Provider";
            var user2LastName = "System User 2 " + currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser2Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser2Id, 40);

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

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser2Id, _teamId, _systemUser2EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            List<UserEmploymentContractInfo> usersEmploymentInfo = new List<UserEmploymentContractInfo>
            {
                new UserEmploymentContractInfo(systemUser1Id, _systemUser1EmploymentContractId),
                new UserEmploymentContractInfo(systemUser2Id, _systemUser2EmploymentContractId),
            };

            var user1BookingSchedule1Id = CreateBookingSchedule(_teamId, _bookingType2, 1, 1, new TimeSpan(9, 0, 0), new TimeSpan(17, 30, 0), providerId, usersEmploymentInfo, _person1ID, _personcontract1Id, careProviderContractServiceId);
            var user1BookingSchedule2Id = CreateBookingSchedule(_teamId, _bookingType2, 2, 2, new TimeSpan(9, 0, 0), new TimeSpan(17, 30, 0), providerId, usersEmploymentInfo, _person1ID, _personcontract1Id, careProviderContractServiceId);
            var user1BookingSchedule3Id = CreateBookingSchedule(_teamId, _bookingType2, 3, 3, new TimeSpan(9, 0, 0), new TimeSpan(17, 30, 0), providerId, usersEmploymentInfo, _person1ID, _personcontract1Id, careProviderContractServiceId);
            var user1BookingSchedule4Id = CreateBookingSchedule(_teamId, _bookingType2, 4, 4, new TimeSpan(9, 0, 0), new TimeSpan(17, 30, 0), providerId, usersEmploymentInfo, _person1ID, _personcontract1Id, careProviderContractServiceId);
            var user1BookingSchedule5Id = CreateBookingSchedule(_teamId, _bookingType2, 5, 5, new TimeSpan(9, 0, 0), new TimeSpan(17, 30, 0), providerId, usersEmploymentInfo, _person1ID, _personcontract1Id, careProviderContractServiceId);

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
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
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

            //Validate that we have 1 error logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResults.Count);
            var messages = new List<string>();
            messages.Add(dbHelper.cpExpressBookingResult.GetById(expressBookingResults[0], "exceptionmessage")["exceptionmessage"].ToString());
            Assert.IsTrue(messages.Contains("Care Provider System User 2 " + currentTimeString + " has exceeded their maximum working hours. They have been deallocated."));

            //validate that the 5 Diary Bookings are created
            var diaryBookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(5, diaryBookings.Count);



            //Monday booking should have 2 staff members
            var diaryBookingId = dbHelper.cPBookingDiary.GetByBookingScheduleAndPlannedStartTime(user1BookingSchedule1Id).FirstOrDefault();
            var bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookingId);
            Assert.AreEqual(2, bookingDiaryStaffRecords.Count);
            List<string> contracts = new List<string>();
            contracts.Add(dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            contracts.Add(dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[1], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            Assert.IsTrue(contracts.Contains(_systemUser1EmploymentContractId.ToString()));
            Assert.IsTrue(contracts.Contains(_systemUser2EmploymentContractId.ToString()));

            //Tueday booking should have 2 staff members
            diaryBookingId = dbHelper.cPBookingDiary.GetByBookingScheduleAndPlannedStartTime(user1BookingSchedule2Id).FirstOrDefault();
            bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookingId);
            Assert.AreEqual(2, bookingDiaryStaffRecords.Count);
            contracts = new List<string>();
            contracts.Add(dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            contracts.Add(dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[1], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            Assert.IsTrue(contracts.Contains(_systemUser1EmploymentContractId.ToString()));
            Assert.IsTrue(contracts.Contains(_systemUser2EmploymentContractId.ToString()));

            //Wednesday booking should have 2 staff members
            diaryBookingId = dbHelper.cPBookingDiary.GetByBookingScheduleAndPlannedStartTime(user1BookingSchedule3Id).FirstOrDefault();
            bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookingId);
            Assert.AreEqual(2, bookingDiaryStaffRecords.Count);
            contracts = new List<string>();
            contracts.Add(dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            contracts.Add(dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[1], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            Assert.IsTrue(contracts.Contains(_systemUser1EmploymentContractId.ToString()));
            Assert.IsTrue(contracts.Contains(_systemUser2EmploymentContractId.ToString()));

            //Thursday booking should have 2 staff members
            diaryBookingId = dbHelper.cPBookingDiary.GetByBookingScheduleAndPlannedStartTime(user1BookingSchedule4Id).FirstOrDefault();
            bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookingId);
            Assert.AreEqual(2, bookingDiaryStaffRecords.Count);
            contracts = new List<string>();
            contracts.Add(dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            contracts.Add(dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[1], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());
            Assert.IsTrue(contracts.Contains(_systemUser1EmploymentContractId.ToString()));
            Assert.IsTrue(contracts.Contains(_systemUser2EmploymentContractId.ToString()));

            //Friday booking should have 2 staff members
            diaryBookingId = dbHelper.cPBookingDiary.GetByBookingScheduleAndPlannedStartTime(user1BookingSchedule5Id).FirstOrDefault();
            bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookingId);
            Assert.AreEqual(2, bookingDiaryStaffRecords.Count);
            contracts = new List<string>();

            var fields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            if (fields.ContainsKey("systemuseremploymentcontractid"))
                contracts.Add(fields["systemuseremploymentcontractid"].ToString());

            fields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[1], "systemuseremploymentcontractid");
            if (fields.ContainsKey("systemuseremploymentcontractid"))
                contracts.Add(fields["systemuseremploymentcontractid"].ToString());

            Assert.IsTrue(contracts.Contains(_systemUser1EmploymentContractId.ToString()));
            Assert.IsFalse(contracts.Contains(_systemUser2EmploymentContractId.ToString()));


            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6309

        [TestProperty("JiraIssueID", "ACC-6430")]
        [Description("Step(s) 7 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ExpressBook_ACC_6250_UITestCases004()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-6250 1", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

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

            var firstName = "Pedro";
            var lastName = currentTimeString;
            var _person1ID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontract1Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _person1ID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontract1Id, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6250", "98910", null, new DateTime(2020, 1, 1), null);

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

            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser1Id, 14);

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

            #region Diary Booking

            var nextWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);
            var cpBookingDiaryId = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType2, providerId, nextWeekMonday, new TimeSpan(2, 0, 0), nextWeekMonday, new TimeSpan(12, 0, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", cpBookingDiaryId, _systemUser1EmploymentContractId, systemUser1Id);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(14, 0, 0), new TimeSpan(20, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _person1ID, _personcontract1Id, careProviderContractServiceId);

            #endregion


            #region Step 7

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
                .InsertTextOnExpressBookingStartDate(nextWeekMonday.ToString("dd/MM/yyyy"))
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
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

            //Validate that we have 1 error logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResults.Count);
            var messages = new List<string>();
            messages.Add(dbHelper.cpExpressBookingResult.GetById(expressBookingResults[0], "exceptionmessage")["exceptionmessage"].ToString());
            Assert.IsTrue(messages.Contains("Care Provider System User 1 " + currentTimeString + " has exceeded their maximum working hours. They have been deallocated."));

            //validate that the 1 Diary Bookings are created
            var diaryBookings = dbHelper.cPBookingDiary.GetByCPBookingScheduleId(cpBookingSchedule1Id);
            Assert.AreEqual(1, diaryBookings.Count);

            //booking should be dealocated
            var bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookings[0]);
            Assert.AreEqual(1, bookingDiaryStaffRecords.Count);
            Assert.IsFalse(dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid").ContainsKey("systemuseremploymentcontractid"));

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6431")]
        [Description("Step(s) 8 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ExpressBook_ACC_6250_UITestCases005()
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

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-6250 T1", 1, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType1, 1).FirstOrDefault(); //Booking (to location)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType1, false);

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

            var careProviderServiceMapping1Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType1, null, "");

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

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType1, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);


            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6250", "98910", null, new DateTime(2020, 1, 1), null);

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

            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser1Id, 14);

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType1);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Diary Booking

            var nextWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);
            var cpBookingDiaryId = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType1, providerId, nextWeekMonday, new TimeSpan(9, 0, 0), nextWeekMonday, new TimeSpan(19, 0, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", cpBookingDiaryId, _systemUser1EmploymentContractId, systemUser1Id);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, 1, 1, new TimeSpan(5, 0, 0), new TimeSpan(11, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

            #endregion


            #region Step 8

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
                .InsertTextOnExpressBookingStartDate(nextWeekMonday.ToString("dd/MM/yyyy"))
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
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

            //Validate that we have 1 error logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(0, expressBookingResults.Count);

            //validate that the 1 Diary Bookings are created
            var diaryBookings = dbHelper.cPBookingDiary.GetByCPBookingScheduleId(cpBookingSchedule1Id);
            Assert.AreEqual(1, diaryBookings.Count);

            //booking should be allocated to the staff member
            var bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookings[0]);
            Assert.AreEqual(1, bookingDiaryStaffRecords.Count);
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString());

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6432")]
        [Description("Step(s) 9 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ExpressBook_ACC_6250_UITestCases006()
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

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-6250 T1", 1, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-6250 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType1, 2).FirstOrDefault(); //Booking (to internal care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType2, 1).FirstOrDefault(); //Booking (to location)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType1, false);
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

            commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType1, null, "");
            commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType2, null, "");

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

            var careProviderContractService1Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType1, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService2Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService1Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService2Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);


            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6250", "98910", null, new DateTime(2020, 1, 1), null);

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

            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser1Id, 12);

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Diary Booking

            var nextWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);
            var cpBookingDiaryId = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType1, providerId, nextWeekMonday, new TimeSpan(9, 0, 0), nextWeekMonday, new TimeSpan(19, 0, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", cpBookingDiaryId, _systemUser1EmploymentContractId, systemUser1Id);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(5, 0, 0), new TimeSpan(11, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

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
                .InsertTextOnExpressBookingStartDate(nextWeekMonday.ToString("dd/MM/yyyy"))
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
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

            //Validate that we have 1 error logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResults.Count);
            var messages = new List<string>();
            messages.Add(dbHelper.cpExpressBookingResult.GetById(expressBookingResults[0], "exceptionmessage")["exceptionmessage"].ToString());
            Assert.IsTrue(messages.Contains("Care Provider System User 1 " + currentTimeString + " has exceeded their maximum working hours. They have been deallocated."));

            //validate that the 1 Diary Bookings are created
            var diaryBookings = dbHelper.cPBookingDiary.GetByCPBookingScheduleId(cpBookingSchedule1Id);
            Assert.AreEqual(1, diaryBookings.Count);

            //booking should be dealocated
            var bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookings[0]);
            Assert.AreEqual(1, bookingDiaryStaffRecords.Count);
            Assert.IsFalse(dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid").ContainsKey("systemuseremploymentcontractid"));

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6433")]
        [Description("Step(s) 10 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ExpressBook_ACC_6250_UITestCases007()
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

            var _bookingType3 = commonMethodsDB.CreateCPBookingType("BTC ACC-6433 T3", 3, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType4 = commonMethodsDB.CreateCPBookingType("BTC ACC-6433 T4", 4, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 3, true, false, false, false, true, false);

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType3, 4).FirstOrDefault(); //Booking (to internal non-care booking e.g. annual leave, training)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType4, 3).FirstOrDefault(); //Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType3, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType4, false);

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

            commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType3, null, "");
            commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType4, null, "");

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

            var careProviderContractService1Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType3, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService2Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType4, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService1Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService2Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);


            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6250", "98910", null, new DateTime(2020, 1, 1), null);

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

            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser1Id, 14);

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType3);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType4);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Diary Booking

            var nextWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);
            var cpBookingDiaryId = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType3, providerId, nextWeekMonday, new TimeSpan(8, 0, 0), nextWeekMonday, new TimeSpan(20, 0, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", cpBookingDiaryId, _systemUser1EmploymentContractId, systemUser1Id);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType4, 1, 1, 1, new TimeSpan(5, 0, 0), new TimeSpan(11, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

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
                .InsertTextOnExpressBookingStartDate(nextWeekMonday.ToString("dd/MM/yyyy"))
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
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

            //Validate that we have 0 error logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(0, expressBookingResults.Count);

            //validate that the 1 Diary Booking(s) is(are) created
            var diaryBookings = dbHelper.cPBookingDiary.GetByCPBookingScheduleId(cpBookingSchedule1Id);
            Assert.AreEqual(1, diaryBookings.Count);

            //booking should be alocated
            var bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookings[0]);
            Assert.AreEqual(1, bookingDiaryStaffRecords.Count);
            var bookingDiary_EmploymentContractId = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString();
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), bookingDiary_EmploymentContractId);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6434")]
        [Description("Step(s) 11 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ExpressBook_ACC_6250_UITestCases008()
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

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-6250 T1", 1, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-6250 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType3 = commonMethodsDB.CreateCPBookingType("BTC ACC-6250 T3", 3, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType1, 3).FirstOrDefault(); //Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType2, 3).FirstOrDefault(); //Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType3, 3).FirstOrDefault(); //Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType1, false);
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

            commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType1, null, "");
            commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType2, null, "");
            commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType3, null, "");

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

            var careProviderContractService1Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType1, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService2Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService3Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType3, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService1Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService2Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService3Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);


            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6250", "98910", null, new DateTime(2020, 1, 1), null);

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

            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser1Id, 14);

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType3);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Diary Booking

            var nextWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);
            var cpBookingDiaryId = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType3, providerId, nextWeekMonday, new TimeSpan(8, 0, 0), nextWeekMonday, new TimeSpan(20, 0, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", cpBookingDiaryId, _systemUser1EmploymentContractId, systemUser1Id);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 2, 2, new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0), providerId, "Express Book Testing");
            var cpBookingSchedule2Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType3, 1, 3, 3, new TimeSpan(9, 0, 0), new TimeSpan(11, 0, 0), providerId, "Express Book Testing");
            var cpBookingSchedule3Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, 4, 4, new TimeSpan(10, 0, 0), new TimeSpan(11, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule2Id, 1);
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule3Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule2Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule3Id, _systemUser1EmploymentContractId, systemUser1Id);

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
                .InsertTextOnExpressBookingStartDate(nextWeekMonday.ToString("dd/MM/yyyy"))
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
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

            //Validate that we have 1 error logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResults.Count);
            var messages = new List<string>();
            messages.Add(dbHelper.cpExpressBookingResult.GetById(expressBookingResults[0], "exceptionmessage")["exceptionmessage"].ToString());
            Assert.IsTrue(messages.Contains("Care Provider System User 1 " + currentTimeString + " has exceeded their maximum working hours. They have been deallocated."));


            //validate that the 1 Diary Booking is created for Tuesday 9am to 10 am
            var diaryBookings = dbHelper.cPBookingDiary.GetByCPBookingScheduleId(cpBookingSchedule1Id);
            Assert.AreEqual(1, diaryBookings.Count);

            //Tuesday 9am to 10 am booking should be alocated
            var bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookings[0]);
            Assert.AreEqual(1, bookingDiaryStaffRecords.Count);
            var bookingDiary_EmploymentContractId = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString();
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), bookingDiary_EmploymentContractId);


            //validate that the 1 Diary Booking is created for Wednesday 9am to 11am
            diaryBookings = dbHelper.cPBookingDiary.GetByCPBookingScheduleId(cpBookingSchedule2Id);
            Assert.AreEqual(1, diaryBookings.Count);

            //Wednesday 9am to 11am booking should be dealocated
            bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookings[0]);
            Assert.AreEqual(1, bookingDiaryStaffRecords.Count);
            Assert.IsFalse(dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid").ContainsKey("systemuseremploymentcontractid"));



            //validate that the 1 Diary Booking is created for Thursday 10 am to 11 am
            diaryBookings = dbHelper.cPBookingDiary.GetByCPBookingScheduleId(cpBookingSchedule3Id);
            Assert.AreEqual(1, diaryBookings.Count);

            //Thursday 10 am to 11 am booking should be alocated
            bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookings[0]);
            Assert.AreEqual(1, bookingDiaryStaffRecords.Count);
            bookingDiary_EmploymentContractId = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString();
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), bookingDiary_EmploymentContractId);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6435")]
        [Description("Step(s) 12 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ExpressBook_ACC_6250_UITestCases009()
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

            var _bookingType3 = commonMethodsDB.CreateCPBookingType("BTC ACC-6250 T3", 3, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType4 = commonMethodsDB.CreateCPBookingType("BTC ACC-6250 T4", 4, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 3, true, false, false, false, true, false);

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType3, 4).FirstOrDefault(); //Booking (to internal non-care booking e.g. annual leave, training)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType4, 3).FirstOrDefault(); //Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType4, false);
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

            commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType3, null, "");
            commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType4, null, "");

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

            var careProviderContractService3Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType3, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService4Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType4, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService3Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService4Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6250", "98910", null, new DateTime(2020, 1, 1), null);

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

            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser1Id, 14);

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType3);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType4);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Diary Booking

            var nextWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);
            var cpBookingDiaryId = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType3, providerId, nextWeekMonday, new TimeSpan(8, 0, 0), nextWeekMonday, new TimeSpan(22, 0, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", cpBookingDiaryId, _systemUser1EmploymentContractId, systemUser1Id);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType4, 1, 2, 2, new TimeSpan(9, 0, 0), new TimeSpan(11, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

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
                .InsertTextOnExpressBookingStartDate(nextWeekMonday.ToString("dd/MM/yyyy"))
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
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

            //Validate that we have 0 error logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(0, expressBookingResults.Count);


            //validate that the 1 Diary Booking is created for Tuesday 9am to 10 am
            var diaryBookings = dbHelper.cPBookingDiary.GetByCPBookingScheduleId(cpBookingSchedule1Id);
            Assert.AreEqual(1, diaryBookings.Count);

            //Tuesday 9am to 11 am booking should be alocated
            var bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookings[0]);
            Assert.AreEqual(1, bookingDiaryStaffRecords.Count);
            var bookingDiary_EmploymentContractId = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString();
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), bookingDiary_EmploymentContractId);


            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6310

        [TestProperty("JiraIssueID", "ACC-6474")]
        [Description("Step(s) 13 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ExpressBook_ACC_6250_UITestCases010()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-6250 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType3 = commonMethodsDB.CreateCPBookingType("BTC ACC-6250 T3", 3, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType2, 3).FirstOrDefault(); //Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

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

            var careProviderContractService1Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService2Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType3, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService1Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService2Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6250", "98910", null, new DateTime(2020, 1, 1), null);

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

            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser1Id, 14);

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType3);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Diary Booking

            var nextWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);
            var cpBookingDiaryId = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType3, providerId, nextWeekMonday, new TimeSpan(8, 0, 0), nextWeekMonday, new TimeSpan(20, 0, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", cpBookingDiaryId, _systemUser1EmploymentContractId, systemUser1Id);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 7, 1, new TimeSpan(21, 0, 0), new TimeSpan(2, 0, 0), providerId, "Express Book Testing");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

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
                .InsertTextOnExpressBookingStartDate(nextWeekMonday.ToString("dd/MM/yyyy"))
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
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

            //Validate that we have 1 error logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResults.Count);
            var messages = new List<string>();
            messages.Add(dbHelper.cpExpressBookingResult.GetById(expressBookingResults[0], "exceptionmessage")["exceptionmessage"].ToString());
            Assert.IsTrue(messages.Contains("Care Provider System User 1 " + currentTimeString + " has exceeded their maximum working hours. They have been deallocated."));

            //validate that the 1 Diary Bookings are created
            var diaryBookings = dbHelper.cPBookingDiary.GetByCPBookingScheduleId(cpBookingSchedule1Id);
            Assert.AreEqual(1, diaryBookings.Count);

            //booking should be dealocated
            var bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookings[0]);
            Assert.AreEqual(1, bookingDiaryStaffRecords.Count);
            Assert.IsFalse(dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid").ContainsKey("systemuseremploymentcontractid"));

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6475")]
        [Description("Step(s) 14 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ExpressBook_ACC_6250_UITestCases011()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-6250 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType3 = commonMethodsDB.CreateCPBookingType("BTC ACC-6250 T3", 3, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType2, 3).FirstOrDefault(); //Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

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

            var careProviderContractService1Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService2Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType3, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService1Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService2Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6250", "98910", null, new DateTime(2020, 1, 1), null);

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

            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser1Id, 14);

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType3);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Diary Booking

            var nextWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);
            var cpBookingDiaryId = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType3, providerId, nextWeekMonday, new TimeSpan(8, 0, 0), nextWeekMonday, new TimeSpan(20, 0, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", cpBookingDiaryId, _systemUser1EmploymentContractId, systemUser1Id);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 7, 1, new TimeSpan(22, 0, 0), new TimeSpan(2, 0, 0), providerId, "Express Book Testing");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

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
                .InsertTextOnExpressBookingStartDate(nextWeekMonday.ToString("dd/MM/yyyy"))
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
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

            //Validate that we have 0 error logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(0, expressBookingResults.Count);

            //validate that the 1 Diary Bookings are created
            var diaryBookings = dbHelper.cPBookingDiary.GetByCPBookingScheduleId(cpBookingSchedule1Id);
            Assert.AreEqual(1, diaryBookings.Count);

            //booking should be allocated to the staff
            var bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookings[0]);
            Assert.AreEqual(1, bookingDiaryStaffRecords.Count);
            var bookingDiary_EmploymentContractId = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString();
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), bookingDiary_EmploymentContractId);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6476")]
        [Description("Step(s) 15 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ExpressBook_ACC_6250_UITestCases012()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var addressType = 10; //Home

            var provider1Name = "Provider A " + currentTimeString;
            var provider1Id = commonMethodsDB.CreateProvider(provider1Name, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var provider2Name = "Provider B " + currentTimeString;
            var provider2Id = commonMethodsDB.CreateProvider(provider2Name, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var provider3Name = "Provider C " + currentTimeString;
            var provider3Id = commonMethodsDB.CreateProvider(provider3Name, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var provider4Name = "Provider D " + currentTimeString;
            var provider4Id = commonMethodsDB.CreateProvider(provider4Name, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-6250 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType3 = commonMethodsDB.CreateCPBookingType("BTC ACC-6250 T3", 3, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType2, 3).FirstOrDefault(); //Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, provider1Id, _bookingType2, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, provider2Id, _bookingType2, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, provider3Id, _bookingType2, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, provider4Id, _bookingType2, false);

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, provider1Id, _bookingType3, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, provider2Id, _bookingType3, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, provider3Id, _bookingType3, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, provider4Id, _bookingType3, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1Id, funderProviderID);

            var contractScheme2Name = "CPCS_B_" + currentTimeString;
            var contractScheme2Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme2Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme2Name, new DateTime(2020, 1, 1), contractScheme2Code, provider2Id, funderProviderID);

            var contractScheme3Name = "CPCS_C_" + currentTimeString;
            var contractScheme3Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme3Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme3Name, new DateTime(2020, 1, 1), contractScheme3Code, provider3Id, funderProviderID);

            var contractScheme4Name = "CPCS_D_" + currentTimeString;
            var contractScheme4Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme4Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme4Name, new DateTime(2020, 1, 1), contractScheme4Code, provider4Id, funderProviderID);

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

            dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme2Id, _careProviderBatchGroupingId,
                new DateTime(2023, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme3Id, _careProviderBatchGroupingId,
                new DateTime(2023, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme4Id, _careProviderBatchGroupingId,
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

            var careProviderContractService1Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", provider1Id, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService2Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", provider1Id, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType3, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            var careProviderContractService3Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", provider2Id, funderProviderID, careProviderContractScheme2Id, careProviderService1Id, null, _bookingType2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService4Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", provider2Id, funderProviderID, careProviderContractScheme2Id, careProviderService1Id, null, _bookingType3, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            var careProviderContractService5Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", provider3Id, funderProviderID, careProviderContractScheme3Id, careProviderService1Id, null, _bookingType2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService6Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", provider3Id, funderProviderID, careProviderContractScheme3Id, careProviderService1Id, null, _bookingType3, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            var careProviderContractService7Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", provider4Id, funderProviderID, careProviderContractScheme4Id, careProviderService1Id, null, _bookingType2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService8Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", provider4Id, funderProviderID, careProviderContractScheme4Id, careProviderService1Id, null, _bookingType3, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService1Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService2Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService3Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService4Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService5Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService6Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService7Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService8Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6250", "98910", null, new DateTime(2020, 1, 1), null);

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

            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser1Id, 14);

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType3);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Diary Booking

            var nextWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);

            var cpBookingDiary1Id = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType3, provider1Id, nextWeekMonday, new TimeSpan(4, 0, 0), nextWeekMonday, new TimeSpan(8, 0, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", cpBookingDiary1Id, _systemUser1EmploymentContractId, systemUser1Id);

            var cpBookingDiary2Id = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType3, provider2Id, nextWeekMonday, new TimeSpan(8, 0, 0), nextWeekMonday, new TimeSpan(12, 0, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", cpBookingDiary2Id, _systemUser1EmploymentContractId, systemUser1Id);

            var cpBookingDiary3Id = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType3, provider3Id, nextWeekMonday, new TimeSpan(12, 0, 0), nextWeekMonday, new TimeSpan(16, 0, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", cpBookingDiary3Id, _systemUser1EmploymentContractId, systemUser1Id);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(17, 0, 0), new TimeSpan(22, 0, 0), provider4Id, "Express Book Testing");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

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

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(provider4Name, provider4Id);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(nextWeekMonday.ToString("dd/MM/yyyy"))
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickSaveAndCloseButton();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickRefreshButton();

            var expressBookings = dbHelper.cpExpressBookingCriteria.GetByRegardingID(provider4Id);
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

            //Validate that we have 1 error logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResults.Count);
            var messages = new List<string>();
            messages.Add(dbHelper.cpExpressBookingResult.GetById(expressBookingResults[0], "exceptionmessage")["exceptionmessage"].ToString());
            Assert.IsTrue(messages.Contains("Care Provider System User 1 " + currentTimeString + " has exceeded their maximum working hours. They have been deallocated."));

            //validate that the 1 Diary Bookings are created
            var diaryBookings = dbHelper.cPBookingDiary.GetByCPBookingScheduleId(cpBookingSchedule1Id);
            Assert.AreEqual(1, diaryBookings.Count);

            //booking should be dealocated
            var bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookings[0]);
            Assert.AreEqual(1, bookingDiaryStaffRecords.Count);
            Assert.IsFalse(dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid").ContainsKey("systemuseremploymentcontractid"));


            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6477")]
        [Description("Step(s) 16 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ExpressBook_ACC_6250_UITestCases013()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var addressType = 10; //Home

            var provider1Name = "Provider A " + currentTimeString;
            var provider1Id = commonMethodsDB.CreateProvider(provider1Name, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var provider2Name = "Provider B " + currentTimeString;
            var provider2Id = commonMethodsDB.CreateProvider(provider2Name, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var provider3Name = "Provider C " + currentTimeString;
            var provider3Id = commonMethodsDB.CreateProvider(provider3Name, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var provider4Name = "Provider D " + currentTimeString;
            var provider4Id = commonMethodsDB.CreateProvider(provider4Name, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-6250 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType3 = commonMethodsDB.CreateCPBookingType("BTC ACC-6250 T3", 3, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType2, 3).FirstOrDefault(); //Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, provider1Id, _bookingType2, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, provider2Id, _bookingType2, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, provider3Id, _bookingType2, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, provider4Id, _bookingType2, false);

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, provider1Id, _bookingType3, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, provider2Id, _bookingType3, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, provider3Id, _bookingType3, false);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, provider4Id, _bookingType3, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1Id, funderProviderID);

            var contractScheme2Name = "CPCS_B_" + currentTimeString;
            var contractScheme2Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme2Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme2Name, new DateTime(2020, 1, 1), contractScheme2Code, provider2Id, funderProviderID);

            var contractScheme3Name = "CPCS_C_" + currentTimeString;
            var contractScheme3Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme3Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme3Name, new DateTime(2020, 1, 1), contractScheme3Code, provider3Id, funderProviderID);

            var contractScheme4Name = "CPCS_D_" + currentTimeString;
            var contractScheme4Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme4Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme4Name, new DateTime(2020, 1, 1), contractScheme4Code, provider4Id, funderProviderID);

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

            dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme2Id, _careProviderBatchGroupingId,
                new DateTime(2023, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme3Id, _careProviderBatchGroupingId,
                new DateTime(2023, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme4Id, _careProviderBatchGroupingId,
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

            var careProviderContractService1Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", provider1Id, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService2Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", provider1Id, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType3, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            var careProviderContractService3Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", provider2Id, funderProviderID, careProviderContractScheme2Id, careProviderService1Id, null, _bookingType2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService4Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", provider2Id, funderProviderID, careProviderContractScheme2Id, careProviderService1Id, null, _bookingType3, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            var careProviderContractService5Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", provider3Id, funderProviderID, careProviderContractScheme3Id, careProviderService1Id, null, _bookingType2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService6Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", provider3Id, funderProviderID, careProviderContractScheme3Id, careProviderService1Id, null, _bookingType3, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            var careProviderContractService7Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", provider4Id, funderProviderID, careProviderContractScheme4Id, careProviderService1Id, null, _bookingType2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService8Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", provider4Id, funderProviderID, careProviderContractScheme4Id, careProviderService1Id, null, _bookingType3, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService1Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService2Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService3Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService4Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService5Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService6Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService7Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService8Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6250", "98910", null, new DateTime(2020, 1, 1), null);

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

            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser1Id, 14);

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType3);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Diary Booking

            var nextWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);

            var cpBookingDiary1Id = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType3, provider1Id, nextWeekMonday, new TimeSpan(4, 0, 0), nextWeekMonday, new TimeSpan(8, 0, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", cpBookingDiary1Id, _systemUser1EmploymentContractId, systemUser1Id);

            var cpBookingDiary2Id = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType3, provider2Id, nextWeekMonday, new TimeSpan(8, 0, 0), nextWeekMonday, new TimeSpan(12, 0, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", cpBookingDiary2Id, _systemUser1EmploymentContractId, systemUser1Id);

            var cpBookingDiary3Id = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType3, provider3Id, nextWeekMonday, new TimeSpan(12, 0, 0), nextWeekMonday, new TimeSpan(16, 0, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", cpBookingDiary3Id, _systemUser1EmploymentContractId, systemUser1Id);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 2, 2, new TimeSpan(17, 0, 0), new TimeSpan(19, 0, 0), provider4Id, "Express Book Testing");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

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

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(provider4Name, provider4Id);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(nextWeekMonday.ToString("dd/MM/yyyy"))
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickSaveAndCloseButton();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickRefreshButton();

            var expressBookings = dbHelper.cpExpressBookingCriteria.GetByRegardingID(provider4Id);
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

            //Validate that we have 0 error logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(0, expressBookingResults.Count);

            //validate that the 1 Diary Bookings are created
            var diaryBookings = dbHelper.cPBookingDiary.GetByCPBookingScheduleId(cpBookingSchedule1Id);
            Assert.AreEqual(1, diaryBookings.Count);

            //booking should be allocated to the staff
            var bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookings[0]);
            Assert.AreEqual(1, bookingDiaryStaffRecords.Count);
            var bookingDiary_EmploymentContractId = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid")["systemuseremploymentcontractid"].ToString();
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), bookingDiary_EmploymentContractId);


            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6478")]
        [Description("Step(s) 17 (P1) from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ExpressBook_ACC_6250_UITestCases014()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-6250 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType3 = commonMethodsDB.CreateCPBookingType("BTC ACC-6250 T3", 3, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType2, 3).FirstOrDefault(); //Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

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

            var careProviderContractService1Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService2Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType3, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService1Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService2Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6250", "98910", null, new DateTime(2020, 1, 1), null);

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

            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser1Id, 8);

            var user2name = "cpsu_2_" + currentTimeString;
            var user2FirstName = "Care Provider";
            var user2LastName = "System User 2 " + currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser2Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser2Id, 8);

            var user3name = "cpsu_3_" + currentTimeString;
            var user3FirstName = "Care Provider";
            var user3LastName = "System User 3 " + currentTimeString;
            var systemUser3Id = commonMethodsDB.CreateSystemUserRecord(user3name, user3FirstName, user3LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser3Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser3Id, 8);

            var user4name = "cpsu_4_" + currentTimeString;
            var user4FirstName = "Care Provider";
            var user4LastName = "System User 4 " + currentTimeString;
            var systemUser4Id = commonMethodsDB.CreateSystemUserRecord(user4name, user4FirstName, user4LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser4Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser4Id, 8);

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContract1Id = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser1EmploymentContract2Id = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser2Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser1EmploymentContract3Id = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser3Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser1EmploymentContract4Id = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser4Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContract1Id, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContract2Id, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContract3Id, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContract4Id, _bookingType2);

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContract1Id, _bookingType3);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContract2Id, _bookingType3);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContract3Id, _bookingType3);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContract4Id, _bookingType3);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContract1Id, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser2Id, _teamId, _systemUser1EmploymentContract2Id, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser3Id, _teamId, _systemUser1EmploymentContract3Id, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser4Id, _teamId, _systemUser1EmploymentContract4Id, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0), providerId, "Express Book Testing");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContract1Id, systemUser1Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContract2Id, systemUser2Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContract3Id, systemUser3Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContract4Id, systemUser4Id);

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

            var nextWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(nextWeekMonday.ToString("dd/MM/yyyy"))
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
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

            //Validate that we have 1 error logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResults.Count);
            var messages = new List<string>();
            messages.Add(dbHelper.cpExpressBookingResult.GetById(expressBookingResults[0], "exceptionmessage")["exceptionmessage"].ToString());
            Assert.IsTrue(messages.Contains("Care Provider System User 1 " + currentTimeString + ", Care Provider System User 2 " + currentTimeString + ", Care Provider System User 3 " + currentTimeString + ", Care Provider System User 4 " + currentTimeString + " have exceeded their maximum working hours. They have been deallocated."));

            //validate that the 1 Diary Bookings are created
            var diaryBookings = dbHelper.cPBookingDiary.GetByCPBookingScheduleId(cpBookingSchedule1Id);
            Assert.AreEqual(1, diaryBookings.Count);

            //booking should have 4 dealocated staff
            var bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookings[0]);
            Assert.AreEqual(4, bookingDiaryStaffRecords.Count);
            Assert.IsFalse(dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid").ContainsKey("systemuseremploymentcontractid"));
            Assert.IsFalse(dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[1], "systemuseremploymentcontractid").ContainsKey("systemuseremploymentcontractid"));
            Assert.IsFalse(dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[2], "systemuseremploymentcontractid").ContainsKey("systemuseremploymentcontractid"));
            Assert.IsFalse(dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[3], "systemuseremploymentcontractid").ContainsKey("systemuseremploymentcontractid"));

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6479")]
        [Description("Step(s) 17 (P2) from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ExpressBook_ACC_6250_UITestCases015()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-6250 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType3 = commonMethodsDB.CreateCPBookingType("BTC ACC-6250 T3", 3, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType2, 3).FirstOrDefault(); //Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

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

            var careProviderContractService1Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService2Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType3, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService1Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService2Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6250", "98910", null, new DateTime(2020, 1, 1), null);

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

            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser1Id, 6);

            var user2name = "cpsu_2_" + currentTimeString;
            var user2FirstName = "Care Provider";
            var user2LastName = "System User 2 " + currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser2Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser2Id, 6);

            var user3name = "cpsu_3_" + currentTimeString;
            var user3FirstName = "Care Provider";
            var user3LastName = "System User 3 " + currentTimeString;
            var systemUser3Id = commonMethodsDB.CreateSystemUserRecord(user3name, user3FirstName, user3LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser3Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser3Id, 8);

            var user4name = "cpsu_4_" + currentTimeString;
            var user4FirstName = "Care Provider";
            var user4LastName = "System User 4 " + currentTimeString;
            var systemUser4Id = commonMethodsDB.CreateSystemUserRecord(user4name, user4FirstName, user4LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser4Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser4Id, 8);

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContract1Id = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser2EmploymentContract1Id = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser2Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser3EmploymentContract1Id = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser3Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser4EmploymentContract1Id = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser4Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContract1Id, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContract1Id, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContract1Id, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser4EmploymentContract1Id, _bookingType2);

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContract1Id, _bookingType3);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContract1Id, _bookingType3);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContract1Id, _bookingType3);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser4EmploymentContract1Id, _bookingType3);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContract1Id, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser2Id, _teamId, _systemUser2EmploymentContract1Id, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser3Id, _teamId, _systemUser3EmploymentContract1Id, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser4Id, _teamId, _systemUser4EmploymentContract1Id, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(8, 0, 0), new TimeSpan(16, 0, 0), providerId, "Express Book Testing");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContract1Id, systemUser1Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser2EmploymentContract1Id, systemUser2Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser3EmploymentContract1Id, systemUser3Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser4EmploymentContract1Id, systemUser4Id);

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

            var nextWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(nextWeekMonday.ToString("dd/MM/yyyy"))
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
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

            //Validate that we have 1 error logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResults.Count);
            var messages = new List<string>();
            messages.Add(dbHelper.cpExpressBookingResult.GetById(expressBookingResults[0], "exceptionmessage")["exceptionmessage"].ToString());
            Assert.IsTrue(messages.Contains("Care Provider System User 1 " + currentTimeString + ", Care Provider System User 2 " + currentTimeString + " have exceeded their maximum working hours. They have been deallocated."));

            //validate that the 1 Diary Bookings are created
            var diaryBookings = dbHelper.cPBookingDiary.GetByCPBookingScheduleId(cpBookingSchedule1Id);
            Assert.AreEqual(1, diaryBookings.Count);

            //booking should have 2 dealocated staff
            var bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookings[0]);
            Assert.AreEqual(4, bookingDiaryStaffRecords.Count);

            var linkedEmploymentContractIds = new List<string>();
            var bookingDiaryStaffRecordFields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            if (bookingDiaryStaffRecordFields.ContainsKey("systemuseremploymentcontractid"))
                linkedEmploymentContractIds.Add(bookingDiaryStaffRecordFields["systemuseremploymentcontractid"].ToString());

            bookingDiaryStaffRecordFields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[1], "systemuseremploymentcontractid");
            if (bookingDiaryStaffRecordFields.ContainsKey("systemuseremploymentcontractid"))
                linkedEmploymentContractIds.Add(bookingDiaryStaffRecordFields["systemuseremploymentcontractid"].ToString());

            bookingDiaryStaffRecordFields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[2], "systemuseremploymentcontractid");
            if (bookingDiaryStaffRecordFields.ContainsKey("systemuseremploymentcontractid"))
                linkedEmploymentContractIds.Add(bookingDiaryStaffRecordFields["systemuseremploymentcontractid"].ToString());

            bookingDiaryStaffRecordFields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[3], "systemuseremploymentcontractid");
            if (bookingDiaryStaffRecordFields.ContainsKey("systemuseremploymentcontractid"))
                linkedEmploymentContractIds.Add(bookingDiaryStaffRecordFields["systemuseremploymentcontractid"].ToString());

            Assert.IsFalse(linkedEmploymentContractIds.Contains(_systemUser1EmploymentContract1Id.ToString()));
            Assert.IsFalse(linkedEmploymentContractIds.Contains(_systemUser2EmploymentContract1Id.ToString()));
            Assert.IsTrue(linkedEmploymentContractIds.Contains(_systemUser3EmploymentContract1Id.ToString()));
            Assert.IsTrue(linkedEmploymentContractIds.Contains(_systemUser4EmploymentContract1Id.ToString()));

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6480")]
        [Description("Step(s) 17 (P3) from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ExpressBook_ACC_6250_UITestCases016()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-6250 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType3 = commonMethodsDB.CreateCPBookingType("BTC ACC-6250 T3", 3, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType2, 3).FirstOrDefault(); //Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

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

            var careProviderContractService1Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService2Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType3, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService1Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService2Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6250", "98910", null, new DateTime(2020, 1, 1), null);

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

            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser1Id, 5);

            var user2name = "cpsu_2_" + currentTimeString;
            var user2FirstName = "Care Provider";
            var user2LastName = "System User 2 " + currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser2Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser2Id, 6);

            var user3name = "cpsu_3_" + currentTimeString;
            var user3FirstName = "Care Provider";
            var user3LastName = "System User 3 " + currentTimeString;
            var systemUser3Id = commonMethodsDB.CreateSystemUserRecord(user3name, user3FirstName, user3LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser3Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser3Id, 8);

            var user4name = "cpsu_4_" + currentTimeString;
            var user4FirstName = "Care Provider";
            var user4LastName = "System User 4 " + currentTimeString;
            var systemUser4Id = commonMethodsDB.CreateSystemUserRecord(user4name, user4FirstName, user4LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser4Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser4Id, 8);

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContract1Id = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser2EmploymentContract1Id = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser2Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser3EmploymentContract1Id = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser3Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser4EmploymentContract1Id = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser4Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContract1Id, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContract1Id, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContract1Id, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser4EmploymentContract1Id, _bookingType2);

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContract1Id, _bookingType3);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContract1Id, _bookingType3);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContract1Id, _bookingType3);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser4EmploymentContract1Id, _bookingType3);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContract1Id, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser2Id, _teamId, _systemUser2EmploymentContract1Id, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser3Id, _teamId, _systemUser3EmploymentContract1Id, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser4Id, _teamId, _systemUser4EmploymentContract1Id, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(8, 0, 0), new TimeSpan(14, 0, 0), providerId, "Express Book Testing");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContract1Id, systemUser1Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser2EmploymentContract1Id, systemUser2Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser3EmploymentContract1Id, systemUser3Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser4EmploymentContract1Id, systemUser4Id);

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

            var nextWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(nextWeekMonday.ToString("dd/MM/yyyy"))
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
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

            //Validate that we have 1 error logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResults.Count);
            var messages = new List<string>();
            messages.Add(dbHelper.cpExpressBookingResult.GetById(expressBookingResults[0], "exceptionmessage")["exceptionmessage"].ToString());
            Assert.IsTrue(messages.Contains("Care Provider System User 1 " + currentTimeString + " has exceeded their maximum working hours. They have been deallocated."));

            //validate that the 1 Diary Bookings are created
            var diaryBookings = dbHelper.cPBookingDiary.GetByCPBookingScheduleId(cpBookingSchedule1Id);
            Assert.AreEqual(1, diaryBookings.Count);

            //booking should have 2 dealocated staff
            var bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookings[0]);
            Assert.AreEqual(4, bookingDiaryStaffRecords.Count);

            var linkedEmploymentContractIds = new List<string>();
            var bookingDiaryStaffRecordFields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            if (bookingDiaryStaffRecordFields.ContainsKey("systemuseremploymentcontractid"))
                linkedEmploymentContractIds.Add(bookingDiaryStaffRecordFields["systemuseremploymentcontractid"].ToString());

            bookingDiaryStaffRecordFields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[1], "systemuseremploymentcontractid");
            if (bookingDiaryStaffRecordFields.ContainsKey("systemuseremploymentcontractid"))
                linkedEmploymentContractIds.Add(bookingDiaryStaffRecordFields["systemuseremploymentcontractid"].ToString());

            bookingDiaryStaffRecordFields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[2], "systemuseremploymentcontractid");
            if (bookingDiaryStaffRecordFields.ContainsKey("systemuseremploymentcontractid"))
                linkedEmploymentContractIds.Add(bookingDiaryStaffRecordFields["systemuseremploymentcontractid"].ToString());

            bookingDiaryStaffRecordFields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[3], "systemuseremploymentcontractid");
            if (bookingDiaryStaffRecordFields.ContainsKey("systemuseremploymentcontractid"))
                linkedEmploymentContractIds.Add(bookingDiaryStaffRecordFields["systemuseremploymentcontractid"].ToString());

            Assert.IsFalse(linkedEmploymentContractIds.Contains(_systemUser1EmploymentContract1Id.ToString()));
            Assert.IsTrue(linkedEmploymentContractIds.Contains(_systemUser2EmploymentContract1Id.ToString()));
            Assert.IsTrue(linkedEmploymentContractIds.Contains(_systemUser3EmploymentContract1Id.ToString()));
            Assert.IsTrue(linkedEmploymentContractIds.Contains(_systemUser4EmploymentContract1Id.ToString()));

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6481")]
        [Description("Step(s) 17 (P4) from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Express Booking Criteria")]
        public void ExpressBook_ACC_6250_UITestCases017()
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

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-6250 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType3 = commonMethodsDB.CreateCPBookingType("BTC ACC-6250 T3", 3, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Booking Type Clash Action

            var cpBookingTypeClashActionId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(_bookingType2, 3).FirstOrDefault(); //Booking (to external care activity)
            dbHelper.cpBookingTypeClashAction.UpdateDoubleBookingActionId(cpBookingTypeClashActionId, 1); //Allow

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

            var careProviderContractService1Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService2Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType3, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService1Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService2Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6250", "98910", null, new DateTime(2020, 1, 1), null);

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

            var user1name = "cpsu_1_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser1Id, 5);

            var user2name = "cpsu_2_" + currentTimeString;
            var user2FirstName = "Care Provider";
            var user2LastName = "System User 2 " + currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser2Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser2Id, 6);

            var user3name = "cpsu_3_" + currentTimeString;
            var user3FirstName = "Care Provider";
            var user3LastName = "System User 3 " + currentTimeString;
            var systemUser3Id = commonMethodsDB.CreateSystemUserRecord(user3name, user3FirstName, user3LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser3Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser3Id, 8);

            var user4name = "cpsu_4_" + currentTimeString;
            var user4FirstName = "Care Provider";
            var user4LastName = "System User 4 " + currentTimeString;
            var systemUser4Id = commonMethodsDB.CreateSystemUserRecord(user4name, user4FirstName, user4LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser4Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser4Id, 8);

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContract1Id = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser2EmploymentContract1Id = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser2Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser3EmploymentContract1Id = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser3Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser4EmploymentContract1Id = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser4Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContract1Id, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContract1Id, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContract1Id, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser4EmploymentContract1Id, _bookingType2);

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContract1Id, _bookingType3);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContract1Id, _bookingType3);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser3EmploymentContract1Id, _bookingType3);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser4EmploymentContract1Id, _bookingType3);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContract1Id, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser2Id, _teamId, _systemUser2EmploymentContract1Id, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser3Id, _teamId, _systemUser3EmploymentContract1Id, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser4Id, _teamId, _systemUser4EmploymentContract1Id, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType2, 1, 1, 1, new TimeSpan(8, 0, 0), new TimeSpan(12, 0, 0), providerId, "Express Book Testing");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContract1Id, systemUser1Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser2EmploymentContract1Id, systemUser2Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser3EmploymentContract1Id, systemUser3Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser4EmploymentContract1Id, systemUser4Id);

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

            var nextWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .InsertTextOnExpressBookingStartDate(nextWeekMonday.ToString("dd/MM/yyyy"))
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ClickCloseAndSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
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

            //Validate that we have 1 error logged in the results
            var expressBookingResults = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(0, expressBookingResults.Count);

            //validate that the 1 Diary Bookings are created
            var diaryBookings = dbHelper.cPBookingDiary.GetByCPBookingScheduleId(cpBookingSchedule1Id);
            Assert.AreEqual(1, diaryBookings.Count);

            //booking should have 2 dealocated staff
            var bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookings[0]);
            Assert.AreEqual(4, bookingDiaryStaffRecords.Count);

            var linkedEmploymentContractIds = new List<string>();
            var bookingDiaryStaffRecordFields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            if (bookingDiaryStaffRecordFields.ContainsKey("systemuseremploymentcontractid"))
                linkedEmploymentContractIds.Add(bookingDiaryStaffRecordFields["systemuseremploymentcontractid"].ToString());

            bookingDiaryStaffRecordFields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[1], "systemuseremploymentcontractid");
            if (bookingDiaryStaffRecordFields.ContainsKey("systemuseremploymentcontractid"))
                linkedEmploymentContractIds.Add(bookingDiaryStaffRecordFields["systemuseremploymentcontractid"].ToString());

            bookingDiaryStaffRecordFields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[2], "systemuseremploymentcontractid");
            if (bookingDiaryStaffRecordFields.ContainsKey("systemuseremploymentcontractid"))
                linkedEmploymentContractIds.Add(bookingDiaryStaffRecordFields["systemuseremploymentcontractid"].ToString());

            bookingDiaryStaffRecordFields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[3], "systemuseremploymentcontractid");
            if (bookingDiaryStaffRecordFields.ContainsKey("systemuseremploymentcontractid"))
                linkedEmploymentContractIds.Add(bookingDiaryStaffRecordFields["systemuseremploymentcontractid"].ToString());

            Assert.IsTrue(linkedEmploymentContractIds.Contains(_systemUser1EmploymentContract1Id.ToString()));
            Assert.IsTrue(linkedEmploymentContractIds.Contains(_systemUser2EmploymentContract1Id.ToString()));
            Assert.IsTrue(linkedEmploymentContractIds.Contains(_systemUser3EmploymentContract1Id.ToString()));
            Assert.IsTrue(linkedEmploymentContractIds.Contains(_systemUser4EmploymentContract1Id.ToString()));

            #endregion

        }

        #endregion


    }
}

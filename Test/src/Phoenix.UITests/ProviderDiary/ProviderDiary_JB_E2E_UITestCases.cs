using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phoenix.DBHelper.Models;
using Phoenix.UITests.Framework.PageObjects;
using Phoenix.UITests.Framework.PageObjects.Settings.Configuration.CPFinance;
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
    public class ProviderDiary_JB_E2E_UITestCases : FunctionalTest
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
        internal Guid _recurrencePattern_Occursevery1days;

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

        internal void ExecuteScheduleJob(string ScheduleJobName)
        {
            System.Threading.Thread.Sleep(1000);

            //Get the schedule job id
            Guid financeTransactionTriggerJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName(ScheduleJobName)[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(financeTransactionTriggerJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(financeTransactionTriggerJobId);
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

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("E2E BUA " + currentDateString);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                teamName = "E2E TA " + currentDateString;
                _teamId = commonMethodsDB.CreateTeam(teamName, null, _businessUnitId, "", "E2ETA" + currentDateString + "@careworkstempmail.com", teamName, "020 123456");

                #endregion

                #region Create default system user

                _loginUser_Username = "e2e_user_01";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUser_Username, "End to End", "User 01", "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

                #endregion

                #region Care Provider Scheduling Setup

                var cPSchedulingSetupId = dbHelper.cPSchedulingSetup.GetAllActiveRecords().FirstOrDefault();
                dbHelper.cPSchedulingSetup.UpdateCheckStaffAvailability(cPSchedulingSetupId, 4); //Check and Offer Create
                dbHelper.cPSchedulingSetup.UpdateUseBookingTypeClashActions(cPSchedulingSetupId, true, null); //Use 'Booking Type: Clash Actions' Setting for Clashes with Schedule Bookings

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-9369

        [TestProperty("JiraIssueID", "ACC-9437")]
        [Description("Plan S3")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("BusinessModule2", "Care Provider Payroll")]
        [TestProperty("Screen1", "Provider Diary")]
        [TestProperty("Screen2", "Payroll Batches")]
        [TestProperty("Screen3", "Booking Payments")]
        public void ProviderDiary_End2End_UITestCases001()
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

            var WorkingContractedTime = 1;
            var IsAbsence = false;
            int? capduration = null;
            DateTime? validfromdate = null;
            DateTime? validtodate = null;
            var cpbookingchargetypeid = 1;
            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC E2E T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), WorkingContractedTime, IsAbsence, capduration, validfromdate, validtodate, cpbookingchargetypeid);

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
            var IsScheduledService = true;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, IsScheduledService);

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
            var financetransactionsupto = todayDate.AddMonths(1);
            var separateinvoices = false;

            dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false, careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2023, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true, _teamId);

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Contract Service

            var careProviderContractService1Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService1Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Orion";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2023, 11, 20), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractService1Id, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

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
            _recurrencePattern_Occursevery1days = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").First();

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region System User

            var user1name = "cpsu_" + currentTimeString;
            var user1FirstName = "Care Provider";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid, new List<Guid>(), 3);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser1Id, 40);

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeId, _teamId, _employmentContractTypeid1, 47);
            var _systemUser1EmploymentContractName = (string)dbHelper.systemUserEmploymentContract.GetByID(_systemUser1EmploymentContractId, "name")["name"];

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            //Create 1 Ad-Hoc availability record 2 days in the past
            var pastTwoDaysDate = commonMethodsHelper.GetDatePartWithoutCulture().AddDays(-2);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule("Ad-Hoc", systemUser1Id, _teamId, _recurrencePattern_Occursevery1days, _systemUser1EmploymentContractId, _availabilityTypeId, pastTwoDaysDate, pastTwoDaysDate, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), null, true);

            #endregion

            #region Master Pay Arrangement

            Dictionary<Guid, string> Providers = new Dictionary<Guid, string>();
            Providers.Add(providerId, providerName);

            Dictionary<Guid, string> BookingTypes = new Dictionary<Guid, string>();
            BookingTypes.Add(_bookingType2, "BTC E2E T2");

            Dictionary<Guid, string> SystemUserEmploymentContracts = new Dictionary<Guid, string>();
            SystemUserEmploymentContracts.Add(_systemUser1EmploymentContractId, _systemUser1EmploymentContractName);

            var masterPayArrangementName = "MPA E2E " + currentTimeString;
            var ppayrollunittypeid = 6; //Booking
            var defaultrate = 20.55m;
            var allowforhybridrates = false;
            var ispayscheduledcareonactuals = false;
            var startdate = new DateTime(2024, 1, 1);
            var isdraft = false;

            var isproviderall = false;
            var isemploymentcontracttypeall = true;
            var iscpbookingtypeall = false;
            var iscareproviderstaffroletypeall = true;
            var iscontractschemeall = true;
            var ispersoncontractall = true;
            var issystemuseremploymentcontractall = false;
            var iscptimebandsetall = true;

            var masterPayArrangementId1 = dbHelper.careProviderMasterPayArrangement.CreateRecord(_teamId, masterPayArrangementName
                , ppayrollunittypeid, defaultrate, allowforhybridrates, ispayscheduledcareonactuals, startdate, null, isdraft
                , isproviderall, Providers, isemploymentcontracttypeall, null, iscpbookingtypeall, BookingTypes, iscareproviderstaffroletypeall, null
                , iscontractschemeall, null, ispersoncontractall, issystemuseremploymentcontractall, SystemUserEmploymentContracts, iscptimebandsetall, null);

            #endregion



            #region Part 1 - Create and confirm the booking & validate staff cost

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            var startYear = pastTwoDaysDate.Year.ToString();
            var startMonth = pastTwoDaysDate.ToString("MMMM");
            var startDay = pastTwoDaysDate.Day.ToString();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(startYear, startMonth, startDay)
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .SelectBookingType("BTC E2E T2")
                .SelectStartDate(startYear, startMonth, startDay)
                .SelectEndDate(startYear, startMonth, startDay)
                .InsertStartTime("07", "00")
                .InsertEndTime("08", "00")
                .ClickSelectPeopleButton();

            selectMultiplePeoplePopup
                .WaitForSelectMultiplePeopleAreaToLoad()
                .ClickOnRecordCheckbox(_personcontractId)
                .ClickConfirmSelectionButton();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickOnStaffTab();

            createDiaryBookingPopup
                .WaitForStaffTabToLoad()
                .ValidateBookingCost_StaffTab(_systemUser1EmploymentContractId, "20.55")
                .ValidateBreakTime_StaffTab(_systemUser1EmploymentContractId, "0")
                .ValidatePaidHours_StaffTab(_systemUser1EmploymentContractId, "1 hours")
                .ValidateMasterPayArrangements_StaffTab(_systemUser1EmploymentContractId, masterPayArrangementName);

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();

            System.Threading.Thread.Sleep(4000);

            var diaryRecords = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(1, diaryRecords.Count);
            var cpBookingDiaryId = diaryRecords.First();

            providerDiaryPage
                .RightClickDiaryBooking(cpBookingDiaryId)
                .WaitForContextMenuToLoad()
                .ClickConfirmAllButton_ContextMenu();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();

            changeBookingStaffStatusDialogPopup
                .WaitForPopupToLoad()
                .ValidateStaffCostsCheckmarkDisplayed()
                .ValidateStaffCostsMessage("Staff costs calculated for this booking.")
                .ClickSaveChangesButton();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();

            #endregion

            #region Part 2 - Run Payroll Batch

            ExecuteScheduleJob("Process CP Diary Bookings (Payroll)");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPayrollBatchesSection();

            payrollBatchesPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            payrollBatchRecordPage
                .WaitForPageToLoad()
                .ClickProvidersLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery(providerName).TapSearchButton().SelectResultElement(providerId);

            payrollBatchRecordPage
                .WaitForPageToLoad()
                .ClickApplyToAnyEmployee_YesRadioButton()
                .InsertTextOnStartDate(pastTwoDaysDate.ToString("dd/MM/yyyy"))
                .InsertTextOnEndDate(DateTime.Now.ToString("dd/MM/yyyy"))
                .SelectBookingDateCriteria("Bookings ended before end date of payroll batch")
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("The Booking Date Criteria field cannot be changed after the Payroll Batch has been saved and Booking Payments are allocated")
                .TapOKButton();

            payrollBatchesPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var payrollBatchRecords = dbHelper.careProviderPayrollBatch.GetByProviderId(providerId);
            Assert.AreEqual(1, payrollBatchRecords.Count);
            var payrollBatchId = payrollBatchRecords[0];

            payrollBatchesPage
                .OpenRecord(payrollBatchId);

            payrollBatchRecordPage
                .WaitForPageToLoad()
                .ClickRunPayrollBatchButton()
                .WaitForPageToLoad()
                .ClickBookingPaymentsTab();

            bookingPaymentsPage
                .WaitForPageToLoadFromPayrollBatch();

            var bookingPaymentRecords = dbHelper.cpSystemUserShiftPayment.GetByPayrollBatchId(payrollBatchId);
            Assert.AreEqual(1, bookingPaymentRecords.Count);
            var bookingPaymentId = bookingPaymentRecords[0];

            bookingPaymentsPage
                .OpenRecord(bookingPaymentId);

            bookingPaymentRecordPage
                .WaitForPageToLoad()
                .ValidateEmployeeLinkText(user1FirstName + " " + user1LastName)
                .ValidateProviderLinkText(providerName)
                .ValidateRoleLinkText("Helper")
                .ValidateGrossAmountText("20.55")
                .ValidateStartDateText(pastTwoDaysDate.ToString("dd/MM/yyyy"))
                .ValidateStartDate_TimeText("07:00")
                .ValidateEndDateText(pastTwoDaysDate.ToString("dd/MM/yyyy"))
                .ValidateEndDate_TimeText("08:00")
                .ValidatePaidHoursText("1.00");

            #endregion

            #region Part 3 - Create additional bookings for the same provider and staff member

            #region Diary Booking

            var cpBookingDiary2Id = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType2, providerId, pastTwoDaysDate, new TimeSpan(9, 0, 0), pastTwoDaysDate, new TimeSpan(10, 0, 0));
            var cpBookingDiary3Id = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType2, providerId, pastTwoDaysDate, new TimeSpan(11, 0, 0), pastTwoDaysDate, new TimeSpan(12, 0, 0));

            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", cpBookingDiary2Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", cpBookingDiary3Id, _systemUser1EmploymentContractId, systemUser1Id);

            dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_teamId, _businessUnitId, "title", cpBookingDiary2Id, _personID, _personcontractId);
            dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_teamId, _businessUnitId, "title", cpBookingDiary3Id, _personID, _personcontractId);

            dbHelper.cPBookingDiary.UpdateConfirmed(cpBookingDiary2Id, true);
            dbHelper.cPBookingDiary.UpdateConfirmed(cpBookingDiary3Id, true);

            ExecuteScheduleJob("Process CP Diary Bookings (Payroll)");

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPayrollBatchesSection();

            payrollBatchesPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            payrollBatchRecordPage
                .WaitForPageToLoad()
                .ClickProvidersLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery(providerName).TapSearchButton().SelectResultElement(providerId);

            payrollBatchRecordPage
                .WaitForPageToLoad()
                .ClickApplyToAnyEmployee_YesRadioButton()
                .InsertTextOnStartDate(pastTwoDaysDate.ToString("dd/MM/yyyy"))
                .InsertTextOnEndDate(DateTime.Now.ToString("dd/MM/yyyy"))
                .SelectBookingDateCriteria("Bookings ended before end date of payroll batch")
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("The Booking Date Criteria field cannot be changed after the Payroll Batch has been saved and Booking Payments are allocated")
                .TapOKButton();

            payrollBatchesPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            payrollBatchRecords = dbHelper.careProviderPayrollBatch.GetByProviderId(providerId);
            Assert.AreEqual(2, payrollBatchRecords.Count);
            var payrollBatch2Id = payrollBatchRecords.Where(c=>c != payrollBatchId).First();

            payrollBatchesPage
                .OpenRecord(payrollBatch2Id);

            payrollBatchRecordPage
                .WaitForPageToLoad()
                .ClickRunPayrollBatchButton()
                .WaitForPageToLoad()
                .ClickBookingPaymentsTab();

            bookingPaymentsPage
                .WaitForPageToLoadFromPayrollBatch();

            bookingPaymentRecords = dbHelper.cpSystemUserShiftPayment.GetByPayrollBatchId(payrollBatch2Id);
            Assert.AreEqual(2, bookingPaymentRecords.Count);
            var bookingPayment2Id = bookingPaymentRecords[0];
            var bookingPayment3Id = bookingPaymentRecords[1];

            bookingPaymentsPage
                .OpenRecord(bookingPayment2Id);

            bookingPaymentRecordPage
                .WaitForPageToLoad()
                .ValidateEmployeeLinkText(user1FirstName + " " + user1LastName)
                .ValidateProviderLinkText(providerName)
                .ValidateRoleLinkText("Helper")
                .ValidateGrossAmountText("20.55")
                .ValidateStartDateText(pastTwoDaysDate.ToString("dd/MM/yyyy"))
                .ValidateEndDateText(pastTwoDaysDate.ToString("dd/MM/yyyy"))
                .ValidatePaidHoursText("1.00")
                .ClickBackButton();

            bookingPaymentsPage
                .WaitForPageToLoadFromPayrollBatch()
                .OpenRecord(bookingPayment3Id);

            bookingPaymentRecordPage
                .WaitForPageToLoad()
                .ValidateEmployeeLinkText(user1FirstName + " " + user1LastName)
                .ValidateProviderLinkText(providerName)
                .ValidateRoleLinkText("Helper")
                .ValidateGrossAmountText("20.55")
                .ValidateStartDateText(pastTwoDaysDate.ToString("dd/MM/yyyy"))
                .ValidateEndDateText(pastTwoDaysDate.ToString("dd/MM/yyyy"))
                .ValidatePaidHoursText("1.00")
                .ClickBackButton();

            #endregion

        }

        #endregion

    }
}

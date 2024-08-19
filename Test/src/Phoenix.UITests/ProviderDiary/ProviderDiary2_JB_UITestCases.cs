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
    public class ProviderDIary2_JB_UITestCases : FunctionalTest
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
        private string currentDateTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");
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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("PD BU " + currentDateString);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                teamName = "PD T " + currentDateString;
                _teamId = commonMethodsDB.CreateTeam(teamName, null, _businessUnitId, "90400", "PDT" + currentDateString + "@careworkstempmail.com", teamName, "020 123456");

                #endregion

                #region Create default system user

                System.Threading.Thread.Sleep(3000);
                _loginUser_Username = "PDU_" + currentDateTimeString;
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUser_Username, "ProviderDiary", "User" + currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

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

        #region https://advancedcsg.atlassian.net/browse/ACC-7961

        [TestProperty("JiraIssueID", "ACC-8114")]
        [Description("Step(s) 4 from the original test - Create new booking")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderDiary_ACC_7708_UITestCases001()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var nextWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);

            #region Provider

            var providerName = "Provider " + currentDateTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentDateTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-7708 A", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType2, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentDateTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentDateTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, true);

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMapping1Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType2, null, "");

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentDateTimeString;
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
            var lastName = currentDateTimeString;
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

            var user1name = "George_" + currentDateTimeString;
            var user1FirstName = "George";
            var user1LastName = currentDateTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user2name = "Jearldine_" + currentDateTimeString;
            var user2FirstName = "Jearldine";
            var user2LastName = currentDateTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user3name = "Bethany_" + currentDateTimeString;
            var user3FirstName = "Bethany";
            var user3LastName = currentDateTimeString;
            var systemUser3Id = commonMethodsDB.CreateSystemUserRecord(user3name, user3FirstName, user3LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user4name = "John_" + currentDateTimeString;
            var user4FirstName = "John";
            var user4LastName = currentDateTimeString;
            var systemUser4Id = commonMethodsDB.CreateSystemUserRecord(user4name, user4FirstName, user4LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user5name = "Jack_" + currentDateTimeString;
            var user5FirstName = "Jack";
            var user5LastName = currentDateTimeString;
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
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(nextWeekMonday.ToString("yyyy"), nextWeekMonday.ToString("MMMM"), nextWeekMonday.Day.ToString())
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .SelectBookingType("BTC ACC-7708 A")
                .SelectStartDate(nextWeekMonday.Year.ToString(), nextWeekMonday.ToString("MMMM"), nextWeekMonday.Day.ToString())
                .SelectEndDate(nextWeekMonday.Year.ToString(), nextWeekMonday.ToString("MMMM"), nextWeekMonday.Day.ToString())
                .InsertStartTime("07", "00")
                .InsertEndTime("08", "00")
                .ClickSelectPeopleButton();

            selectMultiplePeoplePopUp
                .WaitForSelectMultiplePeoplePopUpPageToLoad()
                .ClickPeopleRecordCellText(_personcontractId.ToString())
                .ClickConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .ClickOnlyShowAvailableStaff()
                .EnterTextIntoFilterStaffByNameSearchBox(currentDateTimeString)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser2EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser3EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser4EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser5EmploymentContractId);

            //before saving we set the end date for Jhon (otherwise we cannot select this contract on the booking screen)
            dbHelper.systemUserEmploymentContract.UpdateEndDate(_systemUser4EmploymentContractId, commonMethodsHelper.GetDatePartWithoutCulture().AddHours(23).AddMinutes(55), contractEndReasonId);

            selectStaffPopup
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking()
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("George " + currentDateTimeString + " - Helper contract is not started at the selected booking time.", 1)
                .ValidateMessage_DynamicDialogue("Jearldine " + currentDateTimeString + " - Helper contract is not valid before " + commonMethodsHelper.GetDatePartWithoutCulture().AddMonths(2).ToString("dd/MM/yyyy 00:00:00") + ".", 2)
                .ValidateMessage_DynamicDialogue("John " + currentDateTimeString + " - Helper contract is not valid after " + commonMethodsHelper.GetDatePartWithoutCulture().AddHours(23).AddMinutes(55).ToString("dd/MM/yyyy HH:mm:ss") + ".", 3)
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .RemoveStaffFromSelectedStaffField(_systemUser1EmploymentContractId.ToString())
                .RemoveStaffFromSelectedStaffField(_systemUser2EmploymentContractId.ToString())
                .RemoveStaffFromSelectedStaffField(_systemUser4EmploymentContractId.ToString())
                .ClickCreateBooking()
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("Bethany " + currentDateTimeString + " - Cook contract is suspended at the selected booking time.", 1)
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .RemoveStaffFromSelectedStaffField(_systemUser3EmploymentContractId.ToString())
                .ClickCreateBooking();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();

            System.Threading.Thread.Sleep(4000);

            var bookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(1, bookings.Count);
            var booking1Id = bookings[0];

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-8117")]
        [Description("Step(s) 4 from the original test - Context menu Copy To")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderDiary_ACC_7708_UITestCases002()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "Provider " + currentDateTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentDateTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-7708 A", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType2, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentDateTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentDateTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, true);

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMapping1Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType2, null, "");

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentDateTimeString;
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
            var lastName = currentDateTimeString;
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

            var user1name = "George_" + currentDateTimeString;
            var user1FirstName = "George";
            var user1LastName = currentDateTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user2name = "Jearldine_" + currentDateTimeString;
            var user2FirstName = "Jearldine";
            var user2LastName = currentDateTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user3name = "Bethany_" + currentDateTimeString;
            var user3FirstName = "Bethany";
            var user3LastName = currentDateTimeString;
            var systemUser3Id = commonMethodsDB.CreateSystemUserRecord(user3name, user3FirstName, user3LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user4name = "John_" + currentDateTimeString;
            var user4FirstName = "John";
            var user4LastName = currentDateTimeString;
            var systemUser4Id = commonMethodsDB.CreateSystemUserRecord(user4name, user4FirstName, user4LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user5name = "Jack_" + currentDateTimeString;
            var user5FirstName = "Jack";
            var user5LastName = currentDateTimeString;
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
            dbHelper.systemUserEmploymentContract.UpdateEndDate(_systemUser4EmploymentContractId, commonMethodsHelper.GetDatePartWithoutCulture().AddHours(23).AddMinutes(55), contractEndReasonId);

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

            #region Diary Booking

            var nextWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);

            var booking1Id = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType2, providerId, nextWeekMonday, new TimeSpan(7, 0, 0), nextWeekMonday, new TimeSpan(8, 0, 0));

            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", booking1Id, _systemUser5EmploymentContractId, systemUser5Id);

            dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_teamId, booking1Id, _personID, _personcontractId, careProviderContractServiceId);

            #endregion



            #region Step 4

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(nextWeekMonday.ToString("yyyy"), nextWeekMonday.ToString("MMMM"), nextWeekMonday.Day.ToString())
                .WaitForProviderDiaryPageToLoad()
                .RightClickDiaryBooking(booking1Id)
                .WaitForContextMenuToLoad()
                .ClickCopyToButton_ContextMenu();

            copyBookingToDialogPopup
                .WaitForPopupToLoad()
                .SelectEmploymentContracts(_systemUser3EmploymentContractId.ToString())
                .ClickCopyButton();

            wallchartWarningDialogPopup
                .WaitForWallchartErrorDialogPopupToLoad()
                .ValidateMessageLine(1, "Booking:")
                .ValidateMessageLine(2, "BTC ACC-7708 A - diary booking starting at 07:00 on " + nextWeekMonday.ToString("dd/MM/yyyy") + " at Provider " + currentDateTimeString + " assigned to Bethany " + currentDateTimeString + ".")
                .ValidateMessageLine(3, "Issue:")
                .ValidateBulletlistLine(0, "Bethany " + currentDateTimeString + " - Cook contract is suspended at the selected booking time.")
                .ClickCancelButton();

            System.Threading.Thread.Sleep(2000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderDiaryPageToLoad();

            var bookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(1, bookings.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-8119")]
        [Description("Step(s) 4 from the original test - Context menu Specify Care Worker")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderDiary_ACC_7708_UITestCases003()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "Provider " + currentDateTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentDateTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-7708 A", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType2, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentDateTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentDateTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, true);

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMapping1Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType2, null, "");

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentDateTimeString;
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
            var lastName = currentDateTimeString;
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

            var user1name = "George_" + currentDateTimeString;
            var user1FirstName = "George";
            var user1LastName = currentDateTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user2name = "Jearldine_" + currentDateTimeString;
            var user2FirstName = "Jearldine";
            var user2LastName = currentDateTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user3name = "Bethany_" + currentDateTimeString;
            var user3FirstName = "Bethany";
            var user3LastName = currentDateTimeString;
            var systemUser3Id = commonMethodsDB.CreateSystemUserRecord(user3name, user3FirstName, user3LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user4name = "John_" + currentDateTimeString;
            var user4FirstName = "John";
            var user4LastName = currentDateTimeString;
            var systemUser4Id = commonMethodsDB.CreateSystemUserRecord(user4name, user4FirstName, user4LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user5name = "Jack_" + currentDateTimeString;
            var user5FirstName = "Jack";
            var user5LastName = currentDateTimeString;
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

            #region Diary Booking

            var nextWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);

            var booking1Id = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType2, providerId, nextWeekMonday, new TimeSpan(7, 0, 0), nextWeekMonday, new TimeSpan(8, 0, 0));

            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", booking1Id, _systemUser5EmploymentContractId, systemUser5Id);

            dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_teamId, booking1Id, _personID, _personcontractId, careProviderContractServiceId);

            #endregion

            #region Step 4

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(nextWeekMonday.ToString("yyyy"), nextWeekMonday.ToString("MMMM"), nextWeekMonday.Day.ToString())
                .WaitForProviderDiaryPageToLoad()
                .RightClickDiaryBooking(booking1Id)
                .WaitForContextMenuToLoad()
                .ClickSpecifyCareWorkerButton_ContextMenu();

            specifyCareWorkerPopup
                .WaitForPopupToLoad()
                .SelectCareWorker(currentDateTimeString, _systemUser1EmploymentContractId)
                .ClickSaveChangesButton();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();

            wallchartWarningDialogPopup
                .WaitForWallchartErrorDialogPopupToLoad()
                .ValidateMessageLine(1, "Booking:")
                .ValidateMessageLine(2, "BTC ACC-7708 A - diary booking starting at 07:00 on " + nextWeekMonday.ToString("dd/MM/yyyy") + " at Provider " + currentDateTimeString + " assigned to Jack " + currentDateTimeString + ".")
                .ValidateMessageLine(3, "Issue:")
                .ValidateBulletlistLine(0, "George " + currentDateTimeString + " - Helper contract is not started at the selected booking time.")
                .ClickCancelButton();

            System.Threading.Thread.Sleep(2000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderDiaryPageToLoad()
                .RightClickDiaryBooking(booking1Id)
                .WaitForContextMenuToLoad()
                .ClickSpecifyCareWorkerButton_ContextMenu();

            specifyCareWorkerPopup
                .WaitForPopupToLoad()
                .SelectCareWorker(currentDateTimeString, _systemUser2EmploymentContractId)
                .ClickSaveChangesButton();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();

            wallchartWarningDialogPopup
                .WaitForWallchartErrorDialogPopupToLoad()
                .ValidateMessageLine(1, "Booking:")
                .ValidateMessageLine(2, "BTC ACC-7708 A - diary booking starting at 07:00 on " + nextWeekMonday.ToString("dd/MM/yyyy") + " at Provider " + currentDateTimeString + " assigned to Jack " + currentDateTimeString + ".")
                .ValidateMessageLine(3, "Issue:")
                .ValidateBulletlistLine(0, "Jearldine " + currentDateTimeString + " - Helper contract is not valid before " + commonMethodsHelper.GetDatePartWithoutCulture().AddMonths(2).ToString("dd/MM/yyyy") + " 00:00:00.")
                .ClickCancelButton();

            System.Threading.Thread.Sleep(2000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderDiaryPageToLoad()
                .RightClickDiaryBooking(booking1Id)
                .WaitForContextMenuToLoad()
                .ClickSpecifyCareWorkerButton_ContextMenu();

            specifyCareWorkerPopup
                .WaitForPopupToLoad()
                .SelectCareWorker(currentDateTimeString, _systemUser3EmploymentContractId)
                .ClickSaveChangesButton();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();

            wallchartWarningDialogPopup
                .WaitForWallchartErrorDialogPopupToLoad()
                .ValidateMessageLine(1, "Booking:")
                .ValidateMessageLine(2, "BTC ACC-7708 A - diary booking starting at 07:00 on " + nextWeekMonday.ToString("dd/MM/yyyy") + " at Provider " + currentDateTimeString + " assigned to Jack " + currentDateTimeString + ".")
                .ValidateMessageLine(3, "Issue:")
                .ValidateBulletlistLine(0, "Bethany " + currentDateTimeString + " - Cook contract is suspended at the selected booking time.")
                .ClickCancelButton();

            System.Threading.Thread.Sleep(2000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderDiaryPageToLoad()
                .RightClickDiaryBooking(booking1Id)
                .WaitForContextMenuToLoad()
                .ClickSpecifyCareWorkerButton_ContextMenu();

            //here we set an end date for Jhon contract
            var contractEndDate = commonMethodsHelper.GetDatePartWithoutCulture().AddHours(23).AddMinutes(55);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(_systemUser4EmploymentContractId, contractEndDate, contractEndReasonId);

            specifyCareWorkerPopup
                .WaitForPopupToLoad()
                .SelectCareWorker(currentDateTimeString, _systemUser4EmploymentContractId)
                .ClickSaveChangesButton();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();

            wallchartWarningDialogPopup
                .WaitForWallchartErrorDialogPopupToLoad()
                .ValidateMessageLine(1, "Booking:")
                .ValidateMessageLine(2, "BTC ACC-7708 A - diary booking starting at 07:00 on " + nextWeekMonday.ToString("dd/MM/yyyy") + " at Provider " + currentDateTimeString + " assigned to Jack " + currentDateTimeString + ".")
                .ValidateMessageLine(3, "Issue:")
                .ValidateBulletlistLine(0, "John " + currentDateTimeString + " - Helper contract is not valid after " + contractEndDate.ToString("dd/MM/yyyy") + " 23:55:00.")
                .ClickCancelButton();

            System.Threading.Thread.Sleep(2000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderDiaryPageToLoad();

            var bookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(1, bookings.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-8120")]
        [Description("Step(s) 4 from the original test - Drag booking to another staff member")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderDiary_ACC_7708_UITestCases004()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "Provider " + currentDateTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentDateTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-7708 A", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType2, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentDateTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentDateTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, true);

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMapping1Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType2, null, "");

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentDateTimeString;
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
            var lastName = currentDateTimeString;
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

            var user1name = "George_" + currentDateTimeString;
            var user1FirstName = "George";
            var user1LastName = currentDateTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user2name = "Jearldine_" + currentDateTimeString;
            var user2FirstName = "Jearldine";
            var user2LastName = currentDateTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user3name = "Bethany_" + currentDateTimeString;
            var user3FirstName = "Bethany";
            var user3LastName = currentDateTimeString;
            var systemUser3Id = commonMethodsDB.CreateSystemUserRecord(user3name, user3FirstName, user3LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user4name = "John_" + currentDateTimeString;
            var user4FirstName = "John";
            var user4LastName = currentDateTimeString;
            var systemUser4Id = commonMethodsDB.CreateSystemUserRecord(user4name, user4FirstName, user4LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user5name = "Jack_" + currentDateTimeString;
            var user5FirstName = "Jack";
            var user5LastName = currentDateTimeString;
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

            #region Diary Booking

            var nextWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7);

            var booking1Id = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType2, providerId, nextWeekMonday, new TimeSpan(11, 30, 0), nextWeekMonday, new TimeSpan(12, 30, 0));

            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", booking1Id, _systemUser5EmploymentContractId, systemUser5Id);

            dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_teamId, booking1Id, _personID, _personcontractId, careProviderContractServiceId);

            #endregion

            #region Step 4

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(nextWeekMonday.ToString("yyyy"), nextWeekMonday.ToString("MMMM"), nextWeekMonday.Day.ToString())
                .WaitForProviderDiaryPageToLoad()
                .DragBookingToAnotherStaffMember(booking1Id, _systemUser3EmploymentContractId);

            wallchartWarningDialogPopup
                .WaitForWallchartErrorDialogPopupToLoad()
                .ValidateMessageLine(1, "Booking:")
                .ValidateMessageLine(2, "BTC ACC-7708 A - diary booking starting at 11:30 on " + nextWeekMonday.ToString("dd/MM/yyyy") + " at Provider " + currentDateTimeString + " assigned to Jack " + currentDateTimeString + ".")
                .ValidateMessageLine(3, "Issue:")
                .ValidateBulletlistLine(0, "Bethany " + currentDateTimeString + " - Cook contract is suspended at the selected booking time.")
                .ClickCancelButton();

            System.Threading.Thread.Sleep(2000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderDiaryPageToLoad();

            var bookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(1, bookings.Count);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-8530

        [TestProperty("JiraIssueID", "ACC-8617")]
        [Description("Scenario where we express book a schedule with 1 Person and 1 Staff Member")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderDiary_ACC_8527_UITestCases001()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType5 = commonMethodsDB.CreateCPBookingType("BTC ACC-8527 T5", 5, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType5, false);

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

            var careProviderServiceMapping1Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType5, null, "");

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

            var careProviderContractService1Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType5, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService1Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Matheo";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontract1Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontract1Id, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractService1Id, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 8527", "98910", null, new DateTime(2020, 1, 1), null);

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

            var user1name = "su_" + currentTimeString;
            var user1FirstName = "System User";
            var user1LastName = currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser1Id, 40);

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType5);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType5, 1, 1, 1, new TimeSpan(8, 0, 0), new TimeSpan(9, 0, 0), providerId, "Express Book Testing");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontract1Id, careProviderContractService1Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);

            #endregion

            #region Express Book for Provider

            DateTime bookingStartDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7); //Monday of next week
            DateTime bookingEndDate = bookingStartDate.AddDays(6);
            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, providerId, bookingStartDate, bookingEndDate, commonMethodsHelper.GetCurrentDateWithoutCulture(), providerId, "provider", providerName);
            System.Threading.Thread.Sleep(2000);

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


            #region Steps 1 to 10

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .SearchProviderRecord(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(bookingStartDate.ToString("yyyy"), bookingStartDate.ToString("MMMM"), bookingStartDate.Day.ToString())
                .WaitForProviderDiaryPageToLoad();

            var allDiaryBookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(1, allDiaryBookings.Count);
            var diaryBookingId = allDiaryBookings[0];

            var allDiaryBookingToPeople = dbHelper.diaryBookingToPeople.GetByDiaryId(diaryBookingId);
            Assert.AreEqual(1, allDiaryBookingToPeople.Count);
            var diaryBookingToPeopleId = allDiaryBookingToPeople[0];

            var allDiaryBookingToStaff = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookingId);
            Assert.AreEqual(1, allDiaryBookingToStaff.Count);
            var diaryBookingToStaffId = allDiaryBookingToStaff[0];

            providerDiaryPage
                .ClickDiaryBooking(diaryBookingId);

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .VerifySelectedStaffRecordInStaffForBookingIsDisplayed(_systemUser1EmploymentContractId.ToString(), user1FirstName + " " + user1LastName, true)
                .RemoveStaffFromSelectedStaffField(_systemUser1EmploymentContractId.ToString())
                .ClickCreateBooking();

            createDiaryBookingPopup
              .WaitForDynamicDialogueToLoad()
              .ValidateMessage_DynamicDialogue("You need to have at least 1 staff member for this booking type.")
              .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ClickAddUnassignedStaff()
                .ClickCreateBooking();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .SearchProviderRecord(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(diaryBookingId);

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .VerifySelectedStaffRecordInStaffForBookingIsDisplayed("0", "Unassigned", true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-8618")]
        [Description("Scenario where we express book a schedule with 1 Person and 2 Staff Members")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderDiary_ACC_8527_UITestCases002()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType5 = commonMethodsDB.CreateCPBookingType("BTC ACC-8527 T5", 5, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType5, false);

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

            var careProviderServiceMapping1Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType5, null, "");

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

            var careProviderContractService1Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType5, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService1Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Matheo";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontract1Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontract1Id, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractService1Id, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 8527", "98910", null, new DateTime(2020, 1, 1), null);

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

            var user1name = "su_a_" + currentTimeString;
            var user1FirstName = "System User A";
            var user1LastName = currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user2name = "su_b_" + currentTimeString;
            var user2FirstName = "System User B";
            var user2LastName = currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser2Id, commonMethodsHelper.GetThisWeekFirstMonday());

            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser1Id, 40);
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser2Id, 40);

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser2EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser2Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType5);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingType5);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser2Id, _teamId, _systemUser2EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType5, 1, 1, 1, new TimeSpan(8, 0, 0), new TimeSpan(9, 0, 0), providerId, "Express Book Testing");

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingSchedule1Id, _personID, _personcontract1Id, careProviderContractService1Id);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUser2EmploymentContractId, systemUser2Id);

            #endregion

            #region Express Book for Provider

            DateTime bookingStartDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7); //Monday of next week
            DateTime bookingEndDate = bookingStartDate.AddDays(6);
            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, providerId, bookingStartDate, bookingEndDate, commonMethodsHelper.GetCurrentDateWithoutCulture(), providerId, "provider", providerName);
            System.Threading.Thread.Sleep(2000);

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


            #region Steps 1 to 10

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .SearchProviderRecord(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(bookingStartDate.ToString("yyyy"), bookingStartDate.ToString("MMMM"), bookingStartDate.Day.ToString())
                .WaitForProviderDiaryPageToLoad();

            var allDiaryBookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(1, allDiaryBookings.Count);
            var diaryBookingId = allDiaryBookings[0];

            var allDiaryBookingToPeople = dbHelper.diaryBookingToPeople.GetByDiaryId(diaryBookingId);
            Assert.AreEqual(1, allDiaryBookingToPeople.Count);
            var diaryBookingToPeopleId = allDiaryBookingToPeople[0];

            var allDiaryBookingToStaff = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(diaryBookingId);
            Assert.AreEqual(2, allDiaryBookingToStaff.Count);
            var diaryBookingToStaff1Id = allDiaryBookingToStaff[0];
            var diaryBookingToStaff2Id = allDiaryBookingToStaff[1];

            providerDiaryPage
                .ClickDiaryBooking(diaryBookingId);

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .VerifySelectedStaffRecordInStaffForBookingIsDisplayed(_systemUser1EmploymentContractId.ToString(), user1FirstName + " " + user1LastName, true)
                .VerifySelectedStaffRecordInStaffForBookingIsDisplayed(_systemUser2EmploymentContractId.ToString(), user2FirstName + " " + user2LastName, true)
                .RemoveStaffFromSelectedStaffField(_systemUser1EmploymentContractId.ToString())
                .RemoveStaffFromSelectedStaffField(_systemUser2EmploymentContractId.ToString())
                .ClickCreateBooking();

            createDiaryBookingPopup
              .WaitForDynamicDialogueToLoad()
              .ValidateMessage_DynamicDialogue("You need to have at least 1 staff member for this booking type.")
              .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ClickAddUnassignedStaff()
                .ClickCreateBooking();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .SearchProviderRecord(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(diaryBookingId);

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .VerifySelectedStaffRecordInStaffForBookingIsDisplayed("0", "Unassigned", true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-8619")]
        [Description("Scenario where we create a dirary booking directly and then remove the staff members")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderDiary_ACC_8527_UITestCases003()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType5 = commonMethodsDB.CreateCPBookingType("BTC ACC-8527 T5", 5, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType5, false);

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

            var careProviderServiceMapping1Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType5, null, "");

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

            var careProviderContractService1Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType5, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractService1Id, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName1 = "Matheo";
            var lastName1 = currentTimeString;
            var _person1ID = dbHelper.person.CreatePersonRecord("", firstName1, "", lastName1, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            var firstName2 = "Himdal";
            var lastName2 = currentTimeString;
            var _person2ID = dbHelper.person.CreatePersonRecord("", firstName2, "", lastName2, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontract1Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _person1ID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);
            var _personcontract2Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _person2ID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontract1Id, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractService1Id, todayDate, 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontract2Id, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractService1Id, todayDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 8527", "98910", null, new DateTime(2020, 1, 1), null);

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

            var user1name = "su_a_" + currentTimeString;
            var user1FirstName = "System User A";
            var user1LastName = currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user2name = "su_b_" + currentTimeString;
            var user2FirstName = "System User B";
            var user2LastName = currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser2Id, commonMethodsHelper.GetThisWeekFirstMonday());

            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser1Id, 40);
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser2Id, 40);

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            var _systemUser2EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser2Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType5);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser2EmploymentContractId, _bookingType5);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(systemUser2Id, _teamId, _systemUser2EmploymentContractId, _availabilityTypeId);

            #endregion



            #region Steps 1 to 10

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            DateTime bookingStartDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7); //Monday of next week
            var startYear = bookingStartDate.Year.ToString();
            var startMonth = bookingStartDate.ToString("MMMM");
            var startDay = bookingStartDate.Day.ToString();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .SearchProviderRecord(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(bookingStartDate.ToString("yyyy"), bookingStartDate.ToString("MMMM"), bookingStartDate.Day.ToString())
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()

                .SelectBookingType("BTC ACC-8527 T5")
                .SelectEndDate(startYear, startMonth, startDay)
                .SelectStartDate(startYear, startMonth, startDay)
                .InsertStartTime("08", "00")
                .InsertEndTime("09", "00")
                .ClickSelectPeopleButton();

            selectMultiplePeoplePopup
                .WaitForSelectMultiplePeopleAreaToLoad()
                .ClickOnRecordCheckbox(_personcontract1Id)
                .ClickOnRecordCheckbox(_personcontract2Id)
                .ClickConfirmSelectionButton();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(currentTimeString)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffRecordCellText(_systemUser2EmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            createDiaryBookingPopup
              .WaitForDynamicDialogueToLoad()
              .ValidateMessage_DynamicDialogue("Only one person is allowed for this booking type.")
              .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .RemoveSelectedPerson(_personcontract2Id)
                .ClickCreateBooking();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .SearchProviderRecord(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderDiaryPageToLoad();

            var allDiaryBookings = dbHelper.cPBookingDiary.GetByLocationId(providerId);
            Assert.AreEqual(1, allDiaryBookings.Count);
            var diaryBookingId = allDiaryBookings[0];

            providerDiaryPage
                .ClickDiaryBooking(diaryBookingId);

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .VerifySelectedStaffRecordInStaffForBookingIsDisplayed(_systemUser1EmploymentContractId.ToString(), user1FirstName + " " + user1LastName, true)
                .VerifySelectedStaffRecordInStaffForBookingIsDisplayed(_systemUser2EmploymentContractId.ToString(), user2FirstName + " " + user2LastName, true)
                .RemoveStaffFromSelectedStaffField(_systemUser1EmploymentContractId.ToString())
                .RemoveStaffFromSelectedStaffField(_systemUser2EmploymentContractId.ToString())
                .ClickCreateBooking();

            createDiaryBookingPopup
              .WaitForDynamicDialogueToLoad()
              .ValidateMessage_DynamicDialogue("You need to have at least 1 staff member for this booking type.")
              .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .ClickAddUnassignedStaff()
                .ClickCreateBooking();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .SearchProviderRecord(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderDiaryPageToLoad()
                .ClickDiaryBooking(diaryBookingId);

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .VerifySelectedStaffRecordInStaffForBookingIsDisplayed("0", "Unassigned", true);

            #endregion

        }

        #endregion

    }
}

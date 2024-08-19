using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.ProviderDiary
{
    /// <summary>
    /// This class contains Automated UI test scripts for Provider Diary - Contract Validations.
    /// </summary>
    [TestClass]
    public class ProviderDiaryBooking_ACC_6578_UITestCases : FunctionalTest
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
        private Guid cpSchedulingSetupId;

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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("Care Providers CV");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                teamName = "Care Providers CV";
                _teamId = commonMethodsDB.CreateTeam(teamName, null, _businessUnitId, "90400", "CareProvidersCV@careworkstempmail.com", teamName, "020 123456");

                #endregion

                #region Create default system user

                _loginUser_Username = "ProviderScheduleUser6578";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUser_Username, "ProviderSchedule", "User6578", "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

                #endregion

                #region Care Provider Scheduling Setup

                cpSchedulingSetupId = dbHelper.cPSchedulingSetup.GetAllActiveRecords().FirstOrDefault();
                dbHelper.cPSchedulingSetup.UpdateCheckStaffAvailability(cpSchedulingSetupId, 4); //Check and Offer Create
                dbHelper.cPSchedulingSetup.UpdateUpdateBookingEndDayDateTime(cpSchedulingSetupId, true);
                dbHelper.cPSchedulingSetup.UpdateContractedField(cpSchedulingSetupId, 1); //Check
                dbHelper.cPSchedulingSetup.UpdateSalariedField(cpSchedulingSetupId, 1); //Check

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }


        #region https://advancedcsg.atlassian.net/browse/ACC-6580

        [TestProperty("JiraIssueID", "ACC-6590")]
        [Description("Step(s) 1 to 9 from the original test ACC-6578")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderDiary_ACC_6578_UITestCases001()
        {

            dbHelper.cPSchedulingSetup.UpdateContractedField(cpSchedulingSetupId, 1); //Check

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "P6590 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType5Name = "BTC5 6590";
            var _bookingType5Id = commonMethodsDB.CreateCPBookingType(_bookingType5Name, 5, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, null, null, null, null, 3);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType5Id, true);

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

            var careProviderServiceMapping1Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType5Id, null, "");

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType5Id, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);


            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Nova";
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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6578a", "98910", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractTypeid1 = dbHelper.employmentContractType.GetByName("Salaried").FirstOrDefault();

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

            var user1name = "CP6590" + currentTimeString;
            var user1FirstName = "CP6590";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            dbHelper.systemUserEmploymentContract.UpdateContractHoursPerWeek(_systemUser1EmploymentContractId, 10m);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType5Id);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL");

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(user1LastName)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickSelectPeopleButton();

            selectMultiplePeoplePopup
                .WaitForSelectMultiplePeopleAreaToLoad()
                .ClickOnRecordCheckbox(_personcontract1Id)
                .ClickConfirmSelectionButton();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("This booking exceeds the contracted hours for " + user1FirstName + " " + user1LastName + ". Please allocate a new contract or deallocate bookings for this contract to continue")
                .ClickDismissButton_DynamicDialogue();

            #endregion

            #region Step 3

            dbHelper.systemUserEmploymentContract.UpdateContractHoursPerWeek(_systemUser1EmploymentContractId, 48m);

            System.Threading.Thread.Sleep(1000);

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(5000);

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupClosed();

            var cpDiaryBookingId1 = dbHelper.cPBookingDiary.GetByLocationId(providerId)[0];

            var bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId1);
            var contract = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), contract["systemuseremploymentcontractid"].ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(cpDiaryBookingId1.ToString());

            #endregion

            #region Step 4

            dbHelper.systemUserEmploymentContract.UpdateContractHoursPerWeek(_systemUser1EmploymentContractId, 10m);
            dbHelper.cPSchedulingSetup.UpdateSalariedField(cpSchedulingSetupId, 3); //Warn Only

            #endregion

            #region Step 5, 6

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL");

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            var startYear = todayDate.AddDays(1).Year.ToString();
            var startMonth = todayDate.AddDays(1).ToString("MMMM");
            var startDay = todayDate.AddDays(1).Day.ToString();


            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .SelectStartDate(startYear, startMonth, startDay)
                .InsertStartTime("07", "00")
                .InsertEndTime("23", "00")
                .SelectEndDate(startYear, startMonth, startDay)
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(user1LastName)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickSelectPeopleButton();

            selectMultiplePeoplePopup
                .WaitForSelectMultiplePeopleAreaToLoad()
                .ClickOnRecordCheckbox(_personcontract1Id)
                .ClickConfirmSelectionButton();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("This booking exceeds the contracted hours for " + user1FirstName + " " + user1LastName + ". Please confirm that you are happy to exceed this limit.")
                .ClickDismissButton_DynamicDialogue();

            #endregion

            #region Step 7

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("This booking exceeds the contracted hours for " + user1FirstName + " " + user1LastName + ". Please confirm that you are happy to exceed this limit.")
                .ClickSaveButton_DynamicDialogue();

            System.Threading.Thread.Sleep(5000);

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupClosed();

            var cpDiaryBookingId2 = dbHelper.cPBookingDiary.GetByLocationId(providerId)[0];
            bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId2);
            contract = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), contract["systemuseremploymentcontractid"].ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(todayDate.AddDays(1));

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(cpDiaryBookingId2.ToString());

            #endregion

            #region Step 8

            dbHelper.cPSchedulingSetup.UpdateSalariedField(cpSchedulingSetupId, 2); //No Check

            #endregion

            #region Step 9

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL");

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            startYear = todayDate.AddDays(2).Year.ToString();
            startMonth = todayDate.AddDays(2).ToString("MMMM");
            startDay = todayDate.AddDays(2).Day.ToString();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .SelectStartDate(startYear, startMonth, startDay)
                .InsertStartTime("05", "00")
                .InsertEndTime("21", "00")
                .SelectEndDate(startYear, startMonth, startDay)
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(user1LastName)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickSelectPeopleButton();

            selectMultiplePeoplePopup
                .WaitForSelectMultiplePeopleAreaToLoad()
                .ClickOnRecordCheckbox(_personcontract1Id)
                .ClickConfirmSelectionButton();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(5000);

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupClosed();

            var cpDiaryBookingId3 = dbHelper.cPBookingDiary.GetByLocationId(providerId)[0];

            bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId3);
            contract = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), contract["systemuseremploymentcontractid"].ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(todayDate.AddDays(2));

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(cpDiaryBookingId3.ToString());

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6592

        [TestProperty("JiraIssueID", "ACC-6592")]
        [Description("Step(s) 10 to 12 from the original test ACC-6578")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderDiary_ACC_6578_UITestCases002()
        {

            dbHelper.cPSchedulingSetup.UpdateSalariedField(cpSchedulingSetupId, 1); //Check

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "P6591 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType5Name = "BTC5 6591";
            var _bookingType5Id = commonMethodsDB.CreateCPBookingType(_bookingType5Name, 5, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, null, null, null, null, 3);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType5Id, true);

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

            var careProviderServiceMapping1Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType5Id, null, "");

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType5Id, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);


            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Nova";
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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6578a", "98910", null, new DateTime(2020, 1, 1), null);

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

            var user1name = "CP6591" + currentTimeString;
            var user1FirstName = "CP6591";
            var user1LastName = "SystemUser1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            dbHelper.systemUserEmploymentContract.UpdateContractHoursPerWeek(_systemUser1EmploymentContractId, 10m);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType5Id);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Step 10

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
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(user1LastName)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickSelectPeopleButton();

            selectMultiplePeoplePopup
                .WaitForSelectMultiplePeopleAreaToLoad()
                .ClickOnRecordCheckbox(_personcontract1Id)
                .ClickConfirmSelectionButton();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("This booking exceeds the contracted hours for " + user1FirstName + " " + user1LastName + ". Please allocate a new contract or deallocate bookings for this contract to continue")
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickOnCloseButton()
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("You have unsaved changes. Are you sure you want to close the drawer?")
                .ClickConfirmButton_WarningDialogue();

            #endregion

            #region Step 11

            dbHelper.cPSchedulingSetup.UpdateSalariedField(cpSchedulingSetupId, 3); //Warn Only

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(user1LastName)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickSelectPeopleButton();

            selectMultiplePeoplePopup
                .WaitForSelectMultiplePeopleAreaToLoad()
                .ClickOnRecordCheckbox(_personcontract1Id)
                .ClickConfirmSelectionButton();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("This booking exceeds the contracted hours for " + user1FirstName + " " + user1LastName + ". Please confirm that you are happy to exceed this limit.")
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("This booking exceeds the contracted hours for " + user1FirstName + " " + user1LastName + ". Please confirm that you are happy to exceed this limit.")
                .ClickSaveButton_DynamicDialogue();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupClosed();

            var cpDiaryBookingId1 = dbHelper.cPBookingDiary.GetByLocationId(providerId)[0];
            var bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId1);
            var contract = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), contract["systemuseremploymentcontractid"].ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(cpDiaryBookingId1.ToString());

            #endregion

            #region Step 12

            dbHelper.cPSchedulingSetup.UpdateSalariedField(cpSchedulingSetupId, 2); //No Check

            var startYear = todayDate.AddDays(1).Year.ToString();
            var startMonth = todayDate.AddDays(1).ToString("MMMM");
            var startDay = todayDate.AddDays(1).Day.ToString();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .SelectStartDate(startYear, startMonth, startDay)
                .InsertStartTime("05", "00")
                .InsertEndTime("21", "00")
                .SelectEndDate(startYear, startMonth, startDay)
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(user1LastName)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickSelectPeopleButton();

            selectMultiplePeoplePopup
                .WaitForSelectMultiplePeopleAreaToLoad()
                .ClickOnRecordCheckbox(_personcontract1Id)
                .ClickConfirmSelectionButton();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupClosed();

            var cpDiaryBookingId2 = dbHelper.cPBookingDiary.GetByLocationId(providerId)[0];

            bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId2);
            contract = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), contract["systemuseremploymentcontractid"].ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(todayDate.AddDays(1));

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(cpDiaryBookingId2.ToString());

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-6597")]
        [Description("Steps 15 to 17 from the original test ACC-6578")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderDiary_ACC_6578_UITestCases003()
        {

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var startYear = todayDate.AddDays(1).Year.ToString();
            var startMonth = todayDate.AddDays(1).ToString("MMMM");
            var startDay = todayDate.AddDays(1).Day.ToString();

            var startYear2 = todayDate.AddDays(2).Year.ToString();
            var startMonth2 = todayDate.AddDays(2).ToString("MMMM");
            var startDay2 = todayDate.AddDays(2).Day.ToString();

            #region Provider

            var providerName = "P6597 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType5Name = "BTC5 6597 " + currentTimeString;
            var _bookingType5Id = commonMethodsDB.CreateCPBookingType(_bookingType5Name, 5, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, null, null, null, null, 3);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType5Id, true);

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

            var careProviderServiceMapping1Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType5Id, null, "");

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType5Id, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);


            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Nova";
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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6578a", "98910", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractTypeid1 = dbHelper.employmentContractType.GetByName("Salaried").FirstOrDefault();

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

            var user1name = "CP6597SU1" + currentTimeString;
            var user1FirstName = "CP6597";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            dbHelper.systemUserEmploymentContract.UpdateContractHoursPerWeek(_systemUser1EmploymentContractId, 5m);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType5Id);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Step 15

            dbHelper.cpBookingType.UpdateWorkingContractedTime(_bookingType5Id, 2);
            dbHelper.cpBookingType.UpdateCapDuration(_bookingType5Id, 60);

            #endregion

            #region Step 16

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
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(user1LastName)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickSelectPeopleButton();

            selectMultiplePeoplePopup
                .WaitForSelectMultiplePeopleAreaToLoad()
                .ClickOnRecordCheckbox(_personcontract1Id)
                .ClickConfirmSelectionButton();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(3000);

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupClosed();

            var cpDiaryBookingId1 = dbHelper.cPBookingDiary.GetByLocationId(providerId)[0];

            var bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId1);
            var contract = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), contract["systemuseremploymentcontractid"].ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(cpDiaryBookingId1.ToString());

            #endregion

            #region Step 17

            dbHelper.cpBookingType.UpdateCapDuration(_bookingType5Id, 150);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .SelectStartDate(startYear, startMonth, startDay)
                .SelectEndDate(startYear, startMonth, startDay)
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(user1LastName)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickSelectPeopleButton();

            selectMultiplePeoplePopup
                .WaitForSelectMultiplePeopleAreaToLoad()
                .ClickOnRecordCheckbox(_personcontract1Id)
                .ClickConfirmSelectionButton();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupClosed();

            var cpDiaryBookingId2 = dbHelper.cPBookingDiary.GetByLocationId(providerId)[0];

            bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId2);
            contract = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), contract["systemuseremploymentcontractid"].ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(todayDate.AddDays(1));

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(cpDiaryBookingId2.ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .SelectStartDate(startYear2, startMonth2, startDay2)
                .SelectEndDate(startYear2, startMonth2, startDay2)
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(user1LastName)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickSelectPeopleButton();

            selectMultiplePeoplePopup
                .WaitForSelectMultiplePeopleAreaToLoad()
                .ClickOnRecordCheckbox(_personcontract1Id)
                .ClickConfirmSelectionButton();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("This booking exceeds the contracted hours for " + user1FirstName + " " + user1LastName + ". Please allocate a new contract or deallocate bookings for this contract to continue")
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickOnCloseButton()
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("You have unsaved changes. Are you sure you want to close the drawer?")
                .ClickConfirmButton_WarningDialogue()
                .WaitForCreateDiaryBookingPopupClosed();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6603")]
        [Description("Steps 18 from the original test ACC-6578")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderDiary_ACC_6578_UITestCases004()
        {

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var startYear = todayDate.AddDays(1).Year.ToString();
            var startMonth = todayDate.AddDays(1).ToString("MMMM");
            var startDay = todayDate.AddDays(1).Day.ToString();

            var startYear2 = todayDate.AddDays(2).Year.ToString();
            var startMonth2 = todayDate.AddDays(2).ToString("MMMM");
            var startDay2 = todayDate.AddDays(2).Day.ToString();

            #region Provider

            var providerName = "P6603 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType5Name = "BTC5 6603 " + currentTimeString;
            var _bookingType5Id = commonMethodsDB.CreateCPBookingType(_bookingType5Name, 5, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, null, null, null, null, 3);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType5Id, true);

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

            var careProviderServiceMapping1Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType5Id, null, "");

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType5Id, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);


            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Nova";
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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6578a", "98910", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractTypeid1 = dbHelper.employmentContractType.GetByName("Salaried").FirstOrDefault();

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

            var user1name = "CP6603SU1" + currentTimeString;
            var user1FirstName = "CP6603";
            var user1LastName = "System User 1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            dbHelper.systemUserEmploymentContract.UpdateContractHoursPerWeek(_systemUser1EmploymentContractId, 5m);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType5Id);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Step 18

            dbHelper.cpBookingType.UpdateWorkingContractedTime(_bookingType5Id, 2);
            dbHelper.cpBookingType.UpdateCapDuration(_bookingType5Id, 150);
            dbHelper.cPSchedulingSetup.UpdateSalariedField(cpSchedulingSetupId, 3); //Warn Only

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
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(user1LastName)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickSelectPeopleButton();

            selectMultiplePeoplePopup
                .WaitForSelectMultiplePeopleAreaToLoad()
                .ClickOnRecordCheckbox(_personcontract1Id)
                .ClickConfirmSelectionButton();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupClosed();

            var cpDiaryBookingId1 = dbHelper.cPBookingDiary.GetByLocationId(providerId)[0];

            var bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId1);
            var contract = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), contract["systemuseremploymentcontractid"].ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(cpDiaryBookingId1.ToString());

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .SelectStartDate(startYear, startMonth, startDay)
                .SelectEndDate(startYear, startMonth, startDay)
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(user1LastName)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickSelectPeopleButton();

            selectMultiplePeoplePopup
                .WaitForSelectMultiplePeopleAreaToLoad()
                .ClickOnRecordCheckbox(_personcontract1Id)
                .ClickConfirmSelectionButton();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupClosed();

            var cpDiaryBookingId2 = dbHelper.cPBookingDiary.GetByLocationId(providerId)[0];

            bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId2);
            contract = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), contract["systemuseremploymentcontractid"].ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(todayDate.AddDays(1));

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(cpDiaryBookingId2.ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .SelectStartDate(startYear2, startMonth2, startDay2)
                .SelectEndDate(startYear2, startMonth2, startDay2)
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(user1LastName)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickSelectPeopleButton();

            selectMultiplePeoplePopup
                .WaitForSelectMultiplePeopleAreaToLoad()
                .ClickOnRecordCheckbox(_personcontract1Id)
                .ClickConfirmSelectionButton();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("This booking exceeds the contracted hours for " + user1FirstName + " " + user1LastName + ". Please confirm that you are happy to exceed this limit.")
                .ClickDismissButton_DynamicDialogue();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("This booking exceeds the contracted hours for " + user1FirstName + " " + user1LastName + ". Please confirm that you are happy to exceed this limit.")
                .ClickSaveButton_DynamicDialogue();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupClosed();

            var cpDiaryBookingId3 = dbHelper.cPBookingDiary.GetByLocationId(providerId)[0];

            bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId2);
            contract = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), contract["systemuseremploymentcontractid"].ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(todayDate.AddDays(2));

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(cpDiaryBookingId3.ToString());

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6624

        [TestProperty("JiraIssueID", "ACC-6626")]
        [Description("Steps 19 from the original test ACC-6578")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderDiary_ACC_6578_UITestCases005()
        {

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var startYear = todayDate.AddDays(1).Year.ToString();
            var startMonth = todayDate.AddDays(1).ToString("MMMM");
            var startDay = todayDate.AddDays(1).Day.ToString();

            var startYear2 = todayDate.AddDays(2).Year.ToString();
            var startMonth2 = todayDate.AddDays(2).ToString("MMMM");
            var startDay2 = todayDate.AddDays(2).Day.ToString();

            #region Provider

            var providerName = "P6626 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType5Name = "BTC5 6626 " + currentTimeString;
            var _bookingType5Id = commonMethodsDB.CreateCPBookingType(_bookingType5Name, 5, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, null, null, null, null, 3);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType5Id, true);

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

            var careProviderServiceMapping1Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType5Id, null, "");

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType5Id, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);


            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Nova";
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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6578a", "98910", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

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

            var user1name = "CP6626SU2" + currentTimeString;
            var user1FirstName = "CP6626";
            var user1LastName = "System User 2 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            dbHelper.systemUserEmploymentContract.UpdateContractHoursPerWeek(_systemUser1EmploymentContractId, 5m);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType5Id);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Step 19

            dbHelper.cpBookingType.UpdateWorkingContractedTime(_bookingType5Id, 2);
            dbHelper.cpBookingType.UpdateCapDuration(_bookingType5Id, 150);
            dbHelper.cPSchedulingSetup.UpdateContractedField(cpSchedulingSetupId, 2); //No Check

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
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(user1LastName)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickSelectPeopleButton();

            selectMultiplePeoplePopup
                .WaitForSelectMultiplePeopleAreaToLoad()
                .ClickOnRecordCheckbox(_personcontract1Id)
                .ClickConfirmSelectionButton();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupClosed();

            var cpDiaryBookingId1 = dbHelper.cPBookingDiary.GetByLocationId(providerId)[0];

            var bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId1);
            var contract = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), contract["systemuseremploymentcontractid"].ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(cpDiaryBookingId1.ToString());

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .SelectStartDate(startYear, startMonth, startDay)
                .SelectEndDate(startYear, startMonth, startDay)
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(user1LastName)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickSelectPeopleButton();

            selectMultiplePeoplePopup
                .WaitForSelectMultiplePeopleAreaToLoad()
                .ClickOnRecordCheckbox(_personcontract1Id)
                .ClickConfirmSelectionButton();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupClosed();

            var cpDiaryBookingId2 = dbHelper.cPBookingDiary.GetByLocationId(providerId)[0];

            bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId2);
            contract = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), contract["systemuseremploymentcontractid"].ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(todayDate.AddDays(1));

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(cpDiaryBookingId2.ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .SelectStartDate(startYear2, startMonth2, startDay2)
                .SelectEndDate(startYear2, startMonth2, startDay2)
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(user1LastName)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickSelectPeopleButton();

            selectMultiplePeoplePopup
                .WaitForSelectMultiplePeopleAreaToLoad()
                .ClickOnRecordCheckbox(_personcontract1Id)
                .ClickConfirmSelectionButton();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupClosed();

            var cpDiaryBookingId3 = dbHelper.cPBookingDiary.GetByLocationId(providerId)[0];

            bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId2);
            contract = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), contract["systemuseremploymentcontractid"].ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(todayDate.AddDays(2));

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(cpDiaryBookingId3.ToString());

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6627")]
        [Description("Steps 22 from the original test ACC-6578")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Diary")]
        public void ProviderDiary_ACC_6578_UITestCases006()
        {

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var startYear = todayDate.AddDays(1).Year.ToString();
            var startMonth = todayDate.AddDays(1).ToString("MMMM");
            var startDay = todayDate.AddDays(1).Day.ToString();

            var startYear2 = todayDate.AddDays(2).Year.ToString();
            var startMonth2 = todayDate.AddDays(2).ToString("MMMM");
            var startDay2 = todayDate.AddDays(2).Day.ToString();

            #region Provider

            var providerName = "P6627 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType5Name = "BTC5 6627 " + currentTimeString;
            var _bookingType5Id = commonMethodsDB.CreateCPBookingType(_bookingType5Name, 5, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, null, null, null, null, 3);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType5Id, true);

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

            var careProviderServiceMapping1Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType5Id, null, "");

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType5Id, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, _teamId);


            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Nova";
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

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6578a", "98910", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

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

            var user1name = "CP6627SU3" + currentTimeString;
            var user1FirstName = "CP6627";
            var user1LastName = "System User 3 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);
            dbHelper.systemUserEmploymentContract.UpdateContractHoursPerWeek(_systemUser1EmploymentContractId, 5m);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType5Id);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Step 22

            dbHelper.cpBookingType.UpdateWorkingContractedTime(_bookingType5Id, 3); //Don't include in "Working" hours            
            dbHelper.cPSchedulingSetup.UpdateContractedField(cpSchedulingSetupId, 1); //Check

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
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(user1LastName)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickSelectPeopleButton();

            selectMultiplePeoplePopup
                .WaitForSelectMultiplePeopleAreaToLoad()
                .ClickOnRecordCheckbox(_personcontract1Id)
                .ClickConfirmSelectionButton();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupClosed();

            var cpDiaryBookingId1 = dbHelper.cPBookingDiary.GetByLocationId(providerId)[0];

            var bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId1);
            var contract = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), contract["systemuseremploymentcontractid"].ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(cpDiaryBookingId1.ToString());

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(providerName + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .SelectStartDate(startYear, startMonth, startDay)
                .SelectEndDate(startYear, startMonth, startDay)
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(user1LastName)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickSelectPeopleButton();

            selectMultiplePeoplePopup
                .WaitForSelectMultiplePeopleAreaToLoad()
                .ClickOnRecordCheckbox(_personcontract1Id)
                .ClickConfirmSelectionButton();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupClosed();

            var cpDiaryBookingId2 = dbHelper.cPBookingDiary.GetByLocationId(providerId)[0];

            bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId2);
            contract = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), contract["systemuseremploymentcontractid"].ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(todayDate.AddDays(1));

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(cpDiaryBookingId2.ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .SelectStartDate(startYear2, startMonth2, startDay2)
                .SelectEndDate(startYear2, startMonth2, startDay2)
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox(user1LastName)
                .ClickStaffRecordCellText(_systemUser1EmploymentContractId)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickSelectPeopleButton();

            selectMultiplePeoplePopup
                .WaitForSelectMultiplePeopleAreaToLoad()
                .ClickOnRecordCheckbox(_personcontract1Id)
                .ClickConfirmSelectionButton();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickCreateBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupClosed();

            var cpDiaryBookingId3 = dbHelper.cPBookingDiary.GetByLocationId(providerId)[0];

            bookingDiaryStaffRecords = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId2);
            contract = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(bookingDiaryStaffRecords[0], "systemuseremploymentcontractid");
            Assert.AreEqual(_systemUser1EmploymentContractId.ToString(), contract["systemuseremploymentcontractid"].ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(todayDate.AddDays(2));

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(cpDiaryBookingId3.ToString());

            #endregion

        }

        #endregion

    }
}

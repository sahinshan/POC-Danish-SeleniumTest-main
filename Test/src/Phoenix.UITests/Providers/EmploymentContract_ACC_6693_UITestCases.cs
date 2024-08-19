using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Providers
{
    [TestClass]
    public class EmploymentContract_ACC_6693_UITestCases : FunctionalTest
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
        private Guid cpBookingScheduleId;

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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("Employee Schedule BU");

                #endregion

                #region Team

                _teamName = "EmployeeScheduleT1";
                _teamId = commonMethodsDB.CreateTeam(_teamName, null, _businessUnitId, "107623", "EmployeeScheduleT1@careworkstempmail.com", "EmployeeScheduleT1", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region Create default system user

                _systemUsername = "EmployeeScheduleUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "EmployeeSchedule", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

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

        internal Guid CreateCPBookingSchedule(Guid TeamID, Guid BookingTypeId, Guid ProviderID, Guid SystemUserEmploymentContractId, Guid SystemUserId)
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            switch (todayDate.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 1, 1, new TimeSpan(0, 0, 0), new TimeSpan(8, 0, 0), ProviderID, "Express Book Contracted Hours Validation1");
                    dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingScheduleId, 1);
                    dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);
                    break;

                case DayOfWeek.Tuesday:
                    cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 2, 2, new TimeSpan(0, 0, 0), new TimeSpan(8, 0, 0), ProviderID, "Express Book Contracted Hours Validation1");
                    dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);
                    break;

                case DayOfWeek.Wednesday:
                    cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 3, 3, new TimeSpan(0, 0, 0), new TimeSpan(8, 0, 0), ProviderID, "Express Book Contracted Hours Validation1");
                    dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);
                    break;

                case DayOfWeek.Thursday:
                    cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 4, 4, new TimeSpan(0, 0, 0), new TimeSpan(8, 0, 0), ProviderID, "Express Book Contracted Hours Validation1");
                    dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);
                    break;

                case DayOfWeek.Friday:
                    cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 5, 5, new TimeSpan(0, 0, 0), new TimeSpan(8, 0, 0), ProviderID, "Express Book Contracted Hours Validation1");
                    dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);
                    break;

                case DayOfWeek.Saturday:
                    cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 6, 6, new TimeSpan(0, 0, 0), new TimeSpan(8, 0, 0), ProviderID, "Express Book Contracted Hours Validation1");
                    dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);
                    break;

                case DayOfWeek.Sunday:
                    cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 7, 7, new TimeSpan(0, 0, 0), new TimeSpan(8, 0, 0), ProviderID, "Express Book Contracted Hours Validation1");
                    dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);
                    break;
            }
            return cpBookingScheduleId;
        }


        #region https://advancedcsg.atlassian.net/browse/ACC-6743

        [TestProperty("JiraIssueID", "ACC-6760")]
        [Description("Step(s) 1 to 7 from the original test ACC-6693")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Employee Schedule")]
        [TestProperty("Screen2", "Employee Diary")]
        public void EmploymentContract_ACC_6693_UITestMethod001()
        {

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role6693" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);
            var _staffRoleTypeId2 = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Nurse6693" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);
            var _staffRoleTypeId3 = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Carer6693" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);
            var _staffRoleTypeId4 = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Support6693" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "P6693 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC1 6693", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "ECUser1" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "ECUser1", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId1, 1); //1 = Male

            string _staffBName = "ECUser2" + currentTimeString;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_staffBName, "ECUser2", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId2, 1); //1 = Male


            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId2, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region Active System User Employment Contract

            System.Globalization.CultureInfo cultureinfo = System.Globalization.CultureInfo.CurrentCulture;

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, DateTime.Parse("01/01/2022", cultureinfo), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });
            var _systemUserEmploymentContractId1_b = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, DateTime.Parse("01/01/2023", cultureinfo), _staffRoleTypeId2, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId2, DateTime.Parse("01/01/2022", cultureinfo), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region Link Booking Type to Active Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1_b, _bookingType1);
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
            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1_b, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId2, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region System User Employment Contract

            var _notStarted_systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, null, _staffRoleTypeId2, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region System User Employment Contract for Suspended status

            var systemUserEmploymentContractStartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);
            var suspension_systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, systemUserEmploymentContractStartDate, _staffRoleTypeId3, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });


            #endregion

            #region System User Employment Contract for Ended status

            var systemUserEmploymentContractStartDate2 = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var ended_systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, systemUserEmploymentContractStartDate2, _staffRoleTypeId4, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_notStarted_systemUserEmploymentContractId, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(suspension_systemUserEmploymentContractId, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(ended_systemUserEmploymentContractId, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId1, _teamId, suspension_systemUserEmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId1, _teamId, ended_systemUserEmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId1, _teamId, _notStarted_systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region End System User Employment Contract

            var systemUserEmploymentContractEndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-2);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(ended_systemUserEmploymentContractId, systemUserEmploymentContractEndDate, contractEndReasonId);

            #endregion

            #region Create System User Contract Suspension

            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date;
            var contracts = new List<Guid> { suspension_systemUserEmploymentContractId };
            var suspensionId = dbHelper.systemUserSuspension.CreateSystemUserSuspension(_systemUserId1, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);

            #endregion

            #region Employment contract titles 

            string _systemUser1EmploymentContractId1_Title = (string)dbHelper.systemUserEmploymentContract.GetByID(_systemUserEmploymentContractId1, "name")["name"];
            string _systemUser1EmploymentContractId1_b_Title = (string)dbHelper.systemUserEmploymentContract.GetByID(_systemUserEmploymentContractId1_b, "name")["name"];
            string _systemUser1NotStartedEmploymentContractId_Title = (string)dbHelper.systemUserEmploymentContract.GetByID(_notStarted_systemUserEmploymentContractId, "name")["name"];
            string _systemUser1SuspendedEmploymentContractId_Title = (string)dbHelper.systemUserEmploymentContract.GetByID(suspension_systemUserEmploymentContractId, "name")["name"];
            string _systemUser1EndedEmploymentContractId_Title = (string)dbHelper.systemUserEmploymentContract.GetByID(ended_systemUserEmploymentContractId, "name")["name"];

            #endregion

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("*" + currentTimeString)
                .ClickSearchButton()
                .OpenRecord(_systemUserId2);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmployeeSchedulePage();

            #endregion

            #region Step 3

            employeeSchedulePage
                .WaitForEmployeeSchedulePageToLoad();

            System.Threading.Thread.Sleep(1500);

            employeeSchedulePage
                .ValidateShowAllBookingsCheckBoxIsVisible(false)
                .ValidateEmploymentContractDropdownIsVisible(true);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmployeeDiaryPage();

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ValidateShowAllBookingsCheckBoxIsVisible(false)
                .ValidateEmploymentContractDropdownIsVisible(true);

            #endregion

            #region Step 4

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ClickBackButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .OpenRecord(_systemUserId1);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmployeeSchedulePage();

            System.Threading.Thread.Sleep(2000);

            employeeSchedulePage
                .WaitForEmployeeSchedulePageToLoad()
                .ValidateShowAllBookingsCheckBoxIsVisible(true)
                .ValidateEmploymentContractDropdownIsVisible(true);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmployeeDiaryPage();

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ValidateShowAllBookingsCheckBoxIsVisible(true)
                .ValidateEmploymentContractDropdownIsVisible(true);

            #endregion

            #region Step 7, 5, 6

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmployeeSchedulePage();

            System.Threading.Thread.Sleep(2000);

            employeeSchedulePage
                .WaitForEmployeeSchedulePageToLoad()
                .ValidateSelectedEmploymentContractText("ECUser1 " + currentTimeString + ", Role6693" + currentTimeString + ", 40.00 hrs, 01/01/2022 (Active)");

            employeeSchedulePage
                .WaitForEmployeeSchedulePageToLoad()
                .ClickEmploymentContractDropdown()
                .ValidateEmploymentContractTextStatus("ECUser1 " + currentTimeString + ", Nurse6693" + currentTimeString + ", 40.00 hrs, 01/01/2023 (Active)")
                .ValidateEmploymentContractTextStatus("ECUser1 " + currentTimeString + ", Role6693" + currentTimeString + ", 40.00 hrs, 01/01/2022 (Active)")
                .ValidateEmploymentContractTextStatus("ECUser1 " + currentTimeString + ", Nurse6693" + currentTimeString + ", 40.00 hrs, Not Started")
                .ValidateEmploymentContractTextStatus("ECUser1 " + currentTimeString + ", Carer6693" + currentTimeString + ", 40.00 hrs, " + systemUserEmploymentContractStartDate.ToString("dd'/'MM'/'yyyy") + " (Suspended)")
                .ValidateEmploymentContractTextStatus("ECUser1 " + currentTimeString + ", Support6693" + currentTimeString + ", 40.00 hrs, " + systemUserEmploymentContractStartDate2.ToString("dd'/'MM'/'yyyy") + " - " + systemUserEmploymentContractEndDate.ToString("dd'/'MM'/'yyy") + " (Ended)");

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmployeeDiaryPage();

            System.Threading.Thread.Sleep(2000);

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ValidateSelectedEmploymentContractText("ECUser1 " + currentTimeString + ", Role6693" + currentTimeString + ", 40.00 hrs, 01/01/2022 (Active)");

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ClickEmploymentContractDropdown()
                .ValidateEmploymentContractTextStatus("ECUser1 " + currentTimeString + ", Nurse6693" + currentTimeString + ", 40.00 hrs, 01/01/2023 (Active)")
                .ValidateEmploymentContractTextStatus("ECUser1 " + currentTimeString + ", Role6693" + currentTimeString + ", 40.00 hrs, 01/01/2022 (Active)")
                .ValidateEmploymentContractTextStatus("ECUser1 " + currentTimeString + ", Nurse6693" + currentTimeString + ", 40.00 hrs, Not Started")
                .ValidateEmploymentContractTextStatus("ECUser1 " + currentTimeString + ", Carer6693" + currentTimeString + ", 40.00 hrs, " + systemUserEmploymentContractStartDate.ToString("dd'/'MM'/'yyyy") + " (Suspended)")
                .ValidateEmploymentContractTextStatus("ECUser1 " + currentTimeString + ", Support6693" + currentTimeString + ", 40.00 hrs, " + systemUserEmploymentContractStartDate2.ToString("dd'/'MM'/'yyyy") + " - " + systemUserEmploymentContractEndDate.ToString("dd'/'MM'/'yyy") + " (Ended)");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6745

        [TestProperty("JiraIssueID", "ACC-6761")]
        [Description("Step(s) 8 to 16 from the original test ACC-6693")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Employee Schedule")]
        [TestProperty("Screen2", "Employee Diary")]
        public void EmploymentContract_ACC_6693_UITestMethod002()
        {

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Business Unit

            var _businessUnitId2 = commonMethodsDB.CreateBusinessUnit("Employee Schedule BU2");

            #endregion

            #region Team

            var _teamName2 = "EmployeeScheduleT2";
            var _teamId2 = commonMethodsDB.CreateTeam(_teamName2, null, _businessUnitId2, "707624", "EmployeeScheduleT2@careworkstempmail.com", "ProviderScheduleBookingT2", "020 123456");

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role6693" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);
            var _staffRoleTypeId2 = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Nurse6693" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);
            var _staffRoleTypeId3 = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Carer6693" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);
            var _staffRoleTypeId4 = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Support6693" + currentTimeString, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            var _providerName = "P6693 " + currentTimeString;
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

            var _providerName2 = "P6693b " + currentTimeString;
            var _providerId2 = commonMethodsDB.CreateProvider(_providerName2, _teamId, 12, true); // Training Provider

            var _providerName3 = "P6693c " + currentTimeString;
            var _providerId3 = commonMethodsDB.CreateProvider(_providerName3, _teamId2, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC1 6693", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId2, _bookingType1, true);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId2, _providerId3, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "ECUser1" + currentTimeString;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "ECUser1", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId1, 1); //1 = Male

            string _staffBName = "ECUser2" + currentTimeString;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_staffBName, "ECUser2", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId2, 1); //1 = Male


            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId2, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region Active System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1, 6, 0, 0), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });
            var _systemUserEmploymentContractId1_b = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2023, 1, 1, 6, 0, 0), _staffRoleTypeId2, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region Link Booking Type to Active Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1_b, _bookingType1);

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
            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1_b, _availabilityTypeId);

            #endregion

            #region System User Employment Contract

            var _notStarted_systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, null, _staffRoleTypeId2, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region System User Employment Contract for Suspended status

            var systemUserEmploymentContractStartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);
            var suspension_systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, systemUserEmploymentContractStartDate, _staffRoleTypeId3, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });


            #endregion

            #region System User Employment Contract for Ended status

            var systemUserEmploymentContractStartDate2 = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var ended_systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, systemUserEmploymentContractStartDate2, _staffRoleTypeId4, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_notStarted_systemUserEmploymentContractId, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(suspension_systemUserEmploymentContractId, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(ended_systemUserEmploymentContractId, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId1, _teamId, suspension_systemUserEmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId1, _teamId, ended_systemUserEmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId1, _teamId, _notStarted_systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region End System User Employment Contract

            var systemUserEmploymentContractEndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-2);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(ended_systemUserEmploymentContractId, systemUserEmploymentContractEndDate, contractEndReasonId);

            #endregion

            #region Create System User Contract Suspension

            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date;
            var contracts = new List<Guid> { suspension_systemUserEmploymentContractId };
            dbHelper.systemUserSuspension.CreateSystemUserSuspension(_systemUserId1, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);

            #endregion

            #region Employment contract titles 

            string _systemUser1EmploymentContractId1_Title = (string)dbHelper.systemUserEmploymentContract.GetByID(_systemUserEmploymentContractId1, "name")["name"];

            #endregion

            #region Booking Schedules

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, 1, 1, new TimeSpan(4, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Contracted Hours Validation");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId1, _systemUserId1);

            var cpBookingSchedule2Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, 2, 2, new TimeSpan(4, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Contracted Hours Validation");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule2Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule2Id, _systemUserEmploymentContractId1_b, _systemUserId1);

            var cpBookingSchedule3Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, 3, 3, new TimeSpan(4, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Contracted Hours Validation");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule3Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule3Id, _notStarted_systemUserEmploymentContractId, _systemUserId1);

            var cpBookingSchedule4Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, 4, 4, new TimeSpan(4, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Contracted Hours Validation");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule4Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule4Id, suspension_systemUserEmploymentContractId, _systemUserId1);

            var cpBookingSchedule5Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, 5, 5, new TimeSpan(4, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Contracted Hours Validation");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule5Id, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule5Id, ended_systemUserEmploymentContractId, _systemUserId1);

            var cpBookingSchedule1Id_b = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, 1, 1, new TimeSpan(10, 0, 0), new TimeSpan(14, 0, 0), _providerId2, "Express Book Contracted Hours Validation");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id_b, 1);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id_b, _systemUserEmploymentContractId1, _systemUserId1);

            #endregion

            #region Diary Bookings

            var _cpBookingDiary1Id = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType1, _providerId, todayDate, new TimeSpan(16, 0, 0), todayDate, new TimeSpan(20, 0, 0));
            dbHelper.cPBookingDiary.UpdateGenderPreference(_cpBookingDiary1Id, 1);
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", _cpBookingDiary1Id, _systemUserEmploymentContractId1, _systemUserId1);

            var _cpBookingDiary2Id = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType1, _providerId, todayDate.AddDays(1), new TimeSpan(16, 0, 0), todayDate.AddDays(1), new TimeSpan(20, 0, 0));
            dbHelper.cPBookingDiary.UpdateGenderPreference(_cpBookingDiary2Id, 1);
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", _cpBookingDiary2Id, _systemUserEmploymentContractId1_b, _systemUserId1);

            var _cpBookingDiary1Id_b = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType1, _providerId2, todayDate, new TimeSpan(21, 0, 0), todayDate, new TimeSpan(23, 0, 0));
            dbHelper.cPBookingDiary.UpdateGenderPreference(_cpBookingDiary1Id_b, 1);
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", _cpBookingDiary1Id_b, _systemUserEmploymentContractId1, _systemUserId1);

            #endregion

            #region Step 8 to Step 16

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("*" + currentTimeString)
                .ClickSearchButton()
                .OpenRecord(_systemUserId2);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmployeeSchedulePage();

            System.Threading.Thread.Sleep(2000);

            employeeSchedulePage
                .WaitForEmployeeSchedulePageToLoad(false)
                .ValidateClickToAddNewContractButtonIsVisible()
                .ClickAddNewContractButton();

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateSystemUser_LinkField("ECUser2 " + currentTimeString)
                .ClickBackButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ClickBackButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .OpenRecord(_systemUserId1);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmployeeSchedulePage();

            System.Threading.Thread.Sleep(2000);

            employeeSchedulePage
                .WaitForEmployeeSchedulePageToLoad()
                .ValidateSelectedEmploymentContractText("ECUser1 " + currentTimeString + ", Role6693" + currentTimeString + ", 40.00 hrs, 01/01/2022 (Active)");

            employeeSchedulePage
                .WaitForEmployeeSchedulePageToLoad()
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true)
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id_b.ToString(), true); //Step 9

            employeeSchedulePage
                .WaitForEmployeeSchedulePageToLoad()
                .ValidateScheduleBookingIsPresent(cpBookingSchedule2Id.ToString(), false)
                .ValidateScheduleBookingIsPresent(cpBookingSchedule3Id.ToString(), false)
                .ValidateScheduleBookingIsPresent(cpBookingSchedule4Id.ToString(), false)
                .ValidateScheduleBookingIsPresent(cpBookingSchedule5Id.ToString(), false);

            employeeSchedulePage
                .WaitForEmployeeSchedulePageToLoad()
                .ClickScheduleBooking(cpBookingSchedule1Id);

            //Step 12
            createScheduleBookingPopup
                 .WaitForEditScheduleBookingPopupPageToLoad()
                 .ValidateSelectedStaffFieldValues(_systemUserEmploymentContractId1, "ECUser1 " + currentTimeString)
                 .ClickOnCloseButton();

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupClosed();

            //Step 16
            employeeSchedulePage
                .WaitForEmployeeSchedulePageToLoad()
                .ClickAddBooking();

            createScheduleBookingPopup
                 .WaitForCreateScheduleBookingPopupPageToLoad()
                 .ValidateEmployeeContractInfoText("ECUser1 " + currentTimeString + ", Role6693" + currentTimeString + ", 40.00 hrs, 01/01/2022 (Active)");

            //Step 11
            createScheduleBookingPopup
                .SelectLocationProvider(_providerName3 + " - No Address")
                .SetStartDay("Wednesday")
                .SetStartTime("21", "00")
                .SetEndTime("22", "00")
                .SetEndDay("Wednesday")
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateWarningAlertMessage("The staff contract of " + "ECUser1 " + currentTimeString + " " + _systemUser1EmploymentContractId1_Title + " is invalid for " + _providerName3 + ".")
                .ClickDismissButton_DynamicDialogue();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickOnCloseButton()
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage("You have unsaved changes. Are you sure you want to close the drawer?")
                .ClickConfirmButton_WarningDialogue();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            employeeSchedulePage
                .WaitForEmployeeSchedulePageToLoad()
                .MouseHoverScheduleBooking(cpBookingSchedule1Id.ToString());

            System.Threading.Thread.Sleep(1000);

            //Step 10
            employeeSchedulePage
                .WaitForEmployeeSchedulePageToLoad()
                .ValidateStaffLabelText("ECUser1 " + currentTimeString)
                .ValidateTimeLabelText("Monday 04:00 - 08:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC1 6693")
                .ValidateOccursLabelText("Every 1 week");

            employeeSchedulePage
                .WaitForEmployeeSchedulePageToLoad()
                .MouseHoverScheduleBooking(cpBookingSchedule1Id_b.ToString());

            System.Threading.Thread.Sleep(1000);

            employeeSchedulePage
                .WaitForEmployeeSchedulePageToLoad()
                .ValidateStaffLabelText("ECUser1 " + currentTimeString)
                .ValidateTimeLabelText("Monday 10:00 - 14:00")
                .ValidateProviderLabelText(_providerName2)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC1 6693")
                .ValidateOccursLabelText("Every 1 week");

            //Step 13 and Step 14
            employeeSchedulePage
                .ClickShowAllBookingsCheckBox();

            employeeSchedulePage
                .WaitForEmployeeSchedulePageToLoad()
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true)
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id_b.ToString(), true);

            employeeSchedulePage
                .WaitForEmployeeSchedulePageToLoad()
                .ValidateScheduleBookingIsPresent(cpBookingSchedule2Id.ToString(), true)
                .ValidateScheduleBookingIsPresent(cpBookingSchedule3Id.ToString(), true)
                .ValidateScheduleBookingIsPresent(cpBookingSchedule4Id.ToString(), true)
                .ValidateScheduleBookingIsPresent(cpBookingSchedule5Id.ToString(), true);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmployeeDiaryPage();

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ValidateSelectedEmploymentContractText("ECUser1 " + currentTimeString + ", Role6693" + currentTimeString + ", 40.00 hrs, 01/01/2022 (Active)");

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(_cpBookingDiary1Id.ToString(), true)
                .ValidateDiaryBookingIsPresent(_cpBookingDiary1Id_b.ToString(), true); //Step 9

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(_cpBookingDiary2Id.ToString(), false);

            //Step 10
            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ClickDiaryBooking(_cpBookingDiary1Id);

            //Step 12
            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                .VerifySelectedStaffRecordInStaffForBookingIsDisplayed(_systemUserEmploymentContractId1.ToString(), "ECUser1 " + currentTimeString)
                .ClickOnCloseButton();

            //Step 16
            employeeDiaryPage
                  .WaitForEmployeeDiaryPageToLoad()
                  .ClickAddBookingButton();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ValidateEmployeeContractInfoText("ECUser1 " + currentTimeString + ", Role6693" + currentTimeString + ", 40.00 hrs, 01/01/2022 (Active)")
                .ClickOnCloseButton();

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .MouseHoverDiaryBooking(_cpBookingDiary1Id.ToString());

            System.Threading.Thread.Sleep(1000);

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ValidateStaffLabelText("ECUser1 " + currentTimeString)
                .ValidateTimeLabelText(todayDate.ToString("dd'/'MM'/'yyyy") + " 16:00 - 20:00")
                //.ValidateStartEndDateLabelText(todayDate.ToString("dd'/'MM'/'yyyy") + " - " + todayDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateProviderLabelText(_providerName)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC1 6693");

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .MouseHoverDiaryBooking(_cpBookingDiary1Id_b.ToString());

            System.Threading.Thread.Sleep(1000);

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ValidateStaffLabelText("ECUser1 " + currentTimeString)
                .ValidateTimeLabelText(todayDate.ToString("dd'/'MM'/'yyyy") + " 21:00 - 23:00")
                //.ValidateStartEndDateLabelText(todayDate.ToString("dd'/'MM'/'yyyy") + " - " + todayDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateProviderLabelText(_providerName2)
                .ValidateAddressLabelText("No Address")
                .ValidateBookingTypeLabelText("BTC1 6693");

            //Step 13 and Step 14 for Employee Diary Page
            employeeDiaryPage
                .ClickShowAllBookingsCheckBox();

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(_cpBookingDiary1Id.ToString(), true)
                .ValidateDiaryBookingIsPresent(_cpBookingDiary1Id_b.ToString(), true);

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(_cpBookingDiary2Id.ToString(), true);

            #endregion

        }

        #endregion

    }
}

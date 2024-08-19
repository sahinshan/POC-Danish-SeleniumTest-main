using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Recuritments
{
    /// <summary>
    /// This class contains Automated UI test scripts for Verify That Calendar Icon is disabled
    /// </summary>
    [TestClass]
    public class ScheduleAvailability_CalendarIcon_UITestCases : FunctionalTest
    {
        #region Properties

        private string EnvironmentName;
        private Guid authenticationproviderid = new Guid("64d2d456-11dc-e611-80d4-0050560502cc");//Internal
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _defaultLoginUserID;
        private Guid _systemUserID;
        private Guid _careProviderStaffRoleTypeid;
        private Guid _careProviderStaffRoleTypeid2;
        private Guid _employmentContractTypeid;
        private Guid _employmentContractTypeid2;
        private Guid _employmentContractId;
        private Guid _employmentContractId2;
        public Guid environmentid;
        private string _loginUser_Username;
        private string _systemUser_Username;
        private string _careProviderStaffRoleType_Name;
        private string _careProviderStaffRoleType2_Name;
        private string _employmentContractName;
        private string _employmentContract2Name;
        private string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");

        #endregion


        [TestInitialize()]
        public void TestSetup()
        {
            try
            {

                #region Environment Name

                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);
                #endregion

                #region Business Unit

                _careProviders_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareProviders");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                _careProviders_TeamId = commonMethodsDB.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");

                #endregion

                #region Create default system user

                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord("CW_Admin_Test_Schedule_1", "CW", "Admin_Test_User_1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
                _loginUser_Username = "CW_Admin_Test_Schedule_1";

                #endregion  Create default system user


                #region Team Manager

                dbHelper.team.UpdateTeamManager(_careProviders_TeamId, _defaultLoginUserID);

                #endregion

                #region Create SystemUser Record

                var newSystemUser = dbHelper.systemUser.GetSystemUserByUserName("ACC-4003_User").Any();
                if (!newSystemUser)
                    _systemUserID = dbHelper.systemUser.CreateSystemUser("ACC-4003_User", "ACC-4003", "User", "ACC-4003 User", "Passw0rd_!", "ACC-4003_User@somemail.com", "ACC-4003_User@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);

                if (_systemUserID == Guid.Empty)
                    _systemUserID = dbHelper.systemUser.GetSystemUserByUserName("ACC-4003_User").FirstOrDefault();

                _systemUser_Username = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemUserID, "username")["username"];

                #endregion  Create SystemUser Record

                #region Care provider staff role type

                _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Carer_ACC-4003", "2", null, new DateTime(2020, 1, 1), null);

                _careProviderStaffRoleType_Name = (string)dbHelper.careProviderStaffRoleType.GetCareProviderStaffRoleTypeByID(_careProviderStaffRoleTypeid, "name")["name"];

                #endregion

                #region Care provider staff role type 2

                _careProviderStaffRoleTypeid2 = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Cook_ACC-4003", "2", null, new DateTime(2020, 1, 1), null);

                _careProviderStaffRoleType2_Name = (string)dbHelper.careProviderStaffRoleType.GetCareProviderStaffRoleTypeByID(_careProviderStaffRoleTypeid2, "name")["name"];

                #endregion

                #region Employment Contract Type

                _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Employment Contract Type 4003 " + currentTimeString, "2", null, new DateTime(2020, 1, 1));

                #endregion

                #region Employment Contract Type 2

                _employmentContractTypeid2 = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Employment Contract Type2 4003 " + currentTimeString, "2", null, new DateTime(2020, 1, 1));

                #endregion

                #region Delete Employment Contracts for System User

                foreach (var workSheduleId in dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserID))
                    dbHelper.userWorkSchedule.DeleteUserWorkSchedule(workSheduleId);


                var employmentContracts = dbHelper.systemUserEmploymentContract.GetBySystemUserId(_systemUserID);

                foreach (var employmentContract in employmentContracts)
                {
                    dbHelper.systemUserEmploymentContract.DeleteSystemUserEmploymentContract(employmentContract);
                }

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }



        #region https://advancedcsg.atlassian.net/browse/ACC-4003

        [TestProperty("JiraIssueID", "ACC-4003")]
        [Description("Login CD as a Care Provider." +
            "The Schedule Availability Button Should be added In Availability Tab." +
            "Verify calendar icon is disbaled and having date as current week's monday as start date.\"")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("Screen1", "Schedule Availability")]
        public void VerifyCalendarIconInScheduleScreen_UITestCases001()
        {

            #region Step 1 login

            loginPage

              .GoToLoginPage()
              .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2 navigate to availability and verify schedule availability screen is there after creating record

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUser_Username)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateSystemUserWarningMessage("Current system user does not have any employment contracts.");

            #endregion

            #region Employment Contract 

            _employmentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserID,
                    new DateTime(2020, 1, 1), _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid);
            if (_employmentContractId == Guid.Empty)
            {
                _employmentContractId = dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByName(_careProviderStaffRoleType_Name + " - " + new DateTime(2020, 1, 1).ToString("dd'/'MM'/'yyyy")).FirstOrDefault();

            }

            _employmentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserID,
                    new DateTime(2021, 1, 1), _careProviderStaffRoleTypeid2, _careProviders_TeamId, _employmentContractTypeid2);

            if (_employmentContractId2 == Guid.Empty)
            {
                _employmentContractId2 = dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByName(_careProviderStaffRoleType2_Name + " - " + new DateTime(2021, 1, 1).ToString("dd'/'MM'/'yyyy")).FirstOrDefault();

            }
            #endregion

            _employmentContractName = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_employmentContractId, "name")["name"];

            _employmentContract2Name = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_employmentContractId2, "name")["name"];

            #region Step 3 verify calendar icon is disabled

            systemUserAvailabilitySubPage
              .ClickRefreshButton();

            DateTime currentDayOfTheWeek = DateTime.Now;
            string firstDayofTheCurrentWeek = datetimeFirstDay.GetFirstDayOfWeek(currentDayOfTheWeek).ToString("dd'/'MM'/'yyyy");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateScheduleAvailabilityAreaDislayed()
                .ClickScheduleAvailabilityCard()
                .ValidateWeek1CycleDateReadonly(true)
                .ValidateWeek1CycleDate(firstDayofTheCurrentWeek);

            #endregion

        }

        #endregion

    }

    static class datetimeFirstDay
    {
        //To Get The First Day of the Week in C#
        public static DateTime GetFirstDayOfWeek(this DateTime date)
        {
            var culture = System.Globalization.CultureInfo.GetCultureInfo("en-GB");
            var diff = date.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
                diff += 7;
            return date.AddDays(-diff).Date;
        }
    }
}

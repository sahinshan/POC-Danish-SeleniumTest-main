using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Security
{
    /// <summary>
    /// This class contains Automated UI test scripts for Manage Ad Hoc Page
    /// </summary>
    [TestClass]
    public class ViewDiaryManageAdHoc_UITestCases : FunctionalTest
    {
        #region Properties

        private string _environmentName;
        private string tenantName;
        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _defaultLoginUserID;
        private Guid _careProviderStaffRoleTypeid;
        private Guid _recurrencePatternId;
        private Guid _recurrencePatternId_weekly;
        private string _currentDayOfTheWeek;
        private string _loginUsername;
        private Guid _careProviderStaffRoleTypeid2;
        private Guid _employmentContractTypeid;
        private Guid _employmentContractTypeid2;
        private string _employmentContractTypeName;
        private Guid _employmentContractId;
        private Guid _employmentContractId2;
        private Guid _employmentContractId3;
        private Guid _employmentContractId4;
        private Guid _systemUserSuspensionReasonId;
        public Guid environmentid;
        private string _careProviderStaffRoleType_Name;
        private string _employmentContractName;
        private string _employmentContract2Name;
        private string _employmentContract3Name;
        private string _employmentContract4Name;
        private Guid _availabilityTypeId;
        private Guid _availabilityTypeId_Regular;
        private Guid _availabilityTypeId_overtime;
        private Guid _userWorkScheduleId1;
        private Guid _userWorkScheduleId2;
        private Guid _userWorkScheduleId3;
        private Guid _userWorkScheduleId4;
        private string pastDate = DateTime.Now.AddDays(-1).ToString("dd'/'MM'/'yyyy");
        private string futureDate = DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy");
        private string contractName;
        string username;
        string password;
        #endregion

        internal DateTime GetThisWeekFirstMonday()
        {
            DateTime dt = DateTime.Now;
            int diff = (7 + (dt.DayOfWeek - DayOfWeek.Monday)) % 7;
            return dt.AddDays(-1 * diff).Date;

        }

        [TestInitialize()]
        public void TestSetup()
        {
            try
            {
                #region Environment Name

                _environmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

                username = ConfigurationManager.AppSettings["Username"];
                password = ConfigurationManager.AppSettings["Password"];
                string DataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                if (DataEncoded.Equals("true"))
                {
                    var base64EncodedBytes = System.Convert.FromBase64String(username);
                    username = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

                    base64EncodedBytes = System.Convert.FromBase64String(password);
                    password = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                }

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").First();

                #endregion

                #region Default User

                var userid = dbHelper.systemUser.GetSystemUserByUserName("administrator").FirstOrDefault();
                dbHelper.systemUser.UpdateLastPasswordChangedDate(userid, DateTime.Now.Date);

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

                _loginUsername = "Test_User_CDV6_19419";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUsername, "Test_User", "CDV6_19419", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, _authenticationproviderid);

                dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_defaultLoginUserID, GetThisWeekFirstMonday());
                dbHelper.systemUser.UpdateSATransportWeek1CycleStartDate(_defaultLoginUserID, GetThisWeekFirstMonday());

                #endregion

                #region Care provider staff role type

                _careProviderStaffRoleType_Name = "Role_19419";
                _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Role_19419", "2", null, new DateTime(2020, 1, 1), null);

                #endregion

                #region Care provider staff role type 2

                _careProviderStaffRoleTypeid2 = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Role_19419_2", "2", null, new DateTime(2020, 2, 1), null);

                #endregion

                #region Recurrence pattern

                _currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();

                _recurrencePatternId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").FirstOrDefault();

                _recurrencePatternId_weekly = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on " + DateTime.Now.AddDays(1).DayOfWeek.ToString()).FirstOrDefault();

                #endregion

                #region Availability Type

                _availabilityTypeId = commonMethodsDB.CreateAvailabilityType("Standard", 3, false, _careProviders_TeamId, 1, 1, true);
                _availabilityTypeId_Regular = commonMethodsDB.CreateAvailabilityType("Regular", 5, false, _careProviders_TeamId, 1, 1, true);
                _availabilityTypeId_overtime = commonMethodsDB.CreateAvailabilityType("OverTime", 4, false, _careProviders_TeamId, 1, 1, true);

                #endregion

                #region Employment Contract Type

                _employmentContractTypeName = "Employment Contract Type 19419";
                _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, _employmentContractTypeName, "2", null, new DateTime(2020, 1, 1));

                #endregion

                #region Employment Contract Type 2

                _employmentContractTypeid2 = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Employment Contract Type2 19419", "2", null, new DateTime(2020, 1, 1));

                #endregion

                #region System User Suspension Reason

                _systemUserSuspensionReasonId = dbHelper.systemUserSuspensionReason.CreateSystemUserSuspensionReason(_careProviders_TeamId, "CDV6-19419_" + DateTime.Now.ToString("yyyy-MM-dd_HHmmss_ffff"), DateTime.Now.Date);

                #endregion

                #region Delete User Work Schedules for System User

                foreach (var workScheduleId in dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_defaultLoginUserID))
                    dbHelper.userWorkSchedule.DeleteUserWorkSchedule(workScheduleId);

                #endregion

                #region Delete EmploymentContract
                var employmentContracts = dbHelper.systemUserEmploymentContract.GetBySystemUserId(_defaultLoginUserID);

                foreach (var employmentContract in employmentContracts)
                {
                    var suspensions = dbHelper.systemUserSuspension.GetSystemUserSuspensionBySystemUserId(_defaultLoginUserID);

                    foreach (var suspension in suspensions)
                    {
                        dbHelper.systemUserSuspension.DeleteSystemUserSuspension(suspension);
                    }
                    dbHelper.systemUserEmploymentContract.DeleteSystemUserEmploymentContract(employmentContract);
                }

                contractName = _careProviderStaffRoleType_Name + " (" + _employmentContractTypeName + ") - " + DateTime.Now.Date.ToString("dd'/'MM'/'yyyy");

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                this.ShutDownAllProcesses();

                throw;
            }

        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-19419

        [TestProperty("JiraIssueID", "ACC-3099")]
        [Description("Login CD as a Care Provider." +
            "Verify whether the alert message is displayed or not when no employment contracts are available for the system user." +
            "Verify that if schedule availability is not  created for the Employment contracts then it should display the Empty View Diary screen with the employment contracts in View Dairy screen." +
            "Verify whether all the 4 different contracts are displaying in the View dairy screen/Manage Ad hoc screen." +
            "Verify whether user can add ad hoc slots for all the 4 different contracts." +
            "Verify that user able to Edit the availability type of Ad hoc slot in the View Dairy/ Manage Ad hoc screen." +
            "Verify that user able to Edit the availability type for Existing Scheduled slot in the View Dairy/ Manage Ad hoc screen.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("BusinessModule2", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "View Diary Manage Ad Hoc")]
        public void ViewDiaryManageAdHoc_UITestCase01()
        {
            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            #endregion

            #region Step 2

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_loginUsername)
                .ClickSearchButton()
                .OpenRecord(_defaultLoginUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateViewDiary_ManageAdHocAreaDislayed();

            #endregion

            #region Step 3

            systemUserAvailabilitySubPage
                .ValidateSystemUserWarningMessage("Current system user does not have any employment contracts.")
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad();

            systemUserViewDiaryManageAdHocPage
                .ValidateAdHocRecordNotDisplayed(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), _currentDayOfTheWeek, contractName.ToUpper());

            #endregion

            #region Step 4

            _employmentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_defaultLoginUserID,
                    DateTime.Now, _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid);
            if (_employmentContractId == Guid.Empty)
            {
                _employmentContractId = dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByName(_careProviderStaffRoleType_Name + " - " + new DateTime(2020, 1, 1).ToString("dd'/'MM'/'yyyy")).FirstOrDefault();

            }
            _employmentContractName = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_employmentContractId, "name")["name"];


            systemUserViewDiaryManageAdHocPage
                .ClickRefreshButton()
                .ClickScheduleAvailabilityCard();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateCreateScheduleAvailabilityButton_Monday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Tuesday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Wednesday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Thursday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Friday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Saturday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Sunday(_employmentContractName.ToUpper());

            systemUserAvailabilitySubPage
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickRefreshButton()
                .ValidateCreateScheduleAvailabilityButton_Monday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Tuesday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Wednesday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Thursday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Friday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Saturday(_employmentContractName.ToUpper());

            #endregion

            #region Step 5
            _employmentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_defaultLoginUserID,
                    DateTime.Now.AddDays(2), _careProviderStaffRoleTypeid2, _careProviders_TeamId, _employmentContractTypeid2, null);
            _employmentContract2Name = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_employmentContractId2, "name")["name"];

            _employmentContractId3 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_defaultLoginUserID,
                    DateTime.Now.AddDays(-2), _careProviderStaffRoleTypeid2, _careProviders_TeamId, _employmentContractTypeid2, DateTime.Now.Date);
            _employmentContract3Name = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_employmentContractId3, "name")["name"];

            _employmentContractId4 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_defaultLoginUserID,
                    new DateTime(2020, 1, 1), _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid);
            _employmentContract4Name = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_employmentContractId4, "name")["name"];

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickRefreshButton()
                .ValidateCreateScheduleAvailabilityButton_Monday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Tuesday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Wednesday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Thursday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Friday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Saturday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Sunday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Monday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Tuesday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Wednesday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Thursday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Friday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Saturday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Sunday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Monday(_employmentContract3Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Tuesday(_employmentContract3Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Wednesday(_employmentContract3Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Thursday(_employmentContract3Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Friday(_employmentContract3Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Saturday(_employmentContract3Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Sunday(_employmentContract3Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Monday(_employmentContract4Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Tuesday(_employmentContract4Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Wednesday(_employmentContract4Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Thursday(_employmentContract4Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Friday(_employmentContract4Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Saturday(_employmentContract4Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Sunday(_employmentContract4Name.ToUpper());

            #endregion

            #region Step 6, Step 7

            _userWorkScheduleId1 = dbHelper.userWorkSchedule.CreateUserWorkSchedule("Ad-Hoc: CDV6_1", _defaultLoginUserID, _careProviders_TeamId, _recurrencePatternId, _employmentContractId, _availabilityTypeId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), null, true);
            string _userWorkSchedule1_Name = (string)dbHelper.userWorkSchedule.GetUserWorkScheduleByID(_userWorkScheduleId1, "title")["title"];

            _userWorkScheduleId2 = dbHelper.userWorkSchedule.CreateUserWorkSchedule("Ad-Hoc: CDV6_2", _defaultLoginUserID, _careProviders_TeamId, _recurrencePatternId, _employmentContractId2, _availabilityTypeId_Regular, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), null, true);
            string _userWorkSchedule2_Name = (string)dbHelper.userWorkSchedule.GetUserWorkScheduleByID(_userWorkScheduleId2, "title")["title"];

            _userWorkScheduleId3 = dbHelper.userWorkSchedule.CreateUserWorkSchedule("Ad-Hoc: CDV6_3", _defaultLoginUserID, _careProviders_TeamId, _recurrencePatternId, _employmentContractId3, _availabilityTypeId_Regular, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), null, true);
            string _userWorkSchedule3_Name = (string)dbHelper.userWorkSchedule.GetUserWorkScheduleByID(_userWorkScheduleId3, "title")["title"];

            _userWorkScheduleId4 = dbHelper.userWorkSchedule.CreateUserWorkSchedule("Ad-Hoc: CDV6_4", _defaultLoginUserID, _careProviders_TeamId, _recurrencePatternId, _employmentContractId4, _availabilityTypeId_overtime, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0), null, true);
            string _userWorkSchedule4_Name = (string)dbHelper.userWorkSchedule.GetUserWorkScheduleByID(_userWorkScheduleId4, "title")["title"];

            systemUserViewDiaryManageAdHocPage
                .ClickRefreshButton()
                .ValidateCreatedAdHocSlot(DateTime.Now.ToString("dd'/'MM'/'yyyy"), _employmentContract4Name)
                .ClickScheduleAvailabilityCard();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateCreateScheduleAvailabilityButton_Monday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Tuesday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Wednesday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Thursday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Friday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Saturday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Sunday(_employmentContractName.ToUpper());

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickRefreshButton();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateCreatedAdHocSlot(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), _employmentContract4Name)
                .ValidateCreatedAdHocSlot(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), _employmentContract3Name)
                .ValidateCreatedAdHocSlot(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), _employmentContract2Name)
                .ValidateCreatedAdHocSlot(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), _employmentContractName);

            #endregion

            #region Step 8

            var newPastDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-1).Date);
            var newFutureDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(2).Date);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule("Ad-Hoc: CDV6_5", _defaultLoginUserID, _careProviders_TeamId, _recurrencePatternId, _employmentContractId3, _availabilityTypeId_Regular, newPastDate, newPastDate, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), null, true);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule("Ad-Hoc: CDV6_6", _defaultLoginUserID, _careProviders_TeamId, _recurrencePatternId, _employmentContractId2, _availabilityTypeId_Regular, newFutureDate, newFutureDate, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), null, true);

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickRefreshButton()
                .ValidateCreatedAdHocSlot(pastDate, _employmentContract3Name)
                .ValidateCreatedAdHocSlot(futureDate, _employmentContract2Name);

            #endregion

            #region Step 9

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickRefreshButton()
                .ClickCreatedScheduleAvailability(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), _employmentContract4Name);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickRefreshButton()
                .ValidateCreatedAdHocSlot(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), _employmentContract4Name);

            System.Threading.Thread.Sleep(3000);

            var userWorkSchedule4_updated_AvailabilityType = dbHelper.userWorkSchedule.GetUserWorkScheduleByID(_userWorkScheduleId1, "AvailabilityTypesId");
            Assert.AreEqual(_availabilityTypeId.ToString(), userWorkSchedule4_updated_AvailabilityType["availabilitytypesid"].ToString());

            #endregion

            #region Step 10       

            var tomorrowDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(1).Date);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule("CDV6_7", _defaultLoginUserID, _careProviders_TeamId, _recurrencePatternId_weekly, _employmentContractId, _availabilityTypeId, tomorrowDate, null, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), 1, false);


            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickRefreshButton()
                .ValidateCreatedAdHocSlot(DateTime.Now.AddDays(1).Date.ToString("dd'/'MM'/'yyyy"), _employmentContractName); ;

            systemUserViewDiaryManageAdHocPage
                .ClickScheduleAvailabilityCard();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton()
                .ValidateCreatedScheduleAvailabilitySlot(DateTime.Now.AddDays(1).Date.ToString("dd'/'MM'/'yyyy"), _employmentContractName);

            systemUserAvailabilitySubPage
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickRefreshButton()
                .ClickCreatedScheduleAvailability(DateTime.Now.AddDays(1).Date.ToString("dd'/'MM'/'yyyy"), _employmentContractName);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("OverTime");

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickRefreshButton()
                .ValidateCreatedAdHocSlot(DateTime.Now.AddDays(1).Date.ToString("dd'/'MM'/'yyyy"), _employmentContractName);

            Guid existingAdHoWorkSchedule = dbHelper.userWorkSchedule.GetUserWorkScheduleByTitle("Ad-Hoc: " + _employmentContractName + ", Overtime")[0];

            var userWorkSchedule7_updated_AvailabilityType = dbHelper.userWorkSchedule.GetUserWorkScheduleByID(existingAdHoWorkSchedule, "AvailabilityTypesId");
            Assert.AreEqual(_availabilityTypeId_overtime.ToString(), userWorkSchedule7_updated_AvailabilityType["availabilitytypesid"].ToString());

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-19421

        [TestProperty("JiraIssueID", "ACC-3100")]
        [Description("Login CD as a Care Provider." +
            "Verify whether user able to merge the same availability type for Ad hoc in the View Dairy/ Manage Ad hoc screen." +
            "Verify whether user able to merge the same availability type for Existing scheduled slot in the View Dairy/ Manage Ad hoc screen." +
            "Verify whether system should not allow user to merge the different type of availability in the View Dairy/ Manage Ad hoc screen. " +
            "Verify whether system should allow user to Extend/Shrink the time slot for Ad hoc in the View Dairy/ Manage Ad hoc screen." +
            "Verify whether system should allow user to extend/shrink the Existing schedule slot in the View Dairy/ Manage Ad hoc screen.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("BusinessModule2", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "View Diary Manage Ad Hoc")]
        public void ViewDiaryManageAdHoc_UITestCase02()
        {
            #region Employment Contract

            _employmentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_defaultLoginUserID,
                    DateTime.Now, _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid);
            if (_employmentContractId == Guid.Empty)
                _employmentContractId = dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByName(_careProviderStaffRoleType_Name + " - " + new DateTime(2020, 1, 1).ToString("dd'/'MM'/'yyyy")).FirstOrDefault();
            _employmentContractName = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_employmentContractId, "name")["name"];

            #endregion

            #region Step 11

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_loginUsername)
                .ClickSearchButton()
                .OpenRecord(_defaultLoginUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateViewDiary_ManageAdHocAreaDislayed();

            systemUserAvailabilitySubPage
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateCreateScheduleAvailabilityButton_Monday(_employmentContractName.ToUpper())
                .ClickCreateScheduleAvailability(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), _employmentContractName.ToUpper());

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityRegularButton();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickCreateScheduleAvailability(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), 2, 3);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityRegularButton();
            System.Threading.Thread.Sleep(3000);

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .DragAndDropAdHocSlotFromLeftToRight(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), 4, 2);
            System.Threading.Thread.Sleep(5000);

            Guid AdHoWorkScheduleId = dbHelper.userWorkSchedule.GetUserWorkScheduleByTitle("Ad-Hoc: " + _employmentContractName + ", Regular")[0];

            var userWorkSchedule_Merge_Time = dbHelper.userWorkSchedule.GetUserWorkScheduleByID(AdHoWorkScheduleId, "starttime", "endtime");
            Assert.AreEqual("09:00", userWorkSchedule_Merge_Time["starttime"].ToString().Substring(0, 5));
            Assert.AreEqual("23:30", userWorkSchedule_Merge_Time["endtime"].ToString().Substring(0, 5));

            #endregion

            #region Step 12

            systemUserAvailabilitySubPage
                .ClickScheduleAvailabilityCard()
                .ClickCreateScheduleAvailabilityButtonByDate(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), 1, _employmentContractName.ToUpper());

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickCreateScheduleAvailability(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), 2, 3);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            System.Threading.Thread.Sleep(3000);

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .DragAndDropAdHocSlotFromLeftToRight(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), 4, 2);
            System.Threading.Thread.Sleep(5000);

            AdHoWorkScheduleId = dbHelper.userWorkSchedule.GetUserWorkScheduleByTitle("Ad-Hoc: " + _employmentContractName + ", Standard").Last();

            userWorkSchedule_Merge_Time = dbHelper.userWorkSchedule.GetUserWorkScheduleByID(AdHoWorkScheduleId, "starttime", "endtime");
            Assert.AreEqual("09:00", userWorkSchedule_Merge_Time["starttime"].ToString().Substring(0, 5));
            Assert.AreEqual("23:30", userWorkSchedule_Merge_Time["endtime"].ToString().Substring(0, 5));

            #endregion

            #region Step 13

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .InsertGoToDate(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .ClickCreateScheduleAvailability(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"), _employmentContractName.ToUpper());

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Regular");

            System.Threading.Thread.Sleep(3000);

            var regularUserWorkScheduleId = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID_StartDate_Title(_defaultLoginUserID, DateTime.Now.AddDays(2).ToString("yyyy-MM-dd"), "Ad-Hoc: " + _employmentContractName + ", Regular").Last();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickCreateScheduleAvailability(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"), 2, 3);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("OverTime");

            System.Threading.Thread.Sleep(3000);

            var overtimeUserWorkScheduleId = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID_StartDate_Title(_defaultLoginUserID, DateTime.Now.AddDays(2).ToString("yyyy-MM-dd"), "Ad-Hoc: " + _employmentContractName + ", Overtime").Last();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .DragAndDropAdHocSlotFromLeftToRight(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"), 4, 2);
            System.Threading.Thread.Sleep(5000);

            var regularUserWorkSchedule_Time = dbHelper.userWorkSchedule.GetUserWorkScheduleByID(regularUserWorkScheduleId, "starttime", "endtime");
            var overtimeUserWorkSchedule_Time = dbHelper.userWorkSchedule.GetUserWorkScheduleByID(overtimeUserWorkScheduleId, "starttime", "endtime");

            Assert.AreEqual("09:00", regularUserWorkSchedule_Time["starttime"].ToString().Substring(0, 5));
            Assert.AreEqual("17:00", regularUserWorkSchedule_Time["endtime"].ToString().Substring(0, 5));
            Assert.AreEqual("17:00", overtimeUserWorkSchedule_Time["starttime"].ToString().Substring(0, 5));
            Assert.AreEqual("23:30", overtimeUserWorkSchedule_Time["endtime"].ToString().Substring(0, 5));

            #endregion

            #region Delete User Work Schedules for System User

            foreach (var workScheduleId in dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_defaultLoginUserID))
                dbHelper.userWorkSchedule.DeleteUserWorkSchedule(workScheduleId);

            #endregion

            #region Step 14

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickRefreshButton()
                .InsertGoToDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickCreateScheduleAvailability(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), _employmentContractName.ToUpper());

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Regular");

            System.Threading.Thread.Sleep(3000);

            var scheduledId = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID_StartDate_Title(_defaultLoginUserID, DateTime.Now.Date.ToString("yyyy-MM-dd"), "Ad-Hoc: " + _employmentContractName + ", Regular").Last();
            regularUserWorkSchedule_Time = dbHelper.userWorkSchedule.GetUserWorkScheduleByID(scheduledId, "starttime", "endtime");

            Assert.AreEqual("09:00", regularUserWorkSchedule_Time["starttime"].ToString().Substring(0, 5));
            Assert.AreEqual("17:00", regularUserWorkSchedule_Time["endtime"].ToString().Substring(0, 5));

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .DragSpecificAdHocSlotToRight(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), 2);
            System.Threading.Thread.Sleep(5000);

            regularUserWorkSchedule_Time = dbHelper.userWorkSchedule.GetUserWorkScheduleByID(scheduledId, "starttime", "endtime");

            Assert.AreEqual("09:00", regularUserWorkSchedule_Time["starttime"].ToString().Substring(0, 5));
            Assert.AreEqual("17:15", regularUserWorkSchedule_Time["endtime"].ToString().Substring(0, 5));

            #endregion

            #region Step 15

            systemUserAvailabilitySubPage
                .ClickScheduleAvailabilityCard()
                .ClickCreateScheduleAvailabilityButtonByDate(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), 1, _employmentContractName.ToUpper());

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("OverTime");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .DragSpecificAdHocSlotToRight(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), 2);
            System.Threading.Thread.Sleep(5000);

            scheduledId = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID_StartDate_Title(_defaultLoginUserID, DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"), "Ad-Hoc: " + _employmentContractName + ", Overtime").Last();
            overtimeUserWorkSchedule_Time = dbHelper.userWorkSchedule.GetUserWorkScheduleByID(scheduledId, "starttime", "endtime");

            Assert.AreEqual("09:00", overtimeUserWorkSchedule_Time["starttime"].ToString().Substring(0, 5));
            Assert.AreEqual("17:15", overtimeUserWorkSchedule_Time["endtime"].ToString().Substring(0, 5));

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3101")]
        [Description("Login CD as a Care Provider." +
            "Verify whether user able to remove the Ad hoc slots by clicking on remove time slot in the View Dairy/ Manage Ad hoc screen." +
            "Verify whether user able to remove the scheduled slots by clicking on remove time slot in the View Dairy/ Manage Ad hoc screen." +
            "If you have two slots of the same type with a 15-minute gap between them and you add a new slot of the same type in that gap, the diary will automatically merge all 3 slots." +
            "Verify whether the changes made in view dairy screen is impacting in schedule Availability screen." +
            "Verify whether if user did any ad hoc changes then it is only for the specific date.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("BusinessModule2", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "View Diary Manage Ad Hoc")]
        public void ViewDiaryManageAdHoc_UITestCase03()
        {
            #region Step 16

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_loginUsername)
                .ClickSearchButton()
                .OpenRecord(_defaultLoginUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateViewDiary_ManageAdHocAreaDislayed()
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad();

            _employmentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_defaultLoginUserID,
                    DateTime.Now, _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid);
            if (_employmentContractId == Guid.Empty)
            {
                _employmentContractId = dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByName(_careProviderStaffRoleType_Name + " - " + new DateTime(2020, 1, 1).ToString("dd'/'MM'/'yyyy")).FirstOrDefault();

            }
            _employmentContractName = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_employmentContractId, "name")["name"];

            systemUserAvailabilitySubPage
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickRefreshButton()
                .ValidateCreateScheduleAvailabilityButton_Monday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Tuesday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Wednesday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Thursday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Friday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Saturday(_employmentContractName.ToUpper());


            _userWorkScheduleId1 = dbHelper.userWorkSchedule.CreateUserWorkSchedule("Ad-Hoc: CDV6_1", _defaultLoginUserID, _careProviders_TeamId, _recurrencePatternId, _employmentContractId, _availabilityTypeId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), null, true);
            //string _userWorkSchedule1_Name = (string)dbHelper.userWorkSchedule.GetUserWorkScheduleByID(_userWorkScheduleId1, "title")["title"];

            systemUserViewDiaryManageAdHocPage
                .ClickRefreshButton()
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateCreatedAdHocSlot(DateTime.Now.ToString("dd'/'MM'/'yyyy"), _employmentContractName)
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickCreatedScheduleAvailability(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), _employmentContractName);

            createScheduledAvailabilityPopup
                .WaitForCreateScheduledAvailabilityPopupToLoad(false)
                .ClickRemoveTimeSlotButton();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickRefreshButton()
                .ValidateDefaultAdHocSlotIsDisplayed(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), _currentDayOfTheWeek, _employmentContractName.ToUpper());

            #endregion

            #region Step 17

            _userWorkScheduleId2 = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _defaultLoginUserID, _careProviders_TeamId, _recurrencePatternId_weekly, _employmentContractId, _availabilityTypeId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), 1, false);

            systemUserViewDiaryManageAdHocPage
                .ClickScheduleAvailabilityCard();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton()
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickRefreshButton()
                .ValidateCreatedAdHocSlot(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), _employmentContractName)
                .ClickCreatedScheduleAvailability(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), _employmentContractName);

            createScheduledAvailabilityPopup
                .WaitForCreateScheduledAvailabilityPopupToLoad(false)
                .ClickRemoveTimeSlotButton();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickRefreshButton()
                .ValidateDefaultAdHocSlotIsDisplayed(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), DateTime.Now.AddDays(1).DayOfWeek.ToString(), _employmentContractName.ToUpper())
                .ValidateCreatedAdHocSlot(DateTime.Now.AddDays(8).ToString("dd'/'MM'/'yyyy"), _employmentContractName)
                .ValidateCreatedAdHocSlot(DateTime.Now.AddDays(15).ToString("dd'/'MM'/'yyyy"), _employmentContractName);

            #endregion

            #region Step 18

            var baseDate = DateTime.Today.AddDays(2);
            var startDate = new DateTime(baseDate.Year, baseDate.Month, baseDate.Day);
            _userWorkScheduleId3 = dbHelper.userWorkSchedule.CreateUserWorkSchedule("Ad-Hoc: CDV6_3", _defaultLoginUserID, _careProviders_TeamId, _recurrencePatternId, _employmentContractId, _availabilityTypeId, startDate, startDate, new TimeSpan(9, 0, 0), new TimeSpan(13, 0, 0), null, true);
            _userWorkScheduleId4 = dbHelper.userWorkSchedule.CreateUserWorkSchedule("Ad-Hoc: CDV6_4", _defaultLoginUserID, _careProviders_TeamId, _recurrencePatternId, _employmentContractId, _availabilityTypeId, startDate, startDate, new TimeSpan(13, 15, 0), new TimeSpan(17, 0, 0), null, true);

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickRefreshButton()
                .ClickCreateAdhocScheduleAvailabilityButton(startDate.ToString("dd'/'MM'/'yyyy"), 3);

            createScheduledAvailabilityPopup
                .WaitForCreateScheduledAvailabilityPopupToLoad(true)
                .ClickAvailabilityButton("Standard");

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateCreatedAdHocSlot(startDate.ToString("dd'/'MM'/'yyyy"), _employmentContractName);

            System.Threading.Thread.Sleep(2000);

            //var userWorkSchedule = dbHelper.userWorkSchedule.GetUserWorkScheduleByTitle("Ad-hoc: "+ _employmentContractName+ ", Standard").Last();

            var userWorkSchedule = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_defaultLoginUserID).Last();

            var userWorkScheduleFields = dbHelper.userWorkSchedule.GetUserWorkScheduleByID(userWorkSchedule, "starttime", "endtime");
            Assert.AreEqual("09:00", DateTime.Parse(userWorkScheduleFields["starttime"].ToString()).ToShortTimeString());
            Assert.AreEqual("17:00", DateTime.Parse(userWorkScheduleFields["endtime"].ToString()).ToShortTimeString());

            #endregion

            #region Step 19 and 20

            systemUserViewDiaryManageAdHocPage
                .ClickScheduleAvailabilityCard();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton()
                .ValidateCreateScheduleAvailabilitySlotByDateIsVisible(startDate.ToString("dd'/'MM'/'yyyy"), 1, _employmentContractName.ToUpper())
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateDefaultAdHocSlotIsDisplayed(DateTime.Now.AddDays(9).ToString("dd'/'MM'/'yyyy"), DateTime.Now.AddDays(9).DayOfWeek.ToString(), _employmentContractName.ToUpper())
                .ValidateDefaultAdHocSlotIsDisplayed(DateTime.Now.AddDays(16).ToString("dd'/'MM'/'yyyy"), DateTime.Now.AddDays(16).DayOfWeek.ToString(), _employmentContractName.ToUpper());

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3102")]
        [Description("Login CD as a Care Provider." +
            "Verify create, updated changes will be displayed in the Audit list." +
            "Verify created Schedule Availability record should display in the View Dairy/ Manage Ad hoc screen again create Ad Hoc slot and validate work schedule." +
            "Verify whether updated scheduled slot record should not display in the View Dairy/ Manage Ad hoc screen for Same Date but Updated slot should be display for next Day of week." +
            "Verify whether Removed scheduled slot record should not reflect in the View Dairy/ Manage Ad hoc screen for same Date but should be removed slot for next Day of week.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("BusinessModule2", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "View Diary Manage Ad Hoc")]
        public void ViewDiaryManageAdHoc_UITestCase04()
        {
            #region Employment Contract

            _employmentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_defaultLoginUserID,
                    DateTime.Now, _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid);
            if (_employmentContractId == Guid.Empty)
                _employmentContractId = dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByName(_careProviderStaffRoleType_Name + " - " + new DateTime(2020, 1, 1).ToString("dd'/'MM'/'yyyy")).FirstOrDefault();
            _employmentContractName = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_employmentContractId, "name")["name"];

            #endregion

            #region Step 21

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);

            _userWorkScheduleId1 = dbHelper.userWorkSchedule.CreateUserWorkSchedule("Ad-Hoc: CDV6_1", _defaultLoginUserID, _careProviders_TeamId, _recurrencePatternId, _employmentContractId, _availabilityTypeId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), null, true);

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_loginUsername)
                .ClickSearchButton()
                .OpenRecord(_defaultLoginUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickRefreshButton()
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickCreatedScheduleAvailability(DateTime.Now.ToString("dd'/'MM'/'yyyy"), _employmentContractName);

            createScheduledAvailabilityPopup
                .WaitForCreateScheduledAvailabilityPopupToLoad(false)
                .ClickAvailabilityButton("OverTime");

            System.Threading.Thread.Sleep(2500);

            var auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                AllowMultiSelect = "false",
                CurrentPage = "1",
                IsGeneralAuditSearch = false,
                Operation = 1, //Create operation
                PageNumber = 1,
                RecordsPerPage = "100",
                TypeName = "audit",
                ParentTypeName = "userworkschedule",
                UsePaging = true,
                ParentId = _userWorkScheduleId1.ToString(),
                ViewGroup = "1",
                ViewType = "0",
                Year = "Last 90 Days",
            };

            WebAPIHelper.Security.Authenticate(tenantName, username, password);
            var auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);

            Assert.AreEqual(1, auditResponseData.GridData.Count);
            Assert.AreEqual("Created", auditResponseData.GridData[0].cols[0].Text);

            auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                AllowMultiSelect = "false",
                CurrentPage = "1",
                IsGeneralAuditSearch = false,
                Operation = 2, //Update operation
                PageNumber = 1,
                RecordsPerPage = "100",
                TypeName = "audit",
                ParentTypeName = "userworkschedule",
                UsePaging = true,
                ParentId = _userWorkScheduleId1.ToString(),
                ViewGroup = "1",
                ViewType = "0",
                Year = "Last 90 Days",
            };

            WebAPIHelper.Security.Authenticate(tenantName, username, password);
            auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);

            Assert.AreEqual(1, auditResponseData.GridData.Count);
            Assert.AreEqual("Updated", auditResponseData.GridData[0].cols[0].Text);

            #endregion

            #region Step 22

            systemUserViewDiaryManageAdHocPage
                 .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                 .ClickScheduleAvailabilityCard();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleAvailabilityButtonByDate(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), 1, _employmentContractName.ToUpper());

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityRegularButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickCreateAdhocScheduleAvailabilityButton(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), 3);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityRegularButton();

            System.Threading.Thread.Sleep(3000);

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);

            var workScheduleId1 = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID_StartDate_Title(_defaultLoginUserID, DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"), "AutoGenerated");
            var workScheduleId2 = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID_StartDate_Title(_defaultLoginUserID, DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"), "Ad-Hoc: " + _employmentContractName + ", Regular");
            Assert.AreEqual(1, workScheduleId1.Count);
            Assert.AreEqual(2, workScheduleId2.Count);

            #endregion

            #region Step 23

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickScheduleAvailabilityCard();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), 2);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("OverTime");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad();

            Guid workScheduleId = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID_StartDate_Title(_defaultLoginUserID, DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"), "AutoGenerated").Last();
            var userWorkSchedule_updated_AvailabilityType = dbHelper.userWorkSchedule.GetUserWorkScheduleByID(workScheduleId, "AvailabilityTypesId");

            Assert.AreEqual(_availabilityTypeId_overtime.ToString(), userWorkSchedule_updated_AvailabilityType["availabilitytypesid"].ToString());

            workScheduleId2 = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID_StartDate_Title(_defaultLoginUserID, DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"), "Ad-Hoc: " + _employmentContractName + ", Overtime");
            Assert.AreEqual(0, workScheduleId2.Count);

            systemUserViewDiaryManageAdHocPage
                .ValidateTooltipOnMouseHoverForAdHocSlot(DateTime.Now.AddDays(8).ToString("dd'/'MM'/'yyyy"), 2, "Test_User CDV6_19419 - Role_19419 (OverTime): 09:00 – 17:00");

            #endregion

            #region Step 24

            systemUserViewDiaryManageAdHocPage
                .ClickScheduleAvailabilityCard();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), 2);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickRemoveTimeSlotButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad();

            var autogeneratedWorkSchedule = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID_StartDate_Title(_defaultLoginUserID, DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"), "AutoGenerated");
            Assert.AreEqual(0, autogeneratedWorkSchedule.Count);

            systemUserViewDiaryManageAdHocPage
                .InsertGoToDate(DateTime.Now.AddDays(8).ToString("dd'/'MM'/'yyyy"))
                .ValidateAdHocRecordNotDisplayed(DateTime.Now.AddDays(8).ToString("dd'/'MM'/'yyyy"), _currentDayOfTheWeek, contractName.ToUpper());

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3103")]
        [Description("Login CD as a Care Provider." +
            "Verify whether user Add new contract it will be will be displayed for the Ad hoc change day." +
            "Verify whether Create schedule slot and remove this slot from Ad Hoc should not impact in Schedule availability Slot record." +
            "Verify whether updated scheduled availability slot and validate updated slot should not be impact for seleted date record in Ad Hoc.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("BusinessModule2", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "View Diary Manage Ad Hoc")]
        public void ViewDiaryManageAdHoc_UITestCase05()
        {
            #region Employment Contract

            _employmentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_defaultLoginUserID,
                    DateTime.Now, _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid);
            if (_employmentContractId == Guid.Empty)
                _employmentContractId = dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByName(_careProviderStaffRoleType_Name + " - " + new DateTime(2020, 1, 1).ToString("dd'/'MM'/'yyyy")).FirstOrDefault();
            _employmentContractName = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_employmentContractId, "name")["name"];

            #endregion

            #region Step 25

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_loginUsername)
                .ClickSearchButton()
                .OpenRecord(_defaultLoginUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleAvailabilityButtonByDate(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), 1, _employmentContractName.ToUpper());

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityRegularButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateCreatedAdHocSlot(DateTime.Now.ToString("dd'/'MM'/'yyyy"), _employmentContractName);

            #region Employment Contract 2

            _employmentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_defaultLoginUserID,
                   DateTime.Now.AddDays(2), _careProviderStaffRoleTypeid2, _careProviders_TeamId, _employmentContractTypeid2, null);
            _employmentContract2Name = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_employmentContractId2, "name")["name"];

            #endregion

            systemUserViewDiaryManageAdHocPage
                .ClickScheduleAvailabilityCard();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton()
                .ClickCreateScheduleAvailabilityButtonByDate(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), 1, _employmentContract2Name.ToUpper());

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickRefreshButton()
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateCreatedAdHocSlot(DateTime.Now.ToString("dd'/'MM'/'yyyy"), _employmentContract2Name);

            #endregion

            #region Step 26

            systemUserViewDiaryManageAdHocPage
                .ClickCreatedScheduleAvailabilityButton(DateTime.Now.ToString("dd'/'MM'/'yyyy"), _employmentContract2Name);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickRemoveTimeSlotButton();
            System.Threading.Thread.Sleep(3000);

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickScheduleAvailabilityCard();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton()
                .ClickCreatedScheduledAvailabilityButton(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), _employmentContract2Name);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityOvertimeButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickRefreshButton()
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateDefaultAdHocSlotIsDisplayed(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), _currentDayOfTheWeek, _employmentContract2Name.ToUpper());

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-255

        [TestProperty("JiraIssueID", "ACC-3104")]
        [Description("Test automation for step 1 to step 19 from original jira test CDV6-14361")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("BusinessModule2", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "View Diary Manage Ad Hoc")]
        public void ViewDiaryManageAdHoc_UITestCase06()
        {
            #region Step 5

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_loginUsername)
                .ClickSearchButton()
                .OpenRecord(_defaultLoginUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateAdHocRecordNotDisplayed(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), _currentDayOfTheWeek, contractName.ToUpper())
                .ValidateGoToDateFieldText(commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("dd'/'MM'/'yyyy"));

            #endregion

            #region Step 6

            _employmentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_defaultLoginUserID,
                    DateTime.Now, _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid, null, "test1");
            if (_employmentContractId == Guid.Empty)
            {
                _employmentContractId = dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByName(_careProviderStaffRoleType_Name + " - " + new DateTime(2020, 1, 1).ToString("dd'/'MM'/'yyyy")).FirstOrDefault();

            }
            _employmentContractName = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_employmentContractId, "name")["name"];

            systemUserViewDiaryManageAdHocPage
                .ClickRefreshButton()
                .ClickScheduleAvailabilityCard();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateCreateScheduleAvailabilityButton_Monday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Tuesday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Wednesday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Thursday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Friday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Saturday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Sunday(_employmentContractName.ToUpper());

            systemUserAvailabilitySubPage
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateCreateScheduleAvailabilityButton_Monday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Tuesday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Wednesday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Thursday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Friday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Saturday(_employmentContractName.ToUpper());

            systemUserViewDiaryManageAdHocPage
                .ValidateFilterEmploymentContractPicklistIsDisplayed(true)
                .ValidateFilterAvailabilityTypePicklistIsDisplayed(true)
                .ValidateGoToDateFieldIsDisplayed(true);

            #endregion

            #region Step 7, 8 and 13, 14
            _employmentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_defaultLoginUserID,
                    DateTime.Now, _careProviderStaffRoleTypeid2, _careProviders_TeamId, _employmentContractTypeid2, null, "test2");
            _employmentContract2Name = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_employmentContractId2, "name")["name"];

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickRefreshButton()
                .ValidateCreateScheduleAvailabilityButton_Monday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Tuesday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Wednesday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Thursday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Friday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Saturday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Sunday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Monday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Tuesday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Wednesday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Thursday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Friday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Saturday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Sunday(_employmentContract2Name.ToUpper());

            #region TransportType

            string currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();
            var recurrencePatternTitle = "Occurs every 1 week(s) on " + DateTime.Now.DayOfWeek.ToString().ToLower();
            var recurrencePatternId = dbHelper.recurrencePattern.GetByTitle(recurrencePatternTitle).FirstOrDefault();
            var _transportTypeId_Car = dbHelper.transportType.GetTransportTypeByName("Car").First();
            var todayDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            #endregion

            dbHelper.userTransportationSchedule.CreateSystemUserTransportationSchedule(_careProviders_TeamId, _defaultLoginUserID, "AutoGenerated",
                todayDate, null, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), recurrencePatternId, _transportTypeId_Car);

            _userWorkScheduleId1 = dbHelper.userWorkSchedule.CreateUserWorkSchedule("Ad-Hoc: CDV6_1", _defaultLoginUserID, _careProviders_TeamId, _recurrencePatternId, _employmentContractId, _availabilityTypeId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), null, false);

            _userWorkScheduleId2 = dbHelper.userWorkSchedule.CreateUserWorkSchedule("Ad-Hoc: CDV6_2", _defaultLoginUserID, _careProviders_TeamId, _recurrencePatternId, _employmentContractId2, _availabilityTypeId_Regular, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), null, false);

            systemUserViewDiaryManageAdHocPage
                .ClickRefreshButton()
                .ValidateCreatedAdHocSlot(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), _employmentContract2Name)
                .ValidateCreatedAdHocSlot(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), _employmentContractName)
                .ValidateRecordUnderViewDiary_ManageAdHocArea(currentDayOfTheWeek, 2, "car");

            systemUserViewDiaryManageAdHocPage
                .SelectEmploymentContractFilter(_employmentContractName);

            systemUserViewDiaryManageAdHocPage
                .ValidateCreatedAdHocSlot(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), _employmentContractName)
                .ValidateAdHocRecordNotDisplayed(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), currentDayOfTheWeek, _employmentContract2Name);

            systemUserViewDiaryManageAdHocPage
                .SelectEmploymentContractFilter(_employmentContract2Name)
                .ValidateCreatedAdHocSlot(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), _employmentContract2Name)
                .ValidateAdHocRecordNotDisplayed(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), currentDayOfTheWeek, _employmentContractName);

            systemUserViewDiaryManageAdHocPage
                .SelectEmploymentContractFilter("All")
                .ValidateCreatedAdHocSlot(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), _employmentContract2Name)
                .ValidateCreatedAdHocSlot(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), _employmentContractName)
                .ValidateRecordUnderViewDiary_ManageAdHocArea(currentDayOfTheWeek, 2, "car");

            #endregion

            #region Step 18 and Step 19
            systemUserViewDiaryManageAdHocPage
                .SelectAvailabilityTypeFilter("Standard");

            systemUserViewDiaryManageAdHocPage
                .ValidateCreatedAdHocSlot(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), _employmentContractName)
                .ValidateAdHocRecordNotDisplayed(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), currentDayOfTheWeek, _employmentContract2Name)
                .ValidateDefaultAdHocSlotIsDisplayed(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), currentDayOfTheWeek, _employmentContract2Name.ToUpper());

            systemUserViewDiaryManageAdHocPage
                .SelectAvailabilityTypeFilter("Regular");

            systemUserViewDiaryManageAdHocPage
                .ValidateCreatedAdHocSlot(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), _employmentContract2Name)
                .ValidateAdHocRecordNotDisplayed(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), currentDayOfTheWeek, _employmentContractName)
                .ValidateDefaultAdHocSlotIsDisplayed(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), currentDayOfTheWeek, _employmentContractName.ToUpper());

            #endregion

            #region Step 9

            dbHelper.userWorkSchedule.DeleteUserWorkSchedule(_userWorkScheduleId1);
            dbHelper.userWorkSchedule.DeleteUserWorkSchedule(_userWorkScheduleId2);

            systemUserViewDiaryManageAdHocPage
                .ClickRefreshButton()
                .ValidateAdHocRecordNotDisplayed(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), currentDayOfTheWeek, _employmentContractName)
                .ValidateAdHocRecordNotDisplayed(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), currentDayOfTheWeek, _employmentContract2Name)
                .ValidateRecordUnderViewDiary_ManageAdHocArea(currentDayOfTheWeek, 2, "car");

            #endregion

            #region Step 10 and Step 15
            systemUserViewDiaryManageAdHocPage
                .ValidateFilterEmploymentContractSelectedText("All")
                .ValidateFilterAvailabilityTypeSelectedText("All");

            #endregion

            #region Step 11 and Step 16
            systemUserViewDiaryManageAdHocPage
                .ValidateFilterEmploymentContractPicklistContainsText(_employmentContractName)
                .ValidateFilterEmploymentContractPicklistContainsText(_employmentContract2Name);

            systemUserViewDiaryManageAdHocPage
                .ValidateFilterAvailabilityTypePicklistContainsText("Standard")
                .ValidateFilterAvailabilityTypePicklistContainsText("Regular")
                .ValidateFilterAvailabilityTypePicklistContainsText("OverTime");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3105")]
        [Description("Test automation for step 20 from original jira test CDV6-14361")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("BusinessModule2", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "View Diary Manage Ad Hoc")]
        public void ViewDiaryManageAdHoc_UITestCase07()
        {

            #region Step 20

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            string currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();
            _recurrencePatternId_weekly = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on " + currentDayOfTheWeek).FirstOrDefault();
            _employmentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_defaultLoginUserID,
                    DateTime.Now, _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid, null, "test1");
            if (_employmentContractId == Guid.Empty)
            {
                _employmentContractId = dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByName(_careProviderStaffRoleType_Name + " - " + new DateTime(2020, 1, 1).ToString("dd'/'MM'/'yyyy")).FirstOrDefault();

            }
            _employmentContractName = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_employmentContractId, "name")["name"];


            _employmentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_defaultLoginUserID,
                    DateTime.Now, _careProviderStaffRoleTypeid2, _careProviders_TeamId, _employmentContractTypeid2, null, "test2");
            _employmentContract2Name = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_employmentContractId2, "name")["name"];

            _userWorkScheduleId1 = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _defaultLoginUserID, _careProviders_TeamId, _recurrencePatternId_weekly, _employmentContractId, _availabilityTypeId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), 1);

            _userWorkScheduleId2 = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _defaultLoginUserID, _careProviders_TeamId, _recurrencePatternId_weekly, _employmentContractId2, _availabilityTypeId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), 1);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_loginUsername)
                .ClickSearchButton()
                .OpenRecord(_defaultLoginUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickRefreshButton()
                .SelectEmploymentContractFilter(_employmentContractName)
                .SelectAvailabilityTypeFilter("Standard");

            systemUserViewDiaryManageAdHocPage
                .ValidateCreatedAdHocSlot(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), _employmentContractName)
                .ValidateAdHocRecordNotDisplayed(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), currentDayOfTheWeek, _employmentContract2Name);

            systemUserViewDiaryManageAdHocPage
                .SelectEmploymentContractFilter(_employmentContract2Name)
                .SelectAvailabilityTypeFilter("Standard")
                .ValidateCreatedAdHocSlot(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), _employmentContract2Name)
                .ValidateAdHocRecordNotDisplayed(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), currentDayOfTheWeek, _employmentContractName);

            #endregion

            #region Step 21

            //Step 21 is same as Step 20

            #endregion            

        }

        #endregion

    }
}

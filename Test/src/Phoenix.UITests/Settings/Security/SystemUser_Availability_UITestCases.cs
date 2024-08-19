using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Security
{
    /// <summary>
    /// This class contains Automated UI test scripts for Schedule Availability
    /// </summary>
    [TestClass]
    public class SystemUser_Availability_UITestCases : FunctionalTest
    {
        #region Properties

        private string EnvironmentName;
        private Guid authenticationproviderid;
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _defaultLoginUserID;
        private Guid _systemUserID;
        private string _systemUserName;
        public Guid environmentid;
        private string _loginUser_Username;
        private Guid _transportTypeId_Car;
        private string partialStringSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");
        private string tenantName;

        #endregion


        [TestInitialize()]
        public void TestSetup()
        {
            try
            {
                #region Authentication

                authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").First();

                #endregion Authentication

                #region Environment Name

                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

                #region Business Unit
                var businessUnitExists = dbHelper.businessUnit.GetByName("CareProviders").Any();
                if (!businessUnitExists)
                    dbHelper.businessUnit.CreateBusinessUnit("CareProviders");
                _careProviders_BusinessUnitId = dbHelper.businessUnit.GetByName("CareProviders")[0];


                #endregion

                #region Language

                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
                if (!language)
                    dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
                _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

                #endregion Language

                #region Team

                var teamsExist = dbHelper.team.GetTeamIdByName("CareProviders").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");
                _careProviders_TeamId = dbHelper.team.GetTeamIdByName("CareProviders")[0];

                #endregion

                #region Create default system user

                _loginUser_Username = "SystemAvailabilityUser23";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUser_Username, "SystemAvailability", "User23", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
                #endregion

                #region Team Manager

                dbHelper.team.UpdateTeamManager(_careProviders_TeamId, _defaultLoginUserID);

                #endregion

                #region TransportType

                if (!dbHelper.transportType.GetTransportTypeByName("Car").Any())
                    dbHelper.transportType.CreateTransportType(_careProviders_TeamId, "Car", DateTime.Now.Date, 1, "50", 1);
                _transportTypeId_Car = dbHelper.transportType.GetTransportTypeByName("Car")[0];

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        private DateTime GetThisWeekFirstMonday()
        {
            DateTime dt = DateTime.Now;
            int diff = (7 + (dt.DayOfWeek - DayOfWeek.Monday)) % 7;
            var tempDate = dt.AddDays(-1 * diff).Date;

            return new DateTime(tempDate.Year, tempDate.Month, tempDate.Day);
        }

        private void CreateNewSystemUser(string userName, string userFirstName, string userLastName)
        {

            _systemUserID = dbHelper.systemUser.CreateSystemUser(userName, userFirstName, userLastName, userFirstName + " " + userLastName, "Passw0rd_!", userName + "@somemail.com", userName + "@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSATransportWeek1CycleStartDate(_systemUserID, GetThisWeekFirstMonday());


        }

        private void CreateTransportationTypesForSystemUser(bool CreateDifferentTranspoortationTypes = true)
        {
            var recurrencePatternTitle = "Occurs every 1 week(s) on " + DateTime.Now.DayOfWeek.ToString().ToLower();
            var recurrencePatternId = dbHelper.recurrencePattern.GetByTitle(recurrencePatternTitle).FirstOrDefault();
            var transportType_CarId = dbHelper.transportType.GetTransportTypeByName("Car").First();
            var todayDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            if (CreateDifferentTranspoortationTypes)
            {
                var transportType_BicycleId = dbHelper.transportType.GetTransportTypeByName("Bicycle").First();

                dbHelper.userTransportationSchedule.CreateSystemUserTransportationSchedule(_careProviders_TeamId, _systemUserID, "AutoGenerated", todayDate, null, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), recurrencePatternId, transportType_CarId);
                dbHelper.userTransportationSchedule.CreateSystemUserTransportationSchedule(_careProviders_TeamId, _systemUserID, "AutoGenerated", todayDate, null, new TimeSpan(17, 15, 0), new TimeSpan(23, 30, 0), recurrencePatternId, transportType_BicycleId);
            }
            else
            {
                dbHelper.userTransportationSchedule.CreateSystemUserTransportationSchedule(_careProviders_TeamId, _systemUserID, "AutoGenerated", todayDate, null, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), recurrencePatternId, transportType_CarId);
                dbHelper.userTransportationSchedule.CreateSystemUserTransportationSchedule(_careProviders_TeamId, _systemUserID, "AutoGenerated", todayDate, null, new TimeSpan(17, 15, 0), new TimeSpan(23, 30, 0), recurrencePatternId, transportType_CarId);
            }
        }

        private void CreateEmploymentContractsForSystemUser()
        {
            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = Guid.Empty;
            if (!dbHelper.careProviderStaffRoleType.GetByName("Helper").Any())
                _careProviderStaffRoleTypeid = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            if (_careProviderStaffRoleTypeid == Guid.Empty)
                _careProviderStaffRoleTypeid = dbHelper.careProviderStaffRoleType.GetByName("Helper").FirstOrDefault();

            #endregion

            var employmentContractType_Contracted_Id = dbHelper.employmentContractType.GetByName("Contracted").FirstOrDefault();
            var employmentContractType_Hourly_Id = dbHelper.employmentContractType.GetByName("Hourly").FirstOrDefault();
            var employmentContractType_Salaried_Id = dbHelper.employmentContractType.GetByName("Salaried").FirstOrDefault();
            var employmentContractType_Volunteer_Id = dbHelper.employmentContractType.GetByName("Volunteer").FirstOrDefault();


            //Helper - Contracted
            dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserID, DateTime.Now.Date, _careProviderStaffRoleTypeid, _careProviders_TeamId, employmentContractType_Contracted_Id, 48m);

            //Helper - Hourly
            dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserID, DateTime.Now.Date, _careProviderStaffRoleTypeid, _careProviders_TeamId, employmentContractType_Hourly_Id, 48m);

            //Helper - Salaried
            dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserID, DateTime.Now.Date, _careProviderStaffRoleTypeid, _careProviders_TeamId, employmentContractType_Salaried_Id, 48m);

            //Helper - Volunteer
            dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserID, DateTime.Now.Date, _careProviderStaffRoleTypeid, _careProviders_TeamId, employmentContractType_Volunteer_Id, 48m);
        }


        #region https://advancedcsg.atlassian.net/browse/CDV6-19412

        [TestProperty("JiraIssueID", "ACC-3527")]
        [Description("Steps 1 to 6 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("BusinessModule2", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Availability")]
        [TestProperty("Screen2", "View Diary Manage Ad Hoc")]
        [TestProperty("Screen3", "Schedule Transport")]
        public void SystemUser_Availability_ScheduleTransport_UITestCases001()
        {
            var currentTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            _systemUserName = "TestUser_CDV6_19412_" + currentTime;

            CreateNewSystemUser(_systemUserName, "TestUser_CDV6_19412", currentTime);

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2

            //Not possible to automate step 2. We are forbidden to activate or deactivate business modules 

            #endregion

            #region Step 3

            //Not possible to automate step 2. We are forbidden to activate or deactivate business modules 

            #endregion

            #region Step 4

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard()
                .ValidateScheduleTransportAreaDislayed();

            #endregion

            #region Step 5

            systemUserAvailabilitySubPage
                .ValidateSystemUserWarningMessage("Current system user does not have any employment contracts.")
                .ClickCreateScheduleTransportButton(DateTime.Now.DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ClickBackButton();

            #endregion

            #region Step 6

            systemUsersPage
                 .WaitForSystemUsersPageToLoad()
                 .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard()
                .ValidateScheduleTransportAreaDislayed();

            systemUserAvailabilitySubPage
                .ValidateSystemUserWarningMessage("Current system user does not have any employment contracts.")
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car", true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3528")]
        [Description("Steps 7 to 8 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("BusinessModule2", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Availability")]
        [TestProperty("Screen2", "View Diary Manage Ad Hoc")]
        [TestProperty("Screen3", "Schedule Transport")]
        public void SystemUser_Availability_ScheduleTransport_UITestCases002()
        {
            var currentTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            _systemUserName = "TestUser_CDV6_19412_" + currentTime;

            CreateNewSystemUser(_systemUserName, "TestUser_CDV6_19412", currentTime);

            #region Navigate to the system user Schedule Transport tab

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard()
                .ValidateScheduleTransportAreaDislayed();

            #endregion

            #region Step 7

            systemUserAvailabilitySubPage
                .ClickCreateScheduleTransportButton(DateTime.Now.DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButton(DateTime.Now.DayOfWeek.ToString(), 3);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickBicycleButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ClickBackButton();

            systemUsersPage
                 .WaitForSystemUsersPageToLoad()
                 .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard()
                .ValidateScheduleTransportAreaDislayed();

            systemUserAvailabilitySubPage
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "bicycle", true);

            #endregion

            #region Step 8

            systemUserAvailabilitySubPage
                .ClickCreateScheduleTransportButton(DateTime.Now.AddDays(1).DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ClickBackButton();

            systemUsersPage
                 .WaitForSystemUsersPageToLoad()
                 .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard()
                .ValidateScheduleTransportAreaDislayed();

            systemUserAvailabilitySubPage
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "bicycle", true)
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), "walking", true);

            systemUserAvailabilitySubPage
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad();

            systemUserViewDiaryManageAdHocPage
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car", true)
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "bicycle", true)
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), "walking", true);


            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3529")]
        [Description("Steps 9 to 10 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("BusinessModule2", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Availability")]
        [TestProperty("Screen2", "View Diary Manage Ad Hoc")]
        [TestProperty("Screen3", "Schedule Transport")]
        public void SystemUser_Availability_ScheduleTransport_UITestCases003()
        {
            var currentTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            _systemUserName = "TestUser_CDV6_19412_" + currentTime;

            CreateNewSystemUser(_systemUserName, "TestUser_CDV6_19412", currentTime);

            CreateTransportationTypesForSystemUser();

            #region Navigate to the system user Schedule Transport tab

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard()
                .ValidateScheduleTransportAreaDislayed();

            #endregion

            #region Step 9

            systemUserAvailabilitySubPage
                .ClickSpecificSlotToCreateOrEditScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car");

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(false).ClickRemoveTimeSlotButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ClickBackButton();

            systemUsersPage
                 .WaitForSystemUsersPageToLoad()
                 .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard()
                .ValidateScheduleTransportAreaDislayed();

            systemUserAvailabilitySubPage
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car", false)
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "bicycle", true)
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad();

            systemUserViewDiaryManageAdHocPage
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car", false)
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "bicycle", true);

            #endregion

            #region Step 10


            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard()
                .ValidateScheduleTransportAreaDislayed()
                .ClickSpecificSlotToCreateOrEditScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "bicycle"); ;

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(false).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ClickBackButton();

            systemUsersPage
                 .WaitForSystemUsersPageToLoad()
                 .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard()
                .ValidateScheduleTransportAreaDislayed();

            systemUserAvailabilitySubPage
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car", false)
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "bicycle", false)
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "walking", true)
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad();

            systemUserViewDiaryManageAdHocPage
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car", false)
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "bicycle", false)
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "walking", true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3530")]
        [Description("Step 11 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("BusinessModule2", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Availability")]
        [TestProperty("Screen2", "View Diary Manage Ad Hoc")]
        [TestProperty("Screen3", "Schedule Transport")]
        public void SystemUser_Availability_ScheduleTransport_UITestCases004()
        {
            var currentTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            _systemUserName = "TestUser_CDV6_19412_" + currentTime;

            CreateNewSystemUser(_systemUserName, "TestUser_CDV6_19412", currentTime);

            CreateTransportationTypesForSystemUser(false);

            #region Navigate to the system user Schedule Transport tab

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard()
                .ValidateScheduleTransportAreaDislayed();

            #endregion

            #region Step 11

            systemUserAvailabilitySubPage
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car", true)
                .DragAvailabilityToLeft(DateTime.Now.DayOfWeek.ToString(), 4)
                .ClickOnSaveButton()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton()
                .ClickScheduleTransportCard()
                .ValidateScheduleTransportAreaDislayed()
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car", true);


            var userTransportationScheduleRecords = dbHelper.userTransportationSchedule.GetBySystemUsertID(_systemUserID);
            Assert.AreEqual(1, userTransportationScheduleRecords.Count);
            var fields = dbHelper.userTransportationSchedule.GetUserTransportationScheduleByID(userTransportationScheduleRecords[0], "startdate", "enddate", "starttime", "endtime");

            var expectedStartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            Assert.AreEqual(expectedStartDate, fields["startdate"]);
            Assert.IsFalse(fields.ContainsKey("enddate"));
            Assert.AreEqual(new TimeSpan(9, 0, 0), fields["starttime"]);
            Assert.AreEqual(new TimeSpan(23, 30, 0), fields["endtime"]);
            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-19415

        [TestProperty("JiraIssueID", "ACC-3531")]
        [Description("Step 12 to 13 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("BusinessModule2", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Availability")]
        [TestProperty("Screen2", "View Diary Manage Ad Hoc")]
        [TestProperty("Screen3", "Schedule Transport")]
        public void SystemUser_Availability_ScheduleTransport_UITestCases005()
        {
            var currentTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            _systemUserName = "TestUser_CDV6_19412_" + currentTime;

            CreateNewSystemUser(_systemUserName, "TestUser_CDV6_19412", currentTime);

            CreateTransportationTypesForSystemUser();

            #region Navigate to the system user Schedule Transport tab

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard()
                .ValidateScheduleTransportAreaDislayed();

            #endregion

            #region Step 12

            systemUserAvailabilitySubPage
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "bicycle", true)
                .DragAvailabilityToLeft(DateTime.Now.DayOfWeek.ToString(), 4)
                .ClickOnSaveButton()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton();

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard()
                .ValidateScheduleTransportAreaDislayed()
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "bicycle", true);


            var userTransportationScheduleRecords = dbHelper.userTransportationSchedule.GetBySystemUsertID(_systemUserID);
            Assert.AreEqual(2, userTransportationScheduleRecords.Count);

            #endregion

            #region Step 13

            systemUserAvailabilitySubPage
                .ClickSpecificSlotToCreateOrEditScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car");

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(false).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton();

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard()
                .ValidateScheduleTransportAreaDislayed()
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "walking", true)
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "bicycle", true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3532")]
        [Description("Step 14 to 16 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("BusinessModule2", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Availability")]
        [TestProperty("Screen2", "View Diary Manage Ad Hoc")]
        [TestProperty("Screen3", "Schedule Transport")]
        public void SystemUser_Availability_ScheduleTransport_UITestCases006()
        {
            var currentTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            _systemUserName = "TestUser_CDV6_19412_" + currentTime;

            CreateNewSystemUser(_systemUserName, "TestUser_CDV6_19412", currentTime);

            CreateTransportationTypesForSystemUser();

            #region Navigate to the system user Schedule Transport tab

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard()
                .ValidateScheduleTransportAreaDislayed();

            #endregion

            #region Step 14

            systemUserAvailabilitySubPage
                .ClickAddWeekButton()
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car", false) //on week 2 there should be no slots at the moment
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "bicycle", false) //on week 2 there should be no slots at the moment
                .ClickCreateScheduleTransportButton(DateTime.Now.AddDays(7).DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton();

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateScheduleTransportAreaDislayed()
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "bicycle", true)
                .ClickWeekButton(2)
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy"), "walking", true);


            #endregion

            #region Step 15

            systemUserAvailabilitySubPage
                .ClickWeekExpandButton(1)
                .ClickRemoveWeekButton(1)
                .ClickOnSaveButton()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton();

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateScheduleTransportAreaDislayed()
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car", false) //week 1 was removed
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "bicycle", false) //week 1 was removed
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "walking", true); //week 2 is now week 1

            #endregion

            #region Step 16

            systemUserAvailabilitySubPage
                .ClickWeekExpandButton(1)
                .ClickDuplicateWeekButton(1)
                .ClickOnSaveButton()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton();

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateScheduleTransportAreaDislayed()
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "walking", true) //week 2 is now week 1
                .ClickWeekButton(2)
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy"), "walking", true);

            systemUserAvailabilitySubPage
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car", false)
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "bicycle", false)
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "walking", true)
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy"), "walking", true);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3533")]
        [Description("Step 17 and 18 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("BusinessModule2", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Availability")]
        [TestProperty("Screen2", "View Diary Manage Ad Hoc")]
        [TestProperty("Screen3", "Schedule Transport")]
        public void SystemUser_Availability_ScheduleTransport_UITestCases007()
        {
            var currentTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            _systemUserName = "TestUser_CDV6_19412_" + currentTime;

            CreateNewSystemUser(_systemUserName, "TestUser_CDV6_19412", currentTime);

            CreateTransportationTypesForSystemUser();

            #region Navigate to the system user Schedule Transport tab

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickScheduleTransportCard()
                .ValidateScheduleTransportAreaDislayed();

            #endregion

            #region Step 17

            systemUserAvailabilitySubPage
                .ClickAddWeekButton()
                .ClickCreateScheduleTransportButton(DateTime.Now.AddDays(7).DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton();

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateScheduleTransportAreaDislayed()
                .ClickWeekButton(2)
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy"), "walking", true)
                .ClickWeekExpandButton(2)
                .ClickMoveWeekLeftButton(2)//move the week to the left
                .ClickOnSaveButton()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton();

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateScheduleTransportAreaDislayed()
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "walking", true)
                .ClickWeekButton(2)
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy"), "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy"), "bicycle", true)
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage //validate the View Diary / Manage Ad Hoc tab
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "walking", true)
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy"), "car", true)
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy"), "bicycle", true);

            #endregion

            #region Step 18

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateScheduleTransportAreaDislayed()
                .ClickWeekButton(1)
                .ClickWeekExpandButton(1)
                .ClickMoveWeekRightButton(1)//move the week to the right side
                .ClickOnSaveButton()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton();

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateScheduleTransportAreaDislayed()
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "bicycle", true)
                .ClickWeekButton(2)
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy"), "walking", true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3534")]
        [Description("Step 19 and 20 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("BusinessModule2", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Availability")]
        [TestProperty("Screen2", "View Diary Manage Ad Hoc")]
        [TestProperty("Screen3", "Schedule Transport")]
        public void SystemUser_Availability_ScheduleTransport_UITestCases008()
        {
            var currentTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            _systemUserName = "TestUser_CDV6_19412_" + currentTime;

            CreateNewSystemUser(_systemUserName, "TestUser_CDV6_19412", currentTime);

            CreateTransportationTypesForSystemUser();

            var mondayFromPreviousWeek = GetThisWeekFirstMonday().AddDays(-7);

            #region Navigate to the system user Schedule Transport tab

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickScheduleTransportCard()
                .ValidateScheduleTransportAreaDislayed();

            #endregion

            #region Step 19

            systemUserAvailabilitySubPage
                .ClickAddWeekButton()
                .ClickCreateScheduleTransportButton(DateTime.Now.AddDays(7).DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton();

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateScheduleTransportAreaDislayed()
                .InsertWeek1CycleDate(mondayFromPreviousWeek.ToString("dd'/'MM'/'yyyy")) //set the Week 1 Cycle Date to the monday of the previous week
                .ClickOnSaveButton()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton();

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateScheduleTransportAreaDislayed()
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "walking", true) //week 2 is displayed and Walking is displayed is switched with the dates from week 1
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy"), "car", false) //week 1 is hidden now. Plus week 1 calendar dates are moved 7 days forward
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy"), "bicycle", false) //week 1 is hidden now. Plus week 1 calendar dates are moved 7 days forward
                .ClickWeekButton(1)
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "walking", false) //we selected week 1 , so week 2 info is hidden now
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy"), "car", true) //we selected week 1, so the new calendar dates should be displayed now
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy"), "bicycle", true) //we selected week 1, so the new calendar dates should be displayed now
                ;


            #endregion

            #region Step 20

            systemUserAvailabilitySubPage
               .InsertWeek1CycleDate(GetThisWeekFirstMonday().AddDays(7).ToString("dd'/'MM'/'yyyy")) //set a future date
               .ValidateSaveButtonsAreDisabled();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3535")]
        [Description("Step 21 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("BusinessModule2", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Availability")]
        [TestProperty("Screen2", "View Diary Manage Ad Hoc")]
        [TestProperty("Screen3", "Schedule Transport")]
        public void SystemUser_Availability_ScheduleTransport_UITestCases009()
        {
            var currentTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            _systemUserName = "TestUser_CDV6_19412_" + currentTime;

            CreateNewSystemUser(_systemUserName, "TestUser_CDV6_19412", currentTime);

            var transportType_Bicycle_Id = dbHelper.transportType.GetTransportTypeByName("Bicycle")[0];
            var transportType_Boat_Id = dbHelper.transportType.GetTransportTypeByName("Boat")[0];
            var transportType_Bus_Id = dbHelper.transportType.GetTransportTypeByName("Bus")[0];
            var transportType_Car_Id = dbHelper.transportType.GetTransportTypeByName("Car")[0];

            #region Navigate to the system user Schedule Transport tab

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            #endregion

            #region Step 21

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ClickAlwaysAvailableTransportLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(transportType_Bicycle_Id.ToString())
                .ValidateResultElementPresent(transportType_Boat_Id.ToString())
                .ValidateResultElementPresent(transportType_Bus_Id.ToString())
                .ValidateResultElementPresent(transportType_Car_Id.ToString())
                .TypeSearchQuery("Bicycle")
                .TapSearchButton()
                .SelectResultElement(transportType_Bicycle_Id.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .SelectGender_Options("Male")
                .ClickSaveAndCloseButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoadWithInfoSectionVisible()
                .ValidateInfoAreaMessage("You have already selected that you always have access to the transportation type: BICYCLE");


            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3536")]
        [Description("Step 22 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("BusinessModule2", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Availability")]
        [TestProperty("Screen2", "View Diary Manage Ad Hoc")]
        [TestProperty("Screen3", "Schedule Transport")]
        public void SystemUser_Availability_ScheduleTransport_UITestCases010()
        {
            var currentTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            _systemUserName = "TestUser_CDV6_19412_" + currentTime;

            CreateNewSystemUser(_systemUserName, "TestUser_CDV6_19412", currentTime);

            CreateEmploymentContractsForSystemUser();

            #region Navigate to the system user Schedule Transport tab

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickScheduleTransportCard()
                .ValidateScheduleTransportAreaDislayed();

            #endregion

            #region Step 9

            systemUserAvailabilitySubPage
                .ClickCreateScheduleTransportButton(DateTime.Now.DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton();

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard()
                .ValidateScheduleTransportAreaDislayed()
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car", true);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4086

        [TestProperty("JiraIssueID", "ACC-559")]
        [Description("Step 1 to 3 and 8 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("Screen1", "Schedule Availability")]
        public void SystemUser_Availability_UITestCases001()
        {
            #region Test System User
            dbHelper = new DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);
            _systemUserName = "ACC559U" + partialStringSuffix; ;

            _systemUserID = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ACC559", "U" + partialStringSuffix, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSATransportWeek1CycleStartDate(_systemUserID, GetThisWeekFirstMonday());

            #endregion

            #region Care provider staff role type
            dbHelper = new DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string careProviderStaffRoleName = "Staff559";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "Staff559...");

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Navigate to the system user Schedule availability tab

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateSystemUserWarningMessage("Current system user does not have any employment contracts.");

            systemUserAvailabilitySubPage
                .ValidateScheduleAvailabilityAreaNotDisplayed();

            #region System User Employment Contract

            var _employmentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserID, DateTime.Now.Date, _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid, 48m);
            string _employmentContractName = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_employmentContractId, "name")["name"];

            #endregion

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ValidateRecordVisibility(_employmentContractId.ToString(), true);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            string firstDayofTheCurrentWeek = commonMethodsHelper.GetThisWeekFirstMonday().ToString("dd'/'MM'/'yyyy");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateScheduleAvailabilityAreaDislayed()
                .ClickScheduleAvailabilityCard()
                .ValidateWeek1CycleDateReadonly(true)
                .ValidateWeek1CycleDate(firstDayofTheCurrentWeek)
                .ValidateCreateScheduleAvailabilityButton_Monday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Tuesday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Wednesday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Thursday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Friday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Saturday(_employmentContractName.ToUpper());

            var DayOfTheWeek_Today_Date = DateTime.Now.ToString("dd'/'MM'/'yyyy");

            systemUserAvailabilitySubPage
                .ClickCreateScheduleAvailabilityButtonByDate(DayOfTheWeek_Today_Date, 1, _employmentContractName.ToUpper());

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Salaried/Contracted");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateSaveButtonsAreDisabled()
                .ValidateSelectedScheduleAvailabilitySlotIsVisible(DayOfTheWeek_Today_Date, _employmentContractName, true);

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateCreatedAdHocSlot(DayOfTheWeek_Today_Date, _employmentContractName);

            var systemSettingId = dbHelper.systemSetting.GetSystemSettingIdByName("UserWorkScheduleYearsSlots")[0];
            int systemSettingValue = Int32.Parse(dbHelper.systemSetting.GetByID(systemSettingId, "settingvalue")["settingvalue"].ToString());


            string DayOfTheWeek_NextYear_Date = datetimeext.GetDayOfWeek(DateTime.Now.AddYears(1).Date, DateTime.Now.DayOfWeek).ToString("dd'/'MM'/'yyyy");
            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateCreatedAdHocSlot(DayOfTheWeek_NextYear_Date, _employmentContractName);

            string DayOfTheWeek_PlusNYears_Date = datetimeext.GetDayOfWeek(DateTime.Now.AddYears(systemSettingValue).Date, DateTime.Now.DayOfWeek).ToString("dd'/'MM'/'yyyy");
            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateCreatedAdHocSlot(DayOfTheWeek_PlusNYears_Date, _employmentContractName);

            var DayOfTheWeek_PlusNx2Years_Date = datetimeext.GetDayOfWeek(DateTime.Now.AddYears(systemSettingValue * 2).Date, DateTime.Now.DayOfWeek).ToString("dd'/'MM'/'yyyy");
            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateCreatedAdHocSlot(DayOfTheWeek_PlusNx2Years_Date, _employmentContractName);

            string DayOfTheWeek_2NPlus1Years_Date = datetimeext.GetDayOfWeek(DateTime.Now.AddYears(systemSettingValue * 2 + 1).Date, DateTime.Now.DayOfWeek).ToString("dd'/'MM'/'yyyy");
            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateDefaultAdHocSlotIsDisplayed(DayOfTheWeek_2NPlus1Years_Date, (datetimeext.GetDayOfWeek(DateTime.Now.AddYears(systemSettingValue * 2 + 1).Date, DateTime.Now.DayOfWeek).DayOfWeek.ToString()), _employmentContractName.ToUpper());

            string DayOfTheWeek_2NYearsPlus1Week_Date = datetimeext.GetDayOfWeek(DateTime.Now.AddYears(systemSettingValue * 2).Date, DateTime.Now.DayOfWeek).AddDays(7).ToString("dd'/'MM'/'yyyy");
            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateDefaultAdHocSlotIsDisplayed(DayOfTheWeek_2NYearsPlus1Week_Date, (datetimeext.GetDayOfWeek(DateTime.Now.AddYears(systemSettingValue * 2).Date, DateTime.Now.DayOfWeek).AddDays(7).DayOfWeek.ToString()), _employmentContractName.ToUpper());

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4178

        [TestProperty("JiraIssueID", "ACC-4261")]
        [Description("Step 7 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("Screen1", "Schedule Availability")]
        public void SystemUser_ScheduleAvailability_UITestCases001()
        {
            DateTime thisWeekMonday;
            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                thisWeekMonday = GetThisWeekFirstMonday();
            }
            else
            {
                thisWeekMonday = GetThisWeekFirstMonday().AddDays(14); //if today is not monday we need the monday of 2 weeks from now (as we add an extra week in the test)
            }

            var mondayFromPreviousWeek = GetThisWeekFirstMonday().AddDays(-14);

            #region Test System User

            var _systemUserFullName = "ACC573 U" + partialStringSuffix;
            _systemUserName = "ACC573U" + partialStringSuffix;
            _systemUserID = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ACC573", "U" + partialStringSuffix, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserID, GetThisWeekFirstMonday());

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var _employmentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserID, DateTime.Now.Date, _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid, 48m);
            string _employmentContractName = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_employmentContractId, "name")["name"];

            #endregion

            #region Step 7

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickAddWeekButton()
                .ClickWeekButton(1)
                .InsertWeek1CycleDate(mondayFromPreviousWeek.ToString("dd'/'MM'/'yyyy"))
                .ValidateCreateScheduleAvailabilitySlotByDateIsVisible(thisWeekMonday.ToString("dd'/'MM'/'yyyy"), 1, _systemUserFullName + " - HELPER");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4262")]
        [Description("Step 16 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("Screen1", "Schedule Availability")]
        public void SystemUser_ScheduleAvailability_UITestCases002()
        {
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Test System User

            var _systemUserFullName = "ACC573 U" + partialStringSuffix;
            _systemUserName = "ACC573U" + partialStringSuffix;
            _systemUserID = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ACC573", "U" + partialStringSuffix, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserID, GetThisWeekFirstMonday());

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var _employmentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserID, DateTime.Now.Date, _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid, 48m);

            #endregion

            #region Availability Type

            commonMethodsDB.CreateAvailabilityType("Standard", 3, false, _careProviders_TeamId, 1, 1, true);

            #endregion

            #region Step 16

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleAvailabilityButtonByDate(currentDate.ToString("dd'/'MM'/'yyyy"), 1, _systemUserFullName + " - HELPER");

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .DragAvailabilityToLeft(currentDate.DayOfWeek.ToString(), 2);

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            var userSchedules = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserID);
            Assert.AreEqual(1, userSchedules.Count);

            var fields = dbHelper.userWorkSchedule.GetUserWorkScheduleByID(userSchedules[0], "starttime");
            Assert.AreEqual(new TimeSpan(8, 45, 0), fields["starttime"]);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4263")]
        [Description("Step 19 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("Screen1", "Schedule Availability")]
        public void SystemUser_ScheduleAvailability_UITestCases003()
        {
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Test System User

            var _systemUserFullName = "ACC573 U" + partialStringSuffix;
            _systemUserName = "ACC573U" + partialStringSuffix;
            _systemUserID = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ACC573", "U" + partialStringSuffix, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserID, GetThisWeekFirstMonday());

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var _employmentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserID, DateTime.Now.Date, _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid, 48m);

            #endregion

            #region Availability Type

            commonMethodsDB.CreateAvailabilityType("Standard", 3, false, _careProviders_TeamId, 1, 1, true);

            #endregion

            #region Step 19

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleAvailabilityButtonByDate(currentDate.ToString("dd'/'MM'/'yyyy"), 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleAvailabilityButtonByDate(currentDate.ToString("dd'/'MM'/'yyyy"), 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .DragAvailabilityToLeft(currentDate.DayOfWeek.ToString(), 4)
                .DragAvailabilityToLeft(currentDate.DayOfWeek.ToString(), 4);

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            var userSchedules = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserID);
            Assert.AreEqual(1, userSchedules.Count);

            var fields = dbHelper.userWorkSchedule.GetUserWorkScheduleByID(userSchedules[0], "starttime");
            Assert.AreEqual(new TimeSpan(0, 30, 0), fields["starttime"]);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4264")]
        [Description("Step 20 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("Screen1", "Schedule Availability")]
        public void SystemUser_ScheduleAvailability_UITestCases004()
        {
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Test System User

            var _systemUserFullName = "ACC573 U" + partialStringSuffix;
            _systemUserName = "ACC573U" + partialStringSuffix;
            _systemUserID = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ACC573", "U" + partialStringSuffix, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserID, GetThisWeekFirstMonday());

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var _employmentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserID, DateTime.Now.Date, _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid, 48m);

            #endregion

            #region Availability Type

            commonMethodsDB.CreateAvailabilityType("Standard", 3, false, _careProviders_TeamId, 1, 1, true);
            commonMethodsDB.CreateAvailabilityType("Regular", 5, false, _careProviders_TeamId, 1, 1, true);

            #endregion

            #region Step 20

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleAvailabilityButtonByDate(currentDate.ToString("dd'/'MM'/'yyyy"), 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleAvailabilityButtonByDate(currentDate.ToString("dd'/'MM'/'yyyy"), 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Regular");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .DragAvailabilityToLeft(currentDate.DayOfWeek.ToString(), 4)
                .DragAvailabilityToLeft(currentDate.DayOfWeek.ToString(), 4);

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            var userSchedules = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserID);
            Assert.AreEqual(2, userSchedules.Count);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4265")]
        [Description("Step 21 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("Screen1", "Schedule Availability")]
        public void SystemUser_ScheduleAvailability_UITestCases005()
        {
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Test System User

            var _systemUserFullName = "ACC573 U" + partialStringSuffix;
            _systemUserName = "ACC573U" + partialStringSuffix;
            _systemUserID = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ACC573", "U" + partialStringSuffix, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserID, GetThisWeekFirstMonday());

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var _employmentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserID, DateTime.Now.Date, _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid, 48m);

            #endregion

            #region Availability Type

            commonMethodsDB.CreateAvailabilityType("Standard", 3, false, _careProviders_TeamId, 1, 1, true);
            commonMethodsDB.CreateAvailabilityType("Regular", 5, false, _careProviders_TeamId, 1, 1, true);

            #endregion

            #region Step 21

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleAvailabilityButtonByDate(currentDate.ToString("dd'/'MM'/'yyyy"), 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .MouseoverCreatedScheduleAvailabilitySlot(currentDate.ToString("dd'/'MM'/'yyyy"), _systemUserFullName + " - Helper")
                .ValidateToolTip(_systemUserFullName + " - Helper (Standard): 09:00 – 17:00");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4266")]
        [Description("Step 23 to 27 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("Screen1", "Schedule Availability")]
        public void SystemUser_ScheduleAvailability_UITestCases006()
        {
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var nextWeekMonday = GetThisWeekFirstMonday().AddDays(7);
            var thisWeekTuesDay = GetThisWeekFirstMonday().AddDays(1);

            #region Test System User

            var _systemUserFullName = "ACC573 U" + partialStringSuffix;
            _systemUserName = "ACC573U" + partialStringSuffix;
            _systemUserID = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ACC573", "U" + partialStringSuffix, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserID, GetThisWeekFirstMonday());

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var _employmentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserID, DateTime.Now.Date, _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid, 48m);

            #endregion

            #region Availability Type

            commonMethodsDB.CreateAvailabilityType("Standard", 3, false, _careProviders_TeamId, 1, 1, true);
            commonMethodsDB.CreateAvailabilityType("Regular", 5, false, _careProviders_TeamId, 1, 1, true);

            #endregion

            #region Step 23

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleAvailabilityButtonByDate(currentDate.ToString("dd'/'MM'/'yyyy"), 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickWeekExpandButton(1)
                .ClickDuplicateWeekButton(1)
                .ValidateCreatedScheduleAvailabilitySlot(currentDate.AddDays(7).ToString("dd'/'MM'/'yyyy"), _systemUserFullName + " - Helper");

            #endregion

            #region Step 24

            systemUserAvailabilitySubPage
                .ValidateWeekButtonVisibility(1, true)
                .ValidateWeekButtonVisibility(2, true)
                .ValidateWeekButtonVisibility(3, false)
                .ClickWeekExpandButton(2)
                .ClickRemoveWeekButton(2)
                .ValidateWeekButtonVisibility(1, true)
                .ValidateWeekButtonVisibility(2, false)
                .ValidateWeekButtonVisibility(3, false);

            #endregion

            #region Step 25

            systemUserAvailabilitySubPage
                .ClickWeekExpandButton(1)
                .ClickAddWeekButton()
                .ValidateCreateScheduleAvailabilitySlotByDateIsVisible(nextWeekMonday.ToString("dd'/'MM'/'yyyy"), 1, _systemUserFullName + " - HELPER");

            #endregion

            #region Step 27

            systemUserAvailabilitySubPage
                .ClickWeek1CycleDateDatePicker()
                .ValidateWeek1CycleDateCalendarDateDisabled(thisWeekTuesDay.ToString("MMMM d, yyyy"));

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4267")]
        [Description("Step 28 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("Screen1", "Schedule Availability")]
        public void SystemUser_ScheduleAvailability_UITestCases007()
        {
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Test System User

            var _systemUserFullName = "ACC573 U" + partialStringSuffix;
            _systemUserName = "ACC573U" + partialStringSuffix;
            _systemUserID = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ACC573", "U" + partialStringSuffix, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserID, GetThisWeekFirstMonday());

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var _employmentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserID, DateTime.Now.Date, _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid, 48m);

            #endregion

            #region Availability Type

            commonMethodsDB.CreateAvailabilityType("Standard", 3, false, _careProviders_TeamId, 1, 1, true);
            commonMethodsDB.CreateAvailabilityType("Regular", 5, false, _careProviders_TeamId, 1, 1, true);

            #endregion

            #region Step 28

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleAvailabilityButtonByDate(currentDate.ToString("dd'/'MM'/'yyyy"), 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleAvailabilityButtonByDate(currentDate.ToString("dd'/'MM'/'yyyy"), 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleAvailabilityButtonByDate(currentDate.ToString("dd'/'MM'/'yyyy"), 5);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .DragScheduleAvailabilitySlotFromLeftToRight(currentDate.DayOfWeek.ToString(), 4, 2)
                .ScrollAndClickOnScheduleAvailabilityDayLabel(currentDate.DayOfWeek.ToString())
                .DragScheduleAvailabilitySlotFromLeftToRight(currentDate.DayOfWeek.ToString(), 4, 2)
                .ScrollAndClickOnScheduleAvailabilityDayLabel(currentDate.DayOfWeek.ToString())
                .DragAvailabilityToLeft(currentDate.DayOfWeek.ToString(), 2)
                .DragAvailabilityToLeft(currentDate.DayOfWeek.ToString(), 2)
                .ScrollAndClickOnScheduleAvailabilityDayLabel(currentDate.DayOfWeek.ToString())
                .DragAvailabilityRecordRightArea(currentDate.ToString("dd'/'MM'/'yyyy"), _systemUserFullName + " - Helper", 2, 40)
                .ClickOnSaveAndCloseButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            var userSchedules = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserID);
            Assert.AreEqual(1, userSchedules.Count);

            var fields = dbHelper.userWorkSchedule.GetUserWorkScheduleByID(userSchedules[0], "starttime", "endtime");
            Assert.AreEqual(new TimeSpan(0, 0, 0), fields["starttime"]);
            Assert.AreEqual(new TimeSpan(0, 0, 0), fields["endtime"]);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4268")]
        [Description("Step 29 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("Screen1", "Schedule Availability")]
        public void SystemUser_ScheduleAvailability_UITestCases008()
        {
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Test System User

            var _systemUserFullName = "ACC573 U" + partialStringSuffix;
            _systemUserName = "ACC573U" + partialStringSuffix;
            _systemUserID = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ACC573", "U" + partialStringSuffix, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserID, GetThisWeekFirstMonday());

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var _employmentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserID, DateTime.Now.Date, _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid, 48m);

            #endregion

            #region Availability Type

            commonMethodsDB.CreateAvailabilityType("Standard", 3, false, _careProviders_TeamId, 1, 1, true);
            commonMethodsDB.CreateAvailabilityType("Regular", 5, false, _careProviders_TeamId, 1, 1, true);

            #endregion

            #region Step 29

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleAvailabilityButtonByDate(currentDate.ToString("dd'/'MM'/'yyyy"), 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleAvailabilityButtonByDate(currentDate.ToString("dd'/'MM'/'yyyy"), 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Regular");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleAvailabilityButtonByDate(currentDate.ToString("dd'/'MM'/'yyyy"), 5);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Regular");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()

                .DragAvailabilityToLeft(currentDate.DayOfWeek.ToString(), 2)
                .DragAvailabilityToLeft(currentDate.DayOfWeek.ToString(), 2);

            systemUserAvailabilitySubPage
                .DragAvailabilityToLeft(currentDate.DayOfWeek.ToString(), 4);

            systemUserAvailabilitySubPage
                .DragAvailabilityToLeft(currentDate.DayOfWeek.ToString(), 4);

            systemUserAvailabilitySubPage
                .DragAvailabilityToLeft(currentDate.DayOfWeek.ToString(), 6);

            systemUserAvailabilitySubPage
                .DragAvailabilityToLeft(currentDate.DayOfWeek.ToString(), 6);

            systemUserAvailabilitySubPage
                .DragAvailabilityToRight(currentDate.DayOfWeek.ToString(), 6);

            systemUserAvailabilitySubPage
                .DragAvailabilityToRight(currentDate.DayOfWeek.ToString(), 6)

                .ClickOnSaveAndCloseButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            var userSchedules = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserID);
            Assert.AreEqual(3, userSchedules.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4269")]
        [Description("Step 30 to 31 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("Screen1", "Schedule Availability")]
        public void SystemUser_ScheduleAvailability_UITestCases009()
        {
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var futureDate1 = currentDate.AddDays(1);
            var futureDate2 = currentDate.AddDays(2);

            #region Test System User

            var _systemUserFullName = "ACC573 U" + partialStringSuffix;
            _systemUserName = "ACC573U" + partialStringSuffix;
            _systemUserID = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ACC573", "U" + partialStringSuffix, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserID, GetThisWeekFirstMonday());

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var _employmentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserID, DateTime.Now.Date, _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid, 48m);

            #endregion

            #region Availability Type

            commonMethodsDB.CreateAvailabilityType("Standard", 3, false, _careProviders_TeamId, 1, 1, true);
            commonMethodsDB.CreateAvailabilityType("Regular", 5, false, _careProviders_TeamId, 1, 1, true);

            #endregion

            #region Step 30

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleAvailabilityButtonByDate(currentDate.ToString("dd'/'MM'/'yyyy"), 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleAvailabilityButtonByDate(futureDate2.ToString("dd'/'MM'/'yyyy"), 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            var userSchedules = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserID);
            Assert.AreEqual(2, userSchedules.Count);

            #endregion

            #region Step 31

            systemUserAvailabilitySubPage
                .ClickCreateScheduleAvailabilityButtonByDate(futureDate1.ToString("dd'/'MM'/'yyyy"), 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Regular");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .DragAvailabilityToLeft(futureDate2.ToString("dd'/'MM'/'yyyy"), 2)
                .DragAvailabilityToRight(futureDate2.ToString("dd'/'MM'/'yyyy"), 2)
                .ClickOnSaveAndCloseButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            userSchedules = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserID);
            Assert.AreEqual(3, userSchedules.Count);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4270")]
        [Description("Step 32 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("Screen1", "Schedule Availability")]
        public void SystemUser_ScheduleAvailability_UITestCases010()
        {
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var futureDate = currentDate.AddDays(14);

            #region Test System User

            var _systemUserFullName = "ACC573 U" + partialStringSuffix;
            _systemUserName = "ACC573U" + partialStringSuffix;
            _systemUserID = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ACC573", "U" + partialStringSuffix, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserID, GetThisWeekFirstMonday());

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var _employmentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserID, DateTime.Now.Date, _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid, 48m);

            #endregion

            #region Availability Type

            commonMethodsDB.CreateAvailabilityType("Standard", 3, false, _careProviders_TeamId, 1, 1, true);
            commonMethodsDB.CreateAvailabilityType("Regular", 5, false, _careProviders_TeamId, 1, 1, true);

            #endregion

            #region Step 32

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickAddWeekButton()
                .ClickAddWeekButton()
                .ClickCreateScheduleAvailabilityButtonByDate(futureDate.ToString("dd'/'MM'/'yyyy"), 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            var userSchedules = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserID);
            Assert.AreEqual(1, userSchedules.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4271")]
        [Description("Step 33 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("Screen1", "Schedule Availability")]
        public void SystemUser_ScheduleAvailability_UITestCases011()
        {
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var futureDate1 = currentDate.AddDays(7);
            var futureDate2 = currentDate.AddDays(14);

            #region Test System User

            var _systemUserFullName = "ACC573 U" + partialStringSuffix;
            _systemUserName = "ACC573U" + partialStringSuffix;
            _systemUserID = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ACC573", "U" + partialStringSuffix, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserID, GetThisWeekFirstMonday());

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var _employmentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserID, DateTime.Now.Date, _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid, 48m);

            #endregion

            #region Availability Type

            commonMethodsDB.CreateAvailabilityType("Standard", 3, false, _careProviders_TeamId, 1, 1, true);
            commonMethodsDB.CreateAvailabilityType("Regular", 5, false, _careProviders_TeamId, 1, 1, true);

            #endregion

            #region Step 33

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickAddWeekButton()
                .ClickAddWeekButton()
                .ClickWeekButton(1)
                .ClickCreateScheduleAvailabilityButtonByDate(currentDate.ToString("dd'/'MM'/'yyyy"), 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickWeekButton(2)
                .ClickCreateScheduleAvailabilityButtonByDate(futureDate1.ToString("dd'/'MM'/'yyyy"), 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Regular");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickWeekButton(3)
                .ClickCreateScheduleAvailabilityButtonByDate(futureDate2.ToString("dd'/'MM'/'yyyy"), 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            var userSchedules = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserID);
            Assert.AreEqual(3, userSchedules.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4272")]
        [Description("Step 34 to 39 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("Screen1", "Schedule Availability")]
        public void SystemUser_ScheduleAvailability_UITestCases012()
        {
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var futureDate1 = currentDate.AddDays(1);
            var futureDate2 = currentDate.AddDays(7);

            #region Test System User

            var _systemUserFullName = "ACC573 U" + partialStringSuffix;
            _systemUserName = "ACC573U" + partialStringSuffix;
            _systemUserID = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ACC573", "U" + partialStringSuffix, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserID, GetThisWeekFirstMonday());

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleType1id = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType2id = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Cook", "", "", new DateTime(2000, 1, 1), "");

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var _employmentContract1Id = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserID, DateTime.Now.Date, _careProviderStaffRoleType1id, _careProviders_TeamId, _employmentContractTypeid, 48m);
            var _employmentContract2Id = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserID, DateTime.Now.Date, _careProviderStaffRoleType2id, _careProviders_TeamId, _employmentContractTypeid, 48m);

            #endregion

            #region Availability Type

            commonMethodsDB.CreateAvailabilityType("Standard", 3, false, _careProviders_TeamId, 1, 1, true);
            commonMethodsDB.CreateAvailabilityType("Regular", 5, false, _careProviders_TeamId, 1, 1, true);

            #endregion

            #region Step 34

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleAvailabilityButtonByDate(currentDate.ToString("dd'/'MM'/'yyyy"), 1, _systemUserFullName + " - COOK");

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleAvailabilityButtonByDate(currentDate.ToString("dd'/'MM'/'yyyy"), 1, _systemUserFullName + " - HELPER");

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Regular");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            var userSchedules = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserID);
            Assert.AreEqual(2, userSchedules.Count);

            #endregion

            #region Step 35

            systemUserAvailabilitySubPage
                .DragAvailabilityRecordLeftArea(currentDate.ToString("dd'/'MM'/'yyyy"), _systemUserFullName + " - Cook", 2, -60)
                .DragAvailabilityRecordRightArea(currentDate.ToString("dd'/'MM'/'yyyy"), _systemUserFullName + " - Cook", 2, -60)
                .ClickOnSaveAndCloseButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            userSchedules = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserID);
            Assert.AreEqual(2, userSchedules.Count);

            #endregion

            #region Step 36

            systemUserAvailabilitySubPage
                .ClickCreateScheduleAvailabilityButtonByDate(futureDate1.ToString("dd'/'MM'/'yyyy"), 1, _systemUserFullName + " - COOK");

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleAvailabilityButtonByDate(futureDate1.ToString("dd'/'MM'/'yyyy"), 1, _systemUserFullName + " - HELPER");

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Regular");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            userSchedules = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserID);
            Assert.AreEqual(4, userSchedules.Count);

            #endregion

            #region Step 37

            //this step will be ignored. It seems to be repeted

            #endregion

            #region Step 38

            systemUserAvailabilitySubPage
                .ClickAddWeekButton()
                .ClickCreateScheduleAvailabilityButtonByDate(futureDate2.ToString("dd'/'MM'/'yyyy"), 1, _systemUserFullName + " - COOK");

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleAvailabilityButtonByDate(futureDate2.ToString("dd'/'MM'/'yyyy"), 1, _systemUserFullName + " - HELPER");

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Regular");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .DragAvailabilityRecordLeftArea(futureDate2.ToString("dd'/'MM'/'yyyy"), _systemUserFullName + " - Cook", 2, -60)
                .DragAvailabilityRecordRightArea(futureDate2.ToString("dd'/'MM'/'yyyy"), _systemUserFullName + " - Cook", 2, -60)
                .ClickOnSaveAndCloseButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            userSchedules = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserID);
            Assert.AreEqual(6, userSchedules.Count);

            #endregion

            #region Step 39

            //seems similar to the previous steps

            #endregion

            #region Step 40

            //Step 40 can also be ignored here. we used the save, save and close and refresh in other tests.

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4273")]
        [Description("Step 40 to 46 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("BusinessModule2", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Availability")]
        [TestProperty("Screen2", "Schedule Transport")]
        public void SystemUser_ScheduleAvailability_UITestCases013()
        {
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var futureDate1 = currentDate.AddDays(1);

            #region Test System User

            var _systemUserFullName = "ACC573 U" + partialStringSuffix;
            _systemUserName = "ACC573U" + partialStringSuffix;
            _systemUserID = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ACC573", "U" + partialStringSuffix, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserID, GetThisWeekFirstMonday());

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleType1id = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType2id = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Cook", "", "", new DateTime(2000, 1, 1), "");

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var _employmentContract1Id = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserID, DateTime.Now.Date, _careProviderStaffRoleType1id, _careProviders_TeamId, _employmentContractTypeid, 48m);
            var _employmentContract2Id = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserID, DateTime.Now.Date, _careProviderStaffRoleType2id, _careProviders_TeamId, _employmentContractTypeid, 48m);

            #endregion

            #region Availability Type

            commonMethodsDB.CreateAvailabilityType("Standard", 3, false, _careProviders_TeamId, 1, 1, true);
            commonMethodsDB.CreateAvailabilityType("Regular", 5, false, _careProviders_TeamId, 1, 1, true);

            #endregion

            #region Step 40

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleAvailabilityButtonByDate(currentDate.ToString("dd'/'MM'/'yyyy"), 1, _systemUserFullName + " - COOK");

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            var userSchedules = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserID);
            Assert.AreEqual(1, userSchedules.Count);

            systemUserAvailabilitySubPage
                .ClickCreateScheduleAvailabilityButtonByDate(currentDate.ToString("dd'/'MM'/'yyyy"), 1, _systemUserFullName + " - HELPER");

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Regular");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            userSchedules = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserID);
            Assert.AreEqual(2, userSchedules.Count);

            #endregion

            #region Step 41

            systemUserAvailabilitySubPage
                .ClickCreateScheduleAvailabilityButtonByDate(futureDate1.ToString("dd'/'MM'/'yyyy"), 1, _systemUserFullName + " - HELPER");

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Regular");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton();

            availabilitySaveChangesDialogPopup
                .WaitForAvailabilitySaveChangesDialogPopupPageToLoad()
                .ValidateDialogText("You have changes made to Schedule Availability, do you want to save these changes?")
                .ValidateReloadButton()
                .ValidateSaveAndReloadButton()
                .ValidateCloseButton();

            #endregion

            #region Step 42

            availabilitySaveChangesDialogPopup
                .ClickOnCloseButton();

            #endregion

            #region Step 43

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton();

            availabilitySaveChangesDialogPopup
                .WaitForAvailabilitySaveChangesDialogPopupPageToLoad()
                .ClickOnReloadButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            userSchedules = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserID);
            Assert.AreEqual(2, userSchedules.Count); //last changes were not saved since we reloaded the page

            #endregion

            #region Step 44

            systemUserAvailabilitySubPage
                .ClickCreateScheduleAvailabilityButtonByDate(futureDate1.ToString("dd'/'MM'/'yyyy"), 1, _systemUserFullName + " - HELPER");

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Regular");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton();

            availabilitySaveChangesDialogPopup
                .WaitForAvailabilitySaveChangesDialogPopupPageToLoad()
                .ClickOnSaveAndReloadButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            userSchedules = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserID);
            Assert.AreEqual(3, userSchedules.Count); //last changes were not saved since we reloaded the page

            #endregion

            #region Step 45

            systemUserAvailabilitySubPage
                .ClickCreateScheduleAvailabilityButtonByDate(futureDate1.ToString("dd'/'MM'/'yyyy"), 1, _systemUserFullName + " - COOK");

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickScheduleTransportCard();

            availabilitySaveChangesDialogPopup
                .WaitForAvailabilitySaveChangesDialogPopupPageToLoad()
                .ValidateDialogText("Your changes made to your availability schedule have not been saved.\r\nTo stay on the page so you can save your changes, click Close.")
                .ClickOnCloseButton();

            #endregion

            #region Step 46

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4179

        [TestProperty("JiraIssueID", "ACC-4312")]
        [Description("Step 33 to 34 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("Screen1", "Schedule Availability")]
        public void SystemUser_ScheduleAvailability_UITestCases014()
        {
            #region Current Week

            DateTime CurrentDate = commonMethodsHelper.GetDatePartWithoutCulture();
            DateTime CurrentDate_1 = CurrentDate.AddDays(1);
            DateTime CurrentDate_2 = CurrentDate.AddDays(2);
            DateTime CurrentDate_3 = CurrentDate.AddDays(3);
            DateTime CurrentDate_4 = CurrentDate.AddDays(4);
            DateTime CurrentDate_5 = CurrentDate.AddDays(5);
            DateTime CurrentDate_6 = CurrentDate.AddDays(6);

            #endregion

            #region Test System User

            var _systemUserFullName = "ACC563 U" + partialStringSuffix;
            _systemUserName = "ACC563U" + partialStringSuffix;
            _systemUserID = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ACC563", "U" + partialStringSuffix, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserID, GetThisWeekFirstMonday());

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleType1id = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserID, DateTime.Now.Date, _careProviderStaffRoleType1id, _careProviders_TeamId, _employmentContractTypeid, 48m);

            #endregion

            #region Availability Type

            commonMethodsDB.CreateAvailabilityType("Standard", 3, false, _careProviders_TeamId, 1, 1, true);
            commonMethodsDB.CreateAvailabilityType("Regular", 5, false, _careProviders_TeamId, 1, 1, true);
            commonMethodsDB.CreateAvailabilityType("OverTime", 4, false, _careProviders_TeamId, 1, 1, true);

            #endregion

            #region Step 33, 34

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleAvailabilityButtonByDate(CurrentDate.ToString("dd'/'MM'/'yyyy"), 1, _systemUserFullName + " - HELPER");

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ScrollAndClickOnScheduleAvailabilityDateLabel(CurrentDate_1.ToString("dd'/'MM'/'yyyy"))
                .ClickCreateScheduleAvailabilityButtonByDate(CurrentDate_1.ToString("dd'/'MM'/'yyyy"), 1, _systemUserFullName + " - HELPER");

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Regular");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ScrollAndClickOnScheduleAvailabilityDateLabel(CurrentDate_2.ToString("dd'/'MM'/'yyyy"))
                .ClickCreateScheduleAvailabilityButtonByDate(CurrentDate_2.ToString("dd'/'MM'/'yyyy"), 1, _systemUserFullName + " - HELPER");

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("OverTime");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ScrollAndClickOnScheduleAvailabilityDateLabel(CurrentDate_3.ToString("dd'/'MM'/'yyyy"))
                .ClickCreateScheduleAvailabilityButtonByDate(CurrentDate_3.ToString("dd'/'MM'/'yyyy"), 1, _systemUserFullName + " - HELPER");

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ScrollAndClickOnScheduleAvailabilityDateLabel(CurrentDate_4.ToString("dd'/'MM'/'yyyy"))
                .ClickCreateScheduleAvailabilityButtonByDate(CurrentDate_4.ToString("dd'/'MM'/'yyyy"), 1, _systemUserFullName + " - HELPER");

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Regular");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ScrollAndClickOnScheduleAvailabilityDateLabel(CurrentDate_5.ToString("dd'/'MM'/'yyyy"))
                .ClickCreateScheduleAvailabilityButtonByDate(CurrentDate_5.ToString("dd'/'MM'/'yyyy"), 1, _systemUserFullName + " - HELPER");

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("OverTime");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ScrollAndClickOnScheduleAvailabilityDateLabel(CurrentDate_6.ToString("dd'/'MM'/'yyyy"))
                .ClickCreateScheduleAvailabilityButtonByDate(CurrentDate_6.ToString("dd'/'MM'/'yyyy"), 1, _systemUserFullName + " - HELPER");

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Regular");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            var userSchedules = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserID);
            Assert.AreEqual(7, userSchedules.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4313")]
        [Description("Step 35 to 36 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("Screen1", "Schedule Availability")]
        public void SystemUser_ScheduleAvailability_UITestCases015()
        {
            #region Test System User

            _systemUserName = "ACC563U" + partialStringSuffix;
            _systemUserID = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ACC563", "U" + partialStringSuffix, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserID, GetThisWeekFirstMonday());

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleType1id = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserID, DateTime.Now.Date, _careProviderStaffRoleType1id, _careProviders_TeamId, _employmentContractTypeid, 48m);

            #endregion

            #region Availability Type

            commonMethodsDB.CreateAvailabilityType("Standard", 3, false, _careProviders_TeamId, 1, 1, true);
            commonMethodsDB.CreateAvailabilityType("Regular", 5, false, _careProviders_TeamId, 1, 1, true);
            commonMethodsDB.CreateAvailabilityType("OverTime", 4, false, _careProviders_TeamId, 1, 1, true);

            #endregion

            #region Step 35, 36

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleAvailabilityButton("Monday", 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .DragTransportAvailabilityToLeft("Monday", 2)
                .ScrollAndClickOnScheduleAvailabilityDayLabel("Tuesday")
                .ClickCreateScheduleAvailabilityButton("Tuesday", 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Regular");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .DragAvailabilityToRight("Tuesday", 2)
                .ScrollAndClickOnScheduleAvailabilityDayLabel("Wednesday")
                .ClickCreateScheduleAvailabilityButton("Wednesday", 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("OverTime");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .DragTransportAvailabilityToLeft("Wednesday", 2)
                .ScrollAndClickOnScheduleAvailabilityDayLabel("Thursday")
                .ClickCreateScheduleAvailabilityButton("Thursday", 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .DragAvailabilityToRight("Thursday", 2)
                .ScrollAndClickOnScheduleAvailabilityDayLabel("Friday")
                .ClickCreateScheduleAvailabilityButton("Friday", 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Regular");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .DragTransportAvailabilityToLeft("Friday", 2)
                .ScrollAndClickOnScheduleAvailabilityDayLabel("Saturday")
                .ClickCreateScheduleAvailabilityButton("Saturday", 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("OverTime");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .DragAvailabilityToRight("Saturday", 2)
                .ScrollAndClickOnScheduleAvailabilityDayLabel("Sunday")
                .ClickCreateScheduleAvailabilityButton("Sunday", 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Regular");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .DragTransportAvailabilityToLeft("Sunday", 2)
                .ClickOnSaveButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            System.Threading.Thread.Sleep(5000);
            var userSchedules = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserID);
            Assert.AreEqual(7, userSchedules.Count);

            systemUserAvailabilitySubPage
                .ClickAddWeekButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateCurrentSelectedWeek(2)
                .ClickCreateScheduleAvailabilityButton("Monday", 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .DragTransportAvailabilityToLeft("Monday", 2)
                .ScrollAndClickOnScheduleAvailabilityDayLabel("Tuesday")
                .ClickCreateScheduleAvailabilityButton("Tuesday", 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Regular");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .DragAvailabilityToRight("Tuesday", 2)
                .ScrollAndClickOnScheduleAvailabilityDayLabel("Wednesday")
                .ClickCreateScheduleAvailabilityButton("Wednesday", 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("OverTime");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .DragTransportAvailabilityToLeft("Wednesday", 2)
                .ScrollAndClickOnScheduleAvailabilityDayLabel("Thursday")
                .ClickCreateScheduleAvailabilityButton("Thursday", 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .DragAvailabilityToRight("Thursday", 2)
                .ScrollAndClickOnScheduleAvailabilityDayLabel("Friday")
                .ClickCreateScheduleAvailabilityButton("Friday", 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Regular");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .DragTransportAvailabilityToLeft("Friday", 2)
                .ScrollAndClickOnScheduleAvailabilityDayLabel("Saturday")
                .ClickCreateScheduleAvailabilityButton("Saturday", 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("OverTime");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .DragAvailabilityToRight("Saturday", 2)
                .ScrollAndClickOnScheduleAvailabilityDayLabel("Sunday")
                .ClickCreateScheduleAvailabilityButton("Sunday", 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("OverTime");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            System.Threading.Thread.Sleep(10000);
            userSchedules = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserID);
            Assert.AreEqual(14, userSchedules.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4314")]
        [Description("Step 37 to 38 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("Screen1", "Schedule Availability")]
        public void SystemUser_ScheduleAvailability_UITestCases016()
        {
            #region Test System User

            _systemUserName = "ACC563U" + partialStringSuffix;
            _systemUserID = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ACC563", "U" + partialStringSuffix, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserID, GetThisWeekFirstMonday());

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleType1id = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserID, DateTime.Now.Date, _careProviderStaffRoleType1id, _careProviders_TeamId, _employmentContractTypeid, 48m);

            #endregion

            #region Availability Type

            commonMethodsDB.CreateAvailabilityType("Standard", 3, false, _careProviders_TeamId, 1, 1, true);
            commonMethodsDB.CreateAvailabilityType("Regular", 5, false, _careProviders_TeamId, 1, 1, true);
            commonMethodsDB.CreateAvailabilityType("OverTime", 4, false, _careProviders_TeamId, 1, 1, true);

            #endregion

            #region Step 37, 38

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleAvailabilityButton("Monday", 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickAddWeekButton()

                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateCurrentSelectedWeek(2)
                .ClickCreateScheduleAvailabilityButton("Monday", 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Regular");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickAddWeekButton()

                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateCurrentSelectedWeek(3)
                .ClickCreateScheduleAvailabilityButton("Tuesday", 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("OverTime");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickAddWeekButton()

                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateCurrentSelectedWeek(4)
                .ClickCreateScheduleAvailabilityButton("Friday", 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickAddWeekButton()

                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateCurrentSelectedWeek(5)
                .ClickCreateScheduleAvailabilityButton("Monday", 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Regular");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickAddWeekButton()

                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateCurrentSelectedWeek(6)
                .ClickCreateScheduleAvailabilityButton("Saturday", 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("OverTime");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickAddWeekButton()

                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateCurrentSelectedWeek(7)
                .ClickCreateScheduleAvailabilityButton("Sunday", 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickAddWeekButton()

                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateCurrentSelectedWeek(8)
                .ClickCreateScheduleAvailabilityButton("Monday", 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Regular");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickAddWeekButton()

                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateCurrentSelectedWeek(9)
                .ClickCreateScheduleAvailabilityButton("Wednesday", 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("OverTime");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickAddWeekButton()

                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateCurrentSelectedWeek(10)
                .ClickCreateScheduleAvailabilityButton("Monday", 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Regular");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickAddWeekButton()

                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateCurrentSelectedWeek(11)
                .ClickCreateScheduleAvailabilityButton("Thursday", 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Standard");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickAddWeekButton()

                .WaitForSystemUserAvailabilitySubPageToLoad(false)
                .ValidateCurrentSelectedWeek(12)
                .ClickCreateScheduleAvailabilityButton("Friday", 1);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Regular");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad(false)
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved(false);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad();

            var userSchedules = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserID);
            Assert.AreEqual(12, userSchedules.Count);

            #endregion

        }

        #endregion

        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

    }

    static class datetimeext
    {
        //To Get The Day of the Week in C#
        public static DateTime GetDayOfWeek(this DateTime date, DayOfWeek dayOfTheWeek)
        {
            var dt = date.Date;
            var days = ((int)dayOfTheWeek - (int)dt.DayOfWeek) % 7;
            return dt.AddDays(days);
        }
    }

}

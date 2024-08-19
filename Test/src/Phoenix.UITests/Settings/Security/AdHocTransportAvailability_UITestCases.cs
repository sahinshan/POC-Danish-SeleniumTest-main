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
    public class AdHocTransportAvailability_UITestCases : FunctionalTest
    {
        #region Properties

        private string EnvironmentName;
        private Guid authenticationproviderid;
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _defaultLoginUserID;
        private Guid _systemUserID;
        public Guid environmentid;
        private string _loginUser_Name;
        private string _loginUser_Username;
        private string _systemUser_Username;
        private Guid _availabilityTypeId;
        private Guid _availabilityTypeId_Regular;
        private Guid _availabilityTypeId_overtime;
        private Guid _transportTypeId_Car;
        private Guid _transportTypeId_Walking;
        private Guid _transportTypeId_Bicycle;

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
                string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);

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

                var defaultLoginUserExists = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_1").Any();
                if (!defaultLoginUserExists)
                    _defaultLoginUserID = dbHelper.systemUser.CreateSystemUser("CW_Admin_Test_User_1", "CW", "Admin_Test_User_1", "CW Admin Test User 1", "Passw0rd_!", "CW_Admin_Test_User_1@somemail.com", "CW_Admin_Test_User_1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId, true, 4, null, DateTime.Now.Date);

                if (Guid.Empty == _defaultLoginUserID)
                    _defaultLoginUserID = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_1").FirstOrDefault();

                dbHelper.systemUser.UpdateLastPasswordChangedDate(_defaultLoginUserID, DateTime.Now.Date);

                _loginUser_Name = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultLoginUserID, "fullname")["fullname"];
                _loginUser_Username = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultLoginUserID, "username")["username"];

                #endregion  Create default system user

                #region Team Manager

                dbHelper.team.UpdateTeamManager(_careProviders_TeamId, _defaultLoginUserID);

                #endregion

                #region Create SystemUser Record

                var newSystemUser = dbHelper.systemUser.GetSystemUserByUserName("CDV6_15411_User").Any();
                if (!newSystemUser)
                    _systemUserID = dbHelper.systemUser.CreateSystemUser("CDV6_15411_User", "CDV6_15411", "User", "CDV6_15411 User", "Passw0rd_!", "CDV6_15411_User@somemail.com", "CDV6_15411_User@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);

                if (_systemUserID == Guid.Empty)
                    _systemUserID = dbHelper.systemUser.GetSystemUserByUserName("CDV6_15411_User").FirstOrDefault();

                _systemUser_Username = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemUserID, "username")["username"];

                #endregion  Create SystemUser Record

                #region Availability Types

                var availabilityTypeID = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").Any();
                if (!availabilityTypeID)
                {
                    _availabilityTypeId = dbHelper.availabilityTypes.CreateAvailabilityType("Salaried/Contracted", 1, false, _careProviders_TeamId, 1, 1, true);

                }
                if (_availabilityTypeId == Guid.Empty)
                {
                    _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted")[0];
                }

                var _availabilityTypeId1 = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Regular").Any();
                if (!_availabilityTypeId1)
                {
                    _availabilityTypeId_Regular = dbHelper.availabilityTypes.CreateAvailabilityType("Regular", 5, false, _careProviders_TeamId, 1, 1, true);

                }
                if (_availabilityTypeId_Regular == Guid.Empty)
                {
                    _availabilityTypeId_Regular = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Regular")[0];
                }

                var availabilityTypeID3 = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Hourly/Overtime").Any();
                if (!availabilityTypeID3)
                {
                    _availabilityTypeId_overtime = dbHelper.availabilityTypes.CreateAvailabilityType("Hourly/Overtime", 52, false, _careProviders_TeamId, 1, 1, true);

                }
                if (_availabilityTypeId_overtime == Guid.Empty)
                {
                    _availabilityTypeId_overtime = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Hourly/Overtime")[0];
                }

                #endregion

                #region TransportType

                var transportTypeExists = dbHelper.transportType.GetTransportTypeByName("Car").Any();
                if (!transportTypeExists)
                    dbHelper.transportType.CreateTransportType(_careProviders_TeamId, "Car", DateTime.Now.Date, 1, "50", 1);
                _transportTypeId_Car = dbHelper.transportType.GetTransportTypeByName("Car")[0];

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

        #region https://advancedcsg.atlassian.net/browse/CDV6-19411

        [TestProperty("JiraIssueID", "ACC-3071")]
        [Description("Login CD as a Care Provider." +
            "Verify That  active system user without Employement Contract can able to see 'Schedule Transport' & 'View Diary / Manage Ad Hoc' tabs." +
            "Verify that In The Availabiity Tab The Error Message Should Display As \"Current system user does not have any employment contracts.\"" +
            "Verify That created Schedule Transport availability Record should be display in View Diary / Manage Ad Hoc section.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Availability")]
        [TestProperty("Screen1", "View Diary Manage Ad Hoc")]
        public void AdHocTransportAvailability_UITestCases001()
        {
            string currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();

            foreach (var sysuserId in dbHelper.userTransportationSchedule.GetUserTransportationScheduleIdBySystemUserId(_systemUserID))
                dbHelper.userTransportationSchedule.DeleteUserTransportationSchedule(sysuserId);

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
                .InsertUserName(_systemUser_Username)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateViewDiary_ManageAdHocAreaDislayed();

            #endregion

            #region Step 3

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard()
                .ValidateScheduleTransportAreaDislayed()
                .ClickRefreshButton();

            System.Threading.Thread.Sleep(3000);

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateSystemUserWarningMessage("Current system user does not have any employment contracts.");

            systemUserAvailabilitySubPage
                .ClickCreateScheduleTransportButton(currentDayOfTheWeek, 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ClickCarButton();

            System.Threading.Thread.Sleep(1000);

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForSystemUserAvailabilitySubPageToLoad();

            #endregion

            #region Step 4

            systemUserAvailabilitySubPage
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateRecordUnderViewDiary_ManageAdHocArea(currentDayOfTheWeek, 2, "car");

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-19436

        [TestProperty("JiraIssueID", "ACC-3070")]
        [Description("Login CD as a Care Provider." +
            "Add new Slot for Ad Hoc Transport availability in the 'View Diary / Manage Ad Hoc' screen." +
            "Verify newly Added slot should be display in User Transport Schedules screen through Advance Search.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Availability")]
        [TestProperty("Screen1", "View Diary Manage Ad Hoc")]
        public void AdHocTransportAvailability_UITestCases002()
        {
            string currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();
            string currentDate = DateTime.Now.ToString("dd\\/MM\\/yyyy");

            _transportTypeId_Car = dbHelper.transportType.GetTransportTypeByName("Car")[0];

            foreach (var sysuserId in dbHelper.userTransportationSchedule.GetUserTransportationScheduleIdBySystemUserId(_systemUserID))
                dbHelper.userTransportationSchedule.DeleteUserTransportationSchedule(sysuserId);

            #region Step 5

            loginPage
              .GoToLoginPage()
              .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

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
                .ValidateScheduleTransportAreaDislayed()
                .ValidateViewDiary_ManageAdHocAreaDislayed();

            systemUserAvailabilitySubPage
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickCreateTransportAvailabilityButton(currentDate, 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoadFromViewDiarAdhoc(true)
                .ClickCarButton();

            System.Threading.Thread.Sleep(5000);

            var _userTransportationScheduleid = dbHelper.userTransportationSchedule.GetUserTransportationScheduleIdBySystemUserId(_systemUserID).FirstOrDefault();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
               .WaitForAdvanceSearchPageToLoad()
               .SelectRecordType("User Transportation Schedules")

               .SelectFilter("1", "User")
               .SelectOperator("1", "Equals")
               .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery(_systemUser_Username).TapSearchButton().SelectResultElement(_systemUserID.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton();
            System.Threading.Thread.Sleep(5000);

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_userTransportationScheduleid.ToString())
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid.ToString(), 2, "Ad-Hoc: Car")
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid.ToString(), 7, currentDate)
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid.ToString(), 8, "09:00")
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid.ToString(), 9, currentDate)
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid.ToString(), 10, "17:00");

            #endregion

            #region Step 6

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
                .ValidateScheduleTransportAreaDislayed()
                .ValidateViewDiary_ManageAdHocAreaDislayed();

            systemUserAvailabilitySubPage
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickCreateTransportAvailabilityButton(currentDate, 2);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoadFromViewDiarAdhoc(false)
                .ClickWalkingButton();
            System.Threading.Thread.Sleep(3000);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
               .WaitForAdvanceSearchPageToLoad()
               .SelectRecordType("User Transportation Schedules")

               .SelectFilter("1", "User")
               .SelectOperator("1", "Equals")
               .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery(_systemUser_Username).TapSearchButton().SelectResultElement(_systemUserID.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton();
            System.Threading.Thread.Sleep(5000);

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_userTransportationScheduleid.ToString())
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid.ToString(), 2, "Ad-Hoc: Walking")
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid.ToString(), 7, currentDate)
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid.ToString(), 8, "09:00")
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid.ToString(), 9, currentDate)
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid.ToString(), 10, "17:00");

            #endregion
        }


        [TestProperty("JiraIssueID", "ACC-3073")]
        [Description("Login CD as a Care Provider." +
            "Edit Transport type for Existing transport availability slots in the 'View Diary / Manage Ad Hoc' screen." +
            "Verify Edited Ad Hoc Slot should be display in User Transport Schedules screen through Advance Search.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Availability")]
        [TestProperty("Screen1", "View Diary Manage Ad Hoc")]
        public void AdHocTransportAvailability_UITestCases003()
        {
            string currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();
            string currentDate = DateTime.Now.ToString("dd\\/MM\\/yyyy");

            _transportTypeId_Car = dbHelper.transportType.GetTransportTypeByName("Car")[0];

            foreach (var sysuserId in dbHelper.userTransportationSchedule.GetUserTransportationScheduleIdBySystemUserId(_systemUserID))
                dbHelper.userTransportationSchedule.DeleteUserTransportationSchedule(sysuserId);

            _transportTypeId_Walking = dbHelper.transportType.GetTransportTypeByName("Walking")[0];

            #region Step 7

            loginPage
              .GoToLoginPage()
              .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

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
                .ClickScheduleTransportCard()
                .WaitForSystemUserAvailabilitySubPageToLoad();

            systemUserAvailabilitySubPage
                .ClickCreateScheduleTransportButtonByDate(currentDate, 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForSystemUserAvailabilitySubPageToLoad();

            System.Threading.Thread.Sleep(2000);

            systemUserAvailabilitySubPage
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate, 2, "car");

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickCreateTransportAvailabilityButton(currentDate, 2);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoadFromViewDiarAdhoc(false)
                .ClickWalkingButton();

            System.Threading.Thread.Sleep(3000);

            var _userTransportationScheduleid = dbHelper.userTransportationSchedule.GetUserTransportationScheduleBySystemUserIdAndTransportTypeId(_systemUserID, _transportTypeId_Walking).FirstOrDefault();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
               .WaitForAdvanceSearchPageToLoad()
               .SelectRecordType("User Transportation Schedules")

               .SelectFilter("1", "User")
               .SelectOperator("1", "Equals")
               .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery(_systemUser_Username).TapSearchButton().SelectResultElement(_systemUserID.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton();
            System.Threading.Thread.Sleep(3000);

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_userTransportationScheduleid.ToString())
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid.ToString(), 2, "Ad-Hoc: Walking")
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid.ToString(), 7, currentDate)
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid.ToString(), 8, "09:00")
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid.ToString(), 9, currentDate)
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid.ToString(), 10, "17:00");


            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3072")]
        [Description("Login CD as a Care Provider." +
            "Merge the same transport types for Ad Hoc and Existing Transport availability slots." +
            "Verify that Merged Ad Hoc Slot should be display in User Transport Schedules screen through Advance Search.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule", "Care Provider Availability")]
        [TestProperty("Screen1", "View Diary Manage Ad Hoc")]
        public void AdHocTransportAvailability_UITestCases004()
        {
            string currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();
            string currentDate = DateTime.Now.ToString("dd\\/MM\\/yyyy");

            _transportTypeId_Car = dbHelper.transportType.GetTransportTypeByName("Car")[0];

            foreach (var sysuserId in dbHelper.userTransportationSchedule.GetUserTransportationScheduleIdBySystemUserId(_systemUserID))
                dbHelper.userTransportationSchedule.DeleteUserTransportationSchedule(sysuserId);

            #region Step 8 & 9

            loginPage
              .GoToLoginPage()
              .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

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
                .ValidateScheduleTransportAreaDislayed()
                .ValidateViewDiary_ManageAdHocAreaDislayed();

            systemUserAvailabilitySubPage
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickCreateTransportAvailabilityButton(currentDate, 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoadFromViewDiarAdhoc(true)
                .ClickCarButton();

            System.Threading.Thread.Sleep(3000);

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickCreateTransportAvailabilityButton(currentDate, 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoadFromViewDiarAdhoc(true)
                .ClickCarButton();

            System.Threading.Thread.Sleep(3000);

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .DragAndDropTransportAvailabilitySlotFromLeftToRight(currentDate, 4, 2);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
               .WaitForAdvanceSearchPageToLoad()
               .SelectRecordType("User Transportation Schedules")

               .SelectFilter("1", "User")
               .SelectOperator("1", "Equals")
               .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery(_systemUser_Username).TapSearchButton().SelectResultElement(_systemUserID.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton();

            System.Threading.Thread.Sleep(3000);

            var _userTransportationScheduleid = dbHelper.userTransportationSchedule.GetUserTransportationScheduleIdBySystemUserId(_systemUserID).FirstOrDefault();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_userTransportationScheduleid.ToString())
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid.ToString(), 2, "Ad-Hoc: Car")
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid.ToString(), 7, currentDate)
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid.ToString(), 8, "00:30")
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid.ToString(), 9, currentDate)
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid.ToString(), 10, "17:00");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3075")]
        [Description("Login CD as a Care Provider." +
            "Merge the different transport types for Ad Hoc slots." +
            "Verify that system should not allow to merge the different types of Ad Hoc slots.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Availability")]
        [TestProperty("Screen1", "View Diary Manage Ad Hoc")]
        public void AdHocTransportAvailability_UITestCases005()
        {
            string currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();
            string currentDate = DateTime.Now.ToString("dd\\/MM\\/yyyy");

            _transportTypeId_Car = dbHelper.transportType.GetTransportTypeByName("Car")[0];
            _transportTypeId_Walking = dbHelper.transportType.GetTransportTypeByName("Walking")[0];

            foreach (var sysuserId in dbHelper.userTransportationSchedule.GetUserTransportationScheduleIdBySystemUserId(_systemUserID))
                dbHelper.userTransportationSchedule.DeleteUserTransportationSchedule(sysuserId);

            #region Step 10

            loginPage
              .GoToLoginPage()
              .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

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
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickCreateTransportAvailabilityButton(currentDate, 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoadFromViewDiarAdhoc(true)
                .ClickCarButton();

            System.Threading.Thread.Sleep(5000);

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickCreateTransportAvailabilityButton(currentDate, 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoadFromViewDiarAdhoc(true)
                .ClickWalkingButton();

            System.Threading.Thread.Sleep(5000);

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .DragAndDropTransportAvailabilitySlotFromLeftToRight(currentDate, 4, 2);

            var _userTransportationScheduleid1 = dbHelper.userTransportationSchedule.GetUserTransportationScheduleBySystemUserIdAndTransportTypeId(_systemUserID, _transportTypeId_Car).FirstOrDefault();
            var _userTransportationScheduleid2 = dbHelper.userTransportationSchedule.GetUserTransportationScheduleBySystemUserIdAndTransportTypeId(_systemUserID, _transportTypeId_Walking).FirstOrDefault();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
               .WaitForAdvanceSearchPageToLoad()
               .SelectRecordType("User Transportation Schedules")

               .SelectFilter("1", "User")
               .SelectOperator("1", "Equals")
               .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery(_systemUser_Username).TapSearchButton().SelectResultElement(_systemUserID.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton();
            System.Threading.Thread.Sleep(3000);

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_userTransportationScheduleid1.ToString())
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid1.ToString(), 2, "Ad-Hoc: Car")
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid1.ToString(), 7, currentDate)
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid1.ToString(), 8, "08:30")
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid1.ToString(), 9, currentDate)
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid1.ToString(), 10, "17:00")

                .ValidateSearchResultRecordPresent(_userTransportationScheduleid2.ToString())
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid2.ToString(), 2, "Ad-Hoc: Walking")
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid2.ToString(), 7, currentDate)
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid2.ToString(), 8, "00:30")
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid2.ToString(), 9, currentDate)
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid2.ToString(), 10, "08:30");

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-19414

        [TestProperty("JiraIssueID", "ACC-3074")]
        [Description("Login CD as a Care Provider." +
            "Verify that User able to Extend/Shrink the Transport availabilty and Existing Schedule Transport availability time slot for Ad Hoc" +
            "Verify that User able to Remove Ad Hoc Transport & Schedule Transport Availability Slot." +
            "Verify that changes will be display in the Audit List.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Availability")]
        [TestProperty("Screen1", "View Diary Manage Ad Hoc")]
        [TestProperty("Screen2", "Schedule Transport")]
        public void AdHocTransportAvailability_UITestCases006()
        {
            string currentDate = DateTime.Now.ToString("dd\\/MM\\/yyyy");

            _transportTypeId_Car = dbHelper.transportType.GetTransportTypeByName("Car")[0];
            _transportTypeId_Walking = dbHelper.transportType.GetTransportTypeByName("Walking")[0];

            foreach (var sysuserId in dbHelper.userTransportationSchedule.GetUserTransportationScheduleIdBySystemUserId(_systemUserID))
                dbHelper.userTransportationSchedule.DeleteUserTransportationSchedule(sysuserId);

            #region Step 11 & 12 & 16

            loginPage
              .GoToLoginPage()
              .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

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
                .ValidateViewDiary_ManageAdHocAreaDislayed()
                .ClickScheduleTransportCard();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(currentDate, 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ClickCarButton();

            System.Threading.Thread.Sleep(5000);

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForSystemUserAvailabilitySubPageToLoad();

            systemUserAvailabilitySubPage
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickCreateTransportAvailabilityButton(currentDate, 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoadFromViewDiarAdhoc(true)
                .ClickWalkingButton();

            System.Threading.Thread.Sleep(5000);

            #region Step 16

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad();

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "walking", false);

            #endregion

            systemUserAvailabilitySubPage
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .DragSpecificSlotToRight(currentDate, "car"); System.Threading.Thread.Sleep(3000);

            systemUserViewDiaryManageAdHocPage
                .DragSpecificSlotToRight(currentDate, "walking")
                .DragSpecificSlotToRight(currentDate, "car"); System.Threading.Thread.Sleep(3000);

            var _userTransportationScheduleid1 = dbHelper.userTransportationSchedule.GetUserTransportationScheduleBySystemUserId_TransportTypeId_AdHoc(_systemUserID, _transportTypeId_Car, 1).FirstOrDefault();
            var _userTransportationScheduleid2 = dbHelper.userTransportationSchedule.GetUserTransportationScheduleBySystemUserId_TransportTypeId_AdHoc(_systemUserID, _transportTypeId_Walking, 1).FirstOrDefault();
            var carFields = dbHelper.userTransportationSchedule.GetUserTransportationScheduleByID(_userTransportationScheduleid1, "starttime", "endtime");
            var walkingFields = dbHelper.userTransportationSchedule.GetUserTransportationScheduleByID(_userTransportationScheduleid2, "starttime", "endtime");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
               .WaitForAdvanceSearchPageToLoad()
               .SelectRecordType("User Transportation Schedules")

               .SelectFilter("1", "User")
               .SelectOperator("1", "Equals")
               .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery(_systemUser_Username).TapSearchButton().SelectResultElement(_systemUserID.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton();
            System.Threading.Thread.Sleep(3000);

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_userTransportationScheduleid1.ToString())
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid1.ToString(), 2, "Ad-Hoc: Car")
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid1.ToString(), 7, currentDate)
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid1.ToString(), 8, carFields["starttime"].ToString().Substring(0, 5))
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid1.ToString(), 9, currentDate)
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid1.ToString(), 10, carFields["endtime"].ToString().Substring(0, 5))

                .ValidateSearchResultRecordPresent(_userTransportationScheduleid2.ToString())
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid2.ToString(), 2, "Ad-Hoc: Walking")
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid2.ToString(), 7, currentDate)
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid2.ToString(), 8, walkingFields["starttime"].ToString().Substring(0, 5))
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid2.ToString(), 9, currentDate)
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid2.ToString(), 10, walkingFields["endtime"].ToString().Substring(0, 5));

            #endregion

            #region Step 17

            advanceSearchPage
                .OpenRecord(_userTransportationScheduleid1.ToString());

            userTransportationSchedulePage
                .WaitForResultsPageToLoad(_userTransportationScheduleid1.ToString())
                .NavigateToRelatedItemsSubPage_Audit();

            userTransportationSchedulePage
                .WaitForAuditListPageToLoad(_userTransportationScheduleid1.ToString())
                .ValidateAuditRecordOnAuditList("Created")
                .ValidateAuditRecordOnAuditList("Updated");

            #endregion

            #region Step 13 & 14

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
                .ValidateViewDiary_ManageAdHocAreaDislayed()
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickSpecificSlotToCreateOrEditTransportAvailability(currentDate, "walking");

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoadFromViewDiarAdhoc(false)
                .ClickRemoveTimeSlotButton();

            System.Threading.Thread.Sleep(3000);

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickSpecificSlotToCreateOrEditTransportAvailability(currentDate, "car");

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoadFromViewDiarAdhoc(false)
                .ClickRemoveTimeSlotButton();

            System.Threading.Thread.Sleep(3000);

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(currentDate, "car", false)
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(currentDate, "walking", false);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3077")]
        [Description("Login CD as a Care Provider." +
            "Verify that Automatic slots merge will be haapen when slot should be created in between 15 min gap of two same transport slots.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Availability")]
        [TestProperty("Screen1", "View Diary Manage Ad Hoc")]
        public void AdHocTransportAvailability_UITestCases007()
        {
            string currentDate = DateTime.Now.ToString("dd\\/MM\\/yyyy");
            _transportTypeId_Walking = dbHelper.transportType.GetTransportTypeByName("Walking")[0];

            foreach (var sysuserId in dbHelper.userTransportationSchedule.GetUserTransportationScheduleIdBySystemUserId(_systemUserID))
                dbHelper.userTransportationSchedule.DeleteUserTransportationSchedule(sysuserId);

            #region Step 15

            loginPage
              .GoToLoginPage()
              .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

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
                .ValidateViewDiary_ManageAdHocAreaDislayed()
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickCreateTransportAvailabilityButton(currentDate, 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoadFromViewDiarAdhoc(true)
                .ClickWalkingButton();

            System.Threading.Thread.Sleep(5000);

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateAvailableSlotsOnMangeAdHoc(currentDate, "walking", 1);

            systemUserViewDiaryManageAdHocPage
                .ClickCreateTransportAvailabilityButton(currentDate, 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoadFromViewDiarAdhoc(true)
                .ClickWalkingButton();

            System.Threading.Thread.Sleep(3000);

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateAvailableSlotsOnMangeAdHoc(currentDate, "walking", 2);

            systemUserViewDiaryManageAdHocPage
                .ClickCreateTransportAvailabilityButton(currentDate, 3);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoadFromViewDiarAdhoc(true)
                .ClickWalkingButton();

            System.Threading.Thread.Sleep(3000);

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateAvailableSlotsOnMangeAdHoc(currentDate, "walking", 1);

            var _userTransportationScheduleid1 = dbHelper.userTransportationSchedule.GetUserTransportationScheduleBySystemUserIdAndTransportTypeId(_systemUserID, _transportTypeId_Walking).FirstOrDefault();
            var walkingFields = dbHelper.userTransportationSchedule.GetUserTransportationScheduleByID(_userTransportationScheduleid1, "starttime", "endtime");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
               .WaitForAdvanceSearchPageToLoad()
               .SelectRecordType("User Transportation Schedules")

               .SelectFilter("1", "User")
               .SelectOperator("1", "Equals")
               .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery(_systemUser_Username).TapSearchButton().SelectResultElement(_systemUserID.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton();

            System.Threading.Thread.Sleep(3000);

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_userTransportationScheduleid1.ToString())
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid1.ToString(), 2, "Ad-Hoc: Walking")
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid1.ToString(), 7, currentDate)
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid1.ToString(), 8, walkingFields["starttime"].ToString().Substring(0, 5))
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid1.ToString(), 9, currentDate)
                .ValidateSearchResultRecordCellContent(_userTransportationScheduleid1.ToString(), 10, walkingFields["endtime"].ToString().Substring(0, 5));

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3076")]
        [Description("Login CD as a Care Provider." +
            "Verify that Changing a schedule should not generate Transport availability diary record for Ad Hoc Date" +
            "Verify that Changing a schedule should not generate Transport availability diary records for an ad-hoc date" +
            "Verify that Removing a schedule should not generate Transport availability diary records for an ad-hoc date")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Availability")]
        [TestProperty("Screen1", "View Diary Manage Ad Hoc")]
        [TestProperty("Screen2", "Schedule Transport")]
        public void AdHocTransportAvailability_UITestCases008()
        {
            string currentDate = DateTime.Now.ToString("dd\\/MM\\/yyyy");
            string currentDate_7 = DateTime.Now.AddDays(7).ToString("dd\\/MM\\/yyyy");
            string currentDate_14 = DateTime.Now.AddDays(14).ToString("dd\\/MM\\/yyyy");

            _transportTypeId_Car = dbHelper.transportType.GetTransportTypeByName("Car")[0];
            _transportTypeId_Bicycle = dbHelper.transportType.GetTransportTypeByName("Bicycle")[0];

            foreach (var sysuserId in dbHelper.userTransportationSchedule.GetUserTransportationScheduleIdBySystemUserId(_systemUserID))
                dbHelper.userTransportationSchedule.DeleteUserTransportationSchedule(sysuserId);

            #region Step 18 & 19 & 20

            loginPage
              .GoToLoginPage()
              .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

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
                .ValidateViewDiary_ManageAdHocAreaDislayed()
                .ClickScheduleTransportCard();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(currentDate, 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ClickBicycleButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForSystemUserAvailabilitySubPageToLoad();

            systemUserAvailabilitySubPage
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickCreateTransportAvailabilityButton(currentDate, 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoadFromViewDiarAdhoc(true)
                .ClickCarButton();

            System.Threading.Thread.Sleep(5000);

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(currentDate, "bicycle")
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(currentDate, "car");

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .DragSpecificSlotToRight(currentDate, "bicycle");

            System.Threading.Thread.Sleep(5000);

            systemUserAvailabilitySubPage
                .ClickOnSaveButton()
                .WaitForSystemUserAvailabilitySubPageToLoad();

            dbHelper = new DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var _userTransportationScheduleid1 = dbHelper.userTransportationSchedule.GetUserTransportationScheduleBySystemUserId_TransportTypeId_AdHoc(_systemUserID, _transportTypeId_Bicycle, 0).FirstOrDefault();
            var bicycleFields = dbHelper.userTransportationSchedule.GetUserTransportationScheduleByID(_userTransportationScheduleid1, "starttime", "endtime");

            systemUserAvailabilitySubPage
               .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateTooltipOnMouseHover(currentDate, "bicycle", "Bicycle: 09:00 – 17:00")
                .ValidateTooltipOnMouseHover(currentDate_7, "bicycle", "Bicycle: " + bicycleFields["starttime"].ToString().Substring(0, 5) + " – " + bicycleFields["endtime"].ToString().Substring(0, 5) + "")
                .ValidateTooltipOnMouseHover(currentDate_14, "bicycle", "Bicycle: " + bicycleFields["starttime"].ToString().Substring(0, 5) + " – " + bicycleFields["endtime"].ToString().Substring(0, 5) + "");

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickSpecificSlotToCreateOrEditScheduleTransport(currentDate, "bicycle");

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(false)
                .ClickRemoveTimeSlotButton();

            System.Threading.Thread.Sleep(3000);

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForSystemUserAvailabilitySubPageToLoad();

            systemUserAvailabilitySubPage
               .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(currentDate, "bicycle", true)
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(currentDate_7, "bicycle", false)
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(currentDate_14, "bicycle", false);

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

}

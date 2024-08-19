using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phoenix.UITests.Framework.PageObjects;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Security
{
    [TestClass]
    public class UserWorkSchedules_UITestCases : FunctionalTest
    {
        #region Properties

        private string EnvironmentName;
        private Guid authenticationproviderid;
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _defaultLoginUserID;
        public Guid environmentid;
        public Guid systemAdministratorSecurityProfileId;
        public Guid systemUserSecureFieldsSecurityProfileId;
        public string _loginUser_Name;
        public string _loginUser_Username;

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

                EnvironmentName = ConfigurationManager.AppSettings["EnvironmentName"];
                string tenantName = ConfigurationManager.AppSettings["TenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);

                #endregion

                #region Business Unit
                var businessUnitExists = dbHelper.businessUnit.GetByName("CareDirector").Any();
                if (!businessUnitExists)
                    dbHelper.businessUnit.CreateBusinessUnit("CareDirector");
                _careProviders_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector")[0];


                #endregion

                #region Language

                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
                if (!language)
                    dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
                _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

                #endregion Language

                #region Team

                var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CareDirector", null, _careProviders_BusinessUnitId, "90400", "CareDirector@careworkstempmail.com", "CareDirector", "020 123456");
                _careProviders_TeamId = dbHelper.team.GetTeamIdByName("CareDirector")[0];

                #endregion

                #region Security Profiles

                systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();

                #endregion


            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-20302

        [TestProperty("JiraIssueID", "CDV6-20316")]
        [Description("Set the 'EnableAdvancedSearchAllToolbarButtons' system setting to True - open a system user record - navigate to the  Work Schedule tab - " +
            "Validate that is possible to create a new work schedule record for the user")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void UserWorkSchedules__UITestCases001()
        {
            #region Create default system user

            var currentDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            var username = "Testing_CDV6_20302" + "_" + currentDate;

            _defaultLoginUserID = dbHelper.systemUser.CreateSystemUser(username, "Testing_CDV6_20302_", "currentDate", "Testing_CDV6_20302" + " " + currentDate, "Passw0rd_!", username + "@somemail.com", username + "@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId, true);

            //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, systemAdministratorSecurityProfileId);
            //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, systemUserSecureFieldsSecurityProfileId);

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_defaultLoginUserID, DateTime.Now.Date);

            _loginUser_Name = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultLoginUserID, "fullname")["fullname"];
            _loginUser_Username = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultLoginUserID, "username")["username"];

            #endregion

            if (!dbHelper.systemSetting.GetSystemSettingIdByName("EnableAdvancedSearchAllToolbarButtons").Any())
                dbHelper.systemSetting.CreateSystemSetting("EnableAdvancedSearchAllToolbarButtons", "True", "", false, "");

            var systemSettingId = dbHelper.systemSetting.GetSystemSettingIdByName("EnableAdvancedSearchAllToolbarButtons")[0];
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingId, "True");

            var recurrencePatternId = dbHelper.recurrencePattern.GetRecurrencePatternIdByName("Occurs every 1 days")[0];


            loginPage
              .GoToLoginPage()
              .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName)
              .WaitFormHomePageToLoad(false, false, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
              .WaitForSystemUsersPageToLoad()
              .InsertUserName(_loginUser_Username)
              .ClickSearchButton()
              .WaitForResultsGridToLoad()
              .OpenRecord(_defaultLoginUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToWorkScheduleSubPage();

            userWorkSchedulesPage
                .WaitForUserWorkSchedulesPageToLoad()
                .ClickNewRecordButton();

            SystemUserUserWorkScheduleRecordPage
                .WaitForSystemUserUserWorkScheduleRecordPageToLoad()
                .InsertTextTitleTextBar("Default schedule")
                .InsertStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertStartTime("09:00")
                .InsertEndTime("21:00")
                .ClickRecurrencePatterLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Occurs every 1 days").SelectResultElement(recurrencePatternId.ToString());

            SystemUserUserWorkScheduleRecordPage
                .WaitForSystemUserUserWorkScheduleRecordPageToLoad()
                .ClickSaveAndCloseButton();

            userWorkSchedulesPage
                .WaitForUserWorkSchedulesPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var totalWorkSchedules = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_defaultLoginUserID).Count();
            Assert.AreEqual(1, totalWorkSchedules);
        }

        [TestProperty("JiraIssueID", "CDV6-20327")]
        [Description("Set the 'EnableAdvancedSearchAllToolbarButtons' system setting to False - open a system user record - navigate to the  Work Schedule tab - " +
            "Validate that, even when the system setting is set to false, is possible to create a new work schedule record for the user")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void UserWorkSchedules__UITestCases002()
        {
            #region Create default system user

            var currentDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            var username = "Testing_CDV6_20302" + "_" + currentDate;

            _defaultLoginUserID = dbHelper.systemUser.CreateSystemUser(username, "Testing_CDV6_20302_", "currentDate", "Testing_CDV6_20302" + " " + currentDate, "Passw0rd_!", username + "@somemail.com", username + "@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId, true);

            //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, systemAdministratorSecurityProfileId);
            //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, systemUserSecureFieldsSecurityProfileId);

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_defaultLoginUserID, DateTime.Now.Date);

            _loginUser_Name = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultLoginUserID, "fullname")["fullname"];
            _loginUser_Username = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultLoginUserID, "username")["username"];

            #endregion

            if (!dbHelper.systemSetting.GetSystemSettingIdByName("EnableAdvancedSearchAllToolbarButtons").Any())
                dbHelper.systemSetting.CreateSystemSetting("EnableAdvancedSearchAllToolbarButtons", "True", "", false, "");

            var systemSettingId = dbHelper.systemSetting.GetSystemSettingIdByName("EnableAdvancedSearchAllToolbarButtons")[0];
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingId, "False");

            var recurrencePatternId = dbHelper.recurrencePattern.GetRecurrencePatternIdByName("Occurs every 1 days")[0];


            loginPage
              .GoToLoginPage()
              .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName)
              .WaitFormHomePageToLoad(false, false, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
              .WaitForSystemUsersPageToLoad()
              .InsertUserName(_loginUser_Username)
              .ClickSearchButton()
              .WaitForResultsGridToLoad()
              .OpenRecord(_defaultLoginUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToWorkScheduleSubPage();

            userWorkSchedulesPage
                .WaitForUserWorkSchedulesPageToLoad()
                .ClickNewRecordButton();

            SystemUserUserWorkScheduleRecordPage
                .WaitForSystemUserUserWorkScheduleRecordPageToLoad()
                .InsertTextTitleTextBar("Default schedule")
                .InsertStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertStartTime("09:00")
                .InsertEndTime("21:00")
                .ClickRecurrencePatterLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Occurs every 1 days").SelectResultElement(recurrencePatternId.ToString());

            SystemUserUserWorkScheduleRecordPage
                .WaitForSystemUserUserWorkScheduleRecordPageToLoad()
                .ClickSaveAndCloseButton();

            userWorkSchedulesPage
                .WaitForUserWorkSchedulesPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var totalWorkSchedules = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_defaultLoginUserID).Count();
            Assert.AreEqual(1, totalWorkSchedules);
        }

        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

        #endregion

    }
}

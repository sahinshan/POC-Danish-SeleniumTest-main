using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Security
{
    [TestClass]
    public class SystemUser_UITestCases2 : FunctionalTest
    {
        private string _environmentName;
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private string _systemUsername;
        private string _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");


        [TestInitialize()]
        public void TestsSetupMethod()
        {
            try
            {
                #region Environment 

                _environmentName = ConfigurationManager.AppSettings["EnvironmentName"];

                #endregion

                #region Internal

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("Security BU2");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("Security T2", null, _businessUnitId, "907678", "SecurityT2@careworkstempmail.com", "Security T2", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region System User SecurityUser2

                _systemUsername = "SecurityUser2";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "Security", "User2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion             
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-1083

        [TestProperty("JiraIssueID", "CDV6-25236")]
        [Description("Step(s) 1 to 5 for the test CDV6-5122")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void SystemUser_Deactivation_UITestMethod01()
        {
            var currentDateTime = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User to deactivate

            _systemUsername = "DeactivationUser_" + currentDateTime;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "DeactivationUser", currentDateTime, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Job Role Type

            var _jobRoleTypeId = commonMethodsDB.CreateJobRoleType(_teamId, "Default Consultant", new DateTime(2020, 1, 1), 1);

            #endregion

            #region Update System User Job Role Type

            dbHelper.systemUser.UpdateJobRoleType(_systemUserId, _jobRoleTypeId);

            #endregion

            #region Recurrent Pattern

            var _recurrencePatternId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").FirstOrDefault();

            #endregion

            #region User Work Schedule

            var startDate = new DateTime(2023, 1, 1);
            DateTime? endDate = null;
            var startTime = new TimeSpan(0, 5, 0);
            var endTime = new TimeSpan(23, 55, 0);
            commonMethodsDB.CreateUserWorkSchedule("Default", _systemUserId, _teamId, _recurrencePatternId, startDate, endDate, startTime, endTime);

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login("SecurityUser2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_systemUsername)
                .ClickSearchButton()
                .OpenRecord(_systemUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad();

            #endregion

            #region Step 3

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ClickInactiveStatus_YesOption();

            #endregion

            #region Step 4

            systemUserRecordPage
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Cannot deactivate this User record, there are active Work Schedule records associated with it.")
                .TapCloseButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-25237")]
        [Description("Step(s) 5 and 6 for the test CDV6-5122")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void SystemUser_Deactivation_UITestMethod02()
        {
            var currentDateTime = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User to deactivate

            _systemUsername = "DeactivationUser_" + currentDateTime;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "DeactivationUser", currentDateTime, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Job Role Type

            var _jobRoleTypeId = commonMethodsDB.CreateJobRoleType(_teamId, "Default Consultant", new DateTime(2020, 1, 1), 1);

            #endregion

            #region Update System User Job Role Type

            dbHelper.systemUser.UpdateJobRoleType(_systemUserId, _jobRoleTypeId);

            #endregion

            #region Recurrent Pattern

            var _recurrencePatternId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").FirstOrDefault();

            #endregion

            #region User Work Schedule

            var startDate = new DateTime(2023, 1, 1);
            var endDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddMonths(1)).Date;
            var startTime = new TimeSpan(0, 5, 0);
            var endTime = new TimeSpan(23, 55, 0);
            commonMethodsDB.CreateUserWorkSchedule("Default", _systemUserId, _teamId, _recurrencePatternId, startDate, endDate, startTime, endTime);

            #endregion

            #region Step 5 and 6

            loginPage
                .GoToLoginPage()
                .Login("SecurityUser2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_systemUsername)
                .ClickSearchButton()
                .OpenRecord(_systemUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad();

            systemUserRecordPage
                .NavigateToDetailsPage()
                .ClickInactiveStatus_YesOption();

            systemUserRecordPage
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Cannot deactivate this User record, there are active Work Schedule records associated with it.")
                .TapCloseButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-25238")]
        [Description("Step(s) 7 and 8 for the test CDV6-5122")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void SystemUser_Deactivation_UITestMethod03()
        {
            var currentDateTime = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User to deactivate

            _systemUsername = "DeactivationUser_" + currentDateTime;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "DeactivationUser", currentDateTime, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Job Role Type

            var _jobRoleTypeId = commonMethodsDB.CreateJobRoleType(_teamId, "Default Consultant", new DateTime(2020, 1, 1), 1);

            #endregion

            #region Update System User Job Role Type

            dbHelper.systemUser.UpdateJobRoleType(_systemUserId, _jobRoleTypeId);

            #endregion

            #region Recurrent Pattern

            var _recurrencePatternId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").FirstOrDefault();

            #endregion

            #region User Work Schedule

            var startDate = new DateTime(2023, 1, 1);
            var endDate = new DateTime(2023, 2, 1);
            var startTime = new TimeSpan(0, 5, 0);
            var endTime = new TimeSpan(23, 55, 0);
            commonMethodsDB.CreateUserWorkSchedule("Default", _systemUserId, _teamId, _recurrencePatternId, startDate, endDate, startTime, endTime);

            #endregion

            #region Step 7 and 8

            loginPage
                .GoToLoginPage()
                .Login("SecurityUser2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_systemUsername)
                .ClickSearchButton()
                .OpenRecord(_systemUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad();

            systemUserRecordPage
                .NavigateToDetailsPage()
                .ClickInactiveStatus_YesOption();

            systemUserRecordPage
                .ClickSaveAndCloseButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .OpenRecord(_systemUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad();

            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-25239")]
        [Description("Step(s) 9 to 16 for the test CDV6-5122")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void SystemUser_Deactivation_UITestMethod04()
        {
            var currentDateTime = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User to deactivate

            _systemUsername = "DeactivationUser_" + currentDateTime;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "DeactivationUser", currentDateTime, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Job Role Type

            var _jobRoleTypeId = commonMethodsDB.CreateJobRoleType(_teamId, "Default Consultant", new DateTime(2020, 1, 1), 1);

            #endregion

            #region Update System User Job Role Type

            dbHelper.systemUser.UpdateJobRoleType(_systemUserId, _jobRoleTypeId);

            #endregion

            #region Recurrent Pattern

            var _recurrencePatternId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").FirstOrDefault();

            #endregion

            #region User Work Schedule

            var uwsStartDate = new DateTime(2023, 1, 1);
            DateTime? uwsEndDate = new DateTime(2023, 4, 1);
            var uwsStartTime = new TimeSpan(0, 5, 0);
            var uwsEndTime = new TimeSpan(23, 55, 0);
            var userWorkScheduleId = commonMethodsDB.CreateUserWorkSchedule("Default", _systemUserId, _teamId, _recurrencePatternId, uwsStartDate, uwsEndDate, uwsStartTime, uwsEndTime);

            #endregion

            #region Provider (Carer)

            var providerName = "Security Provider " + currentDateTime;
            var _providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 7);

            #endregion

            #region Community and Clinic Team

            var title = "Security Clinic Team " + currentDateTime;
            var communityAndClinicTeamId = commonMethodsDB.CreateCommunityAndClinicTeam(_teamId, _providerId, _teamId, title, "Jira test id CDV6-5122");

            #endregion

            #region Diary View Setup

            var dvsStartDate = new DateTime(2023, 1, 1);
            DateTime? dvsEndDate = null;
            var dvsStartTime = new TimeSpan(0, 5, 0);
            var dvsEndTime = new TimeSpan(23, 55, 0);
            var communityClinicDiaryViewSetupId = dbHelper.communityClinicDiaryViewSetup.CreateCommunityClinicDiaryViewSetup(
                _teamId, communityAndClinicTeamId, "Home Visit DVS", dvsStartDate, dvsEndDate, dvsStartTime, dvsEndTime, 500, 100, 100);

            #endregion

            #region Community Clinic Linked Professional

            var lpStartDate = new DateTime(2023, 1, 1);
            DateTime? lpEndDate = new DateTime(2023, 3, 1);
            var LPStartTime = new TimeSpan(0, 5, 0);
            var LPEndTime = new TimeSpan(23, 55, 0);
            dbHelper.communityClinicLinkedProfessional.CreateCommunityClinicLinkedProfessional(_teamId, communityClinicDiaryViewSetupId, _systemUserId, lpStartDate, lpEndDate, LPStartTime, LPEndTime, _recurrencePatternId, "new record");

            #endregion


            #region Step 9 to 16

            loginPage
                .GoToLoginPage()
                .Login("SecurityUser2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_systemUsername)
                .ClickSearchButton()
                .OpenRecord(_systemUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ClickInactiveStatus_YesOption();

            systemUserRecordPage
                .ClickSaveAndCloseButton();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-2341

        [TestProperty("JiraIssueID", "CDV6-25754")]
        [Description("Step(s) 1 to 2 for the test CDV6-23357")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void SystemUser_AlternativeStaffNumber_UITestMethod01()
        {
            #region System User

            _systemUsername = "ASN_" + _currentDateString;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "ASN", _currentDateString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Step 1 & 2

            loginPage
                .GoToLoginPage()
                .Login("SecurityUser2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_systemUsername)
                .ClickSearchButton()
                .OpenRecord(_systemUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateAlternativeStaffNumber("")
                .InsertAlternativeStaffNumber("9876567890")
                .ClickSaveAndCloseButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .OpenRecord(_systemUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateAlternativeStaffNumber("9876567890");

            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-25755")]
        [Description("Step(s) 13 to 14 for the test CDV6-23357")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void SystemUser_AlternativeStaffNumber_UITestMethod02()
        {

            #region Step 13

            loginPage
                .GoToLoginPage()
                .Login("SecurityUser2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("System Users")
                .SelectFilter("1", "Alternative Staff Number");

            #endregion

            #region Step 14

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Case Involvements")
                .SelectFilter("1", "Social Worker Change Reason");

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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace Phoenix.UITests.Settings.ViewsAndDashboards
{
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("Files\\Automation Testing User Dashboard.Zip")]
    [TestClass]
    public class UserDashboards_UITestCases : FunctionalTest
    {
        private string _environmentName;
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private string _systemUsername;

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
                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("Dashboard BU");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("Dashboard Team", null, _businessUnitId, "907678", "DashboardTeam@careworkstempmail.com", "Dashboard Team", "");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region Default System User

                _systemUsername = "UserDashboardUser1";
                var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "UserDashboard", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-4617

        [TestProperty("JiraIssueID", "CDV6-24928")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4617 - " +
            "Login in the web app - Navigate to Settings; Views & Dashboards; Summary Dashboards - Open a Dashboard record - Go to the details page - " +
            "Set AutoRefresh to No - Tap on the save button - Validate that the information is set in the database")]
        [TestCategory("UITest")]
        [TestMethod]
        public void UserDashboards_UITestMethod01()
        {
            Guid UserDahsboardID = new Guid("e55a5a65-8608-eb11-a2cd-005056926fe4");
            dbHelper.userDashboard.UpdateUserDashboard(UserDahsboardID, true, 120);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToUserDashboardsSection();

            userDashboardsPage
                .WaitForUserDashboardsPageToLoad()
                .SearchUserDashboardRecord("Automation Testing User Dashboard")
                .OpenUserDashboardRecord(UserDahsboardID.ToString());

            userDashboardRecordPage
                .WaitForUserDashboardRecordPageToLoad()
                .TapDetailsTab()
                .TapAutoRefreshNoRadioButton()
                .TapSaveButton();

            var fields = dbHelper.userDashboard.GetUserDashboardByID(UserDahsboardID, "autorefresh", "refreshtime");
            Assert.AreEqual(false, fields["autorefresh"]);
            Assert.AreEqual(false, fields.ContainsKey("refreshtime"));
        }

        [TestProperty("JiraIssueID", "CDV6-24929")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4617 - " +
            "Login in the web app - Navigate to Settings; Views & Dashboards; Summary Dashboards - Open a Dashboard record - Go to the details page - " +
            "Set AutoRefresh to Yes - Set a Refresh Time greater than 20 seconds - Tap on the save button - Validate that the information is set in the database")]
        [TestCategory("UITest")]
        [TestMethod]
        public void UserDashboards_UITestMethod02()
        {
            Guid UserDahsboardID = new Guid("e55a5a65-8608-eb11-a2cd-005056926fe4");
            dbHelper.userDashboard.UpdateUserDashboard(UserDahsboardID, false, null);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToUserDashboardsSection();

            userDashboardsPage
                .WaitForUserDashboardsPageToLoad()
                .SearchUserDashboardRecord("Automation Testing User Dashboard")
                .OpenUserDashboardRecord(UserDahsboardID.ToString());

            userDashboardRecordPage
                .WaitForUserDashboardRecordPageToLoad()
                .TapDetailsTab()
                .TapAutoRefreshYesRadioButton()
                .InsertRefreshTime("20")
                .TapSaveButton();

            var fields = dbHelper.userDashboard.GetUserDashboardByID(UserDahsboardID, "autorefresh", "refreshtime");
            Assert.AreEqual(true, fields["autorefresh"]);
            Assert.AreEqual(20, fields["refreshtime"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24930")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4617 - " +
            "'Automation Testing User Dashboard' has AutoRefresh option set to Yes and Refresh Time set to 20 seconds - " +
            "Login in the web app - Navigate to the Home Page - Select the Dashboard 'Automation Testing User Dashboard' - " +
            "Create a new Task record for a person - wait for 20 seconds - Validate that the record is automatically displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void UserDashboards_UITestMethod03()
        {
            Guid careDirectorTeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");
            Guid personID = new Guid("483ba013-dc07-eb11-a2cd-005056926fe4"); //Julianna Strauss
            Guid UserDahsboardID = new Guid("e55a5a65-8608-eb11-a2cd-005056926fe4");

            dbHelper.userDashboard.UpdateUserDashboard(UserDahsboardID, true, 20);

            foreach (Guid taskid in dbHelper.task.GetTaskByPersonID(personID))
                dbHelper.task.DeleteTask(taskid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!");

            homePage
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDashboardsSection();

            dashboardsPage
                .WaitForDashboardPageToLoad()
                .SelectDashboard("Automation Testing User Dashboard")
                .WaitForDashboardWidgetToLoad();

            Guid taskID = dbHelper.task.CreatePersonTask(personID, "Julianna Strauss", "Task 001", "notes....", careDirectorTeamID);

            System.Threading.Thread.Sleep(25000);

            dashboardsPage
                .WaitForDashboardWidgetToLoad()
                .ValidateAutomationTestingSummaryDashboardRecordPresent(taskID.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-24931")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4617 - " +
            "'Automation Testing User Dashboard' has AutoRefresh option set to No - " +
            "Login in the web app - Navigate to the Home Page - Select the Dashboard 'Automation Testing User Dashboard' - " +
            "Create a new Task record for a person - wait for 20 seconds - Validate that the record is not automatically displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void UserDashboards_UITestMethod04()
        {
            Guid careDirectorTeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");
            Guid personID = new Guid("483ba013-dc07-eb11-a2cd-005056926fe4"); //Julianna Strauss
            Guid SummaryDahsboardID = new Guid("e55a5a65-8608-eb11-a2cd-005056926fe4");

            dbHelper.userDashboard.UpdateUserDashboard(SummaryDahsboardID, false, null);

            foreach (Guid taskid in dbHelper.task.GetTaskByPersonID(personID))
                dbHelper.task.DeleteTask(taskid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!");

            homePage
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDashboardsSection();

            dashboardsPage
                .WaitForDashboardPageToLoad()
                .SelectDashboard("Automation Testing User Dashboard")
                .WaitForDashboardWidgetToLoad();

            Guid taskID = dbHelper.task.CreatePersonTask(personID, "Julianna Strauss", "Task 001", "notes....", careDirectorTeamID);

            System.Threading.Thread.Sleep(25000);

            dashboardsPage
                .WaitForDashboardWidgetToLoad()
                .ValidateAutomationTestingSummaryDashboardRecordNotPresent(taskID.ToString());
        }


        #endregion


        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}

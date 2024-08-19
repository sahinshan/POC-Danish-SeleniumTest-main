using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace Phoenix.UITests.Settings.ViewsAndDashboards
{
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("Files\\Automation Testing System Dashboard.Zip")]
    [DeploymentItem("Files\\Automation Testing System Dashboard CDV6-10189.Zip")]
    [TestClass]
    public class SystemDashboards_UITestCases : FunctionalTest
    {
        private string _environmentName;
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private string _systemUsername;
        private Guid _personID;
        private Guid _systemDahsboardId_1;
        private Guid _systemDahsboardId_2;

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

                _systemUsername = "SystemDashboardUser1";
                commonMethodsDB.CreateSystemUserRecord(_systemUsername, "SystemDashboard", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

                #region Import System Dashboard

                _systemDahsboardId_1 = commonMethodsDB.ImportSystemDashboardIfNeeded("Automation Testing System Dashboard", "Automation Testing System Dashboard.Zip");
                _systemDahsboardId_2 = commonMethodsDB.ImportSystemDashboardIfNeeded("Automation Testing System Dashboard CDV6-10189", "Automation Testing System Dashboard CDV6-10189.Zip");

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

        [TestProperty("JiraIssueID", "CDV6-24938")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4617 - " +
            "Login in the web app - Navigate to Settings; Views & Dashboards; System Dashboards - Open a Dashboard record - Go to the details page - " +
            "Set AutoRefresh to No - Tap on the save button - Validate that the information is set in the database")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod]
        public void SystemDashboards_UITestMethod01()
        {
            dbHelper.systemDashboard.UpdateSystemDashboard(_systemDahsboardId_1, true, 120);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemDashboardsSection();

            systemDashboardsPage
                .WaitForSystemDashboardsPageToLoad()
                .SearchSystemDashboardRecord("Automation Testing System Dashboard")
                .OpenSystemDashboardRecord(_systemDahsboardId_1.ToString());

            systemDashboardRecordPage
                .WaitForSystemDashboardRecordPageToLoad()
                .TapDetailsTab()
                .TapAutoRefreshNoRadioButton()
                .TapSaveButton();

            var fields = dbHelper.systemDashboard.GetSystemDashboardByID(_systemDahsboardId_1, "autorefresh", "refreshtime");
            Assert.AreEqual(false, fields["autorefresh"]);
            Assert.AreEqual(false, fields.ContainsKey("refreshtime"));

        }

        [TestProperty("JiraIssueID", "CDV6-24939")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4617 - " +
            "Login in the web app - Navigate to Settings; Views & Dashboards; System Dashboards - Open a Dashboard record - Go to the details page - " +
            "Set AutoRefresh to Yes - Set a Refresh Time greater than 20 seconds - Tap on the save button - Validate that the information is set in the database")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod]
        public void SystemDashboards_UITestMethod02()
        {
            dbHelper.systemDashboard.UpdateSystemDashboard(_systemDahsboardId_1, true, 120);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemDashboardsSection();

            systemDashboardsPage
                .WaitForSystemDashboardsPageToLoad()
                .SearchSystemDashboardRecord("Automation Testing System Dashboard")
                .OpenSystemDashboardRecord(_systemDahsboardId_1.ToString());

            systemDashboardRecordPage
                .WaitForSystemDashboardRecordPageToLoad()
                .TapDetailsTab()
                .TapAutoRefreshYesRadioButton()
                .InsertRefreshTime("20")
                .TapSaveButton();

            var fields = dbHelper.systemDashboard.GetSystemDashboardByID(_systemDahsboardId_1, "autorefresh", "refreshtime");
            Assert.AreEqual(true, fields["autorefresh"]);
            Assert.AreEqual(20, fields["refreshtime"]);

        }

        [TestProperty("JiraIssueID", "CDV6-24940")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4617 - " +
            "'Automation Testing System Dashboard' has AutoRefresh option set to Yes and Refresh Time set to 20 seconds - " +
            "Login in the web app - Navigate to the Home Page - Select the Dashboard 'Automation Testing System Dashboard' - " +
            "Create a new Task record for a person - wait for 20 seconds - Validate that the record is automatically displayed")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod]
        public void SystemDashboards_UITestMethod03()
        {
            dbHelper.systemDashboard.UpdateSystemDashboard(_systemDahsboardId_1, true, 20);

            #region Create Person / Remove Person Task

            _personID = commonMethodsDB.CreatePersonRecord("Julianna", "Strauss", _ethnicityId, _teamId, new DateTime(2000, 1, 2));

            foreach (Guid taskid in dbHelper.task.GetTaskByPersonID(_personID))
                dbHelper.task.DeleteTask(taskid);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            homePage
                .WaitFormHomePageToLoad()
                .SelectDahsboard("Automation Testing System Dashboard");

            homePage_DashboardsArea
                .WaitForHomePage_DashboardsAreaToLoad();

            Guid taskID = dbHelper.task.CreatePersonTask(_personID, "Julianna Strauss", "Task 001", "notes....", _teamId);
            System.Threading.Thread.Sleep(25000);

            homePage_DashboardsArea
                .WaitForHomePage_AutomationTestingSystemDashboardToLoad()
                .ValidateAutomationTestingSystemDashboardRecordPresent(taskID.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-24941")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4617 - " +
            "'Automation Testing System Dashboard' has AutoRefresh option set to No - " +
            "Login in the web app - Navigate to the Home Page - Select the Dashboard 'Automation Testing System Dashboard' - " +
            "Create a new Task record for a person - wait for 20 seconds - Validate that the record is not automatically displayed")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod]
        public void SystemDashboards_UITestMethod04()
        {
            dbHelper.systemDashboard.UpdateSystemDashboard(_systemDahsboardId_1, false, null);

            #region Create Person / Remove Person Task

            _personID = commonMethodsDB.CreatePersonRecord("Julianna", "Strauss", _ethnicityId, _teamId, new DateTime(2000, 1, 2));

            foreach (Guid taskid in dbHelper.task.GetTaskByPersonID(_personID))
                dbHelper.task.DeleteTask(taskid);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            homePage
                .WaitFormHomePageToLoad()
                .SelectDahsboard("Automation Testing System Dashboard");

            homePage_DashboardsArea
                .WaitForHomePage_DashboardsAreaToLoad();

            Guid taskID = dbHelper.task.CreatePersonTask(_personID, "Julianna Strauss", "Task 001", "notes....", _teamId);
            System.Threading.Thread.Sleep(25000);

            homePage_DashboardsArea
                .WaitForHomePage_AutomationTestingSystemDashboardToLoad()
                .ValidateAutomationTestingSystemDashboardRecordNotPresent(taskID.ToString());

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-10189

        [TestProperty("JiraIssueID", "CDV6-24942")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4617 - " +
            "Login in the web app - Navigate to Settings; Views & Dashboards; System Dashboards - Open a Dashboard record - Wait for the Dashbaord tab to load - " +
            "Edit one Grid View widget - Change the Header Colour - Save and Close the widget popup - Click on the save dashboard button - Validate that the widget header color is updated")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod]
        public void SystemDashboardIssueHeaderColour_UITestMethod01()
        {
            dbHelper.systemDashboard.UpdateSystemDashboard(_systemDahsboardId_2, "<Layout  >    <Widgets>      <widget>        <Title>Person - People I Created</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>GridView</Type>          <DataViewId>8df84b2d-b169-e411-bf00-005056c00008</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>30f84b2d-b169-e411-bf00-005056c00008</BusinessObjectId>          <BusinessObjectName>person</BusinessObjectName>          <HeaderColour>bg-DeepOrange</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>Person - My Recent Records</Title>        <X>3</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>ListView</Type>          <DataViewId>0cd5b72a-c469-e411-bf00-005056c00008</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>30f84b2d-b169-e411-bf00-005056c00008</BusinessObjectId>          <BusinessObjectName>person</BusinessObjectName>          <HeaderColour>bg-Yellow</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>    </Widgets>  </Layout>");

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemDashboardsSection();

            systemDashboardsPage
                .WaitForSystemDashboardsPageToLoad()
                .SearchSystemDashboardRecord("Automation Testing System Dashboard CDV6-10189")
                .OpenSystemDashboardRecord(_systemDahsboardId_2.ToString());

            systemDashboardRecordPage
                .WaitForSystemDashboardRecordPageToLoad()
                .WaitForDashboardTabToLoad()

                .ClickEditButton_DashboardTab()
                .ClickWidgetSettingsButton_DashboardTab(1);

            widgetSettingsPopup
                .WaitForWidgetSettingsPopupToLoad()
                .SelectHeaderColor("Pink")
                .TapSaveSettingsButton();

            systemDashboardRecordPage
                .WaitForSystemDashboardRecordPageToLoad()
                .WaitForDashboardTabToLoad()
                .ClickSaveButton_DashboardTab()
                .WaitForDashboardTabToLoad()
                .ValidateWidgetHeaderBackgroudColor_DashboardTab(1, "Pink");

        }

        [TestProperty("JiraIssueID", "CDV6-24943")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4617 - " +
            "Login in the web app - Navigate to Settings; Views & Dashboards; System Dashboards - Open a Dashboard record - Wait for the Dashbaord tab to load - " +
            "Edit one List View widget - Change the Header Colour - Save and Close the widget popup - Click on the save dashboard button - Validate that the widget header color is updated")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod]
        public void SystemDashboardIssueHeaderColour_UITestMethod02()
        {
            dbHelper.systemDashboard.UpdateSystemDashboard(_systemDahsboardId_2, "<Layout  >    <Widgets>      <widget>        <Title>Person - People I Created</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>GridView</Type>          <DataViewId>8df84b2d-b169-e411-bf00-005056c00008</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>30f84b2d-b169-e411-bf00-005056c00008</BusinessObjectId>          <BusinessObjectName>person</BusinessObjectName>          <HeaderColour>bg-DeepOrange</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>Person - My Recent Records</Title>        <X>3</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>ListView</Type>          <DataViewId>0cd5b72a-c469-e411-bf00-005056c00008</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>30f84b2d-b169-e411-bf00-005056c00008</BusinessObjectId>          <BusinessObjectName>person</BusinessObjectName>          <HeaderColour>bg-Yellow</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>    </Widgets>  </Layout>");

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemDashboardsSection();

            systemDashboardsPage
                .WaitForSystemDashboardsPageToLoad()
                .SearchSystemDashboardRecord("Automation Testing System Dashboard CDV6-10189")
                .OpenSystemDashboardRecord(_systemDahsboardId_2.ToString());

            systemDashboardRecordPage
                .WaitForSystemDashboardRecordPageToLoad()
                .WaitForDashboardTabToLoad()

                .ClickEditButton_DashboardTab()
                .ClickWidgetSettingsButton_DashboardTab(2);

            widgetSettingsPopup
                .WaitForWidgetSettingsPopupToLoad()
                .SelectHeaderColor("Green")
                .TapSaveSettingsButton();

            systemDashboardRecordPage
                .WaitForSystemDashboardRecordPageToLoad()
                .WaitForDashboardTabToLoad()
                .ClickSaveButton_DashboardTab()
                .WaitForDashboardTabToLoad()
                .ValidateWidgetHeaderBackgroudColor_DashboardTab(2, "Green");

        }

        #endregion


        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace Phoenix.UITests.Settings.ViewsAndDashboards
{
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("Files\\Automation Testing Summary Dashboard.Zip")]
    [TestClass]
    public class SummaryDashboards_UITestCases : FunctionalTest
    {
        private string _environmentName;
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private string _systemUsername;
        private Guid _personID;
        private Guid _summaryDahsboardId;

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
                var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "SystemDashboard", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

                #region Import System Dashboard

                _summaryDahsboardId = commonMethodsDB.ImportSummaryDashboardIfNeeded("Automation Testing Summary Dashboard", "Automation Testing Summary Dashboard.Zip");
                //dbHelper.userDashboard.UpdateOwningUserId(_summaryDahsboardId, _systemUserId);

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

        [TestProperty("JiraIssueID", "CDV6-24932")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4617 - " +
            "Login in the web app - Navigate to Settings; Views & Dashboards; Summary Dashboards - Open a Dashboard record - Go to the details page - " +
            "Set AutoRefresh to No - Tap on the save button - Validate that the information is set in the database")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod]
        public void SummaryDashboards_UITestMethod01()
        {
            dbHelper.businessObjectDashboard.UpdateBusinessObjectDashboard(_summaryDahsboardId, true, 120);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSummaryDashboardsSection();

            summaryDashboardsPage
                .WaitForSummaryDashboardsPageToLoad()
                .SearchSummaryDashboardRecord("Automation Testing Summary Dashboard")
                .OpenSummaryDashboardRecord(_summaryDahsboardId.ToString());

            summaryDashboardRecordPage
                .WaitForSummaryDashboardRecordPageToLoad()
                .TapDetailsTab()
                .TapAutoRefreshNoRadioButton()
                .TapSaveButton();

            var fields = dbHelper.businessObjectDashboard.GetBusinessObjectDashboardByID(_summaryDahsboardId, "autorefresh", "refreshtime");
            Assert.AreEqual(false, fields["autorefresh"]);
            Assert.AreEqual(false, fields.ContainsKey("refreshtime"));

        }

        [TestProperty("JiraIssueID", "CDV6-24933")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4617 - " +
            "Login in the web app - Navigate to Settings; Views & Dashboards; Summary Dashboards - Open a Dashboard record - Go to the details page - " +
            "Set AutoRefresh to Yes - Set a Refresh Time greater than 20 seconds - Tap on the save button - Validate that the information is set in the database")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod]
        public void SummaryDashboards_UITestMethod02()
        {
            dbHelper.businessObjectDashboard.UpdateBusinessObjectDashboard(_summaryDahsboardId, false, null);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSummaryDashboardsSection();

            summaryDashboardsPage
                .WaitForSummaryDashboardsPageToLoad()
                .SearchSummaryDashboardRecord("Automation Testing Summary Dashboard")
                .OpenSummaryDashboardRecord(_summaryDahsboardId.ToString());

            summaryDashboardRecordPage
                .WaitForSummaryDashboardRecordPageToLoad()
                .TapDetailsTab()
                .TapAutoRefreshYesRadioButton()
                .InsertRefreshTime("20")
                .TapSaveButton();

            var fields = dbHelper.businessObjectDashboard.GetBusinessObjectDashboardByID(_summaryDahsboardId, "autorefresh", "refreshtime");
            Assert.AreEqual(true, fields["autorefresh"]);
            Assert.AreEqual(20, fields["refreshtime"]);

        }

        [TestProperty("JiraIssueID", "CDV6-24934")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4617 - " +
            "'Automation Testing Summary Dashboard' has AutoRefresh option set to Yes and Refresh Time set to 20 seconds - " +
            "Login in the web app - Navigate to the Home Page - Select the Dashboard 'Automation Testing Summary Dashboard' - " +
            "Create a new Task record for a person - wait for 20 seconds - Validate that the record is automatically displayed")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod]
        public void SummaryDashboards_UITestMethod03()
        {
            dbHelper.businessObjectDashboard.UpdateBusinessObjectDashboard(_summaryDahsboardId, true, 20);

            _personID = commonMethodsDB.CreatePersonRecord("Julianna", "Strauss", _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            foreach (Guid taskid in dbHelper.task.GetTaskByPersonID(_personID))
                dbHelper.task.DeleteTask(taskid);


            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            homePage
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapSummaryTab()
                .WaitForSummaryTabToLoad()
                .SelectDashboard("Automation Testing Summary Dashboard");

            Guid taskID = dbHelper.task.CreatePersonTask(_personID, "Julianna Strauss", "Task 001", "notes....", _teamId);

            System.Threading.Thread.Sleep(25000);

            personRecordPage_SummaryArea
                .WaitForPersonRecordPage_SummaryAreaToLoad()
                .ValidateAutomationTestingSummaryDashboardRecordPresent(taskID.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-24935")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4617 - " +
            "'Automation Testing Summary Dashboard' has AutoRefresh option set to No - " +
            "Login in the web app - Navigate to the Home Page - Select the Dashboard 'Automation Testing Summary Dashboard' - " +
            "Create a new Task record for a person - wait for 20 seconds - Validate that the record is not automatically displayed")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod]
        public void SummaryDashboards_UITestMethod04()
        {
            dbHelper.businessObjectDashboard.UpdateBusinessObjectDashboard(_summaryDahsboardId, false, null);

            _personID = commonMethodsDB.CreatePersonRecord("Julianna", "Strauss", _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            foreach (Guid taskid in dbHelper.task.GetTaskByPersonID(_personID))
                dbHelper.task.DeleteTask(taskid);


            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            homePage
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapSummaryTab()
                .WaitForSummaryTabToLoad()
                .SelectDashboard("Automation Testing Summary Dashboard");

            Guid taskID = dbHelper.task.CreatePersonTask(_personID, "Julianna Strauss", "Task 001", "notes....", _teamId);

            System.Threading.Thread.Sleep(25000);

            personRecordPage_SummaryArea
                .WaitForPersonRecordPage_SummaryAreaToLoad()
                .ValidateAutomationTestingSummaryDashboardRecordNotPresent(taskID.ToString());

        }


        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-11611

        [TestProperty("JiraIssueID", "CDV6-24936")]
        [Description("Login in Caredirector - Navigate to System Dashboards - Open an a Dashboard with an empty widget - " +
            "Open the Dashboard tab to load - Click on the Toggle Edit Button - Click on the widget edit button - Wait for the Widget Settings popup to load - " +
            "Set Widget Type to 'Current Record' - Set all the mandatory fields - Click on the Save Settings button - Click on the dashboard save button - " +
            "Validate that the widget is correctly saved")]
        [TestCategory("UITest")]
        [TestMethod]
        public void SummaryDashboards_CurrentRecordOption_UITestMethod01()
        {
            Guid SummaryDahsboardID = new Guid("2ce21415-90ef-eb11-a325-005056926fe4"); //CDV6-11611-Edit-Dashboard

            //reset the dashboard layout
            dbHelper.businessObjectDashboard.UpdateBusinessObjectDashboard(SummaryDahsboardID, "<Layout  >    <Widgets>      <widget>        <Title>New widget</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>8</Height>      </widget>    </Widgets>  </Layout>");


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSummaryDashboardsSection();

            summaryDashboardsPage
                .WaitForSummaryDashboardsPageToLoad()
                .SearchSummaryDashboardRecord("CDV6-11611-Edit-Dashboard")
                .OpenSummaryDashboardRecord(SummaryDahsboardID.ToString());

            summaryDashboardRecordPage
                .WaitForDashboardTabToLoad()
                .ClickToogleEditWidgetsButton()
                .ClickEditWidgetButton(1);

            widgetSettingsPopup
                .WaitForWidgetSettingsPopupToLoad()
                .SelectWidgetType("Current Record")
                .ClickSelectViewPicklist()
                .SelectView("Related Records")
                .SelectHeaderColor("Yellow")
                .InsertWidgetTitle("CDV6-11611")
                .TapSaveSettingsButton();

            summaryDashboardRecordPage
                .WaitForDashboardTabToLoad()
                .ClickSaveWidgetButton();


            var fields = dbHelper.businessObjectDashboard.GetBusinessObjectDashboardByID(SummaryDahsboardID, "layoutxml");
            Assert.AreEqual("<Layout  >\r\n  <Widgets>\r\n    <widget>\r\n      <Title>CDV6-11611</Title>\r\n      <X>0</X>\r\n      <Y>0</Y>\r\n      <Width>3</Width>\r\n      <Height>8</Height>\r\n      <Settings>\r\n        <Type>CurrentRecord</Type>\r\n        <DataViewId>424a4433-b169-e411-bf00-005056c00008</DataViewId>\r\n        <DataViewType>system</DataViewType>\r\n        <HeaderColour>bg-Yellow</HeaderColour>\r\n        <HideHeader>false</HideHeader>\r\n        <FullSize>false</FullSize>\r\n      </Settings>\r\n    </widget>\r\n  </Widgets>\r\n</Layout>", fields["layoutxml"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24937")]
        [Description("Open a Person record - Navigate to the Summary tab - select the dashboard 'CDV6-11611 - Test Automation Person Dashboard' - " +
            "Wait for the dashboard to load - Validate that the person information is correctly displayed in the dashboard widget")]
        [TestCategory("UITest")]
        [TestMethod]
        public void SummaryDashboards_CurrentRecordOption_UITestMethod02()
        {
            Guid personID = new Guid("483ba013-dc07-eb11-a2cd-005056926fe4"); //Julianna Strauss

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!");

            homePage
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID("505949", personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapSummaryTab()
                .WaitForSummaryTabToLoad()
                .SelectDashboard("CDV6-11611 - Test Automation Person Dashboard");


            personRecordPage_SummaryArea
                .WaitForPersonRecordPage_SummaryAreaToLoad()
                .ValidateRepresentsAlertHazardText("Represents Alert/Hazard?: No")
                .ValidateIdText("Id: 505949")
                .ValidateFirstNameText("First Name: Julianna")
                .ValidateLastNameText("Last Name: Strauss")
                .ValidateStatedGenderText("Stated Gender: Female")
                .ValidateDOBText("DOB: 01/10/2000")
                .ValidatePostCodeText("Postcode: CR0 3RL")
                .ValidateCreatedByText("Created By: José Brazeta")
                .ValidateCreatedOnText("Created On: 06/10/2020 14:58:57");
        }

        #endregion

        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}

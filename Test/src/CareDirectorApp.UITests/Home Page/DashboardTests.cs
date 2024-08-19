using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;

namespace CareDirectorApp.UITests.Dashboard
{
    /// <summary>
    /// This class contains all test methods for Dashboards
    /// </summary>
    [TestFixture]
    [Category("Mobile_TabletMode_Online")]
    public class DashboardTests : TestBase
    {
        static UIHelper uIHelper;

        #region properties

        string LayoutXml_NoWidgets = "";
        string LayoutXml_OneWidgetNotConfigured = "<Layout  >    <Widgets>      <widget>        <Title>New widget</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_OneWidget_GridView = "<Layout  >    <Widgets>      <widget>        <Title>Task - My Active Tasks</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>GridView</Type>          <DataViewId>bfdef053-6cc5-e811-9c01-1866da1e4209</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-Indigo</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_OneWidgetListView = "<Layout  >    <Widgets>      <widget>        <Title>Task - My Active Tasks</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>ListView</Type>          <DataViewId>bfdef053-6cc5-e811-9c01-1866da1e4209</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-LightGreen</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_TwoWidgets_GridView_ListView = "<Layout  >    <Widgets>      <widget>        <Title>Task - My Active Tasks</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>GridView</Type>          <DataViewId>bfdef053-6cc5-e811-9c01-1866da1e4209</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-LightGreen</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>Case Note (For Case) - Active Records</Title>        <X>3</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>ListView</Type>          <DataViewId>ae1c53f3-3010-e911-80dc-0050560502cc</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>b41b53f3-3010-e911-80dc-0050560502cc</BusinessObjectId>          <BusinessObjectName>casecasenote</BusinessObjectName>          <HeaderColour>bg-Green</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_OneWidget_SharedUserView = "  <Layout  >    <Widgets>      <widget>        <Title>Task - Tasks created in the last 30 days</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>GridView</Type>          <DataViewId>65ffa115-4890-ea11-a2cd-005056926fe4</DataViewId>          <DataViewType>shared</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-LightBlue</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_AllWidgets = "  <Layout  >    <Widgets>      <widget>        <Title>New widget</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>IFrame</Type>          <Url>www.google.com</Url>          <HeaderColour>bg-Pink</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>Task - My Active Tasks</Title>        <X>3</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>GridView</Type>          <DataViewId>bfdef053-6cc5-e811-9c01-1866da1e4209</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-LightBlue</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>New widget</Title>        <X>6</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>Chart</Type>          <BusinessObjectId>30f84b2d-b169-e411-bf00-005056c00008</BusinessObjectId>          <BusinessObjectName>person</BusinessObjectName>          <ChartId>5b540885-d816-ea11-a2c8-005056926fe4</ChartId>          <ChartGroup>System</ChartGroup>          <HeaderColour>bg-Yellow</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>Case Note (For Case) - Active Records</Title>        <X>0</X>        <Y>3</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>ListView</Type>          <DataViewId>ae1c53f3-3010-e911-80dc-0050560502cc</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>b41b53f3-3010-e911-80dc-0050560502cc</BusinessObjectId>          <BusinessObjectName>casecasenote</BusinessObjectName>          <HeaderColour>bg-LightGreen</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>New widget</Title>        <X>3</X>        <Y>3</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>WidgetCatalogue</Type>          <HTMLWebResource>Html_ClinicAvailableSlotsSearch.html</HTMLWebResource>          <HeaderColour>bg-Red</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>New widget</Title>        <X>6</X>        <Y>3</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>Report</Type>          <HeaderColour>bg-Purple</HeaderColour>          <HideHeader>false</HideHeader>          <ReportId>dc3d3d9f-1765-ea11-a2cb-005056926fe4</ReportId>        </Settings>      </widget>    </Widgets>  </Layout>";


        #endregion

        [TestFixtureSetUp]
        public void ClassInitializationMethod()
        {
            if (this.IgnoreTestFixtureSetUp)
                return;

            //authenticate a user against the platform services
            this.PlatformServicesHelper = new PlatformServicesHelper("SecurityTestUserAdmin", "Passw0rd_!");

            //start the APP
            uIHelper = new UIHelper();
            this._app = uIHelper.StartApp(this._apkFileLocation, this._deviceSerial, AppDataMode.DoNotClear);

            //set the default URL
            this.SetDefaultEndpointURL();

            //Login with test user account
            var changeUserButtonVisible = loginPage.WaitForBasicLoginPageToLoad().GetChangeUserButtonVisibility();
            if (changeUserButtonVisible)
            {
                //Login with test user account
                loginPage
                    .WaitForBasicLoginPageToLoad()
                    .TapChangeUserButton();

                warningPopup
                    .WaitForWarningPopupToLoad()
                    .TapOnYesButton();

                loginPage
                   .WaitForLoginPageToLoad()
                   .InsertUserName("Mobile_Test_User_1")
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

                //if the offline mode warning is displayed, then close it
                warningPopup.TapNoButtonIfPopupIsOpen();

                //wait for the homepage to load
                homePage
                    .WaitForHomePageToLoad();
            }
            else
            {
                //Login with test user account
                loginPage
                    .WaitForBasicLoginPageToLoad()
                    .InsertUserName("Mobile_Test_User_1")
                    .InsertPassword("Passw0rd_!")
                    .TapLoginButton();

                //Set the PIN Code
                pinPage
                    .WaitForPinPageToLoad()
                    .TapButton1()
                    .TapButton2()
                    .TapButton3()
                    .TapButton4()
                    .TapButtonOK()
                    .WaitForConfirmationPinPageToLoad()
                    .TapButton1()
                    .TapButton2()
                    .TapButton3()
                    .TapButton4()
                    .TapButtonOK();

                //wait for the homepage to load
                homePage
                    .WaitForHomePageToLoad();
            }
        }

        [SetUp] 
        public void TestInitializationMethod()
        {
            if (this.IgnoreSetUp)
                return;

            //if the error popup is open close it
            errorPopup
                .ClosePopupIfOpen();

            //if the warning popup is open close it
            warningPopup
                .CloseWarningPopupIfOpen();

            //navigate to the Settings page
            mainMenu
                .NavigateToSettingsPage();
        }


        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6617")]
        [Description("UI Test for Dashboards - 0001 - Remove all widgets and application components from the test dashboard - " +
            "sync the APP - validate that the test dashboard is not displayed")]
        public void Dashboard_TestMethod1()
        {
            Guid testDashboard = new Guid("564FCAD6-A58F-EA11-A2CD-005056926FE4"); //_A_Mobile_Dashboard_Test

            //remove all widgets for the dashboard _A_Mobile_Dashboard_Test
            this.PlatformServicesHelper.systemDashboard.UpdateLayoutXML(testDashboard, LayoutXml_NoWidgets);

            //remove all application components for the dashboard _A_Mobile_Dashboard_Test
            foreach (Guid applicationComponentID in this.PlatformServicesHelper.applicationComponent.GetApplicationComponentByComponentID(testDashboard))
                this.PlatformServicesHelper.applicationComponent.DeleteApplicationComponent(applicationComponentID);

            //trigger a sync
            settingsPage
                .WaitForSettingsPageToLoad()
                .WaitForSyncProcessToFinish()
                .TapSyncNowButton()
                .WaitForSyncProcessToFinish();

            //Navigate to the home page
            mainMenu
                .NavigateToHomePage();

            //the _Mobile_Dashboard_Test dashboard should not be displayed 
            homePage
                .WaitForHomePageToLoad()
                .WaitForDashboardToLoad("_Default Dashboard")
                .WaitForDashboardWidgetToLoad("Task - My Team Active Tasks", "task", true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6618")]
        [Description("UI Test for Dashboards - 0002 - Remove all widgets and application components from the test dashboard - " +
            "Add a 'Caredirector App' application component - sync the APP - validate that the test dashboard is not displayed")]
        public void Dashboard_TestMethod2()
        {
            Guid testDashboard = new Guid("564FCAD6-A58F-EA11-A2CD-005056926FE4"); //_A_Mobile_Dashboard_Test

            //Set the widgets for the dashboard _A_Mobile_Dashboard_Test
            this.PlatformServicesHelper.systemDashboard.UpdateLayoutXML(testDashboard, LayoutXml_NoWidgets);

            //remove all application components for the dashboard _A_Mobile_Dashboard_Test
            foreach (Guid applicationComponentID in this.PlatformServicesHelper.applicationComponent.GetApplicationComponentByComponentID(testDashboard))
                this.PlatformServicesHelper.applicationComponent.DeleteApplicationComponent(applicationComponentID);

            //add a "Caredirector App" application component
            Guid applicationID = this.PlatformServicesHelper.application.GetApplicationByDisplayName("CareDirector App")[0];
            this.PlatformServicesHelper.applicationComponent.CreateApplicationComponent(applicationID, testDashboard, "systemdashboard", "_A_Mobile_Dashboard_Test");

            //trigger a sync
            settingsPage
                .WaitForSettingsPageToLoad()
                .WaitForSyncProcessToFinish()
                .TapSyncNowButton()
                .WaitForSyncProcessToFinish();

            //Navigate to the home page
            mainMenu
                .NavigateToHomePage();

            //the _A_Mobile_Dashboard_Test dashboard should not be displayed 
            homePage
                .WaitForHomePageToLoad()
                .WaitForDashboardToLoad("_Default Dashboard")
                .WaitForDashboardWidgetToLoad("Task - My Team Active Tasks", "task", true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6619")]
        [Description("UI Test for Dashboards - 0003 - Remove all widgets and application components from the test dashboard - " +
            "Add a 'Caredirector App' application component - Add a dashboard widget (not configured) - sync the APP - validate that the test dashboard is not displayed")]
        public void Dashboard_TestMethod3()
        {
            Guid testDashboard = new Guid("564FCAD6-A58F-EA11-A2CD-005056926FE4"); //_A_Mobile_Dashboard_Test

            //Set the widgets for the dashboard _A_Mobile_Dashboard_Test
            this.PlatformServicesHelper.systemDashboard.UpdateLayoutXML(testDashboard, LayoutXml_OneWidgetNotConfigured);

            //remove all application components for the dashboard _A_Mobile_Dashboard_Test
            foreach (Guid applicationComponentID in this.PlatformServicesHelper.applicationComponent.GetApplicationComponentByComponentID(testDashboard))
                this.PlatformServicesHelper.applicationComponent.DeleteApplicationComponent(applicationComponentID);

            //add a "Caredirector App" application component
            Guid applicationID = this.PlatformServicesHelper.application.GetApplicationByDisplayName("CareDirector App")[0];
            this.PlatformServicesHelper.applicationComponent.CreateApplicationComponent(applicationID, testDashboard, "systemdashboard", "_A_Mobile_Dashboard_Test");

            //trigger a sync
            settingsPage
                .WaitForSettingsPageToLoad()
                .WaitForSyncProcessToFinish()
                .TapSyncNowButton()
                .WaitForSyncProcessToFinish();

            //Navigate to the home page
            mainMenu
                .NavigateToHomePage();

            //the _A_Mobile_Dashboard_Test dashboard should not be displayed 
            homePage
                .WaitForHomePageToLoad()
                .WaitForDashboardToLoad("_Default Dashboard")
                .WaitForDashboardWidgetToLoad("Task - My Team Active Tasks", "task", true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6620")]
        [Description("UI Test for Dashboards - 0004 - Remove all widgets and application components from the test dashboard - " +
            "Add a 'Caredirector App' application component - Add a dashboard widget (Grid View - My Active Tasks) - sync the APP - validate that the test dashboard is displayed")]
        public void Dashboard_TestMethod4()
        {
            Guid testDashboard = new Guid("564FCAD6-A58F-EA11-A2CD-005056926FE4"); //_A_Mobile_Dashboard_Test

            //Set the widgets for the dashboard _A_Mobile_Dashboard_Test
            this.PlatformServicesHelper.systemDashboard.UpdateLayoutXML(testDashboard, LayoutXml_OneWidget_GridView);

            //remove all application components for the dashboard _A_Mobile_Dashboard_Test
            foreach (Guid applicationComponentID in this.PlatformServicesHelper.applicationComponent.GetApplicationComponentByComponentID(testDashboard))
                this.PlatformServicesHelper.applicationComponent.DeleteApplicationComponent(applicationComponentID);

            //add a "Caredirector App" application component
            Guid applicationID = this.PlatformServicesHelper.application.GetApplicationByDisplayName("CareDirector App")[0];
            this.PlatformServicesHelper.applicationComponent.CreateApplicationComponent(applicationID, testDashboard, "systemdashboard", "_A_Mobile_Dashboard_Test");

            //trigger a sync
            settingsPage
                .WaitForSettingsPageToLoad()
                .WaitForSyncProcessToFinish()
                .TapSyncNowButton()
                .WaitForSyncProcessToFinish();

            //Navigate to the home page
            mainMenu
                .NavigateToHomePage();

            //the _A_Mobile_Dashboard_Test dashboard should not be displayed 
            homePage
                .WaitForHomePageToLoad()
                .WaitForDashboardToLoad("_A_Mobile_Dashboard_Test")
                .WaitForDashboardWidgetToLoad("Task - My Active Tasks", "task", true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6621")]
        [Description("UI Test for Dashboards - 0005 - Remove all widgets and application components from the test dashboard - " +
            "Add a 'Caredirector App' application component - Add a dashboard widget (List View - My Active Tasks) - sync the APP - validate that the test dashboard is displayed")]
        public void Dashboard_TestMethod5()
        {
            Guid testDashboard = new Guid("564FCAD6-A58F-EA11-A2CD-005056926FE4"); //_A_Mobile_Dashboard_Test

            //Set the widgets for the dashboard _A_Mobile_Dashboard_Test
            this.PlatformServicesHelper.systemDashboard.UpdateLayoutXML(testDashboard, LayoutXml_OneWidgetListView);

            //remove all application components for the dashboard _A_Mobile_Dashboard_Test
            foreach (Guid applicationComponentID in this.PlatformServicesHelper.applicationComponent.GetApplicationComponentByComponentID(testDashboard))
                this.PlatformServicesHelper.applicationComponent.DeleteApplicationComponent(applicationComponentID);

            //add a "Caredirector App" application component
            Guid applicationID = this.PlatformServicesHelper.application.GetApplicationByDisplayName("CareDirector App")[0];
            this.PlatformServicesHelper.applicationComponent.CreateApplicationComponent(applicationID, testDashboard, "systemdashboard", "_A_Mobile_Dashboard_Test");

            //trigger a sync
            settingsPage
                .WaitForSettingsPageToLoad()
                .WaitForSyncProcessToFinish()
                .TapSyncNowButton()
                .WaitForSyncProcessToFinish();

            //Navigate to the home page
            mainMenu
                .NavigateToHomePage();

            //the _A_Mobile_Dashboard_Test dashboard should be displayed 
            homePage
                .WaitForHomePageToLoad()
                .WaitForDashboardToLoad("_A_Mobile_Dashboard_Test")
                .WaitForDashboardWidgetToLoad("Task - My Active Tasks", "task", true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6622")]
        [Description("UI Test for Dashboards - 0006 - Remove all widgets and application components from the test dashboard - " +
            "Add a 'Caredirector App' application component - Add two dashboard widgets (List View - My Active Tasks and Grid View - ) - sync the APP - validate that the test dashboard is displayed")]
        public void Dashboard_TestMethod6()
        {
            Guid testDashboard = new Guid("564FCAD6-A58F-EA11-A2CD-005056926FE4"); //_A_Mobile_Dashboard_Test

            //Set the widgets for the dashboard _A_Mobile_Dashboard_Test
            this.PlatformServicesHelper.systemDashboard.UpdateLayoutXML(testDashboard, LayoutXml_TwoWidgets_GridView_ListView);

            //remove all application components for the dashboard _A_Mobile_Dashboard_Test
            foreach (Guid applicationComponentID in this.PlatformServicesHelper.applicationComponent.GetApplicationComponentByComponentID(testDashboard))
                this.PlatformServicesHelper.applicationComponent.DeleteApplicationComponent(applicationComponentID);

            //add a "Caredirector App" application component
            Guid applicationID = this.PlatformServicesHelper.application.GetApplicationByDisplayName("CareDirector App")[0];
            this.PlatformServicesHelper.applicationComponent.CreateApplicationComponent(applicationID, testDashboard, "systemdashboard", "_A_Mobile_Dashboard_Test");

            //trigger a sync
            settingsPage
                .WaitForSettingsPageToLoad()
                .WaitForSyncProcessToFinish()
                .TapSyncNowButton()
                .WaitForSyncProcessToFinish();

            //Navigate to the home page
            mainMenu
                .NavigateToHomePage();

            //the _A_Mobile_Dashboard_Test dashboard should be displayed 
            homePage
                .WaitForHomePageToLoad()
                .WaitForDashboardToLoad("_A_Mobile_Dashboard_Test")
                .WaitForDashboardWidgetToLoad("Task - My Active Tasks", "task", true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6623")]
        [Description("UI Test for Dashboards - 0008 - Remove all widgets and application components from the test dashboard - " +
            "Add a 'Caredirector App' application component - Add one dashboard widgets (Grid View) using a user view - View is share with test user - " +
            "sync the APP - validate that the test dashboard is displayed")]
        public void Dashboard_TestMethod7()
        {
            Guid mobile_test_user_1_UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //mobile_test_user_1

            Guid testDashboard = new Guid("564FCAD6-A58F-EA11-A2CD-005056926FE4"); //_A_Mobile_Dashboard_Test
            Guid userDataView = new Guid("65FFA115-4890-EA11-A2CD-005056926FE4"); //Tasks created in the last 30 days

            //Remove any share to the user view
            foreach (Guid shareID in this.PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(userDataView))
                this.PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //Share the user view with the mobile user
            this.PlatformServicesHelper.recordLevelAccess.CreateRecordLevelAccess(userDataView, "UserDataView", "Tasks created in the last 30 days", mobile_test_user_1_UserID, "SystemUser", "Mobile Test User 1", true, false, false, false);


            //set the widgets for the dashboard _A_Mobile_Dashboard_Test
            this.PlatformServicesHelper.systemDashboard.UpdateLayoutXML(testDashboard, LayoutXml_OneWidget_SharedUserView);

            //remove all application components for the dashboard _A_Mobile_Dashboard_Test
            foreach (Guid applicationComponentID in this.PlatformServicesHelper.applicationComponent.GetApplicationComponentByComponentID(testDashboard))
                this.PlatformServicesHelper.applicationComponent.DeleteApplicationComponent(applicationComponentID);

            //add a "Caredirector App" application component
            Guid applicationID = this.PlatformServicesHelper.application.GetApplicationByDisplayName("CareDirector App")[0];
            this.PlatformServicesHelper.applicationComponent.CreateApplicationComponent(applicationID, testDashboard, "systemdashboard", "_A_Mobile_Dashboard_Test");

            //trigger a sync
            settingsPage
                .WaitForSettingsPageToLoad()
                .WaitForSyncProcessToFinish()
                .TapSyncNowButton()
                .WaitForSyncProcessToFinish();

            //Navigate to the home page
            mainMenu
                .NavigateToHomePage();

            //the _A_Mobile_Dashboard_Test dashboard should not be displayed (the view is not shared with the mobile user)
            homePage
                .WaitForHomePageToLoad()
                .WaitForDashboardToLoad("_A_Mobile_Dashboard_Test")
                .WaitForDashboardWidgetToLoad("Task - Tasks created in the last 30 days", "task", true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6624")]
        [Description("UI Test for Dashboards - 0008 - Remove all widgets and application components from the test dashboard - " +
            "Add a 'Caredirector App' application component - Add one dashboard widgets (Grid View) using a user view - View is not share with test user - " +
            "sync the APP - validate that the test dashboard is not displayed")]
        public void Dashboard_TestMethod8()
        {
            Guid testDashboard = new Guid("564FCAD6-A58F-EA11-A2CD-005056926FE4"); //_A_Mobile_Dashboard_Test
            Guid userDataView = new Guid("65FFA115-4890-EA11-A2CD-005056926FE4"); //Tasks created in the last 30 days

            //Remove any share to the user view
            foreach (Guid shareID in this.PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(userDataView))
                this.PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //set the widgets for the dashboard _A_Mobile_Dashboard_Test
            this.PlatformServicesHelper.systemDashboard.UpdateLayoutXML(testDashboard, LayoutXml_OneWidget_SharedUserView);

            //remove all application components for the dashboard _A_Mobile_Dashboard_Test
            foreach (Guid applicationComponentID in this.PlatformServicesHelper.applicationComponent.GetApplicationComponentByComponentID(testDashboard))
                this.PlatformServicesHelper.applicationComponent.DeleteApplicationComponent(applicationComponentID);

            //add a "Caredirector App" application component
            Guid applicationID = this.PlatformServicesHelper.application.GetApplicationByDisplayName("CareDirector App")[0];
            this.PlatformServicesHelper.applicationComponent.CreateApplicationComponent(applicationID, testDashboard, "systemdashboard", "_A_Mobile_Dashboard_Test");

            //trigger a sync
            settingsPage
                .WaitForSettingsPageToLoad()
                .WaitForSyncProcessToFinish()
                .TapSyncNowButton()
                .WaitForSyncProcessToFinish();

            //Navigate to the home page
            mainMenu
                .NavigateToHomePage();

            //the _A_Mobile_Dashboard_Test dashboard should be displayed 
            homePage
                .WaitForHomePageToLoad()
                .WaitForDashboardToLoad("_A_Mobile_Dashboard_Test")
                .ValidateDashboardWidgetNotVisible("Task - My Active Tasks", "task");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6625")]
        [Description("UI Test for Dashboards - 0009 - Remove all widgets and application components from the test dashboard - " +
            "Add a 'Caredirector App' application component - Add widgets of each possible type - sync the APP - " +
            "sync the APP - validate that the test dashboard is displayed and only the List View and Grid View widgets are displayed")]
        public void Dashboard_TestMethod9()
        {
            Guid testDashboard = new Guid("564FCAD6-A58F-EA11-A2CD-005056926FE4"); //_A_Mobile_Dashboard_Test

            //Set the widgets for the dashboard _A_Mobile_Dashboard_Test
            this.PlatformServicesHelper.systemDashboard.UpdateLayoutXML(testDashboard, LayoutXml_AllWidgets);

            //remove all application components for the dashboard _A_Mobile_Dashboard_Test
            foreach (Guid applicationComponentID in this.PlatformServicesHelper.applicationComponent.GetApplicationComponentByComponentID(testDashboard))
                this.PlatformServicesHelper.applicationComponent.DeleteApplicationComponent(applicationComponentID);

            //add a "Caredirector App" application component
            Guid applicationID = this.PlatformServicesHelper.application.GetApplicationByDisplayName("CareDirector App")[0];
            this.PlatformServicesHelper.applicationComponent.CreateApplicationComponent(applicationID, testDashboard, "systemdashboard", "_A_Mobile_Dashboard_Test");

            //trigger a sync
            settingsPage
                .WaitForSettingsPageToLoad()
                .WaitForSyncProcessToFinish()
                .TapSyncNowButton()
                .WaitForSyncProcessToFinish();

            //Navigate to the home page
            mainMenu
                .NavigateToHomePage();

            //the _A_Mobile_Dashboard_Test dashboard should be displayed with 2 visible widgets
            homePage
                .WaitForHomePageToLoad()
                .WaitForDashboardToLoad("_A_Mobile_Dashboard_Test")
                .WaitForDashboardWidgetToLoad("Task - My Active Tasks", "task", true)
                .WaitForDashboardWidgetToLoad("Case Note (For Case) - Active Records", "task", false);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6626")]
        [Description("UI Test for Dashboards - 0010 - Remove all widgets and application components from the test dashboard - " +
            "Add a 'Caredirector App' application component - Add a dashboard widget (Grid View - My Active Tasks) - sync the APP - validate that the test dashboard is displayed - " +
            "Remove the 'Caredirector App' application component - sync the APP - validate that the test dashboard is no longer displayed")]
        public void Dashboard_TestMethod10()
        {
            Guid testDashboard = new Guid("564FCAD6-A58F-EA11-A2CD-005056926FE4"); //_A_Mobile_Dashboard_Test

            //Set the widgets for the dashboard _A_Mobile_Dashboard_Test
            this.PlatformServicesHelper.systemDashboard.UpdateLayoutXML(testDashboard, LayoutXml_OneWidget_GridView);

            //remove all application components for the dashboard _A_Mobile_Dashboard_Test
            foreach (Guid applicationComponentID in this.PlatformServicesHelper.applicationComponent.GetApplicationComponentByComponentID(testDashboard))
                this.PlatformServicesHelper.applicationComponent.DeleteApplicationComponent(applicationComponentID);

            //add a "Caredirector App" application component
            Guid applicationID = this.PlatformServicesHelper.application.GetApplicationByDisplayName("CareDirector App")[0];
            this.PlatformServicesHelper.applicationComponent.CreateApplicationComponent(applicationID, testDashboard, "systemdashboard", "_A_Mobile_Dashboard_Test");

            //trigger a sync
            settingsPage
                .WaitForSettingsPageToLoad()
                .WaitForSyncProcessToFinish()
                .TapSyncNowButton()
                .WaitForSyncProcessToFinish();

            //Navigate to the home page
            mainMenu
                .NavigateToHomePage();

            //the _A_Mobile_Dashboard_Test dashboard should not be displayed 
            homePage
                .WaitForHomePageToLoad()
                .WaitForDashboardToLoad("_A_Mobile_Dashboard_Test")
                .WaitForDashboardWidgetToLoad("Task - My Active Tasks", "task", true);




            //remove all application components for the dashboard _A_Mobile_Dashboard_Test
            foreach (Guid applicationComponentID in this.PlatformServicesHelper.applicationComponent.GetApplicationComponentByComponentID(testDashboard))
                this.PlatformServicesHelper.applicationComponent.DeleteApplicationComponent(applicationComponentID);

            //Navigate to the home page
            mainMenu
                .NavigateToSettingsPage();

            //trigger a Sync
            settingsPage
                .WaitForSettingsPageToLoad()
                .WaitForSyncProcessToFinish()
                .TapSyncNowButton()
                .WaitForSyncProcessToFinish();

            //Navigate to the home page
            mainMenu
                .NavigateToHomePage();

            //the _Mobile_Dashboard_Test dashboard should not be displayed 
            homePage
                .WaitForHomePageToLoad()
                .WaitForDashboardToLoad("_Default Dashboard")
                .WaitForDashboardWidgetToLoad("Task - My Team Active Tasks", "task", true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6627")]
        [Description("UI Test for Dashboards - 0011 - Remove all widgets and application components from the test dashboard - " +
            "Add a 'Caredirector App' application component - Add a dashboard widget (Grid View - My Active Tasks) - sync the APP - validate that the test dashboard is displayed - " +
            "Remove all widgets from the system dashboard - sync the APP - validate that the test dashboard is no longer displayed")]
        public void Dashboard_TestMethod11()
        {
            Guid testDashboard = new Guid("564FCAD6-A58F-EA11-A2CD-005056926FE4"); //_A_Mobile_Dashboard_Test

            //Set the widgets for the dashboard _A_Mobile_Dashboard_Test
            this.PlatformServicesHelper.systemDashboard.UpdateLayoutXML(testDashboard, LayoutXml_OneWidget_GridView);

            //remove all application components for the dashboard _A_Mobile_Dashboard_Test
            foreach (Guid applicationComponentID in this.PlatformServicesHelper.applicationComponent.GetApplicationComponentByComponentID(testDashboard))
                this.PlatformServicesHelper.applicationComponent.DeleteApplicationComponent(applicationComponentID);

            //add a "Caredirector App" application component
            Guid applicationID = this.PlatformServicesHelper.application.GetApplicationByDisplayName("CareDirector App")[0];
            this.PlatformServicesHelper.applicationComponent.CreateApplicationComponent(applicationID, testDashboard, "systemdashboard", "_A_Mobile_Dashboard_Test");

            //trigger a sync
            settingsPage
                .WaitForSettingsPageToLoad()
                .WaitForSyncProcessToFinish()
                .TapSyncNowButton()
                .WaitForSyncProcessToFinish();

            //Navigate to the home page
            mainMenu
                .NavigateToHomePage();

            //the _A_Mobile_Dashboard_Test dashboard should not be displayed 
            homePage
                .WaitForHomePageToLoad()
                .WaitForDashboardToLoad("_A_Mobile_Dashboard_Test")
                .WaitForDashboardWidgetToLoad("Task - My Active Tasks", "task", true);




            //remove all wiidgets from the dashboard
            this.PlatformServicesHelper.systemDashboard.UpdateLayoutXML(testDashboard, LayoutXml_NoWidgets);

            //Navigate to the home page
            mainMenu
                .NavigateToSettingsPage();

            //trigger a Sync
            settingsPage
                .WaitForSettingsPageToLoad()
                .WaitForSyncProcessToFinish()
                .TapSyncNowButton()
                .WaitForSyncProcessToFinish();

            //Navigate to the home page
            mainMenu
                .NavigateToHomePage();

            //the _Mobile_Dashboard_Test dashboard should not be displayed 
            homePage
                .WaitForHomePageToLoad()
                .WaitForDashboardToLoad("_Default Dashboard")
                .WaitForDashboardWidgetToLoad("Task - My Team Active Tasks", "task", true);
        }

        [Description("Method will return the name of all tests and the Description of each one")]
        [Test]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}

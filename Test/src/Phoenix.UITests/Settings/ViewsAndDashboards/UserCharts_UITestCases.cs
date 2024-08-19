using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Phoenix.UITests.Settings.ViewsAndDashboards
{
    /// <summary>
    /// This class contains Automated UI test scripts for Summary Dashboards
    /// </summary>
    [TestClass]
    public class UserCharts_UITestCases : FunctionalTest
    {

        [TestInitialize()]
        public void SetupMethod()
        {
            try
            {
                var systemyserId = dbHelper.systemUser.GetSystemUserByUserName("DashboardsUserAdmin").First();
                dbHelper.systemUser.UpdateLastPasswordChangedDate(systemyserId, DateTime.Now.Date);
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-6296

        [TestProperty("JiraIssueID", "CDV6-24916")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-6296 - " +
            "Login in the web app - Navigate to Settings; Views & Dashboards; User Charts - Open a chart record (Area Chart) - with a DateTime field in the Category - " +
            "Validate that all fields are correctly set")]
        [TestCategory("UITest")]
        [TestMethod]
        public void UserCharts_UITestMethod01()
        {

            Guid UserChartID = new Guid("a568d481-8535-eb11-a2d7-005056926fe4"); //Area Chart


            loginPage
                .GoToLoginPage()
                .Login("DashboardsUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToUserChartsSection();

            userChartsPage
                .WaitForUserChartsPageToLoad()
                .OpenUserChartRecord(UserChartID.ToString());

            userChartPopup
                .WaitForUserChartPopupToLoad()
                .ValidatePopupHeaderTitle("User Chart: Area Chart")
                .ValidateName("Area Chart")
                .ValidateType("Area")
                .ValidateRecordType("Task")
                .ValidateDataView("Kelli Chan Tasks")

                .ValidateSeries(1, "Reason")
                .ValidateSeriesResultType(1, "Count Non-Empty")
                .ValidateSeriesLabel(1, "Non Empty")

                .ValidateSeries(2, "Reason")
                .ValidateSeriesResultType(2, "Count Empty")
                .ValidateSeriesLabel(2, "Empty")

                .ValidateCategory(1, "Due")
                .ValidateCategoryGroupType(1, "Day");
        }

        [TestProperty("JiraIssueID", "CDV6-24917")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-6296 - " +
            "Login in the web app - Navigate to Settings; Views & Dashboards; User Charts - Open a chart record (Bar Chart) - with a DateTime field in the Category - " +
            "Validate that all fields are correctly set")]
        [TestCategory("UITest")]
        [TestMethod]
        public void UserCharts_UITestMethod02()
        {
            Guid UserChartID = new Guid("a4429c05-8135-eb11-a2d7-005056926fe4"); //Bar Chart - Reason | Due Date


            loginPage
                .GoToLoginPage()
                .Login("DashboardsUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToUserChartsSection();

            userChartsPage
                .WaitForUserChartsPageToLoad()
                .OpenUserChartRecord(UserChartID.ToString());

            userChartPopup
                .WaitForUserChartPopupToLoad()
                .ValidatePopupHeaderTitle("User Chart: Bar Chart - Reason | Due Date")
                .ValidateName("Bar Chart - Reason | Due Date")
                .ValidateType("Bar")
                .ValidateRecordType("Task")
                .ValidateDataView("Kelli Chan Tasks")

                .ValidateSeries(1, "Reason")
                .ValidateSeriesResultType(1, "Count Non-Empty")
                .ValidateSeriesLabel(1, "")

                .ValidateSeries(2, "Reason")
                .ValidateSeriesResultType(2, "Count Empty")
                .ValidateSeriesLabel(2, "")

                .ValidateCategory(1, "Due")
                .ValidateCategoryGroupType(1, "")

                .ValidateChartBarVisible(11, 0, 2) //  03/12/2020  Non-Empty
                .ValidateChartBarVisible(11, 1, 1) //  03/12/2020  Empty

                .ValidateChartBarVisible(14, 1, 4) //  04/12/2020  Empty

                ;
            ;
        }

        [TestProperty("JiraIssueID", "CDV6-24918")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-6296 - " +
            "Login in the web app - Navigate to Settings; Views & Dashboards; User Charts - Open a chart record (Column Chart) - with a DateTime field in the Category and Day in the Category Group Type - " +
            "Validate that all fields are correctly set")]
        [TestCategory("UITest")]
        [TestMethod]
        public void UserCharts_UITestMethod03()
        {
            Guid UserChartID = new Guid("aeacfe4c-7f35-eb11-a2d7-005056926fe4"); //Column Chart - Reason | Due Date (Day)


            loginPage
                .GoToLoginPage()
                .Login("DashboardsUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToUserChartsSection();

            userChartsPage
                .WaitForUserChartsPageToLoad()
                .OpenUserChartRecord(UserChartID.ToString());

            userChartPopup
                .WaitForUserChartPopupToLoad()
                .ValidatePopupHeaderTitle("User Chart: Column Chart - Reason | Due Date (Day)")
                .ValidateName("Column Chart - Reason | Due Date (Day)")
                .ValidateType("Column")
                .ValidateRecordType("Task")
                .ValidateDataView("Kelli Chan Tasks")

                .ValidateSeries(1, "Reason")
                .ValidateSeriesResultType(1, "Count Non-Empty")
                .ValidateSeriesLabel(1, "")

                .ValidateSeries(2, "Reason")
                .ValidateSeriesResultType(2, "Count Empty")
                .ValidateSeriesLabel(2, "")

                .ValidateCategory(1, "Due")
                .ValidateCategoryGroupType(1, "Day")

                .ValidateChartBarVisible(4, 0, 4) //  03/12/2020  Non-Empty
                .ValidateChartBarVisible(4, 1, 1) //  03/12/2020  Empty

                .ValidateChartBarVisible(5, 1, 4) //  04/12/2020  Empty

                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24919")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-6296 - " +
            "Login in the web app - Navigate to Settings; Views & Dashboards; User Charts - Open a chart record (Column Chart) - with a DateTime field in the Category and Month in the Category Group Type - " +
            "Validate that all fields are correctly set")]
        [TestCategory("UITest")]
        [TestMethod]
        public void UserCharts_UITestMethod04()
        {
            Guid UserChartID = new Guid("d80323f8-8935-eb11-a2d7-005056926fe4"); //Column Chart - Reason | Due Date (Month)


            loginPage
                .GoToLoginPage()
                .Login("DashboardsUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToUserChartsSection();

            userChartsPage
                .WaitForUserChartsPageToLoad()
                .OpenUserChartRecord(UserChartID.ToString());

            userChartPopup
                .WaitForUserChartPopupToLoad()
                .ValidatePopupHeaderTitle("User Chart: Column Chart - Reason | Due Date (Month)")
                .ValidateName("Column Chart - Reason | Due Date (Month)")
                .ValidateType("Column")
                .ValidateRecordType("Task")
                .ValidateDataView("Kelli Chan Tasks")

                .ValidateSeries(1, "Reason")
                .ValidateSeriesResultType(1, "Count Non-Empty")
                .ValidateSeriesLabel(1, "Non Empty")

                .ValidateSeries(2, "Reason")
                .ValidateSeriesResultType(2, "Count Empty")
                .ValidateSeriesLabel(2, "Empty")

                .ValidateCategory(1, "Due")
                .ValidateCategoryGroupType(1, "Month")

                .ValidateChartBarVisible(0, 0, 2) //  Nov 2019  Non-Empty
                .ValidateChartBarVisible(0, 1, 1) //  Nov 2019  Empty

                .ValidateChartBarVisible(1, 0, 1) //  Nov 2020  Non-Empty
                .ValidateChartBarVisible(1, 1, 3) //  Nov 2020  Empty

                .ValidateChartBarVisible(2, 0, 6) //  Dec 2020  Non-Empty
                .ValidateChartBarVisible(2, 1, 7) //  Dec 2020  Empty
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24920")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-6296 - " +
            "Login in the web app - Navigate to Settings; Views & Dashboards; User Charts - Open a chart record (Column Chart) - with a DateTime field in the Category and Year in the Category Group Type - " +
            "Validate that all fields are correctly set")]
        [TestCategory("UITest")]
        [TestMethod]
        public void UserCharts_UITestMethod05()
        {
            Guid UserChartID = new Guid("189986c7-8a35-eb11-a2d7-005056926fe4"); //Column Chart - Reason | Due Date (Year)

            loginPage
                .GoToLoginPage()
                .Login("DashboardsUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToUserChartsSection();

            userChartsPage
                .WaitForUserChartsPageToLoad()
                .OpenUserChartRecord(UserChartID.ToString());

            userChartPopup
                .WaitForUserChartPopupToLoad()
                .ValidatePopupHeaderTitle("User Chart: Column Chart - Reason | Due Date (Year)")
                .ValidateName("Column Chart - Reason | Due Date (Year)")
                .ValidateType("Column")
                .ValidateRecordType("Task")
                .ValidateDataView("Kelli Chan Tasks")

                .ValidateSeries(1, "Reason")
                .ValidateSeriesResultType(1, "Count Empty")
                .ValidateSeriesLabel(1, "")

                .ValidateSeries(2, "Reason")
                .ValidateSeriesResultType(2, "Count Non-Empty")
                .ValidateSeriesLabel(2, "")

                .ValidateCategory(1, "Due")
                .ValidateCategoryGroupType(1, "Year")


                .ValidateChartBarVisible(0, 0, 1) //  2019  Non-Empty
                .ValidateChartBarVisible(0, 1, 2) //  2019  Empty

                .ValidateChartBarVisible(1, 0, 10) //  2020  Non-Empty
                .ValidateChartBarVisible(1, 1, 7) //  2020  Empty
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24921")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-6296 - " +
            "Login in the web app - Navigate to Settings; Views & Dashboards; User Charts - Open a chart record (Column Chart) - with a Date field in the Category and Day in the Category Group Type - " +
            "Validate that all fields are correctly set")]
        [TestCategory("UITest")]
        [TestMethod]
        public void UserCharts_UITestMethod06()
        {
            Guid UserChartID = new Guid("121b5b5b-5236-eb11-a2d8-005056926fe4"); //Column Chart - Reason | Event Date (Day)


            loginPage
                .GoToLoginPage()
                .Login("DashboardsUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToUserChartsSection();

            userChartsPage
                .WaitForUserChartsPageToLoad()
                .OpenUserChartRecord(UserChartID.ToString());

            userChartPopup
                .WaitForUserChartPopupToLoad()
                .ValidatePopupHeaderTitle("User Chart: Column Chart - Reason | Event Date (Day)")
                .ValidateName("Column Chart - Reason | Event Date (Day)")
                .ValidateType("Column")
                .ValidateRecordType("Task")
                .ValidateDataView("Kelli Chan Tasks")

                .ValidateSeries(1, "Reason")
                .ValidateSeriesResultType(1, "Count Non-Empty")
                .ValidateSeriesLabel(1, "")

                .ValidateSeries(2, "Reason")
                .ValidateSeriesResultType(2, "Count Empty")
                .ValidateSeriesLabel(2, "")

                .ValidateCategory(1, "Event Date")
                .ValidateCategoryGroupType(1, "Day")

                .ValidateChartBarVisible(0, 0, 5) //  NULL  Non-Empty
                .ValidateChartBarVisible(0, 1, 8) //  NULL  Empty

                .ValidateChartBarVisible(1, 0, 2) //  26/11/2019  Non-Empty
                .ValidateChartBarVisible(1, 1, 1) //  26/11/2019  Empty

                .ValidateChartBarVisible(3, 0, 2) //  03/12/2020  Empty
                .ValidateChartBarVisible(3, 1, 1) //  03/12/2020  Empty

                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24922")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-6296 - " +
            "Login in the web app - Navigate to Settings; Views & Dashboards; User Charts - Open a chart record (Column Chart) - with a Date field in the Category and Month in the Category Group Type - " +
            "Validate that all fields are correctly set")]
        [TestCategory("UITest")]
        [TestMethod]
        public void UserCharts_UITestMethod07()
        {
            Guid UserChartID = new Guid("6f1e1e39-5336-eb11-a2d8-005056926fe4"); //Column Chart - Reason | Event Date (Month)


            loginPage
                .GoToLoginPage()
                .Login("DashboardsUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToUserChartsSection();

            userChartsPage
                .WaitForUserChartsPageToLoad()
                .OpenUserChartRecord(UserChartID.ToString());

            userChartPopup
                .WaitForUserChartPopupToLoad()
                .ValidatePopupHeaderTitle("User Chart: Column Chart - Reason | Event Date (Month)")
                .ValidateName("Column Chart - Reason | Event Date (Month)")
                .ValidateType("Column")
                .ValidateRecordType("Task")
                .ValidateDataView("Kelli Chan Tasks")

                .ValidateSeries(1, "Reason")
                .ValidateSeriesResultType(1, "Count Non-Empty")
                .ValidateSeriesLabel(1, "")

                .ValidateSeries(2, "Reason")
                .ValidateSeriesResultType(2, "Count Empty")
                .ValidateSeriesLabel(2, "")

                .ValidateCategory(1, "Event Date")
                .ValidateCategoryGroupType(1, "Month")

                .ValidateChartBarVisible(1, 0, 2) //  Nov 2019  Non-Empty
                .ValidateChartBarVisible(1, 1, 1) //  Nov 2019  Empty

                //.ValidateChartBarVisible("SvgjsPath1025", 1) //  Nov 2020  Non-Empty
                .ValidateChartBarVisible(2, 1, 1) //  Nov 2020  Empty

                .ValidateChartBarVisible(3, 0, 2) //  Dec 2020  Non-Empty
                .ValidateChartBarVisible(3, 1, 1) //  Dec 2020  Empty
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24923")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-6296 - " +
            "Login in the web app - Navigate to Settings; Views & Dashboards; User Charts - Open a chart record (Column Chart) - with a Date field in the Category and Year in the Category Group Type - " +
            "Validate that all fields are correctly set")]
        [TestCategory("UITest")]
        [TestMethod]
        public void UserCharts_UITestMethod08()
        {
            Guid UserChartID = new Guid("0a3abaf7-5336-eb11-a2d8-005056926fe4"); //Column Chart - Reason | Event Date (Year)

            loginPage
                .GoToLoginPage()
                .Login("DashboardsUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToUserChartsSection();

            userChartsPage
                .WaitForUserChartsPageToLoad()
                .OpenUserChartRecord(UserChartID.ToString());

            userChartPopup
                .WaitForUserChartPopupToLoad()
                .ValidatePopupHeaderTitle("User Chart: Column Chart - Reason | Event Date (Year)")
                .ValidateName("Column Chart - Reason | Event Date (Year)")
                .ValidateType("Column")
                .ValidateRecordType("Task")
                .ValidateDataView("Kelli Chan Tasks")

                .ValidateSeries(1, "Reason")
                .ValidateSeriesResultType(1, "Count Non-Empty")
                .ValidateSeriesLabel(1, "")

                .ValidateSeries(2, "Reason")
                .ValidateSeriesResultType(2, "Count Empty")
                .ValidateSeriesLabel(2, "")

                .ValidateCategory(1, "Event Date")
                .ValidateCategoryGroupType(1, "Year")


                .ValidateChartBarVisible(0, 0, 5) //  NULL  Non-Empty
                .ValidateChartBarVisible(0, 1, 8) //  NULL Empty

                .ValidateChartBarVisible(1, 0, 2) //  2019  Non-Empty
                .ValidateChartBarVisible(1, 1, 1) //  2019 Empty

                .ValidateChartBarVisible(2, 0, 2) //  2020  Non-Empty
                .ValidateChartBarVisible(2, 1, 2) //  2020  Empty
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24924")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-6296 - " +
            "Login in the web app - Navigate to Settings; Views & Dashboards; User Charts - Open a chart record (Donut Chart) - with a Date field in the Category and Year in the Category Group Type - " +
            "Validate that all fields are correctly set")]
        [TestCategory("UITest")]
        [TestMethod]
        public void UserCharts_UITestMethod09()
        {
            Guid UserChartID = new Guid("bca5bcb6-8535-eb11-a2d7-005056926fe4"); //Column Chart - Reason | Due Date (Year)

            loginPage
                .GoToLoginPage()
                .Login("DashboardsUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToUserChartsSection();

            userChartsPage
                .WaitForUserChartsPageToLoad()
                .OpenUserChartRecord(UserChartID.ToString());

            userChartPopup
                .WaitForUserChartPopupToLoad()
                .ValidatePopupHeaderTitle("User Chart: Donut Chart")
                .ValidateName("Donut Chart")
                .ValidateType("Donut")
                .ValidateRecordType("Task")
                .ValidateDataView("Kelli Chan Tasks")

                .ValidateSeries(1, "Reason")
                .ValidateSeriesResultType(1, "Count Empty")
                .ValidateSeriesLabel(1, "")

                .ValidateCategory(1, "Due")
                .ValidateCategoryGroupType(1, "Day")

                .ValidateDonutBarVisible(5, 0, "130.9090909090909") // 36.4%
                .ValidateDonutBarVisible(0, 0, "32.72727272727273") // 9.1%
                .ValidateDonutBarVisible(1, 0, "98.18181818181819") // 27.3%
                .ValidateDonutBarVisible(2, 0, "65.45454545454547") // 18.2%
                .ValidateDonutBarVisible(4, 0, "32.72727272727272") // 9.1%
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24925")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-6296 - " +
            "Login in the web app - Navigate to Settings; Views & Dashboards; User Charts - Tap on the add new record button - " +
            "Select Column for the bar type - Select Task for the Record Type - Select Kelli Chan Tasks for the record type - add one series for 'Reason' & 'Count Empty' - " +
            "add one series for 'Reason' 'Count Non-Empty' - Select 'Due' field in category and add month as the sub group - set all other mandatory fields - Save the record - Validate that the record is correctly saved.")]
        [TestCategory("UITest")]
        [TestMethod]
        public void UserCharts_UITestMethod10()
        {
            Guid UserChartID = new Guid("bca5bcb6-8535-eb11-a2d7-005056926fe4"); //Column Chart - Reason | Due Date (Year)

            foreach (var userchartid in dbHelper.userChart.GetUserChartByName("DashboardsUserAdmin - New Column Chart"))
                dbHelper.userChart.DeleteUserChart(userchartid);

            loginPage
                .GoToLoginPage()
                .Login("DashboardsUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToUserChartsSection();

            userChartsPage
                .WaitForUserChartsPageToLoad()
                .TapAddNewRecordButton();

            userChartPopup
                .WaitForUserChartPopupToLoad()
                .ValidatePopupHeaderTitle("User Chart: New")
                .InsertName("DashboardsUserAdmin - New Column Chart")
                .SelectType("Column")
                .SelectRecordType("Task")
                .SelectDataView("Kelli Chan Tasks")

                .SelectSeries(1, "Reason")
                .SelectSeriesResultType(1, "Count Empty")

                .TapAddSeries()

                .SelectSeries(2, "Reason")
                .SelectSeriesResultType(2, "Count Non-Empty")

                .SelectCategory(1, "Due")
                .SelectCategoryGroupType(1, "Month")

                .TapSavebutton();

            System.Threading.Thread.Sleep(2000);

            userChartPopup
                .ValidatePopupHeaderTitle("User Chart: DashboardsUserAdmin - New Column Chart")
                .ValidateName("DashboardsUserAdmin - New Column Chart")
                .ValidateType("Column")
                .ValidateRecordType("Task")
                .ValidateDataView("Kelli Chan Tasks")

                .ValidateSeries(1, "Reason")
                .ValidateSeriesResultType(1, "Count Empty")
                .ValidateSeriesLabel(1, "")

                .ValidateSeries(2, "Reason")
                .ValidateSeriesResultType(2, "Count Non-Empty")
                .ValidateSeriesLabel(2, "")

                .ValidateCategory(1, "Due")
                .ValidateCategoryGroupType(1, "Month")

                .ValidateChartBarVisible(0, 0, 1) //  Nov 2019  Non-Empty
                .ValidateChartBarVisible(0, 1, 2) //  Nov 2019  Empty

                .ValidateChartBarVisible(1, 0, 3) //  Nov 2020  Non-Empty
                .ValidateChartBarVisible(1, 1, 1) //  Nov 2020  Empty

                .ValidateChartBarVisible(2, 0, 7) //  Dec 2020  Non-Empty
                .ValidateChartBarVisible(2, 1, 6) //  Dec 2020  Empty
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24926")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-6296 - " +
            "Login in the Web APP - Navigate to the Dashboards page - Select 'Dashboard User Charts' - Validate that the Column Chart (by Day) is displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void UserCharts_UITestMethod11()
        {
            Guid UserChartID = new Guid("aeacfe4c-7f35-eb11-a2d7-005056926fe4"); //Column Chart - Reason | Due Date (Day)


            loginPage
                .GoToLoginPage()
                .Login("DashboardsUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDashboardsSection();

            dashboardsPage
                .WaitForDashboardPageToLoad()
                .SelectDashboard("Dashboard User Charts")
                .WaitForDashboardWidgetToLoad(UserChartID.ToString(), "task")
                .ValidateColumnChartYAxixName("Count Non-Empty (Reason)")
                .ValidateColumnChartXAxixName("Due")
                .ValidateChartBarVisible(0, 0, 2)
                .ValidateChartBarVisible(0, 1, 1)
                .ValidateChartBarVisible(1, 0, 1)
                .ValidateChartBarVisible(1, 1, 3)
                .ValidateChartBarVisible(2, 0, 1)
                .ValidateChartBarVisible(2, 1, 2)
                .ValidateChartBarVisible(3, 0, 1)
                .ValidateChartBarVisible(4, 0, 4)
                .ValidateChartBarVisible(4, 1, 1)
                .ValidateChartBarVisible(5, 1, 4)
                ;
        }

        //[TestProperty("JiraIssueID", "CDV6-24927")]
        //[Description("https://advancedcsg.atlassian.net/browse/CDV6-6296 - " +
        //    "Login in the Web APP - Navigate to the Dashboards page - Select 'Dashboard User Charts' - For the Column Chart (by Day) tap on a chart bar - " +
        //    "Validate that the user is redirected to the tasks page and that only the matching tasks are displayed")]
        //[TestCategory("UITest")]
        //[TestMethod]
        //public void UserCharts_UITestMethod12()
        //{
        //    Guid UserChartID = new Guid("aeacfe4c-7f35-eb11-a2d7-005056926fe4"); //Column Chart - Reason | Due Date (Day)

        //    Guid taskID1 = new Guid("e40290ff-8a35-eb11-a2d7-005056926fe4"); //Task 20
        //    Guid taskID2 = new Guid("e40290ff-8a35-eb11-a2d7-005056926fe4"); //Task 11
        //    Guid taskID3 = new Guid("e40290ff-8a35-eb11-a2d7-005056926fe4"); //Task 10
        //    Guid taskID4 = new Guid("e40290ff-8a35-eb11-a2d7-005056926fe4"); //Task 9

        //    Guid taskID5 = new Guid("1e6e2db7-7e35-eb11-a2d7-005056926fe4"); //Task 12
        //    Guid taskID6 = new Guid("9719b664-7e35-eb11-a2d7-005056926fe4"); //Task 7
        //    Guid taskID7 = new Guid("a2fd9887-7e35-eb11-a2d7-005056926fe4"); //Task 8
        //    Guid taskID8 = new Guid("0269c84d-7e35-eb11-a2d7-005056926fe4"); //Task 3


        //    loginPage
        //        .GoToLoginPage()
        //        .Login("DashboardsUserAdmin", "Passw0rd_!")
        //        .WaitFormHomePageToLoad();

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToDashboardsSection();

        //    dashboardsPage
        //        .WaitForDashboardPageToLoad()
        //        .SelectDashboard("Dashboard User Charts")

        //        .WaitForDashboardWidgetToLoad(UserChartID.ToString(), "task")
        //        .ClickChartBar(5, 1, 4);

        //    queryResultsPage
        //        .WaitForQueryResultsPage("Tasks")
        //        .ValidateRecordCellText(taskID1.ToString(), "2", "Task 20")
        //        .ValidateRecordCellText(taskID2.ToString(), "2", "Task 11")
        //        .ValidateRecordCellText(taskID3.ToString(), "2", "Task 10")
        //        .ValidateRecordCellText(taskID4.ToString(), "2", "Task 9")

        //        .ValidateRecordNotPresent(taskID5.ToString())
        //        .ValidateRecordNotPresent(taskID6.ToString())
        //        .ValidateRecordNotPresent(taskID7.ToString())
        //        .ValidateRecordNotPresent(taskID8.ToString())

        //        ;


        //}

        #endregion


        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}

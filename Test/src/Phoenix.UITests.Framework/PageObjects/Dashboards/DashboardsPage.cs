using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class DashboardsPage : CommonMethods
    {
        public DashboardsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By widgetIframe = By.XPath("//iframe[@onload='CW.DashboardControl.ResizeIFrame(this)']");

        By widgetIframeBySource(string srcID, string type) => By.XPath("//iframe[contains(@src,'" + srcID + "&type=" + type + "')]");
        


        readonly By DashboardsPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By dashboardSelector = By.Id("CWDashboardSelector");
        readonly By refreshButton = By.Id("btnRefresh");



        readonly By automationtestingUsedDashboard_NewRecordButton = By.Id("TI_NewRecordButton");
        By automationtestingUsedDashboard_Record(string recordID) => By.XPath("//*[@id='" + recordID + "_Primary']");



        #region User Chart

        By chart_Y_Axix_Name(string ExpectedText) => By.XPath("//*[@class='apexcharts-text apexcharts-yaxis-title-text axis-title'][text()='" + ExpectedText + "']");
        By chart_X_Axix_Name(string ExpectedText) => By.XPath("//*[@class='apexcharts-text apexcharts-xaxis-title-text axis-title'][text()='" + ExpectedText + "']");
        By chartBarByIDAndValue(string id, int Val) => By.XPath("//*[@id='" + id + "'][@val='" + Val + "']");
        By chartBarByJAndIndexAndValue(int j, int index, int val) => By.XPath("//*[@j='" + j + "'][@index='" + index + "'][@val='" + val + "']");
        By chartBarByPathTo(string pathTo) => By.XPath("//*[@pathTo='" + pathTo + "']");

        #endregion




        public DashboardsPage WaitForDashboardPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(NewRecordButton);
            WaitForElement(dashboardSelector);
            WaitForElement(refreshButton);

            ValidateElementText(DashboardsPageHeader, "Dashboard");

            return this;
        }

        public DashboardsPage SelectDashboard(string DashboardName)
        {
            SelectPicklistElementByText(dashboardSelector, DashboardName);

            return this;
        }

        public DashboardsPage WaitForDashboardWidgetToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(widgetIframe);
            WaitForElementToBeClickable(widgetIframe);
            SwitchToIframe(widgetIframe);

            WaitForElement(automationtestingUsedDashboard_NewRecordButton);

            return this;
        }

        public DashboardsPage WaitForDashboardWidgetToLoad(string srcID, string Type)
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(widgetIframeBySource(srcID, Type));
            WaitForElementToBeClickable(widgetIframeBySource(srcID, Type));
            SwitchToIframe(widgetIframeBySource(srcID, Type));

            return this;
        }


        public DashboardsPage ValidateAutomationTestingSummaryDashboardRecordPresent(string RecordID)
        {
            WaitForElement(automationtestingUsedDashboard_Record(RecordID));

            return this;
        }
        public DashboardsPage ValidateAutomationTestingSummaryDashboardRecordNotPresent(string RecordID)
        {
            WaitForElementNotVisible(automationtestingUsedDashboard_Record(RecordID), 7);

            return this;
        }




        public DashboardsPage ValidateColumnChartYAxixName(string ExpectedText)
        {
            WaitForElement(chart_Y_Axix_Name(ExpectedText));

            return this;
        }
        public DashboardsPage ValidateColumnChartXAxixName(string ExpectedText)
        {
            WaitForElement(chart_X_Axix_Name(ExpectedText));

            return this;
        }
        public DashboardsPage ValidateChartBarVisible(string BarID, int BarValue)
        {
            WaitForElement(chartBarByIDAndValue(BarID, BarValue));

            return this;
        }

        public DashboardsPage ValidateChartBarVisible(int j, int index, int val)
        {
            WaitForElement(chartBarByJAndIndexAndValue(j, index, val));

            return this;
        }


        public DashboardsPage ClickChartBar(string BarID, int BarValue)
        {
            System.Threading.Thread.Sleep(2000);
            WaitForElementToBeClickable(chartBarByIDAndValue(BarID, BarValue));
            Click(chartBarByIDAndValue(BarID, BarValue));

            return this;
        }

        public DashboardsPage ClickChartBar(int j, int index, int val)
        {
            System.Threading.Thread.Sleep(2000);
            WaitForElementToBeClickable(chartBarByJAndIndexAndValue(j, index, val));
            Click(chartBarByJAndIndexAndValue(j, index, val));

            return this;
        }

        public DashboardsPage ClickChartBarUsingPathToAttribute(string pathTo)
        {
            System.Threading.Thread.Sleep(2000);
            WaitForElementToBeClickable(chartBarByPathTo(pathTo));
            ClickSpecial(chartBarByPathTo(pathTo));

            return this;
        }


    }
}

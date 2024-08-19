using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;



namespace Phoenix.UITests.Framework.PageObjects
{
    public class HomePage : CommonMethods
    {
        public HomePage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By SystemDashboardRecordIFrame = By.XPath("//iframe[@id = 'CWFrame'][contains(@src,'dashboardpage.aspx')]");

        readonly By topDiv = By.XPath("//*[@id='CWContent']");

        readonly By DashboardsLink = By.XPath("//li[@id='CWTab_d1574480-95ff-e811-9c08-1866da1e4209']/a[text()='Dashboards']");
        readonly By DashboardsLink_New = By.XPath("//li[@id='CWTab_1dab788f-a5f3-ec11-a335-005056926fe4']/a[text()='My Dashboards']");
        readonly By MyPeopleLink = By.XPath("//li[@id='CWTab_21754496-a6f3-ec11-a335-005056926fe4']/a[text()='My People']");

        readonly By MyCasesLink = By.XPath("//li[@id='CWTab_0417c61a-caff-e811-9c08-1866da1e4209']/a[text()='My Cases']");
        readonly By MyFormsLink = By.XPath("//li[@id='CWTab_79780062-c114-e911-80dc-0050560502cc']/a[text()='My Forms']");

        readonly By dashboardSelector = By.Id("CWDashboardSelector");
        readonly By advancedSearchButton = By.XPath("//a[@title = 'Advanced Search']");

        readonly By ActiveRecordsMainGrid = By.Id("CWMain");

        public HomePage WaitFormHomePageToLoad()
        {
            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            Wait.Until(c => c.FindElement(DashboardsLink));
            Wait.Until(c => c.FindElement(MyCasesLink));
            Wait.Until(c => c.FindElement(MyFormsLink));

            return this;
        }

        public HomePage WaitFormHealthNLocalAuthHomePageToLoad()
        {
            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            Wait.Until(c => c.FindElement(DashboardsLink_New));
            Wait.Until(c => c.FindElement(MyPeopleLink));

            return this;
        }

        public HomePage WaitForCareProvidermHomePageToLoad()
        {
            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(topDiv);

            WaitForElement(SystemDashboardRecordIFrame);
            SwitchToIframe(SystemDashboardRecordIFrame);

            WaitForElement(ActiveRecordsMainGrid);

            return this;
        }



        public HomePage WaitFormHomePageToLoad(bool DashboardsLinkVisible, bool MyCasesLinkVisible, bool MyFormsLinkVisible)
        {
            this.SwitchToDefaultFrame();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            WaitForElement(topDiv);

            return this;
        }
        public HomePage ClickAdvanceSearchButton()
        {
            SwitchToDefaultFrame();
            WaitForElementToBeClickable(advancedSearchButton);
            Click(advancedSearchButton);

            return this;
        }

        public HomePage SelectDahsboard(string DashboardName)
        {
            SwitchToIframe(SystemDashboardRecordIFrame);

            WaitForElement(dashboardSelector);
            SelectPicklistElementByText(dashboardSelector, DashboardName);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

       
    }

    
}

using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SummaryDashboardsPage : CommonMethods
    {
        public SummaryDashboardsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By SummaryDashboardsPageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Summary Dashboards']");


        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        By SummaryDashboardRow(string SummaryDashboardID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + SummaryDashboardID + "']/td[2]");
        By SummaryDashboardRowCheckBox(string SummaryDashboardID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + SummaryDashboardID + "']/td[1]/input");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");






        public SummaryDashboardsPage WaitForSummaryDashboardsPageToLoad()
        {
            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(SummaryDashboardsPageHeader);

            return this;
        }

        public SummaryDashboardsPage SearchSummaryDashboardRecord(string SearchQuery, string SummaryDashboardID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(SummaryDashboardRow(SummaryDashboardID));

            return this;
        }

        public SummaryDashboardsPage SearchSummaryDashboardRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public SummaryDashboardsPage OpenSummaryDashboardRecord(string SummaryDashboardId)
        {
            WaitForElement(SummaryDashboardRow(SummaryDashboardId));
            Click(SummaryDashboardRow(SummaryDashboardId));

            return this;
        }

        public SummaryDashboardsPage SelectSummaryDashboardRecord(string SummaryDashboardId)
        {
            WaitForElement(SummaryDashboardRowCheckBox(SummaryDashboardId));
            Click(SummaryDashboardRowCheckBox(SummaryDashboardId));

            return this;
        }

        
    }
}

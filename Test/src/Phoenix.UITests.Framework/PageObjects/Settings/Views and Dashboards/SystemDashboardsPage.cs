using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SystemDashboardsPage : CommonMethods
    {
        public SystemDashboardsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By SystemDashboardsPageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='System Dashboards']");


        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        By SystemDashboardRow(string SystemDashboardID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + SystemDashboardID + "']/td[2]");
        By SystemDashboardRowCheckBox(string SystemDashboardID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + SystemDashboardID + "']/td[1]/input");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");






        public SystemDashboardsPage WaitForSystemDashboardsPageToLoad()
        {
            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(SystemDashboardsPageHeader);

            return this;
        }

        public SystemDashboardsPage SearchSystemDashboardRecord(string SearchQuery, string SystemDashboardID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(SystemDashboardRow(SystemDashboardID));

            return this;
        }

        public SystemDashboardsPage SearchSystemDashboardRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public SystemDashboardsPage OpenSystemDashboardRecord(string SystemDashboardId)
        {
            WaitForElement(SystemDashboardRow(SystemDashboardId));
            Click(SystemDashboardRow(SystemDashboardId));

            return this;
        }

        public SystemDashboardsPage SelectSystemDashboardRecord(string SystemDashboardId)
        {
            WaitForElement(SystemDashboardRowCheckBox(SystemDashboardId));
            Click(SystemDashboardRowCheckBox(SystemDashboardId));

            return this;
        }

        
    }
}

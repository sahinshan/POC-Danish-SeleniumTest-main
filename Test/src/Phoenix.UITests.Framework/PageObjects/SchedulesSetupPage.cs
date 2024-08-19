using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SchedulesSetupPage : CommonMethods
    {
        public SchedulesSetupPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By addButton = By.Id("TI_NewRecordButton");

        readonly By searchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");

        By scheduleSetupRecord(string RecordID) => By.XPath("//*[@id='" + RecordID + "']/td[2]");


        

        public SchedulesSetupPage WaitForSchedulesSetupPageToLoad()
        {
            this.driver.SwitchTo().DefaultContent();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(pageHeader);
            this.WaitForElement(searchTextBox);
            this.WaitForElement(searchButton);

            this.WaitForElementNotVisible("CWRefreshPanel", 10);

            if (driver.FindElement(pageHeader).Text != "Schedules Setup")
                throw new Exception("Page title do not equals: \"Schedules Setup\" ");

            return this;
        }


        public SchedulesSetupPage InsertSearchQuery(string SearchQuery)
        {
            this.SendKeys(searchTextBox, SearchQuery);

            return this;
        }

        public SchedulesSetupPage ClickSearchButton()
        {
            this.Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public SchedulesSetupPage CliickOnRecord(string RecordID)
        {
            this.Click(scheduleSetupRecord(RecordID));

            return this;
        }

        public SchedulesSetupPage ClickAddButton()
        {
            this.WaitForElementToBeClickable(addButton);

            this.Click(addButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

    }
}

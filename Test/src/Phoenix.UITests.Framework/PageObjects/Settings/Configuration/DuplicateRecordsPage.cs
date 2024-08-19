using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class DuplicateRecordsPage : CommonMethods
    {
        public DuplicateRecordsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
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

        By DuplicateRecord(string RecordID) => By.XPath("//*[@id='" + RecordID + "']/td[2]");

        readonly By viewSelector = By.Id("CWViewSelector");


        

        public DuplicateRecordsPage WaitForDuplicateRecordsPageToLoad()
        {
            this.SwitchToDefaultFrame();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(pageHeader);
            this.WaitForElement(searchTextBox);
            this.WaitForElement(searchButton);

            this.WaitForElementNotVisible("CWRefreshPanel", 10);

            if (driver.FindElement(pageHeader).Text != "Duplicate Records")
                throw new Exception("Page title do not equals: \"Duplicate Records\" ");

            return this;
        }


        public DuplicateRecordsPage InsertSearchQuery(string SearchQuery)
        {
            this.SendKeys(searchTextBox, SearchQuery);

            return this;
        }

        public DuplicateRecordsPage ClickSearchButton()
        {
            this.Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public DuplicateRecordsPage CliickOnRecord(string RecordID)
        {
            this.Click(DuplicateRecord(RecordID));

            return this;
        }

        public DuplicateRecordsPage ClickAddButton()
        {
            this.WaitForElementToBeClickable(addButton);

            this.Click(addButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public DuplicateRecordsPage SelectViewByName(string ElementText)
        {
            SelectPicklistElementByText(viewSelector, ElementText);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
    }
}

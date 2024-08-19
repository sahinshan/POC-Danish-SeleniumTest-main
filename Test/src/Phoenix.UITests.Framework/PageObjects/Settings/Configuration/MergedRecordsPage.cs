using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class MergedRecordsPage : CommonMethods
    {
        public MergedRecordsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
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

        By MergeRecord(string RecordID) => By.XPath("//*[@id='" + RecordID + "']/td[2]");

        readonly By viewSelector = By.Id("CWViewSelector");


        

        public MergedRecordsPage WaitForMergedRecordsPageToLoad()
        {
            this.SwitchToDefaultFrame();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(pageHeader);
            this.WaitForElement(searchTextBox);
            this.WaitForElement(searchButton);

            this.WaitForElementNotVisible("CWRefreshPanel", 10);

            if (driver.FindElement(pageHeader).Text != "Merged Records")
                throw new Exception("Page title do not equals: \"Merged Records\" ");

            return this;
        }


        public MergedRecordsPage InsertSearchQuery(string SearchQuery)
        {
            this.SendKeys(searchTextBox, SearchQuery);

            return this;
        }

        public MergedRecordsPage ClickSearchButton()
        {
            this.Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public MergedRecordsPage CliickOnRecord(string RecordID)
        {
            this.Click(MergeRecord(RecordID));

            return this;
        }

        public MergedRecordsPage ClickAddButton()
        {
            this.WaitForElementToBeClickable(addButton);

            this.Click(addButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public MergedRecordsPage SelectViewByName(string ElementText)
        {
            SelectPicklistElementByText(viewSelector, ElementText);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
    }
}

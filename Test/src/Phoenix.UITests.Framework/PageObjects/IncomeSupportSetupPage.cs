using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class IncomeSupportSetupPage : CommonMethods
    {
        public IncomeSupportSetupPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
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

        By chargingRulesSetupRecord(string RecordID) => By.XPath("//*[@id='" + RecordID + "']/td[2]");


        

        public IncomeSupportSetupPage WaitForIncomeSupportSetupPageToLoad()
        {
            this.driver.SwitchTo().DefaultContent();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(pageHeader);
            this.WaitForElement(searchTextBox);
            this.WaitForElement(searchButton);

            this.WaitForElementNotVisible("CWRefreshPanel", 10);

            if (driver.FindElement(pageHeader).Text != "Income Support Setup")
                throw new Exception("Page title do not equals: \"Income Support Setup\" ");

            return this;
        }


        public IncomeSupportSetupPage InsertSearchQuery(string SearchQuery)
        {
            this.SendKeys(searchTextBox, SearchQuery);

            return this;
        }

        public IncomeSupportSetupPage ClickSearchButton()
        {
            this.Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public IncomeSupportSetupPage ClickOnRecord(string RecordID)
        {
            this.Click(chargingRulesSetupRecord(RecordID));

            return this;
        }

        public IncomeSupportSetupPage ClickAddButton()
        {
            this.WaitForElementToBeClickable(addButton);

            this.Click(addButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

    }
}

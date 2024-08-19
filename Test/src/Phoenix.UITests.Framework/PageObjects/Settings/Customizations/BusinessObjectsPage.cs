using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class BusinessObjectsPage : CommonMethods
    {
        public BusinessObjectsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By quickSearchTextbox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");

        public BusinessObjectsPage WaitForBusinessObjectsPageToLoad()
        {
            this.SwitchToDefaultFrame();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(pageHeader);

            this.ValidateElementText(pageHeader, "Business Objects");

            this.WaitForElement(quickSearchTextbox);

            this.WaitForElement(quickSearchButton);

            this.WaitForElement(refreshButton);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public BusinessObjectsPage InsertQuickSearchText(string Text)
        {
            this.SendKeys(quickSearchTextbox, Text);

            return this;
        }

        public BusinessObjectsPage ClickQuickSearchButton()
        {
            this.Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public BusinessObjectsPage OpenRecord(string RecordID)
        {
            this.Click(recordRow(RecordID));

            return this;
        }

        //verify recordrow is present or not present
        public BusinessObjectsPage ValidateRecordIsPresent(string RecordID, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(recordRow(RecordID));
            else
                WaitForElementNotVisible(recordRow(RecordID), 3);

            return this;
        }


    }
}

using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class FileDestructionGDPRPage : CommonMethods
    {
        public FileDestructionGDPRPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
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


        

        public FileDestructionGDPRPage WaitForFileDestructionGDPRPageToLoad()
        {
            this.SwitchToDefaultFrame();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(pageHeader);
            this.WaitForElement(searchTextBox);
            this.WaitForElement(searchButton);

            this.WaitForElementNotVisible("CWRefreshPanel", 10);

            if (driver.FindElement(pageHeader).Text != "File Destruction (GDPR)")
                throw new Exception("Page title do not equals: \"File Destruction (GDPR)\" ");

            return this;
        }


        public FileDestructionGDPRPage InsertSearchQuery(string SearchQuery)
        {
            this.SendKeys(searchTextBox, SearchQuery);

            return this;
        }

        public FileDestructionGDPRPage ClickSearchButton()
        {
            this.Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public FileDestructionGDPRPage CliickOnRecord(string RecordID)
        {
            this.Click(MergeRecord(RecordID));

            return this;
        }

        public FileDestructionGDPRPage ClickAddButton()
        {
            this.WaitForElementToBeClickable(addButton);

            this.Click(addButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public FileDestructionGDPRPage SelectViewByName(string ElementText)
        {
            SelectPicklistElementByText(viewSelector, ElementText);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
    }
}

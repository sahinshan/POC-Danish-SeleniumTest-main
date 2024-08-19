using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Settings
{
    public class AboutMeSetupPage : CommonMethods
    {
        public AboutMeSetupPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By pageHeader = By.XPath("//h1");

        #region Option toolbar
        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By assignButton = By.Id("TI_AssignRecordButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        #endregion

        #region Search panel
        readonly By viewsPicklist = By.Id("CWViewSelector");

        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By noRecordMessage = By.XPath("//*[@id='CWGridHolder']/div/h2");
        readonly By refreshButton = By.Id("CWRefreshButton");
        #endregion

        #region Records grid
        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        By recordRowUsingName(string name) => By.XPath("//table[@id='CWGrid']/tbody/tr/td[@title = "+name+"]");
        #endregion

        public AboutMeSetupPage WaitForAboutMeSetupPageToLoad()
        {
            SwitchToDefaultFrame();
            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(pageHeader);
            

            WaitForElement(newRecordButton);
            WaitForElement(deleteButton);
            WaitForElement(viewsPicklist);
            WaitForElement(quickSearchTextBox);
            WaitForElement(quickSearchButton);
            WaitForElement(refreshButton);


            return this;
        }

        public AboutMeSetupPage TypeSearchQuery(string Query)
        {
            SendKeys(quickSearchTextBox, Query);
            Click(quickSearchButton);
            return this;
        }
        public AboutMeSetupPage ClickRefreshButton()
        {
            WaitForElement(refreshButton);
            Click(refreshButton);

            return this;
        }
        public AboutMeSetupPage SearchAboutMeSetupRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            WaitForElement(quickSearchButton);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }
        public AboutMeSetupPage OpenAboutMeSetupRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId), 7);
            MoveToElementInPage(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }


        public AboutMeSetupPage SelectAvailableViewByText(string PicklistText)
        {
            WaitForElement(viewsPicklist);
            SelectPicklistElementByText(viewsPicklist, PicklistText);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public AboutMeSetupPage ClickNewRecordButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);

            return this;
        }


        public AboutMeSetupPage ValidateNoRecordMessageVisible(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(noRecordMessage);

            }
            else
            {
                WaitForElementNotVisible(noRecordMessage, 5);
            }
            return this;
        }

        public AboutMeSetupPage ValidateAboutMeSetupRecordAvailable(string aboutMeSetupId, string expectedAboutMeSetup)
        {
            WaitForElement(recordRow(aboutMeSetupId));
            string recordTitle = GetElementByAttributeValue(recordRow(aboutMeSetupId), "title");
            Assert.IsTrue(recordTitle.Contains(expectedAboutMeSetup));            

            return this;
        }

    }
}

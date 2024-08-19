using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class LeavesAWOLPage : CommonMethods
    {
        public LeavesAWOLPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By caseFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=case&')]");
        readonly By CWNavItem_InpatientLeaveAwolFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Leave/AWOL']");

        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By viewSelector = By.Id("CWViewSelector");


        readonly By NoRecordLabel = By.XPath("//div[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");


       


        public LeavesAWOLPage WaitForLeavesAWOLPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(caseFrame);
            SwitchToIframe(caseFrame);

            WaitForElement(CWNavItem_InpatientLeaveAwolFrame);
            SwitchToIframe(CWNavItem_InpatientLeaveAwolFrame);

            WaitForElement(pageHeader);

            return this;
        }

        public LeavesAWOLPage SearchLeaveAWOLRecord(string SearchQuery, string PersonID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow(PersonID));

            return this;
        }

        public LeavesAWOLPage SearchLeaveAWOLRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public LeavesAWOLPage OpenLeaveAWOLRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }

        public LeavesAWOLPage SelectLeaveAWOLRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public LeavesAWOLPage ClickNewRecordButton()
        {
            Click(NewRecordButton);

            return this;
        }
        public LeavesAWOLPage ValidateNewRecordButtonNotVisible()
        {
            WaitForElementNotVisible(NewRecordButton, 5);

            return this;
        }

        public LeavesAWOLPage SelectViewSelector(string ExpectedViewSelector)
        {
            WaitForElementToBeClickable(viewSelector);
            SelectPicklistElementByText(viewSelector, ExpectedViewSelector);

            return this;
        }

        public LeavesAWOLPage ValidateNoRecordMessageVisibile(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(NoRecordMessage);

            }
            else
            {
                WaitForElementNotVisible(NoRecordMessage, 5);
            }
            return this;
        }

        public LeavesAWOLPage ValidateRecordVisible(string RecordId)
        {
            WaitForElementVisible(recordRow(RecordId));

            return this;
        }

    }
}

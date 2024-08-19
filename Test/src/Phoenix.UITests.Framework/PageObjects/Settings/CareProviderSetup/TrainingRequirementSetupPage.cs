using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Settings.CareProviderSetup
{
    public class TrainingRequirementSetupPage : CommonMethods
    {
        public TrainingRequirementSetupPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Search View Panel

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']//h1");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");

        readonly By quickSearchTextbox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        readonly By viewSelector = By.Id("CWViewSelector");

        readonly By recordId_HeaderLink = By.XPath("//*[@id='CWGridHeaderRow']/th//a[contains(@title, 'Record Id')]");

        #endregion

        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");

        public TrainingRequirementSetupPage WaitForTrainingRequirementSetupPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(pageHeader);

            WaitForElementVisible(newRecordButton);
            WaitForElementVisible(exportToExcelButton);
            WaitForElementVisible(deleteRecordButton);

            ValidateElementText(pageHeader, "Training Requirement Setup");

            ScrollToElement(recordId_HeaderLink);
            WaitForElementVisible(recordId_HeaderLink);

            return this;
        }

        public TrainingRequirementSetupPage ClickRecordIdHeaderLink()
        {
            WaitForElement(recordId_HeaderLink);
            ScrollToElement(recordId_HeaderLink);
            Click(recordId_HeaderLink);

            return this;
        }

        public TrainingRequirementSetupPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);

            return this;
        }

        public TrainingRequirementSetupPage InsertQuickSearchText(string Text)
        {
            WaitForElementToBeClickable(quickSearchTextbox);
            SendKeys(quickSearchTextbox, Text);

            return this;
        }

        public TrainingRequirementSetupPage ClickQuickSearchButton()
        {
            WaitForElementToBeClickable(quickSearchButton);
            Click(quickSearchButton);

            return this;
        }

        public TrainingRequirementSetupPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            MoveToElementInPage(refreshButton);
            Click(refreshButton);

            return this;
        }

        public TrainingRequirementSetupPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordRow(RecordID));
            MoveToElementInPage(recordRow(RecordID));
            Click(recordRow(RecordID));

            return this;
        }

        public TrainingRequirementSetupPage ValidateRecordIsPresent(string RecordID)
        {
            WaitForElementVisible(recordRow(RecordID));
            bool ExpectedVisible = GetElementVisibility(recordRow(RecordID));
            Assert.IsTrue(ExpectedVisible);

            return this;
        }

        public TrainingRequirementSetupPage ValidateRecordIsNotPresent(string RecordID)
        {
            WaitForElementNotVisible(recordRow(RecordID), 7);
            bool ExpectedVisible = GetElementVisibility(recordRow(RecordID));
            Assert.IsFalse(ExpectedVisible);

            return this;
        }

        public TrainingRequirementSetupPage SelectView(string ViewToSelect)
        {
            WaitForElementVisible(viewSelector);
            MoveToElementInPage(viewSelector);
            SelectPicklistElementByText(viewSelector, ViewToSelect);

            return this;
        }

        public TrainingRequirementSetupPage ValidateSelectedViewElementText(string ViewElementText)
        {
            WaitForElementVisible(viewSelector);
            ValidatePicklistContainsElementByText(viewSelector, ViewElementText);

            return this;
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class FinanceTransactionTriggersPage : CommonMethods
    {
        public FinanceTransactionTriggersPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.XPath("//iframe[contains(@id,'CWContentIFrame')][contains(@src,'type=financetransactiontrigger')]");

        

        readonly By FinanceTransactionTriggersPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Finance Transaction Triggers']");

        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By BookmarkDialogButton = By.Id("TI_OpenBookmarkDialog");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By RefreshButton = By.Id("CWRefreshButton");
        readonly By searchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");

        readonly By noRecordsMessage = By.XPath("//*[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By noResultsMessage = By.XPath("//*[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");

        By recordRowCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");

        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");


        public FinanceTransactionTriggersPage WaitForPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(FinanceTransactionTriggersPageHeader);
            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(AssignRecordButton);
            WaitForElementVisible(DeleteRecordButton);
            WaitForElementVisible(BookmarkDialogButton);

            return this;
        }

        

        public FinanceTransactionTriggersPage SelectSystemView(string ExpectedTextToSelect)
        {
            WaitForElementToBeClickable(viewSelector);
            MoveToElementInPage(viewSelector);
            SelectPicklistElementByText(viewSelector, ExpectedTextToSelect);

            return this;
        }

        public FinanceTransactionTriggersPage ValidateSelectedSystemView(string ExpectedText)
        {
            WaitForElementToBeClickable(viewSelector);
            MoveToElementInPage(viewSelector);
            ValidatePicklistSelectedText(viewSelector, ExpectedText);

            return this;
        }

        public FinanceTransactionTriggersPage InsertSearchQuery(string SearchQuery)
        {
            WaitForElementToBeClickable(searchTextBox);
            MoveToElementInPage(searchTextBox);
            SendKeys(searchTextBox, SearchQuery);

            return this;
        }

        public FinanceTransactionTriggersPage TapSearchButton()
        {
            WaitForElementToBeClickable(searchButton);
            MoveToElementInPage(searchButton);
            Click(searchButton);

            return this;
        }

        public FinanceTransactionTriggersPage SelectFinanceInvoiceBatchSetupRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            MoveToElementInPage(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public FinanceTransactionTriggersPage ValidateRecordCellContent(string RecordId, int CellPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(recordRowCell(RecordId, CellPosition));
            ValidateElementText(recordRowCell(RecordId, CellPosition), ExpectedText);

            return this;
        }

        public FinanceTransactionTriggersPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(ExportToExcelButton);
            MoveToElementInPage(ExportToExcelButton);
            Click(ExportToExcelButton);

            return this;
        }

        public FinanceTransactionTriggersPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            MoveToElementInPage(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public FinanceTransactionTriggersPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            MoveToElementInPage(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }

        public FinanceTransactionTriggersPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID));
            MoveToElementInPage(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return this;
        }

        public FinanceTransactionTriggersPage OpenRecord(Guid RecordID)
        {
            return OpenRecord(RecordID.ToString());
        }

        public FinanceTransactionTriggersPage ValidateRecordIsPresent(string RecordID, bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementToBeClickable(RecordIdentifier(RecordID));
                MoveToElementInPage(RecordIdentifier(RecordID));
            }
            else
                WaitForElementNotVisible(RecordIdentifier(RecordID), 3);

            Assert.AreEqual(ExpectedVisible, GetElementVisibility(RecordIdentifier(RecordID)));

            return this;

        }

        public FinanceTransactionTriggersPage ValidateNoRecordsMessageVisible()
        {
            WaitForElementVisible(noRecordsMessage);
            WaitForElementVisible(noResultsMessage);

            return this;
        }

        public FinanceTransactionTriggersPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(RefreshButton);
            MoveToElementInPage(RefreshButton);
            Click(RefreshButton);

            return this;
        }

    }
}

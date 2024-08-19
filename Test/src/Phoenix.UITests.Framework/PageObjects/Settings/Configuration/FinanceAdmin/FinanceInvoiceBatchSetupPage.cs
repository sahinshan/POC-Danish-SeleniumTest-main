﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class FinanceInvoiceBatchSetupPage : CommonMethods
    {
        public FinanceInvoiceBatchSetupPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By FinanceInvoiceBatchSetupPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By RefreshButton = By.Id("CWRefreshButton");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By searchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");

        readonly By noRecordsMessage = By.XPath("//*[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By noResultsMessage = By.XPath("//*[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");

        By recordRowCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");

        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");


        public FinanceInvoiceBatchSetupPage WaitForFinanceInvoiceBatchSetupPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(FinanceInvoiceBatchSetupPageHeader);
            WaitForElementVisible(NewRecordButton);
            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(AssignRecordButton);
            WaitForElementVisible(DeleteRecordButton);

            return this;
        }

        public FinanceInvoiceBatchSetupPage SelectSystemView(string ExpectedTextToSelect)
        {
            WaitForElementToBeClickable(viewSelector);
            MoveToElementInPage(viewSelector);
            SelectPicklistElementByText(viewSelector, ExpectedTextToSelect);

            return this;
        }

        public FinanceInvoiceBatchSetupPage ValidateSelectedSystemView(string ExpectedText)
        {
            WaitForElementToBeClickable(viewSelector);
            MoveToElementInPage(viewSelector);
            ValidatePicklistSelectedText(viewSelector, ExpectedText);

            return this;
        }

        public FinanceInvoiceBatchSetupPage InsertSearchQuery(string SearchQuery)
        {
            WaitForElementToBeClickable(searchTextBox);
            MoveToElementInPage(searchTextBox);
            SendKeys(searchTextBox, SearchQuery);

            return this;
        }

        public FinanceInvoiceBatchSetupPage TapSearchButton()
        {
            WaitForElementToBeClickable(searchButton);
            MoveToElementInPage(searchButton);
            Click(searchButton);

            return this;
        }

        public FinanceInvoiceBatchSetupPage SelectFinanceInvoiceBatchSetupRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            MoveToElementInPage(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public FinanceInvoiceBatchSetupPage ValidateRecordCellContent(string RecordId, int CellPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(recordRowCell(RecordId, CellPosition));
            ValidateElementText(recordRowCell(RecordId, CellPosition), ExpectedText);

            return this;
        }

        public FinanceInvoiceBatchSetupPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            MoveToElementInPage(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public FinanceInvoiceBatchSetupPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(ExportToExcelButton);
            MoveToElementInPage(ExportToExcelButton);
            Click(ExportToExcelButton);

            return this;
        }

        public FinanceInvoiceBatchSetupPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            MoveToElementInPage(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public FinanceInvoiceBatchSetupPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            MoveToElementInPage(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }

        public FinanceInvoiceBatchSetupPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID));
            MoveToElementInPage(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return this;
        }

        public FinanceInvoiceBatchSetupPage ValidateRecordIsPresent(string RecordID, bool ExpectedVisible)
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

        public FinanceInvoiceBatchSetupPage ValidateNoRecordsMessageVisible()
        {
            WaitForElementVisible(noRecordsMessage);
            WaitForElementVisible(noResultsMessage);

            return this;
        }

        public FinanceInvoiceBatchSetupPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(RefreshButton);
            MoveToElementInPage(RefreshButton);
            Click(RefreshButton);

            return this;
        }

    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CPFinanceExtractBatchesPage : CommonMethods
    {
        public CPFinanceExtractBatchesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By NewRecordButton = By.XPath("//*[@id='TI_NewRecordButton']");
        readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
        readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");
        readonly By MailMerge_CreateInvoice_Button = By.XPath("//*[@id = 'TI_CreateInvoiceFile']");

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By CPFinanceExtractBatchesPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By OpenBookmarkDialog = By.Id("TI_OpenBookmarkDialog");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By searchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        readonly By noRecordsMessage = By.XPath("//*[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By noResultsMessage = By.XPath("//*[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");

        By recordRowCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");

        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");

        By gridPageHeaderElement(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");

        By gridPageHeaderElement(string ColumnName) => By.XPath("//*[@id='CWGridHeaderRow']/th/a/span[text() = '" + ColumnName + "']");

        By gridPageHeaderElementSortOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/a/span[2]");

        By gridPageHeaderElementSortAscendingOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/a/span[2][@class = 'sortasc']");
        By gridPageHeaderElementSortDescendingOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/a/span[2][@class = 'sortdesc']");
        By gridPageHeaderElementSortAscendingOrder(string FieldName) => By.XPath("//*[@id='CWGridHeaderRow']/th/a[@title = 'Sort by " + FieldName + "']/span[2][@class = 'sortasc']");
        By gridPageHeaderElementSortDescendingOrder(string FieldName) => By.XPath("//*[@id='CWGridHeaderRow']/th/a[@title = 'Sort by " + FieldName + "']/span[2][@class = 'sortdesc']");


        public CPFinanceExtractBatchesPage WaitForCPFinanceExtractBatchesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 45);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 45);

            WaitForElementVisible(CPFinanceExtractBatchesPageHeader);
            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(OpenBookmarkDialog);

            WaitForElementNotVisible("CWRefreshPanel", 45);

            return this;
        }

        public CPFinanceExtractBatchesPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public CPFinanceExtractBatchesPage ValidateNewRecordButtonIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(NewRecordButton);
            else
                WaitForElementNotVisible(NewRecordButton, 3);

            return this;
        }        

        public CPFinanceExtractBatchesPage ClickMailMergeButton()
        {
            WaitForElementToBeClickable(MailMerge_CreateInvoice_Button);
            Click(MailMerge_CreateInvoice_Button);

            return this;
        }

        public CPFinanceExtractBatchesPage ValidateMailMergeButtonIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(MailMerge_CreateInvoice_Button);
            else
                WaitForElementNotVisible(MailMerge_CreateInvoice_Button, 3);

            return this;
        }

        public CPFinanceExtractBatchesPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public CPFinanceExtractBatchesPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }

        public CPFinanceExtractBatchesPage ValidateExportToExcelButtonIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(ExportToExcelButton);
            else
                WaitForElementNotVisible(ExportToExcelButton, 3);

            return this;
        }

        public CPFinanceExtractBatchesPage SelectSystemView(string ExpectedTextToSelect)
        {
            WaitForElementToBeClickable(viewSelector);
            MoveToElementInPage(viewSelector);
            SelectPicklistElementByText(viewSelector, ExpectedTextToSelect);

            return this;
        }

        public CPFinanceExtractBatchesPage ValidateSelectedSystemView(string ExpectedText)
        {
            WaitForElementToBeClickable(viewSelector);
            MoveToElementInPage(viewSelector);
            ValidatePicklistSelectedText(viewSelector, ExpectedText);

            return this;
        }

        public CPFinanceExtractBatchesPage ValidateSystemViewOptionNotPresent(string optionNotPresent)
        {
            MoveToElementInPage(viewSelector);
            ValidatePicklistDoesNotContainsElementByText(viewSelector, optionNotPresent);
            return this;
        }

        public CPFinanceExtractBatchesPage InsertSearchQuery(string SearchQuery)
        {
            WaitForElementToBeClickable(searchTextBox);
            ScrollToElement(searchTextBox);
            SendKeys(searchTextBox, SearchQuery);

            return this;
        }

        public CPFinanceExtractBatchesPage ClickSearchButton()
        {
            WaitForElementToBeClickable(searchButton);
            ScrollToElement(searchButton);
            Click(searchButton);

            return this;
        }

        public CPFinanceExtractBatchesPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            MoveToElementInPage(refreshButton);
            Click(refreshButton);

            return this;
        }

        public CPFinanceExtractBatchesPage SelectFinanceInvoiceBatchSetupRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            MoveToElementInPage(recordRowCheckBox(RecordId));

            if (GetElementByAttributeValue(recordRowCheckBox(RecordId), "class") != "selrow")
                Click(recordRowCheckBox(RecordId));

            return this;
        }

        public CPFinanceExtractBatchesPage SelectFinanceInvoiceBatchSetupRecord(Guid RecordId)
        {
            return SelectFinanceInvoiceBatchSetupRecord(RecordId.ToString());
        }


        public CPFinanceExtractBatchesPage ValidateRecordCellContent(string RecordId, int CellPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(recordRowCell(RecordId, CellPosition));
            ValidateElementText(recordRowCell(RecordId, CellPosition), ExpectedText);

            return this;
        }

        public CPFinanceExtractBatchesPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(ExportToExcelButton);
            MoveToElementInPage(ExportToExcelButton);
            Click(ExportToExcelButton);

            return this;
        }

        public CPFinanceExtractBatchesPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID));
            MoveToElementInPage(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return this;
        }

        public CPFinanceExtractBatchesPage OpenRecord(Guid RecordID)
        {
            return OpenRecord(RecordID.ToString());
        }

        public CPFinanceExtractBatchesPage SelectRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordID));
            MoveToElementInPage(recordRowCheckBox(RecordID));
            Click(recordRowCheckBox(RecordID));

            return this;
        }

        public CPFinanceExtractBatchesPage SelectRecord(Guid RecordID)
        {
            return SelectRecord(RecordID.ToString());
        }

        public CPFinanceExtractBatchesPage RecordsPageValidateHeaderCellText(int CellPosition, string ExpectedText)
        {
            MoveToElementInPage(gridPageHeaderElement(CellPosition));
            WaitForElementVisible(gridPageHeaderElement(CellPosition));
            ValidateElementText(gridPageHeaderElement(CellPosition), ExpectedText);

            return this;
        }

        public CPFinanceExtractBatchesPage ClickColumnHeader(int CellPosition)
        {
            ScrollToElement(gridPageHeaderElement(CellPosition));
            WaitForElementVisible(gridPageHeaderElement(CellPosition));
            Click(gridPageHeaderElement(CellPosition));

            return this;
        }

        public CPFinanceExtractBatchesPage ClickColumnHeader(string ColumnName)
        {
            ScrollToElement(gridPageHeaderElement(ColumnName));
            WaitForElementVisible(gridPageHeaderElement(ColumnName));
            Click(gridPageHeaderElement(ColumnName));

            return this;
        }

        public CPFinanceExtractBatchesPage ClickColumnHeaderSortDescendingOrder(int CellPosition)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
            MoveToElementInPage(gridPageHeaderElementSortOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
            Click(gridPageHeaderElementSortAscendingOrder(CellPosition));

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public CPFinanceExtractBatchesPage ClickColumnHeaderSortDescendingOrder(string ColumnName)
        {
            WaitForElement(gridPageHeaderElementSortAscendingOrder(ColumnName));
            MoveToElementInPage(gridPageHeaderElementSortAscendingOrder(ColumnName));
            WaitForElementToBeClickable(gridPageHeaderElementSortAscendingOrder(ColumnName));
            Click(gridPageHeaderElementSortAscendingOrder(ColumnName));

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public CPFinanceExtractBatchesPage ClickColumnHeaderSortAscendingOrder(string ColumnName)
        {
            WaitForElement(gridPageHeaderElementSortDescendingOrder(ColumnName));
            MoveToElementInPage(gridPageHeaderElementSortDescendingOrder(ColumnName));
            WaitForElementToBeClickable(gridPageHeaderElementSortDescendingOrder(ColumnName));
            Click(gridPageHeaderElementSortDescendingOrder(ColumnName));

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public CPFinanceExtractBatchesPage ResultsPageValidateHeaderCellSortAscendingOrder(int CellPosition)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
            ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortasc");

            return this;
        }

        public CPFinanceExtractBatchesPage ClickColumnHeaderSortAscendingOrder(int CellPosition)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
            Click(gridPageHeaderElementSortDescendingOrder(CellPosition));

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public CPFinanceExtractBatchesPage ResultsPageValidateHeaderCellSortDescendingOrder(int CellPosition)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
            ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortdesc");

            return this;
        }

        public CPFinanceExtractBatchesPage ValidateRecordIsPresent(string RecordID, bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementToBeClickable(RecordIdentifier(RecordID));
                ScrollToElement(RecordIdentifier(RecordID));
            }
            else
                WaitForElementNotVisible(RecordIdentifier(RecordID), 3);

            Assert.AreEqual(ExpectedVisible, GetElementVisibility(RecordIdentifier(RecordID)));

            return this;

        }

        public CPFinanceExtractBatchesPage ValidateNoRecordsMessageVisible()
        {
            WaitForElementVisible(noRecordsMessage);
            WaitForElementVisible(noResultsMessage);

            return this;
        }     

    }
}

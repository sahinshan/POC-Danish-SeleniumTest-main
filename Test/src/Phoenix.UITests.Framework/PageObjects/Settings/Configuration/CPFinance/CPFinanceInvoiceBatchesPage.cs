using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CPFinanceInvoiceBatchesPage : CommonMethods
    {
        public CPFinanceInvoiceBatchesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By NewRecordButton = By.XPath("//*[@id='TI_NewRecordButton']");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By BulkEditButton = By.XPath("//*[@id='TI_BulkEditButton']");
        readonly By OpenBookmarkDialog = By.Id("TI_OpenBookmarkDialog");

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame_financeInvoiceBatchSetup = By.XPath("//iframe[contains(@id, 'iframe_CWDialog')][contains(@src, 'type=careproviderfinanceinvoicebatchsetup&')]");
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By CPFinanceInvoiceBatchesPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");              

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By searchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        readonly By noRecordsMessage = By.XPath("//*[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By noResultsMessage = By.XPath("//*[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");

        By recordRowCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");

        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");

        By RecordIdentifier(int Row, string RecordID) => By.XPath("//tr["+Row+"][@id='" + RecordID + "']/td[2]");

        By gridPageHeaderSpanElement(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[1]");
        By gridPageHeaderElement(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");

        By gridPageHeaderElement(string ColumnName) => By.XPath("//*[@id='CWGridHeaderRow']/th//a/span[text() = '" + ColumnName + "']");

        By gridPageHeaderElementSortOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[2]");

        By gridPageHeaderElementSortAscendingOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[2][@class = 'sortasc']");
        By gridPageHeaderElementSortDescendingOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[2][@class = 'sortdesc']");
        By gridPageHeaderElementSortAscendingOrder(string FieldName) => By.XPath("//*[@id='CWGridHeaderRow']/th//a[@title = 'Sort by " + FieldName + "']//span[2][@class = 'sortasc']");
        By gridPageHeaderElementSortDescendingOrder(string FieldName) => By.XPath("//*[@id='CWGridHeaderRow']/th//a[@title = 'Sort by " + FieldName + "']//span[2][@class = 'sortdesc']");

        By cwDialog_IFrame(string parentRecordIdSuffix) => By.XPath("//iframe[contains(@id, 'iframe_CWDialog_" + parentRecordIdSuffix + "')]");

        By cwDialog_TypeId(string parentRecordBoName) => By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=" + parentRecordBoName + "')]");

        readonly By urlIFrame = By.Id("CWUrlPanel_IFrame");


        public CPFinanceInvoiceBatchesPage WaitForCPFinanceInvoiceBatchesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 45);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 45);

            WaitForElementVisible(CPFinanceInvoiceBatchesPageHeader);
            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(OpenBookmarkDialog);

            return this;
        }

        public CPFinanceInvoiceBatchesPage WaitForCPFinanceInvoiceBatchesPageToLoad(string parentRecordIdSuffix, bool IsInvoiceRecord = true)
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            if (IsInvoiceRecord)
            {
                WaitForElement(cwDialog_IFrame(parentRecordIdSuffix));
                SwitchToIframe(cwDialog_IFrame(parentRecordIdSuffix));
            }
            else
            {
                WaitForElement(cwDialog_TypeId(parentRecordIdSuffix));
                SwitchToIframe(cwDialog_TypeId(parentRecordIdSuffix));
            }

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(urlIFrame);
            SwitchToIframe(urlIFrame);


            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public CPFinanceInvoiceBatchesPage WaitForPageToLoad_FromFIBSRecord()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(cwDialogIFrame_financeInvoiceBatchSetup);
            SwitchToIframe(cwDialogIFrame_financeInvoiceBatchSetup);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElementVisible(CPFinanceInvoiceBatchesPageHeader);
            WaitForElementVisible(ExportToExcelButton);

            return this;
        }

        public CPFinanceInvoiceBatchesPage ValidateToolbarOptionsAreVisible(bool NewRecordButtonVisible = true, bool ExportToExcelVisible = true, bool BulkEditVisible = true)
        {
            ValidateNewRecordButtonIsDisplayed(NewRecordButtonVisible);
            ValidateExportToExcelButtonIsDisplayed(ExportToExcelVisible);
            ValidateBulkEditButtonIsDisplayed(BulkEditVisible);

            return this;
        }

        public CPFinanceInvoiceBatchesPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public CPFinanceInvoiceBatchesPage ValidateNewRecordButtonIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(NewRecordButton);
            else
                WaitForElementNotVisible(NewRecordButton, 3);

            return this;
        }

        public CPFinanceInvoiceBatchesPage ClickBulkEditButton()
        {
            WaitForElementToBeClickable(BulkEditButton);
            Click(BulkEditButton);

            return this;
        }

        public CPFinanceInvoiceBatchesPage ValidateBulkEditButtonIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(BulkEditButton);
            else
                WaitForElementNotVisible(BulkEditButton, 3);

            return this;
        }

        public CPFinanceInvoiceBatchesPage ValidateExportToExcelButtonIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(ExportToExcelButton);
            else
                WaitForElementNotVisible(ExportToExcelButton, 3);

            return this;
        }

        public CPFinanceInvoiceBatchesPage SelectSystemView(string ExpectedTextToSelect)
        {
            WaitForElementToBeClickable(viewSelector);
            MoveToElementInPage(viewSelector);
            SelectPicklistElementByText(viewSelector, ExpectedTextToSelect);

            return this;
        }

        public CPFinanceInvoiceBatchesPage ValidateSelectedSystemView(string ExpectedText)
        {
            WaitForElementToBeClickable(viewSelector);
            MoveToElementInPage(viewSelector);
            ValidatePicklistSelectedText(viewSelector, ExpectedText);

            return this;
        }

        //validate system view option is present
        public CPFinanceInvoiceBatchesPage ValidateSystemViewOptionPresent(string optionPresent)
        {
            WaitForElementVisible(viewSelector);
            ValidatePicklistContainsElementByText(viewSelector, optionPresent);
            return this;
        }

        public CPFinanceInvoiceBatchesPage ValidateSystemViewOptionNotPresent(string optionNotPresent)
        {
            WaitForElementVisible(viewSelector);
            ValidatePicklistDoesNotContainsElementByText(viewSelector, optionNotPresent);
            return this;
        }

        public CPFinanceInvoiceBatchesPage InsertSearchQuery(string SearchQuery)
        {
            WaitForElementToBeClickable(searchTextBox);
            ScrollToElement(searchTextBox);
            SendKeys(searchTextBox, SearchQuery);

            return this;
        }

        public CPFinanceInvoiceBatchesPage ClickSearchButton()
        {
            WaitForElementToBeClickable(searchButton);
            ScrollToElement(searchButton);
            Click(searchButton);

            return this;
        }

        public CPFinanceInvoiceBatchesPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            ScrollToElement(refreshButton);
            Click(refreshButton);

            return this;
        }

        public CPFinanceInvoiceBatchesPage SelectFinanceInvoiceBatchRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            ScrollToElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public CPFinanceInvoiceBatchesPage SelectFinanceInvoiceBatchRecord(Guid RecordId)
        {
            return SelectFinanceInvoiceBatchRecord(RecordId.ToString());
        }


        public CPFinanceInvoiceBatchesPage ValidateRecordCellContent(string RecordId, int CellPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(recordRowCell(RecordId, CellPosition));
            ValidateElementText(recordRowCell(RecordId, CellPosition), ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchesPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(ExportToExcelButton);
            ScrollToElement(ExportToExcelButton);
            Click(ExportToExcelButton);

            return this;
        }

        public CPFinanceInvoiceBatchesPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID));
            ScrollToElement(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return this;
        }

        public CPFinanceInvoiceBatchesPage OpenRecord(Guid RecordID)
        {
            return OpenRecord(RecordID.ToString());
        }

        public CPFinanceInvoiceBatchesPage RecordsPageValidateHeaderCellText(int CellPosition, string ExpectedText)
        {
            ScrollToElement(gridPageHeaderSpanElement(CellPosition));
            WaitForElementVisible(gridPageHeaderElement(CellPosition));
            ValidateElementText(gridPageHeaderElement(CellPosition), ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchesPage ClickColumnHeader(int CellPosition)
        {
            ScrollToElement(gridPageHeaderElement(CellPosition));
            WaitForElementVisible(gridPageHeaderElement(CellPosition));
            Click(gridPageHeaderElement(CellPosition));

            return this;
        }

        public CPFinanceInvoiceBatchesPage ClickColumnHeader(string ColumnName)
        {
            ScrollToElement(gridPageHeaderElement(ColumnName));
            WaitForElementVisible(gridPageHeaderElement(ColumnName));
            Click(gridPageHeaderElement(ColumnName));

            return this;
        }

        public CPFinanceInvoiceBatchesPage ClickColumnHeaderSortDescendingOrder(int CellPosition)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
            Click(gridPageHeaderElementSortAscendingOrder(CellPosition));

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public CPFinanceInvoiceBatchesPage ClickColumnHeaderSortDescendingOrder(string ColumnName)
        {
            WaitForElement(gridPageHeaderElementSortAscendingOrder(ColumnName));
            ScrollToElement(gridPageHeaderElementSortAscendingOrder(ColumnName));
            WaitForElementToBeClickable(gridPageHeaderElementSortAscendingOrder(ColumnName));
            Click(gridPageHeaderElementSortAscendingOrder(ColumnName));

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public CPFinanceInvoiceBatchesPage ClickColumnHeaderSortAscendingOrder(string ColumnName)
        {
            WaitForElement(gridPageHeaderElementSortDescendingOrder(ColumnName));
            ScrollToElement(gridPageHeaderElementSortDescendingOrder(ColumnName));
            WaitForElementToBeClickable(gridPageHeaderElementSortDescendingOrder(ColumnName));
            Click(gridPageHeaderElementSortDescendingOrder(ColumnName));

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public CPFinanceInvoiceBatchesPage ResultsPageValidateHeaderCellSortAscendingOrder(int CellPosition)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
            ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortasc");

            return this;
        }

        public CPFinanceInvoiceBatchesPage ClickColumnHeaderSortAscendingOrder(int CellPosition)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
            Click(gridPageHeaderElementSortDescendingOrder(CellPosition));

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public CPFinanceInvoiceBatchesPage ResultsPageValidateHeaderCellSortDescendingOrder(int CellPosition)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
            ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortdesc");

            return this;
        }

        public CPFinanceInvoiceBatchesPage ValidateRecordIsPresent(string RecordID, bool ExpectedVisible)
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

        public CPFinanceInvoiceBatchesPage ValidateNoRecordsMessageVisible()
        {
            WaitForElementVisible(noRecordsMessage);
            WaitForElementVisible(noResultsMessage);

            return this;
        }

        //validate record is present based on the row number
        public CPFinanceInvoiceBatchesPage ValidateRecordIsPresent(int Row, string RecordID, bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementToBeClickable(RecordIdentifier(Row, RecordID));
                ScrollToElement(RecordIdentifier(Row, RecordID));
            }
            else
                WaitForElementNotVisible(RecordIdentifier(Row, RecordID), 3);

            Assert.AreEqual(ExpectedVisible, GetElementVisibility(RecordIdentifier(Row, RecordID)));

            return this;

        }

    }
}

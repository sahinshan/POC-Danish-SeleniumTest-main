using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Configuration.CPFinanceAdmin
{
    public class CPFinanceTransactionTriggersPage : CommonMethods
    {
        public CPFinanceTransactionTriggersPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By CWDialogIframe = By.XPath("//iframe[contains(@id, 'iframe_CWDialog')][contains(@src, 'type=careproviderfinancetransactiontrigger&')]");
        By iframe_CWDialog_(string ParentBOName) => By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=" + ParentBOName + "')]");
        readonly By CWUrlIframe = By.Id("CWUrlPanel_IFrame");



        readonly By FinanceInvoiceBatchSetupPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By NewRecordButton = By.XPath("//*[@id='TI_NewRecordButton']");
        readonly By ExportToExcelButton = By.XPath("//*[@id='TI_ExportToExcelButton']");
        readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
        readonly By BulkUpdateButton = By.XPath("//*[@id='TI_BulkEditButton']");
        readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");
        readonly By AdditionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By ToolBarMenu = By.Id("CWToolbarMenu");
        readonly By viewSelector = By.XPath("//*[@id='CWViewSelector']");
        readonly By searchTextBox = By.XPath("//*[@id='CWQuickSearch']");
        readonly By searchButton = By.XPath("//*[@id='CWQuickSearchButton']");
        readonly By RefreshButton = By.XPath("//*[@id='CWRefreshButton']");

        By RecordCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");
        By RecordCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");
        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");

        By RecordIdentifier(int RecordPosition, string RecordID) => By.XPath("//tr[" + RecordPosition + "][@id='" + RecordID + "']/td[2]");

        By gridPageHeaderElement(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");
        By gridPageHeaderSortIcon(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[2]");
        By gridPageHeaderElement(string ColumnName) => By.XPath("//*[@id='CWGridHeaderRow']/th/a/span[text() = '" + ColumnName + "']");

        By gridPageHeaderElementSortOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/a/span[2]");
        By gridPageHeaderElementSortOrder(int HeaderCellPosition, string ColumnName) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/a/span[text() = '" + ColumnName + "']");


        By gridPageHeaderElementSortAscendingOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/a/span[2][@class = 'sortasc']");
        By gridPageHeaderElementSortDescendingOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/a/span[2][@class = 'sortdesc']");
        By gridPageHeaderElementSortAscendingOrder(string FieldName) => By.XPath("//*[@id='CWGridHeaderRow']/th/a[@title = 'Sort by " + FieldName + "']/span[2][@class = 'sortasc']");
        By gridPageHeaderElementSortDescendingOrder(string FieldName) => By.XPath("//*[@id='CWGridHeaderRow']/th/a[@title = 'Sort by " + FieldName + "']/span[2][@class = 'sortdesc']");

        readonly By noRecordsMessage = By.XPath("//*[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By noResultsMessage = By.XPath("//*[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        public CPFinanceTransactionTriggersPage WaitForCPFinanceTransactionTriggersPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWDialogIframe);
            SwitchToIframe(CWDialogIframe);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWUrlIframe);
            SwitchToIframe(CWUrlIframe);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElementVisible(FinanceInvoiceBatchSetupPageHeader);

            return this;
        }

        public CPFinanceTransactionTriggersPage WaitForPageToLoad(string ParentBOName)
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(iframe_CWDialog_(ParentBOName));
            SwitchToIframe(iframe_CWDialog_(ParentBOName));

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(CWUrlIframe);
            SwitchToIframe(CWUrlIframe);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(FinanceInvoiceBatchSetupPageHeader);

            WaitForElementVisible(AssignRecordButton);

            return this;
        }

        public CPFinanceTransactionTriggersPage WaitForPageToLoad()
        {

            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElementVisible(FinanceInvoiceBatchSetupPageHeader);

            return this;
        }

        public CPFinanceTransactionTriggersPage ValidateToolbarOptionsDisplayed()
        {

            WaitForElementVisible(NewRecordButton);
            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(AssignRecordButton);
            WaitForElementVisible(BulkUpdateButton);
            WaitForElementVisible(DeleteRecordButton);

            return this;
        }

        public CPFinanceTransactionTriggersPage ClickAdditionalItemsButton()
        {
            WaitForElementToBeClickable(AdditionalItemsButton);
            Click(AdditionalItemsButton);

            return this;
        }

        public CPFinanceTransactionTriggersPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(ExportToExcelButton);
            Click(ExportToExcelButton);

            return this;
        }

        public CPFinanceTransactionTriggersPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public CPFinanceTransactionTriggersPage SelectSystemView(string ExpectedTextToSelect)
        {
            WaitForElementToBeClickable(viewSelector);
            ScrollToElement(viewSelector);
            SelectPicklistElementByText(viewSelector, ExpectedTextToSelect);

            return this;
        }

        public CPFinanceTransactionTriggersPage ValidateSelectedSystemView(string ExpectedText)
        {
            WaitForElementToBeClickable(viewSelector);
            ScrollToElement(viewSelector);
            ValidatePicklistSelectedText(viewSelector, ExpectedText);

            return this;
        }

        public CPFinanceTransactionTriggersPage ValidateSystemViewOptionNotPresent(string optionNotPresent)
        {
            ScrollToElement(viewSelector);
            ValidatePicklistDoesNotContainsElementByText(viewSelector, optionNotPresent);
            return this;
        }

        public CPFinanceTransactionTriggersPage InsertSearchQuery(string SearchQuery)
        {
            WaitForElementToBeClickable(searchTextBox);
            ScrollToElement(searchTextBox);
            SendKeys(searchTextBox, SearchQuery);

            return this;
        }

        public CPFinanceTransactionTriggersPage ClickSearchButton()
        {
            WaitForElementToBeClickable(searchButton);
            ScrollToElement(searchButton);
            Click(searchButton);

            return this;
        }

        public CPFinanceTransactionTriggersPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(RefreshButton);
            ScrollToElement(RefreshButton);
            Click(RefreshButton);

            return this;
        }

        public CPFinanceTransactionTriggersPage ValidateRecordVisible(Guid RecordID)
        {
            WaitForElementVisible(RecordCheckBox(RecordID.ToString()));

            return this;
        }

        public CPFinanceTransactionTriggersPage ValidateRecordVisible(int RecordPosition, string RecordID, bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementToBeClickable(RecordIdentifier(RecordPosition, RecordID));
                ScrollToElement(RecordIdentifier(RecordPosition, RecordID));
            }
            else
                WaitForElementNotVisible(RecordIdentifier(RecordPosition, RecordID), 3);

            return this;
        }

        public CPFinanceTransactionTriggersPage ValidateRecordNotVisible(Guid RecordID)
        {
            WaitForElementNotVisible(RecordCheckBox(RecordID.ToString()), 7);

            return this;
        }

        public CPFinanceTransactionTriggersPage SelectRecord(Guid RecordID)
        {
            WaitForElementToBeClickable(RecordCheckBox(RecordID.ToString()));
            Click(RecordCheckBox(RecordID.ToString()));

            return this;
        }

        public CPFinanceTransactionTriggersPage OpenRecord(Guid RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID.ToString()));
            ScrollToElement(RecordIdentifier(RecordID.ToString()));
            Click(RecordIdentifier(RecordID.ToString()));

            return this;
        }

        public CPFinanceTransactionTriggersPage OpenRecord(int RecordPosition, Guid RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordPosition, RecordID.ToString()));
            Click(RecordIdentifier(RecordPosition, RecordID.ToString()));

            return this;
        }

        public CPFinanceTransactionTriggersPage ValidateRecordCellText(Guid RecordID, int CellPosition, string ExpectedText)
        {
            ValidateElementText(RecordCell(RecordID.ToString(), CellPosition), ExpectedText);

            return this;
        }

        public CPFinanceTransactionTriggersPage ValidateHeaderCellText(int CellPosition, string ExpectedText)
        {
            ScrollToElement(gridPageHeaderElement(CellPosition));
            WaitForElementVisible(gridPageHeaderElement(CellPosition));
            ValidateElementText(gridPageHeaderElement(CellPosition), ExpectedText);

            return this;
        }

        public CPFinanceTransactionTriggersPage ValidateHeaderCellSortIcon(int CellPosition, bool AscendingSort)
        {
            ScrollToElement(gridPageHeaderSortIcon(CellPosition));
            WaitForElementVisible(gridPageHeaderSortIcon(CellPosition));

            if (AscendingSort)
                ValidateElementAttribute(gridPageHeaderSortIcon(CellPosition), "class", "sortasc");
            else
                ValidateElementAttribute(gridPageHeaderSortIcon(CellPosition), "class", "sortdesc");

            return this;
        }

        public CPFinanceTransactionTriggersPage SelectRecord(string RecordId)
        {
            WaitForElementToBeClickable(RecordCheckBox(RecordId));
            ScrollToElement(RecordCheckBox(RecordId));
            Click(RecordCheckBox(RecordId));

            return this;
        }

        public CPFinanceTransactionTriggersPage ValidateHeaderCellText(string ExpectedText)
        {
            ScrollToElement(gridPageHeaderElement(ExpectedText));
            WaitForElementVisible(gridPageHeaderElement(ExpectedText));
            ValidateElementText(gridPageHeaderElement(ExpectedText), ExpectedText);
            return this;
        }

        public CPFinanceTransactionTriggersPage ValidateRecordCellContent(string RecordId, int CellPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(RecordCell(RecordId, CellPosition));
            ValidateElementText(RecordCell(RecordId, CellPosition), ExpectedText);

            return this;
        }


        public CPFinanceTransactionTriggersPage ClickColumnHeader(int CellPosition)
        {
            ScrollToElement(gridPageHeaderElement(CellPosition));
            WaitForElementVisible(gridPageHeaderElement(CellPosition));
            Click(gridPageHeaderElement(CellPosition));

            return this;
        }

        public CPFinanceTransactionTriggersPage ClickColumnHeader(string ColumnName)
        {
            ScrollToElement(gridPageHeaderElement(ColumnName));
            WaitForElementVisible(gridPageHeaderElement(ColumnName));
            Click(gridPageHeaderElement(ColumnName));

            return this;
        }

        public CPFinanceTransactionTriggersPage ClickColumnHeaderSortDescendingOrder(int CellPosition)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
            Click(gridPageHeaderElementSortAscendingOrder(CellPosition));

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public CPFinanceTransactionTriggersPage ClickColumnHeaderSortDescendingOrder(string ColumnName)
        {
            WaitForElement(gridPageHeaderElementSortAscendingOrder(ColumnName));
            ScrollToElement(gridPageHeaderElementSortAscendingOrder(ColumnName));
            WaitForElementToBeClickable(gridPageHeaderElementSortAscendingOrder(ColumnName));
            Click(gridPageHeaderElementSortAscendingOrder(ColumnName));

            WaitForElementNotVisible("CWRefreshPanel", 7);
            return this;
        }


        public CPFinanceTransactionTriggersPage ClickColumnHeaderSortAscendingOrder(string ColumnName)
        {
            WaitForElement(gridPageHeaderElementSortDescendingOrder(ColumnName));
            ScrollToElement(gridPageHeaderElementSortDescendingOrder(ColumnName));
            WaitForElementToBeClickable(gridPageHeaderElementSortDescendingOrder(ColumnName));
            Click(gridPageHeaderElementSortDescendingOrder(ColumnName));

            WaitForElementNotVisible("CWRefreshPanel", 7);
            return this;
        }

        public CPFinanceTransactionTriggersPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID));
            ScrollToElement(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return this;
        }

        public CPFinanceTransactionTriggersPage ResultsPageValidateHeaderCellSortAscendingOrder(int CellPosition)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
            ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortasc");

            return this;
        }

        public CPFinanceTransactionTriggersPage ResultsPageValidateHeaderCellSortAscendingOrder(int CellPosition, string ColumnName)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition, ColumnName));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition, ColumnName));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition, ColumnName));
            ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortasc");

            return this;
        }

        public CPFinanceTransactionTriggersPage ClickColumnHeaderSortAscendingOrder(int CellPosition)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
            Click(gridPageHeaderElementSortDescendingOrder(CellPosition));

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public CPFinanceTransactionTriggersPage ResultsPageValidateHeaderCellSortDescendingOrder(int CellPosition)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
            ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortdesc");
            return this;
        }

        public CPFinanceTransactionTriggersPage ValidateRecordIsPresent(string RecordID, bool ExpectedVisible)
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

        public CPFinanceTransactionTriggersPage ValidateNoRecordsMessageVisible()
        {
            WaitForElementVisible(noRecordsMessage);
            WaitForElementVisible(noResultsMessage);

            return this;
        }

        public CPFinanceTransactionTriggersPage ResultsPageValidateHeaderCellSortDescendingOrder(int CellPosition, string ColumnName)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition, ColumnName));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition, ColumnName));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition, ColumnName));
            ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortdesc");
            return this;
        }

    }
}

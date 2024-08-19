using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class FinanceInvoiceBatchesPage : CommonMethods
    {
        public FinanceInvoiceBatchesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame_financeInvoiceBatchSetup = By.XPath("//iframe[contains(@id, 'iframe_CWDialog')][contains(@src, 'type=financeinvoicebatchsetup&')]");
        readonly By urlIFrame = By.Id("CWUrlPanel_IFrame");

        readonly By FinanceInvoiceBatchesPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By OpenBookmarkDialog = By.Id("TI_OpenBookmarkDialog");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By searchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");

        readonly By noRecordsMessage = By.XPath("//*[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By noResultsMessage = By.XPath("//*[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");

        By recordRowCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");

        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");

        By gridPageHeaderElement(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");

        By gridPageHeaderElementSortOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/a/span[2]");

        By gridPageHeaderElementSortAscendingOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/ a/span[2][@class = 'sortasc']");
        By gridPageHeaderElementSortDescendingOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/a/span[2][@class = 'sortdesc']");
        public FinanceInvoiceBatchesPage WaitForFinanceInvoiceBatchSetupPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(FinanceInvoiceBatchesPageHeader);
            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(OpenBookmarkDialog);

            return this;
        }

        public FinanceInvoiceBatchesPage WaitForFinanceInvoiceBatchSetup_FinanceInvoiceBatchesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(cwDialogIFrame_financeInvoiceBatchSetup);
            SwitchToIframe(cwDialogIFrame_financeInvoiceBatchSetup);

            WaitForElement(urlIFrame);
            SwitchToIframe(urlIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(FinanceInvoiceBatchesPageHeader);
            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(viewSelector);

            return this;
        }

        public FinanceInvoiceBatchesPage SelectSystemView(string ExpectedTextToSelect)
        {
            WaitForElementToBeClickable(viewSelector);
            MoveToElementInPage(viewSelector);
            SelectPicklistElementByText(viewSelector, ExpectedTextToSelect);

            return this;
        }

        public FinanceInvoiceBatchesPage ValidateSelectedSystemView(string ExpectedText)
        {
            WaitForElementToBeClickable(viewSelector);
            MoveToElementInPage(viewSelector);
            ValidatePicklistSelectedText(viewSelector, ExpectedText);

            return this;
        }

        public FinanceInvoiceBatchesPage InsertSearchQuery(string SearchQuery)
        {
            WaitForElementToBeClickable(searchTextBox);
            MoveToElementInPage(searchTextBox);
            SendKeys(searchTextBox, SearchQuery);

            return this;
        }

        public FinanceInvoiceBatchesPage TapSearchButton()
        {
            WaitForElementToBeClickable(searchButton);
            MoveToElementInPage(searchButton);
            Click(searchButton);

            return this;
        }

        public FinanceInvoiceBatchesPage SelectFinanceInvoiceBatchSetupRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            MoveToElementInPage(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public FinanceInvoiceBatchesPage ValidateRecordCellContent(string RecordId, int CellPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(recordRowCell(RecordId, CellPosition));
            ValidateElementText(recordRowCell(RecordId, CellPosition), ExpectedText);

            return this;
        }

        public FinanceInvoiceBatchesPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(ExportToExcelButton);
            MoveToElementInPage(ExportToExcelButton);
            Click(ExportToExcelButton);

            return this;
        }

        public FinanceInvoiceBatchesPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID));
            MoveToElementInPage(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return this;
        }

        public FinanceInvoiceBatchesPage OpenRecord(Guid RecordID)
        {
            return OpenRecord(RecordID.ToString());
        }

        public FinanceInvoiceBatchesPage ValidateRecordIsPresent(string RecordID, bool ExpectedVisible)
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

        public FinanceInvoiceBatchesPage ValidateNoRecordsMessageVisible()
        {
            WaitForElement(noRecordsMessage);
            WaitForElement(noResultsMessage);

            return this;
        }

        public FinanceInvoiceBatchesPage ClickColumnHeader(int CellPosition)
        {
            ScrollToElement(gridPageHeaderElement(CellPosition));
            WaitForElementVisible(gridPageHeaderElement(CellPosition));
            Click(gridPageHeaderElement(CellPosition));

            return this;
        }

        public FinanceInvoiceBatchesPage ClickColumnHeaderSortDescendingOrder(int CellPosition)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
            MoveToElementInPage(gridPageHeaderElementSortOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
            Click(gridPageHeaderElementSortAscendingOrder(CellPosition));

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public FinanceInvoiceBatchesPage ResultsPageValidateHeaderCellSortAscendingOrder(int CellPosition)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
            ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortasc");

            return this;
        }

        public FinanceInvoiceBatchesPage ClickColumnHeaderSortAscendingOrder(int CellPosition)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
            Click(gridPageHeaderElementSortDescendingOrder(CellPosition));

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public FinanceInvoiceBatchesPage ResultsPageValidateHeaderCellSortDescendingOrder(int CellPosition)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
            ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortdesc");

            return this;
        }
    }
}

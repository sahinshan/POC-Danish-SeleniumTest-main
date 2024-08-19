using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CPFinanceTransactionsPage : CommonMethods
    {

        public CPFinanceTransactionsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        By cwDialog_IFrame(string parentRecordIdSuffix) => By.XPath("//iframe[contains(@id, 'iframe_CWDialog_" + parentRecordIdSuffix + "')]");

        By cwDialog_TypeId(string parentRecordBoName) => By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=" + parentRecordBoName + "')]");

        readonly By urlIFrame = By.Id("CWUrlPanel_IFrame");

        readonly By FinanceInvoicePageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By MailMergeButton = By.Id("TI_MailMergeButton");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By searchTextBox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By RefreshButton = By.Id("CWRefreshButton");
        readonly By noRecordsMessage = By.XPath("//*[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By noResultsMessage = By.XPath("//*[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");

        By recordRowCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");

        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");

        By gridPageHeaderLinkElement(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a");
        By gridPageHeaderLinkText(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a//span");

        By gridPageHeaderSortDescIcon(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[@class='sortdesc']");
        By gridPageHeaderSortAscIcon(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[@class='sortasc']");

        By gridPageHeaderElement(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");

        By gridPageHeaderElement(int HeaderCellPosition, string HeaderCellText) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[text() = '" + HeaderCellText + "']");

        //rename method references to CPFinanceTransactionsPage
        public CPFinanceTransactionsPage WaitForCPFinanceTransactionsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElementVisible(FinanceInvoicePageHeader);
            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(viewSelector);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public CPFinanceTransactionsPage WaitForCPFinanceTransactionsPageToLoad(string parentRecordIdSuffix, bool IsInvoiceRecord = true)
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 10);

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

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(FinanceInvoicePageHeader);
            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(viewSelector);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public CPFinanceTransactionsPage SelectSystemView(string ExpectedTextToSelect)
        {
            WaitForElementToBeClickable(viewSelector);
            ScrollToElement(viewSelector);
            SelectPicklistElementByText(viewSelector, ExpectedTextToSelect);

            return this;
        }

        public CPFinanceTransactionsPage ValidateSelectedSystemView(string ExpectedText)
        {
            WaitForElementToBeClickable(viewSelector);
            ScrollToElement(viewSelector);
            ValidatePicklistSelectedText(viewSelector, ExpectedText);

            return this;
        }

        public CPFinanceTransactionsPage InsertSearchQuery(string SearchQuery)
        {
            WaitForElementToBeClickable(searchTextBox);
            ScrollToElement(searchTextBox);
            SendKeys(searchTextBox, SearchQuery);

            return this;
        }

        public CPFinanceTransactionsPage ClickSearchButton()
        {
            WaitForElementToBeClickable(quickSearchButton);
            ScrollToElement(quickSearchButton);
            Click(quickSearchButton);

            return this;
        }

        public CPFinanceTransactionsPage SelectFinanceInvoiceRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            ScrollToElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public CPFinanceTransactionsPage ValidateRecordCellContent(string RecordId, int CellPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(recordRowCell(RecordId, CellPosition));
            ScrollToElement(recordRowCell(RecordId, CellPosition));
            ValidateElementText(recordRowCell(RecordId, CellPosition), ExpectedText);

            return this;
        }

        public CPFinanceTransactionsPage RecordsPageValidateHeaderCellText(int CellPosition, string ExpectedText)
        {
            ScrollToElement(gridPageHeaderElement(CellPosition));
            WaitForElementVisible(gridPageHeaderElement(CellPosition));
            ValidateElementText(gridPageHeaderElement(CellPosition), ExpectedText);

            return this;
        }

        public CPFinanceTransactionsPage RecordsPageValidateHeaderLinkText(int CellPosition, string ExpectedText)
        {
            ScrollToElement(gridPageHeaderLinkText(CellPosition));
            WaitForElementVisible(gridPageHeaderLinkElement(CellPosition));
            ValidateElementText(gridPageHeaderLinkText(CellPosition), ExpectedText);

            return this;
        }

        public CPFinanceTransactionsPage RecordsPageValidateHeaderSortDescIconVisible(int CellPosition)
        {
            ScrollToElement(gridPageHeaderSortDescIcon(CellPosition));
            WaitForElementVisible(gridPageHeaderSortDescIcon(CellPosition));;

            return this;
        }

        public CPFinanceTransactionsPage RecordsPageValidateHeaderSortAscIconVisible(int CellPosition)
        {
            ScrollToElement(gridPageHeaderSortAscIcon(CellPosition));
            WaitForElementVisible(gridPageHeaderSortAscIcon(CellPosition)); ;

            return this;
        }

        //click on the header cell text
        public CPFinanceTransactionsPage ClickHeaderCellText(int CellPosition, string HeaderCellText)
        {
            WaitForElement(gridPageHeaderElement(CellPosition, HeaderCellText));
            ScrollToElement(gridPageHeaderElement(CellPosition, HeaderCellText));
            Click(gridPageHeaderElement(CellPosition, HeaderCellText));

            return this;
        }

        public CPFinanceTransactionsPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(ExportToExcelButton);
            ScrollToElement(ExportToExcelButton);
            Click(ExportToExcelButton);

            return this;
        }

        public CPFinanceTransactionsPage ClickMailMergeButton()
        {
            WaitForElementToBeClickable(MailMergeButton);
            ScrollToElement(MailMergeButton);
            Click(MailMergeButton);

            return this;
        }

        public CPFinanceTransactionsPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID));
            ScrollToElement(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return this;
        }

        public CPFinanceTransactionsPage OpenRecord(Guid RecordID)
        {
            return OpenRecord(RecordID.ToString());
        }

        public CPFinanceTransactionsPage ValidateRecordIsPresent(string RecordID, bool ExpectedVisible = true)
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

        public CPFinanceTransactionsPage ValidateRecordIsPresent(Guid RecordID, bool ExpectedVisible = true)
        {
            return ValidateRecordIsPresent(RecordID.ToString(), ExpectedVisible);

        }

        public CPFinanceTransactionsPage ValidateNoRecordsMessageVisible(bool IsVisible)
        {
            if (IsVisible)
            {
                WaitForElementVisible(noRecordsMessage);
                WaitForElementVisible(noResultsMessage);
            }
            else
            {
                WaitForElementNotVisible(noRecordsMessage, 3);
                WaitForElementNotVisible(noResultsMessage, 3);
            }

            return this;
        }

        public CPFinanceTransactionsPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(RefreshButton);
            ScrollToElement(RefreshButton);
            Click(RefreshButton);

            return this;
        }
    }
}

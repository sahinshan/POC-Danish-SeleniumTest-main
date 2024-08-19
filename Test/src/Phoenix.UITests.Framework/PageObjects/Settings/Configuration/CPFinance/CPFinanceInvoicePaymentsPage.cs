using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Configuration.CPFinance
{
    public class CPFinanceInvoicePaymentsPage : CommonMethods
    {

        readonly By contentIFrame = By.Id("CWContentIFrame");
        By cwDialog_IFrame(string parentRecordIdSuffix) => By.XPath("//iframe[contains(@id, 'iframe_CWDialog_" + parentRecordIdSuffix + "')]");

        readonly By cwDialog_TypeId = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderfinanceinvoice&')]");

        readonly By urlIFrame = By.Id("CWUrlPanel_IFrame");

        readonly By PageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By NewRecordButton = By.XPath("//*[@id='TI_NewRecordButton']");
        readonly By ExportToExcelButton = By.XPath("//*[@id='TI_ExportToExcelButton']");
        readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
        readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");
        readonly By OpenBookmarkDialog = By.XPath("//*[@id='TI_OpenBookmarkDialog']");
        readonly By ViewSelectorPicklist = By.XPath("//*[@id='CWViewSelector']");
        readonly By QuickSearchTextBox = By.XPath("//*[@id='CWQuickSearch']");
        readonly By QuickSearchButton = By.XPath("//*[@id='CWQuickSearchButton']");
        readonly By RefreshButton = By.XPath("//*[@id='CWRefreshButton']");

        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");
        By recordRowCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");
        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");
        By RecordIdentifier(int CellPosition, string RecordID) => By.XPath("//tr[" + CellPosition + "][@id='" + RecordID + "']");
        By gridPageHeaderLinkElement(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a");
        By gridPageHeaderSortDescIcon(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[@class='sortdesc']");
        By gridPageHeaderSortAscIcon(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[@class='sortasc']");
        By gridPageHeaderElement(int HeaderCellPosition, string HeaderCellText) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[text() = '" + HeaderCellText + "']");

        public CPFinanceInvoicePaymentsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        public CPFinanceInvoicePaymentsPage WaitForPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElementVisible(PageHeader);
            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(ViewSelectorPicklist);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            return this;
        }

        public CPFinanceInvoicePaymentsPage WaitForPageToLoad(Guid? parentRecordIdSuffix, bool parentRecordIdUsed)
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            if (parentRecordIdUsed && parentRecordIdSuffix.HasValue)
            {
                WaitForElement(cwDialog_IFrame(parentRecordIdSuffix.ToString()));
                SwitchToIframe(cwDialog_IFrame(parentRecordIdSuffix.ToString()));
            }
            else
            {
                WaitForElement(cwDialog_TypeId);
                SwitchToIframe(cwDialog_TypeId);
            }

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(urlIFrame);
            SwitchToIframe(urlIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(PageHeader);
            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(ViewSelectorPicklist);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public CPFinanceInvoicePaymentsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            ScrollToElement(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public CPFinanceInvoicePaymentsPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(ExportToExcelButton);
            Click(ExportToExcelButton);

            return this;
        }

        public CPFinanceInvoicePaymentsPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public CPFinanceInvoicePaymentsPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }

        public CPFinanceInvoicePaymentsPage ClickOpenBookmarkDialog()
        {
            WaitForElementToBeClickable(OpenBookmarkDialog);
            Click(OpenBookmarkDialog);

            return this;
        }

        public CPFinanceInvoicePaymentsPage SelectViewSelector(string TextToSelect)
        {
            WaitForElementToBeClickable(ViewSelectorPicklist);
            SelectPicklistElementByText(ViewSelectorPicklist, TextToSelect);

            return this;
        }

        public CPFinanceInvoicePaymentsPage ValidateSelectedSystemView(string ExpectedText)
        {
            WaitForElementVisible(ViewSelectorPicklist);
            ValidatePicklistSelectedText(ViewSelectorPicklist, ExpectedText);

            return this;
        }

        public CPFinanceInvoicePaymentsPage ValidateQuickSearchText(string ExpectedText)
        {
            WaitForElementVisible(QuickSearchTextBox);
            ValidateElementValue(QuickSearchTextBox, ExpectedText);

            return this;
        }

        public CPFinanceInvoicePaymentsPage InsertTextOnQuickSearch(string TextToInsert)
        {
            WaitForElementToBeClickable(QuickSearchTextBox);
            SendKeys(QuickSearchTextBox, TextToInsert);

            return this;
        }

        public CPFinanceInvoicePaymentsPage ClickQuickSearchButton()
        {
            WaitForElementToBeClickable(QuickSearchButton);
            Click(QuickSearchButton);

            return this;
        }

        public CPFinanceInvoicePaymentsPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(RefreshButton);
            Click(RefreshButton);

            return this;
        }

        public CPFinanceInvoicePaymentsPage SelectFinanceInvoiceRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            ScrollToElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public CPFinanceInvoicePaymentsPage ValidateRecordCellContent(string RecordId, int CellPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(recordRowCell(RecordId, CellPosition));
            ScrollToElement(recordRowCell(RecordId, CellPosition));
            ValidateElementText(recordRowCell(RecordId, CellPosition), ExpectedText);

            return this;
        }

        public CPFinanceInvoicePaymentsPage RecordsPageValidateHeaderLinkText(int CellPosition, string ExpectedText)
        {
            ScrollToElement(gridPageHeaderLinkElement(CellPosition));
            WaitForElementVisible(gridPageHeaderLinkElement(CellPosition));
            ValidateElementText(gridPageHeaderLinkElement(CellPosition), ExpectedText);

            return this;
        }

        public CPFinanceInvoicePaymentsPage RecordsPageValidateHeaderSortDescIconVisible(int CellPosition)
        {
            ScrollToElement(gridPageHeaderSortDescIcon(CellPosition));
            WaitForElementVisible(gridPageHeaderSortDescIcon(CellPosition)); ;

            return this;
        }

        public CPFinanceInvoicePaymentsPage RecordsPageValidateHeaderSortAscIconVisible(int CellPosition)
        {
            ScrollToElement(gridPageHeaderSortAscIcon(CellPosition));
            WaitForElementVisible(gridPageHeaderSortAscIcon(CellPosition)); ;

            return this;
        }

        //click on the header cell text
        public CPFinanceInvoicePaymentsPage ClickHeaderCellText(int CellPosition, string HeaderCellText)
        {
            WaitForElementToBeClickable(gridPageHeaderElement(CellPosition, HeaderCellText));
            ScrollToElement(gridPageHeaderElement(CellPosition, HeaderCellText));
            Click(gridPageHeaderElement(CellPosition, HeaderCellText));

            return this;
        }

        public CPFinanceInvoicePaymentsPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID));
            ScrollToElement(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return this;
        }

        public CPFinanceInvoicePaymentsPage OpenRecord(Guid RecordID)
        {
            return OpenRecord(RecordID.ToString());
        }

        public CPFinanceInvoicePaymentsPage ValidateRecordIsPresent(string RecordID, bool ExpectedVisible = true)
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

        public CPFinanceInvoicePaymentsPage ValidateRecordIsPresent(Guid RecordID, bool ExpectedVisible = true)
        {
            return ValidateRecordIsPresent(RecordID.ToString(), ExpectedVisible);

        }

        public CPFinanceInvoicePaymentsPage ValidateRecordIsPresent(int CellPosition, string RecordID, bool ExpectedVisible = true)
        {
            if (ExpectedVisible)
            {
                WaitForElementToBeClickable(RecordIdentifier(CellPosition, RecordID));
                ScrollToElement(RecordIdentifier(CellPosition, RecordID));
            }
            else
                WaitForElementNotVisible(RecordIdentifier(CellPosition, RecordID), 3);

            Assert.AreEqual(ExpectedVisible, GetElementVisibility(RecordIdentifier(CellPosition, RecordID)));

            return this;
        }

        public CPFinanceInvoicePaymentsPage ValidateRecordIsPresent(int CellPosition, Guid RecordID, bool ExpectedVisible = true)
        {
            return ValidateRecordIsPresent(CellPosition, RecordID.ToString(), ExpectedVisible);

        }
    }
}

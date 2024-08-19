using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Configuration.CPFinance
{
    public class PayrollBatchesPage : CommonMethods
    {

        readonly By contentIFrame = By.Id("CWContentIFrame");
       
        readonly By PageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By NewRecordButton = By.XPath("//*[@id='TI_NewRecordButton']");
        readonly By ExportToExcelButton = By.XPath("//*[@id='TI_ExportToExcelButton']");
        readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
        readonly By AdditionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']");
        readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");

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

        public PayrollBatchesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        public PayrollBatchesPage WaitForPageToLoad()
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);

            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(PageHeader);
            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(ViewSelectorPicklist);

            return this;
        }


        public PayrollBatchesPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            ScrollToElement(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public PayrollBatchesPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(ExportToExcelButton);
            Click(ExportToExcelButton);

            return this;
        }

        public PayrollBatchesPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public PayrollBatchesPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(AdditionalItemsButton);
            Click(AdditionalItemsButton);

            WaitForElementToBeClickable(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }


        public PayrollBatchesPage SelectViewSelector(string TextToSelect)
        {
            WaitForElementToBeClickable(ViewSelectorPicklist);
            SelectPicklistElementByText(ViewSelectorPicklist, TextToSelect);

            return this;
        }

        public PayrollBatchesPage ValidateSelectedSystemView(string ExpectedText)
        {
            WaitForElementVisible(ViewSelectorPicklist);
            ValidatePicklistSelectedText(ViewSelectorPicklist, ExpectedText);

            return this;
        }

        public PayrollBatchesPage ValidateQuickSearchText(string ExpectedText)
        {
            WaitForElementVisible(QuickSearchTextBox);
            ValidateElementValue(QuickSearchTextBox, ExpectedText);

            return this;
        }

        public PayrollBatchesPage InsertTextOnQuickSearch(string TextToInsert)
        {
            WaitForElementToBeClickable(QuickSearchTextBox);
            SendKeys(QuickSearchTextBox, TextToInsert);

            return this;
        }

        public PayrollBatchesPage ClickQuickSearchButton()
        {
            WaitForElementToBeClickable(QuickSearchButton);
            Click(QuickSearchButton);

            return this;
        }

        public PayrollBatchesPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(RefreshButton);
            Click(RefreshButton);

            return this;
        }

        public PayrollBatchesPage SelectFinanceInvoiceRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            ScrollToElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public PayrollBatchesPage ValidateRecordCellContent(string RecordId, int CellPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(recordRowCell(RecordId, CellPosition));
            ScrollToElement(recordRowCell(RecordId, CellPosition));
            ValidateElementText(recordRowCell(RecordId, CellPosition), ExpectedText);

            return this;
        }

        public PayrollBatchesPage RecordsPageValidateHeaderLinkText(int CellPosition, string ExpectedText)
        {
            ScrollToElement(gridPageHeaderLinkElement(CellPosition));
            WaitForElementVisible(gridPageHeaderLinkElement(CellPosition));
            ValidateElementText(gridPageHeaderLinkElement(CellPosition), ExpectedText);

            return this;
        }

        public PayrollBatchesPage RecordsPageValidateHeaderSortDescIconVisible(int CellPosition)
        {
            ScrollToElement(gridPageHeaderSortDescIcon(CellPosition));
            WaitForElementVisible(gridPageHeaderSortDescIcon(CellPosition)); ;

            return this;
        }

        public PayrollBatchesPage RecordsPageValidateHeaderSortAscIconVisible(int CellPosition)
        {
            ScrollToElement(gridPageHeaderSortAscIcon(CellPosition));
            WaitForElementVisible(gridPageHeaderSortAscIcon(CellPosition)); ;

            return this;
        }

        //click on the header cell text
        public PayrollBatchesPage ClickHeaderCellText(int CellPosition, string HeaderCellText)
        {
            WaitForElementToBeClickable(gridPageHeaderElement(CellPosition, HeaderCellText));
            ScrollToElement(gridPageHeaderElement(CellPosition, HeaderCellText));
            Click(gridPageHeaderElement(CellPosition, HeaderCellText));

            return this;
        }

        public PayrollBatchesPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID));
            ScrollToElement(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return this;
        }

        public PayrollBatchesPage OpenRecord(Guid RecordID)
        {
            return OpenRecord(RecordID.ToString());
        }

        public PayrollBatchesPage ValidateRecordIsPresent(string RecordID, bool ExpectedVisible = true)
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

        public PayrollBatchesPage ValidateRecordIsPresent(Guid RecordID, bool ExpectedVisible = true)
        {
            return ValidateRecordIsPresent(RecordID.ToString(), ExpectedVisible);

        }

        public PayrollBatchesPage ValidateRecordIsPresent(int CellPosition, string RecordID, bool ExpectedVisible = true)
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

        public PayrollBatchesPage ValidateRecordIsPresent(int CellPosition, Guid RecordID, bool ExpectedVisible = true)
        {
            return ValidateRecordIsPresent(CellPosition, RecordID.ToString(), ExpectedVisible);

        }
    }
}

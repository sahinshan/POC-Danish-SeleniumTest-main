using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Phoenix.UITests.Framework.PageObjects.People;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class FinanceTransactionsPage : CommonMethods
    {
        public FinanceTransactionsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=serviceprovision&')]");        
        By cwDynamicDialogIFrame(string BusinessObjectName) => By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type="+ BusinessObjectName + "&')]");        
        By cwDialog_IFrame(string parentRecordIdSuffix) => By.XPath("//iframe[contains(@id, 'iframe_CWDialog_"+parentRecordIdSuffix+"')]");
        readonly By relatedRecordsPanelIFrame = By.Id("CWUrlPanel_IFrame");
        readonly By financeTransactionsPageHeader = By.XPath("//div[@id='CWToolbar']//h1[text()='Finance Transactions']");

        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        By recordRow(string FinanceTransactionId) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + FinanceTransactionId + "']/td[2]");
        By recordRowCheckbox(string FinanceTransactionId) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + FinanceTransactionId + "']/td[1]");

        readonly By GridHeaderSelectorCheckbox = By.XPath("//*[@id='cwgridheaderselector']");

        readonly By backButton = By.XPath("//button[@title = 'Back']");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By TotalsButton = By.Id("TI_Totals");
        readonly By RemoveButton = By.Id("TI_Remove");
        readonly By ToolbarOptionButton = By.Id("CWToolbarMenu");
        readonly By MoveToNewButton = By.Id("TI_Move");

        readonly By noRecordsMessage = By.XPath("//*[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By noResultsMessage = By.XPath("//*[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");


        By FinanceTransactionsHeaderCell(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");

        By FinanceTransactionsRecordCell(string FinanceTransactionId, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + FinanceTransactionId + "']/td[" + CellPosition + "]");



        public FinanceTransactionsPage WaitForFinanceTransactionsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(cwDialogIFrame);
            SwitchToIframe(cwDialogIFrame);

            WaitForElement(relatedRecordsPanelIFrame);
            SwitchToIframe(relatedRecordsPanelIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(financeTransactionsPageHeader);

            WaitForElementVisible(viewsPicklist);
            WaitForElementVisible(quickSearchTextBox);
            WaitForElementVisible(quickSearchButton);
            WaitForElementVisible(refreshButton);

            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(AssignRecordButton);
            WaitForElementVisible(TotalsButton);

            return this;
        }

        public FinanceTransactionsPage WaitForFinanceTransactionsPageToLoad(string parentRecordIdSuffix)
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(cwDialog_IFrame(parentRecordIdSuffix));
            SwitchToIframe(cwDialog_IFrame(parentRecordIdSuffix));

            WaitForElement(relatedRecordsPanelIFrame);
            SwitchToIframe(relatedRecordsPanelIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(financeTransactionsPageHeader);

            WaitForElementVisible(viewsPicklist);
            WaitForElementVisible(quickSearchTextBox);
            WaitForElementVisible(quickSearchButton);
            WaitForElementVisible(refreshButton);

            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(AssignRecordButton);
            WaitForElementVisible(TotalsButton);

            return this;
        }

        public FinanceTransactionsPage WaitForFinanceTransactionsPageToLoadInsidePersonContractPage(string BusinessObjectName)
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(cwDynamicDialogIFrame(BusinessObjectName));
            SwitchToIframe(cwDynamicDialogIFrame(BusinessObjectName));

            WaitForElement(relatedRecordsPanelIFrame);
            SwitchToIframe(relatedRecordsPanelIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 60);

            WaitForElementVisible(viewsPicklist);
            WaitForElementVisible(quickSearchTextBox);
            WaitForElementVisible(quickSearchButton);
            WaitForElementVisible(refreshButton);

            WaitForElementVisible(ExportToExcelButton);

            return this;
        }

        public FinanceTransactionsPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            MoveToElementInPage(backButton);
            Click(backButton);

            return this;
        }

        public FinanceTransactionsPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            MoveToElementInPage(refreshButton);
            Click(refreshButton);

            return this;
        }

        public FinanceTransactionsPage ClickRemoveButton()
        {
            WaitForElementToBeClickable(RemoveButton);
            MoveToElementInPage(RemoveButton);
            Click(RemoveButton);

            return this;
        }

        public FinanceTransactionsPage ClickMoveToNewButton()
        {
            WaitForElementToBeClickable(ToolbarOptionButton);
            MoveToElementInPage(ToolbarOptionButton);
            Click(ToolbarOptionButton);

            WaitForElementToBeClickable(MoveToNewButton);
            MoveToElementInPage(MoveToNewButton);
            Click(MoveToNewButton);

            Assert.IsTrue(GetElementVisibilityByID("CWRefreshPanel"));
            return this;
        }

        public FinanceTransactionsPage ClickGridHeaderSelectorRowCheckbox()
        {
            MoveToElementInPage(GridHeaderSelectorCheckbox);
            WaitForElementToBeClickable(GridHeaderSelectorCheckbox);
            Click(GridHeaderSelectorCheckbox);

            return this;
        }

        public FinanceTransactionsPage SearchFinanceTransactionsRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public FinanceTransactionsPage SelectFinanceTransactionRecord(string FinanceTransactionId)
        {
            WaitForElementToBeClickable(recordRowCheckbox(FinanceTransactionId));
            MoveToElementInPage(recordRowCheckbox(FinanceTransactionId));
            Click(recordRowCheckbox(FinanceTransactionId));

            return this;
        }

        public FinanceTransactionsPage ClickTotalsButton()
        {
            WaitForElementToBeClickable(TotalsButton);
            MoveToElementInPage(TotalsButton);
            Click(TotalsButton);

            return this;
        }

        public FinanceTransactionsPage OpenFinanceTransactionsRecord(string FinanceTransactionId)
        {
            WaitForElementToBeClickable(recordRow(FinanceTransactionId));
            MoveToElementInPage(recordRow(FinanceTransactionId));
            Click(recordRow(FinanceTransactionId));

            return this;
        }

        public FinanceTransactionsPage SelectAvailableViewByText(string PicklistText)
        {
            SelectPicklistElementByText(viewsPicklist, PicklistText);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public FinanceTransactionsPage ValidateRecordPresent(string RecordId)
        {
            WaitForElementVisible(recordRow(RecordId));
            MoveToElementInPage(recordRow(RecordId));
            Assert.IsTrue(GetElementVisibility(recordRow(RecordId)));

            return this;
        }

        public FinanceTransactionsPage ValidateRecordNotPresent(string RecordId)
        {
            WaitForElementNotVisible(recordRow(RecordId), 7);

            return this;
        }

        public FinanceTransactionsPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(FinanceTransactionsRecordCell(RecordID, CellPosition));
            ValidateElementText(FinanceTransactionsRecordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public FinanceTransactionsPage ValidateSelectedViewText(string ExpectedText)
        {
            WaitForElementVisible(viewsPicklist);
            MoveToElementInPage(viewsPicklist);
            ValidatePicklistSelectedText(viewsPicklist, ExpectedText);

            return this;
        }

        public FinanceTransactionsPage ValidateNoRecordsMessageVisible(bool Visibility)
        {
            if (Visibility)
            {
                WaitForElementVisible(noRecordsMessage);
                WaitForElementVisible(noResultsMessage);
            }
            else
            {
                WaitForElementNotVisible(noRecordsMessage, 5);
                WaitForElementNotVisible(noResultsMessage, 5);
            }

            return this;
        }

        public FinanceTransactionsPage ValidateRemoveTransactionsButtonVisible(bool Visibility)
        {
            if (Visibility)
            {
                WaitForElementVisible(RemoveButton);                
            }
            else
            {
                WaitForElementNotVisible(RemoveButton, 5);
            }

            return this;
        }

        public FinanceTransactionsPage ValidateMoveToNewButtonVisible(bool Visibility)
        {
            if (Visibility)
            {
                WaitForElementToBeClickable(ToolbarOptionButton);
                MoveToElementInPage(ToolbarOptionButton);
                Click(ToolbarOptionButton);

                WaitForElementVisible(MoveToNewButton);
            }
            else
            {
                WaitForElementNotVisible(ToolbarOptionButton, 3);
                WaitForElementNotVisible(MoveToNewButton, 3);
            }

            return this;
        }

        public FinanceTransactionsPage ValidateHeaderCellText(int CellPosition, string ExpectedText)
        {
            ScrollToElement(FinanceTransactionsHeaderCell(CellPosition));
            WaitForElementVisible(FinanceTransactionsHeaderCell(CellPosition));
            ValidateElementText(FinanceTransactionsHeaderCell(CellPosition), ExpectedText);

            return this;
        }
    }
}

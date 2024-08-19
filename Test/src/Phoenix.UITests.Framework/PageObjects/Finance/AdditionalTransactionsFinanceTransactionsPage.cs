using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class AdditionalTransactionsFinanceTransactionsPage : CommonMethods
    {
        public AdditionalTransactionsFinanceTransactionsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame_additionalFinanceTransactions = By.XPath("//iframe[contains(@id, 'iframe_CWDialog')][contains(@src, 'type=financeinvoice&')]");
        readonly By url_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By FinanceTransactionsPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By TotalsButton = By.Id("TI_Totals");
        readonly By ToolbarMenuButton = By.Id("CWToolbarMenu");
        readonly By RefreshButton = By.Id("CWRefreshButton");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By searchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");

        readonly By noRecordsMessage = By.XPath("//*[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By noResultsMessage = By.XPath("//*[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");

        By recordRowCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");

        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");


        public AdditionalTransactionsFinanceTransactionsPage WaitForAdditionalTransactionsFinanceTransactionsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(cwDialogIFrame_additionalFinanceTransactions);
            SwitchToIframe(cwDialogIFrame_additionalFinanceTransactions);

            WaitForElement(url_IFrame);
            SwitchToIframe(url_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(FinanceTransactionsPageHeader);            
            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(AssignRecordButton);            

            return this;
        }

        public AdditionalTransactionsFinanceTransactionsPage SelectSystemView(string ExpectedTextToSelect)
        {
            WaitForElementToBeClickable(viewSelector);
            MoveToElementInPage(viewSelector);
            SelectPicklistElementByText(viewSelector, ExpectedTextToSelect);

            return this;
        }

        public AdditionalTransactionsFinanceTransactionsPage ValidateSelectedSystemView(string ExpectedText)
        {
            WaitForElementToBeClickable(viewSelector);
            MoveToElementInPage(viewSelector);
            ValidatePicklistSelectedText(viewSelector, ExpectedText);

            return this;
        }

        public AdditionalTransactionsFinanceTransactionsPage InsertSearchQuery(string SearchQuery)
        {
            WaitForElementToBeClickable(searchTextBox);
            MoveToElementInPage(searchTextBox);
            SendKeys(searchTextBox, SearchQuery);

            return this;
        }

        public AdditionalTransactionsFinanceTransactionsPage TapSearchButton()
        {
            WaitForElementToBeClickable(searchButton);
            MoveToElementInPage(searchButton);
            Click(searchButton);

            return this;
        }

        public AdditionalTransactionsFinanceTransactionsPage SelectAdditionalTransactionsFinanceTransactionsRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            MoveToElementInPage(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public AdditionalTransactionsFinanceTransactionsPage ValidateRecordCellContent(string RecordId, int CellPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(recordRowCell(RecordId, CellPosition));
            ValidateElementText(recordRowCell(RecordId, CellPosition), ExpectedText);

            return this;
        }

        public AdditionalTransactionsFinanceTransactionsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            MoveToElementInPage(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public AdditionalTransactionsFinanceTransactionsPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(ExportToExcelButton);
            MoveToElementInPage(ExportToExcelButton);
            Click(ExportToExcelButton);

            return this;
        }

        public AdditionalTransactionsFinanceTransactionsPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            MoveToElementInPage(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public AdditionalTransactionsFinanceTransactionsPage ClickTotalsButton()
        {
            WaitForElementToBeClickable(TotalsButton);
            MoveToElementInPage(TotalsButton);
            Click(TotalsButton);

            return this;
        }

        public AdditionalTransactionsFinanceTransactionsPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(ToolbarMenuButton);
            MoveToElementInPage(ToolbarMenuButton);
            Click(ToolbarMenuButton);

            WaitForElementToBeClickable(DeleteRecordButton);
            MoveToElementInPage(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }

        public AdditionalTransactionsFinanceTransactionsPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID));
            MoveToElementInPage(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return this;
        }

        public AdditionalTransactionsFinanceTransactionsPage ValidateRecordIsPresent(string RecordID, bool ExpectedVisible)
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

        public AdditionalTransactionsFinanceTransactionsPage ValidateNoRecordsMessageVisible()
        {
            WaitForElementVisible(noRecordsMessage);
            WaitForElementVisible(noResultsMessage);

            return this;
        }

        public AdditionalTransactionsFinanceTransactionsPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(RefreshButton);
            MoveToElementInPage(RefreshButton);
            Click(RefreshButton);

            return this;
        }

        public AdditionalTransactionsFinanceTransactionsPage ValidateDeleteRecordButtonIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
            {
                WaitForElementToBeClickable(ToolbarMenuButton);
                MoveToElementInPage(ToolbarMenuButton);
                Click(ToolbarMenuButton);
                
                WaitForElementVisible(DeleteRecordButton);
            }
            else
            {
                WaitForElementNotVisible(ToolbarMenuButton, 3);
                WaitForElementNotVisible(DeleteRecordButton, 3);
            }
            return this;
        }

        public AdditionalTransactionsFinanceTransactionsPage ValidateNewRecordButtonIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
            {
                WaitForElementVisible(NewRecordButton);
            }
            else
            {
                WaitForElementNotVisible(NewRecordButton, 3);                
            }
            return this;
        }

    }
}

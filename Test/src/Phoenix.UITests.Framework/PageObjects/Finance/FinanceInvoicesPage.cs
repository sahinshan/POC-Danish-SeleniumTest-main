using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class FinanceInvoicesPage : CommonMethods
    {
        public FinanceInvoicesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");        
        By cwDialog_IFrame(string parentRecordIdSuffix) => By.XPath("//iframe[contains(@id, 'iframe_CWDialog_" + parentRecordIdSuffix + "')]");
        readonly By urlIFrame = By.Id("CWUrlPanel_IFrame");

        readonly By FinanceInvoicePageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By OpenBookmarkDialog = By.Id("TI_OpenBookmarkDialog");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By searchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");
        readonly By DoNotUseViewFilterCheckbox = By.Id("CWIgnoreViewFilter");
        readonly By InvoiceNumberField = By.Id("CWField_financeinvoice_invoicenumber");
        readonly By PersonLookupField = By.Id("CWLookupBtn_financeinvoice_personid");        
        readonly By SearchButton = By.Id("CWSearchButton");
        readonly By ToolbarOptionsMenu = By.Id("CWToolbarMenu");
        readonly By AuthoriseButton = By.Id("TI_Authorise");
        readonly By ReadyToAuthoriseButton = By.Id("TI_ReadyToAuthorize");
        readonly By noRecordsMessage = By.XPath("//*[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By noResultsMessage = By.XPath("//*[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");

        By recordRowCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");

        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");

        By gridPageHeaderElement(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");

        public FinanceInvoicesPage WaitForFinanceInvoicesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(FinanceInvoicePageHeader);
            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(viewSelector);

            return this;
        }

        public FinanceInvoicesPage WaitForFinanceInvoicesPageToLoad(string parentRecordIdSuffix)
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(cwDialog_IFrame(parentRecordIdSuffix));
            SwitchToIframe(cwDialog_IFrame(parentRecordIdSuffix));

            WaitForElement(urlIFrame);
            SwitchToIframe(urlIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(FinanceInvoicePageHeader);
            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(viewSelector);

            return this;
        }

        public FinanceInvoicesPage SelectSystemView(string ExpectedTextToSelect)
        {
            WaitForElementToBeClickable(viewSelector);
            MoveToElementInPage(viewSelector);
            SelectPicklistElementByText(viewSelector, ExpectedTextToSelect);

            return this;
        }

        public FinanceInvoicesPage ValidateSelectedSystemView(string ExpectedText)
        {
            WaitForElementToBeClickable(viewSelector);
            MoveToElementInPage(viewSelector);
            ValidatePicklistSelectedText(viewSelector, ExpectedText);

            return this;
        }

        public FinanceInvoicesPage InsertSearchQuery(string SearchQuery)
        {
            WaitForElementToBeClickable(searchTextBox);
            MoveToElementInPage(searchTextBox);
            SendKeys(searchTextBox, SearchQuery);

            return this;
        }

        public FinanceInvoicesPage InsertInvoiceNumber(string TextToInsert)
        {
            WaitForElementToBeClickable(InvoiceNumberField);
            MoveToElementInPage(InvoiceNumberField);
            SendKeys(InvoiceNumberField, TextToInsert);

            return this;
        }

        public FinanceInvoicesPage ClickPersonFieldLookupButton()
        {
            MoveToElementInPage(PersonLookupField);
            WaitForElementToBeClickable(PersonLookupField);
            Click(PersonLookupField);

            return this;
        }

        public FinanceInvoicesPage ClickSearchButton()
        {
            WaitForElementToBeClickable(SearchButton);
            MoveToElementInPage(SearchButton);
            Click(SearchButton);

            return this;
        }

        public FinanceInvoicesPage TapSearchButton()
        {
            WaitForElementToBeClickable(searchButton);
            MoveToElementInPage(searchButton);
            Click(searchButton);

            return this;
        }

        public FinanceInvoicesPage SelectFinanceInvoiceRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            MoveToElementInPage(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public FinanceInvoicesPage ValidateRecordCellContent(string RecordId, int CellPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(recordRowCell(RecordId, CellPosition));
            ValidateElementText(recordRowCell(RecordId, CellPosition), ExpectedText);

            return this;
        }

        public FinanceInvoicesPage RecordsPageValidateHeaderCellText(int CellPosition, string ExpectedText)
        {
            MoveToElementInPage(gridPageHeaderElement(CellPosition));
            WaitForElementVisible(gridPageHeaderElement(CellPosition));
            ValidateElementText(gridPageHeaderElement(CellPosition), ExpectedText);

            return this;
        }

        public FinanceInvoicesPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(ExportToExcelButton);
            MoveToElementInPage(ExportToExcelButton);
            Click(ExportToExcelButton);

            return this;
        }

        public FinanceInvoicesPage ClickReadyToAuthoriseButton()
        {
            WaitForElementToBeClickable(ReadyToAuthoriseButton);
            MoveToElementInPage(ReadyToAuthoriseButton);
            Click(ReadyToAuthoriseButton);

            return this;
        }

        public FinanceInvoicesPage ClickAuthoriseButton()
        {
            WaitForElementToBeClickable(ToolbarOptionsMenu);
            MoveToElementInPage(ToolbarOptionsMenu);
            Click(ToolbarOptionsMenu);

            WaitForElementToBeClickable(AuthoriseButton);
            MoveToElementInPage(AuthoriseButton);
            Click(AuthoriseButton);

            return this;
        }

        public FinanceInvoicesPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID));
            MoveToElementInPage(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return this;
        }

        public FinanceInvoicesPage ValidateRecordIsPresent(string RecordID, bool ExpectedVisible)
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

        public FinanceInvoicesPage ValidateNoRecordsMessageVisible()
        {
            WaitForElement(noRecordsMessage);
            WaitForElement(noResultsMessage);

            return this;
        }

        public FinanceInvoicesPage SearchFinanceInvoiceByInvoiceNumber(string SearchQuery)
        {
            ClickDoNotUseViewFilterCheckbox();
            InsertInvoiceNumber(SearchQuery);
            ClickSearchButton();

            WaitForElementNotVisible("CWRefreshPanel", 30);

            return this;
        }

        public FinanceInvoicesPage ClickDoNotUseViewFilterCheckbox()
        {
            WaitForElementToBeClickable(DoNotUseViewFilterCheckbox);
            string elementChecked = GetElementByAttributeValue(DoNotUseViewFilterCheckbox, "checked");
            if (elementChecked != "true")
                Click(DoNotUseViewFilterCheckbox);

            return this;
        }

    }
}

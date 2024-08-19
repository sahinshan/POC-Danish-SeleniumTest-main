using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class FinanceCodeMappingsPage : CommonMethods
    {
        public FinanceCodeMappingsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CareProviderContractScheme = By.Id("iframe_careprovidercontractscheme");
        readonly By iframe_CWDialog_CareProviderContractScheme = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careprovidercontractscheme&')]");
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By FinanceCodeMappingPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");

        readonly By ViewSelector = By.Id("CWViewSelector");
        readonly By SearchTextBox = By.Id("CWQuickSearch");
        readonly By SearchButton = By.Id("CWQuickSearchButton");
        readonly By RefreshButton = By.Id("CWRefreshButton");


        By pageHeaderElement(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");

        By pageHeaderElementSortOrded(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/a/span[2]");


        readonly By noRecordsMessage = By.XPath("//*[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By noResultsMessage = By.XPath("//*[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");


        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");

        By recordRowCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");

        By recordRowPosition(int RowPosition, string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr["+ RowPosition + "][@id='"+ recordID + "']");

        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");

        By gridPageHeaderElementSortAscendingOrder(int HeaderCellPosition, string FieldName) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/a[@title = 'Sort by " + FieldName + "']/span[2][@class = 'sortasc']");



        public FinanceCodeMappingsPage WaitForFinanceCodeMappingsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CareProviderContractScheme);
            SwitchToIframe(iframe_CareProviderContractScheme);

            WaitForElement(iframe_CWDialog_CareProviderContractScheme);
            SwitchToIframe(iframe_CWDialog_CareProviderContractScheme);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElementVisible(FinanceCodeMappingPageHeader);
            WaitForElementVisible(NewRecordButton);
            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(AssignRecordButton);
            WaitForElementVisible(DeleteRecordButton);

            WaitForElementVisible(SearchTextBox);
            WaitForElementVisible(SearchButton);
            WaitForElementVisible(RefreshButton);

            return this;
        }

        public FinanceCodeMappingsPage SelectSystemView(string ExpectedTextToSelect)
        {
            WaitForElementToBeClickable(ViewSelector);
            MoveToElementInPage(ViewSelector);
            SelectPicklistElementByText(ViewSelector, ExpectedTextToSelect);

            return this;
        }

        public FinanceCodeMappingsPage ValidateSelectedSystemView(string ExpectedText)
        {
            WaitForElementToBeClickable(ViewSelector);
            MoveToElementInPage(ViewSelector);
            ValidatePicklistSelectedText(ViewSelector, ExpectedText);
            return this;
        }

        public FinanceCodeMappingsPage InsertSearchQuery(string SearchQuery)
        {
            WaitForElementToBeClickable(SearchTextBox);
            MoveToElementInPage(SearchTextBox);
            SendKeys(SearchTextBox, SearchQuery);

            return this;
        }

        public FinanceCodeMappingsPage TapSearchButton()
        {
            WaitForElementToBeClickable(SearchButton);
            MoveToElementInPage(SearchButton);
            Click(SearchButton);

            return this;
        }

        public FinanceCodeMappingsPage SelectFinanceCodeMappingRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            MoveToElementInPage(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public FinanceCodeMappingsPage ValidateRecordCellContent(string RecordId, int CellPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(recordRowCell(RecordId, CellPosition));
            ValidateElementText(recordRowCell(RecordId, CellPosition), ExpectedText);

            return this;
        }

        public FinanceCodeMappingsPage ValidateRecordRowPosition(int RowPosition, string RecordId)
        {
            WaitForElementVisible(recordRowPosition(RowPosition, RecordId));
            
            return this;
        }

        public FinanceCodeMappingsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            MoveToElementInPage(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public FinanceCodeMappingsPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            MoveToElementInPage(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }

        public FinanceCodeMappingsPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID));
            MoveToElementInPage(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return this;
        }

        public FinanceCodeMappingsPage ValidateNoRecordsMessageVisible()
        {
            WaitForElement(noRecordsMessage);
            WaitForElement(noResultsMessage);

            return this;
        }

        public FinanceCodeMappingsPage ValidateHeaderCellText(int CellPosition, string ExpectedText)
        {
            WaitForElementVisible(pageHeaderElement(CellPosition));
            ValidateElementText(pageHeaderElement(CellPosition), ExpectedText);

            return this;
        }

        public FinanceCodeMappingsPage ValidateHeaderCellSortOrdedAscending(int CellPosition)
        {
            WaitForElementVisible(pageHeaderElementSortOrded(CellPosition));
            ValidateElementAttribute(pageHeaderElementSortOrded(CellPosition), "class", "sortasc");

            return this;
        }

        public FinanceCodeMappingsPage ValidateHeaderCellSortOrdedDescending(int CellPosition)
        {
            WaitForElementVisible(pageHeaderElementSortOrded(CellPosition));
            ValidateElementAttribute(pageHeaderElementSortOrded(CellPosition), "class", "sortdesc");

            return this;
        }

        public FinanceCodeMappingsPage ValidateRecordPresent(string RecordID, bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
                WaitForElementVisible(RecordIdentifier(RecordID));
            else
                WaitForElementNotVisible(RecordIdentifier(RecordID), 7);


            return this;
        }

        public FinanceCodeMappingsPage ValidateHeaderCellSortOrdedAscending(int CellPosition, string headerName)
        {
            ScrollToElement(gridPageHeaderElementSortAscendingOrder(CellPosition, headerName));
            WaitForElementVisible(gridPageHeaderElementSortAscendingOrder(CellPosition, headerName));
            ValidateElementAttribute(gridPageHeaderElementSortAscendingOrder(CellPosition, headerName), "class", "sortasc");

            return this;
        }

    }
}
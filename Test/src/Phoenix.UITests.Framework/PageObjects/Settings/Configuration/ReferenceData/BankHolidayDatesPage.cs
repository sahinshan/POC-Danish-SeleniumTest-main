using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class BankHolidayDatesPage : CommonMethods
    {
        public BankHolidayDatesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cpbankholidaychargingcalendar&')]");
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By BankHolidayDatesPageHeader = By.XPath("//*[@id='CWToolbar']//h1[text()='Bank Holiday Dates']");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By searchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");

        readonly By noRecordsMessage = By.XPath("//*[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By noResultsMessage = By.XPath("//*[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");

        By recordRowCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");

        By headerCell(int CellPosition) => By.XPath("//table[@id='CWGridHeader']//tr/th[" + CellPosition + "]//a");

        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");

        By gridPageHeaderElementSortAscendingOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[2][@class = 'sortasc']");


        public BankHolidayDatesPage WaitForBankHolidayDatesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(BankHolidayDatesPageHeader);

            WaitForElementVisible(newRecordButton);
            WaitForElementVisible(exportToExcelButton);
            WaitForElementVisible(assignRecordButton);
            WaitForElementVisible(deleteRecordButton);

            return this;
        }

        public BankHolidayDatesPage SelectSystemView(string ExpectedTextToSelect)
        {
            WaitForElementToBeClickable(viewSelector);
            MoveToElementInPage(viewSelector);
            SelectPicklistElementByText(viewSelector, ExpectedTextToSelect);

            return this;
        }

        public BankHolidayDatesPage ValidateSelectedSystemView(string ExpectedText)
        {
            WaitForElementToBeClickable(viewSelector);
            MoveToElementInPage(viewSelector);
            ValidatePicklistSelectedText(viewSelector, ExpectedText);

            return this;
        }

        public BankHolidayDatesPage InsertSearchQuery(string SearchQuery)
        {
            WaitForElementToBeClickable(searchTextBox);
            MoveToElementInPage(searchTextBox);
            SendKeys(searchTextBox, SearchQuery);

            return this;
        }

        public BankHolidayDatesPage TapSearchButton()
        {
            WaitForElementToBeClickable(searchButton);
            MoveToElementInPage(searchButton);
            Click(searchButton);

            return this;
        }

        public BankHolidayDatesPage SelectBankHolidayDatesRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            MoveToElementInPage(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public BankHolidayDatesPage ValidateRecordCellContent(string RecordId, int CellPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(recordRowCell(RecordId, CellPosition));
            ValidateElementText(recordRowCell(RecordId, CellPosition), ExpectedText);

            return this;
        }

        public BankHolidayDatesPage ValidateHeaderCellText(int CellPosition, string ExpectedText)
        {
            ScrollToElement(headerCell(CellPosition));
            WaitForElementVisible(headerCell(CellPosition));
            ValidateElementText(headerCell(CellPosition), ExpectedText);

            return this;
        }

        public BankHolidayDatesPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            MoveToElementInPage(newRecordButton);
            Click(newRecordButton);

            return this;
        }

        public BankHolidayDatesPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID));
            MoveToElementInPage(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return this;
        }

        public BankHolidayDatesPage ValidateRecordPresentOrNot(string RecordID, bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
                WaitForElementVisible(RecordIdentifier(RecordID));
            else
                WaitForElementNotVisible(RecordIdentifier(RecordID), 7);


            return this;
        }

        public BankHolidayDatesPage ValidateNoRecordsMessageVisible()
        {
            WaitForElement(noRecordsMessage);
            WaitForElement(noResultsMessage);

            return this;
        }

        public BankHolidayDatesPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(deleteRecordButton);
            MoveToElementInPage(deleteRecordButton);
            Click(deleteRecordButton);

            return this;
        }

        public BankHolidayDatesPage ValidateGridHeaderCellSortAscendingOrder(int CellPosition, string ExpectedText)
        {
            ScrollToElement(gridPageHeaderElementSortAscendingOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortAscendingOrder(CellPosition));
            ValidateElementText(headerCell(CellPosition), ExpectedText);

            return this;
        }

    }
}

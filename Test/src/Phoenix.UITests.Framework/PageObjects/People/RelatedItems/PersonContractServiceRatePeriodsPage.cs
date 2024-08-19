using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class PersonContractServiceRatePeriodsPage : CommonMethods
    {
        public PersonContractServiceRatePeriodsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By CareProviderPersonContractService_IFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderpersoncontractservice&')]");
        readonly By UrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Person Contract Service Rate Periods']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        readonly By cloneButton = By.XPath("//*[@id='TI_CloneRecordButton']");

        readonly By ViewSelector = By.Id("CWViewSelector");
        readonly By SearchField = By.Id("CWQuickSearch");
        readonly By SearchButton = By.Id("CWQuickSearchButton");
        readonly By RefreshButton = By.Id("CWRefreshButton");

        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By CloneRecordButton = By.Id("TI_CloneRecordButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");

        readonly By ChargePerWeekTab = By.Id("CWNavGroup_ChargePerWeek");
        readonly By FinanceTransactionsTab = By.Id("CWNavGroup_FinanceTransactions");

        readonly By GridHeaderIdField = By.XPath("//*[@id='CWGridHeaderRow']//a[@title='Sort by Id']");


        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");

        By recordPosition(int RecordPosition, string RecordID) => By.XPath("//table/tbody/tr[" + RecordPosition + "][@id='" + RecordID + "']");

        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");


        public PersonContractServiceRatePeriodsPage WaitForPersonContractServiceRatePeriodsPageToLoad()
        {

            WaitForElementNotVisible("CWRefreshPanel", 20);

            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(CareProviderPersonContractService_IFrame);
            SwitchToIframe(CareProviderPersonContractService_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(UrlPanel_IFrame);
            SwitchToIframe(UrlPanel_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(ViewSelector);
            WaitForElementVisible(SearchField);
            WaitForElementVisible(SearchButton);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(NewRecordButton);
            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(AssignRecordButton);
            WaitForElementVisible(CloneRecordButton);
            WaitForElementVisible(DeleteRecordButton);

            return this;
        }

        public PersonContractServiceRatePeriodsPage WaitForPersonContractServiceRatePeriodsTabSectionToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(CareProviderPersonContractService_IFrame);
            SwitchToIframe(CareProviderPersonContractService_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 60);

            WaitForElementVisible(ChargePerWeekTab);
            WaitForElementVisible(FinanceTransactionsTab);

            return this;
        }

        public PersonContractServiceRatePeriodsPage WaitForPersonContractServiceRatePeriodsPageToLoadFromWorkplace()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementVisible(ViewSelector);
            WaitForElementVisible(SearchField);
            WaitForElementVisible(SearchButton);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(NewRecordButton);

            return this;
        }

        public PersonContractServiceRatePeriodsPage ClickCloneButton()
        {
            WaitForElementToBeClickable(cloneButton);
            Click(cloneButton);

            return this;

        }

        public PersonContractServiceRatePeriodsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            ScrollToElement(NewRecordButton);
            Click(NewRecordButton);

            return this;

        }

        public PersonContractServiceRatePeriodsPage ValidateNoRecordMessageVisibile(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(NoRecordMessage);
            else
                WaitForElementNotVisible(NoRecordMessage, 5);

            return this;
        }

        public PersonContractServiceRatePeriodsPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            WaitForElementVisible(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public PersonContractServiceRatePeriodsPage ValidateRecordCellText(Guid RecordID, int CellPosition, string ExpectedText)
        {
            return ValidateRecordCellText(RecordID.ToString(), CellPosition, ExpectedText);
        }

        public PersonContractServiceRatePeriodsPage SelectViewSelector(string TextToSelect)
        {
            SelectPicklistElementByText(ViewSelector, TextToSelect);

            return this;
        }

        public PersonContractServiceRatePeriodsPage SelectRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public PersonContractServiceRatePeriodsPage SelectRecord(Guid RecordId)
        {
            return SelectRecord(RecordId.ToString());
        }

        public PersonContractServiceRatePeriodsPage ValidateRecordPresent(string RecordId, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(recordRowCheckBox(RecordId));
            else
                WaitForElementNotVisible(recordRowCheckBox(RecordId), 3);

            return this;
        }

        public PersonContractServiceRatePeriodsPage ValidateRecordPresent(Guid RecordId, bool ExpectVisible)
        {
            return ValidateRecordPresent(RecordId.ToString(), ExpectVisible);
        }

        public PersonContractServiceRatePeriodsPage ValidateRecordPosition(int ElementPosition, string RecordID)
        {
            WaitForElementVisible(recordPosition(ElementPosition, RecordID));

            return this;
        }

        public PersonContractServiceRatePeriodsPage SearchRecord(string SearchQuery)
        {
            WaitForElementToBeClickable(SearchField);
            SendKeys(SearchField, SearchQuery);

            WaitForElementToBeClickable(SearchButton);
            ScrollToElement(SearchButton);
            Click(SearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public PersonContractServiceRatePeriodsPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(RefreshButton);
            ScrollToElement(RefreshButton);
            Click(RefreshButton);

            return this;
        }

        public PersonContractServiceRatePeriodsPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordCell(RecordID, 2));
            ScrollToElement(recordCell(RecordID, 2));
            Click(recordCell(RecordID, 2));

            return this;
        }

        public PersonContractServiceRatePeriodsPage OpenRecord(Guid RecordID)
        {
            return OpenRecord(RecordID.ToString());
        }

        public PersonContractServiceRatePeriodsPage GridHeaderIdFieldLink()
        {
            WaitForElementToBeClickable(GridHeaderIdField);
            Click(GridHeaderIdField);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public PersonContractServiceRatePeriodsPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            ScrollToElement(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public PersonContractServiceRatePeriodsPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            ScrollToElement(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }

        public PersonContractServiceRatePeriodsPage ClickChargePerWeekTab()
        {
            WaitForElementToBeClickable(ChargePerWeekTab);
            ScrollToElement(ChargePerWeekTab);
            Click(ChargePerWeekTab);

            return this;
        }

    }
}
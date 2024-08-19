using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class PersonContractServiceChargesPerWeekPage : CommonMethods
    {
        public PersonContractServiceChargesPerWeekPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By CareProviderPersonContractService_IFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderpersoncontractservice&')]");
        readonly By UrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Person Contract Service Charges Per Week']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        readonly By ViewSelector = By.Id("CWViewSelector");
        readonly By SearchField = By.Id("CWQuickSearch");
        readonly By SearchButton = By.Id("CWQuickSearchButton");
        readonly By RefreshButton = By.Id("CWRefreshButton");

        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By CalculateChargesPerWeekButton = By.Id("TI_CalculateButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");

        readonly By GridHeaderIdField = By.XPath("//*[@id='CWGridHeaderRow']//a[@title='Sort by Id']");


        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");

        By recordPosition(int RecordPosition, string RecordID) => By.XPath("//table/tbody/tr[" + RecordPosition + "][@id='" + RecordID + "']");

        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");

        By pageHeaderElement(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");

        By gridPageHeaderElementSortAscendingOrder(int HeaderCellPosition, string FieldName) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/a[@title = 'Sort by " + FieldName + "']/span[2][@class = 'sortdesc']");
      
        public PersonContractServiceChargesPerWeekPage WaitForPersonContractServiceChargesPerWeekPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(CareProviderPersonContractService_IFrame);
            SwitchToIframe(CareProviderPersonContractService_IFrame);

            WaitForElement(UrlPanel_IFrame);
            SwitchToIframe(UrlPanel_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 60);

            WaitForElementVisible(ViewSelector);
            WaitForElementVisible(SearchField);
            WaitForElementVisible(SearchButton);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(AssignRecordButton);

            return this;
        }

        public PersonContractServiceChargesPerWeekPage ValidateNoRecordMessageVisibile(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(NoRecordMessage);
            else
                WaitForElementNotVisible(NoRecordMessage, 5);

            return this;
        }

        public PersonContractServiceChargesPerWeekPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(recordCell(RecordID, CellPosition));
            WaitForElementVisible(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public PersonContractServiceChargesPerWeekPage SelectViewSelector(string TextToSelect)
        {
            WaitForElementToBeClickable(ViewSelector);
            MoveToElementInPage(ViewSelector);
            SelectPicklistElementByText(ViewSelector, TextToSelect);

            return this;
        }

        public PersonContractServiceChargesPerWeekPage ValidateSelectedViewPicklistText(string ExpectedText)
        {
            WaitForElementToBeClickable(ViewSelector);
            MoveToElementInPage(ViewSelector);
            ValidatePicklistSelectedText(ViewSelector, ExpectedText);

            return this;
        }

        public PersonContractServiceChargesPerWeekPage ValidateRecordPresent(string RecordId, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(recordRowCheckBox(RecordId));
            else
                WaitForElementNotVisible(recordRowCheckBox(RecordId), 3);

            return this;
        }

        public PersonContractServiceChargesPerWeekPage ValidateRecordPresent(Guid RecordId, bool ExpectVisible)
        {
            return ValidateRecordPresent(RecordId.ToString(), ExpectVisible);
        }

        public PersonContractServiceChargesPerWeekPage ValidateRecordPosition(int ElementPosition, string RecordID)
        {
            WaitForElementVisible(recordPosition(ElementPosition, RecordID));

            return this;
        }

        public PersonContractServiceChargesPerWeekPage SearchRecord(string SearchQuery)
        {
            WaitForElementToBeClickable(SearchField);
            SendKeys(SearchField, SearchQuery);

            WaitForElementToBeClickable(SearchButton);
            MoveToElementInPage(SearchButton);
            Click(SearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public PersonContractServiceChargesPerWeekPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(RefreshButton);
            MoveToElementInPage(RefreshButton);
            Click(RefreshButton);

            return this;
        }

        public PersonContractServiceChargesPerWeekPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordCell(RecordID, 2));
            MoveToElementInPage(recordCell(RecordID, 2));
            Click(recordCell(RecordID, 2));

            return this;
        }

        public PersonContractServiceChargesPerWeekPage GridHeaderIdFieldLink()
        {
            WaitForElementToBeClickable(GridHeaderIdField);
            Click(GridHeaderIdField);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public PersonContractServiceChargesPerWeekPage ValidateHeaderCellText(int CellPosition, string ExpectedText)
        {
            ScrollToElement(pageHeaderElement(CellPosition));
            WaitForElementVisible(pageHeaderElement(CellPosition));
            ValidateElementText(pageHeaderElement(CellPosition), ExpectedText);

            return this;
        }

        public PersonContractServiceChargesPerWeekPage ValidateHeaderCellSortOrdedByDescending(int CellPosition, string headerName)
        {
            ScrollToElement(gridPageHeaderElementSortAscendingOrder(CellPosition, headerName));
            WaitForElementVisible(gridPageHeaderElementSortAscendingOrder(CellPosition, headerName));
            ValidateElementAttribute(gridPageHeaderElementSortAscendingOrder(CellPosition, headerName), "class", "sortdesc");

            return this;
        }
      
    }
}
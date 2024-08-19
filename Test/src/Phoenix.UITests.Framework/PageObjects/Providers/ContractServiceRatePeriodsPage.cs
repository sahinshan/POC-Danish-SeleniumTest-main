using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.Providers
{
    public class ContractServiceRatePeriodsPage : CommonMethods
    {
        public ContractServiceRatePeriodsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By providerIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careprovidercontractservice&')]");
        By CWDialogIFrame(string RecordId) => By.XPath("//*[@id='iframe_CWDialog_" + RecordId + "']");
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Contract Service Rate Periods']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By searchField = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By assignButton = By.Id("TI_AssignRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By cloneRecordButton = By.Id("TI_CloneRecordButton");


        By GridHeaderCell(int cellPosition, string ExpectedText) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + cellPosition + "]//*[text()='" + ExpectedText + "']");

        By GridHeaderCell(int cellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + cellPosition + "]//a");


        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");

        By recordPosition(int RecordPosition, string RecordID) => By.XPath("//table/tbody/tr[" + RecordPosition + "][@id='" + RecordID + "']");

        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");


        public ContractServiceRatePeriodsPage WaitForContractServiceRatePeriodsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(providerIFrame);
            SwitchToIframe(providerIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementVisible(viewSelector);
            WaitForElementVisible(searchField);
            WaitForElementVisible(searchButton);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(newRecordButton);
            WaitForElementVisible(exportToExcelButton);

            return this;
        }

        public ContractServiceRatePeriodsPage WaitForContractServiceRatePeriodsPageToLoad(string RecordId)
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(CWDialogIFrame(RecordId));
            SwitchToIframe(CWDialogIFrame(RecordId));

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementVisible(viewSelector);
            WaitForElementVisible(searchField);
            WaitForElementVisible(searchButton);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(newRecordButton);
            WaitForElementVisible(exportToExcelButton);

            return this;
        }

        public ContractServiceRatePeriodsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);

            return this;

        }

        public ContractServiceRatePeriodsPage ClickCloneRecordButton()
        {
            WaitForElementToBeClickable(cloneRecordButton);
            Click(cloneRecordButton);

            return this;

        }

        public ContractServiceRatePeriodsPage ValidateNoRecordMessageVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(NoRecordMessage);
            else
                WaitForElementNotVisible(NoRecordMessage, 5);

            return this;
        }

        public ContractServiceRatePeriodsPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            WaitForElementVisible(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public ContractServiceRatePeriodsPage ValidateRecordCellText(Guid RecordID, int CellPosition, string ExpectedText)
        {
            return ValidateRecordCellText(RecordID.ToString(), CellPosition, ExpectedText);
        }

        public ContractServiceRatePeriodsPage SelectView(string TextToSelect)
        {
            SelectPicklistElementByText(viewSelector, TextToSelect);

            return this;
        }

        public ContractServiceRatePeriodsPage SelectRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            MoveToElementInPage(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public ContractServiceRatePeriodsPage ValidateRecordPresent(string RecordId, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(recordRowCheckBox(RecordId));
            else
                WaitForElementNotVisible(recordRowCheckBox(RecordId), 3);

            return this;
        }

        public ContractServiceRatePeriodsPage ValidateRecordPresent(Guid RecordId, bool ExpectVisible)
        {
            return ValidateRecordPresent(RecordId.ToString(), ExpectVisible);
        }

        public ContractServiceRatePeriodsPage ValidateRecordPosition(int ElementPosition, string RecordID)
        {
            WaitForElementVisible(recordPosition(ElementPosition, RecordID));

            return this;
        }

        public ContractServiceRatePeriodsPage SearchRecord(string SearchQuery)
        {
            WaitForElementToBeClickable(searchField);
            SendKeys(searchField, SearchQuery);
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public ContractServiceRatePeriodsPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            Click(refreshButton);

            return this;
        }

        public ContractServiceRatePeriodsPage ClickAssignButton()
        {
            WaitForElementToBeClickable(assignButton);
            Click(assignButton);

            return this;
        }

        public ContractServiceRatePeriodsPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordCell(RecordID, 2));
            Click(recordCell(RecordID, 2));

            return this;
        }

        public ContractServiceRatePeriodsPage OpenRecord(Guid RecordID)
        {
            return OpenRecord(RecordID.ToString());
        }

        public ContractServiceRatePeriodsPage ValidateHeaderCellText(int cellPosition, string ExpectedText)
        {
            WaitForElement(GridHeaderCell(cellPosition, ExpectedText));
            
            return this;
        }

        public ContractServiceRatePeriodsPage ClickHeaderCellLink(int cellPosition)
        {
            MoveToElementInPage(GridHeaderCell(cellPosition));
            Click(GridHeaderCell(cellPosition));

            return this;
        }


    }
}
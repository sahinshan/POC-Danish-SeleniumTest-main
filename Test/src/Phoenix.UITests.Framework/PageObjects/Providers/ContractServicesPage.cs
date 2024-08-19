using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.Providers
{
    public class ContractServicesPage : CommonMethods
    {
        public ContractServicesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By providerIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=provider&')]");
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Contract Services']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By establishmentLookupButton = By.Id("CWLookupBtn_careprovidercontractservice_establishmentproviderid");
        readonly By contractSchemeLookupButton = By.Id("CWLookupBtn_careprovidercontractservice_careprovidercontractschemeid");
        readonly By contractSchemeClearLookupButton = By.Id("CWClearLookup_careprovidercontractservice_careprovidercontractschemeid");
        readonly By funderLookupButton = By.Id("CWLookupBtn_careprovidercontractservice_funderproviderid");
        readonly By clearFiltersButton = By.Id("CWClearFiltersButton");
        readonly By searchButton = By.Id("CWSearchButton");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By assignButton = By.Id("TI_AssignRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");


        By GridHeaderCell(int cellPosition, string ExpectedText) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + cellPosition + "]//*[text()='" + ExpectedText + "']");

        By GridHeaderCell(int cellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + cellPosition + "]//a");


        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");

        By recordPosition(int RecordPosition, string RecordID) => By.XPath("//table/tbody/tr[" + RecordPosition + "][@id='" + RecordID + "']");

        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");


        public ContractServicesPage WaitForContractServicesPageToLoad()
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
            WaitForElementVisible(establishmentLookupButton);
            WaitForElementVisible(searchButton);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(newRecordButton);
            WaitForElementVisible(exportToExcelButton);

            return this;
        }

        public ContractServicesPage WaitForContractServicesPageToLoadFromFinanceAdminArea()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementVisible(viewSelector);
            WaitForElementVisible(establishmentLookupButton);
            WaitForElementVisible(searchButton);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(newRecordButton);
            WaitForElementVisible(exportToExcelButton);

            return this;
        }


        public ContractServicesPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);

            return this;

        }

        public ContractServicesPage ValidateNoRecordMessageVisibile(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(NoRecordMessage);
            else
                WaitForElementNotVisible(NoRecordMessage, 5);

            return this;
        }

        public ContractServicesPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            WaitForElementVisible(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public ContractServicesPage ValidateRecordCellText(Guid RecordID, int CellPosition, string ExpectedText)
        {
            return ValidateRecordCellText(RecordID.ToString(), CellPosition, ExpectedText);
        }

        public ContractServicesPage SelectView(string TextToSelect)
        {
            SelectPicklistElementByText(viewSelector, TextToSelect);

            return this;
        }

        public ContractServicesPage SelectRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public ContractServicesPage ValidateRecordPresent(string RecordId, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(recordRowCheckBox(RecordId));
            else
                WaitForElementNotVisible(recordRowCheckBox(RecordId), 3);

            return this;
        }

        public ContractServicesPage ValidateRecordPresent(Guid RecordId, bool ExpectVisible)
        {
            return ValidateRecordPresent(RecordId.ToString(), ExpectVisible);
        }

        public ContractServicesPage ValidateRecordPosition(int ElementPosition, string RecordID)
        {
            WaitForElementVisible(recordPosition(ElementPosition, RecordID));

            return this;
        }

        public ContractServicesPage ClickEstablishmentLookupButton()
        {
            WaitForElementToBeClickable(establishmentLookupButton);
            Click(establishmentLookupButton);

            return this;
        }

        public ContractServicesPage ClickContractSchemeLookupButton()
        {
            WaitForElementToBeClickable(contractSchemeLookupButton);
            Click(contractSchemeLookupButton);

            return this;
        }

        public ContractServicesPage ClickContractSchemeClearLookupButton()
        {
            WaitForElementToBeClickable(contractSchemeClearLookupButton);
            ScrollToElement(contractSchemeClearLookupButton);
            Click(contractSchemeClearLookupButton);

            return this;
        }

        public ContractServicesPage ClickFunderLookupButton()
        {
            WaitForElementToBeClickable(funderLookupButton);
            Click(funderLookupButton);

            return this;
        }

        public ContractServicesPage ClickClearFiltersButton()
        {
            WaitForElementToBeClickable(clearFiltersButton);
            Click(clearFiltersButton);

            return this;
        }

        public ContractServicesPage ClickSearchButton()
        {
            WaitForElementToBeClickable(searchButton);
            Click(searchButton);

            return this;
        }

        public ContractServicesPage ClickAssignButton()
        {
            WaitForElementToBeClickable(assignButton);
            Click(assignButton);

            return this;
        }

        public ContractServicesPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordCell(RecordID, 2));
            Click(recordCell(RecordID, 2));

            return this;
        }

        public ContractServicesPage OpenRecord(Guid RecordID)
        {
            return OpenRecord(RecordID.ToString());
        }

        public ContractServicesPage ValidateHeaderCellText(int cellPosition, string ExpectedText)
        {
            WaitForElement(GridHeaderCell(cellPosition, ExpectedText));
            
            return this;
        }

        public ContractServicesPage ClickHeaderCellLink(int cellPosition)
        {
            MoveToElementInPage(GridHeaderCell(cellPosition));
            Click(GridHeaderCell(cellPosition));

            return this;
        }


    }
}
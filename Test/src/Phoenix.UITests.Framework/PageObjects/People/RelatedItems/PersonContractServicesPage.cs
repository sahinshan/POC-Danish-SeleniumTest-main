using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class PersonContractServicesPage : CommonMethods
    {
        public PersonContractServicesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By CareProviderPersonContract_IFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderpersoncontract&')]");
        readonly By UrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Person Contract Services']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        readonly By ViewSelector = By.Id("CWViewSelector");
        readonly By SearchField = By.Id("CWQuickSearch");
        readonly By SearchButton = By.Id("CWQuickSearchButton");
        readonly By RefreshButton = By.Id("CWRefreshButton");

        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By CalculateChargesPerWeekButton = By.Id("TI_CalculateChargesPerWeekButton");
        readonly By PinButton = By.Id("TI_PinToMeButton");
        readonly By UnpinButton = By.Id("TI_UnpinFromMeButton");
        readonly By ToolbarMenu= By.Id("CWToolbarMenu");
        readonly By BulkEditButton = By.Id("TI_BulkEditButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By DetailsTab = By.XPath("//*[@id='CWNavGroup_EditForm']/a");

        readonly By GridHeaderIdField = By.XPath("//*[@id='CWGridHeaderRow']//a[@title='Sort by Id']");


        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");

        By recordRowCheckBoxSelection(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']");

        By recordPosition(int RecordPosition, string RecordID) => By.XPath("//table/tbody/tr[" + RecordPosition + "][@id='" + RecordID + "']");

        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");

        By pageHeaderElement(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");

        By gridPageHeaderElementSortAscendingOrder(int HeaderCellPosition, string FieldName) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a[@title = 'Sort by " + FieldName + "']/span[2][@class = 'sortdesc']");


        public PersonContractServicesPage WaitForPersonContractServicesPageToLoad(bool NavigateFromPeople = true)
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            if (NavigateFromPeople)
            {
                WaitForElement(CareProviderPersonContract_IFrame);
                SwitchToIframe(CareProviderPersonContract_IFrame);

                WaitForElement(UrlPanel_IFrame);
                SwitchToIframe(UrlPanel_IFrame);
            }

            WaitForElementNotVisible("CWRefreshPanel", 60);

            WaitForElementVisible(ViewSelector);
            WaitForElementVisible(SearchField);
            WaitForElementVisible(SearchButton);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(NewRecordButton);
            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(CalculateChargesPerWeekButton);
            WaitForElementVisible(PinButton);

            return this;
        }

        public PersonContractServicesPage WaitForPersonContractServicesTabSectionToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(CareProviderPersonContract_IFrame);
            SwitchToIframe(CareProviderPersonContract_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 60);

            WaitForElementVisible(DetailsTab);

            return this;
        }

        public PersonContractServicesPage WaitForPersonContractServicesPageToLoadFromWorkplace()
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

        public PersonContractServicesPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            ScrollToElement(NewRecordButton);
            Click(NewRecordButton);

            return this;

        }

        public PersonContractServicesPage ValidateNoRecordMessageVisibile(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(NoRecordMessage);
            else
                WaitForElementNotVisible(NoRecordMessage, 5);

            return this;
        }

        public PersonContractServicesPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(recordCell(RecordID, CellPosition));
            WaitForElementVisible(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public PersonContractServicesPage SelectViewSelector(string TextToSelect)
        {
            WaitForElementToBeClickable(ViewSelector);
            ScrollToElement(ViewSelector);
            SelectPicklistElementByText(ViewSelector, TextToSelect);

            return this;
        }

        public PersonContractServicesPage ValidateSelectedViewPicklistText(string ExpectedText)
        {
            WaitForElementToBeClickable(ViewSelector);
            ScrollToElement(ViewSelector);
            ValidatePicklistSelectedText(ViewSelector, ExpectedText);

            return this;
        }

        public PersonContractServicesPage SelectRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            ScrollToElement(recordRowCheckBox(RecordId));

            if (GetElementByAttributeValue(recordRowCheckBoxSelection(RecordId), "class") != "selrow")
                Click(recordRowCheckBox(RecordId));

            return this;
        }

        public PersonContractServicesPage ValidateRecordPresent(string RecordId, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(recordRowCheckBox(RecordId));
            else
                WaitForElementNotVisible(recordRowCheckBox(RecordId), 3);

            return this;
        }

        public PersonContractServicesPage ValidateRecordPresent(Guid RecordId, bool ExpectVisible)
        {
            return ValidateRecordPresent(RecordId.ToString(), ExpectVisible);
        }

        public PersonContractServicesPage ValidateRecordPosition(int ElementPosition, string RecordID)
        {
            WaitForElementVisible(recordPosition(ElementPosition, RecordID));

            return this;
        }

        public PersonContractServicesPage SearchRecord(string SearchQuery)
        {
            WaitForElementToBeClickable(SearchField);
            SendKeys(SearchField, SearchQuery);

            WaitForElementToBeClickable(SearchButton);
            ScrollToElement(SearchButton);
            Click(SearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public PersonContractServicesPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(RefreshButton);
            ScrollToElement(RefreshButton);
            Click(RefreshButton);

            return this;
        }

        public PersonContractServicesPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordCell(RecordID, 2));
            ScrollToElement(recordCell(RecordID, 2));
            Click(recordCell(RecordID, 2));

            return this;
        }

        public PersonContractServicesPage OpenRecord(Guid RecordID)
        {
            return OpenRecord(RecordID.ToString());
        }

        public PersonContractServicesPage GridHeaderIdFieldLink()
        {
            WaitForElementToBeClickable(GridHeaderIdField);
            Click(GridHeaderIdField);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public PersonContractServicesPage ClickPinButton()
        {
            WaitForElementToBeClickable(PinButton);
            ScrollToElement(PinButton);
            Click(PinButton);

            return this;
        }

        public PersonContractServicesPage ClickUnpinButton()
        {
            if (GetElementVisibility(UnpinButton))
            {
                ScrollToElement(UnpinButton);
                Click(UnpinButton);
            }
            else
            {
                WaitForElementToBeClickable(ToolbarMenu);
                ScrollToElement(ToolbarMenu);
                Click(ToolbarMenu);

                WaitForElementToBeClickable(UnpinButton);
                ScrollToElement(UnpinButton);
                Click(UnpinButton);
            }

            return this;
        }

        public PersonContractServicesPage ClickDetailsTab()
        {
            WaitForElementToBeClickable(DetailsTab);
            Click(DetailsTab);

            return this;
        }

        public PersonContractServicesPage ValidateHeaderCellText(int CellPosition, string ExpectedText)
        {
            ScrollToElement(pageHeaderElement(CellPosition));
            WaitForElementVisible(pageHeaderElement(CellPosition));
            ValidateElementText(pageHeaderElement(CellPosition), ExpectedText);

            return this;
        }

        public PersonContractServicesPage ValidateHeaderCellSortOrdedByDescending(int CellPosition, string headerName)
        {
            ScrollToElement(gridPageHeaderElementSortAscendingOrder(CellPosition, headerName));
            WaitForElementVisible(gridPageHeaderElementSortAscendingOrder(CellPosition, headerName));
            ValidateElementAttribute(gridPageHeaderElementSortAscendingOrder(CellPosition, headerName), "class", "sortdesc");

            return this;
        }

        public PersonContractServicesPage UnCheckRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            ScrollToElement(recordRowCheckBox(RecordId));

            if (GetElementByAttributeValue(recordRowCheckBoxSelection(RecordId), "class") == "selrow")
                Click(recordRowCheckBox(RecordId));

            return this;
        }

        public PersonContractServicesPage ClickBulkEditButton()
        {
            if (GetElementVisibility(BulkEditButton) == false)
            {
                WaitForElementToBeClickable(ToolbarMenu);
                ScrollToElement(ToolbarMenu);
                Click(ToolbarMenu);
            }

            WaitForElementToBeClickable(BulkEditButton);
            ScrollToElement(BulkEditButton);
            Click(BulkEditButton);

            return this;
        }

        public PersonContractServicesPage ClickDeleteRecordButton()
        {
            if (GetElementVisibility(DeleteRecordButton) == false)
            {
                WaitForElementToBeClickable(ToolbarMenu);
                ScrollToElement(ToolbarMenu);
                Click(ToolbarMenu);
            }

            WaitForElementToBeClickable(DeleteRecordButton);
            ScrollToElement(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }

        public PersonContractServicesPage ValidateDeleteRecordOption(bool IsVisible)
        {
            if (GetElementVisibility(DeleteRecordButton) == false)
            {
                WaitForElementToBeClickable(ToolbarMenu);
                ScrollToElement(ToolbarMenu);
                Click(ToolbarMenu);
            }

            if (IsVisible)
                WaitForElementVisible(DeleteRecordButton);
            else
                WaitForElementNotVisible(DeleteRecordButton, 3);

            return this;
        }

    }
}
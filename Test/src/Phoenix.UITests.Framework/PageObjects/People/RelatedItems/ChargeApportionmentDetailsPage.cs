using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class ChargeApportionmentDetailsPage : CommonMethods
    {
        public ChargeApportionmentDetailsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        By ChargeApportionmentDetails_IFrame(string type) => By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=" + type + "&')]");
                                                   //iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderchargeapportionment&')]

        readonly By UrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Charge Apportionment Details']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        readonly By BackButton = By.Id("BackButton");

        readonly By ViewSelector = By.Id("CWViewSelector");
        readonly By SearchField = By.Id("CWQuickSearch");
        readonly By SearchButton = By.Id("CWQuickSearchButton");
        readonly By RefreshButton = By.Id("CWRefreshButton");

        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By ToolbarMenu = By.Id("CWToolbarMenu");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");

        readonly By DetailTab = By.Id("CWNavGroup_EditForm");

        readonly By GridHeaderIdField = By.XPath("//*[@id='CWGridHeaderRow']//a[@title='Sort by Id']");

        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");

        By recordRowCheckBoxSelection(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']");

        By recordPosition(int RecordPosition, string RecordID) => By.XPath("//table/tbody/tr[" + RecordPosition + "][@id='" + RecordID + "']");

        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");

        By pageHeaderElement(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");

        By gridPageHeaderElementSortAscendingOrder(int HeaderCellPosition, string FieldName) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/a[@title = 'Sort by " + FieldName + "']/span[2][@class = 'sortdesc']");


        public ChargeApportionmentDetailsPage WaitForChargeApportionmentDetailsPageToLoad(string pageType = "person", bool isNotValidated = true)
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(ChargeApportionmentDetails_IFrame(pageType));
            SwitchToIframe(ChargeApportionmentDetails_IFrame(pageType));

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(UrlPanel_IFrame);
            SwitchToIframe(UrlPanel_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(ViewSelector);
            WaitForElementVisible(SearchField);
            WaitForElementVisible(SearchButton);

            WaitForElementVisible(pageHeader);
            if(isNotValidated)
            {
                WaitForElementVisible(NewRecordButton);
                WaitForElementVisible(DeleteRecordButton);
            }
            WaitForElementVisible(ExportToExcelButton);

            return this;
        }

        public ChargeApportionmentDetailsPage WaitForChargeApportionmentDetailsTabSectionToLoad(string pageType = "person")
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(ChargeApportionmentDetails_IFrame(pageType));
            SwitchToIframe(ChargeApportionmentDetails_IFrame(pageType));

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public ChargeApportionmentDetailsPage WaitForChargeApportionmentDetailsTabsSectionToLoad(string pageType = "person")
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(ChargeApportionmentDetails_IFrame(pageType));
            SwitchToIframe(ChargeApportionmentDetails_IFrame(pageType));

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public ChargeApportionmentDetailsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            ScrollToElement(NewRecordButton);
            Click(NewRecordButton);

            return this;

        }

        public ChargeApportionmentDetailsPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public ChargeApportionmentDetailsPage ValidateNoRecordMessageVisibile(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(NoRecordMessage);
            else
                WaitForElementNotVisible(NoRecordMessage, 5);

            return this;
        }

        public ChargeApportionmentDetailsPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(recordCell(RecordID, CellPosition));
            WaitForElementVisible(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public ChargeApportionmentDetailsPage SelectViewSelector(string TextToSelect)
        {
            WaitForElementToBeClickable(ViewSelector);
            ScrollToElement(ViewSelector);
            SelectPicklistElementByText(ViewSelector, TextToSelect);

            return this;
        }

        public ChargeApportionmentDetailsPage ValidateSelectedViewPicklistText(string ExpectedText)
        {
            WaitForElementToBeClickable(ViewSelector);
            ScrollToElement(ViewSelector);
            ValidatePicklistSelectedText(ViewSelector, ExpectedText);

            return this;
        }

        public ChargeApportionmentDetailsPage SelectRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            ScrollToElement(recordRowCheckBox(RecordId));

            if (GetElementByAttributeValue(recordRowCheckBoxSelection(RecordId), "class") != "selrow")
                Click(recordRowCheckBox(RecordId));

            return this;
        }

        public ChargeApportionmentDetailsPage ValidateRecordPresent(string RecordId, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(recordRowCheckBox(RecordId));
            else
                WaitForElementNotVisible(recordRowCheckBox(RecordId), 3);

            return this;
        }

        public ChargeApportionmentDetailsPage ValidateRecordPosition(int ElementPosition, string RecordID)
        {
            WaitForElementVisible(recordPosition(ElementPosition, RecordID));

            return this;
        }

        public ChargeApportionmentDetailsPage SearchRecord(string SearchQuery)
        {
            WaitForElementToBeClickable(SearchField);
            SendKeys(SearchField, SearchQuery);

            WaitForElementToBeClickable(SearchButton);
            ScrollToElement(SearchButton);
            Click(SearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public ChargeApportionmentDetailsPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(RefreshButton);
            ScrollToElement(RefreshButton);
            Click(RefreshButton);

            return this;
        }

        public ChargeApportionmentDetailsPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordCell(RecordID, 2));
            ScrollToElement(recordCell(RecordID, 2));
            Click(recordCell(RecordID, 2));

            return this;
        }

        public ChargeApportionmentDetailsPage ValidateHeaderCellText(int CellPosition, string ExpectedText)
        {
            ScrollToElement(pageHeaderElement(CellPosition));
            WaitForElementVisible(pageHeaderElement(CellPosition));
            ValidateElementText(pageHeaderElement(CellPosition), ExpectedText);

            return this;
        }

        public ChargeApportionmentDetailsPage ValidateHeaderCellSortOrdedByDescending(int CellPosition, string headerName)
        {
            ScrollToElement(gridPageHeaderElementSortAscendingOrder(CellPosition, headerName));
            WaitForElementVisible(gridPageHeaderElementSortAscendingOrder(CellPosition, headerName));
            ValidateElementAttribute(gridPageHeaderElementSortAscendingOrder(CellPosition, headerName), "class", "sortdesc");

            return this;
        }

        public ChargeApportionmentDetailsPage ClickDeleteRecordButton()
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

        public ChargeApportionmentDetailsPage ValidateToobarButtonIsPresent()
        {
            WaitForElementVisible(NewRecordButton);
            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(AssignRecordButton);
            WaitForElementVisible(DeleteRecordButton);

            return this;
        }

        public ChargeApportionmentDetailsPage NavigateToDetailsTab()
        {
            WaitForElementToBeClickable(DetailTab);
            Click(DetailTab);

            return this;
        }

    }
}
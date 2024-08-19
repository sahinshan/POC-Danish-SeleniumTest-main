using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class ChargeApportionmentsPage : CommonMethods
    {
        public ChargeApportionmentsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        //readonly By ChargeApportionments_IFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        By ChargeApportionments_IFrame(string type) => By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=" + type + "&')]");

        readonly By UrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Charge Apportionments']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        readonly By ViewSelector = By.Id("CWViewSelector");
        readonly By SearchField = By.Id("CWQuickSearch");
        readonly By SearchButton = By.Id("CWQuickSearchButton");
        readonly By RefreshButton = By.Id("CWRefreshButton");

        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By ToolbarMenu = By.Id("CWToolbarMenu");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");

        readonly By GridHeaderIdField = By.XPath("//*[@id='CWGridHeaderRow']//a[@title='Sort by Id']");

        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");

        By recordRowCheckBoxSelection(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']");

        By recordPosition(int RecordPosition, string RecordID) => By.XPath("//table/tbody/tr[" + RecordPosition + "][@id='" + RecordID + "']");

        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");

        By pageHeaderElement(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");

        By gridPageHeaderElementSortDescendingOrder(int HeaderCellPosition, string FieldName) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a[@title = 'Sort by " + FieldName + "']/span[2][@class = 'sortdesc']");
        By gridPageHeaderElementSortAscendingOrder(int HeaderCellPosition, string FieldName) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a[@title = 'Sort by " + FieldName + "']/span[2][@class = 'sortasc']");


        public ChargeApportionmentsPage WaitForChargeApportionmentsPageToLoad(string pageType="person")
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(ChargeApportionments_IFrame(pageType));
            SwitchToIframe(ChargeApportionments_IFrame(pageType));

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
            WaitForElementVisible(DeleteRecordButton);

            return this;
        }

        public ChargeApportionmentsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            ScrollToElement(NewRecordButton);
            Click(NewRecordButton);

            return this;

        }

        public ChargeApportionmentsPage ValidateNoRecordMessageVisibile(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(NoRecordMessage);
            else
                WaitForElementNotVisible(NoRecordMessage, 5);

            return this;
        }

        public ChargeApportionmentsPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(recordCell(RecordID, CellPosition));
            WaitForElementVisible(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public ChargeApportionmentsPage SelectViewSelector(string TextToSelect)
        {
            WaitForElementToBeClickable(ViewSelector);
            ScrollToElement(ViewSelector);
            SelectPicklistElementByText(ViewSelector, TextToSelect);

            return this;
        }

        public ChargeApportionmentsPage ValidateSelectedViewPicklistText(string ExpectedText)
        {
            WaitForElementToBeClickable(ViewSelector);
            ScrollToElement(ViewSelector);
            ValidatePicklistSelectedText(ViewSelector, ExpectedText);

            return this;
        }

        public ChargeApportionmentsPage SelectRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            ScrollToElement(recordRowCheckBox(RecordId));

            if (GetElementByAttributeValue(recordRowCheckBoxSelection(RecordId), "class") != "selrow")
                Click(recordRowCheckBox(RecordId));

            return this;
        }

        public ChargeApportionmentsPage ValidateRecordPresent(string RecordId, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(recordRowCheckBox(RecordId));
            else
                WaitForElementNotVisible(recordRowCheckBox(RecordId), 3);

            return this;
        }

        public ChargeApportionmentsPage ValidateRecordPosition(int ElementPosition, string RecordID)
        {
            WaitForElementVisible(recordPosition(ElementPosition, RecordID));

            return this;
        }

        public ChargeApportionmentsPage SearchRecord(string SearchQuery)
        {
            WaitForElementToBeClickable(SearchField);
            SendKeys(SearchField, SearchQuery);

            WaitForElementToBeClickable(SearchButton);
            ScrollToElement(SearchButton);
            Click(SearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public ChargeApportionmentsPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(RefreshButton);
            ScrollToElement(RefreshButton);
            Click(RefreshButton);

            return this;
        }

        public ChargeApportionmentsPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordCell(RecordID, 2));
            ScrollToElement(recordCell(RecordID, 2));
            Click(recordCell(RecordID, 2));

            return this;
        }

        public ChargeApportionmentsPage ValidateHeaderCellText(int CellPosition, string ExpectedText)
        {
            ScrollToElement(pageHeaderElement(CellPosition));
            WaitForElementVisible(pageHeaderElement(CellPosition));
            ValidateElementText(pageHeaderElement(CellPosition), ExpectedText);

            return this;
        }

        public ChargeApportionmentsPage ValidateHeaderCellSortOrdedByDescending(int CellPosition, string headerName)
        {
            ScrollToElement(gridPageHeaderElementSortDescendingOrder(CellPosition, headerName));
            WaitForElementVisible(gridPageHeaderElementSortDescendingOrder(CellPosition, headerName));
            ValidateElementAttribute(gridPageHeaderElementSortDescendingOrder(CellPosition, headerName), "class", "sortdesc");

            return this;
        }

        public ChargeApportionmentsPage ValidateHeaderCellSortOrdedByAscending(int CellPosition, string headerName)
        {
            ScrollToElement(gridPageHeaderElementSortAscendingOrder(CellPosition, headerName));
            WaitForElementVisible(gridPageHeaderElementSortAscendingOrder(CellPosition, headerName));
            ValidateElementAttribute(gridPageHeaderElementSortAscendingOrder(CellPosition, headerName), "class", "sortasc");

            return this;
        }

        public ChargeApportionmentsPage ClickDeleteRecordButton()
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

        //validate Delete Record Button is present
        public ChargeApportionmentsPage ValidateDeleteRecordButtonIsPresent()
        {
            if (GetElementVisibility(DeleteRecordButton) == false)
            {
                WaitForElementToBeClickable(ToolbarMenu);
                ScrollToElement(ToolbarMenu);
                Click(ToolbarMenu);
            }

            ScrollToElement(DeleteRecordButton);
            WaitForElementVisible(DeleteRecordButton);

            return this;
        }


        public ChargeApportionmentsPage ValidateToobarButtonIsPresent()
        {
            WaitForElementVisible(NewRecordButton);
            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(AssignRecordButton);
            WaitForElementVisible(DeleteRecordButton);

            return this;
        }

    }
}
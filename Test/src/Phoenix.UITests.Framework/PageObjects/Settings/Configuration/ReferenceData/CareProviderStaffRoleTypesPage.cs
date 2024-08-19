using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CareProviderStaffRoleTypesPage : CommonMethods
    {
        public CareProviderStaffRoleTypesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CareProviderStaffRoleTypes = By.Id("iframe_careproviderstaffroletype");

        readonly By careProviderStaffRoleTypesPageHeader = By.XPath("//*[@id='CWToolbar']//h1[text()='Care Provider Staff Role Types']");

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

        By headerCell(int CellPosition) => By.XPath("//table[@id='CWGridHeader']//tr/th[" + CellPosition + "]//a/span");

        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");

        By gridPageHeaderElementSortAscendingOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[2][@class = 'sortasc']");

        By gridPageHeaderElementSortDescendingOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[2][@class = 'sortdesc']");


        public CareProviderStaffRoleTypesPage WaitForCareProviderStaffRoleTypesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(careProviderStaffRoleTypesPageHeader);

            WaitForElementVisible(newRecordButton);
            WaitForElementVisible(exportToExcelButton);
            WaitForElementVisible(assignRecordButton);
            WaitForElementVisible(deleteRecordButton);

            return this;
        }

        public CareProviderStaffRoleTypesPage SelectSystemView(string ExpectedTextToSelect)
        {
            WaitForElementToBeClickable(viewSelector);
            ScrollToElement(viewSelector);
            SelectPicklistElementByText(viewSelector, ExpectedTextToSelect);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            return this;
        }

        public CareProviderStaffRoleTypesPage ValidateSelectedSystemView(string ExpectedText)
        {
            WaitForElementToBeClickable(viewSelector);
            ScrollToElement(viewSelector);
            ValidatePicklistSelectedText(viewSelector, ExpectedText);

            return this;
        }

        public CareProviderStaffRoleTypesPage InsertSearchQuery(string SearchQuery)
        {
            WaitForElementToBeClickable(searchTextBox);
            ScrollToElement(searchTextBox);
            SendKeys(searchTextBox, SearchQuery);

            return this;
        }

        public CareProviderStaffRoleTypesPage TapSearchButton()
        {
            WaitForElementToBeClickable(searchButton);
            ScrollToElement(searchButton);
            Click(searchButton);

            return this;
        }

        public CareProviderStaffRoleTypesPage SelectCareProviderStaffRoleTypesRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            ScrollToElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public CareProviderStaffRoleTypesPage ValidateRecordCellContent(string RecordId, int CellPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(recordRowCell(RecordId, CellPosition));
            ValidateElementText(recordRowCell(RecordId, CellPosition), ExpectedText);

            return this;
        }

        public CareProviderStaffRoleTypesPage ValidateHeaderCellText(int CellPosition, string ExpectedText)
        {
            ScrollToElement(headerCell(CellPosition));
            WaitForElementVisible(headerCell(CellPosition));
            ValidateElementText(headerCell(CellPosition), ExpectedText);

            return this;
        }

        public CareProviderStaffRoleTypesPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            ScrollToElement(newRecordButton);
            Click(newRecordButton);

            return this;
        }

        public CareProviderStaffRoleTypesPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID));
            ScrollToElement(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return this;
        }

        public CareProviderStaffRoleTypesPage ValidateRecordPresentOrNot(string RecordID, bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
                WaitForElementVisible(RecordIdentifier(RecordID));
            else
                WaitForElementNotVisible(RecordIdentifier(RecordID), 7);


            return this;
        }

        public CareProviderStaffRoleTypesPage ValidateNoRecordsMessageVisible()
        {
            WaitForElement(noRecordsMessage);
            WaitForElement(noResultsMessage);

            return this;
        }

        public CareProviderStaffRoleTypesPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(deleteRecordButton);
            ScrollToElement(deleteRecordButton);
            Click(deleteRecordButton);

            return this;
        }

        public CareProviderStaffRoleTypesPage ValidateGridHeaderCellSortAscendingOrder(int CellPosition, string ExpectedText)
        {
            ScrollToElement(gridPageHeaderElementSortAscendingOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortAscendingOrder(CellPosition));
            ValidateElementText(headerCell(CellPosition), ExpectedText);

            return this;
        }

        public CareProviderStaffRoleTypesPage ValidateGridHeaderCellSortDescendingOrder(int CellPosition, string ExpectedText)
        {
            ScrollToElement(gridPageHeaderElementSortDescendingOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortDescendingOrder(CellPosition));
            ValidateElementText(headerCell(CellPosition), ExpectedText);

            return this;
        }

    }
}

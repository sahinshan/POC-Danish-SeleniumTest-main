using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CareProviderStaffRoleTypeGroupsPage : CommonMethods
    {
        public CareProviderStaffRoleTypeGroupsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CareProviderStaffRoleTypeGroups = By.Id("iframe_careproviderstaffroletypegroup");

        readonly By careProviderStaffRoleTypeGroupsPageHeader = By.XPath("//*[@id='CWToolbar']//h1[text()='Care Provider Staff Role Type Groups']");

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


        public CareProviderStaffRoleTypeGroupsPage WaitForCareProviderStaffRoleTypeGroupsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(careProviderStaffRoleTypeGroupsPageHeader);

            WaitForElementVisible(newRecordButton);
            WaitForElementVisible(exportToExcelButton);
            WaitForElementVisible(assignRecordButton);
            WaitForElementVisible(deleteRecordButton);

            return this;
        }

        public CareProviderStaffRoleTypeGroupsPage SelectSystemView(string ExpectedTextToSelect)
        {
            WaitForElementToBeClickable(viewSelector);
            ScrollToElement(viewSelector);
            SelectPicklistElementByText(viewSelector, ExpectedTextToSelect);

            return this;
        }

        public CareProviderStaffRoleTypeGroupsPage ValidateSelectedSystemView(string ExpectedText)
        {
            WaitForElementToBeClickable(viewSelector);
            ScrollToElement(viewSelector);
            ValidatePicklistSelectedText(viewSelector, ExpectedText);

            return this;
        }

        public CareProviderStaffRoleTypeGroupsPage InsertSearchQuery(string SearchQuery)
        {
            WaitForElementToBeClickable(searchTextBox);
            ScrollToElement(searchTextBox);
            SendKeys(searchTextBox, SearchQuery);

            return this;
        }

        public CareProviderStaffRoleTypeGroupsPage TapSearchButton()
        {
            WaitForElementToBeClickable(searchButton);
            ScrollToElement(searchButton);
            Click(searchButton);

            return this;
        }

        public CareProviderStaffRoleTypeGroupsPage SelectCareProviderStaffRoleTypeGroupRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            ScrollToElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public CareProviderStaffRoleTypeGroupsPage ValidateRecordCellContent(string RecordId, int CellPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(recordRowCell(RecordId, CellPosition));
            ValidateElementText(recordRowCell(RecordId, CellPosition), ExpectedText);

            return this;
        }

        public CareProviderStaffRoleTypeGroupsPage ValidateHeaderCellText(int CellPosition, string ExpectedText)
        {
            ScrollToElement(headerCell(CellPosition));
            WaitForElementVisible(headerCell(CellPosition));
            ValidateElementText(headerCell(CellPosition), ExpectedText);

            return this;
        }

        public CareProviderStaffRoleTypeGroupsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            ScrollToElement(newRecordButton);
            Click(newRecordButton);

            return this;
        }

        public CareProviderStaffRoleTypeGroupsPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID));
            ScrollToElement(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return this;
        }

        public CareProviderStaffRoleTypeGroupsPage ValidateRecordPresentOrNot(string RecordID, bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
                WaitForElementVisible(RecordIdentifier(RecordID));
            else
                WaitForElementNotVisible(RecordIdentifier(RecordID), 7);


            return this;
        }

        public CareProviderStaffRoleTypeGroupsPage ValidateNoRecordsMessageVisible()
        {
            WaitForElement(noRecordsMessage);
            WaitForElement(noResultsMessage);

            return this;
        }

        public CareProviderStaffRoleTypeGroupsPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(deleteRecordButton);
            ScrollToElement(deleteRecordButton);
            Click(deleteRecordButton);

            return this;
        }

        public CareProviderStaffRoleTypeGroupsPage ValidateGridHeaderCellSortAscendingOrder(int CellPosition, string ExpectedText)
        {
            ScrollToElement(gridPageHeaderElementSortAscendingOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortAscendingOrder(CellPosition));
            ValidateElementText(headerCell(CellPosition), ExpectedText);

            return this;
        }

    }
}

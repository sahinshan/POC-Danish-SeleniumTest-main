using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class RTTPathwaysSetupPage : CommonMethods
    {
        public RTTPathwaysSetupPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']//h1[text()='RTT Pathways Setup']");

        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");

        readonly By quickSearchTextbox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");
        readonly By viewSelector = By.Id("CWViewSelector");

        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");

        By recordCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");

        By recordCheckListItem(string recordID) => By.XPath("//div[@id = 'CWCheckList']/ul/li/*[@id='" + recordID + "'][1]");

        By gridPageHeaderElement(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");

        public RTTPathwaysSetupPage WaitForRTTPathwaysSetupPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(NewRecordButton);
            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(AssignRecordButton);
            WaitForElementVisible(DeleteRecordButton);

            return this;
        }

        public RTTPathwaysSetupPage InsertQuickSearchText(string Text)
        {
            WaitForElementVisible(quickSearchButton);
            MoveToElementInPage(quickSearchButton);
            SendKeys(quickSearchTextbox, Text);

            return this;
        }

        public RTTPathwaysSetupPage ClickQuickSearchButton()
        {
            WaitForElementToBeClickable(quickSearchButton);
            MoveToElementInPage(quickSearchButton);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public RTTPathwaysSetupPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            MoveToElementInPage(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public RTTPathwaysSetupPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            MoveToElementInPage(refreshButton);
            Click(refreshButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public RTTPathwaysSetupPage SelectRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordCheckListItem(RecordID));
            MoveToElementInPage(recordCheckListItem(RecordID));
            Click(recordCheckListItem(RecordID));

            return this;
        }

        public RTTPathwaysSetupPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordRow(RecordID));
            MoveToElementInPage(recordRow(RecordID));
            Click(recordRow(RecordID));

            return this;
        }

        public RTTPathwaysSetupPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            WaitForElementVisible(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public RTTPathwaysSetupPage ValidateHeaderCellText(int CellPosition, string ExpectedText)
        {
            MoveToElementInPage(gridPageHeaderElement(CellPosition));
            WaitForElementVisible(gridPageHeaderElement(CellPosition));
            ValidateElementText(gridPageHeaderElement(CellPosition), ExpectedText);

            return this;
        }

        public RTTPathwaysSetupPage SelectViewByText(string Text)
        {
            WaitForElementToBeClickable(viewSelector);
            SelectPicklistElementByText(viewSelector, Text);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public RTTPathwaysSetupPage ValidateRecordPresentOrNot(string RecordId, bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
                WaitForElementVisible(recordRow(RecordId));
            else
                WaitForElementNotVisible(recordRow(RecordId), 7);

            return this;
        }

    }
}

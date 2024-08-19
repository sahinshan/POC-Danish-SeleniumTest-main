using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class RTTTreatmentFunctionsPage : CommonMethods
    {
        public RTTTreatmentFunctionsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_RTTTreatmentFunctionCode = By.Id("iframe_rtttreatmentfunctioncode");

        readonly By rttTreatmentFunctionsPageHeader = By.XPath("//*[@id='CWToolbar']//h1[text()='RTT Treatment Functions']");

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

        By headerCell(int CellPosition) => By.XPath("//table[@id='CWGridHeader']//tr/th[" + CellPosition + "]/a/span");

        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");


        public RTTTreatmentFunctionsPage WaitForRTTTreatmentFunctionsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_RTTTreatmentFunctionCode);
            SwitchToIframe(iframe_RTTTreatmentFunctionCode);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(rttTreatmentFunctionsPageHeader);

            WaitForElementVisible(newRecordButton);
            WaitForElementVisible(exportToExcelButton);
            WaitForElementVisible(assignRecordButton);
            WaitForElementVisible(deleteRecordButton);

            return this;
        }

        public RTTTreatmentFunctionsPage SelectSystemView(string ExpectedTextToSelect)
        {
            WaitForElementToBeClickable(viewSelector);
            MoveToElementInPage(viewSelector);
            SelectPicklistElementByText(viewSelector, ExpectedTextToSelect);

            return this;
        }

        public RTTTreatmentFunctionsPage ValidateSelectedSystemView(string ExpectedText)
        {
            WaitForElementToBeClickable(viewSelector);
            MoveToElementInPage(viewSelector);
            ValidatePicklistSelectedText(viewSelector, ExpectedText);

            return this;
        }

        public RTTTreatmentFunctionsPage InsertSearchQuery(string SearchQuery)
        {
            WaitForElementToBeClickable(searchTextBox);
            MoveToElementInPage(searchTextBox);
            SendKeys(searchTextBox, SearchQuery);

            return this;
        }

        public RTTTreatmentFunctionsPage TapSearchButton()
        {
            WaitForElementToBeClickable(searchButton);
            MoveToElementInPage(searchButton);
            Click(searchButton);

            return this;
        }

        public RTTTreatmentFunctionsPage SelectContactReasonsRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            MoveToElementInPage(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public RTTTreatmentFunctionsPage ValidateRecordCellContent(string RecordId, int CellPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(recordRowCell(RecordId, CellPosition));
            ValidateElementText(recordRowCell(RecordId, CellPosition), ExpectedText);

            return this;
        }

        public RTTTreatmentFunctionsPage ValidateHeaderCellText(int CellPosition, string ExpectedText)
        {
            ScrollToElement(headerCell(CellPosition));
            WaitForElementVisible(headerCell(CellPosition));
            ValidateElementText(headerCell(CellPosition), ExpectedText);

            return this;
        }

        public RTTTreatmentFunctionsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            MoveToElementInPage(newRecordButton);
            Click(newRecordButton);

            return this;
        }

        public RTTTreatmentFunctionsPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID));
            MoveToElementInPage(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return this;
        }

        public RTTTreatmentFunctionsPage ValidateRecordPresentOrNot(string RecordID, bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
                WaitForElementVisible(RecordIdentifier(RecordID));
            else
                WaitForElementNotVisible(RecordIdentifier(RecordID), 7);


            return this;
        }

        public RTTTreatmentFunctionsPage ValidateNoRecordsMessageVisible()
        {
            WaitForElement(noRecordsMessage);
            WaitForElement(noResultsMessage);

            return this;
        }

    }
}

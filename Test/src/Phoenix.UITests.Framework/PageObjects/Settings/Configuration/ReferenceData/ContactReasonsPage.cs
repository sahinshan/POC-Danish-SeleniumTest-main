using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ContactReasonsPage : CommonMethods
    {
        public ContactReasonsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_ContactReasonsPage = By.Id("iframe_contactreason");

        readonly By ContactReasonsPageHeader = By.XPath("//*[@id='CWToolbar']//h1[text()='Contact Reasons']");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By searchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");

        readonly By noRecordsMessage = By.XPath("//*[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By noResultsMessage = By.XPath("//*[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");


        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");

        By recordRowCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");

        By headerCell(int CellPosition) => By.XPath("//table[@id='CWGridHeader']//tr/th[" + CellPosition + "]/a/span");

        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");


        public ContactReasonsPage WaitForContactReasonsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_ContactReasonsPage);
            SwitchToIframe(iframe_ContactReasonsPage);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(ContactReasonsPageHeader);

            return this;
        }

        public ContactReasonsPage SelectSystemView(string ExpectedTextToSelect)
        {
            WaitForElementToBeClickable(viewSelector);
            MoveToElementInPage(viewSelector);
            SelectPicklistElementByText(viewSelector, ExpectedTextToSelect);

            return this;
        }

        public ContactReasonsPage ValidateSelectedSystemView(string ExpectedText)
        {
            WaitForElementToBeClickable(viewSelector);
            MoveToElementInPage(viewSelector);
            ValidatePicklistSelectedText(viewSelector, ExpectedText);
            return this;
        }

        public ContactReasonsPage InsertSearchQuery(string SearchQuery)
        {
            WaitForElementToBeClickable(searchTextBox);
            MoveToElementInPage(searchTextBox);
            SendKeys(searchTextBox, SearchQuery);

            return this;
        }

        public ContactReasonsPage TapSearchButton()
        {
            WaitForElementToBeClickable(searchButton);
            MoveToElementInPage(searchButton);
            Click(searchButton);

            return this;
        }

        public ContactReasonsPage SelectContactReasonsRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            MoveToElementInPage(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public ContactReasonsPage ValidateRecordCellContent(string RecordId, int CellPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(recordRowCell(RecordId, CellPosition));
            ValidateElementText(recordRowCell(RecordId, CellPosition), ExpectedText);

            return this;
        }

        public ContactReasonsPage ValidateHeaderCellText(int CellPosition, string ExpectedText)
        {
            ScrollToElement(headerCell(CellPosition));
            ValidateElementText(headerCell(CellPosition), ExpectedText);

            return this;
        }

        public ContactReasonsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            MoveToElementInPage(newRecordButton);
            Click(newRecordButton);

            return this;
        }

        public ContactReasonsPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID));
            MoveToElementInPage(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return this;
        }

        public ContactReasonsPage ValidateRecordPresent(string RecordID)
        {
            WaitForElementVisible(RecordIdentifier(RecordID));

            return this;
        }

        public ContactReasonsPage ValidateNoRecordsMessageVisible()
        {
            WaitForElement(noRecordsMessage);
            WaitForElement(noResultsMessage);

            return this;
        }

    }
}

using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Customizations
{
    public class OptionSetsPage : CommonMethods
    {
        public OptionSetsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By quickSearchTextbox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");

        public OptionSetsPage WaitForOptionSetsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(pageHeader);
            ValidateElementText(pageHeader, "Option Sets");

            WaitForElement(quickSearchTextbox);
            WaitForElement(quickSearchButton);
            WaitForElement(refreshButton);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public OptionSetsPage InsertQuickSearchText(string Text)
        {
            WaitForElementVisible(quickSearchTextbox);
            MoveToElementInPage(quickSearchTextbox);
            SendKeys(quickSearchTextbox, Text);

            return this;
        }

        public OptionSetsPage ClickQuickSearchButton()
        {
            WaitForElementToBeClickable(quickSearchButton);
            MoveToElementInPage(quickSearchButton);
            Click(quickSearchButton);

            return this;
        }

        public OptionSetsPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordRow(RecordID));
            MoveToElementInPage(recordRow(RecordID));
            Click(recordRow(RecordID));

            return this;
        }

        public OptionSetsPage ValidateRecordIsPresent(string RecordID, bool IsPresent)
        {
            if (IsPresent)
                WaitForElementVisible(recordRow(RecordID));
            else
                WaitForElementNotVisible(recordRow(RecordID), 3);

            return this;
        }


    }
}

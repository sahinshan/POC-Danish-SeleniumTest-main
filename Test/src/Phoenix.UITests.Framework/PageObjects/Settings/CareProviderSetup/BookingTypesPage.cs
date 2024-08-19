using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Settings.CareProviderSetup
{
    public class BookingTypesPage : CommonMethods
    {
        public BookingTypesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Toolbar and Search View Panel
        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By quickSearchTextbox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        readonly By viewSelector = By.Id("CWViewSelector");
        #endregion

        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");

        public BookingTypesPage WaitForBookingTypesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 40);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);            

            WaitForElement(pageHeader);

            WaitForElement(newRecordButton);
            WaitForElement(deleteRecordButton);
            WaitForElement(viewSelector);
            WaitForElement(quickSearchTextbox);
            WaitForElement(quickSearchButton);
            WaitForElement(refreshButton);

            return this;
        }

        public BookingTypesPage ClickNewRecordButton()
        {
            ScrollToElement(newRecordButton);
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);

            return this;
        }

        public BookingTypesPage InsertQuickSearchText(string Text)
        {
            ScrollToElement(quickSearchTextbox);
            WaitForElementToBeClickable(quickSearchTextbox);
            SendKeys(quickSearchTextbox, Text);

            return this;
        }

        public BookingTypesPage ClickQuickSearchButton()
        {
            ScrollToElement(quickSearchButton);
            WaitForElementToBeClickable(quickSearchButton);
            Click(quickSearchButton);

            return this;
        }

        public BookingTypesPage ClickRefreshButton()
        {
            ScrollToElement(refreshButton);
            WaitForElementToBeClickable(refreshButton);
            Click(refreshButton);

            return this;
        }

        public BookingTypesPage ClickDeleteButton()
        {
            ScrollToElement(deleteRecordButton);
            WaitForElementToBeClickable(deleteRecordButton);
            Click(deleteRecordButton);

            return this;
        }

        public BookingTypesPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordRow(RecordID));
            ScrollToElement(recordRow(RecordID));
            Click(recordRow(RecordID));

            return this;
        }

        public BookingTypesPage ValidateBookingTypeRecordIsPresent(string RecordID, bool IsPresent)
        {
            if (IsPresent)
            {
                WaitForElementToBeClickable(recordRow(RecordID));
                ScrollToElement(recordRow(RecordID));
            }
            else
            {
                WaitForElementNotVisible(recordRow(RecordID), 3);
            }
            bool actualStatus = GetElementVisibility(recordRow(RecordID));
            Assert.AreEqual(IsPresent, actualStatus);

            return this;
        }
    }
}

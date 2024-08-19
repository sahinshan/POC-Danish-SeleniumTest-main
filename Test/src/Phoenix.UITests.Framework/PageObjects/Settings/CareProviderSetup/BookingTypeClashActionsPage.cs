using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Settings.CareProviderSetup
{
    public class BookingTypeClashActionsPage : CommonMethods
    {
        public BookingTypeClashActionsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Toolbar and Search View Panel

        readonly By CWContent_Iframe = By.Id("CWContentIFrame");
        readonly By cpBookingType_Iframe = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cpbookingtype&')]");
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//h1[text()='Booking Type Clash Actions']");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By quickSearchTextbox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");

        readonly By viewSelector = By.Id("CWViewSelector");

        #endregion

        By RecordCellText(int RowNumber, int CellPosition) => By.XPath("//*[@id='CWGrid']/tbody/tr[" + RowNumber + "]/td[" + CellPosition + "]");

        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");

        By recordCell(string recordID, int CellPosition) => By.XPath("//*[@id='" + recordID + "']/td[" + CellPosition + "]");

        public BookingTypeClashActionsPage WaitForBookingTypeClashActionsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(CWContent_Iframe);
            SwitchToIframe(CWContent_Iframe);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(cpBookingType_Iframe);
            SwitchToIframe(cpBookingType_Iframe);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(pageHeader);

            WaitForElement(newRecordButton);
            WaitForElement(exportToExcelButton);
            WaitForElement(viewSelector);
            WaitForElement(quickSearchTextbox);
            WaitForElement(quickSearchButton);
            WaitForElement(refreshButton);

            return this;
        }

        public BookingTypeClashActionsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            ScrollToElement(newRecordButton);
            Click(newRecordButton);

            return this;
        }

        public BookingTypeClashActionsPage InsertQuickSearchText(string Text)
        {
            WaitForElementToBeClickable(quickSearchTextbox);
            ScrollToElement(quickSearchTextbox);
            SendKeys(quickSearchTextbox, Text);

            return this;
        }

        public BookingTypeClashActionsPage ClickQuickSearchButton()
        {
            WaitForElementToBeClickable(quickSearchButton);
            ScrollToElement(quickSearchButton);
            Click(quickSearchButton);

            return this;
        }

        public BookingTypeClashActionsPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            ScrollToElement(refreshButton);
            Click(refreshButton);

            return this;
        }

        public BookingTypeClashActionsPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordRow(RecordID));
            ScrollToElement(recordRow(RecordID));
            Click(recordRow(RecordID));

            return this;
        }

        public BookingTypeClashActionsPage ValidateRecordCellContent(string recordID, int CellPosition, string ExpectedText)
        {
            WaitForElementVisible(recordCell(recordID, CellPosition));
            ScrollToElement(recordCell(recordID, CellPosition));
            ValidateElementTextContainsText(recordCell(recordID, CellPosition), ExpectedText);

            return this;
        }

        public BookingTypeClashActionsPage ValidateDeleteRecordButtonIsPresent(bool IsPresent)
        {
            if (IsPresent)
                WaitForElementVisible(deleteRecordButton);
            else
                WaitForElementNotVisible(deleteRecordButton, 3);

            return this;
        }

        public BookingTypeClashActionsPage ValidateBookingTypeClashActionRecordIsPresent(string RecordID, bool IsPresent)
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

        public BookingTypeClashActionsPage ValidateCellText(int RowNumber, int CellPosition, string expectedText)
        {
            ValidateElementText(RecordCellText(RowNumber, CellPosition), expectedText);

            return this;
        }

    }
}

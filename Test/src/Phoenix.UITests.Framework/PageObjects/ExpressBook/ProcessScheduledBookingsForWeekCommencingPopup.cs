using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.Providers
{
    public class ProcessScheduledBookingsForWeekCommencingPopup : CommonMethods
    {
        public ProcessScheduledBookingsForWeekCommencingPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id, iframe_CWDialog_)][contains(@src, 'type=cpexpressbookingcriteria&')]");
        readonly By iframe_CPExpressBookingScheduledBookings = By.Id("iframe_CPExpressBookingScheduledBookings");

        readonly By popupHeader = By.XPath("//*[@id='CWHeaderText']");
        By recordCheckbox(string recordId) => By.XPath("//*[@id='CHK_" + recordId + "']");

        readonly By closeButton = By.Id("CloseButton");
        readonly By closeAndSaveButton = By.Id("CloseAndSaveButton");

        readonly By noRecordMessage = By.XPath("//*[@id='CWGridHolder']/div[@class = 'alert alert-info norecords']/h2");

        public ProcessScheduledBookingsForWeekCommencingPopup WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad(bool IsSucceeded=true)
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(iframe_CPExpressBookingScheduledBookings);
            SwitchToIframe(iframe_CPExpressBookingScheduledBookings);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementVisible(closeButton);
            if (IsSucceeded)
                WaitForElementVisible(closeAndSaveButton);

            WaitForElementVisible(popupHeader);

            return this;
        }


        public ProcessScheduledBookingsForWeekCommencingPopup ClickCloseButton()
        {
            WaitForElementToBeClickable(closeButton);
            Click(closeButton);

            return this;

        }

        public ProcessScheduledBookingsForWeekCommencingPopup ClickCloseAndSaveButton()
        {
            WaitForElementToBeClickable(closeAndSaveButton);
            Click(closeAndSaveButton);

            return this;

        }

        public ProcessScheduledBookingsForWeekCommencingPopup ValidateRecordPresent(string RecordId)
        {
            WaitForElement(recordCheckbox(RecordId));
            return this;
        }

        public ProcessScheduledBookingsForWeekCommencingPopup ValidateRecordPresent(Guid RecordId)
        {
            return ValidateRecordPresent(RecordId.ToString());
        }

        public ProcessScheduledBookingsForWeekCommencingPopup ValidateRecordNotPresent(string RecordId)
        {
            WaitForElementNotVisible(recordCheckbox(RecordId), 3);
            return this;
        }

        public ProcessScheduledBookingsForWeekCommencingPopup ValidateRecordNotPresent(Guid RecordId)
        {
            return ValidateRecordNotPresent(RecordId.ToString());
        }

        public ProcessScheduledBookingsForWeekCommencingPopup ValidateNoRecordMessageVisible(bool isVisible)
        {
            if (isVisible)
                WaitForElementVisible(noRecordMessage);
            else
                WaitForElementNotVisible(noRecordMessage, 3);

            return this;
        }
    }
}
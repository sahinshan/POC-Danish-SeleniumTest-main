using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ClonePersonContractServiceRatePeriodPopup : CommonMethods
    {
        public ClonePersonContractServiceRatePeriodPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By popupIframe = By.Id("iframe_ClonePersonContractServiceRatePeriod");

        readonly By popupHeader = By.XPath("//h1[@id='CWHeaderTitle']");

        readonly By startDateFieldLabel = By.XPath("//label[@for='CWField_CloneStartDate']");
        readonly By startDateField = By.XPath("//*[@id='CWField_CloneStartDate']");
        readonly By bottomMessage = By.XPath("//*[@id='CWCloneDateMessage_Container']");

        readonly By notificationMessage = By.XPath("//*[@id='CWNotificationMessage_ClonePersonContractServiceRatePeriod']");
        readonly By notificationMessageHideLink = By.XPath("//*[@id='CWNotificationHolder_ClonePersonContractServiceRatePeriod']/a");

        readonly By cloneButton = By.Id("CWCopy");
        readonly By cancelButton = By.Id("CWCancel");



        public ClonePersonContractServiceRatePeriodPopup WaitForPopupToLoad()
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(popupIframe);
            SwitchToIframe(popupIframe);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(popupHeader);

            WaitForElement(startDateFieldLabel);
            WaitForElement(startDateField);
            WaitForElement(bottomMessage);

            WaitForElement(cloneButton);
            WaitForElement(cancelButton);

            return this;
        }

        public ClonePersonContractServiceRatePeriodPopup ValidatePopupHeaderText(string ExpectedText)
        {
            ValidateElementText(popupHeader, ExpectedText);

            return this;
        }

        public ClonePersonContractServiceRatePeriodPopup ValidateStartDateFieldLabelText(string ExpectedText)
        {
            ValidateElementText(startDateFieldLabel, ExpectedText);

            return this;
        }

        public ClonePersonContractServiceRatePeriodPopup ClickCloneButton()
        {
            WaitForElementToBeClickable(cloneButton);
            Click(cloneButton);
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public ClonePersonContractServiceRatePeriodPopup ClickCancelButton()
        {
            WaitForElementToBeClickable(cancelButton);
            Click(cancelButton);
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public ClonePersonContractServiceRatePeriodPopup InsertStartDate(string TextToInsert)
        {
            SendKeys(startDateField, TextToInsert + Keys.Tab);

            return this;
        }


        public ClonePersonContractServiceRatePeriodPopup ValidateNotificationMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(notificationMessage);
                WaitForElementVisible(notificationMessageHideLink);

            }
            else
            {
                WaitForElementNotVisible(notificationMessage, 7);
                WaitForElementNotVisible(notificationMessageHideLink, 7);
            }

            return this;
        }


        public ClonePersonContractServiceRatePeriodPopup ValidateNotificationMessageText(string ExpectedText)
        {
            ValidateElementText(notificationMessage, ExpectedText);

            return this;
        }


        public ClonePersonContractServiceRatePeriodPopup ClickNotificationMessageHideLinkButton()
        {
            WaitForElementToBeClickable(notificationMessageHideLink);
            Click(notificationMessageHideLink);

            return this;
        }

    }
}

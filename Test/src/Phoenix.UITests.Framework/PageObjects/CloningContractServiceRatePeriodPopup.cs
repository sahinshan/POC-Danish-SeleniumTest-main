using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CloningContractServiceRatePeriodPopup : CommonMethods
    {
        public CloningContractServiceRatePeriodPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By iframe_CloneContractServiceRatePeriod = By.Id("iframe_CloneContractServiceRatePeriod");

        readonly By popupHeader = By.XPath("//h1[@id='CWHeaderTitle']");

        readonly By notificationMessage = By.XPath("//*[@id='CWNotificationMessage_CloneContractServiceRatePeriod']");
        readonly By notificationMessageCloseButton = By.XPath("//*[@id='CWNotificationHolder_CloneContractServiceRatePeriod']/a");

        readonly By startDateField = By.Id("CWField_CloneStartDate");
        readonly By startDateErrorLabel = By.XPath("//label[@for='CWField_CloneStartDate'][@class='formerror']/span");
        
        readonly By bottomMessage = By.XPath("//*[@id='CWCloneDateMessage_Container']");

        readonly By cloneButton = By.Id("CWCopy");
        readonly By closeButton = By.Id("CWCancel");


        public CloningContractServiceRatePeriodPopup WaitForCloningContractServiceRatePeriodPopupToLoad()
        {

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(iframe_CloneContractServiceRatePeriod);
            SwitchToIframe(iframe_CloneContractServiceRatePeriod);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(popupHeader);

            WaitForElement(startDateField);
            WaitForElement(bottomMessage);

            return this;
        }

        public CloningContractServiceRatePeriodPopup InsertStartDate(string TextToInsert)
        {
            WaitForElementToBeClickable(startDateField);
            SendKeys(startDateField, TextToInsert);

            return this;
        }


        public CloningContractServiceRatePeriodPopup ClickCloneButton()
        {
            Click(cloneButton);
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public CloningContractServiceRatePeriodPopup ClickCloseButton()
        {
            Click(closeButton);
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public CloningContractServiceRatePeriodPopup ValidateStartDateErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(startDateErrorLabel);

            }
            else
            {
                WaitForElementNotVisible(startDateErrorLabel, 7);
            }

            return this;
        }

        public CloningContractServiceRatePeriodPopup ValidateStartDateErrorLabelText(string ExpectedText)
        {
            ValidateElementText(startDateErrorLabel, ExpectedText);

            return this;
        }

        public CloningContractServiceRatePeriodPopup ValidateNotificationMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(notificationMessage);
                WaitForElementVisible(notificationMessageCloseButton);
            }
            else
            {
                WaitForElementNotVisible(notificationMessage, 7);
                WaitForElementNotVisible(notificationMessageCloseButton, 7);
            }

            return this;
        }

        public CloningContractServiceRatePeriodPopup ValidateNotificationMessageText(string ExpectedText)
        {
            ValidateElementText(notificationMessage, ExpectedText);

            return this;
        }

    }
}

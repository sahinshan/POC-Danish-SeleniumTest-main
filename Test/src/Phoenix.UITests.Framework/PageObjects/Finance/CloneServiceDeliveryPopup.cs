using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CloneServiceDeliveryPopup : CommonMethods
    {
        public CloneServiceDeliveryPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By popupIframe = By.Id("iframe_CloneServiceDelivery");

        readonly By popupHeader = By.XPath("//h1[@id='CWHeaderTitle']");

        readonly By confirmationMessage = By.XPath("//*[@id='CWCloneConfirmationMsg']");

        readonly By successMessageArea = By.XPath("//*[@id='CWNotificationHolder_CloneServiceDelivery']");
        readonly By successMessage = By.XPath("//*[@id='CWNotificationMessage_CloneServiceDelivery']");
        readonly By successMessageHideLink = By.XPath("//*[@id='CWNotificationHolder_CloneServiceDelivery']/a");

        readonly By cloneButton = By.Id("CWCopy");
        readonly By cancelButton = By.Id("CWCancel");



        public CloneServiceDeliveryPopup WaitForCloneServiceDeliveryPopupToLoad()
        {
            WaitForElement(popupIframe);
            SwitchToIframe(popupIframe);

            WaitForElement(popupHeader);

            WaitForElement(confirmationMessage);

            WaitForElement(cloneButton);
            WaitForElement(cancelButton);

            return this;
        }

        public CloneServiceDeliveryPopup ClickCloneButton()
        {
            WaitForElementToBeClickable(cloneButton);
            Click(cloneButton);
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public CloneServiceDeliveryPopup ClickCancelButton()
        {
            WaitForElementToBeClickable(cancelButton);
            Click(cancelButton);
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public CloneServiceDeliveryPopup ValidateNotificationMessageVisibility(bool ExpectVisible)
        {
            if(ExpectVisible)
            {
                WaitForElementVisible(confirmationMessage);

            }
            else
            {
                WaitForElementNotVisible(confirmationMessage, 7);
            }

            return this;
        }

        public CloneServiceDeliveryPopup ValidateNotificationMessageText(string ExpectedText)
        {
            ValidateElementText(confirmationMessage, ExpectedText);

            return this;
        }


        public CloneServiceDeliveryPopup ValidateSuccessMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(successMessageArea);
                WaitForElementVisible(successMessage);
                WaitForElementVisible(successMessageHideLink);

            }
            else
            {
                WaitForElementNotVisible(successMessageArea, 7);
                WaitForElementNotVisible(successMessage, 7);
                WaitForElementNotVisible(successMessageHideLink, 7);
            }

            return this;
        }


        public CloneServiceDeliveryPopup ValidateSuccessMessageText(string ExpectedText)
        {
            ValidateElementText(successMessage, ExpectedText);

            return this;
        }


        public CloneServiceDeliveryPopup ClickSuccessMessageHideLinkButton()
        {
            WaitForElementToBeClickable(successMessageHideLink);
            Click(successMessageHideLink);

            return this;
        }

    }
}

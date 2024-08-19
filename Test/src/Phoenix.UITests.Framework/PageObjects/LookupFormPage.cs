using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class LookupFormPage : CommonMethods
    {
        public LookupFormPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By popupIframe = By.Id("iframe_CWLookupFormPage");

        readonly By popupHeader = By.XPath("//*[@id='CWHeader']/header/h1");

        readonly By ViewButton = By.Id("CWViewLookupFormButton");

        readonly By CloseButton = By.Id("CWCloseLookupFormButton");

        readonly By recordNotAccessible_notificationMessage = By.Id("CWNotificationMessage_CWBase");
        
        readonly By recordNotAccessibleNotification_CloseButton = By.XPath("//button[text() = 'Close']");


        public LookupFormPage WaitForLookupFormPageToLoad()
        {
            WaitForElement(popupIframe);
            SwitchToIframe(popupIframe);

            WaitForElement(popupHeader);

            WaitForElement(ViewButton);
            WaitForElement(CloseButton);

            return this;
        }

        public LookupFormPage WaitForRecordCannotBeAccessedMessageAreaToLoad()
        {
            WaitForElement(popupIframe);
            SwitchToIframe(popupIframe);

            return this;
        }

        public LookupFormPage ClickViewButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElement(ViewButton);
            Click(ViewButton);

            return this;
        }

        public LookupFormPage ClickCloseButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElement(CloseButton);
            Click(CloseButton);

            return this;
        }

        public LookupFormPage ValidateRecordInaccessibleNotificationMessage(string ExpectedMessage)
        {
            WaitForElementVisible(recordNotAccessible_notificationMessage);

            ValidateElementText(recordNotAccessible_notificationMessage, ExpectedMessage);

            return this;
        }

        public LookupFormPage CloseRecordCannotBeDisplayedNotificationMessage()
        {
            WaitForElementToBeClickable(recordNotAccessibleNotification_CloseButton);
            ScrollToElement(recordNotAccessibleNotification_CloseButton);
            Click(recordNotAccessibleNotification_CloseButton);
            return this;
        }

    }

}


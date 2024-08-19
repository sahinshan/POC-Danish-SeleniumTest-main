using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    /// <summary>
    /// this class represents the window pop-up that is displayed when a user clicks on a business object link button (e.g. with a link to a person record , case record, etc)
    /// </summary>
    public class LookupViewPopup : CommonMethods
    {
        public LookupViewPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By popupIframe = By.Id("iframe_CWLookupFormPage");

        readonly By popupHeader = By.XPath("//*[@id='CWHeader']/header/h1/small");
        readonly By closeButton = By.XPath("//button[text()='Close']");

        readonly By viewButton = By.Id("CWViewLookupFormButton");
        readonly By CWNotificationMessage_CWBase = By.Id("CWNotificationMessage_CWBase");


        public LookupViewPopup WaitForLookupViewPopupToLoad()
        {
            Wait.Until(c => c.FindElement(popupIframe));
            driver.SwitchTo().Frame(driver.FindElement(popupIframe));

            Wait.Until(c => c.FindElement(popupHeader));

            Wait.Until(c => c.FindElement(viewButton));
            return this;
        }

        public LookupViewPopup WaitForAlertLookupViewPopupToLoad()
        {
            Wait.Until(c => c.FindElement(popupIframe));
            driver.SwitchTo().Frame(driver.FindElement(popupIframe));

            Wait.Until(c => c.FindElement(CWNotificationMessage_CWBase));

            Wait.Until(c => c.FindElement(closeButton));
            return this;
        }

        public LookupViewPopup ClickViewButton()
        {
            this.Click(viewButton);

            return this;
        }

        public LookupViewPopup ClickCloseButton()
        {
            this.Click(closeButton);

            return this;
        }

        public LookupViewPopup ValidateAlertCWNotificationMessage()
        {
            ValidateElementTextContainsText(CWNotificationMessage_CWBase, "The record you are attempting to access cannot be displayed because it is either deleted or your security privileges prevent access.");

            return this;
        }

    }
}

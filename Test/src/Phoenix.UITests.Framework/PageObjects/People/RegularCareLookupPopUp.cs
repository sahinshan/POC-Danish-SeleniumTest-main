using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Phoenix.UITests.Framework.PageObjects
{
    /// <summary>
    /// this class represents the window pop-up that is displayed when a user clicks on a business object link button (e.g. with a link to a person record , case record, etc)
    /// </summary>
    public class RegularCareLookupPopUp : CommonMethods
    {
        public RegularCareLookupPopUp(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By popupIframe = By.Id("iframe_CWLookupForm");
        readonly By popupHeader = By.XPath("//*[@id='CWHeader']//h1");
        readonly By closeButton = By.XPath("//button[text()='Close']");

        readonly By viewButton = By.Id("CWViewLookupFormButton");
        readonly By CWNotificationMessage_CWBase = By.Id("CWNotificationMessage_CWBase");
        readonly By RegularCareLookup_LookInDropDown = By.Id("CWViewSelector");
        
        public RegularCareLookupPopUp WaitForLookupViewPopupToLoad()
        {
            Wait.Until(c => c.FindElement(popupIframe));
            driver.SwitchTo().Frame(driver.FindElement(popupIframe));

            Wait.Until(c => c.FindElement(popupHeader));

            Wait.Until(c => c.FindElement(closeButton));
            return this;
        }
     

        public RegularCareLookupPopUp ClickCloseButton()
        {
            this.Click(closeButton);
            return this;
        }

        public RegularCareLookupPopUp ValidateLookINDropDownText(string ExpectedText)
        {
            System.Threading.Thread.Sleep(1000);
            WaitForElement(RegularCareLookup_LookInDropDown);
            ValidatePicklistContainsElementByText(RegularCareLookup_LookInDropDown, ExpectedText);
            return this;
        }


    }
}

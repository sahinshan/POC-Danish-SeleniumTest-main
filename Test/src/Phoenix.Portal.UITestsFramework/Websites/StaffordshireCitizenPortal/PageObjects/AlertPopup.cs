using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.Portal.UITestsFramework.Websites.StaffordshireCitizenPortal.PageObjects
{
    public class AlertPopup : CommonMethods
    {
        private IAlert alert;

        public AlertPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        
        public AlertPopup WaitForAlertPopupToLoad()
        {
            this.alert = WaitForAlert();

            return this;
        }

        public AlertPopup ValidateAlertText(string ExpectedText)
        {
            Assert.AreEqual(ExpectedText, this.alert.Text);

            return this;
        }

        public AlertPopup TapOKButton()
        {
            this.alert.Accept();

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public AlertPopup TapCancelButton()
        {
            this.alert.Dismiss();

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        private IAlert WaitForAlert()
        {
            int i = 0;
            while (i++ < 15)
            {
                try
                {
                    return driver.SwitchTo().Alert();
                }
                catch (NoAlertPresentException e)
                {
                    Thread.Sleep(1000);
                    continue;
                }
            }

            throw new Exception("No Alert was displayed after 5 seconds");
        }

    }
}

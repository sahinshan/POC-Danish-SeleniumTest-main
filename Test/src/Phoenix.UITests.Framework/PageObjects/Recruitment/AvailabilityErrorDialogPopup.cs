using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class AvailabilityErrorDialogPopup : CommonMethods
    {
        public AvailabilityErrorDialogPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By problematicDiv = By.XPath("//mcc-dialog/div");

        readonly By dialogHeader = By.XPath("//mcc-dialog/div/dialog/section/div/h2");
        readonly By dialogMessage = By.XPath("//mcc-dialog/div/dialog/section/div/div");
        
        readonly By closeButton = By.Id("SA-dialog-dismiss");

        public AvailabilityErrorDialogPopup WaitForAvailabilityErrorDialogPopupPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(problematicDiv);
            SetAttributeValue(problematicDiv, "class", "mcc-dialog");

            WaitForElementVisible(dialogHeader);
            WaitForElement(dialogMessage);
            WaitForElementVisible(closeButton);

            return this;
        }

        public AvailabilityErrorDialogPopup ClickOnCloseButton()
        {
            WaitForElementToBeClickable(closeButton);
            Click(closeButton);

            return this;
        }

        public AvailabilityErrorDialogPopup ValidateDialogText(string ExpectedText)
        {
            ValidateElementText(dialogMessage, ExpectedText);
            return this;
        }


    }
}

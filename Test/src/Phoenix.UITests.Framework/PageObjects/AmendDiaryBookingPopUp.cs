using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class AmendDiaryBookingPopUp : CommonMethods
    {
        public AmendDiaryBookingPopUp(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        

        readonly By dialogHeader = By.XPath("//section/div[@class='mcc-dialog__header']");
        private static string dialogMessage1Id = "id--fields--info";
        readonly By dialogMessage1 = By.XPath("//div[@id='id--fields--info']");
        readonly By dialogMessage2 = By.XPath("//div[@id='id--fields--link']");

        readonly By closeButton = By.XPath("//div[@id='id--footer--closeButton']");

        public AmendDiaryBookingPopUp WaitForAmendDiaryBookingPopUpPopupPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(dialogHeader);
            WaitForElement(dialogMessage1);
            WaitForElement(dialogMessage2);
            WaitForElement(closeButton);

            return this;
        }

        public AmendDiaryBookingPopUp ClickOnCloseButton()
        {
            WaitForElement(closeButton);
            ClickSpecial(closeButton);
            return this;
        }

        public AmendDiaryBookingPopUp ValidateDialogText(string ExpectedText)
        {
            var elementText = GetElementTextByJavascript(dialogMessage1Id);
            Assert.AreEqual(ExpectedText, elementText);
            return this;
        }

       

    }
}

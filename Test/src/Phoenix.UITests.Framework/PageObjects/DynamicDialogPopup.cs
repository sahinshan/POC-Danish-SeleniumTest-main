using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    /// this class represents the dynamic dialog that is displayed with messages to the user.
    /// </summary>
    public class DynamicDialogPopup : CommonMethods
    {
        public DynamicDialogPopup(IWebDriver driver, WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");

        readonly By popupHeader = By.XPath("//*[@id='CWDynamicDialog']/div//h2");

        readonly By message = By.XPath("//div[@id = 'CWDynamicDialog']//div/p");

        readonly By closeButton = By.Id("CWDynamicDialog_CloseButton");
        readonly By okButton = By.Id("CWOKButton");


        public DynamicDialogPopup WaitForDynamicDialogPopupToLoad()
        {
            WaitForElement(popupHeader);
            WaitForElement(message);
            WaitForElement(closeButton);

            return this;
        }

        public DynamicDialogPopup WaitForCareCloudDynamicDialoguePopUpToLoad()
        {

            WaitForElement(popupHeader);
            WaitForElement(message);
            WaitForElement(closeButton);

            return this;
        }

        public DynamicDialogPopup WaitForUnauthorizedAccessDynamicDialogPopUpToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);
            
            WaitForElement(message);
            WaitForElement(closeButton);

            return this;
        }


        public DynamicDialogPopup ValidateDynamicDialogPopupNotDisplayed()
        {
            WaitForElementNotVisible(popupHeader, 3);
            WaitForElementNotVisible(message, 3);
            WaitForElementNotVisible(closeButton, 3);

            return this;
        }

        public DynamicDialogPopup TapCloseButton()
        {
            WaitForElementToBeClickable(closeButton);
            ScrollToElement(closeButton);
            Click(closeButton);

            return this;
        }

        public DynamicDialogPopup ValidateMessage(string ExpectedMessage)
        {
            WaitForElementVisible(message);
            string ActualMessage = GetElementText(message);
            Assert.AreEqual(ExpectedMessage, ActualMessage);

            return this;
        }

        public DynamicDialogPopup ValidateMessageContainsText(string ExpectedMessage)
        {
            WaitForElementVisible(message);            
            string ActualMessage = GetElementText(message);
            Assert.IsTrue(ActualMessage.Contains(ExpectedMessage));

            return this;
        }

        public DynamicDialogPopup ValidateMessageDoesNotContainText(string ExpectedMessage)
        {
            WaitForElementVisible(message);
            string ActualMessage = GetElementText(message);
            Assert.IsFalse(ActualMessage.Contains(ExpectedMessage));

            return this;
        }

        public DynamicDialogPopup TapOkButton()
        {
            Click(okButton);

            return this;
        }

    }
}

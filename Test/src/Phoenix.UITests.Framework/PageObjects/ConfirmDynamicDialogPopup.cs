using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{

    /// <summary>
    /// this class represents the dynamic dialog that is displayed with messages to the user.
    /// </summary>
    public class ConfirmDynamicDialogPopup : CommonMethods
    {
        public ConfirmDynamicDialogPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By popupHeader = By.XPath("//*[@id='CWConfirmDynamicDialog']/div/section/div/h2");
        readonly By message = By.XPath("//*[@id='CWConfirmDynamicDialog']/div/section/div/p");
        readonly By okButton = By.XPath("//*[@id = 'CWConfirmDynamicDialog']//button[@id = 'CWOKButton']");
        readonly By cancelButton = By.XPath("//*[@id = 'CWConfirmDynamicDialog']//button[@id = 'CWCancelButton']");

        
        public ConfirmDynamicDialogPopup WaitForConfirmDynamicDialogPopupToLoad()
        {
            WaitForElement(popupHeader);
            WaitForElement(message);
            WaitForElement(okButton);
            WaitForElement(cancelButton);

            return this;
        }

        public ConfirmDynamicDialogPopup ValidateConfirmDynamicDialogPopupNotDisplayed()
        {
            WaitForElementNotVisible(popupHeader, 3);
            WaitForElementNotVisible(message, 3);
            WaitForElementNotVisible(okButton, 3);
            WaitForElementNotVisible(cancelButton, 3);

            return this;
        }

        public ConfirmDynamicDialogPopup TapOKButton()
        {
            Click(okButton);

            return this;
        }

        public ConfirmDynamicDialogPopup TapCancelButton()
        {
            ScrollToElement(cancelButton);
            Click(cancelButton);

            return this;
        }

        public ConfirmDynamicDialogPopup ValidateMessage(string ExpectedMessage)
        {
            WaitForElementVisible(message);
            ValidateElementText(message, ExpectedMessage);

            return this;
        }

        public ConfirmDynamicDialogPopup ValidateMessageTextContains(string ExpectedMessage)
        {
            ValidateElementTextContainsText(message, ExpectedMessage);

            return this;
        }

    }
}

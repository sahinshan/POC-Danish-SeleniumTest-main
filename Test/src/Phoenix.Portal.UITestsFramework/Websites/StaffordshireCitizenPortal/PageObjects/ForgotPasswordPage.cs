using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;



namespace Phoenix.Portal.UITestsFramework.Websites.StaffordshireCitizenPortal.PageObjects
{
    public class ForgotPasswordPage : CommonMethods
    {
        public ForgotPasswordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By pageTitle = By.XPath("//*[@id='CWContent']/mosaic-page-header/div/div/div/div/h1[text()='Forgot password']");

        readonly By instructionsLabel = By.XPath("//*[@id='CWResetPasswordControls']/p");
        readonly By emailLabel = By.XPath("//*[@id='CWUsername']/label");
        readonly By emailField = By.XPath("//*[@id='CWUsername-input']");
        readonly By resetPasswordButton = By.XPath("//*[@id='CWBtnReset']/button");

        readonly By confirmationMessage = By.XPath("//*[@id='CWResetPasswordMessage']/mosaic-alert/div/p[contains(text(),'If an account is registered with this e-mail address we will send an e-mail with instructions to reset your password.')]");


        public ForgotPasswordPage WaitForForgotPasswordPageToLoad()
        {
            this.WaitForBrowserWindowTitle("Forgot password");

            WaitForElement(pageTitle);
            WaitForElement(instructionsLabel);
            WaitForElement(emailLabel);
            WaitForElement(emailField);
            WaitForElement(resetPasswordButton);


            return this;
        }

        public ForgotPasswordPage InsertEmailAddress(string ValueToInsert)
        {
            SendKeys(emailField, ValueToInsert);

            return this;
        }

        public ForgotPasswordPage ClickResetPasswordButton()
        {
            Click(resetPasswordButton);

            return this;
        }


        public ForgotPasswordPage ValidateConfirmationMessageVisible()
        {
            WaitForElementVisible(confirmationMessage);

            return this;
        }





    }
    
}

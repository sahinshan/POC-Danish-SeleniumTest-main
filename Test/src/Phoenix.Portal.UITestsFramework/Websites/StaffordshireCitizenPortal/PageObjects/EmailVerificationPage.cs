using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;



namespace Phoenix.Portal.UITestsFramework.Websites.StaffordshireCitizenPortal.PageObjects
{
    public class EmailVerificationPage : CommonMethods
    {
        public EmailVerificationPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By verifyEmailButton = By.XPath("//*[@id='CWVerifyEmailButton']");
        readonly By messageArea = By.XPath("//*[@id='my-rows']/mosaic-row/mosaic-col/mosaic-alert/div/p");
        readonly By errorMessageArea = By.XPath("//*[@id='CWAlertErrorMessage']");
        readonly By GoToHomePageLink = By.XPath("//*[@id='my-rows']/mosaic-row/mosaic-col/mosaic-button/a/div[text()='Go to Home Page']");



        public EmailVerificationPage GoToEmailVerificationPage(string EmailVerificationPageURL)
        {
            driver.Navigate().GoToUrl(EmailVerificationPageURL);

            return this;
        }


        public EmailVerificationPage WaitForEmailVerificationPageToLoad()
        {
            this.WaitForBrowserWindowTitle("Email verification");

            WaitForElement(messageArea);
            WaitForElement(GoToHomePageLink);

            return this;
        }

        public EmailVerificationPage ClickGoToHomePageButton()
        {
            Click(GoToHomePageLink);

            return this;
        }

        public EmailVerificationPage ValidateMessageAreaText(string ExpectedText)
        {
            WaitForElementVisible(messageArea);

            ValidateElementText(messageArea, ExpectedText);

            return this;
        }

        public EmailVerificationPage ValidateErrorMessageAreaText(string ExpectedText)
        {
            WaitForElementVisible(errorMessageArea);

            ValidateElementText(errorMessageArea, ExpectedText);

            return this;
        }

        public EmailVerificationPage ValidateVerifyEmailButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(verifyEmailButton);
            else
                WaitForElementNotVisible(verifyEmailButton, 7);

            return this;
        }

        public EmailVerificationPage ClickVerifyEmailButton()
        {
            WaitForElementToBeClickable(verifyEmailButton);
            Click(verifyEmailButton);

            return this;
        }

    }
    
}

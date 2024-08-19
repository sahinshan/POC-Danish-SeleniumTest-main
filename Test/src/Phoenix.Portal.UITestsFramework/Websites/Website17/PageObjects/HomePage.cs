using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;



namespace Phoenix.Portal.UITestsFramework.Websites.Website17.PageObjects
{
    public class HomePage : CommonMethods
    {
        public HomePage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        By styleSheetFile(string StileSheetFileName) => By.XPath("//link[@href='/portalwebsite/resources/" + StileSheetFileName + "']");


        readonly By RegisterButton = By.XPath("//mosaic-button/a[@href='/registration']");




        readonly By loginMessage = By.XPath("//*[@id='CWLoginContainer']/mosaic-card-header/div[1]/div[text()='Already Registered? Login Now']");
        readonly By step1Message = By.XPath("//*[@id='CWLoginContainer']/mosaic-card-body/div/h3");
        readonly By emailAddressLabel = By.XPath("//*[@id='CWUsername']/label");
        readonly By passwordAddressLabel = By.XPath("//*[@id='CWPassword']/label");
        readonly By emailAdress_Field = By.XPath("//*[@id='CWUsername-input']");
        readonly By password_Field = By.XPath("//*[@id='CWPassword-input']");
        readonly By pin_Field = By.XPath("//*[@id='CWPin-input']");

        readonly By loginButton = By.XPath("//*[@id='CWLoginButton']/button");
        readonly By validatePinButton = By.XPath("//*[@id='CWPinButton']/button");

        readonly By errorLabel = By.XPath("//*[@id='CWErrorMessage']");
        readonly By warningLabel = By.XPath("//*[@id='CWWarningMessage']");
        readonly By forgotPasswordLink = By.XPath("//*[@id='ForgotPasswordLink']");

        readonly By reIssuePinLink = By.XPath("//*[@id='CWReIssuePin']");

        readonly By SendVerificationEmailLink = By.XPath("//*[@id='CWReSendVerificationEmail']");


        readonly By footerContainer = By.Id("footer-container");



        public HomePage GoToHomePage()
        {
            driver.Navigate().GoToUrl(appURL);

            return this;
        }

        public HomePage WaitForHomePageToLoad()
        {
            WaitForElement(RegisterButton);

            WaitForElement(loginMessage);
            WaitForElement(step1Message);
            WaitForElement(emailAddressLabel);
            WaitForElement(passwordAddressLabel);
            WaitForElement(emailAdress_Field);
            WaitForElement(password_Field);
            WaitForElement(loginButton);
            WaitForElement(forgotPasswordLink);

            WaitForElement(footerContainer);



            return this;
        }

        public HomePage WaitForPinFieldVisible()
        {
            WaitForElementVisible(pin_Field);

            return this;
        }
        public HomePage WaitForValidatePinButtonVisible()
        {
            WaitForElementVisible(validatePinButton);

            return this;
        }



        public HomePage ClickRegisterButton()
        {
            Click(RegisterButton);

            return this;
        }

        public HomePage ClickLoginButton()
        {
            Click(loginButton);

            return this;
        }

        public HomePage ClickValidatePinButton()
        {
            Click(validatePinButton);

            return this;
        }

        public HomePage ClickForgotPasswordLink()
        {
            Click(forgotPasswordLink);

            return this;
        }

        public HomePage ClickReIssuePinLink()
        {
            Click(reIssuePinLink);

            return this;
        }
        public HomePage ClickSendVerificationEmailLink()
        {
            Click(SendVerificationEmailLink);

            return this;
        }

        public HomePage InsertUserName(string UserName)
        {
            SendKeys(emailAdress_Field, UserName);

            return this;
        }

        public HomePage InsertPassword(string Password)
        {
            SendKeys(password_Field, Password);

            return this;
        }
        public HomePage InsertPIN(string Password)
        {
            SendKeys(pin_Field, Password);

            return this;
        }


        public HomePage ValidateSendVerificationEmailLinkVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(SendVerificationEmailLink);
            else
                WaitForElementNotVisible(SendVerificationEmailLink, 5);


            return this;
        }

        public HomePage ValidateErrorMessageVisible()
        {
            WaitForElementVisible(errorLabel);

            return this;
        }

        public HomePage ValidateErrorMessageVisible(bool ExpectVisible)
        {
            if(ExpectVisible)
                WaitForElementVisible(errorLabel);
            else
                WaitForElementNotVisible(errorLabel, 5);


            return this;
        }

        public HomePage ValidateErrorMessage(string ExpectedText)
        {
            ValidateElementText(errorLabel, ExpectedText);

            return this;
        }

        public HomePage ValidateWarningMessage(string ExpectedText)
        {
            ValidateElementText(warningLabel, ExpectedText);

            return this;
        }



        public HomePage ValidateStyleSheetFile(string FileName)
        {
            WaitForElement(styleSheetFile(FileName));
            return this;
        }

        public HomePage ValidateCssPropertyForFooterContainer(string CSSPropertyName, string ExpectedValue)
        {
            ValidateElementCSSPropertyValue(footerContainer, CSSPropertyName, ExpectedValue);

            return this;
        }



    }

}

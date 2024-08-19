using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;



namespace Phoenix.Portal.UITestsFramework.Websites.StaffordshireCitizenPortal.PageObjects
{
    public class ResetPasswordPage : CommonMethods
    {
        public ResetPasswordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By pageTitle = By.XPath("//mosaic-card-header/div/div[text()='Reset Password']");

        readonly By instructionsLabel = By.XPath("//*[@id='CWResetPasswordControls']/p");

        readonly By newPasswordLabel = By.XPath("//*[@id='CWNewPassword1']/label");
        readonly By repeatNewPasswordLabel = By.XPath("//*[@id='CWNewPassword2']/label");

        readonly By newPasswordField = By.Id("CWNewPassword1-input");
        readonly By repeatNewPasswordField = By.Id("CWNewPassword2-input");

        readonly By newPasswordErrorSpan = By.XPath("//*[@id='CWNewPassword1']/div[3]/span");
        readonly By repeatNewPasswordErrorSpan = By.XPath("//*[@id='CWNewPassword2']/div[3]/span");

        readonly By resetPasswordButton = By.XPath("//*[@id='CWBtnReset']/button");

        readonly By errorMessage = By.XPath("//*[@id='CWErrorMessage']");

        readonly By successMessage = By.XPath("//*[@id='CWSuccessMessageText']");

        readonly By GoToHomePageLink = By.XPath("//*[@id='CWSuccessMessage']/div/mosaic-link/a");

        readonly By generalErrorMessage = By.XPath("//*[@id='CWErrorReset']/div/p");


        readonly By PasswordPromptTitle = By.XPath("//*[@id='PasswordPromptTitle']");
        readonly By PasswordPromptMinUpper = By.XPath("//*[@id='PasswordPromptMinUpper']");
        readonly By PasswordPromptMinLower = By.XPath("//*[@id='PasswordPromptMinLower']");
        readonly By PasswordPromptMinNum = By.XPath("//*[@id='PasswordPromptMinNum']");
        readonly By PasswordPromptMinSpecialChar = By.XPath("//*[@id='PasswordPromptMinSpecialChar']");
        readonly By PasswordPromptMinLength = By.XPath("//*[@id='PasswordPromptMinLength']");

        readonly By PasswordPromptMinUpperOkIcon = By.XPath("//*[@id='checkMinUpper']");
        readonly By PasswordPromptMinUpperErrorIcon = By.XPath("//*[@id='checkMinUpperError']");
        readonly By PasswordPromptMinLowerOkIcon = By.XPath("//*[@id='checkMinLower']");
        readonly By PasswordPromptMinLowerErrorIcon = By.XPath("//*[@id='checkMinLowerError']");
        readonly By PasswordPromptMinNumOkIcon = By.XPath("//*[@id='checkMinNum']");
        readonly By PasswordPromptMinNumErrorIcon = By.XPath("//*[@id='checkMinNumError']");
        readonly By PasswordPromptMinSpecialCharOkIcon = By.XPath("//*[@id='checkMinSpecialChar']");
        readonly By PasswordPromptMinSpecialCharErrorIcon = By.XPath("//*[@id='checkMinSpecialCharError']");
        readonly By PasswordPromptMinLengthOkIcon = By.XPath("//*[@id='checkMinLength']");
        readonly By PasswordPromptMinLengthErrorIcon = By.XPath("//*[@id='checkMinLengthError']");





        public ResetPasswordPage WaitForResetPasswordPageToLoad()
        {
            this.WaitForBrowserWindowTitle("Enter your current and new password");

            WaitForElement(pageTitle);

            WaitForElement(instructionsLabel);

            WaitForElement(newPasswordLabel);
            WaitForElement(repeatNewPasswordLabel);

            WaitForElement(newPasswordField);
            WaitForElement(repeatNewPasswordField);

            WaitForElement(resetPasswordButton);


            return this;
        }

        public ResetPasswordPage WaitForResetPasswordErrorPageToLoad()
        {
            this.WaitForBrowserWindowTitle("Reset Password - Consumer Portal");

            WaitForElement(generalErrorMessage);

            return this;
        }

        public ResetPasswordPage WaitForResetPasswordErrorPageToLoad(string ExpectedWindowTitle)
        {
            this.WaitForBrowserWindowTitle(ExpectedWindowTitle);

            WaitForElement(generalErrorMessage);

            return this;
        }

        public ResetPasswordPage GoToResetPasswordPage(string ResetPasswordURL)
        {
            driver.Navigate().GoToUrl(ResetPasswordURL);

            driver.Navigate().Refresh();

            return this;
        }
        public ResetPasswordPage InsertNewPassword(string TextToInsert)
        {
            SendKeys(newPasswordField, TextToInsert);
            SendKeysWithoutClearing(newPasswordField, Keys.Tab);

            return this;
        }
        public ResetPasswordPage InsertRepeatNewPassword(string TextToInsert)
        {
            SendKeys(repeatNewPasswordField, TextToInsert);
            SendKeysWithoutClearing(newPasswordField, Keys.Tab);

            return this;
        }
        public ResetPasswordPage ClickResetPasswordButton()
        {
            Click(resetPasswordButton);

            return this;
        }
        public ResetPasswordPage ValidateErrorMessageVisible()
        {
            WaitForElementVisible(errorMessage);

            return this;
        }
        public ResetPasswordPage ValidateErrorMessageText(string ExpectedText)
        {
            ValidateElementText(errorMessage, ExpectedText);

            return this;
        }
        public ResetPasswordPage ValidateSuccessMessageVisible()
        {
            WaitForElementVisible(successMessage);

            return this;
        }
        public ResetPasswordPage ValidateSuccessMessageText(string ExpectedText)
        {
            ValidateElementText(successMessage, ExpectedText);

            return this;
        }


        public ResetPasswordPage ValidateNewPasswordErrorSpanVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(newPasswordErrorSpan);
            else
                WaitForElementNotVisible(newPasswordErrorSpan, 7);

            return this;
        }
        public ResetPasswordPage ValidateRepeatNewPasswordErrorSpanVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(repeatNewPasswordErrorSpan);
            else
                WaitForElementNotVisible(repeatNewPasswordErrorSpan, 7);

            return this;
        }
        public ResetPasswordPage ValidateNewPasswordErrorSpanText(string ExpectedText)
        {
            ValidateElementText(newPasswordErrorSpan, ExpectedText);

            return this;
        }
        public ResetPasswordPage ValidateRepeatNewPasswordErrorSpanText(string ExpectedText)
        {
            ValidateElementText(repeatNewPasswordErrorSpan, ExpectedText);

            return this;
        }


        public ResetPasswordPage ValidatePasswordPromptTitleVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(PasswordPromptTitle);
            else
                WaitForElementNotVisible(PasswordPromptTitle, 7);

            return this;
        }
        public ResetPasswordPage ValidatePasswordPromptMinUpperVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(PasswordPromptMinUpper);
            else
                WaitForElementNotVisible(PasswordPromptMinUpper, 7);

            return this;
        }
        public ResetPasswordPage ValidatePasswordPromptMinLowerVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(PasswordPromptMinLower);
            else
                WaitForElementNotVisible(PasswordPromptMinLower, 7);

            return this;
        }
        public ResetPasswordPage ValidatePasswordPromptMinNumVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(PasswordPromptMinNum);
            else
                WaitForElementNotVisible(PasswordPromptMinNum, 7);

            return this;
        }
        public ResetPasswordPage ValidatePasswordPromptMinSpecialCharVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(PasswordPromptMinSpecialChar);
            else
                WaitForElementNotVisible(PasswordPromptMinSpecialChar, 7);

            return this;
        }
        public ResetPasswordPage ValidatePasswordPromptMinLengthVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(PasswordPromptMinLength);
            else
                WaitForElementNotVisible(PasswordPromptMinLength, 7);

            return this;
        }



        public ResetPasswordPage ValidatePasswordPromptTitleText(string ExpectedText)
        {
            ValidateElementText(PasswordPromptTitle, ExpectedText);

            return this;
        }
        public ResetPasswordPage ValidatePasswordPromptMinUpperText(string ExpectedText)
        {
            ValidateElementText(PasswordPromptMinUpper, ExpectedText);

            return this;
        }
        public ResetPasswordPage ValidatePasswordPromptMinLowerText(string ExpectedText)
        {
            ValidateElementText(PasswordPromptMinLower, ExpectedText);

            return this;
        }
        public ResetPasswordPage ValidatePasswordPromptMinNumText(string ExpectedText)
        {
            ValidateElementText(PasswordPromptMinNum, ExpectedText);

            return this;
        }
        public ResetPasswordPage ValidatePasswordPromptMinSpecialCharText(string ExpectedText)
        {
            ValidateElementText(PasswordPromptMinSpecialChar, ExpectedText);

            return this;
        }
        public ResetPasswordPage ValidatePasswordPromptMinLengthText(string ExpectedText)
        {
            ValidateElementText(PasswordPromptMinLength, ExpectedText);

            return this;
        }




        public ResetPasswordPage ValidatePasswordPromptMinUpperIconValid(bool ExpectValid)
        {
            if (ExpectValid)
            {
                WaitForElementVisible(PasswordPromptMinUpperOkIcon);
                WaitForElementNotVisible(PasswordPromptMinUpperErrorIcon, 3);
            }
            else
            {
                WaitForElementNotVisible(PasswordPromptMinUpperOkIcon, 3);
                WaitForElementVisible(PasswordPromptMinUpperErrorIcon);
            }

            return this;
        }
        public ResetPasswordPage ValidatePasswordPromptMinLowerIconValid(bool ExpectValid)
        {
            if (ExpectValid)
            {
                WaitForElementVisible(PasswordPromptMinLowerOkIcon);
                WaitForElementNotVisible(PasswordPromptMinLowerErrorIcon, 3);
            }
            else
            {
                WaitForElementNotVisible(PasswordPromptMinLowerOkIcon, 3);
                WaitForElementVisible(PasswordPromptMinLowerErrorIcon);
            }

            return this;
        }
        public ResetPasswordPage ValidatePasswordPromptMinNumIconValid(bool ExpectValid)
        {
            if (ExpectValid)
            {
                WaitForElementVisible(PasswordPromptMinNumOkIcon);
                WaitForElementNotVisible(PasswordPromptMinNumErrorIcon, 3);
            }
            else
            {
                WaitForElementNotVisible(PasswordPromptMinNumOkIcon, 3);
                WaitForElementVisible(PasswordPromptMinNumErrorIcon);
            }

            return this;
        }
        public ResetPasswordPage ValidatePasswordPromptMinSpecialCharIconValid(bool ExpectValid)
        {
            if (ExpectValid)
            {
                WaitForElementVisible(PasswordPromptMinSpecialCharOkIcon);
                WaitForElementNotVisible(PasswordPromptMinSpecialCharErrorIcon, 3);
            }
            else
            {
                WaitForElementNotVisible(PasswordPromptMinSpecialCharOkIcon, 3);
                WaitForElementVisible(PasswordPromptMinSpecialCharErrorIcon);
            }

            return this;
        }
        public ResetPasswordPage ValidatePasswordPromptMinLengthIconValid(bool ExpectValid)
        {
            if (ExpectValid)
            {
                WaitForElementVisible(PasswordPromptMinLengthOkIcon);
                WaitForElementNotVisible(PasswordPromptMinLengthErrorIcon, 3);
            }
            else
            {
                WaitForElementNotVisible(PasswordPromptMinLengthOkIcon, 3);
                WaitForElementVisible(PasswordPromptMinLengthErrorIcon);
            }

            return this;
        }



        public ResetPasswordPage ValidateGoToHomePageLinkVisible()
        {
            WaitForElementVisible(GoToHomePageLink);

            return this;
        }
        public ResetPasswordPage ClickGoToHomePageLink()
        {
            Click(GoToHomePageLink);

            return this;
        }
        public ResetPasswordPage ValidateGeneralErrorMessageVisible()
        {
            WaitForElementVisible(generalErrorMessage);

            return this;
        }
        public ResetPasswordPage ValidateGeneralErrorMessageText(string ExpectedText)
        {
            ValidateElementText(generalErrorMessage, ExpectedText);

            return this;
        }
    }

}

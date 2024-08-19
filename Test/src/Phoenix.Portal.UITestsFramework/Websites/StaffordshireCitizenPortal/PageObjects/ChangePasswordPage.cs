using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;



namespace Phoenix.Portal.UITestsFramework.Websites.StaffordshireCitizenPortal.PageObjects
{
    public class ChangePasswordPage : CommonMethods
    {
        public ChangePasswordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By pageTitle = By.XPath("//*[@id='CWPageHeader']/div/div/div/div/h1[text()='Change password']");

        
        readonly By CurrentPasswordLabel = By.XPath("//*[@id='CWPassword']/label");
        readonly By newPasswordLabel = By.XPath("//*[@id='CWNewPassword1']/label");
        readonly By repeatNewPasswordLabel = By.XPath("//*[@id='CWNewPassword2']/label");

        readonly By CurrentPasswordField = By.Id("CWPassword-input");
        readonly By newPasswordField = By.Id("CWNewPassword1-input");
        readonly By repeatNewPasswordField = By.Id("CWNewPassword2-input");

        readonly By newPasswordErrorSpan = By.XPath("//*[@id='CWNewPassword1']/div[3]/span");
        readonly By repeatNewPasswordErrorSpan = By.XPath("//*[@id='CWNewPassword2']/div[3]/span");

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

        readonly By changePasswordButton = By.XPath("//*[@id='CWBtnReset']");

        readonly By errorMessage = By.XPath("//*[@id='CWErrorMessage']");
        
        readonly By successMessage = By.XPath("//*[@id='CWSuccessMessageText']");
        
        readonly By GoToHomePageLink = By.XPath("//*[@id='CWSuccessMessage']/div/mosaic-link/a");

        readonly By warningMessage = By.XPath("//*[@id='CWWarningMessage']/div[2]");




        public ChangePasswordPage GoToChangePasswordPage(string ChangePasswordURL)
        {
            driver.Navigate().GoToUrl(ChangePasswordURL);

            return this;
        }

        public ChangePasswordPage WaitForChangePasswordPageToLoad()
        {
            this.WaitForBrowserWindowTitle("Change password");

            WaitForElement(pageTitle);
            
            //WaitForElement(CurrentPasswordLabel);
            WaitForElement(newPasswordLabel);
            WaitForElement(repeatNewPasswordLabel);

            WaitForElement(newPasswordField);
            WaitForElement(repeatNewPasswordField);
            
            WaitForElement(PasswordPromptTitle);
            WaitForElement(PasswordPromptMinUpper);
            WaitForElement(PasswordPromptMinNum);
            WaitForElement(PasswordPromptMinSpecialChar);
            WaitForElement(PasswordPromptMinLength);

            WaitForElement(changePasswordButton);
            

            return this;
        }
        public ChangePasswordPage WaitForChangePasswordPageToLoadEmpty()
        {
            this.WaitForBrowserWindowTitle("Change password");

            WaitForElement(pageTitle);

            WaitForElementNotVisible(newPasswordLabel, 5);
            WaitForElementNotVisible(repeatNewPasswordLabel, 5);

            WaitForElementNotVisible(newPasswordField, 5);
            WaitForElementNotVisible(repeatNewPasswordField, 5);

            WaitForElementNotVisible(PasswordPromptTitle, 5);
            WaitForElementNotVisible(PasswordPromptMinUpper, 5);
            WaitForElementNotVisible(PasswordPromptMinNum, 5);
            WaitForElementNotVisible(PasswordPromptMinSpecialChar, 5);
            WaitForElementNotVisible(PasswordPromptMinLength, 5);

            WaitForElementNotVisible(changePasswordButton, 5);


            return this;
        }


        public ChangePasswordPage InsertCurrentPassword(string TextToInsert)
        {
            SendKeys(CurrentPasswordField, TextToInsert);

            return this;
        }
        public ChangePasswordPage InsertNewPassword(string TextToInsert)
        {
            SendKeys(newPasswordField, TextToInsert);

            return this;
        }
        public ChangePasswordPage InsertRepeatNewPassword(string TextToInsert)
        {
            SendKeys(repeatNewPasswordField, TextToInsert);
            SendKeysWithoutClearing(repeatNewPasswordField, Keys.Tab);

            return this;
        }


        public ChangePasswordPage ValidateNewPasswordErrorSpanText(string ExpectedText)
        {
            ValidateElementText(newPasswordErrorSpan, ExpectedText);

            return this;
        }
        public ChangePasswordPage ValidateRepeatNewPasswordErrorSpanText(string ExpectedText)
        {
            ValidateElementText(repeatNewPasswordErrorSpan, ExpectedText);

            return this;
        }
        public ChangePasswordPage ValidateErrorMessageText(string ExpectedText)
        {
            ValidateElementText(errorMessage, ExpectedText);

            return this;
        }
        public ChangePasswordPage ValidateSuccessMessageText(string ExpectedText)
        {
            ValidateElementText(successMessage, ExpectedText);

            return this;
        }
        public ChangePasswordPage ValidateWarningMessageText(string ExpectedText)
        {
            ValidateElementText(warningMessage, ExpectedText);

            return this;
        }


        public ChangePasswordPage ValidatePasswordPromptTitleText(string ExpectedText)
        {
            ValidateElementText(PasswordPromptTitle, ExpectedText);

            return this;
        }
        public ChangePasswordPage ValidatePasswordPromptMinUpperText(string ExpectedText)
        {
            ValidateElementText(PasswordPromptMinUpper, ExpectedText);

            return this;
        }
        public ChangePasswordPage ValidatePasswordPromptMinLowerText(string ExpectedText)
        {
            ValidateElementText(PasswordPromptMinLower, ExpectedText);

            return this;
        }
        public ChangePasswordPage ValidatePasswordPromptMinLowerVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(PasswordPromptMinLower);
            else
                WaitForElementNotVisible(PasswordPromptMinLower, 7);

            return this;
        }
        public ChangePasswordPage ValidatePasswordPromptMinNumText(string ExpectedText)
        {
            ValidateElementText(PasswordPromptMinNum, ExpectedText);

            return this;
        }
        public ChangePasswordPage ValidatePasswordPromptMinSpecialCharText(string ExpectedText)
        {
            ValidateElementText(PasswordPromptMinSpecialChar, ExpectedText);

            return this;
        }
        public ChangePasswordPage ValidatePasswordPromptMinLengthText(string ExpectedText)
        {
            ValidateElementText(PasswordPromptMinLength, ExpectedText);

            return this;
        }



        public ChangePasswordPage ValidateNewPasswordErrorSpanVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(newPasswordErrorSpan);
            else
                WaitForElementNotVisible(newPasswordErrorSpan, 7);

            return this;
        }
        public ChangePasswordPage ValidateRepeatNewPasswordErrorSpanVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(repeatNewPasswordErrorSpan);
            else
                WaitForElementNotVisible(repeatNewPasswordErrorSpan, 7);

            return this;
        }
        public ChangePasswordPage ValidatePasswordPromptTitleVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(PasswordPromptTitle);
            else
                WaitForElementNotVisible(PasswordPromptTitle, 7);

            return this;
        }
        public ChangePasswordPage ValidatePasswordPromptMinUpperVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(PasswordPromptMinUpper);
            else
                WaitForElementNotVisible(PasswordPromptMinUpper, 7);

            return this;
        }
        public ChangePasswordPage ValidatePasswordPromptMinNumVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(PasswordPromptMinNum);
            else
                WaitForElementNotVisible(PasswordPromptMinNum, 7);

            return this;
        }
        public ChangePasswordPage ValidatePasswordPromptMinSpecialCharVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(PasswordPromptMinSpecialChar);
            else
                WaitForElementNotVisible(PasswordPromptMinSpecialChar, 7);

            return this;
        }
        public ChangePasswordPage ValidatePasswordPromptMinLengthVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(PasswordPromptMinLength);
            else
                WaitForElementNotVisible(PasswordPromptMinLength, 7);

            return this;
        }
        public ChangePasswordPage ValidateWarningMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(warningMessage);
            else
                WaitForElementNotVisible(warningMessage, 7);

            return this;
        }


        public ChangePasswordPage ValidatePasswordPromptMinUpperIconValid(bool ExpectValid)
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
        public ChangePasswordPage ValidatePasswordPromptMinLowerIconValid(bool ExpectValid)
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
        public ChangePasswordPage ValidatePasswordPromptMinNumIconValid(bool ExpectValid)
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
        public ChangePasswordPage ValidatePasswordPromptMinSpecialCharIconValid(bool ExpectValid)
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
        public ChangePasswordPage ValidatePasswordPromptMinLengthIconValid(bool ExpectValid)
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




        public ChangePasswordPage ValidateSuccessMessageVisible()
        {
            WaitForElementVisible(successMessage);

            return this;
        }
        public ChangePasswordPage ValidateGoToHomePageLinkVisible()
        {
            WaitForElementVisible(GoToHomePageLink);

            return this;
        }
        public ChangePasswordPage ValidateErrorMessageVisible()
        {
            WaitForElementVisible(errorMessage);

            return this;
        }



        public ChangePasswordPage ClickChangePasswordButton()
        {
            WaitForElementToBeClickable(changePasswordButton);
            Click(changePasswordButton);

            return this;
        }
        public ChangePasswordPage ClickGoToHomePageLink()
        {
            Click(GoToHomePageLink);

            return this;
        }


    }
    
}

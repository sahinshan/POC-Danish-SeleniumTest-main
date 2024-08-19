using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;



namespace Phoenix.Portal.UITestsFramework.Websites.StaffordshireCitizenPortal.PageObjects
{
    public class ChangeTwoFactorAuthenticationTypePage : CommonMethods
    {
        public ChangeTwoFactorAuthenticationTypePage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        #region Field Titles

        readonly By ChangeTwoFactorAuthenticationTypePageTopMessage = By.XPath("//*[@id='CWChange2FA']/form/mosaic-card/mosaic-card-header/div/div[text()='Change Two Factor Authentication Type']");

        readonly By SelectTwoFactorAuthenticationTypeFieldTitle = By.XPath("//*[@id='CWChange2FASelect']/label[text()='Select Two Factor Authentication Type']");

        #endregion

        
        readonly By SelectTwoFactorAuthenticationTypeSelectedValue = By.XPath("//*[@id='CWChange2FASelect']/div/div[@class='ss-single-selected']/span[@class='placeholder']");
        readonly By SelectTwoFactorAuthenticationTypePicklist = By.XPath("//*[@id='CWChange2FASelect-field']");
        readonly By SelectTwoFactorAuthenticationTypeTopField = By.XPath("//*[@id='CWChange2FASelect']/div/div/span[1]");
        readonly By SelectTwoFactorAuthenticationTypeSearchField = By.XPath("//*[@id='CWChange2FASelect']/div/div/div/input[@type='search']");
        By SelectTwoFactorAuthenticationTypeFieldResult(string SelectTwoFactorAuthenticationTypeField) => By.XPath("//*[@id='CWChange2FASelect']/div/div/div/div[text()='" + SelectTwoFactorAuthenticationTypeField + "']");
        readonly By SelectTwoFactorAuthenticationTypeFieldError = By.XPath("//*[@id='CWChange2FASelect']/div[3]/span");

        
        readonly By SubmitButton = By.XPath("//*[@id='CWChange2FA']/form/mosaic-card/mosaic-card-body/div/mosaic-form-actions/mosaic-button");

        readonly By ToastMessage = By.XPath("//div[@class='toastify__message']");
        readonly By ToastMessageCloseButton = By.XPath("//span[@class='toast-close']");



        public ChangeTwoFactorAuthenticationTypePage WaitForChangeTwoFactorAuthenticationTypePageToLoad()
        {
            this.driver.Navigate().Refresh();

            WaitForElement(ChangeTwoFactorAuthenticationTypePageTopMessage);

            WaitForElement(SelectTwoFactorAuthenticationTypeFieldTitle);
            WaitForElement(SelectTwoFactorAuthenticationTypeTopField);
            WaitForElement(SelectTwoFactorAuthenticationTypeSelectedValue);

            WaitForElement(SubmitButton);

            return this;
        }


        public ChangeTwoFactorAuthenticationTypePage ValidateToastMessageVisible()
        {
            WaitForElementVisible(ToastMessage);
            WaitForElementVisible(ToastMessageCloseButton);

            return this;
        }
        public ChangeTwoFactorAuthenticationTypePage ValidateToastMessageText(string ExpectedText)
        {
            ValidateElementText(ToastMessage, ExpectedText);

            return this;
        }


        public ChangeTwoFactorAuthenticationTypePage ClickSubmiitButton()
        {
            Click(SubmitButton);

            return this;
        }


        public ChangeTwoFactorAuthenticationTypePage ClickOnTwoFactorAuthenticationTypeTopField()
        {
            Click(SelectTwoFactorAuthenticationTypeTopField);

            return this;
        }
        public ChangeTwoFactorAuthenticationTypePage ClickOnTwoFactorAuthenticationTypeOption(string TwoFactorAuthenticationType)
        {
            WaitForElementVisible(SelectTwoFactorAuthenticationTypeFieldResult(TwoFactorAuthenticationType));
            Click(SelectTwoFactorAuthenticationTypeFieldResult(TwoFactorAuthenticationType));

            return this;
        }
        public ChangeTwoFactorAuthenticationTypePage InsertTwoFactorAuthenticationTypeSearchText(string TextToInsert)
        {
            WaitForElement(SelectTwoFactorAuthenticationTypeSearchField);

            SendKeys(SelectTwoFactorAuthenticationTypeSearchField, TextToInsert);

            return this;
        }
        public ChangeTwoFactorAuthenticationTypePage ValidateTwoFactorAuthenticationTypeSelectedText(string ExpectedText)
        {
            WaitForElementVisible(SelectTwoFactorAuthenticationTypeSelectedValue);
            ValidateElementText(SelectTwoFactorAuthenticationTypeSelectedValue, ExpectedText);

            return this;
        }
        public ChangeTwoFactorAuthenticationTypePage ValidateTwoFactorAuthenticationTypeFieldErrorVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(SelectTwoFactorAuthenticationTypeFieldError);
            else
                WaitForElementNotVisible(SelectTwoFactorAuthenticationTypeFieldError, 7);

            return this;
        }
        public ChangeTwoFactorAuthenticationTypePage ValidateTwoFactorAuthenticationTypeFieldErrorText(string ExpectedText)
        {
            ValidateElementText(SelectTwoFactorAuthenticationTypeFieldError, ExpectedText);

            return this;
        }
        
    }
    
}

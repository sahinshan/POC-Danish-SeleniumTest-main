using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;



namespace Phoenix.Portal.UITestsFramework.Websites.StaffordshireCitizenPortal.PageObjects
{
    public class HomePage : CommonMethods
    {
        public HomePage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        By styleSheetFile(string StileSheetFileName) => By.XPath("//link[@href='/resources/" + StileSheetFileName + "']");

     
        readonly By RegisterButton = By.XPath("//mosaic-button/a[@href='/registration']");

        readonly By loginMessage = By.XPath("//*[@id='CWLoginContainer']/mosaic-card-header/div[1]/div[text()='Already registered? Log in now']");
        readonly By emailAddressLabel = By.XPath("//*[@id='CWUsername']/label");
        readonly By passwordAddressLabel = By.XPath("//*[@id='CWPassword']/label");

        readonly By emailAdress_Field = By.XPath("//*[@id='CWUsername-input']");
        readonly By emailAdress_Errorlabel = By.XPath("//*[@id='CWUsername']/div[3]/span");
        readonly By password_Field = By.XPath("//*[@id='CWPassword-input']");
        readonly By password_ErrorLabel = By.XPath("//*[@id='CWPassword']/div[3]/span");

        readonly By loginButton = By.XPath("//*[@id='CWLoginButton']/button");
        readonly By errorLabel = By.XPath("//*[@id='CWErrorMessage']");
        readonly By warningLabel = By.XPath("//*[@id='CWWarningMessage']");
        readonly By forgotPasswordLink = By.XPath("//*[@id='ForgotPasswordLink']/a");
        readonly By pin_Field = By.XPath("//*[@id='CWPin-input']");
        readonly By validatePinButton = By.XPath("//*[@id='CWPinButton']/button");
        readonly By reIssuePinLink = By.XPath("//*[@id='CWReIssuePin']");

        readonly By SendVerificationEmailLink = By.XPath("//*[@id='CWReSendVerificationEmail']");

        
        readonly By footer_ContactUsHeader = By.XPath("//*[@id='CWFooter']/mosaic-row/mosaic-col[1]/h4");
        readonly By footer_ContactUsInformation = By.XPath("//*[@id='CWFooter']/mosaic-row/mosaic-col[1]/div");

        readonly By footer_PortalLinksHeaders = By.XPath("//*[@id='CWFooter']/mosaic-row/mosaic-col[2]/h4");
        readonly By footer_PortalLogo = By.XPath("//*[@id='CWFooter']/mosaic-row/mosaic-col[2]/a/mosaic-img/img[@src='/resources/websitelogo.png']");
        readonly By footer_PoweredByCareDirector = By.XPath("//*[@id='CWFooter']/mosaic-row/mosaic-col[2]/div");

        readonly By footer_ConnectWithUsHeader = By.XPath("//*[@id='CWFooter']/mosaic-row/mosaic-col[3]/h4");
        readonly By footer_TwitterIcon = By.XPath("//*[@id='CWTwitterImage']/img[@src='/imgs/social/logo-twitter.png']");
        readonly By footer_FacebookIcon = By.XPath("//*[@id='CWFacebookImage']/img[@src='/imgs/social/logo-facebook.png']");
        readonly By footer_InstagramIcon = By.XPath("//*[@id='CWInstagramImage']/img[@src='/imgs/social/logo-instagram.png']");
        readonly By footer_YoutubeIcon = By.XPath("//*[@id='CWYoutubeImage']/img[@src='/imgs/social/logo-youtube.png']");
        
        readonly By footer_FeedbackButton = By.XPath("//*[@id='CWFeedback']/button");
        


        public HomePage GoToHomePage()
        {
            driver.Navigate().GoToUrl(appURL);

            return this;
        }
        public HomePage WaitForHomePageToLoad()
        {
            WaitForElement(RegisterButton);
            
            WaitForElement(loginMessage);
            WaitForElement(emailAddressLabel);
            WaitForElement(passwordAddressLabel);
            WaitForElement(emailAdress_Field);
            WaitForElement(password_Field);
            WaitForElement(loginButton);
            WaitForElement(forgotPasswordLink);

            

            return this;
        }



        public HomePage ClickRegisterButton()
        {
            Click(RegisterButton);

            return this;
        }
        public HomePage ClickLoginButton()
        {
            WaitForElementToBeClickable(loginButton);
            Click(loginButton);

            return this;
        }
        public HomePage ClickForgotPasswordLink()
        {
            WaitForElementToBeClickable(forgotPasswordLink);
            Click(forgotPasswordLink);

            return this;
        }
        public HomePage ClickFeedbackIcon()
        {
            Click(footer_FeedbackButton);

            return this;
        }


        public HomePage InsertUserName(string UserName)
        {
            SendKeys(emailAdress_Field, UserName);
            SendKeysWithoutClearing(emailAdress_Field, Keys.Tab);

            return this;
        }
        public HomePage InsertPassword(string Password)
        {
            SendKeys(password_Field, Password);
            SendKeysWithoutClearing(password_Field, Keys.Tab);

            return this;
        }



        public HomePage ValidateEmailAddressErrorMessageVisible(bool ExpectVisible)
        {
            if(ExpectVisible)
                WaitForElementVisible(emailAdress_Errorlabel);
            else
                WaitForElementNotVisible(emailAdress_Errorlabel, 5);


            return this;
        }
        public HomePage ValidateEmailAddressErrorMessage(string ExpectedText)
        {
            ValidateElementText(emailAdress_Errorlabel, ExpectedText);

            return this;
        }
        public HomePage ValidatePasswordErrorMessageVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(password_ErrorLabel);
            else
                WaitForElementNotVisible(password_ErrorLabel, 5);

            return this;
        }
        public HomePage ValidatePasswordErrorMessage(string ExpectedText)
        {
            ValidateElementText(password_ErrorLabel, ExpectedText);

            return this;
        }
        public HomePage ValidateErrorMessageVisible()
        {
            WaitForElementVisible(errorLabel);

            return this;
        }
        public HomePage ValidateErrorMessage(string ExpectedText)
        {
            WaitForElementVisible(errorLabel);
            ValidateElementText(errorLabel, ExpectedText);

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


        public HomePage ValidateWarningMessage(string ExpectedText)
        {
            System.Threading.Thread.Sleep(1000);
            WaitForElementVisible(warningLabel);
            ValidateElementText(warningLabel, ExpectedText);

            return this;
        }

        public HomePage ValidateStyleSheetFile(string FileName)
        {
            WaitForElement(styleSheetFile(FileName));
            return this;
        }



        public HomePage ValidatePortalLogoVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(footer_PortalLogo);
            else
                WaitForElementNotVisible(footer_PortalLogo, 5);

            return this;
        }
        public HomePage ValidateTwitterIconVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(footer_TwitterIcon);
            else
                WaitForElementNotVisible(footer_TwitterIcon, 5);

            return this;
        }
        public HomePage ValidateFacebookIconVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(footer_FacebookIcon);
            else
                WaitForElementNotVisible(footer_FacebookIcon, 5);

            return this;
        }
        public HomePage ValidateInstagramIconVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(footer_InstagramIcon);
            else
                WaitForElementNotVisible(footer_InstagramIcon, 5);

            return this;
        }
        public HomePage ValidateYoutubeIconVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(footer_YoutubeIcon);
            else
                WaitForElementNotVisible(footer_YoutubeIcon, 5);

            return this;
        }
        public HomePage ValidateFeedbackButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(footer_FeedbackButton);
            else
                WaitForElementNotVisible(footer_FeedbackButton, 5);

            return this;
        }



        public HomePage ValidateCssPropertyFortwitterImage(string CSSPropertyName, string ExpectedValue)
        {
            ValidateElementCSSPropertyValue(footer_TwitterIcon, CSSPropertyName, ExpectedValue);

            return this;
        }
        public HomePage ValidateCssPropertyForFeedbackImage(string CSSPropertyName, string ExpectedValue)
        {
            ValidateElementCSSPropertyValue(footer_FeedbackButton, CSSPropertyName, ExpectedValue);

            return this;
        }


        public HomePage ValidateFooterContactUsHeaderText(string ExpectedText)
        {
            ValidateElementText(footer_ContactUsHeader, ExpectedText);
            return this;
        }
        public HomePage ValidateFooterContactUsInformationText(string ExpectedText)
        {
            ValidateElementText(footer_ContactUsInformation, ExpectedText);
            return this;
        }


        public HomePage ValidatPortalLinksHeadersText(string ExpectedText)
        {
            ValidateElementText(footer_PortalLinksHeaders, ExpectedText);
            return this;
        }
        public HomePage ValidatePoweredByCareDirectorText(string ExpectedText)
        {
            ValidateElementText(footer_PoweredByCareDirector, ExpectedText);
            return this;
        }


        public HomePage ValidatConnectWithUsHeaderText(string ExpectedText)
        {
            ValidateElementText(footer_ConnectWithUsHeader, ExpectedText);
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

        public HomePage InsertPIN(string Password)
        {
            SendKeys(pin_Field, Password);

            SendKeysWithoutClearing(pin_Field, Keys.Tab);

            return this;
        }

        public HomePage ClickValidatePinButton()
        {
            WaitForElementToBeClickable(validatePinButton);

            Click(validatePinButton);

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
    }
    
}

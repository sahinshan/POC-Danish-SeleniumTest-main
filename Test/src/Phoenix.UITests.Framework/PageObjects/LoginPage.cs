using OpenQA.Selenium;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class LoginPage : CommonMethods
    {
        public LoginPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By usernameTextbox = By.Id("UserNameTextBox");
        readonly By passwordTextbox = By.Id("LoginPasswordTextBox");
        readonly By loginButton = By.Id("CWLoginButton");
        readonly By environmentPicklist = By.XPath("//select[@id='CWEnvironmentList']");


        readonly string defaultEnvironmentName = "Health and Local Authority";
        readonly By advanceSectionLink = By.Id("CWAdvanceSectionLink");

        readonly By versionLabel = By.XPath("//*[@id='VersionLabel']");

        internal bool _ignoreEnvironmentInLoginPage = false;

        public LoginPage GoToLoginPage()
        {
            driver.Navigate().GoToUrl(appURL + "/pages/Login.aspx");

            WaitForElement(usernameTextbox);
            WaitForElement(loginButton);
            //            WaitForElement(versionLabel);

            var ignoreEnvironment = System.Configuration.ConfigurationManager.AppSettings["IgnoreEnvironmentInLoginPage"];
            _ignoreEnvironmentInLoginPage = ignoreEnvironment.Equals("true");

            return this;
        }

        public HomePage Login(string UserName, string Password)
        {
            WaitForElement(usernameTextbox);
            SendKeys(usernameTextbox, UserName);

            WaitForElement(passwordTextbox);
            SendKeys(passwordTextbox, Password);

            var advancedSectionLinkExists = CheckIfElementExists(advanceSectionLink);
            if (advancedSectionLinkExists)
            {
                WaitForElementToBeClickable(advanceSectionLink);
                Click(advanceSectionLink);
            }

            if (!_ignoreEnvironmentInLoginPage) 
            {
                WaitForElementToBeClickable(environmentPicklist);
                SelectPicklistElementByText(environmentPicklist, defaultEnvironmentName);
            }
            

            WaitForElementNotVisible("CWRefreshPanel", 7);

            Click(loginButton);


            return new HomePage(this.driver, this.Wait, this.appURL);
        }

       public HomePage Login(string UserName, string Password, string EnvironmentName)
        {
            SendKeys(usernameTextbox, UserName);
            SendKeys(passwordTextbox, Password);

            var advancedSectionLinkExists = CheckIfElementExists(advanceSectionLink);
            if (advancedSectionLinkExists)
            {
                WaitForElementToBeClickable(advanceSectionLink);
                Click(advanceSectionLink);
            }

            if (!_ignoreEnvironmentInLoginPage)
            {
                WaitForElementToBeClickable(environmentPicklist);
                SelectPicklistElementByText(environmentPicklist, EnvironmentName);
            }

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElementToBeClickable(loginButton);
            Click(loginButton);

            return new HomePage(this.driver, this.Wait, this.appURL);
        }


        public LoginPage ValidateVersionLabelText(string ExpectedText)
        {
            WaitForElement(versionLabel);
            ValidateElementTextContainsText(versionLabel, ExpectedText);
            

            return this;
        }
    }
}

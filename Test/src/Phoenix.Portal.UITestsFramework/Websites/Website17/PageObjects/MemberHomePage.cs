using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;



namespace Phoenix.Portal.UITestsFramework.Websites.Website17.PageObjects
{
    public class MemberHomePage : CommonMethods
    {
        public MemberHomePage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

     

        readonly By footerContainer = By.Id("footer-container");


        public MemberHomePage WaitForMemberHomePageToLoad()
        {
            this.WaitForBrowserWindowTitle("Member Home Page - Consumer Portal");

            return this;
        }


        public MemberHomePage ValidateCssPropertyForFooterContainer(string CSSPropertyName, string ExpectedValue)
        {
            ValidateElementCSSPropertyValue(footerContainer, CSSPropertyName, ExpectedValue);

            return this;
        }



    }
    
}

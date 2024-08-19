using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;



namespace Phoenix.Portal.UITestsFramework.Websites.Website17.PageObjects
{
    public class RegistrationSuccessPage : CommonMethods
    {
        public RegistrationSuccessPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

       
        readonly By pageHeader = By.XPath("//mosaic-page-header/div/div/div/h1[text()='Registration Success']");


        public RegistrationSuccessPage WaitForRegistrationSuccessPageToLoad()
        {
            WaitForElement(pageHeader);

            WaitForBrowserWindowTitle("Registration Success - Consumer Portal");

            return this;
        }



    }
    
}

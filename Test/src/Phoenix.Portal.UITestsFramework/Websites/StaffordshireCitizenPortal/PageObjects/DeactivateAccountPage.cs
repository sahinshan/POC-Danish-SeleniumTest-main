using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;



namespace Phoenix.Portal.UITestsFramework.Websites.StaffordshireCitizenPortal.PageObjects
{
    public class DeactivateAccountPage : CommonMethods
    {
        public DeactivateAccountPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

       
        readonly By pageHeader = By.XPath("//*[@id='CWPageHeader']/div/div/div/div/h1[text()='Deactivate account']");
        
        readonly By warningMessage = By.XPath("//*[@id='CWDeactivateAccountControls']/mosaic-alert/div/p");
        readonly By confirmButton = By.XPath("//*[@id='CWBtnConfirm']");


        public DeactivateAccountPage WaitForRegistrationSuccessPageToLoad()
        {
            WaitForBrowserWindowTitle("Deactivate account");

            WaitForElement(pageHeader);
            WaitForElement(warningMessage);
            WaitForElement(confirmButton);
            
            return this;
        }

        public DeactivateAccountPage VaidateWarningMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(warningMessage);
            else
                WaitForElementNotVisible(warningMessage, 5);

            return this;
        }

        public DeactivateAccountPage VaidateWarningMessageText(string ExpectText)
        {
            ValidateElementText(warningMessage, ExpectText);
         
            return this;
        }

        public DeactivateAccountPage ClickConfirmButton()
        {
            WaitForElementToBeClickable(confirmButton);
            Click(confirmButton);

            return this;
        }

    }
    
}

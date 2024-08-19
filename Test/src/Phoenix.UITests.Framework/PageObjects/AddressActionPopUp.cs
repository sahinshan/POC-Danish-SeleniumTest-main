using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class AddressActionPopUp : CommonMethods
    {


        public AddressActionPopUp(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By addressActionDialog = By.Id("CWAddressActionDialog");
        readonly By addressAction = By.Id("CWAddressUpdateOption");
        readonly By popUpHeader = By.Id("CWHeaderTitle");
        readonly By okButton = By.Id("CWOK");
        readonly By cancelButton = By.Id("CWCancel");
        
        public AddressActionPopUp WaitForAddressActionPopUpToLoad()
        {

                    

            WaitForElement(popUpHeader);

         
            WaitForElement(okButton);
            WaitForElement(cancelButton);


                   return this;
        }

       

        public AddressActionPopUp TapOkButton()
        {
            WaitForElementVisible(okButton);
            MoveToElementInPage(okButton);
            Click(okButton);
           
            return this;
        }

        private IAlert WaitForAlert()
        {
            int i = 0;
            while (i++ < 15)
            {
                try
                {
                    return driver.SwitchTo().Alert();
                }
                catch (NoAlertPresentException e)
                {
                    Thread.Sleep(1000);
                    continue;
                }
            }

            throw new Exception("No Alert was displayed after 5 seconds");
        }

        public AddressActionPopUp SelectViewByText(string TextToSelect)
        {

            SelectPicklistElementByText(addressAction, TextToSelect);
            WaitForElementNotVisible("CWRefreshPanel", 7);


            return this;
        }


    }
}

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
   public  class RestrictPersonAlertAndHazardPopup : CommonMethods
    {

        public RestrictPersonAlertAndHazardPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By restrictPersonAlertAndHazardPopup = By.Id("iframe_CWAssignDataRestrictionDialog");
        readonly By popupHeader = By.Id("CWHeaderText");
        readonly By restriction_HeaderLabel = By.Id("RestrictionTypeLabel");
        readonly By restriction_LookUpButton = By.Id("DataRestrictionButton");
        readonly By okButton = By.Id("AssignButton");
        readonly By cancelButton = By.Id("CancelButton");
        readonly By successfullMessage = By.Id("CWNotificationMessage_DataRestriction");

        public RestrictPersonAlertAndHazardPopup WaitForRestrictPersonAlertAndHazardPopupToLoad()
        {

            Wait.Until(c => c.FindElement(restrictPersonAlertAndHazardPopup));
            driver.SwitchTo().Frame(driver.FindElement(restrictPersonAlertAndHazardPopup));

           

            WaitForElement(popupHeader);

            WaitForElement(restriction_HeaderLabel);
            WaitForElement(restriction_LookUpButton);
            WaitForElement(okButton);
            WaitForElement(cancelButton);
           


            return this;
        }

        public RestrictPersonAlertAndHazardPopup RestrictionTypeTapSearchButton()


        {

            WaitForElementToBeClickable(restriction_LookUpButton);
            Click(restriction_LookUpButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public RestrictPersonAlertAndHazardPopup TapOkButton()
        {
            Click(okButton);
            //driver.SwitchTo().ParentFrame();
            return this;
        }

        public RestrictPersonAlertAndHazardPopup validateSuccessfullMessage(String ExpectedText)
        {
            WaitForElementVisible(successfullMessage);
            ValidateElementText(successfullMessage, ExpectedText);
            return this;
        }

    }
}

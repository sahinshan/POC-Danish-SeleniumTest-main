using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class NoBookingOpenEndedAbsencesPopUp : CommonMethods
    {
        public NoBookingOpenEndedAbsencesPopUp(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By popupHeader = By.XPath("//*[@id='CWConfirmDynamicDialog']//h2");

        readonly By refreshButton = By.Id("CWRefreshButton");
        readonly By okButton = By.Id("CWOKButton");
        readonly By cancelButton = By.Id("CWCancelButton");
        readonly By alertText = By.XPath("//*[@id='CWConfirmDynamicDialog']//div[@class='mcc-dialog__body']/p");

        public NoBookingOpenEndedAbsencesPopUp WaitForNoBookingOpenEndedAbsencesPopUpToLoad()
        {
            

            WaitForElement(popupHeader);
            WaitForElement(okButton);
            WaitForElement(cancelButton);

            return this;
        }

        public NoBookingOpenEndedAbsencesPopUp ValidateAlertText(string ExpectedText)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            ValidateElementText(alertText, ExpectedText);
            return this;
        }

        public NoBookingOpenEndedAbsencesPopUp ClickOkBtn()
        {
            WaitForElementToBeClickable(okButton);
            Click(okButton);
            return this;
        }

        public NoBookingOpenEndedAbsencesPopUp ClickCancelBtn()
        {
            WaitForElementToBeClickable(cancelButton);
            Click(cancelButton);
            return this;
        }


    }

}


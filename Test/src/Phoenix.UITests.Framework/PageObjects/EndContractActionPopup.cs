using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{

    /// <summary>
    /// this class represents the dynamic dialog that is displayed with messages to the user.
    /// </summary>
    public class EndContractActionPopup : CommonMethods
    {
        public EndContractActionPopup(IWebDriver driver, WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By popupHeader = By.XPath("//*[@id='CWHeaderTitle']");
        readonly By topLabel = By.XPath("//*[@id='CWEndContractOptionLabel']");
        readonly By endContractPicklist = By.XPath("//*[@id='CWEndContractOption']");
        readonly By bottomLabel = By.XPath("//*[@id='CWContractActionResult']");
        readonly By cancelButton = By.Id("CWCancel");
        readonly By okButton = By.Id("CWOK");




        public EndContractActionPopup WaitForEndContractActionPopupToLoad()
        {
            WaitForElementVisible(popupHeader);

            WaitForElementVisible(topLabel);
            WaitForElementVisible(endContractPicklist);

            WaitForElementVisible(cancelButton);
            WaitForElementVisible(okButton);

            return this;
        }

        public EndContractActionPopup SelectEndContractOption(string TextToSelect)
        {
            WaitForElementToBeClickable(endContractPicklist);
            SelectPicklistElementByText(endContractPicklist, TextToSelect);

            return this;
        }

        public EndContractActionPopup ValidateSelectEndContractOption(string ExpectedText)
        {
            ValidatePicklistSelectedText(endContractPicklist, ExpectedText);

            return this;
        }

        public EndContractActionPopup ClickCancelButton()
        {
            WaitForElementToBeClickable(cancelButton);
            Click(cancelButton);

            return this;
        }

        public EndContractActionPopup ClickOkButton()
        {
            WaitForElementToBeClickable(okButton);
            Click(okButton);

            return this;
        }

        public EndContractActionPopup ValidateTopMessage(string ExpectedMessage)
        {
            WaitForElementVisible(topLabel);
            ValidateElementText(topLabel, ExpectedMessage);

            return this;
        }

        public EndContractActionPopup ValidateBottomMessage(string ExpectedMessage)
        {
            WaitForElementVisible(bottomLabel);
            ValidateElementText(bottomLabel, ExpectedMessage);

            return this;
        }


    }
}

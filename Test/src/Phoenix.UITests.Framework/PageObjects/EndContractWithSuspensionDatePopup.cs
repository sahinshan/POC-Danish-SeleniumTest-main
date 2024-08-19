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
    public class EndContractWithSuspensionDatePopup : CommonMethods
    {
        public EndContractWithSuspensionDatePopup(IWebDriver driver, WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By popupHeader = By.XPath("//*[@id='CWHeaderTitle']");

        readonly By topLabel = By.XPath("//*[@id='CWInnerForm']/div[1]/label");
        readonly By dateField = By.XPath("//*[@id='CWUseDate']");
        readonly string dateFieldId = "CWUseDate";
        readonly By timeField = By.XPath("//*[@id='CWUseTime']");
        readonly string timeFieldId = "CWUseTime";
        readonly By bottomLabel = By.XPath("//*[@id='CWInnerForm']/div[3]/div/div/label");


        readonly By cancelButton = By.Id("CWCancel");
        readonly By okButton = By.Id("CWOK");




        public EndContractWithSuspensionDatePopup WaitForEndContractWithSuspensionDatePopupToLoad()
        {
            WaitForElementVisible(popupHeader);

            WaitForElementVisible(topLabel);
            WaitForElementVisible(dateField);
            WaitForElementVisible(timeField);
            WaitForElementVisible(bottomLabel);

            WaitForElementVisible(cancelButton);
            WaitForElementVisible(okButton);

            return this;
        }

        public EndContractWithSuspensionDatePopup InsertEndDate(string DateToInsert, string TimeToInsert)
        {
            WaitForElementToBeClickable(dateField);

            if (String.IsNullOrEmpty(DateToInsert))
            {
                SendKeys(dateField, Keys.Backspace + Keys.Backspace + Keys.Backspace + Keys.Backspace + Keys.Backspace + Keys.Backspace + Keys.Backspace + Keys.Backspace + Keys.Backspace + Keys.Backspace + Keys.Backspace);
                SendKeys(dateField, Keys.Tab);
            }
            else
            {
                ClearInputElementViaJavascript("CWUseDate");
                SendKeys(dateField, DateToInsert + Keys.Tab);
            }

            if (String.IsNullOrEmpty(TimeToInsert))
            {
                SendKeys(timeField, Keys.Backspace + Keys.Backspace + Keys.Backspace + Keys.Backspace + Keys.Backspace + Keys.Backspace + Keys.Backspace + Keys.Backspace + Keys.Backspace + Keys.Backspace + Keys.Backspace + Keys.Tab);
                SendKeys(timeField, Keys.Tab);
            }
            else
            {
                ClearInputElementViaJavascript("CWUseTime");
                SendKeys(timeField, TimeToInsert + Keys.Tab);
            }

            return this;
        }

        public EndContractWithSuspensionDatePopup ValidateEndDate(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValueByJavascript(dateFieldId, ExpectedDate);
            ValidateElementValueByJavascript(timeFieldId, ExpectedTime);

            return this;
        }

        public EndContractWithSuspensionDatePopup ClickCancelButton()
        {
            WaitForElementToBeClickable(cancelButton);
            Click(cancelButton);

            return this;
        }

        public EndContractWithSuspensionDatePopup ClickOkButton()
        {
            WaitForElementToBeClickable(okButton);
            Click(okButton);

            return this;
        }

        public EndContractWithSuspensionDatePopup ValidateTopMessage(string ExpectedMessage)
        {
            WaitForElementVisible(topLabel);
            ValidateElementText(topLabel, ExpectedMessage);

            return this;
        }

        public EndContractWithSuspensionDatePopup ValidateBottomMessage(string ExpectedMessage)
        {
            WaitForElementVisible(bottomLabel);
            ValidateElementText(bottomLabel, ExpectedMessage);

            return this;
        }

        public EndContractWithSuspensionDatePopup ValidateBottomMessageTextContains(string ExpectedMessage)
        {
            ValidateElementTextContainsText(bottomLabel, ExpectedMessage);

            return this;
        }


    }
}

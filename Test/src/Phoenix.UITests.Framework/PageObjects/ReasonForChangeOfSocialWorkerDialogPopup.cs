using Microsoft.Office.Interop.Word;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{

    /// <summary>
    /// this class represents the dynamic dialog that is displayed with messages to the user.
    /// </summary>
    public class ReasonForChangeOfSocialWorkerDialogPopup : CommonMethods
    {
        public ReasonForChangeOfSocialWorkerDialogPopup(IWebDriver driver, WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By popupHeader = By.XPath("//*[@id='CWHeaderTitle']");

        readonly By socialWorkerChangeReasonLabel = By.XPath("//*[@id='CWSocialWorkerChangeReasonIdLabel']");
        readonly By socialWorkerChangeReasonLinkField = By.XPath("//*[@id='CWSocialWorkerChangeReasonId_Link']");
        readonly By socialWorkerChangeReasonClearButton = By.XPath("//*[@id='CWClearLookup_CWSocialWorkerChangeReasonId']");
        readonly By socialWorkerChangeReasonLookupButton = By.XPath("//*[@id='CWLookupBtn_CWSocialWorkerChangeReasonId']");

        readonly By cancelButton = By.Id("CWCancel");
        readonly By saveButton = By.Id("CWSave");




        public ReasonForChangeOfSocialWorkerDialogPopup WaitForReasonForChangeOfSocialWorkerDialogPopupToLoad()
        {
            WaitForElementVisible(popupHeader);
            
            WaitForElementVisible(socialWorkerChangeReasonLabel);
            WaitForElementVisible(socialWorkerChangeReasonLookupButton);

            WaitForElementVisible(cancelButton);
            WaitForElementVisible(saveButton);

            return this;
        }


        public ReasonForChangeOfSocialWorkerDialogPopup TapCancelButton()
        {
            WaitForElementToBeClickable(cancelButton);
            Click(cancelButton);

            return this;
        }

        public ReasonForChangeOfSocialWorkerDialogPopup TapSocialWorkerChangeReasonLookupButton()
        {
            WaitForElementToBeClickable(socialWorkerChangeReasonLookupButton);
            Click(socialWorkerChangeReasonLookupButton);

            return this;
        }

        public ReasonForChangeOfSocialWorkerDialogPopup TapSocialWorkerChangeReasonClearButton()
        {
            WaitForElementToBeClickable(socialWorkerChangeReasonClearButton);
            Click(socialWorkerChangeReasonClearButton);

            return this;
        }

        public ReasonForChangeOfSocialWorkerDialogPopup TapOkButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);

            return this;
        }

    }
}

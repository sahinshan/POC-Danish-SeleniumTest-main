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
    public class ReactivateCasePopup : CommonMethods
    {
        public ReactivateCasePopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=case&')]");
        readonly By iframe_InpatientCaseReactivation = By.Id("iframe_InpatientCaseReactivation");

        readonly By popupHeader = By.XPath("//*[@id='CWDialogTitle']");

        readonly By inputStatusLabel = By.XPath("//*[@id='picklist']/label");
        readonly By inputStatusPicklist = By.XPath("//*[@id='CWCaseStatusId']");
        
        readonly By reasonForReopeningLabel = By.XPath("//*[@id='CWCaseReopenReasonIdLabel']");
        readonly By reasonForReopeningLookupButton = By.XPath("//*[@id='CWLookupBtn_CWCaseReopenReasonId']");

        readonly By cancelButton = By.Id("CWCancel");
        readonly By okButton = By.Id("CWReactivate");




        public ReactivateCasePopup WaitForReactivateCasePopupToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);
            
            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElement(iframe_InpatientCaseReactivation);
            SwitchToIframe(iframe_InpatientCaseReactivation);
            
            WaitForElement(inputStatusLabel);
            WaitForElement(inputStatusPicklist);

            WaitForElement(reasonForReopeningLabel);
            WaitForElement(reasonForReopeningLookupButton);

            WaitForElement(cancelButton);
            WaitForElement(okButton);

            return this;
        }


        public ReactivateCasePopup TapCancelButton()
        {
            WaitForElementToBeClickable(cancelButton);
            Click(cancelButton);

            return this;
        }

        public ReactivateCasePopup TapOkButton()
        {
            WaitForElementToBeClickable(okButton);
            Click(okButton);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public ReactivateCasePopup SelectInpatientStatus(string TextToSelect)
        {
            WaitForElementToBeClickable(inputStatusPicklist);
            SelectPicklistElementByText(inputStatusPicklist, TextToSelect);

            return this;
        }

        public ReactivateCasePopup ClickReasonForReopeningLookupButton()
        {
            WaitForElementToBeClickable(reasonForReopeningLookupButton);
            Click(reasonForReopeningLookupButton);

            return this;
        }

    }
}

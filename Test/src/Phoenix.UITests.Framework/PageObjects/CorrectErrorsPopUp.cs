using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CorrectErrorsPopUp : CommonMethods
    {
        public CorrectErrorsPopUp(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By iframe_CWBulkEditDialog = By.Id("iframe_CWBulkEditDialog");

        readonly By updateContactReasonId_CheckBox = By.Id("CWFormCheck_contactreasonid");
        readonly By contactReasonId_LookUpButtoon = By.Id("CWLookupBtn_contactreasonid");

        readonly By updateContactReceivedBy_CheckBox  = By.Id("CWFormCheck_contactreceivedbyid");
        readonly By contactReceivedBy_LookUpButton = By.Id("CWLookupBtn_contactreceivedbyid");

        readonly By updateContactSource_CheckBox = By.Id("CWFormCheck_contactsourceid");
        readonly By contactSource_LookUpButton = By.Id("CWLookupBtn_contactsourceid");

        readonly By updatePresentingPriority_CheckBox = By.Id("CWFormCheck_presentingpriorityid");
        readonly By presentingPriority_LookUpButton = By.Id("CWLookupBtn_presentingpriorityid");

        readonly By auditReason_LookUpButton = By.Id("CWLookupBtn_CWAuditReasonId");

        readonly By update_Button = By.Id("CWUpdateButton");
        readonly By close_Button = By.Id("CWCloseButton");




        public CorrectErrorsPopUp WaitForCorrectErrorsPopupToLoad()
        {
            WaitForElement(iframe_CWBulkEditDialog);
            SwitchToIframe(iframe_CWBulkEditDialog);

            WaitForElement(update_Button);
            WaitForElement(close_Button);

            return this;
        }


        public CorrectErrorsPopUp ClickUpdateContactReason()
        {
            Click(updateContactReasonId_CheckBox);
            return this;
        }

        public CorrectErrorsPopUp ClickUpdateContactReasonLookUp()
        {
            Click(contactReasonId_LookUpButtoon);
            return this;
        }

        public CorrectErrorsPopUp ClickUpdateContactReceivedBy()
        {
            Click(updateContactReceivedBy_CheckBox);
            return this;
        }

        public CorrectErrorsPopUp ClickContactReceivedByLookUButton()
        {
            Click(contactReceivedBy_LookUpButton);
            return this;
        }

        public CorrectErrorsPopUp ClickUpdateContactSource()
        {
            Click(updateContactSource_CheckBox);
            return this;
        }

        public CorrectErrorsPopUp ClickContactSourceLookUpButton()
        {
            Click(contactSource_LookUpButton);
            return this;
        }

        public CorrectErrorsPopUp ClickUpdatePresentingPriority()
        {
            Click(updatePresentingPriority_CheckBox);
            return this;
        }

        public CorrectErrorsPopUp ClickPresentingPriorityLookUpButton()
        {
            Click(presentingPriority_LookUpButton);
            return this;
        }

        public CorrectErrorsPopUp ClickReasonLookUpButton()
        {
            Click(auditReason_LookUpButton);
            return this;
        }

        public CorrectErrorsPopUp ClickUpdateButton()
        {
            WaitForElementToBeClickable(update_Button);
            Click(update_Button);
            return this;
        }
    }
}

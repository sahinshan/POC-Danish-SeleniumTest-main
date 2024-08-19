using Microsoft.Office.Interop.Word;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class AssignRecordPopup : CommonMethods
    {
        public AssignRecordPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By assignDialogIFrame = By.Id("iframe_CWAssignDialog");
        //readonly By popupHeader = By.Id("CWHeaderText");

        readonly By ressponsibleTeamId = By.Id("CWOwnerIdLink");
        readonly By ressponsibleTeamIdLookUpButton = By.Id("CWOwnerLookupButton");
        readonly By responsibleTeamIdRemoveButton = By.Id("CWClearOwnerLookupButton");

        readonly By userDecisionField = By.Id("CWUserDecision");

        readonly By responsibleUserId = By.Id("CWUserIdLink");
        readonly By responsibleUserIdLookUpButton = By.Id("CWUserLookupButton");
        readonly By responsibleUserIdRemoveButton = By.Id("CWClearUserLookupButton");

        readonly By socialWorkerChangeReason = By.Id("CWUserChangeReasonId_Link");
        readonly By socialWorkerChangeReasonLookUpButton = By.Id("CWLookupBtn_CWUserChangeReasonId");
        readonly By socialWorkerChangeReasonRemoveButton = By.Id("CWClearLookup_CWUserChangeReasonId");

        readonly By includeInactive = By.Id("CWIncludeInactive");
        readonly By checkall_CheckBox = By.Id("CWCheckAll");
        readonly By recordRelatedItems = By.Id("CWRelatedRecordsList");

        readonly By okButton = By.Id("CWSave");
        readonly By cancelButton = By.Id("CWClose");

        IWebElement assignDialogIFrameElement { get { return driver.FindElement(assignDialogIFrame); } }



        readonly By popupHeader = By.XPath("//h1[@id='CWHeaderText'][text()='Assign']");
        
        public AssignRecordPopup WaitForAssignRecordPopupToLoad(bool userDecisionFieldVisible = true)
        {
            WaitForElement(assignDialogIFrame);
            SwitchToIframe(assignDialogIFrame);

            WaitForElement(popupHeader);

            WaitForElement(ressponsibleTeamId);
            WaitForElement(ressponsibleTeamIdLookUpButton);
            WaitForElement(responsibleTeamIdRemoveButton);
            
            if(userDecisionFieldVisible)
                WaitForElement(userDecisionField);
            
            WaitForElement(okButton);
            WaitForElement(cancelButton);
            
            return this;
        }

        public AssignRecordPopup WaitForAssignRecordPopupForPersonRecordsToLoad()
        {
            WaitForElement(assignDialogIFrame);
            SwitchToIframe(assignDialogIFrame);

            WaitForElement(popupHeader);

            WaitForElement(ressponsibleTeamId);
            WaitForElement(ressponsibleTeamIdLookUpButton);
            WaitForElement(responsibleTeamIdRemoveButton);
            WaitForElement(okButton);
            WaitForElement(cancelButton);

            return this;
        }

        public AssignRecordPopup WaitForAssignRecordForOrganisationalRiskManagementRecordPopupToLoad()
        {

            Wait.Until(c => c.FindElement(assignDialogIFrame));
            driver.SwitchTo().Frame(driver.FindElement(assignDialogIFrame));

            WaitForElement(popupHeader);

            WaitForElement(ressponsibleTeamId);
            WaitForElement(ressponsibleTeamIdLookUpButton);
            WaitForElement(responsibleTeamIdRemoveButton);
            WaitForElement(userDecisionField);
            WaitForElement(okButton);
            WaitForElement(cancelButton);


            return this;
        }

        public AssignRecordPopup WaitForAssignRecordPopupForPrimarySupportToLoad()
        {

            Wait.Until(c => c.FindElement(assignDialogIFrame));
            driver.SwitchTo().Frame(driver.FindElement(assignDialogIFrame));


            WaitForElement(popupHeader);

            WaitForElement(ressponsibleTeamId);
            WaitForElement(ressponsibleTeamIdLookUpButton);
            WaitForElement(responsibleTeamIdRemoveButton);
            WaitForElement(okButton);
            WaitForElement(cancelButton);


            return this;
        }

        public AssignRecordPopup ClickResponsibleTeamLookupButton()


        {
   
            WaitForElementToBeClickable(ressponsibleTeamIdLookUpButton);
            Click(ressponsibleTeamIdLookUpButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public AssignRecordPopup TapOkButton()
        {
            Click(okButton);
            
            WaitForElementNotVisible("CWRefreshPanel", 7);

            driver.SwitchTo().ParentFrame();

            return this;
        }

        public AssignRecordPopup SelectResponsibleUserDecision(string TextToSelect)
        {
            SelectPicklistElementByText(userDecisionField, TextToSelect);
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public AssignRecordPopup WaitForAssignRecordAlertAndHazardPopupToLoad()
        {

            Wait.Until(c => c.FindElement(assignDialogIFrame));
            driver.SwitchTo().Frame(driver.FindElement(assignDialogIFrame));

            
            WaitForElement(popupHeader);

            WaitForElement(ressponsibleTeamId);
            WaitForElement(ressponsibleTeamIdLookUpButton);
            WaitForElement(responsibleTeamIdRemoveButton);
            WaitForElement(checkall_CheckBox);
            WaitForElement(includeInactive);
            WaitForElement(recordRelatedItems);
            WaitForElement(okButton);
            WaitForElement(cancelButton);


            return this;
        }

        public AssignRecordPopup ClickResponsibleUserLookupButton()
        {
            WaitForElementToBeClickable(responsibleUserIdLookUpButton);
            Click(responsibleUserIdLookUpButton);

            return this;
        }

        public AssignRecordPopup ClickSocialWorkerChangeReasonLookUpButton()
        {
            WaitForElementToBeClickable(socialWorkerChangeReasonLookUpButton);
            Click(socialWorkerChangeReasonLookUpButton);

            return this;
        }

    }
}

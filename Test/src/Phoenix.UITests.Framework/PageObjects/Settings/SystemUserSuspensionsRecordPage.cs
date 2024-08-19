
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SystemUserSuspensionsRecordPage : CommonMethods
    {
        public SystemUserSuspensionsRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region IFrame
        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By recordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=systemusersuspension&')]");

        readonly By iframeEmploymentContract = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=systemuseremploymentcontract&')]");
        readonly By iframe_SystemUserSuspension = By.Id("iframe_SystemUserSuspension");

        #endregion IFrame



        #region Fields

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='System User Suspension: ']");
        readonly By save_Button = By.Id("TI_SaveButton");
        readonly By saveAndClose_Button = By.Id("TI_SaveAndCloseButton");


        readonly By systemUser_Field = By.Id("CWField_systemuserid");
        readonly By systemUser_LinkField = By.Id("CWField_systemuserid_Link");
        readonly By systemUser_LoopUpButton = By.Id("CWLookupBtn_systemuserid");

        readonly By suspensionStartDate_Field = By.Id("CWField_suspensionstartdate");
        readonly By suspensionsStartTime_Field = By.Id("CWField_suspensionstartdate_Time");

        readonly By contracts_LookupButton = By.Id("CWLookupBtn_contracts");
        readonly By contracts_Field = By.Id("CWField_contracts");
        readonly By contracts_LinkField = By.Id("CWField_contracts_Link");

        readonly By responsibleTeam_LookupButton = By.Id("CWLookupBtn_ownerid");
        readonly By responsibleTeam_Field = By.Id("CWField_ownerid");
        readonly By reponsibleTeam_LinkField = By.Id("CWField_ownerid_Link");

        readonly By suspensionreasonid_LookupButton = By.Id("CWLookupBtn_suspensionreasonid");
        readonly By suspensionreasonid_Field = By.Id("CWField_suspensionreasonid");
        readonly By suspensionreasonid_LinkField = By.Id("CWField_suspensionreasonid_Link");

        

        readonly By LeftMenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
        readonly By AuditLink_LeftMenu = By.XPath("//*[@id='CWNavItem_AuditHistory']");


        #endregion


        #region ErrorMessages

        readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");
        readonly By lastName_ErrorLabel = By.XPath("//*[@id='CWControlHolder_lastname']/label/span");

        #endregion ErrorMessages


        public SystemUserSuspensionsRecordPage WaitForSystemUserSuspensionsRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);
           
            return this;
        }

        public SystemUserSuspensionsRecordPage WaitForPageToLoadFromEmploymentContract()
        {
            SwitchToDefaultFrame();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(iframeEmploymentContract);
            SwitchToIframe(iframeEmploymentContract);

            WaitForElement(iframe_SystemUserSuspension);
            SwitchToIframe(iframe_SystemUserSuspension);

            return this;
        }



        public SystemUserSuspensionsRecordPage ValidateSystemUserSuspensionsRecordPageTitle(string PageTitle)
        {


            ValidateElementTextContainsText(pageHeader, PageTitle);
            return this;
        }

        public SystemUserSuspensionsRecordPage WaitForSystemUserSuspensionsRecordPageTitleToLoad(string PageTitle)
        {
            WaitForSystemUserSuspensionsRecordPageToLoad();

            WaitForElementToContainText(pageHeader, "System User Suspension:  " + PageTitle);

            return this;
        }

      

        public SystemUserSuspensionsRecordPage ValidateToolTipTextForSystemUser(string ExpectedText)
        {
            

            ValidateElementToolTip(systemUser_Field, ExpectedText);
           
            return this;
        }

      
        public SystemUserSuspensionsRecordPage ValidateSystemUser_LinkField(string ExpectedText)
        {
            WaitForElement(systemUser_LinkField);
            ValidateElementTextContainsText(systemUser_LinkField, ExpectedText);

            return this;
        }

        public SystemUserSuspensionsRecordPage ValidateContracts_LinkField(string ExpectedText)
        {
            WaitForElement(contracts_LinkField);
            ValidateElementTextContainsText(contracts_LinkField, ExpectedText);

            return this;
        }

        public SystemUserSuspensionsRecordPage ValidateResponsibleTeam_LinkField(string ExpectedText)
        {
            WaitForElement(reponsibleTeam_LinkField);
            ValidateElementTextContainsText(reponsibleTeam_LinkField, ExpectedText);

            return this;
        }

        public SystemUserSuspensionsRecordPage ValidateSuspensionReason_LinkField(string ExpectedText)
        {
            WaitForElement(suspensionreasonid_LinkField);
            ValidateElementTextContainsText(suspensionreasonid_LinkField, ExpectedText);

            return this;
        }

        public SystemUserSuspensionsRecordPage ValidateSuspensionStartDate_Field(string ExpectedText)
        {
            WaitForElement(suspensionStartDate_Field);
            ValidateElementValue(suspensionStartDate_Field, ExpectedText);

            return this;
        }

        public SystemUserSuspensionsRecordPage ValidateSystemUser_Editable(bool ExpectedText)
        {
            if (ExpectedText)
            {

                WaitForElementToBeClickable(systemUser_LinkField);
                ValidateElementEnabled(systemUser_Field);

            }
            else
            {
                WaitForElement(systemUser_LinkField, 5);
                
            }
            return this;
        }

        
        public SystemUserSuspensionsRecordPage ClickSystemUserLoopUpButton()
        {
            WaitForElement(systemUser_LoopUpButton);
            Click(systemUser_LoopUpButton);

            return this;
        }

        public SystemUserSuspensionsRecordPage ClickContractsLoopUpButton()
        {
            WaitForElementToBeClickable(contracts_LookupButton);
            MoveToElementInPage(contracts_LookupButton);
            Click(contracts_LookupButton);

            return this;
        }

        public SystemUserSuspensionsRecordPage ClickSuspensionReasonLoopUpButton()
        {
            WaitForElement(suspensionreasonid_LookupButton);
            Click(suspensionreasonid_LookupButton);

            return this;
        }

        public SystemUserSuspensionsRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(save_Button);
            Click(save_Button);
            return this;
        }

        public SystemUserSuspensionsRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndClose_Button);
            Click(saveAndClose_Button);
            return this;
        }

        public SystemUserSuspensionsRecordPage ValidateAvailableFields()
        {
           
            ValidateElementEnabled(systemUser_Field);
            ValidateElementEnabled(contracts_Field);
            ValidateElementEnabled(suspensionStartDate_Field);
            ValidateElementEnabled(responsibleTeam_Field);
            ValidateElementEnabled(suspensionreasonid_Field);
           
            return this;
        }


        public SystemUserSuspensionsRecordPage ValidateStartDateValue(string ExpectedText)
        {

           
            ValidateElementEnabled(suspensionStartDate_Field);
            ValidateElementTextContainsText(suspensionStartDate_Field, ExpectedText);

            return this;
        }

        public SystemUserSuspensionsRecordPage ValidateMessageAreaText(string ExpectedText)
        {
            ValidateElementText(notificationMessage, ExpectedText);

            return this;
        }
        public SystemUserSuspensionsRecordPage ValidateLastNameFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(lastName_ErrorLabel, ExpectedText);

            return this;
        }

        public SystemUserSuspensionsRecordPage InsertSuspensionStartDate(string TextToInsert)
        {
            WaitForElement(suspensionStartDate_Field);
            SendKeys(suspensionStartDate_Field, TextToInsert);

            return this;
        }

        public SystemUserSuspensionsRecordPage InsertsuspensionStartTime(string TextToInsert)
        {
            WaitForElement(suspensionsStartTime_Field);
            SendKeys(suspensionsStartTime_Field, TextToInsert);

            return this;
        }

        public SystemUserSuspensionsRecordPage NavigateToAuditSubPage()
        {
            WaitForElementToBeClickable(LeftMenuButton);
            Click(LeftMenuButton);

            WaitForElementToBeClickable(AuditLink_LeftMenu);
            Click(AuditLink_LeftMenu);

            return this;
        }
    }
}

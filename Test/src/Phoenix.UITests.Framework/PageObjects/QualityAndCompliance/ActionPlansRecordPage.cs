using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.QualityAndCompliance
{
    public class ActionPlansRecordPage : CommonMethods
    {
        public ActionPlansRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Options Toolbar
        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By OrgRiskActionPlanIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=organisationalriskactionplan&')]");

        readonly By pageHeader = By.XPath("//h1[contains@title='Organisational Risk Action Plan: ']");
        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By BackButton = By.XPath("//button[@title='Back']");
        readonly By Back_Button = By.XPath("//*[@id='CWToolbar']/div/div/button[@onclick = 'CW.DataForm.Close(); return false;']");
        readonly By LoadingImage = By.XPath("//*[@class = 'loader']");
        readonly By AssignButton = By.Id("TI_AssignRecordButton");
        readonly By DeleteButton = By.Id("TI_DeleteRecordButton");

        readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");

        readonly By relatedItemsDetailsElementExpanded = By.XPath("//span[text()='Related Items']/parent::div/parent::summary/parent::details[@open]");
        readonly By relatedItemsLeftSubMenu = By.XPath("//details/summary/div/span[text()='Related Items']");


        readonly By audit_MenuLeftSubMenu = By.XPath("//*[@id='CWNavItem_AuditHistory']");

        #endregion
        #region General Section Fields
        readonly By ActionPlanNumber_Field = By.Id("CWField_actionplannumber");
        readonly By ActionPlanClosureDateField_Label = By.XPath("//li[@id = 'CWLabelHolder_actionplanclosingdate']/label[text() = 'Action Plan Closure Date']");
        readonly By ActionPlanClosureDateField_MandatoryFieldLabel = By.XPath("//li[@id = 'CWLabelHolder_actionplanclosingdate']/label[text() = 'Action Plan Closure Date']/span[@class = 'mandatory' and text() = '*']");
        readonly By ActionPlanTitle_Field = By.Id("CWField_title");
        readonly By StatusForAction_Field = By.Id("CWField_statusforactionid");
        readonly By StatusForAction_MandatoryField = By.XPath("//*[@id='CWLabelHolder_statusforactionid']/label/span");
        readonly By ActionPlanDescription_Field = By.Id("CWField_descriptionofactionplan");
        readonly By ParentRisk_Field = By.Id("CWField_organisationalriskid_Link");
        readonly By ParentRisk_LookupButton = By.Id("CWLookupBtn_organisationalriskid");
        readonly By ParentRisk_MandatoryField = By.XPath("//*[@id='CWLabelHolder_organisationalriskid']/label/span");
        readonly By ResponsibleTeam_Field = By.Id("CWField_ownerid_cwname");
        readonly By ResponsibleTeam_LookupButton = By.Id("CWLookupBtn_ownerid");
        readonly By ResponsibleUser_Field = By.Id("CWField_responsibleuserid_cwname");
        readonly By ResponsibleUser_LookupButton = By.Id("CWLookupBtn_responsibleuserid");
        readonly By NextReviewDate_Field = By.Id("CWField_nextreviewdate");
        readonly By ActionPlanClosureDate_Field = By.Id("CWField_actionplanclosingdate");
        
        readonly By TitleField_ErrorLabel = By.XPath("//*[@id='CWControlHolder_title']/label/span");
        readonly By TitleField_Mandatory = By.XPath("//*[@id='CWLabelHolder_title']/label/span");

        #endregion

        public ActionPlansRecordPage WaitForActionPlansRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(OrgRiskActionPlanIFrame);
            SwitchToIframe(OrgRiskActionPlanIFrame);


            return this;
        }


        public ActionPlansRecordPage WaitForOrganisationalRiskActionPlanRecordPageToLoadFromAdvancedSearch()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(OrgRiskActionPlanIFrame);
            SwitchToIframe(OrgRiskActionPlanIFrame);


            return this;
        }

        public ActionPlansRecordPage ValidateActionPlanFields()
        {
            ValidateElementDisabled(ActionPlanNumber_Field);
            ValidateElementEnabled(ActionPlanTitle_Field);
            ValidateElementEnabled(ResponsibleUser_Field);
            ValidateElementEnabled(ActionPlanDescription_Field);
            ValidateElementEnabled(NextReviewDate_Field);
            ValidateElementDisabled(ActionPlanClosureDate_Field);
            ValidateElementEnabled(ResponsibleUser_Field);

            return this;
        }


        public ActionPlansRecordPage ValidateActionPlanMandatoryFields()
        {
            WaitForElement(TitleField_Mandatory);
            WaitForElement(StatusForAction_MandatoryField);
            WaitForElement(ParentRisk_MandatoryField);
           

            return this;
        }

        public ActionPlansRecordPage ValidateActionPlanLookUpFields()
        {
            WaitForElement(ResponsibleTeam_LookupButton);
            WaitForElement(ResponsibleUser_LookupButton);
            WaitForElement(ParentRisk_LookupButton);


            return this;
        }





        public ActionPlansRecordPage ValidateActionPlanNumber(string ExpecetedText)
        {
            WaitForElement(ActionPlanNumber_Field);
            ValidateElementValue(ActionPlanNumber_Field, ExpecetedText);
            return this;
        }

        public ActionPlansRecordPage NavigateToAuditPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(audit_MenuLeftSubMenu);
            Click(audit_MenuLeftSubMenu);

            return this;
        }
        public ActionPlansRecordPage WaitForActionPlansRecordPageElementsToLoad()
        {

            WaitForElement(SaveButton);
            WaitForElement(SaveAndCloseButton);
            WaitForElement(ActionPlanTitle_Field);
            WaitForElement(ActionPlanNumber_Field);
            WaitForElement(StatusForAction_Field);
            WaitForElement(ActionPlanClosureDateField_Label);
            WaitForElement(ActionPlanClosureDate_Field);

            return this;
        }

        public ActionPlansRecordPage ClickSaveButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            while (GetElementVisibility(LoadingImage))
            {
                WaitForElementNotVisible(LoadingImage, 5);
            }

            return this;
        }


        public ActionPlansRecordPage ClickAssignButton()
        {
            WaitForElement(AssignButton);
            Click(AssignButton);

            return this;
        }

        public ActionPlansRecordPage ClickDeleteButton()
        {
            WaitForElement(DeleteButton);
            Click(DeleteButton);

            return this;
        }


        public ActionPlansRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(SaveAndCloseButton, 5);
            Click(SaveAndCloseButton);

            while (GetElementVisibility(LoadingImage))
            {
                WaitForElementNotVisible(LoadingImage, 5);
            }

            return this;
        }


        public ActionPlansRecordPage ValidateMessageAreaText(string ExpectedText)
        {
            ValidateElementText(notificationMessage, ExpectedText);

            return this;
        }

        public ActionPlansRecordPage ValidateTitleFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(TitleField_ErrorLabel, ExpectedText);

            return this;
        }
        public ActionPlansRecordPage ClickBackButton()
        {
            WaitForElement(BackButton, 5);
            Click(BackButton);

            return this;
        }

        #region General Section Fields

        public string GetActionPlansRecordNumber()
        {
            string actionPlanNumber = GetElementValue(ActionPlanNumber_Field);

            return actionPlanNumber;
        }

        public ActionPlansRecordPage ValidateActionPlanClosureDateFieldLabelText()
        {
            Assert.IsTrue(GetElementVisibility(ActionPlanClosureDateField_Label));

            return this;
        }


        public ActionPlansRecordPage ValidateActionPlanTitleField(string expectedValue)
        {
            WaitForElementVisible(ActionPlanTitle_Field);
            string actualValue = GetElementValue(ActionPlanTitle_Field);
            Assert.AreEqual(expectedValue, actualValue);

            return this;
        }

        public ActionPlansRecordPage ValidateActionFieldDescriptionField(string expectedValue)
        {
            WaitForElementVisible(ActionPlanDescription_Field);
            string actualValue = GetElementText(ActionPlanDescription_Field);
            Assert.AreEqual(expectedValue, actualValue);

            return this;
        }

        public ActionPlansRecordPage ValidateStatusForActionField(string ExpectedValue)
        {
            WaitForElementVisible(StatusForAction_Field);
            ValidatePicklistSelectedText(StatusForAction_Field, ExpectedValue);

            return this;
        }

        public ActionPlansRecordPage ValidateNextReviewDateField(string expectedValue)
        {
            WaitForElementVisible(NextReviewDate_Field);
            string actualValue = GetElementValue(NextReviewDate_Field);
            Assert.AreEqual(expectedValue, actualValue);

            return this;
        }

        public ActionPlansRecordPage ValidateActionPlanClosureDateField(string expectedValue)
        {
            WaitForElementVisible(ActionPlanClosureDate_Field);
            //  ValidateElementValue(ActionPlanClosureDate_Field, expectedValue);
            Thread.Sleep(3000);
            string actualValue = GetElementValue(ActionPlanClosureDate_Field);
            Assert.AreEqual(expectedValue, actualValue);

            return this;
        }

        public ActionPlansRecordPage InsertTitle(string TitleText)
        {
            WaitForElementVisible(ActionPlanTitle_Field);
            SendKeys(ActionPlanTitle_Field, TitleText);

            return this;
        }

        public ActionPlansRecordPage SelectStatusForAction(String OptionToSelect)
        {            
            WaitForElementVisible(StatusForAction_Field);
            SelectPicklistElementByText(StatusForAction_Field, OptionToSelect);


            return this;
        }

        public ActionPlansRecordPage InsertActionPlanDescription(string ActionPlanDescriptionText)
        {
            WaitForElementVisible(ActionPlanDescription_Field);
            SendKeys(ActionPlanDescription_Field, ActionPlanDescriptionText);

            return this;
        }

        public ActionPlansRecordPage ClickParentRiskLookupButton()
        {
            WaitForElementVisible(ParentRisk_LookupButton);
            Click(ParentRisk_LookupButton);

            return this;
        }

        public ActionPlansRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementVisible(ResponsibleTeam_LookupButton);
            Click(ResponsibleTeam_LookupButton);

            return this;
        }

        public ActionPlansRecordPage ClickResponsibleUserLookupButton()
        {
            WaitForElementVisible(ResponsibleUser_LookupButton);
            Click(ResponsibleUser_LookupButton);

            return this;
        }

        public ActionPlansRecordPage InsertNextReviewDate(string DateToInsert)
        {
            WaitForElement(NextReviewDate_Field,5);
            
            SendKeys(NextReviewDate_Field, DateToInsert);

            return this;
        }

        public ActionPlansRecordPage InsertActionPlanClosureDate(string DateToInsert)
        {            
            WaitForElementVisible(ActionPlanClosureDate_Field);
            Thread.Sleep(5000);
           
            SendKeys(ActionPlanClosureDate_Field, DateToInsert);

            return this;
        }

        #endregion

    }
}

using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonPortalTaskRecordPage : CommonMethods
    {

        public PersonPortalTaskRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=portaltask&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");

        
        readonly By Website_FieldName = By.XPath("//*[@id='CWLabelHolder_websiteid']/label[text()='Website']");
        readonly By TargetUser_FieldName = By.XPath("//*[@id='CWLabelHolder_targetuserid']/label[text()='Target User']");
        readonly By Title_FieldName = By.XPath("//*[@id='CWLabelHolder_name']/label[text()='Title']");
        readonly By Action_FieldName = By.XPath("//*[@id='CWLabelHolder_portaltaskactionid']/label[text()='Action']");
        readonly By DueDate_FieldName = By.XPath("//*[@id='CWLabelHolder_duedate']/label[text()='Due Date']");
        readonly By Record_FieldName = By.XPath("//*[@id='CWLabelHolder_recordid']/label[text()='Record']");
        readonly By Workflow_FieldName = By.XPath("//*[@id='CWLabelHolder_workflowid']/label[text()='Workflow']");
        readonly By Status_FieldName = By.XPath("//*[@id='CWLabelHolder_portaltaskstatusid']/label[text()='Status']");
        readonly By TargetPage_FieldName = By.XPath("//*[@id='CWLabelHolder_targetpageid']/label[text()='Target Page']");
        readonly By ResponsibleTeam_FieldName = By.XPath("//*[@id='CWLabelHolder_ownerid']/label[text()='Responsible Team']");


        readonly By NotificationMessage_Field = By.XPath("//*[@id='CWNotificationMessage_DataForm']");


        readonly By Website_FieldLink = By.Id("CWField_websiteid_Link");
        readonly By Website_RemoveButton = By.Id("CWClearLookup_websiteid");
        readonly By Website_LookupButton = By.Id("CWLookupBtn_websiteid");

        readonly By TargetUser_FieldLink = By.Id("CWField_targetuserid_Link");
        readonly By TargetUser_RemoveButton = By.Id("CWClearLookup_targetuserid");
        readonly By TargetUser_LookupButton = By.Id("CWLookupBtn_targetuserid");
        readonly By TargetUser_ErrorLabel = By.XPath("//*[@id='CWControlHolder_targetuserid']/label/span");

        readonly By Title_Field = By.XPath("//*[@id='CWField_name']");
        readonly By Title_ErrorLabel = By.XPath("//*[@id='CWControlHolder_name']/label/span");
        readonly By Action_Picklist = By.XPath("//*[@id='CWField_portaltaskactionid']");
        readonly By Action_ErrorLabel = By.XPath("//*[@id='CWControlHolder_portaltaskactionid']/label/span");
        readonly By DueDate_DateField = By.XPath("//*[@id='CWField_duedate']");
        readonly By Status_Picklist = By.XPath("//*[@id='CWField_portaltaskstatusid']");

        readonly By Workflow_FieldLink = By.Id("CWField_workflowid_Link");
        readonly By Workflow_RemoveButton = By.Id("CWClearLookup_workflowid");
        readonly By Workflow_LookupButton = By.Id("CWLookupBtn_workflowid");
        readonly By Workflow_ErrorLabel = By.XPath("//*[@id='CWControlHolder_workflowid']/label/span");

        readonly By Record_FieldLink = By.Id("CWField_recordid_Link");
        readonly By Record_RemoveButton = By.Id("CWClearLookup_recordid");
        readonly By Record_LookupButton = By.Id("CWLookupBtn_recordid");
        readonly By Record_ErrorLabel = By.XPath("//*[@id='CWControlHolder_recordid']/label/span");

        readonly By TargetPage_FieldLink = By.Id("CWField_targetpageid_Link");
        readonly By TargetPage_RemoveButton = By.Id("CWClearLookup_targetpageid");
        readonly By TargetPage_LookupButton = By.Id("CWLookupBtn_targetpageid");
        readonly By TargetPage_ErrorLabel = By.XPath("//*[@id='CWControlHolder_targetpageid']/label/span");

        readonly By ResponsibleTeam_FieldLink = By.Id("CWField_ownerid_Link");
        readonly By ResponsibleTeam_RemoveButton = By.Id("CWClearLookup_ownerid");
        readonly By ResponsibleTeam_LookupButton = By.Id("CWLookupBtn_ownerid");





        public PersonPortalTaskRecordPage WaitForPersonPortalTaskRecordPageToLoad()
        {
            this.driver.SwitchTo().DefaultContent();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);

            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            this.WaitForElement(Website_FieldName);
            this.WaitForElement(TargetUser_FieldName);
            this.WaitForElement(Title_FieldName);
            this.WaitForElement(Action_FieldName);
            this.WaitForElement(DueDate_FieldName);
            this.WaitForElement(ResponsibleTeam_FieldName);
            this.WaitForElement(Status_FieldName);

            return this;
        }

        public PersonPortalTaskRecordPage ValidateNotificationMessageText(string ExpectedText)
        {
            ValidateElementText(NotificationMessage_Field, ExpectedText);

            return this;
        }
        public PersonPortalTaskRecordPage ValidateWebSiteFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Website_FieldLink, ExpectedText);

            return this;
        }
        public PersonPortalTaskRecordPage ValidateTargetUserFieldLinkText(string ExpectedText)
        {
            ValidateElementText(TargetUser_FieldLink, ExpectedText);

            return this;
        }
        public PersonPortalTaskRecordPage ValidateTitleFieldText(string ExpectedText)
        {
            ValidateElementValue(Title_Field, ExpectedText);

            return this;
        }
        public PersonPortalTaskRecordPage ValidateActionSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Action_Picklist, ExpectedText);

            return this;
        }
        public PersonPortalTaskRecordPage ValidateDueDateFieldText(string ExpectedText)
        {
            ValidateElementValue(DueDate_DateField, ExpectedText);

            return this;
        }
        public PersonPortalTaskRecordPage ValidateStatusSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Status_Picklist, ExpectedText);

            return this;
        }
        public PersonPortalTaskRecordPage ValidateWorkflowFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Workflow_FieldLink, ExpectedText);

            return this;
        }
        public PersonPortalTaskRecordPage ValidateTargetPageFieldLinkText(string ExpectedText)
        {
            ValidateElementText(TargetPage_FieldLink, ExpectedText);

            return this;
        }
        public PersonPortalTaskRecordPage ValidateResponsibleTeamFieldLinkText(string ExpectedText)
        {
            ValidateElementText(ResponsibleTeam_FieldLink, ExpectedText);

            return this;
        }
        public PersonPortalTaskRecordPage ValidateRecordFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Record_FieldLink, ExpectedText);

            return this;
        }



        public PersonPortalTaskRecordPage ValidateTitleFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Title_ErrorLabel, ExpectedText);

            return this;
        }
        public PersonPortalTaskRecordPage ValidateTargetUserFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(TargetUser_ErrorLabel, ExpectedText);

            return this;
        }
        public PersonPortalTaskRecordPage ValidateActionFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Action_ErrorLabel, ExpectedText);

            return this;
        }
        public PersonPortalTaskRecordPage ValidateWorkflowFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Workflow_ErrorLabel, ExpectedText);

            return this;
        }
        public PersonPortalTaskRecordPage ValidateRecordFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Record_ErrorLabel, ExpectedText);

            return this;
        }
        public PersonPortalTaskRecordPage ValidateTargetPageFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(TargetPage_ErrorLabel, ExpectedText);

            return this;
        }



        public PersonPortalTaskRecordPage ValidateNotificationMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(NotificationMessage_Field);
            else
                WaitForElementNotVisible(NotificationMessage_Field, 3);

            return this;
        }
        public PersonPortalTaskRecordPage ValidateTitleFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Title_ErrorLabel);
            else
                WaitForElementNotVisible(Title_ErrorLabel, 3);

            return this;
        }
        public PersonPortalTaskRecordPage ValidateTargetUserFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(TargetUser_ErrorLabel);
            else
                WaitForElementNotVisible(TargetUser_ErrorLabel, 3);

            return this;
        }
        public PersonPortalTaskRecordPage ValidateActionFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Action_ErrorLabel);
            else
                WaitForElementNotVisible(Action_ErrorLabel, 3);

            return this;
        }
        public PersonPortalTaskRecordPage ValidateWorkflowFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Workflow_ErrorLabel);
            else
                WaitForElementNotVisible(Workflow_ErrorLabel, 3);

            return this;
        }
        public PersonPortalTaskRecordPage ValidateRecordFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Record_ErrorLabel);
            else
                WaitForElementNotVisible(Record_ErrorLabel, 3);

            return this;
        }
        public PersonPortalTaskRecordPage ValidateTargetPageFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(TargetPage_ErrorLabel);
            else
                WaitForElementNotVisible(TargetPage_ErrorLabel, 3);

            return this;
        }



        public PersonPortalTaskRecordPage ValidateWorkflowFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            { 
                WaitForElementVisible(Workflow_FieldName);
                WaitForElementVisible(Workflow_LookupButton);
            }
            else
            {
                WaitForElementNotVisible(Workflow_FieldName, 3);
                WaitForElementNotVisible(Workflow_FieldLink, 3);
                WaitForElementNotVisible(Workflow_LookupButton, 3);
            }

            return this;
        }
        public PersonPortalTaskRecordPage ValidateTargetPageFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(TargetPage_FieldName);
                WaitForElementVisible(TargetPage_LookupButton);
            }
            else
            {
                WaitForElementNotVisible(TargetPage_FieldName, 3);
                WaitForElementNotVisible(TargetPage_FieldLink, 3);
                WaitForElementNotVisible(TargetPage_LookupButton, 3);
            }

            return this;
        }
        public PersonPortalTaskRecordPage ValidateRecordFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Record_FieldName);
                WaitForElementVisible(Record_LookupButton);
            }
            else
            {
                WaitForElementNotVisible(Record_FieldName, 3);
                WaitForElementNotVisible(Record_FieldLink, 3);
                WaitForElementNotVisible(Record_LookupButton, 3);
            }

            return this;
        }



        public PersonPortalTaskRecordPage InsertTitle(string TextToInsert)
        {
            SendKeys(Title_Field, TextToInsert);

            return this;
        }
        public PersonPortalTaskRecordPage InsertDueDate(string TextToInsert)
        {
            SendKeys(DueDate_DateField, TextToInsert);

            return this;
        }
        public PersonPortalTaskRecordPage SelectStatus(string TextToSelect)
        {
            SelectPicklistElementByText(Status_Picklist, TextToSelect);

            return this;
        }
        public PersonPortalTaskRecordPage SelectAction(string TextToSelect)
        {
            SelectPicklistElementByText(Action_Picklist, TextToSelect);

            return this;
        }




        public PersonPortalTaskRecordPage TapWebsiteLookupButton()
        {
            Click(Website_LookupButton);

            return this;
        }
        public PersonPortalTaskRecordPage TapWebsiteRemoveButton()
        {
            Click(Website_RemoveButton);

            return this;
        }
        public PersonPortalTaskRecordPage TapTargetUserLookupButton()
        {
            Click(TargetUser_LookupButton);

            return this;
        }
        public PersonPortalTaskRecordPage TapTargetUserRemoveButton()
        {
            Click(TargetUser_RemoveButton);

            return this;
        }
        public PersonPortalTaskRecordPage TapRecordLookupButton()
        {
            Click(Record_LookupButton);

            return this;
        }
        public PersonPortalTaskRecordPage TapRecordRemoveButton()
        {
            Click(Record_RemoveButton);

            return this;
        }
        public PersonPortalTaskRecordPage TapTargetPageLookupButton()
        {
            Click(TargetPage_LookupButton);

            return this;
        }
        public PersonPortalTaskRecordPage TapTargetPageRemoveButton()
        {
            Click(TargetPage_RemoveButton);

            return this;
        }
        public PersonPortalTaskRecordPage TapResponsibleTeamLookupButton()
        {
            Click(ResponsibleTeam_LookupButton);

            return this;
        }
        public PersonPortalTaskRecordPage TapResponsibleTeamRemoveButton()
        {
            Click(ResponsibleTeam_RemoveButton);

            return this;
        }
        public PersonPortalTaskRecordPage TapWorkflowLookupButton()
        {
            Click(Workflow_LookupButton);

            return this;
        }
        public PersonPortalTaskRecordPage TapWorkflowRemoveButton()
        {
            Click(Workflow_RemoveButton);

            return this;
        }



        public PersonPortalTaskRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public PersonPortalTaskRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public PersonPortalTaskRecordPage ClickDeleteButton()
        {
            this.Click(deleteButton);

            return this;
        }

    }
}

    
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WorkflowRecordPage : CommonMethods
    {
        public WorkflowRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=workflow&')]");
        readonly By CWHTMLResourcePanel_IFrame = By.XPath("//*[@id='CWUrlPanel_IFrame']"); 


        
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By publishButton = By.Id("TI_PublishRuleButton");
        readonly By unpublishButton = By.Id("TI_UnpublishRuleButton");
        readonly By ValidateWorkflowButton = By.Id("TI_ValidateWorkflowButton");



        #region Navigation Area

        readonly By MenuButton = By.XPath("//li[@id='CWNavGroup_Menu']/button");
        readonly By workflowJobs_SubLink = By.XPath("//*[@id='CWNavItem_WokflowJobs']");
        

        readonly By WorkflowTab = By.XPath("//*[@id='CWNavGroup_Workflow']/a");
        readonly By DetailsTab = By.XPath("//*[@id='CWNavGroup_EditForm']/a");



        #endregion

        #region Worflows tab

        By AddConditionLink_Level1(string AccordionPanelNumber, string LinkPosition) => By.XPath("//*[@id='AccordionPanelContent" + AccordionPanelNumber + "']/ul[" + LinkPosition + "]/li/a[text()='Add Condition']");
        By AddActionLink_Level1(string AccordionPanelNumber, string LinkPosition) => By.XPath("//*[@id='AccordionPanelContent" + AccordionPanelNumber + "']/ul[" + LinkPosition + "]/li/a[text()='Add Action']");

        By ExistingConditionLink_Level1(string AccordionPanelNumber, string LinkPosition) => By.XPath("//*[@id='AccordionPanelContent" + AccordionPanelNumber + "']/ul[" + LinkPosition + "]/li/ul/li/p/a");
        By ExistingActionLink_Level1(string AccordionPanelNumber, string LinkPosition) => By.XPath("//*[@id='AccordionPanelContent" + AccordionPanelNumber + "']/ul[" + LinkPosition + "]/li/ul/li/a");


        By ValidationErrorMessage_Level1(int GroupConditionNumber) => By.XPath("//*[@id='AccordionPanelContent1']/ul[" + GroupConditionNumber + "]/li/ul/li/div[@class='invalid-rule']");
        By ValidationErrorMessage_Level2(int GroupConditionNumber) => By.XPath("//*[@id='AccordionPanelContent1']/ul[" + GroupConditionNumber + "]/li/ul/li/ul/li/ul/li/div[@class='invalid-rule']");


        #endregion

        #region Details tab

        readonly By notificationMessage = By.XPath("//*[@id='CWNotificationMessage_DataForm']");


        readonly By name_FieldName = By.XPath("//*[@id='CWLabelHolder_name']/label");
        readonly By RecordType_FieldName = By.XPath("//*[@id='CWLabelHolder_businessobjectid']/label");
        readonly By Scope_FieldName = By.XPath("//*[@id='CWLabelHolder_scopeid']/label");
        readonly By Type_FieldName = By.XPath("//*[@id='CWLabelHolder_typeid']/label");
        readonly By DateFields_FieldName = By.XPath("//*[@id='CWLabelHolder_datefieldid']/label");
        readonly By ExecuteBefore_FieldName = By.XPath("//*[@id='CWLabelHolder_beforendays']/label");
        readonly By ExecuteAfter_FieldName = By.XPath("//*[@id='CWLabelHolder_afterndays']/label");


        readonly By name_Field = By.Id("CWField_name");
        readonly By recordType_LookupButton = By.Id("CWLookupBtn_businessobjectid");
        readonly By scope_Picklist = By.Id("CWField_scopeid");
        readonly By type_Picklist = By.Id("CWField_typeid");
        readonly By dateFields_LinkField = By.Id("CWField_datefieldid_Link");
        readonly By dateFields_LookupButton = By.Id("CWLookupBtn_datefieldid");
        readonly By dateFields_RemoveButton = By.Id("CWClearLookup_datefieldid");
        readonly By dateField_ErrorLabel = By.XPath("//*[@id='CWControlHolder_datefieldid']/label/span");
        readonly By executeBeforeInDays_Fields = By.Id("CWField_beforendays");
        readonly By executeAfterInDays_Fields = By.Id("CWField_afterndays");
        readonly By description_Field = By.Id("CWField_description");

        readonly By recordIsCreated_YesRadioButton = By.Id("CWField_recordiscreated_1");
        readonly By recordIsCreated_NoRadioButton = By.Id("CWField_recordiscreated_0");

        readonly By recordIsDeleted_YesRadioButton = By.Id("CWField_recordisdeleted_1");
        readonly By recordIsDeleted_NoRadioButton = By.Id("CWField_recordisdeleted_0");

        #endregion


        public WorkflowRecordPage WaitForDetailsTablToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            WaitForElement(name_FieldName);
            WaitForElement(RecordType_FieldName);
            WaitForElement(Scope_FieldName);
            WaitForElement(Type_FieldName);
            WaitForElement(DateFields_FieldName);
            WaitForElement(ExecuteBefore_FieldName);
            WaitForElement(ExecuteAfter_FieldName);

            return this;
        }

        public WorkflowRecordPage WaitForWorkflowRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(saveButton);
            //WaitForElement(publishButton);


            return this;
        }

        public WorkflowRecordPage WaitForUnpublishedWorkflowRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(CWHTMLResourcePanel_IFrame);
            SwitchToIframe(CWHTMLResourcePanel_IFrame);

            WaitForElement(saveButton);
            WaitForElement(publishButton);
            WaitForElement(ValidateWorkflowButton);

            System.Threading.Thread.Sleep(1500);

            return this;
        }

        public WorkflowRecordPage WaitForPublishedWorkflowRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(CWHTMLResourcePanel_IFrame);
            SwitchToIframe(CWHTMLResourcePanel_IFrame);

            WaitForElement(unpublishButton);


            return this;
        }



        public WorkflowRecordPage ClickDetailsTab()
        {
            Click(DetailsTab);

            return this;
        }
        public WorkflowRecordPage ClickWorkflowTab()
        {
            Click(WorkflowTab);

            return this;
        }


        #region Workflows tab

        public WorkflowRecordPage ValidateExistingConditionLink_Level1Visibility(string AccordionPanelNumber, string LinkPosition, bool ExpectVisible)
        {
            if(ExpectVisible)
                WaitForElementVisible(ExistingConditionLink_Level1(AccordionPanelNumber, LinkPosition));
            else
                WaitForElementNotVisible(ExistingConditionLink_Level1(AccordionPanelNumber, LinkPosition), 3);

            return this;
        }

        public WorkflowRecordPage ValidateExistingConditionLink_Level1Text(string AccordionPanelNumber, string LinkPosition, string ExpectText)
        {
            ValidateElementText(ExistingConditionLink_Level1(AccordionPanelNumber, LinkPosition), ExpectText);

            return this;
        }

        public WorkflowRecordPage ValidateExistingActionLink_Level1Visibility(string AccordionPanelNumber, string LinkPosition, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ExistingActionLink_Level1(AccordionPanelNumber, LinkPosition));
            else
                WaitForElementNotVisible(ExistingActionLink_Level1(AccordionPanelNumber, LinkPosition), 3);

            return this;
        }

        public WorkflowRecordPage ValidateExistingActionLink_Level1Text(string AccordionPanelNumber, string LinkPosition, string ExpectText)
        {
            ValidateElementText(ExistingActionLink_Level1(AccordionPanelNumber, LinkPosition), ExpectText);

            return this;
        }




        public WorkflowRecordPage TapSaveButton()
        {
            WaitForElementToBeClickable(saveButton);

            Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public WorkflowRecordPage TapPublishButton()
        {
            Click(publishButton);

            return this;
        }

        public WorkflowRecordPage TapUnpublishButton()
        {
            Click(unpublishButton);

            return this;
        }

        public WorkflowRecordPage ClickValidateWorkflowButton()
        {
            Click(ValidateWorkflowButton);
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public WorkflowRecordPage ClickAddConditionLink_Level1(string AccordionPanelNumber, string LinkPosition)
        {
            Click(AddConditionLink_Level1(AccordionPanelNumber, LinkPosition));

            return this;
        }

        public WorkflowRecordPage ClickAddActionLink_Level1(string AccordionPanelNumber, string LinkPosition)
        {
            Click(AddActionLink_Level1(AccordionPanelNumber, LinkPosition));

            return this;
        }

        public WorkflowRecordPage ClickExistingActionLink_Level1(string AccordionPanelNumber, string LinkPosition)
        {
            WaitForElementToBeClickable(ExistingActionLink_Level1(AccordionPanelNumber, LinkPosition));
            Click(ExistingActionLink_Level1(AccordionPanelNumber, LinkPosition));

            return this;
        }

        public WorkflowRecordPage ClickExistingConditionLink_Level1(string AccordionPanelNumber, string LinkPosition)
        {
            Click(ExistingConditionLink_Level1(AccordionPanelNumber, LinkPosition));

            return this;
        }



        public WorkflowRecordPage ValidationErrorMessage_Level1_Visibility(int GroupConditionNumber, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ValidationErrorMessage_Level1(GroupConditionNumber));
            else
                WaitForElementNotVisible(ValidationErrorMessage_Level1(GroupConditionNumber), 3);

            return this;
        }

        public WorkflowRecordPage ValidationErrorMessage_Level2_Visibility(int GroupConditionNumber, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ValidationErrorMessage_Level2(GroupConditionNumber));
            else
                WaitForElementNotVisible(ValidationErrorMessage_Level2(GroupConditionNumber), 3);

            return this;
        }

        public WorkflowRecordPage ValidateValidationErrorMessage_Level1Text(int GroupConditionNumber, string ExpectText)
        {
            ValidateElementText(ValidationErrorMessage_Level1(GroupConditionNumber), ExpectText);

            return this;
        }

        public WorkflowRecordPage ValidateValidationErrorMessage_Level2Text(int GroupConditionNumber, string ExpectText)
        {
            ValidateElementText(ValidationErrorMessage_Level2(GroupConditionNumber), ExpectText);

            return this;
        }



        #endregion

        #region Details tab

        public WorkflowRecordPage InsertName(string TextToInsert)
        {
            SendKeys(name_Field, TextToInsert);

            return this;
        }
        public WorkflowRecordPage InsertDescription(string TextToInsert)
        {
            SendKeys(description_Field, TextToInsert);

            return this;
        }
        public WorkflowRecordPage InsertExecuteBefore(string TextToInsert)
        {
            SendKeys(executeBeforeInDays_Fields, TextToInsert);
            System.Threading.Thread.Sleep(300);
            SendKeysWithoutClearing(executeBeforeInDays_Fields, Keys.Tab);
            System.Threading.Thread.Sleep(300);

            return this;
        }
        public WorkflowRecordPage InsertExecuteAfter(string TextToInsert)
        {
            SendKeys(executeAfterInDays_Fields, TextToInsert);
            System.Threading.Thread.Sleep(300);
            SendKeysWithoutClearing(executeAfterInDays_Fields, Keys.Tab);
            System.Threading.Thread.Sleep(300);

            return this;
        }


        public WorkflowRecordPage ClickRecordTypeLookupButton()
        {
            Click(recordType_LookupButton);

            return this;
        }
        public WorkflowRecordPage ClickDateFieldsLookupButton()
        {
            Click(dateFields_LookupButton);

            return this;
        }
        public WorkflowRecordPage ClickDateFieldsRemoveButton()
        {
            Click(dateFields_RemoveButton);

            return this;
        }


        public WorkflowRecordPage SelectScope(string TextToSelect)
        {
            SelectPicklistElementByText(scope_Picklist, TextToSelect);

            return this;
        }
        public WorkflowRecordPage SelectType(string TextToSelect)
        {
            SelectPicklistElementByText(type_Picklist, TextToSelect);

            return this;
        }


        public WorkflowRecordPage ClickRecordIsCreatedYesRadioButton()
        {
            Click(recordIsCreated_YesRadioButton);

            return this;
        }
        public WorkflowRecordPage ClickRecordIsCreatedNoRadioButton()
        {
            Click(recordIsCreated_NoRadioButton);

            return this;
        }




        public WorkflowRecordPage ValidateRecordIsDeletedOptionVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(recordIsDeleted_YesRadioButton);
                WaitForElementVisible(recordIsDeleted_NoRadioButton);
            }
            else
            {
                WaitForElementNotVisible(recordIsDeleted_YesRadioButton, 3);
                WaitForElementNotVisible(recordIsDeleted_NoRadioButton, 3);
            }

            return this;
        }


        public WorkflowRecordPage ValidateDateFieldsVisibility(bool ExpectVisible)
        {
            if(ExpectVisible)
            {
                WaitForElementVisible(DateFields_FieldName);
                WaitForElementVisible(dateFields_LookupButton);
            }
            else
            {
                WaitForElementNotVisible(DateFields_FieldName, 3);
                WaitForElementNotVisible(dateFields_LookupButton, 3);
            }

            return this;
        }
        public WorkflowRecordPage ValidateExecuteBeforeFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ExecuteBefore_FieldName);
                WaitForElementVisible(executeBeforeInDays_Fields);
            }
            else
            {
                WaitForElementNotVisible(ExecuteBefore_FieldName, 3);
                WaitForElementNotVisible(executeBeforeInDays_Fields, 3);
            }

            return this;
        }
        public WorkflowRecordPage ValidateExecuteAfterFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ExecuteAfter_FieldName);
                WaitForElementVisible(executeAfterInDays_Fields);
            }
            else
            {
                WaitForElementNotVisible(ExecuteAfter_FieldName, 3);
                WaitForElementNotVisible(executeAfterInDays_Fields, 3);
            }

            return this;
        }


        public WorkflowRecordPage ValidateNotificationMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(notificationMessage);
                WaitForElementVisible(notificationMessage);
            }
            else
            {
                WaitForElementNotVisible(notificationMessage, 3);
                WaitForElementNotVisible(notificationMessage, 3);
            }

            return this;
        }
        public WorkflowRecordPage ValidateDateFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(dateField_ErrorLabel);
                WaitForElementVisible(dateField_ErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(dateField_ErrorLabel, 3);
                WaitForElementNotVisible(dateField_ErrorLabel, 3);
            }

            return this;
        }

        public WorkflowRecordPage ValidateNotificationMessageText(string ExpectedText)
        {
            ValidateElementText(notificationMessage, ExpectedText);

            return this;
        }
        public WorkflowRecordPage ValidateDateFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(dateField_ErrorLabel, ExpectedText);

            return this;
        }


        #endregion

        public WorkflowRecordPage NavigateToWorkflowJobsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(workflowJobs_SubLink);
            Click(workflowJobs_SubLink);

            return this;
        }
    }
}

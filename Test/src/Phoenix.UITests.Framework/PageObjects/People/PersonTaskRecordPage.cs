using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonTaskRecordPage : CommonMethods
    {

        public PersonTaskRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=task')]");



        readonly By messageArea = By.XPath("//*[@id='CWNotificationMessage_DataForm']");


        //contents rich textbox is inside his own iframe
        readonly By richTextBoxIframe = By.XPath("//iframe[@title='Rich Text Editor, CWField_notes']");



        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");



        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalIttemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By cloneButton = By.Id("TI_CloneRecordButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By activateButton = By.Id("TI_ActivateButton");
        readonly By completeButton = By.Id("TI_CompleteRecordButton");
        readonly By cancelButton = By.Id("TI_CancelRecordButton");



        #region Field Title

        readonly By general_SectionTitle = By.XPath("//*[@id='CWSection_General']/fieldset/div[1]/span[text()='General']");
        readonly By subject_FieldTitle = By.XPath("//*[@id='CWLabelHolder_subject']/label");
        readonly By description_FieldTitle = By.XPath("//*[@id='CWLabelHolder_notes']/label");

        readonly By details_SectionTitle = By.XPath("//*[@id='CWSection_Details']/fieldset/div[1]/span[text()='Details']");
        readonly By regarding_FieldTitle = By.XPath("//*[@id='CWLabelHolder_regardingid']/label");
        readonly By reason_FieldTitle = By.XPath("//*[@id='CWLabelHolder_activityreasonid']/label");
        readonly By priority_FieldTitle = By.XPath("//*[@id='CWLabelHolder_activitypriorityid']/label");
        readonly By due_FieldTitle = By.XPath("//*[@id='CWLabelHolder_duedate']/label");
        readonly By status_FieldTitle = By.XPath("//*[@id='CWLabelHolder_statusid']/label");
        readonly By containsInformationProvidedByThirdParty_FieldTitle = By.XPath("//*[@id='CWLabelHolder_informationbythirdparty']/label");
        readonly By responsibleTeam_FieldTitle = By.XPath("//*[@id='CWLabelHolder_ownerid']/label");
        readonly By responsibleUser_FieldTitle = By.XPath("//*[@id='CWLabelHolder_responsibleuserid']/label");
        readonly By category_FieldTitle = By.XPath("//*[@id='CWLabelHolder_activitycategoryid']/label");
        readonly By subCategory_FieldTitle = By.XPath("//*[@id='CWLabelHolder_activitysubcategoryid']/label");
        readonly By outcome_FieldTitle = By.XPath("//*[@id='CWLabelHolder_activityoutcomeid']/label");
        readonly By isCaseNote_FieldTitle = By.XPath("//*[@id='CWLabelHolder_iscasenote']/label");

        readonly By significantEventDetails_SectionTitle = By.XPath("//*[@id='CWSection_SignificantEventDetails']/fieldset/div[1]/span[text()='Significant Event Details']");
        readonly By significantEvent_FieldTitle = By.XPath("//*[@id='CWLabelHolder_issignificantevent']/label");
        readonly By eventDate_FieldTitle = By.XPath("//*[@id='CWLabelHolder_significanteventdate']/label");
        readonly By eventCategory_FieldTitle = By.XPath("//*[@id='CWLabelHolder_significanteventcategoryid']/label");
        readonly By eventSubCategory_FieldTitle = By.XPath("//*[@id='CWLabelHolder_significanteventsubcategoryid']/label");

        readonly By cloningInformation_SectionTitle = By.XPath("//*[@id='CWSection_CloningInformation']/fieldset/div[1]/span[text()='Cloning Information']");
        readonly By isCloned_FieldTitle = By.XPath("//*[@id='CWLabelHolder_iscloned']/label");
        readonly By clonedFrom_FieldTitle = By.XPath("//*[@id='CWLabelHolder_clonedfromid']/label");

        #endregion


        #region Fields

        readonly By spellCheckButton_NotesField = By.XPath("//*[@id='cke_63']/span[1]");

        readonly By subject_Field = By.XPath("//*[@id='CWField_subject']");
        readonly By subject_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_subject']/label/span");
        readonly By description_TextAreaField = By.XPath("//*[@id='CWField_notes']");
        readonly string description_TextAreaFieldName = "CWField_notes";
        By description_Field(string LineNumber) => By.XPath("/html/body/p[" + LineNumber + "]");


        readonly By regarding_LinkField = By.XPath("//*[@id='CWField_regardingid_Link']");
        readonly By regarding_RemoveButton = By.XPath("//*[@id='CWClearLookup_regardingid']");
        readonly By regarding_LookupButton = By.XPath("//*[@id='CWLookupBtn_regardingid']");

        readonly By reason_LinkField = By.XPath("//*[@id='CWField_activityreasonid_Link']");
        readonly By reason_RemoveButton = By.XPath("//*[@id='CWClearLookup_activityreasonid']");
        readonly By reason_LookupButton = By.XPath("//*[@id='CWLookupBtn_activityreasonid']");
                    
        readonly By priority_LinkField = By.XPath("//*[@id='CWField_activitypriorityid_Link']");
        readonly By priority_RemoveButton = By.XPath("//*[@id='CWClearLookup_activitypriorityid']");
        readonly By priority_LookupButton = By.XPath("//*[@id='CWLookupBtn_activitypriorityid']");
        
        readonly By due_DateField = By.XPath("//*[@id='CWField_duedate']");
        readonly By due_TimeField = By.XPath("//*[@id='CWField_duedate_Time']");
        
        readonly By status_Field = By.XPath("//*[@id='CWField_statusid']");
        readonly By status_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_statusid']/label/span");

        readonly By ContainsInformationProvidedByThirdParty_YesRadioButton = By.XPath("//*[@id='CWField_informationbythirdparty_1']");
        readonly By ContainsInformationProvidedByThirdParty_NoRadioButton = By.XPath("//*[@id='CWField_informationbythirdparty_0']");

        readonly By responsibleTeam_LinkField = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By responsibleTeam_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_ownerid']/label/span");
        readonly By responsibleTeam_RemoveButton = By.XPath("//*[@id='CWClearLookup_ownerid']");
        readonly By responsibleTeam_LookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");

        readonly By responsibleUser_LinkField = By.XPath("//*[@id='CWField_responsibleuserid_Link']");
        readonly By responsibleUser_RemoveButton = By.XPath("//*[@id='CWClearLookup_responsibleuserid']");
        readonly By responsibleUser_LookupButton = By.XPath("//*[@id='CWLookupBtn_responsibleuserid']");

        readonly By category_LinkField = By.XPath("//*[@id='CWField_activitycategoryid_Link']");
        readonly By category_RemoveButton = By.XPath("//*[@id='CWClearLookup_activitycategoryid']");
        readonly By category_LookupButton = By.XPath("//*[@id='CWLookupBtn_activitycategoryid']");

        readonly By subcategory_LinkField = By.XPath("//*[@id='CWField_activitysubcategoryid_Link']");
        readonly By subcategory_RemoveButton = By.XPath("//*[@id='CWClearLookup_activitysubcategoryid']");
        readonly By subcategory_LookupButton = By.XPath("//*[@id='CWLookupBtn_activitysubcategoryid']");

        readonly By outcome_LinkField = By.XPath("//*[@id='CWField_activityoutcomeid_Link']");
        readonly By outcome_RemoveButton = By.XPath("//*[@id='CWClearLookup_activityoutcomeid']");
        readonly By outcome_LookupButton = By.XPath("//*[@id='CWLookupBtn_activityoutcomeid']");

        readonly By isCaseNote_YesRadioButton = By.XPath("//*[@id='CWField_iscasenote_1']");
        readonly By isCaseNote_NoRadioButton = By.XPath("//*[@id='CWField_iscasenote_0']");

        readonly By significantEvent_YesRadioButton = By.XPath("//*[@id='CWField_issignificantevent_1']");
        readonly By significantEvent_NoRadioButton = By.XPath("//*[@id='CWField_issignificantevent_0']");

        readonly By EventDate_Field = By.XPath("//*[@id='CWField_significanteventdate']");
        readonly By EventDate_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_significanteventdate']/label/span");

        readonly By eventCategory_LinkField = By.XPath("//*[@id='CWField_significanteventcategoryid_Link']");
        readonly By eventCategory_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_significanteventcategoryid']/label/span");
        readonly By eventCategory_RemoveButton = By.XPath("//*[@id='CWClearLookup_significanteventcategoryid']");
        readonly By eventCategory_LookupButton = By.XPath("//*[@id='CWLookupBtn_significanteventcategoryid']");

        readonly By eventSubCategory_LinkField = By.XPath("//*[@id='CWField_significanteventsubcategoryid_Link']");
        readonly By eventSubCategory_RemoveButton = By.XPath("//*[@id='CWClearLookup_significanteventsubcategoryid']");
        readonly By eventSubCategory_LookupButton = By.XPath("//*[@id='CWLookupBtn_significanteventsubcategoryid']");

        readonly By isCloned_YesRadioButton = By.XPath("//*[@id='CWField_iscloned_1']");
        readonly By isCloned_NoRadioButton = By.XPath("//*[@id='CWField_iscloned_0']");

        readonly By clonedFrom_LinkField = By.XPath("//*[@id='CWField_clonedfromid_Link']");
        readonly By clonedFrom_RemoveButton = By.XPath("//*[@id='CWClearLookup_clonedfromid']");
        readonly By clonedFrom_LookupButton = By.XPath("//*[@id='CWLookupBtn_clonedfromid']");

        #endregion

        #region Menu

        readonly By LeftMenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
        readonly By AuditLink_LeftMenu = By.XPath("//*[@id='CWNavItem_AuditHistory']");


        #endregion


        public PersonTaskRecordPage WaitForPersonTaskRecordPageToLoad(string TaskTitle)
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            this.WaitForElement(pageHeader);

            this.WaitForElement(general_SectionTitle);
            this.WaitForElement(subject_FieldTitle);
            this.WaitForElement(description_FieldTitle);

            this.WaitForElement(details_SectionTitle);
            this.WaitForElement(regarding_FieldTitle);
            this.WaitForElement(reason_FieldTitle);
            this.WaitForElement(priority_FieldTitle);
            this.WaitForElement(due_FieldTitle);
            this.WaitForElement(status_FieldTitle);
            this.WaitForElement(containsInformationProvidedByThirdParty_FieldTitle);
            this.WaitForElement(responsibleTeam_FieldTitle);
            this.WaitForElement(responsibleUser_FieldTitle);
            this.WaitForElement(category_FieldTitle);
            this.WaitForElement(subCategory_FieldTitle);
            this.WaitForElement(outcome_FieldTitle);
            this.WaitForElement(isCaseNote_FieldTitle);

            this.WaitForElement(significantEventDetails_SectionTitle);
            this.WaitForElement(significantEvent_FieldTitle);

            this.WaitForElement(cloningInformation_SectionTitle);
            this.WaitForElement(isCloned_FieldTitle);
            this.WaitForElement(clonedFrom_FieldTitle);


            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            ValidateElementText(pageHeader, "Task:\r\n" + TaskTitle);

            this.WaitForElementVisible(subject_Field);
            this.WaitForElementVisible(due_DateField);


            return this;
        }

        public PersonTaskRecordPage WaitForInactivePersonTaskRecordPageToLoad(string TaskTitle)
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);

            WaitForElement(general_SectionTitle);
            WaitForElement(subject_FieldTitle);
            WaitForElement(description_FieldTitle);

            WaitForElement(details_SectionTitle);
            WaitForElement(regarding_FieldTitle);
            WaitForElement(reason_FieldTitle);
            WaitForElement(priority_FieldTitle);
            WaitForElement(due_FieldTitle);
            WaitForElement(status_FieldTitle);
            WaitForElement(containsInformationProvidedByThirdParty_FieldTitle);
            WaitForElement(responsibleTeam_FieldTitle);
            WaitForElement(responsibleUser_FieldTitle);
            WaitForElement(category_FieldTitle);
            WaitForElement(subCategory_FieldTitle);
            WaitForElement(outcome_FieldTitle);
            WaitForElement(isCaseNote_FieldTitle);

            WaitForElement(significantEventDetails_SectionTitle);
            WaitForElement(significantEvent_FieldTitle);

            WaitForElement(cloningInformation_SectionTitle);
            WaitForElement(isCloned_FieldTitle);
            WaitForElement(clonedFrom_FieldTitle);


            WaitForElementNotVisible(saveButton, 5);
            WaitForElementNotVisible(saveAndCloseButton, 5);

            ValidateElementText(pageHeader, "Task:\r\n" + TaskTitle);

            ValidateElementDisabled(subject_Field);

            ValidateElementDisabled(regarding_LookupButton);
            ValidateElementDisabled(reason_LookupButton);
            ValidateElementDisabled(priority_LookupButton);
            ValidateElementDisabled(due_DateField);
            ValidateElementDisabled(due_TimeField);
            ValidateElementDisabled(status_Field);
            ValidateElementDisabled(ContainsInformationProvidedByThirdParty_YesRadioButton);
            ValidateElementDisabled(ContainsInformationProvidedByThirdParty_NoRadioButton);
            ValidateElementDisabled(responsibleTeam_LookupButton);
            ValidateElementDisabled(responsibleUser_LookupButton);
            ValidateElementDisabled(category_LookupButton);
            ValidateElementDisabled(subcategory_LookupButton);
            ValidateElementDisabled(outcome_LookupButton);
            ValidateElementDisabled(isCaseNote_YesRadioButton);
            ValidateElementDisabled(isCaseNote_NoRadioButton);

            ValidateElementDisabled(significantEvent_YesRadioButton);
            ValidateElementDisabled(significantEvent_NoRadioButton);

            ValidateElementDisabled(isCloned_YesRadioButton);
            ValidateElementDisabled(isCloned_NoRadioButton);
            
            ValidateElementDisabled(clonedFrom_LookupButton);

            return this;
        }
        public PersonTaskRecordPage LoadDescriptionRichTextBox()
        {
            WaitForElement(richTextBoxIframe);
            SwitchToIframe(richTextBoxIframe);

            return this;
        }





        public PersonTaskRecordPage ValidateContainsInformationProvidedByThirdPartyFieldVisibility(bool ExpectVisible)
        {
            if(ExpectVisible)
            {
                WaitForElementVisible(containsInformationProvidedByThirdParty_FieldTitle);
                WaitForElementVisible(ContainsInformationProvidedByThirdParty_YesRadioButton);
                WaitForElementVisible(ContainsInformationProvidedByThirdParty_NoRadioButton);
            }
            else
            {
                WaitForElementNotVisible(containsInformationProvidedByThirdParty_FieldTitle, 3);
                WaitForElementNotVisible(ContainsInformationProvidedByThirdParty_YesRadioButton, 3);
                WaitForElementNotVisible(ContainsInformationProvidedByThirdParty_NoRadioButton, 3);
            }

            return this;
        }
        public PersonTaskRecordPage ValidateEventDateFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(eventDate_FieldTitle);
                WaitForElementVisible(EventDate_Field);
            }
            else
            {
                WaitForElementNotVisible(eventDate_FieldTitle, 3);
                WaitForElementNotVisible(EventDate_Field, 3);
            }

            return this;
        }
        public PersonTaskRecordPage ValidateEventCategoryFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(eventCategory_FieldTitle);
                WaitForElementVisible(eventCategory_LookupButton);
            }
            else
            {
                WaitForElementNotVisible(eventCategory_FieldTitle, 3);
                WaitForElementNotVisible(eventCategory_LookupButton, 3);
            }

            return this;
        }
        public PersonTaskRecordPage ValidateEventSubCategoryFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(eventSubCategory_FieldTitle);
                WaitForElementVisible(eventSubCategory_LookupButton);
            }
            else
            {
                WaitForElementNotVisible(eventSubCategory_FieldTitle, 3);
                WaitForElementNotVisible(eventSubCategory_LookupButton, 3);
            }

            return this;
        }
        public PersonTaskRecordPage ValidateMessageAreaVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(messageArea);
            }
            else
            {
                WaitForElementNotVisible(messageArea, 3);
            }

            return this;
        }
        public PersonTaskRecordPage ValidateSubjectErrorLabelVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(subject_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(subject_FieldErrorLabel, 3);
            }

            return this;
        }
        public PersonTaskRecordPage ValidateStatusErrorLabelVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(status_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(status_FieldErrorLabel, 3);
            }

            return this;
        }
        public PersonTaskRecordPage ValidateResponsibleTeamErrorLabelVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(responsibleTeam_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(responsibleTeam_FieldErrorLabel, 3);
            }

            return this;
        }
        public PersonTaskRecordPage ValidateEventDateErrorLabelVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(EventDate_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(EventDate_FieldErrorLabel, 3);
            }

            return this;
        }
        public PersonTaskRecordPage ValidateEventCategoryErrorLabelVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(eventCategory_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(eventCategory_FieldErrorLabel, 3);
            }

            return this;
        }







        public PersonTaskRecordPage InsertSubject(string TextToInsert)
        {
            SendKeys(subject_Field, TextToInsert);

            return this;
        }
        public PersonTaskRecordPage InsertDescription(string TextToInsert)
        {
            System.Threading.Thread.Sleep(1000);
            SetElementDisplayStyleToInline(description_TextAreaFieldName);
            SetElementVisibilityStyleToVisible(description_TextAreaFieldName);
            
            WaitForElementVisible(description_TextAreaField);
            SendKeys(description_TextAreaField, TextToInsert);

            return this;
        }
        public PersonTaskRecordPage InsertDueDate(string DateToInsert, string TimeToInsert)
        {
            SendKeys(due_DateField, DateToInsert);
            SendKeysWithoutClearing(due_DateField, Keys.Tab);
            SendKeys(due_TimeField, TimeToInsert);

            return this;
        }
        public PersonTaskRecordPage InsertEventDate(string DateToInsert)
        {
            SendKeys(EventDate_Field, DateToInsert);

            return this;
        }




        public PersonTaskRecordPage SelectStatus(string TextToSelect)
        {
            SelectPicklistElementByText(status_Field, TextToSelect);

            return this;
        }




        public PersonTaskRecordPage ClickNotesFieldSpellCheckButton()
        {
            WaitForElement(spellCheckButton_NotesField);
            Click(spellCheckButton_NotesField);

            return this;
        }
        public PersonTaskRecordPage ClickRegardingLookupButton()
        {
            Click(regarding_LookupButton);

            return this;
        }
        public PersonTaskRecordPage ClickReasonRemoveButton()
        {
            Click(reason_RemoveButton);

            return this;
        }
        public PersonTaskRecordPage ClickReasonLookupButton()
        {
            Click(reason_LookupButton);

            return this;
        }
        public PersonTaskRecordPage ClickPriorityRemoveButton()
        {
            Click(priority_RemoveButton);

            return this;
        }
        public PersonTaskRecordPage ClickPriorityLookupButton()
        {
            Click(priority_LookupButton);

            return this;
        }
        public PersonTaskRecordPage ClickContainsInformationProvidedByThirdPartyYesRadioButton()
        {
            Click(ContainsInformationProvidedByThirdParty_YesRadioButton);

            return this;
        }
        public PersonTaskRecordPage ClickContainsInformationProvidedByThirdPartyNoRadioButton()
        {
            Click(ContainsInformationProvidedByThirdParty_NoRadioButton);

            return this;
        }
        public PersonTaskRecordPage ClickResponsibleTeamRemoveButton()
        {
            Click(responsibleTeam_RemoveButton);

            return this;
        }
        public PersonTaskRecordPage ClickResponsibleTeamLookupButton()
        {
            Click(responsibleTeam_LookupButton);

            return this;
        }
        public PersonTaskRecordPage ClickResponsibleUserRemoveButton()
        {
            ScrollToElement(responsibleUser_RemoveButton);
            Click(responsibleUser_RemoveButton);

            return this;
        }
        public PersonTaskRecordPage ClickResponsibleUserLookupButton()
        {
            Click(responsibleUser_LookupButton);

            return this;
        }
        public PersonTaskRecordPage ClickResponsibleUserLinkFieldText()
        {
            WaitForElementVisible(responsibleUser_LinkField);
            ScrollToElement(responsibleUser_LinkField);
            WaitForElementToBeClickable(responsibleUser_LinkField);
            Click(responsibleUser_LinkField);

            return this;
        }
        public PersonTaskRecordPage ClickCategoryRemoveButton()
        {
            Click(category_RemoveButton);

            return this;
        }
        public PersonTaskRecordPage ClickCategoryLookupButton()
        {
            Click(category_LookupButton);

            return this;
        }
        public PersonTaskRecordPage ClickSubCategoryRemoveButton()
        {
            Click(subcategory_RemoveButton);

            return this;
        }
        public PersonTaskRecordPage ClickSubCategoryLookupButton()
        {
            Click(subcategory_LookupButton);

            return this;
        }
        public PersonTaskRecordPage ClickOutcomeRemoveButton()
        {
            Click(outcome_RemoveButton);

            return this;
        }
        public PersonTaskRecordPage ClickOutcomeLookupButton()
        {
            Click(outcome_LookupButton);

            return this;
        }
        public PersonTaskRecordPage ClickIsCaseNoteYesRadioButton()
        {
            Click(isCaseNote_YesRadioButton);

            return this;
        }
        public PersonTaskRecordPage ClickIsCaseNoteNoRadioButton()
        {
            Click(isCaseNote_NoRadioButton);

            return this;
        }
        public PersonTaskRecordPage ClickSignificantEventYesRadioButton()
        {
            Click(significantEvent_YesRadioButton);

            return this;
        }
        public PersonTaskRecordPage ClickSignificantEventNoRadioButton()
        {
            Click(significantEvent_NoRadioButton);

            return this;
        }
        public PersonTaskRecordPage ClickEventCategoryRemoveButton()
        {
            Click(eventCategory_RemoveButton);

            return this;
        }
        public PersonTaskRecordPage ClickEventCategoryLookupButton()
        {
            Click(eventCategory_LookupButton);

            return this;
        }
        public PersonTaskRecordPage ClickEventSubCategoryRemoveButton()
        {
            Click(eventSubCategory_RemoveButton);

            return this;
        }
        public PersonTaskRecordPage ClickEventSubCategoryLookupButton()
        {
            Click(eventSubCategory_LookupButton);

            return this;
        }
        public PersonTaskRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }
        public PersonTaskRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);
            return this;
        }
        public PersonTaskRecordPage ClickCloneButton()
        {
            WaitForElementToBeClickable(additionalIttemsButton);
            Click(additionalIttemsButton);

            WaitForElementToBeClickable(cloneButton);
            Click(cloneButton);

            return this;
        }
        public PersonTaskRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(additionalIttemsButton);
            Click(additionalIttemsButton);

            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }
        public PersonTaskRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }
        public PersonTaskRecordPage ClickActivateButton()
        {
            WaitForElementToBeClickable(additionalIttemsButton);
            Click(additionalIttemsButton);

            WaitForElementToBeClickable(activateButton);
            Click(activateButton);

            return this;
        }
        public PersonTaskRecordPage ClickCompleteButton()
        {
            WaitForElementToBeClickable(additionalIttemsButton);
            Click(additionalIttemsButton);

            WaitForElementToBeClickable(completeButton);
            Click(completeButton);

            return this;
        }
        public PersonTaskRecordPage ClickCancelButton()
        {
            WaitForElementToBeClickable(additionalIttemsButton);
            Click(additionalIttemsButton);

            WaitForElementToBeClickable(cancelButton);
            Click(cancelButton);

            return this;
        }


        public PersonTaskRecordPage NavigateToAuditSubPage()
        {
            WaitForElementToBeClickable(LeftMenuButton);
            Click(LeftMenuButton);

            WaitForElementToBeClickable(AuditLink_LeftMenu);
            Click(AuditLink_LeftMenu);

            return this;
        }


        public PersonTaskRecordPage ValidateMessageAreaText(string ExpectedText)
        {
            ValidateElementText(messageArea, ExpectedText);

            return this;
        }
        public PersonTaskRecordPage ValidateSubjectFieldText(string ExpectedText)
        {
            ValidateElementValue(subject_Field, ExpectedText);

            return this;
        }
        public PersonTaskRecordPage ValidateDescriptionFieldText(string LineNumber, string ExpectedText)
        {
            ValidateElementText(description_Field(LineNumber), ExpectedText);

            return this;
        }
        public PersonTaskRecordPage ValidateRegardingLinkFieldText(string ExpectedText)
        {
            ValidateElementText(regarding_LinkField, ExpectedText);

            return this;
        }
        public PersonTaskRecordPage ValidateReasonLinkFieldText(string ExpectedText)
        {
            ValidateElementText(reason_LinkField, ExpectedText);

            return this;
        }
        public PersonTaskRecordPage ValidatePriorityLinkFieldText(string ExpectedText)
        {
            ValidateElementText(priority_LinkField, ExpectedText);

            return this;
        }
        public PersonTaskRecordPage ValidateDueDateText(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(due_DateField, ExpectedDate);
            ValidateElementValue(due_TimeField, ExpectedTime);

            return this;
        }
        public PersonTaskRecordPage ValidateSelectedStatus(string ExpectedText)
        {
            ValidatePicklistSelectedText(status_Field, ExpectedText);

            return this;
        }
        public PersonTaskRecordPage ValidateResponsibleTeamLinkFieldText(string ExpectedText)
        {
            ValidateElementText(responsibleTeam_LinkField, ExpectedText);

            return this;
        }
        public PersonTaskRecordPage ValidateResponsibleUserLinkFieldText(string ExpectedText)
        {
            ScrollToElement(responsibleUser_LinkField);
            if(!string.IsNullOrEmpty(ExpectedText))
                WaitForElementVisible(responsibleUser_LinkField);
            ValidateElementText(responsibleUser_LinkField, ExpectedText);

            return this;
        }
        public PersonTaskRecordPage ValidateCategoryLinkFieldText(string ExpectedText)
        {
            ValidateElementText(category_LinkField, ExpectedText);

            return this;
        }
        public PersonTaskRecordPage ValidateSubCategoryLinkFieldText(string ExpectedText)
        {
            ValidateElementText(subcategory_LinkField, ExpectedText);

            return this;
        }
        public PersonTaskRecordPage ValidateOutcomeLinkFieldText(string ExpectedText)
        {
            ValidateElementText(outcome_LinkField, ExpectedText);

            return this;
        }
        public PersonTaskRecordPage ValidateEventDateText(string ExpectedText)
        {
            ValidateElementValue(EventDate_Field, ExpectedText);

            return this;
        }
        public PersonTaskRecordPage ValidateEventCategoryLinkFieldText(string ExpectedText)
        {
            ValidateElementText(eventCategory_LinkField, ExpectedText);

            return this;
        }
        public PersonTaskRecordPage ValidateEventSubCategoryLinkFieldText(string ExpectedText)
        {
            ValidateElementText(eventSubCategory_LinkField, ExpectedText);

            return this;
        }
        public PersonTaskRecordPage ValidateSubjectErrorLabelText(string ExpectedText)
        {
            ValidateElementText(subject_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonTaskRecordPage ValidateStatusErrorLabelText(string ExpectedText)
        {
            ValidateElementText(status_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonTaskRecordPage ValidateResponsibleTeamErrorLabelText(string ExpectedText)
        {
            ValidateElementText(responsibleTeam_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonTaskRecordPage ValidateEventDateErrorLabelText(string ExpectedText)
        {
            ValidateElementText(EventDate_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonTaskRecordPage ValidateEventCategoryErrorLabelText(string ExpectedText)
        {
            ValidateElementText(eventCategory_FieldErrorLabel, ExpectedText);

            return this;
        }




        public PersonTaskRecordPage ValidateContainsInformationProvidedByThirdPartyYesOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(ContainsInformationProvidedByThirdParty_YesRadioButton);
            }
            else
            {
                ValidateElementNotChecked(ContainsInformationProvidedByThirdParty_YesRadioButton);
            }

            return this;
        }
        public PersonTaskRecordPage ValidateContainsInformationProvidedByThirdPartyNoOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(ContainsInformationProvidedByThirdParty_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(ContainsInformationProvidedByThirdParty_NoRadioButton);
            }

            return this;
        }
        public PersonTaskRecordPage ValidateIsCaseNoteYesOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(isCaseNote_YesRadioButton);
            }
            else
            {
                ValidateElementNotChecked(isCaseNote_YesRadioButton);
            }

            return this;
        }
        public PersonTaskRecordPage ValidateIsCaseNoteNoOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(isCaseNote_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(isCaseNote_NoRadioButton);
            }

            return this;
        }
        public PersonTaskRecordPage ValidateSignificantEventYesOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(significantEvent_YesRadioButton);
            }
            else
            {
                ValidateElementNotChecked(significantEvent_YesRadioButton);
            }

            return this;
        }
        public PersonTaskRecordPage ValidateSignificantEventNoOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(significantEvent_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(significantEvent_NoRadioButton);
            }

            return this;
        }

        public PersonTaskRecordPage ValidateIsClonedYesOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(isCloned_YesRadioButton);
            }
            else
            {
                ValidateElementNotChecked(isCloned_YesRadioButton);
            }

            return this;
        }
        public PersonTaskRecordPage ValidateIsClonedNoOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(isCloned_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(isCloned_NoRadioButton);
            }

            return this;
        }
    }
}

using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonCaseNoteRecordPage : CommonMethods
    {

        public PersonCaseNoteRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personcasenote')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");


        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By cloneButton = By.Id("TI_CloneRecordButton");
        readonly By additionalIttemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");


        #region Field Title

        readonly By ContainsInformationProvidedByThirdParty_FieldTitle = By.XPath("//*[@id='CWLabelHolder_informationbythirdparty']/label");


        #endregion



        #region Fields

        readonly By subject_Field = By.XPath("//*[@id='CWField_subject']");
        readonly By subject_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_subject']/label/span");

        readonly By spellCheckButton_NotesField = By.XPath("//*[@id='cke_63']/span[1]");


        readonly By messageArea = By.XPath("//*[@id='CWNotificationMessage_DataForm']");
        readonly By responsibleTeam_LinkField = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By responsibleTeam_RemoveButton = By.XPath("//*[@id='CWClearLookup_ownerid']");
        readonly By responsibleTeam_LookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By person_LinkfieldId = By.Id("CWField_personid_Link");

        readonly By responsibleUser_LinkField = By.XPath("//*[@id='CWField_responsibleuserid_Link']");
        readonly By responsibleUser_RemoveButton = By.XPath("//*[@id='CWClearLookup_responsibleuserid']");
        readonly By responsibleUser_LookupButton = By.XPath("//*[@id='CWLookupBtn_responsibleuserid']");
        readonly By responsibleTeam_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_ownerid']/label/span");

        readonly By containsInformationProvidedByThirdParty_YesRadioButton = By.XPath("//*[@id='CWField_informationbythirdparty_1']");
        readonly By containsInformationProvidedByThirdParty_NoRadioButton = By.XPath("//*[@id='CWField_informationbythirdparty_0']");
        readonly By description_Field = By.XPath("//*[@id='CWField_notes']");


        readonly By description_TextAreaField = By.XPath("//*[@id='CWField_notes']");
        readonly string description_TextAreaFieldName = "CWField_notes";
        By descriptions_Field(string LineNumber) => By.XPath("/html/body/p[" + LineNumber + "]");

        readonly By richTextBoxIframe = By.XPath("//iframe[@title='Rich Text Editor, CWField_notes']");
        readonly By date_DateField = By.XPath("//*[@id='CWField_casenotedate']");
        readonly By date_TimeField = By.XPath("//*[@id='CWField_casenotedate_Time']");
        readonly By eventDate_Field = By.XPath("//*[@id='CWField_significanteventdate']");
        readonly By eventCategory_LinkField = By.XPath("//*[@id='CWField_significanteventcategoryid_Link']");
        readonly By eventSubCategory_LinkField = By.XPath("//*[@id='CWField_significanteventsubcategoryid_Link']");

        readonly By due_DateField = By.Id("CWField_casenotedate");
        readonly By due_TimeField = By.Id("CWField_casenotedate_Time");
        readonly By eventDate_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_significanteventdate']/label/span");
        readonly By status_Field = By.XPath("//*[@id='CWField_statusid']");
        readonly By status_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_statusid']/label/span");
        readonly By reason_LinkField = By.XPath("//*[@id='CWField_activityreasonid_Link']");
        readonly By reason_LookUpButton = By.XPath("//*[@id='CWLookupBtn_activityreasonid']");
        readonly By reason_RemoveButton = By.XPath("//*[@id='CWClearLookup_activityreasonid']");
        readonly By regarding_LinkField = By.Id("CWField_responsibleuserid_Link");
        readonly By priority_LinkField = By.XPath("//*[@id='CWField_activitypriorityid_Link']");
        readonly By priority_LookUpButton = By.XPath("//*[@id='CWLookupBtn_activitypriorityid']");
        readonly By priority_RemoveButton = By.XPath("//*[@id='CWClearLookup_activitypriorityid']");
        readonly By category_LinkField = By.XPath("//*[@id='CWField_activitycategoryid_Link']");
        readonly By category_LookUpButton = By.XPath("//*[@id='CWLookupBtn_activitycategoryid']");
        readonly By category_RemoveButton = By.XPath("//*[@id='CWClearLookup_activitycategoryid']");
        readonly By eventCategory_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_significanteventcategoryid']/label/span");
        readonly By eventCategory_RemoveButton = By.XPath("//*[@id='CWClearLookup_significanteventcategoryid']");
        readonly By eventCategory_LookupButton = By.XPath("//*[@id='CWLookupBtn_significanteventcategoryid']");
        readonly By subCategoryLinkField = By.XPath("//*[@id='CWField_activitysubcategoryid_cwname']");
        readonly By subCategory_LookUpButton = By.XPath("//*[@id='CWLookupBtn_activitysubcategoryid']");
        readonly By subcategory_LinkField = By.XPath("//*[@id='CWField_activitysubcategoryid_Link']");
        readonly By eventSubCategory_LookupButton = By.XPath("//*[@id='CWLookupBtn_significanteventsubcategoryid']");
        readonly By outcome_LinkField = By.XPath("//*[@id='CWField_activityoutcomeid_Link']");
        readonly By outCome_LookUpButton = By.XPath("//*[@id='CWLookupBtn_activityoutcomeid']");
        readonly By outcome_RemoveButton = By.XPath("//*[@id='CWClearLookup_activityoutcomeid']");

        readonly By significantEvent_YesRadioButton = By.XPath("//*[@id='CWField_issignificantevent_1']");
        readonly By significantEvent_NoRadioButton = By.XPath("//*[@id='CWField_issignificantevent_0']");
        readonly By significantEvent_Category_LinkField = By.XPath("//*[@id='CWField_significanteventcategoryid_cwname']");
        readonly By significantEvent_Category_LookUpButton = By.XPath("//*[@id='CWLookupBtn_significanteventcategoryid']");
        readonly By significantEvent_SubCategory_LinkField = By.XPath("//*[@id='CWField_significanteventsubcategoryid_cwname']");
        readonly By significantEvent_SubCategory_LookUpButton = By.XPath("//*[@id='CWLookupBtn_significanteventsubcategoryid']");
        readonly By significantEvent_Date_LinkField = By.XPath("//*[@id='CWField_significanteventdate']");
        readonly By significantEvent_Date_Calender = By.XPath("//*[@id='CWField_casenotedate_DatePicker']");


        readonly By isCloned_YesRadioButton = By.XPath("//*[@id='CWField_iscloned_1']");
        readonly By isCloned_NoRadioButton = By.XPath("//*[@id='CWField_iscloned_0']");
        readonly By clonedFrom_LinkField = By.XPath("//*[@id='CWField_clonedfromid_Link']");
        readonly By clonedFrom_LookupButton = By.XPath("//*[@id='CWLookupBtn_clonedfromid']");
        readonly By isCaseNote_YesRadioButton = By.XPath("//*[@id='CWField_iscloned_1']");
        readonly By isCaseNote_NoRadioButton = By.XPath("//*[@id='CWField_iscloned_0']");

        readonly By additionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By cancelButton = By.Id("TI_CancelRecordButton");
        readonly By leftMenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By auditLink_LeftMenu = By.XPath("//*[@id='CWNavItem_AuditHistory']");


        #endregion

        public PersonCaseNoteRecordPage WaitForPersonCaseNoteRecordPageToLoad(string CaseNoteTitle)
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);


            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            ValidateElementText(pageHeader, "Person Case Note:\r\n" + CaseNoteTitle);

            return this;
        }


        public PersonCaseNoteRecordPage ValidateContainsInformationProvidedByThirdPartyFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ContainsInformationProvidedByThirdParty_FieldTitle);
                WaitForElementVisible(containsInformationProvidedByThirdParty_YesRadioButton);
                WaitForElementVisible(containsInformationProvidedByThirdParty_NoRadioButton);
            }
            else
            {
                WaitForElementNotVisible(ContainsInformationProvidedByThirdParty_FieldTitle, 3);
                WaitForElementNotVisible(containsInformationProvidedByThirdParty_YesRadioButton, 3);
                WaitForElementNotVisible(containsInformationProvidedByThirdParty_NoRadioButton, 3);
            }

            return this;
        }


        public PersonCaseNoteRecordPage ClickNotesFieldSpellCheckButton()
        {
            WaitForElement(spellCheckButton_NotesField);
            Click(spellCheckButton_NotesField);

            return this;
        }

        public PersonCaseNoteRecordPage InsertSubject(string TextToInsert)
        {
            SendKeys(subject_Field, TextToInsert);

            return this;
        }

        public PersonCaseNoteRecordPage ClickResponsibleTeamRemoveButton()
        {
            Click(responsibleTeam_RemoveButton);

            return this;
        }

        public PersonCaseNoteRecordPage ClickResponsibleTeamLookupButton()
        {
            Click(responsibleTeam_LookupButton);

            return this;
        }

        public PersonCaseNoteRecordPage ClickResponsibleUserRemoveButton()
        {
            Click(responsibleUser_RemoveButton);

            return this;
        }

        public PersonCaseNoteRecordPage ClickResponsibleUserLookupButton()
        {
            WaitForElementToBeClickable(responsibleUser_LookupButton);
            Click(responsibleUser_LookupButton);

            return this;
        }

        public PersonCaseNoteRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }

        public PersonCaseNoteRecordPage ClickCloneButton()
        {
            WaitForElementToBeClickable(cloneButton);
            Click(cloneButton);

            return this;
        }

        public PersonCaseNoteRecordPage ClickBackButton()
        {
            WaitForElement(backButton);
            Click(backButton);

            return this;
        }

        public PersonCaseNoteRecordPage ValidateResponsibleTeamLinkFieldText(string ExpectedText)
        {
            ValidateElementText(responsibleTeam_LinkField, ExpectedText);

            return this;
        }

        public PersonCaseNoteRecordPage InsertDescription(string Subject)
        {
            SetElementDisplayStyleToInline("CWField_notes");
            SetElementVisibilityStyleToVisible("CWField_notes");

            SendKeys(description_Field, Subject);

            return this;
        }

        public PersonCaseNoteRecordPage InsertTextDescription(string TextToInsert)
        {
            System.Threading.Thread.Sleep(1000);
            SetElementDisplayStyleToInline(description_TextAreaFieldName);
            SetElementVisibilityStyleToVisible(description_TextAreaFieldName);

            WaitForElementVisible(description_TextAreaField);
            SendKeys(description_TextAreaField, TextToInsert);

            return this;
        }

        public PersonCaseNoteRecordPage InsertDate(string Date, string Time)
        {
            SendKeys(date_DateField, Date);
            SendKeysWithoutClearing(date_DateField, Keys.Tab);
            SendKeys(date_TimeField, Time);

            return this;
        }

        public PersonCaseNoteRecordPage SelectStatus(string TextToSelect)
        {
            SelectPicklistElementByText(status_Field, TextToSelect);

            return this;
        }

        public PersonCaseNoteRecordPage ValidateSubject(string ExpectedText)
        {
            ValidateElementValue(subject_Field, ExpectedText);

            return this;
        }
        public PersonCaseNoteRecordPage ValidateDescription(string ExpectedText)
        {
            SetElementDisplayStyleToInline("CWField_notes");
            SetElementVisibilityStyleToVisible("CWField_notes");

            ValidateElementValue(description_Field, ExpectedText);

            return this;
        }


        public PersonCaseNoteRecordPage ValidateDate(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(date_DateField, ExpectedDate);
            ValidateElementValue(date_TimeField, ExpectedTime);

            return this;
        }
        public PersonCaseNoteRecordPage ValidateStatus(string ExpectedText)
        {
            ValidatePicklistSelectedText(status_Field, ExpectedText);

            return this;
        }

        public PersonCaseNoteRecordPage ClickReasonLookupButton()
        {
            Click(reason_LookUpButton);

            return this;
        }

        public PersonCaseNoteRecordPage ClickPriorityLookupButton()
        {
            Click(priority_LookUpButton);

            return this;
        }


        public PersonCaseNoteRecordPage ClickContainsInformationProvidedByAThirdParty_YesRadioButton()

        {
            Click(containsInformationProvidedByThirdParty_YesRadioButton);

            return this;
        }


        public PersonCaseNoteRecordPage ClickCategoryLookupButton()
        {
            Click(category_LookUpButton);

            return this;
        }
        public PersonCaseNoteRecordPage ClickSubCategoryLookupButton()
        {
            Click(subCategory_LookUpButton);

            return this;
        }

        public PersonCaseNoteRecordPage ClickOutcomeLookupButton()
        {
            Click(outCome_LookUpButton);

            return this;
        }

        public PersonCaseNoteRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public PersonCaseNoteRecordPage ValidateSignificantEventYesOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                WaitForElement(significantEvent_YesRadioButton);
                ValidateElementChecked(significantEvent_YesRadioButton);
            }
            else
            { 
                WaitForElement(significantEvent_YesRadioButton);
                ValidateElementNotChecked(significantEvent_YesRadioButton);
            }

            return this;
        }


        public PersonCaseNoteRecordPage ValidateSignificantEventNoOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
                ValidateElementChecked(significantEvent_NoRadioButton);
            else
                ValidateElementNotChecked(significantEvent_NoRadioButton);

            return this;
        }

        public PersonCaseNoteRecordPage ClickSignificantEventCategoriesLookUp()
        {
            Click(significantEvent_Category_LookUpButton);

            return this;
        }


        public PersonCaseNoteRecordPage ClickSignificantEventSubCategoriesLookUp()
        {
            Click(significantEvent_SubCategory_LookUpButton);

            return this;
        }


        public PersonCaseNoteRecordPage InsertSignificantEventDate(string Date)
        {
            SendKeys(date_DateField, Date);


            return this;
        }
        public PersonCaseNoteRecordPage SelectSignificantEventCategories(string TextToSelect)
        {
            SelectPicklistElementByText(status_Field, TextToSelect);

            return this;
        }

        public PersonCaseNoteRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(additionalIttemsButton);
            Click(additionalIttemsButton);

            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }


        public PersonCaseNoteRecordPage ClickSignificantEventYesRadioButton()
        {
            Click(significantEvent_YesRadioButton);

            return this;
        }
        public PersonCaseNoteRecordPage ClickSignificantEventNoRadioButton()
        {
            Click(significantEvent_NoRadioButton);

            return this;
        }

        public PersonCaseNoteRecordPage ValidateMessageArea(bool ExpectVisible)
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


        public PersonCaseNoteRecordPage ValidateMessageAreaText(string ExpectedText)
        {
            ValidateElementText(messageArea, ExpectedText);

            return this;
        }


        public PersonCaseNoteRecordPage ValidateSubjectErrorLabelVisible(bool ExpectVisible)
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

        public PersonCaseNoteRecordPage ValidateSubjectErrorLabelText(string ExpectedText)
        {
            ValidateElementText(subject_FieldErrorLabel, ExpectedText);

            return this;
        }

        public PersonCaseNoteRecordPage ValidateStatusErrorLabelText(string ExpectedText)
        {
            ValidateElementText(status_FieldErrorLabel, ExpectedText);

            return this;
        }


        public PersonCaseNoteRecordPage ValidateStatusErrorLabelVisible(bool ExpectVisible)
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

        public PersonCaseNoteRecordPage ValidateResponsibleTeamErrorLabelVisible(bool ExpectVisible)
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

        public PersonCaseNoteRecordPage ValidateResponsibleTeamErrorLabelText(string ExpectedText)
        {
            ValidateElementText(status_FieldErrorLabel, ExpectedText);

            return this;
        }

        public PersonCaseNoteRecordPage ValidateEventDateErrorLabelVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(eventDate_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(eventDate_FieldErrorLabel, 3);
            }

            return this;
        }

        public PersonCaseNoteRecordPage ValidateEventDateErrorLabelText(string ExpectedText)
        {
            ValidateElementText(eventDate_FieldErrorLabel, ExpectedText);

            return this;
        }

        public PersonCaseNoteRecordPage ValidateEventCategoryErrorLabelVisible(bool ExpectVisible)
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

        public PersonCaseNoteRecordPage ValidateEventCategoryErrorLabelText(string ExpectedText)
        {
            ValidateElementText(eventCategory_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonCaseNoteRecordPage ValidateSubjectFieldText(string ExpectedText)
        {
            ValidateElementValue(subject_Field, ExpectedText);

            return this;
        }
        public PersonCaseNoteRecordPage InsertEventDate(string DateToInsert)
        {
            SendKeys(eventDate_Field, DateToInsert);

            return this;
        }

        public PersonCaseNoteRecordPage ClickEventCategoryRemoveButton()
        {
            Click(eventCategory_RemoveButton);

            return this;
        }

        public PersonCaseNoteRecordPage ValidateMessageAreaVisible(bool ExpectVisible)
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
        public PersonCaseNoteRecordPage InsertDueDate(string DateToInsert, string TimeToInsert)
        {
            SendKeys(due_DateField, DateToInsert);
            SendKeysWithoutClearing(due_DateField, Keys.Tab);
            SendKeys(due_TimeField, TimeToInsert);

            return this;
        }

        public PersonCaseNoteRecordPage ClickContainsInformationProvidedByThirdPartyYesRadioButton()
        {
            Click(containsInformationProvidedByThirdParty_YesRadioButton);

            return this;
        }
        public PersonCaseNoteRecordPage ClickIsCaseNoteYesRadioButton()
        {
            Click(isCaseNote_YesRadioButton);

            return this;
        }
        public PersonCaseNoteRecordPage ClickEventCategoryLookupButton()
        {
            Click(eventCategory_LookupButton);

            return this;
        }

        public PersonCaseNoteRecordPage ClickEventSubCategoryLookupButton()
        {
            Click(eventSubCategory_LookupButton);

            return this;
        }
        public PersonCaseNoteRecordPage LoadDescriptionRichTextBox()
        {
            WaitForElement(richTextBoxIframe);
            SwitchToIframe(richTextBoxIframe);

            return this;
        }
        public PersonCaseNoteRecordPage ValidateDescriptionFieldText(string LineNumber, string ExpectedText)
        {
            ValidateElementText(descriptions_Field(LineNumber), ExpectedText);

            return this;
        }

        public PersonCaseNoteRecordPage ValidateRegardingLinkFieldText(string ExpectedText)
        {
            ValidateElementText(regarding_LinkField, ExpectedText);

            return this;
        }
        public PersonCaseNoteRecordPage ValidateReasonLinkFieldText(string ExpectedText)
        {
            ValidateElementText(reason_LinkField, ExpectedText);

            return this;
        }
        public PersonCaseNoteRecordPage ValidatePriorityLinkFieldText(string ExpectedText)
        {
            ValidateElementText(priority_LinkField, ExpectedText);

            return this;
        }
        public PersonCaseNoteRecordPage ValidateDueDateText(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(due_DateField, ExpectedDate);
            ValidateElementValue(due_TimeField, ExpectedTime);

            return this;
        }
        public PersonCaseNoteRecordPage ValidateSelectedStatus(string ExpectedText)
        {
            ValidatePicklistSelectedText(status_Field, ExpectedText);

            return this;
        }
        public PersonCaseNoteRecordPage ValidateContainsInformationProvidedByThirdPartyYesOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(containsInformationProvidedByThirdParty_YesRadioButton);
            }
            else
            {
                ValidateElementNotChecked(containsInformationProvidedByThirdParty_YesRadioButton);
            }

            return this;
        }
        public PersonCaseNoteRecordPage ValidateContainsInformationProvidedByThirdPartyNoOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(containsInformationProvidedByThirdParty_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(containsInformationProvidedByThirdParty_NoRadioButton);
            }

            return this;
        }
        public PersonCaseNoteRecordPage ValidateResponsibleUserLinkFieldText(string ExpectedText)
        {
            ValidateElementText(responsibleUser_LinkField, ExpectedText);

            return this;
        }
        public PersonCaseNoteRecordPage ValidateCategoryLinkFieldText(string ExpectedText)
        {
            ValidateElementText(category_LinkField, ExpectedText);

            return this;
        }
        public PersonCaseNoteRecordPage ValidateSubCategoryLinkFieldText(string ExpectedText)
        {
            ValidateElementText(subcategory_LinkField, ExpectedText);

            return this;
        }
        public PersonCaseNoteRecordPage ValidateOutcomeLinkFieldText(string ExpectedText)
        {
            ValidateElementText(outcome_LinkField, ExpectedText);

            return this;
        }
        public PersonCaseNoteRecordPage ValidateIsCaseNoteYesOptionChecked(bool ExpectChecked)
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
        public PersonCaseNoteRecordPage ValidateIsCaseNoteNoOptionChecked(bool ExpectChecked)
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
        public PersonCaseNoteRecordPage ValidateEventDateText(string ExpectedText)
        {
            ValidateElementValue(eventDate_Field, ExpectedText);

            return this;
        }
        public PersonCaseNoteRecordPage ValidateEventCategoryLinkFieldText(string ExpectedText)
        {
            ValidateElementText(eventCategory_LinkField, ExpectedText);

            return this;
        }
        public PersonCaseNoteRecordPage ValidateEventSubCategoryLinkFieldText(string ExpectedText)
        {
            ValidateElementText(eventSubCategory_LinkField, ExpectedText);

            return this;
        }
        public PersonCaseNoteRecordPage ValidateIsClonedYesOptionChecked(bool ExpectChecked)
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
        public PersonCaseNoteRecordPage ValidateIsClonedNoOptionChecked(bool ExpectChecked)
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
        public PersonCaseNoteRecordPage ValidatePersonLinkFieldText(string ExpectedText)
        {
            ValidateElementText(person_LinkfieldId, ExpectedText);

            return this;
        }
        public PersonCaseNoteRecordPage ClickReasonRemoveButton()
        {
            Click(reason_RemoveButton);

            return this;
        }
        public PersonCaseNoteRecordPage ClickPriorityRemoveButton()
        {
            Click(priority_RemoveButton);

            return this;
        }
        public PersonCaseNoteRecordPage ClickCategoryRemoveButton()
        {
            Click(category_RemoveButton);

            return this;
        }
        public PersonCaseNoteRecordPage ClickOutcomeRemoveButton()
        {
            Click(outcome_RemoveButton);

            return this;
        }
        public PersonCaseNoteRecordPage ClickCancelButton()
        {

            Click(additionalItemsButton);
            Click(cancelButton);

            return this;
        }
        public PersonCaseNoteRecordPage NavigateToAuditSubPage()
        {
            WaitForElementToBeClickable(leftMenuButton);
            Click(leftMenuButton);

            WaitForElementToBeClickable(auditLink_LeftMenu);
            Click(auditLink_LeftMenu);

            return this;
        }
    }


}


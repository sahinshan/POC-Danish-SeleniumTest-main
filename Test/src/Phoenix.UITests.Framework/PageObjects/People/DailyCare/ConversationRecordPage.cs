using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class ConversationRecordPage : CommonMethods
    {
        public ConversationRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Options Toolbar

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personconversations&')]");
        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Conversation: ']");

        #endregion

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
        readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
        readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");
		readonly By PersonidLink = By.XPath("//*[@id='CWField_personid_Link']");
        readonly By PersonidLookupButton = By.XPath("//*[@id='CWLookupBtn_personid']");
        readonly string preferences_Id = "CWField_preferences";
        readonly By Preferences = By.XPath("//*[@id='CWField_preferences']");
        readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By Occurred = By.XPath("//*[@id='CWField_occurred']");
        readonly By OccurredDatePicker = By.XPath("//*[@id='CWField_occurred_DatePicker']");
        readonly By Occurred_Time = By.XPath("//*[@id='CWField_occurred_Time']");
        readonly By Occurred_Time_TimePicker = By.XPath("//*[@id='CWField_occurred_Time_TimePicker']");
        readonly By Createdon = By.XPath("//*[@id='CWField_createdon']");
        readonly By CreatedonDatePicker = By.XPath("//*[@id='CWField_createdon_DatePicker']");
        readonly By Createdon_Time = By.XPath("//*[@id='CWField_createdon_Time']");
        readonly By Createdon_Time_TimePicker = By.XPath("//*[@id='CWField_createdon_Time_TimePicker']");
        readonly By NotesTextareaField = By.XPath("//*[@id='CWField_notes']");
		readonly By LocationLookupButton = By.XPath("//*[@id='CWLookupBtn_carephysicallocations']");
        readonly By LocationIfOtherTextareaField = By.Id("CWField_locationifother");
        By location_SelectedElementLink(string ElementId) => By.XPath("//*[@id='MS_carephysicallocations_" + ElementId + "']/a[@id='" + ElementId + "_Link']");
        By location_SelectedElementRemoveButton(string ElementId) => By.XPath("//*[@id='MS_carephysicallocations_" + ElementId + "']/a[text()='Remove']");
        readonly By WellbeingidLink = By.XPath("//*[@id='CWField_carewellbeingid_Link']");
        readonly By WellbeingidClearButton = By.XPath("//*[@id='CWClearLookup_carewellbeingid']");
        readonly By WellbeingidLookupButton = By.XPath("//*[@id='CWLookupBtn_carewellbeingid']");
        readonly By Wellbeing_actiontaken = By.XPath("//*[@id='CWField_actiontaken']");
        readonly By Totaltimespentwithclientminutes = By.XPath("//*[@id='CWField_timespentwithclient']");
        readonly By TotalTimeSpentWithClientMinutesFieldError = By.XPath("//label[@for = 'CWField_timespentwithclient'][@class = 'formerror']/span");
        readonly By Additionalnotes = By.XPath("//*[@id='CWField_additionalnotes']");
        readonly By AssistanceneededidLink = By.XPath("//*[@id='CWField_careassistanceneededid_Link']");
        readonly By AssistanceneededidClearButton = By.XPath("//*[@id='CWClearLookup_careassistanceneededid']");
        readonly By AssistanceneededidLookupButton = By.XPath("//*[@id='CWLookupBtn_careassistanceneededid']");
        By StaffRequired_SelectedOption(string optionId) => By.XPath("//*[@id='MS_staffrequired_" + optionId + "']");
        readonly By StaffrequiredLookupButton = By.XPath("//*[@id='CWLookupBtn_staffrequired']");
        readonly By AssistanceAmountPicklist = By.Id("CWField_careassistancelevelid");
        readonly String CarenoteFieldId = "CWField_carenote";
        readonly By Carenote = By.XPath("//*[@id='CWField_carenote']");
        readonly By LinkedadlcategoriesidLookupButton = By.XPath("//*[@id='CWLookupBtn_linkedadlcategoriesid']");
        By LinkedAdlCategories_SelectedElement(string OptionId) => By.XPath("//*[@id='MS_linkedadlcategoriesid_" + OptionId + "']/a[@id='" + OptionId + "_Link']");
        By LinkedAdlCategories_SelectedElementOnEdit(string OptionId) => By.XPath("//*[@id='MS_linkedadlcategoriesid_" + OptionId + "']");
        readonly By Isincludeinnexthandover_1 = By.XPath("//*[@id='CWField_isincludeinnexthandover_1']");
        readonly By Isincludeinnexthandover_0 = By.XPath("//*[@id='CWField_isincludeinnexthandover_0']");
        readonly By Flagrecordforhandover_1 = By.XPath("//*[@id='CWField_flagrecordforhandover_1']");
        readonly By Flagrecordforhandover_0 = By.XPath("//*[@id='CWField_flagrecordforhandover_0']");


        #region Field Labels

        By FieldLabel(string FieldName) => By.XPath("//*[starts-with(@id, 'CWLabelHolder_')]/*[text() = '" + FieldName + "']");
        By MandatoryField_Label(string FieldName) => By.XPath("//label[text()='" + FieldName + "']/span[@class='mandatory']");

        #endregion

        #region Section postion

        By SectionNameByPosition(int Position) => By.XPath("//*[@id = 'CWInputForm']//div["+Position+"]//*[@class = 'card-header']/*");

        #endregion

        #region Drawer mode

        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'mcc-iframe')][contains(@src,'type=personconversations')]");

        readonly By popupHeader = By.XPath("//*[@class='mcc-drawer__header']//h2");

        readonly By closeIcon = By.XPath("//button[@class='mcc-button mcc-button--sm mcc-button--ghost mcc-button--icon-only btn-close-drawer']");
        readonly By expandIcon = By.XPath("//button[@class='mcc-button mcc-button--sm mcc-button--ghost mcc-button--icon-only btn-full-screen-drawer']");
        readonly By CloseDrawerButton = By.Id("CWCloseDrawerButton");

        #endregion

        public ConversationRecordPage WaitForPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(cwDialogIFrame);
            SwitchToIframe(cwDialogIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(pageHeader);

            return this;
        }

        public ConversationRecordPage WaitForRecordToLoadInDrawerMode()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElementVisible(popupHeader);
            WaitForElementVisible(closeIcon);
            WaitForElementVisible(expandIcon);
            WaitForElementVisible(CloseDrawerButton);

            return this;
        }


        //verify pageHeader text
        public ConversationRecordPage VerifyPageHeaderText(string ExpectedText)
        {
            WaitForElementVisible(pageHeader);
            string pageTitle = GetElementByAttributeValue(pageHeader, "title");
            Assert.AreEqual("Distressed Behaviour: " + ExpectedText, pageTitle);

            return this;
        }

        public ConversationRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public ConversationRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public ConversationRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        //Click CloseDrawerButton
        public ConversationRecordPage ClickCloseDrawerButton()
        {
            WaitForElementToBeClickable(CloseDrawerButton);
            Click(CloseDrawerButton);

            return this;
        }

        //Click closeIcon
        public ConversationRecordPage ClickCloseIcon()
        {
            WaitForElementToBeClickable(closeIcon);
            Click(closeIcon);

            return this;
        }

        //Click expandIcon
        public ConversationRecordPage ClickExpandIcon()
        {
            WaitForElementToBeClickable(expandIcon);
            Click(expandIcon);

            return this;
        }

        public ConversationRecordPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public ConversationRecordPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }

        public ConversationRecordPage ClickPersonLink()
        {
            WaitForElementToBeClickable(PersonidLink);
            Click(PersonidLink);

            return this;
        }

        public ConversationRecordPage ValidatePersonLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(PersonidLink);
            ValidateElementText(PersonidLink, ExpectedText);

            return this;
        }

        public ConversationRecordPage ClickPersonLookupButton()
        {
            WaitForElementToBeClickable(PersonidLookupButton);
            Click(PersonidLookupButton);

            return this;
        }

        public ConversationRecordPage ValidatePreferencesText(string ExpectedText)
        {
            WaitForElement(Preferences);
            ScrollToElement(Preferences);
            ValidateElementValueByJavascript(preferences_Id, ExpectedText);

            return this;
        }

        //veify Preferences field is disabled or not disabled
        public ConversationRecordPage ValidatePreferencesFieldIsDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(Preferences);
            else
                ValidateElementNotDisabled(Preferences);

            return this;
        }

        public ConversationRecordPage ClickResponsibleTeamLink()
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            Click(ResponsibleTeamLink);

            return this;
        }

        public ConversationRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            ValidateElementText(ResponsibleTeamLink, ExpectedText);

            return this;
        }

        public ConversationRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamLookupButton);
            Click(ResponsibleTeamLookupButton);

            return this;
        }

        public ConversationRecordPage ValidateDateAndTimeOccurred_DateText(string ExpectedText)
        {
            ValidateElementValue(Occurred, ExpectedText);

            return this;
        }

        public ConversationRecordPage InsertTextOnOccurred(string TextToInsert)
        {
            WaitForElementToBeClickable(Occurred);
            SendKeys(Occurred, TextToInsert + Keys.Tab);

            return this;
        }

        public ConversationRecordPage ClickOccurredDatePicker()
        {
            WaitForElementToBeClickable(OccurredDatePicker);
            Click(OccurredDatePicker);

            return this;
        }

        public ConversationRecordPage ValidateDateAndTimeOccurred_TimeText(string ExpectedText)
        {
            ValidateElementValue(Occurred_Time, ExpectedText);

            return this;
        }

        public ConversationRecordPage InsertTextOnOccurred_Time(string TextToInsert)
        {
            WaitForElementToBeClickable(Occurred_Time);
            SendKeys(Occurred_Time, TextToInsert + Keys.Tab);

            return this;
        }

        public ConversationRecordPage ClickOccurred_Time_TimePicker()
        {
            WaitForElementToBeClickable(Occurred_Time_TimePicker);
            Click(Occurred_Time_TimePicker);

            return this;
        }

        public ConversationRecordPage SetDateAndTimeOccurred(string DateToInsert, string TimeToInsert)
        {
            WaitForElement(Occurred);
            WaitForElement(Occurred_Time);

            SendKeys(Occurred, DateToInsert + Keys.Tab);
            SendKeys(Occurred_Time, TimeToInsert + Keys.Tab);

            return this;
        }

        public ConversationRecordPage ValidateCreatedonText(string ExpectedText)
        {
            ValidateElementValue(Createdon, ExpectedText);

            return this;
        }

        public ConversationRecordPage InsertTextOnCreatedon(string TextToInsert)
        {
            WaitForElementToBeClickable(Createdon);
            SendKeys(Createdon, TextToInsert + Keys.Tab);

            return this;
        }

        public ConversationRecordPage ClickCreatedonDatePicker()
        {
            WaitForElementToBeClickable(CreatedonDatePicker);
            Click(CreatedonDatePicker);

            return this;
        }

        public ConversationRecordPage ValidateCreatedon_TimeText(string ExpectedText)
        {
            ValidateElementValue(Createdon_Time, ExpectedText);

            return this;
        }

        public ConversationRecordPage InsertTextOnCreatedon_Time(string TextToInsert)
        {
            WaitForElementToBeClickable(Createdon_Time);
            SendKeys(Createdon_Time, TextToInsert + Keys.Tab);

            return this;
        }

        public ConversationRecordPage ClickCreatedon_Time_TimePicker()
        {
            WaitForElementToBeClickable(Createdon_Time_TimePicker);
            Click(Createdon_Time_TimePicker);

            return this;
        }

        public ConversationRecordPage ValidateNotesText(string ExpectedText)
        {
            ValidateElementText(NotesTextareaField, ExpectedText);

            return this;
        }

        public ConversationRecordPage InsertTextOnNotesTextarea(string TextToInsert)
        {
            WaitForElementToBeClickable(NotesTextareaField);
            SendKeys(NotesTextareaField, TextToInsert + Keys.Tab);

            return this;
        }


        public ConversationRecordPage ClickLocationLookupButton()
        {
            WaitForElementToBeClickable(LocationLookupButton);
            Click(LocationLookupButton);

            return this;
        }

        public ConversationRecordPage ClickLocation_SelectedElementLink(string ElementId)
        {
            WaitForElementToBeClickable(location_SelectedElementLink(ElementId));
            Click(location_SelectedElementLink(ElementId));

            return this;
        }

        public ConversationRecordPage ValidateLocation_SelectedElementLinkText(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(location_SelectedElementLink(ElementId));
            ValidateElementText(location_SelectedElementLink(ElementId), ExpectedText);

            return this;
        }

        public ConversationRecordPage ValidateLocation_SelectedElementLinkText(Guid ElementId, string ExpectedText)
        {
            return ValidateLocation_SelectedElementLinkText(ElementId.ToString(), ExpectedText);
        }

        public ConversationRecordPage ClickLocation_SelectedElementRemoveButton(string ElementId)
        {
            WaitForElementToBeClickable(location_SelectedElementRemoveButton(ElementId));
            Click(location_SelectedElementRemoveButton(ElementId));

            return this;
        }

        //Insert text on LocationIfOtherTextareaField 
        public ConversationRecordPage InsertTextOnLocationIfOtherTextareaField(string TextToInsert)
        {
            WaitForElement(LocationIfOtherTextareaField);
            ScrollToElement(LocationIfOtherTextareaField);
            SendKeys(LocationIfOtherTextareaField, TextToInsert + Keys.Tab);

            return this;
        }

        //verify text in LocationIfOtherTextareaField
        public ConversationRecordPage ValidateLocationIfOtherTextareaFieldText(string ExpectedText)
        {
            WaitForElement(LocationIfOtherTextareaField);
            ScrollToElement(LocationIfOtherTextareaField);
            ValidateElementValue(LocationIfOtherTextareaField, ExpectedText);

            return this;
        }

        public ConversationRecordPage ValidateLocationIfOtherVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(LocationIfOtherTextareaField);
            else
                WaitForElementNotVisible(LocationIfOtherTextareaField, 3);

            return this;
        }

        //verify maxlength attribute value of LocationIfOtherTextareaField
        public ConversationRecordPage ValidateLocationIfOtherTextareaFieldMaxLength(string ExpectedLength)
        {
            ValidateElementAttribute(LocationIfOtherTextareaField, "maxlength", ExpectedLength);

            return this;
        }


        public ConversationRecordPage ClickWellbeingLink()
        {
            WaitForElementToBeClickable(WellbeingidLink);
            Click(WellbeingidLink);

            return this;
        }

        public ConversationRecordPage ValidateWellbeingLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(WellbeingidLink);
            ValidateElementText(WellbeingidLink, ExpectedText);

            return this;
        }

        public ConversationRecordPage ClickWellbeingClearButton()
        {
            WaitForElementToBeClickable(WellbeingidClearButton);
            Click(WellbeingidClearButton);

            return this;
        }

        public ConversationRecordPage ClickWellbeingLookupButton()
        {
            WaitForElementToBeClickable(WellbeingidLookupButton);
            Click(WellbeingidLookupButton);

            return this;
        }

        //verify that WellbeingidLookupButton is displayed or not displayed
        public ConversationRecordPage ValidateWellbeingLookupButtonIsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(WellbeingidLookupButton);
            else
                WaitForElementNotVisible(WellbeingidLookupButton, 3);

            return this;
        }

        //verify Wellbeing_actiontaken text
        public ConversationRecordPage VerifyActionTaken_HasPainReliefBeenOfferedText(string ExpectedText)
        {
            ValidateElementValue(Wellbeing_actiontaken, ExpectedText);

            return this;
        }

        public ConversationRecordPage ValidateActionTakenIsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Wellbeing_actiontaken);
            else
                WaitForElementNotVisible(Wellbeing_actiontaken, 3);

            return this;
        }

        //Insert text in Wellbeing_actiontaken field
        public ConversationRecordPage InsertTextOnActionTaken(string TextToInsert)
        {
            WaitForElementToBeClickable(Wellbeing_actiontaken);
            SendKeys(Wellbeing_actiontaken, TextToInsert + Keys.Tab);

            return this;
        }

        public ConversationRecordPage ValidateTotalTimeSpentWithPersonMinutesText(string ExpectedText)
        {
            ValidateElementValue(Totaltimespentwithclientminutes, ExpectedText);

            return this;
        }

        public ConversationRecordPage InsertTextOnTotalTimesSpentWithPersonMinutes(string TextToInsert)
        {
            WaitForElementToBeClickable(Totaltimespentwithclientminutes);
            SendKeys(Totaltimespentwithclientminutes, TextToInsert + Keys.Tab);

            return this;
        }

        //verify TotalTimeSpentWithPersonMinutes field is displayed or not displayed
        public ConversationRecordPage ValidateTotalTimeSpentWithPersonMinutesFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Totaltimespentwithclientminutes);
            else
                WaitForElementNotVisible(Totaltimespentwithclientminutes, 3);

            return this;
        }

        public ConversationRecordPage VerifyTotalTimeSpentWithPersonMinutesFieldErrorVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(TotalTimeSpentWithClientMinutesFieldError);
            else
                WaitForElementNotVisible(TotalTimeSpentWithClientMinutesFieldError, 3);

            return this;
        }


        public ConversationRecordPage VerifyTotalTimeSpentWithPersonMinutesFieldErrorText(string ExpectedText)
        {
            WaitForElement(TotalTimeSpentWithClientMinutesFieldError);
            ValidateElementByTitle(TotalTimeSpentWithClientMinutesFieldError, ExpectedText);

            return this;
        }

        public ConversationRecordPage ValidateAdditionalnotesText(string ExpectedText)
        {
            ValidateElementText(Additionalnotes, ExpectedText);

            return this;
        }

        public ConversationRecordPage InsertTextOnAdditionalnotes(string TextToInsert)
        {
            WaitForElementToBeClickable(Additionalnotes);
            SendKeys(Additionalnotes, TextToInsert + Keys.Tab);

            return this;
        }

        //verify additional notes field is displayed or not displayed
        public ConversationRecordPage ValidateAdditionalNotesFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Additionalnotes);
            else
                WaitForElementNotVisible(Additionalnotes, 3);

            return this;
        }

        public ConversationRecordPage ClickAssistanceneededLink()
        {
            WaitForElementToBeClickable(AssistanceneededidLink);
            Click(AssistanceneededidLink);

            return this;
        }

        public ConversationRecordPage ValidateAssistanceneededLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(AssistanceneededidLink);
            ValidateElementText(AssistanceneededidLink, ExpectedText);

            return this;
        }

        public ConversationRecordPage ClickAssistanceneededClearButton()
        {
            WaitForElementToBeClickable(AssistanceneededidClearButton);
            Click(AssistanceneededidClearButton);

            return this;
        }

        public ConversationRecordPage ClickAssistanceNeededLookupButton()
        {
            WaitForElementToBeClickable(AssistanceneededidLookupButton);
            Click(AssistanceneededidLookupButton);

            return this;
        }

        public ConversationRecordPage ClickStaffRequiredLookupButton()
        {
            WaitForElementToBeClickable(StaffrequiredLookupButton);
            Click(StaffrequiredLookupButton);

            return this;
        }

        public ConversationRecordPage ValidateCarenoteText(string ExpectedText)
        {
            WaitForElement(Carenote);
            ScrollToElement(Carenote);
            var fieldValue = GetElementValueByJavascript(CarenoteFieldId);
            Assert.AreEqual(ExpectedText, fieldValue);

            return this;
        }

        public ConversationRecordPage InsertTextOnCarenote(string TextToInsert)
        {
            WaitForElementToBeClickable(Carenote);
            SendKeys(Carenote, TextToInsert + Keys.Tab);

            return this;
        }

        public ConversationRecordPage ClickLinkedAdlCategoriesLookupButton()
        {
            WaitForElementToBeClickable(LinkedadlcategoriesidLookupButton);
            Click(LinkedadlcategoriesidLookupButton);

            return this;
        }

        //verify LinkedadlcategoriesidLookupButton is displayed or not displayed
        public ConversationRecordPage ValidateLinkedAdlCategoriesLookupButtonIsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(LinkedadlcategoriesidLookupButton);
            else
                WaitForElementNotVisible(LinkedadlcategoriesidLookupButton, 3);

            return this;
        }

        public ConversationRecordPage ClickLinkedAdlCategories_SelectedElementLink(string OptionId)
        {
            WaitForElementToBeClickable(LinkedAdlCategories_SelectedElement(OptionId));
            Click(LinkedAdlCategories_SelectedElement(OptionId));

            return this;
        }

        public ConversationRecordPage ValidateLinkedAdlCategories_SelectedElementLinkText(string OptionId, string ExpectedText)
        {
            WaitForElementToBeClickable(LinkedAdlCategories_SelectedElement(OptionId));
            ScrollToElement(LinkedAdlCategories_SelectedElement(OptionId));
            ValidateElementText(LinkedAdlCategories_SelectedElement(OptionId), ExpectedText);

            return this;
        }

        //Verify LinkedAdlCategories_SelectedElementOnEdit field text on edit before save
        public ConversationRecordPage ValidateLinkedAdlCategories_SelectedElementFieldTextBeforeSave(string OptionId, string ExpectedText)
        {
            WaitForElementToBeClickable(LinkedAdlCategories_SelectedElementOnEdit(OptionId));
            ValidateElementTextContainsText(LinkedAdlCategories_SelectedElementOnEdit(OptionId), ExpectedText);

            return this;
        }

        public ConversationRecordPage ClickIsincludeinnexthandover_YesOption()
        {
            WaitForElementToBeClickable(Isincludeinnexthandover_1);
            Click(Isincludeinnexthandover_1);

            return this;
        }

        public ConversationRecordPage ValidateIsincludeinnexthandover_YesOptionChecked()
        {
            WaitForElement(Isincludeinnexthandover_1);
            ValidateElementChecked(Isincludeinnexthandover_1);

            return this;
        }

        public ConversationRecordPage ValidateIsincludeinnexthandover_YesOptionNotChecked()
        {
            WaitForElement(Isincludeinnexthandover_1);
            ValidateElementNotChecked(Isincludeinnexthandover_1);

            return this;
        }

        public ConversationRecordPage ClickIsincludeinnexthandover_NoOption()
        {
            WaitForElementToBeClickable(Isincludeinnexthandover_0);
            Click(Isincludeinnexthandover_0);

            return this;
        }

        public ConversationRecordPage ValidateIsincludeinnexthandover_NoOptionChecked()
        {
            WaitForElement(Isincludeinnexthandover_0);
            ValidateElementChecked(Isincludeinnexthandover_0);

            return this;
        }

        public ConversationRecordPage ValidateIsincludeinnexthandover_NoOptionNotChecked()
        {
            WaitForElement(Isincludeinnexthandover_0);
            ValidateElementNotChecked(Isincludeinnexthandover_0);

            return this;
        }

        public ConversationRecordPage ClickFlagrecordforhandover_YesOption()
        {
            WaitForElementToBeClickable(Flagrecordforhandover_1);
            Click(Flagrecordforhandover_1);

            return this;
        }

        public ConversationRecordPage ValidateFlagrecordforhandover_YesOptionChecked()
        {
            WaitForElement(Flagrecordforhandover_1);
            ValidateElementChecked(Flagrecordforhandover_1);

            return this;
        }

        public ConversationRecordPage ValidateFlagrecordforhandover_YesOptionNotChecked()
        {
            WaitForElement(Flagrecordforhandover_1);
            ValidateElementNotChecked(Flagrecordforhandover_1);

            return this;
        }

        public ConversationRecordPage ClickFlagrecordforhandover_NoOption()
        {
            WaitForElementToBeClickable(Flagrecordforhandover_0);
            Click(Flagrecordforhandover_0);

            return this;
        }

        public ConversationRecordPage ValidateFlagrecordforhandover_NoOptionChecked()
        {
            WaitForElement(Flagrecordforhandover_0);
            ValidateElementChecked(Flagrecordforhandover_0);

            return this;
        }

        public ConversationRecordPage ValidateFlagrecordforhandover_NoOptionNotChecked()
        {
            WaitForElement(Flagrecordforhandover_0);
            ValidateElementNotChecked(Flagrecordforhandover_0);

            return this;
        }

        public ConversationRecordPage ValidateFieldIsVisible(string FieldName, bool ExpectVisible = true)
        {
            if (ExpectVisible)
                WaitForElementVisible(FieldLabel(FieldName));
            else
                WaitForElementNotVisible(FieldLabel(FieldName), 3);

            return this;
        }

        public ConversationRecordPage ValidateMandatoryFieldIsVisible(string FieldName, bool ExpectVisible = true)
        {
            if (ExpectVisible)
                WaitForElementVisible(MandatoryField_Label(FieldName));
            else
                WaitForElementNotVisible(MandatoryField_Label(FieldName), 3);

            return this;
        }

        //verify SectionNameByPosition
        public ConversationRecordPage ValidateSectionName(string ExpectedText, int Position)
        {
            WaitForElement(SectionNameByPosition(Position));            
            ValidateElementText(SectionNameByPosition(Position), ExpectedText);

            return this;
        }

        //verify sectionnamebyposition is displayed or not displayed
        public ConversationRecordPage ValidateSectionNameVisible(string ExpectedText, int Position, bool ExpectVisible = true)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(SectionNameByPosition(Position));
                ValidateElementText(SectionNameByPosition(Position), ExpectedText);
            }
            else
            {
                WaitForElementNotVisible(SectionNameByPosition(Position), 3);
            }

            return this;
        }

        public ConversationRecordPage ValidateStaffRequiredSelectedOptionText(string OptionId, string ExpectedText)
        {
            WaitForElement(StaffRequired_SelectedOption(OptionId));
            ValidateElementTextContainsText(StaffRequired_SelectedOption(OptionId), ExpectedText);

            return this;
        }

        public ConversationRecordPage ValidateStaffRequiredSelectedOptionText(Guid OptionId, string ExpectedText)
        {
            return ValidateStaffRequiredSelectedOptionText(OptionId.ToString(), ExpectedText);
        }

        public ConversationRecordPage SelectAssistanceAmountFromPicklist(string OptionText)
        {
            WaitForElementToBeClickable(AssistanceAmountPicklist);
            SelectPicklistElementByText(AssistanceAmountPicklist, OptionText);

            return this;
        }

        //verify AssistanceAmountPicklist Selected Text
        public ConversationRecordPage ValidateAssistanceAmountPicklistSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(AssistanceAmountPicklist, ExpectedText);

            return this;
        }

        //verify AssistanceAmountPicklist is visible or not visible
        public ConversationRecordPage ValidateAssistanceAmountPicklistVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(AssistanceAmountPicklist);
            else
                WaitForElementNotVisible(AssistanceAmountPicklist, 3);
            return this;
        }

    }
}

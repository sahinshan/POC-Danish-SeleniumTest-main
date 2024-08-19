using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class EmotionalSupportRecordPage : CommonMethods
    {
        public EmotionalSupportRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cppersonemotional&')]");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
        readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
        readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Emotional Support: ']");

        readonly By TopPageNotification = By.XPath("//*[@id='CWNotificationMessage_DataForm']");

        #region General section

        readonly By PersonidLink = By.XPath("//*[@id='CWField_personid_Link']");
        readonly By PersonidLookupButton = By.XPath("//*[@id='CWLookupBtn_personid']");
        readonly By Preferences = By.XPath("//*[@id='CWField_preferences']");
        readonly By Careconsentgivenid = By.XPath("//*[@id='CWField_careconsentgivenid']");
        readonly By Careconsentgivenid_ErrorLabel = By.XPath("//*[@id='CWControlHolder_careconsentgivenid']/label/span");
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

        readonly By Carenonconsentid = By.XPath("//*[@id='CWField_carenonconsentid']");
        readonly By Carenonconsentid_ErrorLabel = By.XPath("//*[@id='CWControlHolder_carenonconsentid']/label/span");

        readonly By Reasonforabsence = By.XPath("//*[@id='CWField_reasonforabsence']");
        readonly By Reasonforabsence_ErrorLabel = By.XPath("//*[@id='CWControlHolder_reasonforabsence']/label/span");

        readonly By Reasonconsentdeclined = By.XPath("//*[@id='CWField_reasonconsentdeclined']");
        readonly By Reasonconsentdeclined_ErrorLabel = By.XPath("//*[@id='CWControlHolder_reasonconsentdeclined']/label/span");
        readonly By Encouragementgiven = By.XPath("//*[@id='CWField_encouragementgiven']");
        readonly By Encouragementgiven_ErrorLabel = By.XPath("//*[@id='CWControlHolder_encouragementgiven']/label/span");
        readonly By Careprovidedwithoutconsent_1 = By.XPath("//*[@id='CWField_careprovidedwithoutconsent_1']");
        readonly By Careprovidedwithoutconsent_0 = By.XPath("//*[@id='CWField_careprovidedwithoutconsent_0']");

        readonly By Deferredtodate = By.XPath("//*[@id='CWField_deferredtodate']");
        readonly By Deferredtodate_ErrorLabel = By.XPath("//*[@id='CWControlHolder_deferredtodate']/label/span");
        readonly By Timeorshiftid = By.XPath("//*[@id='CWField_timeorshiftid']");
        readonly By Timeorshiftid_ErrorLabel = By.XPath("//*[@id='CWControlHolder_timeorshiftid']/label/span");
        readonly By Deferredtotime = By.XPath("//*[@id='CWField_deferredtotime']");
        readonly By Deferredtotime_ErrorLabel = By.XPath("//*[@id='CWControlHolder_deferredtotime']/label/span");
        readonly By DeferredtoselectedshiftidLink = By.XPath("//*[@id='CWField_deferredtoselectedshiftid_Link']");
        readonly By DeferredtoselectedshiftidLookupButton = By.XPath("//*[@id='CWLookupBtn_deferredtoselectedshiftid']");
        readonly By Deferredtoselectedshiftid_ErrorLabel = By.XPath("//*[@id='CWControlHolder_deferredtoselectedshiftid']/label/span");

        #endregion

        #region Details section

        By TypeEmotionalSupportRequired_SelectedElementLink(string ElementId) => By.XPath("//*[@id='MS_emotionalsupports_" + ElementId + "']/a[@id='" + ElementId + "_Link']");
        By TypeEmotionalSupportRequired_SelectedElementRemoveButton(string ElementId) => By.XPath("//*[@id='MS_emotionalsupports_" + ElementId + "']/a[text()='Remove']");
        readonly By TypeEmotionalSupportRequiredLookupButton = By.XPath("//*[@id='CWLookupBtn_emotionalsupports']");
        readonly By TypeEmotionalSupportRequired_ErrorLabel = By.XPath("//*[@id='CWControlHolder_emotionalsupports']/label/span");

        readonly By Emotionalsupportifother = By.XPath("//*[@id='CWField_emotionalsupportifother']");
        readonly By Emotionalsupportifother_ErrorLabel = By.XPath("//*[@id='CWControlHolder_emotionalsupportifother']/label/span");

        readonly By Describethesupportgiven = By.XPath("//*[@id='CWField_describethesupportgiven']");
        readonly By Describethesupportgiven_ErrorLabel = By.XPath("//*[@id='CWControlHolder_describethesupportgiven']/label/span");

        readonly By Outcome = By.XPath("//*[@id='CWField_outcome']");
        readonly By Outcome_ErrorLabel = By.XPath("//*[@id='CWControlHolder_outcome']/label/span");

        #endregion

        #region Additional Information section

        By Location_SelectedElementLink(string ElementId) => By.XPath("//*[@id='MS_carephysicallocations_" + ElementId + "']/a[@id='" + ElementId + "_Link']");
        By Location_SelectedElementRemoveButton(string ElementId) => By.XPath("//*[@id='MS_carephysicallocations_" + ElementId + "']/a[text()='Remove']");
        readonly By CarephysicallocationidLookupButton = By.XPath("//*[@id='CWLookupBtn_carephysicallocations']");
        readonly By Location_ErrorLabel = By.XPath("//*[@id='CWControlHolder_carephysicallocations']/label/span");

        readonly By locationifother = By.XPath("//*[@id='CWField_locationifother']");
        readonly By LocationIfOther_ErrorLabel = By.XPath("//*[@id='CWControlHolder_locationifother']/label/span");
        readonly By CarewellbeingidLink = By.XPath("//*[@id='CWField_carewellbeingid_Link']");
        readonly By Wellbeing_ErrorLabel = By.XPath("//*[@id='CWControlHolder_carewellbeingid']/label/span");
        readonly By CarewellbeingidClearButton = By.XPath("//*[@id='CWClearLookup_carewellbeingid']");
        readonly By CarewellbeingidLookupButton = By.XPath("//*[@id='CWLookupBtn_carewellbeingid']");
        readonly By actiontaken = By.XPath("//*[@id='CWField_actiontaken']");
        readonly By ActionTaken_ErrorLabel = By.XPath("//*[@id='CWControlHolder_actiontaken']/label/span");
        readonly By Timespentwithclient = By.XPath("//*[@id='CWField_timespentwithclient']");
        readonly By TotalTimeSpentWithPerson_ErrorLabel = By.XPath("//*[@id='CWControlHolder_timespentwithclient']/label/span");
        readonly By Additionalnotes = By.XPath("//*[@id='CWField_additionalnotes']");
        readonly By CareassistanceneededidLink = By.XPath("//*[@id='CWField_careassistanceneededid_Link']");
        readonly By CareassistanceneededidClearButton = By.XPath("//*[@id='CWClearLookup_careassistanceneededid']");
        readonly By CareassistanceneededidLookupButton = By.XPath("//*[@id='CWLookupBtn_careassistanceneededid']");
        readonly By AssistanceNeeded_ErrorLabel = By.XPath("//*[@id='CWControlHolder_careassistanceneededid']/label/span");
        readonly By careassistancelevelid = By.XPath("//*[@id='CWField_careassistancelevelid']");
        readonly By AssistanceAmount_ErrorLabel = By.XPath("//*[@id='CWControlHolder_careassistancelevelid']/label/span");

        By StaffRequired_SelectedElementLinkBeforeSave(string ElementId, string ElementText) => By.XPath("//*[@id='MS_staffrequired_" + ElementId + "'][text()='" + ElementText + "']");
        By StaffRequired_SelectedElementLink(string ElementId) => By.XPath("//*[@id='MS_staffrequired_" + ElementId + "']/a[@id='" + ElementId + "_Link']");
        By StaffRequired_SelectedElementRemoveButton(string ElementId) => By.XPath("//*[@id='MS_staffrequired_" + ElementId + "']/a[text()='Remove']");
        readonly By staffrequiredLookupButton = By.XPath("//*[@id='CWLookupBtn_staffrequired']");

        #endregion

        #region Care Note

        readonly By Carenote = By.XPath("//*[@id='CWField_carenote']");

        #endregion

        #region Care Needs

        readonly By LinkedadlcategoriesidLookupButton = By.XPath("//*[@id='CWLookupBtn_linkedadlcategoriesid']");

        #endregion

        #region Handover

        readonly By Isincludeinnexthandover_1 = By.XPath("//*[@id='CWField_isincludeinnexthandover_1']");
        readonly By Isincludeinnexthandover_0 = By.XPath("//*[@id='CWField_isincludeinnexthandover_0']");
        readonly By Flagrecordforhandover_1 = By.XPath("//*[@id='CWField_flagrecordforhandover_1']");
        readonly By Flagrecordforhandover_0 = By.XPath("//*[@id='CWField_flagrecordforhandover_0']");

        #endregion


        public EmotionalSupportRecordPage WaitForPageToLoad()
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


        public EmotionalSupportRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public EmotionalSupportRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public EmotionalSupportRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public EmotionalSupportRecordPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public EmotionalSupportRecordPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }


        public EmotionalSupportRecordPage ValidateTopPageNotificationText(string ExpectedText)
        {
            ValidateElementText(TopPageNotification, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ValidateTopPageNotificationVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(TopPageNotification);
            else
                WaitForElementNotVisible(TopPageNotification, 3);

            return this;
        }


        public EmotionalSupportRecordPage ClickPersonLink()
        {
            WaitForElementToBeClickable(PersonidLink);
            Click(PersonidLink);

            return this;
        }

        public EmotionalSupportRecordPage ValidatePersonLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(PersonidLink);
            ValidateElementText(PersonidLink, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ClickPersonLookupButton()
        {
            WaitForElementToBeClickable(PersonidLookupButton);
            Click(PersonidLookupButton);

            return this;
        }

        public EmotionalSupportRecordPage ValidatePreferencesText(string ExpectedText)
        {
            var elementValue = this.GetElementValueByJavascript("CWField_preferences");
            Assert.AreEqual(ExpectedText, elementValue);

            return this;
        }

        public EmotionalSupportRecordPage InsertTextOnPreferences(string TextToInsert)
        {
            WaitForElementToBeClickable(Preferences);
            SendKeys(Preferences, TextToInsert + Keys.Tab);

            return this;
        }

        public EmotionalSupportRecordPage SelectNonConsentDetail(string TextToSelect)
        {
            WaitForElementToBeClickable(Carenonconsentid);
            SelectPicklistElementByText(Carenonconsentid, TextToSelect);

            return this;
        }

        public EmotionalSupportRecordPage ValidateNonConsentDetailSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Carenonconsentid, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ValidateNonConsentDetailErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Carenonconsentid_ErrorLabel, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ValidateNonConsentDetailErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Carenonconsentid_ErrorLabel);
            else
                WaitForElementNotVisible(Carenonconsentid_ErrorLabel, 3);

            return this;
        }

        public EmotionalSupportRecordPage ValidateReasonForAbsenceText(string ExpectedText)
        {
            ValidateElementText(Reasonforabsence, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage InsertTextOnReasonForAbsence(string TextToInsert)
        {
            WaitForElementToBeClickable(Reasonforabsence);
            SendKeys(Reasonforabsence, TextToInsert + Keys.Tab);

            return this;
        }

        public EmotionalSupportRecordPage ValidateReasonForAbsenceErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Reasonforabsence_ErrorLabel, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ValidateReasonForAbsenceErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Reasonforabsence_ErrorLabel);
            else
                WaitForElementNotVisible(Reasonforabsence_ErrorLabel, 3);

            return this;
        }

        public EmotionalSupportRecordPage ValidateReasonConsentDeclinedText(string ExpectedText)
        {
            ValidateElementText(Reasonconsentdeclined, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage InsertTextOnReasonConsentDeclined(string TextToInsert)
        {
            WaitForElementToBeClickable(Reasonconsentdeclined);
            SendKeys(Reasonconsentdeclined, TextToInsert + Keys.Tab);

            return this;
        }

        public EmotionalSupportRecordPage ValidateReasonConsentDeclinedErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Reasonconsentdeclined_ErrorLabel, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ValidateReasonConsentDeclinedErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Reasonconsentdeclined_ErrorLabel);
            else
                WaitForElementNotVisible(Reasonconsentdeclined_ErrorLabel, 3);

            return this;
        }

        public EmotionalSupportRecordPage ValidateEncouragementGivenText(string ExpectedText)
        {
            ValidateElementText(Encouragementgiven, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage InsertTextOnEncouragementGiven(string TextToInsert)
        {
            WaitForElementToBeClickable(Encouragementgiven);
            SendKeys(Encouragementgiven, TextToInsert + Keys.Tab);

            return this;
        }

        public EmotionalSupportRecordPage ValidateEncouragementGivenErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Encouragementgiven_ErrorLabel, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ValidateEncouragementGivenErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Encouragementgiven_ErrorLabel);
            else
                WaitForElementNotVisible(Encouragementgiven_ErrorLabel, 3);

            return this;
        }

        public EmotionalSupportRecordPage ClickCareProvidedWithoutConsent_YesRadioButton()
        {
            WaitForElementToBeClickable(Careprovidedwithoutconsent_1);
            Click(Careprovidedwithoutconsent_1);

            return this;
        }

        public EmotionalSupportRecordPage ValidateCareProvidedWithoutConsent_YesRadioButtonChecked()
        {
            WaitForElement(Careprovidedwithoutconsent_1);
            ValidateElementChecked(Careprovidedwithoutconsent_1);

            return this;
        }

        public EmotionalSupportRecordPage ValidateCareProvidedWithoutConsent_YesRadioButtonNotChecked()
        {
            WaitForElement(Careprovidedwithoutconsent_1);
            ValidateElementNotChecked(Careprovidedwithoutconsent_1);

            return this;
        }

        public EmotionalSupportRecordPage ClickCareProvidedWithoutConsent_NoRadioButton()
        {
            WaitForElementToBeClickable(Careprovidedwithoutconsent_0);
            Click(Careprovidedwithoutconsent_0);

            return this;
        }

        public EmotionalSupportRecordPage ValidateCareProvidedWithoutConsent_NoRadioButtonChecked()
        {
            WaitForElement(Careprovidedwithoutconsent_0);
            ValidateElementChecked(Careprovidedwithoutconsent_0);

            return this;
        }

        public EmotionalSupportRecordPage ValidateCareProvidedWithoutConsent_NoRadioButtonNotChecked()
        {
            WaitForElement(Careprovidedwithoutconsent_0);
            ValidateElementNotChecked(Careprovidedwithoutconsent_0);

            return this;
        }

        public EmotionalSupportRecordPage ValidateDeferredToDateText(string ExpectedText)
        {
            ValidateElementValue(Deferredtodate, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage InsertDeferredToDate(string TextToInsert)
        {
            WaitForElementToBeClickable(Deferredtodate);
            SendKeys(Deferredtodate, TextToInsert + Keys.Tab);

            return this;
        }

        public EmotionalSupportRecordPage ValidateDeferredToDateErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Deferredtodate_ErrorLabel, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ValidateDeferredToDateErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Deferredtodate_ErrorLabel);
            else
                WaitForElementNotVisible(Deferredtodate_ErrorLabel, 3);

            return this;
        }

        public EmotionalSupportRecordPage SelectDeferredToTimeOrShift(string TextToSelect)
        {
            WaitForElementToBeClickable(Timeorshiftid);
            SelectPicklistElementByText(Timeorshiftid, TextToSelect);

            return this;
        }

        public EmotionalSupportRecordPage ValidateDeferredToTimeOrShiftSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Timeorshiftid, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ValidateDeferredToTimeOrShiftErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Timeorshiftid_ErrorLabel, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ValidateDeferredToTimeOrShiftErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Timeorshiftid_ErrorLabel);
            else
                WaitForElementNotVisible(Timeorshiftid_ErrorLabel, 3);

            return this;
        }

        public EmotionalSupportRecordPage ValidateDeferredToTimeText(string ExpectedText)
        {
            ValidateElementValue(Deferredtotime, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage InsertDeferredToTime(string TextToInsert)
        {
            WaitForElementToBeClickable(Deferredtotime);
            SendKeys(Deferredtotime, TextToInsert + Keys.Tab);

            return this;
        }

        public EmotionalSupportRecordPage ValidateDeferredToTimeErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Deferredtotime_ErrorLabel, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ValidateDeferredToTimeErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Deferredtotime_ErrorLabel);
            else
                WaitForElementNotVisible(Deferredtotime_ErrorLabel, 3);

            return this;
        }

        public EmotionalSupportRecordPage ClickDeferredToShiftLink()
        {
            WaitForElementToBeClickable(DeferredtoselectedshiftidLink);
            Click(DeferredtoselectedshiftidLink);

            return this;
        }

        public EmotionalSupportRecordPage ValidateDeferredToShiftLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(DeferredtoselectedshiftidLink);
            ValidateElementText(DeferredtoselectedshiftidLink, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ClickDeferredToShiftLookupButton()
        {
            WaitForElementToBeClickable(DeferredtoselectedshiftidLookupButton);
            Click(DeferredtoselectedshiftidLookupButton);

            return this;
        }

        public EmotionalSupportRecordPage ValidateDeferredToShiftErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Deferredtoselectedshiftid_ErrorLabel, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ValidateDeferredToShiftErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Deferredtoselectedshiftid_ErrorLabel);
            else
                WaitForElementNotVisible(Deferredtoselectedshiftid_ErrorLabel, 3);

            return this;
        }

        public EmotionalSupportRecordPage SelectConsentGiven(string TextToSelect)
        {
            WaitForElementToBeClickable(Careconsentgivenid);
            SelectPicklistElementByText(Careconsentgivenid, TextToSelect);

            return this;
        }

        public EmotionalSupportRecordPage ValidateConsentGivenSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Careconsentgivenid, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ValidateConsentGivenErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Careconsentgivenid_ErrorLabel, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ValidateConsentGivenErrorLabelVisibility(bool ExpectVisible)
        {
            if(ExpectVisible)
                WaitForElementVisible(Careconsentgivenid_ErrorLabel);
            else
                WaitForElementNotVisible(Careconsentgivenid_ErrorLabel, 3);

            return this;
        }

        public EmotionalSupportRecordPage ClickResponsibleTeamLink()
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            Click(ResponsibleTeamLink);

            return this;
        }

        public EmotionalSupportRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            ValidateElementText(ResponsibleTeamLink, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamLookupButton);
            Click(ResponsibleTeamLookupButton);

            return this;
        }

        //verify Responsible Team lookup button is visible
        public EmotionalSupportRecordPage ValidateResponsibleTeamLookupButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ResponsibleTeamLookupButton);
            else
                WaitForElementNotVisible(ResponsibleTeamLookupButton, 3);

            return this;
        }

        public EmotionalSupportRecordPage ValidateDateAndTimeOccurredText(string ExpectedText)
        {
            ValidateElementValue(Occurred, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage InsertTextOnDateAndTimeOccurred(string TextToInsert)
        {
            WaitForElementToBeClickable(Occurred);
            SendKeys(Occurred, TextToInsert + Keys.Tab);

            return this;
        }

        public EmotionalSupportRecordPage ClickDateAndTimeOccurredDatePicker()
        {
            WaitForElementToBeClickable(OccurredDatePicker);
            Click(OccurredDatePicker);

            return this;
        }

        public EmotionalSupportRecordPage ValidateDateAndTimeOccurred_TimeText(string ExpectedText)
        {
            ValidateElementValue(Occurred_Time, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage InsertTextOnDateAndTimeOccurred_Time(string TextToInsert)
        {
            WaitForElementToBeClickable(Occurred_Time);
            SendKeys(Occurred_Time, TextToInsert + Keys.Tab);

            return this;
        }

        public EmotionalSupportRecordPage ClickDateAndTimeOccurred_Time_TimePicker()
        {
            WaitForElementToBeClickable(Occurred_Time_TimePicker);
            Click(Occurred_Time_TimePicker);

            return this;
        }

        public EmotionalSupportRecordPage ValidateCreatedOnText(string ExpectedText)
        {
            ValidateElementValue(Createdon, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage InsertTextOnCreatedOn(string TextToInsert)
        {
            WaitForElementToBeClickable(Createdon);
            SendKeys(Createdon, TextToInsert + Keys.Tab);

            return this;
        }

        public EmotionalSupportRecordPage ClickCreatedOnDatePicker()
        {
            WaitForElementToBeClickable(CreatedonDatePicker);
            Click(CreatedonDatePicker);

            return this;
        }

        public EmotionalSupportRecordPage ValidateCreatedOn_TimeText(string ExpectedText)
        {
            ValidateElementValue(Createdon_Time, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage InsertTextOnCreatedOn_Time(string TextToInsert)
        {
            WaitForElementToBeClickable(Createdon_Time);
            SendKeys(Createdon_Time, TextToInsert + Keys.Tab);

            return this;
        }

        public EmotionalSupportRecordPage ClickCreatedOn_Time_TimePicker()
        {
            WaitForElementToBeClickable(Createdon_Time_TimePicker);
            Click(Createdon_Time_TimePicker);

            return this;
        }

        public EmotionalSupportRecordPage ClickTypeEmotionalSupportRequired_SelectedElementLink(string ElementId)
        {
            WaitForElementToBeClickable(TypeEmotionalSupportRequired_SelectedElementLink(ElementId));
            Click(TypeEmotionalSupportRequired_SelectedElementLink(ElementId));

            return this;
        }

        public EmotionalSupportRecordPage ValidateTypeEmotionalSupportRequired_SelectedElementLinkText(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(TypeEmotionalSupportRequired_SelectedElementLink(ElementId));
            ValidateElementText(TypeEmotionalSupportRequired_SelectedElementLink(ElementId), ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ValidateTypeEmotionalSupportRequired_SelectedElementLinkText(Guid ElementId, string ExpectedText)
        {
            return ValidateTypeEmotionalSupportRequired_SelectedElementLinkText(ElementId.ToString(), ExpectedText);
        }

        public EmotionalSupportRecordPage ClickTypeEmotionalSupportRequired_SelectedElementRemoveButton(string ElementId)
        {
            WaitForElementToBeClickable(TypeEmotionalSupportRequired_SelectedElementRemoveButton(ElementId));
            Click(TypeEmotionalSupportRequired_SelectedElementRemoveButton(ElementId));

            return this;
        }

        public EmotionalSupportRecordPage ClickTypeEmotionalSupportRequiredLookupButton()
        {
            WaitForElementToBeClickable(TypeEmotionalSupportRequiredLookupButton);
            Click(TypeEmotionalSupportRequiredLookupButton);

            return this;
        }

        public EmotionalSupportRecordPage ValidateEmotionalSupportIfOtherText(string ExpectedText)
        {
            ValidateElementValue(Emotionalsupportifother, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage InsertTextOnEmotionalSupportIfOther(string TextToInsert)
        {
            WaitForElementToBeClickable(Emotionalsupportifother);
            SendKeys(Emotionalsupportifother, TextToInsert + Keys.Tab);

            return this;
        }

        public EmotionalSupportRecordPage ValidateEmotionalSupportIfOtherVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Emotionalsupportifother);
            else
                WaitForElementNotVisible(Emotionalsupportifother, 3);

            return this;
        }

        public EmotionalSupportRecordPage ValidateDescribeSupportGivenText(string ExpectedText)
        {
            ValidateElementText(Describethesupportgiven, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage InsertTextOnDescribeSupportGiven(string TextToInsert)
        {
            WaitForElementToBeClickable(Describethesupportgiven);
            SendKeys(Describethesupportgiven, TextToInsert + Keys.Tab);

            return this;
        }

        public EmotionalSupportRecordPage InsertTextOnWhatWasTheOutcome(string TextToInsert)
        {
            WaitForElementToBeClickable(Outcome);
            SendKeys(Outcome, TextToInsert + Keys.Tab);

            return this;
        }

        public EmotionalSupportRecordPage ValidateWhatWasTheOutcomeText(string ExpectedText)
        {
            ValidateElementText(Outcome, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ValidateTypeEmotionalSupportRequiredErrorLabelText(string ExpectedText)
        {
            ValidateElementText(TypeEmotionalSupportRequired_ErrorLabel, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ValidateTypeEmotionalSupportRequiredErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(TypeEmotionalSupportRequired_ErrorLabel);
            else
                WaitForElementNotVisible(TypeEmotionalSupportRequired_ErrorLabel, 3);

            return this;
        }

        public EmotionalSupportRecordPage ValidateEmotionalSupportIfOtherErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Emotionalsupportifother_ErrorLabel, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ValidateEmotionalSupportIfOtherErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Emotionalsupportifother_ErrorLabel);
            else
                WaitForElementNotVisible(Emotionalsupportifother_ErrorLabel, 3);

            return this;
        }

        public EmotionalSupportRecordPage ValidateDescribeSupportGivenErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Describethesupportgiven_ErrorLabel, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ValidateDescribeSupportGivenErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Describethesupportgiven_ErrorLabel);
            else
                WaitForElementNotVisible(Describethesupportgiven_ErrorLabel, 3);

            return this;
        }

        public EmotionalSupportRecordPage ValidateWhatWasTheOutcomeErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Outcome_ErrorLabel, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ValidateWhatWasTheOutcomeErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Outcome_ErrorLabel);
            else
                WaitForElementNotVisible(Outcome_ErrorLabel, 3);

            return this;
        }

        public EmotionalSupportRecordPage ClickLocation_SelectedElementLink(string ElementId)
        {
            WaitForElementToBeClickable(Location_SelectedElementLink(ElementId));
            Click(Location_SelectedElementLink(ElementId));

            return this;
        }

        public EmotionalSupportRecordPage ValidateLocation_SelectedElementLinkText(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(Location_SelectedElementLink(ElementId));
            ValidateElementText(Location_SelectedElementLink(ElementId), ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ValidateLocation_SelectedElementLinkText(Guid ElementId, string ExpectedText)
        {
            return ValidateLocation_SelectedElementLinkText(ElementId.ToString(), ExpectedText);
        }

        public EmotionalSupportRecordPage ClickLocation_SelectedElementRemoveButton(string ElementId)
        {
            WaitForElementToBeClickable(Location_SelectedElementRemoveButton(ElementId));
            Click(Location_SelectedElementRemoveButton(ElementId));

            return this;
        }

        public EmotionalSupportRecordPage ClickLocationLookupButton()
        {
            WaitForElementToBeClickable(CarephysicallocationidLookupButton);
            Click(CarephysicallocationidLookupButton);

            return this;
        }

        public EmotionalSupportRecordPage ValidateLocationIfOtherText(string ExpectedText)
        {
            ValidateElementText(locationifother, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage InsertTextOnLocationIfOther(string TextToInsert)
        {
            WaitForElementToBeClickable(locationifother);
            SendKeys(locationifother, TextToInsert + Keys.Tab);

            return this;
        }

        public EmotionalSupportRecordPage ValidateLocationIfOtherVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(locationifother);
            else
                WaitForElementNotVisible(locationifother, 3);

            return this;
        }

        public EmotionalSupportRecordPage ClickWellbeingLink()
        {
            WaitForElementToBeClickable(CarewellbeingidLink);
            Click(CarewellbeingidLink);

            return this;
        }

        public EmotionalSupportRecordPage ValidateWellbeingLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CarewellbeingidLink);
            ValidateElementText(CarewellbeingidLink, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ClickWellbeingClearButton()
        {
            WaitForElementToBeClickable(CarewellbeingidClearButton);
            Click(CarewellbeingidClearButton);

            return this;
        }

        public EmotionalSupportRecordPage ClickWellbeingLookupButton()
        {
            WaitForElementToBeClickable(CarewellbeingidLookupButton);
            Click(CarewellbeingidLookupButton);

            return this;
        }

        public EmotionalSupportRecordPage ValidateActionTakenText(string ExpectedText)
        {
            ValidateElementText(actiontaken, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage InsertTextOnActionTaken(string TextToInsert)
        {
            WaitForElementToBeClickable(actiontaken);
            SendKeys(actiontaken, TextToInsert + Keys.Tab);

            return this;
        }

        public EmotionalSupportRecordPage ValidateActionTakenVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(actiontaken);
            else
                WaitForElementNotVisible(actiontaken, 3);

            return this;
        }

        public EmotionalSupportRecordPage ValidateTotalTimeSpentWithPersonText(string ExpectedText)
        {
            ValidateElementValue(Timespentwithclient, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage InsertTextOnTotalTimeSpentWithPerson(string TextToInsert)
        {
            WaitForElementToBeClickable(Timespentwithclient);
            SendKeys(Timespentwithclient, TextToInsert + Keys.Tab);

            return this;
        }

        public EmotionalSupportRecordPage ValidateAdditionalNotesText(string ExpectedText)
        {
            ValidateElementText(Additionalnotes, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage InsertTextOnAdditionalNotes(string TextToInsert)
        {
            WaitForElementToBeClickable(Additionalnotes);
            SendKeys(Additionalnotes, TextToInsert + Keys.Tab);

            return this;
        }

        public EmotionalSupportRecordPage ClickAssistanceNeededLink()
        {
            WaitForElementToBeClickable(CareassistanceneededidLink);
            Click(CareassistanceneededidLink);

            return this;
        }

        public EmotionalSupportRecordPage ValidateAssistanceNeededLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CareassistanceneededidLink);
            ValidateElementText(CareassistanceneededidLink, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ClickAssistanceNeededClearButton()
        {
            WaitForElementToBeClickable(CareassistanceneededidClearButton);
            Click(CareassistanceneededidClearButton);

            return this;
        }

        public EmotionalSupportRecordPage ClickAssistanceNeededLookupButton()
        {
            WaitForElementToBeClickable(CareassistanceneededidLookupButton);
            Click(CareassistanceneededidLookupButton);

            return this;
        }

        public EmotionalSupportRecordPage SelectAssistanceAmount(string TextToSelect)
        {
            WaitForElementToBeClickable(careassistancelevelid);
            SelectPicklistElementByText(careassistancelevelid, TextToSelect);

            return this;
        }

        public EmotionalSupportRecordPage ValidateAssistanceAmountSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(careassistancelevelid, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ValidateAssistanceAmountVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(careassistancelevelid);
            else
                WaitForElementNotVisible(careassistancelevelid, 3);

            return this;
        }

        public EmotionalSupportRecordPage ValidateLocationErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Location_ErrorLabel, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ValidateLocationErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Location_ErrorLabel);
            else
                WaitForElementNotVisible(Location_ErrorLabel, 3);

            return this;
        }

        public EmotionalSupportRecordPage ValidateLocationIfOtherErrorLabelText(string ExpectedText)
        {
            ValidateElementText(LocationIfOther_ErrorLabel, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ValidateLocationIfOtherErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(LocationIfOther_ErrorLabel);
            else
                WaitForElementNotVisible(LocationIfOther_ErrorLabel, 3);

            return this;
        }

        public EmotionalSupportRecordPage ValidateWellbeingErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Wellbeing_ErrorLabel, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ValidateWellbeingErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Wellbeing_ErrorLabel);
            else
                WaitForElementNotVisible(Wellbeing_ErrorLabel, 3);

            return this;
        }

        public EmotionalSupportRecordPage ValidateActionTakenErrorLabelText(string ExpectedText)
        {
            ValidateElementText(ActionTaken_ErrorLabel, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ValidateActionTakenErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ActionTaken_ErrorLabel);
            else
                WaitForElementNotVisible(ActionTaken_ErrorLabel, 3);

            return this;
        }

        public EmotionalSupportRecordPage ValidateTotalTimeSpentWithPersonErrorLabelText(string ExpectedText)
        {
            ValidateElementText(TotalTimeSpentWithPerson_ErrorLabel, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ValidateTotalTimeSpentWithPersonErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(TotalTimeSpentWithPerson_ErrorLabel);
            else
                WaitForElementNotVisible(TotalTimeSpentWithPerson_ErrorLabel, 3);

            return this;
        }

        public EmotionalSupportRecordPage ValidateAssistanceNeededErrorLabelText(string ExpectedText)
        {
            ValidateElementText(AssistanceNeeded_ErrorLabel, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ValidateAssistanceNeededErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(AssistanceNeeded_ErrorLabel);
            else
                WaitForElementNotVisible(AssistanceNeeded_ErrorLabel, 3);

            return this;
        }

        public EmotionalSupportRecordPage ValidateAssistanceAmountErrorLabelText(string ExpectedText)
        {
            ValidateElementText(AssistanceAmount_ErrorLabel, ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ValidateAssistanceAmountErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(AssistanceAmount_ErrorLabel);
            else
                WaitForElementNotVisible(AssistanceAmount_ErrorLabel, 3);

            return this;
        }

        public EmotionalSupportRecordPage ClickStaffRequired_SelectedElementLink(string ElementId)
        {
            WaitForElementToBeClickable(StaffRequired_SelectedElementLink(ElementId));
            Click(StaffRequired_SelectedElementLink(ElementId));

            return this;
        }

        public EmotionalSupportRecordPage ValidateStaffRequired_SelectedElementLinkTextBeforeSave(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(StaffRequired_SelectedElementLinkBeforeSave(ElementId, ExpectedText));

            return this;
        }

        public EmotionalSupportRecordPage ValidateStaffRequired_SelectedElementLinkTextBeforeSave(Guid ElementId, string ExpectedText)
        {
            return ValidateStaffRequired_SelectedElementLinkTextBeforeSave(ElementId.ToString(), ExpectedText);
        }

        public EmotionalSupportRecordPage ValidateStaffRequired_SelectedElementLinkText(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(StaffRequired_SelectedElementLink(ElementId));
            ValidateElementText(StaffRequired_SelectedElementLink(ElementId), ExpectedText);

            return this;
        }

        public EmotionalSupportRecordPage ValidateStaffRequired_SelectedElementLinkText(Guid ElementId, string ExpectedText)
        {
            return ValidateStaffRequired_SelectedElementLinkText(ElementId.ToString(), ExpectedText);
        }

        public EmotionalSupportRecordPage ClickStaffRequired_SelectedElementRemoveButton(string ElementId)
        {
            WaitForElementToBeClickable(StaffRequired_SelectedElementRemoveButton(ElementId));
            Click(StaffRequired_SelectedElementRemoveButton(ElementId));

            return this;
        }

        public EmotionalSupportRecordPage ClickStaffRequiredLookupButton()
        {
            WaitForElementToBeClickable(staffrequiredLookupButton);
            Click(staffrequiredLookupButton);

            return this;
        }

        public EmotionalSupportRecordPage ValidateCareNoteText(string ExpectedText)
        {
            var elementText = this.GetElementValueByJavascript("CWField_carenote");
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public EmotionalSupportRecordPage InsertTextOnCareNote(string TextToInsert)
        {
            WaitForElementToBeClickable(Carenote);
            SendKeys(Carenote, TextToInsert + Keys.Tab);
            return this;
        }

        public EmotionalSupportRecordPage ClickinkedActivitiesOfDailyLivingLookupButton()
        {
            WaitForElementToBeClickable(LinkedadlcategoriesidLookupButton);
            Click(LinkedadlcategoriesidLookupButton);

            return this;
        }

        public EmotionalSupportRecordPage ClickIncludeInNextHandover_YesRadioButton()
        {
            WaitForElementToBeClickable(Isincludeinnexthandover_1);
            Click(Isincludeinnexthandover_1);

            return this;
        }

        public EmotionalSupportRecordPage ValidateIncludeInNextHandover_YesRadioButtonChecked()
        {
            WaitForElement(Isincludeinnexthandover_1);
            ValidateElementChecked(Isincludeinnexthandover_1);

            return this;
        }

        public EmotionalSupportRecordPage ValidateIncludeInNextHandover_YesRadioButtonNotChecked()
        {
            WaitForElement(Isincludeinnexthandover_1);
            ValidateElementNotChecked(Isincludeinnexthandover_1);

            return this;
        }

        public EmotionalSupportRecordPage ClickIncludeInNextHandover_NoRadioButton()
        {
            WaitForElementToBeClickable(Isincludeinnexthandover_0);
            Click(Isincludeinnexthandover_0);

            return this;
        }

        public EmotionalSupportRecordPage ValidateIncludeInNextHandover_NoRadioButtonChecked()
        {
            WaitForElement(Isincludeinnexthandover_0);
            ValidateElementChecked(Isincludeinnexthandover_0);

            return this;
        }

        public EmotionalSupportRecordPage ValidateIncludeInNextHandover_NoRadioButtonNotChecked()
        {
            WaitForElement(Isincludeinnexthandover_0);
            ValidateElementNotChecked(Isincludeinnexthandover_0);

            return this;
        }

        public EmotionalSupportRecordPage ClickFlagRecordForHandover_YesRadioButton()
        {
            WaitForElementToBeClickable(Flagrecordforhandover_1);
            Click(Flagrecordforhandover_1);

            return this;
        }

        public EmotionalSupportRecordPage ValidateFlagRecordForHandover_YesRadioButtonChecked()
        {
            WaitForElement(Flagrecordforhandover_1);
            ValidateElementChecked(Flagrecordforhandover_1);

            return this;
        }

        public EmotionalSupportRecordPage ValidateFlagRecordForHandover_YesRadioButtonNotChecked()
        {
            WaitForElement(Flagrecordforhandover_1);
            ValidateElementNotChecked(Flagrecordforhandover_1);

            return this;
        }

        public EmotionalSupportRecordPage ClickFlagRecordForHandover_NoRadioButton()
        {
            WaitForElementToBeClickable(Flagrecordforhandover_0);
            Click(Flagrecordforhandover_0);

            return this;
        }

        public EmotionalSupportRecordPage ValidateFlagRecordForHandover_NoRadioButtonChecked()
        {
            WaitForElement(Flagrecordforhandover_0);
            ValidateElementChecked(Flagrecordforhandover_0);

            return this;
        }

        public EmotionalSupportRecordPage ValidateFlagRecordForHandover_NoRadioButtonNotChecked()
        {
            WaitForElement(Flagrecordforhandover_0);
            ValidateElementNotChecked(Flagrecordforhandover_0);

            return this;
        }

    }
}


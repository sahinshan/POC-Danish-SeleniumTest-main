using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonalSafetyAndEnvironmentRecordPage : CommonMethods
    {
        public PersonalSafetyAndEnvironmentRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cppersonpersonalsafetyandenvironment&')]");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
        readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
        readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Personal Safety and Environment: ']");

        readonly By TopPageNotification = By.XPath("//*[@id='CWNotificationMessage_DataForm']");

        #region General section

        readonly By PersonLink = By.XPath("//*[@id='CWField_personid_Link']");
        readonly By PersonLookupButton = By.XPath("//*[@id='CWLookupBtn_personid']");
        readonly By Preferences = By.XPath("//*[@id='CWField_preferences']");
        readonly By CareConsentGiven = By.XPath("//*[@id='CWField_careconsentgivenid']");
        readonly By CareConsentGiven_ErrorLabel = By.XPath("//*[@id='CWControlHolder_careconsentgivenid']/label/span");
        readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By Occurred = By.XPath("//*[@id='CWField_occurred']");
        readonly By OccurredDatePicker = By.XPath("//*[@id='CWField_occurred_DatePicker']");
        readonly By Occurred_Time = By.XPath("//*[@id='CWField_occurred_Time']");
        readonly By Occurred_Time_TimePicker = By.XPath("//*[@id='CWField_occurred_Time_TimePicker']");
        readonly By CreatedOn = By.XPath("//*[@id='CWField_createdon']");
        readonly By CreatedOnDatePicker = By.XPath("//*[@id='CWField_createdon_DatePicker']");
        readonly By CreatedOn_Time = By.XPath("//*[@id='CWField_createdon_Time']");
        readonly By CreatedOn_Time_TimePicker = By.XPath("//*[@id='CWField_createdon_Time_TimePicker']");

        readonly By CareNonConsent = By.XPath("//*[@id='CWField_carenonconsentid']");
        readonly By CareNonConsent_ErrorLabel = By.XPath("//*[@id='CWControlHolder_carenonconsentid']/label/span");

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

        #region Call Bell

        readonly By Bellswitchedon_1 = By.XPath("//*[@id='CWField_bellswitchedon_1']");
        readonly By Bellswitchedon_0 = By.XPath("//*[@id='CWField_bellswitchedon_0']");
        readonly By Bellworking_1 = By.XPath("//*[@id='CWField_bellworking_1']");
        readonly By Bellworking_0 = By.XPath("//*[@id='CWField_bellworking_0']");
        readonly By Bellcorrectposition_1 = By.XPath("//*[@id='CWField_bellcorrectposition_1']");
        readonly By Bellcorrectposition_0 = By.XPath("//*[@id='CWField_bellcorrectposition_0']");

        #endregion

        #region Motion Sensor

        readonly By MotionsensortypeidLookupButton = By.XPath("//*[@id='CWLookupBtn_motionsensortypeid']");
        //Motionsensor type clear button
        readonly By MotionsensortypeidClearButton = By.XPath("//*[@id='CWClearLookup_motionsensortypeid']");
        readonly By MotionsensortypeidLink = By.XPath("//*[@id='CWField_motionsensortypeid_Link']");
        readonly By Motionsensorswitchedon_1 = By.XPath("//*[@id='CWField_motionsensorswitchedon_1']");
        readonly By Motionsensorswitchedon_0 = By.XPath("//*[@id='CWField_motionsensorswitchedon_0']");
        readonly By Motionsensorworking_1 = By.XPath("//*[@id='CWField_motionsensorworking_1']");
        readonly By Motionsensorworking_0 = By.XPath("//*[@id='CWField_motionsensorworking_0']");
        readonly By Motionsensorcorrectposition_1 = By.XPath("//*[@id='CWField_motionsensorcorrectposition_1']");
        readonly By Motionsensorcorrectposition_0 = By.XPath("//*[@id='CWField_motionsensorcorrectposition_0']");

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

        //Staff Required Required Error Label
        readonly By StaffRequiredRequired_ErrorLabel = By.XPath("//*[@id='CWControlHolder_staffrequired']/label/span");

        #endregion

        #region Care Note

        readonly By Carenote = By.XPath("//*[@id='CWField_carenote']");

        #endregion

        #region Care Needs

        readonly By LinkedadlcategoriesidLookupButton = By.XPath("//*[@id='CWLookupBtn_linkedadlcategoriesid']");
        By LinkedAdl_SelectedElementLink(string ElementId) => By.XPath("//*[@id='MS_linkedadlcategoriesid_" + ElementId + "']/a[@id='" + ElementId + "_Link']");
        By LinkedAdl_SelectedElementRemoveButton(string ElementId) => By.XPath("//*[@id='MS_linkedadlcategoriesid_" + ElementId + "']/a[text()='Remove']");

        #endregion

        #region Handover

        readonly By Isincludeinnexthandover_1 = By.XPath("//*[@id='CWField_includeinnexthandover_1']");
        readonly By Isincludeinnexthandover_0 = By.XPath("//*[@id='CWField_includeinnexthandover_0']");
        readonly By Flagrecordforhandover_1 = By.XPath("//*[@id='CWField_flagrecordforhandover_1']");
        readonly By Flagrecordforhandover_0 = By.XPath("//*[@id='CWField_flagrecordforhandover_0']");

        #endregion

        #region Field Labels

        By FieldLabel(string FieldName) => By.XPath("//*[starts-with(@id, 'CWLabelHolder_')]/*[text() = '" + FieldName + "']");
        By MandatoryField_Label(string FieldName) => By.XPath("//label[text()='" + FieldName + "']/span[@class='mandatory']");

        #endregion

        public PersonalSafetyAndEnvironmentRecordPage WaitForPageToLoad()
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


        public PersonalSafetyAndEnvironmentRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickDeleteRecordButton()
        {
            WaitForElement(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }


        public PersonalSafetyAndEnvironmentRecordPage ValidateTopPageNotificationText(string ExpectedText)
        {
            ValidateElementText(TopPageNotification, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateTopPageNotificationVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(TopPageNotification);
            else
                WaitForElementNotVisible(TopPageNotification, 3);

            return this;
        }


        public PersonalSafetyAndEnvironmentRecordPage ClickPersonLink()
        {
            WaitForElementToBeClickable(PersonLink);
            Click(PersonLink);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidatePersonLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(PersonLink);
            ValidateElementText(PersonLink, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickPersonLookupButton()
        {
            WaitForElementToBeClickable(PersonLookupButton);
            Click(PersonLookupButton);

            return this;
        }

        //verify Person lookup button is visible
        public PersonalSafetyAndEnvironmentRecordPage ValidatePersonLookupButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(PersonLookupButton);
            else
                WaitForElementNotVisible(PersonLookupButton, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidatePreferencesText(string ExpectedText)
        {
            WaitForElementVisible(Preferences);
            var elementValue = this.GetElementValueByJavascript("CWField_preferences");
            Assert.AreEqual(ExpectedText, elementValue);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage InsertTextOnPreferences(string TextToInsert)
        {
            WaitForElementToBeClickable(Preferences);
            SendKeys(Preferences, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage SelectNonConsentDetail(string TextToSelect)
        {
            WaitForElementToBeClickable(CareNonConsent);
            SelectPicklistElementByText(CareNonConsent, TextToSelect);

            return this;
        }

        //verify CareNonConsent picklist is visible
        public PersonalSafetyAndEnvironmentRecordPage ValidateNonConsentDetailVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(CareNonConsent);
            else
                WaitForElementNotVisible(CareNonConsent, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateNonConsentDetailSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(CareNonConsent, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateNonConsentDetailErrorLabelText(string ExpectedText)
        {
            ValidateElementText(CareNonConsent_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateNonConsentDetailErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(CareNonConsent_ErrorLabel);
            else
                WaitForElementNotVisible(CareNonConsent_ErrorLabel, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateReasonForAbsenceText(string ExpectedText)
        {
            ValidateElementText(Reasonforabsence, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage InsertTextOnReasonForAbsence(string TextToInsert)
        {
            WaitForElementToBeClickable(Reasonforabsence);
            SendKeys(Reasonforabsence, TextToInsert + Keys.Tab);

            return this;
        }

        //verify reasonforabsence text area maxlength attribute
        public PersonalSafetyAndEnvironmentRecordPage ValidateReasonForAbsenceMaxLength(string ExpectedText)
        {
            ValidateElementAttribute(Reasonforabsence, "maxlength", ExpectedText);

            return this;
        }

        //verify reason for absence field is visible
        public PersonalSafetyAndEnvironmentRecordPage ValidateReasonForAbsenceVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Reasonforabsence);
            else
                WaitForElementNotVisible(Reasonforabsence, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateReasonForAbsenceErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Reasonforabsence_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateReasonForAbsenceErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Reasonforabsence_ErrorLabel);
            else
                WaitForElementNotVisible(Reasonforabsence_ErrorLabel, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateReasonConsentDeclinedText(string ExpectedText)
        {
            ValidateElementText(Reasonconsentdeclined, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage InsertTextOnReasonConsentDeclined(string TextToInsert)
        {
            WaitForElementToBeClickable(Reasonconsentdeclined);
            SendKeys(Reasonconsentdeclined, TextToInsert + Keys.Tab);

            return this;
        }

        //verify Reasonconsentdeclined text area maxlength attribute
        public PersonalSafetyAndEnvironmentRecordPage ValidateReasonConsentDeclinedMaxLength(string ExpectedText)
        {
            ValidateElementAttribute(Reasonconsentdeclined, "maxlength", ExpectedText);

            return this;
        }

        //verify reason consent declined field is visible
        public PersonalSafetyAndEnvironmentRecordPage ValidateReasonConsentDeclinedVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Reasonconsentdeclined);
            else
                WaitForElementNotVisible(Reasonconsentdeclined, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateReasonConsentDeclinedErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Reasonconsentdeclined_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateReasonConsentDeclinedErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Reasonconsentdeclined_ErrorLabel);
            else
                WaitForElementNotVisible(Reasonconsentdeclined_ErrorLabel, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateEncouragementGivenText(string ExpectedText)
        {
            ValidateElementText(Encouragementgiven, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage InsertTextOnEncouragementGiven(string TextToInsert)
        {
            WaitForElementToBeClickable(Encouragementgiven);
            SendKeys(Encouragementgiven, TextToInsert + Keys.Tab);

            return this;
        }

        //verify Encouragementgiven text area maxlength attribute
        public PersonalSafetyAndEnvironmentRecordPage ValidateEncouragementGivenMaxLength(string ExpectedText)
        {
            ValidateElementAttribute(Encouragementgiven, "maxlength", ExpectedText);

            return this;
        }

        //verify encouragement given field is visible
        public PersonalSafetyAndEnvironmentRecordPage ValidateEncouragementGivenVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Encouragementgiven);
            else
                WaitForElementNotVisible(Encouragementgiven, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateEncouragementGivenErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Encouragementgiven_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateEncouragementGivenErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Encouragementgiven_ErrorLabel);
            else
                WaitForElementNotVisible(Encouragementgiven_ErrorLabel, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickCareProvidedWithoutConsent_YesRadioButton()
        {
            WaitForElementToBeClickable(Careprovidedwithoutconsent_1);
            Click(Careprovidedwithoutconsent_1);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateCareProvidedWithoutConsent_YesRadioButtonChecked()
        {
            WaitForElement(Careprovidedwithoutconsent_1);
            ValidateElementChecked(Careprovidedwithoutconsent_1);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateCareProvidedWithoutConsent_YesRadioButtonNotChecked()
        {
            WaitForElement(Careprovidedwithoutconsent_1);
            ValidateElementNotChecked(Careprovidedwithoutconsent_1);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickCareProvidedWithoutConsent_NoRadioButton()
        {
            WaitForElementToBeClickable(Careprovidedwithoutconsent_0);
            Click(Careprovidedwithoutconsent_0);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateCareProvidedWithoutConsent_NoRadioButtonChecked()
        {
            WaitForElement(Careprovidedwithoutconsent_0);
            ValidateElementChecked(Careprovidedwithoutconsent_0);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateCareProvidedWithoutConsent_NoRadioButtonNotChecked()
        {
            WaitForElement(Careprovidedwithoutconsent_0);
            ValidateElementNotChecked(Careprovidedwithoutconsent_0);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateDeferredToDateText(string ExpectedText)
        {
            ValidateElementValue(Deferredtodate, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage InsertDeferredToDate(string TextToInsert)
        {
            WaitForElementToBeClickable(Deferredtodate);
            SendKeys(Deferredtodate, TextToInsert + Keys.Tab);

            return this;
        }

        //verify Deferredtodate field is visible
        public PersonalSafetyAndEnvironmentRecordPage ValidateDeferredToDateVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Deferredtodate);
            else
                WaitForElementNotVisible(Deferredtodate, 3);

            return this;
        }


        public PersonalSafetyAndEnvironmentRecordPage ValidateDeferredToDateErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Deferredtodate_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateDeferredToDateErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Deferredtodate_ErrorLabel);
            else
                WaitForElementNotVisible(Deferredtodate_ErrorLabel, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage SelectDeferredToTimeOrShift(string TextToSelect)
        {
            WaitForElementToBeClickable(Timeorshiftid);
            SelectPicklistElementByText(Timeorshiftid, TextToSelect);

            return this;
        }

        //verify deferred to time or shift picklist is visible
        public PersonalSafetyAndEnvironmentRecordPage ValidateDeferredToTimeOrShiftVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Timeorshiftid);
            else
                WaitForElementNotVisible(Timeorshiftid, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateDeferredToTimeOrShiftSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Timeorshiftid, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateDeferredToTimeOrShiftErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Timeorshiftid_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateDeferredToTimeOrShiftErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Timeorshiftid_ErrorLabel);
            else
                WaitForElementNotVisible(Timeorshiftid_ErrorLabel, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateDeferredToTimeText(string ExpectedText)
        {
            ValidateElementValue(Deferredtotime, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage InsertDeferredToTime(string TextToInsert)
        {
            WaitForElementToBeClickable(Deferredtotime);
            SendKeys(Deferredtotime, TextToInsert + Keys.Tab);

            return this;
        }

        //verify Deferredtotime field is visible
        public PersonalSafetyAndEnvironmentRecordPage ValidateDeferredToTimeVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Deferredtotime);
            else
                WaitForElementNotVisible(Deferredtotime, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateDeferredToTimeErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Deferredtotime_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateDeferredToTimeErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Deferredtotime_ErrorLabel);
            else
                WaitForElementNotVisible(Deferredtotime_ErrorLabel, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickDeferredToShiftLink()
        {
            WaitForElementToBeClickable(DeferredtoselectedshiftidLink);
            Click(DeferredtoselectedshiftidLink);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateDeferredToShiftLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(DeferredtoselectedshiftidLink);
            ValidateElementText(DeferredtoselectedshiftidLink, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickDeferredToShiftLookupButton()
        {
            WaitForElementToBeClickable(DeferredtoselectedshiftidLookupButton);
            Click(DeferredtoselectedshiftidLookupButton);

            return this;
        }

        //verify Deferred to shift lookup button is visible
        public PersonalSafetyAndEnvironmentRecordPage ValidateDeferredToShiftLookupButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(DeferredtoselectedshiftidLookupButton);
            else
                WaitForElementNotVisible(DeferredtoselectedshiftidLookupButton, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateDeferredToShiftErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Deferredtoselectedshiftid_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateDeferredToShiftErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Deferredtoselectedshiftid_ErrorLabel);
            else
                WaitForElementNotVisible(Deferredtoselectedshiftid_ErrorLabel, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage SelectConsentGiven(string TextToSelect)
        {
            WaitForElementToBeClickable(CareConsentGiven);
            SelectPicklistElementByText(CareConsentGiven, TextToSelect);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateConsentGivenSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(CareConsentGiven, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateConsentGivenErrorLabelText(string ExpectedText)
        {
            ValidateElementText(CareConsentGiven_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateConsentGivenErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(CareConsentGiven_ErrorLabel);
            else
                WaitForElementNotVisible(CareConsentGiven_ErrorLabel, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickResponsibleTeamLink()
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            Click(ResponsibleTeamLink);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            ValidateElementText(ResponsibleTeamLink, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamLookupButton);
            Click(ResponsibleTeamLookupButton);

            return this;
        }

        //verify Responsible Team lookup button is visible
        public PersonalSafetyAndEnvironmentRecordPage ValidateResponsibleTeamLookupButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ResponsibleTeamLookupButton);
            else
                WaitForElementNotVisible(ResponsibleTeamLookupButton, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateDateAndTimeOccurred_DateText(string ExpectedText)
        {
            ValidateElementValue(Occurred, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage InsertTextOnDateAndTimeOccurred_Date(string TextToInsert)
        {
            WaitForElementToBeClickable(Occurred);
            SendKeys(Occurred, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickDateAndTimeOccurredDatePicker()
        {
            WaitForElementToBeClickable(OccurredDatePicker);
            Click(OccurredDatePicker);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateDateAndTimeOccurred_TimeText(string ExpectedText)
        {
            ValidateElementValue(Occurred_Time, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage InsertTextOnDateAndTimeOccurred_Time(string TextToInsert)
        {
            WaitForElementToBeClickable(Occurred_Time);
            SendKeys(Occurred_Time, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickDateAndTimeOccurred_Time_TimePicker()
        {
            WaitForElementToBeClickable(Occurred_Time_TimePicker);
            Click(Occurred_Time_TimePicker);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateCreatedOnText(string ExpectedText)
        {
            ValidateElementValue(CreatedOn, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage InsertTextOnCreatedOn(string TextToInsert)
        {
            WaitForElementToBeClickable(CreatedOn);
            SendKeys(CreatedOn, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickCreatedOnDatePicker()
        {
            WaitForElementToBeClickable(CreatedOnDatePicker);
            Click(CreatedOnDatePicker);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateCreatedOn_TimeText(string ExpectedText)
        {
            ValidateElementValue(CreatedOn_Time, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage InsertTextOnCreatedOn_Time(string TextToInsert)
        {
            WaitForElementToBeClickable(CreatedOn_Time);
            SendKeys(CreatedOn_Time, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickCreatedOn_Time_TimePicker()
        {
            WaitForElementToBeClickable(CreatedOn_Time_TimePicker);
            Click(CreatedOn_Time_TimePicker);

            return this;
        }

        //click Bellswitchedon_1 radio button
        public PersonalSafetyAndEnvironmentRecordPage ClickBellswitchedon_YesRadioButton()
        {
            WaitForElementToBeClickable(Bellswitchedon_1);
            Click(Bellswitchedon_1);

            return this;
        }

        //click Bellswitchedon_0 radio button
        public PersonalSafetyAndEnvironmentRecordPage ClickBellswitchedon_NoRadioButton()
        {
            WaitForElementToBeClickable(Bellswitchedon_0);
            Click(Bellswitchedon_0);

            return this;
        }

        //verify Bellswitchedon_1 radio button is checked
        public PersonalSafetyAndEnvironmentRecordPage ValidateBellswitchedon_YesRadioButtonChecked()
        {
            WaitForElement(Bellswitchedon_1);
            ValidateElementChecked(Bellswitchedon_1);

            return this;
        }

        //verify Bellswitchedon_1 radio button is not checked
        public PersonalSafetyAndEnvironmentRecordPage ValidateBellswitchedon_YesRadioButtonNotChecked()
        {
            WaitForElement(Bellswitchedon_1);
            ValidateElementNotChecked(Bellswitchedon_1);

            return this;
        }

        //verify Bellswitchedon_0 radio button is checked
        public PersonalSafetyAndEnvironmentRecordPage ValidateBellswitchedon_NoRadioButtonChecked()
        {
            WaitForElement(Bellswitchedon_0);
            ValidateElementChecked(Bellswitchedon_0);

            return this;
        }

        //verify Bellswitchedon_0 radio button is not checked
        public PersonalSafetyAndEnvironmentRecordPage ValidateBellswitchedon_NoRadioButtonNotChecked()
        {
            WaitForElement(Bellswitchedon_0);
            ValidateElementNotChecked(Bellswitchedon_0);

            return this;
        }

        //verify Bellswitchedon_1 and Bellswitchedon_0 options are visible or not visible
        public PersonalSafetyAndEnvironmentRecordPage ValidateBellSwitchedOnRadioButtonsVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Bellswitchedon_1);
                WaitForElementVisible(Bellswitchedon_0);
            }
            else
            {
                WaitForElementNotVisible(Bellswitchedon_1, 3);
                WaitForElementNotVisible(Bellswitchedon_0, 3);
            }

            return this;
        }

        //verify Bellswitchedon_1 and Bellswitchedon_0 options are enabled or disabled
        public PersonalSafetyAndEnvironmentRecordPage ValidateBellSwitchedOnRadioButtonsEnabled(bool ExpectEnabled)
        {
            if (ExpectEnabled)
            {
                ValidateElementEnabled(Bellswitchedon_1);
                ValidateElementEnabled(Bellswitchedon_0);
            }
            else
            {
                ValidateElementDisabled(Bellswitchedon_1);
                ValidateElementDisabled(Bellswitchedon_0);
            }

            return this;
        }

        //click Bellworking_1 radio button
        public PersonalSafetyAndEnvironmentRecordPage ClickBellworking_YesRadioButton()
        {
            WaitForElementToBeClickable(Bellworking_1);
            Click(Bellworking_1);

            return this;
        }

        //click Bellworking_0 radio button
        public PersonalSafetyAndEnvironmentRecordPage ClickBellworking_NoRadioButton()
        {
            WaitForElementToBeClickable(Bellworking_0);
            Click(Bellworking_0);

            return this;
        }

        //verify Bellworking_1 radio button is checked
        public PersonalSafetyAndEnvironmentRecordPage ValidateBellworking_YesRadioButtonChecked()
        {
            WaitForElement(Bellworking_1);
            ValidateElementChecked(Bellworking_1);

            return this;
        }

        //verify Bellworking_1 radio button is not checked
        public PersonalSafetyAndEnvironmentRecordPage ValidateBellworking_YesRadioButtonNotChecked()
        {
            WaitForElement(Bellworking_1);
            ValidateElementNotChecked(Bellworking_1);

            return this;
        }

        //verify Bellworking_0 radio button is checked
        public PersonalSafetyAndEnvironmentRecordPage ValidateBellworking_NoRadioButtonChecked()
        {
            WaitForElement(Bellworking_0);
            ValidateElementChecked(Bellworking_0);

            return this;
        }

        //verify Bellworking_0 radio button is not checked
        public PersonalSafetyAndEnvironmentRecordPage ValidateBellworking_NoRadioButtonNotChecked()
        {
            WaitForElement(Bellworking_0);
            ValidateElementNotChecked(Bellworking_0);

            return this;
        }

        //verify Bellworking_1 and Bellworking_0 options are visible or not visible
        public PersonalSafetyAndEnvironmentRecordPage ValidateBellWorkingRadioButtonsVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Bellworking_1);
                WaitForElementVisible(Bellworking_0);
            }
            else
            {
                WaitForElementNotVisible(Bellworking_1, 3);
                WaitForElementNotVisible(Bellworking_0, 3);
            }

            return this;
        }

        //verify Bellworking_1 and Bellworking_0 options are enabled or disabled
        public PersonalSafetyAndEnvironmentRecordPage ValidateBellWorkingRadioButtonsEnabled(bool ExpectEnabled)
        {
            if (ExpectEnabled)
            {
                ValidateElementEnabled(Bellworking_1);
                ValidateElementEnabled(Bellworking_0);
            }
            else
            {
                ValidateElementDisabled(Bellworking_1);
                ValidateElementDisabled(Bellworking_0);
            }

            return this;
        }

        //click Bellcorrectposition_1 radio button
        public PersonalSafetyAndEnvironmentRecordPage ClickBellcorrectposition_YesRadioButton()
        {
            WaitForElementToBeClickable(Bellcorrectposition_1);
            Click(Bellcorrectposition_1);

            return this;
        }

        //click Bellcorrectposition_0 radio button
        public PersonalSafetyAndEnvironmentRecordPage ClickBellcorrectposition_NoRadioButton()
        {
            WaitForElementToBeClickable(Bellcorrectposition_0);
            Click(Bellcorrectposition_0);

            return this;
        }

        //verify Bellcorrectposition_1 radio button is checked
        public PersonalSafetyAndEnvironmentRecordPage ValidateBellcorrectposition_YesRadioButtonChecked()
        {
            WaitForElement(Bellcorrectposition_1);
            ValidateElementChecked(Bellcorrectposition_1);

            return this;
        }

        //verify Bellcorrectposition_1 radio button is not checked
        public PersonalSafetyAndEnvironmentRecordPage ValidateBellcorrectposition_YesRadioButtonNotChecked()
        {
            WaitForElement(Bellcorrectposition_1);
            ValidateElementNotChecked(Bellcorrectposition_1);

            return this;
        }

        //verify Bellcorrectposition_0 radio button is checked
        public PersonalSafetyAndEnvironmentRecordPage ValidateBellcorrectposition_NoRadioButtonChecked()
        {
            WaitForElement(Bellcorrectposition_0);
            ValidateElementChecked(Bellcorrectposition_0);

            return this;
        }

        //verify Bellcorrectposition_0 radio button is not checked
        public PersonalSafetyAndEnvironmentRecordPage ValidateBellcorrectposition_NoRadioButtonNotChecked()
        {
            WaitForElement(Bellcorrectposition_0);
            ValidateElementNotChecked(Bellcorrectposition_0);

            return this;
        }

        //verify Bellcorrectposition_1 and Bellcorrectposition_0 options are visible or not visible
        public PersonalSafetyAndEnvironmentRecordPage ValidateBellCorrectPositionRadioButtonsVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Bellcorrectposition_1);
                WaitForElementVisible(Bellcorrectposition_0);
            }
            else
            {
                WaitForElementNotVisible(Bellcorrectposition_1, 3);
                WaitForElementNotVisible(Bellcorrectposition_0, 3);
            }

            return this;
        }

        //verify Bellcorrectposition_1 and Bellcorrectposition_0 options are enabled or disabled
        public PersonalSafetyAndEnvironmentRecordPage ValidateBellCorrectPositionRadioButtonsEnabled(bool ExpectEnabled)
        {
            if (ExpectEnabled)
            {
                ValidateElementEnabled(Bellcorrectposition_1);
                ValidateElementEnabled(Bellcorrectposition_0);
            }
            else
            {
                ValidateElementDisabled(Bellcorrectposition_1);
                ValidateElementDisabled(Bellcorrectposition_0);
            }

            return this;
        }

        //click MotionsensortypeidClearButton
        public PersonalSafetyAndEnvironmentRecordPage ClickMotionsensortypeidClearButton()
        {
            WaitForElementToBeClickable(MotionsensortypeidClearButton);
            Click(MotionsensortypeidClearButton);

            return this;
        }

        //click MotionsensortypeidLookupButton
        public PersonalSafetyAndEnvironmentRecordPage ClickMotionsensortypeLookupButton()
        {
            WaitForElementToBeClickable(MotionsensortypeidLookupButton);
            Click(MotionsensortypeidLookupButton);

            return this;
        }

        //verify motionsensor type lookup button is visible
        public PersonalSafetyAndEnvironmentRecordPage ValidateMotionsensortypeidLookupButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(MotionsensortypeidLookupButton);
            else
                WaitForElementNotVisible(MotionsensortypeidLookupButton, 3);

            return this;
        }   

        //verify MotionsensortypeidLink text
        public PersonalSafetyAndEnvironmentRecordPage ValidateMotionsensortypeidLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(MotionsensortypeidLink);
            ValidateElementText(MotionsensortypeidLink, ExpectedText);

            return this;
        }

        //verify MotionsensortypeidLink visibility
        public PersonalSafetyAndEnvironmentRecordPage ValidateMotionsensortypeidLinkVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(MotionsensortypeidLink);
            else
                WaitForElementNotVisible(MotionsensortypeidLink, 3);

            return this;
        }

        //click Motionsensorswitchedon_1 radio button
        public PersonalSafetyAndEnvironmentRecordPage ClickMotionsensorswitchedon_YesRadioButton()
        {
            WaitForElementToBeClickable(Motionsensorswitchedon_1);
            Click(Motionsensorswitchedon_1);

            return this;
        }

        //click Motionsensorswitchedon_0 radio button
        public PersonalSafetyAndEnvironmentRecordPage ClickMotionsensorswitchedon_NoRadioButton()
        {
            WaitForElementToBeClickable(Motionsensorswitchedon_0);
            Click(Motionsensorswitchedon_0);

            return this;
        }

        //verify Motionsensorswitchedon_1 radio button is checked
        public PersonalSafetyAndEnvironmentRecordPage ValidateMotionsensorswitchedon_YesRadioButtonChecked()
        {
            WaitForElement(Motionsensorswitchedon_1);
            ValidateElementChecked(Motionsensorswitchedon_1);

            return this;
        }

        //verify Motionsensorswitchedon_1 radio button is not checked
        public PersonalSafetyAndEnvironmentRecordPage ValidateMotionsensorswitchedon_YesRadioButtonNotChecked()
        {
            WaitForElement(Motionsensorswitchedon_1);
            ValidateElementNotChecked(Motionsensorswitchedon_1);

            return this;
        }

        //verify Motionsensorswitchedon_0 radio button is checked
        public PersonalSafetyAndEnvironmentRecordPage ValidateMotionsensorswitchedon_NoRadioButtonChecked()
        {
            WaitForElement(Motionsensorswitchedon_0);
            ValidateElementChecked(Motionsensorswitchedon_0);

            return this;
        }

        //verify Motionsensorswitchedon_0 radio button is not checked
        public PersonalSafetyAndEnvironmentRecordPage ValidateMotionsensorswitchedon_NoRadioButtonNotChecked()
        {
            WaitForElement(Motionsensorswitchedon_0);
            ValidateElementNotChecked(Motionsensorswitchedon_0);

            return this;
        }

        //verify Motionsensorswitchedon_1 and Motionsensorswitchedon_0 options are visible or not visible
        public PersonalSafetyAndEnvironmentRecordPage ValidateMotionSensorSwitchedOnRadioButtonsVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Motionsensorswitchedon_1);
                WaitForElementVisible(Motionsensorswitchedon_0);
            }
            else
            {
                WaitForElementNotVisible(Motionsensorswitchedon_1, 3);
                WaitForElementNotVisible(Motionsensorswitchedon_0, 3);
            }

            return this;
        }

        //verify Motionsensorswitchedon_1 and Motionsensorswitchedon_0 options are enabled or disabled
        public PersonalSafetyAndEnvironmentRecordPage ValidateMotionSensorSwitchedOnRadioButtonsEnabled(bool ExpectEnabled)
        {
            if (ExpectEnabled)
            {
                ValidateElementEnabled(Motionsensorswitchedon_1);
                ValidateElementEnabled(Motionsensorswitchedon_0);
            }
            else
            {
                ValidateElementDisabled(Motionsensorswitchedon_1);
                ValidateElementDisabled(Motionsensorswitchedon_0);
            }

            return this;
        }

        //click Motionsensorworking_1 radio button
        public PersonalSafetyAndEnvironmentRecordPage ClickMotionsensorworking_YesRadioButton()
        {
            WaitForElementToBeClickable(Motionsensorworking_1);
            Click(Motionsensorworking_1);

            return this;
        }

        //click Motionsensorworking_0 radio button
        public PersonalSafetyAndEnvironmentRecordPage ClickMotionsensorworking_NoRadioButton()
        {
            WaitForElementToBeClickable(Motionsensorworking_0);
            Click(Motionsensorworking_0);

            return this;
        }

        //verify Motionsensorworking_1 radio button is checked
        public PersonalSafetyAndEnvironmentRecordPage ValidateMotionsensorworking_YesRadioButtonChecked()
        {
            WaitForElement(Motionsensorworking_1);
            ValidateElementChecked(Motionsensorworking_1);

            return this;
        }

        //verify Motionsensorworking_1 radio button is not checked
        public PersonalSafetyAndEnvironmentRecordPage ValidateMotionsensorworking_YesRadioButtonNotChecked()
        {
            WaitForElement(Motionsensorworking_1);
            ValidateElementNotChecked(Motionsensorworking_1);

            return this;
        }

        //verify Motionsensorworking_0 radio button is checked
        public PersonalSafetyAndEnvironmentRecordPage ValidateMotionsensorworking_NoRadioButtonChecked()
        {
            WaitForElement(Motionsensorworking_0);
            ValidateElementChecked(Motionsensorworking_0);

            return this;
        }

        //verify Motionsensorworking_0 radio button is not checked
        public PersonalSafetyAndEnvironmentRecordPage ValidateMotionsensorworking_NoRadioButtonNotChecked()
        {
            WaitForElement(Motionsensorworking_0);
            ValidateElementNotChecked(Motionsensorworking_0);

            return this;
        }

        //verify Motionsensorworking_1 and Motionsensorworking_0 options are visible or not visible
        public PersonalSafetyAndEnvironmentRecordPage ValidateMotionSensorWorkingRadioButtonsVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Motionsensorworking_1);
                WaitForElementVisible(Motionsensorworking_0);
            }
            else
            {
                WaitForElementNotVisible(Motionsensorworking_1, 3);
                WaitForElementNotVisible(Motionsensorworking_0, 3);
            }

            return this;
        }

        //verify Motionsensorworking_1 and Motionsensorworking_0 options are enabled or disabled
        public PersonalSafetyAndEnvironmentRecordPage ValidateMotionSensorWorkingRadioButtonsEnabled(bool ExpectEnabled)
        {
            if (ExpectEnabled)
            {
                ValidateElementEnabled(Motionsensorworking_1);
                ValidateElementEnabled(Motionsensorworking_0);
            }
            else
            {
                ValidateElementDisabled(Motionsensorworking_1);
                ValidateElementDisabled(Motionsensorworking_0);
            }

            return this;
        }

        //click Motionsensorcorrectposition_1 radio button
        public PersonalSafetyAndEnvironmentRecordPage ClickIsTheMotionSensorInTheCorrectPosition_YesRadioButton()
        {
            WaitForElementToBeClickable(Motionsensorcorrectposition_1);
            Click(Motionsensorcorrectposition_1);

            return this;
        }

        //click Motionsensorcorrectposition_0 radio button
        public PersonalSafetyAndEnvironmentRecordPage ClickIsTheMotionSensorInTheCorrectPosition_NoRadioButton()
        {
            WaitForElementToBeClickable(Motionsensorcorrectposition_0);
            Click(Motionsensorcorrectposition_0);

            return this;
        }

        //verify Motionsensorcorrectposition_1 radio button is checked
        public PersonalSafetyAndEnvironmentRecordPage ValidateMotionsensorcorrectposition_YesRadioButtonChecked()
        {
            WaitForElement(Motionsensorcorrectposition_1);
            ValidateElementChecked(Motionsensorcorrectposition_1);

            return this;
        }

        //verify Motionsensorcorrectposition_1 radio button is not checked
        public PersonalSafetyAndEnvironmentRecordPage ValidateMotionsensorcorrectposition_YesRadioButtonNotChecked()
        {
            WaitForElement(Motionsensorcorrectposition_1);
            ValidateElementNotChecked(Motionsensorcorrectposition_1);

            return this;
        }

        //verify Motionsensorcorrectposition_0 radio button is checked
        public PersonalSafetyAndEnvironmentRecordPage ValidateMotionsensorcorrectposition_NoRadioButtonChecked()
        {
            WaitForElement(Motionsensorcorrectposition_0);
            ValidateElementChecked(Motionsensorcorrectposition_0);

            return this;
        }

        //verify Motionsensorcorrectposition_0 radio button is not checked
        public PersonalSafetyAndEnvironmentRecordPage ValidateMotionsensorcorrectposition_NoRadioButtonNotChecked()
        {
            WaitForElement(Motionsensorcorrectposition_0);
            ValidateElementNotChecked(Motionsensorcorrectposition_0);

            return this;
        }

        //verify Motionsensorcorrectposition_1 and Motionsensorcorrectposition_0 options are visible or not visible
        public PersonalSafetyAndEnvironmentRecordPage ValidateMotionSensorCorrectPositionRadioButtonsVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Motionsensorcorrectposition_1);
                WaitForElementVisible(Motionsensorcorrectposition_0);
            }
            else
            {
                WaitForElementNotVisible(Motionsensorcorrectposition_1, 3);
                WaitForElementNotVisible(Motionsensorcorrectposition_0, 3);
            }

            return this;
        }

        //verify Motionsensorcorrectposition_1 and Motionsensorcorrectposition_0 options are enabled or disabled
        public PersonalSafetyAndEnvironmentRecordPage ValidateMotionSensorCorrectPositionRadioButtonsEnabled(bool ExpectEnabled)
        {
            if (ExpectEnabled)
            {
                ValidateElementEnabled(Motionsensorcorrectposition_1);
                ValidateElementEnabled(Motionsensorcorrectposition_0);
            }
            else
            {
                ValidateElementDisabled(Motionsensorcorrectposition_1);
                ValidateElementDisabled(Motionsensorcorrectposition_0);
            }

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickLocation_SelectedElementLink(string ElementId)
        {
            WaitForElementToBeClickable(Location_SelectedElementLink(ElementId));
            Click(Location_SelectedElementLink(ElementId));

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateLocation_SelectedElementLinkText(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(Location_SelectedElementLink(ElementId));
            ValidateElementText(Location_SelectedElementLink(ElementId), ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateLocation_SelectedElementLinkText(Guid ElementId, string ExpectedText)
        {
            return ValidateLocation_SelectedElementLinkText(ElementId.ToString(), ExpectedText);
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickLocation_SelectedElementRemoveButton(string ElementId)
        {
            WaitForElementToBeClickable(Location_SelectedElementRemoveButton(ElementId));
            Click(Location_SelectedElementRemoveButton(ElementId));

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickLocationLookupButton()
        {
            WaitForElementToBeClickable(CarephysicallocationidLookupButton);
            Click(CarephysicallocationidLookupButton);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateLocationIfOtherText(string ExpectedText)
        {
            ValidateElementText(locationifother, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage InsertTextOnLocationIfOther(string TextToInsert)
        {
            WaitForElementToBeClickable(locationifother);
            SendKeys(locationifother, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateLocationIfOtherVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(locationifother);
            else
                WaitForElementNotVisible(locationifother, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickWellbeingLink()
        {
            WaitForElementToBeClickable(CarewellbeingidLink);
            Click(CarewellbeingidLink);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateWellbeingLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CarewellbeingidLink);
            ValidateElementText(CarewellbeingidLink, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickWellbeingClearButton()
        {
            WaitForElementToBeClickable(CarewellbeingidClearButton);
            Click(CarewellbeingidClearButton);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickWellbeingLookupButton()
        {
            WaitForElementToBeClickable(CarewellbeingidLookupButton);
            Click(CarewellbeingidLookupButton);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateActionTakenText(string ExpectedText)
        {
            ValidateElementText(actiontaken, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage InsertTextOnActionTaken(string TextToInsert)
        {
            WaitForElementToBeClickable(actiontaken);
            SendKeys(actiontaken, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateActionTakenVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(actiontaken);
            else
                WaitForElementNotVisible(actiontaken, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateTotalTimeSpentWithPersonText(string ExpectedText)
        {
            ValidateElementValue(Timespentwithclient, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage InsertTextOnTotalTimeSpentWithPerson(string TextToInsert)
        {
            WaitForElementToBeClickable(Timespentwithclient);
            SendKeys(Timespentwithclient, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateTotalTimeSpentWithPersonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Timespentwithclient);
            else
                WaitForElementNotVisible(Timespentwithclient, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateAdditionalNotesText(string ExpectedText)
        {
            ValidateElementText(Additionalnotes, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage InsertTextOnAdditionalNotes(string TextToInsert)
        {
            WaitForElementToBeClickable(Additionalnotes);
            SendKeys(Additionalnotes, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickAssistanceNeededLink()
        {
            WaitForElementToBeClickable(CareassistanceneededidLink);
            Click(CareassistanceneededidLink);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateAssistanceNeededLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CareassistanceneededidLink);
            ValidateElementText(CareassistanceneededidLink, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickAssistanceNeededClearButton()
        {
            WaitForElementToBeClickable(CareassistanceneededidClearButton);
            Click(CareassistanceneededidClearButton);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickAssistanceNeededLookupButton()
        {
            WaitForElementToBeClickable(CareassistanceneededidLookupButton);
            Click(CareassistanceneededidLookupButton);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage SelectAssistanceAmount(string TextToSelect)
        {
            WaitForElementToBeClickable(careassistancelevelid);
            SelectPicklistElementByText(careassistancelevelid, TextToSelect);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateAssistanceAmountSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(careassistancelevelid, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateAssistanceAmountVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(careassistancelevelid);
            else
                WaitForElementNotVisible(careassistancelevelid, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateLocationErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Location_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateLocationErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Location_ErrorLabel);
            else
                WaitForElementNotVisible(Location_ErrorLabel, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateLocationIfOtherErrorLabelText(string ExpectedText)
        {
            ValidateElementText(LocationIfOther_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateLocationIfOtherErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(LocationIfOther_ErrorLabel);
            else
                WaitForElementNotVisible(LocationIfOther_ErrorLabel, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateWellbeingErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Wellbeing_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateWellbeingErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Wellbeing_ErrorLabel);
            else
                WaitForElementNotVisible(Wellbeing_ErrorLabel, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateActionTakenErrorLabelText(string ExpectedText)
        {
            ValidateElementText(ActionTaken_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateActionTakenErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ActionTaken_ErrorLabel);
            else
                WaitForElementNotVisible(ActionTaken_ErrorLabel, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateTotalTimeSpentWithPersonErrorLabelText(string ExpectedText)
        {
            ValidateElementText(TotalTimeSpentWithPerson_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateTotalTimeSpentWithPersonErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(TotalTimeSpentWithPerson_ErrorLabel);
            else
                WaitForElementNotVisible(TotalTimeSpentWithPerson_ErrorLabel, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateAssistanceNeededErrorLabelText(string ExpectedText)
        {
            ValidateElementText(AssistanceNeeded_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateAssistanceNeededErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(AssistanceNeeded_ErrorLabel);
            else
                WaitForElementNotVisible(AssistanceNeeded_ErrorLabel, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateAssistanceAmountErrorLabelText(string ExpectedText)
        {
            ValidateElementText(AssistanceAmount_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateAssistanceAmountErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(AssistanceAmount_ErrorLabel);
            else
                WaitForElementNotVisible(AssistanceAmount_ErrorLabel, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickStaffRequired_SelectedElementLink(string ElementId)
        {
            WaitForElementToBeClickable(StaffRequired_SelectedElementLink(ElementId));
            Click(StaffRequired_SelectedElementLink(ElementId));

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateStaffRequired_SelectedElementLinkTextBeforeSave(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(StaffRequired_SelectedElementLinkBeforeSave(ElementId, ExpectedText));

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateStaffRequired_SelectedElementLinkTextBeforeSave(Guid ElementId, string ExpectedText)
        {
            return ValidateStaffRequired_SelectedElementLinkTextBeforeSave(ElementId.ToString(), ExpectedText);
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateStaffRequired_SelectedElementLinkText(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(StaffRequired_SelectedElementLink(ElementId));
            ValidateElementText(StaffRequired_SelectedElementLink(ElementId), ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateStaffRequired_SelectedElementLinkText(Guid ElementId, string ExpectedText)
        {
            return ValidateStaffRequired_SelectedElementLinkText(ElementId.ToString(), ExpectedText);
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickStaffRequired_SelectedElementRemoveButton(string ElementId)
        {
            WaitForElementToBeClickable(StaffRequired_SelectedElementRemoveButton(ElementId));
            Click(StaffRequired_SelectedElementRemoveButton(ElementId));

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickStaffRequiredLookupButton()
        {
            WaitForElementToBeClickable(staffrequiredLookupButton);
            Click(staffrequiredLookupButton);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateStaffRequiredErrorLabelText(string ExpectedText)
        {
            ValidateElementText(StaffRequiredRequired_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateStaffRequiredErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(StaffRequiredRequired_ErrorLabel);
            else
                WaitForElementNotVisible(StaffRequiredRequired_ErrorLabel, 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateCareNoteText(string ExpectedText)
        {
            var elementText = this.GetElementValueByJavascript("CWField_carenote");
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage InsertTextOnCareNote(string TextToInsert)
        {
            WaitForElementToBeClickable(Carenote);
            SendKeys(Carenote, TextToInsert + Keys.Tab);
            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickLinkedActivitiesOfDailyLivingLookupButton()
        {
            WaitForElement(LinkedadlcategoriesidLookupButton);
            ScrollToElement(LinkedadlcategoriesidLookupButton);
            Click(LinkedadlcategoriesidLookupButton);

            return this;
        }


        public PersonalSafetyAndEnvironmentRecordPage ValidateLinkedAdlCategories_SelectedElementLinkText(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(LinkedAdl_SelectedElementLink(ElementId));
            ValidateElementText(LinkedAdl_SelectedElementLink(ElementId), ExpectedText);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateLinkedAdlCategories_SelectedElementLinkText(Guid ElementId, string ExpectedText)
        {
            return ValidateLinkedAdlCategories_SelectedElementLinkText(ElementId.ToString(), ExpectedText);
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickLinkedAdlCategories_SelectedElementRemoveButton(string ElementId)
        {
            WaitForElementToBeClickable(LinkedAdl_SelectedElementRemoveButton(ElementId));
            Click(LinkedAdl_SelectedElementRemoveButton(ElementId));

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickIncludeInNextHandover_YesRadioButton()
        {
            WaitForElementToBeClickable(Isincludeinnexthandover_1);
            Click(Isincludeinnexthandover_1);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateIncludeInNextHandover_YesRadioButtonChecked()
        {
            WaitForElement(Isincludeinnexthandover_1);
            ValidateElementChecked(Isincludeinnexthandover_1);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateIncludeInNextHandover_YesRadioButtonNotChecked()
        {
            WaitForElement(Isincludeinnexthandover_1);
            ValidateElementNotChecked(Isincludeinnexthandover_1);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickIncludeInNextHandover_NoRadioButton()
        {
            WaitForElementToBeClickable(Isincludeinnexthandover_0);
            Click(Isincludeinnexthandover_0);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateIncludeInNextHandover_NoRadioButtonChecked()
        {
            WaitForElement(Isincludeinnexthandover_0);
            ValidateElementChecked(Isincludeinnexthandover_0);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateIncludeInNextHandover_NoRadioButtonNotChecked()
        {
            WaitForElement(Isincludeinnexthandover_0);
            ValidateElementNotChecked(Isincludeinnexthandover_0);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickFlagRecordForHandover_YesRadioButton()
        {
            WaitForElementToBeClickable(Flagrecordforhandover_1);
            Click(Flagrecordforhandover_1);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateFlagRecordForHandover_YesRadioButtonChecked()
        {
            WaitForElement(Flagrecordforhandover_1);
            ValidateElementChecked(Flagrecordforhandover_1);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateFlagRecordForHandover_YesRadioButtonNotChecked()
        {
            WaitForElement(Flagrecordforhandover_1);
            ValidateElementNotChecked(Flagrecordforhandover_1);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ClickFlagRecordForHandover_NoRadioButton()
        {
            WaitForElementToBeClickable(Flagrecordforhandover_0);
            Click(Flagrecordforhandover_0);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateFlagRecordForHandover_NoRadioButtonChecked()
        {
            WaitForElement(Flagrecordforhandover_0);
            ValidateElementChecked(Flagrecordforhandover_0);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateFlagRecordForHandover_NoRadioButtonNotChecked()
        {
            WaitForElement(Flagrecordforhandover_0);
            ValidateElementNotChecked(Flagrecordforhandover_0);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateFieldIsVisible(string FieldName, bool ExpectVisible = true)
        {
            if (ExpectVisible)
                WaitForElementVisible(FieldLabel(FieldName));
            else
                WaitForElementNotVisible(FieldLabel(FieldName), 3);

            return this;
        }

        public PersonalSafetyAndEnvironmentRecordPage ValidateMandatoryFieldIsVisible(string FieldName, bool ExpectVisible = true)
        {
            if (ExpectVisible)
                WaitForElementVisible(MandatoryField_Label(FieldName));
            else
                WaitForElementNotVisible(MandatoryField_Label(FieldName), 3);

            return this;
        }
    }
}


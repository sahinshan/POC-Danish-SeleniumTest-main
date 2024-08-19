using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonActivityRecordPage : CommonMethods
    {
        public PersonActivityRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cppersonactivities&')]");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
        readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
        readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Person Activity: ']");

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

        By ActivitiesParticipatedIn_SelectedElementLink(string ElementId) => By.XPath("//*[@id='MS_activitiesparticipated_" + ElementId + "']/a[@id='" + ElementId + "_Link']");
        By ActivitiesParticipatedIn_SelectedElementRemoveButton(string ElementId) => By.XPath("//*[@id='MS_activitiesparticipated_" + ElementId + "']/a[text()='Remove']");
        readonly By ActivitiesParticipatedInLookupButton = By.XPath("//*[@id='CWLookupBtn_activitiesparticipated']");

        readonly By Activitiesother = By.XPath("//*[@id='CWField_activitiesother']");
        readonly By Detailsofactivity = By.XPath("//*[@id='CWField_detailsofactivity']");
        readonly By Enjoymentofactivityid = By.XPath("//*[@id='CWField_enjoymentofactivityid']");

        #endregion

        #region Additional Information section

        By Location_SelectedElementLink(string ElementId) => By.XPath("//*[@id='MS_carephysicallocationid_" + ElementId + "']/a[@id='" + ElementId + "_Link']");
        By Location_SelectedElementRemoveButton(string ElementId) => By.XPath("//*[@id='MS_carephysicallocationid_" + ElementId + "']/a[text()='Remove']");
        readonly By CarephysicallocationidLookupButton = By.XPath("//*[@id='CWLookupBtn_carephysicallocationid']");

        readonly By locationifother = By.XPath("//*[@id='CWField_locationifother']");
        readonly By CarewellbeingidLink = By.XPath("//*[@id='CWField_carewellbeingid_Link']");
        readonly By CarewellbeingidClearButton = By.XPath("//*[@id='CWClearLookup_carewellbeingid']");
        readonly By CarewellbeingidLookupButton = By.XPath("//*[@id='CWLookupBtn_carewellbeingid']");
        readonly By actiontaken = By.XPath("//*[@id='CWField_actiontaken']");
        readonly By Timespentwithclient = By.XPath("//*[@id='CWField_timespentwithclient']");
        readonly By Additionalnotes = By.XPath("//*[@id='CWField_additionalnotes']");
        readonly By CareassistanceneededidLink = By.XPath("//*[@id='CWField_careassistanceneededid_Link']");
        readonly By CareassistanceneededidClearButton = By.XPath("//*[@id='CWClearLookup_careassistanceneededid']");
        readonly By CareassistanceneededidLookupButton = By.XPath("//*[@id='CWLookupBtn_careassistanceneededid']");
        readonly By careassistancelevelid = By.XPath("//*[@id='CWField_careassistancelevelid']");

        By StaffRequired_SelectedElementLinkBeforeSave(string ElementId, string ElementText) => By.XPath("//*[@id='MS_otherstaffwhoassistedid_" + ElementId + "'][text()='" + ElementText + "']");
        By StaffRequired_SelectedElementLink(string ElementId) => By.XPath("//*[@id='MS_otherstaffwhoassistedid_" + ElementId + "']/a[@id='" + ElementId + "_Link']");
        By StaffRequired_SelectedElementRemoveButton(string ElementId) => By.XPath("//*[@id='MS_otherstaffwhoassistedid_" + ElementId + "']/a[text()='Remove']");
        readonly By OtherstaffwhoassistedidLookupButton = By.XPath("//*[@id='CWLookupBtn_otherstaffwhoassistedid']");

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


        public PersonActivityRecordPage WaitForPageToLoad()
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


        public PersonActivityRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public PersonActivityRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public PersonActivityRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public PersonActivityRecordPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public PersonActivityRecordPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }


        public PersonActivityRecordPage ValidateTopPageNotificationText(string ExpectedText)
        {
            ValidateElementText(TopPageNotification, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage ValidateTopPageNotificationVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(TopPageNotification);
            else
                WaitForElementNotVisible(TopPageNotification, 3);

            return this;
        }


        public PersonActivityRecordPage ClickPersonLink()
        {
            WaitForElementToBeClickable(PersonidLink);
            Click(PersonidLink);

            return this;
        }

        public PersonActivityRecordPage ValidatePersonLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(PersonidLink);
            ValidateElementText(PersonidLink, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage ClickPersonLookupButton()
        {
            WaitForElementToBeClickable(PersonidLookupButton);
            Click(PersonidLookupButton);

            return this;
        }

        public PersonActivityRecordPage ValidatePreferencesText(string ExpectedText)
        {
            var elementValue = this.GetElementValueByJavascript("CWField_preferences");
            Assert.AreEqual(ExpectedText, elementValue);

            return this;
        }

        public PersonActivityRecordPage InsertTextOnPreferences(string TextToInsert)
        {
            WaitForElementToBeClickable(Preferences);
            SendKeys(Preferences, TextToInsert);

            return this;
        }

        public PersonActivityRecordPage SelectNonConsentDetail(string TextToSelect)
        {
            WaitForElementToBeClickable(Carenonconsentid);
            SelectPicklistElementByText(Carenonconsentid, TextToSelect);

            return this;
        }

        public PersonActivityRecordPage ValidateNonConsentDetailSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Carenonconsentid, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage ValidateNonConsentDetailErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Carenonconsentid_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage ValidateNonConsentDetailErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Carenonconsentid_ErrorLabel);
            else
                WaitForElementNotVisible(Carenonconsentid_ErrorLabel, 3);

            return this;
        }

        public PersonActivityRecordPage ValidateReasonForAbsenceText(string ExpectedText)
        {
            ValidateElementText(Reasonforabsence, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage InsertTextOnReasonForAbsence(string TextToInsert)
        {
            WaitForElementToBeClickable(Reasonforabsence);
            SendKeys(Reasonforabsence, TextToInsert);

            return this;
        }

        public PersonActivityRecordPage ValidateReasonForAbsenceErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Reasonforabsence_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage ValidateReasonForAbsenceErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Reasonforabsence_ErrorLabel);
            else
                WaitForElementNotVisible(Reasonforabsence_ErrorLabel, 3);

            return this;
        }

        public PersonActivityRecordPage ValidateReasonConsentDeclinedText(string ExpectedText)
        {
            ValidateElementText(Reasonconsentdeclined, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage InsertTextOnReasonConsentDeclined(string TextToInsert)
        {
            WaitForElementToBeClickable(Reasonconsentdeclined);
            SendKeys(Reasonconsentdeclined, TextToInsert);

            return this;
        }

        public PersonActivityRecordPage ValidateReasonConsentDeclinedErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Reasonconsentdeclined_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage ValidateReasonConsentDeclinedErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Reasonconsentdeclined_ErrorLabel);
            else
                WaitForElementNotVisible(Reasonconsentdeclined_ErrorLabel, 3);

            return this;
        }

        public PersonActivityRecordPage ValidateEncouragementGivenText(string ExpectedText)
        {
            ValidateElementText(Encouragementgiven, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage InsertTextOnEncouragementGiven(string TextToInsert)
        {
            WaitForElementToBeClickable(Encouragementgiven);
            SendKeys(Encouragementgiven, TextToInsert);

            return this;
        }

        public PersonActivityRecordPage ValidateEncouragementGivenErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Encouragementgiven_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage ValidateEncouragementGivenErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Encouragementgiven_ErrorLabel);
            else
                WaitForElementNotVisible(Encouragementgiven_ErrorLabel, 3);

            return this;
        }

        public PersonActivityRecordPage ClickCareProvidedWithoutConsent_YesRadioButton()
        {
            WaitForElementToBeClickable(Careprovidedwithoutconsent_1);
            Click(Careprovidedwithoutconsent_1);

            return this;
        }

        public PersonActivityRecordPage ValidateCareProvidedWithoutConsent_YesRadioButtonChecked()
        {
            WaitForElement(Careprovidedwithoutconsent_1);
            ValidateElementChecked(Careprovidedwithoutconsent_1);

            return this;
        }

        public PersonActivityRecordPage ValidateCareProvidedWithoutConsent_YesRadioButtonNotChecked()
        {
            WaitForElement(Careprovidedwithoutconsent_1);
            ValidateElementNotChecked(Careprovidedwithoutconsent_1);

            return this;
        }

        public PersonActivityRecordPage ClickCareProvidedWithoutConsent_NoRadioButton()
        {
            WaitForElementToBeClickable(Careprovidedwithoutconsent_0);
            Click(Careprovidedwithoutconsent_0);

            return this;
        }

        public PersonActivityRecordPage ValidateCareProvidedWithoutConsent_NoRadioButtonChecked()
        {
            WaitForElement(Careprovidedwithoutconsent_0);
            ValidateElementChecked(Careprovidedwithoutconsent_0);

            return this;
        }

        public PersonActivityRecordPage ValidateCareProvidedWithoutConsent_NoRadioButtonNotChecked()
        {
            WaitForElement(Careprovidedwithoutconsent_0);
            ValidateElementNotChecked(Careprovidedwithoutconsent_0);

            return this;
        }

        public PersonActivityRecordPage ValidateDeferredToDateText(string ExpectedText)
        {
            ValidateElementValue(Deferredtodate, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage InsertDeferredToDate(string TextToInsert)
        {
            WaitForElementToBeClickable(Deferredtodate);
            SendKeys(Deferredtodate, TextToInsert);

            return this;
        }

        public PersonActivityRecordPage ValidateDeferredToDateErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Deferredtodate_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage ValidateDeferredToDateErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Deferredtodate_ErrorLabel);
            else
                WaitForElementNotVisible(Deferredtodate_ErrorLabel, 3);

            return this;
        }

        public PersonActivityRecordPage SelectDeferredToTimeOrShift(string TextToSelect)
        {
            WaitForElementToBeClickable(Timeorshiftid);
            SelectPicklistElementByText(Timeorshiftid, TextToSelect);

            return this;
        }

        public PersonActivityRecordPage ValidateDeferredToTimeOrShiftSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Timeorshiftid, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage ValidateDeferredToTimeOrShiftErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Timeorshiftid_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage ValidateDeferredToTimeOrShiftErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Timeorshiftid_ErrorLabel);
            else
                WaitForElementNotVisible(Timeorshiftid_ErrorLabel, 3);

            return this;
        }

        public PersonActivityRecordPage ValidateDeferredToTimeText(string ExpectedText)
        {
            ValidateElementValue(Deferredtotime, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage InsertDeferredToTime(string TextToInsert)
        {
            WaitForElementToBeClickable(Deferredtotime);
            SendKeys(Deferredtotime, TextToInsert);

            return this;
        }

        public PersonActivityRecordPage ValidateDeferredToTimeErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Deferredtotime_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage ValidateDeferredToTimeErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Deferredtotime_ErrorLabel);
            else
                WaitForElementNotVisible(Deferredtotime_ErrorLabel, 3);

            return this;
        }

        public PersonActivityRecordPage ClickDeferredToShiftLink()
        {
            WaitForElementToBeClickable(DeferredtoselectedshiftidLink);
            Click(DeferredtoselectedshiftidLink);

            return this;
        }

        public PersonActivityRecordPage ValidateDeferredToShiftLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(DeferredtoselectedshiftidLink);
            ValidateElementText(DeferredtoselectedshiftidLink, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage ClickDeferredToShiftLookupButton()
        {
            WaitForElementToBeClickable(DeferredtoselectedshiftidLookupButton);
            Click(DeferredtoselectedshiftidLookupButton);

            return this;
        }

        public PersonActivityRecordPage ValidateDeferredToShiftErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Deferredtoselectedshiftid_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage ValidateDeferredToShiftErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Deferredtoselectedshiftid_ErrorLabel);
            else
                WaitForElementNotVisible(Deferredtoselectedshiftid_ErrorLabel, 3);

            return this;
        }

        public PersonActivityRecordPage SelectConsentGiven(string TextToSelect)
        {
            WaitForElementToBeClickable(Careconsentgivenid);
            SelectPicklistElementByText(Careconsentgivenid, TextToSelect);

            return this;
        }

        public PersonActivityRecordPage ValidateConsentGivenSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Careconsentgivenid, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage ValidateConsentGivenErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Careconsentgivenid_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage ValidateConsentGivenErrorLabelVisibility(bool ExpectVisible)
        {
            if(ExpectVisible)
                WaitForElementVisible(Careconsentgivenid_ErrorLabel);
            else
                WaitForElementNotVisible(Careconsentgivenid_ErrorLabel, 3);

            return this;
        }

        public PersonActivityRecordPage ClickResponsibleTeamLink()
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            Click(ResponsibleTeamLink);

            return this;
        }

        public PersonActivityRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            ValidateElementText(ResponsibleTeamLink, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamLookupButton);
            Click(ResponsibleTeamLookupButton);

            return this;
        }

        public PersonActivityRecordPage ValidateDateAndTimeOccurredText(string ExpectedText)
        {
            ValidateElementValue(Occurred, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage InsertTextOnDateAndTimeOccurred(string TextToInsert)
        {
            WaitForElementToBeClickable(Occurred);
            SendKeys(Occurred, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public PersonActivityRecordPage ClickDateAndTimeOccurredDatePicker()
        {
            WaitForElementToBeClickable(OccurredDatePicker);
            Click(OccurredDatePicker);

            return this;
        }

        public PersonActivityRecordPage ValidateDateAndTimeOccurred_TimeText(string ExpectedText)
        {
            ValidateElementValue(Occurred_Time, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage InsertTextOnDateAndTimeOccurred_Time(string TextToInsert)
        {
            WaitForElementToBeClickable(Occurred_Time);
            SendKeys(Occurred_Time, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public PersonActivityRecordPage ClickDateAndTimeOccurred_Time_TimePicker()
        {
            WaitForElementToBeClickable(Occurred_Time_TimePicker);
            Click(Occurred_Time_TimePicker);

            return this;
        }

        public PersonActivityRecordPage ValidateCreatedOnText(string ExpectedText)
        {
            ValidateElementValue(Createdon, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage InsertTextOnCreatedOn(string TextToInsert)
        {
            WaitForElementToBeClickable(Createdon);
            SendKeys(Createdon, TextToInsert);

            return this;
        }

        public PersonActivityRecordPage ClickCreatedOnDatePicker()
        {
            WaitForElementToBeClickable(CreatedonDatePicker);
            Click(CreatedonDatePicker);

            return this;
        }

        public PersonActivityRecordPage ValidateCreatedOn_TimeText(string ExpectedText)
        {
            ValidateElementValue(Createdon_Time, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage InsertTextOnCreatedOn_Time(string TextToInsert)
        {
            WaitForElementToBeClickable(Createdon_Time);
            SendKeys(Createdon_Time, TextToInsert);

            return this;
        }

        public PersonActivityRecordPage ClickCreatedOn_Time_TimePicker()
        {
            WaitForElementToBeClickable(Createdon_Time_TimePicker);
            Click(Createdon_Time_TimePicker);

            return this;
        }

        public PersonActivityRecordPage ClickActivitiesParticipatedIn_SelectedElementLink(string ElementId)
        {
            WaitForElementToBeClickable(ActivitiesParticipatedIn_SelectedElementLink(ElementId));
            Click(ActivitiesParticipatedIn_SelectedElementLink(ElementId));

            return this;
        }

        public PersonActivityRecordPage ValidateActivitiesParticipatedIn_SelectedElementLinkText(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(ActivitiesParticipatedIn_SelectedElementLink(ElementId));
            ValidateElementText(ActivitiesParticipatedIn_SelectedElementLink(ElementId), ExpectedText);

            return this;
        }

        public PersonActivityRecordPage ValidateActivitiesParticipatedIn_SelectedElementLinkText(Guid ElementId, string ExpectedText)
        {
            return ValidateActivitiesParticipatedIn_SelectedElementLinkText(ElementId.ToString(), ExpectedText);
        }

        public PersonActivityRecordPage ClickActivitiesParticipatedIn_SelectedElementRemoveButton(string ElementId)
        {
            WaitForElementToBeClickable(ActivitiesParticipatedIn_SelectedElementRemoveButton(ElementId));
            Click(ActivitiesParticipatedIn_SelectedElementRemoveButton(ElementId));

            return this;
        }

        public PersonActivityRecordPage ClickActivitiesParticipatedInLookupButton()
        {
            WaitForElementToBeClickable(ActivitiesParticipatedInLookupButton);
            Click(ActivitiesParticipatedInLookupButton);

            return this;
        }

        public PersonActivityRecordPage ValidateActivitiesOtherText(string ExpectedText)
        {
            ValidateElementValue(Activitiesother, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage InsertTextOnActivitiesOther(string TextToInsert)
        {
            WaitForElementToBeClickable(Activitiesother);
            SendKeys(Activitiesother, TextToInsert);

            return this;
        }

        public PersonActivityRecordPage ValidateActivitiesOtherVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Activitiesother);
            else
                WaitForElementNotVisible(Activitiesother, 3);

            return this;
        }

        public PersonActivityRecordPage ValidateDetailsOfActivityText(string ExpectedText)
        {
            ValidateElementText(Detailsofactivity, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage InsertTextOnDetailsOfActivity(string TextToInsert)
        {
            WaitForElementToBeClickable(Detailsofactivity);
            SendKeys(Detailsofactivity, TextToInsert);

            return this;
        }

        public PersonActivityRecordPage SelectEnjoymentOfActivity(string TextToSelect)
        {
            WaitForElementToBeClickable(Enjoymentofactivityid);
            SelectPicklistElementByText(Enjoymentofactivityid, TextToSelect);

            return this;
        }

        public PersonActivityRecordPage ValidateEnjoymentOfActivitySelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Enjoymentofactivityid, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage ClickLocation_SelectedElementLink(string ElementId)
        {
            WaitForElementToBeClickable(Location_SelectedElementLink(ElementId));
            Click(Location_SelectedElementLink(ElementId));

            return this;
        }

        public PersonActivityRecordPage ValidateLocation_SelectedElementLinkText(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(Location_SelectedElementLink(ElementId));
            ValidateElementText(Location_SelectedElementLink(ElementId), ExpectedText);

            return this;
        }

        public PersonActivityRecordPage ValidateLocation_SelectedElementLinkText(Guid ElementId, string ExpectedText)
        {
            return ValidateLocation_SelectedElementLinkText(ElementId.ToString(), ExpectedText);
        }

        public PersonActivityRecordPage ClickLocation_SelectedElementRemoveButton(string ElementId)
        {
            WaitForElementToBeClickable(Location_SelectedElementRemoveButton(ElementId));
            Click(Location_SelectedElementRemoveButton(ElementId));

            return this;
        }

        public PersonActivityRecordPage ClickLocationLookupButton()
        {
            WaitForElementToBeClickable(CarephysicallocationidLookupButton);
            Click(CarephysicallocationidLookupButton);

            return this;
        }

        public PersonActivityRecordPage ValidateLocationIfOtherText(string ExpectedText)
        {
            ValidateElementText(locationifother, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage InsertTextOnLocationIfOther(string TextToInsert)
        {
            WaitForElementToBeClickable(locationifother);
            SendKeys(locationifother, TextToInsert);

            return this;
        }

        public PersonActivityRecordPage ValidateLocationIfOtherVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(locationifother);
            else
                WaitForElementNotVisible(locationifother, 3);

            return this;
        }

        public PersonActivityRecordPage ClickWellbeingLink()
        {
            WaitForElementToBeClickable(CarewellbeingidLink);
            Click(CarewellbeingidLink);

            return this;
        }

        public PersonActivityRecordPage ValidateWellbeingLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CarewellbeingidLink);
            ValidateElementText(CarewellbeingidLink, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage ClickWellbeingClearButton()
        {
            WaitForElementToBeClickable(CarewellbeingidClearButton);
            Click(CarewellbeingidClearButton);

            return this;
        }

        public PersonActivityRecordPage ClickWellbeingLookupButton()
        {
            WaitForElementToBeClickable(CarewellbeingidLookupButton);
            Click(CarewellbeingidLookupButton);

            return this;
        }

        public PersonActivityRecordPage ValidateActionTakenText(string ExpectedText)
        {
            ValidateElementText(actiontaken, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage InsertTextOnActionTaken(string TextToInsert)
        {
            WaitForElementToBeClickable(actiontaken);
            SendKeys(actiontaken, TextToInsert);

            return this;
        }

        public PersonActivityRecordPage ValidateActionTakenVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(actiontaken);
            else
                WaitForElementNotVisible(actiontaken, 3);

            return this;
        }

        public PersonActivityRecordPage ValidateTotalTimeSpentWithPersonText(string ExpectedText)
        {
            ValidateElementValue(Timespentwithclient, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage InsertTextOnTotalTimeSpentWithPerson(string TextToInsert)
        {
            WaitForElementToBeClickable(Timespentwithclient);
            SendKeys(Timespentwithclient, TextToInsert);

            return this;
        }

        public PersonActivityRecordPage ValidateAdditionalNotesText(string ExpectedText)
        {
            ValidateElementText(Additionalnotes, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage InsertTextOnAdditionalNotes(string TextToInsert)
        {
            WaitForElementToBeClickable(Additionalnotes);
            SendKeys(Additionalnotes, TextToInsert);

            return this;
        }

        public PersonActivityRecordPage ClickAssistanceNeededLink()
        {
            WaitForElementToBeClickable(CareassistanceneededidLink);
            Click(CareassistanceneededidLink);

            return this;
        }

        public PersonActivityRecordPage ValidateAssistanceNeededLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CareassistanceneededidLink);
            ValidateElementText(CareassistanceneededidLink, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage ClickAssistanceNeededClearButton()
        {
            WaitForElementToBeClickable(CareassistanceneededidClearButton);
            Click(CareassistanceneededidClearButton);

            return this;
        }

        public PersonActivityRecordPage ClickAssistanceNeededLookupButton()
        {
            WaitForElementToBeClickable(CareassistanceneededidLookupButton);
            Click(CareassistanceneededidLookupButton);

            return this;
        }

        public PersonActivityRecordPage SelectAssistanceAmount(string TextToSelect)
        {
            WaitForElementToBeClickable(careassistancelevelid);
            SelectPicklistElementByText(careassistancelevelid, TextToSelect);

            return this;
        }

        public PersonActivityRecordPage ValidateAssistanceAmountSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(careassistancelevelid, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage ValidateAssistanceAmountVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(careassistancelevelid);
            else
                WaitForElementNotVisible(careassistancelevelid, 3);

            return this;
        }

        public PersonActivityRecordPage ClickStaffRequired_SelectedElementLink(string ElementId)
        {
            WaitForElementToBeClickable(StaffRequired_SelectedElementLink(ElementId));
            Click(StaffRequired_SelectedElementLink(ElementId));

            return this;
        }

        public PersonActivityRecordPage ValidateStaffRequired_SelectedElementLinkTextBeforeSave(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(StaffRequired_SelectedElementLinkBeforeSave(ElementId, ExpectedText));

            return this;
        }

        public PersonActivityRecordPage ValidateStaffRequired_SelectedElementLinkTextBeforeSave(Guid ElementId, string ExpectedText)
        {
            return ValidateStaffRequired_SelectedElementLinkTextBeforeSave(ElementId.ToString(), ExpectedText);
        }

        public PersonActivityRecordPage ValidateStaffRequired_SelectedElementLinkText(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(StaffRequired_SelectedElementLink(ElementId));
            ValidateElementText(StaffRequired_SelectedElementLink(ElementId), ExpectedText);

            return this;
        }

        public PersonActivityRecordPage ValidateStaffRequired_SelectedElementLinkText(Guid ElementId, string ExpectedText)
        {
            return ValidateStaffRequired_SelectedElementLinkText(ElementId.ToString(), ExpectedText);
        }

        public PersonActivityRecordPage ClickStaffRequired_SelectedElementRemoveButton(string ElementId)
        {
            WaitForElementToBeClickable(StaffRequired_SelectedElementRemoveButton(ElementId));
            Click(StaffRequired_SelectedElementRemoveButton(ElementId));

            return this;
        }

        public PersonActivityRecordPage ClickStaffRequiredLookupButton()
        {
            WaitForElementToBeClickable(OtherstaffwhoassistedidLookupButton);
            Click(OtherstaffwhoassistedidLookupButton);

            return this;
        }

        public PersonActivityRecordPage ValidateCareNoteText(string ExpectedText)
        {
            ValidateElementText(Carenote, ExpectedText);

            return this;
        }

        public PersonActivityRecordPage InsertTextOnCareNote(string TextToInsert)
        {
            WaitForElementToBeClickable(Carenote);
            SendKeys(Carenote, TextToInsert);
            return this;
        }

        public PersonActivityRecordPage ClickinkedActivitiesOfDailyLivingLookupButton()
        {
            WaitForElementToBeClickable(LinkedadlcategoriesidLookupButton);
            Click(LinkedadlcategoriesidLookupButton);

            return this;
        }

        public PersonActivityRecordPage ClickIncludeInNextHandover_YesRadioButton()
        {
            WaitForElementToBeClickable(Isincludeinnexthandover_1);
            Click(Isincludeinnexthandover_1);

            return this;
        }

        public PersonActivityRecordPage ValidateIncludeInNextHandover_YesRadioButtonChecked()
        {
            WaitForElement(Isincludeinnexthandover_1);
            ValidateElementChecked(Isincludeinnexthandover_1);

            return this;
        }

        public PersonActivityRecordPage ValidateIncludeInNextHandover_YesRadioButtonNotChecked()
        {
            WaitForElement(Isincludeinnexthandover_1);
            ValidateElementNotChecked(Isincludeinnexthandover_1);

            return this;
        }

        public PersonActivityRecordPage ClickIncludeInNextHandover_NoRadioButton()
        {
            WaitForElementToBeClickable(Isincludeinnexthandover_0);
            Click(Isincludeinnexthandover_0);

            return this;
        }

        public PersonActivityRecordPage ValidateIncludeInNextHandover_NoRadioButtonChecked()
        {
            WaitForElement(Isincludeinnexthandover_0);
            ValidateElementChecked(Isincludeinnexthandover_0);

            return this;
        }

        public PersonActivityRecordPage ValidateIncludeInNextHandover_NoRadioButtonNotChecked()
        {
            WaitForElement(Isincludeinnexthandover_0);
            ValidateElementNotChecked(Isincludeinnexthandover_0);

            return this;
        }

        public PersonActivityRecordPage ClickFlagRecordForHandover_YesRadioButton()
        {
            WaitForElementToBeClickable(Flagrecordforhandover_1);
            Click(Flagrecordforhandover_1);

            return this;
        }

        public PersonActivityRecordPage ValidateFlagRecordForHandover_YesRadioButtonChecked()
        {
            WaitForElement(Flagrecordforhandover_1);
            ValidateElementChecked(Flagrecordforhandover_1);

            return this;
        }

        public PersonActivityRecordPage ValidateFlagRecordForHandover_YesRadioButtonNotChecked()
        {
            WaitForElement(Flagrecordforhandover_1);
            ValidateElementNotChecked(Flagrecordforhandover_1);

            return this;
        }

        public PersonActivityRecordPage ClickFlagRecordForHandover_NoRadioButton()
        {
            WaitForElementToBeClickable(Flagrecordforhandover_0);
            Click(Flagrecordforhandover_0);

            return this;
        }

        public PersonActivityRecordPage ValidateFlagRecordForHandover_NoRadioButtonChecked()
        {
            WaitForElement(Flagrecordforhandover_0);
            ValidateElementChecked(Flagrecordforhandover_0);

            return this;
        }

        public PersonActivityRecordPage ValidateFlagRecordForHandover_NoRadioButtonNotChecked()
        {
            WaitForElement(Flagrecordforhandover_0);
            ValidateElementNotChecked(Flagrecordforhandover_0);

            return this;
        }

    }
}


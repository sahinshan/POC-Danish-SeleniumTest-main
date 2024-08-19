using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OpenQA.Selenium;
using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PainManagementRecordPage : CommonMethods
    {
        public PainManagementRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cppersonpainmanagement&')]");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
        readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
        readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Pain Management: ']");

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

        #region Pain Review section

        readonly By isthepersoninpain_1 = By.XPath("//*[@id='CWField_isthepersoninpain_1']");
        readonly By isthepersoninpain_0 = By.XPath("//*[@id='CWField_isthepersoninpain_0']");
        readonly By isthepersoninpain_ErrorLabel = By.XPath("//*[@id='CWControlHolder_isthepersoninpain']/label/span");

        readonly By whereisthepain = By.XPath("//*[@id='CWField_whereisthepain']");
        readonly By whereisthepain_ErrorLabel = By.XPath("//*[@id='CWControlHolder_whereisthepain']/label/span");

        readonly By severityofpainid = By.XPath("//*[@id='CWField_severityofpainid']");
        readonly By severityofpainid_ErrorLabel = By.XPath("//*[@id='CWControlHolder_severityofpainid']/label/span");

        #endregion

        #region Abbey Pain Scale

        readonly By vocalisationid = By.XPath("//*[@id='CWField_vocalisationid']");
        readonly By vocalisationid_ErrorLabel = By.XPath("//*[@id='CWControlHolder_vocalisationid']/label/span");

        readonly By facialexpressionid = By.XPath("//*[@id='CWField_facialexpressionid']");
        readonly By facialexpressionid_ErrorLabel = By.XPath("//*[@id='CWControlHolder_facialexpressionid']/label/span");

        readonly By changeinbodylanguageid = By.XPath("//*[@id='CWField_changeinbodylanguageid']");
        readonly By changeinbodylanguageid_ErrorLabel = By.XPath("//*[@id='CWControlHolder_changeinbodylanguageid']/label/span");

        readonly By behaviouralchangeid = By.XPath("//*[@id='CWField_behaviouralchangeid']");
        readonly By behaviouralchangeid_ErrorLabel = By.XPath("//*[@id='CWControlHolder_behaviouralchangeid']/label/span");

        readonly By physiologicalchangeid = By.XPath("//*[@id='CWField_physiologicalchangeid']");
        readonly By physiologicalchangeid_ErrorLabel = By.XPath("//*[@id='CWControlHolder_physiologicalchangeid']/label/span");

        readonly By physicalchangesid = By.XPath("//*[@id='CWField_physicalchangesid']");
        readonly By physicalchangesid_ErrorLabel = By.XPath("//*[@id='CWControlHolder_physicalchangesid']/label/span");

        readonly By totalpainscore = By.XPath("//*[@id='CWField_totalpainscore']");
        readonly By totalpainscoredescriptionid = By.XPath("//*[@id='CWField_totalpainscoredescriptionid']");

        readonly By reviewrequiredbyseniorcolleague_1 = By.XPath("//*[@id='CWField_reviewrequiredbyseniorcolleague_1']");
        readonly By reviewrequiredbyseniorcolleague_0 = By.XPath("//*[@id='CWField_reviewrequiredbyseniorcolleague_0']");

        readonly By reviewdetails = By.XPath("//*[@id='CWField_reviewdetails']");
        readonly By reviewdetails_ErrorLabel = By.XPath("//*[@id='CWControlHolder_reviewdetails']/label/span");

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
        readonly By Timespentwithclient = By.XPath("//*[@id='CWField_totaltimespentwithclientminutes']");
        readonly By TotalTimeSpentWithPerson_ErrorLabel = By.XPath("//*[@id='CWField_totaltimespentwithclientminutes']/label/span");
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


        public PainManagementRecordPage WaitForPageToLoad()
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


        public PainManagementRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public PainManagementRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public PainManagementRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public PainManagementRecordPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public PainManagementRecordPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }


        public PainManagementRecordPage ValidateTopPageNotificationText(string ExpectedText)
        {
            ValidateElementText(TopPageNotification, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateTopPageNotificationVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(TopPageNotification);
            else
                WaitForElementNotVisible(TopPageNotification, 3);

            return this;
        }


        public PainManagementRecordPage ClickPersonLink()
        {
            WaitForElementToBeClickable(PersonidLink);
            Click(PersonidLink);

            return this;
        }

        public PainManagementRecordPage ValidatePersonLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(PersonidLink);
            ValidateElementText(PersonidLink, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ClickPersonLookupButton()
        {
            WaitForElementToBeClickable(PersonidLookupButton);
            Click(PersonidLookupButton);

            return this;
        }

        public PainManagementRecordPage ValidatePreferencesText(string ExpectedText)
        {
            var elementValue = this.GetElementValueByJavascript("CWField_preferences");
            Assert.AreEqual(ExpectedText, elementValue);

            return this;
        }

        public PainManagementRecordPage InsertTextOnPreferences(string TextToInsert)
        {
            WaitForElementToBeClickable(Preferences);
            SendKeys(Preferences, TextToInsert + Keys.Tab);

            return this;
        }

        public PainManagementRecordPage SelectNonConsentDetail(string TextToSelect)
        {
            WaitForElementToBeClickable(Carenonconsentid);
            SelectPicklistElementByText(Carenonconsentid, TextToSelect);

            return this;
        }

        public PainManagementRecordPage ValidateNonConsentDetailSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Carenonconsentid, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateNonConsentDetailErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Carenonconsentid_ErrorLabel, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateNonConsentDetailErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Carenonconsentid_ErrorLabel);
            else
                WaitForElementNotVisible(Carenonconsentid_ErrorLabel, 3);

            return this;
        }

        public PainManagementRecordPage ValidateReasonForAbsenceText(string ExpectedText)
        {
            ValidateElementText(Reasonforabsence, ExpectedText);

            return this;
        }

        public PainManagementRecordPage InsertTextOnReasonForAbsence(string TextToInsert)
        {
            WaitForElementToBeClickable(Reasonforabsence);
            SendKeys(Reasonforabsence, TextToInsert + Keys.Tab);

            return this;
        }

        public PainManagementRecordPage ValidateReasonForAbsenceErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Reasonforabsence_ErrorLabel, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateReasonForAbsenceErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Reasonforabsence_ErrorLabel);
            else
                WaitForElementNotVisible(Reasonforabsence_ErrorLabel, 3);

            return this;
        }

        public PainManagementRecordPage ValidateReasonConsentDeclinedText(string ExpectedText)
        {
            ValidateElementText(Reasonconsentdeclined, ExpectedText);

            return this;
        }

        public PainManagementRecordPage InsertTextOnReasonConsentDeclined(string TextToInsert)
        {
            WaitForElementToBeClickable(Reasonconsentdeclined);
            SendKeys(Reasonconsentdeclined, TextToInsert + Keys.Tab);

            return this;
        }

        public PainManagementRecordPage ValidateReasonConsentDeclinedErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Reasonconsentdeclined_ErrorLabel, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateReasonConsentDeclinedErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Reasonconsentdeclined_ErrorLabel);
            else
                WaitForElementNotVisible(Reasonconsentdeclined_ErrorLabel, 3);

            return this;
        }

        public PainManagementRecordPage ValidateEncouragementGivenText(string ExpectedText)
        {
            ValidateElementText(Encouragementgiven, ExpectedText);

            return this;
        }

        public PainManagementRecordPage InsertTextOnEncouragementGiven(string TextToInsert)
        {
            WaitForElementToBeClickable(Encouragementgiven);
            SendKeys(Encouragementgiven, TextToInsert + Keys.Tab);

            return this;
        }

        public PainManagementRecordPage ValidateEncouragementGivenErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Encouragementgiven_ErrorLabel, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateEncouragementGivenErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Encouragementgiven_ErrorLabel);
            else
                WaitForElementNotVisible(Encouragementgiven_ErrorLabel, 3);

            return this;
        }

        public PainManagementRecordPage ClickCareProvidedWithoutConsent_YesRadioButton()
        {
            WaitForElementToBeClickable(Careprovidedwithoutconsent_1);
            Click(Careprovidedwithoutconsent_1);

            return this;
        }

        public PainManagementRecordPage ValidateCareProvidedWithoutConsent_YesRadioButtonChecked()
        {
            WaitForElement(Careprovidedwithoutconsent_1);
            ValidateElementChecked(Careprovidedwithoutconsent_1);

            return this;
        }

        public PainManagementRecordPage ValidateCareProvidedWithoutConsent_YesRadioButtonNotChecked()
        {
            WaitForElement(Careprovidedwithoutconsent_1);
            ValidateElementNotChecked(Careprovidedwithoutconsent_1);

            return this;
        }

        public PainManagementRecordPage ClickCareProvidedWithoutConsent_NoRadioButton()
        {
            WaitForElementToBeClickable(Careprovidedwithoutconsent_0);
            Click(Careprovidedwithoutconsent_0);

            return this;
        }

        public PainManagementRecordPage ValidateCareProvidedWithoutConsent_NoRadioButtonChecked()
        {
            WaitForElement(Careprovidedwithoutconsent_0);
            ValidateElementChecked(Careprovidedwithoutconsent_0);

            return this;
        }

        public PainManagementRecordPage ValidateCareProvidedWithoutConsent_NoRadioButtonNotChecked()
        {
            WaitForElement(Careprovidedwithoutconsent_0);
            ValidateElementNotChecked(Careprovidedwithoutconsent_0);

            return this;
        }

        public PainManagementRecordPage ValidateDeferredToDateText(string ExpectedText)
        {
            ValidateElementValue(Deferredtodate, ExpectedText);

            return this;
        }

        public PainManagementRecordPage InsertDeferredToDate(string TextToInsert)
        {
            WaitForElementToBeClickable(Deferredtodate);
            SendKeys(Deferredtodate, TextToInsert + Keys.Tab);

            return this;
        }

        public PainManagementRecordPage ValidateDeferredToDateErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Deferredtodate_ErrorLabel, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateDeferredToDateErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Deferredtodate_ErrorLabel);
            else
                WaitForElementNotVisible(Deferredtodate_ErrorLabel, 3);

            return this;
        }

        public PainManagementRecordPage SelectDeferredToTimeOrShift(string TextToSelect)
        {
            WaitForElementToBeClickable(Timeorshiftid);
            SelectPicklistElementByText(Timeorshiftid, TextToSelect);

            return this;
        }

        public PainManagementRecordPage ValidateDeferredToTimeOrShiftSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Timeorshiftid, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateDeferredToTimeOrShiftErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Timeorshiftid_ErrorLabel, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateDeferredToTimeOrShiftErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Timeorshiftid_ErrorLabel);
            else
                WaitForElementNotVisible(Timeorshiftid_ErrorLabel, 3);

            return this;
        }

        public PainManagementRecordPage ValidateDeferredToTimeText(string ExpectedText)
        {
            ValidateElementValue(Deferredtotime, ExpectedText);

            return this;
        }

        public PainManagementRecordPage InsertDeferredToTime(string TextToInsert)
        {
            WaitForElementToBeClickable(Deferredtotime);
            SendKeys(Deferredtotime, TextToInsert + Keys.Tab);

            return this;
        }

        public PainManagementRecordPage ValidateDeferredToTimeErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Deferredtotime_ErrorLabel, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateDeferredToTimeErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Deferredtotime_ErrorLabel);
            else
                WaitForElementNotVisible(Deferredtotime_ErrorLabel, 3);

            return this;
        }

        public PainManagementRecordPage ClickDeferredToShiftLink()
        {
            WaitForElementToBeClickable(DeferredtoselectedshiftidLink);
            Click(DeferredtoselectedshiftidLink);

            return this;
        }

        public PainManagementRecordPage ValidateDeferredToShiftLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(DeferredtoselectedshiftidLink);
            ValidateElementText(DeferredtoselectedshiftidLink, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ClickDeferredToShiftLookupButton()
        {
            WaitForElementToBeClickable(DeferredtoselectedshiftidLookupButton);
            Click(DeferredtoselectedshiftidLookupButton);

            return this;
        }

        public PainManagementRecordPage ValidateDeferredToShiftErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Deferredtoselectedshiftid_ErrorLabel, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateDeferredToShiftErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Deferredtoselectedshiftid_ErrorLabel);
            else
                WaitForElementNotVisible(Deferredtoselectedshiftid_ErrorLabel, 3);

            return this;
        }

        public PainManagementRecordPage SelectConsentGiven(string TextToSelect)
        {
            WaitForElementToBeClickable(Careconsentgivenid);
            SelectPicklistElementByText(Careconsentgivenid, TextToSelect);

            return this;
        }

        public PainManagementRecordPage ValidateConsentGivenSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Careconsentgivenid, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateConsentGivenErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Careconsentgivenid_ErrorLabel, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateConsentGivenErrorLabelVisibility(bool ExpectVisible)
        {
            if(ExpectVisible)
                WaitForElementVisible(Careconsentgivenid_ErrorLabel);
            else
                WaitForElementNotVisible(Careconsentgivenid_ErrorLabel, 3);

            return this;
        }

        public PainManagementRecordPage ClickResponsibleTeamLink()
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            Click(ResponsibleTeamLink);

            return this;
        }

        public PainManagementRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            ValidateElementText(ResponsibleTeamLink, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamLookupButton);
            Click(ResponsibleTeamLookupButton);

            return this;
        }

        //verify Responsible Team lookup button is visible
        public PainManagementRecordPage ValidateResponsibleTeamLookupButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ResponsibleTeamLookupButton);
            else
                WaitForElementNotVisible(ResponsibleTeamLookupButton, 3);

            return this;
        }

        public PainManagementRecordPage ValidateDateAndTimeOccurredText(string ExpectedText)
        {
            ValidateElementValue(Occurred, ExpectedText);

            return this;
        }

        public PainManagementRecordPage InsertTextOnDateAndTimeOccurred(string TextToInsert)
        {
            WaitForElementToBeClickable(Occurred);
            SendKeys(Occurred, TextToInsert + Keys.Tab);

            return this;
        }

        public PainManagementRecordPage ClickDateAndTimeOccurredDatePicker()
        {
            WaitForElementToBeClickable(OccurredDatePicker);
            Click(OccurredDatePicker);

            return this;
        }

        public PainManagementRecordPage ValidateDateAndTimeOccurred_TimeText(string ExpectedText)
        {
            ValidateElementValue(Occurred_Time, ExpectedText);

            return this;
        }

        public PainManagementRecordPage InsertTextOnDateAndTimeOccurred_Time(string TextToInsert)
        {
            WaitForElementToBeClickable(Occurred_Time);
            SendKeys(Occurred_Time, TextToInsert + Keys.Tab);

            return this;
        }

        public PainManagementRecordPage ClickDateAndTimeOccurred_Time_TimePicker()
        {
            WaitForElementToBeClickable(Occurred_Time_TimePicker);
            Click(Occurred_Time_TimePicker);

            return this;
        }

        public PainManagementRecordPage ClickIsThePersonInPain_YesRadioButton()
        {
            WaitForElementToBeClickable(isthepersoninpain_1);
            Click(isthepersoninpain_1);

            return this;
        }

        public PainManagementRecordPage ValidateIsThePersonInPain_YesRadioButtonChecked()
        {
            WaitForElement(isthepersoninpain_1);
            ValidateElementChecked(isthepersoninpain_1);

            return this;
        }

        public PainManagementRecordPage ValidateIsThePersonInPain_YesRadioButtonNotChecked()
        {
            WaitForElement(isthepersoninpain_1);
            ValidateElementNotChecked(isthepersoninpain_1);

            return this;
        }

        public PainManagementRecordPage ClickIsThePersonInPain_NoRadioButton()
        {
            WaitForElementToBeClickable(isthepersoninpain_0);
            Click(isthepersoninpain_0);

            return this;
        }

        public PainManagementRecordPage ValidateIsThePersonInPain_NoRadioButtonChecked()
        {
            WaitForElement(isthepersoninpain_0);
            ValidateElementChecked(isthepersoninpain_0);

            return this;
        }

        public PainManagementRecordPage ValidateIsThePersonInPain_NoRadioButtonNotChecked()
        {
            WaitForElement(isthepersoninpain_0);
            ValidateElementNotChecked(isthepersoninpain_0);

            return this;
        }

        public PainManagementRecordPage ValidateIsThePersonInPainErrorLabelText(string ExpectedText)
        {
            ValidateElementText(isthepersoninpain_ErrorLabel, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateIsThePersonInPainErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(isthepersoninpain_ErrorLabel);
            else
                WaitForElementNotVisible(isthepersoninpain_ErrorLabel, 3);

            return this;
        }

        public PainManagementRecordPage ValidateWhereIsThePainText(string ExpectedText)
        {
            ValidateElementValue(whereisthepain, ExpectedText);

            return this;
        }

        public PainManagementRecordPage InsertTextOnWhereIsThePain(string TextToInsert)
        {
            WaitForElementToBeClickable(whereisthepain);
            SendKeys(whereisthepain, TextToInsert + Keys.Tab);

            return this;
        }

        public PainManagementRecordPage ValidateWhereIsThePainVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(whereisthepain);
            else
                WaitForElementNotVisible(whereisthepain, 3);

            return this;
        }

        public PainManagementRecordPage ValidateWhereIsThePainErrorLabelText(string ExpectedText)
        {
            ValidateElementText(whereisthepain_ErrorLabel, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateWhereIsThePainErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(whereisthepain_ErrorLabel);
            else
                WaitForElementNotVisible(whereisthepain_ErrorLabel, 3);

            return this;
        }

        public PainManagementRecordPage ValidateSeverityOfPainSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(severityofpainid, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateSeverityOfPainVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(severityofpainid);
            else
                WaitForElementNotVisible(severityofpainid, 3);

            return this;
        }

        public PainManagementRecordPage SelectSeverityOfPain(string TextToSelect)
        {
            WaitForElementToBeClickable(severityofpainid);
            SelectPicklistElementByText(severityofpainid, TextToSelect);

            return this;
        }

        public PainManagementRecordPage ValidateSeverityOfPainErrorLabelText(string ExpectedText)
        {
            ValidateElementText(severityofpainid_ErrorLabel, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateSeverityOfPainErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(severityofpainid_ErrorLabel);
            else
                WaitForElementNotVisible(severityofpainid_ErrorLabel, 3);

            return this;
        }

        public PainManagementRecordPage ValidateVocalisationSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(vocalisationid, ExpectedText);

            return this;
        }

        public PainManagementRecordPage SelectVocalisation(string TextToSelect)
        {
            WaitForElementToBeClickable(vocalisationid);
            SelectPicklistElementByText(vocalisationid, TextToSelect);

            return this;
        }

        public PainManagementRecordPage ValidateVocalisationVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(vocalisationid);
            else
                WaitForElementNotVisible(vocalisationid, 3);

            return this;
        }

        public PainManagementRecordPage ValidateVocalisationErrorLabelText(string ExpectedText)
        {
            ValidateElementText(vocalisationid_ErrorLabel, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateVocalisationErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(vocalisationid_ErrorLabel);
            else
                WaitForElementNotVisible(vocalisationid_ErrorLabel, 3);

            return this;
        }

        public PainManagementRecordPage ValidateFacialExpressionSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(facialexpressionid, ExpectedText);

            return this;
        }

        public PainManagementRecordPage SelectFacialExpression(string TextToSelect)
        {
            WaitForElementToBeClickable(facialexpressionid);
            SelectPicklistElementByText(facialexpressionid, TextToSelect);

            return this;
        }

        public PainManagementRecordPage ValidateFacialExpressionVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(facialexpressionid);
            else
                WaitForElementNotVisible(facialexpressionid, 3);

            return this;
        }

        public PainManagementRecordPage ValidateFacialExpressionErrorLabelText(string ExpectedText)
        {
            ValidateElementText(facialexpressionid_ErrorLabel, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateFacialExpressionErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(facialexpressionid_ErrorLabel);
            else
                WaitForElementNotVisible(facialexpressionid_ErrorLabel, 3);

            return this;
        }


        public PainManagementRecordPage ValidateChangeInBodyLanguageSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(changeinbodylanguageid, ExpectedText);

            return this;
        }

        public PainManagementRecordPage SelectChangeInBodyLanguage(string TextToSelect)
        {
            WaitForElementToBeClickable(changeinbodylanguageid);
            SelectPicklistElementByText(changeinbodylanguageid, TextToSelect);

            return this;
        }

        public PainManagementRecordPage ValidateChangeInBodyLanguageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(changeinbodylanguageid);
            else
                WaitForElementNotVisible(changeinbodylanguageid, 3);

            return this;
        }

        public PainManagementRecordPage ValidateChangeInBodyLanguageErrorLabelText(string ExpectedText)
        {
            ValidateElementText(changeinbodylanguageid_ErrorLabel, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateChangeInBodyLanguageErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(changeinbodylanguageid_ErrorLabel);
            else
                WaitForElementNotVisible(changeinbodylanguageid_ErrorLabel, 3);

            return this;
        }

        public PainManagementRecordPage ValidateBehaviouralChangeSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(behaviouralchangeid, ExpectedText);

            return this;
        }

        public PainManagementRecordPage SelectBehaviouralChange(string TextToSelect)
        {
            WaitForElementToBeClickable(behaviouralchangeid);
            SelectPicklistElementByText(behaviouralchangeid, TextToSelect);

            return this;
        }

        public PainManagementRecordPage ValidateBehaviouralChangeVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(behaviouralchangeid);
            else
                WaitForElementNotVisible(behaviouralchangeid, 3);

            return this;
        }

        public PainManagementRecordPage ValidateBehaviouralChangeErrorLabelText(string ExpectedText)
        {
            ValidateElementText(behaviouralchangeid_ErrorLabel, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateBehaviouralChangeErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(behaviouralchangeid_ErrorLabel);
            else
                WaitForElementNotVisible(behaviouralchangeid_ErrorLabel, 3);

            return this;
        }

        public PainManagementRecordPage ValidatePhysiologicalChangeSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(physiologicalchangeid, ExpectedText);

            return this;
        }

        public PainManagementRecordPage SelectPhysiologicalChange(string TextToSelect)
        {
            WaitForElementToBeClickable(physiologicalchangeid);
            SelectPicklistElementByText(physiologicalchangeid, TextToSelect);

            return this;
        }

        public PainManagementRecordPage ValidatePhysiologicalChangeVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(physiologicalchangeid);
            else
                WaitForElementNotVisible(physiologicalchangeid, 3);

            return this;
        }

        public PainManagementRecordPage ValidatePhysiologicalChangeErrorLabelText(string ExpectedText)
        {
            ValidateElementText(physiologicalchangeid_ErrorLabel, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidatePhysiologicalChangeErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(physiologicalchangeid_ErrorLabel);
            else
                WaitForElementNotVisible(physiologicalchangeid_ErrorLabel, 3);

            return this;
        }

        public PainManagementRecordPage ValidatePhysicalChangesSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(physicalchangesid, ExpectedText);

            return this;
        }

        public PainManagementRecordPage SelectPhysicalChanges(string TextToSelect)
        {
            WaitForElementToBeClickable(physicalchangesid);
            SelectPicklistElementByText(physicalchangesid, TextToSelect);

            return this;
        }

        public PainManagementRecordPage ValidatePhysicalChangesVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(physicalchangesid);
            else
                WaitForElementNotVisible(physicalchangesid, 3);

            return this;
        }

        public PainManagementRecordPage ValidatePhysicalChangesErrorLabelText(string ExpectedText)
        {
            ValidateElementText(physicalchangesid_ErrorLabel, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidatePhysicalChangesErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(physicalchangesid_ErrorLabel);
            else
                WaitForElementNotVisible(physicalchangesid_ErrorLabel, 3);

            return this;
        }

        public PainManagementRecordPage ValidateTotalPainScoreText(string ExpectedText)
        {
            ValidateElementValue(totalpainscore, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateTotalPainScoreVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(totalpainscore);
            else
                WaitForElementNotVisible(totalpainscore, 3);

            return this;
        }

        public PainManagementRecordPage ValidateSelectedTotalPainScoreDescription(string ExpectedText)
        {
            ValidatePicklistSelectedText(totalpainscoredescriptionid, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ClickReviewRequiredBySeniorColleague_YesRadioButton()
        {
            WaitForElementToBeClickable(reviewrequiredbyseniorcolleague_1);
            Click(reviewrequiredbyseniorcolleague_1);

            return this;
        }

        public PainManagementRecordPage ValidateReviewRequiredBySeniorColleague_YesRadioButtonChecked()
        {
            WaitForElement(reviewrequiredbyseniorcolleague_1);
            ValidateElementChecked(reviewrequiredbyseniorcolleague_1);

            return this;
        }

        public PainManagementRecordPage ValidateReviewRequiredBySeniorColleague_YesRadioButtonNotChecked()
        {
            WaitForElement(reviewrequiredbyseniorcolleague_1);
            ValidateElementNotChecked(reviewrequiredbyseniorcolleague_1);

            return this;
        }

        public PainManagementRecordPage ClickReviewRequiredBySeniorColleague_NoRadioButton()
        {
            WaitForElementToBeClickable(reviewrequiredbyseniorcolleague_0);
            Click(reviewrequiredbyseniorcolleague_0);

            return this;
        }

        public PainManagementRecordPage ValidateReviewRequiredBySeniorColleague_NoRadioButtonChecked()
        {
            WaitForElement(reviewrequiredbyseniorcolleague_0);
            ValidateElementChecked(reviewrequiredbyseniorcolleague_0);

            return this;
        }

        public PainManagementRecordPage ValidateReviewRequiredBySeniorColleague_NoRadioButtonNotChecked()
        {
            WaitForElement(reviewrequiredbyseniorcolleague_0);
            ValidateElementNotChecked(reviewrequiredbyseniorcolleague_0);

            return this;
        }

        public PainManagementRecordPage ValidateReviewDetailsText(string ExpectedText)
        {
            ValidateElementValue(reviewdetails, ExpectedText);

            return this;
        }

        public PainManagementRecordPage InsertTextOnReviewDetails(string TextToInsert)
        {
            WaitForElementToBeClickable(reviewdetails);
            SendKeys(reviewdetails, TextToInsert + Keys.Tab);

            return this;
        }

        public PainManagementRecordPage ValidateReviewDetailsVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(reviewdetails);
            else
                WaitForElementNotVisible(reviewdetails, 3);

            return this;
        }

        public PainManagementRecordPage ValidateReviewDetailsErrorLabelText(string ExpectedText)
        {
            ValidateElementText(reviewdetails_ErrorLabel, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateReviewDetailsErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(reviewdetails_ErrorLabel);
            else
                WaitForElementNotVisible(reviewdetails_ErrorLabel, 3);

            return this;
        }

        public PainManagementRecordPage ValidateTotalPainScoreDescriptionVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(totalpainscoredescriptionid);
            else
                WaitForElementNotVisible(totalpainscoredescriptionid, 3);

            return this;
        }

        public PainManagementRecordPage ClickLocation_SelectedElementLink(string ElementId)
        {
            WaitForElementToBeClickable(Location_SelectedElementLink(ElementId));
            Click(Location_SelectedElementLink(ElementId));

            return this;
        }

        public PainManagementRecordPage ValidateLocation_SelectedElementLinkText(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(Location_SelectedElementLink(ElementId));
            ValidateElementText(Location_SelectedElementLink(ElementId), ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateLocation_SelectedElementLinkText(Guid ElementId, string ExpectedText)
        {
            return ValidateLocation_SelectedElementLinkText(ElementId.ToString(), ExpectedText);
        }

        public PainManagementRecordPage ClickLocation_SelectedElementRemoveButton(string ElementId)
        {
            WaitForElementToBeClickable(Location_SelectedElementRemoveButton(ElementId));
            Click(Location_SelectedElementRemoveButton(ElementId));

            return this;
        }

        public PainManagementRecordPage ClickLocationLookupButton()
        {
            WaitForElementToBeClickable(CarephysicallocationidLookupButton);
            Click(CarephysicallocationidLookupButton);

            return this;
        }

        public PainManagementRecordPage ValidateLocationIfOtherText(string ExpectedText)
        {
            ValidateElementText(locationifother, ExpectedText);

            return this;
        }

        public PainManagementRecordPage InsertTextOnLocationIfOther(string TextToInsert)
        {
            WaitForElementToBeClickable(locationifother);
            SendKeys(locationifother, TextToInsert + Keys.Tab);

            return this;
        }

        public PainManagementRecordPage ValidateLocationIfOtherVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(locationifother);
            else
                WaitForElementNotVisible(locationifother, 3);

            return this;
        }

        public PainManagementRecordPage ClickWellbeingLink()
        {
            WaitForElementToBeClickable(CarewellbeingidLink);
            Click(CarewellbeingidLink);

            return this;
        }

        public PainManagementRecordPage ValidateWellbeingLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CarewellbeingidLink);
            ValidateElementText(CarewellbeingidLink, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ClickWellbeingClearButton()
        {
            WaitForElementToBeClickable(CarewellbeingidClearButton);
            Click(CarewellbeingidClearButton);

            return this;
        }

        public PainManagementRecordPage ClickWellbeingLookupButton()
        {
            WaitForElementToBeClickable(CarewellbeingidLookupButton);
            Click(CarewellbeingidLookupButton);

            return this;
        }

        public PainManagementRecordPage ValidateActionTakenText(string ExpectedText)
        {
            ValidateElementText(actiontaken, ExpectedText);

            return this;
        }

        public PainManagementRecordPage InsertTextOnActionTaken(string TextToInsert)
        {
            WaitForElementToBeClickable(actiontaken);
            SendKeys(actiontaken, TextToInsert + Keys.Tab);

            return this;
        }

        public PainManagementRecordPage ValidateActionTakenVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(actiontaken);
            else
                WaitForElementNotVisible(actiontaken, 3);

            return this;
        }

        public PainManagementRecordPage ValidateTotalTimeSpentWithPersonText(string ExpectedText)
        {
            ValidateElementValue(Timespentwithclient, ExpectedText);

            return this;
        }

        public PainManagementRecordPage InsertTextOnTotalTimeSpentWithPerson(string TextToInsert)
        {
            WaitForElementToBeClickable(Timespentwithclient);
            SendKeys(Timespentwithclient, TextToInsert + Keys.Tab);

            return this;
        }

        public PainManagementRecordPage ValidateAdditionalNotesText(string ExpectedText)
        {
            ValidateElementText(Additionalnotes, ExpectedText);

            return this;
        }

        public PainManagementRecordPage InsertTextOnAdditionalNotes(string TextToInsert)
        {
            WaitForElementToBeClickable(Additionalnotes);
            SendKeys(Additionalnotes, TextToInsert + Keys.Tab);

            return this;
        }

        public PainManagementRecordPage ClickAssistanceNeededLink()
        {
            WaitForElementToBeClickable(CareassistanceneededidLink);
            Click(CareassistanceneededidLink);

            return this;
        }

        public PainManagementRecordPage ValidateAssistanceNeededLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CareassistanceneededidLink);
            ValidateElementText(CareassistanceneededidLink, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ClickAssistanceNeededClearButton()
        {
            WaitForElementToBeClickable(CareassistanceneededidClearButton);
            Click(CareassistanceneededidClearButton);

            return this;
        }

        public PainManagementRecordPage ClickAssistanceNeededLookupButton()
        {
            WaitForElementToBeClickable(CareassistanceneededidLookupButton);
            Click(CareassistanceneededidLookupButton);

            return this;
        }

        public PainManagementRecordPage SelectAssistanceAmount(string TextToSelect)
        {
            WaitForElementToBeClickable(careassistancelevelid);
            SelectPicklistElementByText(careassistancelevelid, TextToSelect);

            return this;
        }

        public PainManagementRecordPage ValidateAssistanceAmountSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(careassistancelevelid, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateAssistanceAmountVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(careassistancelevelid);
            else
                WaitForElementNotVisible(careassistancelevelid, 3);

            return this;
        }

        public PainManagementRecordPage ValidateLocationErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Location_ErrorLabel, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateLocationErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Location_ErrorLabel);
            else
                WaitForElementNotVisible(Location_ErrorLabel, 3);

            return this;
        }

        public PainManagementRecordPage ValidateLocationIfOtherErrorLabelText(string ExpectedText)
        {
            ValidateElementText(LocationIfOther_ErrorLabel, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateLocationIfOtherErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(LocationIfOther_ErrorLabel);
            else
                WaitForElementNotVisible(LocationIfOther_ErrorLabel, 3);

            return this;
        }

        public PainManagementRecordPage ValidateWellbeingErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Wellbeing_ErrorLabel, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateWellbeingErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Wellbeing_ErrorLabel);
            else
                WaitForElementNotVisible(Wellbeing_ErrorLabel, 3);

            return this;
        }

        public PainManagementRecordPage ValidateActionTakenErrorLabelText(string ExpectedText)
        {
            ValidateElementText(ActionTaken_ErrorLabel, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateActionTakenErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ActionTaken_ErrorLabel);
            else
                WaitForElementNotVisible(ActionTaken_ErrorLabel, 3);

            return this;
        }

        public PainManagementRecordPage ValidateTotalTimeSpentWithPersonErrorLabelText(string ExpectedText)
        {
            ValidateElementText(TotalTimeSpentWithPerson_ErrorLabel, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateTotalTimeSpentWithPersonErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(TotalTimeSpentWithPerson_ErrorLabel);
            else
                WaitForElementNotVisible(TotalTimeSpentWithPerson_ErrorLabel, 3);

            return this;
        }

        public PainManagementRecordPage ValidateAssistanceNeededErrorLabelText(string ExpectedText)
        {
            ValidateElementText(AssistanceNeeded_ErrorLabel, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateAssistanceNeededErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(AssistanceNeeded_ErrorLabel);
            else
                WaitForElementNotVisible(AssistanceNeeded_ErrorLabel, 3);

            return this;
        }

        public PainManagementRecordPage ValidateAssistanceAmountErrorLabelText(string ExpectedText)
        {
            ValidateElementText(AssistanceAmount_ErrorLabel, ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateAssistanceAmountErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(AssistanceAmount_ErrorLabel);
            else
                WaitForElementNotVisible(AssistanceAmount_ErrorLabel, 3);

            return this;
        }

        public PainManagementRecordPage ClickStaffRequired_SelectedElementLink(string ElementId)
        {
            WaitForElementToBeClickable(StaffRequired_SelectedElementLink(ElementId));
            Click(StaffRequired_SelectedElementLink(ElementId));

            return this;
        }

        public PainManagementRecordPage ValidateStaffRequired_SelectedElementLinkTextBeforeSave(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(StaffRequired_SelectedElementLinkBeforeSave(ElementId, ExpectedText));

            return this;
        }

        public PainManagementRecordPage ValidateStaffRequired_SelectedElementLinkTextBeforeSave(Guid ElementId, string ExpectedText)
        {
            return ValidateStaffRequired_SelectedElementLinkTextBeforeSave(ElementId.ToString(), ExpectedText);
        }

        public PainManagementRecordPage ValidateStaffRequired_SelectedElementLinkText(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(StaffRequired_SelectedElementLink(ElementId));
            ValidateElementText(StaffRequired_SelectedElementLink(ElementId), ExpectedText);

            return this;
        }

        public PainManagementRecordPage ValidateStaffRequired_SelectedElementLinkText(Guid ElementId, string ExpectedText)
        {
            return ValidateStaffRequired_SelectedElementLinkText(ElementId.ToString(), ExpectedText);
        }

        public PainManagementRecordPage ClickStaffRequired_SelectedElementRemoveButton(string ElementId)
        {
            WaitForElementToBeClickable(StaffRequired_SelectedElementRemoveButton(ElementId));
            Click(StaffRequired_SelectedElementRemoveButton(ElementId));

            return this;
        }

        public PainManagementRecordPage ClickStaffRequiredLookupButton()
        {
            WaitForElementToBeClickable(staffrequiredLookupButton);
            Click(staffrequiredLookupButton);

            return this;
        }

        public PainManagementRecordPage ValidateCareNoteText(string ExpectedText)
        {
            var elementText = this.GetElementValueByJavascript("CWField_carenote");
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PainManagementRecordPage InsertTextOnCareNote(string TextToInsert)
        {
            WaitForElementToBeClickable(Carenote);
            SendKeys(Carenote, TextToInsert + Keys.Tab);
            return this;
        }

        public PainManagementRecordPage ClickinkedActivitiesOfDailyLivingLookupButton()
        {
            WaitForElementToBeClickable(LinkedadlcategoriesidLookupButton);
            Click(LinkedadlcategoriesidLookupButton);

            return this;
        }

        public PainManagementRecordPage ClickIncludeInNextHandover_YesRadioButton()
        {
            WaitForElementToBeClickable(Isincludeinnexthandover_1);
            Click(Isincludeinnexthandover_1);

            return this;
        }

        public PainManagementRecordPage ValidateIncludeInNextHandover_YesRadioButtonChecked()
        {
            WaitForElement(Isincludeinnexthandover_1);
            ValidateElementChecked(Isincludeinnexthandover_1);

            return this;
        }

        public PainManagementRecordPage ValidateIncludeInNextHandover_YesRadioButtonNotChecked()
        {
            WaitForElement(Isincludeinnexthandover_1);
            ValidateElementNotChecked(Isincludeinnexthandover_1);

            return this;
        }

        public PainManagementRecordPage ClickIncludeInNextHandover_NoRadioButton()
        {
            WaitForElementToBeClickable(Isincludeinnexthandover_0);
            Click(Isincludeinnexthandover_0);

            return this;
        }

        public PainManagementRecordPage ValidateIncludeInNextHandover_NoRadioButtonChecked()
        {
            WaitForElement(Isincludeinnexthandover_0);
            ValidateElementChecked(Isincludeinnexthandover_0);

            return this;
        }

        public PainManagementRecordPage ValidateIncludeInNextHandover_NoRadioButtonNotChecked()
        {
            WaitForElement(Isincludeinnexthandover_0);
            ValidateElementNotChecked(Isincludeinnexthandover_0);

            return this;
        }

        public PainManagementRecordPage ClickFlagRecordForHandover_YesRadioButton()
        {
            WaitForElementToBeClickable(Flagrecordforhandover_1);
            Click(Flagrecordforhandover_1);

            return this;
        }

        public PainManagementRecordPage ValidateFlagRecordForHandover_YesRadioButtonChecked()
        {
            WaitForElement(Flagrecordforhandover_1);
            ValidateElementChecked(Flagrecordforhandover_1);

            return this;
        }

        public PainManagementRecordPage ValidateFlagRecordForHandover_YesRadioButtonNotChecked()
        {
            WaitForElement(Flagrecordforhandover_1);
            ValidateElementNotChecked(Flagrecordforhandover_1);

            return this;
        }

        public PainManagementRecordPage ClickFlagRecordForHandover_NoRadioButton()
        {
            WaitForElementToBeClickable(Flagrecordforhandover_0);
            Click(Flagrecordforhandover_0);

            return this;
        }

        public PainManagementRecordPage ValidateFlagRecordForHandover_NoRadioButtonChecked()
        {
            WaitForElement(Flagrecordforhandover_0);
            ValidateElementChecked(Flagrecordforhandover_0);

            return this;
        }

        public PainManagementRecordPage ValidateFlagRecordForHandover_NoRadioButtonNotChecked()
        {
            WaitForElement(Flagrecordforhandover_0);
            ValidateElementNotChecked(Flagrecordforhandover_0);

            return this;
        }

    }
}


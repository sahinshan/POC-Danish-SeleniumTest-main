using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Phoenix.UITests.Framework.PageObjects.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonPhysicalObservationsRecordPage : CommonMethods
    {
        public PersonPhysicalObservationsRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']");

        readonly By activitiesDetailsElementExpanded = By.XPath("//span[text()='Activities']/parent::div/parent::summary/parent::details[@open]");
        readonly By activitiesLeftSubMenu = By.XPath("//details/summary/div/span[text()='Activities']");

        readonly By caseNotestLeftSubMenuItem = By.Id("CWNavItem_PersonPhysicalObservationCaseNote");


        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personphysicalobservation&')]");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Person Physical Observation: ']");

        #region Options toolbar

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
        readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");

        #endregion

        #region General
        readonly By PersonidLink = By.XPath("//*[@id='CWField_personid_Link']");
        readonly By PersonidLookupButton = By.XPath("//*[@id='CWLookupBtn_personid']");
        readonly string preferences_Id = "CWField_preferences";
        readonly By Preferences = By.XPath("//*[@id='CWField_preferences']");
        readonly By Careconsentgivenid = By.XPath("//*[@id='CWField_careconsentgivenid']");
        readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By Datetimetaken = By.XPath("//*[@id='CWField_datetimetaken']");
        readonly By DatetimetakenDatePicker = By.XPath("//*[@id='CWField_datetimetaken_DatePicker']");
        readonly By Datetimetaken_Time = By.XPath("//*[@id='CWField_datetimetaken_Time']");
        readonly By Datetimetaken_Time_TimePicker = By.XPath("//*[@id='CWField_datetimetaken_Time_TimePicker']");

        readonly By ReviewRequiredbyseniorcolleague_YesOption = By.XPath("//*[@id='CWField_reviewrequiredbyseniorcolleague_1']");
        readonly By ReviewRequiredbyseniorcolleague_NoOption = By.XPath("//*[@id='CWField_reviewrequiredbyseniorcolleague_0']");
        readonly By ReviewDetails = By.XPath("//*[@id='CWField_reviewdetails']");

        //Fields displayed when Consent Given = No
        readonly By nonconsentDetail = By.Id("CWField_carenonconsentid");

        readonly By reasonforabsence = By.Id("CWField_reasonforabsence");

        readonly By reasonconsentdeclined = By.Id("CWField_reasonconsentdeclined");
        readonly By encouragementgiven = By.Id("CWField_encouragementgiven");
        readonly By careprovidedwithoutconsent_YesRadioButton = By.Id("CWField_careprovidedwithoutconsent_1");
        readonly By careprovidedwithoutconsent_NoRadioButton = By.Id("CWField_careprovidedwithoutconsent_0");

        readonly By deferredToDate = By.Id("CWField_deferredtodate");
        readonly By deferredToDate_DatePicker = By.Id("CWField_deferredtodate_DatePicker");
        readonly By deferredToDate_ErrorLabel = By.XPath("//*[@id='CWControlHolder_deferredtodate']/label/span");

        readonly By deferredToTimeOrShift = By.Id("CWField_timeorshiftid");
        readonly By deferredToTimeOrShift_ErrorLabel = By.XPath("//*[@id='CWControlHolder_timeorshiftid']/label/span");

        readonly By deferredToTime = By.Id("CWField_deferredtotime");
        readonly By deferredToTime_TimePicker = By.Id("CWField_deferredtotime_TimePicker");
        readonly By deferredToTime_ErrorLabel = By.XPath("//*[@id='CWControlHolder_deferredtotime']/label/span");

        readonly By deferredToShift_LookupButton = By.Id("CWLookupBtn_deferredtoselectedshiftid");
        readonly By deferredToShift_LinkField = By.Id("CWField_deferredtoselectedshiftid_Link");
        readonly By deferredToShift_ClearButton = By.Id("CWClearLookup_deferredtoselectedshiftid");
        readonly By deferredToShift_ErrorLabel = By.XPath("//*[@id='CWControlHolder_deferredtoselectedshiftid']/label/span");

        #endregion

        #region Additional Information

        By carephysicallocation_SelectedElementLink(string ElementId) => By.XPath("//*[@id='MS_carephysicallocationid_" + ElementId + "']/a[@id='" + ElementId + "_Link']");
        By carephysicallocation_SelectedElementRemoveButton(string ElementId) => By.XPath("//*[@id='MS_carephysicallocationid_" + ElementId + "']/a[text()='Remove']");
        readonly By CarephysicallocationidLookupButton = By.XPath("//*[@id='CWLookupBtn_carephysicallocationid']");
        readonly By LocationIfOtherTextareaField = By.Id("CWField_locationifother");

        readonly By CarewellbeingidLink = By.XPath("//*[@id='CWField_carewellbeingid_Link']");
        readonly By CarewellbeingidClearButton = By.XPath("//*[@id='CWClearLookup_carewellbeingid']");
        readonly By CarewellbeingidLookupButton = By.XPath("//*[@id='CWLookupBtn_carewellbeingid']");
        readonly By ActionTakenHasPainReliefBeenOfferedTextareaField = By.Id("CWField_actiontaken");
        readonly By Totaltimespentwithclientminutes = By.XPath("//*[@id='CWField_totaltimespentwithclientminutes']");
        readonly By Additionalnotes = By.XPath("//*[@id='CWField_additionalnotes']");

        By Equipment_SelectedElementLink(string ElementId) => By.XPath("//*[@id='MS_equipment_" + ElementId + "']/a[@id='" + ElementId + "_Link']");
        By Equipment_SelectedElementRemoveButton(string ElementId) => By.XPath("//*[@id='MS_equipment_" + ElementId + "']/a[text()='Remove']");
        readonly By EquipmentLookupButton = By.XPath("//*[@id='CWLookupBtn_equipment']");
        readonly By EquipmentIfOtherTextareaField = By.Id("CWField_equipmentifother");

        readonly By CareassistanceneededidLink = By.XPath("//*[@id='CWField_careassistanceneededid_Link']");
        readonly By CareassistanceneededidClearButton = By.XPath("//*[@id='CWClearLookup_careassistanceneededid']");
        readonly By CareassistanceneededidLookupButton = By.XPath("//*[@id='CWLookupBtn_careassistanceneededid']");
        readonly By StaffrequiredLookupButton = By.XPath("//*[@id='CWLookupBtn_staffrequired']");
        readonly By AssistanceAmountPicklist = By.Id("CWField_careassistancelevelid");

        By Otherstaffwhoassistedid_SelectedElementLink(string ElementId) => By.XPath("//*[@id='MS_otherstaffwhoassistedid_" + ElementId + "']/a[@id='" + ElementId + "_Link']");
        By Otherstaffwhoassistedid_SelectedElementRemoveButton(string ElementId) => By.XPath("//*[@id='MS_otherstaffwhoassistedid_" + ElementId + "']/a[text()='Remove']");
        readonly By OtherstaffwhoassistedidLookupButton = By.XPath("//*[@id='CWLookupBtn_otherstaffwhoassistedid']");

        By StaffRequired_SelectedOption(string optionId) => By.XPath("//*[@id='MS_staffrequired_" + optionId + "']/a[@id = '" + optionId + "_Link']");
        #endregion

        #region Care note, Care need, Handover        

        readonly string CarenoteFieldId = "CWField_carenote";
        readonly By Carenote = By.XPath("//*[@id='CWField_carenote']");
        By LinkedADL_DomainOfNeed_SelectedOption(string optionId) => By.XPath("//*[@id='MS_careplanneeddomainid_" + optionId + "']/a[@id='" + optionId + "_Link']");
        readonly By LinkedActivitiesOfDailyLiving_DomainOfNeedLookupButton = By.XPath("//*[@id='CWLookupBtn_careplanneeddomainid']");
        readonly By Isincludeinnexthandover_YesRadioButton = By.XPath("//*[@id='CWField_isincludeinnexthandover_1']");
        readonly By Isincludeinnexthandover_NoRadioButton = By.XPath("//*[@id='CWField_isincludeinnexthandover_0']");
        readonly By Flagrecordforhandover_YesRadioButton = By.XPath("//*[@id='CWField_flagrecordforhandover_1']");
        readonly By Flagrecordforhandover_NoRadioButton = By.XPath("//*[@id='CWField_flagrecordforhandover_0']");

        #endregion

        #region Field Labels

        By FieldLabel(string FieldName) => By.XPath("//*[starts-with(@id, 'CWLabelHolder_')]/*[text() = '" + FieldName + "']");
        By MandatoryField_Label(string FieldName) => By.XPath("//label[text()='" + FieldName + "']/span[@class='mandatory']");

        #endregion

        #region Visual A-E Assessment Section

        readonly By TalkingInSentencesPicklist = By.XPath("//*[@id='CWField_talkinginsentencesid']");
        readonly By BreathingIsNoisyPicklist = By.XPath("//*[@id='CWField_breathingisnoisyid']");
        readonly By BreathingBetween11to25Picklist = By.XPath("//*[@id='CWField_breathingbetween11to25id']");
        readonly By LabouredBreathingPicklist = By.XPath("//*[@id='CWField_labouredbreathingid']");
        readonly By AbleToStandUnaidedPicklist = By.XPath("//*[@id='CWField_abletostandunaidedid']");
        readonly By UnableToStandUnaidedPicklist = By.XPath("//*[@id='CWField_unabletostandunaidedid']");
        readonly By AlertAndResponsivePicklist = By.XPath("//*[@id='CWField_alertandresponsiveid']");
        readonly By SleepyAndDrowsyPicklist = By.XPath("//*[@id='CWField_sleepyanddrowsyid']");
        readonly By DoesNotHavePhysicalHealthProblemPicklist = By.XPath("//*[@id='CWField_doesnothavephysicalhealthproblemid']");
        readonly By HasPhysicalHealthProblemPicklist = By.XPath("//*[@id='CWField_hasphysicalhealthproblemid']");

        #endregion

        #region NEWS type

        readonly By Temperature = By.XPath("//*[@id='CWField_temperature']");
        readonly By TemperatureRouteid = By.XPath("//*[@id='CWField_temperaturerouteid']");
        readonly By Vitalsignsactionstakenrequired = By.XPath("//*[@id='CWField_vitalsignsactionstakenrequired']");
        readonly By Temperatureearlywarningscore = By.XPath("//*[@id='CWField_temperatureearlywarningscore']");
        readonly By Bpsystolic = By.XPath("//*[@id='CWField_bpsystolic']");
        readonly By Bpdiastolic = By.XPath("//*[@id='CWField_bpdiastolic']");
        readonly By Requiresecondarybpreading_YesOption = By.XPath("//*[@id='CWField_requiresecondarybpreading_1']");
        readonly By Requiresecondarybpreading_NoOption = By.XPath("//*[@id='CWField_requiresecondarybpreading_0']");
        readonly By Secondarybpsystolic = By.XPath("//*[@id='CWField_secondarybpsystolic']");
        readonly By Secondarybpdiastolic = By.XPath("//*[@id='CWField_secondarybpdiastolic']");
        readonly By Bloodpressurereadingactionstakenrequired = By.XPath("//*[@id='CWField_bloodpressurereadingactionstakenrequired']");
        readonly By Bpreadingtypeid = By.XPath("//*[@id='CWField_bpreadingtypeid']");
        readonly By Bpearlywarningscore = By.XPath("//*[@id='CWField_bpearlywarningscore']");
        readonly By Secondarybpreadingtypeid = By.XPath("//*[@id='CWField_secondarybpreadingtypeid']");
        readonly By Secondarybpearlywarningscore = By.XPath("//*[@id='CWField_secondarybpearlywarningscore']");
        readonly By Pulse = By.XPath("//*[@id='CWField_pulse']");
        readonly By Ispulseregularorirregularid = By.XPath("//*[@id='CWField_ispulseregularorirregularid']");
        readonly By Personphysicalobservationwhenpulsetakenid = By.XPath("//*[@id='CWField_personphysicalobservationwhenpulsetakenid']");
        readonly By Pulseearlywarningscore = By.XPath("//*[@id='CWField_pulseearlywarningscore']");
        readonly By Pulseactionstakenrequired = By.XPath("//*[@id='CWField_pulseactionstakenrequired']");
        readonly By Respiration = By.XPath("//*[@id='CWField_respiration']");
        readonly By Respirationactionstakenrequired = By.XPath("//*[@id='CWField_respirationactionstakenrequired']");
        readonly By Respiratorydistressearlywarningscore = By.XPath("//*[@id='CWField_respiratorydistressearlywarningscore']");
        readonly By Oxygensaturation = By.XPath("//*[@id='CWField_oxygensaturation']");
        readonly By Peakflow = By.XPath("//*[@id='CWField_peakflow']");
        readonly By Oxygensaturationactionstakenrequired = By.XPath("//*[@id='CWField_oxygensaturationactionstakenrequired']");
        readonly By Oxygensaturationearlywarningscore = By.XPath("//*[@id='CWField_oxygensaturationearlywarningscore']");
        readonly By Bloodsugarlevel = By.XPath("//*[@id='CWField_bloodsugarlevel']");
        readonly By Bloodglucoseactionstakenrequired = By.XPath("//*[@id='CWField_bloodglucoseactionstakenrequired']");
        readonly By Bloodsugarwhenwasreadingtakenid = By.XPath("//*[@id='CWField_bloodsugarwhenwasreadingtakenid']");
        readonly By Rightpupilsizereactionid = By.XPath("//*[@id='CWField_rightpupilsizereactionid']");
        readonly By Rightpupilreactiontypeid = By.XPath("//*[@id='CWField_rightpupilreactiontypeid']");
        readonly By Neurologicalobservationsactionstakenrequired = By.XPath("//*[@id='CWField_neurologicalobservationsactionstakenrequired']");
        readonly By Leftpupilsizereactionid = By.XPath("//*[@id='CWField_leftpupilsizereactionid']");
        readonly By Leftpupilreactiontypeid = By.XPath("//*[@id='CWField_leftpupilreactiontypeid']");
        readonly By Totalscore = By.XPath("//*[@id='CWField_totalscore']");
        readonly By Totalscoresactionstakenrequired = By.XPath("//*[@id='CWField_totalscoresactionstakenrequired']");

        #endregion

        public PersonPhysicalObservationsRecordPage WaitForPersonPhysicalObservationsRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 10);
            WaitForElement(pageHeader);

            return this;
        }

        public PersonPhysicalObservationsRecordPage NavigateToPersonPhysicalObservationCaseNotesPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(activitiesDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(activitiesLeftSubMenu);
                Click(activitiesLeftSubMenu);
            }

            WaitForElementToBeClickable(caseNotestLeftSubMenuItem);
            Click(caseNotestLeftSubMenuItem);

            return this;
        }

        //CP Physical Observation record fields and validations
        public PersonPhysicalObservationsRecordPage VerifyPageHeaderText(string ExpectedText)
        {
            WaitForElementVisible(pageHeader);
            string pageTitle = GetElementByAttributeValue(pageHeader, "title");
            Assert.AreEqual("Height & Weight: " + ExpectedText, pageTitle);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateFieldIsVisible(string FieldName, bool ExpectVisible = true)
        {
            if (ExpectVisible)
                WaitForElementVisible(FieldLabel(FieldName));
            else
                WaitForElementNotVisible(FieldLabel(FieldName), 3);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateMandatoryFieldIsVisible(string FieldName, bool ExpectVisible = true)
        {
            if (ExpectVisible)
                WaitForElementVisible(MandatoryField_Label(FieldName));
            else
                WaitForElementNotVisible(MandatoryField_Label(FieldName), 3);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickPersonidLink()
        {
            WaitForElementToBeClickable(PersonidLink);
            Click(PersonidLink);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidatePersonidLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(PersonidLink);
            ValidateElementText(PersonidLink, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickPersonidLookupButton()
        {
            WaitForElementToBeClickable(PersonidLookupButton);
            Click(PersonidLookupButton);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidatePreferencesText(string ExpectedText)
        {
            WaitForElement(Preferences);
            ScrollToElement(Preferences);
            ValidateElementValueByJavascript(preferences_Id, ExpectedText);

            return this;
        }

        //verify preferences textare field is displayed or not displayed
        public PersonPhysicalObservationsRecordPage VerifyPreferencesTextAreaFieldIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
            {
                WaitForElementVisible(Preferences);
            }
            else
            {
                WaitForElementNotVisible(Preferences, 2);
            }

            return this;
        }

        //verify preferences textare field is disabled or not disabled
        public PersonPhysicalObservationsRecordPage VerifyPreferencesTextAreaFieldIsDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
            {
                ValidateElementAttribute(Preferences, "disabled", "true");
            }
            else
            {
                ValidateElementAttribute(Preferences, "disabled", "false");
            }

            return this;
        }


        public PersonPhysicalObservationsRecordPage InsertTextOnPreferences(string TextToInsert)
        {
            WaitForElementToBeClickable(Preferences);
            SendKeys(Preferences, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsRecordPage SelectConsentGiven(string TextToSelect)
        {
            WaitForElementToBeClickable(Careconsentgivenid);
            SelectPicklistElementByText(Careconsentgivenid, TextToSelect);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateConsentGivenSelectedText(string ExpectedText)
        {
            ValidatePicklistContainsElementByText(Careconsentgivenid, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage SelectNonConsentDetail(string TextToSelect)
        {
            WaitForElementVisible(nonconsentDetail);
            SelectPicklistElementByText(nonconsentDetail, TextToSelect);

            return this;
        }

        //verify nonconsentDetail picklist options
        public PersonPhysicalObservationsRecordPage ValidateNonConsentDetailOptions(string OptionText, bool ExpectedPresent)
        {
            WaitForElement(nonconsentDetail);
            if (ExpectedPresent)
                ValidatePicklistContainsElementByText(nonconsentDetail, OptionText);
            else
                ValidatePicklistContainsElementByText(nonconsentDetail, OptionText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateSelectedNonConsentDetail(string ExpectedText)
        {
            ValidatePicklistSelectedText(nonconsentDetail, ExpectedText);

            return this;
        }

        //verify nonconsentDetail field is displayed or not displayed
        public PersonPhysicalObservationsRecordPage ValidateNonConsentDetailFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(nonconsentDetail);
            else
                WaitForElementNotVisible(nonconsentDetail, 3);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextInReasonForAbsence(string TextToInsert)
        {
            WaitForElementVisible(reasonforabsence);
            SendKeys(reasonforabsence, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateReasonForAbsence(string ExpectedText)
        {
            ValidateElementValue(reasonforabsence, ExpectedText);

            return this;
        }

        //verify reasonforabsence field is displayed or not displayed
        public PersonPhysicalObservationsRecordPage ValidateReasonForAbsenceFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(reasonforabsence);
            else
                WaitForElementNotVisible(reasonforabsence, 3);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextInReasonConsentDeclined(string TextToInsert)
        {
            WaitForElementVisible(reasonconsentdeclined);
            SendKeys(reasonconsentdeclined, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateReasonConsentDeclined(string ExpectedText)
        {
            ValidateElementValue(reasonconsentdeclined, ExpectedText);

            return this;
        }

        //verify reasonconsentdeclined field is displayed or not displayed
        public PersonPhysicalObservationsRecordPage ValidateReasonConsentDeclinedFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(reasonconsentdeclined);
            else
                WaitForElementNotVisible(reasonconsentdeclined, 3);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextInEncouragementGiven(string TextToInsert)
        {
            WaitForElementVisible(encouragementgiven);
            SendKeys(encouragementgiven, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateEncouragementGiven(string ExpectedText)
        {
            ValidateElementValue(encouragementgiven, ExpectedText);

            return this;
        }

        //verify encouragementgiven field is displayed or not displayed
        public PersonPhysicalObservationsRecordPage ValidateEncouragementGivenFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(encouragementgiven);
            else
                WaitForElementNotVisible(encouragementgiven, 3);

            return this;
        }

        //Verify encouragementgiven field label tooltip
        public PersonPhysicalObservationsRecordPage ValidateEncouragementGivenFieldTooltip(string ExpectedText)
        {
            ValidateElementToolTip(encouragementgiven, ExpectedText);
            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickCareProvidedWithoutConsent_YesRadioButton()
        {
            WaitForElementToBeClickable(careprovidedwithoutconsent_YesRadioButton);
            Click(careprovidedwithoutconsent_YesRadioButton);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateCareProvidedWithoutConsent_YesRadioButtonChecked()
        {
            WaitForElement(careprovidedwithoutconsent_YesRadioButton);
            ValidateElementChecked(careprovidedwithoutconsent_YesRadioButton);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateCareProvidedWithoutConsent_YesRadioButtonNotChecked()
        {
            WaitForElement(careprovidedwithoutconsent_YesRadioButton);
            ValidateElementNotChecked(careprovidedwithoutconsent_YesRadioButton);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickCareProvidedWithoutConsent_NoRadioButton()
        {
            WaitForElementToBeClickable(careprovidedwithoutconsent_NoRadioButton);
            Click(careprovidedwithoutconsent_NoRadioButton);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateCareProvidedWithoutConsent_NoRadioButtonChecked()
        {
            WaitForElement(careprovidedwithoutconsent_NoRadioButton);
            ValidateElementChecked(careprovidedwithoutconsent_NoRadioButton);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateCareProvidedWithoutConsent_NoRadioButtonNotChecked()
        {
            WaitForElement(careprovidedwithoutconsent_NoRadioButton);
            ValidateElementNotChecked(careprovidedwithoutconsent_NoRadioButton);

            return this;
        }

        //verify CareProvidedWithoutConsent options are displayed or not displayed
        public PersonPhysicalObservationsRecordPage ValidateCareProvidedWithoutConsentOptionsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(careprovidedwithoutconsent_YesRadioButton);
                WaitForElementVisible(careprovidedwithoutconsent_NoRadioButton);
            }
            else
            {
                WaitForElementNotVisible(careprovidedwithoutconsent_YesRadioButton, 3);
                WaitForElementNotVisible(careprovidedwithoutconsent_NoRadioButton, 3);
            }

            return this;
        }

        public PersonPhysicalObservationsRecordPage SetDeferredToDate(string TextToInsert)
        {
            WaitForElementVisible(deferredToDate);
            SendKeys(deferredToDate, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateDeferredToDate(string ExpectedText)
        {
            ValidateElementValue(deferredToDate, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateDeferredToDateErrorLabel(string ExpectedText)
        {
            WaitForElementVisible(deferredToDate_ErrorLabel);
            ValidateElementText(deferredToDate_ErrorLabel, ExpectedText);

            return this;
        }

        //Validate deferred to date field is displayed or not displayed
        public PersonPhysicalObservationsRecordPage ValidateDeferredToDateFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(deferredToDate);
            }
            else
            {
                WaitForElementNotVisible(deferredToDate, 3);
            }

            return this;
        }

        //Insert text on deferredToDate field
        public PersonPhysicalObservationsRecordPage InsertTextInDeferredToDate(string TextToInsert)
        {
            WaitForElementVisible(deferredToDate);
            SendKeys(deferredToDate, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        //click deferred to date datepicker
        public PersonPhysicalObservationsRecordPage ClickDeferredToDate_DatePicker()
        {
            WaitForElementToBeClickable(deferredToDate_DatePicker);
            Click(deferredToDate_DatePicker);

            return this;
        }

        //verify deferred to date datepicker is displayed or not displayed
        public PersonPhysicalObservationsRecordPage VerifyDeferredToDate_DatePickerIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
            {
                WaitForElementVisible(deferredToDate_DatePicker);
            }
            else
            {
                WaitForElementNotVisible(deferredToDate_DatePicker, 2);
            }

            return this;
        }

        public PersonPhysicalObservationsRecordPage SelectDeferredToTimeOrShift(string TextToSelect)
        {
            WaitForElementVisible(deferredToTimeOrShift);
            SelectPicklistElementByText(deferredToTimeOrShift, TextToSelect);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateSelectedDeferredToTimeOrShift(string ExpectedText)
        {
            ValidatePicklistSelectedText(deferredToTimeOrShift, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateDeferredToTimeOrShiftErrorLabel(string ExpectedText)
        {
            WaitForElementVisible(deferredToTimeOrShift_ErrorLabel);
            ValidateElementText(deferredToTimeOrShift_ErrorLabel, ExpectedText);

            return this;
        }

        //verify deferredToTimeorsift field is displayed or not displayed
        public PersonPhysicalObservationsRecordPage ValidateDeferredToTimeOrShiftFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(deferredToTimeOrShift);
            else
                WaitForElementNotVisible(deferredToTimeOrShift, 3);

            return this;
        }

        public PersonPhysicalObservationsRecordPage SetDeferredToTime(string TextToInsert)
        {
            WaitForElementVisible(deferredToTime);
            SendKeys(deferredToTime, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateDeferredToTime(string ExpectedText)
        {
            ValidateElementValue(deferredToTime, ExpectedText);

            return this;
        }

        //verify deferredToTime field is displayed or not displayed
        public PersonPhysicalObservationsRecordPage ValidateDeferredToTimeFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(deferredToTime);
            else
                WaitForElementNotVisible(deferredToTime, 3);

            return this;
        }

        //click deferredToTime_TimePicker field
        public PersonPhysicalObservationsRecordPage ClickDeferredToTime_TimePicker()
        {
            WaitForElement(deferredToTime);
            Click(deferredToTime);
            SendKeys(deferredToTime, Keys.Tab);

            WaitForElement(deferredToTime_TimePicker);
            ScrollToElement(deferredToTime_TimePicker);
            Click(deferredToTime_TimePicker);

           return this;
        }

        //veify deferredToTime_TimePicker field is displayed or not displayed
        public PersonPhysicalObservationsRecordPage ValidateDeferredToTime_TimePickerVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(deferredToTime_TimePicker);
            else
                WaitForElementNotVisible(deferredToTime_TimePicker, 3);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateDeferredToTimeErrorLabel(string ExpectedText)
        {
            WaitForElementVisible(deferredToTime_ErrorLabel);
            ValidateElementText(deferredToTime_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickDeferredToShiftLookupButton()
        {
            WaitForElementToBeClickable(deferredToShift_LookupButton);
            Click(deferredToShift_LookupButton);

            return this;
        }

        //verify deferredToShift_LookupButton is displayed or not displayed
        public PersonPhysicalObservationsRecordPage ValidateDeferredToShiftLookupButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(deferredToShift_LookupButton);
            else
                WaitForElementNotVisible(deferredToShift_LookupButton, 3);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateDeferredToShiftLinkText(string ExpectedText)
        {
            ValidateElementText(deferredToShift_LinkField, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickDeferredToShiftClearButton()
        {
            Click(deferredToShift_ClearButton);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateDeferredToShiftErrorLabel(string ExpectedText)
        {
            WaitForElementVisible(deferredToShift_ErrorLabel);
            ValidateElementText(deferredToShift_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickResponsibleTeamLink()
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            Click(ResponsibleTeamLink);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            ValidateElementText(ResponsibleTeamLink, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamLookupButton);
            Click(ResponsibleTeamLookupButton);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateDateAndTimeOccurred_DateText(string ExpectedText)
        {
            ValidateElementValue(Datetimetaken, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnDateAndTimeOccurred_DateField(string TextToInsert)
        {
            WaitForElementToBeClickable(Datetimetaken);
            SendKeys(Datetimetaken, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickDateAndTimeOccurred_DatePicker()
        {
            WaitForElementToBeClickable(DatetimetakenDatePicker);
            Click(DatetimetakenDatePicker);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateDateAndTimeOccurred_TimeText(string ExpectedText)
        {
            ValidateElementValue(Datetimetaken_Time, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnDateAndTimeOccurred_TimeField(string TextToInsert)
        {
            WaitForElementToBeClickable(Datetimetaken_Time);
            SendKeys(Datetimetaken_Time, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickDateAndTimeOccurred_TimePicker()
        {
            WaitForElement(Datetimetaken_Time);
            ScrollToElement(Datetimetaken_Time);
            Click(Datetimetaken_Time);

            WaitForElement(Datetimetaken_Time_TimePicker);
            ScrollToElement(Datetimetaken_Time_TimePicker);
            Click(Datetimetaken_Time_TimePicker);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickLocationLookupButton()
        {
            WaitForElementToBeClickable(CarephysicallocationidLookupButton);
            Click(CarephysicallocationidLookupButton);

            return this;
        }

        //verify CarephysicallocationidLookupButton is displayed or not displayed
        public PersonPhysicalObservationsRecordPage ValidateLocationLookupButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(CarephysicallocationidLookupButton);
            else
                WaitForElementNotVisible(CarephysicallocationidLookupButton, 3);

            return this;
        }

        //verify LocationIfOtherTextareaField is displayed or not displayed
        public PersonPhysicalObservationsRecordPage ValidateLocationIfOtherTextareaFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(LocationIfOtherTextareaField);
            else
                WaitForElementNotVisible(LocationIfOtherTextareaField, 3);

            return this;
        }

        //Insert text in LocationIfOtherTextareaField
        public PersonPhysicalObservationsRecordPage InsertTextInLocationIfOtherTextareaField(string TextToInsert)
        {
            WaitForElement(LocationIfOtherTextareaField);
            SendKeys(LocationIfOtherTextareaField, TextToInsert + Keys.Tab);

            return this;
        }

        //veriy LocationIfOtherTextareaField text
        public PersonPhysicalObservationsRecordPage ValidateLocationIfOtherTextareaFieldText(string ExpectedText)
        {
            ValidateElementValue(LocationIfOtherTextareaField, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickLocation_SelectedElementLink(string ElementId)
        {
            WaitForElementToBeClickable(carephysicallocation_SelectedElementLink(ElementId));
            Click(carephysicallocation_SelectedElementLink(ElementId));

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateLocation_SelectedElementLinkText(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(carephysicallocation_SelectedElementLink(ElementId));
            ValidateElementText(carephysicallocation_SelectedElementLink(ElementId), ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateLocation_SelectedElementLinkText(Guid ElementId, string ExpectedText)
        {
            return ValidateLocation_SelectedElementLinkText(ElementId.ToString(), ExpectedText);
        }

        public PersonPhysicalObservationsRecordPage ClickLocation_SelectedElementRemoveButton(string ElementId)
        {
            WaitForElementToBeClickable(carephysicallocation_SelectedElementRemoveButton(ElementId));
            Click(carephysicallocation_SelectedElementRemoveButton(ElementId));

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickWellbeingLookupButton()
        {
            WaitForElementToBeClickable(CarewellbeingidLookupButton);
            Click(CarewellbeingidLookupButton);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickWellbeingLink()
        {
            WaitForElementToBeClickable(CarewellbeingidLink);
            Click(CarewellbeingidLink);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateWellbeingLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CarewellbeingidLink);
            ValidateElementText(CarewellbeingidLink, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickWellbeingClearButton()
        {
            WaitForElementToBeClickable(CarewellbeingidClearButton);
            Click(CarewellbeingidClearButton);

            return this;
        }


        //Insert text in ActionTakenHasPainReliefBeenOfferedTextareaField
        public PersonPhysicalObservationsRecordPage InsertTextInActionTakenHasPainReliefBeenOfferedTextareaField(string TextToInsert)
        {
            WaitForElement(ActionTakenHasPainReliefBeenOfferedTextareaField);
            SendKeys(ActionTakenHasPainReliefBeenOfferedTextareaField, TextToInsert + Keys.Tab);

            return this;
        }

        //verify ActionTakenHasPainReliefBeenOfferedTextareaField text
        public PersonPhysicalObservationsRecordPage ValidateActionTakenHasPainReliefBeenOfferedTextareaFieldText(string ExpectedText)
        {
            ValidateElementValue(ActionTakenHasPainReliefBeenOfferedTextareaField, ExpectedText);

            return this;
        }

        //verify WellbeingidlookupButton is displayed or not displayed
        public PersonPhysicalObservationsRecordPage ValidateWellbeingLookupButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(CarewellbeingidLookupButton);
            else
                WaitForElementNotVisible(CarewellbeingidLookupButton, 3);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateTotalTimeSpentWithPersonMinutesText(string ExpectedText)
        {
            ValidateElementValue(Totaltimespentwithclientminutes, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnTotalTimeSpentWithPersonMinutes(string TextToInsert)
        {
            WaitForElementToBeClickable(Totaltimespentwithclientminutes);
            SendKeys(Totaltimespentwithclientminutes, TextToInsert);

            return this;
        }

        //verify totaltimespentwithclientminutes is visible or not visible
        public PersonPhysicalObservationsRecordPage ValidateTotalTimeSpentWithPersonMinutesFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Totaltimespentwithclientminutes);
            else
                WaitForElementNotVisible(Totaltimespentwithclientminutes, 3);

            return this;
        }

        //verify Totaltimespentwithclientminutes field range
        public PersonPhysicalObservationsRecordPage ValidateTotalTimeSpentWithPersonMinutesFieldRange(string ExpectedValue)
        {
            WaitForElement(Totaltimespentwithclientminutes);
            ScrollToElement(Totaltimespentwithclientminutes);
            ValidateElementAttribute(Totaltimespentwithclientminutes, "range", ExpectedValue);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateAdditionalNotesText(string ExpectedText)
        {
            ValidateElementText(Additionalnotes, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnAdditionalNotes(string TextToInsert)
        {
            WaitForElementToBeClickable(Additionalnotes);
            SendKeys(Additionalnotes, TextToInsert);

            return this;
        }

        //verify Additionalnotes is visible or not visible
        public PersonPhysicalObservationsRecordPage ValidateAdditionalNotesFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Additionalnotes);
            else
                WaitForElementNotVisible(Additionalnotes, 3);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickEquipmentLookupButton()
        {
            WaitForElementToBeClickable(EquipmentLookupButton);
            Click(EquipmentLookupButton);

            return this;
        }

        //verify EquipmentIfOtherTextareaField is displayed or not displayed
        public PersonPhysicalObservationsRecordPage ValidateEquipmentIfOtherTextareaFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(EquipmentIfOtherTextareaField);
            else
                WaitForElementNotVisible(EquipmentIfOtherTextareaField, 3);

            return this;
        }

        //Insert text in EquipmentIfOtherTextareaField
        public PersonPhysicalObservationsRecordPage InsertTextInEquipmentIfOtherTextareaField(string TextToInsert)
        {
            WaitForElement(EquipmentIfOtherTextareaField);
            SendKeys(EquipmentIfOtherTextareaField, TextToInsert + Keys.Tab);

            return this;
        }

        //veriy EquipmentIfOtherTextareaField text
        public PersonPhysicalObservationsRecordPage ValidateEquipmentIfOtherTextareaFieldText(string ExpectedText)
        {
            ValidateElementValue(EquipmentIfOtherTextareaField, ExpectedText);

            return this;
        }

        //verify EquipmentLookupButton is displayed or not displayed
        public PersonPhysicalObservationsRecordPage ValidateEquipmentLookupButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(EquipmentLookupButton);
            else
                WaitForElementNotVisible(EquipmentLookupButton, 3);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickEquipment_SelectedElementLink(string ElementId)
        {
            WaitForElementToBeClickable(Equipment_SelectedElementLink(ElementId));
            Click(Equipment_SelectedElementLink(ElementId));

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateEquipment_SelectedElementLinkText(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(Equipment_SelectedElementLink(ElementId));
            ValidateElementText(Equipment_SelectedElementLink(ElementId), ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateEquipment_SelectedElementLinkText(Guid ElementId, string ExpectedText)
        {
            return ValidateEquipment_SelectedElementLinkText(ElementId.ToString(), ExpectedText);
        }

        public PersonPhysicalObservationsRecordPage ClickEquipment_SelectedElementRemoveButton(string ElementId)
        {
            WaitForElementToBeClickable(Equipment_SelectedElementRemoveButton(ElementId));
            Click(Equipment_SelectedElementRemoveButton(ElementId));

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickAssistanceNeededLink()
        {
            WaitForElementToBeClickable(CareassistanceneededidLink);
            Click(CareassistanceneededidLink);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateAssistanceNeededLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CareassistanceneededidLink);
            ValidateElementText(CareassistanceneededidLink, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickAssistanceNeededClearButton()
        {
            WaitForElementToBeClickable(CareassistanceneededidClearButton);
            Click(CareassistanceneededidClearButton);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickAssistanceNeededLookupButton()
        {
            WaitForElementToBeClickable(CareassistanceneededidLookupButton);
            Click(CareassistanceneededidLookupButton);

            return this;
        }

        //verify CareassistanceneededidLookupButton is displayed or not displayed
        public PersonPhysicalObservationsRecordPage ValidateAssistanceNeededLookupButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(CareassistanceneededidLookupButton);
            else
                WaitForElementNotVisible(CareassistanceneededidLookupButton, 3);

            return this;
        }

        //Select value from CareAssistanceLevelPicklist
        public PersonPhysicalObservationsRecordPage SelectAssistanceAmountFromPicklist(string OptionText)
        {
            WaitForElementToBeClickable(AssistanceAmountPicklist);
            SelectPicklistElementByText(AssistanceAmountPicklist, OptionText);

            return this;
        }

        //verify AssistanceAmountPicklist is visible or not visible
        public PersonPhysicalObservationsRecordPage ValidateAssistanceAmountPicklistVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(AssistanceAmountPicklist);
            else
                WaitForElementNotVisible(AssistanceAmountPicklist, 3);
            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickStaffRequired_SelectedElementLink(string ElementId)
        {
            WaitForElementToBeClickable(Otherstaffwhoassistedid_SelectedElementLink(ElementId));
            Click(Otherstaffwhoassistedid_SelectedElementLink(ElementId));

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateStaffRequired_SelectedElementLinkText(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(Otherstaffwhoassistedid_SelectedElementLink(ElementId));
            ValidateElementText(Otherstaffwhoassistedid_SelectedElementLink(ElementId), ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickStaffRequired_SelectedElementRemoveButton(string ElementId)
        {
            WaitForElementToBeClickable(Otherstaffwhoassistedid_SelectedElementRemoveButton(ElementId));
            Click(Otherstaffwhoassistedid_SelectedElementRemoveButton(ElementId));

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickStaffrequiredLookupButton()
        {
            WaitForElementToBeClickable(StaffrequiredLookupButton);
            Click(StaffrequiredLookupButton);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateStaffRequiredLookupButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(StaffrequiredLookupButton);
            else
                WaitForElementNotVisible(StaffrequiredLookupButton, 3);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateCarenoteText(string ExpectedText)
        {
            WaitForElement(Carenote);
            ScrollToElement(Carenote);
            var fieldValue = GetElementValueByJavascript(CarenoteFieldId);
            Assert.AreEqual(ExpectedText, fieldValue);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnCarenote(string TextToInsert)
        {
            WaitForElementToBeClickable(Carenote);
            SendKeys(Carenote, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickLinkedActivitiesOfDailyLiving_DomainOfNeedLookupButton()
        {
            WaitForElement(LinkedActivitiesOfDailyLiving_DomainOfNeedLookupButton);
            ScrollToElement(LinkedActivitiesOfDailyLiving_DomainOfNeedLookupButton);
            Click(LinkedActivitiesOfDailyLiving_DomainOfNeedLookupButton);

            return this;
        }

        //verify LinkedActivitiesOfDailyLiving_DomainOfNeedLookupButton is displayed or not displayed
        public PersonPhysicalObservationsRecordPage ValidateLinkedActivitiesOfDailyLiving_DomainOfNeedLookupButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(LinkedActivitiesOfDailyLiving_DomainOfNeedLookupButton);
            else
                WaitForElementNotVisible(LinkedActivitiesOfDailyLiving_DomainOfNeedLookupButton, 3);

            return this;
        }

        //verify LinkedADL_DomainOfNeed_SelectedOption
        public PersonPhysicalObservationsRecordPage ValidateLinkedADL_DomainOfNeed_SelectedOption(string OptionId, string ExpectedText)
        {
            WaitForElement(LinkedADL_DomainOfNeed_SelectedOption(OptionId));
            ValidateElementText(LinkedADL_DomainOfNeed_SelectedOption(OptionId), ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickIncludeInNextHandover_YesRadioButton()
        {
            WaitForElementToBeClickable(Isincludeinnexthandover_YesRadioButton);
            Click(Isincludeinnexthandover_YesRadioButton);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateIncludeInNextHandover_YesRadioButtonChecked()
        {
            WaitForElement(Isincludeinnexthandover_YesRadioButton);
            ValidateElementChecked(Isincludeinnexthandover_YesRadioButton);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateIncludeInNextHandover_YesRadioButtonNotChecked()
        {
            WaitForElement(Isincludeinnexthandover_YesRadioButton);
            ValidateElementNotChecked(Isincludeinnexthandover_YesRadioButton);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickIncludeInNextHandover_NoRadioButton()
        {
            WaitForElementToBeClickable(Isincludeinnexthandover_NoRadioButton);
            Click(Isincludeinnexthandover_NoRadioButton);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateIncludeInNextHandover_NoRadioButtonChecked()
        {
            WaitForElement(Isincludeinnexthandover_NoRadioButton);
            ValidateElementChecked(Isincludeinnexthandover_NoRadioButton);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateIncludeInNextHandover_NoRadioButtonNotChecked()
        {
            WaitForElement(Isincludeinnexthandover_NoRadioButton);
            ValidateElementNotChecked(Isincludeinnexthandover_NoRadioButton);

            return this;
        }

        //verify IncludeInNextHandover options are displayed or not displayed
        public PersonPhysicalObservationsRecordPage ValidateIncludeInNextHandoverOptionsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Isincludeinnexthandover_YesRadioButton);
                WaitForElementVisible(Isincludeinnexthandover_NoRadioButton);
            }
            else
            {
                WaitForElementNotVisible(Isincludeinnexthandover_YesRadioButton, 3);
                WaitForElementNotVisible(Isincludeinnexthandover_NoRadioButton, 3);
            }

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickFlagRecordForHandover_YesRadioButton()
        {
            WaitForElementToBeClickable(Flagrecordforhandover_YesRadioButton);
            Click(Flagrecordforhandover_YesRadioButton);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateFlagRecordForHandover_YesRadioButtonChecked()
        {
            WaitForElement(Flagrecordforhandover_YesRadioButton);
            ValidateElementChecked(Flagrecordforhandover_YesRadioButton);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateFlagRecordForHandover_YesRadioButtonNotChecked()
        {
            WaitForElement(Flagrecordforhandover_YesRadioButton);
            ValidateElementNotChecked(Flagrecordforhandover_YesRadioButton);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickFlagRecordForHandover_NoRadioButton()
        {
            WaitForElementToBeClickable(Flagrecordforhandover_NoRadioButton);
            Click(Flagrecordforhandover_NoRadioButton);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateFlagRecordForHandover_NoRadioButtonChecked()
        {
            WaitForElement(Flagrecordforhandover_NoRadioButton);
            ValidateElementChecked(Flagrecordforhandover_NoRadioButton);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateFlagRecordForHandover_NoRadioButtonNotChecked()
        {
            WaitForElement(Flagrecordforhandover_NoRadioButton);
            ValidateElementNotChecked(Flagrecordforhandover_NoRadioButton);

            return this;
        }

        //Verify FlagRecordForHandover options are displayed or not displayed
        public PersonPhysicalObservationsRecordPage ValidateFlagRecordForHandoverOptionsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Flagrecordforhandover_YesRadioButton);
                WaitForElementVisible(Flagrecordforhandover_NoRadioButton);
            }
            else
            {
                WaitForElementNotVisible(Flagrecordforhandover_YesRadioButton, 3);
                WaitForElementNotVisible(Flagrecordforhandover_NoRadioButton, 3);
            }

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateStaffRequiredSelectedOptionText(string OptionId, string ExpectedText)
        {
            WaitForElement(StaffRequired_SelectedOption(OptionId));
            ValidateElementText(StaffRequired_SelectedOption(OptionId), ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateStaffRequiredSelectedOptionText(Guid OptionId, string ExpectedText)
        {
            return ValidateStaffRequiredSelectedOptionText(OptionId.ToString(), ExpectedText);
        }

        //select TalkingInSentences value
        public PersonPhysicalObservationsRecordPage SelectTalkingInSentencesValue(string TextToSelect)
        {
            WaitForElement(TalkingInSentencesPicklist);
            ScrollToElement(TalkingInSentencesPicklist);
            SelectPicklistElementByText(TalkingInSentencesPicklist, TextToSelect);

            return this;
        }

        //verify TalkingInSentencesPicklist selected text
        public PersonPhysicalObservationsRecordPage ValidateTalkingInSentencesSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(TalkingInSentencesPicklist, ExpectedText);

            return this;
        }

        //verify TalkingInSentencesPicklist is displayed or not displayed
        public PersonPhysicalObservationsRecordPage ValidateTalkingInSentencesFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(TalkingInSentencesPicklist);
            else
                WaitForElementNotVisible(TalkingInSentencesPicklist, 3);

            return this;
        }

        //select BreathingIsNoisy value
        public PersonPhysicalObservationsRecordPage SelectBreathingIsNoisyValue(string TextToSelect)
        {
            WaitForElement(BreathingIsNoisyPicklist);
            ScrollToElement(BreathingIsNoisyPicklist);
            SelectPicklistElementByText(BreathingIsNoisyPicklist, TextToSelect);

            return this;
        }

        //verify BreathingIsNoisyPicklist selected text
        public PersonPhysicalObservationsRecordPage ValidateBreathingIsNoisySelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(BreathingIsNoisyPicklist, ExpectedText);

            return this;
        }

        //verify BreathingIsNoisyPicklist is displayed or not displayed
        public PersonPhysicalObservationsRecordPage ValidateBreathingIsNoisyFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(BreathingIsNoisyPicklist);
            else
                WaitForElementNotVisible(BreathingIsNoisyPicklist, 3);

            return this;
        }

        //select BreathingBetween11to25 value
        public PersonPhysicalObservationsRecordPage SelectBreathingBetween11to25Value(string TextToSelect)
        {
            WaitForElement(BreathingBetween11to25Picklist);
            ScrollToElement(BreathingBetween11to25Picklist);
            SelectPicklistElementByText(BreathingBetween11to25Picklist, TextToSelect);

            return this;
        }

        //verify BreathingBetween11to25Picklist selected text
        public PersonPhysicalObservationsRecordPage ValidateBreathingBetween11to25SelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(BreathingBetween11to25Picklist, ExpectedText);

            return this;
        }

        //verify BreathingBetween11to25Picklist is displayed or not displayed
        public PersonPhysicalObservationsRecordPage ValidateBreathingBetween11to25FieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(BreathingBetween11to25Picklist);
            else
                WaitForElementNotVisible(BreathingBetween11to25Picklist, 3);

            return this;
        }

        //select LabouredBreathing value
        public PersonPhysicalObservationsRecordPage SelectLabouredBreathingValue(string TextToSelect)
        {
            WaitForElement(LabouredBreathingPicklist);
            ScrollToElement(LabouredBreathingPicklist);
            SelectPicklistElementByText(LabouredBreathingPicklist, TextToSelect);

            return this;
        }

        //verify LabouredBreathingPicklist selected text
        public PersonPhysicalObservationsRecordPage ValidateLabouredBreathingSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(LabouredBreathingPicklist, ExpectedText);

            return this;
        }

        //verify LabouredBreathingPicklist is displayed or not displayed
        public PersonPhysicalObservationsRecordPage ValidateLabouredBreathingFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(LabouredBreathingPicklist);
            else
                WaitForElementNotVisible(LabouredBreathingPicklist, 3);

            return this;
        }

        //select AbleToStandUnaided value
        public PersonPhysicalObservationsRecordPage SelectAbleToStandUnaidedValue(string TextToSelect)
        {
            WaitForElement(AbleToStandUnaidedPicklist);
            ScrollToElement(AbleToStandUnaidedPicklist);
            SelectPicklistElementByText(AbleToStandUnaidedPicklist, TextToSelect);

            return this;
        }

        //verify AbleToStandUnaidedPicklist selected text
        public PersonPhysicalObservationsRecordPage ValidateAbleToStandUnaidedSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(AbleToStandUnaidedPicklist, ExpectedText);

            return this;
        }

        //verify AbleToStandUnaidedPicklist is displayed or not displayed
        public PersonPhysicalObservationsRecordPage ValidateAbleToStandUnaidedFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(AbleToStandUnaidedPicklist);
            else
                WaitForElementNotVisible(AbleToStandUnaidedPicklist, 3);

            return this;
        }

        //select UnableToStandUnaided value
        public PersonPhysicalObservationsRecordPage SelectUnableToStandUnaidedValue(string TextToSelect)
        {
            WaitForElement(UnableToStandUnaidedPicklist);
            ScrollToElement(UnableToStandUnaidedPicklist);
            SelectPicklistElementByText(UnableToStandUnaidedPicklist, TextToSelect);

            return this;
        }

        //verify UnableToStandUnaidedPicklist selected text
        public PersonPhysicalObservationsRecordPage ValidateUnableToStandUnaidedSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(UnableToStandUnaidedPicklist, ExpectedText);

            return this;
        }

        //verify UnableToStandUnaidedPicklist is displayed or not displayed
        public PersonPhysicalObservationsRecordPage ValidateUnableToStandUnaidedFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(UnableToStandUnaidedPicklist);
            else
                WaitForElementNotVisible(UnableToStandUnaidedPicklist, 3);

            return this;
        }

        //select AlertAndResponsive value
        public PersonPhysicalObservationsRecordPage SelectAlertAndResponsiveValue(string TextToSelect)
        {
            WaitForElement(AlertAndResponsivePicklist);
            ScrollToElement(AlertAndResponsivePicklist);
            SelectPicklistElementByText(AlertAndResponsivePicklist, TextToSelect);

            return this;
        }

        //verify AlertAndResponsivePicklist selected text
        public PersonPhysicalObservationsRecordPage ValidateAlertAndResponsiveSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(AlertAndResponsivePicklist, ExpectedText);

            return this;
        }

        //verify AlertAndResponsivePicklist is displayed or not displayed
        public PersonPhysicalObservationsRecordPage ValidateAlertAndResponsiveFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(AlertAndResponsivePicklist);
            else
                WaitForElementNotVisible(AlertAndResponsivePicklist, 3);

            return this;
        }

        //select SleepyAndDrowsyPicklist value
        public PersonPhysicalObservationsRecordPage SelectSleepyAndDrowsyValue(string TextToSelect)
        {
            WaitForElement(SleepyAndDrowsyPicklist);
            ScrollToElement(SleepyAndDrowsyPicklist);
            SelectPicklistElementByText(SleepyAndDrowsyPicklist, TextToSelect);

            return this;
        }

        //verify SleepyAndDrowsyPicklist selected text
        public PersonPhysicalObservationsRecordPage ValidateSleepyAndDrowsySelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(SleepyAndDrowsyPicklist, ExpectedText);

            return this;
        }

        //verify SleepyAndDrowsyPicklist is displayed or not displayed
        public PersonPhysicalObservationsRecordPage ValidateSleepyAndDrowsyFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(SleepyAndDrowsyPicklist);
            else
                WaitForElementNotVisible(SleepyAndDrowsyPicklist, 3);

            return this;
        }

        //select DoesNotHavePhysicalHealthProblemPicklist value
        public PersonPhysicalObservationsRecordPage SelectDoesNotHavePhysicalHealthProblemValue(string TextToSelect)
        {
            WaitForElement(DoesNotHavePhysicalHealthProblemPicklist);
            ScrollToElement(DoesNotHavePhysicalHealthProblemPicklist);
            SelectPicklistElementByText(DoesNotHavePhysicalHealthProblemPicklist, TextToSelect);

            return this;
        }

        //verify DoesNotHavePhysicalHealthProblemPicklist selected text
        public PersonPhysicalObservationsRecordPage ValidateDoesNotHavePhysicalHealthProblemSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(DoesNotHavePhysicalHealthProblemPicklist, ExpectedText);

            return this;
        }

        //verify DoesNotHavePhysicalHealthProblemPicklist is displayed or not displayed
        public PersonPhysicalObservationsRecordPage ValidateDoesNotHavePhysicalHealthProblemFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(DoesNotHavePhysicalHealthProblemPicklist);
            else
                WaitForElementNotVisible(DoesNotHavePhysicalHealthProblemPicklist, 3);

            return this;
        }

        //select HasPhysicalHealthProblemPicklist value
        public PersonPhysicalObservationsRecordPage SelectHasPhysicalHealthProblemValue(string TextToSelect)
        {
            WaitForElement(HasPhysicalHealthProblemPicklist);
            ScrollToElement(HasPhysicalHealthProblemPicklist);
            SelectPicklistElementByText(HasPhysicalHealthProblemPicklist, TextToSelect);

            return this;
        }

        //verify HasPhysicalHealthProblemPicklist selected text
        public PersonPhysicalObservationsRecordPage ValidateHasPhysicalHealthProblemSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(HasPhysicalHealthProblemPicklist, ExpectedText);

            return this;
        }

        //verify HasPhysicalHealthProblemPicklist is displayed or not displayed
        public PersonPhysicalObservationsRecordPage ValidateHasPhysicalHealthProblemFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(HasPhysicalHealthProblemPicklist);
            else
                WaitForElementNotVisible(HasPhysicalHealthProblemPicklist, 3);

            return this;
        }

        #region NEWS Type

        public PersonPhysicalObservationsRecordPage ValidateTemperatureText(string ExpectedText)
        {
            ValidateElementValue(Temperature, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnTemperature(string TextToInsert)
        {
            WaitForElementToBeClickable(Temperature);
            SendKeys(Temperature, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsRecordPage SelectAreaTemperatureTaken(string TextToSelect)
        {
            WaitForElementToBeClickable(TemperatureRouteid);
            SelectPicklistElementByText(TemperatureRouteid, TextToSelect);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateAreaTemperatureTakenSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(TemperatureRouteid, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateTemperatureActionsTakenRequiredText(string ExpectedText)
        {
            ValidateElementText(Vitalsignsactionstakenrequired, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnTemperatureActionsTakenRequired(string TextToInsert)
        {
            WaitForElementToBeClickable(Vitalsignsactionstakenrequired);
            SendKeys(Vitalsignsactionstakenrequired, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateTemperatureEarlyWarningScoreText(string ExpectedText)
        {
            ValidateElementValue(Temperatureearlywarningscore, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnTemperatureearlywarningscore(string TextToInsert)
        {
            WaitForElementToBeClickable(Temperatureearlywarningscore);
            SendKeys(Temperatureearlywarningscore, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateBpSystolicText(string ExpectedText)
        {
            ValidateElementValue(Bpsystolic, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnBpsystolic(string TextToInsert)
        {
            WaitForElementToBeClickable(Bpsystolic);
            SendKeys(Bpsystolic, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateBpDiastolicText(string ExpectedText)
        {
            ValidateElementValue(Bpdiastolic, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnBpdiastolic(string TextToInsert)
        {
            WaitForElementToBeClickable(Bpdiastolic);
            SendKeys(Bpdiastolic, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickRequireSecondaryBpReading_YesOption()
        {
            WaitForElementToBeClickable(Requiresecondarybpreading_YesOption);
            Click(Requiresecondarybpreading_YesOption);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateRequireSecondaryBpReading_YesOptionChecked()
        {
            WaitForElement(Requiresecondarybpreading_YesOption);
            ValidateElementChecked(Requiresecondarybpreading_YesOption);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateRequireSecondaryBpReading_YesOptionNotChecked()
        {
            WaitForElement(Requiresecondarybpreading_YesOption);
            ValidateElementNotChecked(Requiresecondarybpreading_YesOption);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickRequireSecondaryBpReading_NoOption()
        {
            WaitForElementToBeClickable(Requiresecondarybpreading_NoOption);
            Click(Requiresecondarybpreading_NoOption);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateRequireSecondaryBpReading_NoOptionChecked()
        {
            WaitForElement(Requiresecondarybpreading_NoOption);
            ValidateElementChecked(Requiresecondarybpreading_NoOption);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateRequireSecondaryBpReading_NoOptionNotChecked()
        {
            WaitForElement(Requiresecondarybpreading_NoOption);
            ValidateElementNotChecked(Requiresecondarybpreading_NoOption);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateSecondaryBpSystolicText(string ExpectedText)
        {
            ValidateElementValue(Secondarybpsystolic, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnSecondaryBpSystolic(string TextToInsert)
        {
            WaitForElementToBeClickable(Secondarybpsystolic);
            SendKeys(Secondarybpsystolic, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateSecondaryBpDiastolicText(string ExpectedText)
        {
            ValidateElementValue(Secondarybpdiastolic, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnSecondaryBpDiastolic(string TextToInsert)
        {
            WaitForElementToBeClickable(Secondarybpdiastolic);
            SendKeys(Secondarybpdiastolic, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateBloodPressureReadingActionsTakenRequiredText(string ExpectedText)
        {
            ValidateElementText(Bloodpressurereadingactionstakenrequired, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnBloodPressureReadingActionsTakenRequired(string TextToInsert)
        {
            WaitForElementToBeClickable(Bloodpressurereadingactionstakenrequired);
            SendKeys(Bloodpressurereadingactionstakenrequired, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsRecordPage SelectPositionWhenReadingTaken(string TextToSelect)
        {
            WaitForElementToBeClickable(Bpreadingtypeid);
            SelectPicklistElementByText(Bpreadingtypeid, TextToSelect);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidatePositionWhenReadingTakenSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Bpreadingtypeid, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateBpEarlyWarningScoreText(string ExpectedText)
        {
            ValidateElementValue(Bpearlywarningscore, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnBpEarlyWarningScore(string TextToInsert)
        {
            WaitForElementToBeClickable(Bpearlywarningscore);
            SendKeys(Bpearlywarningscore, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsRecordPage SelectSecondaryBpPositionWhenReadingTaken(string TextToSelect)
        {
            WaitForElementToBeClickable(Secondarybpreadingtypeid);
            SelectPicklistElementByText(Secondarybpreadingtypeid, TextToSelect);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateSecondaryBpPositionWhenReadingTakenSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Secondarybpreadingtypeid, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateSecondaryBpEarlyWarningScoreText(string ExpectedText)
        {
            ValidateElementValue(Secondarybpearlywarningscore, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnSecondaryBpEarlyWarningScore(string TextToInsert)
        {
            WaitForElementToBeClickable(Secondarybpearlywarningscore);
            SendKeys(Secondarybpearlywarningscore, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidatePulseText(string ExpectedText)
        {
            ValidateElementValue(Pulse, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnPulse(string TextToInsert)
        {
            WaitForElementToBeClickable(Pulse);
            SendKeys(Pulse, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsRecordPage SelectIsPulseRegularOrIrregular(string TextToSelect)
        {
            WaitForElementToBeClickable(Ispulseregularorirregularid);
            SelectPicklistElementByText(Ispulseregularorirregularid, TextToSelect);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateIsPulseRegularOrIrregularSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Ispulseregularorirregularid, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage SelectPersonPhysicalObservationWhenPulseTaken(string TextToSelect)
        {
            WaitForElementToBeClickable(Personphysicalobservationwhenpulsetakenid);
            SelectPicklistElementByText(Personphysicalobservationwhenpulsetakenid, TextToSelect);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateWhenPulseTakenSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Personphysicalobservationwhenpulsetakenid, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidatePulseEarlyWarningScoreText(string ExpectedText)
        {
            ValidateElementValue(Pulseearlywarningscore, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnPulseEarlyWarningScore(string TextToInsert)
        {
            WaitForElementToBeClickable(Pulseearlywarningscore);
            SendKeys(Pulseearlywarningscore, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidatePulseActionsTakenRequiredText(string ExpectedText)
        {
            ValidateElementText(Pulseactionstakenrequired, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnPulseActionsTakenRequired(string TextToInsert)
        {
            WaitForElementToBeClickable(Pulseactionstakenrequired);
            SendKeys(Pulseactionstakenrequired, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateRespirationText(string ExpectedText)
        {
            ValidateElementValue(Respiration, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnRespiration(string TextToInsert)
        {
            WaitForElementToBeClickable(Respiration);
            SendKeys(Respiration, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateRespirationActionsTakenRequiredText(string ExpectedText)
        {
            ValidateElementValue(Respirationactionstakenrequired, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnRespirationActionsTakenRequired(string TextToInsert)
        {
            WaitForElementToBeClickable(Respirationactionstakenrequired);
            SendKeys(Respirationactionstakenrequired, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateRespiratoryDistressEarlyWarningScoreText(string ExpectedText)
        {
            ValidateElementValue(Respiratorydistressearlywarningscore, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnRespiratoryDistressEarlyWarningScore(string TextToInsert)
        {
            WaitForElementToBeClickable(Respiratorydistressearlywarningscore);
            SendKeys(Respiratorydistressearlywarningscore, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateOxygenSaturationText(string ExpectedText)
        {
            ValidateElementValue(Oxygensaturation, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnOxygenSaturation(string TextToInsert)
        {
            WaitForElementToBeClickable(Oxygensaturation);
            SendKeys(Oxygensaturation, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidatePeakFlowText(string ExpectedText)
        {
            ValidateElementValue(Peakflow, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnPeakFlow(string TextToInsert)
        {
            WaitForElementToBeClickable(Peakflow);
            SendKeys(Peakflow, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateOxygenSaturationActionsTakenRequiredText(string ExpectedText)
        {
            ValidateElementValue(Oxygensaturationactionstakenrequired, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnOxygenSaturationActionsTakenRequired(string TextToInsert)
        {
            WaitForElementToBeClickable(Oxygensaturationactionstakenrequired);
            SendKeys(Oxygensaturationactionstakenrequired, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateOxygenSaturationEarlyWarningScoreText(string ExpectedText)
        {
            ValidateElementValue(Oxygensaturationearlywarningscore, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnOxygenSaturationEarlyWarningScore(string TextToInsert)
        {
            WaitForElementToBeClickable(Oxygensaturationearlywarningscore);
            SendKeys(Oxygensaturationearlywarningscore, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateBloodSugarlevelText(string ExpectedText)
        {
            ValidateElementValue(Bloodsugarlevel, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnBloodSugarLevel(string TextToInsert)
        {
            WaitForElementToBeClickable(Bloodsugarlevel);
            SendKeys(Bloodsugarlevel, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateBloodGlucoseActionsTakenRequiredText(string ExpectedText)
        {
            ValidateElementValue(Bloodglucoseactionstakenrequired, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnBloodGlucoseActionsTakenRequired(string TextToInsert)
        {
            WaitForElementToBeClickable(Bloodglucoseactionstakenrequired);
            SendKeys(Bloodglucoseactionstakenrequired, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsRecordPage SelectBloodSugarWhenWasReadingTaken(string TextToSelect)
        {
            WaitForElementToBeClickable(Bloodsugarwhenwasreadingtakenid);
            SelectPicklistElementByText(Bloodsugarwhenwasreadingtakenid, TextToSelect);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateBloodSugarWhenWasReadingTakenSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Bloodsugarwhenwasreadingtakenid, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage SelectRightPupilSize(string TextToSelect)
        {
            WaitForElementToBeClickable(Rightpupilsizereactionid);
            SelectPicklistElementByText(Rightpupilsizereactionid, TextToSelect);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateRightPupilSizeSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Rightpupilsizereactionid, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage SelectRightPupilReactionType(string TextToSelect)
        {
            WaitForElementToBeClickable(Rightpupilreactiontypeid);
            SelectPicklistElementByText(Rightpupilreactiontypeid, TextToSelect);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateRightPupilReactionTypeSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Rightpupilreactiontypeid, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateNeurologicalObservationsActionsTakenRequiredText(string ExpectedText)
        {
            ValidateElementText(Neurologicalobservationsactionstakenrequired, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnNeurologicalObservationsActionsTakenRequired(string TextToInsert)
        {
            WaitForElementToBeClickable(Neurologicalobservationsactionstakenrequired);
            SendKeys(Neurologicalobservationsactionstakenrequired, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsRecordPage SelectLeftPupilSize(string TextToSelect)
        {
            WaitForElementToBeClickable(Leftpupilsizereactionid);
            SelectPicklistElementByText(Leftpupilsizereactionid, TextToSelect);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateLeftPupilSizeSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Leftpupilsizereactionid, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage SelectLeftPupilReactionType(string TextToSelect)
        {
            WaitForElementToBeClickable(Leftpupilreactiontypeid);
            SelectPicklistElementByText(Leftpupilreactiontypeid, TextToSelect);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateLeftPupilReactionTypeSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Leftpupilreactiontypeid, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateTotalscoreText(string ExpectedText)
        {
            ValidateElementValue(Totalscore, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnTotalscore(string TextToInsert)
        {
            WaitForElementToBeClickable(Totalscore);
            SendKeys(Totalscore, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateTotalScoresActionsTakenRequiredText(string ExpectedText)
        {
            ValidateElementText(Totalscoresactionstakenrequired, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnTotalScoresActionsTakenRequired(string TextToInsert)
        {
            WaitForElementToBeClickable(Totalscoresactionstakenrequired);
            SendKeys(Totalscoresactionstakenrequired, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickReviewRequiredBySeniorColleague_YesOption()
        {
            WaitForElementToBeClickable(ReviewRequiredbyseniorcolleague_YesOption);
            Click(ReviewRequiredbyseniorcolleague_YesOption);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateReviewRequiredBySeniorColleague_YesOptionChecked()
        {
            WaitForElement(ReviewRequiredbyseniorcolleague_YesOption);
            ValidateElementChecked(ReviewRequiredbyseniorcolleague_YesOption);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateReviewRequiredBySeniorColleague_YesOptionNotChecked()
        {
            WaitForElement(ReviewRequiredbyseniorcolleague_YesOption);
            ValidateElementNotChecked(ReviewRequiredbyseniorcolleague_YesOption);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ClickReviewRequiredBySeniorColleague_NoOption()
        {
            WaitForElementToBeClickable(ReviewRequiredbyseniorcolleague_NoOption);
            Click(ReviewRequiredbyseniorcolleague_NoOption);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateReviewRequiredBySeniorColleague_NoOptionChecked()
        {
            WaitForElement(ReviewRequiredbyseniorcolleague_NoOption);
            ValidateElementChecked(ReviewRequiredbyseniorcolleague_NoOption);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateReviewRequiredBySeniorColleague_NoOptionNotChecked()
        {
            WaitForElement(ReviewRequiredbyseniorcolleague_NoOption);
            ValidateElementNotChecked(ReviewRequiredbyseniorcolleague_NoOption);

            return this;
        }

        public PersonPhysicalObservationsRecordPage ValidateReviewDetailsText(string ExpectedText)
        {
            ValidateElementText(ReviewDetails, ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsRecordPage InsertTextOnReviewDetails(string TextToInsert)
        {
            WaitForElementToBeClickable(ReviewDetails);
            SendKeys(ReviewDetails, TextToInsert);

            return this;
        }

        #endregion
    }
}

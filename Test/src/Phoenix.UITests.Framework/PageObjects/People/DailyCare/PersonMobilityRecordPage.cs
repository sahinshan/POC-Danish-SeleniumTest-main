using OpenQA.Selenium;
using System;


namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonMobilityRecordPage : CommonMethods
    {
        public PersonMobilityRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }
        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        readonly By CWPanelIFrame = By.Id("CWUrlPanel_IFrame");
        readonly By personMobilityIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cppersonmobility&')]");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Person Mobility: ']");
        readonly By SaveNCloseBtn = By.Id("TI_SaveAndCloseButton");
        readonly By SaveBtn = By.Id("TI_SaveButton");
        readonly By BackButton = By.Id("BackButton");

        readonly By TopPageNotification = By.XPath("//*[@id='CWNotificationMessage_DataForm']");

        readonly By MenuButton = By.XPath("//*[@id ='CWNavGroup_Menu']/button");

        readonly By RelatedItemsDetailsElementExpanded = By.XPath("//span[text()='Related Items']/parent::div/parent::summary/parent::details[@open]");
        readonly By RelatedItemsLeftSubMenu = By.XPath("//details/summary/div/span[text()='Related Items']");

        readonly By AttachmentsMenuItem = By.Id("CWNavItem_CPMobilityAttachment");

        #region General

        readonly By PersonidLink = By.XPath("//*[@id='CWField_personid_Link']");
        readonly By PersonidLookupButton = By.XPath("//*[@id='CWLookupBtn_personid']");

        readonly By consentGiven = By.Id("CWField_careconsentgivenid");
        readonly By Careconsentgivenid_ErrorLabel = By.XPath("//*[@id='CWControlHolder_careconsentgivenid']/label/span");

        readonly By occurred = By.Id("CWField_occurred");
        readonly By occurredTime = By.Id("CWField_occurred_Time");

        readonly By nonconsentDetail = By.Id("CWField_carenonconsentid");
        readonly By Carenonconsentid_ErrorLabel = By.XPath("//*[@id='CWControlHolder_carenonconsentid']/label/span");

        readonly By Reasonforabsence = By.XPath("//*[@id='CWField_reasonforabsence']");
        readonly By Reasonforabsence_ErrorLabel = By.XPath("//*[@id='CWControlHolder_reasonforabsence']/label/span");

        readonly By Reasonconsentdeclined = By.XPath("//*[@id='CWField_reasonconsentdeclined']");
        readonly By Reasonconsentdeclined_ErrorLabel = By.XPath("//*[@id='CWControlHolder_reasonconsentdeclined']/label/span");
        readonly By Encouragementgiven = By.XPath("//*[@id='CWField_encouragementgiven']");
        readonly By Encouragementgiven_ErrorLabel = By.XPath("//*[@id='CWControlHolder_encouragementgiven']/label/span");
        readonly By Careprovidedwithoutconsent_1 = By.XPath("//*[@id='CWField_careprovidedwithoutconsent_1']");
        readonly By Careprovidedwithoutconsent_0 = By.XPath("//*[@id='CWField_careprovidedwithoutconsent_0']");

        readonly By PreferencesTextareField = By.XPath("//*[@id = 'CWField_preferences']");

        readonly By deferredToDateFieldLabel = By.XPath("//*[@id = 'CWLabelHolder_deferredtodate']/label");
        readonly By deferredToDate = By.Id("CWField_deferredtodate");
        readonly By deferredToDate_DatePicker = By.Id("CWField_deferredtodate_DatePicker");
        readonly By deferredToDate_ErrorLabel = By.XPath("//*[@id='CWControlHolder_deferredtodate']/label/span");

        readonly By deferredToTimeOrShift = By.Id("CWField_timeorshiftid");
        readonly By deferredToTimeOrShift_ErrorLabel = By.XPath("//*[@id='CWControlHolder_timeorshiftid']/label/span");

        readonly By deferredToTime = By.Id("CWField_deferredtotime");
        readonly By deferredToTime_ErrorLabel = By.XPath("//*[@id='CWControlHolder_deferredtotime']/label/span");

        readonly By deferredToShift_LookupButton = By.Id("CWLookupBtn_deferredtoshiftid");
        readonly By deferredToShift_LinkField = By.Id("CWField_deferredtoshiftid_Link");
        readonly By deferredToShift_ClearButton = By.Id("CWClearLookup_deferredtoshiftid");
        readonly By deferredToShift_ErrorLabel = By.XPath("//*[@id='CWControlHolder_deferredtoshiftid']/label/span");

        #endregion

        #region Details

        readonly By MobilisedFromLink = By.XPath("//*[@id='CWField_mobilisedfromlocationid_Link']");
        readonly By MobilisedFromLookUpBtn = By.Id("CWLookupBtn_mobilisedfromlocationid");
        readonly By MobilisedFrom_ErrorLabel = By.XPath("//*[@id='CWControlHolder_mobilisedfromlocationid']/label/span");

        readonly By mobilisedfromothertextField = By.Id("CWField_mobilisedfromothertext");
        readonly By mobilisedfromothertext_ErrorLabel = By.XPath("//*[@id='CWControlHolder_mobilisedfromothertext']/label/span");

        readonly By ApproximateDiatanceFld = By.Id("CWField_approximatedistance");
        readonly By ApproximateDiatance_ErrorLabel = By.XPath("//*[@id='CWControlHolder_approximatedistance']/label/span");

        readonly By ApproximateDiatanceUnitLookupBtn = By.Id("CWControlHolder_approximatedistanceunitid");
        readonly By ApproximateDiatanceUnitClearButton = By.Id("CWClearLookup_approximatedistanceunitid");
        readonly By ApproximateDiatanceUnitLinkField = By.Id("CWField_approximatedistanceunitid_Link");
        readonly By ApproximateDiatanceUnit_ErrorLabel = By.XPath("//*[@id='CWControlHolder_approximatedistanceunitid']/label/span");

        readonly By MobilisedToLink = By.XPath("//*[@id='CWField_mobilisedtolocationid_Link']");
        readonly By MobilisedToLookUpBtn = By.Id("CWLookupBtn_mobilisedtolocationid");
        readonly By MobilisedTo_ErrorLabel = By.XPath("//*[@id='CWControlHolder_mobilisedtolocationid']/label/span");

        readonly By mobilisedtoothertextField = By.Id("CWField_mobilisedtoothertext");
        readonly By mobilisedtoothertext_ErrorLabel = By.XPath("//*[@id='CWControlHolder_mobilisedtoothertext']/label/span");

        #endregion

        #region Additional Information

        By Equipment_SelectedElementLink(string ElementId) => By.XPath("//*[@id='MS_careequipmentid_" + ElementId + "']/a[@id='" + ElementId + "_Link']");
        By Equipment_SelectedElementRemoveButton(string ElementId) => By.XPath("//*[@id='MS_careequipmentid_" + ElementId + "']/a[text()='Remove']");
        readonly By EquipmentLookUpBtn = By.Id("CWLookupBtn_careequipmentid");
        readonly By equipmentifother = By.Id("CWField_equipmentifother");
        readonly By AssistanceNeededLink = By.XPath("//*[@id='CWField_careassistanceneededid_Link']");
        readonly By AssistanceNeededLookUpBtn = By.Id("CWLookupBtn_careassistanceneededid");
        readonly By AssistanceAmountPicklist = By.Id("CWField_careassistancelevelid");
        readonly By WellBeingLink = By.XPath("//*[@id='CWField_carewellbeingid_Link']");
        readonly By WellBeingLookUpBtn = By.Id("CWLookupBtn_carewellbeingid");
        readonly By actiontaken = By.Id("CWField_actiontaken");
        readonly By TotalTimeSpentFld = By.Id("CWField_timespentwithclient");
        readonly By AdditionalNotesTextArea = By.Id("CWField_additionalnotes");
        readonly By carenoteTextArea = By.Id("CWField_carenote");

        #endregion

        #region Care needs and Handover

        readonly By LinkedActivitiesOfAdlLookupButton = By.Id("CWLookupBtn_linkedadlcategoriesid");
        readonly By IncludeInNextHandover_YesOption = By.Id("CWField_isincludeinnexthandover_1");
        readonly By IncludeInNextHandover_NoOption = By.Id("CWField_isincludeinnexthandover_0");
        readonly By FlagRecordForHandover_YesOption = By.Id("CWField_flagrecordforhandover_1");
        readonly By FlagRecordForHandover_NoOption = By.Id("CWField_flagrecordforhandover_0");

        #endregion

        #region Resident Voice

        readonly By ResidentVoiceSection = By.Id("CWSection_ResidentVoice");
        readonly By MobilityAttachmentIframe = By.Id("CWIFrame_CPMobilityAttachment");
        readonly By SectionHeader = By.XPath("//*[@id = 'CWToolbar']//h1[text() = 'Attachments (For Mobility)']");
        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");

        #endregion

        public PersonMobilityRecordPage WaitForPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(personMobilityIFrame);
            SwitchToIframe(personMobilityIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(pageHeader);

            return this;
        }

        public PersonMobilityRecordPage ValidateTopPageNotificationText(string ExpectedText)
        {
            ValidateElementText(TopPageNotification, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage ValidateTopPageNotificationVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(TopPageNotification);
            else
                WaitForElementNotVisible(TopPageNotification, 3);

            return this;
        }

        #region General

        public PersonMobilityRecordPage ClickPersonLink()
        {
            WaitForElementToBeClickable(PersonidLink);
            Click(PersonidLink);

            return this;
        }

        public PersonMobilityRecordPage ValidatePersonLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(PersonidLink);
            ValidateElementText(PersonidLink, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage ClickPersonLookupButton()
        {
            WaitForElementToBeClickable(PersonidLookupButton);
            Click(PersonidLookupButton);

            return this;
        }

        public PersonMobilityRecordPage SelectConsentGiven(string TextToSelect)
        {
            WaitForElementVisible(consentGiven);
            SelectPicklistElementByText(consentGiven, TextToSelect);


            return this;
        }

        public PersonMobilityRecordPage ValidateSelectedConsentGiven(string ExpectedText)
        {
            ValidatePicklistSelectedText(consentGiven, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage ValidateConsentGivenErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Careconsentgivenid_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage ValidateConsentGivenErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Careconsentgivenid_ErrorLabel);
            else
                WaitForElementNotVisible(Careconsentgivenid_ErrorLabel, 3);

            return this;
        }

        public PersonMobilityRecordPage InsertTextOnDateAndTimeOccurred(string TextToInsert)
        {
            WaitForElementVisible(occurred);
            SendKeys(occurred, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public PersonMobilityRecordPage InsertTextOnDateAndTimeOccurred_Time(string TextToInsert)
        {
            WaitForElementVisible(occurredTime);
            ClearText(occurredTime);
            System.Threading.Thread.Sleep(2000);
            SendKeys(occurredTime, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public PersonMobilityRecordPage ValidateDateAndTimeOccurredText(string ExpectedText)
        {
            ValidateElementValue(occurred, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage ValidateDateAndTimeOccurred_TimeText(string ExpectedText)
        {
            ValidateElementValue(occurredTime, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage SelectNonConsentDetail(string TextToSelect)
        {
            WaitForElementVisible(nonconsentDetail);
            SelectPicklistElementByText(nonconsentDetail, TextToSelect);

            return this;
        }

        public PersonMobilityRecordPage ValidateSelectedNonConsentDetail(string ExpectedText)
        {
            ValidatePicklistSelectedText(nonconsentDetail, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage ValidateNonConsentDetailErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Carenonconsentid_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage ValidateNonConsentDetailErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Carenonconsentid_ErrorLabel);
            else
                WaitForElementNotVisible(Carenonconsentid_ErrorLabel, 3);

            return this;
        }

        public PersonMobilityRecordPage ValidateReasonForAbsenceText(string ExpectedText)
        {
            ValidateElementText(Reasonforabsence, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage InsertTextOnReasonForAbsence(string TextToInsert)
        {
            WaitForElementToBeClickable(Reasonforabsence);
            SendKeys(Reasonforabsence, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonMobilityRecordPage ValidateReasonForAbsenceErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Reasonforabsence_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage ValidateReasonForAbsenceErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Reasonforabsence_ErrorLabel);
            else
                WaitForElementNotVisible(Reasonforabsence_ErrorLabel, 3);

            return this;
        }

        public PersonMobilityRecordPage ValidateReasonConsentDeclinedText(string ExpectedText)
        {
            ValidateElementText(Reasonconsentdeclined, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage InsertTextOnReasonConsentDeclined(string TextToInsert)
        {
            WaitForElementToBeClickable(Reasonconsentdeclined);
            SendKeys(Reasonconsentdeclined, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonMobilityRecordPage ValidateReasonConsentDeclinedErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Reasonconsentdeclined_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage ValidateReasonConsentDeclinedErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Reasonconsentdeclined_ErrorLabel);
            else
                WaitForElementNotVisible(Reasonconsentdeclined_ErrorLabel, 3);

            return this;
        }

        public PersonMobilityRecordPage ValidateEncouragementGivenText(string ExpectedText)
        {
            ValidateElementText(Encouragementgiven, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage InsertTextOnEncouragementGiven(string TextToInsert)
        {
            WaitForElementToBeClickable(Encouragementgiven);
            SendKeys(Encouragementgiven, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonMobilityRecordPage ValidateEncouragementGivenErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Encouragementgiven_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage ValidateEncouragementGivenErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Encouragementgiven_ErrorLabel);
            else
                WaitForElementNotVisible(Encouragementgiven_ErrorLabel, 3);

            return this;
        }

        public PersonMobilityRecordPage ClickCareProvidedWithoutConsent_YesRadioButton()
        {
            WaitForElementToBeClickable(Careprovidedwithoutconsent_1);
            Click(Careprovidedwithoutconsent_1);

            return this;
        }

        public PersonMobilityRecordPage ValidateCareProvidedWithoutConsent_YesRadioButtonChecked()
        {
            WaitForElement(Careprovidedwithoutconsent_1);
            ValidateElementChecked(Careprovidedwithoutconsent_1);

            return this;
        }

        public PersonMobilityRecordPage ValidateCareProvidedWithoutConsent_YesRadioButtonNotChecked()
        {
            WaitForElement(Careprovidedwithoutconsent_1);
            ValidateElementNotChecked(Careprovidedwithoutconsent_1);

            return this;
        }

        public PersonMobilityRecordPage ClickCareProvidedWithoutConsent_NoRadioButton()
        {
            WaitForElementToBeClickable(Careprovidedwithoutconsent_0);
            Click(Careprovidedwithoutconsent_0);

            return this;
        }

        public PersonMobilityRecordPage ValidateCareProvidedWithoutConsent_NoRadioButtonChecked()
        {
            WaitForElement(Careprovidedwithoutconsent_0);
            ValidateElementChecked(Careprovidedwithoutconsent_0);

            return this;
        }

        public PersonMobilityRecordPage ValidateCareProvidedWithoutConsent_NoRadioButtonNotChecked()
        {
            WaitForElement(Careprovidedwithoutconsent_0);
            ValidateElementNotChecked(Careprovidedwithoutconsent_0);

            return this;
        }

        public PersonMobilityRecordPage SetDeferredToDate(string TextToInsert)
        {
            WaitForElementVisible(deferredToDate);
            SendKeys(deferredToDate, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public PersonMobilityRecordPage ValidateDeferredToDate(string ExpectedText)
        {
            ValidateElementValue(deferredToDate, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage ValidateDeferredToDateErrorLabel(string ExpectedText)
        {
            WaitForElementVisible(deferredToDate_ErrorLabel);
            ValidateElementText(deferredToDate_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage ValidateDeferredToDateErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(deferredToDate_ErrorLabel);
            else
                WaitForElementNotVisible(deferredToDate_ErrorLabel, 3);

            return this;
        }

        //Validate deferred to date field is displayed or not displayed
        public PersonMobilityRecordPage ValidateDeferredToDateFieldVisible(bool ExpectVisible)
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

        //Validate deferredToDateFieldLabel label name
        public PersonMobilityRecordPage ValidateDeferredToDateFieldLabel(string ExpectedText)
        {
            WaitForElement(deferredToDateFieldLabel);
            ScrollToElement(deferredToDateFieldLabel);
            ValidateElementByTitle(deferredToDateFieldLabel, ExpectedText);

            return this;
        }

        //click deferred to date datepicker
        public PersonMobilityRecordPage ClickDeferredToDate_DatePicker()
        {
            WaitForElementToBeClickable(deferredToDate_DatePicker);
            Click(deferredToDate_DatePicker);

            return this;
        }

        //verify deferred to date datepicker is displayed or not displayed
        public PersonMobilityRecordPage VerifyDeferredToDate_DatePickerIsDisplayed(bool ExpectedDisplayed)
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

        public PersonMobilityRecordPage SelectDeferredToTimeOrShift(string TextToSelect)
        {
            WaitForElementVisible(deferredToTimeOrShift);
            SelectPicklistElementByText(deferredToTimeOrShift, TextToSelect);

            return this;
        }

        public PersonMobilityRecordPage ValidateSelectedDeferredToTimeOrShift(string ExpectedText)
        {
            ValidatePicklistSelectedText(deferredToTimeOrShift, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage ValidateDeferredToTimeOrShiftErrorLabel(string ExpectedText)
        {
            WaitForElementVisible(deferredToTimeOrShift_ErrorLabel);
            ValidateElementText(deferredToTimeOrShift_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage ValidateDeferredToTimeOrShiftErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(deferredToTimeOrShift_ErrorLabel);
            else
                WaitForElementNotVisible(deferredToTimeOrShift_ErrorLabel, 3);

            return this;
        }

        public PersonMobilityRecordPage SetDeferredToTime(string TextToInsert)
        {
            WaitForElementVisible(deferredToTime);
            SendKeys(deferredToTime, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public PersonMobilityRecordPage ValidateDeferredToTime(string ExpectedText)
        {
            ValidateElementValue(deferredToTime, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage ValidateDeferredToTimeErrorLabel(string ExpectedText)
        {
            WaitForElementVisible(deferredToTime_ErrorLabel);
            ValidateElementText(deferredToTime_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage ValidateDeferredToTimeErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(deferredToTime_ErrorLabel);
            else
                WaitForElementNotVisible(deferredToTime_ErrorLabel, 3);

            return this;
        }

        public PersonMobilityRecordPage ClickDeferredToShiftLookupButton()
        {
            WaitForElementToBeClickable(deferredToShift_LookupButton);
            Click(deferredToShift_LookupButton);

            return this;
        }

        public PersonMobilityRecordPage ValidateDeferredToShiftLinkText(string ExpectedText)
        {
            ValidateElementText(deferredToShift_LinkField, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage ClickDeferredToShiftClearButton()
        {
            Click(deferredToShift_ClearButton);

            return this;
        }

        public PersonMobilityRecordPage ValidateDeferredToShiftErrorLabel(string ExpectedText)
        {
            WaitForElementVisible(deferredToShift_ErrorLabel);
            ValidateElementText(deferredToShift_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage ValidateDeferredToShiftErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(deferredToShift_ErrorLabel);
            else
                WaitForElementNotVisible(deferredToShift_ErrorLabel, 3);

            return this;
        }

        //verify preferences textare field is displayed or not displayed
        public PersonMobilityRecordPage VerifyPreferencesTextAreaFieldIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
            {
                WaitForElementVisible(PreferencesTextareField);
            }
            else
            {
                WaitForElementNotVisible(PreferencesTextareField, 2);
            }

            return this;
        }

        //verify preferences textare field is disabled or not disabled
        public PersonMobilityRecordPage VerifyPreferencesTextAreaFieldIsDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
            {
                ValidateElementAttribute(PreferencesTextareField, "disabled", "true");
            }
            else
            {
                ValidateElementAttribute(PreferencesTextareField, "disabled", "false");
            }

            return this;
        }

        //verify preferences textare field text
        public PersonMobilityRecordPage VerifyPreferencesTextAreaFieldText(string ExpectedText)
        {
            ScrollToElement(PreferencesTextareField);
            WaitForElementVisible(PreferencesTextareField);
            ValidateElementValue(PreferencesTextareField, ExpectedText);

            return this;
        }

        //verify preference textarea field attribute value
        public PersonMobilityRecordPage VerifyPreferencesTextAreaFieldMaxLength(string ExpectedValue)
        {
            ScrollToElement(PreferencesTextareField);
            WaitForElementVisible(PreferencesTextareField);
            ValidateElementAttribute(PreferencesTextareField, "maxlength", ExpectedValue);

            return this;
        }

        #endregion

        #region Details

        public PersonMobilityRecordPage ClickMobilisedFromLink()
        {
            WaitForElementToBeClickable(MobilisedFromLink);
            Click(MobilisedFromLink);

            return this;
        }

        public PersonMobilityRecordPage ValidateMobilisedFromLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(MobilisedFromLink);
            ValidateElementText(MobilisedFromLink, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage ClickMobilisedFromLookupButton()
        {
            WaitForElementToBeClickable(MobilisedFromLookUpBtn);
            Click(MobilisedFromLookUpBtn);

            return this;
        }

        public PersonMobilityRecordPage ValidateMobilisedFromErrorLabel(string ExpectedText)
        {
            WaitForElementVisible(MobilisedFrom_ErrorLabel);
            ValidateElementText(MobilisedFrom_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage InsertMobilisedFromOther(string TextToInsert)
        {
            WaitForElement(mobilisedfromothertextField);
            SendKeys(mobilisedfromothertextField, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonMobilityRecordPage ValidateMobilisedFromOther(string ExpectedText)
        {
            WaitForElementVisible(mobilisedfromothertextField);
            ValidateElementText(mobilisedfromothertextField, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage ValidateMobilisedFromOtherFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(mobilisedfromothertextField);
            }
            else
            {
                WaitForElementNotVisible(mobilisedfromothertextField, 3);
            }

            return this;
        }

        public PersonMobilityRecordPage ValidateMobilisedFromOtherErrorLabel(string ExpectedText)
        {
            WaitForElementVisible(mobilisedfromothertext_ErrorLabel);
            ValidateElementText(mobilisedfromothertext_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage SetApproximateDistance(string TextToInsert)
        {
            WaitForElement(ApproximateDiatanceFld);
            SendKeys(ApproximateDiatanceFld, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public PersonMobilityRecordPage ValidateApproximateDistance(string ExpectedText)
        {
            ValidateElementValue(ApproximateDiatanceFld, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage ValidateApproximateDiatanceErrorLabel(string ExpectedText)
        {
            if (!string.IsNullOrEmpty(ExpectedText))
            {
                WaitForElementVisible(ApproximateDiatance_ErrorLabel);
                ValidateElementText(ApproximateDiatance_ErrorLabel, ExpectedText);
            }
            else
            {
                WaitForElementNotVisible(ApproximateDiatance_ErrorLabel, 3);
            }
            return this;
        }

        public PersonMobilityRecordPage ClickApproximateDistanceUnitLookupButton()
        {
            WaitForElementToBeClickable(ApproximateDiatanceUnitLookupBtn);
            Click(ApproximateDiatanceUnitLookupBtn);

            return this;
        }

        public PersonMobilityRecordPage ClickApproximateDistanceUnitClearButton()
        {
            WaitForElementToBeClickable(ApproximateDiatanceUnitClearButton);
            Click(ApproximateDiatanceUnitClearButton);

            return this;
        }

        public PersonMobilityRecordPage ValidateApproximateDiatanceUnitLinkFieldText(string ExpectedText)
        {
            ValidateElementText(ApproximateDiatanceUnitLinkField, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage ValidateApproximateDiatanceUnitErrorLabel(string ExpectedText)
        {
            if (!string.IsNullOrEmpty(ExpectedText))
            {
                WaitForElementVisible(ApproximateDiatanceUnit_ErrorLabel);
                ValidateElementText(ApproximateDiatanceUnit_ErrorLabel, ExpectedText);
            }
            else
            {
                WaitForElementNotVisible(ApproximateDiatanceUnit_ErrorLabel, 3);
            }
            return this;
        }

        public PersonMobilityRecordPage ClickMobilisedToLink()
        {
            WaitForElementToBeClickable(MobilisedToLink);
            Click(MobilisedToLink);

            return this;
        }

        public PersonMobilityRecordPage ValidateMobilisedToLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(MobilisedToLink);
            ValidateElementText(MobilisedToLink, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage ClickMobilisedToLookupButton()
        {
            ScrollToElement(MobilisedToLookUpBtn);
            WaitForElementToBeClickable(MobilisedToLookUpBtn);
            Click(MobilisedToLookUpBtn);

            return this;
        }

        public PersonMobilityRecordPage ValidateMobilisedToErrorLabel(string ExpectedText)
        {
            WaitForElementVisible(MobilisedTo_ErrorLabel);
            ValidateElementText(MobilisedTo_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage InsertMobilisedToOther(string TextToInsert)
        {
            WaitForElement(mobilisedtoothertextField);
            SendKeys(mobilisedtoothertextField, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public PersonMobilityRecordPage ValidateMobilisedToOther(string ExpectedText)
        {
            WaitForElementVisible(mobilisedtoothertextField);
            ValidateElementText(mobilisedtoothertextField, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage ValidateMobilisedToOtherFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(mobilisedtoothertextField);
            }
            else
            {
                WaitForElementNotVisible(mobilisedtoothertextField, 3);
            }

            return this;
        }

        public PersonMobilityRecordPage ValidateMobilisedToOtherErrorLabel(string ExpectedText)
        {
            WaitForElementVisible(mobilisedtoothertext_ErrorLabel);
            ValidateElementText(mobilisedtoothertext_ErrorLabel, ExpectedText);

            return this;
        }

        #endregion

        #region Additional Information

        public PersonMobilityRecordPage ClickEquipment_SelectedElementLink(string ElementId)
        {
            WaitForElementToBeClickable(Equipment_SelectedElementLink(ElementId));
            Click(Equipment_SelectedElementLink(ElementId));

            return this;
        }

        public PersonMobilityRecordPage ValidateEquipment_SelectedElementLinkText(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(Equipment_SelectedElementLink(ElementId));
            ValidateElementText(Equipment_SelectedElementLink(ElementId), ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage ValidateEquipment_SelectedElementLinkText(Guid ElementId, string ExpectedText)
        {
            return ValidateEquipment_SelectedElementLinkText(ElementId.ToString(), ExpectedText);
        }

        public PersonMobilityRecordPage ClickEquipment_SelectedElementRemoveButton(string ElementId)
        {
            WaitForElementToBeClickable(Equipment_SelectedElementRemoveButton(ElementId));
            Click(Equipment_SelectedElementRemoveButton(ElementId));

            return this;
        }


        public PersonMobilityRecordPage ClickEquipmentLookUpBtn()
        {
            WaitForElementToBeClickable(EquipmentLookUpBtn);
            Click(EquipmentLookUpBtn);

            return this;
        }

        public PersonMobilityRecordPage ValidateEquipmentIfOtherText(string ExpectedText)
        {
            ValidateElementValue(equipmentifother, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage InsertTextOnEquipmentIfOther(string TextToInsert)
        {
            WaitForElementToBeClickable(equipmentifother);
            SendKeys(equipmentifother, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonMobilityRecordPage ValidateEquipmentIfOtherVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(equipmentifother);
            else
                WaitForElementNotVisible(equipmentifother, 3);

            return this;
        }

        public PersonMobilityRecordPage ClickAssistanceNeededLookUpBtn()
        {
            WaitForElementToBeClickable(AssistanceNeededLookUpBtn);
            Click(AssistanceNeededLookUpBtn);

            return this;
        }

        public PersonMobilityRecordPage ClickAssistanceNeededLink()
        {
            WaitForElementToBeClickable(AssistanceNeededLink);
            Click(AssistanceNeededLink);

            return this;
        }

        public PersonMobilityRecordPage ValidateAssistanceNeededLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(AssistanceNeededLink);
            ValidateElementText(AssistanceNeededLink, ExpectedText);

            return this;
        }

        //Select value from CareAssistanceLevePicklist
        public PersonMobilityRecordPage SelectAssistanceAmount(string TextToSelect)
        {
            WaitForElement(AssistanceAmountPicklist);
            ScrollToElement(AssistanceAmountPicklist);
            SelectPicklistElementByText(AssistanceAmountPicklist, TextToSelect);

            return this;
        }

        public PersonMobilityRecordPage ValidateSelectedAssistanceAmount(string ExpectedText)
        {
            ValidatePicklistSelectedText(AssistanceAmountPicklist, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage ClickWellbeingLookUpBtn()
        {
            WaitForElementToBeClickable(WellBeingLookUpBtn);
            Click(WellBeingLookUpBtn);

            return this;
        }

        public PersonMobilityRecordPage ClickWellbeingLink()
        {
            WaitForElementToBeClickable(WellBeingLink);
            Click(WellBeingLink);

            return this;
        }

        public PersonMobilityRecordPage ValidateWellbeingLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(WellBeingLink);
            ValidateElementText(WellBeingLink, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage ValidateActionTakenText(string ExpectedText)
        {
            ValidateElementValue(actiontaken, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage InsertTextOnActionTaken(string TextToInsert)
        {
            WaitForElementToBeClickable(actiontaken);
            SendKeys(actiontaken, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonMobilityRecordPage ValidateActionTakenVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(actiontaken);
            else
                WaitForElementNotVisible(actiontaken, 3);

            return this;
        }

        public PersonMobilityRecordPage SetTotalTimeSpentWithClient(string TotalTimeSpent)
        {
            ScrollToElement(TotalTimeSpentFld);
            WaitForElementToBeClickable(TotalTimeSpentFld);
            SendKeys(TotalTimeSpentFld, TotalTimeSpent + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        //Insert text in AdditionalNotesTextArea
        public PersonMobilityRecordPage InsertTextInAdditionalNotesTextArea(string TextToInsert)
        {
            WaitForElement(AdditionalNotesTextArea);
            ScrollToElement(AdditionalNotesTextArea);
            SendKeys(AdditionalNotesTextArea, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        //verify text in AdditionalNotesTextArea
        public PersonMobilityRecordPage VerifyTextInAdditionalNotesTextArea(string ExpectedText)
        {
            ScrollToElement(AdditionalNotesTextArea);
            WaitForElementVisible(AdditionalNotesTextArea);
            ValidateElementValue(AdditionalNotesTextArea, ExpectedText);

            return this;
        }

        public PersonMobilityRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveNCloseBtn);
            Click(SaveNCloseBtn);

            return this;
        }

        public PersonMobilityRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveBtn);
            Click(SaveBtn);

            return this;
        }

        //click BackButton
        public PersonMobilityRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public PersonMobilityRecordPage ValidateTotalTimeSpentFild(string totaltimespent)
        {

            ScrollToElement(TotalTimeSpentFld);
            WaitForElementVisible(TotalTimeSpentFld);

            ValidateElementValue(TotalTimeSpentFld, totaltimespent);

            return this;
        }

        public PersonMobilityRecordPage ValidateTextInCareNoteTextArea(string perfonfirstname, string locationfrom, string locationto, string approximateunit, string equipment, string wellbeing, string assistanceneed, string occureddatetime, string AssistanceAmount = "")
        {
            if (!string.IsNullOrEmpty(AssistanceAmount))
                AssistanceAmount = " " + AssistanceAmount;

            ScrollToElement(carenoteTextArea);
            WaitForElementVisible(carenoteTextArea);
            System.Threading.Thread.Sleep(1000);
            ValidateElementValue(carenoteTextArea, perfonfirstname + " moved from the " + locationfrom + " to the " + locationto + ", approximately 2 " + approximateunit + ".\r\n" +
perfonfirstname + " used the following equipment: " + equipment + ".\r\n" +
perfonfirstname + " came across as " + wellbeing + ".\r\n" +
perfonfirstname + " required assistance: " + assistanceneed + ". Amount given:" + AssistanceAmount + ".\r\n" +
"This care was given at " + occureddatetime + ".\r\n" +
perfonfirstname + " was assisted by 1 colleague(s).\r\n" +
"Overall, I spent 1 minutes with " + perfonfirstname + ".");


            return this;
        }

        public PersonMobilityRecordPage ValidateTextInCareNoteTextArea(string ExpectedText)
        {
            WaitForElement(carenoteTextArea);
            ScrollToElement(carenoteTextArea);
            WaitForElementVisible(carenoteTextArea);
            System.Threading.Thread.Sleep(1000);
            ValidateElementValue(carenoteTextArea, ExpectedText);

            return this;
        }

        #endregion

        #region Care Needs

        //click linkedactivitiesofadllookupbutton 
        public PersonMobilityRecordPage ClickLinkedActivitiesOfAdlLookupButton()
        {
            WaitForElement(LinkedActivitiesOfAdlLookupButton);
            ScrollToElement(LinkedActivitiesOfAdlLookupButton);
            Click(LinkedActivitiesOfAdlLookupButton);

            return this;
        }


        #endregion

        #region Handover

        public PersonMobilityRecordPage VerifyIncludeInNextHandoverOptions(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
            {
                WaitForElementVisible(IncludeInNextHandover_YesOption);
                WaitForElementVisible(IncludeInNextHandover_NoOption);
            }
            else
            {
                WaitForElementNotVisible(IncludeInNextHandover_YesOption, 2);
                WaitForElementNotVisible(IncludeInNextHandover_NoOption, 2);
            }

            return this;
        }

        public PersonMobilityRecordPage VerifyFlagRecordForHandoverOptions(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
            {
                WaitForElementVisible(FlagRecordForHandover_YesOption);
                WaitForElementVisible(FlagRecordForHandover_NoOption);
            }
            else
            {
                WaitForElementNotVisible(FlagRecordForHandover_YesOption, 2);
                WaitForElementNotVisible(FlagRecordForHandover_NoOption, 2);
            }

            return this;
        }

        public PersonMobilityRecordPage SelectIncludeInNextHandoverOption(bool Option)
        {
            if (Option)
            {
                Click(IncludeInNextHandover_YesOption);
            }
            else
            {
                Click(IncludeInNextHandover_NoOption);
            }

            return this;
        }

        public PersonMobilityRecordPage SelectFlagRecordForHandoverOption(bool Option)
        {
            if (Option)
            {
                Click(FlagRecordForHandover_YesOption);
            }
            else
            {
                Click(FlagRecordForHandover_NoOption);
            }

            return this;
        }

        //verify includeinnext handover option is selected or not selected
        public PersonMobilityRecordPage VerifyIncludeInNextHandoverOptionSelected(bool ExpectedOption)
        {
            if (ExpectedOption)
            {
                ValidateElementChecked(IncludeInNextHandover_YesOption);
                ValidateElementNotChecked(IncludeInNextHandover_NoOption);
            }
            else
            {
                ValidateElementNotChecked(IncludeInNextHandover_YesOption);
                ValidateElementChecked(IncludeInNextHandover_NoOption);
            }

            return this;
        }

        //verify flagrecordforhandover option is selected or not selected
        public PersonMobilityRecordPage VerifyFlagRecordForHandoverOptionSelected(bool ExpectedOption)
        {
            if (ExpectedOption)
            {
                ValidateElementChecked(FlagRecordForHandover_YesOption);
                ValidateElementNotChecked(FlagRecordForHandover_NoOption);
            }
            else
            {
                ValidateElementNotChecked(FlagRecordForHandover_YesOption);
                ValidateElementChecked(FlagRecordForHandover_NoOption);
            }

            return this;
        }


        #endregion

        #region Resident Voice section

        public PersonMobilityRecordPage WaitForResidentVoiceSectionToLoad()
        {
            WaitForElement(ResidentVoiceSection);
            SwitchToIframe(MobilityAttachmentIframe);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(SectionHeader);
            ScrollToElement(SectionHeader);
            WaitForElement(NewRecordButton);
            WaitForElement(DeleteRecordButton);

            return this;
        }

        public PersonMobilityRecordPage VerifyResidentVoiceSectionIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
            {
                ScrollToElement(ResidentVoiceSection);
                WaitForElementVisible(ResidentVoiceSection);
            }
            else
            {
                WaitForElementNotVisible(ResidentVoiceSection, 2);
            }

            return this;
        }

        public PersonMobilityRecordPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public PersonMobilityRecordPage NavigateToAttachmentsPage()
        {
            WaitForElement(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(RelatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(RelatedItemsLeftSubMenu);
                Click(RelatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(AttachmentsMenuItem);
            Click(AttachmentsMenuItem);

            return this;
        }

        #endregion
    }
}

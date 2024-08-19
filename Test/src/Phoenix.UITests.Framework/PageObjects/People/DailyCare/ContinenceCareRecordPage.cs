using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Phoenix.UITests.Framework.PageObjects.People;
using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ContinenceCareRecordPage : CommonMethods
    {
        public ContinenceCareRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cppersontoileting&')]");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Continence Care: ']");
        readonly By SaveNCloseBtn = By.Id("TI_SaveAndCloseButton");
        readonly By SaveBtn = By.Id("TI_SaveButton");
        readonly By BackButton = By.Id("BackButton");

        #region Fields mapping

        #region General

        readonly By PersonLookupFieldLink = By.Id("CWField_personid_Link");

        readonly By PreferencesTextareField = By.XPath("//*[@id = 'CWField_preferences']");

        readonly By DateAndTimeOccurredFieldLabel = By.Id("CWLabelHolder_occurred");
        readonly By DateAndTimeOccurred_DateField = By.Id("CWField_occurred");
        readonly By DateAndTimeOccurred_TimeField = By.Id("CWField_occurred_Time");
        readonly By DateAndTimeOccurred_DatePicker = By.Id("CWField_occurred_DatePicker");
        readonly By DateAndTimeOccurred_TimePicker = By.Id("CWField_occurred_Time_TimePicker");

        readonly By ConsentGivenPicklist = By.Id("CWField_careconsentgivenid");
        readonly By nonconsentDetail = By.Id("CWField_carenonconsentid");
        readonly By ReasonForAbsenceTextareaField = By.Id("CWField_reasonforabsence");
        readonly By ReasonConsentDeclinedTextareaField = By.Id("CWField_reasonconsentdeclined");
        readonly By EncouragementGivenTextareaField = By.Id("CWField_encouragementgiven");
        readonly By CareProvidedWithoutConsentFieldLabel = By.XPath("//*[@id = 'CWLabelHolder_careprovidedwithoutconsent']/label");
        readonly By CareProvidedWithoutConsent_YesOption = By.Id("CWField_careprovidedwithoutconsent_1");
        readonly By CareProvidedWithoutConsent_NoOption = By.Id("CWField_careprovidedwithoutconsent_0");

        readonly By deferredToDate = By.Id("CWField_deferredtodate");
        readonly By deferredToDate_DatePicker = By.Id("CWField_deferredtodate_DatePicker");
        readonly By deferredToDate_ErrorLabel = By.XPath("//*[@id='CWControlHolder_deferredtodate']/label/span");

        readonly By deferredToTimeOrShift = By.Id("CWField_timeorshiftid");
        readonly By deferredToTimeOrShift_ErrorLabel = By.XPath("//*[@id='CWControlHolder_timeorshiftid']/label/span");

        readonly By deferredToTime = By.XPath("//*[@id = 'CWField_deferredtotime']");
        readonly By deferredtotime_TimePicker = By.XPath("//*[@id = 'CWField_deferredtotime_TimePicker']");
        readonly By deferredToTime_ErrorLabel = By.XPath("//*[@id='CWControlHolder_deferredtotime']/label/span");

        readonly By deferredToShift_LookupButton = By.Id("CWLookupBtn_deferredtoselectedshiftid");
        readonly By deferredToShift_LinkField = By.Id("CWField_deferredtoselectedshiftid_Link");
        readonly By deferredToShift_ClearButton = By.Id("CWClearLookup_deferredtoselectedshiftid");
        readonly By deferredToShift_ErrorLabel = By.XPath("//*[@id='CWControlHolder_deferredtoselectedshiftid']/label/span");

        #endregion

        #region Urine & Catheter Care

        readonly By Cathetercarerequired_1 = By.XPath("//*[@id='CWField_cathetercarerequired_1']");
        readonly By Cathetercarerequired_0 = By.XPath("//*[@id='CWField_cathetercarerequired_0']");
        readonly By Catheterpatentanddraining_1 = By.XPath("//*[@id='CWField_catheterpatentanddraining_1']");
        readonly By Catheterpatentanddraining_0 = By.XPath("//*[@id='CWField_catheterpatentanddraining_0']");
        readonly By Catheterbagemptied_1 = By.XPath("//*[@id='CWField_catheterbagemptied_1']");
        readonly By Catheterbagemptied_0 = By.XPath("//*[@id='CWField_catheterbagemptied_0']");
        readonly By Passedurine_1 = By.XPath("//*[@id='CWField_passedurine_1']");
        readonly By Passedurine_0 = By.XPath("//*[@id='CWField_passedurine_0']");
        readonly By Urineoutputamount = By.XPath("//*[@id='CWField_urineoutputamount']");
        readonly By UrinecolouridLink = By.XPath("//*[@id='CWField_urinecolourid_Link']");
        readonly By UrinecolouridClearButton = By.XPath("//*[@id='CWClearLookup_urinecolourid']");
        readonly By UrinecolouridLookupButton = By.XPath("//*[@id='CWLookupBtn_urinecolourid']");
        readonly By Catheterpositionedsecured_1 = By.XPath("//*[@id='CWField_catheterpositionedsecured_1']");
        readonly By Catheterpositionedsecured_0 = By.XPath("//*[@id='CWField_catheterpositionedsecured_0']");
        readonly By Catheterareaproperlycleaned_1 = By.XPath("//*[@id='CWField_catheterareaproperlycleaned_1']");
        readonly By Catheterareaproperlycleaned_0 = By.XPath("//*[@id='CWField_catheterareaproperlycleaned_0']");
        readonly By Malodour_1 = By.XPath("//*[@id='CWField_malodour_1']");
        readonly By Malodour_0 = By.XPath("//*[@id='CWField_malodour_0']");

        #endregion

        #region Stool

        readonly By Bowelsopened_1 = By.XPath("//*[@id='CWField_bowelsopened_1']");
        readonly By Bowelsopened_0 = By.XPath("//*[@id='CWField_bowelsopened_0']");
        readonly By Stooltypeid = By.XPath("//*[@id='CWField_stooltypeid']");
        readonly By Stoolamountid = By.XPath("//*[@id='CWField_stoolamountid']");
        readonly By Mucuspresent_1 = By.XPath("//*[@id='CWField_mucuspresent_1']");
        readonly By Mucuspresent_0 = By.XPath("//*[@id='CWField_mucuspresent_0']");
        readonly By Bloodpresent_1 = By.XPath("//*[@id='CWField_bloodpresent_1']");
        readonly By Bloodpresent_0 = By.XPath("//*[@id='CWField_bloodpresent_0']");
        readonly By LasttimebowelsopenedTextareaField = By.XPath("//*[@id='CWField_lasttimebowelsopened']");
        readonly By StoolactiontakenTextareaField = By.XPath("//*[@id='CWField_stoolactiontaken']");

        #endregion

        #region Other

        readonly By Skinconcernisany_1 = By.XPath("//*[@id='CWField_skinconcernisany_1']");
        readonly By Skinconcernisany_0 = By.XPath("//*[@id='CWField_skinconcernisany_0']");
        readonly By Skinconcernwhere = By.XPath("//*[@id='CWField_skinconcernwhere']");

        By DescribeSkinCondition_SelectedElementLink(string ElementId) => By.XPath("//*[@id='MS_skinconditions_" + ElementId + "']/a[@id='" + ElementId + "_Link']");
        By DescribeSkinCondition_SelectedElementRemoveButton(string ElementId) => By.XPath("//*[@id='MS_skinconditions_" + ElementId + "']/a[text()='Remove']");
        readonly By SkinconditionsLookupButton = By.XPath("//*[@id='CWLookupBtn_skinconditions']");

        readonly By Continencepadchangedid = By.XPath("//*[@id='CWField_continencepadchangedid']");
        readonly By Seniorreviewrequired_1 = By.XPath("//*[@id='CWField_seniorreviewrequired_1']");
        readonly By Seniorreviewrequired_0 = By.XPath("//*[@id='CWField_seniorreviewrequired_0']");
        readonly By Reviewdetails = By.XPath("//*[@id='CWField_reviewdetails']");

        #endregion

        #region Additional Information

        By carephysicallocation_SelectedElementLink(string ElementId) => By.XPath("//*[@id='MS_carephysicallocation_" + ElementId + "']/a[@id='" + ElementId + "_Link']");
        By carephysicallocation_SelectedElementRemoveButton(string ElementId) => By.XPath("//*[@id='MS_carephysicallocation_" + ElementId + "']/a[text()='Remove']");
        readonly By CarephysicallocationLookupButton = By.XPath("//*[@id='CWLookupBtn_carephysicallocation']");

        readonly By CarewellbeingidLink = By.XPath("//*[@id='CWField_carewellbeingid_Link']");
        readonly By CarewellbeingidClearButton = By.XPath("//*[@id='CWClearLookup_carewellbeingid']");
        readonly By CarewellbeingidLookupButton = By.XPath("//*[@id='CWLookupBtn_carewellbeingid']");

        readonly By Actiontaken = By.XPath("//*[@id='CWField_actiontaken']");
        readonly By Additionalnotes = By.XPath("//*[@id='CWField_additionalnotes']");

        By Equipment_SelectedElementLink(string ElementId) => By.XPath("//*[@id='MS_equipment_" + ElementId + "']/a[@id='" + ElementId + "_Link']");
        By Equipment_SelectedElementRemoveButton(string ElementId) => By.XPath("//*[@id='MS_equipment_" + ElementId + "']/a[text()='Remove']");
        readonly By EquipmentLookupButton = By.XPath("//*[@id='CWLookupBtn_equipment']");

        readonly By CareassistanceneededidLink = By.XPath("//*[@id='CWField_careassistanceneededid_Link']");
        readonly By CareassistanceneededidClearButton = By.XPath("//*[@id='CWClearLookup_careassistanceneededid']");
        readonly By CareassistanceneededidLookupButton = By.XPath("//*[@id='CWLookupBtn_careassistanceneededid']");

        By Otherstaffwhoassistedid_SelectedElementLink(string ElementId) => By.XPath("//*[@id='MS_otherstaffwhoassistedid_" + ElementId + "']/a[@id='" + ElementId + "_Link']");
        By Otherstaffwhoassistedid_SelectedElementRemoveButton(string ElementId) => By.XPath("//*[@id='MS_otherstaffwhoassistedid_" + ElementId + "']/a[text()='Remove']");
        readonly By OtherstaffwhoassistedidLookupButton = By.XPath("//*[@id='CWLookupBtn_otherstaffwhoassistedid']");

        readonly By TotalTimeSpentWithClientMinutesFieldLabel = By.XPath("//*[@id = 'CWLabelHolder_timespentwithclient']/label[text() = 'Total Time Spent With Person (Minutes)']");
        readonly By TotalTimeSpentWithClientMinutesField = By.Id("CWField_timespentwithclient");
        readonly By TotalTimeSpentWithClientMinutesFieldError = By.XPath("//label[@for = 'CWField_timespentwithclient'][@class = 'formerror']/span");

        #endregion

        #region Care Note

        readonly string CarenoteFieldId = "CWField_carenote";
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

        #endregion

        #region Field Labels

        By FieldLabel(string FieldName) => By.XPath("//*[starts-with(@id, 'CWLabelHolder_')]/*[text() = '" + FieldName + "']");
        By MandatoryField_Label(string FieldName) => By.XPath("//label[text()='" + FieldName + "']/span[@class='mandatory']");

        #endregion

        public ContinenceCareRecordPage WaitForPageToLoad()
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

        public ContinenceCareRecordPage VerifyPageHeaderText(string ExpectedText)
        {
            ScrollToElement(pageHeader);
            WaitForElementVisible(pageHeader);
            string pageTitle = GetElementByAttributeValue(pageHeader, "title");
            Assert.AreEqual("Continence Care: " + ExpectedText, pageTitle);

            return this;
        }


        #region Options Toolbar

        public ContinenceCareRecordPage ClickSaveAndClose()
        {
            WaitForElementToBeClickable(SaveNCloseBtn);
            Click(SaveNCloseBtn);

            return this;
        }

        public ContinenceCareRecordPage ClickSave()
        {
            WaitForElementToBeClickable(SaveBtn);
            Click(SaveBtn);

            return this;
        }

        public ContinenceCareRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        #endregion

        #region General

        public ContinenceCareRecordPage SetDateOccurred(String dateoccured)
        {
            WaitForElement(DateAndTimeOccurred_DateField);
            SendKeys(DateAndTimeOccurred_DateField, dateoccured);

            return this;
        }

        //click DateAndTimeOccurred_DatePicker
        public ContinenceCareRecordPage ClickDateAndTimeOccurredDatePicker()
        {
            WaitForElement(DateAndTimeOccurred_DatePicker);
            ScrollToElement(DateAndTimeOccurred_DatePicker);
            Click(DateAndTimeOccurred_DatePicker);

            return this;
        }

        public ContinenceCareRecordPage SetTimeOccurred(String timeoccured)
        {
            WaitForElement(DateAndTimeOccurred_TimeField);
            System.Threading.Thread.Sleep(1000);
            ScrollToElement(DateAndTimeOccurred_TimeField);
            WaitForElementVisible(DateAndTimeOccurred_TimeField);
            Click(DateAndTimeOccurred_TimeField);
            ClearText(DateAndTimeOccurred_TimeField);
            System.Threading.Thread.Sleep(1000);
            SendKeys(DateAndTimeOccurred_TimeField, timeoccured);


            return this;
        }

        //click DateAndTimeOccurred_TimePicker
        public ContinenceCareRecordPage ClickDateAndTimeOccurredTimePicker()
        {
            WaitForElement(DateAndTimeOccurred_TimePicker);
            ScrollToElement(DateAndTimeOccurred_TimePicker);
            Click(DateAndTimeOccurred_TimePicker);

            return this;
        }

        //verify personlookupfieldlinktext
        public ContinenceCareRecordPage VerifyPersonLookupFieldLinkText(string ExpectedText)
        {
            ScrollToElement(PersonLookupFieldLink);
            WaitForElementVisible(PersonLookupFieldLink);
            ValidateElementByTitle(PersonLookupFieldLink, ExpectedText);

            return this;
        }

        //verify totaltimespentwithclientminutesfield is present or not
        public ContinenceCareRecordPage VerifyTotalTimeSpentWithClientMinutesFieldIsDisplayed(bool ExpectedPresent)
        {
            if (ExpectedPresent)
            {
                WaitForElementVisible(TotalTimeSpentWithClientMinutesFieldLabel);
                ScrollToElement(TotalTimeSpentWithClientMinutesField);
                WaitForElementVisible(TotalTimeSpentWithClientMinutesField);
            }
            else
            {
                WaitForElementNotVisible(TotalTimeSpentWithClientMinutesField, 2);
            }

            return this;
        }

        //verify totaltimespentwithclientminutesfield
        public ContinenceCareRecordPage VerifyTotalTimeSpentWithClientMinutesFieldText(string ExpectedText)
        {
            ScrollToElement(TotalTimeSpentWithClientMinutesField);
            WaitForElementVisible(TotalTimeSpentWithClientMinutesField);
            ValidateElementValue(TotalTimeSpentWithClientMinutesField, ExpectedText);

            return this;
        }

        //verify totaltimespentwithclientminutesfield error
        public ContinenceCareRecordPage VerifyTotalTimeSpentWithClientMinutesFieldErrorText(string ExpectedText)
        {
            WaitForElement(TotalTimeSpentWithClientMinutesFieldError);
            ScrollToElement(TotalTimeSpentWithClientMinutesFieldError);
            ValidateElementByTitle(TotalTimeSpentWithClientMinutesFieldError, ExpectedText);

            return this;
        }

        //verify dateandtimeoccurredfieldlabel and dateandtimeoccurredfield is displayed or not displayed
        public ContinenceCareRecordPage VerifyDateAndTimeOccurredFieldsAreDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
            {
                WaitForElementVisible(DateAndTimeOccurredFieldLabel);
                ScrollToElement(DateAndTimeOccurredFieldLabel);
                WaitForElementVisible(DateAndTimeOccurred_DateField);
                ScrollToElement(DateAndTimeOccurred_TimeField);
                WaitForElementVisible(DateAndTimeOccurred_TimeField);
            }
            else
            {
                WaitForElementNotVisible(DateAndTimeOccurred_DateField, 2);
                WaitForElementNotVisible(DateAndTimeOccurred_TimeField, 2);
            }

            return this;
        }

        //verify dateandtimeoccurred_datefield
        public ContinenceCareRecordPage VerifyDateAndTimeOccurredDateFieldText(string ExpectedText)
        {
            ScrollToElement(DateAndTimeOccurred_DateField);
            WaitForElementVisible(DateAndTimeOccurred_DateField);
            ValidateElementValue(DateAndTimeOccurred_DateField, ExpectedText);

            return this;
        }

        //verify dateandtimeoccurred_timefield
        public ContinenceCareRecordPage VerifyDateAndTimeOccurredTimeFieldText(string ExpectedText)
        {
            ScrollToElement(DateAndTimeOccurred_TimeField);
            WaitForElementVisible(DateAndTimeOccurred_TimeField);
            ValidateElementValue(DateAndTimeOccurred_TimeField, ExpectedText);

            return this;
        }

        //verify preferences textare field is displayed or not displayed
        public ContinenceCareRecordPage VerifyPreferencesTextAreaFieldIsDisplayed(bool ExpectedDisplayed)
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
        public ContinenceCareRecordPage VerifyPreferencesTextAreaFieldIsDisabled(bool ExpectedDisabled)
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
        public ContinenceCareRecordPage VerifyPreferencesTextAreaFieldText(string ExpectedText)
        {
            ScrollToElement(PreferencesTextareField);
            WaitForElementVisible(PreferencesTextareField);
            ValidateElementValue(PreferencesTextareField, ExpectedText);

            return this;
        }

        //verify preference textarea field attribute value
        public ContinenceCareRecordPage VerifyPreferencesTextAreaFieldMaxLength(string ExpectedValue)
        {
            ScrollToElement(PreferencesTextareField);
            WaitForElementVisible(PreferencesTextareField);
            ValidateElementAttribute(PreferencesTextareField, "maxlength", ExpectedValue);

            return this;
        }

        //Continence Care
        //Insert text in ReasonConsentDeclinedTextareaField
        public ContinenceCareRecordPage InsertTextInReasonConsentDeclinedTextareaField(String TextToInsert)
        {
            ScrollToElement(ReasonConsentDeclinedTextareaField);
            WaitForElementVisible(ReasonConsentDeclinedTextareaField);
            SendKeys(ReasonConsentDeclinedTextareaField, TextToInsert + Keys.Tab);

            return this;
        }

        //verify ReasonConsentDeclinedTextareaField text
        public ContinenceCareRecordPage VerifyReasonConsentDeclinedTextareaFieldText(string ExpectedText)
        {
            ScrollToElement(ReasonConsentDeclinedTextareaField);
            WaitForElementVisible(ReasonConsentDeclinedTextareaField);
            ValidateElementValue(ReasonConsentDeclinedTextareaField, ExpectedText);

            return this;
        }

        //verify reasonconsentdeclined field is displayed or not displayed
        public ContinenceCareRecordPage ValidateReasonConsentDeclinedTextareaFieldFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ReasonConsentDeclinedTextareaField);
            else
                WaitForElementNotVisible(ReasonConsentDeclinedTextareaField, 3);

            return this;
        }

        //Insert text in EncouragementGivenTextareaField
        public ContinenceCareRecordPage InsertTextInEncouragementGivenTextareaField(String TextToInsert)
        {
            ScrollToElement(EncouragementGivenTextareaField);
            WaitForElementVisible(EncouragementGivenTextareaField);
            SendKeys(EncouragementGivenTextareaField, TextToInsert + Keys.Tab);

            return this;
        }

        //verify EncouragementGivenTextareaField text
        public ContinenceCareRecordPage VerifyEncouragementGivenTextareaFieldText(string ExpectedText)
        {
            ScrollToElement(EncouragementGivenTextareaField);
            WaitForElementVisible(EncouragementGivenTextareaField);
            ValidateElementValue(EncouragementGivenTextareaField, ExpectedText);

            return this;
        }

        //verify encouragementgiven field is displayed or not displayed
        public ContinenceCareRecordPage ValidateEncouragementGivenFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(EncouragementGivenTextareaField);
            else
                WaitForElementNotVisible(EncouragementGivenTextareaField, 3);

            return this;
        }

        //Select CareProvidedWithoutConsent_YesOption
        public ContinenceCareRecordPage SelectCareProvidedWithoutConsent_YesOption()
        {
            ScrollToElement(CareProvidedWithoutConsent_YesOption);
            WaitForElementVisible(CareProvidedWithoutConsent_YesOption);
            Click(CareProvidedWithoutConsent_YesOption);

            return this;
        }

        //Select CareProvidedWithoutConsent_NoOption
        public ContinenceCareRecordPage SelectCareProvidedWithoutConsent_NoOption()
        {
            ScrollToElement(CareProvidedWithoutConsent_NoOption);
            WaitForElementVisible(CareProvidedWithoutConsent_NoOption);
            Click(CareProvidedWithoutConsent_NoOption);

            return this;
        }

        //verify CareProvidedWithoutConsent_YesOption selected or not selected
        public ContinenceCareRecordPage VerifyCareProvidedWithoutConsent_YesOptionSelected(bool ExpectedSelected)
        {
            ScrollToElement(CareProvidedWithoutConsentFieldLabel);
            WaitForElementVisible(CareProvidedWithoutConsentFieldLabel);

            if (ExpectedSelected)
                ValidateElementChecked(CareProvidedWithoutConsent_YesOption);
            else
                ValidateElementNotChecked(CareProvidedWithoutConsent_YesOption);

            return this;
        }

        //verify CareProvidedWithoutConsent_NoOption selected or not selected
        public ContinenceCareRecordPage VerifyCareProvidedWithoutConsent_NoOptionSelected(bool ExpectedSelected)
        {
            ScrollToElement(CareProvidedWithoutConsentFieldLabel);
            WaitForElementVisible(CareProvidedWithoutConsentFieldLabel);

            if (ExpectedSelected)
                ValidateElementChecked(CareProvidedWithoutConsent_NoOption);
            else
                ValidateElementNotChecked(CareProvidedWithoutConsent_NoOption);

            return this;
        }

        //verify CareProvidedWithoutConsent options are displayed or not displayed
        public ContinenceCareRecordPage ValidateCareProvidedWithoutConsentOptionsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(CareProvidedWithoutConsent_YesOption);
                WaitForElementVisible(CareProvidedWithoutConsent_NoOption);
            }
            else
            {
                WaitForElementNotVisible(CareProvidedWithoutConsent_YesOption, 3);
                WaitForElementNotVisible(CareProvidedWithoutConsent_NoOption, 3);
            }

            return this;
        }

        public ContinenceCareRecordPage SetDeferredToDate(string TextToInsert)
        {
            WaitForElementVisible(deferredToDate);
            SendKeys(deferredToDate, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public ContinenceCareRecordPage ValidateDeferredToDate(string ExpectedText)
        {
            ValidateElementValue(deferredToDate, ExpectedText);

            return this;
        }

        public ContinenceCareRecordPage ValidateDeferredToDateErrorLabel(string ExpectedText)
        {
            WaitForElementVisible(deferredToDate_ErrorLabel);
            ValidateElementText(deferredToDate_ErrorLabel, ExpectedText);

            return this;
        }

        //Validate deferred to date field is displayed or not displayed
        public ContinenceCareRecordPage ValidateDeferredToDateFieldVisible(bool ExpectVisible)
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

        //click deferred to date datepicker
        public ContinenceCareRecordPage ClickDeferredToDate_DatePicker()
        {
            WaitForElementToBeClickable(deferredToDate_DatePicker);
            Click(deferredToDate_DatePicker);

            return this;
        }

        //verify deferred to date datepicker is displayed or not displayed
        public ContinenceCareRecordPage VerifyDeferredToDate_DatePickerIsDisplayed(bool ExpectedDisplayed)
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

        public ContinenceCareRecordPage SelectDeferredToTimeOrShift(string TextToSelect)
        {
            WaitForElementVisible(deferredToTimeOrShift);
            SelectPicklistElementByText(deferredToTimeOrShift, TextToSelect);

            return this;
        }

        public ContinenceCareRecordPage ValidateSelectedDeferredToTimeOrShift(string ExpectedText)
        {
            ValidatePicklistSelectedText(deferredToTimeOrShift, ExpectedText);

            return this;
        }

        public ContinenceCareRecordPage ValidateDeferredToTimeOrShiftErrorLabel(string ExpectedText)
        {
            WaitForElementVisible(deferredToTimeOrShift_ErrorLabel);
            ValidateElementText(deferredToTimeOrShift_ErrorLabel, ExpectedText);

            return this;
        }

        //verify deferredToTimeorsift field is displayed or not displayed
        public ContinenceCareRecordPage ValidateDeferredToTimeOrShiftFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(deferredToTimeOrShift);
            else
                WaitForElementNotVisible(deferredToTimeOrShift, 3);

            return this;
        }

        public  ContinenceCareRecordPage SetDeferredToTime(string TextToInsert)
        {
            WaitForElement(deferredToTime);
            ScrollToElement(deferredToTime);
            SendKeys(deferredToTime, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public ContinenceCareRecordPage ValidateDeferredToTime(string ExpectedText)
        {
            ValidateElementValue(deferredToTime, ExpectedText);

            return this;
        }

        //verify deferredToTime field is displayed or not displayed
        public ContinenceCareRecordPage ValidateDeferredToTimeFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(deferredToTime);
            else
                WaitForElementNotVisible(deferredToTime, 3);

            return this;
        }

        public ContinenceCareRecordPage ValidateDeferredToTimeErrorLabel(string ExpectedText)
        {
            WaitForElementVisible(deferredToTime_ErrorLabel);
            ValidateElementText(deferredToTime_ErrorLabel, ExpectedText);

            return this;
        }

        //click deferredtotime_TimePicker
        public ContinenceCareRecordPage ClickDeferredToTime_TimePicker()
        {           
            System.Threading.Thread.Sleep(1000);
            SetDeferredToTime(Keys.Tab);
            WaitForElement(deferredtotime_TimePicker);
            ScrollToElement(deferredtotime_TimePicker);
            Click(deferredtotime_TimePicker);

            return this;
        }

        //verify deferredtotime_TimePicker is displayed or not displayed
        public ContinenceCareRecordPage VerifyDeferredToTime_TimePickerIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(deferredtotime_TimePicker);
            else

                WaitForElementNotVisible(deferredtotime_TimePicker, 2);

            return this;
        }


        public ContinenceCareRecordPage ClickDeferredToShiftLookupButton()
        {
            WaitForElementToBeClickable(deferredToShift_LookupButton);
            Click(deferredToShift_LookupButton);

            return this;
        }

        //verify deferredToShift_LookupButton is displayed or not displayed
        public ContinenceCareRecordPage ValidateDeferredToShiftLookupButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(deferredToShift_LookupButton);
            else
                WaitForElementNotVisible(deferredToShift_LookupButton, 3);

            return this;
        }

        public ContinenceCareRecordPage ValidateDeferredToShiftLinkText(string ExpectedText)
        {
            ValidateElementText(deferredToShift_LinkField, ExpectedText);

            return this;
        }

        public ContinenceCareRecordPage ClickDeferredToShiftClearButton()
        {
            Click(deferredToShift_ClearButton);

            return this;
        }

        public ContinenceCareRecordPage ValidateDeferredToShiftErrorLabel(string ExpectedText)
        {
            WaitForElementVisible(deferredToShift_ErrorLabel);
            ValidateElementText(deferredToShift_ErrorLabel, ExpectedText);

            return this;
        }


        //Select value from consentgivenpicklist by text
        public ContinenceCareRecordPage SelectConsentGivenPicklistValueByText(string TextToSelect)
        {
            ScrollToElement(ConsentGivenPicklist);
            WaitForElementVisible(ConsentGivenPicklist);
            SelectPicklistElementByText(ConsentGivenPicklist, TextToSelect);

            return this;
        }

        //verify consentgivenpicklist selected value
        public ContinenceCareRecordPage VerifyConsentGivenPicklistSelectedValue(string ExpectedText)
        {
            ScrollToElement(ConsentGivenPicklist);
            WaitForElementVisible(ConsentGivenPicklist);
            ValidatePicklistSelectedText(ConsentGivenPicklist, ExpectedText);

            return this;
        }

        //Select value from nonconsentdetail by text
        public ContinenceCareRecordPage SelectNonConsentDetailValueByText(string TextToSelect)
        {
            ScrollToElement(nonconsentDetail);
            WaitForElementVisible(nonconsentDetail);
            SelectPicklistElementByText(nonconsentDetail, TextToSelect);

            return this;
        }

        //verify nonconsentdetail selected value
        public ContinenceCareRecordPage VerifyNonConsentDetailSelectedValue(string ExpectedText)
        {
            ScrollToElement(nonconsentDetail);
            WaitForElementVisible(nonconsentDetail);
            ValidatePicklistSelectedText(nonconsentDetail, ExpectedText);

            return this;
        }

        //Insert text in reasonforabsence textarea field
        public ContinenceCareRecordPage InsertTextInReasonForAbsence(String TextToInsert)
        {
            ScrollToElement(ReasonForAbsenceTextareaField);
            WaitForElementVisible(ReasonForAbsenceTextareaField);
            SendKeys(ReasonForAbsenceTextareaField, TextToInsert + Keys.Tab);

            return this;
        }

        //verify reasonforabsence textarea field text
        public ContinenceCareRecordPage VerifyReasonForAbsenceTextareaFieldText(string ExpectedText)
        {
            ScrollToElement(ReasonForAbsenceTextareaField);
            WaitForElementVisible(ReasonForAbsenceTextareaField);
            ValidateElementValue(ReasonForAbsenceTextareaField, ExpectedText);

            return this;
        }

        //verify reasonforabsence field is displayed or not displayed
        public ContinenceCareRecordPage ValidateReasonForAbsenceTextareaFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ReasonForAbsenceTextareaField);
            else
                WaitForElementNotVisible(ReasonForAbsenceTextareaField, 3);

            return this;
        }


        #endregion

        #region Urine & Catheter Care

        public ContinenceCareRecordPage ClickDoesThePersonNeedCatheterCare_YesRadioButton()
        {
            WaitForElementToBeClickable(Cathetercarerequired_1);
            Click(Cathetercarerequired_1);

            return this;
        }

        public ContinenceCareRecordPage ValidateDoesThePersonNeedCatheterCare_YesRadioButtonChecked()
        {
            WaitForElement(Cathetercarerequired_1);
            ValidateElementChecked(Cathetercarerequired_1);

            return this;
        }

        public ContinenceCareRecordPage ValidateDoesThePersonNeedCatheterCare_YesRadioButtonNotChecked()
        {
            WaitForElement(Cathetercarerequired_1);
            ValidateElementNotChecked(Cathetercarerequired_1);

            return this;
        }

        public ContinenceCareRecordPage ClickDoesThePersonNeedCatheterCare_NoRadioButton()
        {
            WaitForElementToBeClickable(Cathetercarerequired_0);
            Click(Cathetercarerequired_0);

            return this;
        }

        public ContinenceCareRecordPage ValidateDoesThePersonNeedCatheterCare_NoRadioButtonChecked()
        {
            WaitForElement(Cathetercarerequired_0);
            ValidateElementChecked(Cathetercarerequired_0);

            return this;
        }

        public ContinenceCareRecordPage ValidateDoesThePersonNeedCatheterCare_NoRadioButtonNotChecked()
        {
            WaitForElement(Cathetercarerequired_0);
            ValidateElementNotChecked(Cathetercarerequired_0);

            return this;
        }

        //verify IsTheCatheterPatentAndDraining options are displayed or not displayed
        public ContinenceCareRecordPage ValidateIsTheCatheterPatentAndDrainingOptionsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Catheterpatentanddraining_1);
                WaitForElementVisible(Catheterpatentanddraining_0);
            }
            else
            {
                WaitForElementNotVisible(Catheterpatentanddraining_1, 3);
                WaitForElementNotVisible(Catheterpatentanddraining_0, 3);
            }

            return this;
        }

        public ContinenceCareRecordPage ClickIsTheCatheterPatentAndDraining_YesRadioButton()
        {
            WaitForElementToBeClickable(Catheterpatentanddraining_1);
            Click(Catheterpatentanddraining_1);

            return this;
        }

        public ContinenceCareRecordPage ValidateIsTheCatheterPatentAndDraining_YesRadioButtonChecked()
        {
            WaitForElement(Catheterpatentanddraining_1);
            ValidateElementChecked(Catheterpatentanddraining_1);

            return this;
        }

        public ContinenceCareRecordPage ValidateIsTheCatheterPatentAndDraining_YesRadioButtonNotChecked()
        {
            WaitForElement(Catheterpatentanddraining_1);
            ValidateElementNotChecked(Catheterpatentanddraining_1);

            return this;
        }

        public ContinenceCareRecordPage ClickIsTheCatheterPatentAndDraining_NoRadioButton()
        {
            WaitForElementToBeClickable(Catheterpatentanddraining_0);
            Click(Catheterpatentanddraining_0);

            return this;
        }

        public ContinenceCareRecordPage ValidateIsTheCatheterPatentAndDraining_NoRadioButtonChecked()
        {
            WaitForElement(Catheterpatentanddraining_0);
            ValidateElementChecked(Catheterpatentanddraining_0);

            return this;
        }

        public ContinenceCareRecordPage ValidateIsTheCatheterPatentAndDraining_NoRadioButtonNotChecked()
        {
            WaitForElement(Catheterpatentanddraining_0);
            ValidateElementNotChecked(Catheterpatentanddraining_0);

            return this;
        }

        //verify HasTheCatheterBagBeenEmptied options are displayed or not displayed
        public ContinenceCareRecordPage ValidateHasTheCatheterBagBeenEmptiedOptionsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Catheterbagemptied_1);
                WaitForElementVisible(Catheterbagemptied_0);
            }
            else
            {
                WaitForElementNotVisible(Catheterbagemptied_1, 3);
                WaitForElementNotVisible(Catheterbagemptied_0, 3);
            }

            return this;
        }

        public ContinenceCareRecordPage ClickHasTheCatheterBagBeenEmptied_YesRadioButton()
        {
            WaitForElementToBeClickable(Catheterbagemptied_1);
            Click(Catheterbagemptied_1);

            return this;
        }

        public ContinenceCareRecordPage ValidateHasTheCatheterBagBeenEmptied_YesRadioButtonChecked()
        {
            WaitForElement(Catheterbagemptied_1);
            ValidateElementChecked(Catheterbagemptied_1);

            return this;
        }

        public ContinenceCareRecordPage ValidateHasTheCatheterBagBeenEmptied_YesRadioButtonNotChecked()
        {
            WaitForElement(Catheterbagemptied_1);
            ValidateElementNotChecked(Catheterbagemptied_1);

            return this;
        }

        public ContinenceCareRecordPage ClickHasTheCatheterBagBeenEmptied_NoRadioButton()
        {
            WaitForElementToBeClickable(Catheterbagemptied_0);
            Click(Catheterbagemptied_0);

            return this;
        }

        public ContinenceCareRecordPage ValidateHasTheCatheterBagBeenEmptied_NoRadioButtonChecked()
        {
            WaitForElement(Catheterbagemptied_0);
            ValidateElementChecked(Catheterbagemptied_0);

            return this;
        }

        public ContinenceCareRecordPage ValidateHasTheCatheterBagBeenEmptied_NoRadioButtonNotChecked()
        {
            WaitForElement(Catheterbagemptied_0);
            ValidateElementNotChecked(Catheterbagemptied_0);

            return this;
        }

        public ContinenceCareRecordPage ClickPassedUrine_YesRadioButton()
        {
            WaitForElementToBeClickable(Passedurine_1);
            Click(Passedurine_1);

            return this;
        }

        public ContinenceCareRecordPage ValidatePassedUrine_YesRadioButtonChecked()
        {
            WaitForElement(Passedurine_1);
            ValidateElementChecked(Passedurine_1);

            return this;
        }

        public ContinenceCareRecordPage ValidatePassedUrine_YesRadioButtonNotChecked()
        {
            WaitForElement(Passedurine_1);
            ValidateElementNotChecked(Passedurine_1);

            return this;
        }

        public ContinenceCareRecordPage ClickPassedUrine_NoRadioButton()
        {
            WaitForElementToBeClickable(Passedurine_0);
            Click(Passedurine_0);

            return this;
        }

        public ContinenceCareRecordPage ValidatePassedUrine_NoRadioButtonChecked()
        {
            WaitForElement(Passedurine_0);
            ValidateElementChecked(Passedurine_0);

            return this;
        }

        public ContinenceCareRecordPage ValidatePassedUrine_NoRadioButtonNotChecked()
        {
            WaitForElement(Passedurine_0);
            ValidateElementNotChecked(Passedurine_0);

            return this;
        }

        //verify Urineoutputamount field is displayed or not displayed
        public ContinenceCareRecordPage ValidateUrineOutputAmountFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Urineoutputamount);
            else
                WaitForElementNotVisible(Urineoutputamount, 3);

            return this;
        }

        public ContinenceCareRecordPage ValidateUrineOutputAmountText(string ExpectedText)
        {
            ValidateElementValue(Urineoutputamount, ExpectedText);

            return this;
        }

        public ContinenceCareRecordPage InsertTextOnUrineOutputAmount(string TextToInsert)
        {
            WaitForElementToBeClickable(Urineoutputamount);
            SendKeys(Urineoutputamount, TextToInsert);

            return this;
        }

        public ContinenceCareRecordPage ClickDescribeUrineColourLink()
        {
            WaitForElementToBeClickable(UrinecolouridLink);
            Click(UrinecolouridLink);

            return this;
        }

        public ContinenceCareRecordPage ValidateDescribeUrineColourLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(UrinecolouridLink);
            ValidateElementText(UrinecolouridLink, ExpectedText);

            return this;
        }

        public ContinenceCareRecordPage ClickDescribeUrineColourClearButton()
        {
            WaitForElementToBeClickable(UrinecolouridClearButton);
            Click(UrinecolouridClearButton);

            return this;
        }

        public ContinenceCareRecordPage ClickDescribeUrineColourLookupButton()
        {
            WaitForElementToBeClickable(UrinecolouridLookupButton);
            Click(UrinecolouridLookupButton);

            return this;
        }

        //verify UrinecolouridLookupButton is displayed or not displayed
        public ContinenceCareRecordPage ValidateDescribeUrineColourLookupButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(UrinecolouridLookupButton);
            else
                WaitForElementNotVisible(UrinecolouridLookupButton, 3);

            return this;
        }

        //verify IsTheCatheterPositionedSecured options are displayed or not displayed
        public ContinenceCareRecordPage ValidateIsTheCatheterPositionedSecuredOptionsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Catheterpositionedsecured_1);
                WaitForElementVisible(Catheterpositionedsecured_0);
            }
            else
            {
                WaitForElementNotVisible(Catheterpositionedsecured_1, 3);
                WaitForElementNotVisible(Catheterpositionedsecured_0, 3);
            }

            return this;
        }

        public ContinenceCareRecordPage ClickIsTheCatheterPositionedSecured_YesRadioButton()
        {
            WaitForElementToBeClickable(Catheterpositionedsecured_1);
            Click(Catheterpositionedsecured_1);

            return this;
        }

        public ContinenceCareRecordPage ValidateIsTheCatheterPositionedSecured_YesRadioButtonChecked()
        {
            WaitForElement(Catheterpositionedsecured_1);
            ValidateElementChecked(Catheterpositionedsecured_1);

            return this;
        }

        public ContinenceCareRecordPage ValidateIsTheCatheterPositionedSecured_YesRadioButtonNotChecked()
        {
            WaitForElement(Catheterpositionedsecured_1);
            ValidateElementNotChecked(Catheterpositionedsecured_1);

            return this;
        }

        public ContinenceCareRecordPage ClickIsTheCatheterPositionedSecured_NoRadioButton()
        {
            WaitForElementToBeClickable(Catheterpositionedsecured_0);
            Click(Catheterpositionedsecured_0);

            return this;
        }

        public ContinenceCareRecordPage ValidateIsTheCatheterPositionedSecured_NoRadioButtonChecked()
        {
            WaitForElement(Catheterpositionedsecured_0);
            ValidateElementChecked(Catheterpositionedsecured_0);

            return this;
        }

        public ContinenceCareRecordPage ValidateIsTheCatheterPositionedSecured_NoRadioButtonNotChecked()
        {
            WaitForElement(Catheterpositionedsecured_0);
            ValidateElementNotChecked(Catheterpositionedsecured_0);

            return this;
        }

        //verify HaveYouCleanedTheCatheterArea options are displayed or not displayed
        public ContinenceCareRecordPage ValidateHaveYouCleanedTheCatheterAreaOptionsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Catheterareaproperlycleaned_1);
                WaitForElementVisible(Catheterareaproperlycleaned_0);
            }
            else
            {
                WaitForElementNotVisible(Catheterareaproperlycleaned_1, 3);
                WaitForElementNotVisible(Catheterareaproperlycleaned_0, 3);
            }

            return this;
        }

        public ContinenceCareRecordPage ClickHaveYouCleanedTheCatheterArea_YesRadioButton()
        {
            WaitForElementToBeClickable(Catheterareaproperlycleaned_1);
            Click(Catheterareaproperlycleaned_1);

            return this;
        }

        public ContinenceCareRecordPage ValidateHaveYouCleanedTheCatheterArea_YesRadioButtonChecked()
        {
            WaitForElement(Catheterareaproperlycleaned_1);
            ValidateElementChecked(Catheterareaproperlycleaned_1);

            return this;
        }

        public ContinenceCareRecordPage ValidateHaveYouCleanedTheCatheterArea_YesRadioButtonNotChecked()
        {
            WaitForElement(Catheterareaproperlycleaned_1);
            ValidateElementNotChecked(Catheterareaproperlycleaned_1);

            return this;
        }

        public ContinenceCareRecordPage ClickHaveYouCleanedTheCatheterArea_NoRadioButton()
        {
            WaitForElementToBeClickable(Catheterareaproperlycleaned_0);
            Click(Catheterareaproperlycleaned_0);

            return this;
        }

        public ContinenceCareRecordPage ValidateHaveYouCleanedTheCatheterArea_NoRadioButtonChecked()
        {
            WaitForElement(Catheterareaproperlycleaned_0);
            ValidateElementChecked(Catheterareaproperlycleaned_0);

            return this;
        }

        public ContinenceCareRecordPage ValidateHaveYouCleanedTheCatheterArea_NoRadioButtonNotChecked()
        {
            WaitForElement(Catheterareaproperlycleaned_0);
            ValidateElementNotChecked(Catheterareaproperlycleaned_0);

            return this;
        }

        //verify isthereanymalodour options are displayed or not displayed
        public ContinenceCareRecordPage ValidateIsThereAnyMalodourOptionsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Malodour_1);
                WaitForElementVisible(Malodour_0);
            }
            else
            {
                WaitForElementNotVisible(Malodour_1, 3);
                WaitForElementNotVisible(Malodour_0, 3);
            }

            return this;
        }

        public ContinenceCareRecordPage ClickIsThereAnyMalodour_YesRadioButton()
        {
            WaitForElementToBeClickable(Malodour_1);
            Click(Malodour_1);

            return this;
        }

        public ContinenceCareRecordPage ValidateIsThereAnyMalodour_YesRadioButtonChecked()
        {
            WaitForElement(Malodour_1);
            ValidateElementChecked(Malodour_1);

            return this;
        }

        public ContinenceCareRecordPage ValidateIsThereAnyMalodour_YesRadioButtonNotChecked()
        {
            WaitForElement(Malodour_1);
            ValidateElementNotChecked(Malodour_1);

            return this;
        }

        public ContinenceCareRecordPage ClickIsThereAnyMalodour_NoRadioButton()
        {
            WaitForElementToBeClickable(Malodour_0);
            Click(Malodour_0);

            return this;
        }

        public ContinenceCareRecordPage ValidateIsThereAnyMalodour_NoRadioButtonChecked()
        {
            WaitForElement(Malodour_0);
            ValidateElementChecked(Malodour_0);

            return this;
        }

        public ContinenceCareRecordPage ValidateIsThereAnyMalodour_NoRadioButtonNotChecked()
        {
            WaitForElement(Malodour_0);
            ValidateElementNotChecked(Malodour_0);

            return this;
        }

        #endregion

        #region Stool

        public ContinenceCareRecordPage ClickBowelsOpened_YesRadioButton()
        {
            WaitForElementToBeClickable(Bowelsopened_1);
            Click(Bowelsopened_1);

            return this;
        }

        public ContinenceCareRecordPage ValidateBowelsOpened_YesRadioButtonChecked()
        {
            WaitForElement(Bowelsopened_1);
            ValidateElementChecked(Bowelsopened_1);

            return this;
        }

        public ContinenceCareRecordPage ValidateBowelsOpened_YesRadioButtonNotChecked()
        {
            WaitForElement(Bowelsopened_1);
            ValidateElementNotChecked(Bowelsopened_1);

            return this;
        }

        public ContinenceCareRecordPage ClickBowelsOpened_NoRadioButton()
        {
            WaitForElementToBeClickable(Bowelsopened_0);
            Click(Bowelsopened_0);

            return this;
        }

        public ContinenceCareRecordPage ValidateBowelsOpened_NoRadioButtonChecked()
        {
            WaitForElement(Bowelsopened_0);
            ValidateElementChecked(Bowelsopened_0);

            return this;
        }

        public ContinenceCareRecordPage ValidateBowelsOpened_NoRadioButtonNotChecked()
        {
            WaitForElement(Bowelsopened_0);
            ValidateElementNotChecked(Bowelsopened_0);

            return this;
        }

        //insert text in LasttimebowelsopenedTextareaField
        public ContinenceCareRecordPage InsertTextInLastTimeBowelsOpenedTextareaField(String TextToInsert)
        {
            WaitForElement(LasttimebowelsopenedTextareaField);
            ScrollToElement(LasttimebowelsopenedTextareaField);
            SendKeys(LasttimebowelsopenedTextareaField, TextToInsert + Keys.Tab);

            return this;
        }

        //verify LasttimebowelsopenedTextareaField is displayed or not displayed
        public ContinenceCareRecordPage ValidateLastTimeBowelsOpenedTextareaFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(LasttimebowelsopenedTextareaField);
            else
                WaitForElementNotVisible(LasttimebowelsopenedTextareaField, 3);

            return this;
        }

        //Verify LasttimebowelsopenedTextareaField text
        public ContinenceCareRecordPage VerifyLastTimeBowelsOpenedTextareaFieldText(string ExpectedText)
        {
            WaitForElement(LasttimebowelsopenedTextareaField);
            ScrollToElement(LasttimebowelsopenedTextareaField);
            ValidateElementValue(LasttimebowelsopenedTextareaField, ExpectedText);

            return this;
        }

        //Insert text in StoolactiontakenTextareaField
        public ContinenceCareRecordPage InsertTextInStoolActionTakenTextareaField(String TextToInsert)
        {
            WaitForElement(StoolactiontakenTextareaField);
            ScrollToElement(StoolactiontakenTextareaField);
            SendKeys(StoolactiontakenTextareaField, TextToInsert + Keys.Tab);

            return this;
        }

        //verify StoolactiontakenTextareaField is displayed or not displayed
        public ContinenceCareRecordPage ValidateStoolActionTakenTextareaFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(StoolactiontakenTextareaField);
            else
                WaitForElementNotVisible(StoolactiontakenTextareaField, 3);

            return this;
        }

        //Verify text in StoolactiontakenTextareaField
        public ContinenceCareRecordPage VerifyStoolActionTakenTextareaFieldText(string ExpectedText)
        {
            WaitForElement(StoolactiontakenTextareaField);
            ScrollToElement(StoolactiontakenTextareaField);
            ValidateElementValue(StoolactiontakenTextareaField, ExpectedText);

            return this;
        }

        public ContinenceCareRecordPage SelectStoolType(string TextToSelect)
        {
            WaitForElementToBeClickable(Stooltypeid);
            SelectPicklistElementByText(Stooltypeid, TextToSelect);

            return this;
        }

        public ContinenceCareRecordPage ValidateStoolTypeSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Stooltypeid, ExpectedText);

            return this;
        }

        //verify Stooltypeid is displayed or not displayed
        public ContinenceCareRecordPage ValidateStoolTypeFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Stooltypeid);
            else
                WaitForElementNotVisible(Stooltypeid, 3);

            return this;
        }

        public ContinenceCareRecordPage SelectStoolAmount(string TextToSelect)
        {
            WaitForElementToBeClickable(Stoolamountid);
            SelectPicklistElementByText(Stoolamountid, TextToSelect);

            return this;
        }

        public ContinenceCareRecordPage ValidateStoolAmountSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Stoolamountid, ExpectedText);

            return this;
        }

        //verify Stoolamountid is displayed or not displayed
        public ContinenceCareRecordPage ValidateStoolAmountFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Stoolamountid);
            else
                WaitForElementNotVisible(Stoolamountid, 3);

            return this;
        }

        //verify mucuspresent options are displayed or not displayed
        public ContinenceCareRecordPage ValidateMucusPresentOptionsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Mucuspresent_1);
                WaitForElementVisible(Mucuspresent_0);
            }
            else
            {
                WaitForElementNotVisible(Mucuspresent_1, 3);
                WaitForElementNotVisible(Mucuspresent_0, 3);
            }

            return this;
        }

        public ContinenceCareRecordPage ClickMucusPresent_YesRadioButton()
        {
            WaitForElementToBeClickable(Mucuspresent_1);
            Click(Mucuspresent_1);

            return this;
        }

        public ContinenceCareRecordPage ValidateMucusPresent_YesRadioButtonChecked()
        {
            WaitForElement(Mucuspresent_1);
            ValidateElementChecked(Mucuspresent_1);

            return this;
        }

        public ContinenceCareRecordPage ValidateMucusPresent_YesRadioButtonNotChecked()
        {
            WaitForElement(Mucuspresent_1);
            ValidateElementNotChecked(Mucuspresent_1);

            return this;
        }

        public ContinenceCareRecordPage ClickMucusPresent_NoRadioButton()
        {
            WaitForElementToBeClickable(Mucuspresent_0);
            Click(Mucuspresent_0);

            return this;
        }

        public ContinenceCareRecordPage ValidateMucusPresent_NoRadioButtonChecked()
        {
            WaitForElement(Mucuspresent_0);
            ValidateElementChecked(Mucuspresent_0);

            return this;
        }

        public ContinenceCareRecordPage ValidateMucusPresent_NoRadioButtonNotChecked()
        {
            WaitForElement(Mucuspresent_0);
            ValidateElementNotChecked(Mucuspresent_0);

            return this;
        }

        //verify bloodpresent options are displayed or not displayed
        public ContinenceCareRecordPage ValidateBloodPresentOptionsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Bloodpresent_1);
                WaitForElementVisible(Bloodpresent_0);
            }
            else
            {
                WaitForElementNotVisible(Bloodpresent_1, 3);
                WaitForElementNotVisible(Bloodpresent_0, 3);
            }

            return this;
        }

        public ContinenceCareRecordPage ClickBloodPresent_YesRadioButton()
        {
            WaitForElementToBeClickable(Bloodpresent_1);
            Click(Bloodpresent_1);

            return this;
        }

        public ContinenceCareRecordPage ValidateBloodPresent_YesRadioButtonChecked()
        {
            WaitForElement(Bloodpresent_1);
            ValidateElementChecked(Bloodpresent_1);

            return this;
        }

        public ContinenceCareRecordPage ValidateBloodPresent_YesRadioButtonNotChecked()
        {
            WaitForElement(Bloodpresent_1);
            ValidateElementNotChecked(Bloodpresent_1);

            return this;
        }

        public ContinenceCareRecordPage ClickBloodPresent_NoRadioButton()
        {
            WaitForElementToBeClickable(Bloodpresent_0);
            Click(Bloodpresent_0);

            return this;
        }

        public ContinenceCareRecordPage ValidateBloodPresent_NoRadioButtonChecked()
        {
            WaitForElement(Bloodpresent_0);
            ValidateElementChecked(Bloodpresent_0);

            return this;
        }

        public ContinenceCareRecordPage ValidateBloodPresent_NoRadioButtonNotChecked()
        {
            WaitForElement(Bloodpresent_0);
            ValidateElementNotChecked(Bloodpresent_0);

            return this;
        }

        #endregion

        #region Other

        public ContinenceCareRecordPage ClickAreThereAnyNewConcernsWithThePersonSkin_YesRadioButton()
        {
            WaitForElementToBeClickable(Skinconcernisany_1);
            Click(Skinconcernisany_1);

            return this;
        }

        public ContinenceCareRecordPage ValidateAreThereAnyNewConcernsWithThePersonSkin_YesRadioButtonChecked()
        {
            WaitForElement(Skinconcernisany_1);
            ValidateElementChecked(Skinconcernisany_1);

            return this;
        }

        public ContinenceCareRecordPage ValidateAreThereAnyNewConcernsWithThePersonSkin_YesRadioButtonNotChecked()
        {
            WaitForElement(Skinconcernisany_1);
            ValidateElementNotChecked(Skinconcernisany_1);

            return this;
        }

        public ContinenceCareRecordPage ClickAreThereAnyNewConcernsWithThePersonSkin_NoRadioButton()
        {
            WaitForElementToBeClickable(Skinconcernisany_0);
            Click(Skinconcernisany_0);

            return this;
        }

        public ContinenceCareRecordPage ValidateAreThereAnyNewConcernsWithThePersonSkin_NoRadioButtonChecked()
        {
            WaitForElement(Skinconcernisany_0);
            ValidateElementChecked(Skinconcernisany_0);

            return this;
        }

        public ContinenceCareRecordPage ValidateAreThereAnyNewConcernsWithThePersonSkin_NoRadioButtonNotChecked()
        {
            WaitForElement(Skinconcernisany_0);
            ValidateElementNotChecked(Skinconcernisany_0);

            return this;
        }

        public ContinenceCareRecordPage ValidateWhereOnTheBodyText(string ExpectedText)
        {
            ValidateElementText(Skinconcernwhere, ExpectedText);

            return this;
        }

        public ContinenceCareRecordPage InsertTextOnWhereOnTheBody(string TextToInsert)
        {
            WaitForElementToBeClickable(Skinconcernwhere);
            SendKeys(Skinconcernwhere, TextToInsert);

            return this;
        }

        public ContinenceCareRecordPage ClickDescribeSkinCondition_SelectedElementLink(string ElementId)
        {
            WaitForElementToBeClickable(DescribeSkinCondition_SelectedElementLink(ElementId));
            Click(DescribeSkinCondition_SelectedElementLink(ElementId));

            return this;
        }

        public ContinenceCareRecordPage ValidateDescribeSkinCondition_SelectedElementLinkText(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(DescribeSkinCondition_SelectedElementLink(ElementId));
            ValidateElementText(DescribeSkinCondition_SelectedElementLink(ElementId), ExpectedText);

            return this;
        }

        public ContinenceCareRecordPage ValidateDescribeSkinCondition_SelectedElementLinkText(Guid ElementId, string ExpectedText)
        {
            return ValidateDescribeSkinCondition_SelectedElementLinkText(ElementId.ToString(), ExpectedText);
        }

        public ContinenceCareRecordPage ClickDescribeSkinCondition_SelectedElementRemoveButton(string ElementId)
        {
            WaitForElementToBeClickable(DescribeSkinCondition_SelectedElementRemoveButton(ElementId));
            Click(DescribeSkinCondition_SelectedElementRemoveButton(ElementId));

            return this;
        }

        public ContinenceCareRecordPage ClickDescribeSkinConditionLookupButton()
        {
            WaitForElementToBeClickable(SkinconditionsLookupButton);
            Click(SkinconditionsLookupButton);

            return this;
        }

        //verify Continencepadchangedid is displayed or not displayed
        public ContinenceCareRecordPage ValidateContinencePadChangedVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Continencepadchangedid);
            else
                WaitForElementNotVisible(Continencepadchangedid, 3);

            return this;
        }

        public ContinenceCareRecordPage SelectHasContinencePadBeenChanged(string TextToSelect)
        {
            WaitForElementToBeClickable(Continencepadchangedid);
            SelectPicklistElementByText(Continencepadchangedid, TextToSelect);

            return this;
        }

        public ContinenceCareRecordPage ValidateHasContinencePadBeenChangedSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Continencepadchangedid, ExpectedText);

            return this;
        }

        //verify ReviewRequiredBySeniorColleague options are displayed or not displayed
        public ContinenceCareRecordPage ValidateReviewRequiredBySeniorColleagueOptionsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Seniorreviewrequired_1);
                WaitForElementVisible(Seniorreviewrequired_0);
            }
            else
            {
                WaitForElementNotVisible(Seniorreviewrequired_1, 3);
                WaitForElementNotVisible(Seniorreviewrequired_0, 3);
            }

            return this;
        }

        public ContinenceCareRecordPage ClickReviewRequiredBySeniorColleague_YesRadioButton()
        {
            WaitForElementToBeClickable(Seniorreviewrequired_1);
            Click(Seniorreviewrequired_1);

            return this;
        }

        public ContinenceCareRecordPage ValidateReviewRequiredBySeniorColleague_YesRadioButtonChecked()
        {
            WaitForElement(Seniorreviewrequired_1);
            ValidateElementChecked(Seniorreviewrequired_1);

            return this;
        }

        public ContinenceCareRecordPage ValidateReviewRequiredBySeniorColleague_YesRadioButtonNotChecked()
        {
            WaitForElement(Seniorreviewrequired_1);
            ValidateElementNotChecked(Seniorreviewrequired_1);

            return this;
        }

        public ContinenceCareRecordPage ClickReviewRequiredBySeniorColleague_NoRadioButton()
        {
            WaitForElementToBeClickable(Seniorreviewrequired_0);
            Click(Seniorreviewrequired_0);

            return this;
        }

        public ContinenceCareRecordPage ValidateReviewRequiredBySeniorColleague_NoRadioButtonChecked()
        {
            WaitForElement(Seniorreviewrequired_0);
            ValidateElementChecked(Seniorreviewrequired_0);

            return this;
        }

        public ContinenceCareRecordPage ValidateReviewRequiredBySeniorColleague_NoRadioButtonNotChecked()
        {
            WaitForElement(Seniorreviewrequired_0);
            ValidateElementNotChecked(Seniorreviewrequired_0);

            return this;
        }

        public ContinenceCareRecordPage ValidateReviewDetailsText(string ExpectedText)
        {
            ValidateElementText(Reviewdetails, ExpectedText);

            return this;
        }

        public ContinenceCareRecordPage InsertTextOnReviewDetails(string TextToInsert)
        {
            WaitForElementToBeClickable(Reviewdetails);
            SendKeys(Reviewdetails, TextToInsert);

            return this;
        }

        #endregion

        #region Additional Information

        public ContinenceCareRecordPage InsertTotalTimeSpentWithClientMinutes(String TextToInsert)
        {
            ScrollToElement(TotalTimeSpentWithClientMinutesField);
            WaitForElementVisible(TotalTimeSpentWithClientMinutesField);
            SendKeys(TotalTimeSpentWithClientMinutesField, TextToInsert + Keys.Tab);


            return this;
        }

        public ContinenceCareRecordPage ClickLocation_SelectedElementLink(string ElementId)
        {
            WaitForElementToBeClickable(carephysicallocation_SelectedElementLink(ElementId));
            Click(carephysicallocation_SelectedElementLink(ElementId));

            return this;
        }

        public ContinenceCareRecordPage ValidateLocation_SelectedElementLinkText(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(carephysicallocation_SelectedElementLink(ElementId));
            ValidateElementText(carephysicallocation_SelectedElementLink(ElementId), ExpectedText);

            return this;
        }

        public ContinenceCareRecordPage ValidateLocation_SelectedElementLinkText(Guid ElementId, string ExpectedText)
        {
            return ValidateLocation_SelectedElementLinkText(ElementId.ToString(), ExpectedText);
        }

        public ContinenceCareRecordPage ClickLocation_SelectedElementRemoveButton(string ElementId)
        {
            WaitForElementToBeClickable(carephysicallocation_SelectedElementRemoveButton(ElementId));
            Click(carephysicallocation_SelectedElementRemoveButton(ElementId));

            return this;
        }

        public ContinenceCareRecordPage ClickLocationLookupButton()
        {
            WaitForElementToBeClickable(CarephysicallocationLookupButton);
            Click(CarephysicallocationLookupButton);

            return this;
        }

        public ContinenceCareRecordPage ClickWellbeingLink()
        {
            WaitForElementToBeClickable(CarewellbeingidLink);
            Click(CarewellbeingidLink);

            return this;
        }

        public ContinenceCareRecordPage ValidateWellbeingLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CarewellbeingidLink);
            ValidateElementText(CarewellbeingidLink, ExpectedText);

            return this;
        }

        public ContinenceCareRecordPage ClickWellbeingClearButton()
        {
            WaitForElementToBeClickable(CarewellbeingidClearButton);
            Click(CarewellbeingidClearButton);

            return this;
        }

        public ContinenceCareRecordPage ClickWellbeingLookupButton()
        {
            WaitForElementToBeClickable(CarewellbeingidLookupButton);
            Click(CarewellbeingidLookupButton);

            return this;
        }

        public ContinenceCareRecordPage ValidateActionTakenText(string ExpectedText)
        {
            ValidateElementText(Actiontaken, ExpectedText);

            return this;
        }

        public ContinenceCareRecordPage InsertTextOnActionTaken(string TextToInsert)
        {
            WaitForElementToBeClickable(Actiontaken);
            SendKeys(Actiontaken, TextToInsert);

            return this;
        }

        public ContinenceCareRecordPage ValidateAdditionalNotesText(string ExpectedText)
        {
            ValidateElementText(Additionalnotes, ExpectedText);

            return this;
        }

        public ContinenceCareRecordPage InsertTextOnAdditionalNotes(string TextToInsert)
        {
            WaitForElementToBeClickable(Additionalnotes);
            SendKeys(Additionalnotes, TextToInsert);

            return this;
        }

        public ContinenceCareRecordPage ClickEquipment_SelectedElementLink(string ElementId)
        {
            WaitForElementToBeClickable(Equipment_SelectedElementLink(ElementId));
            Click(Equipment_SelectedElementLink(ElementId));

            return this;
        }

        public ContinenceCareRecordPage ValidateEquipment_SelectedElementLinkText(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(Equipment_SelectedElementLink(ElementId));
            ValidateElementText(Equipment_SelectedElementLink(ElementId), ExpectedText);

            return this;
        }

        public ContinenceCareRecordPage ValidateEquipment_SelectedElementLinkText(Guid ElementId, string ExpectedText)
        {
            return ValidateEquipment_SelectedElementLinkText(ElementId.ToString(), ExpectedText);
        }

        public ContinenceCareRecordPage ClickEquipment_SelectedElementRemoveButton(string ElementId)
        {
            WaitForElementToBeClickable(Equipment_SelectedElementRemoveButton(ElementId));
            Click(Equipment_SelectedElementRemoveButton(ElementId));

            return this;
        }

        public ContinenceCareRecordPage ClickEquipmentLookupButton()
        {
            WaitForElementToBeClickable(EquipmentLookupButton);
            Click(EquipmentLookupButton);

            return this;
        }

        public ContinenceCareRecordPage ClickAssistanceNeededLink()
        {
            WaitForElementToBeClickable(CareassistanceneededidLink);
            Click(CareassistanceneededidLink);

            return this;
        }

        public ContinenceCareRecordPage ValidateAssistanceNeededLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CareassistanceneededidLink);
            ValidateElementText(CareassistanceneededidLink, ExpectedText);

            return this;
        }

        public ContinenceCareRecordPage ClickAssistanceNeededClearButton()
        {
            WaitForElementToBeClickable(CareassistanceneededidClearButton);
            Click(CareassistanceneededidClearButton);

            return this;
        }

        public ContinenceCareRecordPage ClickAssistanceNeededLookupButton()
        {
            WaitForElementToBeClickable(CareassistanceneededidLookupButton);
            Click(CareassistanceneededidLookupButton);

            return this;
        }

        public ContinenceCareRecordPage ClickStaffRequired_SelectedElementLink(string ElementId)
        {
            WaitForElementToBeClickable(Otherstaffwhoassistedid_SelectedElementLink(ElementId));
            Click(Otherstaffwhoassistedid_SelectedElementLink(ElementId));

            return this;
        }

        public ContinenceCareRecordPage ValidateStaffRequired_SelectedElementLinkText(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(Otherstaffwhoassistedid_SelectedElementLink(ElementId));
            ValidateElementText(Otherstaffwhoassistedid_SelectedElementLink(ElementId), ExpectedText);

            return this;
        }

        public ContinenceCareRecordPage ValidateStaffRequired_SelectedElementLinkText(Guid ElementId, string ExpectedText)
        {
            return ValidateStaffRequired_SelectedElementLinkText(ElementId.ToString(), ExpectedText);
        }

        public ContinenceCareRecordPage ClickStaffRequired_SelectedElementRemoveButton(string ElementId)
        {
            WaitForElementToBeClickable(Otherstaffwhoassistedid_SelectedElementRemoveButton(ElementId));
            Click(Otherstaffwhoassistedid_SelectedElementRemoveButton(ElementId));

            return this;
        }

        public ContinenceCareRecordPage ClickStaffRequiredLookupButton()
        {
            WaitForElementToBeClickable(OtherstaffwhoassistedidLookupButton);
            Click(OtherstaffwhoassistedidLookupButton);

            return this;
        }

        #endregion

        #region Care Note

        //verify Carenote field is displayed or not displayed
        public ContinenceCareRecordPage ValidateCareNoteFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Carenote);
            else
                WaitForElementNotVisible(Carenote, 3);

            return this;
        }

        public ContinenceCareRecordPage ValidateCareNoteText(string ExpectedText)
        {
            WaitForElement(Carenote);
            ScrollToElement(Carenote);
            var fieldValue = GetElementValueByJavascript(CarenoteFieldId);
            Assert.AreEqual(ExpectedText, fieldValue);

            return this;
        }

        public ContinenceCareRecordPage InsertTextOnCareNote(string TextToInsert)
        {
            WaitForElementToBeClickable(Carenote);
            SendKeys(Carenote, TextToInsert);

            return this;
        }

        #endregion

        #region Care Needs

        public ContinenceCareRecordPage ClickLinkedActivitiesOfDailyLivingLookupButton()
        {
            WaitForElementToBeClickable(LinkedadlcategoriesidLookupButton);
            Click(LinkedadlcategoriesidLookupButton);

            return this;
        }

        #endregion

        #region Handover

        public ContinenceCareRecordPage ClickIncludeInNextHandover_YesRadioButton()
        {
            WaitForElementToBeClickable(Isincludeinnexthandover_1);
            Click(Isincludeinnexthandover_1);

            return this;
        }

        public ContinenceCareRecordPage ValidateIncludeInNextHandover_YesRadioButtonChecked()
        {
            WaitForElement(Isincludeinnexthandover_1);
            ValidateElementChecked(Isincludeinnexthandover_1);

            return this;
        }

        public ContinenceCareRecordPage ValidateIncludeInNextHandover_YesRadioButtonNotChecked()
        {
            WaitForElement(Isincludeinnexthandover_1);
            ValidateElementNotChecked(Isincludeinnexthandover_1);

            return this;
        }

        public ContinenceCareRecordPage ClickIncludeInNextHandover_NoRadioButton()
        {
            WaitForElementToBeClickable(Isincludeinnexthandover_0);
            Click(Isincludeinnexthandover_0);

            return this;
        }

        public ContinenceCareRecordPage ValidateIncludeInNextHandover_NoRadioButtonChecked()
        {
            WaitForElement(Isincludeinnexthandover_0);
            ValidateElementChecked(Isincludeinnexthandover_0);

            return this;
        }

        public ContinenceCareRecordPage ValidateIncludeInNextHandover_NoRadioButtonNotChecked()
        {
            WaitForElement(Isincludeinnexthandover_0);
            ValidateElementNotChecked(Isincludeinnexthandover_0);

            return this;
        }

        public ContinenceCareRecordPage ClickFlagRecordForHandover_YesRadioButton()
        {
            WaitForElementToBeClickable(Flagrecordforhandover_1);
            Click(Flagrecordforhandover_1);

            return this;
        }

        public ContinenceCareRecordPage ValidateFlagRecordForHandover_YesRadioButtonChecked()
        {
            WaitForElement(Flagrecordforhandover_1);
            ValidateElementChecked(Flagrecordforhandover_1);

            return this;
        }

        public ContinenceCareRecordPage ValidateFlagRecordForHandover_YesRadioButtonNotChecked()
        {
            WaitForElement(Flagrecordforhandover_1);
            ValidateElementNotChecked(Flagrecordforhandover_1);

            return this;
        }

        public ContinenceCareRecordPage ClickFlagRecordForHandover_NoRadioButton()
        {
            WaitForElementToBeClickable(Flagrecordforhandover_0);
            Click(Flagrecordforhandover_0);

            return this;
        }

        public ContinenceCareRecordPage ValidateFlagRecordForHandover_NoRadioButtonChecked()
        {
            WaitForElement(Flagrecordforhandover_0);
            ValidateElementChecked(Flagrecordforhandover_0);

            return this;
        }

        public ContinenceCareRecordPage ValidateFlagRecordForHandover_NoRadioButtonNotChecked()
        {
            WaitForElement(Flagrecordforhandover_0);
            ValidateElementNotChecked(Flagrecordforhandover_0);

            return this;
        }

        #endregion

        public ContinenceCareRecordPage ValidateFieldIsVisible(string FieldName, bool ExpectVisible = true)
        {
            if (ExpectVisible)
            { 
                ScrollToElement(FieldLabel(FieldName));
                WaitForElementVisible(FieldLabel(FieldName));
            }
            else
                WaitForElementNotVisible(FieldLabel(FieldName), 3);

            return this;
        }

        public ContinenceCareRecordPage ValidateMandatoryFieldIsVisible(string FieldName, bool ExpectVisible = true)
        {
            if (ExpectVisible)
                WaitForElementVisible(MandatoryField_Label(FieldName));
            else
                WaitForElementNotVisible(MandatoryField_Label(FieldName), 3);

            return this;
        }

    }
}

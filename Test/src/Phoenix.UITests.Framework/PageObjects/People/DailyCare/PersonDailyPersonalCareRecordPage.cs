using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Net.NetworkInformation;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonDailyPersonalCareRecordPage : CommonMethods
    {

        public PersonDailyPersonalCareRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cppersonpersonalcaredailyrecord&')]");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Person Daily Personal Care: ']");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By BackButton = By.Id("BackButton");


        #region Fields Mapping

        #region General

        readonly By PersonidLink = By.XPath("//*[@id='CWField_personid_Link']");
        readonly By PersonidLookupButton = By.XPath("//*[@id='CWLookupBtn_personid']");

        readonly By PreferencesTextareField = By.XPath("//*[@id = 'CWField_preferences']");

        readonly By Careconsentgivenid = By.XPath("//*[@id='CWField_careconsentgivenid']");

        readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");

        readonly By DateAndTimeOccurredFieldLabel = By.Id("CWLabelHolder_occurred");
        readonly By DateAndTimeOccurred_DateField = By.Id("CWField_occurred");
        readonly By DateAndTimeOccurred_TimeField = By.Id("CWField_occurred_Time");
        readonly By DateAndTimeOccurred_DatePicker = By.Id("CWField_occurred_DatePicker");
        readonly By DateAndTimeOccurred_TimePicker = By.Id("CWField_occurred_Time_TimePicker");

        readonly By reasonforabsence = By.Id("CWField_reasonforabsence");

        readonly By reasonconsentdeclined = By.Id("CWField_reasonconsentdeclined");
        readonly By encouragementgiven = By.Id("CWField_encouragementgiven");
        readonly By careprovidedwithoutconsent_1 = By.Id("CWField_careprovidedwithoutconsent_1");
        readonly By careprovidedwithoutconsent_0 = By.Id("CWField_careprovidedwithoutconsent_0");

        readonly By nonconsentDetail = By.Id("CWField_carenonconsentid");

        readonly By deferredToDateFieldLabel = By.XPath("//*[@id = 'CWLabelHolder_deferredtodate']/label");
        readonly By deferredToDate = By.Id("CWField_deferredtodate");
        readonly By deferredToDate_DatePicker = By.Id("CWField_deferredtodate_DatePicker");
        readonly By deferredToDate_ErrorLabel = By.XPath("//*[@id='CWControlHolder_deferredtodate']/label/span");

        readonly By deferredToTimeOrShift = By.Id("CWField_timeorshiftid");
        readonly By deferredToTimeOrShift_ErrorLabel = By.XPath("//*[@id='CWControlHolder_timeorshiftid']/label/span");

        readonly By deferredToTime = By.Id("CWField_deferredtotime");
        readonly By deferredToTime_ErrorLabel = By.XPath("//*[@id='CWControlHolder_deferredtotime']/label/span");

        readonly By deferredToShift_LookupButton = By.Id("CWLookupBtn_deferredtoselectedshiftid");
        readonly By deferredToShift_LinkField = By.Id("CWField_deferredtoselectedshiftid_Link");
        readonly By deferredToShift_ClearButton = By.Id("CWClearLookup_deferredtoselectedshiftid");
        readonly By deferredToShift_ErrorLabel = By.XPath("//*[@id='CWControlHolder_deferredtoselectedshiftid']/label/span");

        #endregion

        #region Details

        readonly By WashidLookupButton = By.XPath("//*[@id='CWLookupBtn_washid']");
        By wash_OptionLinkField(string recordId) => By.XPath("//*[@id='MS_washid_" + recordId + "']/a[@id='" + recordId + "_Link']");
        By wash_OptionRemoveButton(string recordId) => By.XPath("//*[@id='MS_washid_" + recordId + "']/a[text()='Remove']");


        readonly By BodyAreasLookupButton = By.XPath("//*[@id='CWLookupBtn_personalcarebodyareasid']");
        By BodyAreas_OptionLinkField(string recordId) => By.XPath("//*[@id='MS_personalcarebodyareasid_" + recordId + "']/a[@id='" + recordId + "_Link']");
        By BodyAreas_OptionRemoveButton(string recordId) => By.XPath("//*[@id='MS_personalcarebodyareasid_" + recordId + "']/a[text()='Remove']");


        readonly By ClothesidLookupButton = By.XPath("//*[@id='CWLookupBtn_clothesid']");
        By Clothes_OptionLinkField(string recordId) => By.XPath("//*[@id='MS_clothesid_" + recordId + "']/a[@id='" + recordId + "_Link']");
        By Clothes_OptionRemoveButton(string recordId) => By.XPath("//*[@id='MS_clothesid_" + recordId + "']/a[text()='Remove']");

        readonly By Glassesclean_1 = By.XPath("//*[@id='CWField_glassesclean_1']");
        readonly By Glassesclean_0 = By.XPath("//*[@id='CWField_glassesclean_0']");
        readonly By Glassesbeingworn_1 = By.XPath("//*[@id='CWField_glassesbeingworn_1']");
        readonly By Glassesbeingworn_0 = By.XPath("//*[@id='CWField_glassesbeingworn_0']");
        readonly By Hearingaidsswitchedon_1 = By.XPath("//*[@id='CWField_hearingaidsswitchedon_1']");
        readonly By Hearingaidsswitchedon_0 = By.XPath("//*[@id='CWField_hearingaidsswitchedon_0']");
        readonly By Hearingaidsworking_1 = By.XPath("//*[@id='CWField_hearingaidsworking_1']");
        readonly By Hearingaidsworking_0 = By.XPath("//*[@id='CWField_hearingaidsworking_0']");
        readonly By Hearingaidscorrectposition_1 = By.XPath("//*[@id='CWField_hearingaidscorrectposition_1']");
        readonly By Hearingaidscorrectposition_0 = By.XPath("//*[@id='CWField_hearingaidscorrectposition_0']");

        readonly By OralcarerefdataidLookupButton = By.XPath("//*[@id='CWLookupBtn_oralcarerefdataid']");
        By OralCare_OptionLinkField(string recordId) => By.XPath("//*[@id='MS_oralcarerefdataid_" + recordId + "']/a[@id='" + recordId + "_Link']");
        By OralCare_OptionRemoveButton(string recordId) => By.XPath("//*[@id='MS_oralcarerefdataid_" + recordId + "']/a[text()='Remove']");

        By Other_OptionLinkField(string recordId) => By.XPath("//*[@id='MS_otherrefdataid_" + recordId + "']/a[@id='" + recordId + "_Link']");
        By Other_OptionRemoveButton(string recordId) => By.XPath("//*[@id='MS_otherrefdataid_" + recordId + "']/a[text()='Remove']");
        readonly By OtherrefdataidLookupButton = By.XPath("//*[@id='CWLookupBtn_otherrefdataid']");

        readonly By OtherText_Field = By.XPath("//*[@id='CWField_otherfreetext']");

        readonly By Skinconcernisany_1 = By.XPath("//*[@id='CWField_skinconcernisany_1']");
        readonly By Skinconcernisany_0 = By.XPath("//*[@id='CWField_skinconcernisany_0']");

        readonly By Skinconcernwhere_Field = By.XPath("//*[@id='CWField_skinconcernwhere']");

        By Skinconditions_OptionLinkField(string recordId) => By.XPath("//*[@id='MS_skinconditions_" + recordId + "']/a[@id='" + recordId + "_Link']");
        By Skinconditions_OptionRemoveButton(string recordId) => By.XPath("//*[@id='MS_skinconditions_" + recordId + "']/a[text()='Remove']");
        readonly By SkinconditionsLookupButton = By.XPath("//*[@id='CWLookupBtn_skinconditions']");

        readonly By Reviewrequiredbyseniorcolleague_1 = By.XPath("//*[@id='CWField_reviewrequiredbyseniorcolleague_1']");
        readonly By Reviewrequiredbyseniorcolleague_0 = By.XPath("//*[@id='CWField_reviewrequiredbyseniorcolleague_0']");

        readonly By Reviewdetails_Field = By.XPath("//*[@id='CWField_reviewdetails']");


        #endregion

        #region Additional Information

        By Location_OptionLinkField(string recordId) => By.XPath("//*[@id='MS_locationid_" + recordId + "']/a[@id='" + recordId + "_Link']");
        By Location_OptionRemoveButton(string recordId) => By.XPath("//*[@id='MS_locationid_" + recordId + "']/a[text()='Remove']");
        readonly By LocationidLookupButton = By.XPath("//*[@id='CWLookupBtn_locationid']");

        readonly By LocationIfOther = By.XPath("//*[@id='CWField_locationifother']");

        readonly By CarewellbeingidLink = By.XPath("//*[@id='CWField_carewellbeingid_Link']");
        readonly By CarewellbeingidClearButton = By.XPath("//*[@id='CWClearLookup_carewellbeingid']");
        readonly By CarewellbeingidLookupButton = By.XPath("//*[@id='CWLookupBtn_carewellbeingid']");

        readonly By Actiontaken = By.XPath("//*[@id='CWField_actiontaken']");

        readonly By Additionalnotes = By.XPath("//*[@id='CWField_additionalnotes']");

        By Equipment_OptionLinkField(string recordId) => By.XPath("//*[@id='MS_equipmentid_" + recordId + "']/a[@id='" + recordId + "_Link']");
        By Equipment_OptionRemoveButton(string recordId) => By.XPath("//*[@id='MS_equipmentid_" + recordId + "']/a[text()='Remove']");
        readonly By EquipmentidLookupButton = By.XPath("//*[@id='CWLookupBtn_equipmentid']");

        readonly By EquipmentIfOther = By.XPath("//*[@id='CWField_equipmentifother']");

        readonly By CareassistanceneededidLink = By.XPath("//*[@id='CWField_careassistanceneededid_Link']");
        readonly By CareassistanceneededidClearButton = By.XPath("//*[@id='CWClearLookup_careassistanceneededid']");
        readonly By CareassistanceneededidLookupButton = By.XPath("//*[@id='CWLookupBtn_careassistanceneededid']");

        readonly By AssistanceAmount = By.XPath("//*[@id='CWField_careassistancelevelid']");

        By StaffRequired_OptionLinkField(string recordId) => By.XPath("//*[@id='MS_staffrequired_" + recordId + "']/a[@id='" + recordId + "_Link']");
        By StaffRequired_OptionRemoveButton(string recordId) => By.XPath("//*[@id='MS_staffrequired_" + recordId + "']/a[text()='Remove']");
        readonly By StaffrequiredLookupButton = By.XPath("//*[@id='CWLookupBtn_staffrequired']");

        readonly By TotalTimeSpentWithClientMinutesFieldLabel = By.XPath("//*[@id = 'CWLabelHolder_timespentwithclient']/label[text() = 'Total Time Spent With Person (Minutes)']");
        readonly By TotalTimeSpentWithClientMinutesField = By.Id("CWField_timespentwithclient");
        readonly By TotalTimeSpentWithClientMinutesFieldError = By.XPath("//label[@for = 'CWField_timespentwithclient'][@class = 'formerror']/span");

        #endregion

        #region Care Note

        readonly By Carenote = By.XPath("//*[@id='CWField_carenote']");

        #endregion

        #region Care Needs

        By LinkedActivitiesOfDailyLiving_OptionLinkField(string recordId) => By.XPath("//*[@id='MS_linkedadlcategories_" + recordId + "']/a[@id='" + recordId + "_Link']");
        By LinkedActivitiesOfDailyLiving_OptionRemoveButton(string recordId) => By.XPath("//*[@id='MS_linkedadlcategories_" + recordId + "']/a[text()='Remove']");
        readonly By LinkedadlcategoriesLookupButton = By.XPath("//*[@id='CWLookupBtn_linkedadlcategories']");

        #endregion

        #region Handover

        readonly By Isincludeinnexthandover_1 = By.XPath("//*[@id='CWField_isincludeinnexthandover_1']");
        readonly By Isincludeinnexthandover_0 = By.XPath("//*[@id='CWField_isincludeinnexthandover_0']");
        readonly By Flagrecordforhandover_1 = By.XPath("//*[@id='CWField_flagrecordforhandover_1']");
        readonly By Flagrecordforhandover_0 = By.XPath("//*[@id='CWField_flagrecordforhandover_0']");

        #endregion

        #endregion


        public PersonDailyPersonalCareRecordPage WaitForPageToLoad()
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


        #region Options Toolbar

        public PersonDailyPersonalCareRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        #endregion

        #region General

        public PersonDailyPersonalCareRecordPage ClickPersonLink()
        {
            WaitForElementToBeClickable(PersonidLink);
            Click(PersonidLink);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidatePersonLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(PersonidLink);
            ValidateElementText(PersonidLink, ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickPersonLookupButton()
        {
            WaitForElementToBeClickable(PersonidLookupButton);
            Click(PersonidLookupButton);

            return this;
        }

        //verify preferences textare field is displayed or not displayed
        public PersonDailyPersonalCareRecordPage VerifyPreferencesTextAreaFieldIsDisplayed(bool ExpectedDisplayed)
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
        public PersonDailyPersonalCareRecordPage VerifyPreferencesTextAreaFieldIsDisabled(bool ExpectedDisabled)
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
        public PersonDailyPersonalCareRecordPage VerifyPreferencesTextAreaFieldText(string ExpectedText)
        {
            ScrollToElement(PreferencesTextareField);
            WaitForElementVisible(PreferencesTextareField);
            ValidateElementValue(PreferencesTextareField, ExpectedText);

            return this;
        }

        //verify preference textarea field attribute value
        public PersonDailyPersonalCareRecordPage VerifyPreferencesTextAreaFieldMaxLength(string ExpectedValue)
        {
            ScrollToElement(PreferencesTextareField);
            WaitForElementVisible(PreferencesTextareField);
            ValidateElementAttribute(PreferencesTextareField, "maxlength", ExpectedValue);

            return this;
        }

        public PersonDailyPersonalCareRecordPage SelectConsentGiven(string TextToSelect)
        {
            WaitForElementToBeClickable(Careconsentgivenid);
            SelectPicklistElementByText(Careconsentgivenid, TextToSelect);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateConsentGivenSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Careconsentgivenid, ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickResponsibleTeamLink()
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            Click(ResponsibleTeamLink);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            ValidateElementText(ResponsibleTeamLink, ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamLookupButton);
            Click(ResponsibleTeamLookupButton);

            return this;
        }

        public PersonDailyPersonalCareRecordPage SetDateOccurred(String dateoccured)
        {
            ScrollToElement(DateAndTimeOccurred_DateField);
            WaitForElementVisible(DateAndTimeOccurred_DateField);
            SendKeys(DateAndTimeOccurred_DateField, dateoccured);


            return this;
        }

        //click DateAndTimeOccurred_DatePicker
        public PersonDailyPersonalCareRecordPage ClickDateAndTimeOccurredDatePicker()
        {
            WaitForElement(DateAndTimeOccurred_DatePicker);
            ScrollToElement(DateAndTimeOccurred_DatePicker);
            Click(DateAndTimeOccurred_DatePicker);

            return this;
        }

        public PersonDailyPersonalCareRecordPage SetTimeOccurred(String timeoccured)
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
        public PersonDailyPersonalCareRecordPage ClickDateAndTimeOccurredTimePicker()
        {
            WaitForElement(DateAndTimeOccurred_TimePicker);
            ScrollToElement(DateAndTimeOccurred_TimePicker);
            Click(DateAndTimeOccurred_TimePicker);

            return this;
        }

        //verify dateandtimeoccurredfieldlabel and dateandtimeoccurredfield is displayed or not displayed
        public PersonDailyPersonalCareRecordPage VerifyDateAndTimeOccurredFieldsAreDisplayed(bool ExpectedDisplayed)
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

        public PersonDailyPersonalCareRecordPage SetDateAndTimeOccurred(string DateToInsert, string TimeToInsert)
        {
            WaitForElement(DateAndTimeOccurred_DateField);
            WaitForElement(DateAndTimeOccurred_TimeField);

            SendKeys(DateAndTimeOccurred_DateField, DateToInsert + OpenQA.Selenium.Keys.Tab);
            SendKeys(DateAndTimeOccurred_TimeField, TimeToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        //verify dateandtimeoccurred_datefield
        public PersonDailyPersonalCareRecordPage VerifyDateAndTimeOccurredDateFieldText(string ExpectedText)
        {
            ScrollToElement(DateAndTimeOccurred_DateField);
            WaitForElementVisible(DateAndTimeOccurred_DateField);
            ValidateElementValue(DateAndTimeOccurred_DateField, ExpectedText);

            return this;
        }

        //verify dateandtimeoccurred_timefield
        public PersonDailyPersonalCareRecordPage VerifyDateAndTimeOccurredTimeFieldText(string ExpectedText)
        {
            ScrollToElement(DateAndTimeOccurred_TimeField);
            WaitForElementVisible(DateAndTimeOccurred_TimeField);
            ValidateElementValue(DateAndTimeOccurred_TimeField, ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage SetReasonForAbsence(string TextToInsert)
        {
            WaitForElementVisible(reasonforabsence);
            SendKeys(reasonforabsence, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateReasonForAbsence(string ExpectedText)
        {
            ValidateElementValue(reasonforabsence, ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage SetReasonConsentDeclined(string TextToInsert)
        {
            WaitForElementVisible(reasonconsentdeclined);
            SendKeys(reasonconsentdeclined, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateReasonConsentDeclined(string ExpectedText)
        {
            ValidateElementValue(reasonconsentdeclined, ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage SetEncouragementGiven(string TextToInsert)
        {
            WaitForElementVisible(encouragementgiven);
            SendKeys(encouragementgiven, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateEncouragementGiven(string ExpectedText)
        {
            ValidateElementValue(encouragementgiven, ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickCareProvidedWithoutConsent_YesRadioButton()
        {
            WaitForElementToBeClickable(careprovidedwithoutconsent_1);
            Click(careprovidedwithoutconsent_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateCareProvidedWithoutConsent_YesRadioButtonChecked()
        {
            WaitForElement(careprovidedwithoutconsent_1);
            ValidateElementChecked(careprovidedwithoutconsent_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateCareProvidedWithoutConsent_YesRadioButtonNotChecked()
        {
            WaitForElement(careprovidedwithoutconsent_1);
            ValidateElementNotChecked(careprovidedwithoutconsent_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickCareProvidedWithoutConsent_NoRadioButton()
        {
            WaitForElementToBeClickable(careprovidedwithoutconsent_0);
            Click(careprovidedwithoutconsent_0);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateCareProvidedWithoutConsent_NoRadioButtonChecked()
        {
            WaitForElement(careprovidedwithoutconsent_0);
            ValidateElementChecked(careprovidedwithoutconsent_0);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateCareProvidedWithoutConsent_NoRadioButtonNotChecked()
        {
            WaitForElement(careprovidedwithoutconsent_0);
            ValidateElementNotChecked(careprovidedwithoutconsent_0);

            return this;
        }

        public PersonDailyPersonalCareRecordPage SelectNonConsentDetail(string TextToSelect)
        {
            WaitForElementVisible(nonconsentDetail);
            SelectPicklistElementByText(nonconsentDetail, TextToSelect);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateSelectedNonConsentDetail(string ExpectedText)
        {
            ValidatePicklistSelectedText(nonconsentDetail, ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage SetDeferredToDate(string TextToInsert)
        {
            WaitForElementVisible(deferredToDate);
            SendKeys(deferredToDate, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateDeferredToDate(string ExpectedText)
        {
            ValidateElementValue(deferredToDate, ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateDeferredToDateErrorLabel(string ExpectedText)
        {
            WaitForElementVisible(deferredToDate_ErrorLabel);
            ValidateElementText(deferredToDate_ErrorLabel, ExpectedText);

            return this;
        }

        //Validate deferred to date field is displayed or not displayed
        public PersonDailyPersonalCareRecordPage ValidateDeferredToDateFieldVisible(bool ExpectVisible)
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
        public PersonDailyPersonalCareRecordPage ValidateDeferredToDateFieldLabel(string ExpectedText)
        {
            WaitForElement(deferredToDateFieldLabel);
            ScrollToElement(deferredToDateFieldLabel);
            ValidateElementByTitle(deferredToDateFieldLabel, ExpectedText);

            return this;
        }

        //click deferred to date datepicker
        public PersonDailyPersonalCareRecordPage ClickDeferredToDate_DatePicker()
        {
            WaitForElementToBeClickable(deferredToDate_DatePicker);
            Click(deferredToDate_DatePicker);

            return this;
        }

        //verify deferred to date datepicker is displayed or not displayed
        public PersonDailyPersonalCareRecordPage VerifyDeferredToDate_DatePickerIsDisplayed(bool ExpectedDisplayed)
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

        public PersonDailyPersonalCareRecordPage SelectDeferredToTimeOrShift(string TextToSelect)
        {
            WaitForElementVisible(deferredToTimeOrShift);
            SelectPicklistElementByText(deferredToTimeOrShift, TextToSelect);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateSelectedDeferredToTimeOrShift(string ExpectedText)
        {
            ValidatePicklistSelectedText(deferredToTimeOrShift, ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateDeferredToTimeOrShiftErrorLabel(string ExpectedText)
        {
            WaitForElementVisible(deferredToTimeOrShift_ErrorLabel);
            ValidateElementText(deferredToTimeOrShift_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage SetDeferredToTime(string TextToInsert)
        {
            WaitForElementVisible(deferredToTime);
            SendKeys(deferredToTime, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateDeferredToTime(string ExpectedText)
        {
            ValidateElementValue(deferredToTime, ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateDeferredToTimeErrorLabel(string ExpectedText)
        {
            WaitForElementVisible(deferredToTime_ErrorLabel);
            ValidateElementText(deferredToTime_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickDeferredToShiftLookupButton()
        {
            WaitForElementToBeClickable(deferredToShift_LookupButton);
            Click(deferredToShift_LookupButton);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateDeferredToShiftLinkText(string ExpectedText)
        {
            ValidateElementText(deferredToShift_LinkField, ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickDeferredToShiftClearButton()
        {
            Click(deferredToShift_ClearButton);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateDeferredToShiftErrorLabel(string ExpectedText)
        {
            WaitForElementVisible(deferredToShift_ErrorLabel);
            ValidateElementText(deferredToShift_ErrorLabel, ExpectedText);

            return this;
        }


        #endregion

        #region Details

        public PersonDailyPersonalCareRecordPage ClickWashOptionLinkField(string RecordId)
        {
            WaitForElementToBeClickable(wash_OptionLinkField(RecordId));
            Click(wash_OptionLinkField(RecordId));

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickWashOptionLinkField(Guid RecordId)
        {
            return ClickWashOptionLinkField(RecordId.ToString());
        }

        public PersonDailyPersonalCareRecordPage ClickWashOptionRemoveButton(string RecordId)
        {
            WaitForElementToBeClickable(wash_OptionRemoveButton(RecordId));
            Click(wash_OptionRemoveButton(RecordId));

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickWashOptionRemoveButton(Guid RecordId)
        {
            return ClickWashOptionRemoveButton(RecordId.ToString());
        }

        public PersonDailyPersonalCareRecordPage ValidateWashOptionLinkFieldText(string RecordId, string ExpectedText)
        {
            WaitForElementToBeClickable(wash_OptionLinkField(RecordId));
            ValidateElementText(wash_OptionLinkField(RecordId), ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateWashOptionLinkFieldText(Guid RecordId, string ExpectedText)
        {
            return ValidateWashOptionLinkFieldText(RecordId.ToString(), ExpectedText);
        }

        public PersonDailyPersonalCareRecordPage ClickWashLookupButton()
        {
            WaitForElementToBeClickable(WashidLookupButton);
            Click(WashidLookupButton);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickBodyAreasOptionLinkField(string RecordId)
        {
            WaitForElementToBeClickable(BodyAreas_OptionLinkField(RecordId));
            Click(BodyAreas_OptionLinkField(RecordId));

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickBodyAreasOptionLinkField(Guid RecordId)
        {
            return ClickBodyAreasOptionLinkField(RecordId.ToString());
        }

        public PersonDailyPersonalCareRecordPage ClickBodyAreasOptionRemoveButton(string RecordId)
        {
            WaitForElementToBeClickable(BodyAreas_OptionRemoveButton(RecordId));
            Click(BodyAreas_OptionRemoveButton(RecordId));

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickBodyAreasOptionRemoveButton(Guid RecordId)
        {
            return ClickBodyAreasOptionRemoveButton(RecordId.ToString());
        }

        public PersonDailyPersonalCareRecordPage ValidateBodyAreasOptionLinkFieldText(string RecordId, string ExpectedText)
        {
            WaitForElementToBeClickable(BodyAreas_OptionLinkField(RecordId));
            ValidateElementText(BodyAreas_OptionLinkField(RecordId), ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateBodyAreasOptionLinkFieldText(Guid RecordId, string ExpectedText)
        {
            return ValidateBodyAreasOptionLinkFieldText(RecordId.ToString(), ExpectedText);
        }

        public PersonDailyPersonalCareRecordPage ClickBodyAreasLookupButton()
        {
            WaitForElementToBeClickable(BodyAreasLookupButton);
            Click(BodyAreasLookupButton);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateBodyAreasLookupButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(BodyAreasLookupButton);
            else
                WaitForElementNotVisible(BodyAreasLookupButton, 3);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickClothesOptionLinkField(string RecordId)
        {
            WaitForElementToBeClickable(Clothes_OptionLinkField(RecordId));
            Click(Clothes_OptionLinkField(RecordId));

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickClothesOptionLinkField(Guid RecordId)
        {
            return ClickClothesOptionLinkField(RecordId.ToString());
        }

        public PersonDailyPersonalCareRecordPage ClickClothesOptionRemoveButton(string RecordId)
        {
            WaitForElementToBeClickable(Clothes_OptionRemoveButton(RecordId));
            Click(Clothes_OptionRemoveButton(RecordId));

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickClothesOptionRemoveButton(Guid RecordId)
        {
            return ClickClothesOptionRemoveButton(RecordId.ToString());
        }

        public PersonDailyPersonalCareRecordPage ValidateClothesOptionLinkFieldText(string RecordId, string ExpectedText)
        {
            WaitForElementToBeClickable(Clothes_OptionLinkField(RecordId));
            ValidateElementText(Clothes_OptionLinkField(RecordId), ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateClothesOptionLinkFieldText(Guid RecordId, string ExpectedText)
        {
            return ValidateClothesOptionLinkFieldText(RecordId.ToString(), ExpectedText);
        }

        public PersonDailyPersonalCareRecordPage ClickClothesLookupButton()
        {
            WaitForElementToBeClickable(ClothesidLookupButton);
            Click(ClothesidLookupButton);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickAreTheirGlassesClean_YesRadioButton()
        {
            WaitForElementToBeClickable(Glassesclean_1);
            Click(Glassesclean_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateAreTheirGlassesClean_YesRadioButtonChecked()
        {
            WaitForElement(Glassesclean_1);
            ValidateElementChecked(Glassesclean_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateAreTheirGlassesClean_YesRadioButtonNotChecked()
        {
            WaitForElement(Glassesclean_1);
            ValidateElementNotChecked(Glassesclean_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickAreTheirGlassesClean_NoRadioButton()
        {
            WaitForElementToBeClickable(Glassesclean_0);
            Click(Glassesclean_0);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateAreTheirGlassesClean_NoRadioButtonChecked()
        {
            WaitForElement(Glassesclean_0);
            ValidateElementChecked(Glassesclean_0);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateAreTheirGlassesClean_NoRadioButtonNotChecked()
        {
            WaitForElement(Glassesclean_0);
            ValidateElementNotChecked(Glassesclean_0);
            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickAreTheirGlassesBeingWorn_YesRadioButton()
        {
            WaitForElementToBeClickable(Glassesbeingworn_1);
            Click(Glassesbeingworn_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateAreTheirGlassesBeingWorn_YesRadioButtonChecked()
        {
            WaitForElement(Glassesbeingworn_1);
            ValidateElementChecked(Glassesbeingworn_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateAreTheirGlassesBeingWorn_YesRadioButtonNotChecked()
        {
            WaitForElement(Glassesbeingworn_1);
            ValidateElementNotChecked(Glassesbeingworn_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickAreTheirGlassesBeingWorn_NoRadioButton()
        {
            WaitForElementToBeClickable(Glassesbeingworn_0);
            Click(Glassesbeingworn_0);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateAreTheirGlassesBeingWorn_NoRadioButtonChecked()
        {
            WaitForElement(Glassesbeingworn_0);
            ValidateElementChecked(Glassesbeingworn_0);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateAreTheirGlassesBeingWorn_NoRadioButtonNotChecked()
        {
            WaitForElement(Glassesbeingworn_0);
            ValidateElementNotChecked(Glassesbeingworn_0);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickAreTheirHearingAidsSwitchedOn_YesRadioButton()
        {
            WaitForElementToBeClickable(Hearingaidsswitchedon_1);
            Click(Hearingaidsswitchedon_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateAreTheirHearingAidsSwitchedOn_YesRadioButtonChecked()
        {
            WaitForElement(Hearingaidsswitchedon_1);
            ValidateElementChecked(Hearingaidsswitchedon_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateAreTheirHearingAidsSwitchedOn_YesRadioButtonNotChecked()
        {
            WaitForElement(Hearingaidsswitchedon_1);
            ValidateElementNotChecked(Hearingaidsswitchedon_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickAreTheirHearingAidsSwitchedOn_NoRadioButton()
        {
            WaitForElementToBeClickable(Hearingaidsswitchedon_0);
            Click(Hearingaidsswitchedon_0);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateAreTheirHearingAidsSwitchedOn_NoRadioButtonChecked()
        {
            WaitForElement(Hearingaidsswitchedon_0);
            ValidateElementChecked(Hearingaidsswitchedon_0);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateAreTheirHearingAidsSwitchedOn_NoRadioButtonNotChecked()
        {
            WaitForElement(Hearingaidsswitchedon_0);
            ValidateElementNotChecked(Hearingaidsswitchedon_0);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickAreTheirHearingAidsWorking_YesRadioButton()
        {
            WaitForElementToBeClickable(Hearingaidsworking_1);
            Click(Hearingaidsworking_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateAreTheirHearingAidsWorking_YesRadioButtonChecked()
        {
            WaitForElement(Hearingaidsworking_1);
            ValidateElementChecked(Hearingaidsworking_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateAreTheirHearingAidsWorking_YesRadioButtonNotChecked()
        {
            WaitForElement(Hearingaidsworking_1);
            ValidateElementNotChecked(Hearingaidsworking_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickAreTheirHearingAidsWorking_NoRadioButton()
        {
            WaitForElementToBeClickable(Hearingaidsworking_0);
            Click(Hearingaidsworking_0);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateAreTheirHearingAidsWorking_NoRadioButtonChecked()
        {
            WaitForElement(Hearingaidsworking_0);
            ValidateElementChecked(Hearingaidsworking_0);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateAreTheirHearingAidsWorking_NoRadioButtonNotChecked()
        {
            WaitForElement(Hearingaidsworking_0);
            ValidateElementNotChecked(Hearingaidsworking_0);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickAreTheirHearingAidsInTheCorrectPosition_YesRadioButton()
        {
            WaitForElementToBeClickable(Hearingaidscorrectposition_1);
            Click(Hearingaidscorrectposition_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateAreTheirHearingAidsInTheCorrectPosition_YesRadioButtonChecked()
        {
            WaitForElement(Hearingaidscorrectposition_1);
            ValidateElementChecked(Hearingaidscorrectposition_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateAreTheirHearingAidsInTheCorrectPosition_YesRadioButtonNotChecked()
        {
            WaitForElement(Hearingaidscorrectposition_1);
            ValidateElementNotChecked(Hearingaidscorrectposition_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickAreTheirHearingAidsInTheCorrectPosition_NoRadioButton()
        {
            WaitForElementToBeClickable(Hearingaidscorrectposition_0);
            Click(Hearingaidscorrectposition_0);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateAreTheirHearingAidsInTheCorrectPosition_NoRadioButtonChecked()
        {
            WaitForElement(Hearingaidscorrectposition_0);
            ValidateElementChecked(Hearingaidscorrectposition_0);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateAreTheirHearingAidsInTheCorrectPosition_NoRadioButtonNotChecked()
        {
            WaitForElement(Hearingaidscorrectposition_0);
            ValidateElementNotChecked(Hearingaidscorrectposition_0);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickOralCareOptionLinkField(string RecordId)
        {
            WaitForElementToBeClickable(OralCare_OptionLinkField(RecordId));
            Click(OralCare_OptionLinkField(RecordId));

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickOralCareOptionLinkField(Guid RecordId)
        {
            return ClickOralCareOptionLinkField(RecordId.ToString());
        }

        public PersonDailyPersonalCareRecordPage ClickOralCareOptionRemoveButton(string RecordId)
        {
            WaitForElementToBeClickable(OralCare_OptionRemoveButton(RecordId));
            Click(OralCare_OptionRemoveButton(RecordId));

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickOralCareOptionRemoveButton(Guid RecordId)
        {
            return ClickOralCareOptionRemoveButton(RecordId.ToString());
        }

        public PersonDailyPersonalCareRecordPage ValidateOralCareOptionLinkFieldText(string RecordId, string ExpectedText)
        {
            WaitForElementToBeClickable(OralCare_OptionLinkField(RecordId));
            ValidateElementText(OralCare_OptionLinkField(RecordId), ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateOralCareOptionLinkFieldText(Guid RecordId, string ExpectedText)
        {
            return ValidateOralCareOptionLinkFieldText(RecordId.ToString(), ExpectedText);
        }

        public PersonDailyPersonalCareRecordPage ClickOralCareLookupButton()
        {
            WaitForElementToBeClickable(OralcarerefdataidLookupButton);
            Click(OralcarerefdataidLookupButton);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickOtherOptionLinkField(string RecordId)
        {
            WaitForElementToBeClickable(Other_OptionLinkField(RecordId));
            Click(Other_OptionLinkField(RecordId));

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickOtherOptionLinkField(Guid RecordId)
        {
            return ClickOtherOptionLinkField(RecordId.ToString());
        }

        public PersonDailyPersonalCareRecordPage ClickOtherOptionRemoveButton(string RecordId)
        {
            WaitForElementToBeClickable(Other_OptionRemoveButton(RecordId));
            Click(Other_OptionRemoveButton(RecordId));

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickOtherOptionRemoveButton(Guid RecordId)
        {
            return ClickOtherOptionRemoveButton(RecordId.ToString());
        }

        public PersonDailyPersonalCareRecordPage ValidateOtherOptionLinkFieldText(string RecordId, string ExpectedText)
        {
            WaitForElementToBeClickable(Other_OptionLinkField(RecordId));
            ValidateElementText(Other_OptionLinkField(RecordId), ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateOtherOptionLinkFieldText(Guid RecordId, string ExpectedText)
        {
            return ValidateOtherOptionLinkFieldText(RecordId.ToString(), ExpectedText);
        }

        public PersonDailyPersonalCareRecordPage ClickOtherLookupButton()
        {
            WaitForElementToBeClickable(OtherrefdataidLookupButton);
            Click(OtherrefdataidLookupButton);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateOtherText(string ExpectedText)
        {
            WaitForElement(OtherText_Field);
            ValidateElementText(OtherText_Field, ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage InsertOtherText(string TextToInsert)
        {
            WaitForElement(OtherText_Field);
            SendKeys(OtherText_Field, TextToInsert + OpenQA.Selenium.Keys.Tab);
            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateOtherTextVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(OtherText_Field);
            else
                WaitForElementNotVisible(OtherText_Field, 3);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickAreThereAnyNewConcernsWithThePersonSkin_YesRadioButton()
        {
            WaitForElementToBeClickable(Skinconcernisany_1);
            Click(Skinconcernisany_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateAreThereAnyNewConcernsWithThePersonSkin_YesRadioButtonChecked()
        {
            WaitForElement(Skinconcernisany_1);
            ValidateElementChecked(Skinconcernisany_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateAreThereAnyNewConcernsWithThePersonSkin_YesRadioButtonNotChecked()
        {
            WaitForElement(Skinconcernisany_1);
            ValidateElementNotChecked(Skinconcernisany_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickAreThereAnyNewConcernsWithThePersonSkin_NoRadioButton()
        {
            WaitForElementToBeClickable(Skinconcernisany_0);
            Click(Skinconcernisany_0);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateAreThereAnyNewConcernsWithThePersonSkin_NoRadioButtonChecked()
        {
            WaitForElement(Skinconcernisany_0);
            ValidateElementChecked(Skinconcernisany_0);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateAreThereAnyNewConcernsWithThePersonSkin_NoRadioButtonNotChecked()
        {
            WaitForElement(Skinconcernisany_0);
            ValidateElementNotChecked(Skinconcernisany_0);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateWhereOnTheBody(string ExpectedText)
        {
            WaitForElement(Skinconcernwhere_Field);
            ValidateElementText(Skinconcernwhere_Field, ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage InsertWhereOnTheBody(string TextToInsert)
        {
            WaitForElement(Skinconcernwhere_Field);
            SendKeys(Skinconcernwhere_Field, TextToInsert + OpenQA.Selenium.Keys.Tab);
            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickDescribeSkinConditionOptionLinkField(string RecordId)
        {
            WaitForElementToBeClickable(Skinconditions_OptionLinkField(RecordId));
            Click(Skinconditions_OptionLinkField(RecordId));

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickDescribeSkinConditionOptionLinkField(Guid RecordId)
        {
            return ClickDescribeSkinConditionOptionLinkField(RecordId.ToString());
        }

        public PersonDailyPersonalCareRecordPage ClickDescribeSkinConditionOptionRemoveButton(string RecordId)
        {
            WaitForElementToBeClickable(Skinconditions_OptionRemoveButton(RecordId));
            Click(Skinconditions_OptionRemoveButton(RecordId));

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickDescribeSkinConditionOptionRemoveButton(Guid RecordId)
        {
            return ClickDescribeSkinConditionOptionRemoveButton(RecordId.ToString());
        }

        public PersonDailyPersonalCareRecordPage ValidateDescribeSkinConditionOptionLinkFieldText(string RecordId, string ExpectedText)
        {
            WaitForElementToBeClickable(Skinconditions_OptionLinkField(RecordId));
            ValidateElementText(Skinconditions_OptionLinkField(RecordId), ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateDescribeSkinConditionOptionLinkFieldText(Guid RecordId, string ExpectedText)
        {
            return ValidateDescribeSkinConditionOptionLinkFieldText(RecordId.ToString(), ExpectedText);
        }

        public PersonDailyPersonalCareRecordPage ClickDescribeSkinConditionLookupButton()
        {
            WaitForElementToBeClickable(SkinconditionsLookupButton);
            Click(SkinconditionsLookupButton);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickReviewrequiredbyseniorcolleague_YesRadioButton()
        {
            WaitForElementToBeClickable(Reviewrequiredbyseniorcolleague_1);
            Click(Reviewrequiredbyseniorcolleague_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateReviewrequiredbyseniorcolleague_YesRadioButtonChecked()
        {
            WaitForElement(Reviewrequiredbyseniorcolleague_1);
            ValidateElementChecked(Reviewrequiredbyseniorcolleague_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateReviewrequiredbyseniorcolleague_YesRadioButtonNotChecked()
        {
            WaitForElement(Reviewrequiredbyseniorcolleague_1);
            ValidateElementNotChecked(Reviewrequiredbyseniorcolleague_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickReviewrequiredbyseniorcolleague_NoRadioButton()
        {
            WaitForElementToBeClickable(Reviewrequiredbyseniorcolleague_0);
            Click(Reviewrequiredbyseniorcolleague_0);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateReviewrequiredbyseniorcolleague_NoRadioButtonChecked()
        {
            WaitForElement(Reviewrequiredbyseniorcolleague_0);
            ValidateElementChecked(Reviewrequiredbyseniorcolleague_0);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateReviewrequiredbyseniorcolleague_NoRadioButtonNotChecked()
        {
            WaitForElement(Reviewrequiredbyseniorcolleague_0);
            ValidateElementNotChecked(Reviewrequiredbyseniorcolleague_0);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateReviewDetails(string ExpectedText)
        {
            WaitForElement(Reviewdetails_Field);
            ValidateElementValue(Reviewdetails_Field, ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage InsertReviewDetails(string TextToInsert)
        {
            WaitForElement(Reviewdetails_Field);
            SendKeys(Reviewdetails_Field, TextToInsert + OpenQA.Selenium.Keys.Tab);
            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateReviewDetailsVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Reviewdetails_Field);
            else
                WaitForElementNotVisible(Reviewdetails_Field, 3);

            return this;
        }

        #endregion

        #region Additional Information

        public PersonDailyPersonalCareRecordPage ClickLocationOptionLinkField(string RecordId)
        {
            WaitForElementToBeClickable(Location_OptionLinkField(RecordId));
            Click(Location_OptionLinkField(RecordId));

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickLocationOptionLinkField(Guid RecordId)
        {
            return ClickLocationOptionLinkField(RecordId.ToString());
        }

        public PersonDailyPersonalCareRecordPage ClickLocationOptionRemoveButton(string RecordId)
        {
            WaitForElementToBeClickable(Location_OptionRemoveButton(RecordId));
            Click(Location_OptionRemoveButton(RecordId));

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickLocationOptionRemoveButton(Guid RecordId)
        {
            return ClickLocationOptionRemoveButton(RecordId.ToString());
        }

        public PersonDailyPersonalCareRecordPage ValidateLocationOptionLinkFieldText(string RecordId, string ExpectedText)
        {
            WaitForElementToBeClickable(Location_OptionLinkField(RecordId));
            ValidateElementText(Location_OptionLinkField(RecordId), ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateLocationOptionLinkFieldText(Guid RecordId, string ExpectedText)
        {
            return ValidateLocationOptionLinkFieldText(RecordId.ToString(), ExpectedText);
        }

        public PersonDailyPersonalCareRecordPage ClickLocationLookupButton()
        {
            WaitForElementToBeClickable(LocationidLookupButton);
            Click(LocationidLookupButton);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateLocationIfOtherText(string ExpectedText)
        {
            ValidateElementText(LocationIfOther, ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage InsertTextOnLocationIfOther(string TextToInsert)
        {
            WaitForElementToBeClickable(LocationIfOther);
            SendKeys(LocationIfOther, TextToInsert);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateLocationIfOtherVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(LocationIfOther);
            else
                WaitForElementNotVisible(LocationIfOther, 3);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickWellbeingLink()
        {
            WaitForElementToBeClickable(CarewellbeingidLink);
            Click(CarewellbeingidLink);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateWellbeingLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CarewellbeingidLink);
            ValidateElementText(CarewellbeingidLink, ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickWellbeingClearButton()
        {
            WaitForElementToBeClickable(CarewellbeingidClearButton);
            Click(CarewellbeingidClearButton);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickWellbeingLookupButton()
        {
            WaitForElementToBeClickable(CarewellbeingidLookupButton);
            Click(CarewellbeingidLookupButton);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateActionTakenText(string ExpectedText)
        {
            ValidateElementText(Actiontaken, ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage InsertTextOnActionTaken(string TextToInsert)
        {
            WaitForElementToBeClickable(Actiontaken);
            SendKeys(Actiontaken, TextToInsert);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateActionTakenVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Actiontaken);
            else
                WaitForElementNotVisible(Actiontaken, 3);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateAdditionalNotesText(string ExpectedText)
        {
            ValidateElementText(Additionalnotes, ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage InsertTextOnAdditionalNotes(string TextToInsert)
        {
            WaitForElementToBeClickable(Additionalnotes);
            SendKeys(Additionalnotes, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickEquipmentOptionLinkField(string RecordId)
        {
            WaitForElementToBeClickable(Equipment_OptionLinkField(RecordId));
            Click(Equipment_OptionLinkField(RecordId));

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickEquipmentOptionLinkField(Guid RecordId)
        {
            return ClickEquipmentOptionLinkField(RecordId.ToString());
        }

        public PersonDailyPersonalCareRecordPage ClickEquipmentOptionRemoveButton(string RecordId)
        {
            WaitForElementToBeClickable(Equipment_OptionRemoveButton(RecordId));
            Click(Equipment_OptionRemoveButton(RecordId));

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickEquipmentOptionRemoveButton(Guid RecordId)
        {
            return ClickEquipmentOptionRemoveButton(RecordId.ToString());
        }

        public PersonDailyPersonalCareRecordPage ValidateEquipmentOptionLinkFieldText(string RecordId, string ExpectedText)
        {
            WaitForElementToBeClickable(Equipment_OptionLinkField(RecordId));
            ValidateElementText(Equipment_OptionLinkField(RecordId), ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateEquipmentOptionLinkFieldText(Guid RecordId, string ExpectedText)
        {
            return ValidateEquipmentOptionLinkFieldText(RecordId.ToString(), ExpectedText);
        }

        public PersonDailyPersonalCareRecordPage ClickEquipmentLookupButton()
        {
            WaitForElementToBeClickable(EquipmentidLookupButton);
            Click(EquipmentidLookupButton);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateEquipmentIfOtherText(string ExpectedText)
        {
            ValidateElementText(EquipmentIfOther, ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage InsertTextOnEquipmentIfOther(string TextToInsert)
        {
            WaitForElementToBeClickable(EquipmentIfOther);
            SendKeys(EquipmentIfOther, TextToInsert);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateEquipmentIfOtherVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(EquipmentIfOther);
            else
                WaitForElementNotVisible(EquipmentIfOther, 3);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickAssistanceNeededLink()
        {
            WaitForElementToBeClickable(CareassistanceneededidLink);
            Click(CareassistanceneededidLink);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateAssistanceNeededLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CareassistanceneededidLink);
            ValidateElementText(CareassistanceneededidLink, ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickAssistanceNeededClearButton()
        {
            WaitForElementToBeClickable(CareassistanceneededidClearButton);
            Click(CareassistanceneededidClearButton);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickAssistanceNeededLookupButton()
        {
            WaitForElementToBeClickable(CareassistanceneededidLookupButton);
            Click(CareassistanceneededidLookupButton);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateAssistanceAmountText(string ExpectedText)
        {
            ValidatePicklistSelectedText(AssistanceAmount, ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage SelectAssistanceAmount(string TextToSelect)
        {
            WaitForElementToBeClickable(AssistanceAmount);
            SelectPicklistElementByText(AssistanceAmount, TextToSelect);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateAssistanceAmountVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(AssistanceAmount);
            else
                WaitForElementNotVisible(AssistanceAmount, 3);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickStaffRequiredOptionLinkField(string RecordId)
        {
            WaitForElementToBeClickable(StaffRequired_OptionLinkField(RecordId));
            Click(StaffRequired_OptionLinkField(RecordId));

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickStaffRequiredOptionLinkField(Guid RecordId)
        {
            return ClickStaffRequiredOptionLinkField(RecordId.ToString());
        }

        public PersonDailyPersonalCareRecordPage ClickStaffRequiredOptionRemoveButton(string RecordId)
        {
            WaitForElementToBeClickable(StaffRequired_OptionRemoveButton(RecordId));
            Click(StaffRequired_OptionRemoveButton(RecordId));

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickStaffRequiredOptionRemoveButton(Guid RecordId)
        {
            return ClickStaffRequiredOptionRemoveButton(RecordId.ToString());
        }

        public PersonDailyPersonalCareRecordPage ValidateStaffRequiredOptionLinkFieldText(string RecordId, string ExpectedText)
        {
            WaitForElementToBeClickable(StaffRequired_OptionLinkField(RecordId));
            ValidateElementText(StaffRequired_OptionLinkField(RecordId), ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateStaffRequiredOptionLinkFieldText(Guid RecordId, string ExpectedText)
        {
            return ValidateStaffRequiredOptionLinkFieldText(RecordId.ToString(), ExpectedText);
        }

        public PersonDailyPersonalCareRecordPage ClickStaffRequiredLookupButton()
        {
            WaitForElementToBeClickable(StaffrequiredLookupButton);
            Click(StaffrequiredLookupButton);

            return this;
        }


        public PersonDailyPersonalCareRecordPage InsertTotalTimeSpentWithPersonMinutes(String TextToInsert)
        {
            ScrollToElement(TotalTimeSpentWithClientMinutesField);
            WaitForElementVisible(TotalTimeSpentWithClientMinutesField);
            SendKeys(TotalTimeSpentWithClientMinutesField, TextToInsert + OpenQA.Selenium.Keys.Tab);


            return this;
        }

        //verify totaltimespentwithclientminutesfield is present or not
        public PersonDailyPersonalCareRecordPage VerifyTotalTimeSpentWithPersonMinutesFieldIsDisplayed(bool ExpectedPresent)
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
        public PersonDailyPersonalCareRecordPage VerifyTotalTimeSpentWithPersonMinutesFieldText(string ExpectedText)
        {
            ScrollToElement(TotalTimeSpentWithClientMinutesField);
            WaitForElementVisible(TotalTimeSpentWithClientMinutesField);
            ValidateElementValue(TotalTimeSpentWithClientMinutesField, ExpectedText);

            return this;
        }

        //verify totaltimespentwithclientminutesfield error
        public PersonDailyPersonalCareRecordPage VerifyTotalTimeSpentWithPersonMinutesFieldErrorText(string ExpectedText)
        {
            WaitForElement(TotalTimeSpentWithClientMinutesFieldError);
            ValidateElementByTitle(TotalTimeSpentWithClientMinutesFieldError, ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage VerifyTotalTimeSpentWithPersonMinutesFieldErrorVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(TotalTimeSpentWithClientMinutesFieldError);
            else
                WaitForElementNotVisible(TotalTimeSpentWithClientMinutesFieldError, 3);

            return this;
        }

        #endregion

        #region Care Note

        public PersonDailyPersonalCareRecordPage ValidateCareNoteText(string ExpectedText)
        {
            var fieldText = GetElementValueByJavascript("CWField_carenote");
            Assert.AreEqual(ExpectedText, fieldText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage InsertTextOnCareNote(string TextToInsert)
        {
            WaitForElementToBeClickable(Carenote);
            SendKeys(Carenote, TextToInsert);

            return this;
        }

        #endregion

        #region Care Needs

        public PersonDailyPersonalCareRecordPage ClickLinkedActivitiesOfDailyLivingOptionLinkField(string RecordId)
        {
            WaitForElementToBeClickable(LinkedActivitiesOfDailyLiving_OptionLinkField(RecordId));
            Click(LinkedActivitiesOfDailyLiving_OptionLinkField(RecordId));

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickLinkedActivitiesOfDailyLivingOptionLinkField(Guid RecordId)
        {
            return ClickLinkedActivitiesOfDailyLivingOptionLinkField(RecordId.ToString());
        }

        public PersonDailyPersonalCareRecordPage ClickLinkedActivitiesOfDailyLivingOptionRemoveButton(string RecordId)
        {
            WaitForElementToBeClickable(LinkedActivitiesOfDailyLiving_OptionRemoveButton(RecordId));
            Click(LinkedActivitiesOfDailyLiving_OptionRemoveButton(RecordId));

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickLinkedActivitiesOfDailyLivingOptionRemoveButton(Guid RecordId)
        {
            return ClickLinkedActivitiesOfDailyLivingOptionRemoveButton(RecordId.ToString());
        }

        public PersonDailyPersonalCareRecordPage ValidateLinkedActivitiesOfDailyLivingOptionLinkFieldText(string RecordId, string ExpectedText)
        {
            WaitForElementToBeClickable(LinkedActivitiesOfDailyLiving_OptionLinkField(RecordId));
            ValidateElementText(LinkedActivitiesOfDailyLiving_OptionLinkField(RecordId), ExpectedText);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateLinkedActivitiesOfDailyLivingOptionLinkFieldText(Guid RecordId, string ExpectedText)
        {
            return ValidateLinkedActivitiesOfDailyLivingOptionLinkFieldText(RecordId.ToString(), ExpectedText);
        }

        public PersonDailyPersonalCareRecordPage ClickLinkedActivitiesOfDailyLivingLookupButton()
        {
            WaitForElementToBeClickable(LinkedadlcategoriesLookupButton);
            Click(LinkedadlcategoriesLookupButton);

            return this;
        }

        #endregion

        #region Handover

        public PersonDailyPersonalCareRecordPage ClickIncludeInNextHandover_YesRadioButton()
        {
            WaitForElementToBeClickable(Isincludeinnexthandover_1);
            Click(Isincludeinnexthandover_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateIncludeInNextHandover_YesRadioButtonChecked()
        {
            WaitForElement(Isincludeinnexthandover_1);
            ValidateElementChecked(Isincludeinnexthandover_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateIncludeInNextHandover_YesRadioButtonNotChecked()
        {
            WaitForElement(Isincludeinnexthandover_1);
            ValidateElementNotChecked(Isincludeinnexthandover_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickIncludeInNextHandover_NoRadioButton()
        {
            WaitForElementToBeClickable(Isincludeinnexthandover_0);
            Click(Isincludeinnexthandover_0);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateIncludeInNextHandover_NoRadioButtonChecked()
        {
            WaitForElement(Isincludeinnexthandover_0);
            ValidateElementChecked(Isincludeinnexthandover_0);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateIncludeInNextHandover_NoRadioButtonNotChecked()
        {
            WaitForElement(Isincludeinnexthandover_0);
            ValidateElementNotChecked(Isincludeinnexthandover_0);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickFlagRecordForHandover_YesRadioButton()
        {
            WaitForElementToBeClickable(Flagrecordforhandover_1);
            Click(Flagrecordforhandover_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateFlagRecordForHandover_YesRadioButtonChecked()
        {
            WaitForElement(Flagrecordforhandover_1);
            ValidateElementChecked(Flagrecordforhandover_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateFlagRecordForHandover_YesRadioButtonNotChecked()
        {
            WaitForElement(Flagrecordforhandover_1);
            ValidateElementNotChecked(Flagrecordforhandover_1);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ClickFlagRecordForHandover_NoRadioButton()
        {
            WaitForElementToBeClickable(Flagrecordforhandover_0);
            Click(Flagrecordforhandover_0);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateFlagRecordForHandover_NoRadioButtonChecked()
        {
            WaitForElement(Flagrecordforhandover_0);
            ValidateElementChecked(Flagrecordforhandover_0);

            return this;
        }

        public PersonDailyPersonalCareRecordPage ValidateFlagRecordForHandover_NoRadioButtonNotChecked()
        {
            WaitForElement(Flagrecordforhandover_0);
            ValidateElementNotChecked(Flagrecordforhandover_0);

            return this;
        }

        #endregion
    }
}

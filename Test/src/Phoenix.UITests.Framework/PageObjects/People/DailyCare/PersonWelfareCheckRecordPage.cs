using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Web.UI;
using System.Windows.Forms;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonWelfareCheckRecordPage : CommonMethods
    {
        public PersonWelfareCheckRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        #region Locators

        #region Options Toolbar

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cppersondaynightcheck&')]");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Welfare Check: ']");
        readonly By SaveNCloseBtn = By.Id("TI_SaveAndCloseButton");
        readonly By SaveBtn = By.Id("TI_SaveButton");
        readonly By BackButton = By.Id("BackButton");

        #endregion

        #region General

        readonly By PersonLookupFieldLink = By.Id("CWField_personid_Link");
        readonly By PersonidLookupButton = By.XPath("//*[@id='CWLookupBtn_personid']");

        readonly By PreferencesTextareField = By.XPath("//*[@id = 'CWField_preferences']");

        readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");

        readonly By DateAndTimeOccurredFieldLabel = By.Id("CWLabelHolder_occurred");
        readonly By DateAndTimeOccurred_DateField = By.Id("CWField_occurred");
        readonly By DateAndTimeOccurred_TimeField = By.Id("CWField_occurred_Time");
        readonly By DateAndTimeOccurred_DatePicker = By.Id("CWField_occurred_DatePicker");
        readonly By DateAndTimeOccurred_TimePicker = By.Id("CWField_occurred_Time_TimePicker");

        #endregion

        #region Details

        readonly By Sleepstatusid = By.XPath("//*[@id='CWField_sleepstatusid']");
        readonly By DaynightcheckobservationsidLookupButton = By.XPath("//*[@id='CWLookupBtn_daynightcheckobservationsid']");
        By Daynightcheckobservations_SelectedOption(int optionId) => By.XPath("//*[@id='MS_daynightcheckobservationsid_" + optionId + "']");
        By Daynightcheckobservations_RemoveButton(int optionId) => By.XPath("//*[@id='MS_daynightcheckobservationsid_" + optionId + "']/a");

        readonly By Otherobservation = By.XPath("//*[@id='CWField_otherobservation']");
        readonly By detailsofconversation = By.XPath("//*[@id='CWField_detailsofconversation']");
        readonly By Istheresidentinasafeplace_1 = By.XPath("//*[@id='CWField_istheresidentinasafeplace_1']");
        readonly By Istheresidentinasafeplace_0 = By.XPath("//*[@id='CWField_istheresidentinasafeplace_0']");
        readonly By actiontakensafeplaceaction = By.XPath("//*[@id='CWField_actiontakensafeplaceaction']");

        #endregion

        #region Additional Information

        readonly By CarephysicallocationidLookupButton = By.XPath("//*[@id='CWLookupBtn_carephysicallocationid']");
        By Carephysicallocation_SelectedOption(string optionId) => By.XPath("//*[@id='MS_carephysicallocationid_" + optionId + "']");
        By Carephysicallocation_RemoveButton(string optionId) => By.XPath("//*[@id='MS_carephysicallocationid_" + optionId + "']/a[text()='Remove']");

        readonly By locationifother = By.XPath("//*[@id='CWField_locationifother']");

        readonly By CarewellbeingidLookupButton = By.XPath("//*[@id='CWLookupBtn_carewellbeingid']");

        readonly By actiontaken = By.XPath("//*[@id='CWField_actiontaken']");

        readonly By TotalTimeSpentWithClientMinutesFieldLabel = By.XPath("//*[@id = 'CWLabelHolder_timespentwithclient']/label[text() = 'Total Time Spent With Person (Minutes)']");
        readonly By TotalTimeSpentWithClientMinutesField = By.Id("CWField_timespentwithclient");
        readonly By TotalTimeSpentWithClientMinutesFieldError = By.XPath("//label[@for = 'CWField_timespentwithclient'][@class = 'formerror']/span");

        readonly By Additionalnotes = By.XPath("//*[@id='CWField_additionalnotes']");

        readonly By StaffrequiredLookupButton = By.XPath("//*[@id='CWLookupBtn_staffrequired']");
        By Staffrequired_SelectedOption(string optionId) => By.XPath("//*[@id='MS_staffrequired_" + optionId + "']");
        By Staffrequired_RemoveButton(string optionId) => By.XPath("//*[@id='MS_staffrequired_" + optionId + "']/a[text()='Remove']");

        #endregion

        #region Care Note

        readonly string CarenoteFieldId = "CWField_carenote";
        readonly By Carenote = By.XPath("//*[@id='CWField_carenote']");

        #endregion

        #region Care Needs

        readonly By LinkedadlcategoriesidLookupButton = By.XPath("//*[@id='CWLookupBtn_linkedadlcategoriesid']");
        By Linkedadlcategories_SelectedOption(string optionId) => By.XPath("//*[@id='MS_linkedadlcategoriesid_" + optionId + "']");
        By Linkedadlcategories_RemoveButton(string optionId) => By.XPath("//*[@id='MS_linkedadlcategoriesid_" + optionId + "']/a[text()='Remove']");

        #endregion

        #region Handover

        readonly By Isincludeinnexthandover_1 = By.XPath("//*[@id='CWField_isincludeinnexthandover_1']");
        readonly By Isincludeinnexthandover_0 = By.XPath("//*[@id='CWField_isincludeinnexthandover_0']");
        readonly By Flagrecordforhandover_1 = By.XPath("//*[@id='CWField_flagrecordforhandover_1']");
        readonly By Flagrecordforhandover_0 = By.XPath("//*[@id='CWField_flagrecordforhandover_0']");

        #endregion

        #endregion


        public PersonWelfareCheckRecordPage WaitForPageToLoad()
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
            WaitForElement(CarephysicallocationidLookupButton);

            return this;
        }

        //verify pageHeader text
        public PersonWelfareCheckRecordPage VerifyPageHeaderText(string ExpectedText)
        {
            WaitForElementVisible(pageHeader);
            string pageTitle = GetElementByAttributeValue(pageHeader, "title");
            Assert.AreEqual("Welfare Check: " + ExpectedText, pageTitle);

            return this;
        }

        #region Options Toolbar

        public PersonWelfareCheckRecordPage ClickSaveAndClose()
        {
            WaitForElementToBeClickable(SaveNCloseBtn);
            Click(SaveNCloseBtn);

            return this;
        }

        public PersonWelfareCheckRecordPage ClickSave()
        {
            WaitForElementToBeClickable(SaveBtn);
            Click(SaveBtn);

            return this;
        }

        public PersonWelfareCheckRecordPage ClickBack()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        #endregion

        #region General

        //verify personlookupfieldlinktext
        public PersonWelfareCheckRecordPage VerifyPersonLookupFieldLinkText(string ExpectedText)
        {
            ScrollToElement(PersonLookupFieldLink);
            WaitForElementVisible(PersonLookupFieldLink);
            ValidateElementByTitle(PersonLookupFieldLink, ExpectedText);

            return this;
        }

        public PersonWelfareCheckRecordPage ClickPersonLookupButton()
        {
            WaitForElementToBeClickable(PersonidLookupButton);
            Click(PersonidLookupButton);

            return this;
        }

        //verify preferences textare field is displayed or not displayed
        public PersonWelfareCheckRecordPage VerifyPreferencesTextAreaFieldIsDisplayed(bool ExpectedDisplayed)
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
        public PersonWelfareCheckRecordPage VerifyPreferencesTextAreaFieldIsDisabled(bool ExpectedDisabled)
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
        public PersonWelfareCheckRecordPage VerifyPreferencesTextAreaFieldText(string ExpectedText)
        {
            ScrollToElement(PreferencesTextareField);
            WaitForElementVisible(PreferencesTextareField);
            ValidateElementValue(PreferencesTextareField, ExpectedText);

            return this;
        }

        //verify preference textarea field attribute value
        public PersonWelfareCheckRecordPage VerifyPreferencesTextAreaFieldMaxLength(string ExpectedValue)
        {
            ScrollToElement(PreferencesTextareField);
            WaitForElementVisible(PreferencesTextareField);
            ValidateElementAttribute(PreferencesTextareField, "maxlength", ExpectedValue);

            return this;
        }

        public PersonWelfareCheckRecordPage ClickResponsibleTeamLink()
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            Click(ResponsibleTeamLink);

            return this;
        }

        public PersonWelfareCheckRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            ValidateElementText(ResponsibleTeamLink, ExpectedText);

            return this;
        }

        public PersonWelfareCheckRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamLookupButton);
            Click(ResponsibleTeamLookupButton);

            return this;
        }

        public PersonWelfareCheckRecordPage SetDateOccurred(string dateoccured)
        {
            ScrollToElement(DateAndTimeOccurred_DateField);
            SendKeys(DateAndTimeOccurred_DateField, dateoccured + OpenQA.Selenium.Keys.Tab);


            return this;
        }

        //click DateAndTimeOccurred_DatePicker
        public PersonWelfareCheckRecordPage ClickDateAndTimeOccurredDatePicker()
        {
            WaitForElement(DateAndTimeOccurred_DatePicker);
            Click(DateAndTimeOccurred_DatePicker);

            return this;
        }

        public PersonWelfareCheckRecordPage SetTimeOccurred(string timeoccured)
        {
            WaitForElementVisible(DateAndTimeOccurred_TimeField);
            SendKeys(DateAndTimeOccurred_TimeField, timeoccured + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        //click DateAndTimeOccurred_TimePicker
        public PersonWelfareCheckRecordPage ClickDateAndTimeOccurredTimePicker()
        {
            WaitForElement(DateAndTimeOccurred_TimePicker);
            Click(DateAndTimeOccurred_TimePicker);

            return this;
        }

        //verify dateandtimeoccurredfieldlabel and dateandtimeoccurredfield is displayed or not displayed
        public PersonWelfareCheckRecordPage VerifyDateAndTimeOccurredFieldsAreDisplayed(bool ExpectedDisplayed)
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
        public PersonWelfareCheckRecordPage VerifyDateAndTimeOccurredDateFieldText(string ExpectedText)
        {
            ScrollToElement(DateAndTimeOccurred_DateField);
            WaitForElementVisible(DateAndTimeOccurred_DateField);
            ValidateElementValue(DateAndTimeOccurred_DateField, ExpectedText);

            return this;
        }

        //verify dateandtimeoccurred_timefield
        public PersonWelfareCheckRecordPage VerifyDateAndTimeOccurredTimeFieldText(string ExpectedText)
        {
            ScrollToElement(DateAndTimeOccurred_TimeField);
            WaitForElementVisible(DateAndTimeOccurred_TimeField);
            ValidateElementValue(DateAndTimeOccurred_TimeField, ExpectedText);

            return this;
        }












        #endregion

        #region Details

        public PersonWelfareCheckRecordPage SelectWereTheyAsleepOrAwake(string TextToSelect)
        {
            WaitForElementToBeClickable(Sleepstatusid);
            SelectPicklistElementByText(Sleepstatusid, TextToSelect);

            return this;
        }

        public PersonWelfareCheckRecordPage ValidateWereTheyAsleepOrAwakeSelectedText(string ExpectedText)
        {
            ValidateElementText(Sleepstatusid, ExpectedText);

            return this;
        }

        public PersonWelfareCheckRecordPage ClickObservationsLookupButton()
        {
            WaitForElementToBeClickable(DaynightcheckobservationsidLookupButton);
            Click(DaynightcheckobservationsidLookupButton);

            return this;
        }

        public PersonWelfareCheckRecordPage ValidateObservationsSelectedOptionText(int OptionId, string ExpectedText)
        {
            WaitForElement(Daynightcheckobservations_SelectedOption(OptionId));
            ValidateElementText(Daynightcheckobservations_SelectedOption(OptionId), ExpectedText);

            return this;
        }

        public PersonWelfareCheckRecordPage ClickObservationsSelectedOptionRemoveButton(int OptionId)
        {
            WaitForElementToBeClickable(Daynightcheckobservations_RemoveButton(OptionId));
            Click(Daynightcheckobservations_RemoveButton(OptionId));

            return this;
        }

        public PersonWelfareCheckRecordPage ValidateOtherObservationText(string ExpectedText)
        {
            ValidateElementValue(Otherobservation, ExpectedText);

            return this;
        }

        public PersonWelfareCheckRecordPage InsertTextOnOtherObservationText(string TextToInsert)
        {
            WaitForElementToBeClickable(Otherobservation);
            SendKeys(Otherobservation, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public PersonWelfareCheckRecordPage ClickIsTheResidentInASafePlace_YesRadioButton()
        {
            WaitForElementToBeClickable(Istheresidentinasafeplace_1);
            Click(Istheresidentinasafeplace_1);

            return this;
        }

        public PersonWelfareCheckRecordPage ValidateIsTheResidentInASafePlace_YesRadioButtonChecked()
        {
            WaitForElement(Istheresidentinasafeplace_1);
            ValidateElementChecked(Istheresidentinasafeplace_1);

            return this;
        }

        public PersonWelfareCheckRecordPage ValidateIsTheResidentInASafePlace_YesRadioButtonNotChecked()
        {
            WaitForElement(Istheresidentinasafeplace_1);
            ValidateElementNotChecked(Istheresidentinasafeplace_1);

            return this;
        }

        public PersonWelfareCheckRecordPage ClickIsTheResidentInASafePlace_NoRadioButton()
        {
            WaitForElementToBeClickable(Istheresidentinasafeplace_0);
            Click(Istheresidentinasafeplace_0);

            return this;
        }

        public PersonWelfareCheckRecordPage ValidateIsTheResidentInASafePlace_NoRadioButtonChecked()
        {
            WaitForElement(Istheresidentinasafeplace_0);
            ValidateElementChecked(Istheresidentinasafeplace_0);

            return this;
        }

        public PersonWelfareCheckRecordPage ValidateIsTheResidentInASafePlace_NoRadioButtonNotChecked()
        {
            WaitForElement(Istheresidentinasafeplace_0);
            ValidateElementNotChecked(Istheresidentinasafeplace_0);

            return this;
        }

        public PersonWelfareCheckRecordPage InsertTextOnDetailsOfConversation(string TextToInsert)
        {
            WaitForElementVisible(detailsofconversation);
            SendKeys(detailsofconversation, TextToInsert + OpenQA.Selenium.Keys.Tab);


            return this;
        }

        public PersonWelfareCheckRecordPage ValidateDetailsOfConversationText(string ExpectedText)
        {
            ValidateElementValue(detailsofconversation, ExpectedText);

            return this;
        }

        public PersonWelfareCheckRecordPage InsertTextOnActionTaken(string TextToInsert)
        {
            WaitForElementVisible(actiontakensafeplaceaction);
            SendKeys(actiontakensafeplaceaction, TextToInsert + OpenQA.Selenium.Keys.Tab);


            return this;
        }

        public PersonWelfareCheckRecordPage ValidateActionTakenText(string ExpectedText)
        {
            ValidateElementValue(actiontakensafeplaceaction, ExpectedText);

            return this;
        }

        #endregion

        #region Additional Information

        public PersonWelfareCheckRecordPage ClickLocationLookupButton()
        {
            WaitForElementToBeClickable(CarephysicallocationidLookupButton);
            Click(CarephysicallocationidLookupButton);

            return this;
        }

        public PersonWelfareCheckRecordPage ValidateLocationSelectedOptionText(string OptionId, string ExpectedText)
        {
            WaitForElement(Carephysicallocation_SelectedOption(OptionId));
            ValidateElementText(Carephysicallocation_SelectedOption(OptionId), ExpectedText);

            return this;
        }

        public PersonWelfareCheckRecordPage ValidateLocationSelectedOptionText(Guid OptionId, string ExpectedText)
        {
            return ValidateLocationSelectedOptionText(OptionId.ToString(), ExpectedText);
        }

        public PersonWelfareCheckRecordPage ClickLocationSelectedOptionRemoveButton(string OptionId)
        {
            WaitForElementToBeClickable(Carephysicallocation_RemoveButton(OptionId));
            Click(Carephysicallocation_RemoveButton(OptionId));

            return this;
        }

        public PersonWelfareCheckRecordPage ClickLocationSelectedOptionRemoveButton(Guid OptionId)
        {
            return ClickLocationSelectedOptionRemoveButton(OptionId.ToString());
        }

        public PersonWelfareCheckRecordPage ValidateLocationIfOtherText(string ExpectedText)
        {
            ValidateElementText(locationifother, ExpectedText);

            return this;
        }

        public PersonWelfareCheckRecordPage InsertTextOnLocationIfOther(string TextToInsert)
        {
            WaitForElementToBeClickable(locationifother);
            SendKeys(locationifother, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public PersonWelfareCheckRecordPage ClickWellbeingLookupButton()
        {
            WaitForElementToBeClickable(CarewellbeingidLookupButton);
            Click(CarewellbeingidLookupButton);

            return this;
        }

        public PersonWelfareCheckRecordPage ValidateActionTakenHasPainReliefBeenOfferedText(string ExpectedText)
        {
            ValidateElementText(actiontaken, ExpectedText);

            return this;
        }

        public PersonWelfareCheckRecordPage InsertTextOnActionTakenHasPainReliefBeenOffered(string TextToInsert)
        {
            WaitForElementToBeClickable(actiontaken);
            SendKeys(actiontaken, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public PersonWelfareCheckRecordPage InsertTotalTimeSpentWithPersonMinutes(string TextToInsert)
        {
            WaitForElementVisible(TotalTimeSpentWithClientMinutesField);
            SendKeys(TotalTimeSpentWithClientMinutesField, TextToInsert + OpenQA.Selenium.Keys.Tab);


            return this;
        }

        //verify totaltimespentwithclientminutesfield is present or not
        public PersonWelfareCheckRecordPage VerifyTotalTimeSpentWithPersonMinutesFieldIsDisplayed(bool ExpectedPresent)
        {
            if (ExpectedPresent)
            {
                WaitForElementVisible(TotalTimeSpentWithClientMinutesFieldLabel);
                WaitForElementVisible(TotalTimeSpentWithClientMinutesField);
            }
            else
            {
                WaitForElementNotVisible(TotalTimeSpentWithClientMinutesField, 2);
            }

            return this;
        }

        //verify totaltimespentwithclientminutesfield
        public PersonWelfareCheckRecordPage VerifyTotalTimeSpentWithPersonMinutesFieldText(string ExpectedText)
        {
            WaitForElementVisible(TotalTimeSpentWithClientMinutesField);
            ValidateElementValue(TotalTimeSpentWithClientMinutesField, ExpectedText);

            return this;
        }

        //verify totaltimespentwithclientminutesfield error
        public PersonWelfareCheckRecordPage VerifyTotalTimeSpentWithPersonMinutesFieldErrorText(string ExpectedText)
        {
            WaitForElement(TotalTimeSpentWithClientMinutesFieldError);
            ValidateElementByTitle(TotalTimeSpentWithClientMinutesFieldError, ExpectedText);

            return this;
        }

        public PersonWelfareCheckRecordPage ValidateAdditionalNotesText(string ExpectedText)
        {
            ValidateElementText(Additionalnotes, ExpectedText);

            return this;
        }

        public PersonWelfareCheckRecordPage InsertTextOnAdditionalNotes(string TextToInsert)
        {
            WaitForElementToBeClickable(Additionalnotes);
            SendKeys(Additionalnotes, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public PersonWelfareCheckRecordPage ClickStaffRequiredLookupButton()
        {
            WaitForElementToBeClickable(StaffrequiredLookupButton);
            Click(StaffrequiredLookupButton);

            return this;
        }

        public PersonWelfareCheckRecordPage ValidateStaffRequiredSelectedOptionText(string OptionId, string ExpectedText)
        {
            WaitForElement(Staffrequired_SelectedOption(OptionId));
            ValidateElementText(Staffrequired_SelectedOption(OptionId), ExpectedText);

            return this;
        }

        public PersonWelfareCheckRecordPage ValidateStaffRequiredSelectedOptionText(Guid OptionId, string ExpectedText)
        {
            return ValidateStaffRequiredSelectedOptionText(OptionId.ToString(), ExpectedText);
        }

        public PersonWelfareCheckRecordPage ClickStaffRequiredSelectedOptionRemoveButton(string OptionId)
        {
            WaitForElementToBeClickable(Staffrequired_RemoveButton(OptionId));
            Click(Staffrequired_RemoveButton(OptionId));

            return this;
        }

        public PersonWelfareCheckRecordPage ClickStaffRequiredSelectedOptionRemoveButton(Guid OptionId)
        {
            return ClickStaffRequiredSelectedOptionRemoveButton(OptionId.ToString());
        }

        #endregion

        #region Care Note

        public PersonWelfareCheckRecordPage ValidateCareNoteText(string ExpectedText)
        {
            var fieldValue = GetElementValueByJavascript(CarenoteFieldId);
            Assert.AreEqual(ExpectedText, fieldValue);

            return this;
        }

        public PersonWelfareCheckRecordPage InsertTextOnCareNote(string TextToInsert)
        {
            WaitForElementToBeClickable(Carenote);
            SendKeys(Carenote, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        #endregion

        #region Care Needs

        public PersonWelfareCheckRecordPage ClickLinkedActivitiesOfDailyLivingLookupButton()
        {
            WaitForElementToBeClickable(LinkedadlcategoriesidLookupButton);
            Click(LinkedadlcategoriesidLookupButton);

            return this;
        }

        public PersonWelfareCheckRecordPage ValidateLinkedActivitiesOfDailyLivingSelectedOptionText(string OptionId, string ExpectedText)
        {
            WaitForElement(Linkedadlcategories_SelectedOption(OptionId));
            ValidateElementText(Linkedadlcategories_SelectedOption(OptionId), ExpectedText);

            return this;
        }

        public PersonWelfareCheckRecordPage ValidateLinkedActivitiesOfDailyLivingSelectedOptionText(Guid OptionId, string ExpectedText)
        {
            return ValidateLinkedActivitiesOfDailyLivingSelectedOptionText(OptionId.ToString(), ExpectedText);
        }

        public PersonWelfareCheckRecordPage ClickLinkedActivitiesOfDailyLivingSelectedOptionRemoveButton(string OptionId)
        {
            WaitForElementToBeClickable(Linkedadlcategories_RemoveButton(OptionId));
            Click(Linkedadlcategories_RemoveButton(OptionId));

            return this;
        }

        public PersonWelfareCheckRecordPage ClickLinkedActivitiesOfDailyLivingSelectedOptionRemoveButton(Guid OptionId)
        {
            return ClickLinkedActivitiesOfDailyLivingSelectedOptionRemoveButton(OptionId.ToString());
        }

        #endregion

        #region Handover

        public PersonWelfareCheckRecordPage ClickIncludeInNextHandover_YesRadioButton()
        {
            WaitForElementToBeClickable(Isincludeinnexthandover_1);
            Click(Isincludeinnexthandover_1);

            return this;
        }

        public PersonWelfareCheckRecordPage ValidateIncludeInNextHandover_YesRadioButtonChecked()
        {
            WaitForElement(Isincludeinnexthandover_1);
            ValidateElementChecked(Isincludeinnexthandover_1);

            return this;
        }

        public PersonWelfareCheckRecordPage ValidateIncludeInNextHandover_YesRadioButtonNotChecked()
        {
            WaitForElement(Isincludeinnexthandover_1);
            ValidateElementNotChecked(Isincludeinnexthandover_1);

            return this;
        }

        public PersonWelfareCheckRecordPage ClickIncludeInNextHandover_NoRadioButton()
        {
            WaitForElementToBeClickable(Isincludeinnexthandover_0);
            Click(Isincludeinnexthandover_0);

            return this;
        }

        public PersonWelfareCheckRecordPage ValidateIncludeInNextHandover_NoRadioButtonChecked()
        {
            WaitForElement(Isincludeinnexthandover_0);
            ValidateElementChecked(Isincludeinnexthandover_0);

            return this;
        }

        public PersonWelfareCheckRecordPage ValidateIncludeInNextHandover_NoRadioButtonNotChecked()
        {
            WaitForElement(Isincludeinnexthandover_0);
            ValidateElementNotChecked(Isincludeinnexthandover_0);

            return this;
        }

        public PersonWelfareCheckRecordPage ClickFlagRecordForHandover_YesRadioButton()
        {
            WaitForElementToBeClickable(Flagrecordforhandover_1);
            Click(Flagrecordforhandover_1);

            return this;
        }

        public PersonWelfareCheckRecordPage ValidateFlagRecordForHandover_YesRadioButtonChecked()
        {
            WaitForElement(Flagrecordforhandover_1);
            ValidateElementChecked(Flagrecordforhandover_1);

            return this;
        }

        public PersonWelfareCheckRecordPage ValidateFlagRecordForHandover_YesRadioButtonNotChecked()
        {
            WaitForElement(Flagrecordforhandover_1);
            ValidateElementNotChecked(Flagrecordforhandover_1);

            return this;
        }

        public PersonWelfareCheckRecordPage ClickFlagRecordForHandover_NoRadioButton()
        {
            WaitForElementToBeClickable(Flagrecordforhandover_0);
            Click(Flagrecordforhandover_0);

            return this;
        }

        public PersonWelfareCheckRecordPage ValidateFlagRecordForHandover_NoRadioButtonChecked()
        {
            WaitForElement(Flagrecordforhandover_0);
            ValidateElementChecked(Flagrecordforhandover_0);

            return this;
        }

        public PersonWelfareCheckRecordPage ValidateFlagRecordForHandover_NoRadioButtonNotChecked()
        {
            WaitForElement(Flagrecordforhandover_0);
            ValidateElementNotChecked(Flagrecordforhandover_0);

            return this;
        }

        #endregion

    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using static System.Windows.Forms.LinkLabel;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonRepositioningRecordPage : CommonMethods
    {
        public PersonRepositioningRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }
        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cppersonturning&')]");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Person Repositioning: ']");
        readonly By SaveNCloseBtn = By.Id("TI_SaveAndCloseButton");
        readonly By SaveBtn = By.Id("TI_SaveButton");
        readonly By BackButton = By.Id("BackButton");


        #region General
        //readonly By occurred = By.XPath("//input[@id = 'CWField_occurred']");
        //readonly By occurredTime = By.XPath("//input[@id = 'CWField_occurred_Time']");

        readonly By PersonLookupFieldLink = By.Id("CWField_personid_Link");
        readonly By CareConsentGivenId = By.XPath("//*[@id='CWField_careconsentgivenid']");

        readonly By PreferencesTextareField = By.XPath("//*[@id = 'CWField_preferences']");

        readonly By TotalTimeSpentWithClientMinutesFieldLabel = By.XPath("//*[@id = 'CWLabelHolder_timespentwithclient']/label[text() = 'Total Time Spent With Person (Minutes)']");
        readonly By TotalTimeSpentWithClientMinutesField = By.Id("CWField_timespentwithclient");
        readonly By TotalTimeSpentWithClientMinutesFieldError = By.XPath("//label[@for = 'CWField_timespentwithclient'][@class = 'formerror']/span");

        readonly By DateAndTimeOccurredFieldLabel = By.Id("CWLabelHolder_occurred");
        readonly By DateAndTimeOccurred_DateField = By.Id("CWField_occurred");
        readonly By DateAndTimeOccurred_TimeField = By.Id("CWField_occurred_Time");
        readonly By DateAndTimeOccurred_DatePicker = By.Id("CWField_occurred_DatePicker");
        readonly By DateAndTimeOccurred_TimePicker = By.Id("CWField_occurred_Time_TimePicker");

        readonly By ConsentGivenPicklist = By.Id("CWField_careconsentgivenid");
        readonly By nonconsentDetail = By.Id("CWField_carenonconsentid");
        readonly By ReasonForAbsenceTextareaField = By.Id("CWField_reasonforabsence");

        #endregion

        #region Details

        readonly By Startingpositionid = By.XPath("//*[@id='CWField_startingpositionid']");
        readonly By Confirmturnedid = By.XPath("//*[@id='CWField_confirmturnedid']");
        readonly By Endingpositionid = By.XPath("//*[@id='CWField_endingpositionid']");
        readonly By Positionturnedid = By.XPath("//*[@id='CWField_positionturnedid']");
        readonly By Comfortableid = By.XPath("//*[@id='CWField_comfortableid']");
        readonly By SpecialistmattressidLink = By.XPath("//*[@id='CWField_specialistmattressid_Link']");
        readonly By SpecialistmattressidClearButton = By.XPath("//*[@id='CWClearLookup_specialistmattressid']");
        readonly By SpecialistmattressidLookupButton = By.XPath("//*[@id='CWLookupBtn_specialistmattressid']");
        readonly By Arethereanynewconcernswiththeresidentskin_1 = By.XPath("//*[@id='CWField_arethereanynewconcernswiththeresidentskin_1']");
        readonly By Arethereanynewconcernswiththeresidentskin_0 = By.XPath("//*[@id='CWField_arethereanynewconcernswiththeresidentskin_0']");
        readonly By Skinconditionwhere = By.XPath("//*[@id='CWField_skinconditionwhere']");
		readonly By DescribeskinconditionLookupButton = By.XPath("//*[@id='CWLookupBtn_describeskincondition']");

        By DescribeSkinCondition_SelectedOption(string optionId) => By.XPath("//*[@id='MS_describeskincondition_" + optionId + "']");
        By DescribeSkinCondition_RemoveButton(string optionId) => By.XPath("//*[@id='MS_describeskincondition_" + optionId + "']/a[text()='Remove']");

        #endregion

        #region Additional Information

        readonly By CarephysicallocationidLookupButton = By.XPath("//*[@id='CWLookupBtn_carephysicallocationid']");
        By Carephysicallocation_SelectedOption(string optionId) => By.XPath("//*[@id='MS_carephysicallocationid_" + optionId + "']");
        By Carephysicallocation_RemoveButton(string optionId) => By.XPath("//*[@id='MS_carephysicallocationid_" + optionId + "']/a[text()='Remove']");

        readonly By locationifother = By.XPath("//*[@id='CWField_locationifother']");

        readonly By StaffrequiredLookupButton = By.XPath("//*[@id='CWLookupBtn_staffrequired']");
        By Staffrequired_SelectedOption(string optionId) => By.XPath("//*[@id='MS_staffrequired_" + optionId + "']");
        By Staffrequired_RemoveButton(string optionId) => By.XPath("//*[@id='MS_staffrequired_" + optionId + "']/a[text()='Remove']");

        readonly By EquipmentLookUpBtn = By.Id("CWLookupBtn_equipmentid");
        readonly By AssistanceNeededLookUpBtn = By.Id("CWLookupBtn_careassistanceneededid");
        readonly By AssistanceAmountPicklist = By.Id("CWField_careassistancelevelid");
        readonly By WellBeingLookUpBtn = By.Id("CWLookupBtn_carewellbeingid");
        readonly By TotalTimeSpentWithClientMinutes = By.Id("CWField_totaltimespentwithclientminutes");
        readonly By AdditionalNotesTextArea = By.Id("CWField_additionalnotes");

        #endregion

        #region Care Note

        readonly string CarenoteFieldId = "CWField_carenote";
        readonly By Carenote = By.XPath("//*[@id='CWField_carenote']");

        #endregion

        #region Care needs and Handover

        readonly By LinkedActivitiesOfAdlLookupButton = By.Id("CWLookupBtn_linkedadlcategoriesid");
        readonly By IncludeInNextHandover_YesOption = By.Id("CWField_isincludeinnexthandover_1");
        readonly By IncludeInNextHandover_NoOption = By.Id("CWField_isincludeinnexthandover_0");
        readonly By FlagRecordForHandover_YesOption = By.Id("CWField_flagrecordforhandover_1");
        readonly By FlagRecordForHandover_NoOption = By.Id("CWField_flagrecordforhandover_0");

        #endregion


        public PersonRepositioningRecordPage WaitForPageToLoad()
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

        #region general

        public PersonRepositioningRecordPage InsertTotalTimeSpentWithClientMinutes(String TextToInsert)
        {
            ScrollToElement(TotalTimeSpentWithClientMinutes);
            WaitForElementVisible(TotalTimeSpentWithClientMinutes);
            SendKeys(TotalTimeSpentWithClientMinutes, TextToInsert + Keys.Tab);


            return this;
        }


        public PersonRepositioningRecordPage SetDateOccurred(String dateoccured)
        {
            ScrollToElement(DateAndTimeOccurred_DateField);
            WaitForElementVisible(DateAndTimeOccurred_DateField);
            SendKeys(DateAndTimeOccurred_DateField, dateoccured);


            return this;
        }

        //click DateAndTimeOccurred_DatePicker
        public PersonRepositioningRecordPage ClickDateAndTimeOccurredDatePicker()
        {
            WaitForElement(DateAndTimeOccurred_DatePicker);
            ScrollToElement(DateAndTimeOccurred_DatePicker);
            Click(DateAndTimeOccurred_DatePicker);

            return this;
        }

        public PersonRepositioningRecordPage SetTimeOccurred(String timeoccured)
        {
            WaitForElement(DateAndTimeOccurred_TimeField);
            System.Threading.Thread.Sleep(1000);
            ScrollToElement(DateAndTimeOccurred_TimeField);
            WaitForElementVisible(DateAndTimeOccurred_TimeField);
            Click(DateAndTimeOccurred_TimeField);
            ClearText(DateAndTimeOccurred_TimeField);
            System.Threading.Thread.Sleep(1000);
            SendKeys(DateAndTimeOccurred_TimeField, timeoccured+Keys.Tab);


            return this;
        }

        //click DateAndTimeOccurred_TimePicker
        public PersonRepositioningRecordPage ClickDateAndTimeOccurredTimePicker()
        {
            WaitForElement(DateAndTimeOccurred_TimePicker);
            ScrollToElement(DateAndTimeOccurred_TimePicker);
            Click(DateAndTimeOccurred_TimePicker);

            return this;
        }

        //verify personlookupfieldlinktext
        public PersonRepositioningRecordPage VerifyPersonLookupFieldLinkText(string ExpectedText)
        {
            ScrollToElement(PersonLookupFieldLink);
            WaitForElementVisible(PersonLookupFieldLink);
            ValidateElementByTitle(PersonLookupFieldLink, ExpectedText);

            return this;
        }

        //verify totaltimespentwithclientminutesfield is present or not
        public PersonRepositioningRecordPage VerifyTotalTimeSpentWithClientMinutesFieldIsDisplayed(bool ExpectedPresent)
        {
            if(ExpectedPresent)
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
        public PersonRepositioningRecordPage VerifyTotalTimeSpentWithClientMinutesFieldText(string ExpectedText)
        {
            ScrollToElement(TotalTimeSpentWithClientMinutes);
            WaitForElementVisible(TotalTimeSpentWithClientMinutes);
            ValidateElementValue(TotalTimeSpentWithClientMinutes, ExpectedText);

            return this;
        }

        //verify totaltimespentwithclientminutesfield error
        public PersonRepositioningRecordPage VerifyTotalTimeSpentWithClientMinutesFieldErrorText(string ExpectedText)
        {
            WaitForElement(TotalTimeSpentWithClientMinutesFieldError);
            ScrollToElement(TotalTimeSpentWithClientMinutesFieldError);
            ValidateElementByTitle(TotalTimeSpentWithClientMinutesFieldError, ExpectedText);

            return this;
        }

        //verify dateandtimeoccurredfieldlabel and dateandtimeoccurredfield is displayed or not displayed
        public PersonRepositioningRecordPage VerifyDateAndTimeOccurredFieldsAreDisplayed(bool ExpectedDisplayed)
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
        public PersonRepositioningRecordPage VerifyDateAndTimeOccurredDateFieldText(string ExpectedText)
        {
            ScrollToElement(DateAndTimeOccurred_DateField);
            WaitForElementVisible(DateAndTimeOccurred_DateField);
            ValidateElementValue(DateAndTimeOccurred_DateField, ExpectedText);

            return this;
        }

        //verify dateandtimeoccurred_timefield
        public PersonRepositioningRecordPage VerifyDateAndTimeOccurredTimeFieldText(string ExpectedText)
        {
            ScrollToElement(DateAndTimeOccurred_TimeField);
            WaitForElementVisible(DateAndTimeOccurred_TimeField);
            ValidateElementValue(DateAndTimeOccurred_TimeField, ExpectedText);

            return this;
        }

        //verify preferences textare field is displayed or not displayed
        public PersonRepositioningRecordPage VerifyPreferencesTextAreaFieldIsDisplayed(bool ExpectedDisplayed)
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
        public PersonRepositioningRecordPage VerifyPreferencesTextAreaFieldIsDisabled(bool ExpectedDisabled)
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
        public PersonRepositioningRecordPage VerifyPreferencesTextAreaFieldText(string ExpectedText)
        {
            ScrollToElement(PreferencesTextareField);
            WaitForElementVisible(PreferencesTextareField);
            ValidateElementValue(PreferencesTextareField, ExpectedText);

            return this;
        }

        //verify preference textarea field attribute value
        public PersonRepositioningRecordPage VerifyPreferencesTextAreaFieldMaxLength(string ExpectedValue)
        {
            ScrollToElement(PreferencesTextareField);
            WaitForElementVisible(PreferencesTextareField);
            ValidateElementAttribute(PreferencesTextareField, "maxlength", ExpectedValue);

            return this;
        }

        #endregion

        #region Options Toolbar

        public PersonRepositioningRecordPage ClickSaveAndClose()
        {
            WaitForElementToBeClickable(SaveNCloseBtn);
            Click(SaveNCloseBtn);

            return this;
        }

        public PersonRepositioningRecordPage ClickSave()
        {
            WaitForElementToBeClickable(SaveBtn);
            Click(SaveBtn);

            return this;
        }

        public PersonRepositioningRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        #endregion

        public PersonRepositioningRecordPage VerifyPageHeaderText(string ExpectedText)
        {
            ScrollToElement(pageHeader);
            WaitForElementVisible(pageHeader);
            string pageTitle = GetElementByAttributeValue(pageHeader, "title");
            Assert.AreEqual("Person Repositioning: " + ExpectedText, pageTitle);

            return this;
        }

        //Select value from consentgivenpicklist by text
        public PersonRepositioningRecordPage SelectConsentGivenPicklistValueByText(string TextToSelect)
        {
            ScrollToElement(ConsentGivenPicklist);
            WaitForElementVisible(ConsentGivenPicklist);
            SelectPicklistElementByText(ConsentGivenPicklist, TextToSelect);

            return this;
        }

        //verify consentgivenpicklist selected value
        public PersonRepositioningRecordPage VerifyConsentGivenPicklistSelectedValue(string ExpectedText)
        {
            ScrollToElement(ConsentGivenPicklist);
            WaitForElementVisible(ConsentGivenPicklist);
            ValidatePicklistSelectedText(ConsentGivenPicklist, ExpectedText);

            return this;
        }

        //Select value from nonconsentdetail by text
        public PersonRepositioningRecordPage SelectNonConsentDetailValueByText(string TextToSelect)
        {
            ScrollToElement(nonconsentDetail);
            WaitForElementVisible(nonconsentDetail);
            SelectPicklistElementByText(nonconsentDetail, TextToSelect);

            return this;
        }

        //verify nonconsentdetail selected value
        public PersonRepositioningRecordPage VerifyNonConsentDetailSelectedValue(string ExpectedText)
        {
            ScrollToElement(nonconsentDetail);
            WaitForElementVisible(nonconsentDetail);
            ValidatePicklistSelectedText(nonconsentDetail, ExpectedText);

            return this;
        }

        //Insert text in reasonforabsence textarea field
        public PersonRepositioningRecordPage InsertTextInReasonForAbsence(String TextToInsert)
        {
            ScrollToElement(ReasonForAbsenceTextareaField);
            WaitForElementVisible(ReasonForAbsenceTextareaField);
            SendKeys(ReasonForAbsenceTextareaField, TextToInsert + Keys.Tab);

            return this;
        }

        //verify reasonforabsence textarea field text
        public PersonRepositioningRecordPage VerifyReasonForAbsenceTextareaFieldText(string ExpectedText)
        {
            ScrollToElement(ReasonForAbsenceTextareaField);
            WaitForElementVisible(ReasonForAbsenceTextareaField);
            ValidateElementValue(ReasonForAbsenceTextareaField, ExpectedText);

            return this;
        }

        #region Care Note

        public PersonRepositioningRecordPage ValidateCareNoteText(string ExpectedText)
        {
            WaitForElement(Carenote);
            ScrollToElement(Carenote);
            var fieldValue = GetElementValueByJavascript(CarenoteFieldId);
            Assert.AreEqual(ExpectedText, fieldValue);

            return this;
        }

        public PersonRepositioningRecordPage InsertTextOnCareNote(string TextToInsert)
        {
            WaitForElementToBeClickable(Carenote);
            SendKeys(Carenote, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        #endregion

        #region Details

        public PersonRepositioningRecordPage SelectStartingPosition(string TextToSelect)
        {
            WaitForElementToBeClickable(Startingpositionid);
            SelectPicklistElementByText(Startingpositionid, TextToSelect);

            return this;
        }

        public PersonRepositioningRecordPage ValidateStartingPositionSelectedText(string ExpectedText)
        {
            WaitForElement(Startingpositionid);
            ScrollToElement(Startingpositionid);
            ValidatePicklistSelectedText(Startingpositionid, ExpectedText);

            return this;
        }

        public PersonRepositioningRecordPage SelectConfirmRepositioned(string TextToSelect)
        {
            WaitForElementToBeClickable(Confirmturnedid);
            SelectPicklistElementByText(Confirmturnedid, TextToSelect);

            return this;
        }

        public PersonRepositioningRecordPage ValidateConfirmRepositionedSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Confirmturnedid, ExpectedText);

            return this;
        }

        public PersonRepositioningRecordPage SelectRepositionedToPosition(string TextToSelect)
        {
            WaitForElementToBeClickable(Endingpositionid);
            ScrollToElement(Endingpositionid);
            SelectPicklistElementByText(Endingpositionid, TextToSelect);

            return this;
        }

        public PersonRepositioningRecordPage ValidateRepositionedToPositionSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Endingpositionid, ExpectedText);

            return this;
        }

        public PersonRepositioningRecordPage SelectRepositionedToSide(string TextToSelect)
        {
            WaitForElementToBeClickable(Positionturnedid);
            SelectPicklistElementByText(Positionturnedid, TextToSelect);

            return this;
        }

        public PersonRepositioningRecordPage ValidateRepositionedToSideSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Positionturnedid, ExpectedText);

            return this;
        }

        public PersonRepositioningRecordPage SelectAreTheyComfortable(string TextToSelect)
        {
            WaitForElementToBeClickable(Comfortableid);
            SelectPicklistElementByText(Comfortableid, TextToSelect);

            return this;
        }

        public PersonRepositioningRecordPage ValidateAreTheyComfortableSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Comfortableid, ExpectedText);

            return this;
        }

        public PersonRepositioningRecordPage ClickTypeOfPressureRelievingEquipmentInUseLink()
        {
            WaitForElementToBeClickable(SpecialistmattressidLink);
            Click(SpecialistmattressidLink);

            return this;
        }

        public PersonRepositioningRecordPage ValidateTypeOfPressureRelievingEquipmentInUseLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(SpecialistmattressidLink);
            ValidateElementText(SpecialistmattressidLink, ExpectedText);

            return this;
        }

        public PersonRepositioningRecordPage ClickTypeOfPressureRelievingEquipmentInUseClearButton()
        {
            WaitForElementToBeClickable(SpecialistmattressidClearButton);
            Click(SpecialistmattressidClearButton);

            return this;
        }

        public PersonRepositioningRecordPage ClickTypeOfPressureRelievingEquipmentInUseLookupButton()
        {
            WaitForElementToBeClickable(SpecialistmattressidLookupButton);
            Click(SpecialistmattressidLookupButton);

            return this;
        }

        public PersonRepositioningRecordPage ClickAreThereAnyNewConcernsWithThePersonsSkin_YesRadioButton()
        {
            WaitForElementToBeClickable(Arethereanynewconcernswiththeresidentskin_1);
            Click(Arethereanynewconcernswiththeresidentskin_1);

            return this;
        }

        public PersonRepositioningRecordPage ValidateAreThereAnyNewConcernsWithThePersonsSkin_YesRadioButtonChecked()
        {
            WaitForElement(Arethereanynewconcernswiththeresidentskin_1);
            ValidateElementChecked(Arethereanynewconcernswiththeresidentskin_1);

            return this;
        }

        public PersonRepositioningRecordPage ValidateAreThereAnyNewConcernsWithThePersonsSkin_YesRadioButtonNotChecked()
        {
            WaitForElement(Arethereanynewconcernswiththeresidentskin_1);
            ValidateElementNotChecked(Arethereanynewconcernswiththeresidentskin_1);

            return this;
        }

        public PersonRepositioningRecordPage ClickAreThereAnyNewConcernsWithThePersonsSkin_NoRadioButton()
        {
            WaitForElementToBeClickable(Arethereanynewconcernswiththeresidentskin_0);
            Click(Arethereanynewconcernswiththeresidentskin_0);

            return this;
        }

        public PersonRepositioningRecordPage ValidateAreThereAnyNewConcernsWithThePersonsSkin_NoRadioButtonChecked()
        {
            WaitForElement(Arethereanynewconcernswiththeresidentskin_0);
            ValidateElementChecked(Arethereanynewconcernswiththeresidentskin_0);

            return this;
        }

        public PersonRepositioningRecordPage ValidateAreThereAnyNewConcernsWithThePersonsSkin_NoRadioButtonNotChecked()
        {
            WaitForElement(Arethereanynewconcernswiththeresidentskin_0);
            ValidateElementNotChecked(Arethereanynewconcernswiththeresidentskin_0);

            return this;
        }

        public PersonRepositioningRecordPage ValidateWhereOnTheBodyText(string ExpectedText)
        {
            ValidateElementValue(Skinconditionwhere, ExpectedText);

            return this;
        }

        public PersonRepositioningRecordPage InsertTextOnWhereOnTheBodyField(string TextToInsert)
        {
            WaitForElement(Skinconditionwhere);
            ScrollToElement(Skinconditionwhere);
            SendKeys(Skinconditionwhere, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonRepositioningRecordPage ClickDescribeSkinConditionLookupButton()
        {
            WaitForElementToBeClickable(DescribeskinconditionLookupButton);
            Click(DescribeskinconditionLookupButton);

            return this;
        }

        public PersonRepositioningRecordPage ValidateDescribeSkinConditionSelectedOptionText(string OptionId, string ExpectedText)
        {
            WaitForElement(DescribeSkinCondition_SelectedOption(OptionId));
            ValidateElementText(DescribeSkinCondition_SelectedOption(OptionId), ExpectedText);

            return this;
        }

        public PersonRepositioningRecordPage ValidateDescribeSkinConditionSelectedOptionText(Guid OptionId, string ExpectedText)
        {
            return ValidateDescribeSkinConditionSelectedOptionText(OptionId.ToString(), ExpectedText);
        }

        public PersonRepositioningRecordPage ClickDescribeSkinConditionSelectedOptionRemoveButton(string OptionId)
        {
            WaitForElementToBeClickable(DescribeSkinCondition_RemoveButton(OptionId));
            Click(DescribeSkinCondition_RemoveButton(OptionId));

            return this;
        }

        public PersonRepositioningRecordPage ClickDescribeSkinConditionSelectedOptionRemoveButton(Guid OptionId)
        {
            return ClickDescribeSkinConditionSelectedOptionRemoveButton(OptionId.ToString());
        }

        #endregion

        #region Additional Information

        public PersonRepositioningRecordPage ClickLocationLookupButton()
        {
            WaitForElementToBeClickable(CarephysicallocationidLookupButton);
            Click(CarephysicallocationidLookupButton);

            return this;
        }

        public PersonRepositioningRecordPage ValidateLocationSelectedOptionText(string OptionId, string ExpectedText)
        {
            WaitForElement(Carephysicallocation_SelectedOption(OptionId));
            ValidateElementText(Carephysicallocation_SelectedOption(OptionId), ExpectedText);

            return this;
        }

        public PersonRepositioningRecordPage ValidateLocationSelectedOptionText(Guid OptionId, string ExpectedText)
        {
            return ValidateLocationSelectedOptionText(OptionId.ToString(), ExpectedText);
        }

        public PersonRepositioningRecordPage ClickLocationSelectedOptionRemoveButton(string OptionId)
        {
            WaitForElementToBeClickable(Carephysicallocation_RemoveButton(OptionId));
            Click(Carephysicallocation_RemoveButton(OptionId));

            return this;
        }

        public PersonRepositioningRecordPage ClickLocationSelectedOptionRemoveButton(Guid OptionId)
        {
            return ClickLocationSelectedOptionRemoveButton(OptionId.ToString());
        }

        public PersonRepositioningRecordPage ValidateLocationIfOtherText(string ExpectedText)
        {
            ValidateElementText(locationifother, ExpectedText);

            return this;
        }

        public PersonRepositioningRecordPage InsertTextOnLocationIfOther(string TextToInsert)
        {
            WaitForElementToBeClickable(locationifother);
            SendKeys(locationifother, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }


        public PersonRepositioningRecordPage ClickEquipmentLookUpBtn()
        {
            WaitForElement(EquipmentLookUpBtn);
            ScrollToElement(EquipmentLookUpBtn);
            Click(EquipmentLookUpBtn);

            return this;
        }

        public PersonRepositioningRecordPage ClickAssistanceNeededLookUpBtn()
        {
            WaitForElementToBeClickable(AssistanceNeededLookUpBtn);
            Click(AssistanceNeededLookUpBtn);

            return this;
        }

        //Select value from CareAssistanceLevePicklist
        public PersonRepositioningRecordPage SelectAssistanceAmount(string TextToSelect)
        {
            WaitForElement(AssistanceAmountPicklist);
            ScrollToElement(AssistanceAmountPicklist);
            SelectPicklistElementByText(AssistanceAmountPicklist, TextToSelect);

            return this;
        }

        public PersonRepositioningRecordPage ClickWellbeingLookUpBtn()
        {
            WaitForElementToBeClickable(WellBeingLookUpBtn);
            Click(WellBeingLookUpBtn);

            return this;
        }

        public PersonRepositioningRecordPage SetTotalTimeSpentWithClient(string TotalTimeSpent)
        {
            ScrollToElement(TotalTimeSpentWithClientMinutes);
            WaitForElementToBeClickable(TotalTimeSpentWithClientMinutes);
            SendKeys(TotalTimeSpentWithClientMinutes, TotalTimeSpent + Keys.Tab);

            return this;
        }

        //Insert text in AdditionalNotesTextArea
        public PersonRepositioningRecordPage InsertTextInAdditionalNotesTextArea(string TextToInsert)
        {
            WaitForElement(AdditionalNotesTextArea);
            ScrollToElement(AdditionalNotesTextArea);
            SendKeys(AdditionalNotesTextArea, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        //verify text in AdditionalNotesTextArea
        public PersonRepositioningRecordPage VerifyTextInAdditionalNotesTextArea(string ExpectedText)
        {
            ScrollToElement(AdditionalNotesTextArea);
            WaitForElementVisible(AdditionalNotesTextArea);
            ValidateElementValue(AdditionalNotesTextArea, ExpectedText);

            return this;
        }

        public PersonRepositioningRecordPage ClickStaffRequiredLookupButton()
        {
            WaitForElementToBeClickable(StaffrequiredLookupButton);
            Click(StaffrequiredLookupButton);

            return this;
        }

        public PersonRepositioningRecordPage ValidateStaffRequiredSelectedOptionText(string OptionId, string ExpectedText)
        {
            WaitForElement(Staffrequired_SelectedOption(OptionId));
            ValidateElementText(Staffrequired_SelectedOption(OptionId), ExpectedText);

            return this;
        }

        public PersonRepositioningRecordPage ValidateStaffRequiredSelectedOptionText(Guid OptionId, string ExpectedText)
        {
            return ValidateStaffRequiredSelectedOptionText(OptionId.ToString(), ExpectedText);
        }

        public PersonRepositioningRecordPage ClickStaffRequiredSelectedOptionRemoveButton(string OptionId)
        {
            WaitForElementToBeClickable(Staffrequired_RemoveButton(OptionId));
            Click(Staffrequired_RemoveButton(OptionId));

            return this;
        }

        public PersonRepositioningRecordPage ClickStaffRequiredSelectedOptionRemoveButton(Guid OptionId)
        {
            return ClickStaffRequiredSelectedOptionRemoveButton(OptionId.ToString());
        }

        #endregion

        #region Care Needs

        //click linkedactivitiesofadllookupbutton 
        public PersonRepositioningRecordPage ClickLinkedActivitiesOfAdlLookupButton()
        {
            WaitForElement(LinkedActivitiesOfAdlLookupButton);
            ScrollToElement(LinkedActivitiesOfAdlLookupButton);
            Click(LinkedActivitiesOfAdlLookupButton);

            return this;
        }


        #endregion

        #region Handover

        public PersonRepositioningRecordPage VerifyIncludeInNextHandoverOptions(bool ExpectedDisplayed)
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

        public PersonRepositioningRecordPage VerifyFlagRecordForHandoverOptions(bool ExpectedDisplayed)
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

        public PersonRepositioningRecordPage SelectIncludeInNextHandoverOption(bool Option)
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

        public PersonRepositioningRecordPage SelectFlagRecordForHandoverOption(bool Option)
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
        public PersonRepositioningRecordPage VerifyIncludeInNextHandoverOptionSelected(bool ExpectedOption)
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
        public PersonRepositioningRecordPage VerifyFlagRecordForHandoverOptionSelected(bool ExpectedOption)
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
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonDailyRecord_RecordPage : CommonMethods
    {
        public PersonDailyRecord_RecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }
        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cppersondailyrecord&')]");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Person Daily Record: ']");
        readonly By SaveNCloseBtn = By.Id("TI_SaveAndCloseButton");
        readonly By SaveBtn = By.Id("TI_SaveButton");
        readonly By BackButton = By.Id("BackButton");


        #region General
        //readonly By occurred = By.XPath("//input[@id = 'CWField_occurred']");
        //readonly By occurredTime = By.XPath("//input[@id = 'CWField_occurred_Time']");

        readonly By PersonLookupFieldLink = By.Id("CWField_personid_Link");

        readonly By PreferencesTextareField = By.XPath("//*[@id = 'CWField_preferences']");

        readonly By TotalTimeSpentWithClientMinutesFieldLabel = By.XPath("//*[@id = 'CWLabelHolder_timespentwithclient']/label[text() = 'Total Time Spent With Person (Minutes)']");
        readonly By TotalTimeSpentWithClientMinutesField = By.Id("CWField_timespentwithclient");
        readonly By TotalTimeSpentWithClientMinutesFieldError = By.XPath("//label[@for = 'CWField_timespentwithclient'][@class = 'formerror']/span");

        readonly By DateAndTimeOccurredFieldLabel = By.Id("CWLabelHolder_occurred");
        readonly By DateAndTimeOccurred_DateField = By.Id("CWField_occurred");
        readonly By DateAndTimeOccurred_TimeField = By.Id("CWField_occurred_Time");
        readonly By DateAndTimeOccurred_DatePicker = By.Id("CWField_occurred_DatePicker");
        readonly By DateAndTimeOccurred_TimePicker = By.Id("CWField_occurred_Time_TimePicker");

        readonly By NotesTextareaField = By.Id("CWField_notes");
        readonly By LinkedActivitiesOfAdlLookupButton = By.Id("CWLookupBtn_linkedadlcategoriesid");
        readonly By IncludeInNextHandover_YesOption = By.Id("CWField_isincludeinnexthandover_1");
        readonly By IncludeInNextHandover_NoOption = By.Id("CWField_isincludeinnexthandover_0");
        readonly By FlagRecordForHandover_YesOption = By.Id("CWField_flagrecordforhandover_1");
        readonly By FlagRecordForHandover_NoOption = By.Id("CWField_flagrecordforhandover_0");

        #endregion


        public PersonDailyRecord_RecordPage WaitForPageToLoad()
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


        public PersonDailyRecord_RecordPage SetDateOccurred(String dateoccured)
        {
            ScrollToElement(DateAndTimeOccurred_DateField);
            WaitForElementVisible(DateAndTimeOccurred_DateField);
            SendKeys(DateAndTimeOccurred_DateField, dateoccured);


            return this;
        }

        //click DateAndTimeOccurred_DatePicker
        public PersonDailyRecord_RecordPage ClickDateAndTimeOccurredDatePicker()
        {
            WaitForElement(DateAndTimeOccurred_DatePicker);
            ScrollToElement(DateAndTimeOccurred_DatePicker);
            Click(DateAndTimeOccurred_DatePicker);

            return this;
        }

        public PersonDailyRecord_RecordPage SetTimeOccurred(String timeoccured)
        {
            WaitForElement(DateAndTimeOccurred_TimeField);
            System.Threading.Thread.Sleep(1000);
            ScrollToElement(DateAndTimeOccurred_TimeField);
            WaitForElementVisible(DateAndTimeOccurred_TimeField);
            Click(DateAndTimeOccurred_TimeField);
            ClearText(DateAndTimeOccurred_TimeField);
            System.Threading.Thread.Sleep(1000);
            SendKeys(DateAndTimeOccurred_TimeField, timeoccured + Keys.Tab);


            return this;
        }

        //click DateAndTimeOccurred_TimePicker
        public PersonDailyRecord_RecordPage ClickDateAndTimeOccurredTimePicker()
        {
            WaitForElement(DateAndTimeOccurred_TimePicker);
            ScrollToElement(DateAndTimeOccurred_TimePicker);
            Click(DateAndTimeOccurred_TimePicker);

            return this;
        }

        //verify personlookupfieldlinktext
        public PersonDailyRecord_RecordPage VerifyPersonLookupFieldLinkText(string ExpectedText)
        {
            ScrollToElement(PersonLookupFieldLink);
            WaitForElementVisible(PersonLookupFieldLink);
            ValidateElementByTitle(PersonLookupFieldLink, ExpectedText);

            return this;
        }

        //verify dateandtimeoccurredfieldlabel and dateandtimeoccurredfield is displayed or not displayed
        public PersonDailyRecord_RecordPage VerifyDateAndTimeOccurredFieldsAreDisplayed(bool ExpectedDisplayed)
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
        public PersonDailyRecord_RecordPage VerifyDateAndTimeOccurredDateFieldText(string ExpectedText)
        {
            ScrollToElement(DateAndTimeOccurred_DateField);
            WaitForElementVisible(DateAndTimeOccurred_DateField);
            ValidateElementValue(DateAndTimeOccurred_DateField, ExpectedText);

            return this;
        }

        //verify dateandtimeoccurred_timefield
        public PersonDailyRecord_RecordPage VerifyDateAndTimeOccurredTimeFieldText(string ExpectedText)
        {
            ScrollToElement(DateAndTimeOccurred_TimeField);
            WaitForElementVisible(DateAndTimeOccurred_TimeField);
            ValidateElementValue(DateAndTimeOccurred_TimeField, ExpectedText);

            return this;
        }

        //verify preferences textare field is displayed or not displayed
        public PersonDailyRecord_RecordPage VerifyPreferencesTextAreaFieldIsDisplayed(bool ExpectedDisplayed)
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
        public PersonDailyRecord_RecordPage VerifyPreferencesTextAreaFieldIsDisabled(bool ExpectedDisabled)
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

        //Insert text in notes textarea field
        public PersonDailyRecord_RecordPage InsertTextInNotesField(String TextToInsert)
        {
            ScrollToElement(NotesTextareaField);
            WaitForElementVisible(NotesTextareaField);
            SendKeys(NotesTextareaField, TextToInsert + Keys.Tab);

            return this;
        }

        //verify notes textarea field text
        public PersonDailyRecord_RecordPage VerifyNotesTextareaFieldText(string ExpectedText)
        {
            ScrollToElement(NotesTextareaField);
            WaitForElementVisible(NotesTextareaField);
            ValidateElementValue(NotesTextareaField, ExpectedText);

            return this;
        }


        #endregion

        #region Options Toolbar

        public PersonDailyRecord_RecordPage ClickSaveAndClose()
        {
            WaitForElementToBeClickable(SaveNCloseBtn);
            Click(SaveNCloseBtn);

            return this;
        }

        public PersonDailyRecord_RecordPage ClickSave()
        {
            WaitForElementToBeClickable(SaveBtn);
            Click(SaveBtn);

            return this;
        }

        public PersonDailyRecord_RecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        #endregion

        public PersonDailyRecord_RecordPage VerifyPageHeaderText(string ExpectedText)
        {
            ScrollToElement(pageHeader);
            WaitForElementVisible(pageHeader);
            string pageTitle = GetElementByAttributeValue(pageHeader, "title");
            Assert.AreEqual("Person Daily Record: " + ExpectedText, pageTitle);

            return this;
        }

        #region Care Needs

        //click linkedactivitiesofadllookupbutton 
        public PersonDailyRecord_RecordPage ClickLinkedActivitiesOfAdlLookupButton()
        {
            WaitForElement(LinkedActivitiesOfAdlLookupButton);
            ScrollToElement(LinkedActivitiesOfAdlLookupButton);
            Click(LinkedActivitiesOfAdlLookupButton);

            return this;
        }


        #endregion

        #region Handover

        public PersonDailyRecord_RecordPage VerifyIncludeInNextHandoverOptions(bool ExpectedDisplayed)
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

        public PersonDailyRecord_RecordPage VerifyFlagRecordForHandoverOptions(bool ExpectedDisplayed)
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

        public PersonDailyRecord_RecordPage SelectIncludeInNextHandoverOption(bool Option)
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

        public PersonDailyRecord_RecordPage SelectFlagRecordForHandoverOption(bool Option)
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
        public PersonDailyRecord_RecordPage VerifyIncludeInNextHandoverOptionSelected(bool ExpectedOption)
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
        public PersonDailyRecord_RecordPage VerifyFlagRecordForHandoverOptionSelected(bool ExpectedOption)
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

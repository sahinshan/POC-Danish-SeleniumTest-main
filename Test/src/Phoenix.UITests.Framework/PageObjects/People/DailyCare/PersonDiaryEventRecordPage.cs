using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonDiaryEventRecordPage : CommonMethods
    {
        public PersonDiaryEventRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cppersondiaryevent&')]");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
        readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
        readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Person Diary Event: ']");

        readonly By TopPageNotification = By.XPath("//*[@id='CWNotificationMessage_DataForm']");

        #region General section

        readonly By PersonidLink = By.XPath("//*[@id='CWField_personid_Link']");
        readonly By PersonidLookupButton = By.XPath("//*[@id='CWLookupBtn_personid']");
        readonly By DiaryeventtypeidLink = By.XPath("//*[@id='CWField_diaryeventtypeid_Link']");
        readonly By DiaryeventtypeidClearButton = By.XPath("//*[@id='CWClearLookup_diaryeventtypeid']");
        readonly By DiaryeventtypeidLookupButton = By.XPath("//*[@id='CWLookupBtn_diaryeventtypeid']");
        readonly By Diaryeventtypeid_ErrorLabel = By.XPath("//*[@id='CWControlHolder_diaryeventtypeid']/label/span");
        readonly By diaryeventtypeother = By.XPath("//*[@id='CWField_diaryeventtypeother']");
        readonly By diaryeventtypeother_ErrorLabel = By.XPath("//*[@id='CWControlHolder_diaryeventtypeother']/label/span");
        readonly By Startdatetime = By.XPath("//*[@id='CWField_startdatetime']");
        readonly By StartdatetimeDatePicker = By.XPath("//*[@id='CWField_startdatetime_DatePicker']");
        readonly By Startdatetime_Time = By.XPath("//*[@id='CWField_startdatetime_Time']");
        readonly By Startdatetime_Time_TimePicker = By.XPath("//*[@id='CWField_startdatetime_Time_TimePicker']");
        readonly By startdatetime_ErrorLabel = By.XPath("//*[@id='CWControlHolder_startdatetime']//label/span");

        readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By Createdon = By.XPath("//*[@id='CWField_createdon']");
        readonly By CreatedonDatePicker = By.XPath("//*[@id='CWField_createdon_DatePicker']");
        readonly By Createdon_Time = By.XPath("//*[@id='CWField_createdon_Time']");
        readonly By Createdon_Time_TimePicker = By.XPath("//*[@id='CWField_createdon_Time_TimePicker']");
        readonly By Enddatetime = By.XPath("//*[@id='CWField_enddatetime']");
        readonly By EnddatetimeDatePicker = By.XPath("//*[@id='CWField_enddatetime_DatePicker']");
        readonly By Enddatetime_Time = By.XPath("//*[@id='CWField_enddatetime_Time']");
        readonly By Enddatetime_Time_TimePicker = By.XPath("//*[@id='CWField_enddatetime_Time_TimePicker']");

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


        public PersonDiaryEventRecordPage WaitForPageToLoad()
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


        public PersonDiaryEventRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public PersonDiaryEventRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public PersonDiaryEventRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public PersonDiaryEventRecordPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public PersonDiaryEventRecordPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }

        public PersonDiaryEventRecordPage ValidateTopPageNotificationText(string ExpectedText)
        {
            ValidateElementText(TopPageNotification, ExpectedText);

            return this;
        }

        public PersonDiaryEventRecordPage ValidateTopPageNotificationVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(TopPageNotification);
            else
                WaitForElementNotVisible(TopPageNotification, 3);

            return this;
        }

        public PersonDiaryEventRecordPage ClickPersonLink()
        {
            WaitForElementToBeClickable(PersonidLink);
            Click(PersonidLink);

            return this;
        }

        public PersonDiaryEventRecordPage ValidatePersonLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(PersonidLink);
            ValidateElementText(PersonidLink, ExpectedText);

            return this;
        }

        public PersonDiaryEventRecordPage ClickPersonLookupButton()
        {
            WaitForElementToBeClickable(PersonidLookupButton);
            Click(PersonidLookupButton);

            return this;
        }

        public PersonDiaryEventRecordPage ClickDiaryEventTypeLink()
        {
            WaitForElementToBeClickable(DiaryeventtypeidLink);
            Click(DiaryeventtypeidLink);

            return this;
        }

        public PersonDiaryEventRecordPage ValidateDiaryEventTypeLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(DiaryeventtypeidLink);
            ValidateElementText(DiaryeventtypeidLink, ExpectedText);

            return this;
        }

        public PersonDiaryEventRecordPage ClickDiaryEventTypeClearButton()
        {
            WaitForElementToBeClickable(DiaryeventtypeidClearButton);
            Click(DiaryeventtypeidClearButton);

            return this;
        }

        public PersonDiaryEventRecordPage ClickDiaryEventTypeLookupButton()
        {
            WaitForElementToBeClickable(DiaryeventtypeidLookupButton);
            Click(DiaryeventtypeidLookupButton);

            return this;
        }

        public PersonDiaryEventRecordPage ValidateDiaryEventTypeErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Diaryeventtypeid_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonDiaryEventRecordPage ValidateDiaryEventTypeErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Diaryeventtypeid_ErrorLabel);
            else
                WaitForElementNotVisible(Diaryeventtypeid_ErrorLabel, 3);

            return this;
        }

        public PersonDiaryEventRecordPage ValidateDiaryEventTypeOtherText(string ExpectedText)
        {
            ValidateElementValue(diaryeventtypeother, ExpectedText);

            return this;
        }

        public PersonDiaryEventRecordPage InsertTextOnDiaryEventTypeOther(string TextToInsert)
        {
            WaitForElementToBeClickable(diaryeventtypeother);
            SendKeys(diaryeventtypeother, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonDiaryEventRecordPage ValidateDiaryEventTypeOtherFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(diaryeventtypeother);
            else
                WaitForElementNotVisible(diaryeventtypeother, 3);

            return this;
        }

        public PersonDiaryEventRecordPage ValidateDiaryEventTypeOtherErrorLabelText(string ExpectedText)
        {
            ValidateElementText(diaryeventtypeother_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonDiaryEventRecordPage ValidateDiaryEventTypeOtherErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(diaryeventtypeother_ErrorLabel);
            else
                WaitForElementNotVisible(diaryeventtypeother_ErrorLabel, 3);

            return this;
        }

        public PersonDiaryEventRecordPage ValidateStartDateTimeText(string ExpectedText)
        {
            ValidateElementValue(Startdatetime, ExpectedText);

            return this;
        }

        public PersonDiaryEventRecordPage InsertTextOnStartDateTime(string TextToInsert)
        {
            WaitForElementToBeClickable(Startdatetime);
            SendKeys(Startdatetime, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonDiaryEventRecordPage ClickStartDateTimeDatePicker()
        {
            WaitForElementToBeClickable(StartdatetimeDatePicker);
            Click(StartdatetimeDatePicker);

            return this;
        }

        public PersonDiaryEventRecordPage ValidateStartDateTime_TimeText(string ExpectedText)
        {
            ValidateElementValue(Startdatetime_Time, ExpectedText);

            return this;
        }

        public PersonDiaryEventRecordPage InsertTextOnStartDateTime_Time(string TextToInsert)
        {
            WaitForElementToBeClickable(Startdatetime_Time);
            SendKeys(Startdatetime_Time, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonDiaryEventRecordPage ClickStartDateTime_Time_TimePicker()
        {
            WaitForElementToBeClickable(Startdatetime_Time_TimePicker);
            Click(Startdatetime_Time_TimePicker);

            return this;
        }

        public PersonDiaryEventRecordPage ValidateStartDateTimeErrorLabelText(string ExpectedText)
        {
            ValidateElementText(startdatetime_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonDiaryEventRecordPage ValidateStartDateTimeErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(startdatetime_ErrorLabel);
            else
                WaitForElementNotVisible(startdatetime_ErrorLabel, 3);

            return this;
        }

        public PersonDiaryEventRecordPage ClickResponsibleTeamLink()
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            Click(ResponsibleTeamLink);

            return this;
        }

        public PersonDiaryEventRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            ValidateElementText(ResponsibleTeamLink, ExpectedText);

            return this;
        }

        public PersonDiaryEventRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamLookupButton);
            Click(ResponsibleTeamLookupButton);

            return this;
        }

        public PersonDiaryEventRecordPage ValidateDateCreatedText(string ExpectedText)
        {
            ValidateElementValue(Createdon, ExpectedText);

            return this;
        }

        public PersonDiaryEventRecordPage InsertTextOnDateCreated(string TextToInsert)
        {
            WaitForElementToBeClickable(Createdon);
            SendKeys(Createdon, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonDiaryEventRecordPage ClickDateCreatedDatePicker()
        {
            WaitForElementToBeClickable(CreatedonDatePicker);
            Click(CreatedonDatePicker);

            return this;
        }

        public PersonDiaryEventRecordPage ValidateDateCreated_TimeText(string ExpectedText)
        {
            ValidateElementValue(Createdon_Time, ExpectedText);

            return this;
        }

        public PersonDiaryEventRecordPage InsertTextOnDateCreated_Time(string TextToInsert)
        {
            WaitForElementToBeClickable(Createdon_Time);
            SendKeys(Createdon_Time, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonDiaryEventRecordPage ClickDateCreated_Time_TimePicker()
        {
            WaitForElementToBeClickable(Createdon_Time_TimePicker);
            Click(Createdon_Time_TimePicker);

            return this;
        }

        public PersonDiaryEventRecordPage ValidateEnddatetimeText(string ExpectedText)
        {
            ValidateElementValue(Enddatetime, ExpectedText);

            return this;
        }

        public PersonDiaryEventRecordPage InsertTextOnEndDateTime(string TextToInsert)
        {
            WaitForElementToBeClickable(Enddatetime);
            SendKeys(Enddatetime, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonDiaryEventRecordPage ClickEndDateTimeDatePicker()
        {
            WaitForElementToBeClickable(EnddatetimeDatePicker);
            Click(EnddatetimeDatePicker);

            return this;
        }

        public PersonDiaryEventRecordPage ValidateEndDateTime_TimeText(string ExpectedText)
        {
            ValidateElementValue(Enddatetime_Time, ExpectedText);

            return this;
        }

        public PersonDiaryEventRecordPage InsertTextOnEndDateTime_Time(string TextToInsert)
        {
            WaitForElementToBeClickable(Enddatetime_Time);
            SendKeys(Enddatetime_Time, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonDiaryEventRecordPage ClickEndDateTime_Time_TimePicker()
        {
            WaitForElementToBeClickable(Enddatetime_Time_TimePicker);
            Click(Enddatetime_Time_TimePicker);

            return this;
        }

        public PersonDiaryEventRecordPage ValidateCareNoteText(string ExpectedText)
        {
            ValidateElementText(Carenote, ExpectedText);

            return this;
        }

        public PersonDiaryEventRecordPage InsertTextOnCareNote(string TextToInsert)
        {
            WaitForElementToBeClickable(Carenote);
            SendKeys(Carenote, TextToInsert + Keys.Tab);
            return this;
        }

        public PersonDiaryEventRecordPage ClickinkedActivitiesOfDailyLivingLookupButton()
        {
            WaitForElementToBeClickable(LinkedadlcategoriesidLookupButton);
            Click(LinkedadlcategoriesidLookupButton);

            return this;
        }

        public PersonDiaryEventRecordPage ClickIncludeInNextHandover_YesRadioButton()
        {
            WaitForElementToBeClickable(Isincludeinnexthandover_1);
            Click(Isincludeinnexthandover_1);

            return this;
        }

        public PersonDiaryEventRecordPage ValidateIncludeInNextHandover_YesRadioButtonChecked()
        {
            WaitForElement(Isincludeinnexthandover_1);
            ValidateElementChecked(Isincludeinnexthandover_1);

            return this;
        }

        public PersonDiaryEventRecordPage ValidateIncludeInNextHandover_YesRadioButtonNotChecked()
        {
            WaitForElement(Isincludeinnexthandover_1);
            ValidateElementNotChecked(Isincludeinnexthandover_1);

            return this;
        }

        public PersonDiaryEventRecordPage ClickIncludeInNextHandover_NoRadioButton()
        {
            WaitForElementToBeClickable(Isincludeinnexthandover_0);
            Click(Isincludeinnexthandover_0);

            return this;
        }

        public PersonDiaryEventRecordPage ValidateIncludeInNextHandover_NoRadioButtonChecked()
        {
            WaitForElement(Isincludeinnexthandover_0);
            ValidateElementChecked(Isincludeinnexthandover_0);

            return this;
        }

        public PersonDiaryEventRecordPage ValidateIncludeInNextHandover_NoRadioButtonNotChecked()
        {
            WaitForElement(Isincludeinnexthandover_0);
            ValidateElementNotChecked(Isincludeinnexthandover_0);

            return this;
        }

        public PersonDiaryEventRecordPage ClickFlagRecordForHandover_YesRadioButton()
        {
            WaitForElementToBeClickable(Flagrecordforhandover_1);
            Click(Flagrecordforhandover_1);

            return this;
        }

        public PersonDiaryEventRecordPage ValidateFlagRecordForHandover_YesRadioButtonChecked()
        {
            WaitForElement(Flagrecordforhandover_1);
            ValidateElementChecked(Flagrecordforhandover_1);

            return this;
        }

        public PersonDiaryEventRecordPage ValidateFlagRecordForHandover_YesRadioButtonNotChecked()
        {
            WaitForElement(Flagrecordforhandover_1);
            ValidateElementNotChecked(Flagrecordforhandover_1);

            return this;
        }

        public PersonDiaryEventRecordPage ClickFlagRecordForHandover_NoRadioButton()
        {
            WaitForElementToBeClickable(Flagrecordforhandover_0);
            Click(Flagrecordforhandover_0);

            return this;
        }

        public PersonDiaryEventRecordPage ValidateFlagRecordForHandover_NoRadioButtonChecked()
        {
            WaitForElement(Flagrecordforhandover_0);
            ValidateElementChecked(Flagrecordforhandover_0);

            return this;
        }

        public PersonDiaryEventRecordPage ValidateFlagRecordForHandover_NoRadioButtonNotChecked()
        {
            WaitForElement(Flagrecordforhandover_0);
            ValidateElementNotChecked(Flagrecordforhandover_0);

            return this;
        }

    }
}


using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class PersonContractServiceRatePeriodRecordPage : CommonMethods
    {
        public PersonContractServiceRatePeriodRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderpersoncontractservicerateperiod')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By recordTitle = By.XPath("//*[@id='CWToolbar']/div/h1/span");

        readonly By BackButton = By.Id("BackButton");
        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
        readonly By auditSubMenuLink = By.XPath("//*[@id='CWNavItem_AuditHistory']");


        readonly By DetailsTab = By.XPath("//*[@id='CWNavGroup_EditForm']/a");

        readonly By NotificationArea = By.XPath("//*[@id='CWNotificationHolder_DataForm']");

        #region Genereal

        readonly By GeneralSectionTitle = By.XPath("//*[@id='CWSection_General']//span[text()='General']");

        readonly By PersonContractService_LabelField = By.XPath("//*[@id='CWLabelHolder_careproviderpersoncontractserviceid']/label[text()='Person Contract Service']");
        readonly By PersonContractService_MandatoryField = By.XPath("//*[@id='CWLabelHolder_careproviderpersoncontractserviceid']/label/span[@class='mandatory']");
        readonly By PersonContractService_LinkText = By.Id("CWField_careproviderpersoncontractserviceid_Link");
        readonly By PersonContractService_LookupButton = By.Id("CWLookupBtn_careproviderpersoncontractserviceid");
        readonly By PersonContractService_RemoveButton = By.Id("CWClearLookup_careproviderpersoncontractserviceid");

        readonly By ResponsibleTeam_LabelField = By.XPath("//*[@id='CWLabelHolder_ownerid']/label[text()='Responsible Team']");
        readonly By ResponsibleTeam_MandatoryField = By.XPath("//*[@id='CWLabelHolder_ownerid']/label/span[@class='mandatory']");
        readonly By ResponsibleTeam_LinkText = By.Id("CWField_ownerid_Link");
        readonly By ResponsibleTeam_LookupButton = By.Id("CWLookupBtn_ownerid");

        readonly By StartDate_LabelField = By.XPath("//*[@id='CWLabelHolder_startdate']/label[text()='Start Date']");
        readonly By StartDate_MandatoryField = By.XPath("//*[@id='CWLabelHolder_startdate']/label/span[@class='mandatory']");
        readonly By StartDate_Field = By.Id("CWField_startdate");
        readonly By StartDate_DatePicker = By.Id("CWField_startdate_DatePicker");

        readonly By EndDate_LabelField = By.XPath("//*[@id='CWLabelHolder_enddate']/label[text()='End Date']");
        readonly By EndDate_MandatoryField = By.XPath("//*[@id='CWLabelHolder_enddate']/label/span[@class='mandatory']");
        readonly By EndDate_Field = By.Id("CWField_enddate");
        readonly By EndDate_DatePicker = By.Id("CWField_enddate_DatePicker");

        readonly By Rate_LabelField = By.XPath("//*[@id='CWLabelHolder_rate']/label[text()='Rate']");
        readonly By Rate_MandatoryField = By.XPath("//*[@id='CWLabelHolder_rate']/label/span[@class='mandatory']");
        readonly By Rate_Field = By.Id("CWField_rate");
        readonly By Rate_ErrorLabel = By.XPath("//*[@id='CWControlHolder_rate']/label/span");

        readonly By RateUnit_LabelField = By.XPath("//*[@id='CWLabelHolder_careproviderrateunitid']/label[text()='Rate Unit']");
        readonly By RateUnit_MandatoryField = By.XPath("//*[@id='CWLabelHolder_careproviderrateunitid']/label/span[@class='mandatory']");
        readonly By RateUnit_LinkText = By.Id("CWField_careproviderrateunitid_Link");
        readonly By RateUnit_LookupButton = By.Id("CWLookupBtn_careproviderrateunitid");
        readonly By SelectAllDays_1 = By.XPath("//*[@id='CWField_rateappliestoselectall_1']");
        readonly By SelectAllDays_0 = By.XPath("//*[@id='CWField_rateappliestoselectall_0']");
        readonly By Monday_1 = By.XPath("//*[@id='CWField_rateappliestomonday_1']");
        readonly By Monday_0 = By.XPath("//*[@id='CWField_rateappliestomonday_0']");
        readonly By Tuesday_1 = By.XPath("//*[@id='CWField_rateappliestotuesday_1']");
        readonly By Tuesday_0 = By.XPath("//*[@id='CWField_rateappliestotuesday_0']");
        readonly By Wednesday_1 = By.XPath("//*[@id='CWField_rateappliestowednesday_1']");
        readonly By Wednesday_0 = By.XPath("//*[@id='CWField_rateappliestowednesday_0']");
        readonly By Thursday_1 = By.XPath("//*[@id='CWField_rateappliestothursday_1']");
        readonly By Thursday_0 = By.XPath("//*[@id='CWField_rateappliestothursday_0']");
        readonly By Friday_1 = By.XPath("//*[@id='CWField_rateappliestofriday_1']");
        readonly By Friday_0 = By.XPath("//*[@id='CWField_rateappliestofriday_0']");
        readonly By Saturday_1 = By.XPath("//*[@id='CWField_rateappliestosaturday_1']");
        readonly By Saturday_0 = By.XPath("//*[@id='CWField_rateappliestosaturday_0']");
        readonly By Sunday_1 = By.XPath("//*[@id='CWField_rateappliestosunday_1']");
        readonly By Sunday_0 = By.XPath("//*[@id='CWField_rateappliestosunday_0']");
        readonly By TimeBandStart_Field = By.Id("CWField_timebandstart");
        readonly By TimeBandEnd_Field = By.Id("CWField_timebandend");

        #endregion

        #region Notes

        readonly By NotesSectionTitle = By.XPath("//*[@id='CWSection_Notes']//span[text()='Notes']");
        readonly By NoteText_LabelField = By.XPath("//*[@id='CWLabelHolder_notetext']/label[text()='Note Text']");
        readonly By NoteText_MandatoryField = By.XPath("//*[@id='CWLabelHolder_notetext']/label/span[@class='mandatory']");
        readonly By NoteText_Field = By.Id("CWField_notetext");

        #endregion


        public PersonContractServiceRatePeriodRecordPage WaitForPersonContractServiceRatePeriodRecordPageToLoad()
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);

            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(pageHeader);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(BackButton);

            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidatePageElementsVisible()
        {
            WaitForElementVisible(PersonContractService_LookupButton);
            WaitForElementVisible(StartDate_Field);
            WaitForElementVisible(Rate_Field);

            WaitForElementVisible(ResponsibleTeam_LookupButton);
            WaitForElementVisible(EndDate_Field);
            WaitForElementVisible(RateUnit_LookupButton);

            WaitForElementVisible(NoteText_Field);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateNoteTextSection()
        {
            WaitForElementVisible(NotesSectionTitle);
            ScrollToElement(NotesSectionTitle);

            WaitForElementVisible(NoteText_LabelField);
            WaitForElementVisible(NoteText_Field);
            ScrollToElement(NoteText_Field);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidatePageHeaderText(string ExpectedText)
        {
            WaitForElementVisible(recordTitle);
            ValidateElementText(recordTitle, ExpectedText);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage NavigateToAuditPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(auditSubMenuLink);
            Click(auditSubMenuLink);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ClickPersonContractServiceLookupButton()
        {
            WaitForElementToBeClickable(PersonContractService_LookupButton);
            ScrollToElement(PersonContractService_LookupButton);
            Click(PersonContractService_LookupButton);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_LookupButton);
            Click(ResponsibleTeam_LookupButton);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);
            WaitForElementVisible(AssignRecordButton);
            WaitForElementVisible(DeleteRecordButton);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage InsertStartDate(string StartDate)
        {
            WaitForElementToBeClickable(StartDate_Field);
            SendKeys(StartDate_Field, StartDate + Keys.Tab);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage InsertEndDate(string EndDate)
        {
            WaitForElementToBeClickable(EndDate_Field);
            SendKeys(EndDate_Field, EndDate + Keys.Tab);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage InsertRate(string TextToInsert)
        {
            WaitForElementToBeClickable(Rate_Field);
            SendKeys(Rate_Field, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage InsertTextOnNoteText(string TextToInsert)
        {
            WaitForElementToBeClickable(NoteText_Field);
            SendKeys(NoteText_Field, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateNotificationAreaVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(NotificationArea);
            else
                WaitForElementNotVisible(NotificationArea, 3);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateNotificationAreaText(string ExpectedText)
        {
            ValidateElementText(NotificationArea, ExpectedText);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidatePersonContractServiceLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(PersonContractService_LinkText);
            ValidateElementText(PersonContractService_LinkText, ExpectedText);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeam_LinkText);
            ValidateElementText(ResponsibleTeam_LinkText, ExpectedText);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateStartDateFieldText(string ExpectedText)
        {
            WaitForElementToBeClickable(StartDate_Field);
            ValidateElementValue(StartDate_Field, ExpectedText);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateEndDateFieldText(string ExpectedText)
        {
            WaitForElementToBeClickable(EndDate_Field);
            ValidateElementValue(EndDate_Field, ExpectedText);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateRateFieldText(string ExpectedText)
        {
            WaitForElementVisible(Rate_Field);
            ValidateElementValue(Rate_Field, ExpectedText);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateRateUnitLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(RateUnit_LinkText);
            ScrollToElement(RateUnit_LinkText);
            ValidateElementText(RateUnit_LinkText, ExpectedText);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateNoteTextText(string ExpectedText)
        {
            ValidateElementValue(NoteText_Field, ExpectedText);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidatePersonContractServiceMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(PersonContractService_LabelField);
            ScrollToElement(PersonContractService_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(PersonContractService_MandatoryField);
            else
                WaitForElementNotVisible(PersonContractService_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateResponsibleTeamMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(ResponsibleTeam_LabelField);
            ScrollToElement(ResponsibleTeam_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(ResponsibleTeam_LabelField);
            else
                WaitForElementNotVisible(ResponsibleTeam_LabelField, 3);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateStartDateMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(StartDate_LabelField);
            ScrollToElement(StartDate_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(StartDate_MandatoryField);
            else
                WaitForElementNotVisible(StartDate_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateEndDateMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(EndDate_LabelField);
            ScrollToElement(EndDate_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(EndDate_MandatoryField);
            else
                WaitForElementNotVisible(EndDate_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateRateMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(Rate_LabelField);
            ScrollToElement(Rate_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(Rate_MandatoryField);
            else
                WaitForElementNotVisible(Rate_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateRateErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Rate_ErrorLabel);
            else
                WaitForElementNotVisible(Rate_ErrorLabel, 3);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateRateUnitMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(RateUnit_LabelField);
            ScrollToElement(RateUnit_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(RateUnit_MandatoryField);
            else
                WaitForElementNotVisible(RateUnit_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidatePersonContractServiceLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(PersonContractService_LookupButton);
            ScrollToElement(PersonContractService_LookupButton);

            if (IsDisabled)
                ValidateElementDisabled(PersonContractService_LookupButton);
            else
                ValidateElementNotDisabled(PersonContractService_LookupButton);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateResponsibleTeamLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(ResponsibleTeam_LookupButton);
            ScrollToElement(ResponsibleTeam_LookupButton);

            if (IsDisabled)
                ValidateElementDisabled(ResponsibleTeam_LookupButton);
            else
                ValidateElementNotDisabled(ResponsibleTeam_LookupButton);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateStartDateFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(StartDate_Field);
            ScrollToElement(StartDate_Field);

            if (IsDisabled)
                ValidateElementDisabled(StartDate_Field);
            else
                ValidateElementNotDisabled(StartDate_Field);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateEndDateFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(EndDate_Field);
            ScrollToElement(EndDate_Field);

            if (IsDisabled)
                ValidateElementDisabled(EndDate_Field);
            else
                ValidateElementNotDisabled(EndDate_Field);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateRateFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(Rate_Field);
            ScrollToElement(Rate_Field);

            if (IsDisabled)
                ValidateElementDisabled(Rate_Field);
            else
                ValidateElementNotDisabled(Rate_Field);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateRateUnitLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(RateUnit_LookupButton);
            ScrollToElement(RateUnit_LookupButton);

            if (IsDisabled)
                ValidateElementDisabled(RateUnit_LookupButton);
            else
                ValidateElementNotDisabled(RateUnit_LookupButton);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateNoteTextFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(NoteText_Field);
            ScrollToElement(NoteText_Field);

            if (IsDisabled)
                ValidateElementDisabled(NoteText_Field);
            else
                ValidateElementNotDisabled(NoteText_Field);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateRateAppliesToDays_NoRadioButtonCheckedForSelectAllDays(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementChecked(SelectAllDays_0);
            else
                ValidateElementChecked(SelectAllDays_1);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateRateAppliesToDays_NoRadioButtonCheckedForMonday(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementChecked(Monday_0);
            else
                ValidateElementChecked(Monday_1);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateRateAppliesToDays_NoRadioButtonCheckedForTuesday(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementChecked(Tuesday_0);
            else
                ValidateElementChecked(Tuesday_1);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateRateAppliesToDays_NoRadioButtonCheckedForWednesday(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementChecked(Wednesday_0);
            else
                ValidateElementChecked(Wednesday_1);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateRateAppliesToDays_NoRadioButtonCheckedForThursday(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementChecked(Thursday_0);
            else
                ValidateElementChecked(Thursday_1);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateRateAppliesToDays_NoRadioButtonCheckedForFriday(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementChecked(Friday_0);
            else
                ValidateElementChecked(Friday_1);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateRateAppliesToDays_NoRadioButtonCheckedForSaturday(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementChecked(Saturday_0);
            else
                ValidateElementChecked(Saturday_1);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateRateAppliesToDays_NoRadioButtonCheckedForSunday(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementChecked(Sunday_0);
            else
                ValidateElementChecked(Sunday_1);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ClickYesRadioButtonForSelectAllDays()
        {
            WaitForElementToBeClickable(SelectAllDays_1);
            ScrollToElement(SelectAllDays_1);
            Click(SelectAllDays_1);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateTimeBandEndValueByJavaScript(string ExpectedValue)
        {
            ScrollToElement(TimeBandEnd_Field);
            ValidateElementValueByJavascript("CWField_timebandend", ExpectedValue);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateTimeBandStartValueByJavaScript(string ExpectedValue)
        {
            ScrollToElement(TimeBandStart_Field);
            ValidateElementValueByJavascript("CWField_timebandstart", ExpectedValue);

            return this;
        }

        public PersonContractServiceRatePeriodRecordPage ValidateRateErrorLabelText(string ExpectedValue)
        {
            ValidateElementText(Rate_ErrorLabel, ExpectedValue);

            return this;
        }
    }
}

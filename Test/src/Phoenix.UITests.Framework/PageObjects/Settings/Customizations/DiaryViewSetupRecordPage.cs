using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Customizations
{
    public class DiaryViewSetupRecordPage : CommonMethods
    {

        public DiaryViewSetupRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=communityclinicdiaryviewsetup&')]");


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
        readonly By AuditLinkButton = By.XPath("//*[@id='CWNavItem_AuditHistory']");
        readonly By LinkedProfessionalLinkButton = By.XPath("//*[@id='CWNavItem_LinkedProfessional']");
        readonly By RestrictionsLinkButton = By.XPath("//*[@id='CWNavItem_Restrictions']");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
        readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
        readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");

        readonly By Title = By.XPath("//*[@id='CWField_title']");
        readonly By CommunityandclinicteamidLink = By.XPath("//*[@id='CWField_communityandclinicteamid_Link']");
        readonly By CommunityandclinicteamidLookupButton = By.XPath("//*[@id='CWLookupBtn_communityandclinicteamid']");
        readonly By Startdate = By.XPath("//*[@id='CWField_startdate']");
        readonly By StartdateDatePicker = By.XPath("//*[@id='CWField_startdate_DatePicker']");
        readonly By Enddate = By.XPath("//*[@id='CWField_enddate']");
        readonly By EnddateDatePicker = By.XPath("//*[@id='CWField_enddate_DatePicker']");
        readonly By Homevisit_1 = By.XPath("//*[@id='CWField_homevisit_1']");
        readonly By Homevisit_0 = By.XPath("//*[@id='CWField_homevisit_0']");
        readonly By Availableonbankholidays_1 = By.XPath("//*[@id='CWField_availableonbankholidays_1']");
        readonly By Availableonbankholidays_0 = By.XPath("//*[@id='CWField_availableonbankholidays_0']");
        readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By Individualsperday = By.XPath("//*[@id='CWField_individualsperday']");
        readonly By Starttime = By.XPath("//*[@id='CWField_starttime']");
        readonly By Starttime_TimePicker = By.XPath("//*[@id='CWField_starttime_TimePicker']");
        readonly By Endtime = By.XPath("//*[@id='CWField_endtime']");
        readonly By Endtime_TimePicker = By.XPath("//*[@id='CWField_endtime_TimePicker']");
        readonly By Groupbookingallowed_1 = By.XPath("//*[@id='CWField_groupbookingallowed_1']");
        readonly By Groupbookingallowed_0 = By.XPath("//*[@id='CWField_groupbookingallowed_0']");
        readonly By Groupsperday = By.XPath("//*[@id='CWField_groupsperday']");
        readonly By Individualspergroup = By.XPath("//*[@id='CWField_individualspergroup']");


        public DiaryViewSetupRecordPage WaitForDiaryViewSetupRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElementVisible(pageHeader);

            return this;
        }


        public DiaryViewSetupRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public DiaryViewSetupRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public DiaryViewSetupRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public DiaryViewSetupRecordPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public DiaryViewSetupRecordPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }



        public DiaryViewSetupRecordPage ValidateTitleText(string ExpectedText)
        {
            WaitForElementVisible(Title);
            ValidateElementValue(Title, ExpectedText);

            return this;
        }

        public DiaryViewSetupRecordPage InsertTextOnTitle(string TextToInsert)
        {
            WaitForElementToBeClickable(Title);
            SendKeys(Title, TextToInsert);

            return this;
        }

        public DiaryViewSetupRecordPage ClickCommunityClinicTeamLink()
        {
            WaitForElementToBeClickable(CommunityandclinicteamidLink);
            Click(CommunityandclinicteamidLink);

            return this;
        }

        public DiaryViewSetupRecordPage ValidateCommunityClinicTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CommunityandclinicteamidLink);
            ValidateElementText(CommunityandclinicteamidLink, ExpectedText);

            return this;
        }

        public DiaryViewSetupRecordPage ClickCommunityClinicTeamLookupButton()
        {
            WaitForElementToBeClickable(CommunityandclinicteamidLookupButton);
            Click(CommunityandclinicteamidLookupButton);

            return this;
        }

        public DiaryViewSetupRecordPage ValidateStartDateText(string ExpectedText)
        {
            WaitForElementVisible(Startdate);
            ValidateElementValue(Startdate, ExpectedText);

            return this;
        }

        public DiaryViewSetupRecordPage InsertTextOnStartDate(string TextToInsert)
        {
            WaitForElementToBeClickable(Startdate);
            SendKeys(Startdate, TextToInsert);

            return this;
        }

        public DiaryViewSetupRecordPage ClickStartDateDatePicker()
        {
            WaitForElementToBeClickable(StartdateDatePicker);
            Click(StartdateDatePicker);

            return this;
        }

        public DiaryViewSetupRecordPage ValidateEndDateText(string ExpectedText)
        {
            WaitForElementVisible(Enddate);
            ValidateElementValue(Enddate, ExpectedText);

            return this;
        }

        public DiaryViewSetupRecordPage InsertTextOnEndDate(string TextToInsert)
        {
            WaitForElementToBeClickable(Enddate);
            SendKeys(Enddate, TextToInsert + Keys.Tab);

            return this;
        }

        public DiaryViewSetupRecordPage ClickEndDateDatePicker()
        {
            WaitForElementToBeClickable(EnddateDatePicker);
            Click(EnddateDatePicker);

            return this;
        }

        public DiaryViewSetupRecordPage ClickHomeVisit_YesRadioButton()
        {
            WaitForElementToBeClickable(Homevisit_1);
            Click(Homevisit_1);

            return this;
        }

        public DiaryViewSetupRecordPage ValidateHomeVisit_YesRadioButtonChecked()
        {
            WaitForElement(Homevisit_1);
            ValidateElementChecked(Homevisit_1);

            return this;
        }

        public DiaryViewSetupRecordPage ValidateHomeVisit_YesRadioButtonNotChecked()
        {
            WaitForElement(Homevisit_1);
            ValidateElementNotChecked(Homevisit_1);

            return this;
        }

        public DiaryViewSetupRecordPage ValidateHomeVisit_YesRadioButtonDisabled(bool ExpectDisabled)
        {
            WaitForElement(Homevisit_1);
            if (ExpectDisabled)
                ValidateElementDisabled(Homevisit_1);
            else
                ValidateElementNotDisabled(Homevisit_1);

            return this;
        }

        public DiaryViewSetupRecordPage ClickHomeVisit_NoRadioButton()
        {
            WaitForElementToBeClickable(Homevisit_0);
            Click(Homevisit_0);

            return this;
        }

        public DiaryViewSetupRecordPage ValidateHomeVisit_NoRadioButtonChecked()
        {
            WaitForElement(Homevisit_0);
            ValidateElementChecked(Homevisit_0);

            return this;
        }

        public DiaryViewSetupRecordPage ValidateHomeVisit_NoRadioButtonNotChecked()
        {
            WaitForElement(Homevisit_0);
            ValidateElementNotChecked(Homevisit_0);

            return this;
        }

        public DiaryViewSetupRecordPage ValidateHomeVisit_NoRadioButtonDisabled(bool ExpectDisabled)
        {
            WaitForElement(Homevisit_0);
            if (ExpectDisabled)
                ValidateElementDisabled(Homevisit_0);
            else
                ValidateElementNotDisabled(Homevisit_0);

            return this;
        }

        public DiaryViewSetupRecordPage ClickAvailableOnBankHolidays_YesRadioButton()
        {
            WaitForElementToBeClickable(Availableonbankholidays_1);
            Click(Availableonbankholidays_1);

            return this;
        }

        public DiaryViewSetupRecordPage ValidateAvailableOnBankHolidays_YesRadioButtonChecked()
        {
            WaitForElement(Availableonbankholidays_1);
            ValidateElementChecked(Availableonbankholidays_1);

            return this;
        }

        public DiaryViewSetupRecordPage ValidateAvailableOnBankHolidays_YesRadioButtonNotChecked()
        {
            WaitForElement(Availableonbankholidays_1);
            ValidateElementNotChecked(Availableonbankholidays_1);

            return this;
        }

        public DiaryViewSetupRecordPage ClickAvailableOnBankHolidays_NoRadioButton()
        {
            WaitForElementToBeClickable(Availableonbankholidays_0);
            Click(Availableonbankholidays_0);

            return this;
        }

        public DiaryViewSetupRecordPage ValidateAvailableOnBankHolidays_NoRadioButtonChecked()
        {
            WaitForElement(Availableonbankholidays_0);
            ValidateElementChecked(Availableonbankholidays_0);

            return this;
        }

        public DiaryViewSetupRecordPage ValidateAvailableOnBankHolidays_NoRadioButtonNotChecked()
        {
            WaitForElement(Availableonbankholidays_0);
            ValidateElementNotChecked(Availableonbankholidays_0);

            return this;
        }

        public DiaryViewSetupRecordPage ClickResponsibleTeamLink()
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            Click(ResponsibleTeamLink);

            return this;
        }

        public DiaryViewSetupRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            ValidateElementText(ResponsibleTeamLink, ExpectedText);

            return this;
        }

        public DiaryViewSetupRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamLookupButton);
            Click(ResponsibleTeamLookupButton);

            return this;
        }

        public DiaryViewSetupRecordPage ValidateIndividualsPerDayText(string ExpectedText)
        {
            WaitForElementVisible(Individualsperday);
            ValidateElementValue(Individualsperday, ExpectedText);

            return this;
        }

        public DiaryViewSetupRecordPage InsertTextOnIndividualsPerDay(string TextToInsert)
        {
            WaitForElementToBeClickable(Individualsperday);
            SendKeys(Individualsperday, TextToInsert);

            return this;
        }

        public DiaryViewSetupRecordPage ValidateStartTimeText(string ExpectedText)
        {
            WaitForElementVisible(Starttime);
            ValidateElementValue(Starttime, ExpectedText);

            return this;
        }

        public DiaryViewSetupRecordPage InsertTextOnStartTime(string TextToInsert)
        {
            WaitForElementToBeClickable(Starttime);
            SendKeys(Starttime, TextToInsert);

            return this;
        }

        public DiaryViewSetupRecordPage ClickStartTime_TimePicker()
        {
            WaitForElementToBeClickable(Starttime_TimePicker);
            Click(Starttime_TimePicker);

            return this;
        }

        public DiaryViewSetupRecordPage ValidateEndTimeText(string ExpectedText)
        {
            WaitForElementVisible(Endtime);
            ValidateElementValue(Endtime, ExpectedText);

            return this;
        }

        public DiaryViewSetupRecordPage InsertTextOnEndTime(string TextToInsert)
        {
            WaitForElementToBeClickable(Endtime);
            SendKeys(Endtime, TextToInsert);

            return this;
        }

        public DiaryViewSetupRecordPage ClickEndTime_TimePicker()
        {
            WaitForElementToBeClickable(Endtime_TimePicker);
            Click(Endtime_TimePicker);

            return this;
        }

        public DiaryViewSetupRecordPage ClickGroupBookingAllowed_YesRadioButton()
        {
            WaitForElementToBeClickable(Groupbookingallowed_1);
            Click(Groupbookingallowed_1);

            return this;
        }

        public DiaryViewSetupRecordPage ValidateGroupBookingAllowed_YesRadioButtonChecked()
        {
            WaitForElement(Groupbookingallowed_1);
            ValidateElementChecked(Groupbookingallowed_1);

            return this;
        }

        public DiaryViewSetupRecordPage ValidateGroupBookingAllowed_YesRadioButtonNotChecked()
        {
            WaitForElement(Groupbookingallowed_1);
            ValidateElementNotChecked(Groupbookingallowed_1);

            return this;
        }

        public DiaryViewSetupRecordPage ClickGroupBookingAllowed_NoRadioButton()
        {
            WaitForElementToBeClickable(Groupbookingallowed_0);
            Click(Groupbookingallowed_0);

            return this;
        }

        public DiaryViewSetupRecordPage ValidateGroupBookingAllowed_NoRadioButtonChecked()
        {
            WaitForElement(Groupbookingallowed_0);
            ValidateElementChecked(Groupbookingallowed_0);

            return this;
        }

        public DiaryViewSetupRecordPage ValidateGroupBookingAllowed_NoRadioButtonNotChecked()
        {
            WaitForElement(Groupbookingallowed_0);
            ValidateElementNotChecked(Groupbookingallowed_0);

            return this;
        }

        public DiaryViewSetupRecordPage ValidateNumberOfGroupsPerDayText(string ExpectedText)
        {
            WaitForElementVisible(Groupsperday);
            ValidateElementValue(Groupsperday, ExpectedText);

            return this;
        }

        public DiaryViewSetupRecordPage InsertTextOnNumberOfGroupsPerDay(string TextToInsert)
        {
            WaitForElementToBeClickable(Groupsperday);
            SendKeys(Groupsperday, TextToInsert);

            return this;
        }

        public DiaryViewSetupRecordPage ValidateNumberOfIndividualsPerGroupText(string ExpectedText)
        {
            WaitForElementVisible(Individualspergroup);
            ValidateElementValue(Individualspergroup, ExpectedText);

            return this;
        }

        public DiaryViewSetupRecordPage InsertTextOnNumberOfIndividualsPerGroup(string TextToInsert)
        {
            WaitForElementToBeClickable(Individualspergroup);
            SendKeys(Individualspergroup, TextToInsert);

            return this;
        }

        public DiaryViewSetupRecordPage NavigateToAudit()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(AuditLinkButton);
            Click(AuditLinkButton);

            return this;
        }

        public DiaryViewSetupRecordPage NavigateToLinkedProfessional()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(LinkedProfessionalLinkButton);
            Click(LinkedProfessionalLinkButton);

            return this;
        }

        public DiaryViewSetupRecordPage NavigateToRestrictions()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(RestrictionsLinkButton);
            Click(RestrictionsLinkButton);

            return this;
        }

    }
}

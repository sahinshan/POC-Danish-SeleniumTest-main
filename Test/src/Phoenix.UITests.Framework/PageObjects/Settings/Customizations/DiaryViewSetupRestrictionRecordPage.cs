using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Customizations
{
	public class DiaryViewSetupRestrictionRecordPage : CommonMethods
	{
        public DiaryViewSetupRestrictionRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=communityclinicrestriction&')]");


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
		readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
		readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");

		readonly By CommunityclinicdiaryviewsetupidLink = By.XPath("//*[@id='CWField_communityclinicdiaryviewsetupid_Link']");
		readonly By CommunityclinicdiaryviewsetupidLookupButton = By.XPath("//*[@id='CWLookupBtn_communityclinicdiaryviewsetupid']");
		readonly By Communityclinicrestrictiontypeid = By.XPath("//*[@id='CWField_communityclinicrestrictiontypeid']");
		readonly By Startdate = By.XPath("//*[@id='CWField_startdate']");
		readonly By StartdateDatePicker = By.XPath("//*[@id='CWField_startdate_DatePicker']");
		readonly By Enddate = By.XPath("//*[@id='CWField_enddate']");
		readonly By EnddateDatePicker = By.XPath("//*[@id='CWField_enddate_DatePicker']");
		readonly By HealthprofessionalidLink = By.XPath("//*[@id='CWField_healthprofessionalid_Link']");
		readonly By HealthprofessionalidLookupButton = By.XPath("//*[@id='CWLookupBtn_healthprofessionalid']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
		readonly By RecurrencepatternidLink = By.XPath("//*[@id='CWField_recurrencepatternid_Link']");
		readonly By RecurrencepatternidLookupButton = By.XPath("//*[@id='CWLookupBtn_recurrencepatternid']");
		readonly By Starttime = By.XPath("//*[@id='CWField_starttime']");
		readonly By Starttime_TimePicker = By.XPath("//*[@id='CWField_starttime_TimePicker']");
		readonly By Endtime = By.XPath("//*[@id='CWField_endtime']");
		readonly By Endtime_TimePicker = By.XPath("//*[@id='CWField_endtime_TimePicker']");
		readonly By Details = By.XPath("//*[@id='CWField_details']");


        public DiaryViewSetupRestrictionRecordPage WaitForDiaryViewSetupRestrictionRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElementVisible(pageHeader);

            return this;
        }



        public DiaryViewSetupRestrictionRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public DiaryViewSetupRestrictionRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			Click(SaveButton);

			return this;
		}

		public DiaryViewSetupRestrictionRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public DiaryViewSetupRestrictionRecordPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}

		public DiaryViewSetupRestrictionRecordPage ClickDeleteRecordButton()
		{
			WaitForElementToBeClickable(DeleteRecordButton);
			Click(DeleteRecordButton);

			return this;
		}



		public DiaryViewSetupRestrictionRecordPage ClickDiaryViewSetupLink()
		{
			WaitForElementToBeClickable(CommunityclinicdiaryviewsetupidLink);
			Click(CommunityclinicdiaryviewsetupidLink);

			return this;
		}

		public DiaryViewSetupRestrictionRecordPage ValidateDiaryViewSetupLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(CommunityclinicdiaryviewsetupidLink);
			ValidateElementText(CommunityclinicdiaryviewsetupidLink, ExpectedText);

			return this;
		}

		public DiaryViewSetupRestrictionRecordPage ClickDiaryViewSetupLookupButton()
		{
			WaitForElementToBeClickable(CommunityclinicdiaryviewsetupidLookupButton);
			Click(CommunityclinicdiaryviewsetupidLookupButton);

			return this;
		}

		public DiaryViewSetupRestrictionRecordPage SelectRestrictionType(string TextToSelect)
		{
			WaitForElementToBeClickable(Communityclinicrestrictiontypeid);
			SelectPicklistElementByText(Communityclinicrestrictiontypeid, TextToSelect);

			return this;
		}

		public DiaryViewSetupRestrictionRecordPage ValidateRestrictionTypeSelectedText(string ExpectedText)
        {
            WaitForElementVisible(Communityclinicrestrictiontypeid);
            ValidatePicklistSelectedText(Communityclinicrestrictiontypeid, ExpectedText);

			return this;
		}

		public DiaryViewSetupRestrictionRecordPage ValidateStartDateText(string ExpectedText)
        {
            WaitForElementVisible(Startdate);
            ValidateElementValue(Startdate, ExpectedText);

			return this;
		}

		public DiaryViewSetupRestrictionRecordPage InsertTextOnStartDate(string TextToInsert)
		{
			WaitForElementToBeClickable(Startdate);
			SendKeys(Startdate, TextToInsert);
			
			return this;
		}

		public DiaryViewSetupRestrictionRecordPage ClickStartDateDatePicker()
		{
			WaitForElementToBeClickable(StartdateDatePicker);
			Click(StartdateDatePicker);

			return this;
		}

		public DiaryViewSetupRestrictionRecordPage ValidateEndDateText(string ExpectedText)
        {
            WaitForElementVisible(Enddate);
            ValidateElementValue(Enddate, ExpectedText);

			return this;
		}

		public DiaryViewSetupRestrictionRecordPage InsertTextOnEndDate(string TextToInsert)
		{
			WaitForElementToBeClickable(Enddate);
			SendKeys(Enddate, TextToInsert);
			
			return this;
		}

		public DiaryViewSetupRestrictionRecordPage ClickEndDateDatePicker()
		{
			WaitForElementToBeClickable(EnddateDatePicker);
			Click(EnddateDatePicker);

			return this;
		}

		public DiaryViewSetupRestrictionRecordPage ClickHealthProfessionalLink()
		{
			WaitForElementToBeClickable(HealthprofessionalidLink);
			Click(HealthprofessionalidLink);

			return this;
		}

		public DiaryViewSetupRestrictionRecordPage ValidateHealthProfessionalLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(HealthprofessionalidLink);
			ValidateElementText(HealthprofessionalidLink, ExpectedText);

			return this;
		}

		public DiaryViewSetupRestrictionRecordPage ClickHealthProfessionalLookupButton()
		{
			WaitForElementToBeClickable(HealthprofessionalidLookupButton);
			Click(HealthprofessionalidLookupButton);

			return this;
		}

		public DiaryViewSetupRestrictionRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public DiaryViewSetupRestrictionRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public DiaryViewSetupRestrictionRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

		public DiaryViewSetupRestrictionRecordPage ClickRecurrencePatternLink()
		{
			WaitForElementToBeClickable(RecurrencepatternidLink);
			Click(RecurrencepatternidLink);

			return this;
		}

		public DiaryViewSetupRestrictionRecordPage ValidateRecurrencePatternLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(RecurrencepatternidLink);
			ValidateElementText(RecurrencepatternidLink, ExpectedText);

			return this;
		}

		public DiaryViewSetupRestrictionRecordPage ClickRecurrencePatternLookupButton()
		{
			WaitForElementToBeClickable(RecurrencepatternidLookupButton);
			Click(RecurrencepatternidLookupButton);

			return this;
		}

		public DiaryViewSetupRestrictionRecordPage ValidateStartTimeText(string ExpectedText)
        {
            WaitForElementVisible(Starttime);
            ValidateElementValue(Starttime, ExpectedText);

			return this;
		}

		public DiaryViewSetupRestrictionRecordPage InsertTextOnStartTime(string TextToInsert)
		{
			WaitForElementToBeClickable(Starttime);
			SendKeys(Starttime, TextToInsert);
			
			return this;
		}

		public DiaryViewSetupRestrictionRecordPage ClickStartTime_TimePicker()
		{
			WaitForElementToBeClickable(Starttime_TimePicker);
			Click(Starttime_TimePicker);

			return this;
		}

		public DiaryViewSetupRestrictionRecordPage ValidateEndTimeText(string ExpectedText)
        {
            WaitForElementVisible(Endtime);
            ValidateElementValue(Endtime, ExpectedText);

			return this;
		}

		public DiaryViewSetupRestrictionRecordPage InsertTextOnEndTime(string TextToInsert)
		{
			WaitForElementToBeClickable(Endtime);
			SendKeys(Endtime, TextToInsert);
			
			return this;
		}

		public DiaryViewSetupRestrictionRecordPage ClickEndTime_TimePicker()
		{
			WaitForElementToBeClickable(Endtime_TimePicker);
			Click(Endtime_TimePicker);

			return this;
		}

		public DiaryViewSetupRestrictionRecordPage ValidateDetailsText(string ExpectedText)
		{
			WaitForElementVisible(Details);
            ValidateElementText(Details, ExpectedText);

			return this;
		}

		public DiaryViewSetupRestrictionRecordPage InsertTextOnDetails(string TextToInsert)
		{
			WaitForElementToBeClickable(Details);
			SendKeys(Details, TextToInsert);
			
			return this;
		}

	}
}

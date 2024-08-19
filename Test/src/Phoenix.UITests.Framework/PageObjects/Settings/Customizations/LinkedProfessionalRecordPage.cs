using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Customizations
{
	public class LinkedProfessionalRecordPage : CommonMethods
	{
        public LinkedProfessionalRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=communitycliniclinkedprofessional&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
		readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
		readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");

		readonly By CommunityclinicdiaryviewsetupidLink = By.XPath("//*[@id='CWField_communityclinicdiaryviewsetupid_Link']");
		readonly By CommunityclinicdiaryviewsetupidLookupButton = By.XPath("//*[@id='CWLookupBtn_communityclinicdiaryviewsetupid']");
		readonly By SystemuseridLink = By.XPath("//*[@id='CWField_systemuserid_Link']");
		readonly By SystemuseridLookupButton = By.XPath("//*[@id='CWLookupBtn_systemuserid']");
		readonly By Startdate = By.XPath("//*[@id='CWField_startdate']");
		readonly By StartdateDatePicker = By.XPath("//*[@id='CWField_startdate_DatePicker']");
		readonly By Enddate = By.XPath("//*[@id='CWField_enddate']");
		readonly By EnddateDatePicker = By.XPath("//*[@id='CWField_enddate_DatePicker']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
		readonly By RecurrencepatternidLink = By.XPath("//*[@id='CWField_recurrencepatternid_Link']");
		readonly By RecurrencepatternidLookupButton = By.XPath("//*[@id='CWLookupBtn_recurrencepatternid']");
		readonly By Starttime = By.XPath("//*[@id='CWField_starttime']");
		readonly By Starttime_TimePicker = By.XPath("//*[@id='CWField_starttime_TimePicker']");
		readonly By Endtime = By.XPath("//*[@id='CWField_endtime']");
		readonly By Endtime_TimePicker = By.XPath("//*[@id='CWField_endtime_TimePicker']");
		readonly By Individualsperday = By.XPath("//*[@id='CWField_individualsperday']");
		readonly By Groupsperday = By.XPath("//*[@id='CWField_groupsperday']");
		readonly By Individualspergroup = By.XPath("//*[@id='CWField_individualspergroup']");



        public LinkedProfessionalRecordPage WaitForLinkedProfessionalRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElementVisible(pageHeader);

            return this;
        }


        public LinkedProfessionalRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public LinkedProfessionalRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			Click(SaveButton);

			return this;
		}

		public LinkedProfessionalRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public LinkedProfessionalRecordPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}

		public LinkedProfessionalRecordPage ClickDeleteRecordButton()
		{
			WaitForElementToBeClickable(DeleteRecordButton);
			Click(DeleteRecordButton);

			return this;
		}



		public LinkedProfessionalRecordPage ClickDiaryViewSetupLink()
		{
			WaitForElementToBeClickable(CommunityclinicdiaryviewsetupidLink);
			Click(CommunityclinicdiaryviewsetupidLink);

			return this;
		}

		public LinkedProfessionalRecordPage ValidateDiaryViewSetupLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(CommunityclinicdiaryviewsetupidLink);
			ValidateElementText(CommunityclinicdiaryviewsetupidLink, ExpectedText);

			return this;
		}

		public LinkedProfessionalRecordPage ClickDiaryViewSetupLookupButton()
		{
			WaitForElementToBeClickable(CommunityclinicdiaryviewsetupidLookupButton);
			Click(CommunityclinicdiaryviewsetupidLookupButton);

			return this;
		}

		public LinkedProfessionalRecordPage ClickProfessionalLink()
		{
			WaitForElementToBeClickable(SystemuseridLink);
			Click(SystemuseridLink);

			return this;
		}

		public LinkedProfessionalRecordPage ValidateProfessionalLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(SystemuseridLink);
			ValidateElementText(SystemuseridLink, ExpectedText);

			return this;
		}

		public LinkedProfessionalRecordPage ClickProfessionalLookupButton()
		{
			WaitForElementToBeClickable(SystemuseridLookupButton);
			Click(SystemuseridLookupButton);

			return this;
		}

		public LinkedProfessionalRecordPage ValidateStartDateText(string ExpectedText)
		{
			ValidateElementValue(Startdate, ExpectedText);

			return this;
		}

		public LinkedProfessionalRecordPage InsertTextOnStartDate(string TextToInsert)
		{
			WaitForElementToBeClickable(Startdate);
			SendKeys(Startdate, TextToInsert);
			
			return this;
		}

		public LinkedProfessionalRecordPage ClickStartDateDatePicker()
		{
			WaitForElementToBeClickable(StartdateDatePicker);
			Click(StartdateDatePicker);

			return this;
		}

		public LinkedProfessionalRecordPage ValidateEndDateText(string ExpectedText)
		{
			ValidateElementValue(Enddate, ExpectedText);

			return this;
		}

		public LinkedProfessionalRecordPage InsertTextOnEndDate(string TextToInsert)
		{
			WaitForElementToBeClickable(Enddate);
			SendKeys(Enddate, TextToInsert);
			
			return this;
		}

		public LinkedProfessionalRecordPage ClickEndDateDatePicker()
		{
			WaitForElementToBeClickable(EnddateDatePicker);
			Click(EnddateDatePicker);

			return this;
		}

		public LinkedProfessionalRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public LinkedProfessionalRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public LinkedProfessionalRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

		public LinkedProfessionalRecordPage ClickRecurrencePatternLink()
		{
			WaitForElementToBeClickable(RecurrencepatternidLink);
			Click(RecurrencepatternidLink);

			return this;
		}

		public LinkedProfessionalRecordPage ValidateRecurrencePatternLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(RecurrencepatternidLink);
			ValidateElementText(RecurrencepatternidLink, ExpectedText);

			return this;
		}

		public LinkedProfessionalRecordPage ClickRecurrencePatternLookupButton()
		{
			WaitForElementToBeClickable(RecurrencepatternidLookupButton);
			Click(RecurrencepatternidLookupButton);

			return this;
		}

		public LinkedProfessionalRecordPage ValidateStartTimeText(string ExpectedText)
		{
			ValidateElementValue(Starttime, ExpectedText);

			return this;
		}

		public LinkedProfessionalRecordPage InsertTextOnStartTime(string TextToInsert)
		{
			WaitForElementToBeClickable(Starttime);
			SendKeys(Starttime, TextToInsert);
			
			return this;
		}

		public LinkedProfessionalRecordPage ClickStartTime_TimePicker()
		{
			WaitForElementToBeClickable(Starttime_TimePicker);
			Click(Starttime_TimePicker);

			return this;
		}

		public LinkedProfessionalRecordPage ValidateEndTimeText(string ExpectedText)
		{
			ValidateElementValue(Endtime, ExpectedText);

			return this;
		}

		public LinkedProfessionalRecordPage InsertTextOnEndTime(string TextToInsert)
		{
			WaitForElementToBeClickable(Endtime);
			SendKeys(Endtime, TextToInsert);
			
			return this;
		}

		public LinkedProfessionalRecordPage ClickEndTime_TimePicker()
		{
			WaitForElementToBeClickable(Endtime_TimePicker);
			Click(Endtime_TimePicker);

			return this;
		}

		public LinkedProfessionalRecordPage ValidateNumberOfIndividualsPerDayText(string ExpectedText)
		{
			ValidateElementValue(Individualsperday, ExpectedText);

			return this;
		}

		public LinkedProfessionalRecordPage InsertTextOnNumberOfIndividualsPerDay(string TextToInsert)
		{
			WaitForElementToBeClickable(Individualsperday);
			SendKeys(Individualsperday, TextToInsert);
			
			return this;
		}

		public LinkedProfessionalRecordPage ValidateNumberOfGroupsPerDayText(string ExpectedText)
		{
			ValidateElementValue(Groupsperday, ExpectedText);

			return this;
		}

		public LinkedProfessionalRecordPage InsertTextOnNumberOfGroupsPerDay(string TextToInsert)
		{
			WaitForElementToBeClickable(Groupsperday);
			SendKeys(Groupsperday, TextToInsert);
			
			return this;
		}

		public LinkedProfessionalRecordPage ValidateNumberOfIndividualsPerGroupText(string ExpectedText)
		{
			ValidateElementValue(Individualspergroup, ExpectedText);

			return this;
		}

		public LinkedProfessionalRecordPage InsertTextOnNumberOfIndividualsPerGroup(string TextToInsert)
		{
			WaitForElementToBeClickable(Individualspergroup);
			SendKeys(Individualspergroup, TextToInsert);
			
			return this;
		}

	}
}

using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
	public class ExpressBookingCriteriaRecordPage : CommonMethods
	{
        public ExpressBookingCriteriaRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id, iframe_CWDialog_)][contains(@src, 'type=cpexpressbookingcriteria&')]");


        readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
		readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
		readonly By ViewScheduledBookings = By.XPath("//*[@id='TI_ViewScheduledBookings']");

        readonly By detailsTab = By.XPath("//*[@id='CWNavGroup_EditForm']");
		readonly By resultsTab = By.XPath("//*[@id='CWNavGroup_Results']");

        readonly By RegardingidLink = By.XPath("//*[@id='CWField_regardingid_Link']");
		readonly By RegardingidLookupButton = By.XPath("//*[@id='CWLookupBtn_regardingid']");
		readonly By Statusid = By.XPath("//*[@id='CWField_statusid']");
		readonly By Bookingrunstartdate = By.XPath("//*[@id='CWField_bookingrunstartdate']");
		readonly By Scheduledjobstart = By.XPath("//*[@id='CWField_scheduledjobstart']");
		readonly By Scheduledjobstart_Time = By.XPath("//*[@id='CWField_scheduledjobstart_Time']");
		readonly By Scheduledjobstart_Time_TimePicker = By.XPath("//*[@id='CWField_scheduledjobstart_Time_TimePicker']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
		readonly By Bookingrunenddate = By.XPath("//*[@id='CWField_bookingrunenddate']");
		readonly By Scheduledjobend = By.XPath("//*[@id='CWField_scheduledjobend']");
		readonly By Scheduledjobend_Time = By.XPath("//*[@id='CWField_scheduledjobend_Time']");
		readonly By Scheduledjobend_Time_TimePicker = By.XPath("//*[@id='CWField_scheduledjobend_Time_TimePicker']");



        public ExpressBookingCriteriaRecordPage WaitForExpressBookingCriteriaRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElement(BackButton);
            WaitForElement(detailsTab);

            return this;
        }



        public ExpressBookingCriteriaRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public ExpressBookingCriteriaRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			Click(SaveButton);

			return this;
		}

		public ExpressBookingCriteriaRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public ExpressBookingCriteriaRecordPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}

		public ExpressBookingCriteriaRecordPage ClickViewScheduledBookings()
		{
			WaitForElementToBeClickable(ViewScheduledBookings);
			Click(ViewScheduledBookings);

			return this;
		}



		public ExpressBookingCriteriaRecordPage ClickRegardingLink()
		{
			WaitForElementToBeClickable(RegardingidLink);
			Click(RegardingidLink);

			return this;
		}

		public ExpressBookingCriteriaRecordPage ValidateRegardingLinkText(string ExpectedText)
		{
			WaitForElement(RegardingidLink);
			ValidateElementText(RegardingidLink, ExpectedText);

			return this;
		}

		public ExpressBookingCriteriaRecordPage ClickRegardingLookupButton()
		{
			WaitForElementToBeClickable(RegardingidLookupButton);
			Click(RegardingidLookupButton);

			return this;
		}

		public ExpressBookingCriteriaRecordPage SelectStatus(string TextToSelect)
		{
			WaitForElementToBeClickable(Statusid);
			SelectPicklistElementByText(Statusid, TextToSelect);

			return this;
		}

		public ExpressBookingCriteriaRecordPage ValidateStatusSelectedText(string ExpectedText)
		{
			ValidatePicklistSelectedText(Statusid, ExpectedText);

			return this;
		}

        public ExpressBookingCriteriaRecordPage ValidateStatusFieldDisabled()
        {
            ValidateElementDisabled(Statusid);

            return this;
        }

        public ExpressBookingCriteriaRecordPage ValidateExpressBookingStartDateText(string ExpectedText)
		{
			ValidateElementValue(Bookingrunstartdate, ExpectedText);

			return this;
		}

		public ExpressBookingCriteriaRecordPage InsertTextOnExpressBookingStartDate(string TextToInsert)
		{
			WaitForElementToBeClickable(Bookingrunstartdate);
			SendKeys(Bookingrunstartdate, TextToInsert);
			
			return this;
		}

		public ExpressBookingCriteriaRecordPage ValidateScheduledJobStartDateText(string ExpectedText)
		{
			ValidateElementValue(Scheduledjobstart, ExpectedText);

			return this;
		}

		public ExpressBookingCriteriaRecordPage InsertTextOnScheduledJobStartDate(string TextToInsert)
		{
			WaitForElementToBeClickable(Scheduledjobstart);
			SendKeys(Scheduledjobstart, TextToInsert);
			
			return this;
		}

		public ExpressBookingCriteriaRecordPage ValidateScheduledJobStartTimeText(string ExpectedText)
		{
			ValidateElementValue(Scheduledjobstart_Time, ExpectedText);

			return this;
		}

		public ExpressBookingCriteriaRecordPage InsertTextOnScheduledJobStartTime(string TextToInsert)
		{
			WaitForElementToBeClickable(Scheduledjobstart_Time);
			SendKeys(Scheduledjobstart_Time, TextToInsert);
			
			return this;
		}

		public ExpressBookingCriteriaRecordPage ClickScheduledJobStartDate_TimePicker()
		{
			WaitForElementToBeClickable(Scheduledjobstart_Time_TimePicker);
			Click(Scheduledjobstart_Time_TimePicker);

			return this;
		}

		public ExpressBookingCriteriaRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public ExpressBookingCriteriaRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElement(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public ExpressBookingCriteriaRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

		public ExpressBookingCriteriaRecordPage ValidateExpressBookingEndDateText(string ExpectedText)
		{
			ValidateElementValue(Bookingrunenddate, ExpectedText);

			return this;
		}

        public ExpressBookingCriteriaRecordPage ValidateExpressBookingEndDateFieldDisabled()
        {
            ValidateElementDisabled(Bookingrunenddate);

            return this;
        }

        public ExpressBookingCriteriaRecordPage InsertTextOnExpressBookingEndDate(string TextToInsert)
		{
			WaitForElementToBeClickable(Bookingrunenddate);
			SendKeys(Bookingrunenddate, TextToInsert);
			
			return this;
		}

		public ExpressBookingCriteriaRecordPage ValidateScheduledJobEndDateText(string ExpectedText)
		{
			ValidateElementValue(Scheduledjobend, ExpectedText);

			return this;
		}

		public ExpressBookingCriteriaRecordPage InsertTextOnScheduledJobEndDate(string TextToInsert)
		{
			WaitForElementToBeClickable(Scheduledjobend);
			SendKeys(Scheduledjobend, TextToInsert);
			
			return this;
		}

		public ExpressBookingCriteriaRecordPage ValidateScheduledJobEndTimeText(string ExpectedText)
		{
			ValidateElementValue(Scheduledjobend_Time, ExpectedText);

			return this;
		}

		public ExpressBookingCriteriaRecordPage InsertTextOnScheduledJobEndTime(string TextToInsert)
		{
			WaitForElementToBeClickable(Scheduledjobend_Time);
			SendKeys(Scheduledjobend_Time, TextToInsert);
			
			return this;
		}

        public ExpressBookingCriteriaRecordPage ValidateScheduledJobEndDateTimeFieldDisabled()
        {
            ValidateElementDisabled(Scheduledjobend);
            ValidateElementDisabled(Scheduledjobend_Time);

            return this;
        }

        public ExpressBookingCriteriaRecordPage ClickScheduledJobEndTime_TimePicker()
		{
			WaitForElementToBeClickable(Scheduledjobend_Time_TimePicker);
			Click(Scheduledjobend_Time_TimePicker);

			return this;
		}

		public ExpressBookingCriteriaRecordPage ClickDetailsTab()
		{
            WaitForElementToBeClickable(detailsTab);
			ScrollToElement(detailsTab);
            Click(detailsTab);

            return this;
        }

		public ExpressBookingCriteriaRecordPage ClickResultsTab()
		{             
			WaitForElementToBeClickable(resultsTab);
			ScrollToElement(resultsTab);
			Click(resultsTab);
		
			return this;
		}

	}
}

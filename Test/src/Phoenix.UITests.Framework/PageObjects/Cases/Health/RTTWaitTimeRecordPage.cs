using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Cases.Health
{
	public class RTTWaitTimeRecordPage : CommonMethods
	{

        public RTTWaitTimeRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=rttwaittime&')]");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
		readonly By RestrictAccessButton = By.XPath("//*[@id='TI_RestrictAccessButton']");
		readonly By RunOnDemandWorkflow = By.XPath("//*[@id='TI_RunOnDemandWorkflow']");

        readonly By leftMainMenu = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By RTTEventSubLink = By.XPath("//*[@id='CWNavItem_RTTEvent']");

        readonly By Case_Link = By.XPath("//*[@id='CWField_caseid_Link']");
		readonly By Case_LookupButton = By.XPath("//*[@id='CWLookupBtn_caseid']");

		readonly By RTTPathwaySetup_Link = By.XPath("//*[@id='CWField_rttpathwaysetupid_Link']");
		readonly By RTTPathwaySetup_LookupButton = By.XPath("//*[@id='CWLookupBtn_rttpathwaysetupid']");

		readonly By RTTTreatmentFunction_Link = By.XPath("//*[@id='CWField_rtttreatmentfunctionid_Link']");
		readonly By RTTTreatmentFunction_LookupButton = By.XPath("//*[@id='CWLookupBtn_rtttreatmentfunctionid']");

		readonly By DaysWaited = By.XPath("//*[@id='CWField_dayswaited']");
		readonly By WeeksWaited = By.XPath("//*[@id='CWField_weekswaited']");

		readonly By IsClockRunning_1 = By.XPath("//*[@id='CWField_isclockrunning_1']");
		readonly By IsClockRunning_0 = By.XPath("//*[@id='CWField_isclockrunning_0']");

		readonly By ResponsibleUser_Link = By.XPath("//*[@id='CWField_responsibleuserid_Link']");
		readonly By ResponsibleUser_LookupButton = By.XPath("//*[@id='CWLookupBtn_responsibleuserid']");

		readonly By ProvideridLink = By.XPath("//*[@id='CWField_providerid_Link']");
		readonly By ProvideridLookupButton = By.XPath("//*[@id='CWLookupBtn_providerid']");

		readonly By CaseStartDate = By.XPath("//*[@id='CWField_casestartdate']");
		readonly By CaseStartDateDatePicker = By.XPath("//*[@id='CWField_casestartdate_DatePicker']");

		readonly By LatestStartDate = By.XPath("//*[@id='CWField_lateststartdate']");
		readonly By LatestStartDateDatePicker = By.XPath("//*[@id='CWField_lateststartdate_DatePicker']");

		readonly By BreachDate = By.XPath("//*[@id='CWField_breachdate']");
		readonly By BreachDateDatePicker = By.XPath("//*[@id='CWField_breachdate_DatePicker']");

		#region RTT Events

		readonly By IFrame_RTTEvents = By.Id("CWIFrame_RTTEvents");

		readonly By RTTEvent_ExportToExcelButton = By.Id("TI_ExportToExcelButton");
		readonly By RTTEvent_PageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

		By rttEventRecordRow(string RecordID, int RecordPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[" + RecordPosition + "][@id='" + RecordID + "']/td[2]");
		
		By rttEventRecord(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']");
		
		By rttEventRecordCellText(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");

		#endregion


		public RTTWaitTimeRecordPage WaitForRTTWaitTimeRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElementVisible(BackButton);
            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);
            
			WaitForElementVisible(Case_LookupButton);

            return this;
        }

        public RTTWaitTimeRecordPage NavigateToRttEventsSubPage()
        {
            WaitForElementToBeClickable(leftMainMenu);
            Click(leftMainMenu);

            WaitForElementToBeClickable(RTTEventSubLink);
            Click(RTTEventSubLink);

            return this;
        }

        public RTTWaitTimeRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			MoveToElementInPage(BackButton);
			Click(BackButton);

			return this;
		}

		public RTTWaitTimeRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			MoveToElementInPage(SaveButton);
			Click(SaveButton);

			return this;
		}

		public RTTWaitTimeRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			MoveToElementInPage(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public RTTWaitTimeRecordPage ClickRestrictAccessButton()
		{
			WaitForElementToBeClickable(RestrictAccessButton);
			MoveToElementInPage(RestrictAccessButton);
			Click(RestrictAccessButton);

			return this;
		}

		public RTTWaitTimeRecordPage ClickRunOnDemandWorkflow()
		{
			WaitForElementToBeClickable(RunOnDemandWorkflow);
			MoveToElementInPage(RunOnDemandWorkflow);
			Click(RunOnDemandWorkflow);

			return this;
		}

		public RTTWaitTimeRecordPage ClickRelatedCaseLink()
		{
			WaitForElementToBeClickable(Case_Link);
			MoveToElementInPage(Case_Link);
			Click(Case_Link);

			return this;
		}

		public RTTWaitTimeRecordPage ValidateRelatedCaseLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(Case_Link);
			MoveToElementInPage(Case_Link);
			ValidateElementText(Case_Link, ExpectedText);

			return this;
		}

		public RTTWaitTimeRecordPage ClickRelatedCaseLookupButton()
		{
			WaitForElementToBeClickable(Case_LookupButton);
			MoveToElementInPage(Case_LookupButton);
			Click(Case_LookupButton);

			return this;
		}

		public RTTWaitTimeRecordPage ClickRTTPathwayLink()
		{
			WaitForElementToBeClickable(RTTPathwaySetup_Link);
			MoveToElementInPage(RTTPathwaySetup_Link);
			Click(RTTPathwaySetup_Link);

			return this;
		}

		public RTTWaitTimeRecordPage ValidateRTTPathwayLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(RTTPathwaySetup_Link);
			MoveToElementInPage(RTTPathwaySetup_Link);
			ValidateElementText(RTTPathwaySetup_Link, ExpectedText);

			return this;
		}

		public RTTWaitTimeRecordPage ClickRTTPathwayLookupButton()
		{
			WaitForElementToBeClickable(RTTPathwaySetup_LookupButton);
			MoveToElementInPage(RTTPathwaySetup_LookupButton);
			Click(RTTPathwaySetup_LookupButton);

			return this;
		}

		public RTTWaitTimeRecordPage ClickRTTTreatmentFunctionLink()
		{
			WaitForElementToBeClickable(RTTTreatmentFunction_Link);
			MoveToElementInPage(RTTTreatmentFunction_Link);
			Click(RTTTreatmentFunction_Link);

			return this;
		}

		public RTTWaitTimeRecordPage ValidateRTTTreatmentFunctionLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(RTTTreatmentFunction_Link);
			MoveToElementInPage(RTTTreatmentFunction_Link);
			ValidateElementText(RTTTreatmentFunction_Link, ExpectedText);

			return this;
		}

		public RTTWaitTimeRecordPage ClickRTTTreatmentFunctionLookupButton()
		{
			WaitForElementToBeClickable(RTTTreatmentFunction_LookupButton);
			MoveToElementInPage(RTTTreatmentFunction_LookupButton);
			Click(RTTTreatmentFunction_LookupButton);

			return this;
		}

		public RTTWaitTimeRecordPage ValidateDaysWaitedText(string ExpectedText)
		{
			ValidateElementValue(DaysWaited, ExpectedText);

			return this;
		}

		public RTTWaitTimeRecordPage InsertTextOnDaysWaited(string TextToInsert)
		{
			WaitForElementToBeClickable(DaysWaited);
			MoveToElementInPage(DaysWaited);
			SendKeys(DaysWaited, TextToInsert);
			
			return this;
		}

		public RTTWaitTimeRecordPage ValidateWeeksWaitedText(string ExpectedText)
		{
			ValidateElementValue(WeeksWaited, ExpectedText);

			return this;
		}

		public RTTWaitTimeRecordPage InsertTextOnWeeksWaited(string TextToInsert)
		{
			WaitForElementToBeClickable(WeeksWaited);
			MoveToElementInPage(WeeksWaited);
			SendKeys(WeeksWaited, TextToInsert);
			
			return this;
		}

		public RTTWaitTimeRecordPage ClickIsClockRunning_YesRadioButton()
		{
			WaitForElementToBeClickable(IsClockRunning_1);
			MoveToElementInPage(IsClockRunning_1);
			Click(IsClockRunning_1);

			return this;
		}

		public RTTWaitTimeRecordPage ValidateIsClockRunning_YesRadioButtonChecked()
		{
			WaitForElementVisible(IsClockRunning_1);
			MoveToElementInPage(IsClockRunning_1);
			ValidateElementChecked(IsClockRunning_1);
			
			return this;
		}

		public RTTWaitTimeRecordPage ValidateIsClockRunning_YesRadioButtonNotChecked()
		{
			WaitForElementVisible(IsClockRunning_1);
			MoveToElementInPage(IsClockRunning_1);
			ValidateElementNotChecked(IsClockRunning_1);
			
			return this;
		}

		public RTTWaitTimeRecordPage ClickIsClockRunning_NoRadioButton()
		{
			WaitForElementToBeClickable(IsClockRunning_0);
			MoveToElementInPage(IsClockRunning_0);
			Click(IsClockRunning_0);

			return this;
		}

		public RTTWaitTimeRecordPage ValidateIsClockRunning_NoRadioButtonChecked()
		{
			WaitForElementVisible(IsClockRunning_0);
			MoveToElementInPage(IsClockRunning_0);
			ValidateElementChecked(IsClockRunning_0);
			
			return this;
		}

		public RTTWaitTimeRecordPage ValidateIsClockRunning_NoRadioButtonNotChecked()
		{
			WaitForElementVisible(IsClockRunning_0);
			MoveToElementInPage(IsClockRunning_0);
			ValidateElementNotChecked(IsClockRunning_0);
			
			return this;
		}

		public RTTWaitTimeRecordPage ClickResponsibleUserLink()
		{
			WaitForElementToBeClickable(ResponsibleUser_Link);
			MoveToElementInPage(ResponsibleUser_Link);
			Click(ResponsibleUser_Link);

			return this;
		}

		public RTTWaitTimeRecordPage ValidateResponsibleUserLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleUser_Link);
			MoveToElementInPage(ResponsibleUser_Link);
			ValidateElementText(ResponsibleUser_Link, ExpectedText);

			return this;
		}

		public RTTWaitTimeRecordPage ClickResponsibleUserLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleUser_LookupButton);
			MoveToElementInPage(ResponsibleUser_LookupButton);
			Click(ResponsibleUser_LookupButton);

			return this;
		}

		public RTTWaitTimeRecordPage ClickProviderLink()
		{
			WaitForElementToBeClickable(ProvideridLink);
			MoveToElementInPage(ProvideridLink);
			Click(ProvideridLink);

			return this;
		}

		public RTTWaitTimeRecordPage ValidateProviderLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ProvideridLink);
			MoveToElementInPage(ProvideridLink);
			ValidateElementText(ProvideridLink, ExpectedText);

			return this;
		}

		public RTTWaitTimeRecordPage ClickProviderLookupButton()
		{
			WaitForElementToBeClickable(ProvideridLookupButton);
			MoveToElementInPage(ProvideridLookupButton);
			Click(ProvideridLookupButton);

			return this;
		}

		public RTTWaitTimeRecordPage ValidateCaseStartDateText(string ExpectedText)
		{
			ValidateElementValue(CaseStartDate, ExpectedText);

			return this;
		}

		public RTTWaitTimeRecordPage InsertTextOnCaseStartDate(string TextToInsert)
		{
			WaitForElementToBeClickable(CaseStartDate);
			MoveToElementInPage(CaseStartDate);
			SendKeys(CaseStartDate, TextToInsert);
			
			return this;
		}

		public RTTWaitTimeRecordPage ClickCaseStartDateDatePicker()
		{
			WaitForElementToBeClickable(CaseStartDateDatePicker);
			MoveToElementInPage(CaseStartDateDatePicker);
			Click(CaseStartDateDatePicker);

			return this;
		}

		public RTTWaitTimeRecordPage ValidateLatestStartDateText(string ExpectedText)
		{
			ValidateElementValue(LatestStartDate, ExpectedText);

			return this;
		}

		public RTTWaitTimeRecordPage InsertTextOnLatestStartDate(string TextToInsert)
		{
			WaitForElementToBeClickable(LatestStartDate);
			MoveToElementInPage(LatestStartDate);
			SendKeys(LatestStartDate, TextToInsert);
			
			return this;
		}

		public RTTWaitTimeRecordPage ClickLatestStartDateDatePicker()
		{
			WaitForElementToBeClickable(LatestStartDateDatePicker);
			MoveToElementInPage(LatestStartDateDatePicker);
			Click(LatestStartDateDatePicker);

			return this;
		}

		public RTTWaitTimeRecordPage ValidateBreachDateText(string ExpectedText)
		{
			ValidateElementValue(BreachDate, ExpectedText);

			return this;
		}

		public RTTWaitTimeRecordPage InsertTextOnBreachDate(string TextToInsert)
		{
			WaitForElementToBeClickable(BreachDate);
			MoveToElementInPage(BreachDate);
			SendKeys(BreachDate, TextToInsert);
			
			return this;
		}

		public RTTWaitTimeRecordPage ClickBreachDate_DatePicker()
		{
			WaitForElementToBeClickable(BreachDateDatePicker);
			MoveToElementInPage(BreachDateDatePicker);
			Click(BreachDateDatePicker);

			return this;
		}

		#region RTT Events Section

		public RTTWaitTimeRecordPage WaitForRTTEventsSectionToLoad()
		{
			WaitForElement(IFrame_RTTEvents);
			SwitchToIframe(IFrame_RTTEvents);

			WaitForElementVisible(RTTEvent_ExportToExcelButton);
			WaitForElementVisible(RTTEvent_PageHeader);
			ValidateElementText(RTTEvent_PageHeader, "RTT Events");

			return this;
		}

		public RTTWaitTimeRecordPage ValidateRTTEventRecordRowPosition(string RecordID, int RecordPosition)
		{
			WaitForElementVisible(rttEventRecordRow(RecordID, RecordPosition));
			ScrollToElement(rttEventRecordRow(RecordID, RecordPosition));

			return this;
		}

		public RTTWaitTimeRecordPage ValidateRTTEventRecordPresent(string RecordID)
		{
			WaitForElementVisible(rttEventRecord(RecordID));
			ScrollToElement(rttEventRecord(RecordID));

			return this;
		}

		public RTTWaitTimeRecordPage ValidateRTTEventRecordCellText(string RecordID, int CellPosition, string ExpectedText)
		{
			WaitForElementVisible(rttEventRecordCellText(RecordID, CellPosition));
			ScrollToElement(rttEventRecordCellText(RecordID, CellPosition));
			ValidateElementText(rttEventRecordCellText(RecordID, CellPosition), ExpectedText);

			return this;
		}

		#endregion

	}
}

using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
	public class BookingDiaryStaffRecordPage : CommonMethods
	{
        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By CWDialogIFrame = By.XPath("//*[starts-with(@id, 'iframe_CWDialog_')][contains(@src, 'type=cpbookingdiarystaff&')]");
        readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
		readonly By RunOnDemandWorkflow = By.XPath("//*[@id='TI_RunOnDemandWorkflow']");
		readonly By CopyRecordLink = By.XPath("//*[@id='TI_CopyRecordLink']");
		readonly By CpbookingdiaryidLink = By.XPath("//*[@id='CWField_cpbookingdiaryid_Link']");
		readonly By CpbookingdiaryidLookupButton = By.XPath("//*[@id='CWLookupBtn_cpbookingdiaryid']");
		readonly By SystemuseremploymentcontractidLink = By.XPath("//*[@id='CWField_systemuseremploymentcontractid_Link']");
		readonly By SystemuseremploymentcontractidClearButton = By.XPath("//*[@id='CWClearLookup_systemuseremploymentcontractid']");
		readonly By SystemuseremploymentcontractidLookupButton = By.XPath("//*[@id='CWLookupBtn_systemuseremploymentcontractid']");
		readonly By Cppayrollstatusid = By.XPath("//*[@id='CWField_cppayrollstatusid']");
		readonly By Attendancestartdate = By.XPath("//*[@id='CWField_attendancestartdate']");
		readonly By AttendancestartdateDatePicker = By.XPath("//*[@id='CWField_attendancestartdate_DatePicker']");
		readonly By Attendanceenddate = By.XPath("//*[@id='CWField_attendanceenddate']");
		readonly By AttendanceenddateDatePicker = By.XPath("//*[@id='CWField_attendanceenddate_DatePicker']");
		readonly By Attendancedurationminutes = By.XPath("//*[@id='CWField_attendancedurationminutes']");
		readonly By Latitude = By.XPath("//*[@id='CWField_latitude']");
		readonly By Problemsandnotes = By.XPath("//*[@id='CWField_problemsandnotes']");
		readonly By Confirmed_1 = By.XPath("//*[@id='CWField_confirmed_1']");
		readonly By Confirmed_0 = By.XPath("//*[@id='CWField_confirmed_0']");
		readonly By CWFieldButton_ShowOnMap = By.XPath("//*[@id='CWFieldButton_ShowOnMap']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
		readonly By SystemuseridLink = By.XPath("//*[@id='CWField_systemuserid_Link']");
		readonly By SystemuseridLookupButton = By.XPath("//*[@id='CWLookupBtn_systemuserid']");
		readonly By Attendancestarttime = By.XPath("//*[@id='CWField_attendancestarttime']");
		readonly By Attendancestarttime_TimePicker = By.XPath("//*[@id='CWField_attendancestarttime_TimePicker']");
		readonly By Attendanceendtime = By.XPath("//*[@id='CWField_attendanceendtime']");
		readonly By Attendanceendtime_TimePicker = By.XPath("//*[@id='CWField_attendanceendtime_TimePicker']");
		readonly By Attendancedurationhours = By.XPath("//*[@id='CWField_attendancedurationhours']");
		readonly By Longitude = By.XPath("//*[@id='CWField_longitude']");
		readonly By Differencebetweenstartvisitandaddress = By.XPath("//*[@id='CWField_differencebetweenstartvisitandaddress']");
		readonly By Costoverride_1 = By.XPath("//*[@id='CWField_costoverride_1']");
		readonly By Costoverride_0 = By.XPath("//*[@id='CWField_costoverride_0']");
		readonly By Breakoverride_1 = By.XPath("//*[@id='CWField_breakoverride_1']");
		readonly By Breakoverride_0 = By.XPath("//*[@id='CWField_breakoverride_0']");
		readonly By Overriddencost = By.XPath("//*[@id='CWField_overriddencost']");
		readonly By Overriddenbreaklength = By.XPath("//*[@id='CWField_overriddenbreaklength']");

        public BookingDiaryStaffRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        public BookingDiaryStaffRecordPage WaitForBookingDiaryStaffRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(CWDialogIFrame);
            SwitchToIframe(CWDialogIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(SystemuseridLink);

            return this;
        }

        public BookingDiaryStaffRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public BookingDiaryStaffRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			Click(SaveButton);

			return this;
		}

		public BookingDiaryStaffRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public BookingDiaryStaffRecordPage ClickRunOnDemandWorkflow()
		{
			WaitForElementToBeClickable(RunOnDemandWorkflow);
			Click(RunOnDemandWorkflow);

			return this;
		}

		public BookingDiaryStaffRecordPage ClickCopyRecordLink()
		{
			WaitForElementToBeClickable(CopyRecordLink);
			Click(CopyRecordLink);

			return this;
		}

		public BookingDiaryStaffRecordPage ClickCpbookingdiaryidLink()
		{
			WaitForElementToBeClickable(CpbookingdiaryidLink);
			Click(CpbookingdiaryidLink);

			return this;
		}

		public BookingDiaryStaffRecordPage ValidateCpbookingdiaryidLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(CpbookingdiaryidLink);
			ValidateElementText(CpbookingdiaryidLink, ExpectedText);

			return this;
		}

		public BookingDiaryStaffRecordPage ClickCpbookingdiaryidLookupButton()
		{
			WaitForElementToBeClickable(CpbookingdiaryidLookupButton);
			Click(CpbookingdiaryidLookupButton);

			return this;
		}

		public BookingDiaryStaffRecordPage ClickSystemuseremploymentcontractidLink()
		{
			WaitForElementToBeClickable(SystemuseremploymentcontractidLink);
			Click(SystemuseremploymentcontractidLink);

			return this;
		}

		public BookingDiaryStaffRecordPage ValidateSystemuseremploymentcontractidLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(SystemuseremploymentcontractidLink);
			ValidateElementText(SystemuseremploymentcontractidLink, ExpectedText);

			return this;
		}

		public BookingDiaryStaffRecordPage ClickSystemuseremploymentcontractidClearButton()
		{
			WaitForElementToBeClickable(SystemuseremploymentcontractidClearButton);
			Click(SystemuseremploymentcontractidClearButton);

			return this;
		}

		public BookingDiaryStaffRecordPage ClickSystemuseremploymentcontractidLookupButton()
		{
			WaitForElementToBeClickable(SystemuseremploymentcontractidLookupButton);
			Click(SystemuseremploymentcontractidLookupButton);

			return this;
		}

		public BookingDiaryStaffRecordPage SelectCppayrollstatusid(string TextToSelect)
		{
			WaitForElementToBeClickable(Cppayrollstatusid);
			SelectPicklistElementByText(Cppayrollstatusid, TextToSelect);

			return this;
		}

		public BookingDiaryStaffRecordPage ValidateCppayrollstatusidSelectedText(string ExpectedText)
		{
			ValidateElementText(Cppayrollstatusid, ExpectedText);

			return this;
		}

		public BookingDiaryStaffRecordPage ValidateAttendancestartdateText(string ExpectedText)
		{
			ValidateElementValue(Attendancestartdate, ExpectedText);

			return this;
		}

		public BookingDiaryStaffRecordPage InsertTextOnAttendancestartdate(string TextToInsert)
		{
			WaitForElementToBeClickable(Attendancestartdate);
			SendKeys(Attendancestartdate, TextToInsert);
			
			return this;
		}

		public BookingDiaryStaffRecordPage ClickAttendancestartdateDatePicker()
		{
			WaitForElementToBeClickable(AttendancestartdateDatePicker);
			Click(AttendancestartdateDatePicker);

			return this;
		}

		public BookingDiaryStaffRecordPage ValidateAttendanceenddateText(string ExpectedText)
		{
			ValidateElementValue(Attendanceenddate, ExpectedText);

			return this;
		}

		public BookingDiaryStaffRecordPage InsertTextOnAttendanceenddate(string TextToInsert)
		{
			WaitForElementToBeClickable(Attendanceenddate);
			SendKeys(Attendanceenddate, TextToInsert);
			
			return this;
		}

		public BookingDiaryStaffRecordPage ClickAttendanceenddateDatePicker()
		{
			WaitForElementToBeClickable(AttendanceenddateDatePicker);
			Click(AttendanceenddateDatePicker);

			return this;
		}

		public BookingDiaryStaffRecordPage ValidateAttendancedurationminutesText(string ExpectedText)
		{
			ValidateElementValue(Attendancedurationminutes, ExpectedText);

			return this;
		}

		public BookingDiaryStaffRecordPage InsertTextOnAttendancedurationminutes(string TextToInsert)
		{
			WaitForElementToBeClickable(Attendancedurationminutes);
			SendKeys(Attendancedurationminutes, TextToInsert);
			
			return this;
		}

		public BookingDiaryStaffRecordPage ValidateLatitudeText(string ExpectedText)
		{
			ValidateElementValue(Latitude, ExpectedText);

			return this;
		}

		public BookingDiaryStaffRecordPage InsertTextOnLatitude(string TextToInsert)
		{
			WaitForElementToBeClickable(Latitude);
			SendKeys(Latitude, TextToInsert);
			
			return this;
		}

		public BookingDiaryStaffRecordPage ValidateProblemsandnotesText(string ExpectedText)
		{
			ValidateElementText(Problemsandnotes, ExpectedText);

			return this;
		}

		public BookingDiaryStaffRecordPage InsertTextOnProblemsandnotes(string TextToInsert)
		{
			WaitForElementToBeClickable(Problemsandnotes);
			SendKeys(Problemsandnotes, TextToInsert);
			
			return this;
		}

		public BookingDiaryStaffRecordPage ClickConfirmed_1()
		{
			WaitForElementToBeClickable(Confirmed_1);
			Click(Confirmed_1);

			return this;
		}

		public BookingDiaryStaffRecordPage ValidateConfirmed_1Checked()
		{
			WaitForElement(Confirmed_1);
			ValidateElementChecked(Confirmed_1);
			
			return this;
		}

		public BookingDiaryStaffRecordPage ValidateConfirmed_1NotChecked()
		{
			WaitForElement(Confirmed_1);
			ValidateElementNotChecked(Confirmed_1);
			
			return this;
		}

		public BookingDiaryStaffRecordPage ClickConfirmed_0()
		{
			WaitForElementToBeClickable(Confirmed_0);
			Click(Confirmed_0);

			return this;
		}

		public BookingDiaryStaffRecordPage ValidateConfirmed_0Checked()
		{
			WaitForElement(Confirmed_0);
			ValidateElementChecked(Confirmed_0);
			
			return this;
		}

		public BookingDiaryStaffRecordPage ValidateConfirmed_0NotChecked()
		{
			WaitForElement(Confirmed_0);
			ValidateElementNotChecked(Confirmed_0);
			
			return this;
		}

		public BookingDiaryStaffRecordPage ClickCWFieldButton_ShowOnMap()
		{
			WaitForElementToBeClickable(CWFieldButton_ShowOnMap);
			Click(CWFieldButton_ShowOnMap);

			return this;
		}

		public BookingDiaryStaffRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public BookingDiaryStaffRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public BookingDiaryStaffRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

		public BookingDiaryStaffRecordPage ClickSystemuseridLink()
		{
			WaitForElementToBeClickable(SystemuseridLink);
			Click(SystemuseridLink);

			return this;
		}

		public BookingDiaryStaffRecordPage ValidateSystemuseridLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(SystemuseridLink);
			ValidateElementText(SystemuseridLink, ExpectedText);

			return this;
		}

		public BookingDiaryStaffRecordPage ClickSystemuseridLookupButton()
		{
			WaitForElementToBeClickable(SystemuseridLookupButton);
			Click(SystemuseridLookupButton);

			return this;
		}

		public BookingDiaryStaffRecordPage ValidateAttendancestarttimeText(string ExpectedText)
		{
			ValidateElementValue(Attendancestarttime, ExpectedText);

			return this;
		}

		public BookingDiaryStaffRecordPage InsertTextOnAttendancestarttime(string TextToInsert)
		{
			WaitForElementToBeClickable(Attendancestarttime);
			SendKeys(Attendancestarttime, TextToInsert);
			
			return this;
		}

		public BookingDiaryStaffRecordPage ClickAttendancestarttime_TimePicker()
		{
			WaitForElementToBeClickable(Attendancestarttime_TimePicker);
			Click(Attendancestarttime_TimePicker);

			return this;
		}

		public BookingDiaryStaffRecordPage ValidateAttendanceendtimeText(string ExpectedText)
		{
			ValidateElementValue(Attendanceendtime, ExpectedText);

			return this;
		}

		public BookingDiaryStaffRecordPage InsertTextOnAttendanceendtime(string TextToInsert)
		{
			WaitForElementToBeClickable(Attendanceendtime);
			SendKeys(Attendanceendtime, TextToInsert);
			
			return this;
		}

		public BookingDiaryStaffRecordPage ClickAttendanceendtime_TimePicker()
		{
			WaitForElementToBeClickable(Attendanceendtime_TimePicker);
			Click(Attendanceendtime_TimePicker);

			return this;
		}

		public BookingDiaryStaffRecordPage ValidateAttendancedurationhoursText(string ExpectedText)
		{
			ValidateElementValue(Attendancedurationhours, ExpectedText);

			return this;
		}

		public BookingDiaryStaffRecordPage InsertTextOnAttendancedurationhours(string TextToInsert)
		{
			WaitForElementToBeClickable(Attendancedurationhours);
			SendKeys(Attendancedurationhours, TextToInsert);
			
			return this;
		}

		public BookingDiaryStaffRecordPage ValidateLongitudeText(string ExpectedText)
		{
			ValidateElementValue(Longitude, ExpectedText);

			return this;
		}

		public BookingDiaryStaffRecordPage InsertTextOnLongitude(string TextToInsert)
		{
			WaitForElementToBeClickable(Longitude);
			SendKeys(Longitude, TextToInsert);
			
			return this;
		}

		public BookingDiaryStaffRecordPage ValidateDifferencebetweenstartvisitandaddressText(string ExpectedText)
		{
			ValidateElementValue(Differencebetweenstartvisitandaddress, ExpectedText);

			return this;
		}

		public BookingDiaryStaffRecordPage InsertTextOnDifferencebetweenstartvisitandaddress(string TextToInsert)
		{
			WaitForElementToBeClickable(Differencebetweenstartvisitandaddress);
			SendKeys(Differencebetweenstartvisitandaddress, TextToInsert);
			
			return this;
		}

		public BookingDiaryStaffRecordPage ClickCostoverride_1()
		{
			WaitForElementToBeClickable(Costoverride_1);
			Click(Costoverride_1);

			return this;
		}

		public BookingDiaryStaffRecordPage ValidateCostoverride_1Checked()
		{
			WaitForElement(Costoverride_1);
			ValidateElementChecked(Costoverride_1);
			
			return this;
		}

		public BookingDiaryStaffRecordPage ValidateCostoverride_1NotChecked()
		{
			WaitForElement(Costoverride_1);
			ValidateElementNotChecked(Costoverride_1);
			
			return this;
		}

		public BookingDiaryStaffRecordPage ClickCostoverride_0()
		{
			WaitForElementToBeClickable(Costoverride_0);
			Click(Costoverride_0);

			return this;
		}

		public BookingDiaryStaffRecordPage ValidateCostoverride_0Checked()
		{
			WaitForElement(Costoverride_0);
			ValidateElementChecked(Costoverride_0);
			
			return this;
		}

		public BookingDiaryStaffRecordPage ValidateCostoverride_0NotChecked()
		{
			WaitForElement(Costoverride_0);
			ValidateElementNotChecked(Costoverride_0);
			
			return this;
		}

		public BookingDiaryStaffRecordPage ClickBreakoverride_1()
		{
			WaitForElementToBeClickable(Breakoverride_1);
			Click(Breakoverride_1);

			return this;
		}

		public BookingDiaryStaffRecordPage ValidateBreakoverride_1Checked()
		{
			WaitForElement(Breakoverride_1);
			ValidateElementChecked(Breakoverride_1);
			
			return this;
		}

		public BookingDiaryStaffRecordPage ValidateBreakoverride_1NotChecked()
		{
			WaitForElement(Breakoverride_1);
			ValidateElementNotChecked(Breakoverride_1);
			
			return this;
		}

		public BookingDiaryStaffRecordPage ClickBreakoverride_0()
		{
			WaitForElementToBeClickable(Breakoverride_0);
			Click(Breakoverride_0);

			return this;
		}

		public BookingDiaryStaffRecordPage ValidateBreakoverride_0Checked()
		{
			WaitForElement(Breakoverride_0);
			ValidateElementChecked(Breakoverride_0);
			
			return this;
		}

		public BookingDiaryStaffRecordPage ValidateBreakoverride_0NotChecked()
		{
			WaitForElement(Breakoverride_0);
			ValidateElementNotChecked(Breakoverride_0);
			
			return this;
		}

		public BookingDiaryStaffRecordPage ValidateOverriddencostText(string ExpectedText)
		{
			ValidateElementValue(Overriddencost, ExpectedText);

			return this;
		}

		public BookingDiaryStaffRecordPage InsertTextOnOverriddencost(string TextToInsert)
		{
			WaitForElementToBeClickable(Overriddencost);
			SendKeys(Overriddencost, TextToInsert);
			
			return this;
		}

		public BookingDiaryStaffRecordPage ValidateOverriddenbreaklengthText(string ExpectedText)
		{
			ValidateElementValue(Overriddenbreaklength, ExpectedText);

			return this;
		}

		public BookingDiaryStaffRecordPage InsertTextOnOverriddenbreaklength(string TextToInsert)
		{
			WaitForElementToBeClickable(Overriddenbreaklength);
			SendKeys(Overriddenbreaklength, TextToInsert);
			
			return this;
		}

	}
}

using OpenQA.Selenium;
using Phoenix.UITests.Framework.PageObjects.Settings.Configuration.CPFinance;

namespace Phoenix.UITests.Framework.PageObjects
{
	public class BookingPaymentRecordPage : CommonMethods
	{
        public BookingPaymentRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id, 'iframe_CWDialog')][contains(@src, 'type=cpsystemusershiftpayment&')]");

        readonly By PageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
		readonly By CreditAndReplace = By.XPath("//*[@id='TI_CreditAndReplace']");
		readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");

		readonly By EmployeeLink = By.XPath("//*[@id='CWField_systemuserid_Link']");
		readonly By EmployeeLookupButton = By.XPath("//*[@id='CWLookupBtn_systemuserid']");
		readonly By RoleLink = By.XPath("//*[@id='CWField_careproviderstaffroletypeid_Link']");
		readonly By RoleLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderstaffroletypeid']");
		readonly By StartDate = By.XPath("//*[@id='CWField_startdate']");
		readonly By StartDateDatePicker = By.XPath("//*[@id='CWField_startdate_DatePicker']");
		readonly By StartDate_Time = By.XPath("//*[@id='CWField_startdate_Time']");
		readonly By StartDate_Time_TimePicker = By.XPath("//*[@id='CWField_startdate_Time_TimePicker']");
		readonly By PayrollBatchLink = By.XPath("//*[@id='CWField_careproviderpayrollbatchid_Link']");
		readonly By PayrollBatchLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderpayrollbatchid']");
		readonly By Status = By.XPath("//*[@id='CWField_cpsystemusershiftpaymentstatusid']");
		readonly By Duration = By.XPath("//*[@id='CWField_duration']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
		readonly By ProviderLink = By.XPath("//*[@id='CWField_providerid_Link']");
		readonly By ProviderLookupButton = By.XPath("//*[@id='CWLookupBtn_providerid']");
		readonly By GrossAmount = By.XPath("//*[@id='CWField_netamount']");
		readonly By EndDate = By.XPath("//*[@id='CWField_enddate']");
		readonly By EndDateDatePicker = By.XPath("//*[@id='CWField_enddate_DatePicker']");
		readonly By EndDate_Time = By.XPath("//*[@id='CWField_enddate_Time']");
		readonly By EndDate_Time_TimePicker = By.XPath("//*[@id='CWField_enddate_Time_TimePicker']");
		readonly By EmployeeContractLink = By.XPath("//*[@id='CWField_systemuseremploymentcontractid_Link']");
		readonly By EmployeeContractLookupButton = By.XPath("//*[@id='CWLookupBtn_systemuseremploymentcontractid']");
		readonly By BookingReferenceLink = By.XPath("//*[@id='CWField_cpbookingdiaryid_Link']");
		readonly By BookingReferenceLookupButton = By.XPath("//*[@id='CWLookupBtn_cpbookingdiaryid']");
		readonly By PartialBookingPayment_YesRadioButton = By.XPath("//*[@id='CWField_partialbookingpayment_1']");
		readonly By PartialBookingPayment_NoRadioButton = By.XPath("//*[@id='CWField_partialbookingpayment_0']");
		readonly By PaidHours = By.XPath("//*[@id='CWField_paidhours']");
		readonly By BookingTypeLink = By.XPath("//*[@id='CWField_cpbookingtypeid_Link']");
		readonly By BookingTypeLookupButton = By.XPath("//*[@id='CWLookupBtn_cpbookingtypeid']");
		readonly By MasterPayArrangementsLookupButton = By.XPath("//*[@id='CWLookupBtn_careprovidermasterpayarrangementid']");


        public BookingPaymentRecordPage WaitForPageToLoad()
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

            WaitForElementVisible(PageHeader);
            WaitForElementVisible(BackButton);
            WaitForElementVisible(AssignRecordButton);

            return this;
        }


        public BookingPaymentRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public BookingPaymentRecordPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}

		public BookingPaymentRecordPage ClickCreditAndReplace()
		{
			WaitForElementToBeClickable(CreditAndReplace);
			Click(CreditAndReplace);

			return this;
		}

		public BookingPaymentRecordPage ClickDeleteRecordButton()
		{
			WaitForElementToBeClickable(DeleteRecordButton);
			Click(DeleteRecordButton);

			return this;
		}

		public BookingPaymentRecordPage ClickEmployeeLink()
		{
			WaitForElementToBeClickable(EmployeeLink);
			Click(EmployeeLink);

			return this;
		}

		public BookingPaymentRecordPage ValidateEmployeeLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(EmployeeLink);
			ValidateElementText(EmployeeLink, ExpectedText);

			return this;
		}

		public BookingPaymentRecordPage ClickEmployeeLookupButton()
		{
			WaitForElementToBeClickable(EmployeeLookupButton);
			Click(EmployeeLookupButton);

			return this;
		}

		public BookingPaymentRecordPage ClickRoleLink()
		{
			WaitForElementToBeClickable(RoleLink);
			Click(RoleLink);

			return this;
		}

		public BookingPaymentRecordPage ValidateRoleLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(RoleLink);
			ValidateElementText(RoleLink, ExpectedText);

			return this;
		}

		public BookingPaymentRecordPage ClickRoleLookupButton()
		{
			WaitForElementToBeClickable(RoleLookupButton);
			Click(RoleLookupButton);

			return this;
		}

		public BookingPaymentRecordPage ValidateStartDateText(string ExpectedText)
		{
			ValidateElementValue(StartDate, ExpectedText);

			return this;
		}

		public BookingPaymentRecordPage InsertTextOnStartDate(string TextToInsert)
		{
			WaitForElementToBeClickable(StartDate);
			SendKeys(StartDate, TextToInsert);
			
			return this;
		}

		public BookingPaymentRecordPage ClickStartDateDatePicker()
		{
			WaitForElementToBeClickable(StartDateDatePicker);
			Click(StartDateDatePicker);

			return this;
		}

		public BookingPaymentRecordPage ValidateStartDate_TimeText(string ExpectedText)
		{
			ValidateElementValue(StartDate_Time, ExpectedText);

			return this;
		}

		public BookingPaymentRecordPage InsertTextOnStartDate_Time(string TextToInsert)
		{
			WaitForElementToBeClickable(StartDate_Time);
			SendKeys(StartDate_Time, TextToInsert);
			
			return this;
		}

		public BookingPaymentRecordPage ClickStartDate_Time_TimePicker()
		{
			WaitForElementToBeClickable(StartDate_Time_TimePicker);
			Click(StartDate_Time_TimePicker);

			return this;
		}

		public BookingPaymentRecordPage ClickPayrollBatchLink()
		{
			WaitForElementToBeClickable(PayrollBatchLink);
			Click(PayrollBatchLink);

			return this;
		}

		public BookingPaymentRecordPage ValidatePayrollBatchLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(PayrollBatchLink);
			ValidateElementText(PayrollBatchLink, ExpectedText);

			return this;
		}

		public BookingPaymentRecordPage ClickPayrollBatchLookupButton()
		{
			WaitForElementToBeClickable(PayrollBatchLookupButton);
			Click(PayrollBatchLookupButton);

			return this;
		}

		public BookingPaymentRecordPage SelectStatus(string TextToSelect)
		{
			WaitForElementToBeClickable(Status);
			SelectPicklistElementByText(Status, TextToSelect);

			return this;
		}

		public BookingPaymentRecordPage ValidateStatusSelectedText(string ExpectedText)
		{
			ValidateElementText(Status, ExpectedText);

			return this;
		}

		public BookingPaymentRecordPage ValidateDurationText(string ExpectedText)
		{
			ValidateElementValue(Duration, ExpectedText);

			return this;
		}

		public BookingPaymentRecordPage InsertTextOnDuration(string TextToInsert)
		{
			WaitForElementToBeClickable(Duration);
			SendKeys(Duration, TextToInsert);
			
			return this;
		}

		public BookingPaymentRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public BookingPaymentRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public BookingPaymentRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

		public BookingPaymentRecordPage ClickProviderLink()
		{
			WaitForElementToBeClickable(ProviderLink);
			Click(ProviderLink);

			return this;
		}

		public BookingPaymentRecordPage ValidateProviderLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ProviderLink);
			ValidateElementText(ProviderLink, ExpectedText);

			return this;
		}

		public BookingPaymentRecordPage ClickProviderLookupButton()
		{
			WaitForElementToBeClickable(ProviderLookupButton);
			Click(ProviderLookupButton);

			return this;
		}

		public BookingPaymentRecordPage ValidateGrossAmountText(string ExpectedText)
		{
			ValidateElementValue(GrossAmount, ExpectedText);

			return this;
		}

		public BookingPaymentRecordPage InsertTextOnGrossAmount(string TextToInsert)
		{
			WaitForElementToBeClickable(GrossAmount);
			SendKeys(GrossAmount, TextToInsert);
			
			return this;
		}

		public BookingPaymentRecordPage ValidateEndDateText(string ExpectedText)
		{
			ValidateElementValue(EndDate, ExpectedText);

			return this;
		}

		public BookingPaymentRecordPage InsertTextOnEndDate(string TextToInsert)
		{
			WaitForElementToBeClickable(EndDate);
			SendKeys(EndDate, TextToInsert);
			
			return this;
		}

		public BookingPaymentRecordPage ClickEndDateDatePicker()
		{
			WaitForElementToBeClickable(EndDateDatePicker);
			Click(EndDateDatePicker);

			return this;
		}

		public BookingPaymentRecordPage ValidateEndDate_TimeText(string ExpectedText)
		{
			ValidateElementValue(EndDate_Time, ExpectedText);

			return this;
		}

		public BookingPaymentRecordPage InsertTextOnEndDate_Time(string TextToInsert)
		{
			WaitForElementToBeClickable(EndDate_Time);
			SendKeys(EndDate_Time, TextToInsert);
			
			return this;
		}

		public BookingPaymentRecordPage ClickEndDate_Time_TimePicker()
		{
			WaitForElementToBeClickable(EndDate_Time_TimePicker);
			Click(EndDate_Time_TimePicker);

			return this;
		}

		public BookingPaymentRecordPage ClickEmployeeContractLink()
		{
			WaitForElementToBeClickable(EmployeeContractLink);
			Click(EmployeeContractLink);

			return this;
		}

		public BookingPaymentRecordPage ValidateEmployeeContractLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(EmployeeContractLink);
			ValidateElementText(EmployeeContractLink, ExpectedText);

			return this;
		}

		public BookingPaymentRecordPage ClickEmployeeContractLookupButton()
		{
			WaitForElementToBeClickable(EmployeeContractLookupButton);
			Click(EmployeeContractLookupButton);

			return this;
		}

		public BookingPaymentRecordPage ClickBookingReferenceLink()
		{
			WaitForElementToBeClickable(BookingReferenceLink);
			Click(BookingReferenceLink);

			return this;
		}

		public BookingPaymentRecordPage ValidateBookingReferenceLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(BookingReferenceLink);
			ValidateElementText(BookingReferenceLink, ExpectedText);

			return this;
		}

		public BookingPaymentRecordPage ClickBookingReferenceLookupButton()
		{
			WaitForElementToBeClickable(BookingReferenceLookupButton);
			Click(BookingReferenceLookupButton);

			return this;
		}

		public BookingPaymentRecordPage ClickPartialBookingPayment_YesRadioButton()
		{
			WaitForElementToBeClickable(PartialBookingPayment_YesRadioButton);
			Click(PartialBookingPayment_YesRadioButton);

			return this;
		}

		public BookingPaymentRecordPage ValidatePartialBookingPayment_YesRadioButtonChecked()
		{
			WaitForElement(PartialBookingPayment_YesRadioButton);
			ValidateElementChecked(PartialBookingPayment_YesRadioButton);
			
			return this;
		}

		public BookingPaymentRecordPage ValidatePartialBookingPayment_YesRadioButtonNotChecked()
		{
			WaitForElement(PartialBookingPayment_YesRadioButton);
			ValidateElementNotChecked(PartialBookingPayment_YesRadioButton);
			
			return this;
		}

		public BookingPaymentRecordPage ClickPartialBookingPayment_NoRadioButton()
		{
			WaitForElementToBeClickable(PartialBookingPayment_NoRadioButton);
			Click(PartialBookingPayment_NoRadioButton);

			return this;
		}

		public BookingPaymentRecordPage ValidatePartialBookingPayment_NoRadioButtonChecked()
		{
			WaitForElement(PartialBookingPayment_NoRadioButton);
			ValidateElementChecked(PartialBookingPayment_NoRadioButton);
			
			return this;
		}

		public BookingPaymentRecordPage ValidatePartialBookingPayment_NoRadioButtonNotChecked()
		{
			WaitForElement(PartialBookingPayment_NoRadioButton);
			ValidateElementNotChecked(PartialBookingPayment_NoRadioButton);
			
			return this;
		}

		public BookingPaymentRecordPage ValidatePaidHoursText(string ExpectedText)
		{
			ValidateElementValue(PaidHours, ExpectedText);

			return this;
		}

		public BookingPaymentRecordPage InsertTextOnPaidHours(string TextToInsert)
		{
			WaitForElementToBeClickable(PaidHours);
			SendKeys(PaidHours, TextToInsert);
			
			return this;
		}

		public BookingPaymentRecordPage ClickBookingTypeLink()
		{
			WaitForElementToBeClickable(BookingTypeLink);
			Click(BookingTypeLink);

			return this;
		}

		public BookingPaymentRecordPage ValidateBookingTypeLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(BookingTypeLink);
			ValidateElementText(BookingTypeLink, ExpectedText);

			return this;
		}

		public BookingPaymentRecordPage ClickBookingTypeLookupButton()
		{
			WaitForElementToBeClickable(BookingTypeLookupButton);
			Click(BookingTypeLookupButton);

			return this;
		}

		public BookingPaymentRecordPage ClickMasterPayArrangementsLookupButton()
		{
			WaitForElementToBeClickable(MasterPayArrangementsLookupButton);
			Click(MasterPayArrangementsLookupButton);

			return this;
		}

	}
}

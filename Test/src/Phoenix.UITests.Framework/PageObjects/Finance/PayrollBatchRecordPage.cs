using OpenQA.Selenium;
using Phoenix.UITests.Framework.PageObjects.Finance;
using Phoenix.UITests.Framework.WebAppAPI.Classes;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
	public class PayrollBatchRecordPage : CommonMethods
	{
        public PayrollBatchRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderpayrollbatch&')]");


        readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
		readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
		readonly By RunPayrollBatchButton = By.XPath("//*[@id='TI_RunPayrollBatch']");

        readonly By bookingPaymentsTab = By.XPath("//*[@id='CWNavGroup_ShiftPayments']/a");

        readonly By Payrollbatchnumber = By.XPath("//*[@id='CWField_payrollbatchnumber']");
		readonly By Anyprovider_1 = By.XPath("//*[@id='CWField_anyprovider_1']");
		readonly By Anyprovider_0 = By.XPath("//*[@id='CWField_anyprovider_0']");

		By Providerid_SelectedElementLink(string ElementId) => By.XPath("//*[@id='MS_providerid_" + ElementId + "']/a[@id='" + ElementId + "_Link']");
        By Providerid_SelectedElementRemoveButton(string ElementId) => By.XPath("//*[@id='MS_providerid_" + ElementId + "']/a[text()='Remove']");
        readonly By ProvideridLookupButton = By.XPath("//*[@id='CWLookupBtn_providerid']");

		readonly By Startdate = By.XPath("//*[@id='CWField_startdate']");
		readonly By StartdateDatePicker = By.XPath("//*[@id='CWField_startdate_DatePicker']");
		readonly By Processautomatically_1 = By.XPath("//*[@id='CWField_processautomatically_1']");
		readonly By Processautomatically_0 = By.XPath("//*[@id='CWField_processautomatically_0']");
		readonly By Netamount = By.XPath("//*[@id='CWField_netamount']");
		readonly By CareproviderpayrollbatchsetupidLink = By.XPath("//*[@id='CWField_careproviderpayrollbatchsetupid_Link']");
		readonly By CareproviderpayrollbatchsetupidLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderpayrollbatchsetupid']");
		readonly By Isextractonlyconfirmedbookingpayments_1 = By.XPath("//*[@id='CWField_isextractonlyconfirmedbookingpayments_1']");
		readonly By Isextractonlyconfirmedbookingpayments_0 = By.XPath("//*[@id='CWField_isextractonlyconfirmedbookingpayments_0']");
		readonly By Usebatchstartdataforextraction_1 = By.XPath("//*[@id='CWField_usebatchstartdataforextraction_1']");
		readonly By Usebatchstartdataforextraction_0 = By.XPath("//*[@id='CWField_usebatchstartdataforextraction_0']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
		readonly By Anysystemuser_1 = By.XPath("//*[@id='CWField_anysystemuser_1']");
		readonly By Anysystemuser_0 = By.XPath("//*[@id='CWField_anysystemuser_0']");
		readonly By SystemuseridLookupButton = By.XPath("//*[@id='CWLookupBtn_systemuserid']");
		readonly By Enddate = By.XPath("//*[@id='CWField_enddate']");
		readonly By EnddateDatePicker = By.XPath("//*[@id='CWField_enddate_DatePicker']");
		readonly By Bookingdatecriteriaid = By.XPath("//*[@id='CWField_bookingdatecriteriaid']");
		readonly By Processingdate = By.XPath("//*[@id='CWField_processingdate']");
		readonly By ProcessingdateDatePicker = By.XPath("//*[@id='CWField_processingdate_DatePicker']");
		readonly By Careproviderpayrollbatchstatusid = By.XPath("//*[@id='CWField_careproviderpayrollbatchstatusid']");
		readonly By Extractcontent_FileLink = By.XPath("//*[@id='CWField_extractcontent_FileLink']");


        public PayrollBatchRecordPage WaitForPageToLoad()
        {
            WaitForElementNotVisible("CWRefreshPanel", 14);

            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 14);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 14);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElementNotVisible("CWRefreshPanel", 14);

            WaitForElementVisible(BackButton);

            WaitForElementVisible(Payrollbatchnumber);

            return this;
        }



        public PayrollBatchRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public PayrollBatchRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			Click(SaveButton);

			return this;
		}

		public PayrollBatchRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public PayrollBatchRecordPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}
		
        public PayrollBatchRecordPage ClickRunPayrollBatchButton()
        {
            WaitForElementToBeClickable(RunPayrollBatchButton);
            Click(RunPayrollBatchButton);

            return this;
        }



        public PayrollBatchRecordPage ClickBookingPaymentsTab()
        {
            WaitForElementToBeClickable(bookingPaymentsTab);
            Click(bookingPaymentsTab);

            return this;
        }



        public PayrollBatchRecordPage ValidatePayrollBatchIdText(string ExpectedText)
		{
			ValidateElementValue(Payrollbatchnumber, ExpectedText);

			return this;
		}

		public PayrollBatchRecordPage InsertTextOnPayrollBatchId(string TextToInsert)
		{
			WaitForElementToBeClickable(Payrollbatchnumber);
			SendKeys(Payrollbatchnumber, TextToInsert);
			
			return this;
		}

		public PayrollBatchRecordPage ClickApplyToAnyProvider_YesRadioButton()
		{
			WaitForElementToBeClickable(Anyprovider_1);
			Click(Anyprovider_1);

			return this;
		}

		public PayrollBatchRecordPage ValidateApplyToAnyProvider_YesRadioButtonChecked()
		{
			WaitForElement(Anyprovider_1);
			ValidateElementChecked(Anyprovider_1);
			
			return this;
		}

		public PayrollBatchRecordPage ValidateApplyToAnyProvider_YesRadioButtonNotChecked()
		{
			WaitForElement(Anyprovider_1);
			ValidateElementNotChecked(Anyprovider_1);
			
			return this;
		}

		public PayrollBatchRecordPage ClickApplyToAnyProvider_NoRadioButton()
		{
			WaitForElementToBeClickable(Anyprovider_0);
			Click(Anyprovider_0);

			return this;
		}

		public PayrollBatchRecordPage ValidateApplyToAnyProvider_NoRadioButtonChecked()
		{
			WaitForElement(Anyprovider_0);
			ValidateElementChecked(Anyprovider_0);
			
			return this;
		}

		public PayrollBatchRecordPage ValidateApplyToAnyProvider_NoRadioButtonNotChecked()
		{
			WaitForElement(Anyprovider_0);
			ValidateElementNotChecked(Anyprovider_0);
			
			return this;
		}

        public PayrollBatchRecordPage ClickProviders_SelectedElementLink(string ElementId)
        {
            WaitForElementToBeClickable(Providerid_SelectedElementLink(ElementId));
            Click(Providerid_SelectedElementLink(ElementId));

            return this;
        }

        public PayrollBatchRecordPage ClickProviders_SelectedElementLink(Guid ElementId)
        {
            return ClickProviders_SelectedElementLink(ElementId.ToString());
        }

        public PayrollBatchRecordPage ValidateProviders_SelectedElementLinkText(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(Providerid_SelectedElementLink(ElementId));
            ValidateElementText(Providerid_SelectedElementLink(ElementId), ExpectedText);

            return this;
        }

        public PayrollBatchRecordPage ValidateProviders_SelectedElementLinkText(Guid ElementId, string ExpectedText)
        {
            return ValidateProviders_SelectedElementLinkText(ElementId.ToString(), ExpectedText);
        }

        public PayrollBatchRecordPage ClickProviders_SelectedElementRemoveButton(string ElementId)
        {
            WaitForElementToBeClickable(Providerid_SelectedElementRemoveButton(ElementId));
            Click(Providerid_SelectedElementRemoveButton(ElementId));

            return this;
        }

        public PayrollBatchRecordPage ClickProviders_SelectedElementRemoveButton(Guid ElementId)
        {
            return ClickProviders_SelectedElementRemoveButton(ElementId.ToString());
        }

        public PayrollBatchRecordPage ClickProvidersLookupButton()
		{
			WaitForElementToBeClickable(ProvideridLookupButton);
			Click(ProvideridLookupButton);

			return this;
		}

		public PayrollBatchRecordPage ValidateStartDateText(string ExpectedText)
		{
			ValidateElementValue(Startdate, ExpectedText);

			return this;
		}

		public PayrollBatchRecordPage InsertTextOnStartDate(string TextToInsert)
		{
			WaitForElementToBeClickable(Startdate);
			SendKeys(Startdate, TextToInsert);
			
			return this;
		}

		public PayrollBatchRecordPage ClickStartDateDatePicker()
		{
			WaitForElementToBeClickable(StartdateDatePicker);
			Click(StartdateDatePicker);

			return this;
		}

		public PayrollBatchRecordPage ClickProcessAutomatically_YesRadioButton()
		{
			WaitForElementToBeClickable(Processautomatically_1);
			Click(Processautomatically_1);

			return this;
		}

		public PayrollBatchRecordPage ValidateProcessAutomatically_YesRadioButtonChecked()
		{
			WaitForElement(Processautomatically_1);
			ValidateElementChecked(Processautomatically_1);
			
			return this;
		}

		public PayrollBatchRecordPage ValidateProcessAutomatically_YesRadioButtonNotChecked()
		{
			WaitForElement(Processautomatically_1);
			ValidateElementNotChecked(Processautomatically_1);
			
			return this;
		}

		public PayrollBatchRecordPage ClickProcessAutomatically_NoRadioButton()
		{
			WaitForElementToBeClickable(Processautomatically_0);
			Click(Processautomatically_0);

			return this;
		}

		public PayrollBatchRecordPage ValidateProcessAutomatically_NoRadioButtonChecked()
		{
			WaitForElement(Processautomatically_0);
			ValidateElementChecked(Processautomatically_0);
			
			return this;
		}

		public PayrollBatchRecordPage ValidateProcessAutomatically_NoRadioButtonNotChecked()
		{
			WaitForElement(Processautomatically_0);
			ValidateElementNotChecked(Processautomatically_0);
			
			return this;
		}

		public PayrollBatchRecordPage ValidateGrossAmountText(string ExpectedText)
		{
			ValidateElementValue(Netamount, ExpectedText);

			return this;
		}

		public PayrollBatchRecordPage InsertTextOnGrossAmount(string TextToInsert)
		{
			WaitForElementToBeClickable(Netamount);
			SendKeys(Netamount, TextToInsert);
			
			return this;
		}

		public PayrollBatchRecordPage ClickPayrollBatchSetupLink()
		{
			WaitForElementToBeClickable(CareproviderpayrollbatchsetupidLink);
			Click(CareproviderpayrollbatchsetupidLink);

			return this;
		}

		public PayrollBatchRecordPage ValidatePayrollBatchSetupLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(CareproviderpayrollbatchsetupidLink);
			ValidateElementText(CareproviderpayrollbatchsetupidLink, ExpectedText);

			return this;
		}

		public PayrollBatchRecordPage ClickPayrollBatchSetupLookupButton()
		{
			WaitForElementToBeClickable(CareproviderpayrollbatchsetupidLookupButton);
			Click(CareproviderpayrollbatchsetupidLookupButton);

			return this;
		}

		public PayrollBatchRecordPage ClickExtractOnlyConfirmedBookingPayments_YesRadioButton()
		{
			WaitForElementToBeClickable(Isextractonlyconfirmedbookingpayments_1);
			Click(Isextractonlyconfirmedbookingpayments_1);

			return this;
		}

		public PayrollBatchRecordPage ValidateExtractOnlyConfirmedBookingPayments_YesRadioButtonChecked()
		{
			WaitForElement(Isextractonlyconfirmedbookingpayments_1);
			ValidateElementChecked(Isextractonlyconfirmedbookingpayments_1);
			
			return this;
		}

		public PayrollBatchRecordPage ValidateExtractOnlyConfirmedBookingPayments_YesRadioButtonNotChecked()
		{
			WaitForElement(Isextractonlyconfirmedbookingpayments_1);
			ValidateElementNotChecked(Isextractonlyconfirmedbookingpayments_1);
			
			return this;
		}

		public PayrollBatchRecordPage ClickExtractOnlyConfirmedBookingPayments_NoRadioButton()
		{
			WaitForElementToBeClickable(Isextractonlyconfirmedbookingpayments_0);
			Click(Isextractonlyconfirmedbookingpayments_0);

			return this;
		}

		public PayrollBatchRecordPage ValidateExtractOnlyConfirmedBookingPayments_NoRadioButtonChecked()
		{
			WaitForElement(Isextractonlyconfirmedbookingpayments_0);
			ValidateElementChecked(Isextractonlyconfirmedbookingpayments_0);
			
			return this;
		}

		public PayrollBatchRecordPage ValidateExtractOnlyConfirmedBookingPayments_NoRadioButtonNotChecked()
		{
			WaitForElement(Isextractonlyconfirmedbookingpayments_0);
			ValidateElementNotChecked(Isextractonlyconfirmedbookingpayments_0);
			
			return this;
		}

		public PayrollBatchRecordPage ClickExtractOnlyRecordsWithinPayrollBatchDates_YesRadioButton()
		{
			WaitForElementToBeClickable(Usebatchstartdataforextraction_1);
			Click(Usebatchstartdataforextraction_1);

			return this;
		}

		public PayrollBatchRecordPage ValidateExtractOnlyRecordsWithinPayrollBatchDates_YesRadioButtonChecked()
		{
			WaitForElement(Usebatchstartdataforextraction_1);
			ValidateElementChecked(Usebatchstartdataforextraction_1);
			
			return this;
		}

		public PayrollBatchRecordPage ValidateExtractOnlyRecordsWithinPayrollBatchDates_YesRadioButtonNotChecked()
		{
			WaitForElement(Usebatchstartdataforextraction_1);
			ValidateElementNotChecked(Usebatchstartdataforextraction_1);
			
			return this;
		}

		public PayrollBatchRecordPage ClickExtractOnlyRecordsWithinPayrollBatchDates_NoRadioButton()
		{
			WaitForElementToBeClickable(Usebatchstartdataforextraction_0);
			Click(Usebatchstartdataforextraction_0);

			return this;
		}

		public PayrollBatchRecordPage ValidateExtractOnlyRecordsWithinPayrollBatchDates_NoRadioButtonChecked()
		{
			WaitForElement(Usebatchstartdataforextraction_0);
			ValidateElementChecked(Usebatchstartdataforextraction_0);
			
			return this;
		}

		public PayrollBatchRecordPage ValidateExtractOnlyRecordsWithinPayrollBatchDates_NoRadioButtonNotChecked()
		{
			WaitForElement(Usebatchstartdataforextraction_0);
			ValidateElementNotChecked(Usebatchstartdataforextraction_0);
			
			return this;
		}

		public PayrollBatchRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public PayrollBatchRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public PayrollBatchRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

		public PayrollBatchRecordPage ClickApplyToAnyEmployee_YesRadioButton()
		{
			WaitForElementToBeClickable(Anysystemuser_1);
			Click(Anysystemuser_1);

			return this;
		}

		public PayrollBatchRecordPage ValidateApplyToAnyEmployee_YesRadioButtonChecked()
		{
			WaitForElement(Anysystemuser_1);
			ValidateElementChecked(Anysystemuser_1);
			
			return this;
		}

		public PayrollBatchRecordPage ValidateApplyToAnyEmployee_YesRadioButtonNotChecked()
		{
			WaitForElement(Anysystemuser_1);
			ValidateElementNotChecked(Anysystemuser_1);
			
			return this;
		}

		public PayrollBatchRecordPage ClickApplyToAnyEmployee_NoRadioButton()
		{
			WaitForElementToBeClickable(Anysystemuser_0);
			Click(Anysystemuser_0);

			return this;
		}

		public PayrollBatchRecordPage ValidateApplyToAnyEmployee_NoRadioButtonChecked()
		{
			WaitForElement(Anysystemuser_0);
			ValidateElementChecked(Anysystemuser_0);
			
			return this;
		}

		public PayrollBatchRecordPage ValidateApplyToAnyEmployee_NoRadioButtonNotChecked()
		{
			WaitForElement(Anysystemuser_0);
			ValidateElementNotChecked(Anysystemuser_0);
			
			return this;
		}

		public PayrollBatchRecordPage ClickEmployeesLookupButton()
		{
			WaitForElementToBeClickable(SystemuseridLookupButton);
			Click(SystemuseridLookupButton);

			return this;
		}

		public PayrollBatchRecordPage ValidateEndDateText(string ExpectedText)
		{
			ValidateElementValue(Enddate, ExpectedText);

			return this;
		}

		public PayrollBatchRecordPage InsertTextOnEndDate(string TextToInsert)
		{
			WaitForElementToBeClickable(Enddate);
			SendKeys(Enddate, TextToInsert);
			
			return this;
		}

		public PayrollBatchRecordPage ClickEndDateDatePicker()
		{
			WaitForElementToBeClickable(EnddateDatePicker);
			Click(EnddateDatePicker);
            return this;
        }

		public PayrollBatchRecordPage SelectBookingDateCriteria(string TextToSelect)
		{
			WaitForElementToBeClickable(Bookingdatecriteriaid);
			SelectPicklistElementByText(Bookingdatecriteriaid, TextToSelect);

			return this;
		}

		public PayrollBatchRecordPage ValidateBookingDateCriteriaSelectedText(string ExpectedText)
		{
			ValidateElementText(Bookingdatecriteriaid, ExpectedText);

			return this;
		}

		public PayrollBatchRecordPage ValidateProcessingDateText(string ExpectedText)
		{
			ValidateElementValue(Processingdate, ExpectedText);

			return this;
		}

		public PayrollBatchRecordPage InsertTextOnProcessingDate(string TextToInsert)
		{
			WaitForElementToBeClickable(Processingdate);
			SendKeys(Processingdate, TextToInsert);
			
			return this;
		}

		public PayrollBatchRecordPage ClickProcessingDateDatePicker()
		{
			WaitForElementToBeClickable(ProcessingdateDatePicker);
			Click(ProcessingdateDatePicker);

			return this;
		}

		public PayrollBatchRecordPage SelectBatchStatus(string TextToSelect)
		{
			WaitForElementToBeClickable(Careproviderpayrollbatchstatusid);
			SelectPicklistElementByText(Careproviderpayrollbatchstatusid, TextToSelect);

			return this;
		}

		public PayrollBatchRecordPage ValidateBatchStatusSelectedText(string ExpectedText)
		{
			ValidateElementText(Careproviderpayrollbatchstatusid, ExpectedText);

			return this;
		}

		public PayrollBatchRecordPage ClickSummaryExtractFile_FileLink()
		{
			WaitForElementToBeClickable(Extractcontent_FileLink);
			Click(Extractcontent_FileLink);

			return this;
		}

		public PayrollBatchRecordPage ValidateSummaryExtractFile_FileLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(Extractcontent_FileLink);
			ValidateElementText(Extractcontent_FileLink, ExpectedText);

			return this;
		}

	}
}

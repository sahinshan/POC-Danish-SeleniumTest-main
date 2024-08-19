using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Finance
{
	public class FinanceInvoiceBatchRecordPage : CommonMethods
	{
		public FinanceInvoiceBatchRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
		{
			this.driver = driver;
			this.Wait = Wait;
			this.appURL = appURL;
		}

		readonly By contentIFrame = By.Id("CWContentIFrame");
		readonly By cwDialogIFrame_financeInvoiceBatch = By.XPath("//iframe[contains(@id, 'iframe_CWDialog')][contains(@src, 'type=financeinvoicebatch&')]");
		readonly By ToolbarMenuButton = By.Id("CWToolbarMenu");
		readonly By RunInvoiceBatchButton = By.Id("TI_RunFinanceInvoiceBatch");
		readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
		readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
		readonly By FinanceInvoiceBatchNumber = By.XPath("//*[@id='CWField_financeinvoicebatchnumber']");
		readonly By Runontime = By.XPath("//*[@id='CWField_runontime']");
		readonly By RunontimeDatePicker = By.XPath("//*[@id='CWField_runontime_DatePicker']");
		readonly By Runontime_Time = By.XPath("//*[@id='CWField_runontime_Time']");
		readonly By Runontime_Time_TimePicker = By.XPath("//*[@id='CWField_runontime_Time_TimePicker']");
		readonly By PeriodStartDate = By.XPath("//*[@id='CWField_periodstartdate']");
		readonly By PeriodStartDateDatePicker = By.XPath("//*[@id='CWField_periodstartdate_DatePicker']");
		readonly By FinanceinvoicebatchsetupLink = By.XPath("//*[@id='CWField_financeinvoicebatchsetupid_Link']");
		readonly By FinanceinvoicebatchsetupLookupButton = By.XPath("//*[@id='CWLookupBtn_financeinvoicebatchsetupid']");
		readonly By BatchStatusId = By.XPath("//*[@id='CWField_batchstatusid']");
		readonly By PeriodEndDate = By.XPath("//*[@id='CWField_periodenddate']");
		readonly By PeriodEndDateDatePicker = By.XPath("//*[@id='CWField_periodenddate_DatePicker']");
		readonly By FinanceModuleId = By.XPath("//*[@id='CWField_financemoduleid']");
		readonly By ServiceElement1Link = By.XPath("//*[@id='CWField_serviceelement1id_Link']");
		readonly By ServiceElement1LookupButton = By.XPath("//*[@id='CWLookupBtn_serviceelement1id']");
		readonly By ProviderBatchGroupingLink = By.XPath("//*[@id='CWField_providerbatchgroupingid_Link']");
		readonly By ProviderBatchGroupingLookupButton = By.XPath("//*[@id='CWLookupBtn_providerbatchgroupingid']");
		readonly By Isadhocbatch_1 = By.XPath("//*[@id='CWField_isadhocbatch_1']");
		readonly By Isadhocbatch_0 = By.XPath("//*[@id='CWField_isadhocbatch_0']");
		readonly By PaymentTypeLink = By.XPath("//*[@id='CWField_paymenttypeid_Link']");
		readonly By PaymentTypeLookupButton = By.XPath("//*[@id='CWLookupBtn_paymenttypeid']");
		readonly By NetBatchTotal = By.XPath("//*[@id='CWField_netbatchtotal']");
		readonly By VatTotal = By.XPath("//*[@id='CWField_vattotal']");
		readonly By NumberOfInvoicesCreated = By.XPath("//*[@id='CWField_numberofinvoicescreated']");
		readonly By UserLink = By.XPath("//*[@id='CWField_userid_Link']");
		readonly By UserLookupButton = By.XPath("//*[@id='CWLookupBtn_userid']");
		readonly By GrossBatchTotal = By.XPath("//*[@id='CWField_grossbatchtotal']");
		readonly By NumberOfUniqueProvidersPayees = By.XPath("//*[@id='CWField_numberofuniqueproviderspayees']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
		readonly By FinanceInvoiceBatchRecordPageHeader = By.XPath("//h1");
		readonly By InvoicesTab = By.XPath("//li[@id = 'CWNavGroup_Invoices']");

		public FinanceInvoiceBatchRecordPage WaitForFinanceInvoiceBatchRecordPageToLoad()
		{
			SwitchToDefaultFrame();

			WaitForElement(contentIFrame);
			SwitchToIframe(contentIFrame);

			WaitForElement(cwDialogIFrame_financeInvoiceBatch);
			SwitchToIframe(cwDialogIFrame_financeInvoiceBatch);

			WaitForElementNotVisible("CWRefreshPanel", 100);

			WaitForElementVisible(FinanceInvoiceBatchRecordPageHeader);

			return this;
		}


		public FinanceInvoiceBatchRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			Click(SaveButton);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ClickRunInvoiceBatchRecordButton()
		{
			WaitForElementToBeClickable(ToolbarMenuButton);
			MoveToElementInPage(ToolbarMenuButton);
			Click(ToolbarMenuButton);

			WaitForElementToBeClickable(RunInvoiceBatchButton);
			MoveToElementInPage(RunInvoiceBatchButton);
			Click(RunInvoiceBatchButton);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ValidateFinanceinvoicebatchnumberText(string ExpectedText)
		{
			ValidateElementValue(FinanceInvoiceBatchNumber, ExpectedText);

			return this;
		}

		public FinanceInvoiceBatchRecordPage InsertTextOnFinanceinvoicebatchnumber(string TextToInsert)
		{
			WaitForElementToBeClickable(FinanceInvoiceBatchNumber);
			SendKeys(FinanceInvoiceBatchNumber, TextToInsert);
			
			return this;
		}

		public FinanceInvoiceBatchRecordPage ValidateRunontimeText(string ExpectedText)
		{
			ValidateElementValue(Runontime, ExpectedText);

			return this;
		}

		public FinanceInvoiceBatchRecordPage InsertTextOnRunontime(string TextToInsert)
		{
			WaitForElementToBeClickable(Runontime);
			SendKeys(Runontime, TextToInsert);
			
			return this;
		}

		public FinanceInvoiceBatchRecordPage ClickRunontimeDatePicker()
		{
			WaitForElementToBeClickable(RunontimeDatePicker);
			Click(RunontimeDatePicker);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ValidateRunontime_TimeText(string ExpectedText)
		{
			ValidateElementValue(Runontime_Time, ExpectedText);

			return this;
		}

		public FinanceInvoiceBatchRecordPage InsertTextOnRunontime_Time(string TextToInsert)
		{
			WaitForElementToBeClickable(Runontime_Time);
			SendKeys(Runontime_Time, TextToInsert);
			
			return this;
		}

		public FinanceInvoiceBatchRecordPage ClickRunontime_Time_TimePicker()
		{
			WaitForElementToBeClickable(Runontime_Time_TimePicker);
			Click(Runontime_Time_TimePicker);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ValidatePeriodstartdateText(string ExpectedText)
		{
			ValidateElementValue(PeriodStartDate, ExpectedText);

			return this;
		}

		public FinanceInvoiceBatchRecordPage InsertTextOnPeriodstartdate(string TextToInsert)
		{
			WaitForElementToBeClickable(PeriodStartDate);
			SendKeys(PeriodStartDate, TextToInsert);
			
			return this;
		}

		public FinanceInvoiceBatchRecordPage ClickPeriodstartdateDatePicker()
		{
			WaitForElementToBeClickable(PeriodStartDateDatePicker);
			Click(PeriodStartDateDatePicker);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ClickFinanceInvoiceBatchSetupLink()
		{
			WaitForElementToBeClickable(FinanceinvoicebatchsetupLink);
			Click(FinanceinvoicebatchsetupLink);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ValidateFinanceInvoiceBatchSetupLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(FinanceinvoicebatchsetupLink);
			ValidateElementText(FinanceinvoicebatchsetupLink, ExpectedText);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ClickFinanceInvoiceBatchSetupLookupButton()
		{
			WaitForElementToBeClickable(FinanceinvoicebatchsetupLookupButton);
			Click(FinanceinvoicebatchsetupLookupButton);

			return this;
		}

		public FinanceInvoiceBatchRecordPage SelectBatchStatus(string TextToSelect)
		{
			WaitForElementToBeClickable(BatchStatusId);
			SelectPicklistElementByText(BatchStatusId, TextToSelect);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ValidateBatchstatusSelectedText(string ExpectedText)
		{
			WaitForElementVisible(BatchStatusId);
			ValidatePicklistSelectedText(BatchStatusId, ExpectedText);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ValidatePeriodEndDateText(string ExpectedText)
		{
			WaitForElementVisible(PeriodEndDate);
			ValidateElementValue(PeriodEndDate, ExpectedText);

			return this;
		}

		public FinanceInvoiceBatchRecordPage InsertTextOnPeriodEndDate(string TextToInsert)
		{
			WaitForElementToBeClickable(PeriodEndDate);
			SendKeys(PeriodEndDate, TextToInsert);
			
			return this;
		}

		public FinanceInvoiceBatchRecordPage ClickPeriodEndDateDatePicker()
		{
			WaitForElementToBeClickable(PeriodEndDateDatePicker);
			Click(PeriodEndDateDatePicker);

			return this;
		}

		public FinanceInvoiceBatchRecordPage SelectFinanceModule(string TextToSelect)
		{
			WaitForElementToBeClickable(FinanceModuleId);
			SelectPicklistElementByText(FinanceModuleId, TextToSelect);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ValidateFinanceModuleSelectedText(string ExpectedText)
		{
			MoveToElementInPage(FinanceModuleId);
			WaitForElementVisible(FinanceModuleId);
			ValidatePicklistSelectedText(FinanceModuleId, ExpectedText);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ClickServiceElement1Link()
		{
			WaitForElementToBeClickable(ServiceElement1Link);
			Click(ServiceElement1Link);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ValidateServiceElement1LinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ServiceElement1Link);
			ValidateElementText(ServiceElement1Link, ExpectedText);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ClickServiceElement1LookupButton()
		{
			WaitForElementToBeClickable(ServiceElement1LookupButton);
			Click(ServiceElement1LookupButton);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ClickProviderBatchGroupingLink()
		{
			WaitForElementToBeClickable(ProviderBatchGroupingLink);
			Click(ProviderBatchGroupingLink);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ValidateProviderBatchGroupingLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ProviderBatchGroupingLink);
			ValidateElementText(ProviderBatchGroupingLink, ExpectedText);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ClickProviderBatchGroupingidLookupButton()
		{
			WaitForElementToBeClickable(ProviderBatchGroupingLookupButton);
			Click(ProviderBatchGroupingLookupButton);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ClickIsadhocbatch_YesButton()
		{
			WaitForElementToBeClickable(Isadhocbatch_1);
			Click(Isadhocbatch_1);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ValidateIsadhocbatch_YesChecked()
		{
			WaitForElement(Isadhocbatch_1);
			ValidateElementChecked(Isadhocbatch_1);
			
			return this;
		}

		public FinanceInvoiceBatchRecordPage ValidateIsadhocbatch_YesNotChecked()
		{
			WaitForElement(Isadhocbatch_1);
			ValidateElementNotChecked(Isadhocbatch_1);
			
			return this;
		}

		public FinanceInvoiceBatchRecordPage ClickIsadhocbatch_NoButton()
		{
			WaitForElementToBeClickable(Isadhocbatch_0);
			Click(Isadhocbatch_0);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ValidateIsadhocbatch_NoChecked()
		{
			WaitForElement(Isadhocbatch_0);
			ValidateElementChecked(Isadhocbatch_0);
			
			return this;
		}

		public FinanceInvoiceBatchRecordPage ValidateIsadhocbatch_NoNotChecked()
		{
			WaitForElement(Isadhocbatch_0);
			ValidateElementNotChecked(Isadhocbatch_0);
			
			return this;
		}

		public FinanceInvoiceBatchRecordPage ClickPaymentTypeLink()
		{
			WaitForElementToBeClickable(PaymentTypeLink);
			Click(PaymentTypeLink);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ValidatePaymentTypeLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(PaymentTypeLink);
			ValidateElementText(PaymentTypeLink, ExpectedText);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ClickPaymentTypeLookupButton()
		{
			WaitForElementToBeClickable(PaymentTypeLookupButton);
			Click(PaymentTypeLookupButton);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ValidateNetBatchTotalText(string ExpectedText)
		{
			ValidateElementValue(NetBatchTotal, ExpectedText);

			return this;
		}

		public FinanceInvoiceBatchRecordPage InsertTextOnNetBatchTotal(string TextToInsert)
		{
			WaitForElementToBeClickable(NetBatchTotal);
			SendKeys(NetBatchTotal, TextToInsert);
			
			return this;
		}

		public FinanceInvoiceBatchRecordPage ValidateVatTotalText(string ExpectedText)
		{
			ValidateElementValue(VatTotal, ExpectedText);

			return this;
		}

		public FinanceInvoiceBatchRecordPage InsertTextOnVatTotal(string TextToInsert)
		{
			WaitForElementToBeClickable(VatTotal);
			SendKeys(VatTotal, TextToInsert);
			
			return this;
		}

		public FinanceInvoiceBatchRecordPage ValidateNumberOfInvoicesCreatedText(string ExpectedText)
		{
			ValidateElementValue(NumberOfInvoicesCreated, ExpectedText);

			return this;
		}

		public FinanceInvoiceBatchRecordPage InsertTextOnNumberOfInvoicesCreated(string TextToInsert)
		{
			WaitForElementToBeClickable(NumberOfInvoicesCreated);
			SendKeys(NumberOfInvoicesCreated, TextToInsert);
			
			return this;
		}

		public FinanceInvoiceBatchRecordPage ClickUserLink()
		{
			WaitForElementToBeClickable(UserLink);
			Click(UserLink);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ValidateUserLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(UserLink);
			ValidateElementText(UserLink, ExpectedText);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ClickUserLookupButton()
		{
			WaitForElementToBeClickable(UserLookupButton);
			Click(UserLookupButton);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ValidateGrossBatchTotalText(string ExpectedText)
		{
			ValidateElementValue(GrossBatchTotal, ExpectedText);

			return this;
		}

		public FinanceInvoiceBatchRecordPage InsertTextOnGrossbatchtotal(string TextToInsert)
		{
			WaitForElementToBeClickable(GrossBatchTotal);
			SendKeys(GrossBatchTotal, TextToInsert);
			
			return this;
		}

		public FinanceInvoiceBatchRecordPage ValidateNumberOfuniqueProvidersPayeesText(string ExpectedText)
		{
			ValidateElementValue(NumberOfUniqueProvidersPayees, ExpectedText);

			return this;
		}

		public FinanceInvoiceBatchRecordPage InsertTextOnNumberOfUniqueProvidersPayees(string TextToInsert)
		{
			WaitForElementToBeClickable(NumberOfUniqueProvidersPayees);
			SendKeys(NumberOfUniqueProvidersPayees, TextToInsert);
			
			return this;
		}

		public FinanceInvoiceBatchRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

		public FinanceInvoiceBatchRecordPage ClickInvoicesTab()
        {
			WaitForElementToBeClickable(InvoicesTab);
			Click(InvoicesTab);

			return this;
        }

	}
}

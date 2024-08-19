using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.Finance
{
	public class FinanceTransactionRecordPage : CommonMethods
	{
		public FinanceTransactionRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
		{
			this.driver = driver;
			this.Wait = Wait;
			this.appURL = appURL;
		}

		readonly By contentIFrame = By.Id("CWContentIFrame");
		readonly By cwDialogIFrame_financeTransaction = By.XPath("//iframe[contains(@id, 'iframe_CWDialog')][contains(@src, 'type=financetransaction&')]");
		readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
		readonly By Financemodule = By.XPath("//*[@id='CWField_financemoduleid']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamClearButton = By.XPath("//*[@id='CWClearLookup_ownerid']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
		readonly By PersonLookupButton = By.XPath("//*[@id='CWLookupBtn_personid']");
		readonly By PersonLink = By.Id("CWField_personid_Link");
		readonly By PersonNumber = By.XPath("//*[@id='CWField_personnumber']");
		readonly By StartDate = By.XPath("//*[@id='CWField_startdate']");
		readonly By StartDateDatePicker = By.XPath("//*[@id='CWField_startdate_DatePicker']");
		readonly By TransactionNumber = By.XPath("//*[@id='CWField_transactionnumber']");
		readonly By NetAmount = By.XPath("//*[@id='CWField_netamount']");
		readonly By GrossAmount = By.XPath("//*[@id='CWField_grossamount']");
		readonly By Informationonly_1 = By.XPath("//*[@id='CWField_informationonly_1']");
		readonly By Informationonly_0 = By.XPath("//*[@id='CWField_informationonly_0']");
		readonly By EndDate = By.XPath("//*[@id='CWField_enddate']");
		readonly By EndDateDatePicker = By.XPath("//*[@id='CWField_enddate_DatePicker']");
		readonly By TransactionClass = By.XPath("//*[@id='CWField_transactionclassid']");
		readonly By VatAmount = By.XPath("//*[@id='CWField_vatamount']");
		readonly By VatCodeLookupButton = By.XPath("//*[@id='CWLookupBtn_vatcodeid']");
		readonly By VatCodeLink = By.Id("CWField_vatcodeid_Link");
		readonly By TransactionType = By.XPath("//*[@id='CWField_transactiontypeid']");
		readonly By FinanceInvoiceLink = By.XPath("//*[@id='CWField_financeinvoiceid_Link']");
		readonly By FinanceInvoiceLookupButton = By.XPath("//*[@id='CWLookupBtn_financeinvoiceid']");
		readonly By InvoiceStatusLink = By.XPath("//*[@id='CWField_invoicestatusid_Link']");
		readonly By InvoiceStatusLookupButton = By.XPath("//*[@id='CWLookupBtn_invoicestatusid']");
		readonly By InvoiceNo = By.XPath("//*[@id='CWField_invoiceno']");
		readonly By ProviderLookupButton = By.XPath("//*[@id='CWLookupBtn_providerid']");
		readonly By ProviderLink = By.XPath("//*[@id='CWField_providerid_Link']");
		readonly By ServiceProvisionLink = By.XPath("//*[@id='CWField_serviceprovisionid_Link']");
		readonly By ServiceProvisionLookupButton = By.XPath("//*[@id='CWLookupBtn_serviceprovisionid']");
		readonly By ServiceElement1LookupButton = By.XPath("//*[@id='CWLookupBtn_serviceelement1id']");
		readonly By ServiceElement1Link = By.XPath("//*[@id='CWField_serviceelement1id_Link']");
		readonly By TotalUnits = By.XPath("//*[@id='CWField_totalunits']");
		readonly By WhotoPay = By.XPath("//*[@id='CWField_whotopayid']");
		readonly By ServiceProvidedLink = By.XPath("//*[@id='CWField_serviceprovidedid_Link']");
		readonly By ServiceProvidedLookupButton = By.XPath("//*[@id='CWLookupBtn_serviceprovidedid']");
		readonly By RateUnitLookupButton = By.XPath("//*[@id='CWLookupBtn_rateunitid']");
		readonly By RateUnitLink = By.XPath("//*[@id='CWField_rateunitid_Link']");
		readonly By ActualUnits = By.XPath("//*[@id='CWField_actualunits']");
		readonly By FinancialAssessmentLink = By.XPath("//*[@id='CWField_financialassessmentid_Link']");
		readonly By FinancialAssessmentLookupButton = By.XPath("//*[@id='CWLookupBtn_financialassessmentid']");
		readonly By ContributionLink = By.XPath("//*[@id='CWField_contributionid_Link']");
		readonly By ContributionLookupButton = By.XPath("//*[@id='CWLookupBtn_contributionid']");
		readonly By TransactionText = By.XPath("//*[@id='CWField_transactiontext']");
		readonly By MaximumCharge = By.XPath("//*[@id='CWField_maximumcharge']");
		readonly By PayeeLink = By.XPath("//*[@id='CWField_payeeid_Link']");
		readonly By PayeeLookupButton = By.XPath("//*[@id='CWLookupBtn_payeeid']");
		readonly By ContributionTypeLink = By.XPath("//*[@id='CWField_contributiontypeid_Link']");
		readonly By ContributionTypeLookupButton = By.XPath("//*[@id='CWLookupBtn_contributiontypeid']");
		readonly By RuleTypeLink = By.XPath("//*[@id='CWField_ruletypeid_Link']");
		readonly By RuleTypeLookupButton = By.XPath("//*[@id='CWLookupBtn_ruletypeid']");
		readonly By CostOfServices = By.XPath("//*[@id='CWField_costofservices']");
		readonly By GlCode = By.XPath("//*[@id='CWField_glcode']");
		readonly By FeeLink = By.XPath("//*[@id='CWField_feeid_Link']");
		readonly By FeeLookupButton = By.XPath("//*[@id='CWLookupBtn_feeid']");
		readonly By AllowanceLink = By.XPath("//*[@id='CWField_allowanceid_Link']");
		readonly By AllowanceLookupButton = By.XPath("//*[@id='CWLookupBtn_allowanceid']");
		readonly By CarerRateUnitLink = By.XPath("//*[@id='CWField_carerrateunitid_Link']");
		readonly By CarerRateUnitLookupButton = By.XPath("//*[@id='CWLookupBtn_carerrateunitid']");
		readonly By PayeeType = By.XPath("//*[@id='CWField_payeetypeid']");
		readonly By Units = By.XPath("//*[@id='CWField_units']");
		readonly By Notes = By.XPath("//*[@id='CWField_notes']");
		readonly By CalculationTrace = By.XPath("//*[@id='CWField_calculationtrace']");
		readonly By FinanceTransactionPageHeader = By.XPath("//h1");

		public FinanceTransactionRecordPage WaitForFinanceTransactionRecordPageToLoad()
		{
			SwitchToDefaultFrame();

			WaitForElement(contentIFrame);
			SwitchToIframe(contentIFrame);

			WaitForElement(cwDialogIFrame_financeTransaction);
			SwitchToIframe(cwDialogIFrame_financeTransaction);

			WaitForElementNotVisible("CWRefreshPanel", 100);

			WaitForElementVisible(FinanceTransactionPageHeader);

			return this;
		}


		public FinanceTransactionRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public FinanceTransactionRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			Click(SaveButton);

			return this;
		}

		public FinanceTransactionRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public FinanceTransactionRecordPage SelectFinanceModule(string TextToSelect)
		{
			WaitForElementToBeClickable(Financemodule);
			SelectPicklistElementByText(Financemodule, TextToSelect);

			return this;
		}

		public FinanceTransactionRecordPage ValidateFinanceModuleSelectedText(string ExpectedText)
		{
			WaitForElement(Financemodule);
			ValidatePicklistSelectedText(Financemodule, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public FinanceTransactionRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementByTitle(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage ClickResponsibleTeamClearButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamClearButton);
			Click(ResponsibleTeamClearButton);

			return this;
		}

		public FinanceTransactionRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

		public FinanceTransactionRecordPage ClickPersonLookupButton()
		{
			WaitForElementToBeClickable(PersonLookupButton);
			Click(PersonLookupButton);

			return this;
		}

		public FinanceTransactionRecordPage ValidatePersonLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(PersonLink);
			ValidateElementByTitle(PersonLink, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage ValidatePersonNumberText(string ExpectedText)
		{
			WaitForElementVisible(PersonNumber);
			ValidateElementValue(PersonNumber, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage InsertTextOnPersonNumber(string TextToInsert)
		{
			WaitForElement(PersonNumber);
			SendKeys(PersonNumber, TextToInsert);
			
			return this;
		}

		public FinanceTransactionRecordPage ValidateStartDateText(string ExpectedText)
		{
			MoveToElementInPage(StartDate);
			WaitForElementVisible(StartDate);
			ValidateElementValue(StartDate, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage InsertTextOnStartDate(string TextToInsert)
		{
			WaitForElementToBeClickable(StartDate);
			SendKeys(StartDate, TextToInsert);
			
			return this;
		}

		public FinanceTransactionRecordPage ClickStartDateDatePicker()
		{
			WaitForElementToBeClickable(StartDateDatePicker);
			Click(StartDateDatePicker);

			return this;
		}

		public FinanceTransactionRecordPage ValidateTransactionNumberText(string ExpectedText)
		{
			MoveToElementInPage(TransactionNumber);
			WaitForElementVisible(TransactionNumber);
			ValidateElementValue(TransactionNumber, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage InsertTextOnTransactionNumber(string TextToInsert)
		{
			WaitForElementToBeClickable(TransactionNumber);
			SendKeys(TransactionNumber, TextToInsert);
			
			return this;
		}

		public FinanceTransactionRecordPage ValidateNetAmountText(string ExpectedText)
		{
			MoveToElementInPage(NetAmount);
			WaitForElementVisible(NetAmount);
			ValidateElementValue(NetAmount, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage InsertTextOnNetAmount(string TextToInsert)
		{
			WaitForElementToBeClickable(NetAmount);
			SendKeys(NetAmount, TextToInsert);
			
			return this;
		}

		public FinanceTransactionRecordPage ValidateGrossAmountText(string ExpectedText)
		{
			MoveToElementInPage(GrossAmount);
			WaitForElementVisible(GrossAmount);
			ValidateElementValue(GrossAmount, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage InsertTextOnGrossAmount(string TextToInsert)
		{
			WaitForElementToBeClickable(GrossAmount);
			SendKeys(GrossAmount, TextToInsert);
			
			return this;
		}

		public FinanceTransactionRecordPage ClickInformationonly_YesOption()
		{
			WaitForElementToBeClickable(Informationonly_1);
			Click(Informationonly_1);

			return this;
		}

		public FinanceTransactionRecordPage ValidateInformationonly_YesOptionChecked()
		{
			WaitForElement(Informationonly_1);
			ValidateElementChecked(Informationonly_1);
			
			return this;
		}

		public FinanceTransactionRecordPage ValidateInformationonly_YesOptionNotChecked()
		{
			WaitForElement(Informationonly_1);
			ValidateElementNotChecked(Informationonly_1);
			
			return this;
		}

		public FinanceTransactionRecordPage ClickInformationonly_NoOption()
		{
			WaitForElementToBeClickable(Informationonly_0);
			Click(Informationonly_0);

			return this;
		}

		public FinanceTransactionRecordPage ValidateInformationonly_NoOptionChecked()
		{
			WaitForElement(Informationonly_0);
			ValidateElementChecked(Informationonly_0);
			
			return this;
		}

		public FinanceTransactionRecordPage ValidateInformationonly_NoOptionNotChecked()
		{
			WaitForElement(Informationonly_0);
			ValidateElementNotChecked(Informationonly_0);
			
			return this;
		}

		public FinanceTransactionRecordPage ValidateEndDateText(string ExpectedText)
		{
			MoveToElementInPage(EndDate);
			WaitForElementVisible(EndDate);
			ValidateElementValue(EndDate, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage InsertTextOnEndDate(string TextToInsert)
		{
			WaitForElementToBeClickable(EndDate);
			SendKeys(EndDate, TextToInsert);
			
			return this;
		}

		public FinanceTransactionRecordPage ClickEndDateDatePicker()
		{
			WaitForElementToBeClickable(EndDateDatePicker);
			Click(EndDateDatePicker);

			return this;
		}

		public FinanceTransactionRecordPage SelectTransactionClass(string TextToSelect)
		{
			WaitForElementToBeClickable(TransactionClass);
			SelectPicklistElementByText(TransactionClass, TextToSelect);

			return this;
		}

		public FinanceTransactionRecordPage ValidateTransactionClassSelectedText(string ExpectedText)
		{
			WaitForElement(TransactionClass);
			MoveToElementInPage(TransactionClass);
			ValidatePicklistSelectedText(TransactionClass, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage ValidateVatAmountText(string ExpectedText)
		{
			WaitForElement(VatAmount);
			MoveToElementInPage(VatAmount);
			ValidateElementValue(VatAmount, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage InsertTextOnVatAmount(string TextToInsert)
		{
			WaitForElement(VatAmount);
			MoveToElementInPage(VatAmount);
			SendKeys(VatAmount, TextToInsert);
			
			return this;
		}

		public FinanceTransactionRecordPage ClickVatCodeLookupButton()
		{
			WaitForElementToBeClickable(VatCodeLookupButton);
			Click(VatCodeLookupButton);

			return this;
		}

		public FinanceTransactionRecordPage ClickVatCodeLink()
		{
			WaitForElementToBeClickable(VatCodeLink);
			Click(VatCodeLink);

			return this;
		}

		public FinanceTransactionRecordPage ValidateVatCodeLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(VatCodeLink);
			ValidateElementByTitle(VatCodeLink, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage SelectTransactionType(string TextToSelect)
		{
			WaitForElementToBeClickable(TransactionType);
			SelectPicklistElementByText(TransactionType, TextToSelect);

			return this;
		}

		public FinanceTransactionRecordPage ValidateTransactiontypeSelectedText(string ExpectedText)
		{
			WaitForElement(TransactionType);
			MoveToElementInPage(TransactionType);
			ValidatePicklistSelectedText(TransactionType, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage ClickFinanceInvoiceLink()
		{
			WaitForElementToBeClickable(FinanceInvoiceLink);
			Click(FinanceInvoiceLink);

			return this;
		}

		public FinanceTransactionRecordPage ValidateFinanceInvoiceLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(FinanceInvoiceLink);
			ValidateElementByTitle(FinanceInvoiceLink, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage ClickFinanceInvoiceLookupButton()
		{
			WaitForElementToBeClickable(FinanceInvoiceLookupButton);
			Click(FinanceInvoiceLookupButton);

			return this;
		}

		public FinanceTransactionRecordPage ClickInvoiceStatusLink()
		{
			WaitForElementToBeClickable(InvoiceStatusLink);
			Click(InvoiceStatusLink);

			return this;
		}

		public FinanceTransactionRecordPage ValidateInvoiceStatusLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(InvoiceStatusLink);
			ValidateElementByTitle(InvoiceStatusLink, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage ClickInvoiceStatusLookupButton()
		{
			WaitForElementToBeClickable(InvoiceStatusLookupButton);
			Click(InvoiceStatusLookupButton);

			return this;
		}

		public FinanceTransactionRecordPage ValidateInvoiceNoText(string ExpectedText)
		{
			WaitForElement(InvoiceNo);
			MoveToElementInPage(InvoiceNo);
			ValidateElementValue(InvoiceNo, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage InsertTextOnInvoiceNo(string TextToInsert)
		{
			WaitForElementToBeClickable(InvoiceNo);
			SendKeys(InvoiceNo, TextToInsert);
			
			return this;
		}

		public FinanceTransactionRecordPage ClickProviderLookupButton()
		{
			WaitForElementToBeClickable(ProviderLookupButton);
			Click(ProviderLookupButton);

			return this;
		}

		public FinanceTransactionRecordPage ClickServiceProvisionLink()
		{
			WaitForElementToBeClickable(ServiceProvisionLink);
			Click(ServiceProvisionLink);

			return this;
		}

		public FinanceTransactionRecordPage ValidateServiceProvisionLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ServiceProvisionLink);
			ValidateElementByTitle(ServiceProvisionLink, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage ClickServiceProvisionLookupButton()
		{
			WaitForElementToBeClickable(ServiceProvisionLookupButton);
			Click(ServiceProvisionLookupButton);

			return this;
		}

		public FinanceTransactionRecordPage ClickServiceElement1LookupButton()
		{
			WaitForElementToBeClickable(ServiceElement1LookupButton);
			Click(ServiceElement1LookupButton);

			return this;
		}

		public FinanceTransactionRecordPage ValidateServiceElement1LinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ServiceElement1Link);
			ValidateElementByTitle(ServiceElement1Link, ExpectedText);

			return this;
		}
		public FinanceTransactionRecordPage ValidateTotalUnitsText(string ExpectedText)
		{
			WaitForElement(TotalUnits);
			MoveToElementInPage(TotalUnits);
			ValidateElementValue(TotalUnits, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage InsertTextOnTotalunits(string TextToInsert)
		{
			WaitForElementToBeClickable(TotalUnits);
			SendKeys(TotalUnits, TextToInsert);
			
			return this;
		}

		public FinanceTransactionRecordPage SelectWhoToPay(string TextToSelect)
		{
			WaitForElementToBeClickable(WhotoPay);
			SelectPicklistElementByText(WhotoPay, TextToSelect);

			return this;
		}

		public FinanceTransactionRecordPage ValidateWhoToPaySelectedText(string ExpectedText)
		{
			WaitForElement(WhotoPay);
			MoveToElementInPage(WhotoPay);
			ValidatePicklistSelectedText(WhotoPay, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage ClickServiceProvidedLink()
		{
			WaitForElementToBeClickable(ServiceProvidedLink);
			Click(ServiceProvidedLink);

			return this;
		}

		public FinanceTransactionRecordPage ValidateServiceProvidedLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ServiceProvidedLink);
			ValidateElementByTitle(ServiceProvidedLink, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage ClickServiceProvidedLookupButton()
		{
			WaitForElementToBeClickable(ServiceProvidedLookupButton);
			Click(ServiceProvidedLookupButton);

			return this;
		}

		public FinanceTransactionRecordPage ClickRateUnitLookupButton()
		{
			WaitForElementToBeClickable(RateUnitLookupButton);
			Click(RateUnitLookupButton);

			return this;
		}

		public FinanceTransactionRecordPage ValidateActualUnitsText(string ExpectedText)
		{
			WaitForElement(ActualUnits);
			MoveToElementInPage(ActualUnits);
			ValidateElementValue(ActualUnits, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage InsertTextOnActualUnits(string TextToInsert)
		{
			WaitForElementToBeClickable(ActualUnits);
			SendKeys(ActualUnits, TextToInsert);
			
			return this;
		}

		public FinanceTransactionRecordPage ClickFinancialAssessmentLink()
		{
			WaitForElementToBeClickable(FinancialAssessmentLink);
			Click(FinancialAssessmentLink);

			return this;
		}

		public FinanceTransactionRecordPage ValidateFinancialAssessmentLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(FinancialAssessmentLink);
			ValidateElementByTitle(FinancialAssessmentLink, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage ClickFinancialAssessmentLookupButton()
		{
			WaitForElementToBeClickable(FinancialAssessmentLookupButton);
			Click(FinancialAssessmentLookupButton);

			return this;
		}

		public FinanceTransactionRecordPage ClickContributionLink()
		{
			WaitForElementToBeClickable(ContributionLink);
			Click(ContributionLink);

			return this;
		}

		public FinanceTransactionRecordPage ValidateContributionLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ContributionLink);
			ValidateElementByTitle(ContributionLink, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage ClickContributionLookupButton()
		{
			WaitForElementToBeClickable(ContributionLookupButton);
			Click(ContributionLookupButton);

			return this;
		}

		public FinanceTransactionRecordPage ValidateTransactionTextText(string ExpectedText)
		{
			ValidateElementValue(TransactionText, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage InsertTextOnTransactiontext(string TextToInsert)
		{
			WaitForElementToBeClickable(TransactionText);
			SendKeys(TransactionText, TextToInsert);
			
			return this;
		}

		public FinanceTransactionRecordPage ValidateMaximumChargeText(string ExpectedText)
		{
			ValidateElementValue(MaximumCharge, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage InsertTextOnMaximumCharge(string TextToInsert)
		{
			WaitForElementToBeClickable(MaximumCharge);
			SendKeys(MaximumCharge, TextToInsert);
			
			return this;
		}

		public FinanceTransactionRecordPage ClickPayeeLink()
		{
			WaitForElementToBeClickable(PayeeLink);
			Click(PayeeLink);

			return this;
		}

		public FinanceTransactionRecordPage ValidatePayeeLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(PayeeLink);
			ValidateElementByTitle(PayeeLink, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage ClickPayeeLookupButton()
		{
			WaitForElementToBeClickable(PayeeLookupButton);
			Click(PayeeLookupButton);

			return this;
		}

		public FinanceTransactionRecordPage ClickContributionTypeLink()
		{
			WaitForElementToBeClickable(ContributionTypeLink);
			Click(ContributionTypeLink);

			return this;
		}

		public FinanceTransactionRecordPage ValidateContributionTypeLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ContributionTypeLink);
			ValidateElementByTitle(ContributionTypeLink, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage ClickContributionTypeLookupButton()
		{
			WaitForElementToBeClickable(ContributionTypeLookupButton);
			Click(ContributionTypeLookupButton);

			return this;
		}

		public FinanceTransactionRecordPage ClickRuleTypeLink()
		{
			WaitForElementToBeClickable(RuleTypeLink);
			Click(RuleTypeLink);

			return this;
		}

		public FinanceTransactionRecordPage ValidateRuleTypeLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(RuleTypeLink);
			ValidateElementByTitle(RuleTypeLink, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage ClickRuleTypeLookupButton()
		{
			WaitForElementToBeClickable(RuleTypeLookupButton);
			Click(RuleTypeLink);

			return this;
		}

		public FinanceTransactionRecordPage ValidateCostOfServicesText(string ExpectedText)
		{
			ValidateElementValue(CostOfServices, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage InsertTextOnCostOfServices(string TextToInsert)
		{
			WaitForElementToBeClickable(CostOfServices);
			SendKeys(CostOfServices, TextToInsert);
			
			return this;
		}

		public FinanceTransactionRecordPage ValidateGlCodeText(string ExpectedText)
		{
			WaitForElement(GlCode);
			MoveToElementInPage(GlCode);
			ValidateElementValue(GlCode, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage InsertTextOnGlCode(string TextToInsert)
		{
			WaitForElementToBeClickable(GlCode);
			SendKeys(GlCode, TextToInsert);
			
			return this;
		}

		public FinanceTransactionRecordPage ClickFeeLink()
		{
			WaitForElementToBeClickable(FeeLink);
			Click(FeeLink);

			return this;
		}

		public FinanceTransactionRecordPage ValidateFeeLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(FeeLink);
			ValidateElementByTitle(FeeLink, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage ClickFeeLookupButton()
		{
			WaitForElementToBeClickable(FeeLookupButton);
			Click(FeeLookupButton);

			return this;
		}

		public FinanceTransactionRecordPage ClickAllowanceLink()
		{
			WaitForElementToBeClickable(AllowanceLink);
			Click(AllowanceLink);

			return this;
		}

		public FinanceTransactionRecordPage ValidateAllowanceLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(AllowanceLink);
			ValidateElementByTitle(AllowanceLink, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage ClickAllowanceLookupButton()
		{
			WaitForElementToBeClickable(AllowanceLookupButton);
			Click(AllowanceLookupButton);

			return this;
		}

		public FinanceTransactionRecordPage ClickCarerRateUnitLink()
		{
			WaitForElementToBeClickable(CarerRateUnitLink);
			Click(CarerRateUnitLink);

			return this;
		}

		public FinanceTransactionRecordPage ValidateCarerRateUnitLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(CarerRateUnitLink);
			ValidateElementByTitle(CarerRateUnitLink, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage ClickCarerRateUnitLookupButton()
		{
			WaitForElementToBeClickable(CarerRateUnitLookupButton);
			Click(CarerRateUnitLookupButton);

			return this;
		}

		public FinanceTransactionRecordPage SelectPayeeType(string TextToSelect)
		{
			WaitForElementToBeClickable(PayeeType);
			SelectPicklistElementByText(PayeeType, TextToSelect);

			return this;
		}

		public FinanceTransactionRecordPage ValidatePayeeTypeSelectedText(string ExpectedText)
		{
			ValidateElementText(PayeeType, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage ValidateUnitsText(string ExpectedText)
		{
			ValidateElementValue(Units, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage InsertTextOnUnits(string TextToInsert)
		{
			WaitForElementToBeClickable(Units);
			SendKeys(Units, TextToInsert);
			
			return this;
		}

		public FinanceTransactionRecordPage ValidateNotesText(string ExpectedText)
		{
			ValidateElementText(Notes, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage InsertTextOnNotes(string TextToInsert)
		{
			WaitForElementToBeClickable(Notes);
			SendKeys(Notes, TextToInsert);
			
			return this;
		}

		public FinanceTransactionRecordPage ValidateCalculationTraceText(string ExpectedText)
		{
			ValidateElementText(CalculationTrace, ExpectedText);

			return this;
		}

		public FinanceTransactionRecordPage InsertTextOnCalculationTrace(string TextToInsert)
		{
			WaitForElementToBeClickable(CalculationTrace);
			SendKeys(CalculationTrace, TextToInsert);
			
			return this;
		}

		public FinanceTransactionRecordPage ValidateStartDateFieldDisplayed(bool ExpectedDisplayed)
		{			
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(StartDate);
			}
			else
			{
				WaitForElementNotVisible(StartDate, 3);
			}
			return this;
		}

		public FinanceTransactionRecordPage ValidateEndDateFieldDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(EndDate);
			}
			else
			{
				WaitForElementNotVisible(EndDate, 3);
			}
			return this;
		}

		public FinanceTransactionRecordPage ValidateNetAmountFieldDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(NetAmount);
			}
			else
			{
				WaitForElementNotVisible(NetAmount, 3);
			}
			return this;
		}

		public FinanceTransactionRecordPage ValidateVatAmountFieldDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(VatAmount);
			}
			else
			{
				WaitForElementNotVisible(VatAmount, 3);
			}
			return this;
		}

		public FinanceTransactionRecordPage ValidateGLCodeFieldDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(GlCode);
			}
			else
			{
				WaitForElementNotVisible(GlCode, 3);
			}
			return this;
		}

		public FinanceTransactionRecordPage ValidateVatCodeFieldDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(VatCodeLink);
			}
			else
			{
				WaitForElementNotVisible(VatCodeLink, 3);
			}
			return this;
		}

		public FinanceTransactionRecordPage ValidateFinanceInvoiceFieldDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(FinanceInvoiceLink);
			}
			else
			{
				WaitForElementNotVisible(FinanceInvoiceLink, 3);
			}
			return this;
		}

		public FinanceTransactionRecordPage ValidateFinanceInvoiceFieldDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(FinanceInvoiceLookupButton);
			}
			else
			{
				ValidateElementNotDisabled(FinanceInvoiceLookupButton);
			}
			return this;
		}

		public FinanceTransactionRecordPage ValidateServiceProvisionFieldDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(ServiceProvisionLink);
			}
			else
			{
				WaitForElementNotVisible(ServiceProvisionLink, 3);
			}
			return this;
		}

		public FinanceTransactionRecordPage ValidateNotesFieldDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(Notes);
			}
			else
			{
				WaitForElementNotVisible(Notes, 3);
			}
			return this;
		}

		public FinanceTransactionRecordPage ValidateFinanceModulePicklistDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(Financemodule);
			}
			else
			{
				WaitForElementNotVisible(Financemodule, 3);
			}
			return this;
		}

		public FinanceTransactionRecordPage ValidateResponsibleTeamLinkFieldDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(ResponsibleTeamLink);
			}
			else
			{
				WaitForElementNotVisible(ResponsibleTeamLink, 3);
			}
			return this;
		}

		public FinanceTransactionRecordPage ValidatePersonLinkFieldDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(PersonLink);
			}
			else
			{
				WaitForElementNotVisible(PersonLink, 3);
			}
			return this;
		}

		public FinanceTransactionRecordPage ValidatePersonNumberFieldDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(PersonNumber);
			}
			else
			{
				WaitForElementNotVisible(PersonNumber, 3);
			}
			return this;
		}

		public FinanceTransactionRecordPage ValidateTransactionNumberFieldDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(TransactionNumber);
			}
			else
			{
				WaitForElementNotVisible(TransactionNumber, 3);
			}
			return this;
		}

		public FinanceTransactionRecordPage ValidateTransactionClassFieldDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(TransactionClass);
			}
			else
			{
				WaitForElementNotVisible(TransactionClass, 3);
			}
			return this;
		}

		public FinanceTransactionRecordPage ValidateGrossAmountFieldDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(GrossAmount);
			}
			else
			{
				WaitForElementNotVisible(GrossAmount, 3);
			}
			return this;
		}

		public FinanceTransactionRecordPage ValidateTransactionTypePicklistDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(TransactionType);
			}
			else
			{
				WaitForElementNotVisible(TransactionType, 3);
			}
			return this;
		}
		public FinanceTransactionRecordPage ValidateFinanceInvoiceLinkFieldDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(FinanceInvoiceLink);
			}
			else
			{
				WaitForElementNotVisible(FinanceInvoiceLink, 3);
			}
			return this;
		}
		public FinanceTransactionRecordPage ValidateInvoiceStatusFieldDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(InvoiceStatusLink);
			}
			else
			{
				WaitForElementNotVisible(InvoiceStatusLink, 3);
			}
			return this;
		}
		public FinanceTransactionRecordPage ValidateInvoiceNumberFieldDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(InvoiceNo);
			}
			else
			{
				WaitForElementNotVisible(InvoiceNo, 3);
			}
			return this;
		}
		public FinanceTransactionRecordPage ValidateProviderLinkFieldDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(ProviderLink);
			}
			else
			{
				WaitForElementNotVisible(ProviderLink, 3);
			}
			return this;
		}
		public FinanceTransactionRecordPage ValidateServiceProvisionLinkFieldDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(ServiceProvisionLink);
			}
			else
			{
				WaitForElementNotVisible(ServiceProvisionLink, 3);
			}
			return this;
		}
		public FinanceTransactionRecordPage ValidateServiceElement1LinkFieldDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(ServiceElement1Link);
			}
			else
			{
				WaitForElementNotVisible(ServiceElement1Link, 3);
			}
			return this;
		}

		public FinanceTransactionRecordPage ValidateWhoToPayPicklistFieldDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(WhotoPay);
			}
			else
			{
				WaitForElementNotVisible(WhotoPay, 3);
			}
			return this;
		}

		public FinanceTransactionRecordPage ValidateRateUnitLinkFieldDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(RateUnitLink);
			}
			else
			{
				WaitForElementNotVisible(RateUnitLink, 3);
			}
			return this;
		}

		public FinanceTransactionRecordPage ValidateTotalUnitsFieldDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(TotalUnits);
			}
			else
			{
				WaitForElementNotVisible(TotalUnits, 3);
			}
			return this;
		}

		public FinanceTransactionRecordPage ValidateActualUnitsFieldDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(ActualUnits);
			}
			else
			{
				WaitForElementNotVisible(ActualUnits, 3);
			}
			return this;
		}

		public FinanceTransactionRecordPage ValidateInformationOnlyOptionsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
            {
				MoveToElementInPage(Informationonly_1);
				WaitForElementVisible(Informationonly_1);
				WaitForElementVisible(Informationonly_0);
            }
            else
            {
				WaitForElementNotVisible(Informationonly_1, 3);
				WaitForElementNotVisible(Informationonly_0, 3);
			}

			return this;
        }
	}
}

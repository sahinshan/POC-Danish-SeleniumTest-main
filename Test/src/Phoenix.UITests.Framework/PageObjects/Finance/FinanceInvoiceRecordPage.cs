using OpenQA.Selenium;
using System.Runtime.CompilerServices;

namespace Phoenix.UITests.Framework.PageObjects.Finance
{
	public class FinanceInvoiceRecordPage : CommonMethods
	{
		public FinanceInvoiceRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
		{
			this.driver = driver;
			this.Wait = Wait;
			this.appURL = appURL;
		}

		readonly By contentIFrame = By.Id("CWContentIFrame");
		readonly By cwDialogIFrame_financeInvoiceBatch = By.XPath("//iframe[contains(@id, 'iframe_CWDialog')][contains(@src, 'type=financeinvoice&')]");
        readonly By cwDialogIFrame_cpFinanceInvoiceBatch = By.XPath("//iframe[contains(@id, 'iframe_CWDialog')][contains(@src, 'type=careproviderfinanceinvoice&')]");
        By cwDialog_IFrame(string parentRecordIdSuffix) => By.XPath("//iframe[contains(@id, 'iframe_CWDialog_" + parentRecordIdSuffix + "')]");
		readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");
		readonly By ToolbarMenuButton = By.Id("CWToolbarMenu");
		readonly By ReadyToAuthoriseButton = By.Id("TI_ReadyToAuthorize");
		readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
		readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");

		readonly By FinanceModulePicklist = By.XPath("//*[@id='CWField_financemoduleid']");

		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");

		readonly By InvoiceBatchLookupButton = By.Id("CWLookupBtn_invoicebatchid");
		readonly By InvoiceBatchLinkField = By.Id("CWField_invoicebatchid_Link");
		readonly By InvoiceStatusLookupButton = By.Id("CWLookupBtn_invoicestatusid");
		readonly By InvoiceStatusLinkField = By.Id("CWField_invoicestatusid_Link");
		readonly By InvoiceStatusClearButton = By.Id("CWClearLookup_invoicestatusid");

		readonly By AuthorisationErrorLookupButton = By.Id("CWLookupBtn_authorisationerrorid");
		readonly By AuthorisationErrorLinkField = By.Id("CWField_authorisationerrorid_Link");

		readonly By ProviderOrPersonLinkField = By.Id("CWField_providerorpersonid_Link");
		readonly By ProviderOrPersonLookupButton = By.Id("CWLookupBtn_providerorpersonid");

		readonly By CreditReferenceNumberField = By.Id("CWField_creditorreferencenumber");

		readonly By ServiceElement1LinkField = By.XPath("//*[@id='CWField_serviceelement1id_Link']");
		readonly By ServiceElement1LookupButton = By.XPath("//*[@id='CWLookupBtn_serviceelement1id']");

		readonly By PaymentTypeLinkField = By.XPath("//*[@id='CWField_paymenttypeid_Link']");
		readonly By PaymentTypeLookupButton = By.XPath("//*[@id='CWLookupBtn_paymenttypeid']");

		readonly By PurchasingTeamLookupButton = By.Id("CWLookupBtn_purchasingteamid");
		readonly By PurchasingTeamLinkField = By.Id("CWField_purchasingteamid_Link");
		readonly By PersonLookupButton = By.Id("CWLookupBtn_personid");
		readonly By PersonLinkField = By.Id("CWField_personid_Link");

		readonly By TotalUnitsField = By.Id("CWField_totalunits");
		readonly By ActualUnitsField = By.Id("CWField_actualunits");

		readonly By InvoiceNumberField = By.Id("CWField_invoicenumber");
		readonly By ProviderInvoiceNumberField = By.Id("CWField_providerinvoicenumber");
		readonly By InvoiceDateField = By.Id("CWField_invoicedate");
		readonly By InvoiceReceivedDateField = By.Id("CWField_invoicereceiveddate");
		readonly By TransactionsUpToField = By.Id("CWField_transactionsupto");
		readonly By NetAmountField = By.Id("CWField_netamount");
		readonly By GrossAmountField = By.Id("CWField_grossamount");
		readonly By VATAmountField = By.Id("CWField_vatamount");
		readonly By ValuesVerifiedYesOption = By.Id("CWField_valuesverified_1");
		readonly By ValuesVerifiedNoOption = By.Id("CWField_valuesverified_0");
		readonly By FinanceInvoiceRecordPageHeader = By.XPath("//h1");
		readonly By DetailsTab = By.Id("CWNavGroup_EditForm");
		readonly By FinanceTransactionTab = By.XPath("//li/a[@title='Finance Transactions']");
		readonly By AdditionalTransactionsTab = By.XPath("//li[@id='CWNavGroup_AdditionalTransaction']");

		readonly By MenuButton = By.Id("CWNavGroup_Menu");		        
        readonly By RelatedItemsSubMenuButton = By.XPath("//details/summary/div/span[text()='Related Items']");
        readonly By RelatedItemsDetailsElementExpanded = By.XPath("//span[text()='Related Items']/parent::div/parent::summary/parent::details[@open]");
        readonly By AuditHistorySubMenuItemButton = By.Id("CWNavItem_AuditHistory");

		#region Care Provide Finance Invoice Record Page

		readonly By ToolBarMenuButton = By.XPath("//*[@id = 'CWToolbarMenu']/button");
        readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");

        readonly By PaymentsTab = By.XPath("//li[@id = 'CWNavGroup_Payments']");
		//Payment Sub Menu Item Button
		readonly By PaymentSubMenuItemButton = By.XPath("//*[@id = 'CWNavItem_Payment']");
		readonly By PaymentsMadeField = By.Id("CWField_paymentsmade");
		readonly By DebtWrittenOffField = By.Id("CWField_debtwrittenoff");
		readonly By BalanceOutstandingField = By.Id("CWField_balanceoutstanding");
		readonly By FinanceInvoiceStatusPicklist = By.Id("CWField_financeinvoicestatusid");
        readonly By EstablishmentLookupButton = By.Id("CWLookupBtn_establishmentproviderid");
		readonly By EstablishmentLinkField = By.Id("CWField_establishmentproviderid_Link");
		readonly By DirectDebitExtractBatchLookupButton = By.Id("CWLookupBtn_directdebitextractbatchid");
		readonly By DirectDebitExtractBatchLinkField = By.Id("CWField_directdebitextractbatchid_Link");
		readonly By CustomerAccountCode = By.Id("CWField_customeraccountcode");

		readonly By AlternativeInvoiceNumberField = By.Id("CWField_alternativeinvoicenumber");
		readonly By ChargesUpto_DateField = By.Id("CWField_chargesupto");

		readonly By IncludeInDebtOutstanding_YesOption = By.Id("CWField_includeindebtoutstanding_1");
		readonly By IncludeInDebtOutstanding_NoOption = By.Id("CWField_includeindebtoutstanding_0");

		readonly By FinanceInvoiceBatchLookupButton = By.Id("CWLookupBtn_careproviderfinanceinvoicebatchid");
		readonly By FinanceInvoiceBatchLinkField = By.Id("CWField_careproviderfinanceinvoicebatchid_Link");
		readonly By ExtractLookupButton = By.Id("CWLookupBtn_careproviderfinanceextractbatchid");
		readonly By ExtractLinkField = By.Id("CWField_careproviderfinanceextractbatchid_Link");
		readonly By ContractSchemeLookupButton = By.Id("CWLookupBtn_careprovidercontractschemeid");
		readonly By ContractSchemeLinkField = By.Id("CWField_careprovidercontractschemeid_Link");
		readonly By BatchGroupingLookupButton = By.Id("CWLookupBtn_careproviderbatchgroupingid");
		readonly By BatchGroupingLinkField = By.Id("CWField_careproviderbatchgroupingid_Link");
		readonly By FunderLookupButton = By.Id("CWLookupBtn_funderproviderid");
		readonly By FunderLinkField = By.Id("CWField_funderproviderid_Link");
		readonly By DebtorReferenceNumberField = By.Id("CWField_debtorreferencenumber");		
		readonly By InvoiceFile = By.Id("CWField_invoicefile_FileLink");
		readonly By InvoiceDeliveryStatus = By.Id("CWField_careproviderdeliverystatusid");
		readonly By InvoiceTextDetailsField = By.Id("CWField_invoicetext");
		readonly By NoteText = By.Id("CWField_notetext");

        #endregion

        public FinanceInvoiceRecordPage WaitForFinanceInvoiceRecordPageToLoad()
		{
			SwitchToDefaultFrame();

			WaitForElement(contentIFrame);
			SwitchToIframe(contentIFrame);

			WaitForElement(cwDialogIFrame_financeInvoiceBatch);
			SwitchToIframe(cwDialogIFrame_financeInvoiceBatch);

			WaitForElementNotVisible("CWRefreshPanel", 100);

			WaitForElementVisible(FinanceInvoiceRecordPageHeader);

			return this;
		}

		public FinanceInvoiceRecordPage WaitForCPFinanceInvoiceRecordPageToLoad()
		{
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(cwDialogIFrame_cpFinanceInvoiceBatch);
            SwitchToIframe(cwDialogIFrame_cpFinanceInvoiceBatch);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(FinanceInvoiceRecordPageHeader);

            return this;
        }

		public FinanceInvoiceRecordPage WaitForFinanceInvoiceRecordPageToLoad(string parentRecordIdSuffix)
		{
			SwitchToDefaultFrame();

			WaitForElement(contentIFrame);
			SwitchToIframe(contentIFrame);

			WaitForElement(cwDialog_IFrame(parentRecordIdSuffix));
			SwitchToIframe(cwDialog_IFrame(parentRecordIdSuffix));

			WaitForElementNotVisible("CWRefreshPanel", 100);

			WaitForElementVisible(FinanceInvoiceRecordPageHeader);

			return this;
		}

		public FinanceInvoiceRecordPage WaitForFinanceInvoiceRecordPageToLoadFromAdvancedSearch(string frameId)
		{
			SwitchToDefaultFrame();

			WaitForElement(contentIFrame);
			SwitchToIframe(contentIFrame);

			WaitForElement(iframe_CWDataFormDialog);
			SwitchToIframe(iframe_CWDataFormDialog);

			WaitForElement(cwDialog_IFrame(frameId));
			SwitchToIframe(cwDialog_IFrame(frameId));

			WaitForElementNotVisible("CWRefreshPanel", 100);
			return this;
		}

		public FinanceInvoiceRecordPage NavigateToAuditPage()
		{
			WaitForElementToBeClickable(MenuButton);
			MoveToElementInPage(MenuButton);
			Click(MenuButton);

			WaitForElementToBeClickable(RelatedItemsSubMenuButton);
			MoveToElementInPage(RelatedItemsSubMenuButton);
			Click(RelatedItemsSubMenuButton);

			WaitForElementToBeClickable(AuditHistorySubMenuItemButton);
			MoveToElementInPage(AuditHistorySubMenuItemButton);
			Click(AuditHistorySubMenuItemButton);

			return this;
		}


		public FinanceInvoiceRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			ScrollToElement(BackButton);
			Click(BackButton);

			return this;
		}

		public FinanceInvoiceRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			Click(SaveButton);

			return this;
		}

		public FinanceInvoiceRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public FinanceInvoiceRecordPage ClickReadyToAuthoriseButton()
		{
			WaitForElementToBeClickable(ReadyToAuthoriseButton);
			MoveToElementInPage(ReadyToAuthoriseButton);
			Click(ReadyToAuthoriseButton);

			return this;
		}
		public FinanceInvoiceRecordPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}

		public FinanceInvoiceRecordPage ClickValuesVerifiedYesOption()
		{
			MoveToElementInPage(ValuesVerifiedYesOption);
			WaitForElementToBeClickable(ValuesVerifiedYesOption);
			Click(ValuesVerifiedYesOption);

			return this;
		}

		public FinanceInvoiceRecordPage ClickValuesVerifiedNoOption()
		{
			MoveToElementInPage(ValuesVerifiedNoOption);
			WaitForElementToBeClickable(ValuesVerifiedNoOption);
			Click(ValuesVerifiedNoOption);

			return this;
		}

		public FinanceInvoiceRecordPage ClickDetailsTab()
		{
			WaitForElementToBeClickable(DetailsTab);
			MoveToElementInPage(DetailsTab);
			Click(DetailsTab);

			return this;
		}

		public FinanceInvoiceRecordPage ClickFinanceTransactionsTab()
		{
			WaitForElementToBeClickable(FinanceTransactionTab);
			MoveToElementInPage(FinanceTransactionTab);
			Click(FinanceTransactionTab);

			return this;
		}

		public FinanceInvoiceRecordPage ClickAdditionalTransactionsTab()
		{
			WaitForElementToBeClickable(AdditionalTransactionsTab);
			MoveToElementInPage(AdditionalTransactionsTab);
			Click(AdditionalTransactionsTab);

			return this;
		}

		public FinanceInvoiceRecordPage InsertTextOnProviderInvoiceNumberField(string TextToInsert)
		{
			MoveToElementInPage(ProviderInvoiceNumberField);
			WaitForElementToBeClickable(ProviderInvoiceNumberField);
			SendKeys(ProviderInvoiceNumberField, TextToInsert);
			SendKeysWithoutClearing(ProviderInvoiceNumberField, Keys.Tab);

			return this;
		}

		public FinanceInvoiceRecordPage InsertTextOnInvoiceDateField(string TextToInsert)
		{
			MoveToElementInPage(InvoiceDateField);
			WaitForElementToBeClickable(InvoiceDateField);
			SendKeys(InvoiceDateField, TextToInsert);
			SendKeysWithoutClearing(InvoiceDateField, Keys.Tab);

			return this;
		}

		public FinanceInvoiceRecordPage InsertTextOnInvoiceReceivedDateField(string TextToInsert)
		{
			MoveToElementInPage(InvoiceReceivedDateField);
			WaitForElementToBeClickable(InvoiceReceivedDateField);
			SendKeys(InvoiceReceivedDateField, TextToInsert);
			SendKeysWithoutClearing(InvoiceReceivedDateField, Keys.Tab);

			return this;
		}

		public FinanceInvoiceRecordPage ValidateFinanceModulePicklistText(string ExpectedText)
		{
			WaitForElementVisible(FinanceModulePicklist);
			ValidatePicklistSelectedText(FinanceModulePicklist, ExpectedText);

			return this;
		}

		public FinanceInvoiceRecordPage ValidateFinanceModulePicklistFieldDisabled(bool ExpectedDisabled)
		{
			WaitForElementVisible(FinanceModulePicklist);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(FinanceModulePicklist);
			}
			else
			{
				ValidateElementNotDisabled(FinanceModulePicklist);
			}
			return this;
		}

		public FinanceInvoiceRecordPage ValidateInvoiceBatchFieldText(string ExpectedText)
		{
			WaitForElementVisible(InvoiceBatchLinkField);
			ValidateElementByTitle(InvoiceBatchLinkField, ExpectedText);

			return this;
		}

		public FinanceInvoiceRecordPage ValidateInvoiceBatchLookupButtonDisabled(bool ExpectedDisabled)
		{
			WaitForElementVisible(InvoiceBatchLookupButton);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(InvoiceBatchLookupButton);
			}
			else
			{
				ValidateElementNotDisabled(InvoiceBatchLookupButton);
			}
			return this;
		}

		public FinanceInvoiceRecordPage ClickInvoiceStatusFieldLookupButton()
		{
			WaitForElementVisible(InvoiceStatusLookupButton);
			MoveToElementInPage(InvoiceStatusLookupButton);
			Click(InvoiceStatusLookupButton);

			return this;
		}

		public FinanceInvoiceRecordPage ClickInvoiceStatusFieldClearButton()
		{
			WaitForElementVisible(InvoiceStatusClearButton);
			MoveToElementInPage(InvoiceStatusClearButton);
			Click(InvoiceStatusClearButton);

			return this;
		}

		public FinanceInvoiceRecordPage ValidateInvoiceStatusFieldText(string ExpectedText)
		{
			WaitForElementVisible(InvoiceStatusLinkField);
			ScrollToElement(InvoiceStatusLinkField);
			ValidateElementByTitle(InvoiceStatusLinkField, ExpectedText);

			return this;
		}

		public FinanceInvoiceRecordPage ValidateInvoiceStatusLookupButtonDisabled(bool ExpectedDisabled)
		{
			WaitForElementVisible(InvoiceStatusLookupButton);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(InvoiceStatusLookupButton);
			}
			else
			{
				ValidateElementNotDisabled(InvoiceStatusLookupButton);
			}
			return this;
		}

		public FinanceInvoiceRecordPage ValidateAuthorisationErrorFieldText(string ExpectedText)
		{
			WaitForElementVisible(AuthorisationErrorLinkField);
			ValidateElementByTitle(AuthorisationErrorLinkField, ExpectedText);

			return this;
		}
		public FinanceInvoiceRecordPage ValidateAuthorisationErrorLookupButtonDisabled(bool ExpectedDisabled)
		{
			WaitForElementVisible(AuthorisationErrorLookupButton);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(AuthorisationErrorLookupButton);
			}
			else
			{
				ValidateElementNotDisabled(AuthorisationErrorLookupButton);
			}
			return this;
		}

		public FinanceInvoiceRecordPage ValidatePayerFieldText(string ExpectedText)
		{
			WaitForElementVisible(ProviderOrPersonLinkField);
			ValidateElementText(ProviderOrPersonLinkField, ExpectedText);

			return this;
		}
		public FinanceInvoiceRecordPage ValidatePayerFieldLookupButtonDisabled(bool ExpectedDisabled)
		{
			WaitForElementVisible(ProviderOrPersonLookupButton);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(ProviderOrPersonLookupButton);
			}
			else
			{
				ValidateElementNotDisabled(ProviderOrPersonLookupButton);
			}
			return this;
		}

		//Method to verify ProviderLookupButton is visible or not visible
		public FinanceInvoiceRecordPage ValidatePayerLookupButtonVisible(bool ExpectedVisible)
		{
			if (ExpectedVisible)
			{
				WaitForElementVisible(ProviderOrPersonLookupButton);
			}
			else
			{
				WaitForElementNotVisible(ProviderOrPersonLookupButton, 2);
			}
			return this;
		}

		public FinanceInvoiceRecordPage ValidateServiceElement1FieldText(string ExpectedText)
		{
			WaitForElementVisible(ServiceElement1LinkField);
			ValidateElementByTitle(ServiceElement1LinkField, ExpectedText);

			return this;
		}
		public FinanceInvoiceRecordPage ValidateServiceElement1FieldLookupButtonDisabled(bool ExpectedDisabled)
		{
			WaitForElementVisible(ServiceElement1LookupButton);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(ServiceElement1LookupButton);
			}
			else
			{
				ValidateElementNotDisabled(ServiceElement1LookupButton);
			}
			return this;
		}

		public FinanceInvoiceRecordPage ValidatePurchasingTeamFieldText(string ExpectedText)
		{
			WaitForElementVisible(PurchasingTeamLinkField);
			ValidateElementByTitle(PurchasingTeamLinkField, ExpectedText);

			return this;
		}
		public FinanceInvoiceRecordPage ValidatePurchasingTeamFieldLookupButtonDisabled(bool ExpectedDisabled)
		{
			WaitForElementVisible(PurchasingTeamLookupButton);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(PurchasingTeamLookupButton);
			}
			else
			{
				ValidateElementNotDisabled(PurchasingTeamLookupButton);
			}
			return this;
		}

		public FinanceInvoiceRecordPage ValidateTotalUnitsFieldText(string ExpectedText)
		{
			WaitForElementVisible(TotalUnitsField);
			ValidateElementValue(TotalUnitsField, ExpectedText);

			return this;
		}
		public FinanceInvoiceRecordPage ValidateTotalUnitsFieldDisabled(bool ExpectedDisabled)
		{
			WaitForElementVisible(TotalUnitsField);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(TotalUnitsField);
			}
			else
			{
				ValidateElementNotDisabled(TotalUnitsField);
			}
			return this;
		}

		public FinanceInvoiceRecordPage ValidateActuallUnitsFieldText(string ExpectedText)
		{
			WaitForElementVisible(ActualUnitsField);
			ValidateElementValue(ActualUnitsField, ExpectedText);

			return this;
		}
		public FinanceInvoiceRecordPage ValidateActualUnitsFieldDisabled(bool ExpectedDisabled)
		{
			WaitForElementVisible(ActualUnitsField);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(ActualUnitsField);
			}
			else
			{
				ValidateElementNotDisabled(ActualUnitsField);
			}
			return this;
		}

		public FinanceInvoiceRecordPage ValidateCreditorReferenceFieldText(string ExpectedText)
		{
			WaitForElementVisible(CreditReferenceNumberField);
			ValidateElementValue(CreditReferenceNumberField, ExpectedText);

			return this;
		}
		public FinanceInvoiceRecordPage ValidateCreditorReferenceFieldDisabled(bool ExpectedDisabled)
		{
			WaitForElementVisible(CreditReferenceNumberField);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(CreditReferenceNumberField);
			}
			else
			{
				ValidateElementNotDisabled(CreditReferenceNumberField);
			}
			return this;
		}

		public FinanceInvoiceRecordPage ValidatePaymentTypeFieldText(string ExpectedText)
		{
			WaitForElementVisible(PaymentTypeLinkField);
			ValidateElementByTitle(PaymentTypeLinkField, ExpectedText);

			return this;
		}
		public FinanceInvoiceRecordPage ValidatePaymentTypeFieldLookupButtonDisabled(bool ExpectedDisabled)
		{
			WaitForElementVisible(PaymentTypeLookupButton);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(PaymentTypeLookupButton);
			}
			else
			{
				ValidateElementNotDisabled(PaymentTypeLookupButton);
			}
			return this;
		}

		public FinanceInvoiceRecordPage ValidatePersonFieldText(string ExpectedText)
		{
			WaitForElement(PersonLinkField);
			ValidateElementText(PersonLinkField, ExpectedText);

			return this;
		}
		public FinanceInvoiceRecordPage ValidatePersonLinkFieldLookupButtonDisabled(bool ExpectedDisabled)
		{
			WaitForElementVisible(PersonLookupButton);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(PersonLookupButton);
			}
			else
			{
				ValidateElementNotDisabled(PersonLookupButton);
			}
			return this;
		}

		public FinanceInvoiceRecordPage ValidateInvoiceNumberFieldText(string ExpectedText)
		{
			WaitForElementVisible(InvoiceNumberField);
			ValidateElementValue(InvoiceNumberField, ExpectedText);

			return this;
		}
		public FinanceInvoiceRecordPage ValidateInvoiceNumberFieldDisabled(bool ExpectedDisabled)
		{
			WaitForElement(InvoiceNumberField);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(InvoiceNumberField);
			}
			else
			{
				ValidateElementNotDisabled(InvoiceNumberField);
			}
			return this;
		}

		public FinanceInvoiceRecordPage ValidateInvoiceDateFieldText(string ExpectedText)
		{
			ScrollToElement(InvoiceDateField);
			WaitForElementVisible(InvoiceDateField);
			ValidateElementValue(InvoiceDateField, ExpectedText);

			return this;
		}
		public FinanceInvoiceRecordPage ValidateInvoiceDateFieldDisabled(bool ExpectedDisabled)
		{
			MoveToElementInPage(InvoiceDateField);
			WaitForElementVisible(InvoiceDateField);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(InvoiceDateField);
			}
			else
			{
				ValidateElementNotDisabled(InvoiceDateField);
			}
			return this;
		}

		public FinanceInvoiceRecordPage ValidateTransactionsUpToFieldText(string ExpectedText)
		{
			WaitForElementVisible(TransactionsUpToField);
			ValidateElementValue(TransactionsUpToField, ExpectedText);

			return this;
		}
		public FinanceInvoiceRecordPage ValidateTransactionsUpToFieldDisabled(bool ExpectedDisabled)
		{
			WaitForElementVisible(TransactionsUpToField);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(TransactionsUpToField);
			}
			else
			{
				ValidateElementNotDisabled(TransactionsUpToField);
			}
			return this;
		}

		public FinanceInvoiceRecordPage ValidateProviderInvoiceNumberFieldText(string ExpectedText)
		{
			WaitForElementVisible(ProviderInvoiceNumberField);
			ValidateElementValue(ProviderInvoiceNumberField, ExpectedText);

			return this;
		}
		public FinanceInvoiceRecordPage ValidateProviderInvoiceNumberFieldDisabled(bool ExpectedDisabled)
		{
			WaitForElementVisible(ProviderInvoiceNumberField);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(ProviderInvoiceNumberField);
			}
			else
			{
				ValidateElementNotDisabled(ProviderInvoiceNumberField);
			}
			return this;
		}

		public FinanceInvoiceRecordPage ValidateInvoiceReceivedDateFieldText(string ExpectedText)
		{
			WaitForElementVisible(InvoiceReceivedDateField);
			ValidateElementValue(InvoiceReceivedDateField, ExpectedText);

			return this;
		}
		public FinanceInvoiceRecordPage ValidateInvoiceReceivedDateFieldDisabled(bool ExpectedDisabled)
		{
			WaitForElementVisible(InvoiceReceivedDateField);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(InvoiceReceivedDateField);
			}
			else
			{
				ValidateElementNotDisabled(InvoiceReceivedDateField);
			}
			return this;
		}

		public FinanceInvoiceRecordPage ValidateNetAmountFieldText(string ExpectedText)
		{
			WaitForElementVisible(NetAmountField);
			ValidateElementValue(NetAmountField, ExpectedText);

			return this;
		}
		public FinanceInvoiceRecordPage ValidateNetAmountFieldDisabled(bool ExpectedDisabled)
		{
			WaitForElement(NetAmountField);
            ScrollToElement(NetAmountField);
            if (ExpectedDisabled)
			{
				ValidateElementDisabled(NetAmountField);
			}
			else
			{
				ValidateElementNotDisabled(NetAmountField);
			}
			return this;
		}

		public FinanceInvoiceRecordPage ValidateGrossAmountFieldText(string ExpectedText)
		{
			WaitForElementVisible(GrossAmountField);
			ValidateElementValue(GrossAmountField, ExpectedText);

			return this;
		}
		public FinanceInvoiceRecordPage ValidateGrossAmountFieldDisabled(bool ExpectedDisabled)
		{
			WaitForElementVisible(GrossAmountField);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(GrossAmountField);
			}
			else
			{
				ValidateElementNotDisabled(GrossAmountField);
			}
			return this;
		}

		public FinanceInvoiceRecordPage ValidateVatAmountFieldText(string ExpectedText)
		{
			WaitForElementVisible(VATAmountField);
			ValidateElementValue(VATAmountField, ExpectedText);

			return this;
		}
		public FinanceInvoiceRecordPage ValidateVatAmountFieldDisabled(bool ExpectedDisabled)
		{
            WaitForElement(VATAmountField);
            ScrollToElement(VATAmountField);
            if (ExpectedDisabled)
			{
				ValidateElementDisabled(VATAmountField);
			}
			else
			{
				ValidateElementNotDisabled(VATAmountField);
			}
			return this;
		}

		public FinanceInvoiceRecordPage ValidateValuesVerifiedYesOptionSelected(bool ExpectedSelected)
		{
			WaitForElement(ValuesVerifiedYesOption);
			WaitForElement(ValuesVerifiedNoOption);
			MoveToElementInPage(ValuesVerifiedYesOption);
			if (ExpectedSelected)
			{
				ValidateElementChecked(ValuesVerifiedYesOption);
				ValidateElementNotChecked(ValuesVerifiedNoOption);
			}
			else
			{
				ValidateElementNotChecked(ValuesVerifiedYesOption);
				ValidateElementChecked(ValuesVerifiedNoOption);
			}
			return this;
		}

		public FinanceInvoiceRecordPage ValidateValuesVerifiedOptionsDisabled(bool ExpectedDisabled)
		{
			WaitForElement(ValuesVerifiedYesOption);
			WaitForElement(ValuesVerifiedNoOption);
			MoveToElementInPage(ValuesVerifiedYesOption);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(ValuesVerifiedYesOption);
				ValidateElementDisabled(ValuesVerifiedNoOption);
			}
			else
			{
				ValidateElementNotDisabled(ValuesVerifiedYesOption);
				ValidateElementNotDisabled(ValuesVerifiedNoOption);
			}
			return this;
		}

		//Validate if PaymentsTab is visible or not
		public FinanceInvoiceRecordPage ValidatePaymentsTabVisible(bool ExpectedVisible)
		{
			if (ExpectedVisible)
			{
				WaitForElementVisible(PaymentsTab);
			}
			else
			{
				WaitForElementNotVisible(PaymentsTab, 2);
			}
			return this;
		}

		//Click PaymentsTab
		public FinanceInvoiceRecordPage ClickPaymentsTab()
		{
			WaitForElement(PaymentsTab);
			ScrollToElement(PaymentsTab);
			Click(PaymentsTab);

			return this;
		}

		//Navigate to Payment Sub Menu via Menu > Related Items menu
		public FinanceInvoiceRecordPage NavigateToPaymentSubMenu()
		{
			WaitForElement(MenuButton);
			ScrollToElement(MenuButton);
			Click(MenuButton);

            if (!CheckIfElementExists(RelatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(RelatedItemsSubMenuButton);
                ScrollToElement(RelatedItemsSubMenuButton);
                Click(RelatedItemsSubMenuButton);
            }

            WaitForElement(PaymentSubMenuItemButton);
			ScrollToElement(PaymentSubMenuItemButton);
			Click(PaymentSubMenuItemButton);

			return this;
		}

		//Navigate to Menu > Related Items menu, Validate Payment Sub Menu is visible or not
		public FinanceInvoiceRecordPage ValidatePaymentSubMenuVisible(bool ExpectedVisible)
		{
			WaitForElementToBeClickable(MenuButton);
			ScrollToElement(MenuButton);
			Click(MenuButton);

            if (!CheckIfElementExists(RelatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(RelatedItemsSubMenuButton);
				ScrollToElement(RelatedItemsSubMenuButton);
                Click(RelatedItemsSubMenuButton);
            }

            if (ExpectedVisible)
			{
				WaitForElementVisible(PaymentSubMenuItemButton);
			}
			else
			{
				WaitForElementNotVisible(PaymentSubMenuItemButton, 2);
			}
			return this;
		}

		//Method to Validate Payments Made Field Text
		public FinanceInvoiceRecordPage ValidatePaymentsMadeFieldText(string ExpectedText)
		{
			WaitForElementVisible(PaymentsMadeField);
			ValidateElementValue(PaymentsMadeField, ExpectedText);

			return this;
		}

        //Method to verify PaymentsMadeField disabled or not disabled
		public FinanceInvoiceRecordPage ValidatePaymentsMadeFieldDisabled(bool ExpectedDisabled)
		{
            WaitForElement(PaymentsMadeField);
            if (ExpectedDisabled)
                ValidateElementDisabled(PaymentsMadeField);
            else
                ValidateElementNotDisabled(PaymentsMadeField);
            return this;
        }

        //Method to Validate Debt Written Off Field Text
        public FinanceInvoiceRecordPage ValidateDebtWrittenOffFieldText(string ExpectedText)
		{
			WaitForElementVisible(DebtWrittenOffField);
			ValidateElementValue(DebtWrittenOffField, ExpectedText);

			return this;
		}

		//Debt Written Off Field is disabled or not disabled
		public FinanceInvoiceRecordPage ValidateDebtWrittenOffFieldDisabled(bool ExpectedDisabled)
		{
            WaitForElement(DebtWrittenOffField);
            if (ExpectedDisabled)
                ValidateElementDisabled(DebtWrittenOffField);
            else
                ValidateElementNotDisabled(DebtWrittenOffField);
            return this;
        }

		//Balance Outstanding field is disabled or not disabled
		public FinanceInvoiceRecordPage ValidateBalanceOutstandingFieldDisabled(bool ExpectedDisabled)
		{
            WaitForElement(BalanceOutstandingField);
            if (ExpectedDisabled)
                ValidateElementDisabled(BalanceOutstandingField);
            else
                ValidateElementNotDisabled(BalanceOutstandingField);
            return this;
        }


		//Method to Validate Balance Outstanding Field Text
		public FinanceInvoiceRecordPage ValidateBalanceOutstandingFieldText(string ExpectedText)
		{
			WaitForElementVisible(BalanceOutstandingField);
			ValidateElementValue(BalanceOutstandingField, ExpectedText);

			return this;
		}

		//Method to EstablishmentLookupButton is visible or not visible
		public FinanceInvoiceRecordPage ValidateEstablishmentLookupButtonVisible(bool ExpectedVisible)
		{
			if (ExpectedVisible)
			{
				WaitForElementVisible(EstablishmentLookupButton);
			}
			else
			{
				WaitForElementNotVisible(EstablishmentLookupButton, 2);
			}
			return this;
		}

        //Method to EstablishmentLookupButton is disabled or not disabled
		public FinanceInvoiceRecordPage ValidateEstablishmentLookupButtonDisabled(bool ExpectedDisabled)
		{
            if (ExpectedDisabled)
			{
                ValidateElementDisabled(EstablishmentLookupButton);
            }
            else
			{
                ValidateElementNotDisabled(EstablishmentLookupButton);
            }
            return this;
        }

        //Method to click EstablishmentLookupButton
		public FinanceInvoiceRecordPage ClickEstablishmentLookupButton()
		{
            WaitForElementToBeClickable(EstablishmentLookupButton);
            Click(EstablishmentLookupButton);

            return this;
        }

        //Validate EstablishmentLinkField Text
		public FinanceInvoiceRecordPage ValidateEstablishmentFieldText(string ExpectedText)
		{
            WaitForElement(EstablishmentLinkField);
            ValidateElementText(EstablishmentLinkField, ExpectedText);

            return this;
        }

        //Method to DirectDebitExtractBatchLookupButton is visible or not visible
        public FinanceInvoiceRecordPage ValidateDirectDebitExtractBatchLookupButtonVisible(bool ExpectedVisible)
		{
			if (ExpectedVisible)
			{
				WaitForElementVisible(DirectDebitExtractBatchLookupButton);
			}
			else
			{
				WaitForElementNotVisible(DirectDebitExtractBatchLookupButton, 2);

			}
			return this;
		}

		//Method to DirectDebitExtractBatchLookupButton is disabled or not disabled
		public FinanceInvoiceRecordPage ValidateDirectDebitExtractBatchLookupButtonDisabled(bool ExpectedDisabled)
		{
            if (ExpectedDisabled)
			{
                ValidateElementDisabled(DirectDebitExtractBatchLookupButton);
            }
            else
			{
                ValidateElementNotDisabled(DirectDebitExtractBatchLookupButton);
            }
            return this;
        }

		public FinanceInvoiceRecordPage ValidateDirectDebitExtractBatchFieldText(string ExpectedText)
		{
            WaitForElement(DirectDebitExtractBatchLinkField);
            ValidateElementText(DirectDebitExtractBatchLinkField, ExpectedText);

			return this;
        }

        //Method to verify Customer Account Code Field is visible or not visible
        public FinanceInvoiceRecordPage ValidateCustomerAccountCodeFieldVisible(bool ExpectedVisible)
		{
            if (ExpectedVisible)
			{
                WaitForElementVisible(CustomerAccountCode);
            }
            else
			{
                WaitForElementNotVisible(CustomerAccountCode, 2);
            }
            return this;
        }

		//Method to verify Customer Account Code Field is disabled or not disabled
		public FinanceInvoiceRecordPage ValidateCustomerAccountCodeFieldDisabled(bool ExpectedDisabled)
		{
            if (ExpectedDisabled)
			{
                ValidateElementDisabled(CustomerAccountCode);
            }
            else
			{
                ValidateElementNotDisabled(CustomerAccountCode);
            }
            return this;
        }

		//Method to verify Customer Account Code Field Text
		public FinanceInvoiceRecordPage ValidateCustomerAccountCodeFieldText(string ExpectedText)
		{
            WaitForElementVisible(CustomerAccountCode);
            ValidateElementValue(CustomerAccountCode, ExpectedText);

            return this;
        }

        //Method to verify that PersonLookupButton is visible or not visible
		public FinanceInvoiceRecordPage ValidatePersonLookupButtonVisible(bool ExpectedVisible)
		{
            if (ExpectedVisible)
			{
                WaitForElementVisible(PersonLookupButton);
            }
            else
			{
                WaitForElementNotVisible(PersonLookupButton, 2);
            }
            return this;
        }

        //Method to verify that AlternativeInvoiceNumberField is visible or not visible
		public FinanceInvoiceRecordPage ValidateAlternativeInvoiceNumberFieldVisible(bool ExpectedVisible)
		{
			if (ExpectedVisible)
            {
                WaitForElementVisible(AlternativeInvoiceNumberField);
            }
            else
            {
                WaitForElementNotVisible(AlternativeInvoiceNumberField, 2);
            }
            return this;
        }

		//Method to verify that AlternativeInvoiceNumberField is disabled or not disabled
		public FinanceInvoiceRecordPage ValidateAlternativeInvoiceNumberFieldDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
			{
                ValidateElementDisabled(AlternativeInvoiceNumberField);
            }
            else
			{
                ValidateElementNotDisabled(AlternativeInvoiceNumberField);
            }
            return this;
		}

		//Method to insert text on AlternativeInvoiceNumberField
		public FinanceInvoiceRecordPage InsertTextOnAlternativeInvoiceNumberField(string TextToInsert)
		{
			ScrollToElement(AlternativeInvoiceNumberField);
            WaitForElementToBeClickable(AlternativeInvoiceNumberField);
            SendKeys(AlternativeInvoiceNumberField, TextToInsert);
            SendKeysWithoutClearing(AlternativeInvoiceNumberField, Keys.Tab);

            return this;	
		}

        //Verify AlternativeInvoiceNumberField Text
		public FinanceInvoiceRecordPage ValidateAlternativeInvoiceNumberFieldText(string ExpectedText)
		{
			WaitForElementVisible(AlternativeInvoiceNumberField);
			ScrollToElement(AlternativeInvoiceNumberField);
            ValidateElementValue(AlternativeInvoiceNumberField, ExpectedText);

            return this;
		}

		//Method to verify that ChargesUpto_DateField is visible or not visible
		public FinanceInvoiceRecordPage ValidateChargesUpto_DateFieldVisible(bool ExpectedVisible)
		{
			if (ExpectedVisible)
			{
                WaitForElementVisible(ChargesUpto_DateField);
            }
            else
			{
                WaitForElementNotVisible(ChargesUpto_DateField, 2);
            }
            return this;
		}

		//Method to verify that ChargesUpto_DateField is disabled or not disabled
		public FinanceInvoiceRecordPage ValidateChargesUpto_DateFieldDisabled(bool ExpectedDisabled)
		{
			WaitForElement(ChargesUpto_DateField);
			ScrollToElement(ChargesUpto_DateField);
			if (ExpectedDisabled)
			{
                ValidateElementDisabled(ChargesUpto_DateField);
            }
            else
			{
                ValidateElementNotDisabled(ChargesUpto_DateField);
            }
            return this;
		}

		//Method to insert text on ChargesUpto_DateField
		public FinanceInvoiceRecordPage InsertTextOnChargesUpto_DateField(string TextToInsert)
		{
			ScrollToElement(ChargesUpto_DateField);
            WaitForElementToBeClickable(ChargesUpto_DateField);
            SendKeys(ChargesUpto_DateField, TextToInsert);
            SendKeysWithoutClearing(ChargesUpto_DateField, Keys.Tab);

            return this;
		}

		//Verify ChargesUpto_DateField Text
		public FinanceInvoiceRecordPage ValidateChargesUpto_DateFieldText(string ExpectedText)
		{
			WaitForElementVisible(ChargesUpto_DateField);
            ScrollToElement(ChargesUpto_DateField);
            ValidateElementValue(ChargesUpto_DateField, ExpectedText);

            return this;
		}

        //Verify NetAmountField is visible or not visible
		public FinanceInvoiceRecordPage ValidateNetAmountFieldVisible(bool ExpectedVisible)
		{
			if (ExpectedVisible)
			{
                WaitForElementVisible(NetAmountField);
            }
            else
			{
                WaitForElementNotVisible(NetAmountField, 2);
            }
            return this;
		}

		//Insert text on NetAmountField
		public FinanceInvoiceRecordPage InsertTextOnNetAmountField(string TextToInsert)
		{
			ScrollToElement(NetAmountField);
            WaitForElementToBeClickable(NetAmountField);
            SendKeys(NetAmountField, TextToInsert);
            SendKeysWithoutClearing(NetAmountField, Keys.Tab);

            return this;
		}

        //Verify that GrossAmountField is visible or not
		public FinanceInvoiceRecordPage ValidateGrossAmountFieldVisible(bool ExpectedVisible)
		{
			if (ExpectedVisible)
			{
                WaitForElementVisible(GrossAmountField);
            }
            else
			{
                WaitForElementNotVisible(GrossAmountField, 2);
            }
            return this;
		}

		//Insert text on GrossAmountField
		public FinanceInvoiceRecordPage InsertTextOnGrossAmountField(string TextToInsert)
		{
ScrollToElement(GrossAmountField);
            WaitForElementToBeClickable(GrossAmountField);
            SendKeys(GrossAmountField, TextToInsert);
            SendKeysWithoutClearing(GrossAmountField, Keys.Tab);

            return this;
        }

        //Verify VATAmountField is visible or not
		public FinanceInvoiceRecordPage ValidateVATAmountFieldVisible(bool ExpectedVisible)
		{
			if (ExpectedVisible)
			{
                WaitForElementVisible(VATAmountField);
            }
            else
			{
                WaitForElementNotVisible(VATAmountField, 2);
            }
            return this;
		}

		//Insert text on VATAmountField
		public FinanceInvoiceRecordPage InsertTextOnVATAmountField(string TextToInsert)
		{
			ScrollToElement(VATAmountField);
            WaitForElementToBeClickable(VATAmountField);
            SendKeys(VATAmountField, TextToInsert);
            SendKeysWithoutClearing(VATAmountField, Keys.Tab);

            return this;
		}

		//Verify IncludeInDebtOutstanding options are visible or not visible
		public FinanceInvoiceRecordPage ValidateIncludeInDebtOutstandingOptionsVisible(bool ExpectedVisible)
		{
			if (ExpectedVisible)
			{
                WaitForElementVisible(IncludeInDebtOutstanding_YesOption);
                WaitForElementVisible(IncludeInDebtOutstanding_NoOption);
            }
            else
			{
                WaitForElementNotVisible(IncludeInDebtOutstanding_YesOption, 2);
                WaitForElementNotVisible(IncludeInDebtOutstanding_NoOption, 2);
            }
            return this;
		}

		//Verify IncludeInDebtOutstanding options are disabled or not disabled
		public FinanceInvoiceRecordPage ValidateIncludeInDebtOutstandingOptionsDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
			{
                ValidateElementDisabled(IncludeInDebtOutstanding_YesOption);
                ValidateElementDisabled(IncludeInDebtOutstanding_NoOption);
            }
            else
			{
                ValidateElementNotDisabled(IncludeInDebtOutstanding_YesOption);
                ValidateElementNotDisabled(IncludeInDebtOutstanding_NoOption);
            }
            return this;
		}

        //verify CWField_includeindebtoutstanding_1 is checked or not checked, or if CWField_includeindebtoutstanding_0 is checked or not checked
		public FinanceInvoiceRecordPage ValidateIncludeInDebtOutstandingOptionsChecked(bool ExpectedSelected)
		{
			if (ExpectedSelected)
			{
                ValidateElementChecked(IncludeInDebtOutstanding_YesOption);
                ValidateElementNotChecked(IncludeInDebtOutstanding_NoOption);
            }
            else
			{
                ValidateElementNotChecked(IncludeInDebtOutstanding_YesOption);
                ValidateElementChecked(IncludeInDebtOutstanding_NoOption);
            }
            return this;
		}

		//Method to click IncludeInDebtOutstanding_YesOption
		public FinanceInvoiceRecordPage ClickIncludeInDebtOutstanding_YesOption()
		{
			WaitForElementToBeClickable(IncludeInDebtOutstanding_YesOption);
            ScrollToElement(IncludeInDebtOutstanding_YesOption);
            Click(IncludeInDebtOutstanding_YesOption);

            return this;
		}

		//Method to click IncludeInDebtOutstanding_NoOption
		public FinanceInvoiceRecordPage ClickIncludeInDebtOutstanding_NoOption()
		{
			WaitForElementToBeClickable(IncludeInDebtOutstanding_NoOption);
            ScrollToElement(IncludeInDebtOutstanding_NoOption);
            Click(IncludeInDebtOutstanding_NoOption);

            return this;
		}

        //click Payer (ProviderLookupButton) lookup button
		public FinanceInvoiceRecordPage ClickPayerLookupButton()
		{
			WaitForElementToBeClickable(ProviderOrPersonLookupButton);
			ScrollToElement(ProviderOrPersonLookupButton);
            Click(ProviderOrPersonLookupButton);

            return this;
        }

        //Method to Click PersonLookupButton 
		public FinanceInvoiceRecordPage ClickPersonLookupButton()
		{
			WaitForElementToBeClickable(PersonLookupButton);
            ScrollToElement(PersonLookupButton);
            Click(PersonLookupButton);

            return this;
        }   
		
		//Method to validate finance invoice status picklist text
		public FinanceInvoiceRecordPage ValidateInvoiceStatusPicklistText(string ExpectedText)
		{
			WaitForElementVisible(FinanceInvoiceStatusPicklist);
            ValidatePicklistSelectedText(FinanceInvoiceStatusPicklist, ExpectedText);

            return this;
		}

		//FinanceInvoiceStatusPicklist is disabled or not disabled
		public FinanceInvoiceRecordPage ValidateInvoiceStatusPicklistDisabled(bool ExpectedDisabled)
		{
			WaitForElementVisible(FinanceInvoiceStatusPicklist);
            if (ExpectedDisabled)
			{
                ValidateElementDisabled(FinanceInvoiceStatusPicklist);
            }
            else
			{
                ValidateElementNotDisabled(FinanceInvoiceStatusPicklist);
            }
            return this;
		}

        //Method to verify FinanceTransactionTab is visible or not visible
		public FinanceInvoiceRecordPage ValidateFinanceTransactionTabVisible(bool ExpectedVisible)
		{
			if (ExpectedVisible)
			{
                WaitForElementVisible(FinanceTransactionTab);
            }
            else
			{
                WaitForElementNotVisible(FinanceTransactionTab, 2);
            }
            return this;
		}

		//method to check if aria-expanded attribute of Toolbar menu button is false, if its false then click Toolbar menu button
		public FinanceInvoiceRecordPage ClickToolbarMenuButton()
		{
            WaitForElementToBeClickable(ToolBarMenuButton);
			ScrollToElement(ToolBarMenuButton);
            var IsMenuExpanded = GetElementByAttributeValue(ToolBarMenuButton, "aria-expanded");
            if (IsMenuExpanded.Equals("false"))
				Click(ToolBarMenuButton);
            return this;
		}

		//Method to click Delete button
		public FinanceInvoiceRecordPage ClickDeleteButton()
		{

			var IsDeleteOptionAvailable = GetElementVisibility(DeleteRecordButton);

			if(IsDeleteOptionAvailable)
			{
                WaitForElementToBeClickable(DeleteRecordButton);
                ScrollToElement(DeleteRecordButton);
                Click(DeleteRecordButton);
            }
            else
			{
                ClickToolbarMenuButton();
                WaitForElementToBeClickable(DeleteRecordButton);
                ScrollToElement(DeleteRecordButton);
                Click(DeleteRecordButton);
            }

            return this;
		}


		public FinanceInvoiceRecordPage ValidateDeleteButtonVisible(bool ExpectedVisible)
		{
			if (ExpectedVisible)
			{
                var IsDeleteOptionAvailable = GetElementVisibility(DeleteRecordButton);

				if (IsDeleteOptionAvailable)
				{
					ScrollToElement(DeleteRecordButton);
					WaitForElementVisible(DeleteRecordButton);
				}
				else
				{
					ClickToolbarMenuButton();
					WaitForElementVisible(DeleteRecordButton);
				}

                WaitForElementVisible(DeleteRecordButton);
            }
            else
			{
                WaitForElementNotVisible(DeleteRecordButton, 2);
				ClickToolbarMenuButton();
				WaitForElementNotVisible(DeleteRecordButton, 2);
            }
            return this;
		}

        //Check FinanceInvoiceBatchLookupButton visiblity
		public FinanceInvoiceRecordPage ValidateFinanceInvoiceBatchLookupButtonVisible(bool ExpectedVisible)
		{
			if (ExpectedVisible)
                WaitForElementVisible(FinanceInvoiceBatchLookupButton);
            else
                WaitForElementNotVisible(FinanceInvoiceBatchLookupButton, 2);
            return this;
		}

		//Check FinanceInvoiceBatchLookupButton is disabled or not
		public FinanceInvoiceRecordPage ValidateFinanceInvoiceBatchLookupButtonDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
                ValidateElementDisabled(FinanceInvoiceBatchLookupButton);
            else
                ValidateElementNotDisabled(FinanceInvoiceBatchLookupButton);
            return this;
		}

		//Click FinanceInvoiceBatchLookupButton
		public FinanceInvoiceRecordPage ClickFinanceInvoiceBatchLookupButton()
		{
			WaitForElement(FinanceInvoiceBatchLookupButton);
			ScrollToElement(FinanceInvoiceBatchLookupButton);
            Click(FinanceInvoiceBatchLookupButton);

            return this;
		}

		//Method to verify FinanceInvoiceBatchLinkField text
		public FinanceInvoiceRecordPage ValidateFinanceInvoiceBatchLinkFieldText(string ExpectedText)
		{
			WaitForElement(FinanceInvoiceBatchLinkField);
			ScrollToElement(FinanceInvoiceBatchLinkField);
            ValidateElementText(FinanceInvoiceBatchLinkField, ExpectedText);

            return this;
		}

        //Check ExtracLookupButton visiblity
		public FinanceInvoiceRecordPage ValidateExtractLookupButtonVisible(bool ExpectedVisible)
		{
			if (ExpectedVisible)
                WaitForElementVisible(ExtractLookupButton);
            else
                WaitForElementNotVisible(ExtractLookupButton, 2);
            return this;
		}

		//Check ExtractLookupButton is disabled or not
		public FinanceInvoiceRecordPage ValidateExtractLookupButtonDisabled(bool ExpectedDisabled)
		{
			   if (ExpectedDisabled)
                ValidateElementDisabled(ExtractLookupButton);
            else
                ValidateElementNotDisabled(ExtractLookupButton);
            return this;
		}

		//Click ExtractLookupButton
		public FinanceInvoiceRecordPage ClickExtractLookupButton()
		{
			WaitForElement(ExtractLookupButton);
			ScrollToElement(ExtractLookupButton);
            Click(ExtractLookupButton);

            return this;
		}

		//Method to verify ExtractLinkField text
		public FinanceInvoiceRecordPage ValidateExtractLinkFieldText(string ExpectedText)
		{
			WaitForElement(ExtractLinkField);
            ValidateElementText(ExtractLinkField, ExpectedText);

            return this;
		}

        //Check ContractSchemeLookupButton visiblity
		public FinanceInvoiceRecordPage ValidateContractSchemeLookupButtonVisible(bool ExpectedVisible)
		{
			if (ExpectedVisible)
                WaitForElementVisible(ContractSchemeLookupButton);
            else
                WaitForElementNotVisible(ContractSchemeLookupButton, 2);
            return this;
		}

		//Check ContractSchemeLookupButton is disabled or not
		public FinanceInvoiceRecordPage ValidateContractSchemeLookupButtonDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
                ValidateElementDisabled(ContractSchemeLookupButton);
            else
                ValidateElementNotDisabled(ContractSchemeLookupButton);
            return this;
		}

		//Click ContractSchemeLookupButton
		public FinanceInvoiceRecordPage ClickContractSchemeLookupButton()
		{
			WaitForElement(ContractSchemeLookupButton);
            ScrollToElement(ContractSchemeLookupButton);
            Click(ContractSchemeLookupButton);

            return this;
		}

		//Method to verify ContractSchemeLinkField text
		public FinanceInvoiceRecordPage ValidateContractSchemeLinkFieldText(string ExpectedText)
		{
			WaitForElement(ContractSchemeLinkField);
            ValidateElementText(ContractSchemeLinkField, ExpectedText);

            return this;
        }

        //Check BatchGroupingLookupButton visiblity
		public FinanceInvoiceRecordPage ValidateBatchGroupingLookupButtonVisible(bool ExpectedVisible)
		{
			if (ExpectedVisible)
                WaitForElementVisible(BatchGroupingLookupButton);
            else
                WaitForElementNotVisible(BatchGroupingLookupButton, 2);
            return this;
		}

		//Check BatchGroupingLookupButton is disabled or not
		public FinanceInvoiceRecordPage ValidateBatchGroupingLookupButtonDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
                ValidateElementDisabled(BatchGroupingLookupButton);
            else
                ValidateElementNotDisabled(BatchGroupingLookupButton);
            return this;
		}

		//Click BatchGroupingLookupButton
		public FinanceInvoiceRecordPage ClickBatchGroupingLookupButton()
		{
			WaitForElement(BatchGroupingLookupButton);
            ScrollToElement(BatchGroupingLookupButton);
            Click(BatchGroupingLookupButton);

            return this;
		}

		//Method to verify BatchGroupingLinkField text
		public FinanceInvoiceRecordPage ValidateBatchGroupingLinkFieldText(string ExpectedText)
		{
			WaitForElement(BatchGroupingLinkField);
            ValidateElementText(BatchGroupingLinkField, ExpectedText);

            return this;
        }

        //Check FunderLookupButton visiblity
		public FinanceInvoiceRecordPage ValidateFunderLookupButtonVisible(bool ExpectedVisible)
		{
			if (ExpectedVisible)
                WaitForElementVisible(FunderLookupButton);
            else
                WaitForElementNotVisible(FunderLookupButton, 2);
            return this;
		}

		//Check FunderLookupButton is disabled or not
		public FinanceInvoiceRecordPage ValidateFunderLookupButtonDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
                ValidateElementDisabled(FunderLookupButton);
            else
                ValidateElementNotDisabled(FunderLookupButton);
            return this;
		}

		//Click FunderLookupButton
		public FinanceInvoiceRecordPage ClickFunderLookupButton()
		{
			WaitForElement(FunderLookupButton);
            ScrollToElement(FunderLookupButton);
            Click(FunderLookupButton);

            return this;
		}

		//Method to verify FunderLinkField text
		public FinanceInvoiceRecordPage ValidateFunderLinkFieldText(string ExpectedText)
		{
			WaitForElement(FunderLinkField);
            ValidateElementText(FunderLinkField, ExpectedText);

            return this;
        }

        //Check DebtorReferenceNumberField visiblity
		public FinanceInvoiceRecordPage ValidateDebtorReferenceNumberFieldVisible(bool ExpectedVisible)
		{
			if (ExpectedVisible)
                WaitForElementVisible(DebtorReferenceNumberField);
            else
                WaitForElementNotVisible(DebtorReferenceNumberField, 2);
            return this;
		}

		//Check DebtorReferenceNumberField is disabled or not
		public FinanceInvoiceRecordPage ValidateDebtorReferenceNumberFieldDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
                ValidateElementDisabled(DebtorReferenceNumberField);
            else
                ValidateElementNotDisabled(DebtorReferenceNumberField);
            return this;
		}

		//Verify DebtorReferenceNumberField Text
		public FinanceInvoiceRecordPage ValidateDebtorReferenceNumberFieldText(string ExpectedText)
		{
			WaitForElement(DebtorReferenceNumberField);
			ScrollToElement(DebtorReferenceNumberField);
            ValidateElementValue(DebtorReferenceNumberField, ExpectedText);

            return this;
        }

        //Check InvoiceNumber visiblity
		public FinanceInvoiceRecordPage ValidateInvoiceNumberFieldVisible(bool ExpectedVisible)
		{
			if (ExpectedVisible)
                WaitForElementVisible(InvoiceNumberField);
            else
                WaitForElementNotVisible(InvoiceNumberField, 2);
            return this;
		}

        //Check InvoiceDateField is visible or not
		public FinanceInvoiceRecordPage ValidateInvoiceDateFieldVisible(bool ExpectedVisible)
		{
			if (ExpectedVisible)
                WaitForElementVisible(InvoiceDateField);
            else
                WaitForElementNotVisible(InvoiceDateField, 2);
            return this;
		}

        //Check InvoiceFile visiblity
		public FinanceInvoiceRecordPage ValidateInvoiceFileFieldVisible(bool ExpectedVisible)
		{
			if (ExpectedVisible)
                WaitForElementVisible(InvoiceFile);
            else
                WaitForElementNotVisible(InvoiceFile, 2);
            return this;
		}

		//Check InvoiceFile is disabled or not
		public FinanceInvoiceRecordPage ValidateInvoiceFileFieldDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
                ValidateElementDisabled(InvoiceFile);
            else
                ValidateElementNotDisabled(InvoiceFile);
            return this;
		}

		//Check InvoiceFile field text
		public FinanceInvoiceRecordPage ValidateInvoiceFileFieldText(string ExpectedText)
		{
			WaitForElement(InvoiceFile);
			ScrollToElement(InvoiceFile);
            ValidateElementValue(InvoiceFile, ExpectedText);

            return this;
		}

        //Check InvoiceDeliveryStatus visiblity
		public FinanceInvoiceRecordPage ValidateInvoiceDeliveryStatusFieldVisible(bool ExpectedVisible)
		{
			if (ExpectedVisible)
                WaitForElementVisible(InvoiceDeliveryStatus);
            else
                WaitForElementNotVisible(InvoiceDeliveryStatus, 2);
            return this;
		}

		//Check InvoiceDeliveryStatus is disabled or not
		public FinanceInvoiceRecordPage ValidateInvoiceDeliveryStatusFieldDisabled(bool ExpectedDisabled) 
		{
			if (ExpectedDisabled)
                ValidateElementDisabled(InvoiceDeliveryStatus);
            else
                ValidateElementNotDisabled(InvoiceDeliveryStatus);
            return this;
		}

		//Check InvoiceDeliveryStatus field picklist selected text
		public FinanceInvoiceRecordPage ValidateInvoiceDeliveryStatusFieldText(string ExpectedText)
		{
			WaitForElement(InvoiceDeliveryStatus);
            ScrollToElement(InvoiceDeliveryStatus);
            ValidatePicklistSelectedText(InvoiceDeliveryStatus, ExpectedText);

            return this;
        }

        //Check InvoiceTextDetailsField visiblity
        public FinanceInvoiceRecordPage ValidateInvoiceTextDetailsFieldVisible(bool ExpectedVisible)
		{
			if (ExpectedVisible)
                WaitForElementVisible(InvoiceTextDetailsField);
            else
                WaitForElementNotVisible(InvoiceTextDetailsField, 2);
            return this;
		}

		//Check InvoiceTextDetailsField is disabled or not
		public FinanceInvoiceRecordPage ValidateInvoiceTextDetailsFieldDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
                ValidateElementDisabled(InvoiceTextDetailsField);
            else
                ValidateElementNotDisabled(InvoiceTextDetailsField);
            return this;
		}

		//Check InvoiceTextDetailsField field text
		public FinanceInvoiceRecordPage ValidateInvoiceTextDetailsFieldText(string ExpectedText)
		{
			WaitForElement(InvoiceTextDetailsField);
            ScrollToElement(InvoiceTextDetailsField);
            ValidateElementValue(InvoiceTextDetailsField, ExpectedText);

            return this;
        }

        //Check InvoiceTextDetailsField is visible or not

        //Check NoteText visiblity
		public FinanceInvoiceRecordPage ValidateNoteTextFieldVisible(bool ExpectedVisible)
		{
			if (ExpectedVisible)
                WaitForElementVisible(NoteText);
            else
                WaitForElementNotVisible(NoteText, 2);
            return this;
		}

		//Check NoteText is disabled or not
		public FinanceInvoiceRecordPage ValidateNoteTextFieldDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
                ValidateElementDisabled(NoteText);
            else
                ValidateElementNotDisabled(NoteText);
            return this;
		}

		//Check NoteText field text
		public FinanceInvoiceRecordPage ValidateNoteTextFieldText(string ExpectedText)
		{
			WaitForElement(NoteText);
            ScrollToElement(NoteText);
            ValidateElementValue(NoteText, ExpectedText);

            return this;
        }

        //Insert text on NoteText field
		public FinanceInvoiceRecordPage InsertTextOnNoteTextField(string TextToInsert)
		{
			ScrollToElement(NoteText);
            WaitForElementToBeClickable(NoteText);
            SendKeys(NoteText, TextToInsert);
            SendKeysWithoutClearing(NoteText, Keys.Tab);

            return this;
        }

		//Check Responsible Team visiblity
		public FinanceInvoiceRecordPage ValidateResponsibleTeamLookupButtonVisible(bool ExpectedVisible)
		{
			if (ExpectedVisible)
                WaitForElementVisible(ResponsibleTeamLookupButton);
            else
                WaitForElementNotVisible(ResponsibleTeamLookupButton, 2);
            return this;
		}

		//Check ResponsibleTeamLookupButton is disabled or not
		public FinanceInvoiceRecordPage ValidateResponsibleTeamLookupButtonDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
                ValidateElementDisabled(ResponsibleTeamLookupButton);
            else
                ValidateElementNotDisabled(ResponsibleTeamLookupButton);
            return this;
		}

		//Verify Responsible Team Link Field Text
		public FinanceInvoiceRecordPage ValidateResponsibleTeamLinkFieldText(string ExpectedText)
		{
			WaitForElement(ResponsibleTeamLink);
            ValidateElementByTitle(ResponsibleTeamLink, ExpectedText);

            return this;
        }
        


    }
}

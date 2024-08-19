using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class FinanceInvoiceBatchSetupRecordPage : CommonMethods
    {
        public FinanceInvoiceBatchSetupRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By ContentIFrame = By.Id("CWContentIFrame");
        readonly By CWDataFormDialog_IFrame = By.Id("iframe_CWDataFormDialog");
        readonly By FinanceinvoicebatchsetupFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=financeinvoicebatchsetup')]");

        #region option toolbar

        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By DeleteButton = By.Id("TI_DeleteRecordButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By AdditionalToolbarButton = By.XPath("//div[@id='CWToolbarMenu']/button");
        readonly By BackButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");

        #endregion

        readonly By MenuButton = By.Id("CWNavGroup_Menu");
        readonly By FinanceInvoiceBatchesMenuSubItem = By.Id("CWNavItem_FinanceInvoiceBatchs");

        readonly By NotificationMessage = By.Id("CWNotificationMessage_DataForm");

        readonly By FinanceModule_FieldLabel = By.XPath("//*[@id='CWLabelHolder_financemoduleid']/label");
        readonly By FinanceModule_Field = By.Id("CWField_financemoduleid");
        readonly By FinanceModule_Field_NottificationMessage = By.XPath("//*[@id='CWControlHolder_financemoduleid']/label/span");

        readonly By PermitAuthorisationOfCreditInvoices_FieldLabel = By.XPath("//*[@id='CWLabelHolder_authoriseinvoiceslessthanzero']/label");
        readonly By PermitAuthorisationOfCreditInvoices_YesRadioButton = By.Id("CWField_authoriseinvoiceslessthanzero_1");
        readonly By PermitAuthorisationOfCreditInvoices_NoRadioButton = By.Id("CWField_authoriseinvoiceslessthanzero_0");

        readonly By ApplyFinanceTransactionsToEarliestFinanceInvoice_FieldLabel = By.XPath("//*[@id='CWLabelHolder_applyftearliestfinanceinvoice']/label");
        readonly By ApplyFinanceTransactionsToEarliestFinanceInvoice_YesRadioButton = By.Id("CWField_applyftearliestfinanceinvoice_1");
        readonly By ApplyFinanceTransactionsToEarliestFinanceInvoice_NoRadioButton = By.Id("CWField_applyftearliestfinanceinvoice_0");

        readonly By ApplySuspensions_FieldLabel = By.XPath("//*[@id='CWLabelHolder_applysuspensions']/label");
        readonly By ApplySuspensions_YesRadioButton = By.Id("CWField_applysuspensions_1");
        readonly By ApplySuspensions_NoRadioButton = By.Id("CWField_applysuspensions_0");

        readonly By CreateFinanceInvoiceBatch_FieldLabel = By.XPath("//*[@id='CWLabelHolder_createfinanceinvoicebatch']/label[text()='Create Finance Invoice Batch?']");
        readonly By CreateFinanceInvoiceBatch_YesRadioButton = By.Id("CWField_createfinanceinvoicebatch_1");
        readonly By CreateFinanceInvoiceBatch_NoRadioButton = By.Id("CWField_createfinanceinvoicebatch_0");

        readonly By CreateFinanceTransactions_FieldLabel = By.XPath("//*[@id='CWLabelHolder_createfinancetransactions']/label[text()='Create Finance Transactions?']");
        readonly By CreateFinanceTransactions_YesRadioButton = By.Id("CWField_createfinancetransactions_1");
        readonly By CreateFinanceTransactions_NoRadioButton = By.Id("CWField_createfinancetransactions_0");

        readonly By CreditorReferenceNumberRequired_FieldLabel = By.XPath("//*[@id='CWLabelHolder_creditorreferencenumberrequired']/label");
        readonly By CreditorReferenceNumberRequired_YesRadioButton = By.Id("CWField_creditorreferencenumberrequired_1");
        readonly By CreditorReferenceNumberRequired_NoRadioButton = By.Id("CWField_creditorreferencenumberrequired_0");

        readonly By SeparateInvoices_FieldLabel = By.XPath("//*[@id='CWLabelHolder_inseparateinvoices']/label");
        readonly By SeparateInvoices_YesRadioButton = By.Id("CWField_inseparateinvoices_1");
        readonly By SeparateInvoices_NoRadioButton = By.Id("CWField_inseparateinvoices_0");


        readonly By StartDate_FieldLabel = By.XPath("//*[@id='CWLabelHolder_startdate']/label");
        readonly By StartDate_Field = By.Id("CWField_startdate");
        readonly By StartDate_DatePicker = By.Id("CWField_startdate_DatePicker");
        readonly By StartDate_Field_NottificationMessage = By.XPath("//*[@id='CWControlHolder_startdate']/label/span");

        readonly By StartTime_FieldLabel = By.XPath("//*[@id='CWLabelHolder_starttime']/label");
        readonly By StartTime_Field = By.Id("CWField_starttime");
        readonly By StartTime_TimePicker = By.Id("CWField_starttime_TimePicker");

        readonly By CreateBatchWithin_FieldLabel = By.XPath("//*[@id='CWLabelHolder_createbatchwithin']/label");
        readonly By CreateBatchWithin_Field = By.Id("CWField_createbatchwithin");
        readonly By CreateBatchWithin_Field_NottificationMessage = By.XPath("//*[@id='CWControlHolder_createbatchwithin']/label/span");

        readonly By PayToDay_FieldLabel = By.XPath("//*[@id='CWLabelHolder_paytodayid']/label");
        readonly By PayToDay_Field = By.Id("CWField_paytodayid");
        readonly By PayToDay_Field_NottificationMessage = By.XPath("//*[@id='CWControlHolder_paytodayid']/label/span");


        readonly By DebtorsVATCode_FieldLabel = By.XPath("//*[@id='CWLabelHolder_debtorsvatcodeid']/label");
        readonly By DebtorsVATCode_Mandatory = By.XPath("//*[@id='CWLabelHolder_debtorsvatcodeid']/label/span[@class = 'mandatory']");
        readonly By DebtorsVATCode_InputField = By.Id("CWField_debtorsvatcodeid_cwname");
        readonly By DebtorsVATCode_LookupButtonField = By.Id("CWLookupBtn_debtorsvatcodeid");
        readonly By DebtorsVATCode_RemoveButtonField = By.Id("CWClearLookup_debtorsvatcodeid");
        readonly By DebtorsVATCode_LinkField = By.Id("CWField_debtorsvatcodeid_Link");

        readonly By ServiceElement1_FieldLabel = By.XPath("//*[@id='CWLabelHolder_serviceelement1id']/label");
        readonly By ServiceElement1_LookupButtonField = By.Id("CWLookupBtn_serviceelement1id");
        readonly By ServiceElement1_RemoveButtonField = By.Id("CWClearLookup_serviceelement1id");
        readonly By ServiceElement1_LinkField = By.Id("CWField_serviceelement1id_Link");
        readonly By ServiceElement1_Field_NottificationMessage = By.XPath("//*[@id='CWControlHolder_serviceelement1id']/label/span");

        readonly By PaymentType_FieldLabel = By.XPath("//*[@id='CWLabelHolder_paymenttypeid']/label");
        readonly By PaymentType_LookupButtonField = By.Id("CWLookupBtn_paymenttypeid");
        readonly By PaymentType_RemoveButtonField = By.Id("CWClearLookup_paymenttypeid");
        readonly By PaymentType_LinkField = By.Id("CWField_paymenttypeid_Link");
        readonly By PaymentType_Field_NottificationMessage = By.XPath("//*[@id='CWControlHolder_paymenttypeid']/label/span");

        readonly By ProviderBatchGrouping_FieldLabel = By.XPath("//*[@id='CWLabelHolder_providerbatchgroupingid']/label");
        readonly By ProviderBatchGrouping_LookupButtonField = By.Id("CWLookupBtn_providerbatchgroupingid");
        readonly By ProviderBatchGrouping_RemoveButtonField = By.Id("CWClearLookup_providerbatchgroupingid");
        readonly By ProviderBatchGrouping_LinkField = By.Id("CWField_providerbatchgroupingid_Link");
        readonly By ProviderBatchGrouping_Field_NottificationMessage = By.XPath("//*[@id='CWControlHolder_providerbatchgroupingid']/label/span");

        readonly By InvoiceBy_FieldLabel = By.XPath("//*[@id='CWLabelHolder_invoicebyid']/label");
        readonly By InvoiceBy_LookupButtonField = By.Id("CWLookupBtn_invoicebyid");
        readonly By InvoiceBy_RemoveButtonField = By.Id("CWClearLookup_invoicebyid");
        readonly By InvoiceBy_LinkField = By.Id("CWField_invoicebyid_Link");
        readonly By InvoiceBy_Field_NottificationMessage = By.XPath("//*[@id='CWControlHolder_invoicebyid']/label/span");

        readonly By InvoiceFrequency_FieldLabel = By.XPath("//*[@id='CWLabelHolder_invoicefrequencyid']/label");
        readonly By InvoiceFrequency_LookupButtonField = By.Id("CWLookupBtn_invoicefrequencyid");
        readonly By InvoiceFrequency_RemoveButtonField = By.Id("CWClearLookup_invoicefrequencyid");
        readonly By InvoiceFrequency_LinkField = By.Id("CWField_invoicefrequencyid_Link");
        readonly By InvoiceFrequency_Field_NottificationMessage = By.XPath("//*[@id='CWControlHolder_invoicefrequencyid']/label/span");

        readonly By ExtractName_FieldLabel = By.XPath("//*[@id='CWLabelHolder_extractnameid']/label");
        readonly By ExtractName_LookupButtonField = By.Id("CWLookupBtn_extractnameid");
        readonly By ExtractName_RemoveButtonField = By.Id("CWClearLookup_extractnameid");
        readonly By ExtractName_LinkField = By.Id("CWField_extractnameid_Link");
        readonly By ExtractName_Field_NottificationMessage = By.XPath("//*[@id='CWControlHolder_extractnameid']/label/span");

        readonly By ResponsibleTeam_FieldLabel = By.XPath("//*[@id='CWLabelHolder_ownerid']/label");
        readonly By ResponsibleTeam_LookupButtonField = By.Id("CWLookupBtn_ownerid");
        readonly By ResponsibleTeam_RemoveButtonField = By.Id("CWClearLookup_ownerid");
        readonly By ResponsibleTeam_LinkField = By.Id("CWField_ownerid_Link");

        readonly By DebtorReferenceNumberRequired_FieldLabel = By.XPath("//*[@id='CWLabelHolder_debtorreferencenumberrequired']/label");
        readonly By DebtorReferenceNumberRequired_YesRadioButton = By.Id("CWField_debtorreferencenumberrequired_1");
        readonly By DebtorReferenceNumberRequired_NoRadioButton = By.Id("CWField_debtorreferencenumberrequired_0");

        #region Carer Payments

        readonly By CarerBatchGrouping_FieldLabel = By.XPath("//*[@id='CWLabelHolder_carerbatchgroupingid']/label");
        readonly By CarerBatchGrouping_LookupButtonField = By.Id("CWLookupBtn_carerbatchgroupingid");
        readonly By CarerBatchGrouping_RemoveButtonField = By.Id("CWClearLookup_carerbatchgroupingid");
        readonly By CarerBatchGrouping_LinkField = By.Id("CWField_carerbatchgroupingid_Link");

        #endregion

        #region Debtor Invoice Text

        readonly By RuleType_FieldLabel = By.XPath("//*[@id='CWLabelHolder_ruletypeid']/label");
        readonly By RuleType_LookupButtonField = By.Id("CWLookupBtn_ruletypeid");
        readonly By RuleType_RemoveButtonField = By.Id("CWClearLookup_ruletypeid");
        readonly By RuleType_LinkField = By.Id("CWField_ruletypeid_Link");

        readonly By ContributionType_FieldLabel = By.XPath("//*[@id='CWLabelHolder_contributiontypeid']/label");
        readonly By ContributionType_LookupButtonField = By.Id("CWLookupBtn_contributiontypeid");
        readonly By ContributionType_RemoveButtonField = By.Id("CWClearLookup_contributiontypeid");
        readonly By ContributionType_LinkField = By.Id("CWField_contributiontypeid_Link");

        readonly By RecoveryMethod_FieldLabel = By.XPath("//*[@id='CWLabelHolder_recoverymethodid']/label");
        readonly By RecoveryMethod_LookupButtonField = By.Id("CWLookupBtn_recoverymethodid");
        readonly By RecoveryMethod_RemoveButtonField = By.Id("CWClearLookup_recoverymethodid");
        readonly By RecoveryMethod_LinkField = By.Id("CWField_recoverymethodid_Link");

        readonly By DebtorBatchGrouping_FieldLabel = By.XPath("//*[@id='CWLabelHolder_debtorbatchgroupingid']/label");
        readonly By DebtorBatchGrouping_LookupButtonField = By.Id("CWLookupBtn_debtorbatchgroupingid");
        readonly By DebtorBatchGrouping_RemoveButtonField = By.Id("CWClearLookup_debtorbatchgroupingid");
        readonly By DebtorBatchGrouping_LinkField = By.Id("CWField_debtorbatchgroupingid_Link");

        readonly By DebtorHeaderText_FieldLabel = By.XPath("//*[@id='CWLabelHolder_debtorheadertextid']/label");
        readonly By DebtorHeaderText_LookupButtonField = By.Id("CWLookupBtn_debtorheadertextid");
        readonly By DebtorHeaderText_RemoveButtonField = By.Id("CWClearLookup_debtorheadertextid");
        readonly By DebtorHeaderText_LinkField = By.Id("CWField_debtorheadertextid_Link");

        readonly By DebtorTransactionText_FieldLabel = By.XPath("//*[@id='CWLabelHolder_debtortransactiontextid']/label");
        readonly By DebtorTransactionText_LookupButtonField = By.Id("CWLookupBtn_debtortransactiontextid");
        readonly By DebtorTransactionText_RemoveButtonField = By.Id("CWClearLookup_debtortransactiontextid");
        readonly By DebtorTransactionText_LinkField = By.Id("CWField_debtortransactiontextid_Link");

        readonly By DebtorRecoveryText_FieldLabel = By.XPath("//*[@id='CWLabelHolder_debtorrecoverytextid']/label");
        readonly By DebtorRecoveryText_LookupButtonField = By.Id("CWLookupBtn_debtorrecoverytextid");
        readonly By DebtorRecoveryText_RemoveButtonField = By.Id("CWClearLookup_debtorrecoverytextid");
        readonly By DebtorRecoveryText_LinkField = By.Id("CWField_debtorrecoverytextid_Link");

        #endregion

        readonly By FinanceinvoicebatchsetupPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        public FinanceInvoiceBatchSetupRecordPage WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(ContentIFrame);
            SwitchToIframe(ContentIFrame);

            WaitForElement(FinanceinvoicebatchsetupFrame);
            SwitchToIframe(FinanceinvoicebatchsetupFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(FinanceinvoicebatchsetupPageHeader);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage WaitForFinanceInvoiceBatchSetupRecordPageToLoadFromAdvancedSearch()
        {
            SwitchToDefaultFrame();

            WaitForElement(ContentIFrame);
            SwitchToIframe(ContentIFrame);

            WaitForElement(CWDataFormDialog_IFrame);
            SwitchToIframe(CWDataFormDialog_IFrame);

            WaitForElement(FinanceinvoicebatchsetupFrame);
            SwitchToIframe(FinanceinvoicebatchsetupFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(FinanceinvoicebatchsetupPageHeader);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage NavigateToFinanceInvoicesBatchesPage()
        {
            WaitForElementToBeClickable(MenuButton);
            MoveToElementInPage(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(FinanceInvoiceBatchesMenuSubItem);
            MoveToElementInPage(FinanceInvoiceBatchesMenuSubItem);
            Click(FinanceInvoiceBatchesMenuSubItem);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage SelectFinanceModule(string TextToSelect)
        {
            WaitForElementToBeClickable(FinanceModule_Field);
            MoveToElementInPage(FinanceModule_Field);
            SelectPicklistElementByText(FinanceModule_Field, TextToSelect);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateFinanceModuleIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(FinanceModule_Field);
            MoveToElementInPage(FinanceModule_Field);
            if (IsDisabled)
                ValidateElementDisabled(FinanceModule_Field);
            else
                ValidateElementNotDisabled(FinanceModule_Field);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage SelectPayToDayValue(string ValueToSelect)
        {
            WaitForElementToBeClickable(PayToDay_Field);
            MoveToElementInPage(PayToDay_Field);
            SelectPicklistElementByValue(PayToDay_Field, ValueToSelect);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidatePayToDayIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(PayToDay_Field);
            MoveToElementInPage(PayToDay_Field);
            if (IsDisabled)
                ValidateElementDisabled(PayToDay_Field);
            else
                ValidateElementNotDisabled(PayToDay_Field);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateDebtorsVATCodeFieldOptionsVisibility(bool visibility = true)
        {
            if (visibility)
            {
                WaitForElementVisible(DebtorsVATCode_FieldLabel);
                WaitForElementVisible(DebtorsVATCode_LookupButtonField);
                WaitForElementVisible(DebtorsVATCode_InputField);
                MoveToElementInPage(DebtorsVATCode_InputField);
            }
            else
            {
                WaitForElementNotVisible(DebtorsVATCode_FieldLabel, 5);
                WaitForElementNotVisible(DebtorsVATCode_LookupButtonField, 5);
                WaitForElementNotVisible(DebtorsVATCode_InputField, 5);
            }

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateDebtorsVATCodeLinkFieldTextVisibility(bool visibility)
        {
            if (visibility)
                WaitForElementVisible(DebtorsVATCode_LinkField);
            else
                WaitForElementNotVisible(DebtorsVATCode_LinkField, 5);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateDebtorsVATCodeFieldIsNotMandatory()
        {
            WaitForElementNotVisible(DebtorsVATCode_Mandatory, 5);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickDebtorsVATCodeLookupButton()
        {
            WaitForElementToBeClickable(DebtorsVATCode_LookupButtonField);
            MoveToElementInPage(DebtorsVATCode_LookupButtonField);
            Click(DebtorsVATCode_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateDebtorsVATCodeLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(DebtorsVATCode_LookupButtonField);
            MoveToElementInPage(DebtorsVATCode_LookupButtonField);
            if (IsDisabled)
                ValidateElementDisabled(DebtorsVATCode_LookupButtonField);
            else
                ValidateElementNotDisabled(DebtorsVATCode_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickServiceElement1LookupButton()
        {
            WaitForElementToBeClickable(ServiceElement1_LookupButtonField);
            MoveToElementInPage(ServiceElement1_LookupButtonField);
            Click(ServiceElement1_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateServiceElement1LookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(ServiceElement1_LookupButtonField);
            MoveToElementInPage(ServiceElement1_LookupButtonField);
            if (IsDisabled)
                ValidateElementDisabled(ServiceElement1_LookupButtonField);
            else
                ValidateElementNotDisabled(ServiceElement1_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickPaymentTypeLookupButton()
        {
            WaitForElementToBeClickable(PaymentType_LookupButtonField);
            MoveToElementInPage(PaymentType_LookupButtonField);
            Click(PaymentType_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidatePaymentTypeLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(PaymentType_LookupButtonField);
            MoveToElementInPage(PaymentType_LookupButtonField);
            if (IsDisabled)
                ValidateElementDisabled(PaymentType_LookupButtonField);
            else
                ValidateElementNotDisabled(PaymentType_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickProviderBatchGroupingLookupButton()
        {
            WaitForElementToBeClickable(ProviderBatchGrouping_LookupButtonField);
            MoveToElementInPage(ProviderBatchGrouping_LookupButtonField);
            Click(ProviderBatchGrouping_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateProviderBatchGroupingLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(ProviderBatchGrouping_LookupButtonField);
            MoveToElementInPage(ProviderBatchGrouping_LookupButtonField);
            if (IsDisabled)
                ValidateElementDisabled(ProviderBatchGrouping_LookupButtonField);
            else
                ValidateElementNotDisabled(ProviderBatchGrouping_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickCarerBatchGroupingLookupButton()
        {
            WaitForElementToBeClickable(CarerBatchGrouping_LookupButtonField);
            MoveToElementInPage(CarerBatchGrouping_LookupButtonField);
            Click(CarerBatchGrouping_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateCarerBatchGroupingLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(CarerBatchGrouping_LookupButtonField);
            MoveToElementInPage(CarerBatchGrouping_LookupButtonField);
            if (IsDisabled)
                ValidateElementDisabled(CarerBatchGrouping_LookupButtonField);
            else
                ValidateElementNotDisabled(CarerBatchGrouping_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickDebtorBatchGroupingLookupButton()
        {
            WaitForElementToBeClickable(DebtorBatchGrouping_LookupButtonField);
            MoveToElementInPage(DebtorBatchGrouping_LookupButtonField);
            Click(DebtorBatchGrouping_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateDebtorBatchGroupingLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(DebtorBatchGrouping_LookupButtonField);
            MoveToElementInPage(DebtorBatchGrouping_LookupButtonField);
            if (IsDisabled)
                ValidateElementDisabled(DebtorBatchGrouping_LookupButtonField);
            else
                ValidateElementNotDisabled(DebtorBatchGrouping_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickInvoiceByLookupButton()
        {
            WaitForElementToBeClickable(InvoiceBy_LookupButtonField);
            MoveToElementInPage(InvoiceBy_LookupButtonField);
            Click(InvoiceBy_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateInvoiceByLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(InvoiceBy_LookupButtonField);
            MoveToElementInPage(InvoiceBy_LookupButtonField);
            if (IsDisabled)
                ValidateElementDisabled(InvoiceBy_LookupButtonField);
            else
                ValidateElementNotDisabled(InvoiceBy_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickInvoiceFrequencyLookupButton()
        {
            WaitForElementToBeClickable(InvoiceFrequency_LookupButtonField);
            MoveToElementInPage(InvoiceFrequency_LookupButtonField);
            Click(InvoiceFrequency_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateInvoiceFrequencyLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(InvoiceFrequency_LookupButtonField);
            MoveToElementInPage(InvoiceFrequency_LookupButtonField);
            if (IsDisabled)
                ValidateElementDisabled(InvoiceFrequency_LookupButtonField);
            else
                ValidateElementNotDisabled(InvoiceFrequency_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickExtractNameLookupButton()
        {
            WaitForElementToBeClickable(ExtractName_LookupButtonField);
            MoveToElementInPage(ExtractName_LookupButtonField);
            Click(ExtractName_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateExtractNameLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(ExtractName_LookupButtonField);
            MoveToElementInPage(ExtractName_LookupButtonField);
            if (IsDisabled)
                ValidateElementDisabled(ExtractName_LookupButtonField);
            else
                ValidateElementNotDisabled(ExtractName_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_LookupButtonField);
            MoveToElementInPage(ResponsibleTeam_LookupButtonField);
            Click(ResponsibleTeam_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateResponsibleTeamLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(ResponsibleTeam_LookupButtonField);
            MoveToElementInPage(ResponsibleTeam_LookupButtonField);
            if (IsDisabled)
                ValidateElementDisabled(ResponsibleTeam_LookupButtonField);
            else
                ValidateElementNotDisabled(ResponsibleTeam_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickDebtorHeaderTextLookupButton()
        {
            WaitForElementToBeClickable(DebtorHeaderText_LookupButtonField);
            MoveToElementInPage(DebtorHeaderText_LookupButtonField);
            Click(DebtorHeaderText_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateDebtorHeaderTextLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(DebtorHeaderText_LookupButtonField);
            MoveToElementInPage(DebtorHeaderText_LookupButtonField);
            if (IsDisabled)
                ValidateElementDisabled(DebtorHeaderText_LookupButtonField);
            else
                ValidateElementNotDisabled(DebtorHeaderText_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickDebtorTransactionTextLookupButton()
        {
            WaitForElementToBeClickable(DebtorTransactionText_LookupButtonField);
            MoveToElementInPage(DebtorTransactionText_LookupButtonField);
            Click(DebtorTransactionText_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateDebtorTransactionTextLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(DebtorTransactionText_LookupButtonField);
            MoveToElementInPage(DebtorTransactionText_LookupButtonField);
            if (IsDisabled)
                ValidateElementDisabled(DebtorTransactionText_LookupButtonField);
            else
                ValidateElementNotDisabled(DebtorTransactionText_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickDebtorRecoveryTextLookupButton()
        {
            WaitForElementToBeClickable(DebtorRecoveryText_LookupButtonField);
            MoveToElementInPage(DebtorRecoveryText_LookupButtonField);
            Click(DebtorRecoveryText_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateDebtorRecoveryTextLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(DebtorRecoveryText_LookupButtonField);
            MoveToElementInPage(DebtorRecoveryText_LookupButtonField);
            if (IsDisabled)
                ValidateElementDisabled(DebtorRecoveryText_LookupButtonField);
            else
                ValidateElementNotDisabled(DebtorRecoveryText_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage InsertStartDate(string TextToInsert)
        {
            WaitForElementToBeClickable(StartDate_Field);
            SendKeys(StartDate_Field, TextToInsert);
            SendKeysWithoutClearing(StartDate_Field, Keys.Tab);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateStartDateFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(StartDate_Field);
            MoveToElementInPage(StartDate_Field);
            if (IsDisabled)
                ValidateElementDisabled(StartDate_Field);
            else
                ValidateElementNotDisabled(StartDate_Field);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage InsertStartTime(string TextToInsert)
        {
            WaitForElementToBeClickable(StartTime_Field);
            SendKeys(StartTime_Field, TextToInsert);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateStartTimeFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(StartTime_Field);
            MoveToElementInPage(StartTime_Field);
            if (IsDisabled)
                ValidateElementDisabled(StartTime_Field);
            else
                ValidateElementNotDisabled(StartTime_Field);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage InsertCreateBatchWithin(string TextToInsert)
        {
            WaitForElementToBeClickable(CreateBatchWithin_Field);
            SendKeys(CreateBatchWithin_Field, TextToInsert);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateCreateBatchWithinFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(CreateBatchWithin_Field);
            MoveToElementInPage(CreateBatchWithin_Field);
            if (IsDisabled)
                ValidateElementDisabled(CreateBatchWithin_Field);
            else
                ValidateElementNotDisabled(CreateBatchWithin_Field);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickPermitAuthorisationOfCreditInvoicesYesOption()
        {
            MoveToElementInPage(PermitAuthorisationOfCreditInvoices_YesRadioButton);
            WaitForElementToBeClickable(PermitAuthorisationOfCreditInvoices_YesRadioButton);
            Click(PermitAuthorisationOfCreditInvoices_YesRadioButton);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickPermitAuthorisationOfCreditInvoicesNoOption()
        {
            MoveToElementInPage(PermitAuthorisationOfCreditInvoices_NoRadioButton);
            WaitForElementToBeClickable(PermitAuthorisationOfCreditInvoices_NoRadioButton);
            Click(PermitAuthorisationOfCreditInvoices_NoRadioButton);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidatePermitAuthorisationOfCreditInvoicesFieldOptionsDisplayed()
        {
            WaitForElementVisible(PermitAuthorisationOfCreditInvoices_YesRadioButton);
            WaitForElementVisible(PermitAuthorisationOfCreditInvoices_NoRadioButton);
            MoveToElementInPage(PermitAuthorisationOfCreditInvoices_YesRadioButton);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidatePermitAuthorisationOfCreditInvoicesYesOptionChecked(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(PermitAuthorisationOfCreditInvoices_YesRadioButton);
                ValidateElementNotChecked(PermitAuthorisationOfCreditInvoices_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(PermitAuthorisationOfCreditInvoices_YesRadioButton);
                ValidateElementChecked(PermitAuthorisationOfCreditInvoices_NoRadioButton);
            }

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidatePermitAuthorisationOfCreditInvoicesOptionsIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(PermitAuthorisationOfCreditInvoices_YesRadioButton);
            MoveToElementInPage(PermitAuthorisationOfCreditInvoices_YesRadioButton);
            if (IsDisabled)
            {
                ValidateElementDisabled(PermitAuthorisationOfCreditInvoices_YesRadioButton);
                ValidateElementDisabled(PermitAuthorisationOfCreditInvoices_NoRadioButton);
            }
            else
            {
                ValidateElementNotDisabled(PermitAuthorisationOfCreditInvoices_YesRadioButton);
                ValidateElementNotDisabled(PermitAuthorisationOfCreditInvoices_NoRadioButton);
            }

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickApplyFinanceTransactionsToEarliestFinanceInvoiceYesOption()
        {
            MoveToElementInPage(ApplyFinanceTransactionsToEarliestFinanceInvoice_YesRadioButton);
            WaitForElementToBeClickable(ApplyFinanceTransactionsToEarliestFinanceInvoice_YesRadioButton);
            Click(ApplyFinanceTransactionsToEarliestFinanceInvoice_YesRadioButton);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickApplyFinanceTransactionsToEarliestFinanceInvoiceNoOption()
        {
            MoveToElementInPage(ApplyFinanceTransactionsToEarliestFinanceInvoice_NoRadioButton);
            WaitForElementToBeClickable(ApplyFinanceTransactionsToEarliestFinanceInvoice_NoRadioButton);
            Click(ApplyFinanceTransactionsToEarliestFinanceInvoice_NoRadioButton);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateApplyFinanceTransactionsToEarliestFinanceInvoiceFieldOptionsDisplayed()
        {
            WaitForElementVisible(ApplyFinanceTransactionsToEarliestFinanceInvoice_YesRadioButton);
            WaitForElementVisible(ApplyFinanceTransactionsToEarliestFinanceInvoice_NoRadioButton);
            MoveToElementInPage(ApplyFinanceTransactionsToEarliestFinanceInvoice_YesRadioButton);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateApplyFinanceTransactionsToEarliestFinanceInvoiceYesOptionChecked(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(ApplyFinanceTransactionsToEarliestFinanceInvoice_YesRadioButton);
                ValidateElementNotChecked(ApplyFinanceTransactionsToEarliestFinanceInvoice_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(ApplyFinanceTransactionsToEarliestFinanceInvoice_YesRadioButton);
                ValidateElementChecked(ApplyFinanceTransactionsToEarliestFinanceInvoice_NoRadioButton);
            }

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateApplyFinanceTransactionsToEarliestFinanceInvoiceOptionsIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(ApplyFinanceTransactionsToEarliestFinanceInvoice_YesRadioButton);
            MoveToElementInPage(ApplyFinanceTransactionsToEarliestFinanceInvoice_YesRadioButton);
            if (IsDisabled)
            {
                ValidateElementDisabled(ApplyFinanceTransactionsToEarliestFinanceInvoice_YesRadioButton);
                ValidateElementDisabled(ApplyFinanceTransactionsToEarliestFinanceInvoice_NoRadioButton);
            }
            else
            {
                ValidateElementNotDisabled(ApplyFinanceTransactionsToEarliestFinanceInvoice_YesRadioButton);
                ValidateElementNotDisabled(ApplyFinanceTransactionsToEarliestFinanceInvoice_NoRadioButton);
            }

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickApplySuspensionsYesOption()
        {
            MoveToElementInPage(ApplySuspensions_YesRadioButton);
            WaitForElementToBeClickable(ApplySuspensions_YesRadioButton);
            Click(ApplySuspensions_YesRadioButton);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickApplySuspensionsNoOption()
        {
            MoveToElementInPage(ApplySuspensions_NoRadioButton);
            WaitForElementToBeClickable(ApplySuspensions_NoRadioButton);
            Click(ApplySuspensions_NoRadioButton);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateApplySuspensionsFieldOptionsDisplayed()
        {
            WaitForElementVisible(ApplySuspensions_YesRadioButton);
            WaitForElementVisible(ApplySuspensions_NoRadioButton);
            MoveToElementInPage(ApplySuspensions_YesRadioButton);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateApplySuspensionsYesOptionChecked(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(ApplySuspensions_YesRadioButton);
                ValidateElementNotChecked(ApplySuspensions_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(ApplySuspensions_YesRadioButton);
                ValidateElementChecked(ApplySuspensions_NoRadioButton);
            }

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateApplySuspensionsOptionsIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(ApplySuspensions_YesRadioButton);
            MoveToElementInPage(ApplySuspensions_YesRadioButton);
            if (IsDisabled)
            {
                ValidateElementDisabled(ApplySuspensions_YesRadioButton);
                ValidateElementDisabled(ApplySuspensions_NoRadioButton);
            }
            else
            {
                ValidateElementNotDisabled(ApplySuspensions_YesRadioButton);
                ValidateElementNotDisabled(ApplySuspensions_NoRadioButton);
            }

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickCreateFinanceInvoiceBatchYesOption()
        {
            MoveToElementInPage(CreateFinanceInvoiceBatch_YesRadioButton);
            WaitForElementToBeClickable(CreateFinanceInvoiceBatch_YesRadioButton);
            Click(CreateFinanceInvoiceBatch_YesRadioButton);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickCreateFinanceInvoiceBatchNoOption()
        {
            MoveToElementInPage(CreateFinanceInvoiceBatch_NoRadioButton);
            WaitForElementToBeClickable(CreateFinanceInvoiceBatch_NoRadioButton);
            Click(CreateFinanceInvoiceBatch_NoRadioButton);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateCreateFinanceInvoiceBatchFieldOptionsDisplayed()
        {
            WaitForElementVisible(CreateFinanceInvoiceBatch_FieldLabel);
            WaitForElementVisible(CreateFinanceInvoiceBatch_YesRadioButton);
            WaitForElementVisible(CreateFinanceInvoiceBatch_NoRadioButton);
            MoveToElementInPage(CreateFinanceInvoiceBatch_YesRadioButton);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateCreateFinanceInvoiceBatchYesOptionChecked(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(CreateFinanceInvoiceBatch_YesRadioButton);
                ValidateElementNotChecked(CreateFinanceInvoiceBatch_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(CreateFinanceInvoiceBatch_YesRadioButton);
                ValidateElementChecked(CreateFinanceInvoiceBatch_NoRadioButton);
            }

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateCreateFinanceInvoiceBatchOptionsIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(CreateFinanceInvoiceBatch_YesRadioButton);
            MoveToElementInPage(CreateFinanceInvoiceBatch_YesRadioButton);
            if (IsDisabled)
            {
                ValidateElementDisabled(CreateFinanceInvoiceBatch_YesRadioButton);
                ValidateElementDisabled(CreateFinanceInvoiceBatch_NoRadioButton);
            }
            else
            {
                ValidateElementNotDisabled(CreateFinanceInvoiceBatch_YesRadioButton);
                ValidateElementNotDisabled(CreateFinanceInvoiceBatch_NoRadioButton);
            }

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickCreditorReferenceNumberRequiredYesOption()
        {
            MoveToElementInPage(CreditorReferenceNumberRequired_YesRadioButton);
            WaitForElementToBeClickable(CreditorReferenceNumberRequired_YesRadioButton);
            Click(CreditorReferenceNumberRequired_YesRadioButton);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickCreditorReferenceNumberRequiredNoOption()
        {
            MoveToElementInPage(CreditorReferenceNumberRequired_NoRadioButton);
            WaitForElementToBeClickable(CreditorReferenceNumberRequired_NoRadioButton);
            Click(CreditorReferenceNumberRequired_NoRadioButton);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateCreditorReferenceNumberRequiredFieldOptionsDisplayed()
        {
            WaitForElementVisible(CreditorReferenceNumberRequired_YesRadioButton);
            WaitForElementVisible(CreditorReferenceNumberRequired_NoRadioButton);
            MoveToElementInPage(CreditorReferenceNumberRequired_YesRadioButton);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateCreditorReferenceNumberRequiredYesOptionChecked(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(CreditorReferenceNumberRequired_YesRadioButton);
                ValidateElementNotChecked(CreditorReferenceNumberRequired_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(CreditorReferenceNumberRequired_YesRadioButton);
                ValidateElementChecked(CreditorReferenceNumberRequired_NoRadioButton);
            }

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateCreditorReferenceNumberRequiredOptionsIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(CreditorReferenceNumberRequired_YesRadioButton);
            MoveToElementInPage(CreditorReferenceNumberRequired_YesRadioButton);
            if (IsDisabled)
            {
                ValidateElementDisabled(CreditorReferenceNumberRequired_YesRadioButton);
                ValidateElementDisabled(CreditorReferenceNumberRequired_NoRadioButton);
            }
            else
            {
                ValidateElementNotDisabled(CreditorReferenceNumberRequired_YesRadioButton);
                ValidateElementNotDisabled(CreditorReferenceNumberRequired_NoRadioButton);
            }

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickSeparateInvoicesYesOption()
        {
            MoveToElementInPage(SeparateInvoices_YesRadioButton);
            WaitForElementToBeClickable(SeparateInvoices_YesRadioButton);
            Click(SeparateInvoices_YesRadioButton);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickSeparateInvoicesNoOption()
        {
            MoveToElementInPage(SeparateInvoices_NoRadioButton);
            WaitForElementToBeClickable(SeparateInvoices_NoRadioButton);
            Click(SeparateInvoices_NoRadioButton);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateSeparateInvoicesFieldOptionsDisplayed()
        {
            WaitForElementVisible(SeparateInvoices_YesRadioButton);
            WaitForElementVisible(SeparateInvoices_NoRadioButton);
            MoveToElementInPage(SeparateInvoices_YesRadioButton);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateSeparateInvoicesYesOptionChecked(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(SeparateInvoices_YesRadioButton);
                ValidateElementNotChecked(SeparateInvoices_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(SeparateInvoices_YesRadioButton);
                ValidateElementChecked(SeparateInvoices_NoRadioButton);
            }

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateSeparateInvoicesOptionsIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(SeparateInvoices_YesRadioButton);
            MoveToElementInPage(SeparateInvoices_YesRadioButton);
            if (IsDisabled)
            {
                ValidateElementDisabled(SeparateInvoices_YesRadioButton);
                ValidateElementDisabled(SeparateInvoices_NoRadioButton);
            }
            else
            {
                ValidateElementNotDisabled(SeparateInvoices_YesRadioButton);
                ValidateElementNotDisabled(SeparateInvoices_NoRadioButton);
            }

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            MoveToElementInPage(SaveButton);
            Click(SaveButton);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            MoveToElementInPage(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            MoveToElementInPage(BackButton);
            Click(BackButton);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);
            WaitForElementVisible(AssignRecordButton);
            WaitForElementVisible(DeleteButton);
            WaitForElementVisible(AdditionalToolbarButton);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateMessageAreaText(string ExpectedText)
        {
            WaitForElement(NotificationMessage);
            ValidateElementText(NotificationMessage, ExpectedText);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateFinanceInvoiceBatchSetupRecordPageFieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(FinanceModule_Field_NottificationMessage);
            ValidateElementText(FinanceModule_Field_NottificationMessage, ExpectedText);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickRuleTypeLookupButton()
        {
            WaitForElementToBeClickable(RuleType_LookupButtonField);
            MoveToElementInPage(RuleType_LookupButtonField);
            Click(RuleType_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateRuleTypeLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(RuleType_LookupButtonField);
            MoveToElementInPage(RuleType_LookupButtonField);
            if (IsDisabled)
                ValidateElementDisabled(RuleType_LookupButtonField);
            else
                ValidateElementNotDisabled(RuleType_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickContributionTypeLookupButton()
        {
            WaitForElementToBeClickable(ContributionType_LookupButtonField);
            MoveToElementInPage(ContributionType_LookupButtonField);
            Click(ContributionType_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateContributionTypeLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(ContributionType_LookupButtonField);
            MoveToElementInPage(ContributionType_LookupButtonField);
            if (IsDisabled)
                ValidateElementDisabled(ContributionType_LookupButtonField);
            else
                ValidateElementNotDisabled(ContributionType_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickRecoveryMethodLookupButton()
        {
            WaitForElementToBeClickable(RecoveryMethod_LookupButtonField);
            MoveToElementInPage(RecoveryMethod_LookupButtonField);
            Click(RecoveryMethod_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateRecoveryMethodLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(RecoveryMethod_LookupButtonField);
            MoveToElementInPage(RecoveryMethod_LookupButtonField);
            if (IsDisabled)
                ValidateElementDisabled(RecoveryMethod_LookupButtonField);
            else
                ValidateElementNotDisabled(RecoveryMethod_LookupButtonField);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickDebtorReferenceNumberRequiredYesOption()
        {
            MoveToElementInPage(DebtorReferenceNumberRequired_YesRadioButton);
            WaitForElementToBeClickable(DebtorReferenceNumberRequired_YesRadioButton);
            Click(DebtorReferenceNumberRequired_YesRadioButton);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickDebtorReferenceNumberRequiredNoOption()
        {
            MoveToElementInPage(DebtorReferenceNumberRequired_NoRadioButton);
            WaitForElementToBeClickable(DebtorReferenceNumberRequired_NoRadioButton);
            Click(DebtorReferenceNumberRequired_NoRadioButton);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateDebtorReferenceNumberRequiredFieldOptionsDisplayed()
        {
            WaitForElementVisible(DebtorReferenceNumberRequired_YesRadioButton);
            WaitForElementVisible(DebtorReferenceNumberRequired_NoRadioButton);
            MoveToElementInPage(DebtorReferenceNumberRequired_YesRadioButton);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateDebtorReferenceNumberRequiredYesOptionChecked(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(DebtorReferenceNumberRequired_YesRadioButton);
                ValidateElementNotChecked(DebtorReferenceNumberRequired_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(DebtorReferenceNumberRequired_YesRadioButton);
                ValidateElementChecked(DebtorReferenceNumberRequired_NoRadioButton);
            }

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateDebtorReferenceNumberRequiredOptionsIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(DebtorReferenceNumberRequired_YesRadioButton);
            MoveToElementInPage(DebtorReferenceNumberRequired_YesRadioButton);
            if (IsDisabled)
            {
                ValidateElementDisabled(DebtorReferenceNumberRequired_YesRadioButton);
                ValidateElementDisabled(DebtorReferenceNumberRequired_NoRadioButton);
            }
            else
            {
                ValidateElementNotDisabled(DebtorReferenceNumberRequired_YesRadioButton);
                ValidateElementNotDisabled(DebtorReferenceNumberRequired_NoRadioButton);
            }

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateAssignRecordButtonIsVisible()
        {
            WaitForElementVisible(AssignRecordButton);
            MoveToElementInPage(AssignRecordButton);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateServiceElement1FieldErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(ServiceElement1_Field_NottificationMessage);
            MoveToElementInPage(ServiceElement1_Field_NottificationMessage);
            ValidateElementText(ServiceElement1_Field_NottificationMessage, ExpectedText);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidatePaymentTypeFieldErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(PaymentType_Field_NottificationMessage);
            MoveToElementInPage(PaymentType_Field_NottificationMessage);
            ValidateElementText(PaymentType_Field_NottificationMessage, ExpectedText);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateProviderBatchGroupingFieldErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(ProviderBatchGrouping_Field_NottificationMessage);
            MoveToElementInPage(ProviderBatchGrouping_Field_NottificationMessage);
            ValidateElementText(ProviderBatchGrouping_Field_NottificationMessage, ExpectedText);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateStartDateFieldErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(StartDate_Field_NottificationMessage);
            MoveToElementInPage(StartDate_Field_NottificationMessage);
            ValidateElementText(StartDate_Field_NottificationMessage, ExpectedText);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateInvoiceByFieldErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(InvoiceBy_Field_NottificationMessage);
            MoveToElementInPage(InvoiceBy_Field_NottificationMessage);
            ValidateElementText(InvoiceBy_Field_NottificationMessage, ExpectedText);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateCreateBatchWithinFieldErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(CreateBatchWithin_Field_NottificationMessage);
            MoveToElementInPage(CreateBatchWithin_Field_NottificationMessage);
            ValidateElementText(CreateBatchWithin_Field_NottificationMessage, ExpectedText);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateInvoiceFrequencyFieldErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(InvoiceFrequency_Field_NottificationMessage);
            MoveToElementInPage(InvoiceFrequency_Field_NottificationMessage);
            ValidateElementText(InvoiceFrequency_Field_NottificationMessage, ExpectedText);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidatePayToDayFieldErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(PayToDay_Field_NottificationMessage);
            MoveToElementInPage(PayToDay_Field_NottificationMessage);
            ValidateElementText(PayToDay_Field_NottificationMessage, ExpectedText);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateExtractNameFieldErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(ExtractName_Field_NottificationMessage);
            MoveToElementInPage(ExtractName_Field_NottificationMessage);
            ValidateElementText(ExtractName_Field_NottificationMessage, ExpectedText);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(DeleteButton);
            MoveToElementInPage(DeleteButton);
            Click(DeleteButton);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateCreateFinanceTransactionsFieldOptionsDisplayed(bool visibility = true)
        {
            if (visibility)
            {
                WaitForElementVisible(CreateFinanceTransactions_FieldLabel);
                WaitForElementVisible(CreateFinanceTransactions_YesRadioButton);
                WaitForElementVisible(CreateFinanceTransactions_NoRadioButton);
                MoveToElementInPage(CreateFinanceTransactions_YesRadioButton);
            }
            else
            {
                WaitForElementNotVisible(CreateFinanceTransactions_FieldLabel, 5);
                WaitForElementNotVisible(CreateFinanceTransactions_YesRadioButton, 5);
                WaitForElementNotVisible(CreateFinanceTransactions_NoRadioButton, 5);
            }

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickCreateFinanceTransactionsYesOption()
        {
            MoveToElementInPage(CreateFinanceTransactions_YesRadioButton);
            WaitForElementToBeClickable(CreateFinanceTransactions_YesRadioButton);
            Click(CreateFinanceTransactions_YesRadioButton);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ClickCreateFinanceTransactionsNoOption()
        {
            MoveToElementInPage(CreateFinanceTransactions_NoRadioButton);
            WaitForElementToBeClickable(CreateFinanceTransactions_NoRadioButton);
            Click(CreateFinanceTransactions_NoRadioButton);

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateCreateFinanceTransactionsYesOptionChecked(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(CreateFinanceTransactions_YesRadioButton);
                ValidateElementNotChecked(CreateFinanceTransactions_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(CreateFinanceTransactions_YesRadioButton);
                ValidateElementChecked(CreateFinanceTransactions_NoRadioButton);
            }

            return this;
        }

        public FinanceInvoiceBatchSetupRecordPage ValidateCreateFinanceTransactionsOptionsIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(CreateFinanceTransactions_YesRadioButton);
            MoveToElementInPage(CreateFinanceTransactions_YesRadioButton);

            if (IsDisabled)
            {
                ValidateElementDisabled(CreateFinanceTransactions_YesRadioButton);
                ValidateElementDisabled(CreateFinanceTransactions_NoRadioButton);
            }
            else
            {
                ValidateElementNotDisabled(CreateFinanceTransactions_YesRadioButton);
                ValidateElementNotDisabled(CreateFinanceTransactions_NoRadioButton);
            }

            return this;
        }


    }
}
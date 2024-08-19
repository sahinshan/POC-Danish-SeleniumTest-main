using Microsoft.Office.Interop.Word;
using OpenQA.Selenium;
using Phoenix.UITests.Framework.PageObjects.Settings.Configuration.CPFinanceAdmin;
using Phoenix.UITests.Framework.WebAppAPI.Classes;
using System.Linq;
using System.Web.Util;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CPFinanceInvoiceBatchSetupRecordPage : CommonMethods
    {
        public CPFinanceInvoiceBatchSetupRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        #region Internal Properties

        readonly By ContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderfinanceinvoicebatchsetup')]");


        readonly By BackButton = By.XPath("//*[@id='BackButton']");
        readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
        readonly By AdditionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By DeleteButton = By.XPath("//*[@id='TI_DeleteRecordButton']");
        readonly By EndDateButton = By.XPath("//*[@id='TI_EndDateButton']");

        readonly By Issundry_1 = By.XPath("//*[@id='CWField_issundry_1']");
        readonly By Issundry_0 = By.XPath("//*[@id='CWField_issundry_0']");

        readonly By CareprovidercontractschemeidLink = By.XPath("//*[@id='CWField_careprovidercontractschemeid_Link']");
        readonly By CareprovidercontractschemeidLookupButton = By.XPath("//*[@id='CWLookupBtn_careprovidercontractschemeid']");
        readonly By CareproviderbatchgroupingidLink = By.XPath("//*[@id='CWField_careproviderbatchgroupingid_Link']");
        readonly By CareproviderbatchgroupingidLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderbatchgroupingid']");

        readonly By Startdate = By.XPath("//*[@id='CWField_startdate']");
        readonly By Starttime = By.XPath("//*[@id='CWField_starttime']");
        readonly By Starttime_TimePicker = By.XPath("//*[@id='CWField_starttime_TimePicker']");
        readonly By Enddate = By.XPath("//*[@id='CWField_enddate']");

        By Invoicebyid_Option(string optionText) => By.XPath("//*[@id='CWField_invoicebyid']/option[text()='" + optionText + "']");
        readonly By Invoicebyid = By.XPath("//*[@id='CWField_invoicebyid']");
        readonly By Createbatchwithin = By.XPath("//*[@id='CWField_createbatchwithin']");
        readonly By Whentobatchfinancetransactionsid = By.XPath("//*[@id='CWField_whentobatchfinancetransactionsid']");
        readonly By Financetransactionsupto = By.XPath("//*[@id='CWField_financetransactionsupto']");
        readonly By Careproviderinvoicefrequencyid = By.XPath("//*[@id='CWField_careproviderinvoicefrequencyid']");
        readonly By Chargetodayid = By.XPath("//*[@id='CWField_chargetodayid']");
        readonly By Useenddatewhenbatchingfinancetransactions_Label = By.XPath("//*[@id='CWLabelHolder_useenddatewhenbatchingfinancetransactions']/label");
        readonly By Useenddatewhenbatchingfinancetransactions_1 = By.XPath("//*[@id='CWField_useenddatewhenbatchingfinancetransactions_1']");
        readonly By Useenddatewhenbatchingfinancetransactions_0 = By.XPath("//*[@id='CWField_useenddatewhenbatchingfinancetransactions_0']");
        readonly By Separateinvoices_1 = By.XPath("//*[@id='CWField_separateinvoices_1']");
        readonly By Separateinvoices_0 = By.XPath("//*[@id='CWField_separateinvoices_0']");

        readonly By Invoicetext = By.XPath("//*[@id='CWField_invoicetext']");
        readonly By Transactiontextcontra = By.XPath("//*[@id='CWField_transactiontextcontra']");
        readonly By Transactiontextadditional = By.XPath("//*[@id='CWField_transactiontextadditional']");
        readonly By Transactiontextapportioned = By.XPath("//*[@id='CWField_transactiontextapportioned']");
        readonly By Transactiontextapportionedprovider = By.XPath("//*[@id='CWField_transactiontextapportionedprovider']");
        readonly By Transactiontextsundry = By.XPath("//*[@id='CWField_transactiontextsundry']");
        readonly By Transactiontextreduction = By.XPath("//*[@id='CWField_transactiontextreduction']");
        readonly By Transactiontextstandard = By.XPath("//*[@id='CWField_transactiontextstandard']");
        readonly By Transactiontextend = By.XPath("//*[@id='CWField_transactiontextend']");
        readonly By Transactiontextnetincome = By.XPath("//*[@id='CWField_transactiontextnetincome']");
        readonly By Transactiontextapportionedfunder = By.XPath("//*[@id='CWField_transactiontextapportionedfunder']");
        readonly By Transactiontextadjustment = By.XPath("//*[@id='CWField_transactiontextadjustment']");
        readonly By Transactiontextexpense = By.XPath("//*[@id='CWField_transactiontextexpense']");

        readonly By CareproviderextractnameidLink = By.XPath("//*[@id='CWField_careproviderextractnameid_Link']");
        readonly By CareproviderextractnameidClearButton = By.XPath("//*[@id='CWClearLookup_careproviderextractnameid']");
        readonly By CareproviderextractnameidLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderextractnameid']");
        readonly By Debtorreferencenumberrequired_1 = By.XPath("//*[@id='CWField_debtorreferencenumberrequired_1']");
        readonly By Debtorreferencenumberrequired_0 = By.XPath("//*[@id='CWField_debtorreferencenumberrequired_0']");

        readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");

		readonly By FinanceInvoiceBatchesTab = By.XPath("//li[@id = 'CWNavGroup_FinanceInvoiceBatches']/a");

        #endregion


        public CPFinanceInvoiceBatchSetupRecordPage WaitForPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(ContentIFrame);
            SwitchToIframe(ContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElementVisible(Issundry_1);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateToolbarOptionsDisplayed()
        {
            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);
            WaitForElementVisible(AssignRecordButton);
            WaitForElementVisible(DeleteButton);

            return this;
        }
        public CPFinanceInvoiceBatchSetupRecordPage ValidateBatchingTypeSectionFieldsVisible()
        {
            WaitForElementVisible(Issundry_1);
            WaitForElementVisible(Issundry_0);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateBatchGroupingSectionFieldsVisible(bool ContractSchemeVisible = true, bool BatchGroupingVisible = true)
        {
            if (ContractSchemeVisible)
                WaitForElementVisible(CareprovidercontractschemeidLookupButton);
            else
                WaitForElementNotVisible(CareprovidercontractschemeidLookupButton, 3);

            if (BatchGroupingVisible)
                WaitForElementVisible(CareproviderbatchgroupingidLookupButton);
            else
                WaitForElementNotVisible(CareproviderbatchgroupingidLookupButton, 3);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateDatesSectionFieldsVisible()
        {
            WaitForElementVisible(Startdate);
            WaitForElementVisible(Starttime);
            WaitForElementVisible(Starttime_TimePicker);
            WaitForElementVisible(Enddate);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateBatchingCriteriaSectionFieldsVisible(bool CreateBatchWithinVisible = true, bool FinanceTransactionsUpToVisible = true)
        {
            WaitForElementVisible(Invoicebyid);

            if (CreateBatchWithinVisible)
                WaitForElementVisible(Createbatchwithin);
            else
                WaitForElementNotVisible(Createbatchwithin, 3);

            WaitForElementVisible(Whentobatchfinancetransactionsid);

            if (FinanceTransactionsUpToVisible)
                WaitForElementVisible(Financetransactionsupto);
            else
                WaitForElementNotVisible(Financetransactionsupto, 3);

            WaitForElementVisible(Careproviderinvoicefrequencyid);
            WaitForElementVisible(Chargetodayid);
            WaitForElementVisible(Useenddatewhenbatchingfinancetransactions_1);
            WaitForElementVisible(Useenddatewhenbatchingfinancetransactions_0);
            WaitForElementVisible(Separateinvoices_1);
            WaitForElementVisible(Separateinvoices_0);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateFinanceInvoiceTextSectionFieldsVisible(
            bool InvoiceTextVisible = true, bool TransactionTextStandardVisible = true, bool TransactionTextContraVisible = true, bool TransactionTextEndReasonVisible = true,
            bool TransactionTextAdditionalVisible = true, bool TransactionTextNetIncomeVisible = true, bool TransactionTextApportionedPersonVisible = true,
            bool TransactionTextApportionedFunderVisible = true, bool TransactionTextApportionedProviderVisible = true, bool TransactionTextAdjustmentVisible = true,
            bool ReductionTransactionTextVisible = true, bool TransactionTextExpenseVisible = true, bool TransactionTextSundryVisible = false)
        {
            if (InvoiceTextVisible)
                WaitForElementVisible(Invoicetext);
            else
                WaitForElementNotVisible(Invoicetext, 3);

            if (TransactionTextStandardVisible)
                WaitForElementVisible(Transactiontextstandard);
            else
                WaitForElementNotVisible(Transactiontextstandard, 3);

            if (TransactionTextContraVisible)
                WaitForElementVisible(Transactiontextcontra);
            else
                WaitForElementNotVisible(Transactiontextcontra, 3);

            if (TransactionTextEndReasonVisible)
                WaitForElementVisible(Transactiontextend);
            else
                WaitForElementNotVisible(Transactiontextend, 3);

            if (TransactionTextAdditionalVisible)
                WaitForElementVisible(Transactiontextadditional);
            else
                WaitForElementNotVisible(Transactiontextadditional, 3);

            if (TransactionTextNetIncomeVisible)
                WaitForElementVisible(Transactiontextnetincome);
            else
                WaitForElementNotVisible(Transactiontextnetincome, 3);

            if (TransactionTextApportionedPersonVisible)
                WaitForElementVisible(Transactiontextapportioned);
            else
                WaitForElementNotVisible(Transactiontextapportioned, 3);

            if (TransactionTextApportionedFunderVisible)
                WaitForElementVisible(Transactiontextapportionedfunder);
            else
                WaitForElementNotVisible(Transactiontextapportionedfunder, 3);

            if (TransactionTextApportionedProviderVisible)
                WaitForElementVisible(Transactiontextapportionedprovider);
            else
                WaitForElementNotVisible(Transactiontextapportionedprovider, 3);

            if (TransactionTextAdjustmentVisible)
                WaitForElementVisible(Transactiontextadjustment);
            else
                WaitForElementNotVisible(Transactiontextadjustment, 3);

            if (ReductionTransactionTextVisible)
                WaitForElementVisible(Transactiontextreduction);
            else
                WaitForElementNotVisible(Transactiontextreduction, 3);

            if (TransactionTextExpenseVisible)
                WaitForElementVisible(Transactiontextexpense);
            else
                WaitForElementNotVisible(Transactiontextexpense, 3);

            if (TransactionTextSundryVisible)
                WaitForElementVisible(Transactiontextsundry);
            else
                WaitForElementNotVisible(Transactiontextsundry, 3);

            if (TransactionTextSundryVisible)
                WaitForElementVisible(Transactiontextsundry);
            else
                WaitForElementNotVisible(Transactiontextsundry, 3);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateOtherSettingsSectionFieldsVisible()
        {
            WaitForElementVisible(CareproviderextractnameidLookupButton);
            WaitForElementVisible(Debtorreferencenumberrequired_1);
            WaitForElementVisible(Debtorreferencenumberrequired_0);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateGeneralSectionFieldsVisible()
        {
            WaitForElementVisible(ResponsibleTeamLink);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ClickAdditionalItemsButton()
        {
            WaitForElementToBeClickable(AdditionalItemsButton);
            Click(AdditionalItemsButton);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(AdditionalItemsButton);
            Click(AdditionalItemsButton);

            WaitForElementToBeClickable(DeleteButton);
            Click(DeleteButton);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ClickEndDateButton()
        {
            WaitForElementToBeClickable(EndDateButton);
            Click(EndDateButton);

            return this;
        }


        public CPFinanceInvoiceBatchSetupRecordPage ValidateIsThisASundryBatchFieldEnabled(bool ExpectEnabled)
        {
            if(ExpectEnabled)
            {
                ValidateElementNotDisabled(Issundry_0);
                ValidateElementNotDisabled(Issundry_1);
            }
            else
            {
                ValidateElementDisabled(Issundry_0);
                ValidateElementDisabled(Issundry_1);
            }

            return this;
        }


        public CPFinanceInvoiceBatchSetupRecordPage ClickIsThisASundryBatch_YesRadioButton()
        {
            WaitForElementToBeClickable(Issundry_1);
            Click(Issundry_1);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateIsThisASundryBatch_YesRadioButtonChecked()
        {
            WaitForElement(Issundry_1);
            ValidateElementChecked(Issundry_1);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateIsThisASundryBatch_YesRadioButtonNotChecked()
        {
            WaitForElement(Issundry_1);
            ValidateElementNotChecked(Issundry_1);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ClickIsThisASundryBatch_NoRadioButton()
        {
            WaitForElementToBeClickable(Issundry_0);
            Click(Issundry_0);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateIsThisASundryBatch_NoRadioButtonChecked()
        {
            WaitForElement(Issundry_0);
            ValidateElementChecked(Issundry_0);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateIsThisASundryBatch_NoRadioButtonNotChecked()
        {
            WaitForElement(Issundry_0);
            ValidateElementNotChecked(Issundry_0);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ClickContractSchemeLink()
        {
            WaitForElementToBeClickable(CareprovidercontractschemeidLink);
            Click(CareprovidercontractschemeidLink);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateContractSchemeLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CareprovidercontractschemeidLink);
            ValidateElementText(CareprovidercontractschemeidLink, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ClickContractSchemeLookupButton()
        {
            WaitForElementToBeClickable(CareprovidercontractschemeidLookupButton);
            Click(CareprovidercontractschemeidLookupButton);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ClickBatchGroupingLink()
        {
            WaitForElementToBeClickable(CareproviderbatchgroupingidLink);
            Click(CareproviderbatchgroupingidLink);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateBatchGroupingLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CareproviderbatchgroupingidLink);
            ValidateElementText(CareproviderbatchgroupingidLink, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ClickBatchGroupingLookupButton()
        {
            WaitForElementToBeClickable(CareproviderbatchgroupingidLookupButton);
            Click(CareproviderbatchgroupingidLookupButton);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateStartDateText(string ExpectedText)
        {
            ValidateElementValueByJavascript("CWField_startdate", ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage InsertTextOnStartDate(string TextToInsert)
        {
            WaitForElementToBeClickable(Startdate);
            SendKeys(Startdate, TextToInsert);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateStartTimeText(string ExpectedText)
        {
            ValidateElementValueByJavascript("CWField_starttime", ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage InsertTextOnStartTime(string TextToInsert)
        {
            WaitForElementToBeClickable(Starttime);
            SendKeys(Starttime, TextToInsert);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ClickStartTime_TimePicker()
        {
            WaitForElementToBeClickable(Starttime_TimePicker);
            Click(Starttime_TimePicker);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateEndDateNotEditable()
        {
            ValidateElementDisabled(Enddate);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateEndDateText(string ExpectedText)
        {
            ValidateElementValueByJavascript("CWField_enddate", ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage InsertTextOnEndDate(string TextToInsert)
        {
            WaitForElementToBeClickable(Enddate);
            SendKeys(Enddate, TextToInsert);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateInvoiceByOptionDisabled(string OptionText)
        {
            ValidateElementDisabled(Invoicebyid_Option(OptionText));

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateInvoiceByVisibility(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(Invoicebyid);
            else
                WaitForElementNotVisible(Invoicebyid, 3);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateInvoiceByNotEditable()
        {
            ValidateElementDisabled(Invoicebyid);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage SelectInvoiceBy(string TextToSelect)
        {
            WaitForElementToBeClickable(Invoicebyid);
            SelectPicklistElementByText(Invoicebyid, TextToSelect);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateInvoiceBySelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Invoicebyid, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateCreateBatchWithinVisibility(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(Createbatchwithin);
            else
                WaitForElementNotVisible(Createbatchwithin, 3);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateCreateBatchWithinText(string ExpectedText)
        {
            ValidateElementValue(Createbatchwithin, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage InsertTextOnCreateBatchWithin(string TextToInsert)
        {
            WaitForElementToBeClickable(Createbatchwithin);
            SendKeys(Createbatchwithin, TextToInsert);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage SelectWhenToBatchFinanceTransactions(string TextToSelect)
        {
            WaitForElementToBeClickable(Whentobatchfinancetransactionsid);
            SelectPicklistElementByText(Whentobatchfinancetransactionsid, TextToSelect);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateWhenToBatchFinanceTransactionsNotEditable()
        {
            ValidateElementDisabled(Whentobatchfinancetransactionsid);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateWhenToBatchFinanceTransactionsSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Whentobatchfinancetransactionsid, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateFinanceTransactionsUpToText(string ExpectedText)
        {
            ValidateElementValue(Financetransactionsupto, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage InsertTextOnFinanceTransactionsUpTo(string TextToInsert)
        {
            WaitForElementToBeClickable(Financetransactionsupto);
            SendKeys(Financetransactionsupto, TextToInsert);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateInvoiceFrequencyNotEditable()
        {
            ValidateElementDisabled(Careproviderinvoicefrequencyid);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage SelectInvoiceFrequency(string TextToSelect)
        {
            WaitForElementToBeClickable(Careproviderinvoicefrequencyid);
            SelectPicklistElementByText(Careproviderinvoicefrequencyid, TextToSelect);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateInvoiceFrequencySelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Careproviderinvoicefrequencyid, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage SelectChargeToDay(string TextToSelect)
        {
            WaitForElementToBeClickable(Chargetodayid);
            SelectPicklistElementByText(Chargetodayid, TextToSelect);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateChargeToDaySelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Chargetodayid, ExpectedText);
            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateUseTransactionEndDateWhenBatchingFinanceTransactionsTooltip(string ExpectedTooltip)
        {
            ValidateElementAttribute(Useenddatewhenbatchingfinancetransactions_Label, "title", ExpectedTooltip);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateUseTransactionEndDateWhenBatchingFinanceTransactionsNotEditable()
        {
            ValidateElementDisabled(Useenddatewhenbatchingfinancetransactions_0);
            ValidateElementDisabled(Useenddatewhenbatchingfinancetransactions_1);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ClickUseTransactionEndDateWhenBatchingFinanceTransactions_YesRadioButton()
        {
            WaitForElementToBeClickable(Useenddatewhenbatchingfinancetransactions_1);
            Click(Useenddatewhenbatchingfinancetransactions_1);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateUseTransactionEndDateWhenBatchingFinanceTransactions_YesRadioButtonChecked()
        {
            WaitForElement(Useenddatewhenbatchingfinancetransactions_1);
            ValidateElementChecked(Useenddatewhenbatchingfinancetransactions_1);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateUseTransactionEndDateWhenBatchingFinanceTransactions_YesRadioButtonNotChecked()
        {
            WaitForElement(Useenddatewhenbatchingfinancetransactions_1);
            ValidateElementNotChecked(Useenddatewhenbatchingfinancetransactions_1);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ClickUseTransactionEndDateWhenBatchingFinanceTransactions_NoRadioButton()
        {
            WaitForElementToBeClickable(Useenddatewhenbatchingfinancetransactions_0);
            Click(Useenddatewhenbatchingfinancetransactions_0);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateUseTransactionEndDateWhenBatchingFinanceTransactions_NoRadioButtonChecked()
        {
            WaitForElement(Useenddatewhenbatchingfinancetransactions_0);
            ValidateElementChecked(Useenddatewhenbatchingfinancetransactions_0);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateUseTransactionEndDateWhenBatchingFinanceTransactions_NoRadioButtonNotChecked()
        {
            WaitForElement(Useenddatewhenbatchingfinancetransactions_0);
            ValidateElementNotChecked(Useenddatewhenbatchingfinancetransactions_0);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateSeparateInvoicesNotEditable()
        {
            ValidateElementDisabled(Separateinvoices_0);
            ValidateElementDisabled(Separateinvoices_1);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ClickSeparateInvoices_YesRadioButton()
        {
            WaitForElementToBeClickable(Separateinvoices_1);
            Click(Separateinvoices_1);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateSeparateInvoices_YesRadioButtonChecked()
        {
            WaitForElement(Separateinvoices_1);
            ValidateElementChecked(Separateinvoices_1);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateSeparateInvoices_YesRadioButtonNotChecked()
        {
            WaitForElement(Separateinvoices_1);
            ValidateElementNotChecked(Separateinvoices_1);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ClickSeparateInvoices_NoRadioButton()
        {
            WaitForElementToBeClickable(Separateinvoices_0);
            Click(Separateinvoices_0);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateSeparateInvoices_NoRadioButtonChecked()
        {
            WaitForElement(Separateinvoices_0);
            ValidateElementChecked(Separateinvoices_0);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateSeparateInvoices_NoRadioButtonNotChecked()
        {
            WaitForElement(Separateinvoices_0);
            ValidateElementNotChecked(Separateinvoices_0);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateInvoiceTextTooltip(string ExpectedTooltip)
        {
            ValidateElementAttribute(Invoicetext, "title", ExpectedTooltip);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateInvoiceText_Text(string ExpectedText)
        {
            ValidateElementValue(Invoicetext, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage InsertTextOnInvoiceText(string TextToInsert)
        {
            WaitForElementToBeClickable(Invoicetext);
            SendKeys(Invoicetext, TextToInsert);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateTransactionTextContraTooltip(string ExpectedTooltip)
        {
            ValidateElementAttribute(Transactiontextcontra, "title", ExpectedTooltip);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateTransactionTextContraText(string ExpectedText)
        {
            ValidateElementValue(Transactiontextcontra, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage InsertTextOnTransactionTextContra(string TextToInsert)
        {
            WaitForElementToBeClickable(Transactiontextcontra);
            SendKeys(Transactiontextcontra, TextToInsert);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateTransactionTextAdditionalTooltip(string ExpectedTooltip)
        {
            ValidateElementAttribute(Transactiontextadditional, "title", ExpectedTooltip);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateTransactionTextAdditionalText(string ExpectedText)
        {
            ValidateElementValue(Transactiontextadditional, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage InsertTextOnTransactionTextAdditional(string TextToInsert)
        {
            WaitForElementToBeClickable(Transactiontextadditional);
            SendKeys(Transactiontextadditional, TextToInsert);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateTransactionTextApportionedPersonTooltip(string ExpectedTooltip)
        {
            ValidateElementAttribute(Transactiontextapportioned, "title", ExpectedTooltip);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateTransactionTextApportionedPersonText(string ExpectedText)
        {
            ValidateElementValue(Transactiontextapportioned, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage InsertTextOnTransactionTextApportionedPerson(string TextToInsert)
        {
            WaitForElementToBeClickable(Transactiontextapportioned);
            SendKeys(Transactiontextapportioned, TextToInsert);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateTransactionTextApportionedProviderTooltip(string ExpectedTooltip)
        {
            ValidateElementAttribute(Transactiontextapportionedprovider, "title", ExpectedTooltip);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateTransactionTextApportionedProviderText(string ExpectedText)
        {
            ValidateElementValue(Transactiontextapportionedprovider, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage InsertTextOnTransactionTextApportionedProvider(string TextToInsert)
        {
            WaitForElementToBeClickable(Transactiontextapportionedprovider);
            SendKeys(Transactiontextapportionedprovider, TextToInsert);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateTransactionTextSundryText(string ExpectedText)
        {
            ValidateElementValue(Transactiontextsundry, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage InsertTextOnTransactionTextSundry(string TextToInsert)
        {
            WaitForElementToBeClickable(Transactiontextsundry);
            SendKeys(Transactiontextsundry, TextToInsert);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateReductionTransactionText_Text(string ExpectedText)
        {
            ValidateElementValue(Transactiontextreduction, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage InsertTextOnReductionTransactionText(string TextToInsert)
        {
            WaitForElementToBeClickable(Transactiontextreduction);
            SendKeys(Transactiontextreduction, TextToInsert);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateTransactionTextStandardTooltip(string ExpectedTooltip)
        {
            ValidateElementAttribute(Transactiontextstandard, "title", ExpectedTooltip);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateTransactionTextStandardText(string ExpectedText)
        {
            ValidateElementValue(Transactiontextstandard, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage InsertTextOnTransactionTextStandard(string TextToInsert)
        {
            WaitForElementToBeClickable(Transactiontextstandard);
            SendKeys(Transactiontextstandard, TextToInsert);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateTransactionTextEndReasonTooltip(string ExpectedTooltip)
        {
            ValidateElementAttribute(Transactiontextend, "title", ExpectedTooltip);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateTransactionTextEndReasonText(string ExpectedText)
        {
            ValidateElementValue(Transactiontextend, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage InsertTextOnTransactionTextEndReason(string TextToInsert)
        {
            WaitForElementToBeClickable(Transactiontextend);
            SendKeys(Transactiontextend, TextToInsert);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateTransactionTextNetIncomeTooltip(string ExpectedTooltip)
        {
            ValidateElementAttribute(Transactiontextnetincome, "title", ExpectedTooltip);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateTransactionTextNetIncomeText(string ExpectedText)
        {
            ValidateElementValue(Transactiontextnetincome, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage InsertTextOnTransactionTextNetIncome(string TextToInsert)
        {
            WaitForElementToBeClickable(Transactiontextnetincome);
            SendKeys(Transactiontextnetincome, TextToInsert);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateTransactionTextApportionedFunderTooltip(string ExpectedTooltip)
        {
            ValidateElementAttribute(Transactiontextapportionedfunder, "title", ExpectedTooltip);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateTransactionTextApportionedFunderText(string ExpectedText)
        {
            ValidateElementValue(Transactiontextapportionedfunder, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage InsertTextOnTransactionTextApportionedFunder(string TextToInsert)
        {
            WaitForElementToBeClickable(Transactiontextapportionedfunder);
            SendKeys(Transactiontextapportionedfunder, TextToInsert);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateTransactionTextAdjustmentText(string ExpectedText)
        {
            ValidateElementValue(Transactiontextadjustment, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage InsertTextOnTransactionTextAdjustment(string TextToInsert)
        {
            WaitForElementToBeClickable(Transactiontextadjustment);
            SendKeys(Transactiontextadjustment, TextToInsert);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateTransactionTextExpenseTooltip(string ExpectedTooltip)
        {
            ValidateElementAttribute(Transactiontextexpense, "title", ExpectedTooltip);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateTransactionTextExpenseText(string ExpectedText)
        {
            ValidateElementValue(Transactiontextexpense, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage InsertTextOnTransactionTextExpense(string TextToInsert)
        {
            WaitForElementToBeClickable(Transactiontextexpense);
            SendKeys(Transactiontextexpense, TextToInsert);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ClickExtractNameLink()
        {
            WaitForElementToBeClickable(CareproviderextractnameidLink);
            Click(CareproviderextractnameidLink);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateExtractNameLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CareproviderextractnameidLink);
            ValidateElementText(CareproviderextractnameidLink, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ClickExtractNameClearButton()
        {
            WaitForElementToBeClickable(CareproviderextractnameidClearButton);
            Click(CareproviderextractnameidClearButton);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ClickExtractNameLookupButton()
        {
            WaitForElementToBeClickable(CareproviderextractnameidLookupButton);
            Click(CareproviderextractnameidLookupButton);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ClickDebtorReferenceNumberRequired_YesRadioButton()
        {
            WaitForElementToBeClickable(Debtorreferencenumberrequired_1);
            Click(Debtorreferencenumberrequired_1);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateDebtorReferenceNumberRequired_YesRadioButtonChecked()
        {
            WaitForElement(Debtorreferencenumberrequired_1);
            ValidateElementChecked(Debtorreferencenumberrequired_1);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateDebtorReferenceNumberRequired_YesRadioButtonNotChecked()
        {
            WaitForElement(Debtorreferencenumberrequired_1);
            ValidateElementNotChecked(Debtorreferencenumberrequired_1);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ClickDebtorReferenceNumberRequired_NoRadioButton()
        {
            WaitForElementToBeClickable(Debtorreferencenumberrequired_0);
            Click(Debtorreferencenumberrequired_0);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateDebtorReferenceNumberRequired_NoRadioButtonChecked()
        {
            WaitForElement(Debtorreferencenumberrequired_0);
            ValidateElementChecked(Debtorreferencenumberrequired_0);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateDebtorReferenceNumberRequired_NoRadioButtonNotChecked()
        {
            WaitForElement(Debtorreferencenumberrequired_0);
            ValidateElementNotChecked(Debtorreferencenumberrequired_0);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ClickResponsibleTeamLink()
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            Click(ResponsibleTeamLink);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            ValidateElementText(ResponsibleTeamLink, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchSetupRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamLookupButton);
            Click(ResponsibleTeamLookupButton);

            return this;
        }

		public CPFinanceInvoiceBatchSetupRecordPage ClickFinanceInvoiceBatchesTab()
		{
            WaitForElementToBeClickable(FinanceInvoiceBatchesTab);
            Click(FinanceInvoiceBatchesTab);

            return this;
        }

		//Validate Finance Invoice Batches Tab is present
		public CPFinanceInvoiceBatchSetupRecordPage ValidateFinanceInvoiceBatchesTabPresent()
		{
            WaitForElementVisible(FinanceInvoiceBatchesTab);

            return this;
        }

		public CPFinanceInvoiceBatchSetupRecordPage ValidateFinanceInvoiceBatchesTabNotPresent()
		{
            WaitForElementNotVisible(FinanceInvoiceBatchesTab, 3);

            return this;
        }
	}
}

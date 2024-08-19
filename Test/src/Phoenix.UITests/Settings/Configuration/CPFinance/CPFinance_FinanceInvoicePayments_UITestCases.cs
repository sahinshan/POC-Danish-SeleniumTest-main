using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace Phoenix.UITests.Settings.Configuration.CPFinance
{
    /// <summary>
    /// This class contains Automated UI test scripts for CP Finance > Finance Invoice Payment Allocation
    /// </summary>
    [TestClass]
    public class CPFinance_FinanceInvoicePayments_UITestCases : FunctionalTest
    {
        #region Properties

        private string _environmentName;
        private string _tenantName;
        private Guid _authenticationProviderId;
        private Guid _languageId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private string _teamName;
        private Guid _defaultLoginUserID;
        public Guid environmentid;
        private string _loginUsername;
        private string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");

        #endregion

        [TestInitialize()]
        public void TestSetup()
        {
            try
            {
                #region Environment Name

                _environmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                _tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];

                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);

                #endregion

                #region Authentication

                _authenticationProviderId = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").First();

                #endregion Authentication

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CP_FIP_BU");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                _teamName = "CP_FIP_Team";
                _teamId = commonMethodsDB.CreateTeam(_teamName, null, _businessUnitId, "90455", "CareProvidersFIP@careworkstempmail.com", _teamName, "020 123456");

                #endregion

                #region Create default system user

                _loginUsername = "CP_FIPUser1";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUsername, "CP_FIP", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationProviderId);

                #endregion

                #region Accounting Periods

                var accountingPeriods = dbHelper.careProviderAccountingPeriod.GetAll();

                foreach (Guid accountPeriod in accountingPeriods)
                {

                    var payments = dbHelper.careProviderFinanceInvoicePayment.GetByAccountingPeriodId(accountPeriod);
                    foreach (Guid payment in payments)
                    {
                        dbHelper.careProviderFinanceInvoicePayment.DeleteCareProviderFinanceInvoicePayment(payment);
                    }
                    dbHelper.careProviderAccountingPeriod.DeleteCareProviderAccountingPeriod(accountPeriod);
                }


                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                this.ShutDownAllProcesses();

                throw;
            }
        }

        internal void ProcessCPFinanceScheduledJob(string ScheduledJobName)
        {
            #region Process CP Finance Invoice Batches

            System.Threading.Thread.Sleep(10000);

            //Get the schedule job id
            Guid processCpFinanceScheduledJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName(ScheduledJobName)[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(processCpFinanceScheduledJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the authentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(processCpFinanceScheduledJobId);

            #endregion
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-7407

        [TestProperty("JiraIssueID", "ACC-7453")]
        [Description("Test for Finance Invoice Payments - ACC-932. Step 1 to Step 10")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice")]
        [TestProperty("Screen2", "Finance Invoice Payments")]
        public void CPFinance_FinanceInvoicePayments_UITestMethod001()
        {

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Deco";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId1 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var careProviderServiceDetailName = "Care Provider Service Details Test 2";
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, code, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(careProviderServiceId1, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name, Extract Type

            var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);
            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type: Generic Extract File

            #endregion

            #region Finance Extract Batch Setup, Finance Extract Batch

            Guid regardingId = _teamId;
            string regardingIdTableName = "team";
            string regardingIdName = _teamName;

            var _cpFinanceExtractBatchSetupId1 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0), null, careProviderExtractNameId, 1, _cpGenericExtractTypeId, _invoiceGenerateTemplate_MailMergeId, regardingId, regardingIdTableName, regardingIdName);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateCareProviderFinanceExtractBatchSetupExtractOnDayFlagToYesByCurrentDayOfWeek(_cpFinanceExtractBatchSetupId1, new DateTime(2024, 1, 1));
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateGenerateAndSendInvoicesAutomaticallyFlag(_cpFinanceExtractBatchSetupId1, true);

            var _cpFinanceExtractBatchId = dbHelper
                                .careProviderFinanceExtractBatch
                                .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, careProviderExtractNameId);
            Assert.AreEqual(1, _cpFinanceExtractBatchId.Count);
            Guid _cpFinanceExtractBatch1Id = _cpFinanceExtractBatchId[0];
            int _cpFinanceExtractBatch1IdNumber1 = (int)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch1Id, "batchid")["batchid"];

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 5; //Every Calendar Month
            var createbatchwithin = 0;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2025, 12, 31);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2024, 5, 6), new TimeSpan(1, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Care Provider Person Contract

            DateTime personContractStartDate = new DateTime(2024, 1, 1);
            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, personContractStartDate, null, true);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                personContractStartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batch

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var _cpFinanceInvoiceBatchId1 = _careProviderFinanceInvoiceBatchId[0];

            var runOn1 = DateTime.Now.AddSeconds(5);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId1, runOn1, null, null);
            Thread.Sleep(5000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            var completedFinanceInvoiceBatchIds = dbHelper.careProviderFinanceInvoiceBatch.GetFinanceInvoiceBatchBySetupIDAndStatusID(_careProviderFinanceInvoiceBatchSetupId1, 2);
            Assert.AreEqual(1, completedFinanceInvoiceBatchIds.Count);
            Assert.AreEqual(_cpFinanceInvoiceBatchId1, completedFinanceInvoiceBatchIds[0]);

            #region Update invoice status to completed
            dbHelper.provider.UpdateDebtorNumber(funderProviderID, currentTimeString);

            //update invoice status to completed
            var _cpFinanceInvoiceId = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchIdAndInvoiceStatusId(_cpFinanceInvoiceBatchId1, 1)[0];
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId, 20);
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId, 2);

            #endregion

            #region Payment Methods

            var paymentMethodId = dbHelper.paymentMethod.GetByName("Cheque")[0];

            #endregion

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicesSection();

            Assert.AreEqual(2, dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupID(_careProviderFinanceInvoiceBatchSetupId1).Count);

            Guid _cpFinanceInvoiceId1 = _cpFinanceInvoiceId;
            Assert.AreEqual(1, dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchIdAndInvoiceStatusId(_cpFinanceInvoiceBatchId1, 2).Count);
            var _cpFinanceInvoiceId1_Title = (string)(dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceById(_cpFinanceInvoiceId1, "name")["name"]);
            var _cpFinanceInvoiceId1_Id = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceById(_cpFinanceInvoiceId1, "invoicenumber")["invoicenumber"];

            cpFinanceInvoicesPage
                .WaitForCPFinanceInvoicesPageToLoad()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceInvoicesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceId1);

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad(_cpFinanceInvoiceId1.ToString())
                .ValidatePaymentsTabVisible(false);

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad(_cpFinanceInvoiceId1.ToString())
                .ValidatePaymentSubMenuVisible(false);

            #region Schdeduled job for "Process CP Finance Extract Batches"

            ProcessCPFinanceScheduledJob("Process CP Finance Extract Batches");

            #endregion

            #region Care Provider Finance Extract Batch

            var batchstatusid = 2; //completed
            var allFinanceExtractBatches = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, careProviderExtractNameId, batchstatusid);
            Assert.AreEqual(1, allFinanceExtractBatches.Count);

            #endregion

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad(_cpFinanceInvoiceId1.ToString())
                .ClickBackButton();

            cpFinanceInvoicesPage
                .WaitForCPFinanceInvoicesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceId1);

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad(_cpFinanceInvoiceId1.ToString())
                .ValidatePaymentsTabVisible(true);

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad(_cpFinanceInvoiceId1.ToString())
                .ValidatePaymentSubMenuVisible(true);

            #endregion

            #region Step 2, 4

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad(_cpFinanceInvoiceId1.ToString())
                .ClickPaymentsTab();

            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad(null, false)
                .RecordsPageValidateHeaderLinkText(2, "Alternative Invoice Number")
                .RecordsPageValidateHeaderLinkText(3, "Payment Date")
                .RecordsPageValidateHeaderLinkText(4, "Posted Date")
                .RecordsPageValidateHeaderLinkText(5, "Payment Amount")
                .RecordsPageValidateHeaderLinkText(6, "Debt Written Off")
                .RecordsPageValidateHeaderLinkText(7, "Payment Method")
                .RecordsPageValidateHeaderLinkText(8, "Paid By")
                .RecordsPageValidateHeaderLinkText(9, "Reference")
                .RecordsPageValidateHeaderLinkText(10, "Total Record?")
                .RecordsPageValidateHeaderLinkText(11, "Allocated?")
                .RecordsPageValidateHeaderLinkText(12, "Finance Invoice")
                .ValidateSelectedSystemView("Related Records");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicePaymentsPage();

            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad()
                .RecordsPageValidateHeaderLinkText(2, "Invoice Number")
                .RecordsPageValidateHeaderLinkText(3, "Alternative Invoice Number")
                .RecordsPageValidateHeaderLinkText(4, "Payment Date")
                .RecordsPageValidateHeaderLinkText(5, "Posted Date")
                .RecordsPageValidateHeaderLinkText(6, "Payment Amount")
                .RecordsPageValidateHeaderLinkText(7, "Debt Written Off")
                .RecordsPageValidateHeaderLinkText(8, "Payment Method")
                .RecordsPageValidateHeaderLinkText(9, "Paid By")
                .RecordsPageValidateHeaderLinkText(10, "Reference")
                .RecordsPageValidateHeaderLinkText(11, "Total Record?")
                .RecordsPageValidateHeaderLinkText(12, "Allocated?")
                .RecordsPageValidateHeaderLinkText(13, "Finance Invoice")
                .RecordsPageValidateHeaderLinkText(14, "Modified On")
                .RecordsPageValidateHeaderLinkText(15, "Modified By")
                .ClickNewRecordButton();

            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad()
                .ValidateCareproviderFinanceInvoiceLookupButtonDisplayed(true)
                .ValidateEstablishmentLookupButtonDisplayed(true)
                .ValidateInvoiceNumberDisplayed(true)
                .ValidatePaymentDateDisplayed(true)
                .ValidatePaymentAmountDisplayed(true)
                .ValidatePaymentMethodLookupButtonDisplayed(true)
                .ValidateTotalRecordOptionsDisplayed(true)
                .ValidateReferenceFieldIsDisplayed(true);

            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad()
                .ValidateResponsibleTeamLookupButtonDisplayed(true)
                .ValidatePaymentMethodLookupButtonDisplayed(true)
                .ValidateAlternativeInvoiceNumberDisplayed(true)
                .ValidatePostedDateDisplayed(true)
                .ValidateDebtWrittenOffDisplayed(true)
                .ValidatePaidByDisplayed(true)
                .ValidateAllocatedOptionsDisplayed(true);

            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad()
                .ValidateMandatoryFieldIsDisplayed("Finance Invoice", false)
                .ValidateMandatoryFieldIsDisplayed("Establishment", true)
                .ValidateMandatoryFieldIsDisplayed("Invoice Number", false)
                .ValidateMandatoryFieldIsDisplayed("Payment Date", true)
                .ValidateMandatoryFieldIsDisplayed("Payment Amount", true)
                .ValidateMandatoryFieldIsDisplayed("Payment Method", true)
                .ValidateMandatoryFieldIsDisplayed("Total Record?", true)
                .ValidateMandatoryFieldIsDisplayed("Reference", false);

            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad()
                .ValidateMandatoryFieldIsDisplayed("Responsible Team", true)
                .ValidateMandatoryFieldIsDisplayed("Payer", false)
                .ValidateMandatoryFieldIsDisplayed("Alternative Invoice Number", false)
                .ValidateMandatoryFieldIsDisplayed("Posted Date", false)
                .ValidateMandatoryFieldIsDisplayed("Debt Written Off", false)
                .ValidateMandatoryFieldIsDisplayed("Paid By", false)
                .ValidateMandatoryFieldIsDisplayed("Allocated?", false);

            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad()
                .ValidatePaymentAmountAttributeValue("range", "-999999.9900,999999.9900")
                .ValidateTotalrecord_YesOptionChecked()
                .ValidateTotalrecord_NoOptionNotChecked()
                .ValidateDebtWrittenOffText("0.00")
                .ValidateAllocated_NoOptionChecked()
                .ValidateAllocated_YesOptionNotChecked()
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad();

            #endregion

            #region Step 7, 6, 3

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicesSection();

            cpFinanceInvoicesPage
                .WaitForCPFinanceInvoicesPageToLoad()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceInvoicesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceId1);

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad(_cpFinanceInvoiceId1.ToString())
                .ValidatePaymentsMadeFieldText("0.00")
                .ValidateDebtWrittenOffFieldText("")
                .NavigateToPaymentSubMenu();

            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad(_cpFinanceInvoiceId, true)
                .ClickNewRecordButton();

            //var _newCpFinanceInvoiceId_Id = int.Parse(_cpFinanceInvoiceId1_Id.ToString()) + 1;

            //Step 7
            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad()
                .ValidateCareproviderFinanceInvoiceLinkText(_cpFinanceInvoiceId1_Title)
                .ValidateInvoiceNumberText(_cpFinanceInvoiceId1_Id.ToString())
                .ValidateResponsibleTeamLinkText(_teamName);

            //Step 6
            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad()
                .ValidateCareProviderFinanceInvoiceLookupButtonDisabled(true)
                .ValidateInvoiceNumberDisabled(true)
                .ValidateTotalRecordFieldsDisabled(true)
                .ValidateAllocatedFieldsDisabled(true)
                .ValidateResponsibleTeamLookupButtonDisabled(false);

            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnPaymentDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnPaymentAmount("5")
                .InsertTextOnDebtWrittenOff("1")
                .ClickPaymentMethodLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Cheque")
                .TapSearchButton()
                .SelectResultElement(paymentMethodId);

            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnAlternativeInvoiceNumber("N_" + currentTimeString)
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            //Step 7
            cpFinanceInvoicePaymentRecordPage
                .ValidateCareproviderFinanceInvoiceLinkText(_cpFinanceInvoiceId1_Title)
                .ValidateEstablishmentLinkText(providerName)
                .ValidatePayerLinkText(funderProviderName)
                .ValidateInvoiceNumberText(_cpFinanceInvoiceId1_Id.ToString())
                .ValidateAlternativeInvoiceNumberText("N_" + currentTimeString)
                .ValidateDebtWrittenOffText("1.00")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidatePaymentDateText(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidatePaymentMethodLinkText("Cheque")
                .ValidatePaymentAmountText("5.00")
                .ValidateTotalrecord_NoOptionChecked()
                .ValidateTotalrecord_YesOptionNotChecked()
                .ValidateAllocated_NoOptionChecked()
                .ValidateAllocated_YesOptionNotChecked();

            //Step 6
            cpFinanceInvoicePaymentRecordPage
                .ValidateCareProviderFinanceInvoiceLookupButtonDisabled(true)
                .ValidateInvoiceNumberDisabled(true)
                .ValidateTotalRecordFieldsDisabled(true)
                .ValidateAllocatedFieldsDisabled(true)
                .ValidateResponsibleTeamLookupButtonDisabled(true);

            var _cpFinanceInvoicePaymentId = dbHelper.careProviderFinanceInvoicePayment.GetByInvoiceId(_cpFinanceInvoiceId1)[0];

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicePaymentsPage();

            //Step 3
            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad()
                .ValidateSelectedSystemView("Active Records")
                .ValidateRecordIsPresent(_cpFinanceInvoicePaymentId);

            #endregion

            #region Step 9, 10

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicesSection();

            cpFinanceInvoicesPage
                .WaitForCPFinanceInvoicesPageToLoad()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceInvoicesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceId1);

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad(_cpFinanceInvoiceId1.ToString())
                .ValidatePaymentsMadeFieldText("5.00")
                .ValidateDebtWrittenOffFieldText("1.00");

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad(_cpFinanceInvoiceId1.ToString())
                .ClickPaymentsTab();

            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad(_cpFinanceInvoiceId, true)
                .OpenRecord(_cpFinanceInvoicePaymentId);

            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnPaymentAmount("3")
                .InsertTextOnDebtWrittenOff("2")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForPageToLoad();

            System.Threading.Thread.Sleep(1000);

            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad()
                .ValidatePaymentAmountText("3.00")
                .ValidateDebtWrittenOffText("2.00")
                .ClickBackButton();

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad(_cpFinanceInvoiceId1.ToString())
                .ClickBackButton();

            cpFinanceInvoicesPage
                .WaitForCPFinanceInvoicesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceId1);

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad(_cpFinanceInvoiceId1.ToString())
                .ValidatePaymentsMadeFieldText("3.00")
                .ValidateDebtWrittenOffFieldText("2.00");

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad(_cpFinanceInvoiceId1.ToString())
                .ClickPaymentsTab();

            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad(_cpFinanceInvoiceId, true)
                .SelectFinanceInvoiceRecord(_cpFinanceInvoicePaymentId.ToString())
                .ClickDeleteRecordButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad(_cpFinanceInvoiceId, true)
                .ValidateRecordIsPresent(_cpFinanceInvoicePaymentId.ToString(), false);

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad(_cpFinanceInvoiceId1.ToString())
                .ClickBackButton();

            cpFinanceInvoicesPage
                .WaitForCPFinanceInvoicesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceId1);

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad(_cpFinanceInvoiceId1.ToString())
                .ValidatePaymentsMadeFieldText("0.00")
                .ValidateDebtWrittenOffFieldText("0.00");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-7408

        [TestProperty("JiraIssueID", "ACC-7457")]
        [Description("Test for Finance Invoice Payments - ACC-932. Step 5 and Step 8")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice")]
        [TestProperty("Screen2", "Finance Invoice Payments")]
        public void CPFinance_FinanceInvoicePayments_UITestMethod002()
        {

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Deco";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId1 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var careProviderServiceDetailName = "Care Provider Service Details Test 2";
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, code, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(careProviderServiceId1, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name, Extract Type

            var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);
            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type: Generic Extract File

            #endregion

            #region Finance Extract Batch Setup, Finance Extract Batch

            Guid regardingId = _teamId;
            string regardingIdTableName = "team";
            string regardingIdName = _teamName;

            var _cpFinanceExtractBatchSetupId1 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0), null, careProviderExtractNameId, 1, _cpGenericExtractTypeId, _invoiceGenerateTemplate_MailMergeId, regardingId, regardingIdTableName, regardingIdName);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateCareProviderFinanceExtractBatchSetupExtractOnDayFlagToYesByCurrentDayOfWeek(_cpFinanceExtractBatchSetupId1, new DateTime(2024, 1, 1));
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateGenerateAndSendInvoicesAutomaticallyFlag(_cpFinanceExtractBatchSetupId1, true);

            var _cpFinanceExtractBatchId = dbHelper
                                .careProviderFinanceExtractBatch
                                .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, careProviderExtractNameId);
            Assert.AreEqual(1, _cpFinanceExtractBatchId.Count);
            Guid _cpFinanceExtractBatch1Id = _cpFinanceExtractBatchId[0];
            int _cpFinanceExtractBatch1IdNumber1 = (int)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch1Id, "batchid")["batchid"];

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 5; //Every Calendar Month
            var createbatchwithin = 0;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2025, 12, 31);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2024, 5, 6), new TimeSpan(1, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Care Provider Person Contract

            DateTime personContractStartDate = new DateTime(2024, 1, 1);
            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, personContractStartDate, null, true);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                personContractStartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batch

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var _cpFinanceInvoiceBatchId1 = _careProviderFinanceInvoiceBatchId[0];

            var runOn1 = DateTime.Now.AddSeconds(5);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId1, runOn1, null, null);
            Thread.Sleep(5000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            var completedFinanceInvoiceBatchIds = dbHelper.careProviderFinanceInvoiceBatch.GetFinanceInvoiceBatchBySetupIDAndStatusID(_careProviderFinanceInvoiceBatchSetupId1, 2);
            Assert.AreEqual(1, completedFinanceInvoiceBatchIds.Count);
            Assert.AreEqual(_cpFinanceInvoiceBatchId1, completedFinanceInvoiceBatchIds[0]);

            #endregion

            #region Update invoice status to completed
            dbHelper.provider.UpdateDebtorNumber(funderProviderID, currentTimeString);

            //update invoice status to completed
            var _cpFinanceInvoiceId = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchIdAndInvoiceStatusId(_cpFinanceInvoiceBatchId1, 1)[0];
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId, 2);

            #endregion

            #region Payment Methods

            var paymentMethodId = dbHelper.paymentMethod.GetByName("Cheque")[0];

            #endregion

            #region Schdeduled job for "Process CP Finance Extract Batches"

            ProcessCPFinanceScheduledJob("Process CP Finance Extract Batches");

            #endregion

            #region Process Care Provider Finance Extract Batch

            var batchstatusid = 2; //completed
            var allFinanceExtractBatches = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, careProviderExtractNameId, batchstatusid);
            Assert.AreEqual(1, allFinanceExtractBatches.Count);

            #endregion

            #region Finance Invoice Payments

            var _cpFinanceInvoicePaymentId1 = dbHelper.careProviderFinanceInvoicePayment.CreateCareProviderFinanceInvoicePayment(_teamId,
                _cpFinanceInvoiceId, providerId, funderProviderID, "provider", providerName, "A_" + currentTimeString,
                commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(3)), null, 1m, 0m, paymentMethodId, "", false, false, "Ref7407_2_" + currentTimeString, null);

            System.Threading.Thread.Sleep(1000);

            var _cpFinanceInvoicePaymentId2 = dbHelper.careProviderFinanceInvoicePayment.CreateCareProviderFinanceInvoicePayment(_teamId,
                               _cpFinanceInvoiceId, providerId, funderProviderID, "provider", providerName, "B_" + currentTimeString,
                               commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(1)), null, 2m, 1m, paymentMethodId, "", false, false, "Ref_3_7407_" + currentTimeString, null);

            var _cpFinanceInvoicePaymentId3 = dbHelper.careProviderFinanceInvoicePayment.CreateCareProviderFinanceInvoicePayment(_teamId,
                                              _cpFinanceInvoiceId, providerId, funderProviderID, "provider", providerName, "C_" + currentTimeString,
                                              commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(2)), null, 5m, 3m, paymentMethodId, "", false, false, "Test_" + currentTimeString, null);

            #endregion

            #region Step 5, Step 8

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicesSection();

            Assert.AreEqual(2, dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupID(_careProviderFinanceInvoiceBatchSetupId1).Count);

            Guid _cpFinanceInvoiceId1 = _cpFinanceInvoiceId;
            Assert.AreEqual(1, dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchIdAndInvoiceStatusId(_cpFinanceInvoiceBatchId1, 3).Count); //Extracted

            cpFinanceInvoicesPage
                .WaitForCPFinanceInvoicesPageToLoad()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceInvoicesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceId1);

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad(_cpFinanceInvoiceId1.ToString())
                .ValidatePaymentsMadeFieldText("8.00")
                .ValidateDebtWrittenOffFieldText("4.00")
                .ValidateBalanceOutstandingFieldText("32.57")
                .ValidateGrossAmountFieldText("44.57")
                .ClickPaymentsTab();

            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad(null, false)
                .RecordsPageValidateHeaderSortDescIconVisible(3) //Payment Date
                .ValidateRecordIsPresent(1, _cpFinanceInvoicePaymentId1)
                .ValidateRecordIsPresent(3, _cpFinanceInvoicePaymentId2)
                .ValidateRecordIsPresent(2, _cpFinanceInvoicePaymentId3);

            var allPayments = dbHelper.careProviderFinanceInvoicePayment.GetAll();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicePaymentsPage();

            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad()
                .RecordsPageValidateHeaderSortDescIconVisible(4) //Payment Date
                .ValidateRecordIsPresent(1, allPayments[0])
                .ValidateRecordIsPresent(2, allPayments[1])
                .ValidateRecordIsPresent(3, allPayments[2]);

            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad()
                .InsertTextOnQuickSearch("B_" + currentTimeString)
                .ClickQuickSearchButton()
                .WaitForPageToLoad()
                .ValidateRecordIsPresent(_cpFinanceInvoicePaymentId2);

            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad()
                .InsertTextOnQuickSearch("Ref7407_2_" + currentTimeString)
                .ClickQuickSearchButton()
                .WaitForPageToLoad()
                .ValidateRecordIsPresent(_cpFinanceInvoicePaymentId1);

            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad()
                .InsertTextOnQuickSearch("*" + currentTimeString)
                .ClickQuickSearchButton()
                .WaitForPageToLoad()
                .ValidateRecordIsPresent(_cpFinanceInvoicePaymentId1)
                .ValidateRecordIsPresent(_cpFinanceInvoicePaymentId2)
                .ValidateRecordIsPresent(_cpFinanceInvoicePaymentId3);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            //Wait for Advanced Search Page to Load, select Record Type, Select Filter Type and value
            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Finance Invoice Payments")
                .SelectFilter("1", "Accounting Period")
                .SelectFilter("1", "Allocated?")
                .SelectFilter("1", "Alternative Invoice Number")
                .SelectFilter("1", "Data Extracted?")
                .SelectFilter("1", "Debt Written Off")
                .SelectFilter("1", "Establishment")
                .SelectFilter("1", "Finance Invoice")
                .SelectFilter("1", "Finance Invoice Payment")
                .SelectFilter("1", "Finance Invoice Payment (allocated to)")
                .SelectFilter("1", "Invoice Number")
                .SelectFilter("1", "Paid By")
                .SelectFilter("1", "Payer")
                .SelectFilter("1", "Payment Amount")
                .SelectFilter("1", "Payment Date")
                .SelectFilter("1", "Payment Method")
                .SelectFilter("1", "Posted Date")
                .SelectFilter("1", "Reference")
                .SelectFilter("1", "Responsible Team")
                .SelectFilter("1", "Total Record?")
                .SelectFilter("1", "Payment Date")
                .SelectOperator("1", "Equals")
                .SelectValueType("1", "Value")
                .InsertRuleValueText("1", commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(3)).ToString("dd'/'MM'/'yyyy"))
                .ClickSearchButton()
                .WaitForResultsPageToLoad();

            advanceSearchPage
                .ValidateSearchResultRecordPresent(_cpFinanceInvoicePaymentId1.ToString())
                .ClickBackButton_ResultsPage();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Finance Invoice Payments")
                .SelectFilter("1", "Reference")
                .SelectOperator("1", "Equals")
                .InsertRuleValueText("1", "Test_" + currentTimeString)
                .ClickSearchButton()
                .WaitForResultsPageToLoad();

            advanceSearchPage
                .ValidateSearchResultRecordPresent(_cpFinanceInvoicePaymentId3.ToString())
                .ClickBackButton_ResultsPage();

            dbHelper.careProviderFinanceInvoicePayment.DeleteCareProviderFinanceInvoicePayment(_cpFinanceInvoicePaymentId1);
            dbHelper.careProviderFinanceInvoicePayment.DeleteCareProviderFinanceInvoicePayment(_cpFinanceInvoicePaymentId2);
            dbHelper.careProviderFinanceInvoicePayment.DeleteCareProviderFinanceInvoicePayment(_cpFinanceInvoicePaymentId3);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicesSection();

            cpFinanceInvoicesPage
                .WaitForCPFinanceInvoicesPageToLoad()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceInvoicesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceId1);

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad(_cpFinanceInvoiceId1.ToString())
                .ValidatePaymentsMadeFieldText("0.00")
                .ValidateDebtWrittenOffFieldText("0.00")
                .ValidateBalanceOutstandingFieldText("44.57")
                .ValidateGrossAmountFieldText("44.57");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7468")]
        [Description("Test for Finance Invoice Payments - ACC-932. Step 11 - 14")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice")]
        [TestProperty("Screen2", "Finance Invoice Payments")]
        public void CPFinance_FinanceInvoicePayments_UITestMethod003()
        {

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Deco";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId1 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var careProviderServiceDetailName = "Care Provider Service Details Test 2";
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, code, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(careProviderServiceId1, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name, Extract Type

            var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);
            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type: Generic Extract File

            #endregion

            #region Finance Extract Batch Setup, Finance Extract Batch

            Guid regardingId = _teamId;
            string regardingIdTableName = "team";
            string regardingIdName = _teamName;

            var _cpFinanceExtractBatchSetupId1 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0), null, careProviderExtractNameId, 1, _cpGenericExtractTypeId, _invoiceGenerateTemplate_MailMergeId, regardingId, regardingIdTableName, regardingIdName);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateCareProviderFinanceExtractBatchSetupExtractOnDayFlagToYesByCurrentDayOfWeek(_cpFinanceExtractBatchSetupId1, new DateTime(2024, 1, 1));
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateGenerateAndSendInvoicesAutomaticallyFlag(_cpFinanceExtractBatchSetupId1, true);

            var _cpFinanceExtractBatchId = dbHelper
                                .careProviderFinanceExtractBatch
                                .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, careProviderExtractNameId);
            Assert.AreEqual(1, _cpFinanceExtractBatchId.Count);
            Guid _cpFinanceExtractBatch1Id = _cpFinanceExtractBatchId[0];
            int _cpFinanceExtractBatch1IdNumber1 = (int)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch1Id, "batchid")["batchid"];

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 5; //Every Calendar Month
            var createbatchwithin = 0;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2025, 12, 31);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2024, 5, 6), new TimeSpan(1, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Care Provider Person Contract

            DateTime personContractStartDate = new DateTime(2024, 1, 1);
            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, personContractStartDate, null, true);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                personContractStartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batch

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var _cpFinanceInvoiceBatchId1 = _careProviderFinanceInvoiceBatchId[0];

            var runOn1 = DateTime.Now.AddSeconds(5);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId1, runOn1, null, null);
            Thread.Sleep(5000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            var completedFinanceInvoiceBatchIds = dbHelper.careProviderFinanceInvoiceBatch.GetFinanceInvoiceBatchBySetupIDAndStatusID(_careProviderFinanceInvoiceBatchSetupId1, 2);
            Assert.AreEqual(1, completedFinanceInvoiceBatchIds.Count);
            Assert.AreEqual(_cpFinanceInvoiceBatchId1, completedFinanceInvoiceBatchIds[0]);

            #endregion

            #region Update invoice status to completed
            dbHelper.provider.UpdateDebtorNumber(funderProviderID, currentTimeString);

            //update invoice status to completed
            var _cpFinanceInvoiceId = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchIdAndInvoiceStatusId(_cpFinanceInvoiceBatchId1, 1)[0];
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId, 20);
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId, 2);

            #endregion

            #region Payment Methods

            var paymentMethodId = dbHelper.paymentMethod.GetByName("Cheque")[0];

            #endregion

            #region Schdeduled job for "Process CP Finance Extract Batches"

            ProcessCPFinanceScheduledJob("Process CP Finance Extract Batches");

            #endregion

            #region Process Care Provider Finance Extract Batch

            var batchstatusid = 2; //completed
            var allFinanceExtractBatches = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, careProviderExtractNameId, batchstatusid);
            Assert.AreEqual(1, allFinanceExtractBatches.Count);

            #endregion

            #region Finance Invoice Payments

            var _cpFinanceInvoicePaymentId = dbHelper.careProviderFinanceInvoicePayment.CreateCareProviderFinanceInvoicePayment(_teamId,
                _cpFinanceInvoiceId, providerId, funderProviderID, "provider", providerName, "ACC-7468" + currentTimeString,
                commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(3)), null, 1m, 0m, paymentMethodId, "", false, false, "ExceedTest7468" + currentTimeString, null);

            #endregion

            #region Step 13

            try
            {
                loginPage
                    .GoToLoginPage()
                    .Login(_loginUsername, "Passw0rd_!", _environmentName);

                mainMenu
                    .WaitForMainMenuToLoad()
                    .NavigateToFinanceInvoicePaymentsPage();

                cpFinanceInvoicePaymentsPage
                    .WaitForPageToLoad()
                    .InsertTextOnQuickSearch("ExceedTest7468" + currentTimeString)
                    .ClickQuickSearchButton()
                    .WaitForPageToLoad()
                    .OpenRecord(_cpFinanceInvoicePaymentId);

                cpFinanceInvoicePaymentRecordPage
                    .WaitForPageToLoad()
                    .VerifyAttachmentsSubMenuItemIsDisplayed()
                    .VerifyTasksSubMenuItemIsDisplayed();

                #endregion

                #region Step 11

                cpFinanceInvoicePaymentRecordPage
                    .WaitForPageToLoad()
                    .InsertTextOnPaymentAmount("45")
                    .ClickSaveButton();

                confirmDynamicDialogPopup
                    .WaitForConfirmDynamicDialogPopupToLoad()
                    .ValidateMessage("This change will lead to the Finance Invoice being overpaid. Do you wish to continue?")
                    .TapCancelButton();

                #endregion

                #region Step 14

                cpFinanceInvoicePaymentRecordPage
                    .WaitForPageToLoad()
                    .InsertTextOnPaymentAmount("1")
                    .InsertTextOnDebtWrittenOff("45")
                    .ClickSaveButton();

                dynamicDialogPopup
                    .WaitForDynamicDialogPopupToLoad()
                    .ValidateMessage("You are not permitted to record a debt written off amount which is greater than the value of the Finance Invoice. Please correct as necessary.")
                    .TapCloseButton();

                #endregion

                #region Step 12

                cpFinanceInvoicePaymentRecordPage
                    .WaitForPageToLoad()
                    .InsertTextOnPaymentAmount("50")
                    .InsertTextOnDebtWrittenOff("0")
                    .ClickSaveButton();

                confirmDynamicDialogPopup
                    .WaitForConfirmDynamicDialogPopupToLoad()
                    .ValidateMessage("This change will lead to the Finance Invoice being overpaid. Do you wish to continue?")
                    .TapOKButton();

                cpFinanceInvoicePaymentRecordPage
                    .WaitForPageToLoad()
                    .WaitForRecordToBeSaved();

                cpFinanceInvoicePaymentRecordPage
                    .WaitForPageToLoad()
                    .ValidatePaymentAmountText("50.00");

                mainMenu
                    .WaitForMainMenuToLoad()
                    .NavigateToFinanceInvoicesSection();

                Guid _cpFinanceInvoiceId1 = _cpFinanceInvoiceId;

                cpFinanceInvoicesPage
                    .WaitForCPFinanceInvoicesPageToLoad()
                    .InsertSearchQuery("*" + currentTimeString)
                    .ClickSearchButton()
                    .WaitForCPFinanceInvoicesPageToLoad()
                    .OpenRecord(_cpFinanceInvoiceId1);

                financeInvoiceRecordPage
                    .WaitForFinanceInvoiceRecordPageToLoad(_cpFinanceInvoiceId1.ToString())
                    .ValidatePaymentsMadeFieldText("50.00")
                    .ValidateDebtWrittenOffFieldText("0.00")
                    .ValidateBalanceOutstandingFieldText("-5.43")
                    .ValidateGrossAmountFieldText("44.57");
            }

            finally
            {
                dbHelper.careProviderFinanceInvoicePayment.DeleteCareProviderFinanceInvoicePayment(_cpFinanceInvoicePaymentId);
            }

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-7409

        [TestProperty("JiraIssueID", "ACC-7481")]
        [Description("Test for Finance Invoice Payments - ACC-932. Step 15 - 16")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice")]
        [TestProperty("Screen2", "Finance Invoice Payments")]
        public void CPFinance_FinanceInvoicePayments_UITestMethod004()
        {

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Diogo";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId1 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var careProviderServiceDetailName = "Care Provider Service Details Test 2";
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, code, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(careProviderServiceId1, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name, Extract Type

            var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);
            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type: Generic Extract File

            #endregion

            #region Finance Extract Batch Setup, Finance Extract Batch

            Guid regardingId = _teamId;
            string regardingIdTableName = "team";
            string regardingIdName = _teamName;

            var _cpFinanceExtractBatchSetupId1 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0), null, careProviderExtractNameId, 1, _cpGenericExtractTypeId, _invoiceGenerateTemplate_MailMergeId, regardingId, regardingIdTableName, regardingIdName);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateCareProviderFinanceExtractBatchSetupExtractOnDayFlagToYesByCurrentDayOfWeek(_cpFinanceExtractBatchSetupId1, new DateTime(2024, 1, 1));
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateGenerateAndSendInvoicesAutomaticallyFlag(_cpFinanceExtractBatchSetupId1, true);

            var _cpFinanceExtractBatchId = dbHelper
                                .careProviderFinanceExtractBatch
                                .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, careProviderExtractNameId);
            Assert.AreEqual(1, _cpFinanceExtractBatchId.Count);
            Guid _cpFinanceExtractBatch1Id = _cpFinanceExtractBatchId[0];
            int _cpFinanceExtractBatch1IdNumber1 = (int)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch1Id, "batchid")["batchid"];

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 5; //Every Calendar Month
            var createbatchwithin = 0;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2025, 12, 31);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2024, 5, 6), new TimeSpan(1, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Care Provider Person Contract

            DateTime personContractStartDate = new DateTime(2024, 1, 1);
            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, personContractStartDate, null, true);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                personContractStartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batch

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var _cpFinanceInvoiceBatchId1 = _careProviderFinanceInvoiceBatchId[0];

            var runOn1 = DateTime.Now.AddSeconds(5);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId1, runOn1, null, null);
            Thread.Sleep(5000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            var completedFinanceInvoiceBatchIds = dbHelper.careProviderFinanceInvoiceBatch.GetFinanceInvoiceBatchBySetupIDAndStatusID(_careProviderFinanceInvoiceBatchSetupId1, 2);
            Assert.AreEqual(1, completedFinanceInvoiceBatchIds.Count);
            Assert.AreEqual(_cpFinanceInvoiceBatchId1, completedFinanceInvoiceBatchIds[0]);

            #endregion

            #region Update invoice status to completed
            dbHelper.provider.UpdateDebtorNumber(funderProviderID, currentTimeString);

            //update invoice status to completed
            var _cpFinanceInvoiceId = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchIdAndInvoiceStatusId(_cpFinanceInvoiceBatchId1, 1)[0];
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId, 20);
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId, 2);

            #endregion

            #region Payment Methods

            var paymentMethodId = dbHelper.paymentMethod.GetByName("Cheque")[0];

            #endregion

            #region Schdeduled job for "Process CP Finance Extract Batches"

            ProcessCPFinanceScheduledJob("Process CP Finance Extract Batches");

            #endregion

            #region Process Care Provider Finance Extract Batch

            var batchstatusid = 2; //completed
            var allFinanceExtractBatches = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, careProviderExtractNameId, batchstatusid);
            Assert.AreEqual(1, allFinanceExtractBatches.Count);

            #endregion

            #region Accounting Periods

            DateTime periodFrom1 = new DateTime(DateTime.Now.AddYears(-2).Year, 1, 1);
            DateTime periodTo1 = new DateTime(DateTime.Now.AddYears(-2).Year, 12, 31);

            DateTime periodFrom2 = new DateTime(DateTime.Now.AddYears(-1).Year, 1, 1);
            DateTime periodTo2 = new DateTime(DateTime.Now.AddYears(-1).Year, 12, 31);

            var accountingPeriodIdExists = dbHelper.careProviderAccountingPeriod.GetByPeriodFromAndPeriodToDateAndCurrentPeriod(periodFrom1, periodTo1, true, false).Any();
            if (accountingPeriodIdExists.Equals(false))
            {
                dbHelper.careProviderAccountingPeriod.CreateCareProviderAccountingPeriod(_teamId, periodFrom1, periodTo1, null, true, false, false);
            }
            var accountingPeriodId1 = dbHelper.careProviderAccountingPeriod.GetByPeriodFromAndPeriodToDateAndCurrentPeriod(periodFrom1, periodTo1, true, false)[0];

            accountingPeriodIdExists = dbHelper.careProviderAccountingPeriod.GetByPeriodFromAndPeriodToDateAndCurrentPeriod(periodFrom2, periodTo2, true, false).Any();
            if (accountingPeriodIdExists.Equals(false))
            {
                dbHelper.careProviderAccountingPeriod.CreateCareProviderAccountingPeriod(_teamId, periodFrom2, periodTo2, null, true, false, false);
            }
            var accountingPeriodId2 = dbHelper.careProviderAccountingPeriod.GetByPeriodFromAndPeriodToDateAndCurrentPeriod(periodFrom2, periodTo2, true, false)[0];

            #endregion

            #region Step 15

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicesSection();

            cpFinanceInvoicesPage
                .WaitForCPFinanceInvoicesPageToLoad()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceInvoicesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceId);

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad(_cpFinanceInvoiceId.ToString())
                .ClickPaymentsTab();

            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad(_cpFinanceInvoiceId, true)
                .ClickNewRecordButton();

            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnPaymentDate(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnPaymentAmount("10")
                .ClickPaymentMethodLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Cheque", paymentMethodId);

            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnPostedDate(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("You are not permitted to record a Posted Date that is later than today. Please correct as necessary.")
                .TapCloseButton();

            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnPostedDate(periodTo1.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("You are not permitted to record a Posted Date that has a date outside of the range recorded on the current period’s Accounting Period record. Please correct as necessary.")
                .TapCloseButton();

            #endregion

            #region Step 16

            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnPaymentDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnPostedDate(DateTime.Now.AddYears(-1).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton();

            cpFinanceInvoicePaymentRecordPage
                .WaitForRecordToBeSaved()
                .WaitForPageToLoad()
                .ValidatePaymentAmountText("10.00")
                .ValidatePaymentDateText(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidatePostedDateText(DateTime.Now.AddYears(-1).ToString("dd'/'MM'/'yyyy"));

            cpFinanceInvoicePaymentRecordPage
                .InsertTextOnPaymentAmount("20")
                .InsertTextOnPostedDate(DateTime.Now.AddYears(-1).AddDays(-1).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton();

            cpFinanceInvoicePaymentRecordPage
                .WaitForRecordToBeSaved()
                .WaitForPageToLoad()
                .ValidatePaymentAmountText("20.00")
                .ValidatePostedDateText(DateTime.Now.AddYears(-1).AddDays(-1).ToString("dd'/'MM'/'yyyy"));

            var paymentId = dbHelper.careProviderFinanceInvoicePayment.GetByEstablishmentId(providerId)[0];

            cpFinanceInvoicePaymentRecordPage
                .ClickBackButton();

            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad(_cpFinanceInvoiceId, true)
                .SelectFinanceInvoiceRecord(paymentId.ToString())
                .ClickDeleteRecordButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad(_cpFinanceInvoiceId, true)
                .ValidateRecordIsPresent(paymentId.ToString(), false);


            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-7482")]
        [Description("Test for Finance Invoice Payments - ACC-932. Step 17")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice")]
        [TestProperty("Screen2", "Finance Invoice Payments")]
        public void CPFinance_FinanceInvoicePayments_UITestMethod005()
        {

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Diogo";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority            

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId1 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var careProviderServiceDetailName = "Care Provider Service Details Test 2";
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, code, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(careProviderServiceId1, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name, Extract Type

            var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);
            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type: Generic Extract File

            #endregion

            #region Finance Extract Batch Setup, Finance Extract Batch

            Guid regardingId = _teamId;
            string regardingIdTableName = "team";
            string regardingIdName = _teamName;

            var _cpFinanceExtractBatchSetupId1 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0), null, careProviderExtractNameId, 1, _cpGenericExtractTypeId, _invoiceGenerateTemplate_MailMergeId, regardingId, regardingIdTableName, regardingIdName);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateCareProviderFinanceExtractBatchSetupExtractOnDayFlagToYesByCurrentDayOfWeek(_cpFinanceExtractBatchSetupId1, new DateTime(2024, 1, 1));
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateGenerateAndSendInvoicesAutomaticallyFlag(_cpFinanceExtractBatchSetupId1, true);

            var _cpFinanceExtractBatchId = dbHelper
                                .careProviderFinanceExtractBatch
                                .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, careProviderExtractNameId);
            Assert.AreEqual(1, _cpFinanceExtractBatchId.Count);
            Guid _cpFinanceExtractBatch1Id = _cpFinanceExtractBatchId[0];
            int _cpFinanceExtractBatch1IdNumber1 = (int)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch1Id, "batchid")["batchid"];

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 5; //Every Calendar Month
            var createbatchwithin = 0;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2025, 12, 31);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2024, 5, 6), new TimeSpan(1, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Care Provider Person Contract

            DateTime personContractStartDate = new DateTime(2024, 1, 1);
            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, personContractStartDate, null, true);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                personContractStartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batch

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var _cpFinanceInvoiceBatchId1 = _careProviderFinanceInvoiceBatchId[0];

            var runOn1 = DateTime.Now.AddSeconds(4);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId1, runOn1, null, null);
            Thread.Sleep(2000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            var completedFinanceInvoiceBatchIds = dbHelper.careProviderFinanceInvoiceBatch.GetFinanceInvoiceBatchBySetupIDAndStatusID(_careProviderFinanceInvoiceBatchSetupId1, 2);
            Assert.AreEqual(1, completedFinanceInvoiceBatchIds.Count);
            Assert.AreEqual(_cpFinanceInvoiceBatchId1, completedFinanceInvoiceBatchIds[0]);

            #endregion

            #region Update invoice status to completed
            dbHelper.provider.UpdateDebtorNumber(funderProviderID, currentTimeString);

            //update invoice status to completed
            var _cpFinanceInvoiceId = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchIdAndInvoiceStatusId(_cpFinanceInvoiceBatchId1, 1)[0];
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId, 2);

            #endregion

            #region Payment Methods

            var paymentMethodId = dbHelper.paymentMethod.GetByName("Cheque")[0];

            #endregion

            #region Schdeduled job for "Process CP Finance Extract Batches"

            ProcessCPFinanceScheduledJob("Process CP Finance Extract Batches");

            #endregion

            #region Process Care Provider Finance Extract Batch

            var batchstatusid = 2; //completed
            var allFinanceExtractBatches = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, careProviderExtractNameId, batchstatusid);
            Assert.AreEqual(1, allFinanceExtractBatches.Count);

            #endregion

            #region Accounting Periods and Finance Invoice Payment

            DateTime periodFrom1 = new DateTime(DateTime.Now.AddYears(-2).Year, 1, 1);
            DateTime periodTo1 = new DateTime(DateTime.Now.AddYears(-2).Year, 12, 31);

            DateTime periodFrom2 = new DateTime(DateTime.Now.AddYears(-1).Year, 1, 1);
            DateTime periodTo2 = new DateTime(DateTime.Now.AddYears(-1).Year, 12, 31);

            var accountingPeriodIdExists = dbHelper.careProviderAccountingPeriod.GetByPeriodFromAndPeriodToDateAndCurrentPeriod(periodFrom1, periodTo1, true, false).Any();
            if (accountingPeriodIdExists.Equals(false))
            {
                dbHelper.careProviderAccountingPeriod.CreateCareProviderAccountingPeriod(_teamId, periodFrom1, periodTo1, null, true, false, false);
            }
            var accountingPeriodId1 = dbHelper.careProviderAccountingPeriod.GetByPeriodFromAndPeriodToDateAndCurrentPeriod(periodFrom1, periodTo1, true, false)[0];

            var _cpFinanceInvoicePaymentId1 = dbHelper.careProviderFinanceInvoicePayment.CreateCareProviderFinanceInvoicePayment(_teamId,
                _cpFinanceInvoiceId, providerId, funderProviderID, "provider", providerName, "ACC-7409" + currentTimeString,
                commonMethodsHelper.GetDateWithoutCulture(DateTime.Now), commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddYears(-2)), 10m, 0m, paymentMethodId, "", false, false, "Test7409" + currentTimeString, null);

            accountingPeriodIdExists = dbHelper.careProviderAccountingPeriod.GetByPeriodFromAndPeriodToDateAndCurrentPeriod(periodFrom2, periodTo2, true, false).Any();
            if (accountingPeriodIdExists.Equals(false))
            {
                dbHelper.careProviderAccountingPeriod.CreateCareProviderAccountingPeriod(_teamId, periodFrom2, periodTo2, null, true, false, false);
            }
            var accountingPeriodId2 = dbHelper.careProviderAccountingPeriod.GetByPeriodFromAndPeriodToDateAndCurrentPeriod(periodFrom2, periodTo2, true, false)[0];

            #endregion

            #region Step 17

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicePaymentsPage();

            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad()
                .InsertTextOnQuickSearch("Test7409" + currentTimeString)
                .ClickQuickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(_cpFinanceInvoicePaymentId1);

            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad()
                .ValidateCareProviderFinanceInvoiceLookupButtonDisabled()
                .ValidateEstablishmentLookupButtonDisabled()
                .ValidatePayerLookupButtonDisabled()
                .ValidateInvoiceNumberDisabled()
                .ValidateAlternativeInvoiceNumberDisabled()
                .ValidatePaymentDateDisabled()
                .ValidatePostedDateDisabled()
                .ValidatePaymentAmountDisabled()
                .ValidateDebtWrittenOffDisabled()
                .ValidatePaymentMethodLookupButtonDisabled()
                .ValidatePaidByDisabled()
                .ValidateTotalRecordFieldsDisabled()
                .ValidateAllocatedFieldsDisabled()
                .ValidateReferenceFieldDisabled()
                .ValidateAllocatedFinanceInvoicePaymentLookupButtonDisabled();

            cpFinanceInvoicePaymentRecordPage
                .ClickBackButton();

            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad()
                .ClickDeleteRecordButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("0 item(s) deleted. 1 item(s) not deleted.")
                .TapOKButton();

            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad()
                .ValidateRecordIsPresent(_cpFinanceInvoicePaymentId1);

            Assert.AreEqual(1, dbHelper.careProviderFinanceInvoicePayment.GetByAccountingPeriodId(accountingPeriodId1).Count);
            var fields = dbHelper.careProviderAccountingPeriod.GetById(accountingPeriodId1, "currentperiod", "ispreviousperiod");
            Assert.AreEqual(false, (bool)fields["currentperiod"]);
            Assert.AreEqual(true, (bool)fields["ispreviousperiod"]);

            #endregion


        }


        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-7773

        [TestProperty("JiraIssueID", "ACC-7981")]
        [Description("Test for Finance Invoice Payment Allocation Process - ACC-6731")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice")]
        [TestProperty("Screen2", "Finance Invoice Payments")]
        public void FinanceInvoicePayments_ACC6731_UITestMethod001()
        {

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Diogo";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            var firstName2 = "Aady";
            var _personId2 = dbHelper.person.CreatePersonRecord("", firstName2, "", lastName, "", new DateTime(2000, 2, 1), _ethnicityId, _teamId, 1, 1);
            #endregion

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority            

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS1_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, providerId, false, true);

            var contractScheme2Name = "CPCS2_" + currentTimeString;
            var contractScheme2Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme2Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme2Name, new DateTime(2020, 1, 1), contractScheme2Code, providerId, providerId, false, true);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId1 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var careProviderServiceDetailName = "Care Provider Service Details Test 2";
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, code, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(careProviderServiceId1, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name, Extract Type

            var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);
            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type: Generic Extract File

            #endregion

            #region Finance Extract Batch Setup, Finance Extract Batch

            Guid regardingId = _teamId;
            string regardingIdTableName = "team";
            string regardingIdName = _teamName;

            var _cpFinanceExtractBatchSetupId1 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0), null, careProviderExtractNameId, 1, _cpGenericExtractTypeId, _invoiceGenerateTemplate_MailMergeId, regardingId, regardingIdTableName, regardingIdName);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateCareProviderFinanceExtractBatchSetupExtractOnDayFlagToYesByCurrentDayOfWeek(_cpFinanceExtractBatchSetupId1, new DateTime(2024, 1, 1));
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateGenerateAndSendInvoicesAutomaticallyFlag(_cpFinanceExtractBatchSetupId1, true);

            var _cpFinanceExtractBatchId = dbHelper
                                .careProviderFinanceExtractBatch
                                .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, careProviderExtractNameId);
            Assert.AreEqual(1, _cpFinanceExtractBatchId.Count);
            Guid _cpFinanceExtractBatch1Id = _cpFinanceExtractBatchId[0];
            int _cpFinanceExtractBatch1IdNumber1 = (int)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch1Id, "batchid")["batchid"];

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 2; //funder/service user
            var careproviderinvoicefrequencyid = 5; //Every Calendar Month
            var createbatchwithin = 0;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2025, 12, 31);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2024, 4, 1), new TimeSpan(6, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Care Provider Person Contract

            DateTime personContractStartDate = new DateTime(2024, 4, 1);
            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, providerId, personContractStartDate, null, true);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                personContractStartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batch

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var _cpFinanceInvoiceBatchId1 = _careProviderFinanceInvoiceBatchId[0];

            var runOn1 = DateTime.Now.AddMinutes(1);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId1, runOn1, null, null);
            Thread.Sleep(60000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            var completedFinanceInvoiceBatchIds = dbHelper.careProviderFinanceInvoiceBatch.GetFinanceInvoiceBatchBySetupIDAndStatusID(_careProviderFinanceInvoiceBatchSetupId1, 2);
            Assert.AreEqual(1, completedFinanceInvoiceBatchIds.Count);
            Assert.AreEqual(_cpFinanceInvoiceBatchId1, completedFinanceInvoiceBatchIds[0]);

            #endregion

            #region Update invoice status to completed
            dbHelper.provider.UpdateDebtorNumber(funderProviderID, currentTimeString);
            dbHelper.person.UpdateDebtorNumber(_personId, currentTimeString);
            dbHelper.person.UpdateDebtorNumber(_personId2, currentTimeString);

            //update invoice status to completed
            var _cpFinanceInvoiceId = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchIdAndInvoiceStatusId(_cpFinanceInvoiceBatchId1, 1)[0];
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId, 20);
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId, 2);

            #endregion

            #region Payment Methods

            var paymentMethodId = dbHelper.paymentMethod.GetByName("Cheque")[0];

            #endregion

            #region Scheduled job for "Process CP Finance Extract Batches"

            ProcessCPFinanceScheduledJob("Process CP Finance Extract Batches");

            #endregion

            #region Process Care Provider Finance Extract Batch

            var batchstatusid = 2; //completed
            var allFinanceExtractBatches = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, careProviderExtractNameId, batchstatusid);
            Assert.AreEqual(1, allFinanceExtractBatches.Count);

            #endregion

            #region Care Provider Person Contract, Person Contract Service

            var careProviderPersonContractId2 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId2, _defaultLoginUserID, providerId, careProviderContractScheme1Id, providerId, personContractStartDate, null, true);
            var careProviderPersonContractServiceId2 = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId2, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                personContractStartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId2, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batch

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);
            var _cpFinanceInvoiceBatchId2 = _careProviderFinanceInvoiceBatchId[0];

            var runOn2 = DateTime.Now.AddMinutes(1);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId2, runOn2, null, null);
            Thread.Sleep(60000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            completedFinanceInvoiceBatchIds = dbHelper.careProviderFinanceInvoiceBatch.GetFinanceInvoiceBatchBySetupIDAndStatusID(_careProviderFinanceInvoiceBatchSetupId1, 2);
            Assert.AreEqual(2, completedFinanceInvoiceBatchIds.Count);
            Assert.AreEqual(_cpFinanceInvoiceBatchId2, completedFinanceInvoiceBatchIds[0]);

            #endregion

            #region Finance Invoice Payment

            var _cpFinanceInvoicePaymentId1 = dbHelper.careProviderFinanceInvoicePayment.CreateCareProviderFinanceInvoicePayment(_teamId,
                null, providerId, null, "", "", "6731A" + currentTimeString,
                commonMethodsHelper.GetDateWithoutCulture(DateTime.Now), null, 10m, 0m, paymentMethodId, "", true, false, "Test6731A" + currentTimeString, null);

            #endregion

            #region Step 1, 2

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicePaymentsPage();

            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad()
                .InsertTextOnQuickSearch("6731A" + currentTimeString)
                .ClickQuickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(_cpFinanceInvoicePaymentId1);

            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad()
                .ValidateAllocateButtonIsDisplayed()
                .ClickAllocateButton();

            paymentAllocatePage
                .WaitForPageToLoad()
                .ValidateSearchCriteriaPanelVisible()
                .ValidateResultsGridVisible();

            #endregion

            #region Step 3

            paymentAllocatePage
                .ValidateEstablishmentLinkFieldVisible(providerName)
                .ValidateContractSchemeLookupButtonVisible()
                .ValidateContractSchemeLookupButtonDisabled(false)
                .ValidatePersonLookupButtonVisible()
                .ValidatePersonLookupButtonDisabled(false)
                .ValidateCompletedOnVisible()
                .ValidateCompletedOnDatePickerVisible();

            //Direct Debit BM is inactive
            paymentAllocatePage
                .ValidateDirectDebitOptionsVisible(false);

            #endregion

            #region Step 4

            paymentAllocatePage
                .ClickContractSchemeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("*" + currentTimeString)
                .TapSearchButton()
                .ValidateResultElementPresent(careProviderContractScheme1Id)
                .ValidateResultElementPresent(careProviderContractScheme2Id)
                .ClickCloseButton();

            #endregion

            #region Step 5

            paymentAllocatePage
                .WaitForPageToLoad()
                .ClickPersonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("*" + currentTimeString)
                .TapSearchButton()
                .ValidateResultElementPresent(_personId)
                .ValidateResultElementNotPresent(_personId2)
                .ClickCloseButton();

            paymentAllocatePage
                .WaitForPageToLoad()
                .ClickContractSchemeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("*" + currentTimeString)
                .TapSearchButton()
                .SelectResultElement(careProviderContractScheme1Id);

            paymentAllocatePage
                .WaitForPageToLoad()
                .ClickPersonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("*" + currentTimeString)
                .TapSearchButton()
                .ValidateResultElementPresent(_personId)
                .ValidateResultElementNotPresent(_personId2)
                .ClickCloseButton();

            paymentAllocatePage
                .WaitForPageToLoad()
                .ClickContractSchemeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("*" + currentTimeString)
                .TapSearchButton()
                .SelectResultElement(careProviderContractScheme2Id);

            paymentAllocatePage
                .WaitForPageToLoad()
                .ClickPersonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("*" + currentTimeString)
                .TapSearchButton()
                .ValidateResultElementNotPresent(_personId)
                .ValidateResultElementNotPresent(_personId2)
                .ClickCloseButton();

            #endregion

        }


        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-7815

        [TestProperty("JiraIssueID", "ACC-7982")]
        [Description("Test for Finance Invoice Payment Allocation Process - ACC-6731")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice")]
        [TestProperty("Screen2", "Finance Invoice Payments")]
        public void FinanceInvoicePayments_ACC6731_UITestMethod002()
        {

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Diogo";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            //var firstName2 = "Aady";
            //var _personId2 = dbHelper.person.CreatePersonRecord("", firstName2, "", lastName, "", new DateTime(2000, 2, 1), _ethnicityId, _teamId, 1, 1);
            #endregion

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority            

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS1_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, providerId, false, true);

            //var contractScheme2Name = "CPCS2_" + currentTimeString;
            //var contractScheme2Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            //var careProviderContractScheme2Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme2Name, new DateTime(2020, 1, 1), contractScheme2Code, providerId, providerId, false, true);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId1 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var careProviderServiceDetailName = "Care Provider Service Details Test 2";
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, code, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(careProviderServiceId1, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name, Extract Type

            var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);
            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type: Generic Extract File

            #endregion

            #region Finance Extract Batch Setup, Finance Extract Batch

            Guid regardingId = _teamId;
            string regardingIdTableName = "team";
            string regardingIdName = _teamName;

            var _cpFinanceExtractBatchSetupId1 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0), null, careProviderExtractNameId, 1, _cpGenericExtractTypeId, _invoiceGenerateTemplate_MailMergeId, regardingId, regardingIdTableName, regardingIdName);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateCareProviderFinanceExtractBatchSetupExtractOnDayFlagToYesByCurrentDayOfWeek(_cpFinanceExtractBatchSetupId1, new DateTime(2024, 1, 1));
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateGenerateAndSendInvoicesAutomaticallyFlag(_cpFinanceExtractBatchSetupId1, true);

            var _cpFinanceExtractBatchId = dbHelper
                                .careProviderFinanceExtractBatch
                                .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, careProviderExtractNameId);
            Assert.AreEqual(1, _cpFinanceExtractBatchId.Count);
            Guid _cpFinanceExtractBatch1Id = _cpFinanceExtractBatchId[0];
            int _cpFinanceExtractBatch1IdNumber1 = (int)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch1Id, "batchid")["batchid"];

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 2; //funder/service user
            var careproviderinvoicefrequencyid = 5; //Every Calendar Month
            var createbatchwithin = 0;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2025, 12, 31);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2024, 4, 1), new TimeSpan(6, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Care Provider Person Contract

            DateTime personContractStartDate = new DateTime(2024, 4, 1);
            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, providerId, personContractStartDate, null, true);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                personContractStartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batch

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var _cpFinanceInvoiceBatchId1 = _careProviderFinanceInvoiceBatchId[0];

            var runOn1 = DateTime.Now.AddMinutes(1);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId1, runOn1, null, null);
            Thread.Sleep(60000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            var completedFinanceInvoiceBatchIds = dbHelper.careProviderFinanceInvoiceBatch.GetFinanceInvoiceBatchBySetupIDAndStatusID(_careProviderFinanceInvoiceBatchSetupId1, 2);
            Assert.AreEqual(1, completedFinanceInvoiceBatchIds.Count);
            Assert.AreEqual(_cpFinanceInvoiceBatchId1, completedFinanceInvoiceBatchIds[0]);

            #endregion

            #region Update invoice status to completed
            dbHelper.provider.UpdateDebtorNumber(funderProviderID, currentTimeString);
            dbHelper.person.UpdateDebtorNumber(_personId, currentTimeString);
            //dbHelper.person.UpdateDebtorNumber(_personId2, currentTimeString);

            //update invoice status to completed
            var _cpFinanceInvoiceId = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchIdAndInvoiceStatusId(_cpFinanceInvoiceBatchId1, 1)[0];
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId, 20);
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId, 2);

            #endregion

            #region Payment Methods

            var paymentMethodId = dbHelper.paymentMethod.GetByName("Cheque")[0];

            #endregion

            #region Scheduled job for "Process CP Finance Extract Batches"

            ProcessCPFinanceScheduledJob("Process CP Finance Extract Batches");

            #endregion

            #region Process Care Provider Finance Extract Batch

            var batchstatusid = 2; //completed
            var allFinanceExtractBatches = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, careProviderExtractNameId, batchstatusid);
            Assert.AreEqual(1, allFinanceExtractBatches.Count);

            #endregion

            //#region Care Provider Person Contract, Person Contract Service

            //var careProviderPersonContractId2 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId2, _defaultLoginUserID, providerId, careProviderContractScheme1Id, providerId, personContractStartDate, null, true);
            //var careProviderPersonContractServiceId2 = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId2, _teamId,
            //    careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
            //    personContractStartDate, 1, 1, _careProviderRateUnitId);

            //dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId2, 90); //Completed = 90

            //#endregion

            //#region Process CP Finance Transaction Triggers

            //ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            //#endregion

            //#region Process CP Finance Invoice Batch

            //dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            //commonMethodsDB = new CommonMethodsDB(dbHelper);

            //_careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);
            //var _cpFinanceInvoiceBatchId2 = _careProviderFinanceInvoiceBatchId[0];

            //var runOn2 = DateTime.Now.AddMinutes(1);
            //dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId2, runOn2, null, null);
            //Thread.Sleep(60000);

            //ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            //completedFinanceInvoiceBatchIds = dbHelper.careProviderFinanceInvoiceBatch.GetFinanceInvoiceBatchBySetupIDAndStatusID(_careProviderFinanceInvoiceBatchSetupId1, 2);
            //Assert.AreEqual(1, completedFinanceInvoiceBatchIds.Count);
            //Assert.AreEqual(_cpFinanceInvoiceBatchId2, completedFinanceInvoiceBatchIds[0]);

            //#endregion

            #region Finance Invoice Payment

            var _cpFinanceInvoicePaymentId1 = dbHelper.careProviderFinanceInvoicePayment.CreateCareProviderFinanceInvoicePayment(_teamId,
                null, providerId, null, "", "", "6731A" + currentTimeString,
                commonMethodsHelper.GetDateWithoutCulture(DateTime.Now), null, 10m, 0m, paymentMethodId, "", true, false, "Test6731A" + currentTimeString, null);

            #endregion

            #region Step 6, 7

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicePaymentsPage();

            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad()
                .InsertTextOnQuickSearch("6731A" + currentTimeString)
                .ClickQuickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(_cpFinanceInvoicePaymentId1);

            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad()
                .ValidateAllocateButtonIsDisplayed()
                .ClickAllocateButton();

            paymentAllocatePage
                .WaitForPageToLoad()
                .ClickContractSchemeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("*" + currentTimeString, careProviderContractScheme1Id);

            paymentAllocatePage
                .WaitForPageToLoad()
                .ClickPersonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("*" + currentTimeString, _personId);

            paymentAllocatePage
                .WaitForPageToLoad()
                .InsertTextOnCompletedOn(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickSearchButton();

            paymentAllocatePage
                .WaitForPageToLoad()
                .ValidateResultsSectionTopAreaVisible()
                .ValidateFinanceInvoiceRecordVisible(_cpFinanceInvoiceId.ToString());

            paymentAllocatePage
                .ValidateRunningTotalValue("£0.00")
                .ValidateAllocationValue("£10.00")
                .ValidateVariationValue("-£10.00");

            paymentAllocatePage
                .ValidateCancelButtonVisible()
                .ValidateAllocateButtonVisible();

            paymentAllocatePage
                .SelectFinanceInvoiceRecord(_cpFinanceInvoiceId.ToString())
                .WaitForPageToLoad()
                //.ValidateResultsSectionTopAreaVisible()
                .ValidateRunningTotalValue("£51.43")
                .ValidateAllocationValue("£10.00")
                .ValidateVariationValue("£41.43");

            paymentAllocatePage
                .SelectFinanceInvoiceRecord(_cpFinanceInvoiceId.ToString())
                .WaitForPageToLoad()
                .ValidateRunningTotalValue("£0.00")
                .ValidateAllocationValue("£10.00")
                .ValidateVariationValue("-£10.00");


            #endregion

            #region Step 8

            paymentAllocatePage
                .ClickClearFiltersButton();

            paymentAllocatePage
                .ValidateContractSchemeLinkField("")
                .ValidatePersonLinkField("")
                .ValidateCompletedOnText("")
                .ValidateDirectDebit_NoOptionChecked()
                .ValidateDirectDebit_YesOptionNotChecked();

            #endregion

            #region Step 9 > value to allocated = £10.00, Allocation amount = £51.43, Allocate Button disabled

            paymentAllocatePage
                .ClickSearchButton()
                .WaitForPageToLoad()
                .ValidateAllocateButtonDisabled(); //FI not selected

            paymentAllocatePage
                .SelectFinanceInvoiceRecord(_cpFinanceInvoiceId.ToString())
                .WaitForPageToLoad()
                .ValidateVariationValue("£41.43");

            paymentAllocatePage
                .WaitForPageToLoad()
                .ValidateFinanceInvoiceRecordCellContent(_cpFinanceInvoiceId.ToString(), 14, "51.43")
                .ValidateAllocateButtonDisabled();

            #endregion

            #region Step 9 > value to allocated = £10.00, Allocation amount = £10, Allocate Button enabled

            paymentAllocatePage
                .InsertTextInFinanceInvoiceRecordAllocationField(_cpFinanceInvoiceId.ToString(), "10")
                .WaitForPageToLoad()
                .ValidateVariationValue("£0.00")
                .ValidateAllocateButtonDisabled(false);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7983")]
        [Description("Test for Finance Invoice Payment Allocation Process - ACC-6731")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice")]
        [TestProperty("Screen2", "Finance Invoice Payments")]
        public void FinanceInvoicePayments_ACC6731_UITestMethod003()
        {

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Diogo";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority            

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS1_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, providerId, false, true);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId1 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var careProviderServiceDetailName = "Care Provider Service Details Test 2";
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, code, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(careProviderServiceId1, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name, Extract Type

            var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);
            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type: Generic Extract File

            #endregion

            #region Finance Extract Batch Setup, Finance Extract Batch

            Guid regardingId = _teamId;
            string regardingIdTableName = "team";
            string regardingIdName = _teamName;

            var _cpFinanceExtractBatchSetupId1 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0), null, careProviderExtractNameId, 1, _cpGenericExtractTypeId, _invoiceGenerateTemplate_MailMergeId, regardingId, regardingIdTableName, regardingIdName);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateCareProviderFinanceExtractBatchSetupExtractOnDayFlagToYesByCurrentDayOfWeek(_cpFinanceExtractBatchSetupId1, new DateTime(2024, 1, 1));
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateGenerateAndSendInvoicesAutomaticallyFlag(_cpFinanceExtractBatchSetupId1, true);

            var _cpFinanceExtractBatchId = dbHelper
                                .careProviderFinanceExtractBatch
                                .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, careProviderExtractNameId);
            Assert.AreEqual(1, _cpFinanceExtractBatchId.Count);
            Guid _cpFinanceExtractBatch1Id = _cpFinanceExtractBatchId[0];
            int _cpFinanceExtractBatch1IdNumber1 = (int)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch1Id, "batchid")["batchid"];

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 2; //funder/service user
            var careproviderinvoicefrequencyid = 5; //Every Calendar Month
            var createbatchwithin = 0;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2025, 12, 31);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2024, 4, 1), new TimeSpan(6, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Care Provider Person Contract

            DateTime personContractStartDate = new DateTime(2024, 4, 1);
            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, providerId, personContractStartDate, null, true);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                personContractStartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batch

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var _cpFinanceInvoiceBatchId1 = _careProviderFinanceInvoiceBatchId[0];

            var runOn1 = DateTime.Now.AddMinutes(1);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId1, runOn1, null, null);
            Thread.Sleep(60000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            var completedFinanceInvoiceBatchIds = dbHelper.careProviderFinanceInvoiceBatch.GetFinanceInvoiceBatchBySetupIDAndStatusID(_careProviderFinanceInvoiceBatchSetupId1, 2);
            Assert.AreEqual(1, completedFinanceInvoiceBatchIds.Count);
            Assert.AreEqual(_cpFinanceInvoiceBatchId1, completedFinanceInvoiceBatchIds[0]);

            #endregion

            #region Update invoice status to completed
            dbHelper.provider.UpdateDebtorNumber(funderProviderID, currentTimeString);
            dbHelper.person.UpdateDebtorNumber(_personId, currentTimeString);
            //dbHelper.person.UpdateDebtorNumber(_personId2, currentTimeString);

            //update invoice status to completed
            var _cpFinanceInvoiceId = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchIdAndInvoiceStatusId(_cpFinanceInvoiceBatchId1, 1)[0];
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId, 20);
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId, 2);

            #endregion

            #region Payment Methods

            var paymentMethodId = dbHelper.paymentMethod.GetByName("Cheque")[0];

            #endregion

            #region Scheduled job for "Process CP Finance Extract Batches"

            ProcessCPFinanceScheduledJob("Process CP Finance Extract Batches");

            #endregion

            #region Process Care Provider Finance Extract Batch

            var batchstatusid = 2; //completed
            var allFinanceExtractBatches = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, careProviderExtractNameId, batchstatusid);
            Assert.AreEqual(1, allFinanceExtractBatches.Count);

            #endregion

            #region Process CP Finance Invoice Batch

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);
            var _cpFinanceInvoiceBatchId2 = _careProviderFinanceInvoiceBatchId[0];

            var runOn2 = DateTime.Now.AddSeconds(10);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId2, runOn2, null, null);
            Thread.Sleep(10000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            completedFinanceInvoiceBatchIds = dbHelper.careProviderFinanceInvoiceBatch.GetFinanceInvoiceBatchBySetupIDAndStatusID(_careProviderFinanceInvoiceBatchSetupId1, 2);
            Assert.AreEqual(2, completedFinanceInvoiceBatchIds.Count);
            Assert.AreEqual(_cpFinanceInvoiceBatchId2, completedFinanceInvoiceBatchIds[0]);

            #endregion

            #region Update invoice status to completed

            //update invoice status to completed
            var _cpFinanceInvoiceId2 = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchIdAndInvoiceStatusId(_cpFinanceInvoiceBatchId2, 1)[0];
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId2, 20);
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId2, 2);

            #endregion

            #region Process CP Finance Extract Batches
            var _cpFinanceExtractBatchId2 = dbHelper
                                .careProviderFinanceExtractBatch
                                .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, careProviderExtractNameId);
            Assert.AreEqual(2, _cpFinanceExtractBatchId2.Count);
            Guid _cpFinanceExtractBatch2Id = _cpFinanceExtractBatchId[0];

            var runOn3 = DateTime.Now.AddSeconds(5);
            dbHelper.careProviderFinanceExtractBatch.UpdateCareProviderFinanceExtractBatchRunOnDate(_cpFinanceExtractBatch2Id, runOn3);
            Thread.Sleep(5000);

            ProcessCPFinanceScheduledJob("Process CP Finance Extract Batches");

            #endregion

            #region Finance Invoice Payment

            var _cpFinanceInvoicePaymentId1 = dbHelper.careProviderFinanceInvoicePayment.CreateCareProviderFinanceInvoicePayment(_teamId,
                null, providerId, null, "", "", "6731B" + currentTimeString,
                commonMethodsHelper.GetDateWithoutCulture(DateTime.Now), null, 10m, 0m, paymentMethodId, "", true, false, "Test6731A" + currentTimeString, null);

            #endregion

            #region Step 10

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicePaymentsPage();

            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad()
                .InsertTextOnQuickSearch("6731B" + currentTimeString)
                .ClickQuickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(_cpFinanceInvoicePaymentId1);

            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad()
                .ValidateAllocateButtonIsDisplayed()
                .ClickAllocateButton();

            paymentAllocatePage
                .WaitForPageToLoad()
                .ClickSearchButton();

            paymentAllocatePage
                .WaitForPageToLoad()
                .ValidateResultsSectionTopAreaVisible()
                .ValidateFinanceInvoiceRecordVisible(_cpFinanceInvoiceId.ToString())
                .ValidateFinanceInvoiceRecordVisible(_cpFinanceInvoiceId2.ToString())
                .ValidateAllocateButtonDisabled(true);

            paymentAllocatePage
                .ValidateAllocateButtonDisabled()
                .SelectFinanceInvoiceRecord(_cpFinanceInvoiceId.ToString())
                .SelectFinanceInvoiceRecord(_cpFinanceInvoiceId2.ToString())
                .ValidateAllocateButtonDisabled();

            paymentAllocatePage
                .WaitForPageToLoad()
                .InsertTextInFinanceInvoiceRecordAllocationField(_cpFinanceInvoiceId.ToString(), "5")
                .WaitForPageToLoad()
                .ValidateAllocateButtonDisabled();

            paymentAllocatePage
                .WaitForPageToLoad()
                .InsertTextInFinanceInvoiceRecordAllocationField(_cpFinanceInvoiceId2.ToString(), "5")
                .WaitForPageToLoad()
                .ValidateAllocateButtonDisabled(false)
                .ClickCancelButton();

            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad()
                .ValidateAllocateButtonIsDisplayed()
                .ClickAllocateButton();

            paymentAllocatePage
                .WaitForPageToLoad()
                .ClickSearchButton();

            paymentAllocatePage
                .WaitForPageToLoad()
                .ValidateResultsSectionTopAreaVisible()
                .ValidateAllocateButtonDisabled()
                .SelectFinanceInvoiceRecord(_cpFinanceInvoiceId.ToString())
                .SelectFinanceInvoiceRecord(_cpFinanceInvoiceId2.ToString())
                .InsertTextInFinanceInvoiceRecordAllocationField(_cpFinanceInvoiceId.ToString(), "3")
                .ValidateAllocateButtonDisabled()
                .SelectFinanceInvoiceRecord(_cpFinanceInvoiceId2.ToString())
                .ValidateAllocateButtonDisabled(false);

            #endregion

            #region Step 11

            paymentAllocatePage
                .SelectFinanceInvoiceRecord(_cpFinanceInvoiceId2.ToString())
                .InsertTextInFinanceInvoiceRecordAllocationField(_cpFinanceInvoiceId2.ToString(), "3")
                .ClickAllocateButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("The Allocation values of the Finance Invoices selected do NOT equal the value to be allocated and varies by -£4.00. Please either Cancel in order to make corrections or Click Continue and this will create a further Total Finance Invoice Payment record = £4.00 for future allocation.");

            #endregion

            #region Step 18 > Click Cancel Button

            confirmDynamicDialogPopup
                .TapCancelButton();

            paymentAllocatePage
                .WaitForPageToLoad()
                .ClickCancelButton();

            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad()
                .ValidatePaymentAmountText("10.00")
                .ValidateCareproviderFinanceInvoiceLinkText("");
            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-7939

        [TestProperty("JiraIssueID", "ACC-7984")]
        [Description("Test for Finance Invoice Payment Allocation Process - ACC-6731")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice")]
        [TestProperty("Screen2", "Finance Invoice Payments")]
        public void FinanceInvoicePayments_ACC6731_UITestMethod004()
        {

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Diogo";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority            

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS1_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, providerId, false, true);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId1 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var careProviderServiceDetailName = "Care Provider Service Details Test 2";
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, code, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(careProviderServiceId1, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name, Extract Type

            var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);
            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type: Generic Extract File

            #endregion

            #region Finance Extract Batch Setup, Finance Extract Batch

            Guid regardingId = _teamId;
            string regardingIdTableName = "team";
            string regardingIdName = _teamName;

            var _cpFinanceExtractBatchSetupId1 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0), null, careProviderExtractNameId, 1, _cpGenericExtractTypeId, _invoiceGenerateTemplate_MailMergeId, regardingId, regardingIdTableName, regardingIdName);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateCareProviderFinanceExtractBatchSetupExtractOnDayFlagToYesByCurrentDayOfWeek(_cpFinanceExtractBatchSetupId1, new DateTime(2024, 1, 1));
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateGenerateAndSendInvoicesAutomaticallyFlag(_cpFinanceExtractBatchSetupId1, true);

            var _cpFinanceExtractBatchId = dbHelper
                                .careProviderFinanceExtractBatch
                                .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, careProviderExtractNameId);
            Assert.AreEqual(1, _cpFinanceExtractBatchId.Count);
            Guid _cpFinanceExtractBatch1Id = _cpFinanceExtractBatchId[0];
            int _cpFinanceExtractBatch1IdNumber1 = (int)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch1Id, "batchid")["batchid"];

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 2; //funder/service user
            var careproviderinvoicefrequencyid = 5; //Every Calendar Month
            var createbatchwithin = 0;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2025, 12, 31);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2024, 4, 1), new TimeSpan(6, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Care Provider Person Contract

            DateTime personContractStartDate = new DateTime(2024, 4, 1);
            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, providerId, personContractStartDate, null, true);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                personContractStartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batch

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var _cpFinanceInvoiceBatchId1 = _careProviderFinanceInvoiceBatchId[0];

            var runOn1 = DateTime.Now.AddMinutes(1);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId1, runOn1, null, null);
            Thread.Sleep(60000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            var completedFinanceInvoiceBatchIds = dbHelper.careProviderFinanceInvoiceBatch.GetFinanceInvoiceBatchBySetupIDAndStatusID(_careProviderFinanceInvoiceBatchSetupId1, 2);
            Assert.AreEqual(1, completedFinanceInvoiceBatchIds.Count);
            Assert.AreEqual(_cpFinanceInvoiceBatchId1, completedFinanceInvoiceBatchIds[0]);

            #endregion

            #region Update invoice status to completed
            dbHelper.provider.UpdateDebtorNumber(funderProviderID, currentTimeString);
            dbHelper.person.UpdateDebtorNumber(_personId, currentTimeString);
            //dbHelper.person.UpdateDebtorNumber(_personId2, currentTimeString);

            //update invoice status to completed
            var _cpFinanceInvoiceId = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchIdAndInvoiceStatusId(_cpFinanceInvoiceBatchId1, 1)[0];
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId, 20);
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId, 2);

            #endregion

            #region Payment Methods

            var paymentMethodId = dbHelper.paymentMethod.GetByName("Cheque")[0];

            #endregion

            #region Scheduled job for "Process CP Finance Extract Batches"

            ProcessCPFinanceScheduledJob("Process CP Finance Extract Batches");

            #endregion

            #region Process Care Provider Finance Extract Batch

            var batchstatusid = 2; //completed
            var allFinanceExtractBatches = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, careProviderExtractNameId, batchstatusid);
            Assert.AreEqual(1, allFinanceExtractBatches.Count);

            #endregion

            #region Finance Invoice Payment

            var _cpFinanceInvoicePaymentId1 = dbHelper.careProviderFinanceInvoicePayment.CreateCareProviderFinanceInvoicePayment(_teamId,
                null, providerId, null, "", "", "6731B" + currentTimeString,
                commonMethodsHelper.GetDateWithoutCulture(DateTime.Now), null, 51.43m, 0m, paymentMethodId, "", true, false, "Test6731B" + currentTimeString, null);
            var paymentRecordFields = dbHelper.careProviderFinanceInvoicePayment.GetCareProviderFinanceInvoicePaymentById(_cpFinanceInvoicePaymentId1, "name");

            #endregion

            #region Step 14, 15, 13

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicePaymentsPage();

            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad()
                .InsertTextOnQuickSearch("6731B" + currentTimeString)
                .ClickQuickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(_cpFinanceInvoicePaymentId1);

            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad()
                .ValidateAllocateButtonIsDisplayed()
                .ClickAllocateButton();

            paymentAllocatePage
                .WaitForPageToLoad()
                .ClickSearchButton();

            //Mentioned columns should display in search result listing screen            
            paymentAllocatePage
                .WaitForPageToLoad()
                .ValidateResultsGridVisible()
                .ClickAutoFitColumnWidthButton()
                .WaitForPageToLoad()
                .ValidateResultsHeaderCellContent(1, "Row")
                .ValidateResultsHeaderCellContent(2, "FEB Id")
                .ValidateResultsHeaderCellContent(3, "FIB Id")
                .ValidateResultsHeaderCellContent(4, "Payer")
                .ValidateResultsHeaderCellContent(5, "Person")
                .ValidateResultsHeaderCellContent(6, "Id")
                .ValidateResultsHeaderCellContent(7, "Ref")
                .ValidateResultsHeaderCellContent(8, "Contract Scheme")
                .ValidateResultsHeaderCellContent(9, "Inv No")
                .ValidateResultsHeaderCellContent(10, "Invoice Date")
                .ValidateResultsHeaderCellContent(11, "Charges Up To")
                .ValidateResultsHeaderCellContent(12, "Amount")
                .ValidateResultsHeaderCellContent(13, "Balance")
                .ValidateResultsHeaderCellContent(14, "Allocation")
                .ValidateSelectAllHeaderCheckboxChecked(false)
                .ValidateFinanceInvoiceRecordCheckboxChecked(_cpFinanceInvoiceId.ToString(), false);

            //Verify that only Allocation and Select record (check box) fields are live in Result listing screen
            paymentAllocatePage
                .ValidateFinanceInvoiceRecordInputCellIsAvailable(_cpFinanceInvoiceId.ToString(), 1, false)
                .ValidateFinanceInvoiceRecordInputCellIsAvailable(_cpFinanceInvoiceId.ToString(), 2, false)
                .ValidateFinanceInvoiceRecordInputCellIsAvailable(_cpFinanceInvoiceId.ToString(), 3, false)
                .ValidateFinanceInvoiceRecordInputCellIsAvailable(_cpFinanceInvoiceId.ToString(), 4, false)
                .ValidateFinanceInvoiceRecordInputCellIsAvailable(_cpFinanceInvoiceId.ToString(), 5, false)
                .ValidateFinanceInvoiceRecordInputCellIsAvailable(_cpFinanceInvoiceId.ToString(), 6, false)
                .ValidateFinanceInvoiceRecordInputCellIsAvailable(_cpFinanceInvoiceId.ToString(), 7, false)
                .ValidateFinanceInvoiceRecordInputCellIsAvailable(_cpFinanceInvoiceId.ToString(), 8, false)
                .ValidateFinanceInvoiceRecordInputCellIsAvailable(_cpFinanceInvoiceId.ToString(), 9, false)
                .ValidateFinanceInvoiceRecordInputCellIsAvailable(_cpFinanceInvoiceId.ToString(), 10, false)
                .ValidateFinanceInvoiceRecordInputCellIsAvailable(_cpFinanceInvoiceId.ToString(), 11, false)
                .ValidateFinanceInvoiceRecordInputCellIsAvailable(_cpFinanceInvoiceId.ToString(), 12, false)
                .ValidateFinanceInvoiceRecordInputCellIsAvailable(_cpFinanceInvoiceId.ToString(), 13, false)
                .ValidateFinanceInvoiceRecordInputCellIsAvailable(_cpFinanceInvoiceId.ToString(), 14, true)
                .ValidateFinanceInvoiceRecordInputCellIsAvailable(_cpFinanceInvoiceId.ToString(), 15, true)
                .SelectFinanceInvoiceRecord(_cpFinanceInvoiceId.ToString())
                .InsertTextInFinanceInvoiceRecordAllocationField(_cpFinanceInvoiceId.ToString(), "5")
                .SelectFinanceInvoiceRecord(_cpFinanceInvoiceId.ToString())
                .InsertTextInFinanceInvoiceRecordAllocationField(_cpFinanceInvoiceId.ToString(), "51.43");

            paymentAllocatePage
                .WaitForPageToLoad()
                .SelectFinanceInvoiceRecord(_cpFinanceInvoiceId.ToString())
                .WaitForPageToLoad()
                .ClickAllocateButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Allocated Finance Invoice Payment records successfully created")
                .TapCloseButton();

            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad()
                .ValidateAllocateButtonIsDisplayed(false)
                .ValidateAllocated_YesOptionChecked() //Allocated flag is set to Yes in FIP record
                .ValidateAllocated_NoOptionNotChecked()
                .ValidateAllocationsTabIsVisible()
                .ValidatePaymentAmountDisabled();

            #endregion

            #region Step 19

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicesSection();

            cpFinanceInvoicesPage
                .WaitForCPFinanceInvoicesPageToLoad()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceInvoicesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceId);

            financeInvoiceRecordPage
                .WaitForCPFinanceInvoiceRecordPageToLoad()
                .ValidatePaymentsTabVisible(true)
                .ClickPaymentsTab();

            var paymentRecords = dbHelper.careProviderFinanceInvoicePayment.GetByInvoiceId(_cpFinanceInvoiceId);
            Assert.AreEqual(1, paymentRecords.Count);
            var _cpFinanceInvoicePaymentId2 = paymentRecords[0];

            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad(null, false)
                .ValidateRecordIsPresent(_cpFinanceInvoicePaymentId2)
                .OpenRecord(_cpFinanceInvoicePaymentId2);

            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad()
                .ValidatePaymentAmountText("51.43")
                .ValidateTotalrecord_NoOptionChecked()
                .ValidateTotalrecord_YesOptionNotChecked()
                .ValidateAllocated_YesOptionChecked()
                .ValidateAllocated_NoOptionNotChecked()
                .ValidateFinanceInvoicePayment_AllocatedTo_LinkText(paymentRecordFields["name"].ToString());


            //Verify new FIP record in Allocation tab of original FIP record.
            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicePaymentsPage();

            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad()
                .InsertTextOnQuickSearch("*" + currentTimeString)
                .ClickQuickSearchButton()
                .WaitForPageToLoad()
                .ValidateRecordIsPresent(_cpFinanceInvoicePaymentId1)
                .ValidateRecordIsPresent(_cpFinanceInvoicePaymentId2)
                .OpenRecord(_cpFinanceInvoicePaymentId1);

            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad()
                .ClickAllocationsTab();

            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad(_cpFinanceInvoicePaymentId1, true)
                .ValidateRecordIsPresent(_cpFinanceInvoicePaymentId2);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7985")]
        [Description("Test for Finance Invoice Payment Allocation Process - ACC-6731")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice")]
        [TestProperty("Screen2", "Finance Invoice Payments")]
        public void FinanceInvoicePayments_ACC6731_UITestMethod005()
        {

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Diogo";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority            

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS1_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, providerId, false, true);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId1 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var careProviderServiceDetailName = "Care Provider Service Details Test 2";
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, code, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(careProviderServiceId1, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name, Extract Type

            var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);
            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type: Generic Extract File

            #endregion

            #region Finance Extract Batch Setup, Finance Extract Batch

            Guid regardingId = _teamId;
            string regardingIdTableName = "team";
            string regardingIdName = _teamName;

            var _cpFinanceExtractBatchSetupId1 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0), null, careProviderExtractNameId, 1, _cpGenericExtractTypeId, _invoiceGenerateTemplate_MailMergeId, regardingId, regardingIdTableName, regardingIdName);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateCareProviderFinanceExtractBatchSetupExtractOnDayFlagToYesByCurrentDayOfWeek(_cpFinanceExtractBatchSetupId1, new DateTime(2024, 1, 1));
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateGenerateAndSendInvoicesAutomaticallyFlag(_cpFinanceExtractBatchSetupId1, true);

            var _cpFinanceExtractBatchId = dbHelper
                                .careProviderFinanceExtractBatch
                                .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, careProviderExtractNameId);
            Assert.AreEqual(1, _cpFinanceExtractBatchId.Count);
            Guid _cpFinanceExtractBatch1Id = _cpFinanceExtractBatchId[0];
            int _cpFinanceExtractBatch1IdNumber1 = (int)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch1Id, "batchid")["batchid"];

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 2; //funder/service user
            var careproviderinvoicefrequencyid = 5; //Every Calendar Month
            var createbatchwithin = 0;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2025, 12, 31);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2024, 4, 1), new TimeSpan(6, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Care Provider Person Contract

            DateTime personContractStartDate = new DateTime(2024, 4, 1);
            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, providerId, personContractStartDate, null, true);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                personContractStartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batch

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var _cpFinanceInvoiceBatchId1 = _careProviderFinanceInvoiceBatchId[0];

            var runOn1 = DateTime.Now.AddMinutes(1);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId1, runOn1, null, null);
            Thread.Sleep(60000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            var completedFinanceInvoiceBatchIds = dbHelper.careProviderFinanceInvoiceBatch.GetFinanceInvoiceBatchBySetupIDAndStatusID(_careProviderFinanceInvoiceBatchSetupId1, 2);
            Assert.AreEqual(1, completedFinanceInvoiceBatchIds.Count);
            Assert.AreEqual(_cpFinanceInvoiceBatchId1, completedFinanceInvoiceBatchIds[0]);

            #endregion

            #region Update invoice status to completed
            dbHelper.provider.UpdateDebtorNumber(funderProviderID, currentTimeString);
            dbHelper.person.UpdateDebtorNumber(_personId, currentTimeString);

            //update invoice status to completed
            var _cpFinanceInvoiceId = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchIdAndInvoiceStatusId(_cpFinanceInvoiceBatchId1, 1)[0];
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId, 20);
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId, 2);

            #endregion

            #region Payment Methods

            var paymentMethodId = dbHelper.paymentMethod.GetByName("Cheque")[0];

            #endregion

            #region Scheduled job for "Process CP Finance Extract Batches"

            ProcessCPFinanceScheduledJob("Process CP Finance Extract Batches");

            #endregion

            #region Process Care Provider Finance Extract Batch

            var batchstatusid = 2; //completed
            var allFinanceExtractBatches = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, careProviderExtractNameId, batchstatusid);
            Assert.AreEqual(1, allFinanceExtractBatches.Count);

            #endregion

            #region Finance Invoice Payment

            var _cpFinanceInvoicePaymentId1 = dbHelper.careProviderFinanceInvoicePayment.CreateCareProviderFinanceInvoicePayment(_teamId,
                null, providerId, null, "", "", "6731B" + currentTimeString,
                commonMethodsHelper.GetDateWithoutCulture(DateTime.Now), null, 51.43m, 0m, paymentMethodId, "", true, false, "Test6731B" + currentTimeString, null);

            #endregion

            #region Step 16

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicePaymentsPage();

            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad()
                .InsertTextOnQuickSearch("6731B" + currentTimeString)
                .ClickQuickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(_cpFinanceInvoicePaymentId1);

            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad()
                .ValidateAllocateButtonIsDisplayed()
                .ClickAllocateButton();

            paymentAllocatePage
                .WaitForPageToLoad()
                .ClickSearchButton();

            paymentAllocatePage
                .WaitForPageToLoad()
                .ValidateResultsGridVisible()
                .SelectFinanceInvoiceRecord(_cpFinanceInvoiceId.ToString())
                .InsertTextInFinanceInvoiceRecordAllocationField(_cpFinanceInvoiceId.ToString(), "-test")
                .ValidateFinanceInvoiceRecordAllocationField_FormError(_cpFinanceInvoiceId, "Please enter a valid decimal value");

            paymentAllocatePage
                .WaitForPageToLoad()
                .InsertTextInFinanceInvoiceRecordAllocationField(_cpFinanceInvoiceId.ToString(), "-51.4343")
                .ValidateFinanceInvoiceRecordAllocationField_FormError(_cpFinanceInvoiceId, "Number of decimal places cannot be greater than 2.");

            paymentAllocatePage
                .WaitForPageToLoad()
                .InsertTextInFinanceInvoiceRecordAllocationField(_cpFinanceInvoiceId.ToString(), "")
                .ValidateFinanceInvoiceRecordAllocationField_FormError(_cpFinanceInvoiceId, "Please fill out this field.");

            paymentAllocatePage
                .WaitForPageToLoad()
                .InsertTextInFinanceInvoiceRecordAllocationField(_cpFinanceInvoiceId.ToString(), "51.44")
                .ValidateFinanceInvoiceRecordAllocationField_FormError(_cpFinanceInvoiceId, "Value cannot be greater than Balance");

            paymentAllocatePage
                .WaitForPageToLoad()
                .InsertTextInFinanceInvoiceRecordAllocationField(_cpFinanceInvoiceId.ToString(), "51.44")
                .ValidateFinanceInvoiceRecordAllocationField_FormError(_cpFinanceInvoiceId, "Value cannot be greater than Balance");

            #endregion

            #region Step 17

            paymentAllocatePage
                .WaitForPageToLoad()
                .InsertTextInFinanceInvoiceRecordAllocationField(_cpFinanceInvoiceId.ToString(), "-50")
                .ClickAllocateButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("The Allocation values of the Finance Invoices selected do NOT equal the value to be allocated and varies by -£101.43. Please either Cancel in order to make corrections or Click Continue and this will create a further Total Finance Invoice Payment record = £101.43 for future allocation.");

            #endregion


        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-7957

        [TestProperty("JiraIssueID", "ACC-7986")]
        [Description("Test for Finance Invoice Payment Allocation Process - ACC-6731")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice")]
        [TestProperty("Screen2", "Finance Invoice Payments")]
        public void FinanceInvoicePayments_ACC6731_UITestMethod006()
        {

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Diogo";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority            

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS1_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, providerId, false, true);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId1 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var careProviderServiceDetailName = "Care Provider Service Details Test 2";
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, code, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(careProviderServiceId1, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name, Extract Type

            var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);
            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type: Generic Extract File

            #endregion

            #region Finance Extract Batch Setup, Finance Extract Batch

            Guid regardingId = _teamId;
            string regardingIdTableName = "team";
            string regardingIdName = _teamName;

            var _cpFinanceExtractBatchSetupId1 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0), null, careProviderExtractNameId, 1, _cpGenericExtractTypeId, _invoiceGenerateTemplate_MailMergeId, regardingId, regardingIdTableName, regardingIdName);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateCareProviderFinanceExtractBatchSetupExtractOnDayFlagToYesByCurrentDayOfWeek(_cpFinanceExtractBatchSetupId1, new DateTime(2024, 1, 1));
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateGenerateAndSendInvoicesAutomaticallyFlag(_cpFinanceExtractBatchSetupId1, true);

            var _cpFinanceExtractBatchId = dbHelper
                                .careProviderFinanceExtractBatch
                                .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, careProviderExtractNameId);
            Assert.AreEqual(1, _cpFinanceExtractBatchId.Count);
            Guid _cpFinanceExtractBatch1Id = _cpFinanceExtractBatchId[0];
            int _cpFinanceExtractBatch1IdNumber1 = (int)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch1Id, "batchid")["batchid"];

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 2; //funder/service user
            var careproviderinvoicefrequencyid = 5; //Every Calendar Month
            var createbatchwithin = 0;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2025, 12, 31);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2024, 4, 1), new TimeSpan(6, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Care Provider Person Contract

            DateTime personContractStartDate = new DateTime(2024, 4, 1);
            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, providerId, personContractStartDate, null, true);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                personContractStartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batch

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var _cpFinanceInvoiceBatchId1 = _careProviderFinanceInvoiceBatchId[0];

            var runOn1 = DateTime.Now.AddMinutes(1);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId1, runOn1, null, null);
            Thread.Sleep(60000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            var completedFinanceInvoiceBatchIds = dbHelper.careProviderFinanceInvoiceBatch.GetFinanceInvoiceBatchBySetupIDAndStatusID(_careProviderFinanceInvoiceBatchSetupId1, 2);
            Assert.AreEqual(1, completedFinanceInvoiceBatchIds.Count);
            Assert.AreEqual(_cpFinanceInvoiceBatchId1, completedFinanceInvoiceBatchIds[0]);

            #endregion

            #region Update invoice status to completed
            dbHelper.provider.UpdateDebtorNumber(funderProviderID, currentTimeString);
            dbHelper.person.UpdateDebtorNumber(_personId, currentTimeString);
            //dbHelper.person.UpdateDebtorNumber(_personId2, currentTimeString);

            //update invoice status to completed
            var _cpFinanceInvoiceId = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchIdAndInvoiceStatusId(_cpFinanceInvoiceBatchId1, 1)[0];
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId, 2);

            #endregion

            #region Payment Methods

            var paymentMethodId = dbHelper.paymentMethod.GetByName("Cheque")[0];

            #endregion

            #region Report Type

            var _reportTypeId = commonMethodsDB.CreateCareProviderFinanceInvoicePaymentReportType(_teamId, "Test", new DateTime(2024, 1, 1), 99, "99");

            #endregion

            #region Scheduled job for "Process CP Finance Extract Batches"

            ProcessCPFinanceScheduledJob("Process CP Finance Extract Batches");

            #endregion

            #region Finance Invoice Payment

            var _cpFinanceInvoicePaymentId1 = dbHelper.careProviderFinanceInvoicePayment.CreateCareProviderFinanceInvoicePayment(_teamId,
                null, providerId, null, "", "", "6731B" + currentTimeString,
                commonMethodsHelper.GetDateWithoutCulture(DateTime.Now), null, 51.43m, 0m, paymentMethodId, "", true, false, "Test6731A" + currentTimeString, null, _reportTypeId);
            var paymentRecordFields = dbHelper.careProviderFinanceInvoicePayment.GetCareProviderFinanceInvoicePaymentById(_cpFinanceInvoicePaymentId1, "name");

            #endregion

            #region Step 12

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicePaymentsPage();

            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad()
                .InsertTextOnQuickSearch("6731B" + currentTimeString)
                .ClickQuickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(_cpFinanceInvoicePaymentId1);

            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad()
                .ClickAllocateButton();

            paymentAllocatePage
                .WaitForPageToLoad()
                .ClickSearchButton();

            paymentAllocatePage
                .WaitForPageToLoad()
                .ValidateResultsSectionTopAreaVisible()
                .SelectFinanceInvoiceRecord(_cpFinanceInvoiceId.ToString());

            paymentAllocatePage
                .WaitForPageToLoad()
                .InsertTextInFinanceInvoiceRecordAllocationField(_cpFinanceInvoiceId.ToString(), "40")
                .ClickAllocateButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("The Allocation values of the Finance Invoices selected do NOT equal the value to be allocated and varies by -£11.43. Please either Cancel in order to make corrections or Click Continue and this will create a further Total Finance Invoice Payment record = £11.43 for future allocation.")
                .TapOKButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Allocated Finance Invoice Payment records successfully created")
                .TapCloseButton();

            var paymentRecord1 = dbHelper.careProviderFinanceInvoicePayment.GetByInvoiceId(_cpFinanceInvoiceId);
            var _allocatedCpFinanceInvoicePaymentId1 = paymentRecord1[0];
            var _allocatedCpFIP_Fields = dbHelper.careProviderFinanceInvoicePayment.GetCareProviderFinanceInvoicePaymentById(_allocatedCpFinanceInvoicePaymentId1, "allocatedfinanceinvoicepaymentid");

            var _unallocatedCpFinanceInvoicePaymentId2 = dbHelper.careProviderFinanceInvoicePayment.GetByTotalRecord_Allocated_FinanceInvoicePaymentAllocatedTo(true, false, (Guid)_allocatedCpFIP_Fields["allocatedfinanceinvoicepaymentid"])[0];

            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad()
                .ValidateAllocated_YesOptionChecked() //Allocated flag is set to Yes in FIP record
                .ValidateAllocated_NoOptionNotChecked()
                .ValidateTotalrecord_YesOptionChecked()
                .ValidateTotalrecord_NoOptionNotChecked();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicePaymentsPage();

            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad()
                .InsertTextOnQuickSearch("*" + currentTimeString)
                .ClickQuickSearchButton()
                .WaitForPageToLoad()
                .ValidateRecordIsPresent(_cpFinanceInvoicePaymentId1)
                .ValidateRecordIsPresent(_allocatedCpFinanceInvoicePaymentId1)
                .OpenRecord(_allocatedCpFinanceInvoicePaymentId1);

            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad(_allocatedCpFinanceInvoicePaymentId1.ToString())
                .ValidateAllocated_YesOptionChecked()
                .ValidateAllocated_NoOptionNotChecked()
                .ValidateTotalrecord_NoOptionChecked()
                .ValidateTotalrecord_YesOptionNotChecked()
                .ValidateAllocateButtonIsDisplayed(false)
                .ValidateFinanceInvoicePayment_AllocatedTo_LinkText(paymentRecordFields["name"].ToString())
                .ValidateReportTypeLinkText("")
                .ClickBackButton();

            cpFinanceInvoicePaymentsPage
            .WaitForPageToLoad()
            .OpenRecord(_unallocatedCpFinanceInvoicePaymentId2);

            cpFinanceInvoicePaymentRecordPage
                .WaitForPageToLoad(_unallocatedCpFinanceInvoicePaymentId2.ToString())
                .ValidateAllocated_NoOptionChecked()
                .ValidateAllocated_YesOptionNotChecked()
                .ValidateTotalrecord_YesOptionChecked()
                .ValidateTotalrecord_NoOptionNotChecked()
                .ValidateAllocateButtonIsDisplayed(true)
                .ValidateFinanceInvoicePayment_AllocatedTo_LinkText(paymentRecordFields["name"].ToString())
                .ValidateReportTypeLinkText("Test");

            #endregion

        }

        #endregion

    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Finance.FinanceAdmin
{
    [TestClass]
    public class FinanceInvoiceBatchSetups_UITestCases : FunctionalTest
    {
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private string _systemUsername;
        private Guid _paymentTypeCodeId;
        private string _paymentTypeCodeName;
        private Guid _providerBatchGroupingId;
        private string _providerBatchGroupingName;
        private Guid _invoiceById;
        private string _invoiceByName;
        private Guid _invoiceFrequencyId;
        private string _invoiceFrequencyName;
        private Guid _extractNameId;
        private string _extractName;
        private Guid _vatCodeId;
        private Guid _rateType;
        private Guid _rateunitid;
        private string currentDate = DateTime.Now.ToString("yyyyMMddHHmmss");

        [TestInitialize()]
        public void TestsSetupMethod()
        {
            try
            {
                #region Internal

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("Finance Invoice Batch Setup T1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("Finance Invoice Batch Setup T1", null, _businessUnitId, "989767", "FinanceInvoiceBatchSetupT1@careworkstempmail.com", "FinanceInvoiceBatchSetup T1", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region System User FinanceInvoiceBatch

                _systemUsername = "FinanceInvoiceBatchUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "FinanceInvoiceBatch", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

                #region Payment Type Code

                _paymentTypeCodeName = "Default Payment";
                _paymentTypeCodeId = commonMethodsDB.CreatePaymentTypeCode(_paymentTypeCodeName, new DateTime(2023, 1, 1), _teamId);

                #endregion

                #region Provider Batch Grouping

                _providerBatchGroupingName = "Default Batching";
                _providerBatchGroupingId = commonMethodsDB.CreateProviderBatchGrouping(_providerBatchGroupingName, new DateTime(2023, 1, 1), _teamId);

                #endregion

                #region Invoice By

                _invoiceByName = "Invoice Default";
                _invoiceById = commonMethodsDB.CreateInvoiceBy(_teamId, _invoiceByName, new DateTime(2022, 1, 1), true, false, false);

                #endregion

                #region Invoice Frequencies

                _invoiceFrequencyName = "Every Week";
                _invoiceFrequencyId = dbHelper.invoiceFrequency.GetByName(_invoiceFrequencyName)[0];

                #endregion

                #region Extract Name

                _extractName = "Generic Supplier Payments Extract";
                _extractNameId = commonMethodsDB.CreateExtractName(_teamId, _extractName, new DateTime(2022, 12, 12), true, false, false);

                #endregion

                #region VAT Code

                _vatCodeId = dbHelper.vatCode.GetByName("Standard Rated")[0];

                #endregion

                #region  Rate Type

                var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
                if (!dbHelper.rateType.GetByName("Hours (Whole)").Any())
                    dbHelper.rateType.CreateRateType(_teamId, _teamId, "Hours (Whole)", code, new DateTime(2020, 1, 1), 5, 6, 7);
                _rateType = dbHelper.rateType.GetByName("Hours (Whole)")[0];

                #endregion

                #region  Rate Unit

                if (!dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").Any())
                    dbHelper.rateUnit.CreateRateUnit(_teamId, _businessUnitId, "Per 1 Hour", new DateTime(2020, 1, 1), code, _rateType);
                _rateunitid = dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault();

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-1594

        [TestProperty("JiraIssueID", "ACC-1646")]
        [Description("Test automation for Step 1 to 4 from CDV6-8046: Finance Invoice Batch Setups")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void FinanceInvoiceBatchSetups_UITestMethod001()
        {
            DateTime startDate = DateTime.Now.Date;

            #region Invoice Frequencies

            var _invoiceFrequencyName2 = "Ad Hoc";
            var _invoiceFrequencyId2 = dbHelper.invoiceFrequency.GetByName(_invoiceFrequencyName2)[0];

            #endregion

            #region Rate Units

            var rateunitid = dbHelper.rateUnit.GetByName("Per Day \\ Days")[0];
            var validRateUnits = new List<Guid>();
            validRateUnits.Add(rateunitid);

            #endregion

            #region Service Element 1

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var _serviceElement1IdName = "ACC_1646_1_" + currentDate;
            var _serviceElement1Id = commonMethodsDB.CreateServiceElement1(_serviceElement1IdName, _teamId, new DateTime(2021, 1, 1), code, 1, 2, validRateUnits);

            var code2 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var _serviceElement1IdName2 = "ACC_1646_2_" + currentDate;
            var _serviceElement1Id2 = commonMethodsDB.CreateServiceElement1(_serviceElement1IdName2, _teamId, new DateTime(2021, 1, 1), code2, 1, 2, validRateUnits);

            #endregion

            #region step 1 & 2

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickFinanceProcessingAreaButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            financeInvoiceBatchSetupPage
                .WaitForFinanceInvoiceBatchSetupPageToLoad()
                .ClickNewRecordButton();

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .ClickSaveButton()
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateFinanceInvoiceBatchSetupRecordPageFieldErrorLabelText("Please fill out this field.");

            financeInvoiceBatchSetupRecordPage
                .SelectFinanceModule("Supplier Payments")
                .ClickServiceElement1LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_serviceElement1IdName)
                .TapSearchButton()
                .SelectResultElement(_serviceElement1Id.ToString());

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .ClickPaymentTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_paymentTypeCodeName)
                .TapSearchButton()
                .SelectResultElement(_paymentTypeCodeId.ToString());

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .ClickProviderBatchGroupingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_providerBatchGroupingName)
                .TapSearchButton()
                .SelectResultElement(_providerBatchGroupingId.ToString());

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .InsertStartDate(startDate.ToString("dd'/'MM'/'yyyy"))
                .ClickInvoiceByLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_invoiceByName)
                .TapSearchButton()
                .SelectResultElement(_invoiceById.ToString());

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .InsertCreateBatchWithin("1")
                .ClickInvoiceFrequencyLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_invoiceFrequencyName)
                .TapSearchButton()
                .SelectResultElement(_invoiceFrequencyId.ToString());

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .SelectPayToDayValue("1")
                .ClickExtractNameLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_extractName)
                .TapSearchButton()
                .SelectResultElement(_extractNameId.ToString());

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(2000);

            Guid FinanceInvoiceBatchingSetupId = dbHelper.financeInvoiceBatchSetup.GetByFinanceModule_ServiceElement1_PaymentType_ProviderBatchGrouping(2, _serviceElement1Id, _paymentTypeCodeId, _providerBatchGroupingId).FirstOrDefault();
            var FinanceInvoiceBatchingSetupName = (string)dbHelper.financeInvoiceBatchSetup.GetByID(FinanceInvoiceBatchingSetupId, "name")["name"];

            financeInvoiceBatchSetupPage
                .WaitForFinanceInvoiceBatchSetupPageToLoad()
                .InsertSearchQuery("*" + _serviceElement1IdName.ToString() + "*")
                .TapSearchButton()
                .ValidateRecordIsPresent(FinanceInvoiceBatchingSetupId.ToString(), true);

            #endregion

            #region Step 3

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Finance Invoice Batch Setups")
                .SelectFilter("1", "Service Element 1")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup Records").TypeSearchQuery(_serviceElement1IdName).TapSearchButton().SelectResultElement(_serviceElement1Id.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(FinanceInvoiceBatchingSetupId.ToString());

            advanceSearchPage
                .ClickNewRecordButton_ResultsPage();

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoadFromAdvancedSearch()
                .SelectFinanceModule("Supplier Payments")
                .ClickServiceElement1LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_serviceElement1IdName2)
                .TapSearchButton()
                .SelectResultElement(_serviceElement1Id2.ToString());

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoadFromAdvancedSearch()
                .ClickPaymentTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_paymentTypeCodeName)
                .TapSearchButton()
                .SelectResultElement(_paymentTypeCodeId.ToString());

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoadFromAdvancedSearch()
                .ClickProviderBatchGroupingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_providerBatchGroupingName)
                .TapSearchButton()
                .SelectResultElement(_providerBatchGroupingId.ToString());

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoadFromAdvancedSearch()
                .InsertStartDate(startDate.ToString("dd'/'MM'/'yyyy"))
                .ClickInvoiceByLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_invoiceByName)
                .TapSearchButton()
                .SelectResultElement(_invoiceById.ToString());

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoadFromAdvancedSearch()
                .InsertCreateBatchWithin("1")
                .ClickInvoiceFrequencyLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_invoiceFrequencyName2)
                .TapSearchButton()
                .SelectResultElement(_invoiceFrequencyId2.ToString());

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoadFromAdvancedSearch()
                .SelectPayToDayValue("1")
                .ClickExtractNameLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_extractName)
                .TapSearchButton()
                .SelectResultElement(_extractNameId.ToString());

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoadFromAdvancedSearch()
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(2000);

            var FinanceInvoiceBatchingSetupId2 = dbHelper.financeInvoiceBatchSetup.GetByFinanceModule_ServiceElement1_PaymentType_ProviderBatchGrouping(2, _serviceElement1Id2, _paymentTypeCodeId, _providerBatchGroupingId);
            Assert.AreEqual(1, FinanceInvoiceBatchingSetupId2.Count);

            var FinanceInvoiceBatchId = dbHelper.financeInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(FinanceInvoiceBatchingSetupId2[0]).FirstOrDefault();

            #endregion

            #region Step 4

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToInvoiceBatchesSection();

            financeInvoiceBatchesPage
                .WaitForFinanceInvoiceBatchSetupPageToLoad()
                .InsertSearchQuery("*" + _serviceElement1IdName2.ToString() + "*")
                .TapSearchButton()
                .ValidateRecordIsPresent(FinanceInvoiceBatchId.ToString(), true);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1595

        [TestProperty("JiraIssueID", "ACC-1661")]
        [Description("Test automation for Step 5 to 9 from CDV6-8046: Finance Invoice Batch Setups")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void FinanceInvoiceBatchSetups_UITestMethod002()
        {
            #region Payment Type Code

            var _paymentTypeCodeName1 = "Schedule";
            var _paymentTypeCodeId1 = dbHelper.paymentTypeCode.GetByName(_paymentTypeCodeName1)[0];

            var _paymentTypeCodeName2 = "Invoice";
            var _paymentTypeCodeId2 = dbHelper.paymentTypeCode.GetByName(_paymentTypeCodeName2)[0];

            var _paymentTypeCodeName3 = "No Payments";
            var _paymentTypeCodeId3 = dbHelper.paymentTypeCode.GetByName(_paymentTypeCodeName3)[0];

            #endregion

            #region Provider Batch Grouping

            var _providerBatchGroupingName1 = "Batching Not Applicable";
            var _providerBatchGroupingId1 = dbHelper.providerBatchGrouping.GetByName(_providerBatchGroupingName1)[0];

            #endregion

            #region Debtor Batch Groupings

            var _debtorBatchGroupingName1 = "Batching Not Applicable";
            var _debtorBatchGroupingId1 = dbHelper.debtorBatchGrouping.GetByName(_debtorBatchGroupingName1)[0];

            #endregion

            #region carer Batch Grouping

            var _carerBatchGroupingName1 = "Batching Not Applicable";
            var _carerBatchGroupingId1 = dbHelper.carerBatchGrouping.GetByName(_carerBatchGroupingName1)[0];

            #endregion

            #region Rate Units

            var rateunitid = dbHelper.rateUnit.GetByName("Per Day \\ Days")[0];
            var validRateUnits = new List<Guid>();
            validRateUnits.Add(rateunitid);

            #endregion

            #region Service Element 1

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var _serviceElement1IdName = "ACC_1661_1_" + currentDate;
            var _serviceElement1Id = commonMethodsDB.CreateServiceElement1(_serviceElement1IdName, _teamId, new DateTime(2021, 1, 1), code, 1, 2, validRateUnits);

            #endregion

            #region step 5

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickFinanceProcessingAreaButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            financeInvoiceBatchSetupPage
                .WaitForFinanceInvoiceBatchSetupPageToLoad()
                .ClickNewRecordButton();

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .SelectFinanceModule("Supplier Payments")
                .ClickPaymentTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_paymentTypeCodeName1)
                .TapSearchButton()
                .ValidateResultElementPresent(_paymentTypeCodeId1.ToString())

                .TypeSearchQuery(_paymentTypeCodeName2)
                .TapSearchButton()
                .ValidateResultElementPresent(_paymentTypeCodeId2.ToString())

                .TypeSearchQuery(_paymentTypeCodeName3)
                .TapSearchButton()
                .ValidateResultElementPresent(_paymentTypeCodeId3.ToString())

                .SelectResultElement(_paymentTypeCodeId3.ToString());

            #endregion

            #region Step 6

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .ClickProviderBatchGroupingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_providerBatchGroupingName1)
                .TapSearchButton()
                .ValidateResultElementPresent(_providerBatchGroupingId1.ToString())
                .SelectResultElement(_providerBatchGroupingId1.ToString());

            #endregion

            #region Step 7

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .SelectFinanceModule("Debtors")
                .ClickDebtorBatchGroupingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_debtorBatchGroupingName1)
                .TapSearchButton()
                .ValidateResultElementPresent(_debtorBatchGroupingId1.ToString())
                .SelectResultElement(_debtorBatchGroupingId1.ToString());

            #endregion

            #region Step 8

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .SelectFinanceModule("Carer Payments")
                .ClickCarerBatchGroupingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_carerBatchGroupingName1)
                .TapSearchButton()
                .ValidateResultElementPresent(_carerBatchGroupingId1.ToString())
                .SelectResultElement(_carerBatchGroupingId1.ToString());

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            #endregion

            #region Step 9

            Guid FinanceInvoiceBatchSetupId = dbHelper.financeInvoiceBatchSetup.CreateFinanceInvoiceBatchSetup(_teamId, _businessUnitId, "", _extractNameId, 2, _invoiceById, _invoiceFrequencyId, _paymentTypeCodeId, _providerBatchGroupingId, 3, _serviceElement1Id, new DateTime(2023, 5, 4), true, true, true, 1);

            financeInvoiceBatchSetupPage
                .WaitForFinanceInvoiceBatchSetupPageToLoad()
                .InsertSearchQuery("*" + _serviceElement1IdName.ToString() + "*")
                .TapSearchButton()
                .ValidateRecordIsPresent(FinanceInvoiceBatchSetupId.ToString(), true);

            var FinanceInvoiceBatchId = dbHelper.financeInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(FinanceInvoiceBatchSetupId).FirstOrDefault();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToInvoiceBatchesSection();

            financeInvoiceBatchesPage
                .WaitForFinanceInvoiceBatchSetupPageToLoad()
                .InsertSearchQuery("*" + _serviceElement1IdName.ToString() + "*")
                .TapSearchButton()
                .ValidateRecordIsPresent(FinanceInvoiceBatchId.ToString(), true)
                .ValidateRecordCellContent(FinanceInvoiceBatchId.ToString(), 6, "10/05/2023");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-1662")]
        [Description("Test automation for Step 10 to 13 from CDV6-8046: Finance Invoice Batch Setups")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void FinanceInvoiceBatchSetups_UITestMethod003()
        {
            #region Contribution Type

            var _contributionTypeName_PersonCharge = "Person Charge";
            var _contributionTypeId_PersonCharge = dbHelper.contributionType.GetByName(_contributionTypeName_PersonCharge)[0];

            var _contributionTypeName_PersonChargeDeferred = "Person Charge - Deferred";
            var _contributionTypeId_PersonChargeDeferred = dbHelper.contributionType.GetByName(_contributionTypeName_PersonChargeDeferred)[0];

            var _contributionTypeName_ThirdParty = "Third Party";
            var _contributionTypeId_ThirdParty = dbHelper.contributionType.GetByName(_contributionTypeName_ThirdParty)[0];

            var _contributionTypeName_HealthNursing = "Health - Nursing";
            var _contributionTypeId_HealthNursing = dbHelper.contributionType.GetByName(_contributionTypeName_HealthNursing)[0];

            var _contributionTypeName_HealthOther = "Health - Other";
            var _contributionTypeId_HealthOther = dbHelper.contributionType.GetByName(_contributionTypeName_HealthOther)[0];

            var _contributionTypeName_ILF = "ILF";
            var _contributionTypeId_ILF = dbHelper.contributionType.GetByName(_contributionTypeName_ILF)[0];

            var _contributionTypeName_JointFunded = "Joint Funded";
            var _contributionTypeId_JointFunded = dbHelper.contributionType.GetByName(_contributionTypeName_JointFunded)[0];

            var _contributionTypeName_ThirdParty_Multiple = "Third Party (multiple)";
            var _contributionTypeId_ThirdParty_Multiple = dbHelper.contributionType.GetByName(_contributionTypeName_ThirdParty_Multiple)[0];

            var _contributionTypeName_RecoveryOfOverpaidDirectPayments = "Recovery of overpaid Direct Payments";
            var _contributionTypeId_RecoveryOfOverpaidDirectPayments = dbHelper.contributionType.GetByName(_contributionTypeName_RecoveryOfOverpaidDirectPayments)[0];

            var _contributionTypeName_FirstPartyTopUp = "First Party Top-Up";
            var _contributionTypeId_FirstPartyTopUp = dbHelper.contributionType.GetByName(_contributionTypeName_FirstPartyTopUp)[0];

            var _contributionTypeName_BrokerageFee = "Brokerage Fee";
            var _contributionTypeId_BrokerageFee = dbHelper.contributionType.GetByName(_contributionTypeName_BrokerageFee)[0];

            var _contributionTypeName_PropertyValuationFee = "Property Valuation Fee";
            var _contributionTypeId_PropertyValuationFee = dbHelper.contributionType.GetByName(_contributionTypeName_PropertyValuationFee)[0];

            var _contributionTypeName_NonPropertyValuationFee = "Non-Property Valuation Fee";
            var _contributionTypeId_NonPropertyValuationFee = dbHelper.contributionType.GetByName(_contributionTypeName_NonPropertyValuationFee)[0];

            var _contributionTypeName_AnnualAdministrationCharge = "Annual Administration Charge";
            var _contributionTypeId_AnnualAdministrationCharge = dbHelper.contributionType.GetByName(_contributionTypeName_AnnualAdministrationCharge)[0];

            #endregion

            #region Recovery Method

            var _recoveryMethodName_NetAgainstSuppliersPayment = "Net against Suppliers Payment";
            var _recoveryMethodId_NetAgainstSuppliersPayment = dbHelper.recoveryMethod.GetByName(_recoveryMethodName_NetAgainstSuppliersPayment).FirstOrDefault();

            var _recoveryMethodName_DebtorInvoice = "Debtor Invoice";
            var _recoveryMethodId_DebtorInvoice = dbHelper.recoveryMethod.GetByName(_recoveryMethodName_DebtorInvoice).FirstOrDefault();

            var _recoveryMethodName_ClientMonies = "Client Monies";
            var _recoveryMethodId_ClientMonies = dbHelper.recoveryMethod.GetByName(_recoveryMethodName_ClientMonies).FirstOrDefault();

            var _recoveryMethodName_SwipeCard = "Swipe Card";
            var _recoveryMethodId_SwipeCard = dbHelper.recoveryMethod.GetByName(_recoveryMethodName_SwipeCard).FirstOrDefault();

            var _recoveryMethodName_StandingOrder = "Standing Order";
            var _recoveryMethodId_StandingOrder = dbHelper.recoveryMethod.GetByName(_recoveryMethodName_StandingOrder).FirstOrDefault();

            var _recoveryMethodName_DirectDebit = "Direct Debit";
            var _recoveryMethodId_DirectDebit = dbHelper.recoveryMethod.GetByName(_recoveryMethodName_DirectDebit).FirstOrDefault();

            var _recoveryMethodName_ExternalRecovery = "External Recovery";
            var _recoveryMethodId_ExternalRecovery = dbHelper.recoveryMethod.GetByName(_recoveryMethodName_ExternalRecovery).FirstOrDefault();

            var _recoveryMethodName_DebtorInvoice_DirectDebit = "Debtor Invoice (Direct Debit)";
            var _recoveryMethodId_DebtorInvoice_DirectDebit = dbHelper.recoveryMethod.GetByName(_recoveryMethodName_DebtorInvoice_DirectDebit).FirstOrDefault();

            var _recoveryMethodName_ForInformationOnly = "For Information Only";
            var _recoveryMethodId_ForInformationOnly = dbHelper.recoveryMethod.GetByName(_recoveryMethodName_ForInformationOnly).FirstOrDefault();

            #endregion

            #region Invoice By

            var _invoiceByName_Provider = "Provider";
            var _invoiceById_Provider = dbHelper.invoiceBy.GetByName(_invoiceByName_Provider)[0];

            var _invoiceByName_ProviderClient = "Provider\\Client";
            var _invoiceById_ProviderClient = dbHelper.invoiceBy.GetByName(_invoiceByName_ProviderClient)[0];

            var _invoiceByName_ProviderPurchasingTeam = "Provider\\Purchasing Team";
            var _invoiceById_ProviderPurchasingTeam = dbHelper.invoiceBy.GetByName(_invoiceByName_ProviderPurchasingTeam)[0];

            var _invoiceByName_ProviderPurchasingTeamClient = "Provider\\Purchasing Team\\Client";
            var _invoiceById_ProviderPurchasingTeamClient = dbHelper.invoiceBy.GetByName(_invoiceByName_ProviderPurchasingTeamClient)[0];

            var _invoiceByName_Payee = "Payee";
            var _invoiceById_Payee = dbHelper.invoiceBy.GetByName(_invoiceByName_Payee)[0];

            var _invoiceByName_PayeeAssessment = "Payee\\Assessment";
            var _invoiceById_PayeeAssessment = dbHelper.invoiceBy.GetByName(_invoiceByName_PayeeAssessment)[0];

            #endregion

            #region Invoice Frequency

            var _invoiceFrequencyName_EveryWeek = "Every Week";
            var _invoiceFrequencyId_EveryWeek = dbHelper.invoiceFrequency.GetByName(_invoiceFrequencyName_EveryWeek)[0];

            var _invoiceFrequencyName_Every2Weeks = "Every 2 Weeks";
            var _invoiceFrequencyId_Every2Weeks = dbHelper.invoiceFrequency.GetByName(_invoiceFrequencyName_Every2Weeks)[0];

            var _invoiceFrequencyName_Every4Weeks = "Every 4 Weeks";
            var _invoiceFrequencyId_Every4Weeks = dbHelper.invoiceFrequency.GetByName(_invoiceFrequencyName_Every4Weeks)[0];

            var _invoiceFrequencyName_EveryCalendarMonth = "Every Calendar Month";
            var _invoiceFrequencyId_EveryCalendarMonth = dbHelper.invoiceFrequency.GetByName(_invoiceFrequencyName_EveryCalendarMonth)[0];

            var _invoiceFrequencyName_Every3CalendarMonths = "Every 3 Calendar Months";
            var _invoiceFrequencyId_Every3CalendarMonths = dbHelper.invoiceFrequency.GetByName(_invoiceFrequencyName_Every3CalendarMonths)[0];

            var _invoiceFrequencyName_EveryCalendarYear = "Every Calendar Year";
            var _invoiceFrequencyId_EveryCalendarYear = dbHelper.invoiceFrequency.GetByName(_invoiceFrequencyName_EveryCalendarYear)[0];

            var _invoiceFrequencyName_AdHoc = "Ad Hoc";
            var _invoiceFrequencyId_AdHoc = dbHelper.invoiceFrequency.GetByName(_invoiceFrequencyName_AdHoc)[0];

            var _invoiceFrequencyName_LastPayUpToDayOfEveryMonth = "Last pay-up-to-day of Every Month";
            var _invoiceFrequencyId_LastPayUpToDayOfEveryMonth = dbHelper.invoiceFrequency.GetByName(_invoiceFrequencyName_LastPayUpToDayOfEveryMonth)[0];

            #endregion

            #region step 10

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickFinanceProcessingAreaButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            financeInvoiceBatchSetupPage
                .WaitForFinanceInvoiceBatchSetupPageToLoad()
                .ClickNewRecordButton();

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .SelectFinanceModule("Debtors")
                .ClickContributionTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_contributionTypeName_PersonCharge)
                .TapSearchButton()
                .ValidateResultElementPresent(_contributionTypeId_PersonCharge.ToString())

                .TypeSearchQuery(_contributionTypeName_PersonChargeDeferred)
                .TapSearchButton()
                .ValidateResultElementPresent(_contributionTypeId_PersonChargeDeferred.ToString())

                .TypeSearchQuery(_contributionTypeName_ThirdParty)
                .TapSearchButton()
                .ValidateResultElementPresent(_contributionTypeId_ThirdParty.ToString())

                .TypeSearchQuery(_contributionTypeName_HealthNursing)
                .TapSearchButton()
                .ValidateResultElementPresent(_contributionTypeId_HealthNursing.ToString())

                .TypeSearchQuery(_contributionTypeName_HealthOther)
                .TapSearchButton()
                .ValidateResultElementPresent(_contributionTypeId_HealthOther.ToString())

                .TypeSearchQuery(_contributionTypeName_ILF)
                .TapSearchButton()
                .ValidateResultElementPresent(_contributionTypeId_ILF.ToString())

                .TypeSearchQuery(_contributionTypeName_JointFunded)
                .TapSearchButton()
                .ValidateResultElementPresent(_contributionTypeId_JointFunded.ToString())

                .TypeSearchQuery(_contributionTypeName_ThirdParty_Multiple)
                .TapSearchButton()
                .ValidateResultElementPresent(_contributionTypeId_ThirdParty_Multiple.ToString())

                .TypeSearchQuery(_contributionTypeName_RecoveryOfOverpaidDirectPayments)
                .TapSearchButton()
                .ValidateResultElementPresent(_contributionTypeId_RecoveryOfOverpaidDirectPayments.ToString())

                .TypeSearchQuery(_contributionTypeName_FirstPartyTopUp)
                .TapSearchButton()
                .ValidateResultElementPresent(_contributionTypeId_FirstPartyTopUp.ToString())

                .TypeSearchQuery(_contributionTypeName_BrokerageFee)
                .TapSearchButton()
                .ValidateResultElementPresent(_contributionTypeId_BrokerageFee.ToString())

                .TypeSearchQuery(_contributionTypeName_PropertyValuationFee)
                .TapSearchButton()
                .ValidateResultElementPresent(_contributionTypeId_PropertyValuationFee.ToString())

                .TypeSearchQuery(_contributionTypeName_NonPropertyValuationFee)
                .TapSearchButton()
                .ValidateResultElementPresent(_contributionTypeId_NonPropertyValuationFee.ToString())

                .TypeSearchQuery(_contributionTypeName_AnnualAdministrationCharge)
                .TapSearchButton()
                .ValidateResultElementPresent(_contributionTypeId_AnnualAdministrationCharge.ToString())

                .ClickCloseButton();

            #endregion

            #region Step 11

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .ClickRecoveryMethodLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_recoveryMethodName_NetAgainstSuppliersPayment)
                .TapSearchButton()
                .ValidateResultElementPresent(_recoveryMethodId_NetAgainstSuppliersPayment.ToString())

                .TypeSearchQuery(_recoveryMethodName_DebtorInvoice)
                .TapSearchButton()
                .ValidateResultElementPresent(_recoveryMethodId_DebtorInvoice.ToString())

                .TypeSearchQuery(_recoveryMethodName_ClientMonies)
                .TapSearchButton()
                .ValidateResultElementPresent(_recoveryMethodId_ClientMonies.ToString())

                .TypeSearchQuery(_recoveryMethodName_SwipeCard)
                .TapSearchButton()
                .ValidateResultElementPresent(_recoveryMethodId_SwipeCard.ToString())

                .TypeSearchQuery(_recoveryMethodName_StandingOrder)
                .TapSearchButton()
                .ValidateResultElementPresent(_recoveryMethodId_StandingOrder.ToString())

                .TypeSearchQuery(_recoveryMethodName_DirectDebit)
                .TapSearchButton()
                .ValidateResultElementPresent(_recoveryMethodId_DirectDebit.ToString())

                .TypeSearchQuery(_recoveryMethodName_ExternalRecovery)
                .TapSearchButton()
                .ValidateResultElementPresent(_recoveryMethodId_ExternalRecovery.ToString())

                .TypeSearchQuery(_recoveryMethodName_DebtorInvoice_DirectDebit)
                .TapSearchButton()
                .ValidateResultElementPresent(_recoveryMethodId_DebtorInvoice_DirectDebit.ToString())

                .TypeSearchQuery(_recoveryMethodName_ForInformationOnly)
                .TapSearchButton()
                .ValidateResultElementPresent(_recoveryMethodId_ForInformationOnly.ToString())

                .ClickCloseButton();

            #endregion

            #region Step 13

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .ClickInvoiceFrequencyLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_invoiceFrequencyName_EveryWeek)
                .TapSearchButton()
                .ValidateResultElementPresent(_invoiceFrequencyId_EveryWeek.ToString())

                .TypeSearchQuery(_invoiceFrequencyName_Every2Weeks)
                .TapSearchButton()
                .ValidateResultElementPresent(_invoiceFrequencyId_Every2Weeks.ToString())

                .TypeSearchQuery(_invoiceFrequencyName_Every4Weeks)
                .TapSearchButton()
                .ValidateResultElementPresent(_invoiceFrequencyId_Every4Weeks.ToString())

                .TypeSearchQuery(_invoiceFrequencyName_EveryCalendarMonth)
                .TapSearchButton()
                .ValidateResultElementPresent(_invoiceFrequencyId_EveryCalendarMonth.ToString())

                .TypeSearchQuery(_invoiceFrequencyName_Every3CalendarMonths)
                .TapSearchButton()
                .ValidateResultElementPresent(_invoiceFrequencyId_Every3CalendarMonths.ToString())

                .TypeSearchQuery(_invoiceFrequencyName_EveryCalendarYear)
                .TapSearchButton()
                .ValidateResultElementPresent(_invoiceFrequencyId_EveryCalendarYear.ToString())

                .TypeSearchQuery(_invoiceFrequencyName_AdHoc)
                .TapSearchButton()
                .ValidateResultElementPresent(_invoiceFrequencyId_AdHoc.ToString())

                .TypeSearchQuery(_invoiceFrequencyName_LastPayUpToDayOfEveryMonth)
                .TapSearchButton()
                .ValidateResultElementPresent(_invoiceFrequencyId_LastPayUpToDayOfEveryMonth.ToString())

                .ClickCloseButton();

            #endregion

            #region Step 12

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .ClickInvoiceByLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_invoiceByName_Payee)
                .TapSearchButton()
                .ValidateResultElementPresent(_invoiceById_Payee.ToString())

                .TypeSearchQuery("*Assessment*")
                .TapSearchButton()
                .ValidateResultElementPresent(_invoiceById_PayeeAssessment.ToString())

                .ClickCloseButton();

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .SelectFinanceModule("Supplier Payments")
                .ClickInvoiceByLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_invoiceByName_Provider)
                .TapSearchButton()
                .ValidateResultElementPresent(_invoiceById_Provider.ToString())

                .TypeSearchQuery("*Client*")
                .TapSearchButton()
                .ValidateResultElementPresent(_invoiceById_ProviderClient.ToString())

                .TypeSearchQuery("*Purchasing Team*")
                .TapSearchButton()
                .ValidateResultElementPresent(_invoiceById_ProviderPurchasingTeam.ToString())
                .ValidateResultElementPresent(_invoiceById_ProviderPurchasingTeamClient.ToString())

                .ClickCloseButton();

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1596

        [TestProperty("JiraIssueID", "ACC-1675")]
        [Description("Test automation for Step 14 to 17 from CDV6-8046: Finance Invoice Batch Setups")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void FinanceInvoiceBatchSetups_UITestMethod004()
        {
            #region Extract Name

            var _extractName_GenericSupplierPaymentsExtract = "Generic Supplier Payments Extract";
            var _extractNameId_GenericSupplierPaymentsExtract = dbHelper.extractName.GetByName(_extractName_GenericSupplierPaymentsExtract).FirstOrDefault();

            var _extractName_GenericDebtorsExtract = "Generic Debtors Extract";
            var _extractNameId_GenericDebtorsExtract = dbHelper.extractName.GetByName(_extractName_GenericDebtorsExtract).FirstOrDefault();

            var _extractName_GenericCarerPaymentsExtract = "Generic Carer Payments Extract";
            var _extractNameId_GenericCarerPaymentsExtract = dbHelper.extractName.GetByName(_extractName_GenericCarerPaymentsExtract).FirstOrDefault();

            #endregion

            #region Debtor Header Text

            var _debtorHeaderTextName_1 = "Residential Care Charges for {Client name} at {provider on service provision} for the period {invoice to LESS frequency} to {invoice to}";
            var _debtorHeaderTextId_1 = dbHelper.debtorHeaderText.GetByName(_debtorHeaderTextName_1).FirstOrDefault();

            var _debtorHeaderTextName_2 = "Non-Residential Care Charges for {Client name} for the period {invoice to LESS frequency} to {invoice to}";
            var _debtorHeaderTextId_2 = dbHelper.debtorHeaderText.GetByName(_debtorHeaderTextName_2).FirstOrDefault();

            var _debtorHeaderTextName_3 = "Meal Charges for {Client name} for the period {invoice to LESS frequency} to {invoice to}";
            var _debtorHeaderTextId_3 = dbHelper.debtorHeaderText.GetByName(_debtorHeaderTextName_3).FirstOrDefault();

            var _debtorHeaderTextName_4 = "Transport Charges for {Client name} for the period {invoice to LESS frequency} to {invoice to}";
            var _debtorHeaderTextId_4 = dbHelper.debtorHeaderText.GetByName(_debtorHeaderTextName_4).FirstOrDefault();

            var _debtorHeaderTextName_5 = "Parental Contributions for {Client name} for the period {invoice to LESS frequency} to {invoice to}";
            var _debtorHeaderTextId_5 = dbHelper.debtorHeaderText.GetByName(_debtorHeaderTextName_5).FirstOrDefault();

            var _debtorHeaderTextName_6 = "Property Charge under Deferred Payments scheme for {Client name} at {Provider on Service Provision} for the period up to {invoice to}";
            var _debtorHeaderTextId_6 = dbHelper.debtorHeaderText.GetByName(_debtorHeaderTextName_6).FirstOrDefault();

            var _debtorHeaderTextName_7 = "Third Party Contributions towards Residential Care for {Client Name} at {Provider on Service Provision} for the period {invoice to LESS frequency} to {invoice to}";
            var _debtorHeaderTextId_7 = dbHelper.debtorHeaderText.GetByName(_debtorHeaderTextName_7).FirstOrDefault();

            var _debtorHeaderTextName_8 = "Recovery of Nursing Care costs for the period {invoice to LESS frequency} to {invoice to}";
            var _debtorHeaderTextId_8 = dbHelper.debtorHeaderText.GetByName(_debtorHeaderTextName_8).FirstOrDefault();

            var _debtorHeaderTextName_9 = "Recovery of Contributions towards Cost of care for the period {invoice to LESS frequency} to {invoice to}";
            var _debtorHeaderTextId_9 = dbHelper.debtorHeaderText.GetByName(_debtorHeaderTextName_9).FirstOrDefault();

            var _debtorHeaderTextName_10 = "Charges for the period {invoice to LESS frequency} to {invoice to}";
            var _debtorHeaderTextId_10 = dbHelper.debtorHeaderText.GetByName(_debtorHeaderTextName_10).FirstOrDefault();

            var _debtorHeaderTextName_11 = "Non-Residential Care Charges for {Client name} for the period {invoice to LESS frequency} to {invoice to} at the Lower of Actual costs or your Maximum Assessed charge";
            var _debtorHeaderTextId_11 = dbHelper.debtorHeaderText.GetByName(_debtorHeaderTextName_11).FirstOrDefault();

            #endregion

            #region Debtor Transaction Text

            var _debtorTransactionTextName_1 = "Residential Care Charges for the period {transaction start date} to {transaction end date} at {charge value} per week";
            var _debtorTransactionTextId_1 = dbHelper.debtorTransactionText.GetByName(_debtorTransactionTextName_1).FirstOrDefault();

            var _debtorTransactionTextName_2 = "Non-Residential Care Charges for the period {transaction start date} to {transaction end date} at the Lower of Actual costs or your Maximum Assessed charge of {charge value} per week";
            var _debtorTransactionTextId_2 = dbHelper.debtorTransactionText.GetByName(_debtorTransactionTextName_2).FirstOrDefault();

            var _debtorTransactionTextName_3 = "Meal Charges for the period {transaction start date} to {transaction end date} at {charge value} per week";
            var _debtorTransactionTextId_3 = dbHelper.debtorTransactionText.GetByName(_debtorTransactionTextName_3).FirstOrDefault();

            var _debtorTransactionTextName_4 = "Transport Charges for the period {transaction start date} to {transaction end date} at {charge value} per week";
            var _debtorTransactionTextId_4 = dbHelper.debtorTransactionText.GetByName(_debtorTransactionTextName_4).FirstOrDefault();

            var _debtorTransactionTextName_5 = "Parental Contributions for the period {transaction start date} to {transaction end date} at {charge value} per week";
            var _debtorTransactionTextId_5 = dbHelper.debtorTransactionText.GetByName(_debtorTransactionTextName_5).FirstOrDefault();

            var _debtorTransactionTextName_6 = "Property Charge under Deferred Payments scheme for the period {transaction start date} to {transaction end date} at {charge value} per week";
            var _debtorTransactionTextId_6 = dbHelper.debtorTransactionText.GetByName(_debtorTransactionTextName_6).FirstOrDefault();

            var _debtorTransactionTextName_7 = "Third Party Contribution towards Residential Care for the period {transaction start date} to {transaction end date} at {charge value} per week";
            var _debtorTransactionTextId_7 = dbHelper.debtorTransactionText.GetByName(_debtorTransactionTextName_7).FirstOrDefault();

            var _debtorTransactionTextName_8 = "Recovery of Nursing Care costs for {Client name} at {Provider on Service Provision} for the period {transaction start date} to {transaction end date} at {charge value} per week";
            var _debtorTransactionTextId_8 = dbHelper.debtorTransactionText.GetByName(_debtorTransactionTextName_8).FirstOrDefault();

            var _debtorTransactionTextName_9 = "Recovery of Contributions towards Cost of care for {Client name} at {Supplier name on Service Placement} for the period {transaction start date} to {transaction end date} at {charge value} per week";
            var _debtorTransactionTextId_9 = dbHelper.debtorTransactionText.GetByName(_debtorTransactionTextName_9).FirstOrDefault();

            var _debtorTransactionTextName_11 = "Non-Residential Care Charges for the period {transaction start date} to {transaction end date}";
            var _debtorTransactionTextId_11 = dbHelper.debtorTransactionText.GetByName(_debtorTransactionTextName_11).FirstOrDefault();

            #endregion

            #region Debtor Recovery Text

            var _debtorRecoveryTextName_1 = "Adjustment of previously invoiced amount for the period {transaction start date} to {transaction end date}";
            var _debtorRecoveryTextId_1 = dbHelper.debtorRecoveryText.GetByName(_debtorRecoveryTextName_1).FirstOrDefault();

            var _debtorRecoveryTextName_2 = "Adjustment of previously invoiced amount for {Client name} at {Supplier name on Service Placement} for the period {transaction start date} to {transaction end date}";
            var _debtorRecoveryTextId_2 = dbHelper.debtorRecoveryText.GetByName(_debtorRecoveryTextName_2).FirstOrDefault();

            var _debtorRecoveryTextName_3 = "Adjustment of previously invoiced amount for {Client name} for the period {transaction start date} to {transaction end date}";
            var _debtorRecoveryTextId_3 = dbHelper.debtorRecoveryText.GetByName(_debtorRecoveryTextName_3).FirstOrDefault();

            #endregion

            #region step 14

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickFinanceProcessingAreaButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            financeInvoiceBatchSetupPage
                .WaitForFinanceInvoiceBatchSetupPageToLoad()
                .ClickNewRecordButton();

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .SelectFinanceModule("Supplier Payments")
                .ClickExtractNameLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_extractName_GenericSupplierPaymentsExtract)
                .TapSearchButton()
                .ValidateResultElementPresent(_extractNameId_GenericSupplierPaymentsExtract.ToString())
                .ClickCloseButton();

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .SelectFinanceModule("Carer Payments")
                .ClickExtractNameLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_extractName_GenericCarerPaymentsExtract)
                .TapSearchButton()
                .ValidateResultElementPresent(_extractNameId_GenericCarerPaymentsExtract.ToString())
                .ClickCloseButton();

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .SelectFinanceModule("Debtors")
                .ClickExtractNameLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_extractName_GenericDebtorsExtract)
                .TapSearchButton()
                .ValidateResultElementPresent(_extractNameId_GenericDebtorsExtract.ToString())
                .ClickCloseButton();

            #endregion

            #region Step 15

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .ClickDebtorHeaderTextLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_debtorHeaderTextName_1)
                .TapSearchButton()
                .ValidateResultElementPresent(_debtorHeaderTextId_1.ToString())

                .TypeSearchQuery(_debtorHeaderTextName_2)
                .TapSearchButton()
                .ValidateResultElementPresent(_debtorHeaderTextId_2.ToString())

                .TypeSearchQuery(_debtorHeaderTextName_3)
                .TapSearchButton()
                .ValidateResultElementPresent(_debtorHeaderTextId_3.ToString())

                .TypeSearchQuery(_debtorHeaderTextName_4)
                .TapSearchButton()
                .ValidateResultElementPresent(_debtorHeaderTextId_4.ToString())

                .TypeSearchQuery(_debtorHeaderTextName_5)
                .TapSearchButton()
                .ValidateResultElementPresent(_debtorHeaderTextId_5.ToString())

                .TypeSearchQuery(_debtorHeaderTextName_6)
                .TapSearchButton()
                .ValidateResultElementPresent(_debtorHeaderTextId_6.ToString())

                .TypeSearchQuery(_debtorHeaderTextName_7)
                .TapSearchButton()
                .ValidateResultElementPresent(_debtorHeaderTextId_7.ToString())

                .TypeSearchQuery(_debtorHeaderTextName_8)
                .TapSearchButton()
                .ValidateResultElementPresent(_debtorHeaderTextId_8.ToString())

                .TypeSearchQuery(_debtorHeaderTextName_9)
                .TapSearchButton()
                .ValidateResultElementPresent(_debtorHeaderTextId_9.ToString())

                .TypeSearchQuery(_debtorHeaderTextName_10)
                .TapSearchButton()
                .ValidateResultElementPresent(_debtorHeaderTextId_10.ToString())

                .TypeSearchQuery(_debtorHeaderTextName_11)
                .TapSearchButton()
                .ValidateResultElementPresent(_debtorHeaderTextId_11.ToString())

                .ClickCloseButton();

            #endregion

            #region Step 16

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .ClickDebtorTransactionTextLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_debtorTransactionTextName_1)
                .TapSearchButton()
                .ValidateResultElementPresent(_debtorTransactionTextId_1.ToString())

                .TypeSearchQuery(_debtorTransactionTextName_2)
                .TapSearchButton()
                .ValidateResultElementPresent(_debtorTransactionTextId_2.ToString())

                .TypeSearchQuery(_debtorTransactionTextName_3)
                .TapSearchButton()
                .ValidateResultElementPresent(_debtorTransactionTextId_3.ToString())

                .TypeSearchQuery(_debtorTransactionTextName_4)
                .TapSearchButton()
                .ValidateResultElementPresent(_debtorTransactionTextId_4.ToString())

                .TypeSearchQuery(_debtorTransactionTextName_5)
                .TapSearchButton()
                .ValidateResultElementPresent(_debtorTransactionTextId_5.ToString())

                .TypeSearchQuery(_debtorTransactionTextName_6)
                .TapSearchButton()
                .ValidateResultElementPresent(_debtorTransactionTextId_6.ToString())

                .TypeSearchQuery(_debtorTransactionTextName_7)
                .TapSearchButton()
                .ValidateResultElementPresent(_debtorTransactionTextId_7.ToString())

                .TypeSearchQuery(_debtorTransactionTextName_8)
                .TapSearchButton()
                .ValidateResultElementPresent(_debtorTransactionTextId_8.ToString())

                .TypeSearchQuery(_debtorTransactionTextName_9)
                .TapSearchButton()
                .ValidateResultElementPresent(_debtorTransactionTextId_9.ToString())

                .TypeSearchQuery(_debtorTransactionTextName_11)
                .TapSearchButton()
                .ValidateResultElementPresent(_debtorTransactionTextId_11.ToString())

                .ClickCloseButton();

            #endregion

            #region Step 17

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .ClickDebtorRecoveryTextLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_debtorRecoveryTextName_1)
                .TapSearchButton()
                .ValidateResultElementPresent(_debtorRecoveryTextId_1.ToString())

                .TypeSearchQuery(_debtorRecoveryTextName_2)
                .TapSearchButton()
                .ValidateResultElementPresent(_debtorRecoveryTextId_2.ToString())

                .TypeSearchQuery(_debtorRecoveryTextName_3)
                .TapSearchButton()
                .ValidateResultElementPresent(_debtorRecoveryTextId_3.ToString())

                .ClickCloseButton();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-1685")]
        [Description("Test automation for Step 18 to 19 from CDV6-8046: Finance Invoice Batch Setups")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void FinanceInvoiceBatchSetups_UITestMethod005()
        {
            #region Extract Name

            var _extractName = "Generic Debtors Extract";
            var _extractNameId = dbHelper.extractName.GetByName(_extractName).FirstOrDefault();

            #endregion

            #region Debtor Header Text

            var _debtorHeaderTextName = "Residential Care Charges for {Client name} at {provider on service provision} for the period {invoice to LESS frequency} to {invoice to}";
            var _debtorHeaderTextId = dbHelper.debtorHeaderText.GetByName(_debtorHeaderTextName).FirstOrDefault();

            #endregion

            #region Debtor Transaction Text

            var _debtorTransactionTextName = "Residential Care Charges for the period {transaction start date} to {transaction end date} at {charge value} per week";
            var _debtorTransactionTextId = dbHelper.debtorTransactionText.GetByName(_debtorTransactionTextName).FirstOrDefault();

            #endregion

            #region Debtor Recovery Text

            var _debtorRecoveryTextName = "Adjustment of previously invoiced amount for the period {transaction start date} to {transaction end date}";
            var _debtorRecoveryTextId = dbHelper.debtorRecoveryText.GetByName(_debtorRecoveryTextName).FirstOrDefault();

            #endregion

            #region Rule Type

            var _chargingRuleTypeName = "Rule Type_" + currentDate;
            var _chargingRuleTypeId = dbHelper.chargingRuleType.CreateChargingRuleType(_chargingRuleTypeName, _teamId, new DateTime(2020, 1, 1));

            #endregion

            #region Contribution Type

            var _contributionTypeName = "Health - Other";
            var _contributionTypeId = dbHelper.contributionType.GetByName(_contributionTypeName)[0];

            #endregion

            #region Recovery Method

            var _recoveryMethodName = "Debtor Invoice";
            var _recoveryMethodId = dbHelper.recoveryMethod.GetByName(_recoveryMethodName).FirstOrDefault();

            #endregion

            #region Debtor Batch Grouping Name

            var _debtorBatchGroupingName = "Batching Not Applicable";
            var _debtorBatchGroupingId = dbHelper.debtorBatchGrouping.GetByName(_debtorBatchGroupingName)[0];

            #endregion

            #region Invoice By

            var _invoiceByName = "Payee";
            var _invoiceById = dbHelper.invoiceBy.GetByName(_invoiceByName)[0];

            #endregion

            #region Invoice Frequency

            var _invoiceFrequencyName = "Every Week";
            var _invoiceFrequencyId = dbHelper.invoiceFrequency.GetByName(_invoiceFrequencyName)[0];

            #endregion

            #region Finance Invoice Batch Setup

            Guid FinanceInvoiceBatchSetupId = dbHelper.financeInvoiceBatchSetup.CreateFinanceInvoiceBatchSetup(_teamId, _businessUnitId, 3, _chargingRuleTypeId,
                _contributionTypeId, _recoveryMethodId, _debtorBatchGroupingId, new DateTime(2023, 5, 4), "01:00", _invoiceById,
                1, _invoiceFrequencyId, 3, _extractNameId, _debtorHeaderTextId, _debtorTransactionTextId, _debtorRecoveryTextId,
                true, true, true, true, true);

            #endregion

            #region step 18

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickFinanceProcessingAreaButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            financeInvoiceBatchSetupPage
                .WaitForFinanceInvoiceBatchSetupPageToLoad()
                .InsertSearchQuery("*" + _chargingRuleTypeName.ToString() + "*")
                .TapSearchButton()
                .ValidateRecordIsPresent(FinanceInvoiceBatchSetupId.ToString(), true)
                .OpenRecord(FinanceInvoiceBatchSetupId.ToString());

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .ValidateFinanceModuleIsDisabled(true)
                .ValidateCreateFinanceInvoiceBatchOptionsIsDisabled(false)
                .ValidatePermitAuthorisationOfCreditInvoicesOptionsIsDisabled(true)
                .ValidateDebtorReferenceNumberRequiredOptionsIsDisabled(false)
                .ValidateApplyFinanceTransactionsToEarliestFinanceInvoiceOptionsIsDisabled(false)
                .ValidateApplySuspensionsOptionsIsDisabled(false)

                .ValidateRuleTypeLookupButtonIsDisabled(true)
                .ValidateContributionTypeLookupButtonIsDisabled(true)
                .ValidateRecoveryMethodLookupButtonIsDisabled(true)
                .ValidateDebtorBatchGroupingLookupButtonIsDisabled(true)

                .ValidateStartDateFieldIsDisabled(true)
                .ValidateStartTimeFieldIsDisabled(false)

                .ValidateInvoiceByLookupButtonIsDisabled(true)
                .ValidateCreateBatchWithinFieldIsDisabled(true)
                .ValidateInvoiceFrequencyLookupButtonIsDisabled(true)
                .ValidatePayToDayIsDisabled(true)
                .ValidateSeparateInvoicesOptionsIsDisabled(true)
                .ValidateExtractNameLookupButtonIsDisabled(true)

                .ValidateDebtorHeaderTextLookupButtonIsDisabled(false)
                .ValidateDebtorTransactionTextLookupButtonIsDisabled(false)
                .ValidateDebtorRecoveryTextLookupButtonIsDisabled(false)

                .ValidateResponsibleTeamLookupButtonIsDisabled(true)
                .ValidateAssignRecordButtonIsVisible()
                .ClickBackButton();

            #endregion

            #region Step 19

            financeInvoiceBatchSetupPage
                .WaitForFinanceInvoiceBatchSetupPageToLoad()
                .ClickNewRecordButton();

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .SelectFinanceModule("Debtors")
                .ClickRuleTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_chargingRuleTypeName)
                .TapSearchButton()
                .SelectResultElement(_chargingRuleTypeId.ToString());

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .ClickContributionTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_contributionTypeName)
                .TapSearchButton()
                .SelectResultElement(_contributionTypeId.ToString());

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .ClickRecoveryMethodLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_recoveryMethodName)
                .TapSearchButton()
                .SelectResultElement(_recoveryMethodId.ToString());

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .ClickDebtorBatchGroupingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_debtorBatchGroupingName)
                .TapSearchButton()
                .SelectResultElement(_debtorBatchGroupingId.ToString());

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .InsertStartDate("04/05/2023")
                .InsertStartTime("01:00")
                .ClickInvoiceByLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_invoiceByName)
                .TapSearchButton()
                .SelectResultElement(_invoiceById.ToString());

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .InsertCreateBatchWithin("1")
                .ClickInvoiceFrequencyLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_invoiceFrequencyName)
                .TapSearchButton()
                .SelectResultElement(_invoiceFrequencyId.ToString());

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .SelectPayToDayValue("3")
                .ClickExtractNameLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_extractName)
                .TapSearchButton()
                .SelectResultElement(_extractNameId.ToString());

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("There is already a Finance Invoice Batch record using the Invoice Batch Grouping selections. Please correct as necessary")
                .TapCloseButton();

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1690

        [TestProperty("JiraIssueID", "ACC-1692")]
        [Description("Test automation for Step 20 to 25 from CDV6-8046: Finance Invoice Batch Setups")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void FinanceInvoiceBatchSetups_UITestMethod006()
        {
            #region Rate Units

            var rateunitid = dbHelper.rateUnit.GetByName("Per Day \\ Days")[0];
            var validRateUnits = new List<Guid>();
            validRateUnits.Add(rateunitid);

            #endregion

            #region Service Element 1

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var _serviceElement1IdName = "ACC_1692_1_" + currentDate;
            var _serviceElement1Id = commonMethodsDB.CreateServiceElement1(_serviceElement1IdName, _teamId, new DateTime(2021, 1, 1), code, 1, 2, validRateUnits);

            #endregion

            #region Step 20, 21 & 22

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickFinanceProcessingAreaButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            financeInvoiceBatchSetupPage
                .WaitForFinanceInvoiceBatchSetupPageToLoad()
                .ClickNewRecordButton();

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .SelectFinanceModule("Supplier Payments")
                .ClickSaveButton();

            financeInvoiceBatchSetupRecordPage
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateServiceElement1FieldErrorLabelText("Please fill out this field.")
                .ValidatePaymentTypeFieldErrorLabelText("Please fill out this field.")
                .ValidateProviderBatchGroupingFieldErrorLabelText("Please fill out this field.")
                .ValidateStartDateFieldErrorLabelText("Please fill out this field.")
                .ValidateInvoiceByFieldErrorLabelText("Please fill out this field.")
                .ValidateCreateBatchWithinFieldErrorLabelText("Please fill out this field.")
                .ValidateInvoiceFrequencyFieldErrorLabelText("Please fill out this field.")
                .ValidatePayToDayFieldErrorLabelText("Please fill out this field.")
                .ValidateExtractNameFieldErrorLabelText("Please fill out this field.")

                .ClickBackButton();

            #endregion

            // Step 23 & 24 already covered in step 19 & 18
            #region Step 25

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            System.Threading.Thread.Sleep(1000);

            Guid FinanceInvoiceBatchSetupId = dbHelper.financeInvoiceBatchSetup.CreateFinanceInvoiceBatchSetup(_teamId, _businessUnitId, "", _extractNameId, 2, _invoiceById, _invoiceFrequencyId, _paymentTypeCodeId, _providerBatchGroupingId, 3, _serviceElement1Id, new DateTime(2023, 5, 4), true, true, true, 1);
            var FinanceInvoiceBatchId = dbHelper.financeInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(FinanceInvoiceBatchSetupId);
            Assert.AreEqual(1, FinanceInvoiceBatchId.Count);

            financeInvoiceBatchSetupPage
                .WaitForFinanceInvoiceBatchSetupPageToLoad()
                .InsertSearchQuery("*" + _serviceElement1IdName.ToString() + "*")
                .TapSearchButton()
                .ValidateRecordIsPresent(FinanceInvoiceBatchSetupId.ToString(), true)
                .OpenRecord(FinanceInvoiceBatchSetupId.ToString());

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            System.Threading.Thread.Sleep(2000);

            var FinanceInvoiceBatchingSetupCount = dbHelper.financeInvoiceBatchSetup.GetByFinanceModule_ServiceElement1_PaymentType_ProviderBatchGrouping(2, _serviceElement1Id, _paymentTypeCodeId, _providerBatchGroupingId);
            FinanceInvoiceBatchId = dbHelper.financeInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(FinanceInvoiceBatchSetupId);

            Assert.AreEqual(0, FinanceInvoiceBatchingSetupCount.Count);
            Assert.AreEqual(0, FinanceInvoiceBatchId.Count);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1821

        [TestProperty("JiraIssueID", "ACC-1837")]
        [Description("Test automation for Step 26 to 32 from CDV6-8046: Finance Invoice Batch Setups")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void FinanceInvoiceBatchSetups_UITestMethod007()
        {
            #region Step 26 to 28

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickFinanceProcessingAreaButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            financeInvoiceBatchSetupPage
                .WaitForFinanceInvoiceBatchSetupPageToLoad()
                .ClickNewRecordButton();

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .SelectFinanceModule("Supplier Payments")
                .ValidateCreateFinanceTransactionsFieldOptionsDisplayed();

            #endregion

            #region Step 29 & 30

            financeInvoiceBatchSetupRecordPage
                .ValidateCreateFinanceTransactionsYesOptionChecked(true)

                .ValidateCreateFinanceInvoiceBatchFieldOptionsDisplayed()
                .ValidateCreateFinanceInvoiceBatchYesOptionChecked(true)
                .ValidateCreateFinanceTransactionsOptionsIsDisabled(true);

            #endregion

            #region Step 31 & 32

            financeInvoiceBatchSetupRecordPage
                .ClickCreateFinanceInvoiceBatchNoOption()
                .ValidateCreateFinanceInvoiceBatchYesOptionChecked(false)
                .ValidateCreateFinanceTransactionsYesOptionChecked(true)
                .ValidateCreateFinanceTransactionsOptionsIsDisabled(false);

            financeInvoiceBatchSetupRecordPage
                .ClickCreateFinanceTransactionsNoOption()
                .ValidateCreateFinanceTransactionsYesOptionChecked(false);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1822

        [TestProperty("JiraIssueID", "ACC-1838")]
        [Description("Test automation for Step 33 from CDV6-8046: Finance Invoice Batch Setups")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void FinanceInvoiceBatchSetups_UITestMethod008()
        {
            #region Finance General Settings

            commonMethodsDB.CreateFinanceGeneralSettingsIfNeeded(_teamId, 2, true, new DateTime(2024, 3, 31));

            #endregion

            #region Service Provision Status

            var Draft_Serviceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Draft")[0];
            var ReadyForAuthorisation_Serviceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Ready for Authorisation")[0];
            var Authorised_Serviceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Authorised")[0];

            #endregion

            #region Service Provision Start Reason

            var _serviceprovisionstartreasonid = commonMethodsDB.CreateServiceProvisionStartReason(_teamId, _businessUnitId, "Default", new DateTime(2020, 1, 1));

            #endregion

            #region Placement Room Type

            var _placementRoomTypeId = dbHelper.placementRoomType.GetPlacementRoomTypeByName("Not Applicable")[0];

            #endregion

            #region GL Code Location

            var _glCodeLocationId = dbHelper.glCodeLocation.GetByName("Service Element 1").FirstOrDefault();

            #endregion

            #region GL Code
            var _glCodeId = commonMethodsDB.CreateGLCode(_teamId, _glCodeLocationId, "InvoiceGLCode", "9995", "9995");

            #endregion

            #region Rate Units

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(_rateunitid);

            #endregion

            #region Service Element 1 and Service Element 2

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var _serviceElement1IdName = "SE_1_" + currentDate;
            var _serviceElement1Id = commonMethodsDB.CreateServiceElement1(_serviceElement1IdName, _teamId, new DateTime(2023, 5, 17), code, 1, 2, validRateUnits, validRateUnits[0], _glCodeId);

            var _serviceElement2IdName = "SE_2_" + currentDate;
            var _serviceElement2Id = commonMethodsDB.CreateServiceElement2(_teamId, _serviceElement2IdName, new DateTime(2021, 5, 17), code);

            #endregion

            #region Service Permissions

            dbHelper.servicePermission.CreateServicePermission(_teamId, _businessUnitId, _serviceElement1Id, _teamId);

            #endregion

            #region Service Mappings

            commonMethodsDB.CreateServiceMapping(_teamId, _serviceElement1Id, _serviceElement2Id);

            #endregion

            #region GL Code Mappings

            var glCode_Level1Id = dbHelper.glCodeLocation.GetByName("Service Element 1")[0];
            dbHelper.glCodeMapping.CreateGLCodeMapping(_teamId, _serviceElement1Id, glCode_Level1Id);

            #endregion

            #region Service GL Coding

            dbHelper.serviceGLCoding.CreateServiceGLCoding(_teamId, false, true, _serviceElement1Id, _serviceElement2Id, null, null, _glCodeId, null, null);

            #endregion

            #region Authorisation Level

            if (!dbHelper.authorisationLevel.GetBySystemUserId(_systemUserId, 7, new DateTime(2021, 1, 1)).Any())
                dbHelper.authorisationLevel.CreateAuthorisationLevel(_teamId, _systemUserId, new DateTime(2021, 1, 1), 7, 999999m, true, true);

            #endregion

            #region Finance Invoice Batch Setup

            dbHelper.financeInvoiceBatchSetup.CreateFinanceInvoiceBatchSetup(_teamId, _businessUnitId, "", _extractNameId, 2,
                _invoiceById, _invoiceFrequencyId, _paymentTypeCodeId, _providerBatchGroupingId, 2, _serviceElement1Id, new DateTime(2023, 5, 17), true, true, true, 1);

            #endregion

            #region Person

            var _firstName = "John";
            var _lastName = "LN_" + currentDate;
            var _personId = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId, new DateTime(2003, 1, 2));
            var _personNumber = dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"].ToString();

            #endregion

            #region Provider, Service Provided

            Guid _providerId = commonMethodsDB.CreateProvider("ACC_1838_" + currentDate, _teamId, 2);
            Guid _serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_teamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, null, null, null, 2);

            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_teamId, _serviceProvidedId, _paymentTypeCodeId, _vatCodeId, _providerBatchGroupingId, new DateTime(2023, 5, 17), null, 0);

            var ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, _serviceProvidedId, validRateUnits[0], new DateTime(2023, 5, 17), null, 1);

            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, ratePeriodId1, _serviceProvidedId, 10m, 15m, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), true, true, true,
                true, true, true, true, true);

            dbHelper.serviceProvidedRatePeriod.UpdateApprovalStatus(ratePeriodId1, 2);
            dbHelper.serviceProvided.UpdateStatus(_serviceProvidedId, 2);

            #endregion

            #region Service Provision
            var plannedStartDate1 = new DateTime(2023, 5, 17);
            var _serviceProvisionId = dbHelper.serviceProvision.CreateServiceProvision(_teamId, _systemUserId, _personId, Draft_Serviceprovisionstatusid,
                _serviceElement1Id, _serviceElement2Id, null, null, validRateUnits[0], _serviceprovisionstartreasonid, null,
                _teamId, _serviceProvidedId, _providerId, _systemUserId, _placementRoomTypeId, plannedStartDate1, null, null, null, null);

            #endregion

            #region Authorisation Level

            var authorisationLevelExist = dbHelper.authorisationLevel.GetBySystemUserId(_systemUserId, 4).Any();
            if (!authorisationLevelExist)
                dbHelper.authorisationLevel.CreateAuthorisationLevel(_teamId, _systemUserId, new DateTime(2020, 1, 1), 4, 9999m, true, true);

            #endregion            

            #region Service Delivery

            dbHelper.serviceDelivery.CreateServiceDelivery(_teamId, _personId, _serviceProvisionId, validRateUnits[0], 1, 1, true, true, true, true, true, true, true, true, "");

            #endregion

            #region Authorise Service Provision

            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId, ReadyForAuthorisation_Serviceprovisionstatusid);
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId, Authorised_Serviceprovisionstatusid);

            #endregion

            #region Execute 'Expand and Process Finance Transaction Triggers' scheduled job

            var serviceProvision_FinanceTransactions = dbHelper.financeTransaction.GetFinanceTransactionByServiceProvisionID(_serviceProvisionId);
            Assert.AreEqual(0, serviceProvision_FinanceTransactions.Count);

            //Get the schedule job id
            Guid financeTransactionTriggerJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Expand and Process Finance Transaction Triggers")[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(financeTransactionTriggerJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(financeTransactionTriggerJobId);

            #endregion

            #region Step 33

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(_serviceProvisionId.ToString());

            serviceProvisionRecordPage
                 .WaitForServiceProvisionRecordPageToLoad()
                 .ClickFinanceTransactionsTab();

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad()
                .ValidateNoRecordsMessageVisible(false);

            serviceProvision_FinanceTransactions = dbHelper.financeTransaction.GetFinanceTransactionByServiceProvisionID(_serviceProvisionId);
            Assert.IsTrue(serviceProvision_FinanceTransactions.Count > 0);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-1901")]
        [Description("Test automation for Step 34 from CDV6-8046: Finance Invoice Batch Setups")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void FinanceInvoiceBatchSetups_UITestMethod009()
        {
            #region Finance General Settings

            commonMethodsDB.CreateFinanceGeneralSettingsIfNeeded(_teamId, 2, true, new DateTime(2024, 3, 31));

            #endregion

            #region Service Provision Status

            var Draft_Serviceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Draft")[0];
            var ReadyForAuthorisation_Serviceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Ready for Authorisation")[0];
            var Authorised_Serviceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Authorised")[0];

            #endregion

            #region Service Provision Start Reason

            var _serviceprovisionstartreasonid = commonMethodsDB.CreateServiceProvisionStartReason(_teamId, _businessUnitId, "Default", new DateTime(2020, 1, 1));

            #endregion

            #region Placement Room Type

            var _placementRoomTypeId = dbHelper.placementRoomType.GetPlacementRoomTypeByName("Not Applicable")[0];

            #endregion

            #region GL Code Location

            var _glCodeLocationId = dbHelper.glCodeLocation.GetByName("Service Element 1").FirstOrDefault();

            #endregion

            #region GL Code
            var _glCodeId = commonMethodsDB.CreateGLCode(_teamId, _glCodeLocationId, "InvoiceGLCode", "9995", "9995");

            #endregion

            #region Rate Units

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(_rateunitid);

            #endregion

            #region Service Element 1 and Service Element 2

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var _serviceElement1IdName = "SE_1_" + currentDate;
            var _serviceElement1Id = commonMethodsDB.CreateServiceElement1(_serviceElement1IdName, _teamId, new DateTime(2023, 5, 17), code, 1, 2, validRateUnits, validRateUnits[0], _glCodeId);

            var _serviceElement2IdName = "SE_2_" + currentDate;
            var _serviceElement2Id = commonMethodsDB.CreateServiceElement2(_teamId, _serviceElement2IdName, new DateTime(2021, 5, 17), code);

            #endregion

            #region Service Permissions

            dbHelper.servicePermission.CreateServicePermission(_teamId, _businessUnitId, _serviceElement1Id, _teamId);

            #endregion

            #region Service Mappings

            commonMethodsDB.CreateServiceMapping(_teamId, _serviceElement1Id, _serviceElement2Id);

            #endregion

            #region GL Code Mappings

            var glCode_Level1Id = dbHelper.glCodeLocation.GetByName("Service Element 1")[0];
            dbHelper.glCodeMapping.CreateGLCodeMapping(_teamId, _serviceElement1Id, glCode_Level1Id);

            #endregion

            #region Service GL Coding

            dbHelper.serviceGLCoding.CreateServiceGLCoding(_teamId, false, true, _serviceElement1Id, _serviceElement2Id, null, null, _glCodeId, null, null);

            #endregion

            #region Authorisation Level

            if (!dbHelper.authorisationLevel.GetBySystemUserId(_systemUserId, 7, new DateTime(2021, 1, 1)).Any())
                dbHelper.authorisationLevel.CreateAuthorisationLevel(_teamId, _systemUserId, new DateTime(2021, 1, 1), 7, 999999m, true, true);

            #endregion

            #region Finance Invoice Batch Setup

            dbHelper.financeInvoiceBatchSetup.CreateFinanceInvoiceBatchSetup(_teamId, _businessUnitId, "", _extractNameId, 2,
                _invoiceById, _invoiceFrequencyId, _paymentTypeCodeId, _providerBatchGroupingId, 2, _serviceElement1Id, new DateTime(2023, 5, 17), false, null, false, 1);

            #endregion

            #region Person

            var _firstName = "John";
            var _lastName = "LN_" + currentDate;
            var _personId = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId, new DateTime(2003, 1, 2));
            var _personNumber = dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"].ToString();

            #endregion

            #region Provider, Service Provided

            Guid _providerId = commonMethodsDB.CreateProvider("ACC_1901_" + currentDate, _teamId, 2);
            Guid _serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_teamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, null, null, null, 2);

            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_teamId, _serviceProvidedId, _paymentTypeCodeId, _vatCodeId, _providerBatchGroupingId, new DateTime(2023, 5, 17), null, 0);

            var ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, _serviceProvidedId, validRateUnits[0], new DateTime(2023, 5, 17), null, 1);

            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, ratePeriodId1, _serviceProvidedId, 10m, 15m, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), true, true, true,
                true, true, true, true, true);

            dbHelper.serviceProvidedRatePeriod.UpdateApprovalStatus(ratePeriodId1, 2);
            dbHelper.serviceProvided.UpdateStatus(_serviceProvidedId, 2);

            #endregion

            #region Service Provision
            var plannedStartDate1 = new DateTime(2023, 5, 17);
            var _serviceProvisionId = dbHelper.serviceProvision.CreateServiceProvision(_teamId, _systemUserId, _personId, Draft_Serviceprovisionstatusid,
                _serviceElement1Id, _serviceElement2Id, null, null, validRateUnits[0], _serviceprovisionstartreasonid, null,
                _teamId, _serviceProvidedId, _providerId, _systemUserId, _placementRoomTypeId, plannedStartDate1, null, null, null, null);

            #endregion

            #region Authorisation Level

            var authorisationLevelExist = dbHelper.authorisationLevel.GetBySystemUserId(_systemUserId, 4).Any();
            if (!authorisationLevelExist)
                dbHelper.authorisationLevel.CreateAuthorisationLevel(_teamId, _systemUserId, new DateTime(2020, 1, 1), 4, 9999m, true, true);

            #endregion            

            #region Service Delivery

            dbHelper.serviceDelivery.CreateServiceDelivery(_teamId, _personId, _serviceProvisionId, validRateUnits[0], 1, 1, true, true, true, true, true, true, true, true, "");

            #endregion

            #region Authorise Service Provision

            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId, ReadyForAuthorisation_Serviceprovisionstatusid);
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId, Authorised_Serviceprovisionstatusid);

            #endregion

            #region Execute 'Expand and Process Finance Transaction Triggers' scheduled job

            var serviceProvision_FinanceTransactions = dbHelper.financeTransaction.GetFinanceTransactionByServiceProvisionID(_serviceProvisionId);
            Assert.AreEqual(0, serviceProvision_FinanceTransactions.Count);

            //Get the schedule job id
            Guid financeTransactionTriggerJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Expand and Process Finance Transaction Triggers")[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(financeTransactionTriggerJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(financeTransactionTriggerJobId);

            #endregion

            #region Step 34

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(_serviceProvisionId.ToString());

            serviceProvisionRecordPage
                 .WaitForServiceProvisionRecordPageToLoad()
                 .ClickFinanceTransactionsTab();

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad()
                .ValidateNoRecordsMessageVisible(true);

            serviceProvision_FinanceTransactions = dbHelper.financeTransaction.GetFinanceTransactionByServiceProvisionID(_serviceProvisionId);
            Assert.AreEqual(0, serviceProvision_FinanceTransactions.Count);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1922

        [TestProperty("JiraIssueID", "ACC-1944")]
        [Description("Test automation for Step 35 from CDV6-8046: Finance Invoice Batch Setups")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void FinanceInvoiceBatchSetups_UITestMethod010()
        {
            #region Finance General Settings

            commonMethodsDB.CreateFinanceGeneralSettingsIfNeeded(_teamId, 2, true, new DateTime(2024, 3, 31));

            #endregion

            #region Service Provision Status

            var Draft_Serviceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Draft")[0];
            var ReadyForAuthorisation_Serviceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Ready for Authorisation")[0];
            var Authorised_Serviceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Authorised")[0];

            #endregion

            #region Service Provision Start Reason

            var _serviceprovisionstartreasonid = commonMethodsDB.CreateServiceProvisionStartReason(_teamId, _businessUnitId, "Default", new DateTime(2020, 1, 1));

            #endregion

            #region Placement Room Type

            var _placementRoomTypeId = dbHelper.placementRoomType.GetPlacementRoomTypeByName("Not Applicable")[0];

            #endregion

            #region GL Code Location

            var _glCodeLocationId = dbHelper.glCodeLocation.GetByName("Service Element 1").FirstOrDefault();

            #endregion

            #region GL Code
            var _glCodeId = commonMethodsDB.CreateGLCode(_teamId, _glCodeLocationId, "InvoiceGLCode", "9995", "9995");

            #endregion

            #region Rate Units

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(_rateunitid);

            #endregion

            #region Service Element 1 and Service Element 2

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var _serviceElement1IdName = "SE_1_" + currentDate;
            var _serviceElement1Id = commonMethodsDB.CreateServiceElement1(_serviceElement1IdName, _teamId, new DateTime(2023, 5, 17), code, 1, 2, validRateUnits, validRateUnits[0], _glCodeId);

            var _serviceElement2IdName = "SE_2_" + currentDate;
            var _serviceElement2Id = commonMethodsDB.CreateServiceElement2(_teamId, _serviceElement2IdName, new DateTime(2021, 5, 17), code);

            #endregion

            #region Service Permissions

            dbHelper.servicePermission.CreateServicePermission(_teamId, _businessUnitId, _serviceElement1Id, _teamId);

            #endregion

            #region Service Mappings

            commonMethodsDB.CreateServiceMapping(_teamId, _serviceElement1Id, _serviceElement2Id);

            #endregion

            #region GL Code Mappings

            var glCode_Level1Id = dbHelper.glCodeLocation.GetByName("Service Element 1")[0];
            dbHelper.glCodeMapping.CreateGLCodeMapping(_teamId, _serviceElement1Id, glCode_Level1Id);

            #endregion

            #region Service GL Coding

            dbHelper.serviceGLCoding.CreateServiceGLCoding(_teamId, false, true, _serviceElement1Id, _serviceElement2Id, null, null, _glCodeId, null, null);

            #endregion

            #region Authorisation Level

            if (!dbHelper.authorisationLevel.GetBySystemUserId(_systemUserId, 7, new DateTime(2021, 1, 1)).Any())
                dbHelper.authorisationLevel.CreateAuthorisationLevel(_teamId, _systemUserId, new DateTime(2021, 1, 1), 7, 999999m, true, true);

            #endregion

            #region Finance Invoice Batch Setup

            Guid FinanceInvoiceBatchSetupId = dbHelper.financeInvoiceBatchSetup.CreateFinanceInvoiceBatchSetup(_teamId, _businessUnitId, "", _extractNameId, 2,
                _invoiceById, _invoiceFrequencyId, _paymentTypeCodeId, _providerBatchGroupingId, 2, _serviceElement1Id, new DateTime(2023, 5, 17), false, null, true, 1);

            #endregion

            #region Person

            var _firstName = "John";
            var _lastName = "LN_" + currentDate;
            var _personId = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId, new DateTime(2003, 1, 2));
            var _personNumber = dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"].ToString();

            #endregion

            #region Provider, Service Provided

            var _providerName = "ACC_1944_" + currentDate;
            Guid _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 2);
            Guid _serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_teamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, null, null, null, 2);

            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_teamId, _serviceProvidedId, _paymentTypeCodeId, _vatCodeId, _providerBatchGroupingId, new DateTime(2023, 5, 17), null, 0);

            var ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, _serviceProvidedId, validRateUnits[0], new DateTime(2023, 5, 17), null, 1);

            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, ratePeriodId1, _serviceProvidedId, 10m, 15m, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), true, true, true,
                true, true, true, true, true);

            dbHelper.serviceProvidedRatePeriod.UpdateApprovalStatus(ratePeriodId1, 2);
            dbHelper.serviceProvided.UpdateStatus(_serviceProvidedId, 2);

            #endregion

            #region Service Provision
            var plannedStartDate1 = new DateTime(2023, 5, 17);
            var _serviceProvisionId = dbHelper.serviceProvision.CreateServiceProvision(_teamId, _systemUserId, _personId, Draft_Serviceprovisionstatusid,
                _serviceElement1Id, _serviceElement2Id, null, null, validRateUnits[0], _serviceprovisionstartreasonid, null,
                _teamId, _serviceProvidedId, _providerId, _systemUserId, _placementRoomTypeId, plannedStartDate1, null, null, null, null);

            #endregion

            #region Authorisation Level

            var authorisationLevelExist = dbHelper.authorisationLevel.GetBySystemUserId(_systemUserId, 4).Any();
            if (!authorisationLevelExist)
                dbHelper.authorisationLevel.CreateAuthorisationLevel(_teamId, _systemUserId, new DateTime(2020, 1, 1), 4, 9999m, true, true);

            #endregion            

            #region Service Delivery

            dbHelper.serviceDelivery.CreateServiceDelivery(_teamId, _personId, _serviceProvisionId, validRateUnits[0], 1, 1, true, true, true, true, true, true, true, true, "");

            #endregion

            #region Authorise Service Provision

            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId, ReadyForAuthorisation_Serviceprovisionstatusid);
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId, Authorised_Serviceprovisionstatusid);

            #endregion

            #region Execute 'Expand and Process Finance Transaction Triggers' scheduled job

            var serviceProvision_FinanceTransactions = dbHelper.financeTransaction.GetFinanceTransactionByServiceProvisionID(_serviceProvisionId);
            Assert.AreEqual(0, serviceProvision_FinanceTransactions.Count);

            //Get the schedule job id
            Guid financeTransactionTriggerJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Expand and Process Finance Transaction Triggers")[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(financeTransactionTriggerJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(financeTransactionTriggerJobId);

            #endregion

            #region Step 35

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickFinanceProcessingAreaButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            financeInvoiceBatchSetupPage
                .WaitForFinanceInvoiceBatchSetupPageToLoad()
                .OpenRecord(FinanceInvoiceBatchSetupId.ToString());

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .NavigateToFinanceInvoicesBatchesPage();

            var invoiceBatchId = dbHelper.financeInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(FinanceInvoiceBatchSetupId);

            financeInvoiceBatchesPage
                .WaitForFinanceInvoiceBatchSetup_FinanceInvoiceBatchesPageToLoad()
                .ValidateNoRecordsMessageVisible();

            Assert.AreEqual(0, invoiceBatchId.Count);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToProvidersSection();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(_providerName.ToString())
                .OpenProviderRecord(_providerId.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .TapServiceProvisionsTab();

            serviceProvisionsPage
                .WaitForServiceProvisionsPageToLoad(_providerId.ToString())
                .OpenServiceProvisionRecord(_serviceProvisionId.ToString());

            serviceProvisionRecordPage
                 .WaitForServiceProvisionRecordPageToLoad()
                 .ClickFinanceTransactionsTab();

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad()
                .ValidateNoRecordsMessageVisible(false);

            serviceProvision_FinanceTransactions = dbHelper.financeTransaction.GetFinanceTransactionByServiceProvisionID(_serviceProvisionId);
            Assert.IsTrue(serviceProvision_FinanceTransactions.Count > 0);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-1945")]
        [Description("Test automation for Step 36 from CDV6-8046: Finance Invoice Batch Setups")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void FinanceInvoiceBatchSetups_UITestMethod011()
        {
            #region Finance General Settings

            commonMethodsDB.CreateFinanceGeneralSettingsIfNeeded(_teamId, 2, true, new DateTime(2024, 3, 31));

            #endregion

            #region Service Provision Status

            var Draft_Serviceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Draft")[0];
            var ReadyForAuthorisation_Serviceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Ready for Authorisation")[0];
            var Authorised_Serviceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Authorised")[0];

            #endregion

            #region Service Provision Start Reason

            var _serviceprovisionstartreasonid = commonMethodsDB.CreateServiceProvisionStartReason(_teamId, _businessUnitId, "Default", new DateTime(2020, 1, 1));

            #endregion

            #region Placement Room Type

            var _placementRoomTypeId = dbHelper.placementRoomType.GetPlacementRoomTypeByName("Not Applicable")[0];

            #endregion

            #region GL Code Location

            var _glCodeLocationId = dbHelper.glCodeLocation.GetByName("Service Element 1").FirstOrDefault();

            #endregion

            #region GL Code

            var _glCodeId = commonMethodsDB.CreateGLCode(_teamId, _glCodeLocationId, "InvoiceGLCode", "9995", "9995");

            #endregion

            #region Valid Rate Units

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(_rateunitid);

            #endregion

            #region Service Element 1 and Service Element 2

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var _serviceElement1IdName = "SE_1_" + currentDate;
            var _serviceElement1Id = commonMethodsDB.CreateServiceElement1(_serviceElement1IdName, _teamId, new DateTime(2023, 5, 17), code, 1, 2, validRateUnits, validRateUnits[0], _glCodeId);

            var _serviceElement2IdName = "SE_2_" + currentDate;
            var _serviceElement2Id = commonMethodsDB.CreateServiceElement2(_teamId, _serviceElement2IdName, new DateTime(2021, 5, 17), code);

            #endregion

            #region Service Permissions

            dbHelper.servicePermission.CreateServicePermission(_teamId, _businessUnitId, _serviceElement1Id, _teamId);

            #endregion

            #region Service Mappings

            commonMethodsDB.CreateServiceMapping(_teamId, _serviceElement1Id, _serviceElement2Id);

            #endregion

            #region GL Code Mappings

            var glCode_Level1Id = dbHelper.glCodeLocation.GetByName("Service Element 1")[0];
            dbHelper.glCodeMapping.CreateGLCodeMapping(_teamId, _serviceElement1Id, glCode_Level1Id);

            #endregion

            #region Service GL Coding

            dbHelper.serviceGLCoding.CreateServiceGLCoding(_teamId, false, true, _serviceElement1Id, _serviceElement2Id, null, null, _glCodeId, null, null);

            #endregion

            #region Authorisation Level

            if (!dbHelper.authorisationLevel.GetBySystemUserId(_systemUserId, 7, new DateTime(2021, 1, 1)).Any())
                dbHelper.authorisationLevel.CreateAuthorisationLevel(_teamId, _systemUserId, new DateTime(2021, 1, 1), 7, 999999m, true, true);

            #endregion

            #region Finance Invoice Batch Setup

            var FinanceInvoiceBatchingSetupId = dbHelper.financeInvoiceBatchSetup.CreateFinanceInvoiceBatchSetup(_teamId, _businessUnitId, "", null, 2,
                null, null, _paymentTypeCodeId, _providerBatchGroupingId, 2, _serviceElement1Id, new DateTime(2023, 5, 17), false, null, false, 1);

            var FinanceInvoiceBatchingSetupName = (string)dbHelper.financeInvoiceBatchSetup.GetByID(FinanceInvoiceBatchingSetupId, "name")["name"];

            #endregion

            #region Person

            var _firstName = "John";
            var _lastName = "LN_" + currentDate;
            var _personId = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId, new DateTime(2003, 1, 2));
            var _personNumber = dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"].ToString();

            #endregion

            #region Provider, Service Provided

            Guid _providerId = commonMethodsDB.CreateProvider("ACC_1945_" + currentDate, _teamId, 2);
            Guid _serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_teamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, null, null, null, 2);

            var ServiceFinanceSettingsId = dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_teamId, _serviceProvidedId, _paymentTypeCodeId, _vatCodeId, _providerBatchGroupingId, new DateTime(2023, 5, 17), null, 0);
            var ServiceFinanceSettingsName = (string)dbHelper.serviceFinanceSettings.GetByID(ServiceFinanceSettingsId, "name")["name"];

            var ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, _serviceProvidedId, validRateUnits[0], new DateTime(2023, 5, 17), null, 1);
            var ratePeriodName = (string)dbHelper.serviceProvidedRatePeriod.GetByID(ratePeriodId1, "name")["name"];

            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, ratePeriodId1, _serviceProvidedId, 10m, 15m, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), true, true, true,
                true, true, true, true, true);

            dbHelper.serviceProvidedRatePeriod.UpdateApprovalStatus(ratePeriodId1, 2);
            dbHelper.serviceProvided.UpdateStatus(_serviceProvidedId, 2);

            #endregion

            #region Service Provision
            var plannedStartDate1 = new DateTime(2023, 5, 17);
            var _serviceProvisionId = dbHelper.serviceProvision.CreateServiceProvision(_teamId, _systemUserId, _personId, Draft_Serviceprovisionstatusid,
                _serviceElement1Id, _serviceElement2Id, null, null, validRateUnits[0], _serviceprovisionstartreasonid, null,
                _teamId, _serviceProvidedId, _providerId, _systemUserId, _placementRoomTypeId, plannedStartDate1, null, null, null, null);

            var _serviceProvisionName = (string)dbHelper.serviceProvision.GetByID(_serviceProvisionId, "name")["name"];

            #endregion

            #region Authorisation Level

            var authorisationLevelExist = dbHelper.authorisationLevel.GetBySystemUserId(_systemUserId, 4).Any();
            if (!authorisationLevelExist)
                dbHelper.authorisationLevel.CreateAuthorisationLevel(_teamId, _systemUserId, new DateTime(2020, 1, 1), 4, 9999m, true, true);

            #endregion            

            #region Service Delivery

            dbHelper.serviceDelivery.CreateServiceDelivery(_teamId, _personId, _serviceProvisionId, validRateUnits[0], 1, 1, true, true, true, true, true, true, true, true, "");

            #endregion

            #region Authorise Service Provision

            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId, ReadyForAuthorisation_Serviceprovisionstatusid);
            System.Threading.Thread.Sleep(500);
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId, Authorised_Serviceprovisionstatusid);

            #endregion

            #region Execute 'Expand and Process Finance Transaction Triggers' scheduled job

            var serviceProvision_FinanceTransactions = dbHelper.financeTransaction.GetFinanceTransactionByServiceProvisionID(_serviceProvisionId);
            Assert.AreEqual(0, serviceProvision_FinanceTransactions.Count);

            //Get the schedule job id
            Guid financeTransactionTriggerJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Expand and Process Finance Transaction Triggers")[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(financeTransactionTriggerJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(financeTransactionTriggerJobId);

            #endregion

            #region Step 36

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickFinanceProcessingAreaButton()
                .ClickFinanceTransactionTriggersButton();

            financeTransactionTriggersPage
                .WaitForPageToLoad()
                .SelectSystemView("Processed");

            var FinanceInvoiceBatchSetupTriggerId = dbHelper.financeTransactionTrigger.GetByRecordidTableNameAndRecordidName("financeinvoicebatchsetup", FinanceInvoiceBatchingSetupName.ToString());
            Assert.AreEqual(1, FinanceInvoiceBatchSetupTriggerId.Count);

            var ServiceFinanceSettingsTriggerId = dbHelper.financeTransactionTrigger.GetByRecordidTableNameAndRecordidName("servicefinancesettings", ServiceFinanceSettingsName.ToString());
            Assert.AreEqual(1, ServiceFinanceSettingsTriggerId.Count);

            var ServiceProvidedRatePeriodTriggerId = dbHelper.financeTransactionTrigger.GetByRecordidTableNameAndRecordidName("serviceprovidedrateperiod", ratePeriodName.ToString());
            Assert.AreEqual(1, ServiceProvidedRatePeriodTriggerId.Count);

            var ServiceProvisionTriggerId = dbHelper.financeTransactionTrigger.GetByRecordidTableNameAndRecordidName("serviceprovision", _serviceProvisionName.ToString());
            Assert.AreEqual(1, ServiceProvisionTriggerId.Count);

            financeTransactionTriggersPage
                .ValidateRecordIsPresent(FinanceInvoiceBatchSetupTriggerId[0].ToString(), true)
                .ValidateRecordIsPresent(ServiceFinanceSettingsTriggerId[0].ToString(), true)
                .ValidateRecordIsPresent(ServiceProvidedRatePeriodTriggerId[0].ToString(), true)
                .ValidateRecordIsPresent(ServiceProvisionTriggerId[0].ToString(), true);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1923

        [TestProperty("JiraIssueID", "ACC-1981")]
        [Description("Test automation for Step 37 to 43 from CDV6-8046: Finance Invoice Batch Setups")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void FinanceInvoiceBatchSetups_UITestMethod012()
        {
            #region Rate Units

            var rateunitid = dbHelper.rateUnit.GetByName("Per Day \\ Days")[0];
            var validRateUnits = new List<Guid>();
            validRateUnits.Add(rateunitid);

            #endregion

            #region Service Element 1

            var whoToPay_Provider = 1; // Provider
            var whoToPay_Carer = 2; // Carer
            var whoToPay_Person = 3; // Person

            var code1 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var _serviceElement1_ProviderName = "SE1_Provider_1981_" + currentDate;
            var _serviceElement1_ProviderId = commonMethodsDB.CreateServiceElement1(_serviceElement1_ProviderName, _teamId, new DateTime(2021, 1, 1), code1, whoToPay_Provider, 2, validRateUnits);

            var code2 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var _serviceElement1_CarerName = "SE1_Carer_1981_" + currentDate;
            var _serviceElement1_CarerId = commonMethodsDB.CreateServiceElement1(_serviceElement1_CarerName, _teamId, new DateTime(2021, 1, 1), code2, whoToPay_Carer, 2, null, false);

            var code3 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var _serviceElement1_PersonName = "SE1_Person_1981_" + currentDate;
            var _serviceElement1_PersonId = commonMethodsDB.CreateServiceElement1(_serviceElement1_PersonName, _teamId, new DateTime(2021, 1, 1), code3, whoToPay_Person, 2, validRateUnits);

            #endregion

            #region Step 37

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickFinanceProcessingAreaButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            financeInvoiceBatchSetupPage
                .WaitForFinanceInvoiceBatchSetupPageToLoad()
                .ClickNewRecordButton();

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .SelectFinanceModule("Supplier Payments")
                .ValidateCreateFinanceTransactionsFieldOptionsDisplayed()
                .ValidateCreateFinanceTransactionsOptionsIsDisabled(true)
                .ValidateCreateFinanceTransactionsYesOptionChecked(true)

                .ValidatePermitAuthorisationOfCreditInvoicesFieldOptionsDisplayed()
                .ValidatePermitAuthorisationOfCreditInvoicesOptionsIsDisabled(false)

                .ValidateCreateFinanceInvoiceBatchFieldOptionsDisplayed()
                .ValidateCreateFinanceInvoiceBatchOptionsIsDisabled(false)

                .ValidateApplyFinanceTransactionsToEarliestFinanceInvoiceFieldOptionsDisplayed()
                .ValidateApplyFinanceTransactionsToEarliestFinanceInvoiceOptionsIsDisabled(false)

                .ValidateApplySuspensionsFieldOptionsDisplayed()
                .ValidateApplySuspensionsOptionsIsDisabled(false)

                .ValidateCreditorReferenceNumberRequiredFieldOptionsDisplayed()
                .ValidateCreditorReferenceNumberRequiredOptionsIsDisabled(false);

            #endregion

            #region Step 38

            financeInvoiceBatchSetupRecordPage
                .SelectFinanceModule("Carer Payments")
                .ValidateCreateFinanceTransactionsFieldOptionsDisplayed(false)

                .SelectFinanceModule("Debtors")
                .ValidateCreateFinanceTransactionsFieldOptionsDisplayed(false);

            #endregion

            #region Step 39

            financeInvoiceBatchSetupRecordPage
                .SelectFinanceModule("Supplier Payments")
                .ValidateDebtorsVATCodeFieldOptionsVisibility(true);

            #endregion

            #region Step 42 & 43

            financeInvoiceBatchSetupRecordPage
                .ClickServiceElement1LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_serviceElement1_ProviderName)
                .TapSearchButton()
                .ValidateResultElementPresent(_serviceElement1_ProviderId.ToString())

                .TypeSearchQuery(_serviceElement1_PersonName)
                .TapSearchButton()
                .ValidateResultElementPresent(_serviceElement1_PersonId.ToString())

                .TypeSearchQuery(_serviceElement1_CarerName)
                .TapSearchButton()
                .ValidateResultElementNotPresent(_serviceElement1_CarerId.ToString())

                .ClickCloseButton();

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .SelectFinanceModule("Carer Payments")
                .ClickServiceElement1LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_serviceElement1_ProviderName)
                .TapSearchButton()
                .ValidateResultElementNotPresent(_serviceElement1_ProviderId.ToString())

                .TypeSearchQuery(_serviceElement1_PersonName)
                .TapSearchButton()
                .ValidateResultElementNotPresent(_serviceElement1_PersonId.ToString())

                .TypeSearchQuery(_serviceElement1_CarerName)
                .TapSearchButton()
                .ValidateResultElementPresent(_serviceElement1_CarerId.ToString())

                .ClickCloseButton();

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad();

            #endregion

            #region Step 41

            financeInvoiceBatchSetupRecordPage
                .ValidateDebtorsVATCodeLinkFieldTextVisibility(false)
                .ValidateDebtorsVATCodeFieldIsNotMandatory()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            Guid FinanceInvoiceBatchSetupId = dbHelper.financeInvoiceBatchSetup.CreateFinanceInvoiceBatchSetup(_teamId, _businessUnitId, "", _extractNameId, 2, _invoiceById, _invoiceFrequencyId, _paymentTypeCodeId, _providerBatchGroupingId, 3, _serviceElement1_ProviderId, new DateTime(2023, 5, 4), true, true, true, 1);

            financeInvoiceBatchSetupPage
                .WaitForFinanceInvoiceBatchSetupPageToLoad()
                .InsertSearchQuery("*" + _serviceElement1_ProviderName.ToString() + "*")
                .TapSearchButton()
                .ValidateRecordIsPresent(FinanceInvoiceBatchSetupId.ToString(), true)
                .OpenRecord(FinanceInvoiceBatchSetupId.ToString());

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .ValidateDebtorsVATCodeLookupButtonIsDisabled(false);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Finance Invoice Batch Setups")

                .ValidateSelectFilterFieldOptionIsPresent("1", "Debtors VAT Code")
                .SelectFilter("1", "Debtors VAT Code");

            #endregion
        }

        #endregion

    }
}

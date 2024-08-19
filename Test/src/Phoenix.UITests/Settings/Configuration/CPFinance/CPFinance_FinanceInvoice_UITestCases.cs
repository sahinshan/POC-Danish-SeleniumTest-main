using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace Phoenix.UITests.Settings.Configuration.CPFinance
{
    /// <summary>
    /// This class contains Automated UI test scripts for CP Finance > Finance Invoices
    /// </summary>
    [TestClass]
    public class CPFinance_FinanceInvoice_UITestCases : FunctionalTest
    {
        #region Properties

        private string _environmentName;
        private string _tenantName;
        private Guid _authenticationProviderId;
        private Guid _languageId;
        private Guid _businessUnitId;
        private Guid _businessUnitId2;
        private Guid _teamId;
        private Guid _teamId2;
        private string _teamName;
        private string _teamName2;
        private Guid _defaultLoginUserID;
        public Guid environmentid;
        private string _loginUsername;
        private string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");

        internal Guid _recurrencePattern_Every1WeekMondayId;
        internal Guid _recurrencePattern_Every1WeekTuesdayId;
        internal Guid _recurrencePattern_Every1WeekWednesdayId;
        internal Guid _recurrencePattern_Every1WeekThursdayId;
        internal Guid _recurrencePattern_Every1WeekFridayId;
        internal Guid _recurrencePattern_Every1WeekSaturdayId;
        internal Guid _recurrencePattern_Every1WeekSundayId;

        #endregion

        #region Internal Methods

        internal void CreateUserWorkSchedule(Guid UserId, Guid TeamId, Guid SystemUserEmploymentContractId, Guid availabilityTypeId)
        {
            for (int i = 0; i < 7; i++)
            {
                var workScheduleDate = DateTime.Now.AddDays(i).Date;

                switch (workScheduleDate.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekSundayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    case DayOfWeek.Monday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekMondayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    case DayOfWeek.Tuesday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekTuesdayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    case DayOfWeek.Wednesday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekWednesdayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    case DayOfWeek.Thursday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekThursdayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    case DayOfWeek.Friday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekFridayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    case DayOfWeek.Saturday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekSaturdayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    default:
                        break;
                }
            }
        }

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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CP_FI_BU");

                #endregion

                #region Business Unit 2

                _businessUnitId2 = commonMethodsDB.CreateBusinessUnit("CP_FI_BU2");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                _teamName = "CP_FI_Team";
                _teamId = commonMethodsDB.CreateTeam(_teamName, null, _businessUnitId, "90400", "CareProvidersFI@careworkstempmail.com", _teamName, "020 123456");

                #endregion

                #region Team

                _teamName2 = "CP_FI_Team2";
                _teamId2 = commonMethodsDB.CreateTeam(_teamName2, null, _businessUnitId2, "90400", "CareProvidersFI2@careworkstempmail.com", _teamName2, "020 123456");

                #endregion

                #region Create default system user

                _loginUsername = "CP_FIUser1";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUsername, "CP_FI", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationProviderId);

                commonMethodsDB.CreateTeamMember(_teamId, _defaultLoginUserID, new DateTime(2020, 1, 1), null);
                dbHelper.systemUser.UpdateDefaultTeam(_defaultLoginUserID, _teamId);

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

            //Get the schedule job id
            Guid processCpFinanceScheduledJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName(ScheduledJobName)[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(processCpFinanceScheduledJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the authentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(processCpFinanceScheduledJobId);

            #endregion
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-7410

        [TestProperty("JiraIssueID", "ACC-7508")]
        [Description("Test for manually creation of Finance Invoices. Step 1 to Step 5.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoices")]
        public void CPFinance_FinanceInvoice_UITestMethod001()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Rui";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Provider

            var providerName = "Establishment Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority
            dbHelper.provider.UpdateDebtorNumber(funderProviderID, currentTimeString);

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

            //var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);
            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type: Generic Extract File

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
            Thread.Sleep(4000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            Thread.Sleep(10000);

            var completedFinanceInvoiceBatchIds = dbHelper.careProviderFinanceInvoiceBatch.GetFinanceInvoiceBatchBySetupIDAndStatusID(_careProviderFinanceInvoiceBatchSetupId1, 2);
            Assert.AreEqual(1, completedFinanceInvoiceBatchIds.Count);
            Assert.AreEqual(_cpFinanceInvoiceBatchId1, completedFinanceInvoiceBatchIds[0]);

            #endregion

            #region Update invoice status to completed

            //update invoice status to completed
            var _cpFinanceInvoiceId = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchIdAndInvoiceStatusId(_cpFinanceInvoiceBatchId1, 1)[0];
            //dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId, 2);

            #endregion

            #region Step 1

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);


            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicesSection();

            cpFinanceInvoicesPage
                .WaitForCPFinanceInvoicesPageToLoad()
                .ValidateNewRecordButtonVisible();

            #endregion

            #region Step 2

            cpFinanceInvoicesPage
                .WaitForCPFinanceInvoicesPageToLoad()
                .ClickNewRecordButton();

            financeInvoiceRecordPage
                .WaitForCPFinanceInvoiceRecordPageToLoad()
                .ValidatePayerLookupButtonVisible(true)
                .ValidateEstablishmentLookupButtonVisible(true)
                .ValidateDirectDebitExtractBatchLookupButtonVisible(true)
                .ValidatePersonLookupButtonVisible(true)
                .ValidateCustomerAccountCodeFieldVisible(true);

            financeInvoiceRecordPage
                .ValidateAlternativeInvoiceNumberFieldVisible(true)
                .ValidateChargesUpto_DateFieldVisible(true);

            financeInvoiceRecordPage
                .ValidateNetAmountFieldVisible(true)
                .ValidateGrossAmountFieldVisible(true)
                .ValidateVATAmountFieldVisible(true)
                .ValidateIncludeInDebtOutstandingOptionsVisible(true);

            #endregion

            #region Step 3

            financeInvoiceRecordPage
                .ClickPayerLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateLookForPickListExpectedTextIsPresent("Providers", true)
                .ValidateLookForPickListExpectedTextIsPresent("People", true)
                .ValidateLookForSelectedValue("People");

            #endregion

            #region Step 4

            lookupPopup
                .ValidateLookInPickListExpectedTextIsPresent("All Active People With Finance Invoices", true)
                .SelectLookFor("Providers")
                .ValidateLookInPickListExpectedTextIsPresent("All Active Providers With Finance Invoices", true)
                .ClickCloseButton();

            #endregion

            #region Step 5

            financeInvoiceRecordPage
                .WaitForCPFinanceInvoiceRecordPageToLoad()
                .ClickEstablishmentLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateLookInSelectedValue("Residential Establishment View")
                .ValidateLookInFieldIsDisabled(true);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-7575

        [TestProperty("JiraIssueID", "ACC-7607")]
        [Description("Test for manually creation of Finance Invoices. Step 12 to Step 17.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoices")]
        public void CPFinance_FinanceInvoice_UITestMethod002()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Rui";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Provider

            var providerName = "Establishment ProviderB " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder ProviderB " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority
            dbHelper.provider.UpdateDebtorNumber(funderProviderID, currentTimeString);

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

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);
            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type: Generic Extract File

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

            var runOn1 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddSeconds(10));
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId1, runOn1, null, null);
            Thread.Sleep(10000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            Thread.Sleep(10000);

            var completedFinanceInvoiceBatchIds = dbHelper.careProviderFinanceInvoiceBatch.GetFinanceInvoiceBatchBySetupIDAndStatusID(_careProviderFinanceInvoiceBatchSetupId1, 2);
            Assert.AreEqual(1, completedFinanceInvoiceBatchIds.Count);
            Assert.AreEqual(_cpFinanceInvoiceBatchId1, completedFinanceInvoiceBatchIds[0]);

            #endregion

            #region Update invoice status to completed

            //update invoice status to completed
            var _cpFinanceInvoiceId = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchIdAndInvoiceStatusId(_cpFinanceInvoiceBatchId1, 1)[0];
            //dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId, 2);

            #endregion

            #region Step 12

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicesSection();

            cpFinanceInvoicesPage
                .WaitForCPFinanceInvoicesPageToLoad()
                .ClickNewRecordButton();

            financeInvoiceRecordPage
                .WaitForCPFinanceInvoiceRecordPageToLoad()
                .ValidatePayerFieldText("")
                .ValidateEstablishmentFieldText("")
                .ValidateDirectDebitExtractBatchFieldText("")
                .ValidatePersonFieldText("");

            financeInvoiceRecordPage
                .ValidateChargesUpto_DateFieldText("")
                .ValidateNetAmountFieldText("")
                .ValidateGrossAmountFieldText("0.00")
                .ValidateGrossAmountFieldDisabled(true)
                .ValidateIncludeInDebtOutstandingOptionsChecked(true)
                .ValidateVatAmountFieldText("0.00");

            #endregion

            #region Step 13

            financeInvoiceRecordPage
                .InsertTextOnNetAmountField("10.00")
                .ValidateGrossAmountFieldText("10.00");

            financeInvoiceRecordPage
                .InsertTextOnVATAmountField("1.00")
                .ValidateGrossAmountFieldText("11.00");

            financeInvoiceRecordPage
                .InsertTextOnNetAmountField("15.00")
                .ValidateGrossAmountFieldText("16.00")
                .InsertTextOnVATAmountField("5.00")
                .ValidateGrossAmountFieldText("20.00");

            #endregion

            #region Step 14

            //click payer lookup button
            //wait for lookup popup to load, select 'Providers' in Look For picklist
            //search for Funder Provider
            //select the Funder Provider
            //click OK button

            financeInvoiceRecordPage
                .ClickPayerLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookFor("Providers")
                .SearchAndSelectRecord(funderProviderName, funderProviderID);

            //click person lookup button
            //wait for lookup popup to load, select 'All Active People' in Look In picklist
            //search for the person created in the test
            //select the person
            //click OK button

            financeInvoiceRecordPage
                .WaitForCPFinanceInvoiceRecordPageToLoad()
                .ClickPersonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("All Active People")
                .SearchAndSelectRecord(firstName + " " + lastName, _personId);

            //click establishment lookup button
            //wait for lookup popup to load, search establishment provider
            //select the establishment provider
            //click OK button

            financeInvoiceRecordPage
                .WaitForCPFinanceInvoiceRecordPageToLoad()
                .ClickEstablishmentLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(providerName, providerId);

            //insert charges upto 

            financeInvoiceRecordPage
                .WaitForCPFinanceInvoiceRecordPageToLoad()
                .InsertTextOnChargesUpto_DateField(commonMethodsHelper.GetLastDayOfMonth(todayDate).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton();

            financeInvoiceRecordPage
                .WaitForCPFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusPicklistText("Brought Forward")
                .ClickBackButton();

            var _broughtForwardCpFinanceInvoiceId = dbHelper.careProviderFinanceInvoice.GetByPayerAndFinanceInvoiceStatusId(funderProviderID, 5)[0];

            #endregion

            #region Step 15

            cpFinanceInvoicesPage
                .WaitForCPFinanceInvoicesPageToLoad()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceInvoicesPageToLoad()
                .OpenRecord(_broughtForwardCpFinanceInvoiceId);

            financeInvoiceRecordPage
                .WaitForCPFinanceInvoiceRecordPageToLoad()
                .ValidatePaymentsTabVisible(true)
                .ValidateFinanceTransactionTabVisible(false)
                .ClickPaymentsTab();

            cpFinanceInvoicePaymentsPage
                .WaitForPageToLoad(_broughtForwardCpFinanceInvoiceId, true);

            #endregion

            #region Step 16

            financeInvoiceRecordPage
                .WaitForCPFinanceInvoiceRecordPageToLoad()
                .ClickDetailsTab();

            financeInvoiceRecordPage
                .InsertTextOnNetAmountField("")
                .ValidateGrossAmountFieldText("0.00")
                .InsertTextOnNetAmountField("15.00")
                .ValidateGrossAmountFieldText("20.00")
                .InsertTextOnVATAmountField("")
                .ValidateGrossAmountFieldText("0.00")
                .InsertTextOnVATAmountField("5.00")
                .ValidateGrossAmountFieldText("20.00");

            #endregion

            #region Step 17

            financeInvoiceRecordPage
                .ClickBackButton();

            #region Payment Methods

            var paymentMethodId = dbHelper.paymentMethod.GetByName("Cheque")[0];

            #endregion

            #region Accounting Periods and Finance Invoice Payment

            DateTime periodFrom1 = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime periodTo1 = new DateTime(DateTime.Now.Year, 12, 31);
            DateTime PaymentPostedDate = commonMethodsHelper.GetDatePartWithoutCulture();

            var accountingPeriodIdExists = dbHelper.careProviderAccountingPeriod.GetByPeriodFromAndPeriodToDateAndCurrentPeriod(periodFrom1, periodTo1, true, false).Any();
            if (accountingPeriodIdExists.Equals(false))
            {
                dbHelper.careProviderAccountingPeriod.CreateCareProviderAccountingPeriod(_teamId, periodFrom1, periodTo1, null, true, false, false);
            }

            var _cpFinanceInvoicePaymentId1 = dbHelper.careProviderFinanceInvoicePayment.CreateCareProviderFinanceInvoicePayment(_teamId,
                _broughtForwardCpFinanceInvoiceId, providerId, funderProviderID, "provider", funderProviderName, "ACC-7409" + currentTimeString,
                PaymentPostedDate, PaymentPostedDate, 10m, 0m, paymentMethodId, "", false, false, "Test7575_" + currentTimeString, null);

            #endregion

            cpFinanceInvoicesPage
                .WaitForCPFinanceInvoicesPageToLoad()
                .OpenRecord(_broughtForwardCpFinanceInvoiceId);

            financeInvoiceRecordPage
                .WaitForCPFinanceInvoiceRecordPageToLoad()
                .ValidateChargesUpto_DateFieldDisabled(true)
                .ValidateNetAmountFieldDisabled(true)
                .ValidateVatAmountFieldDisabled(true)
                .ValidateDeleteButtonVisible(false);

            dbHelper.careProviderFinanceInvoicePayment.DeleteCareProviderFinanceInvoicePayment(_cpFinanceInvoicePaymentId1);

            financeInvoiceRecordPage
                .WaitForCPFinanceInvoiceRecordPageToLoad()
                .ClickBackButton();

            cpFinanceInvoicesPage
                .WaitForCPFinanceInvoicesPageToLoad()
                .OpenRecord(_broughtForwardCpFinanceInvoiceId);

            financeInvoiceRecordPage
                .WaitForCPFinanceInvoiceRecordPageToLoad()
                .ValidateChargesUpto_DateFieldDisabled(false)
                .ValidateNetAmountFieldDisabled(false)
                .ValidateVatAmountFieldDisabled(false)
                .ValidateDeleteButtonVisible(true)
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            cpFinanceInvoicesPage
                .WaitForCPFinanceInvoicesPageToLoad()
                .ValidateRecordIsPresent(_broughtForwardCpFinanceInvoiceId.ToString(), false);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-7576


        [TestProperty("JiraIssueID", "ACC-7611")]
        [Description("Test for manually creation of Finance Invoices. Step 6 to Step 11.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoices")]
        public void CPFinance_FinanceInvoice_UITestMethod003()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Team Member

            var _teamMemberId2 = commonMethodsDB.CreateTeamMember(_teamId2, _defaultLoginUserID, new DateTime(2020, 1, 1), null);
            dbHelper.systemUser.UpdateDefaultTeam(_defaultLoginUserID, _teamId2);
            var teamMemberId1 = dbHelper.teamMember.GetTeamMemberByUserAndTeamID(_defaultLoginUserID, _teamId).FirstOrDefault();
            dbHelper.teamMember.DeleteTeamMember(teamMemberId1); //remove the old team from the user            

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Rui";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Provider

            var providerName1 = "Establishment ProviderC1";
            var addressType = 10; //Home
            var providerId1 = commonMethodsDB.CreateProvider(providerName1, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var providerName2 = "Establishment ProviderC2";
            var providerId2 = commonMethodsDB.CreateProvider(providerName2, _teamId2, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder ProviderC1";
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority
            dbHelper.provider.UpdateDebtorNumber(funderProviderID, currentTimeString);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId1, funderProviderID);

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

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId1, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name, Extract Type

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);
            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type: Generic Extract File

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
            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId1, careProviderContractScheme1Id, funderProviderID, personContractStartDate, null, true);

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

            var runOn1 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddSeconds(30));
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId1, runOn1, null, null);
            Thread.Sleep(30000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            Thread.Sleep(10000);

            var completedFinanceInvoiceBatchIds = dbHelper.careProviderFinanceInvoiceBatch.GetFinanceInvoiceBatchBySetupIDAndStatusID(_careProviderFinanceInvoiceBatchSetupId1, 2);
            Assert.AreEqual(1, completedFinanceInvoiceBatchIds.Count);
            Assert.AreEqual(_cpFinanceInvoiceBatchId1, completedFinanceInvoiceBatchIds[0]);

            #endregion

            #region Update invoice status to completed

            //update invoice status to completed
            var _cpFinanceInvoiceId = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchIdAndInvoiceStatusId(_cpFinanceInvoiceBatchId1, 1)[0];

            #endregion

            #region Step 6

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicesSection();

            cpFinanceInvoicesPage
                .WaitForCPFinanceInvoicesPageToLoad()
                .ClickNewRecordButton();

            financeInvoiceRecordPage
                .WaitForCPFinanceInvoiceRecordPageToLoad()
                .ValidateEstablishmentFieldText(providerName2);

            //Add team1 to user and update default team to team1
            dbHelper.teamMember.CreateTeamMember(_teamId, _defaultLoginUserID, new DateTime(2020, 1, 1), null);
            dbHelper.systemUser.UpdateDefaultTeam(_defaultLoginUserID, _teamId);

            financeInvoiceRecordPage
                .WaitForCPFinanceInvoiceRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            cpFinanceInvoicesPage
                .WaitForCPFinanceInvoicesPageToLoad()
                .ClickNewRecordButton();

            financeInvoiceRecordPage
                .WaitForCPFinanceInvoiceRecordPageToLoad()
                .ValidateEstablishmentFieldText("");

            #endregion

            #region Step 7

            financeInvoiceRecordPage
                .ClickPersonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateLookInSelectedValue("All Active People With Finance Invoices")
                .ClickCloseButton();

            #endregion

            #region Step 8

            financeInvoiceRecordPage
                .WaitForCPFinanceInvoiceRecordPageToLoad()
                .ClickPayerLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookFor("Providers")
                .SearchAndSelectRecord(funderProviderName, funderProviderID);

            financeInvoiceRecordPage
                .WaitForCPFinanceInvoiceRecordPageToLoad()
                .ClickPersonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("All Active People")
                .SearchAndSelectRecord(firstName + " " + lastName, _personId);

            financeInvoiceRecordPage
                .WaitForCPFinanceInvoiceRecordPageToLoad()
                .ClickEstablishmentLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(providerName2, providerId2);

            financeInvoiceRecordPage
                .WaitForCPFinanceInvoiceRecordPageToLoad()
                .InsertTextOnChargesUpto_DateField(commonMethodsHelper.GetLastDayOfMonth(todayDate.AddYears(1)).ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnNetAmountField("10.00")
                .InsertTextOnVATAmountField("5.00")
                .ClickSaveButton();

            financeInvoiceRecordPage
                .WaitForCPFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusPicklistText("Brought Forward")
                .ValidatePayerFieldText(funderProviderName)
                .ValidatePersonFieldText(firstName + " " + lastName)
                .ValidateEstablishmentFieldText(providerName2)
                .ValidateChargesUpto_DateFieldText(commonMethodsHelper.GetLastDayOfMonth(todayDate.AddYears(1)).ToString("dd'/'MM'/'yyyy"))
                .ValidateNetAmountFieldText("10.00")
                .ValidateVatAmountFieldText("5.00")
                .ValidateGrossAmountFieldText("15.00")
                .ValidateIncludeInDebtOutstandingOptionsChecked(true);

            var _cpBroughtForwardInvoiceId = dbHelper.careProviderFinanceInvoice.GetByPayerAndFinanceInvoiceStatusId(funderProviderID, 5)[0];
            string invoiceNumber = (string)dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceById(_cpBroughtForwardInvoiceId, "invoicenumber")["invoicenumber"];

            #endregion

            #region Step 9

            financeInvoiceRecordPage
                .WaitForCPFinanceInvoiceRecordPageToLoad()
                .ValidateFinanceInvoiceBatchLinkFieldText("")
                .ValidateFinanceInvoiceBatchLookupButtonDisabled(true)
                .ValidateExtractLinkFieldText("")
                .ValidateExtractLookupButtonDisabled(true)
                .ValidateInvoiceStatusPicklistText("Brought Forward")
                .ValidateInvoiceStatusPicklistDisabled(true)
                .ValidatePayerFieldText(funderProviderName)
                .ValidatePayerFieldLookupButtonDisabled(true)
                .ValidateDebtorReferenceNumberFieldText(currentTimeString)
                .ValidateDebtorReferenceNumberFieldDisabled(true)
                .ValidateContractSchemeLinkFieldText("")
                .ValidateContractSchemeLookupButtonDisabled(true)
                .ValidateBatchGroupingLinkFieldText("")
                .ValidateBatchGroupingLookupButtonDisabled(true)
                .ValidateEstablishmentFieldText(providerName2)
                .ValidateEstablishmentLookupButtonDisabled(true)
                .ValidateFunderLinkFieldText("")
                .ValidateFunderLookupButtonDisabled(true)
                .ValidatePersonFieldText(firstName + " " + lastName)
                .ValidatePersonLinkFieldLookupButtonDisabled(true)
                .ValidateInvoiceNumberFieldText(invoiceNumber)
                .ValidateInvoiceNumberFieldDisabled(true)
                .ValidateAlternativeInvoiceNumberFieldText("")
                .ValidateAlternativeInvoiceNumberFieldDisabled(true)
                .ValidateChargesUpto_DateFieldText(commonMethodsHelper.GetLastDayOfMonth(todayDate.AddYears(1)).ToString("dd'/'MM'/'yyyy"))
                .ValidateChargesUpto_DateFieldDisabled(false)
                .ValidateInvoiceDeliveryStatusFieldText("")
                .ValidateInvoiceDeliveryStatusFieldDisabled(true)
                .ValidateInvoiceTextDetailsFieldText("")
                .ValidateInvoiceTextDetailsFieldDisabled(true)
                .ValidateNetAmountFieldText("10.00")
                .ValidateNetAmountFieldDisabled(false)
                .ValidateVatAmountFieldText("5.00")
                .ValidateVatAmountFieldDisabled(false)
                .ValidateGrossAmountFieldText("15.00")
                .ValidateGrossAmountFieldDisabled(true)
                .ValidateBalanceOutstandingFieldText("15.00")
                .ValidateBalanceOutstandingFieldDisabled(true)
                .ValidatePaymentsMadeFieldText("0.00")
                .ValidatePaymentsMadeFieldDisabled(true)
                .ValidateDebtWrittenOffFieldText("")
                .ValidateDebtWrittenOffFieldDisabled(true)
                .ValidateResponsibleTeamLinkFieldText(_teamName)
                .ValidateResponsibleTeamLookupButtonDisabled(true)
                .ValidateNoteTextFieldText("")
                .ValidateNoteTextFieldDisabled(false);

            financeInvoiceRecordPage
                .ValidateInvoiceFileFieldText(null);

            #endregion

            #region Step 10

            //we need to verify that auto-generated FIs has some mandatory fields - which will be blank for Manually created ones in that case, it should not throw any errors
            //this is covered in step 9 in which we verify that the values entered in mandatory fileds are saved when we create the FI record manually. So we can skip this step and verify that record is saved successfully and it exists.

            financeInvoiceRecordPage
                .ClickBackButton();

            cpFinanceInvoicesPage
                .WaitForCPFinanceInvoicesPageToLoad()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceInvoicesPageToLoad()
                .ValidateRecordIsPresent(_cpBroughtForwardInvoiceId.ToString(), true)
                .OpenRecord(_cpBroughtForwardInvoiceId);

            #endregion

            #region Step 11

            //Insert value in Charge Upto Date field = 1 day ahead from current value, Net Amount field and VAT Amount field, and Note Text field. and click Include in Debt Outstanding to false
            //Click Save button

            financeInvoiceRecordPage
                .WaitForCPFinanceInvoiceRecordPageToLoad()
                .InsertTextOnChargesUpto_DateField(commonMethodsHelper.GetLastDayOfMonth(todayDate.AddYears(1)).AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnNetAmountField("20.00")
                .InsertTextOnVATAmountField("10.00")
                .InsertTextOnNoteTextField("Test Note Text")
                .ClickIncludeInDebtOutstanding_NoOption()
                .ClickSaveButton();

            financeInvoiceRecordPage
                .WaitForCPFinanceInvoiceRecordPageToLoad()
                .ValidateChargesUpto_DateFieldText(commonMethodsHelper.GetLastDayOfMonth(todayDate.AddYears(1)).AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .ValidateNetAmountFieldText("20.00")
                .ValidateVatAmountFieldText("10.00")
                .ValidateGrossAmountFieldText("30.00")
                .ValidateIncludeInDebtOutstandingOptionsChecked(false)
                .ValidateNoteTextFieldText("Test Note Text");


            #endregion

        }

        #endregion

    }
}

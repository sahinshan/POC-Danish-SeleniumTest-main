using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Configuration.CPFinanceAdmin
{
    /// <summary>
    /// This class contains Automated UI test scripts for CP Finance > Contract Services (Clone)
    /// </summary>
    [TestClass]
    public class CPFinance_ContractServices_UITestCases : FunctionalTest
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
        private Guid cpSchedulingSetupId;

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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CPContractServiceBU");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                _teamName = "CPContractServiceTeam";
                _teamId = commonMethodsDB.CreateTeam(_teamName, null, _businessUnitId, "90400", "CareProvidersCS@careworkstempmail.com", _teamName, "020 123456");

                #endregion

                #region Create default system user

                _loginUsername = "ContractServiceUser1";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUsername, "ContractService", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationProviderId);

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

                #region Care Provider Scheduling Setup

                cpSchedulingSetupId = dbHelper.cPSchedulingSetup.GetAllActiveRecords().FirstOrDefault();
                dbHelper.cPSchedulingSetup.UpdateCheckStaffAvailability(cpSchedulingSetupId, 4); //Check and Offer Create

                #endregion

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

        #region https://advancedcsg.atlassian.net/browse/ACC-7648

        [TestProperty("JiraIssueID", "ACC-7750")]
        [Description("Test for Contract Service - Batching & Cloning 1. Step 1, 2a")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void CPContractServices_UITestMethod001()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Jay";
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

            #region Care Provider Extract Name

            //var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);
            //var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type: Generic Extract File

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

            #region Care Provider Person Contract

            DateTime personContractStartDate = new DateTime(2024, 1, 1);
            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, personContractStartDate, null, true);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                personContractStartDate, 1, 1, _careProviderRateUnitId);

            //dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            cpFinanceAdminPage
                .WaitForCPFinanceAdminPageToLoad()
                .ClickPersonContractsAreaButton()
                .ClickContractServicesButton();

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickEstablishmentLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("*" + currentTimeString)
                .TapSearchButton()
                .ClickAddSelectedButton(providerId.ToString());

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            cpContractServiceRecordPage
                .WaitForPageToLoad()
                .ValidateIsUsedInFinance_NoOptionChecked()
                .ValidateIsUsedInFinance_YesOptionNotChecked();

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            cpContractServiceRecordPage
                .ClickBackButton();

            cpContractServicesPage
                .WaitForPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            cpContractServiceRecordPage
                .WaitForPageToLoad()
                .ValidateIsUsedInFinance_YesOptionChecked()
                .ValidateIsUsedInFinance_NoOptionNotChecked();

            #endregion

            #region Step 2 - Spot: Verify that Contract Service has a matching FIBS set up (Contract Scheme and Batch Grouping) exists, then Used in Finance Invoice Batch is set to Yes

            cpContractServiceRecordPage
                .ValidateIsUsedInFinanceInvoiceBatch_YesOptionChecked()
                .ValidateIsUsedInFinanceInvoiceBatch_NoOptionNotChecked();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7751")]
        [Description("Test for Contract Service - Batching & Cloning 1. Step 2b, 3")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void CPContractServices_UITestMethod002()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Jay";
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

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 2, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name


            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

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

            #region Step 2 - Verify that when Contract Type = Block, Once Finance Transactions are associated with the Contract Service record, Used in Finance flag is set to Yes

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            cpFinanceAdminPage
                .WaitForCPFinanceAdminPageToLoad()
                .ClickPersonContractsAreaButton()
                .ClickContractServicesButton();

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickEstablishmentLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("*" + currentTimeString)
                .TapSearchButton()
                .ClickAddSelectedButton(providerId.ToString());

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            cpContractServiceRecordPage
                .WaitForPageToLoad()
                .ValidateIsUsedInFinance_YesOptionChecked()
                .ValidateIsUsedInFinance_NoOptionNotChecked();

            #endregion

            #region Step 3

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Contract Services")
                .ValidateSelectFieldOption("Used In Finance?")
                .ValidateSelectFieldOption("Used In Finance Invoice Batch Setup?")
                .ValidateSelectFieldOption("Batch Grouping");

            advanceSearchPage
                .SelectFilter("1", "Used In Finance?")
                .SelectFilter("1", "Used In Finance Invoice Batch Setup?")
                .SelectFilter("1", "Batch Grouping")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Default Care Provider Batch Grouping", _careProviderBatchGroupingId);

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad();

            advanceSearchPage
                .ClickColumnHeader(15) //sort by modified on asc
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(15) //sort by modified on desc
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(careProviderContractServiceId.ToString());





            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-7752")]
        [Description("Test for Contract Service - Batching & Cloning 1. Step 4, 5")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void CPContractServices_UITestMethod003()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Jay";
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

            #region Care Provider Extract Name

            //var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);
            //var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type: Generic Extract File

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

            #region Step 4

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            cpFinanceAdminPage
                .WaitForCPFinanceAdminPageToLoad()
                .ClickPersonContractsAreaButton()
                .ClickContractServicesButton();

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickEstablishmentLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("*" + currentTimeString)
                .TapSearchButton()
                .ClickAddSelectedButton(providerId.ToString());

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            cpContractServiceRecordPage
                .WaitForPageToLoad()
                .ValidateCloneExistingContractServiceRecordButtonVisible(true);

            #endregion

            #region Step 5

            cpContractServiceRecordPage
                .WaitForPageToLoad()
                .ClickCloneExistingContractServiceRecordButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ValidateCloneMessage("This process will clone the existing record to create a new Contract Service record. However, the new record needs to be different, as duplicates are not permitted, therefore change at least one of the following values.");

            cloningContractServicePopup
                .ValidateEstablishmentLookupVisibility(true)
                .ValidateEstablishmentLinkText(providerName)
                .ValidateContractSchemeLookupVisibility(true)
                .ValidateContractSchemeLinkText(careProviderContractScheme1Id, contractScheme1Name)
                .ValidateServiceLookupVisibility(true)
                .ValidateServiceLinkText(careProviderServiceName)
                .ValidateServiceDetailLookupVisibility(true)
                .ValidateBatchGroupingLookupVisibility(true)
                .ValidateBatchGroupingLinkText("Default Care Provider Batch Grouping")
                .ValidateCopyRatesRadioButtonsVisibility(true)
                .ValidateCopyRatesYesRadioButtonChecked(true);

            cloningContractServicePopup
                .ValidateServiceDetailLinkFieldText("");

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-7753")]
        [Description("Test for Contract Service - Batching & Cloning 1. Step 6")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void CPContractServices_UITestMethod004()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Jay";
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

            var providerName2 = "Establishment Provider2 " + currentTimeString;
            var providerId2 = commonMethodsDB.CreateProvider(providerName2, _teamId, 13, true, new DateTime(2020, 2, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            var contractScheme2Name = "CPCS2_" + currentTimeString;
            var contractScheme2Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme2Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme2Name, new DateTime(2020, 2, 1), contractScheme2Code, providerId2, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId1 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = dbHelper.careProviderServiceDetail.GetHighestCode() + 1;
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

            var cpContractServiceRatePeriodId = dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name

            //var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);
            //var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type: Generic Extract File

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

            #region Step 6

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            cpFinanceAdminPage
                .WaitForCPFinanceAdminPageToLoad()
                .ClickPersonContractsAreaButton()
                .ClickContractServicesButton();

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickEstablishmentLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("*" + currentTimeString)
                .TapSearchButton()
                .ClickAddSelectedButton(providerId.ToString());

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            cpContractServiceRecordPage
                .WaitForPageToLoad()
                .ClickCloneExistingContractServiceRecordButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickServiceDetailLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Care Provider Service Details Test 2")
                .TapSearchButton()
                .ClickAddSelectedButton(careProviderServiceDetailId.ToString());

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickCloneButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ValidateCloneNotificationMessageVisibility(true)
                .ValidateCloneNotificationMessageText("Contract Service cloned successfully.")
                .ValidateOpenNewRecordButtonIsVisible(true)
                .ClickOpenNewRecordButton();


            var contractServices = dbHelper.careProviderContractService.GetByEstablishmentId(providerId);
            Assert.AreEqual(2, contractServices.Count);
            var clonedContractServiceId = contractServices[0];

            var cpContractServiceRatePeriodId2 = dbHelper.careProviderContractServiceRatePeriod.GetByContractServiceID(clonedContractServiceId)[0];

            cpContractServiceRecordPage
                .WaitForPageToLoad(clonedContractServiceId.ToString())
                .ValidateEstablishmentProviderLinkFieldText(providerName)
                .ValidateContractSchemeLinkFieldText(contractScheme1Name)
                .ValidateFunderProviderLinkText(funderProviderName)
                .ValidateResponsibleUserLinkFieldText("ContractService User1")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateServiceLinkFieldText(careProviderServiceName)
                .ValidateServiceDetailLinkFieldText(careProviderServiceDetailName)
                .ValidateRateUnitLinkFieldText("Default Care Provider Rate Unit")
                .ValidateVATCodeLinkFieldText("Standard Rated")
                .ValidateBatchGroupingLinkFieldText("Default Care Provider Batch Grouping")
                .ValidateIsUsedInFinanceInvoiceBatch_YesOptionChecked()
                .ValidateIsUsedInFinanceInvoiceBatch_NoOptionNotChecked()
                .ClickRatesToRecordTab();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad(clonedContractServiceId.ToString())
                .ValidateRecordPresent(cpContractServiceRatePeriodId2, true)
                .ValidateRecordCellText(cpContractServiceRatePeriodId2, 2, "01/01/2023")
                .ValidateRecordCellText(cpContractServiceRatePeriodId2, 4, "Default Care Provider Rate Unit")
                .ValidateRecordCellText(cpContractServiceRatePeriodId2, 5, "10.00");

            cpContractServiceRecordPage
                .WaitForPageToLoad(clonedContractServiceId.ToString())
                .ClickDetailsTab();

            cpContractServiceRecordPage
                .ClickCloneExistingContractServiceRecordButton();

            cloningContractServicePopup
                .WaitForPopupToLoad(clonedContractServiceId.ToString())
                .ClickEstablishmentLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(providerName2)
                .TapSearchButton()
                .SelectResultElement(providerId2.ToString());

            cloningContractServicePopup
                .WaitForPopupToLoad(clonedContractServiceId.ToString())
                .ClickContractSchemeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(contractScheme2Name)
                .TapSearchButton()
                .ClickAddSelectedButton(careProviderContractScheme2Id.ToString());

            cloningContractServicePopup
                .WaitForPopupToLoad(clonedContractServiceId.ToString())
                .ClickServiceDetailLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Care Provider Service Details Test 2")
                .TapSearchButton()
                .ClickAddSelectedButton(careProviderServiceDetailId.ToString());

            cloningContractServicePopup
                .WaitForPopupToLoad(clonedContractServiceId.ToString())
                .ClickCopyRatesNoRadioButton()
                .ClickCloneButton();

            cloningContractServicePopup
                .WaitForPopupToLoad(clonedContractServiceId.ToString())
                .ValidateCloneNotificationMessageVisibility(true)
                .ValidateCloneNotificationMessageText("Contract Service cloned successfully.")
                .ValidateOpenNewRecordButtonIsVisible(true)
                .ClickOpenNewRecordButton();


            contractServices = dbHelper.careProviderContractService.GetByEstablishmentId(providerId);
            Assert.AreEqual(2, contractServices.Count);
            contractServices = dbHelper.careProviderContractService.GetByEstablishmentId(providerId2);
            Assert.AreEqual(1, contractServices.Count);
            var clonedContractServiceId2 = contractServices[0];

            cpContractServiceRecordPage
                .WaitForPageToLoad(clonedContractServiceId2.ToString())
                .ValidateEstablishmentProviderLinkFieldText(providerName2)
                .ValidateContractSchemeLinkFieldText(contractScheme2Name)
                .ValidateFunderProviderLinkText(funderProviderName)
                .ValidateResponsibleUserLinkFieldText("ContractService User1")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateServiceLinkFieldText(careProviderServiceName)
                .ValidateServiceDetailLinkFieldText(careProviderServiceDetailName)
                .ValidateRateUnitLinkFieldText("Default Care Provider Rate Unit")
                .ValidateVATCodeLinkFieldText("Standard Rated")
                .ValidateBatchGroupingLinkFieldText("Default Care Provider Batch Grouping")
                .ValidateIsUsedInFinanceInvoiceBatch_NoOptionChecked()
                .ValidateIsUsedInFinanceInvoiceBatch_YesOptionNotChecked()
                .ClickRatesToRecordTab();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad(clonedContractServiceId2.ToString())
                .ValidateNoRecordMessageVisible(true);


            #endregion


        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-7681

        [TestProperty("JiraIssueID", "ACC-7756")]
        [Description("Test for Contract Service - Batching & Cloning 2. Step 7.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void CPContractServices_UITestMethod005()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Aadi";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Provider

            var providerName = "Establishment ProviderA1 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority
            dbHelper.provider.UpdateDebtorNumber(funderProviderID, currentTimeString);

            var providerName2 = "Establishment ProviderA2 " + currentTimeString;
            var providerId2 = commonMethodsDB.CreateProvider(providerName2, _teamId, 13, true, new DateTime(2020, 2, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCSA1_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            var contractScheme2Name = "CPCSA2_" + currentTimeString;
            var contractScheme2Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme2Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme2Name, new DateTime(2020, 2, 1), contractScheme2Code, providerId2, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId1 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = dbHelper.careProviderServiceDetail.GetHighestCode() + 1;
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

            var cpContractServiceRatePeriodId = dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

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

            #region Step 7

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            cpFinanceAdminPage
                .WaitForCPFinanceAdminPageToLoad()
                .ClickPersonContractsAreaButton()
                .ClickContractServicesButton();

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickEstablishmentLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("*" + currentTimeString)
                .TapSearchButton()
                .ClickAddSelectedButton(providerId.ToString());

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            cpContractServiceRecordPage
                .WaitForPageToLoad()
                .ClickCloneExistingContractServiceRecordButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickEstablishmentLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(providerName2)
                .TapSearchButton()
                .SelectResultElement(providerId2.ToString());

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ValidateContractSchemeInContractSchemeLinkField(careProviderContractScheme1Id, false)
                .ValidateContractSchemeText(contractScheme1Name, false);

            cloningContractServicePopup
                .ClickContractSchemeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndValidateRecordPresentOrNot(contractScheme1Name, careProviderContractScheme1Id, false)
                .SearchAndValidateRecordPresentOrNot(contractScheme2Name, careProviderContractScheme2Id, true)
                .ClickAddSelectedButton(careProviderContractScheme2Id.ToString());

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ValidateContractSchemeInContractSchemeLinkField(careProviderContractScheme2Id, true)
                .ValidateContractSchemeText(contractScheme2Name, true);

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-7757")]
        [Description("Test for Contract Service - Batching & Cloning 2. Step 8a.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void CPContractServices_UITestMethod006()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Aadi";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Provider

            var providerName = "Establishment ProviderA1 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority
            dbHelper.provider.UpdateDebtorNumber(funderProviderID, currentTimeString);

            #endregion

            #region Booking Type 2

            dbHelper.cPSchedulingSetup.UpdateUpdateBookingEndDayDateTime(cpSchedulingSetupId, true);

            var _bookingType2Id = commonMethodsDB.CreateCPBookingType("BTC2 841a", 2, 240, new TimeSpan(6, 0, 0), new TimeSpan(10, 0, 0), 1, false, 1000, null, null, null);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType2Id, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCSA1_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId1 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, true);

            var careProviderServiceName2 = "CPS B " + currentTimeString;
            var careProviderServiceCode2 = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId2 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName2, new DateTime(2020, 1, 1), careProviderServiceCode2, null, true);

            var careProviderServiceName3 = "CPS C " + currentTimeString;
            var careProviderServiceCode3 = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId3 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName3, new DateTime(2020, 1, 1), careProviderServiceCode3, null, false);


            #endregion

            #region Care Provider Service Detail

            var code = dbHelper.careProviderServiceDetail.GetHighestCode() + 1;
            var careProviderServiceDetailName = "Care Provider Service Details Test 2";
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, code, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(careProviderServiceId1, _teamId, null, _bookingType2Id, null, "");

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

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _bookingType2Id, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            var cpContractServiceRatePeriodId = dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

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

            #region Step 8

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            cpFinanceAdminPage
                .WaitForCPFinanceAdminPageToLoad()
                .ClickPersonContractsAreaButton()
                .ClickContractServicesButton();

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .ClickEstablishmentLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("*" + currentTimeString)
                .TapSearchButton()
                .ClickAddSelectedButton(providerId.ToString());

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            cpContractServiceRecordPage
                .WaitForPageToLoad()
                .ClickCloneExistingContractServiceRecordButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickServiceLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(careProviderServiceName)
                .TapSearchButton()
                .ValidateResultElementPresent(careProviderServiceId1)
                .SearchAndValidateRecordPresentOrNot(careProviderServiceName2, careProviderServiceId2, true)
                .SearchAndValidateRecordPresentOrNot(careProviderServiceName3, careProviderServiceId3, false);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7758")]
        [Description("Test for Contract Service - Batching & Cloning 2. Step 8b.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void CPContractServices_UITestMethod007()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Aadi";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Provider

            var providerName = "Establishment ProviderA1 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority
            dbHelper.provider.UpdateDebtorNumber(funderProviderID, currentTimeString);

            var providerName2 = "Establishment ProviderA2 " + currentTimeString;
            var providerId2 = commonMethodsDB.CreateProvider(providerName2, _teamId, 13, true, new DateTime(2020, 2, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCSA1_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            var contractScheme2Name = "CPCSA2_" + currentTimeString;
            var contractScheme2Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme2Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme2Name, new DateTime(2020, 2, 1), contractScheme2Code, providerId2, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId1 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, false);

            var careProviderServiceName2 = "CPS B " + currentTimeString;
            var careProviderServiceCode2 = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId2 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName2, new DateTime(2020, 1, 1), careProviderServiceCode2, null, false);

            var careProviderServiceName3 = "CPS C " + currentTimeString;
            var careProviderServiceCode3 = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId3 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName3, new DateTime(2020, 1, 1), careProviderServiceCode3, null, true);


            #endregion

            #region Care Provider Service Detail

            var code = dbHelper.careProviderServiceDetail.GetHighestCode() + 1;
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

            var cpContractServiceRatePeriodId = dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

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

            #region Step 8

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            cpFinanceAdminPage
                .WaitForCPFinanceAdminPageToLoad()
                .ClickPersonContractsAreaButton()
                .ClickContractServicesButton();

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickEstablishmentLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("*" + currentTimeString)
                .TapSearchButton()
                .ClickAddSelectedButton(providerId.ToString());

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            cpContractServiceRecordPage
                .WaitForPageToLoad()
                .ClickCloneExistingContractServiceRecordButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickServiceLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(careProviderServiceName)
                .TapSearchButton()
                .ValidateResultElementPresent(careProviderServiceId1)
                .SearchAndValidateRecordPresentOrNot(careProviderServiceName2, careProviderServiceId2, true)
                .SearchAndValidateRecordPresentOrNot(careProviderServiceName3, careProviderServiceId3, false);

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-7759")]
        [Description("Test for Contract Service - Batching & Cloning 2. Step 9.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void CPContractServices_UITestMethod008()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Aadi";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Provider

            var providerName = "Establishment ProviderA1 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority
            dbHelper.provider.UpdateDebtorNumber(funderProviderID, currentTimeString);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCSA1_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            var contractScheme2Name = "CPCSA2_" + currentTimeString;
            var contractScheme2Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme2Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme2Name, new DateTime(2020, 1, 1), contractScheme2Code, providerId, providerId, false, true);


            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId1 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = dbHelper.careProviderServiceDetail.GetHighestCode() + 1;
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

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 2, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            var cpContractServiceRatePeriodId = dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

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

            #region Step 9

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            cpFinanceAdminPage
                .WaitForCPFinanceAdminPageToLoad()
                .ClickPersonContractsAreaButton()
                .ClickContractServicesButton();

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickEstablishmentLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("*" + currentTimeString)
                .TapSearchButton()
                .ClickAddSelectedButton(providerId.ToString());

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            cpContractServiceRecordPage
                .WaitForPageToLoad()
                .ClickCloneExistingContractServiceRecordButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickContractSchemeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("*" + currentTimeString)
                .TapSearchButton()
                .ValidateResultElementPresent(careProviderContractScheme1Id)
                .ValidateResultElementNotPresent(careProviderContractScheme2Id);

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-7760")]
        [Description("Test for Contract Service - Batching & Cloning 2. Step 10 - 12.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void CPContractServices_UITestMethod009()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Aadi";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Provider

            var providerName = "Establishment ProviderA1 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority
            dbHelper.provider.UpdateDebtorNumber(funderProviderID, currentTimeString);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCSA1_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A1 " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId1 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, false);

            var careProviderServiceName2 = "CPS A2 " + currentTimeString;
            var careProviderServiceCode2 = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId2 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName2, new DateTime(2020, 1, 1), careProviderServiceCode2, null, false);


            #endregion

            #region Care Provider Service Detail

            var code = dbHelper.careProviderServiceDetail.GetHighestCode() + 1;
            var careProviderServiceDetailName = "Care Provider Service Details Test 2";
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, code, null, new DateTime(2020, 1, 1));

            var code2 = dbHelper.careProviderServiceDetail.GetHighestCode() + 1;
            var careProviderServiceDetailName2 = "Care Provider Service Details Test 3";
            var careProviderServiceDetailId2 = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName2, code2, null, new DateTime(2020, 2, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(careProviderServiceId1, _teamId, careProviderServiceDetailId, null, null, "");
            commonMethodsDB.CreateCareProviderServiceMapping(careProviderServiceId2, _teamId, careProviderServiceDetailId2, null, null, "");

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

            var cpContractServiceRatePeriodId = dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

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

            #region Step 10

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            cpFinanceAdminPage
                .WaitForCPFinanceAdminPageToLoad()
                .ClickPersonContractsAreaButton()
                .ClickContractServicesButton();

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickEstablishmentLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("*" + currentTimeString)
                .TapSearchButton()
                .ClickAddSelectedButton(providerId.ToString());

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            cpContractServiceRecordPage
                .WaitForPageToLoad()
                .ClickCloneExistingContractServiceRecordButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickServiceLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("*" + currentTimeString, careProviderServiceId2);

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickServiceDetailLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(careProviderServiceDetailName2)
                .TapSearchButton()
                .ValidateResultElementPresent(careProviderServiceDetailId2);

            #endregion

            #region Step 11

            lookupPopup
                .ClickAddSelectedButton(careProviderServiceDetailId2.ToString());

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickCloneButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ValidateOpenNewRecordButtonIsVisible(true)
                .ValidateCancelButtonVisibility(true)
                .ClickCancelButton();

            cpContractServiceRecordPage
                .WaitForPageToLoad()
                .ClickCloneExistingContractServiceRecordButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickServiceDetailLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(careProviderServiceDetailName)
                .TapSearchButton()
                .ClickAddSelectedButton(careProviderServiceDetailId.ToString());

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickCloneButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ValidateOpenNewRecordButtonIsVisible(true)
                .ValidateCancelButtonVisibility(true)
                .ClickOpenNewRecordButton();

            var contractServices = dbHelper.careProviderContractService.GetByEstablishmentId(providerId);
            Assert.AreEqual(3, contractServices.Count);
            var clonedContractServiceId = contractServices[0];

            cpContractServiceRecordPage
                .WaitForPageToLoad(clonedContractServiceId.ToString())
                .ClickCloneExistingContractServiceRecordButton();

            #endregion

            #region Step 12

            cloningContractServicePopup
                .WaitForPopupToLoad(clonedContractServiceId.ToString())
                .ClickCloneButton();

            cloningContractServicePopup
                .WaitForPopupToLoad(clonedContractServiceId.ToString())
                .ValidateCloneNotificationMessageVisibility(true)
                .ValidateCloneNotificationMessageText("A record already exists with the same combination of: Contract Scheme / Service / Service Detail or Booking Type / Booking Type Grouping\r\n- " + contractScheme1Name + " / " + careProviderServiceName + " / " + careProviderServiceDetailName + " which is not permitted, as it would create a duplicate record. There could be other combinations that are also not permitted.\r\nPlease correct as necessary.");

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-7761")]
        [Description("Test for Contract Service - Batching & Cloning 2. Step 13.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void CPContractServices_UITestMethod010()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Aadi";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Provider

            var providerName = "Establishment ProviderA1 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority
            dbHelper.provider.UpdateDebtorNumber(funderProviderID, currentTimeString);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCSA1_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A1 " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId1 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = dbHelper.careProviderServiceDetail.GetHighestCode() + 1;
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

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, true);

            #endregion

            #region Contract Service Rate Period

            var cpContractServiceRatePeriodId = dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

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

            #region Step 13

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            cpFinanceAdminPage
                .WaitForCPFinanceAdminPageToLoad()
                .ClickPersonContractsAreaButton()
                .ClickContractServicesButton();

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickEstablishmentLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("*" + currentTimeString)
                .TapSearchButton()
                .ClickAddSelectedButton(providerId.ToString());

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            cpContractServiceRecordPage
                .WaitForPageToLoad()
                .ClickCloneExistingContractServiceRecordButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ValidateCopyRatesRadioButtonsVisibility(false);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-7732

        [TestProperty("JiraIssueID", "ACC-7762")]
        [Description("Test for Contract Service - Batching & Cloning 4. Step 14a.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void CPContractServices_UITestMethod011()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Aadi";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Provider

            var providerName = "Establishment ProviderA1 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority
            dbHelper.provider.UpdateDebtorNumber(funderProviderID, currentTimeString);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCSA1_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            var contractScheme2Name = "CPCSA2_" + currentTimeString;
            var contractScheme2Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme2Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme2Name, new DateTime(2020, 1, 1), contractScheme2Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A1 " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId1 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = dbHelper.careProviderServiceDetail.GetHighestCode() + 1;
            var careProviderServiceDetailName = "Care Provider Service Details Test 2";
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, code, null, new DateTime(2020, 1, 1));

            var code2 = dbHelper.careProviderServiceDetail.GetHighestCode() + 1;
            var careProviderServiceDetailName2 = "Care Provider Service Details Test 3";
            var careProviderServiceDetailId2 = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName2, code2, null, new DateTime(2020, 2, 1));


            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(careProviderServiceId1, _teamId, careProviderServiceDetailId, null, null, "");
            commonMethodsDB.CreateCareProviderServiceMapping(careProviderServiceId1, _teamId, careProviderServiceDetailId2, null, null, "");

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

            var cpContractServiceRatePeriodId = dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

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

            #region Step 14

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            cpFinanceAdminPage
                .WaitForCPFinanceAdminPageToLoad()
                .ClickPersonContractsAreaButton()
                .ClickContractServicesButton();

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickEstablishmentLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("*" + currentTimeString)
                .TapSearchButton()
                .ClickAddSelectedButton(providerId.ToString());

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            cpContractServiceRecordPage
                .WaitForPageToLoad()
                .ClickCloneExistingContractServiceRecordButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickContractSchemeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectElementAndClickAddRecordsButton("*" + currentTimeString, careProviderContractScheme1Id.ToString())
                .ClickAddSelectedButton(careProviderContractScheme2Id.ToString());

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickServiceDetailLookupButton();

            //lookupPopup
            //    .WaitForLookupPopupToLoad()
            //    .TypeSearchQuery(careProviderServiceDetailName)
            //    .TapSearchButton()
            //    .ClickAddSelectedButton(careProviderServiceDetailId.ToString());

            lookupPopup
                .WaitForLookupPopupToLoad()
                //.TypeSearchQuery(careProviderServiceDetailName)
                //.TapSearchButton()
                .SelectElementAndClickAddRecordsButton(careProviderServiceDetailName, careProviderServiceDetailId.ToString())
                //.TypeSearchQuery(careProviderServiceDetailName2)
                //.TapSearchButton()
                .SelectElementAndClickAddRecordsButton(careProviderServiceDetailName2, careProviderServiceDetailId2.ToString())
                .ClickOkButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickCloneButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ValidateCloneNotificationMessageVisibility(true)
                .ValidateCloneNotificationMessageText("Contract Service cloned successfully.")
                .ValidateOpenNewRecordButtonIsVisible(false)
                .ValidateCancelButtonVisibility(true);

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickCancelButton();

            cpContractServiceRecordPage
                .WaitForPageToLoad()
                .ClickBackButton();

            var contractServices = dbHelper.careProviderContractService.GetByEstablishmentId(providerId);
            Assert.AreEqual(5, contractServices.Count);

            var clonedContractServiceId1 = contractServices[0];
            var clonedContractServiceId2 = contractServices[1];
            var clonedContractServiceId3 = contractServices[2];
            var clonedContractServiceId4 = contractServices[3];

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickSearchButton()
                .WaitForPageToLoad()
                .ValidateRecordIsPresent(clonedContractServiceId1.ToString(), true)
                .ValidateRecordIsPresent(clonedContractServiceId2.ToString(), true)
                .ValidateRecordIsPresent(clonedContractServiceId3.ToString(), true)
                .ValidateRecordIsPresent(clonedContractServiceId4.ToString(), true)
                .ValidateRecordIsPresent(careProviderContractServiceId.ToString(), true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7763")]
        [Description("Test for Contract Service - Batching & Cloning 4. Step 14b.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void CPContractServices_UITestMethod012()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Aadi";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Provider

            var providerName = "Establishment ProviderA1 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority
            dbHelper.provider.UpdateDebtorNumber(funderProviderID, currentTimeString);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCSA1_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            var contractScheme2Name = "CPCSA2_" + currentTimeString;
            var contractScheme2Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme2Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme2Name, new DateTime(2020, 1, 1), contractScheme2Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS B1 " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId1 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = dbHelper.careProviderServiceDetail.GetHighestCode() + 1;
            var careProviderServiceDetailName = "Care Provider Service Details Test 2";
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, code, null, new DateTime(2020, 1, 1));

            var code2 = dbHelper.careProviderServiceDetail.GetHighestCode() + 1;
            var careProviderServiceDetailName2 = "Care Provider Service Details Test 3";
            var careProviderServiceDetailId2 = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName2, code2, null, new DateTime(2020, 2, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(careProviderServiceId1, _teamId, careProviderServiceDetailId, null, null, "");
            commonMethodsDB.CreateCareProviderServiceMapping(careProviderServiceId1, _teamId, careProviderServiceDetailId2, null, null, "");

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

            var cpContractServiceRatePeriodId = dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

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

            #region Step 14

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            cpFinanceAdminPage
                .WaitForCPFinanceAdminPageToLoad()
                .ClickPersonContractsAreaButton()
                .ClickContractServicesButton();

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickEstablishmentLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("*" + currentTimeString)
                .TapSearchButton()
                .ClickAddSelectedButton(providerId.ToString());

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            cpContractServiceRecordPage
                .WaitForPageToLoad()
                .ClickCloneExistingContractServiceRecordButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickServiceDetailLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectElementAndClickAddRecordsButton(careProviderServiceDetailName, careProviderServiceDetailId.ToString())
                .SelectElementAndClickAddRecordsButton(careProviderServiceDetailName2, careProviderServiceDetailId2.ToString())
                .ClickOkButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickCloneButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ValidateCloneNotificationMessageVisibility(true)
                .ValidateCloneNotificationMessageText("Contract Service cloned successfully.")
                .ValidateOpenNewRecordButtonIsVisible(false)
                .ValidateCancelButtonVisibility(true);

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickCancelButton();

            cpContractServiceRecordPage
                .WaitForPageToLoad()
                .ClickBackButton();

            var contractServices = dbHelper.careProviderContractService.GetByEstablishmentId(providerId);
            Assert.AreEqual(3, contractServices.Count);

            var clonedContractServiceId1 = contractServices[0];
            var clonedContractServiceId2 = contractServices[1];

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickSearchButton()
                .WaitForPageToLoad()
                .ValidateRecordIsPresent(clonedContractServiceId1.ToString(), true)
                .ValidateRecordIsPresent(clonedContractServiceId2.ToString(), true)
                .ValidateRecordIsPresent(careProviderContractServiceId.ToString(), true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7764")]
        [Description("Test for Contract Service - Batching & Cloning 4. Step 15a. When Used in Finance fields = Yes")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void CPContractServices_UITestMethod013()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Aadi";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Provider

            var providerName = "Establishment ProviderA1 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority
            dbHelper.provider.UpdateDebtorNumber(funderProviderID, currentTimeString);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCSA1_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS B1 " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId1 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = dbHelper.careProviderServiceDetail.GetHighestCode() + 1;
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

            var cpContractServiceRatePeriodId = dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

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

            #region Step 15

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            cpFinanceAdminPage
                .WaitForCPFinanceAdminPageToLoad()
                .ClickPersonContractsAreaButton()
                .ClickContractServicesButton();

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickEstablishmentLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("*" + currentTimeString)
                .TapSearchButton()
                .ClickAddSelectedButton(providerId.ToString());

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            cpContractServiceRecordPage
                .WaitForPageToLoad()
                .ClickCloneExistingContractServiceRecordButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickServiceDetailLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectElementAndClickAddRecordsButton(careProviderServiceDetailName, careProviderServiceDetailId.ToString())
                .ClickOkButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickCloneButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickOpenNewRecordButton();

            var contractServices = dbHelper.careProviderContractService.GetByEstablishmentId(providerId);
            Assert.AreEqual(2, contractServices.Count);

            var clonedContractServiceId = contractServices[0];

            cpContractServiceRecordPage
                .WaitForPageToLoad(clonedContractServiceId.ToString())
                .ValidateResponsibleUserLookupButtonDisabled(false)
                .ValidateNoteTextDisabled(false)
                .ValidateIsRoomsApplyDisabled(false)
                .ValidateInactiveOptionsDisabled(false)
                .ValidateIsPermitRateOverrideOptionsAreDisabled(false)
                .ValidateCareproviderVatCodeLookupButtonDisabled(true)
                .ValidateCareproviderBatchGroupingLookupButtonDisabled(true)
                .ValidateIsNetIncomeDisabled(false)
                .ValidateContractServiceAdjustedDaysDisabled(true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7765")]
        [Description("Test for Contract Service - Batching & Cloning 4. Step 15b. When Used in Finance fields = No")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void CPContractServices_UITestMethod014()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Aadi";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Provider

            var providerName = "Establishment ProviderC1 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority
            dbHelper.provider.UpdateDebtorNumber(funderProviderID, currentTimeString);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCSC1_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS C1 " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId1 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = dbHelper.careProviderServiceDetail.GetHighestCode() + 1;
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

            #region Care Provider Person Contract

            DateTime personContractStartDate = new DateTime(2024, 1, 1);
            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, personContractStartDate, null, true);

            #endregion

            #region Care Provider Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                personContractStartDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Step 15

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            cpFinanceAdminPage
                .WaitForCPFinanceAdminPageToLoad()
                .ClickPersonContractsAreaButton()
                .ClickContractServicesButton();

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickEstablishmentLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("*" + currentTimeString)
                .TapSearchButton()
                .ClickAddSelectedButton(providerId.ToString());

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            cpContractServiceRecordPage
                .WaitForPageToLoad()
                .ClickCloneExistingContractServiceRecordButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickServiceDetailLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectElementAndClickAddRecordsButton(careProviderServiceDetailName, careProviderServiceDetailId.ToString())
                .ClickOkButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickCloneButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickOpenNewRecordButton();

            var contractServices = dbHelper.careProviderContractService.GetByEstablishmentId(providerId);
            Assert.AreEqual(2, contractServices.Count);

            var clonedContractServiceId = contractServices[0];

            cpContractServiceRecordPage
                .WaitForPageToLoad(clonedContractServiceId.ToString())
                .ValidateResponsibleUserLookupButtonDisabled(false)
                .ValidateNoteTextDisabled(false)
                .ValidateIsRoomsApplyDisabled(false)
                .ValidateInactiveOptionsDisabled(false)
                .ValidateIsPermitRateOverrideOptionsAreDisabled(false)
                .ValidateCareproviderVatCodeLookupButtonDisabled(false)
                .ValidateCareproviderBatchGroupingLookupButtonDisabled(false)
                .ValidateIsNetIncomeDisabled(false)
                .ValidateContractServiceAdjustedDaysDisabled(false);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7766")]
        [Description("Test for Contract Service - Batching & Cloning 4. Step 15c.When Contract Type = Block")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void CPContractServices_UITestMethod015()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Aadi";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Provider

            var providerName = "Establishment ProviderC1 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority
            dbHelper.provider.UpdateDebtorNumber(funderProviderID, currentTimeString);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCSC1_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS C1 " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId1 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = dbHelper.careProviderServiceDetail.GetHighestCode() + 1;
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

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 2, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Person Contract

            DateTime personContractStartDate = new DateTime(2024, 1, 1);
            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, personContractStartDate, null, true);

            #endregion

            #region Care Provider Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                personContractStartDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Step 15

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            cpFinanceAdminPage
                .WaitForCPFinanceAdminPageToLoad()
                .ClickPersonContractsAreaButton()
                .ClickContractServicesButton();

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickEstablishmentLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("*" + currentTimeString)
                .TapSearchButton()
                .ClickAddSelectedButton(providerId.ToString());

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            cpContractServiceRecordPage
                .WaitForPageToLoad()
                .ClickCloneExistingContractServiceRecordButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickServiceDetailLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectElementAndClickAddRecordsButton(careProviderServiceDetailName, careProviderServiceDetailId.ToString())
                .ClickOkButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickCloneButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickOpenNewRecordButton();

            var contractServices = dbHelper.careProviderContractService.GetByEstablishmentId(providerId);
            Assert.AreEqual(2, contractServices.Count);

            var clonedContractServiceId = contractServices[0];

            cpContractServiceRecordPage
                .WaitForPageToLoad(clonedContractServiceId.ToString())
                .ValidateResponsibleUserLookupButtonDisabled(false)
                .ValidateNoteTextDisabled(false)
                .ValidateIsRoomsApplyDisabled(false)
                .ValidateInactiveOptionsDisabled(false)
                .ValidateCareproviderVatCodeLookupButtonDisabled(false)
                .ValidateCareproviderBatchGroupingLookupButtonDisabled(false)
                .ValidateIsNetIncomeDisabled(true)
                .ValidateDateToCommenceBlockChargingDisabled(false)
                .ValidateDateToCommenceBlockChargingDatePickerDisabled(false)
                .ValidateContractServiceAdjustedDaysDisabled(true);

            cpContractServiceRecordPage
                .ValidateIsPermitRateOverrideOptionsAreDisabled(true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7767")]
        [Description("Test for Contract Service - Batching & Cloning 4. Step 16. When Used in Finance Invoice Batch = Yes, Used in Finance = No, Contract Type = Block")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void CPContractServices_UITestMethod016()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Aadi";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Provider

            var providerName = "Establishment ProviderD1 " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority
            dbHelper.provider.UpdateDebtorNumber(funderProviderID, currentTimeString);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCSD1_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS D1 " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId1 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = dbHelper.careProviderServiceDetail.GetHighestCode() + 1;
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

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 2, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            var cpContractServiceRatePeriodId = dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

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

            #region Care Provider Person Contract

            DateTime personContractStartDate = new DateTime(2024, 1, 1);
            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, personContractStartDate, null, true);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                personContractStartDate, 1, 1, _careProviderRateUnitId);

            #endregion

            #region Step 16

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            cpFinanceAdminPage
                .WaitForCPFinanceAdminPageToLoad()
                .ClickPersonContractsAreaButton()
                .ClickContractServicesButton();

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickEstablishmentLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("*" + currentTimeString)
                .TapSearchButton()
                .ClickAddSelectedButton(providerId.ToString());

            cpContractServicesPage
                .WaitForPageToLoad()
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            cpContractServiceRecordPage
                .WaitForPageToLoad()
                .ClickCloneExistingContractServiceRecordButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickServiceDetailLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectElementAndClickAddRecordsButton(careProviderServiceDetailName, careProviderServiceDetailId.ToString())
                .ClickOkButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickCloneButton();

            cloningContractServicePopup
                .WaitForPopupToLoad()
                .ClickOpenNewRecordButton();

            var contractServices = dbHelper.careProviderContractService.GetByEstablishmentId(providerId);
            Assert.AreEqual(2, contractServices.Count);

            var clonedContractServiceId = contractServices[0];

            cpContractServiceRecordPage
                .WaitForPageToLoad(clonedContractServiceId.ToString())
                .ValidateResponsibleUserLookupButtonDisabled(false)
                .ValidateNoteTextDisabled(false)
                .ValidateIsRoomsApplyDisabled(false)
                .ValidateInactiveOptionsDisabled(false)
                .ValidateIsPermitRateOverrideOptionsAreDisabled(true);

            cpContractServiceRecordPage
                .ValidateDateToCommenceBlockChargingDisabled(false)
                .ValidateDateToCommenceBlockChargingDatePickerDisabled(false);

            cpContractServiceRecordPage
                .ValidateDatetocommenceblockchargingText("")
                .ValidateCareproviderVatCodeLookupButtonDisabled(true)
                .ValidateCareproviderBatchGroupingLookupButtonDisabled(true)
                .ValidateIsNetIncomeDisabled(true)
                .ValidateContractServiceAdjustedDaysDisabled(true);


            #endregion

        }

        #endregion
    }
}

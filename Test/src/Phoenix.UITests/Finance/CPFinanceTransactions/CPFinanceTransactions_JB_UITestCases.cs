using Microsoft.Office.Interop.Word;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Providers
{
    /// <summary>
    /// This class contains Automated UI test scripts for Provider Type
    /// </summary>
    [TestClass]
    public class CPFinanceTransactions_JB_UITestCases : FunctionalTest
    {
        #region Properties

        private Guid authenticationproviderid;
        private Guid _languageId;
        private Guid _businessUnitId;
        private string _teamName;
        private Guid _teamId;
        private Guid _defaultLoginUserID;
        private string _defaultLoginUserName;
        private string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");
        private string currentDateString = DateTime.Now.ToString("yyyyMMdd");

        internal Guid _recurrencePattern_Every1WeekMondayId;
        internal Guid _recurrencePattern_Every1WeekTuesdayId;
        internal Guid _recurrencePattern_Every1WeekWednesdayId;
        internal Guid _recurrencePattern_Every1WeekThursdayId;
        internal Guid _recurrencePattern_Every1WeekFridayId;
        internal Guid _recurrencePattern_Every1WeekSaturdayId;
        internal Guid _recurrencePattern_Every1WeekSundayId;

        #endregion

        #region Private Methods

        private void ExecuteScheduleJob(string ScheduleJobName)
        {
            System.Threading.Thread.Sleep(10000);

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName(ScheduleJobName).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            System.Threading.Thread.Sleep(2000);

            dbHelper = new DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

        }

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
                #region Authentication

                authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").First();

                #endregion

                #region SDK API User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];
                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);

                #endregion

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CPFTBU1 " + currentDateString);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                _teamName = "CPFTT1 " + currentDateString;
                _teamId = commonMethodsDB.CreateTeam(_teamName, null, _businessUnitId, "90400", "CPFT1_" + currentDateString + "@careworkstempmail.com", _teamName, "020 123456");

                #endregion

                #region Create default system user

                _defaultLoginUserName = "financetransactions_user01";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_defaultLoginUserName, "Finance Transactions", "User 01", "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }


        #region https://advancedcsg.atlassian.net/browse/ACC-7202

        [TestProperty("JiraIssueID", "ACC-7367")]
        [Description("Step(s) 1 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases001()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-891 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-891", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 7);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 1), null, true);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 1), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Schdeduled job for "Process CP Finance Transaction Triggers"

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transactions

            var fibsTriggers = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(1, fibsTriggers.Count);

            var financeTransactionId = fibsTriggers[0];
            var transactionNumber = (int)(dbHelper.careProviderFinanceTransaction.GetFinanceTransactionById(financeTransactionId, "careproviderfinancetransactionnumber")["careproviderfinancetransactionnumber"]);

            #endregion

            #region Care Provider Finance Invoice batch

            var allFinanceInvoiceBatches = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupID(fibsId);
            Assert.AreEqual(1, allFinanceInvoiceBatches.Count);
            var FinanceInvoicebatchId = allFinanceInvoiceBatches.First();

            #endregion

            #region Schdeduled job for "Process CP Finance Invoice Batches"

            ExecuteScheduleJob("Process CP Finance Invoice Batches");

            #endregion

            #region Care Provider Finance Invoices

            var allFinanceInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(FinanceInvoicebatchId);
            Assert.AreEqual(1, allFinanceInvoices.Count);
            var financeInvoiceId = allFinanceInvoices.First();

            #endregion


            #region Step 1

            /*Workplace → Finance*/
            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceTransactionsSection();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad()
                .InsertSearchQuery(transactionNumber.ToString())
                .ClickSearchButton()
                .WaitForCPFinanceTransactionsPageToLoad()
                .ValidateRecordIsPresent(financeTransactionId, true);

            /*Person Contract Service*/
            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_personnumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .ValidateRecordIsPresent(financeTransactionId);

            /*Establishment / Funder*/
            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(funderProviderName, funderProviderID)
                .WaitForProvidersPageToLoad()
                .OpenProviderRecord(funderProviderID);

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .TapFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("provider", false)
                .ValidateRecordIsPresent(financeTransactionId);


            /*Contract Scheme*/
            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Contract Schemes")
                .ClickReferenceDataMainHeader("Person Contracts")
                .ClickReferenceDataElement("Contract Schemes");

            contractSchemesPage
                .WaitForContractSchemesPageToLoad()
                .InsertTextOnQuickSearch(contractScheme1Name)
                .ClickQuickSearchButton()
                .WaitForContractSchemesPageToLoad()
                .OpenContractSchemeRecord(careProviderContractScheme1Id);

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("careprovidercontractscheme").ClickOnExpandIcon();

            contractSchemeRecordPage
                .WaitForContractSchemeRecordPageToLoad(true, false)
                .NavigateToFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careprovidercontractscheme", false)
                .ValidateRecordIsPresent(financeTransactionId);


            /*Contract Service*/
            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(providerName, providerId)
                .WaitForProvidersPageToLoad()
                .OpenProviderRecord(providerId);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();

            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careprovidercontractservice", false)
                .ValidateRecordIsPresent(financeTransactionId);



            /*Finance Invoice Batch*/
            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingExpandButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            cpFinanceInvoiceBatchSetupsPage
                .InsertSearchQuery(contractScheme1Name)
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(fibsId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickFinanceInvoiceBatchesTab();

            cpFinanceInvoiceBatchesPage
                .WaitForPageToLoad_FromFIBSRecord()
                .OpenRecord(FinanceInvoicebatchId);

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ClickFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad(FinanceInvoicebatchId.ToString(), true)
                .ValidateRecordIsPresent(financeTransactionId);


            /*Finance Invoice*/
            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ClickFinanceInvoicesTab();

            cpFinanceInvoicesPage
                .WaitForCPFinanceInvoicesPageToLoad(FinanceInvoicebatchId.ToString())
                .OpenRecord(financeInvoiceId);

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad(financeInvoiceId.ToString())
                .ClickFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad(financeInvoiceId.ToString(), true)
                .ValidateRecordIsPresent(financeTransactionId);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7368")]
        [Description("Step(s) 2 to 6 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases002()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-891 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-891", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 7);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 1), null, true);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 1), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed
            var careProviderPersonContractServiceTitle = (string)(dbHelper.careProviderPersonContractService.GetByID(careProviderPersonContractServiceId, "title")["title"]);

            #endregion

            #region Finance Transaction Triggers

            var financeTransactionTriggers = dbHelper.careProviderFinanceTransactionTrigger.GetByRecordId(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactionTriggers.Count);
            var financeTransactionTriggerID = financeTransactionTriggers[0];

            #endregion


            #region Step 2


            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_personnumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToFinanceTransactionTriggersPage();

            cpFinanceTransactionTriggersPage
                .WaitForPageToLoad("careproviderpersoncontractservice")
                .OpenRecord(financeTransactionTriggerID);

            cpFinanceTransactionTriggerRecordPage
                .WaitForPageToLoad()
                .ValidateReasonSelectedText("Person Contract Service - Authorised");

            #endregion

            #region Step 3

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            var fibsTriggers = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(1, fibsTriggers.Count);

            var financeTransactionId = fibsTriggers[0];
            var transactionNumber = (int)(dbHelper.careProviderFinanceTransaction.GetFinanceTransactionById(financeTransactionId, "careproviderfinancetransactionnumber")["careproviderfinancetransactionnumber"]);

            ExecuteScheduleJob("Process CP Finance Invoice Batches");

            var allFinanceInvoiceBatches = dbHelper.careProviderFinanceInvoiceBatch.GetFinanceInvoiceBatchBySetupIDAndStatusID(fibsId, 2);
            Assert.AreEqual(1, allFinanceInvoiceBatches.Count);
            var FinanceInvoicebatchId = allFinanceInvoiceBatches.First();


            #endregion

            #region Step 4

            //ignoring this step as it would become a repetition of step 1

            #endregion

            #region Step 5 & 6

            cpFinanceTransactionTriggerRecordPage
                .ClickBackButton();

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactionId);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidatePageHeader(contractScheme1Name + " / " + careProviderServiceName + " / 01/01/2024");

            //Finance Transaction Details
            cpFinanceTransactionRecordPage
                .ValidatePersonLinkText(personFullName)
                .ValidateStartDateText("01/01/2024")
                .ValidateStartTimeText("")
                .ValidateTransactionClassSelectedText("Standard")
                .ValidateTransactionNoText(transactionNumber.ToString())
                .ValidateNetAmount("10.00")
                .ValidateVatAmount("2.00")
                .ValidateGrossAmount("12.00")
                .ValidateApportioned_NoRadioButtonChecked()
                .ValidateForInformationOnly_NoRadioButtonChecked()
                .ValidateFinanceCodeText("")
                .ValidateEndDateText("07/01/2024")
                .ValidateEndTimeText("")
                .ValidateAccountCodeText("")
                .ValidateTransactionNoContradText("")
                .ValidateEndDateWeeklyChargeText("10.00")
                .ValidateVATCodeText("Standard Rated")
                .ValidateVatReferenceText("")
                .ValidateConfirmed_NoRadioButtonIsChecked();

            //Person Contract Details
            cpFinanceTransactionRecordPage
                .ValidatePersonContractServiceText(careProviderPersonContractServiceTitle)
                .ValidateBookingDiaryLinkText("")
                .ValidateEstablishmentLinkText(providerName)
                .ValidateContractSchemeLinkText(contractScheme1Name)
                .ValidateRateUnitLinkText("Default Care Provider Rate Unit")
                .ValidateTotalUnitsText("1.00")
                .ValidateContractServiceLinkText(careProviderContractServiceTitle)
                .ValidateExpenseTypeLinkText("")
                .ValidatePayerLinkText(funderProviderName)
                .ValidateBookingTypeLinkText("")
                .ValidateFunderLinkText(funderProviderName)
                .ValidateServiceLinkText(careProviderServiceName)
                .ValidateServiceDetailLinkText("CPSD ACC-891")
                .ValidateChargeableUnitsText("1.00")
                .ValidateContractTypeSelectedText("Spot")
                .ValidateAdHocExpenseLinkText("");

            var financeInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(FinanceInvoicebatchId);
            Assert.AreEqual(1, financeInvoices.Count);
            var financeInvoiceId = financeInvoices.First();
            var financeInvoiceNumber = (dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceById(financeInvoiceId, "invoicenumber")["invoicenumber"]).ToString();

            //Finance Invoice
            cpFinanceTransactionRecordPage
                .ValidateFinanceInvoiceLinkText("Funder Provider " + currentTimeString + " / CPCS_A_" + currentTimeString + " / " + financeInvoiceNumber + " / Charges Up to = 07/01/2024")
                .ValidateFinanceInvoiceStatusSelectedText("New")
                .ValidateFinanceInvoiceBatchSetupLinkText("CPCS_A_" + currentTimeString + " \\ Monthly \\ 01/01/2024")
                .ValidateInvoiceNoText(financeInvoiceNumber)
                .ValidateFinanceInvoiceBatchLinkText("CPCS_A_" + currentTimeString + " \\ Monthly \\ 02/01/2024 00:00:00");

            //Invoice Details
            cpFinanceTransactionRecordPage
                .ValidateTransactionText("Charge for Carlos " + currentTimeString + " for the period 01/01/2024 to 07/01/2024");

            //Extract
            cpFinanceTransactionRecordPage
                .ValidateFinanceExtractBatchLinkText("")
                .ValidateExtractNoText("")
                .ValidateFinanceExtractBatchSetupLinkText("");

            //General
            cpFinanceTransactionRecordPage
                .ValidateResponsibleTeamLinkText(_teamName);

            //Notes
            cpFinanceTransactionRecordPage
                .ValidateNoteText_Text("");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-7203

        [TestProperty("JiraIssueID", "ACC-7369")]
        [Description("Step(s) 7 to 9 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases003()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-826 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-826", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 28);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            dbHelper.person.UpdateAnonymousForBilling(_personID, true);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 8), null, true);

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonId = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, "Default Person Contract Service End Reason", new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 8), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateEndDateTime(careProviderPersonContractServiceId, new DateTime(2024, 1, 27), careProviderPersonContractServiceEndReasonId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed
            var careProviderPersonContractServiceTitle = (string)(dbHelper.careProviderPersonContractService.GetByID(careProviderPersonContractServiceId, "title")["title"]);

            #endregion

            #region Finance Transaction Triggers

            var financeTransactionTriggers = dbHelper.careProviderFinanceTransactionTrigger.GetByRecordId(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactionTriggers.Count);
            var financeTransactionTriggerID = financeTransactionTriggers[0];

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            var financeTransactions = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(3, financeTransactions.Count);

            var financeTransactionId = financeTransactions[2];
            var transactionNumber = (int)(dbHelper.careProviderFinanceTransaction.GetFinanceTransactionById(financeTransactionId, "careproviderfinancetransactionnumber")["careproviderfinancetransactionnumber"]);

            #endregion


            #region Step 7 & 8

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_personnumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactionId);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("08/01/2024")
                .ValidateEndDateText("14/01/2024")
                .ValidateTransactionText("Charge for " + _personnumber + " for the period 08/01/2024 to 14/01/2024")
                .InsertTextOnTransactionText("Charge for " + _personnumber + " for the period 08/01/2024 to 14/01/2024 - Updated")
                .ClickSaveAndCloseButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactionId);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateTransactionText("Charge for " + _personnumber + " for the period 08/01/2024 to 14/01/2024 - Updated")
                .ClickBackButton();

            #endregion

            #region Step 9

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[1]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("15/01/2024")
                .ValidateEndDateText("21/01/2024")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[0]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("22/01/2024")
                .ValidateEndDateText("27/01/2024");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7371")]
        [Description("Step(s) 9 to 10 from the original test. " +
            "Step 9 - PCS End Date (if on CS, associated to the PCS, Adjusted days = ”Charge End Date Less 1 Day”, then use PCS End Date less 1 day). " +
            "Step 10 - Rate = (ii) If no PCSRP record exists, then look at CSRP, fetch its rate, that is open between the FT Start and End Date. ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases005()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-826 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-826", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var contractserviceadjusteddaysid = 2;
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, contractserviceadjusteddaysid, false, false);
            var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 28);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            dbHelper.person.UpdateAnonymousForBilling(_personID, true);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 8), null, true);

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonId = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, "Default Person Contract Service End Reason", new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 8), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateEndDateTime(careProviderPersonContractServiceId, new DateTime(2024, 1, 27), careProviderPersonContractServiceEndReasonId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed
            var careProviderPersonContractServiceTitle = (string)(dbHelper.careProviderPersonContractService.GetByID(careProviderPersonContractServiceId, "title")["title"]);

            #endregion

            #region Finance Transaction Triggers

            var financeTransactionTriggers = dbHelper.careProviderFinanceTransactionTrigger.GetByRecordId(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactionTriggers.Count);
            var financeTransactionTriggerID = financeTransactionTriggers[0];

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            var financeTransactions = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(3, financeTransactions.Count);

            #endregion


            #region Step 9 & 10

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_personnumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[2]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("08/01/2024")
                .ValidateEndDateText("14/01/2024")
                .ValidateNetAmount("10.00")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[1]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("15/01/2024")
                .ValidateEndDateText("21/01/2024")
                .ValidateNetAmount("10.00")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[0]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("22/01/2024")
                .ValidateEndDateText("26/01/2024")
                .ValidateNetAmount("7.14");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7372")]
        [Description("Step(s) 10 from the original test. " +
            "Step 10 - (i) Where PCS has a PCSRP, fetch its rate, that is open between the FT Start and End Date.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases006()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-826 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-826", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var contractserviceadjusteddaysid = 1; //Charge End Date
            var isnegotiatedratescanapply = true;
            var ispermitrateoverride = true;
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, contractserviceadjusteddaysid, false, isnegotiatedratescanapply, ispermitrateoverride);
            var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 28);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            dbHelper.person.UpdateAnonymousForBilling(_personID, true);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 8), null, true);

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonId = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, "Default Person Contract Service End Reason", new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 8), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateEndDateTime(careProviderPersonContractServiceId, new DateTime(2024, 1, 27), careProviderPersonContractServiceEndReasonId);

            #endregion

            #region Person Contract Service Rate Period

            dbHelper.careProviderPersonContractServiceRatePeriod.CreateCareProviderPersonContractServiceRatePeriod(_teamId, careProviderPersonContractServiceId, _careProviderRateUnitId, new DateTime(2024, 1, 1), 15, "");

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.UpdateOverrideRateAndRateVerified(careProviderPersonContractServiceId, true, true);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Finance Transaction Triggers

            var financeTransactionTriggers = dbHelper.careProviderFinanceTransactionTrigger.GetByRecordId(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactionTriggers.Count);
            var financeTransactionTriggerID = financeTransactionTriggers[0];

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            var financeTransactions = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(3, financeTransactions.Count);

            #endregion


            #region Step 10

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_personnumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[2]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateNetAmount("15.00")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[1]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateNetAmount("15.00")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[0]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateNetAmount("12.86");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7373")]
        [Description("Step(s) 11 to 12 from the original test. ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases007()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-826 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-826", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var contractserviceadjusteddaysid = 1; //Charge End Date
            var isnegotiatedratescanapply = true;
            var ispermitrateoverride = true;
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, contractserviceadjusteddaysid, false, isnegotiatedratescanapply, ispermitrateoverride);
            var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 28);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            dbHelper.person.UpdateAnonymousForBilling(_personID, true);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 8), null, true);

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonId = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, "Default Person Contract Service End Reason", new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 8), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateEndDateTime(careProviderPersonContractServiceId, new DateTime(2024, 1, 27), careProviderPersonContractServiceEndReasonId);

            #endregion

            #region Person Contract Service Rate Period

            dbHelper.careProviderPersonContractServiceRatePeriod.CreateCareProviderPersonContractServiceRatePeriod(_teamId, careProviderPersonContractServiceId, _careProviderRateUnitId, new DateTime(2024, 1, 1), 11, "", new DateTime(2024, 1, 10));
            dbHelper.careProviderPersonContractServiceRatePeriod.CreateCareProviderPersonContractServiceRatePeriod(_teamId, careProviderPersonContractServiceId, _careProviderRateUnitId, new DateTime(2024, 1, 11), 13, "", new DateTime(2024, 1, 18));
            dbHelper.careProviderPersonContractServiceRatePeriod.CreateCareProviderPersonContractServiceRatePeriod(_teamId, careProviderPersonContractServiceId, _careProviderRateUnitId, new DateTime(2024, 1, 19), 15, "");

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.UpdateOverrideRateAndRateVerified(careProviderPersonContractServiceId, true, true);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Finance Transaction Triggers

            var financeTransactionTriggers = dbHelper.careProviderFinanceTransactionTrigger.GetByRecordId(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactionTriggers.Count);
            var financeTransactionTriggerID = financeTransactionTriggers[0];

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            var financeTransactions = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(3, financeTransactions.Count);

            #endregion


            #region Step 11 & 12

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_personnumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[2]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateNetAmount("12.14")
                .ValidateVatAmount("2.43")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[1]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateNetAmount("13.86")
                .ValidateVatAmount("2.77")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[0]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateNetAmount("12.86")
                .ValidateVatAmount("2.57");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7374")]
        [Description("Step(s) 13 from the original test. ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases008()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-826 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = commonMethodsDB.CreateCareProviderVatCode(_teamId, "ACC-826", new DateTime(2020, 1, 1), 90710);

            #endregion

            commonMethodsDB.CreateCareProviderVATCodeSetup(_teamId, _careProviderVATCodeId, 10, "1A", new DateTime(2000, 1, 1), new DateTime(2024, 1, 11));
            commonMethodsDB.CreateCareProviderVATCodeSetup(_teamId, _careProviderVATCodeId, 15, "2A", new DateTime(2024, 1, 12), new DateTime(2024, 1, 18));
            commonMethodsDB.CreateCareProviderVATCodeSetup(_teamId, _careProviderVATCodeId, 20, "3A", new DateTime(2024, 1, 19), new DateTime(2024, 1, 25));
            commonMethodsDB.CreateCareProviderVATCodeSetup(_teamId, _careProviderVATCodeId, 25, "4A", new DateTime(2024, 1, 26), null);


            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-826", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var contractserviceadjusteddaysid = 1; //Charge End Date
            var isnegotiatedratescanapply = true;
            var ispermitrateoverride = true;
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, contractserviceadjusteddaysid, false, isnegotiatedratescanapply, ispermitrateoverride);
            var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 28);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            dbHelper.person.UpdateAnonymousForBilling(_personID, true);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 8), null, true);

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonId = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, "Default Person Contract Service End Reason", new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 8), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateEndDateTime(careProviderPersonContractServiceId, new DateTime(2024, 1, 27), careProviderPersonContractServiceEndReasonId);

            #endregion

            #region Person Contract Service Rate Period

            dbHelper.careProviderPersonContractServiceRatePeriod.CreateCareProviderPersonContractServiceRatePeriod(_teamId, careProviderPersonContractServiceId, _careProviderRateUnitId, new DateTime(2024, 1, 1), 7, "", new DateTime(2024, 1, 14));
            dbHelper.careProviderPersonContractServiceRatePeriod.CreateCareProviderPersonContractServiceRatePeriod(_teamId, careProviderPersonContractServiceId, _careProviderRateUnitId, new DateTime(2024, 1, 15), 13, "", new DateTime(2024, 1, 21));
            dbHelper.careProviderPersonContractServiceRatePeriod.CreateCareProviderPersonContractServiceRatePeriod(_teamId, careProviderPersonContractServiceId, _careProviderRateUnitId, new DateTime(2024, 1, 22), 15, "");

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.UpdateOverrideRateAndRateVerified(careProviderPersonContractServiceId, true, true);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Finance Transaction Triggers

            var financeTransactionTriggers = dbHelper.careProviderFinanceTransactionTrigger.GetByRecordId(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactionTriggers.Count);
            var financeTransactionTriggerID = financeTransactionTriggers[0];

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            var financeTransactions = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(3, financeTransactions.Count);

            #endregion


            #region Step 13

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_personnumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[2]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateNetAmount("7.00")
                .ValidateVatAmount("0.85")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[1]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateNetAmount("13.00")
                .ValidateVatAmount("2.23")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[0]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateNetAmount("12.86")
                .ValidateVatAmount("2.79");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7375")]
        [Description("Step(s) 14 from the original test. ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases009()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-826 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = commonMethodsDB.CreateCareProviderVatCode(_teamId, "ACC-826 - V2", new DateTime(2020, 1, 1), 90712);

            #endregion

            commonMethodsDB.CreateCareProviderVATCodeSetup(_teamId, _careProviderVATCodeId, 10, "1A", new DateTime(2000, 1, 1), new DateTime(2024, 1, 11));
            commonMethodsDB.CreateCareProviderVATCodeSetup(_teamId, _careProviderVATCodeId, 20, "2A", new DateTime(2024, 1, 12), new DateTime(2024, 1, 18));
            commonMethodsDB.CreateCareProviderVATCodeSetup(_teamId, _careProviderVATCodeId, 25, "3A", new DateTime(2024, 1, 19), null);


            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-826", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var contractserviceadjusteddaysid = 1; //Charge End Date
            var isnegotiatedratescanapply = true;
            var ispermitrateoverride = true;
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, contractserviceadjusteddaysid, false, isnegotiatedratescanapply, ispermitrateoverride);
            var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 28);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            dbHelper.person.UpdateAnonymousForBilling(_personID, true);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 8), null, true);

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonId = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, "Default Person Contract Service End Reason", new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 8), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateEndDateTime(careProviderPersonContractServiceId, new DateTime(2024, 1, 27), careProviderPersonContractServiceEndReasonId);

            #endregion

            #region Person Contract Service Rate Period

            dbHelper.careProviderPersonContractServiceRatePeriod.CreateCareProviderPersonContractServiceRatePeriod(_teamId, careProviderPersonContractServiceId, _careProviderRateUnitId, new DateTime(2024, 1, 1), 7, "", new DateTime(2024, 1, 10));
            dbHelper.careProviderPersonContractServiceRatePeriod.CreateCareProviderPersonContractServiceRatePeriod(_teamId, careProviderPersonContractServiceId, _careProviderRateUnitId, new DateTime(2024, 1, 11), 14, "", new DateTime(2024, 1, 14));
            dbHelper.careProviderPersonContractServiceRatePeriod.CreateCareProviderPersonContractServiceRatePeriod(_teamId, careProviderPersonContractServiceId, _careProviderRateUnitId, new DateTime(2024, 1, 15), 20, "");

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.UpdateOverrideRateAndRateVerified(careProviderPersonContractServiceId, true, true);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Finance Transaction Triggers

            var financeTransactionTriggers = dbHelper.careProviderFinanceTransactionTrigger.GetByRecordId(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactionTriggers.Count);
            var financeTransactionTriggerID = financeTransactionTriggers[0];

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            var financeTransactions = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(3, financeTransactions.Count);

            #endregion


            #region Step 14

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_personnumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[2]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateNetAmount("11.00")
                .ValidateVatAmount("1.70")
                .ClickBackButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7376")]
        [Description("Step(s) 15 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases010()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-826 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-826", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 28);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            dbHelper.person.UpdateAnonymousForBilling(_personID, true);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 1), null, true);

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonId = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, "Default Person Contract Service End Reason", new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 1), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateEndDateTime(careProviderPersonContractServiceId, new DateTime(2024, 1, 6), careProviderPersonContractServiceEndReasonId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed
            var careProviderPersonContractServiceTitle = (string)(dbHelper.careProviderPersonContractService.GetByID(careProviderPersonContractServiceId, "title")["title"]);

            #endregion

            #region Finance Transaction Triggers

            var financeTransactionTriggers = dbHelper.careProviderFinanceTransactionTrigger.GetByRecordId(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactionTriggers.Count);
            var financeTransactionTriggerID = financeTransactionTriggers[0];

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            var financeTransactions = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactions.Count);

            #endregion


            #region Step 15

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_personnumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .ValidateRecordIsPresent(financeTransactions[0]);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-7204

        [TestProperty("JiraIssueID", "ACC-7377")]
        [Description("Step(s) 16 to 17 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases011()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-826 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2022, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2022, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2022, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-826", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2022, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 5; //Every Calendar Month
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2022, 6, 30);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2022, 5, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            dbHelper.person.UpdateAnonymousForBilling(_personID, true);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2022, 1, 1), null, true);

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonId = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, "Default Person Contract Service End Reason", new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2022, 5, 23), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateEndDateTime(careProviderPersonContractServiceId, new DateTime(2022, 6, 12), careProviderPersonContractServiceEndReasonId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed
            var careProviderPersonContractServiceTitle = (string)(dbHelper.careProviderPersonContractService.GetByID(careProviderPersonContractServiceId, "title")["title"]);

            #endregion

            #region Finance Transaction Triggers

            var financeTransactionTriggers = dbHelper.careProviderFinanceTransactionTrigger.GetByRecordId(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactionTriggers.Count);
            var financeTransactionTriggerID = financeTransactionTriggers[0];

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            var financeTransactions = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(4, financeTransactions.Count);

            #endregion


            #region Step 16

            //already validated in test CPFinanceTransactions_ACC_7202_UITestCases003

            #endregion

            #region Step 17

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_personnumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[3]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("23/05/2022")
                .ValidateEndDateText("29/05/2022")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[2]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("30/05/2022")
                .ValidateEndDateText("31/05/2022")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[1]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("01/06/2022")
                .ValidateEndDateText("05/06/2022")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[0]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("06/06/2022")
                .ValidateEndDateText("12/06/2022")
                .ClickBackButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7378")]
        [Description("Step(s) 17 from the original test - FIBS Invoice Frequency = 'Every Week (by Month)' ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases012()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-826 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2022, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2022, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2022, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-826", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2022, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 21; //Every Week (by Month)
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2022, 6, 30);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2022, 5, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            dbHelper.person.UpdateAnonymousForBilling(_personID, true);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2022, 1, 1), null, true);

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonId = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, "Default Person Contract Service End Reason", new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2022, 5, 23), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateEndDateTime(careProviderPersonContractServiceId, new DateTime(2022, 6, 12), careProviderPersonContractServiceEndReasonId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed
            var careProviderPersonContractServiceTitle = (string)(dbHelper.careProviderPersonContractService.GetByID(careProviderPersonContractServiceId, "title")["title"]);

            #endregion

            #region Finance Transaction Triggers

            var financeTransactionTriggers = dbHelper.careProviderFinanceTransactionTrigger.GetByRecordId(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactionTriggers.Count);
            var financeTransactionTriggerID = financeTransactionTriggers[0];

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            var financeTransactions = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(4, financeTransactions.Count);

            #endregion

            #region Step 17

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_personnumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[3]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("23/05/2022")
                .ValidateEndDateText("29/05/2022")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[2]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("30/05/2022")
                .ValidateEndDateText("31/05/2022")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[1]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("01/06/2022")
                .ValidateEndDateText("05/06/2022")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[0]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("06/06/2022")
                .ValidateEndDateText("12/06/2022")
                .ClickBackButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7379")]
        [Description("Step(s) 17 from the original test - FIBS Invoice Frequency = 'Every 4 Weeks (by Month)' ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases013()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-826 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2022, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2022, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2022, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-826", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2022, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 23; //Every 4 Weeks (by Month)
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2022, 6, 30);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2022, 5, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            dbHelper.person.UpdateAnonymousForBilling(_personID, true);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2022, 1, 1), null, true);

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonId = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, "Default Person Contract Service End Reason", new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2022, 5, 23), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateEndDateTime(careProviderPersonContractServiceId, new DateTime(2022, 6, 12), careProviderPersonContractServiceEndReasonId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed
            var careProviderPersonContractServiceTitle = (string)(dbHelper.careProviderPersonContractService.GetByID(careProviderPersonContractServiceId, "title")["title"]);

            #endregion

            #region Finance Transaction Triggers

            var financeTransactionTriggers = dbHelper.careProviderFinanceTransactionTrigger.GetByRecordId(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactionTriggers.Count);
            var financeTransactionTriggerID = financeTransactionTriggers[0];

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            var financeTransactions = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(4, financeTransactions.Count);

            #endregion


            #region Step 17

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_personnumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[3]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("23/05/2022")
                .ValidateEndDateText("29/05/2022")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[2]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("30/05/2022")
                .ValidateEndDateText("31/05/2022")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[1]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("01/06/2022")
                .ValidateEndDateText("05/06/2022")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[0]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("06/06/2022")
                .ValidateEndDateText("12/06/2022")
                .ClickBackButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7380")]
        [Description("Step(s) 17 from the original test - FIBS Invoice Frequency = 'Every Quarter (Date Specific)' ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases014()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-826 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2022, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2022, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2022, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-826", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2022, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 23; //Every 4 Weeks (by Month)
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2022, 4, 30);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2022, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            dbHelper.person.UpdateAnonymousForBilling(_personID, true);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2022, 1, 1), null, true);

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonId = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, "Default Person Contract Service End Reason", new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2022, 3, 21), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateEndDateTime(careProviderPersonContractServiceId, new DateTime(2022, 4, 10), careProviderPersonContractServiceEndReasonId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed
            var careProviderPersonContractServiceTitle = (string)(dbHelper.careProviderPersonContractService.GetByID(careProviderPersonContractServiceId, "title")["title"]);

            #endregion

            #region Finance Transaction Triggers

            var financeTransactionTriggers = dbHelper.careProviderFinanceTransactionTrigger.GetByRecordId(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactionTriggers.Count);
            var financeTransactionTriggerID = financeTransactionTriggers[0];

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            var financeTransactions = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(4, financeTransactions.Count);

            #endregion


            #region Step 17

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_personnumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[3]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("21/03/2022")
                .ValidateEndDateText("27/03/2022")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[2]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("28/03/2022")
                .ValidateEndDateText("31/03/2022")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[1]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("01/04/2022")
                .ValidateEndDateText("03/04/2022")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[0]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("04/04/2022")
                .ValidateEndDateText("10/04/2022")
                .ClickBackButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7381")]
        [Description("Step(s) 18 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases015()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-826 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-826", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var contractserviceadjusteddaysid = 2; //Charge End Date Less 1 Day
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, contractserviceadjusteddaysid, false, false);
            var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 31);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            dbHelper.person.UpdateAnonymousForBilling(_personID, true);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 1), null, true);

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonId = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, "Default Person Contract Service End Reason", new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 1), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateEndDateTime(careProviderPersonContractServiceId, new DateTime(2024, 1, 21), careProviderPersonContractServiceEndReasonId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed
            var careProviderPersonContractServiceTitle = (string)(dbHelper.careProviderPersonContractService.GetByID(careProviderPersonContractServiceId, "title")["title"]);

            #endregion

            #region Finance Transaction Triggers

            var financeTransactionTriggers = dbHelper.careProviderFinanceTransactionTrigger.GetByRecordId(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactionTriggers.Count);
            var financeTransactionTriggerID = financeTransactionTriggers[0];

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            var financeTransactions = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(3, financeTransactions.Count);

            #endregion


            #region Step 18

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_personnumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[2]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("01/01/2024")
                .ValidateEndDateText("07/01/2024")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[1]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("08/01/2024")
                .ValidateEndDateText("14/01/2024")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[0]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("15/01/2024")
                .ValidateEndDateText("20/01/2024")
                .ClickBackButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7382")]
        [Description("Step(s) 19 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases016()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2021, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2021, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-826 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2021, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2021, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Standard";
            Guid careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, careProviderBatchGroupingName, new DateTime(2020, 1, 1), 8888888);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2021, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Per Week Pro Rata", new DateTime(2020, 1, 1), 12);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-826", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var contractserviceadjusteddaysid = 2; //Charge End Date Less 1 Day
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, contractserviceadjusteddaysid, false, false);
            var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2021, 1, 1), _careProviderRateUnitId, 420, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 3; //Every 4 Weeks
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2022, 12, 31);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2021, 11, 17), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            dbHelper.person.UpdateAnonymousForBilling(_personID, true);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2021, 11, 17), null, true);

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonId = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, "Default Person Contract Service End Reason", new DateTime(2021, 11, 17), 999999);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2021, 11, 17), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Finance Transaction Triggers

            var financeTransactionTriggers = dbHelper.careProviderFinanceTransactionTrigger.GetByRecordId(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactionTriggers.Count);
            var financeTransactionTriggerID = financeTransactionTriggers[0];

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            var financeTransactions = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(59, financeTransactions.Count);

            #endregion


            #region Step 19

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_personnumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[58]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("17/11/2021")
                .ValidateEndDateText("21/11/2021")
                .ValidateNetAmount("300.00")
                .ValidateVatAmount("60.00")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[57]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("22/11/2021")
                .ValidateEndDateText("28/11/2021")
                .ValidateNetAmount("420.00")
                .ValidateVatAmount("84.00")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[1]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("19/12/2022")
                .ValidateEndDateText("25/12/2022")
                .ValidateNetAmount("420.00")
                .ValidateVatAmount("84.00")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[0]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("26/12/2022")
                .ValidateEndDateText("31/12/2022")
                .ValidateNetAmount("360.00")
                .ValidateVatAmount("72.00");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7383")]
        [Description("Step(s) 20 to 21 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases017()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-826 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Standard";
            Guid careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, careProviderBatchGroupingName, new DateTime(2020, 1, 1), 8888888);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Per Week Pro Rata", new DateTime(2020, 1, 1), 12);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-826", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var contractserviceadjusteddaysid = 2; //Charge End Date Less 1 Day
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, contractserviceadjusteddaysid, false, false);
            var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 420, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 2, 29);
            var separateinvoices = false;
            var debtorreferencenumberrequired = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, debtorreferencenumberrequired,
                _teamId);

            #endregion

            #region Care Provider Extract Type

            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type: Generic Extract File

            #endregion

            #region Mail Merge

            var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            #endregion

            #region Finance Extract Batch Setup

            var careProviderFinanceExtractBatchSetupId = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0), null, careProviderExtractName1Id, 1, _cpGenericExtractTypeId, _invoiceGenerateTemplate_MailMergeId,
                _defaultLoginUserID, "systemuser", _defaultLoginUserName, false, false, false, false, false, false, true);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            dbHelper.person.UpdateAnonymousForBilling(_personID, true);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 1), null, true);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 1), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Finance Transaction Triggers

            var financeTransactionTriggers = dbHelper.careProviderFinanceTransactionTrigger.GetByRecordId(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactionTriggers.Count);
            var financeTransactionTriggerID = financeTransactionTriggers[0];

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            var financeTransactions = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(9, financeTransactions.Count);

            #endregion

            #region Care Provider Finance Invoice batch

            var allFinanceInvoiceBatches = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupID(fibsId);
            Assert.AreEqual(1, allFinanceInvoiceBatches.Count);
            var FinanceInvoicebatchId = allFinanceInvoiceBatches.First();

            #endregion

            #region Schdeduled job for "Process CP Finance Invoice Batches"

            ExecuteScheduleJob("Process CP Finance Invoice Batches");

            #endregion

            #region Care Provider Finance Invoice

            var allFinanceInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(FinanceInvoicebatchId);
            Assert.AreEqual(1, allFinanceInvoices.Count);
            var FinanceInvoiceId = allFinanceInvoices.First();

            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(FinanceInvoiceId, 2); //Completed

            #endregion

            #region Schdeduled job for "Process CP Finance Extract Batches"

            ExecuteScheduleJob("Process CP Finance Extract Batches");

            #endregion

            #region Care Provider Finance Extract Batch

            var batchstatusid = 2; //completed
            var allFinanceExtractBatches = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(careProviderFinanceExtractBatchSetupId, careProviderExtractName1Id, batchstatusid);
            Assert.AreEqual(1, allFinanceExtractBatches.Count);
            var financeExtractBatchId = allFinanceExtractBatches.First();
            var batchid = (int)(dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(financeExtractBatchId, "batchid")["batchid"]);

            #endregion

            #region Step 20

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_personnumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[8]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateFinanceExtractBatchLinkText("CPEN " + currentTimeString + " \\ 01/01/2024 00:00:00")
                .ValidateFinanceExtractBatchSetupLinkText("CPEN " + currentTimeString + " \\ 01/01/2024")
                .ValidateExtractNoText(batchid.ToString())
                .ClickBackButton();

            #endregion

            #region Step 21

            //this step has been indirectly tested by other steps in this group. Finance invoice batches, finance invoices, finance transactions, finance transaction triggers, finance extract batches are all example of records being created automatically by the schedule jobs

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7384")]
        [Description("Step(s) 22 to 24 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases018()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-826 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Standard";
            Guid careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, careProviderBatchGroupingName, new DateTime(2020, 1, 1), 8888888);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Per Week Pro Rata", new DateTime(2020, 1, 1), 12);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-826", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var contractserviceadjusteddaysid = 2; //Charge End Date Less 1 Day
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, contractserviceadjusteddaysid, false, false);
            var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 420, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 7);
            var separateinvoices = false;
            var debtorreferencenumberrequired = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, debtorreferencenumberrequired,
                _teamId);

            #endregion

            #region Care Provider Extract Type

            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type: Generic Extract File

            #endregion

            #region Mail Merge

            var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            #endregion

            #region Finance Extract Batch Setup

            var careProviderFinanceExtractBatchSetupId = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0), null, careProviderExtractName1Id, 1, _cpGenericExtractTypeId, _invoiceGenerateTemplate_MailMergeId,
                _defaultLoginUserID, "systemuser", _defaultLoginUserName, false, false, false, false, false, false, true);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            dbHelper.person.UpdateAnonymousForBilling(_personID, true);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 1), null, true);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 1), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Finance Transaction Triggers

            var financeTransactionTriggers = dbHelper.careProviderFinanceTransactionTrigger.GetByRecordId(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactionTriggers.Count);
            var financeTransactionTriggerID = financeTransactionTriggers[0];

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            var financeTransactions = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactions.Count);

            #endregion

            #region Step 22

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_personnumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[0]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .InsertTextOnTransactionText("Updating transaction text ...")
                .InsertTextOnNoteText("Updating notes ...")
                .ClickSaveAndCloseButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[0]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateTransactionText("Updating transaction text ...")
                .ValidateNoteText_Text("Updating notes ...");

            #endregion

            #region Step 23

            //indirectly validated as part of all "wait for page to load" methods

            #endregion

            #region Step 24

            cpFinanceTransactionRecordPage
                .ValidatePersonLinkText(personFullName);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7385")]
        [Description("Step(s) 24 from the original test - Contract Type = Block")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases019()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-826 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Standard";
            Guid careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, careProviderBatchGroupingName, new DateTime(2020, 1, 1), 8888888);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Per Week Pro Rata", new DateTime(2020, 1, 1), 12);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-826", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var contractserviceadjusteddaysid = 1; //Charge End Date
            var contractservicetypeid = 2; //Block
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, contractservicetypeid, contractserviceadjusteddaysid, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 420, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 7);
            var separateinvoices = false;
            var debtorreferencenumberrequired = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, debtorreferencenumberrequired,
                _teamId);

            #endregion

            #region Care Provider Extract Type

            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type: Generic Extract File

            #endregion

            #region Mail Merge

            var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            #endregion

            #region Finance Extract Batch Setup

            var careProviderFinanceExtractBatchSetupId = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0), null, careProviderExtractName1Id, 1, _cpGenericExtractTypeId, _invoiceGenerateTemplate_MailMergeId,
                _defaultLoginUserID, "systemuser", _defaultLoginUserName, false, false, false, false, false, false, true);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            dbHelper.person.UpdateAnonymousForBilling(_personID, true);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 1), null, true);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 1), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Care Provider Contract Service

            dbHelper.careProviderContractService.UpdateDateToCommenceBlockCharging(careProviderContractServiceId, new DateTime(2024, 1, 1));

            #endregion

            #region Finance Transaction Triggers

            var financeTransactionTriggers = dbHelper.careProviderFinanceTransactionTrigger.GetByRecordId(careProviderContractServiceId);
            Assert.AreEqual(2, financeTransactionTriggers.Count);
            var financeTransactionTriggerID = financeTransactionTriggers[0];

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            var financeTransactions = dbHelper.careProviderFinanceTransaction.GetByCareProviderContractService(careProviderContractServiceId);
            Assert.AreEqual(1, financeTransactions.Count);
            var financeTransactionNumber = (dbHelper.careProviderFinanceTransaction.GetFinanceTransactionById(financeTransactions[0], "careproviderfinancetransactionnumber")["careproviderfinancetransactionnumber"]).ToString();

            #endregion

            #region Step 24

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceTransactionsSection();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad()
                .InsertSearchQuery(financeTransactionNumber)
                .ClickSearchButton()
                .WaitForCPFinanceTransactionsPageToLoad()
                .OpenRecord(financeTransactions[0]);


            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText("");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7386")]
        [Description("Step(s) 25 to 26 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases020()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-826 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Standard";
            Guid careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, careProviderBatchGroupingName, new DateTime(2020, 1, 1), 8888888);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Per Week Pro Rata", new DateTime(2020, 1, 1), 12);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-826", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var contractserviceadjusteddaysid = 2; //Charge End Date Less 1 Day
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, contractserviceadjusteddaysid, false, false);
            var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 420, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 2, 29);
            var separateinvoices = false;
            var debtorreferencenumberrequired = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, debtorreferencenumberrequired,
                _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            dbHelper.person.UpdateAnonymousForBilling(_personID, true);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 1), null, true);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 1), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Finance Transaction Triggers

            var financeTransactionTriggers = dbHelper.careProviderFinanceTransactionTrigger.GetByRecordId(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactionTriggers.Count);
            var financeTransactionTriggerID = financeTransactionTriggers[0];

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            var financeTransactions = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(9, financeTransactions.Count);

            var financeTransaction1Number = (dbHelper.careProviderFinanceTransaction.GetFinanceTransactionById(financeTransactions[0], "careproviderfinancetransactionnumber")["careproviderfinancetransactionnumber"]).ToString();

            #endregion

            #region Care Provider Finance Invoice batch

            var allFinanceInvoiceBatches = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupID(fibsId);
            Assert.AreEqual(1, allFinanceInvoiceBatches.Count);
            var FinanceInvoicebatchId = allFinanceInvoiceBatches.First();

            #endregion

            #region Schdeduled job for "Process CP Finance Invoice Batches"

            ExecuteScheduleJob("Process CP Finance Invoice Batches");

            #endregion

            #region Care Provider Finance Invoice

            var allFinanceInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(FinanceInvoicebatchId);
            Assert.AreEqual(1, allFinanceInvoiceBatches.Count);
            var financeInvoiceId = allFinanceInvoices.First();

            var financeInvoice1Number = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceById(financeInvoiceId, "invoicenumber")["invoicenumber"].ToString();

            #endregion

            #region Step 25

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_personnumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .RecordsPageValidateHeaderLinkText(2, "Payer")
                .RecordsPageValidateHeaderLinkText(3, "Funder")
                .RecordsPageValidateHeaderLinkText(4, "Establishment")
                .RecordsPageValidateHeaderLinkText(5, "Person")
                .RecordsPageValidateHeaderLinkText(6, "Contract Scheme")
                .RecordsPageValidateHeaderLinkText(7, "Service")
                .RecordsPageValidateHeaderLinkText(8, "Booking Type")
                .RecordsPageValidateHeaderLinkText(9, "Transaction No")
                .RecordsPageValidateHeaderLinkText(10, "Start Date")
                .RecordsPageValidateHeaderLinkText(11, "Start Time")
                .RecordsPageValidateHeaderLinkText(12, "End Date")
                .RecordsPageValidateHeaderLinkText(13, "End Time")
                .RecordsPageValidateHeaderLinkText(14, "Net Amount")
                .RecordsPageValidateHeaderLinkText(15, "VAT Amount")
                .RecordsPageValidateHeaderLinkText(16, "Gross Amount")
                .RecordsPageValidateHeaderLinkText(17, "Finance Code")
                .RecordsPageValidateHeaderLinkText(18, "Rate Unit")
                .RecordsPageValidateHeaderLinkText(19, "Total Units")
                .RecordsPageValidateHeaderLinkText(20, "Contract Type")
                .RecordsPageValidateHeaderLinkText(21, "Invoice Number")
                .RecordsPageValidateHeaderLinkText(22, "Finance Invoice Status")
                .RecordsPageValidateHeaderLinkText(23, "Extract No")
                .RecordsPageValidateHeaderLinkText(24, "Apportioned?")
                .RecordsPageValidateHeaderLinkText(25, "Full Net Amount")
                .RecordsPageValidateHeaderLinkText(26, "For Information Only?")
                .RecordsPageValidateHeaderLinkText(27, "Has Contra?")
                .RecordsPageValidateHeaderLinkText(28, "Modified On")
                .RecordsPageValidateHeaderLinkText(29, "Created On")

                .RecordsPageValidateHeaderSortDescIconVisible(10) //Start Date desc
                .RecordsPageValidateHeaderSortAscIconVisible(11); //Start Time desc

            #endregion

            #region Step 26

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceTransactionsSection();

            //Funder
            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad()
                .InsertSearchQuery(funderProviderName)
                .ClickSearchButton()
                .WaitForCPFinanceTransactionsPageToLoad()
                .ValidateRecordIsPresent(financeTransactions[0])
                .ValidateRecordIsPresent(financeTransactions[1])
                .ValidateRecordIsPresent(financeTransactions[2])
                .ValidateRecordIsPresent(financeTransactions[3])
                .ValidateRecordIsPresent(financeTransactions[4])
                .ValidateRecordIsPresent(financeTransactions[5])
                .ValidateRecordIsPresent(financeTransactions[6])
                .ValidateRecordIsPresent(financeTransactions[7])
                .ValidateRecordIsPresent(financeTransactions[8]);

            //Establishment
            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad()
                .InsertSearchQuery(providerName)
                .ClickSearchButton()
                .WaitForCPFinanceTransactionsPageToLoad()
                .ValidateRecordIsPresent(financeTransactions[0])
                .ValidateRecordIsPresent(financeTransactions[1])
                .ValidateRecordIsPresent(financeTransactions[2])
                .ValidateRecordIsPresent(financeTransactions[3])
                .ValidateRecordIsPresent(financeTransactions[4])
                .ValidateRecordIsPresent(financeTransactions[5])
                .ValidateRecordIsPresent(financeTransactions[6])
                .ValidateRecordIsPresent(financeTransactions[7])
                .ValidateRecordIsPresent(financeTransactions[8]);

            //Person
            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad()
                .InsertSearchQuery(personFullName)
                .ClickSearchButton()
                .WaitForCPFinanceTransactionsPageToLoad()
                .ValidateRecordIsPresent(financeTransactions[0])
                .ValidateRecordIsPresent(financeTransactions[1])
                .ValidateRecordIsPresent(financeTransactions[2])
                .ValidateRecordIsPresent(financeTransactions[3])
                .ValidateRecordIsPresent(financeTransactions[4])
                .ValidateRecordIsPresent(financeTransactions[5])
                .ValidateRecordIsPresent(financeTransactions[6])
                .ValidateRecordIsPresent(financeTransactions[7])
                .ValidateRecordIsPresent(financeTransactions[8]);

            //Contract Scheme
            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad()
                .InsertSearchQuery(contractScheme1Name)
                .ClickSearchButton()
                .WaitForCPFinanceTransactionsPageToLoad()
                .ValidateRecordIsPresent(financeTransactions[0])
                .ValidateRecordIsPresent(financeTransactions[1])
                .ValidateRecordIsPresent(financeTransactions[2])
                .ValidateRecordIsPresent(financeTransactions[3])
                .ValidateRecordIsPresent(financeTransactions[4])
                .ValidateRecordIsPresent(financeTransactions[5])
                .ValidateRecordIsPresent(financeTransactions[6])
                .ValidateRecordIsPresent(financeTransactions[7])
                .ValidateRecordIsPresent(financeTransactions[8]);

            //Service
            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad()
                .InsertSearchQuery(careProviderServiceName)
                .ClickSearchButton()
                .WaitForCPFinanceTransactionsPageToLoad()
                .ValidateRecordIsPresent(financeTransactions[0])
                .ValidateRecordIsPresent(financeTransactions[1])
                .ValidateRecordIsPresent(financeTransactions[2])
                .ValidateRecordIsPresent(financeTransactions[3])
                .ValidateRecordIsPresent(financeTransactions[4])
                .ValidateRecordIsPresent(financeTransactions[5])
                .ValidateRecordIsPresent(financeTransactions[6])
                .ValidateRecordIsPresent(financeTransactions[7])
                .ValidateRecordIsPresent(financeTransactions[8]);

            //Transactions No
            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad()
                .InsertSearchQuery(financeTransaction1Number)
                .ClickSearchButton()
                .WaitForCPFinanceTransactionsPageToLoad()
                .ValidateRecordIsPresent(financeTransactions[0])
                .ValidateRecordIsPresent(financeTransactions[1], false)
                .ValidateRecordIsPresent(financeTransactions[2], false)
                .ValidateRecordIsPresent(financeTransactions[3], false)
                .ValidateRecordIsPresent(financeTransactions[4], false)
                .ValidateRecordIsPresent(financeTransactions[5], false)
                .ValidateRecordIsPresent(financeTransactions[6], false)
                .ValidateRecordIsPresent(financeTransactions[7], false)
                .ValidateRecordIsPresent(financeTransactions[8], false);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7387")]
        [Description("Step(s) 27 to 28 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases021()
        {

            #region Step 27

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Finance Transactions")
                .SelectFilterInsideOptGroup("1", "Account Code")
                .SelectFilterInsideOptGroup("1", "Person")
                .SelectFilterInsideOptGroup("1", "Start Date")
                .SelectFilterInsideOptGroup("1", "Start Time")
                .SelectFilterInsideOptGroup("1", "End Date")
                .SelectFilterInsideOptGroup("1", "End Time")
                .SelectFilterInsideOptGroup("1", "Transaction No")
                .SelectFilterInsideOptGroup("1", "Transaction Class")
                .SelectFilterInsideOptGroup("1", "Net Amount")
                .SelectFilterInsideOptGroup("1", "VAT Amount")
                .SelectFilterInsideOptGroup("1", "Gross Amount")
                .SelectFilterInsideOptGroup("1", "VAT Code")
                .SelectFilterInsideOptGroup("1", "VAT Reference")
                .SelectFilterInsideOptGroup("1", "Finance Code")
                .SelectFilterInsideOptGroup("1", "Person Contract Service")
                .SelectFilterInsideOptGroup("1", "Establishment")
                .SelectFilterInsideOptGroup("1", "Funder")
                .SelectFilterInsideOptGroup("1", "Contract Scheme")
                .SelectFilterInsideOptGroup("1", "Service")
                .SelectFilterInsideOptGroup("1", "Rate Unit")
                .SelectFilterInsideOptGroup("1", "Total Units")
                .SelectFilterInsideOptGroup("1", "Contract Service")
                .SelectFilterInsideOptGroup("1", "Contract Type")
                .SelectFilterInsideOptGroup("1", "Finance Invoice")
                .SelectFilterInsideOptGroup("1", "Invoice Number")
                .SelectFilterInsideOptGroup("1", "Finance Invoice Status")
                .SelectFilterInsideOptGroup("1", "Finance Transaction")
                .SelectFilterInsideOptGroup("1", "Extract No")
                .SelectFilterInsideOptGroup("1", "Apportioned?")
                .SelectFilterInsideOptGroup("1", "Full Net Amount")
                .SelectFilterInsideOptGroup("1", "For Information Only?")
                .SelectFilterInsideOptGroup("1", "Has Contra?")
                .SelectFilterInsideOptGroup("1", "Schedule Staff Member")
                .SelectFilterInsideOptGroup("1", "Chargeable Units")
                .SelectFilterInsideOptGroup("1", "Responsible Team");

            #endregion

            #region Step 28

            var optionSetId = dbHelper.optionSet.GetOptionSetIdByName("CP Transaction Class")[0];

            var optionSet1Id = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetId, "Additional")[0];
            var optionSet2Id = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetId, "Contra")[0];
            var optionSet3Id = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetId, "End Reason")[0];
            var optionSet4Id = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetId, "Standard")[0];
            var optionSet5Id = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetId, "Apportioned")[0];
            var optionSet6Id = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetId, "Cancelled")[0];
            var optionSet7Id = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetId, "Net Income")[0];
            var optionSet8Id = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetId, "Commitment")[0];
            var optionSet9Id = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetId, "Sundry")[0];
            var optionSet10Id = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetId, "Expense")[0];
            var optionSet11Id = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetId, "Adjustment")[0];

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Transport Types")
                .SelectFilterInsideOptGroup("1", "Transaction Class")
                .ClickRuleValueLookupButton("1");

            lookupPopup
                .WaitForOptionSetLookupPopupToLoad()
                .ValidateResultElementPresent(optionSet1Id)
                .ValidateResultElementPresent(optionSet2Id)
                .ValidateResultElementPresent(optionSet3Id)
                .ValidateResultElementPresent(optionSet4Id)
                .ValidateResultElementPresent(optionSet5Id)
                .ValidateResultElementPresent(optionSet6Id)
                .ValidateResultElementPresent(optionSet7Id)
                .ValidateResultElementPresent(optionSet8Id)
                .ValidateResultElementPresent(optionSet9Id)
                .ValidateResultElementPresent(optionSet10Id)
                .ValidateResultElementPresent(optionSet11Id)
                .ClickCloseButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad();


            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7388")]
        [Description("Step(s) 29 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases022()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-826 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Standard";
            Guid careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, careProviderBatchGroupingName, new DateTime(2020, 1, 1), 8888888);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Per Week Pro Rata", new DateTime(2020, 1, 1), 12);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-826", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var contractserviceadjusteddaysid = 2; //Charge End Date Less 1 Day
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, contractserviceadjusteddaysid, false, false);
            var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 420, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 14);
            var separateinvoices = false;
            var debtorreferencenumberrequired = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, debtorreferencenumberrequired,
                _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            dbHelper.person.UpdateAnonymousForBilling(_personID, true);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 1), null, true);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 1), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Finance Transaction Triggers

            var financeTransactionTriggers = dbHelper.careProviderFinanceTransactionTrigger.GetByRecordId(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactionTriggers.Count);
            var financeTransactionTriggerID = financeTransactionTriggers[0];

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            var financeTransactions = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(2, financeTransactions.Count);

            var financeTransaction1Number = (dbHelper.careProviderFinanceTransaction.GetFinanceTransactionById(financeTransactions[0], "careproviderfinancetransactionnumber")["careproviderfinancetransactionnumber"]).ToString();

            #endregion

            #region Care Provider Finance Invoice batch

            var allFinanceInvoiceBatches = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupID(fibsId);
            Assert.AreEqual(1, allFinanceInvoiceBatches.Count);
            var FinanceInvoicebatchId = allFinanceInvoiceBatches.First();

            #endregion

            #region Schdeduled job for "Process CP Finance Invoice Batches"

            ExecuteScheduleJob("Process CP Finance Invoice Batches");

            #endregion

            #region Care Provider Finance Invoice

            var allFinanceInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(FinanceInvoicebatchId);
            Assert.AreEqual(1, allFinanceInvoiceBatches.Count);
            var financeInvoiceId = allFinanceInvoices.First();

            var financeInvoice1Number = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceById(financeInvoiceId, "invoicenumber")["invoicenumber"].ToString();

            #endregion

            #region Step 29

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_personnumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[1]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateFinanceInvoiceLinkText("Funder Provider " + currentTimeString + " / CPCS_A_" + currentTimeString + " / " + financeInvoice1Number + " / Charges Up to = 07/01/2024")
                .ValidateFinanceInvoiceStatusSelectedText("New")
                .ValidateFinanceInvoiceBatchSetupLinkText("CPCS_A_" + currentTimeString + " \\ Standard \\ 01/01/2024")
                .ValidateInvoiceNoText(financeInvoice1Number)
                .ValidateFinanceInvoiceBatchLinkText("CPCS_A_" + currentTimeString + " \\ Standard \\ 02/01/2024 00:00:00");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-7416

        [TestProperty("JiraIssueID", "ACC-7548")]
        [Description("Step(s) 30 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases023()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-826 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Standard";
            Guid careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, careProviderBatchGroupingName, new DateTime(2020, 1, 1), 8888888);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            bool oneoff = true;
            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "One-Off", new DateTime(2020, 1, 1), 987654, false, false, oneoff, false);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-826", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var contractserviceadjusteddaysid = 2; //Charge End Date Less 1 Day
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, contractserviceadjusteddaysid, false, false);
            var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 420, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 2; //Every 2 Weeks
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 21);
            var separateinvoices = false;
            var debtorreferencenumberrequired = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, debtorreferencenumberrequired,
                _teamId);

            #endregion

            #region Care Provider Extract Type

            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type: Generic Extract File

            #endregion

            #region Mail Merge

            var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            #endregion

            #region Finance Extract Batch Setup

            var careProviderFinanceExtractBatchSetupId = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0), null, careProviderExtractName1Id, 1, _cpGenericExtractTypeId, _invoiceGenerateTemplate_MailMergeId,
                _defaultLoginUserID, "systemuser", _defaultLoginUserName, false, false, false, false, false, false, true);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            dbHelper.person.UpdateAnonymousForBilling(_personID, true);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 1), null, true);

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonId = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, "Default Person Contract Service End Reason", new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 1), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateEndDateTime(careProviderPersonContractServiceId, new DateTime(2024, 1, 14), careProviderPersonContractServiceEndReasonId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Finance Transaction Triggers

            var financeTransactionTriggers = dbHelper.careProviderFinanceTransactionTrigger.GetByRecordId(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactionTriggers.Count);
            var financeTransactionTriggerID = financeTransactionTriggers[0];

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            var financeTransactions = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactions.Count);

            var financeTransaction1Number = (dbHelper.careProviderFinanceTransaction.GetFinanceTransactionById(financeTransactions[0], "careproviderfinancetransactionnumber")["careproviderfinancetransactionnumber"]).ToString();

            #endregion

            #region Step 30

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_personnumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[0]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateTransactionClass("Standard")
                .ValidateStartDateText("01/01/2024")
                .ValidateEndDateText("14/01/2024");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-754")]
        [Description("Step(s) 31 to 32 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases024()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-826 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Standard";
            Guid careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, careProviderBatchGroupingName, new DateTime(2020, 1, 1), 8888888);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            bool oneoff = true;
            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "One-Off", new DateTime(2020, 1, 1), 987654, false, false, oneoff, false);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-826", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var contractserviceadjusteddaysid = 2; //Charge End Date Less 1 Day
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, contractserviceadjusteddaysid, false, false);
            var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 420, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 2; //Every 2 Weeks
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 21);
            var separateinvoices = false;
            var debtorreferencenumberrequired = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, debtorreferencenumberrequired,
                _teamId);

            #endregion

            #region Care Provider Extract Type

            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type: Generic Extract File

            #endregion

            #region Mail Merge

            var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            #endregion

            #region Finance Extract Batch Setup

            var careProviderFinanceExtractBatchSetupId = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0), null, careProviderExtractName1Id, 1, _cpGenericExtractTypeId, _invoiceGenerateTemplate_MailMergeId,
                _defaultLoginUserID, "systemuser", _defaultLoginUserName, false, false, false, false, false, false, true);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            dbHelper.person.UpdateAnonymousForBilling(_personID, true);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 1), null, true);

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonId = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, "Default Person Contract Service End Reason", new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 1), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateEndDateTime(careProviderPersonContractServiceId, new DateTime(2024, 1, 7), careProviderPersonContractServiceEndReasonId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Finance Transaction Triggers

            var financeTransactionTriggers = dbHelper.careProviderFinanceTransactionTrigger.GetByRecordId(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactionTriggers.Count);
            var financeTransactionTriggerID = financeTransactionTriggers[0];

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            var financeTransactions = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactions.Count);

            var financeTransaction1Number = (dbHelper.careProviderFinanceTransaction.GetFinanceTransactionById(financeTransactions[0], "careproviderfinancetransactionnumber")["careproviderfinancetransactionnumber"]).ToString();

            #endregion

            #region Care Provider Finance Invoice batch

            var allFinanceInvoiceBatches = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupID(fibsId);
            Assert.AreEqual(1, allFinanceInvoiceBatches.Count);
            var FinanceInvoicebatchId = allFinanceInvoiceBatches.First();

            #endregion

            #region Schdeduled job for "Process CP Finance Invoice Batches"

            ExecuteScheduleJob("Process CP Finance Invoice Batches");

            #endregion

            #region Care Provider Finance Invoice

            var allFinanceInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(FinanceInvoicebatchId);
            Assert.AreEqual(1, allFinanceInvoiceBatches.Count);
            var financeInvoiceId = allFinanceInvoices.First();

            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(financeInvoiceId, 2); //Completed

            #endregion

            #region Schdeduled job for "Process CP Finance Extract Batches"

            ExecuteScheduleJob("Process CP Finance Extract Batches");

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.UpdateEndDateTime(careProviderPersonContractServiceId, new DateTime(2024, 1, 14), careProviderPersonContractServiceEndReasonId);

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            financeTransactions = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(3, financeTransactions.Count);

            financeTransaction1Number = (dbHelper.careProviderFinanceTransaction.GetFinanceTransactionById(financeTransactions[0], "careproviderfinancetransactionnumber")["careproviderfinancetransactionnumber"]).ToString();

            #endregion


            #region Step 31

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_personnumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[0]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateTransactionClass("Contra")
                .ValidateStartDateText("01/01/2024")
                .ValidateEndDateText("07/01/2024")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[1]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateTransactionClass("Standard")
                .ValidateStartDateText("01/01/2024")
                .ValidateEndDateText("14/01/2024");

            #endregion

            #region Step 32

            cpFinanceTransactionRecordPage
                .ValidateCalculationTraceContainsText("dates: 01/01/2024 to 14/01/2024");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7550")]
        [Description("Step(s) 33 from the original test - Invoice Frequencies : Every Week (by Month)")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases025()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-826 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Standard";
            Guid careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, careProviderBatchGroupingName, new DateTime(2020, 1, 1), 8888888);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Per Week Pro Rata", new DateTime(2020, 1, 1), 12);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-826", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var contractserviceadjusteddaysid = 2; //Charge End Date Less 1 Day
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, contractserviceadjusteddaysid, false, false);
            var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 420, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 21; //Every Week (by Month)
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 2, 11);
            var separateinvoices = false;
            var debtorreferencenumberrequired = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 29), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, debtorreferencenumberrequired,
                _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            dbHelper.person.UpdateAnonymousForBilling(_personID, true);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 29), null, true);

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonId = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, "Default Person Contract Service End Reason", new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 29), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateEndDateTime(careProviderPersonContractServiceId, new DateTime(2024, 2, 11), careProviderPersonContractServiceEndReasonId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Finance Transaction Triggers

            var financeTransactionTriggers = dbHelper.careProviderFinanceTransactionTrigger.GetByRecordId(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactionTriggers.Count);
            var financeTransactionTriggerID = financeTransactionTriggers[0];

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            var financeTransactions = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(3, financeTransactions.Count);

            #endregion

            #region Step 33

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_personnumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[0]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateTransactionClass("Standard")
                .ValidateStartDateText("05/02/2024")
                .ValidateEndDateText("10/02/2024")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[1]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateTransactionClass("Standard")
                .ValidateStartDateText("01/02/2024")
                .ValidateEndDateText("04/02/2024")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[2]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateTransactionClass("Standard")
                .ValidateStartDateText("29/01/2024")
                .ValidateEndDateText("31/01/2024");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7551")]
        [Description("Step(s) 33 from the original test - Invoice Frequencies : Every 2 Weeks (by Month)")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases026()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-826 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Standard";
            Guid careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, careProviderBatchGroupingName, new DateTime(2020, 1, 1), 8888888);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Per Week Pro Rata", new DateTime(2020, 1, 1), 12);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-826", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var contractserviceadjusteddaysid = 2; //Charge End Date Less 1 Day
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, contractserviceadjusteddaysid, false, false);
            var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 420, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 22; //Every 2 Weeks (by Month)
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 2, 11);
            var separateinvoices = false;
            var debtorreferencenumberrequired = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 29), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, debtorreferencenumberrequired,
                _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            dbHelper.person.UpdateAnonymousForBilling(_personID, true);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 29), null, true);

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonId = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, "Default Person Contract Service End Reason", new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 29), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateEndDateTime(careProviderPersonContractServiceId, new DateTime(2024, 2, 11), careProviderPersonContractServiceEndReasonId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Finance Transaction Triggers

            var financeTransactionTriggers = dbHelper.careProviderFinanceTransactionTrigger.GetByRecordId(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactionTriggers.Count);
            var financeTransactionTriggerID = financeTransactionTriggers[0];

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            var financeTransactions = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(3, financeTransactions.Count);

            #endregion

            #region Step 33

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_personnumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[0]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateTransactionClass("Standard")
                .ValidateStartDateText("05/02/2024")
                .ValidateEndDateText("10/02/2024")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[1]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateTransactionClass("Standard")
                .ValidateStartDateText("01/02/2024")
                .ValidateEndDateText("04/02/2024")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[2]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateTransactionClass("Standard")
                .ValidateStartDateText("29/01/2024")
                .ValidateEndDateText("31/01/2024");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7552")]
        [Description("Step(s) 33 from the original test - Invoice Frequencies : Every 4 Weeks (by Month)")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases027()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-826 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Standard";
            Guid careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, careProviderBatchGroupingName, new DateTime(2020, 1, 1), 8888888);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Per Week Pro Rata", new DateTime(2020, 1, 1), 12);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-826", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var contractserviceadjusteddaysid = 2; //Charge End Date Less 1 Day
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, contractserviceadjusteddaysid, false, false);
            var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 420, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 23; //Every 4 Weeks (by Month)
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 2, 11);
            var separateinvoices = false;
            var debtorreferencenumberrequired = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 29), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, debtorreferencenumberrequired,
                _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            dbHelper.person.UpdateAnonymousForBilling(_personID, true);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 29), null, true);

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonId = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, "Default Person Contract Service End Reason", new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 29), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateEndDateTime(careProviderPersonContractServiceId, new DateTime(2024, 2, 11), careProviderPersonContractServiceEndReasonId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Finance Transaction Triggers

            var financeTransactionTriggers = dbHelper.careProviderFinanceTransactionTrigger.GetByRecordId(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactionTriggers.Count);
            var financeTransactionTriggerID = financeTransactionTriggers[0];

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            var financeTransactions = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(3, financeTransactions.Count);

            #endregion

            #region Step 33

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_personnumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[0]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateTransactionClass("Standard")
                .ValidateStartDateText("05/02/2024")
                .ValidateEndDateText("10/02/2024")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[1]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateTransactionClass("Standard")
                .ValidateStartDateText("01/02/2024")
                .ValidateEndDateText("04/02/2024")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[2]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateTransactionClass("Standard")
                .ValidateStartDateText("29/01/2024")
                .ValidateEndDateText("31/01/2024");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7553")]
        [Description("Step(s) 34 to 35 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases028()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-826 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Standard";
            Guid careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, careProviderBatchGroupingName, new DateTime(2020, 1, 1), 8888888);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            bool oneoff = true;
            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "One-Off", new DateTime(2020, 1, 1), 987654, false, false, oneoff, false);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-826", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var contractserviceadjusteddaysid = 2; //Charge End Date Less 1 Day
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, contractserviceadjusteddaysid, false, false);
            var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 420, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 22; //Every 2 Weeks (by Month)
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 2, 25);
            var separateinvoices = false;
            var debtorreferencenumberrequired = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 29), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, debtorreferencenumberrequired,
                _teamId);

            #endregion

            #region Care Provider Extract Type

            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type: Generic Extract File

            #endregion

            #region Mail Merge

            var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            #endregion

            #region Finance Extract Batch Setup

            var careProviderFinanceExtractBatchSetupId = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0), null, careProviderExtractName1Id, 1, _cpGenericExtractTypeId, _invoiceGenerateTemplate_MailMergeId,
                _defaultLoginUserID, "systemuser", _defaultLoginUserName, false, false, false, false, false, false, true);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            dbHelper.person.UpdateAnonymousForBilling(_personID, true);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 29), null, true);

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonId = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, "Default Person Contract Service End Reason", new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 29), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateEndDateTime(careProviderPersonContractServiceId, new DateTime(2024, 2, 4), careProviderPersonContractServiceEndReasonId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Finance Transaction Triggers

            var financeTransactionTriggers = dbHelper.careProviderFinanceTransactionTrigger.GetByRecordId(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactionTriggers.Count);
            var financeTransactionTriggerID = financeTransactionTriggers[0];

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            var financeTransactions = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactions.Count);

            var financeTransaction1Number = (dbHelper.careProviderFinanceTransaction.GetFinanceTransactionById(financeTransactions[0], "careproviderfinancetransactionnumber")["careproviderfinancetransactionnumber"]).ToString();

            #endregion

            #region Care Provider Finance Invoice batch

            var allFinanceInvoiceBatches = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupID(fibsId);
            Assert.AreEqual(1, allFinanceInvoiceBatches.Count);
            var FinanceInvoicebatchId = allFinanceInvoiceBatches.First();

            #endregion

            #region Schdeduled job for "Process CP Finance Invoice Batches"

            ExecuteScheduleJob("Process CP Finance Invoice Batches");

            #endregion

            #region Care Provider Finance Invoice

            var allFinanceInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(FinanceInvoicebatchId);
            Assert.AreEqual(1, allFinanceInvoiceBatches.Count);
            var financeInvoiceId = allFinanceInvoices.First();
            var financeInvoiceNumber = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceById(financeInvoiceId, "invoicenumber")["invoicenumber"].ToString();

            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(financeInvoiceId, 2); //Completed

            #endregion

            #region Schdeduled job for "Process CP Finance Extract Batches"

            ExecuteScheduleJob("Process CP Finance Extract Batches");

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.UpdateEndDateTime(careProviderPersonContractServiceId, new DateTime(2024, 2, 11), careProviderPersonContractServiceEndReasonId);

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            financeTransactions = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(3, financeTransactions.Count);

            #endregion


            #region Step 34

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_personnumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[0]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateTransactionClass("Contra")
                .ValidateStartDateText("29/01/2024")
                .ValidateEndDateText("04/02/2024")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[1]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateTransactionClass("Standard")
                .ValidateStartDateText("29/01/2024")
                .ValidateEndDateText("11/02/2024");

            #endregion

            #region Step 35

            cpFinanceTransactionRecordPage
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[2]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ClickFinanceExtractBatchLink();

            lookupViewPopup.WaitForLookupViewPopupToLoad().ClickViewButton();

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ClickInvoiceFiles_FileLink();

            var fileExists = fileIOHelper.WaitForFileToExist(this.DownloadsDirectory, "Funder-Provider-" + currentTimeString + "-" + financeInvoiceNumber + "-" + DateTime.Now.ToString("dd_MM_yyyy") + ".pdf", 15);
            Assert.IsTrue(fileExists);

            bool textFound = pdfHelper.FindText("for the period 29/01/2024 to 04/02/2024", this.DownloadsDirectory + "\\Funder-Provider-" + currentTimeString + "-" + financeInvoiceNumber + "-" + DateTime.Now.ToString("dd_MM_yyyy") + ".pdf");
            Assert.IsTrue(textFound);

            #endregion


        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-7417

        [TestProperty("JiraIssueID", "ACC-7554")]
        [Description("Step(s) 36 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases029()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-826 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Standard";
            Guid careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, careProviderBatchGroupingName, new DateTime(2020, 1, 1), 8888888);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Per Week Pro Rata", new DateTime(2020, 1, 1), 12);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-826", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var contractserviceadjusteddaysid = 2; //Charge End Date Less 1 Day
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, contractserviceadjusteddaysid, false, false);
            var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 420, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 23; //Every 4 Weeks (by Month)
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 2, 11);
            var separateinvoices = false;
            var debtorreferencenumberrequired = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 29), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, debtorreferencenumberrequired,
                _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            dbHelper.person.UpdateAnonymousForBilling(_personID, true);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 29), null, true);

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonId = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, "Default Person Contract Service End Reason", new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 29), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateAccountCode(careProviderPersonContractServiceId, currentTimeString);
            dbHelper.careProviderPersonContractService.UpdateEndDateTime(careProviderPersonContractServiceId, new DateTime(2024, 2, 11), careProviderPersonContractServiceEndReasonId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Finance Transaction Triggers

            var financeTransactionTriggers = dbHelper.careProviderFinanceTransactionTrigger.GetByRecordId(careProviderPersonContractServiceId);
            Assert.AreEqual(1, financeTransactionTriggers.Count);
            var financeTransactionTriggerID = financeTransactionTriggers[0];

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            var financeTransactions = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(3, financeTransactions.Count);

            #endregion

            #region Step 36

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_personnumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[0]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateAccountCodeText(currentTimeString)
                .ValidateAccountCodeFieldEnabled(false)
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[1]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateAccountCodeText(currentTimeString)
                .ValidateAccountCodeFieldEnabled(false)
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(financeTransactions[2]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateAccountCodeText(currentTimeString)
                .ValidateAccountCodeFieldEnabled(false);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7555")]
        [Description("Step(s) 37 from the original test - Transaction Class = Standard")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases030()
        {
            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            var nextWeekMonday = thisWeekMonday.AddDays(7);

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var bookingTypeClassId = 5; //Booking (To Service User)
            var workingContractedTime = 1; //Count full booking length
            var bookingChargeType = 3; //Per Staff Number
            var _bookingType5 = commonMethodsDB.CreateCPBookingType("BTC ACC-826 T5", bookingTypeClassId, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), workingContractedTime, false, null, null, null, bookingChargeType);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType5, true);

            #endregion

            #region Care Provider Service

            var isScheduledService = true;
            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, isScheduledService);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Standard";
            Guid careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, careProviderBatchGroupingName, new DateTime(2020, 1, 1), 8888888);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Per Week Pro Rata", new DateTime(2020, 1, 1), 12);

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType5, null, "");

            #endregion

            #region Care Provider Contract Service

            var contractserviceadjusteddaysid = 2; //Charge End Date Less 1 Day
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType5, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, contractserviceadjusteddaysid, false, false);
            var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 420, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            DateTime? financetransactionsupto = null;
            var startDate = thisWeekMonday;
            var separateinvoices = false;
            var debtorreferencenumberrequired = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                startDate, new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, debtorreferencenumberrequired,
                _teamId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT ACC-826", "91710", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region Recurrence Patterns

            _recurrencePattern_Every1WeekMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on monday").First();
            _recurrencePattern_Every1WeekTuesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on tuesday").First();
            _recurrencePattern_Every1WeekWednesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on wednesday").First();
            _recurrencePattern_Every1WeekThursdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on thursday").First();
            _recurrencePattern_Every1WeekFridayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on friday").First();
            _recurrencePattern_Every1WeekSaturdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on saturday").First();
            _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region System User

            var user1name = "ftsu_" + currentTimeString;
            var user1FirstName = "Finance Transactions";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser1Id, 40);

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType5);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            dbHelper.person.UpdateAnonymousForBilling(_personID, true);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, thisWeekMonday, null, true);

            #endregion

            #region Diary Booking

            var cpBookingDiaryId = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType5, providerId, nextWeekMonday, new TimeSpan(9, 0, 0), nextWeekMonday, new TimeSpan(12, 0, 0), careProviderService1Id);
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", cpBookingDiaryId, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_teamId, cpBookingDiaryId, _personID, _personcontractId, careProviderContractServiceId);

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            var financeTransactions = dbHelper.careProviderFinanceTransaction.GetByCareProviderContractService(careProviderContractServiceId);
            Assert.AreEqual(1, financeTransactions.Count);

            #endregion

            #region Step 37

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_personnumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickFinanceTransactionTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontract", false)
                .OpenRecord(financeTransactions[0]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateTransactionClass("Standard")
                .ValidateRosteredEmployeeLinkText("Finance Transactions System User " + currentTimeString)
                .ValidateScheduleStaffMemberFieldVisibility(false);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7556")]
        [Description("Step(s) 37 from the original test - Transaction Class = Commitment")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases031()
        {
            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            var nextWeekMonday = thisWeekMonday.AddDays(7);

            #region Team

            var defaultTeamId = dbHelper.team.GetFirstTeams(1, 1, true)[0];

            #endregion

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var bookingTypeClassId = 5; //Booking (To Service User)
            var workingContractedTime = 1; //Count full booking length
            var bookingChargeType = 3; //Per Staff Number
            var _bookingType5 = commonMethodsDB.CreateCPBookingType("BTC ACC-826 T5", bookingTypeClassId, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), workingContractedTime, false, null, null, null, bookingChargeType);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType5, true);

            #endregion

            #region Care Provider Service

            var isScheduledService = true;
            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, isScheduledService);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Standard";
            Guid careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(defaultTeamId, careProviderBatchGroupingName, new DateTime(2020, 1, 1), 8888888);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(defaultTeamId, "CPRU ACC-7556 A", new DateTime(2020, 1, 1), 750000, true);

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType5, null, "");

            #endregion

            #region Care Provider Contract Service

            var contractserviceadjusteddaysid = 2; //Charge End Date Less 1 Day
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType5, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, contractserviceadjusteddaysid, false, false);
            var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

            #endregion

            #region Timeband Set

            var timebandSetId = commonMethodsDB.CreateTimebandSet("TBS ACC-7202", defaultTeamId);

            #endregion

            #region Timeband

            var startDay = 1; //Monday
            var endDay = 7; //Sunday
            var timebandId = commonMethodsDB.CreateTimeband(defaultTeamId, timebandSetId, startDay, new TimeSpan(0, 0, 0), endDay, new TimeSpan(0, 0, 0));

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 420, _teamId, timebandSetId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            DateTime? financetransactionsupto = null;
            var startDate = thisWeekMonday;
            var separateinvoices = false;
            var debtorreferencenumberrequired = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                startDate, new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, debtorreferencenumberrequired,
                _teamId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT ACC-826", "91710", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region Recurrence Patterns

            _recurrencePattern_Every1WeekMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on monday").First();
            _recurrencePattern_Every1WeekTuesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on tuesday").First();
            _recurrencePattern_Every1WeekWednesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on wednesday").First();
            _recurrencePattern_Every1WeekThursdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on thursday").First();
            _recurrencePattern_Every1WeekFridayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on friday").First();
            _recurrencePattern_Every1WeekSaturdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on saturday").First();
            _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region System User

            var user1name = "ftsu_" + currentTimeString;
            var user1FirstName = "Finance Transactions";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser1Id, 40);

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType5);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            dbHelper.person.UpdateAnonymousForBilling(_personID, true);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, thisWeekMonday, null, true);

            #endregion

            #region Provider Schedule

            var cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType5, 1, 1, 1, new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0), providerId, "", careProviderService1Id);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingScheduleId, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, cpBookingScheduleId, _personID, _personcontractId, careProviderContractServiceId);

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            var financeTransactions = dbHelper.careProviderFinanceTransaction.GetByCareProviderContractService(careProviderContractServiceId);
            Assert.IsTrue(financeTransactions.Count > 0);

            #endregion

            #region Step 37

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_personnumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickFinanceTransactionTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontract", false)
                .OpenRecord(financeTransactions[0]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateTransactionClass("Commitment")
                .ValidateScheduleStaffMemberLinkText("Finance Transactions System User " + currentTimeString + " - Finance Transactions System User " + currentTimeString + " - CPSRT ACC-826; Status: Active")
                .ValidateDiaryStaffMemberFieldVisibility(false)
                .ValidateScheduleStaffMemberFieldVisibility(true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7557")]
        [Description("Step(s) 38 from the original test - 'Chargeable Units' is completed with the same value as recorded in Total Units")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases032()
        {
            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            var nextWeekMonday = thisWeekMonday.AddDays(7);

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var bookingTypeClassId = 5; //Booking (To Service User)
            var workingContractedTime = 1; //Count full booking length
            var bookingChargeType = 3; //Per Staff Number
            var _bookingType5 = commonMethodsDB.CreateCPBookingType("BTC ACC-826 T5", bookingTypeClassId, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), workingContractedTime, false, null, null, null, bookingChargeType);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType5, true);

            #endregion

            #region Care Provider Service

            var isScheduledService = true;
            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, isScheduledService);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Standard";
            Guid careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, careProviderBatchGroupingName, new DateTime(2020, 1, 1), 8888888);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Per Week Pro Rata", new DateTime(2020, 1, 1), 12);

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType5, null, "");

            #endregion

            #region Care Provider Contract Service

            var contractserviceadjusteddaysid = 2; //Charge End Date Less 1 Day
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType5, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, contractserviceadjusteddaysid, false, false);
            var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 420, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            DateTime? financetransactionsupto = null;
            var startDate = thisWeekMonday;
            var separateinvoices = false;
            var debtorreferencenumberrequired = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                startDate, new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, debtorreferencenumberrequired,
                _teamId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT ACC-826", "91710", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region Recurrence Patterns

            _recurrencePattern_Every1WeekMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on monday").First();
            _recurrencePattern_Every1WeekTuesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on tuesday").First();
            _recurrencePattern_Every1WeekWednesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on wednesday").First();
            _recurrencePattern_Every1WeekThursdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on thursday").First();
            _recurrencePattern_Every1WeekFridayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on friday").First();
            _recurrencePattern_Every1WeekSaturdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on saturday").First();
            _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region System User

            var user1name = "ftsu_" + currentTimeString;
            var user1FirstName = "Finance Transactions";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser1Id, 40);

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType5);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            dbHelper.person.UpdateAnonymousForBilling(_personID, true);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, thisWeekMonday, null, true);

            #endregion

            #region Diary Booking

            var cpBookingDiaryId = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType5, providerId, nextWeekMonday, new TimeSpan(9, 0, 0), nextWeekMonday, new TimeSpan(12, 0, 0), careProviderService1Id);
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", cpBookingDiaryId, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_teamId, cpBookingDiaryId, _personID, _personcontractId, careProviderContractServiceId);

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            var financeTransactions = dbHelper.careProviderFinanceTransaction.GetByCareProviderContractService(careProviderContractServiceId);
            Assert.AreEqual(1, financeTransactions.Count);

            #endregion

            #region Step 37

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_personnumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickFinanceTransactionTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontract", false)
                .OpenRecord(financeTransactions[0]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateTotalUnitsText("3.00")
                .ValidateChargeableUnitsText("3.00");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7558")]
        [Description("Step(s) 38 from the original test - Booking Type (on the FT) has Booking Charge Type =  Per Booking / Client Number (Numeric Code = 2)")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void CPFinanceTransactions_ACC_7202_UITestCases033()
        {
            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            var nextWeekMonday = thisWeekMonday.AddDays(7);

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var bookingTypeClassId = 2; //Booking (to internal care activity)
            var workingContractedTime = 1; //Count full booking length
            var bookingChargeType = 2; //Per Booking / Client Number
            var _bookingType5 = commonMethodsDB.CreateCPBookingType("BTC ACC-826 T5 B", bookingTypeClassId, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), workingContractedTime, false, null, null, null, bookingChargeType);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType5, true);

            #endregion

            #region Care Provider Service

            var isScheduledService = true;
            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, isScheduledService);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Standard";
            Guid careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, careProviderBatchGroupingName, new DateTime(2020, 1, 1), 8888888);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Per Week Pro Rata", new DateTime(2020, 1, 1), 12);

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, null, _bookingType5, null, "");

            #endregion

            #region Care Provider Contract Service

            var contractserviceadjusteddaysid = 2; //Charge End Date Less 1 Day
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType5, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, contractserviceadjusteddaysid, false, false);
            var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 420, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            DateTime? financetransactionsupto = null;
            var startDate = thisWeekMonday;
            var separateinvoices = false;
            var debtorreferencenumberrequired = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                startDate, new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, debtorreferencenumberrequired,
                _teamId);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT ACC-826", "91710", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region Recurrence Patterns

            _recurrencePattern_Every1WeekMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on monday").First();
            _recurrencePattern_Every1WeekTuesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on tuesday").First();
            _recurrencePattern_Every1WeekWednesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on wednesday").First();
            _recurrencePattern_Every1WeekThursdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on thursday").First();
            _recurrencePattern_Every1WeekFridayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on friday").First();
            _recurrencePattern_Every1WeekSaturdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on saturday").First();
            _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region System User

            var user1name = "ftsu_" + currentTimeString;
            var user1FirstName = "Finance Transactions";
            var user1LastName = "System User " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateMaximumWorkingHours(systemUser1Id, 40);

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType5);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var _person1ID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);
            var _person1Number = (int)dbHelper.person.GetPersonById(_person1ID, "personnumber")["personnumber"];

            firstName = "Pedro";
            var _person2ID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontract1Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _person1ID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, thisWeekMonday, null, true);
            var _personcontract2Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _person2ID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, thisWeekMonday, null, true);

            #endregion

            #region Diary Booking

            var cpBookingDiaryId = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType5, providerId, nextWeekMonday, new TimeSpan(9, 0, 0), nextWeekMonday, new TimeSpan(12, 0, 0), careProviderService1Id);
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", cpBookingDiaryId, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_teamId, cpBookingDiaryId, _person1ID, _personcontract1Id, careProviderContractServiceId);
            dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_teamId, cpBookingDiaryId, _person2ID, _personcontract2Id, careProviderContractServiceId);

            #endregion

            #region Schedule Job

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transaction

            var financeTransactions = dbHelper.careProviderFinanceTransaction.GetByPersonId(_person1ID);
            Assert.AreEqual(1, financeTransactions.Count);

            #endregion

            #region Step 37

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .InsertID(_person1Number)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_person1ID);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(_personcontract1Id);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickFinanceTransactionTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontract", false)
                .OpenRecord(financeTransactions[0]);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateTotalUnitsText("3.00")
                .ValidateChargeableUnitsText("1.50");

            #endregion

        }

        #endregion

    }
}

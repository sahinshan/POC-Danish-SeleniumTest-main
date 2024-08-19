using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phoenix.UITests.Framework.WebAppAPI.Entities.CareDirector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CareCloud.AdvanceFinancials.UITests
{
    [TestClass]
    public class APITests : BaseTestClass
    {
        #region Private properties

        private string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _teamId;
        private Guid _defaultSystemUserId;
        private Guid _businessUnitId;


        #endregion

        #region Private Methods

        internal void ProcessCPFinanceScheduledJob(string ScheduledJobName)
        {
            #region Process CP Finance Extract Batches

            //Get the schedule job id
            Guid processCpFinanceScheduledJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName(ScheduledJobName)[0];

            //authenticate against the v6 Web API
            this.webAPIHelper.Security.Authenticate();

            //execute the schedule job and wait for the Idle status
            this.webAPIHelper.ScheduleJob.Execute(processCpFinanceScheduledJobId.ToString(), this.webAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the authentication using the web api class
            dbHelper = new Phoenix.DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(processCpFinanceScheduledJobId);

            #endregion
        }

        #endregion

        [TestInitialize()]
        public void TestsInitialization()
        {
            #region Default User

            string username = ConfigurationManager.AppSettings["Username"];
            string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

            username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
            _defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];

            TimeZone localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_defaultSystemUserId, localZone.StandardName);

            #endregion

            #region Authentication

            var _authenticationProviderId = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").First();

            #endregion Authentication

            #region Business Unit

            _businessUnitId = commonMethodsDB.CreateBusinessUnit("API AF BU A");

            #endregion

            #region Team

            var _teamName = "API AF Team A";
            _teamId = commonMethodsDB.CreateTeam(_teamName, null, _businessUnitId, "07790", "API_AF_Team_A@careworkstempmail.com", _teamName, "910 123456");

            #endregion

            #region Language

            var _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

            #endregion Language

            #region Create default system user

            var _loginUsername = "api_af_user1";
            var _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUsername, "API AF", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationProviderId);

            #endregion

            #region Finance Extract Batches

            /*Update all Finance Extract Batches so that we have no record with "Downloaded?" set to "No" */
            /*This is necessary because if we have more than 250 Finance Extract Batch records that where never downloaded then the API will only return the 250 oldest records first*/
            /*This will cause failures in all test that validate the Finance Extract Batch information returned by the API*/

            foreach (Guid financeExtractBatchId in dbHelper.careProviderFinanceExtractBatch.GetAllRecordsNeverDownloaded())
                dbHelper.careProviderFinanceExtractBatch.UpdateDownloaded(financeExtractBatchId, true);

            #endregion
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "ACC-8886")]
        [Description("Test for the HealthCheck api method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        public void API_TestMethod001()
        {
            webAPIHelper.AdvanceFinancials.Authenticate();

            webAPIHelper.AdvanceFinancials.HealthCheck();
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "ACC-8887")]
        [Description("Test for the Pending Finance Extracts api method - 1 Finance Extract Batch (with extract content set) is generated - Validate that the finance extract batch data is present in the data returned by the api")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        public void API_TestMethod002()
        {
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region VAT Code
            dbHelper = new Phoenix.DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _careProviderVATCodeName = "Standard Rated";
            Guid _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName(_careProviderVATCodeName)[0];

            #endregion

            #region Care Provider Rate Unit

            string _careProviderRateUnitName = "Per Week Pro Rata";
            Guid _careProviderRateUnitId = dbHelper.careProviderRateUnit.GetByName(_careProviderRateUnitName)[0];

            #endregion

            #region Care Provider Batch Grouping

            string _careProviderBatchGroupingName = "Monthly";
            Guid _careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(_careProviderBatchGroupingName)[0];

            #endregion

            #region Provider

            var provider1Name = "Provider " + currentTimeString;
            var provider2Name = "Parent Provider " + currentTimeString;


            var providerType1 = 13; //Residential Establishment
            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _teamId, providerType1, false);

            var providerType2 = 10; //Local Authority
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _teamId, providerType2, false);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service

            var _careProviderServiceName = "Care Provider Service Record";
            var _careProviderServiceId = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName, new DateTime(2020, 1, 1), 7895, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var careProviderServiceDetailName = "Care Provider Service Details Test 1";
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, code, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(_careProviderServiceId, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Mail Merge

            var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            #endregion

            #region Extract Name

            var ExtractNames = dbHelper.careProviderExtractName.GetAllCareProviderExtractName();
            int newExtractNameCode1 = ExtractNames.Count();

            while (dbHelper.careProviderExtractName.GetByCode(newExtractNameCode1).Any())
                newExtractNameCode1 = newExtractNameCode1 + 1;

            var _cpExtractNameId1 = commonMethodsDB.CreateCareProviderExtractName(_teamId, "CPEN_A1_" + currentTimeString, newExtractNameCode1, null, currentDate);

            #endregion

            #region Extract Type

            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type = Generic Extract File

            #endregion

            #region Finance Extract Batch Setup

            var _cpFinanceExtractBatchSetupId1 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, currentDate, new TimeSpan(2, 0, 0), null, _cpExtractNameId1, 1, _cpGenericExtractTypeId, _invoiceGenerateTemplate_MailMergeId);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateCareProviderFinanceExtractBatchSetupExtractOnDayFlagToYesByCurrentDayOfWeek(_cpFinanceExtractBatchSetupId1, currentDate);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateGenerateAndSendInvoicesAutomaticallyFlag(_cpFinanceExtractBatchSetupId1, true);

            #endregion

            #region Finance Extract Batch

            var _cpFinanceExtractBatchId = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1);
            Assert.AreEqual(1, _cpFinanceExtractBatchId.Count);
            var _cpFinanceExtractBatch1Id = _cpFinanceExtractBatchId[0];

            var financeExtractBatchFields = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch1Id, "batchid", "name");

            var _cpFinanceExtractBatch1Number = (int)financeExtractBatchFields["batchid"];
            var _cpFinanceExtractBatch1Name = (string)financeExtractBatchFields["name"];

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "CS_" + currentTimeString;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultSystemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, provider1ID, provider2ID);

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultSystemUserId, _businessUnitId, "", provider1ID, provider2ID, careProviderContractSchemeId, _careProviderServiceId, careProviderServiceDetailId, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            dbHelper.careProviderContractService.UpdateCareProviderServiceDetailId(CareProviderContractServiceId, careProviderServiceDetailId);
            var CareProviderContractServiceTitle = (dbHelper.careProviderContractService.GetCareProviderContractServiceByID(CareProviderContractServiceId, "title")["title"]).ToString();

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(CareProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup
            var todayDate = currentDate;
            var invoicebyid = 2; //funder/service user
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 1; //Monday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(currentDate.Year, 12, 31);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractSchemeId, _careProviderBatchGroupingId,
                new DateTime(2023, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                _cpExtractNameId1, false,
                _teamId);

            #endregion

            #region Person

            var firstName = "Aaron";
            var lastName = currentTimeString;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Care Provider Person Contract

            var StartDate = new DateTime(2024, 5, 14);
            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultSystemUserId, provider1ID, careProviderContractSchemeId, provider2ID, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractSchemeId, _careProviderServiceId, CareProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batches

            var _cpFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId)[0];

            var runOn = commonMethodsHelper.GetCurrentDateWithoutCulture().AddSeconds(3);

            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId, runOn, new DateTime(2024, 5, 14), new DateTime(2024, 5, 21));

            Thread.Sleep(5000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            #endregion

            #region Finance Invoices

            var financeInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(_cpFinanceInvoiceBatchId);
            Assert.AreEqual(1, financeInvoices.Count);
            var financeInvoiceId = financeInvoices[0];

            //update invoice status to completed
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(financeInvoiceId, 2);

            #endregion

            #region Process CP Finance Extract Batches

            ProcessCPFinanceScheduledJob("Process CP Finance Extract Batches");

            #endregion

            financeExtractBatchFields = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch1Id, "extractcontent");
            var _cpFinanceExtractBatch1ContentId = (Guid)financeExtractBatchFields["extractcontent"];

            var documentFileFields = dbHelper.documentFile.GetByID(_cpFinanceExtractBatch1ContentId, "name", "filesize");
            var _cpFinanceExtractBatch1ContentName = (string)documentFileFields["name"];
            var _cpFinanceExtractBatch1ContentSize = (int)documentFileFields["filesize"];

            /**Authenticate against the API**/
            webAPIHelper.AdvanceFinancials.Authenticate();

            /**Call the api method to get all pending finance extracts**/
            var financeData = webAPIHelper.AdvanceFinancials.PendingFinanceExtracts();

            /**We should have 1 match for the finance extract batch generated by this test**/
            var financeExtractBatches = financeData.data.Where(c => c.batchid == _cpFinanceExtractBatch1Number).ToList();
            Assert.AreEqual(1, financeExtractBatches.Count);

            /**Validate the data returned by the API**/
            Assert.AreEqual(_cpFinanceExtractBatch1Id, financeExtractBatches[0].id);
            Assert.AreEqual(_cpFinanceExtractBatch1Name, financeExtractBatches[0].name);

            Assert.AreEqual(_cpFinanceExtractBatch1ContentId, financeExtractBatches[0].extractcontent.id);
            Assert.AreEqual(_cpFinanceExtractBatch1ContentName, financeExtractBatches[0].extractcontent.name);
            Assert.AreEqual(Math.Round((decimal)(_cpFinanceExtractBatch1ContentSize) / 1024M, 2) + " KB", financeExtractBatches[0].extractcontent.size);
            Assert.AreEqual("/api/files/download-url/careproviderfinanceextractbatch/" + _cpFinanceExtractBatch1ContentId.ToString(), financeExtractBatches[0].extractcontent.uri);

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "ACC-8888")]
        [Description("Test for the Pending Finance Extracts api method - 2 Finance Extract Batch (with extract content set) are generated - Validate that the finance extract batches data is present in the data returned by the api")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        public void API_TestMethod003()
        {
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region VAT Code
            dbHelper = new Phoenix.DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _careProviderVATCodeName = "Standard Rated";
            Guid _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName(_careProviderVATCodeName)[0];

            #endregion

            #region Care Provider Rate Unit

            string _careProviderRateUnitName = "Per Week Pro Rata";
            Guid _careProviderRateUnitId = dbHelper.careProviderRateUnit.GetByName(_careProviderRateUnitName)[0];

            #endregion

            #region Care Provider Batch Grouping

            string _careProviderBatchGroupingName = "Monthly";
            Guid _careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(_careProviderBatchGroupingName)[0];

            #endregion

            #region Provider

            var provider1Name = "Provider " + currentTimeString;
            var provider2Name = "Parent Provider " + currentTimeString;


            var providerType1 = 13; //Residential Establishment
            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _teamId, providerType1, false);

            var providerType2 = 10; //Local Authority
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _teamId, providerType2, false);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service

            var _careProviderServiceName = "Care Provider Service Record";
            var _careProviderServiceId = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName, new DateTime(2020, 1, 1), 7895, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var careProviderServiceDetailName = "Care Provider Service Details Test 1";
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, code, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(_careProviderServiceId, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Mail Merge

            var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            #endregion

            #region Extract Name

            var ExtractNames = dbHelper.careProviderExtractName.GetAllCareProviderExtractName();
            int newExtractNameCode1 = ExtractNames.Count();

            while (dbHelper.careProviderExtractName.GetByCode(newExtractNameCode1).Any())
                newExtractNameCode1 = newExtractNameCode1 + 1;

            var _cpExtractNameId1 = commonMethodsDB.CreateCareProviderExtractName(_teamId, "CPEN_A1_" + currentTimeString, newExtractNameCode1, null, currentDate);

            #endregion

            #region Extract Type

            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type = Generic Extract File

            #endregion

            #region Finance Extract Batch Setup

            var _cpFinanceExtractBatchSetupId1 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, currentDate, new TimeSpan(2, 0, 0), null, _cpExtractNameId1, 1, _cpGenericExtractTypeId, _invoiceGenerateTemplate_MailMergeId);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateCareProviderFinanceExtractBatchSetupExtractOnDayFlagToYesByCurrentDayOfWeek(_cpFinanceExtractBatchSetupId1, currentDate);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateGenerateAndSendInvoicesAutomaticallyFlag(_cpFinanceExtractBatchSetupId1, true);

            #endregion

            #region Finance Extract Batch

            var _cpFinanceExtractBatches = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1);
            Assert.AreEqual(1, _cpFinanceExtractBatches.Count);
            var _cpFinanceExtractBatch1Id = _cpFinanceExtractBatches[0];

            var financeExtractBatchFields = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch1Id, "batchid", "name");

            var _cpFinanceExtractBatch1Number = (int)financeExtractBatchFields["batchid"];
            var _cpFinanceExtractBatch1Name = (string)financeExtractBatchFields["name"];

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "CS_" + currentTimeString;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultSystemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, provider1ID, provider2ID);

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultSystemUserId, _businessUnitId, "", provider1ID, provider2ID, careProviderContractSchemeId, _careProviderServiceId, careProviderServiceDetailId, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            dbHelper.careProviderContractService.UpdateCareProviderServiceDetailId(CareProviderContractServiceId, careProviderServiceDetailId);
            var CareProviderContractServiceTitle = (dbHelper.careProviderContractService.GetCareProviderContractServiceByID(CareProviderContractServiceId, "title")["title"]).ToString();

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(CareProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup
            var todayDate = currentDate;
            var invoicebyid = 2; //funder/service user
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 1; //Monday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(currentDate.Year, 12, 31);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractSchemeId, _careProviderBatchGroupingId,
                new DateTime(2023, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                _cpExtractNameId1, false,
                _teamId);

            #endregion

            #region Person

            var firstName = "Aaron";
            var lastName = currentTimeString;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Care Provider Person Contract

            var StartDate = new DateTime(2024, 5, 14);
            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultSystemUserId, provider1ID, careProviderContractSchemeId, provider2ID, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractSchemeId, _careProviderServiceId, CareProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion



            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batches

            var _cpFinanceInvoiceBatch1Id = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId)[0];

            var runOn = commonMethodsHelper.GetCurrentDateWithoutCulture().AddSeconds(3);

            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatch1Id, runOn, new DateTime(2024, 5, 14), new DateTime(2024, 5, 20));

            Thread.Sleep(5000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            #endregion

            #region Finance Invoices

            var financeInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(_cpFinanceInvoiceBatch1Id);
            Assert.AreEqual(1, financeInvoices.Count);
            var financeInvoiceId = financeInvoices[0];

            //update invoice status to completed
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(financeInvoiceId, 2);

            #endregion

            #region Process CP Finance Extract Batches

            ProcessCPFinanceScheduledJob("Process CP Finance Extract Batches");

            #endregion



            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batches

            var _cpFinanceInvoiceBatch2Id = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId)[0];

            runOn = commonMethodsHelper.GetCurrentDateWithoutCulture().AddSeconds(3);

            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatch2Id, runOn, new DateTime(2024, 5, 21), new DateTime(2024, 5, 27));

            Thread.Sleep(5000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            #endregion

            #region Finance Invoices

            financeInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(_cpFinanceInvoiceBatch2Id);
            Assert.AreEqual(1, financeInvoices.Count);
            financeInvoiceId = financeInvoices[0];

            //update invoice status to completed
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(financeInvoiceId, 2);

            #endregion

            #region Finance Extract Batch

            _cpFinanceExtractBatches = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1);
            Assert.AreEqual(2, _cpFinanceExtractBatches.Count);
            var _cpFinanceExtractBatch2Id = _cpFinanceExtractBatches[0];

            runOn = commonMethodsHelper.GetCurrentDateWithoutCulture().AddSeconds(3);
            dbHelper.careProviderFinanceExtractBatch.UpdateCareProviderFinanceExtractBatchRunOnDate(_cpFinanceExtractBatch2Id, runOn);
            Thread.Sleep(5000);

            financeExtractBatchFields = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch2Id, "batchid", "name");
            var _cpFinanceExtractBatch2Number = (int)financeExtractBatchFields["batchid"];
            var _cpFinanceExtractBatch2Name = (string)financeExtractBatchFields["name"];

            #endregion

            #region Process CP Finance Extract Batches

            ProcessCPFinanceScheduledJob("Process CP Finance Extract Batches");

            #endregion




            financeExtractBatchFields = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch1Id, "extractcontent");
            var _cpFinanceExtractBatchContentId = (Guid)financeExtractBatchFields["extractcontent"];

            var documentFileFields = dbHelper.documentFile.GetByID(_cpFinanceExtractBatchContentId, "name", "filesize");
            var _cpFinanceExtractBatch1ContentName = (string)documentFileFields["name"];
            var _cpFinanceExtractBatch1ContentSize = (int)documentFileFields["filesize"];

            /**Authenticate against the API**/
            webAPIHelper.AdvanceFinancials.Authenticate();

            /**Call the api method to get all pending finance extracts**/
            var financeData = webAPIHelper.AdvanceFinancials.PendingFinanceExtracts();

            /**We should have 1 match for the finance extract batch generated by this test**/
            var financeExtractBatches = financeData.data.Where(c => c.batchid == _cpFinanceExtractBatch1Number).ToList();
            Assert.AreEqual(1, financeExtractBatches.Count);

            /**Validate the data returned by the API**/
            Assert.AreEqual(_cpFinanceExtractBatch1Id, financeExtractBatches[0].id);
            Assert.AreEqual(_cpFinanceExtractBatch1Name, financeExtractBatches[0].name);

            Assert.AreEqual(_cpFinanceExtractBatchContentId, financeExtractBatches[0].extractcontent.id);
            Assert.AreEqual(_cpFinanceExtractBatch1ContentName, financeExtractBatches[0].extractcontent.name);
            Assert.AreEqual(Math.Round((decimal)(_cpFinanceExtractBatch1ContentSize) / 1024M, 2) + " KB", financeExtractBatches[0].extractcontent.size);
            Assert.AreEqual("/api/files/download-url/careproviderfinanceextractbatch/" + _cpFinanceExtractBatchContentId.ToString(), financeExtractBatches[0].extractcontent.uri);




            financeExtractBatchFields = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch2Id, "extractcontent");
            _cpFinanceExtractBatchContentId = (Guid)financeExtractBatchFields["extractcontent"];

            documentFileFields = dbHelper.documentFile.GetByID(_cpFinanceExtractBatchContentId, "name", "filesize");
            var _cpFinanceExtractBatch2ContentName = (string)documentFileFields["name"];
            var _cpFinanceExtractBatch2ContentSize = (int)documentFileFields["filesize"];

            /**Call the api method to get all pending finance extracts**/
            financeData = webAPIHelper.AdvanceFinancials.PendingFinanceExtracts();

            /**We should have 1 match for the finance extract batch generated by this test**/
            financeExtractBatches = financeData.data.Where(c => c.batchid == _cpFinanceExtractBatch2Number).ToList();
            Assert.AreEqual(1, financeExtractBatches.Count);

            /**Validate the data returned by the API**/
            Assert.AreEqual(_cpFinanceExtractBatch2Id, financeExtractBatches[0].id);
            Assert.AreEqual(_cpFinanceExtractBatch2Name, financeExtractBatches[0].name);

            Assert.AreEqual(_cpFinanceExtractBatchContentId, financeExtractBatches[0].extractcontent.id);
            Assert.AreEqual(_cpFinanceExtractBatch2ContentName, financeExtractBatches[0].extractcontent.name);
            Assert.AreEqual(Math.Round((decimal)(_cpFinanceExtractBatch2ContentSize) / 1024M, 2) + " KB", financeExtractBatches[0].extractcontent.size);
            Assert.AreEqual("/api/files/download-url/careproviderfinanceextractbatch/" + _cpFinanceExtractBatchContentId.ToString(), financeExtractBatches[0].extractcontent.uri);

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "ACC-8889")]
        [Description("Test for the Get Signed URL api method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        public void API_TestMethod004()
        {
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region VAT Code
            dbHelper = new Phoenix.DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _careProviderVATCodeName = "Standard Rated";
            Guid _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName(_careProviderVATCodeName)[0];

            #endregion

            #region Care Provider Rate Unit

            string _careProviderRateUnitName = "Per Week Pro Rata";
            Guid _careProviderRateUnitId = dbHelper.careProviderRateUnit.GetByName(_careProviderRateUnitName)[0];

            #endregion

            #region Care Provider Batch Grouping

            string _careProviderBatchGroupingName = "Monthly";
            Guid _careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(_careProviderBatchGroupingName)[0];

            #endregion

            #region Provider

            var provider1Name = "Provider " + currentTimeString;
            var provider2Name = "Parent Provider " + currentTimeString;


            var providerType1 = 13; //Residential Establishment
            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _teamId, providerType1, false);

            var providerType2 = 10; //Local Authority
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _teamId, providerType2, false);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service

            var _careProviderServiceName = "Care Provider Service Record";
            var _careProviderServiceId = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName, new DateTime(2020, 1, 1), 7895, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var careProviderServiceDetailName = "Care Provider Service Details Test 1";
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, code, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(_careProviderServiceId, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Mail Merge

            var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            #endregion

            #region Extract Name

            var ExtractNames = dbHelper.careProviderExtractName.GetAllCareProviderExtractName();
            int newExtractNameCode1 = ExtractNames.Count();

            while (dbHelper.careProviderExtractName.GetByCode(newExtractNameCode1).Any())
                newExtractNameCode1 = newExtractNameCode1 + 1;

            var _cpExtractNameId1 = commonMethodsDB.CreateCareProviderExtractName(_teamId, "CPEN_A1_" + currentTimeString, newExtractNameCode1, null, currentDate);

            #endregion

            #region Extract Type

            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type = Generic Extract File

            #endregion

            #region Finance Extract Batch Setup

            var _cpFinanceExtractBatchSetupId1 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, currentDate, new TimeSpan(2, 0, 0), null, _cpExtractNameId1, 1, _cpGenericExtractTypeId, _invoiceGenerateTemplate_MailMergeId);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateCareProviderFinanceExtractBatchSetupExtractOnDayFlagToYesByCurrentDayOfWeek(_cpFinanceExtractBatchSetupId1, currentDate);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateGenerateAndSendInvoicesAutomaticallyFlag(_cpFinanceExtractBatchSetupId1, true);

            #endregion

            #region Finance Extract Batch

            var _cpFinanceExtractBatchId = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1);
            Assert.AreEqual(1, _cpFinanceExtractBatchId.Count);
            var _cpFinanceExtractBatch1Id = _cpFinanceExtractBatchId[0];

            var financeExtractBatchFields = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch1Id, "batchid", "name");

            var _cpFinanceExtractBatch1Number = (int)financeExtractBatchFields["batchid"];
            var _cpFinanceExtractBatch1Name = (string)financeExtractBatchFields["name"];

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "CS_" + currentTimeString;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultSystemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, provider1ID, provider2ID);

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultSystemUserId, _businessUnitId, "", provider1ID, provider2ID, careProviderContractSchemeId, _careProviderServiceId, careProviderServiceDetailId, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            dbHelper.careProviderContractService.UpdateCareProviderServiceDetailId(CareProviderContractServiceId, careProviderServiceDetailId);
            var CareProviderContractServiceTitle = (dbHelper.careProviderContractService.GetCareProviderContractServiceByID(CareProviderContractServiceId, "title")["title"]).ToString();

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(CareProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup
            var todayDate = currentDate;
            var invoicebyid = 2; //funder/service user
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 1; //Monday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(currentDate.Year, 12, 31);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractSchemeId, _careProviderBatchGroupingId,
                new DateTime(2023, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                _cpExtractNameId1, false,
                _teamId);

            #endregion

            #region Person

            var firstName = "Aaron";
            var lastName = currentTimeString;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Care Provider Person Contract

            var StartDate = new DateTime(2024, 5, 14);
            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultSystemUserId, provider1ID, careProviderContractSchemeId, provider2ID, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractSchemeId, _careProviderServiceId, CareProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batches

            var _cpFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId)[0];

            var runOn = commonMethodsHelper.GetCurrentDateWithoutCulture().AddSeconds(3);

            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId, runOn, new DateTime(2024, 5, 14), new DateTime(2024, 5, 21));

            Thread.Sleep(5000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            #endregion

            #region Finance Invoices

            var financeInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(_cpFinanceInvoiceBatchId);
            Assert.AreEqual(1, financeInvoices.Count);
            var financeInvoiceId = financeInvoices[0];

            //update invoice status to completed
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(financeInvoiceId, 2);

            #endregion

            #region Process CP Finance Extract Batches

            ProcessCPFinanceScheduledJob("Process CP Finance Extract Batches");

            #endregion

            financeExtractBatchFields = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch1Id, "extractcontent");
            var _cpFinanceExtractBatch1ContentId = (Guid)financeExtractBatchFields["extractcontent"];

            var documentFileFields = dbHelper.documentFile.GetByID(_cpFinanceExtractBatch1ContentId, "name", "filesize");
            var _cpFinanceExtractBatch1ContentName = (string)documentFileFields["name"];
            var _cpFinanceExtractBatch1ContentSize = (int)documentFileFields["filesize"];

            /**Authenticate against the API**/
            webAPIHelper.AdvanceFinancials.Authenticate();

            /**Call the api method to get all pending finance extracts**/
            var financeData = webAPIHelper.AdvanceFinancials.PendingFinanceExtracts();

            /**We should have 1 match for the finance extract batch generated by this test**/
            var financeExtractBatches = financeData.data.Where(c => c.batchid == _cpFinanceExtractBatch1Number).ToList();
            Assert.AreEqual(1, financeExtractBatches.Count);

            /**Assert the content of the signed URL**/
            var signedURL = webAPIHelper.AdvanceFinancials.GetSignedURL(financeExtractBatches[0].extractcontent.id.ToString());
            StringAssert.Contains(signedURL, "https://");
            StringAssert.Contains(signedURL, "/careproviderfinanceextractbatch/" + financeExtractBatches[0].extractcontent.id.ToString());
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "ACC-8890")]
        [Description("Test for the Download File From S3 api method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        public void API_TestMethod005()
        {
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region VAT Code
            dbHelper = new Phoenix.DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _careProviderVATCodeName = "Standard Rated";
            Guid _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName(_careProviderVATCodeName)[0];

            #endregion

            #region Care Provider Rate Unit

            string _careProviderRateUnitName = "Per Week Pro Rata";
            Guid _careProviderRateUnitId = dbHelper.careProviderRateUnit.GetByName(_careProviderRateUnitName)[0];

            #endregion

            #region Care Provider Batch Grouping

            string _careProviderBatchGroupingName = "Monthly";
            Guid _careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(_careProviderBatchGroupingName)[0];

            #endregion

            #region Provider

            var provider1Name = "Provider " + currentTimeString;
            var provider2Name = "Parent Provider " + currentTimeString;


            var providerType1 = 13; //Residential Establishment
            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _teamId, providerType1, false);

            var providerType2 = 10; //Local Authority
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _teamId, providerType2, false);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service

            var _careProviderServiceName = "Care Provider Service Record";
            var _careProviderServiceId = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName, new DateTime(2020, 1, 1), 7895, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var careProviderServiceDetailName = "Care Provider Service Details Test 1";
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, code, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(_careProviderServiceId, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Mail Merge

            var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            #endregion

            #region Extract Name

            var ExtractNames = dbHelper.careProviderExtractName.GetAllCareProviderExtractName();
            int newExtractNameCode1 = ExtractNames.Count();

            while (dbHelper.careProviderExtractName.GetByCode(newExtractNameCode1).Any())
                newExtractNameCode1 = newExtractNameCode1 + 1;

            var _cpExtractNameId1 = commonMethodsDB.CreateCareProviderExtractName(_teamId, "CPEN_A1_" + currentTimeString, newExtractNameCode1, null, currentDate);

            #endregion

            #region Extract Type

            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type = Generic Extract File

            #endregion

            #region Finance Extract Batch Setup

            var _cpFinanceExtractBatchSetupId1 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, currentDate, new TimeSpan(2, 0, 0), null, _cpExtractNameId1, 1, _cpGenericExtractTypeId, _invoiceGenerateTemplate_MailMergeId);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateCareProviderFinanceExtractBatchSetupExtractOnDayFlagToYesByCurrentDayOfWeek(_cpFinanceExtractBatchSetupId1, currentDate);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateGenerateAndSendInvoicesAutomaticallyFlag(_cpFinanceExtractBatchSetupId1, true);

            #endregion

            #region Finance Extract Batch

            var _cpFinanceExtractBatchId = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1);
            Assert.AreEqual(1, _cpFinanceExtractBatchId.Count);
            var _cpFinanceExtractBatch1Id = _cpFinanceExtractBatchId[0];

            var financeExtractBatchFields = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch1Id, "batchid", "name");

            var _cpFinanceExtractBatch1Number = (int)financeExtractBatchFields["batchid"];
            var _cpFinanceExtractBatch1Name = (string)financeExtractBatchFields["name"];

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "CS_" + currentTimeString;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultSystemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, provider1ID, provider2ID);

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultSystemUserId, _businessUnitId, "", provider1ID, provider2ID, careProviderContractSchemeId, _careProviderServiceId, careProviderServiceDetailId, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            dbHelper.careProviderContractService.UpdateCareProviderServiceDetailId(CareProviderContractServiceId, careProviderServiceDetailId);
            var CareProviderContractServiceTitle = (dbHelper.careProviderContractService.GetCareProviderContractServiceByID(CareProviderContractServiceId, "title")["title"]).ToString();

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(CareProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup
            var todayDate = currentDate;
            var invoicebyid = 2; //funder/service user
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 1; //Monday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(currentDate.Year, 12, 31);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractSchemeId, _careProviderBatchGroupingId,
                new DateTime(2023, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                _cpExtractNameId1, false,
                _teamId);

            #endregion

            #region Person

            var firstName = "Aaron";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Care Provider Person Contract

            var StartDate = new DateTime(2024, 5, 14);
            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultSystemUserId, provider1ID, careProviderContractSchemeId, provider2ID, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractSchemeId, _careProviderServiceId, CareProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batches

            var _cpFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId)[0];

            var runOn = commonMethodsHelper.GetCurrentDateWithoutCulture().AddSeconds(3);

            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId, runOn, new DateTime(2024, 5, 14), new DateTime(2024, 5, 21));

            Thread.Sleep(5000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            #endregion

            #region Finance Invoices

            var financeInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(_cpFinanceInvoiceBatchId);
            Assert.AreEqual(1, financeInvoices.Count);
            var financeInvoiceId = financeInvoices[0];

            //update invoice status to completed
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(financeInvoiceId, 2);

            #endregion

            #region Process CP Finance Extract Batches

            ProcessCPFinanceScheduledJob("Process CP Finance Extract Batches");

            #endregion

            financeExtractBatchFields = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch1Id, "extractcontent");
            var _cpFinanceExtractBatch1ContentId = (Guid)financeExtractBatchFields["extractcontent"];

            var documentFileFields = dbHelper.documentFile.GetByID(_cpFinanceExtractBatch1ContentId, "name", "filesize", "filelink");
            var _cpFinanceExtractBatch1ContentName = (string)documentFileFields["name"];
            var _cpFinanceExtractBatch1ContentSize = (int)documentFileFields["filesize"];

            /**Authenticate against the API**/
            webAPIHelper.AdvanceFinancials.Authenticate();

            /**Call the api method to get all pending finance extracts**/
            var financeData = webAPIHelper.AdvanceFinancials.PendingFinanceExtracts();

            /**We should have 1 match for the finance extract batch generated by this test**/
            var financeExtractBatches = financeData.data.Where(c => c.batchid == _cpFinanceExtractBatch1Number).ToList();
            Assert.AreEqual(1, financeExtractBatches.Count);

            /**Get the signed URL**/
            var signedURL = webAPIHelper.AdvanceFinancials.GetSignedURL(financeExtractBatches[0].extractcontent.id.ToString());

            /**Call the download method**/
            var fileContent = webAPIHelper.AdvanceFinancials.DownloadFileFromS3(signedURL);
            StringAssert.Contains(fileContent, provider1Name);
            StringAssert.Contains(fileContent, contractSchemeName);
            StringAssert.Contains(fileContent, provider2Name);
            StringAssert.Contains(fileContent, personFullName);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "ACC-8891")]
        [Description("Test for the Set Extract As Downloaded api method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        public void API_TestMethod006()
        {
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region VAT Code
            dbHelper = new Phoenix.DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _careProviderVATCodeName = "Standard Rated";
            Guid _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName(_careProviderVATCodeName)[0];

            #endregion

            #region Care Provider Rate Unit

            string _careProviderRateUnitName = "Per Week Pro Rata";
            Guid _careProviderRateUnitId = dbHelper.careProviderRateUnit.GetByName(_careProviderRateUnitName)[0];

            #endregion

            #region Care Provider Batch Grouping

            string _careProviderBatchGroupingName = "Monthly";
            Guid _careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(_careProviderBatchGroupingName)[0];

            #endregion

            #region Provider

            var provider1Name = "Provider " + currentTimeString;
            var provider2Name = "Parent Provider " + currentTimeString;


            var providerType1 = 13; //Residential Establishment
            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _teamId, providerType1, false);

            var providerType2 = 10; //Local Authority
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _teamId, providerType2, false);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service

            var _careProviderServiceName = "Care Provider Service Record";
            var _careProviderServiceId = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName, new DateTime(2020, 1, 1), 7895, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var careProviderServiceDetailName = "Care Provider Service Details Test 1";
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, code, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(_careProviderServiceId, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Mail Merge

            var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            #endregion

            #region Extract Name

            var ExtractNames = dbHelper.careProviderExtractName.GetAllCareProviderExtractName();
            int newExtractNameCode1 = ExtractNames.Count();

            while (dbHelper.careProviderExtractName.GetByCode(newExtractNameCode1).Any())
                newExtractNameCode1 = newExtractNameCode1 + 1;

            var _cpExtractNameId1 = commonMethodsDB.CreateCareProviderExtractName(_teamId, "CPEN_A1_" + currentTimeString, newExtractNameCode1, null, currentDate);

            #endregion

            #region Extract Type

            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type = Generic Extract File

            #endregion

            #region Finance Extract Batch Setup

            var _cpFinanceExtractBatchSetupId1 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, currentDate, new TimeSpan(2, 0, 0), null, _cpExtractNameId1, 1, _cpGenericExtractTypeId, _invoiceGenerateTemplate_MailMergeId);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateCareProviderFinanceExtractBatchSetupExtractOnDayFlagToYesByCurrentDayOfWeek(_cpFinanceExtractBatchSetupId1, currentDate);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateGenerateAndSendInvoicesAutomaticallyFlag(_cpFinanceExtractBatchSetupId1, true);

            #endregion

            #region Finance Extract Batch

            var _cpFinanceExtractBatchId = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1);
            Assert.AreEqual(1, _cpFinanceExtractBatchId.Count);
            var _cpFinanceExtractBatch1Id = _cpFinanceExtractBatchId[0];

            var financeExtractBatchFields = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch1Id, "batchid", "name");

            var _cpFinanceExtractBatch1Number = (int)financeExtractBatchFields["batchid"];
            var _cpFinanceExtractBatch1Name = (string)financeExtractBatchFields["name"];

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "CS_" + currentTimeString;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultSystemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, provider1ID, provider2ID);

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultSystemUserId, _businessUnitId, "", provider1ID, provider2ID, careProviderContractSchemeId, _careProviderServiceId, careProviderServiceDetailId, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            dbHelper.careProviderContractService.UpdateCareProviderServiceDetailId(CareProviderContractServiceId, careProviderServiceDetailId);
            var CareProviderContractServiceTitle = (dbHelper.careProviderContractService.GetCareProviderContractServiceByID(CareProviderContractServiceId, "title")["title"]).ToString();

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(CareProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup
            var todayDate = currentDate;
            var invoicebyid = 2; //funder/service user
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 1; //Monday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(currentDate.Year, 12, 31);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractSchemeId, _careProviderBatchGroupingId,
                new DateTime(2023, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                _cpExtractNameId1, false,
                _teamId);

            #endregion

            #region Person

            var firstName = "Aaron";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + lastName;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Care Provider Person Contract

            var StartDate = new DateTime(2024, 5, 14);
            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultSystemUserId, provider1ID, careProviderContractSchemeId, provider2ID, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractSchemeId, _careProviderServiceId, CareProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batches

            var _cpFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId)[0];

            var runOn = commonMethodsHelper.GetCurrentDateWithoutCulture().AddSeconds(3);

            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId, runOn, new DateTime(2024, 5, 14), new DateTime(2024, 5, 21));

            Thread.Sleep(5000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            #endregion

            #region Finance Invoices

            var financeInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(_cpFinanceInvoiceBatchId);
            Assert.AreEqual(1, financeInvoices.Count);
            var financeInvoiceId = financeInvoices[0];

            //update invoice status to completed
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(financeInvoiceId, 2);

            #endregion

            #region Process CP Finance Extract Batches

            ProcessCPFinanceScheduledJob("Process CP Finance Extract Batches");

            #endregion


            /**Authenticate against the API**/
            webAPIHelper.AdvanceFinancials.Authenticate();

            /**Call the api method to get all pending finance extracts**/
            var financeData = webAPIHelper.AdvanceFinancials.PendingFinanceExtracts();

            /**We should have 1 match for the finance extract batch generated by this test**/
            var financeExtractBatches = financeData.data.Where(c => c.batchid == _cpFinanceExtractBatch1Number && c.id == _cpFinanceExtractBatch1Id).ToList();
            Assert.AreEqual(1, financeExtractBatches.Count);

            /**Set the extract as downloaded**/
            webAPIHelper.AdvanceFinancials.SetExtractAsDownloaded(_cpFinanceExtractBatch1Id.ToString());

            /**Call the api method to get all pending finance extracts again**/
            financeData = webAPIHelper.AdvanceFinancials.PendingFinanceExtracts();

            /**We should have 0 match for the finance extract batch generated by this test because it was already marked as downloaded**/
            financeExtractBatches = financeData.data.Where(c => c.batchid == _cpFinanceExtractBatch1Number && c.id == _cpFinanceExtractBatch1Id).ToList();
            Assert.AreEqual(0, financeExtractBatches.Count);


        }

        [TestMethod]
        [TestProperty("JiraIssueID", "ACC-8892")]
        [Description("Test for the Get Signed URL api method (Supply invalid Id to the api)")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        public void API_TestMethod007()
        {
            /**Authenticate against the API**/
            webAPIHelper.AdvanceFinancials.Authenticate();

            /**Call the API with an invalid ID**/
            try
            {
                webAPIHelper.AdvanceFinancials.GetSignedURL(Guid.NewGuid().ToString());
            }
            catch (Exception ex)
            {
                Assert.AreEqual("NotFound - {\"errorMessage\":\"Unable to find the file.\",\"errorCode\":\"RecordNotFound\"}", ex.Message);
                return;
            }

            Assert.Fail("No Exception was thrown by the API.");

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "ACC-8893")]
        [Description("Test for the Set Extract As Downloaded api method (Supply invalid Id to the api)")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        public void API_TestMethod008()
        {
            /**Authenticate against the API**/
            webAPIHelper.AdvanceFinancials.Authenticate();

            var fakeRecordId = Guid.NewGuid().ToString();

            /**Call the API with an invalid ID**/
            try
            {
                webAPIHelper.AdvanceFinancials.SetExtractAsDownloaded(fakeRecordId);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("NotFound - {\"errorMessage\":\"The record you are attempting to access cannot be displayed because it is either deleted or your security privileges prevent access. Id " + fakeRecordId + " and Type CareProviderFinanceExtractBatch.\",\"errorCode\":\"RecordNotFound\"}", ex.Message);
                return;
            }

            Assert.Fail("No Exception was thrown by the API.");

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "ACC-8894")]
        [Description("Test for the Download File From S3 api method (Supply invalid Id to the api)")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        public void API_TestMethod009()
        {
            /**Authenticate against the API**/
            webAPIHelper.AdvanceFinancials.Authenticate();

            /**Call the API with an invalid ID**/
            try
            {
                webAPIHelper.AdvanceFinancials.DownloadFileFromS3("https://advancedcarecloud-tenant-qa.s3.eu-west-2.amazonaws.com/automationcareprovidercombined_2d8f2316-a61d-ef11-bafb-f7c6ce32d503/careproviderfinanceextractbatch/000a0000-a00a-aa00-000a-0000000aaa0a?X-Amz-Expires=300&X-Amz-Security-Token=IQoJb3JpZ2luX2VjEHkaCWV1LXdlc3QtMiJHMEUCIDGujTunWUbxA6kKezxgV8Ip9TiSzmHJbpKrkx6So6%2F%2FAiEA6PcJd87P%2BsPuDugJs0ooakfg5pktYopNwMhOm0w3Xi8qlwQIQRAEGgwzNTYyNDMxMTA0MTgiDCI91L7qd3vUh7DamCr0A6PZtlT%2FY8bkDwzzXOoFd%2FrRLiqErl4XR02KEHj%2BW1GWRmouAma9aWkMQRcEO2r%2BLsMvyEJHtjqYzdoUY9VTj%2FYgPjN1%2BWNRUZCLY9UOKn0Y3VenkVIWVBMhtUKxRl4XtF3IsDGyKHmsUpS2MA3iMA2X%2FpmFHTnVu4hg5Qn0i%2BJSHfnWCQCle%2B4z1T86us4uMM0j4rHVND9QdPaPMGxRs%2B%2BBlG8fd3RR4gN0v1q23FlqdkvvGd4cxdaOFm0YYGZ1KFSKk%2BfVNPHWq3ZAdT85u4I0N6HvmDAkQxcDfPZMQMB7u3YPwEs%2BCkUwNmxUEzhRhtVIJ4uhSAVLY8uJHf2C4lUnz%2B%2B3B%2F2HEl7vjTf80hJHw2pzSQ3wIRLy%2F1EC9cyYgs8tYoWZSKYwpYjgTOJRNcOQAQ0WbXAY0yOIIjHlUXDdkfmVPlHJDD7%2BOe%2FxE2ZtAqk%2BldgNgn4kTnyOIXjx1CvXL%2BEMo%2BdnQfSRWqzfqtGt%2BM5TvoWWlMc5FauHMVobY6kM%2B5SJax%2BURNpb7OBC9015keviT2PjWTG9v2SnvAFZHvDyjYYkxbuWyCWhwVshNb1MNPZd0feXJ01GGxFd9ERTzjBebDmLQHvyT%2Fo35e2XJDxABGQWUSZLHFhTzuyF%2FnTLwCldfDjkwZ6EANOtykNdR299MKyHubQGOqYBZ4UiKIwIaf8ZdT%2FiUncLZOD9yIh5bjRhF5U4bGdXJ%2FHrCDpk9volpdhV%2Fe6V5%2BJ89yP%2Bnw9bzLqViWREMUNzJXZqrIjSQYKSpwyvFrwqsRb%2Bu3fHMvhs24CtoxJJkmb5aIdKNtL5K6%2FORfiMa7AeYqPhA2hNyXzxXtB2%2BFGpE8rUptAcuEnASQBnvDavsSyLmEO1qi5oasXIzC0OWZXhjb5JgUlm0A%3D%3D&X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=ASIAVF4N6PIJERA6VP5J%2F20240710%2Feu-west-2%2Fs3%2Faws4_request&X-Amz-Date=20240710T083459Z&X-Amz-SignedHeaders=host&X-Amz-Signature=e615ada5859e6018e73b09395d1983598859394d971ad90ce5b75a6aaf1d8ab0");
            }
            catch (Exception ex)
            {
                StringAssert.Contains(ex.Message, "Forbidden - <?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<Error><Code>AccessDenied</Code><Message>Request has expired</Message><X-Amz-Expires>300");
                return;
            }

            Assert.Fail("No Exception was thrown by the API.");

        }


    }
}

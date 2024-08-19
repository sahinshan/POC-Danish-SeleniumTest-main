using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Finance.FinanceAdmin
{
    [TestClass]
    public class FinanceTransactions_UITestCases : FunctionalTest
    {
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private string _systemUsername;
        private string _defaultUserFullname;
        private Guid _paymentTypeCodeId;
        private string _paymentTypeCodeName;
        private Guid _providerBatchGroupingId;
        private Guid _vatCodeId;
        private string _providerBatchGroupingName;
        private Guid _invoiceById;
        private string _invoiceByName;
        private Guid _invoiceFrequencyId;
        private string _invoiceFrequencyName;
        private Guid _extractNameId;
        private string _extractName;
        private string currentDate = DateTime.Now.ToString("yyyyMMddHHmmss");
        private string _firstName;
        private string _lastName;
        private string _personFullname;
        private Guid _personId;
        private string _personNumber;
        private Guid Draft_Serviceprovisionstatusid;
        private Guid ReadyForAuthorisation_Serviceprovisionstatusid;
        private Guid Authorised_Serviceprovisionstatusid;
        private Guid Cancelled_Serviceprovisionstatusid;
        private Guid _serviceprovisionstartreasonid;
        private Guid _placementRoomTypeId;
        private Guid _financeInvoiceStatusId_newlyCreated;
        private Guid _financeInvoiceStatusId_beingActioned;
        private Guid _financeInvoiceStatusId_held;
        private Guid _financeInvoiceStatusId_completed;
        private Guid _financeInvoiceStatusId_errorUponAuthorisation;
        private Guid _financeInvoiceStatusId_readyForAuthorisation;
        private Guid _financeInvoiceStatusId_authorised;
        private Guid _financeInvoiceStatusId_extracted;
        private Guid _serviceProvisionId;
        private Guid _serviceProvisionId2;
        private string _providerName;
        private Guid _providerId;
        private Guid _serviceProvidedId;
        private Guid ratePeriodId1;
        private Guid FinanceInvoiceBatchSetupId;
        private Guid FinanceInvoiceBatchSetupId_Person;
        private string _serviceProvisionTitle;
        private string _serviceProvisionTitle2;
        private Guid _serviceElement1Id;
        private Guid _serviceElement1Id_Person;
        private string serviceElement1IdName_Person;
        private Guid _serviceElement2Id;
        private string _serviceElement1IdName;
        private string _serviceElement2IdName;
        private Guid _serviceElement2Id_Person;
        private string serviceElement2IdName_Person;
        private DateTime plannedStartDate1;
        private Guid rateunitid;
        private List<Guid> validRateUnits;
        private Guid _glCodeLocationId;
        private Guid _glCodeId;
        private Guid glCode_Level1Id;
        private Guid glCode_Level1Id2;

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

                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);
                _defaultUserFullname = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(defaultSystemUserId, "fullname")["fullname"];

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("Finance Invoice T2");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("Finance Invoice T2", null, _businessUnitId, "957767", "FinanceInvoice2@careworkstempmail.com", "FinanceInvoice2", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region System User FinanceInvoiceUser

                _systemUsername = "FinanceInvoiceUser3";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "FinanceInvoice", "User3", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

                #region Finance General Settings

                commonMethodsDB.CreateFinanceGeneralSettingsIfNeeded(_teamId, _businessUnitId, 2, true, new DateTime(DateTime.Now.AddYears(1).Year, 3, 31));

                #endregion

                #region Payment Type Code

                _paymentTypeCodeName = "Invoice";
                _paymentTypeCodeId = commonMethodsDB.CreatePaymentTypeCode(_paymentTypeCodeName, new DateTime(2023, 1, 1), _teamId);

                #endregion

                #region Provider Batch Grouping

                _providerBatchGroupingName = "Default Batching";
                _providerBatchGroupingId = commonMethodsDB.CreateProviderBatchGrouping(_providerBatchGroupingName, new DateTime(2023, 1, 1), _teamId);

                #endregion

                #region VAT Code

                _vatCodeId = dbHelper.vatCode.GetByName("Standard Rated")[0];

                #endregion

                #region Invoice By

                _invoiceByName = "Provider\\Client";
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

                #region Finance Invoice Status
                _financeInvoiceStatusId_newlyCreated = dbHelper.financeInvoiceStatus.GetByName("Newly Created")[0];
                _financeInvoiceStatusId_beingActioned = dbHelper.financeInvoiceStatus.GetByName("Being Actioned")[0];
                _financeInvoiceStatusId_held = dbHelper.financeInvoiceStatus.GetByName("Held")[0];
                _financeInvoiceStatusId_completed = dbHelper.financeInvoiceStatus.GetByName("Completed")[0];
                _financeInvoiceStatusId_errorUponAuthorisation = dbHelper.financeInvoiceStatus.GetByName("Error upon Authorisation")[0];
                _financeInvoiceStatusId_readyForAuthorisation = dbHelper.financeInvoiceStatus.GetByName("Ready for Authorisation")[0];
                _financeInvoiceStatusId_authorised = dbHelper.financeInvoiceStatus.GetByName("Authorised")[0];
                _financeInvoiceStatusId_extracted = dbHelper.financeInvoiceStatus.GetByName("Extracted")[0];

                #endregion

                #region Service Provision Status

                Draft_Serviceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Draft")[0];
                ReadyForAuthorisation_Serviceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Ready for Authorisation")[0];
                Authorised_Serviceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Authorised")[0];
                Cancelled_Serviceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Cancelled")[0];

                #endregion

                #region Service Provision Start Reason

                _serviceprovisionstartreasonid = commonMethodsDB.CreateServiceProvisionStartReason(_teamId, _businessUnitId, "Default", new DateTime(2020, 1, 1));

                #endregion

                #region Placement Room Type

                _placementRoomTypeId = dbHelper.placementRoomType.GetPlacementRoomTypeByName("Not Applicable")[0];

                #endregion

                #region Person

                _firstName = "John";
                _lastName = "LN_" + currentDate;
                _personFullname = _firstName + " " + _lastName;
                _personId = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId, new DateTime(2003, 1, 2));
                _personNumber = dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"].ToString();

                #endregion

                #region Rate Units

                rateunitid = dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)")[0];
                validRateUnits = new List<Guid>();
                validRateUnits.Add(rateunitid);
                validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)")[0]);

                #endregion

                #region Authorisation Level - Service Provision and Finance Invoice
                commonMethodsDB.CreateAuthorisationLevel(_teamId, _systemUserId, new DateTime(2021, 1, 1), 7, 999999m, true, true);
                commonMethodsDB.CreateAuthorisationLevel(_teamId, _systemUserId, new DateTime(2021, 1, 1), 4, 999999m, true, true);

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        private void SetupForServiceElementWhoToPayProvider()
        {
            #region GL Code Location

            _glCodeLocationId = dbHelper.glCodeLocation.GetByName("Service Element 1").FirstOrDefault();

            #endregion

            #region GL Code
            _glCodeId = commonMethodsDB.CreateGLCode(_teamId, _glCodeLocationId, "InvoiceGLCode", "9995", "9995");

            #endregion

            #region Service Element 1 and Service Element 2

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            _serviceElement1IdName = "SE3438_1_" + currentDate;
            _serviceElement1Id = commonMethodsDB.CreateServiceElement1(_serviceElement1IdName, _teamId, new DateTime(2021, 1, 1), code, 1, 2, validRateUnits, validRateUnits[0], _glCodeId);

            _serviceElement2IdName = "SE3438_2_" + currentDate;
            _serviceElement2Id = commonMethodsDB.CreateServiceElement2(_teamId, _serviceElement2IdName, new DateTime(2021, 1, 1), code);

            #endregion            

            #region Service Permissions
            dbHelper.servicePermission.CreateServicePermission(_teamId, _businessUnitId, _serviceElement1Id, _teamId);

            #endregion

            #region Service Mappings
            commonMethodsDB.CreateServiceMapping(_teamId, _serviceElement1Id, _serviceElement2Id);

            #endregion

            #region GL Code Mappings
            glCode_Level1Id = dbHelper.glCodeLocation.GetByName("Service Element 1")[0];
            dbHelper.glCodeMapping.CreateGLCodeMapping(_teamId, _serviceElement1Id, glCode_Level1Id);

            #endregion

            #region Service GL Coding

            dbHelper.serviceGLCoding.CreateServiceGLCoding(_teamId, false, true, _serviceElement1Id, _serviceElement2Id, null,
                null, _glCodeId, null, null);

            #endregion

            #region Finance Invoice Batch Setup
            FinanceInvoiceBatchSetupId = dbHelper.financeInvoiceBatchSetup.CreateFinanceInvoiceBatchSetup(_teamId, _businessUnitId, "", _extractNameId, 2,
                _invoiceById, _invoiceFrequencyId, _paymentTypeCodeId, _providerBatchGroupingId, 7, _serviceElement1Id, new DateTime(2023, 1, 1), true, true, true, 1);

            #endregion

            #region Provider, Service Provided
            _providerName = "SE3438_" + currentDate;
            _providerId = commonMethodsDB.CreateProvider("SE3438_" + currentDate, _teamId, 2);
            dbHelper.provider.UpdateCreditorNumberField(_providerId, "Prv3438");
            _serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_teamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, null, null, null, 2);

            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_teamId, _serviceProvidedId, _paymentTypeCodeId, _vatCodeId, _providerBatchGroupingId, new DateTime(2019, 1, 1), null, 0);

            ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, _serviceProvidedId, validRateUnits[0], new DateTime(2020, 1, 1), null, 1);

            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, ratePeriodId1, _serviceProvidedId, 50m, 50m, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), true, true, true,
                true, true, true, true, true);

            dbHelper.serviceProvidedRatePeriod.UpdateApprovalStatus(ratePeriodId1, 2);
            dbHelper.serviceProvided.UpdateStatus(_serviceProvidedId, 2);

            #endregion

            #region Service Provision
            plannedStartDate1 = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-28);
            _serviceProvisionId = dbHelper.serviceProvision.CreateServiceProvision(_teamId, _systemUserId, _personId, Draft_Serviceprovisionstatusid,
                _serviceElement1Id, _serviceElement2Id, null, null, validRateUnits[0], _serviceprovisionstartreasonid, null,
                _teamId, _serviceProvidedId, _providerId, _systemUserId, _placementRoomTypeId, plannedStartDate1, null, null, null, null);
            _serviceProvisionTitle = (string)dbHelper.serviceProvision.GetByID(_serviceProvisionId, "name")["name"];

            #endregion

            #region Service Delivery

            dbHelper.serviceDelivery.CreateServiceDelivery(_teamId, _personId, _serviceProvisionId, validRateUnits[0], 1, 1, true, true, true, true, true, true, true, true, "");

            #endregion

            #region Authorise Service Provision
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId, ReadyForAuthorisation_Serviceprovisionstatusid);
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId, Authorised_Serviceprovisionstatusid);

            #endregion
        }

        private void SetupForServiceElementWhoToPayProviderAndServiceDelivery(int numberOfCarers)
        {
            #region GL Code Location

            _glCodeLocationId = dbHelper.glCodeLocation.GetByName("Service Element 1").FirstOrDefault();

            #endregion

            #region GL Code
            _glCodeId = commonMethodsDB.CreateGLCode(_teamId, _glCodeLocationId, "InvoiceGLCode", "9995", "9995");

            #endregion

            #region Service Element 1 and Service Element 2

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            _serviceElement1IdName = "SE3438_1_" + currentDate;
            _serviceElement1Id = commonMethodsDB.CreateServiceElement1(_serviceElement1IdName, _teamId, new DateTime(2021, 1, 1), code, 1, 2, validRateUnits, validRateUnits[0], _glCodeId);

            _serviceElement2IdName = "SE3438_2_" + currentDate;
            _serviceElement2Id = commonMethodsDB.CreateServiceElement2(_teamId, _serviceElement2IdName, new DateTime(2021, 1, 1), code);

            #endregion

            #region Service Permissions
            dbHelper.servicePermission.CreateServicePermission(_teamId, _businessUnitId, _serviceElement1Id, _teamId);

            #endregion

            #region Service Mappings
            commonMethodsDB.CreateServiceMapping(_teamId, _serviceElement1Id, _serviceElement2Id);

            #endregion

            #region GL Code Mappings
            glCode_Level1Id = dbHelper.glCodeLocation.GetByName("Service Element 1")[0];
            dbHelper.glCodeMapping.CreateGLCodeMapping(_teamId, _serviceElement1Id, glCode_Level1Id);

            #endregion

            #region Service GL Coding

            dbHelper.serviceGLCoding.CreateServiceGLCoding(_teamId, false, true, _serviceElement1Id, _serviceElement2Id, null,
                null, _glCodeId, null, null);

            #endregion

            #region Finance Invoice Batch Setup
            FinanceInvoiceBatchSetupId = dbHelper.financeInvoiceBatchSetup.CreateFinanceInvoiceBatchSetup(_teamId, _businessUnitId, "", _extractNameId, 2,
                _invoiceById, _invoiceFrequencyId, _paymentTypeCodeId, _providerBatchGroupingId, 7, _serviceElement1Id, new DateTime(2023, 1, 1), true, true, true, 1);

            #endregion

            #region Provider, Service Provided
            _providerName = "SE3438_" + currentDate;
            _providerId = commonMethodsDB.CreateProvider("SE3438_" + currentDate, _teamId, 2);
            dbHelper.provider.UpdateCreditorNumberField(_providerId, "Prv3438");
            _serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_teamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, null, null, null, 2);

            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_teamId, _serviceProvidedId, _paymentTypeCodeId, _vatCodeId, _providerBatchGroupingId, new DateTime(2019, 1, 1), null, 0);

            ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, _serviceProvidedId, validRateUnits[0], new DateTime(2020, 1, 1), null, 1);

            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, ratePeriodId1, _serviceProvidedId, 50m, 50m, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), true, true, true,
                true, true, true, true, true);

            dbHelper.serviceProvidedRatePeriod.UpdateApprovalStatus(ratePeriodId1, 2);
            dbHelper.serviceProvided.UpdateStatus(_serviceProvidedId, 2);

            #endregion

            #region Service Provision
            plannedStartDate1 = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-28);
            _serviceProvisionId = dbHelper.serviceProvision.CreateServiceProvision(_teamId, _systemUserId, _personId, Draft_Serviceprovisionstatusid,
                _serviceElement1Id, _serviceElement2Id, null, null, validRateUnits[0], _serviceprovisionstartreasonid, null,
                _teamId, _serviceProvidedId, _providerId, _systemUserId, _placementRoomTypeId, plannedStartDate1, null, null, null, null);
            _serviceProvisionTitle = (string)dbHelper.serviceProvision.GetByID(_serviceProvisionId, "name")["name"];

            #endregion

            #region Service Delivery

            dbHelper.serviceDelivery.CreateServiceDelivery(_teamId, _personId, _serviceProvisionId, validRateUnits[0], 1, numberOfCarers, true, true, true, true, true, true, true, true, "");

            #endregion

            #region Authorise Service Provision
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId, ReadyForAuthorisation_Serviceprovisionstatusid);
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId, Authorised_Serviceprovisionstatusid);

            #endregion
        }

        private void ExpandAndProcessFinanceTransactionTriggersScheduledJob()
        {
            #region Execute 'Expand and Process Finance Transaction Triggers' scheduled job
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
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-2325

        [TestProperty("JiraIssueID", "CDV6-25699")]
        [Description("Test automation for Step 1 to 9 from CDV6-3438")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void FinanceTransactions_CDV6_3438_UITestMethod001()
        {
            #region step 1 to 9
            dbHelper = new Phoenix.DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);
            SetupForServiceElementWhoToPayProvider();
            ExpandAndProcessFinanceTransactionTriggersScheduledJob();

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
                .InsertSearchQuery("*" + _serviceElement1IdName + "*")
                .TapSearchButton()
                .WaitForFinanceInvoiceBatchSetupPageToLoad()
                .OpenRecord(FinanceInvoiceBatchSetupId.ToString());

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .NavigateToFinanceInvoicesBatchesPage();

            var invoiceBatchId = dbHelper.financeInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(FinanceInvoiceBatchSetupId)[0];
            dbHelper.financeInvoiceBatch.UpdateFinanceInvoiceBatchDates(invoiceBatchId, DateTime.Now, DateTime.Now);

            #region Execute 'Process Invoice Batches' scheduled job
            //Get the schedule job id
            Guid processFinanceBatchesJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Process Invoice Batches")[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(processFinanceBatchesJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(processFinanceBatchesJobId);

            #endregion

            financeInvoiceBatchesPage
                .WaitForFinanceInvoiceBatchSetup_FinanceInvoiceBatchesPageToLoad()
                .OpenRecord(invoiceBatchId.ToString());

            financeInvoiceBatchRecordPage
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ValidateBatchstatusSelectedText("Completed")
                .ValidateFinanceModuleSelectedText("Supplier Payments")
                .ValidatePeriodEndDateText(DateTime.Now.ToString("dd'/'MM'/'yyyy"));

            var invoiceBatch_InvoiceId = dbHelper.financeInvoice.GetFinanceInvoiceByInvoiceBatchId(invoiceBatchId);
            Assert.AreEqual(1, invoiceBatch_InvoiceId.Count);

            var financeInvoiceId = dbHelper.financeInvoice.GetFinanceInvoiceById(invoiceBatch_InvoiceId[0], "invoicestatusid", "invoicenumber");
            Assert.AreEqual(_financeInvoiceStatusId_newlyCreated.ToString(), financeInvoiceId["invoicestatusid"].ToString());
            string _invoiceNumber = (string)financeInvoiceId["invoicenumber"];

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToServiceProvisionsSection();

            serviceProvisionsPage
                .WaitForServiceProvisionsPageToLoad()
                .SearchServiceProvisionRecord("*" + _serviceElement1IdName + "*", _serviceProvisionId.ToString())
                .OpenServiceProvisionRecord(_serviceProvisionId.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ClickFinanceTransactionsTab();

            var financeTransactions = dbHelper.financeTransaction.GetFinanceTransactionByFinanceInvoiceIdAndTransactionClassId(invoiceBatch_InvoiceId[0], 5);

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad(_serviceProvisionId.ToString())
                .ValidateRecordPresent(financeTransactions[0].ToString())
                .ValidateRecordCellText(financeTransactions[0].ToString(), 14, "Standard")
                .ValidateRecordCellText(financeTransactions[0].ToString(), 20, "No")
                .ValidateRecordCellText(financeTransactions[0].ToString(), 15, _invoiceNumber);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-2326

        [TestProperty("JiraIssueID", "CDV6-25707")]
        [Description("Test automation for Step 10 to 18 from CDV6-3438")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void FinanceTransactions_CDV6_3438_UITestMethod002()
        {
            #region step 10 to 18
            dbHelper = new Phoenix.DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);
            SetupForServiceElementWhoToPayProvider();

            ExpandAndProcessFinanceTransactionTriggersScheduledJob();

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
                .InsertSearchQuery("*" + _serviceElement1IdName + "*")
                .TapSearchButton()
                .WaitForFinanceInvoiceBatchSetupPageToLoad()
                .OpenRecord(FinanceInvoiceBatchSetupId.ToString());

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .NavigateToFinanceInvoicesBatchesPage();

            var invoiceBatchId = dbHelper.financeInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(FinanceInvoiceBatchSetupId)[0];
            dbHelper.financeInvoiceBatch.UpdateFinanceInvoiceBatchDates(invoiceBatchId, DateTime.Now, DateTime.Now);

            #region Execute 'Process Invoice Batches' scheduled job
            //Get the schedule job id
            Guid processFinanceBatchesJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Process Invoice Batches")[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(processFinanceBatchesJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(processFinanceBatchesJobId);

            #endregion

            financeInvoiceBatchesPage
                .WaitForFinanceInvoiceBatchSetup_FinanceInvoiceBatchesPageToLoad()
                .OpenRecord(invoiceBatchId.ToString());

            financeInvoiceBatchRecordPage
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ValidateBatchstatusSelectedText("Completed")
                .ValidateFinanceModuleSelectedText("Supplier Payments");

            financeInvoiceBatchRecordPage
                .ClickInvoicesTab();

            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad(invoiceBatchId.ToString());

            var invoiceBatch_InvoiceId = dbHelper.financeInvoice.GetFinanceInvoiceByInvoiceBatchId(invoiceBatchId);
            Assert.AreEqual(1, invoiceBatch_InvoiceId.Count);

            var financeInvoiceId = dbHelper.financeInvoice.GetFinanceInvoiceById(invoiceBatch_InvoiceId[0], "invoicestatusid", "invoicenumber");
            Assert.AreEqual(_financeInvoiceStatusId_newlyCreated.ToString(), financeInvoiceId["invoicestatusid"].ToString());
            string _invoiceNumber = (string)financeInvoiceId["invoicenumber"];

            dbHelper.financeInvoice.UpdateInvoiceReceivedDate(invoiceBatch_InvoiceId[0], DateTime.Now);
            dbHelper.financeInvoice.UpdateProviderInvoiceNumber(invoiceBatch_InvoiceId[0], "PIn3438");
            dbHelper.financeInvoice.SetValuesVerifiedField(invoiceBatch_InvoiceId[0], true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToServiceProvisionsSection();

            serviceProvisionsPage
                .WaitForServiceProvisionsPageToLoad()
                .SearchServiceProvisionRecord("*" + _serviceElement1IdName + "*", _serviceProvisionId.ToString())
                .OpenServiceProvisionRecord(_serviceProvisionId.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ClickFinanceTransactionsTab();

            var financeTransactions = dbHelper.financeTransaction.GetFinanceTransactionByFinanceInvoiceIdAndTransactionClassId(invoiceBatch_InvoiceId[0], 5);
            Assert.AreEqual(4, financeTransactions.Count);

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad(_serviceProvisionId.ToString())
                .ValidateRecordPresent(financeTransactions[0].ToString())
                .ValidateRecordCellText(financeTransactions[0].ToString(), 14, "Standard")
                .ValidateRecordCellText(financeTransactions[0].ToString(), 20, "No")
                .ValidateRecordCellText(financeTransactions[0].ToString(), 15, _invoiceNumber);

            dbHelper.financeInvoice.UpdateInvoiceStatus(invoiceBatch_InvoiceId[0], _financeInvoiceStatusId_readyForAuthorisation);
            dbHelper.serviceProvidedRatePeriod.UpdateEndDate(ratePeriodId1, plannedStartDate1.AddDays(13));

            var ratePeriodId2 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, _serviceProvidedId, validRateUnits[0], plannedStartDate1.AddDays(14), null, 1);
            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, ratePeriodId2, _serviceProvidedId, 0m, 0m, new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0), true, true, true,
                true, true, true, true, true);
            dbHelper.serviceProvidedRatePeriod.UpdateApprovalStatus(ratePeriodId2, 2);

            ExpandAndProcessFinanceTransactionTriggersScheduledJob();

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad(_serviceProvisionId.ToString())
                .ClickRefreshButton();

            var financeTransactions_Contra = dbHelper.financeTransaction.GetFinanceTransactionByServiceProvisionIdAndTransactionClassId(_serviceProvisionId, 3);
            Assert.AreEqual(2, financeTransactions_Contra.Count);

            //Finance Transaction that is part of invoice, where Class = Contra for existing transaction, and new transaction is generated.
            var financeTransactionsForTheDays = dbHelper.financeTransaction
                .GetFinanceTransactionByServiceProvisionIdAndStartDateAndEndDate(_serviceProvisionId
                , plannedStartDate1.AddDays(14), plannedStartDate1.AddDays(20));
            Assert.AreEqual(3, financeTransactionsForTheDays.Count);

            //Existing Finance Transaction part of the invoice, where Class = Standard
            var financeTransactionsForTheDays1 = dbHelper.financeTransaction.GetFinanceTransactionById(financeTransactionsForTheDays[2], "netamount", "grossamount", "transactionclassid");
            Assert.AreEqual("350.00", financeTransactionsForTheDays1["netamount"].ToString());
            Assert.AreEqual("350.00", financeTransactionsForTheDays1["grossamount"].ToString());
            Assert.AreEqual("5", financeTransactionsForTheDays1["transactionclassid"].ToString());

            //New Finance Transaction in which Amount = 0, and where Class = Standard
            var financeTransactionsForTheDays2 = dbHelper.financeTransaction.GetFinanceTransactionById(financeTransactionsForTheDays[1], "netamount", "grossamount", "transactionclassid");
            Assert.AreEqual("0.00", financeTransactionsForTheDays2["netamount"].ToString());
            Assert.AreEqual("0.00", financeTransactionsForTheDays2["grossamount"].ToString());
            Assert.AreEqual("5", financeTransactionsForTheDays2["transactionclassid"].ToString());

            //New Finance Transaction in which Amount = -350, and where Class = Contra
            var financeTransactionsForTheDays3 = dbHelper.financeTransaction.GetFinanceTransactionById(financeTransactionsForTheDays[0], "netamount", "grossamount", "transactionclassid");
            Assert.AreEqual("-350.00", financeTransactionsForTheDays3["netamount"].ToString());
            Assert.AreEqual("-350.00", financeTransactionsForTheDays3["grossamount"].ToString());
            Assert.AreEqual("3", financeTransactionsForTheDays3["transactionclassid"].ToString());

            dbHelper.serviceProvidedRatePeriod.UpdateEndDate(ratePeriodId2, plannedStartDate1.AddDays(20));

            var ratePeriodId3 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, _serviceProvidedId, validRateUnits[0], plannedStartDate1.AddDays(21), null, 1);
            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, ratePeriodId3, _serviceProvidedId, 50m, 50m, new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0), true, true, true,
                true, true, true, true, true);
            dbHelper.serviceProvidedRatePeriod.UpdateApprovalStatus(ratePeriodId3, 2);

            ExpandAndProcessFinanceTransactionTriggersScheduledJob();

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad(_serviceProvisionId.ToString())
                .ClickRefreshButton();

            financeTransactionsForTheDays = dbHelper.financeTransaction
                .GetFinanceTransactionByServiceProvisionIdAndStartDateAndEndDate(_serviceProvisionId
                , plannedStartDate1.AddDays(28), plannedStartDate1.AddDays(34));
            Assert.AreEqual(1, financeTransactionsForTheDays.Count);

            financeTransactionsForTheDays1 = dbHelper.financeTransaction.GetFinanceTransactionById(financeTransactionsForTheDays[0], "netamount", "grossamount", "transactionclassid");
            Assert.AreEqual("350.00", financeTransactionsForTheDays1["netamount"].ToString());
            Assert.AreEqual("350.00", financeTransactionsForTheDays1["grossamount"].ToString());
            Assert.AreEqual("5", financeTransactionsForTheDays1["transactionclassid"].ToString());

            //Finance Transaction after adding new rate period which is same as rate period 1, transaction is recalculated from 0 to 350
            financeTransactionsForTheDays = dbHelper.financeTransaction
                            .GetFinanceTransactionByServiceProvisionIdAndStartDateAndEndDate(_serviceProvisionId
                            , plannedStartDate1.AddDays(21), plannedStartDate1.AddDays(27));
            financeTransactionsForTheDays2 = dbHelper.financeTransaction.GetFinanceTransactionById(financeTransactionsForTheDays[0], "netamount", "grossamount", "transactionclassid");
            Assert.AreEqual("350.00", financeTransactionsForTheDays2["netamount"].ToString());
            Assert.AreEqual("350.00", financeTransactionsForTheDays2["grossamount"].ToString());
            Assert.AreEqual("5", financeTransactionsForTheDays2["transactionclassid"].ToString());

            #endregion
        }

        #endregion     

        #region https://advancedcsg.atlassian.net/browse/ACC-2327

        [TestProperty("JiraIssueID", "CDV6-25723")]
        [Description("Test automation for Step 19 to 21 from CDV6-3438")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void FinanceTransactions_CDV6_3438_UITestMethod003()
        {
            #region step 19 to 21
            dbHelper = new Phoenix.DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            SetupForServiceElementWhoToPayProviderAndServiceDelivery(1);

            #region Person

            var _firstName2 = "Leo";
            var _lastName2 = "LN_" + currentDate;
            var _personId2 = commonMethodsDB.CreatePersonRecord(_firstName2, _lastName2, _ethnicityId, _teamId, new DateTime(2003, 1, 2));

            #region Service Provision
            var plannedStartDate2 = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-28);
            var _serviceProvisionId2 = dbHelper.serviceProvision.CreateServiceProvision(_teamId, _systemUserId, _personId2, Draft_Serviceprovisionstatusid,
                _serviceElement1Id, _serviceElement2Id, null, null, validRateUnits[0], _serviceprovisionstartreasonid, null,
                _teamId, _serviceProvidedId, _providerId, _systemUserId, _placementRoomTypeId, plannedStartDate2, null, null, null, null);

            #endregion

            #region Service Delivery

            dbHelper.serviceDelivery.CreateServiceDelivery(_teamId, _personId, _serviceProvisionId2, validRateUnits[0], 1, 2, true, true, true, true, true, true, true, true, "");

            #endregion

            #region Authorise Service Provision
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId2, ReadyForAuthorisation_Serviceprovisionstatusid);
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId2, Authorised_Serviceprovisionstatusid);

            #endregion

            #endregion

            ExpandAndProcessFinanceTransactionTriggersScheduledJob();

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
                .InsertSearchQuery("*" + _serviceElement1IdName + "*")
                .TapSearchButton()
                .WaitForFinanceInvoiceBatchSetupPageToLoad()
                .OpenRecord(FinanceInvoiceBatchSetupId.ToString());

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .NavigateToFinanceInvoicesBatchesPage();

            var invoiceBatchId = dbHelper.financeInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(FinanceInvoiceBatchSetupId)[0];
            dbHelper.financeInvoiceBatch.UpdateFinanceInvoiceBatchDates(invoiceBatchId, DateTime.Now, DateTime.Now);

            #region Execute 'Process Invoice Batches' scheduled job
            //Get the schedule job id
            Guid processFinanceBatchesJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Process Invoice Batches")[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(processFinanceBatchesJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(processFinanceBatchesJobId);

            #endregion

            financeInvoiceBatchesPage
                .WaitForFinanceInvoiceBatchSetup_FinanceInvoiceBatchesPageToLoad()
                .OpenRecord(invoiceBatchId.ToString());

            financeInvoiceBatchRecordPage
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ClickInvoicesTab();

            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad(invoiceBatchId.ToString());

            var invoiceBatch_InvoiceId = dbHelper.financeInvoice.GetFinanceInvoiceByInvoiceBatchId(invoiceBatchId);
            Assert.AreEqual(2, invoiceBatch_InvoiceId.Count);

            Guid financeInvoiceId1 = invoiceBatch_InvoiceId[0];
            Guid financeInvoiceId2 = invoiceBatch_InvoiceId[1];
            var financeInvoiceId1Fields = dbHelper.financeInvoice.GetFinanceInvoiceById(financeInvoiceId1, "invoicestatusid", "invoicenumber");
            Assert.AreEqual(_financeInvoiceStatusId_newlyCreated.ToString(), financeInvoiceId1Fields["invoicestatusid"].ToString());
            string _invoiceNumber = (string)financeInvoiceId1Fields["invoicenumber"];

            dbHelper.financeInvoice.UpdateInvoiceReceivedDate(financeInvoiceId1, DateTime.Now);
            dbHelper.financeInvoice.UpdateProviderInvoiceNumber(financeInvoiceId1, "PIn3438");
            dbHelper.financeInvoice.SetValuesVerifiedField(financeInvoiceId1, true);
            dbHelper.financeInvoice.UpdateInvoiceStatus(financeInvoiceId1, _financeInvoiceStatusId_readyForAuthorisation);

            var financeInvoiceId2Fields = dbHelper.financeInvoice.GetFinanceInvoiceById(financeInvoiceId2, "invoicestatusid", "invoicenumber");
            Assert.AreEqual(_financeInvoiceStatusId_newlyCreated.ToString(), financeInvoiceId2Fields["invoicestatusid"].ToString());
            string _invoiceNumber2 = (string)financeInvoiceId2Fields["invoicenumber"];

            dbHelper.financeInvoice.UpdateInvoiceReceivedDate(financeInvoiceId2, DateTime.Now);
            dbHelper.financeInvoice.UpdateProviderInvoiceNumber(financeInvoiceId2, "PIn3438_2");
            dbHelper.financeInvoice.SetValuesVerifiedField(financeInvoiceId2, true);
            dbHelper.financeInvoice.UpdateInvoiceStatus(financeInvoiceId2, _financeInvoiceStatusId_readyForAuthorisation);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToServiceProvisionsSection();

            serviceProvisionsPage
                .WaitForServiceProvisionsPageToLoad()
                .SearchServiceProvisionRecord("*" + _serviceElement1IdName + "*", _serviceProvisionId.ToString())
                .OpenServiceProvisionRecord(_serviceProvisionId.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ClickFinanceTransactionsTab();


            var financeTransactionsForInvoice1 = dbHelper.financeTransaction.GetFinanceTransactionByFinanceInvoiceIdAndTransactionClassId(financeInvoiceId1, 5);
            Assert.AreEqual(4, financeTransactionsForInvoice1.Count);

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad(_serviceProvisionId.ToString())
                .ValidateRecordPresent(financeTransactionsForInvoice1[0].ToString())
                .ValidateRecordCellText(financeTransactionsForInvoice1[0].ToString(), 14, "Standard")
                .ValidateRecordCellText(financeTransactionsForInvoice1[0].ToString(), 20, "No")
                .ValidateRecordCellText(financeTransactionsForInvoice1[0].ToString(), 15, _invoiceNumber)
                .ValidateRecordCellText(financeTransactionsForInvoice1[0].ToString(), 8, "£350.00")
                .ValidateRecordCellText(financeTransactionsForInvoice1[0].ToString(), 10, "£350.00")
                .OpenFinanceTransactionsRecord(financeTransactionsForInvoice1[0].ToString());

            financeTransactionRecordPage
                .WaitForFinanceTransactionRecordPageToLoad()
                .ValidateNetAmountText("350.00")
                .ValidateGrossAmountText("350.00");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToServiceProvisionsSection();

            serviceProvisionsPage
                .WaitForServiceProvisionsPageToLoad()
                .SearchServiceProvisionRecord("*" + _serviceElement1IdName + "*", _serviceProvisionId2.ToString())
                .OpenServiceProvisionRecord(_serviceProvisionId2.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ClickFinanceTransactionsTab();

            var financeTransactionsForInvoice2 = dbHelper.financeTransaction.GetFinanceTransactionByFinanceInvoiceIdAndTransactionClassId(financeInvoiceId2, 5);
            Assert.AreEqual(4, financeTransactionsForInvoice2.Count);

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad(_serviceProvisionId2.ToString())
                .ValidateRecordPresent(financeTransactionsForInvoice2[0].ToString())
                .ValidateRecordCellText(financeTransactionsForInvoice2[0].ToString(), 14, "Standard")
                .ValidateRecordCellText(financeTransactionsForInvoice2[0].ToString(), 20, "No")
                .ValidateRecordCellText(financeTransactionsForInvoice2[0].ToString(), 15, _invoiceNumber2)
                .ValidateRecordCellText(financeTransactionsForInvoice2[0].ToString(), 8, "£700.00")
                .ValidateRecordCellText(financeTransactionsForInvoice2[0].ToString(), 10, "£700.00")
                .OpenFinanceTransactionsRecord(financeTransactionsForInvoice2[0].ToString());

            financeTransactionRecordPage
                .WaitForFinanceTransactionRecordPageToLoad()
                .ValidateNetAmountText("700.00")
                .ValidateGrossAmountText("700.00");

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-2328 and https://advancedcsg.atlassian.net/browse/ACC-2614 (Step 30 to 33)

        [TestProperty("JiraIssueID", "CDV6-25834")]
        [Description("Test automation for Step 22 to 33 from CDV6-3438")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void FinanceTransactions_CDV6_3438_UITestMethod004()
        {
            #region step 22 to 29
            dbHelper = new Phoenix.DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            #region GL Code Location

            var _glCodeLocationId_Block = dbHelper.glCodeLocation.GetByName("Block").FirstOrDefault();
            var _gloCodeLocationId_ProvSE1SE2CC = dbHelper.glCodeLocation.GetByName("Provider / Service Element 1 / Service Element 2 / Client Category").FirstOrDefault();

            #endregion

            #region GL Code
            var _glCodeId_Block = commonMethodsDB.CreateGLCode(_teamId, _glCodeLocationId_Block, "BLC", "EC1", "2328");
            var _glCodeId_ProvSE1SE2CC = commonMethodsDB.CreateGLCode(_teamId, _gloCodeLocationId_ProvSE1SE2CC, "PSSC1", "2328b", "2328b");


            #endregion

            #region Service Element 1 and Service Element 2

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            validRateUnits = new List<Guid>();
            var rateUnit_PerWeekProRata = dbHelper.rateUnit.GetByName("Per Week Pro Rata \\ Weekly")[0];
            validRateUnits.Add(rateUnit_PerWeekProRata);
            _serviceElement1IdName = "SE2328_1_" + currentDate;
            _serviceElement1Id = commonMethodsDB.CreateServiceElement1(_serviceElement1IdName, _teamId, new DateTime(2021, 1, 1), code, 1, 2, validRateUnits, rateUnit_PerWeekProRata, _glCodeId_Block);

            _serviceElement2IdName = "SE2328_2_" + currentDate;
            _serviceElement2Id = commonMethodsDB.CreateServiceElement2(_teamId, _serviceElement2IdName, new DateTime(2021, 1, 1), code);

            #endregion

            #region Service Permissions
            dbHelper.servicePermission.CreateServicePermission(_teamId, _businessUnitId, _serviceElement1Id, _teamId);

            #endregion

            #region Service Mappings
            commonMethodsDB.CreateServiceMapping(_teamId, _serviceElement1Id, _serviceElement2Id);

            #endregion

            #region GL Code Mappings
            glCode_Level1Id = dbHelper.glCodeLocation.GetByName("Service Element 1")[0];
            dbHelper.glCodeMapping.CreateGLCodeMapping(_teamId, _serviceElement1Id, glCode_Level1Id);

            #endregion

            #region Service GL Coding

            dbHelper.serviceGLCoding.CreateServiceGLCoding(_teamId, false, true, _serviceElement1Id, _serviceElement2Id, null,
                null, _glCodeId, null, null);

            #endregion

            #region Finance Invoice Batch Setup
            FinanceInvoiceBatchSetupId = dbHelper.financeInvoiceBatchSetup.CreateFinanceInvoiceBatchSetup(_teamId, _businessUnitId, "", _extractNameId, 2,
                _invoiceById, _invoiceFrequencyId, _paymentTypeCodeId, _providerBatchGroupingId, 7, _serviceElement1Id, new DateTime(2023, 1, 1), true, true, true, 1);

            #endregion

            #region Provider, Service Provided
            _providerName = "SE2328_" + currentDate;
            _providerId = commonMethodsDB.CreateProvider("SE2328_" + currentDate, _teamId, 2);
            dbHelper.provider.UpdateCreditorNumberField(_providerId, "Prv2328");
            _serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_teamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, null, null, null, _glCodeId_ProvSE1SE2CC, 2, false, 2, false);
            string serviceProvidedName = (string)dbHelper.serviceProvided.GetByID(_serviceProvidedId, "name")["name"];

            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_teamId, _serviceProvidedId, _paymentTypeCodeId, _vatCodeId, _providerBatchGroupingId, new DateTime(2019, 1, 1), null, 0);

            ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, _serviceProvidedId, validRateUnits[0], new DateTime(2020, 1, 1), null, "1", _glCodeId_Block);
            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, ratePeriodId1, _serviceProvidedId, 50m, null, null, null, null, null, null,
                null, null, null, null, null, 5m);

            dbHelper.serviceProvidedRatePeriod.UpdateApprovalStatus(ratePeriodId1, 2);
            dbHelper.serviceProvided.UpdateStatus(_serviceProvidedId, 2);

            ExpandAndProcessFinanceTransactionTriggersScheduledJob();
            #endregion            

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            var invoiceBatchId = dbHelper.financeInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(FinanceInvoiceBatchSetupId)[0];
            dbHelper.financeInvoiceBatch.UpdateFinanceInvoiceBatchDates(invoiceBatchId, DateTime.Now, DateTime.Now);

            #region Execute 'Process Invoice Batches' scheduled job
            //Get the schedule job id
            Guid processFinanceBatchesJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Process Invoice Batches")[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(processFinanceBatchesJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(processFinanceBatchesJobId);

            #endregion

            var invoiceBatch_InvoiceId = dbHelper.financeInvoice.GetFinanceInvoiceByInvoiceBatchId(invoiceBatchId);
            Assert.AreEqual(1, invoiceBatch_InvoiceId.Count);

            Guid financeInvoiceId1 = invoiceBatch_InvoiceId[0];

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(_providerName, _providerId.ToString())
                .OpenProviderRecord(_providerId.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .NavigateToFinanceTransactionsPage();

            var financeTransactionsForInvoice1 = dbHelper.financeTransaction.GetFinanceTransactionByFinanceInvoiceIdAndTransactionClassId(financeInvoiceId1, 5);
            Assert.IsTrue(financeTransactionsForInvoice1.Count > 0);

            var financeTransactions1Fields = dbHelper.financeTransaction.GetFinanceTransactionById(financeTransactionsForInvoice1[0], "startdate", "enddate");
            Assert.AreEqual(new DateTime(2023, 1, 1), financeTransactions1Fields["startdate"]);
            Assert.AreEqual(new DateTime(2023, 1, 1), financeTransactions1Fields["enddate"]);

            var financeTransactions2Fields = dbHelper.financeTransaction.GetFinanceTransactionById(financeTransactionsForInvoice1[financeTransactionsForInvoice1.Count - 1], "startdate", "enddate");
            Assert.AreEqual(commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-7), financeTransactions2Fields["startdate"]); //Previous week's Monday
            Assert.AreEqual(commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-1), financeTransactions2Fields["enddate"]); //Previous week's Sunda

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad(_providerId.ToString())
                .OpenFinanceTransactionsRecord(financeTransactionsForInvoice1[0].ToString());

            financeTransactionRecordPage
                .WaitForFinanceTransactionRecordPageToLoad()
                .ValidateServiceProvisionLinkText("")
                .ValidateServiceProvidedLinkText(serviceProvidedName)
                .ClickServiceProvidedLink();

            lookupViewPopup
                .WaitForLookupViewPopupToLoad()
                .ClickViewButton();

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad()
                .ValidateUsedInFinanceOptionChecked(true);

            #endregion

            #region Step 30 to 33

            #region Service Provision
            var plannedStartDate1 = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-28);
            var _serviceProvisionId1 = dbHelper.serviceProvision.CreateServiceProvision(_teamId, _systemUserId, _personId, Draft_Serviceprovisionstatusid,
                _serviceElement1Id, _serviceElement2Id, null, null, validRateUnits[0], _serviceprovisionstartreasonid, null,
                _teamId, _serviceProvidedId, _providerId, _systemUserId, _placementRoomTypeId, plannedStartDate1, null, null, null, null);

            #endregion

            #region Service Delivery

            dbHelper.serviceDelivery.CreateServiceDelivery(_teamId, _personId, _serviceProvisionId1, validRateUnits[0], 1, 2, true, true, true, true, true, true, true, true, "");

            #endregion

            #region Authorise Service Provision
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId1, ReadyForAuthorisation_Serviceprovisionstatusid);
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId1, Authorised_Serviceprovisionstatusid);

            #endregion

            #region Step 33

            var _serviceProvisionFinanceTransactions = dbHelper.financeTransaction.GetFinanceTransactionByServiceProvisionID(_serviceProvisionId1);
            Assert.AreEqual(0, _serviceProvisionFinanceTransactions.Count);

            var financeInvoiceTransactionWithinRatePeriodDates1 = dbHelper.financeTransaction.GetFinanceTransactionByFinanceInvoiceIdAndStartDateAndEndDate(financeInvoiceId1,
                commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-14), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-8))[0];

            var financeInvoiceTransactionWithinRatePeriodDates2 = dbHelper.financeTransaction.GetFinanceTransactionByServiceProvidedIdAndStartDateAndEndDate(_serviceProvidedId,
                commonMethodsHelper.GetThisWeekFirstMonday().AddDays(14), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(20))[0];

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad()
                .ClickBackButton();

            financeTransactionRecordPage
                .WaitForFinanceTransactionRecordPageToLoad()
                .ClickBackButton();

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .NavigateToFinanceTransactionsPage();

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad(_providerId.ToString())
                .ValidateRecordPresent(financeInvoiceTransactionWithinRatePeriodDates1.ToString())
                .ValidateRecordPresent(financeInvoiceTransactionWithinRatePeriodDates2.ToString());

            dbHelper.serviceProvidedRatePeriod.UpdateEndDate(ratePeriodId1, commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-15));
            ExpandAndProcessFinanceTransactionTriggersScheduledJob();

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad(_providerId.ToString())
                .ClickRefreshButton();

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad(_providerId.ToString())
                .ValidateRecordNotPresent(financeInvoiceTransactionWithinRatePeriodDates1.ToString())
                .ValidateRecordNotPresent(financeInvoiceTransactionWithinRatePeriodDates2.ToString());

            #endregion

            #region Step 30 and 31

            dbHelper.serviceProvidedRatePeriod.UpdateEndDate(ratePeriodId1, null);
            ExpandAndProcessFinanceTransactionTriggersScheduledJob();
            dbHelper.serviceProvidedRatePeriod.UpdateApprovalStatus(ratePeriodId1, 3); //Status = Cancelled
            ExpandAndProcessFinanceTransactionTriggersScheduledJob();

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad(_providerId.ToString())
                .ClickRefreshButton();

            var financeInvoiceTransactionWithinRatePeriodDates1_new = dbHelper.financeTransaction.GetFinanceTransactionByFinanceInvoiceIdAndStartDateAndEndDate(financeInvoiceId1,
                            commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-14), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-8))[0];
            var financeInvoiceTransactionWithinRatePeriodDates2_new = dbHelper.financeTransaction.GetFinanceTransactionByFinanceInvoiceIdAndStartDateAndEndDate(financeInvoiceId1,
                            new DateTime(2023, 1, 1), new DateTime(2023, 1, 1))[0];
            var financeInvoiceTransactionWithinRatePeriodDates3_new = dbHelper.financeTransaction.GetFinanceTransactionByServiceProvidedIdAndStartDateAndEndDate(_serviceProvidedId,
                 commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(13))[0];

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad(_providerId.ToString())
                .ValidateRecordPresent(financeInvoiceTransactionWithinRatePeriodDates1_new.ToString())
                .ValidateRecordPresent(financeInvoiceTransactionWithinRatePeriodDates2_new.ToString())
                .ValidateRecordPresent(financeInvoiceTransactionWithinRatePeriodDates3_new.ToString())
                .OpenFinanceTransactionsRecord(financeInvoiceTransactionWithinRatePeriodDates1_new.ToString());

            financeTransactionRecordPage
                .WaitForFinanceTransactionRecordPageToLoad()
                .ValidateNetAmountText("0.00")
                .ValidateVatAmountText("0.00")
                .ValidateGrossAmountText("0.00")
                .ValidateInvoiceStatusLinkText("Newly Created")
                .ClickBackButton();

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad(_providerId.ToString())
                .ValidateRecordPresent(financeInvoiceTransactionWithinRatePeriodDates2_new.ToString())
                .OpenFinanceTransactionsRecord(financeInvoiceTransactionWithinRatePeriodDates2_new.ToString());

            financeTransactionRecordPage
                .WaitForFinanceTransactionRecordPageToLoad()
                .ValidateNetAmountText("0.00")
                .ValidateVatAmountText("0.00")
                .ValidateGrossAmountText("0.00")
                .ValidateInvoiceStatusLinkText("Newly Created")
                .ClickBackButton();

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad(_providerId.ToString())
                .ValidateRecordPresent(financeInvoiceTransactionWithinRatePeriodDates3_new.ToString())
                .OpenFinanceTransactionsRecord(financeInvoiceTransactionWithinRatePeriodDates3_new.ToString());

            financeTransactionRecordPage
                .WaitForFinanceTransactionRecordPageToLoad()
                .ValidateNetAmountText("0.00")
                .ValidateVatAmountText("0.00")
                .ValidateGrossAmountText("0.00")
                .ValidateInvoiceStatusLinkText("")
                .ClickBackButton();

            #endregion

            #region Step 32

            var ratePeriodId2 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, _serviceProvidedId, validRateUnits[0], new DateTime(2020, 1, 1), null, "1", _glCodeId_Block);
            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, ratePeriodId2, _serviceProvidedId, 50m, null, null, null, null, null, null,
                null, null, null, null, null, 5m);
            dbHelper.serviceProvidedRatePeriod.UpdateApprovalStatus(ratePeriodId2, 2);

            ExpandAndProcessFinanceTransactionTriggersScheduledJob();

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad(_providerId.ToString())
                .ClickRefreshButton();

            financeTransactionsForInvoice1 = dbHelper.financeTransaction.GetFinanceTransactionByFinanceInvoiceIdAndTransactionClassId(financeInvoiceId1, 5);
            Assert.AreEqual(1, dbHelper.financeTransaction.GetFinanceTransactionByServiceProvidedIdAndStartDateAndEndDate(_serviceProvidedId,
                commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-7), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-1)).Count);
            dbHelper.financeInvoice.UpdateProviderInvoiceNumber(financeInvoiceId1, "P-2614");
            dbHelper.financeInvoice.UpdateInvoiceReceivedDate(financeInvoiceId1, DateTime.Now);
            dbHelper.financeInvoice.SetValuesVerifiedField(financeInvoiceId1, true);
            dbHelper.financeInvoice.UpdateInvoiceStatus(financeInvoiceId1, _financeInvoiceStatusId_readyForAuthorisation);

            financeTransactionsPage
                .ValidateRecordCellText(financeTransactionsForInvoice1[0].ToString(), 8, "£7.14")
                .ValidateRecordCellText(financeTransactionsForInvoice1[0].ToString(), 14, "Standard");

            dbHelper.serviceProvidedRatePeriod.UpdateApprovalStatus(ratePeriodId2, 3); //Status = Cancelled
            System.Threading.Thread.Sleep(1000);
            ExpandAndProcessFinanceTransactionTriggersScheduledJob();
            System.Threading.Thread.Sleep(1500);
            financeTransactionsPage
                .ClickRefreshButton();

            var financeTransactions_Contra = dbHelper.financeTransaction.GetFinanceTransactionByServiceProvidedIdAndTransactionClassId(_serviceProvidedId, 3);
            Assert.IsTrue(financeTransactions_Contra.Count > 0);
            Assert.AreEqual(financeTransactionsForInvoice1.Count, financeTransactions_Contra.Count);

            financeTransactionsPage
                .ValidateRecordPresent(financeTransactions_Contra[0].ToString());

            //Finance Transaction that is part of invoice, where Class = Contra for existing transaction, and new transaction is generated.
            var financeTransactionsForTheDaysRange = dbHelper.financeTransaction
                .GetFinanceTransactionByServiceProvidedIdAndStartDateAndEndDate(_serviceProvidedId
                , new DateTime(2023, 1, 1), new DateTime(2023, 1, 1));
            Assert.AreEqual(3, financeTransactionsForTheDaysRange.Count);

            //Existing Finance Transaction part of the invoice, where Class = Standard
            var financeTransactionsForTheDays1 = dbHelper.financeTransaction.GetFinanceTransactionById(financeTransactionsForTheDaysRange[2], "netamount", "grossamount", "transactionclassid");
            Assert.AreEqual("7.14", financeTransactionsForTheDays1["netamount"].ToString());
            Assert.AreEqual("7.14", financeTransactionsForTheDays1["grossamount"].ToString());
            Assert.AreEqual("5", financeTransactionsForTheDays1["transactionclassid"].ToString());

            //New Finance Transaction in which Amount = 0, and where Class = Standard
            var financeTransactionsForTheDays2 = dbHelper.financeTransaction.GetFinanceTransactionById(financeTransactionsForTheDaysRange[1], "netamount", "grossamount", "transactionclassid");
            Assert.AreEqual("0.00", financeTransactionsForTheDays2["netamount"].ToString());
            Assert.AreEqual("0.00", financeTransactionsForTheDays2["grossamount"].ToString());
            Assert.AreEqual("5", financeTransactionsForTheDays2["transactionclassid"].ToString());

            //New Finance Transaction in which Amount = negative, and where Class = Contra
            var financeTransactionsForTheDays3 = dbHelper.financeTransaction.GetFinanceTransactionById(financeTransactionsForTheDaysRange[0], "netamount", "grossamount", "transactionclassid");
            Assert.AreEqual("-7.14", financeTransactionsForTheDays3["netamount"].ToString());
            Assert.AreEqual("-7.14", financeTransactionsForTheDays3["grossamount"].ToString());
            Assert.AreEqual("3", financeTransactionsForTheDays3["transactionclassid"].ToString());

            #endregion

            #endregion

        }
        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-2614

        [TestProperty("JiraIssueID", "CDV6-25873")]
        [Description("Test automation for Step 34 to 35 from CDV6-3438")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void FinanceTransactions_CDV6_3438_UITestMethod005()
        {
            #region step 34 to 35
            dbHelper = new Phoenix.DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            #region GL Code Location

            _glCodeLocationId = dbHelper.glCodeLocation.GetByName("Service Element 1").FirstOrDefault();

            #endregion

            #region GL Code
            _glCodeId = commonMethodsDB.CreateGLCode(_teamId, _glCodeLocationId, "InvoiceGLCode", "9995", "9995");

            #endregion

            #region Service Element 1 and Service Element 2

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            _serviceElement1IdName = "SE2614_1_" + currentDate;
            _serviceElement1Id = commonMethodsDB.CreateServiceElement1(_serviceElement1IdName, _teamId, new DateTime(2021, 1, 1), code, 1, 2, validRateUnits, validRateUnits[0], _glCodeId);

            _serviceElement2IdName = "SE2614_2_" + currentDate;
            _serviceElement2Id = commonMethodsDB.CreateServiceElement2(_teamId, _serviceElement2IdName, new DateTime(2021, 1, 1), code);

            #endregion            

            #region Service Permissions
            dbHelper.servicePermission.CreateServicePermission(_teamId, _businessUnitId, _serviceElement1Id, _teamId);

            #endregion

            #region Service Mappings
            commonMethodsDB.CreateServiceMapping(_teamId, _serviceElement1Id, _serviceElement2Id);

            #endregion

            #region GL Code Mappings
            glCode_Level1Id = dbHelper.glCodeLocation.GetByName("Service Element 1")[0];
            dbHelper.glCodeMapping.CreateGLCodeMapping(_teamId, _serviceElement1Id, glCode_Level1Id);

            #endregion

            #region Service GL Coding

            dbHelper.serviceGLCoding.CreateServiceGLCoding(_teamId, false, true, _serviceElement1Id, _serviceElement2Id, null,
                null, _glCodeId, null, null);

            #endregion

            #region Finance Invoice Batch Setup
            FinanceInvoiceBatchSetupId = dbHelper.financeInvoiceBatchSetup.CreateFinanceInvoiceBatchSetup(_teamId, _businessUnitId, "", _extractNameId, 2,
                _invoiceById, _invoiceFrequencyId, _paymentTypeCodeId, _providerBatchGroupingId, 7, _serviceElement1Id, new DateTime(2023, 1, 1), true, true, true, 1);

            #endregion

            #region Provider, Service Provided
            _providerName = "SE2614_" + currentDate;
            _providerId = commonMethodsDB.CreateProvider("SE2614_" + currentDate, _teamId, 2);
            dbHelper.provider.UpdateCreditorNumberField(_providerId, "Prv2614");
            _serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_teamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, null, null, null, 1);

            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_teamId, _serviceProvidedId, _paymentTypeCodeId, _vatCodeId, _providerBatchGroupingId, new DateTime(2019, 1, 1), null, 0);

            ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, _serviceProvidedId, validRateUnits[0], new DateTime(2020, 1, 1), null, 1);

            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, ratePeriodId1, _serviceProvidedId, 50m, 50m, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), true, true, true,
                true, true, true, true, true);

            dbHelper.serviceProvidedRatePeriod.UpdateApprovalStatus(ratePeriodId1, 2);

            //Provider and Service Provided Record 2
            var _provider2Name = "SE2614b_" + currentDate;
            var _providerId2 = commonMethodsDB.CreateProvider(_provider2Name, _teamId, 2);
            dbHelper.provider.UpdateCreditorNumberField(_providerId2, "Prvb2614");
            var _serviceProvidedId2 = dbHelper.serviceProvided.CreateServiceProvided(_teamId, _systemUserId, _providerId2, _serviceElement1Id, _serviceElement2Id, null, null, null, 1);

            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_teamId, _serviceProvidedId2, _paymentTypeCodeId, _vatCodeId, _providerBatchGroupingId, new DateTime(2019, 1, 1), null, 0);

            var ratePeriodId2 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, _serviceProvidedId2, validRateUnits[0], new DateTime(2020, 1, 1), null, 1);

            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, ratePeriodId2, _serviceProvidedId2, 50m, 50m, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), true, true, true,
                true, true, true, true, true);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(_providerName, _providerId.ToString())
                .OpenProviderRecord(_providerId.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .TapServicesProvidedTab();

            servicesProvidedPage
                .WaitForServicesProvidedPageToLoad()
                .OpenServiceProvidedRecord(_serviceProvidedId.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad()
                .SelectContractType("Block")
                .ClickSaveButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("You are changing the Contract Type from Spot to Block, but there are Rate Period records associated that use a Rate Unit that is not permitted (i.e. it is not a Weekly Rate Type). These records will be deleted if you continue.")
                .TapOKButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("The Service Provided Rate Period records could not be deleted. Rate Period records that have been approved cannot be deleted.")
                .TapCloseButton();

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad()
                .SelectContractType("Spot");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(_provider2Name, _providerId2.ToString())
                .OpenProviderRecord(_providerId2.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .TapServicesProvidedTab();

            servicesProvidedPage
                .WaitForServicesProvidedPageToLoad()
                .OpenServiceProvidedRecord(_serviceProvidedId2.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad()
                .NavigateToRatePeriodsTab();

            serviceProvidedRatePeriodsPage
                .WaitForServicesProvidedPageToLoad()
                .SelectAvailableViewByText("Pending Rate Periods")
                .ValidateRecordPresent(ratePeriodId2.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad()
                .NavigateToDetailsTab()
                .SelectContractType("Block")
                .ClickSaveButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("You are changing the Contract Type from Spot to Block, but there are Rate Period records associated that use a Rate Unit that is not permitted (i.e. it is not a Weekly Rate Type). These records will be deleted if you continue.")
                .TapOKButton();

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad()
                .ValidateContractTypePickListSelectedText("Block")
                .NavigateToRatePeriodsTab();

            serviceProvidedRatePeriodsPage
                .WaitForServicesProvidedPageToLoad()
                .SelectAvailableViewByText("Related Records")
                .ValidateRecordNotPresent(ratePeriodId2.ToString());


            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1795

        [TestProperty("JiraIssueID", "CDV6-25874")]
        [Description("Test automation for Step 36 to 39 from CDV6-3438")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void FinanceTransactions_CDV6_3438_UITestMethod006()
        {
            #region step 36 to 39

            #region Step 36

            dbHelper = new Phoenix.DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            #region GL Code Location

            var _glCodeLocationId_Block = dbHelper.glCodeLocation.GetByName("Block").FirstOrDefault();
            var _gloCodeLocationId_ProvSE1SE2CC = dbHelper.glCodeLocation.GetByName("Provider / Service Element 1 / Service Element 2 / Client Category").FirstOrDefault();

            #endregion

            #region GL Code
            var _glCodeId_Block = commonMethodsDB.CreateGLCode(_teamId, _glCodeLocationId_Block, "BLC", "EC1", "2328");
            var _glCodeId_ProvSE1SE2CC = commonMethodsDB.CreateGLCode(_teamId, _gloCodeLocationId_ProvSE1SE2CC, "PSSC1", "2328b", "2328b");


            #endregion

            #region Service Element 1 and Service Element 2

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            validRateUnits = new List<Guid>();
            var rateUnit_PerWeekProRata = dbHelper.rateUnit.GetByName("Per Week Pro Rata \\ Weekly")[0];
            validRateUnits.Add(rateUnit_PerWeekProRata);
            _serviceElement1IdName = "SE2328_1_" + currentDate;
            _serviceElement1Id = commonMethodsDB.CreateServiceElement1(_serviceElement1IdName, _teamId, new DateTime(2021, 1, 1), code, 1, 2, validRateUnits, rateUnit_PerWeekProRata, _glCodeId_Block);

            _serviceElement2IdName = "SE2328_2_" + currentDate;
            _serviceElement2Id = commonMethodsDB.CreateServiceElement2(_teamId, _serviceElement2IdName, new DateTime(2021, 1, 1), code);

            #endregion

            #region Service Permissions
            dbHelper.servicePermission.CreateServicePermission(_teamId, _businessUnitId, _serviceElement1Id, _teamId);

            #endregion

            #region Service Mappings
            commonMethodsDB.CreateServiceMapping(_teamId, _serviceElement1Id, _serviceElement2Id);

            #endregion

            #region GL Code Mappings
            glCode_Level1Id = dbHelper.glCodeLocation.GetByName("Service Element 1")[0];
            dbHelper.glCodeMapping.CreateGLCodeMapping(_teamId, _serviceElement1Id, glCode_Level1Id);

            #endregion

            #region Service GL Coding

            dbHelper.serviceGLCoding.CreateServiceGLCoding(_teamId, false, true, _serviceElement1Id, _serviceElement2Id, null,
                null, _glCodeId, null, null);

            #endregion

            #region Finance Invoice Batch Setup
            FinanceInvoiceBatchSetupId = dbHelper.financeInvoiceBatchSetup.CreateFinanceInvoiceBatchSetup(_teamId, _businessUnitId, "", _extractNameId, 2,
                _invoiceById, _invoiceFrequencyId, _paymentTypeCodeId, _providerBatchGroupingId, 7, _serviceElement1Id, new DateTime(2023, 1, 1), true, true, true, 1);

            #endregion

            #region Provider, Service Provided
            _providerName = "SE3438_" + currentDate;
            _providerId = commonMethodsDB.CreateProvider("SE3438_" + currentDate, _teamId, 2);
            dbHelper.provider.UpdateCreditorNumberField(_providerId, "Prv3438");
            _serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_teamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, null, null, null, _glCodeId_ProvSE1SE2CC, 2, false, 2, false);
            string serviceProvidedName = (string)dbHelper.serviceProvided.GetByID(_serviceProvidedId, "name")["name"];

            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_teamId, _serviceProvidedId, _paymentTypeCodeId, _vatCodeId, _providerBatchGroupingId, new DateTime(2019, 1, 1), null, 0);

            ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, _serviceProvidedId, validRateUnits[0], new DateTime(2020, 1, 1), null, "1", _glCodeId_Block);
            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, ratePeriodId1, _serviceProvidedId, 50m, null, null, null, null, null, null,
                null, null, null, null, null, 5m);

            dbHelper.serviceProvidedRatePeriod.UpdateApprovalStatus(ratePeriodId1, 2);
            dbHelper.serviceProvided.UpdateStatus(_serviceProvidedId, 2);

            ExpandAndProcessFinanceTransactionTriggersScheduledJob();
            #endregion

            var invoiceBatchId = dbHelper.financeInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(FinanceInvoiceBatchSetupId)[0];
            dbHelper.financeInvoiceBatch.UpdateFinanceInvoiceBatchDates(invoiceBatchId, DateTime.Now, DateTime.Now);

            #region Execute 'Process Invoice Batches' scheduled job
            //Get the schedule job id
            Guid processFinanceBatchesJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Process Invoice Batches")[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(processFinanceBatchesJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(processFinanceBatchesJobId);

            #endregion

            var invoiceBatch_InvoiceId = dbHelper.financeInvoice.GetFinanceInvoiceByInvoiceBatchId(invoiceBatchId);
            Assert.AreEqual(1, invoiceBatch_InvoiceId.Count);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(_providerName, _providerId.ToString())
                .OpenProviderRecord(_providerId.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .NavigateToFinanceTransactionsPage();

            var financeTransactionsForServiceProvided = dbHelper.financeTransaction.GetFinanceTransactionByServiceProvidedIdAndTransactionClassId(_serviceProvidedId, 5);
            Assert.IsTrue(financeTransactionsForServiceProvided.Count > 0);

            int FinanceTransactionsCount_Before = financeTransactionsForServiceProvided.Count;

            var financeTransaction1 = dbHelper.financeTransaction.GetFinanceTransactionByServiceProvidedIdAndStartDateAndEndDate(_serviceProvidedId, new DateTime(2023, 1, 1), new DateTime(2023, 1, 1))[0];
            var financeTransaction2 = dbHelper.financeTransaction.GetFinanceTransactionByServiceProvidedIdAndStartDateAndEndDate(_serviceProvidedId, commonMethodsHelper.GetThisWeekFirstMonday().AddDays(14), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(20))[0];

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad(_providerId.ToString())
                .ClickRefreshButton()
                .WaitForFinanceTransactionsPageToLoad(_providerId.ToString());

            financeTransactionsPage
                .ValidateRecordPresent(financeTransaction1.ToString())
                .ValidateRecordPresent(financeTransaction2.ToString());

            #region Service Provision
            var plannedStartDate1 = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-28);
            var _serviceProvisionId1 = dbHelper.serviceProvision.CreateServiceProvision(_teamId, _systemUserId, _personId, Draft_Serviceprovisionstatusid,
                _serviceElement1Id, _serviceElement2Id, null, null, validRateUnits[0], _serviceprovisionstartreasonid, null,
                _teamId, _serviceProvidedId, _providerId, _systemUserId, _placementRoomTypeId, plannedStartDate1, null, null, null, null);

            #endregion

            #region Authorise Service Provision
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId1, ReadyForAuthorisation_Serviceprovisionstatusid);
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId1, Authorised_Serviceprovisionstatusid);

            #endregion
            #endregion

            #region Step 37
            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .TapServiceProvisionsTab();

            serviceProvisionsPage
                .WaitForServiceProvisionsPageToLoad(_providerId.ToString())
                .OpenServiceProvisionRecord(_serviceProvisionId1.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ClickFinanceTransactionsTab();

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad(_serviceProvisionId1.ToString());

            var _serviceProvisionFinanceTransactions = dbHelper.financeTransaction.GetFinanceTransactionByServiceProvisionID(_serviceProvisionId1);
            Assert.AreEqual(0, _serviceProvisionFinanceTransactions.Count);
            #endregion

            #region Step 38
            //cancel the service provision
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId1, Cancelled_Serviceprovisionstatusid);
            Assert.AreEqual(Cancelled_Serviceprovisionstatusid.ToString(), dbHelper.serviceProvision.GetByID(_serviceProvisionId1, "serviceprovisionstatusid")["serviceprovisionstatusid"].ToString());
            #endregion

            #region Step 39

            ExpandAndProcessFinanceTransactionTriggersScheduledJob();

            financeTransactionsForServiceProvided = dbHelper.financeTransaction.GetFinanceTransactionByServiceProvidedIdAndTransactionClassId(_serviceProvidedId, 5);
            int FinanceTransactionsCount_After = financeTransactionsForServiceProvided.Count;

            Assert.AreEqual(FinanceTransactionsCount_Before, FinanceTransactionsCount_After);

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ClickBackButton();

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .NavigateToFinanceTransactionsPage();

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad(_providerId.ToString())
                .ClickRefreshButton()
                .WaitForFinanceTransactionsPageToLoad(_providerId.ToString());

            financeTransactionsPage
                .ValidateRecordPresent(financeTransaction1.ToString())
                .ValidateRecordPresent(financeTransaction2.ToString());
            #endregion

            #endregion
        }
        #endregion
    }
}
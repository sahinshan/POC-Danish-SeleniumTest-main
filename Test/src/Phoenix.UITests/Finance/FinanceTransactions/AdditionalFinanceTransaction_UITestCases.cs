using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Finance.FinanceAdmin
{
    [TestClass]
    public class AdditionalFinanceTransaction_UITestCases : FunctionalTest
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

                _systemUsername = "FinanceInvoiceUser2";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "FinanceInvoice", "User2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

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

            #region Rate Units

            rateunitid = dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)")[0];
            validRateUnits = new List<Guid>();
            validRateUnits.Add(rateunitid);
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)")[0]);

            #endregion

            #region Service Element 1 and Service Element 2

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            _serviceElement1IdName = "SE80_1_" + currentDate;
            _serviceElement1Id = commonMethodsDB.CreateServiceElement1(_serviceElement1IdName, _teamId, new DateTime(2021, 1, 1), code, 1, 2, validRateUnits, validRateUnits[0], _glCodeId);

            _serviceElement2IdName = "SE80_2_" + currentDate;
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
            _providerName = "SE3087_" + currentDate;
            _providerId = commonMethodsDB.CreateProvider("SE1780_" + currentDate, _teamId, 2);
            _serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_teamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, null, null, null, 2);

            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_teamId, _serviceProvidedId, _paymentTypeCodeId, _vatCodeId, _providerBatchGroupingId, new DateTime(2019, 1, 1), null, 0);

            var ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, _serviceProvidedId, validRateUnits[0], new DateTime(2020, 1, 1), null, 1);

            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, ratePeriodId1, _serviceProvidedId, 10m, 15m, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), true, true, true,
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

        private void SetupForServiceElementWhoToPayPerson()
        {
            serviceElement1IdName_Person = "SE92_1_" + currentDate;
            var code2 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            _serviceElement1Id_Person =
                commonMethodsDB.CreateServiceElement1(_teamId, serviceElement1IdName_Person, new DateTime(2021, 1, 1), code2, 3, 2, validRateUnits, validRateUnits[1], _paymentTypeCodeId, _providerBatchGroupingId, 0, _vatCodeId);
            serviceElement2IdName_Person = "SE92_2_" + currentDate;
            _serviceElement2Id_Person = commonMethodsDB.CreateServiceElement2(_teamId, serviceElement2IdName_Person, new DateTime(2021, 1, 1), code2);
            dbHelper.servicePermission.CreateServicePermission(_teamId, _businessUnitId, _serviceElement1Id_Person, _teamId);
            commonMethodsDB.CreateServiceMapping(_teamId, _serviceElement1Id_Person, _serviceElement2Id_Person);
            var _glCodeLocationId2 = dbHelper.glCodeLocation.GetByName("Service Element 1 / Service Element 2 / Client Category").FirstOrDefault();
            var _glCodeId2 = commonMethodsDB.CreateGLCode(_teamId, _glCodeLocationId2, "TestServiceGLCode", "8995", "8995");
            dbHelper.glCodeMapping.CreateGLCodeMapping(_teamId, _serviceElement1Id_Person, _glCodeLocationId2);
            dbHelper.serviceGLCoding.CreateServiceGLCoding(_teamId, false, true, _serviceElement1Id_Person, _serviceElement2Id_Person, null,
                null, _glCodeId2, null, null);

            #region Finance Invoice Batch Setup
            FinanceInvoiceBatchSetupId_Person = dbHelper.financeInvoiceBatchSetup.CreateFinanceInvoiceBatchSetup(_teamId, _businessUnitId, "", _extractNameId, 2,
                _invoiceById, _invoiceFrequencyId, _paymentTypeCodeId, _providerBatchGroupingId, 7, _serviceElement1Id_Person, new DateTime(2023, 1, 1), true, true, true, 1);

            #endregion

            #region Service Provision
            plannedStartDate1 = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-28);
            _serviceProvisionId2 = dbHelper.serviceProvision.CreateServiceProvision(_teamId, _systemUserId, _personId, Draft_Serviceprovisionstatusid,
                _serviceElement1Id_Person, _serviceElement2Id_Person, null, null, validRateUnits[1], _serviceprovisionstartreasonid, null,
                _teamId, null, null, _systemUserId, _placementRoomTypeId, plannedStartDate1, null, null, null, null, null, true);
            _serviceProvisionTitle2 = (string)dbHelper.serviceProvision.GetByID(_serviceProvisionId2, "name")["name"];
            var ratePeriodId2 = dbHelper.serviceProvisionRatePeriod.CreateServiceProvisionRatePeriod(_teamId, _personId, _serviceProvisionTitle2, _serviceProvisionId2, validRateUnits[1], new DateTime(2020, 1, 1), null, 1);

            dbHelper.serviceProvisionRateSchedule.CreateServiceProvisionRateSchedule(_teamId, _personId, _serviceProvisionId2, ratePeriodId2, 10m, 15m, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), true, true, true,
                true, true, true, true, true);

            dbHelper.serviceProvisionRatePeriod.UpdateStatus(ratePeriodId2, 2);

            #endregion

            #region Service Delivery

            dbHelper.serviceDelivery.CreateServiceDelivery(_teamId, _personId, _serviceProvisionId2, validRateUnits[1], 1, 1, true, true, true, true, true, true, true, true, "");

            #endregion

            #region Authorise Service Provision
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId2, ReadyForAuthorisation_Serviceprovisionstatusid);
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId2, Authorised_Serviceprovisionstatusid);

            #endregion

        }

        private void ExpandAndProcessFinanceTransactionTriggersScheduledJob(Guid serviceProvisionId)
        {
            #region Execute 'Expand and Process Finance Transaction Triggers' scheduled job
            var serviceProvision_FinanceTransactions = dbHelper.financeTransaction.GetFinanceTransactionByServiceProvisionID(serviceProvisionId);
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

            serviceProvision_FinanceTransactions = dbHelper.financeTransaction.GetFinanceTransactionByServiceProvisionID(serviceProvisionId);
            Assert.IsTrue(serviceProvision_FinanceTransactions.Count > 0);

            #endregion
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-1792

        [TestProperty("JiraIssueID", "CDV6-25670")]
        [Description("Test automation for Step 1 to 4 from CDV6-3087")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void AdditionalFinanceTransaction_CDV63087_UITestMethod001()
        {
            #region step 1 to 4, 6, 7
            dbHelper = new Phoenix.DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);
            SetupForServiceElementWhoToPayProvider();
            ExpandAndProcessFinanceTransactionTriggersScheduledJob(_serviceProvisionId);

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
                .OpenRecord(FinanceInvoiceBatchSetupId.ToString());

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .NavigateToFinanceInvoicesBatchesPage();

            var invoiceBatchId = dbHelper.financeInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(FinanceInvoiceBatchSetupId)[0];

            financeInvoiceBatchesPage
                .WaitForFinanceInvoiceBatchSetup_FinanceInvoiceBatchesPageToLoad()
                .OpenRecord(invoiceBatchId.ToString());

            financeInvoiceBatchRecordPage
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ValidateBatchstatusSelectedText("New")
                .InsertTextOnRunontime(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnPeriodEndDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ClickRunInvoiceBatchRecordButton()
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ClickInvoicesTab();

            var invoiceBatch_InvoiceId = dbHelper.financeInvoice.GetFinanceInvoiceByInvoiceBatchId(invoiceBatchId);
            Assert.AreEqual(1, invoiceBatch_InvoiceId.Count);

            var financeInvoiceId = dbHelper.financeInvoice.GetFinanceInvoiceById(invoiceBatch_InvoiceId[0], "invoicestatusid", "name");
            Assert.AreEqual(_financeInvoiceStatusId_newlyCreated.ToString(), financeInvoiceId["invoicestatusid"].ToString());
            string _invoiceTitle = (string)financeInvoiceId["name"];

            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad(invoiceBatchId.ToString())
                .OpenRecord(invoiceBatch_InvoiceId[0].ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusFieldText("Newly Created")
                .ClickAdditionalTransactionsTab();

            additionalTransactionsFinanceTransactionsPage
                .WaitForAdditionalTransactionsFinanceTransactionsPageToLoad()
                .ClickNewRecordButton();

            financeTransactionRecordPage
                .WaitForFinanceTransactionRecordPageToLoad()
                .InsertTextOnStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnEndDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnNetAmount("10")
                .InsertTextOnVatAmount("0")
                .ClickVatCodeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_vatCodeId.ToString());

            financeTransactionRecordPage
                .WaitForFinanceTransactionRecordPageToLoad()
                .ClickServiceProvisionLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_serviceProvisionId.ToString());

            financeTransactionRecordPage
                .WaitForFinanceTransactionRecordPageToLoad()
                .ClickSaveButton()
                .WaitForFinanceTransactionRecordPageToLoad();

            financeTransactionRecordPage
                .WaitForFinanceTransactionRecordPageToLoad()
                .ValidateTransactionClassSelectedText("Additional")
                .ValidateFinanceInvoiceLinkText(_invoiceTitle)
                .ValidateInvoiceStatusLinkText("Newly Created")
                .ValidateServiceProvisionLinkText(_serviceProvisionTitle)
                .ValidateServiceElement1LinkText(_serviceElement1IdName);

            var financeTransactions = dbHelper.financeTransaction.GetFinanceTransactionByFinanceInvoiceIdAndTransactionClassId(invoiceBatch_InvoiceId[0], 2);
            Assert.AreEqual(1, financeTransactions.Count);

            Guid financeTransaction_NewlyCreated = financeTransactions[0];

            financeTransactionRecordPage
                .ClickBackButton();

            additionalTransactionsFinanceTransactionsPage
                .WaitForAdditionalTransactionsFinanceTransactionsPageToLoad()
                .ValidateRecordIsPresent(financeTransaction_NewlyCreated.ToString(), true);

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickFinanceTransactionsTab();

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad(invoiceBatch_InvoiceId[0].ToString())
                .SelectAvailableViewByText("Related Records")
                .ValidateRecordPresent(financeTransaction_NewlyCreated.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickDetailsTab()
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickInvoiceStatusFieldLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_financeInvoiceStatusId_completed.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickSaveButton()
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusFieldText("Completed")
                .ClickAdditionalTransactionsTab();

            additionalTransactionsFinanceTransactionsPage
                .WaitForAdditionalTransactionsFinanceTransactionsPageToLoad()
                .ClickNewRecordButton();

            financeTransactionRecordPage
                .WaitForFinanceTransactionRecordPageToLoad()
                .InsertTextOnStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnEndDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnNetAmount("20")
                .InsertTextOnVatAmount("0")
                .ClickVatCodeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_vatCodeId.ToString());

            financeTransactionRecordPage
                .WaitForFinanceTransactionRecordPageToLoad()
                .ClickServiceProvisionLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_serviceProvisionId.ToString());

            financeTransactionRecordPage
                .WaitForFinanceTransactionRecordPageToLoad()
                .ClickSaveButton()
                .WaitForFinanceTransactionRecordPageToLoad();

            financeTransactionRecordPage
                .WaitForFinanceTransactionRecordPageToLoad()
                .ValidateTransactionClassSelectedText("Additional")
                .ValidateFinanceInvoiceLinkText(_invoiceTitle)
                .ValidateInvoiceStatusLinkText("Completed")
                .ValidateServiceProvisionLinkText(_serviceProvisionTitle)
                .ValidateServiceElement1LinkText(_serviceElement1IdName);

            financeTransactions = dbHelper.financeTransaction.GetFinanceTransactionByFinanceInvoiceIdAndTransactionClassId(invoiceBatch_InvoiceId[0], 2);
            Guid financeTransaction_Completed = financeTransactions[0];
            financeTransaction_NewlyCreated = financeTransactions[1];

            financeTransactionRecordPage
                .ClickBackButton();

            additionalTransactionsFinanceTransactionsPage
                .WaitForAdditionalTransactionsFinanceTransactionsPageToLoad()
                .SelectAdditionalTransactionsFinanceTransactionsRecord(financeTransaction_Completed.ToString())
                .SelectAdditionalTransactionsFinanceTransactionsRecord(financeTransaction_NewlyCreated.ToString())
                .ClickDeleteRecordButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Are you sure you wish to delete this record?  This action will also delete any Interest Transaction record associated to it")
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("2 item(s) deleted.")
                .TapOKButton();

            additionalTransactionsFinanceTransactionsPage
                .WaitForAdditionalTransactionsFinanceTransactionsPageToLoad()
                .ValidateRecordIsPresent(financeTransaction_Completed.ToString(), false)
                .ValidateRecordIsPresent(financeTransaction_NewlyCreated.ToString(), false);

            financeTransactions = dbHelper.financeTransaction.GetFinanceTransactionByFinanceInvoiceIdAndTransactionClassId(invoiceBatch_InvoiceId[0], 2);
            Assert.AreEqual(0, financeTransactions.Count);

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickFinanceTransactionsTab();

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad(invoiceBatch_InvoiceId[0].ToString())
                .SelectAvailableViewByText("Related Records")
                .ClickGridHeaderSelectorRowCheckbox()
                .ClickTotalsButton();

            totalsPage
                .WaitForTotalsPageToLoad(invoiceBatch_InvoiceId[0].ToString())
                .ValidateNumberOfRecordsValue("4")
                .ValidateNetAmountValue("£280.00")
                .ValidateVatAmountValue("£0.00")
                .ValidateGrossAmountValue("£280.00")
                .ClickBackButton();

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickAdditionalTransactionsTab();

            additionalTransactionsFinanceTransactionsPage
                .WaitForAdditionalTransactionsFinanceTransactionsPageToLoad()
                .ClickNewRecordButton();

            financeTransactionRecordPage
                .WaitForFinanceTransactionRecordPageToLoad()
                .InsertTextOnStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnEndDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnNetAmount("10")
                .InsertTextOnVatAmount("5")
                .ClickVatCodeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_vatCodeId.ToString());

            financeTransactionRecordPage
                .WaitForFinanceTransactionRecordPageToLoad()
                .ClickServiceProvisionLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_serviceProvisionId.ToString());

            financeTransactionRecordPage
                .WaitForFinanceTransactionRecordPageToLoad()
                .ClickSaveButton()
                .WaitForFinanceTransactionRecordPageToLoad();

            financeTransactionRecordPage
                .WaitForFinanceTransactionRecordPageToLoad()
                .ValidateTransactionClassSelectedText("Additional")
                .ValidateFinanceInvoiceLinkText(_invoiceTitle)
                .ValidateInvoiceStatusLinkText("Completed")
                .ValidateServiceProvisionLinkText(_serviceProvisionTitle)
                .ValidateServiceElement1LinkText(_serviceElement1IdName);

            financeTransactions = dbHelper.financeTransaction.GetFinanceTransactionByFinanceInvoiceIdAndTransactionClassId(invoiceBatch_InvoiceId[0], 2);
            Assert.AreEqual(1, financeTransactions.Count);

            financeTransactionRecordPage
                .ClickBackButton();

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickFinanceTransactionsTab();

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad(invoiceBatch_InvoiceId[0].ToString())
                .SelectAvailableViewByText("Related Records")
                .ClickGridHeaderSelectorRowCheckbox()
                .ClickTotalsButton();

            totalsPage
                .WaitForTotalsPageToLoad(invoiceBatch_InvoiceId[0].ToString())
                .ValidateNumberOfRecordsValue("5")
                .ValidateNetAmountValue("£290.00")
                .ValidateVatAmountValue("£5.00")
                .ValidateGrossAmountValue("£295.00")
                .ClickBackButton();

            #endregion
        }

        [TestProperty("JiraIssueID", "CDV6-25685")]
        [Description("Test automation for Step 5 from CDV6-3087")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void AdditionalFinanceTransaction_CDV63087_UITestMethod002()
        {
            #region step 5
            dbHelper = new Phoenix.DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);
            SetupForServiceElementWhoToPayPerson();

            ExpandAndProcessFinanceTransactionTriggersScheduledJob(_serviceProvisionId2);

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
                .InsertSearchQuery("*" + serviceElement1IdName_Person + "*")
                .TapSearchButton()
                .OpenRecord(FinanceInvoiceBatchSetupId_Person.ToString());

            financeInvoiceBatchSetupRecordPage
                .WaitForFinanceInvoiceBatchSetupRecordPageToLoad()
                .NavigateToFinanceInvoicesBatchesPage();

            var invoiceBatchId = dbHelper.financeInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(FinanceInvoiceBatchSetupId_Person)[0];

            financeInvoiceBatchesPage
                .WaitForFinanceInvoiceBatchSetup_FinanceInvoiceBatchesPageToLoad()
                .OpenRecord(invoiceBatchId.ToString());

            financeInvoiceBatchRecordPage
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ValidateBatchstatusSelectedText("New")
                .InsertTextOnRunontime(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnPeriodEndDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ClickRunInvoiceBatchRecordButton()
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ClickInvoicesTab();

            var invoiceBatch_InvoiceId = dbHelper.financeInvoice.GetFinanceInvoiceByInvoiceBatchId(invoiceBatchId);
            Assert.AreEqual(1, invoiceBatch_InvoiceId.Count);

            var financeInvoiceId = dbHelper.financeInvoice.GetFinanceInvoiceById(invoiceBatch_InvoiceId[0], "invoicestatusid", "name");
            Assert.AreEqual(_financeInvoiceStatusId_newlyCreated.ToString(), financeInvoiceId["invoicestatusid"].ToString());
            string _invoiceTitle = (string)financeInvoiceId["name"];

            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad(invoiceBatchId.ToString())
                .OpenRecord(invoiceBatch_InvoiceId[0].ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusFieldText("Newly Created")
                .ClickAdditionalTransactionsTab();

            additionalTransactionsFinanceTransactionsPage
                .WaitForAdditionalTransactionsFinanceTransactionsPageToLoad()
                .ClickNewRecordButton();

            financeTransactionRecordPage
                .WaitForFinanceTransactionRecordPageToLoad()
                .InsertTextOnStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnEndDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnNetAmount("10")
                .InsertTextOnVatAmount("0")
                .ClickVatCodeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_vatCodeId.ToString());

            financeTransactionRecordPage
                .WaitForFinanceTransactionRecordPageToLoad()
                .ClickServiceProvisionLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_serviceProvisionId2.ToString());

            financeTransactionRecordPage
                .WaitForFinanceTransactionRecordPageToLoad()
                .ClickSaveButton()
                .WaitForFinanceTransactionRecordPageToLoad();

            financeTransactionRecordPage
                .WaitForFinanceTransactionRecordPageToLoad()
                .ValidateTransactionClassSelectedText("Additional")
                .ValidateFinanceInvoiceLinkText(_invoiceTitle)
                .ValidateInvoiceStatusLinkText("Newly Created")
                .ValidateServiceProvisionLinkText(_serviceProvisionTitle2)
                .ValidateServiceElement1LinkText(serviceElement1IdName_Person);

            var financeTransactions = dbHelper.financeTransaction.GetFinanceTransactionByFinanceInvoiceIdAndTransactionClassId(invoiceBatch_InvoiceId[0], 2);
            Assert.AreEqual(1, financeTransactions.Count);

            financeTransactionRecordPage
                .ClickBackButton();

            additionalTransactionsFinanceTransactionsPage
                .WaitForAdditionalTransactionsFinanceTransactionsPageToLoad()
                .ValidateRecordIsPresent(financeTransactions[0].ToString(), true);

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickFinanceTransactionsTab();

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad(invoiceBatch_InvoiceId[0].ToString())
                .SelectAvailableViewByText("Related Records")
                .ValidateRecordPresent(financeTransactions[0].ToString());

            #endregion
        }

        #endregion

    }
}

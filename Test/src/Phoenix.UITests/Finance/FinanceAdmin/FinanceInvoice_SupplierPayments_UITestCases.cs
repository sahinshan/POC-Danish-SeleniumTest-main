using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Finance.FinanceAdmin
{
    [TestClass]
    public class FinanceInvoice_SupplierPayments_UITestCases : FunctionalTest
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
        private string _providerName;
        private Guid _providerId;
        private Guid _serviceProvidedId;
        private Guid FinanceInvoiceBatchSetupId;
        private string _serviceProvisionTitle;
        private Guid _serviceElement1Id;
        private Guid _serviceElement2Id;
        private string _serviceElement1IdName;
        private string _serviceElement2IdName;
        private DateTime plannedStartDate1;
        private Guid rateunitid;
        private List<Guid> validRateUnits;

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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("Finance Invoice T1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("Finance Invoice T1", null, _businessUnitId, "987767", "FinanceInvoice@careworkstempmail.com", "FinanceInvoice", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region System User FinanceInvoiceUser

                _systemUsername = "FinanceInvoiceUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "FinanceInvoice", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

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

                #region GL Code Location

                var _glCodeLocationId = dbHelper.glCodeLocation.GetByName("Service Element 1").FirstOrDefault();

                #endregion

                #region GL Code
                var _glCodeId = commonMethodsDB.CreateGLCode(_teamId, _glCodeLocationId, "InvoiceGLCode", "9995", "9995");

                #endregion

                #region Rate Units

                rateunitid = dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)")[0];
                validRateUnits = new List<Guid>();
                validRateUnits.Add(rateunitid);

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
                var glCode_Level1Id = dbHelper.glCodeLocation.GetByName("Service Element 1")[0];
                dbHelper.glCodeMapping.CreateGLCodeMapping(_teamId, _serviceElement1Id, glCode_Level1Id);

                #endregion

                #region Service GL Coding

                dbHelper.serviceGLCoding.CreateServiceGLCoding(_teamId, false, true, _serviceElement1Id, _serviceElement2Id, null,
                    null, _glCodeId, null, null);

                #endregion

                #region Authorisation Level

                if (!dbHelper.authorisationLevel.GetBySystemUserId(_systemUserId, 7, new DateTime(2021, 1, 1)).Any())
                    dbHelper.authorisationLevel.CreateAuthorisationLevel(_teamId, _systemUserId, new DateTime(2021, 1, 1), 7, 999999m, true, true);

                #endregion

                #region Finance Invoice Batch Setup
                FinanceInvoiceBatchSetupId = dbHelper.financeInvoiceBatchSetup.CreateFinanceInvoiceBatchSetup(_teamId, _businessUnitId, "", _extractNameId, 2,
                    _invoiceById, _invoiceFrequencyId, _paymentTypeCodeId, _providerBatchGroupingId, 7, _serviceElement1Id, new DateTime(2023, 1, 1), true, true, true, 1);

                #endregion

                #region Provider, Service Provided
                _providerName = "ACC1780_" + currentDate;
                _providerId = commonMethodsDB.CreateProvider("ACC1780_" + currentDate, _teamId, 2);
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

                serviceProvision_FinanceTransactions = dbHelper.financeTransaction.GetFinanceTransactionByServiceProvisionID(_serviceProvisionId);
                Assert.IsTrue(serviceProvision_FinanceTransactions.Count > 0);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-1779

        [TestProperty("JiraIssueID", "CDV6-25492")]
        [Description("Test automation for Step 1 to 4 from CDV6-2996: Verifying the generation and update of Finance Invoice for Supplier Payments")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void FinanceInvoice_UITestMethod001()
        {
            #region step 1 to 4

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
                .ValidateBatchstatusSelectedText("Completed");

            financeInvoiceBatchRecordPage
                .ClickInvoicesTab();

            var invoiceBatch_InvoiceId = dbHelper.financeInvoice.GetFinanceInvoiceByInvoiceBatchId(invoiceBatchId);
            Assert.AreEqual(1, invoiceBatch_InvoiceId.Count);

            var invoicestatusId = (string)dbHelper.financeInvoice.GetFinanceInvoiceById(invoiceBatch_InvoiceId[0], "invoicestatusid")["invoicestatusid"];
            Assert.AreEqual(_financeInvoiceStatusId_newlyCreated.ToString(), invoicestatusId);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1780

        [TestProperty("JiraIssueID", "CDV6-25508")]
        [Description("Test automation for Step 5 to 11 from CDV6-2996: Verifying the generation and update of Finance Invoice for Supplier Payments")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void FinanceInvoice_UITestMethod002()
        {
            #region step 5 to 11

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
                .ClickAdditionalTransactionsTab();

            additionalTransactionsFinanceTransactionsPage
                .WaitForAdditionalTransactionsFinanceTransactionsPageToLoad()
                .ClickNewRecordButton();

            financeTransactionRecordPage
                .WaitForFinanceTransactionRecordPageToLoad()
                .InsertTextOnStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickEndDateDatePicker();

            calendarDatePicker
                .WaitForCalendarPickerPopupToLoad()
                .SelectCalendarDate(DateTime.Now.AddDays(-1).Date);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The End Date must be on or after the Start Date.")
                .TapOKButton();

            financeTransactionRecordPage
                .WaitForFinanceTransactionRecordPageToLoad()
                .InsertTextOnEndDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnNetAmount("10")
                .InsertTextOnVatAmount("10")
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
                .ValidatePersonLinkText(_personFullname)
                .ValidatePersonNumberText(_personNumber)
                .ValidateStartDateText(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDateText(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidateTransactionClassSelectedText("Additional")
                .ValidateNetAmountText("10.00")
                .ValidateVatAmountText("10.00")
                .ValidateVatCodeLinkText("Standard Rated")
                .ValidateFinanceInvoiceLinkText(_invoiceTitle)
                .ValidateInvoiceStatusLinkText("Newly Created")
                .ValidateServiceProvisionLinkText(_serviceProvisionTitle)
                .ValidateServiceElement1LinkText(_serviceElement1IdName)
                .ValidateGlCodeText("9995");

            financeTransactionRecordPage
                .ValidateStartDateFieldDisplayed(true)
                .ValidateEndDateFieldDisplayed(true)
                .ValidateNetAmountFieldDisplayed(true)
                .ValidateVatAmountFieldDisplayed(true)
                .ValidateGLCodeFieldDisplayed(true)
                .ValidateVatCodeFieldDisplayed(true)
                .ValidateFinanceInvoiceFieldDisplayed(true)
                .ValidateServiceProvisionFieldDisplayed(true)
                .ValidateNotesFieldDisplayed(true);

            financeTransactionRecordPage
                .ValidateFinanceModulePicklistDisplayed(true)
                .ValidateResponsibleTeamLinkFieldDisplayed(true)
                .ValidatePersonLinkFieldDisplayed(true)
                .ValidatePersonNumberFieldDisplayed(true)
                .ValidateTransactionNumberFieldDisplayed(true)
                .ValidateTransactionClassFieldDisplayed(true)
                .ValidateGrossAmountFieldDisplayed(true)
                .ValidateTransactionTypePicklistDisplayed(true)
                .ValidateInformationOnlyOptionsDisplayed(true)
                .ValidateInvoiceNumberFieldDisplayed(true)
                .ValidateInvoiceStatusFieldDisplayed(true)
                .ValidateProviderLinkFieldDisplayed(true)
                .ValidateServiceElement1LinkFieldDisplayed(true)
                .ValidateWhoToPayPicklistFieldDisplayed(true)
                .ValidateRateUnitLinkFieldDisplayed(true)
                .ValidateTotalUnitsFieldDisplayed(true);

            var financeTransactions = dbHelper.financeTransaction.GetFinanceTransactionByFinanceInvoiceIdAndTransactionClassId(invoiceBatch_InvoiceId[0], 2);
            Assert.AreEqual(1, financeTransactions.Count);

            financeTransactionRecordPage
                .ClickBackButton();

            additionalTransactionsFinanceTransactionsPage
                .WaitForAdditionalTransactionsFinanceTransactionsPageToLoad()
                .SelectAdditionalTransactionsFinanceTransactionsRecord(financeTransactions[0].ToString())
                .ClickDeleteRecordButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Are you sure you wish to delete this record?  This action will also delete any Interest Transaction record associated to it")
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("1 item(s) deleted.")
                .TapOKButton();

            additionalTransactionsFinanceTransactionsPage
                .WaitForAdditionalTransactionsFinanceTransactionsPageToLoad()
                .ValidateNoRecordsMessageVisible();

            financeTransactions = dbHelper.financeTransaction.GetFinanceTransactionByFinanceInvoiceIdAndTransactionClassId(invoiceBatch_InvoiceId[0], 2);
            Assert.AreEqual(0, financeTransactions.Count);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1781

        [TestProperty("JiraIssueID", "CDV6-25515")]
        [Description("Test automation for Step 12 to 14 from CDV6-2996: Verifying the generation and update of Finance Invoice for Supplier Payments")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void FinanceInvoice_UITestMethod003()
        {
            #region step 12 to 14

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
                .InsertTextOnRunontime(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnPeriodEndDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ClickRunInvoiceBatchRecordButton()
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ClickInvoicesTab();

            dbHelper.provider.UpdateCreditorNumberField(_providerId, "PRV123");
            var invoiceIds = dbHelper.financeInvoice.GetFinanceInvoiceByInvoiceBatchId(invoiceBatchId);
            Assert.AreEqual(1, invoiceIds.Count);
            string _invoiceNumber = (string)dbHelper.financeInvoice.GetFinanceInvoiceById(invoiceIds[0], "invoicenumber")["invoicenumber"];

            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad(invoiceBatchId.ToString())
                .OpenRecord(invoiceIds[0].ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .InsertTextOnProviderInvoiceNumberField("PrvIn101")
                .InsertTextOnInvoiceReceivedDateField(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickValuesVerifiedYesOption()
                .ClickSaveButton()
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateProviderInvoiceNumberFieldText("PrvIn101")
                .ValidateInvoiceDateFieldText(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickAdditionalTransactionsTab();

            additionalTransactionsFinanceTransactionsPage
                .WaitForAdditionalTransactionsFinanceTransactionsPageToLoad()
                .ClickNewRecordButton();

            financeTransactionRecordPage
                .WaitForFinanceTransactionRecordPageToLoad()
                .InsertTextOnStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnEndDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnNetAmount("10")
                .InsertTextOnVatAmount("10")
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

            var _financeTransactions = dbHelper.financeTransaction.GetFinanceTransactionByFinanceInvoiceIdAndTransactionClassId(invoiceIds[0], 2);
            Assert.AreEqual(1, _financeTransactions.Count);

            financeTransactionRecordPage
                .ClickBackButton();

            additionalTransactionsFinanceTransactionsPage
                .WaitForAdditionalTransactionsFinanceTransactionsPageToLoad()
                .ValidateRecordIsPresent(_financeTransactions[0].ToString(), true);

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickFinanceTransactionsTab();

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad(invoiceIds[0].ToString())
                .SelectAvailableViewByText("Additional Transactions")
                .ValidateRecordPresent(_financeTransactions[0].ToString());

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber)
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToFinanceTransactionsPage();

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad(_personId.ToString())
                .SelectAvailableViewByText("Additional Transactions")
                .ValidateRecordPresent(_financeTransactions[0].ToString());

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

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad(_providerId.ToString())
                .SelectAvailableViewByText("Additional Transactions")
                .ValidateRecordPresent(_financeTransactions[0].ToString());

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicesSection();

            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad()
                .SearchFinanceInvoiceByInvoiceNumber(_invoiceNumber)
                .OpenRecord(invoiceIds[0].ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad(invoiceIds[0].ToString())
                .ValidateInvoiceStatusFieldText("Newly Created")
                .ClickReadyToAuthoriseButton()
                .WaitForFinanceInvoiceRecordPageToLoad();

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusFieldText("Ready for Authorisation")
                .ValidateAuthorisationErrorFieldText("")
                .ClickAdditionalTransactionsTab();

            additionalTransactionsFinanceTransactionsPage
                .WaitForAdditionalTransactionsFinanceTransactionsPageToLoad()
                .ValidateRecordIsPresent(_financeTransactions[0].ToString(), true)
                .ValidateDeleteRecordButtonIsDisplayed(false)
                .ValidateNewRecordButtonIsDisplayed(false);

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickBackButton();

            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad()
                .SelectFinanceInvoiceRecord(invoiceIds[0].ToString())
                .ClickAuthoriseButton()
                .WaitForFinanceInvoicesPageToLoad();

            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad()
                .SearchFinanceInvoiceByInvoiceNumber(_invoiceNumber)
                .OpenRecord(invoiceIds[0].ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad(invoiceIds[0].ToString())
                .ValidateInvoiceStatusFieldText("Authorised")
                .ValidateAuthorisationErrorFieldText("")
                .ClickAdditionalTransactionsTab();

            additionalTransactionsFinanceTransactionsPage
                .WaitForAdditionalTransactionsFinanceTransactionsPageToLoad()
                .ValidateRecordIsPresent(_financeTransactions[0].ToString(), true)
                .ValidateDeleteRecordButtonIsDisplayed(false)
                .ValidateNewRecordButtonIsDisplayed(false);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1782

        [TestProperty("JiraIssueID", "CDV6-25516")]
        [Description("Test automation for Step 15 to 23 from CDV6-2996: Verifying the generation and update of Finance Invoice for Supplier Payments")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void FinanceInvoice_UITestMethod004()
        {
            #region step 15 to 23

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

            dbHelper.financeInvoiceBatch.UpdateFinanceInvoiceBatchDates(invoiceBatchId, DateTime.Now.AddDays(1).Date, DateTime.Now.AddDays(1).Date);
            decimal netamount = 5m;
            decimal vatamount = 0m;
            decimal grossamount = 5m;
            string providerorpersonidtablename = "provider";
            Guid financeInvoiceId = dbHelper.financeInvoice.CreateFinanceInvoice(_teamId, invoiceBatchId, _financeInvoiceStatusId_newlyCreated, _serviceElement1Id, _paymentTypeCodeId, null, _providerId, providerorpersonidtablename, _providerName, DateTime.Now.AddDays(1).Date, DateTime.Now.AddDays(1).Date, netamount, vatamount, grossamount, false);
            string _invoiceNumber = (string)dbHelper.financeInvoice.GetFinanceInvoiceById(financeInvoiceId, "invoicenumber")["invoicenumber"];

            financeInvoiceBatchRecordPage
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ClickInvoicesTab();

            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad(invoiceBatchId.ToString())
                .OpenRecord(financeInvoiceId.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickReadyToAuthoriseButton()
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusFieldText("Error upon Authorisation")
                .ValidateAuthorisationErrorFieldText("Values Verified has not been set to Yes");

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickValuesVerifiedYesOption()
                .ClickSaveButton()
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickReadyToAuthoriseButton()
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusFieldText("Error upon Authorisation")
                .ValidateAuthorisationErrorFieldText("Provider Invoice Number has not been recorded");

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .InsertTextOnProviderInvoiceNumberField("PrvIn102")
                .ClickSaveButton()
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickReadyToAuthoriseButton()
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusFieldText("Error upon Authorisation")
                .ValidateAuthorisationErrorFieldText("Invoice Received Date has not been recorded");

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .InsertTextOnInvoiceReceivedDateField(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickReadyToAuthoriseButton()
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusFieldText("Error upon Authorisation")
                .ValidateAuthorisationErrorFieldText("The Creditor Reference Number has not been recorded against the Provider");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(_providerName, _providerId.ToString())
                .OpenProviderRecord(_providerId.ToString());

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickDetailsTab()
                .InsertCreditorNoField("Prv123")
                .ClickSaveButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicesSection();

            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad()
                .SearchFinanceInvoiceByInvoiceNumber(_invoiceNumber)
                .OpenRecord(financeInvoiceId.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateCreditorReferenceFieldText("Prv123")
                .ClickInvoiceStatusFieldLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_financeInvoiceStatusId_beingActioned.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickSaveButton()
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusFieldText("Being Actioned")
                .ValidateAuthorisationErrorFieldText("");

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickReadyToAuthoriseButton()
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusFieldText("Ready for Authorisation")
                .ClickBackButton();

            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad()
                .SelectFinanceInvoiceRecord(financeInvoiceId.ToString())
                .ClickAuthoriseButton()
                .WaitForFinanceInvoicesPageToLoad()
                .OpenRecord(financeInvoiceId.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusFieldText("Authorised");

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1784

        [TestProperty("JiraIssueID", "CDV6-25525")]
        [Description("Test automation for Step 27 to 31 from CDV6-2996: Verifying the generation and update of Finance Invoice for Supplier Payments")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void FinanceInvoice_UITestMethod005()
        {
            #region step 27 to 31

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
                .ValidateBatchstatusSelectedText("Completed");

            financeInvoiceBatchRecordPage
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ClickInvoicesTab();

            var _financeInvoiceIds = dbHelper.financeInvoice.GetFinanceInvoiceByInvoiceBatchId(invoiceBatchId);
            Assert.AreEqual(1, _financeInvoiceIds.Count);

            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad(invoiceBatchId.ToString())
                .OpenRecord(_financeInvoiceIds[0].ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusFieldText("Newly Created")
                .ClickFinanceTransactionsTab();

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad(_financeInvoiceIds[0].ToString())
                .ValidateRemoveTransactionsButtonVisible(true)
                .ValidateMoveToNewButtonVisible(true);

            var _financeTransactions = dbHelper.financeTransaction.GetFinanceTransactionByInvoiceID(_financeInvoiceIds[0]);
            Assert.IsTrue(_financeTransactions.Count.Equals(4));

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad(_financeInvoiceIds[0].ToString())
                .ValidateRecordPresent(_financeTransactions[0].ToString())
                .ValidateRecordPresent(_financeTransactions[1].ToString())
                .ValidateRecordPresent(_financeTransactions[2].ToString())
                .ValidateRecordPresent(_financeTransactions[3].ToString())
                .SelectFinanceTransactionRecord(_financeTransactions[0].ToString())
                .ClickRemoveButton()
                .WaitForFinanceTransactionsPageToLoad(_financeInvoiceIds[0].ToString())
                .ValidateRecordNotPresent(_financeTransactions[0].ToString());

            financeTransactionsPage
                .SelectFinanceTransactionRecord(_financeTransactions[1].ToString())
                .SelectFinanceTransactionRecord(_financeTransactions[2].ToString())
                .ClickMoveToNewButton()
                .WaitForFinanceTransactionsPageToLoad(_financeInvoiceIds[0].ToString())
                .ValidateRecordNotPresent(_financeTransactions[1].ToString())
                .ValidateRecordNotPresent(_financeTransactions[2].ToString())
                .ValidateRecordPresent(_financeTransactions[3].ToString());

            var newInvoiceId = dbHelper.financeInvoice.GetFinanceInvoiceByInvoiceBatchId(invoiceBatchId)[0];
            string _newInvoiceNumber = (string)dbHelper.financeInvoice.GetFinanceInvoiceById(newInvoiceId, "invoicenumber")["invoicenumber"];

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicesSection();

            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad()
                .SearchFinanceInvoiceByInvoiceNumber(_newInvoiceNumber)
                .OpenRecord(newInvoiceId.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad(newInvoiceId.ToString())
                .ValidateInvoiceStatusFieldText("Newly Created")
                .ClickFinanceTransactionsTab();

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoad(newInvoiceId.ToString())
                .ValidateRecordPresent(_financeTransactions[1].ToString())
                .ValidateRecordPresent(_financeTransactions[2].ToString());

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1783

        [TestProperty("JiraIssueID", "CDV6-25550")]
        [Description("Test automation for Step 24 to 26 from CDV6-2996: Verifying the generation and update of Finance Invoice for Supplier Payments")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void FinanceInvoice_UITestMethod006()
        {
            #region System user without Authorisation Level for Finance Invoices
            dbHelper = new Phoenix.DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var _systemUserName2 = "FIUser2_" + currentDate;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_systemUserName2, "FI", "User2_" + currentDate, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Provider, Service Provided
            var _providerName2 = "ACC1783_" + currentDate;
            var _providerId2 = commonMethodsDB.CreateProvider(_providerName2, _teamId, 2);
            dbHelper.provider.UpdateCreditorNumberField(_providerId, "CN1783");
            var _serviceProvidedId2 = dbHelper.serviceProvided.CreateServiceProvided(_teamId, _systemUserId2, _providerId2, _serviceElement1Id, _serviceElement2Id, null, null, null, 2);

            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_teamId, _serviceProvidedId2, _paymentTypeCodeId, _vatCodeId, _providerBatchGroupingId, new DateTime(2019, 1, 1), null, 0);

            var ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, _serviceProvidedId2, validRateUnits[0], new DateTime(2020, 1, 1), null, 1);

            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, ratePeriodId1, _serviceProvidedId2, 10m, 15m, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), true, true, true,
                true, true, true, true, true);

            dbHelper.serviceProvidedRatePeriod.UpdateApprovalStatus(ratePeriodId1, 2);
            dbHelper.serviceProvided.UpdateStatus(_serviceProvidedId2, 2);

            #endregion

            #region Service Provision
            plannedStartDate1 = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-28);
            var _serviceProvisionId2 = dbHelper.serviceProvision.CreateServiceProvision(_teamId, _systemUserId2, _personId, Draft_Serviceprovisionstatusid,
                _serviceElement1Id, _serviceElement2Id, null, null, validRateUnits[0], _serviceprovisionstartreasonid, null,
                _teamId, _serviceProvidedId2, _providerId2, _systemUserId2, _placementRoomTypeId, plannedStartDate1, null, null, null, null);

            #endregion

            #region Service Delivery

            dbHelper.serviceDelivery.CreateServiceDelivery(_teamId, _personId, _serviceProvisionId2, validRateUnits[0], 1, 1, true, true, true, true, true, true, true, true, "");

            #endregion

            #region Authorise Service Provision
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId2, ReadyForAuthorisation_Serviceprovisionstatusid);
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId2, Authorised_Serviceprovisionstatusid);

            #endregion

            #region step 24 to 26

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName2, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToInvoiceBatchesSection();

            var invoiceBatchId = dbHelper.financeInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(FinanceInvoiceBatchSetupId)[0];

            financeInvoiceBatchesPage
                .WaitForFinanceInvoiceBatchSetupPageToLoad()
                .InsertSearchQuery("*" + _serviceElement1IdName + "*")
                .TapSearchButton()
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
                .ValidateBatchstatusSelectedText("Completed");

            financeInvoiceBatchRecordPage
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ClickInvoicesTab();

            var _financeInvoiceIds = dbHelper.financeInvoice.GetFinanceInvoiceByInvoiceBatchId(invoiceBatchId);
            Assert.AreEqual(1, _financeInvoiceIds.Count);
            Guid _financeInvoiceId1 = _financeInvoiceIds[0];

            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad(invoiceBatchId.ToString())
                .OpenRecord(_financeInvoiceId1.ToString());

            string _invoiceNumber = (string)dbHelper.financeInvoice.GetFinanceInvoiceById(_financeInvoiceId1, "invoicenumber")["invoicenumber"];

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickValuesVerifiedYesOption()
                .InsertTextOnProviderInvoiceNumberField("PIn1783")
                .InsertTextOnInvoiceReceivedDateField(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickReadyToAuthoriseButton()
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusFieldText("Ready for Authorisation");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicesSection();

            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad()
                .SearchFinanceInvoiceByInvoiceNumber(_invoiceNumber)
                .SelectFinanceInvoiceRecord(_financeInvoiceId1.ToString())
                .ClickAuthoriseButton()
                .WaitForFinanceInvoicesPageToLoad()
                .OpenRecord(_financeInvoiceId1.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusLookupButtonDisabled(false)
                .ValidateInvoiceStatusFieldText("Error upon Authorisation")
                .ValidateAuthorisationErrorFieldText("The Authoriser does not have rights to authorise Invoices")
                .ClickReadyToAuthoriseButton()
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusFieldText("Ready for Authorisation");

            var authorisationLevelExist = dbHelper.authorisationLevel.GetBySystemUserId(_systemUserId2, 7).Any();
            if (!authorisationLevelExist)
                dbHelper.authorisationLevel.CreateAuthorisationLevel(_teamId, _systemUserId2, new DateTime(2020, 1, 1), 7, 1m, true, true);

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickBackButton();

            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad()
                .SelectFinanceInvoiceRecord(_financeInvoiceId1.ToString())
                .ClickAuthoriseButton()
                .OpenRecord(_financeInvoiceId1.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusLookupButtonDisabled(false)
                .ValidateInvoiceStatusFieldText("Error upon Authorisation")
                .ValidateAuthorisationErrorFieldText("The Authoriser does not have rights to authorise Invoices with this Net Amount value")
                .ClickReadyToAuthoriseButton()
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusFieldText("Ready for Authorisation");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickSignOutButton();

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicesSection();

            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad()
                .SearchFinanceInvoiceByInvoiceNumber(_invoiceNumber)
                .SelectFinanceInvoiceRecord(_financeInvoiceId1.ToString())
                .ClickAuthoriseButton()
                .WaitForFinanceInvoicesPageToLoad()
                .OpenRecord(_financeInvoiceId1.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusLookupButtonDisabled(true)
                .ValidateInvoiceStatusFieldText("Authorised")
                .ValidateAuthorisationErrorFieldText("");
            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1785

        [TestProperty("JiraIssueID", "CDV6-25559")]
        [Description("Test automation for Step 32 to 41 from CDV6-2996: Verifying the generation and update of Finance Invoice for Supplier Payments")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void FinanceInvoice_UITestMethod007()
        {
            #region Provider, Service Provided
            dbHelper = new Phoenix.DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var _providerName2 = "ACC1785_" + currentDate;
            var _providerId2 = commonMethodsDB.CreateProvider(_providerName2, _teamId, 2);
            dbHelper.provider.UpdateCreditorNumberField(_providerId, "CN1785");
            var _serviceProvidedId2 = dbHelper.serviceProvided.CreateServiceProvided(_teamId, _systemUserId, _providerId2, _serviceElement1Id, _serviceElement2Id, null, null, null, 2);

            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_teamId, _serviceProvidedId2, _paymentTypeCodeId, _vatCodeId, _providerBatchGroupingId, new DateTime(2019, 1, 1), null, 0);

            var ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, _serviceProvidedId2, validRateUnits[0], new DateTime(2020, 1, 1), null, 1);

            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, ratePeriodId1, _serviceProvidedId2, 10m, 15m, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), true, true, true,
                true, true, true, true, true);

            dbHelper.serviceProvidedRatePeriod.UpdateApprovalStatus(ratePeriodId1, 2);
            dbHelper.serviceProvided.UpdateStatus(_serviceProvidedId2, 2);

            #endregion

            #region Service Provision
            plannedStartDate1 = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-28);
            var _serviceProvisionId2 = dbHelper.serviceProvision.CreateServiceProvision(_teamId, _systemUserId, _personId, Draft_Serviceprovisionstatusid,
                _serviceElement1Id, _serviceElement2Id, null, null, validRateUnits[0], _serviceprovisionstartreasonid, null,
                _teamId, _serviceProvidedId2, _providerId2, _systemUserId, _placementRoomTypeId, plannedStartDate1, null, null, null, null);

            #endregion

            #region Service Delivery

            dbHelper.serviceDelivery.CreateServiceDelivery(_teamId, _personId, _serviceProvisionId2, validRateUnits[0], 1, 1, true, true, true, true, true, true, true, true, "");

            #endregion

            #region Authorise Service Provision
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId2, ReadyForAuthorisation_Serviceprovisionstatusid);
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId2, Authorised_Serviceprovisionstatusid);

            #endregion

            #region step 32 to 41

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToInvoiceBatchesSection();

            var invoiceBatchId = dbHelper.financeInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(FinanceInvoiceBatchSetupId)[0];
            dbHelper.financeInvoiceBatch.UpdateFinanceInvoiceBatchDates(invoiceBatchId, DateTime.Now.Date, DateTime.Now.Date);

            financeInvoiceBatchesPage
                .WaitForFinanceInvoiceBatchSetupPageToLoad()
                .InsertSearchQuery("*" + _serviceElement1IdName + "*")
                .TapSearchButton()
                .OpenRecord(invoiceBatchId.ToString());

            financeInvoiceBatchRecordPage
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ValidateBatchstatusSelectedText("New")
                .ClickSaveButton()
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ClickRunInvoiceBatchRecordButton()
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ClickInvoicesTab();

            var _financeInvoiceIds = dbHelper.financeInvoice.GetFinanceInvoiceByInvoiceBatchId(invoiceBatchId);
            Assert.AreEqual(1, _financeInvoiceIds.Count);
            Guid _financeInvoiceId1 = _financeInvoiceIds[0];

            dbHelper.financeInvoice.UpdateProviderInvoiceNumber(_financeInvoiceId1, "PIn1785");
            dbHelper.financeInvoice.UpdateInvoiceReceivedDate(_financeInvoiceId1, DateTime.Now.Date);

            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad(invoiceBatchId.ToString())
                .OpenRecord(_financeInvoiceId1.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickValuesVerifiedYesOption()
                .ClickSaveButton()
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickInvoiceStatusFieldLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_financeInvoiceStatusId_beingActioned.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickSaveButton()
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusFieldText("Being Actioned")
                .NavigateToAuditPage();

            var auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 2, //update operation
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = _financeInvoiceId1.ToString(),
                ParentTypeName = "FinanceInvoice",
                RecordsPerPage = "50",
                ViewType = "0",
                AllowMultiSelect = "false",
                ViewGroup = "1",
                Year = "Last 90 Days",
                IsGeneralAuditSearch = false,
                UsePaging = true,
                PageNumber = 1
            };

            WebAPIHelper.Security.Authenticate();
            var auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);
            Assert.AreEqual("Update", auditResponseData.GridData[0].cols[0].Text);
            Assert.AreEqual("FinanceInvoice User1", auditResponseData.GridData[0].cols[1].Text);

            var auditRecordId1 = auditResponseData.GridData[0].Id;

            auditListPage
                .WaitForAuditListPageToLoad("financeinvoice")
                .ValidateRecordPresent(auditRecordId1)
                .ClickOnAuditRecord(auditRecordId1);

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Update")
                .ValidateChangedBy("FinanceInvoice User1")
                .ValidateOldValueCellText(1, "Newly Created")
                .ValidateNewValueCellText(1, "Being Actioned")
                .TapCloseButton();

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickDetailsTab()
                .ClickInvoiceStatusFieldLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_financeInvoiceStatusId_held.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickSaveButton()
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusFieldText("Held")
                .NavigateToAuditPage();

            auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 2, //update operation
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = _financeInvoiceId1.ToString(),
                ParentTypeName = "FinanceInvoice",
                RecordsPerPage = "50",
                ViewType = "0",
                AllowMultiSelect = "false",
                ViewGroup = "1",
                Year = "Last 90 Days",
                IsGeneralAuditSearch = false,
                UsePaging = true,
                PageNumber = 1
            };

            WebAPIHelper.Security.Authenticate();
            auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);
            Assert.AreEqual("Update", auditResponseData.GridData[0].cols[0].Text);
            Assert.AreEqual("FinanceInvoice User1", auditResponseData.GridData[0].cols[1].Text);

            var auditRecordId2 = auditResponseData.GridData[0].Id;

            auditListPage
                .WaitForAuditListPageToLoad("financeinvoice")
                .ValidateRecordPresent(auditRecordId2)
                .ClickOnAuditRecord(auditRecordId2);

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Update")
                .ValidateChangedBy("FinanceInvoice User1")
                .ValidateOldValueCellText(1, "Being Actioned")
                .ValidateNewValueCellText(1, "Held")
                .TapCloseButton();

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickDetailsTab()
                .ClickInvoiceStatusFieldLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_financeInvoiceStatusId_completed.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickSaveButton()
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusFieldText("Completed")
                .NavigateToAuditPage();

            auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 2, //update operation
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = _financeInvoiceId1.ToString(),
                ParentTypeName = "FinanceInvoice",
                RecordsPerPage = "50",
                ViewType = "0",
                AllowMultiSelect = "false",
                ViewGroup = "1",
                Year = "Last 90 Days",
                IsGeneralAuditSearch = false,
                UsePaging = true,
                PageNumber = 1
            };

            WebAPIHelper.Security.Authenticate();
            auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);
            Assert.AreEqual("Update", auditResponseData.GridData[0].cols[0].Text);
            Assert.AreEqual("FinanceInvoice User1", auditResponseData.GridData[0].cols[1].Text);

            var auditRecordId3 = auditResponseData.GridData[0].Id;

            auditListPage
                .WaitForAuditListPageToLoad("financeinvoice")
                .ValidateRecordPresent(auditRecordId3)
                .ClickOnAuditRecord(auditRecordId3);

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Update")
                .ValidateChangedBy("FinanceInvoice User1")
                .ValidateOldValueCellText(1, "Held")
                .ValidateNewValueCellText(1, "Completed")
                .TapCloseButton();

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickDetailsTab()
                .ClickInvoiceStatusFieldLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_financeInvoiceStatusId_newlyCreated.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickSaveButton()
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusFieldText("Newly Created")
                .ClickReadyToAuthoriseButton()
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusFieldText("Ready for Authorisation")
                .NavigateToAuditPage();

            auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 2, //update operation
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = _financeInvoiceId1.ToString(),
                ParentTypeName = "FinanceInvoice",
                RecordsPerPage = "50",
                ViewType = "0",
                AllowMultiSelect = "false",
                ViewGroup = "1",
                Year = "Last 90 Days",
                IsGeneralAuditSearch = false,
                UsePaging = true,
                PageNumber = 1
            };

            WebAPIHelper.Security.Authenticate();
            auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);
            Assert.AreEqual("Update", auditResponseData.GridData[0].cols[0].Text);
            Assert.AreEqual("FinanceInvoice User1", auditResponseData.GridData[0].cols[1].Text);

            var auditRecordId4 = auditResponseData.GridData[0].Id;

            auditListPage
                .WaitForAuditListPageToLoad("financeinvoice")
                .ValidateRecordPresent(auditRecordId4)
                .ClickOnAuditRecord(auditRecordId4);

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Update")
                .ValidateChangedBy("FinanceInvoice User1")
                .ValidateOldValueCellText(1, "Newly Created")
                .ValidateNewValueCellText(1, "Ready for Authorisation")
                .TapCloseButton();

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1786

        [TestProperty("JiraIssueID", "CDV6-25560")]
        [Description("Test automation for Step 42 to 47 from CDV6-2996: Verifying the generation and update of Finance Invoice for Supplier Payments")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void FinanceInvoice_UITestMethod008()
        {
            #region Provider, Service Provided
            dbHelper = new Phoenix.DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var _providerName2 = "ACC1786_" + currentDate;
            var _providerId2 = commonMethodsDB.CreateProvider(_providerName2, _teamId, 2);
            dbHelper.provider.UpdateCreditorNumberField(_providerId, "CN1786");
            var _serviceProvidedId2 = dbHelper.serviceProvided.CreateServiceProvided(_teamId, _systemUserId, _providerId2, _serviceElement1Id, _serviceElement2Id, null, null, null, 2);

            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_teamId, _serviceProvidedId2, _paymentTypeCodeId, _vatCodeId, _providerBatchGroupingId, new DateTime(2019, 1, 1), null, 0);

            var ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, _serviceProvidedId2, validRateUnits[0], new DateTime(2020, 1, 1), null, 1);

            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, ratePeriodId1, _serviceProvidedId2, 10m, 15m, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), true, true, true,
                true, true, true, true, true);

            dbHelper.serviceProvidedRatePeriod.UpdateApprovalStatus(ratePeriodId1, 2);
            dbHelper.serviceProvided.UpdateStatus(_serviceProvidedId2, 2);

            #endregion

            #region Service Provision
            plannedStartDate1 = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-28);
            var _serviceProvisionId2 = dbHelper.serviceProvision.CreateServiceProvision(_teamId, _systemUserId, _personId, Draft_Serviceprovisionstatusid,
                _serviceElement1Id, _serviceElement2Id, null, null, validRateUnits[0], _serviceprovisionstartreasonid, null,
                _teamId, _serviceProvidedId2, _providerId2, _systemUserId, _placementRoomTypeId, plannedStartDate1, null, null, null, null);

            #endregion

            #region Service Delivery

            dbHelper.serviceDelivery.CreateServiceDelivery(_teamId, _personId, _serviceProvisionId2, validRateUnits[0], 1, 1, true, true, true, true, true, true, true, true, "");

            #endregion

            #region Authorise Service Provision
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId2, ReadyForAuthorisation_Serviceprovisionstatusid);
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId2, Authorised_Serviceprovisionstatusid);

            #endregion

            #region step 42 to 47

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToInvoiceBatchesSection();

            var invoiceBatchId = dbHelper.financeInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(FinanceInvoiceBatchSetupId)[0];
            dbHelper.financeInvoiceBatch.UpdateFinanceInvoiceBatchDates(invoiceBatchId, DateTime.Now, DateTime.Now);

            financeInvoiceBatchesPage
                .WaitForFinanceInvoiceBatchSetupPageToLoad()
                .InsertSearchQuery("*" + _serviceElement1IdName + "*")
                .TapSearchButton()
                .OpenRecord(invoiceBatchId.ToString());

            financeInvoiceBatchRecordPage
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ValidateBatchstatusSelectedText("New")
                .ClickSaveButton()
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ClickRunInvoiceBatchRecordButton()
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ClickInvoicesTab();

            var _financeInvoiceIds = dbHelper.financeInvoice.GetFinanceInvoiceByInvoiceBatchId(invoiceBatchId);
            Assert.AreEqual(1, _financeInvoiceIds.Count);
            Guid _financeInvoiceId1 = _financeInvoiceIds[0];
            string _invoiceNumber = (string)dbHelper.financeInvoice.GetFinanceInvoiceById(_financeInvoiceId1, "invoicenumber")["invoicenumber"];

            dbHelper.financeInvoice.UpdateProviderInvoiceNumber(_financeInvoiceId1, "PIn1786");
            dbHelper.financeInvoice.UpdateInvoiceReceivedDate(_financeInvoiceId1, DateTime.Now);
            dbHelper.financeInvoice.SetValuesVerifiedField(_financeInvoiceId1, true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicesSection();

            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad()
                .SearchFinanceInvoiceByInvoiceNumber(_invoiceNumber)
                .SelectFinanceInvoiceRecord(_financeInvoiceId1.ToString())
                .ClickReadyToAuthoriseButton()
                .WaitForFinanceInvoicesPageToLoad()
                .SelectFinanceInvoiceRecord(_financeInvoiceId1.ToString())
                .ClickAuthoriseButton()
                .WaitForFinanceInvoicesPageToLoad()
                .OpenRecord(_financeInvoiceId1.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusLookupButtonDisabled(true)
                .ValidateInvoiceStatusFieldText("Authorised")
                .NavigateToAuditPage();

            var auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 2, //update operation
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = _financeInvoiceId1.ToString(),
                ParentTypeName = "FinanceInvoice",
                RecordsPerPage = "50",
                ViewType = "0",
                AllowMultiSelect = "false",
                ViewGroup = "1",
                Year = "Last 90 Days",
                IsGeneralAuditSearch = false,
                UsePaging = true,
                PageNumber = 1
            };

            WebAPIHelper.Security.Authenticate();
            var auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);
            Assert.AreEqual("Update", auditResponseData.GridData[0].cols[0].Text);
            Assert.AreEqual("FinanceInvoice User1", auditResponseData.GridData[0].cols[1].Text);

            var auditRecordId1 = auditResponseData.GridData[0].Id;

            auditListPage
                .WaitForAuditListPageToLoad("financeinvoice")
                .ValidateRecordPresent(auditRecordId1)
                .ClickOnAuditRecord(auditRecordId1);

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Update")
                .ValidateChangedBy("FinanceInvoice User1")
                .ValidateOldValueCellText(1, "Ready for Authorisation")
                .ValidateNewValueCellText(1, "Authorised")
                .TapCloseButton();

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1787

        [TestProperty("JiraIssueID", "CDV6-25570")]
        [Description("Test automation for Step 48 to 57 from CDV6-2996: Verifying the generation and update of Finance Invoice for Supplier Payments")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void FinanceInvoice_UITestMethod009()
        {
            #region Provider, Service Provided
            dbHelper = new Phoenix.DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var _providerName2 = "ACC1787_" + currentDate;
            var _providerId2 = commonMethodsDB.CreateProvider(_providerName2, _teamId, 2);
            dbHelper.provider.UpdateCreditorNumberField(_providerId, "CN1787");
            var _serviceProvidedId2 = dbHelper.serviceProvided.CreateServiceProvided(_teamId, _systemUserId, _providerId2, _serviceElement1Id, _serviceElement2Id, null, null, null, 2);

            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_teamId, _serviceProvidedId2, _paymentTypeCodeId, _vatCodeId, _providerBatchGroupingId, new DateTime(2019, 1, 1), null, 0);

            var ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, _serviceProvidedId2, validRateUnits[0], new DateTime(2020, 1, 1), null, 1);

            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, ratePeriodId1, _serviceProvidedId2, 10m, 15m, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), true, true, true,
                true, true, true, true, true);

            dbHelper.serviceProvidedRatePeriod.UpdateApprovalStatus(ratePeriodId1, 2);
            dbHelper.serviceProvided.UpdateStatus(_serviceProvidedId2, 2);

            #endregion

            #region Service Provision
            plannedStartDate1 = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-28);
            var _serviceProvisionId2 = dbHelper.serviceProvision.CreateServiceProvision(_teamId, _systemUserId, _personId, Draft_Serviceprovisionstatusid,
                _serviceElement1Id, _serviceElement2Id, null, null, validRateUnits[0], _serviceprovisionstartreasonid, null,
                _teamId, _serviceProvidedId2, _providerId2, _systemUserId, _placementRoomTypeId, plannedStartDate1, null, null, null, null);

            #endregion

            #region Service Delivery

            dbHelper.serviceDelivery.CreateServiceDelivery(_teamId, _personId, _serviceProvisionId2, validRateUnits[0], 1, 1, true, true, true, true, true, true, true, true, "");

            #endregion

            #region Authorise Service Provision
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId2, ReadyForAuthorisation_Serviceprovisionstatusid);
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId2, Authorised_Serviceprovisionstatusid);

            #endregion

            #region step 48 to 57

            dbHelper.financeInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(FinanceInvoiceBatchSetupId);

            #region Step 53
            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToInvoiceBatchesSection();

            var invoiceBatchId = dbHelper.financeInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(FinanceInvoiceBatchSetupId);
            dbHelper.financeInvoiceBatch.UpdateFinanceInvoiceBatchDates(invoiceBatchId[0], DateTime.Now, DateTime.Now);

            financeInvoiceBatchesPage
                .WaitForFinanceInvoiceBatchSetupPageToLoad()
                .InsertSearchQuery("*" + _serviceElement1IdName + "*")
                .TapSearchButton()
                .OpenRecord(invoiceBatchId[0].ToString());

            financeInvoiceBatchRecordPage
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ValidateBatchstatusSelectedText("New")
                .ClickSaveButton()
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ClickRunInvoiceBatchRecordButton()
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ClickInvoicesTab();

            var _financeInvoiceIds = dbHelper.financeInvoice.GetFinanceInvoiceByInvoiceBatchId(invoiceBatchId[0]);
            Assert.AreEqual(1, _financeInvoiceIds.Count);

            Guid _financeInvoiceId1 = _financeInvoiceIds[0];
            dbHelper.financeInvoice.UpdateInvoiceReceivedDate(_financeInvoiceId1, DateTime.Now);
            dbHelper.financeInvoice.SetValuesVerifiedField(_financeInvoiceId1, true);

            string _invoiceNumber = (string)dbHelper.financeInvoice.GetFinanceInvoiceById(_financeInvoiceId1, "invoicenumber")["invoicenumber"];

            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad(invoiceBatchId[0].ToString())
                .OpenRecord(_financeInvoiceId1.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusFieldText("Newly Created")
                .ClickReadyToAuthoriseButton()
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusFieldText("Error upon Authorisation")
                .ValidateAuthorisationErrorFieldText("Provider Invoice Number has not been recorded");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Finance Invoices")
                .SelectFilter("1", "Invoice Status")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_financeInvoiceStatusId_errorUponAuthorisation.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Invoice Number")
                .SelectOperator("2", "Equals")
                .InsertRuleValueText("2", _invoiceNumber);

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_financeInvoiceId1.ToString())
                .OpenRecord(_financeInvoiceId1.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoadFromAdvancedSearch(_financeInvoiceId1.ToString())
                .InsertTextOnInvoiceDateField(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForFinanceInvoiceRecordPageToLoadFromAdvancedSearch(_financeInvoiceId1.ToString())
                .ValidateInvoiceDateFieldDisabled(false)
                .ValidateInvoiceDateFieldText(DateTime.Now.ToString("dd'/'MM'/'yyyy"));

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicesSection();

            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad()
                .SearchFinanceInvoiceByInvoiceNumber(_invoiceNumber)
                .OpenRecord(_financeInvoiceId1.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickInvoiceStatusFieldLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_financeInvoiceStatusId_newlyCreated.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ClickSaveButton()
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusFieldText("Newly Created")
                .ValidateAuthorisationErrorFieldText("");

            dbHelper.financeInvoice.UpdateProviderInvoiceNumber(_financeInvoiceId1, "PIn1787");

            #endregion

            #region Step 48 - 49
            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Finance Invoices")
                .SelectFilter("1", "Inactive")
                .SelectOperator("1", "Equals")
                .SelectPicklistRuleValue("1", "No");

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Invoice Number")
                .SelectOperator("2", "Equals")
                .InsertRuleValueText("2", _invoiceNumber);

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_financeInvoiceId1.ToString())
                .OpenRecord(_financeInvoiceId1.ToString());

            #endregion

            #region Step 50

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoadFromAdvancedSearch(_financeInvoiceId1.ToString())
                .InsertTextOnInvoiceDateField(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForFinanceInvoiceRecordPageToLoadFromAdvancedSearch(_financeInvoiceId1.ToString())
                .ValidateInvoiceDateFieldDisabled(false)
                .ValidateInvoiceDateFieldText(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"));
            #endregion

            #region Step 54
            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Finance Invoices")
                .SelectFilter("1", "Invoice Status")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_financeInvoiceStatusId_newlyCreated.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Invoice Number")
                .SelectOperator("2", "Equals")
                .InsertRuleValueText("2", _invoiceNumber);

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_financeInvoiceId1.ToString())
                .OpenRecord(_financeInvoiceId1.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoadFromAdvancedSearch(_financeInvoiceId1.ToString())
                .InsertTextOnInvoiceDateField(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForFinanceInvoiceRecordPageToLoadFromAdvancedSearch(_financeInvoiceId1.ToString())
                .ValidateInvoiceDateFieldDisabled(false)
                .ValidateInvoiceDateFieldText(DateTime.Now.ToString("dd'/'MM'/'yyyy"));

            #endregion

            #region Step 51
            dbHelper.financeInvoice.UpdateInvoiceStatus(_financeInvoiceId1, _financeInvoiceStatusId_beingActioned);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Finance Invoices")
                .SelectFilter("1", "Invoice Status")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_financeInvoiceStatusId_beingActioned.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Invoice Number")
                .SelectOperator("2", "Equals")
                .InsertRuleValueText("2", _invoiceNumber);

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_financeInvoiceId1.ToString())
                .OpenRecord(_financeInvoiceId1.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoadFromAdvancedSearch(_financeInvoiceId1.ToString())
                .InsertTextOnInvoiceDateField(DateTime.Now.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForFinanceInvoiceRecordPageToLoadFromAdvancedSearch(_financeInvoiceId1.ToString())
                .ValidateInvoiceDateFieldDisabled(false)
                .ValidateInvoiceDateFieldText(DateTime.Now.AddDays(-1).ToString("dd'/'MM'/'yyyy"));
            #endregion

            #region Step 55
            dbHelper.financeInvoice.UpdateInvoiceStatus(_financeInvoiceId1, _financeInvoiceStatusId_held);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Finance Invoices")
                .SelectFilter("1", "Invoice Status")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_financeInvoiceStatusId_held.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Invoice Number")
                .SelectOperator("2", "Equals")
                .InsertRuleValueText("2", _invoiceNumber);

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_financeInvoiceId1.ToString())
                .OpenRecord(_financeInvoiceId1.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoadFromAdvancedSearch(_financeInvoiceId1.ToString())
                .InsertTextOnInvoiceDateField(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForFinanceInvoiceRecordPageToLoadFromAdvancedSearch(_financeInvoiceId1.ToString())
                .ValidateInvoiceDateFieldDisabled(false)
                .ValidateInvoiceDateFieldText(DateTime.Now.ToString("dd'/'MM'/'yyyy"));

            #endregion

            #region Step 52
            dbHelper.financeInvoice.UpdateInvoiceStatus(_financeInvoiceId1, _financeInvoiceStatusId_completed);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Finance Invoices")
                .SelectFilter("1", "Invoice Status")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_financeInvoiceStatusId_completed.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Invoice Number")
                .SelectOperator("2", "Equals")
                .InsertRuleValueText("2", _invoiceNumber);

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_financeInvoiceId1.ToString())
                .OpenRecord(_financeInvoiceId1.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoadFromAdvancedSearch(_financeInvoiceId1.ToString())
                .InsertTextOnInvoiceDateField(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForFinanceInvoiceRecordPageToLoadFromAdvancedSearch(_financeInvoiceId1.ToString())
                .ValidateInvoiceDateFieldDisabled(false)
                .ValidateInvoiceDateFieldText(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"));
            #endregion

            #region Step 56 - 57
            dbHelper.financeInvoice.UpdateInvoiceStatus(_financeInvoiceId1, _financeInvoiceStatusId_readyForAuthorisation);
            dbHelper.financeInvoice.UpdateInvoiceStatus(_financeInvoiceId1, _financeInvoiceStatusId_authorised);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Finance Invoices")
                .SelectFilter("1", "Invoice Status")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_financeInvoiceStatusId_authorised.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Invoice Number")
                .SelectOperator("2", "Equals")
                .InsertRuleValueText("2", _invoiceNumber);

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_financeInvoiceId1.ToString())
                .OpenRecord(_financeInvoiceId1.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoadFromAdvancedSearch(_financeInvoiceId1.ToString())
                .ValidateInvoiceDateFieldDisabled(true);
            #endregion

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1788

        [TestProperty("JiraIssueID", "CDV6-25578")]
        [Description("Test automation for Step 58 to 59 from CDV6-2996: Verifying the generation and update of Finance Invoice for Supplier Payments")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void FinanceInvoice_UITestMethod010()
        {
            #region Finance Extract Setup  
            dbHelper = new Phoenix.DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var _financeExtractSetupId = commonMethodsDB.CreateFinanceExtractSetupIfNeeded(_teamId, _businessUnitId, 2, new DateTime(2019, 1, 1), new TimeSpan(1, 0, 0),
                _extractNameId, 1, true, true, true, true, true, true, true, 1, false, false, true);

            #endregion

            #region Finance Extract
            var _financeExtractId = dbHelper.financeExtract.GetFinanceExtractByFinanceExtractSetupId(_financeExtractSetupId, 1)[0];

            #endregion

            #region Provider, Service Provided
            var _providerName2 = "ACC1788_" + currentDate;
            var _providerId2 = commonMethodsDB.CreateProvider(_providerName2, _teamId, 2);
            dbHelper.provider.UpdateCreditorNumberField(_providerId, "CN1788");
            var _serviceProvidedId2 = dbHelper.serviceProvided.CreateServiceProvided(_teamId, _systemUserId, _providerId2, _serviceElement1Id, _serviceElement2Id, null, null, null, 2);

            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_teamId, _serviceProvidedId2, _paymentTypeCodeId, _vatCodeId, _providerBatchGroupingId, new DateTime(2019, 1, 1), null, 0);

            var ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, _serviceProvidedId2, validRateUnits[0], new DateTime(2020, 1, 1), null, 1);

            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, ratePeriodId1, _serviceProvidedId2, 10m, 15m, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), true, true, true,
                true, true, true, true, true);

            dbHelper.serviceProvidedRatePeriod.UpdateApprovalStatus(ratePeriodId1, 2);
            dbHelper.serviceProvided.UpdateStatus(_serviceProvidedId2, 2);

            #endregion

            #region Service Provision
            plannedStartDate1 = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-28);
            var _serviceProvisionId2 = dbHelper.serviceProvision.CreateServiceProvision(_teamId, _systemUserId, _personId, Draft_Serviceprovisionstatusid,
                _serviceElement1Id, _serviceElement2Id, null, null, validRateUnits[0], _serviceprovisionstartreasonid, null,
                _teamId, _serviceProvidedId2, _providerId2, _systemUserId, _placementRoomTypeId, plannedStartDate1, null, null, null, null);

            #endregion

            #region Service Delivery

            dbHelper.serviceDelivery.CreateServiceDelivery(_teamId, _personId, _serviceProvisionId2, validRateUnits[0], 1, 1, true, true, true, true, true, true, true, true, "");

            #endregion

            #region Authorise Service Provision
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId2, ReadyForAuthorisation_Serviceprovisionstatusid);
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId2, Authorised_Serviceprovisionstatusid);

            #endregion

            #region step 58 to 59

            dbHelper.financeInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(FinanceInvoiceBatchSetupId);

            #region Step 58
            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToInvoiceBatchesSection();

            var invoiceBatchId = dbHelper.financeInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(FinanceInvoiceBatchSetupId);
            dbHelper.financeInvoiceBatch.UpdateFinanceInvoiceBatchDates(invoiceBatchId[0], DateTime.Now, DateTime.Now);

            financeInvoiceBatchesPage
                .WaitForFinanceInvoiceBatchSetupPageToLoad()
                .InsertSearchQuery("*" + _serviceElement1IdName + "*")
                .TapSearchButton()
                .OpenRecord(invoiceBatchId[0].ToString());

            financeInvoiceBatchRecordPage
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ValidateBatchstatusSelectedText("New")
                .ClickSaveButton()
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ClickRunInvoiceBatchRecordButton()
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ClickInvoicesTab();

            var _financeInvoiceIds = dbHelper.financeInvoice.GetFinanceInvoiceByInvoiceBatchId(invoiceBatchId[0]);
            Assert.AreEqual(1, _financeInvoiceIds.Count);

            Guid _financeInvoiceId1 = _financeInvoiceIds[0];
            dbHelper.financeInvoice.UpdateProviderInvoiceNumber(_financeInvoiceId1, "PIn1788");
            dbHelper.financeInvoice.UpdateInvoiceReceivedDate(_financeInvoiceId1, DateTime.Now);
            dbHelper.financeInvoice.SetValuesVerifiedField(_financeInvoiceId1, true);

            string _invoiceNumber = (string)dbHelper.financeInvoice.GetFinanceInvoiceById(_financeInvoiceId1, "invoicenumber")["invoicenumber"];

            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad(invoiceBatchId[0].ToString())
                .OpenRecord(_financeInvoiceId1.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusFieldText("Newly Created")
                .ClickReadyToAuthoriseButton()
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusFieldText("Ready for Authorisation");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Finance Invoices")
                .SelectFilter("1", "Invoice Status")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_financeInvoiceStatusId_readyForAuthorisation.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Invoice Number")
                .SelectOperator("2", "Equals")
                .InsertRuleValueText("2", _invoiceNumber);

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_financeInvoiceId1.ToString())
                .OpenRecord(_financeInvoiceId1.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoadFromAdvancedSearch(_financeInvoiceId1.ToString())
                .ValidateInvoiceDateFieldDisabled(true);

            #endregion

            #region Step 59

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicesSection();

            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad()
                .SearchFinanceInvoiceByInvoiceNumber(_invoiceNumber)
                .SelectFinanceInvoiceRecord(_financeInvoiceId1.ToString())
                .ClickAuthoriseButton()
                .WaitForFinanceInvoicesPageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Finance Invoices")
                .SelectFilter("1", "Invoice Status")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_financeInvoiceStatusId_authorised.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Invoice Number")
                .SelectOperator("2", "Equals")
                .InsertRuleValueText("2", _invoiceNumber);

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_financeInvoiceId1.ToString())
                .OpenRecord(_financeInvoiceId1.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoadFromAdvancedSearch(_financeInvoiceId1.ToString())
                .ValidateInvoiceDateFieldDisabled(true);

            #endregion

            #region Execute 'Process Finance Extracts' scheduled job
            dbHelper.financeExtract.UpdateFinanceExtractRunOnDate(_financeExtractId, DateTime.Now);
            var _authorisedInvoices = dbHelper.financeInvoice.GetFinanceInvoiceByInvoiceStatusId(_financeInvoiceStatusId_authorised);

            //Get the schedule job id
            Guid processFinanceExtractsJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Process Finance Extracts")[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(processFinanceExtractsJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(processFinanceExtractsJobId);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Finance Invoices")
                .SelectFilter("1", "Invoice Status")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_financeInvoiceStatusId_extracted.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Invoice Number")
                .SelectOperator("2", "Equals")
                .InsertRuleValueText("2", _invoiceNumber);

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_financeInvoiceId1.ToString())
                .OpenRecord(_financeInvoiceId1.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoadFromAdvancedSearch(_financeInvoiceId1.ToString())
                .ValidateInvoiceStatusFieldText("Extracted")
                .ValidateInvoiceDateFieldDisabled(true);

            var _extractedInvoices = dbHelper.financeInvoice.GetFinanceInvoiceByInvoiceStatusIdAndFinanceExtractId(_financeInvoiceStatusId_extracted, _financeExtractId);
            Assert.AreEqual(_authorisedInvoices.Count, _extractedInvoices.Count);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1789

        [TestProperty("JiraIssueID", "CDV6-25590")]
        [Description("Test automation for Step 60 to 68 from CDV6-2996: Verifying the generation and update of Finance Invoice for Supplier Payments")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void FinanceInvoice_UITestMethod011()
        {
            #region Finance Extract Setup  
            dbHelper = new Phoenix.DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var _financeExtractSetupId = commonMethodsDB.CreateFinanceExtractSetupIfNeeded(_teamId, _businessUnitId, 2, new DateTime(2019, 1, 1), new TimeSpan(1, 0, 0),
                _extractNameId, 1, true, true, true, true, true, true, true, 1, false, false, true);

            #endregion

            #region Finance Extract
            var _financeExtractId = dbHelper.financeExtract.GetFinanceExtractByFinanceExtractSetupId(_financeExtractSetupId, 1)[0];

            #endregion

            #region Provider, Service Provided
            var _providerName2 = "ACC1789_" + currentDate;
            var _providerId2 = commonMethodsDB.CreateProvider(_providerName2, _teamId, 2);
            dbHelper.provider.UpdateCreditorNumberField(_providerId, "CN1789");
            var _serviceProvidedId2 = dbHelper.serviceProvided.CreateServiceProvided(_teamId, _systemUserId, _providerId2, _serviceElement1Id, _serviceElement2Id, null, null, null, 2);

            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_teamId, _serviceProvidedId2, _paymentTypeCodeId, _vatCodeId, _providerBatchGroupingId, new DateTime(2019, 1, 1), null, 0);

            var ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, _serviceProvidedId2, validRateUnits[0], new DateTime(2020, 1, 1), null, 1);

            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, ratePeriodId1, _serviceProvidedId2, 10m, 15m, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), true, true, true,
                true, true, true, true, true);

            dbHelper.serviceProvidedRatePeriod.UpdateApprovalStatus(ratePeriodId1, 2);
            dbHelper.serviceProvided.UpdateStatus(_serviceProvidedId2, 2);

            #endregion

            #region Service Provision
            plannedStartDate1 = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-28);
            var _serviceProvisionId2 = dbHelper.serviceProvision.CreateServiceProvision(_teamId, _systemUserId, _personId, Draft_Serviceprovisionstatusid,
                _serviceElement1Id, _serviceElement2Id, null, null, validRateUnits[0], _serviceprovisionstartreasonid, null,
                _teamId, _serviceProvidedId2, _providerId2, _systemUserId, _placementRoomTypeId, plannedStartDate1, null, null, null, null);

            #endregion

            #region Service Delivery

            dbHelper.serviceDelivery.CreateServiceDelivery(_teamId, _personId, _serviceProvisionId2, validRateUnits[0], 1, 1, true, true, true, true, true, true, true, true, "");

            #endregion

            #region Authorise Service Provision
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId2, ReadyForAuthorisation_Serviceprovisionstatusid);
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId2, Authorised_Serviceprovisionstatusid);

            #endregion

            #region step 60 to 68            

            #region Step 60
            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToInvoiceBatchesSection();

            var invoiceBatchId = dbHelper.financeInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(FinanceInvoiceBatchSetupId);
            dbHelper.financeInvoiceBatch.UpdateFinanceInvoiceBatchDates(invoiceBatchId[0], DateTime.Now, DateTime.Now);

            financeInvoiceBatchesPage
                .WaitForFinanceInvoiceBatchSetupPageToLoad()
                .InsertSearchQuery("*" + _serviceElement1IdName + "*")
                .TapSearchButton()
                .OpenRecord(invoiceBatchId[0].ToString());

            financeInvoiceBatchRecordPage
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ValidateBatchstatusSelectedText("New")
                .ClickSaveButton()
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ClickRunInvoiceBatchRecordButton()
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ClickInvoicesTab();

            var _financeInvoiceIds = dbHelper.financeInvoice.GetFinanceInvoiceByInvoiceBatchId(invoiceBatchId[0]);
            Assert.AreEqual(1, _financeInvoiceIds.Count);

            Guid _financeInvoiceId1 = _financeInvoiceIds[0];
            dbHelper.financeInvoice.UpdateProviderInvoiceNumber(_financeInvoiceId1, "PIn1789");
            dbHelper.financeInvoice.UpdateInvoiceReceivedDate(_financeInvoiceId1, DateTime.Now);
            dbHelper.financeInvoice.SetValuesVerifiedField(_financeInvoiceId1, true);

            string _invoiceNumber = (string)dbHelper.financeInvoice.GetFinanceInvoiceById(_financeInvoiceId1, "invoicenumber")["invoicenumber"];

            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad(invoiceBatchId[0].ToString())
                .OpenRecord(_financeInvoiceId1.ToString());

            financeInvoiceRecordPage
                .WaitForFinanceInvoiceRecordPageToLoad()
                .ValidateInvoiceStatusFieldText("Newly Created")
                .ClickReadyToAuthoriseButton()
                .WaitForFinanceInvoiceRecordPageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoicesSection();

            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad()
                .SearchFinanceInvoiceByInvoiceNumber(_invoiceNumber)
                .SelectFinanceInvoiceRecord(_financeInvoiceId1.ToString())
                .ClickAuthoriseButton()
                .WaitForFinanceInvoicesPageToLoad();

            #region Execute 'Process Finance Extracts' scheduled job
            dbHelper.financeExtract.UpdateFinanceExtractRunOnDate(_financeExtractId, DateTime.Now);
            //var _authorisedInvoices = dbHelper.financeInvoice.GetFinanceInvoiceByInvoiceStatusId(_financeInvoiceStatusId_authorised);

            //Get the schedule job id
            Guid processFinanceExtractsJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Process Finance Extracts")[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(processFinanceExtractsJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(processFinanceExtractsJobId);

            #endregion

            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad()
                .SelectSystemView("All")
                .WaitForFinanceInvoicesPageToLoad()
                .RecordsPageValidateHeaderCellText(3, "Provider")
                .RecordsPageValidateHeaderCellText(4, "Person")
                .RecordsPageValidateHeaderCellText(5, "Payee");
            #endregion                                    

            #region Step 61
            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad()
                .SelectSystemView("Supplier Payments")
                .WaitForFinanceInvoicesPageToLoad()
                .RecordsPageValidateHeaderCellText(2, "Provider")
                .RecordsPageValidateHeaderCellText(3, "Person")
                .RecordsPageValidateHeaderCellText(4, "Service Element 1");

            #endregion

            #region Step 62

            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad()
                .ClickPersonFieldLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("All Active People")
                .TypeSearchQuery(_personFullname)
                .TapSearchButton()
                .ClickAddSelectedButton(_personId.ToString());

            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .ClickSearchButton()
                .WaitForFinanceInvoicesPageToLoad()
                .ValidateRecordIsPresent(_financeInvoiceId1.ToString(), true);

            #endregion

            #region Step 63
            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExtractsSection();

            financeExtractsPage
                .WaitForFinanceInvoiceBatchSetupPageToLoad()
                .SelectSystemView("Finance Extract - Supplier Payments")
                .ClickColumnHeader(2)
                .ResultsPageValidateHeaderCellSortAscendingOrder(2)
                .ClickColumnHeaderSortDescendingOrder(2)
                .OpenRecord(_financeExtractId.ToString());

            #endregion

            #region Step 64
            financeExtractRecordPage
                .WaitForFinanceExtractRecordPageToLoad()
                .ClickFinanceInvoicesTab();

            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad(_financeExtractId.ToString())
                .SelectSystemView("Finance Extract Supplier Payments")
                .WaitForFinanceInvoicesPageToLoad(_financeExtractId.ToString())
                .RecordsPageValidateHeaderCellText(2, "Provider")
                .RecordsPageValidateHeaderCellText(3, "Person")
                .RecordsPageValidateHeaderCellText(4, "Service Element 1");

            #endregion

            #region Step 65
            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad(_financeExtractId.ToString())
                .InsertSearchQuery(_personFullname)
                .TapSearchButton()
                .ValidateRecordIsPresent(_financeInvoiceId1.ToString(), true);

            #endregion

            #region Step 66
            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToInvoiceBatchesSection();

            financeInvoiceBatchesPage
                .WaitForFinanceInvoiceBatchSetupPageToLoad()
                .SelectSystemView("Finance Invoice Batch – Supplier Payments")
                .WaitForFinanceInvoiceBatchSetupPageToLoad()
                .ClickColumnHeader(5)
                .ResultsPageValidateHeaderCellSortAscendingOrder(5)
                .ClickColumnHeaderSortDescendingOrder(5)
                .OpenRecord(invoiceBatchId[0].ToString());

            financeInvoiceBatchRecordPage
                .WaitForFinanceInvoiceBatchRecordPageToLoad()
                .ClickInvoicesTab();

            #endregion

            #region Step 67
            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad(invoiceBatchId[0].ToString())
                .SelectSystemView("Finance Invoice Batch Supplier Payments")
                .WaitForFinanceInvoicesPageToLoad(invoiceBatchId[0].ToString())
                .RecordsPageValidateHeaderCellText(2, "Provider")
                .RecordsPageValidateHeaderCellText(3, "Person")
                .RecordsPageValidateHeaderCellText(4, "Service Element 1");

            #endregion

            #region Step 68
            financeInvoicesPage
                .WaitForFinanceInvoicesPageToLoad(invoiceBatchId[0].ToString())
                .InsertSearchQuery(_personFullname)
                .TapSearchButton()
                .ValidateRecordIsPresent(_financeInvoiceId1.ToString(), true);

            #endregion

            #endregion
        }

        #endregion

    }
}

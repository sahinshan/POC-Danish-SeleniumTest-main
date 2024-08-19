using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.UITests.Providers
{
    [TestClass]
    public class ServiceFinanceSettings_UITestCases : FunctionalTest
    {
        #region Properties

        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private string _systemUserName;
        private Guid _systemUserId;
        private Guid _languageId;
        private Guid _serviceElement1Id;
        private Guid _serviceElement2Id;
        private string _serviceElement1IdName;
        private string _serviceElement2IdName;
        private Guid _providerId;
        private int _providerNumber;
        private string _providerName;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");
        private string _paymentTypeName;
        private Guid _paymentTypeId;
        private string _vatCodeName;
        private Guid _vatCodeId;
        private string _providerBatchGroupingName;
        private Guid _providerBatchGroupingId;
        private Guid _serviceProvidedId;
        private Guid _extractNameId;
        private Guid _invoiceById;
        private Guid _invoiceFrequencyId;

        #endregion

        [TestInitialize()]
        public void TestsSetupMethod()
        {
            try
            {

                #region Business Unit

                _careDirectorQA_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

                #endregion

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Team

                _careDirectorQA_TeamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Default System User 

                _systemUserName = "ServiceFinanceSettingUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ServiceFinanceSettings", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion

                #region Provider - Supplier

                _providerName = "Provider_" + _currentDateSuffix;
                _providerId = commonMethodsDB.CreateProvider(_providerName, _careDirectorQA_TeamId, 2, _providerName + "@test.com");
                _providerNumber = (int)dbHelper.provider.GetProviderByID(_providerId, "providernumber")["providernumber"];

                #endregion

                #region Service Element 1 & 2

                var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
                var code2 = commonMethodsHelper.GetRandomValue(1, 2147483647);
                _serviceElement1IdName = "Service_Provided_SE1";
                _serviceElement2IdName = "Service_Provided_SE2";

                var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();
                var validRateUnits = new List<Guid>();
                validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());

                validRateUnits.Add(defaulRateUnitID);
                if (!dbHelper.serviceElement1.GetByName(_serviceElement1IdName).Any())
                    _serviceElement1Id = commonMethodsDB.CreateServiceElement1(_serviceElement1IdName, _careDirectorQA_TeamId, new DateTime(2022, 1, 1), code, 1, 1, validRateUnits, defaulRateUnitID);
                _serviceElement1Id = dbHelper.serviceElement1.GetByName(_serviceElement1IdName)[0];

                if (!dbHelper.serviceElement2.GetByName(_serviceElement2IdName).Any())
                    dbHelper.serviceElement2.CreateServiceElement2(_careDirectorQA_TeamId, _serviceElement2IdName, new DateTime(2022, 1, 1), code);
                _serviceElement2Id = dbHelper.serviceElement2.GetByName(_serviceElement2IdName)[0];

                commonMethodsDB.CreateServiceMapping(_careDirectorQA_TeamId, _serviceElement1Id, _serviceElement2Id);

                #endregion

                #region Payment Type Code

                _paymentTypeName = "Payment_" + _currentDateSuffix;
                _paymentTypeId = commonMethodsDB.CreatePaymentTypeCode(_paymentTypeName, new DateTime(2023, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region VAT Code

                _vatCodeName = "VAT_" + _currentDateSuffix;
                _vatCodeId = commonMethodsDB.CreateVATCode(_vatCodeName, new DateTime(2023, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Provider Batch Grouping

                _providerBatchGroupingName = "Batching_" + _currentDateSuffix;
                _providerBatchGroupingId = commonMethodsDB.CreateProviderBatchGrouping(_providerBatchGroupingName, new DateTime(2023, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Service Provided

                _serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, null, null, null, null, 1, false);

                #endregion

                #region Extract Name

                _extractNameId = dbHelper.extractName.GetByName("Generic Supplier Payments Extract").FirstOrDefault();

                #endregion

                #region Invoice By

                _invoiceById = dbHelper.invoiceBy.GetByName("Provider\\Purchasing Team\\Client")[0];

                #endregion

                #region Invoice Frequency

                _invoiceFrequencyId = dbHelper.invoiceFrequency.GetByName("Every Week")[0];

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Close();

                throw;
            }

        }

        #region https://advancedcsg.atlassian.net/browse/ACC-681

        [TestProperty("JiraIssueID", "ACC-756")]
        [Description("Steps 1 to 5 & 7 from CDV6-3214")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ServiceFinanceSettings_UITestMethod01()
        {
            string currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("dd'/'MM'/'yyyy");
            string pastDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-2).ToString("dd'/'MM'/'yyyy");
            string futureDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(2).ToString("dd'/'MM'/'yyyy");

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad();

            #endregion

            #region Step 3

            mainMenu
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(_providerNumber.ToString(), _providerId.ToString())
                .OpenProviderRecord(_providerId.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .TapServicesProvidedTab();

            servicesProvidedPage
                .WaitForServicesProvidedPageToLoad()
                .OpenServiceProvidedRecord(_serviceProvidedId.ToString());

            serviceProvidedRecordPage
               .WaitForServiceProvidedRecordPageToLoad()
               .NavigateToServiceFinanceSettingsTab();

            serviceFinanceSettingsPage
                .WaitForServiceFinanceSettingsPageToLoad()
                .ClickNewRecordButton();

            serviceFinanceSettingRecordPage
                .WaitForServiceFinanceSettingRecordPageToLoad()
                .ClickSaveButton()

                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.")
                .ValidatePaymentTypeNotificationMessageText("Please fill out this field.")
                .ValidateProviderBatchGroupingNotificationMessageText("Please fill out this field.")
                .ValidateVATCodeNotificationMessageText("Please fill out this field.")
                .ValidateAdjustedDaysNotificationMessageText("Please fill out this field.");

            #endregion

            #region Step 4

            serviceFinanceSettingRecordPage
                .InsertTextOnStartdate(futureDate);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The start date must be the current date or earlier.")
                .TapOKButton();

            serviceFinanceSettingRecordPage
                .WaitForServiceFinanceSettingRecordPageToLoad()
                .ValidateStartdateText(currentDate);

            #endregion

            #region Step 2 & 5

            serviceFinanceSettingRecordPage
                .InsertTextOnStartdate(pastDate)
                .ClickPaymentTypeLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery(_paymentTypeName)
               .TapSearchButton()
               .SelectResultElement(_paymentTypeId.ToString());

            serviceFinanceSettingRecordPage
                .WaitForServiceFinanceSettingRecordPageToLoad()
                .ClickProviderBatchGroupingLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery(_providerBatchGroupingName)
               .TapSearchButton()
               .SelectResultElement(_providerBatchGroupingId.ToString());

            serviceFinanceSettingRecordPage
                .WaitForServiceFinanceSettingRecordPageToLoad()
                .ClickVATCodeLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery(_vatCodeName)
               .TapSearchButton()
               .SelectResultElement(_vatCodeId.ToString());

            serviceFinanceSettingRecordPage
                .WaitForServiceFinanceSettingRecordPageToLoad()
                .InsertTextOnAdjustedDays("1")
                .ClickSaveButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Finance Transactions will only be created from the Start Date recorded.  Do you wish to continue?")
                .TapOKButton();

            serviceFinanceSettingRecordPage
                .WaitForServiceFinanceSettingRecordPageToLoad()
                .WaitForRecordToBeSaved();

            #endregion

            #region Step 7

            serviceFinanceSettingRecordPage
                .ValidateResponsibleTeamLookupDisabled(true)
                .ValidateStartDateFieldDisabled(true)
                .ValidateEndDateFieldDisabled(true)
                .ValidatePaymentTypeLookupDisabled(true)
                .ValidateProviderBatchGroupingLookupDisabled(true)
                .ValidateVATCodeLookupDisabled(true)
                .ValidateAdjustedDaysFieldDisabled(true)
                .ValidateEndReasonRulesApplyOptionsDisabled(true)
                .ValidateUsedInBatchSetupOptionsDisabled(true)
                .ValidateNotesFieldDisabled(false);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-758")]
        [Description("8. Validate user able to Delete Service Finance Settings record." +
                     "9. Verify that once the Finance Invoice Batch setup record has been created, then field “Used in Batch Setup?” should be set to Yes." +
                     "10. Verify that if there is any update to the Finance Service Settings record and if there is no longer a record on the Finance Invoice Batch setup screen " +
                     "With the combination: Service Element 1 / Payment Type / Provider Batch Grouping, then the field “Used in Batch Setup?” should be set to No.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ServiceFinanceSettings_UITestMethod02()
        {
            #region Service Finance Settings

            var serviceFinanceSettingsId = dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_careDirectorQA_TeamId, _serviceProvidedId, _paymentTypeId, _vatCodeId, _providerBatchGroupingId, new DateTime(2022, 1, 1), null, 0);

            #endregion

            #region Step 8

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(_providerNumber.ToString(), _providerId.ToString())
                .OpenProviderRecord(_providerId.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .TapServicesProvidedTab();

            servicesProvidedPage
                .WaitForServicesProvidedPageToLoad()
                .OpenServiceProvidedRecord(_serviceProvidedId.ToString());

            serviceProvidedRecordPage
               .WaitForServiceProvidedRecordPageToLoad()
               .NavigateToServiceFinanceSettingsTab();

            serviceFinanceSettingsPage
                .WaitForServiceFinanceSettingsPageToLoad()
                .OpenServiceFinanceSettingsRecord(serviceFinanceSettingsId.ToString());

            serviceFinanceSettingRecordPage
                .WaitForServiceFinanceSettingRecordPageToLoad()
                .ValidateUsedInBatchSetup_OptionChecked(false)
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            serviceFinanceSettingsPage
                .WaitForServiceFinanceSettingsPageToLoad()
                .ClickRefreshButton()
                .ValidateRecordNotPresent(serviceFinanceSettingsId.ToString());

            #endregion

            #region Step 9

            var serviceFinanceSettingsId2 = dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_careDirectorQA_TeamId, _serviceProvidedId, _paymentTypeId, _vatCodeId, _providerBatchGroupingId, new DateTime(2022, 1, 1), null, 0);

            serviceFinanceSettingsPage
                .ClickRefreshButton()
                .ValidateRecordPresent(serviceFinanceSettingsId2.ToString())
                .OpenServiceFinanceSettingsRecord(serviceFinanceSettingsId2.ToString());

            serviceFinanceSettingRecordPage
                .WaitForServiceFinanceSettingRecordPageToLoad()
                .ValidateUsedInBatchSetup_OptionChecked(false)
                .ValidateUsedInBatchSetupOptionsDisabled(true);

            Guid FinanceInvoiceBatchSetupId = dbHelper.financeInvoiceBatchSetup.CreateFinanceInvoiceBatchSetup(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "", _extractNameId, 2, _invoiceById, _invoiceFrequencyId, _paymentTypeId, _providerBatchGroupingId, 3, _serviceElement1Id, new DateTime(2023, 12, 12), true, true, true);

            serviceFinanceSettingRecordPage
                .ClickBackButton();

            serviceFinanceSettingsPage
                .WaitForServiceFinanceSettingsPageToLoad()
                .OpenServiceFinanceSettingsRecord(serviceFinanceSettingsId2.ToString());

            serviceFinanceSettingRecordPage
                .WaitForServiceFinanceSettingRecordPageToLoad()
                .ValidateUsedInBatchSetup_OptionChecked(true)
                .ValidateUsedInBatchSetupOptionsDisabled(true);

            #endregion

            #region Step 10

            dbHelper.financeInvoiceBatchSetup.DeleteFinanceInvoiceBatchSetupId(FinanceInvoiceBatchSetupId);

            serviceFinanceSettingRecordPage
                .ClickBackButton();

            serviceFinanceSettingsPage
                .WaitForServiceFinanceSettingsPageToLoad()
                .OpenServiceFinanceSettingsRecord(serviceFinanceSettingsId2.ToString());

            serviceFinanceSettingRecordPage
                .WaitForServiceFinanceSettingRecordPageToLoad()
                .ValidateUsedInBatchSetup_OptionChecked(false)
                .ValidateUsedInBatchSetupOptionsDisabled(true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-777")]
        [Description("11, 12, 13 - Validate Charge Using Number Of Carers? Options with Default 'No' options." +
                    "Validate Charge Using Number Of Carers? option is read only after Used in Batch Setup? = Yes" +
                    "Validate 'VAT Apply To Charging' is read only once 'Used In Batch Setup = Yes")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ServiceFinanceSettings_UITestMethod03()
        {
            #region Service Finance Settings

            var serviceFinanceSettingsId = dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_careDirectorQA_TeamId, _serviceProvidedId, _paymentTypeId, _vatCodeId, _providerBatchGroupingId, new DateTime(2022, 1, 1), null, 0);

            #endregion

            #region Step 11

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(_providerNumber.ToString(), _providerId.ToString())
                .OpenProviderRecord(_providerId.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .TapServicesProvidedTab();

            servicesProvidedPage
                .WaitForServicesProvidedPageToLoad()
                .OpenServiceProvidedRecord(_serviceProvidedId.ToString());

            serviceProvidedRecordPage
               .WaitForServiceProvidedRecordPageToLoad()
               .NavigateToServiceFinanceSettingsTab();

            serviceFinanceSettingsPage
                .WaitForServiceFinanceSettingsPageToLoad()
                .OpenServiceFinanceSettingsRecord(serviceFinanceSettingsId.ToString());

            serviceFinanceSettingRecordPage
                .WaitForServiceFinanceSettingRecordPageToLoad()
                .ValidateChargeUsingNumberOfCarersOptionsDisabled(false)
                .ValidateChargeUsingNumberOfCarers_OptionChecked(false)
                .ValidateVATApplyToChargingOptionsDisabled(false)
                .ValidateVATApplyCharging_OptionChecked(false)
                .ValidateUsedInBatchSetup_OptionChecked(false);

            #endregion

            #region Step 12 & 13

            dbHelper.financeInvoiceBatchSetup.CreateFinanceInvoiceBatchSetup(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "", _extractNameId, 2, _invoiceById, _invoiceFrequencyId, _paymentTypeId, _providerBatchGroupingId, 3, _serviceElement1Id, new DateTime(2023, 12, 12), true, true, true);

            serviceFinanceSettingRecordPage
                .ClickBackButton();

            serviceFinanceSettingsPage
                .WaitForServiceFinanceSettingsPageToLoad()
                .OpenServiceFinanceSettingsRecord(serviceFinanceSettingsId.ToString());

            serviceFinanceSettingRecordPage
                .WaitForServiceFinanceSettingRecordPageToLoad()
                .ValidateChargeUsingNumberOfCarersOptionsDisabled(true)
                .ValidateChargeUsingNumberOfCarers_OptionChecked(false)
                .ValidateVATApplyToChargingOptionsDisabled(true)
                .ValidateVATApplyCharging_OptionChecked(false)
                .ValidateUsedInBatchSetup_OptionChecked(true);

            #endregion

        }

        #endregion

        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}

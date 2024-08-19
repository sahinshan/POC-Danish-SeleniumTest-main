using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People.Related_Items
{
    [TestClass]
    public class PersonContractService_JB_UITestCases : FunctionalTest
    {
        #region Private Properties

        private Guid _authenticationproviderid;
        private string _defaultSystemUserFullName;
        private Guid _languageId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private string _teamName;
        private Guid _ethnicityId;
        private Guid _systemUserId;
        private string _systemUserName;
        private string _systemUserFullName;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");

        #endregion


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

                string user = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(user)[0];
                _defaultSystemUserFullName = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(defaultSystemUserId, "fullname")["fullname"];

                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("PCSRP BU1");

                #endregion

                #region Team

                _teamName = "PCSRP T1";
                _teamId = commonMethodsDB.CreateTeam(_teamName, null, _businessUnitId, "907678", "PCSRPT1@careworkstempmail.com", _teamName, "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

                #endregion

                #region Create SystemUser Record

                _systemUserName = "PersonContractServicesUser1";
                _systemUserFullName = "PersonContractServices User1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "PersonContractServices", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-4796

        [TestProperty("JiraIssueID", "ACC-8169")]
        [Description("Step(s) 1 from the original test ACC-856 - Contract Service > Negotiated Rates Apply = Yes")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("BusinessModule2", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Person Contract Services")]
        public void PersonContractServiceRatePeriod_ACC_6617_UITestMethod001()
        {
            #region Provider

            var providerName = "Provider " + _currentDateSuffix;
            var providerType = 13; //Residential Establishment
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, providerType, false);

            var funderProviderName = "Funder Provider " + _currentDateSuffix;
            var funderProviderType = 10; //Local Authority
            var funderProviderId = commonMethodsDB.CreateProvider(funderProviderName, _teamId, funderProviderType, false);

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitName = "Default Care Provider Rate Unit";
            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, _careProviderRateUnitName, new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingName = "Default Care Provider Batch Grouping";
            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, _careProviderBatchGroupingName, new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var _careProviderVATCodeName = "Standard Rated";
            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName(_careProviderVATCodeName)[0];

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "CPCS " + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, funderProviderId);

            #endregion

            #region Care Provider Service

            var _careProviderServiceName = "CPS " + _currentDateSuffix;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var isScheduledService = false;
            var _careProviderServiceId = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, isScheduledService);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailName = "CPSD " + _currentDateSuffix;
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, null, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(_careProviderServiceId, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var roomsApply = false;
            var negotiatedRatesApply = true;
            var permitRateOverride = false;
            var CareProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, funderProviderId, careProviderContractScheme1Id, _careProviderServiceId, careProviderServiceDetailId, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, roomsApply, negotiatedRatesApply, permitRateOverride);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(CareProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Person

            var firstName = "Mathew";
            var lastName = _currentDateSuffix;
            var _person_FullName = firstName + " " + lastName;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Care Provider Person Contract

            var StartDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-13);

            var careProviderPersonContractId_1 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractScheme1Id, funderProviderId, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId_1 = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_1, _teamId,
                careProviderContractScheme1Id, _careProviderServiceId, CareProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId, true);

            #endregion


            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(careProviderPersonContractId_1.ToString());

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId_1.ToString());

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ValidateRatesTabIsVisible(true);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-8170")]
        [Description("Step(s) 1 from the original test ACC-856 - Contract Service > Negotiated Rates Apply = No & Permit Override = Yes & PCS > Override Rate? = Yes")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("BusinessModule2", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Person Contract Services")]
        public void PersonContractServiceRatePeriod_ACC_6617_UITestMethod002()
        {
            #region Provider

            var providerName = "Provider " + _currentDateSuffix;
            var providerType = 13; //Residential Establishment
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, providerType, false);

            var funderProviderName = "Funder Provider " + _currentDateSuffix;
            var funderProviderType = 10; //Local Authority
            var funderProviderId = commonMethodsDB.CreateProvider(funderProviderName, _teamId, funderProviderType, false);

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitName = "Default Care Provider Rate Unit";
            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, _careProviderRateUnitName, new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingName = "Default Care Provider Batch Grouping";
            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, _careProviderBatchGroupingName, new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var _careProviderVATCodeName = "Standard Rated";
            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName(_careProviderVATCodeName)[0];

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "CPCS " + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, funderProviderId);

            #endregion

            #region Care Provider Service

            var _careProviderServiceName = "CPS " + _currentDateSuffix;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var isScheduledService = false;
            var _careProviderServiceId = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, isScheduledService);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailName = "CPSD " + _currentDateSuffix;
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, null, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(_careProviderServiceId, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var roomsApply = false;
            var negotiatedRatesApply = false;
            var permitRateOverride = true;
            var CareProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, funderProviderId, careProviderContractScheme1Id, _careProviderServiceId, careProviderServiceDetailId, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, roomsApply, negotiatedRatesApply, permitRateOverride);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(CareProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Person

            var firstName = "Mathew";
            var lastName = _currentDateSuffix;
            var _person_FullName = firstName + " " + lastName;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Care Provider Person Contract

            var StartDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-13);

            var careProviderPersonContractId_1 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractScheme1Id, funderProviderId, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_1, _teamId,
                careProviderContractScheme1Id, _careProviderServiceId, CareProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId, true);
            dbHelper.careProviderPersonContractService.UpdateOverrideRateAndRateVerified(careProviderPersonContractServiceId, true, true);

            #endregion


            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(careProviderPersonContractId_1.ToString());

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId.ToString());

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ValidateRatesTabIsVisible(true);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-8171")]
        [Description("Step(s) 1 from the original test ACC-856 - Contract Service > Negotiated Rates Apply = No")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("BusinessModule2", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Person Contract Services")]
        public void PersonContractServiceRatePeriod_ACC_6617_UITestMethod003()
        {
            #region Provider

            var providerName = "Provider " + _currentDateSuffix;
            var providerType = 13; //Residential Establishment
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, providerType, false);

            var funderProviderName = "Funder Provider " + _currentDateSuffix;
            var funderProviderType = 10; //Local Authority
            var funderProviderId = commonMethodsDB.CreateProvider(funderProviderName, _teamId, funderProviderType, false);

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitName = "Default Care Provider Rate Unit";
            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, _careProviderRateUnitName, new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingName = "Default Care Provider Batch Grouping";
            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, _careProviderBatchGroupingName, new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var _careProviderVATCodeName = "Standard Rated";
            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName(_careProviderVATCodeName)[0];

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "CPCS " + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, funderProviderId);

            #endregion

            #region Care Provider Service

            var _careProviderServiceName = "CPS " + _currentDateSuffix;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var isScheduledService = false;
            var _careProviderServiceId = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, isScheduledService);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailName = "CPSD " + _currentDateSuffix;
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, null, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(_careProviderServiceId, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var roomsApply = false;
            var negotiatedRatesApply = false;
            var permitRateOverride = false;
            var CareProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, funderProviderId, careProviderContractScheme1Id, _careProviderServiceId, careProviderServiceDetailId, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, roomsApply, negotiatedRatesApply, permitRateOverride);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(CareProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Person

            var firstName = "Mathew";
            var lastName = _currentDateSuffix;
            var _person_FullName = firstName + " " + lastName;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Care Provider Person Contract

            var StartDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-13);

            var careProviderPersonContractId_1 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractScheme1Id, funderProviderId, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId_1 = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(
                careProviderPersonContractId_1, _teamId,
                careProviderContractScheme1Id, _careProviderServiceId, CareProviderContractServiceId, StartDate, 1, 1, _careProviderRateUnitId, false);

            #endregion


            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(careProviderPersonContractId_1.ToString());

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId_1.ToString());

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ValidateRatesTabIsVisible(false);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-8172")]
        [Description("Step(s) 1 from the original test ACC-856 - Contract Service > Negotiated Rates Apply = No & Permit Override = Yes & PCS > Override Rate? = No")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("BusinessModule2", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Person Contract Services")]
        public void PersonContractServiceRatePeriod_ACC_6617_UITestMethod004()
        {
            #region Provider

            var providerName = "Provider " + _currentDateSuffix;
            var providerType = 13; //Residential Establishment
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, providerType, false);

            var funderProviderName = "Funder Provider " + _currentDateSuffix;
            var funderProviderType = 10; //Local Authority
            var funderProviderId = commonMethodsDB.CreateProvider(funderProviderName, _teamId, funderProviderType, false);

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitName = "Default Care Provider Rate Unit";
            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, _careProviderRateUnitName, new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingName = "Default Care Provider Batch Grouping";
            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, _careProviderBatchGroupingName, new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var _careProviderVATCodeName = "Standard Rated";
            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName(_careProviderVATCodeName)[0];

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "CPCS " + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, funderProviderId);

            #endregion

            #region Care Provider Service

            var _careProviderServiceName = "CPS " + _currentDateSuffix;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var isScheduledService = false;
            var _careProviderServiceId = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, isScheduledService);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailName = "CPSD " + _currentDateSuffix;
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, null, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(_careProviderServiceId, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var roomsApply = false;
            var negotiatedRatesApply = false;
            var permitRateOverride = true;
            var CareProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, funderProviderId, careProviderContractScheme1Id, _careProviderServiceId, careProviderServiceDetailId, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, roomsApply, negotiatedRatesApply, permitRateOverride);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(CareProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Person

            var firstName = "Mathew";
            var lastName = _currentDateSuffix;
            var _person_FullName = firstName + " " + lastName;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Care Provider Person Contract

            var StartDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-13);

            var careProviderPersonContractId_1 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractScheme1Id, funderProviderId, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_1, _teamId,
                careProviderContractScheme1Id, _careProviderServiceId, CareProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId, false);
            dbHelper.careProviderPersonContractService.UpdateOverrideRateAndRateVerified(careProviderPersonContractServiceId, false, true);

            #endregion


            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(careProviderPersonContractId_1.ToString());

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId.ToString());

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ValidateRatesTabIsVisible(false);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-8173")]
        [Description("Step(s) 2 to 7 from the original test ACC-856")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("BusinessModule2", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Person Contract Services")]
        public void PersonContractServiceRatePeriod_ACC_6617_UITestMethod005()
        {
            #region Provider

            var providerName = "Provider " + _currentDateSuffix;
            var providerType = 13; //Residential Establishment
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, providerType, false);

            var funderProviderName = "Funder Provider " + _currentDateSuffix;
            var funderProviderType = 10; //Local Authority
            var funderProviderId = commonMethodsDB.CreateProvider(funderProviderName, _teamId, funderProviderType, false);

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitName = "Default Care Provider Rate Unit";
            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, _careProviderRateUnitName, new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingName = "Default Care Provider Batch Grouping";
            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, _careProviderBatchGroupingName, new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var _careProviderVATCodeName = "Standard Rated";
            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName(_careProviderVATCodeName)[0];

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "CPCS " + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, funderProviderId);

            #endregion

            #region Care Provider Service

            var _careProviderServiceName = "CPS " + _currentDateSuffix;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var isScheduledService = false;
            var _careProviderServiceId = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, isScheduledService);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailName = "CPSD " + _currentDateSuffix;
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, null, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(_careProviderServiceId, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var roomsApply = false;
            var negotiatedRatesApply = true;
            var permitRateOverride = false;
            var CareProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, funderProviderId, careProviderContractScheme1Id, _careProviderServiceId, careProviderServiceDetailId, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, roomsApply, negotiatedRatesApply, permitRateOverride);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(CareProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Person

            var firstName = "Mathew";
            var lastName = _currentDateSuffix;
            var _person_FullName = firstName + " " + lastName;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Care Provider Person Contract

            var StartDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-13);

            var careProviderPersonContractId_1 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractScheme1Id, funderProviderId, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId_1 = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_1, _teamId,
                careProviderContractScheme1Id, _careProviderServiceId, CareProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId, true);
            var careProviderPersonContractServiceNumber = dbHelper.careProviderPersonContractService.GetByID(careProviderPersonContractServiceId_1, "careproviderpersoncontractservicenumber")["careproviderpersoncontractservicenumber"].ToString();

            #endregion


            #region Step 2 & 4

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(careProviderPersonContractId_1.ToString());

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId_1.ToString());

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToRatesTab();

            personContractServiceRatePeriodsPage
                .WaitForPersonContractServiceRatePeriodsPageToLoad()
                .ClickNewRecordButton();

            personContractServiceRatePeriodRecordPage
                .WaitForPersonContractServiceRatePeriodRecordPageToLoad()
                .ValidatePageElementsVisible();

            #endregion

            #region Step 3

            var ratePeriodEndDate = StartDate.AddDays(2);
            personContractServiceRatePeriodRecordPage
                .InsertStartDate(StartDate.ToString("dd/MM/yyyy"))
                .InsertEndDate(ratePeriodEndDate.ToString("dd/MM/yyyy"))
                .InsertRate("10.50")
                .InsertTextOnNoteText("test automation")
                .ClickSaveAndCloseButton();

            personContractServiceRatePeriodsPage
                .WaitForPersonContractServiceRatePeriodsPageToLoad()
                .ClickRefreshButton()
                .WaitForPersonContractServiceRatePeriodsPageToLoad();

            var allRecords = dbHelper.careProviderPersonContractServiceRatePeriod.GetByCareProviderPersonContractServiceId(careProviderPersonContractServiceId_1);
            Assert.AreEqual(1, allRecords.Count);
            var careProviderPersonContractServiceRatePeriod1Id = allRecords[0];

            personContractServiceRatePeriodsPage
                .OpenRecord(careProviderPersonContractServiceRatePeriod1Id);

            personContractServiceRatePeriodRecordPage
                .WaitForPersonContractServiceRatePeriodRecordPageToLoad()
                .ValidatePageHeaderText("Mathew " + _currentDateSuffix + " \\ Provider " + _currentDateSuffix + " \\ Funder Provider " + _currentDateSuffix + " \\ CPCS " + _currentDateSuffix + " \\ " + StartDate.ToString("dd/MM/yyyy") + " \\ " + careProviderPersonContractServiceNumber + " \\ Default Care Provider Rate Unit \\ " + StartDate.ToString("dd/MM/yyyy") + " \\ " + ratePeriodEndDate.ToString("dd/MM/yyyy"));

            #endregion

            #region Step 4

            //step 4 is already validated by other automated tests

            #endregion

            #region Step 5

            personContractServiceRatePeriodRecordPage
                .ValidateRateErrorLabelVisibility(false)

                .InsertRate("-1000000")
                .ValidateRateErrorLabelVisibility(true)
                .ValidateRateErrorLabelText("Please enter a value between -999999.99 and 999999.99.")

                .InsertRate("1000000")
                .ValidateRateErrorLabelVisibility(true)
                .ValidateRateErrorLabelText("Please enter a value between -999999.99 and 999999.99.")

                .InsertRate("999999.99")
                .ValidateRateErrorLabelVisibility(false);

            personContractServiceRatePeriodRecordPage
                .InsertEndDate(StartDate.AddDays(-2).ToString("dd/MM/yyyy"));

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("End Date must be equal or after Start Date").TapCloseButton();

            personContractServiceRatePeriodRecordPage
                .WaitForPersonContractServiceRatePeriodRecordPageToLoad();

            #endregion

            #region Step 6 & 7

            personContractServiceRatePeriodRecordPage
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            personContractServiceRatePeriodsPage
                .WaitForPersonContractServiceRatePeriodsPageToLoad()
                .SelectRecord(careProviderPersonContractServiceRatePeriod1Id)
                .ClickCloneButton();

            var clonedRecordStartDate = StartDate.AddDays(3);
            clonePersonContractServiceRatePeriodPopup
                .WaitForPopupToLoad()

                .ValidatePopupHeaderText("Cloning Person Contract Service Rate Period")
                .ValidateStartDateFieldLabelText("Please choose Start Date for cloned record*")

                .InsertStartDate(StartDate.AddDays(-2).ToString("dd/MM/yyyy"))
                .ClickCloneButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Start date selected must be after previous record start date: " + StartDate.ToString("dd/MM/yyyy"))
                .ClickNotificationMessageHideLinkButton()

                .InsertStartDate(clonedRecordStartDate.ToString("dd/MM/yyyy"))
                .ClickCloneButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Contract Service Rate Period cloned successfully.")

                .ClickCancelButton();

            personContractServiceRatePeriodsPage
                .WaitForPersonContractServiceRatePeriodsPageToLoad()
                .ClickRefreshButton()
                .WaitForPersonContractServiceRatePeriodsPageToLoad();

            allRecords = dbHelper.careProviderPersonContractServiceRatePeriod.GetByCareProviderPersonContractServiceId(careProviderPersonContractServiceId_1);
            Assert.AreEqual(2, allRecords.Count);
            var careProviderPersonContractServiceRatePeriod2Id = allRecords.Where(c => c != careProviderPersonContractServiceRatePeriod1Id).First();

            personContractServiceRatePeriodsPage
                .OpenRecord(careProviderPersonContractServiceRatePeriod2Id);

            personContractServiceRatePeriodRecordPage
                .WaitForPersonContractServiceRatePeriodRecordPageToLoad()
                .ValidateStartDateFieldText(clonedRecordStartDate.ToString("dd/MM/yyyy"))
                .ValidateRateFieldText("10.50")
                .ValidateEndDateFieldText("")
                .ValidateResponsibleTeamLinkText("PCSRP T1")
                .ValidateRateUnitLinkText("Default Care Provider Rate Unit")
                .ValidateNoteTextText("test automation")
                .ClickDeleteRecordButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            personContractServiceRatePeriodsPage
                .WaitForPersonContractServiceRatePeriodsPageToLoad()
                .ClickRefreshButton()
                .WaitForPersonContractServiceRatePeriodsPageToLoad();

            allRecords = dbHelper.careProviderPersonContractServiceRatePeriod.GetByCareProviderPersonContractServiceId(careProviderPersonContractServiceId_1);
            Assert.AreEqual(1, allRecords.Count);

            personContractServiceRatePeriodsPage
                .ValidateRecordPresent(careProviderPersonContractServiceRatePeriod2Id, false);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-8174")]
        [Description("Step(s) 8 to 10 from the original test ACC-856")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("BusinessModule2", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Person Contract Services")]
        public void PersonContractServiceRatePeriod_ACC_6617_UITestMethod006()
        {
            #region Provider

            var providerName = "Provider " + _currentDateSuffix;
            var providerType = 13; //Residential Establishment
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, providerType, false);

            var funderProviderName = "Funder Provider " + _currentDateSuffix;
            var funderProviderType = 10; //Local Authority
            var funderProviderId = commonMethodsDB.CreateProvider(funderProviderName, _teamId, funderProviderType, false);

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitName = "Default Care Provider Rate Unit";
            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, _careProviderRateUnitName, new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingName = "Default Care Provider Batch Grouping";
            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, _careProviderBatchGroupingName, new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var _careProviderVATCodeName = "Standard Rated";
            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName(_careProviderVATCodeName)[0];

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "CPCS " + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, funderProviderId);

            #endregion

            #region Care Provider Service

            var _careProviderServiceName = "CPS " + _currentDateSuffix;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var isScheduledService = false;
            var _careProviderServiceId = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, isScheduledService);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailName = "CPSD " + _currentDateSuffix;
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, null, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(_careProviderServiceId, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var roomsApply = false;
            var negotiatedRatesApply = true;
            var permitRateOverride = false;
            var CareProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, funderProviderId, careProviderContractScheme1Id, _careProviderServiceId, careProviderServiceDetailId, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, roomsApply, negotiatedRatesApply, permitRateOverride);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(CareProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Person

            var firstName = "Mathew";
            var lastName = _currentDateSuffix;
            var _person_FullName = firstName + " " + lastName;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Care Provider Person Contract

            var StartDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-13);

            var careProviderPersonContractId_1 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractScheme1Id, funderProviderId, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId_1 = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_1, _teamId,
                careProviderContractScheme1Id, _careProviderServiceId, CareProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId, true);
            var careProviderPersonContractServiceNumber = dbHelper.careProviderPersonContractService.GetByID(careProviderPersonContractServiceId_1, "careproviderpersoncontractservicenumber")["careproviderpersoncontractservicenumber"].ToString();

            #endregion


            #region Step 8

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(careProviderPersonContractId_1.ToString());

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId_1.ToString());

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToRatesTab();

            personContractServiceRatePeriodsPage
                .WaitForPersonContractServiceRatePeriodsPageToLoad()
                .ClickNewRecordButton();

            var ratePeriodEndDate = StartDate.AddDays(2);

            personContractServiceRatePeriodRecordPage
                .WaitForPersonContractServiceRatePeriodRecordPageToLoad()
                .InsertStartDate(StartDate.ToString("dd/MM/yyyy"))
                .InsertEndDate(ratePeriodEndDate.ToString("dd/MM/yyyy"))
                .InsertRate("10.50")
                .InsertTextOnNoteText("test automation")
                .ClickSaveAndCloseButton();

            personContractServiceRatePeriodsPage
                .WaitForPersonContractServiceRatePeriodsPageToLoad()
                .ClickRefreshButton()
                .WaitForPersonContractServiceRatePeriodsPageToLoad();

            var allRecords = dbHelper.careProviderPersonContractServiceRatePeriod.GetByCareProviderPersonContractServiceId(careProviderPersonContractServiceId_1);
            Assert.AreEqual(1, allRecords.Count);
            var careProviderPersonContractServiceRatePeriod1Id = allRecords[0];

            personContractServiceRatePeriodsPage
                .ValidateRecordCellText(careProviderPersonContractServiceRatePeriod1Id, 2, StartDate.ToString("dd/MM/yyyy"))
                .ValidateRecordCellText(careProviderPersonContractServiceRatePeriod1Id, 3, ratePeriodEndDate.ToString("dd/MM/yyyy"))
                .ValidateRecordCellText(careProviderPersonContractServiceRatePeriod1Id, 4, "10.50")
                .ValidateRecordCellText(careProviderPersonContractServiceRatePeriod1Id, 5, "Default Care Provider Rate Unit");

            #endregion

            #region Step 9

            personContractServiceRatePeriodsPage
                .ClickNewRecordButton();

            personContractServiceRatePeriodRecordPage
                .WaitForPersonContractServiceRatePeriodRecordPageToLoad()
                .InsertStartDate(StartDate.ToString("dd/MM/yyyy"))
                .InsertEndDate(ratePeriodEndDate.ToString("dd/MM/yyyy"))
                .InsertRate("10.50")
                .InsertTextOnNoteText("test automation")
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("There already exists record with the same combination of Person Contract Service which overlaps with this one. To save this record change Contract Service or Start / End Dates and/or Time Band Start / Time Band End values to avoid overlapping.")
                .TapCloseButton();

            personContractServiceRatePeriodRecordPage
                .WaitForPersonContractServiceRatePeriodRecordPageToLoad()
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            personContractServiceRatePeriodsPage
                .WaitForPersonContractServiceRatePeriodsPageToLoad();

            #endregion

            #region Step 10

            //Step 10 is validated every time we execute the method personContractServiceRatePeriodsPage.WaitForPersonContractServiceRatePeriodsPageToLoad()

            #endregion

            #region Step 11

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Person Contract Service Rate Periods")
                .SelectFilter("1", "Person Contract Service")
                .SelectFilter("1", "Responsible Team")
                .SelectFilter("1", "Start Date")
                .SelectFilter("1", "End Date")
                .SelectFilter("1", "Rate")
                .SelectFilter("1", "Rate Unit");

            #endregion

        }

        #endregion



    }
}
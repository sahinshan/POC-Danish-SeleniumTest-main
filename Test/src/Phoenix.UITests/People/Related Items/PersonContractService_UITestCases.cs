using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People.Related_Items
{
    [TestClass]
    public class PersonContractService_UITestCases : FunctionalTest
    {
        private string _environmentName;
        private string _tenantName;
        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private string _teamName;
        private Guid _ethnicityId;
        private string _defaultSystemUserFullName;
        private Guid _systemUserId;
        private string _systemUserName;
        private string _systemUserFullName;
        private Guid _careProviderServiceId;
        private string _careProviderServiceName;
        private Guid _careProviderRateUnitId;
        private string _careProviderRateUnitName;
        private Guid _careProviderBatchGroupingId;
        private string _careProviderBatchGroupingName;
        private Guid _careProviderVATCodeId;
        private string _careProviderVATCodeName;
        private Guid _personId;
        private string _person_FullName;
        private int _personNumber;
        private Guid providerId;
        private string providerName;
        private Guid provider2Id;
        private string provider2Name;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");

        [TestInitialize()]
        public void TestsSetupMethod()
        {
            try
            {
                #region Environment Name

                _environmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                _tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

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
                localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

                #endregion

                #region Care Provider Service

                _careProviderServiceName = "Default Care Provider Service";
                _careProviderServiceId = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName, new DateTime(2020, 1, 1), 9999999);

                #endregion

                #region Care Provider Rate Unit

                _careProviderRateUnitName = "Default Care Provider Rate Unit";
                _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, _careProviderRateUnitName, new DateTime(2020, 1, 1), 9999999);

                #endregion

                #region Care Provider Batch Grouping

                _careProviderBatchGroupingName = "Default Care Provider Batch Grouping";
                _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, _careProviderBatchGroupingName, new DateTime(2020, 1, 1), 9999999);

                #endregion

                #region VAT Code

                _careProviderVATCodeName = "Standard Rated";
                _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName(_careProviderVATCodeName)[0];

                #endregion

                #region Person

                var firstName = "Person_Contract_Service";
                var lastName = _currentDateSuffix;
                _person_FullName = firstName + " " + lastName;
                _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
                _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

                #endregion

                #region Provider

                providerName = "Residential Establishment_1_" + _currentDateSuffix;
                var providerType = 13; //Residential Establishment
                providerId = commonMethodsDB.CreateProvider(providerName, _teamId, providerType, false);

                provider2Name = "Residential Establishment_2_" + _currentDateSuffix;
                var provider2Type = 10; //Local Authority
                provider2Id = commonMethodsDB.CreateProvider(provider2Name, _teamId, provider2Type, false);

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-4387

        [TestProperty("JiraIssueID", "ACC-4631")]
        [Description("Step(s) 1 to 3 from the original test" +
                    "1. Verify that all the mandatory and optional fileds" +
                    "2. Verify that fields has the default values" +
                    "3. Verify that Tooltip for Expected End Date/Time field.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Person Contract Services")]
        public void PersonContractService_UITestMethod01()
        {
            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, provider2Id);

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractScheme1Id, _careProviderServiceId, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            var CareProviderContractServiceTitle = (dbHelper.careProviderContractService.GetCareProviderContractServiceByID(CareProviderContractServiceId, "title")["title"]).ToString();

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContract1Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractScheme1Id, provider2Id, new DateTime(2023, 6, 8), new DateTime(2023, 6, 9));
            var careProviderPersonContractNumber = (dbHelper.careProviderPersonContract.GetByID(careProviderPersonContract1Id, "careproviderpersoncontractnumber")["careproviderpersoncontractnumber"]).ToString();
            var careProviderPersonContractTitle = (dbHelper.careProviderPersonContract.GetByID(careProviderPersonContract1Id, "title")["title"]).ToString();

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
                .OpenRecord(careProviderPersonContract1Id.ToString());

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .ClickNewRecordButton();

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ValidateAllFieldsOfGeneralSection()
                .ValidateAllFieldsOfServiceRequestSection()
                .ValidateAllFieldsOfContractRequestSection()
                .ValidateAllFieldsOfLevelOfServiceSection()
                .ValidateAllFieldsOfAuthorisationDetailsSection()
                .ValidateNoteTextSection()

                .ValidateAllMandatoryFieldVisibility()
                .ValidateAllNonMandatoryFieldVisibility()

                .ValidateIdFieldIsDisabled(true)
                .ValidateUpdateFinanceCodeOptionsIsDisabled(true)
                .ValidateFinanceCodeFieldIsDisabled(true)
                .ValidateChargePerWeekFieldIsDisabled(true)
                .ValidateSuspendDebtorInvoicesReasonLookupButtonIsDisabled(true)

                .ValidateRangeOfFrequencyInWeeks("1,9")
                .ValidateMaximumLimitOfNoteText("2000")

                .InsertEndDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndReasonFieldIsVisible(true)
                .ValidateEndReasonMandatoryFieldVisibility(true);

            #endregion

            #region Step 2

            personContractServiceRecordPage
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidatePersonContractLinkText(careProviderPersonContractTitle.ToString())
                .ValidateContractSchemeLinkText(contractSchemeName)

                .ValidateUpdateFinanceCode_OptionIsCheckedOrNot(false)
                .ValidateStartDateFieldText("")
                .ValidateStartTimeFieldText("")
                .ValidateEndDateFieldText("")
                .ValidateEndTimeFieldText("")

                .ValidateFrequencyInWeeksFieldValue("1")
                .ValidateRateRequired_OptionIsCheckedOrNot(false)
                .ValidateSuspendDebtorInvoices_OptionIsCheckedOrNot(false)
                .ValidateStatusPickListValues("In Progress")

                .ClickSuspendDebtorInvoices_Option(true)
                .ValidateSuspendDebtorInvoicesReasonLookupButtonIsDisabled(false)
                .ClickServiceLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(_careProviderServiceName, _careProviderServiceId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ValidateContractServiceLinkText(CareProviderContractServiceTitle)
                .ValidateRateUnitLinkText(_careProviderRateUnitName);

            #endregion

            #region Step 3

            personContractServiceRecordPage
                .ValidateExpectedEndDateTimeFieldLabelToolTip("Optional to record the expected end date of the Service.  It is only an information field.");

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4546

        [TestProperty("JiraIssueID", "ACC-4636")]
        [Description("Step(s) 4 to 9 from the original test" +
                    "4. Verify that Service look-up display all active Service records." +
                    "5. Verify that Service Details look-up display all active Service Detail records." +
                    "6. Verify that Contract Service filed is auto-populates with the only record fits the combination " +
                    "7. Verify that the Rate Required? field has the default values" +
                    "8. Verify that Rate Unit field is a Read Only when Rate Required? = No" +
                    "9. Verify that Rate Unit field is not Read Only when Rate Required? = Yes")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Person Contract Services")]
        public void PersonContractService_UITestMethod02()
        {
            #region Care Provider Service

            var _careProviderServiceName_2 = "Care Provider Service Scheduled Service Yes";
            var _careProviderServiceId_2 = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName_2, new DateTime(2020, 1, 1), 4567, null, true);

            var _careProviderServiceName_3 = "Negotiated Rates Apply Yes";
            var _careProviderServiceId_3 = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName_3, new DateTime(2020, 1, 1), 6780, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, provider2Id);


            var contractSchemeName_2 = "Default2_" + _currentDateSuffix;
            var contractSchemeCode_2 = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode_2).Any())
                contractSchemeCode_2 = contractSchemeCode_2 + 1;

            var careProviderContractScheme1Id_2 = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName_2, new DateTime(2020, 1, 1), contractSchemeCode_2, providerId, provider2Id);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailName1 = "Care Provider Service Details Test 1";
            var careProviderServiceDetailId1 = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName1, Code1, null, new DateTime(2020, 1, 1));

            var Code2 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code2).Any())
                Code2 = Code2 + 1;

            var careProviderServiceDetailName2 = "Care Provider Service Details Test 2";
            var careProviderServiceDetailId2 = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName2, Code2, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(_careProviderServiceId, _teamId, careProviderServiceDetailId1, null, null, "");
            commonMethodsDB.CreateCareProviderServiceMapping(_careProviderServiceId, _teamId, careProviderServiceDetailId2, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractScheme1Id, _careProviderServiceId, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            dbHelper.careProviderContractService.UpdateCareProviderServiceDetailId(CareProviderContractServiceId, careProviderServiceDetailId2);
            var CareProviderContractServiceTitle = (dbHelper.careProviderContractService.GetCareProviderContractServiceByID(CareProviderContractServiceId, "title")["title"]).ToString();

            var CareProviderContractServiceId_2 = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractScheme1Id, _careProviderServiceId_2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            var CareProviderContractServiceId_3 = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractScheme1Id, _careProviderServiceId_3, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, true);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContract1Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractScheme1Id, provider2Id, new DateTime(2023, 6, 8), new DateTime(2023, 6, 9));

            #endregion

            #region Step 4 to 6

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
                .OpenRecord(careProviderPersonContract1Id.ToString());

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .ClickNewRecordButton();

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ClickServiceLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_careProviderServiceName_2).TapSearchButton().ValidateResultElementNotPresent(_careProviderServiceId_2.ToString())
                .SearchAndSelectRecord(_careProviderServiceName, _careProviderServiceId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ClickServiceDetailLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(careProviderServiceDetailName1).TapSearchButton().ValidateResultElementPresent(careProviderServiceDetailId1.ToString())
                .TypeSearchQuery(careProviderServiceDetailName2).TapSearchButton().ValidateResultElementPresent(careProviderServiceDetailId2.ToString())
                .SelectResultElement(careProviderServiceDetailId2.ToString());

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ValidateContractServiceLinkText(CareProviderContractServiceTitle)
                .ValidateContractServiceLookupButtonIsDisabled(true)
                .ValidateRateUnitLinkText(_careProviderRateUnitName)
                .ClickServiceRemoveButton();

            System.Threading.Thread.Sleep(1000);

            personContractServiceRecordPage
                .ValidateServiceLinkText("")
                .ValidateServiceDetailLinkText("")
                .ValidateContractServiceLinkText("")
                .ValidateServiceDetailLookupButtonIsDisabled(true)
                .ValidateContractServiceLookupButtonIsDisabled(true);

            #endregion

            #region Step 7 & 8

            personContractServiceRecordPage
                .ClickServiceLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(_careProviderServiceName, _careProviderServiceId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ClickServiceDetailLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(careProviderServiceDetailName2, careProviderServiceDetailId2);

            System.Threading.Thread.Sleep(2000);
            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ValidateRateUnitLookupButtonIsDisabled(true)
                .ValidateRateRequired_OptionIsCheckedOrNot(false)
                .ClickServiceLookupButton();

            #endregion

            #region Step 7 & 9

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(_careProviderServiceName_3, _careProviderServiceId_3);

            System.Threading.Thread.Sleep(2000);
            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ValidateRateUnitLookupButtonIsDisabled(true)
                .ValidateRateRequired_OptionIsCheckedOrNot(true);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4594

        [TestProperty("JiraIssueID", "ACC-4637")]
        [Description("Step(s) 10 to 12 from the original test ACC-856" +
                    "10. Verify that Rate Unit field becomes Read Only when a Person Contract Service Rate Period record is recorded against PCS." +
                    "11. Verify that End reason field is visible only if the End date field is recorded Also, End reason field should be mandatory when the End date is recorded" +
                    "12. Verify that End Reason look-up displays the active Person Contract Service End Reason record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen2", "Person Contract Services")]
        [TestProperty("Screen3", "Person Contract Service Rate Periods")]
        public void PersonContractService_UITestMethod03()
        {
            #region Care Provider Service

            var _careProviderServiceName_1 = "Negotiated Rates Apply Yes";
            var _careProviderServiceId_1 = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName_1, new DateTime(2020, 1, 1), 6780, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId_1 = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, provider2Id);

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId_1 = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractSchemeId_1, _careProviderServiceId_1, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, true);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContractId_1 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractSchemeId_1, provider2Id, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(5));

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_1, _teamId, careProviderContractSchemeId_1, _careProviderServiceId_1, CareProviderContractServiceId_1, DateTime.Now.AddDays(-1), 1, 1, _careProviderRateUnitId, true);
            dbHelper.careProviderPersonContractService.UpdateOverrideRateAndRateVerified(careProviderPersonContractServiceId, false, false);

            var careProviderPersonContractServiceTitle = (dbHelper.careProviderPersonContractService.GetByID(careProviderPersonContractServiceId, "title")["title"]).ToString();

            #endregion

            #region Step 10

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
                .ValidateRateUnitLookupButtonIsDisabled(false)
                .ValidateRateRequired_OptionIsCheckedOrNot(true)
                .NavigateToRatesTab();

            personContractServiceRatePeriodsPage
                .WaitForPersonContractServiceRatePeriodsPageToLoad()
                .ClickNewRecordButton();

            personContractServiceRatePeriodRecordPage
                .WaitForPersonContractServiceRatePeriodRecordPageToLoad()
                .ValidatePersonContractServiceLinkText(careProviderPersonContractServiceTitle)
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateRateUnitLinkText(_careProviderRateUnitName)
                .InsertStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertRate("10")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

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

            var careProviderPersonContractServiceRatePeriodId = dbHelper.careProviderPersonContractServiceRatePeriod.GetByCareProviderPersonContractServiceId(careProviderPersonContractServiceId).FirstOrDefault();

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ValidateRateUnitLookupButtonIsDisabled(true)
                .ValidateRateRequired_OptionIsCheckedOrNot(true)
                .NavigateToRatesTab();

            personContractServiceRatePeriodsPage
                .WaitForPersonContractServiceRatePeriodsPageToLoad()
                .SelectRecord(careProviderPersonContractServiceRatePeriodId.ToString())
                .ClickDeleteRecordButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("1 item(s) deleted.")
                .TapOKButton();

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
                .ValidateRateUnitLookupButtonIsDisabled(false)
                .ValidateRateRequired_OptionIsCheckedOrNot(true);

            #endregion

            #region Step 11 & 12

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonName_1 = "Active Person Contract Service End Reason";
            var careProviderPersonContractServiceEndReasonId_1 = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, careProviderPersonContractServiceEndReasonName_1, new DateTime(2020, 1, 1), 999999);

            var careProviderPersonContractServiceEndReasonName_2 = "Inactive Person Contract Service End Reason";
            var careProviderPersonContractServiceEndReasonId_2 = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, careProviderPersonContractServiceEndReasonName_2, new DateTime(2020, 1, 1), 989878);
            dbHelper.careProviderPersonContractServiceEndReason.UpdateInactiveStatus(careProviderPersonContractServiceEndReasonId_2, true);

            #endregion

            personContractServiceRecordPage
                .ValidateEndReasonFieldIsVisible(false)
                .InsertEndDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndReasonFieldIsVisible(true)
                .ValidateEndReasonMandatoryFieldVisibility(true)
                .ClickEndReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(careProviderPersonContractServiceEndReasonName_2)
                .TapSearchButton()
                .ValidateResultElementNotPresent(careProviderPersonContractServiceEndReasonId_2.ToString())
                .SearchAndSelectRecord(careProviderPersonContractServiceEndReasonName_1, careProviderPersonContractServiceEndReasonId_1);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ValidateEndReasonLinkText(careProviderPersonContractServiceEndReasonName_1)
                .ClearEndDate();

            personContractServiceRecordPage
                .ValidateEndReasonFieldIsVisible(false);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4638")]
        [Description("Step(s) 14 to from the original test ACC-856" +
                    "14. System allows to create duplicate Person Contract Service records.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Person Contract Services")]
        public void PersonContractService_UITestMethod04()
        {
            #region Care Provider Service

            var _careProviderServiceName_1 = "Care Provider Service Record";
            var _careProviderServiceId_1 = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName_1, new DateTime(2020, 1, 1), 7895, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId_1 = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, provider2Id);

            #endregion

            #region Care Provider Contract Service

            dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractSchemeId_1, _careProviderServiceId_1, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, true);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContractId_1 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractSchemeId_1, provider2Id, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(5));

            #endregion

            #region Step 14

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
                .ClickNewRecordButton();

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ClickServiceLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(_careProviderServiceName_1, _careProviderServiceId_1);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .InsertStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ClickBackButton();

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.GetByPersonContractId(careProviderPersonContractId_1);
            Assert.AreEqual(1, careProviderPersonContractServiceId.Count);

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .ValidateRecordPresent(careProviderPersonContractServiceId[0].ToString(), true)
                .ClickNewRecordButton();

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ClickServiceLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(_careProviderServiceName_1, _careProviderServiceId_1);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .InsertStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ClickBackButton();

            careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.GetByPersonContractId(careProviderPersonContractId_1);
            Assert.AreEqual(2, careProviderPersonContractServiceId.Count);

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .ValidateRecordPresent(careProviderPersonContractServiceId[0].ToString(), true)
                .ValidateRecordPresent(careProviderPersonContractServiceId[1].ToString(), true);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4639")]
        [Description("Step(s) 15 to 19 to from the original test ACC-856" +
                    "15. Verify that when user enters start date/time <  parent Person Contract start date & save the record, error message appears as “Start date cannot be before Start Date of parent Person Contract.Please choose a value after or equal to[parent Person Contract start date]“ ." +
                    "16. Verify that when user enters start date > parent Person Contract end date & save the record, error message appears as “Start date cannot be after End Date of parent Person Contract. Please choose a value before or equal to[parent Person Contract end date]“" +
                    "17. Verify that when user enters start date/time > =  end date/time & save the record, error message appears as “Start Date/Time cannot be after or equal to End Date/Time “" +
                    "18. Verify that when user enters this start date/time < parent Person Contract start date & save the record, error message appears as “Start date cannot be before Start Date of parent Person Contract. Please choose a value after or equal to [parent Person Contract start date]“" +
                    "19. Verify that when user enters this end date/time > parent Person Contract end date & save the record, error message appears as “End date cannot be after End Date of parent Person Contract. Please choose value before or equal to [parent Person Contract end date]”")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Person Contract Services")]
        public void PersonContractService_UITestMethod05()
        {
            #region Care Provider Service

            var _careProviderServiceName_1 = "Care Provider Service Record";
            var _careProviderServiceId_1 = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName_1, new DateTime(2020, 1, 1), 7895, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId_1 = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, provider2Id);

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId_1 = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractSchemeId_1, _careProviderServiceId_1, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, true);

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonName_1 = "Active Person Contract Service End Reason";
            var careProviderPersonContractServiceEndReasonId_1 = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, careProviderPersonContractServiceEndReasonName_1, new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContractId_1 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractSchemeId_1, provider2Id, new DateTime(2023, 10, 2), new DateTime(2023, 10, 8, 10, 15, 0));
            var futureDate = new DateTime(2023, 10, 9).ToString("dd'/'MM'/'yyyy");
            var pastDate = new DateTime(2023, 10, 1).ToString("dd'/'MM'/'yyyy");

            #endregion

            #region Step 15

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
                .ClickNewRecordButton();

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ClickServiceLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(_careProviderServiceName_1, _careProviderServiceId_1);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .InsertStartDate(pastDate)
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Start date cannot be before Start Date of parent Person Contract. Please choose a value after or equal to " + new DateTime(2023, 10, 2).ToString("dd'/'MM'/'yyyy") + "")
                .TapCloseButton();

            #endregion

            #region Step 16

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .InsertStartDate(futureDate)
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Start date cannot be after End Date of parent Person Contract. Please choose a value before or equal to " + new DateTime(2023, 10, 8).ToString("dd'/'MM'/'yyyy") + " 10:15")
                .TapCloseButton();

            #endregion

            #region Step 17

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .InsertStartDate(new DateTime(2023, 10, 4).ToString("dd'/'MM'/'yyyy"))
                .InsertEndDate(new DateTime(2023, 10, 4).Date.ToString("dd'/'MM'/'yyyy"))
                .ClickEndReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(careProviderPersonContractServiceEndReasonName_1, careProviderPersonContractServiceEndReasonId_1);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Start Date/Time cannot be after or equal to End Date/Time")
                .TapCloseButton();

            #endregion

            #region Step 18

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .InsertStartDate(pastDate)
                .InsertEndDate(new DateTime(2023, 10, 4).Date.ToString("dd'/'MM'/'yyyy"))
                .ClickEndReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(careProviderPersonContractServiceEndReasonName_1, careProviderPersonContractServiceEndReasonId_1);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Start date cannot be before Start Date of parent Person Contract. Please choose a value after or equal to " + new DateTime(2023, 10, 2).ToString("dd'/'MM'/'yyyy") + "")
                .TapCloseButton();

            #endregion

            #region Step 19

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .InsertStartDate(new DateTime(2023, 10, 4).ToString("dd'/'MM'/'yyyy"))
                .InsertEndDate(futureDate)
                .ClickEndReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(careProviderPersonContractServiceEndReasonName_1, careProviderPersonContractServiceEndReasonId_1);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("End date cannot be after End Date of parent Person Contract. Please choose value before or equal to " + new DateTime(2023, 10, 8).ToString("dd'/'MM'/'yyyy") + " 10:15")
                .TapCloseButton();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4640")]
        [Description("Step(s) 20 to 22 from the original test ACC-856" +
                    "20. Verify that when user enters this start date/time < parent Person Contract start date & save the record, error message appears as “Start date cannot be before Start Date of parent Person Contract. Please choose a value after or equal to [parent Person Contract start date]“" +
                    "21. Verify that when user enters this start date/time > = end date/time & save the record, error message appears as “Start Date cannot be after or equal to End Date “" +
                    "22. Verify that when user enters this start date/time<  parent Person Contract start date & save the record, error message appears as “Start date cannot be before Start Date of parent Person Contract. Please choose a value after or equal to [parent Person Contract start date]“")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Person Contract Services")]
        public void PersonContractService_UITestMethod06()
        {
            #region Care Provider Service

            var _careProviderServiceName_1 = "Care Provider Service Record";
            var _careProviderServiceId_1 = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName_1, new DateTime(2020, 1, 1), 7895, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId_1 = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, provider2Id);

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId_1 = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractSchemeId_1, _careProviderServiceId_1, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, true);

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonName_1 = "Active Person Contract Service End Reason";
            var careProviderPersonContractServiceEndReasonId_1 = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, careProviderPersonContractServiceEndReasonName_1, new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContractId_1 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractSchemeId_1, provider2Id, new DateTime(2023, 10, 2));
            var pastDate = new DateTime(2023, 10, 1).ToString("dd'/'MM'/'yyyy");

            #endregion

            #region Step 20

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
                .ClickNewRecordButton();

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ClickServiceLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(_careProviderServiceName_1, _careProviderServiceId_1);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .InsertStartDate(pastDate)
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Start date cannot be before Start Date of parent Person Contract. Please choose a value after or equal to " + new DateTime(2023, 10, 2).ToString("dd'/'MM'/'yyyy") + "")
                .TapCloseButton();

            #endregion

            #region Step 21

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .InsertStartDate(new DateTime(2023, 10, 4).ToString("dd'/'MM'/'yyyy"))
                .InsertEndDate(new DateTime(2023, 10, 4).Date.ToString("dd'/'MM'/'yyyy"))
                .ClickEndReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(careProviderPersonContractServiceEndReasonName_1, careProviderPersonContractServiceEndReasonId_1);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Start Date/Time cannot be after or equal to End Date/Time")
                .TapCloseButton();

            #endregion

            #region Step 22

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .InsertStartDate(pastDate)
                .InsertEndDate(new DateTime(2023, 10, 4).Date.ToString("dd'/'MM'/'yyyy"))
                .ClickEndReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(careProviderPersonContractServiceEndReasonName_1, careProviderPersonContractServiceEndReasonId_1);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Start date cannot be before Start Date of parent Person Contract. Please choose a value after or equal to " + new DateTime(2023, 10, 2).ToString("dd'/'MM'/'yyyy") + "")
                .TapCloseButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4667")]
        [Description("Step(s) 23 to 24 from the original test ACC-856" +
                    " Condition: Person Contract Has End Date." +
                    "23. Verify that user is able to save the record when start date / time >= parent Person Contract start date & start date / time <= parent Person Contract end date" +
                    "24. Verify that user is able to save the record when (i) start date / time < end date / time (ii) start date / time >= parent Person Contract start date (iii) end date / time <= parent Person Contract end date")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Person Contract Services")]
        public void PersonContractService_UITestMethod07()
        {
            #region Care Provider Service

            var _careProviderServiceName_1 = "Care Provider Service Record";
            var _careProviderServiceId_1 = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName_1, new DateTime(2020, 1, 1), 7895, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId_1 = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, provider2Id);

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId_1 = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractSchemeId_1, _careProviderServiceId_1, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, true);

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonName_1 = "Active Person Contract Service End Reason";
            var careProviderPersonContractServiceEndReasonId_1 = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, careProviderPersonContractServiceEndReasonName_1, new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContractId_1 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractSchemeId_1, provider2Id, new DateTime(2023, 10, 2), new DateTime(2023, 10, 8, 10, 15, 0));
            var ValidDate_1 = new DateTime(2023, 10, 4).ToString("dd'/'MM'/'yyyy");
            var ValidDate_2 = new DateTime(2023, 10, 5).ToString("dd'/'MM'/'yyyy");

            #endregion

            #region Step 23

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
                .ClickNewRecordButton();

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ClickServiceLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(_careProviderServiceName_1, _careProviderServiceId_1);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .InsertStartDate(ValidDate_1)
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.GetByPersonContractId(careProviderPersonContractId_1);
            Assert.AreEqual(1, careProviderPersonContractServiceId.Count);
            var careProviderPersonContractServiceTitle = (dbHelper.careProviderPersonContractService.GetByID(careProviderPersonContractServiceId[0], "title")["title"]).ToString();

            personContractServiceRecordPage
                .ValidatePageHeaderText(careProviderPersonContractServiceTitle)
                .ClickBackButton();

            #endregion

            #region Step 24

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .ClickNewRecordButton();

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ClickServiceLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(_careProviderServiceName_1, _careProviderServiceId_1);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .InsertStartDate(ValidDate_1)
                .InsertEndDate(ValidDate_2)
                .ClickEndReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(careProviderPersonContractServiceEndReasonName_1, careProviderPersonContractServiceEndReasonId_1);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.GetByPersonContractId(careProviderPersonContractId_1);
            Assert.AreEqual(2, careProviderPersonContractServiceId.Count);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4595

        [TestProperty("JiraIssueID", "ACC-4668")]
        [Description("Step(s) 25 to 26 from the original test ACC-856" +
                    " Condition: Person Contract Has no End Date." +
                    "25. Verify that user is able to save the record when (i) start date / time >= parent Person Contract start date" +
                    "26. Verify that user is able to save the record with when (i) start date / time < end date / time (ii) start date / time >= parent Person Contract start date")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Person Contract Services")]
        public void PersonContractService_UITestMethod08()
        {
            #region Care Provider Service

            var _careProviderServiceName_1 = "Care Provider Service Record";
            var _careProviderServiceId_1 = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName_1, new DateTime(2020, 1, 1), 7895, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId_1 = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, provider2Id);

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId_1 = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractSchemeId_1, _careProviderServiceId_1, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, true);

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonName_1 = "Active Person Contract Service End Reason";
            var careProviderPersonContractServiceEndReasonId_1 = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, careProviderPersonContractServiceEndReasonName_1, new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContractId_1 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractSchemeId_1, provider2Id, new DateTime(2023, 10, 2));
            var ValidDate_1 = new DateTime(2023, 10, 4).ToString("dd'/'MM'/'yyyy");
            var ValidDate_2 = new DateTime(2023, 10, 5).ToString("dd'/'MM'/'yyyy");

            #endregion

            #region Step 25

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
                .ClickNewRecordButton();

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ClickServiceLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(_careProviderServiceName_1, _careProviderServiceId_1);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .InsertStartDate(ValidDate_1)
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.GetByPersonContractId(careProviderPersonContractId_1);
            Assert.AreEqual(1, careProviderPersonContractServiceId.Count);
            var careProviderPersonContractServiceTitle = (dbHelper.careProviderPersonContractService.GetByID(careProviderPersonContractServiceId[0], "title")["title"]).ToString();

            personContractServiceRecordPage
                .ValidatePageHeaderText(careProviderPersonContractServiceTitle)
                .ClickBackButton();

            #endregion

            #region Step 26

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .ClickNewRecordButton();

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ClickServiceLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(_careProviderServiceName_1, _careProviderServiceId_1);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .InsertStartDate(ValidDate_1)
                .InsertEndDate(ValidDate_2)
                .ClickEndReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(careProviderPersonContractServiceEndReasonName_1, careProviderPersonContractServiceEndReasonId_1);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.GetByPersonContractId(careProviderPersonContractId_1);
            Assert.AreEqual(2, careProviderPersonContractServiceId.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4669")]
        [Description("Step(s) 27 to 30 from the original test ACC-856" +
                    "27. Verify that appropriate error message appear when user tries to save the record without entering mandatory fields" +
                    "28. Verify that user is able to Create records with Mandatory + optional fields" +
                    "29. Verify that the title of the Edit screen is : Person Contract \\ ID, Responsible Team & Person Contract fields are non-editable" +
                    "30. Verify that In PCS Edit screen, the Finance Code field is Read only when the PCS status = In Progress or Completed or Cancelled")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Person Contract Services")]
        public void PersonContractService_UITestMethod09()
        {
            #region Care Provider Service

            var _careProviderServiceName_1 = "Care Provider Service Record";
            var _careProviderServiceId_1 = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName_1, new DateTime(2020, 1, 1), 7895, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId_1 = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, provider2Id);
            dbHelper.careProviderContractScheme.UpdateIsUpdateableOnPersonContractService(careProviderContractSchemeId_1, false);

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId_1 = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractSchemeId_1, _careProviderServiceId_1, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, true);

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonName_1 = "Active Person Contract Service End Reason";
            var careProviderPersonContractServiceEndReasonId_1 = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, careProviderPersonContractServiceEndReasonName_1, new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContractId_1 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractSchemeId_1, provider2Id, new DateTime(2023, 10, 2));
            var careProviderPersonContractTitle = (dbHelper.careProviderPersonContract.GetByID(careProviderPersonContractId_1, "title")["title"]).ToString();

            var ValidDate_1 = new DateTime(2023, 10, 4).ToString("dd'/'MM'/'yyyy");
            var ValidDate_2 = new DateTime(2023, 10, 5).ToString("dd'/'MM'/'yyyy");

            #endregion

            #region Step 27

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
                .ClickNewRecordButton();

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ValidateIdFieldIsBlank(true)
                .ClickSaveButton()
                .ValidateNotificationAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateStartDateFieldErrorMessageText("Please fill out this field.")
                .ValidateStartTimeFieldErrorMessageText("Please fill out this field.");

            #endregion

            #region Step 28

            personContractServiceRecordPage
                .ClickServiceLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(_careProviderServiceName_1, _careProviderServiceId_1);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .InsertStartDate(ValidDate_1)
                .InsertStartTime("10:00")
                .InsertEndDate(ValidDate_2)
                .InsertEndTime("10:00")
                .InsertExpectedEndDate(ValidDate_2)
                .InsertExpectedEndTime("12:00")
                .ClickEndReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(careProviderPersonContractServiceEndReasonName_1, careProviderPersonContractServiceEndReasonId_1);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ClickServiceDetailClearLookupButton();

            personContractServiceRecordPage
                .ValidateContractServiceLinkText("Residential Establishment_1_" + _currentDateSuffix + " \\ Residential Establishment_2_" + _currentDateSuffix + " \\ Default1_" + _currentDateSuffix + " \\ Care Provider Service Record \\")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.GetByPersonContractId(careProviderPersonContractId_1);
            Assert.AreEqual(1, careProviderPersonContractServiceId.Count);
            var careProviderPersonContractServiceTitle = (dbHelper.careProviderPersonContractService.GetByID(careProviderPersonContractServiceId[0], "title")["title"]).ToString();

            dbHelper.careProviderPersonContractServiceRatePeriod.CreateCareProviderPersonContractServiceRatePeriod(_teamId, careProviderPersonContractServiceId[0], _careProviderRateUnitId, new DateTime(2023, 10, 4), 10, "");

            #endregion

            #region Step 29

            personContractServiceRecordPage
                .ValidateIdFieldIsBlank(false)
                .GetIdFieldValueAndValidateTitle(careProviderPersonContractTitle)
                .ValidatePageHeaderText(careProviderPersonContractServiceTitle)
                .ValidateResponsibleTeamLookupButtonIsDisabled(true)
                .ValidatePersonContractLookupButtonIsDisabled(true)
                .ValidatePersonContractRemoveButtonVisibility(false);

            #endregion

            #region Step 30

            personContractServiceRecordPage
                .ValidateFinanceCodeFieldIsDisabled(true)
                .ValidateUpdateFinanceCode_OptionIsCheckedOrNot(false)
                .SelectStatus("Completed")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            System.Threading.Thread.Sleep(2000);
            personContractServiceRecordPage
                .ValidateFinanceCodeFieldIsDisabled(true)
                .ValidateUpdateFinanceCode_OptionIsCheckedOrNot(false)
                .SelectStatus("Cancelled")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            System.Threading.Thread.Sleep(1000);
            personContractServiceRecordPage
                .ValidateFinanceCodeFieldIsDisabled(true)
                .ValidateUpdateFinanceCode_OptionIsCheckedOrNot(false);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4670")]
        [Description("Step(s) 31 to 32 from the original test ACC-856")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Person Contract Services")]
        public void PersonContractService_UITestMethod10()
        {
            #region Care Provider Service

            var _careProviderServiceName_1 = "Care Provider Service Record";
            var _careProviderServiceId_1 = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName_1, new DateTime(2020, 1, 1), 7895, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId_1 = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, provider2Id);

            var financeCodeLocationName_Constant = "Constant";
            var financeCodeLocationId_Constant = dbHelper.careProviderFinanceCodeLocation.GetByName(financeCodeLocationName_Constant)[0];

            // Finance Code Mapping
            dbHelper.careProviderFinanceCodeMapping.CreateCareProviderFinanceCodeMapping(_teamId, careProviderContractSchemeId_1, financeCodeLocationId_Constant, null, null, null, "12345");

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId_1 = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractSchemeId_1, _careProviderServiceId_1, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, true);

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonName_1 = "Active Person Contract Service End Reason";
            commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, careProviderPersonContractServiceEndReasonName_1, new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContractId_1 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractSchemeId_1, provider2Id, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(5));

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_1, _teamId, careProviderContractSchemeId_1, _careProviderServiceId_1, CareProviderContractServiceId_1, DateTime.Now.AddDays(-3), 1, 1, _careProviderRateUnitId, true);
            var careProviderPersonContractServiceId_2 = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_1, _teamId, careProviderContractSchemeId_1, _careProviderServiceId_1, CareProviderContractServiceId_1, DateTime.Now.AddDays(-1), 1, 1, _careProviderRateUnitId, true);

            dbHelper.careProviderPersonContractServiceRatePeriod.CreateCareProviderPersonContractServiceRatePeriod(_teamId, careProviderPersonContractServiceId, _careProviderRateUnitId, DateTime.Now.AddDays(-1), 10, "");

            #endregion

            #region Step 31

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
                .ValidateFinanceCodeFieldIsDisabled(true)
                .ValidateUpdateFinanceCodeOptionsIsDisabled(false)
                .ValidateUpdateFinanceCode_OptionIsCheckedOrNot(false);

            personContractServiceRecordPage
                .SelectStatus("Completed")
                .ClickSaveButton();

            System.Threading.Thread.Sleep(1000);
            personContractServiceRecordPage
                .ValidateFinanceCodeFieldIsDisabled(true)
                .ValidateUpdateFinanceCodeOptionsIsDisabled(false)
                .ValidateUpdateFinanceCode_OptionIsCheckedOrNot(false)
                .SelectStatus("Cancelled")
                .ClickSaveButton();

            System.Threading.Thread.Sleep(1000);
            personContractServiceRecordPage
                .ValidateFinanceCodeFieldIsDisabled(true)
                .ValidateUpdateFinanceCodeOptionsIsDisabled(true)
                .ValidateUpdateFinanceCode_OptionIsCheckedOrNot(false)
                .ClickBackButton();

            #endregion

            #region Step 32

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId_2.ToString());

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ValidateUpdateFinanceCode_OptionIsCheckedOrNot(false)
                .ValidateFinanceCodeFieldValue("12345")
                .ClickUpdateFinanceCode_Option(true)
                .ValidateUpdateFinanceCode_OptionIsCheckedOrNot(true)
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ValidateUpdateFinanceCode_OptionIsCheckedOrNot(true)
                .ClickFinanceCodePencilIcon();

            financeCodeUpdaterPopup
                .WaitForPopupToLoad()
                .InsertTextInPosition2("ABC")
                .TapSaveButton();

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ValidateFinanceCodeFieldValue("12345ABC")
                .ValidateUpdateFinanceCode_OptionIsCheckedOrNot(true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4913")]
        [Description("Step(s) 33 to 36 from the original test ACC-856")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Person Contract Services")]
        // Step 35 & 36 Failing Due to Bug - https://advancedcsg.atlassian.net/browse/ACC-4905
        // Bug rejected https://advancedcsg.atlassian.net/browse/ACC-4905. Updated step 35 & 36 according to changes.
        public void PersonContractService_UITestMethod11()
        {
            #region Care Provider Service

            var _careProviderServiceName_1 = "Care Provider Service Record";
            var _careProviderServiceId_1 = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName_1, new DateTime(2020, 1, 1), 7895, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId_1 = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, provider2Id);


            var contractSchemeName_2 = "Default_2_" + _currentDateSuffix;
            var contractSchemeCode_2 = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode_2).Any())
                contractSchemeCode_2 = contractSchemeCode_2 + 1;

            var careProviderContractSchemeId_2 = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName_2, new DateTime(2020, 1, 1), contractSchemeCode_2, providerId, provider2Id);
            dbHelper.careProviderContractScheme.UpdateIsUpdateableOnPersonContractService(careProviderContractSchemeId_2, false);

            var financeCodeLocationName_Constant = "Constant";
            var financeCodeLocationId_Constant = dbHelper.careProviderFinanceCodeLocation.GetByName(financeCodeLocationName_Constant)[0];

            // Finance Code Mapping
            dbHelper.careProviderFinanceCodeMapping.CreateCareProviderFinanceCodeMapping(_teamId, careProviderContractSchemeId_2, financeCodeLocationId_Constant, null, null, null, "12345");

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId_1 = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractSchemeId_1, _careProviderServiceId_1, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, true);
            var CareProviderContractServiceId_2 = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractSchemeId_2, _careProviderServiceId_1, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, true);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContractId_1 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractSchemeId_1, provider2Id, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(5));
            var careProviderPersonContractId_2 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractSchemeId_2, provider2Id, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(5));

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId_1 = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_1, _teamId, careProviderContractSchemeId_1, _careProviderServiceId_1, CareProviderContractServiceId_1, DateTime.Now.AddDays(-3), 1, 1, _careProviderRateUnitId, true);
            var careProviderPersonContractServiceId_2 = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_2, _teamId, careProviderContractSchemeId_2, _careProviderServiceId_1, CareProviderContractServiceId_2, DateTime.Now.AddDays(-2), 1, 1, _careProviderRateUnitId, true);
            var careProviderPersonContractServiceId_3 = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_2, _teamId, careProviderContractSchemeId_2, _careProviderServiceId_1, CareProviderContractServiceId_2, DateTime.Now.AddDays(-1), 1, 1, _careProviderRateUnitId, true);

            #endregion

            #region Step 33

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
                .ValidateUpdateFinanceCode_OptionIsCheckedOrNot(false)
                .ValidateFinanceCodeFieldIsDisabled(true)
                .ClickBackButton();

            dbHelper.careProviderContractScheme.UpdateIsUpdateableOnPersonContractService(careProviderContractSchemeId_1, false);

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .ClickRefreshButton()
                .OpenRecord(careProviderPersonContractServiceId_1.ToString());

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ValidateUpdateFinanceCode_OptionIsCheckedOrNot(false)
                .ValidateFinanceCodeFieldIsDisabled(true)
                .ClickBackButton();

            personContractServicesPage
                .WaitForPersonContractServicesTabSectionToLoad()
                .ClickDetailsTab();

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickBackButton();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .ClickRefreshButton()
                .OpenRecord(careProviderPersonContractId_2.ToString());

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId_2.ToString());

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ValidateUpdateFinanceCode_OptionIsCheckedOrNot(false)
                .ValidateFinanceCodeFieldIsDisabled(true)
                .ClickBackButton();

            dbHelper.careProviderContractScheme.UpdateIsUpdateableOnPersonContractService(careProviderContractSchemeId_2, true);

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .ClickRefreshButton()
                .OpenRecord(careProviderPersonContractServiceId_2.ToString());

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ValidateUpdateFinanceCode_OptionIsCheckedOrNot(false)
                .ValidateFinanceCodeFieldIsDisabled(true)
                .ClickUpdateFinanceCode_Option(true)
                .ClickSaveButton();

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ValidateUpdateFinanceCode_OptionIsCheckedOrNot(true)
                .ClickFinanceCodePencilIcon();

            financeCodeUpdaterPopup
                .WaitForPopupToLoad()
                .InsertTextInPosition2("ACC")
                .TapSaveButton();

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ValidateFinanceCodeFieldValue("12345ACC")
                .ClickBackButton();

            dbHelper.careProviderContractScheme.UpdateIsUpdateableOnPersonContractService(careProviderContractSchemeId_2, false);

            dbHelper.careProviderPersonContractServiceRatePeriod.CreateCareProviderPersonContractServiceRatePeriod(_teamId, careProviderPersonContractServiceId_2, _careProviderRateUnitId, DateTime.Now.AddDays(-1), 14, "");

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId_2.ToString());

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ValidateUpdateFinanceCode_OptionIsCheckedOrNot(true)
                .ValidateFinanceCodeFieldIsDisabled(true);

            #endregion

            #region Step 34

            personContractServiceRecordPage
                .SelectStatus("Completed")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            System.Threading.Thread.Sleep(2000);

            personContractServiceRecordPage
                .ValidateIdFieldIsDisabled(true)
                .ValidateResponsibleTeamLookupButtonIsDisabled(true)
                .ValidatePersonContractLookupButtonIsDisabled(true)

                .ValidateContractSchemeLookupButtonIsDisabled(true)
                .ValidateServiceLookupButtonIsDisabled(true)
                .ValidateServiceDetailLookupButtonIsDisabled(true)
                .ValidateContractServiceLookupButtonIsDisabled(true)
                .ValidateUpdateFinanceCodeOptionsIsDisabled(true)
                .ValidateFinanceCodeFieldIsDisabled(true)
                .ValidateAccountCodeFieldIsDisabled(true)

                .ValidateStartDateTimeFieldIsDisabled(true)
                .ValidateEndDateTimeFieldIsDisabled(false)
                .ValidateExpectedEndDateTimeFieldIsDisabled(false)
                .ValidateStatusPicklistIsDisabled(false)
                .ValidateChargePerWeekFieldIsDisabled(true)

                .ValidateRateUnitLookupButtonIsDisabled(true)
                .ValidateFrequencyInWeeksFieldIsDisabled(true)
                .ValidateRateRequiredOptionsIsDisabled(true)
                .ValidateOverrideRateOptionsIsDisabled(true)
                .ValidateRateVerifiedOptionsIsDisabled(true)
                .ValidateSuspendDebtorInvoicesOptionsIsDisabled(false)
                .ValidateSuspendDebtorInvoicesReasonLookupButtonIsDisabled(true)

                .ValidateCompletedByLookupButtonIsDisabled(true)
                .ValidateCompletedDateFieldIsDisabled(true)

                .ValidateNoteTextFieldIsDisabled(false);

            #endregion

            #region Step 35 & 36

            personContractServiceRecordPage
                .ValidateDeleteRecordButtonVisibility(false)
                .ClickBackButton();

            personContractServicesPage
                .WaitForPersonContractServicesTabSectionToLoad()
                .ClickDetailsTab();

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickBackButton();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .ClickRefreshButton()
                .OpenRecord(careProviderPersonContractId_1.ToString());

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId_1.ToString());

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ValidateDeleteRecordButtonVisibility(true)
                .ClickDeleteRecordButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .ClickRefreshButton()
                .ValidateRecordPresent(careProviderPersonContractServiceId_1.ToString(), false);

            personContractServicesPage
                .WaitForPersonContractServicesTabSectionToLoad()
                .ClickDetailsTab();

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickBackButton();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .ClickRefreshButton()
                .OpenRecord(careProviderPersonContractId_2.ToString());

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .SelectRecord(careProviderPersonContractServiceId_3.ToString())
                .ValidateDeleteRecordOption(false)
                .OpenRecord(careProviderPersonContractServiceId_3.ToString());

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ClickDeleteRecordButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .ClickRefreshButton()
                .ValidateRecordPresent(careProviderPersonContractServiceId_3.ToString(), false);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4723

        [TestProperty("JiraIssueID", "ACC-4914")]
        [Description("Step(s) 37 to 39 from the original test ACC-856")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen1", "Person Contract Services")]
        [TestProperty("Screen2", "Advanced Search")]
        public void PersonContractService_UITestMethod12()
        {
            #region Care Provider Service

            var _careProviderServiceName_1 = "Care Provider Service Record";
            var code1 = dbHelper.careProviderService.GetHighestCode() + 1;
            var _careProviderServiceId_1 = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName_1, new DateTime(2020, 1, 1), code1, null, false);

            var _careProviderServiceName_2 = "Care Provider Service Record_2";
            var code2 = dbHelper.careProviderService.GetHighestCode() + 1;
            var _careProviderServiceId_2 = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName_2, new DateTime(2020, 1, 1), code2, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId_1 = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, provider2Id);

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId_1 = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractSchemeId_1, _careProviderServiceId_1, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, true);
            var CareProviderContractServiceId_2 = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractSchemeId_1, _careProviderServiceId_2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, true);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContractId_1 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractSchemeId_1, provider2Id, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(5));
            var careProviderPersonContractNumber_1 = (dbHelper.careProviderPersonContract.GetByID(careProviderPersonContractId_1, "careproviderpersoncontractnumber")["careproviderpersoncontractnumber"]).ToString();

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonId = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, "Default Person Contract Service End Reason", new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId_1 = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_1, _teamId, careProviderContractSchemeId_1, _careProviderServiceId_1, CareProviderContractServiceId_1, DateTime.Now.AddDays(-3), 1, 1, _careProviderRateUnitId, true);
            var careProviderPersonContractServiceNumber_1 = (dbHelper.careProviderPersonContractService.GetByID(careProviderPersonContractServiceId_1, "careproviderpersoncontractservicenumber")["careproviderpersoncontractservicenumber"]).ToString();


            var careProviderPersonContractServiceId_2 = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_1, _teamId, careProviderContractSchemeId_1, _careProviderServiceId_1, CareProviderContractServiceId_2, DateTime.Now.AddDays(-1), 1, 1, _careProviderRateUnitId, true);
            var careProviderPersonContractServiceNumber_2 = (dbHelper.careProviderPersonContractService.GetByID(careProviderPersonContractServiceId_2, "careproviderpersoncontractservicenumber")["careproviderpersoncontractservicenumber"]).ToString();
            dbHelper.careProviderPersonContractService.UpdateEndDateTime(careProviderPersonContractServiceId_2, DateTime.Now.AddDays(3), careProviderPersonContractServiceEndReasonId);

            #endregion

            #region Step 37

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
                .ValidateRecordPosition(2, careProviderPersonContractServiceId_1.ToString())
                .ValidateRecordPosition(1, careProviderPersonContractServiceId_2.ToString())

                .ValidateHeaderCellText(2, "Id")
                .ValidateHeaderCellText(3, "Person")
                .ValidateHeaderCellText(4, "Start Date/Time")
                .ValidateHeaderCellSortOrdedByDescending(4, "Start Date/Time")
                .ValidateHeaderCellText(5, "End Date/Time")
                .ValidateHeaderCellSortOrdedByDescending(5, "End Date/Time")

                .ValidateHeaderCellText(6, "End Reason")
                .ValidateHeaderCellText(7, "Status")
                .ValidateHeaderCellText(8, "Establishment")
                .ValidateHeaderCellText(9, "Contract Scheme")
                .ValidateHeaderCellText(10, "Funder")
                .ValidateHeaderCellText(11, "Service")
                .ValidateHeaderCellText(12, "Service Detail")
                .ValidateHeaderCellText(13, "Negotiated Rates Apply?")
                .ValidateHeaderCellText(14, "Charge Per Week")
                .ValidateHeaderCellText(15, "VAT Code")
                .ValidateHeaderCellText(16, "Net Income?")
                .ValidateHeaderCellText(17, "Modified On")
                .ValidateHeaderCellText(18, "Modified By")
                .ValidateRecordCellText(careProviderPersonContractServiceId_1.ToString(), 2, careProviderPersonContractServiceNumber_1.ToString())
                .ValidateRecordCellText(careProviderPersonContractServiceId_2.ToString(), 2, careProviderPersonContractServiceNumber_2.ToString());

            #endregion

            #region Step 39

            personContractServicesPage
                .SearchRecord(contractSchemeName)

                .ValidateRecordPosition(2, careProviderPersonContractServiceId_1.ToString())
                .ValidateRecordPosition(1, careProviderPersonContractServiceId_2.ToString())

                .ValidateHeaderCellText(2, "Id")
                .ValidateHeaderCellText(3, "Person")
                .ValidateHeaderCellText(4, "Start Date/Time")
                .ValidateHeaderCellSortOrdedByDescending(4, "Start Date/Time")
                .ValidateHeaderCellText(5, "End Date/Time")
                .ValidateHeaderCellSortOrdedByDescending(5, "End Date/Time")

                .ValidateHeaderCellText(6, "End Reason")
                .ValidateHeaderCellText(7, "Status")
                .ValidateHeaderCellText(8, "Establishment")
                .ValidateHeaderCellText(9, "Contract Scheme")
                .ValidateHeaderCellText(10, "Funder")
                .ValidateHeaderCellText(11, "Service")
                .ValidateHeaderCellText(12, "Service Detail")
                .ValidateHeaderCellText(13, "Negotiated Rates Apply?")
                .ValidateHeaderCellText(14, "Charge Per Week")
                .ValidateHeaderCellText(15, "VAT Code")
                .ValidateHeaderCellText(16, "Net Income?")
                .ValidateHeaderCellText(17, "Modified On")
                .ValidateHeaderCellText(18, "Modified By");

            #endregion

            #region Step 38

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Person Contract Services")
                .ClickSelectFilterFieldOption("1")

                .ValidateSelectFilterFieldOptionIsPresent("1", "Responsible Team")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Person Contract")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Status")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Rate Required?")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Start Date/Time")
                .ValidateSelectFilterFieldOptionIsPresent("1", "End Date/Time")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Frequency (in weeks)")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Completed Date")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Contract Scheme")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Contract Service")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Service")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Service Detail")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Suspend Debtor Invoices?")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Account Code")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Override Rate?")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Rate Verified?")

                .ValidateSelectFilterFieldOptionIsNotPresent("1", "Title")

                .SelectFilter("1", "Contract Scheme")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup Records")
                .TypeSearchQuery(contractSchemeName)
                .TapSearchButton()
                .SelectResultElement(careProviderContractSchemeId_1.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()

                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(careProviderPersonContractServiceId_1.ToString())
                .ValidateSearchResultRecordPresent(careProviderPersonContractServiceId_2.ToString())

                .ResultsPageValidateHeaderCellText(2, "Id")
                .ResultsPageValidateHeaderCellText(3, "Person")
                .ResultsPageValidateHeaderCellText(4, "Start Date/Time")
                .ResultsPageValidateHeaderCellText(5, "End Date/Time")
                .ResultsPageValidateHeaderCellText(6, "End Reason")
                .ResultsPageValidateHeaderCellText(7, "Status")
                .ResultsPageValidateHeaderCellText(8, "Establishment")
                .ResultsPageValidateHeaderCellText(9, "Contract Scheme")
                .ResultsPageValidateHeaderCellText(10, "Funder")
                .ResultsPageValidateHeaderCellText(11, "Service")
                .ResultsPageValidateHeaderCellText(12, "Service Detail")
                .ResultsPageValidateHeaderCellText(13, "Negotiated Rates Apply?")
                .ResultsPageValidateHeaderCellText(14, "Charge Per Week")
                .ResultsPageValidateHeaderCellText(15, "VAT Code")
                .ResultsPageValidateHeaderCellText(16, "Net Income?")
                .ResultsPageValidateHeaderCellText(17, "Modified On")
                .ResultsPageValidateHeaderCellText(18, "Modified By")

                .SelectSearchResultRecord(careProviderPersonContractServiceId_2.ToString())
                .ClickDeleteRecordButtonFromResultPage();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("1 item(s) deleted.")
                .TapOKButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ClickNewRecordButton_ResultsPage();

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoadFromAdvancedSearch()
                .ClickPersonContractLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(careProviderPersonContractNumber_1.ToString(), careProviderPersonContractId_1);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoadFromAdvancedSearch()
                .ClickServiceLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(_careProviderServiceName_1, _careProviderServiceId_1);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoadFromAdvancedSearch()
                .InsertStartDate(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .InsertStartTime("10:00")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ClickSaveAndCloseButton();

            var careProviderPersonContractServiceId_3 = (dbHelper.careProviderPersonContractService.GetByPersonContractId(careProviderPersonContractId_1).FirstOrDefault());

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(careProviderPersonContractServiceId_3.ToString());

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
                .ValidateRecordPresent(careProviderPersonContractServiceId_1.ToString(), true)
                .ValidateRecordPresent(careProviderPersonContractServiceId_2.ToString(), false)
                .ValidateRecordPresent(careProviderPersonContractServiceId_3.ToString(), true);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4795

        [TestProperty("JiraIssueID", "ACC-4915")]
        [Description("Step(s) 40 to 43 from the original test ACC-856")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen1", "Person Contracts")]
        [TestProperty("Screen2", "Person Contract Services")]
        public void PersonContractService_UITestMethod13()
        {
            #region Care Provider Service

            var _careProviderServiceName_1 = "Care Provider Service Record";
            var _careProviderServiceId_1 = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName_1, new DateTime(2020, 1, 1), 7895, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId_1 = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, provider2Id);

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId_1 = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractSchemeId_1, _careProviderServiceId_1, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, true);
            var CareProviderContractServiceId_2 = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractSchemeId_1, _careProviderServiceId, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContractId_1 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractSchemeId_1, provider2Id, DateTime.Now.AddDays(-5));

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonId = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, "Default Person Contract Service End Reason", new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Care Provider Person Contract Service

            DateTime StartDate = Convert.ToDateTime(DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd 08:50:00"));
            DateTime EndDate = Convert.ToDateTime(DateTime.Now.AddDays(3).ToString("yyyy-MM-dd 11:55:00"));

            var careProviderPersonContractServiceId_1 = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_1, _teamId, careProviderContractSchemeId_1, _careProviderServiceId_1, CareProviderContractServiceId_1, StartDate, 1, 1, _careProviderRateUnitId, true);
            dbHelper.careProviderPersonContractService.UpdateEndDateTime(careProviderPersonContractServiceId_1, EndDate, careProviderPersonContractServiceEndReasonId);

            #endregion

            #region Step 40

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
                .ValidateRecordPresent(careProviderPersonContractId_1.ToString(), true)
                .ClickNewRecordButton();

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickResponsibleTeamLookupButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .SelectViewByText("Lookup View")
                 .TypeSearchQuery(_teamName)
                 .TapSearchButton()
                 .SelectResultElement(_teamId.ToString());

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickEstablishmentLookupButton();


            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery(providerName)
                 .TapSearchButton()
                 .SelectResultElement(providerId.ToString());

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ValidateFunderLinkText(provider2Name)
                .ValidateContractSchemeLinkText(contractSchemeName)
                .InsertTextOnStartDate(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("There are already existing records for this Person with the same Contract Scheme which overlap. Change the Contract Scheme or Start / End date values to avoid overlap.")
                .TapCloseButton();

            #endregion

            #region Step 41

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(careProviderPersonContractId_1.ToString());

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .InsertTextOnStartDate(DateTime.Now.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Start date must be earlier than or equal to " + StartDate.ToString("dd'/'MM'/'yyyy HH:mm:ss"))
                .TapCloseButton();

            #endregion

            #region Step 42

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .InsertTextOnStartDate(DateTime.Now.AddDays(-5).ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnEndDateTime(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("End date must be later than or equal to " + EndDate.ToString("dd'/'MM'/'yyyy HH:mm:ss"))
                .TapCloseButton();

            #endregion

            #region Step 43

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId_1.ToString());

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ValidateRateRequired_OptionIsCheckedOrNot(true)
                .ValidateRatesTabIsVisible(true)
                .ClickBackButton();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .ClickNewRecordButton();

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ClickServiceLookupButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery(_careProviderServiceName)
                 .TapSearchButton()
                 .SelectResultElement(_careProviderServiceId.ToString());

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .InsertStartDate(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved()

                .ValidateRateRequired_OptionIsCheckedOrNot(false)
                .ValidateRatesTabIsVisible(false);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4916")]
        [Description("Step(s) 44 to 47 from the original test ACC-856")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Person Contract Services")]
        public void PersonContractService_UITestMethod14()
        {
            #region Care Provider Service

            var _careProviderServiceName_1 = "Care Provider Service Record";
            var _careProviderServiceId_1 = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName_1, new DateTime(2020, 1, 1), 7895, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId_1 = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, provider2Id);

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId_1 = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractSchemeId_1, _careProviderServiceId_1, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, true);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContractId_1 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractSchemeId_1, provider2Id, DateTime.Now.AddDays(-5));

            #endregion

            #region Care Provider Person Contract Service

            DateTime StartDate = Convert.ToDateTime(DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd 08:50:00"));
            var careProviderPersonContractServiceId_1 = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_1, _teamId, careProviderContractSchemeId_1, _careProviderServiceId_1, CareProviderContractServiceId_1, StartDate, 1, 1, _careProviderRateUnitId, true);

            dbHelper.careProviderPersonContractServiceRatePeriod.CreateCareProviderPersonContractServiceRatePeriod(_teamId, careProviderPersonContractServiceId_1, _careProviderRateUnitId, DateTime.Now.AddDays(-1), 10, "");

            #endregion

            #region Step 44

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                 .WaitForReferenceDataPageToLoad()
                 .InsertSearchQuery("Person Contract Service Statuses")
                 .TapSearchButton()
                 .ValidateReferenceDataElementVisibility("Address Borough", false);

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
                .ValidateSelectedStatusPickListValue("In Progress")
                .ValidateStatusPicklist_FieldOptionIsPresent("Completed")
                .ValidateStatusPicklist_FieldOptionIsPresent("Cancelled");

            #endregion

            #region Step 45

            personContractServiceRecordPage
                .ValidateStatusPickList_OptionDisabled("In Progress", false)
                .ValidateStatusPickList_OptionDisabled("Completed", false)
                .ValidateStatusPickList_OptionDisabled("Cancelled", true)

                .SelectStatus("Completed")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            personContractServiceRecordPage
                .ValidateCompletedByMandatoryFieldVisibility(true)
                .ValidateCompletedDateMandatoryFieldVisibility(true)

                .ValidateSelectedStatusPickListValue("Completed")

                .ValidateStatusPickList_OptionDisabled("In Progress", true)
                .ValidateStatusPickList_OptionDisabled("Completed", false)
                .ValidateStatusPickList_OptionDisabled("Cancelled", false);

            #endregion

            #region Step 46

            personContractServiceRecordPage
                .SelectStatus("Cancelled")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            personContractServiceRecordPage
                .ValidateCompletedByMandatoryFieldVisibility(false)
                .ValidateCompletedDateMandatoryFieldVisibility(false)

                .ValidateIdFieldIsDisabled(true)
                .ValidateResponsibleTeamLookupButtonIsDisabled(true)
                .ValidatePersonContractLookupButtonIsDisabled(true)

                .ValidateContractServiceLookupButtonIsDisabled(true)
                .ValidateServiceLookupButtonIsDisabled(true)
                .ValidateServiceDetailLookupButtonIsDisabled(true)
                .ValidateContractServiceLookupButtonIsDisabled(true)
                .ValidateFinanceCodeFieldIsDisabled(true)
                .ValidateUpdateFinanceCodeOptionsIsDisabled(true)
                .ValidateAccountCodeFieldIsDisabled(true)

                .ValidateStartDateTimeFieldIsDisabled(true)
                .ValidateEndDateTimeFieldIsDisabled(true)
                .ValidateStatusPicklistIsDisabled(true)
                .ValidateChargePerWeekFieldIsDisabled(true)

                .ValidateFrequencyInWeeksFieldIsDisabled(true)
                .ValidateRateRequiredOptionsIsDisabled(true)
                .ValidateOverrideRateOptionsIsDisabled(true)
                .ValidateRateVerifiedOptionsIsDisabled(true)
                .ValidateSuspendDebtorInvoicesReasonLookupButtonIsDisabled(true)

                .ValidateCompletedByLookupButtonIsDisabled(true)
                .ValidateCompletedDateFieldIsDisabled(true)

                .ValidateNoteTextFieldIsDisabled(true);

            #endregion

            #region Step 47

            personContractServiceRecordPage
                .NavigateToAuditPage();

            var CPServices = dbHelper.careProviderPersonContractService.GetByPersonContractId(careProviderPersonContractId_1);
            Assert.AreEqual(1, CPServices.Count);

            var auditSearch1 = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 2, //update operation
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = CPServices[0].ToString(),
                ParentTypeName = "CareProviderPersonContractService",
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
            var auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch1, WebAPIHelper.Security.AuthenticationCookie);
            Assert.AreEqual("Updated", auditResponseData.GridData[0].cols[0].Text);
            Assert.AreEqual(_systemUserFullName, auditResponseData.GridData[0].cols[1].Text);

            var auditRecordId1 = auditResponseData.GridData[0].Id;
            var auditRecordId_2 = auditResponseData.GridData[1].Id;

            auditListPage
                .WaitForAuditListPageToLoad("careproviderpersoncontractservice")
                .ValidateCellText(1, 2, "Updated")
                .ValidateCellText(2, 2, "Updated")
                .ValidateCellText(3, 2, "Updated")
                .ValidateCellText(4, 2, "Created")
                .ValidateCellText(5, 2, "Updated");

            auditListPage
                .WaitForAuditListPageToLoad("careproviderpersoncontractservice")
                .ValidateRecordPresent(auditRecordId1)
                .ClickOnAuditRecord(auditRecordId1);

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Updated")
                .ValidateChangedBy(_systemUserFullName)

                .ValidateFieldNameCellText(1, "Status")
                .ValidateOldValueCellText(1, "Completed")
                .ValidateNewValueCellText(1, "Cancelled")
                .TapCloseButton();

            auditListPage
                .WaitForAuditListPageToLoad("careproviderpersoncontractservice")
                .ValidateRecordPresent(auditRecordId_2)
                .ClickOnAuditRecord(auditRecordId_2);

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Updated")
                .ValidateChangedBy(_systemUserFullName)
                .ValidateFieldNameCellText(1, "Completed Date")
                .ValidateOldValueCellText(1, "")
                .ValidateNewValueCellText(1, DateTime.Now.ToString("dd'/'MM'/'yyyy"))

                .ValidateFieldNameCellText(2, "Status")
                .ValidateOldValueCellText(2, "In Progress")
                .ValidateNewValueCellText(2, "Completed")

                .ValidateFieldNameCellText(3, "Completed By")
                .ValidateOldValueCellText(3, "")
                .ValidateNewValueCellText(3, _systemUserFullName)
                .TapCloseButton();


            var auditSearch2 = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 1, //create operation
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = CPServices[0].ToString(),
                ParentTypeName = "CareProviderPersonContractService",
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
            var auditResponseData2 = WebAPIHelper.Audit.RetrieveAudits(auditSearch2, WebAPIHelper.Security.AuthenticationCookie);
            Assert.AreEqual("Created", auditResponseData2.GridData[0].cols[0].Text);
            Assert.AreEqual(_defaultSystemUserFullName, auditResponseData2.GridData[0].cols[1].Text);
            var createdOnDate = auditResponseData2.GridData[0].cols[2].Text;

            var auditRecordId2 = auditResponseData2.GridData[0].Id;

            auditListPage
                .WaitForAuditListPageToLoad("careproviderpersoncontractservice")
                .ValidateRecordPresent(auditRecordId2)
                .ClickOnAuditRecord(auditRecordId2);

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Created")
                .ValidateChangedBy(_defaultSystemUserFullName)
                .ValidateChangedOn(createdOnDate)
                .TapCloseButton();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4917")]
        [Description("Step(s) 48 to 49 from the original test ACC-856")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Person Contract Services")]
        public void PersonContractService_UITestMethod15()
        {
            #region Provider

            var provider3Name = "Residential Establishment_3_" + _currentDateSuffix;
            var provider3Type = 13; //Residential Establishment
            var provider3Id = commonMethodsDB.CreateProvider(provider3Name, _teamId, provider3Type, false);

            #endregion

            #region Care Provider Service

            var _careProviderServiceName_1 = "Care Provider Service Record";
            var _careProviderServiceId_1 = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName_1, new DateTime(2020, 1, 1), 7895, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId_1 = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, provider2Id);


            var contractSchemeName_2 = "Default2_" + _currentDateSuffix;
            var contractSchemeCode_2 = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode_2).Any())
                contractSchemeCode_2 = contractSchemeCode_2 + 1;

            var careProviderContractSchemeId_2 = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName_2, new DateTime(2020, 1, 1), contractSchemeCode_2, providerId, provider3Id);

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId_1 = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractSchemeId_1, _careProviderServiceId_1, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, true);
            var CareProviderContractServiceId_2 = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider3Id, careProviderContractSchemeId_2, _careProviderServiceId_1, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, true);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContractId_1 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractSchemeId_1, provider2Id, DateTime.Now.AddDays(-5));
            var careProviderPersonContractId_2 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractSchemeId_2, provider3Id, DateTime.Now.AddDays(-5));

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonId = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, "Default Person Contract Service End Reason", new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Care Provider Person Contract Service

            DateTime StartDate = Convert.ToDateTime(DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd 08:50:00"));
            DateTime EndDate = Convert.ToDateTime(DateTime.Now.AddDays(5).ToString("yyyy-MM-dd 11:55:00"));

            var careProviderPersonContractServiceId_1 = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_1, _teamId, careProviderContractSchemeId_1, _careProviderServiceId_1, CareProviderContractServiceId_1, StartDate, 1, 1, _careProviderRateUnitId, true);
            dbHelper.careProviderPersonContractService.UpdateEndDateTime(careProviderPersonContractServiceId_1, EndDate, careProviderPersonContractServiceEndReasonId);
            dbHelper.careProviderPersonContractServiceRatePeriod.CreateCareProviderPersonContractServiceRatePeriod(_teamId, careProviderPersonContractServiceId_1, _careProviderRateUnitId, DateTime.Now.AddDays(-1), 12, "");
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId_1, 90); //Completed = 90
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId_1, 99); //Cancelled = 99

            var careProviderPersonContractServiceId_2 = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_1, _teamId, careProviderContractSchemeId_1, _careProviderServiceId_1, CareProviderContractServiceId_1, StartDate, 1, 1, _careProviderRateUnitId, true);
            dbHelper.careProviderPersonContractServiceRatePeriod.CreateCareProviderPersonContractServiceRatePeriod(_teamId, careProviderPersonContractServiceId_2, _careProviderRateUnitId, DateTime.Now.AddDays(-1), 10, "");
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId_2, 90); //Completed = 90

            var careProviderPersonContractServiceId_3 = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_1, _teamId, careProviderContractSchemeId_1, _careProviderServiceId_1, CareProviderContractServiceId_1, StartDate, 1, 1, _careProviderRateUnitId, true);

            var careProviderPersonContractServiceId_4 = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_2, _teamId, careProviderContractSchemeId_2, _careProviderServiceId_1, CareProviderContractServiceId_2, StartDate, 1, 1, _careProviderRateUnitId, true);

            #endregion

            #region Step 48

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPersonContractServicesSection();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad(false)
                .ValidateSelectedViewPicklistText("All Records")

                .ValidateHeaderCellText(2, "Id")
                .ValidateHeaderCellText(3, "Person")
                .ValidateHeaderCellText(4, "Start Date/Time")
                .ValidateHeaderCellSortOrdedByDescending(4, "Start Date/Time")
                .ValidateHeaderCellText(5, "End Date/Time")
                .ValidateHeaderCellSortOrdedByDescending(5, "End Date/Time")

                .ValidateHeaderCellText(6, "End Reason")
                .ValidateHeaderCellText(7, "Status")
                .ValidateHeaderCellText(8, "Establishment")
                .ValidateHeaderCellText(9, "Contract Scheme")
                .ValidateHeaderCellText(10, "Funder")
                .ValidateHeaderCellText(11, "Service")
                .ValidateHeaderCellText(12, "Service Detail")
                .ValidateHeaderCellText(13, "Negotiated Rates Apply?")
                .ValidateHeaderCellText(14, "Charge Per Week")
                .ValidateHeaderCellText(15, "VAT Code")
                .ValidateHeaderCellText(16, "Net Income?")
                .ValidateHeaderCellText(17, "Modified On")
                .ValidateHeaderCellText(18, "Modified By")
                .ClickNewRecordButton();

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ValidatePersonContractLinkText("")
                .ValidatePersonContractLookupButtonIsDisabled(false)
                .ClickBackButton();

            #endregion

            #region Step 49

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad(false)
                .SelectViewSelector("Cancelled")

                .WaitForPersonContractServicesPageToLoad(false)
                .GridHeaderIdFieldLink() //Sort by Id ascending

                .WaitForPersonContractServicesPageToLoad(false)
                .GridHeaderIdFieldLink() //Sort by Id descending

                .ValidateRecordPresent(careProviderPersonContractServiceId_1.ToString(), true)
                .ValidateRecordCellText(careProviderPersonContractServiceId_1.ToString(), 3, _person_FullName)
                .ValidateRecordCellText(careProviderPersonContractServiceId_1.ToString(), 7, "Cancelled");

            personContractServicesPage
                .SelectViewSelector("Completed")

                .WaitForPersonContractServicesPageToLoad(false)
                .GridHeaderIdFieldLink() //Sort by Id ascending

                .WaitForPersonContractServicesPageToLoad(false)
                .GridHeaderIdFieldLink() //Sort by Id descending

                .ValidateRecordPresent(careProviderPersonContractServiceId_2.ToString(), true)
                .ValidateRecordCellText(careProviderPersonContractServiceId_2.ToString(), 3, _person_FullName)
                .ValidateRecordCellText(careProviderPersonContractServiceId_2.ToString(), 7, "Completed");

            personContractServicesPage
                .SelectViewSelector("In Progress")

                .WaitForPersonContractServicesPageToLoad(false)
                .GridHeaderIdFieldLink() //Sort by Id ascending

                .WaitForPersonContractServicesPageToLoad(false)
                .GridHeaderIdFieldLink() //Sort by Id descending

                .ValidateRecordPresent(careProviderPersonContractServiceId_3.ToString(), true)
                .ValidateRecordCellText(careProviderPersonContractServiceId_3.ToString(), 3, _person_FullName)
                .ValidateRecordCellText(careProviderPersonContractServiceId_3.ToString(), 7, "In Progress")

                .ValidateRecordPresent(careProviderPersonContractServiceId_4.ToString(), true)
                .ValidateRecordCellText(careProviderPersonContractServiceId_4.ToString(), 3, _person_FullName)
                .ValidateRecordCellText(careProviderPersonContractServiceId_4.ToString(), 7, "In Progress");

            personContractServicesPage
                .SelectViewSelector("Funded Care")

                .WaitForPersonContractServicesPageToLoad(false)
                .GridHeaderIdFieldLink() //Sort by Id ascending

                .WaitForPersonContractServicesPageToLoad(false)
                .GridHeaderIdFieldLink() //Sort by Id descending

                .ValidateRecordPresent(careProviderPersonContractServiceId_1.ToString(), true)
                .ValidateRecordCellText(careProviderPersonContractServiceId_1.ToString(), 3, _person_FullName)
                .ValidateRecordCellText(careProviderPersonContractServiceId_1.ToString(), 7, "Cancelled")

                .ValidateRecordPresent(careProviderPersonContractServiceId_2.ToString(), true)
                .ValidateRecordCellText(careProviderPersonContractServiceId_2.ToString(), 3, _person_FullName)
                .ValidateRecordCellText(careProviderPersonContractServiceId_2.ToString(), 7, "Completed")

                .ValidateRecordPresent(careProviderPersonContractServiceId_3.ToString(), true)
                .ValidateRecordCellText(careProviderPersonContractServiceId_3.ToString(), 3, _person_FullName)
                .ValidateRecordCellText(careProviderPersonContractServiceId_3.ToString(), 7, "In Progress");

            personContractServicesPage
                .SelectViewSelector("Private Care")

                .WaitForPersonContractServicesPageToLoad(false)
                .GridHeaderIdFieldLink() //Sort by Id ascending

                .WaitForPersonContractServicesPageToLoad(false)
                .GridHeaderIdFieldLink() //Sort by Id descending

                .ValidateRecordPresent(careProviderPersonContractServiceId_4.ToString(), true)
                .ValidateRecordCellText(careProviderPersonContractServiceId_4.ToString(), 3, _person_FullName)
                .ValidateRecordCellText(careProviderPersonContractServiceId_4.ToString(), 7, "In Progress");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4918")]
        [Description("Step(s) 50 from the original test ACC-856")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("BusinessModule2", "System Features")]
        [TestProperty("Screen1", "Person Contracts")]
        [TestProperty("Screen2", "Person Contract Services")]
        [TestProperty("Screen3", "Pinned Records")]
        public void PersonContractService_UITestMethod16()
        {
            #region Care Provider Service

            var _careProviderServiceName_1 = "Care Provider Service Record";
            var _careProviderServiceId_1 = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName_1, new DateTime(2020, 1, 1), 7895, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId_1 = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, provider2Id);

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId_1 = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractSchemeId_1, _careProviderServiceId_1, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, true);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContractId_1 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractSchemeId_1, provider2Id, DateTime.Now.AddDays(-5));

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId_1 = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_1, _teamId, careProviderContractSchemeId_1, _careProviderServiceId_1, CareProviderContractServiceId_1, DateTime.Now.AddDays(-5), 1, 1, _careProviderRateUnitId, true);
            var careProviderPersonContractServiceTitle_1 = (dbHelper.careProviderPersonContractService.GetByID(careProviderPersonContractServiceId_1, "title")["title"]).ToString();

            var careProviderPersonContractServiceId_2 = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_1, _teamId, careProviderContractSchemeId_1, _careProviderServiceId_1, CareProviderContractServiceId_1, DateTime.Now.AddDays(-3), 1, 1, _careProviderRateUnitId, true);

            #endregion

            #region Step 50

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPersonContractServicesSection();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad(false)
                .GridHeaderIdFieldLink() //Sort by Id ascending

                .WaitForPersonContractServicesPageToLoad(false)
                .GridHeaderIdFieldLink() //Sort by Id descending

                .ValidateRecordPresent(careProviderPersonContractServiceId_1.ToString(), true);

            personContractServicesPage
                .SelectRecord(careProviderPersonContractServiceId_1.ToString())
                .SelectRecord(careProviderPersonContractServiceId_2.ToString())
                .ClickPinButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();

            mainMenuPinnedRecordsArea
                .WaitForPinnedRecordsAreaToLoad()
                .ValidatePinnedRecordsAreaLinkElementVisible(careProviderPersonContractServiceId_1.ToString(), true)
                .ValidatePinnedRecordsAreaLinkElementVisible(careProviderPersonContractServiceId_2.ToString(), true);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad(false)
                .UnCheckRecord(careProviderPersonContractServiceId_1.ToString())
                .ClickUnpinButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();

            mainMenuPinnedRecordsArea
                .WaitForPinnedRecordsAreaToLoad()
                .ValidatePinnedRecordsAreaLinkElementVisible(careProviderPersonContractServiceId_1.ToString(), true)
                .ValidatePinnedRecordsAreaLinkElementVisible(careProviderPersonContractServiceId_2.ToString(), false)
                .OpenPinnedRecord(careProviderPersonContractServiceId_1.ToString());

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad(false)
                .ValidatePageHeaderText(careProviderPersonContractServiceTitle_1)
                .ClickUnpinFromMeButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();

            mainMenuPinnedRecordsArea
                .WaitForPinnedRecordsAreaToLoad()
                .ValidatePinnedRecordsAreaLinkElementVisible(careProviderPersonContractServiceId_1.ToString(), false);

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
                .SelectRecord(careProviderPersonContractServiceId_1.ToString())
                .ClickPinButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();

            mainMenuPinnedRecordsArea
                .WaitForPinnedRecordsAreaToLoad()
                .ValidatePinnedRecordsAreaLinkElementVisible(careProviderPersonContractServiceId_1.ToString(), true);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .SelectRecord(careProviderPersonContractServiceId_1.ToString())
                .ClickUnpinButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();

            mainMenuPinnedRecordsArea
                .WaitForPinnedRecordsAreaToLoad()
                .ValidatePinnedRecordsAreaLinkElementVisible(careProviderPersonContractServiceId_1.ToString(), false);

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId_2.ToString());

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ClickPinToMeButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();

            mainMenuPinnedRecordsArea
                .WaitForPinnedRecordsAreaToLoad()
                .ValidatePinnedRecordsAreaLinkElementVisible(careProviderPersonContractServiceId_2.ToString(), true);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ClickUnpinFromMeButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();

            mainMenuPinnedRecordsArea
                .WaitForPinnedRecordsAreaToLoad()
                .ValidatePinnedRecordsAreaLinkElementVisible(careProviderPersonContractServiceId_2.ToString(), false);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4919")]
        [Description("Step(s) 51 to 55 from the original test ACC-856")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Person Contract Services")]
        // Resolved Bug - https://advancedcsg.atlassian.net/browse/ACC-5097
        public void PersonContractService_UITestMethod17()
        {
            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId_1 = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, provider2Id);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailName1 = "Care Provider Service Details Test 1";
            var careProviderServiceDetailId1 = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName1, Code1, null, new DateTime(2020, 1, 1));

            var Code2 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code2).Any())
                Code2 = Code2 + 1;

            var careProviderServiceDetailName2 = "Care Provider Service Details Test 2";
            var careProviderServiceDetailId2 = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName2, Code2, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(_careProviderServiceId, _teamId, careProviderServiceDetailId1, null, null, "");
            commonMethodsDB.CreateCareProviderServiceMapping(_careProviderServiceId, _teamId, careProviderServiceDetailId2, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId_1 = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractSchemeId_1, _careProviderServiceId, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, true);


            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContractId_1 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractSchemeId_1, provider2Id, DateTime.Now.AddDays(-5));

            #endregion

            #region Care Provider Person Contract Service

            DateTime StartDate = Convert.ToDateTime(DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd 08:50:00"));
            var careProviderPersonContractServiceId_1 = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_1, _teamId, careProviderContractSchemeId_1, _careProviderServiceId, careProviderServiceDetailId1, null, StartDate, 1, 1, _careProviderRateUnitId, true);

            #endregion

            #region Step 51

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
                .ValidateSelectedStatusPickListValue("In Progress")
                .ValidateServiceLookupButtonIsDisabled(false)
                .ValidateServiceDetailLookupButtonIsDisabled(false)
                .ValidateStartDateTimeFieldIsDisabled(false)
                .ValidateEndDateTimeFieldIsDisabled(false)
                .ValidateExpectedEndDateTimeFieldIsDisabled(false)
                .ValidateStatusPicklistIsDisabled(false)
                .ValidateFrequencyInWeeksFieldIsDisabled(true)
                .ValidateNoteTextFieldIsDisabled(false);

            #endregion

            #region Step 52

            // this step is already covered in Step 34

            #endregion

            #region Step 53

            // this step is already covered in Step 46

            #endregion

            #region Step 54

            personContractServiceRecordPage
                .SelectStatus("Completed")
                .ValidateSelectedStatusPickListValue("Completed")

                .SelectStatus("In Progress")
                .ValidateSelectedStatusPickListValue("In Progress");

            #endregion

            #region Step 55

            personContractServiceRecordPage
                .SelectStatus("Completed")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("The Status = Completed can not be saved as the Contract Service value is blank. Please change the combination of Service / Service Detail used")
                .TapCloseButton();

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4796

        [TestProperty("JiraIssueID", "ACC-4920")]
        [Description("Step(s) 56 to 58 from the original test ACC-856")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Person Contract Services")]
        public void PersonContractService_UITestMethod18()
        {
            #region Care Provider Rate Unit

            _careProviderRateUnitName = "Rate Unit One-Off is Yes";
            var careProviderRateUnitCode = dbHelper.careProviderRateUnit.GetAll().Count + 1;
            while (dbHelper.careProviderRateUnit.GetByCode(careProviderRateUnitCode).Any())
                careProviderRateUnitCode = careProviderRateUnitCode + 1;

            _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, _careProviderRateUnitName, new DateTime(2020, 1, 1), careProviderRateUnitCode, false, false, true);
            dbHelper.careProviderRateUnit.UpdateOneOff(_careProviderRateUnitId, true);

            #endregion

            #region Care Provider Service

            var _careProviderServiceName_1 = "Care Provider Service Record";
            var _careProviderServiceId_1 = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName_1, new DateTime(2020, 1, 1), 7895, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId_1 = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, provider2Id);

            #endregion

            #region Care Provider Contract Service

            dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractSchemeId_1, _careProviderServiceId_1, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, true);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContractId_1 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractSchemeId_1, provider2Id, DateTime.Now.AddDays(-4));
            var careProviderPersonContractTitle = (dbHelper.careProviderPersonContract.GetByID(careProviderPersonContractId_1, "title")["title"]).ToString();

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonName = "Default Person Contract Service End Reason";
            var careProviderPersonContractServiceEndReasonId = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, careProviderPersonContractServiceEndReasonName, new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Step 56

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
                .ClickNewRecordButton();

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ClickServiceLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(_careProviderServiceName_1, _careProviderServiceId_1);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .InsertStartDate(DateTime.Now.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
                .InsertStartTime("10:00")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("It is mandatory to record an end date when using Rate Unit with One-Off set to Yes, as this indicates the charge is a one-off. Please correct as necessary.")
                .TapCloseButton();

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .InsertEndDate(DateTime.Now.AddDays(3).ToString("dd'/'MM'/'yyyy"))
                .ClickEndReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(careProviderPersonContractServiceEndReasonName, careProviderPersonContractServiceEndReasonId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .InsertStartDate(DateTime.Now.AddDays(-6).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Start date cannot be before Start Date of parent Person Contract. Please choose a value after or equal to " + DateTime.Now.AddDays(-4).ToString("dd'/'MM'/'yyyy"))
                .TapCloseButton();

            #endregion

            #region Step 57

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .InsertStartDate(DateTime.Now.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
                .InsertEndDate(DateTime.Now.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
                .ClickEndReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(careProviderPersonContractServiceEndReasonName, careProviderPersonContractServiceEndReasonId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("End Date/Time cannot be before Start Date/Time")
                .TapCloseButton();

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .InsertEndDate(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .InsertEndTime("11:55")
                .ClickEndReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(careProviderPersonContractServiceEndReasonName, careProviderPersonContractServiceEndReasonId);

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateIdFieldIsBlank(false)
                .GetIdFieldValueAndValidateTitle(careProviderPersonContractTitle);

            #endregion

            #region Step 58

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.GetByPersonContractId(careProviderPersonContractId_1);
            dbHelper.careProviderPersonContractServiceRatePeriod.CreateCareProviderPersonContractServiceRatePeriod(_teamId, careProviderPersonContractServiceId[0], _careProviderRateUnitId, DateTime.Now.AddDays(-1), 12, "");

            personContractServiceRecordPage
                .SelectStatus("Completed")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateSelectedStatusPickListValue("Completed");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4921")]
        [Description("Step(s) 59 to 60 from the original test ACC-856")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("BusinessModule2", "System Features")]
        [TestProperty("Screen", "Person Contract Services")]
        public void PersonContractService_UITestMethod19()
        {
            #region Care Provider Service

            var _careProviderServiceName_1 = "Care Provider Service Record";
            var _careProviderServiceId_1 = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName_1, new DateTime(2020, 1, 1), 7895, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId_1 = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, provider2Id);

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId_1 = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractSchemeId_1, _careProviderServiceId_1, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, true);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContractId_1 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractSchemeId_1, provider2Id, DateTime.Now.AddDays(-5));

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonId = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, "Default Person Contract Service End Reason", new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Suspend Debtor Invoices Reason

            var suspendDebtorInvoicesReasonName_1 = "Reason First";
            var suspendDebtorInvoicesReasonId_1 = commonMethodsDB.CreateCPSuspendDebtorInvoicesReason(_teamId, suspendDebtorInvoicesReasonName_1, new DateTime(2023, 1, 1), 121);
            var suspendDebtorInvoicesReasonId_2 = commonMethodsDB.CreateCPSuspendDebtorInvoicesReason(_teamId, "Reason Second", new DateTime(2023, 1, 1), 122);

            #endregion

            #region Care Provider Person Contract Service

            DateTime StartDate_1 = Convert.ToDateTime(DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd 08:50:00"));
            DateTime StartDate_2 = Convert.ToDateTime(DateTime.Now.AddDays(-4).ToString("yyyy-MM-dd 09:50:00"));
            DateTime StartDate_3 = Convert.ToDateTime(DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd 10:50:00"));

            var careProviderPersonContractServiceId_1 = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_1, _teamId, careProviderContractSchemeId_1, _careProviderServiceId_1, CareProviderContractServiceId_1, StartDate_1, 1, 1, _careProviderRateUnitId, true);
            dbHelper.careProviderPersonContractService.UpdateSuspendDebtorInvoicesAndReason(careProviderPersonContractServiceId_1, true, suspendDebtorInvoicesReasonId_1);

            var careProviderPersonContractServiceId_2 = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_1, _teamId, careProviderContractSchemeId_1, _careProviderServiceId_1, CareProviderContractServiceId_1, StartDate_2, 1, 1, _careProviderRateUnitId, true);
            dbHelper.careProviderPersonContractService.UpdateSuspendDebtorInvoicesAndReason(careProviderPersonContractServiceId_2, true, suspendDebtorInvoicesReasonId_2);

            var careProviderPersonContractServiceId_3 = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_1, _teamId, careProviderContractSchemeId_1, _careProviderServiceId_1, CareProviderContractServiceId_1, StartDate_3, 1, 1, _careProviderRateUnitId, true);
            dbHelper.careProviderPersonContractService.UpdateSuspendDebtorInvoicesAndReason(careProviderPersonContractServiceId_3, true, suspendDebtorInvoicesReasonId_1);

            #endregion

            #region Audit Reasons

            var _auditReasonName = "Default Change Bulk Edit";
            var _auditReasonId = commonMethodsDB.CreateErrorManagementReason(_auditReasonName, new DateTime(2020, 1, 1), 3, _teamId, false);

            #endregion

            #region Step 59

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
                .ValidateSuspendDebtorInvoices_OptionIsCheckedOrNot(true)
                .ValidateSuspendDebtorInvoicesReasonLinkText(suspendDebtorInvoicesReasonName_1)
                .ClickBackButton();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .SelectRecord(careProviderPersonContractServiceId_1.ToString())
                .SelectRecord(careProviderPersonContractServiceId_2.ToString())
                .SelectRecord(careProviderPersonContractServiceId_3.ToString())
                .ClickBulkEditButton();

            bulkEditDialogPopup
                .WaitForBulkEditDialogPopupToLoad("3", false)

                .ValidateUpdateCheckBoxIsVisible("enddatetime")
                .ValidateFieldTitle("enddatetime", "End Date/Time")
                .ValidateInputFieldIsVisible("enddatetime")
                .ValidateInputFieldIsVisible("enddatetime_Time")

                .ValidateUpdateCheckBoxIsVisible("careproviderpersoncontractserviceendreasonid")
                .ValidateFieldTitle("careproviderpersoncontractserviceendreasonid", "End Reason")
                .ValidateLookupButtonIsVisible("careproviderpersoncontractserviceendreasonid")

                .ValidateUpdateCheckBoxIsVisible("cpsuspenddebtorinvoicesreasonid")
                .ValidateFieldTitle("cpsuspenddebtorinvoicesreasonid", "Suspend Debtor Invoices Reason")
                .ValidateLookupButtonIsVisible("cpsuspenddebtorinvoicesreasonid")

                .ValidateUpdateCheckBoxIsVisible("cpsuspenddebtorinvoices")
                .ValidateFieldTitle("cpsuspenddebtorinvoices", "Suspend Debtor Invoices?")
                .ValidateRadioButtonOptionsIsVisible("cpsuspenddebtorinvoices");

            #endregion

            #region Step 60

            bulkEditDialogPopup
                .ClickUpdateCheckBox("cpsuspenddebtorinvoices")
                .ClickNoRadioButtonField("cpsuspenddebtorinvoices")
                .ClickAuditReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_auditReasonName).TapSearchButton().SelectResultElement(_auditReasonId);

            bulkEditDialogPopup
                .WaitForBulkEditDialogPopupToReload("3")
                .ClickUpdateButton();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId_1.ToString());

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ValidateSuspendDebtorInvoices_OptionIsCheckedOrNot(false)
                .ValidateSuspendDebtorInvoicesReasonLinkText("")
                .ClickBackButton();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId_2.ToString());

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ValidateSuspendDebtorInvoices_OptionIsCheckedOrNot(false)
                .ValidateSuspendDebtorInvoicesReasonLinkText("");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4922")]
        [Description("Step(s) 61 to 62 from the original test ACC-856")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen1", "Person Contract Services")]
        [TestProperty("Screen2", "Person Contract Service Rate Periods")]
        public void PersonContractService_UITestMethod20()
        {
            #region Care Provider Service

            var _careProviderServiceName_1 = "Care Provider Service Record";
            var _careProviderServiceId_1 = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName_1, new DateTime(2020, 1, 1), 7895, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId_1 = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, provider2Id);

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId_1 = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractSchemeId_1, _careProviderServiceId_1, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, true);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContractId_1 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractSchemeId_1, provider2Id, DateTime.Now.AddDays(-5));

            #endregion

            #region Care Provider Person Contract Service

            DateTime StartDate = Convert.ToDateTime(DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd 08:50:00"));
            var careProviderPersonContractServiceId_1 = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_1, _teamId, careProviderContractSchemeId_1, _careProviderServiceId_1, CareProviderContractServiceId_1, StartDate, 1, 1, _careProviderRateUnitId, true);

            #endregion

            #region Step 61

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
                .InsertStartDate(DateTime.Now.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
                .InsertEndDate(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .InsertRate("15")
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(1000);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId_1, 90); // Completed

            personContractServiceRatePeriodsPage
                .WaitForPersonContractServiceRatePeriodsTabSectionToLoad()
                .ClickChargePerWeekTab();

            var careProviderPersonContractServiceChargePerWkId = dbHelper.careProviderPersonContractServiceChargePerWk.GetByCareProviderPersonContractServiceId(careProviderPersonContractServiceId_1).FirstOrDefault();

            personContractServiceChargesPerWeekPage
                .WaitForPersonContractServiceChargesPerWeekPageToLoad()
                .ValidateRecordPresent(careProviderPersonContractServiceChargePerWkId.ToString(), true)
                .ValidateRecordCellText(careProviderPersonContractServiceChargePerWkId.ToString(), 2, DateTime.Now.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
                .ValidateRecordCellText(careProviderPersonContractServiceChargePerWkId.ToString(), 3, DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateRecordCellText(careProviderPersonContractServiceChargePerWkId.ToString(), 4, "£15.00");

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
                .ValidateChargePerWeekFieldText("15.00")
                .NavigateToRatesTab();

            #endregion

            #region Step 62

            var careProviderPersonContractServiceRatePeriodId = dbHelper.careProviderPersonContractServiceRatePeriod.GetByCareProviderPersonContractServiceId(careProviderPersonContractServiceId_1).FirstOrDefault();

            personContractServiceRatePeriodsPage
                .WaitForPersonContractServiceRatePeriodsPageToLoad()
                .OpenRecord(careProviderPersonContractServiceRatePeriodId.ToString());

            personContractServiceRatePeriodRecordPage
                .WaitForPersonContractServiceRatePeriodRecordPageToLoad()
                .InsertStartDate(DateTime.Now.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
                .InsertRate("11")
                .ClickSaveAndCloseButton();

            personContractServiceRatePeriodsPage
                .WaitForPersonContractServiceRatePeriodsTabSectionToLoad()
                .ClickChargePerWeekTab();

            careProviderPersonContractServiceChargePerWkId = dbHelper.careProviderPersonContractServiceChargePerWk.GetByCareProviderPersonContractServiceId(careProviderPersonContractServiceId_1).FirstOrDefault();

            personContractServiceChargesPerWeekPage
                .WaitForPersonContractServiceChargesPerWeekPageToLoad()
                .ValidateRecordPresent(careProviderPersonContractServiceChargePerWkId.ToString(), true)
                .ValidateRecordCellText(careProviderPersonContractServiceChargePerWkId.ToString(), 2, DateTime.Now.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
                .ValidateRecordCellText(careProviderPersonContractServiceChargePerWkId.ToString(), 3, DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateRecordCellText(careProviderPersonContractServiceChargePerWkId.ToString(), 4, "£11.00");

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
                .ValidateChargePerWeekFieldText("11.00");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4923")]
        [Description("Step(s) 63 to 64 from the original test ACC-856")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Person Contract Services")]
        public void PersonContractService_UITestMethod21()
        {
            #region Care Provider Service

            var _careProviderServiceName_1 = "Care Provider Service Record";
            var _careProviderServiceId_1 = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName_1, new DateTime(2020, 1, 1), 7895, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId_1 = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, provider2Id);
            dbHelper.careProviderContractScheme.UpdateAccountCodeApplies(careProviderContractSchemeId_1, true);

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId_1 = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractSchemeId_1, _careProviderServiceId_1, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, true);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContractId_1 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractSchemeId_1, provider2Id, DateTime.Now.AddDays(-5));

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId_1 = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_1, _teamId, careProviderContractSchemeId_1, _careProviderServiceId_1, CareProviderContractServiceId_1, DateTime.Now.AddDays(-5), 1, 1, _careProviderRateUnitId, true);
            dbHelper.careProviderPersonContractServiceRatePeriod.CreateCareProviderPersonContractServiceRatePeriod(_teamId, careProviderPersonContractServiceId_1, _careProviderRateUnitId, DateTime.Now.AddDays(-1), 10, "");

            var careProviderPersonContractServiceId_2 = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_1, _teamId, careProviderContractSchemeId_1, _careProviderServiceId_1, CareProviderContractServiceId_1, DateTime.Now.AddDays(-3), 1, 1, _careProviderRateUnitId, true);
            dbHelper.careProviderPersonContractServiceRatePeriod.CreateCareProviderPersonContractServiceRatePeriod(_teamId, careProviderPersonContractServiceId_2, _careProviderRateUnitId, DateTime.Now.AddDays(-1), 12, "");

            #endregion

            #region Step 63

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

            var accountCode = dbHelper.careProviderPersonContractService.GetAll().Count + 1;
            while (dbHelper.careProviderPersonContractService.GetByAccountCode(accountCode).Any())
                accountCode = accountCode + 1;

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ValidateMaximumLimitOfAccountCodeField("100")
                .ValidateAccountCodeMandatoryFieldVisibility(false)
                .SelectStatus("Completed")
                .ValidateAccountCodeMandatoryFieldVisibility(true)
                .ClickSaveButton()

                .ValidateAccountCodeErrorMessageText("Please fill out this field.")
                .InsertTextOnAccountCode(accountCode.ToString())
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            System.Threading.Thread.Sleep(2000);
            personContractServiceRecordPage
                .ValidateAccountCodeFieldIsDisabled(true)
                .SelectStatus("Cancelled")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateAccountCodeFieldIsDisabled(true)
                .ClickBackButton();

            #endregion

            #region Step 64

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId_2.ToString());

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .SelectStatus("Completed")
                .InsertTextOnAccountCode(accountCode.ToString())
                .ClickSaveButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Another Person Contract Service record already uses the Account Code of " + accountCode.ToString() + ". Do you wish to continue?")
                .TapCancelButton();

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ClickSaveButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Another Person Contract Service record already uses the Account Code of " + accountCode.ToString() + ". Do you wish to continue?")
                .TapOKButton();

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .WaitForRecordToBeSaved();

            System.Threading.Thread.Sleep(1000);
            personContractServiceRecordPage
                .ValidateAccountCodeFieldText(accountCode.ToString())
                .ValidateAccountCodeFieldIsDisabled(true);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4924")]
        [Description("Step(s) 65 to 68 from the original test ACC-856")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Person Contract Services")]
        public void PersonContractService_UITestMethod22()
        {
            #region Care Provider Service

            var _careProviderServiceName_1 = "Care Provider Service Record";
            var _careProviderServiceId_1 = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName_1, new DateTime(2020, 1, 1), 7895, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId_1 = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, provider2Id);

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId_1 = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractSchemeId_1, _careProviderServiceId_1, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, true, true);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContractId_1 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractSchemeId_1, provider2Id, DateTime.Now.AddDays(-5));

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId_1 = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_1, _teamId, careProviderContractSchemeId_1, _careProviderServiceId_1, CareProviderContractServiceId_1, DateTime.Now.AddDays(-5), 1, 1, _careProviderRateUnitId, false);

            dbHelper.careProviderPersonContractServiceRatePeriod.CreateCareProviderPersonContractServiceRatePeriod(_teamId, careProviderPersonContractServiceId_1, _careProviderRateUnitId, DateTime.Now.AddDays(-1), 10, "");

            #endregion

            #region Step 65

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
                .ValidateOverrideRateOptionsIsDisabled(false)
                .ValidateRateVerifiedOptionsIsDisabled(false);

            #endregion

            #region Step 66

            personContractServiceRecordPage
                .SelectStatus("Completed")
                .ClickOverrideRate_Option(false)
                .ClickRateVerified_Option(false)
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("The associated Contract Service has Permit Rate Override? = Yes and you have not indicated you are overriding the rate, BUT have not Verified the rate to use. The rate has to be verified before the Status can be changed to Completed.")
                .TapCloseButton();

            #endregion

            #region Step 67

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ClickOverrideRate_Option(true)
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("The associated Contract Service has Permit Rate Override? = Yes and you have indicated you are overriding the rate, BUT have not Verified the rate to use. The rate has to be verified before the Status can be changed to Completed.")
                .TapCloseButton();

            #endregion

            #region Step 68

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ClickOverrideRate_Option(false);

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Changing Override Rate? to No will mean that any records recorded under the Rates Tab will be deleted on save.")
                .TapCloseButton();

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ValidateOverrideRate_OptionIsCheckedOrNot(false)
                .ClickRateVerified_Option(true)
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            System.Threading.Thread.Sleep(1000);
            personContractServiceRecordPage
                .ValidateRateVerifiedOptionsIsDisabled(true)
                .ValidateOverrideRateOptionsIsDisabled(true)
                .SelectStatus("Cancelled")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            System.Threading.Thread.Sleep(1000);
            personContractServiceRecordPage
                .ValidateRateVerifiedOptionsIsDisabled(true)
                .ValidateOverrideRateOptionsIsDisabled(true);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4796

        [TestProperty("JiraIssueID", "ACC-4925")]
        [Description("Step(s) 69 from the original test ACC-856")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("BusinessModule2", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Person Contract Services")]
        [TestProperty("Screen2", "Finance Transactions")]
        public void PersonContractService_UITestMethod23()
        {
            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId_1 = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, provider2Id);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailName = "Care Provider Service Details Test 1";
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, null, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(_careProviderServiceId, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractSchemeId_1, _careProviderServiceId, careProviderServiceDetailId, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false, true);
            dbHelper.careProviderContractService.UpdateCareProviderServiceDetailId(CareProviderContractServiceId, careProviderServiceDetailId);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(CareProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Person Contract
            var StartDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-13);

            var careProviderPersonContractId_1 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractSchemeId_1, provider2Id, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId_1 = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId_1, _teamId,
                careProviderContractSchemeId_1, _careProviderServiceId, CareProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId, true);

            dbHelper.careProviderPersonContractService.UpdateOverrideRateAndRateVerified(careProviderPersonContractServiceId_1, true, true);
            dbHelper.careProviderPersonContractServiceRatePeriod.CreateCareProviderPersonContractServiceRatePeriod(_teamId, careProviderPersonContractServiceId_1, _careProviderRateUnitId, StartDate, 14, "");

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId_1, 90); //Completed = 90

            #endregion

            #region Extract Name, Extract Type, Finance Extract Batch Setup

            var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            var ExtractNames = dbHelper.careProviderExtractName.GetAllCareProviderExtractName();
            int newExtractNameCode1 = ExtractNames.Count();

            while (dbHelper.careProviderExtractName.GetByCode(newExtractNameCode1).Any())
                newExtractNameCode1 = newExtractNameCode1 + 1;

            var _cpExtractNameId1 = commonMethodsDB.CreateCareProviderExtractName(_teamId, "Extract_Batch_A1_" + _currentDateSuffix, newExtractNameCode1, null, new DateTime(2023, 9, 1));
            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type: Generic Extract File

            var _cpFinanceExtractBatchSetupId1 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, new DateTime(2023, 9, 19), new TimeSpan(7, 30, 0), null, _cpExtractNameId1, 1, _cpGenericExtractTypeId, _invoiceGenerateTemplate_MailMergeId);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateCareProviderFinanceExtractBatchSetupExtractOnDayFlagToYesByCurrentDayOfWeek(_cpFinanceExtractBatchSetupId1, new DateTime(2023, 9, 19));
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateGenerateAndSendInvoicesAutomaticallyFlag(_cpFinanceExtractBatchSetupId1, true);

            var _cpFinanceExtractBatchId = dbHelper
                                .careProviderFinanceExtractBatch
                                .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1);
            Assert.AreEqual(1, _cpFinanceExtractBatchId.Count);

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 2; //funder/service user
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 1; //Monday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupFrom = DateTime.Now.AddDays(-15);
            var financetransactionsupto = DateTime.Now.AddDays(30);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false, careProviderContractSchemeId_1, _careProviderBatchGroupingId, financetransactionsupFrom, new TimeSpan(0, 0, 0),
                                                          invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                                                          _cpExtractNameId1, false, _teamId);

            #endregion


            #region Step 69

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
                .NavigateToFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .ValidateNoRecordsMessageVisible(true);

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            cpFinanceTransactionsPage
                .ClickRefreshButton()
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .ValidateNoRecordsMessageVisible(false);

            #endregion
        }

        #endregion

        internal void ProcessCPFinanceScheduledJob(string ScheduledJobName)
        {
            #region Process CP Finance Extract Batches

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

    }
}
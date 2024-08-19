using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People.Related_Items
{
    [TestClass]
    public class ChargeApportionments_UITestCases : FunctionalTest
    {
        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private string _teamName;
        private Guid _ethnicityId;
        private Guid _systemUserId;
        private string _systemUserName;
        private Guid _personId;
        private string firstName;
        private string lastName;
        private string _personFullName;
        private int _personNumber;
        private Guid _careProviderServiceId;
        private string _careProviderServiceName;
        private Guid _careProviderRateUnitId;
        private string _careProviderRateUnitName;
        private Guid _careProviderBatchGroupingId;
        private string _careProviderBatchGroupingName;
        private Guid _careProviderVATCodeId;
        private string _careProviderVATCodeName;
        private Guid _providerId;
        private string _providerName;
        private Guid _provider2Id;
        private string _provider2Name;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");

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
                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("PersonContracts BU1");

                #endregion

                #region Team

                _teamName = "Person Charge Apportionments T1";
                _teamId = commonMethodsDB.CreateTeam(_teamName, null, _businessUnitId, "907678", "PersonChargeApportionmentsT1@careworkstempmail.com", "PesronChargeApportionments T1", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

                #endregion

                #region Create SystemUser Record

                _systemUserName = "PersonChargeApportionmentsUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "PersonChargeApportionments", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
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

                firstName = "Person_Charge_Appointment";
                lastName = _currentDateSuffix;
                _personFullName = firstName + " " + lastName;
                _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
                _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

                #endregion

                #region Provider

                _providerName = "Residential Establishment_1_" + _currentDateSuffix;
                var providerType = 13; //Residential Establishment
                _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, providerType, true);

                _provider2Name = "Residential Establishment_2_" + _currentDateSuffix;
                var provider2Type = 10; //Local Authority
                _provider2Id = commonMethodsDB.CreateProvider(_provider2Name, _teamId, provider2Type, true);

                #endregion


            }
            catch
            {
                if (driver != null)
                    driver.Quit();

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

        #region https://advancedcsg.atlassian.net/browse/ACC-6812

        [TestProperty("JiraIssueID", "ACC-6973")]
        [Description("Step(s) 1 to 4 from the original test ACC-912")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person")]
        [TestProperty("Screen", "Charge Appointments")]
        // Step 3 & 4 Failing due to https://advancedcsg.atlassian.net/browse/ACC-4736
        public void ChargeApportionments_UITestMethod01()
        {
            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, _providerId, _provider2Id);

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", _providerId, _provider2Id, careProviderContractSchemeId, _careProviderServiceId, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, true);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, _providerId, careProviderContractSchemeId, _provider2Id, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(5), true);
            var careProviderPersonContractTitle = (dbHelper.careProviderPersonContract.GetByID(careProviderPersonContractId, "title")["title"]).ToString();

            #endregion

            #region Care Provider Person Contract Service

            // Person Contract Service End Reason
            var careProviderPersonContractServiceEndReasonId = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, "Default Person Contract Service End Reason", new DateTime(2020, 1, 1), 999999);

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId, careProviderContractSchemeId, _careProviderServiceId, CareProviderContractServiceId, DateTime.Now.AddDays(-1), 1, 1, _careProviderRateUnitId, true);
            var careProviderPersonContractServiceTitle = (dbHelper.careProviderPersonContractService.GetByID(careProviderPersonContractServiceId, "title")["title"]).ToString();
            var careProviderPersonContractServiceNumber = (dbHelper.careProviderPersonContractService.GetByID(careProviderPersonContractServiceId, "careproviderpersoncontractservicenumber")["careproviderpersoncontractservicenumber"]).ToString();

            dbHelper.careProviderPersonContractService.UpdateEndDateTime(careProviderPersonContractServiceId, DateTime.Now.AddDays(3), careProviderPersonContractServiceEndReasonId);

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPersonContractsSection();

            personContractsPage
                .WaitForPersonContractsPageToLoadFromWorkplace()
                .SearchRecord(_personFullName)
                .ValidateRecordPresent(careProviderPersonContractId, true)
                .OpenRecord(careProviderPersonContractId.ToString());

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickChargeApportionmentsTab();

            chargeApportionmentsPage
                .WaitForChargeApportionmentsPageToLoad("careproviderpersoncontract")
                .ClickNewRecordButton();

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .ValidateAllFieldsOfGeneralSection()
                .ValidatePersonLinkText(_personFullName)
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateSelectedServiceTypePickListValue("Scheduled Care")
                .ValidateStartDateFieldValue(DateTime.Now.AddDays(-5).Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDateFieldValue(DateTime.Now.AddDays(5).Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateSelectedApportionmentTypePickListValue("")
                .ValidatePersonContractLinkText(careProviderPersonContractTitle);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPersonContractServicesSection();

            //for some reason this alert is not being displayed in chromedriver
            //alertPopup
            //    .WaitForAlertPopupToLoad()
            //    .TapOKButton();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad(false)
                .SearchRecord(careProviderPersonContractServiceNumber.ToString())
                .ValidateRecordPresent(careProviderPersonContractServiceId.ToString(), true)
                .OpenRecord(careProviderPersonContractServiceId.ToString());

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToChargeApportionmentsTab();

            chargeApportionmentsPage
                .WaitForChargeApportionmentsPageToLoad("careproviderpersoncontractservice")
                .ClickNewRecordButton();

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .ValidateAllFieldsOfGeneralSection()
                .ValidatePersonLinkText(_personFullName)
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateSelectedServiceTypePickListValue("Residential Care")
                .ValidateStartDateFieldValue(DateTime.Now.AddDays(-1).Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDateFieldValue(DateTime.Now.AddDays(3).Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateSelectedApportionmentTypePickListValue("")
                .ValidatePersonContractServiceLinkText(careProviderPersonContractServiceTitle);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            //for some reason this alert is not being displayed in chromedriver
            //alertPopup
            //    .WaitForAlertPopupToLoad()
            //    .TapOKButton();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToChargeApportionmentsPage();

            chargeApportionmentsPage
                .WaitForChargeApportionmentsPageToLoad()
                .ClickNewRecordButton();

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad();

            #endregion

            #region Step 2

            chargeApportionmentRecordPage
                .ValidateAllFieldsOfGeneralSection()
                .ValidatePersonMandatoryFieldVisibility(true)
                .ValidatePersonLinkText(_personFullName)

                .ValidateServiceTypeMandatoryFieldVisibility(true)
                .ValidateSelectedServiceTypePickListValue("")

                .ValidateStartDateMandatoryFieldVisibility(true)
                .ValidateStartDateFieldValue(DateTime.Now.AddDays(-5).Date.ToString("dd'/'MM'/'yyyy"))

                .ValidateApportionmentTypeMandatoryFieldVisibility(true)
                .ValidateSelectedApportionmentTypePickListValue("")

                .ValidateResponsibleTeamMandatoryFieldVisibility(true)
                .ValidateResponsibleTeamLinkText(_teamName)

                .ValidateEndDateMandatoryFieldVisibility(false)
                .ValidateEndDateFieldValue(DateTime.Now.AddDays(5).Date.ToString("dd'/'MM'/'yyyy"))

                .ValidateValidatedMandatoryFieldVisibility(true);

            #endregion

            #region Step 3

            chargeApportionmentRecordPage
                .SelectServiceType("Residential Care")
                .ValidatePersonContractServiceFieldIsVisible(true)
                .ValidatePersonContractFieldIsVisible(false)
                .ValidateStartDateFieldValue(DateTime.Now.AddDays(-1).Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDateFieldValue(DateTime.Now.AddDays(3).Date.ToString("dd'/'MM'/'yyyy"))
                .ValidatePersonContractServiceLinkText(careProviderPersonContractServiceTitle)

                .SelectServiceType("Scheduled Care")
                .ValidatePersonContractFieldIsVisible(true)
                .ValidatePersonContractServiceFieldIsVisible(false)
                .ValidateStartDateFieldValue(DateTime.Now.AddDays(-5).Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDateFieldValue(DateTime.Now.AddDays(5).Date.ToString("dd'/'MM'/'yyyy"))
                .ValidatePersonContractLinkText(careProviderPersonContractTitle);

            #endregion

            #region Step 4

            chargeApportionmentRecordPage
                .InsertStartDate(DateTime.Now.AddDays(-6).Date.ToString("dd'/'MM'/'yyyy"))
                .SelectApportionmentType("Amount")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("The Start Date can NOT be earlier than the associated PC record Start Date.")
                .TapCloseButton();

            chargeApportionmentRecordPage
                .InsertStartDate(DateTime.Now.AddDays(-2).Date.ToString("dd'/'MM'/'yyyy"))
                .InsertEndDate(DateTime.Now.AddDays(7).Date.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("The End Date can NOT be later than the associated PC record End Date.")
                .TapCloseButton();


            chargeApportionmentRecordPage
                .SelectServiceType("Residential Care")
                .ValidatePersonContractLinkText(careProviderPersonContractTitle)
                .InsertStartDate(DateTime.Now.AddDays(-2).Date.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("The Start Date can NOT be earlier than the associated PCS record Start Date.")
                .TapCloseButton();


            chargeApportionmentRecordPage
                .InsertStartDate(DateTime.Now.AddDays(-1).Date.ToString("dd'/'MM'/'yyyy"))
                .InsertEndDate(DateTime.Now.AddDays(7).Date.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("The End Date can NOT be later than the associated PCS record End Date.")
                .TapCloseButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6974")]
        [Description("Step(s) 6 to 7 from the original test ACC-912")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person")]
        [TestProperty("Screen", "Charge Appointments")]
        public void ChargeApportionments_UITestMethod02()
        {
            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, _providerId, _provider2Id);

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", _providerId, _provider2Id, careProviderContractSchemeId, _careProviderServiceId, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, true);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, _providerId, careProviderContractSchemeId, _provider2Id, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(5), true);
            var careProviderPersonContractTitle = (dbHelper.careProviderPersonContract.GetByID(careProviderPersonContractId, "title")["title"]).ToString();

            #endregion

            #region Care Provider Person Contract Service

            // Person Contract Service End Reason
            var careProviderPersonContractServiceEndReasonId = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, "Default Person Contract Service End Reason", new DateTime(2020, 1, 1), 999999);

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId, careProviderContractSchemeId, _careProviderServiceId, CareProviderContractServiceId, DateTime.Now.AddDays(-1), 1, 1, _careProviderRateUnitId, true);
            var careProviderPersonContractServiceTitle = (dbHelper.careProviderPersonContractService.GetByID(careProviderPersonContractServiceId, "title")["title"]).ToString();
            var careProviderPersonContractServiceNumber = (dbHelper.careProviderPersonContractService.GetByID(careProviderPersonContractServiceId, "careproviderpersoncontractservicenumber")["careproviderpersoncontractservicenumber"]).ToString();

            dbHelper.careProviderPersonContractService.UpdateEndDateTime(careProviderPersonContractServiceId, DateTime.Now.AddDays(3), careProviderPersonContractServiceEndReasonId);

            #endregion

            #region Step 6

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPersonContractsSection();

            personContractsPage
                .WaitForPersonContractsPageToLoadFromWorkplace()
                .SearchRecord(_personFullName)
                .ValidateRecordPresent(careProviderPersonContractId, true)
                .OpenRecord(careProviderPersonContractId.ToString());

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickChargeApportionmentsTab();

            chargeApportionmentsPage
                .WaitForChargeApportionmentsPageToLoad("careproviderpersoncontract")
                .ClickNewRecordButton();

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .ValidateAllFieldsOfGeneralSection()
                .ValidatePersonLinkText(_personFullName)
                .ValidatePersonMandatoryFieldVisibility(true)
                .ValidatePersonLookupButtonIsDisabled(true)

                .ValidateSelectedServiceTypePickListValue("Scheduled Care")
                .ValidateServiceTypeMandatoryFieldVisibility(true)
                .ValidateServiceTypePickListIsDisabled(true)

                .ValidateStartDateFieldValue(DateTime.Now.AddDays(-5).Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateStartDateMandatoryFieldVisibility(true)

                .ValidateSelectedApportionmentTypePickListValue("")
                .ValidateApportionmentTypeMandatoryFieldVisibility(true)

                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateResponsibleTeamMandatoryFieldVisibility(true)
                .ValidateResponsibleTeamLookupButtonIsDisabled(true)

                .ValidatePersonContractLinkText(careProviderPersonContractTitle)
                .ValidatePersonContractMandatoryFieldVisibility(true)
                .ValidatePersonContractLookupButtonIsDisabled(true)

                .ValidateEndDateFieldValue(DateTime.Now.AddDays(5).Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDateMandatoryFieldVisibility(false)

                .ValidateValidatedMandatoryFieldVisibility(true);

            #endregion

            #region Step 7

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPersonContractServicesSection();

            //for some reason this alert is not being displayed in chromedriver
            //alertPopup
            //    .WaitForAlertPopupToLoad()
            //    .TapOKButton();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad(false)
                .SearchRecord(careProviderPersonContractServiceNumber.ToString())
                .ValidateRecordPresent(careProviderPersonContractServiceId.ToString(), true)
                .OpenRecord(careProviderPersonContractServiceId.ToString());

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToChargeApportionmentsTab();

            chargeApportionmentsPage
                .WaitForChargeApportionmentsPageToLoad("careproviderpersoncontractservice")
                .ClickNewRecordButton();

            chargeApportionmentRecordPage

                .WaitForChargeApportionmentRecordPageToLoad()
                .ValidateAllFieldsOfGeneralSection()
                .ValidatePersonLinkText(_personFullName)
                .ValidatePersonMandatoryFieldVisibility(true)
                .ValidatePersonLookupButtonIsDisabled(true)

                .ValidateSelectedServiceTypePickListValue("Residential Care")
                .ValidateServiceTypeMandatoryFieldVisibility(true)
                .ValidateServiceTypePickListIsDisabled(true)

                .ValidateStartDateFieldValue(DateTime.Now.AddDays(-1).Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateStartDateMandatoryFieldVisibility(true)

                .ValidateSelectedApportionmentTypePickListValue("")
                .ValidateApportionmentTypeMandatoryFieldVisibility(true)

                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateResponsibleTeamMandatoryFieldVisibility(true)
                .ValidateResponsibleTeamLookupButtonIsDisabled(true)

                .ValidatePersonContractServiceLinkText(careProviderPersonContractServiceTitle)
                .ValidatePersonContractServiceMandatoryFieldVisibility(true)
                .ValidatePersonContractServiceLookupButtonIsDisabled(true)

                .ValidateEndDateFieldValue(DateTime.Now.AddDays(3).Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDateMandatoryFieldVisibility(false)

                .ValidateValidatedMandatoryFieldVisibility(true);

            #endregion

        }

        #endregion

        // Step 8 is Pending due to open Bug. Ones Bug is resoved Need to Create Script for Step 8

        #region https://advancedcsg.atlassian.net/browse/ACC-6869

        [TestProperty("JiraIssueID", "ACC-6975")]
        [Description("Step(s) 9 to 15 to from the original test ACC-912")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person")]
        [TestProperty("Screen", "Charge Appointments")]
        public void ChargeApportionments_UITestMethod03()
        {
            #region Care Provider Contract Scheme

            var contractSchemeName = "Contact_Scheme_1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, _providerId, _provider2Id);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, _providerId, careProviderContractSchemeId, _provider2Id, DateTime.Now.AddDays(3), DateTime.Now.AddDays(6), false);

            DateTime personContractStartDate = DateTime.Now.AddDays(-30).Date;
            var careProviderPersonContractId2 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, _providerId, careProviderContractSchemeId, _provider2Id, personContractStartDate, DateTime.Now.AddDays(2), true);

            #endregion

            #region Step 9

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPersonContractsSection();

            personContractsPage
                .WaitForPersonContractsPageToLoadFromWorkplace()
                .SearchRecord(_personFullName)
                .ValidateRecordPresent(careProviderPersonContractId, true)
                .OpenRecord(careProviderPersonContractId.ToString());

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ValidateChargeApportionmentsTabIsVisible(false);

            #endregion

            #region Step 10

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPersonContractsSection();

            personContractsPage
                .WaitForPersonContractsPageToLoadFromWorkplace()
                .SearchRecord(_personFullName)
                .ValidateRecordPresent(careProviderPersonContractId2, true)
                .OpenRecord(careProviderPersonContractId2.ToString());

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickChargeApportionmentsTab();

            chargeApportionmentsPage
                .WaitForChargeApportionmentsPageToLoad("careproviderpersoncontract")
                .ClickNewRecordButton();

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .ValidatePersonLinkText(_personFullName)
                .ValidateApportionmentTypeMandatoryFieldVisibility(true)
                .ValidateApportionmentTypePickListValues("Amount")
                .ValidateApportionmentTypePickListValues("Percentage");

            #endregion

            #region Step 11 & 13

            chargeApportionmentRecordPage
                .SelectApportionmentType("Amount")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateValidateChargeApportionmentButtonIsVisible(true);

            chargeApportionmentRecordPage
                .ValidatePageHeaderText(_personFullName + "/" + personContractStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateApportionmentDetailsTabIsVisible(true);

            #endregion

            #region Step 14

            chargeApportionmentRecordPage
                .NavigateToApportionmentDetailsTab();

            chargeApportionmentDetailsPage
                .WaitForChargeApportionmentDetailsPageToLoad("careproviderchargeapportionment")
                .ClickNewRecordButton();

            chargeApportionmentDetailRecordPage
                .WaitForChargeApportionmentDetailRecordPageToLoad()
                .ValidateAllFieldsOfGeneralSection()

                .ValidateChargeApportionmentMandatoryFieldVisibility(true)
                .ValidateChargeApportionmentLinkText(_personFullName + "/" + personContractStartDate.ToString("dd'/'MM'/'yyyy"))

                .ValidatePriorityMandatoryFieldVisibility(true)
                .ValidatePriorityFieldValue("1")
                .ValidateRangeOfPriorityField("1,9")

                .ValidateBalanceMandatoryFieldVisibility(true)
                .ValidateBalance_OptionIsCheckedOrNot(false)

                .ValidateResponsibleTeamMandatoryFieldVisibility(true)
                .ValidateResponsibleTeamLinkText(_teamName)

                .ValidatePayerMandatoryFieldVisibility(true)
                .ValidateAmountMandatoryFieldVisibility(true)
                .ValidateAmountFieldValue("")
                .ValidateRangeOfAmountField("0.0100,999999.9900");

            #endregion

            #region Step 15

            chargeApportionmentDetailRecordPage
                .ClickPayerLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateLookForPickListExpectedTextIsPresent("People", true)
                .ValidateLookForPickListExpectedTextIsPresent("Providers", true)
                .SelectLookFor("People")

                .ValidateLookInPickListExpectedTextIsPresent("Active Funders [Charge Apportionments]", false)
                .ClickCloseButton();

            #endregion

            #region Step 12 & 13

            chargeApportionmentDetailRecordPage
                .WaitForChargeApportionmentDetailRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            chargeApportionmentDetailsPage
                .WaitForChargeApportionmentDetailsTabsSectionToLoad("careproviderchargeapportionment")
                .ClickBackButton();

            chargeApportionmentsPage
                .WaitForChargeApportionmentsPageToLoad("careproviderpersoncontract")
                .ValidateToobarButtonIsPresent()
                .ClickNewRecordButton();

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .SelectApportionmentType("Amount")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("You are not permitted to create another Charge Apportionment record that links to the same Person Contract or Person Contract Service, where the dates overlap. Please correct as necessary.")
                .TapCloseButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6976")]
        [Description("Step(s) 16, 17 & 20 to from the original test ACC-912")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person")]
        [TestProperty("Screen", "Charge Appointments")]
        public void ChargeApportionments_UITestMethod04()
        {
            #region Care Provider Contract Scheme

            var contractSchemeName = "Contact_Scheme_1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, _providerId, _provider2Id);

            #endregion

            #region Care Provider Person Contract

            DateTime personContractStartDate = DateTime.Now.AddDays(-5);
            DateTime personContractEndDate = DateTime.Now.AddDays(5);
            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, _providerId, careProviderContractSchemeId, _provider2Id, personContractStartDate, personContractEndDate, true);

            #endregion

            var ChargeApportionmentStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-5));
            var ChargeApportionmentEndDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(5));
            var CareProviderChargeApportionmentId = dbHelper.careProviderChargeApportionment.CreateCareProviderChargeApportionment(_teamId, _businessUnitId, "", _personId, ChargeApportionmentStartDate, ChargeApportionmentEndDate, 2, careProviderPersonContractId, null, 1, 0);

            #region Step 16

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPersonContractsSection();

            personContractsPage
                .WaitForPersonContractsPageToLoadFromWorkplace()
                .SearchRecord(_personFullName)
                .ValidateRecordPresent(careProviderPersonContractId, true)
                .OpenRecord(careProviderPersonContractId.ToString());

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickChargeApportionmentsTab();

            chargeApportionmentsPage
                .WaitForChargeApportionmentsPageToLoad("careproviderpersoncontract")
                .OpenRecord(CareProviderChargeApportionmentId.ToString());

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .ValidatePersonLinkText(_personFullName)
                .ClickValidateChargeApportionmentButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The apportionment will apply to all Finance Transactions that have a start date equal to or greater than " + ChargeApportionmentStartDate.ToString("dd'/'MM'/'yyyy") + ". Do you wish to continue?")
                .TapCancelButton();

            #endregion

            #region Step 17

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .ClickBackButton();

            ChargeApportionmentStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-29));
            //Added below line to update PC Start Date as per ACC-4736. There was a change related to date as per this issue.
            dbHelper.careProviderPersonContract.UpdateStartDate(careProviderPersonContractId, ChargeApportionmentStartDate);
            dbHelper.careProviderChargeApportionment.UpdateStartDate(CareProviderChargeApportionmentId, ChargeApportionmentStartDate);

            chargeApportionmentsPage
                .WaitForChargeApportionmentsPageToLoad("careproviderpersoncontract")
                .ClickRefreshButton()
                .WaitForChargeApportionmentsPageToLoad("careproviderpersoncontract")
                .OpenRecord(CareProviderChargeApportionmentId.ToString());

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .ValidatePersonLinkText(_personFullName)
                .ClickValidateChargeApportionmentButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The apportionment will apply to all Finance Transactions that have a start date equal to or greater than " + ChargeApportionmentStartDate.ToString("dd'/'MM'/'yyyy") + ". This is also a date more than 28 days in the past and therefore could affect Finance Transactions already extracted for charging. Do you wish to continue?")
                .TapOKButton();

            #endregion

            #region Step 20

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("The Charge Apportionment can not be validated as there needs to be at least 2 Charge Apportionment Detail records. Please correct as necessary.")
                .TapCloseButton();

            var CreateCareProviderChargeApportionmentDetailId1 = dbHelper.careProviderChargeApportionmentDetail.CreateCareProviderChargeApportionmentDetail(_teamId, _businessUnitId, "", CareProviderChargeApportionmentId, 1, _personId, "person", _personFullName, true);
            var CreateCareProviderChargeApportionmentDetailId2 = dbHelper.careProviderChargeApportionmentDetail.CreateCareProviderChargeApportionmentDetail(_teamId, _businessUnitId, "", CareProviderChargeApportionmentId, 2, _personId, "person", _personFullName, false, 10);

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .NavigateToApportionmentDetailsTab();

            chargeApportionmentDetailsPage
                .WaitForChargeApportionmentDetailsPageToLoad("careproviderchargeapportionment")
                .ValidateRecordCellText(CreateCareProviderChargeApportionmentDetailId1.ToString(), 2, "1")
                .ValidateRecordCellText(CreateCareProviderChargeApportionmentDetailId1.ToString(), 3, _personFullName)
                .ValidateRecordCellText(CreateCareProviderChargeApportionmentDetailId1.ToString(), 4, "Yes")
                .ValidateRecordCellText(CreateCareProviderChargeApportionmentDetailId1.ToString(), 5, "")

                .ValidateRecordCellText(CreateCareProviderChargeApportionmentDetailId2.ToString(), 2, "2")
                .ValidateRecordCellText(CreateCareProviderChargeApportionmentDetailId2.ToString(), 3, _personFullName)
                .ValidateRecordCellText(CreateCareProviderChargeApportionmentDetailId2.ToString(), 4, "No")
                .ValidateRecordCellText(CreateCareProviderChargeApportionmentDetailId2.ToString(), 5, "£10.00");

            chargeApportionmentDetailsPage
                .WaitForChargeApportionmentDetailsTabSectionToLoad("careproviderchargeapportionment")
                .NavigateToDetailsTab();

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .ClickValidateChargeApportionmentButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The apportionment will apply to all Finance Transactions that have a start date equal to or greater than " + ChargeApportionmentStartDate.ToString("dd'/'MM'/'yyyy") + ". This is also a date more than 28 days in the past and therefore could affect Finance Transactions already extracted for charging. Do you wish to continue?")
                .TapOKButton();

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("The Charge Apportionment can not be validated as the Charge Apportionment Detail record with the lowest priority (highest value), does not have Balance? = Yes. Please correct as necessary.")
                .TapCloseButton();

            dbHelper.careProviderChargeApportionmentDetail.UpdateBalanceOption(CreateCareProviderChargeApportionmentDetailId1, false);
            dbHelper.careProviderChargeApportionmentDetail.UpdateAmount(CreateCareProviderChargeApportionmentDetailId1, 10);

            dbHelper.careProviderChargeApportionmentDetail.UpdateBalanceOption(CreateCareProviderChargeApportionmentDetailId2, true);
            dbHelper.careProviderChargeApportionmentDetail.UpdateAmount(CreateCareProviderChargeApportionmentDetailId2, null);

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .NavigateToApportionmentDetailsTab();

            chargeApportionmentDetailsPage
                .WaitForChargeApportionmentDetailsPageToLoad("careproviderchargeapportionment")
                .ValidateRecordCellText(CreateCareProviderChargeApportionmentDetailId1.ToString(), 2, "1")
                .ValidateRecordCellText(CreateCareProviderChargeApportionmentDetailId1.ToString(), 3, _personFullName)
                .ValidateRecordCellText(CreateCareProviderChargeApportionmentDetailId1.ToString(), 4, "No")
                .ValidateRecordCellText(CreateCareProviderChargeApportionmentDetailId1.ToString(), 5, "£10.00")

                .ValidateRecordCellText(CreateCareProviderChargeApportionmentDetailId2.ToString(), 2, "2")
                .ValidateRecordCellText(CreateCareProviderChargeApportionmentDetailId2.ToString(), 3, _personFullName)
                .ValidateRecordCellText(CreateCareProviderChargeApportionmentDetailId2.ToString(), 4, "Yes")
                .ValidateRecordCellText(CreateCareProviderChargeApportionmentDetailId2.ToString(), 5, "");

            chargeApportionmentDetailsPage
                .WaitForChargeApportionmentDetailsTabSectionToLoad("careproviderchargeapportionment")
                .NavigateToDetailsTab();

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .ClickValidateChargeApportionmentButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The apportionment will apply to all Finance Transactions that have a start date equal to or greater than " + ChargeApportionmentStartDate.ToString("dd'/'MM'/'yyyy") + ". This is also a date more than 28 days in the past and therefore could affect Finance Transactions already extracted for charging. Do you wish to continue?")
                .TapOKButton();

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("The Charge Apportionment can not be validated as 2 or more of the Charge Apportionment Detail records have the same Payer. Please correct as necessary.")
                .TapCloseButton();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6870

        [TestProperty("JiraIssueID", "ACC-6977")]
        [Description("Step(s) 18 & 19 to from the original test ACC-912")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person")]
        [TestProperty("Screen", "Charge Appointments")]
        public void ChargeApportionments_UITestMethod05()
        {
            #region Care Provider Contract Scheme

            var contractSchemeName = "Contact_Scheme_1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, _providerId, _provider2Id);

            #endregion

            #region Provider

            var _ProviderName = "Provider_" + _currentDateSuffix;
            var _providereId = commonMethodsDB.CreateProvider(_ProviderName, _teamId, 13, true); //create a "Residential Establishment" provider

            #endregion

            #region Care Provider Person Contract

            DateTime personContractStartDate = DateTime.Now.AddDays(-5);
            DateTime personContractEndDate = DateTime.Now.AddDays(5);
            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, _providerId, careProviderContractSchemeId, _provider2Id, personContractStartDate, personContractEndDate, true);

            #endregion

            #region Charge Apportionment

            var ChargeApportionmentStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-5));
            var ChargeApportionmentEndDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(5));
            var CareProviderChargeApportionmentId = dbHelper.careProviderChargeApportionment.CreateCareProviderChargeApportionment(_teamId, _businessUnitId, "", _personId, ChargeApportionmentStartDate, ChargeApportionmentEndDate, 2, careProviderPersonContractId, null, 1, 0);

            #endregion

            #region Charge Apportionment Detail

            dbHelper.careProviderChargeApportionmentDetail.CreateCareProviderChargeApportionmentDetail(_teamId, _businessUnitId, "", CareProviderChargeApportionmentId, 1, _personId, "person", _personFullName, false, 10);
            dbHelper.careProviderChargeApportionmentDetail.CreateCareProviderChargeApportionmentDetail(_teamId, _businessUnitId, "", CareProviderChargeApportionmentId, 2, _providereId, "provider", _ProviderName, true);

            #endregion

            #region Step 18, 19

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPersonContractsSection();

            personContractsPage
                .WaitForPersonContractsPageToLoadFromWorkplace()
                .SearchRecord(_personFullName)
                .ValidateRecordPresent(careProviderPersonContractId, true)
                .OpenRecord(careProviderPersonContractId.ToString());

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickChargeApportionmentsTab();

            chargeApportionmentsPage
                .WaitForChargeApportionmentsPageToLoad("careproviderpersoncontract")
                .OpenRecord(CareProviderChargeApportionmentId.ToString());

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .ValidatePersonLinkText(_personFullName)
                .ClickValidateChargeApportionmentButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The apportionment will apply to all Finance Transactions that have a start date equal to or greater than " + ChargeApportionmentStartDate.ToString("dd'/'MM'/'yyyy") + ". Do you wish to continue?")
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The Charge Apportionment has been successfully validated and will apply to all Finance Transactions that have a start date equal to or greater than " + ChargeApportionmentStartDate.ToString("dd'/'MM'/'yyyy") + ".")
                .TapOKButton();

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .ValidateServiceTypePickListIsDisabled(true)
                .ValidateStartDateFieldIsDisabled(true)
                .ValidateApportionmentTypePickListIsDisabled(true)
                .ValidatePersonContractLookupButtonIsDisabled(true)
                .ValidateEndDateFieldIsDisabled(false)
                .ValidateValidatedOptionsIsDisabled(true)
                .ValidateValidatedOptionsIsSelected(true);

            #endregion


        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-7207

        [TestProperty("JiraIssueID", "ACC-7253")]
        [Description("Step(s) 21 to 26 to from the original test ACC-912")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person")]
        [TestProperty("Screen", "Charge Appointments")]
        public void ChargeApportionments_UITestMethod06()
        {
            #region Provider

            _providerName = "Provider_" + _currentDateSuffix;
            _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 13, true); //create a "Residential Establishment" provider

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Contact_Scheme_1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, _providerId, _provider2Id);

            #endregion

            #region Care Provider Person Contract

            DateTime personContractStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-63)).Date;
            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, _providerId, careProviderContractSchemeId, _provider2Id, personContractStartDate, DateTime.Now.AddDays(50), true);
            var careProviderPersonContractTitle = (dbHelper.careProviderPersonContract.GetByID(careProviderPersonContractId, "title")["title"]).ToString();

            #endregion

            #region Charge Apportionment

            var ChargeApportionmentStartDate = personContractStartDate.AddDays(7);
            var ChargeApportionmentEndDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(49));
            var CareProviderChargeApportionmentId = dbHelper.careProviderChargeApportionment.CreateCareProviderChargeApportionment(_teamId, _businessUnitId, "", _personId, ChargeApportionmentStartDate, ChargeApportionmentEndDate, 2, careProviderPersonContractId, null, 1, 0);

            #endregion

            #region Charge Apportionment Detail

            var cpChargeApportionmentDetailId1 = dbHelper.careProviderChargeApportionmentDetail.CreateCareProviderChargeApportionmentDetail(_teamId, _businessUnitId, "", CareProviderChargeApportionmentId, 1, _personId, "person", _personFullName, false, 10);
            var cpCaDetailId1_Priority = (dbHelper.careProviderChargeApportionmentDetail.GetByID(cpChargeApportionmentDetailId1, "priority")["priority"]).ToString();
            var cpChargeApportionmentDetailId2 = dbHelper.careProviderChargeApportionmentDetail.CreateCareProviderChargeApportionmentDetail(_teamId, _businessUnitId, "", CareProviderChargeApportionmentId, 2, _providerId, "provider", _providerName, true);
            var cpCaDetailId2_Priority = (dbHelper.careProviderChargeApportionmentDetail.GetByID(cpChargeApportionmentDetailId2, "priority")["priority"]).ToString();

            #endregion

            #region Step 23

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPersonContractsSection();

            personContractsPage
                .WaitForPersonContractsPageToLoadFromWorkplace()
                .SearchRecord(_personFullName)
                .ValidateRecordPresent(careProviderPersonContractId, true)
                .OpenRecord(careProviderPersonContractId.ToString());

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickChargeApportionmentsTab();

            chargeApportionmentsPage
                .WaitForChargeApportionmentsPageToLoad("careproviderpersoncontract")
                .OpenRecord(CareProviderChargeApportionmentId.ToString());

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .ValidateSelectedApportionmentTypePickListValue("Amount")
                .ClickValidateChargeApportionmentButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The apportionment will apply to all Finance Transactions that have a start date equal to or greater than " + ChargeApportionmentStartDate.ToString("dd'/'MM'/'yyyy") + ". This is also a date more than 28 days in the past and therefore could affect Finance Transactions already extracted for charging. Do you wish to continue?")
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The Charge Apportionment has been successfully validated and will apply to all Finance Transactions that have a start date equal to or greater than " + ChargeApportionmentStartDate.ToString("dd'/'MM'/'yyyy") + ".")
                .TapOKButton();

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .ValidateValidatedOptionsIsSelected(true);

            var _cpFinanceTransactionTriggers = dbHelper.careProviderFinanceTransactionTrigger.GetByRecordId(careProviderPersonContractId, "careproviderpersoncontract", careProviderPersonContractTitle, 1); // New = 1
            Assert.AreEqual(1, _cpFinanceTransactionTriggers.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingExpandButton()
                .ClickFinanceTransactionTriggersButton();

            cpFinanceTransactionTriggersPage
                .WaitForPageToLoad()
                .ValidateSelectedSystemView("New");

            cpFinanceTransactionTriggersPage
                .OpenRecord(_cpFinanceTransactionTriggers[0]);

            cpFinanceTransactionTriggerRecordPage
                .WaitForPageToLoad()
                .ValidateRecordLinkText(careProviderPersonContractTitle)
                .ValidateReasonSelectedText("Charge Apportionment has Been Created")
                .ValidateIstoexpand_YesChecked()
                .ValidateIstoexpand_NoNotChecked()
                .ValidateStatusSelectedText("New");

            #endregion

            #region Step 24

            //Not to be automated - Ignored for automation

            #endregion

            #region Step 25

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPersonContractsSection();

            personContractsPage
                .WaitForPersonContractsPageToLoadFromWorkplace()
                .SearchRecord(_personFullName)
                .ValidateRecordPresent(careProviderPersonContractId, true)
                .OpenRecord(careProviderPersonContractId.ToString());

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickChargeApportionmentsTab();

            chargeApportionmentsPage
                .WaitForChargeApportionmentsPageToLoad("careproviderpersoncontract")
                .OpenRecord(CareProviderChargeApportionmentId.ToString());

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .NavigateToApportionmentDetailsTab();

            chargeApportionmentDetailsPage
                .WaitForChargeApportionmentDetailsPageToLoad("careproviderchargeapportionment", false)
                .OpenRecord(cpChargeApportionmentDetailId1.ToString());

            chargeApportionmentDetailRecordPage
                .WaitForChargeApportionmentDetailRecordPageToLoad()
                .ValidatePageHeaderText(cpCaDetailId1_Priority + "/" + _personFullName)
                .ValidatePriorityFieldValue("1")
                .ValidateBalance_OptionIsCheckedOrNot(false)
                .ValidateAmountFieldValue("10.00")
                .ClickBackButton();

            chargeApportionmentDetailsPage
                .WaitForChargeApportionmentDetailsPageToLoad("careproviderchargeapportionment", false)
                .OpenRecord(cpChargeApportionmentDetailId2.ToString());

            chargeApportionmentDetailRecordPage
                .WaitForChargeApportionmentDetailRecordPageToLoad()
                .ValidatePageHeaderText(cpCaDetailId2_Priority + "/" + _providerName)
                .ValidatePriorityFieldValue("2")
                .ValidateBalance_OptionIsCheckedOrNot(true)
                .ValidateAmountMandatoryFieldVisibility(false)
                .ClickBackButton();

            #endregion

            #region Step 26

            chargeApportionmentDetailsPage
                .WaitForChargeApportionmentDetailsTabSectionToLoad("careproviderchargeapportionment")
                .NavigateToDetailsTab();

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .ValidateSelectedApportionmentTypePickListValue("Amount");

            #endregion

            #region Step 21

            chargeApportionmentRecordPage
                .InsertEndDate("")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForChargeApportionmentRecordPageToLoad()
                .ValidateEndDateFieldValue("");

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad();

            //insert end date and save record. validate alert message. accept alert
            chargeApportionmentRecordPage
                .InsertEndDate(ChargeApportionmentEndDate.AddDays(-7).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The apportionment will cease for all Finance Transactions that have an end date later than " + ChargeApportionmentEndDate.AddDays(-7).ToString("dd'/'MM'/'yyyy") + ". Do you wish to continue?")
                .TapOKButton();

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .ValidateEndDateFieldValue(ChargeApportionmentEndDate.AddDays(-7).ToString("dd'/'MM'/'yyyy"));

            #endregion

            #region Step 22

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .InsertEndDate("")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForChargeApportionmentRecordPageToLoad()
                .ValidateEndDateFieldValue("");

            var ChargeApportionmentStartDate2 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-29));

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad();

            chargeApportionmentRecordPage
                .InsertEndDate(ChargeApportionmentStartDate2.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The apportionment will cease for all Finance Transactions that have an end date later than " + ChargeApportionmentStartDate2.ToString("dd'/'MM'/'yyyy") + ".  This is also a date more than 28 days in the past and therefore could affect Finance Transactions already extracted for charging. Do you wish to continue?")
                .TapOKButton();

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .ValidateEndDateFieldValue(ChargeApportionmentStartDate2.ToString("dd'/'MM'/'yyyy"));

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7254")]
        [Description("Step(s) 27 to 32 to from the original test ACC-912")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person")]
        [TestProperty("Screen", "Charge Appointments")]
        public void ChargeApportionments_UITestMethod07()
        {
            #region Provider

            _providerName = "Provider_" + _currentDateSuffix;
            _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 13, true); //create a "Residential Establishment" provider

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Contact_Scheme_1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, _providerId, _provider2Id);

            #endregion

            #region Care Provider Person Contract

            DateTime personContractStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-63)).Date;
            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, _providerId, careProviderContractSchemeId, _provider2Id, personContractStartDate, DateTime.Now.AddDays(50), true);
            var careProviderPersonContractTitle = (dbHelper.careProviderPersonContract.GetByID(careProviderPersonContractId, "title")["title"]).ToString();

            #endregion

            #region Charge Apportionment

            var ChargeApportionmentStartDate = personContractStartDate.AddDays(7);
            var ChargeApportionmentEndDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(49));
            var CareProviderChargeApportionmentId = dbHelper.careProviderChargeApportionment.CreateCareProviderChargeApportionment(_teamId, _businessUnitId, "", _personId, ChargeApportionmentStartDate, ChargeApportionmentEndDate, 2, careProviderPersonContractId, null, 2, 0);

            #endregion

            #region Charge Apportionment Detail

            var cpChargeApportionmentDetailId1 = dbHelper.careProviderChargeApportionmentDetail.CreateCareProviderChargeApportionmentDetail(_teamId, _businessUnitId, "", CareProviderChargeApportionmentId, 1, _personId, "person", _personFullName, false, null, 10);
            var cpChargeApportionmentDetailId2 = dbHelper.careProviderChargeApportionmentDetail.CreateCareProviderChargeApportionmentDetail(_teamId, _businessUnitId, "", CareProviderChargeApportionmentId, 2, _providerId, "provider", _providerName, true);

            #endregion

            #region Step 32, 27

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPersonContractsSection();

            personContractsPage
                .WaitForPersonContractsPageToLoadFromWorkplace()
                .SearchRecord(_personFullName)
                .OpenRecord(careProviderPersonContractId.ToString());

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickChargeApportionmentsTab();

            chargeApportionmentsPage
                .WaitForChargeApportionmentsPageToLoad("careproviderpersoncontract");

            chargeApportionmentsPage
                .ValidateHeaderCellSortOrdedByAscending(2, "Person")
                .ValidateHeaderCellSortOrdedByDescending(3, "Start Date")
                .ValidateHeaderCellText(4, "End Date")
                .ValidateHeaderCellText(5, "Apportionment Type")
                .ValidateHeaderCellText(6, "Validated?")
                .ValidateHeaderCellText(7, "Service Type")
                .ValidateHeaderCellText(8, "Person Contract Service")
                .ValidateHeaderCellText(9, "Person Contract")
                .ValidateHeaderCellText(10, "Modified On")
                .ValidateHeaderCellText(11, "Modified By");

            chargeApportionmentsPage
                .OpenRecord(CareProviderChargeApportionmentId.ToString());

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .ValidateSelectedApportionmentTypePickListValue("Percentage") //Step 27 - Verify linked apportionment
                .NavigateToApportionmentDetailsTab();

            chargeApportionmentDetailsPage
                .WaitForChargeApportionmentDetailsPageToLoad("careproviderchargeapportionment", true)
                .ValidateHeaderCellText(2, "Priority")
                .ValidateHeaderCellText(3, "Payer")
                .ValidateHeaderCellText(4, "Balance?")
                .ValidateHeaderCellText(5, "Amount")
                .ValidateHeaderCellText(6, "Percentage");

            #endregion

            #region Step 31

            chargeApportionmentDetailsPage
                .WaitForChargeApportionmentDetailsPageToLoad("careproviderchargeapportionment", true)
                .SearchRecord(_personFullName)
                .WaitForChargeApportionmentDetailsPageToLoad("careproviderchargeapportionment", true)
                .ValidateRecordPresent(cpChargeApportionmentDetailId1.ToString(), true)
                .ValidateRecordPresent(cpChargeApportionmentDetailId2.ToString(), false);

            #endregion

            #region Step 30, 27

            chargeApportionmentDetailsPage
                .ValidateToobarButtonIsPresent()
                .OpenRecord(cpChargeApportionmentDetailId1.ToString());

            chargeApportionmentDetailRecordPage
                .WaitForChargeApportionmentDetailRecordPageToLoad()
                .ValidatePercentageMandatoryFieldVisibility(true)
                .ValidatePercentageFieldValue("10.00") //Step 27 - Validate Percentage Field and its value
                .ValidateToobarButtonIsPresent()
                .ValidateResponsibleTeamLookupButtonIsDisabled()
                .ValidateChargeApportionmentLookupButtonIsDisabled();

            #endregion

            #region Step 29

            dbHelper.careProviderChargeApportionmentDetail.UpdateBalanceOption(cpChargeApportionmentDetailId2, false);
            dbHelper.careProviderChargeApportionmentDetail.UpdatePercentage(cpChargeApportionmentDetailId2, 10);

            dbHelper.careProviderChargeApportionmentDetail.UpdateBalanceOption(cpChargeApportionmentDetailId1, true);
            dbHelper.careProviderChargeApportionmentDetail.UpdatePercentage(cpChargeApportionmentDetailId1, null);

            chargeApportionmentDetailRecordPage
                .ClickBackButton();

            chargeApportionmentDetailsPage
                .WaitForChargeApportionmentDetailsPageToLoad("careproviderchargeapportionment", true)
                .ClickNewRecordButton();

            chargeApportionmentDetailRecordPage
                .WaitForChargeApportionmentDetailRecordPageToLoad()
                .SelectBalanceOption(true)
                .ClickPayerLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_personId);

            chargeApportionmentDetailRecordPage
                .WaitForChargeApportionmentDetailRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Only one record can be created that has Balance? = Yes. Please correct as necessary.")
                .TapCloseButton();

            #endregion

            #region Step 28

            chargeApportionmentDetailRecordPage
                .SelectBalanceOption(false)
                .InsertPercentage("5")
                .InsertPriority("1")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Records can not be created that use the same priority value. Please correct as necessary.")
                .TapCloseButton();

            #endregion

        }


        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-7208

        [TestProperty("JiraIssueID", "ACC-7264")]
        [Description("Step(s) 33 to from the original test ACC-912")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person")]
        [TestProperty("Screen", "Charge Appointments")]
        public void ChargeApportionments_UITestMethod08()
        {
            #region Provider

            _providerName = "Provider_" + _currentDateSuffix;
            _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 13, true); //create a "Residential Establishment" provider

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Contact_Scheme_1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, _providerId, _provider2Id);

            #endregion

            #region Care Provider Person Contract

            DateTime personContractStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-63)).Date;
            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, _providerId, careProviderContractSchemeId, _provider2Id, personContractStartDate, DateTime.Now.AddDays(70), true);
            var careProviderPersonContractTitle = (dbHelper.careProviderPersonContract.GetByID(careProviderPersonContractId, "title")["title"]).ToString();

            #endregion

            #region Charge Apportionment

            var ChargeApportionment1StartDate = personContractStartDate.AddDays(7);
            var ChargeApportionment1EndDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(49));
            var CareProviderChargeApportionmentId1 = dbHelper.careProviderChargeApportionment.CreateCareProviderChargeApportionment(_teamId, _businessUnitId, "", _personId, ChargeApportionment1StartDate, ChargeApportionment1EndDate, 2, careProviderPersonContractId, null, 2, 0);

            var ChargeApportionment2StartDate = ChargeApportionment1EndDate.AddDays(1);
            var ChargeApportionment2EndDate = ChargeApportionment2StartDate.AddDays(8);
            var CareProviderChargeApportionmentId2 = dbHelper.careProviderChargeApportionment.CreateCareProviderChargeApportionment(_teamId, _businessUnitId, "", _personId, ChargeApportionment2StartDate, ChargeApportionment2EndDate, 2, careProviderPersonContractId, null, 2, 0);

            #endregion

            #region Charge Apportionment Detail

            dbHelper.careProviderChargeApportionmentDetail.CreateCareProviderChargeApportionmentDetail(_teamId, _businessUnitId, "", CareProviderChargeApportionmentId1, 1, _personId, "person", _personFullName, false, null, 10);
            dbHelper.careProviderChargeApportionmentDetail.CreateCareProviderChargeApportionmentDetail(_teamId, _businessUnitId, "", CareProviderChargeApportionmentId1, 2, _providerId, "provider", _providerName, true);

            var cpChargeApportionment2DetailId1 = dbHelper.careProviderChargeApportionmentDetail.CreateCareProviderChargeApportionmentDetail(_teamId, _businessUnitId, "", CareProviderChargeApportionmentId2, 1, _personId, "person", _personFullName, false, null, 10);
            var cpChargeApportionment2DetailId2 = dbHelper.careProviderChargeApportionmentDetail.CreateCareProviderChargeApportionmentDetail(_teamId, _businessUnitId, "", CareProviderChargeApportionmentId2, 2, _providerId, "provider", _providerName, true);


            #endregion

            #region Step 33

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPersonContractsSection();

            personContractsPage
                .WaitForPersonContractsPageToLoadFromWorkplace()
                .SearchRecord(_personFullName)
                .OpenRecord(careProviderPersonContractId.ToString());

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickChargeApportionmentsTab();

            chargeApportionmentsPage
                .WaitForChargeApportionmentsPageToLoad("careproviderpersoncontract")
                .ValidateRecordPresent(CareProviderChargeApportionmentId1.ToString(), true)
                .ValidateRecordPresent(CareProviderChargeApportionmentId2.ToString(), true)
                .OpenRecord(CareProviderChargeApportionmentId1.ToString());

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .ClickValidateChargeApportionmentButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .ValidateValidatedOptionsIsSelected(true);

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .ClickDeleteRecordButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Can not delete validated records.")
                .TapCloseButton();

            Assert.AreEqual(2, dbHelper.careProviderChargeApportionmentDetail.GetByChargeApportionmentId(CareProviderChargeApportionmentId1).Count);

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .ClickBackButton();

            chargeApportionmentsPage
                .WaitForChargeApportionmentsPageToLoad("careproviderpersoncontract")
                .ValidateRecordPresent(CareProviderChargeApportionmentId1.ToString(), true)
                .ValidateRecordPresent(CareProviderChargeApportionmentId2.ToString(), true)
                .OpenRecord(CareProviderChargeApportionmentId2.ToString());

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .ClickDeleteRecordButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            chargeApportionmentsPage
                .WaitForChargeApportionmentsPageToLoad("careproviderpersoncontract")
                .ValidateRecordPresent(CareProviderChargeApportionmentId1.ToString(), true)
                .ValidateRecordPresent(CareProviderChargeApportionmentId2.ToString(), false);

            Assert.AreEqual(0, dbHelper.careProviderChargeApportionment.GetById(CareProviderChargeApportionmentId2).Count);
            Assert.AreEqual(0, dbHelper.careProviderChargeApportionmentDetail.GetByChargeApportionmentDetailId(cpChargeApportionment2DetailId1).Count);
            Assert.AreEqual(0, dbHelper.careProviderChargeApportionmentDetail.GetByChargeApportionmentDetailId(cpChargeApportionment2DetailId2).Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7265")]
        [Description("Step(s) 34 to from the original test ACC-912")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person")]
        [TestProperty("Screen", "Charge Appointments")]
        public void ChargeApportionments_UITestMethod09()
        {

            #region Provider

            var providerName = "Provider " + _currentDateSuffix;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + _currentDateSuffix;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + _currentDateSuffix;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + _currentDateSuffix;
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

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + _currentDateSuffix;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 5; //Every Calendar Month
            var createbatchwithin = 0;
            var chargetodayid = 5; //Friday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2025, 12, 31);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2024, 4, 26), new TimeSpan(1, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Care Provider Person Contract

            DateTime personContractStartDate = new DateTime(2024, 1, 19);
            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, _providerId, careProviderContractScheme1Id, _provider2Id, personContractStartDate, null, true);

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

            #region Charge Apportionment

            var ChargeApportionmentStartDate = new DateTime(2024, 7, 1);
            var ChargeApportionmentEndDate = new DateTime(2024, 7, 5);
            var CareProviderChargeApportionmentId = dbHelper.careProviderChargeApportionment.CreateCareProviderChargeApportionment(_teamId, _businessUnitId, "", _personId, ChargeApportionmentStartDate, null, 1, null, careProviderPersonContractServiceId, 2, 0);

            #endregion

            #region Charge Apportionment Detail

            var cpChargeApportionmentDetailId1 = dbHelper.careProviderChargeApportionmentDetail.CreateCareProviderChargeApportionmentDetail(_teamId, _businessUnitId, "", CareProviderChargeApportionmentId, 1, _personId, "person", _personFullName, false, null, 10);
            var cpChargeApportionmentDetailId2 = dbHelper.careProviderChargeApportionmentDetail.CreateCareProviderChargeApportionmentDetail(_teamId, _businessUnitId, "", CareProviderChargeApportionmentId, 2, _providerId, "provider", _providerName, true);

            #endregion

            #region Step 35

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPersonContractsSection();

            personContractsPage
                .WaitForPersonContractsPageToLoadFromWorkplace()
                .SearchRecord(_personFullName)
                .OpenRecord(careProviderPersonContractId.ToString());

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId.ToString());

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToFinanceTransactionsTab();

            var _financeTransactionId1 = dbHelper.careProviderFinanceTransaction.GetByPersonContractServiceAndStartDateAndEndDate(careProviderPersonContractServiceId, new DateTime(2024, 6, 29), new DateTime(2024, 6, 30))[0];
            var _financeTransactionId2 = dbHelper.careProviderFinanceTransaction.GetByPersonContractServiceAndStartDateAndEndDate(careProviderPersonContractServiceId, new DateTime(2024, 7, 1), new DateTime(2024, 7, 5))[0];
            var _financeTransactionId3 = dbHelper.careProviderFinanceTransaction.GetByPersonContractServiceAndStartDateAndEndDate(careProviderPersonContractServiceId, new DateTime(2024, 7, 6), new DateTime(2024, 7, 12))[0];

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(_financeTransactionId1.ToString());

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateApportioned_YesRadioButtonIsChecked(false)
                .ValidateTransactionClass("Standard")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(_financeTransactionId2.ToString());

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateApportioned_YesRadioButtonIsChecked(false)
                .ValidateTransactionClass("Standard")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(_financeTransactionId3.ToString());

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateApportioned_YesRadioButtonIsChecked(false)
                .ValidateTransactionClass("Standard")
                .ClickBackButton();

            dbHelper.careProviderChargeApportionment.UpdateValidated(CareProviderChargeApportionmentId, true);

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .ClickRefreshButton();

            var _financeTransactionId4 = dbHelper.careProviderFinanceTransaction.GetByPersonContractServiceAndStartDateAndEndDate(careProviderPersonContractServiceId, new DateTime(2024, 6, 29), new DateTime(2024, 6, 30))[0];
            var _financeTransactionId5 = dbHelper.careProviderFinanceTransaction.GetByPersonContractServiceAndStartDateAndEndDate(careProviderPersonContractServiceId, new DateTime(2024, 7, 1), new DateTime(2024, 7, 5))[0];
            var _financeTransactionId6 = dbHelper.careProviderFinanceTransaction.GetByPersonContractServiceAndStartDateAndEndDate(careProviderPersonContractServiceId, new DateTime(2024, 7, 6), new DateTime(2024, 7, 12))[0];

            cpFinanceTransactionsPage
                .ClickHeaderCellText(9, "Transaction No")
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false);

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(_financeTransactionId4.ToString());

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateApportioned_YesRadioButtonIsChecked(false)
                .ValidateTransactionClass("Standard")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(_financeTransactionId5.ToString());

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateApportioned_YesRadioButtonIsChecked(true)
                .ValidateTransactionClass("Apportioned")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(_financeTransactionId6.ToString());

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateApportioned_YesRadioButtonIsChecked(true)
                .ValidateTransactionClass("Apportioned")
                .ClickBackButton();

            dbHelper.careProviderChargeApportionment.UpdateEndDate(CareProviderChargeApportionmentId, ChargeApportionmentEndDate);

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .ClickRefreshButton();

            var _financeTransactionId7 = dbHelper.careProviderFinanceTransaction.GetByPersonContractServiceAndStartDateAndEndDate(careProviderPersonContractServiceId, new DateTime(2024, 6, 29), new DateTime(2024, 6, 30))[0];
            var _financeTransactionId8 = dbHelper.careProviderFinanceTransaction.GetByPersonContractServiceAndStartDateAndEndDate(careProviderPersonContractServiceId, new DateTime(2024, 7, 1), new DateTime(2024, 7, 5))[0];
            var _financeTransactionId9 = dbHelper.careProviderFinanceTransaction.GetByPersonContractServiceAndStartDateAndEndDate(careProviderPersonContractServiceId, new DateTime(2024, 7, 6), new DateTime(2024, 7, 12))[0];

            cpFinanceTransactionsPage
                .ClickHeaderCellText(10, "Start Date")
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false);

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(_financeTransactionId7.ToString());

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateApportioned_YesRadioButtonIsChecked(false)
                .ValidateTransactionClass("Standard")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(_financeTransactionId8.ToString());

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateApportioned_YesRadioButtonIsChecked(true)
                .ValidateTransactionClass("Apportioned")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(_financeTransactionId9.ToString());

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateApportioned_YesRadioButtonIsChecked(false)
                .ValidateTransactionClass("Standard");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7266")]
        [Description("Step(s) 35 to from the original test ACC-912")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person")]
        [TestProperty("Screen", "Charge Appointments")]
        public void ChargeApportionments_UITestMethod10()
        {

            #region Provider

            var providerName = "Provider " + _currentDateSuffix;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + _currentDateSuffix;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + _currentDateSuffix;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + _currentDateSuffix;
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

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + _currentDateSuffix;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 5; //Every Calendar Month
            var createbatchwithin = 0;
            var chargetodayid = 5; //Friday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2025, 12, 31);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2024, 4, 26), new TimeSpan(1, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Care Provider Person Contract

            DateTime personContractStartDate = new DateTime(2024, 1, 19);
            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, _providerId, careProviderContractScheme1Id, _provider2Id, personContractStartDate, null, true);

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

            #region Charge Apportionment

            var ChargeApportionmentStartDate = new DateTime(2024, 7, 1);
            var CareProviderChargeApportionmentId = dbHelper.careProviderChargeApportionment.CreateCareProviderChargeApportionment(_teamId, _businessUnitId, "", _personId, ChargeApportionmentStartDate, null, 1, null, careProviderPersonContractServiceId, 2, 0);

            #endregion

            #region Charge Apportionment Detail

            dbHelper.careProviderChargeApportionmentDetail.CreateCareProviderChargeApportionmentDetail(_teamId, _businessUnitId, "", CareProviderChargeApportionmentId, 2, _providerId, "person", _personFullName, true);

            #endregion

            #region Step 35

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPersonContractsSection();

            personContractsPage
                .WaitForPersonContractsPageToLoadFromWorkplace()
                .SearchRecord(_personFullName)
                .OpenRecord(careProviderPersonContractId.ToString());

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId.ToString());

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToChargeApportionmentsTab();

            chargeApportionmentsPage
                .WaitForChargeApportionmentsPageToLoad("careproviderpersoncontractservice")
                .OpenRecord(CareProviderChargeApportionmentId.ToString());

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .ClickValidateChargeApportionmentButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .ValidateValidatedOptionsIsSelected(true)
                .ClickBackButton();

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .NavigateToFinanceTransactionsTab();

            var _financeTransactionId1 = dbHelper.careProviderFinanceTransaction.GetByPersonContractServiceAndStartDateAndEndDate(careProviderPersonContractServiceId, new DateTime(2024, 6, 29), new DateTime(2024, 6, 30))[0];
            var _financeTransactionId2 = dbHelper.careProviderFinanceTransaction.GetByPersonContractServiceAndStartDateAndEndDate(careProviderPersonContractServiceId, new DateTime(2024, 7, 1), new DateTime(2024, 7, 5))[0];

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .ClickHeaderCellText(10, "Start Date")
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(_financeTransactionId1.ToString());

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateApportioned_YesRadioButtonIsChecked(false)
                .ValidateTransactionClass("Standard")
                .ClickBackButton();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderpersoncontractservice", false)
                .OpenRecord(_financeTransactionId2.ToString());

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateApportioned_YesRadioButtonIsChecked(true)
                .ValidateTransactionClass("Apportioned");
            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7288")]
        [Description("Step 8 to from the original test ACC-912")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person")]
        [TestProperty("Screen", "Charge Appointments")]
        public void ChargeApportionments_UITestMethod11()
        {

            #region Provider

            //var providerName = "Provider " + _currentDateSuffix;
            var addressType = 10; //Home
            //var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + _currentDateSuffix;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + _currentDateSuffix;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, _providerId, _providerId, false, true);

            //create contractScheme2Name
            var contractScheme2Name = "B_CPCS_" + _currentDateSuffix;
            var contractScheme2Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme2Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractScheme2Name, new DateTime(2020, 1, 1), contractScheme2Code, _providerId, funderProviderID);
            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + _currentDateSuffix;
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

            var careProviderContractServiceId1 = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", _providerId, _providerId, careProviderContractScheme1Id, careProviderServiceId1, careProviderServiceDetailId, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            var careProviderContractServiceId2 = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", _providerId, funderProviderID, careProviderContractScheme2Id, careProviderServiceId1, careProviderServiceDetailId, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            dbHelper.careProviderContractService.UpdateIsNetIncome(careProviderContractServiceId2, true);
            dbHelper.careProviderContractService.UpdateIncomeCareProviderContractService(careProviderContractServiceId2, careProviderContractServiceId1);
            string careProviderContractServiceId2_Title = (string)dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId2, "title")["title"];

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId1, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId2, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Person Contract

            DateTime personContractStartDate = new DateTime(2024, 1, 19);
            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, _providerId, careProviderContractScheme1Id, funderProviderID, personContractStartDate, null, false);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme2Id, careProviderServiceId1, careProviderContractServiceId2,
                personContractStartDate, 1, 1, _careProviderRateUnitId);

            int careProviderPersonContractService_IdNumber = (int)dbHelper.careProviderPersonContractService.GetByID(careProviderPersonContractServiceId, "careproviderpersoncontractservicenumber")["careproviderpersoncontractservicenumber"];

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

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
                .SearchPersonRecordByLastName(lastName.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToChargeApportionmentsPage();

            chargeApportionmentsPage
                .WaitForChargeApportionmentsPageToLoad()
                .ClickNewRecordButton();

            chargeApportionmentRecordPage
                .WaitForChargeApportionmentRecordPageToLoad()
                .SelectServiceType("Residential Care")
                .ValidatePersonContractServiceFieldIsVisible(true)
                .ClickPersonContractServiceLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(careProviderPersonContractService_IdNumber.ToString())
                .TapSearchButton()
                .ValidateResultElementNotPresent(careProviderPersonContractServiceId)
                .ClickCloseButton();


            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPersonContractsSection();

            //for some reason this alert is not being displayed in chromedriver
            //alertPopup
            //    .WaitForAlertPopupToLoad()
            //    .TapOKButton();

            personContractsPage
                .WaitForPersonContractsPageToLoadFromWorkplace()
                .SearchRecord(_personFullName)
                .OpenRecord(careProviderPersonContractId.ToString());

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractServicesTab();

            personContractServicesPage
                .WaitForPersonContractServicesPageToLoad()
                .OpenRecord(careProviderPersonContractServiceId.ToString());

            personContractServiceRecordPage
                .WaitForPersonContractServiceRecordPageToLoad()
                .ValidateChargeApportionmentsTabIsVisible(false)
                .ValidateContractServiceLinkText(careProviderContractServiceId2_Title);

            #endregion

        }


        #endregion

    }

}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Providers
{
    /// <summary>
    /// This class contains Automated UI test scripts for Provider Type
    /// </summary>
    [TestClass]
    public class ProviderType_UITestCases : FunctionalTest
    {
        #region Properties

        private string appConfigSystemUserFullName;
        private string EnvironmentName;
        private Guid authenticationproviderid;
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _defaultLoginUserID;
        public Guid environmentid;
        private string _loginUser_Username;
        private string teamName;
        private Guid _professionalTypeId;
        private Guid _professionalId;
        private Guid _providerId1;
        private string _providerName;
        private string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _AddressBoroughId;
        private Guid _AddressWardId;
        private Guid _AddressGazetteerId;
        private string tenantName;

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
                var fields = dbHelper.systemUser.GetSystemUserBySystemUserID(defaultSystemUserId, "firstname", "lastname");
                appConfigSystemUserFullName = fields["firstname"] + " " + fields["lastname"];

                #endregion

                #region Environment Name

                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

                #region Business Unit

                _careProviders_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareProviders");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                teamName = "CareProviders";
                _careProviders_TeamId = commonMethodsDB.CreateTeam(teamName, null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", teamName, "020 123456");

                #endregion

                #region Create default system user

                _loginUser_Username = "ProviderType_User_1";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUser_Username, "ProviderType", "User_1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-22133

        [TestProperty("JiraIssueID", "ACC-3575")]
        [Description("Login into CD." +
            "Navigate to Provider module and validate Provider page should be load." +
            "Create new Provider button and hit on save button without entering mandatory field and Validate error message." +
            "Validate 'Training Provider' option under Provider Type picklist." +
            "Enter mandatory filed and validate record should be saved." +
            "Enter mandatory and some option filed and validate record should be saved.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Provider")]
        [TestProperty("Screen1", "Providers")]
        public void ProviderType_UITestCases001()
        {
            #region Professional Type 

            var professionalType = dbHelper.professionType.GetByName("Care Coordinator").Any();
            if (!professionalType)
                _professionalTypeId = dbHelper.professionType.CreateProfessionType(_careProviders_TeamId, "Care Coordinator", new DateTime(2022, 10, 10));

            _professionalTypeId = dbHelper.professionType.GetByName("Care Coordinator")[0];

            #endregion

            #region Professional

            var professionalExist = dbHelper.professional.GetProfessionalIdByFirstName("CDV6-22169").Any();
            if (!professionalExist)
                _professionalId = dbHelper.professional.CreateProfessional(_careProviders_TeamId, _professionalTypeId, "", "CDV6-22169", "1", "CDV6_22169@mail.com");

            _professionalId = dbHelper.professional.GetProfessionalIdByFirstName("CDV6-22169")[0];

            #endregion

            #region Step 1 & 2

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad();

            #endregion

            #region Step 3

            providersPage
                .ClickNewRecordButton();

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .SelectProviderType("")
                .ClickSaveButton()
                .ValidateNameErrorLabelText("Please fill out this field.")
                .ValidateProviderTypeErrorLabelText("Please fill out this field.");

            #endregion

            #region Step 4

            providersRecordPage
                .ValidateProviderTypeFieldValue("Training Provider")
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            providersPage
                .WaitForProvidersPageToLoad();

            #endregion

            #region Step 5

            providersPage
                .ClickNewRecordButton();

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .InsertName("22169_Provider_1_" + currentTimeString)
                .SelectProviderType("Training Provider")
                .ValidateResponsibleTeamLinkFieldText(teamName)
                .ClickSaveButton()
                .WaitForRecordToBeSaved("22169_Provider_1_" + currentTimeString)
                .ClickDetailsTab()
                .ValidateNameFieldValue("22169_Provider_1_" + currentTimeString)
                .ValidateSelectedProviderTypeValue("Training Provider");

            #endregion

            #region Step 6

            providersRecordPage
                .ClickBackButton();

            providersPage
                .WaitForProvidersPageToLoad()
                .ClickNewRecordButton();

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .InsertName("22169_Provider_2_" + currentTimeString)
                .SelectProviderType("Training Provider")
                .InsertMainPhoneNo("9988776655")
                .InsertOtherPhoneNo("5566778899")
                .InsertWebsite("https://www.google.com")
                .InsertEmailId("CDV6_19562@test.com")
                .ClickPrimaryContactLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery("CDV6-22169")
               .TapSearchButton()
               .SelectResultElement(_professionalId.ToString());

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .InsertDescription("Health Checkup")
                .InsertStartDate(DateTime.Now.AddDays(-5).ToString("dd'/'MM'/'yyyy"))
                .InsertEndDate(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .InsertNotesText("Appointment with doctor")
                .ClickSaveButton()
                .WaitForRecordToBeSaved("22169_Provider_2_" + currentTimeString)
                .ClickDetailsTab()
                .ValidateNameFieldValue("22169_Provider_2_" + currentTimeString)
                .ValidateSelectedProviderTypeValue("Training Provider")
                .ValidateMainPhoneFieldValue("9988776655")
                .ValidateOtherPhoneFieldValue("5566778899")
                .ValidateWebsiteFieldText("https://www.google.com")
                .ValidateEmailFieldValue("CDV6_19562@test.com")
                .ValidateDescriptionFieldText("Health Checkup")
                .ValidateNotesFieldText("Appointment with doctor")
                .ValidateStartDateFieldValue(DateTime.Now.AddDays(-5).ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDateFieldValue(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"));

            #endregion

        }

        //[TestProperty("JiraIssueID", "ACC-3576")]
        //[Description("This Test case is to Create a new 'Training Provider' under Provider type section")]
        //[TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), Ignore]
        //public void ProviderType_UITestCases002()
        //{
        //    #region Step 7

        //    #region Address Borough

        //    var boroughExists = dbHelper.addressBorough.GetAddressBoroughByName("Camden 22133").Any();
        //    if (!boroughExists)
        //    {
        //        _AddressBoroughId = dbHelper.addressBorough.CreateAddressBorough("Camden 22133", new DateTime(2020, 1, 2), _careProviders_TeamId);
        //    }
        //    if (_AddressBoroughId == Guid.Empty)
        //        _AddressBoroughId = dbHelper.addressBorough.GetAddressBoroughByName("Camden 22133").FirstOrDefault();

        //    #endregion

        //    #region Address Ward

        //    var wardExists = dbHelper.addressWard.GetAddressWardByName("Horse Hill 22133").Any();
        //    if (!wardExists)
        //    {
        //        _AddressWardId = dbHelper.addressWard.CreateAddressWard("Horse Hill 22133", new DateTime(2020, 1, 2), _careProviders_TeamId);
        //    }
        //    if (_AddressWardId == Guid.Empty)
        //        _AddressWardId = dbHelper.addressWard.GetAddressWardByName("Horse Hill 22133").FirstOrDefault();

        //    #endregion

        //    #region Addresses Gazetteer               

        //    _AddressGazetteerId = dbHelper.addressGazetteer.CreateAddressGazetteer("CDV6-22133 UPRN A" + currentTimeString, "CDV6-22133 PNO A" + currentTimeString, "CDV6-22133 PNA A" + currentTimeString, "CDV6-22133 ST A" + currentTimeString, "CDV6-22133 VLG A" + currentTimeString, "CDV6-22133 TW A" + currentTimeString, "CDV6-22133 CON A" + currentTimeString, "CDV6-22133 COUNTRY A" + currentTimeString, "CDV6-22133 LA A" + currentTimeString, "CR0 22133", "1234", "1235", "4321", "4322", _AddressBoroughId, _AddressWardId);

        //    #endregion

        //    //Provider Name
        //    _providerName = "CDV6-22170_Provider 1" + DateTime.Now.ToString("ddMMyyyyHHmmss");

        //    loginPage
        //        .GoToLoginPage()
        //        .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToProvidersSection();

        //    providersPage
        //        .WaitForProvidersPageToLoad()
        //        .ClickNewRecordButton();

        //    providersRecordPage
        //        .WaitForProvidersRecordPageToLoad()
        //        .InsertName(_providerName)
        //        .SelectProviderType("Training Provider")
        //        .ValidateResponsibleTeamLinkFieldText(teamName)
        //        .ClickSaveButton()
        //        .WaitForRecordToBeSaved(_providerName);

        //    providerRecordPage
        //        .WaitForProviderRecordPageToLoad()
        //        .TapDetailsTab();

        //    providersRecordPage
        //        .WaitForProvidersRecordPageToLoad()
        //        .InsertPropertyName("CDV6-22133 PNA A" + currentTimeString)
        //        .WaitForProvidersRecordPageToLoad()
        //        .ClickAddressSearchButton();

        //    System.Threading.Thread.Sleep(3500);

        //    providersRecordPage
        //        .WaitForProvidersRecordPageToLoad()
        //        .ValidatePropertyNameFieldValue("CDV6-22133 PNA A" + currentTimeString)
        //        .ValidatePropertyNoFieldValue("CDV6-22133 PNO A" + currentTimeString)
        //        .ValidateStreetFieldValue("CDV6-22133 ST A" + currentTimeString)
        //        .ValidateVillageDistrictFieldValue("CDV6-22133 VLG A" + currentTimeString)
        //        .ValidateTownCityFieldValue("CDV6-22133 TW A" + currentTimeString)
        //        .ValidatePostcodeFieldValue("CR0 22133")
        //        .ValidateCountyFieldValue("CDV6-22133 CON A" + currentTimeString)
        //        .ValidateCountryFieldValue("CDV6-22133 COUNTRY A" + currentTimeString);

        //    #endregion

        //}

        [TestProperty("JiraIssueID", "ACC-3577")]
        [Description("This Test case is to Create a new 'Training Provider' under Provider type section")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Provider")]
        [TestProperty("Screen1", "Providers")]
        public void ProviderType_UITestCases003()
        {
            #region Step 8 - 9

            _providerName = "CDV6-22170_Provider 2" + DateTime.Now.ToString("ddMMyyyyHHmmss");

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .ClickNewRecordButton();

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .InsertName(_providerName)
                .SelectProviderType("Training Provider")
                .ValidateResponsibleTeamLinkFieldText(teamName)
                .InsertStartDateOfAddress(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .SelectAddressTypeId("Care Address")
                .InsertPropertyName("Property Name")
                .InsertPostCodeNo("PP NO")
                .InsertStreetName("ST Name")
                .InsertVlgDistrictName("VLG")
                .InsertTownCityName("CITY")
                .InsertCounty("COUNTY")
                .InsertPostCodeNo("PCN")
                .InsertCountryName("CNTRY");


            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickClearAddressButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Are you sure you want to clear address fields?")
                .TapOKButton();

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ValidatePropertyNameFieldValue("")
                .ValidatePropertyNoFieldValue("")
                .ValidateStreetFieldValue("")
                .ValidateVillageDistrictFieldValue("")
                .ValidateTownCityFieldValue("")
                .ValidatePostcodeFieldValue("")
                .ValidateCountyFieldValue("")
                .ValidateCountryFieldValue("")
                .ClickSaveButton();

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickBackButton();

            string _provider2Name = "CDV6-22170_Provider 2" + DateTime.Now.ToString("ddMMyyyyHHmmss");

            providersPage
                .WaitForProvidersPageToLoad()
                .ClickNewRecordButton();

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .InsertName(_provider2Name)
                .SelectProviderType("Training Provider")
                .ValidateResponsibleTeamLinkFieldText(teamName)
                .InsertTownCityName("Chennai")
                .ClickSaveButton()
                .WaitForRecordToBeSaved(_provider2Name);

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .TapDetailsTab();

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ValidateTownCityFieldValue("Chennai");

            #endregion

            #region Step 10

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickBackButton();

            string _providerOtherTypeName = "CDV6-22170_Provider other" + DateTime.Now.ToString("ddMMyyyyHHmmss");

            providersPage
                .WaitForProvidersPageToLoad()
                .ClickNewRecordButton();

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .InsertName(_providerOtherTypeName)
                .SelectProviderType("Hospital")
                .ValidateResponsibleTeamLinkFieldText(teamName)
                .ClickSaveButton()
                .WaitForRecordToBeSaved(_providerOtherTypeName)
                .ClickDetailsTab();

            providersRecordPage
                .ValidateNameFieldVisible(true)
                .ValidateProviderTypeFieldVisible(true)
                .ValidateResponsibleTeamLinkFieldVisible(true)
                .ValidateAccountNumberFieldVisible(true)
                .ValidateParentProviderLookupVisible(true)
                .ValidateCqcLocationIdFieldVisible(true)
                .ValidateOdsCodeFieldVisible(false);

            #endregion

            #region Step 11

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickBackButton();

            string _provider3Name = "CDV6-22170_Provider 3" + DateTime.Now.ToString("ddMMyyyyHHmmss");

            providersPage
                .WaitForProvidersPageToLoad()
                .ClickNewRecordButton();

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .InsertName(_provider3Name)
                .SelectProviderType("Training Provider")
                .ValidateResponsibleTeamLinkFieldText(teamName)
                .ClickSaveButton()
                .WaitForRecordToBeSaved(_provider3Name)
                .ClickDetailsTab();

            providersRecordPage
                .ValidateNameFieldVisible(true)
                .ValidateProviderTypeFieldVisible(true)
                .ValidateResponsibleTeamLinkFieldVisible(true)
                .ValidateAccountNumberFieldVisible(false)
                .ValidateParentProviderLookupVisible(false)
                .ValidateCqcLocationIdFieldVisible(false)
                .ValidateOdsCodeFieldVisible(false);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3578")]
        [Description("Login into CD." +
            "Navigate to Advanced Search and move to Provider Search section." +
            "Click create new Provider button and validate 'Training Provider' Record should be create." +
            "Validate Menu > Sub Menu options for created Provider record." +
            "Validate Timeline, Summary and Details tabs for created Provider record.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Provider")]
        [TestProperty("Screen1", "Providers")]
        [TestProperty("Screen2", "Advanced Search")]
        public void ProviderType_UITestCases004()
        {
            #region Create New Provider

            _providerName = "22171_Provider_1_" + DateTime.Now.ToString("ddMMyyyyHHmmss");
            _providerId1 = dbHelper.provider.CreateProvider(_providerName, _careProviders_TeamId, 12); // Creating a "Training Provider" provider

            #endregion

            #region Step 12

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Providers")
                .SelectFilter("1", "Name")
                .SelectOperator("1", "Equals")
                .InsertRuleValueText("1", _providerName)
                .ClickSearchButton();

            System.Threading.Thread.Sleep(3000);

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_providerId1.ToString());

            #endregion

            #region Step 13

            advanceSearchPage
                .ClickNewRecordButton_ResultsPage();

            providersRecordPage
                .WaitForProvidersRecordPageFromAdvancedSearchToLoad()
                .ValidateProviderTypeFieldValue("Training Provider")
                .InsertName("22171_Provider_4_" + currentTimeString)
                .SelectProviderType("Training Provider")
                .ValidateResponsibleTeamLinkFieldText(teamName)
                .ClickSaveButton()
                .WaitForRecordToBeSaved("22171_Provider_4_" + currentTimeString);

            #endregion

            #region Step 14

            providersRecordPage
                .ClickOnMenuAndValidateSubMenus();

            #endregion

            #region Step 15

            providersRecordPage
                .ValidateTimelineTabIsDisplayed()
                .ValidateSummaryTabIsDisplayed()
                .ValidateDetailsTabIsDisplayed();

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4180

        [TestProperty("JiraIssueID", "ACC-4364")]
        [Description("Step(s) 1 to 9 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule1", "Provider")]
        [TestProperty("Screen1", "Providers")]
        public void Provider_Finance_UITestCases001()
        {

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .ClickNewRecordButton();

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .SelectProviderType("Residential Establishment")
                .ValidateGeneralSectionTitleVisible(true)
                .ValidateContactSectionTitleVisible(true)
                .ValidateAddressSectionTitleVisible(true)
                .ValidateFinanceDetailsSectionTitleVisible(true)
                .ValidateBankDetailsSectionTitleVisible(true)
                .ValidateClassificationSectionTitleVisible(true)
                .ValidateNotesSectionTitleVisible(true);

            #endregion

            #region Step 2

            providersRecordPage
                .ValidateGeneralSectionFieldsVisible(true);

            #endregion

            #region Step 3

            providersRecordPage
                .ValidateContactSectionFieldsVisible(true);

            #endregion

            #region Step 4

            providersRecordPage
                .ValidateAddressSectionFieldsVisible(true);

            #endregion

            #region Step 5

            providersRecordPage
                .ValidateFinanceDetailsSectionFieldsVisible(true);

            #endregion

            #region Step 6

            providersRecordPage
                .ValidateSuspendDebtorInvoicesReasonLookupButtonDisabled(true)
                .ClickSuspendDebtorInvoicesYesRadioButton()
                .ValidateSuspendDebtorInvoicesReasonLookupButtonDisabled(false);

            #endregion

            #region Step 7

            providersRecordPage
                .ValidateBankDetailsSectionFieldsVisible(true);

            #endregion

            #region Step 8

            providersRecordPage
                .ValidateClassificationSectionFieldsVisible(true);

            #endregion

            #region Step 9

            //Not valid for automation. The fields “Creditor No“ and “Debtor No 2“ are linked with the Finance business module. This BM is not available for care providers 

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4365")]
        [Description("Step(s) 10 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Provider")]
        [TestProperty("Screen1", "Providers")]
        public void Provider_Finance_UITestCases002()
        {
            #region Provider

            var providerName = "ACC-4180 " + currentTimeString;
            var addressType = 10; //Home
            var providerID = commonMethodsDB.CreateProvider(providerName, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL");

            #endregion

            #region Step 10

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(providerName)
                .OpenProviderRecord(providerID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickDetailsTab()
                .InsertPropertyName("pna updated")
                .ClickCalculateAnnualLeaveForEmployeesNoRadioButton()
                .ClickSaveButton();

            addressActionPopUp
                .WaitForAddressActionPopUpToLoad()
                .SelectViewByText("Create new address record")
                .SelectViewByText("Update existing address record")
                .TapOkButton();

            providersRecordPage
                .WaitForProvidersRecordPageToLoad();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4366")]
        [Description("Step(s) 11 to 14 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Provider")]
        [TestProperty("BusinessModule2", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Providers")]
        [TestProperty("Screen2", "Advanced Search")]
        public void Provider_Finance_UITestCases003()
        {
            #region Suspend Debtor Invoices Reason

            var suspendDebtorInvoicesReasonId = commonMethodsDB.CreateCPSuspendDebtorInvoicesReason(_careProviders_TeamId, "SDIR ACC_884", new DateTime(2023, 1, 1), 999);

            #endregion

            #region Finance Code Location

            var financeCodeLocationId = dbHelper.careProviderFinanceCodeLocation.GetByName("Organisation")[0];

            #endregion

            #region Finance Code

            var financeCodeId = commonMethodsDB.CreateCareProviderFinanceCode(_careProviders_TeamId, financeCodeLocationId, "ACC_884", "FC_ACC_884");

            #endregion

            #region Provider

            var providerName = "ACC-4180 " + currentTimeString;
            var addressType = 10; //Home
            var providerID = commonMethodsDB.CreateProvider(providerName, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL");

            dbHelper.provider.UpdateFinanceDetails(providerID, "", true, "", 2, "", null, "", null);

            var providerNumber = (dbHelper.provider.GetProviderByID(providerID, "providernumber")["providernumber"]).ToString();

            #endregion

            #region Step 11

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Providers");

            advanceSearchPage
                .SelectFilter("1", "Suspend Debtor Invoices?")
                .SelectOperator("1", "Equals")
                .SelectPicklistRuleValue("1", "Yes");

            advanceSearchPage
                .ClickAddRuleButton(1)

                .SelectFilter("2", "Suspend Debtor Invoices Reason")
                .SelectOperator("2", "Does Not Contain Data")

                .ClickAddRuleButton(1);

            advanceSearchPage
                .SelectFilter("3", "Id")
                .SelectOperator("3", "Equals")
                .InsertRuleValueText("3", providerNumber)

                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(providerID.ToString())
                .ClickNewRecordButton_ResultsPage();

            providersRecordPage
                .WaitForProvidersRecordPageFromAdvancedSearchToLoad();

            #endregion

            #region Step 12

            providersRecordPage
                .SelectProviderType("Residential Establishment")
                .ClickSuspendDebtorInvoicesYesRadioButton()
                .ClickSuspendDebtorInvoicesReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("SDIR ACC_884", suspendDebtorInvoicesReasonId);

            providersRecordPage
                .WaitForProvidersRecordPageFromAdvancedSearchToLoad()
                .ClickFinanceCodeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("ACC_884", financeCodeId);

            providersRecordPage
                .WaitForProvidersRecordPageFromAdvancedSearchToLoad();

            #endregion

            #region Step 13

            //Providers in care provider tenants do seem to contain any section called facilities

            #endregion

            #region Step 14

            providersRecordPage
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Providers");

            advanceSearchPage
                .ValidateFilterNotAvailable("1", "creditor number")
                .ValidateFilterNotAvailable("1", "debtor number 2")
                .ValidateFilterNotAvailable("1", "provider gl code")
                .ValidateFilterNotAvailable("1", "sector gl code")
                .ValidateFilterNotAvailable("1", "bank roll number")
                .ValidateFilterNotAvailable("1", "Advocacy Policy")
                .ValidateFilterNotAvailable("1", "En-Suite Rooms")
                .ValidateFilterNotAvailable("1", "London Wide Contract")
                .ValidateFilterNotAvailable("1", "Visiting Chiropodist")
                .ValidateFilterNotAvailable("1", "Visiting Dentist")
                .ValidateFilterNotAvailable("1", "Double Rooms")
                .ValidateFilterNotAvailable("1", "Genders Accommodated")
                .ValidateFilterNotAvailable("1", "Single Rooms")
                .ValidateFilterNotAvailable("1", "Visiting Optician")
                .ValidateFilterNotAvailable("1", "Maximum No Of Places");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4367")]
        [Description("Step(s) 15 to 16 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Provider")]
        [TestProperty("Screen1", "Providers")]
        public void Provider_Finance_UITestCases004()
        {
            #region Provider

            var providerName = "ACC-4180 " + currentTimeString;
            var addressType = 10; //Home
            var providerID = commonMethodsDB.CreateProvider(providerName, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL");

            dbHelper.provider.UpdateFinanceDetails(providerID, "", true, "", 2, "", null, "", null);

            var providerNumber = (dbHelper.provider.GetProviderByID(providerID, "providernumber")["providernumber"]).ToString();

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "PersonContractsUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "PersonContracts", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default_" + currentTimeString;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractSchemeId = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerID, providerID);

            #endregion

            #region Care Provider Service

            var careProviderServiceId = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "Default Care Provider Service", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", providerID, providerID, careProviderContractSchemeId, careProviderServiceId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);
            var contractid = (dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "contractid")["contractid"]).ToString();

            #endregion

            #region Contract Service Rate Period

            var careProviderContractServiceRatePeriodId = dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), careProviderRateUnitId, 15, _careProviders_TeamId);

            #endregion

            #region Step 15

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(providerName)
                .OpenProviderRecord(providerID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickDetailsTab()
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab()
                .WaitForProvidersRecordPageToLoad()
                .ClickFinanceTransactionsTab();

            #endregion

            #region Step 16

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesWithRatesTab();

            contractServicesWithRatesPage
                .WaitForContractServicesWithRatesPageToLoad()
                .ValidateRecordCellText(careProviderContractServiceRatePeriodId, 2, contractid)
                .ValidateRecordCellText(careProviderContractServiceRatePeriodId, 3, providerName)
                .ValidateRecordCellText(careProviderContractServiceRatePeriodId, 4, providerName)
                .ValidateRecordCellText(careProviderContractServiceRatePeriodId, 5, "Default Care Provider Service")
                .ValidateRecordCellText(careProviderContractServiceRatePeriodId, 6, "")
                .ValidateRecordCellText(careProviderContractServiceRatePeriodId, 7, "")
                .ValidateRecordCellText(careProviderContractServiceRatePeriodId, 8, "01/01/2023")
                .ValidateRecordCellText(careProviderContractServiceRatePeriodId, 9, "No")
                .ValidateRecordCellText(careProviderContractServiceRatePeriodId, 10, "Default Care Provider Rate Unit")
                .ValidateRecordCellText(careProviderContractServiceRatePeriodId, 11, "15.00")
                .ValidateRecordCellText(careProviderContractServiceRatePeriodId, 12, "")
                .ValidateRecordCellText(careProviderContractServiceRatePeriodId, 13, "")
                .ValidateRecordCellText(careProviderContractServiceRatePeriodId, 14, "")
                .ValidateRecordCellText(careProviderContractServiceRatePeriodId, 15, "")
                .ValidateRecordCellText(careProviderContractServiceRatePeriodId, 16, "")
                .ValidateRecordCellText(careProviderContractServiceRatePeriodId, 17, "Standard Rated")
                .ValidateRecordCellText(careProviderContractServiceRatePeriodId, 18, "Default Care Provider Batch Grouping")
                .ValidateRecordCellText(careProviderContractServiceRatePeriodId, 19, "Spot")
                .ValidateRecordCellText(careProviderContractServiceRatePeriodId, 20, "No")
                ;

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4368")]
        [Description("Step(s) 17 to 21 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Provider")]
        [TestProperty("Screen1", "Providers")]
        public void Provider_Finance_UITestCases005()
        {
            #region Mail Merge

            var mailMergeTemplateId = dbHelper.mailMerge.GetByName("CP Staff Timesheet Template - By Role")[0];
            dbHelper.mailMerge.UpdatePrintFileType(mailMergeTemplateId, 1); //Word

            #endregion

            #region Business Unit

            var businessUnitId = commonMethodsDB.CreateBusinessUnit("ACC 4180 BU1");

            #endregion

            #region Team

            var teamId = commonMethodsDB.CreateTeam("ACC 4180 T1", _defaultLoginUserID, businessUnitId, "ACC4180T1", "ACC4180T1@somemail.com", "....", "123456789");

            #endregion

            #region Provider

            var providerName = "ACC-4180 " + currentTimeString;
            var addressType = 10; //Home
            var providerID = commonMethodsDB.CreateProvider(providerName, teamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL");

            dbHelper.provider.UpdateFinanceDetails(providerID, "", true, "", 2, "", null, "", null);

            var providerNumber = (dbHelper.provider.GetProviderByID(providerID, "providernumber")["providernumber"]).ToString();

            #endregion

            #region Create SystemUser Record

            var systemUserName = "ACC_4180_User2";
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(systemUserName, "ACC_4180", "User2", "Passw0rd_!", businessUnitId, teamId, _languageId, authenticationproviderid);
            dbHelper.systemUser.UpdateSystemUserTimezone(systemUserId, TimeZone.CurrentTimeZone.StandardName);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Grip", "888", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Hourly

            var employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Hourly", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region Employment Contract 

            commonMethodsDB.CreateSystemUserEmploymentContract(systemUserId, new DateTime(2020, 1, 1), _careProviderStaffRoleTypeid, teamId, employmentContractTypeid, 48);

            #endregion

            #region Step 17

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(providerName)
                .SelectProviderRecord(providerID)
                .ClickMailMergeButton();

            mailMergePopup
                .WaitForMailMergePopupToLoad()
                .SelectMailMergeTemplateByText("CP Staff Timesheet Template - By Role")
                .TapCreateActivityNoRadioButton()
                .ClickOKButton();

            fileIOHelper.WaitForFileToExist(this.DownloadsDirectory, "CP-Staff-Timesheet-Template---By-Role.docx", 10);
            msWordHelper.ValidateWordsPresent(this.DownloadsDirectory + "\\CP-Staff-Timesheet-Template---By-Role.docx", "Staff Timesheet", providerName, "Date: " + DateTime.Now.ToString("dd'/'MM'/'yyyy"));

            #endregion

            #region Step 18

            msWordHelper.ValidateWordsPresent(this.DownloadsDirectory + "\\CP-Staff-Timesheet-Template---By-Role.docx", "Staff Name: ACC_4180 User2", "Grip", "Mon ", "Tue ", "Wed ", "Thu ", "Fri ", "Sat ", "Sun ", "Total");

            #endregion

            #region Step 19

            var matchingRecords = dbHelper.systemSetting.GetSystemSettingIdByName("CPStaffTimesheet.Settings.NumberOfDaysToAdd");
            Assert.AreEqual(1, matchingRecords.Count);

            matchingRecords = dbHelper.systemSetting.GetSystemSettingIdByName("CPStaffTimesheet.Settings.DayOfWeek");
            Assert.AreEqual(1, matchingRecords.Count);

            matchingRecords = dbHelper.systemSetting.GetSystemSettingIdByName("CPStaffTimesheet.Settings.StartDayInMonth");
            Assert.AreEqual(1, matchingRecords.Count);

            #endregion

            #region Step 20

            var settingid = dbHelper.systemSetting.GetSystemSettingIdByName("CPStaffTimesheet.Settings.StartDayInMonth")[0];
            var startDayInMonth = int.Parse((dbHelper.systemSetting.GetByID(settingid, "settingvalue")["settingvalue"]).ToString());
            var dateToFind = "";

            if (DateTime.Now.Day <= startDayInMonth)
                dateToFind = DateTime.Now.ToString("01'/'MM'/'yyyy");
            else
                dateToFind = DateTime.Now.AddMonths(1).ToString("01'/'MM'/'yyyy");

            msWordHelper.ValidateWordsPresent(this.DownloadsDirectory + "\\CP-Staff-Timesheet-Template---By-Role.docx", dateToFind);

            #endregion

            #region Step 21

            msWordHelper.ValidateWordsPresent(this.DownloadsDirectory + "\\CP-Staff-Timesheet-Template---By-Role.docx", "Total Contract Hours Per Week For All Roles: 48.00", "Grip");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4369")]
        [Description("Step(s) 22 to 24 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Provider")]
        [TestProperty("BusinessModule2", "Mail Merge")]
        [TestProperty("Screen1", "Providers")]
        public void Provider_Finance_UITestCases006()
        {
            #region Mail Merge

            var mailMergeTemplateId = dbHelper.mailMerge.GetByName("TimeSheet Template - Manual Entry For Role")[0];
            dbHelper.mailMerge.UpdatePrintFileType(mailMergeTemplateId, 1); //Word

            #endregion

            #region Business Unit

            var businessUnitId = commonMethodsDB.CreateBusinessUnit("ACC 4180 BU1");

            #endregion

            #region Team

            var teamId = commonMethodsDB.CreateTeam("ACC 4180 T1", _defaultLoginUserID, businessUnitId, "ACC4180T1", "ACC4180T1@somemail.com", "....", "123456789");

            #endregion

            #region Provider

            var providerName = "ACC-4180 " + currentTimeString;
            var addressType = 10; //Home
            var providerID = commonMethodsDB.CreateProvider(providerName, teamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL");

            dbHelper.provider.UpdateFinanceDetails(providerID, "", true, "", 2, "", null, "", null);

            var providerNumber = (dbHelper.provider.GetProviderByID(providerID, "providernumber")["providernumber"]).ToString();

            #endregion

            #region Create SystemUser Record

            var systemUserName = "ACC_4180_User2";
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(systemUserName, "ACC_4180", "User2", "Passw0rd_!", businessUnitId, teamId, _languageId, authenticationproviderid);
            dbHelper.systemUser.UpdateSystemUserTimezone(systemUserId, TimeZone.CurrentTimeZone.StandardName);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Grip", "888", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Hourly

            var employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Hourly", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region Employment Contract 

            commonMethodsDB.CreateSystemUserEmploymentContract(systemUserId, new DateTime(2020, 1, 1), _careProviderStaffRoleTypeid, teamId, employmentContractTypeid, 48);

            #endregion

            #region Step 22

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(providerName)
                .SelectProviderRecord(providerID)
                .ClickMailMergeButton();

            mailMergePopup
                .WaitForMailMergePopupToLoad()
                .SelectMailMergeTemplateByText("TimeSheet Template - Manual Entry For Role")
                .TapCreateActivityNoRadioButton()
                .ClickOKButton();

            fileIOHelper.WaitForFileToExist(this.DownloadsDirectory, "CP-Staff-Timesheet-Template---No-Role.docx", 10);
            msWordHelper.ValidateWordsPresent(this.DownloadsDirectory + "\\CP-Staff-Timesheet-Template---No-Role.docx", "Staff Timesheet", providerName, "Date: " + DateTime.Now.ToString("dd'/'MM'/'yyyy"));

            #endregion

            #region Step 23

            msWordHelper.ValidateWordsPresent(this.DownloadsDirectory + "\\CP-Staff-Timesheet-Template---No-Role.docx", "Sign In", "Sign Out", "Role", "Annual Leave", "Hours Worked", "Total", "Initial");

            #endregion

            #region Step 24

            //this step will be ignored. we cannot test the generation of PDFs. We can only test for the download of docx documents

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4370")]
        [Description("Step(s) 25 to 29 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Provider")]
        [TestProperty("BusinessModule2", "Mail Merge")]
        [TestProperty("Screen1", "Providers")]
        public void Provider_Finance_UITestCases007()
        {
            #region Mail Merge

            var mailMergeTemplateId = dbHelper.mailMerge.GetByName("CP Staff Monthly Residential Rota")[0];
            dbHelper.mailMerge.UpdatePrintFileType(mailMergeTemplateId, 1); //Word

            #endregion

            #region Business Unit

            var businessUnitId = commonMethodsDB.CreateBusinessUnit("ACC 4180 BU2");

            #endregion

            #region Team

            var teamId = commonMethodsDB.CreateTeam("ACC 4180 T2", _defaultLoginUserID, businessUnitId, "ACC4180T2", "ACC4180T2@somemail.com", "....", "123456789");

            #endregion

            #region Recurrence Patterns

            var _recurrencePattern_Every1WeekMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on monday").First();
            var _recurrencePattern_Every1WeekTuesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on tuesday").First();
            var _recurrencePattern_Every1WeekWednesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on wednesday").First();
            var _recurrencePattern_Every1WeekThursdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on thursday").First();
            var _recurrencePattern_Every1WeekFridayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on friday").First();
            var _recurrencePattern_Every1WeekSaturdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on saturday").First();
            var _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();

            #endregion 

            #region Provider

            var providerName = "ACC-4180 " + currentTimeString;
            var addressType = 10; //Home
            var providerID = commonMethodsDB.CreateProvider(providerName, teamId, 13, true, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL");
            dbHelper.provider.UpdateFinanceDetails(providerID, "", true, "", 2, "", null, "", null);

            #endregion

            #region Create SystemUser Record

            var systemUserName = "ACC_4180_U_" + currentTimeString;
            var systemUserFullName = "ACC_4180 U_" + currentTimeString;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(systemUserName, "ACC_4180", "U_" + currentTimeString, "Passw0rd_!", businessUnitId, teamId, _languageId, authenticationproviderid);
            dbHelper.systemUser.UpdateSystemUserTimezone(systemUserId, TimeZone.CurrentTimeZone.StandardName);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Grip", "888", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Hourly

            var employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Hourly", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region Booking Type

            //"Booking (to internal care activity)" & "Count full booking length"

            Guid _bookingTypeID = Guid.Empty;

            if (!dbHelper.cpBookingType.GetByName("ACC_4180 BT2").Any())
                _bookingTypeID = dbHelper.cpBookingType.CreateBookingType("ACC_4180 BT2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null);

            if (_bookingTypeID == Guid.Empty)
                _bookingTypeID = dbHelper.cpBookingType.GetByName("ACC_4180 BT2").First();

            #endregion

            #region Provider Allowable Booking Types

            dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(teamId, providerID, _bookingTypeID, true);

            #endregion

            #region System User Employment Contract 

            var SystemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUserId, new DateTime(2020, 1, 1), _careProviderStaffRoleTypeid, teamId, employmentContractTypeid, 48);

            //Link Booking Type with the Employment Contract
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(SystemUserEmploymentContractId, _bookingTypeID);

            #endregion

            #region Availability Type

            var availabilityTypes_StandardId = commonMethodsDB.CreateAvailabilityType("Standard", 3, false, _careProviders_TeamId, 1, 1, true);

            #endregion

            #region User Work Schedule

            var workScheduleDate = DateTime.Now.Date;

            switch (workScheduleDate.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    dbHelper.userWorkSchedule.CreateUserWorkSchedule(systemUserId, teamId, _recurrencePattern_Every1WeekSundayId, SystemUserEmploymentContractId, availabilityTypes_StandardId, workScheduleDate, null, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1);
                    break;
                case DayOfWeek.Monday:
                    dbHelper.userWorkSchedule.CreateUserWorkSchedule(systemUserId, teamId, _recurrencePattern_Every1WeekMondayId, SystemUserEmploymentContractId, availabilityTypes_StandardId, workScheduleDate, null, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1);
                    break;
                case DayOfWeek.Tuesday:
                    dbHelper.userWorkSchedule.CreateUserWorkSchedule(systemUserId, teamId, _recurrencePattern_Every1WeekTuesdayId, SystemUserEmploymentContractId, availabilityTypes_StandardId, workScheduleDate, null, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1);
                    break;
                case DayOfWeek.Wednesday:
                    dbHelper.userWorkSchedule.CreateUserWorkSchedule(systemUserId, teamId, _recurrencePattern_Every1WeekWednesdayId, SystemUserEmploymentContractId, availabilityTypes_StandardId, workScheduleDate, null, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1);
                    break;
                case DayOfWeek.Thursday:
                    dbHelper.userWorkSchedule.CreateUserWorkSchedule(systemUserId, teamId, _recurrencePattern_Every1WeekThursdayId, SystemUserEmploymentContractId, availabilityTypes_StandardId, workScheduleDate, null, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1);
                    break;
                case DayOfWeek.Friday:
                    dbHelper.userWorkSchedule.CreateUserWorkSchedule(systemUserId, teamId, _recurrencePattern_Every1WeekFridayId, SystemUserEmploymentContractId, availabilityTypes_StandardId, workScheduleDate, null, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1);
                    break;
                case DayOfWeek.Saturday:
                    dbHelper.userWorkSchedule.CreateUserWorkSchedule(systemUserId, teamId, _recurrencePattern_Every1WeekSaturdayId, SystemUserEmploymentContractId, availabilityTypes_StandardId, workScheduleDate, null, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1);
                    break;
                default:
                    break;
            }

            #endregion

            #region CP Booking Schedule

            int StartDayOfWeekId = (int)workScheduleDate.DayOfWeek;
            int EndDayOfWeekId = StartDayOfWeekId;
            var cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(teamId, _bookingTypeID, 1, StartDayOfWeekId, EndDayOfWeekId, new TimeSpan(9, 0, 0), new TimeSpan(12, 0, 0), providerID, "Express Book Testing");

            #endregion

            #region CP Booking Schedule Staff

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(teamId, cpBookingScheduleId, SystemUserEmploymentContractId, systemUserId);

            #endregion

            #region CP Express Booking Criteria

            int statusid = 1;
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();
            DateTime StartDate = commonMethodsHelper.GetFirstMondayOfMonthAndYear(currentDate.AddMonths(1).Month, currentDate.AddMonths(1).Year);
            DateTime bookingStartDate = StartDate;
            DateTime bookingEndDate = bookingStartDate.AddDays(6);

            var expressbookingcriteriaid = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(
                teamId, businessUnitId, "title", statusid, providerID, bookingStartDate, bookingEndDate, currentDate, providerID, "provider", providerName);

            #endregion

            #region execute the scheduled job to pin the records

            Guid systemUserEmploymentContractJobId = dbHelper.scheduledJob.GetScheduledJobByRecordId(providerID)[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(systemUserEmploymentContractJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(systemUserEmploymentContractJobId);

            #endregion


            #region Step 25

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(providerName)
                .SelectProviderRecord(providerID)
                .ClickMailMergeButton();

            mailMergePopup
                .WaitForMailMergePopupToLoad()
                .SelectMailMergeTemplateByText("CP Staff Monthly Residential Rota")
                .TapCreateActivityNoRadioButton()
                .ClickOKButton();

            fileIOHelper.WaitForFileToExist(this.DownloadsDirectory, "CP-Staff-Monthly-Residential-Rota.docx", 10);
            msWordHelper.ValidateWordsPresent(this.DownloadsDirectory + "\\CP-Staff-Monthly-Residential-Rota.docx", "Monthly Residential Staff Rota", "Date: " + DateTime.Now.ToString("dd'/'MM'/'yyyy"), "Page: ");

            #endregion

            #region Step 26

            dbHelper.provider.UpdateEnableScheduling(providerID, false);

            mailMergePopup
                .ClickOKButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Please select a Provider where the Provider Type is Residential Establishment and Scheduling is Enabled.")
                .TapCloseButton();

            #endregion

            #region Step 27

            //the system setting "MonthlyRotaReport.PrintNextMonthReport" is not exported by default. the end users will need to create it

            #endregion

            #region Step 28

            msWordHelper.ValidateWordsPresent(this.DownloadsDirectory + "\\CP-Staff-Monthly-Residential-Rota.docx", bookingStartDate.ToString("MMMM"), "S", "M", "T", "W", "T", "F", "S", providerName, systemUserFullName + " (Grip)", "??");

            #endregion

            #region Step 29

            msWordHelper.ValidateWordsPresent(this.DownloadsDirectory + "\\CP-Staff-Monthly-Residential-Rota.docx", "48.00");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4371")]
        [Description("Step(s) 30 to 35 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Provider")]
        [TestProperty("BusinessModule2", "Mail Merge")]
        [TestProperty("Screen1", "Providers")]
        public void Provider_Finance_UITestCases008()
        {
            #region Mail Merge

            var mailMergeTemplateId = dbHelper.mailMerge.GetByName("CP Staff Weekly Residential Rota")[0];
            dbHelper.mailMerge.UpdatePrintFileType(mailMergeTemplateId, 1); //Word

            #endregion

            #region Business Unit

            var businessUnitId = commonMethodsDB.CreateBusinessUnit("ACC 4180 BU2");

            #endregion

            #region Team

            var teamId = commonMethodsDB.CreateTeam("ACC 4180 T2", _defaultLoginUserID, businessUnitId, "ACC4180T2", "ACC4180T2@somemail.com", "....", "123456789");

            #endregion

            #region Recurrence Patterns

            var _recurrencePattern_Every1WeekMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on monday").First();
            var _recurrencePattern_Every1WeekTuesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on tuesday").First();
            var _recurrencePattern_Every1WeekWednesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on wednesday").First();
            var _recurrencePattern_Every1WeekThursdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on thursday").First();
            var _recurrencePattern_Every1WeekFridayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on friday").First();
            var _recurrencePattern_Every1WeekSaturdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on saturday").First();
            var _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();

            #endregion 

            #region Provider

            var providerName = "ACC-4180 " + currentTimeString;
            var addressType = 10; //Home
            var providerID = commonMethodsDB.CreateProvider(providerName, teamId, 13, true, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL");
            dbHelper.provider.UpdateFinanceDetails(providerID, "", true, "", 2, "", null, "", null);

            #endregion

            #region Create SystemUser Record

            var systemUserName = "ACC_4180_U_" + currentTimeString;
            var systemUserFullName = "ACC_4180 U_" + currentTimeString;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(systemUserName, "ACC_4180", "U_" + currentTimeString, "Passw0rd_!", businessUnitId, teamId, _languageId, authenticationproviderid);
            dbHelper.systemUser.UpdateSystemUserTimezone(systemUserId, TimeZone.CurrentTimeZone.StandardName);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Grip", "888", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Hourly

            var employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Hourly", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region Booking Type

            //"Booking (to internal care activity)" & "Count full booking length"

            Guid _bookingTypeID = Guid.Empty;

            if (!dbHelper.cpBookingType.GetByName("ACC_4180 BT2").Any())
                _bookingTypeID = dbHelper.cpBookingType.CreateBookingType("ACC_4180 BT2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null);

            if (_bookingTypeID == Guid.Empty)
                _bookingTypeID = dbHelper.cpBookingType.GetByName("ACC_4180 BT2").First();

            #endregion

            #region Provider Allowable Booking Types

            dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(teamId, providerID, _bookingTypeID, true);

            #endregion

            #region System User Employment Contract 

            var SystemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUserId, new DateTime(2020, 1, 1), _careProviderStaffRoleTypeid, teamId, employmentContractTypeid, 48);

            //Link Booking Type with the Employment Contract
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(SystemUserEmploymentContractId, _bookingTypeID);

            #endregion

            #region Availability Type

            var availabilityTypes_StandardId = commonMethodsDB.CreateAvailabilityType("Standard", 3, false, _careProviders_TeamId, 1, 1, true);

            #endregion

            #region User Work Schedule

            var workScheduleDate = DateTime.Now.Date;

            switch (workScheduleDate.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    dbHelper.userWorkSchedule.CreateUserWorkSchedule(systemUserId, teamId, _recurrencePattern_Every1WeekSundayId, SystemUserEmploymentContractId, availabilityTypes_StandardId, workScheduleDate, null, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1);
                    break;
                case DayOfWeek.Monday:
                    dbHelper.userWorkSchedule.CreateUserWorkSchedule(systemUserId, teamId, _recurrencePattern_Every1WeekMondayId, SystemUserEmploymentContractId, availabilityTypes_StandardId, workScheduleDate, null, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1);
                    break;
                case DayOfWeek.Tuesday:
                    dbHelper.userWorkSchedule.CreateUserWorkSchedule(systemUserId, teamId, _recurrencePattern_Every1WeekTuesdayId, SystemUserEmploymentContractId, availabilityTypes_StandardId, workScheduleDate, null, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1);
                    break;
                case DayOfWeek.Wednesday:
                    dbHelper.userWorkSchedule.CreateUserWorkSchedule(systemUserId, teamId, _recurrencePattern_Every1WeekWednesdayId, SystemUserEmploymentContractId, availabilityTypes_StandardId, workScheduleDate, null, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1);
                    break;
                case DayOfWeek.Thursday:
                    dbHelper.userWorkSchedule.CreateUserWorkSchedule(systemUserId, teamId, _recurrencePattern_Every1WeekThursdayId, SystemUserEmploymentContractId, availabilityTypes_StandardId, workScheduleDate, null, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1);
                    break;
                case DayOfWeek.Friday:
                    dbHelper.userWorkSchedule.CreateUserWorkSchedule(systemUserId, teamId, _recurrencePattern_Every1WeekFridayId, SystemUserEmploymentContractId, availabilityTypes_StandardId, workScheduleDate, null, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1);
                    break;
                case DayOfWeek.Saturday:
                    dbHelper.userWorkSchedule.CreateUserWorkSchedule(systemUserId, teamId, _recurrencePattern_Every1WeekSaturdayId, SystemUserEmploymentContractId, availabilityTypes_StandardId, workScheduleDate, null, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1);
                    break;
                default:
                    break;
            }

            #endregion

            #region CP Booking Schedule

            int StartDayOfWeekId = (int)workScheduleDate.DayOfWeek;
            int EndDayOfWeekId = StartDayOfWeekId;
            var cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(teamId, _bookingTypeID, 1, StartDayOfWeekId, EndDayOfWeekId, new TimeSpan(9, 15, 0), new TimeSpan(10, 0, 0), providerID, "Express Book Testing");

            #endregion

            #region CP Booking Schedule Staff

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(teamId, cpBookingScheduleId, SystemUserEmploymentContractId, systemUserId);

            #endregion

            #region CP Express Booking Criteria

            int statusid = 1;
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();
            DateTime bookingStartDate = commonMethodsHelper.GetWeekFirstMonday(currentDate.AddDays(7)); //Monday of next week
            DateTime bookingEndDate = bookingStartDate.AddDays(6);

            var expressbookingcriteriaid = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(
                teamId, businessUnitId, "title", statusid, providerID, bookingStartDate, bookingEndDate, currentDate, providerID, "provider", providerName);

            #endregion

            #region execute the scheduled job to pin the records

            Guid systemUserEmploymentContractJobId = dbHelper.scheduledJob.GetScheduledJobByRecordId(providerID)[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(systemUserEmploymentContractJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(systemUserEmploymentContractJobId);

            #endregion



            #region Step 30

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(providerName)
                .SelectProviderRecord(providerID)
                .ClickMailMergeButton();

            mailMergePopup
                .WaitForMailMergePopupToLoad()
                .SelectMailMergeTemplateByText("CP Staff Weekly Residential Rota")
                .TapCreateActivityNoRadioButton()
                .ClickOKButton();

            fileIOHelper.WaitForFileToExist(this.DownloadsDirectory, "CP-Staff-Weekly-Residential-Rota.docx", 10);
            msWordHelper.ValidateWordsPresent(this.DownloadsDirectory + "\\CP-Staff-Weekly-Residential-Rota.docx", "Weekly Residential Staff Rota", "Week commencing: ", "Date: ", "Page: ", providerName);

            #endregion

            #region Step 31

            dbHelper.provider.UpdateEnableScheduling(providerID, false);

            mailMergePopup
                .ClickOKButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Please select a Provider where the Provider Type is Residential Establishment and Scheduling is Enabled.")
                .TapCloseButton();

            #endregion

            #region Step 32

            //the system setting "WeeklyRotaReport.PrintNextWeekReport" is not exported by default. the end users will need to create it

            #endregion

            #region Step 33

            //check tomorrow with Hari

            #endregion

            #region Step 34

            msWordHelper.ValidateWordsPresent(this.DownloadsDirectory + "\\CP-Staff-Weekly-Residential-Rota.docx",
                "Employee", systemUserFullName, "Grip", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday", "Role", "From", "To", "09:15", "10:00");

            #endregion

            #region Step 35

            mailMergePopup
                .WaitForMailMergePopupToLoad()
                .ClickCancelButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Booking Types")
                .SelectFilter("1", "Booking Type Short Code"); //validate that the field is accessible

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4372")]
        [Description("Step(s) 36 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Provider")]
        [TestProperty("BusinessModule2", "System Features")]
        [TestProperty("Screen1", "Providers")]
        public void Provider_Finance_UITestCases009()
        {
            #region Provider
            var addressType = 10; //Home

            var provider1Name = "ACC-4180 P1 " + currentTimeString;
            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL");

            var provider2Name = "ACC-4180 P2 " + currentTimeString;
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL");

            #endregion

            #region Step 36

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord("*" + currentTimeString)
                .SelectProviderRecord(provider1ID)
                .SelectProviderRecord(provider2ID)
                .ClickBulkEditButton();

            bulkEditDialogPopup
                .WaitForBulkEditDialogPopupToLoad("2");

            #endregion

        }


        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4394

        [TestProperty("JiraIssueID", "ACC-4502")]
        [Description("Step(s) 1 to 6 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void Provider_ContractServices_UITestCases001()
        {
            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4394_1 " + currentTimeString;
            var provider2Name = "ACC-4394_2 " + currentTimeString;
            var provider3Name = "ACC-4394_3 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 3, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Hospital
            var provider3ID = commonMethodsDB.CreateProvider(provider3Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var provider1Number = (dbHelper.provider.GetProviderByID(provider1ID, "providernumber")["providernumber"]).ToString();
            var provider2Number = (dbHelper.provider.GetProviderByID(provider2ID, "providernumber")["providernumber"]).ToString();

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractService", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider1ID);

            var contractScheme2Name = "CPCS_C_" + currentTimeString;
            var contractScheme2Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme2Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme2Name, new DateTime(2020, 1, 1), contractScheme2Code, provider3ID, provider3ID);


            #endregion

            #region Step 1

            var matchingOptionSets = dbHelper.optionSet.GetOptionSetIdByName("CP Contract Service Adjusted Days");
            Assert.AreEqual(1, matchingOptionSets.Count);
            var optionSetId = matchingOptionSets[0];

            var matchingOptionSetValues = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetId, "Charge End Date");
            Assert.AreEqual(1, matchingOptionSetValues.Count);

            matchingOptionSetValues = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetId, "Charge End Date Less 1 Day");
            Assert.AreEqual(1, matchingOptionSetValues.Count);

            #endregion

            #region Step 2

            matchingOptionSets = dbHelper.optionSet.GetOptionSetIdByName("CP Contract Service Type");
            Assert.AreEqual(1, matchingOptionSets.Count);
            optionSetId = matchingOptionSets[0];

            matchingOptionSetValues = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetId, "Spot");
            Assert.AreEqual(1, matchingOptionSetValues.Count);

            matchingOptionSetValues = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetId, "Block");
            Assert.AreEqual(1, matchingOptionSetValues.Count);

            #endregion

            #region Step 3

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();

            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .ClickNewRecordButton();

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ValidateGeneralSectionFieldsVisibleForNewRecord()
                .ValidateServicesSectionFieldsVisibleForNewRecord()
                .ValidateFinanceSectionFieldsVisibleForNewRecord()
                .ValidateOtherSettingsSectionFieldsVisibleForNewRecord()
                .ValidateNotesSectionFieldsVisibleForNewRecord();

            #endregion

            #region Step 4

            contractServiceRecordPage
                .ValidateInactive_NoRadioButtonChecked()
                .ValidateResponsibleUserLinkText("ProviderType User_1")
                .ValidateResponsibleTeamLinkText(teamName)
                .ValidateEstablishmentLinkText(provider1Name)
                .ValidateContractTypeSelectedText("Spot")
                .ValidateRoomsApply_NoRadioButtonChecked()
                .ValidateNegotiatedRatesApply_NoRadioButtonChecked();

            #endregion

            #region Step 5

            contractServiceRecordPage
                .ClickEstablishmentLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(provider1Name).TapSearchButton().ValidateResultElementPresent(provider1ID)
                .TypeSearchQuery(provider2Name).TapSearchButton().ValidateResultElementNotPresent(provider2ID)
                .ClickCloseButton();

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad();

            #endregion

            #region Step 6

            contractServiceRecordPage
                .ClickContractSchemeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(contractScheme1Name).TapSearchButton().ValidateResultElementPresent(careProviderContractScheme1Id)
                .TypeSearchQuery(contractScheme2Name).TapSearchButton().ValidateResultElementNotPresent(careProviderContractScheme2Id)
                .ClickCloseButton();

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4503")]
        [Description("Step(s) 8 to 9 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void Provider_ContractServices_UITestCases002()
        {
            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4394_1 " + currentTimeString;
            var provider2Name = "ACC-4394_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            var provider1Number = (dbHelper.provider.GetProviderByID(provider1ID, "providernumber")["providernumber"]).ToString();
            var provider2Number = (dbHelper.provider.GetProviderByID(provider2ID, "providernumber")["providernumber"]).ToString();

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractService", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider1ID);

            var contractScheme2Name = "CPCS_B_" + currentTimeString;
            var contractScheme2Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme2Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme2Name, new DateTime(2020, 1, 1), contractScheme2Code, provider2ID, provider2ID);


            #endregion

            #region Care Provider Service

            var defaultCareProviderServiceId = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "Default Care Provider Service", new DateTime(2020, 1, 1), 9999999);
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-879", new DateTime(2020, 1, 1), 9999900, null, false);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_careProviders_TeamId, "CPSD ACC-879", null, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Step 8

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();

            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .ClickNewRecordButton();

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ValidateContractTypeFieldDisabled(false);

            #endregion

            #region Step 9

            contractServiceRecordPage
                .ClickServiceLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Default Care Provider Service").TapSearchButton().ValidateResultElementPresent(defaultCareProviderServiceId)
                .SearchAndSelectRecord("CPS ACC-879", careProviderService1Id);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickServiceDetailLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("CPSD ACC-879", careProviderServiceDetailId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4504")]
        [Description("Step(s) 10 to 13 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void Provider_ContractServices_UITestCases003()
        {
            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4394_1 " + currentTimeString;
            var provider2Name = "ACC-4394_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, true, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 10, true, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractService", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider1ID);

            #endregion

            #region Care Provider Service

            var defaultCareProviderServiceId = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "Default Care Provider Service", new DateTime(2020, 1, 1), 9999999);
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-879 B", new DateTime(2020, 1, 1), 9999800, null, true);

            #endregion

            #region booking Type

            var bookingTypeClassId = 2; //Booking (to internal care activity)
            var workingContractedTime = 1; //Count full booking length
            var cpbookingchargetypeid = 1; //Per Booking
            var bookingTypeId = commonMethodsDB.CreateBookingType("BT_ACC4394", bookingTypeClassId, 60, new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0), workingContractedTime, false, null, null, null, cpbookingchargetypeid);

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, null, bookingTypeId, null, "");

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_careProviders_TeamId, provider1ID, bookingTypeId, true);

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);
            dbHelper.careProviderRateUnit.UpdateTimeAndDays(careProviderRateUnitId, true);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Step 10

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();

            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .ClickNewRecordButton();

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ValidateContractTypeFieldDisabled(false);

            contractServiceRecordPage
                .ClickContractSchemeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(contractScheme1Name, careProviderContractScheme1Id);

            contractServiceRecordPage
                .ClickServiceLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("CPS ACC-879 B", careProviderService1Id);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ValidateServiceDetailLookupButtonVisibility(false)
                .ClickBookingTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("BT_ACC4394", bookingTypeId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ValidateChargeScheduledCareOnActualsVisible(true);

            #endregion

            #region Step 12

            contractServiceRecordPage
                .ClickRateUnitLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default Care Provider Rate Unit", careProviderRateUnitId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad();

            #endregion

            #region Step 13

            contractServiceRecordPage
                .ClickVATCodeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Standard Rated", careProviderVATCodeId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4505")]
        [Description("Step(s) 14 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void Provider_ContractServices_UITestCases004()
        {
            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4394_1 " + currentTimeString;
            var provider2Name = "ACC-4394_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            var provider1Number = (dbHelper.provider.GetProviderByID(provider1ID, "providernumber")["providernumber"]).ToString();
            var provider2Number = (dbHelper.provider.GetProviderByID(provider2ID, "providernumber")["providernumber"]).ToString();

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractService", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider1ID);

            #endregion

            #region Care Provider Service

            var defaultCareProviderServiceId = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "Default Care Provider Service", new DateTime(2020, 1, 1), 9999999);
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-879", new DateTime(2020, 1, 1), 9999900, null, false);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_careProviders_TeamId, "CPSD ACC-879", 9100, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Step 14

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();

            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .ClickNewRecordButton();

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickContractSchemeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(contractScheme1Name, careProviderContractScheme1Id);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickServiceLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("CPS ACC-879", careProviderService1Id);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickServiceDetailLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("CPSD ACC-879", careProviderServiceDetailId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickRateUnitLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default Care Provider Rate Unit", careProviderRateUnitId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickVATCodeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Standard Rated", careProviderVATCodeId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .SelectAdjustedDays("Charge End Date")
                .SelectAdjustedDays("Charge End Date Less 1 Day")
                .ClickBatchGroupingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default Care Provider Batch Grouping", careProviderBatchGroupingId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ValidateNetIncomeFieldVisibility(false)
                .ClickSaveAndCloseButton();

            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .ClickSearchButton()
                .WaitForContractServicesPageToLoad();

            var allServiceRecords = dbHelper.careProviderContractService.GetByEstablishmentId(provider1ID);
            Assert.AreEqual(1, allServiceRecords.Count());

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4506")]
        [Description("Step(s) 15 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void Provider_ContractServices_UITestCases005()
        {
            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4394_1 " + currentTimeString;
            var provider2Name = "ACC-4394_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var provider1Number = (dbHelper.provider.GetProviderByID(provider1ID, "providernumber")["providernumber"]).ToString();
            var provider2Number = (dbHelper.provider.GetProviderByID(provider2ID, "providernumber")["providernumber"]).ToString();

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractService", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider2ID);

            #endregion

            #region Care Provider Service

            var defaultCareProviderServiceId = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "Default Care Provider Service", new DateTime(2020, 1, 1), 9999999);
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-879", new DateTime(2020, 1, 1), 9999900, null, false);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_careProviders_TeamId, "CPSD ACC-879", null, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Step 15

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();

            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .ClickNewRecordButton();

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickContractSchemeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(contractScheme1Name, careProviderContractScheme1Id);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ValidateNetIncomeFieldVisibility(true);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4507")]
        [Description("Step(s) 16 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void Provider_ContractServices_UITestCases006()
        {
            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4394_1 " + currentTimeString;
            var provider2Name = "ACC-4394_2 " + currentTimeString;
            var provider3Name = "ACC-4394_3 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider3ID = commonMethodsDB.CreateProvider(provider3Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractService", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider1ID);

            var contractScheme2Name = "CPCS_B_" + currentTimeString;
            var contractScheme2Code = contractScheme1Code + 1;
            var careProviderContractScheme2Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme2Name, new DateTime(2020, 1, 1), contractScheme2Code, provider1ID, provider2ID);

            var contractScheme3Name = "CPCS_C_" + currentTimeString;
            var contractScheme3Code = contractScheme2Code + 1;
            var careProviderContractScheme3Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme3Name, new DateTime(2020, 1, 1), contractScheme3Code, provider1ID, provider3ID);


            #endregion

            #region Care Provider Service

            var defaultCareProviderServiceId = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "Default Care Provider Service", new DateTime(2020, 1, 1), 9999999);
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-879", new DateTime(2020, 1, 1), 9999900, null, false);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_careProviders_TeamId, "CPSD ACC-879 " + currentTimeString, Code1, Code1, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var careProviderContractService1Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider1ID, careProviderContractScheme1Id, careProviderService1Id, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService2Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme2Id, careProviderService1Id, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);

            #endregion


            #region Step 16

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();

            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .ClickNewRecordButton();

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickContractSchemeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(contractScheme3Name, careProviderContractScheme3Id);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickNetIncome_YesRadioButton()
                .ClickIncomeContractServiceLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("", careProviderContractService1Id);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad();

            #endregion

            #region Step 17

            contractServiceRecordPage
                .ValidateTopErrorLabelVisibility(false)
                .ClickSaveAndCloseButton()
                .ValidateTopErrorLabelVisibility(true)
                .ValidateTopErrorLabelText("Some data is not correct. Please review the data in the Form.");

            #endregion

            #region step 18

            contractServiceRecordPage
                .ClickServiceLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("CPS ACC-879", careProviderService1Id);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickServiceDetailLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("CPSD ACC-879 " + currentTimeString, careProviderServiceDetailId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickRateUnitLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default Care Provider Rate Unit", careProviderRateUnitId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickVATCodeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Standard Rated", careProviderVATCodeId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .SelectAdjustedDays("Charge End Date")
                .ClickBatchGroupingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default Care Provider Batch Grouping", careProviderBatchGroupingId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickSaveAndCloseButton();

            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .ClickSearchButton()
                .WaitForContractServicesPageToLoad();

            var allServiceRecords = dbHelper.careProviderContractService.GetByEstablishmentId(provider1ID);
            Assert.AreEqual(3, allServiceRecords.Count());
            var careProviderContractService3Id = allServiceRecords.Where(c => c != careProviderContractService1Id && c != careProviderContractService2Id).First();

            #endregion

            #region Step 19

            contractServicesPage
                .OpenRecord(careProviderContractService3Id);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ValidateEstablishmentFieldDisabled(true)
                .ValidateContractSchemeFieldDisabled(true)
                .ValidateFunderFieldDisabled(true)
                .ValidateServiceFieldDisabled(true)
                .ValidateServiceDetailFieldDisabled(true)
                .ValidateNegotiatedRatesApplyFieldDisabled(true)
                .ValidateRateUnitFieldDisabled(true)
                .ValidateUsedInFinanceInvoiceBatchFieldDisabled(true)
                .ValidateContractTypeFieldDisabled(true)
                .ValidateUsedInFinanceFieldDisabled(true)
                .ValidateNetIncomeFieldDisabled(false) //only disabled if set to No
                ;

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4395

        [TestProperty("JiraIssueID", "ACC-4571")]
        [Description("Step(s) 21 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void Provider_ContractServices_UITestCases007()
        {
            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4395_1 " + currentTimeString;
            var provider2Name = "ACC-4395_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractService", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider2ID);

            #endregion

            #region Care Provider Service

            var defaultCareProviderServiceId = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "Default Care Provider Service", new DateTime(2020, 1, 1), 9999999);
            var isScheduledService = false;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-879", new DateTime(2020, 1, 1), 9999900, null, isScheduledService);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_careProviders_TeamId, "CPSD ACC-879", null, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var careProviderContractService1Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);

            #endregion


            #region Step 21

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();

            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .OpenRecord(careProviderContractService1Id);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ValidatePageHeaderText("Contract Service:\r\n" + provider1Name + " \\ " + provider2Name + " \\ " + contractScheme1Name + " \\ CPS ACC-879 \\ CPSD ACC-879")
                .ValidateResponsibleTeamFieldDisabled(true)
                .ValidateEstablishmentFieldDisabled(true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4572")]
        [Description("Step(s) 22 to 24 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void Provider_ContractServices_UITestCases008()
        {
            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4395_1 " + currentTimeString;
            var provider2Name = "ACC-4395_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractService", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider2ID);

            #endregion

            #region Care Provider Service

            var defaultCareProviderServiceId = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "Default Care Provider Service", new DateTime(2020, 1, 1), 9999999);
            var isScheduledService = false;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-879", new DateTime(2020, 1, 1), 9999900, null, isScheduledService);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_careProviders_TeamId, "CPSD ACC-879", null, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var careProviderContractService1Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);

            #endregion


            #region Step 22

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();

            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .ClickNewRecordButton();

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickContractSchemeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(contractScheme1Name, careProviderContractScheme1Id);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickServiceLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("CPS ACC-879", careProviderService1Id);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickServiceDetailLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("CPSD ACC-879", careProviderServiceDetailId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickRateUnitLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default Care Provider Rate Unit", careProviderRateUnitId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickVATCodeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Standard Rated", careProviderVATCodeId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickBatchGroupingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default Care Provider Batch Grouping", careProviderBatchGroupingId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickVATCodeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Standard Rated", careProviderVATCodeId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .SelectAdjustedDays("Charge End Date")
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("An active record already exists with the same combination of: Contract Scheme; Service; Service Detail; Booking Type; Booking Type Grouping. Please correct as necessary.")
                .TapCloseButton();

            #endregion

            #region Step 23

            dbHelper.careProviderContractService.UpdateInactive(careProviderContractService1Id, true);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("An inactive record already exists with the same combination of: Contract Scheme; Service; Service Detail; Booking Type; Booking Type Grouping. Please update values as necessary or cancel and activate the existing record manually.")
                .TapCloseButton();

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad();

            #endregion

            #region Step 24

            contractServiceRecordPage
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .OpenRecord(careProviderContractService1Id);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickDeleteRecordButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .ClickSearchButton()
                .WaitForContractServicesPageToLoad();

            var totalContractServices = dbHelper.careProviderContractService.GetByEstablishmentId(provider1ID).Count();
            Assert.AreEqual(0, totalContractServices);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4573")]
        [Description("Step(s) 25 to 26 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void Provider_ContractServices_UITestCases009()
        {
            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4395_1 " + currentTimeString;
            var provider2Name = "ACC-4395_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractService", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider2ID);

            #endregion

            #region Care Provider Service

            var defaultCareProviderServiceId = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "Default Care Provider Service", new DateTime(2020, 1, 1), 9999999);
            var isScheduledService = false;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-879", new DateTime(2020, 1, 1), 9999900, null, isScheduledService);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_careProviders_TeamId, "CPSD ACC-879", null, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var careProviderContractService1Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Step 25

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();

            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .OpenRecord(careProviderContractService1Id);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickInactive_YesRadioButton()
                .ClickSaveAndCloseButton();

            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .OpenRecord(careProviderContractService1Id);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ValidateInactive_YesRadioButtonChecked()
                .ClickActivateButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to activate this record? To continue, click ok.").TapOKButton();

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ValidateInactive_NoRadioButtonChecked();

            #endregion

            #region Step 26

            contractServiceRecordPage
                .NavigateToAuditPage();

            auditListPage
                .WaitForAuditListPageToLoad("careprovidercontractservice")

                .ValidateCellText(1, 2, "Updated")
                .ValidateCellText(1, 3, "ProviderType User_1")

                .ValidateCellText(2, 2, "Updated")
                .ValidateCellText(2, 3, "ProviderType User_1")

                .ValidateCellText(3, 2, "Created")
                .ValidateCellText(3, 3, appConfigSystemUserFullName);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4574")]
        [Description("Step(s) 27 to 29 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void Provider_ContractServices_UITestCases010()
        {
            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4395_1 " + currentTimeString;
            var provider2Name = "ACC-4395_2 " + currentTimeString;
            var provider3Name = "ACC-4395_3 " + currentTimeString;
            var provider4Name = "ACC-4395_4 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider3ID = commonMethodsDB.CreateProvider(provider3Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider4ID = commonMethodsDB.CreateProvider(provider4Name, _careProviders_TeamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractService", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider1ID);

            var contractScheme2Name = "CPCS_B_" + currentTimeString;
            var contractScheme2Code = contractScheme1Code + 1;
            var careProviderContractScheme2Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme2Name, new DateTime(2020, 1, 1), contractScheme2Code, provider1ID, provider2ID);

            var contractScheme3Name = "CPCS_C_" + currentTimeString;
            var contractScheme3Code = contractScheme2Code + 2;
            var careProviderContractScheme3Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme3Name, new DateTime(2020, 1, 1), contractScheme3Code, provider1ID, provider3ID);

            var contractScheme4Name = "CPCS_D_" + currentTimeString;
            var contractScheme4Code = contractScheme2Code + 3;
            var careProviderContractScheme4Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme4Name, new DateTime(2020, 1, 1), contractScheme4Code, provider1ID, provider4ID);


            #endregion

            #region Care Provider Service

            var defaultCareProviderServiceId = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "Default Care Provider Service", new DateTime(2020, 1, 1), 9999999);
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-879", new DateTime(2020, 1, 1), 9999900, null, false);

            #endregion

            #region Care Provider Service Detail
            dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var Code = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail();
            int newCode = Code.Count();

            while (dbHelper.careProviderServiceDetail.GetByCode(newCode).Any())
            {
                newCode = newCode + 1;
            }

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_careProviders_TeamId, "CPSD ACC-879" + currentTimeString, newCode, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var careProviderContractService1Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider1ID, careProviderContractScheme1Id, careProviderService1Id, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService2Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme2Id, careProviderService1Id, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService3Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider3ID, careProviderContractScheme3Id, careProviderService1Id, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService4Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider4ID, careProviderContractScheme4Id, careProviderService1Id, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);

            dbHelper.careProviderContractService.UpdateInactive(careProviderContractService2Id, true);

            #endregion

            #region Step 27

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickContractServicesButton();

            contractServicesPage
                .WaitForContractServicesPageToLoadFromFinanceAdminArea()
                .SelectView("Active Records")
                .WaitForContractServicesPageToLoadFromFinanceAdminArea()
                .ClickHeaderCellLink(15) //sort ascending (Modified On)
                .WaitForContractServicesPageToLoadFromFinanceAdminArea()
                .ClickHeaderCellLink(15) //sort descending (Modified On)
                .WaitForContractServicesPageToLoadFromFinanceAdminArea()
                .ValidateRecordPresent(careProviderContractService1Id, true)
                .ValidateRecordPresent(careProviderContractService2Id, false)
                .ValidateRecordPresent(careProviderContractService3Id, true)
                .ValidateRecordPresent(careProviderContractService4Id, true);

            contractServicesPage
                .SelectView("Inactive Records")
                .WaitForContractServicesPageToLoadFromFinanceAdminArea()
                .ClickHeaderCellLink(15) //sort ascending (Modified On)
                .WaitForContractServicesPageToLoadFromFinanceAdminArea()
                .ClickHeaderCellLink(15) //sort descending (Modified On)
                .WaitForContractServicesPageToLoadFromFinanceAdminArea()
                .ValidateRecordPresent(careProviderContractService1Id, false)
                .ValidateRecordPresent(careProviderContractService2Id, true)
                .ValidateRecordPresent(careProviderContractService3Id, false)
                .ValidateRecordPresent(careProviderContractService4Id, false);

            contractServicesPage
                .SelectView("Private Care (Active)")
                .WaitForContractServicesPageToLoadFromFinanceAdminArea()
                .ClickHeaderCellLink(15) //sort ascending (Modified On)
                .WaitForContractServicesPageToLoadFromFinanceAdminArea()
                .ClickHeaderCellLink(15) //sort descending (Modified On)
                .WaitForContractServicesPageToLoadFromFinanceAdminArea()
                .ValidateRecordPresent(careProviderContractService1Id, true)
                .ValidateRecordPresent(careProviderContractService2Id, false)
                .ValidateRecordPresent(careProviderContractService3Id, true)
                .ValidateRecordPresent(careProviderContractService4Id, false);

            contractServicesPage
                .SelectView("Funded Care (Active)")
                .WaitForContractServicesPageToLoadFromFinanceAdminArea()
                .ClickHeaderCellLink(15) //sort ascending (Modified On)
                .WaitForContractServicesPageToLoadFromFinanceAdminArea()
                .ClickHeaderCellLink(15) //sort descending (Modified On)
                .WaitForContractServicesPageToLoadFromFinanceAdminArea()
                .ValidateRecordPresent(careProviderContractService1Id, false)
                .ValidateRecordPresent(careProviderContractService2Id, false)
                .ValidateRecordPresent(careProviderContractService3Id, false)
                .ValidateRecordPresent(careProviderContractService4Id, true);

            #endregion

            #region Step 28

            contractServicesPage
                .WaitForContractServicesPageToLoadFromFinanceAdminArea()
                .ValidateHeaderCellText(2, "Establishment")
                .ValidateHeaderCellText(3, "Contract Scheme")
                .ValidateHeaderCellText(4, "Funder")
                .ValidateHeaderCellText(5, "Service")
                .ValidateHeaderCellText(6, "Service Detail")
                .ValidateHeaderCellText(7, "Booking Type")
                .ValidateHeaderCellText(8, "Booking Type Grouping")
                .ValidateHeaderCellText(9, "Rate Unit")
                .ValidateHeaderCellText(10, "Batch Grouping")
                .ValidateHeaderCellText(11, "VAT Code")
                .ValidateHeaderCellText(12, "Used In Finance Invoice Batch Setup?")
                .ValidateHeaderCellText(13, "Net Income?")
                .ValidateHeaderCellText(14, "Contract Type")
                .ValidateHeaderCellText(15, "Modified On")
                .ValidateHeaderCellText(16, "Modified By");

            #endregion

            #region Step 29

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();

            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .ValidateHeaderCellText(2, "Establishment")
                .ValidateHeaderCellText(3, "Contract Scheme")
                .ValidateHeaderCellText(4, "Funder")
                .ValidateHeaderCellText(5, "Service")
                .ValidateHeaderCellText(6, "Service Detail")
                .ValidateHeaderCellText(7, "Booking Type")
                .ValidateHeaderCellText(8, "Booking Type Grouping")
                .ValidateHeaderCellText(9, "Rate Unit")
                .ValidateHeaderCellText(10, "Batch Grouping")
                .ValidateHeaderCellText(11, "VAT Code")
                .ValidateHeaderCellText(12, "Used In Finance Invoice Batch Setup?")
                .ValidateHeaderCellText(13, "Net Income?")
                .ValidateHeaderCellText(14, "Contract Type")
                .ValidateHeaderCellText(15, "Modified On")
                .ValidateHeaderCellText(16, "Modified By");

            #endregion

            #region Step 30

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickContractServicesButton();

            contractServicesPage
                .WaitForContractServicesPageToLoadFromFinanceAdminArea()
                .ClickEstablishmentLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery(provider1Name).TapSearchButton().SelectResultElement(provider1ID);

            contractServicesPage
                .WaitForContractServicesPageToLoadFromFinanceAdminArea()
                .ClickSearchButton()
                .WaitForContractServicesPageToLoadFromFinanceAdminArea()
                .ValidateRecordPresent(careProviderContractService1Id, true)
                .ValidateRecordPresent(careProviderContractService2Id, true)
                .ValidateRecordPresent(careProviderContractService3Id, true)
                .ValidateRecordPresent(careProviderContractService4Id, true);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4575")]
        [Description("Step(s) 30 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void Provider_ContractServices_UITestCases011()
        {
            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4395_1 " + currentTimeString;
            var provider2Name = "ACC-4395_2 " + currentTimeString;
            var provider3Name = "ACC-4395_3 " + currentTimeString;
            var provider4Name = "ACC-4395_4 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider3ID = commonMethodsDB.CreateProvider(provider3Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider4ID = commonMethodsDB.CreateProvider(provider4Name, _careProviders_TeamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractService", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            //var contractScheme1Name = "CPCS_A_" + currentTimeString;
            //var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            //var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider1ID);

            var contractScheme2Name = "CPCS_B_" + currentTimeString;
            var contractScheme2Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme2Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme2Name, new DateTime(2020, 1, 1), contractScheme2Code, provider1ID, provider2ID);

            var contractScheme3Name = "CPCS_C_" + currentTimeString;
            var contractScheme3Code = contractScheme2Code + 1;
            var careProviderContractScheme3Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme3Name, new DateTime(2020, 1, 1), contractScheme3Code, provider1ID, provider3ID);

            var contractScheme4Name = "CPCS_D_" + currentTimeString;
            var contractScheme4Code = contractScheme3Code + 1;
            var careProviderContractScheme4Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme4Name, new DateTime(2020, 1, 1), contractScheme4Code, provider1ID, provider4ID);


            #endregion

            #region Care Provider Service

            var defaultCareProviderServiceId = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "Default Care Provider Service", new DateTime(2020, 1, 1), 9999999);
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-879", new DateTime(2020, 1, 1), 9999900, null, false);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_careProviders_TeamId, "CPSD ACC-879", null, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            //var careProviderContractService1Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider1ID, careProviderContractScheme1Id, careProviderService1Id, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService2Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme2Id, careProviderService1Id, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService3Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider3ID, careProviderContractScheme3Id, careProviderService1Id, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);
            var careProviderContractService4Id = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider4ID, careProviderContractScheme4Id, careProviderService1Id, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);

            dbHelper.careProviderContractService.UpdateInactive(careProviderContractService2Id, true);

            #endregion

            #region Step 30

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickContractServicesButton();

            contractServicesPage
                .WaitForContractServicesPageToLoadFromFinanceAdminArea()
                .ClickContractSchemeLookupButton();//contract scheme

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery(contractScheme2Name).TapSearchButton().AddElementToList(careProviderContractScheme2Id).TapOKButton();

            contractServicesPage
                .WaitForContractServicesPageToLoadFromFinanceAdminArea()
                .ClickSearchButton()
                .WaitForContractServicesPageToLoadFromFinanceAdminArea()
                .ValidateRecordPresent(careProviderContractService2Id, true)
                .ValidateRecordPresent(careProviderContractService3Id, false)
                .ValidateRecordPresent(careProviderContractService4Id, false);

            contractServicesPage
                .ClickClearFiltersButton()
                .ClickFunderLookupButton();//founder

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery(provider3Name).TapSearchButton().AddElementToList(provider3ID).TapOKButton();

            contractServicesPage
                .WaitForContractServicesPageToLoadFromFinanceAdminArea()
                .ClickSearchButton()
                .WaitForContractServicesPageToLoadFromFinanceAdminArea()
                .ValidateRecordPresent(careProviderContractService2Id, false)
                .ValidateRecordPresent(careProviderContractService3Id, true)
                .ValidateRecordPresent(careProviderContractService4Id, false);

            //not possible to search by service anymore
            //contractServicesPage
            //    .SearchRecord("CPS ACC-879")//service
            //    .WaitForContractServicesPageToLoadFromFinanceAdminArea()
            //    .ClickHeaderCellLink(14) //sort ascending (Modified On)
            //    .WaitForContractServicesPageToLoadFromFinanceAdminArea()
            //    .ClickHeaderCellLink(14) //sort descending (Modified On)
            //    .WaitForContractServicesPageToLoadFromFinanceAdminArea()
            //    .ValidateRecordPresent(careProviderContractService2Id, true)
            //    .ValidateRecordPresent(careProviderContractService3Id, true)
            //    .ValidateRecordPresent(careProviderContractService4Id, true);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4576")]
        [Description("Step(s) 31 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void Provider_ContractServices_UITestCases012()
        {

            #region Step 31

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Contract Services")

                .SelectFilter("1", "Id")
                .SelectFilter("1", "Inactive")
                .SelectFilter("1", "Responsible Team")
                .SelectFilter("1", "Responsible User")
                .SelectFilter("1", "Establishment")
                .SelectFilter("1", "Funder")
                .SelectFilter("1", "Contract Scheme")
                .SelectFilter("1", "Service")
                .SelectFilter("1", "Service Detail")
                .SelectFilter("1", "Rooms Apply?")
                .SelectFilter("1", "Negotiated Rates Apply?")
                .SelectFilter("1", "Permit Rate Override?")
                .SelectFilter("1", "VAT Code")
                .SelectFilter("1", "Adjusted Days")
                .SelectFilter("1", "Used In Finance Invoice Batch Setup?")
                .SelectFilter("1", "Used In Finance?")
                .SelectFilter("1", "Batch Grouping")
                .SelectFilter("1", "Booking Type")
                .SelectFilter("1", "Charge Scheduled Care On Actuals?")
                .SelectOperator("1", "Equals")
                .SelectPicklistRuleValue("1", "Yes")
                .ClickSearchButton();

            System.Threading.Thread.Sleep(3000);

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ClickNewRecordButton_ResultsPage();

            contractServiceRecordPage
                .WaitForContractServicesPageToLoadFromAdvancedSearch();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4577")]
        [Description("Step(s) 33 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void Provider_ContractServices_UITestCases013()
        {
            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4395_1 " + currentTimeString;
            var provider2Name = "ACC-4395_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractService", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme2Name = "CPCS_B_" + currentTimeString;
            var contractScheme2Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme2Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme2Name, new DateTime(2020, 1, 1), contractScheme2Code, provider1ID, provider2ID);

            #endregion

            #region Care Provider Service

            var defaultCareProviderServiceId = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "Default Care Provider Service", new DateTime(2020, 1, 1), 9999999);
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-879", new DateTime(2020, 1, 1), 9999900, null, false);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_careProviders_TeamId, "CPSD ACC-879", null, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme2Id, careProviderService1Id, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Step 33

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();

            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ValidateBookingTypeLookupButtonVisible(false);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4578")]
        [Description("Step(s) 34 to 35 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void Provider_ContractServices_UITestCases014()
        {
            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4395_1 " + currentTimeString;
            var provider2Name = "ACC-4395_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, true, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 10, true, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractService", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider2ID);

            #endregion

            #region Care Provider Service

            var defaultCareProviderServiceId = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "Default Care Provider Service", new DateTime(2020, 1, 1), 9999999);
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-4395 B", new DateTime(2020, 1, 1), 9999500, null, true);

            #endregion

            #region booking Type

            var bookingTypeClassId = 2; //Booking (to internal care activity)
            var workingContractedTime = 1; //Count full booking length
            var cpbookingchargetypeid = 1; //Per Booking
            var bookingTypeId = commonMethodsDB.CreateBookingType("BT_ACC4395", bookingTypeClassId, 60, new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0), workingContractedTime, false, null, null, null, cpbookingchargetypeid);

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, null, bookingTypeId, null, "");

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_careProviders_TeamId, provider1ID, bookingTypeId, true);

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);
            dbHelper.careProviderRateUnit.UpdateTimeAndDays(careProviderRateUnitId, true);

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme1Id, careProviderService1Id, null, bookingTypeId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Step 34

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();

            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .ClickNewRecordButton();

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickContractSchemeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(contractScheme1Name, careProviderContractScheme1Id);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickServiceLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("CPS ACC-4395 B", careProviderService1Id);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickBookingTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("BT_ACC4395", bookingTypeId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickRateUnitLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default Care Provider Rate Unit", careProviderRateUnitId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickVATCodeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Standard Rated", careProviderVATCodeId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickBatchGroupingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default Care Provider Batch Grouping", careProviderBatchGroupingId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("An active record already exists with the same combination of: Contract Scheme; Service; Service Detail; Booking Type; Booking Type Grouping. Please correct as necessary.")
                .TapCloseButton();

            #endregion

            #region Step 35

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ValidateChargeScheduledCareOnActualsDisabled(true);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4579")]
        [Description("Step(s) 36 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void Provider_ContractServices_UITestCases015()
        {
            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4395_1 " + currentTimeString;
            var provider2Name = "ACC-4395_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractService", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider2ID);

            #endregion

            #region Care Provider Service

            var defaultCareProviderServiceId = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "Default Care Provider Service", new DateTime(2020, 1, 1), 9999999);
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-4395 B1", new DateTime(2020, 1, 1), 9999300, null, true);

            #endregion

            #region booking Type

            var bookingTypeClassId = 5; //Booking (To Service User)
            var workingContractedTime = 1; //Count full booking length
            var cpbookingchargetypeid = 1; //Per Booking
            var bookingTypeId = commonMethodsDB.CreateBookingType("BT_ACC4395_B1", bookingTypeClassId, 60, new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0), workingContractedTime, false, null, null, null, cpbookingchargetypeid);

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, null, bookingTypeId, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme1Id, careProviderService1Id, null, bookingTypeId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Step 36

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();


            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ValidateChargeScheduledCareOnActualsDisabled(false);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4570

        [TestProperty("JiraIssueID", "ACC-4580")]
        [Description("Step(s) 37 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void Provider_ContractServices_UITestCases016()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region booking Type

            var bookingTypeClassId = 5; //Booking (To Service User)
            var workingContractedTime = 1; //Count full booking length
            var cpbookingchargetypeid = 1; //Per Booking
            var bookingTypeId = commonMethodsDB.CreateBookingType("BT_ACC4570_B1", bookingTypeClassId, 60, new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0), workingContractedTime, false, null, null, null, cpbookingchargetypeid);

            #endregion

            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4570_1 " + currentTimeString;
            var provider2Name = "ACC-4570_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, true, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Provider to Booking Type Link

            dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_careProviders_TeamId, provider1ID, bookingTypeId, true);

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "PCSU_" + currentTimeString;
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Provider Contract", "Service User " + currentTimeString, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);

            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, thisWeekMonday);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Full Time Employee Contract", "2", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContract = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2023, 1, 1), _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid, 40, new List<Guid> { bookingTypeId });

            #endregion

            #region Recurrence Patterns

            var _recurrencePattern_Every1WeekMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on monday").First();
            var _recurrencePattern_Every1WeekTuesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on tuesday").First();
            var _recurrencePattern_Every1WeekWednesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on wednesday").First();
            var _recurrencePattern_Every1WeekThursdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on thursday").First();
            var _recurrencePattern_Every1WeekFridayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on friday").First();
            var _recurrencePattern_Every1WeekSaturdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on saturday").First();
            var _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();

            #endregion

            #region Availability Type

            var _availabilityTypes_StandardId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Standard").First();

            #endregion

            #region User Work Schedule

            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careProviders_TeamId, _recurrencePattern_Every1WeekMondayId, _systemUserEmploymentContract, _availabilityTypes_StandardId, thisWeekMonday, null, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careProviders_TeamId, _recurrencePattern_Every1WeekTuesdayId, _systemUserEmploymentContract, _availabilityTypes_StandardId, thisWeekMonday.AddDays(1), null, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careProviders_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContract, _availabilityTypes_StandardId, thisWeekMonday.AddDays(2), null, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careProviders_TeamId, _recurrencePattern_Every1WeekThursdayId, _systemUserEmploymentContract, _availabilityTypes_StandardId, thisWeekMonday.AddDays(3), null, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careProviders_TeamId, _recurrencePattern_Every1WeekFridayId, _systemUserEmploymentContract, _availabilityTypes_StandardId, thisWeekMonday.AddDays(4), null, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careProviders_TeamId, _recurrencePattern_Every1WeekSaturdayId, _systemUserEmploymentContract, _availabilityTypes_StandardId, thisWeekMonday.AddDays(5), null, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careProviders_TeamId, _recurrencePattern_Every1WeekSundayId, _systemUserEmploymentContract, _availabilityTypes_StandardId, thisWeekMonday.AddDays(6), null, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider2ID);

            #endregion

            #region Care Provider Service

            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-4570", new DateTime(2020, 1, 1), 9999200, null, false);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_careProviders_TeamId, "CPSD ACC-4570", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_careProviders_TeamId, "CPEN ACC-4570", 9800, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 1; //Monday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = todayDate.AddYears(1);
            var separateinvoices = false;

            dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2023, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _careProviders_TeamId);

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), careProviderRateUnitId, 10, _careProviders_TeamId);

            #endregion

            #region Care Provider Diary Booking

            var bookingDiaryId = dbHelper.cPBookingDiary.CreateCPBookingDiary(_careProviders_TeamId, _careProviders_BusinessUnitId, "", bookingTypeId, provider1ID, todayDate, new TimeSpan(9, 0, 0), todayDate, new TimeSpan(9, 15, 0), "staff", 15, 0, "people", null);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_careProviders_TeamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var _personID = dbHelper.person.CreatePersonRecord("", "Peter", "", currentTimeString, "", new DateTime(2000, 1, 1), _ethnicityId, _careProviders_TeamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_careProviders_TeamId, "title", _personID, _systemUserId, provider1ID, careProviderContractScheme1Id, provider2ID, todayDate.AddDays(-5));

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _careProviders_TeamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, careProviderRateUnitId);

            #endregion

            #region Booking Diary Staff

            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_careProviders_TeamId, "test", bookingDiaryId, _systemUserEmploymentContract, _systemUserId);

            #endregion

            #region Diary Booking To Person

            dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_careProviders_TeamId, bookingDiaryId, _personID, _personcontractId, careProviderContractServiceId);

            #endregion

            #region Schedule Job

            //Get the schedule job id
            var shcduleJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Process CP Finance Transaction Triggers").FirstOrDefault();

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //trigger the schedule job
            this.WebAPIHelper.ScheduleJob.Execute(shcduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //wait for the schedule job Idle state
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(shcduleJobId);

            #endregion

            #region Step 37

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();


            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ValidateUsedInFinance_YesRadioButtonChecked();

            #endregion
        }


        [TestProperty("JiraIssueID", "ACC-4624")]
        [Description("Step(s) 39 from the original test - Check 'Permit Rate Override?' field set to No and disabled")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void Provider_ContractServices_UITestCases017()
        {
            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4570_1 " + currentTimeString;
            var provider2Name = "ACC-4570_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractService", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider2ID);

            #endregion

            #region Care Provider Service

            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-4570", new DateTime(2020, 1, 1), 9999200, null, false);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_careProviders_TeamId, "CPSD ACC-4570", 990, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var contractservicetypeid = 2; //Block
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, contractservicetypeid, 1, false, false);

            #endregion

            #region Step 39

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();


            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ValidatePermitRateOverrideFieldDisabled(true)
                .ValidatePermitRateOverride_NoRadioButtonChecked();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4625")]
        [Description("Step(s) 39 from the original test - Check 'Permit Rate Override?' field set to No and enabled")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Services")]
        public void Provider_ContractServices_UITestCases018()
        {
            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4570_1 " + currentTimeString;
            var provider2Name = "ACC-4570_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractService", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider2ID);

            #endregion

            #region Care Provider Service

            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-4570", new DateTime(2020, 1, 1), 9999200, null, false);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_careProviders_TeamId, "CPSD ACC-4570", 990, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var contractservicetypeid = 1; //Spot
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, contractservicetypeid, 1, false, false);

            #endregion

            #region Step 39

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();


            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ValidatePermitRateOverrideFieldDisabled(false)
                .ValidatePermitRateOverride_NoRadioButtonChecked();

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4586

        [TestProperty("JiraIssueID", "ACC-4699")]
        [Description("Step(s) 1 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Service Rate Periods")]
        public void Provider_ContractServiceRatePeriods_UITestCases001()
        {
            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4586_1 " + currentTimeString;
            var provider2Name = "ACC-4586_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceRatePeriodUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractServiceRatePeriod", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider2ID);

            #endregion

            #region Care Provider Service

            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-4586", new DateTime(2020, 1, 1), 9999100, null, false);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_careProviders_TeamId, "CPSD ACC-4586", 990, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var contractservicetypeid = 1; //Spot
            var isnegotiatedratescanapply = true;
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, contractservicetypeid, 1, false, isnegotiatedratescanapply);

            #endregion

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();


            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ValidateRatesTabVisible(false);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4700")]
        [Description("Step(s) 2 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Service Rate Periods")]
        public void Provider_ContractServiceRatePeriods_UITestCases002()
        {
            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4586_1 " + currentTimeString;
            var provider2Name = "ACC-4586_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceRatePeriodUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractServiceRatePeriod", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider2ID);

            #endregion

            #region Care Provider Service

            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-4586", new DateTime(2020, 1, 1), 9999100, null, false);

            #endregion

            #region Care Provider Service Detail

            var serviceDetailCode = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count();

            while (dbHelper.careProviderServiceDetail.GetByCode(serviceDetailCode).Any())
                serviceDetailCode = serviceDetailCode + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_careProviders_TeamId, "CPSD ACC-4586", serviceDetailCode, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var contractservicetypeid = 1; //Spot
            var isnegotiatedratescanapply = false;
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, contractservicetypeid, 1, false, isnegotiatedratescanapply);

            #endregion

            #region Step 2

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();


            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickRatesTab();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad()
                .ClickNewRecordButton();

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ValidateGeneralSectionFieldsVisible(false, true, false, false)
                .ValidateBlockContractDetailsSectionFieldsVisible(false)
                .ValidateNotesSectionFieldsVisible();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4701")]
        [Description("Step(s) 3 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Service Rate Periods")]
        public void Provider_ContractServiceRatePeriods_UITestCases003()
        {
            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4586_1 " + currentTimeString;
            var provider2Name = "ACC-4586_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceRatePeriodUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractServiceRatePeriod", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider2ID);

            #endregion

            #region Care Provider Service

            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-4586", new DateTime(2020, 1, 1), 9999100, null, false);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_careProviders_TeamId, "CPSD ACC-4586", 980, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "CPRU 4586 A", new DateTime(2020, 1, 1), 9999980, true);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var contractservicetypeid = 1; //Spot
            var isnegotiatedratescanapply = false;
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, contractservicetypeid, 1, false, isnegotiatedratescanapply);

            #endregion

            #region Step 3

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();


            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickRatesTab();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad()
                .ClickNewRecordButton();

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ValidateGeneralSectionFieldsVisible(true, true, true, false);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4702")]
        [Description("Step(s) 4 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Service Rate Periods")]
        public void Provider_ContractServiceRatePeriods_UITestCases004()
        {
            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4586_1 " + currentTimeString;
            var provider2Name = "ACC-4586_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceRatePeriodUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractServiceRatePeriod", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider2ID);

            #endregion

            #region Care Provider Service

            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-4586", new DateTime(2020, 1, 1), 9999100, null, false);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_careProviders_TeamId, "CPSD ACC-4586", 980, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "CPRU 4586 B", new DateTime(2020, 1, 1), 9999970, true, true);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var contractservicetypeid = 1; //Spot
            var isnegotiatedratescanapply = false;
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, contractservicetypeid, 1, false, isnegotiatedratescanapply);

            #endregion

            #region Step 4

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();


            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickRatesTab();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad()
                .ClickNewRecordButton();

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ValidateGeneralSectionFieldsVisible(true, false, true, true);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4703")]
        [Description("Step(s) 5 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Service Rate Periods")]
        public void Provider_ContractServiceRatePeriods_UITestCases005()
        {
            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4586_1 " + currentTimeString;
            var provider2Name = "ACC-4586_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceRatePeriodUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractServiceRatePeriod", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider2ID);

            #endregion

            #region Care Provider Service

            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-4586", new DateTime(2020, 1, 1), 9999100, null, false);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_careProviders_TeamId, "CPSD ACC-4586", 990, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var contractservicetypeid = 2; //Block
            var isnegotiatedratescanapply = false;
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, contractservicetypeid, 1, false, isnegotiatedratescanapply);

            #endregion

            #region Step 5

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();


            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickRatesTab();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad()
                .ClickNewRecordButton();

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ValidateGeneralSectionFieldsVisible(false, true, false, false)
                .ValidateBlockContractDetailsSectionFieldsVisible(true)
                .ValidateNotesSectionFieldsVisible();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4704")]
        [Description("Step(s) 6 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Service Rate Periods")]
        public void Provider_ContractServiceRatePeriods_UITestCases006()
        {
            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4586_1 " + currentTimeString;
            var provider2Name = "ACC-4586_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceRatePeriodUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractServiceRatePeriod", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider2ID);

            #endregion

            #region Care Provider Service

            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-4586", new DateTime(2020, 1, 1), 9999100, null, false);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_careProviders_TeamId, "CPSD ACC-4586", 990, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var contractservicetypeid = 2; //Block
            var isnegotiatedratescanapply = false;
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, contractservicetypeid, 1, false, isnegotiatedratescanapply);

            #endregion

            #region Step 6

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();


            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickRatesTab();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad()
                .ClickNewRecordButton();

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ValidatePersonContractLookupButtonVisibility(false);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4705")]
        [Description("Step(s) 7 to 10 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Service Rate Periods")]
        public void Provider_ContractServiceRatePeriods_UITestCases007()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4586_1 " + currentTimeString;
            var provider2Name = "ACC-4586_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceRatePeriodUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractServiceRatePeriod", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider2ID);

            #endregion

            #region Care Provider Service

            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-4586", new DateTime(2020, 1, 1), 9999100, null, false);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_careProviders_TeamId, "CPSD ACC-4586", 990, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var contractservicetypeid = 2; //Block
            var isnegotiatedratescanapply = false;
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, contractservicetypeid, 1, false, isnegotiatedratescanapply);
            var careProviderContractServiceTitle = dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"].ToString();

            #endregion

            #region Step 7

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();


            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickRatesTab();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad()
                .ClickNewRecordButton();

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ValidateResponsibleTeamLinkText(teamName)
                .ValidateContractServiceLinkText(careProviderContractServiceTitle)
                .ValidateRateUnitLinkText("Default Care Provider Rate Unit")
                .ValidateCapacityCanBeExceededSelectedText("No");

            #endregion

            #region Step 8

            //already automated in Step 4 (test Provider_ContractServiceRatePeriods_UITestCases004) 

            #endregion

            #region Step 9

            contractServiceRatePeriodRecordPage
                .ValidateRateUnitFieldDisabled(true);

            #endregion

            #region Step 10

            contractServiceRatePeriodRecordPage
                .InsertTextOnStartDate(todayDate.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnEndDate(todayDate.AddDays(-2).ToString("dd'/'MM'/'yyyy"));

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("End Date must be equal or after Start Date")
                .TapCloseButton();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4706")]
        [Description("Step(s) 11 to 13 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Service Rate Periods")]
        public void Provider_ContractServiceRatePeriods_UITestCases008()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4586_1 " + currentTimeString;
            var provider2Name = "ACC-4586_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceRatePeriodUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractServiceRatePeriod", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider2ID);

            #endregion

            #region Care Provider Service

            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-4586", new DateTime(2020, 1, 1), 9999100, null, false);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_careProviders_TeamId, "CPSD ACC-4586", 990, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var contractservicetypeid = 2; //Block
            var isnegotiatedratescanapply = false;
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, contractservicetypeid, 1, false, isnegotiatedratescanapply);
            var careProviderContractServiceTitle = dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"].ToString();

            #endregion

            #region Step 11

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();


            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickRatesTab();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad()
                .ClickNewRecordButton();

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ClickSaveAndCloseButton()
                .ValidateNotificationAreaVisible(true)
                .ValidateNotificationAreaText("Some data is not correct. Please review the data in the Form.");

            #endregion

            #region Step 12

            contractServiceRatePeriodRecordPage
                .InsertTextOnStartDate(todayDate.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnRate("10")
                .InsertTextOnRatePerUnitBlock("15")
                .ClickFinanceCodeEditButton();

            financeCodeUpdaterPopup
                .WaitForPopupToLoad()
                .InsertTextInPosition1("1")
                .InsertTextInPosition2("A")
                .TapSaveButton();

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .InsertTextOnCapacity("20")
                .InsertTextOnNoteText("Testing ACC-896")
                .ClickSaveAndCloseButton();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad()
                .ClickRefreshButton();

            var contractServiceRatePeriods = dbHelper.careProviderContractServiceRatePeriod.GetByContractServiceID(careProviderContractServiceId);
            Assert.AreEqual(1, contractServiceRatePeriods.Count);
            var contractServiceRatePeriodId = contractServiceRatePeriods[0];

            contractServiceRatePeriodsPage
                .OpenRecord(contractServiceRatePeriodId);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ValidateStartDateText(todayDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRateUnitLinkText("Default Care Provider Rate Unit")
                .ValidateRateText("10.00")
                .ValidateResponsibleTeamLinkText(teamName)
                .ValidateEndDateText("")
                .ValidateRatePerUnitBlockText("15.00")
                .ValidateFinanceCodeText("1A")
                .ValidateCapacityText("20")
                .ValidateCapacityCanBeExceededSelectedText("No")
                .ValidateNoteTextText("Testing ACC-896");

            #endregion

            #region Step 13

            var expectedTitle = careProviderContractServiceTitle + " \\ Default Care Provider Rate Unit \\ " + todayDate.ToString("dd'/'MM'/'yyyy") + " \\";

            contractServiceRatePeriodRecordPage
                .ValidateTitleText(expectedTitle);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4707")]
        [Description("Step(s) 14 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Service Rate Periods")]
        public void Provider_ContractServiceRatePeriods_UITestCases009()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4586_1 " + currentTimeString;
            var provider2Name = "ACC-4586_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceRatePeriodUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractServiceRatePeriod", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider2ID);

            #endregion

            #region Care Provider Service

            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-4586", new DateTime(2020, 1, 1), 9999100, null, false);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_careProviders_TeamId, "CPSD ACC-4586", 980, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "CPRU 4586 B", new DateTime(2020, 1, 1), 9999970, true, true);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var contractservicetypeid = 1; //Spot
            var isnegotiatedratescanapply = false;
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, contractservicetypeid, 1, false, isnegotiatedratescanapply);

            #endregion

            #region Timeband Set

            var timebandSetId = commonMethodsDB.CreateTimebandSet("Default TS", _careProviders_TeamId);

            #endregion

            #region Timeband

            var startDay = 1; //Monday
            var endDay = 7; //Sunday
            var timebandId = commonMethodsDB.CreateTimeband(_careProviders_TeamId, timebandSetId, startDay, new TimeSpan(0, 0, 0), endDay, new TimeSpan(0, 0, 0));

            #endregion

            #region Bank Holiday Charging Calendar

            var bankHolidayChargingCalendarId = commonMethodsDB.CreateCPBankHolidayChargingCalendar(_careProviders_TeamId, "BHCC ACC-4586 A", "999000");

            #endregion

            #region Band Rate Type

            var bandRateTypeId = commonMethodsDB.CreateCareProviderBandRateType(_careProviders_TeamId, "BRT ACC-4586 A", "999000", new DateTime(2020, 1, 1));

            #endregion

            #region Band Rate Schedule

            commonMethodsDB.CreateCareProviderBandRateSchedule(_careProviders_TeamId, "Level 1", bandRateTypeId, new TimeSpan(0, 1, 0), new TimeSpan(23, 59, 0));

            #endregion

            #region Step 14

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();


            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickRatesTab();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad()
                .ClickNewRecordButton();

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .InsertTextOnStartDate(todayDate.ToString("dd'/'MM'/'yyyy"))
                .ClickTimebandSetLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default TS", timebandSetId);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ClickBankHolidayChargingCalendarLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("BHCC ACC-4586 A", bankHolidayChargingCalendarId);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ClickBandRateTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("BRT ACC-4586 A", bandRateTypeId);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ClickSaveAndCloseButton();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad()
                .ClickRefreshButton();

            var contractServiceRatePeriods = dbHelper.careProviderContractServiceRatePeriod.GetByContractServiceID(careProviderContractServiceId);
            Assert.AreEqual(1, contractServiceRatePeriods.Count);
            var contractServiceRatePeriod1Id = contractServiceRatePeriods[0];

            contractServiceRatePeriodsPage
                .ClickNewRecordButton();

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .InsertTextOnStartDate(todayDate.ToString("dd'/'MM'/'yyyy"))
                .ClickTimebandSetLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default TS", timebandSetId);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ClickBankHolidayChargingCalendarLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("BHCC ACC-4586 A", bankHolidayChargingCalendarId);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ClickBandRateTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("BRT ACC-4586 A", bandRateTypeId);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("There already exists record with the same combination of Contract Service which overlaps with this one. To save this record change Contract Service or Start / End Dates and/or Time Band Set and/or Job Role and/or Person Contract values to avoid overlapping.")
                .TapCloseButton();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4708")]
        [Description("Step(s) 15 to 16 from the original test")]
        // Due to new change in this story https://advancedcsg.atlassian.net/browse/ACC-4950 -> new column added and also re-ordering columns 
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Service Rate Periods")]
        public void Provider_ContractServiceRatePeriods_UITestCases010()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4586_1 " + currentTimeString;
            var provider2Name = "ACC-4586_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceRatePeriodUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractServiceRatePeriod", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider2ID);

            #endregion

            #region Care Provider Service

            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-4586 B", new DateTime(2020, 1, 1), 9998000, null, true);

            #endregion

            #region booking Type

            var bookingTypeClassId = 5; //Booking (To Service User)
            var workingContractedTime = 1; //Count full booking length
            var cpbookingchargetypeid = 1; //Per Booking
            var bookingTypeId = commonMethodsDB.CreateBookingType("BT_ACC4586", bookingTypeClassId, 60, new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0), workingContractedTime, false, null, null, null, cpbookingchargetypeid);

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, null, bookingTypeId, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var contractservicetypeid = 1; //Spot
            var isnegotiatedratescanapply = false;
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme1Id, careProviderService1Id, null, bookingTypeId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, contractservicetypeid, 1, false, isnegotiatedratescanapply);
            var careProviderContractServiceTitle = dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"].ToString();

            #endregion

            #region Care Provider Staff Role Type

            var staffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "CPSRT ACC4586", "999000", "", new DateTime(2020, 1, 1), "");

            #endregion

            #region Step 15

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();


            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickRatesTab();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad()
                .ClickNewRecordButton();

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .InsertTextOnStartDate(todayDate.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnRate("10")
                .ClickJobRoleLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("CPSRT ACC4586").TapSearchButton().SelectResultElement(staffRoleTypeId);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ClickSaveAndCloseButton();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad()
                .ClickRefreshButton();

            var contractServiceRatePeriods = dbHelper.careProviderContractServiceRatePeriod.GetByContractServiceID(careProviderContractServiceId);
            Assert.AreEqual(1, contractServiceRatePeriods.Count);
            var contractServiceRatePeriod1Id = contractServiceRatePeriods[0];

            contractServiceRatePeriodsPage
                .ClickNewRecordButton();

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .InsertTextOnStartDate(todayDate.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnRate("10")
                .ClickJobRoleLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("CPSRT ACC4586").TapSearchButton().SelectResultElement(staffRoleTypeId);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("There already exists record with the same combination of Contract Service which overlaps with this one. To save this record change Contract Service or Start / End Dates and/or Time Band Set and/or Job Role and/or Person Contract values to avoid overlapping.")
                .TapCloseButton();

            #endregion

            #region Step 16

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad()

                .ValidateHeaderCellText(2, "Start Date")
                .ValidateHeaderCellText(3, "End Date")
                .ValidateHeaderCellText(4, "Rate Unit")
                .ValidateHeaderCellText(5, "Rate")
                .ValidateHeaderCellText(6, "Bank Holiday Charging Calendar")
                .ValidateHeaderCellText(7, "Timeband Set")
                .ValidateHeaderCellText(8, "Band Rate Type")
                .ValidateHeaderCellText(9, "Job Roles")
                .ValidateHeaderCellText(10, "Person Contracts")
                .ValidateHeaderCellText(11, "Finance Code")
                .ValidateHeaderCellText(12, "Rate Per Unit (Block)")
                .ValidateHeaderCellText(13, "Capacity")
                .ValidateHeaderCellText(14, "Capacity Can Be Exceeded?")
                .ValidateHeaderCellText(15, "Modified On")
                .ValidateHeaderCellText(16, "Modified By");

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad()
                .ValidateRecordCellText(contractServiceRatePeriod1Id, 2, todayDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRecordCellText(contractServiceRatePeriod1Id, 3, "")
                .ValidateRecordCellText(contractServiceRatePeriod1Id, 4, "Default Care Provider Rate Unit")
                .ValidateRecordCellText(contractServiceRatePeriod1Id, 5, "10.00")
                .ValidateRecordCellText(contractServiceRatePeriod1Id, 6, "")
                .ValidateRecordCellText(contractServiceRatePeriod1Id, 7, "")
                .ValidateRecordCellText(contractServiceRatePeriod1Id, 8, "")
                .ValidateRecordCellText(contractServiceRatePeriod1Id, 9, "CPSRT ACC4586")
                .ValidateRecordCellText(contractServiceRatePeriod1Id, 10, "")
                .ValidateRecordCellText(contractServiceRatePeriod1Id, 11, "")
                .ValidateRecordCellText(contractServiceRatePeriod1Id, 12, "")
                .ValidateRecordCellText(contractServiceRatePeriod1Id, 13, "")
                .ValidateRecordCellText(contractServiceRatePeriod1Id, 14, "No");

            #endregion
        }


        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4804

        [TestProperty("JiraIssueID", "ACC-4839")]
        [Description("Step(s) 17 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Service Rate Periods")]
        public void Provider_ContractServiceRatePeriods_UITestCases011()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4804_1 " + currentTimeString;
            var provider2Name = "ACC-4804_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceRatePeriodUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractServiceRatePeriod", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider2ID);

            #endregion

            #region Care Provider Service

            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-4804 B", new DateTime(2020, 1, 1), 9997000, null, true);

            #endregion

            #region booking Type

            var bookingTypeClassId = 5; //Booking (To Service User)
            var workingContractedTime = 1; //Count full booking length
            var cpbookingchargetypeid = 1; //Per Booking
            var bookingTypeId = commonMethodsDB.CreateBookingType("BT_ACC4804", bookingTypeClassId, 60, new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0), workingContractedTime, false, null, null, null, cpbookingchargetypeid);

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, null, bookingTypeId, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var contractservicetypeid = 1; //Spot
            var isnegotiatedratescanapply = false;
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme1Id, careProviderService1Id, null, bookingTypeId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, contractservicetypeid, 1, false, isnegotiatedratescanapply);
            var careProviderContractServiceTitle = dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"].ToString();

            #endregion

            #region Care Provider Staff Role Type

            var staffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "CPSRT ACC4804", "999000", "", new DateTime(2020, 1, 1), "");

            #endregion

            #region Step 17

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Contract Service Rate Periods")
                .SelectFilter("1", "Contract Service")
                .SelectFilter("1", "Start Date")
                .SelectFilter("1", "Rate Unit")
                .SelectFilter("1", "Rate")
                .SelectFilter("1", "Responsible Team")
                .SelectFilter("1", "End Date")
                .SelectFilter("1", "Contract Service")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(contractScheme1Name, careProviderContractServiceId);

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton();

            System.Threading.Thread.Sleep(3000);

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ClickNewRecordButton_ResultsPage();

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoadFromAdvancedSearch()
                .ClickContractServiceLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(contractScheme1Name, careProviderContractServiceId);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoadFromAdvancedSearch()
                .InsertTextOnStartDate(todayDate.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnRate("10")
                .ClickJobRoleLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("CPSRT ACC4804").TapSearchButton().SelectResultElement(staffRoleTypeId);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoadFromAdvancedSearch()
                .ClickSaveAndCloseButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ClickBackButton_ResultsPage();

            var contractServiceRatePeriods = dbHelper.careProviderContractServiceRatePeriod.GetByContractServiceID(careProviderContractServiceId);
            Assert.AreEqual(1, contractServiceRatePeriods.Count);
            var contractServiceRatePeriod1Id = contractServiceRatePeriods[0];

            #endregion
        }

        //deactivating test because Bank Holiday Rate field was deactivated as part of the story https://advancedcsg.atlassian.net/browse/ACC-2997
        //[TestProperty("JiraIssueID", "ACC-4840")]
        //[Description("Step(s) 18 to 20 from the original test")]
        //[TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //[TestProperty("BusinessModule1", "Person Contracts")]
        //[TestProperty("Screen1", "Contract Service Rate Periods")]
        //public void Provider_ContractServiceRatePeriods_UITestCases012()
        //{
        //    var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

        //    #region Provider

        //    var addressType = 10; //Home

        //    var provider1Name = "ACC-4804_1 " + currentTimeString;
        //    var provider2Name = "ACC-4804_2 " + currentTimeString;

        //    var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
        //    var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

        //    #endregion

        //    #region Create SystemUser Record

        //    var _systemUserName = "ContractServiceRatePeriodUser1";
        //    var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractServiceRatePeriod", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
        //    var localZone = TimeZone.CurrentTimeZone;
        //    dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

        //    #endregion

        //    #region Care Provider Contract Scheme

        //    var contractScheme1Name = "CPCS_A_" + currentTimeString;
        //    var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
        //    var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider2ID);

        //    #endregion

        //    #region Care Provider Service

        //    var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-4804 A", new DateTime(2020, 1, 1), 9996000, null, false);

        //    #endregion

        //    #region Care Provider Service Detail

        //    var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_careProviders_TeamId, "CPSD ACC-4804 A", 1080, null, new DateTime(2020, 1, 1));

        //    #endregion

        //    #region Care Provider Service Mapping

        //    var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, careProviderServiceDetailId, null, null, "");

        //    #endregion

        //    #region Care Provider Rate Unit

        //    var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "CPRU 4804 A", new DateTime(2020, 1, 1), 9999900, true, false);

        //    #endregion

        //    #region VAT Code

        //    var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

        //    #endregion

        //    #region Care Provider Batch Grouping

        //    var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

        //    #endregion

        //    #region Care Provider Contract Service

        //    var contractservicetypeid = 1; //Spot
        //    var isnegotiatedratescanapply = false;
        //    var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, contractservicetypeid, 1, false, isnegotiatedratescanapply);

        //    #endregion

        //    #region Timeband Set

        //    var timebandSetId = commonMethodsDB.CreateTimebandSet("Default TS", _careProviders_TeamId);

        //    #endregion

        //    #region Bank Holiday Charging Calendar

        //    var bankHolidayChargingCalendar1Id = commonMethodsDB.CreateCPBankHolidayChargingCalendar(_careProviders_TeamId, "BHCC ACC-4804 A", "998000");
        //    var bankHolidayChargingCalendar2Id = commonMethodsDB.CreateCPBankHolidayChargingCalendar(_careProviders_TeamId, "BHCC ACC-4804 B", "997000");

        //    #endregion

        //    #region Public Holiday

        //    var bankHolidayDate = new DateTime(DateTime.Now.Year + 1, 12, 25);
        //    var bankHolidayName = "Christmas day " + bankHolidayDate.Year;
        //    var bankHolidayId = commonMethodsDB.CreateBankHoliday(bankHolidayName, bankHolidayDate, bankHolidayName);

        //    #endregion

        //    #region Bank Holiday Type

        //    var careProviderBankHolidayType1Id = dbHelper.careProviderBankHolidayType.GetByName("Manual").FirstOrDefault();
        //    var careProviderBankHolidayType2Id = dbHelper.careProviderBankHolidayType.GetByName("Standard").FirstOrDefault();

        //    #endregion

        //    #region Bank Holiday Date

        //    commonMethodsDB.CreateCPBankHolidayDate(_careProviders_TeamId, bankHolidayChargingCalendar1Id, "BHCC ACC-4804 A", bankHolidayId, careProviderBankHolidayType1Id);
        //    commonMethodsDB.CreateCPBankHolidayDate(_careProviders_TeamId, bankHolidayChargingCalendar2Id, "BHCC ACC-4804 B", bankHolidayId, careProviderBankHolidayType2Id);

        //    #endregion

        //    #region Step 18

        //    loginPage
        //       .GoToLoginPage()
        //       .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToProvidersSection();

        //    providersPage
        //        .WaitForProvidersPageToLoad()
        //        .SearchProviderRecord(provider1Name)
        //        .OpenProviderRecord(provider1ID);

        //    providersRecordPage
        //        .WaitForProvidersRecordPageToLoad()
        //        .ClickContractServicesTab();


        //    contractServicesPage
        //        .WaitForContractServicesPageToLoad()
        //        .OpenRecord(careProviderContractServiceId);

        //    contractServiceRecordPage
        //        .WaitForContractServiceRecordPageToLoad()
        //        .ClickRatesTab();

        //    contractServiceRatePeriodsPage
        //        .WaitForContractServiceRatePeriodsPageToLoad()
        //        .ClickNewRecordButton();

        //    contractServiceRatePeriodRecordPage
        //        .WaitForContractServiceRatePeriodRecordPageToLoad()
        //        .InsertTextOnStartDate(todayDate.ToString("dd'/'MM'/'yyyy"))
        //        .ClickTimebandSetLookupButton();

        //    lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default TS", timebandSetId);

        //    contractServiceRatePeriodRecordPage
        //        .WaitForContractServiceRatePeriodRecordPageToLoad()
        //        .InsertTextOnRate("10")
        //        .ClickBankHolidayChargingCalendarLookupButton();

        //    lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("BHCC ACC-4804 A", bankHolidayChargingCalendar1Id);

        //    contractServiceRatePeriodRecordPage
        //        .WaitForContractServiceRatePeriodRecordPageToLoad()
        //        .InsertTextOnBankHolidayRate("a") //this field was deprecated --https://advancedcsg.atlassian.net/browse/ACC-2997
        //        .ValidateBankHolidayRateErrorLabelText("Please enter a value between -999999999.99 and 999999999.99.");

        //    #endregion

        //    #region Step 19

        //    // already included as part of step 18

        //    #endregion

        //    #region Step 20

        //    contractServiceRatePeriodRecordPage
        //        .ClickBankHolidayChargingCalendarLookupButton();

        //    lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("BHCC ACC-4804 B", bankHolidayChargingCalendar2Id);

        //    contractServiceRatePeriodRecordPage
        //        .WaitForContractServiceRatePeriodRecordPageToLoad()
        //        .InsertTextOnBankHolidayRateFieldVisible(false); //this field was deprecated --https://advancedcsg.atlassian.net/browse/ACC-2997

        //    #endregion
        //}

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4805

        [TestProperty("JiraIssueID", "ACC-4897")]
        [Description("Step(s) 21 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Service Rate Periods")]
        public void Provider_ContractServiceRatePeriods_UITestCases013()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4805_1 " + currentTimeString;
            var provider2Name = "ACC-4805_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceRatePeriodUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractServiceRatePeriod", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider2ID);

            #endregion

            #region Care Provider Service

            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-4805 A", new DateTime(2020, 1, 1), 9995000, null, false);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_careProviders_TeamId, "CPSD ACC-4805 A", 1070, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "CPRU 4805 A", new DateTime(2020, 1, 1), 9999700, true, false);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var contractservicetypeid = 1; //Spot
            var isnegotiatedratescanapply = false;
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, contractservicetypeid, 1, false, isnegotiatedratescanapply);

            #endregion

            #region Timeband Set

            var timebandSetId = commonMethodsDB.CreateTimebandSet("Default TS", _careProviders_TeamId);

            #endregion

            #region Bank Holiday Charging Calendar

            var bankHolidayChargingCalendar1Id = commonMethodsDB.CreateCPBankHolidayChargingCalendar(_careProviders_TeamId, "BHCC ACC-4805 A", "998000");
            var bankHolidayChargingCalendar2Id = commonMethodsDB.CreateCPBankHolidayChargingCalendar(_careProviders_TeamId, "BHCC ACC-4805 B", "997000");

            #endregion

            #region Public Holiday

            var bankHolidayDate = new DateTime(DateTime.Now.Year + 1, 12, 25);
            var bankHolidayName = "Christmas day " + bankHolidayDate.Year;
            var bankHolidayId = commonMethodsDB.CreateBankHoliday(bankHolidayName, bankHolidayDate, bankHolidayName);

            #endregion

            #region Bank Holiday Type

            var careProviderBankHolidayType1Id = dbHelper.careProviderBankHolidayType.GetByName("Manual").FirstOrDefault();
            var careProviderBankHolidayType2Id = dbHelper.careProviderBankHolidayType.GetByName("Standard").FirstOrDefault();

            #endregion

            #region Bank Holiday Date

            commonMethodsDB.CreateCPBankHolidayDate(_careProviders_TeamId, bankHolidayChargingCalendar1Id, "BHCC ACC-4805 A", bankHolidayId, careProviderBankHolidayType1Id);
            commonMethodsDB.CreateCPBankHolidayDate(_careProviders_TeamId, bankHolidayChargingCalendar2Id, "BHCC ACC-4805 B", bankHolidayId, careProviderBankHolidayType2Id);

            #endregion

            #region Step 21

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();


            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickRatesTab();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad()
                .ClickNewRecordButton();

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .InsertTextOnStartDate("01/09/2023")
                .ClickTimebandSetLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default TS", timebandSetId);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .InsertTextOnRate("10.95")
                .ClickBankHolidayChargingCalendarLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("BHCC ACC-4805 A", bankHolidayChargingCalendar1Id);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                //.InsertTextOnBankHolidayRate("21.23") //field is deprecated as part of https://advancedcsg.atlassian.net/browse/ACC-2997
                .ClickSaveAndCloseButton();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad()
                .ClickRefreshButton();

            var contractServiceRatePeriods = dbHelper.careProviderContractServiceRatePeriod.GetByContractServiceID(careProviderContractServiceId);
            Assert.AreEqual(1, contractServiceRatePeriods.Count);
            var contractServiceRatePeriod1Id = contractServiceRatePeriods[0];

            contractServiceRatePeriodsPage
                .SelectRecord(contractServiceRatePeriod1Id.ToString())
                .ClickCloneRecordButton();

            cloningContractServiceRatePeriodPopup
                .WaitForCloningContractServiceRatePeriodPopupToLoad()
                .ClickCloneButton()
                .ValidateStartDateErrorLabelVisibility(true)
                .ValidateStartDateErrorLabelText("Please fill out this field.")
                .InsertStartDate("07/09/2023")
                .ClickCloneButton()
                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Contract Service Rate Period cloned successfully.")
                .ClickCloseButton();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad()
                .ClickRefreshButton();

            contractServiceRatePeriods = dbHelper.careProviderContractServiceRatePeriod.GetByContractServiceID(careProviderContractServiceId);
            Assert.AreEqual(2, contractServiceRatePeriods.Count);
            var contractServiceRatePeriod2Id = contractServiceRatePeriods.Where(c => c != contractServiceRatePeriod1Id).First();

            contractServiceRatePeriodsPage
                .OpenRecord(contractServiceRatePeriod2Id);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ValidateStartDateText("07/09/2023")
                .ValidateRateUnitLinkText("CPRU 4805 A")
                .ValidateTimebandSetLinkText("Default TS")
                .ValidateRateText("10.95")
                .ValidateResponsibleTeamLinkText(teamName)
                .ValidateEndDateText("")
                .ValidateBankHolidayChargingCalendarLinkText("BHCC ACC-4805 A");
            //.ValidateBankHolidayRateText("21.23"); field was deprecated as part of the story https://advancedcsg.atlassian.net/browse/ACC-2997

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4898")]
        [Description("Step(s) 22 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Service Rate Periods")]
        public void Provider_ContractServiceRatePeriods_UITestCases014()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4805_1 " + currentTimeString;
            var provider2Name = "ACC-4805_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceRatePeriodUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractServiceRatePeriod", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider2ID);

            #endregion

            #region Care Provider Service

            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-4805 A", new DateTime(2020, 1, 1), 9995000, null, false);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_careProviders_TeamId, "CPSD ACC-4805 A", 1070, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "CPRU 4805 B", new DateTime(2020, 1, 1), 9999600, true, true);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var contractservicetypeid = 1; //Spot
            var isnegotiatedratescanapply = false;
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, contractservicetypeid, 1, false, isnegotiatedratescanapply);

            #endregion

            #region Timeband Set

            var timebandSetId = commonMethodsDB.CreateTimebandSet("Default TS", _careProviders_TeamId);

            #endregion

            #region Band Rate Type

            var bandRateTypeId = commonMethodsDB.CreateCareProviderBandRateType(_careProviders_TeamId, "BRT ACC-4805 B", "998000", new DateTime(2020, 1, 1));

            #endregion

            #region Band Rate Schedule

            commonMethodsDB.CreateCareProviderBandRateSchedule(_careProviders_TeamId, "Level 1", bandRateTypeId, new TimeSpan(0, 1, 0), new TimeSpan(23, 59, 0));

            #endregion

            #region Step 22

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();


            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickRatesTab();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad()
                .ClickNewRecordButton();

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .InsertTextOnStartDate("01/09/2023")
                .ClickTimebandSetLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default TS", timebandSetId);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ClickBandRateTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("BRT ACC-4805 B", bandRateTypeId);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .InsertTextOnNoteText("notes ...")
                .ClickSaveAndCloseButton();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad()
                .ClickRefreshButton();

            var contractServiceRatePeriods = dbHelper.careProviderContractServiceRatePeriod.GetByContractServiceID(careProviderContractServiceId);
            Assert.AreEqual(1, contractServiceRatePeriods.Count);
            var contractServiceRatePeriod1Id = contractServiceRatePeriods[0];

            contractServiceRatePeriodsPage
                .SelectRecord(contractServiceRatePeriod1Id.ToString())
                .ClickCloneRecordButton();

            cloningContractServiceRatePeriodPopup
                .WaitForCloningContractServiceRatePeriodPopupToLoad()
                .InsertStartDate("07/09/2023")
                .ClickCloneButton()
                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Contract Service Rate Period cloned successfully.")
                .ClickCloseButton();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad()
                .ClickRefreshButton();

            contractServiceRatePeriods = dbHelper.careProviderContractServiceRatePeriod.GetByContractServiceID(careProviderContractServiceId);
            Assert.AreEqual(2, contractServiceRatePeriods.Count);
            var contractServiceRatePeriod2Id = contractServiceRatePeriods.Where(c => c != contractServiceRatePeriod1Id).First();

            contractServiceRatePeriodsPage
                .OpenRecord(contractServiceRatePeriod2Id);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ValidateStartDateText("07/09/2023")
                .ValidateRateUnitLinkText("CPRU 4805 B")
                .ValidateTimebandSetLinkText("Default TS")
                .ValidateResponsibleTeamLinkText(teamName)
                .ValidateEndDateText("")
                .ValidateBandRateTypeLookupLinkText("BRT ACC-4805 B")
                .ValidateNoteTextText("notes ...");

            var bandedRates = dbHelper.careProviderContractServiceBandedRates.GetByContractServiceRatePeriodId(contractServiceRatePeriod2Id);
            Assert.AreEqual(1, bandedRates.Count);
            var bandedRateId = bandedRates[0];

            var allFields = dbHelper.careProviderContractServiceBandedRates.GetByID(bandedRateId, "timefrom", "timeto");
            Assert.AreEqual(new TimeSpan(0, 1, 0), allFields["timefrom"]);
            Assert.AreEqual(new TimeSpan(23, 59, 0), allFields["timeto"]);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4899")]
        [Description("Step(s) 23 to 27 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Service Rate Periods")]
        public void Provider_ContractServiceRatePeriods_UITestCases015()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4805_1 " + currentTimeString;
            var provider2Name = "ACC-4805_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceRatePeriodUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractServiceRatePeriod", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider2ID);

            #endregion

            #region Care Provider Service

            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-4805 A", new DateTime(2020, 1, 1), 9995000, null, false);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_careProviders_TeamId, "CPSD ACC-4805 A", 1070, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "CPRU 4805 B", new DateTime(2020, 1, 1), 9999600, true, true);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var contractservicetypeid = 1; //Spot
            var isnegotiatedratescanapply = false;
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, contractservicetypeid, 1, false, isnegotiatedratescanapply);

            #endregion

            #region Timeband Set

            var timebandSetId = commonMethodsDB.CreateTimebandSet("Default TS", _careProviders_TeamId);

            #endregion

            #region Band Rate Type

            var bandRateTypeId = commonMethodsDB.CreateCareProviderBandRateType(_careProviders_TeamId, "BRT ACC-4805 C", "997000", new DateTime(2020, 1, 1));

            #endregion

            #region Band Rate Schedule

            commonMethodsDB.CreateCareProviderBandRateSchedule(_careProviders_TeamId, "Level 1", bandRateTypeId, new TimeSpan(0, 1, 0), new TimeSpan(12, 59, 0));
            commonMethodsDB.CreateCareProviderBandRateSchedule(_careProviders_TeamId, "Level 2", bandRateTypeId, new TimeSpan(13, 0, 0), new TimeSpan(23, 59, 0));

            #endregion

            #region Step 23

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();


            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickRatesTab();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad()
                .ClickNewRecordButton();

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .InsertTextOnStartDate("01/09/2023")
                .ClickTimebandSetLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default TS", timebandSetId);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ClickBandRateTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("BRT ACC-4805 C", bandRateTypeId);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .InsertTextOnNoteText("notes ...")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Please ensure you add rates to the records created under the Tab = Contract Service Banded Rates")
                .TapCloseButton();

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ClickContractServiceBandedRatesTab();

            #endregion

            #region Step 24

            var contractServiceRatePeriods = dbHelper.careProviderContractServiceRatePeriod.GetByContractServiceID(careProviderContractServiceId);
            Assert.AreEqual(1, contractServiceRatePeriods.Count);
            var contractServiceRatePeriod1Id = contractServiceRatePeriods[0];

            Assert.AreEqual(2, dbHelper.careProviderContractServiceBandedRates.GetByContractServiceRatePeriodId(contractServiceRatePeriod1Id).Count);

            var contractServiceBandedRate1 = dbHelper.careProviderContractServiceBandedRates.GetByContractServiceRatePeriodId(contractServiceRatePeriod1Id, "00:01 - 12:59").First();
            var contractServiceBandedRate2 = dbHelper.careProviderContractServiceBandedRates.GetByContractServiceRatePeriodId(contractServiceRatePeriod1Id, "13:00 - 23:59").First();

            contractServiceBandedRatesPage
                .WaitForContractServiceBandedRatesPageToLoad()
                .ValidateRecordPresent(contractServiceBandedRate1, true)
                .ValidateRecordPresent(contractServiceBandedRate2, true);

            #endregion

            #region Step 25

            contractServiceBandedRatesPage
                .OpenRecord(contractServiceBandedRate1);

            contractServiceBandedRatesRecordPage
                .WaitForContractServiceBandedRatesRecordPageToLoad()
                .ValidatePageTitleText("00:01 - 12:59")
                .ValidateContractServiceRatePeriodLookupButtonDisabled(true)
                .ValidateTimeFromFieldDisabled(true)
                .ValidateBandRateFieldDisabled(false)
                .ValidateContractServiceLookupButtonDisabled(true)
                .ValidateTimeToFieldDisabled(true)
                .ValidateResponsibleTeamLookupButtonDisabled(true);

            #endregion

            #region Step 26

            contractServiceBandedRatesRecordPage
                .InsertTextOnBandRate("14.58")
                .ClickSaveAndCloseButton();

            contractServiceBandedRatesPage
                .WaitForContractServiceBandedRatesPageToLoad()
                .ClickRefreshButton()
                .OpenRecord(contractServiceBandedRate1);

            contractServiceBandedRatesRecordPage
                .WaitForContractServiceBandedRatesRecordPageToLoad()
                .ValidateBandRateText("14.58");

            #endregion

            #region Step 27

            //this is already validated when we call the WaitForContractServiceBandedRatesPageToLoad() method. we make sure the new record button is not present

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4805

        [TestProperty("JiraIssueID", "ACC-4900")]
        [Description("Step(s) 28 to 30 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Service Rate Periods")]
        public void Provider_ContractServiceRatePeriods_UITestCases016()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4805_1 " + currentTimeString;
            var provider2Name = "ACC-4805_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceRatePeriodUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractServiceRatePeriod", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider2ID);

            #endregion

            #region Care Provider Service

            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-4805 A", new DateTime(2020, 1, 1), 9995000, null, false);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_careProviders_TeamId, "CPSD ACC-4805 A", 1070, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "CPRU 4805 B", new DateTime(2020, 1, 1), 9999600, true, true);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var contractservicetypeid = 1; //Spot
            var isnegotiatedratescanapply = false;
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, contractservicetypeid, 1, false, isnegotiatedratescanapply);
            var careProviderContractServiceTitle = dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"].ToString();

            #endregion

            #region Timeband Set

            var timebandSetId = commonMethodsDB.CreateTimebandSet("Default TS", _careProviders_TeamId);

            #endregion

            #region Band Rate Type

            var bandRateTypeId = commonMethodsDB.CreateCareProviderBandRateType(_careProviders_TeamId, "BRT ACC-4805 C", "997000", new DateTime(2020, 1, 1));

            #endregion

            #region Band Rate Schedule

            commonMethodsDB.CreateCareProviderBandRateSchedule(_careProviders_TeamId, "Level 1", bandRateTypeId, new TimeSpan(0, 1, 0), new TimeSpan(12, 59, 0));
            commonMethodsDB.CreateCareProviderBandRateSchedule(_careProviders_TeamId, "Level 2", bandRateTypeId, new TimeSpan(13, 0, 0), new TimeSpan(23, 59, 0));

            #endregion

            #region Step 28

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();


            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickRatesTab();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad()
                .ClickNewRecordButton();

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .InsertTextOnStartDate("01/09/2023")
                .ClickTimebandSetLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default TS", timebandSetId);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ClickBandRateTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("BRT ACC-4805 C", bandRateTypeId);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .InsertTextOnNoteText("notes ...")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Please ensure you add rates to the records created under the Tab = Contract Service Banded Rates")
                .TapCloseButton();

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ClickContractServiceBandedRatesTab();

            var contractServiceRatePeriods = dbHelper.careProviderContractServiceRatePeriod.GetByContractServiceID(careProviderContractServiceId);
            Assert.AreEqual(1, contractServiceRatePeriods.Count);
            var contractServiceRatePeriod1Id = contractServiceRatePeriods[0];
            var contractServiceRatePeriodTitle = dbHelper.careProviderContractServiceRatePeriod.GetByID(contractServiceRatePeriod1Id, "title")["title"].ToString();

            Assert.AreEqual(2, dbHelper.careProviderContractServiceBandedRates.GetByContractServiceRatePeriodId(contractServiceRatePeriod1Id).Count);

            var contractServiceBandedRate1 = dbHelper.careProviderContractServiceBandedRates.GetByContractServiceRatePeriodId(contractServiceRatePeriod1Id, "00:01 - 12:59").First();
            var contractServiceBandedRate2 = dbHelper.careProviderContractServiceBandedRates.GetByContractServiceRatePeriodId(contractServiceRatePeriod1Id, "13:00 - 23:59").First();

            contractServiceBandedRatesPage
                .WaitForContractServiceBandedRatesPageToLoad()
                .OpenRecord(contractServiceBandedRate1);

            contractServiceBandedRatesRecordPage
                .WaitForContractServiceBandedRatesRecordPageToLoad()
                .ValidatePageTitleText("00:01 - 12:59")
                .ValidateContractServiceRatePeriodLinkText(contractServiceRatePeriodTitle)
                .ValidateTimeFromText("00:01")
                .ValidateBandRateText("")
                .ValidateContractServiceLinkText(careProviderContractServiceTitle)
                .ValidateTimeToText("12:59")
                .ValidateResponsibleTeamLinkText(teamName);

            contractServiceBandedRatesRecordPage
                .InsertTextOnBandRate("14.58")
                .ClickSaveAndCloseButton();

            #endregion

            #region Step 29

            contractServiceBandedRatesPage
                .WaitForContractServiceBandedRatesPageToLoad()
                .ClickRefreshButton()
                .ValidateHeaderCellText(2, "Contract Service Rate Period")
                .ValidateHeaderCellText(3, "Visit Length From")
                .ValidateHeaderCellText(4, "Visit Length To")
                .ValidateHeaderCellText(5, "Band Rate")
                .ValidateHeaderCellText(7, "Modified By")
                .ValidateHeaderCellText(8, "Modified On");

            contractServiceBandedRatesPage
                .ValidateRecordPosition(1, contractServiceBandedRate1.ToString())
                .ValidateRecordPosition(2, contractServiceBandedRate2.ToString());

            contractServiceBandedRatesPage
                .ValidateRecordCellText(contractServiceBandedRate1, 2, contractServiceRatePeriodTitle)
                .ValidateRecordCellText(contractServiceBandedRate1, 3, "00:01")
                .ValidateRecordCellText(contractServiceBandedRate1, 4, "12:59")
                .ValidateRecordCellText(contractServiceBandedRate1, 5, "£14.58")
                .ValidateRecordCellText(contractServiceBandedRate1, 7, "ProviderType User_1");

            #endregion

            #region Step 30

            contractServiceBandedRatesPage
                .OpenRecord(contractServiceBandedRate1);

            contractServiceBandedRatesRecordPage
                .WaitForContractServiceBandedRatesRecordPageToLoad()
                .NavigateToAuditPage();

            auditListPage
                .WaitForAuditListPageToLoad("careprovidercontractservicebandedrates")
                .ValidateCellText(1, 2, "Updated")
                .ValidateCellText(1, 3, "ProviderType User_1")

                .ValidateCellText(2, 2, "Created")
                .ValidateCellText(2, 3, "ProviderType User_1");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4807

        [TestProperty("JiraIssueID", "ACC-4985")]
        [Description("Step(s) 31 to 34 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Service Rate Periods")]
        public void Provider_ContractServiceRatePeriods_UITestCases017()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4807_1 " + currentTimeString;
            var provider2Name = "ACC-4807_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceRatePeriodUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractServiceRatePeriod", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider2ID);

            #endregion

            #region Care Provider Service

            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-4807 A", new DateTime(2020, 1, 1), 9994000, null, false);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_careProviders_TeamId, "CPSD ACC-4807 A", 1060, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "CPRU 4807 B", new DateTime(2020, 1, 1), 9999500, true, true);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var contractservicetypeid = 1; //Spot
            var isnegotiatedratescanapply = false;
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, contractservicetypeid, 1, false, isnegotiatedratescanapply);
            var careProviderContractServiceTitle = dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"].ToString();

            #endregion

            #region Timeband Set

            var timebandSetId = commonMethodsDB.CreateTimebandSet("Default TS", _careProviders_TeamId);

            #endregion

            #region Band Rate Type

            var bandRateTypeId = commonMethodsDB.CreateCareProviderBandRateType(_careProviders_TeamId, "BRT ACC-4807 C", "996000", new DateTime(2020, 1, 1));

            #endregion

            #region Band Rate Schedule

            commonMethodsDB.CreateCareProviderBandRateSchedule(_careProviders_TeamId, "Level 1", bandRateTypeId, new TimeSpan(0, 1, 0), new TimeSpan(12, 59, 0));
            commonMethodsDB.CreateCareProviderBandRateSchedule(_careProviders_TeamId, "Level 2", bandRateTypeId, new TimeSpan(13, 0, 0), new TimeSpan(23, 59, 0));

            #endregion

            #region Step 31

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();


            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickRatesTab();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad()
                .ClickNewRecordButton();

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .InsertTextOnStartDate("01/09/2023")
                .ClickTimebandSetLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default TS", timebandSetId);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ClickBandRateTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("BRT ACC-4807 C", bandRateTypeId);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .InsertTextOnNoteText("notes ...")
                .ClickSaveAndCloseButton();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad()
                .ClickRefreshButton();

            var contractServiceRatePeriods = dbHelper.careProviderContractServiceRatePeriod.GetByContractServiceID(careProviderContractServiceId);
            Assert.AreEqual(1, contractServiceRatePeriods.Count);
            var contractServiceRatePeriod1Id = contractServiceRatePeriods[0];
            var contractServiceRatePeriodTitle = dbHelper.careProviderContractServiceRatePeriod.GetByID(contractServiceRatePeriod1Id, "title")["title"].ToString();

            Assert.AreEqual(2, dbHelper.careProviderContractServiceBandedRates.GetByContractServiceRatePeriodId(contractServiceRatePeriod1Id).Count);

            var contractServiceBandedRate1 = dbHelper.careProviderContractServiceBandedRates.GetByContractServiceRatePeriodId(contractServiceRatePeriod1Id, "00:01 - 12:59").First();
            var contractServiceBandedRate2 = dbHelper.careProviderContractServiceBandedRates.GetByContractServiceRatePeriodId(contractServiceRatePeriod1Id, "13:00 - 23:59").First();

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ValidateBandedRatesTabVisible(true);

            #endregion

            #region Step 32

            contractServiceRecordPage
                .ClickBandedRatesTab();

            contractServiceBandedRatesPage
                .WaitForContractServiceBandedRatesPageToLoadFromContractServiceRecord();

            #endregion

            #region Step 33

            contractServiceBandedRatesPage
                .ValidateHeaderCellText(2, "Contract Service [Contract Service Rate Period]")
                .ValidateHeaderCellText(3, "Contract Service Rate Period")
                .ValidateHeaderCellText(4, "Start Date [Contract Service Rate Period]")
                .ValidateHeaderCellText(5, "End Date [Contract Service Rate Period]")
                .ValidateHeaderCellText(6, "Rate Unit [Contract Service Rate Period]")
                .ValidateHeaderCellText(7, "Timeband Set [Contract Service Rate Period]")
                .ValidateHeaderCellText(8, "Band Rate Type [Contract Service Rate Period]")
                .ValidateHeaderCellText(9, "Visit Length From")
                .ValidateHeaderCellText(10, "Visit Length To")
                .ValidateHeaderCellText(11, "Band Rate")
                .ValidateHeaderCellText(13, "Modified By")
                .ValidateHeaderCellText(14, "Modified On");

            contractServiceBandedRatesPage
                .ValidateRecordPosition(1, contractServiceBandedRate1.ToString())
                .ValidateRecordPosition(2, contractServiceBandedRate2.ToString());

            #endregion

            #region Step 34

            contractServiceBandedRatesPage
                .SearchRecord(contractServiceRatePeriodTitle)
                .WaitForContractServiceBandedRatesPageToLoadFromContractServiceRecord()
                .ValidateRecordPosition(1, contractServiceBandedRate1.ToString())
                .ValidateRecordPosition(2, contractServiceBandedRate2.ToString());

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4986")]
        [Description("Step(s) 35 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Service Rate Periods")]
        public void Provider_ContractServiceRatePeriods_UITestCases018()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4807_1 " + currentTimeString;
            var provider2Name = "ACC-4807_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "ContractServiceRatePeriodUser1";
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "ContractServiceRatePeriod", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider2ID);

            #endregion

            #region Care Provider Service

            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-4807 A", new DateTime(2020, 1, 1), 9994000, null, false);

            #endregion

            #region Care Provider Service Detail

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_careProviders_TeamId, "CPSD ACC-4807 A", 1060, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "CPRU 4807 C", new DateTime(2020, 1, 1), 9999400, true, true);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var contractservicetypeid = 1; //Spot
            var isnegotiatedratescanapply = false;
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, contractservicetypeid, 1, false, isnegotiatedratescanapply);
            var careProviderContractServiceTitle = dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"].ToString();

            #endregion

            #region Timeband Set

            var timebandSetId = commonMethodsDB.CreateTimebandSet("Default TS", _careProviders_TeamId);

            #endregion

            #region Band Rate Type

            var bandRateTypeId = commonMethodsDB.CreateCareProviderBandRateType(_careProviders_TeamId, "BRT ACC-4807 D", "995000", new DateTime(2020, 1, 1));

            #endregion

            #region Step 35

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();


            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickRatesTab();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad()
                .ClickNewRecordButton();

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .InsertTextOnStartDate("01/09/2023")
                .ClickTimebandSetLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default TS", timebandSetId);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ClickBandRateTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("BRT ACC-4807 D", bandRateTypeId);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("For the selected Band Rate Type, no Time Bands have been associated to it and therefore this record can not be created. Please correct on the entity = Band Rate Schedule.")
                .TapCloseButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4987")]
        [Description("Step(s) 36 to 37 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Service Rate Periods")]
        public void Provider_ContractServiceRatePeriods_UITestCases019()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region booking Type

            var bookingTypeClassId = 5; //Booking (To Service User)
            var workingContractedTime = 1; //Count full booking length
            var cpbookingchargetypeid = 1; //Per Booking
            var bookingTypeId = commonMethodsDB.CreateBookingType("BT_ACC4807", bookingTypeClassId, 60, new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0), workingContractedTime, false, null, null, null, cpbookingchargetypeid);

            #endregion

            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4807_1 " + currentTimeString;
            var provider2Name = "ACC-4807_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            dbHelper.provider.UpdateEnableScheduling(provider1ID, true);

            #endregion

            #region Provider Allowable Booking Types

            dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_careProviders_TeamId, provider1ID, bookingTypeId, true);

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "PCSU_" + currentTimeString;
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Provider Contract", "Service User " + currentTimeString, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);

            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, thisWeekMonday);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Full Time Employee Contract", "2", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContract = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2023, 1, 1), _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid, 40, new List<Guid> { bookingTypeId });

            #endregion

            #region Recurrence Patterns

            var _recurrencePattern_Every1WeekMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on monday").First();
            var _recurrencePattern_Every1WeekTuesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on tuesday").First();
            var _recurrencePattern_Every1WeekWednesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on wednesday").First();
            var _recurrencePattern_Every1WeekThursdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on thursday").First();
            var _recurrencePattern_Every1WeekFridayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on friday").First();
            var _recurrencePattern_Every1WeekSaturdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on saturday").First();
            var _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();

            #endregion

            #region Availability Type

            var _availabilityTypes_StandardId = commonMethodsDB.CreateAvailabilityType("Standard", 3, false, _careProviders_TeamId, 1, 1, true);

            #endregion

            #region User Work Schedule

            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careProviders_TeamId, _recurrencePattern_Every1WeekMondayId, _systemUserEmploymentContract, _availabilityTypes_StandardId, thisWeekMonday, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careProviders_TeamId, _recurrencePattern_Every1WeekTuesdayId, _systemUserEmploymentContract, _availabilityTypes_StandardId, thisWeekMonday.AddDays(1), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careProviders_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContract, _availabilityTypes_StandardId, thisWeekMonday.AddDays(2), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careProviders_TeamId, _recurrencePattern_Every1WeekThursdayId, _systemUserEmploymentContract, _availabilityTypes_StandardId, thisWeekMonday.AddDays(3), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careProviders_TeamId, _recurrencePattern_Every1WeekFridayId, _systemUserEmploymentContract, _availabilityTypes_StandardId, thisWeekMonday.AddDays(4), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careProviders_TeamId, _recurrencePattern_Every1WeekSaturdayId, _systemUserEmploymentContract, _availabilityTypes_StandardId, thisWeekMonday.AddDays(5), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careProviders_TeamId, _recurrencePattern_Every1WeekSundayId, _systemUserEmploymentContract, _availabilityTypes_StandardId, thisWeekMonday.AddDays(6), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider2ID);

            #endregion

            #region Care Provider Service

            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-4807 B", new DateTime(2020, 1, 1), 9993000, null, true);

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, null, bookingTypeId, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "CPRU 4807 C", new DateTime(2020, 1, 1), 9999400, true, true);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var contractservicetypeid = 1; //Spot
            var isnegotiatedratescanapply = false;
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme1Id, careProviderService1Id, null, bookingTypeId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, contractservicetypeid, 1, false, isnegotiatedratescanapply);
            var careProviderContractServiceTitle = dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"].ToString();

            #endregion

            #region Timeband Set

            var timebandSetId = commonMethodsDB.CreateTimebandSet("Default TS", _careProviders_TeamId);

            #endregion

            #region Timeband

            var startDay = 1; //Monday
            var endDay = 7; //Sunday
            var timebandId = commonMethodsDB.CreateTimeband(_careProviders_TeamId, timebandSetId, startDay, new TimeSpan(0, 0, 0), endDay, new TimeSpan(0, 0, 0));

            #endregion

            #region Band Rate Type

            var bandRateTypeId = commonMethodsDB.CreateCareProviderBandRateType(_careProviders_TeamId, "BRT ACC-4807 E", "995000", new DateTime(2020, 1, 1));

            #endregion

            #region Band Rate Schedule

            commonMethodsDB.CreateCareProviderBandRateSchedule(_careProviders_TeamId, "Level 1", bandRateTypeId, new TimeSpan(0, 1, 0), new TimeSpan(12, 59, 0));
            commonMethodsDB.CreateCareProviderBandRateSchedule(_careProviders_TeamId, "Level 2", bandRateTypeId, new TimeSpan(13, 0, 0), new TimeSpan(23, 59, 0));

            #endregion

            #region Contract Service Rate Period

            var careProviderContractServiceRatePeriodId = dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 9, 1), careProviderRateUnitId, timebandSetId, _careProviders_TeamId, null, null, bandRateTypeId);

            #endregion

            #region Contract Service Banded Rates

            var contractServiceBandedRate1Id = dbHelper.careProviderContractServiceBandedRates.CreateCareProviderContractServiceBandedRate(careProviderContractServiceRatePeriodId, careProviderContractServiceId, new TimeSpan(0, 1, 0), new TimeSpan(12, 59, 0), 12.54m, _careProviders_TeamId);
            var contractServiceBandedRate2Id = dbHelper.careProviderContractServiceBandedRates.CreateCareProviderContractServiceBandedRate(careProviderContractServiceRatePeriodId, careProviderContractServiceId, new TimeSpan(13, 0, 0), new TimeSpan(23, 59, 0), 9.72m, _careProviders_TeamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_careProviders_TeamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var _personID = dbHelper.person.CreatePersonRecord("", "Peter", "", currentTimeString, "", new DateTime(2000, 1, 1), _ethnicityId, _careProviders_TeamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_careProviders_TeamId, "title", _personID, _systemUserId, provider1ID, careProviderContractScheme1Id, provider2ID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _careProviders_TeamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, careProviderRateUnitId);

            #endregion

            #region Step 36

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .WaitForSystemUsersPageToLoad()
                .OpenRecord(_systemUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmployeeDiaryPage();

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ClickAddBookingButton();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .SelectLocationProvider(provider1Name + " - pna, pno, st, dst, tw, co, CR0 3RL")
                .ClickSelectPeopleButton();

            selectMultiplePeoplePopup
                .WaitForSelectMultiplePeopleAreaToLoad()
                .ClickOnRecordCheckbox(_personcontractId)
                .ClickConfirmSelectionButton();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ClickOnChargesTab()
                .ValidateBookingChargeValue_ChargeTab(_personcontractId, "12.54")
                .ValidateContractServiceValue_ChargeTab(_personcontractId, careProviderContractServiceTitle);

            #endregion

            #region Step 37

            createDiaryBookingPopup
                .ClickOnRosteringTab()
                .WaitForCreateDiaryBookingPopupToLoad()
                .InsertEndTime("15", "00")

                /*functionality has changed. The message is not displayed anymore */
                //.WaitForNotificationAreaToBeVisible()
                //.ValidateNotificationAreaMessage("Warning", "The charges data has been reset due to booking changes.")
                //.ClickDismissButton_DynamicDialogue()

                .InsertStartTime("00", "00");

            createDiaryBookingPopup
                .ClickOnChargesTab()
                .ValidateBookingChargeValue_ChargeTab(_personcontractId, "9.72")
                .ValidateContractServiceValue_ChargeTab(_personcontractId, careProviderContractServiceTitle);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4988")]
        [Description("Step(s) 38 to 41 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Contract Service Rate Periods")]
        public void Provider_ContractServiceRatePeriods_UITestCases020()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region booking Type

            var bookingTypeClassId = 5; //Booking (To Service User)
            var workingContractedTime = 1; //Count full booking length
            var cpbookingchargetypeid = 1; //Per Booking
            var bookingTypeId = commonMethodsDB.CreateBookingType("BT_ACC4807", bookingTypeClassId, 60, new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0), workingContractedTime, false, null, null, null, cpbookingchargetypeid);

            #endregion

            #region Provider

            var addressType = 10; //Home

            var provider1Name = "ACC-4807_1 " + currentTimeString;
            var provider2Name = "ACC-4807_2 " + currentTimeString;

            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _careProviders_TeamId, 13, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _careProviders_TeamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            dbHelper.provider.UpdateEnableScheduling(provider1ID, true);

            #endregion

            #region Provider Allowable Booking Types

            dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_careProviders_TeamId, provider1ID, bookingTypeId, true);

            #endregion

            #region Create SystemUser Record

            var _systemUserName = "PCSU_" + currentTimeString;
            var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Provider Contract", "Service User " + currentTimeString, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);

            var localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, thisWeekMonday);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Full Time Employee Contract", "2", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContract = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2023, 1, 1), _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid, 40, new List<Guid> { bookingTypeId });

            #endregion

            #region Recurrence Patterns

            var _recurrencePattern_Every1WeekMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on monday").First();
            var _recurrencePattern_Every1WeekTuesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on tuesday").First();
            var _recurrencePattern_Every1WeekWednesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on wednesday").First();
            var _recurrencePattern_Every1WeekThursdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on thursday").First();
            var _recurrencePattern_Every1WeekFridayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on friday").First();
            var _recurrencePattern_Every1WeekSaturdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on saturday").First();
            var _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();

            #endregion

            #region Availability Type

            var _availabilityTypes_StandardId = commonMethodsDB.CreateAvailabilityType("Standard", 3, false, _careProviders_TeamId, 1, 1, true);

            #endregion

            #region User Work Schedule

            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careProviders_TeamId, _recurrencePattern_Every1WeekMondayId, _systemUserEmploymentContract, _availabilityTypes_StandardId, thisWeekMonday, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careProviders_TeamId, _recurrencePattern_Every1WeekTuesdayId, _systemUserEmploymentContract, _availabilityTypes_StandardId, thisWeekMonday.AddDays(1), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careProviders_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContract, _availabilityTypes_StandardId, thisWeekMonday.AddDays(2), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careProviders_TeamId, _recurrencePattern_Every1WeekThursdayId, _systemUserEmploymentContract, _availabilityTypes_StandardId, thisWeekMonday.AddDays(3), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careProviders_TeamId, _recurrencePattern_Every1WeekFridayId, _systemUserEmploymentContract, _availabilityTypes_StandardId, thisWeekMonday.AddDays(4), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careProviders_TeamId, _recurrencePattern_Every1WeekSaturdayId, _systemUserEmploymentContract, _availabilityTypes_StandardId, thisWeekMonday.AddDays(5), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careProviders_TeamId, _recurrencePattern_Every1WeekSundayId, _systemUserEmploymentContract, _availabilityTypes_StandardId, thisWeekMonday.AddDays(6), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, provider1ID, provider2ID);

            #endregion

            #region Care Provider Service

            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careProviders_TeamId, "CPS ACC-4807 B", new DateTime(2020, 1, 1), 9993000, null, true);

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careProviders_TeamId, null, bookingTypeId, null, "");

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careProviders_TeamId, "CPRU 4807 C", new DateTime(2020, 1, 1), 9999400, true, true);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careProviders_TeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var contractservicetypeid = 1; //Spot
            var isnegotiatedratescanapply = false;
            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "", provider1ID, provider2ID, careProviderContractScheme1Id, careProviderService1Id, null, bookingTypeId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, contractservicetypeid, 1, false, isnegotiatedratescanapply);
            var careProviderContractServiceTitle = dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"].ToString();

            #endregion

            #region Timeband Set

            var timebandSetId = commonMethodsDB.CreateTimebandSet("Default TS", _careProviders_TeamId);

            #endregion

            #region Timeband

            var startDay = 1; //Monday
            var endDay = 7; //Sunday
            var timebandId = commonMethodsDB.CreateTimeband(_careProviders_TeamId, timebandSetId, startDay, new TimeSpan(0, 0, 0), endDay, new TimeSpan(0, 0, 0));

            #endregion

            #region Band Rate Type

            var bandRateTypeId = commonMethodsDB.CreateCareProviderBandRateType(_careProviders_TeamId, "BRT ACC-4807 E", "995000", new DateTime(2020, 1, 1));

            #endregion

            #region Band Rate Schedule

            commonMethodsDB.CreateCareProviderBandRateSchedule(_careProviders_TeamId, "Level 1", bandRateTypeId, new TimeSpan(0, 1, 0), new TimeSpan(12, 59, 0));
            commonMethodsDB.CreateCareProviderBandRateSchedule(_careProviders_TeamId, "Level 2", bandRateTypeId, new TimeSpan(13, 0, 0), new TimeSpan(23, 59, 0));

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_careProviders_TeamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var _personID = dbHelper.person.CreatePersonRecord("", "Peter", "", currentTimeString, "", new DateTime(2000, 1, 1), _ethnicityId, _careProviders_TeamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_careProviders_TeamId, "title", _personID, _systemUserId, provider1ID, careProviderContractScheme1Id, provider2ID, todayDate.AddDays(-5), null, true);

            #endregion

            #region Person Contract Service

            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _careProviders_TeamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 90, 1, careProviderRateUnitId);

            #endregion

            #region Care Provider Staff Role Types

            var staffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "CPSRT 4807", "", "", new DateTime(202, 1, 1), "...");

            #endregion

            #region Step 38

            loginPage
               .GoToLoginPage()
               .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(provider1Name)
                .OpenProviderRecord(provider1ID);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickContractServicesTab();

            contractServicesPage
                .WaitForContractServicesPageToLoad()
                .OpenRecord(careProviderContractServiceId);

            contractServiceRecordPage
                .WaitForContractServiceRecordPageToLoad()
                .ClickRatesTab();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad()
                .ClickNewRecordButton();

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .InsertTextOnStartDate("01/10/2023")
                .ClickTimebandSetLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default TS", timebandSetId);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ClickBandRateTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("BRT ACC-4807 E", bandRateTypeId);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ClickJobRoleLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TapCloseButton();

            #endregion

            #region Step 39

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ClickSaveAndCloseButton();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad()
                .ClickRefreshButton();

            #region Contract Service Banded Rates

            var contractServiceRatePeriodId = dbHelper.careProviderContractServiceRatePeriod.GetByContractServiceID(careProviderContractServiceId).First();
            var contractServiceBandedRate1Id = dbHelper.careProviderContractServiceBandedRates.GetByContractServiceRatePeriodId(contractServiceRatePeriodId, "00:01 - 12:59").First();
            var contractServiceBandedRate2Id = dbHelper.careProviderContractServiceBandedRates.GetByContractServiceRatePeriodId(contractServiceRatePeriodId, "13:00 - 23:59").First();

            dbHelper.careProviderContractServiceBandedRates.UpdateBandRate(contractServiceBandedRate1Id, 9.75m);
            dbHelper.careProviderContractServiceBandedRates.UpdateBandRate(contractServiceBandedRate2Id, 15.91m);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_careProviders_TeamId, "CPEN ACC-4807", 9700, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 1; //Monday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = todayDate.AddYears(1);
            var separateinvoices = false;

            dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2023, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _careProviders_TeamId);

            #endregion

            #region Care Provider Diary Booking

            var bookingDiaryId = dbHelper.cPBookingDiary.CreateCPBookingDiary(_careProviders_TeamId, _careProviders_BusinessUnitId, "", bookingTypeId, provider1ID, todayDate, new TimeSpan(9, 0, 0), todayDate, new TimeSpan(9, 15, 0), "staff", 15, 0, "people", null);

            #endregion

            #region Booking Diary Staff

            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_careProviders_TeamId, "test", bookingDiaryId, _systemUserEmploymentContract, _systemUserId);

            #endregion

            #region Diary Booking To Person

            dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_careProviders_TeamId, bookingDiaryId, _personID, _personcontractId, careProviderContractServiceId);

            #endregion

            #region Schedule Job

            //Get the schedule job id
            var shcduleJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Process CP Finance Transaction Triggers").FirstOrDefault();

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //trigger the schedule job
            this.WebAPIHelper.ScheduleJob.Execute(shcduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //wait for the schedule job Idle state
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(shcduleJobId);

            #endregion

            contractServiceRatePeriodsPage
                .OpenRecord(contractServiceRatePeriodId);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ClickPersonContractLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("*" + currentTimeString + "*").TapSearchButton().SelectResultElement(_personcontractId);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ClickJobRoleLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("CPSRT 4807").TapSearchButton().SelectResultElement(staffRoleTypeId);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ClickSaveAndCloseButton();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad()
                .OpenRecord(contractServiceRatePeriodId);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ClickPersonContractOptionRemoveButton(_personcontractId)
                .ClickSaveAndCloseButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Multi Select Records can only be added (can not be removed). Contract Service Rate Period is used in finance.").TapCloseButton();

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.").TapOKButton();

            contractServiceRatePeriodsPage
                .WaitForContractServiceRatePeriodsPageToLoad()
                .OpenRecord(contractServiceRatePeriodId);

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ClickJobRoleOptionRemoveButton(staffRoleTypeId)
                .ClickSaveAndCloseButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Multi Select Records can only be added (can not be removed). Contract Service Rate Period is used in finance.").TapCloseButton();


            #endregion

            #region Step 40

            contractServiceRatePeriodRecordPage
                .WaitForContractServiceRatePeriodRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Related record exists in Contract Service Banded Rates. Please delete related records before deleting record in Contract Service Rate Period.").TapCloseButton();

            #endregion

            #region Step 41

            //there is no way for us to assert a button that is not there

            #endregion
        }

        #endregion


    }

}

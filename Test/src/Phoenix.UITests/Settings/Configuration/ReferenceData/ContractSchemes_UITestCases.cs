using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Configuration
{
    [TestClass]
    public class ContractSchemes_UITestCases : FunctionalTest
    {
        #region properties

        private string _environmentName;
        private string _tenantName;
        private Guid _authenticationProviderId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private string _teamName;
        private Guid _languageId;
        private string _defaultUserFullname;
        private string _loginUserName;
        private Guid _loginUserId;
        private string _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");

        #endregion

        [TestInitialize()]
        public void TestMethod_Setup()
        {
            #region Environment Name

            _environmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
            _tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            #endregion

            #region Internal

            _authenticationProviderId = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

            #endregion

            #region Default User

            string username = ConfigurationManager.AppSettings["Username"];
            string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

            var _defaultUsername = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
            var _defaultUserId = dbHelper.systemUser.GetSystemUserByUserName(_defaultUsername).FirstOrDefault();
            _defaultUserFullname = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultUserId, "fullname")["fullname"];

            #endregion

            #region Language

            _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

            #endregion Language

            #region Business Unit

            _businessUnitId = commonMethodsDB.CreateBusinessUnit("CareProviders");

            #endregion

            #region Team

            _teamName = "CareProviders";
            _teamId = commonMethodsDB.CreateTeam(_teamName, null, _businessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");

            #endregion

            #region System User

            _loginUserName = "ContractSchemesUser1";
            _loginUserId = commonMethodsDB.CreateSystemUserRecord(_loginUserName, "ContractSchemes", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationProviderId);

            #endregion
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-4332

        [TestProperty("JiraIssueID", "ACC-4360")]
        [Description("Step(s) 1 to 6 from the original test method ACC-923")]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestMethod]
        public void ContractSchemes_UITestMethod001()
        {
            #region Provider

            var ProviderName = "Contract_Scheme_Provider";
            var ProviderId = commonMethodsDB.CreateProvider(ProviderName, _teamId, 13);

            #endregion

            #region Finance Code Location

            var financeCodeLocationId_ContractScheme = dbHelper.careProviderFinanceCodeLocation.GetByName("Contract Scheme")[0];
            var financeCodeLocationId_Constant = dbHelper.careProviderFinanceCodeLocation.GetByName("Constant")[0];

            #endregion

            #region Step 2

            loginPage
                .GoToLoginPage()
                .Login(_loginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Finance Code Locations")
                .ClickReferenceDataMainHeader("Person Contracts")
                .ClickReferenceDataElement("Finance Code Locations");

            financeCodeLocationsPage
                .WaitForFinanceCodeLocationsPageToLoad()
                .ValidateRecordIsPresentOrNot(financeCodeLocationId_ContractScheme.ToString(), true)
                .ValidateRecordCellContent(financeCodeLocationId_Constant.ToString(), 2, "Contract Scheme")
                .ValidateRecordCellContent(financeCodeLocationId_Constant.ToString(), 3, "2")

                .ValidateRecordIsPresentOrNot(financeCodeLocationId_Constant.ToString(), true)
                .ValidateRecordCellContent(financeCodeLocationId_Constant.ToString(), 2, "Constant")
                .ValidateRecordCellContent(financeCodeLocationId_Constant.ToString(), 3, "9");

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
                .ClickCreateNewRecordButton();

            contractSchemeRecordPage
                .WaitForContractSchemeRecordPageToLoad()
                .ValidateAllFieldsVisible()
                .ValidateNameMandatoryFieldVisibility(true)
                .ValidateCodeMandatoryFieldVisibility(true)
                .ValidateCodeFieldIsNumeric()
                .ValidateGovCodeFieldMaximumLimitText("8")
                .ValidateStartDateMandatoryFieldVisibility(true)
                .ValidateResponsibleTeamMandatoryFieldVisibility(true)
                .ValidateResponsibleUserMandatoryFieldVisibility(true)

                .ValidateEstablishmentMandatoryFieldVisibility(true)
                .ValidateFunderMandatoryFieldVisibility(true)
                .ValidateUpdateableOnPersonContractServiceMandatoryFieldVisibility(true)

                .ValidateNoteTextFieldMaximumLimitText("2000");

            #endregion

            #region Step 3

            contractSchemeRecordPage
                .ValidateStartDateText(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateInactive_OptionIsCheckedOrNot(false)
                .ValidateValidForExport_OptionIsCheckedOrNot(false)
                .ValidateResponsibleTeamLinkText(_teamName)

                .ValidateUpdateableOnPersonContractService_OptionIsCheckedOrNot(false)
                .ValidateChargeScheduledCareOnActuals_OptionIsCheckedOrNot(true)
                .ValidateDefaultAllPersonContractsEnabledForScheduledBookings_OptionIsCheckedOrNot(false)
                .ValidateSundriesApply_OptionIsCheckedOrNot(false)
                .ValidateAccountCodeApplies_OptionIsCheckedOrNot(false);

            #endregion

            #region Step 4

            contractSchemeRecordPage
                .ClickSaveButton()
                .ValidateNotificationAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateNameFieldErrorLabelText("Please fill out this field.")
                .ValidateCodeFieldErrorLabelText("Please fill out this field.")
                .ValidateResponsibleUserFieldErrorLabelText("Please fill out this field.")
                .ValidateEstablishmentFieldErrorLabelText("Please fill out this field.")
                .ValidateFunderFieldErrorLabelText("Please fill out this field.");

            #endregion

            #region Step 5

            var contractSchemeName = "Contract_" + _currentDateString;
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);

            contractSchemeRecordPage
                .InsertName(contractSchemeName)
                .InsertCode(code.ToString())
                .InsertGovCode("123")
                .ClickResponsibleUserLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup View")
                .TypeSearchQuery(_loginUserName)
                .TapSearchButton()
                .SelectResultElement(_loginUserId);

            contractSchemeRecordPage
                .WaitForContractSchemeRecordPageToLoad()
                .ClickEstablishmentLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(ProviderName)
                .TapSearchButton()
                .SelectResultElement(ProviderId);

            contractSchemeRecordPage
                .WaitForContractSchemeRecordPageToLoad()
                .ClickFunderLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(ProviderName)
                .TapSearchButton()
                .SelectResultElement(ProviderId);

            contractSchemeRecordPage
                .WaitForContractSchemeRecordPageToLoad()
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateResponsibleTeamLookupButtonIsDisabled(true)

                .ValidateNameFieldIsDisabled(false)
                .ValidateCodeFieldIsDisabled(false)
                .ValidateGovCodeFieldIsDisabled(false)
                .ValidateInactiveOptionsIsDisabled(false)
                .ValidateStartDateFieldIsDisabled(false)
                .ValidateEndDateFieldIsDisabled(false)
                .ValidateValidForExportOptionsIsDisabled(false)
                .ValidateResponsibleUserLookupButtonIsDisabled(false)

                .ValidateEstablishmentLookupButtonIsDisabled(false)
                .ValidateFunderLookupButtonIsDisabled(false)
                .ValidateFinanceCodeLookupButtonIsDisabled(false)
                .ValidateChargeScheduledCareOnActualsOptionsIsDisabled(false)
                .ValidateSundriesApplyOptionsIsDisabled(false)
                .ValidateUpdateableOnPersonContractServiceOptionsIsDisabled(false)
                .ValidateDefaultAllPersonContractsEnabledForScheduledBookingsOptionsIsDisabled(false)
                .ValidateAccountCodeAppliesOptionsIsDisabled(false)

                .ValidateNoteTextFieldIsDisabled(false)
                .ClickBackButton();

            #endregion

            #region Step 6

            contractSchemesPage
                .WaitForContractSchemesPageToLoad()
                .ClickCreateNewRecordButton();

            var code2 = commonMethodsHelper.GetRandomValue(1, 2147483647);

            contractSchemeRecordPage
                .WaitForContractSchemeRecordPageToLoad()
                .InsertName(contractSchemeName)
                .InsertCode(code2.ToString())
                .InsertGovCode("123")
                .ClickResponsibleUserLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup View")
                .TypeSearchQuery(_loginUserName)
                .TapSearchButton()
                .SelectResultElement(_loginUserId);

            contractSchemeRecordPage
                .WaitForContractSchemeRecordPageToLoad()
                .ClickEstablishmentLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(ProviderName)
                .TapSearchButton()
                .SelectResultElement(ProviderId);

            contractSchemeRecordPage
                .WaitForContractSchemeRecordPageToLoad()
                .ClickFunderLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(ProviderName)
                .TapSearchButton()
                .SelectResultElement(ProviderId);

            contractSchemeRecordPage
                .WaitForContractSchemeRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Record with Name = '" + contractSchemeName + "' already exists. Please choose another name.")
                .TapCloseButton();

            var contractSchemeName2 = "Contract_2_" + _currentDateString;

            contractSchemeRecordPage
                .WaitForContractSchemeRecordPageToLoad()
                .InsertName(contractSchemeName2)
                .InsertCode(code.ToString())
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Record with Code = '" + code.ToString() + "' already exists. Please choose another code.")
                .TapCloseButton();

            contractSchemeRecordPage
                .WaitForContractSchemeRecordPageToLoad()
                .InsertName(contractSchemeName)
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Record with Name = '" + contractSchemeName + "' and Code = '" + code.ToString() + "' already exists. Please choose another Name and Code.")
                .TapCloseButton();

            contractSchemeRecordPage
                .WaitForContractSchemeRecordPageToLoad();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4385

        [TestProperty("JiraIssueID", "ACC-4492")]
        [Description("Step(s) 7 to 10 from the original test method ACC-923")]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestMethod]
        public void ContractSchemes_UITestMethod002()
        {
            #region Provider

            var ProviderName1 = "Contract_Scheme_Provider";
            var ProviderId1 = commonMethodsDB.CreateProvider(ProviderName1, _teamId, 13);

            var ProviderName2 = "ACC-4385_Other_Provider";
            var ProviderId2 = commonMethodsDB.CreateProvider(ProviderName2, _teamId);

            #endregion

            #region create contract scheme

            var contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var contractSchemeName = "CareProvider_Contract_Scheme_001";
            var _contractschemeid = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _loginUserId, _businessUnitId, contractSchemeName, new DateTime(2000, 1, 2), contractSchemeCode, ProviderId1, ProviderId1);

            #endregion

            #region Finance Code Location

            var FinanceCodeLocationId = dbHelper.careProviderFinanceCodeLocation.GetByName("Contract Scheme")[0];

            #endregion


            #region Finance Code

            var FinanceCodeName = "ACC_4492";
            var FinanceCodeId = commonMethodsDB.CreateCareProviderFinanceCode(_teamId, FinanceCodeLocationId, FinanceCodeName, "FC_ACC_4492");
            var FinanceCodeTitle = (dbHelper.careProviderFinanceCode.GetByID(FinanceCodeId, "title")["title"]).ToString();


            #endregion

            #region Step 7

            loginPage
                .GoToLoginPage()
                .Login(_loginUserName, "Passw0rd_!");

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
                .ClickCreateNewRecordButton();

            contractSchemeRecordPage
                .WaitForContractSchemeRecordPageToLoad()
                .ClickEstablishmentLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(ProviderName2)
                .TapSearchButton()
                .ValidateResultElementNotPresent(ProviderId2.ToString())

                .TypeSearchQuery(ProviderName1)
                .TapSearchButton()
                .SelectResultElement(ProviderId1);

            #endregion

            #region Step 8

            contractSchemeRecordPage
                .WaitForContractSchemeRecordPageToLoad()
                .ClickFunderLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(ProviderName2)
                .TapSearchButton()
                .ValidateResultElementNotPresent(ProviderId2.ToString())

                .TypeSearchQuery(ProviderName1)
                .TapSearchButton()
                .SelectResultElement(ProviderId1);

            #endregion

            #region Step 9

            contractSchemeRecordPage
                .WaitForContractSchemeRecordPageToLoad()
                .ClickFinanceCodeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(FinanceCodeName)
                .TapSearchButton()
                .ValidateResultElementPresent(FinanceCodeId.ToString())
                .SelectResultElement(FinanceCodeId);

            contractSchemeRecordPage
                .WaitForContractSchemeRecordPageToLoad()
                .ValidateFinanceCodeLinkText(FinanceCodeTitle)
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            #endregion

            #region Step 10

            contractSchemesPage
                .WaitForContractSchemesPageToLoad()
                .InsertTextOnQuickSearch(contractSchemeName)
                .ClickQuickSearchButton()
                .SelectContractSchemeRecord(_contractschemeid.ToString())
                .ClickDeleteRecordButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("1 item(s) deleted.")
                .TapOKButton();

            contractSchemesPage
                .WaitForContractSchemesPageToLoad()
                .InsertTextOnQuickSearch(contractSchemeName)
                .ClickQuickSearchButton()
                .ValidateRecordPresent(_contractschemeid.ToString(), false);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4493")]
        [Description("Step(s) 11 to 14 from the original test method ACC-923")]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestMethod]
        public void ContractSchemes_UITestMethod003()
        {
            #region Provider

            var ProviderName = "Contract_Scheme_Provider_Establishment";
            var ProviderId = commonMethodsDB.CreateProvider(ProviderName, _teamId, 13);

            var ProviderName2 = "Contract_Scheme_Provider_Funder";
            var ProviderId2 = commonMethodsDB.CreateProvider(ProviderName2, _teamId, 13);

            #endregion

            #region Contract Scheme

            var contractSchemeName = "Contract_2_" + _currentDateString;
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);

            #endregion

            #region Step 11

            loginPage
                .GoToLoginPage()
                .Login(_loginUserName, "Passw0rd_!");

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
                .ClickCreateNewRecordButton();

            contractSchemeRecordPage
                .WaitForContractSchemeRecordPageToLoad()
                .InsertName(contractSchemeName)
                .InsertCode(code.ToString())
                .InsertGovCode("123")
                .ClickResponsibleUserLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup View")
                .TypeSearchQuery(_loginUserName)
                .TapSearchButton()
                .SelectResultElement(_loginUserId);

            contractSchemeRecordPage
                .WaitForContractSchemeRecordPageToLoad()
                .ClickEstablishmentLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(ProviderName)
                .TapSearchButton()
                .SelectResultElement(ProviderId);

            contractSchemeRecordPage
                .WaitForContractSchemeRecordPageToLoad()
                .ClickFunderLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(ProviderName2)
                .TapSearchButton()
                .SelectResultElement(ProviderId2);

            contractSchemeRecordPage
                .WaitForContractSchemeRecordPageToLoad()
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            var contractSchemeId = dbHelper.careProviderContractScheme.GetCareProviderContractSchemeByName(contractSchemeName);
            Assert.AreEqual(1, contractSchemeId.Count);

            contractSchemeRecordPage
                .ClickInactive_Option(true)
                .ClickSaveButton();

            contractSchemeRecordPage
                .ValidateActivateButtonIsVisible(true)

                .ValidateNameFieldIsDisabled(true)
                .ValidateCodeFieldIsDisabled(true)
                .ValidateGovCodeFieldIsDisabled(true)
                .ValidateInactiveOptionsIsDisabled(true)
                .ValidateStartDateFieldIsDisabled(true)
                .ValidateEndDateFieldIsDisabled(true)
                .ValidateValidForExportOptionsIsDisabled(true)
                .ValidateResponsibleUserLookupButtonIsDisabled(true)
                .ValidateResponsibleTeamLookupButtonIsDisabled(true)

                .ValidateEstablishmentLookupButtonIsDisabled(true)
                .ValidateFunderLookupButtonIsDisabled(true)
                .ValidateFinanceCodeLookupButtonIsDisabled(true)
                .ValidateChargeScheduledCareOnActualsOptionsIsDisabled(true)
                .ValidateSundriesApplyOptionsIsDisabled(true)
                .ValidateUpdateableOnPersonContractServiceOptionsIsDisabled(true)
                .ValidateDefaultAllPersonContractsEnabledForScheduledBookingsOptionsIsDisabled(true)
                .ValidateAccountCodeAppliesOptionsIsDisabled(true)

                .ValidateNoteTextFieldIsDisabled(true)
                .ValidateContractSchemeActiveLabelText("No")
                .ClickActivateButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Are you sure you want to activate this record? To continue, click ok.")
                .TapOKButton();

            contractSchemeRecordPage
                .WaitForContractSchemeRecordPageToLoad()
                .ValidateActivateButtonIsVisible(false)

                .ValidateNameFieldIsDisabled(false)
                .ValidateCodeFieldIsDisabled(false)
                .ValidateGovCodeFieldIsDisabled(false)
                .ValidateInactiveOptionsIsDisabled(false)
                .ValidateStartDateFieldIsDisabled(false)
                .ValidateEndDateFieldIsDisabled(false)
                .ValidateValidForExportOptionsIsDisabled(false)
                .ValidateResponsibleUserLookupButtonIsDisabled(false)
                .ValidateResponsibleTeamLookupButtonIsDisabled(true)

                .ValidateEstablishmentLookupButtonIsDisabled(false)
                .ValidateFunderLookupButtonIsDisabled(false)
                .ValidateFinanceCodeLookupButtonIsDisabled(false)
                .ValidateChargeScheduledCareOnActualsOptionsIsDisabled(false)
                .ValidateSundriesApplyOptionsIsDisabled(false)
                .ValidateUpdateableOnPersonContractServiceOptionsIsDisabled(false)
                .ValidateDefaultAllPersonContractsEnabledForScheduledBookingsOptionsIsDisabled(false)
                .ValidateAccountCodeAppliesOptionsIsDisabled(false)

                .ValidateNoteTextFieldIsDisabled(false)
                .ValidateContractSchemeActiveLabelText("Yes");

            #endregion

            #region Step 12

            contractSchemeRecordPage
                 .NavigateToAuditPage();

            var auditSearch1 = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 2, //update operation
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = contractSchemeId[0].ToString(),
                ParentTypeName = "careprovidercontractscheme",
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
            Assert.AreEqual("Update", auditResponseData.GridData[0].cols[0].Text);
            Assert.AreEqual("ContractSchemes User1", auditResponseData.GridData[0].cols[1].Text);

            var auditRecordId1 = auditResponseData.GridData[0].Id;

            auditListPage
                .WaitForAuditListPageToLoadFromReferenceDataPage("careprovidercontractscheme", "iframe_careprovidercontractscheme")
                .ValidateCellText(1, 2, "Update")
                .ValidateCellText(2, 2, "Update")
                .ValidateCellText(3, 2, "Create");

            auditListPage
                .WaitForAuditListPageToLoadFromReferenceDataPage("careprovidercontractscheme", "iframe_careprovidercontractscheme")
                .ValidateRecordPresent(auditRecordId1)
                .ClickOnAuditRecord(auditRecordId1);

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Update")
                .ValidateChangedBy("ContractSchemes User1")
                .ValidateFieldNameCellText(1, "Inactive")
                .ValidateOldValueCellText(1, "Yes")
                .ValidateNewValueCellText(1, "No")

                .TapCloseButton();

            var auditSearch2 = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 1, //create operation
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = contractSchemeId[0].ToString(),
                ParentTypeName = "careprovidercontractscheme",
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
            Assert.AreEqual("Create", auditResponseData2.GridData[0].cols[0].Text);
            Assert.AreEqual("ContractSchemes User1", auditResponseData2.GridData[0].cols[1].Text);
            var createdOnDate = auditResponseData2.GridData[0].cols[2].Text;

            var auditRecordId2 = auditResponseData2.GridData[0].Id;

            auditListPage
                .WaitForAuditListPageToLoadFromReferenceDataPage("careprovidercontractscheme", "iframe_careprovidercontractscheme")
                .ValidateRecordPresent(auditRecordId2)
                .ClickOnAuditRecord(auditRecordId2);

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Create")
                .ValidateChangedBy("ContractSchemes User1")
                .ValidateChangedOn(createdOnDate)
                .TapCloseButton();

            #endregion

            #region Step 13

            contractSchemeRecordPage
                .WaitForContractSchemeRecordPageToLoad(false)
                .ClickBackButton();

            contractSchemesPage
                .WaitForContractSchemesPageToLoad()
                .ValidateHeaderCellText(2, "Name")
                .ValidateHeaderCellSortOrdedAscending(2, "Name")
                .ValidateHeaderCellText(3, "Establishment")
                .ValidateHeaderCellText(4, "Funder")
                .ValidateHeaderCellText(5, "Finance Code")
                .ValidateHeaderCellText(6, "Code")
                .ValidateHeaderCellText(7, "Finance Code Updateable on Person Contract Service?")
                .ValidateHeaderCellText(8, "Charge Scheduled Care On Actuals?")
                .ValidateHeaderCellText(9, "Default All Person Contracts Enabled For Scheduled Bookings?")
                .ValidateHeaderCellText(10, "Sundries Apply?")
                .ValidateHeaderCellText(11, "Account Code Applies?");

            #endregion

            #region Step 14

            contractSchemesPage
                .InsertTextOnQuickSearch(contractSchemeName)
                .ClickQuickSearchButton()
                .ValidateRecordPresent(contractSchemeId[0].ToString(), true)

                .InsertTextOnQuickSearch(ProviderName)
                .ClickQuickSearchButton()
                .ValidateRecordPresent(contractSchemeId[0].ToString(), true)

                .InsertTextOnQuickSearch(ProviderName2)
                .ClickQuickSearchButton()
                .ValidateRecordPresent(contractSchemeId[0].ToString(), true);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4386

        [TestProperty("JiraIssueID", "ACC-4494")]
        [Description("Step(s) 15 to 22 from the original test method ACC-923")]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestMethod]
        public void ContractSchemes_UITestMethod004()
        {
            #region Provider

            var ProviderName = "Contract_Scheme_Provider";
            var ProviderId = commonMethodsDB.CreateProvider(ProviderName, _teamId, 13);

            #endregion

            #region create contract scheme

            var _contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var _contractSchemeName = "Contract_Scheme_" + _currentDateString;
            var _contractSchemeId = dbHelper.careProviderContractScheme.CreateCareProviderContractScheme(_teamId, _loginUserId, _businessUnitId, _contractSchemeName, new DateTime(2000, 1, 2), _contractSchemeCode, ProviderId, ProviderId);

            #endregion

            #region Finance Code Location

            var financeCodeLocationName_Organisation = "Organisation";
            var financeCodeLocationId_Organisation = dbHelper.careProviderFinanceCodeLocation.GetByName(financeCodeLocationName_Organisation)[0];

            var financeCodeLocationName_Constant = "Constant";
            var financeCodeLocationId_Constant = dbHelper.careProviderFinanceCodeLocation.GetByName(financeCodeLocationName_Constant)[0];

            var financeCodeLocationName_Team = "Team";
            var financeCodeLocationId_Team = dbHelper.careProviderFinanceCodeLocation.GetByName(financeCodeLocationName_Team)[0];

            var financeCodeLocationName_Service_ServiceDetail = "Service / Service Detail or Booking Type";
            var financeCodeLocationId_Service_ServiceDetail = dbHelper.careProviderFinanceCodeLocation.GetByName(financeCodeLocationName_Service_ServiceDetail)[0];

            #endregion

            #region Step 15

            loginPage
                .GoToLoginPage()
                .Login(_loginUserName, "Passw0rd_!");

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
                .InsertTextOnQuickSearch(_contractSchemeName)
                .ClickQuickSearchButton()
                .OpenContractSchemeRecord(_contractSchemeId.ToString());

            contractSchemeRecordPage
                .WaitForContractSchemeRecordPageToLoad()
                .NavigateToFinanceCodeMappingsTab();

            financeCodeMappingsPage
                .WaitForFinanceCodeMappingsPageToLoad()
                .ClickNewRecordButton();

            financeCodeMappingRecordPage
                .WaitForFinanceCodeMappingRecordPageToLoad()
                .ValidateAllFieldsVisible()
                .ValidateContractSchemeMandatoryFieldVisibility(true)
                .ValidateResponsibleTeamMandatoryFieldVisibility(true)
                .ValidatePositionNumberMandatoryFieldVisibility(true)
                .ValidateInactiveMandatoryFieldVisibility(true)
                .ValidateLevel1LocationMandatoryFieldVisibility(true);

            #endregion

            #region Step 16

            financeCodeMappingRecordPage
                .ValidateContractSchemeLinkText(_contractSchemeName)
                .ValidatePositionNumberText("1")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateInactive_OptionIsCheckedOrNot(false);

            #endregion

            #region Step 17 & 18

            financeCodeMappingRecordPage
                .ClickLevel1LocationLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(financeCodeLocationName_Constant)
                .TapSearchButton()
                .SelectResultElement(financeCodeLocationId_Constant.ToString());

            financeCodeMappingRecordPage
                .WaitForFinanceCodeMappingRecordPageToLoad()
                .ValidateLevel1ConstantIsVisible(true)
                .ValidateLevel1ConstantMandatoryFieldVisibility(false)
                .ValidateLevel1ConstantFieldMaximumLimitText("25")
                .ValidateLevel2LocationIsVisible(false)
                .ClickLevel1LocationLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(financeCodeLocationName_Organisation)
                .TapSearchButton()
                .SelectResultElement(financeCodeLocationId_Organisation.ToString());

            financeCodeMappingRecordPage
                .WaitForFinanceCodeMappingRecordPageToLoad()
                .ValidateLevel1ConstantIsVisible(false)
                .ValidateLevel2LocationIsVisible(true)
                .ValidateLevel2LocationMandatoryFieldVisibility(false)
                .ClickLevel2LocationLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(financeCodeLocationName_Constant)
                .TapSearchButton()
                .SelectResultElement(financeCodeLocationId_Constant.ToString());

            financeCodeMappingRecordPage
                .WaitForFinanceCodeMappingRecordPageToLoad()
                .ValidateLevel2ConstantIsVisible(true)
                .ValidateLevel2ConstantMandatoryFieldVisibility(false)
                .ValidateLevel2ConstantFieldMaximumLimitText("25")
                .ValidateLevel3LocationIsVisible(false)
                .ClickLevel2LocationLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(financeCodeLocationName_Team)
                .TapSearchButton()
                .SelectResultElement(financeCodeLocationId_Team.ToString());

            financeCodeMappingRecordPage
                .WaitForFinanceCodeMappingRecordPageToLoad()
                .ValidateLevel2ConstantIsVisible(false)
                .ValidateLevel3LocationIsVisible(true)
                .ValidateLevel3LocationMandatoryFieldVisibility(false)
                .ClickLevel3LocationLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(financeCodeLocationName_Constant)
                .TapSearchButton()
                .SelectResultElement(financeCodeLocationId_Constant.ToString());

            financeCodeMappingRecordPage
                .WaitForFinanceCodeMappingRecordPageToLoad()
                .ValidateLevel3ConstantIsVisible(true)
                .ValidateLevel3ConstantMandatoryFieldVisibility(false)
                .ValidateLevel3ConstantFieldMaximumLimitText("25")
                .ValidateLevel4LocationIsVisible(false)
                .ClickLevel3LocationLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(financeCodeLocationName_Service_ServiceDetail)
                .TapSearchButton()
                .SelectResultElement(financeCodeLocationId_Service_ServiceDetail.ToString());

            financeCodeMappingRecordPage
                .WaitForFinanceCodeMappingRecordPageToLoad()
                .ValidateLevel3ConstantIsVisible(false)
                .ValidateLevel4LocationIsVisible(true)
                .ValidateLevel4LocationMandatoryFieldVisibility(false)
                .ClickLevel4LocationLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(financeCodeLocationName_Constant)
                .TapSearchButton()
                .SelectResultElement(financeCodeLocationId_Constant.ToString());

            financeCodeMappingRecordPage
                .WaitForFinanceCodeMappingRecordPageToLoad()
                .ValidateLevel4ConstantIsVisible(true)
                .ValidateLevel4ConstantMandatoryFieldVisibility(false)
                .ValidateLevel4ConstantFieldMaximumLimitText("25")
                .ClickLevel4LocationLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(financeCodeLocationName_Team)
                .TapSearchButton()
                .SelectResultElement(financeCodeLocationId_Team.ToString());

            financeCodeMappingRecordPage
                .WaitForFinanceCodeMappingRecordPageToLoad()
                .ValidateLevel4ConstantIsVisible(false);

            #endregion

            #region Step 19

            financeCodeMappingRecordPage
                .ClickContractSchemeRemoveButton()
                .ClickResponsibleTeamRemoveButton()
                .ClickSaveButton()

                .ValidateFormErrorNotificationMessageIsDisplayed("Some data is not correct. Please review the data in the Form.")
                .ValidateContractSchemeFieldErrorLabelText("Please fill out this field.")
                .ValidateResponsibleTeamFieldErrorLabelText("Please fill out this field.")
                .ValidateLevel1LocationFieldErrorLabelText("Please fill out this field.");

            #endregion

            #region Step 20

            financeCodeMappingRecordPage
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            financeCodeMappingsPage
                .WaitForFinanceCodeMappingsPageToLoad()
                .ClickNewRecordButton();

            financeCodeMappingRecordPage
                .WaitForFinanceCodeMappingRecordPageToLoad()
                .ClickLevel1LocationLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(financeCodeLocationName_Organisation)
                .TapSearchButton()
                .SelectResultElement(financeCodeLocationId_Organisation.ToString());

            financeCodeMappingRecordPage
                .WaitForFinanceCodeMappingRecordPageToLoad()
                .ClickLevel2LocationLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(financeCodeLocationName_Team)
                .TapSearchButton()
                .SelectResultElement(financeCodeLocationId_Team.ToString());

            financeCodeMappingRecordPage
                .WaitForFinanceCodeMappingRecordPageToLoad()
                .ClickLevel3LocationLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(financeCodeLocationName_Service_ServiceDetail)
                .TapSearchButton()
                .SelectResultElement(financeCodeLocationId_Service_ServiceDetail.ToString());

            financeCodeMappingRecordPage
                .WaitForFinanceCodeMappingRecordPageToLoad()
                .ClickLevel4LocationLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(financeCodeLocationName_Constant)
                .TapSearchButton()
                .SelectResultElement(financeCodeLocationId_Constant.ToString());

            financeCodeMappingRecordPage
                .WaitForFinanceCodeMappingRecordPageToLoad()
                .ClickSaveButton()
                .WaitForRecordToBeSaved()

                .ValidateContractSchemeLookupButtonIsDisabled(true)
                .ValidateResponsibleTeamLookupButtonIsDisabled(true)
                .ValidatePositionNumberFieldIsDisabled(true)
                .ValidateInactiveOptionsIsDisabled(false)
                .ValidateLevel1LocationLookupButtonIsDisabled(false)
                .ValidateLevel2LocationLookupButtonIsDisabled(false)
                .ValidateLevel3LocationLookupButtonIsDisabled(false)
                .ValidateLevel4LocationLookupButtonIsDisabled(false)
                .ValidateLevel4ConstantFieldIsDisabled(false);

            #endregion

            #region Step 21

            financeCodeMappingRecordPage
                .ValidateFinanceCodeMappingRecordTitle(_contractSchemeName + " \\ 1 \\ " + financeCodeLocationName_Organisation + " \\ " + financeCodeLocationName_Team + " \\ " + financeCodeLocationName_Service_ServiceDetail + " \\ " + financeCodeLocationName_Constant)
                .ClickBackButton();

            #endregion

            #region Step 22

            financeCodeMappingsPage
                .WaitForFinanceCodeMappingsPageToLoad()
                .ClickNewRecordButton();

            financeCodeMappingRecordPage
                .WaitForFinanceCodeMappingRecordPageToLoad()
                .ValidatePositionNumberText("2")
                .ValidatePositionNumberFieldIsDisabled(true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4495")]
        [Description("Step(s) 23 to 27 from the original test method ACC-923")]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestMethod]
        public void ContractSchemes_UITestMethod005()
        {
            #region Provider

            var ProviderName = "Contract_Scheme_Provider";
            var ProviderId = commonMethodsDB.CreateProvider(ProviderName, _teamId, 13);

            #endregion

            #region create contract scheme

            int code = dbHelper.careProviderContractScheme.GetHighestCode();
            while (dbHelper.careProviderContractScheme.GetByCode(code).Any())
                code = code + 1;

            var _contractSchemeName = "Contract_Scheme_" + _currentDateString;
            var _contractSchemeId = dbHelper.careProviderContractScheme.CreateCareProviderContractScheme(_teamId, _loginUserId, _businessUnitId, _contractSchemeName, new DateTime(2000, 1, 2), code, ProviderId, ProviderId);

            #endregion

            #region Finance Code Location

            var financeCodeLocationName_Organisation = "Organisation";
            var financeCodeLocationId_Organisation = dbHelper.careProviderFinanceCodeLocation.GetByName(financeCodeLocationName_Organisation)[0];

            var financeCodeLocationName_Constant = "Constant";
            var financeCodeLocationId_Constant = dbHelper.careProviderFinanceCodeLocation.GetByName(financeCodeLocationName_Constant)[0];

            var financeCodeLocationName_Team = "Team";
            var financeCodeLocationId_Team = dbHelper.careProviderFinanceCodeLocation.GetByName(financeCodeLocationName_Team)[0];

            var financeCodeLocationName_Service_ServiceDetail = "Service / Service Detail or Booking Type";
            var financeCodeLocationId_Service_ServiceDetail = dbHelper.careProviderFinanceCodeLocation.GetByName(financeCodeLocationName_Service_ServiceDetail)[0];

            #endregion

            #region Finance Code Mapping

            var FinanceCodeMappingId1 = dbHelper.careProviderFinanceCodeMapping.CreateCareProviderFinanceCodeMapping(_teamId, _contractSchemeId, financeCodeLocationId_Organisation, financeCodeLocationId_Constant);
            var FinanceCodeMappingId2 = dbHelper.careProviderFinanceCodeMapping.CreateCareProviderFinanceCodeMapping(_teamId, _contractSchemeId, financeCodeLocationId_Constant, financeCodeLocationId_Team);
            var FinanceCodeMappingId3 = dbHelper.careProviderFinanceCodeMapping.CreateCareProviderFinanceCodeMapping(_teamId, _contractSchemeId, financeCodeLocationId_Team, financeCodeLocationId_Service_ServiceDetail);
            var FinanceCodeMappingId4 = dbHelper.careProviderFinanceCodeMapping.CreateCareProviderFinanceCodeMapping(_teamId, _contractSchemeId, financeCodeLocationId_Service_ServiceDetail, financeCodeLocationId_Organisation);
            var FinanceCodeMappingId5 = dbHelper.careProviderFinanceCodeMapping.CreateCareProviderFinanceCodeMapping(_teamId, _contractSchemeId, financeCodeLocationId_Organisation, financeCodeLocationId_Constant);
            var FinanceCodeMappingId6 = dbHelper.careProviderFinanceCodeMapping.CreateCareProviderFinanceCodeMapping(_teamId, _contractSchemeId, financeCodeLocationId_Constant, financeCodeLocationId_Team, financeCodeLocationId_Service_ServiceDetail);
            var FinanceCodeMappingId7 = dbHelper.careProviderFinanceCodeMapping.CreateCareProviderFinanceCodeMapping(_teamId, _contractSchemeId, financeCodeLocationId_Team, financeCodeLocationId_Service_ServiceDetail, financeCodeLocationId_Organisation);
            var FinanceCodeMappingId8 = dbHelper.careProviderFinanceCodeMapping.CreateCareProviderFinanceCodeMapping(_teamId, _contractSchemeId, financeCodeLocationId_Service_ServiceDetail, financeCodeLocationId_Organisation, financeCodeLocationId_Team, financeCodeLocationId_Constant);
            var FinanceCodeMappingId9 = dbHelper.careProviderFinanceCodeMapping.CreateCareProviderFinanceCodeMapping(_teamId, _contractSchemeId, financeCodeLocationId_Organisation, financeCodeLocationId_Team, financeCodeLocationId_Service_ServiceDetail, financeCodeLocationId_Organisation);

            #endregion

            #region Step 23

            loginPage
                .GoToLoginPage()
                .Login(_loginUserName, "Passw0rd_!");

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
                .InsertTextOnQuickSearch(_contractSchemeName)
                .ClickQuickSearchButton()
                .OpenContractSchemeRecord(_contractSchemeId.ToString());

            contractSchemeRecordPage
                .WaitForContractSchemeRecordPageToLoad()
                .NavigateToFinanceCodeMappingsTab();

            financeCodeMappingsPage
                .WaitForFinanceCodeMappingsPageToLoad()

                .ValidateRecordPresent(FinanceCodeMappingId1.ToString(), true)
                .ValidateRecordCellContent(FinanceCodeMappingId1.ToString(), 2, "1")
                .ValidateRecordCellContent(FinanceCodeMappingId1.ToString(), 3, financeCodeLocationName_Organisation)
                .ValidateRecordCellContent(FinanceCodeMappingId1.ToString(), 5, financeCodeLocationName_Constant)

                .ValidateRecordPresent(FinanceCodeMappingId2.ToString(), true)
                .ValidateRecordCellContent(FinanceCodeMappingId2.ToString(), 2, "2")
                .ValidateRecordCellContent(FinanceCodeMappingId2.ToString(), 3, financeCodeLocationName_Constant)
                .ValidateRecordCellContent(FinanceCodeMappingId2.ToString(), 5, financeCodeLocationName_Team)

                .ValidateRecordPresent(FinanceCodeMappingId3.ToString(), true)
                .ValidateRecordCellContent(FinanceCodeMappingId3.ToString(), 2, "3")
                .ValidateRecordCellContent(FinanceCodeMappingId3.ToString(), 3, financeCodeLocationName_Team)
                .ValidateRecordCellContent(FinanceCodeMappingId3.ToString(), 5, financeCodeLocationName_Service_ServiceDetail)

                .ValidateRecordPresent(FinanceCodeMappingId4.ToString(), true)
                .ValidateRecordCellContent(FinanceCodeMappingId4.ToString(), 2, "4")
                .ValidateRecordCellContent(FinanceCodeMappingId4.ToString(), 3, financeCodeLocationName_Service_ServiceDetail)
                .ValidateRecordCellContent(FinanceCodeMappingId4.ToString(), 5, financeCodeLocationName_Organisation)

                .ValidateRecordPresent(FinanceCodeMappingId5.ToString(), true)
                .ValidateRecordCellContent(FinanceCodeMappingId5.ToString(), 2, "5")
                .ValidateRecordCellContent(FinanceCodeMappingId5.ToString(), 3, financeCodeLocationName_Organisation)
                .ValidateRecordCellContent(FinanceCodeMappingId5.ToString(), 5, financeCodeLocationName_Constant)

                .ValidateRecordPresent(FinanceCodeMappingId6.ToString(), true)
                .ValidateRecordCellContent(FinanceCodeMappingId6.ToString(), 2, "6")
                .ValidateRecordCellContent(FinanceCodeMappingId6.ToString(), 3, financeCodeLocationName_Constant)
                .ValidateRecordCellContent(FinanceCodeMappingId6.ToString(), 5, financeCodeLocationName_Team)
                .ValidateRecordCellContent(FinanceCodeMappingId6.ToString(), 7, financeCodeLocationName_Service_ServiceDetail)

                .ValidateRecordPresent(FinanceCodeMappingId7.ToString(), true)
                .ValidateRecordCellContent(FinanceCodeMappingId7.ToString(), 2, "7")
                .ValidateRecordCellContent(FinanceCodeMappingId7.ToString(), 3, financeCodeLocationName_Team)
                .ValidateRecordCellContent(FinanceCodeMappingId7.ToString(), 5, financeCodeLocationName_Service_ServiceDetail)
                .ValidateRecordCellContent(FinanceCodeMappingId7.ToString(), 7, financeCodeLocationName_Organisation)

                .ValidateRecordPresent(FinanceCodeMappingId8.ToString(), true)
                .ValidateRecordCellContent(FinanceCodeMappingId8.ToString(), 2, "8")
                .ValidateRecordCellContent(FinanceCodeMappingId8.ToString(), 3, financeCodeLocationName_Service_ServiceDetail)
                .ValidateRecordCellContent(FinanceCodeMappingId8.ToString(), 5, financeCodeLocationName_Organisation)
                .ValidateRecordCellContent(FinanceCodeMappingId8.ToString(), 7, financeCodeLocationName_Team)
                .ValidateRecordCellContent(FinanceCodeMappingId8.ToString(), 9, financeCodeLocationName_Constant)

                .ValidateRecordPresent(FinanceCodeMappingId9.ToString(), true)
                .ValidateRecordCellContent(FinanceCodeMappingId9.ToString(), 2, "9")
                .ValidateRecordCellContent(FinanceCodeMappingId9.ToString(), 3, financeCodeLocationName_Organisation)
                .ValidateRecordCellContent(FinanceCodeMappingId9.ToString(), 5, financeCodeLocationName_Team)
                .ValidateRecordCellContent(FinanceCodeMappingId9.ToString(), 7, financeCodeLocationName_Service_ServiceDetail)
                .ValidateRecordCellContent(FinanceCodeMappingId9.ToString(), 9, financeCodeLocationName_Organisation)

                .ClickNewRecordButton();

            financeCodeMappingRecordPage
                .WaitForFinanceCodeMappingRecordPageToLoad();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Reached maximum mapping entries - 9. Please update existing ones or delete one of them")
                .TapCloseButton();

            financeCodeMappingRecordPage
                .WaitForFinanceCodeMappingRecordPageToLoad()
                .ClickBackButton();

            #endregion

            #region Step 27

            financeCodeMappingsPage
                .WaitForFinanceCodeMappingsPageToLoad()
                .ValidateRecordRowPosition(1, FinanceCodeMappingId1.ToString())
                .ValidateRecordRowPosition(2, FinanceCodeMappingId2.ToString())
                .ValidateRecordRowPosition(3, FinanceCodeMappingId3.ToString())
                .ValidateRecordRowPosition(4, FinanceCodeMappingId4.ToString())
                .ValidateRecordRowPosition(5, FinanceCodeMappingId5.ToString())
                .ValidateRecordRowPosition(6, FinanceCodeMappingId6.ToString())
                .ValidateRecordRowPosition(7, FinanceCodeMappingId7.ToString())
                .ValidateRecordRowPosition(8, FinanceCodeMappingId8.ToString())
                .ValidateRecordRowPosition(9, FinanceCodeMappingId9.ToString())

                .ValidateHeaderCellText(2, "Position Number")
                .ValidateHeaderCellSortOrdedAscending(2, "Position Number")
                .ValidateHeaderCellText(3, "Level 1 Location")
                .ValidateHeaderCellText(4, "Level 1 Constant")

                .ValidateHeaderCellText(5, "Level 2 Location")
                .ValidateHeaderCellText(6, "Level 2 Constant")

                .ValidateHeaderCellText(7, "Level 3 Location")
                .ValidateHeaderCellText(8, "Level 3 Constant")

                .ValidateHeaderCellText(9, "Level 4 Location")
                .ValidateHeaderCellText(10, "Level 4 Constant");

            #endregion

            #region Step 24

            financeCodeMappingsPage
                .WaitForFinanceCodeMappingsPageToLoad()
                .OpenRecord(FinanceCodeMappingId7.ToString());

            financeCodeMappingRecordPage
                .WaitForFinanceCodeMappingRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("By deleting this record, all records with higher Positions will move up one Position.  As an alternative, you can update this Position’s details. Please select Cancel to discard the deletion or click OK to continue with the deletion")
                .TapOKButton();

            financeCodeMappingsPage
                .WaitForFinanceCodeMappingsPageToLoad()
                .ValidateRecordPresent(FinanceCodeMappingId7.ToString(), false)

                .ValidateRecordPresent(FinanceCodeMappingId8.ToString(), true)
                .ValidateRecordCellContent(FinanceCodeMappingId8.ToString(), 2, "7")
                .ValidateRecordCellContent(FinanceCodeMappingId8.ToString(), 3, financeCodeLocationName_Service_ServiceDetail)
                .ValidateRecordCellContent(FinanceCodeMappingId8.ToString(), 5, financeCodeLocationName_Organisation)
                .ValidateRecordCellContent(FinanceCodeMappingId8.ToString(), 7, financeCodeLocationName_Team)
                .ValidateRecordCellContent(FinanceCodeMappingId8.ToString(), 9, financeCodeLocationName_Constant)

                .ValidateRecordPresent(FinanceCodeMappingId9.ToString(), true)
                .ValidateRecordCellContent(FinanceCodeMappingId9.ToString(), 2, "8")
                .ValidateRecordCellContent(FinanceCodeMappingId9.ToString(), 3, financeCodeLocationName_Organisation)
                .ValidateRecordCellContent(FinanceCodeMappingId9.ToString(), 5, financeCodeLocationName_Team)
                .ValidateRecordCellContent(FinanceCodeMappingId9.ToString(), 7, financeCodeLocationName_Service_ServiceDetail)
                .ValidateRecordCellContent(FinanceCodeMappingId9.ToString(), 9, financeCodeLocationName_Organisation)

                .SelectFinanceCodeMappingRecord(FinanceCodeMappingId2.ToString())
                .ClickDeleteRecordButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("By deleting this record, all records with higher Positions will move up one Position.  As an alternative, you can update this Position’s details. Please select Cancel to discard the deletion or click OK to continue with the deletion")
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("1 item(s) deleted.")
                .TapOKButton();

            financeCodeMappingsPage
                .WaitForFinanceCodeMappingsPageToLoad()

                .ValidateRecordPresent(FinanceCodeMappingId1.ToString(), true)
                .ValidateRecordCellContent(FinanceCodeMappingId1.ToString(), 2, "1")
                .ValidateRecordCellContent(FinanceCodeMappingId1.ToString(), 3, financeCodeLocationName_Organisation)
                .ValidateRecordCellContent(FinanceCodeMappingId1.ToString(), 5, financeCodeLocationName_Constant)

                .ValidateRecordPresent(FinanceCodeMappingId2.ToString(), false)

                .ValidateRecordPresent(FinanceCodeMappingId3.ToString(), true)
                .ValidateRecordCellContent(FinanceCodeMappingId3.ToString(), 2, "2")
                .ValidateRecordCellContent(FinanceCodeMappingId3.ToString(), 3, financeCodeLocationName_Team)
                .ValidateRecordCellContent(FinanceCodeMappingId3.ToString(), 5, financeCodeLocationName_Service_ServiceDetail)

                .ValidateRecordPresent(FinanceCodeMappingId4.ToString(), true)
                .ValidateRecordCellContent(FinanceCodeMappingId4.ToString(), 2, "3")
                .ValidateRecordCellContent(FinanceCodeMappingId4.ToString(), 3, financeCodeLocationName_Service_ServiceDetail)
                .ValidateRecordCellContent(FinanceCodeMappingId4.ToString(), 5, financeCodeLocationName_Organisation)

                .ValidateRecordPresent(FinanceCodeMappingId5.ToString(), true)
                .ValidateRecordCellContent(FinanceCodeMappingId5.ToString(), 2, "4")
                .ValidateRecordCellContent(FinanceCodeMappingId5.ToString(), 3, financeCodeLocationName_Organisation)
                .ValidateRecordCellContent(FinanceCodeMappingId5.ToString(), 5, financeCodeLocationName_Constant)

                .ValidateRecordPresent(FinanceCodeMappingId6.ToString(), true)
                .ValidateRecordCellContent(FinanceCodeMappingId6.ToString(), 2, "5")
                .ValidateRecordCellContent(FinanceCodeMappingId6.ToString(), 3, financeCodeLocationName_Constant)
                .ValidateRecordCellContent(FinanceCodeMappingId6.ToString(), 5, financeCodeLocationName_Team)
                .ValidateRecordCellContent(FinanceCodeMappingId6.ToString(), 7, financeCodeLocationName_Service_ServiceDetail)

                .ValidateRecordPresent(FinanceCodeMappingId7.ToString(), false)

                .ValidateRecordPresent(FinanceCodeMappingId8.ToString(), true)
                .ValidateRecordCellContent(FinanceCodeMappingId8.ToString(), 2, "6")
                .ValidateRecordCellContent(FinanceCodeMappingId8.ToString(), 3, financeCodeLocationName_Service_ServiceDetail)
                .ValidateRecordCellContent(FinanceCodeMappingId8.ToString(), 5, financeCodeLocationName_Organisation)
                .ValidateRecordCellContent(FinanceCodeMappingId8.ToString(), 7, financeCodeLocationName_Team)
                .ValidateRecordCellContent(FinanceCodeMappingId8.ToString(), 9, financeCodeLocationName_Constant)

                .ValidateRecordPresent(FinanceCodeMappingId9.ToString(), true)
                .ValidateRecordCellContent(FinanceCodeMappingId9.ToString(), 2, "7")
                .ValidateRecordCellContent(FinanceCodeMappingId9.ToString(), 3, financeCodeLocationName_Organisation)
                .ValidateRecordCellContent(FinanceCodeMappingId9.ToString(), 5, financeCodeLocationName_Team)
                .ValidateRecordCellContent(FinanceCodeMappingId9.ToString(), 7, financeCodeLocationName_Service_ServiceDetail)
                .ValidateRecordCellContent(FinanceCodeMappingId9.ToString(), 9, financeCodeLocationName_Organisation)
;
            #endregion

            #region Step 25

            financeCodeMappingsPage
                .WaitForFinanceCodeMappingsPageToLoad()
                .OpenRecord(FinanceCodeMappingId8.ToString());

            financeCodeMappingRecordPage
                .WaitForFinanceCodeMappingRecordPageToLoad()
                .ClickInactive_Option(true)
                .ClickSaveButton();

            System.Threading.Thread.Sleep(1000);

            financeCodeMappingRecordPage
                .ValidateContractSchemeLookupButtonIsDisabled(true)
                .ValidateResponsibleTeamLookupButtonIsDisabled(true)
                .ValidatePositionNumberFieldIsDisabled(true)
                .ValidateInactiveOptionsIsDisabled(true)
                .ValidateLevel1LocationLookupButtonIsDisabled(true)
                .ValidateLevel2LocationLookupButtonIsDisabled(true)
                .ValidateLevel3LocationLookupButtonIsDisabled(true)
                .ValidateLevel4LocationLookupButtonIsDisabled(true)
                .ValidateLevel4ConstantFieldIsDisabled(true);

            #endregion

            #region Step 26

            financeCodeMappingRecordPage
                 .NavigateToAuditPage();

            var auditSearch1 = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 2, //update operation
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = FinanceCodeMappingId8.ToString(),
                ParentTypeName = "careproviderfinancecodemapping",
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
            Assert.AreEqual("Update", auditResponseData.GridData[0].cols[0].Text);
            Assert.AreEqual("ContractSchemes User1", auditResponseData.GridData[0].cols[1].Text);

            var auditRecordId1 = auditResponseData.GridData[0].Id;

            auditListPage
                .WaitForAuditListPageToLoad("careproviderfinancecodemapping")
                .ValidateCellText(1, 2, "Update")
                .ValidateCellText(2, 2, "Update")
                .ValidateCellText(3, 2, "Update")
                .ValidateCellText(4, 2, "Create");

            auditListPage
                .WaitForAuditListPageToLoad("careproviderfinancecodemapping")
                .ValidateRecordPresent(auditRecordId1)
                .ClickOnAuditRecord(auditRecordId1);

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Update")
                .ValidateChangedBy("ContractSchemes User1")
                .ValidateFieldNameCellText(1, "Inactive")
                .ValidateOldValueCellText(1, "No")
                .ValidateNewValueCellText(1, "Yes")

                .TapCloseButton();

            var auditSearch2 = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 1, //create operation
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = FinanceCodeMappingId8.ToString(),
                ParentTypeName = "careproviderfinancecodemapping",
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
            Assert.AreEqual("Create", auditResponseData2.GridData[0].cols[0].Text);
            Assert.AreEqual(_defaultUserFullname, auditResponseData2.GridData[0].cols[1].Text);
            var createdOnDate = auditResponseData2.GridData[0].cols[2].Text;

            var auditRecordId2 = auditResponseData2.GridData[0].Id;

            auditListPage
                .WaitForAuditListPageToLoad("careproviderfinancecodemapping")
                .ValidateRecordPresent(auditRecordId2)
                .ClickOnAuditRecord(auditRecordId2);

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Create")
                .ValidateChangedBy(_defaultUserFullname)
                .ValidateChangedOn(createdOnDate)
                .TapCloseButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4496")]
        [Description("Step(s) 28 from the original test method ACC-923")]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestMethod]
        public void ContractSchemes_UITestMethod006()
        {
            #region Provider

            var ProviderName = "Contract_Scheme_Provider";
            var ProviderId = commonMethodsDB.CreateProvider(ProviderName, _teamId, 13);

            #endregion

            #region create contract scheme

            var code = dbHelper.careProviderContractScheme.GetHighestCode();
            while (dbHelper.careProviderContractScheme.GetByCode(code).Any())
                code = code + 1;

            var _contractSchemeName = "Contract_Scheme_" + _currentDateString;
            var _contractSchemeId = dbHelper.careProviderContractScheme.CreateCareProviderContractScheme(_teamId, _loginUserId, _businessUnitId, _contractSchemeName, new DateTime(2000, 1, 2), code, ProviderId, ProviderId);

            #endregion

            #region Finance Code Location

            var financeCodeLocationName_Organisation = "Organisation";
            var financeCodeLocationId_Organisation = dbHelper.careProviderFinanceCodeLocation.GetByName(financeCodeLocationName_Organisation)[0];

            var financeCodeLocationName_Team = "Team";
            var financeCodeLocationId_Team = dbHelper.careProviderFinanceCodeLocation.GetByName(financeCodeLocationName_Team)[0];

            #endregion

            #region Step 28

            loginPage
                .GoToLoginPage()
                .Login(_loginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Finance Code Mappings")
                .ClickSelectFilterFieldOption("1")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Contract Scheme")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Position Number")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Responsible Team")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Inactive")

                .ValidateSelectFilterFieldOptionIsPresent("1", "Level 1 Location")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Level 1 Constant")

                .ValidateSelectFilterFieldOptionIsPresent("1", "Level 2 Location")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Level 2 Constant")

                .ValidateSelectFilterFieldOptionIsPresent("1", "Level 3 Location")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Level 3 Constant")

                .ValidateSelectFilterFieldOptionIsPresent("1", "Level 4 Location")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Level 4 Constant")

                .ClickDeleteButton()

                .ClickSearchButton()
                .WaitForResultsPageToLoad()

                .ClickNewRecordButton_ResultsPage();

            financeCodeMappingRecordPage
                .WaitForFinanceCodeMappingRecordPageToLoad(true)
                .ClickContractSchemeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup Records")
                .TypeSearchQuery(_contractSchemeName)
                .TapSearchButton()
                .SelectResultElement(_contractSchemeId.ToString());

            financeCodeMappingRecordPage
                .WaitForFinanceCodeMappingRecordPageToLoad(true)
                .ClickLevel1LocationLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup Records")
                .TypeSearchQuery(financeCodeLocationName_Organisation)
                .TapSearchButton()
                .SelectResultElement(financeCodeLocationId_Organisation.ToString());

            financeCodeMappingRecordPage
                .WaitForFinanceCodeMappingRecordPageToLoad(true)
                .ClickSaveButton()
                .WaitForRecordToBeSaved()

                .ValidateFinanceCodeMappingRecordTitle(_contractSchemeName + " \\ 1 \\ " + financeCodeLocationName_Organisation)
                .ValidateContractSchemeLookupButtonIsDisabled(true)
                .ValidateContractSchemeLinkText(_contractSchemeName)
                .ValidateResponsibleTeamLookupButtonIsDisabled(true)
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidatePositionNumberFieldIsDisabled(true)
                .ValidatePositionNumberText("1")
                .ValidateInactiveOptionsIsDisabled(false)
                .ValidateLevel1LocationLookupButtonIsDisabled(false)
                .ValidateLevel1LocationLinkText(financeCodeLocationName_Organisation)

                .ClickLevel1LocationLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup Records")
                .TypeSearchQuery(financeCodeLocationName_Team)
                .TapSearchButton()
                .SelectResultElement(financeCodeLocationId_Team.ToString());

            financeCodeMappingRecordPage
                .WaitForFinanceCodeMappingRecordPageToLoad(true)
                .ClickSaveButton();

            System.Threading.Thread.Sleep(1000);

            financeCodeMappingRecordPage
                .ValidateFinanceCodeMappingRecordTitle(_contractSchemeName + " \\ 1 \\ " + financeCodeLocationName_Team)
                .ValidateContractSchemeLookupButtonIsDisabled(true)
                .ValidateContractSchemeLinkText(_contractSchemeName)
                .ValidateResponsibleTeamLookupButtonIsDisabled(true)
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidatePositionNumberFieldIsDisabled(true)
                .ValidatePositionNumberText("1")
                .ValidateInactiveOptionsIsDisabled(false)

                .ValidateLevel1LocationLookupButtonIsDisabled(false)
                .ValidateLevel1LocationLinkText(financeCodeLocationName_Team);

            #endregion

        }

        #endregion

    }
}

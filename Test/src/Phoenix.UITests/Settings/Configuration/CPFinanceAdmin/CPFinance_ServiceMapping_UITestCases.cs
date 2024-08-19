using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Configuration.CPFinanceAdmin
{
    /// <summary>
    /// This class contains Automated UI test scripts for CP Finance > Service Mapping
    /// </summary>
    [TestClass]
    public class CPFinance_ServiceMapping_UITestCases : FunctionalTest
    {
        #region Properties

        private string _environmentName;
        private string _tenantName;
        private Guid _authenticationProviderId;
        private Guid _languageId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private string _teamName;
        private Guid _defaultLoginUserID;
        public Guid environmentid;
        private string _loginUsername;
        private string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");

        #endregion

        [TestInitialize()]
        public void TestSetup()
        {
            try
            {
                #region Environment Name

                _environmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                _tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];

                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);

                #endregion

                #region Authentication

                _authenticationProviderId = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").First();

                #endregion Authentication

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CPServiceMappingBU");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                _teamName = "CPServiceMapping";
                _teamId = commonMethodsDB.CreateTeam(_teamName, null, _businessUnitId, "07410", "CPServiceMapping@careworkstempmail.com", _teamName, "020 123456");

                #endregion

                #region Create default system user

                _loginUsername = "CPServiceMappingUser";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUsername, "CPServiceMapping", "User", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationProviderId);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                this.ShutDownAllProcesses();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-4439

        [TestProperty("JiraIssueID", "ACC-4440")]
        [Description("TC4 - Test for Services / Services Detail / Service Mappings")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen1", "Services")]
        [TestProperty("Screen2", "Service Mappings")]
        [TestProperty("Screen3", "Services Details")]
        [TestProperty("Screen4", "Advanced Search")]
        public void CPFinance_ServiceMapping_UITestMethod001()
        {
            #region Step 25 to Step 36

            /*
             * Step 26, 31 are not applicable when Scheduled Service? = No. Applicable in and covered in ACC-4480 (where Scheduled Service? = Yes)
             */

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var Code = dbHelper.careProviderService.GetCodeNumber();
            int newCode = Code.Count();

            while (dbHelper.careProviderService.GetByCode(newCode).Any())
            {
                newCode = newCode + 1;
            }

            var careProviderServiceId1 = commonMethodsDB.CreateCareProviderService(_teamId, "AAServiceA_" + currentTimeString, new DateTime(2020, 1, 1), newCode);


            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            cpFinanceAdminPage
                .WaitForCPFinanceAdminPageToLoad()
                .ClickPersonContractsAreaButton()
                .ClickServicesButton();

            servicesPage
                .WaitForServicesPageToLoad()
                .OpenRecord(careProviderServiceId1.ToString());

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var serviceDetailCode = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail();
            int newServiceDetailCode = serviceDetailCode.Count();

            while (dbHelper.careProviderServiceDetail.GetByCode(newServiceDetailCode).Any())
            {
                newServiceDetailCode = newServiceDetailCode + 1;
            }

            var cpServiceDetailId1 = dbHelper.careProviderServiceDetail.CreateCareProviderServiceDetail(_teamId, "AASMap_A_" + currentTimeString, newServiceDetailCode, newServiceDetailCode, new DateTime(2020, 1, 1), null);//Start date < current date & End date not recorded

            #region Finance Code Location

            var serviceAndServiceDetailFinanceCodeLocationId = dbHelper.careProviderFinanceCodeLocation.GetByName("Service / Service Detail or Booking Type")[0];

            #endregion

            #region Finance Code

            var financeCodeId = commonMethodsDB.CreateCareProviderFinanceCode(_teamId, serviceAndServiceDetailFinanceCodeLocationId, "ACC_4440", "FC_ACC_4440");

            #endregion

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("careproviderservice")
                .ClickOnExpandIcon();

            serviceRecordPage
                .WaitForServiceRecordPageToLoad()
                .NavigateToServiceMappingsTab();

            cpServiceMappingsPage
                .WaitForCPServiceMappingsPageToLoad()
                .ClickNewRecordButton();

            cpServiceMappingRecordPage
                .WaitForCPServiceMappingRecordPageToLoad()
                .ValidateServiceFieldLookupButtonIsDisplayed(true)
                .ValidateMandatoryFieldIsDisplayed("Service")
                .ValidateServiceDetailLookupButtonIsDisplayed(true)
                .ValidateMandatoryFieldIsDisplayed("Service Detail")
                .ValidateFinanceCodeLookupButtonIsDisplayed(true)
                .ValidateResponsibleTeamLookupButtonIsDisplayed(true)
                .ValidateMandatoryFieldIsDisplayed("Responsible Team")
                .ValidateNoteText_TextFieldIsDisplayed(true);

            cpServiceMappingRecordPage
                .WaitForCPServiceMappingRecordPageToLoad()
                .ValidateServiceLinkText("AAServiceA_" + currentTimeString)
                .ValidateResponsibleTeamLinkText(_teamName);

            cpServiceMappingRecordPage
                .WaitForCPServiceMappingRecordPageToLoad()
                .ClickResponsibleTeamClearButton()
                .ClickSaveButton()
                .WaitForCPServiceMappingRecordPageToLoad()
                .ValidateServiceDetailsMandatoryFieldNotificationMessageIsDisplayed("Please fill out this field.")
                .ValidateResponsibleTeamMandatoryFieldNotificationMessageIsDisplayed("Please fill out this field.")
                .ValidateFormErrorNotificationMessageIsDisplayed("Some data is not correct. Please review the data in the Form.");

            cpServiceMappingRecordPage
                .WaitForCPServiceMappingRecordPageToLoad()
                .ClickResponsibleTeamLookupButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .SelectLookIn("Lookup View")
                 .SearchAndSelectRecord(_teamName, _teamId);

            cpServiceMappingRecordPage
                .WaitForCPServiceMappingRecordPageToLoad()
                .ClickServiceDetailLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("AASMap_A_" + currentTimeString, cpServiceDetailId1);

            cpServiceMappingRecordPage
                .WaitForCPServiceMappingRecordPageToLoad()
                .ClickFinanceCodeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("FC_ACC_4440", financeCodeId);

            cpServiceMappingRecordPage
                .WaitForCPServiceMappingRecordPageToLoad()
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            cpServiceMappingRecordPage
                .WaitForCPServiceMappingRecordPageToLoad()
                .ValidateServiceLinkText("AAServiceA_" + currentTimeString)
                .ValidateServiceDetailLinkText("AASMap_A_" + currentTimeString)
                .ValidateFinanceCodeLinkText("FC_ACC_4440 \\ ACC_4440 \\ Service / Service Detail or Booking Type")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateNoteText_Text("")
                .ValidateResponsibleTeamLookupButtonIsDisabled(true)
                .ValidateServiceMappingRecordTitle("AAServiceA_" + currentTimeString + " \\ " + "AASMap_A_" + currentTimeString)
                .ValidateServiceLookupButtonIsDisabled(true);

            var CPServiceMappings = dbHelper.careProviderServiceMapping.GetByCareProviderServiceId(careProviderServiceId1);
            Assert.AreEqual(1, CPServiceMappings.Count);

            cpServiceMappingRecordPage
                .WaitForCPServiceMappingRecordPageToLoad()
                .ClickBackButton();

            cpServiceMappingsPage
                .WaitForCPServiceMappingsPageToLoad()
                .ClickNewRecordButton();

            cpServiceMappingRecordPage
                .WaitForCPServiceMappingRecordPageToLoad()
                .ClickServiceDetailLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("AASMap_A_" + currentTimeString, cpServiceDetailId1);

            cpServiceMappingRecordPage
                .WaitForCPServiceMappingRecordPageToLoad()
                .ClickFinanceCodeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("FC_ACC_4440", financeCodeId);

            cpServiceMappingRecordPage
                .WaitForCPServiceMappingRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("This record can’t be saved. Record with chosen Service and Service Details already exists.")
                .TapCloseButton();

            cpServiceMappingRecordPage
                .WaitForCPServiceMappingRecordPageToLoad()
                .ClickFinanceCodeClearButton()
                .ClickServiceDetailClearButton()
                .ClickBackButton();

            CPServiceMappings = dbHelper.careProviderServiceMapping.GetByCareProviderServiceId(careProviderServiceId1);
            Assert.AreEqual(1, CPServiceMappings.Count);

            cpServiceMappingsPage
                .WaitForCPServiceMappingsPageToLoad()
                .ResultsPageValidateHeaderCellSortDescendingOrder(2)
                .RecordsPageValidateHeaderCellText(2, "Service")
                .RecordsPageValidateHeaderCellText(3, "Service Detail")
                .RecordsPageValidateHeaderCellText(4, "Booking Type")
                .RecordsPageValidateHeaderCellText(5, "Finance Code")
                .RecordsPageValidateHeaderCellText(6, "Responsible Team")
                .RecordsPageValidateHeaderCellText(7, "Inactive")
                .RecordsPageValidateHeaderCellText(8, "Created By")
                .RecordsPageValidateHeaderCellText(9, "Created On")
                .RecordsPageValidateHeaderCellText(10, "Modified By")
                .RecordsPageValidateHeaderCellText(11, "Modified On");

            cpServiceMappingsPage
                .InsertTextOnQuickSearch("AAServiceA_" + currentTimeString)
                .ClickQuickSearchButton()
                .WaitForCPServiceMappingsPageToLoad()
                .ValidateRecordVisible(CPServiceMappings[0]);

            cpServiceMappingsPage
                .InsertTextOnQuickSearch("AASMap_A_" + currentTimeString)
                .ClickQuickSearchButton()
                .WaitForCPServiceMappingsPageToLoad()
                .ValidateRecordVisible(CPServiceMappings[0]);

            cpServiceMappingsPage
                .InsertTextOnQuickSearch("FC_ACC_4440 \\ ACC_4440 \\ Service / Service Detail or Booking Type")
                .ClickQuickSearchButton()
                .WaitForCPServiceMappingsPageToLoad()
                .ValidateRecordVisible(CPServiceMappings[0]);

            cpServiceMappingsPage
                .InsertTextOnQuickSearch(_teamName)
                .ClickQuickSearchButton()
                .WaitForCPServiceMappingsPageToLoad()
                .ValidateRecordVisible(CPServiceMappings[0]);

            serviceRecordPage
                .WaitForServiceRecordPageToLoad()
                .NavigateToDetailsTab()
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Related record exists in Service Mapping. Please delete related records before deleting record in Service.")
                .TapCloseButton();

            serviceRecordPage
                .WaitForServiceRecordPageToLoad()
                .ClickBackButton();

            servicesPage
                .WaitForServicesPageToLoad()
                .InsertTextOnQuickSearch("AAServiceA_" + currentTimeString)
                .ClickQuickSearchButton()
                .SelectServicesRecord(careProviderServiceId1.ToString())
                .ClickDeleteRecordButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("0 item(s) deleted. 1 item(s) not deleted.")
                .TapOKButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            cpFinanceAdminPage
                .WaitForCPFinanceAdminPageToLoad()
                .ClickPersonContractsAreaButton()
                .ClickServicesDetailButton();

            serviceDetailsPage
                .WaitForServiceDetailsPageToLoad()
                .InsertTextOnQuickSearch("AASMap_A_" + currentTimeString)
                .ClickQuickSearchButton()
                .OpenRecord(cpServiceDetailId1);

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("careproviderservicedetail")
                .ClickOnExpandIcon();

            serviceDetailsRecordPage
                .WaitForServiceDetailsRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Related record exists in Service Mapping. Please delete related records before deleting record in Service Detail.")
                .TapCloseButton();

            serviceDetailsRecordPage
                .WaitForServiceDetailsRecordPageToLoad()
                .ClickBackButton();

            serviceDetailsPage
                .WaitForServiceDetailsPageToLoad()
                .InsertTextOnQuickSearch("AASMap_A_" + currentTimeString)
                .ClickQuickSearchButton()
                .SelectServiceDetailsRecord(cpServiceDetailId1)
                .ClickDeleteRecordButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("0 item(s) deleted. 1 item(s) not deleted.")
                .TapOKButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Services Mapping")
                .SelectFilter("1", "Title")
                .SelectOperator("1", "Equals")
                .InsertRuleValueText("1", "AAServiceA_" + currentTimeString + " \\ " + "AASMap_A_" + currentTimeString)
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(CPServiceMappings[0].ToString());

            advanceSearchPage
                .ValidateNewRecordButton_ResultsPageIsNotPresent(true);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4480")]
        [Description("TC5 - Test for Services / Services Detail / Service Mappings")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen1", "Services")]
        [TestProperty("Screen2", "Service Mappings")]
        [TestProperty("Screen3", "Advanced Search")]
        public void CPFinance_ServiceMapping_UITestMethod002()
        {
            #region Step 25 to Step 36

            /*
             * Step 30, 33, 35, 36 are not applicable when Scheduled Service? = Yes. Applicable and covered in ACC-4440 (Scheduled Service? = No)
             */

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var Code = dbHelper.careProviderService.GetCodeNumber();
            int newCode = Code.Count();

            while (dbHelper.careProviderService.GetByCode(newCode).Any())
            {
                newCode = newCode + 1;
            }

            var careProviderServiceId1 = commonMethodsDB.CreateCareProviderService(_teamId, "AAServiceA2_" + currentTimeString, new DateTime(2020, 1, 1), newCode, null, true);


            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            cpFinanceAdminPage
                .WaitForCPFinanceAdminPageToLoad()
                .ClickPersonContractsAreaButton()
                .ClickServicesButton();

            servicesPage
                .WaitForServicesPageToLoad()
                .OpenRecord(careProviderServiceId1.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("careproviderservice")
                .ClickOnExpandIcon();


            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            //Create Booking Type and Service Mapping            
            var bookingTypeId = commonMethodsDB.CreateBookingType("BT4439_" + currentTimeString, 5, 60, new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0), 1, false, null, null, null, 3);

            #region Finance Code Location

            var serviceAndServiceDetailFinanceCodeLocationId = dbHelper.careProviderFinanceCodeLocation.GetByName("Service / Service Detail or Booking Type")[0];

            #endregion

            #region Finance Code

            var financeCodeId = commonMethodsDB.CreateCareProviderFinanceCode(_teamId, serviceAndServiceDetailFinanceCodeLocationId, "ACC_4439", "FC_ACC_4439");

            #endregion

            serviceRecordPage
                .WaitForServiceRecordPageToLoad()
                .NavigateToServiceMappingsTab();

            cpServiceMappingsPage
                .WaitForCPServiceMappingsPageToLoad()
                .ClickNewRecordButton();

            cpServiceMappingRecordPage
                .WaitForCPServiceMappingRecordPageToLoad()
                .ValidateServiceFieldLookupButtonIsDisplayed(true)
                .ValidateMandatoryFieldIsDisplayed("Service")
                .ValidateBookingTypeLookupButtonIsDisplayed(true)
                .ValidateMandatoryFieldIsDisplayed("Booking Type")
                .ValidateFinanceCodeLookupButtonIsDisplayed(true)
                .ValidateResponsibleTeamLookupButtonIsDisplayed(true)
                .ValidateMandatoryFieldIsDisplayed("Responsible Team")
                .ValidateNoteText_TextFieldIsDisplayed(true);

            cpServiceMappingRecordPage
                .WaitForCPServiceMappingRecordPageToLoad()
                .ValidateServiceLinkText("AAServiceA2_" + currentTimeString)
                .ValidateResponsibleTeamLinkText(_teamName);

            cpServiceMappingRecordPage
                .WaitForCPServiceMappingRecordPageToLoad()
                .ClickResponsibleTeamClearButton()
                .ClickSaveButton()
                .WaitForCPServiceMappingRecordPageToLoad()
                .ValidateBookingTypeMandatoryFieldNotificationMessageIsDisplayed("Please fill out this field.")
                .ValidateResponsibleTeamMandatoryFieldNotificationMessageIsDisplayed("Please fill out this field.")
                .ValidateFormErrorNotificationMessageIsDisplayed("Some data is not correct. Please review the data in the Form.");

            cpServiceMappingRecordPage
                .WaitForCPServiceMappingRecordPageToLoad()
                .ClickResponsibleTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .SearchAndSelectRecord(_teamName, _teamId);

            cpServiceMappingRecordPage
                .WaitForCPServiceMappingRecordPageToLoad()
                .ClickBookingTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("BT4439_" + currentTimeString)
                .TapSearchButton()
                .ValidateResultElementPresent(bookingTypeId)
                .SelectResultElement(bookingTypeId);

            cpServiceMappingRecordPage
                .WaitForCPServiceMappingRecordPageToLoad()
                .ClickFinanceCodeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("FC_ACC_4439")
                .TapSearchButton()
                .ValidateResultElementPresent(financeCodeId)
                .SelectResultElement(financeCodeId);

            cpServiceMappingRecordPage
                .WaitForCPServiceMappingRecordPageToLoad()
                .InsertTextOnNoteTextField("ACC-4439_" + currentTimeString)
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            cpServiceMappingRecordPage
                .WaitForCPServiceMappingRecordPageToLoad()
                .ValidateServiceLinkText("AAServiceA2_" + currentTimeString)
                .ValidateBookingTypeLinkText("BT4439_" + currentTimeString)
                .ValidateFinanceCodeLinkText("FC_ACC_4439 \\ ACC_4439 \\ Service / Service Detail or Booking Type")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateNoteText_Text("ACC-4439_" + currentTimeString)
                .ValidateResponsibleTeamLookupButtonIsDisabled(true)
                .ValidateServiceMappingRecordTitle("AAServiceA2_" + currentTimeString + " \\ " + " BT4439_" + currentTimeString)
                .ValidateServiceLookupButtonIsDisabled(true);

            var CPServiceMappings = dbHelper.careProviderServiceMapping.GetByCareProviderServiceId(careProviderServiceId1);
            Assert.AreEqual(1, CPServiceMappings.Count);

            cpServiceMappingRecordPage
                .WaitForCPServiceMappingRecordPageToLoad()
                .ClickBackButton();

            cpServiceMappingsPage
                .WaitForCPServiceMappingsPageToLoad()
                .ClickNewRecordButton();



            cpServiceMappingRecordPage
                .WaitForCPServiceMappingRecordPageToLoad()
                .ClickBookingTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("BT4439_" + currentTimeString, bookingTypeId);

            cpServiceMappingRecordPage
                .WaitForCPServiceMappingRecordPageToLoad()
                .ClickFinanceCodeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("FC_ACC_4439", financeCodeId);

            cpServiceMappingRecordPage
                .WaitForCPServiceMappingRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("This record can’t be saved. Record with chosen Service and Booking Type already exists.")
                .TapCloseButton();

            cpServiceMappingRecordPage
                .WaitForCPServiceMappingRecordPageToLoad()
                .ClickFinanceCodeClearButton()
                .ClickBookingTypeClearButton()
                .ClickBackButton();

            CPServiceMappings = dbHelper.careProviderServiceMapping.GetByCareProviderServiceId(careProviderServiceId1);
            Assert.AreEqual(1, CPServiceMappings.Count);

            cpServiceMappingsPage
                .WaitForCPServiceMappingsPageToLoad()
                .ResultsPageValidateHeaderCellSortDescendingOrder(2)
                .RecordsPageValidateHeaderCellText(2, "Service")
                .RecordsPageValidateHeaderCellText(3, "Service Detail")
                .RecordsPageValidateHeaderCellText(4, "Booking Type")
                .RecordsPageValidateHeaderCellText(5, "Finance Code")
                .RecordsPageValidateHeaderCellText(6, "Responsible Team")
                .RecordsPageValidateHeaderCellText(7, "Inactive")
                .RecordsPageValidateHeaderCellText(8, "Created By")
                .RecordsPageValidateHeaderCellText(9, "Created On")
                .RecordsPageValidateHeaderCellText(10, "Modified By")
                .RecordsPageValidateHeaderCellText(11, "Modified On");

            cpServiceMappingsPage
                .InsertTextOnQuickSearch("AAServiceA2_" + currentTimeString)
                .ClickQuickSearchButton()
                .WaitForCPServiceMappingsPageToLoad()
                .ValidateRecordVisible(CPServiceMappings[0]);

            cpServiceMappingsPage
                .InsertTextOnQuickSearch("FC_ACC_4439 \\ ACC_4439 \\ Service / Service Detail or Booking Type")
                .ClickQuickSearchButton()
                .WaitForCPServiceMappingsPageToLoad()
                .ValidateRecordVisible(CPServiceMappings[0]);

            cpServiceMappingsPage
                .InsertTextOnQuickSearch(_teamName)
                .ClickQuickSearchButton()
                .WaitForCPServiceMappingsPageToLoad()
                .ValidateRecordVisible(CPServiceMappings[0]);

            serviceRecordPage
                .WaitForServiceRecordPageToLoad()
                .NavigateToDetailsTab()
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Related record exists in Service Mapping. Please delete related records before deleting record in Service.")
                .TapCloseButton();

            serviceRecordPage
                .WaitForServiceRecordPageToLoad()
                .ClickBackButton();

            servicesPage
                .WaitForServicesPageToLoad()
                .InsertTextOnQuickSearch("AAServiceA2_" + currentTimeString)
                .ClickQuickSearchButton()
                .SelectServicesRecord(careProviderServiceId1.ToString())
                .ClickDeleteRecordButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("0 item(s) deleted. 1 item(s) not deleted.")
                .TapOKButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Services Mapping")
                .SelectFilter("1", "Service")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("AAServiceA2_" + currentTimeString, careProviderServiceId1);

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Booking Type")
                .SelectOperator("2", "Equals")
                .ClickRuleValueLookupButton("2");

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("BT4439_" + currentTimeString, bookingTypeId);

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(CPServiceMappings[0].ToString());

            advanceSearchPage
                .ValidateNewRecordButton_ResultsPageIsNotPresent(true);

            #endregion
        }
        #endregion
    }
}

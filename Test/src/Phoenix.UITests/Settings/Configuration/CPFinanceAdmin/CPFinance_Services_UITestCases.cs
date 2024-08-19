using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Configuration.CPFinanceAdmin
{
    /// <summary>
    /// This class contains Automated UI test scripts for CP Finance > Services
    /// </summary>
    [TestClass]
    public class CPFinance_Services_UITestCases : FunctionalTest
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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CPServicesBU");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                _teamName = "CPServices";
                _teamId = commonMethodsDB.CreateTeam(_teamName, null, _businessUnitId, "97410", "CPServices@careworkstempmail.com", _teamName, "020 123456");

                #endregion

                #region Create default system user

                _loginUsername = "CPServicesUser";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUsername, "CPServices", "User", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationProviderId);

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

        #region https://advancedcsg.atlassian.net/browse/ACC-4210

        [TestProperty("JiraIssueID", "ACC-4318")]
        [Description("TC1 - Test for Services / Services Detail / Service Mappings")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen1", "Services")]
        [TestProperty("Screen2", "Service Mappings")]
        public void CPFinance_Services_UITestMethod001()
        {
            #region Step 1

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
                .ClickNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("careproviderservice")
                .ClickOnExpandIcon();

            serviceRecordPage
                .WaitForServiceRecordPageToLoad()
                .ValidateNameFieldIsDisplayed(true)
                .ValidateMandatoryFieldIsDisplayed("Name")
                .ValidateCodeFieldIsDisplayed(true)
                .ValidateGovCodeFieldIsDisplayed(true)
                .ValidateScheduledServiceYesOptionIsDisplayed(true)
                .ValidateScheduledServiceNoOptionIsDisplayed(true)
                .ValidateInactiveYesOptionIsDisplayed(true)
                .ValidateInactiveNoOptionIsDisplayed(true);

            serviceRecordPage
                .ValidateResponsibleTeamLookupButtonIsDisplayed(true)
                .ValidateMandatoryFieldIsDisplayed("Responsible Team")
                .ValidateStartDateFieldIsDisplayed(true)
                .ValidateMandatoryFieldIsDisplayed("Start Date")
                .ValidateEndDateFieldIsDisplayed(true)
                .ValidateValidForExportYesOptionIsDisplayed(true)
                .ValidateValidForExportNoOptionIsDisplayed(true);

            #endregion

            #region Step 2

            serviceRecordPage
                .WaitForServiceRecordPageToLoad()
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateStartDateText(DateTime.Now.ToString("dd'/'MM'/'yyyy"));

            #endregion

            #region Step 3

            serviceRecordPage
                .WaitForServiceRecordPageToLoad()
                .ClickResponsibleTeamClearButton()
                .InsertTextOnStartDate("")
                .ClickSaveButton()
                .WaitForServiceRecordPageToLoad()
                .ValidateNameMandatoryFieldNotificationMessageIsDisplayed("Please fill out this field.")
                .ValidateResponsibleTeamMandatoryFieldNotificationMessageIsDisplayed("Please fill out this field.")
                .ValidateStartDateMandatoryFieldNotificationMessageIsDisplayed("Please fill out this field.")
                .ValidateFormErrorNotificationMessageIsDisplayed("Some data is not correct. Please review the data in the Form.");

            #endregion

            #region Step 4
            serviceRecordPage
                .ClickResponsibleTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .SearchAndSelectRecord(_teamName, _teamId);

            int newCode = dbHelper.careProviderService.GetHighestCode() + 1;

            var CodeNumberId = dbHelper.careProviderService.GetCodeNumber();
            var oldCode = (dbHelper.careProviderService.GetByID(CodeNumberId[0], "code")["code"]).ToString();

            serviceRecordPage
                .WaitForServiceRecordPageToLoad()
                .InsertTextOnStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnName("SR_" + currentTimeString)
                .InsertTextOnCode(newCode.ToString())
                .InsertTextOnGovCode(newCode.ToString())
                .ClickIsscheduledservice_YesOption()
                .InsertTextOnEnddate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForServiceRecordPageToLoad()
                .ValidateNameText("SR_" + currentTimeString)
                .ValidateCodeText(newCode.ToString())
                .ValidateGovCodeText(newCode.ToString())
                .ValidateIsscheduledservice_YesOptionChecked()
                .ValidateIsscheduledservice_NoOptionNotChecked()
                .ValidateInactive_NoOptionChecked()
                .ValidateInactive_YesOptionNotChecked()
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateStartDateText(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDateText(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidateValidforexport_NoOptionChecked()
                .ValidateValidforexport_YesOptionNotChecked()
                .ValidateServiceRecordTitle("SR_" + currentTimeString)
                .ValidateResponsibleTeamLookupButtonIsDisabled(true);

            var CPServices = dbHelper.careProviderService.GetByName("SR_" + currentTimeString);
            Assert.AreEqual(1, CPServices.Count);

            #endregion

            #region Step 5

            //Create Booking Type and Service Mapping
            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);
            var BookingTypeId = commonMethodsDB.CreateBookingType("BT_" + currentTimeString, 5, 60, new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0), 1, false);
            var cpServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(CPServices[0], _teamId, null, BookingTypeId, null, "Test");

            serviceRecordPage
                .WaitForServiceRecordPageToLoad()
                .NavigateToServiceMappingsTab();

            cpServiceMappingsPage
                .WaitForCPServiceMappingsPageToLoad()
                .ValidateRecordVisible(cpServiceMappingId)
                .ValidateRecordCellText(cpServiceMappingId, 3, "")
                .ValidateRecordCellText(cpServiceMappingId, 4, "BT_" + currentTimeString);

            serviceRecordPage
                .WaitForServiceRecordPageToLoad()
                .NavigateToDetailsTab()
                .ClickIsscheduledservice_NoOption()
                .ClickSaveButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("You are changing the mapping of this record to use Service Detail. Do you wish to continue?")
                .TapOKButton();

            serviceRecordPage
                .WaitForServiceRecordPageToLoad()
                .ValidateIsscheduledservice_NoOptionChecked()
                .ValidateIsscheduledservice_YesOptionNotChecked()
                .NavigateToServiceMappingsTab();

            cpServiceMappingsPage
                .WaitForCPServiceMappingsPageToLoad()
                .ValidateRecordVisible(cpServiceMappingId)
                .ValidateRecordCellText(cpServiceMappingId, 4, "");

            #endregion

            #region Step 6

            //Service Detail creation
            var serviceDetailCode = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail();
            int newServiceDetailCode = serviceDetailCode.Count();

            while (dbHelper.careProviderServiceDetail.GetByCode(newServiceDetailCode).Any())
            {
                newServiceDetailCode = newServiceDetailCode + 1;
            }
            var cpServiceDetailId = dbHelper.careProviderServiceDetail.CreateCareProviderServiceDetail(_teamId, "CPSD_" + currentTimeString, newServiceDetailCode, newServiceDetailCode, DateTime.Now, null);
            dbHelper.careProviderServiceMapping.UpdateServiceIdField(cpServiceMappingId, cpServiceDetailId);

            cpServiceMappingsPage
                .WaitForCPServiceMappingsPageToLoad()
                .ClickRefreshButton();

            cpServiceMappingsPage
                .WaitForCPServiceMappingsPageToLoad()
                .ValidateRecordVisible(cpServiceMappingId)
                .ValidateRecordCellText(cpServiceMappingId, 3, "CPSD_" + currentTimeString)
                .ValidateRecordCellText(cpServiceMappingId, 4, "");

            serviceRecordPage
                .WaitForServiceRecordPageToLoad()
                .NavigateToDetailsTab()
                .ClickIsscheduledservice_YesOption()
                .ClickSaveButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("You are changing the mapping of this record to use Booking Type. Do you wish to continue?")
                .TapOKButton();

            serviceRecordPage
                .WaitForServiceRecordPageToLoad()
                .ValidateIsscheduledservice_YesOptionChecked()
                .ValidateIsscheduledservice_NoOptionNotChecked()
                .NavigateToServiceMappingsTab();

            cpServiceMappingsPage
                .WaitForCPServiceMappingsPageToLoad()
                .ValidateRecordVisible(cpServiceMappingId)
                .ValidateRecordCellText(cpServiceMappingId, 3, "");

            #endregion

            #region Step 7

            serviceRecordPage
                .WaitForServiceRecordPageToLoad()
                .NavigateToDetailsTab()
                .InsertTextOnCode((oldCode.ToString()).ToString())
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Service with same combination already exist: Code = " + (oldCode).ToString() + ".")
                .TapCloseButton();

            #endregion

            #region Step 8

            serviceRecordPage
                .WaitForServiceRecordPageToLoad()
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
                .NavigateToServiceMappingsTab();

            cpServiceMappingsPage
                .WaitForCPServiceMappingsPageToLoad()
                .SelectRecord(cpServiceMappingId)
                .ClickDeleteRecordButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("1 item(s) deleted.")
                .TapOKButton();

            cpServiceMappingsPage
                .WaitForCPServiceMappingsPageToLoad()
                .ValidateRecordNotVisible(cpServiceMappingId);

            serviceRecordPage
                .WaitForServiceRecordPageToLoad()
                .NavigateToDetailsTab()
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            servicesPage
                .WaitForServicesPageToLoad()
                .InsertTextOnQuickSearch("*SR_" + currentTimeString + "*")
                .ClickQuickSearchButton()
                .WaitForServicesPageToLoad()
                .ValidateRecordIsPresent(CPServices[0].ToString(), false);

            #endregion

            #region Step 9 and Step 10

            servicesPage
                .WaitForServicesPageToLoad()
                .ClickNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("careproviderservice")
                .ClickOnExpandIcon();

            serviceRecordPage
                .WaitForServiceRecordPageToLoad()
                .InsertTextOnStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnName("SR_" + currentTimeString)
                .InsertTextOnCode(newCode.ToString())
                .InsertTextOnGovCode(newCode.ToString())
                .InsertTextOnEnddate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForServiceRecordPageToLoad()
                .ValidateNameText("SR_" + currentTimeString)
                .ValidateCodeText(newCode.ToString())
                .ValidateServiceRecordTitle("SR_" + currentTimeString);

            serviceRecordPage
                .WaitForServiceRecordPageToLoad()
                .InsertTextOnStartDate(DateTime.Now.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnEnddate(DateTime.Now.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForServiceRecordPageToLoad();

            serviceRecordPage
                .WaitForServiceRecordPageToLoad()
                .NavigateToAuditPage();

            CPServices = dbHelper.careProviderService.GetByName("SR_" + currentTimeString);
            Assert.AreEqual(1, CPServices.Count);

            var auditSearch1 = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 2, //update operation
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = CPServices[0].ToString(),
                ParentTypeName = "CareProviderService",
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
            Assert.AreEqual("CPServices User", auditResponseData.GridData[0].cols[1].Text);

            var auditRecordId1 = auditResponseData.GridData[0].Id;

            auditListPage
                .WaitForAuditListPageToLoad("careproviderservice")
                .ValidateCellText(1, 2, "Updated")
                .ValidateCellText(2, 2, "Created");

            auditListPage
                .WaitForAuditListPageToLoad("careproviderservice")
                .ValidateRecordPresent(auditRecordId1)
                .ClickOnAuditRecord(auditRecordId1);

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Updated")
                .ValidateChangedBy("CPServices User")
                .ValidateFieldNameCellText(1, "Start Date")
                .ValidateOldValueCellText(1, DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidateNewValueCellText(1, DateTime.Now.AddDays(-1).ToString("dd'/'MM'/'yyyy"))

                .ValidateFieldNameCellText(2, "End Date")
                .ValidateOldValueCellText(2, DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidateNewValueCellText(2, DateTime.Now.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
                .TapCloseButton();

            var auditSearch2 = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 1, //create operation
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = CPServices[0].ToString(),
                ParentTypeName = "CareProviderService",
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
            Assert.AreEqual("CPServices User", auditResponseData2.GridData[0].cols[1].Text);
            var createdOnDate = auditResponseData2.GridData[0].cols[2].Text;

            var auditRecordId2 = auditResponseData2.GridData[0].Id;

            auditListPage
                .WaitForAuditListPageToLoad("careproviderservice")
                .ValidateRecordPresent(auditRecordId2)
                .ClickOnAuditRecord(auditRecordId2);

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Created")
                .ValidateChangedBy("CPServices User")
                .ValidateChangedOn(createdOnDate)
                .TapCloseButton();

            serviceRecordPage
                .WaitForServiceRecordPageToLoad()
                .ClickBackButton();

            servicesPage
                .WaitForServicesPageToLoad()
                .SelectSystemView("Inactive Records")
                .ClickColumnHeader("Code")
                .WaitForServicesPageToLoad()
                .ClickColumnHeaderSortDescendingOrder("Code")
                .WaitForServicesPageToLoad()
                .ValidateRecordIsPresent(CPServices[0].ToString(), true);

            servicesPage
                .WaitForServicesPageToLoad()
                .OpenRecord(CPServices[0].ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("careproviderservice")
                .ClickOnExpandIcon();

            serviceRecordPage
                .WaitForServiceRecordPageToLoad()
                .InsertTextOnStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnEnddate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForServiceRecordPageToLoad()
                .ValidateStartDateText(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDateText(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickBackButton();

            servicesPage
                .WaitForServicesPageToLoad()
                .SelectSystemView("Inactive Records");

            System.Threading.Thread.Sleep(2000);
            servicesPage
                .WaitForServicesPageToLoad()
                .ValidateRecordIsPresent(CPServices[0].ToString(), false);

            servicesPage
                .WaitForServicesPageToLoad()
                .SelectSystemView("Active Records");

            System.Threading.Thread.Sleep(2000);
            servicesPage
                .ClickColumnHeader("Code")
                .WaitForServicesPageToLoad()
                .ClickColumnHeaderSortDescendingOrder("Code")
                .WaitForServicesPageToLoad()
                .ValidateRecordIsPresent(CPServices[0].ToString(), true);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4412

        [TestProperty("JiraIssueID", "ACC-4414")]
        [Description("TC2 - Test for Services / Services Detail / Service Mappings")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Services")]
        public void CPFinance_Services_UITestMethod002()
        {
            #region Step 11 to Step 13   

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var Code = dbHelper.careProviderService.GetCodeNumber();
            int newCode = Code.Count();

            while (dbHelper.careProviderService.GetByCode(newCode).Any())
            {
                newCode = newCode + 1;
            }

            var careProviderServiceId1 = dbHelper.careProviderService.CreateCareProviderService(_teamId, "AAServiceA_" + currentTimeString, new DateTime(2020, 1, 1), newCode); //Start date < current date & End date not recorded

            Code = dbHelper.careProviderService.GetCodeNumber();
            int newCode2 = Code.Count();
            while (dbHelper.careProviderService.GetByCode(newCode2).Any())
            {
                newCode2 = newCode2 + 1;
            }
            var careProviderServiceId2 = dbHelper.careProviderService.CreateCareProviderService(_teamId, "AAServiceB_" + currentTimeString, DateTime.Now, newCode2); //Start date = current date & End date not recorded

            Code = dbHelper.careProviderService.GetCodeNumber();
            int newCode3 = Code.Count();
            while (dbHelper.careProviderService.GetByCode(newCode3).Any())
            {
                newCode3 = newCode3 + 1;
            }

            var careProviderServiceId3 = dbHelper.careProviderService.CreateCareProviderService(_teamId, "AAServiceC_" + currentTimeString, new DateTime(2020, 1, 1), newCode3, DateTime.Now); //Start date < current date and End date = current date

            Code = dbHelper.careProviderService.GetCodeNumber();
            int newCode4 = Code.Count();
            while (dbHelper.careProviderService.GetByCode(newCode4).Any())
            {
                newCode4 = newCode4 + 1;
            }
            var careProviderServiceId4 = dbHelper.careProviderService.CreateCareProviderService(_teamId, "AAServiceD_" + currentTimeString, new DateTime(2020, 1, 1), newCode4, DateTime.Now.AddDays(1)); //Start date < current date and End date = future date

            Code = dbHelper.careProviderService.GetCodeNumber();
            int newCode5 = Code.Count();
            while (dbHelper.careProviderService.GetByCode(newCode5).Any())
            {
                newCode5 = newCode5 + 1;
            }
            var careProviderServiceId5 = dbHelper.careProviderService.CreateCareProviderService(_teamId, "AAServiceE_" + currentTimeString, DateTime.Now, newCode5, DateTime.Now); //Start date = current date and End date = current date

            Code = dbHelper.careProviderService.GetCodeNumber();
            int newCode6 = Code.Count();
            while (dbHelper.careProviderService.GetByCode(newCode6).Any())
            {
                newCode6 = newCode6 + 1;
            }
            var careProviderServiceId6 = dbHelper.careProviderService.CreateCareProviderService(_teamId, "AAServiceF_" + currentTimeString, DateTime.Now, newCode6, DateTime.Now.AddDays(1)); //Start date = current date and End date = future date

            Code = dbHelper.careProviderService.GetCodeNumber();
            int newCode7 = Code.Count();
            while (dbHelper.careProviderService.GetByCode(newCode7).Any())
            {
                newCode7 = newCode7 + 1;
            }
            var careProviderServiceId7 = dbHelper.careProviderService.CreateCareProviderService(_teamId, "AAServiceG_" + currentTimeString, DateTime.Now.AddDays(1), newCode7); //Start date = future date and End date not recorded

            Code = dbHelper.careProviderService.GetCodeNumber();
            int newCode8 = Code.Count();
            while (dbHelper.careProviderService.GetByCode(newCode8).Any())
            {
                newCode8 = newCode8 + 1;
            }
            var careProviderServiceId8 = dbHelper.careProviderService.CreateCareProviderService(_teamId, "AAServiceH_" + currentTimeString, new DateTime(2020, 1, 1), newCode8, DateTime.Now.AddDays(-1)); //Start date and End date < current date

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
                .ResultsPageValidateHeaderCellSortAscendingOrder(2)
                .RecordsPageValidateHeaderCellText(2, "Name")
                .RecordsPageValidateHeaderCellText(3, "Code")
                .RecordsPageValidateHeaderCellText(4, "Gov Code")
                .RecordsPageValidateHeaderCellText(5, "Start Date")
                .RecordsPageValidateHeaderCellText(6, "End Date")
                .RecordsPageValidateHeaderCellText(7, "Scheduled Service?")
                .RecordsPageValidateHeaderCellText(8, "Inactive")
                .RecordsPageValidateHeaderCellText(9, "Valid For Export")
                .RecordsPageValidateHeaderCellText(10, "Responsible Team")
                .RecordsPageValidateHeaderCellText(11, "Created By")
                .RecordsPageValidateHeaderCellText(12, "Created On")
                .RecordsPageValidateHeaderCellText(13, "Modified By")
                .RecordsPageValidateHeaderCellText(14, "Modified On");

            var ActiveRecordsList = dbHelper.careProviderService.GetAllSortedByName(true);


            servicesPage
                .ValidateRecordIsPresent(1, ActiveRecordsList[0].ToString(), true)
                .ValidateRecordIsPresent(2, ActiveRecordsList[1].ToString(), true)
                .ValidateRecordIsPresent(3, ActiveRecordsList[2].ToString(), true)
                .ValidateRecordIsPresent(4, ActiveRecordsList[3].ToString(), true)
                .ValidateRecordIsPresent(5, ActiveRecordsList[4].ToString(), true)
                .ValidateRecordIsPresent(6, ActiveRecordsList[5].ToString(), true);

            var InactiveRecordsList = dbHelper.careProviderService.GetAllSortedByName(true, true);

            servicesPage
                .SelectSystemView("Inactive Records")
                .ValidateRecordIsPresent(1, InactiveRecordsList[0].ToString(), true)
                .ValidateRecordIsPresent(2, InactiveRecordsList[1].ToString(), true);

            servicesPage
                .InsertTextOnQuickSearch("AAServiceB_" + currentTimeString)
                .ClickQuickSearchButton()
                .WaitForServicesPageToLoad()
                .ValidateRecordIsPresent(careProviderServiceId2.ToString(), true);
            servicesPage
                .ValidateRecordIsPresent(1, careProviderServiceId1.ToString(), false)
                .ValidateRecordIsPresent(3, careProviderServiceId3.ToString(), false)
                .ValidateRecordIsPresent(4, careProviderServiceId4.ToString(), false)
                .ValidateRecordIsPresent(5, careProviderServiceId5.ToString(), false)
                .ValidateRecordIsPresent(6, careProviderServiceId6.ToString(), false);

            servicesPage
                .InsertTextOnQuickSearch(newCode5.ToString())
                .ClickQuickSearchButton()
                .WaitForServicesPageToLoad()
                .ValidateRecordIsPresent(careProviderServiceId5.ToString(), true);
            servicesPage
                .ValidateRecordIsPresent(1, careProviderServiceId1.ToString(), false)
                .ValidateRecordIsPresent(1, careProviderServiceId2.ToString(), false)
                .ValidateRecordIsPresent(3, careProviderServiceId3.ToString(), false)
                .ValidateRecordIsPresent(4, careProviderServiceId4.ToString(), false)
                .ValidateRecordIsPresent(6, careProviderServiceId6.ToString(), false);

            dbHelper.careProviderService.DeleteCareProviderService(careProviderServiceId1);
            dbHelper.careProviderService.DeleteCareProviderService(careProviderServiceId2);
            dbHelper.careProviderService.DeleteCareProviderService(careProviderServiceId3);
            dbHelper.careProviderService.DeleteCareProviderService(careProviderServiceId4);
            dbHelper.careProviderService.DeleteCareProviderService(careProviderServiceId5);
            dbHelper.careProviderService.DeleteCareProviderService(careProviderServiceId6);
            dbHelper.careProviderService.DeleteCareProviderService(careProviderServiceId7);
            dbHelper.careProviderService.DeleteCareProviderService(careProviderServiceId8);

            Code = dbHelper.careProviderService.GetCodeNumber();
            int newCode9 = Code.Count();
            while (dbHelper.careProviderService.GetByCode(newCode9).Any())
            {
                newCode9 = newCode9 + 1;
            }

            servicesPage
                .WaitForServicesPageToLoad()
                .ClickNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("careproviderservice")
                .ClickOnExpandIcon();

            serviceRecordPage
                .WaitForServiceRecordPageToLoad()
                .InsertTextOnStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnName("ZZServiceZ_" + currentTimeString)
                .InsertTextOnCode(newCode9.ToString())
                .InsertTextOnGovCode(newCode9.ToString())
                .InsertTextOnEnddate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickIsscheduledservice_YesOption()
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForServiceRecordPageToLoad()
                .ValidateNameText("ZZServiceZ_" + currentTimeString)
                .ValidateCodeText(newCode9.ToString())
                .ValidateIsscheduledservice_YesOptionChecked()
                .ValidateIsscheduledservice_NoOptionNotChecked()
                .ValidateServiceRecordTitle("ZZServiceZ_" + currentTimeString);

            #endregion
        }

        #endregion
    }
}

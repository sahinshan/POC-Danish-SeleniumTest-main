using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Configuration.CPFinanceAdmin
{
    /// <summary>
    /// This class contains Automated UI test scripts for CP Finance > Services Details
    /// </summary>
    [TestClass]
    public class CPFinance_ServiceDetails_UITestCases : FunctionalTest
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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CPServiceDetailsBU");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                _teamName = "CPServiceDetails";
                _teamId = commonMethodsDB.CreateTeam(_teamName, null, _businessUnitId, "97413", "CPServiceDetails@careworkstempmail.com", _teamName, "020 123456");

                #endregion

                #region Create default system user

                _loginUsername = "CPServiceDetailsUser";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUsername, "CPServiceDetails", "User", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationProviderId);

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

        #region https://advancedcsg.atlassian.net/browse/ACC-4413

        [TestProperty("JiraIssueID", "ACC-4424")]
        [Description("TC3 - Test for Services / Services Detail / Service Mappings")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Services Detail")]
        public void CPFinance_ServiceDetails_UITestMethod001()
        {
            #region Care Provider Service Detail

            var serviceDetailCodes = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail();
            int newServiceDetailCode = 1;

            if (serviceDetailCodes.Count > 0)
                newServiceDetailCode = ((int)(dbHelper.careProviderServiceDetail.GetByID(serviceDetailCodes[0], "code")["code"])) + 1;

            var oldCode = newServiceDetailCode + 1;
            dbHelper.careProviderServiceDetail.CreateCareProviderServiceDetail(_teamId, "SD_DB_" + currentTimeString, oldCode, oldCode, new DateTime(2020, 1, 1));

            #endregion

            #region Step 14

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            cpFinanceAdminPage
                .WaitForCPFinanceAdminPageToLoad()
                .ClickPersonContractsAreaButton()
                .ClickServicesDetailButton();

            serviceDetailsPage
                .WaitForServiceDetailsPageToLoad()
                .ClickNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("careproviderservicedetail")
                .ClickOnExpandIcon();

            serviceDetailsRecordPage
                .WaitForServiceDetailsRecordPageToLoad()
                .ValidateNameFieldIsDisplayed(true)
                .ValidateMandatoryFieldIsDisplayed("Name")
                .ValidateCodeFieldIsDisplayed(true)
                .ValidateGovCodeFieldIsDisplayed(true)
                .ValidateInactiveYesOptionIsDisplayed(true)
                .ValidateInactiveNoOptionIsDisplayed(true);

            serviceDetailsRecordPage
                .WaitForServiceDetailsRecordPageToLoad()
                .ValidateResponsibleTeamLookupButtonIsDisplayed(true)
                .ValidateMandatoryFieldIsDisplayed("Responsible Team")
                .ValidateStartDateFieldIsDisplayed(true)
                .ValidateMandatoryFieldIsDisplayed("Start Date")
                .ValidateEndDateFieldIsDisplayed(true)
                .ValidateValidForExportYesOptionIsDisplayed(true)
                .ValidateValidForExportNoOptionIsDisplayed(true);

            #endregion

            #region Step 15

            serviceDetailsRecordPage
                .WaitForServiceDetailsRecordPageToLoad()
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateStartDateText(DateTime.Now.ToString("dd'/'MM'/'yyyy"));

            #endregion

            #region Step 16

            serviceDetailsRecordPage
                .WaitForServiceDetailsRecordPageToLoad()
                .ClickResponsibleTeamClearButton()
                .InsertTextOnStartDate("")
                .ClickSaveButton()
                .WaitForServiceDetailsRecordPageToLoad()
                .ValidateNameMandatoryFieldNotificationMessageIsDisplayed("Please fill out this field.")
                .ValidateResponsibleTeamMandatoryFieldNotificationMessageIsDisplayed("Please fill out this field.")
                .ValidateStartDateMandatoryFieldNotificationMessageIsDisplayed("Please fill out this field.")
                .ValidateFormErrorNotificationMessageIsDisplayed("Some data is not correct. Please review the data in the Form.");

            #endregion

            #region Step 17

            serviceDetailsRecordPage
                .ClickResponsibleTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .SearchAndSelectRecord(_teamName, _teamId);

            serviceDetailsRecordPage
                .WaitForServiceDetailsRecordPageToLoad()
                .InsertTextOnStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnName("SD_" + currentTimeString)
                .InsertTextOnCode(newServiceDetailCode.ToString())
                .InsertTextOnGovCode(newServiceDetailCode.ToString())
                .InsertTextOnEnddate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForServiceDetailsRecordPageToLoad()
                .ValidateNameText("SD_" + currentTimeString)
                .ValidateCodeText(newServiceDetailCode.ToString())
                .ValidateGovCodeText(newServiceDetailCode.ToString())
                .ValidateInactive_NoOptionChecked()
                .ValidateInactive_YesOptionNotChecked()
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateStartDateText(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDateText(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidateValidforexport_NoOptionChecked()
                .ValidateValidforexport_YesOptionNotChecked()
                .ValidateServiceDetailsRecordTitle("SD_" + currentTimeString)
                .ValidateResponsibleTeamLookupButtonIsDisabled(true);

            var CPServiceDetails = dbHelper.careProviderServiceDetail.GetByName("SD_" + currentTimeString);
            Assert.AreEqual(1, CPServiceDetails.Count);

            #endregion

            #region Step 18

            serviceDetailsRecordPage
                .WaitForServiceDetailsRecordPageToLoad()
                .InsertTextOnCode(oldCode.ToString())
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Service Detail with same combination already exist: Code = " + oldCode.ToString() + ".")
                .TapCloseButton();

            #endregion

            #region Step 19

            serviceDetailsRecordPage
                .WaitForServiceDetailsRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            serviceDetailsPage
                .WaitForServiceDetailsPageToLoad()
                .InsertTextOnQuickSearch("*SD_" + currentTimeString + "*")
                .ClickQuickSearchButton()
                .WaitForServiceDetailsPageToLoad()
                .ValidateRecordIsPresent(CPServiceDetails[0].ToString(), false);

            #endregion

            #region Step 20 and Step 21

            serviceDetailsPage
                .WaitForServiceDetailsPageToLoad()
                .ClickNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("careproviderservicedetail")
                .ClickOnExpandIcon();

            serviceDetailsRecordPage
                .WaitForServiceDetailsRecordPageToLoad()
                .InsertTextOnStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnName("SD_" + currentTimeString)
                .InsertTextOnCode(newServiceDetailCode.ToString())
                .InsertTextOnGovCode(newServiceDetailCode.ToString())
                .InsertTextOnEnddate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForServiceDetailsRecordPageToLoad()
                .ValidateNameText("SD_" + currentTimeString)
                .ValidateCodeText(newServiceDetailCode.ToString())
                .ValidateServiceDetailsRecordTitle("SD_" + currentTimeString);

            serviceDetailsRecordPage
                .WaitForServiceDetailsRecordPageToLoad()
                .InsertTextOnStartDate(DateTime.Now.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnEnddate(DateTime.Now.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForServiceDetailsRecordPageToLoad();

            serviceDetailsRecordPage
                .WaitForServiceDetailsRecordPageToLoad()
                .NavigateToAuditPage();

            CPServiceDetails = dbHelper.careProviderServiceDetail.GetByName("SD_" + currentTimeString);
            Assert.AreEqual(1, CPServiceDetails.Count);

            var auditSearch1 = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 2, //update operation
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = CPServiceDetails[0].ToString(),
                ParentTypeName = "CareProviderServiceDetails",
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
            Assert.AreEqual("CPServiceDetails User", auditResponseData.GridData[0].cols[1].Text);

            var auditRecordId1 = auditResponseData.GridData[0].Id;

            auditListPage
                .WaitForAuditListPageToLoad("careproviderservicedetail")
                .ValidateCellText(1, 2, "Updated")
                .ValidateCellText(2, 2, "Created");

            auditListPage
                .WaitForAuditListPageToLoad("careproviderservicedetail")
                .ValidateRecordPresent(auditRecordId1)
                .ClickOnAuditRecord(auditRecordId1);

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Updated")
                .ValidateChangedBy("CPServiceDetails User")
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
                ParentId = CPServiceDetails[0].ToString(),
                ParentTypeName = "CareProviderServiceDetails",
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
            Assert.AreEqual("CPServiceDetails User", auditResponseData2.GridData[0].cols[1].Text);
            var createdOnDate = auditResponseData2.GridData[0].cols[2].Text;

            var auditRecordId2 = auditResponseData2.GridData[0].Id;

            auditListPage
                .WaitForAuditListPageToLoad("careproviderservicedetail")
                .ValidateRecordPresent(auditRecordId2)
                .ClickOnAuditRecord(auditRecordId2);

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Created")
                .ValidateChangedBy("CPServiceDetails User")
                .ValidateChangedOn(createdOnDate)
                .TapCloseButton();

            serviceDetailsRecordPage
                .WaitForServiceDetailsRecordPageToLoad()
                .ClickBackButton();

            serviceDetailsPage
                .WaitForServiceDetailsPageToLoad()
                .SelectSystemView("Inactive Records")
                .ClickColumnHeader("Code")
                .WaitForServiceDetailsPageToLoad()
                .ClickColumnHeaderSortDescendingOrder("Code")
                .WaitForServiceDetailsPageToLoad()
                .ValidateRecordIsPresent(CPServiceDetails[0].ToString(), true);

            serviceDetailsPage
                .WaitForServiceDetailsPageToLoad()
                .OpenRecord(CPServiceDetails[0].ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("careproviderservicedetail")
                .ClickOnExpandIcon();

            serviceDetailsRecordPage
                .WaitForServiceDetailsRecordPageToLoad()
                .InsertTextOnStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnEnddate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForServiceDetailsRecordPageToLoad()
                .ValidateStartDateText(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDateText(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickBackButton();

            serviceDetailsPage
                .WaitForServiceDetailsPageToLoad()
                .SelectSystemView("Inactive Records");

            System.Threading.Thread.Sleep(1500);

            serviceDetailsPage
                .WaitForServiceDetailsPageToLoad()
                .ValidateRecordIsPresent(CPServiceDetails[0].ToString(), false);

            serviceDetailsPage
                .WaitForServiceDetailsPageToLoad()
                .SelectSystemView("Active Records");

            System.Threading.Thread.Sleep(1000);

            serviceDetailsPage
                .ClickColumnHeader("Code")
                .WaitForServiceDetailsPageToLoad()
                .ClickColumnHeaderSortDescendingOrder("Code")
                .WaitForServiceDetailsPageToLoad()
                .ValidateRecordIsPresent(CPServiceDetails[0].ToString(), true);

            #endregion

            #region Step 22 to Step 24

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            cpFinanceAdminPage
                .WaitForCPFinanceAdminPageToLoad()
                .ClickPersonContractsAreaButton()
                .ClickServicesDetailButton();

            serviceDetailsPage
                .WaitForServiceDetailsPageToLoad()
                .RecordsPageValidateHeaderCellText(2, "Name")
                .RecordsPageValidateHeaderCellText(3, "Code")
                .RecordsPageValidateHeaderCellText(4, "Gov Code")
                .RecordsPageValidateHeaderCellText(5, "Start Date")
                .RecordsPageValidateHeaderCellText(6, "End Date")
                .RecordsPageValidateHeaderCellText(7, "Inactive")
                .RecordsPageValidateHeaderCellText(8, "Valid For Export")
                .RecordsPageValidateHeaderCellText(9, "Responsible Team")
                .RecordsPageValidateHeaderCellText(10, "Created By")
                .RecordsPageValidateHeaderCellText(11, "Created On")
                .RecordsPageValidateHeaderCellText(12, "Modified By")
                .RecordsPageValidateHeaderCellText(13, "Modified On");

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var Code = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail();
            int newCode = Code.Count();

            while (dbHelper.careProviderServiceDetail.GetByCode(newCode).Any())
            {
                newCode = newCode + 1;
            }

            var cpServiceDetailId1 = dbHelper.careProviderServiceDetail.CreateCareProviderServiceDetail(_teamId, "AACPSD_A_" + currentTimeString, newCode, newCode, new DateTime(2020, 1, 1), null);//Start date < current date & End date not recorded

            Code = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail();
            int newCode2 = Code.Count();
            while (dbHelper.careProviderServiceDetail.GetByCode(newCode2).Any())
            {
                newCode2 = newCode2 + 1;
            }
            var cpServiceDetailId2 = dbHelper.careProviderServiceDetail.CreateCareProviderServiceDetail(_teamId, "AACPSD_B_" + currentTimeString, newCode2, newCode2, DateTime.Now); //Start date = current date & End date not recorded

            Code = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail();
            int newCode3 = Code.Count();
            while (dbHelper.careProviderServiceDetail.GetByCode(newCode3).Any())
            {
                newCode3 = newCode3 + 1;
            }
            var cpServiceDetailId3 = dbHelper.careProviderServiceDetail.CreateCareProviderServiceDetail(_teamId, "AACPSD_C_" + currentTimeString, newCode3, newCode3, new DateTime(2020, 1, 1), DateTime.Now); //Start date < current date and End date = current date

            Code = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail();
            int newCode4 = Code.Count();
            while (dbHelper.careProviderServiceDetail.GetByCode(newCode4).Any())
            {
                newCode4 = newCode4 + 1;
            }
            var cpServiceDetailId4 = dbHelper.careProviderServiceDetail.CreateCareProviderServiceDetail(_teamId, "AACPSD_D_" + currentTimeString, newCode4, newCode4, new DateTime(2020, 1, 1), DateTime.Now.AddDays(1)); //Start date < current date and End date = future date

            Code = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail();
            int newCode5 = Code.Count();
            while (dbHelper.careProviderServiceDetail.GetByCode(newCode5).Any())
            {
                newCode5 = newCode5 + 1;
            }
            var cpServiceDetailId5 = dbHelper.careProviderServiceDetail.CreateCareProviderServiceDetail(_teamId, "AACPSD_E_" + currentTimeString, newCode5, newCode5, DateTime.Now, DateTime.Now); //Start date = current date and End date = current date

            Code = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail();
            int newCode6 = Code.Count();
            while (dbHelper.careProviderServiceDetail.GetByCode(newCode6).Any())
            {
                newCode6 = newCode6 + 1;
            }
            var cpServiceDetailId6 = dbHelper.careProviderServiceDetail.CreateCareProviderServiceDetail(_teamId, "AACPSD_F_" + currentTimeString, newCode6, newCode6, DateTime.Now, DateTime.Now.AddDays(1)); //Start date = current date and End date = future date

            Code = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail();
            int newCode7 = Code.Count();
            while (dbHelper.careProviderServiceDetail.GetByCode(newCode7).Any())
            {
                newCode7 = newCode7 + 1;
            }
            var cpServiceDetailId7 = dbHelper.careProviderServiceDetail.CreateCareProviderServiceDetail(_teamId, "AA2CPSD_A_" + currentTimeString, newCode7, newCode7, DateTime.Now.AddDays(1)); //Start date = future date and End date not recorded

            Code = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail();
            int newCode8 = Code.Count();
            while (dbHelper.careProviderServiceDetail.GetByCode(newCode8).Any())
            {
                newCode8 = newCode8 + 1;
            }
            var cpServiceDetailId8 = dbHelper.careProviderServiceDetail.CreateCareProviderServiceDetail(_teamId, "AA2CPSD_B_" + currentTimeString, newCode8, newCode8, new DateTime(2020, 1, 1), DateTime.Now.AddDays(-1)); //Start date and End date < current date


            serviceDetailsPage
                .WaitForServiceDetailsPageToLoad()
                .ClickRefreshButton()
                .WaitForServiceDetailsPageToLoad();

            serviceDetailsPage
                .ClickColumnHeader(11)
                .WaitForServiceDetailsPageToLoad()
                .ClickColumnHeader(11)
                .WaitForServiceDetailsPageToLoad()
                .ValidateRecordIsPresent(cpServiceDetailId1.ToString(), true)
                .ValidateRecordIsPresent(cpServiceDetailId2.ToString(), true)
                .ValidateRecordIsPresent(cpServiceDetailId3.ToString(), true)
                .ValidateRecordIsPresent(cpServiceDetailId4.ToString(), true)
                .ValidateRecordIsPresent(cpServiceDetailId5.ToString(), true)
                .ValidateRecordIsPresent(cpServiceDetailId6.ToString(), true);

            serviceDetailsPage
                .SelectSystemView("Inactive Records")
                .WaitForServiceDetailsPageToLoad()
                .ClickColumnHeader(11)
                .WaitForServiceDetailsPageToLoad()
                .ClickColumnHeader(11)
                .WaitForServiceDetailsPageToLoad()
                .ValidateRecordIsPresent(cpServiceDetailId7.ToString(), true)
                .ValidateRecordIsPresent(cpServiceDetailId8.ToString(), true);

            serviceDetailsPage
                .InsertTextOnQuickSearch("AACPSD_B_" + currentTimeString)
                .ClickQuickSearchButton()
                .WaitForServiceDetailsPageToLoad()
                .ValidateRecordIsPresent(cpServiceDetailId2.ToString(), true);

            serviceDetailsPage
                .ValidateRecordIsPresent(cpServiceDetailId1.ToString(), false)
                .ValidateRecordIsPresent(cpServiceDetailId3.ToString(), false)
                .ValidateRecordIsPresent(cpServiceDetailId4.ToString(), false)
                .ValidateRecordIsPresent(cpServiceDetailId5.ToString(), false)
                .ValidateRecordIsPresent(cpServiceDetailId6.ToString(), false);

            serviceDetailsPage
                .InsertTextOnQuickSearch(newCode5.ToString())
                .ClickQuickSearchButton()
                .WaitForServiceDetailsPageToLoad()
                .ValidateRecordIsPresent(cpServiceDetailId5.ToString(), true);

            serviceDetailsPage
                .ValidateRecordIsPresent(cpServiceDetailId1.ToString(), false)
                .ValidateRecordIsPresent(cpServiceDetailId2.ToString(), false)
                .ValidateRecordIsPresent(cpServiceDetailId3.ToString(), false)
                .ValidateRecordIsPresent(cpServiceDetailId4.ToString(), false)
                .ValidateRecordIsPresent(cpServiceDetailId6.ToString(), false);

            dbHelper.careProviderServiceDetail.DeleteCareProviderServiceDetailRecord(cpServiceDetailId1);
            dbHelper.careProviderServiceDetail.DeleteCareProviderServiceDetailRecord(cpServiceDetailId2);
            dbHelper.careProviderServiceDetail.DeleteCareProviderServiceDetailRecord(cpServiceDetailId3);
            dbHelper.careProviderServiceDetail.DeleteCareProviderServiceDetailRecord(cpServiceDetailId4);
            dbHelper.careProviderServiceDetail.DeleteCareProviderServiceDetailRecord(cpServiceDetailId5);
            dbHelper.careProviderServiceDetail.DeleteCareProviderServiceDetailRecord(cpServiceDetailId6);
            dbHelper.careProviderServiceDetail.DeleteCareProviderServiceDetailRecord(cpServiceDetailId7);
            dbHelper.careProviderServiceDetail.DeleteCareProviderServiceDetailRecord(cpServiceDetailId8);

            Code = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail();
            int newCode9 = Code.Count();
            while (dbHelper.careProviderServiceDetail.GetByCode(newCode9).Any())
            {
                newCode9 = newCode9 + 1;
            }

            serviceDetailsPage
                .WaitForServiceDetailsPageToLoad()
                .ClickNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("careproviderservicedetail")
                .ClickOnExpandIcon();

            serviceDetailsRecordPage
                .WaitForServiceDetailsRecordPageToLoad()
                .InsertTextOnStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnName("ZZSDZ_" + currentTimeString)
                .InsertTextOnCode(newCode9.ToString())
                .InsertTextOnGovCode(newCode9.ToString())
                .InsertTextOnEnddate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForServiceDetailsRecordPageToLoad()
                .ValidateNameText("ZZSDZ_" + currentTimeString)
                .ValidateCodeText(newCode9.ToString())
                .ValidateServiceDetailsRecordTitle("ZZSDZ_" + currentTimeString);

            #endregion
        }
        #endregion

    }
}

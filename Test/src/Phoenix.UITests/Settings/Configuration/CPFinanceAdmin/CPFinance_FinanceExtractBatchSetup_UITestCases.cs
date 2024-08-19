using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Configuration.CPFinanceAdmin
{
    /// <summary>
    /// This class contains Automated UI test scripts for CP Finance > Finance Extract Batch Setup
    /// </summary>
    [TestClass]
    public class CPFinance_FinanceExtractBatchSetup_UITestCases : FunctionalTest
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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CP_FEBS_BU");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                _teamName = "CP_FEBS_Team";
                _teamId = commonMethodsDB.CreateTeam(_teamName, null, _businessUnitId, "07410", "CPFEBSetupTeam@careworkstempmail.com", _teamName, "010 123456");

                #endregion

                #region Create default system user

                _loginUsername = "CP_FEBSetupUser";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUsername, "CP_FEBSetup", "User", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationProviderId);

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

        [TestProperty("JiraIssueID", "ACC-4523")]
        [Description("Test for Finance Extract Batch Setup - Test automation for Steps 1 to 7")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen", "Finance Extract Batch Setups")]
        public void CPFinance_FinanceExtractBatchSetup_UITestMethod001()
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
                .ClickCareProviderInvoicingAreaButton()
                .ClickFinanceExtractBatchSetupsButton();

            cpFinanceExtractBatchSetupsPage
                .WaitForCPFinanceExtractBatchSetupsPageToLoad()
                .ClickNewRecordButton();

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ValidateStartDateFieldIsDisplayed(true)
                .ValidateStartTimeFieldIsDisplayed(true)
                .ValidateEndDateFieldIsDisplayed(true);

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ValidateCareProviderExtractNameLookupButtonIsDisplayed(true)
                .ValidateExtractFrequencyIsDisplayed(true)
                .ValidateExtractOnMonday_YesOptionIsDisplayed(true)
                .ValidateExtractOnMonday_NoOptionIsDisplayed(true)
                .ValidateExtractOnTuesday_YesOptionIsDisplayed(true)
                .ValidateExtractOnTuesday_NoOptionIsDisplayed(true)
                .ValidateExtractOnWednesday_YesOptionIsDisplayed(true)
                .ValidateExtractOnWednesday_NoOptionIsDisplayed(true)
                .ValidateExtractOnThursday_YesOptionIsDisplayed(true)
                .ValidateExtractOnThursday_NoOptionIsDisplayed(true)
                .ValidateExtractOnFriday_YesOptionIsDisplayed(true)
                .ValidateExtractOnFriday_NoOptionIsDisplayed(true)
                .ValidateExtractOnSaturday_YesOptionIsDisplayed(true)
                .ValidateExtractOnSaturday_NoOptionIsDisplayed(true)
                .ValidateExtractOnSunday_YesOptionIsDisplayed(true)
                .ValidateExtractOnSunday_NoOptionIsDisplayed(true);

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ValidateCareProviderExtractTypeLookupButtonIsDisplayed(true)
                .ValidateExtractReferenceFieldIsDisplayed(true)
                .ValidateExcludeZeroTransactionsFromExtractOptionsAreDisplayed(true)
                .ValidateVatGlCodeFieldIsDisplayed(true);

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ValidateInvoiceTemplateLookupButtonIsDisplayed(true)
                .ValidateInvoiceTransactionsGroupingIsDisplayed(true)
                .ValidateExcludeZeroTransactionsFromInvoiceOptionsAreDisplayed(true)
                .ValidateGenerateAndSendInvoicesAutomaticallyOptionsAreDisplayed(true)
                .ValidatePaymentTermsFieldIsDisplayed(true)
                .ValidatePaymentTermUnitsIsDisplayed(true)
                .ValidateShowRoomNumberOptionsAreDisplayed(true)
                .ValidateShowInvoiceTextOptionsAreDisplayed(true)
                .ValidatePaymentDetailToShowFieldIsDisplayed(true)
                .ValidateShowWeeklyBreakdownOptionsAreDisplayed(true);

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ValidateResponsibleTeamLookupButtonIsDisplayed(true);

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ValidateMandatoryFieldIsDisplayed("Start Date")
                .ValidateMandatoryFieldIsDisplayed("Start Time")
                .ValidateMandatoryFieldIsDisplayed("Extract Name")
                .ValidateMandatoryFieldIsDisplayed("Extract Frequency")
                .ValidateMandatoryFieldIsDisplayed("Monday")
                .ValidateMandatoryFieldIsDisplayed("Tuesday")
                .ValidateMandatoryFieldIsDisplayed("Wednesday")
                .ValidateMandatoryFieldIsDisplayed("Thursday")
                .ValidateMandatoryFieldIsDisplayed("Friday")
                .ValidateMandatoryFieldIsDisplayed("Saturday")
                .ValidateMandatoryFieldIsDisplayed("Sunday")
                .ValidateMandatoryFieldIsDisplayed("Extract Type")
                .ValidateMandatoryFieldIsDisplayed("Exclude Zero Transactions from Extract?")
                .ValidateMandatoryFieldIsDisplayed("Invoice Template")
                .ValidateMandatoryFieldIsDisplayed("Invoice Transactions Grouping")
                .ValidateMandatoryFieldIsDisplayed("Exclude Zero Transactions from Invoice?")
                .ValidateMandatoryFieldIsDisplayed("Exclude Zero Transactions from Invoice?")
                .ValidateMandatoryFieldIsDisplayed("Payment Terms")
                .ValidateMandatoryFieldIsDisplayed("Payment Term Units")
                .ValidateMandatoryFieldIsDisplayed("Payment Detail to Show")
                .ValidateMandatoryFieldIsDisplayed("Responsible Team");

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ValidateStartTimeText("02:00")
                .ValidateExtractOnMonday_NoOptionChecked()
                .ValidateExtractOnTuesday_NoOptionChecked()
                .ValidateExtractOnWednesday_NoOptionChecked()
                .ValidateExtractOnThursday_YesOptionChecked()
                .ValidateExtractOnFriday_NoOptionChecked()
                .ValidateExtractOnSaturday_NoOptionChecked()
                .ValidateExtractOnSunday_NoOptionChecked()
                .ValidateExcludeZeroTransactionsFromInvoice_YesOptionChecked()
                .ValidatePaymentTermsSelectedText("Calendar Months")
                .ValidatePaymentTermUnitsText("1")
                .ValidateShowRoomNumber_NoOptionChecked()
                .ValidateShowInvoiceText_YesOptionChecked()
                .ValidatePaymentDetailSelectedText("Bank Details")
                .ValidateShowWeeklyBreakdown_YesOptionChecked()
                .ValidateResponsibleTeamLinkText(_teamName);


            #endregion

            #region Step 2

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var Code1 = dbHelper.careProviderExtractName.GetAllCareProviderExtractName();
            int newCode1 = Code1.Count();

            while (dbHelper.careProviderExtractName.GetByCode(newCode1).Any())
            {
                newCode1 = newCode1 + 1;
            }
            var _cpExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, "CPEN_" + currentTimeString, newCode1, null, DateTime.Now);

            var Code2 = dbHelper.careProviderExtractType.GetAllCareProviderExtractType();
            int newCode2 = Code2.Count();

            while (dbHelper.careProviderExtractType.GetByCode(newCode2).Any())
            {
                newCode2 = newCode2 + 1;
            }
            var _cpExtractTypeId = commonMethodsDB.CreateCareProviderExtractType(_teamId, "CPET_" + currentTimeString, newCode2, null, DateTime.Now);


            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .InsertTextOnStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnStartTime("01:00")
                .ClickCareProviderExtractNameLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("CPEN_" + currentTimeString, _cpExtractNameId);

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ClickCareProviderExtractTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("CPET_" + currentTimeString, _cpExtractTypeId);

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ClickEmailSenderLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookFor("Teams")
                .SelectLookIn("Lookup View")
                .SearchAndSelectRecord(_teamName, _teamId);

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .InsertTextOnExtractReference("ERef_" + currentTimeString)
                .InsertTextOnVatGlCode("VatGl_" + currentTimeString)
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ValidateStartDateText(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidateStartTimeText("01:00")
                .ValidateEndDateText("")
                .ValidateCareProviderExtractNameLinkText("CPEN_" + currentTimeString)
                .ValidateExtractFrequencySelectedText("Every Week");

            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    #region If current date is a Sunday

                    cpFinanceExtractBatchSetupRecordPage
                        .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                        .ValidateExtractOnSunday_YesOptionChecked()
                        .ValidateExtractOnSunday_NoOptionNotChecked()
                        .ValidateExtractOnMonday_NoOptionChecked()
                        .ValidateExtractOnTuesday_NoOptionChecked()
                        .ValidateExtractOnWednesday_NoOptionChecked()
                        .ValidateExtractOnThursday_NoOptionChecked()
                        .ValidateExtractOnFriday_NoOptionChecked()
                        .ValidateExtractOnSaturday_NoOptionChecked()
                        .ValidateExtractOnSunday_NoOptionChecked();

                    #endregion
                    break;
                case DayOfWeek.Monday:
                    #region If current date is a Monday

                    cpFinanceExtractBatchSetupRecordPage
                        .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                        .ValidateExtractOnMonday_YesOptionChecked()
                        .ValidateExtractOnMonday_NoOptionNotChecked()
                        .ValidateExtractOnTuesday_NoOptionChecked()
                        .ValidateExtractOnWednesday_NoOptionChecked()
                        .ValidateExtractOnThursday_NoOptionChecked()
                        .ValidateExtractOnFriday_NoOptionChecked()
                        .ValidateExtractOnSaturday_NoOptionChecked()
                        .ValidateExtractOnSunday_NoOptionChecked();

                    #endregion
                    break;
                case DayOfWeek.Tuesday:
                    #region If current date is a Tuesday

                    cpFinanceExtractBatchSetupRecordPage
                        .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                        .ValidateExtractOnSunday_NoOptionChecked()
                        .ValidateExtractOnMonday_NoOptionChecked()
                        .ValidateExtractOnTuesday_YesOptionChecked()
                        .ValidateExtractOnTuesday_NoOptionNotChecked()
                        .ValidateExtractOnWednesday_NoOptionChecked()
                        .ValidateExtractOnThursday_NoOptionChecked()
                        .ValidateExtractOnFriday_NoOptionChecked()
                        .ValidateExtractOnSaturday_NoOptionChecked();

                    #endregion
                    break;
                case DayOfWeek.Wednesday:
                    #region If current date is a Wednesday

                    cpFinanceExtractBatchSetupRecordPage
                        .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                        .ValidateExtractOnSunday_NoOptionChecked()
                        .ValidateExtractOnMonday_NoOptionChecked()
                        .ValidateExtractOnTuesday_NoOptionChecked()
                        .ValidateExtractOnWednesday_YesOptionChecked()
                        .ValidateExtractOnWednesday_NoOptionNotChecked()
                        .ValidateExtractOnThursday_NoOptionChecked()
                        .ValidateExtractOnFriday_NoOptionChecked()
                        .ValidateExtractOnSaturday_NoOptionChecked();

                    #endregion
                    break;
                case DayOfWeek.Thursday:
                    #region If current date is a Thursday
                    cpFinanceExtractBatchSetupRecordPage
                        .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                        .ValidateExtractOnMonday_NoOptionChecked()
                        .ValidateExtractOnTuesday_NoOptionChecked()
                        .ValidateExtractOnWednesday_NoOptionChecked()
                        .ValidateExtractOnThursday_YesOptionChecked()
                        .ValidateExtractOnThursday_NoOptionNotChecked()
                        .ValidateExtractOnFriday_NoOptionChecked()
                        .ValidateExtractOnSaturday_NoOptionChecked()
                        .ValidateExtractOnSunday_NoOptionChecked();

                    #endregion
                    break;
                case DayOfWeek.Friday:
                    #region If current date is a Friday
                    cpFinanceExtractBatchSetupRecordPage
                        .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                        .ValidateExtractOnMonday_NoOptionChecked()
                        .ValidateExtractOnTuesday_NoOptionChecked()
                        .ValidateExtractOnWednesday_NoOptionChecked()
                        .ValidateExtractOnThursday_NoOptionChecked()
                        .ValidateExtractOnFriday_YesOptionChecked()
                        .ValidateExtractOnFriday_NoOptionNotChecked()
                        .ValidateExtractOnSaturday_NoOptionChecked()
                        .ValidateExtractOnSunday_NoOptionChecked();

                    #endregion
                    break;
                case DayOfWeek.Saturday:
                    #region If current date is a Saturday
                    cpFinanceExtractBatchSetupRecordPage
                        .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                        .ValidateExtractOnMonday_NoOptionChecked()
                        .ValidateExtractOnTuesday_NoOptionChecked()
                        .ValidateExtractOnWednesday_NoOptionChecked()
                        .ValidateExtractOnThursday_NoOptionChecked()
                        .ValidateExtractOnFriday_NoOptionChecked()
                        .ValidateExtractOnSaturday_YesOptionChecked()
                        .ValidateExtractOnSaturday_NoOptionNotChecked()
                        .ValidateExtractOnSunday_NoOptionChecked();
                    #endregion
                    break;
                default:
                    break;
            }

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ValidateCareProviderExtractTypeLinkText("CPET_" + currentTimeString)
                .ValidateExtractReferenceText("ERef_" + currentTimeString)
                .ValidateExcludeZeroTransactionsFromExtract_YesOptionChecked()
                .ValidateVatGlCodeText("VatGl_" + currentTimeString)
                .ValidateInvoiceTransactionsGroupingSelectedText("Separated")
                .ValidateExcludeZeroTransactionsFromInvoice_YesOptionChecked()
                .ValidatePaymentTermsSelectedText("Calendar Months")
                .ValidatePaymentTermUnitsText("1")
                .ValidateShowRoomNumber_NoOptionChecked()
                .ValidateShowInvoiceText_YesOptionChecked()
                .ValidatePaymentDetailSelectedText("Bank Details")
                .ValidateShowWeeklyBreakdown_YesOptionChecked()
                .ValidateResponsibleTeamLinkText(_teamName);

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ValidateStartDateFieldIsDisabled(true)
                .ValidateStartDatePickerIsDisabled(true)
                .ValidateStartTimeFieldIsDisabled(true)
                .ValidateStartTimePickerIsDisabled(true)
                .ValidateCareProviderExtractNameLookupButtonIsDisabled(true)
                .ValidateResponsibleTeamLookupButtonIsDisabled(true)
                .ValidateFinanceExtractBatchSetupRecordTitle("CPEN_" + currentTimeString + " \\ " + DateTime.Now.ToString("dd'/'MM'/'yyyy"));

            #endregion

            #region Step 3

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ClickBackButton();

            cpFinanceExtractBatchSetupsPage
                .WaitForCPFinanceExtractBatchSetupsPageToLoad()
                .ClickNewRecordButton();

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .InsertTextOnStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnStartTime("01:00")
                .ClickCareProviderExtractNameLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("CPEN_" + currentTimeString, _cpExtractNameId);

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ClickCareProviderExtractTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("CPET_" + currentTimeString, _cpExtractTypeId);

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .InsertTextOnExtractReference("ERef_" + currentTimeString)
                .InsertTextOnVatGlCode("VatGl_" + currentTimeString)
                .ClickEmailSenderLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookFor("Teams")
                .SelectLookIn("Lookup View")
                .SearchAndSelectRecord(_teamName, _teamId);

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("There is already a Finance Extract Batch Setup record using this Extract Name and Extract Type which date overlaps with another record. Please correct as necessary.")
                .TapCloseButton();

            #endregion

            #region Step 4
            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            System.Threading.Thread.Sleep(2000);

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);
            var _cpFinanceExtractBatchSetupIds = dbHelper.careProviderFinanceExtractBatchSetup.GetByCareProviderExtractNameId(_cpExtractNameId);
            Assert.AreEqual(1, _cpFinanceExtractBatchSetupIds.Count);

            var _cpFinanceExtractBatchId = dbHelper
                                            .careProviderFinanceExtractBatch
                                            .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupIds[0], _cpExtractNameId);
            Guid _cpFinanceExtractBatchId1 = _cpFinanceExtractBatchId[0];

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceExtractBatchesPage();

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .InsertSearchQuery("CPEN_" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .ValidateRecordIsPresent(_cpFinanceExtractBatchId1.ToString(), true)
                .ValidateRecordCellContent(_cpFinanceExtractBatchId1.ToString(), 4, "New");


            //Get the schedule job id
            Guid processCpFinanceExtractsJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Process CP Finance Extract Batches")[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(processCpFinanceExtractsJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(processCpFinanceExtractsJobId);

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .InsertSearchQuery("CPEN_" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .ValidateRecordIsPresent(_cpFinanceExtractBatchId1.ToString(), true)
                .ValidateRecordCellContent(_cpFinanceExtractBatchId1.ToString(), 4, "Completed");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            cpFinanceAdminPage
                .WaitForCPFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingAreaButton()
                .ClickFinanceExtractBatchSetupsButton();

            cpFinanceExtractBatchSetupsPage
                .WaitForCPFinanceExtractBatchSetupsPageToLoad()
                .InsertTextOnQuickSearch("CPEN_" + currentTimeString)
                .ClickQuickSearchButton()
                .WaitForCPFinanceExtractBatchSetupsPageToLoad()
                .OpenRecord(_cpFinanceExtractBatchSetupIds[0]);

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ValidateEndDateFieldIsDisabled(false)
                .ValidateEndDatePickerIsDisabled(false);

            #endregion

            #region Step 5, Step 7

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .InsertTextOnEndDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Start date cannot be after or equal to End date")
                .TapCloseButton();

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .InsertTextOnEndDate(DateTime.Now.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Start date cannot be after or equal to End date")
                .TapCloseButton();

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .InsertTextOnEndDate(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ValidateEndDateText(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDateFieldIsDisabled(true)
                .ValidateEndDatePickerIsDisabled(true);

            #endregion

            #region Step 6
            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ClickBackButton();

            cpFinanceExtractBatchSetupsPage
                .WaitForCPFinanceExtractBatchSetupsPageToLoad()
                .ValidateSystemViewOptionNotPresent("Inactive Records");

            #endregion
        }

        #endregion


        #region https://advancedcsg.atlassian.net/browse/ACC-4487

        [TestProperty("JiraIssueID", "ACC-4551")]
        [Description("Test for Finance Extract Batch Setup - Test automation for Steps 8 to 12")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Business Objects")]
        [TestProperty("Screen2", "Finance Extract Batch Setups")]
        public void CPFinance_FinanceExtractBatchSetup_UITestMethod002()
        {
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Step 8

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            var ExtractNames = dbHelper.careProviderExtractName.GetAllCareProviderExtractName();
            int newExtractNameCode1 = ExtractNames.Count();

            while (dbHelper.careProviderExtractName.GetByCode(newExtractNameCode1).Any())
            {
                newExtractNameCode1 = newExtractNameCode1 + 1;
            }

            var ExtractTypes = dbHelper.careProviderExtractType.GetAllCareProviderExtractType();
            int newExtractTypeCode1 = ExtractTypes.Count();

            while (dbHelper.careProviderExtractType.GetByCode(newExtractTypeCode1).Any())
            {
                newExtractTypeCode1 = newExtractTypeCode1 + 1;
            }

            var _cpExtractNameId1 = commonMethodsDB.CreateCareProviderExtractName(_teamId, "CPEN_A1_" + currentTimeString, newExtractNameCode1, null, currentDate);
            var _cpExtractTypeId1 = commonMethodsDB.CreateCareProviderExtractType(_teamId, "CPET_A1_" + currentTimeString, newExtractTypeCode1, null, currentDate);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            var _cpFinanceExtractBatchSetupBOId = dbHelper.businessObject.GetBusinessObjectByName("CareProviderFinanceExtractBatchSetup")[0];

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCustomizationsSection();

            customizationsPage
                .WaitForCustomizationsPageToLoad()
                .ClickBusinessObjectsButton();

            businessObjectsPage
                .WaitForBusinessObjectsPageToLoad()
                .InsertQuickSearchText("CareProviderFinanceExtractBatchSetup")
                .ClickQuickSearchButton()
                .WaitForBusinessObjectsPageToLoad()
                .OpenRecord(_cpFinanceExtractBatchSetupBOId.ToString());

            businessObjectRecordPage
                .WaitForBusinessObjectRecordPageToLoad()
                .ValidateTypeFieldValue("Setup Data")
                .ValidateSingularNameFieldValue("Finance Extract Batch Setup")
                .ValidatePluralNameFieldValue("Finance Extract Batch Setups")
                .ValidateIconFieldValue("")
                .ValidateBusinessModuleFieldValue("Care Provider Invoicing");

            #endregion

            #region Step 9

            var _cpFinanceExtractBatchSetupId1 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, currentDate, new TimeSpan(2, 0, 0), null, _cpExtractNameId1, 1, _cpExtractTypeId1, _invoiceGenerateTemplate_MailMergeId);

            ExtractNames = dbHelper.careProviderExtractName.GetAllCareProviderExtractName();
            int newExtractNameCode2 = ExtractNames.Count();

            while (dbHelper.careProviderExtractName.GetByCode(newExtractNameCode2).Any())
            {
                newExtractNameCode2 = newExtractNameCode2 + 1;
            }

            ExtractTypes = dbHelper.careProviderExtractType.GetAllCareProviderExtractType();
            int newExtractTypeCode2 = ExtractTypes.Count();

            while (dbHelper.careProviderExtractType.GetByCode(newExtractTypeCode2).Any())
            {
                newExtractTypeCode2 = newExtractTypeCode2 + 1;
            }
            var _cpExtractNameId2 = commonMethodsDB.CreateCareProviderExtractName(_teamId, "CPEN_A2_" + currentTimeString, newExtractNameCode2, null, currentDate);
            var _cpExtractTypeId2 = commonMethodsDB.CreateCareProviderExtractType(_teamId, "CPET_A2_" + currentTimeString, newExtractTypeCode2, null, currentDate);
            var _cpFinanceExtractBatchSetupId2 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, currentDate, new TimeSpan(1, 0, 0), null, _cpExtractNameId2, 1, _cpExtractTypeId2, _invoiceGenerateTemplate_MailMergeId);

            ExtractNames = dbHelper.careProviderExtractName.GetAllCareProviderExtractName();
            int newExtractNameCode3 = ExtractNames.Count();

            while (dbHelper.careProviderExtractName.GetByCode(newExtractNameCode3).Any())
            {
                newExtractNameCode3 = newExtractNameCode3 + 1;
            }

            ExtractTypes = dbHelper.careProviderExtractType.GetAllCareProviderExtractType();
            int newExtractTypeCode3 = ExtractTypes.Count();

            while (dbHelper.careProviderExtractType.GetByCode(newExtractTypeCode3).Any())
            {
                newExtractTypeCode3 = newExtractTypeCode3 + 1;
            }
            var _cpExtractNameId3 = commonMethodsDB.CreateCareProviderExtractName(_teamId, "CPEN_A3_" + currentTimeString, newExtractNameCode3, null, currentDate);
            var _cpExtractTypeId3 = commonMethodsDB.CreateCareProviderExtractType(_teamId, "CPET_A3_" + currentTimeString, newExtractTypeCode3, null, currentDate);
            var _cpFinanceExtractBatchSetupId3 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, currentDate.AddDays(1), new TimeSpan(9, 0, 0), null, _cpExtractNameId3, 1, _cpExtractTypeId3, _invoiceGenerateTemplate_MailMergeId);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            cpFinanceAdminPage
                .WaitForCPFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingAreaButton()
                .ClickFinanceExtractBatchSetupsButton();

            cpFinanceExtractBatchSetupsPage
                .WaitForCPFinanceExtractBatchSetupsPageToLoad()
                .RecordsPageValidateHeaderCellText(2, "Extract Name")
                .ResultsPageValidateHeaderCellSortAscendingOrder(2, "Extract Name")
                .RecordsPageValidateHeaderCellText(3, "Start Date")
                .ResultsPageValidateHeaderCellSortDescendingOrder(3, "Start Date")
                .RecordsPageValidateHeaderCellText(4, "End Date")
                .RecordsPageValidateHeaderCellText(5, "Extract Type")
                .RecordsPageValidateHeaderCellText(6, "Bespoke Charge Extract Name")
                .RecordsPageValidateHeaderCellText(7, "Extract Frequency")
                .RecordsPageValidateHeaderCellText(8, "Extract Reference")
                .RecordsPageValidateHeaderCellText(9, "Invoice Template")
                .RecordsPageValidateHeaderCellText(10, "Invoice Transactions Grouping")
                .RecordsPageValidateHeaderCellText(11, "Generate and Email Invoices Automatically")
                .RecordsPageValidateHeaderCellText(12, "Payment Detail to Show")
                .RecordsPageValidateHeaderCellText(13, "Show Weekly Breakdown?")
                .RecordsPageValidateHeaderCellText(14, "Payment Terms")
                .RecordsPageValidateHeaderCellText(15, "Payment Term Units")
                .RecordsPageValidateHeaderCellText(16, "Show Room Number?")
                .RecordsPageValidateHeaderCellText(17, "Show Invoice Text?")
                .RecordsPageValidateHeaderCellText(18, "Responsible Team")
                .RecordsPageValidateHeaderCellText(19, "Modified On");

            cpFinanceExtractBatchSetupsPage
                .WaitForCPFinanceExtractBatchSetupsPageToLoad()
                .InsertTextOnQuickSearch("*" + currentTimeString + "*")
                .ClickQuickSearchButton()
                .ValidateRecordVisible(1, _cpFinanceExtractBatchSetupId3.ToString(), true)
                .ValidateRecordVisible(2, _cpFinanceExtractBatchSetupId1.ToString(), true)
                .ValidateRecordVisible(3, _cpFinanceExtractBatchSetupId2.ToString(), true);

            #endregion

            #region Step 10

            cpFinanceExtractBatchSetupsPage
                .WaitForCPFinanceExtractBatchSetupsPageToLoad()
                .ClickNewRecordButton();

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ClickCareProviderExtractNameLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndValidateRecordPresentOrNot("CPEN_A1_" + currentTimeString, _cpExtractNameId1, true)
                .SearchAndValidateRecordPresentOrNot("CPEN_A2_" + currentTimeString, _cpExtractNameId2, true)
                .SearchAndValidateRecordPresentOrNot("CPEN_A3_" + currentTimeString, _cpExtractNameId3, true)
                .ClickCloseButton();

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ClickCareProviderExtractTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndValidateRecordPresentOrNot("CPET_A1_" + currentTimeString, _cpExtractTypeId1, true)
                .SearchAndValidateRecordPresentOrNot("CPET_A2_" + currentTimeString, _cpExtractTypeId2, true)
                .SearchAndValidateRecordPresentOrNot("CPET_A3_" + currentTimeString, _cpExtractTypeId3, true)
                .ClickCloseButton();

            var _mailMergeTemplateId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ClickInvoiceTemplateLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndValidateRecordPresentOrNot("Care Provider Finance Invoice Generic Template", _mailMergeTemplateId, true)
                .ClickCloseButton();

            var cpFinanceInvoiceExtractFrequencyOptionSetId = dbHelper.optionSet.GetOptionSetIdByName("CP Finance Invoice Extract Frequency")[0];
            var optionSetValueId1 = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(cpFinanceInvoiceExtractFrequencyOptionSetId, "Every Week")[0];
            string optionSetValue_EveryWeek_Text = (string)dbHelper.optionsetValue.GetOptionsetValueByID(optionSetValueId1, "text")["text"];

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ValidateExtractFrequencyOptions(optionSetValue_EveryWeek_Text)
                .ValidateExtractFrequencyOptionSetId(cpFinanceInvoiceExtractFrequencyOptionSetId);

            var cpBatchTransactionGroupingOptionSetId = dbHelper.optionSet.GetOptionSetIdByName("Finance Extract Batch Transaction Grouping")[0];

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ValidateInvoiceTransactionsGroupingOptions("Separated")
                .ValidateInvoiceTransactionsGroupingOptions("Grouped")
                .ValidateInvoiceTransactionsGroupingOptionSetId(cpBatchTransactionGroupingOptionSetId);

            var cpFinanceExtractBatchPaymentDetailOptionSetId = dbHelper.optionSet.GetOptionSetIdByName("CP Finance Extract Batch Payment Detail")[0];

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ValidatePaymentDetailToShowOptions("Bank Details")
                .ValidatePaymentDetailToShowOptions("Remittance Details")
                .ValidatePaymentDetailToShowOptionSetId(cpFinanceExtractBatchPaymentDetailOptionSetId);

            #endregion

            #region Step 11
            cpFinanceExtractBatchSetupRecordPage
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            cpFinanceExtractBatchSetupsPage
                .WaitForCPFinanceExtractBatchSetupsPageToLoad()
                .ValidateToolbarOptionsDisplayed();

            cpFinanceExtractBatchSetupsPage
                .WaitForCPFinanceExtractBatchSetupsPageToLoad()
                .OpenRecord(_cpFinanceExtractBatchSetupId1);

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ValidateToolbarOptionsDisplayed();

            dbHelper.careProviderFinanceExtractBatchSetup.DeleteCareProviderFinanceExtractBatchSetup(_cpFinanceExtractBatchSetupId1);
            dbHelper.careProviderFinanceExtractBatchSetup.DeleteCareProviderFinanceExtractBatchSetup(_cpFinanceExtractBatchSetupId2);
            dbHelper.careProviderFinanceExtractBatchSetup.DeleteCareProviderFinanceExtractBatchSetup(_cpFinanceExtractBatchSetupId3);

            #endregion

            #region Step 12
            ExtractNames = dbHelper.careProviderExtractName.GetAllCareProviderExtractName();
            int newExtractNameCode4 = ExtractNames.Count();

            while (dbHelper.careProviderExtractName.GetByCode(newExtractNameCode4).Any())
            {
                newExtractNameCode4 = newExtractNameCode4 + 1;
            }

            ExtractTypes = dbHelper.careProviderExtractType.GetAllCareProviderExtractType();
            int newExtractTypeCode4 = ExtractTypes.Count();

            while (dbHelper.careProviderExtractType.GetByCode(newExtractTypeCode4).Any())
            {
                newExtractTypeCode4 = newExtractTypeCode4 + 1;
            }
            var _cpExtractNameId4 = commonMethodsDB.CreateCareProviderExtractName(_teamId, "Z_CPEN_NoDel1_" + currentTimeString, newExtractNameCode4, null, new DateTime(2023, 1, 1));
            var _cpExtractTypeId4 = commonMethodsDB.CreateCareProviderExtractType(_teamId, "Z_CPET_NoDel2_" + currentTimeString, newExtractTypeCode4, null, new DateTime(2023, 1, 1));
            var _cpFinanceExtractBatchSetupId4 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, currentDate, new TimeSpan(3, 0, 0), null, _cpExtractNameId4, 1, _cpExtractTypeId4, _invoiceGenerateTemplate_MailMergeId);

            //Get the schedule job id
            Guid processCpFinanceExtractsJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Process CP Finance Extract Batches")[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(processCpFinanceExtractsJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(processCpFinanceExtractsJobId);

            var _completedCpFinanceExtractBatchId = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId4, _cpExtractNameId4, 2);
            Assert.AreEqual(1, _completedCpFinanceExtractBatchId.Count);

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            cpFinanceExtractBatchSetupsPage
                .WaitForCPFinanceExtractBatchSetupsPageToLoad()
                .InsertTextOnQuickSearch("*" + currentTimeString + "*")
                .ClickQuickSearchButton()
                .OpenRecord(_cpFinanceExtractBatchSetupId4);

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Can’t delete this record as there is at least one Finance Extract Batch record linked to this setup with status Completed.")
                .TapCloseButton();

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            ExtractNames = dbHelper.careProviderExtractName.GetAllCareProviderExtractName();
            int newExtractNameCode5 = ExtractNames.Count();

            while (dbHelper.careProviderExtractName.GetByCode(newExtractNameCode5).Any())
            {
                newExtractNameCode5 = newExtractNameCode5 + 1;
            }

            ExtractTypes = dbHelper.careProviderExtractType.GetAllCareProviderExtractType();
            int newExtractTypeCode5 = ExtractTypes.Count();

            while (dbHelper.careProviderExtractType.GetByCode(newExtractTypeCode5).Any())
            {
                newExtractTypeCode5 = newExtractTypeCode5 + 1;
            }
            var _cpExtractNameId5 = commonMethodsDB.CreateCareProviderExtractName(_teamId, "Z1_CPEN_NoDel1_" + currentTimeString, newExtractNameCode5, null, new DateTime(2023, 1, 1));
            var _cpExtractTypeId5 = commonMethodsDB.CreateCareProviderExtractType(_teamId, "Z1_CPET_NoDel2_" + currentTimeString, newExtractTypeCode5, null, new DateTime(2023, 1, 1));
            var _cpFinanceExtractBatchSetupId5 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, currentDate, new TimeSpan(3, 0, 0), null, _cpExtractNameId5, 1, _cpExtractTypeId5, _invoiceGenerateTemplate_MailMergeId);

            cpFinanceExtractBatchSetupsPage
                .WaitForCPFinanceExtractBatchSetupsPageToLoad()
                .InsertTextOnQuickSearch("*" + currentTimeString + "*")
                .ClickQuickSearchButton()
                .OpenRecord(_cpFinanceExtractBatchSetupId5);

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            cpFinanceExtractBatchSetupsPage
                .WaitForCPFinanceExtractBatchSetupsPageToLoad()
                .InsertTextOnQuickSearch("*" + currentTimeString + "*")
                .ClickQuickSearchButton()
                .WaitForCPFinanceExtractBatchSetupsPageToLoad()
                .ValidateRecordNotVisible(_cpFinanceExtractBatchSetupId5);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4601

        [TestProperty("JiraIssueID", "ACC-4607")]
        [Description("Test for Finance Extract Batch Setup - Test automation for Steps 13 to 18")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Advanced Search")]
        [TestProperty("Screen2", "Finance Extract Batch Setups")]
        [TestProperty("Screen3", "Finance Extract Batches")]
        public void CPFinance_FinanceExtractBatchSetup_UITestMethod003()
        {
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Step 13 to Step 15

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            var ExtractNames = dbHelper.careProviderExtractName.GetAllCareProviderExtractName();
            int newExtractNameCode1 = ExtractNames.Count();

            while (dbHelper.careProviderExtractName.GetByCode(newExtractNameCode1).Any())
            {
                newExtractNameCode1 = newExtractNameCode1 + 1;
            }

            var ExtractTypes = dbHelper.careProviderExtractType.GetAllCareProviderExtractType();
            int newExtractTypeCode1 = ExtractTypes.Count();

            while (dbHelper.careProviderExtractType.GetByCode(newExtractTypeCode1).Any())
            {
                newExtractTypeCode1 = newExtractTypeCode1 + 1;
            }

            var _cpExtractNameId1 = commonMethodsDB.CreateCareProviderExtractName(_teamId, "CPEN_A1_" + currentTimeString, newExtractNameCode1, null, currentDate);
            var _cpExtractTypeId1 = commonMethodsDB.CreateCareProviderExtractType(_teamId, "CPET_A1_" + currentTimeString, newExtractTypeCode1, null, currentDate);
            var _cpFinanceExtractBatchSetupId1 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, currentDate, new TimeSpan(2, 0, 0), null, _cpExtractNameId1, 1, _cpExtractTypeId1, _invoiceGenerateTemplate_MailMergeId);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Finance Extract Batch Setups")
                .SelectFilter("1", "Name")
                .SelectOperator("1", "Equals")
                .InsertRuleValueText("1", "CPEN_A1_" + currentTimeString + " \\ " + currentDate.ToString("dd'/'MM'/'yyyy"))
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_cpFinanceExtractBatchSetupId1.ToString());

            advanceSearchPage
                .ResultsPageValidateHeaderCellText(2, "Extract Name")
                .ResultsPageValidateHeaderCellText(3, "Start Date")
                .ResultsPageValidateHeaderCellText(4, "End Date")
                .ResultsPageValidateHeaderCellText(5, "Extract Type")
                .ResultsPageValidateHeaderCellText(6, "Bespoke Charge Extract Name")
                .ResultsPageValidateHeaderCellText(7, "Extract Frequency")
                .ResultsPageValidateHeaderCellText(8, "Extract Reference")
                .ResultsPageValidateHeaderCellText(9, "Invoice Template")
                .ResultsPageValidateHeaderCellText(10, "Invoice Transactions Grouping")
                .ResultsPageValidateHeaderCellText(11, "Generate and Email Invoices Automatically")
                .ResultsPageValidateHeaderCellText(12, "Payment Detail to Show")
                .ResultsPageValidateHeaderCellText(13, "Show Weekly Breakdown?")
                .ResultsPageValidateHeaderCellText(14, "Payment Terms")
                .ResultsPageValidateHeaderCellText(15, "Payment Term Units")
                .ResultsPageValidateHeaderCellText(16, "Show Room Number?")
                .ResultsPageValidateHeaderCellText(17, "Show Invoice Text?")
                .ResultsPageValidateHeaderCellText(18, "Responsible Team")
                .ResultsPageValidateHeaderCellText(19, "Modified On");

            advanceSearchPage
                .ClickBackButton_ResultsPage();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickDeleteButton()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ResultsPageValidateHeaderCellText(2, "Extract Name")
                .ResultsPageValidateHeaderCellText(3, "Start Date")
                .ResultsPageValidateHeaderCellText(4, "End Date")
                .ResultsPageValidateHeaderCellText(5, "Extract Type")
                .ResultsPageValidateHeaderCellText(6, "Bespoke Charge Extract Name")
                .ResultsPageValidateHeaderCellText(7, "Extract Frequency")
                .ResultsPageValidateHeaderCellText(8, "Extract Reference")
                .ResultsPageValidateHeaderCellText(9, "Invoice Template")
                .ResultsPageValidateHeaderCellText(10, "Invoice Transactions Grouping")
                .ResultsPageValidateHeaderCellText(11, "Generate and Email Invoices Automatically")
                .ResultsPageValidateHeaderCellText(12, "Payment Detail to Show")
                .ResultsPageValidateHeaderCellText(13, "Show Weekly Breakdown?")
                .ResultsPageValidateHeaderCellText(14, "Payment Terms")
                .ResultsPageValidateHeaderCellText(15, "Payment Term Units")
                .ResultsPageValidateHeaderCellText(16, "Show Room Number?")
                .ResultsPageValidateHeaderCellText(17, "Show Invoice Text?")
                .ResultsPageValidateHeaderCellText(18, "Responsible Team")
                .ResultsPageValidateHeaderCellText(19, "Modified On");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Finance Extract Batch Setups")
                .SelectFilter("1", "Name")
                .SelectOperator("1", "Equals")
                .InsertRuleValueText("1", "CPEN_A1_" + currentTimeString + " \\ " + currentDate.ToString("dd'/'MM'/'yyyy"))
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .OpenRecord(_cpFinanceExtractBatchSetupId1.ToString());

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoadFromAdvancedSearch()
                .ValidateExcludeZeroTransactionsFromExtract_YesOptionChecked()
                .ValidatePaymentTermsSelectedText("Calendar Months")
                .ValidatePaymentTermUnitsText("1")
                .ValidateShowRoomNumber_NoOptionChecked()
                .ValidateShowInvoiceText_YesOptionChecked()
                .ValidatePaymentDetailSelectedText("Bank Details")
                .ValidateShowWeeklyBreakdown_YesOptionChecked();

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoadFromAdvancedSearch()
                .ClickExcludeZeroTransactionsFromInvoice_NoOption()
                .SelectPaymentTerms("Days")
                .InsertTextOnPaymentTermUnits("2")
                .ClickShowRoomNumber_YesOption()
                .ClickShowInvoiceText_NoOption()
                .SelectPaymentDetail("Remittance Details")
                .ClickShowWeeklyBreakdown_NoOption()
                .ClickEmailSenderLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookFor("Teams")
                .SelectLookIn("Lookup View")
                .SearchAndSelectRecord(_teamName, _teamId);

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()

                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoadFromAdvancedSearch();

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoadFromAdvancedSearch()
                .ValidateExcludeZeroTransactionsFromInvoice_NoOptionChecked()
                .ValidatePaymentTermsSelectedText("Days")
                .ValidatePaymentTermUnitsText("2")
                .ValidateShowRoomNumber_YesOptionChecked()
                .ValidateShowInvoiceText_NoOptionChecked()
                .ValidatePaymentDetailSelectedText("Remittance Details")
                .ValidateShowWeeklyBreakdown_NoOptionChecked();

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);
            var _cpFinanceExtractBatchSetupIds = dbHelper.careProviderFinanceExtractBatchSetup.GetByCareProviderExtractNameId(_cpExtractNameId1);
            Assert.AreEqual(1, _cpFinanceExtractBatchSetupIds.Count);

            var _cpFinanceExtractBatchId = dbHelper
                                            .careProviderFinanceExtractBatch
                                            .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupIds[0], _cpExtractNameId1);
            Guid _cpFinanceExtractBatchId1 = _cpFinanceExtractBatchId[0];

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceExtractBatchesPage();

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .InsertSearchQuery("CPEN_A1_" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .ValidateRecordIsPresent(_cpFinanceExtractBatchId1.ToString(), true)
                .ValidateRecordCellContent(_cpFinanceExtractBatchId1.ToString(), 4, "New");


            //Get the schedule job id
            Guid processCpFinanceExtractsJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Process CP Finance Extract Batches")[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(processCpFinanceExtractsJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(processCpFinanceExtractsJobId);


            _cpFinanceExtractBatchId = dbHelper
                                            .careProviderFinanceExtractBatch
                                            .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupIds[0], _cpExtractNameId1, 1);

            Assert.AreEqual(1, _cpFinanceExtractBatchId.Count);
            Guid _cpFinanceExtractBatchId2 = _cpFinanceExtractBatchId[0];

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .InsertSearchQuery("CPEN_A1_" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .ValidateRecordIsPresent(_cpFinanceExtractBatchId1.ToString(), true)
                .ValidateRecordIsPresent(_cpFinanceExtractBatchId2.ToString(), true)
                .ValidateRecordCellContent(_cpFinanceExtractBatchId1.ToString(), 4, "Completed")
                .ValidateRecordCellContent(_cpFinanceExtractBatchId2.ToString(), 4, "New");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            cpFinanceAdminPage
                .WaitForCPFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingAreaButton()
                .ClickFinanceExtractBatchSetupsButton();

            cpFinanceExtractBatchSetupsPage
                .WaitForCPFinanceExtractBatchSetupsPageToLoad()
                .InsertTextOnQuickSearch("CPEN_A1_" + currentTimeString)
                .ClickQuickSearchButton()
                .OpenRecord(_cpFinanceExtractBatchSetupId1);

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ValidateEndDateFieldIsDisabled(false)
                .ValidateEndDatePickerIsDisabled(false)
                .InsertTextOnEndDate(currentDate.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad();


            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ValidateEndDateText(currentDate.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDateFieldIsDisabled(true)
                .ValidateEndDatePickerIsDisabled(true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceExtractBatchesPage();

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .InsertSearchQuery("CPEN_A1_" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .ValidateRecordIsPresent(_cpFinanceExtractBatchId1.ToString(), true)
                .ValidateRecordIsPresent(_cpFinanceExtractBatchId2.ToString(), false);

            _cpFinanceExtractBatchId = dbHelper
                                            .careProviderFinanceExtractBatch
                                            .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupIds[0], _cpExtractNameId1, 1);

            Assert.AreEqual(0, _cpFinanceExtractBatchId.Count);

            #endregion

            #region Step 16

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            cpFinanceAdminPage
                .WaitForCPFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingAreaButton()
                .ClickFinanceExtractBatchSetupsButton();

            cpFinanceExtractBatchSetupsPage
                .WaitForCPFinanceExtractBatchSetupsPageToLoad()
                .InsertTextOnQuickSearch("CPEN_A1_" + currentTimeString)
                .ClickQuickSearchButton()
                .OpenRecord(_cpFinanceExtractBatchSetupId1);

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ValidateInvoiceTemplateLinkText("Care Provider Finance Invoice Generic Template")
                .ValidateMandatoryFieldIsDisplayed("Payment Detail to Show")
                .ValidateMandatoryFieldIsDisplayed("Payment Terms")
                .ValidateMandatoryFieldIsDisplayed("Payment Term Units");

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .SelectPaymentTerms("")
                .ValidatePaymentTermsMandatoryFieldNotificationMessageIsDisplayed("Please fill out this field.")
                .ValidateMandatoryFieldIsDisplayed("Payment Term Units", false)
                .ValidatePaymentTermUnitsFieldIsDisabled(true);

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ClickInvoiceTemplateLinkClearButton();

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ValidateInvoiceTemplateMandatoryFieldNotificationMessageIsDisplayed("Please fill out this field.")
                .ValidateMandatoryFieldIsDisplayed("Payment Detail to Show", false)
                .ValidateMandatoryFieldIsDisplayed("Payment Terms", false)
                .ValidateMandatoryFieldIsDisplayed("Payment Term Units", false)
                .ValidatePaymentTermsFieldIsDisabled(true)
                .ValidatePaymentTermUnitsFieldIsDisabled(true)
                .ValidatePaymentDetailToShowFieldIsDisabled(false);

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ClickInvoiceTemplateLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Care Provider Finance Invoice Generic Template")
                .TapSearchButton()
                .SelectResultElement(_invoiceGenerateTemplate_MailMergeId);

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .SelectPaymentTerms("Calendar Months")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad();

            #endregion

            #region Step 17

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ValidatePaymentDetailToShowOptions("Bank Details")
                .ValidatePaymentDetailToShowOptions("Remittance Details");

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ValidatePaymentTermsOptions("Calendar Months")
                .ValidatePaymentTermsOptions("Days");

            #endregion

            #region Step 18
            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ValidatePaymentTermsUnitsFieldRangeValue("0,99");

            #endregion
        }

        #endregion
    }
}
